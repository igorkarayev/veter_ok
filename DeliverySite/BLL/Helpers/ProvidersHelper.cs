using Delivery.DAL.DataBaseObjects;
using System.Linq;

namespace Delivery.BLL.Helpers
{
    public class ProvidersHelper
    {
        public static string ProviderNamePrefixToText(int val)
        {
            return Providers.NamePrefixes.Where(u => u.Key == val).Select(p => p.Value.ToString()).First();
        }
    }
}