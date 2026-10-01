<%@ Page Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="~/BranchPages/Update_Godown_Weibrize_details.aspx.cs" Inherits="BranchPages_Update_Godown_Weibrize_details" Title="Update Weighbridge Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../assets/css/style.css" rel="stylesheet" />
    <!-- font awesome -->
    <link href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../assets/New/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assets/New/css/bootstrap.min2.css" rel="stylesheet" />
    <link href="../assets/New/css/select2.min.css" rel="stylesheet" />
    <link href="../CSS/style.css" rel="stylesheet" />
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />
    <script type="text/javascript" src='<%= ResolveUrl("~/NEW_CSS/js/jquery-1.12.4.js") %>'></script>
    <script type="text/javascript">
        window.history.forward();
        function noBack() { window.history.forward(); }
    </script>
    <link href="../Assets/css/bootstrap-datepicker.css" rel="stylesheet" />
    <script type="text/javascript" src="../Assets/js/bootstrap-datepicker.js"></script>
    <script type="text/javascript" src="../assets/New/js/select2.min.js"></script>
    <style type="text/css">
        .button {
            background-color: #4CAF50; /* Green */
            border: none;
            color: white;
            padding: 0px 0px;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            font-size: 12px;
            font-weight: bold;
            margin: 4px 2px;
            -webkit-transition-duration: 0.4s; /* Safari */
            transition-duration: 0.4s;
            cursor: pointer;
        }

        .button1 {
            background-color: white;
            color: black;
            border: 2px solid #4CAF50;
        }

            .button1:hover {
                background-color: #4CAF50;
                color: white;
            }

        .button2 {
            background-color: white;
            color: black;
            border: 2px solid #008CBA;
        }

            .button2:hover {
                background-color: #008CBA;
                color: white;
            }
    </style>
    <style type="text/css">
        .left, .right {
            float: left;
            width: 20%; /* The width is 20%, by default */
        }

        .main {
            float: left;
            width: 60%; /* The width is 60%, by default */
        }

        /* Use a media query to add a breakpoint at 800px: */
        @media screen and (max-width: 800px) {
            .left, .main, .right {
                width: 100%; /* The width is 100%, when the viewport is 800px or smaller */
            }
        }
    </style>
    <style type="text/css">
        fieldset {
            border: 1px solid #2095A1;
            padding: 0.35em 0.625em 0.75em;
            margin: 10px;
            border-radius: 5px;
            padding-left: 20px;
            /*text-align: center;*/ /* aligns content inside fieldset */
        }

        legend {
            padding: 2px 8px;
            border-radius: 10px;
            width: auto;
            border: 1px solid #2095A1;
            font-size: 17px;
            font-weight: bold;
            color: #030203;
        }

        .content-wrapper {
            padding: 1.75rem 1.25rem;
        }

        .table-bordered th, .table-bordered td {
            border: 1px solid #030203;
        }

        .form-control {
            border: 1px solid #767B83;
            border-radius: 8px;
        }

        .table th {
            text-align: center;
        }

        .form-inline {
            display: block !important;
        }

        th.sorting, th.sorting_asc, th.sorting_desc {
            background: #647e68 !important;
            color: black !important;
        }

        .table > tbody > tr > td, .table > tbody > tr > th, .table > tfoot > tr > td, .table > tfoot > tr > th, .table > thead > tr > td, .table > thead > tr > th {
            padding: 8px 5px;
            color: black !important;
        }

        element.style {
            font-size: medium !important;
        }

        .auto-style3 {
            position: relative;
            min-height: 1px;
            float: left;
            width: 50%;
            left: 0px;
            top: 0px;
            padding-left: 15px;
            padding-right: 15px;
        }

        .weighbridge-label {
            color: red;
            font-weight: bold;
            font-size: 18px; /* size yahan control kar sakte ho */
        }
    </style>
    <script type="text/javascript">
        $(function () {
            $("[id*=ddlGodown]").select2();
        });
    </script>
    <script type="text/javascript">
        $(function () {
            $("[id*=ddlDistrict]").select2();
        });
    </script>
    <script type="text/javascript">
        $(function () {
            $("[id*=ddlBranch]").select2();
        });
    </script>
    <script type="text/javascript">
        function isNumberDecimal(evt, element) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;

            // Allow backspace, delete, tab
            if (charCode == 8 || charCode == 9 || charCode == 46) return true;

            // Get current value
            var val = element.value;

            // Allow only one dot
            if (charCode == 46) {
                if (val.indexOf('.') >= 0) return false;
                return true;
            }

            // Only digits
            if (charCode < 48 || charCode > 57) return false;

            // Check for 2 decimals
            var dotIndex = val.indexOf('.');
            if (dotIndex >= 0) {
                var decimalPart = val.substring(dotIndex + 1);
                if (decimalPart.length >= 2) return false;
            }

            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <asp:Label ID="lblMessage" runat="server" CssClass="text-success" Visible="false"></asp:Label>
        <fieldset style="background-color: whitesmoke">
            <legend>Godown Wise WeightBridge details</legend>
            <div class="row">
                <div class="col-md-1" style="margin-top: 7px">
                    <label>Godown Name</label>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList CssClass="form-control select2" ID="ddlGodown" AutoPostBack="true" runat="server">
                    </asp:DropDownList>
                </div>
                <div class="col-md-1" style="margin-top: 7px">
                    <label>Email ID</label>
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" AutoComplete="off" placeholder="Enter Email ID"> </asp:TextBox>
                    <!-- Required Validation -->
                    <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="Email is required" ForeColor="Red"
                        Display="Dynamic" ValidationGroup="vgSubmit"> </asp:RequiredFieldValidator>
                    <!-- Email Format Validation -->
                    <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="Invalid Email Format" ForeColor="Red"
                        Display="Dynamic" ValidationGroup="vgSubmit" ValidationExpression="^[^@\s]+@[^@\s]+\.[^@\s]+$"> </asp:RegularExpressionValidator>
                </div>
                <div class="col-md-1" style="margin-top: 7px">
                    <label>Mobile No</label>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtMobileNo" runat="server" CssClass="form-control" AutoComplete="off" MaxLength="10" placeholder="Enter Mobile Number" onkeypress="return isNumberKey(event)"></asp:TextBox>
                    <!-- Required Validation -->
                    <asp:RequiredFieldValidator ID="rfvMobile" runat="server" ControlToValidate="txtMobileNo" ErrorMessage="Mobile number is required"
                        ForeColor="Red" Display="Dynamic" ValidationGroup="vgSubmit"> </asp:RequiredFieldValidator>
                    <!-- 10 Digit Mobile Validation -->
                    <asp:RegularExpressionValidator ID="revMobile" runat="server" ControlToValidate="txtMobileNo" ErrorMessage="Enter valid 10 digit mobile number" ForeColor="Red"
                        Display="Dynamic" ValidationGroup="vgSubmit" ValidationExpression="^[6-9][0-9]{9}$"> </asp:RegularExpressionValidator>
                </div>
            </div>
            <div class="row" style="margin-top: 20px;">
                <div class="col-md-4">
                    <asp:Label ID="lblWeighbridgeInfo" runat="server" CssClass="weighbridge-label" Text="यदि गोदाम परिसर में WeightBridge Available है |"></asp:Label>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList ID="ddlWeighbridge" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlWeighbridge_SelectedIndexChanged">
                        <asp:ListItem Value="0">-- Select --</asp:ListItem>
                        <asp:ListItem Value="1">Yes</asp:ListItem>
                        <asp:ListItem Value="2">No</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvWBAvail" runat="server" ControlToValidate="ddlWeighbridge" InitialValue=""
                        ErrorMessage="Select option" ForeColor="Red" ValidationGroup="vgSubmit" />
                </div>
                <div class="col-md-2" style="margin-top: 7px">
                    <label>WeightBridge Weight  <span style="color: red; font-weight: bold;">(MT)</span></label>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtWeightbridgeWeight" runat="server" CssClass="form-control" placeholder="Enter Weight" onkeypress="return isNumberDecimal(event, this)"></asp:TextBox>
                    <!-- Required only when Yes -->
                    <asp:RequiredFieldValidator ID="rfvWeight" runat="server" ControlToValidate="txtWeightbridgeWeight" ErrorMessage="Weight is required"
                        ForeColor="Red" Display="Dynamic" ValidationGroup="vgSubmit"> </asp:RequiredFieldValidator>
                    <!-- 2 Decimal Validation -->
                    <asp:RegularExpressionValidator ID="revWeightMT" runat="server" ControlToValidate="txtWeightbridgeWeight" ValidationExpression="^\d+(\.\d{1,2})?$"
                        ErrorMessage="Enter valid number (up to 2 decimals)" ForeColor="Red" Display="Dynamic" ValidationGroup="vgSubmit"> </asp:RegularExpressionValidator>
                </div>
            </div>
        </fieldset>
        <fieldset id="fsNoWB" style="margin-top: 20px;" runat="server" visible="false">
            <legend>Weightbridge Details</legend>
            <div class="row">
                <div class="col-md-1">
                    <label>District</label>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">
                        <%--<asp:ListItem  Value="0">Select</asp:ListItem>--%>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvDistrict" runat="server" ControlToValidate="ddlDistrict" InitialValue="0"
                        ErrorMessage="Select District" ForeColor="Red" ValidationGroup="vgSubmit" />
                </div>
                <div class="col-md-1">
                    <label>Branch</label>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvbranch" runat="server" ControlToValidate="ddlBranch" InitialValue=""
                        ErrorMessage="Select Branch" ForeColor="Red" ValidationGroup="vgSubmit" />
                </div>
                <div class="col-md-1">
                    <label>Address</label>
                </div>
                <div class="col-md-5">
                    <asp:TextBox ID="txtWBAddress" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvAddress" runat="server" ControlToValidate="txtWBAddress" ErrorMessage="Address required"
                        ForeColor="Red" ValidationGroup="vgSubmit" />
                </div>
            </div>
            <!-- Distance -->
            <div class="row" style="margin-top: 10px;">
                <div class="col-md-3">
                    <label>Distance from Warehouse to Weightbridge (KM)</label>
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtDistanceKM" runat="server" onkeypress="return isNumberKey(event)" CssClass="form-control" placeholder="Enter Distance"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvDistance" runat="server" ControlToValidate="txtDistanceKM"
                        ErrorMessage="Distance required" ForeColor="Red" ValidationGroup="vgSubmit" />
                </div>
            </div>
        </fieldset>
        <div class="row" style="margin-top: 20px" runat="server" id="btnsubmit" visible="false">
            <div class="col-md-5"></div>
            <div class="col-md-1">
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-success" ValidationGroup="vgSubmit" OnClick="btnSave_Click" />
            </div>
        </div>
        <div class="row" style="align-content: center" runat="server" id="grddetails" visible="false">
            <div class="col-md-12">
                <fieldset>
                    <legend>Details</legend>
                    <div class="table-responsive" style="height: 200px;">
                        <asp:GridView runat="server" ID="Grdweight" DataKeyNames="Godown_Id" OnRowCommand="Grdweight_RowCommand"
                            CssClass="table table-bordered table-hover datatable" AutoGenerateColumns="False">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Godown Name">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblGodown_Id" Visible="false" Text='<%# Eval("Godown_Id") %>'></asp:Label>
                                        <asp:Label runat="server" ID="lblGodown_Name" Text='<%# Eval("Godown_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Email Id">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblEmail_Id" Text='<%# Eval("Email_Id") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mobile No">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblMobile_No" Text='<%# Eval("Mobile_No") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Weighbridge Available">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblWeighbridge_Available" Text='<%# Eval("Weighbridge_Available") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="WeightBridge Weight MT">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblWeightBridge_Weight_MT" Text='<%# Eval("WeightBridge_Weight_MT") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="District Name">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblDistrict_Name" Text='<%# Eval("District_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Branch Name">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblBranch_Name" Text='<%# Eval("Branch_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="WeightBridge Address">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblWeightBridge_Address" Text='<%# Eval("WeightBridge_Address") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Distance From Warehouse (KM)">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblDistance_From_Warehouse_KM" Text='<%# Eval("Distance_From_Warehouse_KM") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn btn-danger btn-sm"
                                            CommandName="DeleteRow" CommandArgument='<%# Container.DataItemIndex %>' OnClientClick="return confirm('Are you sure you want to delete this record?');" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>No Record Found</EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                </fieldset>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            // Allow backspace, delete, tab
            if (charCode == 8 || charCode == 9 || charCode == 46)
                return true;
            // Allow only numbers (0-9)
            if (charCode < 48 || charCode > 57)
                return false;
            return true;
        }
    </script>
</asp:Content>

