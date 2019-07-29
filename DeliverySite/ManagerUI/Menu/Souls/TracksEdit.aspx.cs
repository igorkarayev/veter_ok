using Delivery.BLL.Helpers;
using Delivery.BLL.StaticMethods;
using Delivery.DAL;
using Delivery.DAL.DataBaseObjects;
using DeliverySite.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace Delivery.ManagerUI.Menu.Souls
{
    public partial class TracksEdit : ManagerBasePage
    {
        protected string ButtonText { get; set; }

        protected string ActionText { get; set; }

        protected void Page_Init(object sender, EventArgs e)
        {
            ButtonText = Page.Request.Params["id"] != null ? "Изменить" : "Добавить";
            ActionText = Page.Request.Params["id"] != null ? "Редактирование" : "Добавление";
            btnCreate.Click += bntCreate_Click;
            DataBind();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            OtherMethods.ActiveRightMenuStyleChanche("hlTracks", this.Page);
            OtherMethods.ActiveRightMenuStyleChanche("hlSouls", this.Page);
            Page.Title = Page.Request.Params["id"] != null ? PagesTitles.ManagerTracksEdit + BackendHelper.TagToValue("page_title_part") : PagesTitles.ManagerTracksCreate + BackendHelper.TagToValue("page_title_part");

            #region Блок доступа к странице
            var userInSession = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());
            if (currentRole.PageTracksEdit != 1)
            {
                Response.Redirect("~/Error.aspx?id=1");
            }
            #endregion

            if (!IsPostBack)
            {
                var dm = new DataManager();
                var dataSet = dm.QueryWithReturnDataSet("SELECT `Name`, `Family`, `ID`, `Role` FROM `users` WHERE (role = 'Manager' || role = 'Operator') AND `Status` = 2 ORDER BY `Family` ASC, `Name` ASC;");
                dataSet.Tables[0].Columns.Add("FIO", typeof(string), "Family + ' ' + Name + ' (' + Role + ')'");
                ddlManager.DataSource = dataSet;
                ddlManager.DataTextField = "FIO";
                ddlManager.DataValueField = "ID";
                ddlManager.DataBind();
                ddlManager.Items.Insert(0, new ListItem("Не назначен", "0"));
            }
            if (Page.Request.Params["id"] != null)
            {
                var track = new Tracks { ID = Convert.ToInt32(Page.Request.Params["id"]) };
                track.GetById();
                if (!IsPostBack)
                {
                    tbName.Text = track.Name;
                    ddlManager.SelectedValue = track.ManagerID.ToString();
                }
            }
        }

        public void bntCreate_Click(Object sender, EventArgs e)
        {
            var id = Page.Request.Params["id"];
            var track = new Tracks
            {
                Name = tbName.Text,
                ManagerID = Convert.ToInt32(ddlManager.SelectedValue)
            };
            if (id == null)
            {
                track.Create();
            }
            else
            {
                track.ID = Convert.ToInt32(id);
                track.Update();
            }
            Page.Response.Redirect("~/ManagerUI/Menu/Souls/TracksView.aspx");
        }
    }
}