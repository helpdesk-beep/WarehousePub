<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Region_Master.master" AutoEventWireup="true" CodeFile="Rpt_Branch_Wise_Pandancy_at_Varius_Level_after_recieved_payment.aspx.cs" Inherits="Region_Reports_Rpt_Branch_Wise_Pandancy_at_Varius_Level_after_recieved_payment" %>


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
    </style>

    <div>       
        <div id="divshowdetails" runat="server" visible="false">
            <div style="text-align: center; font-size: large;">
                 <asp:Label ID="Label5" runat="server" Text="Commodity"></asp:Label>
                <asp:DropDownList ID="ddlcomodity" Height="25px" Width="300px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlcomodity_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:Button ID="Button2" runat="server" Text="ExportToPDF" CssClass="button button2" OnClientClick="printGrid()" />
                &nbsp;&nbsp;
                <input type="button" id="btnExport" value="Exporttoexcel" class="button button2" />

            <br />

            <table style="border: solid 5px #e3e3e8; width: 100%; vertical-align: central;">

                <tr>
                    <td style="text-align: left;">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" ShowFooter="true"
                            OnDataBound="OnDataBound"
                            CssClass="Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No." ItemStyle-Width="3%">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Branch Name">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="sdffsft" runat="server" Target="_blank" NavigateUrl='<%#"~/Reports/Region/Rpt_Godown_Wise_Pandancy_at_Varius_Level_after_recieved_payment.aspx?BranchId="+ (Eval("BranchId").ToString())%>'
                                            title="Branch Name" Text=' <%# Eval("DepotName") %>' ForeColor="Blue"></asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity Name" />
                                <asp:BoundField DataField="noofbill" HeaderText="Total No of Bill Number" HeaderStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="Amount" HeaderText="Total Bill Amount"  HeaderStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="noofbillleftforbmdeuction" HeaderText="no of bill left for BM Deuction" HeaderStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="noofbillleftRMPassingorder" HeaderText="no of bill left RM Passing order" HeaderStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="noofbillleftforRMDSC" HeaderText="no of bill left for RMDSC" HeaderStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="noofbillleftforpaymentfile" HeaderText="no of bill left for Paymentfile" HeaderStyle-HorizontalAlign="Right" />
                            </Columns>
                            <FooterStyle Font-Bold="True" ForeColor="Black" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="../../JS/table2excel.js"></script>
    <script type="text/javascript">
        $("body").on("click", "#btnExport", function () {
            $("[id*=GridView1]").table2excel({
                filename: "Rpt_Get_Payment_Credit_From_MPWLC_To_Godown_Wise.xls"
            });
        });
    </script>
</asp:Content>
