using Delivery.BLL.Helpers;
using Delivery.BLL.StaticMethods;
using Delivery.DAL.DataBaseObjects;
using DeliverySite.Resources;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Delivery.ManagerUI.Menu.Content
{
    public partial class NotificationsEdit : ManagerBasePage
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
            Page.Title = PagesTitles.ManagerNotificationEdit + BackendHelper.TagToValue("page_title_part");
            OtherMethods.ActiveRightMenuStyleChanche("hlNotifications", this.Page);
            OtherMethods.ActiveRightMenuStyleChanche("hlContent", this.Page);

            #region Блок доступа к странице
            var userInSession = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());
            if (currentRole.PageNotificationsEdit != 1)
            {
                Response.Redirect("~/Error.aspx?id=1");
            }
            #endregion

            if (Page.Request.Params["id"] != null)
            {
                var note = new Notification { ID = Convert.ToInt32(Page.Request.Params["id"]) };
                note.GetById();
                if (!IsPostBack)
                {
                    if (string.IsNullOrEmpty(note.Title))
                    {
                        tbTitle.Visible = false;
                        lblTitle.Visible = false;
                    }
                    tbTitle.Text = note.Title;
                    tbBody.Text = note.Description;
                    lblDescriptionMore.Text = note.DescriptionStatic;
                }
            }
        }
        public void bntCreate_Click(Object sender, EventArgs e)
        {
            var id = Page.Request.Params["id"];
            var note = new Notification { Title = tbTitle.Text, Description = tbBody.Text };
            if (id != null)
            {
                note.ID = Convert.ToInt32(id);
                note.Update();
            }
            Page.Response.Redirect("~/ManagerUI/Menu/Content/NotificationsView.aspx");
        }
    }
}