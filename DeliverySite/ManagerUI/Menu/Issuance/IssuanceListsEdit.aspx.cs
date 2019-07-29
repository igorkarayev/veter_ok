using Delivery.BLL.Helpers;
using Delivery.BLL.StaticMethods;
using Delivery.DAL.DataBaseObjects;
using DeliverySite.Resources;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Delivery.ManagerUI.Menu.Issuance
{
    public partial class IssuanceListsEdit : ManagerBasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            bSubmit.Click += bntCreate_Click;
            DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Page.Request.Params["id"]))
            {
                Page.Title = PagesTitles.ManagerIssuanceListsEditTitle.Replace("{0}", Page.Request.Params["id"]) + BackendHelper.TagToValue("page_title_part");
            }
            else
            {
                Page.Title = PagesTitles.ManagerIssuanceListsCreateTitle + BackendHelper.TagToValue("page_title_part");
            }
            OtherMethods.ActiveRightMenuStyleChanche("hlIssuance", this.Page);
            OtherMethods.ActiveRightMenuStyleChanche("hlIssuanceListsEdit", this.Page);

            #region Блок доступа к странице
            var userInSession = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());
            if (currentRole.PageIssuanceListsEdit != 1)
            {
                Response.Redirect("~/Error.aspx?id=1");
            }
            #endregion

            if (Page.Request.Params["id"] != null)
            {
                var issuancelists = new IssuanceLists() { ID = Convert.ToInt32(Page.Request.Params["id"]) };
                issuancelists.GetById();
                if (!IsPostBack)
                {
                    tbComment.Text = issuancelists.Comment;
                    tbIssuanceDate.Text = Convert.ToDateTime(issuancelists.IssuanceDate).ToString("dd-MM-yyyy");
                    tbUID.Text = issuancelists.UserID.ToString();
                }
                lblIssuanceList.Text = "Редактирование рассчетного листа";
            }
        }

        public void bntCreate_Click(Object sender, EventArgs e)
        {
            var id = Page.Request.Params["id"];
            var user = new Users() { ID = Convert.ToInt32(tbUID.Text) };
            user.GetById();
            if (String.IsNullOrEmpty(user.Name))
            {
                lblError.Text = "Пользователя с таким UID не существует!";
                return;
            }

            var issuancelists = new IssuanceLists()
            {
                Comment = tbComment.Text,
                IssuanceDate = Convert.ToDateTime(tbIssuanceDate.Text),
                UserID = Convert.ToInt32(tbUID.Text),
                IssuanceListsStatusID = 1
            };
            if (id == null)
            {
                issuancelists.Create();
            }
            else
            {
                issuancelists.ID = Convert.ToInt32(id);
                issuancelists.Update();
            }
            Page.Response.Redirect("~/ManagerUI/Menu/Issuance/IssuanceListsView.aspx");
        }
    }
}