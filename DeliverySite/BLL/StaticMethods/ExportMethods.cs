using Delivery.BLL.StaticMethods;
using System;
using System.Data;
using System.IO;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Delivery.BLL.Helpers
{
    public class ExportMethods
    {
        public static void CreateXlsFile(HttpResponse responce, DataSet ds, String fileName, string exportType)
        {
            //создаем динамический гридвью, который можно как угодно вертеть
            var gridView = new GridView
            {
                AllowPaging = false,
                DataSource = ds.Tables[0]
            };
            gridView.DataBind();

            var sGenName = fileName + ".xls";
            responce.Clear();
            responce.Buffer = true;
            responce.AddHeader("content-disposition", "attachment;filename=" + sGenName);
            responce.Charset = "";
            responce.ContentType = "application/vnd.ms-excel";
            var sw = new StringWriter();
            var hw = new HtmlTextWriter(sw);
            for (var i = 0; i < gridView.Rows.Count; i++)
            {
                //добавляем стиль к строкам
                gridView.Rows[i].Attributes.Add("class", "textmode");
                //var index = gridView.Rows.IndexOf(currentRow);
                if (exportType == "users")
                {
                    try
                    {
                        gridView.Rows[i].Cells[6].Text = UsersHelper.UserStatusToText(Convert.ToInt32(gridView.Rows[i].Cells[6].Text));
                        gridView.Rows[i].Cells[9].Text = OtherMethods.CheckboxView(Convert.ToInt32(gridView.Rows[i].Cells[9].Text));
                        gridView.Rows[i].Cells[10].Text = OtherMethods.CheckboxView(Convert.ToInt32(gridView.Rows[i].Cells[10].Text));
                    }
                    catch (Exception)
                    {
                    }
                }

                if (exportType == "profiles")
                {
                    try
                    {
                        gridView.Rows[i].Cells[8].Text = UsersProfilesHelper.UserProfileTypeToText(Convert.ToInt32(gridView.Rows[i].Cells[8].Text));
                        gridView.Rows[i].Cells[9].Text = UsersProfilesHelper.UserProfileStatusToText(Convert.ToInt32(gridView.Rows[i].Cells[9].Text));
                        gridView.Rows[i].Cells[24].Text = OtherMethods.CheckboxView(Convert.ToInt32(gridView.Rows[i].Cells[24].Text));
                    }
                    catch (Exception)
                    {
                    }

                }

                if (exportType == "clients")
                {
                    try
                    {
                        gridView.Rows[i].Cells[7].Text = UsersHelper.UserStatusStadyToText(Convert.ToInt32(gridView.Rows[i].Cells[7].Text));
                    }
                    catch (Exception)
                    {
                    }

                }

                if (exportType == "calculation")
                {
                    try
                    {
                        gridView.Rows[i].Cells[0].Text = OtherMethods.DateConvert(gridView.Rows[i].Cells[0].Text);
                        gridView.Rows[i].Cells[1].Text = UsersProfilesHelper.UserProfileIDToFullFamilyOrCompanyname(gridView.Rows[i].Cells[1].Text);
                        gridView.Rows[i].Cells[3].Text = OtherMethods.TicketStatusToTextWithoutColor(gridView.Rows[i].Cells[3].Text);
                        gridView.Rows[i].Cells[4].Text = MoneyMethods.AgreedAssessedDeliveryCosts(gridView.Rows[i].Cells[4].Text);
                        gridView.Rows[i].Cells[6].Text = (Convert.ToDecimal(MoneyMethods.AgreedAssessedDeliveryCosts(gridView.Rows[i].Cells[6].Text)) - Convert.ToDecimal(gridView.Rows[i].Cells[5].Text)).ToString();
                        gridView.Rows[i].Cells[8].Text = WebUtility.HtmlDecode(gridView.Rows[i].Cells[8].Text)
                            .Replace("comment-history-body", "")
                            .Replace("comment-history-name", "")
                            .Replace("comment-history-date", "")
                            .Replace("div", "")
                            .Replace("span", "")
                            .Replace("class", "")
                            .Replace(" =", "");
                        gridView.Rows[i].Cells[9].Text = DriversHelper.DriverIdToName(gridView.Rows[i].Cells[9].Text);
                    }
                    catch (Exception)
                    {
                    }

                }
            }
            gridView.RenderControl(hw);

            //стиль приведение чисел к строкам
            const string style = @"<style> .textmode { mso-number-format:\@; } </style>";
            responce.Write(style);
            responce.Output.Write(sw.ToString());
            responce.Flush();
            responce.End();
        }

    }
}