<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="~/BranchPages/WeightBridge_Regitration.aspx.cs" Inherits="BranchPages_WeightBridge_Regitration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../assets/css/style.css" rel="stylesheet" />
    <!-- font awesome -->
    <link href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../assets/New/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assets/New/css/bootstrap.min2.css" rel="stylesheet" />
    <link href="../CSS/style.css" rel="stylesheet" />
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />
    <script type="text/javascript" src='<%= ResolveUrl("~/NEW_CSS/js/jquery-1.12.4.js") %>'></script>
    <script type="text/javascript">
        window.history.forward();
        function noBack() { window.history.forward(); }
    </script>
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

        .table-hover tbody tr:hover {
            background-color: #f2f7ff;
        }

        .btn {
            margin: 2px;
        }

        .thead-dark th {
            background-color: #caf0f8;
            color: white;
            font-size: 14px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <asp:Label ID="lblMsg" runat="server"></asp:Label>
        <fieldset>
            <legend>Weight Bridge Entry</legend>

            <!-- Row 1 -->
            <div class="row">
                <div class="col-md-3 text-center">
                    <label class="font-weight-bold d-block">WB Serial No</label>
                    <asp:TextBox ID="txtwb_serial" runat="server" MaxLength="15" AutoComplete="off"
                        CssClass="form-control" Placeholder="Enter Weight Bridge Serial no" />
                    <asp:RequiredFieldValidator ID="rfvSerial" runat="server" ControlToValidate="txtwb_serial" ErrorMessage="WB Serial No is required"
                        ForeColor="Red" ValidationGroup="a" />
                    
                </div>
                <div class="col-md-3 text-center">
                    <label class="font-weight-bold">WB Status</label>
                    <asp:DropDownList ID="ddlStatus" runat="server"
                        CssClass="form-control">
                        <asp:ListItem Value="0">-- Select --</asp:ListItem>
                        <asp:ListItem Value="Active">Active</asp:ListItem>
                        <asp:ListItem Value="Inactive">Inactive</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvStatus" runat="server" ControlToValidate="ddlStatus" InitialValue="0" ErrorMessage="Please select WB Status"
                        ForeColor="Red" ValidationGroup="a" />
                </div>

                <div class="col-md-3 text-center">
                    <label class="font-weight-bold d-block">WB Latitude</label>
                    <asp:TextBox ID="txtlat" runat="server" AutoComplete="off"
                        CssClass="form-control" Placeholder="Enter WB Latitude" />
                    <asp:RequiredFieldValidator ID="rfvLat" runat="server" ControlToValidate="txtlat" ErrorMessage="Latitude is required"
                        ForeColor="Red" ValidationGroup="a" />
                    <asp:RegularExpressionValidator ID="revLatitude" runat="server" ControlToValidate="txtlat" ValidationExpression="^(\+|-)?(?:90(?:\.0{4,6})|[0-8]?\d(?:\.\d{4,6}))$"
                        ErrorMessage="Format: ±00.0000 (4-6 decimals required)" Display="Dynamic" ForeColor="Red" />
                </div>
                <div class="col-md-3 text-center">
                    <label class="font-weight-bold d-block">WB Longititude</label>
                    <asp:TextBox ID="txtlong" runat="server" AutoComplete="off"
                        CssClass="form-control" Placeholder="Enter WB Longititude" />
                    <asp:RequiredFieldValidator ID="rfvLong" runat="server" ControlToValidate="txtlong" ErrorMessage="Longitude is required"
                        ForeColor="Red" ValidationGroup="a" />
                    <asp:RegularExpressionValidator ID="revLongitude" runat="server" ControlToValidate="txtlong"
                        ValidationExpression="^(\+|-)?(?:180(?:\.0{4,6})|(?:1[0-7]\d|[1-9]?\d)(?:\.\d{4,6}))$"
                        ErrorMessage="Format: ±00.0000 (4-6 decimals required)" Display="Dynamic" ForeColor="Red" />
                </div>
            </div>
            <!-- Row 3 -->
            <div class="row " style="margin-top: 15px">
                <div class="col-md-3 text-center">
                    <label class="font-weight-bold">Contact Person</label>
                    <asp:TextBox ID="txtContactPerson" runat="server" MaxLength="50" AutoComplete="off"
                        CssClass="form-control" />
                    <asp:RequiredFieldValidator ID="rfvContact" runat="server" ControlToValidate="txtContactPerson" ErrorMessage="Contact Person is required"
                        ForeColor="Red" ValidationGroup="a" />
                </div>
                <div class="col-md-3 text-center">
                    <label class="font-weight-bold">Mobile No</label>
                    <asp:TextBox ID="txtMobile" runat="server" AutoComplete="off"
                        CssClass="form-control" MaxLength="10" />
                    <asp:RequiredFieldValidator ID="rfvMobile" runat="server" ControlToValidate="txtMobile" ErrorMessage="Mobile No is required"
                        ForeColor="Red" ValidationGroup="a" />
                    <asp:RegularExpressionValidator ID="revMobile" runat="server" ControlToValidate="txtMobile" ValidationExpression="^[6-9]\d{9}$"
                        ErrorMessage="Enter valid 10 digit mobile number" ForeColor="Red" ValidationGroup="a" />
                </div>
                <div class="col-md-3 text-center">
                    <label class="font-weight-bold">Capacity (MT)</label>
                    <asp:TextBox ID="txtCapacity" runat="server" MaxLength="6" AutoComplete="off"
                        CssClass="form-control" />
                    <asp:RequiredFieldValidator ID="rfvCapacity" runat="server" ControlToValidate="txtCapacity" ErrorMessage="Capacity is required"
                        ForeColor="Red" ValidationGroup="a" />
                    <asp:RegularExpressionValidator ID="revCapacity" runat="server" ControlToValidate="txtCapacity" ValidationExpression="^\d+(\.\d{1,2})?$" ErrorMessage="Invalid capacity format"
                        ForeColor="Red" ValidationGroup="a" />
                </div>
                <div class="col-md-3 text-center">
                    <label class="font-weight-bold  d-block">WB Address</label>
                    <asp:TextBox ID="txtWBAddress" runat="server"
                        CssClass="form-control" TextMode="MultiLine" />
                    <asp:RequiredFieldValidator ID="rfvAddress" runat="server" ControlToValidate="txtWBAddress" ErrorMessage="WB Address is required"
                        ForeColor="Red" ValidationGroup="a" />
                </div>
            </div>
            <!-- Row 2 -->
            <div class="row" style="margin-top: 15px">
                <div class="col-md-3 text-center">
                    <label class="font-weight-bold d-block">WB Image</label>
                    <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control" />
                    <small class="text-danger font-weight-bold" style="font-size: 14px;">Only PDF / JPG / PNG allowed
                    </small>
                    <asp:RequiredFieldValidator ID="rfvImage" runat="server" ControlToValidate="FileUpload1" ErrorMessage="Please upload WB Image"
                        ForeColor="Red" ValidationGroup="a" />
                </div>
            </div>
            <!-- Buttons -->
            <div class="row" style="margin-top: 10px">
                <div class="col-md-5"></div>
                <div class="col-md-1">
                    <asp:Button runat="server" ID="btnsave" CssClass="btn btn-info btn-block" ValidationGroup="a" Text="SUBMIT" OnClick="btnsave_Click" />
                </div>
                <div class="col-md-1">
                    <asp:Button runat="server" ID="btnClear" CssClass="btn btn-secondary btn-block" Text="Clear" OnClick="btnClear_Click" />
                </div>
            </div>
        </fieldset>
        <fieldset>
            <legend>Details</legend>
            <asp:GridView ID="gvWB" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover table-sm" HeaderStyle-CssClass="thead-dark text-center"
                RowStyle-CssClass="text-center align-middle" DataKeyNames="WB_ID" OnRowEditing="gvWB_RowEditing" OnRowCancelingEdit="gvWB_RowCancelingEdit"
                OnRowUpdating="gvWB_RowUpdating" OnRowDeleting="gvWB_RowDeleting">
                <Columns>
                    <asp:BoundField DataField="WB_ID" HeaderText="WB Reg. No" ReadOnly="true" />
                    <asp:BoundField DataField="WB_Serial_No" HeaderText="Serial No" />
                    <asp:BoundField DataField="WB_Status" HeaderText="Status" />
                    <asp:BoundField DataField="Latitude" HeaderText="Latitude" />
                    <asp:BoundField DataField="Longitude" HeaderText="Longitude" />
                    <asp:BoundField DataField="Contact_Person" HeaderText="Contact Person" />
                    <asp:BoundField DataField="Mobile_No" HeaderText="Mobile No" />
                    <asp:BoundField DataField="Capacity_MT" HeaderText="Capacity (MT)" />
                    <asp:BoundField DataField="WB_Address" HeaderText="Address" />
                    <asp:TemplateField HeaderText="WB Image">
                        <ItemTemplate>
                            <asp:HyperLink ID="HyperLink" runat="server" Target="_blank" CssClass="fa fa-eye" Visible='<%# Eval("WB_Image").ToString() !=""?true:false %>' NavigateUrl='<%# "../WB/" +Eval("WB_Image")%>'></asp:HyperLink>
                            <%-- <asp:Image ID="imgWB" runat="server" ImageUrl='<%# ResolveUrl("~/WB/") + Eval("WB_Image") %>' Width="80px"
                           <%-- <asp:Image ID="imgWB" runat="server" ImageUrl='<%# ResolveUrl("~/WB/") + Eval("WB_Image") %>' Width="80px"
                                Height="60px" Style="border: 1px solid #999; padding: 2px;" />--%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkEdit" runat="server" CommandName="Edit" CssClass="btn btn-sm btn-warning"><i class="fa fa-edit"></i> Edit </asp:LinkButton>
                            <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" CssClass="btn btn-sm btn-danger" OnClientClick="return confirm('Are you sure want to DELETE this record!?');"><i class="fa fa-trash"></i> Delete </asp:LinkButton>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:LinkButton ID="lnkUpdate" runat="server" CommandName="Update" CssClass="btn btn-sm btn-success"> <i class="fa fa-save"></i> Update </asp:LinkButton>
                            <asp:LinkButton ID="lnkCancel" runat="server" CommandName="Cancel" CssClass="btn btn-sm btn-secondary"> <i class="fa fa-times"></i> Cancel </asp:LinkButton>
                        </EditItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </fieldset>
    </div>
</asp:Content>

