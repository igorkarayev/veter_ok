using Delivery.BLL.StaticMethods;
using Delivery.DAL.DataBaseObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Delivery.ManagerUI.Controls.Clients
{
    public partial class Comment : System.Web.UI.UserControl
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

        public string CommentValue
        {
            get
            {
                return ViewState["CommentValue"] != null ? ViewState["CommentValue"].ToString() : String.Empty;
            }
            set
            {
                ViewState["CommentValue"] = value;
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
            lblCommentHistory.Text = WebUtility.HtmlDecode(CommentValue);

            var userInSession = (Users)Session["userinsession"];

            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());
            if (currentRole.ActionControlComment != 1)
            {
                tbComment.Enabled = false;
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