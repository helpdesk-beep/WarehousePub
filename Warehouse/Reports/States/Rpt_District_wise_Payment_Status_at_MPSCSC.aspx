<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Rpt_District_wise_Payment_Status_at_MPSCSC.aspx.cs" Inherits="Reports_State_Rpt_District_wise_Payment_Status_at_MPSCSC" %>

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
     <script src="../../JS/gridviewscroll.js"></script>
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
    <%-- <script type="text/javascript">
         var gridViewScroll = null;
         window.onload = function () {
             gridViewScroll = new GridViewScroll({
                 elementID: "GridView1",
                 width: 'auto',
                 //element.style.width = 'auto';
                 height: 900,
                 freezeColumn: true,
                 freezeFooter: true,
                 freezeColumnCssClass: "GridViewScrollItemFreeze",
                 // freezeFooterCssClass: "GridViewScrollFooterFreeze",
                 //freezeHeaderRowCount: 2,
                 //freezeColumnCount: 3,
                 onscroll: function (scrollTop, scrollLeft) {
                     console.log(scrollTop + " - " + scrollLeft);
                 }
             });
             gridViewScroll.enhance();
         }
         function getScrollPosition() {
             var position = gridViewScroll.scrollPosition;
             alert("scrollTop: " + position.scrollTop + ", scrollLeft: " + position.scrollLeft);
         }
         function setScrollPosition() {
             var scrollPosition = { scrollTop: 50, scrollLeft: 50 };

             gridViewScroll.scrollPosition = scrollPosition;
         }
    </script>--%>
</head>
<body>
    <form id="form1" runat="server">
        <div style="height:500px;">
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
             <h1 style="text-align:center;">Region Wise Payment Status in Cr.</h1>
            <table style="border: solid 5px #e3e3e8; width: 100%; vertical-align: central;">
               
                <tr>
                    <td style="text-align: left;" colspan="10">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" ShowFooter="true"
                            CssClass="Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr" OnDataBound="OnDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="District Name">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="sdffsft" runat="server" Target="_blank" NavigateUrl='<%#"~/Reports/States/District_Wise_Payment_Status.aspx?District_Id="+ (Eval("District_Id").ToString())%>'
                                            title="District Name" Text=' <%# Eval("District_Name") %>' ForeColor="Blue"></asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>
                                <asp:BoundField DataField="NoofGodown" HeaderText="No. of Godown" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="NoOfGenerateBill" HeaderText="In No.'s" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="BillAmt" HeaderText="In Amount" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="NoOfGenerateBillSubmitted" HeaderText="In No.'s" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="SubmittedBillAmt" HeaderText="In Amount" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="NoofbillPaymentReceivedFromMPSCSC" HeaderText="In No.'s" ItemStyle-HorizontalAlign="Right" />
                                <%--<asp:BoundField DataField="TotalBillReceivedPayment" HeaderText="Total Bill Received Payment" ItemStyle-HorizontalAlign="Right" />--%>
                                <asp:BoundField DataField="Gross_Amount" HeaderText="In Amount" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="PendingbillatMPSCSC" HeaderText="In No.'s" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="PendingAmountatMPSCSC" HeaderText="In Amount" ItemStyle-HorizontalAlign="Right" />
                               
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
                    filename: "Godown_WisePayment_Status.xls"
                });
            });
        </script>
    </form>
</body>
</html>
