using Delivery.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace DeliveryNet.Data
{
    [Table("tickets")]
    public class Tickets
    {
        private decimal? _assessedCost;

        private decimal? _deliveryCost;

        private decimal? _agreedCost;

        private decimal? _receivedBLR;

        private decimal? _pril2Cost;

        private decimal? _gruzobozCost;

        public string DenomDate;

        public string DenomCoeff;
        
        public Int32 ID { get; set; }
        
        public Int32? CreditDocuments { get; set; }
        
        public String SecureID { get; set; }
        
        public String SenderProfileID { get; set; }
        
        public String FullSecureID { get; set; }
        
        public Int32? UserID { get; set; }
        
        public Int32? UserProfileID { get; set; }
        
        public Int32? CityID { get; set; }
        
        public Int32? StatusID { get; set; }
        
        public Int32? IssuanceListID { get; set; }
        
        public Int32? StatusIDOld { get; set; }
        
        public Int32? DriverID { get; set; }
        
        public String RecipientFirstName { get; set; }
        
        public String RecipientLastName { get; set; }
        
        public String RecipientThirdName { get; set; }
        
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
        
        public Int32? CourseRUR { get; set; }
        public Int32? CourseUSD { get; set; }
        public Int32? CourseEUR { get; set; }
        public Decimal? ReceivedRUR { get; set; }
        public Decimal? ReceivedUSD { get; set; }
        public Decimal? ReceivedEUR { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public Int32? BoxesNumber { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ChangeDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public DateTime? ProcessedDate { get; set; }
        public DateTime? AdmissionDate { get; set; }
        public Int32? TrackIDUser { get; set; }
        public Int32? PrintNakl { get; set; }
        public Int32? Weight { get; set; }
        public Int32? PrintNaklInMap { get; set; }
        public Int32? NotPrintInPril2 { get; set; }
        public Int32? Pril2BoxesNumber { get; set; }
        public Int32? Pril2Weight { get; set; }
        public Int32? WithoutMoney { get; set; }
        public Int32? IsExchange { get; set; }
        public Int32? CheckedOut { get; set; }
        public Int32? Phoned { get; set; }
        public DateTime? OvDateFrom { get; set; }
        public DateTime? OvDateTo { get; set; }
        public Int32? Billed { get; set; }
        public String Note { get; set; }
        public String StatusDescription { get; set; }
        public String TtnNumber { get; set; }
        public String TtnSeria { get; set; }
        public String OtherDocuments { get; set; }
        public String PassportNumber { get; set; }
        public String PassportSeria { get; set; }
        public String Comment { get; set; }
        public String RecipientStreetPrefix { get; set; }
        public String RecipientStreet { get; set; }
        public String RecipientStreetNumber { get; set; }
        public String RecipientKorpus { get; set; }
        public String RecipientKvartira { get; set; }
        public String RecipientPhone { get; set; }
        public String RecipientPhoneTwo { get; set; }
        public String SenderStreetPrefix { get; set; }
        public String SenderStreetName { get; set; }
        public String SenderStreetNumber { get; set; }
        public String SenderHousing { get; set; }
        public String SenderApartmentNumber { get; set; }
        public Int32? SenderCityID { get; set; }
        public Int32? AvailableOtherDocuments { get; set; }
        public DateTime? ReturnDate { get; set; }
        

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
