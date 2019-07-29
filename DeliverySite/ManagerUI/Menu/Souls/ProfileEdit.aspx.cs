using Delivery.BLL.Helpers;
using Delivery.BLL.StaticMethods;
using Delivery.DAL.DataBaseObjects;
using DeliverySite.BLL.StaticMethods;
using DeliverySite.Resources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Delivery.ManagerUI.Menu.Souls
{
    public partial class ProfileEdit : ManagerBasePage
    {
        protected string ButtonText { get; set; }

        protected string ActionText { get; set; }

        protected void Page_Init(object sender, EventArgs e)
        {
            btnEdit.Click += bntEdit_Click;
            btnReject.Click += btnReject_Click;
            btnActivate.Click += btnActivate_Click;
            btnBlock.Click += btnBlock_Click;
            btnDeleteProfile.Click += btnDeleteProfile_Click;
            btnCreateDoc.Click += btnCreateDoc_Click;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = PagesTitles.ManagerProfileEditTitle + BackendHelper.TagToValue("page_title_part");
            OtherMethods.ActiveRightMenuStyleChanche("hlSouls", this.Page);
            OtherMethods.ActiveRightMenuStyleChanche("hlClients", this.Page);

            #region блок настройки доступа к странице
            var userInSession = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());
            if (currentRole.PageUserProfileView != 1)
            {
                Response.Redirect("~/Error.aspx?id=1");
            }
            #endregion

            #region блок всех текстбоксов (запрет редактирования)
            if (currentRole.PageUserProfileEdit != 1)
            {
                DisableControls(Page);
            }
            #endregion

            var id = Page.Request.Params["id"];
            var profile = new UsersProfiles { ID = Convert.ToInt32(id) };
            profile.GetById();

            if (!IsPostBack)
            {
                lblProfileType.Text = UsersProfilesHelper.UserProfileTypeToText(Convert.ToInt32(profile.TypeID));
                hfProfileType.Value = profile.TypeID.ToString();
                lblStatus.Text = UsersProfilesHelper.UserProfileStatusToText(Convert.ToInt32(profile.StatusID));

                if (currentRole.ActionUserProfileChangeStatus != 1)
                {
                    btnActivate.Visible = btnBlock.Visible = btnReject.Visible = false;
                }

                switch (profile.StatusID)
                {
                    case 1:
                        btnActivate.Visible = false;
                        break;
                    case 2:
                        btnReject.Visible = false;
                        break;
                    case 3:
                        btnBlock.Visible = false;
                        break;
                }
            }

            if (!String.IsNullOrEmpty(id))
            {
                if (!IsPostBack)
                {
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

                    if (profile.TypeID == 2 || profile.TypeID == 3)
                    {
                        trProfileTypeDdl.Visible = true;
                        trProfileTypeLbl.Visible = false;
                        ddlProfileType.DataSource = UsersProfiles.ProfileType;
                        ddlProfileType.DataTextField = "Value";
                        ddlProfileType.DataValueField = "Key";
                        ddlProfileType.DataBind();
                        ddlProfileType.Items.Remove(ddlProfileType.Items.FindByValue("1"));
                        ddlProfileType.SelectedValue = profile.TypeID.ToString();
                    }
                    tbFirstName.Text = profile.FirstName;
                    tbLastName.Text = profile.LastName;
                    tbThirdName.Text = profile.ThirdName;
                    tbFirstName2.Text = profile.FirstName;
                    tbLastName2.Text = profile.LastName;
                    tbThirdName2.Text = profile.ThirdName;
                    tbPassportData.Text = profile.PassportData;
                    tbPassportNumber.Text = profile.PassportNumber;
                    tbAddress.Text = profile.Address;
                    tbCompanuName.Text = profile.CompanyName;
                    tbCompanyAddress.Text = profile.CompanyAddress;
                    tbBankName.Text = profile.BankName;
                    tbBankAddress.Text = profile.BankAddress;
                    tbBankCode.Text = profile.BankCode;
                    tbRS.Text = profile.RasShet;
                    tbUNP.Text = profile.UNP;
                    tbDirectorPhoneNumber.Text = profile.DirectorPhoneNumber;
                    tbContactPersonFIO.Text = profile.ContactPersonFIO;
                    tbPostAddress.Text = profile.PostAddress;
                    tbPassportSeria.Text = profile.PassportSeria;
                    tbPassportDate.Text = Convert.ToDateTime(profile.PassportDate).ToString("dd-MM-yyyy");
                    tbAgreementDate.Text = Convert.ToDateTime(profile.AgreementDate).ToString("dd-MM-yyyy");
                    tbAgreementNumber.Text = profile.AgreementNumber;
                    lblClientId.Text = profile.UserID.ToString();

                    tbContactPhoneNumbers.Text = profile.ContactPhoneNumbers;
                    tbContactPhoneNumbersFiz.Text = profile.ContactPhoneNumbers;
                    if (!String.IsNullOrEmpty(profile.ContactPhoneNumbers) && profile.ContactPhoneNumbers.Length > 20)
                    {
                        tbContactPhoneNumbers2Fiz.Text = profile.ContactPhoneNumbers.Remove(0, 20);
                        tbContactPhoneNumbers2.Text = profile.ContactPhoneNumbers.Remove(0, 20);
                    }
                    tbRejectBlockedMessage.Text = profile.RejectBlockedMessage;
                }
            }

            if (currentRole.ActionUserProfileDelete != 1)
            {
                btnDeleteProfile.Visible = false;
            }
        }

        public void bntEdit_Click(Object sender, EventArgs e)
        {
            UpdateProfile(null);
        }

        public void btnActivate_Click(Object sender, EventArgs e)
        {
            UpdateProfile(1);
        }

        public void btnReject_Click(Object sender, EventArgs e)
        {
            UpdateProfile(2);
        }

        public void btnBlock_Click(Object sender, EventArgs e)
        {
            UpdateProfile(3);
        }

        public void btnDeleteProfile_Click(Object sender, EventArgs e)
        {
            var id = Page.Request.Params["id"];
            var userInSession = (Users)Session["userinsession"];
            var profile = new UsersProfiles();
            profile.Delete(Convert.ToInt32(id), userInSession.ID, OtherMethods.GetIPAddress(), "ClientEdit");
            Page.Response.Redirect("~/ManagerUI/Menu/Souls/ClientsView.aspx");
        }

        public void btnCreateDoc_Click(Object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            Response.AddHeader("content-disposition", "attachment;filename=document.docx");

            new CreateProfileDoc().Create(Page.Request.Params["id"]).WriteTo(Response.OutputStream);
            /*using (var stream = new MemoryStream())
            {
                new CreateProfileDoc().CreateDoc().CopyTo(stream);
            }*/
            Response.End();
        }

        #region Other methods
        public void UpdateProfile(int? profileStatus)
        {
            var id = Page.Request.Params["id"];
            var userInSession = (Users)Session["userinsession"];
            var profile = new UsersProfiles { ID = Convert.ToInt32(id) };

            profile.GetById();
            profile.ContactPersonFIO = tbContactPersonFIO.Text;
            profile.RejectBlockedMessage = tbRejectBlockedMessage.Text;
            if (profile.TypeID == 1)
            {
                profile.PassportData = tbPassportData.Text;
                profile.PassportNumber = tbPassportNumber.Text;
                profile.FirstName = tbFirstName.Text;
                profile.LastName = tbLastName.Text;
                profile.ThirdName = tbThirdName.Text;
                profile.Address = tbAddress.Text;
                try
                {
                    profile.PassportDate = Convert.ToDateTime(tbPassportDate.Text);
                }
                catch (Exception)
                {
                    Page.Response.Redirect("~/usernotification/9");
                }
                profile.PassportSeria = tbPassportSeria.Text;
                if (!String.IsNullOrEmpty(tbContactPhoneNumbers2Fiz.Text))
                {
                    profile.ContactPhoneNumbers = tbContactPhoneNumbersFiz.Text + ";" +
                                                    tbContactPhoneNumbers2Fiz.Text;
                }
                else
                {
                    profile.ContactPhoneNumbers = tbContactPhoneNumbersFiz.Text;
                }
            }
            else
            {
                profile.CompanyName = tbCompanuName.Text.Replace("\"", "''");
                profile.CompanyAddress = tbCompanyAddress.Text;
                profile.BankName = tbBankName.Text;
                profile.BankAddress = tbBankAddress.Text;
                profile.BankCode = tbBankCode.Text;
                profile.RasShet = tbRS.Text;
                profile.UNP = tbUNP.Text;
                profile.DirectorPhoneNumber = tbDirectorPhoneNumber.Text;
                profile.FirstName = tbFirstName2.Text;
                profile.LastName = tbLastName2.Text;
                profile.ThirdName = tbThirdName2.Text;
                profile.PostAddress = tbPostAddress.Text;
                if (trProfileTypeDdl.Visible)
                    profile.TypeID = Convert.ToInt32(ddlProfileType.SelectedValue);
                if (!String.IsNullOrEmpty(tbContactPhoneNumbers2.Text))
                {
                    profile.ContactPhoneNumbers = tbContactPhoneNumbers.Text + ";" + tbContactPhoneNumbers2.Text;
                }
                else
                {
                    profile.ContactPhoneNumbers = tbContactPhoneNumbers.Text;
                }
            }
            if (profileStatus != null)
            {
                profile.StatusID = profileStatus;
            }
            try
            {
                profile.AgreementDate = Convert.ToDateTime(tbAgreementDate.Text);
            }
            catch (Exception)
            {
                Page.Response.Redirect("~/usernotification/9");
            }
            profile.AgreementNumber = tbAgreementNumber.Text;
            profile.Update(userInSession.ID, OtherMethods.GetIPAddress(), "ProfileEdit");

            Page.Response.Redirect("~/ManagerUI/Menu/Souls/ClientsView.aspx");
        }

        protected void DisableControls(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                if (c is TextBox)
                {
                    ((TextBox) (c)).Enabled = false;
                }
                if (c.Controls.Count > 0)
                {
                    DisableControls(c);
                }
            }
        }
        #endregion
    }
}