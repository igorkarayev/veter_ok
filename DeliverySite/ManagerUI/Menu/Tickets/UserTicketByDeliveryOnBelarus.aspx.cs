using Delivery.BLL;
using Delivery.BLL.Filters;
using Delivery.BLL.Helpers;
using Delivery.BLL.StaticMethods;
using Delivery.DAL;
using Delivery.DAL.DataBaseObjects;
using DeliverySite.Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;

namespace Delivery.ManagerUI.Menu.Tickets
{
    public partial class UserTicketByDeliveryOnBelarus : ManagerBasePage
    {
        protected String AppKey { get; set; }

        protected bool IsVisibleCityOrder { get; set; }

        protected bool IsVisibleUserProfile { get; set; }

        protected bool IsVisibleDeliveredForCity { get; set; }

        protected String TickedIdListString { get; set; }

        protected String DriverSearchString { get; set; }

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
            btnReload.Click += btnReload_Click;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = PagesTitles.ManagerUserTicketViewTitle + BackendHelper.TagToValue("page_title_part");
            OtherMethods.ActiveRightMenuStyleChanche("hlTickets", this.Page);
            OtherMethods.ActiveRightMenuStyleChanche("hlUserTicketByDeliveryOnBelarus", this.Page);
            AppKey = Globals.Settings.AppServiceSecureKey;

            #region Блок доступа к странице
            var userInSession = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());
            if (currentRole.PageUserTicketByDeliveryOnBelarus != 1)
            {
                Response.Redirect("~/Error.aspx?id=1");
            }
            #endregion

            IsVisibleCityOrder = true;
            IsVisibleUserProfile = true;
            IsVisibleDeliveredForCity = true;
            pnlCityOrder.Visible = true;
            if (currentRole.ActionDeliveredButtonInCity == 0)
            {
                IsVisibleDeliveredForCity = false;
                pnlDeliveredForCity.Visible = false;
            }

            var sendToRouteAccessArray = BackendHelper.TagToValue("send_to_123route_access");
            btnSendToRouteRu.Visible =
                btnGetFromRouteRu.Visible =
                    btnDownloadXmlForRouteRu.Visible =
                        btnDownloadXmlCityForRouteRu.Visible = sendToRouteAccessArray.Split(new[] { ',' }).Any(p => p.Trim().Contains(userInSession.ID.ToString())); ;

            if (currentRole.PageUserProfileView == 0)
            {
                IsVisibleUserProfile = false;
            }
            hfIsVisibleUserProfile.Value = IsVisibleUserProfile.ToString();

            //механизм нотификаций
            lblStatus.Text = String.Empty;
            if (Session["flash:now"] != null && Session["flash:now"].ToString() != String.Empty)
            {
                lblStatus.Text = Session["flash:now"].ToString();
                Session["flash:now"] = String.Empty;
            }

