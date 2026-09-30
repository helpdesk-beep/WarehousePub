function CharOnly(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    if ((charCode >= 65 && charCode <= 90) || (charCode >= 97 && charCode <= 122)) {

        return true;
    }

    else if (charCode == 8) { return true; }
    else { return false; }
}
function CharspaceOnly(evt) {

    var charCode = (evt.which) ? evt.which : event.keyCode;

    if ((charCode >= 65 && charCode <= 90) || (charCode >= 97 && charCode <= 122)) {

        return true;
    }
    if (charCode == 8) { return true; }
    if (charCode == 32) { return true; }

    else { return false; }
}
function NumberOnly(evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if ((charCode >= 48 && charCode <= 57)) {

        return true;
    }
    if (charCode == 9) { return true; }
    if (charCode == 8) { return true; }
    else { return false; }
}
function CharNumOnly(e) {
    var k;
    document.all ? k = e.keyCode : k = e.which;

    if ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 8 || k == 32) { return true; }
    else if ((k >= 48 && k <= 57)) { return true; }
    //            if (e.keyCode == 46) { return true; }
    //            if (e.keyCode == 37) { return true; }
    //            if (e.keyCode == 39) { return true; }
    if (e.keyCode == 8) { return true; }
    //else if (k != 46) { return true; }
    else { return false; }

}
function CharNumEmailOnly(evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if ((charCode >= 65 && charCode <= 90) || (charCode >= 97 && charCode <= 122)) {

        return true;
    }
    if ((charCode >= 48 && charCode <= 57)) {

        return true;
    }
    if (charCode == 8) { return true; }
    if (charCode == 64) { return true; }
    if (charCode == 95) { return true; }
    if (charCode == 45) { return true; }
    if (charCode == 46) { return true; }
    else { return false; }

}
function CharNumAddressOnly(evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if ((charCode >= 65 && charCode <= 90) || (charCode >= 97 && charCode <= 122)) {

        return true;
    }
    if ((charCode >= 48 && charCode <= 57)) {

        return true;
    }
    if (charCode == 8) { return true; }
    if (charCode == 46) { return true; }
    if (charCode == 45) { return true; }
    if (charCode == 32) { return true; }
    else { return false; }

}
function CharNumSlashOnly(evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode;

    if ((charCode >= 65 && charCode <= 90) || (charCode >= 97 && charCode <= 122)) {

        return true;
    }
    if ((charCode >= 48 && charCode <= 57)) {

        return true;
    }
    if (charCode == 8) { return true; }
    if (charCode == 47) { return true; }
    if (charCode == 92) { return true; }

    else { return false; }

}

$(document).ready(function () {
    $.fn.HideDate = function () {
        $(this).parent("div").hide();
    }
    $.fn.ShowDate = function () {
        $(this).parent("div").show();
    }


    $.validator.addMethod("passwd_policy", function (value, element, param) {
        return this.optional(element)
            || (/.[!,@,#,$,%,^,&,*,?,_,~]/.test(value)
               );
    }, "Password must contain at least one special letter(as '*')");
    $.validator.addMethod("selectNone", function (value, element) {
        return this.optional(element) || element.value != 0;
    }, "Please select an option.");
    $.validator.addMethod("selectNoneBool", function (value, element) {
        return this.optional(element) || element.value != -1;
    }, "Please select an option.");
    jQuery.validator.addMethod("dateITA", function (value, element) {
        var check = false;
        var re = /^\d{1,2}\/\d{1,2}\/\d{4}$/;
        //alert(value);
        if (re.test(value)) {
            var adata = value.split('/');
            var mm = parseInt(adata[1], 10); // was gg (giorno / day)
            var dd = parseInt(adata[0], 10); // was mm (mese / month)
            var yyyy = parseInt(adata[2], 10); // was aaaa (anno / year)
            var xdata = new Date(yyyy, mm - 1, dd);
            if ((xdata.getFullYear() == yyyy) && (xdata.getMonth() == mm - 1) && (xdata.getDate() == dd))
                check = true;
            else
                check = false;
        } else
            check = false;
        return this.optional(element) || check;
    }, "Please specify the date in DD/MM/YYYY format");
    $.validator.addMethod("noSpecialChars", function (value, element) {
        return this.optional(element) || /^[a-z0-9\,-]+$/i.test(value);
    }, "Invalid characters found in your data");

    $.validator.addClassRules({
        vd_no_special_chars: {
            noSpecialChars: true
        },
        vd_DDL_required: {
            selectNone: true
        },
        vd_DDL_required_Bool: {
            selectNoneBool: true
        },
        vd_Required: {
            required: true
        },
        vd_Numbers: {
            number: true
        },
        vd_Digits: {
            digits: true
        },
        vd_PinCode: {
            digits: true,
            minlength: 6,
            maxlength: 6
        },
        vd_Month: {
            digits: true,
            range: [1, 12],
            minlength: 1,
            maxlength: 2
        },
        vd_TuitionFee: {
            digits: true,
            range: [1, 1200000],
            minlength: 1,
            maxlength: 7
        },
        vd_OtherFee: {
            digits: true,
            range: [0, 1500000],
            minlength: 1,
            maxlength: 7
        },
        vd_Aadhar_Card: {
            digits: true,
            minlength: 12,
            maxlength: 12
        },
        vd_Annual_Income: {
            digits: true,
            range: [999, 9999999999],
            minlength: 3,
            maxlength: 10
        },
        vd_MobileNo: {
            digits: true,
            minlength: 10,
            maxlength: 10
        },
        vd_Date: {
            dateITA: true
        },
        vd_Age: {
            digits: true,
            range: [0, 99],
            maxlength: 2
        },
        vd_Bank_Account: {
            minlength: 4,
            maxlength: 26
        },
        vd_Email: {
            email: true
        },
        vd_Percentage: {
            number: true,
            range: [0.01, 99.99]
        },
        vd_Longitude: {
            number: true,
            range: [0.01, 999.99]
        },
        vd_Password: {
            passwd_policy: true
        },
        vd_Images: {
            accept: 'jpeg|jpg'
        },
        vd_Disability_Percentage: {
            digits: true,
            range: [1, 99]
        }
    });
    $.validator.setDefaults({
        onsubmit: false,
        errorPlacement: function (error, element) {
            $("#ei" + element.attr("id")).remove();
            $('<img src="http://cdn.nic.in/common/images/alert.png" id="ei'
					+ element.attr("id")
					+ '" class="tooltips" title="'
					+ error.html() + '" />').insertAfter(element);
            $("#ei" + element.attr("id")).tooltip({
                track: true,
                delay: 0,
                showURL: false,
                showBody: " - ",
                fade: 100
            });
        },
        errorClass: "help-inline",
        errorElement: "span",
        highlight: function (element, errorClass, validClass) {
            $(element).parents('.control-group').removeClass('success');
            $(element).parents('.control-group').addClass('error');
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).parents('.control-group').removeClass('error');
            $(element).parents('.control-group').addClass('success');
            $(element).removeClass(errorClass).addClass(validClass);
            $(element.form).find("label[for=" + element.id + "]")
					.removeClass(errorClass);
            $("#ei" + element.id).remove();
        }

    });
    $("#aspnetForm").validate();
    //////////////////////////////////////////////////////////////
});