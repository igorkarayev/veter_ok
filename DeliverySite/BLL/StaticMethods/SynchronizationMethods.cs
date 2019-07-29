using Delivery.BLL.Helpers;
using Delivery.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace Delivery.BLL.StaticMethods
{
    public class SynchronizationMethods
    {
        public static void SqlBuilder(string idListString)
        {
            List<string> idList = idListString.Split('-').ToList();
            var sqlString = String.Empty;
            foreach (var id in idList)
            {
                sqlString = sqlString + id + ", ";
            }
            var dm = new DataManager();
            var allDataForUpdate = dm.QueryWithReturnDataSet("SELECT * FROM `tickets` WHERE `id` IN (" + sqlString.Remove(sqlString.Length - 2) + ");" +
                                                       "SELECT * FROM `goods` WHERE `TicketFullSecureID` IN (SELECT DISTINCT `FullSecureID` FROM `tickets` WHERE `id` IN (" + sqlString.Remove(sqlString.Length - 2) + "));" +
                                                       "SELECT * FROM `users` WHERE `id` IN (SELECT DISTINCT `UserID` FROM `tickets` WHERE `id` IN (" + sqlString.Remove(sqlString.Length - 2) + "));" +
                                                       "SELECT * FROM `drivers` WHERE `id` IN (SELECT DISTINCT `DriverID` FROM `tickets` WHERE `id` IN (" + sqlString.Remove(sqlString.Length - 2) + "));" +
                                                       "SELECT * FROM `cars` WHERE `id` IN (SELECT DISTINCT `CarID` FROM `drivers` WHERE `id` in (SELECT DISTINCT `DriverID` FROM `tickets` WHERE `id` IN (" + sqlString.Remove(sqlString.Length - 2) + ")));" +
                                                       "SELECT * FROM `usersprofiles` WHERE `id` IN (SELECT DISTINCT `UserProfileID` FROM `tickets` WHERE `id` IN (" + sqlString.Remove(sqlString.Length - 2) + "));");
            var ticketsInsertUpdateStatementArray = new string[200];
            var ticketsInsertUpdateStatement = new System.Text.StringBuilder();

            var goodsInsertUpdateStatementArray = new string[60];
            var goodsInsertUpdateStatement = new System.Text.StringBuilder();

            var usersInsertUpdateStatementArray = new string[120];
            var usersInsertUpdateStatement = new System.Text.StringBuilder();

            var driversInsertUpdateStatementArray = new string[120];
            var driversInsertUpdateStatement = new System.Text.StringBuilder();

            var carsInsertUpdateStatementArray = new string[60];
            var carsInsertUpdateStatement = new System.Text.StringBuilder();

            var usersprofilesInsertUpdateStatementArray = new string[120];
            var usersprofilesInsertUpdateStatement = new System.Text.StringBuilder();

            #region Tickets         INSERT/UPDATE statement
            foreach (DataRow ticket in allDataForUpdate.Tables[0].Rows)
            {
                #region ticketsInsertUpdateStatementArray
                ticketsInsertUpdateStatementArray[0] = "INSERT IGNORE INTO `tickets` SET ";
                ticketsInsertUpdateStatementArray[1] = "`ID` = " + ticket["ID"] + ",";
                ticketsInsertUpdateStatementArray[2] = "`SecureID` = '" + ticket["SecureID"] + "',";
                ticketsInsertUpdateStatementArray[3] = "`UserID` = " + ticket["UserID"] + ",";
                ticketsInsertUpdateStatementArray[4] = "`UserProfileID` = " + ticket["UserProfileID"] + ",";
                ticketsInsertUpdateStatementArray[5] = "`CityID` = " + ticket["CityID"] + ",";
                ticketsInsertUpdateStatementArray[6] = "`DriverID` = " + ticket["DriverID"] + ",";
                ticketsInsertUpdateStatementArray[7] = "`StatusID` = " + ticket["StatusID"] + ",";
                ticketsInsertUpdateStatementArray[8] = "`StatusIDOld` = " + ticket["StatusIDOld"] + ",";
                ticketsInsertUpdateStatementArray[9] = "`FullSecureID` = '" + ticket["FullSecureID"] + "',";
                ticketsInsertUpdateStatementArray[10] = "`RecipientFirstName` = '" + ticket["RecipientFirstName"] + "',";
                ticketsInsertUpdateStatementArray[11] = "`RecipientLastName` = '" + ticket["RecipientLastName"] + "',";
                ticketsInsertUpdateStatementArray[12] = "`RecipientThirdName` = '" + ticket["RecipientThirdName"] + "',";
                ticketsInsertUpdateStatementArray[13] = "`RecipientStreet` = '" + ticket["RecipientStreet"] + "',";
                ticketsInsertUpdateStatementArray[14] = "`RecipientStreetNumber` = '" + ticket["RecipientStreetNumber"] + "',";
                ticketsInsertUpdateStatementArray[15] = "`RecipientKorpus` = '" + ticket["RecipientKorpus"] + "',";
                ticketsInsertUpdateStatementArray[16] = "`RecipientKvartira` = '" + ticket["RecipientKvartira"] + "',";
                ticketsInsertUpdateStatementArray[17] = "`RecipientPhone` = '" + ticket["RecipientPhone"] + "',";
                ticketsInsertUpdateStatementArray[18] = "`RecipientPhoneTwo` = '" + ticket["RecipientPhoneTwo"] + "',";
                ticketsInsertUpdateStatementArray[19] = "`AssessedCost` = " + ticket["AssessedCost"] + ",";
                ticketsInsertUpdateStatementArray[20] = "`AgreedCost` = " + ticket["AgreedCost"] + ",";
                ticketsInsertUpdateStatementArray[21] = "`DeliveryCost` = " + ticket["DeliveryCost"] + ",";
                ticketsInsertUpdateStatementArray[22] = "`ReceivedRUR` = " + ticket["ReceivedRUR"] + ",";
                ticketsInsertUpdateStatementArray[23] = "`ReceivedUSD` = " + ticket["ReceivedUSD"] + ",";
                ticketsInsertUpdateStatementArray[24] = "`ReceivedBLR` = " + ticket["ReceivedBLR"] + ",";
                ticketsInsertUpdateStatementArray[25] = "`ReceivedEUR` = " + ticket["ReceivedEUR"] + ",";
                ticketsInsertUpdateStatementArray[26] = "`Note` = '" + ticket["Note"] + "',";
                ticketsInsertUpdateStatementArray[27] = "`CourseUSD` = " + ticket["CourseUSD"] + ",";
                ticketsInsertUpdateStatementArray[28] = "`CourseRUR` = " + ticket["CourseRUR"] + ",";
                ticketsInsertUpdateStatementArray[29] = "`CourseEUR` = " + ticket["CourseEUR"] + ",";
                ticketsInsertUpdateStatementArray[30] = "`DeliveryDate` = '" + OtherMethods.FullDateTimeConvert(ticket["DeliveryDate"].ToString()) + "',";
                ticketsInsertUpdateStatementArray[31] = "`BoxesNumber` = " + ticket["BoxesNumber"] + ",";
                ticketsInsertUpdateStatementArray[32] = "`CreateDate` = '" + OtherMethods.FullDateTimeConvert(ticket["CreateDate"].ToString()) + "',";
                ticketsInsertUpdateStatementArray[33] = "`ChangeDate` = '" + OtherMethods.FullDateTimeConvert(ticket["ChangeDate"].ToString()) + "',";
                ticketsInsertUpdateStatementArray[34] = "`CompletedDate` = '" + OtherMethods.FullDateTimeConvert(ticket["CompletedDate"].ToString()) + "',";
                ticketsInsertUpdateStatementArray[35] = "`StatusDescription` = '" + ticket["StatusDescription"] + "',";
                ticketsInsertUpdateStatementArray[37] = "`AdmissionDate` = '" + OtherMethods.FullDateTimeConvert(ticket["AdmissionDate"].ToString()) + "',";
                ticketsInsertUpdateStatementArray[38] = "`GruzobozCost` = " + ticket["GruzobozCost"] + ",";
                ticketsInsertUpdateStatementArray[39] = "`TtnSeria` = '" + ticket["TtnSeria"] + "',";
                ticketsInsertUpdateStatementArray[40] = "`TtnNumber` = '" + ticket["TtnNumber"] + "',";
                ticketsInsertUpdateStatementArray[41] = "`OtherDocuments` = '" + ticket["OtherDocuments"] + "',";
                ticketsInsertUpdateStatementArray[42] = "`PassportSeria` = '" + ticket["PassportSeria"] + "',";
                ticketsInsertUpdateStatementArray[43] = "`PassportNumber` = '" + ticket["PassportNumber"] + "',";
                ticketsInsertUpdateStatementArray[44] = "`Comment` = '" + ticket["Comment"] + "',";
                ticketsInsertUpdateStatementArray[45] = "`TrackIDUser` = " + ticket["TrackIDUser"] + ",";
                ticketsInsertUpdateStatementArray[46] = "`PrintNakl` = " + ticket["PrintNakl"] + ",";
                ticketsInsertUpdateStatementArray[47] = "`Weight` = " + ticket["Weight"] + ",";
                ticketsInsertUpdateStatementArray[48] = "`PrintNaklInMap` = " + ticket["PrintNaklInMap"] + ",";
                ticketsInsertUpdateStatementArray[49] = "`IssuanceListID` = " + ticket["IssuanceListID"] + ",";
                ticketsInsertUpdateStatementArray[50] = "`NotPrintInPril2` = " + ticket["NotPrintInPril2"] + ",";
                ticketsInsertUpdateStatementArray[51] = "`Pril2Cost` = " + ticket["Pril2Cost"] + ",";
                ticketsInsertUpdateStatementArray[52] = "`Pril2BoxesNumber` = " + ticket["Pril2BoxesNumber"] + ",";
                ticketsInsertUpdateStatementArray[53] = "`Pril2Weight` = " + ticket["Pril2Weight"] + ",";
                ticketsInsertUpdateStatementArray[54] = "`WithoutMoney` = " + ticket["WithoutMoney"] + ",";
                ticketsInsertUpdateStatementArray[55] = "`IsExchange` = " + ticket["IsExchange"] + ",";
                ticketsInsertUpdateStatementArray[56] = "`ProcessedDate` = '" + OtherMethods.FullDateTimeConvert(ticket["ProcessedDate"].ToString()) + "',";
                ticketsInsertUpdateStatementArray[57] = "`CheckedOut` = " + ticket["CheckedOut"] + ",";
                ticketsInsertUpdateStatementArray[58] = "`Phoned` = " + ticket["Phoned"] + ",";
                ticketsInsertUpdateStatementArray[59] = "`OvDateFrom` = '" + OtherMethods.FullDateTimeConvert(ticket["OvDateFrom"].ToString()) + "',";
                ticketsInsertUpdateStatementArray[60] = "`OvDateTo` = '" + OtherMethods.FullDateTimeConvert(ticket["OvDateTo"].ToString()) + "',";
                ticketsInsertUpdateStatementArray[61] = "`RecipientStreetPrefix` = '" + ticket["RecipientStreetPrefix"] + "',";
                ticketsInsertUpdateStatementArray[62] = "`SenderStreetPrefix` = '" + ticket["SenderStreetPrefix"] + "',";
                ticketsInsertUpdateStatementArray[63] = "`SenderStreetName` = '" + ticket["SenderStreetName"] + "',";
                ticketsInsertUpdateStatementArray[64] = "`SenderStreetNumber` = '" + ticket["SenderStreetNumber"] + "',";
                ticketsInsertUpdateStatementArray[65] = "`SenderHousing` = '" + ticket["SenderHousing"] + "',";
                ticketsInsertUpdateStatementArray[66] = "`SenderApartmentNumber` = '" + ticket["SenderApartmentNumber"] + "',";
                ticketsInsertUpdateStatementArray[67] = "`SenderCityID` = " + ticket["SenderCityID"] + ",";
                ticketsInsertUpdateStatementArray[68] = "`AvailableOtherDocuments` = " + ticket["AvailableOtherDocuments"] + ",";
                ticketsInsertUpdateStatementArray[69] = "`ReturnDate` = '" + ticket["ReturnDate"] + "',";
                ticketsInsertUpdateStatementArray[70] = "`Billed` = " + ticket["Billed"] + " ";

                ticketsInsertUpdateStatementArray[100] = "ON DUPLICATE KEY UPDATE ";

                ticketsInsertUpdateStatementArray[101] = "`SecureID` = '" + ticket["SecureID"] + "',";
                ticketsInsertUpdateStatementArray[102] = "`UserID` = " + ticket["UserID"] + ",";
                ticketsInsertUpdateStatementArray[103] = "`UserProfileID` = " + ticket["UserProfileID"] + ",";
                ticketsInsertUpdateStatementArray[104] = "`CityID` = " + ticket["CityID"] + ",";
                ticketsInsertUpdateStatementArray[105] = "`DriverID` = " + ticket["DriverID"] + ",";
                ticketsInsertUpdateStatementArray[106] = "`StatusID` = " + ticket["StatusID"] + ",";
                ticketsInsertUpdateStatementArray[107] = "`StatusIDOld` = " + ticket["StatusIDOld"] + ",";
                ticketsInsertUpdateStatementArray[108] = "`FullSecureID` = '" + ticket["FullSecureID"] + "',";
                ticketsInsertUpdateStatementArray[109] = "`RecipientFirstName` = '" + ticket["RecipientFirstName"] + "',";
                ticketsInsertUpdateStatementArray[110] = "`RecipientLastName` = '" + ticket["RecipientLastName"] + "',";
                ticketsInsertUpdateStatementArray[111] = "`RecipientThirdName` = '" + ticket["RecipientThirdName"] + "',";
                ticketsInsertUpdateStatementArray[112] = "`RecipientStreet` = '" + ticket["RecipientStreet"] + "',";
                ticketsInsertUpdateStatementArray[113] = "`RecipientStreetNumber` = '" + ticket["RecipientStreetNumber"] + "',";
                ticketsInsertUpdateStatementArray[114] = "`RecipientKorpus` = '" + ticket["RecipientKorpus"] + "',";
                ticketsInsertUpdateStatementArray[115] = "`RecipientKvartira` = '" + ticket["RecipientKvartira"] + "',";
                ticketsInsertUpdateStatementArray[116] = "`RecipientPhone` = '" + ticket["RecipientPhone"] + "',";
                ticketsInsertUpdateStatementArray[117] = "`RecipientPhoneTwo` = '" + ticket["RecipientPhoneTwo"] + "',";
                ticketsInsertUpdateStatementArray[118] = "`AssessedCost` = " + ticket["AssessedCost"] + ",";
                ticketsInsertUpdateStatementArray[119] = "`AgreedCost` = " + ticket["AgreedCost"] + ",";
                ticketsInsertUpdateStatementArray[120] = "`DeliveryCost` = " + ticket["DeliveryCost"] + ",";
                ticketsInsertUpdateStatementArray[121] = "`ReceivedRUR` = " + ticket["ReceivedRUR"] + ",";
                ticketsInsertUpdateStatementArray[122] = "`ReceivedUSD` = " + ticket["ReceivedUSD"] + ",";
                ticketsInsertUpdateStatementArray[123] = "`ReceivedBLR` = " + ticket["ReceivedBLR"] + ",";
                ticketsInsertUpdateStatementArray[124] = "`ReceivedEUR` = " + ticket["ReceivedEUR"] + ",";
                ticketsInsertUpdateStatementArray[125] = "`Note` = '" + ticket["Note"] + "',";
                ticketsInsertUpdateStatementArray[126] = "`CourseUSD` = " + ticket["CourseUSD"] + ",";
                ticketsInsertUpdateStatementArray[127] = "`CourseRUR` = " + ticket["CourseRUR"] + ",";
                ticketsInsertUpdateStatementArray[128] = "`CourseEUR` = " + ticket["CourseEUR"] + ",";
                ticketsInsertUpdateStatementArray[129] = "`DeliveryDate` = '" + OtherMethods.FullDateTimeConvert(ticket["DeliveryDate"].ToString()) + "',";
                ticketsInsertUpdateStatementArray[130] = "`BoxesNumber` = " + ticket["BoxesNumber"] + ",";
                ticketsInsertUpdateStatementArray[131] = "`CreateDate` = '" + OtherMethods.FullDateTimeConvert(ticket["CreateDate"].ToString()) + "',";
                ticketsInsertUpdateStatementArray[132] = "`ChangeDate` = '" + OtherMethods.FullDateTimeConvert(ticket["ChangeDate"].ToString()) + "',";
                ticketsInsertUpdateStatementArray[133] = "`CompletedDate` = '" + OtherMethods.FullDateTimeConvert(ticket["CompletedDate"].ToString()) + "',";
                ticketsInsertUpdateStatementArray[134] = "`StatusDescription` = '" + ticket["StatusDescription"] + "',";
                ticketsInsertUpdateStatementArray[136] = "`AdmissionDate` = '" + OtherMethods.FullDateTimeConvert(ticket["AdmissionDate"].ToString()) + "',";
                ticketsInsertUpdateStatementArray[137] = "`GruzobozCost` = " + ticket["GruzobozCost"] + ",";
                ticketsInsertUpdateStatementArray[138] = "`TtnSeria` = '" + ticket["TtnSeria"] + "',";
                ticketsInsertUpdateStatementArray[139] = "`TtnNumber` = '" + ticket["TtnNumber"] + "',";
                ticketsInsertUpdateStatementArray[140] = "`OtherDocuments` = '" + ticket["OtherDocuments"] + "',";
                ticketsInsertUpdateStatementArray[141] = "`PassportSeria` = '" + ticket["PassportSeria"] + "',";
                ticketsInsertUpdateStatementArray[142] = "`PassportNumber` = '" + ticket["PassportNumber"] + "',";
                ticketsInsertUpdateStatementArray[143] = "`Comment` = '" + ticket["Comment"] + "',";
                ticketsInsertUpdateStatementArray[144] = "`TrackIDUser` = " + ticket["TrackIDUser"] + ",";
                ticketsInsertUpdateStatementArray[145] = "`PrintNakl` = " + ticket["PrintNakl"] + ",";
                ticketsInsertUpdateStatementArray[146] = "`Weight` = " + ticket["Weight"] + ",";
                ticketsInsertUpdateStatementArray[147] = "`PrintNaklInMap` = " + ticket["PrintNaklInMap"] + ",";
                ticketsInsertUpdateStatementArray[148] = "`IssuanceListID` = " + ticket["IssuanceListID"] + ",";
                ticketsInsertUpdateStatementArray[149] = "`NotPrintInPril2` = " + ticket["NotPrintInPril2"] + ",";
                ticketsInsertUpdateStatementArray[150] = "`Pril2Cost` = " + ticket["Pril2Cost"] + ",";
                ticketsInsertUpdateStatementArray[151] = "`Pril2BoxesNumber` = " + ticket["Pril2BoxesNumber"] + ",";
                ticketsInsertUpdateStatementArray[152] = "`Pril2Weight` = " + ticket["Pril2Weight"] + ",";
                ticketsInsertUpdateStatementArray[153] = "`WithoutMoney` = " + ticket["WithoutMoney"] + ",";
                ticketsInsertUpdateStatementArray[154] = "`IsExchange` = " + ticket["IsExchange"] + ",";
                ticketsInsertUpdateStatementArray[155] = "`ProcessedDate` = '" + OtherMethods.FullDateTimeConvert(ticket["ProcessedDate"].ToString()) + "',";
                ticketsInsertUpdateStatementArray[156] = "`CheckedOut` = " + ticket["CheckedOut"] + ",";
                ticketsInsertUpdateStatementArray[157] = "`Phoned` = " + ticket["Phoned"] + ",";
                ticketsInsertUpdateStatementArray[158] = "`OvDateFrom` = '" + OtherMethods.FullDateTimeConvert(ticket["OvDateFrom"].ToString()) + "',";
                ticketsInsertUpdateStatementArray[159] = "`OvDateTo` = '" + OtherMethods.FullDateTimeConvert(ticket["OvDateTo"].ToString()) + "',";
                ticketsInsertUpdateStatementArray[160] = "`RecipientStreetPrefix` = '" + ticket["RecipientStreetPrefix"] + "',";
                ticketsInsertUpdateStatementArray[161] = "`SenderStreetPrefix` = '" + ticket["SenderStreetPrefix"] + "',";
                ticketsInsertUpdateStatementArray[162] = "`SenderStreetName` = '" + ticket["SenderStreetName"] + "',";
                ticketsInsertUpdateStatementArray[163] = "`SenderStreetNumber` = '" + ticket["SenderStreetNumber"] + "',";
                ticketsInsertUpdateStatementArray[164] = "`SenderHousing` = '" + ticket["SenderHousing"] + "',";
                ticketsInsertUpdateStatementArray[165] = "`SenderApartmentNumber` = '" + ticket["SenderApartmentNumber"] + "',";
                ticketsInsertUpdateStatementArray[166] = "`SenderCityID` = " + ticket["SenderCityID"] + ",";
                ticketsInsertUpdateStatementArray[167] = "`AvailableOtherDocuments` = " + ticket["AvailableOtherDocuments"] + ",";
                ticketsInsertUpdateStatementArray[168] = "`ReturnDate` = '" + ticket["ReturnDate"] + "',";
                ticketsInsertUpdateStatementArray[169] = "`Billed` = " + ticket["Billed"] + "; ";
                #endregion

                for (int i = 0; i < 200; i++)
                {
                    if (!String.IsNullOrEmpty(ticketsInsertUpdateStatementArray[i]))
                        ticketsInsertUpdateStatement.Append(ticketsInsertUpdateStatementArray[i]);
                }
            }
            #endregion

            #region Goods           INSERT/UPDATE statement
            foreach (DataRow goods in allDataForUpdate.Tables[1].Rows)
            {
                #region godsInsertUpdateStatementArray
                goodsInsertUpdateStatementArray[0] = "INSERT IGNORE INTO `goods` SET ";
                goodsInsertUpdateStatementArray[1] = "`ID` = " + goods["ID"] + ",";
                goodsInsertUpdateStatementArray[2] = "`TicketFullSecureID` = '" + goods["TicketFullSecureID"] + "',";
                goodsInsertUpdateStatementArray[3] = "`Description` = '" + goods["Description"] + "',";
                goodsInsertUpdateStatementArray[4] = "`Model` = '" + goods["Model"] + "',";
                goodsInsertUpdateStatementArray[5] = "`Number` = " + goods["Number"] + ",";
                goodsInsertUpdateStatementArray[6] = "`Cost` = " + goods["Cost"] + ",";
                goodsInsertUpdateStatementArray[7] = "`WithoutAkciza` = " + goods["WithoutAkciza"] + ",";
                goodsInsertUpdateStatementArray[8] = "`CreateDate` = '" + OtherMethods.FullDateTimeConvert(goods["CreateDate"].ToString()) + "',";
                goodsInsertUpdateStatementArray[9] = "`ChangeDate` = '" + OtherMethods.FullDateTimeConvert(goods["ChangeDate"].ToString()) + "' ";

                goodsInsertUpdateStatementArray[30] = "ON DUPLICATE KEY UPDATE ";

                goodsInsertUpdateStatementArray[31] = "`TicketFullSecureID` = '" + goods["TicketFullSecureID"] + "',";
                goodsInsertUpdateStatementArray[32] = "`Description` = '" + goods["Description"] + "',";
                goodsInsertUpdateStatementArray[33] = "`Model` = '" + goods["Model"] + "',";
                goodsInsertUpdateStatementArray[34] = "`Number` = " + goods["Number"] + ",";
                goodsInsertUpdateStatementArray[35] = "`Cost` = " + goods["Cost"] + ",";
                goodsInsertUpdateStatementArray[36] = "`WithoutAkciza` = " + goods["WithoutAkciza"] + ",";
                goodsInsertUpdateStatementArray[37] = "`CreateDate` = '" + OtherMethods.FullDateTimeConvert(goods["CreateDate"].ToString()) + "',";
                goodsInsertUpdateStatementArray[38] = "`ChangeDate` = '" + OtherMethods.FullDateTimeConvert(goods["ChangeDate"].ToString()) + "'; ";
                #endregion

                for (int i = 0; i < 60; i++)
                {
                    if (!String.IsNullOrEmpty(goodsInsertUpdateStatementArray[i]))
                        goodsInsertUpdateStatement.Append(goodsInsertUpdateStatementArray[i]);
                }
            }
            #endregion

            #region Users           INSERT/UPDATE statement
            foreach (DataRow user in allDataForUpdate.Tables[2].Rows)
            {
                #region usersInsertUpdateStatementArray
                usersInsertUpdateStatementArray[0] = "INSERT IGNORE INTO `users` SET ";
                usersInsertUpdateStatementArray[1] = "`ID` = " + user["ID"] + ",";
                usersInsertUpdateStatementArray[2] = "`Login` = '" + user["Login"] + "',";
                usersInsertUpdateStatementArray[3] = "`Password` = '" + user["Password"] + "',";
                usersInsertUpdateStatementArray[4] = "`Email` = '" + user["Email"] + "',";
                usersInsertUpdateStatementArray[5] = "`Family` = '" + user["Family"] + "',";
                usersInsertUpdateStatementArray[6] = "`Name` = '" + user["Name"] + "',";
                usersInsertUpdateStatementArray[7] = "`Role` = '" + user["Role"] + "',";
                usersInsertUpdateStatementArray[8] = "`Status` = " + user["Status"] + ",";
                usersInsertUpdateStatementArray[9] = "`Phone` = '" + user["Phone"] + "',";
                usersInsertUpdateStatementArray[10] = "`CreateDate` = '" + OtherMethods.FullDateTimeConvert(user["CreateDate"].ToString()) + "',";
                usersInsertUpdateStatementArray[11] = "`ChangeDate` = '" + OtherMethods.FullDateTimeConvert(user["ChangeDate"].ToString()) + "',";
                usersInsertUpdateStatementArray[12] = "`IsCourse` = " + user["IsCourse"] + ",";
                usersInsertUpdateStatementArray[13] = "`Note` = '" + user["Note"] + "',";
                usersInsertUpdateStatementArray[14] = "`Discount` = " + user["Discount"] + ",";
                usersInsertUpdateStatementArray[15] = "`SpecialClient` = " + user["SpecialClient"] + ",";
                usersInsertUpdateStatementArray[16] = "`ManagerID` = " + user["ManagerID"] + ",";
                usersInsertUpdateStatementArray[17] = "`SalesManagerID` = " + user["SalesManagerID"] + ",";
                usersInsertUpdateStatementArray[18] = "`AccessOnlyByWhiteList` = " + user["AccessOnlyByWhiteList"] + ",";
                usersInsertUpdateStatementArray[19] = "`Address` = '" + user["Address"] + "',";
                usersInsertUpdateStatementArray[20] = "`PhoneWorkOne` = '" + user["PhoneWorkOne"] + "',";
                usersInsertUpdateStatementArray[21] = "`PhoneWorkTwo` = '" + user["PhoneWorkTwo"] + "',";
                usersInsertUpdateStatementArray[22] = "`PhoneHome` = '" + user["PhoneHome"] + "',";
                usersInsertUpdateStatementArray[23] = "`PassportSeria` = '" + user["PassportSeria"] + "',";
                usersInsertUpdateStatementArray[24] = "`PassportNumber` = '" + user["PassportNumber"] + "',";
                usersInsertUpdateStatementArray[25] = "`PersonalNumber` = '" + user["PersonalNumber"] + "',";
                usersInsertUpdateStatementArray[26] = "`ROVD` = '" + user["ROVD"] + "',";
                usersInsertUpdateStatementArray[27] = "`Validity` = '" + OtherMethods.FullDateTimeConvert(user["Validity"].ToString()) + "',";
                usersInsertUpdateStatementArray[28] = "`RegistrationAddress` = '" + user["RegistrationAddress"] + "',";
                usersInsertUpdateStatementArray[29] = "`BirthDay` = '" + OtherMethods.FullDateTimeConvert(user["BirthDay"].ToString()) + "',";
                usersInsertUpdateStatementArray[30] = "`DateOfIssue` = '" + OtherMethods.FullDateTimeConvert(user["DateOfIssue"].ToString()) + "',";
                usersInsertUpdateStatementArray[31] = "`Skype` = '" + user["Skype"] + "', ";
                usersInsertUpdateStatementArray[32] = "`RedClient` = " + user["RedClient"] + ", ";
                usersInsertUpdateStatementArray[33] = "`AllowApi` = " + user["AllowApi"] + ", ";
                usersInsertUpdateStatementArray[35] = "`SenderCityID` = " + GetSenderCityId(user["SenderCityID"].ToString()) + ", ";
                usersInsertUpdateStatementArray[36] = "`SenderStreetPrefix` = '" + user["SenderStreetPrefix"] + "', ";
                usersInsertUpdateStatementArray[37] = "`SenderStreetName` = '" + user["SenderStreetName"] + "', ";
                usersInsertUpdateStatementArray[38] = "`SenderStreetNumber` = '" + user["SenderStreetNumber"] + "', ";
                usersInsertUpdateStatementArray[39] = "`SenderApartmentNumber` = '" + user["SenderApartmentNumber"] + "', ";
                usersInsertUpdateStatementArray[40] = "`SenderHousing` = '" + user["SenderHousing"] + "', ";
                usersInsertUpdateStatementArray[41] = "`ProcessedDate` = '" + OtherMethods.FullDateTimeConvert(user["ProcessedDate"].ToString()) + "', ";
                usersInsertUpdateStatementArray[42] = "`ContactDate` = '" + OtherMethods.FullDateTimeConvert(user["ContactDate"].ToString()) + "', ";
                usersInsertUpdateStatementArray[43] = "`ActivatedDate` = '" + OtherMethods.FullDateTimeConvert(user["ActivatedDate"].ToString()) + "', ";
                usersInsertUpdateStatementArray[44] = "`StatusStady` = " + user["StatusStady"] + " ";

                usersInsertUpdateStatementArray[60] = "ON DUPLICATE KEY UPDATE ";

                usersInsertUpdateStatementArray[61] = "`Login` = '" + user["Login"] + "',";
                usersInsertUpdateStatementArray[62] = "`Password` = '" + user["Password"] + "',";
                usersInsertUpdateStatementArray[63] = "`Email` = '" + user["Email"] + "',";
                usersInsertUpdateStatementArray[64] = "`Family` = '" + user["Family"] + "',";
                usersInsertUpdateStatementArray[65] = "`Name` = '" + user["Name"] + "',";
                usersInsertUpdateStatementArray[66] = "`Role` = '" + user["Role"] + "',";
                usersInsertUpdateStatementArray[67] = "`Status` = " + user["Status"] + ",";
                usersInsertUpdateStatementArray[68] = "`Phone` = '" + user["Phone"] + "',";
                usersInsertUpdateStatementArray[69] = "`CreateDate` = '" + OtherMethods.FullDateTimeConvert(user["CreateDate"].ToString()) + "',";
                usersInsertUpdateStatementArray[70] = "`ChangeDate` = '" + OtherMethods.FullDateTimeConvert(user["ChangeDate"].ToString()) + "',";
                usersInsertUpdateStatementArray[71] = "`IsCourse` = " + user["IsCourse"] + ",";
                usersInsertUpdateStatementArray[72] = "`Note` = '" + user["Note"] + "',";
                usersInsertUpdateStatementArray[73] = "`Discount` = " + user["Discount"] + ",";
                usersInsertUpdateStatementArray[74] = "`SpecialClient` = " + user["SpecialClient"] + ",";
                usersInsertUpdateStatementArray[75] = "`ManagerID` = " + user["ManagerID"] + ",";
                usersInsertUpdateStatementArray[76] = "`SalesManagerID` = " + user["SalesManagerID"] + ",";
                usersInsertUpdateStatementArray[77] = "`AccessOnlyByWhiteList` = " + user["AccessOnlyByWhiteList"] + ",";
                usersInsertUpdateStatementArray[78] = "`Address` = '" + user["Address"] + "',";
                usersInsertUpdateStatementArray[79] = "`PhoneWorkOne` = '" + user["PhoneWorkOne"] + "',";
                usersInsertUpdateStatementArray[80] = "`PhoneWorkTwo` = '" + user["PhoneWorkTwo"] + "',";
                usersInsertUpdateStatementArray[81] = "`PhoneHome` = '" + user["PhoneHome"] + "',";
                usersInsertUpdateStatementArray[82] = "`PassportSeria` = '" + user["PassportSeria"] + "',";
                usersInsertUpdateStatementArray[83] = "`PassportNumber` = '" + user["PassportNumber"] + "',";
                usersInsertUpdateStatementArray[84] = "`PersonalNumber` = '" + user["PersonalNumber"] + "',";
                usersInsertUpdateStatementArray[85] = "`ROVD` = '" + user["ROVD"] + "',";
                usersInsertUpdateStatementArray[86] = "`Validity` = '" + OtherMethods.FullDateTimeConvert(user["Validity"].ToString()) + "',";
                usersInsertUpdateStatementArray[87] = "`RegistrationAddress` = '" + user["RegistrationAddress"] + "',";
                usersInsertUpdateStatementArray[88] = "`BirthDay` = '" + OtherMethods.FullDateTimeConvert(user["BirthDay"].ToString()) + "',";
                usersInsertUpdateStatementArray[89] = "`DateOfIssue` = '" + OtherMethods.FullDateTimeConvert(user["DateOfIssue"].ToString()) + "',";
                usersInsertUpdateStatementArray[90] = "`Skype` = '" + user["Skype"] + "', ";
                usersInsertUpdateStatementArray[91] = "`RedClient` = " + user["RedClient"] + ", ";
                usersInsertUpdateStatementArray[92] = "`AllowApi` = " + user["AllowApi"] + ", ";
                usersInsertUpdateStatementArray[94] = "`SenderCityID` = " + GetSenderCityId(user["SenderCityID"].ToString()) + ", ";
                usersInsertUpdateStatementArray[95] = "`SenderStreetPrefix` = '" + user["SenderStreetPrefix"] + "', ";
                usersInsertUpdateStatementArray[96] = "`SenderStreetName` = '" + user["SenderStreetName"] + "', ";
                usersInsertUpdateStatementArray[97] = "`SenderStreetNumber` = '" + user["SenderStreetNumber"] + "', ";
                usersInsertUpdateStatementArray[98] = "`SenderApartmentNumber` = '" + user["SenderApartmentNumber"] + "', ";
                usersInsertUpdateStatementArray[99] = "`SenderHousing` = '" + user["SenderHousing"] + "', ";
                usersInsertUpdateStatementArray[100] = "`ProcessedDate` = '" + OtherMethods.FullDateTimeConvert(user["ProcessedDate"].ToString()) + "', ";
                usersInsertUpdateStatementArray[101] = "`ContactDate` = '" + OtherMethods.FullDateTimeConvert(user["ContactDate"].ToString()) + "', ";
                usersInsertUpdateStatementArray[102] = "`ActivatedDate` = '" + OtherMethods.FullDateTimeConvert(user["ActivatedDate"].ToString()) + "', ";
                usersInsertUpdateStatementArray[103] = "`StatusStady` = " + user["StatusStady"] + "; ";

                #endregion

                for (int i = 0; i < 120; i++)
                {
                    if (!String.IsNullOrEmpty(usersInsertUpdateStatementArray[i]))
                        usersInsertUpdateStatement.Append(usersInsertUpdateStatementArray[i]);
                }
            }
            #endregion

            #region UsersProfiles   INSERT/UPDATE statement
            foreach (DataRow userprofile in allDataForUpdate.Tables[5].Rows)
            {
                #region usersprofilesInsertUpdateStatementArray
                usersprofilesInsertUpdateStatementArray[0] = "INSERT IGNORE INTO `usersprofiles` SET ";
                usersprofilesInsertUpdateStatementArray[1] = "`ID` = " + userprofile["ID"] + ",";
                usersprofilesInsertUpdateStatementArray[2] = "`UserID` = " + userprofile["UserID"] + ",";
                usersprofilesInsertUpdateStatementArray[3] = "`TypeID` = " + userprofile["TypeID"] + ",";
                usersprofilesInsertUpdateStatementArray[4] = "`FirstName` = '" + userprofile["FirstName"] + "',";
                usersprofilesInsertUpdateStatementArray[5] = "`LastName` = '" + userprofile["LastName"] + "',";
                usersprofilesInsertUpdateStatementArray[6] = "`ThirdName` = '" + userprofile["ThirdName"] + "',";
                usersprofilesInsertUpdateStatementArray[7] = "`DirectorPhoneNumber` = '" + userprofile["DirectorPhoneNumber"] + "',";
                usersprofilesInsertUpdateStatementArray[8] = "`PassportNumber` = '" + userprofile["PassportNumber"] + "',";
                usersprofilesInsertUpdateStatementArray[9] = "`PassportSeria` = '" + userprofile["PassportSeria"] + "',";
                usersprofilesInsertUpdateStatementArray[10] = "`PassportData` = '" + userprofile["PassportData"] + "',";
                usersprofilesInsertUpdateStatementArray[11] = "`PassportDate` = '" + OtherMethods.FullDateTimeConvert(userprofile["PassportDate"].ToString()) + "',";
                usersprofilesInsertUpdateStatementArray[12] = "`Address` = '" + userprofile["Address"] + "',";
                usersprofilesInsertUpdateStatementArray[13] = "`CompanyName` = '" + userprofile["CompanyName"] + "',";
                usersprofilesInsertUpdateStatementArray[14] = "`CompanyAddress` = '" + userprofile["CompanyAddress"] + "',";
                usersprofilesInsertUpdateStatementArray[15] = "`PostAddress` = '" + userprofile["PostAddress"] + "',";
                usersprofilesInsertUpdateStatementArray[16] = "`RasShet` = '" + userprofile["RasShet"] + "',";
                usersprofilesInsertUpdateStatementArray[17] = "`UNP` = '" + userprofile["UNP"] + "',";
                usersprofilesInsertUpdateStatementArray[18] = "`BankName` = '" + userprofile["BankName"] + "',";
                usersprofilesInsertUpdateStatementArray[19] = "`BankCode` = '" + userprofile["BankCode"] + "',";
                usersprofilesInsertUpdateStatementArray[20] = "`BankAddress` = '" + userprofile["BankAddress"] + "',";
                usersprofilesInsertUpdateStatementArray[21] = "`CreateDate` = '" + OtherMethods.FullDateTimeConvert(userprofile["CreateDate"].ToString()) + "',";
                usersprofilesInsertUpdateStatementArray[22] = "`ChangeDate` = '" + OtherMethods.FullDateTimeConvert(userprofile["ChangeDate"].ToString()) + "',";
                usersprofilesInsertUpdateStatementArray[23] = "`IsDefault` = " + userprofile["IsDefault"] + ",";
                usersprofilesInsertUpdateStatementArray[24] = "`ContactPersonFIO` = '" + userprofile["ContactPersonFIO"] + "',";
                usersprofilesInsertUpdateStatementArray[25] = "`ContactPhoneNumbers` = '" + userprofile["ContactPhoneNumbers"] + "',";
                usersprofilesInsertUpdateStatementArray[26] = "`StatusID` = " + userprofile["StatusID"] + ",";
                usersprofilesInsertUpdateStatementArray[27] = "`RejectBlockedMessage` = '" + userprofile["RejectBlockedMessage"] + "',";
                usersprofilesInsertUpdateStatementArray[28] = "`AgreementDate` = '" + OtherMethods.FullDateTimeConvert(userprofile["AgreementDate"].ToString()) + "',";
                usersprofilesInsertUpdateStatementArray[29] = "`AgreementNumber` = '" + userprofile["AgreementNumber"] + "' ";

                usersprofilesInsertUpdateStatementArray[60] = "ON DUPLICATE KEY UPDATE ";

                usersprofilesInsertUpdateStatementArray[62] = "`UserID` = " + userprofile["UserID"] + ",";
                usersprofilesInsertUpdateStatementArray[63] = "`TypeID` = " + userprofile["TypeID"] + ",";
                usersprofilesInsertUpdateStatementArray[64] = "`FirstName` = '" + userprofile["FirstName"] + "',";
                usersprofilesInsertUpdateStatementArray[65] = "`LastName` = '" + userprofile["LastName"] + "',";
                usersprofilesInsertUpdateStatementArray[66] = "`ThirdName` = '" + userprofile["ThirdName"] + "',";
                usersprofilesInsertUpdateStatementArray[67] = "`DirectorPhoneNumber` = '" + userprofile["DirectorPhoneNumber"] + "',";
                usersprofilesInsertUpdateStatementArray[68] = "`PassportNumber` = '" + userprofile["PassportNumber"] + "',";
                usersprofilesInsertUpdateStatementArray[69] = "`PassportSeria` = '" + userprofile["PassportSeria"] + "',";
                usersprofilesInsertUpdateStatementArray[70] = "`PassportData` = '" + userprofile["PassportData"] + "',";
                usersprofilesInsertUpdateStatementArray[71] = "`PassportDate` = '" + OtherMethods.FullDateTimeConvert(userprofile["PassportDate"].ToString()) + "',";
                usersprofilesInsertUpdateStatementArray[72] = "`Address` = '" + userprofile["Address"] + "',";
                usersprofilesInsertUpdateStatementArray[73] = "`CompanyName` = '" + userprofile["CompanyName"] + "',";
                usersprofilesInsertUpdateStatementArray[74] = "`CompanyAddress` = '" + userprofile["CompanyAddress"] + "',";
                usersprofilesInsertUpdateStatementArray[75] = "`PostAddress` = '" + userprofile["PostAddress"] + "',";
                usersprofilesInsertUpdateStatementArray[76] = "`RasShet` = '" + userprofile["RasShet"] + "',";
                usersprofilesInsertUpdateStatementArray[77] = "`UNP` = '" + userprofile["UNP"] + "',";
                usersprofilesInsertUpdateStatementArray[78] = "`BankName` = '" + userprofile["BankName"] + "',";
                usersprofilesInsertUpdateStatementArray[79] = "`BankCode` = '" + userprofile["BankCode"] + "',";
                usersprofilesInsertUpdateStatementArray[80] = "`BankAddress` = '" + userprofile["BankAddress"] + "',";
                usersprofilesInsertUpdateStatementArray[81] = "`CreateDate` = '" + OtherMethods.FullDateTimeConvert(userprofile["CreateDate"].ToString()) + "',";
                usersprofilesInsertUpdateStatementArray[82] = "`ChangeDate` = '" + OtherMethods.FullDateTimeConvert(userprofile["ChangeDate"].ToString()) + "',";
                usersprofilesInsertUpdateStatementArray[83] = "`IsDefault` = " + userprofile["IsDefault"] + ",";
                usersprofilesInsertUpdateStatementArray[84] = "`ContactPersonFIO` = '" + userprofile["ContactPersonFIO"] + "',";
                usersprofilesInsertUpdateStatementArray[85] = "`ContactPhoneNumbers` = '" + userprofile["ContactPhoneNumbers"] + "',";
                usersprofilesInsertUpdateStatementArray[86] = "`StatusID` = " + userprofile["StatusID"] + ",";
                usersprofilesInsertUpdateStatementArray[87] = "`RejectBlockedMessage` = '" + userprofile["RejectBlockedMessage"] + "',";
                usersprofilesInsertUpdateStatementArray[88] = "`AgreementDate` = '" + OtherMethods.FullDateTimeConvert(userprofile["AgreementDate"].ToString()) + "',";
                usersprofilesInsertUpdateStatementArray[89] = "`AgreementNumber` = '" + userprofile["AgreementNumber"] + "'; ";

                #endregion

                for (int i = 0; i < 120; i++)
                {
                    if (!String.IsNullOrEmpty(usersprofilesInsertUpdateStatementArray[i]))
                        usersprofilesInsertUpdateStatement.Append(usersprofilesInsertUpdateStatementArray[i]);
                }
            }
            #endregion

            #region Drivers         INSERT/UPDATE statement
            foreach (DataRow driver in allDataForUpdate.Tables[3].Rows)
            {
                #region driversInsertUpdateStatementArray
                driversInsertUpdateStatementArray[0] = "INSERT IGNORE INTO `drivers` SET ";
                driversInsertUpdateStatementArray[1] = "`ID` = " + driver["ID"] + ",";
                driversInsertUpdateStatementArray[2] = "`CarID` = " + driver["CarID"] + ",";
                driversInsertUpdateStatementArray[3] = "`StatusID` = " + driver["StatusID"] + ",";
                driversInsertUpdateStatementArray[4] = "`FirstName` = '" + driver["FirstName"] + "',";
                driversInsertUpdateStatementArray[5] = "`LastName` = '" + driver["LastName"] + "',";
                driversInsertUpdateStatementArray[6] = "`ThirdName` = '" + driver["ThirdName"] + "',";
                driversInsertUpdateStatementArray[7] = "`PhoneOne` = '" + driver["PhoneOne"] + "',";
                driversInsertUpdateStatementArray[8] = "`PhoneTwo` = '" + driver["PhoneTwo"] + "',";
                driversInsertUpdateStatementArray[9] = "`CreateDate` = '" + OtherMethods.FullDateTimeConvert(driver["CreateDate"].ToString()) + "',";
                driversInsertUpdateStatementArray[10] = "`ChangeDate` = '" + OtherMethods.FullDateTimeConvert(driver["ChangeDate"].ToString()) + "',";
                driversInsertUpdateStatementArray[11] = "`CityOrder` = '" + driver["CityOrder"] + "',";
                driversInsertUpdateStatementArray[12] = "`PassportSeria` = '" + driver["PassportSeria"] + "',";
                driversInsertUpdateStatementArray[13] = "`PassportNumber` = '" + driver["PassportNumber"] + "',";
                driversInsertUpdateStatementArray[14] = "`PersonalNumber` = '" + driver["PersonalNumber"] + "',";
                driversInsertUpdateStatementArray[15] = "`ROVD` = '" + driver["ROVD"] + "',";
                driversInsertUpdateStatementArray[16] = "`Validity` = '" + OtherMethods.FullDateTimeConvert(driver["Validity"].ToString()) + "',";
                driversInsertUpdateStatementArray[17] = "`RegistrationAddress` = '" + driver["RegistrationAddress"] + "',";
                driversInsertUpdateStatementArray[18] = "`BirthDay` = '" + OtherMethods.FullDateTimeConvert(driver["BirthDay"].ToString()) + "',";
                driversInsertUpdateStatementArray[19] = "`DateOfIssue` = '" + OtherMethods.FullDateTimeConvert(driver["DateOfIssue"].ToString()) + "',";
                driversInsertUpdateStatementArray[20] = "`HomeAddress` = '" + driver["HomeAddress"] + "',";
                driversInsertUpdateStatementArray[21] = "`HomePhone` = '" + driver["HomePhone"] + "',";
                driversInsertUpdateStatementArray[22] = "`ContactPersonPhone` = '" + driver["ContactPersonPhone"] + "',";
                driversInsertUpdateStatementArray[23] = "`ContactPersonFIO` = '" + driver["ContactPersonFIO"] + "',";
                driversInsertUpdateStatementArray[24] = "`DriverPassport` = '" + driver["DriverPassport"] + "',";
                driversInsertUpdateStatementArray[25] = "`DriverPassportDateOfIssue` = '" + driver["DriverPassportDateOfIssue"] + "',";
                driversInsertUpdateStatementArray[26] = "`DriverPassportValidity` = '" + driver["DriverPassportValidity"] + "',";
                driversInsertUpdateStatementArray[27] = "`MedPolisDateOfIssue` = '" + driver["MedPolisDateOfIssue"] + "',";
                driversInsertUpdateStatementArray[29] = "`MedPolisValidity` = '" + driver["MedPolisValidity"] + "' ";

                driversInsertUpdateStatementArray[60] = "ON DUPLICATE KEY UPDATE ";

                driversInsertUpdateStatementArray[61] = "`CarID` = " + driver["CarID"] + ",";
                driversInsertUpdateStatementArray[62] = "`StatusID` = " + driver["StatusID"] + ",";
                driversInsertUpdateStatementArray[63] = "`FirstName` = '" + driver["FirstName"] + "',";
                driversInsertUpdateStatementArray[64] = "`LastName` = '" + driver["LastName"] + "',";
                driversInsertUpdateStatementArray[65] = "`ThirdName` = '" + driver["ThirdName"] + "',";
                driversInsertUpdateStatementArray[66] = "`PhoneOne` = '" + driver["PhoneOne"] + "',";
                driversInsertUpdateStatementArray[67] = "`PhoneTwo` = '" + driver["PhoneTwo"] + "',";
                driversInsertUpdateStatementArray[68] = "`CreateDate` = '" + OtherMethods.FullDateTimeConvert(driver["CreateDate"].ToString()) + "',";
                driversInsertUpdateStatementArray[69] = "`ChangeDate` = '" + OtherMethods.FullDateTimeConvert(driver["ChangeDate"].ToString()) + "',";
                driversInsertUpdateStatementArray[70] = "`CityOrder` = '" + driver["CityOrder"] + "',";
                driversInsertUpdateStatementArray[71] = "`PassportSeria` = '" + driver["PassportSeria"] + "',";
                driversInsertUpdateStatementArray[72] = "`PassportNumber` = '" + driver["PassportNumber"] + "',";
                driversInsertUpdateStatementArray[73] = "`PersonalNumber` = '" + driver["PersonalNumber"] + "',";
                driversInsertUpdateStatementArray[74] = "`ROVD` = '" + driver["ROVD"] + "',";
                driversInsertUpdateStatementArray[75] = "`Validity` = '" + OtherMethods.FullDateTimeConvert(driver["Validity"].ToString()) + "',";
                driversInsertUpdateStatementArray[76] = "`RegistrationAddress` = '" + driver["RegistrationAddress"] + "',";
                driversInsertUpdateStatementArray[77] = "`BirthDay` = '" + OtherMethods.FullDateTimeConvert(driver["BirthDay"].ToString()) + "',";
                driversInsertUpdateStatementArray[78] = "`DateOfIssue` = '" + OtherMethods.FullDateTimeConvert(driver["DateOfIssue"].ToString()) + "',";
                driversInsertUpdateStatementArray[79] = "`HomeAddress` = '" + driver["HomeAddress"] + "',";
                driversInsertUpdateStatementArray[80] = "`HomePhone` = '" + driver["HomePhone"] + "',";
                driversInsertUpdateStatementArray[81] = "`ContactPersonPhone` = '" + driver["ContactPersonPhone"] + "',";
                driversInsertUpdateStatementArray[82] = "`ContactPersonFIO` = '" + driver["ContactPersonFIO"] + "',";
                driversInsertUpdateStatementArray[83] = "`DriverPassport` = '" + driver["DriverPassport"] + "',";
                driversInsertUpdateStatementArray[84] = "`DriverPassportDateOfIssue` = '" + driver["DriverPassportDateOfIssue"] + "',";
                driversInsertUpdateStatementArray[85] = "`DriverPassportValidity` = '" + driver["DriverPassportValidity"] + "',";
                driversInsertUpdateStatementArray[86] = "`MedPolisDateOfIssue` = '" + driver["MedPolisDateOfIssue"] + "',";
                driversInsertUpdateStatementArray[87] = "`MedPolisValidity` = '" + driver["MedPolisValidity"] + "'; ";

                #endregion

                for (int i = 0; i < 120; i++)
                {
                    if (!String.IsNullOrEmpty(driversInsertUpdateStatementArray[i]))
                        driversInsertUpdateStatement.Append(driversInsertUpdateStatementArray[i]);
                }
            }
            #endregion

            #region Cars            INSERT/UPDATE statement
            foreach (DataRow car in allDataForUpdate.Tables[4].Rows)
            {
                #region carsInsertUpdateStatementArray
                carsInsertUpdateStatementArray[0] = "INSERT IGNORE INTO `cars` SET ";
                carsInsertUpdateStatementArray[1] = "`ID` = " + car["ID"] + ",";
                carsInsertUpdateStatementArray[2] = "`Model` = '" + car["Model"] + "',";
                carsInsertUpdateStatementArray[3] = "`Number` = '" + car["Number"] + "',";
                carsInsertUpdateStatementArray[4] = "`TypeID` = " + car["TypeID"] + ",";
                carsInsertUpdateStatementArray[5] = "`CreateDate` = '" + OtherMethods.FullDateTimeConvert(car["CreateDate"].ToString()) + "',";
                carsInsertUpdateStatementArray[6] = "`ChangeDate` = '" + OtherMethods.FullDateTimeConvert(car["ChangeDate"].ToString()) + "',";
                carsInsertUpdateStatementArray[7] = "`CompanyName` = '" + car["CompanyName"] + "',";
                carsInsertUpdateStatementArray[8] = "`FirstName` = '" + car["FirstName"] + "',";
                carsInsertUpdateStatementArray[9] = "`LastName` = '" + car["LastName"] + "',";
                carsInsertUpdateStatementArray[10] = "`ThirdName` = '" + car["ThirdName"] + "',";
                carsInsertUpdateStatementArray[11] = "`PassportSeria` = '" + car["PassportSeria"] + "',";
                carsInsertUpdateStatementArray[12] = "`PassportNumber` = '" + car["PassportNumber"] + "',";
                carsInsertUpdateStatementArray[13] = "`PersonalNumber` = '" + car["PersonalNumber"] + "',";
                carsInsertUpdateStatementArray[14] = "`ROVD` = '" + car["ROVD"] + "',";
                carsInsertUpdateStatementArray[15] = "`Validity` = '" + OtherMethods.FullDateTimeConvert(car["Validity"].ToString()) + "',";
                carsInsertUpdateStatementArray[16] = "`DateOfIssue` = '" + OtherMethods.FullDateTimeConvert(car["DateOfIssue"].ToString()) + "',";
                carsInsertUpdateStatementArray[17] = "`BirthDay` = '" + OtherMethods.FullDateTimeConvert(car["BirthDay"].ToString()) + "',";
                carsInsertUpdateStatementArray[18] = "`RegistrationAddress` = '" + car["RegistrationAddress"] + "' ";

                carsInsertUpdateStatementArray[30] = "ON DUPLICATE KEY UPDATE ";

                carsInsertUpdateStatementArray[31] = "`Model` = '" + car["Model"] + "',";
                carsInsertUpdateStatementArray[32] = "`Number` = '" + car["Number"] + "',";
                carsInsertUpdateStatementArray[33] = "`TypeID` = " + car["TypeID"] + ",";
                carsInsertUpdateStatementArray[34] = "`CreateDate` = '" + car["CreateDate"] + "',";
                carsInsertUpdateStatementArray[35] = "`ChangeDate` = '" + OtherMethods.FullDateTimeConvert(car["ChangeDate"].ToString()) + "',";
                carsInsertUpdateStatementArray[36] = "`CompanyName` = '" + OtherMethods.FullDateTimeConvert(car["CompanyName"].ToString()) + "',";
                carsInsertUpdateStatementArray[37] = "`FirstName` = '" + car["FirstName"] + "',";
                carsInsertUpdateStatementArray[38] = "`LastName` = '" + car["LastName"] + "',";
                carsInsertUpdateStatementArray[39] = "`ThirdName` = '" + car["ThirdName"] + "',";
                carsInsertUpdateStatementArray[40] = "`PassportSeria` = '" + car["PassportSeria"] + "',";
                carsInsertUpdateStatementArray[41] = "`PassportNumber` = '" + car["PassportNumber"] + "',";
                carsInsertUpdateStatementArray[42] = "`PersonalNumber` = '" + car["PersonalNumber"] + "',";
                carsInsertUpdateStatementArray[43] = "`ROVD` = '" + car["ROVD"] + "',";
                carsInsertUpdateStatementArray[44] = "`Validity` = '" + OtherMethods.FullDateTimeConvert(car["Validity"].ToString()) + "',";
                carsInsertUpdateStatementArray[45] = "`DateOfIssue` = '" + OtherMethods.FullDateTimeConvert(car["DateOfIssue"].ToString()) + "',";
                carsInsertUpdateStatementArray[46] = "`BirthDay` = '" + OtherMethods.FullDateTimeConvert(car["BirthDay"].ToString()) + "',";
                carsInsertUpdateStatementArray[47] = "`RegistrationAddress` = '" + car["RegistrationAddress"] + "'; ";

                #endregion

                for (int i = 0; i < 60; i++)
                {
                    if (!String.IsNullOrEmpty(carsInsertUpdateStatementArray[i]))
                        carsInsertUpdateStatement.Append(carsInsertUpdateStatementArray[i]);
                }
            }
            #endregion


            var resultTicketsString = ticketsInsertUpdateStatement.ToString();
            var resultdriverString = goodsInsertUpdateStatement.ToString();
            var resultUsersString = usersInsertUpdateStatement.ToString();
            var resultUsersprofilesString = usersprofilesInsertUpdateStatement.ToString();
            var resultDriversString = driversInsertUpdateStatement.ToString();
            var resultCarsString = carsInsertUpdateStatement.ToString();
            var result =
                resultTicketsString +
                resultdriverString +
                resultUsersString +
                resultUsersprofilesString +
                resultDriversString +
                resultCarsString;

            File.WriteAllText(HttpContext.Current.Server.MapPath("~/sync.json"), result, Encoding.UTF8);
        }

        public static string SqlSender(string serverAddress)
        {
            string serverUrl = "http://" + serverAddress + "/AppServices/SynchronizationApi.asmx/GetSql";
            var appKey = Globals.Settings.AppServiceSecureKey;
            string currentServerAddress;
            var currentServer = BackendHelper.TagToValue("server_name");
            switch (currentServer)
            {
                case "doc":
                    currentServerAddress = BackendHelper.TagToValue("doc_server_address");
                    break;
                case "test":
                    currentServerAddress = BackendHelper.TagToValue("test_server_address");
                    break;
                case "arch":
                    currentServerAddress = BackendHelper.TagToValue("arch_server_address");
                    break;
                case "primary":
                    currentServerAddress = BackendHelper.TagToValue("primary_server_address");
                    break;
                case "local":
                    currentServerAddress = BackendHelper.TagToValue("local_server_address");
                    break;
                default:
                    currentServerAddress = BackendHelper.TagToValue("doc_server_address");
                    break;
            }

            string postString = string.Format("appkey={1}&resourseServerAddress={0}", currentServerAddress, appKey);
            var webRequest = (HttpWebRequest)WebRequest.Create(serverUrl);
            webRequest.Method = "POST";
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.ContentLength = postString.Length;

            // POST the data
            using (StreamWriter requestWriter2 = new StreamWriter(webRequest.GetRequestStream()))
            {
                requestWriter2.Write(postString);
            }

            using (var responseReader = new StreamReader(webRequest.GetResponse().GetResponseStream(), Encoding.UTF8))
            {
                return responseReader.ReadToEnd();
            }
        }

        private static int GetSenderCityId ( string id )
        {
            return !string.IsNullOrEmpty(id) ? Convert.ToInt32(id) : 0;
        }
    }
}