using Delivery.BLL;
using Delivery.BLL.Helpers;
using Delivery.BLL.StaticMethods;
using Delivery.DAL.DataBaseObjects;
using DeliverySite.Resources;
using System;

namespace Delivery.UserUI
{
    public partial class AccountEdit : UserBasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            btnEdit.Click += bntEdit_Click;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = PagesTitles.UserAccountEditTitle + BackendHelper.TagToValue("page_title_part");
            OtherMethods.ActiveRightMenuStyleChanche("hlMain", this.Page);
            var user = (Users)Session["userinsession"];
            imgGravatar.ImageUrl = Gravatar.GravatarImageLink(user.Email, "180");
            lblUID.Text = user.ID.ToString();
            hlGravatarEdit.NavigateUrl = "https://ru.gravatar.com/";

            if (!IsPostBack)
            {
                var userFull = new Users { ID = user.ID };
                userFull.GetById();
                tbContactPhoneNumbers.Text = userFull.Phone;
                tbEmail.Text = userFull.Email;
                tbLogin.Text = userFull.Login;
            }
        }

        public void bntEdit_Click(Object sender, EventArgs e)
        {
            var userSession = (Users)Session["userinsession"];
            var user = new Users
            {
                ID = userSession.ID
            };
            user.GetById();

            lblError.Text = String.Empty;
            var loginCorrectly = UsersHelper.UserLoginChecker(tbLogin.Text.Trim());
            var emailCorrectly = UsersHelper.UserEmailChecker(tbEmail.Text.Trim());

            var registrationPosible = true;
            if (!emailCorrectly && user.Email != tbEmail.Text)
            {
                lblError.Text += "Пользователь с таким e-mail'ом уже есть в нашей базе!<br/>";
                registrationPosible = false;
            }

            if (!loginCorrectly && user.Login != tbLogin.Text)
            {
                lblError.Text += "Пользователь с таким логином уже есть в нашей базе!<br/>";
                registrationPosible = false;
            }

            //окончательная проверка
            if (!registrationPosible)
            {
                return;
            }

            user.Login = tbLogin.Text.Trim();
            user.Email = tbEmail.Text.Trim();
            user.Phone = tbContactPhoneNumbers.Text;
            user.ChangeDate = DateTime.Now;
            user.Update();

            var body = "Вы изменили личные данные. Ваш логин: " + user.Login + ", ваш е-mail: " + user.Email + ", ваш телефон: " + user.Phone;
            const string subj = "Изменение личных данных";
            EmailMethods.MailSend(subj, body, user.Email);
            Page.Response.Redirect("~/UserUI/Default.aspx");
        }
    }
}