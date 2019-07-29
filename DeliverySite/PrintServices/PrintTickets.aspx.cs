using System;
using System.Collections.Generic;
using System.Linq;
using Delivery.DAL;
using Delivery.DAL.DataBaseObjects;
using Delivery.ManagerUI;

namespace Delivery.PrintServices
{
    public partial class PrintTickets : ManagerBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var controlTicketID = String.Empty;
            var idListString = Request.QueryString["id"];
            if (!String.IsNullOrEmpty(idListString))
            {
                List<string> idList = idListString.Split('-').ToList();
                var sqlString = String.Empty;
                foreach (var id in idList)
                {
                    sqlString = sqlString + "ID = " + id + " OR ";
                    controlTicketID = id;
                }
                var fullSqlString = "SELECT * FROM `tickets` WHERE " + sqlString.Remove(sqlString.Length - 3);
                var dm = new DataManager();
                var dataset =  dm.QueryWithReturnDataSet(fullSqlString);
                lvAllPrint.DataSource = dataset;
                lvAllPrint.DataBind();

                var ticket = new Tickets { ID = Convert.ToInt32(controlTicketID) };
                ticket.GetById();
            }

            if (String.IsNullOrEmpty(idListString) || lvAllPrint.Items.Count == 0)
            {
                Page.Visible = false;
                Response.Write(Resources.PrintResources.PrintTicketsEmptyText);
            }
        }
    }
}