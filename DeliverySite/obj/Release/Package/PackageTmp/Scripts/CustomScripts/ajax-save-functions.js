//Сохранение состояния чекбокса "С акцизой" НАЧАЛО
function SaveCheckboxWithoutAkciza(ticketId, goodsId, appkey, listViewControlFullId, userid, userip, pagename) {
    var checkbox = $(listViewControlFullId + '_GoodsList_' + ticketId + '_lvAllGoods_' + ticketId + '_cbWithoutAkciza_' + goodsId);
    var result = 0;
    if (checkbox.is(':checked')) {
        result = 1;
    }

    $.ajax({
        type: "POST",
        url: "../../../AppServices/SaveAjaxService.asmx/SaveCheckboxWithoutAkcizaValue",
        data: ({
            boolchecked: result,
            goodsid: goodsId,
            appkey: appkey,
            ticketid: ticketId,
            userid: userid,
            userip: userip,
            pagename: pagename
        }),
        success: function (response) {
            $(listViewControlFullId + '_GoodsList_' + ticketId + '_lvAllGoods_' + ticketId + '_lblStatus_' + goodsId).html("<span style='font-size: 10px; color: green;'>ok</span>");
            $(listViewControlFullId + '_GruzobozCost_' + ticketId + '_tbGruzobozCost_' + ticketId).attr('disabled', 'disabled');
            setTimeout(function () { ClearLabelStatusWithoutAkciza(ticketId, goodsId, listViewControlFullId); }, 3000); //очищаем только что присвоеный лейбл состояния Акциз
        },
        error: function (result) {
            $(listViewControlFullId + '_GoodsList_' + ticketId + '_lvAllGoods_' + ticketId + '_lblStatus_' + goodsId).html("<span style='font-size: 10px; color: red;'>err</span>");
        }
    });
}

function ClearLabelStatusWithoutAkciza(ticketId, goodsId, listViewControlFullId) {
    $(listViewControlFullId +'_GoodsList_' + ticketId + '_lvAllGoods_' + ticketId + '_lblStatus_' + goodsId).html("");
}
//Сохранение состояния чекбокса "С акцизой" КОНЕЦ


//Сохранение состояния чекбокса "без оплаты" НАЧАЛО
function SaveCheckboxWithoutMoney(ticketId, appkey, listViewControlFullId, userid, userip, pagename) {
    var checkbox = $(listViewControlFullId + '_MoneyForMoneyView_' + ticketId + '_cbWithoutMoney_' + ticketId);
    var result = 0;
    if (checkbox.is(':checked')) {
        result = 1;
    }

    $.ajax({
        type: "POST",
        url: "../../../AppServices/SaveAjaxService.asmx/SaveCheckboxWithoutMoneyValue",
        data: ({
            boolchecked: result,
            appkey: appkey,
            ticketid: ticketId,
            userid: userid,
            userip: userip,
            pagename: pagename
        }),
        success: function (response) {
            $('.lblStatus').html("<span style='font-size: 12px; color: white; font-weight: bold'>сохранено</span>");
            $('.lblStatus').show();
            setTimeout(function () { ClearLabelStatusWithoutMoney(ticketId, listViewControlFullId); }, 3000); //очищаем только что присвоеный лейбл состояния Акциз
        },
        error: function (result) {
            $('.lblStatus').html("<span style='font-size: 12px; color: red; font-weight: bold'>ошибка</span>");
            $('.lblStatus').show();
        }
    });
}

function ClearLabelStatusWithoutMoney(ticketId, listViewControlFullId) {
    $('.lblStatus').hide();
}
//Сохранение состояния чекбокса "без оплаты" КОНЕЦ


//Сохранение состояния чекбокса "обмен" НАЧАЛО
function SaveCheckboxIsExchange(ticketId, appkey, listViewControlFullId, userid, userip, pagename) {
    var checkbox = $(listViewControlFullId + '_MoneyForMoneyView_' + ticketId + '_cbIsExchange_' + ticketId);
    var result = 0;
    if (checkbox.is(':checked')) {
        result = 1;
    }

    $.ajax({
        type: "POST",
        url: "../AppServices/SaveAjaxService.asmx/SaveCheckboxIsExchangeValue",
        data: ({
            boolchecked: result,
            appkey: appkey,
            ticketid: ticketId,
            userid: userid,
            userip: userip,
            pagename: pagename
        }),
        success: function (response) {
            $('.lblStatus').html("<span style='font-size: 12px; color: white; font-weight: bold'>сохранено</span>");
            $('.lblStatus').show();
            setTimeout(function () { ClearLabelStatusWithoutMoney(ticketId, listViewControlFullId); }, 3000); //очищаем только что присвоеный лейбл состояния Акциз
        },
        error: function (result) {
            $('.lblStatus').html("<span style='font-size: 12px; color: red; font-weight: bold'>ошибка</span>");
            $('.lblStatus').show();
        }
    });
}

function ClearLabelStatusWithoutMoney(ticketId, listViewControlFullId) {
    $('.lblStatus').hide();
}
//Сохранение состояния чекбокса "обмен" КОНЕЦ

//Сохранение состояния чекбокса "бесплатно" НАЧАЛО
function SaveDeliveryCost(ticketId, appkey, listViewControlFullId, userid, userip, pagename) {
    var checkbox = $(listViewControlFullId + '_DeliveryCostForMoneyView_' + ticketId + '_cbWithoutMoney_' + ticketId);
    var hfdeliverycost = $(listViewControlFullId + '_DeliveryCostForMoneyView_' + ticketId + '_hfDeliveryCost_' + ticketId);
    var deliverycost;
    if (checkbox.is(':checked')) {
        deliverycost = 0;
    } else {
        deliverycost = hfdeliverycost.val();
    }

    $.ajax({
        type: "POST",
        url: "../../../AppServices/SaveAjaxService.asmx/SaveDeliveryCostValue",
        data: ({
            deliverycost: deliverycost,
            appkey: appkey,
            ticketid: ticketId,
            userid: userid,
            userip: userip,
            pagename: pagename
        }),
        success: function (response) {
            $('.lblStatus').html("<span style='font-size: 12px; color: white; font-weight: bold'>сохранено</span>");
            $('.lblStatus').show();
            setTimeout(function () { ClearLabelStatusWithoutMoney(ticketId, listViewControlFullId); }, 3000); //очищаем только что присвоеный лейбл состояния Акциз
        },
        error: function (result) {
            $('.lblStatus').html("<span style='font-size: 12px; color: red; font-weight: bold'>ошибка</span>");
            $('.lblStatus').show();
        }
    });
}

