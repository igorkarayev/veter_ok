using Delivery.ViewModels.Shared;
using PagedList;

namespace Delivery.ViewModels.News
{
    public class DetailsViewModel : LayoutViewModel
    {
        public DeliveryNet.Data.News News { get; set; }
        public IPagedList<DeliveryNet.Data.News> AllNews { get; set; }
    }
}