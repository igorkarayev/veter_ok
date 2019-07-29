using Delivery.BLL.StaticMethods;
using Delivery.DAL.DataBaseObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Delivery.ManagerUI.Controls.Clients
{
    public partial class ContactDate : System.Web.UI.UserControl
    {
        public string ClientID
        {
            get
            {
                return ViewState["ClientID"] != null ? ViewState["ClientID"].ToString() : String.Empty;
            }
            set
            {
                ViewState["ClientID"] = value;
            }
        }

        public string ClientStatus
        {
            get
            {
                return ViewState["ClientStatus"] != null ? ViewState["ClientStatus"].ToString() : String.Empty;
            }
            set
            {
                ViewState["ClientStatus"] = value;
            }
        }

        public string DateValue
        {
            get
            {
                return ViewState["DateValue"] != null ? ViewState["DateValue"].ToString() : String.Empty;
            }
            set
            {
                ViewState["DateValue"] = value;
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
            tbContactDate.Text = OtherMethods.DateConvert(DateValue);
            var userInSession = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());
            if (currentRole.PageClientsEdit != 1)
            {
                tbContactDate.Enabled = false;
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