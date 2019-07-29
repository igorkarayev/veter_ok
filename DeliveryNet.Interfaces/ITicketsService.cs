using DeliveryNet.Data;
using System.Collections.Generic;

namespace DeliveryNet.Interfaces
{
    public class StatusTicketModel
    {
        public string Status { get; set; }
        public string OvDate { get; set; }
    }

    public interface ITicketsService
    {
        /*List<CityAdditionalInfo> GetMain();

        List<CityAdditionalInfo> GetCompanions();
        List<CityAdditionalInfo> GetAllCities();*/

        StatusTicketModel GetStatusById(string id);
    }
}
