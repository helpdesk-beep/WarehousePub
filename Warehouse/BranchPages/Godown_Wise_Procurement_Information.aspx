<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="~/BranchPages/Godown_Wise_Procurement_Information.aspx.cs" Inherits="BranchPages_Godown_Wise_Procurement_Information" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../assets/css/style.css" rel="stylesheet" />
    <!-- font awesome -->
    <link href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../assets/New/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assets/New/css/bootstrap.min2.css" rel="stylesheet" />
    <link href="../CSS/style.css" rel="stylesheet" />
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />
    <script type="text/javascript" src='https://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.8.3.min.js'></script>
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
    <script type="text/javascript">
        $(document).ready(function () {
            $('#<%= txtDate.ClientID %>').datepicker({
                format: 'dd/mm/yyyy', // Change format as needed
                endDate: '0d'           // Disable future dates (today is the max allowed date)
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <fieldset style="border: 1px solid navy; border-radius: 10px 10px 10px 10px;">
            <legend>Godown Wise Procurement Details</legend>
            <div style="width: 100%; background-repeat: no-repeat; background-position: center;">
                <div style="position: relative; border-bottom: 3px solid black;">
                    <p style="text-align: start;">
                        <strong style="color: red">* कृपया दी गयी मात्रा को क्विंटल में दर्ज करे !!!
                        </strong>
                        <br />
                    </p>
                </div>
            </div>
            <div class="row" style="margin-top: 10px">
                <div class="col-md-2" style="margin-top: 5px">
                    <label>Godown Type :</label>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <span class="fa-pull-right">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ValidationGroup="a"
                                ErrorMessage="Select Godown Type" ToolTip="Select Godown Type" Text="<i class='fa fa-exclamation-circle' title='Select Godown Type !'></i>"
                                ControlToValidate="ddlgodowntype" InitialValue="0" CssClass="fa fa-pull-right" ForeColor="Red" Display="Dynamic" runat="server">
                            </asp:RequiredFieldValidator>
                        </span>
                        <asp:DropDownList CssClass="form-control select2" ID="ddlgodowntype" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlgodowntype_SelectedIndexChanged">
                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-2" style="margin-top: 5px">
                    <label>Godown Name :</label>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <span class="fa-pull-right">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="a"
                                ErrorMessage="Select Godown Name" ToolTip="Select Godown Name" Text="<i class='fa fa-exclamation-circle' title='Select Godown Name !'></i>"
                                ControlToValidate="ddlGodown" InitialValue="0" CssClass="fa fa-pull-right" ForeColor="Red" Display="Dynamic" runat="server">
                            </asp:RequiredFieldValidator>
                        </span>
                        <asp:DropDownList CssClass="form-control select2" ID="ddlGodown" AutoPostBack="true" runat="server">
                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-2" style="margin-top: 5px;">
                    <label>Commodity</label>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <span class="fa-pull-right">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ValidationGroup="a"
                                ErrorMessage="Select Commodity" ToolTip="Select Commodity" Text="<i class='fa fa-exclamation-circle' title='Select Godown Name !'></i>"
                                ControlToValidate="ddlcommodity" InitialValue="0" CssClass="fa fa-pull-right" ForeColor="Red" Display="Dynamic" runat="server">
                            </asp:RequiredFieldValidator>
                        </span>
                        <asp:DropDownList ID="ddlcommodity" runat="server" CssClass="form-control" AutoPostBack="true" selectionmode="Multiple">
                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2" style="margin-top: 5px">
                    <label>Date :</label>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <span class="fa-pull-right">
                            <asp:RequiredFieldValidator runat="server" CssClass="fa fa-pull-right" ID="RequiredFieldValidator9" Display="Dynamic" ControlToValidate="txtDate" ValidationGroup="a" Text="<i class='fa fa-exclamation-circle'></i>" ToolTip="Please Select Date" ErrorMessage="Please Select Date" ForeColor="Red"></asp:RequiredFieldValidator>
                        </span>
                        <asp:TextBox ID="txtDate" runat="server" placeholder="dd/mm/yyyy"
                            CssClass="form-control dateAdd" data-date-end-date="0d" autocomplete="off" data-provide="datepicker"
                            onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row" style="margin-top: 10px">
                <fieldset>
                    <legend>आज दिनांक को गोदाम में भंडारित मात्रा</legend>
                    <div class="col-md-3" style="margin-top: 5px">
                        <label>आज दिनांक को गोदाम में भंडारित किये गये बोरे :</label>
                    </div>
                    <div class="col-md-2">
                        <div class="form-group">
                            <span class="fa-pull-right">
                                <asp:RequiredFieldValidator runat="server" CssClass="fa fa-pull-right" ID="RequiredFieldValidator6" Display="Dynamic" ControlToValidate="txttodaypurchaseBore" ValidationGroup="a" Text="<i class='fa fa-exclamation-circle'></i>" ToolTip="आज दिनांक को ख़रीदे गये बोरे" ErrorMessage="आज दिनांक को भंडारित किये गये गये बोरे" ForeColor="Red"></asp:RequiredFieldValidator>
                            </span>
                            <asp:TextBox runat="server" ID="txttodaypurchaseBore" CssClass="form-control" onkeypress="return isNumber()" AutoComplete="off"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-3" style="margin-top: 5px">
                        <label>आज दिनांक को गोदाम में भंडारित किये गये बोरे का वजन :</label>
                    </div>
                    <div class="col-md-2">
                        <div class="form-group">
                            <span class="fa-pull-right">
                                <asp:RequiredFieldValidator runat="server" CssClass="fa fa-pull-right" ID="RequiredFieldValidator10" Display="Dynamic" ControlToValidate="txttodaypurchaseWeight" ValidationGroup="a" Text="<i class='fa fa-exclamation-circle'></i>" ToolTip="आज दिनांक को भंडारित किये गये बोरे का वजन" ErrorMessage="आज दिनांक को भंडारित किये गये बोरे का वजन" ForeColor="Red"></asp:RequiredFieldValidator>
                            </span>
                            <asp:TextBox runat="server" ID="txttodaypurchaseWeight" CssClass="form-control" onkeypress="return isNumber()" AutoComplete="off"></asp:TextBox>
                        </div>
                    </div>
                </fieldset>
            </div>
            <fieldset>
                <div class="row" style="margin-top: 10px">
                    <div class="col-md-2" style="margin-top: 5px">
                        <label>आज दिनांक को जारी स्वीक्रति पत्रक :</label>
                    </div>
                    <div class="col-md-2">
                        <div class="form-group">
                            <span class="fa-pull-right">
                                <asp:RequiredFieldValidator runat="server" CssClass="fa fa-pull-right" ID="RequiredFieldValidator4" Display="Dynamic" ControlToValidate="txtaccepatance" ValidationGroup="a" Text="<i class='fa fa-exclamation-circle'></i>" ToolTip="आज दिनांक को जारी स्वीक्रति पत्रक" ErrorMessage="आज दिनांक को जारी स्वीक्रति पत्रक" ForeColor="Red"></asp:RequiredFieldValidator>
                            </span>
                            <asp:TextBox runat="server" ID="txtaccepatance" onkeypress="return isNumber()" CssClass="form-control" AutoComplete="off"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-1" style="margin-top: 5px">
                        <label>बोरे :</label>
                    </div>
                    <div class="col-md-1">
                        <div class="form-group">
                            <span class="fa-pull-right">
                                <asp:RequiredFieldValidator runat="server" CssClass="fa fa-pull-right" ID="RequiredFieldValidator3" Display="Dynamic" ControlToValidate="txtBore" ValidationGroup="a" Text="<i class='fa fa-exclamation-circle'></i>" ToolTip="Please Enter Bore" ErrorMessage="Please Enter Total Bore" ForeColor="Red"></asp:RequiredFieldValidator>
                            </span>
                            <asp:TextBox runat="server" ID="txtBore" onkeypress="return isNumber()" CssClass="form-control" AutoComplete="off"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-1" style="margin-top: 5px">
                        <label>वजन :</label>
                    </div>
                    <div class="col-md-1">
                        <div class="form-group">
                            <span class="fa-pull-right">
                                <asp:RequiredFieldValidator runat="server" CssClass="fa fa-pull-right" ID="RequiredFieldValidator2" Display="Dynamic" ControlToValidate="txtweight" ValidationGroup="a" Text="<i class='fa fa-exclamation-circle'></i>" ToolTip="Please Enter Weight" ErrorMessage="Please Enter Total Weight" ForeColor="Red"></asp:RequiredFieldValidator>
                            </span>
                            <asp:TextBox runat="server" ID="txtweight" onkeypress="return isNumber()" CssClass="form-control" AutoComplete="off"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </fieldset>
            <div class="row" style="margin-top: 10px">
                <fieldset>
                    <legend>आज दिनांक तक गोदाम में भंडारित टोटल मात्रा</legend>
                    <div class="row" style="margin-top: 10px">
                        <div class="col-md-3" style="margin-top: 5px">
                            <label>आज दिनांक तक गोदाम में भंडारित टोटल बोरे :</label>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <span class="fa-pull-right">
                                    <asp:RequiredFieldValidator runat="server" CssClass="fa fa-pull-right" ID="RequiredFieldValidator1" Display="Dynamic" ControlToValidate="txttotalbore" ValidationGroup="a" Text="<i class='fa fa-exclamation-circle'></i>" ToolTip="Please Enter Total Bore" ErrorMessage="Please Enter Total Bore" ForeColor="Red"></asp:RequiredFieldValidator>
                                </span>
                                <asp:TextBox runat="server" ID="txttotalbore" CssClass="form-control" onkeypress="return isNumber()" AutoComplete="off"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3" style="margin-top: 5px">
                            <label>आज दिनांक तक गोदाम में भंडारित टोटल वजन :</label>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <span class="fa-pull-right">
                                    <asp:RequiredFieldValidator runat="server" CssClass="fa fa-pull-right" ID="RequiredFieldValidator5" Display="Dynamic" ControlToValidate="txttotalweight" ValidationGroup="a" Text="<i class='fa fa-exclamation-circle'></i>" ToolTip="Please Enter Total Weight" ErrorMessage="Please Enter Total Weight" ForeColor="Red"></asp:RequiredFieldValidator>
                                </span>
                                <asp:TextBox runat="server" ID="txttotalweight" CssClass="form-control" onkeypress="return isNumber()" AutoComplete="off"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </fieldset>
            </div>
            <div class="row">
                <div class="col-md-5"></div>
                <div class="col-md-2">
                    <asp:Button runat="server" ID="btnsave" CssClass="btn btn-info btn-block" ValidationGroup="a" Text="SUBMIT" OnClick="btnsave_Click" />
                </div>
            </div>
        </fieldset>
        <div class="row" style="align-content: center" runat="server" id="Div1" visible="false">
            <div class="col-md-12">
                <fieldset>
                    <legend>Details</legend>
                    <div class="table-responsive">
                        <asp:GridView runat="server" ID="grdwhr"
                            CssClass="table table-bordered table-hover datatable" AutoGenerateColumns="False" OnRowCommand="grdwhr_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                        <asp:HiddenField ID="hdnGodown_Id" runat="server" Value='<%# Bind("Godown_Id") %>' />
                                        <asp:HiddenField ID="hdnID" runat="server" Value='<%# Bind("ID") %>' />
                                        <asp:HiddenField ID="hdnCommodity_ID" runat="server" Value='<%# Bind("Commodity_ID") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Godown Type" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblGodown_Type" Text='<%# Eval("Godown_Type") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Godown Name" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblGodown_Name" Text='<%# Eval("Godown_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Commodity Name" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblCommodity_Name" Text='<%# Eval("Commodity_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date" HeaderStyle-BackColor="LightBlue" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblProcurement_Date" Text='<%# Eval("Procurement_Date") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="आज दिनांक को गोदाम में भंडारित किये गये बोरे" HeaderStyle-BackColor="LightBlue" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblGodam_Bhandarit_Bore" Text='<%# Eval("Godam_Bhandarit_Bore") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="आज दिनांक को गोदाम में भंडारित किये गये बोरे का वजन" HeaderStyle-BackColor="LightBlue" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblGodam_me_Bhandarit_Bore_Ka_Weight" Text='<%# Eval("Godam_me_Bhandarit_Bore_Ka_Weight") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="आज दिनांक को जारी स्वीक्रति पत्रक" HeaderStyle-BackColor="LightBlue" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblToday_date_Acceptance" Text='<%# Eval("Today_date_Acceptance") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="आज दिनांक को जारी बोरे" HeaderStyle-BackColor="LightBlue" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblAcceptance_Wise_Bore" Text='<%# Eval("Acceptance_Wise_Bore") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="आज दिनांक को जारी वजन" HeaderStyle-BackColor="LightBlue" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblAcceptance_Wise_Bore_Ka_Weight" Text='<%# Eval("Acceptance_Wise_Bore_Ka_Weight") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="आज दिनांक तक गोदाम में भंडारित टोटल बोरे" HeaderStyle-BackColor="LightBlue" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblTotal_Bore" Text='<%# Eval("Total_Bore") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="आज दिनांक तक गोदाम में भंडारित टोटल वजन" HeaderStyle-BackColor="LightBlue" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblTotal_Weight" Text='<%# Eval("Total_Weight") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remove">
                                    <ItemTemplate>
                                        <asp:Button ID="btnRemove" Text="Remove" runat="server" CommandName="RemoveRow" CssClass="btn-danger" CommandArgument='<%# Container.DataItemIndex %>' OnClientClick="return confirm('Do you want to Remove this row?');" />
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
</asp:Content>

