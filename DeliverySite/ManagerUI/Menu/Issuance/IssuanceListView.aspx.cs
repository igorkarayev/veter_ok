using Delivery.BLL.Helpers;
using Delivery.BLL.StaticMethods;
using Delivery.DAL;
using Delivery.DAL.DataBaseObjects;
using DeliverySite.Resources;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.UI.WebControls;

namespace Delivery.ManagerUI.Menu.Issuance
{
    public partial class IssuanceListView : ManagerBasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            btnAction.Click += btnAction_Click;
            btnDelete.Click += btnDelete_Click;
            btnReopen.Click += btnReopen_Click;
            btnClose.Click += btnClose_Click;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = PagesTitles.ManagerIssuanceListViewTitle.Replace("{0}", Page.Request.Params["id"]) + BackendHelper.TagToValue("page_title_part");
            OtherMethods.ActiveRightMenuStyleChanche("hlIssuance", this.Page);
            OtherMethods.ActiveRightMenuStyleChanche("hlIssuanceListsView", this.Page);

            #region Блок доступа к странице
            var userInSession = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());
            if (currentRole.PageIssuanceListView != 1)
            {
                Response.Redirect("~/Error.aspx?id=1");
            }
            #endregion

            if (currentRole.ActionIssuanceListDelete != 1)
            {
                btnDelete.Visible = false;
            }

            if (!String.IsNullOrEmpty(Page.Request.Params["id"]))
            {
                var issuanceList = new IssuanceLists() { ID = Convert.ToInt32(Page.Request.Params["id"]) };
                issuanceList.GetById();
                if (issuanceList.IssuanceListsStatusID == 3 || issuanceList.IssuanceListsStatusID == 1)
                {
                    btnReopen.Visible = false;
                    btnClose.Visible = true;
                }
                else
                {
                    btnReopen.Visible = true;
                    btnClose.Visible = false;
                }
                var user = UsersHelper.UserIDToFullName(issuanceList.UserID.ToString());
                lblListInfo.Text = String.Format("# {0}, {1}, рассчет: {2}", issuanceList.ID, user,
                    OtherMethods.DateConvert(issuanceList.IssuanceDate.ToString()));
            }
            else
            {
                pnlSearschResult.Visible = pnlResultPanel.Visible = btnAction.Visible = false;
                lblPage.Visible = false;
            }

        }

        protected void btnAction_Click(object sender, EventArgs e)
        {
            foreach (var items in lvAllTickets.Items)
            {
                var chkBoxRows = (CheckBox)items.FindControl("cbSelect");

                if (!chkBoxRows.Checked)
                {
                    var id = (HiddenField)items.FindControl("hfID");
                    var ticket = new DAL.DataBaseObjects.Tickets { ID = Convert.ToInt32(id.Value) };
                    ticket.GetById();
                    ticket.IssuanceListID = 0;
                    ticket.Update();

                }
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteAccess();
            var id = Convert.ToInt32(Page.Request.Params["id"]);
            IssuanceListsHelper.DeleteIssuanceList(id);
            Response.Redirect("~/ManagerUI/Menu/Issuance/IssuanceListsView.aspx");
        }

        protected void btnReopen_Click(object sender, EventArgs e)
        {
            var id = Convert.ToInt32(Page.Request.Params["id"]);
            IssuanceListsHelper.ReOpenIssuanceList(id);
            Response.Redirect(Request.Url.ToString());
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            var id = Convert.ToInt32(Page.Request.Params["id"]);
            IssuanceListsHelper.CloseIssuanceList(id);
            Response.Redirect(Request.Url.ToString());
        }

        //этот метод перед самой отрисовкой страницы биндит все данные
        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Page.Request.Params["id"]))
            {
                ListViewData();
            }
        }

        private void ListViewData()
        {
            var dm = new DataManager();
            var query = String.Format("FROM tickets WHERE IssuanceListID = {0}", Page.Request.Params["id"]);
            var ds = dm.QueryWithReturnDataSet("SELECT *" + query);
            lvAllTickets.DataSource = ds;
            lvAllTickets.DataBind();
            lblAllResult.Text = ds.Tables[0].Rows.Count.ToString();
            if (ds.Tables[0].Rows.Count == 0)
            {
                pnlSearschResult.Visible = pnlResultPanel.Visible = btnAction.Visible = false;
                lblPage.Visible = false;
            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                GetMoney(query);
            }
        }

        #region Настройки доступа к странице и действиям
        protected void DeleteAccess()
        {
            var userInSession = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());
            if (currentRole.PageIssuanceListView != 1)
            {
                Response.Redirect("~/Error.aspx?id=1");
            }
        }
        #endregion

        #region Money

        public void GetMoney(string query)
        {
            var searchString = query;
            var dm = new DataManager();
            var eur = Convert.ToString(dm.QueryWithReturnDataSet("SELECT SUM(`ReceivedEUR`) " + searchString).Tables[0].Rows[0][0], CultureInfo.CurrentCulture);
            var usd = Convert.ToString(dm.QueryWithReturnDataSet("SELECT SUM(`ReceivedUSD`) " + searchString).Tables[0].Rows[0][0], CultureInfo.CurrentCulture);
            var rur = Convert.ToString(dm.QueryWithReturnDataSet("SELECT SUM(`ReceivedRUR`) " + searchString).Tables[0].Rows[0][0], CultureInfo.CurrentCulture);

            var allInBLR = Convert.ToString(dm.QueryWithReturnDataSet("SELECT SUM((`ReceivedUSD`*`CourseUSD`)+(`ReceivedRUR`*`CourseRUR`)+(`ReceivedEUR`*`CourseEUR`)) " + searchString).Tables[0].Rows[0][0], CultureInfo.CurrentCulture);
            var allGruzobozCost = Convert.ToString(dm.QueryWithReturnDataSet("SELECT SUM(`GruzobozCost`) " + searchString).Tables[0].Rows[0][0], CultureInfo.CurrentCulture);
            var allAssessedCost = Convert.ToString(dm.QueryWithReturnDataSet("SELECT SUM(`AssessedCost` + `DeliveryCost`) " + searchString + " AND `AgreedCost` = 0").Tables[0].Rows[0][0], CultureInfo.CurrentCulture);

            //если нет согласованных стоимостей - записываем ее равной 0
            var allAgreedCostString =
                Convert.ToString(dm.QueryWithReturnDataSet("SELECT SUM(`AgreedCost` + `DeliveryCost`) " + searchString +
                                         " AND `AgreedCost` > 0").Tables[0].Rows[0][0], CultureInfo.CurrentCulture);
            var allAgreedCost = !String.IsNullOrEmpty(allAgreedCostString) ? allAgreedCostString : "0";

            double allAssessedCostValue = 0.0;
            Double.TryParse(allAssessedCost, out allAssessedCostValue);

            double allAgreedCostValue = 0.0;
            Double.TryParse(allAgreedCost, out allAgreedCostValue);


            var allAgreedAssessedCostValue = allAssessedCostValue + allAgreedCostValue;
            //к выдаче
            
            double allGruzobozCostValue = 0.0;
            Double.TryParse(allGruzobozCost, out allGruzobozCostValue);

            double allInBLRValue = 0.0;
            Double.TryParse(allInBLR, out allInBLRValue);

            var overToIssuance = allAgreedAssessedCostValue - allGruzobozCostValue - allInBLRValue;
            var overToIssuanceString = String.Empty;
            if (overToIssuance > 0)
            {
                overToIssuanceString = "<b>" + MoneyMethods.MoneySeparator(overToIssuance.ToString()) + "</b> BLR;";
            }
            else
            {
                overToIssuanceString = "<b>0</b> BLR (+ забрать у клиента <span style =\"color: red; font-weight: bold;\">" + MoneyMethods.MoneySeparator(overToIssuance.ToString().Replace("-", "")) + "</span> BLR);";
            }

            lblReceivedBLRUser.Text = overToIssuanceString;


            lblReceivedEUROver.Text = lblReceivedEURUser.Text = MoneyMethods.MoneySeparator(eur);
            lblReceivedRUROver.Text = lblReceivedRURUser.Text = MoneyMethods.MoneySeparator(rur);
            lblReceivedUSDOver.Text = lblReceivedUSDUser.Text = MoneyMethods.MoneySeparator(usd);

            lblReceivedBLROverWithCourse.Text = MoneyMethods.MoneySeparator(allInBLR);
            lblOverGruzobozCost.Text = MoneyMethods.MoneySeparator(allGruzobozCost);
        }

        #endregion
    }
}