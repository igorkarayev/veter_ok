using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Delivery.BLL;
using Delivery.BLL.StaticMethods;
using Delivery.DAL.DataBaseObjects;
using System.Globalization;

namespace Delivery.PrintServices.Controls
{
    public partial class GoodsListForZP : System.Web.UI.UserControl
    {
        public string TicketID
        {
            get {
                return ViewState["TicketID"] != null ? ViewState["TicketID"].ToString() : String.Empty;
            }
            set
            {
                ViewState["TicketID"] = value;
            }

        }

        public string ListViewControlFullID
        {
            get
            {
                return ViewState["ListViewControlFullID"] != null ? ViewState["ListViewControlFullID"].ToString() : String.Empty;
            }
            set
            {
                ViewState["ListViewControlFullID"] = value;
            }

        }

        public string Number { get; set; }

        public string Cost { get; set; }

        public string AssessedCost { get; set; }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            var ticket = new Tickets { ID = Convert.ToInt32(TicketID) };
            ticket.GetById();
            AssessedCost = MoneyMethods.MoneySeparator(ticket.AssessedCost);
            
            pnlOldTickets.Visible = false;
            pnlNewTickets.Visible = true;

            #region Создаем датасеты с четными и нечетными грузами
            var goods = new Goods { TicketFullSecureID = ticket.FullSecureID };
            var ds1 = goods.GetAllItems("ID", "ASC", "TicketFullSecureID");
            var ds2 = new DataSet();

            double allCost;
            if (Cost == null)
            {
                allCost = ticket.Pril2Cost == 0 ? Convert.ToDouble(AssessedCost) : Convert.ToDouble(MoneyMethods.MoneySeparator(ticket.Pril2Cost));
            }
            else
            {
                allCost = Convert.ToDouble(MoneyMethods.MoneySeparator(Cost));
            }
            double c = Convert.ToDouble(ticket.Pril2Cost == 0 ? AssessedCost : MoneyMethods.MoneySeparator(ticket.Pril2Cost));
            if (Cost != null && Number != null)
            {                
                double sumCost = 0;
                foreach (DataRow row in ds1.Tables[0].Rows.Cast<DataRow>())
                {
                    row["Cost"] = MoneyMethods.MoneySeparator(Convert.ToDouble(row["Cost"]) + ((double.Parse(Cost, CultureInfo.InvariantCulture) - c) / ds1.Tables[0].Rows.Count));
                    //row["Number"] = Number;
                    sumCost += Convert.ToDouble(row["Cost"]);
                }
                if(sumCost != allCost)
                    foreach (DataRow row in ds1.Tables[0].Rows.Cast<DataRow>())
                    {
                            row["Cost"] = MoneyMethods.MoneySeparator(Convert.ToDouble(row["Cost"]) + (allCost - sumCost));
                            //row["Number"] = Convert.ToInt32(Convert.ToDouble(allCost) / sumCost);
                            row["Number"] = 2;
                            break;
                    }
            }

            ds2 = ds1.Copy();
            //делаем ds1 только с нечетными элементами
            foreach (DataRow row in ds1.Tables[0].Rows.Cast<DataRow>().Where(row => Convert.ToInt32(row["ID"]) % 2 == 0))
            {
                row.Delete();
            }

            //делаем ds2 только с четными элементами
            foreach (DataRow row in ds2.Tables[0].Rows.Cast<DataRow>().Where(row => Convert.ToInt32(row["ID"]) % 2 != 0))
            {
                row.Delete();
            }
            #endregion

            #region Выравниваем колличество столбцов в датасетах
            ds1.AcceptChanges();
            ds2.AcceptChanges();

            var ds1RowsNumber = ds1.Tables[0].Rows.Count;
            var ds2RowsNumber = ds2.Tables[0].Rows.Count;
            var ds1InLeft = true;
            //если нечетных элементов больше чем четных - добавляем к четным еще один (нужно для корректной отрисовки заказ-поручения)
            if (ds1RowsNumber > ds2RowsNumber)
            {
                var row = ds2.Tables[0].NewRow();
                ds2.Tables[0].Rows.Add(row);
            }

            //если четных элементов больше чем нечетных - добавляем к нечетным еще один (нужно для корректной отрисовки заказ-поручения)
            if (ds1RowsNumber < ds2RowsNumber)
            {
                var row = ds1.Tables[0].NewRow();
                ds1.Tables[0].Rows.Add(row);
                ds1InLeft = false;
            }
            #endregion

            #region дополняем датасеты до 3-х элементов (нужно при отрисовке)
            ds1.AcceptChanges();
            ds2.AcceptChanges();
            var dataSetsRowsNumber = ds1.Tables[0].Rows.Count;

            while (dataSetsRowsNumber < 3)
            {
                var row1 = ds1.Tables[0].NewRow();
                var row2 = ds2.Tables[0].NewRow();
                ds1.Tables[0].Rows.Add(row1);
                ds2.Tables[0].Rows.Add(row2);
                ds1.AcceptChanges();
                ds2.AcceptChanges();
                dataSetsRowsNumber = ds1.Tables[0].Rows.Count;
            }
            #endregion

            if (ds1InLeft)
            {
                lvAllGoods1.DataSource = ds1;
                lvAllGoods1.DataBind();

                lvAllGoods2.DataSource = ds2;
                lvAllGoods2.DataBind();
            }
            else
            {
                lvAllGoods1.DataSource = ds2;
                lvAllGoods1.DataBind();

                lvAllGoods2.DataSource = ds1;
                lvAllGoods2.DataBind();
            }

            var lblOveralGoodsCostInListView = (Label)lvAllGoods2.FindControl("lblOveralGoodsCostInListView");
            if (Cost == null)
            {
                lblOveralGoodsCostInListView.Text = ticket.Pril2Cost == 0 ? AssessedCost : MoneyMethods.MoneySeparator(ticket.Pril2Cost);
            }
            else
            {
                lblOveralGoodsCostInListView.Text = MoneyMethods.MoneySeparator(Cost);
            }

            var lblNumber = (Label)lvAllGoods1.FindControl("lblNumber");
            lblNumber.Text = ticket.Pril2BoxesNumber == 0 ? String.Format("кол.:<span class=\"validBoxesNumber\">{0}</span> ", ticket.BoxesNumber) : String.Format("кол.:<span class=\"validBoxesNumber\">{0}</span>", ticket.Pril2BoxesNumber);

            var lblCost = (Label)lvAllGoods1.FindControl("lblCost");
            lblCost.Text = ticket.Pril2Cost == 0 ? String.Format("ст.:<span class=\"validCost\">{0}</span>", AssessedCost) : String.Format("ст.:<span class=\"validCost\">{0}</span>", MoneyMethods.MoneySeparator(ticket.Pril2Cost));
        }
    }
}