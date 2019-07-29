using Delivery.BLL.Helpers;
using Delivery.BLL.StaticMethods;
using Delivery.DAL;
using Delivery.DAL.DataBaseObjects;
using DeliverySite.Resources;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Delivery.ManagerUI.Menu.Finance
{
    public partial class MoneyGruzobozView : ManagerBasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            btnCalc.Click += btnCalc_Click;
            btnReload.Click += btnReload_Click;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = PagesTitles.ManagerMoneyDeliveryView + BackendHelper.TagToValue("page_title_part");
            OtherMethods.ActiveRightMenuStyleChanche("hlMoney", this.Page);
            OtherMethods.ActiveRightMenuStyleChanche("hlAllDeliveryMoney", this.Page);

            #region Блок доступа к странице
            var userInSession = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());
            if (currentRole.PageMoneyDeliveryView != 1)
            {
                Response.Redirect("~/Error.aspx?id=1");
            }
            #endregion

            if (!IsPostBack)
            {
                pnlResultPanel.Visible = false;
            }
        }


        protected void btnCalc_Click(object sender, EventArgs e)
        {
            GetMoney(GetSearchString());
            pnlResultPanel.Visible = true;
        }

        protected void btnReload_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.ServerVariables["URL"]);
        }

        #region Methods
        private void GetMoney(string query)
        {
            var searchString = query;
            var dm = new DataManager();
            var allGruzobozCostString = dm.QueryWithReturnDataSet("SELECT SUM(`GruzobozCost`) " + searchString).Tables[0].Rows[0][0].ToString();

            if (allGruzobozCostString == String.Empty)
            {
                allGruzobozCostString = "0";
            }

            var allGruzobozCost = Convert.ToDecimal(allGruzobozCostString);

            lblGruzobozCostOver.Text = MoneyMethods.MoneySeparator(allGruzobozCost.ToString());
        }


        public String GetSearchString()
        {
            var searchString = String.Empty;
            var searchDateString = String.Empty;
            var searchParametres = new Dictionary<String, String>();

            //формируем cтроку для поиска по Date
            if (!string.IsNullOrEmpty(stbDeliveryDate1.Text) && !string.IsNullOrEmpty(stbDeliveryDate2.Text))
            {
                searchDateString = "(`DeliveryDate` BETWEEN '" +
                                   Convert.ToDateTime(stbDeliveryDate1.Text).ToString("yyyy-MM-dd") + "' AND '" +
                                   Convert.ToDateTime(stbDeliveryDate2.Text).ToString("yyyy-MM-dd") + "')";
            }
            else
            {
                if (!string.IsNullOrEmpty(stbDeliveryDate1.Text))
                {
                    searchDateString = "`DeliveryDate` = '" + Convert.ToDateTime(stbDeliveryDate1.Text).ToString("yyyy-MM-dd") + "'";
                }
            }

            //формируем конечный запро для поиска
            searchParametres.Add("DeliveryDate", searchDateString);

            foreach (var searchParametre in searchParametres)
            {
                if (!string.IsNullOrEmpty(searchParametre.Value))
                {
                    searchString = searchString + searchParametre.Value + " AND ";
                }
            }
            //считаем заявки только в статусе завершено и обработано
            var searchStringOver = searchString.Length < 4 ? "FROM tickets WHERE  (StatusID = 5 or StatusID = 6 or StatusID = 12 or StatusID = 8 or StatusID = 15  or StatusID = 16)  order by ID DESC" : String.Format("FROM tickets WHERE  (StatusID = 5 or StatusID = 6)  AND {0}", searchString.Remove(searchString.Length - 4));

            return searchStringOver;
        }
        #endregion
    }
}