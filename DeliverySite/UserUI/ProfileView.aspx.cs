using System;
using Delivery.BLL;
using Delivery.BLL.Helpers;
using Delivery.BLL.StaticMethods;
using Delivery.DAL.DataBaseObjects;
using DeliverySite.Resources;

namespace Delivery.UserUI
{
    public partial class ProfileView : UserBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = PagesTitles.UserProfileViewTitle + BackendHelper.TagToValue("page_title_part");
            OtherMethods.ActiveRightMenuStyleChanche("hlProfile", this.Page);

            var id = Page.Request.Params["id"];
            var profile = new UsersProfiles { ID = Convert.ToInt32(id)};
            profile.GetById();
            if (!IsPostBack)
            {
                lblFirstName.Text = profile.FirstName;
                lblLastName.Text = profile.LastName;
                lblThirdName.Text = profile.ThirdName;
                lblDirectorFIO.Text = String.Format("{0} {1} {2}", profile.FirstName, profile.LastName,
                         profile.ThirdName);
                lblDirectorPhone.Text = profile.DirectorPhoneNumber;
                lblContactPersonFIO.Text = profile.ContactPersonFIO;
                lblContactPersonPhones.Text = profile.ContactPhoneNumbers;
                lblPassportData.Text = profile.PassportData;
                lblPassportNumber.Text = profile.PassportNumber;
                lblAddress.Text = profile.Address;
                lblCompanuName.Text = profile.CompanyName;
                lblCompanyAddress.Text = profile.CompanyAddress;
                lblBankName.Text = profile.BankName;
                lblBankAddress.Text = profile.BankCode;
                lblBankCode.Text = profile.BankCode;
                lblRS.Text = profile.RasShet;
                lblUNP.Text = profile.UNP;
                lblStatus.Text = UsersProfilesHelper.UserProfileStatusToText(Convert.ToInt32(profile.StatusID));
                if (!String.IsNullOrEmpty(profile.RejectBlockedMessage) && profile.StatusID != 1 && profile.StatusID != 0)
                {
                    pnlStatusDescription.Visible = true;
                    lblStatusDescription.Text = profile.RejectBlockedMessage;
                }
            }
            if (profile.TypeID == 1)
            {
                pnlUr.Visible = false;
                pnlFiz.Visible = true;
            }
            else
            {
                pnlUr.Visible = true;
                pnlFiz.Visible = false;
            }
        }
    }
}