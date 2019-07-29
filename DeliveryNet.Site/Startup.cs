
using Delivery;
using Microsoft.Owin;
using Owin;
using System.Web.Services.Description;

[assembly: OwinStartup(typeof(Startup))]
namespace Delivery
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }        
    }
}
