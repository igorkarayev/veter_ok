using System;

namespace DeliveryNet.Data
{
    public class District
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public int? DeliveryTerms { get; set; }

        public int? Monday { get; set; }

        public int? Tuesday { get; set; }

        public int? Wednesday { get; set; }

        public int? Thursday { get; set; }

        public int? Friday { get; set; }

        public int? Saturday { get; set; }

        public int? Sunday { get; set; }

        public DateTime? ChangeDate { get; set; }

        public int? TrackID { get; set; }
    }
}