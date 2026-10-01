<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="Insert_Images.aspx.cs" Inherits="Admin_News" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
       <h4>Add Current News</h4><hr />

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
            <b style="font-size: large; font-family: 'Times New Roman', Times, serif">Insert Image</b>
        </div>
        <div class="row">
            <div class="col-12">
                <asp:Label ID="lblErr" runat="server" Font-Bold="true"></asp:Label>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-2">
                Image Caption:
            </div>
            <div class="col-10">
                <textarea runat="server" id="txtCaption" style="width: 100%;  min-height:100px;"></textarea>
                <asp:RequiredFieldValidator ID="rvCaption" runat="server"
                    ControlToValidate="txtCaption" ErrorMessage="Please Insert Caption" ForeColor="#CC3300"
                    ValidationGroup="myValidator"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="row">
            <div class="col-2">
                Date:
            </div>
            <div class="col-10">

                <input runat="server" type="text" id="datepicker" maxlength="10" />
                <asp:RequiredFieldValidator ID="rvDatepicker" color="red" runat="server"
                    ControlToValidate="datepicker" ErrorMessage="Please Insert Date" ForeColor="#CC3300"
                    ValidationGroup="myValidator"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="row">
            <div class="col-2">
                Upload Your Image:
            </div>
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
                <asp:CheckBox ID="cbImage" CssClass="mobilesubtitle" runat="server" Text="'    Is news belong to Chattisgarh? Check If Yes!!!" AutoPostBack="True"></asp:CheckBox>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-2"></div>
            <div class="col-10">
                <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Upload" ValidationGroup="myValidator" />
            </div>
        </div>
    </div>
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contbootm" Runat="Server">
     <script type="text/javascript" src="<%= ResolveUrl("~/NEW_CSS/js/jquery-1.12.4.js") %>"></script>
    <script type="text/javascript">
        $(function () {
            $('[id*=txtTitle]').keydown(function (e) {
                if (e.shiftKey || e.ctrlKey || e.altKey) {
                    e.preventDefault();
                } else {
                    var key = e.keyCode;
                    if (!((key == 8) || (key == 32) || (key == 46) || (key >= 35 && key <= 40) || (key >= 65 && key <= 90) || (key >= 48 && key <= 57) || (key >= 96 && key <= 105))) {
                        e.preventDefault();
                    }
                }
            });
        });
        $(function () {
            $('[id*=txtDescription]').keydown(function (e) {
                if (e.shiftKey || e.ctrlKey || e.altKey) {
                    e.preventDefault();
                } else {
                    var key = e.keyCode;
                    if (!((key == 8) || (key == 32) || (key == 46) || (key >= 35 && key <= 40) || (key >= 65 && key <= 90) || (key >= 48 && key <= 57) || (key >= 96 && key <= 105))) {
                        e.preventDefault();
                    }
                }
            });
        });
    </script>
</asp:Content>

