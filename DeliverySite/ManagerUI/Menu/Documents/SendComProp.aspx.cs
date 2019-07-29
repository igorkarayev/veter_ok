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
using System.Web;
using System.Web.UI.WebControls;

namespace Delivery.ManagerUI.Menu.Documents
{
    public class ModelSortingCosts
    {
        public string name { get; set; }
        public string costDD { get; set; }
        public string costSD { get; set; }
        public string costMinsk { get; set; }
    }

    public partial class SendComProp : ManagerBasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            btnSendComProp.Click += btnSendComProp_Click;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = PagesTitles.ManagerSendComProp + BackendHelper.TagToValue("page_title_part");
            OtherMethods.ActiveRightMenuStyleChanche("hlDocuments", this.Page);
            OtherMethods.ActiveRightMenuStyleChanche("hlSendComProp", this.Page);

            #region Блок доступа к странице
            var userInSession = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());
            if (currentRole.PageSendComProp != 1)
            {
                Response.Redirect("~/Error.aspx?id=1");
            }
            #endregion

            if (Session["flash:now"] != null && Session["flash:now"].ToString() != String.Empty)
            {
                lblStatus.Text = Session["flash:now"].ToString();
                Session["flash:now"] = String.Empty;
            }

            if (!IsPostBack)
            {
                var dm = new DataManager();
                var category = dm.QueryWithReturnDataSet("SELECT * FROM `category` ORDER BY `Name` ASC");
                lvAllCategory.DataSource = category;
                lvAllCategory.DataBind();
            }
        }

        public void btnSendComProp_Click(Object sender, EventArgs e)
        {
            //читаем старый файл
            if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/Temp/SendedPrices/")))
            {
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Temp/SendedPrices/"));
            }
            var fileName = "Прайс " + BackendHelper.TagToValue("not_official_name") + " (" + OtherMethods.CreateUniqId(tbEmail.Text + DateTime.Now.ToString("hh:mm:ssttzz")) + ").xls";
            var dataFile = HttpContext.Current.Server.MapPath("~/Temp/SendedPrices/" + fileName);
            var dm = new DataManager();

            //получаем данные отправителя прайса из сотрудников
            var userInSession = (Users)Session["userinsession"];
            var emailSenderData = new Users() { ID = userInSession.ID };
            emailSenderData.GetById();

            var categoryIds = string.Empty;
            var haveCheckedCategory = false;
            foreach (var items in lvAllCategory.Items)
            {
                var hfCategoryId = (HiddenField)items.FindControl("hfCategoryId");
                var cbCategory = (CheckBox)items.FindControl("cbCategory");
                if (cbCategory.Checked)
                {
                    categoryIds += string.Format("{0},", hfCategoryId.Value);
                    haveCheckedCategory = true;
                }
            }

            DataSet ds;
            if (!haveCheckedCategory)
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
                categoryIds = categoryIds.Remove(categoryIds.Length - 1, 1);
                ds = dm.QueryWithReturnDataSet(String.Format(
                    "SELECT C.Name as 'Наименование', " +
                    "C.MarginCoefficient as 'Юр. лицо', " +
                    "C.MarginCoefficient as 'Физ. лицо', " +
                    "C.MarginCoefficient as 'Физ. лица в скид.' " +
                    "FROM `titles` C WHERE C.`CategoryID` IN ({0})  ORDER BY C.`Name`", categoryIds));
            }
            var goodList = new List<GoodsFromAPI>();
            var goods = new GoodsFromAPI();
            var tableToSend = "<table class='table' style='width: 100%; border-collapse: collapse;'><tr>" +
                              "<th style='border: 1px solid #666666; padding: 3px 6px; text-align: left;'>Наименование</th>" +
                              "<th style='border: 1px solid #666666; padding: 3px 6px;'>По РБ</th>" +
                              "<th style='border: 1px solid #666666; padding: 3px 6px;'>По Минску</th>" +                              
                              "</tr>";

            List<ModelSortingCosts> table_list = new List<ModelSortingCosts>();
            DataTable sortedDS = new DataTable();
            sortedDS.Columns.Add(string.Format("Наименование", 0), typeof(string));
            sortedDS.Columns.Add(string.Format("Дверь/Дверь", 1), typeof(string));
            sortedDS.Columns.Add(string.Format("Склад/Дверь", 2), typeof(string));
            sortedDS.Columns.Add(string.Format("По Минску", 3), typeof(string));

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                ModelSortingCosts row_list = new ModelSortingCosts();

                goodList.Clear();
                goods.Description = row[0].ToString();
                goods.Number = 1;
                goods.IsAdditional = 0;
                goods.WithoutAkciza = 0;
                goods.Coefficient = 0;
                goodList.Add(goods);

                row_list.name = row[0].ToString();
                row_list.costDD = Calculator.Calculate(goodList, 187, 1, 239, "1", null, null, true);
                row_list.costSD = Calculator.Calculate(goodList, 187, 1, 239, "1");
                row_list.costMinsk = Calculator.Calculate(goodList, 11, 1, 239, "1");

                table_list.Add(row_list);
            }
            table_list = table_list.OrderBy(x => x.costSD).ToList();
            foreach(ModelSortingCosts model in table_list)
            {
                DataRow dsRow = sortedDS.NewRow();
                dsRow[0] = model.name;
                dsRow[1] = model.costDD;
                dsRow[2] = model.costSD;
                dsRow[3] = model.costMinsk;
                sortedDS.Rows.Add(dsRow);
            }

            foreach (ModelSortingCosts row in table_list)
            {
                tableToSend += string.Format("<tr>" +
                                             "<td style='border: 1px solid #666666; padding: 3px 6px; text-align: left;'>{0}</td>" +
                                             "<td style='border: 1px solid #666666; padding: 3px 6px;'>{1}</td>" +
                                             "<td style='border: 1px solid #666666; padding: 3px 6px;'>{2}</td>" +                                             
                                             "</tr>", row.name, MoneyMethods.MoneySeparator(row.costDD.ToString()), MoneyMethods.MoneySeparator(row.costSD.ToString()), MoneyMethods.MoneySeparator(row.costMinsk.ToString()));
            }
            tableToSend += "</table>";
            DataRow emptyRow = sortedDS.NewRow();
            DataRow newRow = sortedDS.NewRow();
            newRow[0] = String.Format("тел.: {0}, email: {3}, skype: {4}", emailSenderData.PhoneWorkOne, emailSenderData.Name, emailSenderData.Family, emailSenderData.Email, emailSenderData.Skype, userInSession.RussRole.ToLower());
            sortedDS.Rows.Add(emptyRow);
            sortedDS.Rows.Add(newRow);
            sortedDS.TableName = "Прайс" + BackendHelper.TagToValue("not_official_name");

            Workbook book = Workbook.Load(HttpContext.Current.Server.MapPath("~/OtherFiles/" + BackendHelper.TagToValue("comprop_tpl_file_name")));
            AddDataTableToWorkBook(sortedDS, ref book);
            book.Save(dataFile);
            var emailNotification = new EmailNotifications { Name = "comprop_guest_message" };
            emailNotification.GetByName();
            EmailMethods.MailSendHTML(tbSubject.Text, string.Format(emailNotification.Body,
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
                                    tbEmail.Text,
                                    dataFile,
                                    false);

            Session["flash:now"] = "<span style='color: white;'>Комерческое предложение успешно отправлено на e-mail <b>" + tbEmail.Text + "</b>!</span>";
            Page.Response.Redirect("~/ManagerUI/Menu/Documents/SendComProp.aspx");
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
    }
}