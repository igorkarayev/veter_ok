using Delivery.BLL;
using Delivery.BLL.StaticMethods;
using Delivery.DAL;
using Delivery.DAL.DataBaseObjects;
using DeliverySite.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using Delivery.BLL.Helpers;

namespace Delivery.ManagerUI
{
    public partial class Default : ManagerBasePage
    {
        public void Page_Load(object sender, EventArgs e)
        {
            Page.Title = PagesTitles.ManagerDefaultTitle + BackendHelper.TagToValue("page_title_part");
            OtherMethods.ActiveRightMenuStyleChanche("hlMain", this.Page);
            var user = (Users)Session["userinsession"];

            lblUserName.Text = user.Name;
            lblUID.Text = user.ID.ToString();
            lblLogin.Text = user.Login;
            lblEmail.Text = user.Email;
            lblRole.Text = user.RussRole;

            var userInSession = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());

            if (currentRole.Name == Users.Roles.SuperAdmin.ToString())
            {
                lbRestartSubMemcache.Visible = true;
            }
            var dm = new DataManager();
            lblAllUsers.Text =
                MoneyMethods.MoneySeparator(dm.QueryWithReturnDataSet("SELECT COUNT(*) FROM `users` WHERE `Role` = 'User';").Tables[0].Rows[0][0].ToString());
            lblBlockedUsers.Text =
                MoneyMethods.MoneySeparator(dm.QueryWithReturnDataSet("SELECT COUNT(*) FROM `users` WHERE `Role` = 'User' AND `Status` = 3;").Tables[0].Rows[0][0].ToString());
            lblActiveUsers.Text =
                MoneyMethods.MoneySeparator(dm.QueryWithReturnDataSet("SELECT COUNT(*) FROM `users` WHERE `Role` = 'User' AND `Status` = 2;").Tables[0].Rows[0][0].ToString());
            lblNewUsers.Text =
                MoneyMethods.MoneySeparator(dm.QueryWithReturnDataSet("SELECT COUNT(*) FROM `users` WHERE `Role` = 'User' AND `Status` = 1;").Tables[0].Rows[0][0].ToString());
            pnlUsers.Visible = true;

            if (currentRole.PageUserTicketView == 1 || currentRole.PageUserTicketNotProcessedView == 1 || currentRole.PageUserTicketByDeliveryOnMinsk == 1 || currentRole.PageUserTicketByDeliveryOnBelarus == 1)
            {
                lblTicketsAll.Text =
                    MoneyMethods.MoneySeparator(dm.QueryWithReturnDataSet("select count(*) from `tickets`;").Tables[0].Rows[0][0].ToString());
                lblCreateToday.Text =
                    MoneyMethods.MoneySeparator(dm.QueryWithReturnDataSet(String.Format("select count(*) from `tickets` WHERE (`CreateDate` > '{0}' AND `CreateDate` < '{1}' );", DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.AddDays(1).ToString("yyyy-MM-dd"))).Tables[0].Rows[0][0].ToString());
                lblNewToday.Text =
                    MoneyMethods.MoneySeparator(dm.QueryWithReturnDataSet(String.Format("select count(*) from `tickets` WHERE (`CreateDate` > '{0}' AND `CreateDate` < '{1}' AND `StatusID` = 1);", DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.AddDays(1).ToString("yyyy-MM-dd"))).Tables[0].Rows[0][0].ToString());
                lblNewAll.Text =
                    MoneyMethods.MoneySeparator(dm.QueryWithReturnDataSet("select count(*) from `tickets` WHERE `StatusID` = 1;").Tables[0].Rows[0][0].ToString());
                lblDeliveryToday.Text =
                    MoneyMethods.MoneySeparator(dm.QueryWithReturnDataSet(String.Format("select count(*) from `tickets` WHERE (`DeliveryDate` = '{0}');", DateTime.Now.ToString("yyyy-MM-dd"))).Tables[0].Rows[0][0].ToString());
                lblDeliveryTomorow.Text =
                    MoneyMethods.MoneySeparator(dm.QueryWithReturnDataSet(String.Format("select count(*) from `tickets` WHERE (`DeliveryDate` = '{0}');", DateTime.Now.AddDays(1).ToString("yyyy-MM-dd"))).Tables[0].Rows[0][0].ToString());
                lblInProgress.Text =
                    MoneyMethods.MoneySeparator(dm.QueryWithReturnDataSet("select count(*) from `tickets` WHERE `StatusID` = 3;").Tables[0].Rows[0][0].ToString());
                lblDelivered.Text =
                    MoneyMethods.MoneySeparator(dm.QueryWithReturnDataSet("select count(*) from `tickets` WHERE `StatusID` = 12;").Tables[0].Rows[0][0].ToString());
                lblProcessed.Text =
                    MoneyMethods.MoneySeparator(dm.QueryWithReturnDataSet("select count(*) from `tickets` WHERE `StatusID` = 5;").Tables[0].Rows[0][0].ToString());
                lblCompleted.Text =
                    MoneyMethods.MoneySeparator(dm.QueryWithReturnDataSet("select count(*) from `tickets` WHERE `StatusID` = 6;").Tables[0].Rows[0][0].ToString());
                pnlTickets.Visible = true;
            }

