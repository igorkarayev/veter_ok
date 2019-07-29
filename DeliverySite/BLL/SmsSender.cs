using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Delivery.BLL.Helpers;
using Delivery.DAL;
using Delivery.DAL.DataBaseObjects;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Delivery.BLL
{
    public class SmsSender
    {
        public const string SmsServiceFunction = "msg_send_bulk";        

        public static void SendSmsBulk(string ticketIdListString)
        {
            var smsServiceApiProvider = BackendHelper.TagToValue("sms-api-provider");
            var smsServiceSenderName = BackendHelper.TagToValue("sms_sender");
            var smsServiceUser = BackendHelper.TagToValue("sms-service-user");
            var smsServiceApiKey = BackendHelper.TagToValue("sms-service-apikey");
            var settings = new JsonSerializerSettings { ContractResolver = new LowercaseContractResolver() };
            var ticketIdList = ticketIdListString.Split(';');
            var messageObjectList = new List<MessageObject>();
            var smsBody = BackendHelper.TagToValue("sms_text");
            foreach (var ticketId in ticketIdList)
            {
                if(String.IsNullOrEmpty(ticketId)) continue;
                var ticket = new Tickets {ID = Convert.ToInt32(ticketId)};
                ticket.GetById();
                //отправляем нотификации только по заявкам в пути
                if (ticket.StatusID != 3) continue;
                //условия, при которых добавляем заявку в список рассылки (должна присутствовать дата с по)
                if (ticket.OvDateFrom != null && ticket.OvDateTo != null && !String.IsNullOrEmpty(ticket.OvDateFrom.ToString()) && !String.IsNullOrEmpty(ticket.OvDateTo.ToString()))
                {
                    //условия добавления в список рассылки по телефонам получателя
                    if (!String.IsNullOrEmpty(ticket.RecipientPhone) && !ticket.RecipientPhone.Contains("(17)"))
                    {
                        var messageObject = new MessageObject
                        {
                            Recipient = ticket.RecipientPhone.Replace("(", "").Replace(")", "").Replace("-", "").Replace("+", ""),
                            Message = smsBody
                                .Replace("{from}", Convert.ToDateTime(ticket.OvDateFrom).ToString("HH:mm"))
                                .Replace("{to}", Convert.ToDateTime(ticket.OvDateTo).ToString("HH:mm"))
                                .Replace("{id}", ticket.SecureID),
                            Sender = smsServiceSenderName
                        };
                        messageObjectList.Add(messageObject);
                    }

                    if (!String.IsNullOrEmpty(ticket.RecipientPhoneTwo) && !ticket.RecipientPhoneTwo.Contains("(17)"))
                    {
                        var messageObject = new MessageObject
                        {
                            Recipient = ticket.RecipientPhoneTwo.Replace("(", "").Replace(")", "").Replace("-", "").Replace("+", ""),
                            Message = smsBody
                                .Replace("{from}", Convert.ToDateTime(ticket.OvDateFrom).ToString("HH:mm"))
                                .Replace("{to}", Convert.ToDateTime(ticket.OvDateTo).ToString("HH:mm"))
                                .Replace("{id}", ticket.SecureID),
                            Sender = smsServiceSenderName
                        };
                        messageObjectList.Add(messageObject);
                    }
                }
            }
            //считаем колличество запросов для отправки
            const int partSize = 500;
            var parts = messageObjectList.Count / partSize;
            if (messageObjectList.Count % partSize != 0)
                parts = parts + 1;
            //разбиваем сообщения по частям
            List<List<MessageObject>> messagesPartList = messageObjectList.Select((item, index) => new { index, item })
                       .GroupBy(x => x.index % parts)
                       .Select(x => x.Select(y => y.item).ToList()).ToList();

            //отправляем реквесты паралельно друг другу в разных потоках
            Parallel.ForEach(messagesPartList, messagePart =>
            {
                var request = WebRequest.Create(String.Format("{1}/?r=api/{0}", SmsServiceFunction, smsServiceApiProvider));
                request.ContentType = "application/x-www-form-urlencoded";
                request.Method = "POST";

                var json = JsonConvert.SerializeObject(messagePart, Formatting.Indented, settings).Replace("\r\n", "");
                var resultForSend = HttpUtility.UrlEncode(json);
                var postString = String.Format("user={0}&apikey={1}&messages={2}", smsServiceUser, smsServiceApiKey, resultForSend);
                var requestWriter = new StreamWriter(request.GetRequestStream());
                requestWriter.Write(postString);
                requestWriter.Close();

                var responseReader = new StreamReader(request.GetResponse().GetResponseStream());
                var responseData = responseReader.ReadToEnd(); //сюда пишем ответ
                responseReader.Close();

                request.GetResponse().Close(); //отправка запроса
            });
        }

        public static void SendSmsBulkIfTicketOnStock(string ticketIdListString)
        {
            var smsServiceApiProvider = BackendHelper.TagToValue("sms-api-provider");
            var smsServiceSenderName = BackendHelper.TagToValue("sms_sender");
            var smsServiceUser = BackendHelper.TagToValue("sms-service-user");
            var smsServiceApiKey = BackendHelper.TagToValue("sms-service-apikey");
            var settings = new JsonSerializerSettings { ContractResolver = new LowercaseContractResolver() };
            var smsBody = BackendHelper.TagToValue("sms-on-print-checks-text");
            var ticketIdList = ticketIdListString.Split(';');
            var messageObjectList = new List<MessageObject>();
            foreach (var ticketId in ticketIdList)
            {
                if (String.IsNullOrEmpty(ticketId)) continue;
                var ticket = new Tickets { ID = Convert.ToInt32(ticketId) };
                ticket.GetById();
                var allCity = HttpContext.Current.Application["CityList"] as List<City>;
                var city = allCity.FirstOrDefault(u => u.ID == ticket.CityID);
                var allDistricts = HttpContext.Current.Application["Districts"] as List<Districts>;
                var district = allDistricts.FirstOrDefault(u => u.ID == city.DistrictID);
                var dm = new DataManager();
                if (String.IsNullOrEmpty(district.TrackID.ToString()))
                    district.TrackID = -1;
                var operatorsPhoneRows =
                    dm.QueryWithReturnDataSet(
                        "SELECT PhoneWorkOne, PhoneWorkTwo FROM Users WHERE ID = (SELECT ManagerID FROM Tracks WHERE ID = "+ district.TrackID +")")
                        .Tables[0];
                var operatorPhoneOne = "+375 (29/25/33) 604-45-82";
                var operatorPhoneTwo = String.Empty;
                if (operatorsPhoneRows.Rows.Count > 0)
                {
                    operatorPhoneOne = operatorsPhoneRows.Rows[0]["PhoneWorkOne"].ToString();
                    operatorPhoneTwo = operatorsPhoneRows.Rows[0]["PhoneWorkTwo"].ToString();
                }

                //условия добавления в список рассылки по телефонам получателя
                if (!String.IsNullOrEmpty(ticket.RecipientPhone) && !ticket.RecipientPhone.Contains("(17)"))
                {
                    var messageObject = new MessageObject
                    {
                        Recipient = ticket.RecipientPhone.Replace("(", "").Replace(")", "").Replace("-", "").Replace("+", ""),
                        Message = smsBody
                                .Replace("{from}", Convert.ToDateTime(ticket.DeliveryDate).ToString("dd.MM"))
                                .Replace("{to}", Convert.ToDateTime(ticket.DeliveryDate).AddDays(Convert.ToInt32(district.DeliveryTerms)).ToString("dd.MM"))
                                .Replace("{id}", ticket.SecureID)
                                .Replace("{operator-phones}", String.Format("{0}; {1}", operatorPhoneOne, operatorPhoneTwo)),
                        Sender = smsServiceSenderName
                    };
                    messageObjectList.Add(messageObject);
                }

                if (!String.IsNullOrEmpty(ticket.RecipientPhoneTwo) && !ticket.RecipientPhoneTwo.Contains("(17)"))
                {
                    var messageObject = new MessageObject
                    {
                        Recipient = ticket.RecipientPhoneTwo.Replace("(", "").Replace(")", "").Replace("-", "").Replace("+", ""),
                        Message = smsBody
                                .Replace("{from}", Convert.ToDateTime(ticket.DeliveryDate).ToString("dd.MM"))
                                .Replace("{to}", Convert.ToDateTime(ticket.DeliveryDate).AddDays(Convert.ToInt32(district.DeliveryTerms)).ToString("dd.MM"))
                                .Replace("{id}", ticket.SecureID),
                        Sender = smsServiceSenderName
                    };
                    messageObjectList.Add(messageObject);
                }
            }
            //считаем колличество запросов для отправки
            const int partSize = 500;
            var parts = messageObjectList.Count / partSize;
            if (messageObjectList.Count % partSize != 0)
                parts = parts + 1;
            //разбиваем сообщения по частям
            List<List<MessageObject>> messagesPartList = messageObjectList.Select((item, index) => new { index, item })
                       .GroupBy(x => x.index % parts)
                       .Select(x => x.Select(y => y.item).ToList()).ToList();

            //отправляем реквесты паралельно друг другу в разных потоках
            Parallel.ForEach(messagesPartList, messagePart =>
            {
                var request = WebRequest.Create(String.Format("{1}/?r=api/{0}", SmsServiceFunction, smsServiceApiProvider));
                request.ContentType = "application/x-www-form-urlencoded";
                request.Method = "POST";

                var json = JsonConvert.SerializeObject(messagePart, Formatting.Indented, settings).Replace("\r\n", "");
                var resultForSend = HttpUtility.UrlEncode(json);
                var postString = String.Format("user={0}&apikey={1}&messages={2}", smsServiceUser, smsServiceApiKey, resultForSend);
                var requestWriter = new StreamWriter(request.GetRequestStream());
                requestWriter.Write(postString);
                requestWriter.Close();

                var responseReader = new StreamReader(request.GetResponse().GetResponseStream());
                var responseData = responseReader.ReadToEnd(); //сюда пишем ответ
                responseReader.Close();

                request.GetResponse().Close(); //отправка запроса
            });
        }
    }

    public class MessageObject
    {
        public String Recipient { get; set; }
        public String Message { get; set; }
        public String Sender { get; set; }
    }

    public class LowercaseContractResolver : DefaultContractResolver
    {
        protected override string ResolvePropertyName(string propertyName)
        {
            return propertyName.ToLower().Replace("\r\n", "");
        }
    }
}