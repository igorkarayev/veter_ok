using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Delivery.DAL.Attributes;

namespace Delivery.DAL.DataBaseObjects
{
    public class IssuanceLists : IDataManager
    {
        public DataManager DM;

        public String TableName { get; set; }

        public IssuanceLists()
        {
            DM = new DataManager();
            this.TableName = "issuancelists";
            this.CreateDate = DateTime.Now;
        }

        [DataBaseGet]
        public Int32 ID { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String Comment { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? UserID { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? IssuanceListsStatusID { get; set; }


        [DataBaseSet]
        [DataBaseGet]
        public DateTime? CreateDate { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public DateTime? IssuanceDate { get; set; }
        

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

        public static Dictionary<int, string> IssuanceListsStatuses = new Dictionary<int, string>()
        {
            {1, "Открыт"},
            {2, "Закрыт"},
            {3, "Переоткрыт"},
        };

    }
}