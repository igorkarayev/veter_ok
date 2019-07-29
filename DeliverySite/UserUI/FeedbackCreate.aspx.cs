using Delivery.BLL;
using Delivery.BLL.Helpers;
using Delivery.BLL.StaticMethods;
using Delivery.DAL;
using Delivery.DAL.DataBaseObjects;
using DeliverySite.Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Delivery.UserUI
{
    public partial class FeedbackCreate : UserBasePage
    {

        protected void Page_Init(object sender, EventArgs e)
        {
            btnCreate.Click += bntCreate_Click;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            OtherMethods.ActiveRightMenuStyleChanche("hlFeedback", this.Page);
            Page.Title = PagesTitles.UserFeedbackCreate + BackendHelper.TagToValue("page_title_part");
            if (String.IsNullOrEmpty(ActivatedProfilesCount) || Convert.ToInt32(ActivatedProfilesCount) == 0)
            {
                Response.Redirect("~/usernotification/12");
            }

            if (!IsPostBack)
            {
                ddlType.DataSource = Feedback.Types;
                ddlType.DataTextField = "Value";
                ddlType.DataValueField = "Key";
                ddlType.DataBind();
                if (Page.Request.Params["type"] != null && Page.Request.Params["type"] == "new_category")
                {
                    ddlType.SelectedValue = "2";
                }

                ddlPriority.DataSource = Feedback.Priorities;
                ddlPriority.DataTextField = "Value";
                ddlPriority.DataValueField = "Key";
                ddlPriority.DataBind();
            }

        }

        public void bntCreate_Click(Object sender, EventArgs e)
        {
            var fileName = String.Empty;
            if (fuImage.HasFile)
            {
                if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/Images/Feedback/")))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Images/Feedback/"));
                }
                if (fuImage.FileBytes.Length > 4000000)
                {
                    lblError.Text = "Размер картинки должен быть меньее 4 мб.";
                    return;
                }
                String ext = System.IO.Path.GetExtension(fuImage.FileName);
                if (ext.ToLower() != ".jpg" && ext.ToLower() != ".png" && ext.ToLower() != ".gif" && ext.ToLower() != ".jpeg")
                {
                    lblError.Text = "Загружать можно исключительно картинки.";
                    return;
                }
                fileName = OtherMethods.CreateUniqId(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")) + ext;
                fuImage.PostedFile.SaveAs(Server.MapPath("~/Images/Feedback/") + fileName);
            }
            var userInSession = (Users)Session["userinsession"];
            var feedback = new Feedback()
            {
                TypeID = Convert.ToInt32(ddlType.SelectedValue),
                PriorityID = Convert.ToInt32(ddlPriority.SelectedValue),
                StatusID = 0,
                Title = tbTitle.Text,
                Body = BbCode.BBcodeToHtml(tbBody.Text),
                UserID = userInSession.ID,
                PhotoName = fileName,
                ChangeDate = DateTime.Now,
                SecureID = OtherMethods.CreateFullUniqId(DateTime.Now.ToString("yyMdHms"))
            };
            feedback.Create();

            var roles = (List<Roles>)HttpContext.Current.Application["RolesList"];
            if (feedback.TypeID == 0)
            {
                SendNotification(roles.Where(u => u.Feedback0 == 1).ToList(), feedback);
            }
            if (feedback.TypeID == 1)
            {
                SendNotification(roles.Where(u => u.Feedback1 == 1).ToList(), feedback);
            }
            if (feedback.TypeID == 2)
            {
                SendNotification(roles.Where(u => u.Feedback2 == 1).ToList(), feedback);
            }
            if (feedback.TypeID == 3)
            {
                SendNotification(roles.Where(u => u.Feedback3 == 1).ToList(), feedback);
            }
            if (feedback.TypeID == 4)
            {
                SendNotification(roles.Where(u => u.Feedback4 == 1).ToList(), feedback);
            }
            if (feedback.TypeID == 5)
            {
                SendNotification(roles.Where(u => u.Feedback5 == 1).ToList(), feedback);
            }
            if (feedback.TypeID == 6)
            {
                SendNotification(roles.Where(u => u.Feedback6 == 1).ToList(), feedback);
            }
            Page.Response.Redirect("~/UserUI/FeedbacksView.aspx");
        }

        public void SendNotification(List<Roles> responsibleRole, Feedback feedback)
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
                                            "<tr><td colspan='2' style='padding-bottom: 15px; vertical-align: top'>Поступило новое обращение <a href='http://" + BackendHelper.TagToValue("current_app_address") + "/ManagerUI/FeedbackView.aspx?id=" + feedback.SecureID + "' target='_new'><i>{2}</i></a></td></tr>" +
                                            "<tr><td style='vertical-align: top'>Приоритет:</td><td><b>{0}</b></td></tr>" +
                                            "<tr><td style='vertical-align: top'>Тип:</td><td><b>{1}</b></td></tr>" +
                                            "<tr><td style='vertical-align: top'>Содержание:</td><td><b>{3}</b></td></tr>" +
                                            "<tr><td colspan='2' style='padding-top: 15px; vertical-align: top'>Перейдите по <a href='http://" + BackendHelper.TagToValue("current_app_address") + "/ManagerUI/FeedbackView.aspx?id=" + feedback.SecureID + "' target='_new'>этой</a> ссылке для ответа на обращение.</td></tr></table>",
                                            Feedback.Priorities.FirstOrDefault(u => u.Key == feedback.PriorityID).Value,
                                            Feedback.Types.FirstOrDefault(u => u.Key == feedback.TypeID).Value,
                                            feedback.Title,
                                            feedback.Body);
            EmailMethods.MailSendHTML("Новое обращение", messageBody, emailArray);
        }
    }
}