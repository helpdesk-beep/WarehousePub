<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="Rpt_Godown_Bill_Status.aspx.cs" Inherits="Reports_Branch_Rpt_Godown_Bill_Status" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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

        .header {
            padding: 2px;
            text-align: center;
            background: #1abc9c !important;
            color: white;
        }
    </style>

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
        <div style="text-align: center; font-size: large;">
            <h2 class="header">PVT Godown  Storage Charges Bill</h2>
        </div>
        <%--<div style="text-align: center; font-size: large;">

            <asp:Label ID="Label2" runat="server" Text="Select Commodity"></asp:Label>

            <asp:DropDownList ID="drpDwnCommodity" Height="25px" Width="300px" runat="server" OnSelectedIndexChanged="drpDwnCommodity_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>

        </div>--%>
        <table style="border: solid 5px #e3e3e8; width: 100%; vertical-align: central;">
            <tr>
                <td style="width: 100px" align="right">
                    <asp:Label ID="Label2" runat="server" Text="Godown Name" Font-Size="10pt" Font-Bold="true"></asp:Label>
                </td>
                <td style="width: 200px" align="left">
                    <asp:DropDownList ID="ddlGodown" runat="server" Height="25px" Width="155px" AutoPostBack="true"
                        CssClass="tb6" OnSelectedIndexChanged="ddlGodown_SelectedIndexChanged">
                        <asp:ListItem Value="0">--All--</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="width: 100px" align="right">
                    <asp:Label ID="Label1" runat="server" Text="Month Name" Font-Size="10pt" Font-Bold="true"></asp:Label>
                </td>
                <td style="width: 200px" align="left">
                    <asp:DropDownList ID="ddlmonth" runat="server" Height="25px" Width="155px" AutoPostBack="true"
                        CssClass="tb6" OnSelectedIndexChanged="ddlmonth_SelectedIndexChanged">
                        <asp:ListItem Value="0">--All--</asp:ListItem>
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
                </td>
            </tr>
            <tr>
                <td colspan="4"></td>
            </tr>
            <tr>
                <td style="text-align: left;" colspan="4">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                        CssClass="Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No." ItemStyle-Width="3%">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Godown_Name" HeaderText="Godown Name" />
                            <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity" />
                            <asp:BoundField DataField="Crop_Year" HeaderText="Crop Year" />
                            <asp:BoundField DataField="Financial_Year" HeaderText="Financial Year" />
                            <asp:BoundField DataField="Bill_Month" HeaderText="Bill Month" />
                            <asp:BoundField DataField="Bill_Number" HeaderText="Bill Number" />
                            <asp:BoundField DataField="RentBillNO" HeaderText="Rent Bill Number" />
                            <asp:BoundField DataField="Net_Amount" HeaderText="Net Amount" />
                            <asp:BoundField DataField="DSC_BY_BM_SC" HeaderText="DSC BY BM Storage Chrg" />
                            <asp:BoundField DataField="DSC_BY_BM_GR" HeaderText="DSC BY BM Godown Rent" />
                            <asp:BoundField DataField="DSC_BY_MG_GR" HeaderText="DSC BY Gdwn Owner" />
                            <asp:BoundField DataField="RC_Dituction" HeaderText="RC Dituction" />
                            <asp:BoundField DataField="DSC_BY_ICM" HeaderText="DSC BY ICI" />
                            <asp:BoundField DataField="DSC_BY_RM" HeaderText="DSC BY RM" />
                            <asp:BoundField DataField="RecievedAmount" HeaderText="Recieved Amount From DM(MPSCSC)" />
                            <asp:BoundField DataField="PaymentToGO" HeaderText="NEFT Payment to Godown Owner" />
                            <asp:BoundField DataField="UTR_NUMBER" HeaderText="UTR NUMBER" />
                            <asp:BoundField DataField="TRANSACTION_DATE" HeaderText="TRANSACTION DATE" />
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
                filename: "ABC.xls"
            });
        });
    </script>
</asp:Content>



