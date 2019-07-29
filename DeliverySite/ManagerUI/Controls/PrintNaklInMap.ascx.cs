using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Delivery.BLL;
using Delivery.BLL.StaticMethods;
using Delivery.DAL.DataBaseObjects;

namespace Delivery.ManagerUI.Controls
{
    public partial class PrintNaklInMap : System.Web.UI.UserControl
    {
        public string TicketID
        {
            get {
                return ViewState["TicketID"] != null ? ViewState["TicketID"].ToString() : String.Empty;
            }
            set
            {
                ViewState["TicketID"] = value;
            }
        }

        public string PrintNaklInMapValue
        {
            get
            {
                return ViewState["PrintNaklInMapValue"] != null ? ViewState["PrintNaklInMapValue"].ToString() : String.Empty;
            }
            set
            {
                ViewState["PrintNaklInMapValue"] = value;
            }
        }

        public string AvailableOtherDocuments
        {
            get
            {
                return ViewState["AvailableOtherDocuments"] != null ? ViewState["AvailableOtherDocuments"].ToString() : String.Empty;
            }
            set
            {
                ViewState["AvailableOtherDocuments"] = value;
            }
        }

        public string ListViewControlFullID
        {
            get
            {
                return ViewState["ListViewControlFullID"] != null ? ViewState["ListViewControlFullID"].ToString() : String.Empty;
            }
            set
            {
                ViewState["ListViewControlFullID"] = value;
            }
        }

        public string PageName
        {
            get
            {
                return ViewState["PageName"] != null ? ViewState["PageName"].ToString() : String.Empty;
            }
            set
            {
                ViewState["PageName"] = value;
            }
        }

        public string UserID { get; set; }

        public string UserIP { get; set; }

        protected String AppKey { get; set; }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            ddlPrintNaklInMap.SelectedValue = PrintNaklInMapValue;
            ddlAvailableOtherDocuments.SelectedValue = AvailableOtherDocuments;
            var userInSession = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());
            if (currentRole.ActionControlNN != 1)
            {
                ddlPrintNaklInMap.Enabled = false;
            }
        }

        protected void Page_Init()
        {
            AppKey = Globals.Settings.AppServiceSecureKey;
            UserIP = OtherMethods.GetIPAddress();
            var user = (Users)Session["userinsession"];
            UserID = user.ID.ToString();
        }
    }
}