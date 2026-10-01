<%@ Page Language="C#" MasterPageFile="~/Inspections/Masters/State.master" AutoEventWireup="true" CodeFile="Ho_Insecticide_Distribution_Ro_Entry.aspx.cs" Inherits="Inspections_State_Insecticide_Distribution_Entry" Title="Account Detail" %>

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
    <link href="../Assets/css/bootstrap-datepicker.css" rel="stylesheet" />
    <script type="text/javascript" src="../Assets/js/bootstrap-datepicker.js"></script>
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

        .grid-header {
            background-color: #ADD8E6 !important;
            color: black;
            font-weight: bold;
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
    </style>
    <style>
        /* Custom date picker colors */
        .datepicker {
            background-color: #f0f8ff; /* Light blue background */
        }

            .datepicker table tbody tr td.active,
            .datepicker table tbody tr td.active:hover {
                background-color: #ff6347; /* Tomato color for selected date */
                color: white; /* White text color for selected date */
            }

            .datepicker table tbody tr td:hover {
                background-color: #87ceeb; /* Sky blue color on hover */
            }

            .datepicker .datepicker-days .datepicker-switch {
                color: #008080; /* Teal color for the month/year switch */
            }

            .datepicker .datepicker-days .prev,
            .datepicker .datepicker-days .next {
                color: #008080; /* Teal arrows */
            }

            .datepicker table {
                border: 2px solid #008080; /* Teal border around the calendar */
            }

                .datepicker table tbody tr td {
                    color: #333; /* Dark text color for the dates */
                }
    </style>
    <!-- Bootstrap DatePicker CSS -->
    <link href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.9.0/css/bootstrap-datepicker.min.css" rel="stylesheet">
    <!-- Bootstrap DatePicker JS -->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.9.0/js/bootstrap-datepicker.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <asp:Label ID="lblMsg" runat="server"></asp:Label>
        <fieldset style="border: 1px solid navy; border-radius: 10px 10px 10px 10px;">
            <legend style="text-align: center; background-color: aliceblue;">HO Insecticide Distribution RO</legend>
            <div class="row" style="margin-top: 10px">
                <div class="col-md-2" style="margin-top: 5px">
                    <label>Supplier Order No</label>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <span class="fa-pull-right">
                            <asp:RequiredFieldValidator runat="server" CssClass="fa fa-pull-right" ID="RequiredFieldValidator3" Display="Dynamic" ControlToValidate="txtSupplierOrderNO" ValidationGroup="a" Text="<i class='fa fa-exclamation-circle'></i>" ToolTip="Please Enter Supplier Order No" ErrorMessage="Please Enter Supplier Order No" ForeColor="Red"></asp:RequiredFieldValidator>
                        </span>
                        <asp:TextBox runat="server" ID="txtSupplierOrderNO" CssClass="form-control" AutoComplete="off"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-2" style="margin-top: 5px">
                    <label>Dispatch Date</label>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <span class="fa-pull-right">
                            <asp:RequiredFieldValidator runat="server" CssClass="fa fa-pull-right" ID="RequiredFieldValidator4" Display="Dynamic" ControlToValidate="txtTranferDate" ValidationGroup="a" Text="<i class='fa fa-exclamation-circle'></i>" ToolTip="Please Select Supplier Order Date" ErrorMessage="Please Select Supplier Order Date" ForeColor="Red"></asp:RequiredFieldValidator>
                        </span>
                        <asp:TextBox ID="txtTranferDate" runat="server" placeholder="dd/mm/yyyy"
                            CssClass="form-control dateAdd" data-date-end-date="0d" autocomplete="off" data-provide="datepicker"
                            onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy"></asp:TextBox>
                    </div>
                </div>

                <div class="col-md-2" style="margin-top: 5px">
                    <label>Name Supplyer</label>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <span class="fa-pull-right">
                            <asp:RequiredFieldValidator runat="server" CssClass="fa fa-pull-right" ID="RequiredFieldValidator1" Display="Dynamic" ControlToValidate="txtSupplierName" ValidationGroup="a" Text="<i class='fa fa-exclamation-circle'></i>" ToolTip="Please Enter Supplyer Name" ErrorMessage="Please Enter Supplyer Name" ForeColor="Red"></asp:RequiredFieldValidator>
                        </span>
                        <asp:TextBox runat="server" ID="txtSupplierName" CssClass="form-control" onkeypress="return lettersOnly()" AutoComplete="off"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2" style="margin-top: 5px">
                    <label>Region :</label>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <span class="fa-pull-right">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ValidationGroup="a"
                                ErrorMessage="Select Region" ToolTip="Select Region" Text="<i class='fa fa-exclamation-circle' title='Select Region !'></i>"
                                ControlToValidate="ddlregion" InitialValue="0" CssClass="fa fa-pull-right" ForeColor="Red" Display="Dynamic" runat="server">
                            </asp:RequiredFieldValidator>
                        </span>
                        <asp:DropDownList ID="ddlregion" runat="server" class="form-control" AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-2" style="margin-top: 5px">
                    <label>Insecticide Name :</label>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <span class="fa-pull-right">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a"
                                ErrorMessage="Select insecticide" ToolTip="Select insecticide" Text="<i class='fa fa-exclamation-circle' title='Select insecticide !'></i>"
                                ControlToValidate="ddlinsecticide" InitialValue="0" CssClass="fa fa-pull-right" ForeColor="Red" Display="Dynamic" runat="server">
                            </asp:RequiredFieldValidator>
                        </span>
                        <asp:DropDownList ID="ddlinsecticide" Height="35px" Class="form-control" runat="server" OnSelectedIndexChanged="ddlinsecticide_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem Value="0">Select</asp:ListItem>
                            <asp:ListItem Value="1">Alluminium Phosphide</asp:ListItem>
                            <asp:ListItem Value="2">Malathion</asp:ListItem>
                            <asp:ListItem Value="3">Deltamethrin</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-2" style="margin-top: 5px">
                    <label>Unit :</label>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <span class="fa-pull-right">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="a"
                                ErrorMessage="Select Unit Stock" ToolTip="Select Unit Stock" Text="<i class='fa fa-exclamation-circle' title='Select Unit Stock !'></i>"
                                ControlToValidate="ddlUnitStock" InitialValue="0" CssClass="fa fa-pull-right" ForeColor="Red" Display="Dynamic" runat="server">
                            </asp:RequiredFieldValidator>
                        </span>
                        <asp:DropDownList ID="ddlUnitStock" Height="35px" Class="form-control" runat="server">
                            <asp:ListItem Value="0">Select</asp:ListItem>
                            <asp:ListItem Value="1">KiloGram(KG)</asp:ListItem>
                            <asp:ListItem Value="2">Litter</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2" style="margin-top: 5px">
                    <label>Quantity :</label>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <span class="fa-pull-right">
                            <asp:RequiredFieldValidator runat="server" CssClass="fa fa-pull-right" ID="RequiredFieldValidator6" Display="Dynamic" ControlToValidate="txtQuantity" ValidationGroup="a" Text="<i class='fa fa-exclamation-circle'></i>" ToolTip="Please Enter Quantity" ErrorMessage="Please Enter Quantity" ForeColor="Red"></asp:RequiredFieldValidator>
                        </span>
                        <asp:TextBox runat="server" ID="txtQuantity" CssClass="form-control" onkeypress="return isNumber()" AutoComplete="off" OnTextChanged="txtQuantity_TextChanged" AutoPostBack="true"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-2" style="margin-top: 5px">
                    <label>Purchase Rate :</label>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <span class="fa-pull-right">
                            <asp:RequiredFieldValidator runat="server" CssClass="fa fa-pull-right" ID="RequiredFieldValidator7" Display="Dynamic" ControlToValidate="txtMarketRed" ValidationGroup="a" Text="<i class='fa fa-exclamation-circle'></i>" ToolTip="Please Enter Market Rate" ErrorMessage="Please Enter Market Rate" ForeColor="Red"></asp:RequiredFieldValidator>
                        </span>
                        <asp:TextBox runat="server" ID="txtMarketRed" CssClass="form-control" onkeypress="return isNumber()" AutoComplete="off" OnTextChanged="txtMarketRed_TextChanged" AutoPostBack="true"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-2" style="margin-top: 5px">
                    <label>Total Value :</label>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <span class="fa-pull-right">
                            <asp:RequiredFieldValidator runat="server" CssClass="fa fa-pull-right" ID="RequiredFieldValidator9" Display="Dynamic" ControlToValidate="txtValue" ValidationGroup="a" Text="<i class='fa fa-exclamation-circle'></i>" ToolTip="Please Enter Value" ErrorMessage="Please Enter Value" ForeColor="Red"></asp:RequiredFieldValidator>
                        </span>
                        <asp:TextBox runat="server" ID="txtValue" ReadOnly="true" CssClass="form-control" onkeypress="return isNumber()" AutoComplete="off"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2" style="margin-top: 5px">
                    <label>Expriy Date</label>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <span class="fa-pull-right">
                            <asp:RequiredFieldValidator runat="server" CssClass="fa fa-pull-right" ID="RequiredFieldValidator10" Display="Dynamic" ControlToValidate="txtExpriy" ValidationGroup="a" Text="<i class='fa fa-exclamation-circle'></i>" ToolTip="Please Select Expriy Date" ErrorMessage="Please Select Expriy Date" ForeColor="Red"></asp:RequiredFieldValidator>
                        </span>
                        <asp:TextBox ID="txtExpriy" runat="server" placeholder="dd/mm/yyyy"
                            CssClass="form-control dateAdd" autocomplete="off" data-provide="datepicker"
                            onpaste="return false ;" onkeypress="return validateDates();" data-date-format="dd/mm/yyyy"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-2" style="margin-top: 5px">
                    <label>Total Closing Balance</label>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <span class="fa-pull-right">
                            <asp:RequiredFieldValidator runat="server" CssClass="fa fa-pull-right" ID="RequiredFieldValidator11" Display="Dynamic" ControlToValidate="lblincbal" ValidationGroup="a" Text="<i class='fa fa-exclamation-circle'></i>" ToolTip="Please Enter Quantity" ErrorMessage="Please Enter Quantity" ForeColor="Red"></asp:RequiredFieldValidator>
                        </span>
                        <asp:TextBox runat="server" ID="lblincbal" CssClass="form-control" onkeypress="return isNumber()" AutoComplete="off" ReadOnly="true"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-5"></div>
                <div class="col-md-2">
                    <asp:Button runat="server" ID="btnSubmit" CssClass="btn btn-info btn-block" OnClick="btnSubmit_Click" ValidationGroup="a" Text="SUBMIT" />
                    <asp:Button ID="btnUpdate" runat="server" Text="Update" Visible="false" CssClass="btn btn-info btn-block" OnClick="btnUpdate_Click" />

                </div>
            </div>
        </fieldset>
        <fieldset style="border: 1px solid navy; border-radius: 10px 10px 10px 10px;">
            <legend style="text-align: center; background-color: aliceblue;">HO Insecticide Distribution RO List</legend>
            <div class="row">
                <div class="col-12">
                    <asp:GridView ID="GVOfStock" OnRowEditing="GVOfStock_RowEditing" OnRowUpdating="GVOfStock_RowUpdating"
                        AutoGenerateColumns="false" runat="server"
                        OnRowCancelingEdit="GVOfStock_RowCancelingEdit1"
                        OnRowCommand="GVOfStock_RowCommand">
                        <HeaderStyle
                            BackColor="#D69758"
                            Font-Italic="false"
                            ForeColor="Snow" />
                        <Columns>
                            <asp:TemplateField HeaderText="S.No." ItemStyle-Width="3%">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                    <asp:HiddenField ID="hdnID" runat="server" Value='<%# Bind("ID") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Supplier Order No">
                                <ItemTemplate>
                                    <asp:Label ID="lblSupplier_Order_NO" runat="server" Text='<%#Eval("Supplier_Order_NO") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtSupplier_Order_NO" runat="server" Text='<%#Eval("Supplier_Order_NO") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Supplier Order Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblTranfer_date" runat="server" Text='<%#Eval("Tranfer_date") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtTranfer_date" runat="server" Text='<%#Eval("Tranfer_date") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Supplier Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblSupplier_Name" runat="server" Text='<%#Eval("Supplier_Name") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtSupplier_Name" runat="server" Text='<%#Eval("Supplier_Name") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Region">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Region" runat="server" Text='<%#Eval("Regionnm") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txt_Purchase" runat="server" Text='<%#Eval("Regionnm") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="UNIT Name">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Unit_Name" runat="server" Text='<%#Eval("Unit_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Insecticide Name">
                                <ItemTemplate>
                                    <asp:Label ID="Insecticide_Name" runat="server" Text='<%#Eval("Insecticide_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Quantity">
                                <ItemTemplate>
                                    <asp:Label ID="ob_quantity" runat="server" Text='<%#Eval("Opening_Balance_quantity") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txt_ob_quantity" runat="server" Text='<%#Eval("Opening_Balance_quantity") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Market Value">
                                <ItemTemplate>
                                    <asp:Label ID="ob_market_value" runat="server" Text='<%#Eval("Opening_Balance_market_value") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txt_op_market_value" runat="server" Text='<%#Eval("Opening_Balance_market_value") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total Value">
                                <ItemTemplate>
                                    <asp:Label ID="ob_value" runat="server" Text='<%#Eval("Opening_Balance_value") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txt_ob_value" runat="server" Text='<%#Eval("Opening_Balance_value") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Expriy Date">
                                <ItemTemplate>
                                    <asp:Label ID="ob_Expriy" runat="server" Text='<%#Eval("Date_Expriy") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txt_Expiy" runat="server" Text='<%#Eval("Date_Expriy") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>

                </div>
            </div>
        </fieldset>
        <script>
            function isNumber(evt) {
                evt = (evt) ? evt : window.event;
                var charCode = (evt.which) ? evt.which : evt.keyCode;
                if (charCode > 32 && (charCode < 46 || charCode == 47 || charCode > 57)) {
                    return false;
                }
                return true;
            }
            function lettersOnly() {
                var charCode = event.keyCode;
                if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || charCode == 8 || charCode == 32)
                    return true;
                else
                    return false;
            }

            function hindiOnly() {
                var charCode = event.keyCode;
                if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || charCode == 8 || charCode == (u + 0020))
                    return false;
                else
                    return true;
            }
        </script>
        <script>
            function validateDates() {
                const manufacturingDateInput = document.getElementById("txtTranferDate"); // Replace with your actual ID
                const expiryDateInput = document.getElementById("txtExpriy"); // Replace with your actual ID
                const errorMessageElement = document.getElementById("lblMsg"); // Replace with your error message element ID

                const manufacturingDateValue = new Date(manufacturingDateInput.value);
                const expiryDateValue = new Date(expiryDateInput.value);

                if (isNaN(manufacturingDateValue) || isNaN(expiryDateValue)) {
                    errorMessageElement.textContent = "Please enter valid manufacturing and expiry dates.";
                    return false; // Prevent form submission
                }

                if (expiryDateValue <= manufacturingDateValue) {
                    errorMessageElement.textContent = "Expiry date cannot be earlier than or the same as the manufacturing date.";
                    return false; // Prevent form submission
                }

                errorMessageElement.textContent = ""; // Clear any previous error message
                return true; // Allow form submission
            }
        </script>
    </div>
</asp:Content>

