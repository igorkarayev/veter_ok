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
using Delivery.Resources;
using DeliverySite.Resources;

namespace Delivery.ManagerUI.Controls
{
    public partial class MoneyStatuses : System.Web.UI.UserControl
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

        public string StatusID
        {
            get
            {
                return ViewState["StatusID"] != null ? ViewState["StatusID"].ToString() : String.Empty;
            }
            set
            {
                ViewState["StatusID"] = value;
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
            ddlMoneyStatuses.SelectedValue = StatusID;
            ddlMoneyStatuses.Items.FindByText("4").Text = TicketStatusesResources.Transfer_InStock;
            ddlMoneyStatuses.Items.FindByText("3").Text = TicketStatusesResources.OnTheWay;
            ddlMoneyStatuses.Items.FindByText("5").Text = TicketStatusesResources.Processed;
            ddlMoneyStatuses.Items.FindByText("12").Text = TicketStatusesResources.Delivered;
            ddlMoneyStatuses.Items.FindByText("11").Text = TicketStatusesResources.Transfer_InCourier;
            ddlMoneyStatuses.Items.FindByText("7").Text = TicketStatusesResources.Refusing_InCourier;
            ddlMoneyStatuses.Items.FindByText("13").Text = TicketStatusesResources.Exchange_InCourier;
            ddlMoneyStatuses.Items.FindByText("14").Text = TicketStatusesResources.DeliveryFromClient_InCourier;
            ddlMoneyStatuses.Items.FindByText("8").Text = TicketStatusesResources.Return_InStock;
            ddlMoneyStatuses.Items.FindByText("15").Text = TicketStatusesResources.Exchange_InStock;
            ddlMoneyStatuses.Items.FindByText("16").Text = TicketStatusesResources.DeliveryFromClient_InStock;
            ddlMoneyStatuses.Items.FindByText("17").Text = TicketStatusesResources.Refusal_OnTheWay;
            ddlMoneyStatuses.Items.FindByText("18").Text = TicketStatusesResources.Refusal_ByAddress;
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