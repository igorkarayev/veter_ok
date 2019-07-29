using Delivery.BLL.Helpers;
using Delivery.BLL.StaticMethods;
using Delivery.DAL;
using Delivery.DAL.DataBaseObjects;
using DeliverySite.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace Delivery.ManagerUI.Menu.Souls
{
    public partial class UserToCategoryView : ManagerBasePage
    {
        protected string ButtonText { get; set; }

        public string ActionText { get; set; }

        protected void Page_Init(object sender, EventArgs e)
        {
            btnAdd.Click += btnAdd_Click;
            DataBind();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = PagesTitles.ManagerUserToSection + BackendHelper.TagToValue("page_title_part");
            OtherMethods.ActiveRightMenuStyleChanche("hlClients", this.Page);
            OtherMethods.ActiveRightMenuStyleChanche("hlSouls", this.Page);

            var userInSession = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());
            if (currentRole.ActionCategoryAssignToUser != 1)
            {
                Response.Redirect("~/Error.aspx?id=1");
            }

            var id = Page.Request.Params["id"];
            var dm = new DataManager();
            var usersToSessions = dm.QueryWithReturnDataSet("SELECT * FROM `userstocategory` WHERE `UserID` = " + id);
            if (usersToSessions.Tables[0].Rows.Count == 0)
                lblAllAvaliable.Visible = true;
            lvAllTracks.DataSource = usersToSessions;
            lvAllTracks.DataBind();

            if (!IsPostBack)
            {
                var sections = new Category();
                var ds = sections.GetAllItems("Name", "ASC", null);
                ddlSections.DataSource = ds;
                ddlSections.DataTextField = "Name";
                ddlSections.DataValueField = "ID";
                ddlSections.DataBind();
            }
        }

        public void lbDelete_Click(Object sender, EventArgs e)
        {
            var lb = (LinkButton)sender;
            var sectionId = lb.CommandArgument;
            var userId = Page.Request.Params["id"];
            var dm = new DataManager();
            dm.QueryWithoutReturnData(null, String.Format("DELETE FROM `userstocategory` WHERE `CategoryID` = {0} AND `UserID` = {1}", sectionId, userId));
            Page.Response.Redirect("~/ManagerUI/Menu/Souls/UserToCategoryView.aspx?id=" + userId);
        }

        public void btnAdd_Click(Object sender, EventArgs e)
        {
            var id = Page.Request.Params["id"];
            var dm = new DataManager();
            dm.QueryWithoutReturnData(null, String.Format("INSERT IGNORE INTO `userstocategory` (`UserID`, `CategoryID`) VALUES ('{0}', '{1}');", id, ddlSections.SelectedValue));
            Page.Response.Redirect("~/ManagerUI/Menu/Souls/UserToCategoryView.aspx?id=" + id);
        }
    }
}