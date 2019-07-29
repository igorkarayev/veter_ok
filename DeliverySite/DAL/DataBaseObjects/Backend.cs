using System;
using System.Collections.Generic;
using System.Data;
using Delivery.DAL.Attributes;

namespace Delivery.DAL.DataBaseObjects
{
    public class Backend : IDataManager
    {
        public DataManager DM;

        public String TableName { get; set; }

        public Backend()
        {
            DM = new DataManager();
            this.TableName = "backend";
        }

        [DataBaseGet]
        public Int32 ID { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String Value { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String Tag { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String Description { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public DateTime? ChangeDate { get; set; }

        public dynamic GetById()
        {
            return DM.GetDataBy(this, "ID", null);
        }

        public dynamic GetByTag()
        {
            return DM.GetDataBy(this, "Tag", null);
        }

        public DataSet GetAllItems()
        {
            return DM.GetAllData(this, null, null, null);
        }

        public DataSet GetAllItems(string orderBy, string direction, string whereField)
        {
            return DM.GetAllData(this, orderBy, direction, whereField);
        }

        public void Update()
        {
            DM.UpdateDate(this);
        }
        public void Delete(Int32 id)
        {
            this.ID = id;
            DM.DeleteData(this);
        }

        public void Create()
        {
            DM.CreateData(this);
        }

        public List<Backend> GetAllItemsToList()
        {
            var ds = DM.GetAllData(this, null, null, null);
            var backendList = new List<Backend>();
            foreach (DataRow city in ds.Tables[0].Rows)
            {
                var cityToList = new Backend()
                {
                    ID = Convert.ToInt32(city["ID"].ToString()),
                    Value = city["Value"].ToString(),
                    Tag = city["Tag"].ToString()
                };
                backendList.Add(cityToList);
            }
            return backendList;
        }

    }
}