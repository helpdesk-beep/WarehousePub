<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Rpt_Account_Reconcile_Reports_For_Audit_01_Receive_Payment.aspx.cs" Inherits="Reports_States_Rpt_Account_Reconcile_Reports_For_Audit_01_Receive_Payment" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title style="color: white;">MPWLC Payment Credit to Godown Owner through NEFT Payment System</title>

    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <script src="scripts/jquery-3.3.1.min.js"></script>
    <script src="scripts/bootstrap.min.js"></script>
    <link href="Content/dataTables.bootstrap4.min.css" rel="stylesheet" />
    <link href="../../assets/css/style.css" rel="stylesheet" />
    <script src="scripts/dataTables.bootstrap4.min.js"></script>
    <script src="scripts/jquery.dataTables.min.js"></script>

    <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />
    <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />
    <link href="../CSS/style.css" rel="stylesheet" />
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />
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
                <h4 class="header">MPWLC Payment Credit to Godown Owner through NEFT Payment System</h4>
            </div>
            <div style="text-align: center; font-size: large;">
                
                <table style="border: solid 5px #e3e3e8; width: 80%; vertical-align: central; margin-left: 10%;">
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label7" runat="server" Text="Region"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlregion2" Height="25px" Width="300px" runat="server" AutoPostBack="false">
                            </asp:DropDownList>
                        </td>

                        <td align="right">
                            <asp:Label ID="lblDistrict" runat="server" Text="From Date (DD-MM-YYYY) : " Font-Size="10pt" Font-Bold="true"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtfromdate" runat="server"></asp:TextBox>
                        </td>
                        <td align="right">
                            <asp:Label ID="Label6" runat="server" Text="To Date (DD-MM-YYYY) : " Font-Size="10pt" Font-Bold="true"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txttodate" runat="server"></asp:TextBox>
                        </td>
                    </tr>

                </table>

                <asp:Button ID="Button2" runat="server" Text="ExportToPDF" CssClass="button button2" OnClientClick="printGrid()" />
                &nbsp;&nbsp;
                <input type="button" id="btnExport" value="Exporttoexcel" class="button button2" />
                &nbsp;&nbsp;
                <asp:Button ID="btnshow" runat="server" Text="View Date Wise Payment" Width="165px" Height="50px" CssClass="button button2" OnClick="btnshow_Click" />
            </div>
            <table id="grd1" style="border: solid 5px #e3e3e8; width: 100%; vertical-align: central;" runat="server" visible="false">

                <tr>
                    <td style="text-align: left;">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" ShowFooter="true"
                            CssClass="Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr" OnRowDataBound="GridView1_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No." ItemStyle-Width="3%">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="District_Name" HeaderText="District" />
                                <asp:BoundField DataField="DepotName" HeaderText="Branch" />
                                <%--<asp:BoundField DataField="PaymentCreditDate" HeaderText="Payment Credit Date" />--%>
                                <asp:BoundField DataField="Godown_Name" HeaderText="Godown" />                                
                                <asp:BoundField DataField="Godown_ID" HeaderText="Godown_ID" />                                
                                <asp:BoundField DataField="Account_No" HeaderText="Account_No" />                                
                                <asp:BoundField DataField="IFSC_Code" HeaderText="IFSC_Code" />                                
                                <asp:BoundField DataField="JVS_Bill_No" HeaderText="JVS_Bill_No" />                                
                                <asp:BoundField DataField="SC_Bill_No" HeaderText="SC_Bill_No" />                                
                                <asp:BoundField DataField="Financial_Year" HeaderText="Financial_Year" />                                
                                <asp:BoundField DataField="Crop_Year" HeaderText="Crop_Year" />                                
                                <asp:BoundField DataField="Commodity" HeaderText="Commodity" />                                
                                <asp:BoundField DataField="Bill_Month" HeaderText="Bill_Month" />                                
                                <asp:BoundField DataField="Per_Month_Rate" HeaderText="Per_Month_Rate" />                                
                                <asp:BoundField DataField="Rent_Bill_AMT" HeaderText="Rent_Bill_AMT" />                                
                                <asp:BoundField DataField="TDS_Amt" HeaderText="TDS_Amt" />                                
                                <asp:BoundField DataField="Gain_Detuction_Amount" HeaderText="Gain_Detuction_Amount" />                                
                                <asp:BoundField DataField="Other_Detuction_Amt" HeaderText="Other_Detuction_Amt" />                                
                                <asp:BoundField DataField="BM_Deduction" HeaderText="BM_Deduction" />                                
                                <asp:BoundField DataField="Total_Deduction_AMT" HeaderText="Total_Deduction_AMT" />                                
                                <asp:BoundField DataField="PayToGO" HeaderText="PayToGO" />                                
                                <asp:BoundField DataField="PAYtoMPWLC" HeaderText="PAYtoMPWLC" />                                
                                <asp:BoundField DataField="StorageCharBillAMt" HeaderText="StorageCharBillAMt" />                                
                                <asp:BoundField DataField="RO_Approve_Date" HeaderText="RO_Approve_Date" />                                
                                <asp:BoundField DataField="Hired_Type" HeaderText="Hired_Type" />                                
                                <asp:BoundField DataField="Reference_No" HeaderText="Reference_No" />                                
                                <asp:BoundField DataField="PaymentDate" HeaderText="Payment Date" />                                
                                <asp:BoundField DataField="UTRNumber" HeaderText="UTRNumber" />                                
                            </Columns>
                            <FooterStyle Font-Bold="True" ForeColor="Black" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>

            
        </div>
        <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
        <script src="../../JS/table2excel.js"></script>
        <script type="text/javascript">
            $("body").on("click", "#btnExport", function () {
                $("[id*=GridView1]").table2excel({
                    filename: "receivedPaymentfromMPSCSCthroughNEFTPaymentSystem.xls"
                });
            });
        </script>
        <script>
            $(document).ready(function () {
                $("[id$=txtfromdate]").datepicker({
                    defaultDate: "+1w",
                    changeMonth: true,
                    changeYear: true,
                    numberOfMonths: 1,
                    dateFormat: 'dd/mm/yy',
                });
            });
        </script>
        <script>
            $(document).ready(function () {
                $("[id$=txttodate]").datepicker({
                    defaultDate: "+1w",
                    changeMonth: true,
                    changeYear: true,
                    numberOfMonths: 1,
                    dateFormat: 'dd/mm/yy',
                });
            });
        </script>
        <script>
            $(document).ready(function () {
                $("[id$=txtfrom2]").datepicker({
                    defaultDate: "+1w",
                    changeMonth: true,
                    changeYear: true,
                    numberOfMonths: 1,
                    dateFormat: 'dd/mm/yy',
                });
            });
        </script>
        <script>
            $(document).ready(function () {
                $("[id$=txtto2]").datepicker({
                    defaultDate: "+1w",
                    changeMonth: true,
                    changeYear: true,
                    numberOfMonths: 1,
                    dateFormat: 'dd/mm/yy',
                });
            });
        </script>
    </form>
    <!--Java Script -->

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"></script>
    <script type="text/javascript" src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <script src="//code.jquery.com/jquery-1.10.2.js"></script>
    <script src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>
</body>
</html>
