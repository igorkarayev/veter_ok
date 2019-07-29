using Delivery.BLL.Helpers;
using Delivery.BLL.StaticMethods;
using Delivery.DAL;
using Delivery.DAL.DataBaseObjects;
using DeliverySite.Resources;
using System;

namespace Delivery.ManagerUI.Menu.NewsFeed
{
    public partial class NewsFromFeedView : ManagerBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            OtherMethods.ActiveRightMenuStyleChanche("hlNewsFeed", this.Page);
            Page.Title = PagesTitles.ManagerNewsFromFeedView + BackendHelper.TagToValue("page_title_part");

            var userInSession = (Users)Session["userinsession"];
            if (Request.Params["title"] != null)
            {
                var news = new News { TitleUrl = Request.Params["title"].ToString() };
                news.GetByTitleUrl();
                if (!IsPostBack && news.NewsTypeID == 1)
                {
                    lblTitle.Text = news.Title;
                    lblBody.InnerHtml = news.Body;
                    lblCreateDate.Text = news.CreateDate.ToString();
                    var dm = new DataManager();
                    var isUserViewCount = Convert.ToInt32(MoneyMethods.MoneySeparator(
                            dm.QueryWithReturnDataSet(
                                String.Format("select count(*) from `usertonewsview` WHERE `UserID` = {0} AND `NewsID` = {1};", userInSession.ID, news.ID)
                            ).Tables[0].Rows[0][0].ToString()
                        ));

                    //записываем данные о инфе, что усер просмотрел новость
                    if (isUserViewCount == 0)
                    {
                        var userView = new UserToNewsView { UserID = userInSession.ID, NewsID = news.ID };
                        userView.Create();
                    }

                    //пересчитываем просмотренные новости
                    //обновляем\задаем авторизационную куку с данными пользователя
                    AuthenticationMethods.SetUserCookie(userInSession);
                }
                else
                {
                    Response.Redirect("~/ManagerUI/Menu/NewsFeed/NewsFeedView.aspx");
                }
            }
            else
            {
                Response.Redirect("~/ManagerUI/Menu/NewsFeed/NewsFeedView.aspx");
            }
        }
    }
}