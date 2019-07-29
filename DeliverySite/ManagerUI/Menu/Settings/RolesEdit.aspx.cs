using Delivery.BLL.Helpers;
using Delivery.BLL.StaticMethods;
using Delivery.DAL;
using Delivery.DAL.DataBaseObjects;
using DeliverySite.Resources;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Delivery.ManagerUI.Menu.Settings
{
    public partial class RolesEdit : ManagerBasePage
    {
        protected string ButtonText { get; set; }

        protected string ActionText { get; set; }

        protected void Page_Init(object sender, EventArgs e)
        {
            ButtonText = Page.Request.Params["id"] != null ? "Сохранить" : "Добавить";
            ActionText = Page.Request.Params["id"] != null ? "Редактирование" : "Добавление";
            btnCreate.Click += bntCreate_Click;
            DataBind();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = Page.Request.Params["id"] != null ? PagesTitles.ManagerRolesEdit + BackendHelper.TagToValue("page_title_part") : PagesTitles.ManagerRolesCreate + BackendHelper.TagToValue("page_title_part");
            OtherMethods.ActiveRightMenuStyleChanche("hlSettings", this.Page);
            OtherMethods.ActiveRightMenuStyleChanche("hlRoles", this.Page);

            #region Блок доступа к странице
            var userInSession = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());
            if (currentRole.PageRolesEdit != 1)
            {
                Response.Redirect("~/Error.aspx?id=1");
            }
            #endregion

            if (Page.Request.Params["id"] != null)
            {
                var role = new Roles() { ID = Convert.ToInt32(Page.Request.Params["id"]) };
                role.GetById();
                if (!IsPostBack)
                {
                    tbName.Text = role.Name;
                    tbNameOnRuss.Text = role.NameOnRuss;

                    var dm = new DataManager();
                    var usersInRoleCount = Convert.ToInt32(dm.QueryWithReturnDataSet(String.Format("select count(*) from `users` where `role`= '{0}';", role.Name)).Tables[0].Rows[0][0].ToString());
                    if (usersInRoleCount > 0)
                    {
                        tbName.Enabled = false;
                    }

                    if (role.IsBase == 1)
                    {
                        tbName.Enabled = false;
                        tbNameOnRuss.Enabled = false;
                        lblIsBase.Text = "<span style=\"font-style: italic; font-size: 10px;\">(базовая)</span>";
                    }

                    #region Контент
                    if (role.PageNewsView == 1)
                    {
                        cbPageNewsView.Checked = true;
                    }
                    if (role.PageNewsEdit == 1)
                    {
                        cbPageNewsEdit.Checked = true;
                    }
                    if (role.ActionNewsDelete == 1)
                    {
                        cbActionNewsDelete.Checked = true;
                    }


                    if (role.PagePagesView == 1)
                    {
                        cbPagePagesView.Checked = true;
                    }
                    if (role.PagePagesEdit == 1)
                    {
                        cbPagePagesEdit.Checked = true;
                    }


                    if (role.PageNotificationsView == 1)
                    {
                        cbPageNotificationsView.Checked = true;
                    }
                    if (role.PageNotificationsEdit == 1)
                    {
                        cbPageNotificationsEdit.Checked = true;
                    }


                    if (role.PageEmailNotificationsView == 1)
                    {
                        cbPageEmailNotificationsView.Checked = true;
                    }
                    if (role.PageEmailNotificationsEdit == 1)
                    {
                        cbPageEmailNotificationsEdit.Checked = true;
                    }


                    if (role.PageErrorsLogView == 1)
                    {
                        cbPageErrorsLogView.Checked = true;
                    }
                    if (role.PageErrorsLogEdit == 1)
                    {
                        cbPageErrorsLogEdit.Checked = true;
                    }
                    if (role.ActionErrorsLogDelete == 1)
                    {
                        cbActionErrorsLogDelete.Checked = true;
                    }


                    if (role.PageLogsView == 1)
                    {
                        cbPageLogsView.Checked = true;
                    }


                    if (role.PageApiLogView == 1)
                    {
                        cbPageApiLogView.Checked = true;
                    }
                    if (role.AllowBlockingAddApiAccess == 1)
                    {
                        cbAllowBlockingAddApiAccess.Checked = true;
                    }
                    #endregion

                    #region Сущности
                    if (role.PageCategoryView == 1)
                    {
                        cbPageCategoryView.Checked = true;
                    }
                    if (role.PageCategoryEdit == 1)
                    {
                        cbPageCategoryEdit.Checked = true;
                    }
                    if (role.ActionCategoryDelete == 1)
                    {
                        cbActionCategoryDelete.Checked = true;
                    }



                    if (role.PageTitlesView == 1)
                    {
                        cbPageTitlesView.Checked = true;
                    }
                    if (role.PageTitlesEdit == 1)
                    {
                        cbPageTitlesEdit.Checked = true;
                    }
                    if (role.ActionTitlesDelete == 1)
                    {
                        cbActionTitlesDelete.Checked = true;
                    }



                    if (role.PageCityView == 1)
                    {
                        cbPageCityView.Checked = true;
                    }
                    if (role.PageCityEdit == 1)
                    {
                        cbPageCityEdit.Checked = true;
                    }
                    if (role.ActionCityDelete == 1)
                    {
                        cbActionCityDelete.Checked = true;
                    }



                    if (role.PageDistrictsView == 1)
                    {
                        cbPageDistrictsView.Checked = true;
                    }
                    if (role.PageDistrictEdit == 1)
                    {
                        cbPageDistrictEdit.Checked = true;
                    }



                    if (role.PageTracksView == 1)
                    {
                        cbPageTracksView.Checked = true;
                    }
                    if (role.PageTracksEdit == 1)
                    {
                        cbPageTracksEdit.Checked = true;
                    }
                    if (role.ActionTracksDelete == 1)
                    {
                        cbActionTracksDelete.Checked = true;
                    }



                    if (role.PageWarehousesView == 1)
                    {
                        cbPageWarehousesView.Checked = true;
                    }
                    if (role.PageWarehouseEdit == 1)
                    {
                        cbPageWarehouseEdit.Checked = true;
                    }
                    if (role.ActionDeleteWarehouses == 1)
                    {
                        cbActionDeleteWarehouses.Checked = true;
                    }



                    if (role.PageDriversView == 1)
                    {
                        cbPageDriversView.Checked = true;
                    }
                    if (role.PageDriversEdit == 1)
                    {
                        cbPageDriversEdit.Checked = true;
                    }
                    if (role.ActionDriversDelete == 1)
                    {
                        cbActionDriversDelete.Checked = true;
                    }
                    if (role.PageDriverView == 1)
                    {
                        cbPageDriverView.Checked = true;
                    }


                    if (role.PageCarsView == 1)
                    {
                        cbPageCarsView.Checked = true;
                    }
                    if (role.PageCarEdit == 1)
                    {
                        cbPageCarEdit.Checked = true;
                    }
                    if (role.ActionCarsDelete == 1)
                    {
                        cbActionCarsDelete.Checked = true;
                    }
                    if (role.PageCarView == 1)
                    {
                        cbPageCarView.Checked = true;
                    }


                    if (role.PageManagersView == 1)
                    {
                        cbPageManagersView.Checked = true;
                    }
                    if (role.PageManagerView == 1)
                    {
                        cbPageManagerView.Checked = true;
                    }
                    if (role.PageManagerEdit == 1)
                    {
                        cbPageManagersEdit.Checked = true;
                    }
                    if (role.ActionManagersDelete == 1)
                    {
                        cbActionManagersDelete.Checked = true;
                    }


                    if (role.PageClientsView == 1)
                    {
                        cbPageClientsView.Checked = true;
                    }
                    if (role.PageClientsEdit == 1)
                    {
                        cbPageClientsEdit.Checked = true;
                    }
                    if (role.PageClientsCreate == 1)
                    {
                        cbPageClientsCreate.Checked = true;
                    }
                    if (role.ActionClientsDelete == 1)
                    {
                        cbActionClientsDelete.Checked = true;
                    }
                    if (role.ActionAddManagerToUser == 1)
                    {
                        cbActionAddManagerToUser.Checked = true;
                    }
                    if (role.ActionAddSalesManagerToUser == 1)
                    {
                        cbActionAddSalesManagerToUser.Checked = true;
                    }
                    if (role.ActionCategoryAssignToUser == 1)
                    {
                        cbActionCategoryAssignToUser.Checked = true;
                    }
                    if (role.ActionClientActivateBlock == 1)
                    {
                        cbActionClientActivateBlock.Checked = true;
                    }
                    if (role.ViewClientStatInfo == 1)
                    {
                        cbViewClientStatInfo.Checked = true;
                    }
                    if (role.PageUserProfileView == 1)
                    {
                        cbPageUserProfileView.Checked = true;
                    }
                    if (role.PageUserProfileEdit == 1)
                    {
                        cbPageUserProfileEdit.Checked = true;
                    }
                    if (role.ActionUserProfileDelete == 1)
                    {
                        cbActionUserProfileDelete.Checked = true;
                    }
                    if (role.ActionUserProfileChangeStatus == 1)
                    {
                        cbActionUserProfileChangeStatus.Checked = true;
                    }



                    if (role.PageFeedbacksView == 1)
                    {
                        cbPageFeedbacksView.Checked = true;
                    }
                    if (role.Feedback0 == 1)
                    {
                        cbFeedback0.Checked = true;
                    }
                    if (role.Feedback1 == 1)
                    {
                        cbFeedback1.Checked = true;
                    }
                    if (role.Feedback2 == 1)
                    {
                        cbFeedback2.Checked = true;
                    }
                    if (role.Feedback3 == 1)
                    {
                        cbFeedback3.Checked = true;
                    }
                    if (role.Feedback4 == 1)
                    {
                        cbFeedback4.Checked = true;
                    }
                    if (role.Feedback5 == 1)
                    {
                        cbFeedback5.Checked = true;
                    }
                    if (role.Feedback6 == 1)
                    {
                        cbFeedback6.Checked = true;
                    }
                    if (role.FeedbackFullAccess == 1)
                    {
                        cbFeedbackFullAccess.Checked = true;
                    }



                    if (role.ActionProviderEdit == 1)
                    {
                        cbActionProviderEdit.Checked = true;
                    }
                    if (role.ActionProviderDelete == 1)
                    {
                        cbActionProviderDelete.Checked = true;
                    }
                    #endregion

                    #region Заявки
                    if (role.PageMyTickets == 1)
                    {
                        cbPageMyTickets.Checked = true;
                    }
                    if (role.PageUserTicketView == 1)
                    {
                        cbPageUserTicketView.Checked = true;
                    }
                    if (role.PageUserTicketNotProcessedView == 1)
                    {
                        cbPageUserTicketNotProcessedView.Checked = true;
                    }
                    if (role.PageUserTicketByDeliveryOnMinsk == 1)
                    {
                        cbPageUserTicketByDeliveryOnMinsk.Checked = true;
                    }
                    if (role.PageUserTicketByDeliveryOnBelarus == 1)
                    {
                        cbPageUserTicketByDeliveryOnBelarus.Checked = true;
                    }
                    if (role.PageUserTicketEdit == 1)
                    {
                        cbPageUserTicketEdit.Checked = true;
                    }
                    if (role.ActionDisallowTicketChangeWithoutManagerInfo == 1)
                    {
                        cbActionDisallowTicketChangeWithoutManagerInfo.Checked = true;
                    }
                    if (role.ActionUserTicketDelete == 1)
                    {
                        cbActionUserTicketDelete.Checked = true;
                    }
                    if (role.ActionCompletedStatus == 1)
                    {
                        cbActionCompletedStatus.Checked = true;
                    }
                    if (role.ActionDisallowDeliveredToCompletedStatus == 1)
                    {
                        cbActionDisallowDeliveredToCompletedStatus.Checked = true;
                    }
                    if (role.ActionAllowChangeInCompletedWithoutDriver == 1)
                    {
                        cbActionAllowChangeInCompletedWithoutDriver.Checked = true;
                    }
                    if (role.ActionDisallowEditSomeFieldInTickets == 1)
                    {
                        cbActionDisallowEditSomeFieldInTickets.Checked = true;
                    }
                    if (role.ActionAllowChangeMoneyAndCourse == 1)
                    {
                        cbActionAllowChangeMoneyAndCourse.Checked = true;
                    }
                    if (role.ActionSendSmsBulk == 1)
                    {
                        cbActionSendSmsBulk.Checked = true;
                    }
                    if (role.ActionDeliveredButtonInCity == 1)
                    {
                        cbActionDeliveredButtonInCity.Checked = true;
                    }
                    #endregion

                    #region Финансы
                    if (role.PageMoneyView == 1)
                    {
                        cbPageMoneyView.Checked = true;
                    }
                    if (role.PageMoneyDriverView == 1)
                    {
                        cbPageMoneyDriverView.Checked = true;
                    }
                    if (role.PageMoneyDeliveredView == 1)
                    {
                        cbPageMoneyDeliveredView.Checked = true;
                    }
                    if (role.PageMoneyDeliveryView == 1)
                    {
                        cbPageMoneyDeliveryView.Checked = true;
                    }
                    if (role.PageCalculationView == 1)
                    {
                        cbPageCalculationView.Checked = true;
                    }
                    #endregion

                    #region Выдача
                    if (role.PageIssuanceView == 1)
                    {
                        cbPageIssuanceView.Checked = true;
                    }
                    if (role.PageNewIssuanceView == 1)
                    {
                        cbPageNewIssuanceView.Checked = true;
                    }
                    if (role.PageIssuanceListsView == 1)
                    {
                        cbPageIssuanceListsView.Checked = true;
                    }
                    if (role.PageIssuanceListsEdit == 1)
                    {
                        cbPageIssuanceListsEdit.Checked = true;
                    }
                    if (role.PageIssuanceListView == 1)
                    {
                        cbPageIssuanceListView.Checked = true;
                    }
                    if (role.ActionIssuanceListDelete == 1)
                    {
                        cbActionIssuanceListDelete.Checked = true;
                    }
                    #endregion

                    #region Документы
                    if (role.ViewRasch == 1)
                    {
                        cbRasch.Checked = true;
                    }
                    if (role.PageReportsView == 1)
                    {
                        cbPageReportsView.Checked = true;
                    }
                    if (role.PageReportsEdit == 1)
                    {
                        cbPageReportsEdit.Checked = true;
                    }
                    if (role.ActionReportsDelete == 1)
                    {
                        cbActionReportsDelete.Checked = true;
                    }
                    if (role.PageReportsExport == 1)
                    {
                        cbPageReportsExport.Checked = true;
                    }
                    if (role.ActionExportAllUsersInfo == 1)
                    {
                        cbActionExportAllUsersInfo.Checked = true;
                    }
                    if (role.ActionExportAllUsersProfilesInfo == 1)
                    {
                        cbActionExportAllUsersProfilesInfo.Checked = true;
                    }
                    if (role.ActionExportAllClientsInfo == 1)
                    {
                        cbActionExportAllClientsInfo.Checked = true;
                    }
                    if (role.PageSendComProp == 1)
                    {
                        cbPageSendComProp.Checked = true;
                    }
                    #endregion

                    #region Настройки
                    if (role.PageChangePasswords == 1)
                    {
                        cbPageChangePasswords.Checked = true;
                    }

                    if (role.PageUserDiscountView == 1)
                    {
                        cbPageUserDiscountView.Checked = true;
                    }

                    if (role.PageRolesView == 1)
                    {
                        cbPageRolesView.Checked = true;
                    }
                    if (role.PageRolesEdit == 1)
                    {
                        cbPageRolesEdit.Checked = true;
                    }
                    if (role.ActionRolesDelete == 1)
                    {
                        cbActionRolesDelete.Checked = true;
                    }

                    if (role.PageBackendView == 1)
                    {
                        cbPageBackendView.Checked = true;
                    }
                    if (role.PageBackendEdit == 1)
                    {
                        cbPageBackendEdit.Checked = true;
                    }
                    #endregion

                    #region Юзверь
                    if (role.SectionUser == 1)
                    {
                        cbSectionUser.Checked = true;
                    }
                    #endregion

                    #region Плавоющее меню действий над заявками
                    if (role.ActionGroupUserTicketDelete == 1)
                    {
                        cbActionGroupUserTicketDelete.Checked = true;
                    }
                    if (role.ActionDriverAdd == 1)
                    {
                        cbActionDriverAdd.Checked = true;
                    }
                    if (role.ActionStatusAdd == 1)
                    {
                        cbActionStatusAdd.Checked = true;
                    }
                    if (role.ActionPrintVinil == 1)
                    {
                        cbActionPrintVinil.Checked = true;
                    }
                    if (role.ActionPrintCheck == 1)
                    {
                        cbActionPrintCheck.Checked = true;
                    }
                    if (role.ActionPrintMap == 1)
                    {
                        cbActionPrintMap.Checked = true;
                    }
                    if (role.ActionPrintMapForManager == 1)
                    {
                        cbActionPrintMapForManager.Checked = true;
                    }
                    if (role.ActionPrintMapForCashier == 1)
                    {
                        cbActionPrintMapForCashier.Checked = true;
                    }
                    if (role.ActionPrintTickets == 1)
                    {
                        cbActionPrintTickets.Checked = true;
                    }
                    if (role.ActionPrintZP == 1)
                    {
                        cbActionPrintZP.Checked = true;
                    }
                    if (role.ActionPrintNakl == 1)
                    {
                        cbActionPrintNakl.Checked = true;
                    }
                    if (role.ActionPrintNaklPril == 1)
                    {
                        cbActionPrintNaklPril.Checked = true;
                    }
                    if (role.ActionPrintPutFirst == 1)
                    {
                        cbActionPrintPutFirst.Checked = true;
                    }
                    if (role.ActionPrintPutSecond == 1)
                    {
                        cbActionPrintPutSecond.Checked = true;
                    }
                    if (role.ActionPrintAORT == 1)
                    {
                        cbActionPrintAORT.Checked = true;
                    }
                    #endregion

                    #region Другие действия над заявками
                    if (role.ActionControlNN == 1)
                    {
                        cbActionControlNN.Checked = true;
                    }
                    if (role.ActionControlActiza == 1)
                    {
                        cbActionControlActiza.Checked = true;
                    }
                    if (role.ActionControlComment == 1)
                    {
                        cbActionControlComment.Checked = true;
                    }
                    if (role.ActionControlMoneyCheckboxes == 1)
                    {
                        cbActionControlMoneyCheckboxes.Checked = true;
                    }
                    if (role.ActionControlGruzobozCost == 1)
                    {
                        cbActionControlGruzobozCost.Checked = true;
                    }
                    if (role.ActionControlPN == 1)
                    {
                        cbActionControlPN.Checked = true;
                    }
                    if (role.ActionControlWeight == 1)
                    {
                        cbActionControlWeight.Checked = true;
                    }
                    if (role.ActionCheckedOutCheckbox == 1)
                    {
                        cbActionCheckedOutCheckbox.Checked = true;
                    }

                    if (role.ActionPhonedCheckbox == 1)
                    {
                        cbActionPhonedCheckbox.Checked = true;
                    }

                    if (role.ActionBilledCheckbox == 1)
                    {
                        cbActionBilledCheckbox.Checked = true;
                    }
                    #endregion

                    #region Права на видимость статусов
                    if (role.StatusNotProcessed == 1)
                    {
                        cbStatusNotProcessed.Checked = true;
                    }

                    if (role.StatusInStock == 1)
                    {
                        cbStatusInStock.Checked = true;
                    }

                    if (role.StatusOnTheWay == 1)
                    {
                        cbStatusOnTheWay.Checked = true;
                    }

                    if (role.StatusProcessed == 1)
                    {
                        cbStatusProcessed.Checked = true;
                    }

                    if (role.StatusDelivered == 1)
                    {
                        cbStatusDelivered.Checked = true;
                    }

                    if (role.StatusCompleted == 1)
                    {
                        cbStatusCompleted.Checked = true;
                    }

                    if (role.StatusTransferInCourier == 1)
                    {
                        cbStatusTransferInCourier.Checked = true;
                    }

                    if (role.StatusRefusingInCourier == 1)
                    {
                        cbStatusRefusingInCourier.Checked = true;
                    }

                    if (role.StatusExchangeInCourier == 1)
                    {
                        cbStatusExchangeInCourier.Checked = true;
                    }

                    if (role.StatusDeliveryFromClientInCourier == 1)
                    {
                        cbStatusDeliveryFromClientInCourier.Checked = true;
                    }

                    if (role.StatusTransferInStock == 1)
                    {
                        cbStatusTransferInStock.Checked = true;
                    }

                    if (role.StatusReturnInStock == 1)
                    {
                        cbStatusReturnInStock.Checked = true;
                    }

                    if (role.StatusCancelInStock == 1)
                    {
                        cbStatusCancelInStock.Checked = true;
                    }

                    if (role.StatusExchangeInStock == 1)
                    {
                        cbStatusExchangeInStock.Checked = true;
                    }

                    if (role.StatusDeliveryFromClientInStock == 1)
                    {
                        cbStatusDeliveryFromClientInStock.Checked = true;
                    }

                    if (role.StatusCancel == 1)
                    {
                        cbStatusCancel.Checked = true;
                    }

                    if (role.StatusRefusalOnTheWay == 1)
                    {
                        cbStatusRefusalOnTheWay.Checked = true;
                    }

                    if (role.StatusRefusalByAddress == 1)
                    {
                        cbStatusRefusalByAddress.Checked = true;
                    }

                    if (role.StatusUpload == 1)
                    {
                        cbStatusUpload.Checked = true;
                    }
                    #endregion

                    #region Права на видимость столбцов таблицы
                    if (role.TableCost == 1)
                    {
                        cbTableCost.Checked = true;
                    }

                    if (role.TableAgreedCost == 1)
                    {
                        cbTableAgreedCost.Checked = true;
                    }

                    if (role.TableComments == 1)
                    {
                        cbTableComments.Checked = true;
                    }

                    if (role.TableDID == 1)
                    {
                        cbTableDID.Checked = true;
                    }

                    if (role.TableDirection == 1)
                    {
                        cbTableDirection.Checked = true;
                    }

                    if (role.TableGoods == 1)
                    {
                        cbTableGoods.Checked = true;
                    }

                    if (role.TableKK == 1)
                    {
                        cbTableKK.Checked = true;
                    }

                    if (role.TableND == 1)
                    {
                        cbTableND.Checked = true;
                    }

                    if (role.TableNotes == 1)
                    {
                        cbTableNotes.Checked = true;
                    }

                    if (role.TableOtherOptions == 1)
                    {
                        cbTableOtherOptions.Checked = true;
                    }

                    if (role.TablePN == 1)
                    {
                        cbTablePN.Checked = true;
                    }

                    if (role.TableProfile == 1)
                    {
                        cbTableProfile.Checked = true;
                    }

                    if (role.TableRecieveDate == 1)
                    {
                        cbTableRecieveDate.Checked = true;
                    }

                    if (role.TableReciever == 1)
                    {
                        cbTableReciever.Checked = true;
                    }

                    if (role.TableWeight == 1)
                    {
                        cbTableWeight.Checked = true;
                    }

                    if (role.TableStatus == 1)
                    {
                        cbTableStatus.Checked = true;
                    }

                    if (role.TableRecieverAddr == 1)
                    {
                        cbTableRecieverAddr.Checked = true;
                    }

                    if (role.TableRecieverCity == 1)
                    {
                        cbTableRecieverCity.Checked = true;
                    }

                    if (role.TableSendFrom == 1)
                    {
                        cbTableSendFrom.Checked = true;
                    }

                    if (role.TableSendDate == 1)
                    {
                        cbTableSendDate.Checked = true;
                    }

                    if (role.TableUID == 1)
                    {
                        cbTableUID.Checked = true;
                    }
                    #endregion
                }
            }
        }

        public void bntCreate_Click(Object sender, EventArgs e)
        {
            var id = Page.Request.Params["id"];

            #region Инициализация ролей
            var role = new Roles()
            {
                TableNotes = cbTableNotes.Checked ? 1 : 0,
                TableReciever = cbTableReciever.Checked ? 1 : 0,
                TableRecieverCity = cbTableRecieverCity.Checked ? 1 : 0,
                TableSendFrom = cbTableSendFrom.Checked ? 1 : 0,
                TableSendDate = cbTableSendDate.Checked ? 1 : 0,
                TableAgreedCost = cbTableAgreedCost.Checked ? 1 : 0,
                TableComments = cbTableComments.Checked ? 1 : 0,
                TableCost = cbTableCost.Checked ? 1 : 0,
                TableDID = cbTableDID.Checked ? 1 : 0,
                TableDirection = cbTableDirection.Checked ? 1 : 0,
                TableGoods = cbTableGoods.Checked ? 1 : 0,
                TableKK = cbTableKK.Checked ? 1 : 0,
                TableND = cbTableKK.Checked ? 1 : 0,
                TableOtherOptions = cbTableOtherOptions.Checked ? 1 : 0,
                TablePN = cbTablePN.Checked ? 1 : 0,
                TableProfile = cbTableProfile.Checked ? 1 : 0,
                TableRecieveDate = cbTableRecieveDate.Checked ? 1 : 0,
                TableRecieverAddr = cbTableRecieverAddr.Checked ? 1 : 0,
                TableStatus = cbTableStatus.Checked ? 1 : 0,
                TableWeight = cbTableWeight.Checked ? 1 : 0,
                TableUID = cbTableUID.Checked ? 1 : 0,

                PageNewsView = cbPageNewsView.Checked ? 1 : 0,
                PageNewsEdit = cbPageNewsEdit.Checked ? 1 : 0,
                ActionNewsDelete = cbActionNewsDelete.Checked ? 1 : 0,
                PagePagesView = cbPagePagesView.Checked ? 1 : 0,
                PagePagesEdit = cbPagePagesEdit.Checked ? 1 : 0,
                PageNotificationsView = cbPageNotificationsView.Checked ? 1 : 0,
                PageNotificationsEdit = cbPageNotificationsEdit.Checked ? 1 : 0,
                PageEmailNotificationsView = cbPageEmailNotificationsView.Checked ? 1 : 0,
                PageEmailNotificationsEdit = cbPageEmailNotificationsEdit.Checked ? 1 : 0,
                PageErrorsLogView = cbPageErrorsLogView.Checked ? 1 : 0,
                PageErrorsLogEdit = cbPageErrorsLogEdit.Checked ? 1 : 0,
                ActionErrorsLogDelete = cbActionErrorsLogDelete.Checked ? 1 : 0,
                PageLogsView = cbPageLogsView.Checked ? 1 : 0,
                PageApiLogView = cbPageApiLogView.Checked ? 1 : 0,
                AllowBlockingAddApiAccess = cbAllowBlockingAddApiAccess.Checked ? 1 : 0,


                PageCategoryView = cbPageCategoryView.Checked ? 1 : 0,
                PageCategoryEdit = cbPageCategoryEdit.Checked ? 1 : 0,
                ActionCategoryDelete = cbActionCategoryDelete.Checked ? 1 : 0,

                PageTitlesView = cbPageTitlesView.Checked ? 1 : 0,
                PageTitlesEdit = cbPageTitlesEdit.Checked ? 1 : 0,
                ActionTitlesDelete = cbActionTitlesDelete.Checked ? 1 : 0,

                PageCityView = cbPageCityView.Checked ? 1 : 0,
                PageCityEdit = cbPageCityEdit.Checked ? 1 : 0,
                ActionCityDelete = cbActionCityDelete.Checked ? 1 : 0,

                PageDistrictsView = cbPageDistrictsView.Checked ? 1 : 0,
                PageDistrictEdit = cbPageDistrictEdit.Checked ? 1 : 0,

                PageTracksView = cbPageTracksView.Checked ? 1 : 0,
                PageTracksEdit = cbPageTracksEdit.Checked ? 1 : 0,
                ActionTracksDelete = cbActionTracksDelete.Checked ? 1 : 0,

                PageWarehousesView = cbPageWarehousesView.Checked ? 1 : 0,
                PageWarehouseEdit = cbPageWarehouseEdit.Checked ? 1 : 0,
                ActionDeleteWarehouses = cbActionDeleteWarehouses.Checked ? 1 : 0,

                PageDriversView = cbPageDriversView.Checked ? 1 : 0,
                PageDriversEdit = cbPageDriversEdit.Checked ? 1 : 0,
                ActionDriversDelete = cbActionDriversDelete.Checked ? 1 : 0,
                PageDriverView = cbPageDriverView.Checked ? 1 : 0,

                PageCarsView = cbPageCarsView.Checked ? 1 : 0,
                PageCarEdit = cbPageCarEdit.Checked ? 1 : 0,
                ActionCarsDelete = cbActionCarsDelete.Checked ? 1 : 0,
                PageCarView = cbPageCarView.Checked ? 1 : 0,

                PageManagersView = cbPageManagersView.Checked ? 1 : 0,
                PageManagerView = cbPageManagerView.Checked ? 1 : 0,
                PageManagerEdit = cbPageManagersEdit.Checked ? 1 : 0,
                ActionManagersDelete = cbActionManagersDelete.Checked ? 1 : 0,




                PageClientsView = cbPageClientsView.Checked ? 1 : 0,
                PageClientsCreate = cbPageClientsCreate.Checked ? 1 : 0,
                PageClientsEdit = cbPageClientsEdit.Checked ? 1 : 0,
                ActionClientsDelete = cbActionClientsDelete.Checked ? 1 : 0,
                ActionCompletedStatus = cbActionCompletedStatus.Checked ? 1 : 0,
                ActionAddManagerToUser = cbActionAddManagerToUser.Checked ? 1 : 0,
                ActionAddSalesManagerToUser = cbActionAddSalesManagerToUser.Checked ? 1 : 0,
                ActionCategoryAssignToUser = cbActionCategoryAssignToUser.Checked ? 1 : 0,
                ActionClientActivateBlock = cbActionClientActivateBlock.Checked ? 1 : 0,
                ViewClientStatInfo = cbViewClientStatInfo.Checked ? 1 : 0,
                PageUserProfileView = cbPageUserProfileView.Checked ? 1 : 0,
                PageUserProfileEdit = cbPageUserProfileEdit.Checked ? 1 : 0,
                ActionUserProfileChangeStatus = cbActionUserProfileChangeStatus.Checked ? 1 : 0,
                ActionUserProfileDelete = cbActionUserProfileDelete.Checked ? 1 : 0,



                ActionProviderEdit = cbActionProviderEdit.Checked ? 1 : 0,
                ActionProviderDelete = cbActionProviderDelete.Checked ? 1 : 0,




                PageFeedbacksView = cbPageFeedbacksView.Checked ? 1 : 0,
                Feedback0 = cbFeedback0.Checked ? 1 : 0,
                Feedback1 = cbFeedback1.Checked ? 1 : 0,
                Feedback2 = cbFeedback2.Checked ? 1 : 0,
                Feedback3 = cbFeedback3.Checked ? 1 : 0,
                Feedback4 = cbFeedback4.Checked ? 1 : 0,
                Feedback5 = cbFeedback5.Checked ? 1 : 0,
                Feedback6 = cbFeedback6.Checked ? 1 : 0,
                FeedbackFullAccess = cbFeedbackFullAccess.Checked ? 1 : 0,




                PageMyTickets = cbPageMyTickets.Checked ? 1 : 0,
                PageUserTicketView = cbPageUserTicketView.Checked ? 1 : 0,
                PageUserTicketNotProcessedView = cbPageUserTicketNotProcessedView.Checked ? 1 : 0,
                PageUserTicketByDeliveryOnMinsk = cbPageUserTicketByDeliveryOnMinsk.Checked ? 1 : 0,
                PageUserTicketByDeliveryOnBelarus = cbPageUserTicketByDeliveryOnBelarus.Checked ? 1 : 0,
                PageUserTicketEdit = cbPageUserTicketEdit.Checked ? 1 : 0,
                ActionDisallowTicketChangeWithoutManagerInfo = cbActionDisallowTicketChangeWithoutManagerInfo.Checked ? 1 : 0,
                ActionUserTicketDelete = cbActionUserTicketDelete.Checked ? 1 : 0,
                ActionDisallowDeliveredToCompletedStatus = cbActionDisallowDeliveredToCompletedStatus.Checked ? 1 : 0,
                ActionAllowChangeInCompletedWithoutDriver = cbActionAllowChangeInCompletedWithoutDriver.Checked ? 1 : 0,
                ActionDisallowEditSomeFieldInTickets = cbActionDisallowEditSomeFieldInTickets.Checked ? 1 : 0,
                ActionAllowChangeMoneyAndCourse = cbActionAllowChangeMoneyAndCourse.Checked ? 1 : 0,
                ActionSendSmsBulk = cbActionSendSmsBulk.Checked ? 1 : 0,
                ActionDeliveredButtonInCity = cbActionDeliveredButtonInCity.Checked ? 1 : 0,


                PageMoneyView = cbPageMoneyView.Checked ? 1 : 0,
                PageMoneyDriverView = cbPageMoneyDriverView.Checked ? 1 : 0,
                PageMoneyDeliveredView = cbPageMoneyDeliveredView.Checked ? 1 : 0,
                PageMoneyDeliveryView = cbPageMoneyDeliveryView.Checked ? 1 : 0,
                PageCalculationView = cbPageCalculationView.Checked ? 1 : 0,

                PageIssuanceView = cbPageIssuanceView.Checked ? 1 : 0,
                PageNewIssuanceView = cbPageNewIssuanceView.Checked ? 1 : 0,
                PageIssuanceListsView = cbPageIssuanceListsView.Checked ? 1 : 0,
                PageIssuanceListsEdit = cbPageIssuanceListsEdit.Checked ? 1 : 0,
                PageIssuanceListView = cbPageIssuanceListView.Checked ? 1 : 0,
                ActionIssuanceListDelete = cbActionIssuanceListDelete.Checked ? 1 : 0,

                ViewRasch = cbRasch.Checked ? 1 : 0,
                PageReportsView = cbPageReportsView.Checked ? 1 : 0,
                PageReportsEdit = cbPageReportsEdit.Checked ? 1 : 0,
                ActionReportsDelete = cbActionReportsDelete.Checked ? 1 : 0,
                PageReportsExport = cbPageReportsExport.Checked ? 1 : 0,
                ActionExportAllUsersInfo = cbActionExportAllUsersInfo.Checked ? 1 : 0,
                ActionExportAllUsersProfilesInfo = cbActionExportAllUsersProfilesInfo.Checked ? 1 : 0,
                ActionExportAllClientsInfo = cbActionExportAllClientsInfo.Checked ? 1 : 0,
                PageSendComProp = cbPageSendComProp.Checked ? 1 : 0,

                PageChangePasswords = cbPageChangePasswords.Checked ? 1 : 0,
                PageUserDiscountView = cbPageUserDiscountView.Checked ? 1 : 0,
                PageRolesView = cbPageRolesView.Checked ? 1 : 0,
                PageRolesEdit = cbPageRolesEdit.Checked ? 1 : 0,
                ActionRolesDelete = cbActionRolesDelete.Checked ? 1 : 0,
                PageBackendView = cbPageBackendView.Checked ? 1 : 0,
                PageBackendEdit = cbPageBackendEdit.Checked ? 1 : 0,

                ActionGroupUserTicketDelete = cbActionGroupUserTicketDelete.Checked ? 1 : 0,
                ActionDriverAdd = cbActionDriverAdd.Checked ? 1 : 0,
                ActionStatusAdd = cbActionStatusAdd.Checked ? 1 : 0,
                ActionPrintVinil = cbActionPrintVinil.Checked ? 1 : 0,
                ActionPrintCheck = cbActionPrintCheck.Checked ? 1 : 0,
                ActionPrintMap = cbActionPrintMap.Checked ? 1 : 0,
                ActionPrintMapForManager = cbActionPrintMapForManager.Checked ? 1 : 0,
                ActionPrintMapForCashier = cbActionPrintMapForCashier.Checked ? 1 : 0,
                ActionPrintTickets = cbActionPrintTickets.Checked ? 1 : 0,
                ActionPrintZP = cbActionPrintZP.Checked ? 1 : 0,
                ActionPrintNakl = cbActionPrintNakl.Checked ? 1 : 0,
                ActionPrintNaklPril = cbActionPrintNaklPril.Checked ? 1 : 0,
                ActionPrintPutFirst = cbActionPrintPutFirst.Checked ? 1 : 0,
                ActionPrintPutSecond = cbActionPrintPutSecond.Checked ? 1 : 0,
                ActionPrintAORT = cbActionPrintAORT.Checked ? 1 : 0,

                ActionControlNN = cbActionControlNN.Checked ? 1 : 0,
                ActionControlActiza = cbActionControlActiza.Checked ? 1 : 0,
                ActionControlComment = cbActionControlComment.Checked ? 1 : 0,
                ActionControlMoneyCheckboxes = cbActionControlMoneyCheckboxes.Checked ? 1 : 0,
                ActionControlGruzobozCost = cbActionControlGruzobozCost.Checked ? 1 : 0,
                ActionControlPN = cbActionControlPN.Checked ? 1 : 0,
                ActionControlWeight = cbActionControlWeight.Checked ? 1 : 0,
                ActionCheckedOutCheckbox = cbActionCheckedOutCheckbox.Checked ? 1 : 0,
                ActionPhonedCheckbox = cbActionPhonedCheckbox.Checked ? 1 : 0,
                ActionBilledCheckbox = cbActionBilledCheckbox.Checked ? 1 : 0,


                StatusNotProcessed = cbStatusNotProcessed.Checked ? 1 : 0,
                StatusInStock = cbStatusInStock.Checked ? 1 : 0,
                StatusOnTheWay = cbStatusOnTheWay.Checked ? 1 : 0,
                StatusProcessed = cbStatusProcessed.Checked ? 1 : 0,
                StatusDelivered = cbStatusDelivered.Checked ? 1 : 0,
                StatusCompleted = cbStatusCompleted.Checked ? 1 : 0,
                StatusTransferInCourier = cbStatusTransferInCourier.Checked ? 1 : 0,
                StatusRefusingInCourier = cbStatusRefusingInCourier.Checked ? 1 : 0,
                StatusExchangeInCourier = cbStatusExchangeInCourier.Checked ? 1 : 0,
                StatusDeliveryFromClientInCourier = cbStatusDeliveryFromClientInCourier.Checked ? 1 : 0,
                StatusTransferInStock = cbStatusTransferInStock.Checked ? 1 : 0,
                StatusReturnInStock = cbStatusReturnInStock.Checked ? 1 : 0,
                StatusCancelInStock = cbStatusCancelInStock.Checked ? 1 : 0,
                StatusExchangeInStock = cbStatusExchangeInStock.Checked ? 1 : 0,
                StatusDeliveryFromClientInStock = cbStatusDeliveryFromClientInStock.Checked ? 1 : 0,
                StatusCancel = cbStatusCancel.Checked ? 1 : 0,
                StatusRefusalByAddress = cbStatusRefusalByAddress.Checked ? 1 : 0,
                StatusRefusalOnTheWay = cbStatusRefusalOnTheWay.Checked ? 1 : 0,

                SectionUser = cbSectionUser.Checked ? 1 : 0,
            };
            #endregion

            if (id == null)
            {
                role.Name = tbName.Text.Replace("\"", "''");
                role.NameOnRuss = tbNameOnRuss.Text;
                role.Create();
            }
            else
            {
                role.ID = Convert.ToInt32(id);
                var roleOld = new Roles() { ID = role.ID };
                roleOld.GetById();
                if (roleOld.IsBase == 0)
                {
                    role.Name = tbName.Text.Replace("\"", "''");
                    role.NameOnRuss = tbNameOnRuss.Text;
                }
                role.Update();
            }
            //загружаем новые и измененные роли в оперативную память
            var roles = new Roles();
            Application["RolesList"] = roles.GetAllItemsToList();
            Page.Response.Redirect("~/ManagerUI/Menu/Settings/RolesView.aspx");
        }
    }
}