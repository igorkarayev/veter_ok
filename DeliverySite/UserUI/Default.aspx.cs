using System;
using Delivery.BLL;
using Delivery.BLL.Helpers;
using Delivery.BLL.StaticMethods;
using Delivery.DAL;
using Delivery.DAL.DataBaseObjects;
using DeliverySite.Resources;

namespace Delivery.UserUI
{
    public partial class Default : UserBasePage
    {
        public String DeliveryOnMinskPhones { get; set; }

        public String DeliveryOnMinskSkype { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            //SmsSender.SendSmsBulkIfTicketOnStock("37789");

            Page.Title = PagesTitles.UserDefaultTitle + BackendHelper.TagToValue("page_title_part");
            OtherMethods.ActiveRightMenuStyleChanche("hlMain", this.Page);
            var userInSession = (Users)Session["userinsession"];
            var user = new Users
            {
                ID = UserID
            };

            user.GetById();
            lblUID.Text = user.ID.ToString();
            lblEmail.Text = user.Email;
            lblLogin.Text = user.Login;

            if (user.ManagerID != 0)
            {
                trLogistian.Visible = true;

                var userLogistian = new Users() { ID = Convert.ToInt32(user.ManagerID) };
                lblLogistian.Text =  userLogistian.Name + ", тел. " + userLogistian.PhoneWorkOne;
            }

            if (user.SalesManagerID != 0)
            {
                trManager.Visible = true;

                var userManager = new Users() { ID = Convert.ToInt32(user.SalesManagerID) };
                lblManager.Text = userManager.Family + " " + userManager.Name + ", тел. " + userManager.PhoneWorkOne;
            }

            if (user.AllowApi == 1)
            {
                trApiKey.Visible = true;
                lblApiKey.Text = user.ApiKey;
            }
            if (user.Discount == 0)
            {
                tdDiscount.Visible = false;
            }
            else
            {
                lblDiscount.Text = user.Discount + "%";
            }

            DeliveryOnMinskPhones = BackendHelper.TagToValue("delivery_on_minsk_phones");
            DeliveryOnMinskSkype = BackendHelper.TagToValue("delivery_on_minsk_skype");
            
            //пересчитываем просмотренные новости
            //обновляем\задаем авторизационную куку с данными пользователя
            AuthenticationMethods.SetUserCookie(userInSession);
        }
    }
}