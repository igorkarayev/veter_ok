using Delivery.DAL.Attributes;
using System;
using System.Data;

namespace Delivery.DAL.DataBaseObjects
{
    public class Titles : IDataManager
    {
        public DataManager DM;

        public String TableName { get; set; }

        public Titles()
        {
            DM = new DataManager();
            this.TableName = "titles";
        }

        [DataBaseGet]
        public Int32 ID { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32 CategoryID { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String Name { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Double? MarginCoefficient { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Double? WeightMin { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Double? WeightMax { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? Additive { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? CanBeWithoutAkciza { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Double? AdditiveCostWithoutAkciza { get; set; }

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
            return DM.GetAllData(this, null, null, null);
        }

        public DataSet GetAllItems(string orderBy, string direction, string whereField)
        {
            return DM.GetAllData(this, orderBy, direction, whereField);
        }

        public dynamic GetById()
        {
            return DM.GetDataBy(this, "ID", null);
        }

        public dynamic GetByName()
        {
            return DM.GetDataBy(this, "Name", null);
        }

        public void Update()
        {
            DM.UpdateDate(this);
        }
    }
}