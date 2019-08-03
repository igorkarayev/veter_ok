using Delivery.DAL.Attributes;
using DeliverySite.Resources;
using System;
using System.Collections.Generic;
using System.Data;
using Delivery.Resources;

namespace Delivery.DAL.DataBaseObjects
{
    public class Users : IDataManager
    {
        public DataManager DM;

        public String TableName { get; set; }

        public Users()
        {
            DM = new DataManager();
            this.TableName = "users";
            this.CreateDate = DateTime.Now;
            this.ChangeDate = DateTime.Now;
        }

        [DataBaseGet]
        public Int32 ID { get; set; }

        [DataBaseGet]
        [DataBaseSet]
        public String Login { get; set; }

        [DataBaseGet]
        [DataBaseSet]
        public String Password { get; set; }

        [DataBaseGet]
        [DataBaseSet]
        public String Name { get; set; }

        [DataBaseGet]
        [DataBaseSet]
        public String Family { get; set; }

        [DataBaseGet]
        [DataBaseSet]
        public String Email { get; set; }

        [DataBaseGet]
        [DataBaseSet]
        public String Role { get; set; }

        [DataBaseGet]
        [DataBaseSet]
        public Int32? Status { get; set; }

        [DataBaseGet]
        [DataBaseSet]
        public String Phone { get; set; }

        [DataBaseGet]
        [DataBaseSet]
        public String PhoneWorkOne { get; set; }

        [DataBaseGet]
        [DataBaseSet]
        public String PhoneWorkTwo { get; set; }

        [DataBaseGet]
        [DataBaseSet]
        public String PhoneHome { get; set; }

        [DataBaseGet]
        [DataBaseSet]
        public String Address { get; set; }

        [DataBaseGet]
        [DataBaseSet]
        public String Note { get; set; }

        [DataBaseGet]
        [DataBaseSet]
        public DateTime? CreateDate { get; set; }

        [DataBaseGet]
        [DataBaseSet]
        public DateTime? ChangeDate { get; set; }

        [DataBaseGet]
        [DataBaseSet]
        public Int32? IsCourse { get; set; }

        [DataBaseGet]
        [DataBaseSet]
        public Int32? SpecialClient { get; set; }

        [DataBaseGet]
        [DataBaseSet]
        public Int32? RedClient { get; set; }

        [DataBaseGet]
        [DataBaseSet]
        public Int32? ManagerID { get; set; }

        [DataBaseGet]
        [DataBaseSet]
        public Int32? SalesManagerID { get; set; }

        [DataBaseGet]
        [DataBaseSet]
        public Decimal? Discount { get; set; }

        [DataBaseGet]
        [DataBaseSet]
        public Int32? AccessOnlyByWhiteList { get; set; }

        [DataBaseGet]
        [DataBaseSet]
        public String PassportSeria { get; set; }

        [DataBaseGet]
        [DataBaseSet]
        public String PassportNumber { get; set; }

        [DataBaseGet]
        [DataBaseSet]
        public String PersonalNumber { get; set; }

        [DataBaseGet]
        [DataBaseSet]
        public String ROVD { get; set; }

        [DataBaseGet]
        [DataBaseSet]
        public DateTime? Validity { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public DateTime? DateOfIssue { get; set; }

        [DataBaseGet]
        [DataBaseSet]
        public String RegistrationAddress { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public DateTime? BirthDay { get; set; }

        [DataBaseGet]
        [DataBaseSet]
        public String Skype { get; set; }

        [DataBaseGet]
        [DataBaseSet]
        public String ApiKey { get; set; }

        [DataBaseGet]
        [DataBaseSet]
        public Int32? AllowApi { get; set; }

        [DataBaseGet]
        [DataBaseSet]
        public Int32? SenderCityID { get; set; }

        [DataBaseGet]
        [DataBaseSet]
        public String SenderStreetPrefix { get; set; }

        [DataBaseGet]
        [DataBaseSet]
        public String SenderStreetName { get; set; }

        [DataBaseGet]
        [DataBaseSet]
        public String SenderStreetNumber { get; set; }

        [DataBaseGet]
        [DataBaseSet]
        public String SenderApartmentNumber { get; set; }

        [DataBaseGet]
        [DataBaseSet]
        public String SenderHousing { get; set; }

        [DataBaseGet]
        [DataBaseSet]
        public String SenderWharehouse { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public DateTime? ProcessedDate { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? StatusStady { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public DateTime? ContactDate { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public DateTime? ActivatedDate { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? CreateProduct { get; set; }

        public String RussRole { get; set; }

        public List<News> NotReadNews { get; set; }

        public bool PrilNaklTwoAccess { get; set; }

        public void Delete(Int32 id)
        {
            this.ID = id;
            //удаление привязанных профилей
            var userprofile = new UsersProfiles { UserID = id };
            var userprofilelist = userprofile.GetAllItemsByUserID();
            foreach (DataRow row in userprofilelist.Tables[0].Rows)
            {
                var userprofileToDelete = new UsersProfiles();
                userprofileToDelete.Delete(Convert.ToInt32(row["ID"]));
            }

            DM.DeleteData(this);
        }

        public void Delete(Int32 id, int curentUserId, string curentUserIp, string curentPageName)
        {
            this.ID = id;
            //удаление привязанных профилей
            var userprofile = new UsersProfiles { UserID = id };
            var userprofilelist = userprofile.GetAllItemsByUserID();
            foreach (DataRow row in userprofilelist.Tables[0].Rows)
            {
                var userprofileToDelete = new UsersProfiles();
                userprofileToDelete.Delete(Convert.ToInt32(row["ID"]), curentUserId, curentUserIp, curentPageName);
            }

            DM.DeleteData(this, curentUserId, curentUserIp, curentPageName);
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

        public dynamic GetByIdAndRole()
        {
            return DM.GetDataBy(this, "ID", "Role");
        }

        public dynamic GetByEmail()
        {
            return DM.GetDataBy(this, "Email", null);
        }

        public dynamic GetByApiKey()
        {
            return DM.GetDataBy(this, "ApiKey", null);
        }

        public dynamic GetByLogin()
        {
            return DM.GetDataBy(this, "Login", null);
        }

        public dynamic GetByLogin(string _login)
        {
            this.Login = _login;
            return DM.GetDataBy(this, "Login", null);
        }

        public void Update(int curentUserId, string curentUserIp, string curentPageName)
        {
            DM.UpdateDate(this, curentUserId, curentUserIp, curentPageName);
        }

        public void Update()
        {
            DM.UpdateDate(this);
        }

        public DataSet GetAllItemsByRole()
        {
            return DM.GetAllData(this, null, null, "Role");
        }

        public enum Roles
        {
            SuperAdmin,
            Admin,
            Manager,
            SalesManager, //ìàíàãåð ïî ïðîäàæàì
            Operator,
            Cashier, //êàññèð
            Booker, //áóõãàëòåð
            User,
            Logistician, //ëîãèñò
            Storekeeper, //êëàäîâùèê
        };

        public static Dictionary<int, string> UserStatuses = new Dictionary<int, string>()
        {
            {1, UserStatusesResources.NotActivated},
            {2, UserStatusesResources.Activated},
            {3, UserStatusesResources.Blocked},
        };

        public static Dictionary<int, string> UserStatusesStudy = new Dictionary<int, string>()
        {
            {1, UserStatusesResources.NotProcessed},
            {2, UserStatusesResources.InProcess},
            {3, UserStatusesResources.Processed},
        };
    }
}

