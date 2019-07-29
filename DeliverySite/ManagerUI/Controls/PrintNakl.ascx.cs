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
    public partial class PrintNakl : System.Web.UI.UserControl
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

        public string PrintNaklValue
        {
            get
            {
                return ViewState["PrintNakllValue"] != null ? ViewState["PrintNakllValue"].ToString() : String.Empty;
            }
            set
            {
                ViewState["PrintNakllValue"] = value;
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
            ddlPrintNakl.SelectedValue = PrintNaklValue;
            var userInSession = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());
            if (currentRole.ActionControlPN != 1)
            {
                ddlPrintNakl.Enabled = false;
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