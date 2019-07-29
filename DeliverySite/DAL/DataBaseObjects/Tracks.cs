using System;
using System.Data;
using Delivery.DAL;
using Delivery.DAL.Attributes;

namespace Delivery.DAL.DataBaseObjects
{
    public class Tracks : IDataManager
    {
        public DataManager DM;

        public String TableName { get; set; }

        public Tracks()
        {
            DM = new DataManager();
            this.TableName = "tracks";
        }

        [DataBaseGet]
        public Int32 ID { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String Name { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32 ManagerID { get; set; }

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

        public void Update()
        {
            DM.UpdateDate(this);
        }
    }
}