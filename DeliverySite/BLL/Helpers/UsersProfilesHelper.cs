using Delivery.DAL.DataBaseObjects;
using System;
using System.Linq;

namespace Delivery.BLL.Helpers
{
    public class UsersProfilesHelper
    {
        public static String UserProfileIDToFamilyOrCompanyname(string id)
        {
            string result;
            if (String.IsNullOrEmpty(id))
            {
                result = "";
            }
            else
            {
                var userProfile = new UsersProfiles() { ID = Convert.ToInt32(id) };
                userProfile.GetById();
                result = userProfile.TypeID == 1 ? userProfile.LastName + " " + userProfile.FirstName : userProfile.CompanyName;

            }
            return result;
        }

        public static String UserProfileIDToFullFamilyOrCompanyname(string id)
        {
            string result;
            if (String.IsNullOrEmpty(id))
            {
                result = "";
            }
            else
            {
                var userProfile = new UsersProfiles() { ID = Convert.ToInt32(id) };
                userProfile.GetById();
                result = userProfile.TypeID == 1 ? userProfile.FirstName + " " + userProfile.LastName + " " + userProfile.ThirdName : userProfile.CompanyName;

            }
            return result;
        }

        public static String UserProfileToPhone(string id)
        {
            string result;
            if (String.IsNullOrEmpty(id))
            {
                result = "";
            }
            else
            {
                var userProfile = new UsersProfiles() { ID = Convert.ToInt32(id) };
                userProfile.GetById();
                result = userProfile.ContactPhoneNumbers;

            }
            return result;
        }

        public static String UserProfileTypeToText(int val)
        {
            return val == 0 ? "Не определен" : UsersProfiles.ProfileType.Where(u => u.Key == val).Select(p => p.Value.ToString()).First();
        }

        public static string UserTypeToStr(string type)
        {
            var result = String.Empty;
            switch (type)
            {
                case "1":
                    result = "Физ.";
                    break;
                case "2":
                    result = "Юр.";
                    break;
                case "3":
                    result = "Инт.маг.";
                    break;
                default:
                    result = "Не определен.";
                    break;
            }
            return result;
        }

        public static string UserTypeToStr2(string type)
        {
            var result = String.Empty;
            switch (type)
            {
                case "1":
                    result = "Ф";
                    break;
                case "2":
                    result = "Ю";
                    break;
                case "3":
                    result = "П";
                    break;
                default:
                    result = "?";
                    break;
            }
            return result;
        }

        public static int? UserProfileIdToType(int id)
        {
            int? result;
            if (String.IsNullOrEmpty(id.ToString()))
            {
                result = 0;
            }
            else
            {
                var userProfile = new UsersProfiles() { ID = Convert.ToInt32(id) };
                userProfile.GetById();
                result = userProfile.TypeID;

            }
            return result;
        }

        public static String UserProfileStatusToText(int val)
        {
            return UsersProfiles.ProfileStatuses.Where(u => u.Key == val).Select(p => p.Value.ToString()).First();
        }

        public static string ContactFioToString(string fio)
        {
            return fio == "1" ? string.Empty : fio;
        }

        public static string GetAgreementData(string date, string number)
        {
            try
            {
                date = Convert.ToDateTime(date).ToString("yyyy.MM.dd");
            }
            catch (Exception)
            {
                return string.Empty;
            }

            if (string.IsNullOrEmpty(number))
                return string.Empty;
            return string.Format("{0} от {1}", number, date);
        }
    }
}