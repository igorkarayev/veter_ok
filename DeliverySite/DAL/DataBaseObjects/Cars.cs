using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using Delivery.DAL.Attributes;

namespace Delivery.DAL.DataBaseObjects
{
    public class Cars : IDataManager
    {
        public DataManager DM;

        public String TableName { get; set; }

        public Cars()
        {
            DM = new DataManager();
            this.TableName = "cars";
            this.CreateDate = DateTime.Now;
            this.ChangeDate = DateTime.Now;
        }

        [DataBaseGet]
        public Int32 ID { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String Number { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String Model { get; set; }


        [DataBaseSet]
        [DataBaseGet]
        public Int32? TypeID { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public DateTime? CreateDate { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public DateTime? ChangeDate { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String CompanyName { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String FirstName { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String LastName { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String ThirdName { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String PassportSeria { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String PassportNumber { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String PersonalNumber { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String ROVD { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public DateTime? Validity { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public DateTime? DateOfIssue { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public DateTime? BirthDay { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String RegistrationAddress { get; set; }

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
            return DM.GetAllData(this, null, null, null);
        }

        public DataSet GetAllItems(string orderBy, string direction, string whereField)
        {
            return DM.GetAllData(this, orderBy, direction, whereField);
        }

        public DataSet GetAllByNumber()
        {
            return DM.GetAllData(this, "Model", "ASC", "Number");
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

        public static Dictionary<int, string> CarType = new Dictionary<int, string>()
        {
            {1, "Физическое лицо"},
            {2, "Юридическое лицо"},
        };
    }

}