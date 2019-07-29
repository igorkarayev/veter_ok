using System;

namespace DeliveryNet.Data
{
    public class Page
    {
        public int ID { get; set; }

        public string PageName { get; set; }

        public string PageTitle { get; set; }

        public string Content { get; set; }

        public DateTime? ChangeDate { get; set; }
    }
}