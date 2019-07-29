using Delivery.BLL.Helpers;
using Delivery.BLL.StaticMethods;
using Delivery.DAL.DataBaseObjects;
using DeliverySite.Resources;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Delivery.ManagerUI.Menu.Souls
{
    public partial class SectionEdit : ManagerBasePage
    {
        protected string ButtonText { get; set; }

        public string ActionText { get; set; }

        protected void Page_Init(object sender, EventArgs e)
        {
            ButtonText = Page.Request.Params["id"] != null ? "Сохранить" : "Добавить";
            ActionText = Page.Request.Params["id"] != null ? "Редактирование" : "Добавление";
            btnCreate.Click += bntCreate_Click;
            DataBind();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = Page.Request.Params["id"] != null ? PagesTitles.ManagerCategoryEditTitle + BackendHelper.TagToValue("page_title_part") : PagesTitles.ManagerCategoryCreateTitle + BackendHelper.TagToValue("page_title_part");
            OtherMethods.ActiveRightMenuStyleChanche("hlCategory", this.Page);
            OtherMethods.ActiveRightMenuStyleChanche("hlSouls", this.Page);

            #region Блок доступа к странице
            var userInSession = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());
            if (currentRole.PageCategoryEdit != 1)
            {
                Response.Redirect("~/Error.aspx?id=1");
            }
            #endregion

            if (Page.Request.Params["id"] != null)
            {
                var section = new Category() { ID = Convert.ToInt32(Page.Request.Params["id"]) };
                section.GetById();
                if (!IsPostBack)
                {
                    tbName.Text = section.Name;
                }
            }
        }

        public void bntCreate_Click(Object sender, EventArgs e)
        {

            var userInSession = (Users)Session["userinsession"];
            var id = Page.Request.Params["id"];
            var section = new Category
            {
                Name = tbName.Text.Replace("\"", "''"),
            };
            if (id == null)
            {
                section.Create();
            }
            else
            {
                section.ID = Convert.ToInt32(id);
                section.Update(userInSession.ID, OtherMethods.GetIPAddress(), "CategoryEdit");
            }
            Page.Response.Redirect("~/ManagerUI/Menu/Souls/CategoryView.aspx");
        }
    }
}