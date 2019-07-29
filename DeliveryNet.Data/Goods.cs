using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace DeliveryNet.Data
{
    [Table("titles")]
    public class Goods
    {
        public string Name { get; set; }
        public int? ID { get; set; }
        public double MarginCoefficient { get; set; }
        //public string TicketFullSecureID { get; set; }
        //public string Description { get; set; }
        //public string Model { get; set; }
        
        //public double Cost { get; set; }
        //public DateTime? CreateDate { get; set; }

        //public DateTime? ChangeDate { get; set; }        
    }    
}