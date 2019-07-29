using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using Delivery.BLL;
using Delivery.BLL.Helpers;
using Delivery.BLL.StaticMethods;
using Delivery.DAL;
using Delivery.DAL.DataBaseObjects;
using DeliverySite.Resources;

namespace Delivery.UserUI
{
    public partial class TicketView : UserBasePage
    {
        protected String AppKey { get; set; }

        //сохранение состояния чекбосков СТАРТ
        private List<int> IDs
        {
            get
            {
                if (this.ViewState["IDs"] == null)
                {
                    this.ViewState["IDs"] = new List<int>();
                }
                return (List<int>)this.ViewState["IDs"];
            }
        }

        public List<string> checkIDs
        {
            get
            {
                if (this.ViewState["checkIDs"] == null)
                {
                    this.ViewState["checkIDs"] = new List<string>();
                }
                return (List<string>)this.ViewState["checkIDs"];
            }
        }

        protected double CheckedLbCostSum {
            get
            {
                if (this.ViewState["lbCostSum"] == null)
                {
                    this.ViewState["lbCostSum"] = 0;
                }
                return (double)this.ViewState["lbCostSum"];
            }
            set
            {
                this.ViewState["lbCostSum"] = value;
            }
        }
        protected double CheckedDeliveryCostSum
        {
            get
            {
                if (this.ViewState["lbDeliveryCostSum"] == null)
                {
                    this.ViewState["lbDeliveryCostSum"] = 0;
                }
                return (double)this.ViewState["lbDeliveryCostSum"];
            }
            set
            {
                this.ViewState["lbDeliveryCostSum"] = value;
            }
        }

        protected string Focus;
        protected string ControlType;

        protected void Page_Init(object sender, EventArgs e)
        {            
            btnSearch.Click += btnSearch_Click;
            btnReload.Click += btnReload_Click;
            btnPrintVinil.Click += btnPrintVinil_Click;
            btnPrintVinilTermo.Click += btnPrintVinilTermo_Click;
            btnPrintActORT1.Click += btnPrintActORT_Click;
            btnPrintActORT2.Click += btnPrintActORT_Click;
            btnPrintReturn1.Click += btnPrintActReturn_Click;
            btnPrintReturn2.Click += btnPrintActReturn_Click;

            lvAllTickets.ItemCommand += new EventHandler<ListViewCommandEventArgs>(lvAllTickets_ItemCommand);
            AppKey = Globals.Settings.AppServiceSecureKey;
        }

        void lvAllTickets_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            string sortExpression = null;

            if ((String)e.CommandArgument == "SortByType")            
                sortExpression = "ORDER BY U.TypeID DESC"; 

            if ((String)e.CommandArgument == "SortByCreateDate")
                sortExpression = "ORDER BY T.CreateDate DESC";     
            
            if ((String)e.CommandArgument == "SortByAdmissionDate")
                sortExpression = "ORDER BY T.AdmissionDate DESC";  
            
            if ((String)e.CommandArgument == "SortByDeliveryDate")
                sortExpression = "ORDER BY T.DeliveryDate DESC";
            
            if ((String)e.CommandArgument == "SortByStatus")
                sortExpression = "ORDER BY T.StatusID DESC";

            //if ((String)e.CommandArgument == "OverallCost")
            //sortExpression = "OverallCost DESC";
            //SortBySenderCity
            if ((String)e.CommandArgument == "SortByGruzobozCost")
                sortExpression = "ORDER BY T.GruzobozCost DESC";

            if ((String)e.CommandArgument == "SortBySecureID")
                sortExpression = "ORDER BY T.SecureID DESC";

            if ((String)e.CommandArgument == "SortByCity")
                sortExpression = "ORDER BY T.CityID DESC";

            if (sortExpression != null)
                ViewState.Add("SortExpression", sortExpression);

            ListViewDataBind();
            CheckCheckboxes();

