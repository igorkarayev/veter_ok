using Delivery.DAL.DataBaseObjects;
using System;

namespace Delivery
{
    public partial class UserNotification : BasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            btnToMainPage.Click += btnToMainPage_Click;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var id = Context.Items["id"] ?? Page.Request.Params["id"];
            var note = new Notification
            {
                ID = id != null ? Convert.ToInt32(id) : 3
            };
            note.GetById();
            var usernamee = Context.Items["username"] != null ? (Context.Items["username"].ToString()) : "пользователь";
            var replacednote = note.Description.Replace("{userName}", usernamee);
            lblNotifTitle.Text = note.Title;
            lblNotifBody.Text = replacednote;
        }

        public void btnToMainPage_Click(Object sender, EventArgs e)
        {
            Page.Response.Redirect("~/");
        }
    }
}