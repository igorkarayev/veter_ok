using Delivery.BLL.Helpers;
using Delivery.BLL.StaticMethods;
using Delivery.DAL.DataBaseObjects;
using DeliverySite.Resources;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Delivery.ManagerUI.Menu.Settings
{
    public partial class ChangePasswords : ManagerBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = PagesTitles.ManagerChangePassword + BackendHelper.TagToValue("page_title_part");
            OtherMethods.ActiveRightMenuStyleChanche("hlChangePasswords", this.Page);
            OtherMethods.ActiveRightMenuStyleChanche("hlSettings", this.Page);

            #region Блок доступа к странице
            var userInSession = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());
            if (currentRole.PageChangePasswords != 1)
            {
                Response.Redirect("~/Error.aspx?id=1");
            }
            #endregion

            if (Page.Request.Params["uid"] != null)
            {
                tbUID.Text = Page.Request.Params["uid"];
            }
        }

        protected void btnChange_OnClick(Object sender, EventArgs e)
        {
            var userInSession = (Users)Session["userinsession"];
            var user = new Users { ID = Convert.ToInt32(tbUID.Text) };
            user.GetById();

            if (userInSession.Role != Users.Roles.SuperAdmin.ToString() && (user.Role == Users.Roles.Admin.ToString() || user.Role == Users.Roles.SuperAdmin.ToString()) && user.ID != userInSession.ID)
            {
                lblError.Text = "Изменять пароль других администраторов может только администратор+!";
                errorDiv.CssClass = "loginError";
                return;
            }


            if (!string.IsNullOrEmpty(user.Password))
            {
                user.Password = OtherMethods.HashPassword(tbNewPassword.Text);
                user.Update(userInSession.ID, OtherMethods.GetIPAddress(), "ChangePasswords");
                lblError.Text = "Обновлен пароль пользователя: " + user.Name + " " + user.Family + " (" + user.Email + ")";
                errorDiv.CssClass = "loginNotError";
                if (user.Email.Contains("@"))
                {
                    EmailMethods.MailSend("Ваш новый пароль", "Ваш новый пароль: " + tbNewPassword.Text, user.Email);
                }
            }
            else
            {
                lblError.Text = "Пользователь не найден!";
                errorDiv.CssClass = "loginError";
            }
        }
    }
}