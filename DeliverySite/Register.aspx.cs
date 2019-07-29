using Delivery.BLL.StaticMethods;
using System;
using System.Web;
using Delivery.DAL.DataBaseObjects;

namespace Delivery
{
    public partial class Register : BasePage
    {
        public void Page_Load(Object sender, EventArgs e)
        {
            bSubmit.Click += this.bSubmit_Click;
            var user = (Users)Session["userinsession"];
            if (user != null)
            {
                Redirect(user.Role == "User" ? "~/UserUI/" : "~/ManagerUI/");
            }
        }

        public void bSubmit_Click(Object sender, EventArgs e)
        {
            Msg.Text = "";
            int err = 0;
            if (tbUserLogin.Text.Length == 0)
            {
                Msg.Text += "Введите логин<br/>";
                err++;
            }

            if (tbUserPass.Text.Length == 0)
            {
                Msg.Text += "Введите пароль<br/>";
                err++;
            }

            if (tbUserEmail.Text.Length == 0)
            {
                Msg.Text += "Введите Email<br/>";
                err++;
            }

            if (tbUserPhone.Text.Length == 0)
            {
                Msg.Text += "Введите телефон<br/>";
                err++;
            }

            if (tbUserFirstName.Text.Length == 0)
            {
                Msg.Text += "Введите имя<br/>";
                err++;
            }

            if (tbUserLastName.Text.Length == 0)
            {
                Msg.Text += "Введите фамилию<br/>";
                err++;
            }

            if (err == 0)
            {
                var user = new Users();
                var userForLogin = new Users();

                var userL = new Users { Login = tbUserLogin.Text };
                userL.GetByLogin();

                var userE = new Users { Email = tbUserEmail.Text };
                userE.GetByEmail();

                if (userL.ID != 0)
                {
                    Msg.Text += "Пользователь с таким логином уже существует";
                }
                else if (userE.ID != 0)
                {
                    Msg.Text += "Пользователь с таким Email уже существует";
                }
                else
                {
                    user.Login = tbUserLogin.Text;
                    user.Password = tbUserPass.Text;
                    user.Phone = tbUserPhone.Text;
                    user.Email = tbUserEmail.Text;
                    user.Family = tbUserLastName.Text;
                    user.Name = tbUserFirstName.Text;
                    user.Role = "User";
                    user.CreateDate = DateTime.Now;
                    user.Status = 1;

                    user.Password = OtherMethods.HashPassword(user.Password);
                    user.Create();

                    userForLogin.ID = user.ID;
                    userForLogin.GetById();
                    Login(user);
                }
            }
        }


        protected void Login(Users user)
        {
            if (user.Status == 1)
            {
                Redirect("~/usernotification/4/" + user.Name);
            }
            else if (user.Status == 3)
            {
                Redirect("~/usernotification/2/" + user.Name);
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

                Redirect(user.Role == "User" ? "~/UserUI/" : "~/ManagerUI/");
            }
        }
    }
}

