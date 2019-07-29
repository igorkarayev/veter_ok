using DeliveryNet.Data;
using Delivery.ViewModels.Shared;
using System.Collections.Generic;

namespace Delivery.ViewModels.Home
{
    public class CityViewModel : LayoutViewModel
    {
        public Page PageInfo { get; set; }
        public List<CityAdditionalInfo> City { get; set; }
        public List<CityAdditionalInfo> CityCompanions { get; set; }
        /// 
        /// 
        /// 
        public int? Id { get; set; }
        public int? RegionId { get; set; }
        public string Value { get; set; }
        public string Days { get; set; }
        public int Num { get; set; }
        public string Coef { get; set; }
    }
}