function ClearLabelStatusWithoutMoney(ticketId, listViewControlFullId) {
    $('.lblStatus').hide();
}
//Сохранение состояния чекбокса "бесплатно" КОНЕЦ

//Сохранение состояния дропдауна НН НАЧАЛО
function SavePrintNaklInMap(ticketId, appkey, listViewControlFullId, userid, userip, pagename) {
    $.ajax({
        type: "POST",
        url: "../../../AppServices/SaveAjaxService.asmx/SavePrintNaklInMap",
        data: ({
            printnaklinmap: $(listViewControlFullId + '_PrintNaklInMap_' + ticketId + '_ddlPrintNaklInMap_' + ticketId).val(),
            availableotherdocuments: $(listViewControlFullId + '_PrintNaklInMap_' + ticketId + '_ddlAvailableOtherDocuments_' + ticketId).val(),
            appkey: appkey,
            ticketid: ticketId,
            userid: userid,
            userip: userip,
            pagename: pagename
        }),
        success: function (response) {
            $(".lblStatus").show();
            $(".lblStatus").html("<span style='font-size: 10px; color: white;'>сохранено</span>");
            setTimeout(function () { $(".lblStatus").hide(); }, 3000); //очищаем только что присвоеный лейбл состояния ПН 
        },
        error: function (result) {
            $(".lblStatus").show();
            $(".lblStatus").html("<span style='font-size: 10px; color: red;'>ошибка</span>");
        }
    });
}

//Сохранение состояния дропдауна НН КОНЕЦ



//Сохранение состояния дропдауна ПН НАЧАЛО
function SavePrintNakl(ticketId, appkey, listViewControlFullId, userid, userip, pagename) {
    $.ajax({
        type: "POST",
        url: "../../../AppServices/SaveAjaxService.asmx/SavePrintNakl",
        data: ({
            printnakl: $(listViewControlFullId + '_PrintNakl_' + ticketId + "_ddlPrintNakl_" + ticketId).val(),
            appkey: appkey,
            ticketid: ticketId,
            userid: userid,
            userip: userip,
            pagename: pagename
        }),
        success: function(response) {
            $(listViewControlFullId + '_PrintNakl_' + ticketId + '_lblSavePrintNaklStatus_' + ticketId).html("<span style='font-size: 10px; color: green;'>ok</span>");
            setTimeout(function() { $(listViewControlFullId + '_PrintNakl_' + ticketId + '_lblSavePrintNaklStatus_' + ticketId).html(""); }, 3000); //очищаем только что присвоеный лейбл состояния ПН 
        },
        error: function(result) {
            $(listViewControlFullId + '_PrintNakl_' + ticketId + '_lblSavePrintNaklStatus_' + ticketId).html("<span style='font-size: 10px; color: red;'>error</span>");
        }
    });
}

//Сохранение состояния дропдауна ПН КОНЕЦ



//Сохранение состояния GruzobozCost НАЧАЛО
var oldValueGruzobozCost;

function SaveGruzobozCost(ticketId, appkey, listViewControlFullId, userid, userip, pagename) {
    if (oldValueGruzobozCost != $(listViewControlFullId + '_GruzobozCost_' + ticketId + '_tbGruzobozCost_' + ticketId).val()) {
        if (!$(listViewControlFullId + '_GruzobozCost_' + ticketId + '_tbGruzobozCost_' + ticketId).val().match(/\d+/g)) {
            $(listViewControlFullId + '_GruzobozCost_' + ticketId + '_lblSaveGruzobozCostStatus_' + ticketId).html("<span style='font-size: 10px; color: red;'>data error</span>");
        } else {
            $.ajax({
                type: "POST",
                url: "../../../AppServices/SaveAjaxService.asmx/SaveGruzobozCost",
                data: ({
                    gruzobozcost: $(listViewControlFullId + '_GruzobozCost_' + ticketId + '_tbGruzobozCost_' + ticketId).val(),
                    appkey: appkey,
                    ticketid: ticketId,
                    userid: userid,
                    userip: userip,
                    pagename: pagename
                }),
                success: function (response) {
                    $(listViewControlFullId + '_GruzobozCost_' + ticketId + '_lblSaveGruzobozCostStatus_' + ticketId).html("<span style='font-size: 10px; color: green;'>ok</span>");
                    setTimeout(function () { $(listViewControlFullId + '_GruzobozCost_' + ticketId + '_lblSaveGruzobozCostStatus_' + ticketId).html(""); }, 3000); //очищаем только что присвоеный лейбл состояния 
                },
                error: function (result) {
                    $(listViewControlFullId + '_GruzobozCost_' + ticketId + '_lblSaveGruzobozCostStatus_' + ticketId).html("<span style='font-size: 10px; color: red;'>error</span>");
                }
            });
        }
    }
}

function ClearLabelStatusGruzobozCost(ticketId, listViewControlFullId) {
    $(listViewControlFullId + '_GruzobozCost_' + ticketId + '_lblSaveGruzobozCostStatus_' + ticketId).html("");
    oldValueGruzobozCost = $(listViewControlFullId + '_GruzobozCost_' + ticketId + '_tbGruzobozCost_' + ticketId).val();
}
//Сохранение состояния GruzobozCost КОНЕЦ


//Сохранение состояния AgreedCost НАЧАЛО
var oldValueAgreedCost;

