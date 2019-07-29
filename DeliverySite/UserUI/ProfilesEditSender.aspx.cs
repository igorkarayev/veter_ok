using Delivery.BLL;
using Delivery.BLL.Helpers;
using Delivery.BLL.StaticMethods;
using Delivery.DAL;
using Delivery.DAL.DataBaseObjects;
using DeliverySite.Resources;
using Delivery.WebServices.Objects;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Delivery.UserUI;

namespace DeliverySite.UserUI
{
    public partial class ProfilesEditSender : UserBasePage
    {
        protected string userID { get; set; }
        protected void Page_Init(object sender, EventArgs e)
        {
            btnCreate.Click += btnCreate_Click;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var user = (Users)Session["userinsession"];
            userID = user.ID.ToString() == "225" ? "1" : user.ID.ToString();
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            var dm = new DataManager();
            //var dsw = dm.QueryWithReturnDataSet(String.Format("SELECT * FROM `senderprofiles` S WHERE S.`ProfileID` IN (SELECT `ID` FROM `usersprofiles` WHERE `UserID` = {0})", userID));
            var dsw = dm.QueryWithReturnDataSet(String.Format("SELECT * FROM `senderprofiles` S WHERE S.`ProfileID` = {0}", UserID));            
            lvAllProfiles.DataSource = dsw;
            lvAllProfiles.DataBind();

            foreach (DataRow row in dsw.Tables[0].Rows)
            {
                var c  = Convert.ToInt32(row["ID"].ToString());
            }
        }

        public void lbDelete_Click(Object sender, EventArgs e)
        {            
            var lb = (LinkButton)sender;
            var profile = new UsersProfiles();
            var ID = Convert.ToInt32(lb.CommandArgument);

            SenderProfiles senderProfiles = new SenderProfiles();
            senderProfiles.Delete(ID);
        }

        public void lbEdit_Click(Object sender, EventArgs e)
        {
            var lb = (LinkButton)sender;
            var profile = new UsersProfiles();
            var ID = Convert.ToInt32(lb.CommandArgument);

            Response.Redirect(String.Format("~/UserUI/ProfileEditSender.aspx?id={0}", ID));
        }

        public void btnCreate_Click(Object sender, EventArgs e)
        {
            Response.Redirect("~/UserUI/ProfileEditSender.aspx");
        }
    }
}