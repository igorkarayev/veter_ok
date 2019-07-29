using Delivery.BLL.Helpers;
using Delivery.BLL.StaticMethods;
using Delivery.DAL;
using Delivery.DAL.DataBaseObjects;
using DeliverySite.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace Delivery.ManagerUI.Menu.Settings
{
    public partial class UsersDiscountView : ManagerBasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            btnReload.Click += btnReload_Click;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = PagesTitles.ManagerUsersDiscount + BackendHelper.TagToValue("page_title_part");
            OtherMethods.ActiveRightMenuStyleChanche("hlUsersDiscount", this.Page);
            OtherMethods.ActiveRightMenuStyleChanche("hlSettings", this.Page);

            #region Блок доступа к странице
            var userInSession = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());
            if (currentRole.PageUserDiscountView != 1)
            {
                Response.Redirect("~/Error.aspx?id=1");
            }
            #endregion

            if (!IsPostBack)
            {
                sddlStatus.DataSource = Users.UserStatuses;
                sddlStatus.DataTextField = "Value";
                sddlStatus.DataValueField = "Key";
                sddlStatus.DataBind();
                sddlStatus.Items.Insert(0, new ListItem("Все", string.Empty));

                sddlCourse.Items.Add(new ListItem("Да", "1"));
                sddlCourse.Items.Add(new ListItem("Нет", "0"));
                sddlCourse.Items.Insert(0, new ListItem("Все", string.Empty));

                if (Page.Request.Params["uid"] != null)
                {
                    stbUID.Text = Page.Request.Params["uid"];
                }
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            ListViewDataBind();
        }

        protected void btnReload_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }

        #region Methods

        private void ListViewDataBind()
        {
            var dm = new DataManager();
            lvAllUsers.DataSource = dm.QueryWithReturnDataSet(GetSearchString());
            lvAllUsers.DataBind();
            if (lvAllUsers.Items.Count == 0)
            {
                lblPage.Visible = false;
            }
        }

        public String GetSearchString()
        {
            var searchString = String.Empty;
            var seletedCourseString = String.Empty;
            var seletedStatusString = String.Empty;
            var searchUserIdString = String.Empty;
            var searchRecipientPhoneString = String.Empty;
            var searchEmailString = String.Empty;
            var searchFamilyString = String.Empty;
            var searchParametres = new Dictionary<String, String>();

            //формируем cтроку для поиска по UserID
            if (!string.IsNullOrEmpty(stbUID.Text))
            {
                searchUserIdString = "`ID` = '" + stbUID.Text + "'";
            }

            //формируем cтроку для поиска по EmailID
            if (!string.IsNullOrEmpty(stbEmail.Text))
            {
                searchEmailString = "`Email` LIKE '%" + stbEmail.Text + "%'";
            }

            //формируем cтроку для поиска по RecipientPhone
            if (!string.IsNullOrEmpty(stbRecipientPhone.Text))
            {
                searchRecipientPhoneString = "`Phone` = '" + stbRecipientPhone.Text + "'";
            }

            //формируем cтроку для поиска по Family
            if (!string.IsNullOrEmpty(stbFamily.Text))
            {
                searchFamilyString = "`Family` LIKE '%" + stbFamily.Text + "%'";
            }

            //формируем cтроку для поиска по Course
            if (!string.IsNullOrEmpty(sddlCourse.SelectedValue))
            {
                seletedCourseString = "`IsCourse` = '" + sddlCourse.SelectedValue + "'";
            }

            //формируем cтроку для поиска по Status
            if (!string.IsNullOrEmpty(sddlStatus.SelectedValue))
            {
                seletedStatusString = "`Status` = '" + sddlStatus.SelectedValue + "'";
            }


            //формируем конечный запро для поиска
            searchParametres.Add("ID", searchUserIdString);
            searchParametres.Add("Email", searchEmailString);
            searchParametres.Add("Phone", searchRecipientPhoneString);
            searchParametres.Add("Family", searchFamilyString);
            searchParametres.Add("Status", seletedStatusString);
            searchParametres.Add("IsCourse", seletedCourseString);

            foreach (var searchParametre in searchParametres)
            {
                if (!string.IsNullOrEmpty(searchParametre.Value))
                {
                    searchString = searchString + searchParametre.Value + " AND ";
                }
            }

            //формируем строку поика

            searchString = searchString.Length < 4 ? "SELECT * FROM users WHERE `Role` = 'User'  order by ID DESC" : String.Format("SELECT * FROM users WHERE {0} AND `Role` = 'User' order by ID DESC ", searchString.Remove(searchString.Length - 4));

            return searchString;
        }
        #endregion
    }
}