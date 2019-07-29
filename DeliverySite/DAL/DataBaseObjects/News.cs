using System;
using System.Collections.Generic;
using System.Data;
using Delivery.DAL.Attributes;
using Delivery.Resources;
using DeliverySite.Resources;

namespace Delivery.DAL.DataBaseObjects
{
    public class News : IDataManager
    {
        public DataManager DM;

        public String TableName { get; set; }

        public News()
        {
            DM = new DataManager();
            this.TableName = "news";
            this.CreateDate = DateTime.Now;
            this.ChangeDate = DateTime.Now;
        }

        [DataBaseGet]
        public Int32 ID { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String Title { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String TitleUrl { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String Body { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public DateTime? CreateDate { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public DateTime? ChangeDate { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32 NewsTypeID { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32 ForViewing { get; set; }

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

        public DataSet GetLastItems(int count)
        {
            return DM.GetLastData(this, count);
        }

        public DataSet GetLastItemsBy(int count, string fild)
        {
            return DM.GetLastDataBy(this, count, fild);
        }

        public DataSet GetAllItems(string orderBy, string direction, string whereField)
        {
            return DM.GetAllData(this, orderBy, direction, whereField);
        }

        public dynamic GetById()
        {
            return DM.GetDataBy(this, "ID", null);
        }

        public DataSet GetAllByType()
        {
            return DM.GetAllData(this, "ID", "DESC", "NewsTypeID");
        }

        public dynamic GetByTitleUrl()
        {
            return DM.GetDataBy(this, "TitleUrl", null);
        }

        public void Update()
        {
            DM.UpdateDate(this);
        }

        public static Dictionary<int, string> NewsTypes = new Dictionary<int, string>()
        {
            {0, NewsTypesResources.Guest},
            {1, NewsTypesResources.Employee},
            {2, NewsTypesResources.Client},
        };
    }
}