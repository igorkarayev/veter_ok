using Delivery.BLL.Filters;
using Delivery.BLL.Helpers;
using Delivery.BLL.StaticMethods;
using Delivery.DAL;
using Delivery.DAL.DataBaseObjects;
using DeliverySite.Resources;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web.UI.WebControls;

namespace Delivery.ManagerUI.Menu.Tickets
{
    public partial class UserTicketNotProcessedView : ManagerBasePage
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
                ddlAction.Visible = true;
                btnAction.Visible = true;
            }
            else
            {
                lblPage.Visible = false;
                ddlAction.Visible = false;
                btnAction.Visible = false;
            }
        }

        //сохранение состояния чекбосков КОНЕЦ

        protected void Page_Init(object sender, EventArgs e)
        {
            btnAction.Click += btnAction_Click;
            btnSearch.Click += btnSearch_Click;
            btnReload.Click += btnReload_Click;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = PagesTitles.ManagerUserTicketNotProcessedView + BackendHelper.TagToValue("page_title_part");
            OtherMethods.ActiveRightMenuStyleChanche("hlTickets", this.Page);
            OtherMethods.ActiveRightMenuStyleChanche("hlTicketsNotProcessed", this.Page);

            #region Блок доступа к странице
            var userInSession = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());
            if (currentRole.PageUserTicketNotProcessedView != 1)
            {
                Response.Redirect("~/Error.aspx?id=1");
            }
            #endregion

            AppKey = Globals.Settings.AppServiceSecureKey;
            if (Session["flash:now"] != null && Session["flash:now"].ToString() != String.Empty)
            {
                lblStatus.Text = Session["flash:now"].ToString();
                Session["flash:now"] = String.Empty;
            }

            if (!IsPostBack)
            {
                //разбор параметров для сохранения состояния фильтра

                #region Блок визуализации ссылок
                if (currentRole.ActionGroupUserTicketDelete == 1)
                {
                    ddlAction.Items.Add("Удалить");
                }

                if (currentRole.ActionStatusAdd == 1)
                {
                    ddlAction.Items.Add("Изменить статус");
                }

                if (currentRole.ActionPrintVinil == 1)
                {
                    ddlAction.Items.Add("Печать наклеек");
                }

                if (currentRole.ActionPrintCheck == 1)
                {
                    ddlAction.Items.Add("Печать чеков");
                }

                if (currentRole.ActionPrintAORT == 1)
                {
                    ddlAction.Items.Add("Печать акта приема-передачи");
                }
                #endregion

                var track = new Tracks();
                var dataSet4 = track.GetAllItems("Name", "ASC", null);
                sddlTracks.DataSource = dataSet4;
                sddlTracks.DataTextField = "Name";
                sddlTracks.DataValueField = "ID";
                sddlTracks.DataBind();
                sddlTracks.Items.Insert(0, new ListItem("Все", string.Empty));
                sddlTracks.Items.Add(new ListItem("Без направления", "0"));

                ddlStatus.DataSource = DAL.DataBaseObjects.Tickets.TicketStatuses;
                ddlStatus.DataTextField = "Value";
                ddlStatus.DataValueField = "Key";
                ddlStatus.DataBind();

                #region Видимость статусов
                if (currentRole.StatusNotProcessed != 1)
                {
                    ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("1"));
                }

                /*if (currentRole.StatusInStock != 1)
                {*/
                ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("2"));
                /*}*/

                if (currentRole.StatusOnTheWay != 1)
                {
                    ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("3"));
                }

                if (currentRole.StatusProcessed != 1)
                {
                    ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("5"));
                }

                if (currentRole.StatusDelivered != 1)
                {
                    ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("12"));
                }

                if (currentRole.StatusCompleted != 1)
                {
                    ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("6"));
                }

                if (currentRole.StatusTransferInCourier != 1)
                {
                    ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("11"));
                }

                if (currentRole.StatusRefusingInCourier != 1)
                {
                    ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("7"));
                }

                if (currentRole.StatusExchangeInCourier != 1)
                {
                    ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("13"));
                }

                if (currentRole.StatusDeliveryFromClientInCourier != 1)
                {
                    ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("14"));
                }

                /*if (currentRole.StatusTransferInStock != 1)
                {*/
                ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("4"));
                /*}*/

                if (currentRole.StatusReturnInStock != 1)
                {
                    ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("8"));
                }

                if (currentRole.StatusCancelInStock != 1)
                {
                    ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("9"));
                }

                if (currentRole.StatusExchangeInStock != 1)
                {
                    ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("15"));
                }

                if (currentRole.StatusDeliveryFromClientInStock != 1)
                {
                    ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("16"));
                }

                if (currentRole.StatusCancel != 1)
                {
                    ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("10"));
                }

                if (currentRole.StatusRefusalOnTheWay != 1)
                {
                    ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("17"));
                }

                if (currentRole.StatusRefusalByAddress != 1)
                {
                    ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("18"));
                }
                #endregion

                ddlStatus.Visible = true;

                if (!string.IsNullOrEmpty(Page.Request.Params["stateSave"]))
                {
                    if (!string.IsNullOrEmpty(Page.Request.Params["sid"]))
                    {
                        stbID.Text = Page.Request.Params["sid"];
                    }

                    if (!string.IsNullOrEmpty(Page.Request.Params["uid"]))
                    {
                        stbUID.Text = Page.Request.Params["uid"];
                    }

                    if (!string.IsNullOrEmpty(Page.Request.Params["recipientPhone"]))
                    {
                        stbRecipientPhone.Text = Page.Request.Params["recipientPhone"];
                    }

                    if (!string.IsNullOrEmpty(Page.Request.Params["cityID"]))
                    {
                        shfCityID.Value = Page.Request.Params["cityID"];
                    }

                    if (!string.IsNullOrEmpty(Page.Request.Params["trackID"]))
                    {
                        sddlTracks.SelectedValue = Page.Request.Params["trackID"];
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
            }
        }

        //этот метод перед самой отрисовкой страницы биндит все данные
        protected void Page_PreRender(object sender, EventArgs e)
        {
            BindListView(hfSortExpression.Value);
            CheckCheckboxes();
        }

        protected void btnAction_Click(object sender, EventArgs e)
        {
            lblNotif.Text = String.Empty;
            var user = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == user.Role.ToLower());
            if (ddlAction.SelectedValue == "Удалить")
            {
                DeleteAccess();
                foreach (ListViewDataItem items in lvAllTickets.Items)
                {
                    var chkBoxRows = (CheckBox)items.FindControl("cbSelect");

                    if (chkBoxRows.Checked)
                    {
                        var id = (HiddenField)items.FindControl("hfID");
                        var ticketOld = new DAL.DataBaseObjects.Tickets
                        {
                            ID = Convert.ToInt32(id.Value)
                        };
                        ticketOld.GetById();
                        if (ticketOld.StatusID == 1 || user.Role == Users.Roles.Admin.ToString())
                        {
                            var ticket = new DAL.DataBaseObjects.Tickets();
                            ticket.Delete(Convert.ToInt32(id.Value), user.ID, OtherMethods.GetIPAddress(), "UserTicketNotProcessedView", ticketOld.FullSecureID);
                        }
                        else
                        {
                            lblNotif.Text = "Были удалены заявки со статусом 'Не обработана'. Заявки с другими статусами удалить не возможно!";
                            lblNotif.ForeColor = Color.Red;
                        }
                    }
                }
            }



            if (ddlAction.SelectedValue == "Изменить статус")
            {
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
                        var errorText = TicketsFilter.StatusChangeFilter(ref ticket, currentDriverId.Value, currentStatusId.Value, currentStatusDescription.Value, currentAdmissionDate.Value, tbStatusDescription.Text, ddlStatus.SelectedValue, tbDeliveryDate.Text, currentRole);
                        if (errorText == null) //если ошибок после фильтрации нет - сохраняем заявку
                            ticket.Update(user.ID, OtherMethods.GetIPAddress(), "UserTicketView");
                        else //выводим все ошибки, если они есть
                            lblNotif.Text += String.Format("{0}<br/>", errorText);
                    }
                }
            }

            if (ddlAction.SelectedValue == "Печать чеков"
                || ddlAction.SelectedValue == "Печать наклеек"
                || ddlAction.SelectedValue == "Печать акта приема-передачи")
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
                if (ddlAction.SelectedValue == "Печать чеков")
                {
                    Response.Redirect(String.Format("~/PrintServices/PrintCheck.aspx?id={0}&page=userticketsnotprocessed&{1}", idList, OtherMethods.LinkBuilder(stbID.Text,
                        stbUID.Text, stbRecipientPhone.Text, shfCityID.Value, String.Empty, String.Empty, stbDeliveryDate1.Text, stbDeliveryDate2.Text,
                        sddlTracks.SelectedValue)));
                }

                if (ddlAction.SelectedValue == "Печать наклеек")
                {
                    Response.Redirect(String.Format("~/PrintServices/PrintVinil.aspx?id={0}", idList));
                }

                if (ddlAction.SelectedValue == "Печать акта приема-передачи")
                {
                    Response.Redirect(String.Format("~/PrintServices/PrintAORT.aspx?id={0}", idList));
                }
            }

            AddRowstoIDList();
        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            lblNotif.Text = String.Empty;
        }


        protected void btnReload_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.ServerVariables["URL"]);
        }

        protected void lvAllTickets_OnSortingButtonClick(object sender, EventArgs e)
        {
            var commandArgument = ((LinkButton)sender).CommandArgument;

            string sortDirection;

            if (String.IsNullOrEmpty(hfSortDirection.Value))
            {
                sortDirection = "ASC";
                hfSortDirection.Value = "1";
            }
            else
            {
                if (hfSortDirection.Value == "0")
                {
                    sortDirection = "ASC";
                    hfSortDirection.Value = "1";
                }
                else
                {
                    sortDirection = "DESC";
                    hfSortDirection.Value = "0";
                }
            }
            hfSortExpression.Value = "ORDER BY " + commandArgument + " " + sortDirection;

        }

        private void BindListView(string SortExpression)
        {
            var dm = new DataManager();
            var ds = dm.QueryWithReturnDataSet(GetSearchString(SortExpression));
            lvAllTickets.DataSource = ds;
            lblAllResult.Text = ds.Tables[0].Rows.Count.ToString();
            lvAllTickets.DataBind();

            #region Редирект на первую страницу при поиске
            if (lvAllTickets.Items.Count == 0 && lvDataPager.TotalRowCount != 0)
            {
                lvDataPager.SetPageProperties(0, lvDataPager.PageSize, false);
                lvAllTickets.DataBind();
            }
            #endregion
        }

        #region Настройки доступа к странице и действиям
        protected void DeleteAccess()
        {
            var userInSession = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());
            if (currentRole.ActionGroupUserTicketDelete != 1)
            {
                Response.Redirect("~/Error.aspx?id=1");
            }
        }
        #endregion

        #region Methods
        public String GetSearchString(string sortExpression)
        {
            var searchString = String.Empty;
            var seletedCityString = String.Empty;
            var searchUserIdString = String.Empty;
            var searchTicketIdString = String.Empty;
            var seletedTrackString = String.Empty;
            var searchRecipientPhoneString = String.Empty;
            var searchDateString = String.Empty;
            var searchCheckedOut = String.Empty;
            var searchPhoned = String.Empty;
            var searchBilled = String.Empty;
            var searchNotMinsk = String.Empty;
            var searchParametres = new Dictionary<String, String>();

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

            //формируем строку поика для направлений
            foreach (var selectedIndex in sddlTracks.GetSelectedIndices())
            {
                if (!string.IsNullOrEmpty(sddlTracks.Items[selectedIndex].Value))
                {
                    if (sddlTracks.Items[selectedIndex].Value != "0")
                    {
                        seletedTrackString = seletedTrackString + "(C.TrackID = '" +
                                             sddlTracks.Items[selectedIndex].Value + "' OR TrackIDUser = '" +
                                             sddlTracks.Items[selectedIndex].Value + "') OR ";
                    }
                    else
                    {
                        seletedTrackString = seletedTrackString + "(C.TrackID = '0' AND TrackIDUser = '0') OR ";
                    }
                }
            }
            if (seletedTrackString.Length > 3)
            {
                seletedTrackString = "(" + seletedTrackString.Remove(seletedTrackString.Length - 3) + ")";
            }

            //формируем cтроку для поиска по UserID
            if (!string.IsNullOrEmpty(stbUID.Text))
            {
                searchUserIdString = "T.UserID = '" + stbUID.Text + "'";
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

            //формируем cтроку для поиска по TicketSeureID
            if (!string.IsNullOrEmpty(stbID.Text))
            {
                searchTicketIdString = "T.SecureID = '" + stbID.Text + "'";
            }

            //формируем cтроку для поиска по RecipientPhone
            if (!string.IsNullOrEmpty(stbRecipientPhone.Text))
            {
                searchRecipientPhoneString = string.Format("(T.RecipientPhone = '{0}' OR T.RecipientPhoneTwo = '{0}')", stbRecipientPhone.Text);
            }

            //формируем cтроку для поиска по CheckedOut
            if (cbNotCheckedOut.Checked)
            {
                searchCheckedOut = "T.`CheckedOut` != '1'";
            }

            //формируем cтроку для поиска по Phoned
            if (cbNotPhoned.Checked)
            {
                searchPhoned = "T.`Phoned` != '1'";
            }

            //формируем cтроку для поиска по Billed
            if (cbNotBilled.Checked)
            {
                searchBilled = "T.`Billed` != '1'";
            }

            //формируем строку для поиска по SenderCityID
            if (cbNotMinsk.Checked)
            {
                searchNotMinsk = "(T.`SenderStreetName` != '" + BackendHelper.TagToValue("loading_point_street") + "' AND T.`SenderStreetNumber` != '" + BackendHelper.TagToValue("loading_point_street_number") + "')";
            }


            //формируем конечный запро для поиска
            searchParametres.Add("UserID", searchUserIdString);
            searchParametres.Add("CityID", seletedCityString);
            searchParametres.Add("SecureID", searchTicketIdString);
            searchParametres.Add("TrackID", seletedTrackString);
            searchParametres.Add("RecipientPhone", searchRecipientPhoneString);
            searchParametres.Add("DeliveryDate", searchDateString);
            searchParametres.Add("CheckedOut", searchCheckedOut);
            searchParametres.Add("Phoned", searchPhoned);
            searchParametres.Add("Billed", searchBilled);
            searchParametres.Add("NotMinsk", searchNotMinsk);

            foreach (var searchParametre in searchParametres)
            {
                if (!string.IsNullOrEmpty(searchParametre.Value))
                {
                    searchString = searchString + searchParametre.Value + " AND ";
                }
            }


            if (String.IsNullOrEmpty(sortExpression))
            {
                sortExpression = "ORDER BY C.Name ASC";
            }

            const string selectFields = "T.WithoutMoney, " +
                                        "T.CheckedOut, " +
                                        "T.Phoned, " +
                                        "T.Billed, " +
                                        "T.IsExchange, " +
                                        "T.ID, " +
                                        "T.StatusID, " +
                                        "T.StatusDescription, " +
                                        "T.DriverID, " +
                                        "T.AdmissionDate, " +
                                        "T.SecureID, " +
                                        "T.FullSecureID, " +
                                        "T.UserID, " +
                                        "T.UserProfileID, " +
                                        "T.BoxesNumber, " +
                                        "T.CityID, " +
                                        "T.RecipientStreetPrefix, " +
                                        "T.RecipientStreet, " +
                                        "T.RecipientFirstName, " +
                                        "T.RecipientLastName, " +
                                        "T.RecipientThirdName, " +
                                        "T.RecipientPhone, " +
                                        "T.RecipientPhoneTwo, " +
                                        "T.DeliveryDate, " +
                                        "T.DeliveryCost, " +
                                        "T.GruzobozCost, " +
                                        "T.PrintNakl, " +
                                        "T.PrintNaklInMap, " +
                                        "T.AvailableOtherDocuments, " +
                                        "T.Weight, " +
                                        "T.SenderStreetName, " +
                                        "T.SenderCityID";

            searchString = searchString.Length < 4
                    ? String.Format("SELECT {1} FROM `tickets` as T JOIN `city` as C on T.CityID = C.ID WHERE T.StatusID = 1 {0}", sortExpression, selectFields)
                    : String.Format("SELECT {2} FROM `tickets` as T JOIN `city` as C on T.CityID = C.ID  WHERE T.StatusID = 1 AND  {0} {1}", searchString.Remove(searchString.Length - 4), sortExpression, selectFields);

            return searchString;
        }
        #endregion
    }
}