using Delivery.BLL.Helpers;
using Delivery.DAL.DataBaseObjects;
using System;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using MailMessage = System.Net.Mail.MailMessage;

namespace Delivery.BLL.StaticMethods
{
    public class EmailMethods
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
            Credentials = new NetworkCredential(EmailAddress, EmailPassword),
            DeliveryMethod = SmtpDeliveryMethod.Network
        };

        public static Boolean EmailChecker(string email)
        {
            const string pattern = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
            var match = Regex.Match(email.Trim(), pattern, RegexOptions.IgnoreCase);
            return match.Success;
        }

        public static void MailSend(Int32 mailId, String emailTo, String replaceThis, String replaceWithThat, bool async = false)
        {
            var message = new MailMessage
            {
                From = new MailAddress(MainEmail, NotOfficialName)
            };
            message.ReplyToList.Add(new MailAddress(MainEmail, NotOfficialName));
            message.To.Add(new MailAddress(emailTo));
            var mail = new EmailNotifications()
            {
                ID = mailId
            };
            mail.GetById();
            if (!String.IsNullOrEmpty(replaceThis) && !String.IsNullOrEmpty(replaceWithThat))
            {
                message.Subject = mail.Title.Replace(replaceThis, replaceWithThat);
                message.Body = mail.Description.Replace(replaceThis, replaceWithThat);
            }
            else
            {
                message.Subject = "[" + BackendHelper.TagToValue("server_name") + "] " + mail.Title;
                message.Body = mail.Description;
            }

            if (!async)
                client.Send(message);
            else
                client.SendAsync(message, null);
        }

        public static void MailSend(String mailTitle, String mailSubject, String emailTo)
        {
            MailSend(mailTitle, mailSubject, emailTo, false);
        }

        public static void MailSend(String mailTitle, String mailSubject, String emailTo, bool async = false)
        {
            var message = new MailMessage
            {
                From = new MailAddress(MainEmail, NotOfficialName)
            };
            message.ReplyToList.Add(new MailAddress(MainEmail, NotOfficialName));
            if (!String.IsNullOrEmpty(emailTo))
                message.To.Add(new MailAddress(emailTo));
            if (message.To.Count == 0)
                return;
            message.Subject = "[" + BackendHelper.TagToValue("server_name") + "] " + mailTitle;
            message.Body = mailSubject;
            if (!async)
                client.Send(message);
            else
                client.SendAsync(message, null);
        }

        public static void MailSend(String mailTitle, String mailSubject, string[] emailTo, bool async = false)
        {
            var message = new MailMessage
            {
                From = new MailAddress(MainEmail, NotOfficialName)
            };
            message.ReplyToList.Add(new MailAddress(MainEmail, NotOfficialName));
            foreach (var recipientEmail in emailTo)
            {
                if (!String.IsNullOrEmpty(recipientEmail))
                    message.To.Add(new MailAddress(recipientEmail.Trim()));
            }
            if (message.To.Count == 0)
                return;
            message.Subject = "[" + BackendHelper.TagToValue("server_name") + "] " + mailTitle;
            message.Body = mailSubject;

            if (!async)
                client.Send(message);
            else
                client.SendAsync(message, null);
        }

        public static void MailSendHTML(String mailTitle, String mailSubject, String emailTo, bool async = false)
        {
            var message = new MailMessage
            {
                From = new MailAddress(MainEmail, NotOfficialName)
            };
            message.ReplyToList.Add(new MailAddress(MainEmail, NotOfficialName));
            if (!String.IsNullOrEmpty(emailTo))
                message.To.Add(new MailAddress(emailTo));
            if (message.To.Count == 0)
                return;
            message.Subject = "[" + BackendHelper.TagToValue("server_name") + "] " + mailTitle;
            message.Body = String.Format("<html><body>{0}</body></html>", mailSubject);
            message.IsBodyHtml = true;

            if (!async)
                client.Send(message);
            else
                client.SendAsync(message, null);
        }

        public static void MailSendHTML(String mailTitle, String mailSubject, string[] emailTo, bool async = false)
        {
            var message = new MailMessage
            {
                From = new MailAddress(MainEmail, NotOfficialName)
            };
            message.ReplyToList.Add(new MailAddress(MainEmail, NotOfficialName));
            foreach (var recipientEmail in emailTo)
            {
                if (!String.IsNullOrEmpty(recipientEmail))
                    message.To.Add(new MailAddress(recipientEmail.Trim()));
            }
            if (message.To.Count == 0)
                return;
            message.Subject = "[" + BackendHelper.TagToValue("server_name") + "] " + mailTitle;
            message.Body = String.Format("<html><body>{0}</body></html>", mailSubject);
            message.IsBodyHtml = true;

            if(!async)
                client.Send(message);
            else
                client.SendAsync(message, null);
        }

        public static void MailSendHTML(String mailTitle, String mailSubject, String emailTo, String filePath, bool async = false)
        {
            var message = new MailMessage
            {
                From = new MailAddress(MainEmail, NotOfficialName)
            };
            message.ReplyToList.Add(new MailAddress(MainEmail, NotOfficialName));
            if (!String.IsNullOrEmpty(emailTo))
                message.To.Add(new MailAddress(emailTo));
            if (message.To.Count == 0)
                return;
            message.Subject = "[" + BackendHelper.TagToValue("server_name") + "] " + mailTitle;
            message.Body = String.Format("<html><body>{0}</body></html>", mailSubject);
            message.Attachments.Add(new System.Net.Mail.Attachment(filePath));
            message.IsBodyHtml = true;

            // Тот SSL без которого не может Google Mail

            if (!async)
                client.Send(message);
            else
                client.SendAsync(message, null);
        }
    }
}