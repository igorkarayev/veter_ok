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
using Delivery;
using Delivery.UserUI;

namespace DeliverySite.UserUI
{
    public partial class ProfileEditSender : UserBasePage
    {
        public String AppKey { get; set; }
        protected string userID { get; set; }
        protected int profileID { get; set; }
        protected string actionText { get; set; }
        protected string profileName { get; set; }
        protected string tableProfileClass { get; set; }        
        public String AvailableUserProfiles { get; set; }

        protected void Page_Init(object sender, EventArgs e)
        {
            profileID = Convert.ToInt32(Page.Request.Params["id"]);            
            actionText = Page.Request.Params["id"] != null ? "Редактирование" : "Создание";
            tableProfileClass = Page.Request.Params["id"] != null ? "profileNotVisible" : "profileVisible";            
            btnCreate.Text = Page.Request.Params["id"] != null ? "Сохранить" : "Создать";
            btnCreate.Click += bntCreate_Click;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            AppKey = Globals.Settings.AppServiceSecureKey;

            #region Profiles Autocomplete автокомплит профилей
            var dm = new DataManager();
            var userProfilesDataSet = dm.QueryWithReturnDataSet(string.Format("SELECT * FROM `usersprofiles` WHERE `UserID` = {0} AND `StatusID` = 1", UserID));
            var alluserProfiles = new List<AllUserProfilesForAutocompliteResult>();
            foreach (DataRow row in userProfilesDataSet.Tables[0].Rows)
            {
                if (row["TypeID"].ToString() == "2" || row["TypeID"].ToString() == "3")
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
            }
            var js = new JavaScriptSerializer();
            AvailableUserProfiles = js.Serialize(alluserProfiles);
            #endregion
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if(Page.Request.Params["id"] != null)
            {
                var dm = new DataManager();

                SenderProfiles profile = new SenderProfiles() { ID = profileID };
                profile.GetById();
                
                hfCityID.Value = profile.CityID;
                lblCityCost.Text = profile.CityCost;
                lblCityDeliveryDate.Text = profile.CityDeliveryDate;
                lblCityDeliveryTerms.Text = profile.CityDeliveryTerms;
                tbCity.Text = profile.CitySelectedString;
                ddlRecipientStreetPrefix.Text = profile.AddressPrefix;
                tbRecipientStreet.Text = profile.AddressStreet;
                tbRecipientStreetNumber.Text = profile.AddressHouseNumber;
                tbRecipientKorpus.Text = profile.AddressKorpus;
                tbRecipientKvartira.Text = profile.AddressKvartira;
                tbDeliveryDate.Text = profile.SendDate;
                tbRecipientPhone.Text = profile.RecipientPhone1;
                tbRecipientPhone2.Text = profile.RecipientPhone2;
                tbRecipientFirstName.Text = profile.FirstName;
                tbRecipientLastName.Text = profile.LastName;
                tbRecipientThirdName.Text = profile.ThirdName;

                profileName = profile.ProfileName;
            }            
        }

        protected void bntCreate_Click(Object sender, EventArgs e)
        {
            var senderProfile = new SenderProfiles()
            {
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
                ThirdName = tbRecipientThirdName.Text                
            };

            if (Page.Request.Params["id"] != null)
            {
                senderProfile.ID = profileID;
                senderProfile.Update();
                Response.Redirect("~/UserUI/ProfilesEditSender.aspx");
            }
            else
            {
                if (tbNewProfile.Text != null && tbNewProfile.Text != "")
                {
                    lbError.Visible = false;                    
                    senderProfile.ProfileName = tbNewProfile.Text;
                    senderProfile.ProfileID = UserID.ToString();
                    senderProfile.Create();
                    Response.Redirect("~/UserUI/ProfilesEditSender.aspx");
                }         
                else
                {
                    lbError.Visible = true;
                }
            }
        }
    }
}