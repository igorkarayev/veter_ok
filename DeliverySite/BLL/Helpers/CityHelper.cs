using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Delivery.DAL.DataBaseObjects;

namespace Delivery.BLL.Helpers
{
    public class CityHelper
    {
        public static String CityReplacement(String city)
        {
            return city.Replace("г.", "").Replace("ё", "е").Replace("Ё", "Е").Trim();
        }

        public static String RegionIDToRegionName(int? val)
        {
            return val == 0 ? String.Empty : City.Regions.Where(u => u.Key == val).Select(p => p.Value.ToString()).First();
        }

        public static String DistrictIDToDistrictName(int? val)
        {
            return (val == 0 || val == null) ? String.Empty : City.Districts.Where(u => u.Key == val).Select(p => p.Value.Name.ToString()).First();
        }

        public static String CityIDToCityName(string id)
        {
            if (id == null || id == "")
                return null;
            var city = new City { ID = Convert.ToInt32(id) };
            city.GetById();
            return city.Name;            
        }

        public static String CityIDToFullDistrictName(string id)
        {
            var result = String.Empty;
            if (id == "0" || id == "-1") return result;
            var city = new City { ID = Convert.ToInt32(id) };
            city.GetById();
            if (city.DistrictID == 0) return result;
            result = String.Format("{0} р/н",DistrictIDToDistrictName(city.DistrictID));

            return result;
        }

        public static String CityIDToFullRegionName(string id)
        {
            var result = String.Empty;
            if (id == "0" || id == "-1") return result;
            var city = new City { ID = Convert.ToInt32(id) };
            city.GetById();
            if (city.RegionID == 0) return result;
            result = String.Format("{0} обл.", RegionIDToRegionName(city.RegionID));

            return result;
        }

        public static String CityIDToDistrictName(string id)
        {
            var result = String.Empty;
            if (id == "0" || id == "-1" || String.IsNullOrEmpty(id)) return result;
            var city = new City { ID = Convert.ToInt32(id) };
            city.GetById();
            if (city.DistrictID == 0) return result;
            result = String.Format("{0}", DistrictIDToDistrictName(city.DistrictID));

            return result;
        }

        public static String CityIDToRegionName(string id)
        {
            var result = String.Empty;
            if (id == "0" || id == "-1" || String.IsNullOrEmpty(id)) return result;
            var city = new City { ID = Convert.ToInt32(id) };
            city.GetById();
            if (city.RegionID == 0) return result;
            result = String.Format("{0}", RegionIDToRegionName(city.RegionID));

            return result;
        }

        public static String CityNameFilter(string cityName)
        {
            return cityName
                .Replace("гп.", "")
                .Replace("аг.", "")
                .Replace("г.", "")
                .Replace("г.", "")
                .Replace("х.","")
                .Replace("д.","")
                .Replace("с.","")
                .Replace("п.","")
                .Replace("а.","")
                .Trim();
        }


        public static String CityIDToCityNameWithotCustom(string id)
        {

            var city = new City { ID = Convert.ToInt32(id) };
            city.GetById();

            return city.Name;
        }

        public static String CityIDToCityNameForMap(string id)
        {
            var city = new City { ID = Convert.ToInt32(id) };
            city.GetById();
            var result = String.Format("{0}", city.Name);
            if (city.DistrictID == 0) return result;
            result = String.Format("{0}, {1} р/н", city.Name, DistrictIDToDistrictName(city.DistrictID));
            return result;
        }

        public static String CityIDToDistance(string id, string ticketId)
        {
            string result;
            if (id == "0" || id == "-1")
            {
                result = "_____";
            }
            else
            {
                var city = new City { ID = Convert.ToInt32(id) };
                city.GetById();
                result = city.Distance.ToString();
            }

            return result;
        }

        public static String TrackIDToTrackName(int? trackId)
        {
            if (trackId == 0 || trackId == -1)
            { 
                    return String.Empty;
            }

            var track = new Tracks { ID = Convert.ToInt32(trackId) };
            track.GetById();
            return track.Name;
        }

