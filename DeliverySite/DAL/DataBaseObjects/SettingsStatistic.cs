using Delivery.DAL.Attributes;
using DeliverySite.Resources;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using Delivery.Resources;

namespace Delivery.DAL.DataBaseObjects
{
    public class Settings_statistic : IDataManager
    {
        public DataManager DM;

        public string TableName { get; set; }


        public Settings_statistic()
        {
            DM = new DataManager();
            this.TableName = "settings_statistic";
            UserID = null;
        }

        [DataBaseGet]
        public Int32 ID { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? UserID { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String Email { get; set; }

        public void Delete(Int32 id)
        {
            this.ID = id;
            var ticketForDelete = new Ticket_statistic() { ID = id };
            ticketForDelete.GetById();

            DM.DeleteData(this);
        }

        public void Delete(Int32 id, int userId, string userIp, string pageName, string fullsecureid)
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

        public DataSet GetAllItemsByUserID()
        {
            return DM.GetAllData(this, "ID", "ASC", "UserID");
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