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
    public partial class FeedbacksView : ManagerBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            OtherMethods.ActiveRightMenuStyleChanche("hlSouls", this.Page);
            OtherMethods.ActiveRightMenuStyleChanche("hlFeedback", this.Page);
            Page.Title = PagesTitles.UserFeedbacksView + BackendHelper.TagToValue("page_title_part");

            #region Настройки доступа к странице
            var userInSession = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());
            if (currentRole.PageFeedbacksView != 1)
            {
                Response.Redirect("~/Error.aspx?id=1");
            }
            #endregion

            if (!IsPostBack)
            {
                sddlStatus.DataSource = Feedback.Statuses;
                sddlStatus.DataTextField = "Value";
                sddlStatus.DataValueField = "Key";
                sddlStatus.DataBind();
                sddlStatus.Items.Insert(0, new ListItem("Все", string.Empty));
                sddlStatus.SelectedValue = "0";

                sddlPriority.DataSource = Feedback.Priorities;
                sddlPriority.DataTextField = "Value";
                sddlPriority.DataValueField = "Key";
                sddlPriority.DataBind();
                sddlPriority.Items.Insert(0, new ListItem("Все", string.Empty));
                sddlPriority.SelectedValue = "";

                sddlType.DataSource = Feedback.Types;
                sddlType.DataTextField = "Value";
                sddlType.DataValueField = "Key";
                sddlType.DataBind();
                sddlType.Items.Insert(0, new ListItem("Все", string.Empty));
                sddlType.SelectedValue = "";

                //закрываем старые обращения, которым с последнего коммента или создания (при отсутствии коммента) 21 день
                var dm = new DataManager();
                dm.QueryWithoutReturnData(null,
                    "UPDATE `feedback` F SET F.`StatusID` = 1 WHERE (SELECT DATEDIFF(NOW(), F.`CreateDate`) AS DiffDate) > 21 AND F.`ChangeDate` IS NULL AND F.`StatusID` <> 1;" +
                    "UPDATE `feedback` F SET F.`StatusID` = 1 WHERE (SELECT DATEDIFF(NOW(),(SELECT FC.`CreateDate` FROM `feedbackcomments` FC WHERE FC.`FeedbackID` = F.`ID` ORDER BY ID DESC LIMIT 1)) AS DiffDate) > 21 AND F.`StatusID` <> 1;");
            }
        }
        protected void btnReload_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }

        //этот метод перед самой отрисовкой страницы биндит все данные
        protected void Page_PreRender(object sender, EventArgs e)
        {
            ListViewDataBind();
        }

        #region Methods
        protected void ListViewDataBind()
        {
            var dm = new DataManager();
            var ds = dm.QueryWithReturnDataSet(GetSearchString());
            lblAllResult.Text = ds.Tables[0].Rows.Count.ToString();
            lvAllFeedback.DataSource = ds;
            lvAllFeedback.DataBind();

            if (lvAllFeedback.Items.Count == 0)
            {
                lblPage.Visible = false;
            }

            #region Редирект на первую страницу при поиске
            if (lvAllFeedback.Items.Count == 0 && lvDataPager.TotalRowCount != 0)
            {
                lvDataPager.SetPageProperties(0, lvDataPager.PageSize, false);
                lvAllFeedback.DataBind();
            }
            #endregion
        }

        public String GetSearchString()
        {
            var searchString = String.Empty;
            var selectedStatusString = String.Empty;
            var selectedPriorityString = String.Empty;
            var selectedTypeString = String.Empty;
            var searchIDString = String.Empty;
            var searchParametres = new Dictionary<String, String>();

            //формируем cтроку для поиска по Title
            if (!string.IsNullOrEmpty(stbID.Text))
            {
                searchIDString = "F.`ID` = '" + stbID.Text + "'";
            }

            //формируем cтроку для поиска по Status
            if (!string.IsNullOrEmpty(sddlPriority.SelectedValue))
            {
                selectedPriorityString = "F.`PriorityID` = '" + sddlPriority.SelectedValue + "'";
            }

            //формируем cтроку для поиска по Status
            if (!string.IsNullOrEmpty(sddlType.SelectedValue))
            {
                selectedTypeString = "F.`TypeID` = '" + sddlType.SelectedValue + "'";
            }

            //формируем cтроку для поиска по Status
            if (!string.IsNullOrEmpty(sddlStatus.SelectedValue))
            {
                selectedStatusString = "F.`StatusID` = '" + sddlStatus.SelectedValue + "'";
            }


            //формируем конечный запро для поиска
            searchParametres.Add("ID", searchIDString);
            searchParametres.Add("TypeID", selectedTypeString);
            searchParametres.Add("PriorityID", selectedPriorityString);
            searchParametres.Add("StatusID", selectedStatusString);

            foreach (var searchParametre in searchParametres)
            {
                if (!string.IsNullOrEmpty(searchParametre.Value))
                {
                    searchString = searchString + searchParametre.Value + " AND ";
                }
            }

            //формируем строку поика

            searchString = searchString.Length < 4
                ? "SELECT F.*, (SELECT COUNT(ID) FROM feedbackcomments WHERE FeedbackID = F.ID ) as CommetsCount FROM feedback F ORDER BY StatusID ASC, PriorityID DESC, ChangeDate DESC, CreateDate DESC;"
                : String.Format("SELECT F.*, (SELECT COUNT(ID) FROM feedbackcomments WHERE FeedbackID = F.ID ) as CommetsCount FROM feedback F WHERE {0} ORDER BY StatusID ASC, PriorityID DESC, ChangeDate DESC, CreateDate DESC;", searchString.Remove(searchString.Length - 4));

            return searchString;
        }
        #endregion
    }
}