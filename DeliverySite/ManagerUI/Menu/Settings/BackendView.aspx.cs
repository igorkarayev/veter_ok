using Delivery.BLL.Helpers;
using Delivery.BLL.StaticMethods;
using Delivery.DAL;
using Delivery.DAL.DataBaseObjects;
using DeliverySite.Resources;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Delivery.ManagerUI.Menu.Settings
{
    public partial class BackendView : ManagerBasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            btnReload.Click += btnReload_Click;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = PagesTitles.ManagerBackendView + BackendHelper.TagToValue("page_title_part");
            OtherMethods.ActiveRightMenuStyleChanche("hlBackend", this.Page);
            OtherMethods.ActiveRightMenuStyleChanche("hlSettings", this.Page);

            #region Блок доступа к странице
            var userInSession = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());
            if (currentRole.PageBackendView != 1)
            {
                Response.Redirect("~/Error.aspx?id=1");
            }
            #endregion

            if (!IsPostBack)
            {
                stbTag.Text = Page.Request.Params["tag"];
                stbValue.Text = Page.Request.Params["value"];
                stbDescription.Text = Page.Request.Params["description"];
            }

        }

        //этот метод перед самой отрисовкой страницы биндит все данные
        protected void Page_PreRender(object sender, EventArgs e)
        {
            ListViewDataBind();
        }

        protected void btnReload_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ManagerUI/Menu/Settings/BackendView.aspx");
        }

        #region Methods
        private void ListViewDataBind()
        {
            var dm = new DataManager();
            var ds = dm.QueryWithReturnDataSet(GetSearchString());
            lvAllBackend.DataSource = ds;
            lblAllResult.Text = ds.Tables[0].Rows.Count.ToString();
            lvAllBackend.DataBind();
        }

        public String GetSearchString()
        {
            var searchString = String.Empty;
            var searchTagString = String.Empty;
            var searchValueString = String.Empty;
            var searchDescriptionString = String.Empty;
            var searchParametres = new Dictionary<String, String>();

            //формируем cтроку для поиска по Family
            if (!string.IsNullOrEmpty(stbTag.Text))
            {
                searchTagString = "B.`Tag` LIKE '%" + stbTag.Text.Trim() + "%'";
            }

            //формируем cтроку для поиска по Family
            if (!string.IsNullOrEmpty(stbValue.Text))
            {
                searchValueString = "B.`Value` LIKE '%" + stbValue.Text.Trim() + "%'";
            }

            //формируем cтроку для поиска по Family
            if (!string.IsNullOrEmpty(stbDescription.Text))
            {
                searchDescriptionString = "B.`Description` LIKE '%" + stbDescription.Text.Trim() + "%'";
            }

            //формируем конечный запро для поиска
            searchParametres.Add("Tag", searchTagString);
            searchParametres.Add("Value", searchValueString);
            searchParametres.Add("Description", searchDescriptionString);

            foreach (var searchParametre in searchParametres)
            {
                if (!string.IsNullOrEmpty(searchParametre.Value))
                {
                    searchString = searchString + searchParametre.Value + " AND ";
                }
            }
            const string sortExpression = "ORDER BY B.`Tag` ASC";

            //формируем строку поика
            searchString = searchString.Length < 4 ?
                String.Format("SELECT B.* FROM backend B {0}",
                              sortExpression) :
                String.Format("SELECT B.* FROM backend B WHERE {0} {1}",
                              searchString.Remove(searchString.Length - 4),
                              sortExpression);

            return searchString;
        }
        #endregion
    }
}