var Global;
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

    $.validator.addMethod("Letters", function (value, element, param) {
        return this.optional(element) || (/^[a-zA-Z\b]+$/.test(value));
    }, "Please enter only Letters");

    $.validator.addMethod("regex", function (value, element, regexp) {
        var check = false;
        return this.optional(element) || regexp.test(value);
    }, "Please check the value you have entered");

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
    }, "Please specify the date in (DD/MM/YYYY) format");

    $.validator.addMethod("noSpecialChars", function (value, element) {
        return this.optional(element) || /^[a-z0-9\,-]+$/i.test(value);
    }, "Special Characters are not allowed (~,!,@,#,$,%,^,&)");

    $.validator.addMethod("Amount", function (value, element) {
        return this.optional(element) || element.value > 100;
    }, "it should be more than Rs. 100");

    $.validator.addMethod("WorkingDays", function (value, element) {
        return this.optional(element) || element.value > 0 && element.value < 28;
    }, "Working Days should be between 1 and 27");

    $.validator.addMethod("NumberNotZero", function (value, element) {
        return this.optional(element) || element.value > 0;
    }, "0 is not allowed");

    $.validator.addMethod("Mobile", function (value, element, param) {
        return this.optional(element)
            || (/^[7-9][0-9]{9}$/.test(value));
    }, "Please enter valid Mobile number");

    $.validator.addMethod("DOB", function (value, element, param) {
        return this.optional(element)
            || (/^(0?[1-9]|[12][0-9]|3[01])[\/](0?[1-9]|1[012])[\/]\d{4}$/.test(value));
    }, "Please enter Valid Date,it should be dd/mm/yyyy");



    $.validator.addMethod("IFSCCode", function (value, element, param) {
        return this.optional(element)
            || (/^[A-Z|a-z]{4}[0][\d]{6}$/.test(value));
    }, "Please enter Valid IFSC Code");

    $.validator.addMethod("DateTime24", function (value, element, param) {
        return this.optional(element)
            || (/(0[1-9]|1[0-2])\/((0[1-9]|[12][0-9])|3[01])\/(19[5-9][0-9]|2[0-9][0-9][0-9]) ([01][0-9]|2[0-3]):[0-5][0-9]/.test(value));
    }, "Please enter Valid Date and Time,it should be mm/dd/yyyy hh:mm (24 Hour Format)");

    $.validator.addClassRules({
        vd_no_special_chars: {
            noSpecialChars: true
        },
        vd_Mob: {
            Mobile: true
        },

        vd_NumberNotZero:
        {
            NumberNotZero: true
        },
        vd_WorkingDays:
        {
            WorkingDays: true
        },
        vd_DOB:
        {
            DOB: true
        },
        vd_IFSC:
        {
            IFSCCode: true
        },
        vd_DateTime:
        {
            DateTime24: true
        },
        vd_Address: {
            required: true,
            regex: /^[a-zA-z] ?([a-zA-z]|[a-zA-z] )*[a-zA-z]$/,
            minlength: 5
        },
        vd_WebURL:
        {
            regex: /^(www\.|https:\/\/www\.|http:\/\/|https:\/\/)[a-z0-9]+([\-\.]{1}[a-z0-9]+)*\.[a-z]{2,5}(:[0-9]{1,5})?(\/.*)?$/
        },
        vd_Name: {
            regex: /^[a-zA-Z ]*$/,
            minlength: 3
        },
        vd_Alphabets: {
            required: true,
            regex: /^[a-zA-Z ]*$/,
            minlength: 3
        },

        vd_Letters: {
            required: true,
            minlength: 3,
            Letters: true
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
        vd_Aadhar_Card: {
            digits: true,
            minlength: 12,
            maxlength: 12
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
        vd_Bank_Account_Dynamic: {
            BankAccountLength: true
        },
        vd_Email: {
            email: true
        },
        vd_House_No: {
            required: true,
            digits: true,
            range: [1, 6000],
            maxlength: 4
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
            accept: 'jpeg|jpg|png|gif|tiff'
        }
    });
    $.validator.setDefaults({
        onsubmit: false,
        errorPlacement: function (error, element) {
            $("#" + element.attr("id")).css('border', '2px solid #f53939');
            $("#" + element.attr("id")).css('title', error.html());
            //$('<img src="/Resources/Validation/Error.gif" id="ei' + element.attr("id") + '" class="tooltips" title="' + error.html() + '" />').insertAfter(element);
            $("#" + element.attr("id")).prop('title', error.html());
            /*   $("#" + element.attr("id")).tooltip*/
            $("#" + element.attr("id")).tooltip
            ({
                position: {
                    my: "center bottom-20",
                    at: "center top",
                    using: function (position, feedback) {
                        $(this).css(position);
                        $("<div>").addClass("arrow").addClass(feedback.vertical).addClass(feedback.horizontal).appendTo(this);
                    }
                },
                template: '<div class="tooltip error"><div class="arrow"></div><div class="tooltip-inner"></div></div>'
            });
        },
        errorClass: "help-inline",
        errorElement: "span",
        highlight: function (element, errorClass, validClass) {
            $(element).parents('.control-group').removeClass('success');
            $(element).parents('.control-group').addClass('error');
            $(element).parent().parent().find(".rqustar").addClass("blink_me");
            $(element).css('border', '1px solid #EA3131');
            $("#ei" + element.id).css('color', '#E45656');
            $(element).next().find("i").removeClass("fa fa-check");
            $(element).next().find("i").addClass("fa fa-remove");
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).parents('.control-group').removeClass('error');
            $(element).parents('.control-group').addClass('success');
            $(element).css('background', 'white');
            $(element).css('border', '1px solid #6EED6D');
            $(element).removeClass(errorClass).addClass(validClass);
            $(element.form).find("label[for=" + element.id + "]").removeClass(errorClass);
            $(element).parent().parent().find(".rqustar").removeClass("blink_me");
            $(element).next().find("i").removeClass("fa fa-remove");
            $(element).next().find("i").addClass("fa fa-check");
            //$("#ei" + element.id).remove();
            // $("#ei" + element.id).attr("src", "/Resources/Validation/success.png");
            $("#ei" + element.id).attr("title", "Accepted ✓");
            $("#ei" + element.id).css('border', '1px solid #6EED6D');
            $("#ei" + element.id).css('color', '#44D343');

        },
        success: function (element, errorClass, validClass) {

        }
    });
    $("#aspnetForm,form").validate();
    //////////////////////////////////////////////////////////////
});


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