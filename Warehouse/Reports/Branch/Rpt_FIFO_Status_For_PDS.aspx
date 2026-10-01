<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="Rpt_FIFO_Status_For_PDS.aspx.cs" Inherits="Reports_Branch_Rpt_Rpt_FIFO_Status_For_PDS" %>

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
            var prtGrid = document.getElementById('<%=gdstackdetail.ClientID %>');
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
            var gridData = document.getElementById('<%= gdstackdetail.ClientID %>');
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
        <table style="border: solid 5px #e3e3e8; width: 100%; vertical-align: central;">
            <tr>
                <td style="padding-top: 20px; padding-bottom: 20px; background-color: skyblue; font-family: 'Times New Roman'; font-size: 20pt; color: white; text-align: center;">WHR Details & Available Stock for Issue (FIFO)
                </td>
            </tr>
            <tr style="background-color: forestgreen; height: 25px">
                <td valign="Center">
                    <span style="color: White; font-size: 10pt; font-weight: bold;">Total Number of WHR :
                                                                 <asp:Label ID="lblNoofAC" runat="server" Text=""></asp:Label>
                    </span>
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                                 &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                                 &nbsp; &nbsp; &nbsp; &nbsp;
                </td>
            </tr>
            <tr>
                <td style="height: 5px"></td>
            </tr>
            <tr>
                <td style="text-align: left;">
                    <asp:GridView ID="gdstackdetail" runat="server" CellPadding="2" TabIndex="10" Width="100%"
                        ForeColor="Navy" AutoGenerateColumns="False" OnRowDataBound="gdstackdetail_RowDataBound" ShowFooter="true">
                        <columns>
                            <asp:TemplateField HeaderText="S.No." ItemStyle-Width="3%">
                                <itemtemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                     <asp:HiddenField runat="server" ID="hdndiffirence" Value='<%# Eval("FIFOFrizwedStock") %>' />
                                     <asp:HiddenField runat="server" ID="hdnQty" Value='<%# Eval("Qty") %>' />

                                </itemtemplate>
                            </asp:TemplateField>
                            <%--<asp:BoundField DataField="SNo" HeaderText="S.No.">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>--%>
                            <asp:BoundField DataField="Depositor_Name" HeaderText="Depositer Name">
                                <itemstyle horizontalalign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CropYear" HeaderText="Crop Year">
                                <itemstyle horizontalalign="Center" width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Godown" HeaderText="Godown">
                                <itemstyle horizontalalign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity Name">
                                <itemstyle horizontalalign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Depositor_whr_id" HeaderText="Whr No">
                                <itemstyle horizontalalign="Left" width="100px" />
                            </asp:BoundField>

                            <asp:BoundField DataField="Qty" HeaderText="Freeze Qty. for FIFO">
                                <itemstyle horizontalalign="Right" />
                            </asp:BoundField>
                             <asp:BoundField DataField="FIFOFrizwedStock" HeaderText="Delivered Qty.">
                                <itemstyle horizontalalign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="AvailQty" HeaderText="Real Time Balance Qty.">
                                <itemstyle horizontalalign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Godown_ID" HeaderText="Godown ID">
                                <itemstyle horizontalalign="Left" />
                            </asp:BoundField>

                            <asp:BoundField DataField="WHR_Issue_Date" HeaderText="WHR Date" />
                        </columns>
                        <footerstyle backcolor="#5D7B9D" font-bold="True" forecolor="White" />
                        <rowstyle backcolor="#FFFBD6" forecolor="#333333" bordercolor="#FFC080" horizontalalign="Center" />
                        <selectedrowstyle backcolor="#FFCC66" font-bold="True" forecolor="Navy" />
                        <pagerstyle backcolor="#FFCC66" forecolor="#333333" horizontalalign="Center" bordercolor="White" />
                        <headerstyle backcolor="#719cb6" font-bold="True" forecolor="White" horizontalalign="center"
                            height="20px" font-size="10pt" />
                        <alternatingrowstyle backcolor="White" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
    <script type="text/javascript" src="<%= ResolveUrl("~/NEW_CSS/js/jquery-1.12.4.js") %>"></script>
    <script src="../../JS/table2excel.js"></script>
    <script type="text/javascript">
        $("body").on("click", "#btnExport", function () {
            $("[id*=gdstackdetail]").table2excel({
                filename: "State_Level_District_Branch_Wise_Rent_Summary.xls"
            });
        });
    </script>
</asp:Content>

