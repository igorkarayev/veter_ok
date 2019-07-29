using Delivery.BLL.Helpers;
using Delivery.BLL.StaticMethods;
using Delivery.DAL;
using Delivery.DAL.DataBaseObjects;
using DeliverySite.Resources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI.WebControls;

namespace Delivery.ManagerUI.Menu.Documents
{
    public partial class ReportsArchiveView : ManagerBasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            lvAllUsers.ItemCommand += lvAllUsers_Commands;
            btnReload.Click += btnReload_Click;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = PagesTitles.ManagerReportsViewTitle + BackendHelper.TagToValue("page_title_part");
            OtherMethods.ActiveRightMenuStyleChanche("hlDocuments", this.Page);
            OtherMethods.ActiveRightMenuStyleChanche("hlReportsArchive", this.Page);

            #region Блок доступа к странице
            var userInSession = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());
            if (currentRole.PageReportsView != 1)
            {
                Response.Redirect("~/Error.aspx?id=1");
            }
            #endregion

            var pril3ViewAccessArray = BackendHelper.TagToValue("put_3_reports_access");
            pnlPutevoi3.Visible = pril3ViewAccessArray.Split(new[] { ',' }).Any(p => p.Trim().Contains(userInSession.ID.ToString()));

            if (!IsPostBack)
            {
                CbAct.Checked = cbNaklPlil.Checked = cbPutevoi2.Checked = cbPutevoi1.Checked = cbZP.Checked = cbRasch.Checked = true;
                cbPutevoi3.Checked = pril3ViewAccessArray.Split(new[] { ',' }).Any(p => p.Trim().Contains(userInSession.ID.ToString()));
            }

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

        protected void lvAllUsers_Commands(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                DeleteAccess();
                var report = new Reports { ID = Convert.ToInt32(e.CommandArgument.ToString()) };
                report.GetById();
                File.Delete(Server.MapPath("~/ReportsArch/" + report.FileName));
                report.Delete(Convert.ToInt32(e.CommandArgument.ToString()));
                Page.Response.Redirect("~/ManagerUI/Menu/Documents/ReportsView.aspx");
            }

        }

        #region Настройки доступа к странице и действиям
        protected void DeleteAccess()
        {
            var userInSession = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());
            if (currentRole.ActionReportsDelete != 1)
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
            lblAllResult.Text = ds.Tables[0].Rows.Count.ToString();
            lvAllUsers.DataSource = ds;
            lvAllUsers.DataBind();

            #region Редирект на первую страницу при поиске
            if (lvAllUsers.Items.Count == 0 && lvDataPager.TotalRowCount != 0)
            {
                lvDataPager.SetPageProperties(0, lvDataPager.PageSize, false);
                lvAllUsers.DataBind();
            }
            #endregion

            if (lvAllUsers.Items.Count == 0)
            {
                lblPage.Visible = false;
            }
            var userInSession = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());

            foreach (ListViewDataItem item in lvAllUsers.Items)
            {
                var ibView = (HyperLink)item.FindControl("HyperLink1");
                var ibDelete = (ImageButton)item.FindControl("ibDelete");
                var hlEdit = (HyperLink)item.FindControl("hlEdit");
                string typeArch = ((Label)item.FindControl("Label2")).Text.ToString();


                if (currentRole.ActionReportsDelete != 1)
                {
                    ibDelete.Visible = false;
                }

                if (currentRole.PageReportsEdit != 1)
                {
                    hlEdit.Visible = false;
                }

                if (currentRole.ViewRasch != 1 && typeArch == "Расчетный лист")
                {
                    ibDelete.Visible = false;
                    hlEdit.Visible = false;
                    ibView.Visible = false;
                }
            }
        }

        public String GetSearchString()
        {
            var searchBuferString = String.Empty;
            var searchString = String.Empty;
            var searchCreateDateString = String.Empty;
            var searchDocumentDateString = String.Empty;
            var searchDID = String.Empty;
            var searchPut1 = String.Empty;
            var searchPut2 = String.Empty;
            var searchPut3 = String.Empty;
            var searchCbAct = String.Empty;
            var searchZP1 = String.Empty;
            var searchNaklPril1 = String.Empty;
            var searchRasch = String.Empty;
            var searchParametres = new Dictionary<String, String>();
            var searchParametresTypeReport = new Dictionary<String, String>();

            //формируем cтроку для поиска по CreateDate
            if (!string.IsNullOrEmpty(stbCreateDate1.Text) || !string.IsNullOrEmpty(stbCreateDate2.Text))
            {
                if(string.IsNullOrEmpty(stbCreateDate1.Text))
                {
                    searchCreateDateString = "(`CreateDate` <= '" +
                                   Convert.ToDateTime(stbCreateDate2.Text).ToString("yyyy-MM-dd") + "')";
                }

                if (string.IsNullOrEmpty(stbCreateDate2.Text))
                {
                    searchCreateDateString = "(`CreateDate` >= '" +
                                   Convert.ToDateTime(stbCreateDate1.Text).ToString("yyyy-MM-dd") + "')";
                }

                if (!string.IsNullOrEmpty(stbCreateDate1.Text) && !string.IsNullOrEmpty(stbCreateDate2.Text))
                {
                    searchCreateDateString = "(`CreateDate` BETWEEN '" +
                                   Convert.ToDateTime(stbCreateDate1.Text).ToString("yyyy-MM-dd") + "' AND '" +
                                   Convert.ToDateTime(stbCreateDate2.Text).ToString("yyyy-MM-dd") + "')";
                }
                    
            }

            //формируем cтроку для поиска по DocumentDate
            if (!string.IsNullOrEmpty(stbDocumentDate1.Text) || !string.IsNullOrEmpty(stbDocumentDate2.Text))
            {
                if (string.IsNullOrEmpty(stbDocumentDate1.Text))
                {
                    searchDocumentDateString = "(`DocumentDate` <= '" +
                                   Convert.ToDateTime(stbDocumentDate2.Text).ToString("yyyy-MM-dd") + "')";
                }

                if (string.IsNullOrEmpty(stbDocumentDate2.Text))
                {
                    searchDocumentDateString = "(`DocumentDate` >= '" +
                                   Convert.ToDateTime(stbDocumentDate1.Text).ToString("yyyy-MM-dd") + "')";
                }

                if (!string.IsNullOrEmpty(stbDocumentDate1.Text) && !string.IsNullOrEmpty(stbDocumentDate2.Text))
                {
                    searchDocumentDateString = "(`DocumentDate` BETWEEN '" +
                                   Convert.ToDateTime(stbDocumentDate1.Text).ToString("yyyy-MM-dd") + "' AND '" +
                                   Convert.ToDateTime(stbDocumentDate2.Text).ToString("yyyy-MM-dd") + "')";
                }

            }

            //поиск по DID
            if(!string.IsNullOrEmpty(stbDID.Text))
            {
                searchDID = "`DriverID` = " + stbDID.Text;
            }

            //формируем cтроку для поиска по ReportType
            if (cbPutevoi2.Checked)
            {
                searchPut2 = "`ReportType` = 0";
            }

            if (cbPutevoi1.Checked)
            {
                searchPut1 = "`ReportType` = 3";
            }

            if (cbZP.Checked)
            {
                searchZP1 = "`ReportType` = 1";
            }

            if (cbNaklPlil.Checked)
            {
                searchNaklPril1 = "`ReportType` = 2";
            }

            if (cbPutevoi3.Checked)
            {
                searchPut3 = "`ReportType` = 4";
            }

            if (CbAct.Checked)
            {
                searchCbAct = "`ReportType` = 5";
            }

            if (cbRasch.Checked)
            {
                searchRasch = "`ReportType` = 6";
            }
            //формируем конечный запро для поиска
            searchParametres.Add("CreateDate", searchCreateDateString);
            searchParametres.Add("DocumentDate", searchDocumentDateString);
            searchParametres.Add("DID", searchDID);
            searchParametresTypeReport.Add("ReportType0", searchPut2);
            searchParametresTypeReport.Add("ReportType1", searchZP1);
            searchParametresTypeReport.Add("ReportType2", searchNaklPril1);
            searchParametresTypeReport.Add("ReportType3", searchPut1);
            searchParametresTypeReport.Add("ReportType4", searchPut3);
            searchParametresTypeReport.Add("ReportType5", searchCbAct);
            searchParametresTypeReport.Add("ReportType6", searchRasch);

            if (cbNaklPlil.Checked || cbPutevoi1.Checked || cbPutevoi2.Checked || cbZP.Checked || cbPutevoi3.Checked || CbAct.Checked || cbRasch.Checked)
            {
                searchBuferString = "( ";
                foreach (var searchParametre in searchParametresTypeReport)
                {
                    if (!string.IsNullOrEmpty(searchParametre.Value))
                    {
                        searchBuferString = searchBuferString + searchParametre.Value + " OR ";
                    }
                }
                searchBuferString = searchBuferString.Remove(searchBuferString.Length - 3);
                searchBuferString = searchBuferString + ")";
            }

            searchParametres.Add("ReportType", searchBuferString);

            foreach (var searchParametre in searchParametres)
            {
                if (!string.IsNullOrEmpty(searchParametre.Value))
                {
                    searchString = searchString + searchParametre.Value + " AND ";
                }
            }

            //формируем строку поика

            searchString = searchString.Length < 4 ? "SELECT * FROM `reports` order by CreateDate DESC" : String.Format("SELECT * FROM `reports` WHERE {0} order by CreateDate DESC ", searchString.Remove(searchString.Length - 4));

            return searchString;
        }
        #endregion
    }
}