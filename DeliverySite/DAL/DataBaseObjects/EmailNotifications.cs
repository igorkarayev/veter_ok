using Delivery.DAL.Attributes;
using System;
using System.Data;
using System.Net.Mail;

namespace Delivery.DAL.DataBaseObjects
{
    public class EmailNotifications
    {
        public DataManager DM;

        public String TableName { get; set; }

        public EmailNotifications()
        {
            DM = new DataManager();
            this.TableName = "emailnotifications";
            this.ChangeDate = DateTime.Now;
            this.Title = String.Empty;
        }

        [DataBaseGet]
        public Int32 ID { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String Name { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String Title { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String Body { get; set; }

        [DataBaseGet]
        public String Description { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public DateTime? ChangeDate { get; set; }

        public dynamic GetById()
        {
            return DM.GetDataBy(this, "ID", null);
        }

        public dynamic GetByName()
        {
            return DM.GetDataBy(this, "Name", null);
        }

        public DataSet GetAllItems()
        {
            return DM.GetAllData(this, null, null, null);
        }

        public DataSet GetAllItems(string orderBy, string direction, string whereField)
        {
            return DM.GetAllData(this, orderBy, direction, whereField);
        }

        public void Update()
        {
            DM.UpdateDate(this);
        }
        public void MailSend(String name, String emailFrom, String body, String emailTo, String subject)
        {
            // Наш ящик для отправки (логин и пароль от которого мы указали выше)
            // По-идеи можно было бы подставить значение которое вводит пользователь,
            // но gmail по крайней мере показывает в качестве адреса отправителя почту,
            // которую мы использовали для логина. Но хотя бы имя можно поменять.

            var message = new MailMessage
            {
                From = new MailAddress(emailFrom, name)
            };
            message.ReplyToList.Add(new MailAddress(emailFrom, name));

            // Что бы было ясно от кого письмо и куда отвечать:

            // Почта получателя
            message.To.Add(new MailAddress(emailTo));

            // Тема и текст письма
            message.Subject = subject;
            message.Body = body;
            var client = new SmtpClient { EnableSsl = true };

            // Тот SSL без которого не может Google Mail

            client.Send(message);
        }
    }
}
