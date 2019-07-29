using DeliveryNet.Data;
using DeliveryNet.Data.Context;
using DeliveryNet.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace DeliveryNet.Services
{
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [Export(typeof(ITicketsService))]
    public class TicketsService : ITicketsService
    {

        #region Properties and Fields  

        readonly DeliveryNetContext _context;

        #endregion

        #region Constructors

        [ImportingConstructor]
        public TicketsService(DeliveryNetContext ctx)
        {
            _context = ctx;
        }

        #endregion        

        public StatusTicketModel GetStatusById(string id)
        {
            Tickets ticket = _context.Tickets.FirstOrDefault(u => u.SecureID == id);
            StatusTicketModel statusModel = new StatusTicketModel();

            if (ticket != null)
            {
                statusModel.Status = Tickets.TicketStatuses[Convert.ToInt32(ticket.StatusID)];
                if(statusModel.Status.ToLower() == "в пути")
                {
                    statusModel.OvDate += "Доставка с ";
                    statusModel.OvDate += FullDateTimeConvertForCity(ticket.OvDateFrom.ToString());
                    statusModel.OvDate += " до ";
                    statusModel.OvDate += FullDateTimeConvertForCity(ticket.OvDateTo.ToString());
                }
                
                statusModel.Status = char.ToUpper(statusModel.Status[0]) + statusModel.Status.Substring(1);

                return statusModel;
            }

            return new StatusTicketModel() { Status = "Заявка не найдена" };
        }

        private static string FullDateTimeConvertForCity(string date)
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
    }
}