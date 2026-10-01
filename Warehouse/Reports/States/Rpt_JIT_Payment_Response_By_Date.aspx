<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Rpt_JIT_Payment_Response_By_Date.aspx.cs" Inherits="Reports_States_Rpt_JIT_Payment_Response_By_Date" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title style="color: white;">State Level Rerport</title>

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
            Width: 200px;
            height: 50px;
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
            <div class="container py-4">
                <div class="card">

                    <div class="card-body">
                        <asp:Button ID="Button2" runat="server" Text="ExportToPDF" CssClass="button button2" OnClientClick="printGrid()" />
                        &nbsp;&nbsp;&nbsp;&nbsp;
                    <input type="button" id="btnExport" value="Exporttoexcel" class="button button2" />
                    </div>
                </div>

            </div>
            <br />
            <br />
            <table style="border: solid 5px #e3e3e8; width: 100%; vertical-align: central;">
                <tr>
                    <td style="width: 15px; text-align: right;">&nbsp;
                        <asp:Label ID="Label3" runat="server" Text="Godown Type: "></asp:Label>
                    </td>
                    <td style="width: 35px;">
                        <asp:DropDownList ID="ddlGType" runat="server" CssClass="form-control">
                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Owned Godown" Value="O"></asp:ListItem>
                            <asp:ListItem Text="JVS Godown" Value="J"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="width: 15px; text-align: right;">&nbsp;
                        <asp:Label ID="Label2" runat="server" Text="Payment Status: "></asp:Label>
                    </td>
                    <td style="width: 35px;">
                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Success" Value="S"></asp:ListItem>
                            <asp:ListItem Text="Failed" Value="F"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="width: 15px; text-align: right;">
                        <asp:Label ID="Label1" runat="server" Text="From Date (DD/MM/YY): "></asp:Label>
                    </td>
                    <td style="width: 35px;">
                        <asp:TextBox ID="txtFDate" runat="server" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                    </td>
                    <td style="width: 15px; text-align: right;">
                        <asp:Label ID="Label5" runat="server" Text="To Date (DD/MM/YY): "></asp:Label>
                    </td>
                    <td style="width: 35px;">
                        <asp:TextBox ID="txtTDate" runat="server" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                    </td>
                    <td style="width: 15px; text-align: center;">
                        <asp:Button class="btn-info" ID="btn_Search" runat="server" Text="Search"
                            TabIndex="11" Width="150px" Height="30px" OnClick="btn_Search_Click"></asp:Button>
                    </td>
                </tr>

            </table>
            <table style="border: solid 5px #e3e3e8; width: 100%; vertical-align: central;">
                <tr>
                    <td style="text-align: left;">
                        <asp:GridView runat="server" ID="GridView1" ShowFooter="true"
                            AutoGenerateColumns="false" CssClass="Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
                            <Columns>
                                <asp:BoundField DataField="Regionnm" HeaderText="Region" />
                                <asp:BoundField DataField="District_Name" HeaderText="District Name" />
                                <asp:BoundField DataField="DepotName" HeaderText="Branch" />
                                <asp:BoundField DataField="Godown_Name" HeaderText="Godown" />
                                <asp:BoundField DataField="Bill_Number" HeaderText="Bill Number" />
                                <asp:BoundField DataField="Party_Name" HeaderText="Party Name" />
                                <asp:BoundField DataField="Bill_Amt" HeaderText="Bill Amt" />
                                <asp:BoundField DataField="Received_Amt" HeaderText="Received Amt" />
                                <asp:BoundField DataField="Bill_Pass_Date" HeaderText="Bill Pass Date" />
                                <asp:BoundField DataField="JIT_Payment_Date" HeaderText="JIT Payment Date" />
                                <asp:BoundField DataField="Credit_Remark" HeaderText="Credit Remark" />
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </div>
        <script type="text/javascript" src="<%= ResolveUrl("~/NEW_CSS/js/jquery-1.12.4.js") %>"></script>
        <script src="../../JS/table2excel.js"></script>
        <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />
        <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.8.0/css/bootstrap-datepicker.min.css" />
        <link rel="stylesheet" href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
        <script src="<%= ResolveUrl("~/NEW_CSS/js/jquery-ui.js") %>"></script>
        <script type="text/javascript">
            $("body").on("click", "#btnExport", function () {
                $("[id*=GridView1]").table2excel({
                    filename: "State_Level_District_Branch_Wise_Rent_Summary.xls"
                });
            });
        </script>
        <script type="text/javascript">
            $(function () {
                $('#<%=txtFDate.ClientID %>').datepicker({
                    dateFormat: 'dd/mm/yy',
                    changeMonth: true,
                    changeYear: true,
                    yearRange: '1950:2100'
                });
            });
        </script>
        <script type="text/javascript">
            $(function () {
                $('#<%=txtTDate.ClientID %>').datepicker({
                    dateFormat: 'dd/mm/yy',
                    changeMonth: true,
                    changeYear: true,
                    maxDate: 0,
                    yearRange: '1950:2100'
                });
            });
        </script>
    </form>
</body>
</html>
