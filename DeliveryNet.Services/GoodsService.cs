using Delivery;
using DeliveryNet.Data;
using DeliveryNet.Data.Context;
using DeliveryNet.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace DeliveryNet.Services
{
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [Export(typeof(IGoodsService))]
    public class GoodsService : IGoodsService
    {
        #region Properties and Fields  

        readonly DeliveryNetContext _context;

        #endregion

        #region Constructors

        [ImportingConstructor]
        public GoodsService(DeliveryNetContext ctx)
        {
            _context = ctx;
            AllGoods = GetMain();
        }

        #endregion
        public static List<Goods> AllGoods;

        public List<Goods> GetAllGoods()
        {
            return AllGoods;
        }

        public List<Goods> GetMain()
        {
            return (from c in _context.Goods
                    select new
                    {                                                
                        Name = c.Name,
                        MarginCoefficient = c.MarginCoefficient,
                        ID = c.ID
                    }).AsEnumerable().Select(x => new Goods
                    {
                        Name = x.Name,
                        MarginCoefficient = x.MarginCoefficient,
                        ID = x.ID
                    }).ToList();
        }        
    }
}