function SaveAgreedCost(ticketId, appkey, listViewControlFullId, userid, userip, pagename) {
    if (oldValueAgreedCost != $(listViewControlFullId + '_MoneyForMoneyView_' + ticketId + '_tbAgreedAssessedCosts_' + ticketId).val()) {
        if (!$(listViewControlFullId + '_MoneyForMoneyView_' + ticketId + '_tbAgreedAssessedCosts_' + ticketId).val().match(/\d+/g)) {
            $(listViewControlFullId + '_MoneyForMoneyView_' + ticketId + '_lblSaveAgreedCostStatus_' + ticketId).html("<span style='font-size: 10px; color: red;'>data error</span>");
        } else {
            $.ajax({
                type: "POST",
                url: "../../../AppServices/SaveAjaxService.asmx/SaveAgreedCost",
                data: ({
                    Agreedcost: $(listViewControlFullId + '_MoneyForMoneyView_' + ticketId + '_tbAgreedAssessedCosts_' + ticketId).val(),
                    appkey: appkey,
                    ticketid: ticketId,
                    userid: userid,
                    userip: userip,
                    pagename: pagename
                }),
                success: function (response) {
                    $(listViewControlFullId + '_MoneyForMoneyView_' + ticketId + '_lblSaveAgreedCostStatus_' + ticketId).html("<span style='font-size: 10px; color: green;'>ok</span>");
                    setTimeout(function () { $(listViewControlFullId + '_AgreedCost_' + ticketId + '_lblSaveAgreedCostStatus_' + ticketId).html(""); }, 3000); //очищаем только что присвоеный лейбл состояния 
                },
                error: function (result) {
                    $(listViewControlFullId + '_MoneyForMoneyView_' + ticketId + '_lblSaveAgreedCostStatus_' + ticketId).html("<span style='font-size: 10px; color: red;'>error</span>");
                }
            });
        }
    }
}

function ClearLabelStatusAgreedCost(ticketId, listViewControlFullId) {
    $(listViewControlFullId + '_MoneyForMoneyView_' + ticketId + '_lblSaveAgreedCostStatus_' + ticketId).html("");
    oldValueAgreedCost = $(listViewControlFullId + '_MoneyForMoneyView_' + ticketId + '_tbAgreedAssessedCosts_' + ticketId).val();
}
//Сохранение состояния AgreedCost КОНЕЦ

//Сохранение состояния ChangeTitle НАЧАЛО
var oldValueAgreedCost;

function SaveChangeTitle(ticketId, appkey, listViewControlFullId, userid, userip, pagename) {
    if (oldValueAgreedCost != $(listViewControlFullId + '_ChangeTitle_' + ticketId + '_tbChangeTitle_' + ticketId).val()) {
            $.ajax({
                type: "POST",
                url: "../../../AppServices/SaveAjaxService.asmx/SaveChangeTitle",
                data: ({
                    titleText: $(listViewControlFullId + '_ChangeTitle_' + ticketId + '_tbChangeTitle_' + ticketId).val(),
                    appkey: appkey,
                    ticketid: ticketId,
                    userid: userid,
                    userip: userip,
                    pagename: pagename
                }),
                success: function (response) {
                    $(listViewControlFullId + '_ChangeTitle_' + ticketId + '_lblChangeTitle_' + ticketId).html("<span style='font-size: 10px; color: green;'>ok</span>");
                    setTimeout(function () { $(listViewControlFullId + '_AgreedCost_' + ticketId + '_lblChangeTitle_' + ticketId).html(""); }, 3000); //очищаем только что присвоеный лейбл состояния 
                },
                error: function (result) {
                    $(listViewControlFullId + '_ChangeTitle_' + ticketId + '_lblChangeTitle_' + ticketId).html("<span style='font-size: 10px; color: red;'>error</span>");
                }
            });        
    }
}

function ClearLabelStatusChangeTitle(ticketId, listViewControlFullId) {
    $(listViewControlFullId + '_ChangeTitle_' + ticketId + '_lblChangeTitle_' + ticketId).html("");
    oldValueAgreedCost = $(listViewControlFullId + '_ChangeTitle_' + ticketId + '_tbChangeTitle_' + ticketId).val();
}
//Сохранение состояния ChangeTitle КОНЕЦ

//Сохранение состояния Comment НАЧАЛО
var oldValueComment;

function SaveComment(ticketId, appkey, listViewControlFullId, userid, userip, pagename) {
    if ($(listViewControlFullId + '_Comment_' + ticketId + '_tbComment_' + ticketId).val() != "") {
        $.ajax({
            type: "POST",
            url: "../../../AppServices/SaveAjaxService.asmx/SaveComment",
            data: ({
                additionalcomment: $(listViewControlFullId + '_Comment_' + ticketId + '_tbComment_' + ticketId).val(),
                appkey: appkey,
                ticketid: ticketId,
                userid: userid,
                userip: userip,
                pagename: pagename
            }),
            success: function (response) {
                $(listViewControlFullId + '_Comment_' + ticketId + '_lblSaveCommentStatus_' + ticketId).html("<span style='font-size: 10px; color: green;'>ok</span>");
                $(listViewControlFullId + '_Comment_' + ticketId + '_tbComment_' + ticketId).val("");
                LoadCommentHistory(appkey, ticketId, listViewControlFullId);
                setTimeout(function () { $(listViewControlFullId + '_Comment_' + ticketId + '_lblSaveCommentStatus_' + ticketId).html(""); }, 3000); //очищаем только что присвоеный лейбл состояния 
            },
            error: function (result) {
                $(listViewControlFullId + '_Comment_' + ticketId + '_lblSaveCommentStatus_' + ticketId).html("<span style='font-size: 10px; color: red;'>error</span>");
            }
        });
    }
}

function ClearLabelStatusComment(ticketId, listViewControlFullId) {
    $(listViewControlFullId + '_Comment_' + ticketId + '_lblSaveCommentStatus_' + ticketId).html("");
}
//Сохранение состояния Comment КОНЕЦ


