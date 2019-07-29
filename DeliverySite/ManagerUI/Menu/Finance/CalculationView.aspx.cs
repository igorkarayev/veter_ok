using Delivery.BLL.Helpers;
using Delivery.BLL.StaticMethods;
using Delivery.DAL;
using Delivery.DAL.DataBaseObjects;
using DeliverySite.Resources;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Delivery.ManagerUI.Menu.Finance
{
    public partial class CalculationView : ManagerBasePage
    {
        public String SelectToExcelFile { get; set; }

        protected void Page_Init(object sender, EventArgs e)
        {
            btnReload.Click += btnReload_Click;
            btnExport.Click += btnExport_Click;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = PagesTitles.ManagerCalculationView + BackendHelper.TagToValue("page_title_part");
            OtherMethods.ActiveRightMenuStyleChanche("hlMoney", this.Page);
            OtherMethods.ActiveRightMenuStyleChanche("hlCalculation", this.Page);

            #region Блок доступа к странице
            var userInSession = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());
            if (currentRole.PageCalculationView != 1)
            {
                Response.Redirect("~/Error.aspx?id=1");
            }
            #endregion

            if (!IsPostBack)
            {
                stbDeliveryDate1.Text = DateTime.Now.ToString("dd-MM-yyyy");
                if (!string.IsNullOrEmpty(Page.Request.Params["stateSave"]))
                {
                    if (!string.IsNullOrEmpty(Page.Request.Params["deliveryDate1"]))
                    {
                        stbDeliveryDate1.Text = Page.Request.Params["deliveryDate1"];
                    }

                    if (!string.IsNullOrEmpty(Page.Request.Params["deliveryDate2"]))
                    {
                        stbDeliveryDate2.Text = Page.Request.Params["deliveryDate2"];
                    }
                }
            }

            lblPage.Visible = false;
        }

        //этот метод перед самой отрисовкой страницы биндит все данные
        protected void Page_PreRender(object sender, EventArgs e)
        {
            ListViewData();
        }

        private void ListViewData()
        {
            var dm = new DataManager();
            var ds = dm.QueryWithReturnDataSet(GetSearchString());
            lvAllTickets.DataSource = ds;
            lvAllTickets.DataBind();
            lblAllResult.Text = ds.Tables[0].Rows.Count.ToString();

            lblPage.Visible = lvAllTickets.Items.Count != 0;
        }

        protected void btnReload_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.ServerVariables["URL"]);
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            var dm = new DataManager();
            GetSearchString();
            var infoSet = dm.QueryWithReturnDataSet(SelectToExcelFile);
            const string fileName = "касса-рассчет";
            ExportMethods.CreateXlsFile(Response, infoSet, fileName, "calculation");
        }

        #region Methods
        public String GetSearchString()
        {
            var searchString = String.Empty;
            var searchUserIdString = String.Empty;
            var searchDateString = String.Empty;
            var searchFamilyString = String.Empty;
            var searchPhoneString = String.Empty;
            var searchParametres = new Dictionary<String, String>();

            //формируем строку поика для uid
            if (!String.IsNullOrEmpty(stbUID.Text))
            {
                searchUserIdString = "`UserID` = '" + stbUID.Text + "'";
            }

            //формируем строку поика для Family
            if (!String.IsNullOrEmpty(stbFamily.Text))
            {
                searchFamilyString = "`Family` LIKE '%" + stbFamily.Text + "%'";
            }

            //формируем строку поика для Phone
            if (!String.IsNullOrEmpty(stbRecipientPhone.Text))
            {
                searchPhoneString = "`Phone` LIKE '%" + stbRecipientPhone.Text + "%'";
            }

           //формируем cтроку для поиска по дате отправки
            if (!string.IsNullOrEmpty(stbDeliveryDate1.Text) && !string.IsNullOrEmpty(stbDeliveryDate2.Text))
            {
                searchDateString = "(DeliveryDate BETWEEN '" +
                                   Convert.ToDateTime(stbDeliveryDate1.Text).ToString("yyyy-MM-dd") + "' AND '" +
                                   Convert.ToDateTime(stbDeliveryDate2.Text).ToString("yyyy-MM-dd") + "')";
            }
            else if (!string.IsNullOrEmpty(stbDeliveryDate1.Text) && string.IsNullOrEmpty(stbDeliveryDate2.Text))
            {
                searchDateString = "(DeliveryDate BETWEEN '" +
                                   Convert.ToDateTime(stbDeliveryDate1.Text).ToString("yyyy-MM-dd") + "' AND '" +
                                   Convert.ToDateTime(stbDeliveryDate1.Text).AddYears(1).ToString("yyyy-MM-dd") + "')";
            }
            else if (string.IsNullOrEmpty(stbDeliveryDate1.Text) && !string.IsNullOrEmpty(stbDeliveryDate2.Text))
            {
                searchDateString = "(DeliveryDate BETWEEN '" +
                                   Convert.ToDateTime(stbDeliveryDate2.Text).AddYears(-2).ToString("yyyy-MM-dd") +
                                   "' AND '" +
                                   Convert.ToDateTime(stbDeliveryDate2.Text).ToString("yyyy-MM-dd") + "')";
            }
            else
            {
                searchDateString = "(DeliveryDate)";
            }

            //формируем конечный запро для поиска
            searchParametres.Add("UserID", searchUserIdString);
            searchParametres.Add("DeliveryDate", searchDateString);
            searchParametres.Add("Phone", searchPhoneString);
            searchParametres.Add("Family", searchFamilyString);

            foreach (var searchParametre in searchParametres)
            {
                if (!string.IsNullOrEmpty(searchParametre.Value))
                {
                    searchString = searchString + searchParametre.Value + " AND ";
                }
            }

            searchString = searchString.Length < 5 ?
                "FROM tickets WHERE (StatusID = 3 OR StatusID = 5 OR StatusID = 12 OR StatusID = 8 OR StatusID = 15 OR StatusID = 16) order by DeliveryDate DESC" :
                String.Format("FROM tickets WHERE (StatusID = 3 OR StatusID = 5 OR StatusID = 12 OR StatusID = 8 OR StatusID = 15 OR StatusID = 16) AND {0}  order by DeliveryDate DESC", searchString.Remove(searchString.Length - 4));

            var searchStringOver = "SELECT * " + searchString;
            SelectToExcelFile = "SELECT " +
                                "`DeliveryDate` as \"Дата отправки\", " +
                                "`UserProfileId` as \"Профиль\", " +
                                "`SecureID` as \"ID\", " +
                                "`StatusID` as \"Статус\", " +
                                "`ID` as \"Оцен/Согл + за дост.\", " +
                                "`GruzobozCost` as \"За услугу\", " +
                                "`ID` as \"Оцен/Согл + за дост. - за усл.\", " +
                                "`Note` as \"Примечание\", " +
                                "`Comment` as \"Ком. менеджера\", " +
                                "`DriverID` as \"Водитель\" " + searchString;
            return searchStringOver;
        }
        #endregion
    }
}