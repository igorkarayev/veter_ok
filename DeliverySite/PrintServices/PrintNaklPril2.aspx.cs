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
using System.Web.UI.WebControls;

namespace Delivery.PrintServices
{
    public partial class PrintNaklPril2 : ManagerBasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            btnCSVStart.Click += btnCSVStart_Click;
            btnCSVAdd.Click += btnCSVAdd_Click;
            btnCSVEnd.Click += btnCSVEnd_Click;
            
        }

        protected String DriverID { get; set; }

        protected String DriverName { get; set; }

        protected String AppKey { get; set; }

        protected String IdList { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            AppKey = Globals.Settings.AppServiceSecureKey;
            var controlTicketID = String.Empty;
            var dm = new DataManager();
            var idListString = IdList = Request.QueryString["id"];
            var withUrMarker = Request.QueryString["with_ur"];
            string whereUrSql;
            if (!String.IsNullOrEmpty(withUrMarker))
            {
                cbWithUr.Checked = true;
                whereUrSql = " ";
            }
            else
            {
                whereUrSql = " AND U.`TypeID` = '1' ";
                cbWithUr.Checked = false;
            }

            if (!String.IsNullOrEmpty(idListString))
            {
                List<string> idList = idListString.Split('-').ToList();
                var sqlString = String.Empty;
                foreach (var id in idList)
                {
                    sqlString = sqlString + "T.ID = " + id + " OR ";
                    controlTicketID = id;
                }
                var fullSqlString = "SELECT * FROM `tickets` as T " +
                                    "JOIN `city` as C " +
                                    "ON T.CityID = C.ID " +
                                    "JOIN `usersprofiles` as U " +
                                    "ON T.`UserProfileID` = U.`ID` " +
                                    "WHERE ((" + sqlString.Remove(sqlString.Length - 3) + ") AND T.PrintNakl = '1' AND T.NotPrintInPril2 ='0'" + whereUrSql + ") "  //AND (T.StatusID ='3' OR T.StatusID = '19')
                                    ;
               
                var dataset =  dm.QueryWithReturnDataSet(fullSqlString);
                var iter = 1;
                var overBoxes = 0;
                var overWeight = 0;
                decimal overCost = 0;
                DriverID = String.Empty;
                dataset.Tables[0].Columns.Add("PorID");
                dataset.Tables[0].Columns.Add("Pril2CostOrCost");

                foreach (DataRow row in dataset.Tables[0].Rows)
                {
                    row["PorID"] = iter.ToString();
                    iter ++;
                    DriverID = row["DriverID"].ToString();
                    overBoxes += Convert.ToInt32(row["BoxesNumber"]);
                    overWeight += String.IsNullOrEmpty(row["Weight"].ToString()) ? 0 : Convert.ToInt32(row["Weight"]);

                    var zero = 0.00;
                    if (Convert.ToDouble(row["Pril2Cost"]) != zero)
                    {
                        overCost += Convert.ToDecimal(row["Pril2Cost"]);
                        row["Pril2CostOrCost"] = row["Pril2Cost"];
                    }
                    else
                    {
                        overCost += Convert.ToInt32(row["AgreedCost"]) != 0 ? Convert.ToDecimal(row["AgreedCost"]) : Convert.ToDecimal(row["AssessedCost"]);
                        row["Pril2CostOrCost"] = Convert.ToInt32(row["AgreedCost"]) != 0 ? row["AgreedCost"] : row["AssessedCost"];
                    }
                }
                
                lvAllPrint.DataSource = dataset;
                lvAllPrint.DataBind();

                try
                {
                    DriverName = dm.QueryWithReturnDataSet(String.Format("SELECT `FIO` FROM `drivers` WHERE `id` = {0}", DriverID)).Tables[0].Rows[0][0].ToString();
                }
                catch (Exception)
                {
                    DriverName = "Не назначен";
                }

                lblDriver.Text = lblDriver2.Text = DriversHelper.DriverIDToNameZP(DriverID);
                lblOverNumber.Text = dataset.Tables[0].Rows.Count.ToString();
                lblOverCost.Text = lblOverCost2.Text = lblOverCost3.Text = MoneyMethods.MoneySeparator(overCost.ToString());
                lblCostWord.Text = MoneyHelper.ToRussianString(overCost);

                Inpnaklnumber.Text =
                    dm.QueryWithReturnDataSet("SELECT `NaklNumber` FROM `printdata`").Tables[0].Rows[0][0]
                        .ToString();
                Inpseria.Text =
                    dm.QueryWithReturnDataSet("SELECT `NaklSeria` FROM `printdata`").Tables[0].Rows[0][0]
                        .ToString();
                Inpdate.Text =
                    dm.QueryWithReturnDataSet("SELECT `NaklDate` FROM `printdata`").Tables[0].Rows[0][0]
                        .ToString();

                overGruzobozCost.Text =
                    MoneyMethods.MoneySeparator(
                        MoneyMethods.GruzobozCostLoweringPercentage(
                            dm.QueryWithReturnDataSet(
                            "SELECT SUM(`GruzobozCost`) " +
                            "FROM `tickets` T " + 
                            "JOIN `usersprofiles` as U " +
                            "ON T.`UserProfileID` = U.`ID` " +
                            "WHERE ((" + sqlString.Remove(sqlString.Length - 3) + ") AND T.PrintNakl = '1' AND T.NotPrintInPril2 ='0'" + whereUrSql + ")" //AND (T.StatusID ='3' OR T.StatusID = '19')
                            ).Tables[0].Rows[0][0].ToString()
                        )
                    );

                var ticket = new Tickets { ID = Convert.ToInt32(controlTicketID) };
                ticket.GetById();

                var notVisibleCount =
                dm.QueryWithReturnDataSet(
                    "SELECT COUNT(*) " +
                    "FROM `tickets` as T " +
                    "JOIN `usersprofiles` as U " +
                    "ON T.`UserProfileID` = U.`ID` " +
                    "WHERE ((" + sqlString.Remove(sqlString.Length - 3) + ") AND T.PrintNakl = '1' AND T.NotPrintInPril2 ='1'" + whereUrSql + ")").Tables[0].Rows[0][0].ToString(); //AND (T.StatusID ='3' OR T.StatusID = '19')
                btnReload.Enabled = notVisibleCount == "0";

                //рассчет недостающих коробок и веса СТАРТ

                var notVisibleTicketsTable = dm.QueryWithReturnDataSet(
                    "SELECT T.Weight, T.AgreedCost, T.AssessedCost, T.BoxesNumber " +
                    "FROM `tickets` as T " +
                    "JOIN `usersprofiles` as U " +
                    "ON T.`UserProfileID` = U.`ID` " +
                    "WHERE ((" + sqlString.Remove(sqlString.Length - 3) + ") AND T.PrintNakl = '1' AND NotPrintInPril2 ='1'" + whereUrSql + ")").Tables[0]; //AND (T.StatusID ='3' OR T.StatusID = '19')

                foreach (DataRow row in notVisibleTicketsTable.Rows)
                {
                    overBoxes += Convert.ToInt32(row["BoxesNumber"]);
                    overWeight += String.IsNullOrEmpty(row["Weight"].ToString()) ? 0 : Convert.ToInt32(row["Weight"]);
                }

                lblOverBoxes.Text = overBoxes.ToString();
                lblBoxesWord.Text = NumberToRussianString.NumberToString(
                    Convert.ToInt64(overBoxes), NumberToRussianString.WordGender.Masculine);
                lblOverWeight.Text = overWeight.ToString();
                lblWeightWord.Text = NumberToRussianString.NumberToString(
                    Convert.ToInt64(overWeight), NumberToRussianString.WordGender.Masculine);
                //рассчет недостающих коробок и веса КОНЕЦ

                if (OneCMethods.IfFileExist())
                {
                    btnCSVStart.Enabled = false;
                }
                else
                {
                    btnCSVAdd.Enabled = false;
                    btnCSVEnd.Enabled = false;
                }
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

        protected void Td6CostTextBox_TextChanged(object sender, EventArgs e)
        {
            TextBox TextBox = (TextBox)sender;
            TableCell Td7 = TextBox.Parent.Parent.FindControl("Td7") as TableCell;
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

        protected void btnZP2_Click(object sender, EventArgs e)
        {
            int i = 0;
            foreach(ListViewItem row in lvAllPrint.Items)
            {
                i++;
                var c = row.FindControl("Td6");
                
                TableCell c3 = (TableCell)row.FindControl("Td6");
                string c4 = c3.Text;
                
            }
        }

        protected void btnReload_Click(object sender, EventArgs e)
        {
            var withUrMarker = Request.QueryString["with_ur"];
            var whereUrSql = String.Empty;
            if (!String.IsNullOrEmpty(withUrMarker))
            {
                cbWithUr.Checked = true;
                whereUrSql = " ";
            }
            else
            {
                whereUrSql = " AND U.`TypeID` = '1' ";
                cbWithUr.Checked = false;
            }
            var idForMySql = Request.QueryString["id"].Split('-').ToList().Aggregate(String.Empty, (current, id) => current + "T.ID = " + id + " OR ");
            var dm = new DataManager();

            //рассчет недостающей стоимости СТАРТ
            var notVisibleTicketsTable = dm.QueryWithReturnDataSet(
                "SELECT T.Weight, T.AgreedCost, T.AssessedCost, T.BoxesNumber " +
                "FROM `tickets` as T " +
                "JOIN `usersprofiles` as U " +
                "ON T.`UserProfileID` = U.`ID` " +
                "WHERE ((" + idForMySql.Remove(idForMySql.Length - 3) + ") AND T.PrintNakl = '1' AND NotPrintInPril2 ='1'" + whereUrSql).Tables[0]; // AND (T.StatusID ='3' OR T.StatusID = '19')

            var excessCost = notVisibleTicketsTable.Rows.Cast<DataRow>().Sum(row => Convert.ToInt32(row["AgreedCost"]) != 0 ? Convert.ToDecimal(row["AgreedCost"]) : Convert.ToDecimal(row["AssessedCost"]));
            //рассчет недостающей стоимости КОНЕЦ


            //пересчет стоимостей СТАРТ (не учитывая заявки у которых ноль по сумме)
            var visibleTicketsTable = dm.QueryWithReturnDataSet(
                "SELECT T.ID " +
                "FROM `tickets` as T " +
                "JOIN `usersprofiles` as U " +
                "ON T.`UserProfileID` = U.`ID` " +
                "WHERE ((" + idForMySql.Remove(idForMySql.Length - 3) + ") AND T.PrintNakl = '1' AND NotPrintInPril2 ='0' AND (T.AgreedCost + T.AssessedCost) <>'0'" + whereUrSql + ")").Tables[0]; //AND (T.StatusID ='3' OR T.StatusID = '19')
            CalculateNewCost(idForMySql, excessCost, visibleTicketsTable);
            //пересчет стоимостей КОНЕЦ

            Page.Response.Redirect(Request.RawUrl);
        }



        protected void btnReset_Click(object sender, EventArgs e)
        {
            var idListString = Request.QueryString["id"];
            var idList = idListString.Split('-').ToList();
            foreach (var ticket in idList.Select(id => new Tickets
            {
                ID = Convert.ToInt32(id),
                NotPrintInPril2 = 0,
                Pril2BoxesNumber = 0,
                Pril2Cost = 0,
                Pril2Weight = 0
            }))
            {
                ticket.Update();
            }
            Page.Response.Redirect(Request.RawUrl);
        }



        public void btnCSVStart_Click(Object sender, EventArgs e)
        {
            OneCMethods.StartSession();
            Page.Response.Redirect(Request.RawUrl);
        }



        public void btnCSVAdd_Click(Object sender, EventArgs e)
        {
            OneCMethods.GenerateCsv(Request.QueryString["id"], Inpnaklnumber.Text, Inpseria.Text, Inpdate.Text);
        }



        public void btnCSVEnd_Click(Object sender, EventArgs e)
        {
            OneCMethods.EndSession();
            Page.Response.Redirect(Request.RawUrl);
        }



        public void CalculateNewCost(string idForMySql, decimal excessCost, DataTable visibleTicketsTable)
        {
            var withUrMarker = Request.QueryString["with_ur"];
            var whereUrSql = String.Empty;
            if (!String.IsNullOrEmpty(withUrMarker))
            {
                whereUrSql = "" + whereUrSql + " ";
            }
            var dm = new DataManager();
            var searchString = "FROM `tickets` T JOIN `usersprofiles` as U ON T.`UserProfileID` = U.`ID` WHERE (" + idForMySql.Remove(idForMySql.Length - 3) + ")";
            //общее количество коробок на приложение
            var allAssessedCostString = dm.QueryWithReturnDataSet("SELECT SUM(T.AssessedCost) " + searchString + " AND T.AgreedCost = 0 AND T.PrintNakl = '1'" + whereUrSql + "").Tables[0].Rows[0][0].ToString(); //AND (T.StatusID ='3' OR T.StatusID = '19')
            var allAgreedCostString = dm.QueryWithReturnDataSet("SELECT SUM(T.AgreedCost) " + searchString + " AND T.AgreedCost > 0 AND T.PrintNakl = '1'" + whereUrSql + "").Tables[0].Rows[0][0].ToString(); //AND (T.StatusID ='3' OR T.StatusID = '19')
            if (String.IsNullOrEmpty(allAssessedCostString))
            {
                allAssessedCostString = "0";
            }

            if (String.IsNullOrEmpty(allAgreedCostString))
            {
                allAgreedCostString = "0";
            }
            var allCost = Convert.ToDecimal(allAssessedCostString) + Convert.ToDecimal(allAgreedCostString);
            var finish = false;

            //Если старое количество коробок всех заявок в приложении совпадает с новым количеством коробок всех видемых заявок, то не пересчитываем количество коробок СТАРТ
            var allVisibleReCost = Convert.ToDecimal(dm.QueryWithReturnDataSet("SELECT SUM(T.Pril2Cost) " + searchString + " AND T.PrintNakl = '1' AND NotPrintInPril2 ='0')" + whereUrSql + "").Tables[0].Rows[0][0]); //AND (T.StatusID ='3' OR T.StatusID = '19')
            if (allCost == allVisibleReCost)
            {
                finish = true;
            }
            //Если старое количество коробок всех заявок в приложении совпадает с новым количеством коробок всех видимых заявок, то не пересчитываем количество коробок КОНЕЦ

            while (!finish)
            {
                var excessCostNew = excessCost;
                var fullAddedCostForIteration = 0;
                var lastTicketId = 0;
                foreach (DataRow ticketRow in visibleTicketsTable.Rows)
                {
                    lastTicketId = Convert.ToInt32(ticketRow["ID"]);
                    var ticket = new Tickets { ID = lastTicketId };
                    ticket.GetById();

                    var ticketCost = ticket.AgreedCost == 0? ticket.AssessedCost : ticket.AgreedCost;
                    var ticketCostPercentFromAllCost = Math.Round(Convert.ToDecimal(ticketCost * 100 / allCost)); //процентная зависимость начального веса заявки от суммы начальных весов всех видимых заявок
                    var addedCost = Convert.ToInt32(Math.Round(excessCost * ticketCostPercentFromAllCost / 100)); //добавочный вес для текущей заявки

                    //округление
                    addedCost = Convert.ToInt32(MoneyMethods.MoneyRounder100(Convert.ToDecimal(addedCost)));

                    excessCostNew = excessCostNew - addedCost;

                    if (ticket.Pril2Cost == 0)
                    {
                        ticket.Pril2Cost = ticketCost + addedCost;
                    }
                    else
                    {
                        ticket.Pril2Cost = ticket.Pril2Cost + addedCost;
                    }

                    fullAddedCostForIteration += addedCost;

                    ticket.Update();
                }

                //если пробежав все заявки ни к одной не были добавлены коробки - инизиализируем финиш (записываем оставшиеся коробки к последней заявке и выходим из цикла)
                if (fullAddedCostForIteration <= 0)
                {
                    var ticket = new Tickets { ID = lastTicketId };
                    ticket.GetById();
                    var ticketCost = ticket.AgreedCost == 0 ? ticket.AssessedCost : ticket.AgreedCost;
                    
                    if (ticket.Pril2Cost == 0)
                    {
                        ticket.Pril2Cost = ticketCost + excessCostNew;
                    }
                    else
                    {
                        ticket.Pril2Cost = ticket.Pril2Cost + excessCostNew;
                    }
                    ticket.Update();
                    finish = true;
                }
                excessCost = excessCostNew; //записываем сколько веса осталось перераспределить
            }

            //рассчет новых параметров КОНЕЦ
        }



    }
}