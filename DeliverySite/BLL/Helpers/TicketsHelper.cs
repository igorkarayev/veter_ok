using Delivery.DAL.DataBaseObjects;
using System;
using System.Linq;

namespace Delivery.BLL.Helpers
{
    public class TicketsHelper
    {
        public static String DeferredProcessedStatus(string statusId, string oldStatusId, string processedDate)
        {
            if (String.IsNullOrEmpty(processedDate)) return statusId;
            if (statusId != "5") return statusId;
            var additionalProcessedTime = (0 - Convert.ToInt32(BackendHelper.TagToValue("interval_display_tickets_processed")));
            var defferedTime = DateTime.Now.AddHours(additionalProcessedTime);
            var processedTime = Convert.ToDateTime(processedDate);
            var resultDateDifаerence = DateTime.Compare(defferedTime, processedTime);
            return resultDateDifаerence >= 0 ? statusId : oldStatusId;
        }

        public static String StatusReplacement(string statusId)
        {
            if (BackendHelper.TagToValue("delivered_tickets_is_visible_for_client") == "true" && statusId == "12")
                return "3";
            return statusId;
        }

        public static string SelectIfBilled(string isBilled)
        {
            if (!string.IsNullOrEmpty(isBilled) && isBilled == "1")
                return "billed-selected";
            return string.Empty;
        }

        public static String BackgroundCheckedOutPhoned(string phonedId, string checkedOutId)
        {
            var result = String.Empty;

            if (phonedId == "1" && checkedOutId == "1") result = "checkedOutAndPhonedRow";
            if (phonedId == "0" && checkedOutId == "1") result = "checkedOutRow";
            if (phonedId == "1" && checkedOutId == "0") result = "phonedRow";

            return result;
        }

        public static String RecipientStreetCleaner(String street)
        {
            return street.Replace("ул ", "").Replace("ул.", "").Replace("пер.", "").Replace("пр.", "").Trim();
        }

        public static String SenderAddress(string ticketId)
        {
            var ticket = new Tickets { ID = Convert.ToInt32(ticketId) };
            ticket.GetById();
            var city = new City { ID = Convert.ToInt32(ticket.SenderCityID) };
            city.GetById();
            var result = city.Name + ", " + ticket.RecipientStreetPrefix + " " + ticket.SenderStreetName + " " + ticket.SenderStreetNumber;
            if (!String.IsNullOrEmpty(ticket.SenderHousing))
            {
                result += "/" + ticket.SenderHousing;
            }
            if (!String.IsNullOrEmpty(ticket.SenderApartmentNumber))
            {
                result += " кв." + ticket.SenderApartmentNumber;
            }
            return result;
        }

        public static String IfCompletedTicketToPrintRefunds(string completedDate)
        {
            var result = String.Empty;
            if (string.IsNullOrEmpty(completedDate)) result = "grayRow";
            return result;
        }

        public static String TicketStatusIdToSimpleRusText(string id)
        {
            return Tickets.TicketStatuses.Where(u => u.Key == Convert.ToInt32(id)).Select(p => p.Value.ToString()).First();
        }

        public static String TicketStatusIdToSimpleRusTextMale(string id)
        {
            return Tickets.TicketStatusesMale.Where(u => u.Key == Convert.ToInt32(id)).Select(p => p.Value.ToString()).First();
        }
    }
}