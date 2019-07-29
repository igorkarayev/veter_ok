function LoadCommentHistory(appkey, ticketId, listViewControlFullId) {
    $.ajax({
        type: "POST",
        url: "../../../AppServices/LoadAjaxService.asmx/LoadCommentHistory",
        data: ({
            appkey: appkey,
            ticketid: ticketId
        }),
        success: function (response) {
            $(listViewControlFullId + '_Comment_' + ticketId + '_lblCommentHistory_' + ticketId).html(response);
        },
        error: function (result) {
                
        }
    });
}


function LoadCommentClientHistory(appkey, clientid, listViewControlFullId) {
    $.ajax({
        type: "POST",
        url: "../../../AppServices/LoadAjaxService.asmx/LoadCommentClientHistory",
        data: ({
            appkey: appkey,
            clientid: clientid
        }),
        success: function (response) {
            $(listViewControlFullId + '_Comment_' + clientid + '_lblCommentHistory_' + clientid).html(response);
        },
        error: function (result) {

        }
    });
}