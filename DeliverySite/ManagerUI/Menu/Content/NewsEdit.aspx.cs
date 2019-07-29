using Delivery.BLL;
using Delivery.BLL.Helpers;
using Delivery.BLL.StaticMethods;
using Delivery.DAL.DataBaseObjects;
using DeliverySite.Resources;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Delivery.ManagerUI.Menu.Content
{
    public partial class NewsEdit : ManagerBasePage
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
            OtherMethods.ActiveRightMenuStyleChanche("hlNews", this.Page);
            OtherMethods.ActiveRightMenuStyleChanche("hlContent", this.Page);
            Page.Title = Page.Request.Params["id"] != null ? PagesTitles.ManagerNewsEdit + BackendHelper.TagToValue("page_title_part") : PagesTitles.ManagerNewsCreate + BackendHelper.TagToValue("page_title_part");

            #region Блок доступа к странице
            var userInSession = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());
            if (currentRole.PageNewsEdit != 1)
            {
                Response.Redirect("~/Error.aspx?id=1");
            }
            #endregion

            if (!IsPostBack)
            {
                ddlNewsTypeID.DataSource = News.NewsTypes;
                ddlNewsTypeID.DataTextField = "Value";
                ddlNewsTypeID.DataValueField = "Key";
                ddlNewsTypeID.DataBind();
                cbForViewing.Checked = true;
                hfForViewing.Value = "1";
            }

            if (Page.Request.Params["id"] != null)
            {
                var news = new News { ID = Convert.ToInt32(Page.Request.Params["id"]) };
                news.GetById();
                if (!IsPostBack)
                {
                    ddlNewsTypeID.SelectedValue = news.NewsTypeID.ToString();
                    tbTitle.Text = news.Title;
                    tbBody.Text = news.Body;
                    cbForViewing.Checked = news.ForViewing == 1;
                    hfForViewing.Value = news.ForViewing.ToString();
                }
            }
        }

        public void bntCreate_Click(Object sender, EventArgs e)
        {
            var id = Page.Request.Params["id"];
            var partOfTitle = Transliterate.PrettyUrl(tbTitle.Text).Trim();
            var news = new News
            {
                Title = tbTitle.Text.Trim(),
                Body = tbBody.Text,
                NewsTypeID = Convert.ToInt32(ddlNewsTypeID.SelectedValue),
                ForViewing = cbForViewing.Checked ? 1 : 0,
            };
            if (id == null)
            {
                news.TitleUrl = String.Format("{0}_{1}", partOfTitle, DateTime.Now.ToString("dd-MM-yy"));
                var similarNews = new News { TitleUrl = news.TitleUrl };
                var similarNewsTable = similarNews.GetAllItems("ID", "Desc", "TitleUrl");
                if (similarNewsTable.Tables[0].Rows.Count != 0)
                {
                    lblNotif.Text = "Новость с таким названием существует!";
                    return;
                }
                news.Create();
            }
            else
            {
                var oldNews = new News() { ID = Convert.ToInt32(id) };
                oldNews.GetById();
                if (oldNews.TitleUrl.Remove(oldNews.TitleUrl.Length - 9, 9) != partOfTitle)
                {
                    news.TitleUrl = String.Format("{0}_{1}", partOfTitle, DateTime.Now.ToString("dd-MM-yy"));
                    var similarNews = new News { TitleUrl = news.TitleUrl };
                    var similarNewsTable = similarNews.GetAllItems("ID", "Desc", "TitleUrl");
                    if (similarNewsTable.Tables[0].Rows.Count != 0)
                    {
                        lblNotif.Text = "Новость с таким названием существует!";
                        return;
                    }
                }
                news.ID = Convert.ToInt32(id);
                news.Update();
            }
            Page.Response.Redirect("~/ManagerUI/Menu/Content/NewsView.aspx");
        }
    }
}