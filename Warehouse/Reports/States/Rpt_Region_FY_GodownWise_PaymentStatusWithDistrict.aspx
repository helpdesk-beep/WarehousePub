<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Rpt_Region_FY_GodownWise_PaymentStatusWithDistrict.aspx.cs" Inherits="Region_State_Rpt_Region_FY_GodownWise_PaymentStatus" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title style="color: white;">Godown Wise Payment Status</title>

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
    <%--<script type="text/javascript">
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
    </script>--%>
    <%--<script type="text/javascript">
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
                <h4 class="header">Report For Review of Region,&nbsp; Financial Year Wise, Godown wise Payment Position Report (Amount in Cr Rs) </h4>
            </div>
            <div class="col-md-12">
                <div class="form-group">
                    <div class="col-sm-8 col-sm-offset-4">
                        <asp:Label ID="lblmsg" runat="server"></asp:Label>
                        <asp:HiddenField ID="hfId" Value="0" runat="server" />
                        <asp:HiddenField ID="hfFileName" Value="0" runat="server" />
                    </div>
                </div>
                <div>
                    <table style="border: solid 5px #e3e3e8; width: 80%; vertical-align: central; margin-left: 10%;">
                        <tr>
                            <td style="text-align: center;">&nbsp;&nbsp;<asp:Label ID="lblRegion" Font-Bold="true" runat="server" ForeColor="Navy">संभाग:</asp:Label>&nbsp;&nbsp;
                        <asp:DropDownList ID="ddlRegion" runat="server" selectionmode="Multiple" AutoPostBack="true" OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged">
                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                                &nbsp;&nbsp;&nbsp;&nbsp;
                                 &nbsp;&nbsp;
                                <asp:Label ID="lblDistrict" Font-Bold="true" runat="server" ForeColor="Navy">जिला:</asp:Label>&nbsp;&nbsp;
                        &nbsp;&nbsp;<asp:DropDownList ID="ddldistrict" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged" selectionmode="Multiple">
                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                        </asp:DropDownList>&nbsp;&nbsp;&nbsp;<asp:Label ID="lblBranch" Font-Bold="true" runat="server" ForeColor="Navy">शाखा:</asp:Label>&nbsp;&nbsp; &nbsp;<asp:DropDownList ID="ddlbranch" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlbranch_SelectedIndexChanged" selectionmode="Multiple" Height="16px">
                             <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                         </asp:DropDownList>
                                &nbsp;&nbsp;<asp:Label ID="lblFY" Font-Bold="true" runat="server" ForeColor="Navy">वित्‍तीय वर्ष:</asp:Label>&nbsp;&nbsp; &nbsp;
                                <asp:DropDownList ID="ddlFinancialYear" runat="server" selectionmode="Multiple">
                                 <asp:ListItem Value="0">--Select--</asp:ListItem>
                                 <asp:ListItem Value="2010-11">2010-11</asp:ListItem>
                                 <asp:ListItem Value="2011-12">2011-12</asp:ListItem>
                                 <asp:ListItem Value="2012-13">2012-13</asp:ListItem>
                                 <asp:ListItem Value="2013-14">2013-14</asp:ListItem>
                                 <asp:ListItem Value="2014-15">2014-15</asp:ListItem>
                                 <asp:ListItem Value="2015-16">2015-16</asp:ListItem>
                                 <asp:ListItem Value="2016-17">2016-17</asp:ListItem>
                                 <asp:ListItem Value="2017-18">2017-18</asp:ListItem>
                                 <asp:ListItem Value="2018-19">2018-19</asp:ListItem>
                                 <asp:ListItem Value="2019-20">2019-20</asp:ListItem>
                                 <asp:ListItem Value="2020-21">2020-21</asp:ListItem>
                                 <asp:ListItem Value="2021-22">2021-22</asp:ListItem>
                                 <asp:ListItem Value="2022-23">2022-23</asp:ListItem>
                                 <asp:ListItem Value="2023-24">2023-24</asp:ListItem>
                                 <asp:ListItem Value="2024-25">2024-25</asp:ListItem>
                             </asp:DropDownList> &nbsp;&nbsp;
                                <asp:Label ID="lblGodownType" Font-Bold="true" runat="server" ForeColor="Navy">गोदाम का प्रकार:</asp:Label><asp:DropDownList ID="ddlGodownType" runat="server" selectionmode="Multiple">
                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                       
                            </td>
                        </tr>
                        <tr>
                            <td>

                            <%--<asp:Label ID="lblGodownType" Font-Bold="true" runat="server" ForeColor="Navy">गोदाम का प्रकार:</asp:Label>--%>
                                </td>
                        </tr>

                        <tr>
                            <td colspan="8" style="text-align: center;">
                                <asp:Button ID="btnSearch" CssClass="btn btn-success btn-sm" ValidationGroup="A"
                                    runat="server" Text="Search" OnClick="btnSearch_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
                        </tr>
                    </table>
                    <br />
                    <div class="form-group"></div>
                </div>
                <div>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
                <div class="col-md-12">
                    <div class="table-responsive">

                        <div style="overflow-x: scroll;">
                            <asp:GridView ID="GV_FYGodownWisePayment" runat="server" AutoGenerateColumns="False" Width="100%" BackColor="White"
                                EnableModelValidation="True"
                                CellPadding="4" rules="all" ForeColor="#333333" ShowFooter="true"
                                OnRowDataBound="GV_FYGodownWisePayment_OnRowDataBound" OnDataBound="GV_FYGodownWisePayment_OnDataBound"
                                OnRowCreated="GV_FYGodownWisePayment_OnRowCreated">
                                <RowStyle BackColor="White" CssClass="ADFieldsGridText table-responsive" HorizontalAlign="Left" />
                                <Columns>

                                    <asp:TemplateField HeaderText="S.No." ItemStyle-Width="3%">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="संभाग">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRegion" runat="server" Text='<%# Eval("RegionName") %>'>0</asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="जिला">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDistrict" runat="server" Text='<%# Eval("DistrictName") %>'>0</asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="शाखा">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBranch" runat="server" Text='<%# Eval("BranchName") %>'>0</asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="वित्‍तीय वर्ष">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFinancialYear" runat="server" Height="21px" Text='<%# Eval("FinancialYear") %>'> 
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="गोदाम का प्रकार">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGodownType" runat="server" Text='<%# Eval("GodownType") %>'>0</asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
<%--                                    <asp:TemplateField HeaderText="गोदाम संख्या">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGodownCnt" runat="server" Text='<%# Eval("TotalGodownCount") %>'>0</asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>

                                    <asp:TemplateField HeaderText="वित्‍तीय वर्ष में किराये की कुल राशि (राशि Cr.रुपये में)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFY_GodownRentAmount" Enabled="false" runat="server" Text='<%# Eval("TotalAmountInRentInFY") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="वित्‍तीय वर्ष में भुगतान राशि (राशि Cr.रुपये में)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFY_AmountPaymentToGodown" Enabled="false" runat="server" Text='<%# Eval("TotalAmountPaidToGodownOwnerInFY") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="गोदाम संचालक की शेष लंबित राशि (राशि Cr.रुपये में)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGodownOwnerPendingAmount" Enabled="false" runat="server" Text='<%# Eval("RemainingAmountOfGodownOwner") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="#719cb6" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" Font-Size="12pt" />
                                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                                    Height="20px" Font-Size="12pt" />
                                <AlternatingRowStyle BackColor="#eeeeee" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
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
    </form>
    <!--Java Script -->

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"></script>
    <script type="text/javascript" src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <script src="//code.jquery.com/jquery-1.10.2.js"></script>
    <script src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>
</body>
</html>
