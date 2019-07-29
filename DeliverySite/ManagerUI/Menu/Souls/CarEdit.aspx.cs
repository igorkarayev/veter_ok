using Delivery.BLL.Helpers;
using Delivery.BLL.StaticMethods;
using Delivery.DAL.DataBaseObjects;
using DeliverySite.Resources;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Delivery.ManagerUI.Menu.Souls
{
    public partial class CarEdit : ManagerBasePage
    {
        protected string ButtonText { get; set; }

        protected string ActionText { get; set; }

        protected string BackLink { get; set; }

        protected void Page_Init(object sender, EventArgs e)
        {
            ButtonText = Page.Request.Params["id"] != null ? "Изменить" : "Добавить";
            ActionText = Page.Request.Params["id"] != null ? "Редактирование" : "Добавление";
            btnCreate.Click += bntCreate_Click;
            DataBind();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            OtherMethods.ActiveRightMenuStyleChanche("hlCars", this.Page);
            OtherMethods.ActiveRightMenuStyleChanche("hlSouls", this.Page);
            Page.Title = Page.Request.Params["id"] != null ? PagesTitles.ManagerCarEdit + BackendHelper.TagToValue("page_title_part") : PagesTitles.ManagerCarCreate + BackendHelper.TagToValue("page_title_part");

            #region Блок доступа к странице
            var userInSession = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());
            if (currentRole.PageCarEdit != 1)
            {
                Response.Redirect("~/Error.aspx?id=1");
            }
            #endregion

            BackLink = CarsHelper.BackCarLinkBuilder(Page.Request.Params["aid"], Page.Request.Params["model"], Page.Request.Params["number"], Page.Request.Params["typeid"]);

            if (!IsPostBack)
            {
                ddlType.DataSource = Cars.CarType;
                ddlType.DataTextField = "Value";
                ddlType.DataValueField = "Key";
                ddlType.DataBind();
            }

            if (Page.Request.Params["id"] != null)
            {
                var car = new Cars { ID = Convert.ToInt32(Page.Request.Params["id"]) };
                car.GetById();
                if (String.IsNullOrEmpty(car.Model)) Page.Response.Redirect("~/ManagerUI/Menu/Souls/CarsView.aspx?" + BackLink);
                if (!IsPostBack)
                {

                    ddlType.SelectedValue = car.TypeID.ToString();
                    tbModel.Text = car.Model;
                    tbNumber.Text = hfNumber.Value = car.Number;

                    tbCompanyName.Text = car.CompanyName;

                    tbFirstName.Text = car.FirstName;
                    tbLastName.Text = car.LastName;
                    tbThirdName.Text = car.ThirdName;
                    tbPassportSeria.Text = car.PassportSeria;
                    tbPassportNumber.Text = car.PassportNumber;
                    tbPersonalNumber.Text = car.PersonalNumber;
                    tbROVD.Text = car.ROVD;
                    tbRegistrationAddress.Text = car.RegistrationAddress;
                    tbValidity.Text = Convert.ToDateTime(car.Validity).ToString("dd-MM-yyyy");
                    tbBirthDay.Text = Convert.ToDateTime(car.BirthDay).ToString("dd-MM-yyyy");
                    tbDateOfIssue.Text = Convert.ToDateTime(car.DateOfIssue).ToString("dd-MM-yyyy");
                }
            }
        }

        public void bntCreate_Click(Object sender, EventArgs e)
        {
            var id = Page.Request.Params["id"];
            var userInSession = (Users)Session["userinsession"];
            var car = new Cars
            {
                TypeID = Convert.ToInt32(ddlType.SelectedValue),
                Model = tbModel.Text.Trim(),
                Number = tbNumber.Text.Trim(),

                CompanyName = tbCompanyName.Text.Replace("\"", "''")
                        .Replace("“", "''")
                        .Replace("”", "''")
                        .Replace("«", "''")
                        .Replace("»", "''")
                        .Trim(),

                FirstName = tbFirstName.Text.Trim(),
                LastName = tbLastName.Text.Trim(),
                ThirdName = tbThirdName.Text.Trim(),
                PassportSeria = tbPassportSeria.Text.Trim(),
                PassportNumber = tbPassportNumber.Text.Trim(),
                PersonalNumber = tbPersonalNumber.Text.Trim(),
                ROVD = tbROVD.Text.Trim(),
                RegistrationAddress = tbRegistrationAddress.Text.Trim(),
            };

            if (!String.IsNullOrEmpty(tbValidity.Text))
                car.Validity = Convert.ToDateTime(tbValidity.Text);

            if (!String.IsNullOrEmpty(tbBirthDay.Text))
                car.BirthDay = Convert.ToDateTime(tbBirthDay.Text);

            if (!String.IsNullOrEmpty(tbDateOfIssue.Text))
                car.DateOfIssue = Convert.ToDateTime(tbDateOfIssue.Text);

            if (id == null)
            {
                var sameCar = new Cars { Number = tbNumber.Text.Trim() };
                var sameCarTable = sameCar.GetAllByNumber();
                if (sameCarTable.Tables[0].Rows.Count > 0)
                {
                    lblError.Text = "Автомобиль с таким номером уже существует в базе!";
                    return;
                }
                car.CreateDate = DateTime.Now;
                car.Create();
            }
            else
            {
                var sameCar = new Cars { Number = tbNumber.Text.Trim() };
                var sameCarTable = sameCar.GetAllByNumber();
                if (sameCarTable.Tables[0].Rows.Count > 0 && tbNumber.Text.Trim() != hfNumber.Value)
                {
                    lblError.Text = "Автомобиль с таким номером уже существует в базе!";
                    return;
                }
                car.ID = Convert.ToInt32(id);
                car.ChangeDate = DateTime.Now;
                car.Update(userInSession.ID, OtherMethods.GetIPAddress(), "CarEdit");
            }
            Page.Response.Redirect("~/ManagerUI/Menu/Souls/CarsView.aspx?" + BackLink);
        }
    }
}