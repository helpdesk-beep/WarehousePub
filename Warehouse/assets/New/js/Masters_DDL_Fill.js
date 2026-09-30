//------------------Fill Employee Dropdown Method-------------------------//
function Branch(DropdownObject, DID, valueToBeSelect) {
    if (valueToBeSelect == undefined) {
        valueToBeSelect = 0;
    }
    if (DID == undefined) {
        DID = 0;
    }
    var HandlerUrl = "/Handlers/Branch.ashx?DID=" + DID;
    FillDropdown(HandlerUrl, DropdownObject, valueToBeSelect);
}

//-------------------Common Base Method For reusability---------------------------------------------------//
function FillDropdown(HandlerUrl, DropdownObject, valueToBeSelect, IsAll) {
    if (HandlerUrl == undefined) {
        return;
    }
    $("#dvLoading").show();
    DropdownObject.empty();
    DropdownObject.append("<option value='0'>Loading...</option>");
    $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        url: HandlerUrl,
        dataType: "json",
        async: false,
        error: function (data, textStatus) {
        },
        success: function (data) {
            var ins = data;
            DropdownObject.empty();
            if (IsAll == 1)
                DropdownObject.append("<option value='0'>- Select All -</option>");
            else
                DropdownObject.append("<option value='0'>- Select -</option>");
            $.each(ins.rows, function (i, item) {
                DropdownObject.append('<option  value="' + item[0] + '"> ' + item[1] + '</option>');
            });
        },
        complete: function () {
            $("#dvLoading").hide();
            DropdownObject.val(valueToBeSelect);
        }
    });
}
//------------------------------------------------------------------------------------------------------------//