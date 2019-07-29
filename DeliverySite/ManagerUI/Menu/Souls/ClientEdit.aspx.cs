using ExcelLibrary.SpreadSheet;
using Delivery.BLL;
using Delivery.BLL.Helpers;
using Delivery.BLL.StaticMethods;
using Delivery.DAL;
using Delivery.DAL.DataBaseObjects;
using DeliverySite.Resources;
using Delivery.WebServices.Objects;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Delivery.ManagerUI.Menu.Souls
{
    public partial class ClientEdit : ManagerBasePage
    {

        protected void Page_Init(object sender, EventArgs e)
        {
            btnCreate.Click += bntCreate_Click;
            btnAllowApi.Click += btnAllowApi_Click;
            btnDisallowApi.Click += btnDisallowApi_Click;
            btnBlock.Click += btnBlock_Click;
            btnActivation.Click += btnActivate_Click;
            btnDeleteClient.Click += btnDeleteClient_Click;
            btnSendPrice.Click += btnSendPrice_Click;
            DataBind();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = PagesTitles.ManagerClientEditTitle + BackendHelper.TagToValue("page_title_part");
            OtherMethods.ActiveRightMenuStyleChanche("hlSouls", this.Page);
            OtherMethods.ActiveRightMenuStyleChanche("hlClients", this.Page);

            #region Блок доступа к странице
            var userInSession = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());
            if (currentRole.PageClientsView != 1)
            {
                Response.Redirect("~/Error.aspx?id=1");
            }
            #endregion

            #region блок всех текстбоксов (запрет редактирования) в начале
            if (currentRole.PageClientsEdit != 1)
            {
                DisableControls(Page);
            }
            #endregion

            if (Session["flash:now"] != null && Session["flash:now"].ToString() != String.Empty)
            {
                lblStatus.Text = Session["flash:now"].ToString();
                Session["flash:now"] = String.Empty;
            }

            if (currentRole.PageChangePasswords != 1 || Page.Request.Params["id"] == null)
            {
                divChangePassword.Visible = false;
            }

            if (!IsPostBack)
            {
                var dm = new DataManager();
                var dataSet = dm.QueryWithReturnDataSet("select * from `users` WHERE (role = 'Manager') and Status = 2 ORDER BY Family ASC, Name ASC;");
                dataSet.Tables[0].Columns.Add("FIO", typeof(string), "Family + ' ' + Name");
                ddlManager.DataSource = dataSet;
                ddlManager.DataTextField = "FIO";
                ddlManager.DataValueField = "ID";
                ddlManager.DataBind();
                ddlManager.Items.Insert(0, new ListItem("Не назначен", "0"));

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
            }
            if (Page.Request.Params["id"] != null)
            {
                var user = new Users { ID = Convert.ToInt32(Page.Request.Params["id"]) };
                user.GetById();

                if (!IsPostBack)
                {
                    cbIsSpecialClient.Checked = user.SpecialClient != 0;
                    cbIsRedClient.Checked = user.RedClient != 0;
                    lblID.Text = user.ID.ToString();
                    lblRegistartionDate.Text = OtherMethods.DateConvert(user.CreateDate.ToString());
                    tbName.Text = user.Name;
                    tbFamily.Text = user.Family;
                    tbPhone.Text = user.Phone;
                    lblApi.Text = string.IsNullOrEmpty(user.ApiKey) ? "Нет" : "Есть";
                    lblStatusClient.Text = UsersHelper.UserStatusToText(Convert.ToInt32(user.Status));
                    lblNote.Text = WebUtility.HtmlDecode(user.Note);
                    imgGravatar.ImageUrl = Gravatar.GravatarImageLink(user.Email, "180");
                    hlSales.Text = string.Format("Скидка: {0}%", user.Discount.ToString());
                    lblEmail.Text = user.Email;
                    lblLogin.Text = user.Login;
                    tbContactDate.Text = OtherMethods.DateConvert(user.ContactDate.ToString());
                    ddlManager.SelectedValue = user.ManagerID.ToString();
                    ddlSalesManager.SelectedValue = user.SalesManagerID.ToString();
                    ddlStatusStady.SelectedValue = user.StatusStady.ToString();
                    hlChangePassword.NavigateUrl = String.Format("~/ManagerUI/Menu/Settings/ChangePasswords.aspx?uid={0}", user.ID);
                    hlAddSection.NavigateUrl = "~/ManagerUI/Menu/Souls/UserToCategoryView.aspx?id=" + user.ID;
                    hlSales.NavigateUrl = "~/ManagerUI/Menu/Settings/UsersDiscountView.aspx?uid=" + user.ID;
                }

                ddlManager.Enabled = currentRole.ActionAddManagerToUser == 1;
                ddlSalesManager.Enabled = currentRole.ActionAddSalesManagerToUser == 1;
                ddlStatusStady.Enabled = user.Status != 2;
                hlAddSection.Enabled = currentRole.ActionCategoryAssignToUser == 1;
                hlChangePassword.Enabled = currentRole.PageChangePasswords == 1;
                hlSales.Enabled = currentRole.PageUserDiscountView == 1;
                btnActivation.Enabled = currentRole.ActionClientActivateBlock == 1;
                btnBlock.Enabled = currentRole.ActionClientActivateBlock == 1;
                btnDisallowApi.Enabled = currentRole.AllowBlockingAddApiAccess == 1;
                btnAllowApi.Enabled = currentRole.AllowBlockingAddApiAccess == 1;
                btnSendPrice.Enabled = currentRole.PageSendComProp == 1;

                if (currentRole.AllowBlockingAddApiAccess != 1)
                {
                    trApiOpenNotif.Visible = false;
                }

                if (currentRole.ActionClientsDelete != 1)
                {
                    btnDeleteClient.Enabled = false;
                }

                if (user.Status == 2)
                {
                    btnBlock.Visible = true;
                }

                if (user.Status == 3)
                {
                    btnActivation.Visible = true;
                }

                if (user.Status == 1)
                {
                    btnActivation.Visible = btnBlock.Visible = true;
                }

                if (user.AllowApi != null && user.AllowApi == 1)
                {
                    btnDisallowApi.Visible = true;
                }
                else
                {
                    btnAllowApi.Visible = true;
                }

                var usersWhoCanMarkClientAsRed = BackendHelper.TagToValue("users_can_mark_client_as_red");
                cbIsRedClient.Enabled = usersWhoCanMarkClientAsRed.Split(new[] { ',' }).Any(p => p.Trim().Contains(userInSession.ID.ToString()));

                /* Блокировка действий над одноразовым пользователем */
                if (user.ID == 1)
                {
                    btnAllowApi.Visible = false;
                    btnBlock.Visible = false;
                    btnDeleteClient.Visible = false;
                    btnDisallowApi.Visible = false;
                    btnSendPrice.Visible = false;
                    hlAddSection.Visible = false;
                    hlChangePassword.Visible = false;
                    hlSales.Visible = false;
                    btnCreate.Visible = false;
                }
            }
        }

        public void bntCreate_Click(Object sender, EventArgs e)
        {
            var id = Page.Request.Params["id"];
            var isSpecialClient = cbIsSpecialClient.Checked ? 1 : 0;
            var isRedClient = cbIsRedClient.Checked ? 1 : 0;

            var userInSession = (Users)Session["userinsession"];

            var user = new Users
            {
                ID = Convert.ToInt32(id)
            };
            user.GetById();
            user.Name = tbName.Text;
            user.Family = tbFamily.Text;
            user.Phone = tbPhone.Text;
            user.SpecialClient = isSpecialClient;
            user.ContactDate = string.IsNullOrEmpty(tbContactDate.Text) ? Convert.ToDateTime("01.01.0001 0:00:00") : Convert.ToDateTime(tbContactDate.Text);
            user.RedClient = isRedClient;
            user.ManagerID = Convert.ToInt32(ddlManager.SelectedValue);
            user.StatusStady = Convert.ToInt32(ddlStatusStady.SelectedValue);
            user.SalesManagerID = Convert.ToInt32(ddlSalesManager.SelectedValue);

            user.Update(userInSession.ID, OtherMethods.GetIPAddress(), "ClientEdit");
            Page.Response.Redirect(Request.RawUrl);
        }

        public void btnAllowApi_Click(Object sender, EventArgs e)
        {
            var id = Page.Request.Params["id"];
            var userInSession = (Users)Session["userinsession"];
            var user = new Users
            {
                ID = Convert.ToInt32(id),
                AllowApi = 1,
                ApiKey = OtherMethods.HashPassword(DateTime.Now.ToString("yyyy MMMM dd HH:mm:ss"))
            };
            user.Update(userInSession.ID, OtherMethods.GetIPAddress(), "ClientEdit");
            Page.Response.Redirect(Request.RawUrl);
        }

        public void btnDisallowApi_Click(Object sender, EventArgs e)
        {
            var id = Page.Request.Params["id"];
            var userInSession = (Users)Session["userinsession"];
            var user = new Users
            {
                ID = Convert.ToInt32(id),
                AllowApi = 0,
                ApiKey = String.Empty
            };
            user.Update(userInSession.ID, OtherMethods.GetIPAddress(), "ClientEdit");
            Page.Response.Redirect(Request.RawUrl);
        }
        public void btnBlock_Click(Object sender, EventArgs e)
        {
            var id = Page.Request.Params["id"];
            var userInSession = (Users)Session["userinsession"];
            var user = new Users
            {
                ID = Convert.ToInt32(id),
                Status = 3
            };
            user.Update(userInSession.ID, OtherMethods.GetIPAddress(), "ClientEdit");
            user.GetById();
            var emailNotification = new EmailNotifications { Name = "account_blocked" };
            emailNotification.GetByName();
            EmailMethods.MailSendHTML(
                emailNotification.Title,
                string.Format(emailNotification.Body,
                    BackendHelper.TagToValue("current_app_address"),
                    BackendHelper.TagToValue("not_official_name"),
                    BackendHelper.TagToValue("main_phones")),
                user.Email,
                true);
            Page.Response.Redirect(Request.RawUrl);
        }

        public void btnActivate_Click(Object sender, EventArgs e)
        {
            var id = Page.Request.Params["id"];
            var regUser = new Users {ID = Convert.ToInt32(id) };
            var userInSession = (Users)Session["userinsession"];
            //var newPassword = OtherMethods.CreateUniqId(DateTime.Now.ToString("yyMdHms"));
            var user = new Users
            {
                ID = Convert.ToInt32(id),
                Status = 2,
                ActivatedDate = DateTime.Now,
                Password = regUser.Password
            };
            user.Update(userInSession.ID, OtherMethods.GetIPAddress(), "ClientEdit");
            user.GetById();
            var emailNotification = new EmailNotifications { Name = "account_activated" };
            emailNotification.GetByName();
            EmailMethods.MailSendHTML(
                emailNotification.Title,
                string.Format(emailNotification.Body,
                              user.Login,
                              regUser.Password,
                              BackendHelper.TagToValue("official_name"),
                              BackendHelper.TagToValue("current_app_address"),
                              BackendHelper.TagToValue("not_official_name")),
                user.Email, true);
            Page.Response.Redirect(Request.RawUrl);
        }

        public void btnDeleteClient_Click(Object sender, EventArgs e)
        {
            var id = Page.Request.Params["id"];
            var userInSession = (Users)Session["userinsession"];
            var client = new Users();
            client.Delete(Convert.ToInt32(id), userInSession.ID, OtherMethods.GetIPAddress(), "ClientEdit");
            Page.Response.Redirect("~/ManagerUI/Menu/Souls/ClientsView.aspx");
        }

        public void btnSendPrice_Click(Object sender, EventArgs e)
        {
            var id = Page.Request.Params["id"];
            //читаем старый файл
            if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/Temp/SendedPrices/")))
            {
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Temp/SendedPrices/"));
            }
            var fileName = "Прайс " + BackendHelper.TagToValue("official_name") + " (" + OtherMethods.CreateUniqId(id + DateTime.Now.ToString("hh:mm:ssttzz")) + ").xls";
            var dataFile = HttpContext.Current.Server.MapPath("~/Temp/SendedPrices/" + fileName);
            var dm = new DataManager();

            //получаем данные отправителя прайса из сотрудников
            var userInSession = (Users)Session["userinsession"];
            var emailSenderData = new Users() { ID = userInSession.ID };
            emailSenderData.GetById();

            var ds = new DataSet();
            var ifUserHaveAssignSection = dm.QueryWithReturnDataSet(String.Format("SELECT * FROM `userstocategory` WHERE `UserID` = {0}", id));
            if (ifUserHaveAssignSection.Tables[0].Rows.Count == 0)
            {
                ds = dm.QueryWithReturnDataSet(
                    "SELECT Name as 'Наименование', " +
                    "MarginCoefficient as 'Юр. лицо', " +
                    "MarginCoefficient as 'Физ. лицо', " +
                    "MarginCoefficient as 'Физ. лица в скид.' " +
                    "FROM `titles` ORDER BY `Name`");
            }
            else
            {
                ds = dm.QueryWithReturnDataSet(String.Format(
                    "SELECT C.Name as 'Наименование', " +
                    "C.MarginCoefficient as 'Юр. лицо', " +
                    "C.MarginCoefficient as 'Физ. лицо', " +
                    "C.MarginCoefficient as 'Физ. лица в скид.' " +
                    "FROM `titles` C WHERE C.`CategoryID` IN (SELECT `CategoryID` FROM `userstocategory` WHERE `UserID` = {0})  ORDER BY C.`Name`", id));
            }
            var goodList = new List<GoodsFromAPI>();
            var goods = new GoodsFromAPI();
            var tableToSend = "<table class='table' style='width: 100%; border-collapse: collapse;'><tr>" +
                              "<th style='border: 1px solid #666666; padding: 3px 6px; text-align: left;'>Наименование</th>" +
                              "<th style='border: 1px solid #666666; padding: 3px 6px;'>ЮЛ/ТТН</th>" +
                              "<th style='border: 1px solid #666666; padding: 3px 6px;'>ФЛ/Регион</th>" +
                              "<th style='border: 1px solid #666666; padding: 3px 6px;'>ФИЗ. Л. регион/область</th>" +
                              "</tr>";
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                goodList.Clear();
                goods.Description = row[0].ToString();
                goods.Number = 1;
                goods.IsAdditional = 0;
                goods.WithoutAkciza = 0;
                goods.Coefficient = 0;
                goodList.Add(goods);


                row[1] = Calculator.Calculate(goodList, 187, Convert.ToInt32(id), null, "2"); //за обычный город берем Петриков
                row[2] = Calculator.Calculate(goodList, 187, Convert.ToInt32(id), null, "1"); //за обычный город берем Петриков
                row[3] = Calculator.Calculate(goodList, 16, Convert.ToInt32(id), null, "1"); //за скидочный город берем Гродно
                tableToSend += string.Format("<tr>" +
                                             "<td style='border: 1px solid #666666; padding: 3px 6px; text-align: left;'>{0}</td>" +
                                             "<td style='border: 1px solid #666666; padding: 3px 6px;'>{1}</td>" +
                                             "<td style='border: 1px solid #666666; padding: 3px 6px;'>{2}</td>" +
                                             "<td style='border: 1px solid #666666; padding: 3px 6px;'>{3}</td>" +
                                             "</tr>", row[0], MoneyMethods.MoneySeparator(row[1].ToString()), MoneyMethods.MoneySeparator(row[2].ToString()), MoneyMethods.MoneySeparator(row[3].ToString()));
            }
            tableToSend += "</table>";
            DataRow emptyRow = ds.Tables[0].NewRow();
            DataRow newRow = ds.Tables[0].NewRow();
            newRow[0] = String.Format("Наш {5}: {1} {2}, тел.: {0}, email: {3}, skype: {4}", emailSenderData.PhoneWorkOne, emailSenderData.Name, emailSenderData.Family, emailSenderData.Email, emailSenderData.Skype, userInSession.RussRole.ToLower());
            ds.Tables[0].Rows.Add(emptyRow);
            ds.Tables[0].Rows.Add(newRow);
            ds.Tables[0].TableName = "Прайс " + BackendHelper.TagToValue("not_official_name");

            Workbook book = Workbook.Load(HttpContext.Current.Server.MapPath("~/" + BackendHelper.TagToValue("comprop_tpl_file_name")));
            AddDataTableToWorkBook(ds.Tables[0], ref book);
            var user = new Users() { ID = Convert.ToInt32(id) };
            user.GetById();
            book.Save(dataFile);
            var emailNotification = new EmailNotifications { Name = "comprop_message" };
            emailNotification.GetByName();
            EmailMethods.MailSendHTML(
                string.Format(emailNotification.Title, BackendHelper.TagToValue("not_official_name")),
                string.Format(emailNotification.Body,
                                emailSenderData.PhoneWorkOne,
                                emailSenderData.Name,
                                emailSenderData.Family,
                                emailSenderData.Email,
                                emailSenderData.Skype,
                                tableToSend,
                                userInSession.RussRole.ToLower(),
                                BackendHelper.TagToValue("official_name"),
                                BackendHelper.TagToValue("current_app_address"),
                                BackendHelper.TagToValue("not_official_name")),
                user.Email,
                dataFile,
                true);

            Session["flash:now"] = "<span style='color: green; font-size: bold'>Прайс отправлен!</span>";
            Page.Response.Redirect("~/ManagerUI/Menu/Souls/ClientEdit.aspx?id=" + id);
        }


        public static void AddDataTableToWorkBook(DataTable dataTable, ref Workbook workBook)
        {
            workBook.Worksheets[0].Cells.ColumnWidth[0] = 10000;
            workBook.Worksheets[0].Cells.ColumnWidth[1] = 2500;
            workBook.Worksheets[0].Cells.ColumnWidth[2] = 2500;
            workBook.Worksheets[0].Cells.ColumnWidth[3] = 2500;
            for (int i = 0; i < dataTable.Columns.Count; i++)
            {
                // Add column header
                workBook.Worksheets[0].Cells[7, i] = new Cell(dataTable.Columns[i].ColumnName);

                // Populate row data
                for (int j = 0; j < dataTable.Rows.Count; j++)
                //Если нулевые значения, заменяем на пустые строки
                {
                    workBook.Worksheets[0].Cells[j + 8, i] = new Cell(dataTable.Rows[j][i] == DBNull.Value ? "" : dataTable.Rows[j][i]);
                }
            }
        }

        protected void DisableControls(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                if (c is TextBox)
                {
                    ((TextBox)(c)).Enabled = false;
                }
                if (c.Controls.Count > 0)
                {
                    DisableControls(c);
                }
            }
        }
    }
}