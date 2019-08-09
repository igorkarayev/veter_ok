using Delivery;
using Delivery.BLL.StaticMethods;
using Delivery.DAL.DataBaseObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeliverySite.ManagerUI.Controls
{
    public partial class ChangeAddress : System.Web.UI.UserControl
    {
        public string AgreedAccessedCost { get; set; }
        public string TicketID
        {
            get
            {
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
            var userInSession = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());

            if (TicketID != String.Empty && TicketID != " ")
            {
                _ticketID.Value = TicketID;
                var address = OtherMethods.GetRecipientAddress(TicketID);
                ddlSenderStreetPrefix.SelectedValue = address.ContainsKey(0) ? address[0] : "ул.";
                tbSenderStreetName.Text = address.ContainsKey(1) ? address[1] : "";
                tbSenderStreetNumber.Text = address.ContainsKey(2) ? address[2] : "";
                tbSenderHousing.Text = address.ContainsKey(3) ? address[3] : "";
                tbSenderApartmentNumber.Text = address.ContainsKey(4) ? address[4] : "";
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            AppKey = Globals.Settings.AppServiceSecureKey;
            UserIP = OtherMethods.GetIPAddress();
            var user = (Users)Session["userinsession"];
            UserID = user.ID.ToString();
        }

        protected void btnSaveClick(object sender, EventArgs e)
        {
            var ticket = new Tickets { ID = Convert.ToInt32(_ticketID.Value) };
            ticket.GetById();
            ticket.RecipientStreetPrefix = ddlSenderStreetPrefix.SelectedValue;
            ticket.RecipientStreet = tbSenderStreetName.Text;
            ticket.RecipientStreetNumber = tbSenderStreetNumber.Text;
            ticket.RecipientKorpus = tbSenderHousing.Text;
            ticket.RecipientKvartira = tbSenderApartmentNumber.Text;

            ticket.Update();
        }
    }
}