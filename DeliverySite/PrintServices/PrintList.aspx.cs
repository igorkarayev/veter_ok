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
using System.Globalization;

namespace Delivery.PrintServices
{
    public partial class PrintList : ManagerBasePage
    {
        protected String DriverID { get; set; }

        protected String DriverName { get; set; }

        protected String TicketIdList { get; set; }

        protected String AppKey { get; set; }

        protected String UserId { get; set; }

        protected String UserName { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            var userInSession = (Users)Session["userinsession"];
            if (userInSession != null && userInSession.Role != Users.Roles.User.ToString())
            {
                //UserId = 1.ToString();
                UserId = userInSession.ID.ToString();
                UserName = userInSession.Family.ToString() + " " + userInSession.Name.ToString();
            }
            else
            {
                UserId = userInSession.ID.ToString();
                UserName = userInSession.Family.ToString() + " " + userInSession.Name.ToString();
            }

            AppKey = Globals.Settings.AppServiceSecureKey;
            string fullSqlString = String.Empty;
            var controlTicketID = String.Empty;
            var idListString = TicketIdList = Request.QueryString["id"];

            if (!String.IsNullOrEmpty(idListString))
            {
                List<string> idList = idListString.Split('-').ToList();

                var sqlString = String.Empty;
                foreach (var id in idList)
                {
                    sqlString = sqlString + "T.ID = " + id + " OR ";
                    controlTicketID = id;
                }

                fullSqlString =
                    "SELECT T.* FROM tickets T " +
                    "JOIN usersprofiles U " +
                    "ON T.UserProfileID = U.ID " +
                    "WHERE (" + sqlString.Remove(sqlString.Length - 3) +
                    ")" +
                    "ORDER BY T.ID DESC";


                var dm = new DataManager();
                var dataset = dm.QueryWithReturnDataSet(fullSqlString);
                var iter = 1;
                var overBoxes = 0;
                var overWeight = 0;
                decimal overCost = 0;
                DriverID = String.Empty;

                lvAllTickets.DataSource = dataset;
                lvAllTickets.DataBind();

                var ticket = new Tickets { ID = Convert.ToInt32(controlTicketID) };
                ticket.GetById();

                dateAct.Text = OtherMethods.DateConvert(DateTime.Today.ToString());

                GetMoney(fullSqlString);
            }

            if (String.IsNullOrEmpty(idListString) || lvAllTickets.Items.Count == 0)
            {
                Page.Visible = false;
                if (String.IsNullOrEmpty(idListString))
                {
                    Response.Write(Resources.PrintResources.PrintListEmptyText);
                }
                else
                {
                    Response.Write(Resources.PrintResources.PrintListEmptyText);
                }
                Page.Visible = false;
            }

        }

        public void GetMoney(string query)
        {
            var userType = Request.QueryString["type"];

            var searchString = "FROM (" + query + ") as R";
            var dm = new DataManager();
            var eur = Convert.ToString(dm.QueryWithReturnDataSet("SELECT SUM(ReceivedEUR) " + searchString).Tables[0].Rows[0][0], CultureInfo.CurrentCulture);
            var usd = Convert.ToString(dm.QueryWithReturnDataSet("SELECT SUM(ReceivedUSD) " + searchString).Tables[0].Rows[0][0], CultureInfo.CurrentCulture);
            var rur = Convert.ToString(dm.QueryWithReturnDataSet("SELECT SUM(ReceivedRUR) " + searchString).Tables[0].Rows[0][0], CultureInfo.CurrentCulture);

            var allInBLR = Convert.ToString(dm.QueryWithReturnDataSet("SELECT SUM(ReceivedUSD * CourseUSD + ReceivedRUR * CourseRUR + ReceivedEUR * CourseEUR) " + searchString).Tables[0].Rows[0][0], CultureInfo.CurrentCulture);
            var allGruzobozCost = Convert.ToString(dm.QueryWithReturnDataSet("SELECT SUM(GruzobozCost) " + searchString).Tables[0].Rows[0][0], CultureInfo.CurrentCulture);
            var allAssessedCost = Convert.ToString(dm.QueryWithReturnDataSet("SELECT SUM(AssessedCost + DeliveryCost) " + searchString + " Where AgreedCost = 0 AND WithoutMoney = 0").Tables[0].Rows[0][0], CultureInfo.CurrentCulture);

            //если нет согласованных стоимостей - записываем ее равной 0
            var allAgreedCostString =
                Convert.ToString(dm.QueryWithReturnDataSet("SELECT SUM(AgreedCost) " + searchString +
                                         " Where (AgreedCost > 0 OR WithoutMoney = 1)").Tables[0].Rows[0][0], CultureInfo.CurrentCulture);
            var allAgreedCost = !String.IsNullOrEmpty(allAgreedCostString) ? allAgreedCostString : "0";

            double allAssessedCostValue = 0.0;
            Double.TryParse(allAssessedCost, out allAssessedCostValue);

            double allAgreedCostValue = 0.0;
            Double.TryParse(allAgreedCost, out allAgreedCostValue);


            var allAgreedAssessedCostValue = allAssessedCostValue + allAgreedCostValue;
            //к выдаче

            double allGruzobozCostValue = 0.0;
            Double.TryParse(allGruzobozCost, out allGruzobozCostValue);

            double allInBLRValue = 0.0;
            Double.TryParse(allInBLR, out allInBLRValue);

            var overToIssuance = allAgreedAssessedCostValue - allInBLRValue - ((userType != "0" && !String.IsNullOrEmpty(userType) && (userType == "3" || userType == "2")) ? 0 : allGruzobozCostValue);
            var overToIssuanceString = String.Empty;
            overToIssuanceString = "<b>" + MoneyMethods.MoneySeparator(overToIssuance.ToString()) + "</b> BLR";

            lblReceivedBLRUser.Text = overToIssuanceString;
            lblOverGruzobozCost.Text = MoneyMethods.MoneySeparator(allGruzobozCost);
        }
    }
}