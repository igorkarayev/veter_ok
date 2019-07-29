using System.Web.Configuration;

namespace Delivery
{
    public static class Globals
    {
        public readonly static ConfigSection Settings =
            (ConfigSection)WebConfigurationManager.GetSection("deliverynet");
    }
}

