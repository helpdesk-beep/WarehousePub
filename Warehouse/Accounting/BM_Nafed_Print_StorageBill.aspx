<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="~/Accounting/BM_Nafed_Print_StorageBill.aspx.cs" Inherits="Accounting_BM_Nafed_Print_StorageBill" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../assets/css/style.css" rel="stylesheet" />
    <!-- Bootstrap core CSS -->
    <link href="../assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/css/bootstrap-theme.min.css" rel="stylesheet" type="text/css" />

    <!-- font awesome -->
    <link href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" />

    <link href="../assets/css/style.css" rel="stylesheet" type="text/css" />
    <link href="../assets/css/custome.css" rel="stylesheet" type="text/css" />

    <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />
    <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<asp:ValidationSummary ID="vds" runat="server" ShowMessageBox="true" ValidationGroup="a" />--%>
    <div class="content-wrapper">
        <asp:Label runat="server" ID="Label3"></asp:Label>
        <fieldset style="border: 1px solid navy; border-radius: 10px 10px 10px 10px;">
            <legend>Print Nafed Storage Bills</legend>
            <div class="row">
                <div class="col-md-12">
                    <div class="col-md-2">
                        <div class="form-group ">
                            <label style="margin-top: 10px">District Name</label>
                            <asp:TextBox ID="txtDistrictName" runat="server" CssClass="form-control" ReadOnly="true" Font-Size="12px" Font-Bold="true" Text="Select District" ForeColor="Navy"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <div class="form-group ">
                            <label style="margin-top: 10px">Branch Name</label>
                            <asp:TextBox ID="txtBranch" runat="server" CssClass="form-control" ReadOnly="true" Font-Size="12px" Font-Bold="true" Text="Select Branch" ForeColor="Navy"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <div class="form-group">
                            <label style="margin-top: 10px">Financial Year</label>
                            <span class="fa-pull-right">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="a" ToolTip="Select Financial Year"
                                    ErrorMessage="Select Financial Year " InitialValue="0" ForeColor="Red"
                                    Text="<i class='fa fa-exclamation-circle'></i>"
                                    ControlToValidate="ddlFinancialyear" Display="Dynamic" runat="server">
                                </asp:RequiredFieldValidator>
                            </span>
                            <%--<asp:TextBox ID="txtFinancialYear" runat="server" CssClass="form-control" ReadOnly="true" Font-Size="12px" Font-Bold="true" Text="2024" ForeColor="Navy"></asp:TextBox>--%>
                            <asp:DropDownList CssClass="form-control select2" ID="ddlFinancialyear" runat="server">
                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                                <asp:ListItem Value="2023">2023-24</asp:ListItem>
                                <asp:ListItem Value="2024">2024-25</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <div class="form-group">
                            <label style="margin-top: 10px">Month</label>
                            <span class="fa-pull-right">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a" ToolTip="Select Month"
                                    ErrorMessage="Select Month" InitialValue="0" ForeColor="Red"
                                    Text="<i class='fa fa-exclamation-circle'></i>"
                                    ControlToValidate="ddlmonth" Display="Dynamic" runat="server">
                                </asp:RequiredFieldValidator>
                            </span>
                            <asp:DropDownList CssClass="form-control select2" ID="ddlmonth" runat="server">
                                <asp:ListItem Value="0">--Select--</asp:ListItem>
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
                    <div class="col-md-2" style="text-align: center; margin-top: 35px">
                        <asp:Button runat="server" CssClass="btn btn-success btn-sm" ValidationGroup="a" Text="Search" ID="btnSearch" OnClick="btnSearch_Click" autopostback="true" />
                    </div>
                </div>
            </div>

        </fieldset>
        <div class="row" style="align-content: center" runat="server" id="grdbill" visible="false">
            <div class="col-md-12">
                <fieldset>
                    <legend>Bills Details</legend>
                    <div class="table-responsive">
                        <div class="row">
                            <div class="col-md-6">
                                <asp:Label ID="Label18" runat="server" Font-Size="12pt" ForeColor="Blue"
                                    Text="भंडारण शुल्क बिल का प्रिन्ट लेने के लिये दी गई View बटन पे क्लिक करें"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <asp:DropDownList ID="lblBill_Number" runat="server"
                                    Height="25px" Width="200px" AutoPostBack="true" Visible="false">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <asp:GridView runat="server" DataKeyNames="Bill_Number" ID="GrdBills"
                            CssClass="table table-bordered table-hover datatable" AutoGenerateColumns="False">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="District Name" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblDistrict_Name" Text='<%# Eval("District_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Branch Name" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblBranch_Name" Text='<%# Eval("Branch_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Godown Name" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblGodown_Name" Text='<%# Eval("Godown_Name") %>'></asp:Label>
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
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Crop Year" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblCrop_Year" Text='<%# Eval("Crop_Year") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Net Amount" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblNet_Amount" Text='<%# Eval("Net_Amount") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnPrint" runat="server" CausesValidation="false" CommandName="Print" CommandArgument='<%# Eval("Bill_Number")%>' CssClass="BTNBLUE" Text="Print Bill" />
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
    <%-- <fieldset style="width: 800px; border: 1px solid navy; box-shadow: 1px 2px 8px; border-radius: 10px 10px 10px 10px; padding-left: 0px; margin-left: 15px">
        <center>
            <div style="background-color: white;">
                <table cellpadding="0" cellspacing="0" style="width: 90%">
                    <tr>
                        <%--<td align="left" style="width: 200px">
                            <asp:Label ID="lblBillNo" runat="server" Font-Size="12px" Font-Bold="true"
                                Text="Select Bill Number" ForeColor="navy"></asp:Label>
                        </td>--%>
    <%-- <td align="left" style="width: 200px"></td>
                    </tr>
                    <tr>
                        <td colspan="4" align="left"></td>
                    </tr>
                </table>
            </div>
        </center>
    </fieldset>--%>
    <asp:Panel ID="Panel2" runat="server" CssClass="modalPopup" Style="width: 850px; height: 550px; box-shadow: 1px 2px 8px; border-radius: 10px 10px 10px 10px;" Visible="false" BorderStyle="Outset">
        <div>
            <table cellspacing="1" cellpadding="3" style="width: 100%;">
                <tr>
                    <td align="center" width="100%">
                        <div id="printActualBill">
                            <table width="100%">
                                <tr>
                                    <td colspan="2" align="center">
                                        <asp:Image ID="Image3" runat="server" ImageUrl="~/images/mpwlc.png" Height="36px" Width="40px" ImageAlign="Left" />
                                        <asp:Label ID="Label7" runat="server" Text="M.P. Warehousing & Logistics Corporation -" Font-Size="14px" Font-Bold="true" ForeColor="#0066cc"></asp:Label>
                                        <asp:Label ID="Label12" runat="server" Font-Size="14px" Font-Bold="true" ForeColor="#0066cc"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center">
                                        <asp:Label ID="Label22" runat="server" Text="NAFED STORAGE CHARGES BILL" Font-Bold="true" underline="True" Font-Size="14px" ForeColor="#cc0066"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style1"></td>
                                </tr>
                                <tr>
                                    <td align="left" class="auto-style2">
                                        <asp:Label ID="Label28" runat="server" Text="District    :-" Font-Size="12px"></asp:Label>&nbsp;
                                            <asp:Label ID="lblDistrict" runat="server" Text=":- " Font-Size="12px"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="Label36" runat="server" Text="Branch     :-" Font-Size="12px"></asp:Label>&nbsp;
                                            <asp:Label ID="lblBranch" runat="server" Text=":- " Font-Size="12px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" class="auto-style2">
                                        <asp:Label ID="Label11" runat="server" Text="Billing Date :- " Font-Size="12px"></asp:Label>&nbsp;
                                            <asp:Label ID="lblBillingDate" runat="server" Text=" :- " Font-Size="12px"></asp:Label>
                                    </td>
                                    <%-- <td align="left">
                                        <asp:Label ID="Label18" runat="server" Text="Rate(Per MT) :- " Font-Size="12px"></asp:Label>&nbsp;
                                            <asp:Label ID="lblRate" runat="server" Text=" :- " Font-Size="12px"></asp:Label>
                                    </td>--%>
                                </tr>
                                <tr>
                                    <td class="auto-style1"></td>
                                </tr>
                                <tr>
                                    <td align="left" class="auto-style2">
                                        <asp:Label ID="Label13" runat="server" Text="Storage Agency Details :- " Font-Size="12px" Font-Bold="true" ForeColor="#333399"></asp:Label>&nbsp;
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="Label15" runat="server" Text="Depositor Agency Details :- " Font-Size="12px" Font-Bold="true" ForeColor="#333399"></asp:Label>&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" class="auto-style2">
                                        <asp:Label ID="Label23" runat="server" Text="Name :" Font-Size="12px"></asp:Label>&nbsp;
                                            <asp:Label ID="lbldatefromto" runat="server" Text="  MPWLC" Font-Size="12px"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="Label10" runat="server" Text="Name :" Font-Size="12px"></asp:Label>&nbsp;
                                            <asp:Label ID="lbldepositor" runat="server" Text="NAFED" Font-Size="12px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" class="auto-style2">
                                        <asp:Label ID="Label30" runat="server" Text="PAN :- " Font-Size="12px"></asp:Label>&nbsp;
                                            <asp:Label ID="lblSPAN" runat="server" Text="" Font-Size="12px"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="Label34" runat="server" Text="PAN :- " Font-Size="12px"></asp:Label>&nbsp;
                                            <asp:Label ID="lblcmd_ac" runat="server" Text="XXXXXXXXX" Font-Size="12px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" class="auto-style2">
                                        <asp:Label ID="Label41" runat="server" Text="GSTN :" Font-Size="12px"></asp:Label>&nbsp;
                                            <asp:Label ID="lblSGST" runat="server" Text="XXXXXXXXX" Font-Size="12px"></asp:Label>&nbsp;
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="Label16" runat="server" Text="GSTN :" Font-Size="12px"></asp:Label>&nbsp;
                                            <asp:Label ID="Label42" runat="server" Text="XXXXXXXXX" Font-Size="12px"></asp:Label>&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style1"></td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="2">
                                        <asp:GridView ID="gvBill" runat="server" AutoGenerateColumns="False" Width="100%" Font-Names="Arial"
                                            DataKeyNames="Bill_Number" BorderStyle="Double" CellPadding="3" CellSpacing="5" ShowFooter="true"
                                            Font-Size="11px" BorderColor="#CCCCCC" OnSelectedIndexChanged="gvBill_SelectedIndexChanged1">
                                            <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <Columns>
                                                <asp:BoundField DataField="Bill_Number" HeaderText="Bill Number"></asp:BoundField>
                                                <asp:BoundField DataField="Commodity" HeaderText="Commodity" ItemStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Crop_Year" HeaderText="Crop Year" ItemStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Bill_Month" HeaderText="Bill Month" ItemStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Dates_Period" HeaderText="Dates Period" ItemStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Opening_Balance" HeaderText="Opening Balance" ItemStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Receive_Bags" HeaderText="Receive Bags" ItemStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Issue_Bags" HeaderText="Issue Bags" ItemStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Closing_Balance" HeaderText="Closing Balance" ItemStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Reserve_Bags" HeaderText="Reserve Bags" ItemStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Chargable_Bags" HeaderText="Chargable Bags" ItemStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Total_Charges" HeaderText="Total Charges" ItemStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                                </asp:BoundField>
                                            </Columns>
                                            <HeaderStyle ForeColor="#C70039" HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="11px" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="height: 30px;">
                                        <asp:Label ID="lblmsg" ForeColor="Red" Font-Bold="true" runat="server" Text="नोट : देयक मे सम्मिलित गोदाम/केप/साइलों का विस्त्रत विवरण देखने के लिए सामने प्रदर्शित लिंक पर क्लिक करे।-->"></asp:Label>
                                        <asp:LinkButton ID="btnDetail" runat="server" ForeColor="#0066cc" Font-Bold="true" Font-Underline="true" Text="" OnClick="btnDetail_Click"></asp:LinkButton>
                                    </td>
                                </tr>
                                <%-------------DSC Actual Bill Stars------------%>
                                <tr>
                                    <td align="right" colspan="2" style="width: 100%;">
                                        <table style="width: 100%;">
                                            <tr class="fountcolor">
                                                <td align="right">
                                                    <asp:Image ID="Image2" runat="server" Height="30px" Visible="false"
                                                        ImageUrl="~/images/dsc1.png" Width="80px" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                </td>
                                                <td align="right">
                                                    <asp:Image ID="Image1" runat="server" Height="30px" Visible="false"
                                                        ImageUrl="~/images/dsc1.png" Width="80px" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                </td>
                                            </tr>
                                            <tr class="fountcolor">
                                                <td align="right">
                                                    <asp:Label ID="lblICSerialNo" runat="server" Text="" Font-Bold="True"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                <td align="right">
                                                    <asp:Label ID="lblBSerialNo" runat="server" Text="" Font-Bold="True"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                            </tr>
                                            <tr class="fountcolor">
                                                <td align="right">
                                                    <asp:Label ID="lblICHoldername" runat="server" Text="" Font-Bold="false"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                <td align="right">
                                                    <asp:Label ID="lblBHolderName" runat="server" Text="" Font-Bold="false"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                            </tr>
                                            <tr class="fountcolor">
                                                <td align="right">
                                                    <asp:Label ID="lblICCreatedDate" runat="server" Text="" Font-Bold="false"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                <td align="right">
                                                    <asp:Label ID="lblBCreatedDate" runat="server" Text="" Font-Bold="false"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                            </tr>
                                            <tr class="fountcolor">
                                                <td align="right">
                                                    <asp:Label ID="lblICIp" runat="server" Text="" Font-Bold="false"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                <td align="right">
                                                    <asp:Label ID="lblBIP" runat="server" Text="" Font-Bold="false"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                            </tr>
                                            <tr class="fountcolor">
                                                <td colspan="2">
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr class="fountcolor">
                                                <td align="right">
                                                    <asp:Label ID="Label51" runat="server" Text="प्रदाय केन्द्र प्रभारी के हस्ताक्षर" Font-Bold="true"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                <td align="right">
                                                    <asp:Label ID="Label50" runat="server" Text="शाखा प्रबंधक(MPWLC) के हस्ताक्षर" Font-Bold="true"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr class="fountcolor">
                                    <td colspan="2">
                                        <br />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td align="center" style="height: 50px">
                        <asp:Button class="button button2" Width="150px" Height="30px" ID="Button4"
                            runat="server" Text="Close" align="Center" OnClick="Button4_Click" />
                        &nbsp &nbsp &nbsp 
                        <input id="Button5" name="Print" type="button" style="height: 30px; width: 150px;"
                            class="button button2" value="Print" onclick="PrintDiv_Actual();" />
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnllogin" class="popup" runat="server">
        <div class="pop" style="background-color: cornsilk">
            <img visible="true" runat="server" src="~/images/close-icon.png" alt="quit" class="x" id="x" />
            <table cellspacing="1" cellpadding="3">
                <tr>
                    <td colspan="2" align="center">
                        <asp:Label ID="Label1" runat="server" Text="GODOWN WISE STORAGE CHARGES BILLS DETAIL" Font-Bold="true" underline="True" Font-Size="14px" ForeColor="#0066cc"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2">
                        <div style="height: 550px; overflow: scroll; width: 1200px;">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="1200px
                                                "
                                Font-Names="Arial"
                                DataKeyNames="Bill_Number" BorderStyle="Double" CellPadding="3" CellSpacing="5" ShowFooter="true"
                                Font-Size="11px" BorderColor="#CCCCCC" OnSelectedIndexChanged="gvBill_SelectedIndexChanged">
                                <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <Columns>

                                    <asp:BoundField DataField="Bill_Number" HeaderText="Bill Number">
                                        <ItemStyle Width="50px" HorizontalAlign="Center"></ItemStyle>

                                    </asp:BoundField>


                                    <asp:BoundField DataField="Commodity" HeaderText="Commodity" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>

                                    <%--          <asp:BoundField DataField="Opening_Weight" HeaderText="Commodity" ItemStyle-HorizontalAlign="Left" > 
                                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                                    </asp:BoundField>--%>
                                    <asp:BoundField DataField="Crop_Year" HeaderText="Crop Year" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="50px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Bill_Month" HeaderText="Period" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="50px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Godown" HeaderText="Godown Name">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Godown_Id" HeaderText="Godown ID" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="50px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Charges_Amount" HeaderText="Charges Amount" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="50px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="GST_AMT" HeaderText="GST Amount" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="50px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Sup_Charges_Amt" HeaderText="Supervision Charges" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="50px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="GST_Sup_Amt" HeaderText="GST on Supervision" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="50px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Net_Amount" HeaderText="Total Bill Amount" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="50px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <%--                                                  <asp:HyperLinkField DataNavigateUrlFields="TotalGodown" ControlStyle-Font-Underline="true" DataNavigateUrlFormatString="Destination.aspx?QS={0}" HeaderText="Warehouses Details" SortExpression="ARecord" />--%>
                                    <asp:CommandField HeaderText="Show Detail" ShowSelectButton="true" ButtonType="Link"
                                        ItemStyle-ForeColor="red">
                                        <ItemStyle ForeColor="Red"></ItemStyle>
                                        <ItemStyle Width="50px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:CommandField>
                                    <asp:BoundField DataField="JVS_Bill" HeaderText="JVS Bill" ItemStyle-HorizontalAlign="Left"></asp:BoundField>
                                </Columns>
                                <FooterStyle Font-Bold="True" HorizontalAlign="center" Height="15px" Font-Size="11px" ForeColor="#cc0066" />
                                <HeaderStyle ForeColor="#C70039" HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="11px" />
                            </asp:GridView>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="height: 30px;">
                        <asp:Label ID="Label2" ForeColor="Red" Font-Bold="true" runat="server" Text="नोट : देयक का विस्त्रत विवरण देखने के लिए बिल के Show Detail वाले कॉलम मे प्रदर्शित लिंक Select पर क्लिक करे।"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label runat="server" ID="new"></asp:Label></td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    <%--  <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="new" BackgroundCssClass="popup" PopupControlID="pnllogin" CancelControlID="x">
    </asp:ModalPopupExtender>
    <asp:AnimationExtender ID="popupAnimation" runat="server" TargetControlID="new">
        <animations>
            <onclick>
                <parallel animationtarget="pnllogin" duration="0.4" fps="10">
                    <fadein />
                </parallel>
            </onclick>
        </animations>
    </asp:AnimationExtender>--%>
</asp:Content>

