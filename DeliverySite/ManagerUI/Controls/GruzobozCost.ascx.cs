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
    public partial class GruzobozCost : System.Web.UI.UserControl
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

        public string UserID
        {
            get
            {
                return ViewState["UserID"] != null ? ViewState["UserID"].ToString() : String.Empty;
            }
            set
            {
                ViewState["UserID"] = value;
            }
        }

        public string GruzobozCostValue
        {
            get
            {
                return ViewState["GruzobozCostValue"] != null ? ViewState["GruzobozCostValue"].ToString() : String.Empty;
            }
            set
            {
                ViewState["GruzobozCostValue"] = value;
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

        public string SessionUserID { get; set; }

        public string SessionUserIP { get; set; }

        protected String AppKey { get; set; }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            tbGruzobozCost.Text = MoneyMethods.MoneySeparator(GruzobozCostValue);
            var userInSession = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());
            if (currentRole.ActionControlGruzobozCost != 1)
            {
                tbGruzobozCost.Enabled = false;
            }
        }

        protected void Page_Init()
        {
            AppKey = Globals.Settings.AppServiceSecureKey;
            SessionUserIP = OtherMethods.GetIPAddress();
            var user = (Users)Session["userinsession"];
            SessionUserID = user.ID.ToString();
        }
    }
}