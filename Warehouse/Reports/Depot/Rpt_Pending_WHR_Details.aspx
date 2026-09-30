<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Rpt_Pending_WHR_Details.aspx.cs" Inherits="Reports_Depot_Rpt_Pending_WHR_Details" %>

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
            var prtGrid = document.getElementById('<%=gdnewproc.ClientID %>');
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
            var gridData = document.getElementById('<%= gdnewproc.ClientID %>');
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
                    <td style="text-align: left;" colspan="10">
                        <asp:GridView ID="gdnewproc" runat="server" AutoGenerateColumns="False"
                            DataKeyNames="Acceptance_No,DepositerNo" AllowPaging="False" Width="100%"
                            Font-Size="10pt" BorderColor="Navy" BorderWidth="1px" OnPageIndexChanging="gdnewproc_PageIndexChanging"
                            TabIndex="4" CellPadding="4" CellSpacing="2">
                            <Columns>
                                <asp:BoundField DataField="Branch" HeaderText="Branch" SortExpression="Branch">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DepositerNo" HeaderText="Depositor Form No." SortExpression="DepositerNo">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Acceptance_No" HeaderText="Acceptance No" SortExpression="Acceptance_No">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Acceptance_Date" HeaderText="Acceptance Date" SortExpression="Acceptance_Date">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TC_Number" HeaderText="TC Number" SortExpression="TC_Number">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Truck_Number" HeaderText="Truck Number" SortExpression="Truck_Number">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Send Bags">
                                    <ItemTemplate>
                                        <asp:TextBox ID="sendb" BackColor="Transparent" Enabled="false" Font-Bold="true" runat="server" Width="60px" MaxLength="5" onblur="Spc_validatorInt(this)" Text='<%# Eval("Recd_Bags") %>'>0</asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="80px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Send Qty.">
                                    <ItemTemplate>
                                        <asp:TextBox ID="sendq" BackColor="Transparent" Enabled="false" Font-Bold="true" runat="server" Width="65px" MaxLength="12" onkeyup="NumericDecimalCheck(this,5)"
                                            onblur="Spc_validatornumeric(this)" Text='<%# Eval("Recd_Qty") %>'>0</asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#CCCC99" />
                            <PagerStyle BackColor="#719cb6" ForeColor="Black" HorizontalAlign="center" />
                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                                Height="20px" Font-Size="10pt" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </div>
        <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
        <script src="../../JS/table2excel.js"></script>
        <script type="text/javascript">
            $("body").on("click", "#btnExport", function () {
                $("[id*=gdnewproc]").table2excel({
                    filename: "State_Level_District_Branch_Wise_Rent_Summary.xls"
                });
            });
        </script>
    </form>
</body>
</html>
