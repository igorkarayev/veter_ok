using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Delivery.BLL.Helpers;
using Delivery.DAL.DataBaseObjects;

namespace Delivery.BLL.StaticMethods
{
    public class AuthenticationMethods
    {
        public static void CheckAccessByWhiteList(Users user, HttpContext curentContext)
        {
            //если входит менеджер - проверяем имеет ли он доступ с текущего IP.
            if (user.AccessOnlyByWhiteList == 1 && user.Role != Users.Roles.User.ToString())
            {
                var currentIp = HttpContext.Current.Request.UserHostAddress;
                var isCorrectIp = false;
                var whiteListIpList = BackendHelper.TagToValue("ip_white_list");
                var whiteListIpArray = whiteListIpList.Split(new[] { ',' });
                foreach (var arrayIp in whiteListIpArray)
                {
                    if (currentIp == arrayIp && isCorrectIp == false)
                    {
                        isCorrectIp = true;
                    }
                }
                if (!isCorrectIp) curentContext.Response.Redirect("~/usernotification/13");
            }
        }

        public static void SetUserCookie(Users user)
        {
            var prilNaklSecondAccessArray = BackendHelper.TagToValue("pril_nakl_second_access");
            var prilNaklTwoAccess = prilNaklSecondAccessArray.Split(new[] { ',' }).Any(p => p.Trim().Contains(user.ID.ToString()));

            HttpContext.Current.Session["userinsession"] = new Users
            {
                Login = user.Login,
                Email = user.Email,
                Name = user.Name,
                Family = user.Family,
                ID = user.ID,
                Role = user.Role,
                RussRole = UsersHelper.RoleToRuss(user.Role),
                IsCourse = user.IsCourse,
                PrilNaklTwoAccess = prilNaklTwoAccess,
                NotReadNews = NewsHelper.GetNotReadNews(user.ID, user.Role),
                ApiKey = user.ApiKey,
                Discount = user.Discount,
                AccessOnlyByWhiteList = user.AccessOnlyByWhiteList
            };
        }
    }
}