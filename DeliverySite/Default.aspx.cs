using Delivery.BLL.StaticMethods;
using Delivery.DAL.DataBaseObjects;
using System;
using System.Web;

namespace Delivery
{
    public partial class Default : BasePage
    {
        public void Page_Load(Object sender, EventArgs e)
        {
            bSubmit.Click += this.bSubmit_Click;
            var user = (Users)Session["userinsession"];
            if (user != null)
            {
                Response.Redirect(user.Role == "User" ? "~/UserUI/" : "~/ManagerUI/");
            }
        }

        public void bSubmit_Click(Object sender, EventArgs e)
        {
            var user = new Users { Login = tbUserLogin.Text };
            var userForLogin = new Users();
            user.GetByLogin();
            if (user.ID != 0)
            {
                userForLogin.ID = user.ID;
                userForLogin.GetById();
            }
            if (String.Equals(tbUserLogin.Text, userForLogin.Login, StringComparison.CurrentCultureIgnoreCase) && (OtherMethods.HashPassword(tbUserPass.Text) == userForLogin.Password))
            {
                Login(userForLogin);
            }
            else
            {
                if (String.Equals(tbUserLogin.Text, userForLogin.Login, StringComparison.CurrentCultureIgnoreCase) && (OtherMethods.HashPassword(tbUserPass.Text) != userForLogin.Password))
                {
                    Msg.Text = "Вы ввели неверный пароль!";
                }
                else
                {
                    Msg.Text = "Пользователь с таким логином не найден!";
                }
            }
        }


        protected void Login(Users user)
        {
            if (user.Status == 1)
            {
                Response.Redirect("~/usernotification/4/" + user.Name);
            }
            else if (user.Status == 3)
            {
                Response.Redirect("~/usernotification/2/" + user.Name);
            }
            else
            {
                //проверка на доступ по WhiteList
                AuthenticationMethods.CheckAccessByWhiteList(user, HttpContext.Current);

                //обновляем\задаем авторизационную куку с данными пользователя
                AuthenticationMethods.SetUserCookie(user);

                //задаем авторизовачные куки для поднятия пользователя при крахе сеcии.
                byte[] bytLogin = System.Text.Encoding.UTF8.GetBytes(user.Login);
                var cookie = new HttpCookie("_AUTHGRB")
                {
                    Value = user.Password + Convert.ToBase64String(bytLogin),
                    Expires = cbRememberMe.Checked ? DateTime.Now.AddDays(15) : DateTime.Now.AddMinutes(60)
                };

                Response.Cookies.Add(cookie);

                Response.Redirect(user.Role == "User" ? "~/UserUI/" : "~/ManagerUI/");
            }
        }
    }
}

