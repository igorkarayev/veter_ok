using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Delivery.DAL;
using Delivery.ManagerUI;

namespace Delivery.PrintServices
{
    public partial class PrintVinil : BasePage
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
                    sqlString = sqlString + "ID = " + id + " OR ";
                }
                var fullSqlString = "SELECT SecureID, CityID, ID, BoxesNumber, RecipientStreet, RecipientStreetPrefix, UserID, AdmissionDate, DeliveryDate FROM `tickets` WHERE " + sqlString.Remove(sqlString.Length - 3);
                var dm = new DataManager();
                var dataset =  dm.QueryWithReturnDataSet(fullSqlString);
                var datasetNew = new DataSet();
                datasetNew.Tables.Add("Main");
                datasetNew.Tables["Main"].Columns.Add("SecureID");
                datasetNew.Tables["Main"].Columns.Add("CityID");
                datasetNew.Tables["Main"].Columns.Add("BoxesNumber");
                datasetNew.Tables["Main"].Columns.Add("FullRecipientStreet");
                datasetNew.Tables["Main"].Columns.Add("BoxesItem");
                datasetNew.Tables["Main"].Columns.Add("ID");
                datasetNew.Tables["Main"].Columns.Add("UserID");
                datasetNew.Tables["Main"].Columns.Add("AdmissionDate");
                datasetNew.Tables["Main"].Columns.Add("DeliveryDate");
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
                            newRow["UserID"] = row["UserID"];
                            if (!String.IsNullOrEmpty(row["AdmissionDate"].ToString()))
                            {
                                newRow["AdmissionDate"] = Convert.ToDateTime(row["AdmissionDate"])
                                    .ToString("MM.dd");
                            }
                            else
                            {
                                newRow["AdmissionDate"] = DateTime.Now.ToString("dd.MM");
                            }
                            newRow["DeliveryDate"] = Convert.ToDateTime(row["DeliveryDate"]).ToString("dd.MM");
                            newRow["CityID"] = row["CityID"];
                            newRow["BoxesNumber"] = row["BoxesNumber"];

                            if (row["RecipientStreet"].ToString().Length > 12)
                            {
                                newRow["FullRecipientStreet"] = String.Format("{1} {0}",
                                    row["RecipientStreet"].ToString()
                                        .Remove(12, row["RecipientStreet"].ToString().Length - 12),
                                    row["RecipientStreetPrefix"]);
                            }
                            else
                            {
                                newRow["FullRecipientStreet"] = String.Format("{1} {0}", row["RecipientStreet"], row["RecipientStreetPrefix"]);
                            }
                            
                            newRow["BoxesItem"] = i.ToString();
                            datasetNew.Tables["Main"].Rows.Add(newRow);
                        }
                    }
                    else
                    {
                        DataRow newRow = datasetNew.Tables["Main"].NewRow();
                        newRow["SecureID"] = row["SecureID"];
                        newRow["ID"] = row["ID"];
                        newRow["UserID"] = row["UserID"]; 
                        if (!String.IsNullOrEmpty(row["AdmissionDate"].ToString()))
                        {
                            newRow["AdmissionDate"] = Convert.ToDateTime(row["AdmissionDate"])
                                .ToString("MM.dd");
                        }
                        else
                        {
                            newRow["AdmissionDate"] = DateTime.Now.ToString("dd.MM");
                        }
                        newRow["DeliveryDate"] = Convert.ToDateTime(row["DeliveryDate"]).ToString("dd.MM");
                        newRow["CityID"] = row["CityID"];
                        newRow["BoxesNumber"] = row["BoxesNumber"];
                        if (row["RecipientStreet"].ToString().Length > 12)
                        {
                            newRow["FullRecipientStreet"] = String.Format("{1} {0}",
                                row["RecipientStreet"].ToString()
                                    .Remove(12, row["RecipientStreet"].ToString().Length - 12),
                                row["RecipientStreetPrefix"]);
                        }
                        else
                        {
                            newRow["FullRecipientStreet"] = String.Format("{1} {0}", row["RecipientStreet"], row["RecipientStreetPrefix"]);
                        }
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