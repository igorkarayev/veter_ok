using Delivery.BLL;
using Delivery.BLL.Helpers;
using Delivery.BLL.StaticMethods;
using Delivery.DAL;
using Delivery.DAL.DataBaseObjects;
using Delivery.ManagerUI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Delivery.PrintServices
{
    public partial class PrintCheck : ManagerBasePage
    {
        protected string BackLink { get; set; }

        protected void Page_Init(object sender, EventArgs e)
        {
            BackLink = OtherMethods.LinkBuilder(Page.Request.Params["sid"], Page.Request.Params["uid"],
                Page.Request.Params["recipientPhone"], Page.Request.Params["cityID"], Page.Request.Params["statusID"],
                Page.Request.Params["driverID"], Page.Request.Params["deliveryDate1"],
                Page.Request.Params["deliveryDate2"], Page.Request.Params["trackID"]);

            if (!string.IsNullOrEmpty(Page.Request.Params["page"]) && Page.Request.Params["page"] == "money")
            {
                BackLink = "ManagerUI/Menu/Finance/MoneyView.aspx?" + BackLink;
            }

            if (!string.IsNullOrEmpty(Page.Request.Params["page"]) && Page.Request.Params["page"] == "issuance")
            {
                BackLink = "ManagerUI/Menu/Issuance/IssuanceView.aspx?" + BackLink;
            }

            if (!string.IsNullOrEmpty(Page.Request.Params["page"]) && Page.Request.Params["page"] == "userticketsview")
            {
                BackLink = "ManagerUI/Menu/Tickets/UserTicketView.aspx?" + BackLink;
            }

            if (!string.IsNullOrEmpty(Page.Request.Params["page"]) && Page.Request.Params["page"] == "userticketbydeliveryonbelarus")
            {
                BackLink = "ManagerUI/Menu/Tickets/UserTicketByDeliveryOnBelarus.aspx?" + BackLink;
            }

            if (!string.IsNullOrEmpty(Page.Request.Params["page"]) && Page.Request.Params["page"] == "userticketbydeliveryonminsk")
            {
                BackLink = "ManagerUI/Menu/Tickets/UserTicketByDeliveryOnMinsk.aspx?" + BackLink;
            }

            if (!string.IsNullOrEmpty(Page.Request.Params["page"]) && Page.Request.Params["page"] == "userticketsnotprocessed")
            {
                BackLink = "ManagerUI/Menu/Tickets/UserTicketNotProcessedView.aspx?" + BackLink;
            }

            if (!string.IsNullOrEmpty(Page.Request.Params["page"]) && Page.Request.Params["page"] == "userticketsviewmy")
            {
                BackLink = "ManagerUI/Menu/Tickets/UserTicketViewMy.aspx?" + BackLink;
            }

            if (!string.IsNullOrEmpty(Page.Request.Params["page"]) && Page.Request.Params["page"] == "userticketsviewbydeliveryonminsk")
            {
                BackLink = "ManagerUI/Menu/Tickets/UserTicketByDeliveryOnMinsk.aspx?" + BackLink;
            }

            //редирект на страницу со всеми заявками, если не задана страница
            if (string.IsNullOrEmpty(Page.Request.Params["page"]))
            {
                BackLink = "ManagerUI/Menu/Tickets/UserTicketView.aspx?" + BackLink;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var idListString = Request.QueryString["id"];
            if (!String.IsNullOrEmpty(idListString))
            {
                List<string> idList = idListString.Split('-').ToList();
                var sqlString = String.Empty;
                var userIP = OtherMethods.GetIPAddress();
                var userInSession = (Users)Session["userinsession"];
                var userInSessionID = userInSession.ID.ToString();
                var pageName = "PrintCheck";
                var transferListString = String.Empty;
                var ticketToSendSmsListString = String.Empty;
                foreach (var id in idList)
                {
                    sqlString = sqlString + "ID = " + id + " OR ";

                    var currentTicket = new Tickets { ID = Convert.ToInt32(id) };
                    currentTicket.GetById();
                    if (currentTicket.StatusID == 1)
                    {
                        #region Обновление статусов и переносы заявок
                        var updateTicket = new Tickets
                        {
                            ID = Convert.ToInt32(id)
                        };

                        var dateDelivery = Convert.ToDateTime(Convert.ToDateTime(currentTicket.DeliveryDate).ToString("dd-MM-yyyy"));
                        var dateNow = Convert.ToDateTime(DateTime.Now.ToString("dd-MM-yyyy"));

                        //если день доставки просрочен - выставляем перенос. 
                        //Если день доставки вручную выставлен менеджеров через редактирование заявки 
                        //и этот день не просрочен и не поддается графику доставки - перенос не сработает
                        if (dateDelivery < dateNow)
                        {
                            var allCity = Application["CityList"] as List<City>;

                            if (allCity == null) { lblError.Text = "Непредвиденная ошибка! (города)"; return; }

                            var city = allCity.FirstOrDefault(u => u.ID == currentTicket.CityID);
                            var allDistricts = Application["Districts"] as List<Districts>;

                            if (allDistricts == null) { lblError.Text = "Непредвиденная ошибка! (районы)"; return; }
                            if (city == null) { lblError.Text = "Непредвиденная ошибка! (город)"; return; }

                            var district = allDistricts.FirstOrDefault(u => u.ID == city.DistrictID);
                            var avaliableDays = new List<DayOfWeek>();

                            if (district == null) { lblError.Text = "Непредвиденная ошибка! (район)"; return; }

                            if (district.Monday == 1)
                                avaliableDays.Add(DayOfWeek.Monday);
                            if (district.Tuesday == 1)
                                avaliableDays.Add(DayOfWeek.Tuesday);
                            if (district.Wednesday == 1)
                                avaliableDays.Add(DayOfWeek.Wednesday);
                            if (district.Thursday == 1)
                                avaliableDays.Add(DayOfWeek.Thursday);
                            if (district.Friday == 1)
                                avaliableDays.Add(DayOfWeek.Friday);
                            if (district.Saturday == 1)
                                avaliableDays.Add(DayOfWeek.Saturday);
                            if (district.Sunday == 1)
                                avaliableDays.Add(DayOfWeek.Sunday);
                            //день для переноса по умолчанию на сутки доальше чем сегодня. срабатывает, если для района не установлен день доставки
                            var dayOfTransfer = Convert.ToDateTime(DateTime.Now.AddDays(1).ToString("dd-MM-yyyy"));
                            for (int i = 1; i <= 7; i++)
                            {
                                var potencialDayOfTransfer = Convert.ToDateTime(DateTime.Now.AddDays(i).ToString("dd-MM-yyyy"));
                                if (avaliableDays.Contains(potencialDayOfTransfer.DayOfWeek))
                                {
                                    dayOfTransfer = potencialDayOfTransfer;
                                    break;
                                }
                            }
                            updateTicket.StatusID = 4;
                            updateTicket.AdmissionDate = DateTime.Now;
                            updateTicket.DeliveryDate = dayOfTransfer;
                            transferListString += currentTicket.SecureID + ", ";//добавляем запись в flash:now
                            ticketToSendSmsListString += currentTicket.ID + ";";//добавляем запись для отправки СМС
                            var user = new Users { ID = Convert.ToInt32(currentTicket.UserID) };
                            user.GetById();

                            #region Отправка емейла о том, что заявка переведена в статус перенос (на складе) и зануление водителя
                            var email = BackendHelper.TagToValue("transfer_in_stock_event_email");
                            EmailMethods.MailSendHTML("Новый перевод заявки в статус 'Перенос (на складе)' (приемка)", String.Format("Новый перевод заявки в статус 'Перенос (на складе)' (во время приемки)<br/>" +
                                                                                                                                     "ID заявки: <b>{0}</b>", currentTicket.SecureID), email);
                            updateTicket.DriverID = 0;
                            #endregion

                            #region Отправка емейлов отправителям
                            if (!String.IsNullOrEmpty(user.Email))
                            {
                                var address = currentTicket.RecipientStreet;
                                if (currentTicket.RecipientKorpus != String.Empty && currentTicket.RecipientKorpus != " ")
                                {
                                    address += "/" + currentTicket.RecipientKorpus;
                                }
                                if (currentTicket.RecipientKvartira != String.Empty && currentTicket.RecipientKvartira != " ")
                                {
                                    address += " кв." + currentTicket.RecipientKvartira;
                                }
                                const string emailTitle = "Перенос заявки";
                                var emailBody = BackendHelper.TagToValue("email-on-print-checks-text");
                                emailBody = emailBody
                                    .Replace("{full-name}", OtherMethods.GoodsStringFromTicketID(currentTicket.ID.ToString()))
                                    .Replace("{address}", city.Name + " " + address)
                                    .Replace("{id}", currentTicket.SecureID)
                                    .Replace("{recipient-phones}", currentTicket.RecipientPhone + "; " + currentTicket.RecipientPhoneTwo);
                                EmailMethods.MailSendHTML(emailTitle, emailBody, user.Email);
                            }
                            #endregion
                        }
                        else
                        {
                            ticketToSendSmsListString += currentTicket.ID + ";";//добавляем запись для отправки СМС
                            updateTicket.StatusID = 2;
                            updateTicket.AdmissionDate = DateTime.Now;
                        }
                        updateTicket.Update(Convert.ToInt32(userInSessionID), userIP, pageName);
                        #endregion
                    }
                }
                var fullSqlString = "SELECT * FROM `tickets` WHERE " + sqlString.Remove(sqlString.Length - 3);
                var dm = new DataManager();
                var dataset = dm.QueryWithReturnDataSet(fullSqlString);
                lvAllPrint.DataSource = dataset;
                lvAllPrint.DataBind();

                #region Данные для flash:now с перенесенными заявками
                if (transferListString.Length > 2)
                    Session["flash:now"] = "<span style='color: red;'>Перенесенные заявки: " + transferListString.Remove(transferListString.Length - 2, 2) + "</span>";
                #endregion

                #region Отправляем СМС
                if (ticketToSendSmsListString.Length > 2 && BackendHelper.TagToValue("sms-on-print-checks-enable") == "true")
                    SmsSender.SendSmsBulkIfTicketOnStock(ticketToSendSmsListString.Remove(ticketToSendSmsListString.Length - 1, 1));
                #endregion
            }

            if (String.IsNullOrEmpty(idListString) || lvAllPrint.Items.Count == 0)
            {
                Page.Visible = false;
                Response.Write(Resources.PrintResources.PrintCheckEmptyText);
            }
        }
    }
}