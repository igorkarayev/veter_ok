using Delivery.BLL.Helpers;
using Delivery.BLL.StaticMethods;
using Delivery.DAL;
using Delivery.DAL.DataBaseObjects;
using DeliverySite.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using Delivery.Resources;

namespace Delivery.ManagerUI.Menu.Finance
{
    public partial class MoneyDeliveredView : ManagerBasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = PagesTitles.ManagerMoneyDeliveredView.Replace("СТАТУС", TicketStatusesResources.Processed) + BackendHelper.TagToValue("page_title_part");
            OtherMethods.ActiveRightMenuStyleChanche("hlMoney", this.Page);
            OtherMethods.ActiveRightMenuStyleChanche("hlAllDeliveryMoney", this.Page);

            #region Блок доступа к странице
            var userInSession = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());
            if (currentRole.PageMoneyDeliveredView != 1)
            {
                Response.Redirect("~/Error.aspx?id=1");
            }
            #endregion

            GetMoney(GetSearchString());
        }


        #region Methods
        private void GetMoney(string query)
        {
            var searchString = query;
            var dm = new DataManager();
            var allEURString = dm.QueryWithReturnDataSet("SELECT SUM(`ReceivedEUR`) " + searchString).Tables[0].Rows[0][0].ToString();
            var allRURString = dm.QueryWithReturnDataSet("SELECT SUM(`ReceivedRUR`) " + searchString).Tables[0].Rows[0][0].ToString();
            var allUSDString = dm.QueryWithReturnDataSet("SELECT SUM(`ReceivedUSD`) " + searchString).Tables[0].Rows[0][0].ToString();
            var allBLRString = dm.QueryWithReturnDataSet("SELECT SUM(`ReceivedBLR`) " + searchString).Tables[0].Rows[0][0].ToString();

            if (allEURString == String.Empty)
            {
                allEURString = "0";
            }
            if (allRURString == String.Empty)
            {
                allRURString = "0";
            }
            if (allUSDString == String.Empty)
            {
                allUSDString = "0";
            }
            if (allBLRString == String.Empty)
            {
                allBLRString = "0";
            }

            var allEUR = Convert.ToDecimal(allEURString);
            var allRUR = Convert.ToDecimal(allRURString);
            var allUSD = Convert.ToDecimal(allUSDString);
            var allBLR = Convert.ToDecimal(allBLRString);

            lblReceivedEUROver.Text = MoneyMethods.MoneySeparator(allEUR.ToString());
            lblReceivedRUROver.Text = MoneyMethods.MoneySeparator(allRUR.ToString());
            lblReceivedUSDOver.Text = MoneyMethods.MoneySeparator(allUSD.ToString());
            lblReceivedBLROver.Text = MoneyMethods.MoneySeparator(allBLR.ToString());
        }


        public String GetSearchString()
        {
            return "FROM tickets WHERE  (StatusID = 5)";
        }
        #endregion
    }
}