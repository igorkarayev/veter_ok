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
    public partial class PrintNaklPril : ManagerBasePage
    {
        protected String DriverID { get; set; }

        protected String DriverName { get; set; }

        protected String TicketIdList { get; set; }

        protected String AppKey { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            AppKey = Globals.Settings.AppServiceSecureKey;
            var controlTicketID = String.Empty;
            var idListString = TicketIdList = Request.QueryString["id"];
            var withUrMarker = Request.QueryString["with_ur"];
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
                    fullSqlString =
                        "SELECT T.`SecureID`, T.`ID`, T.`BoxesNumber`, T.`Weight`, T.`AgreedCost`, T.`AssessedCost`, T.`DriverID` " +
                        "FROM `tickets` T " +
                        "JOIN `usersprofiles` U " +
                        "ON T.`UserProfileID` = U.`ID` " +
                        "WHERE (" + sqlString.Remove(sqlString.Length - 3) +
                        ") AND U.`TypeID` = '1' AND (T.StatusID ='3' OR T.StatusID = '19')" +
                        "ORDER BY T.`ID` DESC";
                }
                else
                {
                    cbWithUr.Checked = true;
                    fullSqlString =
                        "SELECT T.`SecureID`, T.`ID`, T.`BoxesNumber`, T.`Weight`, T.`AgreedCost`, T.`AssessedCost`, T.`DriverID` " +
                        "FROM `tickets` T " +
                        "JOIN `usersprofiles` U " +
                        "ON T.`UserProfileID` = U.`ID` " +
                        "WHERE (" + sqlString.Remove(sqlString.Length - 3) + ") AND (T.`PrintNaklInMap` = '0' OR U.`TypeID` = '1') AND (T.StatusID ='3' OR T.StatusID = '19')" +
                        "ORDER BY T.`ID` DESC";
                }

                var dm = new DataManager();
                var dataset =  dm.QueryWithReturnDataSet(fullSqlString);
                var iter = 1;
                var overBoxes = 0;
                var overWeight = 0;
                decimal overCost = 0;
                DriverID = String.Empty;
                dataset.Tables[0].Columns.Add("PorID");

                foreach (DataRow row in dataset.Tables[0].Rows)
                {
                    row["PorID"] = iter.ToString();
                    iter ++;
                    DriverID = row["DriverID"].ToString();
                    overBoxes += Convert.ToInt32(row["BoxesNumber"]);
                    overWeight += String.IsNullOrEmpty(row["Weight"].ToString()) ? 0 : Convert.ToInt32(row["Weight"]);
                    overCost += Convert.ToInt32(row["AgreedCost"]) != 0 ? Convert.ToDecimal(row["AgreedCost"]) : Convert.ToDecimal(row["AssessedCost"]);

                }
                lvAllPrint.DataSource = dataset;
                lvAllPrint.DataBind();

                try
                {
                    DriverName = dm.QueryWithReturnDataSet(String.Format("SELECT CONCAT(`FirstName`, ' ',  `LastName`, ' ', `ThirdName`) FROM `drivers` WHERE `id` = {0}", DriverID)).Tables[0].Rows[0][0].ToString();
                }
                catch (Exception)
                {
                    DriverName = "Не назначен";
                }

                lblDriver.Text = lblDriver2.Text = DriversHelper.DriverIDToNameZP(DriverID);
                lblOverNumber.Text = dataset.Tables[0].Rows.Count.ToString();
                lblOverCost.Text = lblOverCost2.Text = MoneyMethods.MoneySeparator(overCost.ToString());
                
                lblCostWord.Text = MoneyHelper.ToRussianString(overCost);

                lblOverBoxes.Text = overBoxes.ToString();
                lblBoxesWord.Text = NumberToRussianString.NumberToString(
                    Convert.ToInt64(overBoxes), NumberToRussianString.WordGender.Masculine);
                lblOverWeight.Text = overWeight.ToString();
                lblWeightWord.Text = NumberToRussianString.NumberToString(
                    Convert.ToInt64(overWeight), NumberToRussianString.WordGender.Masculine);

                Inpnaklnumber.Text =
                    dm.QueryWithReturnDataSet("SELECT `NaklNumber` FROM `printdata`").Tables[0].Rows[0][0]
                        .ToString();
                Inpseria.Text =
                    dm.QueryWithReturnDataSet("SELECT `NaklSeria` FROM `printdata`").Tables[0].Rows[0][0]
                        .ToString();
                Inpdate.Text =
                    dm.QueryWithReturnDataSet("SELECT `NaklDate` FROM `printdata`").Tables[0].Rows[0][0]
                        .ToString();

                var ticket = new Tickets { ID = Convert.ToInt32(controlTicketID) };
                ticket.GetById();
            }

            if (String.IsNullOrEmpty(idListString) || lvAllPrint.Items.Count == 0)
            {
                Page.Visible = false;
                if (String.IsNullOrEmpty(idListString))
                {
                    Response.Write(Resources.PrintResources.PrintNaklPrilEmptyText);
                }
                else
                {
                    Response.Write(Resources.PrintResources.PrintNaklPrilEmptyText +
                    String.Format("<br/><center><a href=\"{0}&with_ur=1\">печать с юр. лицами</a><center>", Request.RawUrl));
                }
                Page.Visible = false;
            }
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