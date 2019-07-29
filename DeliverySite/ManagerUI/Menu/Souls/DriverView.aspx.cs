using Delivery.BLL.Helpers;
using Delivery.BLL.StaticMethods;
using Delivery.DAL.DataBaseObjects;
using DeliverySite.Resources;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Delivery.ManagerUI.Menu.Souls
{
    public partial class DriverView : ManagerBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = PagesTitles.ManagerDriverViewTitle + BackendHelper.TagToValue("page_title_part");
            OtherMethods.ActiveRightMenuStyleChanche("hlDrivers", this.Page);
            OtherMethods.ActiveRightMenuStyleChanche("hlSouls", this.Page);

            #region Блок доступа к странице
            var userInSession = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());
            if (currentRole.PageDriverView != 1)
            {
                Response.Redirect("~/Error.aspx?id=1");
            }
            #endregion

            if (Page.Request.Params["id"] != null)
            {
                if (!IsPostBack)
                {

                    var driver = new Drivers { ID = Convert.ToInt32(Page.Request.Params["id"]) };
                    driver.GetById();
                    lblID.Text = driver.ID.ToString();
                    lblStatus.Text = DriversHelper.DriverStatusToText(Convert.ToInt32(driver.StatusID));
                    lblCar.Text = CarsHelper.CarIdToModelName(driver.CarID.ToString());
                    hlCar.NavigateUrl = "~/ManagerUI/CarView.aspx?id=" + driver.CarID;

                    lblFIO.Text = String.Format("{0} {1} {2}", driver.FirstName, driver.LastName, driver.ThirdName);
                    lblPhoneOne.Text = driver.PhoneOne;
                    lblPhoneTwo.Text = driver.PhoneTwo;
                    lblHomePhone.Text = driver.HomePhone;
                    lblHomeAddress.Text = driver.HomeAddress;
                    lblBirthDay.Text = Convert.ToDateTime(driver.BirthDay).ToString("dd-MM-yyyy");
                    lblContactPersonFIO.Text = driver.ContactPersonFIO;
                    lblContactPersonPhone.Text = driver.ContactPersonPhone;

                    lblPassportData.Text = String.Format("{0}{1}", driver.PassportSeria, driver.PassportNumber);
                    lblPersonalNumber.Text = driver.PersonalNumber;
                    lblROVD.Text = driver.ROVD;
                    lblDateOfIssue.Text = Convert.ToDateTime(driver.DateOfIssue).ToString("dd-MM-yyyy");
                    lblValidity.Text = Convert.ToDateTime(driver.Validity).ToString("dd-MM-yyyy");
                    lblRegistrationAddress.Text = driver.RegistrationAddress;

                    lblDriverPassport.Text = driver.DriverPassport;
                    lblDriverPassportDateOfIssue.Text = Convert.ToDateTime(driver.DriverPassportDateOfIssue).ToString("dd-MM-yyyy");
                    lblDriverPassportValidity.Text = Convert.ToDateTime(driver.DriverPassportValidity).ToString("dd-MM-yyyy");
                    lblMedPolisDateOfIssue.Text = Convert.ToDateTime(driver.MedPolisDateOfIssue).ToString("dd-MM-yyyy");
                    lblMedPolisValidity.Text = Convert.ToDateTime(driver.MedPolisValidity).ToString("dd-MM-yyyy");
                }
            }
        }

        protected void btnEdit_click(object sender, EventArgs e)
        {
            Response.Redirect(String.Format("~/ManagerUI/Menu/Souls/DriversEdit.aspx?id={0}", Page.Request.Params["id"]));
        }
    }
}