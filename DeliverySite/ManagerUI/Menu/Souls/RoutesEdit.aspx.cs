using System;
using Delivery.BLL.StaticMethods;
using Delivery.DAL.DataBaseObjects;

namespace Delivery.ManagerUI.Menu.Souls
{
    public partial class RoutesEdit : ManagerBasePage
    {
        protected string ButtonText { get; set; }

        protected void Page_Init(object sender, EventArgs e)
        {
            ButtonText = Page.Request.Params["id"] != null ? "Изменить" : "Добавить";
            btnCreate.Click += bntCreate_Click;
            DataBind();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            OtherMethods.ActiveRightMenuStyleChanche("hlTracks", this.Page);
            OtherMethods.ActiveRightMenuStyleChanche("hlSouls", this.Page);
            if (Page.Request.Params["id"] != null)
            {
                var route = new Routes { ID = Convert.ToInt32(Page.Request.Params["id"]) };
                route.GetById();
                if (!IsPostBack)
                {
                    tbName.Text = route.Name;
                }
            }
        }

        public void bntCreate_Click(Object sender, EventArgs e)
        {
            var id = Page.Request.Params["id"];
            var route = new Routes { Name = tbName.Text };
            if (id == null)
            {
                route.Create();
            }
            else
            {
                route.ID = Convert.ToInt32(id);
                route.Update();
            }
            Page.Response.Redirect("~/ManagerUI/RoutesView.aspx");
        }
    }
}