using DeliveryNet.Data;
using System.Collections.Generic;

namespace DeliveryNet.Interfaces
{
    public interface IGoodsService
    {
        List<Goods> GetMain();
        List<Goods> GetAllGoods();
    }
}
