using Delivery.ViewModels.Shared;
using PagedList;
using Delivery.ViewModels.News;


namespace Delivery.ViewModels.News
{
    public class StreamViewModel : LayoutViewModel
    {
        public IPagedList<DeliveryNet.Data.News> News { get; set; }
        public DeliveryNet.Data.News LatestNews { get; set; }
        public bool Paging { get; set; }
    }
}