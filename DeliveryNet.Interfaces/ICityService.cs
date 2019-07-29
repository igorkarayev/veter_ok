using DeliveryNet.Data;
using System.Collections.Generic;

namespace DeliveryNet.Interfaces
{
    public interface ICityService
    {
        List<CityAdditionalInfo> GetMain();

        List<CityAdditionalInfo> GetCompanions();
        List<CityAdditionalInfo> GetAllCities();
    }
}
