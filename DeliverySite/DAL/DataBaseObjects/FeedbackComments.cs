using System;
using System.Collections.Generic;
using System.Data;
using Delivery.DAL;
using Delivery.DAL.Attributes;
using DeliverySite.Resources;

namespace Delivery.DAL.DataBaseObjects
{
    public class FeedbackComments : IDataManager
    {
        public DataManager DM;

        public String TableName { get; set; }

        public FeedbackComments()
        {
            DM = new DataManager();
            this.TableName = "feedbackcomments";
            this.CreateDate = DateTime.Now;
            this.ChangeDate = DateTime.Now;
        }

        [DataBaseGet]
        public Int32 ID { get; set; }

        [DataBaseGet]
        [DataBaseSet]
        public Int32 FeedbackID { get; set; }

        [DataBaseGet]
        [DataBaseSet]
        public Int32 UserID { get; set; }

        [DataBaseGet]
        [DataBaseSet]
        public String Comment { get; set; }

        [DataBaseGet]
        [DataBaseSet]
        public DateTime? CreateDate { get; set; }

        [DataBaseGet]
        [DataBaseSet]
        public DateTime? ChangeDate { get; set; }

        [DataBaseGet]
        [DataBaseSet]
        public Int32 IsViewed { get; set; }

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

