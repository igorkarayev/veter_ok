using System;
using System.Collections.Generic;
using System.Linq;
using Delivery.BLL;
using Delivery.BLL.StaticMethods;
using Delivery.DAL.DataBaseObjects;

namespace Delivery.ManagerUI.Controls
{
    public partial class DeliveryFromToTime : System.Web.UI.UserControl
    {
        public string CityRowId
        {
            get {
                return ViewState["CityRowId"] != null ? ViewState["CityRowId"].ToString() : String.Empty;
            }
            set
            {
                ViewState["CityRowId"] = value;
            }
        }

        public string OvdFrom
        {
            get
            {
                return (ViewState["OvdFrom"] != null && !String.IsNullOrEmpty(ViewState["OvdFrom"].ToString())) ? Convert.ToDateTime(ViewState["OvdFrom"].ToString()).ToString() : String.Empty;
            }
            set
            {
                ViewState["OvdFrom"] = value;
            }
        }

        public string OvdTo
        {
            get
            {
                return (ViewState["OvdTo"] != null && !String.IsNullOrEmpty(ViewState["OvdTo"].ToString())) ? Convert.ToDateTime(ViewState["OvdTo"].ToString()).ToString() : String.Empty;
            }
            set
            {
                ViewState["OvdTo"] = value;
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