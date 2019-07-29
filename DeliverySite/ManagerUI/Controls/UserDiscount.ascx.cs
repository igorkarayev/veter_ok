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
    public partial class UserDiscount : System.Web.UI.UserControl
    {
        public string UserID
        {
            get {
                return ViewState["UserID"] != null ? ViewState["UserID"].ToString() : String.Empty;
            }
            set
            {
                ViewState["UserID"] = value;
            }
        }
        public string UserDiscountValue
        {
            get
            {
                return ViewState["UserDiscountValue"] != null ? ViewState["UserDiscountValue"].ToString() : String.Empty;
            }
            set
            {
                ViewState["UserDiscountValue"] = value;
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

        public string CurentUserID { get; set; }

        public string CurentUserIP { get; set; }

        protected String AppKey { get; set; }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            tbUserDiscount.Text = UserDiscountValue;
        }

        protected void Page_Init()
        {
            AppKey = Globals.Settings.AppServiceSecureKey;
            CurentUserIP = OtherMethods.GetIPAddress();
            var user = (Users)Session["userinsession"];
            CurentUserID = user.ID.ToString();
        }
    }
}