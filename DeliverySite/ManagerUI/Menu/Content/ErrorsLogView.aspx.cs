using Delivery.BLL.Helpers;
using Delivery.BLL.StaticMethods;
using Delivery.DAL;
using Delivery.DAL.DataBaseObjects;
using DeliverySite.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace Delivery.ManagerUI.Menu.Content
{
    public partial class ErrorsLogView : ManagerBasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = PagesTitles.ManagerErrorsLogView + BackendHelper.TagToValue("page_title_part");
            OtherMethods.ActiveRightMenuStyleChanche("hlErrors", this.Page);
            OtherMethods.ActiveRightMenuStyleChanche("hlContent", this.Page);
            PageAccess();
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            ListViewDataBind();
        }

        public void lbDelete_Click(Object sender, EventArgs e)
        {
            DeleteAccess();
            var lb = (LinkButton)sender;
            var error = new ErrorsLog();
            error.Delete(Convert.ToInt32(lb.CommandArgument));
            Page.Response.Redirect("~/ManagerUI/Menu/Content/ErrorsLogView.aspx");
        }

        public void lbDeleteAll_Click(Object sender, EventArgs e)
        {
            DeleteAccess();
            var dm = new DataManager();
            dm.QueryWithoutReturnData(null, "TRUNCATE TABLE errorslog");
        }

        protected void ListViewDataBind()
        {
            var errors = new ErrorsLog();
            lvAllErrors.DataSource = errors.GetAllItems("Date", "DESC", null);
            lvAllErrors.DataBind();

            #region Редирект на первую страницу при поиске
            if (lvAllErrors.Items.Count == 0 && lvDataPager.TotalRowCount != 0)
            {
                lvDataPager.SetPageProperties(0, lvDataPager.PageSize, false);
                lvAllErrors.DataBind();
            }
            #endregion
        }

        #region Настройки доступа к странице и действиям
        protected void PageAccess()
        {
            var userInSession = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());
            if (currentRole.PageErrorsLogView != 1)
            {
                Response.Redirect("~/Error.aspx?id=1");
            }
        }

        protected void DeleteAccess()
        {
            var userInSession = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());
            if (currentRole.ActionErrorsLogDelete != 1)
            {
                Response.Redirect("~/Error.aspx?id=1");
            }
        }
        #endregion
    }
}