//Сохранение состояния CommentClient НАЧАЛО
function SaveCommentClient(clientId, appkey, listViewControlFullId, userid, userip, pagename) {
    if ($(listViewControlFullId + '_Comment_' + clientId + '_tbComment_' + clientId).val() != "") {
        $.ajax({
            type: "POST",
            url: "../../../AppServices/SaveAjaxService.asmx/SaveCommentClient",
            data: ({
                additionalcomment: $(listViewControlFullId + '_Comment_' + clientId + '_tbComment_' + clientId).val(),
                appkey: appkey,
                clientid: clientId,
                userid: userid,
                userip: userip,
                pagename: pagename
            }),
            success: function (response) {
                $(listViewControlFullId + '_Comment_' + clientId + '_lblSaveCommentStatus_' + clientId).html("<span style='font-size: 10px; color: green;'>ok</span>");
                $(listViewControlFullId + '_Comment_' + clientId + '_tbComment_' + clientId).val("");
                LoadCommentClientHistory(appkey, clientId, listViewControlFullId);
                setTimeout(function () { $(listViewControlFullId + '_Comment_' + clientId + '_lblSaveCommentStatus_' + clientId).html(""); }, 3000); //очищаем только что присвоеный лейбл состояния 
            },
            error: function (result) {
                $(listViewControlFullId + '_Comment_' + clientId + '_lblSaveCommentStatus_' + clientId).html("<span style='font-size: 10px; color: red;'>error</span>");
            }
        });
    }
}

function ClearLabelStatusCommentClient(ticketId, listViewControlFullId) {
    $(listViewControlFullId + '_Comment_' + ticketId + '_lblSaveCommentStatus_' + ticketId).html("");
}
//Сохранение состояния CommentClient КОНЕЦ


//Сохранение состояния ContactDate НАЧАЛО
var oldValueContactDate;

function SaveContactDate(clientId, appkey, listViewControlFullId, userid, userip, pagename) {
    var date = $(listViewControlFullId + '_ContactDate_' + clientId + '_tbContactDate_' + clientId).val();
    if (oldValueContactDate !== date) {
        $.ajax({
            type: "POST",
            url: "../../../AppServices/SaveAjaxService.asmx/SaveContactDate",
            data: ({
                contactDate: date,
                appkey: appkey,
                clientId: clientId,
                userid: userid,
                userip: userip,
                pagename: pagename
            }),
            success: function (response) {
                $(listViewControlFullId + '_ContactDate_' + clientId + '_lblSaveContactDateStatus_' + clientId).html("<span style='font-size: 10px; color: green;'>ok</span>");
                setTimeout(function () { $(listViewControlFullId + '_ContactDate_' + clientId + '_lblSaveContactDateStatus_' + clientId).html(""); }, 3000); //очищаем только что присвоеный лейбл состояния 
            },
            error: function (result) {
                $(listViewControlFullId + '_ContactDate_' + clientId + '_lblSaveContactDateStatus_' + clientId).html("<span style='font-size: 10px; color: red;'>error</span>");
            }
        });
    }
}

function ClearLabelStatusContactDate(clientId, listViewControlFullId) {
    $(listViewControlFullId + '_ContactDate_' + clientId + '_lblSaveContactDateStatus_' + clientId).html("");
    oldValueContactDate = $(listViewControlFullId + '_ContactDate_' + clientId + '_tbContactDate_' + clientId).val();
}
//Сохранение состояния ContactDate КОНЕЦ

//Сохранение состояния Weight НАЧАЛО
var oldValueWeight;

function SaveWeight(ticketId, appkey, listViewControlFullId, userid, userip, pagename) {
    if (oldValueWeight !== $(listViewControlFullId + '_Weight_' + ticketId + '_tbWeight_' + ticketId).val()) {
        if (!$(listViewControlFullId + '_Weight_' + ticketId + '_tbWeight_' + ticketId).val().match(/\d+/g)) {
            $(listViewControlFullId + '_Weight_' + ticketId + '_lblSaveWeightStatus_' + ticketId).html("<span style='font-size: 10px; color: red;'>data error</span>");
        } else {
            $.ajax({
                type: "POST",
                url: "../../../AppServices/SaveAjaxService.asmx/SaveWeight",
                data: ({
                    weight: $(listViewControlFullId + '_Weight_' + ticketId + '_tbWeight_' + ticketId).val(),
                    appkey: appkey,
                    ticketid: ticketId,
                    userid: userid,
                    userip: userip,
                    pagename: pagename
                }),
                success: function (response) {
                    $(listViewControlFullId + '_Weight_' + ticketId + '_lblSaveWeightStatus_' + ticketId).html("<span style='font-size: 10px; color: green;'>ok</span>");
                    setTimeout(function () { $(listViewControlFullId + '_Weight_' + ticketId + '_lblSaveWeightStatus_' + ticketId).html(""); }, 3000); //очищаем только что присвоеный лейбл состояния 
                },
                error: function (result) {
                    $(listViewControlFullId + '_Weight_' + ticketId + '_lblSaveWeightStatus_' + ticketId).html("<span style='font-size: 10px; color: red;'>error</span>");
                }
            });
        }
    }
}

function ClearLabelStatusWeight(ticketId, listViewControlFullId) {
    $(listViewControlFullId + '_Weight_' + ticketId + '_lblSaveWeightStatus_' + ticketId).html("");
    oldValueWeight = $(listViewControlFullId + '_Weight_' + ticketId + '_tbWeight_' + ticketId).val();
}
//Сохранение состояния Weight КОНЕЦ



//Сохранение состояния KK НАЧАЛО
var oldValueWeight;

