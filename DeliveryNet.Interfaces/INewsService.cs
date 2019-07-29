using DeliveryNet.Data;
using System.Collections.Generic;

namespace DeliveryNet.Interfaces
{
    public interface INewsService
    {
        List<News> GetAllGuestOrderByCreateDate();
        List<News> GetFirstTwoNews();
        News GetByTitleUrl(string titleUrl);
    }
}
