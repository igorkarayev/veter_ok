using System;
using System.Net.Mail;
using Delivery.BLL;
using Delivery.BLL.Helpers;
using Delivery.BLL.StaticMethods;
using Delivery.DAL.DataBaseObjects;
using DeliverySite.Resources;

namespace Delivery.UserUI
{
    public partial class ProfileEdit : UserBasePage
    {
        protected string ButtonText { get; set; }

        protected string ActionText { get; set; }

        protected void Page_Init(object sender, EventArgs e)
        {
            ButtonText = "Отправить на проверку";
            ActionText = Page.Request.Params["id"] != null ? "Редактирование" : "Создание";
            btnEdit.Click += bntEdit_Click;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = Page.Request.Params["id"] != null ? PagesTitles.UserProfileEditTitle + BackendHelper.TagToValue("page_title_part") : PagesTitles.UserProfileCreateTitle + BackendHelper.TagToValue("page_title_part");
            OtherMethods.ActiveRightMenuStyleChanche("hlProfile", this.Page);
            OtherMethods.ActiveRightMenuStyleChanche("hlProfileCreate", this.Page);
            var id = Page.Request.Params["id"];
            var profile = new UsersProfiles { ID = Convert.ToInt32(id), UserID = UserID };

            btnEdit.Text = ButtonText;
            if (!IsPostBack)
            {
                ddlProfileType.DataSource = UsersProfiles.ProfileType;
                ddlProfileType.DataTextField = "Value";
                ddlProfileType.DataValueField = "Key";
                ddlProfileType.DataBind();
            }

            if (!String.IsNullOrEmpty(id))
            {
                if (!IsPostBack)
                {
                    profile.GetById();
                    ddlProfileType.SelectedValue = profile.TypeID.ToString();
                    ddlProfileType.Enabled = false;

                    if (ddlProfileType.SelectedValue == "1")
                    {
                        pnlUr.Visible = false;
                        pnlFiz.Visible = true;
                    }
                    else
                    {
                        pnlUr.Visible = true;
                        pnlFiz.Visible = false;
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

                    tbContactPhoneNumbers.Text = profile.ContactPhoneNumbers;
                    tbContactPhoneNumbersFiz.Text = profile.ContactPhoneNumbers;
                    if (!String.IsNullOrEmpty(profile.ContactPhoneNumbers) && profile.ContactPhoneNumbers.Length > 20)
                    {
                        tbContactPhoneNumbers2Fiz.Text = profile.ContactPhoneNumbers.Remove(0, 20);
                        tbContactPhoneNumbers2.Text = profile.ContactPhoneNumbers.Remove(0, 20);
                    }
                    if (!String.IsNullOrEmpty(profile.RejectBlockedMessage) && profile.StatusID != 1 && profile.StatusID != 0)
                    {
                        pnlReject.Visible = true;
                        lblRejectBlockedMessage.Text = profile.RejectBlockedMessage;
                    }

                    lblStatus.Text = UsersProfilesHelper.UserProfileStatusToText(Convert.ToInt32(profile.StatusID));
                    pnlStatus.Visible = true;
                }
            }
        }

        public void bntEdit_Click(Object sender, EventArgs e)
        {
            var id = Page.Request.Params["id"];
            var profile = new UsersProfiles { ID = Convert.ToInt32(id), UserID = UserID };
            if (!String.IsNullOrEmpty(id))
            {
                profile.GetById();
                profile.ContactPersonFIO = tbContactPersonFIO.Text;
                profile.StatusID = 0;
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
                    profile.CompanyName = tbCompanuName.Text;
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
                    if (!String.IsNullOrEmpty(tbContactPhoneNumbers2.Text))
                    {
                        profile.ContactPhoneNumbers = tbContactPhoneNumbers.Text + ";" + tbContactPhoneNumbers2.Text;
                    }
                    else
                    {
                        profile.ContactPhoneNumbers = tbContactPhoneNumbers.Text;
                    }
                }
                profile.Update();
            }
            else
            {
                profile.TypeID = Convert.ToInt32(ddlProfileType.SelectedValue);
                profile.ContactPersonFIO = tbContactPersonFIO.Text;
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
                    profile.CompanyName = tbCompanuName.Text;
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
                    if (!String.IsNullOrEmpty(tbContactPhoneNumbers2.Text))
                    {
                        profile.ContactPhoneNumbers = tbContactPhoneNumbers.Text + ";" + tbContactPhoneNumbers2.Text;
                    }
                    else
                    {
                        profile.ContactPhoneNumbers = tbContactPhoneNumbers.Text;
                    }
                }
                profile.Create();

                var currentAppAddress = BackendHelper.TagToValue("current_app_address");
                var sendEmailWhenProfileCreateArray = BackendHelper.TagToValue("send_email_when_profile_create");
                var recipientEmailsList = sendEmailWhenProfileCreateArray.Split(new[] { ',' });
                EmailMethods.MailSendHTML(
                    String.Format("Новый профиль от пользователя #{0}", UserID),
                    String.Format(
                        "Пользователь <b>{0}</b> создал новый профиль. Для его просмотра и редактирования перейдите на <a href=\"http://{1}/ManagerUI/ProfilesView.aspx?uid={2}&stateSave=1\">страницу просмотра профилей</a>",
                        UsersHelper.UserIDToFullName(UserID.ToString()),
                        currentAppAddress,
                        UserID),
                    recipientEmailsList);
            }
            
            Page.Response.Redirect("~/UserUI/ProfilesView.aspx");
        }
    }
}