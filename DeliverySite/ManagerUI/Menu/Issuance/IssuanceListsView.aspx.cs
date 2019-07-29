using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Delivery.BLL.Helpers;
using Delivery.BLL.StaticMethods;
using Delivery.DAL;
using Delivery.DAL.DataBaseObjects;
using DeliverySite.Resources;

namespace Delivery.ManagerUI.Menu.Issuance
{
    public partial class IssuanceListsView : ManagerBasePage
    {
        public bool IsDeleteButtonVisible { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            OtherMethods.ActiveRightMenuStyleChanche("hlIssuance", this.Page);
            OtherMethods.ActiveRightMenuStyleChanche("hlIssuanceListsView", this.Page);
            Page.Title = PagesTitles.ManagerIssuanceListsViewTitle + BackendHelper.TagToValue("page_title_part");

            #region Блок доступа к странице
            var userInSession = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());
            if (currentRole.PageIssuanceListsView != 1)
            {
                Response.Redirect("~/Error.aspx?id=1");
            }
            #endregion

            IsDeleteButtonVisible = currentRole.ActionIssuanceListDelete == 1;
        }

        public void lbClose_Click(Object sender, EventArgs e)
        {
            var lb = (LinkButton)sender;
            var notHaveError = IssuanceListsHelper.CloseIssuanceList(Convert.ToInt32(lb.CommandArgument));
            if (!notHaveError) lblError.Text = "Ошибка с правами и условиями! Не все заявки завершены!";
        }

        public void lbDelete_Click(Object sender, EventArgs e)
        {
            DeleteAccess();
            var lb = (LinkButton)sender;
            IssuanceListsHelper.DeleteIssuanceList(Convert.ToInt32(lb.CommandArgument));
        }
        
        public void lbReOpen_Click(Object sender, EventArgs e)
        {
            var lb = (LinkButton)sender;
            var notHaveError = IssuanceListsHelper.ReOpenIssuanceList(Convert.ToInt32(lb.CommandArgument));
            if (!notHaveError) lblError.Text = "Ошибка с правами и условиями! Не все заявки переоткрыты!";
        }

        //этот метод перед самой отрисовкой страницы биндит все данные
        protected void Page_PreRender(object sender, EventArgs e)
        {
            ListViewDataBind();
        }

        #region Настройки доступа к странице и действиям
        protected void DeleteAccess()
        {
            var userInSession = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());
            if (currentRole.ActionIssuanceListDelete != 1)
            {
                Response.Redirect("~/Error.aspx?id=1");
            }
        }
        #endregion

        #region Methods

        protected void ListViewDataBind()
        {
            var dm = new DataManager();
            lvAllCity.DataSource = dm.QueryWithReturnDataSet("SELECT * FROM issuancelists ORDER BY IssuanceDate DESC");
            lvAllCity.DataBind();
            if (lvAllCity.Items.Count == 0)
            {
                lblPage.Visible = false;
            }

            foreach (ListViewDataItem items in lvAllCity.Items)
            {
                var openLink = (LinkButton)items.FindControl("lbOpen");
                var closeLink = (LinkButton)items.FindControl("lbClose");
                var issuanceListsStatusID = (HiddenField)items.FindControl("hfIssuanceListsStatusID");
                if (issuanceListsStatusID.Value == "1" || issuanceListsStatusID.Value == "3")
                {
                    closeLink.Visible = true;
                    openLink.Visible = false;
                }
                else
                {
                    closeLink.Visible = false;
                    openLink.Visible = true;
                }
            }
        }

        #endregion
    }
}