        public static String CityToTrack(int? val, string ticketid)
        {
            if (val == 0 || val == -1)
            {
                var ticket = new Tickets() { ID = Convert.ToInt32(ticketid) };
                ticket.GetById();
                if (ticket.TrackIDUser == 0)
                {
                    return String.Empty;
                }
                var region = new Tracks() { ID = Convert.ToInt32(ticket.TrackIDUser) };
                region.GetById();
                return region.Name;
            }
            var city = new City { ID = Convert.ToInt32(val) };
            city.GetById();
            if (city.TrackID == 0)
            {
                return String.Empty;
            }
            var track = new Tracks { ID = Convert.ToInt32(city.TrackID) };
            track.GetById();
            return track.Name;
        }

        public static String CityToTrackWithBrackets(int? val, string ticketid)
        {
            if (val == 0 || val == -1)
            {
                var ticket = new Tickets() { ID = Convert.ToInt32(ticketid) };
                ticket.GetById();
                if (ticket.TrackIDUser == 0)
                {
                    return String.Empty;
                }
                var region = new Tracks() { ID = Convert.ToInt32(ticket.TrackIDUser) };
                region.GetById();
                return String.Format("({0})", region.Name);
            }
            var city = new City { ID = Convert.ToInt32(val) };
            city.GetById();
            if (city.TrackID == 0)
            {
                return String.Empty;
            }
            var track = new Tracks { ID = Convert.ToInt32(city.TrackID) };
            track.GetById();
            return String.Format("({0})",track.Name);
        }

        public static String CityToTrackOperatorName(int? val)
        {
            var city = new City { ID = Convert.ToInt32(val) };
            city.GetById();
            if (city.TrackID == 0)
            {
                return String.Empty;
            }
            var track = new Tracks { ID = Convert.ToInt32(city.TrackID) };
            track.GetById();

            var manager = new Users() { ID = Convert.ToInt32(track.ManagerID) };
            manager.GetById();
            return !String.IsNullOrEmpty(manager.Name) ? String.Format("{0}", manager.Name) : String.Empty;
        }

        public static String CityToTrackOperatorPhone(int? val)
        {
            var city = new City { ID = Convert.ToInt32(val) };
            city.GetById();
            if (city.TrackID == 0)
            {
                return String.Empty;
            }
            var track = new Tracks { ID = Convert.ToInt32(city.TrackID) };
            track.GetById();

            var manager = new Users() { ID = Convert.ToInt32(track.ManagerID) };
            manager.GetById();

            if (!String.IsNullOrEmpty(manager.PhoneWorkOne)) return manager.PhoneWorkOne;
            if (!String.IsNullOrEmpty(manager.PhoneWorkTwo)) return manager.PhoneWorkTwo;
            return String.Empty;
        }

        public static String CityIDToAutocompleteString(City city)
        {
            string result;
            var villageCouncil = String.Empty;
            var district = String.Empty;
            var region = String.Empty;

            if (!String.IsNullOrEmpty(city.VillageCouncilName) && !String.IsNullOrEmpty(city.VillageCouncilName.Trim()))
            {
                villageCouncil = String.Format("{0} Совет", city.VillageCouncilName.Trim());
            }

            if (city.RegionID != 0)
            {
                region = String.Format("{0} обл.", RegionIDToRegionName(city.RegionID));
            }

            if (city.DistrictID != 0)
            {
                district = String.Format("{0} р/н", DistrictIDToDistrictName(city.DistrictID));
            }


            if (!String.IsNullOrEmpty(district) && !String.IsNullOrEmpty(region) && !String.IsNullOrEmpty(villageCouncil))
            {
                result = String.Format("{0} ({1}, {2}, {3})", city.Name, region, district, villageCouncil);
            }
            else
            {
                if (!String.IsNullOrEmpty(district) && !String.IsNullOrEmpty(region))
                {
                    result = String.Format("{0} ({1}, {2})", city.Name, region, district);
                }
                else
                {
                    if (!String.IsNullOrEmpty(region))
                    {
                        result = String.Format("{0} ({1})", city.Name, region);
                    }
                    else
                    {
                        result = String.Format("{0}", city.Name);
                    }
                }
            }

            result += " ID:" + city.ID;
            return result;
        }
    }
}