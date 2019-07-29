using Delivery.DAL;
using System;

namespace Delivery.ManagerUI.Controls.Clients
{
    public partial class UserCategory : System.Web.UI.UserControl
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

        protected void Page_PreRender(object sender, EventArgs e)
        {
            var dm = new DataManager();
            var usersToCategory = dm.QueryWithReturnDataSet("SELECT * FROM `userstocategory` WHERE `UserID` = " + ClientID);
            if (usersToCategory.Tables[0].Rows.Count == 0)
                lvAllTracks.Visible = true;
            lvAllTracks.DataSource = usersToCategory;
            lvAllTracks.DataBind();
        }
    }
}