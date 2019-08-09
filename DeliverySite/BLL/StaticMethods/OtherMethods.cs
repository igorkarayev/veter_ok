using Delivery.BLL.Helpers;
using Delivery.DAL.DataBaseObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Delivery.BLL.StaticMethods
{
    public class OtherMethods
    {
        public static void ActiveRightMenuStyleChanche(string id, Page page)
        {
            if (page.Master != null)
            {
                var menu = page.Master.FindControl("MainMenu");
                var hyperLink = (HyperLink)menu.FindControl(id);
                hyperLink.CssClass += " active";

            }
        }

        public static String TrackToText(int val)
        {
            if (val == 0)
            {
                return String.Empty;
            }
            var track = new Tracks { ID = val };
            track.GetById();
            return track.Name;
        }

        public static String DisplayHelper(int? val)
        {
            if (val == 1)
            {
                return "displayColumn";
            }
            else
            {
                return "notDisplayColumn";
            }
        }

        public static String GetRecipientAddressWithoutStreet(string ticketId)
        {
            var ticket = new Tickets { ID = Convert.ToInt32(ticketId) };
            ticket.GetById();
            var result = ticket.RecipientStreetNumber;
            if (ticket.RecipientKorpus != String.Empty && ticket.RecipientKorpus != " ")
            {
                result += "/" + ticket.RecipientKorpus;
            }
            if (ticket.RecipientKvartira != String.Empty && ticket.RecipientKvartira != " ")
            {
                result += " кв." + ticket.RecipientKvartira;
            }
            return result;
        }

        public static Dictionary<int, string> GetRecipientAddress(string ticketId)
        {
            var ticket = new Tickets { ID = Convert.ToInt32(ticketId) };
            ticket.GetById();
            var result = new Dictionary<int, string>(5);

            if (ticket.RecipientStreetPrefix != String.Empty && ticket.RecipientStreetPrefix != " ")
            {
                result.Add(0, ticket.RecipientStreetPrefix);
            }
            if (ticket.RecipientStreet != String.Empty && ticket.RecipientStreet != " ")
            {
                result.Add(1, ticket.RecipientStreet);
            }
            if (ticket.RecipientStreetNumber != String.Empty && ticket.RecipientStreetNumber != " ")
            {
                result.Add(2, ticket.RecipientStreetNumber);
            }
            if (ticket.RecipientKorpus != String.Empty && ticket.RecipientKorpus != " ")
            {
                result.Add(3, ticket.RecipientKorpus);
            }
            if (ticket.RecipientKvartira != String.Empty && ticket.RecipientKvartira != " ")
            {
                result.Add(4, ticket.RecipientKvartira);
            }
            return result;
        }

        public static String GetRecipientCourses(string ticketId)
        {
            var ticket = new Tickets { ID = Convert.ToInt32(ticketId) };
            ticket.GetById();
            var result = String.Empty;
            if (ticket.CourseUSD != 1)
            {
                result += "USD: " + ticket.CourseUSD + "<br/>";
            }
            if (ticket.CourseRUR != 1)
            {
                result += "RUR: " + ticket.CourseRUR + "<br/>";
            }
            if (ticket.CourseEUR != 1)
            {
                result += "EUR: " + ticket.CourseEUR + "<br/>";
            }
            return result;
        }


        public static String StatusDescription(string ticketId)
        {
            var ticket = new Tickets { ID = Convert.ToInt32(ticketId) };
            ticket.GetById();
            string result = String.IsNullOrEmpty(ticket.StatusDescription) ? "Не требуется" : ticket.StatusDescription;

            return result;
        }

        public static String StringCutAllNews(string body)
        {
            string result;
            if (body.Length > 600)
            {
                result = body.Remove(600);
                result += "...";
            }
            else
            {
                result = body;
            }

            return result;
        }

        public static String StringCutMenuNews(string body)
        {
            string result;
            if (body.Length > 60)
            {
                result = body.Remove(60);
                result += "...";
            }
            else
            {
                result = body;
            }

            return result;
        }

        public static bool ReadMoreVisible(string body)
        {
            return body.Length > 200;
        }

        public static string NewsMenuDateConvert(string date)
        {
            var dateTime = Convert.ToDateTime(date);
            return dateTime.ToString("dd MMMM yyyy");
        }

        public static string NewsMenuDay(string date)
        {
            var dateTime = Convert.ToDateTime(date);
            return dateTime.ToString("dd");
        }

        public static string NewsMenuMons(string date)
        {
            var dateTime = Convert.ToDateTime(date);
            return dateTime.ToString("MMMM");
        }

        public static string NewsMenuYear(string date)
        {
            var dateTime = Convert.ToDateTime(date);
            return dateTime.ToString("yyyy");
        }

        public static string DateConvert(string date)
        {
            string result;
            if (date == "01.01.0001 0:00:00" || date == String.Empty)
            {
                result = String.Empty;
            }
            else
            {
                var dateTime = Convert.ToDateTime(date);
                result = dateTime.ToString("dd.MM.yyyy");
            }
            return result;
        }

        public static string DateTimeConvert(string date)
        {
            string result;
            if (date == "01.01.0001 0:00:00" || date == String.Empty)
            {
                result = String.Empty;
            }
            else
            {
                var dateTime = Convert.ToDateTime(date);
                result = dateTime.ToString("dd-MM-yyyy HH:mm");
            }
            return result;
        }

        public static string FullDateTimeConvert(string date)
        {
            string result;
            if (date == "01.01.0001 0:00:00" || date == String.Empty)
            {
                result = String.Empty;
            }
            else
            {
                var dateTime = Convert.ToDateTime(date);
                result = dateTime.ToString("yyyy-MM-dd HH:mm:ss");
            }
            return result;
        }

        public static string FullDateTimeConvertForCity(string date)
        {
            string result;
            if (date == "01.01.0001 0:00:00" || date == String.Empty)
            {
                result = String.Empty;
            }
            else
            {
                result = Convert.ToDateTime(date).ToString("HH:mm");
            }
            return result;
        }

        public static string DateConvertWithDot(string date)
        {
            string result;
            if (date == "01.01.0001 0:00:00" || date == String.Empty)
            {
                result = String.Empty;
            }
            else
            {
                var dateTime = Convert.ToDateTime(date);
                result = dateTime.ToString("dd.MM.yyyy");
            }
            return result;
        }

        public static String CreateUniqId(string s)
        {
            //переводим строку в байт-массим  
            byte[] bytes = Encoding.Unicode.GetBytes(s);

            //создаем объект для получения средст шифрования  
            var CSP = new MD5CryptoServiceProvider();

            //вычисляем хеш-представление в байтах  
            byte[] byteHash = CSP.ComputeHash(bytes);

            string hash = String.Empty;

            //формируем одну цельную строку из массива  
            foreach (byte b in byteHash)
            {
                hash += String.Format("{0:x2}", b);
            }
            hash = hash.Substring(0, 7);
            return hash;
        }

        public static String CreateFullUniqId(string s)
        {           
            //переводим строку в байт-массим  
            byte[] bytes = Encoding.Unicode.GetBytes(s);

            //создаем объект для получения средст шифрования  
            var CSP = new MD5CryptoServiceProvider();

            //вычисляем хеш-представление в байтах  
            byte[] byteHash = CSP.ComputeHash(bytes);

            string hash = String.Empty;

            //формируем одну цельную строку из массива  
            foreach (byte b in byteHash)
            {
                hash += String.Format("{0:x2}", b);
            }
            return hash;
        }

        public static string HashPassword(string s)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(s);

            var csp = new MD5CryptoServiceProvider();

            byte[] byteHash = csp.ComputeHash(bytes);

            string hash = String.Empty;

            foreach (byte b in byteHash)
                hash += String.Format("{0:x2}", b);

            return hash;
        }

        public static String BoxesLabel(string boxesItem, string boxesNumber)
        {
            var result = String.Empty;
            if (boxesNumber == "1")
            {
                result = boxesNumber;
            }
            else
            {
                result = boxesItem + "/" + boxesNumber;
            }

            return result;
        }

        public static String LinkBuilder(string sid, string uid, string recipientPhone, string cityID, string statusID, string driverID, string deliveryDate1, string deliveryDate2, string trackID)
        {
            var link = String.Empty;

            if (!String.IsNullOrEmpty(sid))
            {
                link += "sid=" + sid + "&";
            }

            if (!String.IsNullOrEmpty(uid))
            {
                link += "uid=" + uid + "&";
            }

            if (!String.IsNullOrEmpty(recipientPhone))
            {
                link += "recipientPhone=" + recipientPhone + "&";
            }

            if (!String.IsNullOrEmpty(cityID))
            {
                link += "cityID=" + cityID + "&";
            }

            if (!String.IsNullOrEmpty(statusID))
            {
                link += "statusID=" + statusID + "&";
            }

            if (!String.IsNullOrEmpty(driverID))
            {
                link += "driverID=" + driverID + "&";
            }

            if (!String.IsNullOrEmpty(deliveryDate1))
            {
                link += "deliveryDate1=" + deliveryDate1 + "&";
            }

            if (!String.IsNullOrEmpty(deliveryDate2))
            {
                link += "deliveryDate2=" + deliveryDate2 + "&";
            }

            if (!String.IsNullOrEmpty(trackID))
            {
                link += "trackID=" + trackID + "&";
            }

            link += "stateSave=true";
            return link;
        }

        public static String ColoredProfilesRows(string userType)
        {
            var result = String.Empty;
            if (userType == "1") result = "grayRow"; //gray
            if (userType == "2") result = "blueRow"; //blue

            return result;
        }

        public static String TicketColoredStatusRows(string id)
        {
            switch (id)
            {
                case "1":
                    return "redRow";
                case "2":
                case "3":
                case "4":
                case "11":
                case "19":
                    return "yellowRow";
                case "5":
                    return "greenRow";
                case "6":
                    return "blueRow";
                case "7":
                case "8":
                case "9":
                case "10":
                    return "grayRow";
                case "12":
                    return "turquoiseRow";
                case "13":
                case "15":
                    return "purpleRow";
                case "14":
                case "16":
                    return "orangeRow";
                case "17":
                case "18":
                    return "brownRow";
                default:
                    return "generalRow";
            }
        }

        public static String TicketStatusToTextWithoutColor(string id)
        {
            return Tickets.TicketStatuses.Where(u => u.Key == Convert.ToInt32(id)).Select(p => p.Value.ToString()).First();
        }

        public static String TicketStatusToText(string id)
        {
            var statusName = Tickets.TicketStatuses.Where(u => u.Key == Convert.ToInt32(id)).Select(p => p.Value.ToString()).First();
            switch (id)
            {
                case "1":
                    return String.Format("<b class='{1}'>{0}</b>", statusName, "redText");
                case "2":
                case "3":
                case "4":
                    return String.Format("<b class='{1}'>{0}</b>", statusName, "yellowText");
                case "5":
                    return String.Format("<b class='{1}'>{0}</b>", statusName, "greenText");
                case "6":
                    return String.Format("<b class='{1}'>{0}</b>", statusName, "blueText");
                case "7":
                case "8":
                case "9":
                case "10":
                    return String.Format("<b class='{1}'>{0}</b>", statusName, "redText");
                case "11":
                    return String.Format("<b class='{1}'>{0}</b>", statusName, "yellowText");
                case "12":
                    return String.Format("<b class='{1}'>{0}</b>", statusName, "turquoiseText");
                case "13":
                    return String.Format("<b class='{1}'>{0}</b>", statusName, "orangeText");
                case "14":
                    return String.Format("<b class='{1}'>{0}</b>", statusName, "grayText");
                case "15":
                    return String.Format("<b class='{1}'>{0}</b>", statusName, "orangeText");
                case "16":
                    return String.Format("<b class='{1}'>{0}</b>", statusName, "grayText");
                default:
                    return String.Format("<b class='{1}'>{0}</b>", statusName, "generalText");
            }
        }

        public static String ColoredProfileStatusRows(string id)
        {
            var result = String.Empty;
            if (String.IsNullOrEmpty(id))
            {
                result = "generalRow";
            }

            if (id == "0") result = "grayRow";
            if (id == "1") result = "greenRow";
            if (id == "2") result = "yellowRow";
            if (id == "3") result = "redRow";

            return result;
        }

        public static String ColoredIssuanceStatusRows(string id)
        {
            var result = String.IsNullOrEmpty(id) ? "generalRow" : IssuanceListsHelper.IssuanceStatusToText(id);

            if (Convert.ToInt32(id) == 1) result = "greenRow"; //red
            if (Convert.ToInt32(id) == 3) result = "yellowRow"; //red
            if (Convert.ToInt32(id) == 2) result = "grayRow"; //gray

            return result;
        }

        public static String StringCuter(string input)
        {
            string result = input;
            if (input.Length > 30)
            {
                result = input.Substring(0, 30);
            }
            return result;
        }

        public static String StringCuterModel(string input)
        {
            string result = input;
            if (input.Length > 40)
            {
                result = input.Substring(0, 40);
            }
            return result;
        }

        public static String GoodsNumberIfEmpty(string input)
        {
            var result = input;
            if (String.IsNullOrEmpty(input))
            {
                result = "1";
            }
            return result;
        }


        public static String GoodsStringFromTicketID(string id)
        {
            var ticket = new Tickets { ID = Convert.ToInt32(id) };
            ticket.GetById();
            var result = String.Empty;
            var goods = new Goods { TicketFullSecureID = ticket.FullSecureID };
            var ds = goods.GetAllItems("ID", "ASC", "TicketFullSecureID");
            result = ds.Tables[0].Rows.Cast<DataRow>().Aggregate(result, (current, row) => current + (row["Description"] + ", " + row["Model"] + ", " + row["Number"] + "шт.;<br/>"));
            return result;
        }

        public static String SecureIDOrFullSecureID(string id)
        {
            var ticket = new Tickets { ID = Convert.ToInt32(id) };
            ticket.GetById();
            if (!String.IsNullOrEmpty(ticket.FullSecureID))
            {
                return ticket.FullSecureID;
            }
            else
            {
                return ticket.SecureID;
            }
        }

        public static String UserProfileIDToTicketEdit(int? userProfileID)
        {
            var result = String.Empty;
            var userProfile = new UsersProfiles { ID = Convert.ToInt32(userProfileID) };

            userProfile.GetById();
            //если профиль был удален
            if (userProfile.TypeID != null)
            {
                result = userProfile.TypeID.ToString() + userProfile.ID;
            }
            return result;
        }

        public static String GetProfileData(string userProfileID)
        {
            var userProfile = new UsersProfiles { ID = Convert.ToInt32(userProfileID) };
            userProfile.GetById();
            var result = userProfile.TypeID == 1 ? (userProfile.FirstName + ' ' + userProfile.LastName) : userProfile.CompanyName;
            return result;
        }

        public static String GetProfileContactPhone(string userProfileID)
        {
            var userProfile = new UsersProfiles { ID = Convert.ToInt32(userProfileID) };
            userProfile.GetById();
            var result = userProfile.ContactPhoneNumbers;
            if (!String.IsNullOrEmpty(result))
            {
                result = result.Replace("+375 ", " ");
            }
            else
            {
                var user = new Users { ID = Convert.ToInt32(userProfile.UserID) };
                user.GetById();
                if (!String.IsNullOrEmpty(user.Phone))
                {
                    result = user.Phone.Replace("+375 ", " ");
                }
            }
            return result ?? (result = String.Empty);
        }


        public static String AgreedOrAssessedCostCost(string ticketID)
        {
            var ticket = new Tickets { ID = Convert.ToInt32(ticketID) };
            ticket.GetById();
            var result = ticket.AssessedCost.ToString();
            if (!String.IsNullOrEmpty(ticket.AgreedCost.ToString()) && ticket.AgreedCost != 0)
            {
                result = ticket.AgreedCost.ToString();
            }
            return result;
        }

        public static String Shtuk(string description)
        {
            var result = String.IsNullOrEmpty(description) ? String.Empty : "шт";
            return result;
        }

        public static String GoodsNumber(string numer)
        {
            var result = (String.IsNullOrEmpty(numer) || numer == "0") ? String.Empty : numer;
            return result;
        }

        public static String GoodsNumberChange(string numer, string cost, string allCost)
        {
            var result = (String.IsNullOrEmpty(numer) || numer == "0") ? String.Empty : numer;
            if(result != String.Empty)
            {
                result = Convert.ToInt32(Convert.ToDouble(allCost) / Convert.ToDouble(cost)).ToString();
            }
            return result;
        }

        public static string ErrorCuter(string error)
        {
            error = error.Remove(100) + "...";
            return error;
        }

        public static String AdminPageCutter(String text)
        {
            if (text.Length > 90)
            {
                text = text.Remove(90) + "...";
            }
            return text;
        }

        public static String CheckboxFlag(string value)
        {
            var result = String.Empty;
            if (String.IsNullOrEmpty(value))
            {
                return result;
            }
            if (Convert.ToInt32(value) == 1)
            {
                result = "&#10004;";
            }
            return result;
        }

        public static String UserIDToSpecialClientTrClass(string value)
        {
            var result = String.Empty;
            var user = new Users { ID = Convert.ToInt32(value) };
            user.GetById();
            if (user.SpecialClient == 1)
            {
                result = "specialClientTr";
            }
            return result;
        }

        public static String SpecialClientTrClass(string value)
        {
            var result = String.Empty;
            if (!String.IsNullOrEmpty(value) && Convert.ToInt32(value) == 1)
            {
                result = "specialClientTr";
            }
            return result;
        }

        public static String SpecialClientTrClass(Int32 userid)
        {
            var result = String.Empty;
            var user = new Users() { ID = userid };
            user.GetById();
            if (Convert.ToInt32(user.SpecialClient) == 1)
            {
                result = "specialClientTr";
            }
            return result;
        }

        public static String RedClientTrClass(Int32 userid)
        {
            var result = String.Empty;
            var user = new Users() { ID = userid };
            user.GetById();
            if (Convert.ToInt32(user.RedClient) == 1)
            {
                result = "redClientTr";
            }
            return result;
        }

        public static String NaklInMap(string id)
        {
            var ticket = new Tickets();
            ticket.ID = Convert.ToInt32(id);
            ticket.GetById();
            var result = String.Empty;
            if (ticket.PrintNaklInMap == 1 || ticket.CreditDocuments == 1)
            {
                result = " &#10004;";
            }
            return result;
        }

        public static String DeleteBkt(string goodsName)
        {
            var result = String.Empty;
            if (!String.IsNullOrEmpty(goodsName) && goodsName.Length > 5 && goodsName.Contains("*(") && goodsName.Contains(")"))
            {
                var start = goodsName.IndexOf("*(", StringComparison.Ordinal) + 2;
                var end = goodsName.IndexOf(")", start, StringComparison.Ordinal);
                var deleteString = goodsName.Substring(start, end - start);
                result = goodsName.Replace(deleteString, "").Replace(")", "").Replace("*(", "");
            }
            else
            {
                result = goodsName;
            }

            return result;
        }

        public static string AgreedAssessedDeliveryCostsColor(string ticketId)
        {
            var result = String.Empty;
            var ticket = new Tickets { ID = Convert.ToInt32(ticketId) };
            ticket.GetById();
            result = ticket.AgreedCost != 0 || ticket.WithoutMoney == 1 ? "darkorange" : "black";
            return result;
        }

        public static string ChangeTitleColor(string ticketId)
        {
            var result = String.Empty;
            var ticket = new Tickets { ID = Convert.ToInt32(ticketId) };
            ticket.GetById();
            result = ticket.NoteChanged == 1 ? "darkorange" : "black";
            return result;
        }

        public static string IssuanceListIdToString(string id)
        {
            string result;
            if (id == "0" || id == String.Empty)
            {
                result = String.Empty;
            }
            else
            {
                result = id;
            }
            return result;
        }

        public static string GetIPAddress()
        {
            System.Web.HttpContext context = HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!String.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return context.Request.ServerVariables["REMOTE_ADDR"];
        }

        public static string BeInUseReplace(string text)
        {

            return text.Replace("б/у", "").
                Replace("б/y", "").
                Replace("б\\у", "").
                Replace("б\\y", "").
                Replace("б.у.", "").
                Replace("б.y.", "").
                Replace("б.у", "").
                Replace("б.y", "");
        }

        public static string LinkZP2(string idList)
        {
            return "PrintZP2.aspx?id=" + idList;
        }

        public static string LinkZP2All(string idList)
        {
            return "PrintZP2.aspx?id=" + idList;
        }

        public static string LinkPut3(string idList)
        {
            return "PrintPut3.aspx?id=" + idList;
        }

        public static string IfChangedZP2(Int32 id)
        {
            var ticket = new Tickets { ID = id };
            ticket.GetById();
            return ticket.NotPrintInPril2 == 0 ? ticket.SecureID : String.Format("<span style='color:red'>{0}</span>", ticket.SecureID);
        }

        public static string CheckboxView(Int32 value)
        {
            var result = String.Empty;
            if (value == 1)
            {
                result = " &#10004;";
            }

            return result;
        }
        public static string UrgentTicketForMap(Int32 id)
        {
            var result = String.Empty;
            var ticket = new Tickets { ID = id };
            ticket.GetById();
            if (ticket.StatusIDOld == 11 || ticket.StatusIDOld == 4)
            {
                result = "срочно!";
            }
            return result;
        }

        public static string UrgentTicketForMapDistinguish(Int32 id)
        {
            var result = String.Empty;
            var ticket = new Tickets { ID = id };
            ticket.GetById();
            if (ticket.StatusIDOld == 11 || ticket.StatusIDOld == 4)
            {
                result = "urgent-in-map";
            }
            return result;
        }
    }
}