using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Delivery.BLL.Filters;
using Delivery.BLL.StaticMethods;
using Delivery.DAL;
using Delivery.DAL.DataBaseObjects;

namespace Delivery.BLL.Helpers
{
    public class IssuanceListsHelper
    {
        public static bool ReOpenIssuanceList(Int32 issuanceListId)
        {
            var isAllTicketsChanged = true;
            var user = (Users)HttpContext.Current.Session["userinsession"];
            var rolesList = HttpContext.Current.Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == user.Role.ToLower());
            var currentTickets = new Tickets { IssuanceListID = issuanceListId };
            var ds = currentTickets.GetAllItems("ID", "ASC", "IssuanceListID");
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                var currentTicket = new Tickets { ID = Convert.ToInt32(row["ID"]) };
                currentTicket.GetById();
                var updateTicket = new Tickets { ID = Convert.ToInt32(row["ID"]) };
                var statusError = TicketsFilter.StatusChangeFilter(ref updateTicket, currentTicket.DriverID.ToString(), currentTicket.StatusID.ToString(), currentTicket.StatusDescription, currentTicket.AdmissionDate.ToString(), null, currentTicket.StatusIDOld.ToString(), null, currentRole);
                if (statusError != null && isAllTicketsChanged == true)
                {
                    isAllTicketsChanged = false;
                }
                updateTicket.Update(user.ID, OtherMethods.GetIPAddress(), "IssuanceListsView");
            }
            var issuanceList = new IssuanceLists { ID = issuanceListId };
            issuanceList.GetById();
            issuanceList.IssuanceListsStatusID = 3;
            issuanceList.Update();
            return isAllTicketsChanged;
        }

        public static bool CloseIssuanceList(Int32 issuanceListId)
        {
            var isAllTicketsChanged = true;
            var user = (Users)HttpContext.Current.Session["userinsession"];
            var rolesList = HttpContext.Current.Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == user.Role.ToLower());
            var currentTickets = new Tickets { IssuanceListID = issuanceListId };
            var ds = currentTickets.GetAllItems("ID", "ASC", "IssuanceListID");
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                var currentTicket = new Tickets { ID = Convert.ToInt32(row["ID"]) };
                currentTicket.GetById();
                var updateTicket = new Tickets { ID = Convert.ToInt32(row["ID"]) };
                var statusError = TicketsFilter.StatusChangeFilter(ref updateTicket, currentTicket.DriverID.ToString(), currentTicket.StatusID.ToString(), currentTicket.StatusDescription, currentTicket.AdmissionDate.ToString(), null, "6", null, currentRole);
                if (statusError != null && isAllTicketsChanged == true)
                {
                    isAllTicketsChanged = false;
                }
                updateTicket.Update(user.ID, OtherMethods.GetIPAddress(), "IssuanceListsView");
            }
            var issuanceList = new IssuanceLists { ID = issuanceListId };
            issuanceList.GetById();
            issuanceList.IssuanceListsStatusID = 2;
            issuanceList.Update();
            return isAllTicketsChanged;
        }

        public static void DeleteIssuanceList(Int32 issuanceListId)
        {
            var issuanceList = new IssuanceLists();
            issuanceList.Delete(issuanceListId);
        }

        public static String IssuanceStatusToText(string val)
        {
            return IssuanceLists.IssuanceListsStatuses.Where(u => u.Key == Convert.ToInt32(val)).Select(p => p.Value.ToString()).First();
        }
    }
}