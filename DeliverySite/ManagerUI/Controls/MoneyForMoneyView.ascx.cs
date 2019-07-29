using System;
using System.Collections.Generic;
using System.Linq;
using Delivery.BLL;
using Delivery.BLL.StaticMethods;
using Delivery.DAL.DataBaseObjects;

namespace Delivery.ManagerUI.Controls
{
    public partial class MoneyForMoneyView : System.Web.UI.UserControl
    {
        public string AgreedAccessedCost { get; set; }
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

        public string SessionUserID { get; set; }

        public string SessionUserIP { get; set; }
        

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (hfWithoutMoney.Value == "1")
            {
                cbWithoutMoney.Checked = true;
            }

            if (hfIsExchange.Value == "1")
            {
                cbIsExchange.Checked = true;
            }

            var userInSession = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());
            if (currentRole.ActionControlMoneyCheckboxes != 1)
            {
                cbIsExchange.Enabled = false;
                cbWithoutMoney.Enabled = false;
            }

            if(currentRole.ActionAllowChangeMoneyAndCourse != 1)
            {
                tbAgreedAssessedCosts.Enabled = false;
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            AppKey = Globals.Settings.AppServiceSecureKey;
            UserIP = OtherMethods.GetIPAddress();
            var user = (Users)Session["userinsession"];
            UserID = user.ID.ToString();
        }
    }
}