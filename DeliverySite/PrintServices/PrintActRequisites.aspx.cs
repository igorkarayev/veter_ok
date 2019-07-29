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
    public partial class PrintActRequisites : ManagerBasePage
    {
        protected String UserName { get; set; }

        protected String AppKey { get; set; }

        protected Int32 Iterator { get; set; }

        protected Int32 UserID { get; set; }

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

                var dm = new DataManager();
                var fullSqlString = "SELECT * FROM `tickets` as T WHERE " + sqlString.Remove(sqlString.Length - 3) + "ORDER BY T.CreateDate ASC";
                var ds = dm.QueryWithReturnDataSet(fullSqlString);
                Iterator = 1;
                ds.Tables[0].Columns.Add("PNumber", typeof(String));
                ds.Tables[0].Columns.Add("Ttn", typeof(String));
                ds.Tables[0].Columns.Add("Sender", typeof(String));
                ds.Tables[0].Columns.Add("Date", typeof(String));
                ds.Tables[0].Columns.Add("SendDate", typeof(String));
                ds.Tables[0].Columns.Add("GoodsModel", typeof(String));

                int ticketUserID;
                DataRow rowFirst = ds.Tables[0].Rows[0];
                Int32.TryParse(rowFirst["UserProfileID"].ToString(), out ticketUserID);
                UsersProfiles customer = new UsersProfiles { ID = ticketUserID };
                customer.GetById();                

                #region Сообщение, если выбран не один UID
                int ticketUserCheckID;
                Int32.TryParse(rowFirst["UserID"].ToString(), out ticketUserCheckID);

                UserID = ticketUserCheckID;
                UserName = string.Concat(new string[] { customer.FirstName, " " , customer.LastName });

                /*if (String.IsNullOrEmpty(idListString) || ticketsCount == 0)
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
                }*/
                int ticketRowUserId;
                int errorIndex = 0;
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    Int32.TryParse(row["UserID"].ToString(), out ticketRowUserId);
                    if (ticketUserCheckID != ticketRowUserId)
                    {
                        errorIndex = 1;
                        Page.Visible = false;
                    }
                }

                if (customer.TypeID != 2 && customer.TypeID != 3)
                {
                    if (errorIndex == 1)
                        errorIndex = 3;
                    else
                        errorIndex = 2;
                    Page.Visible = false;
                }

                switch (errorIndex)
                {
                    case 1:
                        Response.Write(Resources.PrintResources.PrintActRequisitesEmptyText +
                            String.Format("<br/><center><a href=\"{0}&with_ur=1\">Введите заявки с одним UID</a><center>", Request.RawUrl));
                        break;
                    case 2:
                        Response.Write(Resources.PrintResources.PrintActRequisitesEmptyText +
                        String.Format("<br/><center><a href=\"{0}&with_ur=1\">Введите заявки с юр. лицами и интернет магазинами </a><center>", Request.RawUrl));
                        break;
                    case 3:
                        Response.Write(Resources.PrintResources.PrintActRequisitesEmptyText +
                        String.Format("<br/><center><a href=\"{0}&with_ur=1\">Введите заявки с одним UID юр. лицами и интернет магазинами </a><center>", Request.RawUrl));
                        break;
                }
                #endregion

                double costSum = 0;
                double cost = 0;

                nameExecuter.Text = BackendHelper.TagToValue("official_name");
                infoExecuter.Text = BackendHelper.TagToValue("ExecutorInfo");
                UNPExecuter.Text = BackendHelper.TagToValue("ttn_sender_unp");

                dateAct.Text = InputDateAct.Text = OtherMethods.DateConvert(DateTime.Today.ToString());



                numberContract.Text = customer.AgreementNumber;
                dateContract.Text = OtherMethods.DateConvert(customer.CreateDate.ToString());

                customerCompanyName.Text = customer.CompanyName;
                customerCompanyAddress.Text = customer.CompanyAddress;
                customerRasShet.Text = customer.RasShet;
                customerBankName.Text = customer.BankName;
                customerBankAddress.Text = customer.BankAddress;
                customerContactPhoneNumbers.Text = customer.ContactPhoneNumbers;
                customerUNP.Text = customer.UNP;

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    row["PNumber"] = Iterator++;

                    row["Ttn"] = string.Concat(new string[] { row["TtnSeria"].ToString().ToUpper(), row["TtnNumber"].ToString() });

                    string recipientKorpus = null;

                    if (!String.IsNullOrEmpty(row["RecipientKorpus"].ToString()))
                    {
                        recipientKorpus = string.Concat(new string[] { "/", row["RecipientKorpus"].ToString() });
                    }
                    
                    row["Sender"] = CityHelper.CityIDToCityName(row["CityID"].ToString());

                    row["Date"] = OtherMethods.DateConvert(row["CreateDate"].ToString());

                    row["SendDate"] = OtherMethods.DateConvert(row["DeliveryDate"].ToString());

                    var goodsQueryString = "SELECT * FROM `goods` WHERE TicketFullSecureId = " + "'" + row["FullSecureId"].ToString() + "'";
                    var dmGoods = new DataManager();
                    var dsGoods = dmGoods.QueryWithReturnDataSet(goodsQueryString);
                    foreach (DataRow rowGoods in dsGoods.Tables[0].Rows)
                    {
                        row["GoodsModel"] += rowGoods["Model"].ToString();
                    }

                    double.TryParse(row["GruzobozCost"].ToString(), out cost);
                    Type c = row["GruzobozCost"].GetType();
                    costSum += cost;
                }

                LabelSum1.Text = costSum.ToString("0.00");
                LabelSum2.Text = costSum.ToString("0.00");
                lvAllPrint.DataSource = ds;
                lvAllPrint.DataBind();
                /*
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
                    //row["CreateDate"] = OtherMethods.DateConvert(Eval("CreateDate").ToString());
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
                */
            }

            if (String.IsNullOrEmpty(idListString) || lvAllPrint.Items.Count == 0)
            {
                Page.Visible = false;
                Response.Write(Resources.PrintResources.PrintMapEmptyText);
            }
        }
    }
}