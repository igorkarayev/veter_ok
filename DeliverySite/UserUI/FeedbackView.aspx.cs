using Delivery.BLL;
using Delivery.BLL.Helpers;
using Delivery.BLL.StaticMethods;
using Delivery.DAL;
using Delivery.DAL.DataBaseObjects;
using DeliverySite.Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Delivery.UserUI
{
    public partial class FeedbackView : UserBasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            btnCreate.Click += bntCreate_Click;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            OtherMethods.ActiveRightMenuStyleChanche("hlFeedback", this.Page);
            Page.Title = PagesTitles.UserFeedbackView + BackendHelper.TagToValue("page_title_part");
            if (String.IsNullOrEmpty(ActivatedProfilesCount) || Convert.ToInt32(ActivatedProfilesCount) == 0)
            {
                Response.Redirect("~/usernotification/12");
            }

            if (!IsPostBack)
            {
                var id = Page.Request.Params["id"];
                var feedback = new Feedback { ID = Convert.ToInt32(id) };
                feedback.GetById();
                lblID.Text = feedback.ID.ToString();
                lblBody.Text = feedback.Body;
                lblTitle.Text = feedback.Title;
                lblCreateDate.Text = Convert.ToDateTime(feedback.CreateDate).ToString("dd.MM.yyyy HH:mm");
                lblPriority.Text = Feedback.Priorities.FirstOrDefault(u => u.Key == feedback.PriorityID).Value;
                lblStatus.Text = Feedback.Statuses.FirstOrDefault(u => u.Key == feedback.StatusID).Value;
                lblType.Text = Feedback.Types.FirstOrDefault(u => u.Key == feedback.TypeID).Value;
                if (!String.IsNullOrEmpty(feedback.PhotoName))
                {
                    imgPhoto.ImageUrl = "~/Images/Feedback/" + feedback.PhotoName;
                    hlImage.NavigateUrl = "~/Images/Feedback/" + feedback.PhotoName;
                    pnlImage.Visible = true;
                }

                var dm = new DataManager();
                var dataSet = dm.QueryWithReturnDataSet("SELECT FC.*, (SELECT CONCAT(R.NameOnRuss,\" \",U.Name,\" \", U.Family) FROM users U JOIN roles R On U.Role = R.Name WHERE U.ID = FC.UserID) as Fio FROM feedbackcomments FC WHERE FC.FeedbackID = '" + id + "' ORDER BY CreateDate DESC;");
                lvAllComments.DataSource = dataSet;
                lvAllComments.DataBind();

                dm.QueryWithoutReturnData(null, "UPDATE feedbackcomments SET IsViewed = 1 WHERE FeedbackID = '" + id + "';");

                if (feedback.StatusID == 1)
                {
                    tbComment.Visible = false;
                    btnCreate.Visible = false;
                }
            }
        }

        public void bntCreate_Click(Object sender, EventArgs e)
        {
            var userInSession = (Users)Session["userinsession"];
            var id = Page.Request.Params["id"];

            var feedbackcomment = new FeedbackComments()
            {
                Comment = BbCode.BBcodeToHtml(tbComment.Text),
                FeedbackID = Convert.ToInt32(id),
                UserID = userInSession.ID,
                IsViewed = 1
            };
            feedbackcomment.Create();

            var feedback = new Feedback()
            {
                ID = Convert.ToInt32(id),
            };
            feedback.GetById();
            feedback.ChangeDate = DateTime.Now;
            feedback.Update();

            var roles = (List<Roles>)HttpContext.Current.Application["RolesList"];
            if (feedback.TypeID == 0)
            {
                SendNotification(roles.Where(u => u.Feedback0 == 1).ToList(), feedback, feedbackcomment);
            }
            if (feedback.TypeID == 1)
            {
                SendNotification(roles.Where(u => u.Feedback1 == 1).ToList(), feedback, feedbackcomment);
            }
            if (feedback.TypeID == 2)
            {
                SendNotification(roles.Where(u => u.Feedback2 == 1).ToList(), feedback, feedbackcomment);
            }
            if (feedback.TypeID == 3)
            {
                SendNotification(roles.Where(u => u.Feedback3 == 1).ToList(), feedback, feedbackcomment);
            }
            if (feedback.TypeID == 4)
            {
                SendNotification(roles.Where(u => u.Feedback4 == 1).ToList(), feedback, feedbackcomment);
            }
            if (feedback.TypeID == 5)
            {
                SendNotification(roles.Where(u => u.Feedback5 == 1).ToList(), feedback, feedbackcomment);
            }
            if (feedback.TypeID == 6)
            {
                SendNotification(roles.Where(u => u.Feedback6 == 1).ToList(), feedback, feedbackcomment);
            }

            Page.Response.Redirect("~/UserUI/FeedbackView.aspx?id=" + id);
        }

        public void SendNotification(List<Roles> responsibleRole, Feedback feedback, FeedbackComments comment)
        {
            var roleNameList = responsibleRole.Select(role => role.Name).ToList();
            if (roleNameList.Count == 0)
                return;
            var roleNameSqlString = roleNameList.Aggregate(String.Empty, (current, roleName) => current + ("\"" + roleName + "\","));
            var ds = new DataManager();
            var usersDs = ds.QueryWithReturnDataSet(
                String.Format("SELECT `Email` FROM users WHERE Role in ({0}) AND Status = 2", roleNameSqlString.Remove(roleNameSqlString.Length - 1, 1))
                );
            var emailsString = usersDs.Tables[0].Rows.Cast<DataRow>().Aggregate(String.Empty, (current, row) => current + (row["Email"] + ","));
            var emailArray = emailsString.Remove(emailsString.Length - 1, 1).Split(new[] { ',' });
            if (emailArray.ToList().Count == 0)
                return;
            var messageBody = String.Format("<table>" +
                                            "<tr><td colspan='2' style='padding-bottom: 15px; vertical-align: top'>К обращению <a href='http://" + BackendHelper.TagToValue("current_app_address") + "/ManagerUI/FeedbackView.aspx?id=" + feedback.SecureID + "' target='_new'><i>{2}</i></a> поступил новый комментарий</td></tr>" +
                                            "<tr><td style='vertical-align: top'>Приоритет:</td><td><b>{0}</b></td></tr>" +
                                            "<tr><td style='vertical-align: top'>Тип:</td><td><b>{1}</b> </td></tr>" +
                                            "<tr><td style='vertical-align: top'>Содержание:</td><td><b>{3}</b> </td></tr>" +
                                            "<tr><td style='padding-top: 15px; vertical-align: top'>Комментарий:</td><td style='padding-top: 15px;'><b>{4}</b> </td></tr>" +
                                            "<tr><td colspan='2' style='padding-top: 15px; vertical-align: top'>Перейдите по <a href='http://" + BackendHelper.TagToValue("current_app_address") + "/ManagerUI/FeedbackView.aspx?id=" + feedback.SecureID + "' target='_new'>этой</a> ссылке для ответа на комментарий.</td></tr></table>",
                                            Feedback.Priorities.FirstOrDefault(u => u.Key == feedback.PriorityID).Value,
                                            Feedback.Types.FirstOrDefault(u => u.Key == feedback.TypeID).Value,
                                            feedback.Title,
                                            feedback.Body,
                                            comment.Comment);
            EmailMethods.MailSendHTML("Новый комментарий к обращению", messageBody, emailArray);
        }
    }
}