using Delivery.BLL.Helpers;
using Delivery.BLL.StaticMethods;
using Delivery.DAL;
using Delivery.DAL.DataBaseObjects;
using DeliverySite.Resources;
using System;

namespace Delivery.UserUI
{
    public partial class FeedbacksView : UserBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            OtherMethods.ActiveRightMenuStyleChanche("hlFeedback", this.Page);
            Page.Title = PagesTitles.UserFeedbacksView + BackendHelper.TagToValue("page_title_part");
            if (String.IsNullOrEmpty(ActivatedProfilesCount) || Convert.ToInt32(ActivatedProfilesCount) == 0)
            {
                Response.Redirect("~/usernotification/12");
            }

            //закрываем старые обращения, которым с последнего коммента или создания (при отсутствии коммента) 21 день
            var dm = new DataManager();
            dm.QueryWithoutReturnData(null,
                "UPDATE `feedback` F SET F.`StatusID` = 1 WHERE (SELECT DATEDIFF(NOW(), F.`CreateDate`) AS DiffDate) > 21 AND F.`ChangeDate` IS NULL AND F.`StatusID` <> 1;" +
                "UPDATE `feedback` F SET F.`StatusID` = 1 WHERE (SELECT DATEDIFF(NOW(),(SELECT FC.`CreateDate` FROM `feedbackcomments` FC WHERE FC.`FeedbackID` = F.`ID` ORDER BY ID DESC LIMIT 1)) AS DiffDate) > 21 AND F.`StatusID` <> 1;");
        }

        protected void lvDataPager_PreRender(object sender, EventArgs e)
        {
            var userInSession = (Users)Session["userinsession"];
            var dm = new DataManager();
            var dataSet = dm.QueryWithReturnDataSet("SELECT F.*, (SELECT COUNT(ID) FROM feedbackcomments WHERE FeedbackID = F.ID AND IsViewed = 0) as NewCommetsCount FROM feedback F  WHERE F.UserID = '" + userInSession.ID + "' " +
                                                    "ORDER BY StatusID ASC, NewCommetsCount DESC, PriorityID DESC, ChangeDate DESC, CreateDate DESC;");
            lvAllFeedback.DataSource = dataSet;
            lvAllFeedback.DataBind();
            if (lvAllFeedback.Items.Count == 0)
            {
                lblPage.Visible = false;
            }
        }
    }
}