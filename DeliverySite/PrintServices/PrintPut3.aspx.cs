using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Delivery.DAL;
using Delivery.ManagerUI;

namespace Delivery.PrintServices
{
    public partial class PrintPut3 : ManagerBasePage
    {
        protected String CityStringToHTML { get; set; }

        protected Int32 RowSpanNumb { get; set; }

        protected String DriverID { get; set; }

        protected String DriverName { get; set; }

        protected String AppKey { get; set; }

        protected List<string> CityList { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            AppKey = Globals.Settings.AppServiceSecureKey;
            var idListString = Request.QueryString["id"];
            if (!String.IsNullOrEmpty(idListString))
            {
                List<string> idList = idListString.Split('-').ToList();
                var sqlString = String.Empty;
                DriverID = String.Empty;
                foreach (var id in idList)
                {
                    sqlString = sqlString + "T.`ID` = " + id + " OR ";
                }
                var fullSqlString = "SELECT DISTINCT `Name` FROM `city` C JOIN `tickets` T ON C.`ID` = T.`CityID` WHERE (" + sqlString.Remove(sqlString.Length - 3) + ") AND T.`PrintNakl` = 1 AND T.`NotPrintInPril2` = 0 ";
                var fullSqlString2 = "SELECT DISTINCT `CityID`, `DriverID` FROM `tickets` T WHERE (" + sqlString.Remove(sqlString.Length - 3) + ") AND T.`PrintNakl` = 1 AND T.`NotPrintInPril2` = 0";
                var dm = new DataManager();
                var dataset =  dm.QueryWithReturnDataSet(fullSqlString);
                var dataset2 = dm.QueryWithReturnDataSet(fullSqlString2);

                if (dataset.Tables[0].Rows.Count == 0 && dataset2.Tables[0].Rows.Count == 0)
                {
                    Page.Visible = false;
                    Response.Write(Resources.PrintResources.PrintPutEmptyText);
                    return;
                }

                CityList = new List<string>();
                foreach (DataRow row in dataset.Tables[0].Rows)
                {
                    var cityName = row["Name"].ToString();
                    if (!String.IsNullOrEmpty(cityName) && cityName != "Не назначен")
                    {
                        CityList.Add(cityName);
                    }
                }

                foreach (DataRow row in dataset2.Tables[0].Rows)
                {
                    if (String.IsNullOrEmpty(DriverID))
                    {
                        DriverID = row["DriverID"].ToString();
                    }
                }

                if (String.IsNullOrEmpty(DriverID) || DriverID == "0")
                {
                    Page.Visible = false;
                    Response.Write(Resources.PrintResources.PrintPutEmptyText);
                    return;
                }

                CityList.Sort();

                foreach (var city in CityList)
                {
                    CityStringToHTML += "<tr><td></td><td></td><td>Минск</td><td style=\"text-align: left; padding-left: 5px;\">" + city + "</td><td></td><td>Доставки</td><td>-</td><td>-</td></tr>";
                    RowSpanNumb++;
                }
                if (CityStringToHTML.Length > 5)
                {
                    CityStringToHTML = CityStringToHTML.Remove(CityStringToHTML.Length - 5);
                }

                var driverInfo =
                    dm.QueryWithReturnDataSet(
                        String.Format(
                            "SELECT C.`Model`, C.`Number`, D.`FirstName`, D.`DriverPassport`, D.`LastName`, D.`ThirdName` FROM `drivers` D JOIN `cars` C ON D.`CarID` = C.`ID` WHERE D.`ID` = {0}",
                            DriverID)).Tables[0];
                lblCarModel.Text = driverInfo.Rows[0][0].ToString();
                lblCarPassport.Text = driverInfo.Rows[0][1].ToString();
                lblDriverFIO.Text = DriverName = driverInfo.Rows[0][2].ToString() + " " + driverInfo.Rows[0][4].ToString().Remove(1, driverInfo.Rows[0][4].ToString().Length - 1) + "." + driverInfo.Rows[0][5].ToString().Remove(1, driverInfo.Rows[0][5].ToString().Length - 1) + ".";
                lblDriverPassport.Text = driverInfo.Rows[0][3].ToString();

                Inpdate.Text =
                    dm.QueryWithReturnDataSet("SELECT `NaklDate` FROM `printdata`").Tables[0].Rows[0][0]
                        .ToString();
                Inpputevoi.Text =
                    dm.QueryWithReturnDataSet("SELECT `Putevoi` FROM `printdata`").Tables[0].Rows[0][0]
                        .ToString();
            }
            else
            {
                Page.Visible = false;
                Response.Write(Resources.PrintResources.PrintPutEmptyText);
            }
        }
    }
}