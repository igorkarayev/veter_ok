using Delivery.BLL.Helpers;
using Delivery.DAL.DataBaseObjects;
using System;
using System.Web;

namespace Delivery.UserUI.Controls
{
    public partial class MainMenu : System.Web.UI.UserControl
    {
        protected String AppKey { get; set; }

        protected String UserID { get; set; }

        protected void Page_Init(object sender, EventArgs e)
        {
            var page = new Pages() { ID = 6 };
            page.GetById();
            hlFAQ.Text = page.PageTitle;
            page.ID = 7;
            page.GetById();
            hlDeveloper.Text = page.PageTitle;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            AppKey = Globals.Settings.AppServiceSecureKey;
            var userInSession = (Users)Session["userinsession"];
            UserID = userInSession.ID.ToString();
            if (userInSession.Role != Users.Roles.User.ToString())
            {
                hlManagerUI.Visible = true;
                liFeedback.Visible = false;
            }

            var activatedProfilesCount = UserBasePage.ActivatedProfilesCount;
            if (String.IsNullOrEmpty(activatedProfilesCount) || Convert.ToInt32(activatedProfilesCount) == 0)
            {
                liTickets.Visible = false;
                liDeveloper.Visible = false;
                liFeedback.Visible = false;
            }

            if (BackendHelper.TagToValue("want_payment_enabled") != "true")
            {
                liWantPayment.Visible = false;
            }
            var page = new Pages { ID = 6 };
            page.GetById();
            hlFAQ.Text = page.PageTitle;
            hlSite.NavigateUrl = string.Format("http://{0}", BackendHelper.TagToValue("current_app_address"));
        }

        public void Logoff(object sender, EventArgs e)
        {
            var cookie = new HttpCookie("_AUTHGRB")
            {
                Expires = DateTime.Now.AddDays(-1000)
            };
            Response.Cookies.Add(cookie);
            Session["userinsession"] = null;
            Response.Redirect("~/");
        }
    }
}

