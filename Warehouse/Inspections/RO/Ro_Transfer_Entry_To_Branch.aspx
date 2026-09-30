<%@ Page Title="" Language="C#" MasterPageFile="~/Inspections/Masters/Inspection_RO.master" AutoEventWireup="true" CodeFile="~/Inspections/RO/Ro_Transfer_Entry_To_Branch.aspx.cs" Inherits="Inspections_RO_Ro_Transfer_Entry_To_Branch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        window.history.forward();

        function noBack() { window.history.forward(); }
    </script>
    <script type="text/javascript">
        Cufon.replace('h1,h2,h3,h4,h5,#menu,#copy,.blog-date');
    </script>
    <style type="text/css">
        ul.svertical {
            width: 220px; /* width of menu */
            overflow: auto;
            background: #f4f4f4; /* background of menu */
            margin: 0;
            padding: 0;
            padding-top: 7px; /* top padding */
            list-style-type: none;
        }

            ul.svertical li {
                text-align: right; /* right align menu links */
            }

                ul.svertical li a {
                    position: relative;
                    display: inline-block;
                    text-indent: 5px;
                    overflow: hidden;
                    background: rgb(1, 138, 180); /* initial background color of links */
                    font: bold 16px Germand;
                    text-decoration: none;
                    padding: 5px;
                    margin-bottom: 5px; /* spacing between links */
                    color: White;
                    -moz-box-shadow: inset -7px 0 5px rgba(114,114,114, 0.8); /* inner right shadow added to each link */
                    -webkit-box-shadow: inset -7px 0 5px rgba(114,114,114, 0.8);
                    box-shadow: inset -7px 0 5px rgba(114,114,114, 0.8);
                    -moz-transition: all 0.2s ease-in-out; /* CSS3 transition of hover properties */
                    -webkit-transition: all 0.2s ease-in-out;
                    -o-transition: all 0.2s ease-in-out;
                    -ms-transition: all 0.2s ease-in-out;
                    transition: all 0.2s ease-in-out;
                }

                    ul.svertical li a:hover {
                        padding-right: 30px; /* add right padding to expand link horizontally to the left */
                        color: Black;
                        background: rgb(153,249,75);
                        -moz-box-shadow: inset -3px 0 2px rgba(114,114,114, 0.8); /* contract inner right shadow */
                        -webkit-box-shadow: inset -3px 0 5px rgba(114,114,114, 0.8);
                        box-shadow: inset -3px 0 5px rgba(114,114,114, 0.8);
                    }

                    ul.svertical li a:before { /* CSS generated content: slanted right edge */
                        content: "";
                        position: absolute;
                        left: 0;
                        top: 0;
                        border-style: solid;
                        border-width: 70px 0 0 20px; /* Play around with 1st and 4th value to change slant degree */
                        border-color: transparent transparent transparent #f4f4f4; /* change black to match the background color of the menu UL */
                    }

        /*print*/


        * {
            box-sizing: border-box;
            -moz-box-sizing: border-box;
        }

        .page {
            width: 21cm;
            min-height: 29.7cm;
            padding: 2cm;
            margin: 1cm auto;
            border: 1px #D3D3D3 solid;
            border-radius: 5px;
            background: white;
            box-shadow: 0 0 5px rgba(0, 0, 0, 0.1);
        }

        .subpage {
            padding: 1cm;
            border: 5px red solid;
            height: 237mm;
            outline: 2cm #FFEAEA solid;
        }

        @page {
            size: A4;
            margin: 0;
            font-size: smaller;
        }

        @media print {
            .page {
                margin: 0;
                border: initial;
                border-radius: initial;
                width: initial;
                min-height: initial;
                box-shadow: initial;
                background: initial;
                page-break-after: always;
                font-size: smaller;
            }
        }

        @media print {
            html, body {
                width: 210mm;
                height: 297mm;
                font-size: smaller;
            }
            /* ... the rest of the rules ... */
        }
        /*td{font-size:smaller;}*/
        page[size="A4"] {
            background: white;
            width: 21cm;
            height: 29.7cm;
            display: block;
            margin: 0 auto;
            margin-bottom: 0.5cm;
            box-shadow: 0 0 0.5cm rgba(0,0,0,0.5);
        }

        @media print {
            body, page[size="A4"] {
                margin: 0;
                box-shadow: 0;
                font-size: smaller;
            }
        }

        .style7 {
            height: 15px;
        }

        .style8 {
            height: 20px;
        }
    </style>
    <style type="text/css">
        .button {
            background-color: #4CAF50; /* Green */
            border: none;
            color: Black;
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

        .button3 {
            background-color: white;
            color: black;
            border: 2px solid #f44336;
        }

            .button3:hover {
                background-color: #f44336;
                color: white;
            }

        .button6 {
            background-color: white;
            color: black;
            border: 2px solid #008CBA;
        }

            .button6:hover {
                background-color: #008CBA;
                color: white;
            }
    </style>
    <script type="text/javascript">

        $(document).ready(function () {

            $("#Panel1").hide();

            $("#Button1").click(function () {

                $("#Panel1").show();

            });

        });
    </script>
    <script type="text/javascript">
        function preventInput(evnt) {
            //Checked In IE9,Chrome,FireFox
            if (evnt.which != 9) evnt.preventDefault();
        }
    </script>
    <script type="text/javascript" language="javascript">
        function ConfirmOnDelete() {
            if (confirm("Are you sure want to Delete This Inspection ?") == true)
                return true;
            else
                return false;
        }
    </script>
    <style type="text/css">
        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=60);
            opacity: 0.6;
        }

        .modalPopup {
            background-color: #FFFFFF;
            width: 80%;
            border: 3px solid #0DA9D0;
            border-radius: 12px;
            padding: 0;
        }

            .modalPopup .header {
                background-color: #D69758;
                height: 30px;
                color: White;
                line-height: 30px;
                text-align: center;
                font-weight: bold;
                border-top-left-radius: 6px;
                border-top-right-radius: 6px;
            }

            .modalPopup .body {
                min-height: 50px;
                line-height: 30px;
                text-align: center;
                font-weight: bold;
            }

            .modalPopup .footer {
                padding: 6px;
            }

            .modalPopup .yes, .modalPopup .no {
                height: 23px;
                color: White;
                line-height: 23px;
                text-align: center;
                font-weight: bold;
                cursor: pointer;
                border-radius: 4px;
            }

            .modalPopup .yes {
                background-color: #2FBDF1;
                border: 1px solid #0DA9D0;
            }

            .modalPopup .no {
                background-color: #9F9F9F;
                border: 1px solid #5C5C5C;
            }
    </style>
    <style>
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

        .datepicker.datepicker-dropdown.dropdown-menu.datepicker-orient-left.datepicker-orient-top {
            z-index: 9999 !important;
        }
    </style>
    <!-- Bootstrap DatePicker CSS -->
    <link href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.9.0/css/bootstrap-datepicker.min.css" rel="stylesheet" />
    <!-- Bootstrap DatePicker JS -->
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.9.0/js/bootstrap-datepicker.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.datepicker').datepicker({
                format: 'dd/mm/yyyy',
                autoclose: true,
                todayHighlight: true,

            });
        });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <asp:Label ID="lblMsg" runat="server"></asp:Label>
        <fieldset>
            <legend style="text-align: center">RM To RM Transfer Entry</legend>
            <div class="row">
                <div class="col-md-2">
                    <label>Opening Date :<i style="color: red;">*</i></label>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:RequiredFieldValidator ToolTip="Enter Opening Date" ControlToValidate="txt_OpeningDate"
                            ID="RequiredFieldValidator23" CssClass="fa-pull-right" runat="server" ForeColor="Red"
                            ValidationGroup="a" Display="Dynamic" Text="<i class='fa fa-exclamation-circle'></i>"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txt_OpeningDate" data-provide="datepicker" data-date-end-date="0d" placeholder="dd/mm/yyyy" data-date-format="dd/mm/yyyy" autocomplete="off"
                            data-date-autoclose="true" CssClass="tb6 form-control " Height="28px" Width="222px" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-2">
                    <label>Region :<i style="color: red;">*</i></label>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:RequiredFieldValidator ID="rfv1" ValidationGroup="a"
                            ErrorMessage="Select Region" ToolTip="Select Region" Text="<i class='fa fa-exclamation-circle' title='Select Region !'></i>"
                            ControlToValidate="ddlregion" InitialValue="0" CssClass="fa fa-pull-right" ForeColor="Red" Display="Dynamic" runat="server">
                        </asp:RequiredFieldValidator>
                        <asp:DropDownList ID="ddlregion" runat="server" Width="222px"
                            Height="28px" Font-Bold="true" ForeColor="Navy" CssClass="tb6 form-control" AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-2">
                    <label>Insecticide Name :<i style="color: red;">*</i></label>
                </div>
                <div class="col-md-2" valign="middle">
                    <div class="form-group">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                            ErrorMessage="Select Insecticide" ToolTip="Select Insecticide" Text="<i class='fa fa-exclamation-circle' title='Select Insecticide !'></i>"
                            ControlToValidate="ddlinsecticide" InitialValue="0" CssClass="fa fa-pull-right" ForeColor="Red" Display="Dynamic" runat="server">
                        </asp:RequiredFieldValidator>
                        <asp:DropDownList ID="ddlinsecticide" Width="222px" Height="28px" Class="form-control" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlinsecticide_SelectedIndexChanged">
                            <asp:ListItem Value="0">Select</asp:ListItem>
                            <asp:ListItem Value="1">Alluminium Phosphide</asp:ListItem>
                            <asp:ListItem Value="2">Malathion</asp:ListItem>
                            <asp:ListItem Value="3">Deltamethrin</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <label>Unit :<i style="color: red;">*</i></label>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a"
                            ErrorMessage="Select Unit Stock" ToolTip="Select Unit Stock" Text="<i class='fa fa-exclamation-circle' title='Select Insecticide !'></i>"
                            ControlToValidate="ddlUnitStock" InitialValue="0" CssClass="fa fa-pull-right" ForeColor="Red" Display="Dynamic" runat="server">
                        </asp:RequiredFieldValidator>
                        <asp:DropDownList ID="ddlUnitStock" Width="222px" Height="28px" Class="form-control" runat="server">
                            <asp:ListItem Value="0">Select</asp:ListItem>
                            <asp:ListItem Value="1">KiloGram(KG)</asp:ListItem>
                            <asp:ListItem Value="2">Litter</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-2">
                    <label>Quantity :<i style="color: red;">*</i></label>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:TextBox runat="server" Width="222px" onkeypress="return isNumber()" Height="28px" AutoComplete="off" class="form-control" ID="txtQuantity"
                            placeholder="Quantity" AutoPostBack="true" OnTextChanged="txtQuantity_TextChanged" />
                    </div>
                </div>
                <div class="col-md-2">
                    <label>Market Rate :<i style="color: red;">*</i></label>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:RequiredFieldValidator ToolTip="Enter Bill Number" ErrorMessage="Enter Bill Number" ControlToValidate="txtMarketRed" ID="rfveBillno" CssClass="fa-pull-right" runat="server" ForeColor="Red" ValidationGroup="a" Display="Dynamic" Text="<i class='fa fa-exclamation-circle'></i>"> </asp:RequiredFieldValidator>
                        <asp:TextBox runat="server" onkeypress="return isNumber()" Width="222px" Height="28px" AutoComplete="off" class="form-control" ID="txtMarketRed" placeholder="Market Rate" OnTextChanged="txtMarketRed_TextChanged" AutoPostBack="true" />
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <label>Total Value :<i style="color: red;">*</i></label>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:TextBox runat="server" onkeypress="return isNumber()" ReadOnly="true" Width="222px" Height="28px" AutoComplete="off" class="form-control" ID="txtvalue" placeholder="Value" OnTextChanged="txtMarketRed_TextChanged" AutoPostBack="true" />
                    </div>
                </div>
                <div class="col-md-2">
                    <label>Expriy Date :<i style="color: red;">*</i></label>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:RequiredFieldValidator ToolTip="Enter Expriy Date" ControlToValidate="txtExpiry"
                            ID="RequiredFieldValidator3" CssClass="fa-pull-right" runat="server" ForeColor="Red"
                            ValidationGroup="a" Display="Dynamic" Text="<i class='fa fa-exclamation-circle'></i>"> </asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtExpiry" data-provide="datepicker" placeholder="dd/mm/yyyy" data-date-format="dd/mm/yyyy" autocomplete="off"
                            data-date-autoclose="true" CssClass="tb6 form-control " Height="28px" Width="222px" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-2">
                    <label>Total Opening Balance :<i style="color: red;">*</i></label>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <%-- <asp:TextBox runat="server" onkeypress="return isNumber()" ReadOnly="true" Width="222px" Height="28px" AutoComplete="off" class="form-control" ID="txtopeningblc" placeholder="Fill Openning Balance" />--%>
                        <asp:Label ID="lblopeningblc" class="form-control" Width="222px" Height="28px" runat="server" Text="Total Value"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <label>Ro Invoice NO :<i style="color: red;">*</i></label>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:TextBox runat="server" onkeypress="return isNumber()" Width="222px" Height="28px" AutoComplete="off" class="form-control" ID="txtInvoiceNo" placeholder="Fill Invoice No" />
                    </div>
                </div>
                <div class="col-md-2">
                    <label>Total remainning Balance :<i style="color: red;">*</i></label>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <%-- <asp:TextBox runat="server" onkeypress="return isNumber()" Width="222px" ReadOnly="true" Height="28px" AutoComplete="off" class="form-control" ID="txtremaining" placeholder="Fill Remainning Blance" />--%>
                        <asp:Label ID="lblremaining" class="form-control" Width="222px" Height="28px" runat="server" Text="Total remainning Balance"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-4"></div>
                <div class="col-md-2">
                    <asp:Button class="button button6" ID="btnSubmit" ValidationGroup="a" runat="server" Text="Submit"
                        TabIndex="11" Width="222px" Height="30px" OnClick="btnSubmit_Click"></asp:Button>
                </div>
                <div class="col-md-2">
                    <asp:Button class="button button6" ID="btnClear" runat="server" Text="Clear"
                        TabIndex="11" Width="222px" Height="30px" OnClick="btnClear_Click"></asp:Button>
                </div>
            </div>
        </fieldset>
        <fieldset>
            <legend>Details</legend>
            <div class="row">
                <div class="col-12">
                    <asp:GridView ID="GVRMtransfer" CssClass="table table-bordered table-hover datatable" DataKeyNames="id"
                        OnRowCommand="GVRMtransfer_RowCommand" AutoGenerateColumns="false" runat="server">
                        <HeaderStyle BackColor="#D69758" Font-Italic="false" ForeColor="Snow" />
                        <Columns>
                            <asp:TemplateField HeaderText="S.No." ItemStyle-Width="3%" HeaderStyle-BackColor="LightBlue" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdnid" runat="server" Value='<%# Bind("id") %>' />
                                    <%-- <asp:Label ID="lblRowNumber" runat="server" <%# Container.DataItemIndex + 1 %> Text='<%#Eval("id") %>'></asp:Label>--%>
                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' ToolTip='<%# Eval("id").ToString()%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Opening Date" HeaderStyle-BackColor="LightBlue" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Openingdate" runat="server" Text='<%#Eval("Opening_Date") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Region" HeaderStyle-BackColor="LightBlue" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblRegion_ID" Text='<%# Eval("Region_ID") %>' Visible="false"></asp:Label>
                                    <asp:Label runat="server" ID="lblRegionnm" Text='<%# Eval("Regionnm") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Region From">
                                <ItemTemplate>
                                    <asp:Label ID="lblRegion" runat="server" Text='<%#Eval("transfer_from") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Insecticide Name" HeaderStyle-BackColor="LightBlue" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblInsecticide_ID" Text='<%# Eval("Insecticide_ID") %>' Visible="false"></asp:Label>
                                    <asp:Label runat="server" ID="lblInsecticide_Name" Text='<%# Eval("Insecticide_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unit" HeaderStyle-BackColor="LightBlue" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblUnit_id" Text='<%# Eval("Unit") %>' Visible="false"></asp:Label>
                                    <asp:Label runat="server" ID="lblUnit_Name" Text='<%# Eval("Unit_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Quantity" HeaderStyle-BackColor="LightBlue" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblOpeningBalance_quantity" Text='<%# Eval("Opening_Balance_quantity") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Market Rate" HeaderStyle-BackColor="LightBlue" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblOpeningBalance_marketvalue" Text='<%# Eval("Opening_Balance_market_value") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total Value" HeaderStyle-BackColor="LightBlue" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblTotal_Value" Text='<%# Eval("Opening_Balance_value") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Expriy Date" HeaderStyle-BackColor="LightBlue" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblDate_Expriy" Text='<%# Eval("Date_Expriy") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Ro Invoice NO" HeaderStyle-BackColor="LightBlue" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblRoInvoice_NO" Text='<%# Eval("InvoiceNo") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Action" HeaderStyle-BackColor="LightBlue" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Button ID="btnRemove" Text="Remove" runat="server" CssClass="btn btn-danger" CommandName="RemoveRow" CommandArgument='<%# Container.DataItemIndex %>' OnClientClick="return confirm('Do you want to Remove this row?');" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>No Record Found</EmptyDataTemplate>
                    </asp:GridView>
                </div>
            </div>
        </fieldset>
    </div>
</asp:Content>

