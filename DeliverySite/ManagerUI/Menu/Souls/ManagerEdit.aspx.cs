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
    public partial class ManagerEdit : ManagerBasePage
    {
        protected string ButtonText { get; set; }

        protected void Page_Init(object sender, EventArgs e)
        {
            ButtonText = Page.Request.Params["id"] != null ? "Изменить" : "Добавить";
            btnCreate.Click += bntCreate_Click;
            DataBind();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            OtherMethods.ActiveRightMenuStyleChanche("hlManagers", this.Page);
            OtherMethods.ActiveRightMenuStyleChanche("hlSouls", this.Page);
            Page.Title = Page.Request.Params["id"] != null ? PagesTitles.ManagerManagerEdit + BackendHelper.TagToValue("page_title_part") : PagesTitles.ManagerManagerCreate + BackendHelper.TagToValue("page_title_part");

            #region Блок доступа к странице
            var userInSession = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());
            if (currentRole.PageManagerEdit != 1)
            {
                Response.Redirect("~/Error.aspx?id=1");
            }
            #endregion

            if (currentRole.PageChangePasswords != 1 || Page.Request.Params["id"] == null)
            {
                divChangePassword.Visible = false;
                divGravatar.Visible = false;
            }

            lblTitle.Text = "Создание";
            if (!IsPostBack)
            {
                var roles = new Roles();
                var dataSet = roles.GetAllItems("Name", "ASC", null);
                ddlRole.DataSource = dataSet;
                ddlRole.DataTextField = "NameOnRuss";
                ddlRole.DataValueField = "Name";
                ddlRole.DataBind();
                ddlRole.Items.Remove(ddlRole.Items.FindByValue("SuperAdmin")); //никого нельзя сделать суперадмином
            }

            if (!IsPostBack)
            {
                ddlStatus.DataSource = Users.UserStatuses;
                ddlStatus.DataTextField = "Value";
                ddlStatus.DataValueField = "Key";
                ddlStatus.DataBind();
                ddlRole.SelectedValue = "Manager";
            }
            if (Page.Request.Params["id"] != null)
            {
                lblTitle.Text = "Редактирование";
                lblPass.Visible = false;

                if (!IsPostBack)
                {
                    var manager = new Users { ID = Convert.ToInt32(Page.Request.Params["id"]) };
                    manager.GetById();
                    //если суперадмин - нельзя изменить его статус и роль
                    if (manager.Role == Users.Roles.SuperAdmin.ToString())
                    {
                        divRole.Visible = false;
                        divStatus.Visible = false;
                    }
                    var id = Page.Request.Params["id"];
                    tbPassword.Visible = id == null;
                    tbName.Text = manager.Name;
                    tbFamily.Text = manager.Family;
                    tbLogin.Text = manager.Login;
                    tbEmail.Text = manager.Email;

                    tbAddress.Text = manager.Address;
                    tbPhone.Text = manager.Phone;
                    tbPhoneHome.Text = manager.PhoneHome;
                    tbPhoneWorkOne.Text = manager.PhoneWorkOne;
                    tbPhoneWorkTwo.Text = manager.PhoneWorkTwo;
                    tbBirthDay.Text = Convert.ToDateTime(manager.BirthDay).ToString("dd-MM-yyyy");
                    tbDateOfIssue.Text = Convert.ToDateTime(manager.DateOfIssue).ToString("dd-MM-yyyy");
                    tbSkype.Text = manager.Skype;

                    tbPassportSeria.Text = manager.PassportSeria;
                    tbPassportNumber.Text = manager.PassportNumber;
                    tbPersonalNumber.Text = manager.PersonalNumber;
                    tbROVD.Text = manager.ROVD;
                    tbValidity.Text = Convert.ToDateTime(manager.Validity).ToString("dd-MM-yyyy");
                    tbRegistrationAddress.Text = manager.RegistrationAddress;

                    ddlStatus.SelectedIndex = Convert.ToInt32(manager.Status - 1);
                    ddlRole.SelectedValue = manager.Role;
                    cbAccessOnlyByWhiteList.Checked = manager.AccessOnlyByWhiteList != 0;
                    hlChangePassword.NavigateUrl = String.Format("~/ManagerUI/Menu/Settings/ChangePasswords.aspx?uid={0}", manager.ID);
                    imgGravatar.ImageUrl = Gravatar.GravatarImageLink(manager.Email, "180");
                }
            }
        }

        public void bntCreate_Click(Object sender, EventArgs e)
        {
            var id = Page.Request.Params["id"];
            var userInSession = (Users)Session["userinsession"];
            if (id == null)
            {
                lblError.Text = String.Empty;
                var loginCorrectly = UsersHelper.UserLoginChecker(tbLogin.Text.Trim());
                var emailCorrectly = UsersHelper.UserEmailChecker(tbEmail.Text.Trim());

                var registrationPosible = true;
                if (!emailCorrectly)
                {
                    lblError.Text += "Пользователь с таким e-mail'ом уже есть в нашей базе!<br/>";
                    registrationPosible = false;
                }

                if (!loginCorrectly)
                {
                    lblError.Text += "Пользователь с таким логином уже есть в нашей базе!<br/>";
                    registrationPosible = false;
                }

                //окончательная проверка
                if (!registrationPosible)
                {
                    return;
                }

                var manager = new Users
                {
                    Name = tbName.Text,
                    Family = tbFamily.Text,
                    Email = tbEmail.Text,
                    Login = tbLogin.Text,
                    Password = OtherMethods.HashPassword(tbPassword.Text),
                    Status = Convert.ToInt32(ddlStatus.SelectedValue),
                    Role = ddlRole.SelectedValue,
                    CreateDate = DateTime.Now,

                    Address = tbAddress.Text,
                    Phone = tbPhone.Text,
                    PhoneHome = tbPhoneHome.Text,
                    PhoneWorkOne = tbPhoneWorkOne.Text,
                    PhoneWorkTwo = tbPhoneWorkTwo.Text,
                    BirthDay = Convert.ToDateTime(tbBirthDay.Text),
                    DateOfIssue = Convert.ToDateTime(tbDateOfIssue.Text),

                    PassportSeria = tbPassportSeria.Text,
                    PassportNumber = tbPassportNumber.Text,
                    PersonalNumber = tbPersonalNumber.Text,
                    ROVD = tbROVD.Text,
                    Validity = Convert.ToDateTime(tbValidity.Text),
                    RegistrationAddress = tbRegistrationAddress.Text,
                };
                manager.Create();

                var body = "Вы зарегистрированы на сайте " + BackendHelper.TagToValue("current_app_address") + " в качестве " + manager.Role + ". Ваш логин: " + manager.Login + ", ваш е-mail: " + manager.Email;
                const string subj = "Регистрация нового работника";
                EmailMethods.MailSend(subj, body, manager.Email);
            }
            else
            {
                var manager = new Users { ID = Convert.ToInt32(id) };
                manager.GetById();
                var oldRole = manager.Role;
                lblError.Text = String.Empty;
                var loginCorrectly = UsersHelper.UserLoginChecker(tbLogin.Text.Trim());
                var emailCorrectly = UsersHelper.UserEmailChecker(tbEmail.Text.Trim());

                var registrationPosible = true;
                if (!emailCorrectly && manager.Email != tbEmail.Text)
                {
                    lblError.Text += "Пользователь с таким e-mail'ом уже есть в нашей базе!<br/>";
                    registrationPosible = false;
                }

                if (!loginCorrectly && manager.Login != tbLogin.Text)
                {
                    lblError.Text += "Пользователь с таким логином уже есть в нашей базе!<br/>";
                    registrationPosible = false;
                }

                //окончательная проверка
                if (!registrationPosible)
                {
                    return;
                }

                manager.Name = tbName.Text.Trim();
                manager.Family = tbFamily.Text.Trim();
                manager.Email = tbEmail.Text.Trim();
                manager.Login = tbLogin.Text.Trim();
                manager.Status = Convert.ToInt32(ddlStatus.SelectedValue);
                manager.Role = ddlRole.SelectedValue;
                manager.ChangeDate = DateTime.Now;
                manager.AccessOnlyByWhiteList = cbAccessOnlyByWhiteList.Checked ? 1 : 0;

                manager.Address = tbAddress.Text;
                manager.Phone = tbPhone.Text;
                manager.PhoneHome = tbPhoneHome.Text;
                manager.PhoneWorkOne = tbPhoneWorkOne.Text;
                manager.PhoneWorkTwo = tbPhoneWorkTwo.Text;
                manager.BirthDay = Convert.ToDateTime(tbBirthDay.Text);
                manager.DateOfIssue = Convert.ToDateTime(tbDateOfIssue.Text);
                manager.Skype = tbSkype.Text.Trim();

                manager.PassportSeria = tbPassportSeria.Text;
                manager.PassportNumber = tbPassportNumber.Text;
                manager.PersonalNumber = tbPersonalNumber.Text;
                manager.ROVD = tbROVD.Text;
                manager.Validity = Convert.ToDateTime(tbValidity.Text);
                manager.RegistrationAddress = tbRegistrationAddress.Text;
                //если суперадмин - выставляем ему роль суперадмина и статус активировано
                if (oldRole == Users.Roles.SuperAdmin.ToString())
                {
                    divRole.Visible = false;
                    divStatus.Visible = false;
                    manager.Role = oldRole;
                }

                manager.Update(userInSession.ID, OtherMethods.GetIPAddress(), "ManagerEdit");

                var body = "Ваш профиль на " + BackendHelper.TagToValue("current_app_address") + " был изменен. Ваша роль: " + manager.Role + ", ваш логин: " + manager.Login + ", ваш е-mail: " + manager.Email + ", ваше имя: " + manager.Name + ", ваша фамилия: " + manager.Family + ", ваш статус: " + ddlRole.SelectedValue;
                const string subj = "Изменение профиля работника";
                EmailMethods.MailSend(subj, body, manager.Email);
            }
            Page.Response.Redirect("~/ManagerUI/Menu/Souls/ManagersView.aspx");
        }
    }
}