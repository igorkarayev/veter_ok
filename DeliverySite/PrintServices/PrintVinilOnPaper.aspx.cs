using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Delivery.DAL;
using Delivery.ManagerUI;
using Delivery.UserUI;

namespace Delivery.PrintServices
{
    public partial class PrintVinilOnPaper : UserBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var idListString = Request.QueryString["id"];
            if (!String.IsNullOrEmpty(idListString))
            {
                List<string> idList = idListString.Split('-').ToList();
                var sqlString = String.Empty;
                foreach (var id in idList)
                {
                    sqlString = sqlString + "SecureID = '" + id + "' OR ";
                }
                var fullSqlString = "SELECT SecureID, CityID, ID, BoxesNumber, RecipientStreet, RecipientStreetPrefix FROM `tickets` WHERE " + sqlString.Remove(sqlString.Length - 3);
                var dm = new DataManager();
                var dataset =  dm.QueryWithReturnDataSet(fullSqlString);
                var datasetNew = new DataSet();
                datasetNew.Tables.Add("Main");
                datasetNew.Tables["Main"].Columns.Add("SecureID");
                datasetNew.Tables["Main"].Columns.Add("CityID");
                datasetNew.Tables["Main"].Columns.Add("BoxesNumber");
                datasetNew.Tables["Main"].Columns.Add("RecipientStreet");
                datasetNew.Tables["Main"].Columns.Add("RecipientStreetPrefix");
                datasetNew.Tables["Main"].Columns.Add("BoxesItem");
                datasetNew.Tables["Main"].Columns.Add("ID");
                foreach (DataRow row in dataset.Tables[0].Rows)
                {
                    
                    
                    var str = row["BoxesNumber"];
                    if (Convert.ToInt32(str) > 1)
                    {
                        for (var i = 1; i <= Convert.ToInt32(str); i++)
                        {
                            DataRow newRow = datasetNew.Tables["Main"].NewRow();
                            newRow["SecureID"] = row["SecureID"];
                            newRow["ID"] = row["ID"];
                            newRow["CityID"] = row["CityID"];
                            newRow["BoxesNumber"] = row["BoxesNumber"];
                            newRow["RecipientStreet"] = row["RecipientStreet"];
                            newRow["RecipientStreetPrefix"] = row["RecipientStreetPrefix"];
                            newRow["BoxesItem"] = i.ToString();
                            datasetNew.Tables["Main"].Rows.Add(newRow);
                        }
                    }
                    else
                    {
                        DataRow newRow = datasetNew.Tables["Main"].NewRow();
                        newRow["SecureID"] = row["SecureID"];
                        newRow["ID"] = row["ID"];
                        newRow["CityID"] = row["CityID"];
                        newRow["BoxesNumber"] = row["BoxesNumber"];
                        newRow["RecipientStreet"] = row["RecipientStreet"];
                        newRow["RecipientStreetPrefix"] = row["RecipientStreetPrefix"];
                        newRow["BoxesItem"] = "1";
                        datasetNew.Tables["Main"].Rows.Add(newRow);
                    }
                }
                
                lvAllPrint.DataSource = datasetNew;
                lvAllPrint.DataBind();
            }

            if (String.IsNullOrEmpty(idListString) || lvAllPrint.Items.Count == 0)
            {
                Page.Visible = false;
                Response.Write(Resources.PrintResources.PrintVinilEmptyText);
            }
        }
    }
}