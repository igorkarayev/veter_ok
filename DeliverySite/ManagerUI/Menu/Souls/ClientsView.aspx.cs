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
    public partial class ClientsView : ManagerBasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            btnReload.Click += btnReload_Click;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = PagesTitles.ManagerClientViewTitle + BackendHelper.TagToValue("page_title_part");
            OtherMethods.ActiveRightMenuStyleChanche("hlSouls", this.Page);
            OtherMethods.ActiveRightMenuStyleChanche("hlClients", this.Page);
            PageAccess();

            #region Блок доступа к странице
            var userInSession = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());
            if (currentRole.PageClientsView != 1)
            {
                Response.Redirect("~/Error.aspx?id=1");
            }
            #endregion

            if (!IsPostBack)
            {
                stbDate2.Text = DateTime.Now.ToString("dd-MM-yyyy");
                stbDate1.Text = DateTime.Now.AddMonths(-1).ToString("dd-MM-yyyy");

                var dm = new DataManager();
                var dataSet = dm.QueryWithReturnDataSet("SELECT * FROM `users` WHERE (`Role` = 'SalesManager') and Status = 2 ORDER BY Family ASC, Name ASC;");
                dataSet.Tables[0].Columns.Add("FIO", typeof(string), "Family + ' ' + Name");
                sddlSalesManager.DataSource = dataSet;
                sddlSalesManager.DataTextField = "FIO";
                sddlSalesManager.DataValueField = "ID";
                sddlSalesManager.DataBind();
                sddlSalesManager.Items.Insert(0, new ListItem("Все", string.Empty));

                sddlStatus.DataSource = Users.UserStatuses;
                sddlStatus.DataTextField = "Value";
                sddlStatus.DataValueField = "Key";
                sddlStatus.DataBind();
                sddlStatus.Items.Insert(0, new ListItem("Все", string.Empty));

                sddlStatusStudy.DataSource = Users.UserStatusesStudy;
                sddlStatusStudy.DataTextField = "Value";
                sddlStatusStudy.DataValueField = "Key";
                sddlStatusStudy.DataBind();
                sddlStatusStudy.Items.Insert(0, new ListItem("Все", string.Empty));
            }

            hfDeliveryDate1.Value = stbDate1.Text;
            hfDeliveryDate2.Value = stbDate2.Text;
        }

        //этот метод перед самой отрисовкой страницы биндит все данные
        protected void Page_PreRender(object sender, EventArgs e)
        {
            ListViewDataBind();
        }

        protected void btnReload_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }

        protected void lvAllClients_OnSortingButtonClick(object sender, EventArgs e)
        {
            var commandArgument = ((LinkButton)sender).CommandArgument;

            string sortDirection = "ASC";

            if (String.IsNullOrEmpty(hfSortDirection.Value))
            {
                sortDirection = "ASC";
                hfSortDirection.Value = "1";
            }
            else
            {
                if (hfSortDirection.Value == "0")
                {
                    sortDirection = "ASC";
                    hfSortDirection.Value = "1";
                }
                else
                {
                    sortDirection = "DESC";
                    hfSortDirection.Value = "0";
                }
            }
            hfSortExpression.Value = "ORDER BY " + commandArgument + " " + sortDirection;

        }

        #region Настройки доступа к странице и действиям
        protected void PageAccess()
        {
            var userInSession = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());
            if (currentRole.PageClientsView != 1)
            {
                Response.Redirect("~/Error.aspx?id=1");
            }
        }
        #endregion

        #region Methods

        private void ListViewDataBind()
        {
            var dm = new DataManager();
            var ds = dm.QueryWithReturnDataSet(GetSearchString());
            lvAllClients.DataSource = ds;
            lblAllResult.Text = ds.Tables[0].Rows.Count.ToString();
            lvAllClients.DataBind();

            var userInSession = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());
            foreach (ListViewDataItem item in lvAllClients.Items)
            {
                var tdStatInfoSegment = item.FindControl("tdStatInfoSegment");
                var tdStatInfoCount = item.FindControl("tdStatInfoCount");
                var tdStatInfoAverage = item.FindControl("tdStatInfoAverage");
                var tdStatInfoSumm = item.FindControl("tdStatInfoSumm");
                if (currentRole.ViewClientStatInfo != 1)
                {
                    tdStatInfoSegment.Visible = false;
                    tdStatInfoCount.Visible = false;
                    tdStatInfoAverage.Visible = false;
                    tdStatInfoSumm.Visible = false;
                }
            }

            var thStatInfoSegment = lvAllClients.FindControl("thStatInfoSegment");
            var thStatInfoCount = lvAllClients.FindControl("thStatInfoCount");
            var thStatInfoAverage = lvAllClients.FindControl("thStatInfoAverage");
            var thStatInfoSumm = lvAllClients.FindControl("thStatInfoSumm");
            if (currentRole.ViewClientStatInfo != 1)
            {
                if (thStatInfoSegment != null)
                {
                    thStatInfoSegment.Visible = false;
                }
                if (thStatInfoCount != null)
                {
                    thStatInfoCount.Visible = false;
                }
                if (thStatInfoAverage != null)
                {
                    thStatInfoAverage.Visible = false;
                }
                if (thStatInfoSumm != null)
                {
                    thStatInfoSumm.Visible = false;
                }
                spnlStaticData.Visible = false;
                lblStaticData.Visible = false;
            }



            #region Редирект на первую страницу при поиске
            if (lvAllClients.Items.Count == 0 && lvDataPager.TotalRowCount != 0)
            {
                lvDataPager.SetPageProperties(0, lvDataPager.PageSize, false);
                lvAllClients.DataBind();
            }
            #endregion

            if (lvAllClients.Items.Count == 0)
            {
                lblPage.Visible = false;
            }
        }

        public string GetSearchString()
        {
            var sortExpression = "ORDER BY U.`ID` DESC";
            if (!String.IsNullOrEmpty(hfSortExpression.Value))
            {
                sortExpression = hfSortExpression.Value;
            }

            var searchString = string.Empty;
            var seletedSalesManagerId = string.Empty;
            var seletedStatusString = string.Empty;
            var searchUserIdString = string.Empty;
            var searchPhoneString = string.Empty;
            var searchSkypeString = string.Empty;
            var searchEmailString = string.Empty;
            var searchFamilyString = string.Empty;
            var searchContactDate = string.Empty;
            var searchCompanyName = string.Empty;
            var seletedStatusStudyString = string.Empty;
            var searchParametres = new Dictionary<string, string>();
            //формируем cтроку для поиска по ManagerID
            if (!string.IsNullOrEmpty(sddlSalesManager.SelectedValue))
            {
                seletedSalesManagerId = "U.`SalesManagerID` = '" + sddlSalesManager.SelectedValue + "'";
            }

            //формируем cтроку для поиска по ManagerID
            if (!string.IsNullOrEmpty(stbContactDate.Text))
            {
                searchContactDate = string.Format("U.`ContactDate` = '{0}'",
                                   Convert.ToDateTime(stbContactDate.Text).ToString("yyyy-MM-dd"),
                                   Convert.ToDateTime(stbContactDate.Text).AddDays(1).ToString("yyyy-MM-dd"));
            }

            //формируем cтроку для поиска по UserID
            if (!string.IsNullOrEmpty(stbUID.Text))
            {
                searchUserIdString = "U.`ID` = '" + stbUID.Text.Trim() + "'";
            }

            //формируем cтроку для поиска по EmailID
            if (!string.IsNullOrEmpty(stbEmail.Text))
            {
                searchEmailString = "U.`Email` LIKE '%" + stbEmail.Text.Trim() + "%'";
            }

            //формируем cтроку для поиска по RecipientPhone
            if (!string.IsNullOrEmpty(stbRecipientPhone.Text))
            {
                searchPhoneString = string.Format("(U.`Phone` = '{0}' OR P.`ContactPhoneNumbers` LIKE '%{0}%' OR P.`DirectorPhoneNumber` = '{0}')", stbRecipientPhone.Text.Trim());
            }

            //формируем cтроку для поиска по CompanyName
            if (!string.IsNullOrEmpty(stbCompanyName.Text))
            {
                searchCompanyName = string.Format("P.`CompanyName` LIKE '%{0}%'", stbCompanyName.Text.Trim());
            }

            //формируем cтроку для поиска по Family
            if (!string.IsNullOrEmpty(stbFamily.Text))
            {
                searchFamilyString = "U.`Family` LIKE '%" + stbFamily.Text.Trim() + "%'";
            }

            //формируем cтроку для поиска по Family
            if (!string.IsNullOrEmpty(stbSkype.Text))
            {
                searchSkypeString = "U.`Skype` LIKE '%" + stbSkype.Text.Trim() + "%'";
            }

            //формируем cтроку для поиска по Status
            if (!string.IsNullOrEmpty(sddlStatus.SelectedValue))
            {
                seletedStatusString = "U.`Status` = '" + sddlStatus.SelectedValue + "'";
            }

            //формируем cтроку для поиска по StatusStady
            if (!string.IsNullOrEmpty(sddlStatusStudy.SelectedValue))
            {
                seletedStatusStudyString = "U.`StatusStady` = '" + sddlStatusStudy.SelectedValue + "'";
            }


            //формируем конечный запро для поиска
            searchParametres.Add("ID", searchUserIdString);
            searchParametres.Add("Email", searchEmailString);
            searchParametres.Add("Skype", searchSkypeString);
            searchParametres.Add("CompanyName", searchCompanyName);
            searchParametres.Add("Phone", searchPhoneString);
            searchParametres.Add("Family", searchFamilyString);
            searchParametres.Add("Status", seletedStatusString);
            searchParametres.Add("StatusStady", seletedStatusStudyString);
            searchParametres.Add("SalesManagerID", seletedSalesManagerId);
            searchParametres.Add("ContactDate", searchContactDate);

            foreach (var searchParametre in searchParametres)
            {
                if (!string.IsNullOrEmpty(searchParametre.Value))
                {
                    searchString = searchString + searchParametre.Value + " AND ";
                }
            }

            //формируем выборку по CreateDate
            var searchDateString = string.Empty;
            if (!string.IsNullOrEmpty(stbDate1.Text) && !string.IsNullOrEmpty(stbDate2.Text))
            {
                searchDateString = string.Format("AND (T.DeliveryDate BETWEEN '{0}' AND '{1}')",
                                    Convert.ToDateTime(stbDate1.Text).ToString("yyyy-MM-dd"),
                                    Convert.ToDateTime(stbDate2.Text).ToString("yyyy-MM-dd"));
            }
            else
            {
                if (!string.IsNullOrEmpty(stbDate1.Text))
                {
                    searchDateString = string.Format("AND (T.DeliveryDate BETWEEN '{0}' AND '{1}')",
                                    Convert.ToDateTime(stbDate1.Text).ToString("yyyy-MM-dd"),
                                    Convert.ToDateTime(stbDate1.Text).AddDays(1).ToString("yyyy-MM-dd"));
                }
            }

            searchString = searchString.Length < 4 ?
                string.Format("SELECT DISTINCT " +
                                    "(SELECT COUNT(*)                             " +
                                        "FROM Tickets T JOIN Users UU ON T.`UserID` = UU.`ID` WHERE (UU.`ID` = U.ID) {1} ) as TicketsCount, " +
                                    "(SELECT SUM(T.`GruzobozCost`)                " +
                                        "FROM Tickets T JOIN Users UU ON T.`UserID` = UU.`ID` WHERE (UU.`ID` = U.ID) {1} ) as SummGruzobozCost, " +
                                    "(SELECT SUM(T.`GruzobozCost`) / COUNT(*)     " +
                                        "FROM Tickets T JOIN Users UU ON T.`UserID` = UU.`ID` WHERE (UU.`ID` = U.ID) {1} ) as AverageCost, " +
                                    "U.* " +
                                "FROM users U LEFT JOIN usersprofiles P ON U.`ID` = P.`UserID` WHERE U.`Role` = 'User' {0}",
                              sortExpression,
                              searchDateString) :
                string.Format("SELECT DISTINCT " +
                                    "(SELECT COUNT(*)                             " +
                                        "FROM Tickets T JOIN Users UU ON T.`UserID` = UU.`ID` WHERE (UU.`ID` = U.ID) {2} ) as TicketsCount, " +
                                    "(SELECT SUM(T.`GruzobozCost`)                " +
                                        "FROM Tickets T JOIN Users UU ON T.`UserID` = UU.`ID` WHERE (UU.`ID` = U.ID) {2} ) as SummGruzobozCost, " +
                                    "(SELECT SUM(T.`GruzobozCost`) / COUNT(*)     " +
                                        "FROM Tickets T JOIN Users UU ON T.`UserID` = UU.`ID` WHERE (UU.`ID` = U.ID) {2} ) as AverageCost, " +
                                    "U.* " +
                                "FROM users U LEFT JOIN usersprofiles P ON  U.`ID` = P.`UserID` WHERE {0} AND U.`Role` = 'User' {1}",
                              searchString.Remove(searchString.Length - 4),
                              sortExpression,
                              searchDateString);

            return searchString;
        }
        #endregion
    }
}