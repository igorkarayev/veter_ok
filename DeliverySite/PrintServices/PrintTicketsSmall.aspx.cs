using System;
using Delivery.DAL;
using Delivery.ManagerUI;

namespace Delivery.PrintServices
{
    public partial class PrintTicketsSmall : ManagerBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var idListString = Request.QueryString["value"];
            if (!String.IsNullOrEmpty(Request.QueryString["value"]))
            {
                var dm = new DataManager();
                var dataset = dm.QueryWithReturnDataSet(idListString);
                lvAllPrint.DataSource = dataset;
                lvAllPrint.DataBind();
            }

            if (String.IsNullOrEmpty(idListString) || lvAllPrint.Items.Count == 0)
            {
                Page.Visible = false;
                Response.Write(Resources.PrintResources.PrintTicketsEmptyText);
            }
        }
    }
}