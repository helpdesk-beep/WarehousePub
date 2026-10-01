<%@ Page Title="" Language="C#" ViewStateEncryptionMode="Always" MasterPageFile="../MasterPages/RMMasters.master" Debug="true" AutoEventWireup="true" CodeFile="InsertEmployee.aspx.cs" Inherits="Admin_InsertEmployee" %>


<asp:Content ID="Content1" ContentPlaceHolderID="PM" runat="Server">
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
    <script src="../NEW_CSS/js/jquery-1.12.4.js"></script>
    <script src="../NEW_CSS/js/jquery-ui.js"></script>
    <script src="<%= ResolveUrl("~/NEW_CSS/js/jquery-ui.js") %>"></script>

    <script type="text/javascript">
        $(function () {
            $('#<%=datepicker.ClientID%>').datepicker({
                dateFormat: 'dd/mm/yy',
                defaultDate: -1,
                minDate: new Date("01/23/2018"),
                maxDate: 0
            });
        });

        $(function () {
            $('#<%=btnSave.ClientID%>').bind('click', function () {
                var txtVal = $('#<%=datepicker.ClientID%>').val();

                if (isDate(txtVal))
                    return true;
                else
                    alert('Invalid Date');
            });

            function isDate(datepicker) {
                var currVal = datepicker;
                if (currVal == '')
                    return false;

                var rxDatePattern = /^(\d{1,2})(\/|-)(\d{1,2})(\/|-)(\d{4})$/; //Declare Regex
                var dtArray = currVal.match(rxDatePattern); // is format OK?

                if (dtArray == null)
                    return false;

                //Checks for mm/dd/yyyy format.
                dtDay = dtArray[1];
                dtMonth = dtArray[3];
                dtYear = dtArray[5];

                if (dtMonth < 1 || dtMonth > 12)
                    return false;
                else if (dtDay < 1 || dtDay > 31)
                    return false;
                else if ((dtMonth == 4 || dtMonth == 6 || dtMonth == 9 || dtMonth == 11) && dtDay == 31)
                    return false;
                else if (dtMonth == 2) {
                    var isleap = (dtYear % 4 == 0 && (dtYear % 100 != 0 || dtYear % 400 == 0));
                    if (dtDay > 29 || (dtDay == 29 && !isleap))
                        return false;
                }
                return true;
            }
        });

    </script>
    <style>
        .row {
            margin-left: 0px !important;
            margin-right: 0px !important;
        }

        span.mobilesubtitle {
            display: table;
        }

            span.mobilesubtitle > input {
                display: table-cell;
            }

            span.mobilesubtitle > label {
                display: table-cell;
                vertical-align: top;
            }
    </style>
    <div class="container">
        <div class="alert-warning img-thumbnail" style="margin-bottom: 10px!important;">
            <b style="font-size: large; font-family: 'Times New Roman', Times, serif">Insert Employee Details</b>
        </div>
        <div class="row">
            <div class="col-12">
                <asp:Label ID="lblErr" runat="server" Font-Bold="true"></asp:Label>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-2">
                District:
            </div>
            <div class="col-4">
                <asp:DropDownList ID="ddldistrict" CssClass="form-control" Height="32px" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged">
                    <asp:ListItem Value="0">Select</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-2">
                Branch:
            </div>
            <div class="col-4">
                <asp:DropDownList ID="ddlbranch" CssClass="form-control" Height="32px" AutoPostBack="false" runat="server">
                            <asp:ListItem Value="0">Select</asp:ListItem>
                        </asp:DropDownList>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-2">
                Name in Hindi:
            </div>
            <div class="col-4">
                <input type="text" runat="server" id="txtNameH" style="width: 100%" />
            </div>
            <div class="col-2">
                Name in English:
            </div>
            <div class="col-4">
                <input type="text" runat="server" id="txtNameE" style="width: 100%" />
            </div>
        </div>
        <br />
        <br />
        <div class="row">
            <div class="col-2">
                Date of Birth:
            </div>
            <div class="col-4">
                <input runat="server" type="text" id="datepicker" maxlength="10" />
                <asp:RequiredFieldValidator ID="rvDatepicker" color="red" runat="server"
                    ControlToValidate="datepicker" ErrorMessage="Please Insert Date" ForeColor="#CC3300"
                    ValidationGroup="myValidator"></asp:RequiredFieldValidator>
            </div>
            <div class="col-2">
                Designation:
            </div>
            <div class="col-4">
                <input type="text" runat="server" id="txtDesignation" style="width: 100%" />
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-2">
                Mobile:
            </div>
            <div class="col-4">
                <input type="text" runat="server" id="txtMobile" />
            </div>
            <div class="col-2">
                Email:
            </div>
            <div class="col-4">
                <input type="text" runat="server" id="txtEmail" style="width: 100%" />
            </div>
        </div>
        <br />
        <br />
        <div class="row">
            <div class="col-2">
                Date of Joining:
            </div>
            <div class="col-4">
                <input runat="server" type="text" id="txtDOJ" maxlength="10" />
            </div>
            <div class="col-2">
                Employee Photo:
            </div>
            <div class="col-4">
                <asp:FileUpload ID="FileUpload1" runat="server" />
                <asp:RequiredFieldValidator ID="rvFileUpload1" runat="server"
                    ControlToValidate="FileUpload1" ErrorMessage="Please Insert File" ForeColor="#CC3300"
                    ValidationGroup="myValidator"></asp:RequiredFieldValidator>
            </div>
            <%--  <div class="col-lg-3"><span id="Label345348" class="required" style="display: inline-block; width: 200px;">Photo Preview: </span></div>
            <div class="col-lg-3">
                <asp:Image ID="Image1_vID" runat="server" Height="160px" Width="180px" />
            </div>--%>
        </div>
        <br />
        <div class="row">
        </div>
        <br />
        <div class="row">
            <div class="col-12">
                <center>
                    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Submit" ValidationGroup="myValidator" /></center>
            </div>
        </div>
    </div>
</asp:Content>

