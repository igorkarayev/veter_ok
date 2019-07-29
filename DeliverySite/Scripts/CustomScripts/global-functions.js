function confirmDelete() {
    if (confirm("Вы подтверждаете удаление?")) {
        return true;
    }
    else
    {
        return false;
    }
}

function CalculatorApiReturnValueToLocalString(moneyvalue) {
    switch (moneyvalue) {
        case "OutOfTitles":
            return "Введенного наименования у нас пока нет :( Уточнить стоимость доставки можно у наших менеджеров.";
        case "OutOfCity":
            return "Выберите город из списка!";
        case "ProfileNotSelected":
            return "Выберите профиль из списка!";
        case "DescriptionIsNull":
            return "Введите наименования всех грузов!";
        case "GoodsNumberIsNull":
            return "Введите количество штук всех грузов!";
        case "InnerException":
            return "Ошибка расчетов. Перепроверьте введенную информацию.";
        case "DeviationIsUnknown":
            return "Неизвестна доплата за отклонение. Уточняйте стоимость доставки у наших менеджеров.";
        case "NotAuthorized":
            return "Запрос не прошел серверную авторизацию. Обратитесь в тех. отдел компании";
        case "InternetMarketAssessedCostException":
            return "Невозможно просчитать оценочную стоимость груза. Для профиля 'Интернет-магазин' это является необходимым условием при расчете стоимости за доставку.";
        case "CityNotInicialized":
            return "Город доставки не задан!";
        case "TitleNotAvaliable":
            return "Рассчет для одного из наименований грузов не доступен для вашего аккаунта. Что бы открыть доступ к данному наименованию обратитесь к нашим менеджерам.";
        default:
            return GetMoneyString(parseFloat(moneyvalue.replace(",",".")));
    }
}

function GetMoneyString(num) {
    var floor = Math.floor(num);
    var dec = GetCoints(num);
    return floor + " руб. " + dec + " коп.";
}

function GetCoints(num) {
    var numb = num > 0 ? num - Math.floor(num) : Math.ceil(num) - num;
    return Math.round(parseFloat(numb) * 100);
}