using Delivery.DAL.Attributes;
using DeliverySite.Resources;
using System;
using System.Collections.Generic;
using System.Data;
using Delivery.Resources;

namespace Delivery.DAL.DataBaseObjects
{
    public class UsersProfiles : IDataManager
    {
        public DataManager DM;

        public String TableName { get; set; }

        public UsersProfiles()
        {
            DM = new DataManager();
            this.TableName = "usersprofiles";
            this.CreateDate = DateTime.Now;
            this.ChangeDate = DateTime.Now;
        }

        [DataBaseGet]
        public Int32 ID { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? UserID { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? TypeID { get; set; }

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
        public String DirectorPhoneNumber { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String PassportNumber { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String PassportSeria { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String PassportData { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public DateTime? PassportDate { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String Address { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String CompanyName { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String CompanyAddress { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String PostAddress { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String RasShet { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String UNP { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String BankName { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String BankAddress { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String BankCode { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public DateTime? CreateDate { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public DateTime? ChangeDate { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? IsDefault { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String ContactPersonFIO { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String ContactPhoneNumbers { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? StatusID { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String RejectBlockedMessage { get; set; }

        [DataBaseGet]
        [DataBaseSet]
        public String AgreementNumber { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public DateTime? AgreementDate { get; set; }

        public void Delete(Int32 id)
        {
            this.ID = id;

            //удаление прив¤занных за¤вок
            var profileForDelete = new UsersProfiles() { ID = id };
            profileForDelete.GetById();
            var userTickets = new Tickets() { UserProfileID = profileForDelete.ID };
            var userTicketsList = userTickets.GetAllItemsByUserProfileID();
            foreach (DataRow row in userTicketsList.Tables[0].Rows)
            {
                var userTicketsToDelete = new Tickets();
                userTicketsToDelete.Delete(Convert.ToInt32(row["ID"]));
            }

            DM.DeleteData(this);
        }

        public void Delete(Int32 id, int curentUserId, string curentUserIp, string curentPageName)
        {
            this.ID = id;

            //удаление привязанных за¤вок
            var profileForDelete = new UsersProfiles() { ID = id };
            profileForDelete.GetById();
            var userTickets = new Tickets() { UserProfileID = profileForDelete.ID };
            var userTicketsList = userTickets.GetAllItemsByUserProfileID();
            foreach (DataRow row in userTicketsList.Tables[0].Rows)
            {
                var userTicketsToDelete = new Tickets();
                userTicketsToDelete.Delete(Convert.ToInt32(row["ID"]), curentUserId, curentUserIp, curentPageName, row["FullSecureID"].ToString());
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

        public DataSet GetAllItemsByUserID()
        {
            return DM.GetAllData(this, "FirstName", "ASC", "UserID");
        }

        public DataSet GetAllItems(string orderBy, string direction, string whereField)
        {
            return DM.GetAllData(this, orderBy, direction, whereField);
        }

        public dynamic GetById()
        {
            return DM.GetDataBy(this, "ID", null);
        }

        public dynamic GetByUserIDAndDefault()
        {
            return DM.GetDataBy(this, "UserID", "IsDefault");
        }

        public void Update()
        {
            DM.UpdateDate(this);
        }

        public void Update(int curentUserId, string curentUserIp, string curentPageName)
        {
            DM.UpdateDate(this, curentUserId, curentUserIp, curentPageName);
        }

        public static Dictionary<int, string> ProfileStatuses = new Dictionary<int, string>()
        {
            {0, UserProfileStatusesResources.NotProcessed},
            {1, UserProfileStatusesResources.Active},
            {2, UserProfileStatusesResources.Rejected},
            {3, UserProfileStatusesResources.Blocked},
        };

        public static Dictionary<int, string> ProfileType = new Dictionary<int, string>()
        {
            {1, "Физическое лицо"},
            {2, "Юридическое лицо"},
            {3, "Интернет-магазин"},
        };

        public static Dictionary<int, string> CompanyType = new Dictionary<int, string>()
        {
            {0, "ИП"},
            {1, "ООО"},
            {2, "ОАО"},
            {3, "ЗАО"},
            {4, "ОДО"},
            {5, "СЗАО"},
            {6, "ЧУП"},
            {7, "ЧТУП"},
            {8, "ЧПУП"},
            {9, "СООО"},
        };
    }
}