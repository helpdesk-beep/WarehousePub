<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Rpt_Get_Prapatra_II.aspx.cs" Inherits="Region_States_Rpt_Get_Prapatra_II" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
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
            var prtGrid = document.getElementById('<%=GrdPrapatraI.ClientID %>');
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
            var gridData = document.getElementById('<%= GrdPrapatraI.ClientID %>');
            var windowUrl = 'about:blank';
            //set print document name for gridview
            var uniqueName = new Date();
            var windowName = 'Print_' + uniqueName.getTime();
            location.reload();
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
            <table style="border: solid 5px #e3e3e8; width: 80%; vertical-align: central; margin-left: 10%;">
                <tr>
                    <td style="width: 150px" align="right">
                        <asp:Label ID="lblDistrict" runat="server" Text="Date (DD-MM-YYYY)" Font-Size="10pt" Font-Bold="true"></asp:Label>
                    </td>
                    <td style="width: 150px; margin-left:10px;" align="left">
                       <asp:TextBox ID="txtpaymentdate" runat="server"></asp:TextBox>
                    </td>
                    <td style="width: 150px" align="right">
                        <asp:Label ID="Label9" runat="server" Text="Region:"></asp:Label>
                    </td>
                    <td style="text-align: left;" class="auto-style1">
                        <asp:DropDownList ID="ddlregion" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlregion_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                   <td style="width: 100px" align="right">
                <asp:Button ID="btnshow" Text="Show Details" runat="server" CssClass="button button2" OnClick="btnshow_Click" />
                    </td>
                </tr>
              
            </table>
            <div style="text-align: center;">

            </div>
            <div id="showdetails" runat="server" visible="false" style="width: 100%;">
                <div class="container py-4">
                    <div class="card">

                        <div class="card-body">
                            <asp:Button ID="Button2" runat="server" Text="Print To PDF" CssClass="button button2" OnClientClick="printGrid()" />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                    <input type="button" id="btnExport" value="Export To Excel" class="button button2" />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button Text="Download To PDF" runat="server" OnClick="ExportToPDF" CssClass="button button2" />

                        </div>
                    </div>

                </div>
                <table style="border: solid 5px #e3e3e8; width: 100%; vertical-align: central; overflow:auto;">
                  
                    <tr>
                        <td style="text-align: left;">
                            <asp:GridView ID="GrdPrapatraI" runat="server" AutoGenerateColumns="False" Width="100%" Font-Names="Arial"
                                BorderStyle="Double" CellPadding="3" CellSpacing="5" ShowFooter="true" OnRowDataBound="GrdPrapatraI_RowDataBound"
                                Font-Size="11px" BorderColor="#CCCCCC" OnRowCreated="GrdPrapatraI_RowCreated">
                                <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <Columns>                                 
                                    <asp:BoundField DataField="Regionnm" HeaderText="2">
                                         <ItemStyle Width="100px" HorizontalAlign="Left"></ItemStyle>
                                    </asp:BoundField>
                                     <asp:BoundField DataField="Region_ID" HeaderText="Region_ID" />
                                    <asp:BoundField DataField="District_Name" HeaderText="3">
                                         <ItemStyle Width="100px" HorizontalAlign="Left"></ItemStyle>
                                    </asp:BoundField>                                   
                                     <asp:BoundField DataField="DepotName" HeaderText="4">
                                         <ItemStyle Width="100px" HorizontalAlign="Left"></ItemStyle>
                                    </asp:BoundField>
                                     <asp:BoundField DataField="Commodity_Name" HeaderText="4">
                                         <ItemStyle Width="100px" HorizontalAlign="Left"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Weight" HeaderText="5" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>

                                    <asp:BoundField DataField="Fat_grain_Weight" HeaderText="6" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>

                                    <asp:BoundField DataField="Total_A_B" HeaderText="7" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>

                                    <asp:BoundField DataField="MPWLC_OWN1" HeaderText="9" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>

                                    <asp:BoundField DataField="MPWLC_JVS1" HeaderText="10" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>

                                    <asp:BoundField DataField="MPWLC_HA1" HeaderText="11" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="MPWLC_CWC1" HeaderText="12" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Arremented_PVT_PEG1" HeaderText="13" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Markfed1" HeaderText="14" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="OILFED1" HeaderText="15" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Total_Godown_Capacity1" HeaderText="16" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="MPWLC_OWN2" HeaderText="17" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="MPWLC_Mandi_Cap2" HeaderText="18" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="MPWLC_Mandi_Shed2" HeaderText="19" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Markfed2" HeaderText="20" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Pvt_PEG_Cap2" HeaderText="21" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Total_Cap_Capacity2" HeaderText="22" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="MPWLC_OWN3" HeaderText="23" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="MPWLC_JVS3" HeaderText="24" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="MPWLC_HA3" HeaderText="25" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="MPWLC_CWC3" HeaderText="25" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Arremented_PVT_PEG3" HeaderText="26" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Markfed3" HeaderText="27" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="OILFED3" HeaderText="28" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Total_Godown_Capacity3" HeaderText="29" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="MPWLC_OWN4" HeaderText="30" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="MPWLC_Mandi_Cap4" HeaderText="31" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="MPWLC_Mandi_Shed4" HeaderText="31" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Markfed4" HeaderText="32" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Pvt_PEG_Cap4" HeaderText="33" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Total_Cap_Capacity4" HeaderText="34" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <%--<asp:BoundField DataField="Remark" HeaderText="34" ItemStyle-HorizontalAlign="Left">
                                <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>--%>
                                    <asp:BoundField DataField="MPWLC_OWN5" HeaderText="35" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="MPWLC_JVS5" HeaderText="36" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="MPWLC_HA5" HeaderText="37" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="MPWLC_CWC5" HeaderText="38" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Arremented_PVT_PEG5" HeaderText="39" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Markfed5" HeaderText="40" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="OILFED5" HeaderText="41" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Total_Godown_Capacity5" HeaderText="42" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="MPWLC_OWN6" HeaderText="43" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="MPWLC_Mandi_Cap5" HeaderText="44" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="MPWLC_Mandi_Shed5" HeaderText="45" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Markfed6" HeaderText="46" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Pvt_PEG_Cap5" HeaderText="47" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Total_Cap_Capacity5" HeaderText="48" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                </Columns>
                                <HeaderStyle ForeColor="#C70039" HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="11px" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <script type="text/javascript" src="<%= ResolveUrl("~/NEW_CSS/js/jquery-1.12.4.js") %>"></script>
        <script src="../../JS/table2excel.js"></script>
        <script type="text/javascript">
            $("body").on("click", "#btnExport", function () {
                $("[id*=GridView1]").table2excel({
                    filename: "Payment_Received_From_MPSCSC_Details_From_Aug.xls"
                });
            });
        </script>
         <script>
        $(document).ready(function () {
            $("[id$=txtpaymentdate]").datepicker({
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
    
    <script type="text/javascript" src="<%= ResolveUrl("~/NEW_CSS/js/jquery-1.12.4.js") %>"></script>
    <script type="text/javascript" src="<%= ResolveUrl("~/assets/js/bootstrap.min.js") %>"></script>
    <script src="<%= ResolveUrl("~/NEW_CSS/js/jquery-1.12.4.js") %>"></script>
    <script src="<%= ResolveUrl("~/NEW_CSS/js/jquery-ui.js") %>"></script>
</body>
</html>
