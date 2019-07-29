using Delivery.DAL.Attributes;
using DeliverySite.Resources;
using System;
using System.Collections.Generic;
using System.Data;
using Delivery.Resources;
using Delivery.DAL;

namespace Delivery.DAL.DataBaseObjects
{
    public class UserCost : IDataManager
    {
        public DataManager DM;

        public String TableName { get; set; }

        public UserCost()
        {
            DM = new DataManager();
            this.TableName = "usercost";
        }

        [DataBaseGet]
        public Int32 ID { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32 UserID { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? UserProfileID { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String Cost { get; set; }

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

        public string GetCostByUserID(int? id, int? profileid = null)
        {
            string sql = string.Empty;
            DataSet result;

            if (profileid != null)
            {
                sql = String.Format("SELECT * FROM {0} WHERE UserID = '{1}' AND UserProfileID = '{2}'", TableName, id, profileid);
                result = new DataManager().QueryWithReturnDataSet(sql);

                if (result.Tables[0].Rows.Count == 1)
                    return result.Tables[0].Rows[0].ItemArray[3].ToString();
            }

            sql = String.Format("SELECT * FROM {0} WHERE UserID = '{1}' AND UserProfileID IS NULL", TableName, id);

            result = new DataManager().QueryWithReturnDataSet(sql);

            if (result.Tables[0].Rows.Count == 1)
                return result.Tables[0].Rows[0].ItemArray[3].ToString();

            return null;
        }
    }
}