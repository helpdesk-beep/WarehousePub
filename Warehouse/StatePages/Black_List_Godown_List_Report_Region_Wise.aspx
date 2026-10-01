<%@ Page Language="C#" AutoEventWireup="true" CodeFile="~/StatePages/Black_List_Godown_List_Report_Region_Wise.aspx.cs" Inherits="StatePages_Black_List_Godown_List_Report_Region_Wise" %>

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
            font-size: xx-large;
        }

            .Grid td {
                padding: 2px;
                border: solid 1px #c1c1c1;
            }

            .Grid th {
                padding: 4px 2px;
                color: #fff;
                background: #557db0 url(Images/grid-header.png) repeat-x top;
                border-left: solid 1px #525252;
                font-size: 25px;
                text-align: center;
                font-size: xx-large;
            }

            .Grid .alt {
                background: #fcfcfc url(Images/grid-alt.png) repeat-x top;
            }

            .Grid .pgr {
                background: #557db0 url(Images/grid-pgr.png) repeat-x top;
            }

                .Grid .pgr table {
                    margin: 3px 0;
                    font-size: xx-large;
                }

                .Grid .pgr td {
                    border-width: 0;
                    padding: 0 6px;
                    border-left: solid 1px #666;
                    font-weight: bold;
                    color: #fff;
                    line-height: 12px;
                    font-size: xx-large;
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
            prtWindow.document.write('<html><head >Black List Godown Report</head>');
            prtWindow.document.write('<body style="background:none !important;">');
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
            Width: 110px;
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

        #container {
            display: flex; /* establish flex container */
            flex-direction: row; /* default value; can be omitted */
            flex-wrap: nowrap; /* default value; can be omitted */
            justify-content: space-between; /* switched from default (flex-start, see below) */
            background-color: lightyellow;
        }

            #container > div {
                /*width: 140px;*/
                height: 130px;
                /*border: 2px dashed red;*/
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div id="container" runat="server">
                <div>
                    <img src="../../images/mpwlc.png" />
                </div>
                <div>
                    <span style="font-size: 39px; font-family: bold; text-align: left">M.P. WAREHOUSING & LOGISTICS CORPORATION</span>
                    <br />
                    <span style="font-size: 30px; font-family: bold; text-align: left; margin-left: 190px;">Black List Godown List </span>
                </div>
                <%--<div><span style="WIDTH: 52.68mm; HEIGHT: 6.35mm;">Date:-</span> <asp:Label ID="labelName" runat="server"></asp:Label> --%>
                <br />
                <br />
                <br />
                <br />
                <br />
                <%--<span style="font-size: 20px; font-family:bold;">Qty In M.T.</span>--%>
                <%--</div>--%>
            </div>
        </div>
        <div class="container py-4">
            <div class="card">
                <div class="card-body">
                    <asp:Button ID="Button2" runat="server" Text="ExportToPDF" CssClass="button button2" OnClientClick="printGrid()" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <input type="button" id="btnExport" value="Exporttoexcel" class="button button2" />
                </div>
            </div>

        </div>
        <table style="border: solid 5px #e3e3e8; width: 100%; vertical-align: central;">
            <tr>
                <td valign="Center">
                    <%--<span style="color: brown; font-size: 10pt; font-weight: bold;">Total Number of Black Listed Godown:
                                                                 <asp:Label ID="lblNoofAC" runat="server" Text=""></asp:Label></span>--%>
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                      &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                                 &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;
                                                            <span style="color: brown; font-size: 12pt; font-weight: bold">Black Listed Godown Report
                                                            </span></td>
            </tr>
            <hr />
            <tr>
                <td style="text-align: left;">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" ShowFooter="false"
                        CssClass="Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
                        EmptyDataText="No Records Found." EmptyDataRowStyle-Font-Size="Large"
                        EmptyDataRowStyle-Font-Bold="true" EmptyDataRowStyle-ForeColor="Red">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No." HeaderStyle-Font-Size="Medium" ItemStyle-Font-Size="Medium">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1%>
                                </ItemTemplate>
                                <ItemStyle Width="1%" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="region" HeaderStyle-Font-Size="Medium" ItemStyle-Font-Size="Medium" HeaderText="Region Name" />
                            <asp:BoundField DataField="District_Name" HeaderStyle-Font-Size="Medium" ItemStyle-Font-Size="Medium" HeaderText="District Name" />
                            <asp:BoundField DataField="DepotName" HeaderStyle-Font-Size="Medium" ItemStyle-Font-Size="Medium" HeaderText="Branch Name" />
                            <asp:BoundField DataField="Godown_Name" HeaderStyle-Font-Size="Medium" ItemStyle-Font-Size="Medium" ItemStyle-HorizontalAlign="Center" HeaderText="Godown Name" />
                            <asp:BoundField DataField="GodownJVSRegistrationId" HeaderStyle-Font-Size="Medium" ItemStyle-Font-Size="Medium" ItemStyle-HorizontalAlign="Center" HeaderText="Godown JVS Registration Id" />
                            <asp:BoundField DataField="GodownJVSRegistrationIdByRM" HeaderStyle-Font-Size="Medium" ItemStyle-Font-Size="Medium" ItemStyle-HorizontalAlign="Center" HeaderText="Godown JVS Registration Id By RM" />
                            <asp:BoundField DataField="BlackList_OrderNo" HeaderStyle-Font-Size="Medium" ItemStyle-Font-Size="Medium" ItemStyle-HorizontalAlign="Center" HeaderText="Black List Order No" />
                            <asp:BoundField DataField="BlackListOrderDate" HeaderStyle-Font-Size="Medium" ItemStyle-Font-Size="Medium" ItemStyle-HorizontalAlign="Center" HeaderText="Black List Order Date" />
                            <asp:BoundField DataField="BlackListedTillDate" HeaderStyle-Font-Size="Medium" ItemStyle-Font-Size="Medium" ItemStyle-HorizontalAlign="Center" HeaderText="Black Listed Till Date" />
                            <asp:BoundField DataField="Remarks" HeaderStyle-Font-Size="Medium" ItemStyle-Font-Size="Medium" ItemStyle-HorizontalAlign="Center" HeaderText="Remarks" />
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
                    filename: "Rpt_Black_List_Godown.xls"
                });
            });
        </script>
    </form>
</body>
</html>