            if (!IsPostBack)
            {
                stbDeliveryDate1.Text = DateTime.Now.AddDays(1).ToString("dd-MM-yyyy");
                stbDeliveryDate2.Text = DateTime.Now.AddDays(2).ToString("dd-MM-yyyy");
                //разбор параметров для сохранения состояния фильтра

                #region Блок визуализации ссылок
                if (currentRole.ActionGroupUserTicketDelete == 1)
                {
                    ddlAction.Items.Add("Удалить");
                }

                if (currentRole.ActionDriverAdd == 1)
                {
                    ddlAction.Items.Add("Добавить водителя");
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

                if (currentRole.ActionPrintMap == 1)
                {
                    ddlAction.Items.Add("Печать карты (кур.)");
                }

                if (currentRole.ActionPrintMapForManager == 1)
                {
                    ddlAction.Items.Add("Печать карты (мен.)");
                }

                if (currentRole.ActionPrintMapForCashier == 1)
                {
                    ddlAction.Items.Add("Печать карты (кас.)");
                }

                if (currentRole.ActionPrintTickets == 1)
                {
                    ddlAction.Items.Add("Печать заявок");
                }

                if (currentRole.ActionPrintZP == 1)
                {
                    ddlAction.Items.Add("Печать заказ-поручений");
                }

                if (currentRole.ActionPrintNakl == 1)
                {
                    ddlAction.Items.Add("Печать накладной");
                }

                if (currentRole.ActionPrintNaklPril == 1)
                {
                    ddlAction.Items.Add("Печать приложения");
                }

                if (userInSession.PrilNaklTwoAccess)
                {
                    ddlAction.Items.Add("Печать приложения 2");
                }

                if (currentRole.ActionPrintPutFirst == 1)
                {
                    ddlAction.Items.Add("Печать путевого листа 1");
                }

                if (currentRole.ActionPrintPutSecond == 1)
                {
                    ddlAction.Items.Add("Печать путевого листа 2");
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


                sddlStatus.DataSource = DAL.DataBaseObjects.Tickets.TicketStatuses;
                sddlStatus.DataTextField = "Value";
                sddlStatus.DataValueField = "Key";
                sddlStatus.DataBind();
                sddlStatus.Items.Insert(0, new ListItem("Все", string.Empty));

                ddlStatus.DataSource = DAL.DataBaseObjects.Tickets.TicketStatuses;
                ddlStatus.DataTextField = "Value";
                ddlStatus.DataValueField = "Key";
                ddlStatus.DataBind();

                #region Видимость статусов
                if (currentRole.StatusNotProcessed != 1)
                {
                    ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("1"));
                }

                if (currentRole.StatusInStock != 1)
                {
                    ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("2"));
                }

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

                if (currentRole.StatusTransferInStock != 1)
                {
                    ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("4"));
                }

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

                    if (!string.IsNullOrEmpty(Page.Request.Params["statusID"]))
                    {
                        sddlStatus.SelectedValue = Page.Request.Params["statusID"];
                    }

                    if (!string.IsNullOrEmpty(Page.Request.Params["driverID"]) && sddlDrivers.Items.FindByValue(Page.Request.Params["driverID"]) != null)
                    {
                        sddlDrivers.SelectedValue = Page.Request.Params["driverID"];
                    }

                    if (!string.IsNullOrEmpty(Page.Request.Params["trackID"]))
                    {
                        sddlTracks.SelectedValue = Page.Request.Params["trackID"];
                    }

                    stbDeliveryDate1.Text = !string.IsNullOrEmpty(Page.Request.Params["deliveryDate1"]) ? Page.Request.Params["deliveryDate1"] : String.Empty;

