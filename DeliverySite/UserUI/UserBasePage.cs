using Delivery.DAL;
using Delivery.DAL.DataBaseObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Delivery.UserUI
{
    public class UserBasePage : BasePage
    {
        public static Int32 UserID { get; set; }

        public static String ActivatedProfilesCount { get; set; }

        protected override void OnLoad(EventArgs e)
        {
            var userInSession = (Users)Session["userinsession"];
            //если нет сессии - редиректим на страницу входа
            if (userInSession == null)
            {
                Response.Redirect("~/");
            }
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());

            if (currentRole == null)
            {
                //если роли не существует и это не пользователь - на страницу входа
                if (userInSession.Role != Users.Roles.User.ToString())
                {
                    Response.Redirect("~/");
                }

            }
            else
            {
                //если у роли нет прав на просмотр раздела - редиректим на ошибку
                if (currentRole.SectionUser != 1)
                {
                    Response.Redirect("~/Error.aspx?id=1");
                }
            }

            //блок инициализации айди юзера и доступа к страницам на основании активированых профилей
            if (userInSession != null && userInSession.Role != Users.Roles.User.ToString())
            {
                UserID = 1;
            }
            else
            {
                UserID = userInSession.ID;
            }

            var dm = new DataManager();
            ActivatedProfilesCount = dm.QueryWithReturnDataSet(String.Format("SELECT COUNT(*) FROM `usersprofiles` WHERE `UserID` = {0} AND `StatusID` = 1", UserID)).Tables[0].Rows[0][0].ToString();

            base.OnLoad(e);
        }
    }
}