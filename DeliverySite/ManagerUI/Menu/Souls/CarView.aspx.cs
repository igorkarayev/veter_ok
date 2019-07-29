using Delivery.BLL.Helpers;
using Delivery.BLL.StaticMethods;
using Delivery.DAL;
using Delivery.DAL.DataBaseObjects;
using DeliverySite.Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Delivery.ManagerUI.Menu.Souls
{
    public partial class CarView : ManagerBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = PagesTitles.ManagerCarView + BackendHelper.TagToValue("page_title_part");
            OtherMethods.ActiveRightMenuStyleChanche("hlCars", this.Page);
            OtherMethods.ActiveRightMenuStyleChanche("hlSouls", this.Page);

            #region Блок доступа к странице
            var userInSession = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());
            if (currentRole.PageCarView != 1)
            {
                Response.Redirect("~/Error.aspx?id=1");
            }
            #endregion

            if (Page.Request.Params["id"] != null)
            {
                if (!IsPostBack)
                {
                    var id = Convert.ToInt32(Page.Request.Params["id"]);
                    var car = new Cars { ID = id };
                    car.GetById();
                    lblID.Text = car.ID.ToString();
                    var dm = new DataManager();
                    var driversForCarTable = dm.QueryWithReturnDataSet(String.Format("SELECT ID, FirstName, LastName, ThirdName FROM drivers WHERE CarID = {0}", id));
                    foreach (DataRow driver in driversForCarTable.Tables[0].Rows)
                    {
                        lblDrivers.Text += String.Format("<a href='DriversEdit.aspx?id={3}'>{0} {1}.{2}.</a>&nbsp; &nbsp;",
                            driver["FirstName"],
                            driver["LastName"].ToString().Remove(1, driver["LastName"].ToString().Length - 1),
                            driver["ThirdName"].ToString().Remove(1, driver["ThirdName"].ToString().Length - 1),
                            driver["ID"]);
                    }

                    lblType.Text = CarsHelper.CarTypeToFullString(Convert.ToInt32(car.TypeID));
                    hfTypeID.Value = car.TypeID.ToString();
                    lblModel.Text = car.Model;
                    lblNumber.Text = car.Number;

                    lblCompanyName.Text = car.CompanyName;

                    lblFirstName.Text = car.FirstName;
                    lblLastName.Text = car.LastName;
                    lblThirdName.Text = car.ThirdName;
                    lblPassport.Text = car.PassportSeria + car.PassportNumber;
                    lblPersonalNumber.Text = car.PersonalNumber;
                    lblROVD.Text = car.ROVD;
                    lblRegistrationAddress.Text = car.RegistrationAddress;
                    lblValidity.Text = Convert.ToDateTime(car.Validity).ToString("dd-MM-yyyy");
                    lblBirthDay.Text = Convert.ToDateTime(car.BirthDay).ToString("dd-MM-yyyy");
                    lblDateOfIssue.Text = Convert.ToDateTime(car.DateOfIssue).ToString("dd-MM-yyyy");
                }
            }
        }
    }
}