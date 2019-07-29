using Delivery.BLL.Helpers;
using Delivery.BLL.StaticMethods;
using Delivery.DAL.DataBaseObjects;
using System;
using System.Web;
using System.Web.Services;

namespace Delivery.WebServices.PublicAPI
{
    /// <summary>
    /// Summary description for CityAPI
    /// </summary>
    [WebService(Namespace = "DeliveryNetWebService")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [System.Web.Script.Services.ScriptService]
    public class TicketStatusAPI : System.Web.Services.WebService
    {

        [WebMethod(Description = "Метод возвращает статус заявки")]
        public void GetTicketStatus(string ticketid)
        {
            if (ApiMethods.IsApiAuthRequest())
            {
                var sevicePhone = BackendHelper.TagToValue("status_check_phones");
                var ticket = new Tickets { SecureID = ticketid.ToLower().Trim() };
                ticket.GetBySecureId();
                string text;
                if (ticket.ID == 0)
                {
                    text = "Заказ с заданным ID не найден. <br/>" +
                           "По вопросам статуса заказа вы можете обратиться по телефону " + sevicePhone;
                    Context.Response.Write(text);
                }
                else
                {

                    var ticketStatusText = TicketsHelper.TicketStatusIdToSimpleRusTextMale(ticket.StatusID.ToString());
                    switch (ticket.StatusID)
                    {
                        case 1:
                            text = "Ваша заказ пока не зарегистрирован у нас. Проверьте статус заказа позже. <br/>" +
                                   "По вопросам статуса заказа вы можете обратиться по телефону " + sevicePhone;
                            break;
                        case 2: // на складе
                        case 19: // к загрузке
                            text = String.Format("Ваша заказ находится в статусе <b>\"{0}\"</b>. " +
                                                 "Для уточнения информации обращайтесь по номеру {1}.", ticketStatusText, sevicePhone);
                            break;
                        case 3:
                            if (ticket.OvDateFrom != null && ticket.OvDateTo != null)
                            {
                                text = String.Format("Ваша заказ погружен и находится в пути. <br/>" +
                                                     "Ориентировочная дата доставки: <b>{0}</b><br/>" +
                                                     "Ориентировочное время доставки: <b>с {1} по {2}</b>" +
                                                     "Для уточнения информации обращайтесь по телефону {3}.",
                                    Convert.ToDateTime(ticket.DeliveryDate).ToString("dd.MM.yyyy"),
                                    Convert.ToDateTime(ticket.OvDateFrom).ToString("HH:mm"),
                                    Convert.ToDateTime(ticket.OvDateTo).ToString("HH:mm"), sevicePhone);
                            }
                            else
                            {
                                text = String.Format("Ваша заказ погружен и находится в пути. <br/>" +
                                                 "Ориентировочная дата доставки: <b>{0}</b><br/>" +
                                                     "Для уточнения времени доставки обращайтесь по телефону {1}.",
                                                 Convert.ToDateTime(ticket.DeliveryDate).ToString("dd.MM.yyyy"), sevicePhone);
                            }
                            break;
                        default:
                            text = "Заказ с заданным ID не найден. <br/>" +
                                   "По вопросам статуса заказа вы можете обратиться по телефону " + sevicePhone;
                            break;
                    }
                    Context.Response.Write(text);
                }

                ApiMethods.LoggingRequest("GetTicketStatus",
                "TicketStatusAPI",
                "PublicAPI",
                ticketid,
                text.Length,
                Convert.ToInt32(HttpContext.Current.Request.Params["userid"]),
                HttpContext.Current.Request.Params["apikey"]);
            }
            else
            {
                ApiMethods.ReturnNotAuth();
            }
        }
    }
}
