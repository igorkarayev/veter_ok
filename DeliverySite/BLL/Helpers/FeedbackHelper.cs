using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net.Mime;
using System.Web;
using Delivery.DAL.DataBaseObjects;

namespace Delivery.BLL.Helpers
{
    public class FeedbackHelper
    {
        public static string CommentCountFilter(int commentsCount)
        {
            var result = commentsCount > 0 ? "<b>" + commentsCount + "</b>" : "Нет";
            return result;
        }

        public static String ColoredPriorityAndStatusRows(string priorityId, string statusId)
        {
            var result = String.Empty;
            if (String.IsNullOrEmpty(priorityId))
            {
                result = "grayRow";
            }

            if (statusId == "1")
            {
                result = "blueRow"; //red
                return result;
            }
            if (priorityId == "0") result = "grayRow"; //red
            if (priorityId == "1") result = "orangeRow"; //orange
            if (priorityId == "2") result = "redRow"; //green

            return result;
        }

        public static String ColoredCommentUserUI(int userId)
        {
            var result = String.Empty;
            var userInSession = (Users) HttpContext.Current.Session["userinsession"];
            result = userInSession.ID == userId ? "comment-div-blue" : "comment-div-green";
            return result;
        }

        public static String ColoredCommentManagerUI(int userId)
        {
            var result = String.Empty;
            var user = new Users()
            {
                ID = userId,
            };
            user.GetById();
            result = user.Role == "User" ? "comment-div-blue" : "comment-div-green";
            return result;
        }

        public static String FioFilter(string fio)
        {
            return String.IsNullOrEmpty(fio) ? "Вы" : fio;
        }

        public static String FioFilter(int userId)
        {
            var user = new Users()
            {
                ID = userId,
            };
            user.GetById();
            var roles = (List<Roles>)HttpContext.Current.Application["RolesList"];
            var userRoleRuss = String.Empty;
            userRoleRuss = user.Role == "User" ? "Клиент" : roles.FirstOrDefault(u=>u.Name == user.Role).NameOnRuss;
            return String.Format("{0} {1} {2}", userRoleRuss, user.Name, user.Family);
        }
    }
}