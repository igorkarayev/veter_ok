using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Delivery.BLL;
using Delivery.BLL.Helpers;
using Delivery.BLL.StaticMethods;
using Delivery.DAL.DataBaseObjects;

namespace Delivery.ManagerUI.Controls
{
    public partial class SenderAddress : System.Web.UI.UserControl
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

        protected void Page_Load()
        {
            AppKey = Globals.Settings.AppServiceSecureKey;
            UserIP = OtherMethods.GetIPAddress();
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            var ticket = new Tickets {ID = Convert.ToInt32(TicketID)};
            ticket.GetById();
            if (!String.IsNullOrEmpty(ticket.SenderCityID.ToString()) && ticket.SenderCityID !=0)
            {
                lblResult.Text = CityHelper.CityIDToCityName(ticket.SenderCityID.ToString()) + ", " + ticket.SenderStreetName + " " + ticket.SenderStreetNumber;
                if (!String.IsNullOrEmpty(ticket.SenderHousing))
                {
                    lblResult.Text += "/" + ticket.SenderHousing;
                }
                if (!String.IsNullOrEmpty(ticket.SenderApartmentNumber))
                {
                    lblResult.Text += " кв." + ticket.SenderApartmentNumber;
                }
            }
            
        }
    }
}