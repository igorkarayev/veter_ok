using System;
using System.Data;
using Delivery.DAL.Attributes;

namespace Delivery.DAL.DataBaseObjects
{
    public class ApiLog : IDataManager
    {
        public DataManager DM;

        public String TableName { get; set; }

        public ApiLog()
        {
            DM = new DataManager();
            this.TableName = "apilog";
            this.CreateDate = DateTime.Now;
        }

        [DataBaseGet]
        public Int32 ID { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? UserID { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String UserIP { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String ApiKey { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String MethodName { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String ApiName { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String ApiType { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String IncomingParameters { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32 ResponseBodyLenght { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public DateTime? CreateDate { get; set; }

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

    }
}