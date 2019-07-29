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
    public partial class PrintNakl : ManagerBasePage
    {
        protected String AppKey { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            AppKey = Globals.Settings.AppServiceSecureKey;
            var controlTicketID = String.Empty;
            var idListString = Request.QueryString["id"];
            var withUrMarker = Request.QueryString["with_ur"];
            var ticketsCount = 0;
            if (!String.IsNullOrEmpty(idListString))
            {
                List<string> idList = idListString.Split('-').ToList();
                var sqlString = String.Empty;
                foreach (var id in idList)
                {
                    sqlString = sqlString + "T.`ID` = " + id + " OR ";
                    controlTicketID = id;
                }
                string fullSqlString;

                if (String.IsNullOrEmpty(withUrMarker))
                {
                    cbWithUr.Checked = false;
                    fullSqlString = "SELECT T.`SecureID`, T.`ID`, T.`BoxesNumber`, T.`Weight`, T.`AgreedCost`, T.`AssessedCost`, T.`DriverID` " +
                                    "FROM `tickets` T " +
                                    "JOIN `usersprofiles` as U " +
                                    "ON T.`UserProfileID` = U.`ID` " +
                                    "WHERE (" + sqlString.Remove(sqlString.Length - 3) + ") AND `PrintNakl` = '1' AND U.`TypeID` = '1' AND (T.StatusID ='3' OR T.StatusID = '19')";
                }
                else
                {
                    cbWithUr.Checked = true;
                    fullSqlString = "SELECT T.`SecureID`, T.`ID`, T.`BoxesNumber`, T.`Weight`, T.`AgreedCost`, T.`AssessedCost`, T.`DriverID` " +
                                    "FROM `tickets` T " +
                                    "JOIN `usersprofiles` U " +
                                    "ON T.`UserProfileID` = U.`ID` " +
                                    "WHERE (" + sqlString.Remove(sqlString.Length - 3) + ") AND (T.`PrintNaklInMap` = '0' OR U.`TypeID` = '1') AND (T.StatusID ='3' OR T.StatusID = '19')" +
                                    "ORDER BY T.`ID` DESC";
                }
                
                var dm = new DataManager();
                var dataset =  dm.QueryWithReturnDataSet(fullSqlString);
                var overBoxes = 0;
                var overWeight = 0;
                decimal overCost = 0;
                var driverID = String.Empty;
                foreach (DataRow row in dataset.Tables[0].Rows)
                {
                    driverID = row["DriverID"].ToString();
                    overBoxes += Convert.ToInt32(row["BoxesNumber"]);
                    overWeight += String.IsNullOrEmpty(row["Weight"].ToString()) ? 0 : Convert.ToInt32(row["Weight"]);
                    overCost += Convert.ToInt32(row["AgreedCost"]) != 0 ? Convert.ToDecimal(row["AgreedCost"]) : Convert.ToDecimal(row["AssessedCost"]);
                    ticketsCount++;
                }

                lblCar.Text = DriversHelper.DriverIDToCarZP(driverID);
                lblDriver.Text = lblDriver2.Text = lblDriver3.Text = DriversHelper.DriverIDToNameZP(driverID);
                lblCost.Text = lblCost2.Text = lblCost3.Text = lblCost4.Text = MoneyMethods.MoneySeparator(overCost.ToString());
                lblBoxes2.Text = lblBoxes3.Text = overBoxes.ToString();
                lblWeight.Text = lblWeight2.Text = overWeight.ToString();
                
                lblCostWord.Text = MoneyHelper.ToRussianString(overCost);

                lblWeightWord.Text = NumberToRussianString.NumberToString(
                    Convert.ToInt64(overWeight), NumberToRussianString.WordGender.Masculine);

                lblBoxesWord.Text = NumberToRussianString.NumberToString(
                    Convert.ToInt64(overBoxes), NumberToRussianString.WordGender.Masculine);

                Inpnaklnumber.Text =
                    dm.QueryWithReturnDataSet("SELECT `NaklNumber` FROM `printdata`").Tables[0].Rows[0][0]
                        .ToString();
                Inpseria.Text =
                    dm.QueryWithReturnDataSet("SELECT `NaklSeria` FROM `printdata`").Tables[0].Rows[0][0]
                        .ToString();
                Inpdate.Text =
                    dm.QueryWithReturnDataSet("SELECT `NaklDate` FROM `printdata`").Tables[0].Rows[0][0]
                        .ToString();
                Inpputevoi.Text =
                    dm.QueryWithReturnDataSet("SELECT `Putevoi` FROM `printdata`").Tables[0].Rows[0][0]
                        .ToString();

                var ticket = new Tickets { ID = Convert.ToInt32(controlTicketID) };
                ticket.GetById();
            }

            #region Сообщение, если накладная пустая
            if (String.IsNullOrEmpty(idListString) || ticketsCount == 0)
            {
                Page.Visible = false;
                if (String.IsNullOrEmpty(idListString))
                {
                    Response.Write(Resources.PrintResources.PrintNaklEmptyText);
                }
                else
                {
                    Response.Write(Resources.PrintResources.PrintNaklEmptyText +
                    String.Format("<br/><center><a href=\"{0}&with_ur=1\">печать с юр. лицами</a><center>", Request.RawUrl));
                }
            }
            #endregion
        }

        protected void cbWithUr_CheckedChanged(object sender, EventArgs e)
        {
            if (!cbWithUr.Checked)
            {
                Response.Redirect(Request.RawUrl + "&with_ur=1");
            }
            else
            {
                Response.Redirect(Request.RawUrl.Remove(Request.RawUrl.Length - 10, 10));
            }

        }
    }
}