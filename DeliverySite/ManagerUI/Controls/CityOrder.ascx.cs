using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using Delivery.BLL.StaticMethods;
using Delivery.DAL.DataBaseObjects;

namespace Delivery.ManagerUI.Controls
{
    public partial class CityOrder : System.Web.UI.UserControl
    {
        public string CityRowId
        {
            get
            {
                return ViewState["CityRowId"] != null ? ViewState["CityRowId"].ToString() : String.Empty;
            }
            set
            {
                ViewState["CityRowId"] = value;
            }
        }

        public string CityIdValue
        {
            get
            {
                return ViewState["CityIdValue"] != null ? ViewState["CityIdValue"].ToString() : String.Empty;
            }
            set
            {
                ViewState["CityIdValue"] = value;
            }
        }

        public string DriverIdValue
        {
            get
            {
                return ViewState["DriverIdValue"] != null ? ViewState["DriverIdValue"].ToString() : String.Empty;
            }
            set
            {
                ViewState["DriverIdValue"] = value;
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
            var driver = new Drivers {ID = Convert.ToInt32(DriverIdValue)};
            driver.GetById();
            var jsonString = driver.CityOrder;
            if (String.IsNullOrEmpty(jsonString)) return;
            var js = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue };
            var cityInOrderList = js.Deserialize<List<CityInOrder>>(jsonString);
            foreach (var cityInOrder in cityInOrderList)
            {
                if (cityInOrder.cityid == Convert.ToInt32(CityIdValue))
                {
                    tbCityOrder.Text = cityInOrder.order.ToString();
                }
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