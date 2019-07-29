using Delivery.BLL.Helpers;
using Delivery.BLL.StaticMethods;
using Delivery.DAL;
using Delivery.DAL.DataBaseObjects;
using DeliverySite.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Delivery.ManagerUI.Menu.Souls
{
    public partial class ClientCreate : ManagerBasePage
    {
        #region Property
        public String AppKey { get; set; }
        #endregion

        protected void Page_Init(object sender, EventArgs e)
        {
            btnCreate.Click += bntCreate_Click;
            DataBind();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = PagesTitles.ManagerClientCreateTitle + BackendHelper.TagToValue("page_title_part");
            OtherMethods.ActiveRightMenuStyleChanche("hlSouls", this.Page);
            OtherMethods.ActiveRightMenuStyleChanche("hlClients", this.Page);

            #region Блок доступа к странице
            var userInSession = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());
            if (currentRole.PageClientsCreate != 1)
            {
                Response.Redirect("~/Error.aspx?id=1");
            }
            #endregion

            AppKey = Globals.Settings.AppServiceSecureKey;

            if (Session["flash:now"] != null && Session["flash:now"].ToString() != string.Empty)
            {
                lblStatus.Text = Session["flash:now"].ToString();
                Session["flash:now"] = String.Empty;
            }

            var dm = new DataManager();
            if (!IsPostBack)
            {
                var dataSetForSalesManager = dm.QueryWithReturnDataSet("select * from `users` WHERE (role = 'SalesManager') and Status = 2 ORDER BY Family ASC, Name ASC;");
                dataSetForSalesManager.Tables[0].Columns.Add("FIO", typeof(string), "Family + ' ' + Name");
                ddlSalesManager.DataSource = dataSetForSalesManager;
                ddlSalesManager.DataTextField = "FIO";
                ddlSalesManager.DataValueField = "ID";
                ddlSalesManager.DataBind();
                ddlSalesManager.Items.Insert(0, new ListItem("Не назначен", "0"));

                ddlStatusStady.DataSource = Users.UserStatusesStudy;
                ddlStatusStady.DataTextField = "Value";
                ddlStatusStady.DataValueField = "Key";
                ddlStatusStady.DataBind();

                ddlProfileType.DataSource = UsersProfiles.ProfileType;
                ddlProfileType.DataTextField = "Value";
                ddlProfileType.DataValueField = "Key";
                ddlProfileType.DataBind();
                ddlProfileType.Items.Remove(ddlProfileType.Items.FindByValue("1"));


                ddlCompanyType.DataSource = UsersProfiles.CompanyType;
                ddlCompanyType.DataTextField = "Value";
                ddlCompanyType.DataValueField = "Key";
                ddlCompanyType.DataBind();
            }


            var category = dm.QueryWithReturnDataSet("SELECT * FROM `category` ORDER BY `Name` ASC");
            lvAllCategory.DataSource = category;
            lvAllCategory.DataBind();
        }

        public void bntCreate_Click(object sender, EventArgs e)
        {
            var dm = new DataManager();
            var userRegPosible = UsersHelper.UserLoginEmailRegisteredChecker(tbEmail.Text);
            var emailCorrectly = EmailMethods.EmailChecker(tbEmail.Text);
            var registrationPosible = true;
            if (!userRegPosible)
            {
                lblError.Text = "Пользователь с таким логином либо e-mail'ом уже есть в нашей базе!<br/>";
                registrationPosible = false;
            }

            if (!emailCorrectly)
            {
                lblError.Text = "Введен недопустимый е-mail!<br/>";
                registrationPosible = false;
            }

            if (!registrationPosible)
            {
                return;
            }

            #region Создание клиента

            var userInSession = (Users)Session["userinsession"];
            var user = new Users
            {
                Name = tbName.Text,
                Family = tbFamily.Text,
                Email = tbEmail.Text,
                Login = tbEmail.Text,
                Password = "reg_by_manager",
                Role = Users.Roles.User.ToString(),
                Phone = tbPhone.Text,
                CreateDate = DateTime.Now,
                ContactDate =
                    string.IsNullOrEmpty(tbContactDate.Text)
                        ? Convert.ToDateTime("01.01.0001 0:00:00")
                        : Convert.ToDateTime(tbContactDate.Text),
                StatusStady = Convert.ToInt32(ddlStatusStady.SelectedValue),
                Status = 1, /* not activated */
                SalesManagerID = Convert.ToInt32(ddlSalesManager.SelectedValue),
                Note = WebUtility.HtmlEncode(string.Format(
                                                              "<div class='comment-history-body'>{1}</div>" +
                                                              "<div>" +
                                                                "<span class='comment-history-name'>{0}</span>" +
                                                                "<span class='comment-history-date'>{2}</span>" +
                                                              "</div>",
                    UsersHelper.UserIDToFullName(userInSession.ID.ToString()),
                    tbComment.Text,
                    DateTime.Now.ToString("dd.MM в HH:mm")))
            };
            user.Create();
            #endregion

            var userId = Convert.ToInt32(dm.QueryWithReturnDataSet("SELECT `ID` FROM `users` ORDER BY `ID` DESC LIMIT 1").Tables[0].Rows[0][0]);

            #region Создание профиля
            var profile = new UsersProfiles
            {
                UserID = userId,
                StatusID = 0,
                TypeID = Convert.ToInt32(ddlProfileType.SelectedValue),
                CreateDate = DateTime.Now,
                IsDefault = 1,
                CompanyName = string.Format("{0} «{1}»", ddlCompanyType.SelectedItem, tbCompanyName.Text),
                DirectorPhoneNumber = tbDirectorPhone.Text,
                FirstName = tbDirectorFamily.Text,
                LastName = tbDirectorName.Text,
                ThirdName = tbDirectorPatronymic.Text,
                ContactPersonFIO = tbContactPersonFIO.Text,
            };
            if (!string.IsNullOrEmpty(tbContactPhoneNumbers2.Text))
            {
                profile.ContactPhoneNumbers = tbContactPhoneNumbers.Text + ";" +
                                              tbContactPhoneNumbers2.Text;
            }
            else
            {
                profile.ContactPhoneNumbers = tbContactPhoneNumbers.Text;
            }
            profile.Create();
            #endregion

            #region Добавление клиенту категорий
            foreach (var items in lvAllCategory.Items)
            {
                var hfCategoryId = (HiddenField)items.FindControl("hfCategoryId");
                var cbCategory = (CheckBox)items.FindControl("cbCategory");
                if (cbCategory.Checked)
                {
                    dm.QueryWithoutReturnData(null, string.Format("INSERT IGNORE INTO `userstocategory` (`UserID`, `CategoryID`) VALUES ('{0}', '{1}');", userId, hfCategoryId.Value));
                }
            }
            #endregion

            Page.Response.Redirect("~/ManagerUI/Menu/Souls/ClientsView.aspx");
        }
    }
}