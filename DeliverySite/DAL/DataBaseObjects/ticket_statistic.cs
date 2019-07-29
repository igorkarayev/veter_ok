using Delivery.DAL.Attributes;
using DeliverySite.Resources;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using Delivery.Resources;

namespace Delivery.DAL.DataBaseObjects
{
    public class Ticket_statistic : IDataManager
    {
        public DataManager DM;

        public string TableName { get; set; }


        public Ticket_statistic()
        {
            DM = new DataManager();
            this.TableName = "ticket_statistic";
            this.ChangeDate = DateTime.Now;
            UserID = null;
        }

        [DataBaseGet]
        public Int32 ID { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String SecureID { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? UserID { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String Status { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Decimal? AgreedCost { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Decimal? GruzobozCost { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Decimal? AssessedCost { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public DateTime? ChangeDate { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String Goods { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String Changes { get; set; }

        public void Delete(Int32 id)
        {
            this.ID = id;
            var ticketForDelete = new Ticket_statistic() { ID = id };
            ticketForDelete.GetById();

            DM.DeleteData(this);
        }

        public void Delete(Int32 id, int userId, string userIp, string pageName, string fullsecureid)
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

        public DataSet GetAllItemsByUserID()
        {
            return DM.GetAllData(this, "ID", "ASC", "UserID");
        }

        public dynamic GetBySecureId()
        {
            return DM.GetDataBy(this, "SecureID", null);
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


        public static Dictionary<int, string> ProfileType = new Dictionary<int, string>()
        {
            {1, "Физическое лицо"},
            {2, "Юридическое лицо"},
            {3, "Интернет-магазин"},
        };        

        public static Dictionary<int, string> TicketStatuses = new Dictionary<int, string>()
        {
            {1, TicketStatusesResources.NotProcessed},
            {2, TicketStatusesResources.InStock},
            {4, TicketStatusesResources.Transfer_InStock},
            {3, TicketStatusesResources.OnTheWay},
            {5, TicketStatusesResources.Processed},
            {12, TicketStatusesResources.Delivered},
            {6, TicketStatusesResources.Completed},
            {11, TicketStatusesResources.Transfer_InCourier},
            {7, TicketStatusesResources.Refusing_InCourier},
            {13, TicketStatusesResources.Exchange_InCourier},
            {14, TicketStatusesResources.DeliveryFromClient_InCourier},
            {8, TicketStatusesResources.Return_InStock},
            {9, TicketStatusesResources.Cancel_InStock},
            {15, TicketStatusesResources.Exchange_InStock},
            {16, TicketStatusesResources.DeliveryFromClient_InStock},
            {10, TicketStatusesResources.Cancel},
            {17, TicketStatusesResources.Refusal_OnTheWay},
            {18, TicketStatusesResources.Refusal_ByAddress},
            {19, TicketStatusesResources.Upload},
        };

        public static Dictionary<int, string> TicketStatusesMale = new Dictionary<int, string>()
        {
            {1, TicketStatusesResourcesMale.NotProcessed},
            {2, TicketStatusesResourcesMale.InStock},
            {4, TicketStatusesResourcesMale.Transfer_InStock},
            {3, TicketStatusesResourcesMale.OnTheWay},
            {5, TicketStatusesResourcesMale.Processed},
            {12, TicketStatusesResourcesMale.Delivered},
            {6, TicketStatusesResourcesMale.Completed},
            {11, TicketStatusesResourcesMale.Transfer_InCourier},
            {7, TicketStatusesResourcesMale.Refusing_InCourier},
            {13, TicketStatusesResourcesMale.Exchange_InCourier},
            {14, TicketStatusesResourcesMale.DeliveryFromClient_InCourier},
            {8, TicketStatusesResourcesMale.Return_InStock},
            {9, TicketStatusesResourcesMale.Cancel_InStock},
            {15, TicketStatusesResourcesMale.Exchange_InStock},
            {16, TicketStatusesResourcesMale.DeliveryFromClient_InStock},
            {10, TicketStatusesResourcesMale.Cancel},
            {17, TicketStatusesResourcesMale.Refusal_OnTheWay},
            {18, TicketStatusesResourcesMale.Refusal_ByAddress},
            {19, TicketStatusesResourcesMale.Upload},
        };
    }
}