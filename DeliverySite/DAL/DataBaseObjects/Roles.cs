using Delivery.DAL.Attributes;
using System;
using System.Collections.Generic;
using System.Data;

namespace Delivery.DAL.DataBaseObjects
{
    public class Roles : IDataManager
    {
        public DataManager DM;

        public String TableName { get; set; }

        public Roles()
        {
            DM = new DataManager();
            this.TableName = "roles";
            this.CreateDate = DateTime.Now;
            this.ChangeDate = DateTime.Now;
        }

        [DataBaseGet]
        public Int32 ID { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String Name { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public String NameOnRuss { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? IsBase { get; set; }

        [DataBaseGet]
        [DataBaseSet]
        public DateTime? CreateDate { get; set; }

        [DataBaseGet]
        [DataBaseSet]
        public DateTime? ChangeDate { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? PageBackendEdit { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? PageBackendView { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? PageCategoryEdit { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? PageCategoryView { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? PageChangePasswords { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? PageCityEdit { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? PageCityView { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? PageClientsEdit { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? PageClientsCreate { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? PageClientsView { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? PageDriversEdit { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? PageDriversView { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? PageDriverView { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? PageEmailNotificationsEdit { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? PageEmailNotificationsView { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? PageErrorsLogEdit { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? PageErrorsLogView { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? PageIssuanceListsEdit { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? PageIssuanceListsView { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? PageIssuanceListView { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? PageIssuanceView { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? PageNewIssuanceView { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? PageLogsView { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? PageManagerEdit { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? PageManagersView { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? PageMoneyDeliveredView { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? PageMoneyDriverView { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? PageMoneyDeliveryView { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? PageMoneyView { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? PageNewsEdit { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? PageNewsView { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? PageNotificationsEdit { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? PageNotificationsView { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? PagePagesEdit { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? PagePagesView { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? PageReportsEdit { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? PageReportsView { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? PageRolesEdit { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? PageRolesView { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? PageTracksEdit { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? PageTracksView { get; set; }


        [DataBaseSet]
        [DataBaseGet]
        public Int32? PageUserProfileView { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? PageUserDiscountView { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? PageUserTicketByDeliveryOnBelarus { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? PageUserTicketByDeliveryOnMinsk { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? PageUserTicketEdit { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? PageUserTicketNotProcessedView { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? PageUserTicketView { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? ActionNewsDelete { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? ActionErrorsLogDelete { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? ActionCategoryDelete { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? ActionCityDelete { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? ActionTracksDelete { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? ActionDriversDelete { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? ActionManagersDelete { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? ActionClientsDelete { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? ActionUserTicketDelete { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? ActionIssuanceListDelete { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? ActionReportsDelete { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? ActionRolesDelete { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? ActionCompletedStatus { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? ActionDisallowDeliveredToCompletedStatus { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? ActionAllowChangeInCompletedWithoutDriver { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? ActionGroupUserTicketDelete { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? ActionDriverAdd { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? ActionStatusAdd { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? ActionPrintVinil { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? ActionPrintCheck { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? ActionPrintMap { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? ActionPrintMapForManager { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? ActionPrintTickets { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? ActionPrintZP { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? ActionPrintNakl { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? ActionPrintNaklPril { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? ActionPrintPutFirst { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? ActionPrintPutSecond { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? ActionDisallowEditSomeFieldInTickets { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? ActionAllowChangeMoneyAndCourse { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? ActionControlWeight { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? ActionControlNN { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? ActionControlPN { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? ActionControlActiza { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? ActionControlGruzobozCost { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? ActionControlMoneyCheckboxes { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? ActionControlComment { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? ActionDisallowTicketChangeWithoutManagerInfo { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? ActionPrintMapForCashier { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? PageUserProfileEdit { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? ActionUserProfileDelete { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? ActionUserProfileChangeStatus { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? ActionAddManagerToUser { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? PageMyTickets { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? PageManagerView { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? ActionAddSalesManagerToUser { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? PageReportsExport { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? SectionUser { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? ActionExportAllUsersInfo { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? ActionExportAllUsersProfilesInfo { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? ActionExportAllClientsInfo { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? ActionCheckedOutCheckbox { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? ActionPhonedCheckbox { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? ActionSendSmsBulk { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? ActionBilledCheckbox { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? ActionDeliveredButtonInCity { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? PageCarsView { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? PageCarView { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? PageCarEdit { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? ActionCarsDelete { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? ActionPrintAORT { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? StatusNotProcessed { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? StatusInStock { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? StatusOnTheWay { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? StatusProcessed { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? StatusDelivered { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? StatusCompleted { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? StatusTransferInCourier { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? StatusRefusingInCourier { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? StatusExchangeInCourier { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? StatusDeliveryFromClientInCourier { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? StatusTransferInStock { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? StatusReturnInStock { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? StatusCancelInStock { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? StatusExchangeInStock { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? StatusCancel { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? StatusDeliveryFromClientInStock { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? PageFeedbacksView { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? Feedback0 { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? Feedback1 { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? Feedback2 { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? Feedback3 { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? Feedback4 { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? Feedback5 { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? Feedback6 { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? FeedbackFullAccess { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? PageDistrictsView { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? PageDistrictEdit { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? PageApiLogView { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? AllowBlockingAddApiAccess { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? PageTitlesEdit { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? PageTitlesView { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? ActionTitlesDelete { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? ActionCategoryAssignToUser { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? PageCalculationView { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? PageWarehouseEdit { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? PageWarehousesView { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? ActionDeleteWarehouses { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? StatusRefusalOnTheWay { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? StatusRefusalByAddress { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? StatusUpload { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? PageSendComProp { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? ActionClientActivateBlock { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? ViewClientStatInfo { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? ActionProviderEdit { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? ActionProviderDelete { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? ViewRasch { get; set; }
        /// <summary>
        /// ////////////////////////////////
        /// </summary>
        [DataBaseSet]
        [DataBaseGet]
        public Int32? TableND { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? TableProfile { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? TableGoods { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? TableKK { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? TableSendFrom { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? TableDirection { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? TableRecieverCity { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? TableRecieverAddr { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? TableReciever { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? TableNotes { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? TableComments { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? TableOtherOptions { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? TableRecieveDate { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? TableSendDate { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? TableStatus { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? TableDID { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? TableAgreedCost { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? TableCost { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? TablePN { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? TableWeight { get; set; }

        [DataBaseSet]
        [DataBaseGet]
        public Int32? TableUID { get; set; }

        public void Delete(Int32 id)
        {
            this.ID = id;
            DM.DeleteData(this);
        }

        public void Create()
        {
            DM.CreateData(this);
        }

        public DataSet GetAllItems()
        {
            var ds = DM.GetAllData(this, null, null, null);
            return ds;
        }

        public List<Roles> GetAllItemsToList()
        {
            var ds = DM.GetAllData(this, null, null, null);
            var roleList = new List<Roles>();
            foreach (DataRow role in ds.Tables[0].Rows)
            {
                var roleToList = new Roles()
                {
                    Name = role["Name"].ToString(),
                    NameOnRuss = role["NameOnRuss"].ToString(),

                    PageBackendEdit = Convert.ToInt32(role["PageBackendEdit"]),
                    PageBackendView = Convert.ToInt32(role["PageBackendView"]),
                    PageCategoryEdit = Convert.ToInt32(role["PageCategoryEdit"]),
                    PageCategoryView = Convert.ToInt32(role["PageCategoryView"]),
                    PageChangePasswords = Convert.ToInt32(role["PageChangePasswords"]),
                    PageCityEdit = Convert.ToInt32(role["PageCityEdit"]),
                    PageCityView = Convert.ToInt32(role["PageCityView"]),
                    PageClientsEdit = Convert.ToInt32(role["PageClientsEdit"]),
                    PageClientsCreate = Convert.ToInt32(role["PageClientsCreate"]),
                    PageClientsView = Convert.ToInt32(role["PageClientsView"]),
                    PageDriversEdit = Convert.ToInt32(role["PageDriversEdit"]),
                    PageDriversView = Convert.ToInt32(role["PageDriversView"]),
                    PageDriverView = Convert.ToInt32(role["PageDriverView"]),
                    PageEmailNotificationsEdit = Convert.ToInt32(role["PageEmailNotificationsEdit"]),
                    PageEmailNotificationsView = Convert.ToInt32(role["PageEmailNotificationsView"]),
                    PageErrorsLogEdit = Convert.ToInt32(role["PageErrorsLogEdit"]),
                    PageErrorsLogView = Convert.ToInt32(role["PageErrorsLogView"]),
                    PageIssuanceListsEdit = Convert.ToInt32(role["PageIssuanceListsEdit"]),
                    PageIssuanceListsView = Convert.ToInt32(role["PageIssuanceListsView"]),
                    PageIssuanceListView = Convert.ToInt32(role["PageIssuanceListView"]),
                    PageIssuanceView = Convert.ToInt32(role["PageIssuanceView"]),
                    PageNewIssuanceView = Convert.ToInt32(role["PageNewIssuanceView"]),
                    PageLogsView = Convert.ToInt32(role["PageLogsView"]),
                    PageManagerEdit = Convert.ToInt32(role["PageManagerEdit"]),
                    PageManagersView = Convert.ToInt32(role["PageManagersView"]),
                    PageMoneyDeliveredView = Convert.ToInt32(role["PageMoneyDeliveredView"]),
                    PageMoneyDriverView = Convert.ToInt32(role["PageMoneyDriverView"]),
                    PageMoneyDeliveryView = Convert.ToInt32(role["PageMoneyDeliveryView"]),
                    PageMoneyView = Convert.ToInt32(role["PageMoneyView"]),
                    PageNewsEdit = Convert.ToInt32(role["PageNewsEdit"]),
                    PageNewsView = Convert.ToInt32(role["PageNewsView"]),
                    PageNotificationsEdit = Convert.ToInt32(role["PageNotificationsEdit"]),
                    PageNotificationsView = Convert.ToInt32(role["PageNotificationsView"]),
                    PagePagesEdit = Convert.ToInt32(role["PagePagesEdit"]),
                    PagePagesView = Convert.ToInt32(role["PagePagesView"]),
                    PageReportsEdit = Convert.ToInt32(role["PageReportsEdit"]),
                    PageReportsView = Convert.ToInt32(role["PageReportsView"]),
                    PageRolesEdit = Convert.ToInt32(role["PageRolesEdit"]),
                    PageRolesView = Convert.ToInt32(role["PageRolesView"]),
                    PageTracksEdit = Convert.ToInt32(role["PageTracksEdit"]),
                    PageTracksView = Convert.ToInt32(role["PageTracksView"]),
                    PageUserProfileView = Convert.ToInt32(role["PageUserProfileView"]),
                    PageUserDiscountView = Convert.ToInt32(role["PageUserDiscountView"]),
                    PageUserTicketByDeliveryOnMinsk = Convert.ToInt32(role["PageUserTicketByDeliveryOnMinsk"]),
                    PageUserTicketByDeliveryOnBelarus = Convert.ToInt32(role["PageUserTicketByDeliveryOnBelarus"]),
                    PageUserTicketEdit = Convert.ToInt32(role["PageUserTicketEdit"]),
                    PageUserTicketNotProcessedView = Convert.ToInt32(role["PageUserTicketNotProcessedView"]),
                    PageUserTicketView = Convert.ToInt32(role["PageUserTicketView"]),
                    ActionNewsDelete = Convert.ToInt32(role["ActionNewsDelete"]),
                    ActionErrorsLogDelete = Convert.ToInt32(role["ActionErrorsLogDelete"]),
                    ActionCategoryDelete = Convert.ToInt32(role["ActionCategoryDelete"]),
                    ActionCityDelete = Convert.ToInt32(role["ActionCityDelete"]),
                    ActionTracksDelete = Convert.ToInt32(role["ActionTracksDelete"]),
                    ActionDriversDelete = Convert.ToInt32(role["ActionDriversDelete"]),
                    ActionManagersDelete = Convert.ToInt32(role["ActionManagersDelete"]),
                    ActionClientsDelete = Convert.ToInt32(role["ActionClientsDelete"]),
                    ActionUserTicketDelete = Convert.ToInt32(role["ActionUserTicketDelete"]),
                    ActionIssuanceListDelete = Convert.ToInt32(role["ActionIssuanceListDelete"]),
                    ActionReportsDelete = Convert.ToInt32(role["ActionReportsDelete"]),
                    ActionRolesDelete = Convert.ToInt32(role["ActionRolesDelete"]),
                    ActionCompletedStatus = Convert.ToInt32(role["ActionCompletedStatus"]),
                    ActionDisallowDeliveredToCompletedStatus = Convert.ToInt32(role["ActionDisallowDeliveredToCompletedStatus"]),
                    ActionAllowChangeInCompletedWithoutDriver = Convert.ToInt32(role["ActionAllowChangeInCompletedWithoutDriver"]),
                    ActionGroupUserTicketDelete = Convert.ToInt32(role["ActionGroupUserTicketDelete"]),
                    ActionDriverAdd = Convert.ToInt32(role["ActionDriverAdd"]),
                    ActionStatusAdd = Convert.ToInt32(role["ActionStatusAdd"]),
                    ActionPrintVinil = Convert.ToInt32(role["ActionPrintVinil"]),
                    ActionPrintCheck = Convert.ToInt32(role["ActionPrintCheck"]),
                    ActionPrintMap = Convert.ToInt32(role["ActionPrintMap"]),
                    ActionPrintMapForManager = Convert.ToInt32(role["ActionPrintMapForManager"]),
                    ActionPrintTickets = Convert.ToInt32(role["ActionPrintTickets"]),
                    ActionPrintZP = Convert.ToInt32(role["ActionPrintZP"]),
                    ActionPrintNakl = Convert.ToInt32(role["ActionPrintNakl"]),
                    ActionPrintNaklPril = Convert.ToInt32(role["ActionPrintNaklPril"]),
                    ActionPrintPutFirst = Convert.ToInt32(role["ActionPrintPutFirst"]),
                    ActionPrintPutSecond = Convert.ToInt32(role["ActionPrintPutSecond"]),
                    ActionDisallowEditSomeFieldInTickets = Convert.ToInt32(role["ActionDisallowEditSomeFieldInTickets"]),
                    ActionAllowChangeMoneyAndCourse = Convert.ToInt32(role["ActionAllowChangeMoneyAndCourse"]),
                    ActionControlWeight = Convert.ToInt32(role["ActionControlWeight"]),
                    ActionControlNN = Convert.ToInt32(role["ActionControlNN"]),
                    ActionControlPN = Convert.ToInt32(role["ActionControlPN"]),
                    ActionControlActiza = Convert.ToInt32(role["ActionControlActiza"]),
                    ActionControlGruzobozCost = Convert.ToInt32(role["ActionControlGruzobozCost"]),
                    ActionControlMoneyCheckboxes = Convert.ToInt32(role["ActionControlMoneyCheckboxes"]),
                    ActionControlComment = Convert.ToInt32(role["ActionControlComment"]),
                    ActionDisallowTicketChangeWithoutManagerInfo = Convert.ToInt32(role["ActionDisallowTicketChangeWithoutManagerInfo"]),
                    ActionPrintMapForCashier = Convert.ToInt32(role["ActionPrintMapForCashier"]),
                    PageUserProfileEdit = Convert.ToInt32(role["PageUserProfileEdit"]),
                    ActionUserProfileDelete = Convert.ToInt32(role["ActionUserProfileDelete"]),
                    ActionUserProfileChangeStatus = Convert.ToInt32(role["ActionUserProfileChangeStatus"]),
                    SectionUser = Convert.ToInt32(role["SectionUser"]),
                    ActionAddManagerToUser = Convert.ToInt32(role["ActionAddManagerToUser"]),
                    ActionAddSalesManagerToUser = Convert.ToInt32(role["ActionAddSalesManagerToUser"]),
                    PageMyTickets = Convert.ToInt32(role["PageMyTickets"]),
                    PageManagerView = Convert.ToInt32(role["PageManagerView"]),
                    PageReportsExport = Convert.ToInt32(role["PageReportsExport"]),
                    ActionExportAllUsersInfo = Convert.ToInt32(role["ActionExportAllUsersInfo"]),
                    ActionExportAllUsersProfilesInfo = Convert.ToInt32(role["ActionExportAllUsersProfilesInfo"]),
                    ActionExportAllClientsInfo = Convert.ToInt32(role["ActionExportAllClientsInfo"]),
                    ActionCheckedOutCheckbox = Convert.ToInt32(role["ActionCheckedOutCheckbox"]),
                    ActionPhonedCheckbox = Convert.ToInt32(role["ActionPhonedCheckbox"]),
                    ActionSendSmsBulk = Convert.ToInt32(role["ActionSendSmsBulk"]),
                    ActionBilledCheckbox = Convert.ToInt32(role["ActionBilledCheckbox"]),
                    ActionDeliveredButtonInCity = Convert.ToInt32(role["ActionDeliveredButtonInCity"]),
                    PageCarsView = Convert.ToInt32(role["PageCarsView"]),
                    PageCarView = Convert.ToInt32(role["PageCarView"]),
                    PageCarEdit = Convert.ToInt32(role["PageCarEdit"]),
                    ActionCarsDelete = Convert.ToInt32(role["ActionCarsDelete"]),
                    ActionPrintAORT = Convert.ToInt32(role["ActionPrintAORT"]),
                    StatusNotProcessed = Convert.ToInt32(role["StatusNotProcessed"]),
                    StatusInStock = Convert.ToInt32(role["StatusInStock"]),
                    StatusOnTheWay = Convert.ToInt32(role["StatusOnTheWay"]),
                    StatusProcessed = Convert.ToInt32(role["StatusProcessed"]),
                    StatusDelivered = Convert.ToInt32(role["StatusDelivered"]),
                    StatusCompleted = Convert.ToInt32(role["StatusCompleted"]),
                    StatusTransferInCourier = Convert.ToInt32(role["StatusTransferInCourier"]),
                    StatusRefusingInCourier = Convert.ToInt32(role["StatusRefusingInCourier"]),
                    StatusExchangeInCourier = Convert.ToInt32(role["StatusExchangeInCourier"]),
                    StatusDeliveryFromClientInCourier = Convert.ToInt32(role["StatusDeliveryFromClientInCourier"]),
                    StatusTransferInStock = Convert.ToInt32(role["StatusTransferInStock"]),
                    StatusReturnInStock = Convert.ToInt32(role["StatusReturnInStock"]),
                    StatusCancelInStock = Convert.ToInt32(role["StatusCancelInStock"]),
                    StatusExchangeInStock = Convert.ToInt32(role["StatusExchangeInStock"]),
                    StatusDeliveryFromClientInStock = Convert.ToInt32(role["StatusDeliveryFromClientInStock"]),
                    StatusCancel = Convert.ToInt32(role["StatusCancel"]),
                    PageFeedbacksView = Convert.ToInt32(role["PageFeedbacksView"]),
                    Feedback0 = Convert.ToInt32(role["Feedback0"]),
                    Feedback1 = Convert.ToInt32(role["Feedback1"]),
                    Feedback2 = Convert.ToInt32(role["Feedback2"]),
                    Feedback3 = Convert.ToInt32(role["Feedback3"]),
                    Feedback4 = Convert.ToInt32(role["Feedback4"]),
                    Feedback5 = Convert.ToInt32(role["Feedback5"]),
                    Feedback6 = Convert.ToInt32(role["Feedback6"]),
                    FeedbackFullAccess = Convert.ToInt32(role["FeedbackFullAccess"]),
                    PageDistrictsView = Convert.ToInt32(role["PageDistrictsView"]),
                    PageDistrictEdit = Convert.ToInt32(role["PageDistrictEdit"]),
                    PageApiLogView = Convert.ToInt32(role["PageApiLogView"]),
                    AllowBlockingAddApiAccess = Convert.ToInt32(role["AllowBlockingAddApiAccess"]),
                    PageTitlesEdit = Convert.ToInt32(role["PageTitlesEdit"]),
                    PageTitlesView = Convert.ToInt32(role["PageTitlesView"]),
                    ActionTitlesDelete = Convert.ToInt32(role["ActionTitlesDelete"]),
                    ActionCategoryAssignToUser = Convert.ToInt32(role["ActionCategoryAssignToUser"]),
                    PageCalculationView = Convert.ToInt32(role["PageCalculationView"]),
                    PageWarehouseEdit = Convert.ToInt32(role["PageWarehouseEdit"]),
                    ActionDeleteWarehouses = Convert.ToInt32(role["ActionDeleteWarehouses"]),
                    PageWarehousesView = Convert.ToInt32(role["PageWarehousesView"]),
                    StatusRefusalByAddress = Convert.ToInt32(role["StatusRefusalByAddress"]),
                    StatusRefusalOnTheWay = Convert.ToInt32(role["StatusRefusalOnTheWay"]),
                    StatusUpload = Convert.ToInt32(role["StatusUpload"]),
                    PageSendComProp = Convert.ToInt32(role["PageSendComProp"]),
                    ActionClientActivateBlock = Convert.ToInt32(role["ActionClientActivateBlock"]),
                    ViewClientStatInfo = Convert.ToInt32(role["ViewClientStatInfo"]),
                    ActionProviderEdit = Convert.ToInt32(role["ActionProviderEdit"]),
                    ActionProviderDelete = Convert.ToInt32(role["ActionProviderDelete"]),
                    ViewRasch = Convert.ToInt32(role["ViewRasch"]),
                    TableAgreedCost = Convert.ToInt32(role["TableAgreedCost"]),
                    TableComments = Convert.ToInt32(role["TableComments"]),
                    TableCost = Convert.ToInt32(role["TableCost"]),
                    TableDID = Convert.ToInt32(role["TableDID"]),
                    TableDirection = Convert.ToInt32(role["TableDirection"]),
                    TableGoods = Convert.ToInt32(role["TableGoods"]),
                    TableKK = Convert.ToInt32(role["TableKK"]),
                    TableND = Convert.ToInt32(role["TableND"]),
                    TableNotes = Convert.ToInt32(role["TableNotes"]),
                    TableOtherOptions = Convert.ToInt32(role["TableOtherOptions"]),
                    TablePN = Convert.ToInt32(role["TablePN"]),
                    TableProfile = Convert.ToInt32(role["TableProfile"]),
                    TableRecieveDate = Convert.ToInt32(role["TableRecieveDate"]),
                    TableReciever = Convert.ToInt32(role["TableReciever"]),
                    TableRecieverAddr = Convert.ToInt32(role["TableRecieverAddr"]),
                    TableRecieverCity = Convert.ToInt32(role["TableRecieverCity"]),
                    TableSendDate = Convert.ToInt32(role["TableSendDate"]),
                    TableSendFrom = Convert.ToInt32(role["TableSendFrom"]),
                    TableStatus = Convert.ToInt32(role["TableStatus"]),
                    TableWeight = Convert.ToInt32(role["TableWeight"]),
                    TableUID = Convert.ToInt32(role["TableUID"])
                };
                roleList.Add(roleToList);
            }
            return roleList;
        }

        public DataSet GetAllItems(string orderBy, string direction, string whereField)
        {
            var ds = DM.GetAllData(this, orderBy, direction, whereField);
            return ds;
        }

        public dynamic GetById()
        {
            return DM.GetDataBy(this, "ID", null);
        }

        public dynamic GetByName()
        {
            return DM.GetDataBy(this, "Name", null);
        }

        public void Update()
        {
            DM.UpdateDate(this);
        }

    }
}