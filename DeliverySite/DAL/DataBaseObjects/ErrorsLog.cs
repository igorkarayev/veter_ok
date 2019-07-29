using System;
using System.Data;
using Delivery.DAL.Attributes;

namespace Delivery.DAL.DataBaseObjects
{
    public class ErrorsLog : IDataManager
    {
        public DataManager DM;

        public String TableName { get; set; }

        public ErrorsLog()
        {
            DM = new DataManager();
            this.TableName = "errorslog";
            this.Date = DateTime.Now;
        }

        [DataBaseGet]
        public Int32 ID { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String StackTrase { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? UserID { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String ErrorType { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String IP { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public DateTime? Date { get; set; }

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