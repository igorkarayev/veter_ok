using Delivery.BLL.Helpers;
using Delivery.BLL.StaticMethods;
using Delivery.DAL;
using Delivery.DAL.DataBaseObjects;
using DeliverySite.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace Delivery.ManagerUI.Menu.Settings
{
    public partial class RolesView : ManagerBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = PagesTitles.ManagerRolesView + BackendHelper.TagToValue("page_title_part");
            OtherMethods.ActiveRightMenuStyleChanche("hlSettings", this.Page);
            OtherMethods.ActiveRightMenuStyleChanche("hlRoles", this.Page);

            var userInSession = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());
            if (currentRole.PageRolesView != 1)
            {
                Response.Redirect("~/Error.aspx?id=1");
            }
        }

        public void lbDelete_Click(Object sender, EventArgs e)
        {
            DeleteAccess();
            var lb = (LinkButton)sender;
            var role = new Roles();
            role.Delete(Convert.ToInt32(lb.CommandArgument));
            Page.Response.Redirect("~/ManagerUI/Menu/Settings/RolesView.aspx");
        }

        protected void lvDataPager_PreRender(object sender, EventArgs e)
        {
            var role = new Roles();
            var ds = role.GetAllItems("Name", "ASC", null);
            lvAllRoles.DataSource = ds;
            lvAllRoles.DataBind();

            #region Редирект на первую страницу при поиске
            if (lvAllRoles.Items.Count == 0 && lvDataPager.TotalRowCount != 0)
            {
                lvDataPager.SetPageProperties(0, lvDataPager.PageSize, false);
                lvAllRoles.DataBind();
            }
            #endregion

            var userInSession = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());

            foreach (var item in lvAllRoles.Items)
            {
                var roleName = (Label)item.FindControl("lblName");
                var deleteLink = (LinkButton)item.FindControl("lbDelete");
                var changeLink = (HyperLink)item.FindControl("lbChange");
                var userInRoleCountLabel = (Label)item.FindControl("lblUserInRoleCount");
                var hfIsBase = (HiddenField)item.FindControl("hfIsBase");

                var dm = new DataManager();

                var usersInRoleCount = Convert.ToInt32(dm.QueryWithReturnDataSet(String.Format("select count(*) from `users` where `role`= '{0}';", roleName.Text)).Tables[0].Rows[0][0].ToString());
                userInRoleCountLabel.Text = usersInRoleCount.ToString();
                // с суперадмином никто ничего не может сделать
                if (roleName.Text == Users.Roles.SuperAdmin.ToString())
                {
                    deleteLink.Visible = false;
                    changeLink.Visible = false;
                }
                else
                {
                    //админа может удалить или изменить только суперадмин
                    if (roleName.Text == Users.Roles.Admin.ToString() && userInSession.Role != Users.Roles.SuperAdmin.ToString())
                    {
                        deleteLink.Visible = false;
                        changeLink.Visible = false;
                    }
                    //если в данной роли есть сотрудники - ее нельзя удалить
                    if (usersInRoleCount > 0 || hfIsBase.Value == "1")
                    {
                        deleteLink.Visible = false;
                    }
                }

                //глобальные настройки ссылки для удаления роли
                if (currentRole.ActionRolesDelete != 1)
                {
                    deleteLink.Visible = false;
                }

            }
        }

        #region Настройки доступа к странице и действиям
        protected void DeleteAccess()
        {
            var userInSession = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());
            if (currentRole.ActionRolesDelete != 1)
            {
                Response.Redirect("~/Error.aspx?id=1");
            }
        }
        #endregion
    }
}