using Delivery.BLL.Helpers;
using Delivery.BLL.StaticMethods;
using Delivery.DAL;
using Delivery.DAL.DataBaseObjects;
using DeliverySite.Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Delivery.ManagerUI.Menu.Documents
{
    public partial class ReportsExport : ManagerBasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            lbGetActivatedUsersEmails.Click += lbGetActivatedUsersEmails_Click;
            lbGetAllUsersEmails.Click += lbGetAllUsersEmails_Click;
            lbGetAllUsersInfo.Click += lbGetAllUsersInfo_Click;
            lbGetAllUsersProfilesInfo.Click += lbGetAllUsersProfilesInfo_Click;
            lbGetRefundsInformation.Click += lbGetRefundsInformation_Click;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = PagesTitles.ManagerReportsExportTitle + BackendHelper.TagToValue("page_title_part");
            OtherMethods.ActiveRightMenuStyleChanche("hlDocuments", this.Page);
            OtherMethods.ActiveRightMenuStyleChanche("hlReportsExport", this.Page);

            #region Блок доступа к странице
            var userInSession = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());
            if (currentRole.PageReportsExport != 1)
            {
                Response.Redirect("~/Error.aspx?id=1");
            }
            #endregion

            if (currentRole.ActionExportAllUsersInfo == 1)
            {
                pnlActionExportAllUsersInfo.Visible = true;
            }

            if (currentRole.ActionExportAllUsersProfilesInfo == 1)
            {
                pnlActionExportAllUsersProfilesInfo.Visible = true;
            }
        }

        public void lbGetActivatedUsersEmails_Click(Object sender, EventArgs e)
        {
            var dm = new DataManager();
            var emails = String.Empty;
            var emailsSet = dm.QueryWithReturnDataSet("select `Email` from `users` WHERE `Status` = 2 and `Role` = 'User';");
            emails = emailsSet.Tables[0].Rows.Cast<DataRow>().Aggregate(emails, (current, item) => current + (item["Email"] + ",\n"));
            const string sGenName = "список_емейлов_активированых_клиентов.txt";
            Response.AddHeader("Content-disposition", "attachment; filename=" + sGenName);
            Response.ContentType = "application/octet-stream";
            Response.Write(emails.Remove(emails.Length - 1, 1));
            Response.End();
        }

        public void lbGetAllUsersEmails_Click(Object sender, EventArgs e)
        {
            var dm = new DataManager();
            var emails = String.Empty;
            var emailsSet = dm.QueryWithReturnDataSet("select `Email` from `users` WHERE `Role` = 'User';");
            emails = emailsSet.Tables[0].Rows.Cast<DataRow>().Aggregate(emails, (current, item) => current + (item["Email"] + ",\n"));
            const string sGenName = "список_емейлов_всех_клиентов.txt";
            Response.AddHeader("Content-disposition", "attachment; filename=" + sGenName);
            Response.ContentType = "application/octet-stream";
            Response.Write(emails.Remove(emails.Length - 1, 1));
            Response.Flush();
            Response.End();
        }

        protected void lbGetAllUsersInfo_Click(object sender, EventArgs e)
        {
            var dm = new DataManager();
            var month = DateTime.Now.Month.ToString();
            if (month.Length == 1) month = String.Format("0{0}", month);
            var date1 = Convert.ToDateTime(String.Format("01-{0}-{1}", month, DateTime.Now.Year)).ToString("yyyy-MM-dd");
            var date2 = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
            var infoSet = dm.QueryWithReturnDataSet("SELECT " +
                                                    "U.`ID` as `UID`, " +
                                                    "U.`Family` as `Фамилия`, " +
                                                    "U.`Name` as `Имя`, " +
                                                    "U.`Phone` as `Телефон`, " +
                                                    "U.`Email` as `Емейл`, " +
                                                    "U.`Login` as `Логин`, " +
                                                    "U.`Status` as `Статус`, " +
                                                    "U.`CreateDate` as `Дата регистрации`, " +
                                                    "U.`Discount` as `Скидка`, " +
                                                    "U.`SpecialClient` as `Особый клиент`, " +
                                                    "U.`IsCourse` as `Может выставлять курсы`, " +
                                                    "U.`Note` as `Заметка`, " +
                                                    "CONCAT(UU.`Family`,\" \", UU.`Name`) as `Ответственный менеджер`, " +
                                                    "CONCAT(UUU.`Family`,\" \", UUU.`Name`) as `Ответств. мен. по прод.`, " +
                                                    "(SELECT COUNT(*) FROM `tickets` T WHERE T.`UserID` = U.ID AND (T.`CreateDate` BETWEEN '" + date1 + "' AND '" + date2 + "' )) as `Кол-во зявок за календ. месяц` " +
                                                    "FROM `users` U " +
                                                    "LEFT JOIN `users` UU ON U.`ManagerID` = UU.`ID` " +
                                                    "LEFT JOIN `users` UUU ON U.`SalesManagerID` = UUU.`ID` " +
                                                    "WHERE U.`Role` = 'User' AND U.`ID` <> 1 AND U.`Status` = 2;");
            const string fileName = "полная_информация_клиентов_с_которыми_работаем";
            ExportMethods.CreateXlsFile(Response, infoSet, fileName, "users");
        }

        protected void lbGetAllUsersProfilesInfo_Click(object sender, EventArgs e)
        {
            var dm = new DataManager();
            var infoSet = dm.QueryWithReturnDataSet("SELECT " +
                                                    "P.`ID` as `PID`, " +
                                                    "P.`UserID` as `UID`, " +
                                                    "CONCAT(U.`Family`, \" \", U.`Name`) as `Клиент`, " +
                                                    "P.`FirstName` as `Фамилия`, " +
                                                    "P.`LastName` as `Имя`, " +
                                                    "P.`ThirdName` as `Отчество`, " +
                                                    "P.`ContactPhoneNumbers` as `Контактный телефон`, " +
                                                    "P.`ContactPersonFIO` as `Контактное лицо`, " +
                                                    "P.`TypeID` as `Тип профиля` , " +
                                                    "P.`StatusID` as `Статус`, " +
                                                    "P.`RejectBlockedMessage` as `Расшифровка статуса` , " +
                                                    "CONCAT(P.`PassportSeria`,\" \", P.`PassportNumber`) as `Серия и номер паспорта`, " +
                                                    "P.`PassportData` as `Выда кем`," +
                                                    "P.`PassportDate` as `Выдан когда`, " +
                                                    "P.`Address` as `Адрес проживания`, " +
                                                    "P.`CompanyName` as `Имя компании`, " +
                                                    "P.`CompanyAddress` as `Юр. адрес компании`, " +
                                                    "P.`PostAddress` as `Контактный адрес компании`," +
                                                    "P.`RasShet` as `Расчетный счет`, " +
                                                    "P.`UNP` as `УНП`, " +
                                                    "P.`BankName` as `Имя банка`, " +
                                                    "P.`BankCode` as `Код банка`, " +
                                                    "P.`BankAddress` as `Адрес банка`, " +
                                                    "P.`CreateDate` as `Дата создания`, " +
                                                    "P.`IsDefault` as `Профиль по умолч.` " +
                                                    "FROM `usersprofiles` P " +
                                                    "LEFT JOIN `users` U ON P.`UserID` = U.`ID` U.`Status` = 2;");
            const string fileName = "полная_информация_профилей_клиентов";
            ExportMethods.CreateXlsFile(Response, infoSet, fileName, "profiles");
        }


        public void lbGetRefundsInformation_Click(Object sender, EventArgs e)
        {
            Response.Redirect(String.Format("~/PrintServices/PrintRefunds.aspx?createfrom={0}&createto={1}", stbCreateFrom.Text, stbCreateTo.Text));
        }

    }
}