using Delivery.DAL.Attributes;
using System;
using System.Data;

namespace Delivery.DAL.DataBaseObjects
{
    public class Category : IDataManager
    {
        public DataManager DM;

        public String TableName { get; set; }

        public Category()
        {
            DM = new DataManager();
            this.TableName = "category";
        }

        [DataBaseGet]
        public Int32 ID { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String Name { get; set; }

        public void Delete(Int32 id, int userId, string userIp, string pageName)
        {
            this.ID = id;
            DM.DeleteData(this, userId, userIp, pageName);
            DM.QueryWithoutReturnData(null, String.Format("DELETE FROM `userstocategory` WHERE `CategoryID` = {0}", id));
        }

        public void Delete(Int32 id)
        {
            this.ID = id;
            DM.DeleteData(this);
            DM.QueryWithoutReturnData(null, String.Format("DELETE FROM `userstocategory` WHERE `CategoryID` = {0}", id));
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

        public void Update(int userId, string userIp, string pageName)
        {
            DM.UpdateDate(this, userId, userIp, pageName);
        }
    }
}