using Delivery.BLL;
using Delivery.BLL.Helpers;
using Delivery.BLL.StaticMethods;
using Delivery.DAL.DataBaseObjects;
using DeliverySite.Resources;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Delivery.ManagerUI.Menu.Souls
{
    public partial class ManagerView : ManagerBasePage
    {
        protected string ButtonText { get; set; }

        protected void Page_Init(object sender, EventArgs e)
        {
            DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = PagesTitles.ManagerManagerView + BackendHelper.TagToValue("page_title_part");
            OtherMethods.ActiveRightMenuStyleChanche("hlSouls", this.Page);
            OtherMethods.ActiveRightMenuStyleChanche("hlManagers", this.Page);

            #region Блок доступа к странице
            var userInSession = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());
            if (currentRole.PageManagerView != 1)
            {
                Response.Redirect("~/Error.aspx?id=1");
            }
            #endregion

            if (currentRole.PageManagerEdit != 1)
            {
                btnEdit.Visible = false;
            }

            if (currentRole.PageChangePasswords != 1)
            {
                divChangePassword.Visible = false;
            }

            if (Page.Request.Params["id"] != null)
            {
                if (!IsPostBack)
                {

                    var user = new Users
                    {
                        ID = Convert.ToInt32(Page.Request.Params["id"])
                    };
                    user.GetById();

                    if (user.ID != 0)
                    {
                        lblID.Text = user.ID.ToString();
                        lblName.Text = user.Name;
                        lblFamily.Text = user.Family;
                        lblLogin.Text = user.Login;
                        lblEmail.Text = user.Email;
                        lblRole.Text = UsersHelper.RoleToRuss(user.Role);
                        imgGravatar.ImageUrl = Gravatar.GravatarImageLink(user.Email, "180");
                        lblWhiteList.Text = user.AccessOnlyByWhiteList == 0 ? "нет" : "да";
                        hlChangePassword.NavigateUrl = String.Format("~/ManagerUI/Menu/Settings/ChangePasswords.aspx?uid={0}", user.ID);
                        lblStatus.Text = UsersHelper.UserStatusToText(Convert.ToInt32(user.Status));

                        lblAddress.Text = user.Address;
                        lblPhone.Text = user.Phone;
                        lblPhoneHome.Text = user.PhoneHome;
                        lblPhoneWorkOne.Text = user.PhoneWorkOne;
                        lblPhoneWorkTwo.Text = user.PhoneWorkTwo;
                        lblBirthDay.Text = Convert.ToDateTime(user.BirthDay).ToString("dd-MM-yyyy");
                        lblDateOfIssue.Text = Convert.ToDateTime(user.DateOfIssue).ToString("dd-MM-yyyy");
                        lblSkype.Text = user.Skype;

                        lblPassport.Text = String.Format("{0}{1}", user.PassportSeria, user.PassportNumber);
                        lblPersonalNumber.Text = user.PersonalNumber;
                        lblROVD.Text = user.ROVD;
                        lblValidity.Text = Convert.ToDateTime(user.Validity).ToString("dd-MM-yyyy");
                        lblRegistration.Text = user.RegistrationAddress;
                    }

                }
            }
        }

        protected void btnEdit_click(object sender, EventArgs e)
        {
            Response.Redirect(String.Format("~/ManagerUI/Menu/Souls/ManagerEdit.aspx?id={0}", Page.Request.Params["id"]));
        }
    }
}