            if (currentRole.PageCategoryView == 1)
            {
                lblTitles.Text =
                    MoneyMethods.MoneySeparator(dm.QueryWithReturnDataSet("select count(*) from `titles`;").Tables[0].Rows[0][0].ToString());
                pnlCategory.Visible = true;
            }

            if (currentRole.PageCityView == 1)
            {
                lblCity.Text =
                    MoneyMethods.MoneySeparator(dm.QueryWithReturnDataSet("select count(*) from `city`;").Tables[0].Rows[0][0].ToString());
                pnlCity.Visible = true;
            }

            if (currentRole.PageDriversView == 1)
            {
                lblDrivers.Text =
                    MoneyMethods.MoneySeparator(dm.QueryWithReturnDataSet("SELECT COUNT(*) FROM `drivers` WHERE `StatusID` = 1;").Tables[0].Rows[0][0].ToString());
                pnlDrivers.Visible = true;
            }

            if (currentRole.PageManagersView == 1)
            {
                lblManagers.Text =
                    MoneyMethods.MoneySeparator(dm.QueryWithReturnDataSet("SELECT COUNT(*) FROM `users` WHERE `Role` <> 'User';").Tables[0].Rows[0][0].ToString());
                pnlManagers.Visible = true;
            }

            if (currentRole.PageNewsView == 1)
            {
                lblNews.Text =
                    MoneyMethods.MoneySeparator(dm.QueryWithReturnDataSet("select count(*) from `news`;").Tables[0].Rows[0][0].ToString());
                pnlNews.Visible = true;
            }

            if (currentRole.PageErrorsLogView == 1)
            {
                lblErrors.Text =
                    MoneyMethods.MoneySeparator(dm.QueryWithReturnDataSet("select count(*) from `errorslog`;").Tables[0].Rows[0][0].ToString());
                pnlErrors.Visible = true;
            }

            if (currentRole.PageFeedbacksView == 1)
            {
                lblFeedback.Text =
                    MoneyMethods.MoneySeparator(dm.QueryWithReturnDataSet("SELECT COUNT(*) FROM `feedback` WHERE StatusID = 0;").Tables[0].Rows[0][0].ToString());
                pnlFeedback.Visible = true;
            }

            if (currentRole.PageTracksView == 1)
            {
                lblTracks.Text =
                    MoneyMethods.MoneySeparator(dm.QueryWithReturnDataSet("select count(*) from `tracks`;").Tables[0].Rows[0][0].ToString());
                pnlTracks.Visible = true;
            }

            if (currentRole.PageLogsView == 1)
            {
                lblLogs.Text =
                    MoneyMethods.MoneySeparator(dm.QueryWithReturnDataSet("select count(*) from `userslog`;").Tables[0].Rows[0][0].ToString());
                pnlLogs.Visible = true;
            }

            if (currentRole.PageApiLogView == 1)
            {
                lblApiLogs.Text =
                    MoneyMethods.MoneySeparator(dm.QueryWithReturnDataSet("select count(*) from `apilog`;").Tables[0].Rows[0][0].ToString());
                pnlApiLogs.Visible = true;
            }

            if (currentRole.PageClientsView == 1)
            {
                lblCategory.Text =
                    MoneyMethods.MoneySeparator(dm.QueryWithReturnDataSet("select count(*) from `category`;").Tables[0].Rows[0][0].ToString());
                pnlClients.Visible = true;
            }

            lblProvidersView.Text =
                MoneyMethods.MoneySeparator(dm.QueryWithReturnDataSet("select count(*) from `providers`;").Tables[0].Rows[0][0].ToString());

            var lastNews = dm.QueryWithReturnDataSet("SELECT `CreateDate`, `Title`, `Body`, `TitleUrl` FROM `news` WHERE `NewsTypeID` = '1' ORDER BY ID DESC LIMIT 1;").Tables[0];
            if (lastNews.Rows.Count != 0)
            {
                lblNewsDate.Text = Convert.ToDateTime(lastNews.Rows[0][0]).ToString("dd.MM.yyyy");
                lblNewsTitle.Text = lastNews.Rows[0][1].ToString();
                var body = lastNews.Rows[0][2].ToString();
                if (body.Length > 300)
                {
                    lblNewsText.Text = body.Remove(300, body.Length - 300) + "...";
                }
                else
                {
                    lblNewsText.Text = body;
                }
                hlNewsTitle.NavigateUrl = string.Format("~/ManagerUI/Menu/NewsFeed/NewsFromFeedView.aspx?title={0}", lastNews.Rows[0][3].ToString());
            }

            //пересчитываем просмотренные новости
            //обновляем\задаем авторизационную куку с данными пользователя
            AuthenticationMethods.SetUserCookie(userInSession);

            //механизм нотификаций
            lblStatus.Text = String.Empty;
            if (Session["flash:now"] != null && Session["flash:now"].ToString() != String.Empty)
            {
                lblStatus.Text = Session["flash:now"].ToString();
                Session["flash:now"] = String.Empty;
            }
        }

        public void lbRestartSubMemcache_Click(Object sender, EventArgs e)
        {
            //загружаем роли в оперативную память
            var roles = new Roles();
            Application["RolesList"] = roles.GetAllItemsToList();

            //загружаем города в оперативную память
            var city = new City();
            Application["CityList"] = city.GetAllItemsToList();

            //загружаем backend в оперативную память
            var backend = new Backend();
            Application["BackendList"] = backend.GetAllItemsToList();
        }
    }
}