using Delivery.BLL.Helpers;
using Delivery.BLL.StaticMethods;
using Delivery.DAL.DataBaseObjects;
using DeliverySite.Resources;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Delivery.ManagerUI.Menu.Settings
{
    public partial class BackendEdit : ManagerBasePage
    {
        protected string ButtonText { get; set; }

        protected void Page_Init(object sender, EventArgs e)
        {
            btnCreate.Click += bntCreate_Click;
            DataBind();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = PagesTitles.ManagerBackendEdit + BackendHelper.TagToValue("page_title_part");
            OtherMethods.ActiveRightMenuStyleChanche("hlBackend", this.Page);
            OtherMethods.ActiveRightMenuStyleChanche("hlSettings", this.Page);

            #region Блок доступа к странице
            var userInSession = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());
            if (currentRole.PageBackendEdit != 1)
            {
                Response.Redirect("~/Error.aspx?id=1");
            }
            #endregion

            var backend = new Backend { ID = Convert.ToInt32(Page.Request.Params["id"]) };
            backend.GetById();
            if (!IsPostBack)
            {
                lblTag.Text = backend.Tag;
                lblDescription.Text = backend.Description;
                tbValue.Text = backend.Value;
                var changeDate = OtherMethods.DateConvert(backend.ChangeDate.ToString());
                if (!String.IsNullOrEmpty(changeDate))
                {
                    pnlChangeDate.Visible = true;
                    lblChangeDate.Text = changeDate;
                }
            }

        }

        public void bntCreate_Click(Object sender, EventArgs e)
        {
            var id = Page.Request.Params["id"];
            var backend = new Backend { Value = tbValue.Text };
            if (id == null)
            {
                backend.Create();
            }
            else
            {
                backend.ID = Convert.ToInt32(id);
                backend.ChangeDate = DateTime.Now;
                backend.Update();
            }

            //загружаем backend в оперативную память
            var backendList = new Backend();
            Application["BackendList"] = backendList.GetAllItemsToList();
            Page.Response.Redirect(String.Format("~/ManagerUI/Menu/Settings/BackendView.aspx?tag={0}&value={1}&description={2}", Page.Request.Params["tag"], Page.Request.Params["value"], Page.Request.Params["description"]));
        }
    }
}