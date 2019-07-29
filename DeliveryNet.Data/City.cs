using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace DeliveryNet.Data
{
    [Table("city")]
    public class City
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public int? TrackID { get; set; }

        public int? RegionID { get; set; }

        public int? DistrictID { get; set; }

        public string VillageCouncilName { get; set; }

        public string SOATO { get; set; }

        public int? IsMainCity { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? ChangeDate { get; set; }

        public int? Distance { get; set; }

        public int? DistanceFromCity { get; set; }

        public int? Blocked { get; set; }

        public static Dictionary<int, string> Regions = new Dictionary<int, string>()
        {
            {1, "Mинская"},
            {2, "Бpестская"},
            {3, "Гpодненская"},
            {4, "Mогилевская"},
            {5, "Гoмельская"},
            {6, "Bитебская"},
        };

        public static string GetRegionName(int regionId)
        {
            return Regions.FirstOrDefault(u => u.Key == regionId).Value;
        }
    }

    public class CityAdditionalInfo
    {
        public int CityId { get; set; }
        public string CityName { get; set; }
        public string DistrictName { get; set; }
        public string Days { get; set; }
        public string RegionName { get; set; }
        public int? RegionId { get; set; }
        public int? Monday { get; set; }

        public int? Tuesday { get; set; }

        public int? Wednesday { get; set; }

        public int? Thursday { get; set; }

        public int? Friday { get; set; }

        public int? Saturday { get; set; }

        public int? Sunday { get; set; }

        public static Dictionary<int, string> Regions = new Dictionary<int, string>()
        {
            {1, "Mинская"},
            {2, "Бpестская"},
            {3, "Гpодненская"},
            {4, "Mогилевская"},
            {5, "Гoмельская"},
            {6, "Bитебская"},
        };

        public static string GetRegionName(int? regionId)
        {
            return Regions.FirstOrDefault(u => u.Key == regionId).Value;
        }
    }
}