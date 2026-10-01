<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/StateMasterNafed_Marketing.master" AutoEventWireup="true" CodeFile="~/Accounting/Crop_Year_Date_Wise_Approval_Report.aspx.cs" Inherits="Accounting_Crop_Year_Date_Wise_Approval_Report" %>

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
            //   printWindow.document.write('<html><head><title>WHR</title>');
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
            //   printWindow.document.write('<html><head><title>WHR</title>');
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

        .custom-pager a, .custom-pager span {
            margin: 0 5px; /* Horizontal gap between numbers */
            padding: 5px 10px;
            text-decoration: none;
            background-color: #f0f0f0;
            border-radius: 5px;
            color: black;
        }

        .custom-pager span { /* Current page */
            font-weight: bold;
            background-color: #007bff;
            color: white;
        }
    </style>
    <script type="text/javascript" src='<%= ResolveUrl("~/NEW_CSS/js/jquery-1.12.4.js") %>'></script>
    <%-- <script type="text/javascript" src='https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/5.3.2/js/bootstrap.bundle.min.js'></script>--%>
    <%--<link rel="stylesheet" href='https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/5.3.2/css/bootstrap.min.css' />--%>
    <!-- Bootstrap -->
    <!-- Bootstrap DatePicker -->
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
    <div class="content-wrapper">
        <fieldset style="border: 1px solid navy; border-radius: 10px 10px 10px 10px;">
            <legend>Crop Year/Date Wise Bills Approve By Marketing Manager</legend>
            <div class="row">
                <div class="col-md-2">
                    <div class="form-group">
                        <label style="margin-top: 10px">Commodity</label>
                        <span class="fa-pull-right"></span>
                        <asp:DropDownList CssClass="form-control select2" ID="ddlcommodity" runat="server">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-2">
                    <div class="form-group ">
                        <label style="margin-top: 10px">Crop Year</label>
                        <asp:DropDownList CssClass="form-control" ID="ddlcropyear" runat="server">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <label style="margin-top: 10px">Date</label>
                        <asp:TextBox ID="txtDate" runat="server" placeholder="dd/mm/yyyy"
                            CssClass="form-control dateAdd" data-date-end-date="0d" autocomplete="off" data-provide="datepicker"
                            onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-2">
                    <div class="col-md-2" style="text-align: center; margin-top: 35px">
                        <asp:Button runat="server" CssClass="btn btn-success btn-sm" Text="Search" ID="btnSearch" OnClick="btnSearch_Click" autopostback="true" />
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
                                <asp:Button ID="Button2" runat="server" Text="ExportToPDF" CssClass="button button2" OnClientClick="printGrid()" />
                                &nbsp;&nbsp;&nbsp;&nbsp;
                                 <input type="button" id="btnExport" value="Exporttoexcel" class="button button2" />
                            </div>
                            <div class="col-md-2" style="margin-top: 5px">
                                <asp:Label runat="server" ID="lbltotalcount">Total Count</asp:Label>
                            </div>
                            <div class="col-md-2">
                                <asp:TextBox ID="txtcount" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-2" style="margin-top: 5px">
                                <asp:Label runat="server" ID="lbltotalamount">Total Amount</asp:Label>
                            </div>
                            <div class="col-md-2">
                                <asp:TextBox ID="lblAmount" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row" style="margin-top: 10px; margin-bottom: 10px;">
                        <div class="col-md-7"></div>
                        <div class="col-md-1">
                            <label runat="server" id="Label1">Bill Number</label>
                        </div>
                        <div class="col-md-2">
                            <asp:TextBox ID="txtBillNumber" runat="server" AutoComplete="off" Placeholder="Enter Bill Number" />
                        </div>
                        <div class="col-md-2">
                            <asp:Button ID="btnSearch1" runat="server" Text="Search" OnClick="btnSearch_Click1" />
                        </div>
                    </div>
                    <div class="row" runat="server" id="showbill" visible="false">
                        <div class="table-responsive">
                            <asp:GridView runat="server" DataKeyNames="Bill_Number" ID="GrdBills"
                               CssClass="table table-bordered table-hover datatable" AutoGenerateColumns="False"
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
                                            <asp:Label runat="server" ID="lblRegion_Name" Text='<%# Eval("Regionnm") %>'></asp:Label>
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
                                    <asp:TemplateField HeaderText="Approved Date" HeaderStyle-BackColor="LightBlue">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblApproved_Date" Text='<%# Eval("Approved_Date") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>No Record Found</EmptyDataTemplate>
                                                          </asp:GridView>
                        </div>
                    </div>
                    <div class="Row" id="showsearch" runat="server" visible="false">
                        <div class="table-responsive">
                            <asp:GridView runat="server" DataKeyNames="Bill_Number" ID="Gridsearch"
                                CssClass="table table-bordered table-hover datatable" AutoGenerateColumns="False"
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
                                            <asp:Label runat="server" ID="lblRegion_Name" Text='<%# Eval("Regionnm") %>'></asp:Label>
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
                                    <asp:TemplateField HeaderText="Approved Date" HeaderStyle-BackColor="LightBlue">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblApproved_Date" Text='<%# Eval("Approved_Date") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </fieldset>
            </div>
        </div>
    </div>
    <script type="text/javascript" src="<%= ResolveUrl("~/NEW_CSS/js/jquery-1.12.4.js") %>"></script>
    <script src="../../JS/table2excel.js"></script>
    <script type="text/javascript">
        $("body").on("click", "#btnExport", function () {
            $("[id*=GrdBills]").table2excel({
                filename: "Nafed_Print_Bill_Summary.xls"
            });
        });
    </script>
</asp:Content>

