using Delivery.DAL.Attributes;
using System;
using System.Data;

namespace Delivery.DAL.DataBaseObjects
{
    public class Pages : IDataManager
    {
        public DataManager DM;

        public String TableName { get; set; }

        public Pages()
        {
            DM = new DataManager();
            this.TableName = "pages";
            this.ChangeDate = DateTime.Now;
        }

        [DataBaseGet]
        public Int32 ID { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String PageName { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String PageTitle { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String Content { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public DateTime? ChangeDate { get; set; }

        public dynamic GetById()
        {
            return DM.GetDataBy(this, "ID", null);
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

    }
}