function SaveKK(ticketId, appkey, listViewControlFullId, userid, userip, pagename) {
    if (oldValueKK !== $(listViewControlFullId + '_KK_' + ticketId + '_tbKK_' + ticketId).val()) {
        if (!$(listViewControlFullId + '_KK_' + ticketId + '_tbKK_' + ticketId).val().match(/\d+/g)) {
            $(listViewControlFullId + '_KK_' + ticketId + '_lblSaveKKStatus_' + ticketId).html("<span style='font-size: 10px; color: red;'>data error</span>");
        } else {
            $.ajax({
                type: "POST",
                url: "../../../AppServices/SaveAjaxService.asmx/SaveKK",
                data: ({
                    kk: $(listViewControlFullId + '_KK_' + ticketId + '_tbKK_' + ticketId).val(),
                    appkey: appkey,
                    ticketid: ticketId,
                    userid: userid,
                    userip: userip,
                    pagename: pagename
                }),
                success: function (response) {
                    $(listViewControlFullId + '_KK_' + ticketId + '_lblSaveKKStatus_' + ticketId).html("<span style='font-size: 10px; color: green;'>ok</span>");
                    setTimeout(function () { $(listViewControlFullId + '_KK_' + ticketId + '_lblSaveKKStatus_' + ticketId).html(""); }, 3000); //очищаем только что присвоеный лейбл состояния 
                },
                error: function (result) {
                    $(listViewControlFullId + '_KK_' + ticketId + '_lblSaveKKStatus_' + ticketId).html("<span style='font-size: 10px; color: red;'>error</span>");
                }
            });
        }
    }
}

function ClearLabelStatusKK(ticketId, listViewControlFullId) {
    $(listViewControlFullId + '_KK_' + ticketId + '_lblSaveKKStatus_' + ticketId).html("");
    oldValueKK = $(listViewControlFullId + '_KK_' + ticketId + '_tbKK_' + ticketId).val();
}
//Сохранение состояния KK КОНЕЦ





//Сохранение состояния UserDiscount НАЧАЛО
var oldValueUserDiscount;

function SaveUserDiscount(userId, appkey, listViewControlFullId, curentuserid, curentuserip, pagename) {
    if (oldValueWeight != $(listViewControlFullId + '_UserDiscount_' + userId + '_tbUserDiscount_' + userId).val()) {
        if (!$(listViewControlFullId + '_UserDiscount_' + userId + '_tbUserDiscount_' + userId).val().match(/\d+/g)) {
            $(listViewControlFullId + '_UserDiscount_' + userId + '_lblSaveUserDiscountStatus_' + userId).html("<span style='font-size: 10px; color: red;'>data error</span>");
        } else {
            $.ajax({
                type: "POST",
                url: "../../../AppServices/SaveAjaxService.asmx/SaveUserDiscount",
                data: ({
                    userdiscount: $(listViewControlFullId + '_UserDiscount_' + userId + '_tbUserDiscount_' + userId).val(),
                    appkey: appkey,
                    userid: userId,
                    curentuserid: curentuserid,
                    curentuserip: curentuserip,
                    pagename: pagename
                }),
                success: function (response) {
                    $(listViewControlFullId + '_UserDiscount_' + userId + '_lblSaveUserDiscountStatus_' + userId).html("<span style='font-size: 10px; color: green;'>ok</span>");
                    setTimeout(function () { $(listViewControlFullId + '_UserDiscount_' + userId + '_lblSaveUserDiscountStatus_' + userId).html(""); }, 3000); //очищаем только что присвоеный лейбл состояния 
                },
                error: function (result) {
                    $(listViewControlFullId + '_UserDiscount_' + userId + '_lblSaveUserDiscountStatus_' + userId).html("<span style='font-size: 10px; color: red;'>error</span>");
                }
            });
        }
    }
}

function ClearLabelStatusUserDiscount(userId, listViewControlFullId) {
    $(listViewControlFullId + '_UserDiscount_' + userId + '_lblSaveUserDiscountStatus_' + userId).html("");
    oldValueWeight = $(listViewControlFullId + '_UserDiscount_' + userId + '_tbUserDiscount_' + userId).val();
}
//Сохранение состояния UserDiscount КОНЕЦ



//Сохранение состояния дропдаун MoneyStatuses НАЧАЛО
function SaveMoneyStatuses(ticketId, appkey, listViewControlFullId, userid, userip, pagename) {
    var result = true;
    $.ajax({
        type: "POST",
        url: "../../../AppServices/SaveAjaxService.asmx/SaveMoneyStatuses",
        data: ({
            statusid: $(listViewControlFullId + '_MoneyStatuses_' + ticketId + "_ddlMoneyStatuses_" + ticketId).val(),
            appkey: appkey,
            ticketid: ticketId,
            userid: userid,
            userip: userip,
            pagename: pagename
        }),
        success: function(response) {
            $('.lblStatus').html("<span style='font-size: 12px; color: white; font-weight: bold'>сохранено</span>");
            $('.lblStatus').show();
            setTimeout(function () { $('.lblStatus').hide(); }, 3000); //очищаем только что присвоеный лейбл состояния
        },
        error: function(res) {
            $('.lblStatus').html("<span style='font-size: 12px; color: red; font-weight: bold'>ошибка</span>");
            $('.lblStatus').show();
            result =  false;
        }
    });
    return result;
}
//Сохранение состояния дропдауна ПН КОНЕЦ



//Сохранение состояния чекбокса в приложении 2 НАЧАЛО
function SaveCheckboxPril2(ticketId, appkey, listViewControlFullId, userid, userip, pagename) {
    var checkbox = $(listViewControlFullId + '_SelectToPril2_' + ticketId + '_cbNotPrintInPril2_' + ticketId);
    var result = 0;
    if (checkbox.is(':checked')) {
        result = 1;
    }

    $.ajax({
        type: "POST",
        url: "../AppServices/SaveAjaxService.asmx/SaveCheckboxPril2",
        data: ({
            boolchecked: result,
            appkey: appkey,
            ticketid: ticketId,
            userid: userid,
            userip: userip,
            pagename: pagename
        }),
        success: function (response) {
            $(listViewControlFullId + '_SelectToPril2_' + ticketId + '_lblStatus_' + ticketId).html("<span style='font-size: 10px; color: green;'>ok</span>");
            setTimeout(function () { ClearLabelStatusPril2(ticketId, listViewControlFullId); }, 3000); //очищаем только что присвоеный лейбл состояния Акциз
        },
        error: function (result) {
            $(listViewControlFullId + '_SelectToPril2_' + ticketId + '_lblStatus_' + ticketId).html("<span style='font-size: 10px; color: red;'>err</span>");
        }
    });
}

