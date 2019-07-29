using DeliveryNet.Data;

namespace DeliveryNet.Interfaces
{
    public interface IPageService
    {
        Page GetByName(string pageName);
    }
}
