using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Delivery.DAL.Attributes;

namespace Delivery.DAL.DataBaseObjects
{
    public class City : IDataManager
    {
        public DataManager DM;

        public String TableName { get; set; }

        public City()
        {
            DM = new DataManager();
            this.TableName = "city";
            this.CreateDate = DateTime.Now;
            this.ChangeDate = DateTime.Now;
        }

        [DataBaseGet]
        public Int32 ID { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String Name { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? TrackID { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? RegionID { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? DistrictID { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String VillageCouncilName { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String SOATO { get; set; }
        
        [DataBaseSet]
        [DataBaseGet]
        public Int32? IsMainCity { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public DateTime? CreateDate { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public DateTime? ChangeDate { get; set; }
        
        [DataBaseSet]
        [DataBaseGet]
        public Int32? Distance { get; set; } // используется при печати некоторых документов
        
        [DataBaseSet]
        [DataBaseGet]
        public Int32? DistanceFromCity { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? Blocked { get; set; }

        public void Delete(Int32 id)
        {
            this.ID = id;
            DM.DeleteData(this);
        }

        public void Create()
        {
            DM.CreateData(this);
        }

        public DataSet GetAllItems()
        {
            var ds = DM.GetAllData(this, null, null, null);
            if (ds.Tables[0].Select("ID = -1").Any())
            {
                ds.Tables[0].Rows.Remove(ds.Tables[0].Select("ID = -1").First());
            }
            return ds;
        }

        public DataSet GetAllMainItems()
        {
            this.IsMainCity = 1;
            var ds = DM.GetAllData(this, null, null, "IsMainCity");
            if (ds.Tables[0].Select("ID = -1").Any())
            {
                ds.Tables[0].Rows.Remove(ds.Tables[0].Select("ID = -1").First());
            }
            return ds;
        }

        public DataSet GetAllItems(string orderBy, string direction, string whereField)
        {
            var ds = DM.GetAllData(this, orderBy, direction, whereField);
            if (ds.Tables[0].Select("ID = -1").Any() )
            {
                ds.Tables[0].Rows.Remove(ds.Tables[0].Select("ID = -1").First());
            }
            return ds;
        }

        public DataSet GetAllItemsWithoutBlocked(string orderBy, string direction, string whereField)
        {
            var ds = DM.GetAllData(this, orderBy, direction, whereField);
            if (ds.Tables[0].Select("ID = -1").Any())
            {
                ds.Tables[0].Rows.Remove(ds.Tables[0].Select("ID = -1").First());
            }
            foreach (var listItem in ds.Tables[0].Select("Blocked = 1").ToList())
            {
                ds.Tables[0].Rows.Remove(listItem);
            }
            return ds;
        }

        public dynamic GetById()
        {
            return DM.GetDataBy(this, "ID", null);
        }

        public void Update()
        {
            DM.UpdateDate(this);
        }

        public List<City> GetAllItemsToList()
        {
            var ds = DM.GetAllData(this, null, null, null);
            var cityList = new List<City>();
            foreach (DataRow city in ds.Tables[0].Rows)
            {
                var cityToList = new City()
                {
                    ID = Convert.ToInt32(city["ID"].ToString()),
                    Name = city["Name"].ToString(),
                    SOATO = city["SOATO"].ToString(),
                    RegionID = Convert.ToInt32(city["RegionID"].ToString()),
                    DistrictID = Convert.ToInt32(city["DistrictID"].ToString()),
                    VillageCouncilName = city["VillageCouncilName"].ToString(),
                    IsMainCity = Convert.ToInt32(city["IsMainCity"].ToString()),
                    Distance = (city["Distance"] != DBNull.Value) ? Convert.ToInt32(city["Distance"].ToString()) : -1,
                    DistanceFromCity = Convert.ToInt32(city["DistanceFromCity"].ToString()),
                    Blocked = Convert.ToInt32(city["Blocked"].ToString()),
                };
                cityList.Add(cityToList);
            }
            return cityList;
        }

        public static Dictionary<int, string> Regions = new Dictionary<int, string>()
        {
            {1, "Mинская"},
            {2, "Бpестская"},
            {3, "Гpодненская"},
            {4, "Mогилевская"},
            {5, "Гoмельская"},
            {6, "Bитебская"},
        };

        public static Dictionary<int, Districts> Districts = ((List<Districts>)HttpContext.Current.Application["Districts"]).ToDictionary(x => x.ID);
    }
}