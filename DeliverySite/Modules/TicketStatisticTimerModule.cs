using Delivery.DAL.DataBaseObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading;
using System.Web;
using static Delivery.BLL.StaticMethods.ReadXLSMethods;

namespace DeliverySite.Modules
{
    public class TicketStatisticTimerModule
    {
        static Timer timer;
        long interval = 1000 * 60 * 60 * 24; // 1 день
        static object synclock = new object();
        static bool sent = false;

        int second = 1000;
        int minute = 60 * 1000;
        int hour = 60 * 60 * 1000;

        public void Init()
        {
            int timeNow = DateTime.Now.Second * second + DateTime.Now.Minute * minute + DateTime.Now.Hour * hour;
            int timeToUpdate = Convert.ToInt32(new TimeSpan(18,0,0).TotalMilliseconds);
            int compareTime = (timeToUpdate - timeNow) > 0 ? timeToUpdate - timeNow : timeToUpdate - timeNow + 24 * hour;

            timer = new Timer(new TimerCallback(SendEmail), null, compareTime, interval);
        }

        private void SendEmail(object obj)
        {
            lock (synclock)
            {
                var usersToSend = new Settings_statistic().GetAllItems().Tables[0].Rows;

                List<string> mails = new List<string>();

                foreach (DataRow row in usersToSend)
                {
                    if (!mails.Contains(row.ItemArray[2].ToString()))
                    {
                        mails.Add(row.ItemArray[2].ToString());
                    }
                }                

                foreach (String mail in mails)
                {
                    List<int> UserIDs = new List<int>();
                    foreach (DataRow row in usersToSend)
                    {
                        if (row.ItemArray[2].ToString() == mail)
                            UserIDs.Add(Convert.ToInt32(row.ItemArray[1].ToString()));
                    }
                    
                    SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587);
                    smtp.EnableSsl = true;
                    smtp.Credentials = new System.Net.NetworkCredential("gmbdoto@gmail.com", "2351001057001n");

                    // наш email с заголовком письма
                    MailAddress from = new MailAddress("gmbdoto@gmail.com", "Статистика");
                    // кому отправляем
                    MailAddress to = new MailAddress(mail);
                    // создаем объект сообщения
                    MailMessage m = new MailMessage(from, to);
                    // тема письма
                    m.Subject = "Статистика по заявкам кабинетов";


                    Ticket_statistic statistic = new Ticket_statistic();                        
                    DataTable tickets = statistic.GetAllItems().Tables[0];                                                                            

                    if(tickets.Rows.Count != 0)
                    {
                        string s = "ChangeDate >= '" + DateTime.Now.AddDays(-7) + "'" + "AND UserID IN (" + string.Join(",", UserIDs) + ")";
                        var rows = tickets.Select("ChangeDate >= '" + DateTime.Now.AddDays(-7) + "'" + "AND UserID IN (" + string.Join(",", UserIDs) + ")");
                        if(rows.Count() != 0)
                        {
                            MemoryStream xlsStream = new XLSWrite().GetXLSStreamStatistic(rows.CopyToDataTable());
                            m.Attachments.Add(new Attachment(xlsStream, "статистика для кабинетов.xlsx"));

                            smtp.Send(m);
                        }                        
                    }
                }
            }
        }
        public void Dispose()
        { }
    }
}