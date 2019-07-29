using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryNet.Data
{
    [Table("backend")]
    public class Backend
    {
        public int ID { get; set; }

        public string Value { get; set; }

        public string Tag { get; set; }

        public string Description { get; set; }

        public DateTime? ChangeDate { get; set; }
    }
}