            foreach (ListViewDataItem items in lvAllTickets.Items)
            {
                CheckBox chkBoxRows = (CheckBox)items.FindControl("cbSelect");
                ListViewItem item = (ListViewItem)chkBoxRows.Parent.NamingContainer;
                ListViewDataItem dataItem = (ListViewDataItem)item;
                Label labelID = (Label)(lvAllTickets.Items[dataItem.DisplayIndex]).FindControl("lblSecureID");
                string lbID = labelID.Text.ToString();

                if (this.checkIDs.Contains(lbID))
                {
                    chkBoxRows.Checked = true;
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ListViewDataBind();
            lblError.Text = String.Empty;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = PagesTitles.UserTicketsViewTitle + BackendHelper.TagToValue("page_title_part");
            OtherMethods.ActiveRightMenuStyleChanche("hlTickets", this.Page);

            if (String.IsNullOrEmpty(ActivatedProfilesCount) || Convert.ToInt32(ActivatedProfilesCount) == 0)
            {
                Response.Redirect("~/usernotification/12");
            }
            
            if (!IsPostBack)
            {
                //stbDeliveryDate1.Text = DateTime.Now.AddDays(1).ToString("dd-MM-yyyy");
                //stbDeliveryDate2.Text = DateTime.Now.AddDays(2).ToString("dd-MM-yyyy");

                sddlStatus.DataSource = Tickets.TicketStatuses;
                sddlStatus.DataTextField = "Value";
                sddlStatus.DataValueField = "Key";
                sddlStatus.DataBind();
                sddlStatus.Items.Insert(0, new ListItem("Все", String.Empty));
            }            
        }

        protected void chkBox_OnCheckChange(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            ListViewItem item = (ListViewItem)chk.Parent.NamingContainer;
            ListViewDataItem dataItem = (ListViewDataItem)item;            
            
            var labelCost = (Label)(lvAllTickets.Items[dataItem.DisplayIndex]).FindControl("lbCost");
            var lbCostValue = Convert.ToDouble(labelCost.Text);

            var labelDeliveryCost = (Label)(lvAllTickets.Items[dataItem.DisplayIndex]).FindControl("lbDeliveryCost");
            var lbDeliveryCostValue = Convert.ToDouble(labelDeliveryCost.Text);

            Label labelID = (Label)(lvAllTickets.Items[dataItem.DisplayIndex]).FindControl("lblSecureID");
            string lbID = (labelID.Text).ToString();

            if (chk.Checked == true)
            {
                CheckedLbCostSum += lbCostValue;
                CheckedDeliveryCostSum += lbDeliveryCostValue;

                this.checkIDs.Add(lbID);
            }                
            else
            {
                CheckedLbCostSum -= lbCostValue;
                CheckedDeliveryCostSum -= lbDeliveryCostValue;

                this.checkIDs.Remove(lbID);
            }                

            CheckedLbCost.Text = CheckedLbCostSum.ToString("N2");
            CheckedDeliveryCost.Text = CheckedDeliveryCostSum.ToString("N2");            
        }

        protected void btnPrintVinil_Click(object sender, EventArgs e)
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
            if (idList.Length >=1 )
            {
                idList = idList.Remove(idList.Length - 1);
            }
            
            Response.Redirect(String.Format("~/PrintServices/PrintVinilOnPaper.aspx?id={0}", idList));
            AddRowstoIDList();
        }

        protected void btnPrintActORT_Click(object sender, EventArgs e)
        {
            var idList = String.Empty;
            foreach (ListViewDataItem items in lvAllTickets.Items)
            {
                var chkBoxRows = (CheckBox)items.FindControl("cbSelect");

                if (chkBoxRows.Checked)
                {
                    var id = (HiddenField)items.FindControl("hfIDdef");
                    idList += id.Value + "-";
                }
            }
            if (idList.Length >= 1)
            {
                idList = idList.Remove(idList.Length - 1);
            }
            Response.Redirect(String.Format("~/PrintServices/PrintActORT.aspx?id={0}", idList));
            AddRowstoIDList();
        }

