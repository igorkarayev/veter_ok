using Delivery.BLL.Helpers;
using Delivery.BLL.StaticMethods;
using Delivery.DAL.DataBaseObjects;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.Services;

namespace Delivery.AppServices
{
    /// <summary>
    /// Summary description for MailService
    /// </summary>
    [WebService(Namespace = "DeliveryNetWebService")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class MailService : WebService
    {
        private static readonly string MainEmail = BackendHelper.TagToValue("main_email");

        private static readonly string NotOfficialName = BackendHelper.TagToValue("not_official_name");

        private static readonly string EmailAddress = BackendHelper.TagToValue("main_email");

        private static readonly string EmailPassword = BackendHelper.TagToValue("main_email_password");

        private static readonly SmtpClient client = new SmtpClient
        {
            Host = "smtp.gmail.com",
            Port = 587,
            EnableSsl = true,
            Credentials = new NetworkCredential(EmailAddress, EmailPassword)
        };

        [WebMethod]
        public string SentMap(string body, string drivername, string appkey)
        {
            if (appkey == Globals.Settings.AppServiceSecureKey)
            {
                var sendEmailWithMapArray = BackendHelper.TagToValue("send_email_with_map");
                var recipientEmailsList = sendEmailWithMapArray.Split(new[] { ',' });
                if (!recipientEmailsList.Any() || String.IsNullOrEmpty(sendEmailWithMapArray))
                {
                    return "invalid recipient address";
                }

                var fileName = "map_" + DateTime.Now.ToString("yyyyMMddHHssmm") + ".html";
                if (!Directory.Exists(Server.MapPath("~/Temp/")))
                {
                    Directory.CreateDirectory(Server.MapPath("~/Temp/"));
                }
                var dataFile = Server.MapPath("~/Temp/" + fileName);
                File.WriteAllText(@dataFile, body);



                var message = new MailMessage
                {
                    From = new MailAddress(MainEmail, NotOfficialName)
                };
                message.ReplyToList.Add(new MailAddress(MainEmail, NotOfficialName));
                foreach (var recipientEmail in recipientEmailsList)
                {
                    message.To.Add(new MailAddress(recipientEmail.Trim()));
                }
                message.Subject = "[" + BackendHelper.TagToValue("server_name") + "] Карта (" + drivername + " " + DateTime.Now.AddDays(1).ToString("yy.MM.dd") + ")";
                message.Body = "Это письмо сгенерировано автоматически. Карта на " + drivername + " " + DateTime.Now.AddDays(1).ToString("yy.MM.dd");
                using (var reader = new StreamReader(Server.MapPath("~/Temp/" + fileName)))
                {
                    var a = new Attachment(reader.BaseStream, fileName);
                    message.Attachments.Add(a);
                    client.Send(message);
                }


                File.Delete(Server.MapPath("~/Temp/" + fileName));
                return "OK";
            }
            return "invalid app key";
        }

        [WebMethod]
        public string WantPayment(string userid, string appkey)
        {
            if (appkey != Globals.Settings.AppServiceSecureKey) return "invalid app key";
            try
            {
                var emails = BackendHelper.TagToValue("want_payment_address").Split(new[] { ',' });
                const string title = "Поступил новый запрос на расчет";
                var user = new Users { ID = Convert.ToInt32(userid) };
                user.GetById();
                var body = String.Format("От клиента #{0} ({1} {2}, {3})", user.ID, user.Name, user.Family, user.Phone);
                EmailMethods.MailSend(title, body, emails); //отправка емейла кассирам
                var issuancelist = new IssuanceLists()
                {
                    UserID = user.ID,
                    IssuanceListsStatusID = 1,
                    Comment = String.Format("Для клиента #{0} ({1} {2}, {3})", user.ID, user.Name, user.Family, user.Phone)
                };
                string result;
                if (DateTime.Now.Hour < 13)
                {
                    issuancelist.IssuanceDate = DateTime.Now;
                    result = "ok-now";
                }
                else
                {
                    issuancelist.IssuanceDate = DateTime.Now.AddDays(1);
                    result = "ok-tommorow";
                }
                issuancelist.Create();
                const string titleForUser = "Ваша заявка на расчет принята";
                var titleForBody = "Ваша заявка на расчет будет обработана";
                EmailMethods.MailSendHTML(titleForUser, titleForBody, user.Email); //отправка емейла пользователю
                return result;
            }
            catch (Exception)
            {
                return "invalid data";
            }
        }
    }
}
