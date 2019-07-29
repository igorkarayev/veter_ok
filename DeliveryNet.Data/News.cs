using DeliveryNET.Core;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryNet.Data
{
    public class News
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string TitleUrl { get; set; }

        public string Body { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? ChangeDate { get; set; }

        public int NewsTypeID { get; set; }

        public int ForViewing { get; set; }

        [NotMapped]
        public string ShortBody
            =>
                Body.HtmlToPureText().Length > 100
                    ? Body.HtmlToPureText().Substring(0, 100)
                    : Body.HtmlToPureText();
    }
}