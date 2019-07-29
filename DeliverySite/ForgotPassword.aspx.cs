using Delivery.BLL.Helpers;
using Delivery.DAL.DataBaseObjects;
using System;
using EmailMethods = Delivery.BLL.StaticMethods.EmailMethods;

namespace Delivery
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRemember_OnClick(Object sender, EventArgs e)
        {
            var user = new Users { Email = tbEmail.Text };
            user.GetByEmail();
            if (!string.IsNullOrEmpty(user.Password) && user.Status != 1)
            {
                var currentAppAddress = BackendHelper.TagToValue("current_admin_app_address");
                byte[] bytEmail = System.Text.Encoding.UTF8.GetBytes(user.Email);
                var link = user.Password + Convert.ToBase64String(bytEmail);
                var text = String.Format("Ваш логин: {0}.<br/> Ссылка на восстановление пароля: <a href=\"http://{2}/changepassword/{1}\">http://{2}/changepassword/{1}</a>", user.Login, link, currentAppAddress);
                EmailMethods.MailSendHTML("Восстановление логина и пароля", text, tbEmail.Text);
                Response.Redirect("~/usernotification/5");
            }
            else
            {
                if (!string.IsNullOrEmpty(user.Password) && user.Status == 1)
                {
                    lblError.Visible = true;
                    lblError.Text = "Пользователь еще не активирован!";
                }
                else
                {
                    lblError.Visible = true;
                    lblError.Text = "Пользователь с таким Email не существует!";
                }
            }

        }
    }
}