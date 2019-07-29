using Delivery.BLL;
using Delivery.BLL.Helpers;
using Delivery.BLL.StaticMethods;
using Delivery.DAL;
using Delivery.DAL.DataBaseObjects;
using DeliverySite.Resources;
using Delivery.WebServices.Objects;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Delivery.UserUI
{
    public partial class TicketEdit : UserBasePage
    {
        #region Property
        protected string ButtonText { get; set; }

        protected string AssessedCost { get; set; }

        protected string ActionText { get; set; }

        protected string TicketSecureID { get; set; }

        public String AvailableTitles { get; set; }

        public String AvailableUserProfiles { get; set; }

        public String FullSecureID { get; set; }

        public String AppKey { get; set; }

        public String FirstUserApiKey { get; set; }

        public string DenomDate;

        public string DenomCoeff;

        public String SelectProfiles { get; set; }
        #endregion

        protected void Page_Init(object sender, EventArgs e)
        {
            btnCreate.Click += bntCreate_Click;
            ActionText = Page.Request.Params["id"] != null ? "Редактирование" : "Создание";
            btnCreate.Text = Page.Request.Params["id"] != null ? "Сохранить" : "Создать";
            btNewProfile.Click += btnNewProfile_Click;
            btUpdateProfile.Click += btUpdateProfile_Click;
            btDeleteProfile.Click += btDeleteProfile_Click;
            
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            DenomDate = ConfigurationManager.AppSettings["denom:Date"];
            DenomCoeff = ConfigurationManager.AppSettings["denom:Coeff"];
            Page.Title = Page.Request.Params["id"] != null ? PagesTitles.UserTicketEditTitle + BackendHelper.TagToValue("page_title_part") : PagesTitles.UserTicketCreateTitle + BackendHelper.TagToValue("page_title_part");
            OtherMethods.ActiveRightMenuStyleChanche("hlTickets", this.Page);
            OtherMethods.ActiveRightMenuStyleChanche("hlTicketCreate", this.Page);
            Form.DefaultButton = btnCreate.UniqueID; //кнопка по умолчанию при нажатии
            AppKey = Globals.Settings.AppServiceSecureKey;
            FirstUserApiKey = Globals.Settings.FirstUserApiKey;

            SelectProfiles = "";

            if (String.IsNullOrEmpty(ActivatedProfilesCount) || Convert.ToInt32(ActivatedProfilesCount) == 0)
            {
                Response.Redirect("~/usernotification/12");
            }

            var user = (Users)Session["userinsession"];
            hfUserID.Value = user.ID.ToString();

            #region Category Autocomplete автокомплит наименований
            var dm = new DataManager();
            var avaliableTitlesDs = new DataSet();
            var ifUserHaveAssignSection = dm.QueryWithReturnDataSet(String.Format("SELECT * FROM `userstocategory` WHERE `UserID` = {0}", UserID));
            avaliableTitlesDs = dm.QueryWithReturnDataSet(ifUserHaveAssignSection.Tables[0].Rows.Count == 0 ?
                "SELECT * FROM `titles` T WHERE T.`CategoryID` = 90 ORDER BY `Name`" :
                String.Format("SELECT * FROM `titles` C WHERE C.`CategoryID` IN (SELECT `CategoryID` FROM `userstocategory` WHERE `UserID` = {0})  ORDER BY C.`Name`", UserID));


            var availableTitles = avaliableTitlesDs.Tables[0].Rows.Cast<DataRow>().Aggregate(String.Empty, (current, items) => current + ("\"" + items["Name"] + "\","));
            //bug #465
            if (!availableTitles.Any())
            {
                Page.Response.Redirect("~/usernotification/15");
            }
            availableTitles.Remove(availableTitles.Length - 1);
            AvailableTitles = availableTitles;
            #endregion

            #region Profiles Autocomplete автокомплит профилей

            var userProfilesDataSet = dm.QueryWithReturnDataSet(string.Format("SELECT * FROM `usersprofiles` WHERE `UserID` = {0} AND `StatusID` = 1", UserID));
            var alluserProfiles = new List<AllUserProfilesForAutocompliteResult>();
            foreach (DataRow row in userProfilesDataSet.Tables[0].Rows)
            {
                var profileName = String.Empty;
                profileName = String.IsNullOrEmpty(row["CompanyName"].ToString()) ? String.Format("{0} {1}", row["FirstName"], row["LastName"]) : row["CompanyName"].ToString() + " ";

                alluserProfiles.Add(new AllUserProfilesForAutocompliteResult()
                {
                    value = profileName,
                    data = row["TypeID"].ToString() + row["ID"].ToString(),
                    id = row["ID"].ToString()
                });
            }
            var js = new JavaScriptSerializer();
            AvailableUserProfiles = js.Serialize(alluserProfiles);
            #endregion

            #region блок загрузки доп полей для управленцев (загрузка одноразового пользователя)

            if (user.Role != Users.Roles.User.ToString())
            {
                hfUserID.Value = "1";
                hlFeedbackNewCategory.Visible = false;
                hfUserDiscount.Value = "0";
            }
            else
            {
                hfUserDiscount.Value = user.Discount.ToString();
            }
            #endregion

            if (!IsPostBack)
            {
                #region DefaultProfile выделяем профиль по умолчанию
                var profileDefault = new UsersProfiles
                {
                    UserID = UserID,
                    IsDefault = 1
                };
                profileDefault.GetByUserIDAndDefault();
                if (!String.IsNullOrEmpty(profileDefault.ID.ToString()) && profileDefault.ID != 0 && String.IsNullOrEmpty(Page.Request.Params["a"]) && profileDefault.StatusID == 1)
                {
                    hfUserProfileID.Value = String.Format("{0}{1}", profileDefault.TypeID, profileDefault.ID);
                    tbUserProfile.Text = String.IsNullOrEmpty(profileDefault.CompanyName) ? String.Format("{0} {1}", profileDefault.FirstName, profileDefault.LastName) : profileDefault.CompanyName;
                }

                #endregion
            }

            if(IsPostBack)
            {
                //AddGodsInPanel(Convert.ToInt32(hfHowManyControls.Value));
                if (errAddProfile.Visible == true)
                    errAddProfile.Visible = false;
                if (errUpdateProfile.Visible == true)
                    errAddProfile.Visible = false;
            }

            if (Page.Request.Params["id"] != null && !IsPostBack)
            {
                #region Загрузка tickets по SecureID или FullSecureID. Метод на очистку.
                Tickets ticket;
                if (Page.Request.Params["id"].Length > 7)
                {
                    ticket = new Tickets { FullSecureID = Page.Request.Params["id"] };
                    ticket.GetByFullSecureId();
                }
                else
                {
                    ticket = new Tickets { SecureID = Page.Request.Params["id"] };
                    ticket.GetBySecureId();
                }
                #endregion

                cbIsDeliveryCost.Enabled =
                    tbDeliveryCost.Enabled = true;
                if (!String.IsNullOrEmpty(ticket.DeliveryCost.ToString()) && ticket.DeliveryCost != 0)
                {
                    cbIsDeliveryCost.Checked = true;
                }

                #region Защита от просмотра чужих заявок, если только ты не из управляющих (а если из управляющих - можно смотреть только 1-го пользователя)
                if (ticket.UserID != user.ID)
                {
                    if (user.Role == Users.Roles.User.ToString())
                    {
                        Response.Redirect("~/UserNotification.aspx?id=6");
                    }
                    else
                    {
                        if (ticket.UserID != 1)
                        {
                            Response.Redirect("~/UserNotification.aspx?id=6");
                        }
                    }
                }
                #endregion

                #region Создание форм для груза
                hfHowManyControls.Value = GoodsHelper.GoodsCount(ticket.FullSecureID).ToString();
                AddGodsInPanel(Convert.ToInt32(hfHowManyControls.Value));
                #endregion

                #region Заполнение созданных формы
                var goods = new Goods { TicketFullSecureID = ticket.FullSecureID };
                var ds = goods.GetAllItems("ID", "ASC", "TicketFullSecureID");
                var goodsIterator = 1;
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    var tbGoodsDescription = (TextBox)pnlGoods.FindControl("tbGoodsDescription" + goodsIterator);
                    var tbGoodsModel = (TextBox)pnlGoods.FindControl("tbGoodsModel" + goodsIterator);
                    var tbGoodsNumber = (TextBox)pnlGoods.FindControl("tbGoodsNumber" + goodsIterator);
                    var tbGoodsCost = (TextBox)pnlGoods.FindControl("tbGoodsCost" + goodsIterator);
                    var hfGoodsID = (HiddenField)pnlGoods.FindControl("hfGoodsID" + goodsIterator);
                    tbGoodsDescription.Text = row["Description"].ToString();
                    tbGoodsModel.Text = row["Model"].ToString();
                    tbGoodsNumber.Text = row["Number"].ToString();
                    var flagDate = row["ChangeDate"].ToString();

                    /* деноминация */
                    if (string.IsNullOrEmpty(flagDate))
                    {
                        flagDate = row["CreateDate"].ToString();
                    }
                    tbGoodsCost.Text = Convert.ToDateTime(flagDate) < Convert.ToDateTime(DenomDate)
                        ? MoneyMethods.MoneySeparator(Convert.ToDecimal(row["Cost"].ToString()) / Convert.ToInt32(DenomCoeff))
                        : MoneyMethods.MoneySeparator(row["Cost"].ToString());

                    hfGoodsID.Value = row["ID"].ToString();
                    goodsIterator++;
                }
                #endregion

                #region Вывод старых грузов. Метод на очистку.
                if (hfHowManyControls.Value == "0")
                {
                    lblOldGoods.Visible = true;
                    lblOldGoods.Text = "<b>" + OtherMethods.GoodsStringFromTicketID(ticket.ID.ToString()) + "</b><br/>";
                    btnMore.Visible = false;
                }
                #endregion

                #region Заполняем все поля на странице из ticket'a
                tbDeliveryCost.Text = MoneyMethods.MoneySeparator(ticket.DeliveryCost.ToString());
                tbID.Text = Convert.ToString(ticket.ID);
                ddlRecipientStreetPrefix.SelectedValue = ticket.RecipientStreetPrefix;
                tbRecipientStreet.Text = ticket.RecipientStreet;
                tbRecipientStreetNumber.Text = ticket.RecipientStreetNumber;
                tbRecipientKorpus.Text = ticket.RecipientKorpus;
                tbRecipientKvartira.Text = ticket.RecipientKvartira;
                ddlSenderStreetPrefix.SelectedValue = ticket.SenderStreetPrefix;
                tbSenderStreetName.Text = ticket.SenderStreetName;
                tbSenderStreetNumber.Text = ticket.SenderStreetNumber;
                tbSenderHousing.Text = ticket.SenderHousing;
                tbSenderApartmentNumber.Text = ticket.SenderApartmentNumber;
                tbTtnSeria.Text = ticket.TtnSeria;
                tbTtnNumber.Text = ticket.TtnNumber;
                tbOtherDocuments.Text = ticket.OtherDocuments;
                tbPassportNumber.Text = ticket.PassportNumber;
                tbPassportSeria.Text = ticket.PassportSeria;
                tbRecipientPhone.Text = ticket.RecipientPhone;
                tbRecipientPhone2.Text = ticket.RecipientPhoneTwo;
                tbRecipientFirstName.Text = ticket.RecipientFirstName;
                tbRecipientLastName.Text = ticket.RecipientLastName;
                tbRecipientThirdName.Text = ticket.RecipientThirdName;
                tbNote.Text = ticket.Note;
                tbBoxesNumber.Text = string.IsNullOrEmpty(ticket.BoxesNumber.ToString()) ? "1" : ticket.BoxesNumber.ToString();
                tbDeliveryDate.Text = Convert.ToDateTime(ticket.DeliveryDate).ToString("dd-MM-yyyy");
                
                lblAssessedCost.Text = MoneyMethods.MoneySeparator(ticket.AssessedCost.ToString());
                lblStatusDescriptionLabel.Visible = true;
                lblStatusDescription.Visible = true;
                lblStatus.Visible = true;
                lblStatusLabel.Visible = true;
                lblStatusDescription.Text = ticket.StatusDescription;
                lblStatus.Text =
                    OtherMethods.TicketStatusToText(
                        TicketsHelper.StatusReplacement(
                            TicketsHelper.DeferredProcessedStatus(ticket.StatusID.ToString(), ticket.StatusIDOld.ToString(), ticket.ProcessedDate.ToString())
                        )
                    );

                hfStatus.Value = ticket.StatusID.ToString();
                hfSelectProfileID.Value = ticket.SenderProfileID;

                int senderProfileID;
                if(Int32.TryParse(ticket.SenderProfileID, out senderProfileID))
                {
                    var profile = new SenderProfiles() { ID = senderProfileID };
                    profile.GetById();
                    tbSelectProfile.Text = profile.ProfileName;
                }
                

                #region Заполнение профиля из заявки
                var profileFromTicket = new UsersProfiles { ID = Convert.ToInt32(ticket.UserProfileID) };
                profileFromTicket.GetById();
                hfUserProfileID.Value = String.Format("{0}{1}", profileFromTicket.TypeID, profileFromTicket.ID);
                tbUserProfile.Text = String.IsNullOrEmpty(profileFromTicket.CompanyName) ? String.Format("{0} {1}", profileFromTicket.FirstName, profileFromTicket.LastName) : profileFromTicket.CompanyName;
                #endregion

                #endregion

                #region Заполнение городов
                var allCity = Application["CityList"] as List<City>;
                if (allCity != null)
                {
                    var city = allCity.First(u => u.ID == ticket.CityID);
                    tbCity.Text = CityHelper.CityIDToAutocompleteString(city);
                    hfCityID.Value = ticket.CityID.ToString();

                    var senderCity = allCity.First(u => u.ID == ticket.SenderCityID);
                    tbSenderCity.Text = CityHelper.CityIDToAutocompleteString(senderCity);
                    hfSenderCityID.Value = ticket.SenderCityID.ToString();
                }
                #endregion 

                hfTicketSecureID.Value = String.Format("#{0}", ticket.SecureID);

                if (Page.Request.Params["id"] != null)
                {
                    hfWharehouse.Value = ticket.WharehouseId.ToString();
                }

                #region Ticket disable (отключаем все текстбоксы и прочее если статус != 1)
                if (ticket.StatusID != 1)
                {
                    btnUserProfileChoise.Visible = false;
                    btnCreate.Visible = false;
                    var textboxes = this.Controls.FindAll().OfType<TextBox>();
                    foreach (var t in textboxes)
                    {
                        t.Enabled = false;
                    }
                    var checkboxes = this.Controls.FindAll().OfType<CheckBox>();
                    foreach (var t in checkboxes)
                    {
                        t.Enabled = false;
                    }

                    hlHowMatch.Visible = false;
                    lblHowMatch.Visible = true;
                    lblHowMatchValue.Visible = true;
                    lblHowMatchValue.Text = MoneyMethods.MoneySeparator(ticket.GruzobozCost.ToString()) + "<i> руб.</i>";

                    ActionText = "Просмотр";
                    Page.Title = PagesTitles.UserTicketViewTitle + BackendHelper.TagToValue("page_title_part");
                    btnMore.Visible = btnDeleteLast.Visible = false; // отключение кнопки добавления грузов
                }
                #endregion
            }

            if (Page.Request.Params["id"] != null && !String.IsNullOrEmpty(hfTicketSecureID.Value))
            {
                TicketSecureID = hfTicketSecureID.Value;
            }

            if (Page.Request.Params["id"] != null)
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "postbackReCalculate();", true);
            }

            if (Page.Request.Params["id"] == null && !IsPostBack)
            {
                var goodsCount = 0;
                while (!String.IsNullOrEmpty(Page.Request.Params["gd" + (goodsCount + 1)]))
                {
                    goodsCount++;
                }

                #region Создание формы для первого груза

                if (goodsCount == 0)
                {
                    hfHowManyControls.Value = "1";
                    AddGodsInPanel(Convert.ToInt32(hfHowManyControls.Value));
                }
                else
                {
                    hfHowManyControls.Value = goodsCount.ToString();
                    AddGodsInPanel(Convert.ToInt32(hfHowManyControls.Value));
                }
                #endregion

                #region Автозаполнение города отправки

                var userOrFirstUser = new Users { ID = UserID };
                userOrFirstUser.GetById();
                var allCity = Application["CityList"] as List<City>;
                if (!String.IsNullOrEmpty(userOrFirstUser.SenderCityID.ToString()) && allCity != null && userOrFirstUser.SenderCityID != 0)
                {
                    var senderCity = allCity.First(u => u.ID == userOrFirstUser.SenderCityID);
                    tbSenderCity.Text = CityHelper.CityIDToAutocompleteString(senderCity);
                    hfSenderCityID.Value = userOrFirstUser.SenderCityID.ToString();
                }

                if (!String.IsNullOrEmpty(userOrFirstUser.SenderStreetName))
                    tbSenderStreetName.Text = userOrFirstUser.SenderStreetName;

                if (!String.IsNullOrEmpty(userOrFirstUser.SenderStreetNumber))
                    tbSenderStreetNumber.Text = userOrFirstUser.SenderStreetNumber;

                if (!String.IsNullOrEmpty(userOrFirstUser.SenderStreetPrefix))
                    ddlSenderStreetPrefix.SelectedValue = userOrFirstUser.SenderStreetPrefix;

                if (!String.IsNullOrEmpty(userOrFirstUser.SenderHousing))
                    tbSenderHousing.Text = userOrFirstUser.SenderHousing;

                if (!String.IsNullOrEmpty(user.SenderApartmentNumber))
                    tbSenderApartmentNumber.Text = user.SenderApartmentNumber;

                if (!String.IsNullOrEmpty(userOrFirstUser.SenderWharehouse))
                    hfWharehouse.Value = userOrFirstUser.SenderWharehouse;
                #endregion

                #region User API (обрабатываем пришедшую информацию)
                tbBoxesNumber.Text = String.IsNullOrEmpty(Page.Request.Params["bn"]) ? "1" : Page.Request.Params["bn"];
                tbDeliveryDate.Text = Page.Request.Params["dd"];
                tbRecipientFirstName.Text = Page.Request.Params["rf"];
                tbRecipientLastName.Text = Page.Request.Params["rn"];
                tbRecipientThirdName.Text = Page.Request.Params["ro"];
                tbPassportSeria.Text = Page.Request.Params["ps"];
                tbPassportNumber.Text = Page.Request.Params["pn"];
                if (!String.IsNullOrEmpty(Page.Request.Params["srsp"]))
                    ddlSenderStreetPrefix.SelectedValue = Page.Request.Params["srsp"];
                if (!String.IsNullOrEmpty(Page.Request.Params["srs"]))
                    tbSenderStreetName.Text = Page.Request.Params["srs"];
                if (!String.IsNullOrEmpty(Page.Request.Params["srsn"]))
                    tbSenderStreetNumber.Text = Page.Request.Params["srsn"];
                if (!String.IsNullOrEmpty(Page.Request.Params["srko"]))
                    tbSenderHousing.Text = Page.Request.Params["srko"];
                if (!String.IsNullOrEmpty(Page.Request.Params["srk"]))
                    tbSenderApartmentNumber.Text = Page.Request.Params["srk"];
                ddlRecipientStreetPrefix.SelectedValue = Page.Request.Params["rsp"];
                tbRecipientStreet.Text = Page.Request.Params["rs"];
                tbRecipientStreetNumber.Text = Page.Request.Params["rsn"];
                tbRecipientKorpus.Text = Page.Request.Params["rko"];
                tbRecipientKvartira.Text = Page.Request.Params["rk"];
                if (!String.IsNullOrEmpty(Page.Request.Params["rp1"]) && Page.Request.Params["rp1"].Length > 3)
                {
                    tbRecipientPhone.Text = Page.Request.Params["rp1"].Trim().Replace("+", "").Trim();
                }
                if (!String.IsNullOrEmpty(Page.Request.Params["rp2"]) && Page.Request.Params["rp2"].Length > 3)
                {
                    tbRecipientPhone2.Text = Page.Request.Params["rp2"].Trim().Replace("+", "").Trim();
                }
                tbNote.Text = Page.Request.Params["n"];
                if (!String.IsNullOrEmpty(Page.Request.Params["a"]))
                {
                    #region Заполнение профиля из заявки
                    var profileFromTicket = new UsersProfiles { ID = Convert.ToInt32(Page.Request.Params["a"]) };
                    profileFromTicket.GetById();
                    hfUserProfileID.Value = String.Format("{0}{1}", profileFromTicket.TypeID, profileFromTicket.ID);
                    tbUserProfile.Text = String.IsNullOrEmpty(profileFromTicket.CompanyName) ? String.Format("{0} {1}", profileFromTicket.FirstName, profileFromTicket.LastName) : profileFromTicket.CompanyName;
                    #endregion
                }

                tbTtnNumber.Text = Page.Request.Params["tn"];
                tbTtnSeria.Text = Page.Request.Params["ts"];
                tbOtherDocuments.Text = Page.Request.Params["od"];
                try
                {
                    var cityIdFromApi = Convert.ToInt32(Page.Request.Params["c"]);
                    var cityFromApi = new City { ID = cityIdFromApi };
                    cityFromApi.GetById();
                    if (!String.IsNullOrEmpty(cityFromApi.Name))
                    {
                        hfCityID.Value = Page.Request.Params["c"];
                        tbCity.Text = CityHelper.CityIDToAutocompleteString(cityFromApi);
                    }

                    var senderCityIdFromApi = Convert.ToInt32(Page.Request.Params["sc"]);
                    var senderCityFromApi = new City { ID = senderCityIdFromApi };
                    senderCityFromApi.GetById();
                    if (!String.IsNullOrEmpty(senderCityFromApi.Name))
                    {
                        hfSenderCityID.Value = Page.Request.Params["sc"];
                        tbSenderCity.Text = CityHelper.CityIDToAutocompleteString(senderCityFromApi);
                    }
                }
                catch (Exception) { }

                #endregion

                #region Заполняем необходимые поля на странице при загрузке страницы
                lblAssessedCost.Text = "0"; //при загрузге оценочная стоимость = 0
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

        protected void Page_PreRender(object sender, EventArgs e)
        {
            var dm = new DataManager();
            var dsw = dm.QueryWithReturnDataSet("SELECT * FROM warehouses ORDER BY Name ASC");
            lvAllWarehouses.DataSource = dsw;
            lvAllWarehouses.DataBind();
        }

        public string CreateDataAutocompliteSelectedProfiles()
        {
            var js = new JavaScriptSerializer();            
            var dm = new DataManager();
            var senderProfilesDataSet = dm.QueryWithReturnDataSet(string.Format("SELECT * FROM `senderprofiles` WHERE `ProfileID` = {0}", UserID));
            var alluserProfiles = new List<SelectProfilesForAutocompliteResult>();
            foreach (DataRow row in senderProfilesDataSet.Tables[0].Rows)
            {
                alluserProfiles.Add(new SelectProfilesForAutocompliteResult()
                {       
                    ProfileID = row["ProfileID"].ToString(),
                    AddressHouseNumber = row["AddressHouseNumber"].ToString(),
                    AddressKorpus = row["AddressKorpus"].ToString(),
                    AddressKvartira = row["AddressKvartira"].ToString(),
                    AddressPrefix = row["AddressPrefix"].ToString(),
                    AddressStreet = row["AddressStreet"].ToString(),
                    CityCost = row["CityCost"].ToString(),
                    CityDeliveryDate = row["CityDeliveryDate"].ToString(),
                    CityDeliveryTerms = row["CityDeliveryTerms"].ToString(),
                    CityID = row["CityID"].ToString(),
                    CitySelectedString = row["CitySelectedString"].ToString(),
                    ProfileName = row["ProfileName"].ToString(),
                    RecipientPhone1 = row["RecipientPhone1"].ToString(),
                    RecipientPhone2 = row["RecipientPhone2"].ToString(),
                    SendDate = row["SendDate"].ToString(),
                    FirstName = row["FirstName"].ToString(),
                    LastName = row["LastName"].ToString(),
                    ThirdName = row["ThirdName"].ToString(),
                    value = row["ProfileName"].ToString() + "/&nbsp"
                });
            }                
            return js.Serialize(alluserProfiles);
            
            //return js.Serialize("");
        }

        public void btnNewProfile_Click(Object sender, EventArgs e)
        {     
            if(tbNewProfile.Text == "")
            {
                errAddProfile.Visible = true;
                return;
            }
            var senderProfile = new SenderProfiles()
            {                
                ProfileName = tbNewProfile.Text,
                ProfileID = UserID.ToString(),
                CitySelectedString = tbCity.Text,
                CityID = hfCityID.Value,
                CityCost = lblCityCost.Text,
                CityDeliveryDate = lblCityDeliveryDate.Text,
                CityDeliveryTerms = lblCityDeliveryTerms.Text,
                AddressPrefix = ddlRecipientStreetPrefix.Text,
                AddressStreet = tbRecipientStreet.Text,
                AddressHouseNumber = tbRecipientStreetNumber.Text,
                AddressKorpus = tbRecipientKorpus.Text,
                AddressKvartira = tbRecipientKvartira.Text,
                RecipientPhone1 = tbRecipientPhone.Text,
                RecipientPhone2 = tbRecipientPhone2.Text,
                FirstName = tbRecipientFirstName.Text,
                LastName = tbRecipientLastName.Text,
                ThirdName = tbRecipientThirdName.Text,
                SendDate = tbDeliveryDate.Text
            };
            errAddProfile.Visible = false;
            senderProfile.Create();
        }
        
        public void btUpdateProfile_Click(Object sender, EventArgs e)
        {
            var dm = new DataManager();
            var ds = dm.QueryWithReturnDataSet(string.Format("SELECT * FROM `senderprofiles` WHERE `ProfileName` = '{0}'", hfSelectProfile.Value));

            var id = 0;
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                id = Convert.ToInt32(row["ID"].ToString());
            }

            if(id==0)
            {
                errUpdateProfile.Visible = true;
                return;
            }

            var senderProfile = new SenderProfiles()
            {                
                ProfileName = hfSelectProfile.Value,
                CitySelectedString = tbCity.Text,
                CityID = hfCityID.Value,
                CityCost = lblCityCost.Text,
                CityDeliveryDate = lblCityDeliveryDate.Text,
                CityDeliveryTerms = lblCityDeliveryTerms.Text,
                AddressPrefix = ddlRecipientStreetPrefix.Text,
                AddressStreet = tbRecipientStreet.Text,
                AddressHouseNumber = tbRecipientStreetNumber.Text,
                AddressKorpus = tbRecipientKorpus.Text,
                AddressKvartira = tbRecipientKvartira.Text,
                RecipientPhone1 = tbRecipientPhone.Text,
                RecipientPhone2 = tbRecipientPhone2.Text,
                SendDate = tbDeliveryDate.Text,
                FirstName = tbRecipientFirstName.Text,
                LastName = tbRecipientLastName.Text,
                ThirdName = tbRecipientThirdName.Text,
                ID = id                
            };

            errUpdateProfile.Visible = false;
            senderProfile.Update();
        }

        public void btDeleteProfile_Click(Object sender, EventArgs e)
        {
            var dm = new DataManager();
            var ds = dm.QueryWithReturnDataSet(string.Format("SELECT * FROM `senderprofiles` WHERE `ProfileName` = '{0}'", hfSelectProfile.Value));

            var id = 0;
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                id = Convert.ToInt32(row["ID"].ToString());
            }

            if (id == 0)
            {
                errUpdateProfile.Visible = true;
                return;
            }

            var senderProfile = new SenderProfiles()
            {
                
            };

            errUpdateProfile.Visible = false;
            senderProfile.Delete(id);
        }

        public void bntCreate_Click(Object sender, EventArgs e)
        {
            var id = Page.Request.Params["id"];
            var userInSession = (Users)Session["userinsession"];
            var user = new Users
            {
                ID = UserID,                
                Login = userInSession.Login,
                Email = userInSession.Email,
                Name = userInSession.Name,
                Family = userInSession.Family,
                Role = userInSession.Role,
                RussRole = UsersHelper.RoleToRuss(userInSession.Role),
                IsCourse = userInSession.IsCourse
            };

            #region Начальные проверки
            if (tbDeliveryDate.Text == String.Empty)
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

            var date1 = Convert.ToDateTime(Convert.ToDateTime(tbDeliveryDate.Text).ToString("dd-MM-yyyy"));
            var date2 = Convert.ToDateTime(DateTime.Now.AddDays(1).ToString("dd-MM-yyyy"));

            #region Запрет на создание заявки не в день доставки
            var allCity = Application["CityList"] as List<City>;
            var city = allCity.First(u => u.ID == Convert.ToInt32(hfCityID.Value));
            var district = new Districts { ID = Convert.ToInt32(city.DistrictID) };
            district.GetById();
            var selectDayOfWeek = Convert.ToDateTime(Convert.ToDateTime(tbDeliveryDate.Text).ToString("dd-MM-yyyy")).DayOfWeek;
            if ((selectDayOfWeek == DayOfWeek.Monday && district.Monday != 1)
                || (selectDayOfWeek == DayOfWeek.Tuesday && district.Tuesday != 1)
                || (selectDayOfWeek == DayOfWeek.Wednesday && district.Wednesday != 1)
                || (selectDayOfWeek == DayOfWeek.Thursday && district.Thursday != 1)
                || (selectDayOfWeek == DayOfWeek.Friday && district.Friday != 1)
                || (selectDayOfWeek == DayOfWeek.Saturday && district.Saturday != 1)
                || (selectDayOfWeek == DayOfWeek.Sunday && district.Sunday != 1))
            {
                AddGodsInPanel(Convert.ToInt32(hfHowManyControls.Value));
                lblError.Text = "Отправка в города возможна только в дни доставки!";
                return;
            }
            #endregion

            if (date1 < date2 && hfCityID.Value != "11")
            {
                AddGodsInPanel(Convert.ToInt32(hfHowManyControls.Value));
                lblError.Text = "Отправка в города отличные от Минска возможна только на следующий день после создания заявки!";
                return;
            }

            if (date1 < Convert.ToDateTime(DateTime.Now.ToString("dd-MM-yyyy")) && hfCityID.Value == "11")
            {
                AddGodsInPanel(Convert.ToInt32(hfHowManyControls.Value));
                lblError.Text = "Доставка по Минску возможна только с текущего дня и далее!";
                return;
            }
            #endregion

            #region Присваевание заявке Одноразового пользователя, если управленец а так же инициализация заявки

            var userIdToTicket =
                UsersHelper.GetUserIdByUserProfileId(Convert.ToInt32(hfUserProfileID.Value.Remove(0, 1))); // bug #360 hot fix

            CheckError(userIdToTicket);


            var ticket = new Tickets
            {
                UserID = userIdToTicket,
                RecipientStreetPrefix = ddlRecipientStreetPrefix.SelectedValue,
                RecipientStreet = TicketsHelper.RecipientStreetCleaner(tbRecipientStreet.Text),
                RecipientStreetNumber = tbRecipientStreetNumber.Text,
                RecipientKorpus = tbRecipientKorpus.Text,
                RecipientKvartira = tbRecipientKvartira.Text,
                RecipientFirstName = tbRecipientFirstName.Text,
                RecipientLastName = tbRecipientLastName.Text,
                RecipientThirdName = tbRecipientThirdName.Text,
                RecipientPhone = tbRecipientPhone.Text,
                RecipientPhoneTwo = tbRecipientPhone2.Text,
                Note = tbNote.Text,
                UserProfileID = Convert.ToInt32(hfUserProfileID.Value.Remove(0, 1)),
                CreateDate = DateTime.Now,
                BoxesNumber = Convert.ToInt32(tbBoxesNumber.Text),
                DeliveryDate = Convert.ToDateTime(tbDeliveryDate.Text),
                StatusID = 1,
                CityID = Convert.ToInt32(hfCityID.Value),
                SenderCityID = Convert.ToInt32(hfSenderCityID.Value),
                SenderStreetName = tbSenderStreetName.Text,
                SenderStreetNumber = tbSenderStreetNumber.Text,
                SenderStreetPrefix = ddlSenderStreetPrefix.SelectedValue,
                SenderHousing = tbSenderHousing.Text,
                SenderApartmentNumber = tbSenderApartmentNumber.Text,
                DeliveryCost = cbIsDeliveryCost.Checked ? Convert.ToDecimal(tbDeliveryCost.Text) : 0,
                SenderProfileID = hfSelectProfileID.Value,
                CreditDocuments = cbIsCreditDocuments.Checked ? 1 : 0
            };
            #endregion

            #region Заполнение базовых данных заявки
            //заполнение дополнительных полей при смене типа профиля.
            if (hfUserProfileID.Value.Remove(1, hfUserProfileID.Value.Length - 1) == "2")
            {
                ticket.PrintNaklInMap = 1;
                ticket.PrintNakl = 0;
                ticket.TtnNumber = tbTtnNumber.Text;
                ticket.TtnSeria = tbTtnSeria.Text;
                ticket.OtherDocuments = tbOtherDocuments.Text;
            }
            else
            {
                ticket.PrintNaklInMap = 0;
                ticket.PrintNakl = 1;
                ticket.TtnNumber = tbTtnNumber.Text;
                ticket.TtnSeria = tbTtnSeria.Text;
                ticket.OtherDocuments = tbOtherDocuments.Text;
            }


            ticket.PassportNumber = tbPassportNumber.Text;
            ticket.PassportSeria = tbPassportSeria.Text;
            ticket.AssessedCost = GetAssessedCost();
            #endregion

            #region Рассчет и запись стоимости за услугу
            var profileType = hfUserProfileID.Value.Remove(1, hfUserProfileID.Value.Length - 1);
            var gruzobozCost = Calculator.Calculate(ListOfGoods(), Convert.ToInt32(hfCityID.Value), user.ID, Convert.ToInt32(hfUserProfileID.Value.Remove(0, 1)), profileType, hfAssessedCost.Value, Convert.ToInt32(hfUserDiscount.Value), hfWharehouse.Value == "" );
            //если стоимость за услугу не конвертируется в decimal - значит записываем 0
            try
            {
                ticket.GruzobozCost = Decimal.Parse(gruzobozCost);
            }
            catch (Exception)
            {
                ticket.GruzobozCost = 0;
            }
            #endregion

            if (hfWharehouse.Value != "")
            {
                int whVal = 0;
                if (Int32.TryParse(hfWharehouse.Value, out whVal))
                    ticket.WharehouseId = whVal;
            }

            #region Рассчет и запись общего веса на заявку

            ticket.Weight = GoodsHelper.GoodsWeight(ListOfGoods());

            #endregion

            #region Ticket Create (создаем заявку)
            if (id == null)
            {
                //записываем хеши для заявки
                if (user.Role != Users.Roles.User.ToString())
                {
                    ticket.SecureID = OtherMethods.CreateUniqId(user.ID + DateTime.Now.ToString("yyMdHms"));
                    ticket.FullSecureID = FullSecureID = OtherMethods.CreateFullUniqId(user.ID.ToString() + DateTime.Now.ToString("yyMdHms"));
                }
                else
                {
                    ticket.SecureID = OtherMethods.CreateUniqId(user.ID + DateTime.Now.ToString("yyMdHms"));
                    ticket.FullSecureID = FullSecureID = OtherMethods.CreateFullUniqId(user.ID.ToString() + DateTime.Now.ToString("yyMdHms"));
                }
                SaveGoods(); //сохраняем заявки
                ticket.Create();
                #region Отправка емейла логистам

                var logistRb = BackendHelper.TagToValue("logist_rb_email");
                var logistMinsk = BackendHelper.TagToValue("logist_minsk_email");
                var isMinskRegion = BackendHelper.TagToValue("logist_minsk_region");
                var senderCity = new City { ID = Convert.ToInt32(ticket.SenderCityID) };
                senderCity.GetById();

                if (isMinskRegion != "true")
                {
                    if (!string.IsNullOrEmpty(logistRb) && senderCity.DistrictID != 10)
                    {
                        try
                        {

                            EmailMethods.MailSend("Создана новая заявка с забором в РБ",
                                String.Format("Заявка #{0}, {1}", ticket.SecureID, senderCity.Name), logistRb, true);
                        }
                        catch (Exception)
                        {
                            // ignored
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(logistMinsk) && !(ticket.SenderCityID == 11 && ticket.SenderStreetName.ToLower() == BackendHelper.TagToValue("loading_point_street").ToLower() &&
                            ticket.SenderStreetNumber == BackendHelper.TagToValue("loading_point_street_number")))
                        {
                            try
                            {
                                EmailMethods.MailSend("Создана новая заявка с забором в Минске и районе",
                                String.Format("Заявка #{0}, {1}", ticket.SecureID, senderCity.Name), logistMinsk, true);
                            }
                            catch (Exception)
                            {
                                // ignored
                            }
                        }
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(logistRb) && senderCity.RegionID != 1)
                    {
                        try
                        {
                            EmailMethods.MailSend("Создана новая заявка с забором в РБ",
                            String.Format("Заявка #{0}, {1}", ticket.SecureID, senderCity.Name), logistRb, true);
                        }
                        catch (Exception)
                        {
                            // ignored
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(logistMinsk) && !(ticket.SenderCityID == 11 && ticket.SenderStreetName.ToLower() == BackendHelper.TagToValue("loading_point_street") &&
                            ticket.SenderStreetNumber == BackendHelper.TagToValue("loading_point_street_number")))
                        {
                            try
                            {
                                EmailMethods.MailSend("Создана новая заявка с забором в Минске и области",
                                String.Format("Заявка #{0}, {1}", ticket.SecureID, senderCity.Name), logistMinsk, true);
                            }
                            catch (Exception)
                            {
                                // ignored
                            }
                        }
                    }
                }

                #endregion

                Page.Response.Redirect("~/UserUI/TicketView.aspx");
            }
            #endregion

            #region Ticket Update (обновляем заявку)
            if (id != null)
            {
                var ticketOld = new Tickets { ID = Convert.ToInt32(tbID.Text) };
                ticketOld.GetById();
                if (ticketOld.StatusID == 1)
                {
                    ticket.ID = Convert.ToInt32(tbID.Text);
                    ticket.Update();
                    FullSecureID = ticketOld.FullSecureID;
                    UpdateGoods(Convert.ToInt32(hfHowManyControls.Value));
                    Page.Response.Redirect("~/UserUI/TicketView.aspx");
                }
                else
                {
                    lblError.Text = "Заявка уже обработана менеджером. Вы не можете ее изменять!";
                }
            }
            #endregion
        }

        #region GoodsMethods

        private void CheckError(int userIdToTicket)
        {
            var UserID = userIdToTicket;
            var RecipientStreetPrefix = ddlRecipientStreetPrefix.SelectedValue;
            var RecipientStreet = TicketsHelper.RecipientStreetCleaner(tbRecipientStreet.Text);
            var RecipientStreetNumber = tbRecipientStreetNumber.Text;
            var RecipientKorpus = tbRecipientKorpus.Text;
            var RecipientKvartira = tbRecipientKvartira.Text;
            var RecipientFirstName = tbRecipientFirstName.Text;
            var RecipientLastName = tbRecipientLastName.Text;
            var RecipientThirdName = tbRecipientThirdName.Text;
            var RecipientPhone = tbRecipientPhone.Text;
            var RecipientPhoneTwo = tbRecipientPhone2.Text;
            var Note = tbNote.Text;
            var UserProfileID = Convert.ToInt32(hfUserProfileID.Value.Remove(0, 1));
            var CreateDate = DateTime.Now;
            var BoxesNumber = Convert.ToInt32(tbBoxesNumber.Text);
            var DeliveryDate = Convert.ToDateTime(tbDeliveryDate.Text);
            var CityID = Convert.ToInt32(hfCityID.Value);
            var SenderCityID = Convert.ToInt32(hfSenderCityID.Value);
            var SenderStreetName = tbSenderStreetName.Text;
            var SenderStreetNumber = tbSenderStreetNumber.Text;
            var SenderStreetPrefix = ddlSenderStreetPrefix.SelectedValue;
            var SenderHousing = tbSenderHousing.Text;
            var SenderApartmentNumber = tbSenderApartmentNumber.Text;
            var DeliveryCost = cbIsDeliveryCost.Checked ? Convert.ToDecimal(tbDeliveryCost.Text) : 0;
            var SenderProfileID = hfSelectProfileID.Value;
        }

        protected void btnMore_CLick(object sender, EventArgs e)
        {
            AddGodsInPanel(Convert.ToInt32(hfHowManyControls.Value) + 1);
            hfHowManyControls.Value = (Convert.ToInt32(hfHowManyControls.Value) + 1).ToString();
        }

        protected void btnDeleteLast_CLick(object sender, EventArgs e)
        {
            AddGodsInPanel(Convert.ToInt32(hfHowManyControls.Value) - 1);
            hfHowManyControls.Value = (Convert.ToInt32(hfHowManyControls.Value) - 1).ToString();
        }

        private void AddGodsInPanel(int count)
        {
            if (count == 0) { return; }
            btnDeleteLast.Visible = count > 1;
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
                var cvDeliveryCost = new CustomValidator();

                tbGoodsDescription.ID = "tbGoodsDescription" + i.ToString();
                tbGoodsDescription.Width = new Unit(90, UnitType.Percentage);
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

                cvDeliveryCost.ID = "cvDeliveryCost";
                cvDeliveryCost.ValidationGroup = "LoginGroup";
                cvDeliveryCost.ControlToValidate = "tbDeliveryCost";
                cvDeliveryCost.ClientValidationFunction = "validateIfEmpty";
                cvDeliveryCost.EnableClientScript = true;
                cvDeliveryCost.ValidateEmptyText = true;
                cvDeliveryCost.Display = ValidatorDisplay.None;
                cvDeliveryCost.ErrorMessage = "Вы не ввели стоимость доставки";

                tbGoodsModel.ID = "tbGoodsModel" + i.ToString();
                tbGoodsModel.Width = new Unit(90, UnitType.Percentage);
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
                tbGoodsCost.Width = new Unit(34, UnitType.Percentage);
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
                cvGoodsCost.ErrorMessage = "Вы не ввели стоимость груза #" + i.ToString();




                tbGoodsNumber.ID = "tbGoodsNumber" + i.ToString();
                tbGoodsNumber.Width = new Unit(30, UnitType.Pixel);
                tbGoodsNumber.Attributes.Add("onkeyup", "overAssessedCost(\"" + count.ToString() + "\");");
                tbGoodsNumber.CssClass = "form-control";

                cvGoodsNumber.ID = "cvGoodsNumber" + i.ToString();
                cvGoodsNumber.ValidationGroup = "LoginGroup";
                cvGoodsNumber.ControlToValidate = "tbGoodsNumber" + i.ToString();
                cvGoodsNumber.ClientValidationFunction = "validateNotEmptyAndNotNullNumber";
                cvGoodsNumber.EnableClientScript = true;
                cvGoodsNumber.ValidateEmptyText = true;
                cvGoodsNumber.Display = ValidatorDisplay.None;
                cvGoodsNumber.ErrorMessage = "Вы не ввели колличество груза #" + i.ToString();

                hfGoodsID.ID = "hfGoodsID" + i.ToString();

                //вставляем данные из API при первой загрузке
                if (!IsPostBack && !String.IsNullOrEmpty(Page.Request.Params["gd" + i.ToString()]))
                {
                    tbGoodsDescription.Text = Page.Request.Params["gd" + i.ToString()];
                    tbGoodsModel.Text = Page.Request.Params["gm" + i.ToString()];
                    tbGoodsCost.Text = Page.Request.Params["gc" + i.ToString()];
                    tbGoodsNumber.Text = Page.Request.Params["gn" + i.ToString()];
                }

                if (IsPostBack)
                {
                    //dвосстанавливаем старые значения полей, так как они перерисовываются (поля)
                    tbGoodsDescription.Text =
                        Page.Request.Params["ctl00$MainContent$tbGoodsDescription" + i.ToString()];
                    tbGoodsModel.Text =
                        Page.Request.Params["ctl00$MainContent$tbGoodsModel" + i.ToString()];
                    tbGoodsCost.Text =
                        Page.Request.Params["ctl00$MainContent$tbGoodsCost" + i.ToString()];
                    tbGoodsNumber.Text =
                        Page.Request.Params["ctl00$MainContent$tbGoodsNumber" + i.ToString()];
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

                var string2 = "<span class=\"sumbol-counter\" id=\"counter" + i.ToString() + "\"></span>" +
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

                pnlGoods.Controls.Add(new LiteralControl(jqueruAutocomplite));
                pnlGoods.Controls.Add(new LiteralControl(string1));
                pnlGoods.Controls.Add(tbGoodsDescription);
                pnlGoods.Controls.Add(new LiteralControl(string2));
                pnlGoods.Controls.Add(tbGoodsModel);
                pnlGoods.Controls.Add(new LiteralControl(string3));
                pnlGoods.Controls.Add(tbGoodsCost);
                pnlGoods.Controls.Add(new LiteralControl(string4));
                pnlGoods.Controls.Add(tbGoodsNumber);
                pnlGoods.Controls.Add(new LiteralControl(string5));
                pnlGoods.Controls.Add(hfGoodsID);
                pnlGoods.Controls.Add(cvGoodsDescription);
                pnlGoods.Controls.Add(cvGoodsModel);
                pnlGoods.Controls.Add(cvGoodsCost);
                pnlGoods.Controls.Add(cvGoodsNumber);

            }
        }

        protected List<GoodsFromAPI> ListOfGoods()
        {
            var result = new List<GoodsFromAPI>();

            for (int i = 1; i <= Convert.ToInt32(hfHowManyControls.Value); i++)
            {
                var tbGoodsDescription = Page.Request.Params["ctl00$MainContent$tbGoodsDescription" + i.ToString()];
                var tbGoodsNumber = Page.Request.Params["ctl00$MainContent$tbGoodsNumber" + i.ToString()];
                if (!String.IsNullOrEmpty(tbGoodsDescription))
                {
                    var goods = new GoodsFromAPI
                    {
                        Description = tbGoodsDescription,
                        Number = Convert.ToInt32(tbGoodsNumber.Replace(" ", ""))
                    };
                    result.Add(goods);
                }
            }
            return result;
        }

        protected Decimal? GetAssessedCost()
        {
            Decimal? result = 0;
            for (int i = 1; i <= Convert.ToInt32(hfHowManyControls.Value); i++)
            {
                result += Convert.ToDecimal(Page.Request.Params["ctl00$MainContent$tbGoodsCost" + i.ToString()].Replace(".", ",")) *
                    Convert.ToInt32(Page.Request.Params["ctl00$MainContent$tbGoodsNumber" + i.ToString()].Replace(" ", ""));
            }
            return result;
        }

        protected void UpdateGoods(int count)
        {
            var userInSession = (Users)Session["userinsession"];
            var userIp = OtherMethods.GetIPAddress();
            var goodsNotToDeleteArray = new List<string>();

            #region выщемляются грузы, которые уже были в заявке до ее изменения
            var dm = new DataManager();
            var allticketGoodsDs = dm.QueryWithReturnDataSet(String.Format("SELECT ID FROM Goods WHERE TicketFullSecureID = \"{0}\"", FullSecureID)).Tables[0];
            var goodsTicketArray = (from DataRow row in allticketGoodsDs.Rows select row["ID"].ToString()).ToList();
            #endregion

            for (int i = 1; i <= count; i++)
            {
                var tbGoodsDescription = Page.Request.Params["ctl00$MainContent$tbGoodsDescription" + i.ToString()];
                if (!String.IsNullOrEmpty(tbGoodsDescription))
                {
                    var tbGoodsModel = Page.Request.Params["ctl00$MainContent$tbGoodsModel" + i.ToString()];
                    var tbGoodsCost = Page.Request.Params["ctl00$MainContent$tbGoodsCost" + i.ToString()];
                    var tbGoodsNumber = Page.Request.Params["ctl00$MainContent$tbGoodsNumber" + i.ToString()];
                    var hfGoodsID = Page.Request.Params["ctl00$MainContent$hfGoodsID" + i.ToString()];

                    #region выщемляются грузы, которые уже были и которые удалять не надо
                    if (!String.IsNullOrEmpty(hfGoodsID))
                    {
                        goodsNotToDeleteArray.Add(hfGoodsID);
                    }
                    #endregion

                    //Если имеется hfGoodsID - значит груз есть, и он обновляется. Если его нет - значит создается новый груз.
                    if (String.IsNullOrEmpty(hfGoodsID))
                    {
                        var goods = new Goods
                        {
                            Description = OtherMethods.BeInUseReplace(tbGoodsDescription).Trim(),
                            Model = tbGoodsModel.Trim(),
                            Number = Convert.ToInt32(tbGoodsNumber.Replace(" ", "")),
                            Cost = Convert.ToDecimal(tbGoodsCost),
                            TicketFullSecureID = FullSecureID
                        };

                        goods.Create();
                    }
                    else
                    {
                        var goods = new Goods
                        {
                            Description = OtherMethods.BeInUseReplace(tbGoodsDescription).Trim(),
                            Model = tbGoodsModel.Trim(),
                            Number = Convert.ToInt32(tbGoodsNumber.Replace(" ", "")),
                            Cost = Convert.ToDecimal(tbGoodsCost),
                            ID = Convert.ToInt32(hfGoodsID)
                        };

                        goods.Update();
                    }

                }
            }

            #region  Удаление лишних грузов
            var goodsToDeleteArray = goodsTicketArray.Except(goodsNotToDeleteArray).ToList();
            foreach (var id in goodsToDeleteArray)
            {
                var good = new Goods();
                good.Delete(Convert.ToInt32(id), userInSession.ID, userIp, "TicketEdit");
            }
            #endregion
        }


        protected void SaveGoods()
        {
            for (int i = 1; i <= Convert.ToInt32(hfHowManyControls.Value); i++)
            {
                var tbGoodsDescription = Page.Request.Params["ctl00$MainContent$tbGoodsDescription" + i.ToString()];
                if (!String.IsNullOrEmpty(tbGoodsDescription))
                {
                    var tbGoodsModel = Page.Request.Params["ctl00$MainContent$tbGoodsModel" + i.ToString()];
                    var tbGoodsCost = Page.Request.Params["ctl00$MainContent$tbGoodsCost" + i.ToString()];
                    var tbGoodsNumber = Page.Request.Params["ctl00$MainContent$tbGoodsNumber" + i.ToString()];

                    var goods = new Goods
                    {
                        Description = OtherMethods.BeInUseReplace(tbGoodsDescription).Trim(),
                        Model = tbGoodsModel.Trim(),
                        Number = Convert.ToInt32(tbGoodsNumber.Replace(" ", "")),
                        Cost = Convert.ToDecimal(tbGoodsCost),
                        TicketFullSecureID = FullSecureID
                    };

                    goods.Create();
                }
            }
        }


        #endregion
    }

    #region Disable all textboxes
    //класс, позволяющий создавать лист контролов определенного типа на странице (для Enable = false у элементов)
    public static class Extensions
    {
        public static IEnumerable<Control> FindAll(this ControlCollection collection)
        {
            foreach (Control item in collection)
            {
                yield return item;

                if (item.HasControls())
                {
                    foreach (var subItem in item.Controls.FindAll())
                    {
                        yield return subItem;
                    }
                }
            }
        }
    }
    #endregion

    public class AllUserProfilesForAutocompliteResult
    {
        public String value { get; set; }

        public String data { get; set; }
        public String id { get; set; }
    }

    public class SelectProfilesForAutocompliteResult
    {
        public String value { get; set; }

        public String data { get; set; }

        public Int32 ID { get; set; }

        public String ProfileID { get; set; }

        public String ProfileName { get; set; }
        
        public String CitySelectedString { get; set; }

        public String CityID { get; set; }
        
        public String CityCost { get; set; }
        
        public String CityDeliveryDate { get; set; }
        
        public String CityDeliveryTerms { get; set; }
        
        public String AddressPrefix { get; set; }
        
        public String AddressStreet { get; set; }
        
        public String AddressHouseNumber { get; set; }
        
        public String AddressKorpus { get; set; }
        
        public String AddressKvartira { get; set; }

        public String RecipientPhone1 { get; set; }

        public String RecipientPhone2 { get; set; }

        public String SendDate { get; set; }

        public String FirstName { get; set; }

        public String LastName { get; set; }

        public String ThirdName { get; set; }
    }
}