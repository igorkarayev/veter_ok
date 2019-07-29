using System;
using System.Collections.Generic;
using System.Data;
using Delivery.DAL.Attributes;

namespace Delivery.DAL.DataBaseObjects
{
    public class Reports : IDataManager
    {
        public DataManager DM;

        public String TableName { get; set; }

        public Reports()
        {
            DM = new DataManager();
            this.TableName = "reports";
            this.CreateDate = DateTime.Now;
        }

        [DataBaseGet]
        public Int32 ID { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? ReportType { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String FileName { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? DriverID { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String DriverName { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public DateTime? DocumentDate { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public DateTime? CreateDate { get; set; }

        public dynamic GetById()
        {
            return DM.GetDataBy(this, "ID", null);
        }

        public DataSet GetAllItems()
        {
            return DM.GetAllData(this, null, null, null);
        }

        public DataSet GetAllItems(string orderBy, string direction, string whereField)
        {
            return DM.GetAllData(this, orderBy, direction, whereField);
        }

        public void Update()
        {
            DM.UpdateDate(this);
        }
        public void Delete(Int32 id)
        {
            this.ID = id;
            DM.DeleteData(this);
        }

        public void Create()
        {
            DM.CreateData(this);
        }

        public static Dictionary<int, string> TypeAlias = new Dictionary<int, string>()
        {
            {0, "put_2"},
            {1, "zp_1"},
            {2, "naklpril_1"},
            {3, "put_1"},
            {4, "put_3"},
            {5, "act"},
            {6, "raschet"}
        };

        public static Dictionary<int, string> TypeNames = new Dictionary<int, string>()
        {
            {0, "Путевой лист #2"},
            {1, "Заказ-поручение #1"},
            {2, "Приложение к накладной #1"},
            {3, "Путевой лист #1"},
            {4, "Путевой лист #3"},
            {5, "Акт выполненных работ"},
            {6, "Расчетный лист"}
        };

    }
}