function ClearLabelStatusPril2(ticketId, listViewControlFullId) {
    $(listViewControlFullId + '_SelectToPril2_' + ticketId + '_lblStatus_' + ticketId).html("");
}
//Сохранение состояния чекбокса в приложении 2 КОНЕЦ

//Сохранение состояния Weight в приложении 2 НАЧАЛО
var oldValuePril2Weight;

function SaveWeightToPril2(ticketId, appkey, listViewControlFullId, userid, userip, pagename) {
    
    var tbPril2Weight = $(listViewControlFullId + '_WeightToPril2_' + ticketId + '_tbPril2Weight_' + ticketId);
    var lblSavePril2WeightStatus = $(listViewControlFullId + '_WeightToPril2_' + ticketId + '_lblSavePril2WeightStatus_' + ticketId);
    var lblPril2Weight = $(listViewControlFullId + '_WeightToPril2_' + ticketId + '_lblPril2Weight_' + ticketId);
    
    if (oldValuePril2Weight != tbPril2Weight.val()) {
        if (!tbPril2Weight.val().match(/\d+/g)) {
            lblSavePril2WeightStatus.html("<span style='font-size: 10px; color: red;'>der</span>");
        } else {
            $.ajax({
                type: "POST",
                url: "../AppServices/SaveAjaxService.asmx/SaveWeightToPril2",
                data: ({
                    weight: tbPril2Weight.val(),
                    appkey: appkey,
                    ticketid: ticketId,
                    userid: userid,
                    userip: userip,
                    pagename: pagename
                }),
                success: function (response) {
                    lblSavePril2WeightStatus.html("<span style='font-size: 10px; color: green;'>ok</span>");
                    lblPril2Weight.show();
                    lblPril2Weight.html(tbPril2Weight.val());
                    tbPril2Weight.hide();
                    if (SummPril2Weight()) {
                        lblPril2Weight.css("color", "green");
                    } else {
                        lblPril2Weight.css("color", "red");
                    }
                    setTimeout(function() {
                        lblSavePril2WeightStatus.html("");
                    }, 3000); //очищаем только что присвоеный лейбл состояния 
                },
                error: function (result) {
                    lblSavePril2WeightStatus.html("<span style='font-size: 10px; color: red;'>er</span>");
                }
            });
        }
    }
}

function ClearLabelStatusWeightToPril2(ticketId, listViewControlFullId) {
    var tbPril2Weight = $(listViewControlFullId + '_WeightToPril2_' + ticketId + '_tbPril2Weight_' + ticketId);
    var lblSavePril2WeightStatus = $(listViewControlFullId + '_WeightToPril2_' + ticketId + '_lblSavePril2WeightStatus_' + ticketId);
    lblSavePril2WeightStatus.html("");
    oldValuePril2Weight = tbPril2Weight.val();
}

function ShowTbPril2Weight(ticketId, listViewControlFullId) {
    $(".weight-topril2").each(function () {
        $(this).hide();
        $(this).prev().show();
    }); //скрываем все открытые инпуты столбца
    var tbPril2Weight = $(listViewControlFullId + '_WeightToPril2_' + ticketId + '_tbPril2Weight_' + ticketId);
    var lblPril2Weight = $(listViewControlFullId + '_WeightToPril2_' + ticketId + '_lblPril2Weight_' + ticketId);
    tbPril2Weight.show();
    tbPril2Weight.val(lblPril2Weight.html());
    lblPril2Weight.hide();
}

function SummPril2Weight() {
    var summ = 0;
    $(".weight-topril2-tosumm").each(function () {
        summ += parseFloat($(this).html());
    });
    var result = parseFloat($(".weight-try-summ").html()) - summ;
    $(".weight-summ").html(result);
    if (result == 0) {
        $(".weight-summ").css("color", "green");
        return true;
    } else {
        $(".weight-summ").css("color", "red");
        return false;
    }
}
//Сохранение состояния Weight в приложении 2 КОНЕЦ


//Сохранение состояния BoxesNumber в приложении 2 НАЧАЛО
var oldValuePril2BoxesNumber;

function SaveBoxesNumberToPril2(ticketId, appkey, listViewControlFullId, userid, userip, pagename) {
    
    var tbPril2BoxesNumber = $(listViewControlFullId + '_BoxesNumberToPril2_' + ticketId + '_tbPril2BoxesNumber_' + ticketId);
    var lblSavePril2BoxesNumberStatus = $(listViewControlFullId + '_BoxesNumberToPril2_' + ticketId + '_lblSavePril2BoxesNumberStatus_' + ticketId);
    var lblPril2BoxesNumber = $(listViewControlFullId + '_BoxesNumberToPril2_' + ticketId + '_lblPril2BoxesNumber_' + ticketId);
    
    if (oldValuePril2BoxesNumber != tbPril2BoxesNumber.val()) {
        if (!tbPril2BoxesNumber.val().match(/\d+/g)) {
            lblSavePril2BoxesNumberStatus.html("<span style='font-size: 10px; color: red;'>der</span>");
        } else {
            $.ajax({
                type: "POST",
                url: "../AppServices/SaveAjaxService.asmx/SaveBoxesNumberToPril2",
                data: ({
                    boxesnumber: tbPril2BoxesNumber.val(),
                    appkey: appkey,
                    ticketid: ticketId,
                    userid: userid,
                    userip: userip,
                    pagename: pagename
                }),
                success: function(response) {
                    lblSavePril2BoxesNumberStatus.html("<span style='font-size: 10px; color: green;'>ok</span>");
                    lblPril2BoxesNumber.show();
                    lblPril2BoxesNumber.html(tbPril2BoxesNumber.val());
                    tbPril2BoxesNumber.hide();

                    if (SummPril2BoxesNumber()) {
                        lblPril2BoxesNumber.css("color", "green");
                    } else {
                        lblPril2BoxesNumber.css("color", "red");
                    }
                    
                    setTimeout(function() {
                        lblSavePril2BoxesNumberStatus.html(""); //сброс строки статуса сохранения
                    }, 3000); //очищаем только что присвоеный лейбл состояния 
                },
                error: function(result) {
                    lblSavePril2BoxesNumberStatus.html("<span style='font-size: 10px; color: red;'>er</span>");
                }
            });
        }
    }
}

