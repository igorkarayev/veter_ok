using Delivery.BLL.StaticMethods;
using Delivery.DAL.DataBaseObjects;
using System;

namespace Delivery
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var param = Context.Items["sacredlink"];
            if (param == null || string.IsNullOrEmpty(param.ToString()))
            {
                Response.Redirect("~/usernotification/6");
            }
            var strParam = param.ToString();

            var base64Email = strParam.Remove(0, 32);
            byte[] byteEmail = Convert.FromBase64String(base64Email);
            var email = System.Text.Encoding.UTF8.GetString(byteEmail);
            var password = strParam.Substring(0, 32);
            var user = new Users { Email = email };
            user.GetByEmail();
            if (user.Password != password || string.IsNullOrEmpty(user.ID.ToString()))
            {
                Response.Redirect("~/usernotification/8");
            }
            lblName.Text = user.Name;
        }

        protected void btnSave_OnClick(Object sender, EventArgs e)
        {
            var param = Context.Items["sacredlink"].ToString();
            if (string.IsNullOrEmpty(param))
            {
                Response.Redirect("~/usernotification/6");
            }
            var base64Email = param.Remove(0, 32);
            byte[] byteEmail = Convert.FromBase64String(base64Email);
            var email = System.Text.Encoding.UTF8.GetString(byteEmail);
            var password = param.Substring(0, 32);
            var user = new Users { Email = email };
            user.GetByEmail();
            if (user.Password != password || string.IsNullOrEmpty(user.ID.ToString()))
            {
                Response.Redirect("~/usernotification/6");
            }
            else
            {
                user.Password = OtherMethods.HashPassword(tbNewPassword.Text);
                user.Update();
                EmailMethods.MailSend("Ваш новый пароль", "Ваш новый пароль: " + tbNewPassword.Text, user.Email);
                Response.Redirect("~/usernotification/7");
            }
        }
    }
}