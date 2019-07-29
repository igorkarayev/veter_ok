using Delivery.DAL;
using System;

namespace Delivery.ManagerUI.Controls.Clients
{
    public partial class UserProfilesList : System.Web.UI.UserControl
    {
        public string UserId
        {
            get
            {
                return ViewState["UserId"] != null ? ViewState["UserId"].ToString() : string.Empty;
            }
            set
            {
                ViewState["UserId"] = value;
            }
        }

        public string Status
        {
            get
            {
                return ViewState["Status"] != null ? ViewState["Status"].ToString() : string.Empty;
            }
            set
            {
                ViewState["Status"] = value;
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            ListViewDataBind();
        }

        private void ListViewDataBind()
        {
            var id = Page.Request.Params["id"];
            var dm = new DataManager();
            lvAllUserProfile.DataSource = dm.QueryWithReturnDataSet(string.Format("SELECT * FROM usersprofiles WHERE userid = {0} order by FirstName", UserId));
            lvAllUserProfile.DataBind();
        }
    }
}