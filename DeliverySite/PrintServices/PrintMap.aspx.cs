using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Delivery.BLL;
using Delivery.BLL.Helpers;
using Delivery.BLL.StaticMethods;
using Delivery.DAL;
using Delivery.DAL.DataBaseObjects;
using Delivery.ManagerUI;

namespace Delivery.PrintServices
{
    public partial class PrintMap : ManagerBasePage
    {
        protected String DriverName { get; set; }

        protected String AppKey { get; set; }

        protected Int32 Iterator { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            AppKey = Globals.Settings.AppServiceSecureKey;
            var controlTicketID = String.Empty;
            var idListString = Request.QueryString["id"];
            if (!String.IsNullOrEmpty(idListString))
            {
                List<string> idList = idListString.Split('-').ToList();
                var sqlString = String.Empty;
                foreach (var id in idList)
                {
                    sqlString = sqlString + "T.ID = " + id + " OR ";
                    controlTicketID = id;
                }

                bool withoutDriver = false;

                var fullSqlString = "SELECT * FROM `tickets` as T JOIN `city` as C on T.CityID = C.ID WHERE " + sqlString.Remove(sqlString.Length - 3) + "ORDER BY C.Name ASC";
                var dm = new DataManager();
                var dataset =  dm.QueryWithReturnDataSet(fullSqlString);
                Iterator = 1;
                dataset.Tables[0].Columns.Add("PNumber", typeof(String));
                foreach (DataRow row in dataset.Tables[0].Rows)
                {
                    row["PNumber"] = Iterator++;
                    var driverID = row["DriverID"].ToString();
                    if ((driverID == "0" || driverID == "-1") && withoutDriver == false)
                    {
                        withoutDriver = true;
                    }
                }
                lvAllPrint.DataSource = dataset;
                lvAllPrint.DataBind();

                var ticket = new Tickets { ID = Convert.ToInt32(controlTicketID) };
                ticket.GetById();

                if (withoutDriver==false)
                {
                    DriverName = lblDriver.Text = DriversHelper.DriverIDToFioToPrint(ticket.DriverID.ToString());
                }
                else
                {
                    DriverName = lblDriver.Text = String.Empty;
                }
                
                lblTrack.Text= lblTrack2.Text = CityHelper.CityToTrack(Convert.ToInt32(ticket.CityID), ticket.ID.ToString());
                lblOperatorName.Text = CityHelper.CityToTrackOperatorName(Convert.ToInt32(ticket.CityID));
                lblOperatorPhone.Text = CityHelper.CityToTrackOperatorPhone(Convert.ToInt32(ticket.CityID));
                Inpdate.Text =
                    dm.QueryWithReturnDataSet("SELECT `MapDate` FROM `printdata`").Tables[0].Rows[0][0]
                        .ToString();
            }

            if (String.IsNullOrEmpty(idListString) || lvAllPrint.Items.Count == 0)
            {
                Page.Visible = false;
                Response.Write(Resources.PrintResources.PrintMapEmptyText);
            }
        }
    }
}