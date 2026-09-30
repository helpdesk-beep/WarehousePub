<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CropYearWiseRiceStock.aspx.cs" Inherits="Reports_States_Payment_BillsFromCSMStoMPWLC_Status_Rept_Region_Wise" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title style="color: white;">Payment Received From MPSCSC</title>

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
            var prtGrid = document.getElementById('<%=grdavlqty.ClientID %>');
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
            var gridData = document.getElementById('<%= grdavlqty.ClientID %>');
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
                <h4 class="header">Branch ,Crop Year Wise Fill Details in (Quintal)</h4>
            </div>
            <div style="text-align: center; font-size: large;">

                <asp:Label ID="Label1" runat="server" Text="Region"></asp:Label>
                <asp:DropDownList ID="ddlRegion" Height="25px" Width="300px" runat="server" AutoPostBack="false">
                </asp:DropDownList>

                <asp:Label ID="Label2" runat="server" Text="Commodity"></asp:Label>
                <asp:DropDownList ID="ddlComodity" Height="25px" Width="300px" runat="server" AutoPostBack="false">
                </asp:DropDownList>

                <asp:Label ID="Label3" runat="server" Text="Crop Year"></asp:Label>
                <asp:DropDownList ID="ddlCropYear" Height="25px" Width="300px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCropYear_SelectedIndexChanged">
                </asp:DropDownList>
                <%--  <asp:Label ID="Label2" runat="server" Text="District"></asp:Label>
                <asp:DropDownList ID="ddldistrict" Height="25px" Width="300px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged">
                </asp:DropDownList>

                <asp:Label ID="Label3" runat="server" Text="Branch"></asp:Label>
                <asp:DropDownList ID="ddlbranch" Height="25px" Width="300px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlbranch_SelectedIndexChanged">
                </asp:DropDownList>--%>
                <br />

            </div>
            <table style="border: solid 5px #e3e3e8; width: 100%; vertical-align: central;">

                <tr>
                    <td style="text-align: left;">
                        <asp:GridView ID="grdavlqty" runat="server" AutoGenerateColumns="False" ShowFooter="true"
                            CssClass="Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No." ItemStyle-Width="3%">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                          <asp:HiddenField runat="server" ID="hdnBranchID" Value='<%# Eval("BranchID") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="District_Name" HeaderText="District Name" />
                                <asp:BoundField DataField="DepotName" HeaderText="Branch Name" />
                                <asp:BoundField DataField="CropYear" HeaderText="CropYear" />
                                <asp:TemplateField HeaderText="Available Qty as Per Online in(Quintal)" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOnlineQty" runat="server" Text='<%# Eval("OnlineQty") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Actual Stock Position in(Quintal)" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:TextBox ID="lblAvlQty" runat="server" Text='<%# Eval("AvlQty") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                            <FooterStyle Font-Bold="True" ForeColor="Black" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
            <div id="divbtn" runat="server" visible="false" style="text-align: center;">
                <asp:Button CssClass="btn btn-warning" ID="btn_saveInspDate" runat="server" Text="Submit" Visible="true" OnClick="btn_saveInspDate_Click"></asp:Button>
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