function ClearLabelStatusPril2BoxesNumberToPril2(ticketId, listViewControlFullId) {
    var tbPril2BoxesNumber = $(listViewControlFullId + '_BoxesNumberToPril2_' + ticketId + '_tbPril2BoxesNumber_' + ticketId);
    var lblSavePril2BoxesNumberStatus = $(listViewControlFullId + '_BoxesNumberToPril2_' + ticketId + '_lblSavePril2BoxesNumberStatus_' + ticketId);
    lblSavePril2BoxesNumberStatus.html("");
    oldValuePril2BoxesNumber = tbPril2BoxesNumber.val();
}

function ShowTbPril2BoxesNumber(ticketId, listViewControlFullId) {
    $(".boxes-number-topril2").each(function() {
        $(this).hide();
        $(this).prev().show();
    }); //скрываем все открытые инпуты столбца
    var tbPril2BoxesNumber = $(listViewControlFullId + '_BoxesNumberToPril2_' + ticketId + '_tbPril2BoxesNumber_' + ticketId);
    var lblPril2BoxesNumber = $(listViewControlFullId + '_BoxesNumberToPril2_' + ticketId + '_lblPril2BoxesNumber_' + ticketId);
    tbPril2BoxesNumber.show();
    tbPril2BoxesNumber.val(lblPril2BoxesNumber.html());
    lblPril2BoxesNumber.hide();

    SummPril2BoxesNumber();
}

function SummPril2BoxesNumber() {
    var summ = 0;
    $(".boxes-number-topril2-tosumm").each(function () {
        summ += parseFloat($(this).html());
    });
    var result = parseFloat($(".boxes-number-try-summ").html()) - summ;
    $(".boxes-number-summ").html(result);
    if (result == 0) {
        $(".boxes-number-summ").css("color", "green");
        return true;
    } else {
        $(".boxes-number-summ").css("color", "red");
        return false;
    }
}
//Сохранение состояния BoxesNumber в приложении 2 КОНЕЦ

//Сохранение состояния чекбокса "дозвонились" НАЧАЛО
function SaveCheckboxPhoned(ticketId, appkey, listViewControlFullId, userid, userip, pagename) {
    var checkbox = $(listViewControlFullId + '_AdditionalOptions_' + ticketId + '_cbPhoned_' + ticketId);
    var result = 0;
    if (checkbox.is(':checked')) {
        result = 1;
    }

    $.ajax({
        type: "POST",
        url: "../../../AppServices/SaveAjaxService.asmx/SaveCheckboxPhonedValue",
        data: ({
            value: result,
            appkey: appkey,
            ticketid: ticketId,
            userid: userid,
            userip: userip,
            pagename: pagename
        }),
        success: function (response) {
            $('.lblStatus').html("<span style='font-size: 12px; color: white; font-weight: bold'>сохранено</span>");
            $('.lblStatus').show();
            setTimeout(function () { ClearLabelStatusPhoned(ticketId, listViewControlFullId); }, 3000); //очищаем только что присвоеный лейбл состояния Акциз
        },
        error: function (result) {
            $('.lblStatus').html("<span style='font-size: 12px; color: red; font-weight: bold'>ошибка</span>");
            $('.lblStatus').show();
        }
    });
}

function ClearLabelStatusPhoned() {
    $('.lblStatus').hide();
}
//Сохранение состояния чекбокса "дозвонились" КОНЕЦ


//Сохранение состояния чекбокса "проверено" НАЧАЛО
function SaveCheckboxCheckedOut(ticketId, appkey, listViewControlFullId, userid, userip, pagename) {
    var checkbox = $(listViewControlFullId + '_AdditionalOptions_' + ticketId + '_cbCheckedOut_' + ticketId);
    var result = 0;
    if (checkbox.is(':checked')) {
        result = 1;
    }

    $.ajax({
        type: "POST",
        url: "../../../AppServices/SaveAjaxService.asmx/SaveCheckboxCheckedOutValue",
        data: ({
            value: result,
            appkey: appkey,
            ticketid: ticketId,
            userid: userid,
            userip: userip,
            pagename: pagename
        }),
        success: function (response) {
            $('.lblStatus').html("<span style='font-size: 12px; color: white; font-weight: bold'>сохранено</span>");
            $('.lblStatus').show();
            setTimeout(function () { ClearLabelStatusCheckedOut(ticketId, listViewControlFullId); }, 3000); //очищаем только что присвоеный лейбл состояния Акциз
        },
        error: function (result) {
            $('.lblStatus').html("<span style='font-size: 12px; color: red; font-weight: bold'>ошибка</span>");
            $('.lblStatus').show();
        }
    });
}

function ClearLabelStatusCheckedOut() {
    $('.lblStatus').hide();
}
//Сохранение состояния чекбокса "проверено" КОНЕЦ



//Сохранение состояния чекбокса "счет выставлен" НАЧАЛО
function SaveCheckboxBilled(ticketId, appkey, listViewControlFullId, userid, userip, pagename) {
    var checkbox = $(listViewControlFullId + '_AdditionalOptions_' + ticketId + '_cbBilled_' + ticketId);
    var result = 0;
    if (checkbox.is(':checked')) {
        result = 1;
    }

    $.ajax({
        type: "POST",
        url: "../../../AppServices/SaveAjaxService.asmx/SaveCheckboxBilledValue",
        data: ({
            value: result,
            appkey: appkey,
            ticketid: ticketId,
            userid: userid,
            userip: userip,
            pagename: pagename
        }),
        success: function (response) {
            $('.lblStatus').html("<span style='font-size: 12px; color: white; font-weight: bold'>сохранено</span>");
            $('.lblStatus').show();
            setTimeout(function () { ClearLabelStatusBilled(ticketId, listViewControlFullId); }, 3000); //очищаем только что присвоеный лейбл состояния Акциз
        },
        error: function (result) {
            $('.lblStatus').html("<span style='font-size: 12px; color: red; font-weight: bold'>ошибка</span>");
            $('.lblStatus').show();
        }
    });
}

