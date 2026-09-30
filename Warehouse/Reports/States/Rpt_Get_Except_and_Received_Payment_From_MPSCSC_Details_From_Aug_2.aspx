<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Rpt_Get_Except_and_Received_Payment_From_MPSCSC_Details_From_Aug_2.aspx.cs" Inherits="Region_States_Rpt_Get_Except_and_Received_Payment_From_MPSCSC_Details_From_Aug_2" %>

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
<style 
    >
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
            Width:200px;
            height:50px; 
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
             <div class="container py-4">  
            <div class="card">  
                
                <div class="card-body">                      
                   <asp:Button ID="Button2" runat="server" Text="Print To PDF" CssClass="button button2" OnClientClick="printGrid()" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <input type="button" id="btnExport" value="Export To Excel" class="button button2"/>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button Text="Download To PDF" runat="server" OnClick="ExportToPDF" CssClass="button button2"/>
                   
                </div>  
            </div>  

        </div>  
            <table style="border: solid 5px #e3e3e8; width: 100%; vertical-align: central;">
                <tr>
                    <td style="padding-top: 20px; padding-bottom: 20px; background-color: skyblue; font-family: 'Times New Roman'; font-size: 20pt; color: white; text-align: center;">Region Wise<br /> Payment Received Details From MPSCSC
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left;">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" ShowFooter="true"
                            OnRowDataBound="GridView1_RowDataBound" OnRowCreated="GridView1_RowCreated" OnDataBound="OnDataBound"
                            CssClass="Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
                            <Columns>
                                
                                <asp:BoundField DataField="Regionnm" HeaderText="1" />
                                <asp:BoundField DataField="Region_ID" HeaderText="" />
                                <asp:BoundField DataField="District_Name" HeaderText="2" />

                                <asp:TemplateField HeaderText="3" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNoOfSUBBill" runat="server" Text='<%# Eval("NoOfSUBBill") %>' />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <div style="text-align: right;">
                                            <asp:Label ID="lblTotalqty" runat="server" Font-Bold="true" />
                                        </div>
                                    </FooterTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="4" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSUBBillAmt" runat="server" Text='<%# Eval("SUBBillAmt") %>' />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <div style="text-align: right;">
                                            <asp:Label ID="lblTotalqty1" runat="server" Font-Bold="true" />
                                        </div>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="5" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPaymentReceivedTilldecember" runat="server" Text='<%# Eval("PaymentReceivedTilldecember") %>' />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <div style="text-align: right;">
                                            <asp:Label ID="lblTotalqty2" runat="server" Font-Bold="true" />
                                        </div>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="6" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblqty" runat="server" Text='<%# Eval("TotalPayableAmount") %>' />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <div style="text-align: right;">
                                            <asp:Label ID="lblTotalqty3" runat="server" Font-Bold="true" />
                                        </div>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="7" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblqty1" runat="server" Text='<%# Eval("TotalPayableAmountToday") %>' />
                                    </ItemTemplate>
                                     <FooterTemplate>
                                        <div style="text-align: right;">
                                            <asp:Label ID="lblTotalqty4" runat="server" Font-Bold="true" />
                                        </div>
                                    </FooterTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="8" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNOOFBillPaymentFromMPSCSC" runat="server" Text='<%# Eval("NOOFBillPaymentFromMPSCSC") %>' />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <div style="text-align: right;">
                                            <asp:Label ID="lblTotalqty5" runat="server" Font-Bold="true" />
                                        </div>
                                    </FooterTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="9" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNoofBillGenerateAmt" runat="server" Text='<%# Eval("Payable_Amount") %>' />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <div style="text-align: right;">
                                            <asp:Label ID="lblTotalqty6" runat="server" Font-Bold="true" />
                                        </div>
                                    </FooterTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="10" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPendingBillatMPSCSC" runat="server" Text='<%# Eval("PendingBillatMPSCSC") %>' />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <div style="text-align: right;">
                                            <asp:Label ID="lblTotalqty7" runat="server" Font-Bold="true" />
                                        </div>
                                    </FooterTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="11" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPendingatMPSCSC" runat="server" Text='<%# Eval("PendingatMPSCSC") %>' />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <div style="text-align: right;">
                                            <asp:Label ID="lblTotalqty8" runat="server" Font-Bold="true" />
                                        </div>
                                    </FooterTemplate>
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="12" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNoofBillNEFT" runat="server" Text='<%# Eval("NoofBillNEFT") %>' />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <div style="text-align: right;">
                                            <asp:Label ID="lblTotalqty9" runat="server" Font-Bold="true" />
                                        </div>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="13" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNEFT_Amount_By_Bank" runat="server" Text='<%# Eval("NEFT_Amount_By_Bank") %>' />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <div style="text-align: right;">
                                            <asp:Label ID="lblTotalqty10" runat="server" Font-Bold="true" />
                                        </div>
                                    </FooterTemplate>
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="14" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNoofBillPendingatMPWLC" runat="server" Text='<%# Eval("NoofBillPendingatMPWLC") %>' />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <div style="text-align: right;">
                                            <asp:Label ID="lblTotalqty11" runat="server" Font-Bold="true" />
                                        </div>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="15" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNoofAmountPendingatMPWLC" runat="server" Text='<%# Eval("NoofAmountPendingatMPWLC") %>' />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <div style="text-align: right;">
                                            <asp:Label ID="lblTotalqty12" runat="server" Font-Bold="true" />
                                        </div>
                                    </FooterTemplate>
                                </asp:TemplateField>

                            </Columns>
                         <FooterStyle Font-Bold="True" ForeColor="Black" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </div>
         <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
        <script src="../../JS/table2excel.js"></script>
    <script type="text/javascript">
        $("body").on("click", "#btnExport", function () {
            $("[id*=GridView1]").table2excel({
                filename: "Payment_Received_From_MPSCSC_Details_From_Aug.xls"
            });
        });
    </script>
    </form>
</body>
</html>
