<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Rpt_Branch_Wise_WHR_Wise_FIFO_Details.aspx.cs" Inherits="StatePages_Rpt_Branch_Wise_WHR_Wise_FIFO_Details" %>


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
  <div><img src="../../images/mpwlc.png" width: 75px;/></div>
  <div ><span style="font-size: 39px; font-family:bold;"> M.P. WAREHOUSING & LOGISTICS CORPORATION</span>
        <br />
      <span style="font-size: 30px; font-family:bold; text-align:center;margin-left: 190px;"> WHR Details & Available Stock for Issue (FIFO) <br /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Branch Name :-  <asp:Label ID="lblbracnhName" runat="server"></asp:Label></span>
  </div>
  <div><span style="WIDTH: 52.68mm; HEIGHT: 6.35mm;">Date:-</span> <asp:Label ID="labelName" runat="server"></asp:Label> 
      <br />
      <br />
      <br />
      <br />
      <br />
      <span style="font-size: 20px; font-family:bold;">Qty In M.T.</span>

  </div>
</div>
           
            <div class="container py-4">
                <div class="card">

                    <div class="card-body">
                        <asp:Button ID="btnback" runat="server" Text="Back" CssClass="button button2" OnClick="btnback_Click"  />
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="Button2" runat="server" Text="ExportToPDF" CssClass="button button2" OnClientClick="printGrid()" />
                        &nbsp;&nbsp;&nbsp;&nbsp;
                    <input type="button" id="btnExport" value="Exporttoexcel" class="button button2" />
                        &nbsp;&nbsp;&nbsp;&nbsp;<%--<asp:Button ID="btnUpdate" runat="server" CssClass="button button2" Text="Update Pending Months" Visible="false" OnClick="btnUpdate_Click" />--%>
                    </div>
                </div>

            </div>
            <div style="margin-left: 104px;">
                

            </div>
            <table style="border: solid 5px #e3e3e8; width: 100%; vertical-align: central; ">
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
                    <td style="text-align: left;" colspan="8">

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
        <div>
            <asp:Label ID="lblMsg" runat="server" BackColor="Red" Font-Size="Large"></asp:Label>
        </div>
        <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
        <script src="../../JS/table2excel.js"></script>
        <script type="text/javascript">
            $("body").on("click", "#btnExport", function () {
                $("[id*=GridView1]").table2excel({
                    filename: "Rpt_Procurement_Rabi2022_District.xls"
                });
            });
        </script>
       
    </form>
</body>
</html>
