using Delivery.DAL.DataBaseObjects;
using Delivery.UserUI;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static Delivery.BLL.StaticMethods.ReadXLSMethods;

namespace DeliverySite.UserUI
{
    public partial class Ticketstatistic : UserBasePage
    {
        DataTable allStatsTable { get; set; }

        protected void Page_Init(object sender, EventArgs e)
        {
            DownloadButton.Click += btnDownload_Click;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            allStatsTable = new Ticket_statistic().GetAllItems().Tables[0];
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            if (allStatsTable.Rows.Count != 0)
            {
                List<string> queries = new List<string>();

                DateTime dateFrom;
                DateTime dateTo;
                
                if (DateTime.TryParse(stbDeliveryDate1.Text, out dateFrom))
                {
                    queries.Add("ChangeDate >= '" + dateFrom + "'");
                }

                if (DateTime.TryParse(stbDeliveryDate2.Text, out dateTo))
                {
                    queries.Add("ChangeDate <= '" + dateTo + "'");
                }

                if(!string.IsNullOrEmpty(stbUID.Text))
                {
                    queries.Add("UserID IN(" + stbUID.Text + ")");
                }

                var rows = allStatsTable.Select(string.Join(" AND ", queries));
                if (rows.Count() != 0)
                {
                    MemoryStream xlsStream = new XLSWrite().GetXLSStreamStatistic(rows.CopyToDataTable());
                    xlsStream.CopyTo(Response.OutputStream);

                    Response.ContentType = "Application/OCTET-STREAM";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=статистика по заявкам.xlsx");
                    Response.End();
                }
            }
        }
    }
}