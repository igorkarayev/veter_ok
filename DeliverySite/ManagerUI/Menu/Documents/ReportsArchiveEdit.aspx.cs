using Delivery.BLL.Helpers;
using Delivery.BLL.StaticMethods;
using Delivery.DAL.DataBaseObjects;
using DeliverySite.Resources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Delivery.ManagerUI.Menu.Documents
{
    public partial class ReportsArchiveEdit : ManagerBasePage
    {
        protected string ReportID { get; set; }
        protected string AppKey { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = PagesTitles.ManagerReportsEditTitle + BackendHelper.TagToValue("page_title_part");
            OtherMethods.ActiveRightMenuStyleChanche("hlDocuments", Page);
            OtherMethods.ActiveRightMenuStyleChanche("hlReportsArchive", Page);

            #region Блок доступа к странице

            var userInSession = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());
            if (currentRole.PageReportsEdit != 1)
            {
                Response.Redirect("~/Error.aspx?id=1");
            }

            #endregion

            if (Page.Request.Params["id"] != null)
            {
                var report = new Reports { ID = Convert.ToInt32(Page.Request.Params["id"]) };
                report.GetById();

                var fileStream = new FileStream(Server.MapPath("~/ReportsArch/" + report.FileName), FileMode.Open);
                var fileSize = fileStream.Length;

                var buffer = new byte[(int)fileSize];
                fileStream.Read(buffer, 0, (int)fileSize);
                fileStream.Close();
                var enc = new UTF8Encoding();
                tbReportEdit.Text = enc.GetString(buffer);
                ReportID = Page.Request.Params["id"];
                AppKey = Globals.Settings.AppServiceSecureKey;
            }
        }
    }
}