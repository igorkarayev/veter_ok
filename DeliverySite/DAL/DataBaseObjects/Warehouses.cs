using System;
using System.Collections.Generic;
using System.Data;
using Delivery.DAL;
using Delivery.DAL.Attributes;
using DeliverySite.Resources;

namespace Delivery.DAL.DataBaseObjects
{
    public class Warehouses: IDataManager
    {
        public DataManager DM;

        public String TableName { get; set; }

        public Warehouses()
        {
            DM = new DataManager();
            this.TableName = "warehouses";
            this.CreateDate = DateTime.Now;
            this.ChangeDate = DateTime.Now;
        }

        [DataBaseGet]
        public Int32 ID { get; set; }

        

        [DataBaseSet]
        [DataBaseGet]
        public DateTime? CreateDate { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public DateTime? ChangeDate { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String Name { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String StreetPrefix { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String StreetName { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String StreetNumber { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String Housing { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String ApartmentNumber { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? CityID { get; set; }

        public void Delete(Int32 id)
        {
            this.ID = id;
            DM.DeleteData(this);
        }

        public void Delete(Int32 id, int userId, string userIp, string pageName)
        {
            this.ID = id;
            DM.DeleteData(this, userId, userIp, pageName);
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

        public void Update(int userId, string userIp, string pageName)
        {
            DM.UpdateDate(this, userId, userIp, pageName);
        }
    }
}