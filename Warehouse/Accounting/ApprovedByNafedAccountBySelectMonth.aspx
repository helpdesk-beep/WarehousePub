<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/StateMasterNafed.master" AutoEventWireup="true" CodeFile="ApprovedByNafedAccountBySelectMonth.aspx.cs" Inherits="Accounting_ApprovedByNafedAccountBySelectMonth" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../assets/css/style.css" rel="stylesheet" />
    <link href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../assets/New/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assets/New/css/bootstrap.min2.css" rel="stylesheet" />
    <link href="../CSS/style.css" rel="stylesheet" />
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />

    <script src="<%= ResolveUrl("~/NEW_CSS/js/jquery-1.12.4.js") %>"></script>
    <script type="text/javascript">
        function PrintDiv() {
            var divContents = document.getElementById("PrintDiv").innerHTML;
            var printWindow = window.open('', '', 'height=700,width=1000');
            printWindow.document.write('<html><head><title>Print</title></head><body>');
            printWindow.document.write(divContents);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            printWindow.print();
            printWindow.close();
        }

        function PrintGridData() {
            var prtGrid = document.getElementById('<%= GrdBills.ClientID %>');
            prtGrid.border = 0;
            var prtwin = window.open('', 'PrintGridViewData', 'left=100,top=100,width=1000,height=1000,toolbar=0,scrollbars=1,status=0,resizable=1');
            prtwin.document.write('<html><head><title>Print Grid</title></head><body>');
            prtwin.document.write(prtGrid.outerHTML);
            prtwin.document.write('</body></html>');
            prtwin.document.close();
            prtwin.focus();
            prtwin.print();
            prtwin.close();
        }

        function filterGrid() {
            var input = document.getElementById("<%= txtSearch.ClientID %>");
            var filter = input.value.toLowerCase();
            var table = document.getElementById("<%= GrdBills.ClientID %>");
            var trs = table.getElementsByTagName("tr");

            for (var i = 1; i < trs.length; i++) {
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

        function ShowLoader() {
            document.getElementById('loader').style.display = 'block';
        }

        function HideLoader() {
            document.getElementById('loader').style.display = 'none';
        }
    </script>

    <style type="text/css">
        .status-approved {
            display: inline-block;
            padding: 5px 12px;
            font-size: 13px;
            font-weight: bold;
            border-radius: 20px;
            text-align: center;
            min-width: 90px;
            background-color: #d4edda;
            color: #155724;
            border: 1px solid #c3e6cb;
        }

        .status-rejected {
            display: inline-block;
            padding: 5px 12px;
            font-size: 13px;
            font-weight: bold;
            border-radius: 20px;
            text-align: center;
            min-width: 90px;
            background-color: #f8d7da;
            color: #721c24;
            border: 1px solid #f5c6cb;
        }

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

        th.sorting, th.sorting_asc, th.sorting_desc {
            background: #647e68 !important;
            color: black !important;
        }

        .loader {
            display: none;
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background-color: rgba(255, 255, 255, 0.7);
            z-index: 9999;
            text-align: center;
        }

        .loader-content {
            position: absolute;
            top: 50%;
            left: 50%;
            transform: translate(-50%, -50%);
        }
    </style>

    <style type="text/css">
    /* Custom Pager Styles */
    .custom-pager {
        background: linear-gradient(to bottom, #f8f9fa, #e9ecef);
        padding: 15px 20px;
        border-radius: 8px;
        margin-top: 20px;
        text-align: center;
        border: 1px solid #dee2e6;
        box-shadow: 0 2px 4px rgba(0,0,0,0.05);
    }
    
    .custom-pager td {
        padding: 0 !important;
        border: none !important;
        background: transparent !important;
    }
    
    .custom-pager table {
        margin: 0 auto;
        border-collapse: separate;
        border-spacing: 5px;
    }
    
    .custom-pager a, 
    .custom-pager span {
        display: inline-block;
        padding: 8px 16px;
        margin: 0 3px;
        border-radius: 6px;
        text-decoration: none;
        font-size: 14px;
        font-weight: 600;
        font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        transition: all 0.3s cubic-bezier(0.4, 0, 0.2, 1);
        cursor: pointer;
        min-width: 40px;
        text-align: center;
    }
    
    .custom-pager a {
        color: #2095A1;
        background: white;
        border: 1px solid #dee2e6;
        box-shadow: 0 1px 2px rgba(0,0,0,0.05);
    }
    
    .custom-pager a:hover {
        background: #2095A1;
        color: white;
        border-color: #2095A1;
        transform: translateY(-2px);
        box-shadow: 0 4px 12px rgba(32, 149, 161, 0.3);
    }
    
    .custom-pager span {
        background: #2095A1;
        color: white;
        border: 1px solid #2095A1;
        box-shadow: 0 2px 4px rgba(0,0,0,0.1);
        cursor: default;
    }
    
    .custom-pager a:active {
        transform: translateY(0);
    }
    
    /* Disabled state */
    .custom-pager a[disabled] {
        color: #adb5bd;
        cursor: not-allowed;
        background: #f8f9fa;
        border-color: #dee2e6;
    }
    
    .custom-pager a[disabled]:hover {
        transform: none;
        background: #f8f9fa;
        color: #adb5bd;
        box-shadow: none;
    }
    
    /* Responsive pager */
    @media (max-width: 768px) {
        .custom-pager a, 
        .custom-pager span {
            padding: 6px 12px;
            font-size: 12px;
            min-width: 32px;
        }
        
        .custom-pager {
            padding: 10px;
            overflow-x: auto;
        }
        
        .custom-pager table {
            border-spacing: 3px;
        }
    }
    
    /* Add this to your existing table styles */
    .table-responsive {
        overflow-x: auto;
        -webkit-overflow-scrolling: touch;
    }
    
    .datatable {
        font-size: 14px;
    }
    
    .datatable th {
        background-color: #f0f7f0;
        font-weight: 600;
        color: #2c3e50;
        border-bottom: 2px solid #2095A1 !important;
    }
    
    .datatable td {
        vertical-align: middle;
    }
    
    /* Optional: Add loading overlay for page changes */
    .grid-loading {
        position: relative;
        opacity: 0.6;
        pointer-events: none;
    }
    
    /* Animation for page changes */
    @keyframes pageFadeIn {
        from {
            opacity: 0;
            transform: translateY(10px);
        }
        to {
            opacity: 1;
            transform: translateY(0);
        }
    }
    
    .datatable tbody tr {
        animation: pageFadeIn 0.3s ease-out;
    }
</style>

    <div class="content-wrapper">
        <fieldset>
       
            <legend>Nafed Storage Bills Approve/Reject (Marketing)</legend>
            <div class="row">
                <div class="col-md-2">
                    <div class="form-group">
                        <label>Crop Year</label>
                        <asp:DropDownList CssClass="form-control" ID="ddlcropyear" runat="server">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <label>Month</label>
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

                <div class="col-md-2">
                    <div class="form-group">
                        <label>Commodity</label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="Search"
                            ErrorMessage="Select Commodity" InitialValue="0" ForeColor="Red"
                            ControlToValidate="ddlcommodity" Display="Dynamic" runat="server">
                        </asp:RequiredFieldValidator>
                        <asp:DropDownList CssClass="form-control select2" ID="ddlcommodity" runat="server">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-md-5"></div>
                <div class="col-md-2" style="text-align: center; margin-top: 35px">
                    <asp:Button runat="server" CssClass="btn btn-success btn-sm"
                        ValidationGroup="Search" Text="Search" ID="btnSearch"
                        OnClick="btnSearch_Click" />
                </div>
            </div>
        </fieldset>

        <div class="row" runat="server" id="grdbill" visible="false">
            <div class="col-md-12">
                <fieldset>
                    <legend>Bills Details</legend>
                    <div class="row" style="margin-bottom: 10px">
                        <div class="col-md-12">
                            <div class="col-md-4">
                                <asp:Button ID="btnExportPDF" runat="server" Text="Export To PDF"
                                    CssClass="btn btn-success" OnClientClick="PrintGridData(); return false;" />
                                <asp:Button ID="btnExportExcel" runat="server" Text="Export to Excel"
                                    CssClass="btn btn-primary" OnClick="btnExportExcel_Click" />
                            </div>
                            <div class="col-md-4"></div>
                            <div class="col-md-2" style="margin-top: 5px">
                                <label>Total Count</label>
                                <asp:TextBox CssClass="form-control" ID="txtCount" runat="server" Enabled="false"></asp:TextBox>
                            </div>
                            <div class="col-md-2" style="margin-top: 5px">
                                <label>Total Amount</label>
                                <asp:TextBox ID="lblAmount" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div style="padding: 0px 0px 15px 0px">
                        <asp:TextBox ID="txtSearch" runat="server"
                            placeholder="Search here..."
                            Style="width: 420px; height: 36px; padding: 0 15px; font-size: 15px; border: 1px solid #000; border-radius: 8px;"
                            onkeyup="filterGrid();" Visible="false" />
                    </div>

                    <div class="table-responsive">
                        <%-- <asp:GridView runat="server" DataKeyNames="Bill_Number" ID="GrdBills"
                            CssClass="table table-bordered table-hover datatable" AutoGenerateColumns="False"
                            OnRowCommand="GrdBills_RowCommand" AllowPaging="true" PageSize="500"
                            OnPageIndexChanging="GrdBills_PageIndexChanging">--%>
                        <asp:GridView runat="server"
                            DataKeyNames="Bill_Number"
                            ID="GrdBills"
                            CssClass="table table-bordered table-hover datatable"
                            AutoGenerateColumns="False"
                            OnRowCommand="GrdBills_RowCommand"
                            AllowPaging="true"
                            PageSize="500"
                            OnPageIndexChanging="GrdBills_PageIndexChanging"
                            PagerStyle-CssClass="custom-pager"
                            PagerStyle-HorizontalAlign="Center"
                            PagerSettings-Mode="NumericFirstLast"
                            PagerSettings-FirstPageText="« First"
                            PagerSettings-LastPageText="Last »"
                            PagerSettings-PreviousPageText="‹ Prev"
                            PagerSettings-NextPageText="Next ›"
                            PagerSettings-PageButtonCount="5">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Region Name" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblRegion_Name" Text='<%# Eval("Regionnm") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="District Name" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblDistrict_Name" Text='<%# Eval("District_Name") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField> 
                                <asp:TemplateField HeaderText="Branch Name" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblBranch_Name" Text='<%# Eval("Branch_Name") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Godown Name" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblGodown_Name" Text='<%# Eval("Godown_Name") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bill Name" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblBill_Number" Text='<%# Eval("Bill_Number") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Commodity Name" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblCommodity_Name" Text='<%# Eval("Commodity_Name") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Crop Year" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblCrop_Year" Text='<%# Eval("Crop_Year") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Financial Year" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblFinancial_Year" Text='<%# Eval("Financial_Year") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Month" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblMonth" Text='<%# Eval("MonthYear") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Closing Balance" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblClosing_Balance" Text='<%# Eval("Closing_Balance") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Net Amount" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblNet_Amount" Text='<%# Eval("Net_Amount") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:TemplateField HeaderText="Marketing Approval Date" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblMarketing_Approval_Date" Text='<%# Eval("Marketing_Approve_Date") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Marketing Approval Status" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblAccount_Approval_Date"
                                            CssClass='<%# Eval("Marketing_Approve_Stutes").ToString() == "Y" ? "status-approved" : "status-rejected" %>'
                                            Text='<%# Eval("Marketing_Approve_Stutes").ToString() == "Y" ? "Approved" : "Not Approved" %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Select" HeaderStyle-BackColor="LightBlue">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkSelectAll" runat="server" AutoPostBack="true"
                                            OnCheckedChanged="chkSelectAll_CheckedChanged" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelect" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>No Record Found</EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                </fieldset>
            </div>
        </div>

        <div class="row">
            <div class="col-md-5"></div>
            <div class="col-md-2">
                <asp:Button ID="btnPrintSelected" runat="server" Text="Print Selected"
                    CssClass="btn btn-success" OnClick="btnPrintSelected_Click"
                    OnClientClick="ShowLoader();" />
            </div>
        </div>

        <div id="loader" class="loader">
            <div class="loader-content">
                <img src="../images/mpwlc3.gif" alt="Loading..." />
            </div>
        </div>
    </div>
</asp:Content>
