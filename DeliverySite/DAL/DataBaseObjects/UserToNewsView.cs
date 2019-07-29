using System;
using System.Collections.Generic;
using System.Data;
using Delivery.DAL.Attributes;
using DeliverySite.Resources;

namespace Delivery.DAL.DataBaseObjects
{
    public class UserToNewsView : IDataManager
    {
        public DataManager DM;

        public String TableName { get; set; }

        public UserToNewsView()
        {
            DM = new DataManager();
            this.TableName = "usertonewsview";
            this.CreateDate = DateTime.Now;
        }

        [DataBaseGet]
        public Int32 ID { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public DateTime? CreateDate { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32 UserID { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32 NewsID { get; set; }

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

        public DataSet GetAllByUserID()
        {
            return DM.GetAllData(this, "ID", "DESC", "UserID");
        }

        public DataSet GetAllByNewsID()
        {
            return DM.GetAllData(this, "ID", "DESC", "NewsID");
        }

        public void Update()
        {
            DM.UpdateDate(this);
        }

    }
}