using System;
using System.Collections.Generic;
using System.Linq;
using Delivery.BLL.Helpers;
using Delivery.DAL;
using Delivery.DAL.DataBaseObjects;
using Delivery.ManagerUI;
using Delivery.BLL.StaticMethods;
using System.Data;

namespace Delivery.PrintServices
{
    public partial class PrintZP2 : ManagerBasePage
    {
        protected String DriverID { get; set; }

        protected String AdmissionDate { get; set; }

        protected String DriverName { get; set; }

        protected String AppKey { get; set; }

        protected String StavkaNDS { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            AppKey = Globals.Settings.AppServiceSecureKey;
            StavkaNDS = BackendHelper.TagToValue("stavka_nds");
            var idListString = Request.QueryString["id"];
            var costListString = Request.QueryString["cost"];
            var numberListString = Request.QueryString["number"];
            var withUrMarker = Request.QueryString["with_ur"];
            if (!String.IsNullOrEmpty(idListString))
            {
                var dm = new DataManager();
                var idList = idListString.Split('-').ToList();
                List<string> costList = null;
                List<string> numberList = null;
                if (!String.IsNullOrEmpty(costListString) && !String.IsNullOrEmpty(numberListString))
                {
                    costList = costListString.Split('-').ToList();
                    numberList = numberListString.Split('-').ToList();
                }              
                    
                var sqlString = String.Empty;
                foreach (var id in idList)
                {
                    if (String.IsNullOrEmpty(DriverID))
                    {
                        DriverID = dm.QueryWithReturnDataSet(String.Format("SELECT `DriverID` FROM `tickets` WHERE `id` = {0}", id)).Tables[0].Rows[0][0].ToString();
                    }

                    if (String.IsNullOrEmpty(AdmissionDate))
                    {
                        AdmissionDate = dm.QueryWithReturnDataSet(String.Format("SELECT `AdmissionDate` FROM `tickets` WHERE `id` = {0}", id)).Tables[0].Rows[0][0].ToString();
                    }

                    sqlString = sqlString + "T.`ID` = " + id + " OR ";
                }
                string fullSqlString;

                if (String.IsNullOrEmpty(withUrMarker))
                {
                    cbWithUr.Checked = false;
                    fullSqlString = "SELECT * FROM `tickets` T JOIN `usersprofiles` U ON T.`UserProfileID` = U.`ID` WHERE (" + sqlString.Remove(sqlString.Length - 3) + ") AND T.`PrintNaklInMap` = 0 AND U.`TypeID` = '1'";
                }
                else
                {
                    cbWithUr.Checked = true;
                    fullSqlString = "SELECT * FROM `tickets` T JOIN `usersprofiles` U ON T.`UserProfileID` = U.`ID` WHERE (" + sqlString.Remove(sqlString.Length - 3) + ") AND T.`PrintNaklInMap` = 0";
                }

                var dataset = dm.QueryWithReturnDataSet(fullSqlString);
                dataset.Tables[0].Columns.Add("AllCost", typeof(String));
                dataset.Tables[0].Columns.Add("BoxesNewNumber", typeof(String));

                int iterator = 0;
                if(costList != null && numberList != null)
                {
                    foreach (DataRow row in dataset.Tables[0].Rows)
                    {
                        row["AllCost"] = costList[iterator];
                        row["BoxesNewNumber"] = numberList[iterator];
                        iterator++;
                    }
                }
                
                lvAllPrint.DataSource = dataset;
                lvAllPrint.DataBind();

                #region Загружаем ФИО водителя
                try
                {
                    DriverName =
                        dm.QueryWithReturnDataSet(String.Format("SELECT CONCAT(`FirstName`, ' ',  `LastName`, ' ', `ThirdName`) FROM `drivers` WHERE `id` = {0}", DriverID))
                            .Tables[0].Rows[0][0].ToString();
                }
                catch (Exception)
                {
                    DriverName = String.Empty;
                }
                #endregion

                #region Подгружаем инпут с номером путевого
                Inpputevoi.Text =
                    dm.QueryWithReturnDataSet("SELECT `Putevoi` FROM `printdata`").Tables[0].Rows[0][0]
                        .ToString();
                Inpnaklnumber.Text =
                    dm.QueryWithReturnDataSet("SELECT `NaklNumber` FROM `printdata`").Tables[0].Rows[0][0]
                        .ToString();
                #endregion
            }

            #region Сообщение, если заказ-поручение пустое
            if (String.IsNullOrEmpty(idListString) || lvAllPrint.Items.Count == 0)
            {
                Page.Visible = false;
                if (String.IsNullOrEmpty(idListString))
                {
                    Response.Write(Resources.PrintResources.PrintZPEmptyText);
                }
                else
                {
                    Response.Write(Resources.PrintResources.PrintZPEmptyText +
                    String.Format("<br/><center><a href=\"{0}&with_ur=1\">печать с юр. лицами</a><center>", Request.RawUrl));
                }
            }
            #endregion
        }

        protected void cbWithUr_CheckedChanged(object sender, EventArgs e)
        {
            if (!cbWithUr.Checked)
            {
                Response.Redirect(Request.RawUrl + "&with_ur=1");
            }
            else
            {
                Response.Redirect(Request.RawUrl.Remove(Request.RawUrl.Length - 10, 10));
            }

        }
    }
}