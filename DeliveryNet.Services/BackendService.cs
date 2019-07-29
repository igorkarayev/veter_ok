using DeliveryNet.Data.Context;
using DeliveryNet.Interfaces;
using System.ComponentModel.Composition;
using System.Linq;

namespace DeliveryNet.Services
{
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [Export(typeof(IBackendService))]
    public class BackendService : IBackendService
    {
        #region Properties and Fields  

        readonly DeliveryNetContext _context;

        #endregion

        #region Constructors

        [ImportingConstructor]
        public BackendService(DeliveryNetContext ctx)
        {
            _context = ctx;
        }

        #endregion

        public string GetValueByTag(string tagName)
        {
            var tag = _context.Backends.First(u => u.Tag == tagName);
            return tag != null ? tag.Value : string.Empty;
        }
    }
}
