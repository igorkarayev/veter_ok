using System;
using System.Collections.Generic;
using System.Linq;
using Delivery.DAL;
using Delivery.DAL.DataBaseObjects;
using Delivery.ManagerUI;

namespace Delivery.PrintServices
{
    public partial class PrintAORT : ManagerBasePage
    {
        protected String UserAgrimentDate { get; set; }
        protected String UserAgrimentNumber { get; set; }
        protected String CompanyName { get; set; }
        protected String ProfileFio { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            var idListString = Request.QueryString["id"];
            if (!String.IsNullOrEmpty(idListString))
            {
                var dm = new DataManager();
                var idList = idListString.Split('-').ToList();
                var sqlString = String.Empty;
                foreach (var id in idList)
                {
                    if (String.IsNullOrEmpty(UserAgrimentDate))
                    {
                        var dataTables = dm.QueryWithReturnDataSet(String.Format("SELECT `AgreementDate`,`AgreementNumber`,`CompanyName`,`FirstName`,`LastName`,`ThirdName`  FROM `usersprofiles` WHERE `id` = (SELECT `userprofileid` FROM `tickets` WHERE id = {0})", id));
                        try
                        {
                            UserAgrimentDate = Convert.ToDateTime(dataTables.Tables[0].Rows[0][0]).ToString("dd.MM.yyyyг.");
                        }
                        catch (Exception)
                        {
                            
                        }
                        UserAgrimentNumber = dataTables.Tables[0].Rows[0][1].ToString();
                        CompanyName = dataTables.Tables[0].Rows[0][2].ToString();
                        if (String.IsNullOrEmpty(CompanyName))
                            CompanyName = null;
                        ProfileFio = dataTables.Tables[0].Rows[0][3] + " " + dataTables.Tables[0].Rows[0][4] + " " + dataTables.Tables[0].Rows[0][5];
                    }
                    sqlString = sqlString + "T.`ID` = " + id + " OR ";
                }
                var fullSqlString = "SELECT * FROM `goods` G WHERE G.`TicketFullSEcureID` IN (SELECT T.`FullSecureID` FROM `tickets` T WHERE " + sqlString.Remove(sqlString.Length - 3) + ") ORDER BY G.TicketFullSecureID DESC, G.`ID` ASC";
                
                var dataset = dm.QueryWithReturnDataSet(fullSqlString);
                lvAllPrint.DataSource = dataset;
                lvAllPrint.DataBind();
            }

            #region Сообщение, если заказ-поручение пустое
            if (String.IsNullOrEmpty(idListString) || lvAllPrint.Items.Count == 0)
            {
                Page.Visible = false;
                if (String.IsNullOrEmpty(idListString))
                {
                    Response.Write(Resources.PrintResources.PrintAortEmptyText);
                }
            }
            #endregion
        }
    }
}