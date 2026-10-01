<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/StateMaster.master" AutoEventWireup="true" CodeFile="Rpt_Dist_Yearwise_StockPosition.aspx.cs" Inherits="StatePages_Rpt_Dist_Yearwise_StockPosition" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

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
<%--   <script type="text/javascript">
        function PrintGridData() {
            var prtGrid = document.getElementById('<%=GV_StockReport.ClientID %>');
            prtGrid.border = 0;
            var prtwin = window.open('', 'PrintGridViewData', 'left=100,top=100,width=1000,height=1000,tollbar=0,scrollbars=1,status=0,resizable=1');
            prtwin.document.write(prtGrid.outerHTML);
            prtwin.document.close();
            prtwin.focus();
            prtwin.print();
            prtwin.close();
        }
    </script>--%>
<%--   <script type="text/javascript">
        function printGrid() {
            var gridData = document.getElementById('<%= GV_StockReport.ClientID %>');
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
   </script>--%>
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
   <div>
        <div class="container py-4">
            <div class="card">
                <div class="card-body">
<%--                    <asp:Button ID="Button2" runat="server" Text="ExportToPDF" CssClass="button button2" OnClientClick="printGrid()" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <input type="button" id="btnExport" value="Exporttoexcel" class="button button2" />--%>
<%--                                            <asp:Button runat="server" Text="Export Report Data To Excel " ID="btnExportData" OnClick="btnExportData_Click" Height="25px"  Font-Bold="true" CssClass="auto-style2" />--%>

                </div>
            </div>

        </div>
        <br />
        <br />
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblCropYear" runat="server" Font-Bold="True" Font-Size="10pt" Text="Crop Year"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                    <asp:DropDownList ID="ddlCropYear" runat="server" AutoPostBack="false" Width="150px">
                    <asp:ListItem Text="2018-19"></asp:ListItem>
                    <asp:ListItem Text="2019-20"></asp:ListItem>
                    <asp:ListItem Text="2020-21"></asp:ListItem>
                    <asp:ListItem Text="2021-22"></asp:ListItem>
                    <asp:ListItem Text="2022-23"></asp:ListItem>
                    <asp:ListItem Text="2023-24"></asp:ListItem>
                    <asp:ListItem Text="2024-25"></asp:ListItem>
                    <%--<asp:ListItem Text="2016-17"></asp:ListItem>
                    <asp:ListItem Text="2015-16"></asp:ListItem>
                    <asp:ListItem Text="2014-15"></asp:ListItem>
                    <asp:ListItem Text="2013-14"></asp:ListItem>
                    <asp:ListItem Text="2012-13"></asp:ListItem>--%>
                    </asp:DropDownList>
                </td>


            </tr>
            <tr>&nbsp;&nbsp;</tr>
            <tr>

            <td>
            <asp:Label ID="lblRegion" runat="server" Font-Bold="True" Text="Region" Font-Size="10pt"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
            <asp:DropDownList ID="ddlRegion" Height="25px" Width="300px" runat="server" OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged" AutoPostBack="true" ></asp:DropDownList>
                </td>
                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                <td>
            <asp:Label ID="lblDistrict" runat="server"  Font-Bold="True" Text="District" Font-Size="10pt"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
            <asp:DropDownList ID="ddlDistrict" Height="25px" Width="300px" runat="server" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"  AutoPostBack="true" ></asp:DropDownList>
                </td>
            </tr> 
            <tr>&nbsp;&nbsp;</tr>
            <tr>              
                <td>
            <%-- <asp:Label ID="lblCommodityType" runat="server" Font-Bold="True"  Font-Size="10pt" Text="Commodity Type"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:DropDownList ID="ddlCommodityType" Height="25px" Width="300px" runat="server" OnSelectedIndexChanged="ddlCommodityType_SelectedIndexChanged" >

                <asp:ListItem Text="Coarse Grains"></asp:ListItem>
                <asp:ListItem Text="Rice"></asp:ListItem>
                <asp:ListItem Text="Wheat-PSS"></asp:ListItem>
            </asp:DropDownList>
                </td>
                < <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                <td>--%>
            <asp:Label ID="lblCommodity" runat="server"  Font-Bold="True" Text="Commodity" Font-Size="10pt"></asp:Label>&nbsp;&nbsp;&nbsp;
                
            <asp:DropDownList ID="ddlCommodity" Height="25px" Width="300px" runat="server">
                <asp:ListItem Text="Jowar" Value ="11"></asp:ListItem>
                <asp:ListItem Text="Bajra" Value ="8"></asp:ListItem>
                <asp:ListItem Text="Maize" Value ="12"></asp:ListItem>
                <asp:ListItem Text="Rice" Value ="3"></asp:ListItem>
                <asp:ListItem Text="Wheat-PSS" Value ="22"></asp:ListItem>

            </asp:DropDownList>
            </td>
            </tr>
            <tr>&nbsp;&nbsp;</tr>
            <tr>
                <%--<td>
             <asp:Label ID="lblBranch" runat="server" Font-Bold="True"  Font-Size="10pt" Text="Branch"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
            <asp:DropDownList ID="ddlBranch" Height="25px" Width="300px" runat="server" ></asp:DropDownList>
                </td>--%>
                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                <td>
<%--             <asp:Label ID="lblAgency" runat="server" Font-Bold="True"  Font-Size="10pt" Text="Agency"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;--%>
            <%--<asp:DropDownList ID="ddlAgencyType" Height="25px" Width="300px" runat="server" >
                <asp:ListItem Text="FCI"></asp:ListItem>
                    <asp:ListItem Text="PDS"></asp:ListItem>
                    <asp:ListItem Text="MPSCSC"></asp:ListItem>
                    <asp:ListItem Text="NAFED"></asp:ListItem>

            </asp:DropDownList>--%>
                </td>
                < <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                <td>
                <td>
            </tr>
            <tr>&nbsp;&nbsp;</tr>
              <tr>              <td>
                    &nbsp;</td>
            </tr>
            </table>
        <div style="text-align: center; font-size: large;">
                    <asp:Button ID="btnView" runat="server" Text="View" Font-Bold="true" OnClick="btnView_Click" Width="132px" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        <asp:Button runat="server" Text="Export Report Data To Excel " ID="btnExportData" OnClick="btnExportData_Click" Height="25px"  Font-Bold="true" CssClass="auto-style2" />

        </div>
        <table style="border: solid 5px #e3e3e8; width: 100%; vertical-align: central;">
        <tr>
                <td style="text-align: left; overflow: scroll;">
                    <asp:GridView ID="GV_StockReport" runat="server" AutoGenerateColumns="False" ShowFooter="true"
                        CssClass="Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr" OnSelectedIndexChanged="GV_StockReport_SelectedIndexChanged">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No." ItemStyle-Width="3%">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:BoundField DataField="RegionName" HeaderText="Region"/>--%>
                            <asp:BoundField DataField="DistrictName" HeaderText="District"/>
                            <asp:BoundField DataField="BranchName/IssueCenter" HeaderText="Branch"/>
                            <asp:BoundField DataField="GodownName" HeaderText="Godown"/>
                            <asp:BoundField DataField="CropYear" HeaderText="Crop Year"/>
<%--                        <asp:BoundField DataField="AgencyType" HeaderText="Agency" />--%>
                            <asp:BoundField DataField="Commodity" HeaderText="Commodity"/>
                            <asp:BoundField DataField="Depositor" HeaderText="Depositor"/>
                            <asp:BoundField DataField="ArrivalSourceDetails" HeaderText="Arrival Source"/>
                            <asp:BoundField DataField="ReceivedQty" HeaderText="Received Qty"/>
                            <asp:BoundField DataField="DeliveredQty" HeaderText="Delivered Qty"/>
                           <asp:BoundField DataField="BalanceQty" HeaderText="Balance Qty" />
                            <asp:BoundField DataField="Remark" HeaderText="Remark/StockCondition" />
<%--                            <asp:BoundField DataField="NonFAQ-(Upgradable/Non-Upgradable)" HeaderText="Stock Condition" />--%>
                            <%--<asp:TemplateField HeaderText="View Details">
                    <ItemTemplate >     

                   <a id="Edit" visible="true" href="DetailGodownCapForState.aspx?TYPE=<%#Eval("Hired_Type")%>&BID=<%#Eval("BranchID")%>">View Deatils</a>
                    </ItemTemplate>--%>
<%--                </asp:TemplateField>--%>
                
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

