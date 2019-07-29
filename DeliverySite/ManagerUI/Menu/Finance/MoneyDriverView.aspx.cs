using Delivery.BLL.Helpers;
using Delivery.BLL.StaticMethods;
using Delivery.DAL;
using Delivery.DAL.DataBaseObjects;
using DeliverySite.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace Delivery.ManagerUI.Menu.Finance
{
    public partial class MoneyDriverView : ManagerBasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            btnCalc.Click += btnCalc_Click;
            btnReload.Click += btnReload_Click;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = PagesTitles.ManagerMoneyDriverViewTitle + BackendHelper.TagToValue("page_title_part");
            OtherMethods.ActiveRightMenuStyleChanche("hlMoney", this.Page);
            OtherMethods.ActiveRightMenuStyleChanche("hlDriverMoney", this.Page);

            #region Блок доступа к странице
            var userInSession = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());
            if (currentRole.PageMoneyDriverView != 1)
            {
                Response.Redirect("~/Error.aspx?id=1");
            }
            #endregion

            //формируем форму поиска по водителю СТАРТ
            var dm = new DataManager();

            var dataSet3 = dm.QueryWithReturnDataSet("SELECT DISTINCT D.FirstName, D.LastName, D.ThirdName, D.ID FROM drivers D JOIN tickets T ON T.DriverID = D.ID WHERE (T.StatusID = 5 or T.StatusID = 6) ORDER BY D.FirstName ASC");
            dataSet3.Tables[0].Columns.Add("FIOAndID", typeof(string), "'(' + ID + ') ' + FirstName + ' ' + SUBSTRING(LastName,1,1) + '.' +  SUBSTRING(ThirdName,1,1) + '.'");
            sddlDrivers.DataSource = dataSet3;
            sddlDrivers.DataTextField = "FIOAndID";
            sddlDrivers.DataValueField = "ID";
            sddlDrivers.DataBind();

            var driverIdExist = false;
            foreach (ListItem item in sddlDrivers.Items)
            {
                if (item.Value == Page.Request["ctl00$MainContent$sddlDrivers"] && driverIdExist == false)
                {
                    driverIdExist = true;
                }
            }
            if (driverIdExist)
            {
                sddlDrivers.SelectedValue = Page.Request["ctl00$MainContent$sddlDrivers"];
            }
            //формируем форму поиска по водителю КОНЕЦ


            if (!IsPostBack)
            {

                if (!string.IsNullOrEmpty(Page.Request.Params["stateSave"]))
                {
                    if (!string.IsNullOrEmpty(Page.Request.Params["driverID"]))
                    {
                        sddlDrivers.SelectedValue = Page.Request.Params["driverID"];
                    }

                    if (!string.IsNullOrEmpty(Page.Request.Params["deliveryDate1"]))
                    {
                        stbDeliveryDate1.Text = Page.Request.Params["deliveryDate1"];
                    }

                    if (!string.IsNullOrEmpty(Page.Request.Params["deliveryDate2"]))
                    {
                        stbDeliveryDate2.Text = Page.Request.Params["deliveryDate2"];
                    }
                }
                pnlResultPanel.Visible = false;
            }

        }


        protected void btnCalc_Click(object sender, EventArgs e)
        {
            GetMoney(GetSearchString());
            pnlResultPanel.Visible = true;
        }

        protected void btnReload_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.ServerVariables["URL"]);
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
            var allInBLRString = dm.QueryWithReturnDataSet("SELECT SUM((`ReceivedUSD`*`CourseUSD`)+(`ReceivedRUR`*`CourseRUR`)+(`ReceivedEUR`*`CourseEUR`)+`ReceivedBLR`) " + searchString).Tables[0].Rows[0][0].ToString();
            var allAssessedWithDeliveryCostString = dm.QueryWithReturnDataSet("SELECT SUM(`AssessedCost` + `DeliveryCost`) " + searchString + " AND `AgreedCost` = 0").Tables[0].Rows[0][0].ToString();
            var allAgreedWithDeliveryCostString = dm.QueryWithReturnDataSet("SELECT SUM(`AgreedCost`) " + searchString + " AND `AgreedCost` > 0").Tables[0].Rows[0][0].ToString();

            allEURString = allEURString == String.Empty ? "0" : allEURString;
            allRURString = allRURString == String.Empty ? "0" : allRURString;
            allUSDString = allUSDString == String.Empty ? "0" : allUSDString;
            allBLRString = allBLRString == String.Empty ? "0" : allBLRString;
            allInBLRString = allInBLRString == String.Empty ? "0" : allInBLRString;
            allAssessedWithDeliveryCostString = allAssessedWithDeliveryCostString == String.Empty ? "0" : allAssessedWithDeliveryCostString;
            allAgreedWithDeliveryCostString = allAgreedWithDeliveryCostString == String.Empty ? "0" : allAgreedWithDeliveryCostString;


            var allAssessedWithDeliveryCost = Convert.ToDecimal(allAssessedWithDeliveryCostString);
            var allAgreedWithDeliveryCost = Convert.ToDecimal(allAgreedWithDeliveryCostString);

            var allAgreedAssessedWithDeliveryCost = allAssessedWithDeliveryCost + allAgreedWithDeliveryCost;

            var allBLR = Convert.ToDecimal(allInBLRString);

            var difference = allBLR - allAgreedAssessedWithDeliveryCost;

            var differenceString = String.Empty;

            if (difference > 0)
            {
                differenceString = "<span style =\"color: green;\">" + MoneyMethods.MoneySeparator(difference.ToString()) + "</span>";
            }
            else
            {
                differenceString = "<span style =\"color: red;\">" + MoneyMethods.MoneySeparator(difference.ToString().Replace("-", "")) + "</span>";
            }

            lblReceivedEUROver.Text = MoneyMethods.MoneySeparator(allEURString);
            lblReceivedRUROver.Text = MoneyMethods.MoneySeparator(allRURString);
            lblReceivedUSDOver.Text = MoneyMethods.MoneySeparator(allUSDString);
            lblReceivedBLROver.Text = MoneyMethods.MoneySeparator(allBLRString);

            lblAgreedAssessedCost.Text = MoneyMethods.MoneySeparator(allAgreedAssessedWithDeliveryCost.ToString());
            lblAllReceivedInBLR.Text = MoneyMethods.MoneySeparator(allBLR.ToString());
            lblDifference.Text = differenceString;

        }


        public String GetSearchString()
        {
            var searchString = String.Empty;
            var searchDriverIdString = String.Empty;
            var searchDateString = String.Empty;
            var searchParametres = new Dictionary<String, String>();

            //формируем строку поика для водителей
            if (sddlDrivers.SelectedValue != "0" && !String.IsNullOrEmpty(sddlDrivers.SelectedValue))
            {
                searchDriverIdString = searchDriverIdString + "`DriverID` = '" + sddlDrivers.SelectedValue + "'";
            }


            //формируем cтроку для поиска по Date
            if (!string.IsNullOrEmpty(stbDeliveryDate1.Text) && !string.IsNullOrEmpty(stbDeliveryDate2.Text))
            {
                searchDateString = "(`DeliveryDate` BETWEEN '" +
                                   Convert.ToDateTime(stbDeliveryDate1.Text).ToString("yyyy-MM-dd") + "' AND '" +
                                   Convert.ToDateTime(stbDeliveryDate2.Text).ToString("yyyy-MM-dd") + "')";
            }
            else
            {
                if (!string.IsNullOrEmpty(stbDeliveryDate1.Text))
                {
                    searchDateString = "`DeliveryDate` = '" + Convert.ToDateTime(stbDeliveryDate1.Text).ToString("yyyy-MM-dd") + "'";
                }
            }

            //формируем конечный запро для поиска
            searchParametres.Add("DriverID", searchDriverIdString);
            searchParametres.Add("DeliveryDate", searchDateString);

            foreach (var searchParametre in searchParametres)
            {
                if (!string.IsNullOrEmpty(searchParametre.Value))
                {
                    searchString = searchString + searchParametre.Value + " AND ";
                }
            }
            //считаем заявки только в статусе завершено и обработано
            var searchStringOver = searchString.Length < 4 ? "FROM tickets WHERE  (StatusID = 5 or StatusID = 6)" : String.Format("FROM tickets WHERE  (StatusID = 5 or StatusID = 6)  AND {0}", searchString.Remove(searchString.Length - 4));

            return searchStringOver;
        }
        #endregion
    }
}