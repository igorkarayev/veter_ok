using Delivery.BLL.StaticMethods;
using Delivery.DAL.DataBaseObjects;
using System;
using System.Web;

namespace Delivery
{
    public class BasePage : System.Web.UI.Page
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            //òóò ïðîâåðÿåì, åëè ñåèÿ ðàçðóøåíà îîòâåòñòâóþò ëè êóêè íóæíîìó ïîëüçîâàòåëþ. Åñëè ñîîòâåòòâóþò - ïîäíèìàåì ïîëüçîâàòåëÿ. Òàêèì îáðàçîì ïî êóêè ñåèÿ æèâåò ïîêà êóêè âåðû.
            var user = (Users)Session["userinsession"];
            if (user == null)
            {
                var httpCookie = Request.Cookies["_AUTHGRB"];
                if (httpCookie != null)
                {
                    if (httpCookie.Value.Length > 33)
                    {
                        var authCookie = httpCookie.Value;
                        var base64Login = authCookie.Remove(0, 32);
                        byte[] byteLogin = Convert.FromBase64String(base64Login);
                        var login = System.Text.Encoding.UTF8.GetString(byteLogin);
                        var password = authCookie.Substring(0, 32);
                        var userOld = new Users { Login = login };
                        userOld.GetByLogin();
                        if ((login == userOld.Login) && (password == userOld.Password))
                        {
                            if (userOld.Status == 1)
                            {
                                //ðàçðóøàåì êóêè
                                var cookie = new HttpCookie("_AUTHGRB")
                                {
                                    Expires = DateTime.Now.AddDays(-1000)
                                };
                                Response.Cookies.Add(cookie); //ðàçðóøàåì ñåññèþ
                                Session["userinsession"] = null;
                                Response.Redirect("~/");
                            }
                            else
                            {
                                //проверка на доступ по WhiteList
                                AuthenticationMethods.CheckAccessByWhiteList(userOld, HttpContext.Current);

                                //обновляем\задаем авторизационную куку с данными пользователя
                                AuthenticationMethods.SetUserCookie(userOld);
                            }
                        }
                    }
                }
            }
        }

        protected void Redirect(string url)
        {
            Page.Response.Redirect(url, false);
            Context.ApplicationInstance.CompleteRequest();
        }
    }
}

