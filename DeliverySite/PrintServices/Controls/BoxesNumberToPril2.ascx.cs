using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Delivery.BLL;
using Delivery.BLL.StaticMethods;
using Delivery.DAL.DataBaseObjects;

namespace Delivery.PrintServices.Controls
{
    public partial class BoxesNumberToPril2 : System.Web.UI.UserControl
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

        public string Pril2BoxesNumber
        {
            get
            {
                return ViewState["Pril2BoxesNumber"] != null ? ViewState["Pril2BoxesNumber"].ToString() : String.Empty;
            }
            set
            {
                ViewState["Pril2BoxesNumber"] = value;
            }
        }

        public string BoxesNumber
        {
            get
            {
                return ViewState["BoxesNumber"] != null ? ViewState["BoxesNumber"].ToString() : String.Empty;
            }
            set
            {
                ViewState["BoxesNumber"] = value;
            }
        }

        public string UserID { get; set; }

        public string UserIP { get; set; }

        protected String AppKey { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            tbPril2BoxesNumber.Text = MoneyMethods.MoneySeparator(Pril2BoxesNumber);
            if (Pril2BoxesNumber == "0" || String.IsNullOrEmpty(Pril2BoxesNumber))
            {
                lblPril2BoxesNumber.Text = MoneyMethods.MoneySeparator(BoxesNumber);
            }
            else
            {
                lblPril2BoxesNumber.Text = MoneyMethods.MoneySeparator(Pril2BoxesNumber);
                lblPril2BoxesNumber.ForeColor = Color.Red;
            }
            
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            AppKey = Globals.Settings.AppServiceSecureKey;
            UserIP = OtherMethods.GetIPAddress();
            var user = (Users)Session["userinsession"];
            UserID = user.ID.ToString();
        }

        protected void Page_Load()
        {
        }
    }
}