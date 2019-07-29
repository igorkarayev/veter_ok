namespace Delivery.AppServices
{
    public class AppServicesHelper
    {
        //AJAX Start

        public static string SaveWeightStringOnBlur(string ticketId)
        {
            var result = "saveWeight(\"" + ticketId + "\");";
            return result;
        }

        public static string SaveWeightStringOnFocus(string ticketId)
        {
            var result = "$(\"#ctl00_MainContent_lvAllTickets_lblSaveWeightStatus_" + ticketId + "\").html(\"\");" +
                         "oldValueWeight = $(\"#ctl00_MainContent_lvAllTickets_tbWeight_" + ticketId + "\").val();";
            return result;
        }

        public static string SaveUsersDiscountOnBlur(string userId)
        {
            var result = "saveUsersDiscount(\"" + userId + "\");";
            return result;
        }

        public static string SaveUsersDiscountOnFocus(string userId)
        {
            var result = "$(\"#ctl00_MainContent_lvAllUsers_lblSaveUsersDiscountStatus_" + userId + "\").html(\"\");" +
                         "oldValueUsersDiscount = $(\"#ctl00_MainContent_lvAllUsers_tbUsersDiscount_" + userId + "\").val();";
            return result;
        }


        public static string SaveBLROnBlur(string userId)
        {
            var result = "saveBLR(\"" + userId + "\");";
            return result;
        }

        public static string SaveBLROnFocus(string userId)
        {
            var result = "$(\"#ctl00_MainContent_lvAllTickets_lblReceivedBLRStatus_" + userId + "\").html(\"\");" +
                         "oldValBLR = $(\"#ctl00_MainContent_lvAllTickets_tbReceivedBLR_" + userId + "\").val();";
            return result;
        }


        public static string SaveUSDOnBlur(string userId)
        {
            var result = "saveUSD(\"" + userId + "\");";
            return result;
        }

        public static string SaveUSDOnFocus(string userId)
        {
            var result = "$(\"#ctl00_MainContent_lvAllTickets_lblReceivedUSDStatus_" + userId + "\").html(\"\");" +
                         "oldValUSD = $(\"#ctl00_MainContent_lvAllTickets_tbReceivedUSD_" + userId + "\").val();";
            return result;
        }


        public static string SaveEUROnBlur(string userId)
        {
            var result = "saveEUR(\"" + userId + "\");";
            return result;
        }

        public static string SaveEUROnFocus(string userId)
        {
            var result = "$(\"#ctl00_MainContent_lvAllTickets_lblReceivedEURStatus_" + userId + "\").html(\"\");" +
                         "oldValEUR = $(\"#ctl00_MainContent_lvAllTickets_tbReceivedEUR_" + userId + "\").val();";
            return result;
        }


        public static string SaveRUROnBlur(string userId)
        {
            var result = "saveRUR(\"" + userId + "\");";
            return result;
        }

        public static string SaveRUROnFocus(string userId)
        {
            var result = "$(\"#ctl00_MainContent_lvAllTickets_lblReceivedRURStatus_" + userId + "\").html(\"\");" +
                         "oldValRUR = $(\"#ctl00_MainContent_lvAllTickets_tbReceivedRUR_" + userId + "\").val();";
            return result;
        }


        public static string SaveStatusMoneyOnBlur(string ticketId)
        {
            var result = "saveStatusMoney(\"" + ticketId + "\");";
            return result;
        }

        public static string SaveStatusMoneyOnFocus(string ticketId)
        {
            var result = "$(\"#ctl00_MainContent_lvAllTickets_lblStatusMoneyStatus_" + ticketId + "\").html(\"\");" +
                         "oldValStatusMoney = $(\"#ctl00_MainContent_lvAllTickets_ddlStatusMoney_" + ticketId + "\").val();";
            return result;
        }







        public static string RUROnBlur(string ticketId)
        {
            var result = "if" +
                             "(  $('#ctl00_MainContent_lvAllTickets_tbReceivedRURCourse_" + ticketId + "' ).val() != '' " +
                             "&& $('#ctl00_MainContent_lvAllTickets_tbReceivedRURCourse_" + ticketId + "' ).val() != 0 " +
                             "&& $('#ctl00_MainContent_lvAllTickets_tbReceivedRURCourse_" + ticketId + "' ).val() != 1)" +
                             "{" +
                                "$('#ctl00_MainContent_lvAllTickets_tbReceivedRUR_" + ticketId + "' ).prop( 'disabled', false );" +
                                "$('#ctl00_MainContent_lvAllTickets_tbReceivedRUR_" + ticketId + "' ).removeClass('aspNetDisabled');" +
                             "} " + 
                         "else " +
                             "{" +
                                "$('#ctl00_MainContent_lvAllTickets_tbReceivedRUR_" + ticketId + "' ).prop( 'disabled', true ); " +
                                "$('#ctl00_MainContent_lvAllTickets_tbReceivedRUR_" + ticketId + "' ).addClass('aspNetDisabled');" +
                                "oldValRUR = $('#ctl00_MainContent_lvAllTickets_tbReceivedRUR_" + ticketId + "' ).val();" +
                                "$('#ctl00_MainContent_lvAllTickets_tbReceivedRUR_" + ticketId + "' ).val('0'); " +
                                "saveRUR('" + ticketId + "');" +
                             "}";
            return result;
        }

        public static string USDOnBlur(string ticketId)
        {
            var result = "if" +
                            "(  $('#ctl00_MainContent_lvAllTickets_tbReceivedUSDCourse_" + ticketId + "' ).val() != '' " +
                            "&& $('#ctl00_MainContent_lvAllTickets_tbReceivedUSDCourse_" + ticketId + "' ).val() != 0 " +
                            "&& $('#ctl00_MainContent_lvAllTickets_tbReceivedUSDCourse_" + ticketId + "' ).val() != 1)" +
                            "{" +
                                "$('#ctl00_MainContent_lvAllTickets_tbReceivedUSD_" + ticketId + "' ).prop( 'disabled', false );" +
                                "$('#ctl00_MainContent_lvAllTickets_tbReceivedUSD_" + ticketId + "' ).removeClass('aspNetDisabled');" +
                             "} " +
                         "else " +
                            "{" +
                                "$('#ctl00_MainContent_lvAllTickets_tbReceivedUSD_" + ticketId + "' ).prop( 'disabled', true ); " +
                                "$('#ctl00_MainContent_lvAllTickets_tbReceivedUSD_" + ticketId + "' ).addClass('aspNetDisabled');" +
                                "oldValUSD = $('#ctl00_MainContent_lvAllTickets_tbReceivedUSD_" + ticketId + "' ).val();" +
                                "$('#ctl00_MainContent_lvAllTickets_tbReceivedUSD_" + ticketId + "' ).val('0'); " +
                                "saveUSD('" + ticketId + "');" +
                             "}";
            return result;
        }

        public static string EUROnBlur(string ticketId)
        {
            var result = "if" +
                             "(  $('#ctl00_MainContent_lvAllTickets_tbReceivedEURCourse_" + ticketId + "' ).val() != '' " +
                             "&& $('#ctl00_MainContent_lvAllTickets_tbReceivedEURCourse_" + ticketId + "' ).val() != 0 " +
                             "&& $('#ctl00_MainContent_lvAllTickets_tbReceivedEURCourse_" + ticketId + "' ).val() != 1)" +
                             "{" +
                                 "$('#ctl00_MainContent_lvAllTickets_tbReceivedEUR_" + ticketId + "' ).prop( 'disabled', false );" +
                                 "$('#ctl00_MainContent_lvAllTickets_tbReceivedEUR_" + ticketId + "' ).removeClass('aspNetDisabled');" +
                             "} " +
                         "else " +
                             "{" +
                                 "$('#ctl00_MainContent_lvAllTickets_tbReceivedEUR_" + ticketId + "' ).prop( 'disabled', true ); " +
                                 "$('#ctl00_MainContent_lvAllTickets_tbReceivedEUR_" + ticketId + "' ).addClass('aspNetDisabled');" +
                                 "oldValEUR = $('#ctl00_MainContent_lvAllTickets_tbReceivedEUR_" + ticketId + "' ).val();" +
                                 "$('#ctl00_MainContent_lvAllTickets_tbReceivedEUR_" + ticketId + "' ).val('0'); " +
                                 "saveEUR('" + ticketId + "');" +
                             "}";
            return result;
        }
    }
}