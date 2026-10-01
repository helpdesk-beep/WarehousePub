<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Rpt_District_Wise_Payment_Status_All.aspx.cs" Inherits="Reports_Region_Rpt_District_Wise_Payment_Status_All" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title style="color: white;">District Wise Payment Status</title>

    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <script src="scripts/jquery-3.3.1.min.js"></script>
    <script src="scripts/bootstrap.min.js"></script>
    <link href="Content/dataTables.bootstrap4.min.css" rel="stylesheet" />
    <link href="../../assets/css/style.css" rel="stylesheet" />
    <script src="scripts/dataTables.bootstrap4.min.js"></script>
    <script src="scripts/jquery.dataTables.min.js"></script>
    <script type="text/javascript">  
        $(document).ready(function () {
            $("#EmployeeGridViewList").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable();
        });
    </script>
    <style>
        .btnMargin {
            margin-bottom: 10px !important;
        }
    </style>
    <style>
        .Grid {
            background-color: #fff;
            margin: 5px 0 10px 0;
            border: solid 1px #525252;
            border-collapse: collapse;
            font-family: Calibri;
            color: #474747;
            width: 100%;
        }

            .Grid td {
                padding: 2px;
                border: solid 1px #c1c1c1;
            }

            .Grid th {
                padding: 4px 2px;
                color: #fff;
                background: #00aad2 url(Images/grid-header.png) repeat-x top;
                border-left: solid 1px #525252;
                font-size: 15px;
                text-align: center;
            }

            .Grid .alt {
                background: #fcfcfc url(Images/grid-alt.png) repeat-x top;
            }

            .Grid .pgr {
                background: #00aad2 url(Images/grid-pgr.png) repeat-x top;
            }

                .Grid .pgr table {
                    margin: 3px 0;
                }

                .Grid .pgr td {
                    border-width: 0;
                    padding: 0 6px;
                    border-left: solid 1px #666;
                    font-weight: bold;
                    color: #fff;
                    line-height: 12px;
                }

                .Grid .pgr a {
                    color: Gray;
                    text-decoration: none;
                }

                    .Grid .pgr a:hover {
                        color: #000;
                        text-decoration: none;
                    }
    </style>
    <script type="text/javascript">
        function PrintGridData() {
            var prtGrid = document.getElementById('<%=GridView1.ClientID %>');
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
            var gridData = document.getElementById('<%= GridView1.ClientID %>');
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
            Width: 90px;
            height: 20px;
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
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div style="text-align: center; font-size: large;">
                <h2 class="header">M.P. WAREHOUSING & LOGISTICS CORPORATION</h2>
                <h4 class="header">District Wise Payment Status</h4>
            </div>
            <div style="text-align: center; font-size: large;">

                <%-- <asp:Label ID="Label1" runat="server" Text="Commodity"></asp:Label>
                <asp:DropDownList ID="ddlcommodity" Height="25px" Width="300px" runat="server" OnSelectedIndexChanged="ddlcommodity_SelectedIndexChanged" AutoPostBack="true">
                </asp:DropDownList>--%>

                <%-- <asp:Label ID="Label2" runat="server" Text="District"></asp:Label>
                <asp:DropDownList ID="ddldistrict" Height="25px" Width="300px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged">
                </asp:DropDownList>

                <asp:Label ID="Label3" runat="server" Text="Branch"></asp:Label>
                <asp:DropDownList ID="ddlbranch" Height="25px" Width="300px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlbranch_SelectedIndexChanged">
                </asp:DropDownList>
                <br />
                <br />
                <asp:Label ID="Label5" runat="server" Text="Month"></asp:Label>
                <asp:DropDownList ID="ddlmonth" Height="25px" Width="300px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlmonth_SelectedIndexChanged">
                </asp:DropDownList>--%>
                <asp:Button ID="btnback" runat="server" Text="Back" CssClass="button button2" OnClick="btnback_Click" />
                &nbsp;&nbsp;
                <asp:Button ID="Button2" runat="server" Text="ExportToPDF" CssClass="button button2" OnClientClick="printGrid()" />
                &nbsp;&nbsp;
                <input type="button" id="btnExport" value="Exporttoexcel" class="button button2" />


            </div>
            <table style="border: solid 5px #e3e3e8; width: 80%; vertical-align: central; margin-left: 10%;">
                <tr>
                    <td style="text-align: center;">From Date (DD/MM/YYYY):&nbsp;&nbsp;
                        <asp:TextBox ID="txtFDate" runat="server" ></asp:TextBox>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        To Date (DD/MM/YYYY):&nbsp;&nbsp;<asp:TextBox ID="txtTDate" runat="server"></asp:TextBox>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClientClick=" return validate()" OnClick="btnSearch_Click" />
                    </td>
                </tr>
            </table>
            <table style="border: solid 5px #e3e3e8; width: 100%; vertical-align: central;">

                <tr>
                    <td style="text-align: left;">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" ShowFooter="true"
                            CssClass="Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr" OnRowDataBound="GridView1_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No." ItemStyle-Width="3%">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--  <asp:TemplateField HeaderText="District Name">
                                    <ItemTemplate>
                                       
                                        <asp:HyperLink ID="sdffsft" runat="server" NavigateUrl='<%#"#"%>'
                                            title="District Name" Text=' <%# Eval("District_Name") %>' ForeColor="Blue"></asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>    --%>
                                <asp:BoundField DataField="Region" HeaderText="Region" ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="District" HeaderText="District" ItemStyle-HorizontalAlign="Left" />
                                <%--<asp:BoundField DataField="Branch" HeaderText="Branch" ItemStyle-HorizontalAlign="Left"/>

                                   <asp:TemplateField HeaderText="District Name">
                                    <ItemTemplate>
                                       
                                      <asp:HyperLink ID="sdffsft" runat="server" Target="_blank" NavigateUrl='<%#"~/WarehouseLevel/Rpt_Godown_Bill_Wise_Payment_Status.aspx?GodownID="+ (Eval("Godown_ID").ToString())%>'
                                            title="Godown Name" Text=' <%# Eval("godown_Name") %>' ForeColor="Blue"></asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>    --%>

                                <%--<asp:BoundField DataField="godown_Name" HeaderText="godown_Name" ItemStyle-HorizontalAlign="Left"/>--%>
                                <%--<asp:BoundField DataField="noofgdwn" HeaderText="Total No. of JVS Godown" ItemStyle-HorizontalAlign="Right"/>--%>
                                <asp:BoundField DataField="NoOfGenerateBill" HeaderText="Total No. of Bill Generation" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="BillAmt" HeaderText="Total Bill Amount" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="NoOfSUBBill" HeaderText="Total No. of Submitted Bill" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="SUBBillAmt" HeaderText="Total Submitted Bill Amount" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="PendingBillForSubmisionatBranch" HeaderText="Pending Bill For Submision at Branch" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="PendingBillAmountForSubmision" HeaderText="Pending Bill Amount For Submision" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="NoofbillPaymentReceivedFromMPSCSC" HeaderText="Total Received Bill From MPSCSC" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="NoofbillPaymentAmountReceivedFromMPSCSC" HeaderText="Total Received Bill Amount From MPSCSC" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="PaymentDecuctionbyMPSCSC" HeaderText="Payment Deducted From MPSCSC" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="TotalNoofPendingBillatMPSCSC" HeaderText="Total No. of Pending Bill at MPSCSC" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="TotalNoofPendingBillAmountatMPSCSC" HeaderText="Total No. of Pending Bill Amount at MPSCSC" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="NoOfBillPayment" HeaderText="Total No. of Bill Pay to Godown Owner" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="BillAmtPTG" HeaderText="Total No. of Bill Amount Pay to Godown Owner" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="PendintatRM" HeaderText="Total No. of Bills Pending at RM/BM/Godown" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="PendintBillAmountatRM" HeaderText="Total No. of Bill Amount Pending at RM/BM/Godown" ItemStyle-HorizontalAlign="Right" />
                            </Columns>
                            <FooterStyle Font-Bold="True" ForeColor="Black" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </div>
        <script type="text/javascript" src="<%= ResolveUrl("~/NEW_CSS/js/jquery-1.12.4.js") %>"></script>
        <script src="../../JS/table2excel.js"></script>
        <script type="text/javascript">
            $("body").on("click", "#btnExport", function () {
                $("[id*=GridView1]").table2excel({
                    filename: "receivedPaymentfromMPSCSCthroughNEFTPaymentSystem.xls"
                });
            });
        </script>
        <%--<script language="javascript" type="text/javascript">
            function validate() {
                if (document.getElementById("<%=txtFDate.ClientID%>").value == "") {
                    alert("From Date can not be blank");
                    document.getElementById("<%=txtFDate.ClientID%>").focus();
                    return false;
                }
                if (document.getElementById("<%=txtTDate.ClientID %>").value == "") {
                    alert("To Date can not be blank");
                    document.getElementById("<%=txtTDate.ClientID %>").focus();
                    return false;
                }

            }
            $(document).ready(function () {
                $("[id$=txtFDate]").datepicker({
                    defaultDate: "+1w",
                    changeMonth: true,
                    changeYear: true,
                    numberOfMonths: 1,
                    dateFormat: 'dd/mm/yy',
                    maxDate: 'today',
                });
            });
            $(document).ready(function () {
                $("[id$=txtTDate]").datepicker({
                    defaultDate: "+1w",
                    changeMonth: true,
                    changeYear: true,
                    numberOfMonths: 1,
                    dateFormat: 'dd/mm/yy',
                    maxDate: 'today',
                });
            });
        </script>--%>
    </form>
</body>
</html>
