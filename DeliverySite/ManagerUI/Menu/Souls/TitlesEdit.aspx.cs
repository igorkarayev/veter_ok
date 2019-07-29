using Delivery.BLL.Helpers;
using Delivery.BLL.StaticMethods;
using Delivery.DAL.DataBaseObjects;
using DeliverySite.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace Delivery.ManagerUI.Menu.Souls
{
    public partial class TitlesEdit : ManagerBasePage
    {
        protected string ButtonText { get; set; }

        protected string ActionText { get; set; }

        protected void Page_Init(object sender, EventArgs e)
        {
            ButtonText = Page.Request.Params["id"] != null ? "Сохранить" : "Добавить";
            ActionText = Page.Request.Params["id"] != null ? "Редактирование" : "Добавление";
            btnCreate.Click += bntCreate_Click;
            btnDelete.Click += bntDelete_Click;
            DataBind();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = Page.Request.Params["id"] != null ? PagesTitles.ManagerTitlesEditTitle + BackendHelper.TagToValue("page_title_part") : PagesTitles.ManagerTitlesCreateTitle + BackendHelper.TagToValue("page_title_part");
            OtherMethods.ActiveRightMenuStyleChanche("hlTitles", this.Page);
            OtherMethods.ActiveRightMenuStyleChanche("hlSouls", this.Page);

            #region Блок доступа к странице
            var userInSession = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());
            if (currentRole.PageTitlesEdit != 1)
            {
                Response.Redirect("~/Error.aspx?id=1");
            }
            #endregion

            if (!IsPostBack)
            {
                var category = new Category();
                var ds = category.GetAllItems("Name", "ASC", null);
                ddlCategory.DataSource = ds;
                ddlCategory.DataTextField = "Name";
                ddlCategory.DataValueField = "ID";
                ddlCategory.DataBind();
                ddlCategory.Items.Add(new ListItem("Не назначена", "0"));
            }

            btnDelete.Visible = CanDelete();

            if (Page.Request.Params["id"] != null)
            {
                var title = new Titles() { ID = Convert.ToInt32(Page.Request.Params["id"]) };
                title.GetById();
                if (!IsPostBack)
                {
                    tbName.Text = title.Name;
                    tbMarginCoefficient.Text = title.MarginCoefficient.ToString();
                    tbWeightMin.Text = title.WeightMin.ToString();
                    tbWeightMax.Text = title.WeightMax.ToString();
                    tbAdditiveCostWithoutAkciza.Text = title.AdditiveCostWithoutAkciza.ToString();
                    if (title.Additive == 1)
                        cbAdditive.Checked = true;
                    if (title.CanBeWithoutAkciza == 1)
                        cbCanBeWithoutAkciza.Checked = true;

                    ddlCategory.SelectedValue = title.CategoryID.ToString();
                }
            }
        }

        public void bntCreate_Click(Object sender, EventArgs e)
        {
            var id = Page.Request.Params["id"];
            var marginCoefficient = tbMarginCoefficient.Text.Replace(".", ",") == String.Empty ? 0 : Convert.ToDouble(tbMarginCoefficient.Text.Replace(".", ","));
            var weightmin = tbWeightMin.Text.Replace(".", ",") == String.Empty ? 0 : Convert.ToDouble(tbWeightMin.Text.Replace(".", ","));
            var weightmax = tbWeightMax.Text.Replace(".", ",") == String.Empty ? 0 : Convert.ToDouble(tbWeightMax.Text.Replace(".", ","));
            var title = new Titles
            {
                Name = tbName.Text.Replace("\"", "''"),
                MarginCoefficient = marginCoefficient,
                WeightMin = weightmin,
                WeightMax = weightmax,
                Additive = cbAdditive.Checked ? 1 : 0,
                CanBeWithoutAkciza = cbCanBeWithoutAkciza.Checked ? 1 : 0,
                CategoryID = Convert.ToInt32(ddlCategory.SelectedValue),
                AdditiveCostWithoutAkciza = Convert.ToDouble(tbAdditiveCostWithoutAkciza.Text),
            };
            if (id == null)
            {
                title.Create();
            }
            else
            {
                title.ID = Convert.ToInt32(id);
                title.Update();
            }
            Page.Response.Redirect("~/ManagerUI/Menu/Souls/TitlesView.aspx");
        }

        public void bntDelete_Click(Object sender, EventArgs e)
        {
            var id = Page.Request.Params["id"];
            var title = new Titles();
            title.Delete(Convert.ToInt32(id));
            Page.Response.Redirect("~/ManagerUI/Menu/Souls/TitlesView.aspx");
        }

        protected bool CanDelete()
        {
            var userInSession = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());
            return currentRole.ActionTitlesDelete == 1;
        }
    }
}