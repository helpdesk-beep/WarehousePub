<%@ Page Title="" Language="C#" ViewStateEncryptionMode="Always" MasterPageFile="../MasterPages/adminMaster.master" AutoEventWireup="true" CodeFile="InsertNews.aspx.cs" Inherits="Admin_InsertNews" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PM" runat="Server">
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
    <script src="../NEW_CSS/js/jquery-1.12.4.js"></script>
    <script src="../NEW_CSS/js/jquery-ui.js"></script>
    <script src="<%= ResolveUrl("~/NEW_CSS/js/jquery-ui.js") %>"></script>
    <style>
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

        .row {
            margin-left: 0px !important;
            margin-right: 0px !important;
        }
    </style>
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
    <div class="container">
        <div class="alert-warning img-thumbnail" style="margin-bottom: 10px!important;">
            <b style="font-size: large; font-family: 'Times New Roman', Times, serif">Insert News</b>
        </div>
        <div class="row">
            <div class="col-12">
                <asp:Label ID="lblErr" runat="server" Font-Bold="true"></asp:Label>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-2">
                News Title:
            </div>
            <div class="col-10">
                <textarea runat="server" id="txtTitle" style="width: 100%; min-height: 100px;"></textarea>
                <asp:RequiredFieldValidator ID="rvTitle" runat="server"
                    ControlToValidate="txtTitle" ErrorMessage="Please Insert Title" ForeColor="#CC3300"
                    ValidationGroup="myValidator"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="row">
            <div class="col-2">News Date:</div>
            <div class="col-10">
                <input runat="server" type="text" id="datepicker" maxlength="10" />
                <asp:RequiredFieldValidator ID="rvDatepicker" runat="server"
                    ControlToValidate="datepicker" ErrorMessage="Please Insert Date" ForeColor="#CC3300"
                    ValidationGroup="myValidator"></asp:RequiredFieldValidator>

            </div>
        </div>
        <div class="row">
            <div class="col-2">News Attachment:</div>
            <div class="col-10">
                <asp:FileUpload ID="FileUpload1" runat="server" />
                <asp:RequiredFieldValidator ID="rvFileUpload1" runat="server"
                    ControlToValidate="FileUpload1" ErrorMessage="Please Insert File" ForeColor="#CC3300"
                    ValidationGroup="myValidator"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="row">
            <div class="col-2"></div>
            <div class="col-10">
                <asp:CheckBox ID="cbNews" CssClass="mobilesubtitle" runat="server" Text="'    Is news belong to Chattisgarh? Check If Yes!!!" AutoPostBack="True"></asp:CheckBox></td>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-2"></div>
            <div class="col-10">
                <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Upload"
                    ValidationGroup="myValidator" />
            </div>
        </div>
    </div>
</asp:Content>

