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
    [Export(typeof(ICityService))]
    public class CityService : ICityService
    {
        #region Properties and Fields  

        readonly DeliveryNetContext _context;

        #endregion

        #region Constructors

        [ImportingConstructor]
        public CityService(DeliveryNetContext ctx)
        {
            _context = ctx;
            AllCities = GetMain();
        }

        #endregion
        public static List<CityAdditionalInfo> AllCities;

        public List<CityAdditionalInfo> GetAllCities()
        {
            return AllCities;
        }

        public List<CityAdditionalInfo> GetMain()
        {
            return (from c in _context.City
                    join d in _context.Districts on c.DistrictID equals d.ID
                    where c.IsMainCity == 1
                    orderby c.Name
                    select new CityAdditionalInfo
                    {
                        CityId = c.ID,
                        CityName = c.Name,
                        DistrictName = d.Name,
                        RegionId = c.RegionID,
                        Monday = d.Monday,
                        Tuesday = d.Tuesday,
                        Wednesday = d.Wednesday,
                        Thursday = d.Thursday,
                        Friday = d.Friday,
                        Saturday = d.Saturday,
                        Sunday = d.Sunday
                    }).ToList();
        }

        public List<CityAdditionalInfo> GetCompanions()
        {
            return (from c in _context.City
                    join d in _context.Districts on c.DistrictID equals d.ID
                    where c.DistanceFromCity == -1
                    orderby c.Name
                    select new CityAdditionalInfo
                    {
                        CityName = c.Name,
                        DistrictName = d.Name                        
                    }).ToList();
        }
        
        public void GetAutocopliteCities()
        {
            var City = GetMain();
        }
    }
}
