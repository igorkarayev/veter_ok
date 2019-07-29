using DeliveryNet.Data;
using DeliveryNet.Data.Context;
using DeliveryNet.Interfaces;
using System.ComponentModel.Composition;
using System.Linq;

namespace DeliveryNet.Services
{
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [Export(typeof(IPageService))]
    public class PageService : IPageService
    {
        #region Properties and Fields  

        readonly DeliveryNetContext _context;

        #endregion

        #region Constructors

        [ImportingConstructor]
        public PageService(DeliveryNetContext ctx)
        {
            _context = ctx;
        }

        #endregion

        public Page GetByName(string pageName)
        {
            var pages = _context.Pages.Where(u => u.PageName == pageName);
            return pages.Any() ? pages.First() : null;
        }
    }
}