function ClearLabelStatusBilled() {
    $('.lblStatus').hide();
}
//Сохранение состояния чекбокса "счет выставлен" КОНЕЦ



//Сохранение состояния поля "доставка с" НАЧАЛО
function SaveDeliveryDateFrom(cityRowId, appkey, listViewControlFullId, userid, userip, pagename) {
    var date = $(listViewControlFullId + '_DeliveryFromToTime_' + cityRowId + '_tbOvdFrom_' + cityRowId).val();
    var ticketIdList = $(listViewControlFullId + '_hfTicketIdList_' + cityRowId).val();

    $.ajax({
        type: "POST",
        url: "../../../AppServices/SaveAjaxService.asmx/SaveDeliveryDateFrom",
        data: ({
            value: date,
            appkey: appkey,
            ticketidlist: ticketIdList,
            userid: userid,
            userip: userip,
            pagename: pagename
        }),
        success: function (response) {
            $('.lblStatus').html("<span style='font-size: 12px; color: white; font-weight: bold'>сохранено</span>");
            $('.lblStatus').show();
            setTimeout(function () { ClearLabelStatusDeliveryDateFrom(); }, 3000); //очищаем только что присвоеный лейбл состояния Акциз
        },
        error: function (result) {
            $('.lblStatus').html("<span style='font-size: 12px; color: red; font-weight: bold'>ошибка</span>");
            $('.lblStatus').show();
        }
    });
}

function ClearLabelStatusDeliveryDateFrom() {
    $('.lblStatus').hide();
}
//Сохранение состояния поля "доставка с"  КОНЕЦ

//Сохранение состояния поля "доставка по" НАЧАЛО
function SaveDeliveryDateTo(cityRowId, appkey, listViewControlFullId, userid, userip, pagename) {
    var date = $(listViewControlFullId + '_DeliveryFromToTime_' + cityRowId + '_tbOvdTo_' + cityRowId).val();
    var ticketIdList = $(listViewControlFullId + '_hfTicketIdList_' + cityRowId).val();

    $.ajax({
        type: "POST",
        url: "../../../AppServices/SaveAjaxService.asmx/SaveDeliveryDateTo",
        data: ({
            value: date,
            appkey: appkey,
            ticketidlist: ticketIdList,
            userid: userid,
            userip: userip,
            pagename: pagename
        }),
        success: function (response) {
            $('.lblStatus').html("<span style='font-size: 12px; color: white; font-weight: bold'>сохранено</span>");
            $('.lblStatus').show();
            setTimeout(function () { ClearLabelStatusDeliveryDateFrom(); }, 3000); //очищаем только что присвоеный лейбл состояния Акциз
        },
        error: function (result) {
            $('.lblStatus').html("<span style='font-size: 12px; color: red; font-weight: bold'>ошибка</span>");
            $('.lblStatus').show();
        }
    });
}

function ClearLabelStatusDeliveryDateFrom() {
    $('.lblStatus').hide();
}
//Сохранение состояния поля "доставка по"  КОНЕЦ

//Сохранение заявок при нажатии "доставлено" в городах НАЧАЛО
function SaveDeliveredForCity(cityRowId, appkey, listViewControlFullId, userid, userip, pagename) {
    var ticketIdList = $(listViewControlFullId + '_hfTicketIdList_' + cityRowId).val();

    $.ajax({
        type: "POST",
        url: "../../../AppServices/SaveAjaxService.asmx/SaveDeliveredForCity",
        data: ({
            appkey: appkey,
            ticketidlist: ticketIdList,
            userid: userid,
            userip: userip,
            pagename: pagename
        }),
        success: function (response) {
            $('.lblStatus').html("<span style='font-size: 12px; color: white; font-weight: bold'>сохранено</span>");
            $('.lblStatus').show();
            setTimeout(function () { ClearLabelStatusDeliveredForCity(); }, 3000); //очищаем только что присвоеный лейбл состояния Акциз
        },
        error: function (result) {
            $('.lblStatus').html("<span style='font-size: 12px; color: red; font-weight: bold'>ошибка</span>");
            $('.lblStatus').show();
        }
    });
}

function ClearLabelStatusDeliveredForCity() {
    $('.lblStatus').hide();
}
//Сохранение заявок при нажатии "доставлено" в городах  КОНЕЦ

//Сохранение очередности городов в городах НАЧАЛО
function SaveCityOrder(appkey, listViewControlFullId, userid, userip, pagename, cityId, driverId, cityRowId) {
    var orderVal = $(listViewControlFullId + '_CityOrder_' + cityRowId + '_tbCityOrder_' + cityRowId).val();
    $.ajax({
        type: "POST",
        url: "../../../AppServices/SaveAjaxService.asmx/SaveCityOrder",
        data: ({
            appkey: appkey,
            cityid: cityId,
            driverid: driverId,
            order: orderVal,
            userid: userid,
            userip: userip,
            pagename: pagename
        }),
        success: function (response) {
            $('.lblStatus').html("<span style='font-size: 12px; color: white; font-weight: bold'>сохранено</span>");
            $('.lblStatus').show();
            setTimeout(function () { ClearLabelStatusCityOrder(); }, 3000); //очищаем только что присвоеный лейбл состояния Акциз
        },
        error: function (result) {
            $('.lblStatus').html("<span style='font-size: 12px; color: red; font-weight: bold'>ошибка</span>");
            $('.lblStatus').show();
        }
    });
}

function ClearLabelStatusCityOrder() {
    $('.lblStatus').hide();
}
//Сохранение очередности городов в городах  КОНЕЦ