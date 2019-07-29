using Delivery.DAL.Attributes;
using System;
using System.Collections.Generic;
using System.Data;

namespace Delivery.DAL.DataBaseObjects
{
    public class Providers : IDataManager
    {
        public DataManager DM;

        public string TableName { get; set; }

        public Providers()
        {
            DM = new DataManager();
            this.TableName = "providers";
            this.CreateDate = DateTime.Now;
            this.ChangeDate = DateTime.Now;
        }

        [DataBaseGet]
        public int ID { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public string NamePrefix { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public string Name { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public string ContactFIO { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public string ContactPhone { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public string TypesOfProducts { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public int CityID { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public string Address { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public string Note { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public DateTime? CreateDate { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public DateTime? ChangeDate { get; set; }

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

        public void Create()
        {
            DM.CreateData(this);
        }

        public void Update()
        {
            DM.UpdateDate(this);
        }

        public void Update(int userId, string userIp, string pageName)
        {
            DM.UpdateDate(this, userId, userIp, pageName);
        }

        public void Delete(int id)
        {
            this.ID = id;
            DM.DeleteData(this);
        }

        public void Delete(int id, int userId, string userIp, string pageName)
        {
            this.ID = id;
            DM.DeleteData(this, userId, userIp, pageName);
        }

        public static Dictionary<int, string> NamePrefixes = new Dictionary<int, string>()
        {
            {1, "ИП"},
            {2, "ЧУП"},
            {3, "ЧТУП"},
            {4, "ООО"},
            {5, "ОАО"},
            {6, "ЗАО"},
        };
    }
}