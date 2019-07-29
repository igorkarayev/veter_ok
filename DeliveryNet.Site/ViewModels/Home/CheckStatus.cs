using DeliveryNet.Data;
using Delivery.ViewModels.Shared;
using System.Collections.Generic;

namespace Delivery.ViewModels.Home
{
    public class CheckStatus
    {
        public string id;

        public string CityId { get; set; }
        public string CityRegionId { get; set; }
        public string CityValue { get; set; }
    }
}