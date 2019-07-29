using Delivery.BLL;
using Delivery.BLL.Filters;
using Delivery.BLL.Helpers;
using Delivery.BLL.StaticMethods;
using Delivery.DAL;
using Delivery.DAL.DataBaseObjects;
using Delivery.WebServices.Objects;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Helpers;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI.WebControls;

namespace Delivery.AppServices
{
    /// <summary>
    /// Summary description for SaveAjaxService
    /// </summary>
    [WebService(Namespace = "DeliveryNetWebService")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class SaveAjaxService : System.Web.Services.WebService
    {        
        [WebMethod]
        public string SaveCheckboxWithoutAkcizaValue(string boolchecked, string goodsid, string appkey, string ticketid, string userid, string userip, string pagename)
        {
            if (appkey != Globals.Settings.AppServiceSecureKey) return "invalid app key";

            var goods = new Goods { ID = Convert.ToInt32(goodsid), WithoutAkciza = Convert.ToInt32(boolchecked) };
            goods.Update(Convert.ToInt32(userid), userip, pagename); //1

            var ticket = new Tickets { ID = Convert.ToInt32(ticketid) };
            ticket.GetById(); //2

            var newGoods = new Goods { TicketFullSecureID = ticket.FullSecureID };
            var goodsDataSet = newGoods.GetAllItems("ID", "ASC", "TicketFullSecureID"); //3

            var goodsList = (from DataRow row in goodsDataSet.Tables[0].Rows
                             select new GoodsFromAPI
                             {
                                 Description = row["Description"].ToString(),
                                 Number = Convert.ToInt32(row["Number"].ToString()),
                                 WithoutAkciza = Convert.ToInt32(row["WithoutAkciza"].ToString())
                             }).ToList();

            var profile = new UsersProfiles { ID = Convert.ToInt32(ticket.UserProfileID) };
            profile.GetById(); //4
            var agreedAccessedCost = ticket.AgreedCost != 0 ? (ticket.AgreedCost).ToString() : (ticket.AssessedCost).ToString();
            var gruzobozCost = Calculator.Calculate(goodsList, Convert.ToInt32(ticket.CityID), Convert.ToInt32(ticket.UserID), Convert.ToInt32(ticket.UserProfileID), profile.TypeID.ToString(), agreedAccessedCost);

            if (gruzobozCost != "0" && !String.IsNullOrEmpty(gruzobozCost))
            {
                ticket.GruzobozCost = Convert.ToDecimal(gruzobozCost);
                ticket.Update(Convert.ToInt32(userid), userip, pagename); //5
            }

            return "OK";
        }


        [WebMethod]
        public string SaveCheckboxWithoutMoneyValue(string boolchecked, string appkey, string ticketid, string userid, string userip, string pagename)
        {
            if (appkey != Globals.Settings.AppServiceSecureKey) return "invalid app key";

            var ticket = new Tickets() { ID = Convert.ToInt32(ticketid), WithoutMoney = Convert.ToInt32(boolchecked) };
            ticket.Update(Convert.ToInt32(userid), userip, pagename);
            return "OK";
        }

        [WebMethod]
        public string SaveCheckboxIsExchangeValue(string boolchecked, string appkey, string ticketid, string userid, string userip, string pagename)
        {
            if (appkey != Globals.Settings.AppServiceSecureKey) return "invalid app key";

            var ticket = new Tickets() { ID = Convert.ToInt32(ticketid), IsExchange = Convert.ToInt32(boolchecked) };
            ticket.Update(Convert.ToInt32(userid), userip, pagename);
            return "OK";
        }

        [WebMethod]
        public string SaveDeliveryCostValue(string deliverycost, string appkey, string ticketid, string userid, string userip, string pagename)
        {
            if (appkey != Globals.Settings.AppServiceSecureKey) return "invalid app key";

            var ticket = new Tickets() { ID = Convert.ToInt32(ticketid), DeliveryCost = Convert.ToInt32(deliverycost) };
            ticket.Update(Convert.ToInt32(userid), userip, pagename);
            return "OK";
        }

        [WebMethod]
        public string SavePrintNakl(string printnakl, string appkey, string ticketid, string userid, string userip, string pagename)
        {
            if (appkey != Globals.Settings.AppServiceSecureKey) return "invalid app key";

            var ticket = new Tickets
            {
                ID = Convert.ToInt32(ticketid),
                PrintNakl = Convert.ToInt32(printnakl)
            };
            ticket.Update(Convert.ToInt32(userid), userip, pagename);

            return "OK";
        }



        [WebMethod]
        public string SavePrintNaklInMap(string printnaklinmap, string availableotherdocuments, string appkey, string ticketid, string userid, string userip, string pagename)
        {
            if (appkey != Globals.Settings.AppServiceSecureKey) return "invalid app key";

            var ticket = new Tickets
            {
                ID = Convert.ToInt32(ticketid),
                PrintNaklInMap = Convert.ToInt32(printnaklinmap),
                AvailableOtherDocuments = Convert.ToInt32(availableotherdocuments),

            };
            ticket.Update(Convert.ToInt32(userid), userip, pagename);

            return "OK";
        }



        [WebMethod]
        public string SaveGruzobozCost(string gruzobozcost, string appkey, string ticketid, string userid, string userip, string pagename)
        {
            if (appkey != Globals.Settings.AppServiceSecureKey) return "invalid app key";

            var ticket = new Tickets
            {
                ID = Convert.ToInt32(ticketid)
            };
            ticket.GetById();
            if (new UserCost().GetCostByUserID(ticket.UserID) == null || Convert.ToDecimal(gruzobozcost) == 0)
            {
                ticket.GruzobozCost = Convert.ToDecimal(gruzobozcost);
                ticket.Update(Convert.ToInt32(userid), userip, pagename);
            }

            return "OK";
        }

        [WebMethod]
        public string SaveAgreedCost(string agreedcost, string appkey, string ticketid, string userid, string userip, string pagename)
        {
            if (appkey != Globals.Settings.AppServiceSecureKey) return "invalid app key";

            var ticket = new Tickets
            {
                ID = Convert.ToInt32(ticketid)
            };
            ticket.GetById();
            
            ticket.AgreedCost = Convert.ToDecimal(agreedcost);
            ticket.Update(Convert.ToInt32(userid), userip, pagename);            

            return "OK";
        }

        [WebMethod]
        public string SaveChangeTitle(string titleText, string appkey, string ticketid, string userid, string userip, string pagename)
        {
            if (appkey != Globals.Settings.AppServiceSecureKey) return "invalid app key";

            var ticket = new Tickets
            {
                ID = Convert.ToInt32(ticketid)
            };
            ticket.GetById();

            ticket.Note = titleText;
            ticket.NoteChanged = 1;
            ticket.Update(Convert.ToInt32(userid), userip, pagename);

            return "OK";
        }

        [WebMethod]
        public string SaveComment(string additionalcomment, string appkey, string ticketid, string userid, string userip, string pagename)
        {
            if (appkey != Globals.Settings.AppServiceSecureKey) return "invalid app key";
            if (string.IsNullOrWhiteSpace(additionalcomment)) return "empty string";

            var oldTicket = new Tickets { ID = Convert.ToInt32(ticketid) };
            oldTicket.GetById();
            var ticket = new Tickets
            {
                ID = Convert.ToInt32(ticketid),
                Comment = WebUtility.HtmlEncode(String.Format(
                                                              "<div class='comment-history-body'>{1}</div>" +
                                                              "<div>" +
                                                                "<span class='comment-history-name'>{0}</span>" +
                                                                "<span class='comment-history-date'>{2}</span>" +
                                                              "</div>{3}",
                    UsersHelper.UserIDToFullName(userid),
                    additionalcomment,
                    DateTime.Now.ToString("dd.MM в HH:mm"),
                    WebUtility.HtmlDecode(oldTicket.Comment))),
            };
            ticket.Update(Convert.ToInt32(userid), userip, pagename);

            return "OK";
        }



        [WebMethod]
        public string SaveCommentClient(string additionalcomment, string appkey, string clientid, string userid, string userip, string pagename)
        {
            if (appkey != Globals.Settings.AppServiceSecureKey) return "invalid app key";
            if (string.IsNullOrWhiteSpace(additionalcomment)) return "empty string";
            var oldClient = new Users() { ID = Convert.ToInt32(clientid) };
            oldClient.GetById();
            var client = new Users
            {
                ID = Convert.ToInt32(clientid),
                Note = WebUtility.HtmlEncode(String.Format(
                                                              "<div class='comment-history-body'>{1}</div>" +
                                                              "<div>" +
                                                                "<span class='comment-history-name'>{0}</span>" +
                                                                "<span class='comment-history-date'>{2}</span>" +
                                                              "</div>{3}",
                    UsersHelper.UserIDToFullName(userid),
                    additionalcomment,
                    DateTime.Now.ToString("dd.MM в HH:mm"),
                    WebUtility.HtmlDecode(oldClient.Note))),
            };
            client.Update(Convert.ToInt32(userid), userip, pagename);

            return "OK";
        }



        [WebMethod]
        public string SaveContactDate(string contactDate, string appkey, string clientid, string userid, string userip, string pagename)
        {
            if (appkey != Globals.Settings.AppServiceSecureKey) return "invalid app key";

            var client = new Users
            {
                ID = Convert.ToInt32(clientid),
                ContactDate = string.IsNullOrEmpty(contactDate) ? Convert.ToDateTime("01.01.0001 0:00:00") : Convert.ToDateTime(contactDate)
            };
            client.Update(Convert.ToInt32(userid), userip, pagename);

            return "OK";
        }



        [WebMethod]
        public string SaveWeight(string weight, string appkey, string ticketid, string userid, string userip, string pagename)
        {
            if (appkey != Globals.Settings.AppServiceSecureKey) return "invalid app key";

            var ticket = new Tickets
            {
                ID = Convert.ToInt32(ticketid),
                Weight = Convert.ToInt32(weight)
            };
            ticket.Update(Convert.ToInt32(userid), userip, pagename);

            return "OK";
        }

        [WebMethod]
        public string SaveKK(string kk, string appkey, string ticketid, string userid, string userip, string pagename)
        {
            if (appkey != Globals.Settings.AppServiceSecureKey) return "invalid app key";

            var ticket = new Tickets
            {
                ID = Convert.ToInt32(ticketid),
                BoxesNumber = Convert.ToInt32(kk)
            };
            ticket.Update(Convert.ToInt32(userid), userip, pagename);

            return "OK";
        }


        [WebMethod]
        public string SaveUserDiscount(string userdiscount, string appkey, string userid, string curentuserid, string curentuserip, string pagename)
        {
            if (appkey != Globals.Settings.AppServiceSecureKey) return "invalid app key";

            var user = new Users
            {
                ID = Convert.ToInt32(userid),
                Discount = Convert.ToInt32(userdiscount)
            };
            user.Update(Convert.ToInt32(curentuserid), curentuserip, pagename);

            return "OK";
        }



        [WebMethod]
        public string SavePrintNaklParams(string putevoi, string naklnumber, string naklseria, string nakldate, string appkey)
        {
            if (appkey != Globals.Settings.AppServiceSecureKey) return "invalid app key";

            var sql = string.Empty;

            if (!String.IsNullOrEmpty(naklnumber))
            {
                sql += String.Format("UPDATE `printdata` SET `NaklNumber` = '{0}';", naklnumber);
            }

            if (!String.IsNullOrEmpty(naklseria))
            {
                sql += String.Format("UPDATE `printdata` SET `NaklSeria` = '{0}';", naklseria);
            }

            if (!String.IsNullOrEmpty(nakldate))
            {
                sql += String.Format("UPDATE `printdata` SET `NaklDate` = '{0}';", nakldate);
            }

            if (!String.IsNullOrEmpty(putevoi))
            {
                sql += String.Format("UPDATE `printdata` SET `Putevoi` = '{0}';", putevoi);
            }

            var dm = new DataManager();
            dm.QueryWithoutReturnData(null, sql);
            return "OK";
        }

        [WebMethod]
        public string SaveMapParams(string appkey)
        {
            if (appkey != Globals.Settings.AppServiceSecureKey) return "invalid app key";

            var sql = string.Empty;
            var chec = String.Format("{0:dd.MM.yyyy}", System.DateTime.Now);
            sql += String.Format("UPDATE `printdata` SET `MapDate` = '{0}';", String.Format("{0:dd.MM.yyyy}", System.DateTime.Now));
            
            var dm = new DataManager();
            dm.QueryWithoutReturnData(null, sql);
            return "OK";
        }


        [WebMethod]
        public string SaveMoneyStatuses(string statusid, string appkey, string ticketid, string userid, string userip, string pagename)
        {
            if (appkey != Globals.Settings.AppServiceSecureKey) return "invalid app key";

            var user = new Users { ID = Convert.ToInt32(userid) };
            user.GetById();
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = rolesList.SingleOrDefault(u => u.Name.ToLower() == user.Role.ToLower());
            var currentTicket = new Tickets { ID = Convert.ToInt32(ticketid) };
            currentTicket.GetById();
            var updateTickket = new Tickets { ID = Convert.ToInt32(ticketid) };

            var statusError = TicketsFilter.StatusChangeFilter(ref updateTickket, currentTicket.DriverID.ToString(), currentTicket.StatusID.ToString(), currentTicket.StatusDescription, currentTicket.AdmissionDate.ToString(), null, statusid, null, currentRole);
            if (statusError != null) return statusError;

            updateTickket.Update(Convert.ToInt32(userid), userip, pagename);

            return "OK";
        }



        [WebMethod]
        public string SaveBLR(string money, string appkey, string ticketid, string userid, string userip, string pagename)
        {
            if (appkey != Globals.Settings.AppServiceSecureKey) return "invalid app key";

            string statusError = null;
            var user = new Users { ID = Convert.ToInt32(userid) };
            user.GetById();
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = rolesList.SingleOrDefault(u => u.Name.ToLower() == user.Role.ToLower());
            var currentTicket = new Tickets { ID = Convert.ToInt32(ticketid), };
            currentTicket.GetById();
            var updateTickket = new Tickets { ID = Convert.ToInt32(ticketid) };

            if (currentTicket.ReceivedEUR == 0 && currentTicket.ReceivedRUR == 0 && currentTicket.ReceivedUSD == 0 && money == "0")
            {
                statusError = TicketsFilter.StatusChangeFilter(ref updateTickket, currentTicket.DriverID.ToString(), currentTicket.StatusID.ToString(), currentTicket.StatusDescription, currentTicket.AdmissionDate.ToString(), null, "3", null, currentRole);
            }
            else
            {
                if (currentTicket.StatusID != 5 && currentTicket.StatusID != 7)
                {
                    var autoChangeProcessedStatus = BackendHelper.TagToValue("auto_change_processed_status");
                    if (autoChangeProcessedStatus == "true")
                    {
                        statusError = TicketsFilter.StatusChangeFilter(ref updateTickket, currentTicket.DriverID.ToString(), currentTicket.StatusID.ToString(), currentTicket.StatusDescription, currentTicket.AdmissionDate.ToString(), null, "5", null, currentRole);
                    }
                }
            }
            if (statusError != null) return statusError;

            updateTickket.ReceivedBLR = Convert.ToDecimal(money);
            updateTickket.Update(Convert.ToInt32(userid), userip, pagename);

            return "OK";
        }



        [WebMethod]
        public string SaveEUR(string money, string course, string appkey, string ticketid, string userid, string userip, string pagename)
        {
            if (appkey != Globals.Settings.AppServiceSecureKey) return "invalid app key";

            string statusError = null;
            var user = new Users { ID = Convert.ToInt32(userid) };
            user.GetById();
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = rolesList.SingleOrDefault(u => u.Name.ToLower() == user.Role.ToLower());
            var currentTicket = new Tickets { ID = Convert.ToInt32(ticketid) };
            currentTicket.GetById();
            var updateTickket = new Tickets { ID = Convert.ToInt32(ticketid) };

            if (currentTicket.ReceivedBLR == 0 && currentTicket.ReceivedRUR == 0 && currentTicket.ReceivedUSD == 0 && money == "0")
            {
                statusError = TicketsFilter.StatusChangeFilter(ref updateTickket, currentTicket.DriverID.ToString(), currentTicket.StatusID.ToString(), currentTicket.StatusDescription, currentTicket.AdmissionDate.ToString(), null, "3", null, currentRole);
            }
            else
            {
                if (currentTicket.StatusID != 5)
                {
                    var autoChangeProcessedStatus = BackendHelper.TagToValue("auto_change_processed_status");
                    if (autoChangeProcessedStatus == "true")
                    {
                        statusError = TicketsFilter.StatusChangeFilter(ref updateTickket, currentTicket.DriverID.ToString(), currentTicket.StatusID.ToString(), currentTicket.StatusDescription, currentTicket.AdmissionDate.ToString(), null, "5", null, currentRole);
                    }
                }
            }
            if (statusError != null) return statusError;

            updateTickket.CourseEUR = Convert.ToInt32(course) == 0 ? 1 : Convert.ToInt32(course);
            updateTickket.ReceivedEUR = Convert.ToInt32(money);
            updateTickket.Update(Convert.ToInt32(userid), userip, pagename);

            return "OK";
        }



        [WebMethod]
        public string SaveRUR(string money, string course, string appkey, string ticketid, string userid, string userip, string pagename)
        {
            if (appkey != Globals.Settings.AppServiceSecureKey) return "invalid app key";

            string statusError = null;
            var user = new Users { ID = Convert.ToInt32(userid) };
            user.GetById();
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = rolesList.SingleOrDefault(u => u.Name.ToLower() == user.Role.ToLower());
            var currentTicket = new Tickets { ID = Convert.ToInt32(ticketid) };
            currentTicket.GetById();
            var updateTickket = new Tickets { ID = Convert.ToInt32(ticketid) };

            if (currentTicket.ReceivedEUR == 0 && currentTicket.ReceivedBLR == 0 && currentTicket.ReceivedUSD == 0 && money == "0")
            {
                statusError = TicketsFilter.StatusChangeFilter(ref updateTickket, currentTicket.DriverID.ToString(), currentTicket.StatusID.ToString(), currentTicket.StatusDescription, currentTicket.AdmissionDate.ToString(), null, "3", null, currentRole);
            }
            else
            {
                if (currentTicket.StatusID != 5)
                {
                    var autoChangeProcessedStatus = BackendHelper.TagToValue("auto_change_processed_status");
                    if (autoChangeProcessedStatus == "true")
                    {
                        statusError = TicketsFilter.StatusChangeFilter(ref updateTickket, currentTicket.DriverID.ToString(), currentTicket.StatusID.ToString(), currentTicket.StatusDescription, currentTicket.AdmissionDate.ToString(), null, "5", null, currentRole);
                    }
                }
            }
            if (statusError != null) return statusError;

            updateTickket.CourseRUR = Convert.ToInt32(course) == 0 ? 1 : Convert.ToInt32(course);
            updateTickket.ReceivedRUR = Convert.ToInt32(money);
            updateTickket.Update(Convert.ToInt32(userid), userip, pagename);

            return "OK";
        }



        [WebMethod]
        public string SaveUSD(string money, string course, string appkey, string ticketid, string userid, string userip, string pagename)
        {
            if (appkey != Globals.Settings.AppServiceSecureKey) return "invalid app key";

            string statusError = null;
            var user = new Users { ID = Convert.ToInt32(userid) };
            user.GetById();
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = rolesList.SingleOrDefault(u => u.Name.ToLower() == user.Role.ToLower());
            var currentTicket = new Tickets { ID = Convert.ToInt32(ticketid) };
            currentTicket.GetById();
            var updateTickket = new Tickets { ID = Convert.ToInt32(ticketid) };

            if (currentTicket.ReceivedEUR == 0 && currentTicket.ReceivedBLR == 0 && currentTicket.ReceivedRUR == 0 && money == "0")
            {
                statusError = TicketsFilter.StatusChangeFilter(ref updateTickket, currentTicket.DriverID.ToString(), currentTicket.StatusID.ToString(), currentTicket.StatusDescription, currentTicket.AdmissionDate.ToString(), null, "3", null, currentRole);
            }
            else
            {
                if (currentTicket.StatusID != 5)
                {
                    var autoChangeProcessedStatus = BackendHelper.TagToValue("auto_change_processed_status");
                    if (autoChangeProcessedStatus == "true")
                    {
                        statusError = TicketsFilter.StatusChangeFilter(ref updateTickket, currentTicket.DriverID.ToString(), currentTicket.StatusID.ToString(), currentTicket.StatusDescription, currentTicket.AdmissionDate.ToString(), null, "5", null, currentRole);
                    }
                }
            }
            if (statusError != null) return statusError;

            updateTickket.CourseUSD = Convert.ToInt32(course) == 0 ? 1 : Convert.ToInt32(course);
            updateTickket.ReceivedUSD = Convert.ToInt32(money);
            updateTickket.Update(Convert.ToInt32(userid), userip, pagename);

            return "OK";
        }



        [WebMethod]
        public string SaveCheckboxPril2(string boolchecked, string appkey, string ticketid, string userid, string userip, string pagename)
        {
            if (appkey != Globals.Settings.AppServiceSecureKey) return "invalid app key";

            var ticket = new Tickets
            {
                ID = Convert.ToInt32(ticketid),
                NotPrintInPril2 = Convert.ToInt32(boolchecked)
            };
            ticket.Update(); //без логирования

            return "OK";
        }



        [WebMethod]
        public string SaveWeightToPril2(string weight, string appkey, string ticketid, string userid, string userip, string pagename)
        {
            if (appkey != Globals.Settings.AppServiceSecureKey) return "invalid app key";

            var ticket = new Tickets
            {
                ID = Convert.ToInt32(ticketid),
                Pril2Weight = Convert.ToInt32(weight)
            };
            ticket.Update(); //без логирования

            return "OK";
        }


        [WebMethod]
        public string SaveBoxesNumberToPril2(string boxesnumber, string appkey, string ticketid, string userid, string userip, string pagename)
        {
            if (appkey != Globals.Settings.AppServiceSecureKey) return "invalid app key";

            var ticket = new Tickets
            {
                ID = Convert.ToInt32(ticketid),
                Pril2BoxesNumber = Convert.ToInt32(boxesnumber)
            };
            ticket.Update(); //без логирования

            return "OK";
        }

        [WebMethod]
        public string SaveCheckboxPhonedValue(string value, string appkey, string ticketid, string userid, string userip, string pagename)
        {
            if (appkey != Globals.Settings.AppServiceSecureKey) return "invalid app key";

            var ticket = new Tickets
            {
                ID = Convert.ToInt32(ticketid),
                Phoned = Convert.ToInt32(value)
            };
            ticket.Update(Convert.ToInt32(userid), userip, pagename);

            return "OK";
        }

        [WebMethod]
        public string SaveCheckboxCheckedOutValue(string value, string appkey, string ticketid, string userid, string userip, string pagename)
        {
            if (appkey != Globals.Settings.AppServiceSecureKey) return "invalid app key";

            var ticket = new Tickets
            {
                ID = Convert.ToInt32(ticketid),
                CheckedOut = Convert.ToInt32(value)
            };
            ticket.Update(Convert.ToInt32(userid), userip, pagename);

            return "OK";
        }

        [WebMethod]
        public string SaveCheckboxBilledValue(string value, string appkey, string ticketid, string userid, string userip, string pagename)
        {
            if (appkey != Globals.Settings.AppServiceSecureKey) return "invalid app key";

            var ticket = new Tickets
            {
                ID = Convert.ToInt32(ticketid),
                Billed = Convert.ToInt32(value)
            };
            ticket.Update(Convert.ToInt32(userid), userip, pagename);

            return "OK";
        }

        [WebMethod]
        public string SaveDeliveryDateFrom(string value, string appkey, string ticketidlist, string userid, string userip, string pagename)
        {
            if (appkey != Globals.Settings.AppServiceSecureKey) return "invalid app key";
            var idList = ticketidlist.Split('-').ToList();
            foreach (var ticketid in idList)
            {
                var ticket = new Tickets { ID = Convert.ToInt32(ticketid) };
                if (String.IsNullOrEmpty(value))
                {
                    ticket.OvDateFrom = Convert.ToDateTime("01.01.0001 0:00:00");
                }
                else
                {
                    ticket.OvDateFrom = Convert.ToDateTime(Convert.ToDateTime(value).ToString("2000-01-01 HH:mm"));
                }

                ticket.Update(Convert.ToInt32(userid), userip, pagename);
            }

            return "OK";
        }

        [WebMethod]
        public string SaveDeliveryDateTo(string value, string appkey, string ticketidlist, string userid, string userip, string pagename)
        {
            if (appkey != Globals.Settings.AppServiceSecureKey) return "invalid app key";
            var idList = ticketidlist.Split('-').ToList();
            foreach (var ticketid in idList)
            {
                var ticket = new Tickets { ID = Convert.ToInt32(ticketid) };
                if (String.IsNullOrEmpty(value))
                {
                    ticket.OvDateTo = Convert.ToDateTime("01.01.0001 0:00:00");
                }
                else
                {
                    ticket.OvDateTo = Convert.ToDateTime(Convert.ToDateTime(value).ToString("2000-01-01 HH:mm"));
                }
                ticket.Update(Convert.ToInt32(userid), userip, pagename);
            }

            return "OK";
        }

        [WebMethod]
        public string SaveDeliveredForCity(string appkey, string ticketidlist, string userid, string userip, string pagename)
        {
            if (appkey != Globals.Settings.AppServiceSecureKey) return "invalid app key";
            var user = new Users { ID = Convert.ToInt32(userid) };
            user.GetById();
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = rolesList.SingleOrDefault(u => u.Name.ToLower() == user.Role.ToLower());
            var idList = ticketidlist.Split('-').ToList();
            foreach (var ticketid in idList)
            {
                var currentTicket = new Tickets { ID = Convert.ToInt32(ticketid) };
                currentTicket.GetById();
                var updateTickket = new Tickets { ID = Convert.ToInt32(ticketid) };
                TicketsFilter.StatusChangeFilter(ref updateTickket, currentTicket.DriverID.ToString(), currentTicket.StatusID.ToString(), currentTicket.StatusDescription, currentTicket.AdmissionDate.ToString(), null, "12", null, currentRole);
                updateTickket.Update(Convert.ToInt32(userid), userip, pagename);
            }

            return "OK";
        }

        [WebMethod]
        public string SaveCityOrder(string cityid, string driverid, string order, string appkey, string userid, string userip, string pagename)
        {
            if (appkey != Globals.Settings.AppServiceSecureKey) return "invalid app key";

            var driver = new Drivers { ID = Convert.ToInt32(driverid) };
            driver.GetById();
            var jsonString = driver.CityOrder;
            var cityOrderList = new List<CityInOrder>();
            var js = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue };
            //если обновляем запись у водителя
            if (!String.IsNullOrEmpty(jsonString))
            {

                cityOrderList = js.Deserialize<List<CityInOrder>>(jsonString);
                var cityInOrderList = cityOrderList.FirstOrDefault(u => u.cityid == Convert.ToInt32(cityid));
                if (cityInOrderList != null)
                {
                    cityOrderList.First(u => u.cityid == Convert.ToInt32(cityid)).order = Convert.ToInt32(order);
                }
                else
                {
                    cityOrderList.Add(new CityInOrder
                    {
                        cityid = Convert.ToInt32(cityid),
                        order = Convert.ToInt32(order)
                    });
                }
            }
            else
            {
                cityOrderList.Add(new CityInOrder
                {
                    cityid = Convert.ToInt32(cityid),
                    order = Convert.ToInt32(order)
                });
            }
            driver.CityOrder = js.Serialize(cityOrderList);
            driver.Update();
            return "OK";
        }

        [WebMethod]
        public string SaveFeedbackComment(string currentUserId, string commentId, string comment, string appkey)
        {
            if (appkey != Globals.Settings.AppServiceSecureKey) return "invalid app key";
            var feedbackcomment = new FeedbackComments
            {
                ID = Convert.ToInt32(Convert.ToInt32(commentId)),
            };
            feedbackcomment.GetById();
            if (feedbackcomment.UserID.ToString() != currentUserId)
            {
                return "invalid user";
            }
            feedbackcomment.Comment = comment;
            feedbackcomment.Update();
            return "OK";
        }
    }
}