                    stbDeliveryDate2.Text = !string.IsNullOrEmpty(Page.Request.Params["deliveryDate2"]) ? Page.Request.Params["deliveryDate2"] : String.Empty;
                }
            }
        }


        //этот метод перед самой отрисовкой страницы биндит все данные
        protected void Page_PreRender(object sender, EventArgs e)
        {
            BindListView(hfSortExpression.Value);
            CheckCheckboxes();

            hfsddlDriverID.Value = sddlDrivers.SelectedValue;
            //прописываем водителей только после получения значения DriverSearchString
            var dm = new DataManager();
            var ds1 = dm.QueryWithReturnDataSet("SELECT ID, FirstName, LastName, ThirdName FROM drivers WHERE ID IN (" + DriverSearchString + ") ORDER BY FirstName");
            ds1.Tables[0].Columns.Add("FIOAndID", typeof(string), "'(' + ID + ') ' + FirstName + ' ' + SUBSTRING(LastName,1,1) + '.' +  SUBSTRING(ThirdName,1,1) + '.'");
            sddlDrivers.DataSource = ds1;
            sddlDrivers.DataTextField = "FIOAndID";
            sddlDrivers.DataValueField = "ID";
            sddlDrivers.DataBind();
            sddlDrivers.Items.Insert(0, new ListItem("Все", string.Empty));
            sddlDrivers.Items.Add(new ListItem("Водитель не назначен", "0"));
            if (sddlDrivers.Items.FindByValue(hfsddlDriverID.Value) != null)
                sddlDrivers.SelectedValue = hfsddlDriverID.Value;

            var driver = new Drivers { StatusID = 1 };
            var ds2 = driver.GetAllActivatedDrivers();
            ds2.Tables[0].Columns.Add("FIOAndID", typeof(string), "'(' + ID + ') ' + FirstName + ' ' + SUBSTRING(LastName,1,1) + '.' +  SUBSTRING(ThirdName,1,1) + '.'");
            ddlDrivers.DataSource = ds2;
            ddlDrivers.DataTextField = "FIOAndID";
            ddlDrivers.DataValueField = "ID";
            ddlDrivers.DataBind();
            ddlDrivers.Items.Insert(0, new ListItem("Водитель не назначен", "0"));
            ddlDrivers.Visible = true;
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
                            ticket.Delete(Convert.ToInt32(id.Value), user.ID, OtherMethods.GetIPAddress(), "UserTicketView", ticketOld.FullSecureID);
                        }
                        else
                        {
                            lblNotif.Text = "Были удалены заявки со статусом 'Не обработана'. Заявки с другими статусами удалить не возможно!";
                            lblNotif.ForeColor = Color.Red;
                        }
                    }
                }
            }


            if (ddlAction.SelectedValue == "Добавить водителя")
            {
                foreach (ListViewDataItem items in lvAllTickets.Items)
                {
                    var chkBoxRows = (CheckBox)items.FindControl("cbSelect");

                    if (chkBoxRows.Checked)
                    {
                        var ticketId = (HiddenField)items.FindControl("hfID");
                        var currentDriverId = (HiddenField)items.FindControl("hfDriverID");
                        var currentStatusId = (HiddenField)items.FindControl("hfStatusID");
                        var oldStatusId = (HiddenField)items.FindControl("hfStatusIDOld");
                        var ticket = new DAL.DataBaseObjects.Tickets { ID = Convert.ToInt32(ticketId.Value) };
                        var errorText = TicketsFilter.DriverChangeFilter(ref ticket, currentDriverId.Value, currentStatusId.Value, oldStatusId.Value, ddlDrivers.SelectedValue);
                        if (errorText == null) //если ошибок после фильтрации нет - сохраняем заявку
                            ticket.Update(user.ID, OtherMethods.GetIPAddress(), "UserTicketView");
                        else //выводим все ошибки, если они есть
                            lblNotif.Text += String.Format("{0}<br/>", errorText);
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

            if (ddlAction.SelectedValue == "Печать карты (кур.)"
                || ddlAction.SelectedValue == "Печать карты (мен.)"
                || ddlAction.SelectedValue == "Печать карты (кас.)"
                || ddlAction.SelectedValue == "Печать заявок"
                || ddlAction.SelectedValue == "Печать чеков"
                || ddlAction.SelectedValue == "Печать наклеек"
                || ddlAction.SelectedValue == "Печать заказ-поручений"
                || ddlAction.SelectedValue == "Печать накладной"
                || ddlAction.SelectedValue == "Печать приложения"
                || ddlAction.SelectedValue == "Печать приложения 2"
                || ddlAction.SelectedValue == "Печать путевого листа 1"
                || ddlAction.SelectedValue == "Печать путевого листа 2"
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

                if (ddlAction.SelectedValue == "Печать карты (кур.)")
                {
                    Response.Redirect(String.Format("~/PrintServices/PrintMap.aspx?id={0}", idList));
                }

                if (ddlAction.SelectedValue == "Печать карты (мен.)")
                {
                    Response.Redirect(String.Format("~/PrintServices/PrintMapForManager.aspx?id={0}", idList));
                }

                if (ddlAction.SelectedValue == "Печать карты (кас.)")
                {
                    Response.Redirect(String.Format("~/PrintServices/PrintMapForCashier.aspx?id={0}", idList));
                }

                if (ddlAction.SelectedValue == "Печать заявок")
                {
                    Response.Redirect(String.Format("~/PrintServices/PrintTickets.aspx?id={0}", idList));
                }

                if (ddlAction.SelectedValue == "Печать чеков")
                {
                    Response.Redirect(String.Format("~/PrintServices/PrintCheck.aspx?id={0}", idList));
                }

                if (ddlAction.SelectedValue == "Печать наклеек")
                {
                    Response.Redirect(String.Format("~/PrintServices/PrintVinil.aspx?id={0}", idList));
                }

                if (ddlAction.SelectedValue == "Печать заказ-поручений")
                {
                    Response.Redirect(String.Format("~/PrintServices/PrintZP.aspx?id={0}", idList));
                }

                if (ddlAction.SelectedValue == "Печать накладной")
                {
                    Response.Redirect(String.Format("~/PrintServices/PrintNakl.aspx?id={0}", idList));
                }

                if (ddlAction.SelectedValue == "Печать приложения")
                {
                    Response.Redirect(String.Format("~/PrintServices/PrintNaklPril.aspx?id={0}", idList));
                }

                if (ddlAction.SelectedValue == "Печать приложения 2")
                {
                    Response.Redirect(String.Format("~/PrintServices/PrintNaklPril2.aspx?id={0}", idList));
                }

                if (ddlAction.SelectedValue == "Печать путевого листа 1")
                {
                    Response.Redirect(String.Format("~/PrintServices/PrintPut1.aspx?id={0}", idList));
                }

                if (ddlAction.SelectedValue == "Печать путевого листа 2")
                {
                    Response.Redirect(String.Format("~/PrintServices/PrintPut2.aspx?id={0}", idList));
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

        protected void btnSendToRouteRu_Click(object sender, EventArgs e)
        {
            Www123RouteMethods.GenerateXmlFile(hfIdList.Value, stbDeliveryDate1.Text);
            var result = Www123RouteMethods.SendData();
            if (result != null && result.ErrorLevel == "None")
            {
                lblStatus.Text = "<span style='color: green !important;'>Данные успешно отправлены. Ответ: " + result.Message + "</span>";
            }

            if (result != null && result.ErrorLevel != "None")
            {
                lblStatus.Text = "<span style='color: red !important;'>Ошибка типа " + result.ErrorLevel + ": " + result.Message + "</span>";
            }

            if (result == null)
            {
                lblStatus.Text = "<span style='color: black !important;'>Нет данных на отправку</span>";
            }

            AddRowstoIDList();
        }

        protected void btnGetFromRouteRu_Click(object sender, EventArgs e)
        {
            Www123RouteMethods.GetByDate(Convert.ToDateTime(stbDeliveryDate1.Text), Convert.ToDateTime(stbDeliveryDate1.Text));
            AddRowstoIDList();
        }

        protected void btnDownloadXmlCityForRouteRu_Click(object sender, EventArgs e)
        {
            Www123RouteMethods.GenerateXmlCityFile(hfCityIdList.Value, stbDeliveryDate1.Text);
            const string sGenName = "xml_города_для_route.ru.xml";
            Response.AddHeader("Content-disposition", "attachment; filename=" + sGenName);
            Response.ContentType = "application/octet-stream";
            var dataPackXml = System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath("~/SendCityData.xml"));
            Response.Write(dataPackXml);
            Response.End();
        }

        protected void btnDownloadXmlForRouteRu_Click(object sender, EventArgs e)
        {
            Www123RouteMethods.GenerateXmlFile(hfIdList.Value, stbDeliveryDate1.Text);
            const string sGenName = "xml_для_route.ru.xml";
            Response.AddHeader("Content-disposition", "attachment; filename=" + sGenName);
            Response.ContentType = "application/octet-stream";
            var dataPackXml = System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath("~/SendData.xml"));
            Response.Write(dataPackXml);
            Response.End();
        }

        protected void btnReload_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.ServerVariables["URL"]);
        }

        protected void lvAllTickets_OnSortingButtonClick(object sender, EventArgs e)
        {
            var commandArgument = ((LinkButton)sender).CommandArgument;

            string sortDirection = "ASC";

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
            hfIdList.Value = String.Empty;
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                var id = row[2];
                hfIdList.Value += id + "-";
            }
            if (hfIdList.Value.Length > 1)
                hfIdList.Value = hfIdList.Value.Remove(hfIdList.Value.Length - 1, 1);
            lvAllTickets.DataBind();
            BindCityListView(ds);

            #region Редирект на первую страницу при поиске
            if (lvAllTickets.Items.Count == 0 && lvDataPager.TotalRowCount != 0)
            {
                lvDataPager.SetPageProperties(0, lvDataPager.PageSize, false);
                lvAllTickets.DataBind();
            }
            #endregion
        }

        private void BindCityListView(DataSet ds)
        {
            if (ds.Tables[0].Rows.Count < Convert.ToInt32(BackendHelper.TagToValue("city_list_visible_count_limit")))
            {
                //var listOfCity = new List<String>();
                var dsOfCity = new DataSet();
                dsOfCity.Tables.Add("Main");
                dsOfCity.Tables["Main"].Columns.Add("ID");
                dsOfCity.Tables["Main"].Columns.Add("CityID");
                dsOfCity.Tables["Main"].Columns.Add("DriverID");
                dsOfCity.Tables["Main"].Columns.Add("CityName");
                dsOfCity.Tables["Main"].Columns.Add("RegionName");
                dsOfCity.Tables["Main"].Columns.Add("DistrictName");
                dsOfCity.Tables["Main"].Columns.Add("TrackName");
                dsOfCity.Tables["Main"].Columns.Add("TicketsNumber");
                dsOfCity.Tables["Main"].Columns.Add("TicketsNumberWithoutPhoned");
                dsOfCity.Tables["Main"].Columns.Add("TicketIdList");
                dsOfCity.Tables["Main"].Columns.Add("OvdFrom");
                dsOfCity.Tables["Main"].Columns.Add("OvdTo");
                dsOfCity.Tables["Main"].Columns.Add("CityOrder", typeof(Int32));
                var keys = new DataColumn[1];
                keys[0] = dsOfCity.Tables["Main"].Columns["ID"];
                dsOfCity.Tables["Main"].PrimaryKey = keys;
                var dsId = 1;
                string prevDriverId = null;
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    var newRow = dsOfCity.Tables["Main"].NewRow();
                    bool contains = dsOfCity.Tables["Main"].AsEnumerable().Any(c => c.Field<string>("CityID").Equals(row["CityID"].ToString()));

                    #region Определяем видимость поля с сортировкой по очередности городов
                    if (String.IsNullOrEmpty(row["DriverID"].ToString()) || row["DriverID"].ToString() == "0")
                    {
                        IsVisibleCityOrder = false;
                        pnlCityOrder.Visible = false;
                    }
                    if (prevDriverId != null && prevDriverId != row["DriverID"].ToString())
                    {
                        IsVisibleCityOrder = false;
                        pnlCityOrder.Visible = false;
                    }
                    prevDriverId = row["DriverID"].ToString();
                    #endregion

                    if (!contains && row["CityID"].ToString() != "-1" && row["CityID"].ToString() != "0")
                    {
                        #region заполнение данными строки новой таблицы, если город содержится в выборке единожды
                        newRow["ID"] = dsId;
                        newRow["CityID"] = row["CityID"];
                        newRow["DriverID"] = row["DriverID"];
                        newRow["CityName"] = row["CityName"];
                        newRow["TicketIdList"] = row["ID"];
                        newRow["RegionName"] = CityHelper.RegionIDToRegionName(Convert.ToInt32(row["CityRegionID"]));
                        newRow["DistrictName"] =
                            CityHelper.DistrictIDToDistrictName(Convert.ToInt32(row["CityDistrictID"]));
                        newRow["TrackName"] = CityHelper.TrackIDToTrackName(Convert.ToInt32(row["CityTrackID"]));
                        newRow["TicketsNumber"] = 1;
                        newRow["OvdFrom"] = row["OvDateFrom"];
                        newRow["OvdTo"] = row["OvDateTo"];
                        if (row["Phoned"].ToString() == "1")
                            newRow["TicketsNumberWithoutPhoned"] = 0;
                        else
                            newRow["TicketsNumberWithoutPhoned"] = 1;
                        #endregion

                        dsOfCity.Tables["Main"].Rows.Add(newRow);
                        dsId++;
                    }
                    else
                    {
                        var currentRow = dsOfCity.Tables["Main"].AsEnumerable().FirstOrDefault(c => c.Field<string>("CityID").Equals(row["CityID"].ToString()));
                        if (currentRow == null) continue;
                        var index = dsOfCity.Tables["Main"].Rows.IndexOf(currentRow);
                        #region если город повторяется, то обновляем нужные значения в новой таблице в текущей строке
                        //обновление количества заявок в город
                        dsOfCity.Tables["Main"].Rows[index]["TicketsNumber"] = Convert.ToInt32(dsOfCity.Tables["Main"].Rows[index]["TicketsNumber"]) + 1;

                        //обновление TicketIdList
                        dsOfCity.Tables["Main"].Rows[index]["TicketIdList"] = dsOfCity.Tables["Main"].Rows[index]["TicketIdList"] + "-" + row["ID"];

                        //обновление количества заявок в город за исключением дозвонившихся
                        if (row["Phoned"].ToString() == "0")
                            dsOfCity.Tables["Main"].Rows[index]["TicketsNumberWithoutPhoned"] = Convert.ToInt32(dsOfCity.Tables["Main"].Rows[index]["TicketsNumberWithoutPhoned"]) + 1;
                        #endregion
                    }
                }

                #region Сортировка городов по json-строке из таблицы водителей
                if (IsVisibleCityOrder)
                {
                    var driver = new Drivers { ID = Convert.ToInt32(prevDriverId) };
                    driver.GetById();
                    if (driver.CityOrder != null && !String.IsNullOrEmpty(driver.CityOrder))
                    {
                        var js = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue };
                        var citiInOrder = js.Deserialize<List<CityInOrder>>(driver.CityOrder);
                        foreach (DataRow row in dsOfCity.Tables[0].Rows)
                        {
                            var cityID = row["CityID"];
                            var firstOrDefault = citiInOrder.FirstOrDefault(u => u.cityid == Convert.ToInt32(cityID));
                            if (firstOrDefault != null)
                            {
                                var orderValue = firstOrDefault.order;
                                row["CityOrder"] = orderValue;
                            }
                        }
                    }
                }
                #endregion

                //финальная сортировки datatable
                DataView dv = dsOfCity.Tables["Main"].DefaultView;
                dv.Sort = "CityOrder asc, TrackName asc, TicketsNumber desc, CityName asc";
                DataTable sortedDataTable = dv.ToTable();

                if (dsOfCity.Tables["Main"].Rows.Count != 0)
                {
                    lvAllCity.DataSource = sortedDataTable;
                    lvAllCity.DataBind();
                    lblCityCount.Text = String.Format("Всего городов в выборке: <b>{0}</b>", dsOfCity.Tables["Main"].Rows.Count);
                    pnlLegend.Visible = true;
                    var smsTicketLimitString = BackendHelper.TagToValue("sms_tickets_limit_for_visible");
                    var smsTicketLimit = 500;
                    try
                    {
                        smsTicketLimit = Convert.ToInt32(smsTicketLimitString);
                    }
                    catch (Exception)
                    {

                    }
                    if (ds.Tables[0].Rows.Count <= smsTicketLimit)
                    {
                        var userInSession = (Users)Session["userinsession"];
                        var rolesList = Application["RolesList"] as List<Roles>;
                        var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());
                        if (currentRole.ActionSendSmsBulk == 1)
                        {
                            pnlSendSms.Visible = true;
                            foreach (DataRow row in ds.Tables[0].Rows)
                            {
                                TickedIdListString += row["ID"] + ";";
                            }
                        }

                    }
                    hfCityIdList.Value = String.Empty;
                    #region Заполнение скрытого поля с ID городов
                    foreach (DataRow cityRow in dsOfCity.Tables[0].Rows)
                    {
                        hfCityIdList.Value += cityRow["CityID"] + ";";
                    }
                    if (hfCityIdList.Value.Length > 1)
                        hfCityIdList.Value = hfCityIdList.Value.Remove(hfCityIdList.Value.Length - 1, 1);

                    #endregion
                }
                else
                {
                    lblCityCount.Text = "Городов в выборке нет.";
                    pnlLegend.Visible = false;
                }
            }
            else
            {
                lblCityCount.Text = "Заявок в выборке больше " + BackendHelper.TagToValue("city_list_visible_count_limit") + ". <br/> Уменьшите выборку!";
                pnlLegend.Visible = false;
            }
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
            var seletedStatusString = String.Empty;
            var seletedTrackString = String.Empty;
            var searchUserIdString = String.Empty;
            var searchDriverIdString = String.Empty;
            var searchTicketIdString = String.Empty;
            var searchRecipientPhoneString = String.Empty;
            var searchDateString = String.Empty;
            var searchAdmissionDateString = String.Empty;
            var searchCostIsNull = String.Empty;
            var searchCheckedOut = String.Empty;
            var searchPhoned = String.Empty;
            var searchBilled = String.Empty;
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

            //формируем строку поика для водителей
            foreach (var selectedIndex in sddlDrivers.GetSelectedIndices())
            {
                if (!string.IsNullOrEmpty(sddlDrivers.Items[selectedIndex].Value))
                {
                    if (sddlDrivers.Items[selectedIndex].Value == "0")
                    {
                        searchDriverIdString = searchDriverIdString + "T.DriverID = '" +
                                               sddlDrivers.Items[selectedIndex].Value + "' OR T.DriverID = -1 OR ";
                    }
                    else
                    {
                        searchDriverIdString = searchDriverIdString + "T.DriverID = '" + sddlDrivers.Items[selectedIndex].Value + "' OR ";
                    }
                }
            }
            if (searchDriverIdString.Length > 3)
            {
                searchDriverIdString = "(" + searchDriverIdString.Remove(searchDriverIdString.Length - 3) + ")";
            }

            //формируем строку поика для статусов
            foreach (var selectedIndex in sddlStatus.GetSelectedIndices())
            {
                if (!string.IsNullOrEmpty(sddlStatus.Items[selectedIndex].Value))
                {
                    seletedStatusString = seletedStatusString + "T.StatusID = '" + sddlStatus.Items[selectedIndex].Value + "' OR ";
                }
            }
            if (seletedStatusString.Length > 3)
            {
                seletedStatusString = "(" + seletedStatusString.Remove(seletedStatusString.Length - 3) + ")";
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


            //формируем cтроку для поиска по дате приема
            if (!string.IsNullOrEmpty(stbAdmissionDate1.Text) && !string.IsNullOrEmpty(stbAdmissionDate2.Text))
            {
                searchAdmissionDateString = "(T.AdmissionDate BETWEEN '" +
                                   Convert.ToDateTime(stbAdmissionDate1.Text).ToString("yyyy-MM-dd") + "' AND '" +
                                   Convert.ToDateTime(stbAdmissionDate2.Text).ToString("yyyy-MM-dd") + "')";
            }
            else if (!string.IsNullOrEmpty(stbAdmissionDate1.Text) && string.IsNullOrEmpty(stbAdmissionDate2.Text))
            {
                searchAdmissionDateString = "(T.AdmissionDate BETWEEN '" +
                                Convert.ToDateTime(stbAdmissionDate1.Text).ToString("yyyy-MM-dd") + "' AND '" +
                                Convert.ToDateTime(stbAdmissionDate1.Text).AddYears(1).ToString("yyyy-MM-dd") + "')";
            }
            else if (string.IsNullOrEmpty(stbAdmissionDate1.Text) && !string.IsNullOrEmpty(stbAdmissionDate2.Text))
            {
                searchAdmissionDateString = "(T.AdmissionDate BETWEEN '" +
                                            Convert.ToDateTime(stbAdmissionDate2.Text)
                                                .AddYears(-2)
                                                .ToString("yyyy-MM-dd") + "' AND '" +
                                            Convert.ToDateTime(stbAdmissionDate2.Text).ToString("yyyy-MM-dd") + "')";
            }
            else
            {
                searchAdmissionDateString = "(T.AdmissionDate)";
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

            //формируем cтроку для поиска по CostIsNull
            if (cbGruzobozCostIsNull.Checked)
            {
                searchCostIsNull = "T.`GruzobozCost` = '0'";
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


            //формируем конечный запро для поиска
            searchParametres.Add("UserID", searchUserIdString);
            searchParametres.Add("CityID", seletedCityString);
            searchParametres.Add("DriverID", searchDriverIdString);
            searchParametres.Add("StatusID", seletedStatusString);
            searchParametres.Add("TrackID", seletedTrackString);
            searchParametres.Add("SecureID", searchTicketIdString);
            searchParametres.Add("RecipientPhone", searchRecipientPhoneString);
            searchParametres.Add("DeliveryDate", searchDateString);
            searchParametres.Add("AdmissionDate", searchAdmissionDateString);
            searchParametres.Add("GruzobozCost", searchCostIsNull);
            searchParametres.Add("CheckedOut", searchCheckedOut);
            searchParametres.Add("Phoned", searchPhoned);
            searchParametres.Add("Billed", searchBilled);

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
                                        "T.IsExchange, " +
                                        "T.ID, " +
                                        "T.SecureID, " +
                                        "T.FullSecureID, " +
                                        "T.UserID, " +
                                        "T.BoxesNumber, " +
                                        "T.CityID, " +
                                        "C.Name as `CityName`, " +
                                        "C.DistrictID as `CityDistrictID`, " +
                                        "C.RegionID as `CityRegionID`, " +
                                        "C.TrackID as `CityTrackID`, " +
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
                                        "T.Weight, " +
                                        "T.StatusID, " +
                                        "T.StatusDescription, " +
                                        "T.StatusIDOld, " +
                                        "T.UserProfileID, " +
                                        "T.Note, " +
                                        "T.Comment, " +
                                        "T.AdmissionDate, " +
                                        "T.DriverID, " +
                                        "T.CheckedOut, " +
                                        "T.Phoned, " +
                                        "T.Billed, " +
                                        "T.OvDateFrom, " +
                                        "T.AvailableOtherDocuments, " +
                                        "T.OvDateTo";
            const string cityNotInId = "T.CityID not in (select id from city where DistrictID in (10, 14, 2, 8, 16, 11, 3, 5, 19, 20, 21 ,1))";
            var overSearchString = searchString.Length < 4
                    ? String.Format("SELECT {1} FROM `tickets` as T JOIN `city` as C on T.CityID = C.ID WHERE {2} {0}", sortExpression, selectFields, cityNotInId)
                    : String.Format("SELECT {2} FROM `tickets` as T JOIN `city` as C on T.CityID = C.ID  WHERE {3} AND {0} {1}", searchString.Remove(searchString.Length - 4), sortExpression, selectFields, cityNotInId);

            DriverSearchString = searchString.Length < 4
                    ? String.Format("SELECT DISTINCT T.`DriverID` FROM `tickets` as T JOIN `city` as C on T.CityID = C.ID WHERE {0}", cityNotInId)
                    : String.Format("SELECT DISTINCT T.`DriverID` FROM `tickets` as T JOIN `city` as C on T.CityID = C.ID WHERE {1} AND {0}", searchString.Remove(searchString.Length - 4), cityNotInId);

            return overSearchString;
        }
        #endregion
    }
}