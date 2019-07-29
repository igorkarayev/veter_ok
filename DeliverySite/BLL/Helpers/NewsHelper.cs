using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mime;
using System.Web;
using Delivery.DAL;
using Delivery.DAL.DataBaseObjects;

namespace Delivery.BLL.Helpers
{
    public class NewsHelper
    {
        public static String NewsTypeToText(int val)
        {
            return DAL.DataBaseObjects.News.NewsTypes.Where(u => u.Key == val).Select(p => p.Value.ToString()).First();
        }

        public static List<News> GetNotReadNews(int userId, string userRole)
        {
            var result = new List<News>();
            var dm = new DataManager();
            //выбираем новости в соответствии с ролью  (клиент или сотрудник)
            var newsType = "1";
            if (userRole == Users.Roles.User.ToString())
            {
                newsType = "2";
            }
            var newsListTable =
                dm.QueryWithReturnDataSet(
                    String.Format(
                    "SELECT N.`ID`, N.`Title`, N.`CreateDate`, N.`TitleUrl` " +
                    "FROM `news` N JOIN `users` U " +
                    "WHERE N.`ForViewing` = 1 " +
                    "AND U.`ID` = {0} " + 
                    "AND N.`NewsTypeID` = {1} " +
                    "AND (N.`CreateDate` BETWEEN U.`CreateDate` AND NOW()) " +
                    "AND N.`ID` NOT IN (SELECT UN.`NewsID` FROM `usertonewsview` UN WHERE UN.`UserID` = {0} ) " +
                    "ORDER BY N.`ID` DESC;",
                    userId, newsType)).Tables[0];
            foreach (DataRow row in newsListTable.Rows)
            {
                var news = new News
                {
                    ID = Convert.ToInt32(row[0].ToString()),
                    Title = row[1].ToString(),
                    CreateDate = Convert.ToDateTime(row[2].ToString()),
                    TitleUrl = row[3].ToString(),
                };
                result.Add(news);
            }
            return result;
        }
    }
}