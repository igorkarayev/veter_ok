using Delivery.BLL.Filters;
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
    public partial class NewIssuanceView : ManagerBasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            btnReload.Click += btnReload_Click;
            bCloseList.Click += bCloseList_Click;
            bPrintList.Click += bPrintList_Click;
        }

        private string avaibleStatusesQuery = "T.StatusID IN (5, 8, 9, 15, 16)";

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = PagesTitles.ManagerNewIssuanceViewTitle + BackendHelper.TagToValue("page_title_part");
            OtherMethods.ActiveRightMenuStyleChanche("hlIssuance", this.Page);
            OtherMethods.ActiveRightMenuStyleChanche("hlNewIssuanceView", this.Page);
            if (Session["flash:now"] != null && Session["flash:now"].ToString() != String.Empty)
            {
                lblStatus.Text = Session["flash:now"].ToString();
                Session["flash:now"] = String.Empty;
            }

            #region Блок доступа к странице
            var userInSession = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());
            if (currentRole.PageNewIssuanceView != 1)
            {
                Response.Redirect("~/Error.aspx?id=1");
            }
            #endregion

            var dm = new DataManager();
            
            var dataSet4 = dm.QueryWithReturnDataSet(String.Format("SELECT DISTINCT T.UserID FROM tickets T WHERE " + avaibleStatusesQuery + " ORDER BY T.UserID ASC"));
            sddlUID.DataSource = dataSet4;
            sddlUID.DataTextField = "UserID";
            sddlUID.DataValueField = "UserID";
            sddlUID.DataBind();
            sddlUID.Items.Insert(0, new ListItem("Все", string.Empty));

            var userIdExist = false;
            foreach (ListItem item in sddlUID.Items)
            {
                if (item.Value == Page.Request["ctl00$MainContent$sddlUID"] && userIdExist == false)
                {
                    userIdExist = true;
                }
            }
            if (userIdExist)
            {
                sddlUID.SelectedValue = Page.Request["ctl00$MainContent$sddlUID"];
            }
            //формируем форму поиска по водителю КОНЕЦ

            if (!IsPostBack)
            {
                sddlProfileType.DataSource = DAL.DataBaseObjects.Tickets.ProfileType;
                sddlProfileType.DataTextField = "Value";
                sddlProfileType.DataValueField = "Key";
                sddlProfileType.DataBind();
            }

            lblPage.Visible = false;
            pnlSearschResult.Visible = false; //pnlActions.Visible =
        }

        //этот метод перед самой отрисовкой страницы биндит все данные
        protected void Page_PreRender(object sender, EventArgs e)
        {
            ListViewData();
        }

        private void ListViewData()
        {
            var dm = new DataManager();
            var ds = dm.QueryWithReturnDataSet(GetSearchString());
            lvAllTickets.DataSource = ds;
            lvAllTickets.DataBind();
            lblAllResult.Text = ds.Tables[0].Rows.Count.ToString();

            lblPage.Visible = lvAllTickets.Items.Count != 0;
            pnlSearschResult.Visible = lvAllTickets.Items.Count != 0; //pnlActions.Visible =
        }

        private string getSelectedTicketsId()
        {
            var idList = String.Empty;
            foreach (ListViewDataItem items in lvAllTickets.Items)
            {
                var chkBoxRows = (CheckBox)items.FindControl("cbSelect");

                if (chkBoxRows.Checked)
                {
                    var id = (HiddenField)items.FindControl("hfID");
                    idList += id.Value + "-";
                }
            }
            idList = idList.Remove(idList.Length - 1);

            return idList;
        }

        protected void btnReload_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.ServerVariables["URL"]);
        }

        protected void bCloseList_Click(object sender, EventArgs e)
        {
            var user = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == user.Role.ToLower());

            foreach (ListViewDataItem items in lvAllTickets.Items)
            {
                var chkBoxRows = (CheckBox)items.FindControl("cbSelect");

                if (chkBoxRows.Checked)
                {
                    var ticketId = (HiddenField)items.FindControl("hfID");
                    var currentDriverId = (HiddenField)items.FindControl("hfDriverID");
                    var currentStatusDescription = (HiddenField)items.FindControl("hfStatusDescription");
                    var currentAdmissionDate = (HiddenField)items.FindControl("hfAdmissionDate");
                    var currentStatusId = (HiddenField)items.FindControl("hfStatusID");
                    var ticket = new DAL.DataBaseObjects.Tickets { ID = Convert.ToInt32(ticketId.Value) };
                    var errorText = TicketsFilter.StatusChangeFilter(ref ticket, currentDriverId.Value, currentStatusId.Value, currentStatusDescription.Value, currentAdmissionDate.Value, "", "6", "", currentRole);
                    if (errorText == null) //если ошибок после фильтрации нет - сохраняем заявку
                        ticket.Update(user.ID, OtherMethods.GetIPAddress(), "UserTicketView");
                    else //выводим все ошибки, если они есть
                        lblNotif.Text += String.Format("{0}<br/>", errorText);
                }
            }

            Response.Redirect(Request.ServerVariables["URL"]);
        }

        protected void bPrintList_Click(object sender, EventArgs e)
        {
            Response.Redirect(String.Format("~/PrintServices/PrintList.aspx?id={0}&type={1}", getSelectedTicketsId(), sddlProfileType.SelectedValue));
        }

        #region Methods
        public String GetSearchString()
        {
            var searchString = String.Empty;
            var searchUserIdString = String.Empty;
            var searchDateString = String.Empty;
            var searchParametres = new Dictionary<String, String>();
            var searchTypeIdString = String.Empty;

            //формируем строку поика для uid
            if (sddlUID.SelectedValue != "0" && !String.IsNullOrEmpty(sddlUID.SelectedValue))
            {
                searchUserIdString += "T.UserID = '" + sddlUID.SelectedValue + "'";
            }

            //формируем строку поика для типа профиля
            if (sddlProfileType.SelectedValue != "0" && !String.IsNullOrEmpty(sddlProfileType.SelectedValue))
            {
                searchTypeIdString += "U.TypeID = '" + sddlProfileType.SelectedValue + "'";
            }

            //формируем конечный запроc для поиска
            searchParametres.Add("UserID", searchUserIdString);
            searchParametres.Add("TypeID", searchTypeIdString);

            foreach (var searchParametre in searchParametres)
            {
                if (!string.IsNullOrEmpty(searchParametre.Value))
                {
                    searchString = searchString + searchParametre.Value + " AND ";
                }
            }

            searchString = searchString.Length < 5 ? "FROM tickets as T WHERE " + avaibleStatusesQuery + " order by T.AdmissionDate DESC" : String.Format("FROM tickets as T JOIN usersprofiles as U on T.UserProfileID = U.ID WHERE {1} AND {0}  order by T.AdmissionDate DESC", searchString.Remove(searchString.Length - 4), avaibleStatusesQuery);

            var searchStringOver = "SELECT T.* " + searchString;

            GetMoney(searchStringOver);

            return searchStringOver;
        }
        
        public void GetMoney(string query)
        {
            var searchString = "FROM (" + query + ") as R";
            var dm = new DataManager();
            var eur = Convert.ToString(dm.QueryWithReturnDataSet("SELECT SUM(ReceivedEUR) " + searchString).Tables[0].Rows[0][0], CultureInfo.CurrentCulture);
            var usd = Convert.ToString(dm.QueryWithReturnDataSet("SELECT SUM(ReceivedUSD) " + searchString).Tables[0].Rows[0][0], CultureInfo.CurrentCulture);
            var rur = Convert.ToString(dm.QueryWithReturnDataSet("SELECT SUM(ReceivedRUR) " + searchString).Tables[0].Rows[0][0], CultureInfo.CurrentCulture);

            var allInBLR = Convert.ToString(dm.QueryWithReturnDataSet("SELECT SUM(ReceivedUSD * CourseUSD + ReceivedRUR * CourseRUR + ReceivedEUR * CourseEUR) " + searchString).Tables[0].Rows[0][0], CultureInfo.CurrentCulture);
            var allGruzobozCost = Convert.ToString(dm.QueryWithReturnDataSet("SELECT SUM(GruzobozCost) " + searchString).Tables[0].Rows[0][0], CultureInfo.CurrentCulture);
            var allAssessedCost = Convert.ToString(dm.QueryWithReturnDataSet("SELECT SUM(AssessedCost + DeliveryCost) " + searchString + " Where AgreedCost = 0 AND WithoutMoney = 0").Tables[0].Rows[0][0], CultureInfo.CurrentCulture);

            //если нет согласованных стоимостей - записываем ее равной 0
            var allAgreedCostString =
                Convert.ToString(dm.QueryWithReturnDataSet("SELECT SUM(AgreedCost) " + searchString +
                                         " Where (AgreedCost > 0 OR WithoutMoney = 1)").Tables[0].Rows[0][0], CultureInfo.CurrentCulture);
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

            var overToIssuance = allAgreedAssessedCostValue - allInBLRValue - ((sddlProfileType.SelectedValue != "0" && !String.IsNullOrEmpty(sddlProfileType.SelectedValue) && sddlProfileType.SelectedValue == "3") ? 0 : allGruzobozCostValue);
            var overToIssuanceString = String.Empty;
            if (overToIssuance > 0)
            {
                overToIssuanceString = "<b>" + MoneyMethods.MoneySeparator(overToIssuance.ToString()) + "</b> BLR";
            }
            else
            {
                overToIssuanceString = "<b>0</b> BLR (+ забрать у клиента <span style =\"color: red; font-weight: bold;\">" + MoneyMethods.MoneySeparator(overToIssuance.ToString().Replace("-", "")) + "</span> BLR)";
            }

            lblReceivedBLRUser.Text = overToIssuanceString;
            lblOverGruzobozCost.Text = MoneyMethods.MoneySeparator(allGruzobozCost);
        }
        
        #endregion
    }
}