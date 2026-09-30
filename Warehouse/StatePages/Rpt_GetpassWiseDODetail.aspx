<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Rpt_GetpassWiseDODetail.aspx.cs" Inherits="StatePages_Rpt_GetpassWiseDODetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title style="color: white;">State Level Rerport</title>
    <link href="../assets/css/style.css" rel="stylesheet" />
    <!-- font awesome -->
    <link href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../assets/New/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assets/New/css/bootstrap.min2.css" rel="stylesheet" />
    <link href="../CSS/style.css" rel="stylesheet" />
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />
    <link href="../../assets/New/css/bootstrap-multiselect .css" rel="stylesheet" />

    <script type="text/javascript">  

        $(document).ready(function () {
            $("#EmployeeGridViewList").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable();
        });
    </script>

    <script src="../../JS/gridviewscroll.js"></script>
     <script type="text/javascript">
         var gridViewScroll = null;
         window.onload = function () {
             gridViewScroll = new GridViewScroll({
                 elementID: "GrdGodown",
                 width: 'auto',
                 //element.style.width = 'auto';
                 height: 900,
                 freezeColumn: true,
                 freezeFooter: true,
                 freezeColumnCssClass: "GridViewScrollItemFreeze",
                 freezeFooterCssClass: "GridViewScrollFooterFreeze",
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
                background: #557db0 url(Images/grid-header.png) repeat-x top;
                border-left: solid 1px #525252;
                font-size: 25px;
                text-align: center;
            }

            .Grid .alt {
                background: #fcfcfc url(Images/grid-alt.png) repeat-x top;
            }

            .Grid .pgr {
                background: #557db0 url(Images/grid-pgr.png) repeat-x top;
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
            var prtGrid = document.getElementById('<%=GrdGodown.ClientID %>');
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
            var gridData = document.getElementById('<%= GrdGodown.ClientID %>');
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
    <style>
        li.header {
            font-size: 16px !important;
        }

        span#ctl00_spnUsername {
            text-transform: uppercase;
            color: white;
            font-weight: 600;
            font-size: 16px;
        }

        li.dropdown.tasks-menu.classhide a {
            padding: 4px 10px 0px 0px;
        }

        .datepicker.datepicker-dropdown.dropdown-menu.datepicker-orient-left.datepicker-orient-top {
            z-index: 9999 !important;
        }

        .multiselect-native-select .multiselect {
            text-align: left !important;
        }

        .multiselect-native-select .multiselect-selected-text {
            width: 100% !important;
        }

        .multiselect-native-select .checkbox, .multiselect-native-select .dropdown-menu {
            width: 100% !important;
        }

        .multiselect-native-select .btn .caret {
            float: right !important;
            vertical-align: middle !important;
            margin-top: 8px;
            border-top: 6px dashed;
        }
    </style>
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

        .auto-style1 {
            height: 15px;
        }
    </style>
    <style>
        /* General table styling */
        .styled-table {
            width: 100%;
            border-collapse: collapse;
            margin: 25px 0;
            font-size: 18px;
            font-family: Arial, sans-serif;
            text-align: left;
        }

            /* Table headers */
            .styled-table thead tr {
                background-color: #009879;
                color: #ffffff;
                text-align: center;
                font-weight: bold;
            }

            /* Table rows */
            .styled-table tbody tr {
                border-bottom: 1px solid #dddddd;
            }

                /* Alternate row colors */
                .styled-table tbody tr:nth-of-type(even) {
                    background-color: #f3f3f3;
                }

                /* Hover effect */
                .styled-table tbody tr:hover {
                    background-color: #f1f1f1;
                }

            /* Cell padding */
            .styled-table td, .styled-table th {
                padding: 12px 15px;
            }

            /* Last row border fix */
            .styled-table tbody tr:last-of-type {
                border-bottom: 2px solid #009879;
            }

        table, th, td {
            border: 1px solid black;
        }
    </style>
    <style>    
            .LabelGrey     
            {    
                font-weight: bold;    
                color: darkgray;    
            }    
                
            .LabelBlue     
            {    
                font-weight: normal;    
                color: darkblue;    
            }    
                
            .ClassJoined     
            {    
                font-weight: normal;    
                font-size: 50px;    
                color: darkcyan;    
            }    
        </style>    
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div id="container" runat="server">
  <div><img src="../../images/mpwlc.png" width: 75px;/></div>
  <div ><span style="font-size: 39px; font-family:bold;"> M.P. WAREHOUSING & LOGISTICS CORPORATION</span>
        <br />
      <span style="font-size: 30px; font-family:bold; text-align:center;margin-left: 190px;"> Gatepass Wise DO Detail</span>
  </div>
  <div><span style="WIDTH: 52.68mm; HEIGHT: 6.35mm;">Date:-</span> <asp:Label ID="labelName" runat="server"></asp:Label> 
      <br />
      <br />
      <br />
      <br />
      <br />
      <span style="font-size: 20px; font-family:bold;">Qty In Qtl.</span>

  </div>
</div>   
       
            <table class="styled-table" style="border-left: 3px solid blue; border-right: 3px solid blue">
               <tr id="trgdnlist" runat="server" visible="true">
                        <td align="left" class="auto-style1">
                            <asp:Label ID="Label1" runat="server" Text="Gatepass No." ForeColor="navy" Font-Bold="true"
                                Font-Size="8pt"></asp:Label>
                        </td>
                        <td align="left" valign="middle" class="auto-style1">
                             <asp:Label ID="lblwhrno" runat="server" ForeColor="navy" Font-Bold="true"
                                Font-Size="8pt"></asp:Label>
                        </td>
                     <td align="left" class="auto-style1">
                            <asp:Label ID="Label2" runat="server" Text="Gatepass Date" ForeColor="navy" Font-Bold="true"
                                Font-Size="8pt"></asp:Label>
                        </td>
                        <td align="left" valign="middle" class="auto-style1">
                             <asp:Label ID="lblwhrdate" runat="server" ForeColor="navy" Font-Bold="true"
                                Font-Size="8pt"></asp:Label>
                        </td>
                   
                  
                    </tr>
                 <tr>
                                            <td style="height: 5px" colspan="8"></td>
                                        </tr>
                
                
                </table>               
           
                <div class="row" style="margin-top: 15px">
                    <div class="table-responsive" style="margin-left:20px;">
                        <asp:GridView ID="GrdGodown" runat="server" Width="100%" BackColor="White" AutoGenerateColumns="false"
                       BorderColor="#CC9966" BorderStyle="Double" BorderWidth="1px" ShowFooter="true"
                         CellSpacing="2" Font-Size="10pt" CssClass="table-bordered table-hover GridViewScrollHeader">
                            <Columns>                                
                                  <asp:BoundField DataField="StockDeliveryOrder_Id" HeaderText="Delivery Order ID" />
                                  <asp:BoundField DataField="Delivery_Order_Date" HeaderText="DO Date"/>
                                  <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity"/>
                                  <asp:BoundField DataField="Qty" HeaderText="Bags" ItemStyle-HorizontalAlign="Right" />
                                  <asp:BoundField DataField="Wet" HeaderText="Weight" ItemStyle-HorizontalAlign="Right" />
                                  <asp:BoundField DataField="DepositorIssuer_Name" HeaderText="Depositor Issuer Name"/>
                                 
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
        <div>
            <asp:Label ID="lblMsg" runat="server" BackColor="Red" Font-Size="Large"></asp:Label>
        </div>
       <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
        <script src="../../JS/table2excel.js"></script>
        <script src="../../assets/New/js/bootstrap-multiselect.js"></script>
        <script type="text/javascript">
            $("body").on("click", "#btnExport", function () {
                $("[id*=GridView1]").table2excel({
                    filename: "State_Level_District_Branch_Wise_Rent_Summary.xls"
                });
            });
        </script>
     <script type="text/javascript">
         $(function () {
             $('[id*=Godownchk]').multiselect({
                 includeSelectAllOption: true,
             });
         });
     </script>
    </form>
</body>
</html>
