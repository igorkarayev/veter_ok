using DeliveryNet.Data;
using Delivery.ViewModels.Shared;
using PagedList;

namespace Delivery.ViewModels.Home
{
    public class IndexViewModel : LayoutViewModel
    {
        public Page PageInfo { get; set; }
        public IPagedList<DeliveryNet.Data.News> News { get; set; }
    }
}