using Delivery.BLL.Helpers;
using Delivery.BLL.StaticMethods;
using Delivery.DAL.DataBaseObjects;
using DeliverySite.Resources;
using System;
using System.Web.UI.WebControls;

namespace Delivery.UserUI
{
    public partial class ProfilesView : UserBasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = PagesTitles.UserProfilesViewTitle + BackendHelper.TagToValue("page_title_part");
            OtherMethods.ActiveRightMenuStyleChanche("hlProfile", this.Page);
        }

        public void lbDelete_Click(Object sender, EventArgs e)
        {
            var userInSession = (Users)Session["userinsession"];
            var lb = (LinkButton)sender;
            var profile = new UsersProfiles();
            profile.Delete(Convert.ToInt32(lb.CommandArgument), userInSession.ID, OtherMethods.GetIPAddress(), "UProfilesView");
            Page.Response.Redirect("~/UserUI/ProfilesView.aspx");
        }

        public void lbDefault_Click(Object sender, EventArgs e)
        {
            var lb = (LinkButton)sender;
            var profileOldDefault = new UsersProfiles
            {
                UserID = UserID,
                IsDefault = 1
            };
            profileOldDefault.GetByUserIDAndDefault();
            profileOldDefault.IsDefault = 0;
            profileOldDefault.Update();
            var profile = new UsersProfiles { IsDefault = 1, ID = Convert.ToInt32(lb.CommandArgument) };
            profile.Update();
            Page.Response.Redirect("~/UserUI/ProfilesView.aspx");
        }

        //этот метод перед самой отрисовкой страницы биндит все данные
        protected void Page_PreRender(object sender, EventArgs e)
        {
            ListViewDataBind();
            if (lvAllProfile.Items.Count == 0)
            {
                lblPage.Visible = false;
            }
        }

        protected void ListViewDataBind()
        {
            var profile = new UsersProfiles { UserID = UserID };
            lvAllProfile.DataSource = profile.GetAllItemsByUserID();
            lvAllProfile.DataBind();
            foreach (ListViewDataItem items in lvAllProfile.Items)
            {
                var lbProfileDefaultLinck = (LinkButton)items.FindControl("lbDefault");
                var lblIsDefault = (Label)items.FindControl("lblIsDefault");

                var hlProfileChangeLink = (HyperLink)items.FindControl("hlChange");
                var hfStatus = (HiddenField)items.FindControl("hfStatus");
                var lbDelete = (LinkButton)items.FindControl("lbDelete");

                if (lblIsDefault.Text == "&#10004;")
                {
                    lbProfileDefaultLinck.Visible = false;
                }

                //еслистатус не "Активен"-запрещаемредактировать профиль
                if (hfStatus.Value == "1" || hfStatus.Value == "3")
                {
                    hlProfileChangeLink.Visible = false;
                    lbDelete.Visible = false;
                }
            }
        }
    }
}