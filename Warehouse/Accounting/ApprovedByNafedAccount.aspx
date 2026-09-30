<%@ Page Title="" Language="C#" MasterPageFile="~/Warehouse/MasterPage/StateMasterNafed.master" AutoEventWireup="true" CodeFile="~/Accounting/ApprovedByNafedAccount.aspx.cs" Inherits="Accounting_ApprovedByNafedAccount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../assets/css/style.css" rel="stylesheet" />
    <!-- font awesome -->
    <link href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../assets/New/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assets/New/css/bootstrap.min2.css" rel="stylesheet" />
    <link href="../CSS/style.css" rel="stylesheet" />

    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />
    <script type="text/javascript">
        function PrintDiv() {
            var divContents = document.getElementById("PrintDiv").innerHTML;
            var printWindow = window.open('', '', 'height=700,width=1000');
            //printWindow.document.write('<html><head><title>WHR</title>');
            printWindow.document.write('</head><body >');
            printWindow.document.write(divContents);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            printWindow.print();
            printWindow.close();
        }
    </script>
    <script type="text/javascript">
        function PrintDiv_det() {
            var divContents = document.getElementById("PrintDiv_Det").innerHTML;
            var printWindow = window.open('', '', 'height=700,width=1000');
            printWindow.document.write('</head><body >');
            printWindow.document.write(divContents);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            printWindow.print();
            printWindow.close();
        }
    </script>
    <script type="text/javascript">
        function PrintDiv_Actual() {
            var divContents = document.getElementById("printActualBill").innerHTML;
            var printWindow = window.open('', '', 'height=700,width=1000');
            printWindow.document.write('</head><body >');
            printWindow.document.write(divContents);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            printWindow.print();
            printWindow.close();
        }
    </script>
    <script type="text/javascript">
        function PrintGridData() {
            var prtGrid = document.getElementById('<%=GrdBills.ClientID %>');
            prtGrid.border = 0;
            var prtwin = window.open('', 'PrintGridViewData', 'left=100,top=100,width=1000,height=1000,tollbar=0,scrollbars=1,status=0,resizable=1');
            prtwin.document.write(prtGrid.outerHTML);
            prtwin.document.close();
            prtwin.focus();
            prtwin.print();
            prtwin.close();
        }
    </script>
    <script type="text/javascript">
        function printGrid() {
            var gridData = document.getElementById('<%= GrdBills.ClientID %>');
            var windowUrl = 'about:blank';
            //set print document name for gridview
            var uniqueName = new Date();
            var windowName = 'Print_' + uniqueName.getTime();
            var prtWindow = window.open(windowUrl, windowName,
                'left=100,top=100,right=100,bottom=100,width=700,height=500');
            prtWindow.document.write('<html><head></head>');
            prtWindow.document.write('<body style="background:none !important">');
            prtWindow.document.write(gridData.outerHTML);
            prtWindow.document.write('</body></html>');
            prtWindow.document.close();
            prtWindow.focus();
            prtWindow.print();
            prtWindow.close();
        }
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

        .auto-style1 {
            height: 10px;
            width: 558px;
        }

        .auto-style2 {
            width: 558px;
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

        /* Common Style */
        .status-approved,
        .status-rejected {
            display: inline-block;
            padding: 5px 12px;
            font-size: 13px;
            font-weight: bold;
            border-radius: 20px;
            text-align: center;
            min-width: 90px;
        }

        /* Approved (Green) */
        .status-approved {
            background-color: #d4edda;
            color: #155724;
            border: 1px solid #c3e6cb;
        }

        /* Not Approved (Red) */
        .status-rejected {
            background-color: #f8d7da;
            color: #721c24;
            border: 1px solid #f5c6cb;
        }
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
    <script type="text/javascript" src='https://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.8.3.min.js'></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.6.4/css/bootstrap-datepicker.css" type="text/css" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.6.4/js/bootstrap-datepicker.js" type="text/javascript"></script>
    <!-- Bootstrap DatePicker -->
    <script type="text/javascript">
        $(function () {
            $('[id*=txtDate]').datepicker({
                format: "dd/mm/yyyy",
                language: "tr"
            });
        });
    </script>

    <script type="text/javascript">
        function filterGrid() {

            var input = document.getElementById("<%= txtSearch.ClientID %>");
            var filter = input.value.toLowerCase();

            var table = document.getElementById("<%= GrdBills.ClientID %>");
            var trs = table.getElementsByTagName("tr");

            for (var i = 1; i < trs.length; i++) { // skip header row
                var display = false;
                var tds = trs[i].getElementsByTagName("td");

                for (var j = 0; j < tds.length; j++) {
                    var cell = tds[j];
                    if (cell && cell.textContent.toLowerCase().indexOf(filter) > -1) {
                        display = true;
                        break;
                    }
                }

                trs[i].style.display = display ? "" : "none";
            }
        }
    </script>


    <script type="text/javascript">
        function ExportToExcel() {
            var grid = document.getElementById('<%= GrdBills.ClientID %>');

            if (grid) {
                // Ensure first row is header
                $(grid).find("tr").each(function (index, row) {
                    if (index === 0) {
                        $(row).find("td, th").each(function () {
                            // Convert first row to <th> for bold in Excel
                            $(this).replaceWith(function () {
                                return $("<th>").html($(this).html());
                            });
                        });
                    }
                });

                $(grid).table2excel({
                    name: "Bill Summary",
                    filename: "Nafed_Print_Bill_Summary", // plugin automatically adds .xls
                    fileext: ".xls",
                    exclude: ".noExl"
                });
            } else {
                alert("Grid not found!");
            }
        }
    </script>

    <style>
        /* Table formatting */
        .pdf-table {
            border-collapse: collapse;
            width: 100%;
        }

            .pdf-table thead {
                display: table-header-group; /* Repeat headers on each page */
            }

            .pdf-table tr {
                page-break-inside: avoid; /* Avoid cutting rows */
            }
    </style>

    <%--Update End--%>
    <div class="content-wrapper">
        <fieldset style="border: 1px solid navy; border-radius: 10px 10px 10px 10px;">
            <legend>Approved By NAFED Account</legend>
            <div class="row">
                <div class="col-md-2">
                    <div class="form-group">
                        <label style="margin-top: 10px">Date Of Approval</label>
                        <span class="fa-pull-right">
                            <asp:TextBox ID="txtDate" runat="server" placeholder="dd/mm/yyyy" ReadOnly="true"
                                CssClass="form-control dateAdd" data-date-end-date="0d" autocomplete="off" data-provide="datepicker"
                                onpaste="return false;" onkeypress="return false;" data-date-format="dd/mm/yyyy">
                            </asp:TextBox>

                        </span>
                    </div>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <label style="margin-top: 10px">Commodity</label>
                        <span class="fa-pull-right">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a" ToolTip="Select Commodity"
                                ErrorMessage="Select Commodity" InitialValue="0" ForeColor="Red"
                                Text="<i class='fa fa-exclamation-circle'></i>"
                                ControlToValidate="ddlcommodity" Display="Dynamic" runat="server"> </asp:RequiredFieldValidator>
                        </span>
                        <asp:DropDownList CssClass="form-control select2"
                            ID="ddlcommodity"
                            AutoPostBack="true"
                            OnSelectedIndexChanged="ddlcommodity_SelectedIndexChanged"
                            runat="server">
                        </asp:DropDownList>

                    </div>
                </div>

            </div>
        </fieldset>
        <div class="row" style="align-content: center" runat="server" id="grdbill" visible="false">
            <div class="col-md-12">
                <fieldset>
                    <legend>Bills Details</legend>
                    <div class="row" style="margin-bottom: 10px">
                        <div class="col-md-12">
                            <div class="col-md-4">
                                <%--<asp:Button ID="Button2" runat="server" Text="ExportToPDF" CssClass="button button2" OnClientClick="printGrid()" />--%>
                                <asp:Button ID="Button2" runat="server" Text="ExportToPDF" CssClass="btn btn-success" OnClientClick="printGrid(); return false;" />
                                <asp:Button ID="btnExport" runat="server" Text="Export to Excel" OnClientClick="ExportToExcel(); return false;" CssClass="btn btn-primary" />

                                <%--<input type="button" id="btnExport" value="Exporttoexcel" class="button button2" />--%>
                            </div>
                            <div class="col-md-4"></div>
                            <div class="col-md-2" style="margin-top: 5px">
                                <label>Total Count</label>
                                <asp:TextBox CssClass="form-control" ID="txtCount" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-2" style="margin-top: 5px">
                                <label>Total Amount</label>
                                <asp:TextBox ID="lblAmount" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div>
                        <%--Search Textbox--%>
                        <div style="padding: 0px 0px 15px 0px">
                            <div colspan="10" align="left">
                                <asp:TextBox ID="txtSearch" runat="server"
                                    placeholder="Search here..."
                                    Style="width: 420px; height: 36px; padding: 0 15px; font-size: 15px; border: 1px solid #000; border-radius: 8px; outline: none; transition: all 0.25s ease; box-shadow: 0 2px 6px rgba(0,0,0,0.08);"
                                    onkeyup="filterGrid();" Visible="false" />

                            </div>
                        </div>

                        <%--Search Textbox End--%>
                    </div>
                    <div class="table-responsive">

                        <asp:GridView runat="server" DataKeyNames="Bill_Number" ID="GrdBills"
                            CssClass="table table-bordered table-hover datatable" AutoGenerateColumns="False" OnRowCommand="GrdBills_RowCommand"
                            autopostback="true">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Region Name" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblRegion_Name"
                                            Text='<%# Eval("Regionnm") %>'
                                            EnableViewState="false"> 
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="District Name" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblDistrict_Name" Text='<%# Eval("District_Name") %>'></asp:Label>
                                        <asp:HiddenField runat="server" ID="hdnDistrict_Id" Value='<%#Eval("District_Id")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Branch Name" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblBranch_Name" Text='<%# Eval("Branch_Name") %>'></asp:Label>
                                        <asp:HiddenField runat="server" ID="hdnBranch_Id" Value='<%#Eval("Branch_Id")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Godown Name" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblGodown_Name" Text='<%# Eval("Godown_Name") %>'></asp:Label>
                                        <asp:HiddenField runat="server" ID="hdnGodown_Id" Value='<%#Eval("Godown_Id")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bill Name" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblBill_Number" Text='<%# Eval("Bill_Number") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Commodity Name" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblCommodity_Name" Text='<%# Eval("Commodity_Name") %>'></asp:Label>
                                        <asp:HiddenField runat="server" ID="hdnCommodity_Id" Value='<%#Eval("Commodity_Id")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Crop Year" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblCrop_Year" Text='<%# Eval("Crop_Year") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Financial Year" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblFinancial_Year" Text='<%# Eval("Financial_Year") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Month" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblMonth" Text='<%# Eval("MonthYear") %>'></asp:Label>
                                        <asp:HiddenField runat="server" ID="hdnMonth" Value='<%#Eval("Month")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Closing Balance" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblClosing_Balance" Text='<%# Eval("Closing_Balance") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Net Amount" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblNet_Amount" Text='<%# Eval("Net_Amount") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Marketing Approval Date" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblMarketing_Approval_Date" Text='<%# Eval("Account_Approval_Date") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Account Approval Status" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblAccount_Approval_Date"
                                            CssClass='<%# Eval("Account_Approve_Stutes").ToString() == "Y" ? "status-approved" : "status-rejected" %>'
                                            Text='<%# Eval("Account_Approve_Stutes").ToString() == "Y" ? "Approved" : "Not Approved" %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%--17-09-2025--%>
                                <asp:TemplateField HeaderText="Select" HeaderStyle-BackColor="LightBlue">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkSelectAll" runat="server" AutoPostBack="true"
                                            OnCheckedChanged="chkSelectAll_CheckedChanged" Text="" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelect" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                            <EmptyDataTemplate>No Record Found</EmptyDataTemplate>
                        </asp:GridView>

                    </div>

                    <div id="exportdraftformate" runat="server" visible="false">
                        <asp:GridView runat="server" ID="GridView1" ShowFooter="true"
                            CssClass="table table-bordered table-hover datatable" AutoGenerateColumns="False" OnRowCommand="GrdBills_RowCommand" autopostback="true">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Regional Office" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblRegion_Name" Text='<%# Eval("Regionnm") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="District" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblDistrict_Name" Text='<%# Eval("District_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Branch" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblBranch_Name" Text='<%# Eval("Branch_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Godown Name" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblGodown_Name" Text='<%# Eval("Godown_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SCHEME" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblGodown_Name" Text='<%# Eval("Scheme") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="COMMODITY" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblCommodity_Name" Text='<%# Eval("Commodity_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CROP YEAR" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblCrop_Year" Text='<%# Eval("Crop_Year") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="BILL NO" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblBill_Number" Text='<%# Eval("Bill_Number") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="DATE" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblBill_Date" Text='<%# Eval("Billing_Date") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bill MONTH" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblBill_Date" Text='<%# Eval("BillMonth") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rate per Bag" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblRatePerBag" Text='<%# Eval("RatePerBag") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="BAGS (G.RENT FOR  15 DAYS (1 TO 15 DAYS )" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblStartDate" Text='<%# Eval("StartDate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="AMOUNT (RATE PER BAG FOR 15 DAYS (1 TO 15 DAYS)@3.10 PER BAG" HeaderStyle-BackColor="LightBlue" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblSTotalCharges" Text='<%# Eval("STotalCharges") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="BAGS (G.RENT FOR  15 DAYS (16 TO 30 DAYS)" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblFromDate" Text='<%# Eval("FromDate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="AMOUNT (RATE PER BAG FOR 15 DAYS (15 TO 30 DAYS" HeaderStyle-BackColor="LightBlue" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblFTotalCharges" Text='<%# Eval("FTotalCharges") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CL.BAG" HeaderStyle-BackColor="LightBlue" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblClosingBalance" Text='<%# Eval("ClosingBalance") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bill Amount" HeaderStyle-BackColor="LightBlue" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblBillAmount" Text='<%# Eval("BillAmount") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Marketing Approval Date" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblMarketing_Approval_Date" Text='<%# Eval("Marketing_Approval_Date") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>No Record Found</EmptyDataTemplate>
                        </asp:GridView>

                    </div>
                    <div id="loader" style="display: none; position: fixed; top: 0; left: 0; width: 100%; height: 100%; background-color: rgba(255, 255, 255, 0.7); z-index: 9999; text-align: center;">
                        <div style="position: absolute; top: 50%; left: 50%; transform: translate(-50%, -50%);">
                            <img src="../images/mpwlc3.gif" alt="Loading..." />
                        </div>
                    </div>

                </fieldset>
            </div>
        </div>
        <div class="row">
            <div class="col-md-5"></div>
            <div class="col-md-1">
                <%--<asp:Button ID="btnproceed" runat="server" Text="Proceed" Visible="false" CssClass="btn-success" Enabled="true" OnClick="btnproceed_Click" />--%>
            </div>
            <div class="col-md-1">
                <!-- ✅ Button for Printing Selected Bills -->
                <asp:Button ID="btnPrintSelected" runat="server" Text="Print Selected"
                    CssClass="btn btn-success" OnClick="btnPrintSelected_Click" OnClientClick="ShowLoader();" />
            </div>
        </div>
        <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
        <script src="../../JS/table2excel.js">

        </script>
        <script type="text/javascript">
            $("body").on("click", "#btnExport", function () {
                $("[id*=GrdBills]").table2excel({
                    filename: "Nafed_Print_Bill_Summary.xls"
                });
            });
        </script>

        <script type="text/javascript">
            var TotalChkBx;
            var Counter;
            window.onload = function HeaderClick(CheckBox) {
                //Get total no. of CheckBoxes in side the GridView.
                TotalChkBx = parseInt('<%= this.GrdBills.Rows.Count %>');
                //Get total no. of checked CheckBoxes in side the GridView.
                Counter = 0;
        </script>
        <script type="text/javascript">
                window.onload = function () {
                    var loader = document.getElementById('loader');
                    if (loader) {
                        loader.style.display = 'none';
                    }
                };
        </script>


        <script type="text/javascript">
                function ShowLoader() {
                    document.getElementById('loader').style.display = 'block';
                }
        </script>

        <script type="text/javascript">
                function fnChkEmpty() {
                    var dateValue = document.getElementById("<%= txtDate.ClientID %>").value;
) {
                        alert("Please Select the date.");
                    }
                }
        </script>
    </div>
</asp:Content>

