using System;
using Delivery.BLL.Helpers;
using Delivery.BLL.StaticMethods;
using Delivery.DAL;
using Delivery.DAL.DataBaseObjects;
using DeliverySite.Resources;

namespace Delivery.UserUI
{
    public partial class NewsFromFeedView : UserBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            OtherMethods.ActiveRightMenuStyleChanche("hlNewsFeed", this.Page);
            Page.Title = PagesTitles.ManagerNewsFromFeedView + BackendHelper.TagToValue("page_title_part");

            if (Request.Params["title"] != null)
            {
                var userInSession = (Users)Session["userinsession"];
                var news = new News { TitleUrl = Request.Params["title"].ToString() };
                news.GetByTitleUrl();
                if (!IsPostBack && news.NewsTypeID == 2)
                {
                    lblTitle.Text = news.Title;
                    lblBody.InnerHtml = news.Body;
                    lblCreateDate.Text = news.CreateDate.ToString();

                    //если новость просмотрел клиент - записываем ему просмотр и обновляем список непрочтенных новостей
                    if (userInSession.Role == Users.Roles.User.ToString())
                    {
                        var dm = new DataManager();
                        var isUserViewCount = Convert.ToInt32(
                                dm.QueryWithReturnDataSet(
                                    String.Format("select count(*) from `usertonewsview` WHERE `UserID` = {0} AND `NewsID` = {1};", userInSession.ID, news.ID)
                                ).Tables[0].Rows[0][0].ToString()
                            );
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
                }
                else
                {
                    //Response.Redirect("~/UserUI/NewsFeedView.aspx");
                }
            }
            else
            {
                //Response.Redirect("~/UserUI/NewsFeedView.aspx");
            }
        }
    }
}