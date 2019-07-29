using System;
using System.Data;
using Delivery.DAL;
using Delivery.DAL.Attributes;

namespace Delivery.DAL.DataBaseObjects
{
    public class Notification
    {
        public DataManager DM;

        public String TableName { get; set; }

        public Notification()
        {
            DM = new DataManager();
            this.TableName = "notifications";
            this.ChangeDate = DateTime.Now;
            this.Title = String.Empty;
        }

        [DataBaseGet]
        public Int32 ID { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String Title { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String Description { get; set; }

        [DataBaseGet]
        public String DescriptionStatic { get; set; }

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
            return               DM.GetAllData(this, orderBy, direction, whereField);
        }

        public void Update()
        {
            DM.UpdateDate(this);
        }
    }
}