using Delivery.BLL.Helpers;
using Delivery.BLL.StaticMethods;
using Delivery.DAL;
using Delivery.DAL.DataBaseObjects;
using DeliverySite.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace Delivery.ManagerUI.Menu.Issuance
{
    public partial class IssuanceView : ManagerBasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            btnReload.Click += btnReload_Click;
            btnAction.Click += btnAction_Click;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = PagesTitles.ManagerIssuanceViewTitle + BackendHelper.TagToValue("page_title_part");
            OtherMethods.ActiveRightMenuStyleChanche("hlIssuance", this.Page);
            OtherMethods.ActiveRightMenuStyleChanche("hlIssuanceView", this.Page);
            if (Session["flash:now"] != null && Session["flash:now"].ToString() != String.Empty)
            {
                lblStatus.Text = Session["flash:now"].ToString();
                Session["flash:now"] = String.Empty;
            }

            #region Блок доступа к странице
            var userInSession = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());
            if (currentRole.PageIssuanceView != 1)
            {
                Response.Redirect("~/Error.aspx?id=1");
            }
            #endregion

            var dm = new DataManager();
            var deliveryDate = String.Empty;
            if (!string.IsNullOrEmpty(stbDeliveryDate1.Text) && !string.IsNullOrEmpty(stbDeliveryDate2.Text))
            {
                deliveryDate = "AND (T.`DeliveryDate` BETWEEN '" +
                                   Convert.ToDateTime(stbDeliveryDate1.Text).ToString("yyyy-MM-dd") + "' AND '" +
                                   Convert.ToDateTime(stbDeliveryDate2.Text).ToString("yyyy-MM-dd") + "')";
            }
            else
            {
                if (!string.IsNullOrEmpty(stbDeliveryDate1.Text))
                {
                    deliveryDate = "AND T.`DeliveryDate` = '" + Convert.ToDateTime(stbDeliveryDate1.Text).ToString("yyyy-MM-dd") + "'";
                }
            }


            var dataSet4 = dm.QueryWithReturnDataSet(String.Format("SELECT DISTINCT T.UserID FROM tickets T WHERE (T.StatusID = 5 OR T.StatusID = 7 OR T.StatusID = 8 OR T.StatusID = 9) {0} ORDER BY T.UserID ASC", deliveryDate));
            sddlUID.DataSource = dataSet4;
            sddlUID.DataTextField = "UserID";
            sddlUID.DataValueField = "UserID";
            sddlUID.DataBind();
            sddlUID.Items.Insert(0, new ListItem("Все", string.Empty));

            var userIdExist = false;
            foreach (ListItem item in sddlUID.Items)
            {
                if (item.Value == Page.Request["ctl00$MainContent$sddlUID"] && userIdExist == false)
                {
                    userIdExist = true;
                }
            }
            if (userIdExist)
            {
                sddlUID.SelectedValue = Page.Request["ctl00$MainContent$sddlUID"];
            }
            //формируем форму поиска по водителю КОНЕЦ

            if (!IsPostBack)
            {

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

                var dataSet1 = dm.QueryWithReturnDataSet("SELECT * FROM issuancelists WHERE IssuanceListsStatusID = 1  ORDER BY ID DESC");
                dataSet1.Tables[0].Columns.Add("IDAndDateAndUID", typeof(string), ("'#' + ID + ', uid: ' + UserID + ', др: ' + IssuanceDate"));
                ddlIssuanceLists.DataSource = dataSet1;
                ddlIssuanceLists.DataTextField = "IDAndDateAndUID";
                ddlIssuanceLists.DataValueField = "ID";
                ddlIssuanceLists.DataBind();
                foreach (ListItem item in ddlIssuanceLists.Items)
                {
                    item.Text = item.Text.Remove(item.Text.Length - 8);
                }
                ddlIssuanceLists.Items.Insert(0, new ListItem("Не назначен", "0"));
            }

            lblPage.Visible = false;
            pnlSearschResult.Visible = pnlActions.Visible = false;
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
            pnlSearschResult.Visible = pnlActions.Visible = lvAllTickets.Items.Count != 0;
        }

        protected void btnReload_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.ServerVariables["URL"]);
        }

        protected void btnAction_Click(object sender, EventArgs e)
        {
            lblNotif.Text = String.Empty;
            foreach (var items in lvAllTickets.Items)
            {
                var chkBoxRows = (CheckBox)items.FindControl("cbSelect");

                if (chkBoxRows.Checked)
                {
                    var id = (HiddenField)items.FindControl("hfID");
                    var ticket = new DAL.DataBaseObjects.Tickets { ID = Convert.ToInt32(id.Value) };
                    ticket.GetById();

                    var issuance = new IssuanceLists() { ID = Convert.ToInt32(ddlIssuanceLists.SelectedValue) };
                    issuance.GetById();

                    //присваиваем заявке расчетный лист только если ее юсерАйДИ совпадает с АйДи из расчетного лисна или если убираем расчетный лист с заявки
                    if (ticket.UserID == issuance.UserID || ddlIssuanceLists.SelectedValue == "0")
                    {
                        ticket.IssuanceListID = Convert.ToInt32(ddlIssuanceLists.SelectedValue);
                        ticket.Update();
                    }
                }
            }
        }

        #region Methods
        public String GetSearchString()
        {
            var searchString = String.Empty;
            var searchUserIdString = String.Empty;
            var searchDateString = String.Empty;
            var searchParametres = new Dictionary<String, String>();

            //формируем строку поика для uid
            if (sddlUID.SelectedValue != "0" && !String.IsNullOrEmpty(sddlUID.SelectedValue))
            {
                searchUserIdString = searchUserIdString + "`UserID` = '" + sddlUID.SelectedValue + "'";
            }

            //формируем cтроку для поиска по дате отправки
            if (!string.IsNullOrEmpty(stbDeliveryDate1.Text) && !string.IsNullOrEmpty(stbDeliveryDate2.Text))
            {
                searchDateString = "(DeliveryDate BETWEEN '" +
                                   Convert.ToDateTime(stbDeliveryDate1.Text).ToString("yyyy-MM-dd") + "' AND '" +
                                   Convert.ToDateTime(stbDeliveryDate2.Text).ToString("yyyy-MM-dd") + "')";
            }

            if (!string.IsNullOrEmpty(stbDeliveryDate1.Text) && string.IsNullOrEmpty(stbDeliveryDate2.Text))
            {
                searchDateString = "(DeliveryDate BETWEEN '" +
                                   Convert.ToDateTime(stbDeliveryDate1.Text).ToString("yyyy-MM-dd") + "' AND '" +
                                   Convert.ToDateTime(stbDeliveryDate1.Text).AddYears(1).ToString("yyyy-MM-dd") + "')";
            }

            if (string.IsNullOrEmpty(stbDeliveryDate1.Text) && !string.IsNullOrEmpty(stbDeliveryDate2.Text))
            {
                searchDateString = "(DeliveryDate BETWEEN '" +
                                   Convert.ToDateTime(stbDeliveryDate2.Text).AddYears(-2).ToString("yyyy-MM-dd") + "' AND '" +
                                   Convert.ToDateTime(stbDeliveryDate2.Text).ToString("yyyy-MM-dd") + "')";
            }

            //формируем конечный запро для поиска
            searchParametres.Add("UserID", searchUserIdString);
            searchParametres.Add("DeliveryDate", searchDateString);

            foreach (var searchParametre in searchParametres)
            {
                if (!string.IsNullOrEmpty(searchParametre.Value))
                {
                    searchString = searchString + searchParametre.Value + " AND ";
                }
            }

            searchString = searchString.Length < 5 ? "FROM tickets WHERE (StatusID = 5 OR StatusID = 7 OR StatusID = 8 OR StatusID = 9) order by AdmissionDate DESC" : String.Format("FROM tickets WHERE (StatusID = 5 OR StatusID = 7 OR StatusID = 8 OR StatusID = 9) AND {0}  order by AdmissionDate DESC", searchString.Remove(searchString.Length - 4));

            var searchStringOver = "SELECT * " + searchString;

            return searchStringOver;
        }
        #endregion
    }
}