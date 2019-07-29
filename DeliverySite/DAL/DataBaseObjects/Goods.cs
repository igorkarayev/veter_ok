using Delivery.DAL.Attributes;
using System;
using System.Configuration;
using System.Data;

namespace Delivery.DAL.DataBaseObjects
{
    public class Goods : IDataManager
    {
        public DataManager DM;

        public String TableName { get; set; }

        public string DenomDate;

        public string DenomCoeff;

        public Goods()
        {
            DM = new DataManager();
            this.TableName = "goods";
            this.CreateDate = DateTime.Now;
            this.ChangeDate = DateTime.Now;
            DenomDate = ConfigurationManager.AppSettings["denom:Date"];
            DenomCoeff = ConfigurationManager.AppSettings["denom:Coeff"];
        }

        [DataBaseGet]
        public Int32 ID { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String TicketFullSecureID { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String Description { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String Model { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? Number { get; set; }

        public Decimal? _cost { get; set; }
        [DataBaseSet]
        [DataBaseGet]
        public Decimal? Cost {
            get
            {
                var flagDate = ChangeDate ?? CreateDate;
                if (flagDate < Convert.ToDateTime(DenomDate))
                {
                    return _cost / Convert.ToInt32(DenomCoeff);
                }

                return _cost;
            }
            set { _cost = value; } }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? WithoutAkciza { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public DateTime? CreateDate { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public DateTime? ChangeDate { get; set; }

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
            var ds = DM.GetAllData(this, null, null, null); 
            return ds;
        }

        public DataSet GetAllItems(string orderBy, string direction, string whereField)
        {
            var ds = DM.GetAllData(this, orderBy, direction, whereField);
            return ds;
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