        protected void btnPrintActReturn_Click(object sender, EventArgs e)
        {
            var idList = String.Empty;
            foreach (ListViewDataItem items in lvAllTickets.Items)
            {
                var chkBoxRows = (CheckBox)items.FindControl("cbSelect");

                if (chkBoxRows.Checked)
                {
                    var id = (HiddenField)items.FindControl("hfIDdef");
                    idList += id.Value + "-";
                }
            }
            if (idList.Length >= 1)
            {
                idList = idList.Remove(idList.Length - 1);
            }
            Response.Redirect(String.Format("~/PrintServices/PrintActReturn.aspx?id={0}", idList));
            AddRowstoIDList();
        }

        protected void btnPrintVinilTermo_Click(object sender, EventArgs e)
        {
            var idList = String.Empty;
            foreach (ListViewDataItem items in lvAllTickets.Items)
            {
                var chkBoxRows = (CheckBox)items.FindControl("cbSelect");

                if (chkBoxRows.Checked)
                {
                    var id = (HiddenField)items.FindControl("hfIDdef");
                    idList += id.Value + "-";
                }
            }
            if (idList.Length >= 1)
            {
                idList = idList.Remove(idList.Length - 1);
            }
            Response.Redirect(String.Format("~/PrintServices/PrintVinil.aspx?id={0}", idList));
            AddRowstoIDList();
        }

        public void lbDelete_Click(Object sender, EventArgs e)
        {
            var user = (Users)Session["userinsession"];
            var lb = (LinkButton)sender;
            var ticketOld = new Tickets { ID = Convert.ToInt32(lb.CommandArgument) };
            ticketOld.GetById();
            if (ticketOld.StatusID == 1)
            {
                ticketOld.Delete(Convert.ToInt32(lb.CommandArgument), user.ID, OtherMethods.GetIPAddress(), "TicketView", ticketOld.FullSecureID);
                Page.Response.Redirect("~/UserUI/TicketView.aspx");
            }
            else
            {
                lblError.Text = "Заявка уже обработана менеджером. Вы не можете ее удалить!";
            }
        }

        //этот метод перед самой отрисовкой страницы биндит все данные
        protected void Page_PreRender(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                CheckedLbCostSum = 0;
                CheckedDeliveryCostSum = 0;
                ListViewDataBind();
                CheckCheckboxes();
                if (lvAllTickets.Items.Count == 0)
                {
                    lblPage.Visible = false;
                }

                //прячем ссылку на создание новых заявок
                if (String.IsNullOrEmpty(ActivatedProfilesCount) || Convert.ToInt32(ActivatedProfilesCount) == 0)
                {
                    hlCreateNewTicket.Visible = false;
                }
            }
            else
            {
                ListViewDataBind();   
            }

            /*foreach (ListViewDataItem items in lvAllTickets.Items)
            {
                CheckBox chkBoxRows = (CheckBox)items.FindControl("cbSelect");
                chkBoxRows.CheckedChanged += chkBox_OnCheckChange;
            }*/

            //var userInSession = (Users)Session["userinsession"];
            //var rolesList = Application["RolesList"] as List<Roles>;
            //var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());
            //if (currentRole.ActionPrintAORT != 1)
            //{
            //PrintButtons1.Controls.Remove(btnPrintActORT1);
            //PrintButtons2.Controls.Remove(btnPrintActORT2);
            //}            
        }

