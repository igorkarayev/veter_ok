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
    public partial class MoneyView : ManagerBasePage
    {
        protected String AppKey { get; set; }

        public string PageName { get; set; }

        public string UserID { get; set; }

        public string UserIP { get; set; }

        public String AgreedAssessedWithDeliveryCost { get; set; }

        public String AutoChangeDeliveryStatus { get; set; }

        protected void Page_Init(object sender, EventArgs e)
        {
            btnSearch.Click += btnSearch_Click;
            btnReload.Click += btnReload_Click;
            DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            #region Предварительные настройки страницы
            Page.Title = PagesTitles.ManagerMoneyViewTitle + BackendHelper.TagToValue("page_title_part");
            OtherMethods.ActiveRightMenuStyleChanche("hlMoney", this.Page);
            OtherMethods.ActiveRightMenuStyleChanche("hlMoneyPriemka", this.Page);
            if (Session["flash:now"] != null && Session["flash:now"].ToString() != String.Empty)
            {
                lblStatus.Text = Session["flash:now"].ToString();
                Session["flash:now"] = String.Empty;
            }

            AutoChangeDeliveryStatus = BackendHelper.TagToValue("auto_change_processed_status");
            #endregion

            #region Блок доступа к странице
            var userInSession = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());
            if (currentRole.PageMoneyView != 1)
            {
                Response.Redirect("~/Error.aspx?id=1");
            }
            #endregion

            #region Данные для API логирования
            PageName = "MoneyView";
            UserIP = OtherMethods.GetIPAddress();
            UserID = userInSession.ID.ToString();
            AppKey = Globals.Settings.AppServiceSecureKey;
            #endregion

            #region Формирование поиска по водителю
            var dm = new DataManager();
            var deliveryDate = String.Empty;

            var date1 = stbDeliveryDate1.Text.Split('/');
            var date2 = stbDeliveryDate2.Text.Split('/');

            if (!string.IsNullOrEmpty(stbDeliveryDate1.Text) && !string.IsNullOrEmpty(stbDeliveryDate2.Text))
            {
                deliveryDate = "AND (T.`DeliveryDate` BETWEEN '" +
                                   Convert.ToDateTime(stbDeliveryDate1.Text).ToString("yyyy-MM-dd") + "' AND '" +
                                   Convert.ToDateTime(stbDeliveryDate2.Text).ToString("yyyy-MM-dd") + "')";
            }
            else
            {
                if (!string.IsNullOrEmpty(stbDeliveryDate1.Text))
                {
                    deliveryDate = "AND T.`DeliveryDate` = '" + Convert.ToDateTime(stbDeliveryDate1.Text).ToString("yyyy-MM-dd") + "'";
                }
            }

            //выбираем только тех водителей, которые ехали в этот промежуток времени
            var ds = dm.QueryWithReturnDataSet(String.Format("SELECT DISTINCT D.FirstName, D.LastName, D.ThirdName, D.ID FROM drivers D JOIN tickets T ON T.DriverID = D.ID WHERE (T.StatusID = 3 OR T.StatusID = 5 OR T.StatusID = 7 OR T.StatusID = 12) {0} ORDER BY D.FirstName ASC", deliveryDate));
            ds.Tables[0].Columns.Add("FIOAndID", typeof(string), "'(' + ID + ') ' + FirstName + ' ' + SUBSTRING(LastName,1,1) + '.' +  SUBSTRING(ThirdName,1,1) + '.'");
            sddlDrivers.DataSource = ds;
            sddlDrivers.DataTextField = "FIOAndID";
            sddlDrivers.DataValueField = "ID";
            sddlDrivers.DataBind();

            //если нет результатов - загружаем все водителей с заявками в пути, обработано, отказ
            if (sddlDrivers.Items.Count == 0)
            {
                var newDs = dm.QueryWithReturnDataSet("SELECT DISTINCT D.FirstName, D.LastName, D.ThirdName, D.ID FROM drivers D JOIN tickets T ON T.DriverID = D.ID WHERE (T.StatusID = 3 OR T.StatusID = 5 OR T.StatusID = 7 OR T.StatusID = 12) ORDER BY D.FirstName ASC");
                newDs.Tables[0].Columns.Add("FIOAndID", typeof(string), "'(' + ID + ') ' + FirstName + ' ' + SUBSTRING(LastName,1,1) + '.' +  SUBSTRING(ThirdName,1,1) + '.'");
                sddlDrivers.DataSource = newDs;
                sddlDrivers.DataTextField = "FIOAndID";
                sddlDrivers.DataValueField = "ID";
                sddlDrivers.DataBind();
            }

            //выделяем выбраного водителя
            var driverIdExist = false;
            foreach (var item in sddlDrivers.Items.Cast<ListItem>().Where(item => item.Value == Page.Request["ctl00$MainContent$sddlDrivers"] && driverIdExist == false))
            {
                driverIdExist = true;
            }
            if (driverIdExist)
            {
                sddlDrivers.SelectedValue = Page.Request["ctl00$MainContent$sddlDrivers"];
            }
            #endregion

            #region Сохранение состояния страницы при редиректах с других страниц
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
                ListViewData();
            }
            #endregion

            #region Условия для отображения DataPager
            if ((lvAllTickets.Items.Count / lvDataPager.PageSize) < 1)
            {
                lblPage.Visible = false;
                lvDataPager.Visible = false;
            }
            #endregion
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ListViewData();
        }

        private void ListViewData()
        {
            var dm = new DataManager();
            var ds = dm.QueryWithReturnDataSet(String.Format("SELECT * {0} ORDER BY ID DESC", GetSearchString()));
            lvAllTickets.DataSource = ds;
            lblAllResult.Text = ds.Tables[0].Rows.Count.ToString();
            lvAllTickets.DataBind();

            #region Ограничения на заполнение привезенных денег
            foreach (ListViewDataItem items in lvAllTickets.Items)
            {
                var id = (HiddenField)items.FindControl("hfID");
                var receivedBLR = (TextBox)items.FindControl("tbReceivedBLR");
                var receivedUSD = (TextBox)items.FindControl("tbReceivedUSD");
                var receivedEUR = (TextBox)items.FindControl("tbReceivedEUR");
                var receivedRUR = (TextBox)items.FindControl("tbReceivedRUR");
                var ticket = new DAL.DataBaseObjects.Tickets
                {
                    ID = Convert.ToInt32(id.Value)
                };
                ticket.GetById();
                if (ticket.CourseEUR == 0 || ticket.CourseEUR == 1)
                {
                    receivedEUR.Enabled = false;
                }
                if (ticket.CourseRUR == 0 || ticket.CourseRUR == 1)
                {
                    receivedRUR.Enabled = false;
                }
                if (ticket.CourseUSD == 0 || ticket.CourseUSD == 1)
                {
                    receivedUSD.Enabled = false;
                }

                if (ticket.StatusID == 1 || ticket.StatusID == 2 || ticket.StatusID == 4 || ticket.StatusID == 7 ||
                    ticket.StatusID == 8 || ticket.StatusID == 9 || ticket.StatusID == 10)
                {
                    receivedBLR.Enabled = false;
                    receivedRUR.Enabled = false;
                    receivedUSD.Enabled = false;
                    receivedEUR.Enabled = false;
                }
            }
            #endregion
        }

        protected void btnReload_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.ServerVariables["URL"]);
        }

        #region Methods
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


            var date1 = stbDeliveryDate1.Text.Split('/');
            var date2 = stbDeliveryDate2.Text.Split('/');

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

            //формируем конечный запрос для поиска
            searchParametres.Add("DriverID", searchDriverIdString);
            searchParametres.Add("DeliveryDate", searchDateString);
            var searchStringPart = searchParametres.Where(searchParametre => !string.IsNullOrEmpty(searchParametre.Value)).Aggregate(searchString, (current, searchParametre) => current + searchParametre.Value + " AND ");
            searchString = searchStringPart.Length < 4 ? "FROM tickets WHERE (StatusID = 3 OR StatusID = 5 OR StatusID = 7 OR StatusID = 12 OR StatusID = 17 OR StatusID = 18)" : String.Format("FROM tickets WHERE (StatusID = 3 OR StatusID = 5 OR StatusID = 7 OR StatusID = 12 OR StatusID = 17 OR StatusID = 18) AND {0}", searchStringPart.Remove(searchStringPart.Length - 4));

            var dm = new DataManager();
            var allAssessedWithDeliveryCostString = dm.QueryWithReturnDataSet("SELECT SUM(`AssessedCost` + `DeliveryCost`) " + searchString + " AND `AgreedCost` = 0").Tables[0].Rows[0][0].ToString();
            var allAgreedWithDeliveryCostString = dm.QueryWithReturnDataSet("SELECT SUM(`AgreedCost`) " + searchString + " AND `AgreedCost` > 0 ").Tables[0].Rows[0][0].ToString();
            allAssessedWithDeliveryCostString = allAssessedWithDeliveryCostString == String.Empty ? "0" : allAssessedWithDeliveryCostString;
            allAgreedWithDeliveryCostString = allAgreedWithDeliveryCostString == String.Empty ? "0" : allAgreedWithDeliveryCostString;
            var allAssessedWithDeliveryCost = Convert.ToDecimal(allAssessedWithDeliveryCostString);
            var allAgreedWithDeliveryCost = Convert.ToDecimal(allAgreedWithDeliveryCostString);
            AgreedAssessedWithDeliveryCost = MoneyMethods.MoneySeparator(allAssessedWithDeliveryCost + allAgreedWithDeliveryCost);

            return searchString;
        }
        #endregion
    }
}