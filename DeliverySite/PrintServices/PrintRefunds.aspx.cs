using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Delivery.BLL;
using Delivery.BLL.Helpers;
using Delivery.BLL.StaticMethods;
using Delivery.DAL;
using Delivery.DAL.DataBaseObjects;
using Delivery.ManagerUI;

namespace Delivery.PrintServices
{
    public partial class PrintRefunds : ManagerBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var createFrom = Request.QueryString["createfrom"];
            var createTo = Request.QueryString["createto"];
            var searchDateString = string.Empty;
            //формируем cтроку для поиска по дате отправки
            if (!string.IsNullOrEmpty(createFrom) && !string.IsNullOrEmpty(createTo))
            {
                searchDateString = "(T.ReturnDate BETWEEN '" +
                                   Convert.ToDateTime(createFrom).ToString("yyyy-MM-dd") + "' AND '" +
                                   Convert.ToDateTime(createTo).ToString("yyyy-MM-dd") + "')";
            }

            if (!string.IsNullOrEmpty(createFrom) && string.IsNullOrEmpty(createTo))
            {
                searchDateString = "(T.ReturnDate BETWEEN '" +
                                   Convert.ToDateTime(createFrom).ToString("yyyy-MM-dd") + "' AND '" +
                                   Convert.ToDateTime(createFrom).AddYears(1).ToString("yyyy-MM-dd") + "')";
            }

            if (string.IsNullOrEmpty(createFrom) && !string.IsNullOrEmpty(createTo))
            {
                searchDateString = "(T.ReturnDate BETWEEN '" +
                                   Convert.ToDateTime(createTo).AddYears(-2).ToString("yyyy-MM-dd") + "' AND '" +
                                   Convert.ToDateTime(createTo).ToString("yyyy-MM-dd") + "')";
            }

            if (!String.IsNullOrEmpty(createFrom) || !String.IsNullOrEmpty(createTo))
            {
                var fullSqlString = String.Format("SELECT * FROM `tickets` as T JOIN `city` as C on T.CityID = C.ID " +
                                    "WHERE T.`ReturnDate` IS NOT NULL AND {0} ORDER BY C.Name ASC",
                                    searchDateString);
                var dm = new DataManager();
                var dataset =  dm.QueryWithReturnDataSet(fullSqlString);
                lvAllPrint.DataSource = dataset;
                lvAllPrint.DataBind();
            }

            if ((String.IsNullOrEmpty(createFrom) && String.IsNullOrEmpty(createTo)) || lvAllPrint.Items.Count == 0)
            {
                Page.Visible = false;
                Response.Write(Resources.PrintResources.PrintRefundsEmptyText);
            }
        }
    }
}