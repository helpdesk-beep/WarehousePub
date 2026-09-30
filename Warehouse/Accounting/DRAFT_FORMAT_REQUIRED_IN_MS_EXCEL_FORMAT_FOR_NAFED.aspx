<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/StateMasterNafed.master" AutoEventWireup="true" CodeFile="~/Accounting/DRAFT_FORMAT_REQUIRED_IN_MS_EXCEL_FORMAT_FOR_NAFED.aspx.cs" Inherits="Accounting_DRAFT_FORMAT_REQUIRED_IN_MS_EXCEL_FORMAT_FOR_NAFED" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../assets/css/style.css" rel="stylesheet" />
    <!-- Bootstrap core CSS -->
    <link href="../assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/css/bootstrap-theme.min.css" rel="stylesheet" type="text/css" />

    <!-- font awesome -->
    <link href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" />

    <link href="../assets/css/style.css" rel="stylesheet" type="text/css" />
    <link href="../assets/css/custome.css" rel="stylesheet" type="text/css" />
    <link href="../assets/New/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assets/New/css/bootstrap.min2.css" rel="stylesheet" />
    <%-- <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />
    <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />--%>
    <link href="../CSS/style.css" rel="stylesheet" />
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />
    <script type="text/javascript">
        function PrintDiv() {
            var divContents = document.getElementById("PrintDiv").innerHTML;
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

    <style>
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
    </style>
    <div class="content-wrapper">
        <fieldset style="border: 1px solid navy; border-radius: 10px 10px 10px 10px;">
            <legend>DRAFT FORMAT View and Download IN MS EXCEL </legend>
            <div class="row">
                <div class="col-md-2">
                    <div class="form-group">
                        <label style="margin-top: 10px">Commodity</label>
                        <span class="fa-pull-right">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a" ToolTip="Select Commodity"
                                ErrorMessage="Select Commodity" InitialValue="0" ForeColor="Red"
                                Text="<i class='fa fa-exclamation-circle'></i>"
                                ControlToValidate="ddlcommodity" Display="Dynamic" runat="server">
                            </asp:RequiredFieldValidator>
                        </span>
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
                    <div class="form-group ">
                        <label style="margin-top: 10px">District Name</label>
                        <asp:DropDownList CssClass="form-control" ID="ddldistrict" runat="server" OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem Value="0">All</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-2">
                    <div class="form-group ">
                        <label style="margin-top: 10px">Branch Name</label>
                        <asp:DropDownList CssClass="form-control select2" ID="ddlbranch" runat="server">
                            <asp:ListItem Value="0">All</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <label style="margin-top: 10px">Financial Year</label>
                        <span class="fa-pull-right">
                            <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="a" ToolTip="Select Financial Year"
                                ErrorMessage="Select Financial Year " InitialValue="0" ForeColor="Red"
                                Text="<i class='fa fa-exclamation-circle'></i>"
                                ControlToValidate="ddlFinancialyear" Display="Dynamic" runat="server">
                            </asp:RequiredFieldValidator>--%>
                        </span>
                        <%--<asp:TextBox ID="txtFinancialYear" runat="server" CssClass="form-control" ReadOnly="true" Font-Size="12px" Font-Bold="true" Text="2024" ForeColor="Navy"></asp:TextBox>--%>
                        <asp:DropDownList CssClass="form-control select2" ID="ddlFinancialyear" runat="server">
                            <asp:ListItem Value="0">All</asp:ListItem>
                            <asp:ListItem Value="2023">2023-24</asp:ListItem>
                            <asp:ListItem Value="2024">2024-25</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <label style="margin-top: 10px">Month</label>
                        <span class="fa-pull-right">
                            <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a" ToolTip="Select Month"
                                ErrorMessage="Select Month" InitialValue="0" ForeColor="Red"
                                Text="<i class='fa fa-exclamation-circle'></i>"
                                ControlToValidate="ddlmonth" Display="Dynamic" runat="server">
                            </asp:RequiredFieldValidator>--%>
                        </span>
                        <asp:DropDownList CssClass="form-control select2" ID="ddlmonth" runat="server">
                            <asp:ListItem Value="0">All</asp:ListItem>
                            <asp:ListItem Value="1">January</asp:ListItem>
                            <asp:ListItem Value="2">February</asp:ListItem>
                            <asp:ListItem Value="3">March</asp:ListItem>
                            <asp:ListItem Value="4">April</asp:ListItem>
                            <asp:ListItem Value="5">May</asp:ListItem>
                            <asp:ListItem Value="6">June</asp:ListItem>
                            <asp:ListItem Value="7">July</asp:ListItem>
                            <asp:ListItem Value="8">August</asp:ListItem>
                            <asp:ListItem Value="9">September</asp:ListItem>
                            <asp:ListItem Value="10">October</asp:ListItem>
                            <asp:ListItem Value="11">November</asp:ListItem>
                            <asp:ListItem Value="12">December</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

            </div>
            <div class="row">
                <div class="col-md-5"></div>
                <div class="col-md-2" style="text-align: center; margin-top: 35px">
                    <asp:Button runat="server" CssClass="btn btn-success btn-sm" ValidationGroup="a" Text="Search" ID="btnSearch" OnClick="btnSearch_Click" autopostback="true" />
                    <asp:Button runat="server" CssClass="btn button2 btn-sm" Text="Exporttoexcel" ID="btnExport" OnClick="btnExport_Click" />
                </div>
            </div>
        </fieldset>
        <div class="row" style="align-content: center" runat="server" id="grdbill" visible="false">
            <div class="col-md-12">
                <fieldset>
                    <legend>Bills Details</legend>
                    <div class="table-responsive">
                        <%--  <div class="row">
                            <div class="col-md-6">
                                <asp:Label ID="Label18" runat="server" Font-Size="12pt" ForeColor="Blue"
                                    Text="भंडारण शुल्क बिल का प्रिन्ट लेने के लिये दी गई View बटन पे क्लिक करें"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <asp:DropDownList ID="lblBill_Number" runat="server"
                                    Height="25px" Width="200px" AutoPostBack="true" Visible="false">
                                </asp:DropDownList>
                            </div>
                        </div>--%>
                        <asp:GridView runat="server" ID="GrdBills" ShowFooter="true"
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
                            </Columns>
                            <EmptyDataTemplate>No Record Found</EmptyDataTemplate>
                        </asp:GridView>
                        <div style="width: 100%; background-repeat: no-repeat; background-position: center;">
                            <p>
                                <strong style="color: red; font-size: medium;">*Bills have been checked & processed
                                </strong>
                                <br />
                            </p>
                        </div>
                    </div>
                </fieldset>
            </div>
        </div>
    </div>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="../JS/table2excel.js"></script>
    <script type="text/javascript">
        $("body").on("click", "#btnExport", function () {
            $("[id*=GrdBills]").table2excel({
                filename: "NAFEDDRAFFORMATE.xls"
            });
        });
    </script>
</asp:Content>

