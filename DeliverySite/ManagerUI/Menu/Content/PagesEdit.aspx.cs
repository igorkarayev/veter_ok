using Delivery.BLL.Helpers;
using Delivery.BLL.StaticMethods;
using Delivery.DAL.DataBaseObjects;
using DeliverySite.Resources;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Delivery.ManagerUI.Menu.Content
{
    public partial class PagesEdit : ManagerBasePage
    {
        protected string ButtonText { get; set; }
        protected string PageName { get; set; }
        protected void Page_Init(object sender, EventArgs e)
        {
            ButtonText = Page.Request.Params["id"] != null ? "Изменить" : "Добавить";
            btnCreate.Click += bntCreate_Click;
            DataBind();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = PagesTitles.ManagerPagesEdit + BackendHelper.TagToValue("page_title_part");
            OtherMethods.ActiveRightMenuStyleChanche("hlPages", this.Page);
            OtherMethods.ActiveRightMenuStyleChanche("hlContent", this.Page);

            #region Блок доступа к странице
            var userInSession = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());
            if (currentRole.PagePagesEdit != 1)
            {
                Response.Redirect("~/Error.aspx?id=1");
            }
            #endregion

            if (Page.Request.Params["id"] != null)
            {
                var page = new Pages { ID = Convert.ToInt32(Page.Request.Params["id"]) };
                page.GetById();
                if (!IsPostBack)
                {
                    tbPageTitle.Text = page.PageTitle;
                    tbContent.Text = page.Content;
                    tbPageName.Text = page.PageName;
                }
            }
        }
        public void bntCreate_Click(Object sender, EventArgs e)
        {
            var id = Page.Request.Params["id"];
            var page = new Pages
            {
                PageTitle = tbPageTitle.Text,
                Content = tbContent.Text,
                PageName = tbPageName.Text
            };
            if (id != null)
            {

                page.ID = Convert.ToInt32(id);
                page.Update();
            }
            else
            {
                page.Create();
            }
            Page.Response.Redirect("~/ManagerUI/Menu/Content/PagesView.aspx");
        }
    }
}