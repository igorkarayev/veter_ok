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
    public partial class GoodsList : System.Web.UI.UserControl
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
            var userInSession = (Users)Session["userinsession"];
            UserID = userInSession.ID.ToString();
            
            pnlOldTickets.Visible = false;
            lvAllGoods.Visible = true;
            var goods = new Goods {TicketFullSecureID = ticket.FullSecureID};
            lvAllGoods.DataSource = goods.GetAllItems("ID", "ASC", "TicketFullSecureID");
            lvAllGoods.DataBind();
            foreach (var item in lvAllGoods.Items)
            {
                var lblGoodsDescription = (Label)item.FindControl("lblDescription");
                var pnlWithoutAkciza = (Panel)item.FindControl("pnlWithoutAkciza");
                var cbWithoutAkciza = (CheckBox)item.FindControl("cbWithoutAkciza");
                var hfWithoutAkciza = (HiddenField)item.FindControl("hfWithoutAkciza");
                var category = new Titles { Name = lblGoodsDescription.Text };
                category.GetByName();
                if (category.ID != 0 && category.CanBeWithoutAkciza != 0)
                {
                    pnlWithoutAkciza.Visible = true;
                    if (hfWithoutAkciza.Value == "1")
                    {
                        cbWithoutAkciza.Checked = true;
                    }
                }
                //ограничения 
                var rolesList = Application["RolesList"] as List<Roles>;
                var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());
                if (currentRole.ActionControlActiza != 1)
                {
                    cbWithoutAkciza.Enabled = false;
                }
            }
            
        }

    }
}