using Delivery.DAL.Attributes;
using DeliverySite.Resources;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using Delivery.Resources;
using Delivery.BLL.StaticMethods;

namespace Delivery.DAL.DataBaseObjects
{
    public class Tickets : IDataManager
    {
        public DataManager DM;

        private decimal? _assessedCost;

        private decimal? _deliveryCost;

        private decimal? _agreedCost;

        private decimal? _receivedBLR;

        private decimal? _pril2Cost;

        private decimal? _gruzobozCost;

        public string TableName { get; set; }

        public string DenomDate;

        public string DenomCoeff;

        public Tickets()
        {
            DM = new DataManager();
            this.TableName = "tickets";
            this.CreateDate = DateTime.Now;
            this.ChangeDate = DateTime.Now;
            UserID = null;
            DenomDate = ConfigurationManager.AppSettings["denom:Date"];
            DenomCoeff = ConfigurationManager.AppSettings["denom:Coeff"];
        }

        [DataBaseGet]
        public Int32 ID { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? CreditDocuments { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? NoteChanged { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String SecureID { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String SenderProfileID { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String FullSecureID { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? UserID { get; set; }


        [DataBaseSet]
        [DataBaseGet]
        public Int32? UserProfileID { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? CityID { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? StatusID { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? IssuanceListID { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? StatusIDOld { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? DriverID { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String RecipientFirstName { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String RecipientLastName { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String RecipientThirdName { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Decimal? AssessedCost
        {
            get
            {
                var flagDate = ChangeDate ?? CreateDate;
                if (flagDate < Convert.ToDateTime(DenomDate))
                {
                    return _assessedCost / Convert.ToInt32(DenomCoeff);
                }

                return _assessedCost;
            }
            set { _assessedCost = value; }
        }

        [DataBaseSet]
        [DataBaseGet]
        public Decimal? DeliveryCost
        {
            get
            {
                var flagDate = ChangeDate ?? CreateDate;
                if (flagDate < Convert.ToDateTime(DenomDate))
                {
                    return _deliveryCost / Convert.ToInt32(DenomCoeff);
                }

                return _deliveryCost;
            }
            set { _deliveryCost = value; }
        }

        [DataBaseSet]
        [DataBaseGet]
        public Decimal? AgreedCost
        {
            get
            {
                var flagDate = ChangeDate ?? CreateDate;
                if (flagDate < Convert.ToDateTime(DenomDate))
                {
                    return _agreedCost / Convert.ToInt32(DenomCoeff);
                }

                return _agreedCost;
            }
            set { _agreedCost = value; }
        }

        [DataBaseSet]
        [DataBaseGet]
        public Decimal? ReceivedBLR
        {
            get
            {
                var flagDate = ChangeDate ?? CreateDate;
                if (flagDate < Convert.ToDateTime(DenomDate))
                {
                    return _receivedBLR / Convert.ToInt32(DenomCoeff);
                }

                return _receivedBLR;
            }
            set { _receivedBLR = value; }
        }

        [DataBaseSet]
        [DataBaseGet]
        public Decimal? Pril2Cost
        {
            get
            {
                var flagDate = ChangeDate ?? CreateDate;
                if (flagDate < Convert.ToDateTime(DenomDate))
                {
                    return _pril2Cost / Convert.ToInt32(DenomCoeff);
                }

                return _pril2Cost;
            }
            set { _pril2Cost = value; }
        }

        [DataBaseSet]
        [DataBaseGet]
        public Decimal? GruzobozCost
        {
            get
            {
                var flagDate = ChangeDate ?? CreateDate;
                if (flagDate < Convert.ToDateTime(DenomDate))
                {
                    return _gruzobozCost / Convert.ToInt32(DenomCoeff);
                }

                return _gruzobozCost;
            }
            set { _gruzobozCost = value; }
        }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? CourseRUR { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? CourseUSD { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? CourseEUR { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Decimal? ReceivedRUR { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Decimal? ReceivedUSD { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Decimal? ReceivedEUR { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public DateTime? DeliveryDate { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? BoxesNumber { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public DateTime? CreateDate { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public DateTime? ChangeDate { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public DateTime? CompletedDate { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public DateTime? ProcessedDate { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public DateTime? AdmissionDate { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? TrackIDUser { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? PrintNakl { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? Weight { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? PrintNaklInMap { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? NotPrintInPril2 { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? Pril2BoxesNumber { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? Pril2Weight { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? WithoutMoney { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? IsExchange { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? CheckedOut { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? Phoned { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public DateTime? OvDateFrom { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public DateTime? OvDateTo { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? Billed { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String Note { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String StatusDescription { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String TtnNumber { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String TtnSeria { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String OtherDocuments { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String PassportNumber { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String PassportSeria { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String Comment { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String RecipientStreetPrefix { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String RecipientStreet { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String RecipientStreetNumber { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String RecipientKorpus { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String RecipientKvartira { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String RecipientPhone { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String RecipientPhoneTwo { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String SenderStreetPrefix { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String SenderStreetName { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String SenderStreetNumber { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String SenderHousing { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String SenderApartmentNumber { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? SenderCityID { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? AvailableOtherDocuments { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? WharehouseId { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public DateTime? ReturnDate { get; set; }

        public void Delete(Int32 id)
        {
            this.ID = id;
            var ticketForDelete = new Tickets() { ID = id };
            ticketForDelete.GetById();

            //условия для удаления грузов, привязанных к заявке
            if (!String.IsNullOrEmpty(ticketForDelete.FullSecureID))
            {
                var goods = new Goods() { TicketFullSecureID = ticketForDelete.FullSecureID };
                var goodsList = goods.GetAllItems("ID", "DESC", "TicketFullSecureID");
                foreach (DataRow row in goodsList.Tables[0].Rows)
                {
                    var goodsToDelete = new Goods();
                    goodsToDelete.Delete(Convert.ToInt32(row["ID"]));
                }
            }

            DM.DeleteData(this);
        }

        public void Delete(Int32 id, int userId, string userIp, string pageName, string fullsecureid)
        {
            this.ID = id;
            //условия для удаления грузов, привязанных к заявке
            if (!String.IsNullOrEmpty(fullsecureid))
            {
                var goods = new Goods() { TicketFullSecureID = fullsecureid };
                var goodsList = goods.GetAllItems("ID", "DESC", "TicketFullSecureID");
                foreach (DataRow row in goodsList.Tables[0].Rows)
                {
                    var goodsToDelete = new Goods();
                    goodsToDelete.Delete(Convert.ToInt32(row["ID"]), userId, userIp, pageName);
                }
            }

            DM.DeleteData(this, userId, userIp, pageName);
        }

        public void Create()
        {
            var statistic = CheckChanges("создание");
            DM.CreateData(this);

            string query = String.Format("INSERT INTO `userslog` ( `UserID`, `Method`, `TableName`, `DateTime`, `UserIP`,`PageName`,`TicketFullSecureID`, `TicketUserID`, `FieldID`) VALUES(\"{0}\",\"Create\", \"{1}\", \"{2}\", \"{3}\", \"{4}\", \"{5}\", \"{6}\", \"{7}\"); ",
                             UserID, TableName, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), OtherMethods.GetIPAddress(), "Создание заявки", FullSecureID, UserID, "1");
            new DataManager().QueryWithReturnDataSet(query);

            AproveChanges(statistic, this.SecureID);
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

        public DataSet GetAllItemsByUserProfileID()
        {
            return DM.GetAllData(this, "ID", "ASC", "UserProfileID");
        }

        public dynamic GetBySecureId()
        {
            return DM.GetDataBy(this, "SecureID", null);
        }

        public dynamic GetByFullSecureId()
        {
            return DM.GetDataBy(this, "FullSecureID", null);
        }

        public dynamic GetById()
        {
            return DM.GetDataBy(this, "ID", null);
        }

        public void Update()
        {
            var statistic = CheckChanges("редактирование");
            DM.UpdateDate(this);

            if (statistic != null)
                AproveChanges(statistic);
        }

        public void Update(int userId, string userIp, string pageName)
        {
            var statistic = CheckChanges("редактирование");
            DM.UpdateDate(this, userId, userIp, pageName);

            if(statistic != null)
                AproveChanges(statistic);
        }

        protected Ticket_statistic CheckChanges(string statusChange)
        {
            Tickets ticket = new Tickets() { ID = ID };
            ticket.GetById();

            var statistic = new Ticket_statistic();            

            if (statusChange == "редактирование")
            {
                bool changed = false;
                statistic.Changes = "Редактирование заявки";

                if(GruzobozCost != ticket.GruzobozCost && GruzobozCost != null)
                {
                    changed = true;
                    statistic.Changes += String.Format(", стоимость за услугу изменена с {0} на {1}", ticket.GruzobozCost, GruzobozCost);
                }

                if (AgreedCost != ticket.AgreedCost && AgreedCost != null)
                {
                    changed = true;
                    statistic.Changes += String.Format(", согласованная стоимость изменена с {0} на {1}", ticket.AgreedCost, AgreedCost);
                }

                if (AssessedCost != ticket.AssessedCost && AssessedCost != null)
                {
                    changed = true;
                    statistic.Changes += String.Format(", оценочная стоимость изменена с {0} на {1}", ticket.AssessedCost, AssessedCost);
                }

                if(StatusID != ticket.StatusID && StatusID != null)
                {
                    changed = true;

                    string statusOld = string.Empty;
                    string statusNew = string.Empty;
                    TicketStatuses.TryGetValue(Convert.ToInt32(ticket.StatusID), out statusOld);
                    TicketStatuses.TryGetValue(Convert.ToInt32(StatusID), out statusNew);

                    statistic.Changes += String.Format(", статус изменен с {0} на {1}", statusOld, statusNew);
                }

                if (changed == false)
                    return null;
            }
            else
            {
                statistic.Changes = "Создание заявки";
            }

            return statistic;
        }

        protected void AproveChanges(Ticket_statistic statistic, string secureID = null)
        {
            Tickets ticket = new Tickets();
            if(!string.IsNullOrEmpty(secureID))
            {
                ticket.SecureID = secureID;
                ticket.GetBySecureId();
            }
            else
            {
                ticket.ID = ID;
                ticket.GetById();
            }

            string status = string.Empty;
            TicketStatuses.TryGetValue(Convert.ToInt32(ticket.StatusID), out status);

            statistic.Status = status;
            statistic.SecureID = ticket.SecureID;
            statistic.UserID = ticket.UserID;
            statistic.AgreedCost = ticket.AgreedCost;
            statistic.GruzobozCost = ticket.GruzobozCost;
            statistic.AssessedCost = ticket.AssessedCost;
            statistic.ChangeDate = DateTime.Now;
            statistic.Goods = BLL.Helpers.GoodsHelper.GoodsToString(ticket.FullSecureID);

            statistic.Create();
        }

        public static Dictionary<int, string> ProfileType = new Dictionary<int, string>()
        {
            {1, "Физическое лицо"},
            {2, "Юридическое лицо"},
            {3, "Интернет-магазин"},
        };

        public static Dictionary<int, string> Paging = new Dictionary<int, string>()
        {
            {1, "100"},
            {2, "200"},
            {3, "500"},
            {4, "1000"},
            {5, "2000"},
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