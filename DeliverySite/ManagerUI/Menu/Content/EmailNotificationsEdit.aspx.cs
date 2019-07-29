using Delivery.BLL.Helpers;
using Delivery.BLL.StaticMethods;
using Delivery.DAL.DataBaseObjects;
using DeliverySite.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using DeliverySite.Resources;

namespace Delivery.ManagerUI.Menu.Content
{
    public partial class EmailNotificationsEdit : ManagerBasePage
    {
        protected string ButtonText { get; set; }
        protected void Page_Init(object sender, EventArgs e)
        {
            ButtonText = "Изменить";
            btnCreate.Click += bntCreate_Click;
            DataBind();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = PagesTitles.ManagerEmailNotificationEdit + BackendHelper.TagToValue("page_title_part");
            OtherMethods.ActiveRightMenuStyleChanche("hlEmailNotifications", this.Page);
            OtherMethods.ActiveRightMenuStyleChanche("hlContent", this.Page);

            #region Блок доступа к странице
            var userInSession = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());
            if (currentRole.PageEmailNotificationsEdit != 1)
            {
                Response.Redirect("~/Error.aspx?id=1");
            }
            #endregion

            if (Page.Request.Params["id"] != null)
            {
                var mail = new EmailNotifications { ID = Convert.ToInt32(Page.Request.Params["id"]) };
                mail.GetById();
                if (!IsPostBack)
                {
                    if (string.IsNullOrEmpty(mail.Title))
                    {
                        tbTitle.Visible = false;
                        lblTitle.Visible = false;
                    }
                    tbTitle.Text = mail.Title;
                    tbBody.Text = mail.Body;
                    lblDescriptionMore.Text = mail.Description;
                }
            }
        }
        public void bntCreate_Click(Object sender, EventArgs e)
        {
            var id = Page.Request.Params["id"];
            var mail = new EmailNotifications { Title = tbTitle.Text, Body = tbBody.Text };
            if (id != null)
            {
                mail.ID = Convert.ToInt32(id);
                mail.Update();
            }
            Page.Response.Redirect("~/ManagerUI/Menu/Content/EmailNotificationsView.aspx");
        }
    }
}