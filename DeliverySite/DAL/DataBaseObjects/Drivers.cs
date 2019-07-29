using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using Delivery.DAL.Attributes;
using Delivery.Resources;
using DeliverySite.Resources;

namespace Delivery.DAL.DataBaseObjects
{
    public class Drivers : IDataManager
    {
        public DataManager DM;

        public String TableName { get; set; }

        public Drivers()
        {
            DM = new DataManager();
            this.TableName = "drivers";
            this.CreateDate = DateTime.Now;
            this.ChangeDate = DateTime.Now;
        }

        [DataBaseGet]
        public Int32 ID { get; set; }

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
        public DateTime? CreateDate { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String PhoneOne { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String PhoneTwo { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String CityOrder { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public DateTime? ChangeDate { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? StatusID { get; set; }

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
        public String RegistrationAddress { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public DateTime? BirthDay { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? CarID { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public DateTime? DateOfIssue { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String HomeAddress { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String HomePhone { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String ContactPersonPhone { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String ContactPersonFIO { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String DriverPassport { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public DateTime? DriverPassportDateOfIssue { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public DateTime? DriverPassportValidity { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public DateTime? MedPolisDateOfIssue { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public DateTime? MedPolisValidity { get; set; }

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

        public DataSet GetAllActivatedDrivers()
        {
            return DM.GetAllData(this, "FirstName", "ASC", "StatusID");
        }

        public DataSet GetAllByDriverPassport()
        {
            return DM.GetAllData(this, "FirstName", "ASC", "DriverPassport");
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

        public static Dictionary<int, string> DriverStatuses = new Dictionary<int, string>()
        {
            {1, UserStatusesResources.Activated},
            {2, UserStatusesResources.Blocked},
        };
    }

    public class CityInOrder
    {
        public Int32 cityid { get; set; }
        public Int32 order { get; set; }
    }

    public class CityInOrderList
    {
        public List<CityInOrder> CityList { get; set; } 
    }

}