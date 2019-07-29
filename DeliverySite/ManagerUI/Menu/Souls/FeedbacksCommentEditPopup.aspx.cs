using Delivery.DAL.DataBaseObjects;
using System;

namespace Delivery.ManagerUI.Menu.Souls
{
    public partial class FeedbacksCommentEditPopup : ManagerBasePage
    {
        protected String AppKey { get; set; }
        protected String CommentId { get; set; }
        protected String CurrentUserId { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            AppKey = Globals.Settings.AppServiceSecureKey;
            var userInSession = (Users)Session["userinsession"];
            CurrentUserId = userInSession.ID.ToString();
            var id = Page.Request.Params["id"];
            var feedbackcomment = new FeedbackComments
            {
                ID = Convert.ToInt32(id)
            };
            feedbackcomment.GetById();
            if (feedbackcomment.UserID != userInSession.ID)
            {
                tbComment.Visible = btnSave.Visible = false;
                pnlError.Visible = true;
            }
            CommentId = id;
            if (!IsPostBack)
                tbComment.Text = feedbackcomment.Comment;
        }
    }
}