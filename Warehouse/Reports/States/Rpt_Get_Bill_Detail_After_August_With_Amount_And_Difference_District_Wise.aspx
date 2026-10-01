<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Rpt_Get_Bill_Detail_After_August_With_Amount_And_Difference_District_Wise.aspx.cs" Inherits="Reports_States_Rpt_Get_Bill_Detail_After_August_With_Amount_And_Difference_District_Wise" %>

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
                   <asp:Button ID="Button2" runat="server" Text="ExportToPDF" CssClass="button button2" OnClientClick="printGrid()" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <input type="button" id="btnExport" value="Exporttoexcel" class="button button2"/>
                </div>  
            </div>  

        </div>  
            <table style="border: solid 5px #e3e3e8; width: 100%; vertical-align: central;">
                <tr>
                    <td style="padding-top: 20px; padding-bottom: 20px; background-color: skyblue; font-family: 'Times New Roman'; font-size: 20pt; color: white; text-align: center;">District Wise<br /> M.P. Warehousing & Logistics Corporarion<br /> PVT Godown Storage Charges Bill(After August)<br />Pending at DM MPSCSC Level
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left;">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" ShowFooter="true"
                            OnRowDataBound="GridView1_RowDataBound" OnRowCreated="GridView1_RowCreated" OnDataBound="OnDataBound"
                            CssClass="Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
                            <Columns>
                               <asp:BoundField DataField="Regionnm" HeaderText="Region Name" />
                                <asp:BoundField DataField="Region_ID" HeaderText="Region_ID" />
                                <%--<asp:BoundField DataField="District" HeaderText="District Name" />--%>
                                 <asp:TemplateField HeaderText="District Name">
                                <ItemTemplate>
                                    <asp:HyperLink ID="sdffsft" runat="server" Target="_blank" NavigateUrl='<%#"/Reports/States/Rpt_Get_Bill_Detail_After_August_With_Amount_And_Difference_Branch_Wise.aspx?ID="+ (Eval("District_Id").ToString())%>'
                                        title="District Name" Text=' <%# Eval("District") %>'></asp:HyperLink>
                                 <%--<asp:Label ID="lblRegionnm" runat="server" Text='<%# Eval("Regionnm") %>' />--%>
                                </ItemTemplate>
                                     <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>
                                <asp:TemplateField HeaderText="No. of Godown" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblqty" runat="server" Text='<%# Eval("noofgdwn") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="In No's" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblqty1" runat="server" Text='<%# Eval("NoOfGenerateBill") %>' />
                                    </ItemTemplate>                                    
                                </asp:TemplateField>
                               
                                <asp:TemplateField HeaderText="Amount In Rs." ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblqty2" runat="server" Text='<%# Eval("NoofBillGenerateAmt") %>' />
                                    </ItemTemplate>                                    
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="DSC by BM (In No's)" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblqty3" runat="server" Text='<%# Eval("NoOfDSCSingBill") %>' />
                                    </ItemTemplate>                                    
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="DSC by BM (Amount In Rs.)" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblqty4" runat="server" Text='<%# Eval("NoofBillAmtDSC") %>' />
                                    </ItemTemplate>                                    
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Submitted to ICM (In No's)" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblqty5" runat="server" Text='<%# Eval("NoOfSUBBill") %>' />
                                    </ItemTemplate>                                    
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Submitted to ICM (Amount in Rs.)" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblqty6" runat="server" Text='<%# Eval("SUBBillAmt") %>' />
                                    </ItemTemplate>                                   
                                </asp:TemplateField>                                
                                 <asp:TemplateField HeaderText="Signed by ICM (In No's)" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblqty7" runat="server" Text='<%# Eval("NoOfSignBill_CSMS") %>' />
                                    </ItemTemplate>                                                                       
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Signed by ICM (Amount In Rs.) " ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblqty8" runat="server" Text='<%# Eval("NoofCSMS_BillAmt") %>' />
                                    </ItemTemplate>
                                    
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="In No's" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblqty9" runat="server" Text='<%# Eval("Diffierenceinos12") %>' />
                                    </ItemTemplate>
                                   
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="In Amount Rs." ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblqty10" runat="server" Text='<%# Eval("Diffierenceinos13") %>' />
                                    </ItemTemplate>
                                    
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Payment Recevied from DM SCSC (In No's)" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblqty11" runat="server" Text='<%# Eval("PaymentReceviedfromDMSCSC") %>' />
                                    </ItemTemplate>
                                   
                                </asp:TemplateField>
                              
                                <asp:TemplateField HeaderText="Payment Recevied from DM SCSC (Amount in Rs.)" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblqty12" runat="server" Text='<%# Eval("PaymentReceviedfromDMSCSCAmount") %>' />
                                    </ItemTemplate>
                                   
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="DSC by RM (In No's)" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblqty13" runat="server" Text='<%# Eval("NoOfRMDSC") %>' />
                                    </ItemTemplate>
                                    
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="DSC by RM (Amount in Rs.) " ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblqty14" runat="server" Text='<%# Eval("NoOfRMDSCAmt") %>' />
                                    </ItemTemplate>
                                    
                                </asp:TemplateField>
                               <%-- <asp:TemplateField HeaderText="DSC By RM (In No's)" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblqty15" runat="server" Text='<%# Eval("Diffierenceinos18") %>' />
                                    </ItemTemplate>
                                    
                                </asp:TemplateField>  
                                <asp:TemplateField HeaderText="DSC By RM (Amount in Rs.)" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblqty16" runat="server" Text='<%# Eval("Diffierenceinos19") %>' />
                                    </ItemTemplate>                                   
                                </asp:TemplateField>--%>
                               
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
                filename: "State_Level_District_Branch_Wise_Rent_Summary.xls"
            });
        });
    </script>
    </form>
</body>
</html>
