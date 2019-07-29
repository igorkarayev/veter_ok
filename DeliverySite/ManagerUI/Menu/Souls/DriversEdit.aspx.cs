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
    public partial class DriversEdit : ManagerBasePage
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
            OtherMethods.ActiveRightMenuStyleChanche("hlDrivers", this.Page);
            OtherMethods.ActiveRightMenuStyleChanche("hlSouls", this.Page);
            Page.Title = Page.Request.Params["id"] != null ? PagesTitles.ManagerDriversEdit + BackendHelper.TagToValue("page_title_part") : PagesTitles.ManagerDriversCreate + BackendHelper.TagToValue("page_title_part");

            #region Блок доступа к странице
            var userInSession = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());
            if (currentRole.PageDriversEdit != 1)
            {
                Response.Redirect("~/Error.aspx?id=1");
            }
            #endregion

            BackLink = DriversHelper.BackDriverLinkBuilder(Page.Request.Params["did"], Page.Request.Params["phone"], Page.Request.Params["statusid"], Page.Request.Params["firstname"]);

            if (!IsPostBack)
            {
                ddlStatus.DataSource = Drivers.DriverStatuses;
                ddlStatus.DataTextField = "Value";
                ddlStatus.DataValueField = "Key";
                ddlStatus.DataBind();

                var cars = new Cars();
                var ds = cars.GetAllItems("Model", "ASC", null);
                ds.Tables[0].Columns.Add("ModelAndNumber", typeof(string), "Model + ' ' + Number");
                ddlCar.DataSource = ds;
                ddlCar.DataTextField = "ModelAndNumber";
                ddlCar.DataValueField = "ID";
                ddlCar.DataBind();
                ddlCar.Items.Add(new ListItem("Не назначена", "0"));
            }

            if (Page.Request.Params["id"] != null)
            {
                var driver = new Drivers { ID = Convert.ToInt32(Page.Request.Params["id"]) };
                driver.GetById();
                if (String.IsNullOrEmpty(driver.FirstName)) Page.Response.Redirect("~/ManagerUI/Menu/Souls/DriversView.aspx?" + BackLink);
                if (!IsPostBack)
                {
                    ddlCar.SelectedValue = driver.CarID.ToString();
                    ddlStatus.SelectedValue = driver.StatusID.ToString();

                    tbFirstName.Text = driver.FirstName;
                    tbLastName.Text = driver.LastName;
                    tbThirdName.Text = driver.ThirdName;
                    tbPhoneOne.Text = driver.PhoneOne;
                    tbPhoneTwo.Text = driver.PhoneTwo;
                    tbPhoneHome.Text = driver.HomePhone;
                    tbHomeAddress.Text = driver.HomeAddress;
                    tbBirthDay.Text = Convert.ToDateTime(driver.BirthDay).ToString("dd-MM-yyyy");
                    tbContactPersonFIO.Text = driver.ContactPersonFIO;
                    tbContactPersonPhone.Text = driver.ContactPersonPhone;

                    tbPassportSeria.Text = driver.PassportSeria;
                    tbPassportNumber.Text = driver.PassportNumber;
                    tbPersonalNumber.Text = driver.PersonalNumber;
                    tbROVD.Text = driver.ROVD;
                    tbDateOfIssue.Text = Convert.ToDateTime(driver.DateOfIssue).ToString("dd-MM-yyyy");
                    tbValidity.Text = Convert.ToDateTime(driver.Validity).ToString("dd-MM-yyyy");
                    tbRegistrationAddress.Text = driver.RegistrationAddress;


                    tbDriverPassport.Text = hfDriverPassport.Value = driver.DriverPassport;
                    tbDriverPassportDateOfIssue.Text = Convert.ToDateTime(driver.DriverPassportDateOfIssue).ToString("dd-MM-yyyy");
                    tbDriverPassportValidity.Text = Convert.ToDateTime(driver.DriverPassportValidity).ToString("dd-MM-yyyy");
                    tbMedPolisDateOfIssue.Text = Convert.ToDateTime(driver.MedPolisDateOfIssue).ToString("dd-MM-yyyy");
                    tbMedPolisValidity.Text = Convert.ToDateTime(driver.MedPolisValidity).ToString("dd-MM-yyyy");
                }
            }
        }

        public void bntCreate_Click(Object sender, EventArgs e)
        {
            var id = Page.Request.Params["id"];
            var userInSession = (Users)Session["userinsession"];
            var driver = new Drivers
            {
                CarID = Convert.ToInt32(ddlCar.SelectedValue),
                StatusID = Convert.ToInt32(ddlStatus.SelectedValue),

                FirstName = tbFirstName.Text.Trim(),
                LastName = tbLastName.Text.Trim(),
                ThirdName = tbThirdName.Text.Trim(),
                PhoneOne = tbPhoneOne.Text.Trim(),
                PhoneTwo = tbPhoneTwo.Text.Trim(),
                HomePhone = tbPhoneHome.Text.Trim(),
                HomeAddress = tbHomeAddress.Text.Trim(),
                ContactPersonFIO = tbContactPersonFIO.Text.Trim(),
                ContactPersonPhone = tbContactPersonPhone.Text.Trim(),

                PassportSeria = tbPassportSeria.Text.Trim(),
                PassportNumber = tbPassportNumber.Text.Trim(),
                PersonalNumber = tbPersonalNumber.Text.Trim(),
                ROVD = tbROVD.Text.Trim(),
                RegistrationAddress = tbRegistrationAddress.Text.Trim(),

                DriverPassport = tbDriverPassport.Text.Trim(),
            };

            if (!String.IsNullOrEmpty(tbBirthDay.Text))
                driver.BirthDay = Convert.ToDateTime(tbBirthDay.Text);

            if (!String.IsNullOrEmpty(tbValidity.Text))
                driver.Validity = Convert.ToDateTime(tbValidity.Text);

            if (!String.IsNullOrEmpty(tbDateOfIssue.Text))
                driver.DateOfIssue = Convert.ToDateTime(tbDateOfIssue.Text);

            if (!String.IsNullOrEmpty(tbDriverPassportDateOfIssue.Text))
                driver.DriverPassportDateOfIssue = Convert.ToDateTime(tbDriverPassportDateOfIssue.Text);

            if (!String.IsNullOrEmpty(tbDriverPassportValidity.Text))
                driver.DriverPassportValidity = Convert.ToDateTime(tbDriverPassportValidity.Text);

            if (!String.IsNullOrEmpty(tbMedPolisDateOfIssue.Text))
                driver.MedPolisDateOfIssue = Convert.ToDateTime(tbMedPolisDateOfIssue.Text);

            if (!String.IsNullOrEmpty(tbMedPolisValidity.Text))
                driver.MedPolisValidity = Convert.ToDateTime(tbMedPolisValidity.Text);

            if (id == null)
            {
                var sameDriver = new Drivers { DriverPassport = tbDriverPassport.Text.Trim() };
                var sameDriverTable = sameDriver.GetAllByDriverPassport();
                if (sameDriverTable.Tables[0].Rows.Count > 0)
                {
                    lblError.Text = "Водитель с таким ВУ уже существует в базе!";
                    return;
                }
                driver.CreateDate = DateTime.Now;
                driver.Create();
            }
            else
            {
                var sameDriver = new Drivers { DriverPassport = tbDriverPassport.Text.Trim() };
                var sameDriverTable = sameDriver.GetAllByDriverPassport();
                if (sameDriverTable.Tables[0].Rows.Count > 0 && tbDriverPassport.Text.Trim() != hfDriverPassport.Value)
                {
                    lblError.Text = "Водитель с таким ВУ уже существует в базе!";
                    return;
                }
                driver.ID = Convert.ToInt32(id);
                driver.ChangeDate = DateTime.Now;
                driver.Update(userInSession.ID, OtherMethods.GetIPAddress(), "DriversEdit");
            }
            Page.Response.Redirect("~/ManagerUI/Menu/Souls/DriversView.aspx?" + BackLink);
        }
    }
}