<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/RMMasters.master" AutoEventWireup="true" CodeFile="Employee_Details.aspx.cs" Inherits="Region_Employee_Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PM" runat="Server">

    <script src="<%= ResolveUrl("~/NEW_CSS/js/jquery-ui.js") %>"></script>
    <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />

    <script type="text/javascript" src="<%= ResolveUrl("~/NEW_CSS/js/jquery-1.12.4.js") %>"></script>
    <script type="text/javascript" src="<%= ResolveUrl("~/assets/js/bootstrap.min.js") %>"></script>
    <style type="text/css">
        .modal-dialog {
            width: 1000px;
            margin: 30px auto;
        }

        .btn-info {
            color: #fff;
            background-color: #5bc0de;
            border-color: #46b8da;
        }

        .btn {
            display: inline-block;
            padding: 6px 12px;
            margin-bottom: 0;
            font-size: 14px;
            font-weight: 400;
            line-height: 1.42857143;
            text-align: center;
            white-space: nowrap;
            vertical-align: middle;
            -ms-touch-action: manipulation;
            touch-action: manipulation;
            cursor: pointer;
            -webkit-user-select: none;
            -moz-user-select: none;
            -ms-user-select: none;
            user-select: none;
            background-image: none;
            border: 1px solid transparent;
            border-radius: 4px;
        }

        .btn-info:hover {
            color: black;
            background-color: #31b0d5;
            border-color: #269abc;
        }

        .btn.active, .btn:active {
            background-image: none;
            outline: 0;
            -webkit-box-shadow: inset 0 3px 5px rgba(0,0,0,.125);
            box-shadow: inset 0 3px 5px rgba(0,0,0,.125);
        }
    </style>
    <style type="text/css">
        .datepicker {
            font-size: 0.875em;
        }
            /* solution 2: the original datepicker use 20px so replace with the following:*/

            .datepicker td, .datepicker th {
                width: 100px;
                height: 200px;
            }
    </style>
    <style type="text/css">
        .ui-datepicker {
            font-size: 8pt !important;
            width: 230px;
        }
    </style>

    <div runat="server">

        <div>

            <div>

                <h2 style="text-align: center;">Add New Employee 
                                <asp:Label ID="txt_date" runat="server"></asp:Label></h2>
            </div>
            <div>
                <div class="col-sm-12 col-md-12 col-xs-12">
                    <div class="row">
                       
                        <div class="col-sm-4 col-md-4 col-xs-12">
                            <div class="form-group">
                                District:
                                     <asp:DropDownList ID="ddldistrict" CssClass="form-control" Height="32px" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged">
                                         <asp:ListItem Value="0">Select</asp:ListItem>
                                     </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-4 col-md-4 col-xs-12">
                            <div class="form-group">
                                Branch:
                                   <asp:DropDownList ID="ddlbranch" CssClass="form-control" Height="32px" AutoPostBack="false" runat="server">
                                       <asp:ListItem Value="0">Select</asp:ListItem>
                                   </asp:DropDownList>

                            </div>
                        </div>
                      
                        <div class="col-sm-4 col-md-4 col-xs-12">
                            <div class="form-group">
                                <label>Name</label>
                                <asp:TextBox ID="lblname" runat="server" CssClass="form-control" Placeholder="Name"></asp:TextBox>
                            </div>
                        </div>


                        <div class="col-sm-4 col-md-4 col-xs-12">
                            <div class="form-group">
                                <label>Designation</label>
                                <asp:DropDownList ID="DDLdegingnation" runat="server" Height="32px" AutoPostBack="false" class="form-control innertext">
                                    <asp:ListItem Text="--Select Designation--" Value="0"></asp:ListItem>
                                </asp:DropDownList>

                            </div>
                        </div>
                        <div class="col-sm-4 col-md-4 col-xs-12">
                            <div class="form-group">
                                <label>DOB(Date Of Birth)</label>
                                <asp:TextBox ID="txtdob" runat="server" CssClass="form-control" data-date-format="dd/mm/yyyy" Placeholder="Date of Birth"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rvDatepicker" color="red" runat="server"
                                    ControlToValidate="txtdob" ErrorMessage="Please Insert Date" ForeColor="#CC3300"
                                    ValidationGroup="myValidator"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator runat="server" ControlToValidate="txtdob" ValidationExpression="(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$"
                                    ErrorMessage="Invalid date format." ValidationGroup="A" ForeColor="Red" />

                            </div>
                        </div>
                        <div class="col-sm-4 col-md-4 col-xs-12">
                            <div class="form-group">
                                <label>DOJ(Date Of Joining)</label>
                                <asp:TextBox ID="txtdoj" runat="server" CssClass="form-control" data-date-format="dd/mm/yyyy" Placeholder="Date og Joining"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" color="red" runat="server"
                                    ControlToValidate="txtdoj" ErrorMessage="Please Insert Date" ForeColor="#CC3300"
                                    ValidationGroup="myValidator"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator runat="server" ControlToValidate="txtdoj" ValidationExpression="(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$"
                                    ErrorMessage="Invalid date format." ValidationGroup="A" ForeColor="Red" />
                            </div>
                        </div>
                        <div class="col-sm-4 col-md-4 col-xs-12">
                            <div class="form-group">
                                <label>Mobile No.</label>
                                <asp:TextBox ID="txtmobileno" runat="server" CssClass="form-control" Placeholder="Mobile No."></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                    ControlToValidate="txtmobileno" ErrorMessage="Please Insert Mobile No." ForeColor="#CC3300"
                                    ValidationGroup="myValidator"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-sm-3 col-md-3 col-xs-12">
                            <div class="form-group">
                                <label>Email</label>
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" Placeholder="Email"></asp:TextBox>
                           
                                </div>
                        </div>



                        <div class="col-sm-4 col-md-4 col-xs-12">
                            <div class="form-group">
                                <label>Upload Employee Image</label>
                                <asp:FileUpload ID="FileUpload1" runat="server" />
                                <asp:RequiredFieldValidator ID="rvFileUpload1" runat="server"
                                    ControlToValidate="FileUpload1" ErrorMessage="Please Insert File" ForeColor="#CC3300"
                                    ValidationGroup="myValidator"></asp:RequiredFieldValidator>
                            </div>

                        </div>
                    </div>
                </div>


                <center>
                    <asp:Button ID="btnSave" runat="server" Text="Add New Employee" ValidationGroup="myValidator" CssClass="btn-outline-primary1" OnClick="btnSave_Click1" /></center>
            </div>
            <asp:GridView runat="server" ID="gdImage" HeaderStyle-BackColor="Tomato"  AutoGenerateColumns="false">  
                <Columns>  
                    <asp:BoundField DataField="District_Name" HeaderText="District Name" />  
                    <asp:BoundField DataField="DepotName" HeaderText="Branch Name" />  
                    <asp:BoundField DataField="EmpName" HeaderText="Employee Name" />  
                    <asp:BoundField DataField="Designation" HeaderText="Designation" />  
                    <asp:ImageField DataImageUrlField="Image" HeaderText="Image" ItemStyle-Width="5%"></asp:ImageField>                     
                </Columns>  
            </asp:GridView>
        </div>
    </div>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.7.1/css/bootstrap-datepicker.min.css" rel="stylesheet" />
    <script src="<%= ResolveUrl("~/NEW_CSS/js/jquery-1.12.4.js") %>"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.7.1/js/bootstrap-datepicker.min.js"></script>
    <script src="<%= ResolveUrl("~/assets/js/bootstrap.min.js") %>"></script>

    <script>
        function AllowAlphabet(e) {
            isIE = document.all ? 1 : 0
            keyEntry = !isIE ? e.which : event.keyCode;
            if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || (keyEntry == '46') || (keyEntry == '32') || keyEntry == '45')
                return true;
            else {
                alert('Please Enter Only Character values.');
                return false;
            }
        }

        function AllowNumber(evt) {
            if (evt.charCode > 31 && (evt.charCode < 48 || evt.charCode > 57)) {
                alert("Allow Only Numbers");
                return false;
            }
        }

    </script>
</asp:Content>

