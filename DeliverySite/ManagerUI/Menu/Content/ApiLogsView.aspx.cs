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
    public partial class ApiLogsView : ManagerBasePage
    {

        protected void Page_Init(object sender, EventArgs e)
        {
            btnReload.Click += btnReload_Click;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            OtherMethods.ActiveRightMenuStyleChanche("hlApiLogs", this.Page);
            OtherMethods.ActiveRightMenuStyleChanche("hlContent", this.Page);
            Page.Title = PagesTitles.ManagerApiLogsView + BackendHelper.TagToValue("page_title_part");

            #region Блок доступа к странице
            var userInSession = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());
            if (currentRole.PageApiLogView != 1)
            {
                Response.Redirect("~/Error.aspx?id=1");
            }
            #endregion

            var dm = new DataManager();
            if (!IsPostBack)
            {
                stbCreateDate1.Text = DateTime.Now.ToString("dd-MM-yyyy");
                var dataSet1 = dm.QueryWithReturnDataSet("SELECT DISTINCT ApiType FROM apilog ORDER BY UserID ASC");
                sddApiType.DataSource = dataSet1;
                sddApiType.DataTextField = "ApiType";
                sddApiType.DataValueField = "ApiType";
                sddApiType.DataBind();
                sddApiType.Items.Insert(0, new ListItem("Все", string.Empty));

                var dataSet2 = dm.QueryWithReturnDataSet("SELECT DISTINCT ApiName FROM apilog ORDER BY UserID ASC");
                sddApiName.DataSource = dataSet2;
                sddApiName.DataTextField = "ApiName";
                sddApiName.DataValueField = "ApiName";
                sddApiName.DataBind();
                sddApiName.Items.Insert(0, new ListItem("Все", string.Empty));

                var dataSet3 = dm.QueryWithReturnDataSet("SELECT DISTINCT MethodName FROM apilog ORDER BY UserID ASC");
                sddMethodName.DataSource = dataSet3;
                sddMethodName.DataTextField = "MethodName";
                sddMethodName.DataValueField = "MethodName";
                sddMethodName.DataBind();
                sddMethodName.Items.Insert(0, new ListItem("Все", string.Empty));
            }

            var whereBetween = String.Empty;
            if (!string.IsNullOrEmpty(stbCreateDate1.Text) && !string.IsNullOrEmpty(stbCreateDate2.Text))
            {
                whereBetween = "WHERE (CreateDate BETWEEN '" +
                                   Convert.ToDateTime(stbCreateDate1.Text).ToString("yyyy-MM-dd") + "' AND '" +
                                   Convert.ToDateTime(stbCreateDate2.Text).ToString("yyyy-MM-dd") + "')";
            }

            if (!string.IsNullOrEmpty(stbCreateDate1.Text) && string.IsNullOrEmpty(stbCreateDate2.Text))
            {
                whereBetween = "WHERE (CreateDate BETWEEN '" +
                                Convert.ToDateTime(stbCreateDate1.Text).ToString("yyyy-MM-dd") + "' AND '" +
                                Convert.ToDateTime(stbCreateDate1.Text).AddYears(1).ToString("yyyy-MM-dd") + "')";
            }

            if (string.IsNullOrEmpty(stbCreateDate1.Text) && !string.IsNullOrEmpty(stbCreateDate2.Text))
            {
                whereBetween = "WHERE (CreateDate BETWEEN '" +
                                Convert.ToDateTime(stbCreateDate2.Text).AddYears(-2).ToString("yyyy-MM-dd") + "' AND '" +
                                Convert.ToDateTime(stbCreateDate2.Text).ToString("yyyy-MM-dd") + "')";
            }

            var dataSetForStatInfo = dm.QueryWithReturnDataSet("" +
                                                            "SELECT COUNT(DISTINCT `UserID`) FROM `apilog` " + whereBetween + ";" +
                                                            "SELECT SUM(`ResponseBodyLenght`) FROM `apilog` " + whereBetween + ";" +
                                                            "SELECT COUNT(DISTINCT `ApiKey`) FROM `apilog` " + whereBetween + ";" +
                                                            "SELECT COUNT(*) FROM `apilog` " + whereBetween + ";");
            var dataSetForTopQuery = dm.QueryWithReturnDataSet("SELECT UserID, COUNT(*) as NumberOfQuery FROM `apilog` " + whereBetween + " GROUP BY UserID ORDER BY NumberOfQuery DESC LIMIT 4;");
            var dataSetForTopInfo = dm.QueryWithReturnDataSet("SELECT UserID, SUM(`ResponseBodyLenght`) as InfoCount FROM `apilog` " + whereBetween + " GROUP BY UserID ORDER BY InfoCount DESC LIMIT 4;");
            lblFirstByInfo.Text = lblFirstByQuery.Text = lblSecondByInfo.Text = lblSecondByQuery.Text =
                lblThirdByInfo.Text = lblThirdByQuery.Text = lblFourthByInfo.Text = lblFourthByQuery.Text = String.Empty;

            lblUsersCount.Text =
                MoneyMethods.MoneySeparator(dataSetForStatInfo.Tables[0].Rows[0][0]
                    .ToString());
            var responceBobyLenghtSummInBitString = dataSetForStatInfo.Tables[1].Rows[0][0]
                    .ToString();
            decimal responceBobyLenghtSummInBit = 0;
            if (!String.IsNullOrEmpty(responceBobyLenghtSummInBitString))
                responceBobyLenghtSummInBit = Convert.ToDecimal(responceBobyLenghtSummInBitString);
            lblInfoCount.Text = String.Format("{1} Mb / {0} Kb",
                MoneyMethods.MoneySeparator(responceBobyLenghtSummInBit),
                MoneyMethods.MoneySeparator(Convert.ToDecimal(responceBobyLenghtSummInBit) / 1048576));
            lblApiKeysCount.Text = MoneyMethods.MoneySeparator(dataSetForStatInfo.Tables[2].Rows[0][0].ToString());
            lblQueriesCount.Text = MoneyMethods.MoneySeparator(dataSetForStatInfo.Tables[3].Rows[0][0].ToString());
            var currentAppAddress = BackendHelper.TagToValue("current_app_address");
            if (dataSetForTopQuery.Tables[0].Rows.Count >= 1)
            {
                lblFirstByQuery.Text = String.Format("<a href='http://{2}/ManagerUI/Menu/Souls/ClientEdit.aspx?id={3}'>{0}</a>: {1}",
                    UsersHelper.UserIDToFullName(dataSetForTopQuery.Tables[0].Rows[0][0].ToString()),
                    dataSetForTopQuery.Tables[0].Rows[0][1],
                    currentAppAddress,
                    dataSetForTopQuery.Tables[0].Rows[0][0]);
            }
            if (dataSetForTopQuery.Tables[0].Rows.Count >= 2)
            {
                lblSecondByQuery.Text = String.Format("<a href='http://{2}/ManagerUI/Menu/Souls/ClientEdit.aspx?id={3}'>{0}</a>: {1}",
                    UsersHelper.UserIDToFullName(dataSetForTopQuery.Tables[0].Rows[1][0].ToString()),
                    dataSetForTopQuery.Tables[0].Rows[1][1],
                    currentAppAddress,
                    dataSetForTopQuery.Tables[0].Rows[1][0]);
            }
            if (dataSetForTopQuery.Tables[0].Rows.Count >= 3)
            {
                lblThirdByQuery.Text = String.Format("<a href='http://{2}/ManagerUI/Menu/Souls/ClientEdit.aspx?id={3}'>{0}</a>: {1}",
                    UsersHelper.UserIDToFullName(dataSetForTopQuery.Tables[0].Rows[2][0].ToString()),
                    dataSetForTopQuery.Tables[0].Rows[2][1],
                    currentAppAddress,
                    dataSetForTopQuery.Tables[0].Rows[2][0]);
            }
            if (dataSetForTopQuery.Tables[0].Rows.Count >= 4)
            {
                lblFourthByQuery.Text = String.Format("<a href='http://{2}/ManagerUI/ClientEdit.aspx?id={3}'>{0}</a>: {1}",
                    UsersHelper.UserIDToFullName(dataSetForTopQuery.Tables[0].Rows[3][0].ToString()),
                    dataSetForTopQuery.Tables[0].Rows[3][1],
                    currentAppAddress,
                    dataSetForTopQuery.Tables[0].Rows[3][0]);
            }


            if (dataSetForTopInfo.Tables[0].Rows.Count >= 1)
            {
                lblFirstByInfo.Text = String.Format("<a href='http://{2}/ManagerUI/Menu/Souls/ClientEdit.aspx?id={3}'>{0}</a>: {1} Mb",
                    UsersHelper.UserIDToFullName(dataSetForTopInfo.Tables[0].Rows[0][0].ToString()),
                    MoneyMethods.MoneySeparator(Convert.ToDecimal(dataSetForTopInfo.Tables[0].Rows[0][1]) / 1048576),
                    currentAppAddress,
                    dataSetForTopInfo.Tables[0].Rows[0][0]);
            }
            if (dataSetForTopInfo.Tables[0].Rows.Count >= 2)
            {
                lblSecondByInfo.Text = String.Format("<a href='http://{2}/ManagerUI/Menu/Souls/ClientEdit.aspx?id={3}'>{0}</a>: {1} Mb",
                    UsersHelper.UserIDToFullName(dataSetForTopInfo.Tables[0].Rows[1][0].ToString()),
                    MoneyMethods.MoneySeparator(Convert.ToDecimal(dataSetForTopInfo.Tables[0].Rows[1][1]) / 1048576),
                    currentAppAddress,
                    dataSetForTopInfo.Tables[0].Rows[1][0]);
            }
            if (dataSetForTopInfo.Tables[0].Rows.Count >= 3)
            {
                lblThirdByInfo.Text = String.Format("<a href='http://{2}/ManagerUI/Menu/Souls/ClientEdit.aspx?id={3}'>{0}</a>: {1} Mb",
                    UsersHelper.UserIDToFullName(dataSetForTopInfo.Tables[0].Rows[2][0].ToString()),
                    MoneyMethods.MoneySeparator(Convert.ToDecimal(dataSetForTopInfo.Tables[0].Rows[2][1]) / 1048576),
                    currentAppAddress,
                    dataSetForTopInfo.Tables[0].Rows[2][0]);
            }
            if (dataSetForTopInfo.Tables[0].Rows.Count >= 4)
            {
                lblFourthByInfo.Text = String.Format("<a href='http://{2}/ManagerUI/ClientEdit.aspx?id={3}'>{0}</a>: {1} Mb",
                    UsersHelper.UserIDToFullName(dataSetForTopInfo.Tables[0].Rows[3][0].ToString()),
                    MoneyMethods.MoneySeparator(Convert.ToDecimal(dataSetForTopInfo.Tables[0].Rows[3][1]) / 1048576),
                    currentAppAddress,
                    dataSetForTopInfo.Tables[0].Rows[3][0]);
            }

            if (BackendHelper.TagToValue("allow_unauth_api_request") == "true")
            {
                divOpenApi.Visible = true;
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
            var searchIDString = String.Empty;
            var searchUIDString = String.Empty;
            var searchUIPString = String.Empty;
            var searchApiNameString = String.Empty;
            var searchMethodNameString = String.Empty;
            var searchApiTypeString = String.Empty;
            var searchApiKeyString = String.Empty;
            var searchCreateDateString = String.Empty;
            var searchParametres = new Dictionary<String, String>();

            //формируем строку поика для ID
            if (!String.IsNullOrEmpty(stbID.Text))
            {
                searchIDString = searchIDString + "`ID` = '" + stbID.Text + "'";
            }

            //формируем строку поика для id пользователя
            if (!String.IsNullOrEmpty(stbUID.Text))
            {
                searchUIDString = "`UserID` = '" + stbUID.Text + "'";
            }

            //формируем строку поика для UserIP
            if (!String.IsNullOrEmpty(stbUIP.Text))
            {
                searchUIPString = "`UserIP` LIKEAdmissionDate '%" + stbUIP.Text + "%'";
            }

            //формируем строку поика для ApiType
            if (sddApiType.SelectedValue != "0" && !String.IsNullOrEmpty(sddApiType.SelectedValue))
            {
                searchApiTypeString = "`ApiType` = '" + sddApiType.SelectedValue + "'";
            }

            //формируем строку поика для ApiName
            if (sddApiName.SelectedValue != "0" && !String.IsNullOrEmpty(sddApiName.SelectedValue))
            {
                searchApiNameString = "`ApiName` = '" + sddApiName.SelectedValue + "'";
            }

            //формируем строку поика для MethodName
            if (sddMethodName.SelectedValue != "0" && !String.IsNullOrEmpty(sddMethodName.SelectedValue))
            {
                searchMethodNameString = "`MethodName` = '" + sddMethodName.SelectedValue + "'";
            }

            //формируем строку поика для параметра
            if (!String.IsNullOrEmpty(stbApiKey.Text))
            {
                searchApiKeyString = "`ApiKey` LIKE '%" + stbApiKey.Text + "%'";
            }

            //формируем cтроку для поиска по Date
            if (!string.IsNullOrEmpty(stbCreateDate1.Text) && !string.IsNullOrEmpty(stbCreateDate2.Text))
            {
                searchCreateDateString = "(CreateDate BETWEEN '" +
                                   Convert.ToDateTime(stbCreateDate1.Text).ToString("yyyy-MM-dd") + "' AND '" +
                                   Convert.ToDateTime(stbCreateDate2.Text).ToString("yyyy-MM-dd") + "')";
            }

            if (!string.IsNullOrEmpty(stbCreateDate1.Text) && string.IsNullOrEmpty(stbCreateDate2.Text))
            {
                searchCreateDateString = "(CreateDate BETWEEN '" +
                                Convert.ToDateTime(stbCreateDate1.Text).ToString("yyyy-MM-dd") + "' AND '" +
                                Convert.ToDateTime(stbCreateDate1.Text).AddYears(1).ToString("yyyy-MM-dd") + "')";
            }

            if (string.IsNullOrEmpty(stbCreateDate1.Text) && !string.IsNullOrEmpty(stbCreateDate2.Text))
            {
                searchCreateDateString = "(CreateDate BETWEEN '" +
                                Convert.ToDateTime(stbCreateDate2.Text).AddYears(-2).ToString("yyyy-MM-dd") + "' AND '" +
                                Convert.ToDateTime(stbCreateDate2.Text).ToString("yyyy-MM-dd") + "')";
            }
            //формируем конечный запро для поиска
            searchParametres.Add("FieldID", searchIDString);
            searchParametres.Add("UserID", searchUIDString);
            searchParametres.Add("UserIP", searchUIPString);
            searchParametres.Add("ApiName", searchApiNameString);
            searchParametres.Add("ApiType", searchApiTypeString);
            searchParametres.Add("MethodName", searchMethodNameString);
            searchParametres.Add("ApiKey", searchApiKeyString);
            searchParametres.Add("CreateDateTime", searchCreateDateString);

            foreach (var searchParametre in searchParametres)
            {
                if (!string.IsNullOrEmpty(searchParametre.Value))
                {
                    searchString = searchString + searchParametre.Value + " AND ";
                }
            }

            searchString = searchString.Length < 4 ? "FROM apilog order by ID DESC" : String.Format("FROM apilog WHERE {0} order by ID DESC", searchString.Remove(searchString.Length - 4));

            var searchStringOver = "SELECT * " + searchString;

            return searchStringOver;
        }
        #endregion
    }
}