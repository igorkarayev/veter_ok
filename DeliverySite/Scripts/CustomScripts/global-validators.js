function validateChangeColor(sender, isValid) {
    var object = sender.controltovalidate;
    if (!isValid) {
        $("#" + object).css("background-color", "#ffdda8");
    } else {
        $("#" + object).css("background-color", "#ffffff");
    }
}

function validateIfEmpty(sender, args) {
    var isValid;
    if (args.Value.length == 0) {
        isValid = false;
    } else {
        isValid = true;
    }
    validateChangeColor(sender, isValid);
    args.IsValid = isValid;
}

function validateIfEmptyAndEmail(sender, args) {
    var isValid;
    var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    if (args.Value.length == 0 || !re.test(args.Value)) {
        isValid = false;
    } else {
        isValid = true;
    }
    validateChangeColor(sender, isValid);
    args.IsValid = isValid;
}

function validateIfEmail(sender, args) {
    var isValid;
    var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    if (re.test(args.Value)) {
        isValid = true;
    } else {
        if (args.Value.length == 0) {
            isValid = true;
        } else {
            isValid = false;
        }
    }
    validateChangeColor(sender, isValid);
    args.IsValid = isValid;
}

function validateNotEmptyNumber(sender, args) {
    var isValid;
    if (args.Value.length == 0 || !args.Value.match(/\d+/g)) {
        isValid = false;
    } else {
        isValid = true;
    }
    validateChangeColor(sender, isValid);
    args.IsValid = isValid;
}

function validateNotEmptyAndNotNullNumber(sender, args) {
    var isValid;
    if (args.Value.length == 0 || args.Value == 0 || !args.Value.match(/\d+/g)) {
        isValid = false;
    } else {
        isValid = true;
    }
    validateChangeColor(sender, isValid);
    args.IsValid = isValid;
}

function validatePasportSeria(sender, args) {
    var isValid;
    var myValue = args.Value;
    if (myValue == "" || myValue.length != 2 || myValue.match(/\d+/g)) {
        isValid = false;
        validateChangeColor(sender, isValid);
    } else {
        isValid = true;
        validateChangeColor(sender, isValid);
    }
    args.IsValid = isValid;
}

function validatePasportNumber(sender, args) {
    var isValid;
    var myValue = args.Value;
    if (myValue != "" && myValue.length == 7 && myValue.match(/^\d+$/)) {
        isValid = true;
        validateChangeColor(sender, isValid);
    } else {
        validateChangeColor(sender, isValid);
        isValid = false;
    }
    args.IsValid = isValid;
}

function validatePasportSeriaEmpty(sender, args) {
    var isValid;
    var myValue = args.Value;
    if (myValue != "") {
        if (myValue == "" || myValue.length != 2 || myValue.match(/\d+/g)) {
            isValid = false;
            validateChangeColor(sender, isValid);
        } else {
            isValid = true;
            validateChangeColor(sender, isValid);
        }
    } else {
        isValid = true;
    }

    args.IsValid = isValid;
}

function validatePasportNumberEmpty(sender, args) {
    var isValid;
    var myValue = args.Value;
    if (myValue != "") {
        if (myValue.length == 7 && myValue.match(/^\d+$/)) {
            isValid = true;
            validateChangeColor(sender, isValid);
        } else {
            validateChangeColor(sender, isValid);
            isValid = false;
        }
    } else {
        isValid = true;
    }

    args.IsValid = isValid;
}