        protected void btnReload_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.ServerVariables["URL"]);
        }

        #region Methods

        protected void AddRowstoIDList()
        {
            foreach (ListViewDataItem lvi in lvAllTickets.Items)
            {
                CheckBox chkSelect = (CheckBox)lvi.FindControl("cbSelect");
                if ((((chkSelect) != null)))
                {
                    int ID = Convert.ToInt32(lvAllTickets.DataKeys[lvi.DisplayIndex].Value);

                    //Check if the ID is already there
                    if ((chkSelect.Checked && !this.IDs.Contains(ID)))
                    {
                        this.IDs.Add(ID);
                    }
                    else if ((!chkSelect.Checked && this.IDs.Contains(ID)))
                    {
                        this.IDs.Remove(ID);
                    }
                }
            }

            CheckBox chkSelectAll = (CheckBox)lvAllTickets.FindControl("chkboxSelectAll");
            if ((((chkSelectAll) != null)))
            {
                int ID = 0;
                //Check if the ID is already there
                if ((chkSelectAll.Checked && !this.IDs.Contains(ID)))
                {
                    this.IDs.Add(ID);
                }
                else if ((!chkSelectAll.Checked && this.IDs.Contains(ID)))
                {
                    this.IDs.Remove(ID);
                }
            }
        }

        protected void CheckCheckboxes()
        {
            foreach (ListViewDataItem lvi in lvAllTickets.Items)
            {
                // Get each checkbox Listview Item on DataBound
                var chkSelect = (CheckBox)lvi.FindControl("cbSelect");

                // Make sure we're referencing the correct control
                if ((((chkSelect) != null)))
                {
                    // If the ID exists in our list then check the checkbox
                    int ID = Convert.ToInt32(lvAllTickets.DataKeys[lvi.DisplayIndex].Value);
                    chkSelect.Checked = this.IDs.Contains(ID);
                }
            }
            var chkSelectAll = (CheckBox)lvAllTickets.FindControl("chkboxSelectAll");
            if (chkSelectAll != null)
            {
                chkSelectAll.Checked = IDs.Contains(0);
                lblPage.Visible = true;
            }
            else
            {
                lblPage.Visible = false;
            }
        }

        public void ListViewDataBind()
        {
            var user = (Users)Session["userinsession"];
            var userId = user.Role != Users.Roles.User.ToString() ? 1 : user.ID;
            var dm = new DataManager();
            var ds = dm.QueryWithReturnDataSet(GetSearchString(userId.ToString()));
            ds.Tables[0].Columns.Add("ProfileType", typeof(String));
            ds.Tables[0].Columns.Add("OverallCost", typeof(String));
            foreach (DataRow row in ds.Tables[0].Rows)
            {                
                row["ProfileType"] = UsersProfilesHelper.UserTypeToStr2(UsersProfilesHelper.UserProfileIdToType(Convert.ToInt32(row["UserProfileID"].ToString())).ToString());
                row["OverallCost"] = MoneyMethods.MoneySeparator(MoneyMethods.OveralCostForCheck(MoneyMethods.AgreedAssessedCosts(row["ID"].ToString()), row["DeliveryCost"].ToString()));
            }

            lblAllResult.Text = ds.Tables[0].Rows.Count.ToString();
            lvAllTickets.DataSource = ds;
            lvAllTickets.DataBind();
            
            foreach (ListViewDataItem items in lvAllTickets.Items)
            {
                var deleteLink = (LinkButton)items.FindControl("lbDelete");
                var status = (HiddenField)items.FindControl("lblStatusID");
                if (status.Value != "1")
                {
                    deleteLink.Visible = false;
                }
            }

            foreach (ListViewDataItem items in lvAllTickets.Items)
            {
                CheckBox chkBoxRows = (CheckBox)items.FindControl("cbSelect");
                ListViewItem item = (ListViewItem)chkBoxRows.Parent.NamingContainer;
                ListViewDataItem dataItem = (ListViewDataItem)item;
                Label labelID = (Label)(lvAllTickets.Items[dataItem.DisplayIndex]).FindControl("lblSecureID");
                string lbID = labelID.Text.ToString();

                if (this.checkIDs.Contains(lbID))
                {
                    chkBoxRows.Checked = true;
                }
            }
        }

        public String GetSearchString( string userID )
        {
            var additionalProcessedTime = 0;
            try
            {
                additionalProcessedTime =
                    Convert.ToInt32(BackendHelper.TagToValue("interval_display_tickets_processed"));
            }
            catch (Exception) { } 

            var searchString = String.Empty;
            var searchIdString = String.Empty;
            var searchStatusIdString = String.Empty;
            var searchParametres = new Dictionary<String, String>();
            var searchDateString = String.Empty;
            var seletedCityString = String.Empty;
            var seletedSenderCityString = String.Empty;

            //формируем cтроку для поиска по UserID
            if (!string.IsNullOrEmpty(stbID.Text))
            {
                searchIdString = "T.SecureID = '" + stbID.Text + "'";
            }

            //формируем cтроку для поиска по дате отправки
            if (!string.IsNullOrEmpty(stbDeliveryDate1.Text) && !string.IsNullOrEmpty(stbDeliveryDate2.Text))
            {
                searchDateString = "(T.DeliveryDate BETWEEN '" +
                                   Convert.ToDateTime(stbDeliveryDate1.Text).ToString("yyyy-MM-dd") + "' AND '" +
                                   Convert.ToDateTime(stbDeliveryDate2.Text).ToString("yyyy-MM-dd") + "')";
            }
            else if (!string.IsNullOrEmpty(stbDeliveryDate1.Text) && string.IsNullOrEmpty(stbDeliveryDate2.Text))
            {
                searchDateString = "(T.DeliveryDate BETWEEN '" +
                                   Convert.ToDateTime(stbDeliveryDate1.Text).ToString("yyyy-MM-dd") + "' AND '" +
                                   Convert.ToDateTime(stbDeliveryDate1.Text).AddYears(1).ToString("yyyy-MM-dd") + "')";
            }
            else if (string.IsNullOrEmpty(stbDeliveryDate1.Text) && !string.IsNullOrEmpty(stbDeliveryDate2.Text))
            {
                searchDateString = "(T.DeliveryDate BETWEEN '" +
                                   Convert.ToDateTime(stbDeliveryDate2.Text).AddYears(-2).ToString("yyyy-MM-dd") +
                                   "' AND '" +
                                   Convert.ToDateTime(stbDeliveryDate2.Text).ToString("yyyy-MM-dd") + "')";
            }
            else
            {
                searchDateString = "(T.DeliveryDate)";
            }

            //формируем cтроку для поиска по StatusID
            if (!string.IsNullOrEmpty(sddlStatus.SelectedValue))
            {
                switch (sddlStatus.SelectedValue)
                {
                    case "5":
                        searchStatusIdString =
                            String.Format("(T.`StatusID` = {0} AND (NOW() >= DATE_ADD(T.`ProcessedDate`, INTERVAL {1} HOUR) OR T.`ProcessedDate` IS NULL))",
                                sddlStatus.SelectedValue, 
                                additionalProcessedTime);
                        break;

                    case "6":
                        searchStatusIdString = 
                            String.Format("(T.StatusID = '{0}' AND (T.CompletedDate BETWEEN '{1}' AND '{2}'))", 
                            sddlStatus.SelectedValue, 
                            DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd"), 
                            DateTime.Now.AddDays(1).ToString("yyyy-MM-dd"));
                        break;

                    default:
                        searchStatusIdString = "(T.StatusID = '" + sddlStatus.SelectedValue +
                                               "' || (NOW() < DATE_ADD(T.`ProcessedDate`, INTERVAL " + additionalProcessedTime + " HOUR) && T.`ProcessedDate` IS NOT NULL && T.StatusIDOld = '" + sddlStatus.SelectedValue + "'))";
                        break;
                }

                if (BackendHelper.TagToValue("delivered_tickets_is_visible_for_client") == "true")
                {
                    if (sddlStatus.SelectedValue == "3")
                    {
                        searchStatusIdString ="(T.StatusID = '3' OR T.StatusID = '12')";
                    }

                    if (sddlStatus.SelectedValue == "12")
                    {
                        searchStatusIdString = "(T.StatusID = '300')";
                    }
                }
                
            }

            //формируем строку поика для городов
            foreach (var selectedIndex in shfCityID.Value.Split(';').ToList())
            {
                if (!string.IsNullOrEmpty(shfCityID.Value))
                {
                    if (selectedIndex != "-1")
                    {
                        seletedCityString = seletedCityString + "T.CityID = '" + selectedIndex + "' OR ";
                    }
                    else
                    {
                        seletedCityString = seletedCityString + "T.CityID = '-1' OR ";
                    }
                }
            }
            if (seletedCityString.Length > 3)
            {
                seletedCityString = "(" + seletedCityString.Remove(seletedCityString.Length - 3) + ")";
            }


            //формируем строку поика для городов Отправления
            foreach (var selectedIndex in shfSenderCityID.Value.Split(';').ToList())
            {
                if (!string.IsNullOrEmpty(shfSenderCityID.Value))
                {
                    if (selectedIndex != "-1")
                    {
                        seletedSenderCityString = seletedSenderCityString + "T.SenderCityID = '" + selectedIndex + "' OR ";
                    }
                    else
                    {
                        seletedSenderCityString = seletedSenderCityString + "T.SenderCityID = '-1' OR ";
                    }
                }
            }
            if (seletedSenderCityString.Length > 3)
            {
                seletedSenderCityString = "(" + seletedSenderCityString.Remove(seletedSenderCityString.Length - 3) + ")";
            }

            //формируем конечный запро для поиска
            searchParametres.Add("UserID", searchIdString);
            searchParametres.Add("StatusID", searchStatusIdString);
            searchParametres.Add("DeliveryDate", searchDateString);
            searchParametres.Add("CityID", seletedCityString);
            searchParametres.Add("SenderCityID", seletedSenderCityString);

            searchString = searchParametres.Where(searchParametre => !string.IsNullOrEmpty(searchParametre.Value)).Aggregate(searchString, (current, searchParametre) => current + searchParametre.Value + " AND ");

            string sortExpression = null;
            if (ViewState["SortExpression"] == null)
            {
                sortExpression = "ORDER BY T.ID DESC";
            }
            else
            {
                sortExpression = ViewState["SortExpression"].ToString();
            }      

            const string selectFields = "U.TypeID, T.ID, T.UserId, T.UserProfileID, T.StatusID, T.StatusIDOld, T.CreateDate, T.AdmissionDate, T.DeliveryDate, T.SecureID, T.ProcessedDate, T.DeliveryCost, T.GruzobozCost, T.CityID, T.SenderCityID, T.SenderProfileID";
            
                searchString = searchString.Length < 4
                    ? String.Format("SELECT {1} " +
                                    "FROM `tickets` as T JOIN `usersprofiles` as U ON T.`UserProfileID` = U.`ID` " +
                                    "WHERE T.`UserID` = {2} " +
                                    "AND U.`StatusID` = 1 " +
                                    "AND (T.`StatusID` != 6 OR (T.`UserID` = {2} AND T.`StatusID` = 6 AND (T.`CompletedDate` BETWEEN '{3}' AND '{4}')) ) {0};", 
                                    sortExpression, 
                                    selectFields, 
                                    userID, 
                                    DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd"), 
                                    DateTime.Now.AddDays(1).ToString("yyyy-MM-dd"))
                    : String.Format("SELECT {2} FROM `tickets` as T JOIN `usersprofiles` as U ON T.`UserProfileID` = U.`ID` WHERE T.`UserID` = {3} AND U.`StatusID` = 1 AND {0} {1}", searchString.Remove(searchString.Length - 4), sortExpression, selectFields, userID);

            return searchString;
        }
        #endregion
    }
}