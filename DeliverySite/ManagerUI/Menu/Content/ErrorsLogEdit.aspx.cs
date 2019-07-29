using System;
using System.Collections.Generic;
using System.Linq;
using Delivery.BLL.Helpers;
using Delivery.BLL.StaticMethods;
using Delivery.DAL.DataBaseObjects;
using DeliverySite.Resources;

namespace Delivery.ManagerUI.Menu.Content
{
    public partial class ErrorsLogEdit : ManagerBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            OtherMethods.ActiveRightMenuStyleChanche("hlErrors", this.Page);
            OtherMethods.ActiveRightMenuStyleChanche("hlContent", this.Page);
            Page.Title = PagesTitles.ManagerErrorsLogEdit + BackendHelper.TagToValue("page_title_part");

            #region Блок доступа к странице
            var userInSession = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());
            if (currentRole.PageErrorsLogEdit != 1)
            {
                Response.Redirect("~/Error.aspx?id=1");
            }
            #endregion

            var error = new ErrorsLog() { ID = Convert.ToInt32(Page.Request.Params["id"]) };
            error.GetById();
            lblDate.Text = error.Date.ToString();
            lblIP.Text = error.IP;
            lblType.Text = error.ErrorType;
            tbStackTrase.Text = error.StackTrase;
        }
    }
}