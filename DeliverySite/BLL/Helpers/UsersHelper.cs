using Delivery.DAL.DataBaseObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Delivery.BLL.Helpers
{
    public class UsersHelper
    {
        public static Boolean UserLoginEmailRegisteredChecker(string login)
        {
            var user = new Users { Login = login };
            user.GetByLogin();

            var user2 = new Users { Email = login };
            user2.GetByEmail();

            return user.ID == 0 && user2.ID == 0;
        }

        public static Boolean UserLoginChecker(string login)
        {
            var user = new Users { Login = login };
            user.GetByLogin();

            return user.ID == 0;
        }

        public static Int32 GetUserIdByUserProfileId(int userProfileId)
        {
            var result = 0;
            var userProfile = new UsersProfiles() { ID = userProfileId };
            userProfile.GetById();
            try
            {
                result = Convert.ToInt32(userProfile.UserID);
            }
            catch (Exception)
            {

            }
            return result;
        }

        public static Boolean UserEmailChecker(string email)
        {
            var user = new Users { Email = email };
            user.GetByEmail();

            return user.ID == 0;
        }

        public static bool AllowRoles(string role, bool admin, bool manager, bool opperator, bool cashier, bool booker, bool user)
        {

            if (role == Users.Roles.Admin.ToString() && admin)
            {
                return true;
            }

            if (role == Users.Roles.Manager.ToString() && manager)
            {
                return true;
            }

            if (role == Users.Roles.Operator.ToString() && opperator)
            {
                return true;
            }

            if (role == Users.Roles.Cashier.ToString() && cashier)
            {
                return true;
            }

            if (role == Users.Roles.Booker.ToString() && booker)
            {
                return true;
            }

            if (role == Users.Roles.User.ToString() && user)
            {
                return true;
            }

            return false;
        }

        public static String RoleToRuss(string role)
        {
            var rolesList = HttpContext.Current.Application["RolesList"] as List<Roles>;
            var singleOrDefault = rolesList.SingleOrDefault(u => u.Name.ToLower() == role.ToLower());
            if (singleOrDefault != null)
            {
                var rusRole = singleOrDefault.NameOnRuss;
                return rusRole;
            }
            return "Клиент";
        }

        public static String UserIDToFullName(string id)
        {
            string result;
            if (String.IsNullOrEmpty(id))
            {
                result = "";
            }
            else
            {
                var user = new Users { ID = Convert.ToInt32(id) };
                user.GetById();
                result = user.Name + " " + user.Family;
            }

            return result;
        }

        public static String UserIDToPhone(string id)
        {
            string result;
            if (String.IsNullOrEmpty(id))
            {
                result = "";
            }
            else
            {
                var user = new Users { ID = Convert.ToInt32(id) };
                user.GetById();
                result = user.Phone;
            }

            return result;
        }

        public static String UserIDToName(string id)
        {
            string result;
            if (String.IsNullOrEmpty(id))
            {
                result = "";
            }
            else
            {
                var user = new Users { ID = Convert.ToInt32(id) };
                user.GetById();
                result = user.Name;
            }

            return result;
        }

        public static String UserStatusToText(int val)
        {
            return Users.UserStatuses.Where(u => u.Key == val).Select(p => p.Value.ToString()).First();
        }

        public static String UserStatusStadyToText(int val)
        {
            return Users.UserStatusesStudy.Where(u => u.Key == val).Select(p => p.Value.ToString()).First();
        }

        public static String UserColoredStatusRows(string id)
        {
            var result = String.Empty;
            if (String.IsNullOrEmpty(id))
            {
                result = "generalRow";
            }

            if (id == "3") result = "redRow"; //red
            if (id == "1") result = "grayRow"; //yellow
            if (id == "2") result = "greenRow"; //green

            return result;
        }

        public static String UserProfileColoredStatusRows(string userStatusId, string profileStatusId)
        {
            var result = String.Empty;
            if (String.IsNullOrEmpty(userStatusId) || string.IsNullOrEmpty(profileStatusId))
            {
                result = "generalRow";
            }

            if (userStatusId != "2")
                return UserColoredStatusRows(userStatusId);

            if (profileStatusId == "3") result = "redRow"; //block
            if (profileStatusId == "0") result = "grayRow"; //not proc
            if (profileStatusId == "1") result = "greenRow"; //activ
            if (profileStatusId == "2") result = "yellowRow"; //reject

            return result;
        }


        public static Users GetUserAttachedManager(int userId)
        {
            if (String.IsNullOrEmpty(userId.ToString()))
            {
                return null;
            }
            var user = new Users { ID = userId };
            user.GetById();
            var manager = new Users { ID = Convert.ToInt32(user.ManagerID) };
            manager.GetById();
            return String.IsNullOrEmpty(manager.ID.ToString()) ? null : manager.GetById();
        }

        public static Users GetUserAttachedSalesManager(int userId)
        {
            if (String.IsNullOrEmpty(userId.ToString()))
            {
                return null;
            }
            var user = new Users { ID = userId };
            user.GetById();
            var salesManager = new Users { ID = Convert.ToInt32(user.SalesManagerID) };
            salesManager.GetById();
            return String.IsNullOrEmpty(salesManager.ID.ToString()) ? null : salesManager.GetById();
        }

        public static String SalesManagerName(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return String.Empty;
            }
            var manager = new Users() { ID = Convert.ToInt32(id) };
            manager.GetById();
            if (manager.ID == 0
                || String.IsNullOrEmpty(manager.ID.ToString())
                || String.IsNullOrEmpty(manager.Family)
                || String.IsNullOrEmpty(manager.Name)
                || String.IsNullOrEmpty(manager.Role))
            {
                return String.Empty;
            }
            return String.Format("{0} {1}", manager.Family, manager.Name);
        }

        public static string TicketsCountFormater(string count)
        {
            if (count == "0")
            {
                return string.Empty;
            }
            return count;
        }

        public static string Segment(string ticketCount, string summGruzobozCost)
        {
            var result = "A";
            if (string.IsNullOrEmpty(ticketCount) || string.IsNullOrEmpty(summGruzobozCost))
                return result;

            int count = 0, summ = 0;
            Int32.TryParse(ticketCount, out count);
            Int32.TryParse(summGruzobozCost, out summ);

            if ((count >= 0 && count <= 50) || (summ >= 0 && summ <= 5000000))
                result = "A";
            if ((count > 50 && count <= 100) || (summ > 5000000 && summ <= 10000000))
                result = "B";
            if ((count > 100 && count <= 200) || (summ > 10000000 && summ <= 15000000))
                result = "C";
            if (count > 200 || summ > 15000000)
                result = "V";
            return result;
        }
    }
}