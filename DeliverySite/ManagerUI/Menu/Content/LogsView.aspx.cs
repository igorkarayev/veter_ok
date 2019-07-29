using Delivery.BLL.Helpers;
using Delivery.BLL.StaticMethods;
using Delivery.DAL;
using Delivery.DAL.DataBaseObjects;
using DeliverySite.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace Delivery.ManagerUI.Menu.Content
{
    public partial class LogsView : ManagerBasePage
    {

        protected void Page_Init(object sender, EventArgs e)
        {
            btnReload.Click += btnReload_Click;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            OtherMethods.ActiveRightMenuStyleChanche("hlLogs", this.Page);
            OtherMethods.ActiveRightMenuStyleChanche("hlContent", this.Page);
            Page.Title = PagesTitles.ManagerLogsView + BackendHelper.TagToValue("page_title_part");

            #region Блок доступа к странице
            var userInSession = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());
            if (currentRole.PageLogsView != 1)
            {
                Response.Redirect("~/Error.aspx?id=1");
            }
            #endregion

            if (!IsPostBack)
            {
                stbChangeDate1.Text = DateTime.Now.ToString("dd-MM-yyyy");
                var dm = new DataManager();
                var dataSet1 = dm.QueryWithReturnDataSet("SELECT DISTINCT UserID FROM userslog ORDER BY UserID ASC");
                sddUsers.DataSource = dataSet1;
                sddUsers.DataTextField = "UserID";
                sddUsers.DataValueField = "UserID";
                sddUsers.DataBind();
                foreach (ListItem items in sddUsers.Items)
                {
                    var user = new Users() { ID = Convert.ToInt32(items.Text) };
                    user.GetById();
                    items.Text = String.Format("[{2}] {0} {1}", user.Family, user.Name, items.Text);
                }
                sddUsers.Items.Insert(0, new ListItem("Все", string.Empty));

                var dataSet2 = dm.QueryWithReturnDataSet("SELECT DISTINCT PageName FROM userslog ORDER BY PageName ASC");
                sddPages.DataSource = dataSet2;
                sddPages.DataTextField = "PageName";
                sddPages.DataValueField = "PageName";
                sddPages.DataBind();
                foreach (ListItem items in sddPages.Items)
                {
                    items.Text = LogsMethods.PageNameToRuss(items.Text, null);
                }
                sddPages.Items.Insert(0, new ListItem("Все", string.Empty));

                var dataSet3 = dm.QueryWithReturnDataSet("SELECT DISTINCT Method FROM userslog ORDER BY Method ASC");
                sddActions.DataSource = dataSet3;
                sddActions.DataTextField = "Method";
                sddActions.DataValueField = "Method";
                sddActions.DataBind();
                foreach (ListItem items in sddActions.Items)
                {
                    items.Text = LogsMethods.MethodNameToRuss(items.Text);
                }
                sddActions.Items.Insert(0, new ListItem("Все", string.Empty));

                var dataSet4 = dm.QueryWithReturnDataSet("SELECT DISTINCT TableName FROM userslog ORDER BY TableName ASC");
                sddTable.DataSource = dataSet4;
                sddTable.DataTextField = "TableName";
                sddTable.DataValueField = "TableName";
                sddTable.DataBind();
                foreach (ListItem items in sddTable.Items)
                {
                    items.Text = LogsMethods.TableNameToRuss(items.Text);
                }
                sddTable.Items.Insert(0, new ListItem("Все", string.Empty));

                var dataSet5 = dm.QueryWithReturnDataSet("SELECT DISTINCT PropertyName, TableName FROM userslog ORDER BY TableName ASC, PropertyName ASC");
                dataSet5.Tables[0].Columns.Add("TableFieldName", typeof(string), "TableName+'.'+PropertyName");
                sddField.DataSource = dataSet5;
                sddField.DataTextField = "TableFieldName";
                sddField.DataValueField = "PropertyName";
                sddField.DataBind();
                foreach (ListItem items in sddField.Items)
                {
                    var itemArray = items.Text.Split('.');
                    var tableName = itemArray[0];
                    var fieldName = itemArray.Length > 1 ? itemArray[1] : string.Empty;
                    items.Text = String.Format("{0}: {1}", LogsMethods.TableNameToRuss(tableName), LogsMethods.PropertyNameToRuss(tableName, fieldName));
                }
                sddField.Items.Insert(0, new ListItem("Все", string.Empty));

            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            ListViewDataBind();
        }

        protected void ListViewDataBind()
        {
            var dm = new DataManager();
            var ds = dm.QueryWithReturnDataSet(GetSearchString());
            lvAllErrors.DataSource = ds;

            #region Редирект на первую страницу при поиске
            if (lvAllErrors.Items.Count == 0 && lvDataPager.TotalRowCount != 0)
            {
                lvDataPager.SetPageProperties(0, lvDataPager.PageSize, false);
                lvAllErrors.DataBind();
            }
            #endregion

            lblAllResult.Text = ds.Tables[0].Rows.Count.ToString();

            if (ds.Tables[0].Rows.Count == 0)
            {
                lvDataPager.Visible = false;
                lblPage.Visible = false;
            }
            else
            {
                lvDataPager.Visible = true;
                lblPage.Visible = true;
            }

            lvAllErrors.DataBind();
        }

        protected void btnReload_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.ServerVariables["URL"]);
        }

        #region Methods
        public String GetSearchString()
        {
            var searchString = String.Empty;
            var searchTIDString = String.Empty;
            var searchIDString = String.Empty;
            var searchUserIDString = String.Empty;
            var searchPageNameString = String.Empty;
            var searchMethodString = String.Empty;
            var searchTableNameString = String.Empty;
            var searchFieldNameString = String.Empty;
            var searchDateString = String.Empty;
            var searchParametres = new Dictionary<String, String>();

            //формируем строку поика для TID
            if (!String.IsNullOrEmpty(stbTID.Text))
            {
                searchTIDString = searchTIDString + "`TicketFullSecureID` like '" + stbTID.Text + "%'";
            }

            //формируем строку поика для ID
            if (!String.IsNullOrEmpty(stbID.Text))
            {
                searchIDString = searchIDString + "`FieldID` = '" + stbID.Text + "'";
            }

            //формируем строку поика для id пользователя
            if (sddUsers.SelectedValue != "0" && !String.IsNullOrEmpty(sddUsers.SelectedValue))
            {
                searchUserIDString = searchUserIDString + "`UserID` = '" + sddUsers.SelectedValue + "'";
            }

            //формируем строку поика для имнеи страницы
            if (sddPages.SelectedValue != "0" && !String.IsNullOrEmpty(sddPages.SelectedValue))
            {
                searchPageNameString = searchPageNameString + "`PageName` = '" + sddPages.SelectedValue + "'";
            }

            //формируем строку поика для имнеи таблицы
            if (sddTable.SelectedValue != "0" && !String.IsNullOrEmpty(sddTable.SelectedValue))
            {
                searchTableNameString = searchTableNameString + "`TableName` = '" + sddTable.SelectedValue + "'";
            }

            //формируем строку поика для действию
            if (sddActions.SelectedValue != "0" && !String.IsNullOrEmpty(sddActions.SelectedValue))
            {
                searchMethodString = searchMethodString + "`Method` = '" + sddActions.SelectedValue + "'";
            }

            //формируем строку поика для параметра
            if (sddField.SelectedValue != "0" && !String.IsNullOrEmpty(sddField.SelectedValue))
            {
                searchFieldNameString = searchFieldNameString + "`PropertyName` = '" + sddField.SelectedValue + "'";
            }

            //формируем cтроку для поиска по Date
            if (!string.IsNullOrEmpty(stbChangeDate1.Text) && !string.IsNullOrEmpty(stbChangeDate2.Text))
            {
                searchDateString = "(DateTime BETWEEN '" +
                                   Convert.ToDateTime(stbChangeDate1.Text).ToString("yyyy-MM-dd") + "' AND '" +
                                   Convert.ToDateTime(stbChangeDate2.Text).ToString("yyyy-MM-dd") + "')";
            }

            if (!string.IsNullOrEmpty(stbChangeDate1.Text) && string.IsNullOrEmpty(stbChangeDate2.Text))
            {
                searchDateString = "(DateTime BETWEEN '" +
                                Convert.ToDateTime(stbChangeDate1.Text).ToString("yyyy-MM-dd") + "' AND '" +
                                Convert.ToDateTime(stbChangeDate1.Text).AddYears(1).ToString("yyyy-MM-dd") + "')";
            }

            if (string.IsNullOrEmpty(stbChangeDate1.Text) && !string.IsNullOrEmpty(stbChangeDate2.Text))
            {
                searchDateString = "(DateTime BETWEEN '" +
                                Convert.ToDateTime(stbChangeDate2.Text).AddYears(-2).ToString("yyyy-MM-dd") + "' AND '" +
                                Convert.ToDateTime(stbChangeDate2.Text).ToString("yyyy-MM-dd") + "')";
            }
            //формируем конечный запро для поиска
            searchParametres.Add("TicketFullSecureID", searchTIDString);
            searchParametres.Add("FieldID", searchIDString);
            searchParametres.Add("UserID", searchUserIDString);
            searchParametres.Add("PageName", searchPageNameString);
            searchParametres.Add("TableName", searchTableNameString);
            searchParametres.Add("Method", searchMethodString);
            searchParametres.Add("PropertyName", searchFieldNameString);
            searchParametres.Add("DateTime", searchDateString);

            foreach (var searchParametre in searchParametres)
            {
                if (!string.IsNullOrEmpty(searchParametre.Value))
                {
                    searchString = searchString + searchParametre.Value + " AND ";
                }
            }

            searchString = searchString.Length < 4 ? "FROM userslog order by ID DESC" : String.Format("FROM userslog WHERE {0} order by ID DESC", searchString.Remove(searchString.Length - 4));

            var searchStringOver = "SELECT * " + searchString;

            return searchStringOver;
        }
        #endregion
    }
}