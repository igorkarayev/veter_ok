using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Delivery.DAL.Attributes;

namespace Delivery.DAL.DataBaseObjects
{
    public class Districts : IDataManager
    {
        public DataManager DM;

        public String TableName { get; set; }

        public Districts()
        {
            DM = new DataManager();
            this.TableName = "districts";
            this.ChangeDate = DateTime.Now;
        }

        [DataBaseGet]
        public Int32 ID { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String Name { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? DeliveryTerms { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? Monday { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? Tuesday { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? Wednesday { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? Thursday { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? Friday { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? Saturday { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? Sunday { get; set; }

        [DataBaseGet]
        [DataBaseSet]
        public DateTime? ChangeDate { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? TrackID { get; set; }

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

        public List<Districts> GetAllItemsToList()
        {
            var ds = DM.GetAllData(this, null, null, null);
            var districtList = new List<Districts>();
            foreach (DataRow district in ds.Tables[0].Rows)
            {
                var districtToList = new Districts()
                {
                    ID = Convert.ToInt32(district["ID"].ToString()),
                    Name = district["Name"].ToString(),
                    DeliveryTerms = Convert.ToInt32(district["DeliveryTerms"].ToString()),
                    Monday = Convert.ToInt32(district["Monday"].ToString()),
                    Tuesday = Convert.ToInt32(district["Tuesday"].ToString()),
                    Wednesday = Convert.ToInt32(district["Wednesday"].ToString()),
                    Thursday = Convert.ToInt32(district["Thursday"].ToString()),
                    Friday = Convert.ToInt32(district["Friday"].ToString()),
                    Saturday = Convert.ToInt32(district["Saturday"].ToString()),
                    Sunday = Convert.ToInt32(district["Sunday"].ToString()),
                    TrackID = Convert.ToInt32(district["TrackID"].ToString()),
                };
                districtList.Add(districtToList);
            }
            return districtList;
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

    }
}