using Delivery.BLL.Helpers;
using Delivery.DAL.DataBaseObjects;
using DeliverySite.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Delivery.Resources;

namespace Delivery.ManagerUI.Controls
{
    public partial class MainMenu : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var userInSession = (Users)Session["userinsession"];

            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());

            #region Контент
            liNewsView.Visible = currentRole.PageNewsView != 0;
            liPagesView.Visible = currentRole.PagePagesView != 0;
            liNotificationsView.Visible = currentRole.PageNotificationsView != 0;
            liEmailNotificationsView.Visible = currentRole.PageEmailNotificationsView != 0;
            liErrorsLogView.Visible = currentRole.PageErrorsLogView != 0;
            liLogsView.Visible = currentRole.PageLogsView != 0;
            liApiLogs.Visible = currentRole.PageApiLogView != 0;
            #endregion

            #region Сущности
            liCategoryView.Visible = currentRole.PageCategoryView != 0;
            liTitlesView.Visible = currentRole.PageTitlesView != 0;
            liCityView.Visible = currentRole.PageCityView != 0;
            liDistricts.Visible = currentRole.PageDistrictsView != 0;
            liTracksView.Visible = currentRole.PageTracksView != 0;
            liDriversView.Visible = currentRole.PageDriversView != 0;
            liCarsView.Visible = currentRole.PageCarsView != 0;
            liManagersView.Visible = currentRole.PageManagersView != 0;
            liClientsView.Visible = currentRole.PageClientsView != 0;
            liFeedback.Visible = currentRole.PageFeedbacksView != 0;
            liWarehouses.Visible = currentRole.PageWarehousesView != 0;
            #endregion

            #region Заявки
            liMyTickets.Visible = currentRole.PageMyTickets != 0;
            liUserTicketView.Visible = currentRole.PageUserTicketView != 0;
            liUserTicketNotProcessedView.Visible = currentRole.PageUserTicketNotProcessedView != 0;
            liUserTicketByDeliveryOnBelarus.Visible = currentRole.PageUserTicketByDeliveryOnBelarus != 0;
            liUserTicketByDeliveryOnMinsk.Visible = currentRole.PageUserTicketByDeliveryOnMinsk != 0;
            #endregion

            #region Финансы
            liMoneyView.Visible = currentRole.PageMoneyView != 0;
            liMoneyDriverView.Visible = currentRole.PageMoneyDriverView != 0;
            liMoneyDeliveredView.Visible = currentRole.PageMoneyDeliveredView != 0;
            liMoneyGruzobozView.Visible = currentRole.PageMoneyDeliveryView != 0;
            liCalculation.Visible = currentRole.PageCalculationView != 0;
            #endregion

            #region Выдача
            liIssuanceView.Visible = currentRole.PageIssuanceView != 0;
            liNewIssuanceView.Visible = currentRole.PageNewIssuanceView != 0;
            liIssuanceListsView.Visible = currentRole.PageIssuanceListsView != 0;
            liIssuanceListsEdit.Visible = currentRole.PageIssuanceListsEdit != 0;
            #endregion

            #region Документы
            liReportsArchive.Visible = currentRole.PageReportsView != 0;
            liReportsExport.Visible = currentRole.PageReportsExport != 0;
            liSendComProp.Visible = currentRole.PageSendComProp != 0;
            #endregion

            #region Настройки
            liChangePasswords.Visible = currentRole.PageChangePasswords != 0;
            liUserDiscountView.Visible = currentRole.PageUserDiscountView != 0;
            liRolesView.Visible = currentRole.PageRolesView != 0;
            liBackendView.Visible = currentRole.PageBackendView != 0;
            #endregion

            #region Кабинет клиента
            hlUserUI.Visible = currentRole.SectionUser != 0;
            #endregion

            hlAllDeliveryMoney.Text = hlAllDeliveryMoney.Text.Replace("ÑÒÀÒÓÑ", TicketStatusesResources.Processed);
            hlSite.NavigateUrl = string.Format("http://{0}", BackendHelper.TagToValue("current_app_address"));
        }

        public void Logoff(object sender, EventArgs e)
        {
            var cookie = new HttpCookie("_AUTHGRB")
            {
                Expires = DateTime.Now.AddDays(-1000)
            };
            Response.Cookies.Add(cookie);
            Session["userinsession"] = null;
            Response.Redirect("~/");
        }
    }
}

