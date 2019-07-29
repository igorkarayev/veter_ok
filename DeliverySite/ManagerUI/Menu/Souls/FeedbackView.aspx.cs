using Delivery.BLL;
using Delivery.BLL.Helpers;
using Delivery.BLL.StaticMethods;
using Delivery.DAL;
using Delivery.DAL.DataBaseObjects;
using DeliverySite.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Delivery.ManagerUI.Menu.Souls
{
    public partial class FeedbackView : ManagerBasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            btnCreate.Click += bntCreate_Click;
            btnClose.Click += bntClose_Click;
            btnDelete.Click += bntDelete_Click;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            OtherMethods.ActiveRightMenuStyleChanche("hlSouls", this.Page);
            OtherMethods.ActiveRightMenuStyleChanche("hlFeedback", this.Page);
            Page.Title = PagesTitles.UserFeedbackView + BackendHelper.TagToValue("page_title_part");

            var id = Page.Request.Params["id"];
            var feedback = new Feedback();
            try
            {
                feedback = new Feedback { ID = Convert.ToInt32(id) };
                feedback.GetById();
            }
            catch (Exception)
            {
                feedback = new Feedback { SecureID = id };
                feedback.GetBySecureID();
            }

            if (feedback.ID == 0)
            {
                feedback = new Feedback { SecureID = id };
                feedback.GetBySecureID();
            }

            #region Настройки доступа к странице
            var userInSession = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());
            if (currentRole.FeedbackFullAccess == 0 && (
                    (feedback.TypeID == 0 && currentRole.Feedback0 == 0) ||
                    (feedback.TypeID == 1 && currentRole.Feedback1 == 0) ||
                    (feedback.TypeID == 2 && currentRole.Feedback2 == 0) ||
                    (feedback.TypeID == 3 && currentRole.Feedback3 == 0) ||
                    (feedback.TypeID == 4 && currentRole.Feedback4 == 0) ||
                    (feedback.TypeID == 5 && currentRole.Feedback5 == 0) ||
                    (feedback.TypeID == 6 && currentRole.Feedback6 == 0)
                    ))
            {
                Response.Redirect("~/Error.aspx?id=1");
            }
            #endregion

            if (!IsPostBack)
            {
                lblID.Text = feedback.ID.ToString();
                lblBody.Text = feedback.Body;
                lblTitle.Text = feedback.Title;
                lblCreateDate.Text = Convert.ToDateTime(feedback.CreateDate).ToString("dd.MM.yyyy HH:mm");
                lblPriority.Text = Feedback.Priorities.FirstOrDefault(u => u.Key == feedback.PriorityID).Value;
                lblStatus.Text = Feedback.Statuses.FirstOrDefault(u => u.Key == feedback.StatusID).Value;
                lblType.Text = Feedback.Types.FirstOrDefault(u => u.Key == feedback.TypeID).Value;
                lblUser.Text = UsersHelper.UserIDToFullName(feedback.UserID.ToString());
                hlUser.NavigateUrl = "~/ManagerUI/Menu/Souls/ClientEdit.aspx?id=" + feedback.UserID;
                if (!String.IsNullOrEmpty(feedback.PhotoName))
                {
                    imgPhoto.ImageUrl = "~/Images/Feedback/" + feedback.PhotoName;
                    hlImage.NavigateUrl = "~/Images/Feedback/" + feedback.PhotoName;
                    pnlImage.Visible = true;
                }

                if (currentRole.FeedbackFullAccess == 1)
                {
                    btnDelete.Visible = true;
                }

                if (feedback.StatusID == 1)
                {
                    tbComment.Visible = false;
                    btnClose.Visible = false;
                    btnCreate.Visible = false;
                }
            }

            var dm = new DataManager();
            var dataSet = dm.QueryWithReturnDataSet("SELECT FC.* FROM feedbackcomments FC WHERE FC.FeedbackID = '" + feedback.ID + "' ORDER BY CreateDate DESC;");
            lvAllComments.DataSource = dataSet;
            lvAllComments.DataBind();

            foreach (ListViewDataItem items in lvAllComments.Items)
            {
                var hlEdit = (HtmlAnchor)items.FindControl("hlEdit");
                var hfUserID = (HiddenField)items.FindControl("hfUserID");

                if (hfUserID.Value != userInSession.ID.ToString() || feedback.StatusID == 1)
                {
                    hlEdit.Visible = false;
                }
            }
        }

        public void bntCreate_Click(Object sender, EventArgs e)
        {
            var userInSession = (Users)Session["userinsession"];
            var id = Page.Request.Params["id"];
            Feedback feedback;
            try
            {
                feedback = new Feedback { ID = Convert.ToInt32(id) };
                feedback.GetById();
            }
            catch (Exception)
            {
                feedback = new Feedback { SecureID = id };
                feedback.GetBySecureID();
            }

            if (feedback.ID == 0)
            {
                feedback = new Feedback { SecureID = id };
                feedback.GetBySecureID();
            }

            var feedbackcomment = new FeedbackComments()
            {
                Comment = BbCode.BBcodeToHtml(tbComment.Text),
                FeedbackID = feedback.ID,
                UserID = userInSession.ID,
                IsViewed = 0
            };
            feedbackcomment.Create();
            feedback.ChangeDate = DateTime.Now;
            feedback.Update();
            var userToMail = new Users { ID = Convert.ToInt32(feedback.UserID) };
            userToMail.GetById();
            var messageBody = String.Format("<table class='table'>" +
                                            "<tr><td colspan='2' style='padding-bottom: 15px; vertical-align: top'>Поступил новый комментарий к вашему обращению <a href='http://{2}/UserUI/FeedbackView.aspx?id={3}' target='_new'><i>{0}</i></a></td></tr>" +
                                            "<tr><td style='vertical-align: top'>Содержание комментария:</td><td>{1}</td></tr>" +
                                            "<tr><td colspan='2' style='padding-top: 15px; vertical-align: top'>Перейдите по <a href='http://{2}/UserUI/FeedbackView.aspx?id={3}' target='new'>этой</a> ссылке для ответа на комментарий.</td></tr></table>",
                                            feedback.Title,
                                            feedbackcomment.Comment,
                                            BackendHelper.TagToValue("current_app_address"),
                                            feedback.ID);
            EmailMethods.MailSendHTML("Новый комментарий к вашему обращению", messageBody, userToMail.Email);
            Page.Response.Redirect("~/ManagerUI/Menu/Souls/FeedbackView.aspx?id=" + id);
        }


        public void bntDelete_Click(Object sender, EventArgs e)
        {
            var feedback = new Feedback();
            var id = Page.Request.Params["id"];
            var userInSession = (Users)Session["userinsession"];
            feedback.Delete(Convert.ToInt32(id), userInSession.ID, OtherMethods.GetIPAddress(), "FeedbackView");
            Page.Response.Redirect("~/ManagerUI/Menu/Souls/FeedbacksView.aspx");
        }

        public void bntClose_Click(Object sender, EventArgs e)
        {
            var id = Page.Request.Params["id"];
            var userInSession = (Users)Session["userinsession"];
            var feedback = new Feedback()
            {
                ID = Convert.ToInt32(id),
                StatusID = 1,
                ChangeDate = DateTime.Now
            };
            feedback.Update(userInSession.ID, OtherMethods.GetIPAddress(), "FeedbackView");
            Page.Response.Redirect("~/ManagerUI/Menu/Souls/FeedbacksView.aspx");
        }
    }
}