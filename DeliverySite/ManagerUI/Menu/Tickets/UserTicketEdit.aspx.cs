using Delivery.BLL.Filters;
using Delivery.BLL.Helpers;
using Delivery.BLL.StaticMethods;
using Delivery.DAL.DataBaseObjects;
using DeliverySite.Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Delivery.ManagerUI.Menu.Tickets
{
    public partial class UserTicketEdit : ManagerBasePage
    {
        #region Property

        protected string ButtonText { get; set; }

        protected string BackLink { get; set; }

        protected string AssessedCost { get; set; }

        public String AvailableTitles { get; set; }

        public String FullSecureID { get; set; }

        public Int32 GoodsCount { get; set; }

        public String UserID { get; set; }

        public Int32? SpecialClient { get; set; }

        protected bool IsVisibleUserProfileData { get; set; }

        protected bool IsVisibleUserAccountData { get; set; }
        public String AppKey { get; set; }

        public String FirstUserApiKey { get; set; }

        protected Roles Role { get; set; }
        #endregion

        protected void Page_Init(object sender, EventArgs e)
        {
            BackLink = OtherMethods.LinkBuilder(Page.Request.Params["sid"], Page.Request.Params["uid"],
                Page.Request.Params["recipientPhone"], Page.Request.Params["cityID"], Page.Request.Params["statusID"],
                Page.Request.Params["driverID"], Page.Request.Params["deliveryDate1"],
                Page.Request.Params["deliveryDate2"], Page.Request.Params["trackID"]);
            btnCreate.Click += bntCreate_Click;
            btnPrint.Click += bntPrint_Click;
            btnPrintZP.Click += bntPrintZP_Click;
            btnDelete.Click += btnDelete_Click;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = PagesTitles.ManagerUserTicketEditTitle + BackendHelper.TagToValue("page_title_part");
            OtherMethods.ActiveRightMenuStyleChanche("hlTickets", this.Page);
            Form.DefaultButton = btnCreate.UniqueID;
            AppKey = Globals.Settings.AppServiceSecureKey;
            FirstUserApiKey = Globals.Settings.FirstUserApiKey;

            #region Блок доступа к странице
            var userInSession = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = Role = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());
            if (currentRole.PageUserTicketEdit != 1)
            {
                Response.Redirect("~/Error.aspx?id=1");
            }
            #endregion

            #region Блок доступа к данным на странице
            IsVisibleUserProfileData = true;
            IsVisibleUserAccountData = true;

            if (currentRole.PageUserProfileView == 0)
            {
                IsVisibleUserProfileData = false;
            }

            if (currentRole.PageClientsView == 0)
            {
                IsVisibleUserAccountData = false;
            }

            trUserProfileData.Visible = IsVisibleUserProfileData;
            trUserAccountData.Visible = IsVisibleUserAccountData;
            #endregion

            #region Редирект на страницу всех заявок если заявки нет
            if (Page.Request.Params["id"] == null || Page.Request.Params["id"] == String.Empty)
                Page.Response.Redirect("~/ManagerUI/Menu/Tickets/UserTicketView.aspx");
            #endregion

            if (!IsPostBack)
            {
                #region Загрузка tickets по SecureID или FullSecureID. Метод на очистку.
                DAL.DataBaseObjects.Tickets ticket;
                if (Page.Request.Params["id"].Length > 7)
                {
                    ticket = new DAL.DataBaseObjects.Tickets { FullSecureID = Page.Request.Params["id"] };
                    ticket.GetByFullSecureId();
                }
                else
                {
                    ticket = new DAL.DataBaseObjects.Tickets { SecureID = Page.Request.Params["id"] };
                    ticket.GetBySecureId();
                }
                #endregion

                #region Создание форм для груза
                GoodsCount = GoodsHelper.GoodsCount(ticket.FullSecureID);
                hfHowManyControls.Value = GoodsCount.ToString();
                AddGodsInPanel(GoodsCount);
                #endregion

                #region Заполнение созданных формы
                var goods = new Goods { TicketFullSecureID = ticket.FullSecureID };
                var ds = goods.GetAllItems("ID", "ASC", "TicketFullSecureID");
                var goodsIterator = 1;
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    var tbGoodsDescription = (TextBox)pnlBooks.FindControl("tbGoodsDescription" + goodsIterator);
                    var tbGoodsModel = (TextBox)pnlBooks.FindControl("tbGoodsModel" + goodsIterator);
                    var tbGoodsNumber = (TextBox)pnlBooks.FindControl("tbGoodsNumber" + goodsIterator);
                    var tbGoodsCost = (TextBox)pnlBooks.FindControl("tbGoodsCost" + goodsIterator);
                    var hfGoodsID = (HiddenField)pnlBooks.FindControl("hfGoodsID" + goodsIterator);
                    var hfWithoutAkciza = (HiddenField)pnlBooks.FindControl("hfWithoutAkciza" + goodsIterator);

                    var hfGoodsNumber = (HiddenField)pnlBooks.FindControl("hfGoodsNumber" + goodsIterator);
                    var hfGoodsCost = (HiddenField)pnlBooks.FindControl("hfGoodsCost" + goodsIterator);
                    var hfGoodsDescription = (HiddenField)pnlBooks.FindControl("hfGoodsDescription" + goodsIterator);
                    var hfGoodsModel = (HiddenField)pnlBooks.FindControl("hfGoodsModel" + goodsIterator);

                    tbGoodsDescription.Text = hfGoodsDescription.Value = row["Description"].ToString();
                    tbGoodsModel.Text = hfGoodsModel.Value = row["Model"].ToString();
                    tbGoodsNumber.Text = hfGoodsNumber.Value = row["Number"].ToString();
                    tbGoodsCost.Text = hfGoodsCost.Value = MoneyMethods.MoneySeparator(row["Cost"].ToString());
                    hfGoodsID.Value = row["ID"].ToString();
                    hfWithoutAkciza.Value = row["WithoutAkciza"].ToString();
                    goodsIterator++;
                }
                #endregion

                #region Вывод старых грузов. Метод на очистку.
                if (GoodsCount == 0)
                {
                    lblOldGoods.Visible = true;
                    lblOldGoods.Text = "<b>" + OtherMethods.GoodsStringFromTicketID(ticket.ID.ToString()) + "</b><br/>";
                }
                #endregion

                #region Инициализация сущностей
                var user = new Users();
                user.ID = Convert.ToInt32(ticket.UserID);
                user.GetById();

                #region Блок конфигурации удаления заявки
                if (currentRole.ActionUserTicketDelete != 1)
                {
                    btnDelete.Visible = false;
                }
                #endregion

                var profile = new UsersProfiles();
                profile.ID = Convert.ToInt32(ticket.UserProfileID);
                profile.GetById();
                #endregion

                #region блок общей информации

                SpecialClient = user.SpecialClient;

                hfID.Value = ticket.ID.ToString();
                hfDriverID.Value = ticket.DriverID.ToString();
                hfStatusID.Value = ticket.StatusID.ToString();
                hfStatusIDOld.Value = ticket.StatusIDOld.ToString();
                hfStatusDescription.Value = ticket.StatusDescription;
                hfAdmissionDate.Value = ticket.AdmissionDate.ToString();

                hfUserID.Value = UserID = ticket.UserID.ToString();
                hfUserDiscount.Value = user.Discount.ToString();
                hfUserProfileType.Value = profile.TypeID.ToString();
                hfFullSecureID.Value = ticket.FullSecureID;
                lblID.Text = ticket.SecureID;
                hlUser.Text = user.Family + ' ' + user.Name;
                hlUser.NavigateUrl = "~/ManagerUI/Menu/Souls/ClientEdit.aspx?id=" + ticket.UserID;
                hlProfile.Text = profile.TypeID == 1 ? (profile.FirstName + ' ' + profile.LastName) : profile.CompanyName;
                hlProfile.NavigateUrl = "~/ManagerUI/Menu/Souls/ProfileView.aspx?id=" + ticket.UserProfileID;
                lblProfileType.Text = UsersProfilesHelper.UserProfileTypeToText(Convert.ToInt32(profile.TypeID));
                lblCreateDate.Text = OtherMethods.DateConvert(ticket.CreateDate.ToString());
                lblAdmissionDate.Text = OtherMethods.DateConvert(ticket.AdmissionDate.ToString());
                lblDeliveryDateStatic.Text = OtherMethods.DateConvert(ticket.DeliveryDate.ToString());
                lblIsExchange.Text = ticket.IsExchange == 0 ? "нет" : "да";
                cbWithoutMoney.Checked = ticket.WithoutMoney != 0;
                lblNN.Text = ticket.PrintNaklInMap == 0 ? "нет" : "да";
                lblPN.Text = ticket.PrintNakl == 0 ? "нет" : "да";
                if (String.IsNullOrEmpty(ticket.Comment))
                {
                    tdComment.Visible = false;
                }
                else
                {
                    lblComment.Text = WebUtility.HtmlDecode(ticket.Comment);
                }
                var regionText = CityHelper.CityToTrack(Convert.ToInt32(ticket.CityID), ticket.ID.ToString());
                if (ticket.TrackIDUser != 0 || regionText == "Не задано")
                {
                    ddlUserTrack.Visible = true;
                    lblTrack.Visible = false;

                    var region = new Tracks();
                    ddlUserTrack.DataSource = region.GetAllItems();
                    ddlUserTrack.DataTextField = "Name";
                    ddlUserTrack.DataValueField = "ID";
                    ddlUserTrack.DataBind();
                    ddlUserTrack.Items.Insert(0, new ListItem("Не задано", "0"));
                    ddlUserTrack.SelectedValue = Convert.ToString(ticket.TrackIDUser);
                }
                else
                {
                    lblTrack.Text = regionText;
                }

                if (string.IsNullOrEmpty(lblAdmissionDate.Text)) lblAdmissionDate.Text = "Груз пока не на складе";
                #endregion

                #region блок информации, заполняемой пользователем
                AssessedCost = MoneyMethods.MoneySeparator(ticket.AssessedCost.ToString());
                hfAssessedCost.Value = ticket.AssessedCost.ToString();
                lblAssessedCost.Text = MoneyMethods.MoneySeparator(ticket.AssessedCost.ToString());

                tbDeliveryCost.Text = MoneyMethods.MoneySeparator(ticket.DeliveryCost.ToString());
                ddlRecipientStreetPrefix.SelectedValue = ticket.RecipientStreetPrefix;
                tbRecipientStreet.Text = ticket.RecipientStreet;
                tbRecipientStreetNumber.Text = ticket.RecipientStreetNumber;
                tbRecipientKorpus.Text = ticket.RecipientKorpus;
                tbRecipientKvartira.Text = ticket.RecipientKvartira;
                tbRecipientPhone.Text = ticket.RecipientPhone;
                tbRecipientPhone2.Text = ticket.RecipientPhoneTwo;
                ddlSenderStreetPrefix.SelectedValue = ticket.SenderStreetPrefix;
                tbSenderStreetName.Text = ticket.SenderStreetName;
                tbSenderStreetNumber.Text = ticket.SenderStreetNumber;
                tbSenderHousing.Text = ticket.SenderHousing;
                tbSenderApartmentNumber.Text = ticket.SenderApartmentNumber;
                tbNote.Text = ticket.Note;
                tbBoxesNumber.Text = string.IsNullOrEmpty(ticket.BoxesNumber.ToString()) ? "1" : ticket.BoxesNumber.ToString();
                tbDeliveryDate.Text = Convert.ToDateTime(ticket.DeliveryDate).ToString("dd-MM-yyyy");
                tbRecipientFirstName.Text = ticket.RecipientFirstName;
                tbRecipientLastName.Text = ticket.RecipientLastName;
                tbRecipientThirdName.Text = ticket.RecipientThirdName;
                tbTtnSeria.Text = ticket.TtnSeria;
                tbTtnNumber.Text = ticket.TtnNumber;
                tbOtherDocuments.Text = ticket.OtherDocuments;
                tbPassportNumber.Text = ticket.PassportNumber;
                tbPassportSeria.Text = ticket.PassportSeria;

                if (!String.IsNullOrEmpty(ticket.DeliveryCost.ToString()) && ticket.DeliveryCost != 0)
                {
                    cbIsDeliveryCost.Checked = true;
                }

                //автокомплит наименования
                var titles = new Titles();
                var availableTitles = titles.GetAllItems("Name", "ASC", null).Tables[0].Rows.Cast<DataRow>().Aggregate(String.Empty, (current, items) => current + ("\"" + items["Name"] + "\","));
                AvailableTitles = availableTitles.Remove(availableTitles.Length - 1);
                #endregion

                #region Заполнение города
                var allCity = Application["CityList"] as List<City>;
                if (allCity != null)
                {
                    tbCity.Text = CityHelper.CityIDToAutocompleteString(allCity.FirstOrDefault(u => u.ID == ticket.CityID));
                    hfCityID.Value = ticket.CityID.ToString();

                    var senderCity = allCity.First(u => u.ID == ticket.SenderCityID);
                    tbSenderCity.Text = CityHelper.CityIDToAutocompleteString(senderCity);
                    hfSenderCityID.Value = ticket.SenderCityID.ToString();
                }
                #endregion 

                hfWharehouse.Value = ticket.WharehouseId.ToString();

                #region блок информации, заполняемой менеджером
                ddlStatus.DataSource = DAL.DataBaseObjects.Tickets.TicketStatuses;
                ddlStatus.DataTextField = "Value";
                ddlStatus.DataValueField = "Key";
                ddlStatus.DataBind();
                ddlStatus.SelectedValue = Convert.ToString(ticket.StatusID);

                var ddrivers = new Drivers { StatusID = 1 };
                var dataSet5 = ddrivers.GetAllActivatedDrivers();
                dataSet5.Tables[0].Columns.Add("FIOAndID", typeof(string), "'(' + ID + ') ' + FirstName + ' ' + SUBSTRING(LastName,1,1) + '.' +  SUBSTRING(ThirdName,1,1) + '.'");
                ddlDrivers.DataSource = dataSet5;
                ddlDrivers.DataTextField = "FIOAndID";
                ddlDrivers.DataValueField = "ID";
                ddlDrivers.DataBind();
                ddlDrivers.Items.Insert(0, new ListItem("Водитель не назначен", "0"));

                tbAgreedCost.Text = MoneyMethods.MoneySeparator(ticket.AgreedCost.ToString());
                tbGruzobozCost.Text = MoneyMethods.MoneySeparator(ticket.GruzobozCost.ToString());

                #region Блок блокировки полей
                if (currentRole.ActionControlGruzobozCost != 1)
                {
                    tbGruzobozCost.Enabled = false;
                }
                if (currentRole.ActionStatusAdd != 1)
                {
                    ddlStatus.Enabled = false;
                    tbDeliveryDate.Enabled = false;
                    tbStatusDescription.Enabled = false;
                }
                if (currentRole.ActionDriverAdd != 1)
                {
                    ddlDrivers.Enabled = false;
                }
                if (currentRole.ActionAllowChangeMoneyAndCourse != 1)
                {
                    tbAgreedCost.Enabled = false;
                    tbGruzobozCost.Enabled = false;
                }
                #endregion

                //если id водителя 0 или -1 - водитель не назначен
                if (ticket.DriverID != 0 && ticket.DriverID != -1)
                {
                    ddlDrivers.SelectedValue = ticket.DriverID.ToString();
                }
                else
                {
                    ddlDrivers.SelectedValue = "0";
                }

                //если статусы На складе (перенесен), Отказ (у курьера), Возврат (на складе), Отмена (на складе), Отмена то показываем почему
                if (ddlStatus.SelectedValue == "7" || ddlStatus.SelectedValue == "8" || ddlStatus.SelectedValue == "4" || ddlStatus.SelectedValue == "9" || ddlStatus.SelectedValue == "10")
                {
                    lblStatusDescription.Visible = true;
                    tbStatusDescription.Visible = true;
                    tbStatusDescription.Text = ticket.StatusDescription;
                }
                #endregion

                #region Ограничения на правку текстбоксов
                //если админ или статусы "Не обработана" или "На складе" или "На складе (перенесено)", или "Отмена" или "Отмена (на складе)" - можно изменять все поля пользователя
                if (currentRole.ActionDisallowEditSomeFieldInTickets != 1
                    && (ddlStatus.SelectedValue == "1" || ddlStatus.SelectedValue == "2" || ddlStatus.SelectedValue == "4" || ddlStatus.SelectedValue == "9" || ddlStatus.SelectedValue == "10")
                    && currentRole.ActionDisallowTicketChangeWithoutManagerInfo != 1)
                {
                    tbCity.Enabled =
                    tbRecipientStreetNumber.Enabled =
                    tbRecipientKorpus.Enabled =
                    tbRecipientFirstName.Enabled =
                    tbRecipientLastName.Enabled =
                    tbRecipientThirdName.Enabled =
                    tbRecipientKvartira.Enabled =
                    tbRecipientPhone.Enabled =
                    tbBoxesNumber.Enabled =
                    tbDeliveryDate.Enabled =
                    tbOtherDocuments.Enabled =
                    tbTtnNumber.Enabled =
                    tbTtnSeria.Enabled =
                    tbPassportNumber.Enabled =
                    tbPassportSeria.Enabled =
                    tbRecipientPhone2.Enabled =
                    tbRecipientStreet.Enabled =
                    tbNote.Enabled = true;
                    for (var i = 1; i <= GoodsCount; i++)
                    {
                        var tbGoodsDescription = (TextBox)pnlBooks.FindControl("tbGoodsDescription" + i);
                        var tbGoodsModel = (TextBox)pnlBooks.FindControl("tbGoodsModel" + i);
                        var tbGoodsNumber = (TextBox)pnlBooks.FindControl("tbGoodsNumber" + i);
                        tbGoodsDescription.Enabled = true;
                        tbGoodsModel.Enabled = true;
                        tbGoodsNumber.Enabled = true;

                    }
                }

                if (currentRole.ActionDisallowTicketChangeWithoutManagerInfo == 1)
                {
                    ddlUserTrack.Enabled = false;
                    tbRecipientFirstName.Enabled = false;
                    tbRecipientLastName.Enabled = false;
                    tbRecipientThirdName.Enabled = false;
                }

                //только админ может изменять курсы
                if (currentRole.ActionAllowChangeMoneyAndCourse == 1 && currentRole.ActionDisallowTicketChangeWithoutManagerInfo != 1)
                {
                    cbIsDeliveryCost.Enabled =
                    tbDeliveryCost.Enabled = true;

                    for (var i = 1; i <= GoodsCount; i++)
                    {
                        var tbGoodsCost = (TextBox)pnlBooks.FindControl("tbGoodsCost" + i);
                        tbGoodsCost.Enabled = true;
                    }
                }
                #endregion
            }

            if (!String.IsNullOrEmpty(hfCityID.Value))
            {
                var coefficientDeviationCost = Convert.ToDouble(BackendHelper.TagToValue("coefficient_deviation_cost"));
                var city = new City { ID = Convert.ToInt32(hfCityID.Value) };
                city.GetById();
                var district = new Districts { ID = Convert.ToInt32(city.DistrictID) };
                district.GetById();
                lblCityCost.Text = MoneyMethods.MoneySeparatorForCityTableView((city.DistanceFromCity * Convert.ToDecimal(coefficientDeviationCost)).ToString());
                lblCityDeliveryDate.Text =
                    DistrictsHelper.DeliveryDateStringToRuss(DistrictsHelper.DeliveryDateString(city.DistrictID));
                lblCityDeliveryTerms.Text =
                    DistrictsHelper.DeliveryTermsToRuss(DistrictsHelper.DeliveryTerms(city.DistrictID));
            }

        }

        public void bntPrint_Click(Object sender, EventArgs e)
        {
            Page.Response.Redirect("~/PrintServices/PrintTickets.aspx?id=" + hfID.Value);
        }

        public void bntPrintZP_Click(Object sender, EventArgs e)
        {
            Page.Response.Redirect("~/PrintServices/PrintZP.aspx?id=" + hfID.Value);
        }

        public void btnDelete_Click(Object sender, EventArgs e)
        {
            var userInSession = (Users)Session["userinsession"];

            #region Блок доступа к методу
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());
            if (currentRole.ActionUserTicketDelete != 1)
            {
                Response.Redirect("~/Error.aspx?id=1");
            }
            #endregion

            var ticket = new DAL.DataBaseObjects.Tickets { ID = Convert.ToInt32(hfID.Value) };
            ticket.GetById();
            ticket.Delete(Convert.ToInt32(hfID.Value), Convert.ToInt32(userInSession.ID), OtherMethods.GetIPAddress(), "UserTicketEdit",
                ticket.FullSecureID);
            Session["flash:now"] = String.Format("<span style='color: red;'>заявка {0} удалена</span>", hfID.Value);
            FinalyRedirect();
        }

        public void bntCreate_Click(Object sender, EventArgs e)
        {
            #region Инициализация объектов
            var user = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == user.Role.ToLower());

            var withoutMoney = Convert.ToInt32(cbWithoutMoney.Checked);

            var tbAgreedCostValue = 0.00;
            double.TryParse(tbAgreedCost.Text, out tbAgreedCostValue);

            var tbGruzobozCostValue = 0.00;
            double.TryParse(tbGruzobozCost.Text, out tbGruzobozCostValue);

            var tbDeliveryCostValue = 0.00;
            double.TryParse(tbDeliveryCost.Text, out tbDeliveryCostValue);

            var ticket = new DAL.DataBaseObjects.Tickets
            {
                ID = Convert.ToInt32(hfID.Value),
                ChangeDate = DateTime.Now,
                DeliveryDate = Convert.ToDateTime(tbDeliveryDate.Text),
                AgreedCost = Convert.ToDecimal(tbAgreedCostValue),
                Note = tbNote.Text,
                RecipientStreetPrefix = ddlRecipientStreetPrefix.SelectedValue,
                RecipientStreet = TicketsHelper.RecipientStreetCleaner(tbRecipientStreet.Text),
                RecipientStreetNumber = tbRecipientStreetNumber.Text,
                RecipientKorpus = tbRecipientKorpus.Text,
                RecipientKvartira = tbRecipientKvartira.Text,
                BoxesNumber = Convert.ToInt32(tbBoxesNumber.Text),
                RecipientFirstName = tbRecipientFirstName.Text,
                RecipientLastName = tbRecipientLastName.Text,
                RecipientThirdName = tbRecipientThirdName.Text,
                TtnNumber = tbTtnNumber.Text,
                TtnSeria = tbTtnSeria.Text,
                OtherDocuments = tbOtherDocuments.Text,
                RecipientPhone = tbRecipientPhone.Text,
                RecipientPhoneTwo = tbRecipientPhone2.Text,
                PassportNumber = tbPassportNumber.Text,
                PassportSeria = tbPassportSeria.Text,
                DeliveryCost = cbIsDeliveryCost.Checked ? Convert.ToDecimal(tbDeliveryCostValue) : 0,
                CityID = Convert.ToInt32(hfCityID.Value),
                SenderCityID = Convert.ToInt32(hfSenderCityID.Value),
                SenderStreetName = tbSenderStreetName.Text,
                SenderStreetNumber = tbSenderStreetNumber.Text,
                SenderStreetPrefix = ddlSenderStreetPrefix.SelectedValue,
                SenderHousing = tbSenderHousing.Text,
                SenderApartmentNumber = tbSenderApartmentNumber.Text,
                WithoutMoney = withoutMoney,
                NoteChanged = !string.IsNullOrEmpty(Page.Request.Params["id"]) ? 1 : 0
            };

            Delivery.DAL.DataBaseObjects.Tickets checkTicket = new Delivery.DAL.DataBaseObjects.Tickets();
            checkTicket.ID = ticket.ID;
            checkTicket.GetById();
            if (new UserCost().GetCostByUserID(checkTicket.UserID) == null || Convert.ToDecimal(tbGruzobozCostValue) == 0)
                ticket.GruzobozCost = Convert.ToDecimal(tbGruzobozCostValue);

            //метод на удаление (переход со старой системы заявок)
            if (!lblOldGoods.Visible)
            {
                ticket.AssessedCost = GetAssessedCost();
            }

            #endregion

            if (ddlUserTrack.Visible)
            {
                ticket.TrackIDUser = Convert.ToInt32(ddlUserTrack.SelectedValue);
            }

            #region Обнуление соглассованной стоимости и за доставку при соответствующих условиях
            if (cbAgreedCostIsNull.Checked)
            {
                ticket.AgreedCost = 0;
            }

            if (cbDeliveryCostIsNull.Checked)
            {
                ticket.DeliveryCost = 0;
            }
            #endregion

            #region Запись города
            ticket.CityID = Convert.ToInt32(hfCityID.Value);
            #endregion

            #region Проверки на валидность, фильтрация статуса и курьера
            if (lblAdmissionDate.Text == String.Empty)
            {
                AddGodsInPanel(Convert.ToInt32(hfHowManyControls.Value));
                lblError.Text = "Введите дату!";
                return;
            }

            if (String.IsNullOrEmpty(hfCityID.Value))
            {
                AddGodsInPanel(Convert.ToInt32(hfHowManyControls.Value));
                lblError.Text = "Выберите населенный пункт!";
                return;
            }

            var errorText = TicketsFilter.OverChangeFilter(ref ticket, hfDriverID.Value, hfStatusID.Value, hfStatusDescription.Value, hfAdmissionDate.Value, tbStatusDescription.Text, ddlStatus.SelectedValue, tbDeliveryDate.Text, ddlDrivers.SelectedValue, hfStatusIDOld.Value, currentRole);
            if (errorText != null)
            {
                AddGodsInPanel(Convert.ToInt32(hfHowManyControls.Value));
                lblError.Text = errorText;
                return;
            }
            #endregion

            if (hfWharehouse.Value != "")
            {
                int whVal = 0;
                if (Int32.TryParse(hfWharehouse.Value, out whVal))
                    ticket.WharehouseId = whVal;
            }

            ticket.Update(user.ID, OtherMethods.GetIPAddress(), "UserTicketEdit");
            GoodsCount = GoodsHelper.GoodsCount(hfFullSecureID.Value);
            UpdateGoods(GoodsCount, user.ID);

            FinalyRedirect();
        }

        #region GoodsMethods

        private void AddGodsInPanel(int count)
        {
            if (count == 0) { return; }

            for (int i = 1; i <= count; i++)
            {
                var tbGoodsDescription = new TextBox();
                var cvGoodsDescription = new CustomValidator();
                var tbGoodsModel = new TextBox();
                var cvGoodsModel = new CustomValidator();
                var tbGoodsCost = new TextBox();
                var cvGoodsCost = new CustomValidator();
                var tbGoodsNumber = new TextBox();
                var cvGoodsNumber = new CustomValidator();
                var hfGoodsID = new HiddenField();
                var hfWithoutAkciza = new HiddenField();

                var hfGoodsCost = new HiddenField();
                var hfGoodsNumber = new HiddenField();
                var hfGoodsDescription = new HiddenField();
                var hfGoodsModel = new HiddenField();

                tbGoodsDescription.ID = "tbGoodsDescription" + i.ToString();
                tbGoodsDescription.Width = new Unit(90, UnitType.Percentage);
                tbGoodsDescription.Enabled = false;
                tbGoodsDescription.Attributes.Add("onkeyup", "counterDescription(\"" + i.ToString() + "\");");
                tbGoodsDescription.CssClass += " form-control";

                cvGoodsDescription.ID = "cvGoodsDescription" + i.ToString();
                cvGoodsDescription.ValidationGroup = "LoginGroup";
                cvGoodsDescription.ControlToValidate = "tbGoodsDescription" + i.ToString();
                cvGoodsDescription.ClientValidationFunction = "validateIfEmpty";
                cvGoodsDescription.EnableClientScript = true;
                cvGoodsDescription.ValidateEmptyText = true;
                cvGoodsDescription.Display = ValidatorDisplay.None;
                cvGoodsDescription.ErrorMessage = "Вы не ввели наименование груза #" + i.ToString();



                tbGoodsModel.ID = "tbGoodsModel" + i.ToString();
                tbGoodsModel.Width = new Unit(90, UnitType.Percentage);
                tbGoodsModel.Enabled = false;
                tbGoodsModel.Attributes.Add("onkeyup", "counterModel(\"" + i.ToString() + "\");");
                tbGoodsModel.CssClass += " form-control";

                cvGoodsModel.ID = "cvGoodsModel" + i.ToString();
                cvGoodsModel.ValidationGroup = "LoginGroup";
                cvGoodsModel.ControlToValidate = "tbGoodsModel" + i.ToString();
                cvGoodsModel.ClientValidationFunction = "validateIfEmpty";
                cvGoodsModel.EnableClientScript = true;
                cvGoodsModel.ValidateEmptyText = true;
                cvGoodsModel.Display = ValidatorDisplay.None;
                cvGoodsModel.ErrorMessage = "Вы не ввели модель груза #" + i.ToString();




                tbGoodsCost.ID = "tbGoodsCost" + i.ToString();
                tbGoodsCost.Width = new Unit(25, UnitType.Percentage);
                tbGoodsCost.Enabled = false;
                tbGoodsCost.Attributes.Add("onkeyup", "overAssessedCost(\"" + count.ToString() + "\");");
                tbGoodsCost.CssClass = "moneyMask";
                tbGoodsCost.CssClass += " form-control";

                cvGoodsCost.ID = "cvGoodsCost" + i.ToString();
                cvGoodsCost.ValidationGroup = "LoginGroup";
                cvGoodsCost.ControlToValidate = "tbGoodsCost" + i.ToString();
                cvGoodsCost.ClientValidationFunction = "validateNotEmptyNumber";
                cvGoodsCost.EnableClientScript = true;
                cvGoodsCost.ValidateEmptyText = true;
                cvGoodsCost.Display = ValidatorDisplay.None;
                cvGoodsCost.ErrorMessage = "Вы не ввели модель груза #" + i.ToString();




                tbGoodsNumber.ID = "tbGoodsNumber" + i.ToString();
                tbGoodsNumber.Width = new Unit(30, UnitType.Pixel);
                tbGoodsNumber.Enabled = false;
                tbGoodsNumber.Attributes.Add("onkeyup", "overAssessedCost(\"" + count.ToString() + "\");");
                tbGoodsNumber.CssClass += " form-control";

                cvGoodsNumber.ID = "cvGoodsNumber" + i.ToString();
                cvGoodsNumber.ValidationGroup = "LoginGroup";
                cvGoodsNumber.ControlToValidate = "tbGoodsNumber" + i.ToString();
                cvGoodsNumber.ClientValidationFunction = "validateNotEmptyNumber";
                cvGoodsNumber.EnableClientScript = true;
                cvGoodsNumber.ValidateEmptyText = true;
                cvGoodsNumber.Display = ValidatorDisplay.None;
                cvGoodsNumber.ErrorMessage = "Вы не ввели модель груза #" + i.ToString();

                hfGoodsID.ID = "hfGoodsID" + i.ToString();
                hfWithoutAkciza.ID = "hfWithoutAkciza" + i.ToString();

                hfGoodsCost.ID = "hfGoodsCost" + i.ToString();
                hfGoodsNumber.ID = "hfGoodsNumber" + i.ToString();
                hfGoodsDescription.ID = "hfGoodsDescription" + i.ToString();
                hfGoodsModel.ID = "hfGoodsModel" + i.ToString();

                if (IsPostBack)
                {
                    //dвосстанавливаем старые значения полей, так как они перерисовываются (поля)
                    tbGoodsDescription.Text = hfGoodsDescription.Value =
                        !String.IsNullOrEmpty(Page.Request.Params["ctl00$MainContent$tbGoodsDescription" + i.ToString()]) ? Page.Request.Params["ctl00$MainContent$tbGoodsDescription" + i.ToString()] : Page.Request.Params["ctl00$MainContent$hfGoodsDescription" + i.ToString()];
                    tbGoodsModel.Text = hfGoodsModel.Value =
                        !String.IsNullOrEmpty(Page.Request.Params["ctl00$MainContent$tbGoodsModel" + i.ToString()]) ? Page.Request.Params["ctl00$MainContent$tbGoodsModel" + i.ToString()] : Page.Request.Params["ctl00$MainContent$hfGoodsModel" + i.ToString()];
                    tbGoodsCost.Text = hfGoodsCost.Value =
                        !String.IsNullOrEmpty(Page.Request.Params["ctl00$MainContent$tbGoodsCost" + i.ToString()]) ? Page.Request.Params["ctl00$MainContent$tbGoodsCost" + i.ToString()] : Page.Request.Params["ctl00$MainContent$hfGoodsCost" + i.ToString()];
                    tbGoodsNumber.Text = hfGoodsNumber.Value =
                        !String.IsNullOrEmpty(Page.Request.Params["ctl00$MainContent$tbGoodsNumber" + i.ToString()]) ? Page.Request.Params["ctl00$MainContent$tbGoodsNumber" + i.ToString()] : Page.Request.Params["ctl00$MainContent$hfGoodsNumber" + i.ToString()];
                    hfGoodsID.Value =
                        Page.Request.Params["ctl00$MainContent$hfGoodsID" + i.ToString()];
                }

                var jqueruAutocomplite = "" +
                "<script type=\"text/javascript\">" +
                    "$(function() {" +
                        "$(\"#ctl00_MainContent_tbGoodsDescription" + i.ToString() + "\").autocomplete({" +
                            "lookup: availableTitles," +
                            "onSelect: function (suggestion) { counterDescription(" + i.ToString() + "); }" +
                        "});" +
                    "});" +
                "</script>";
                const string string1 = "<table style=\"width: 95%;\" id=\"good2\" class=\"goodsTable\">" +
                              "<tr>" +
                              "<td style=\"width: 110px; vertical-align: middle; padding-bottom: 15px;\">" +
                              "наименование:" +
                              "</td>" +
                              "<td style=\"vertical-align: middle;\">";

                var string2 = "<span id=\"counter" + i.ToString() + "\"></span>" +
                              "</td>" +
                              "</tr>" +
                              "<tr>" +
                              "<td style=\"width: 110px; vertical-align: middle; padding-bottom: 15px;\">" +
                              "марка/модель:" +
                              "</td>" +
                              "<td style=\"vertical-align: middle;\">";

                const string string3 = "</td>" +
                              "</tr>" +
                              "<tr>" +
                              "<td style=\"width: 110px; vertical-align: middle;\">" +
                              "стоимость <span style=\"font-size: 9px\">(за 1 шт)</span>:" +
                              "</td>" +
                              "<td style=\"vertical-align: middle;\">";

                const string string4 = " руб." +
                                       "&nbsp; штук: ";

                const string string5 = "</td>" +
                              "</tr>" +
                              "</table>";

                pnlBooks.Controls.Add(new LiteralControl(jqueruAutocomplite));
                pnlBooks.Controls.Add(new LiteralControl(string1));
                pnlBooks.Controls.Add(tbGoodsDescription);
                pnlBooks.Controls.Add(new LiteralControl(string2));
                pnlBooks.Controls.Add(tbGoodsModel);
                pnlBooks.Controls.Add(new LiteralControl(string3));
                pnlBooks.Controls.Add(tbGoodsCost);
                pnlBooks.Controls.Add(new LiteralControl(string4));
                pnlBooks.Controls.Add(tbGoodsNumber);
                pnlBooks.Controls.Add(new LiteralControl(string5));
                pnlBooks.Controls.Add(hfGoodsID);
                pnlBooks.Controls.Add(hfWithoutAkciza);
                pnlBooks.Controls.Add(hfGoodsCost);
                pnlBooks.Controls.Add(hfGoodsNumber);
                pnlBooks.Controls.Add(hfGoodsDescription);
                pnlBooks.Controls.Add(hfGoodsModel);
                pnlBooks.Controls.Add(cvGoodsDescription);
                pnlBooks.Controls.Add(cvGoodsModel);
                pnlBooks.Controls.Add(cvGoodsCost);
                pnlBooks.Controls.Add(cvGoodsNumber);

            }
        }

        protected Decimal? GetAssessedCost()
        {
            Decimal? result = 0;
            for (int i = 1; i <= Convert.ToInt32(hfHowManyControls.Value); i++)
            {
                var goodsCost = !String.IsNullOrEmpty(Page.Request.Params["ctl00$MainContent$tbGoodsCost" + i.ToString()]) ? Page.Request.Params["ctl00$MainContent$tbGoodsCost" + i.ToString()] : Page.Request.Params["ctl00$MainContent$hfGoodsCost" + i.ToString()];
                var goodsNumber = !String.IsNullOrEmpty(Page.Request.Params["ctl00$MainContent$tbGoodsNumber" + i.ToString()]) ? Page.Request.Params["ctl00$MainContent$tbGoodsNumber" + i.ToString()] : Page.Request.Params["ctl00$MainContent$hfGoodsNumber" + i.ToString()];

                result += Convert.ToDecimal(goodsCost) * Convert.ToInt32(goodsNumber.Replace(" ", ""));
            }
            return result;
        }

        protected void UpdateGoods(int count, int userId)
        {
            for (int i = 1; i <= count; i++)
            {
                var goodsDescription = !String.IsNullOrEmpty(Page.Request.Params["ctl00$MainContent$tbGoodsDescription" + i.ToString()]) ? Page.Request.Params["ctl00$MainContent$tbGoodsDescription" + i.ToString()] : Page.Request.Params["ctl00$MainContent$hfGoodsDescription" + i.ToString()];
                if (!String.IsNullOrEmpty(goodsDescription))
                {
                    var goodsModel = !String.IsNullOrEmpty(Page.Request.Params["ctl00$MainContent$tbGoodsModel" + i.ToString()]) ? Page.Request.Params["ctl00$MainContent$tbGoodsModel" + i.ToString()] : Page.Request.Params["ctl00$MainContent$hfGoodsModel" + i.ToString()];
                    var goodsCost = !String.IsNullOrEmpty(Page.Request.Params["ctl00$MainContent$tbGoodsCost" + i.ToString()]) ? Page.Request.Params["ctl00$MainContent$tbGoodsCost" + i.ToString()] : Page.Request.Params["ctl00$MainContent$hfGoodsCost" + i.ToString()];
                    var goodsNumber = !String.IsNullOrEmpty(Page.Request.Params["ctl00$MainContent$tbGoodsNumber" + i.ToString()]) ? Page.Request.Params["ctl00$MainContent$tbGoodsNumber" + i.ToString()] : Page.Request.Params["ctl00$MainContent$hfGoodsNumber" + i.ToString()];
                    var hfGoodsID = Page.Request.Params["ctl00$MainContent$hfGoodsID" + i.ToString()];
                    decimal dec;
                    Decimal.TryParse(goodsCost, out dec);

                    var goods = new Goods
                    {
                        Description = goodsDescription,
                        Model = goodsModel,
                        Number = Convert.ToInt32(goodsNumber.Replace(" ", "")),
                        Cost = dec,
                        TicketFullSecureID = FullSecureID,
                        ID = Convert.ToInt32(hfGoodsID)
                    };

                    goods.Update(userId, OtherMethods.GetIPAddress(), "UserTicketEdit");
                }
            }
        }

        #endregion

        #region Финальный редирект на нужную страницу
        public void FinalyRedirect()
        {
            if (!string.IsNullOrEmpty(Page.Request.Params["page"]) && Page.Request.Params["page"] == "money")
            {
                Page.Response.Redirect("~/ManagerUI/Menu/Finance/MoneyView.aspx?" + BackLink);
            }

            if (!string.IsNullOrEmpty(Page.Request.Params["page"]) && Page.Request.Params["page"] == "issuance")
            {
                Page.Response.Redirect("~/ManagerUI/Menu/Issuance/IssuanceView.aspx?" + BackLink);
            }

            if (!string.IsNullOrEmpty(Page.Request.Params["page"]) && Page.Request.Params["page"] == "userticketsview")
            {
                Page.Response.Redirect("~/ManagerUI/Menu/Tickets/UserTicketView.aspx?" + BackLink);
            }

            if (!string.IsNullOrEmpty(Page.Request.Params["page"]) && Page.Request.Params["page"] == "userticketbydeliveryonbelarus")
            {
                Page.Response.Redirect("~/ManagerUI/Menu/Tickets/UserTicketByDeliveryOnBelarus.aspx?" + BackLink);
            }

            if (!string.IsNullOrEmpty(Page.Request.Params["page"]) && Page.Request.Params["page"] == "userticketbydeliveryonminsk")
            {
                Page.Response.Redirect("~/ManagerUI/Menu/Tickets/UserTicketByDeliveryOnMinsk.aspx?" + BackLink);
            }

            if (!string.IsNullOrEmpty(Page.Request.Params["page"]) && Page.Request.Params["page"] == "userticketsnotprocessed")
            {
                Page.Response.Redirect("~/ManagerUI/Menu/Tickets/UserTicketNotProcessedView.aspx?" + BackLink);
            }

            if (!string.IsNullOrEmpty(Page.Request.Params["page"]) && Page.Request.Params["page"] == "userticketsviewmy")
            {
                Page.Response.Redirect("~/ManagerUI/Menu/Tickets/UserTicketViewMy.aspx?" + BackLink);
            }

            if (!string.IsNullOrEmpty(Page.Request.Params["page"]) && Page.Request.Params["page"] == "calculation")
            {
                Page.Response.Redirect("~/ManagerUI/Menu/Finance/CalculationView.aspx?" + BackLink);
            }

            if (!string.IsNullOrEmpty(Page.Request.Params["page"]) && Page.Request.Params["page"] == "userticketsviewbydeliveryonminsk")
            {
                Page.Response.Redirect("~/ManagerUI/Menu/Tickets/UserTicketByDeliveryOnMinsk.aspx?" + BackLink);
            }

            if (!string.IsNullOrEmpty(Page.Request.Params["page"]) && Page.Request.Params["page"] == "newissuanceview")
            {
                Page.Response.Redirect("~/ManagerUI/Menu/Issuance/NewIssuanceView.aspx?" + BackLink);
            }

            //редирект на страницу со всеми заявками, если не задана страница
            if (!string.IsNullOrEmpty(Page.Request.Params["page"]) && string.IsNullOrEmpty(Page.Request.Params["page"]))
            {
                Page.Response.Redirect("~/ManagerUI/Menu/Tickets/UserTicketView.aspx?" + BackLink);
            }
        }
        #endregion
    }
}