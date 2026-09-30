<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Region_Master.master" AutoEventWireup="true" CodeFile="Rpt_Get_Bill_Detail_August_to_Till.aspx.cs" Inherits="Region_Reports_Rpt_Get_Bill_Detail_August_to_Till" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
                    <td style="padding-top: 20px; padding-bottom: 20px; background-color: skyblue; font-family: 'Times New Roman'; font-size: 20pt; color: white; text-align: center;">M.P. Warehousing & Logistics Corporarion<br /> PVT Godown Storage Charges Bill(After August)
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left;">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" ShowFooter="true"
                            OnRowDataBound="GridView1_RowDataBound" OnRowCreated="GridView1_RowCreated" OnDataBound="OnDataBound"
                            CssClass="Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
                            <Columns>
                                <asp:BoundField DataField="District" HeaderText="District Name" />
                                <asp:BoundField DataField="District_Id" HeaderText="District_Id" />
                                <asp:BoundField DataField="Branch" HeaderText="Branch Name" />
                                <%--<asp:BoundField DataField="title_id" HeaderText="title_id" />--%>
                                <asp:TemplateField HeaderText="No. of Godown" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblqty" runat="server" Text='<%# Eval("noofgdwn") %>' />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <div style="text-align: right;">
                                            <asp:Label ID="lblTotalqty" runat="server" Font-Bold="true" />
                                        </div>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Bill generated by BM(in No's)" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblqty1" runat="server" Text='<%# Eval("NoOfGenerateBill") %>' />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <div style="text-align: right;">
                                            <asp:Label ID="lblTotalqty1" runat="server" Font-Bold="true" />
                                        </div>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="DSC Signed by BM(in No's)" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblqty2" runat="server" Text='<%# Eval("NoOfDSCSingBill") %>' />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <div style="text-align: right;">
                                            <asp:Label ID="lblTotalqty2" runat="server" Font-Bold="true" />
                                        </div>
                                    </FooterTemplate>
                                </asp:TemplateField>
                              
                                 <asp:TemplateField HeaderText="Submitted to HO MPSCSC(in No's)" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblqty3" runat="server" Text='<%# Eval("NoOfSUBBill") %>' />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <div style="text-align: right;">
                                            <asp:Label ID="lblTotalqty3" runat="server" Font-Bold="true" />
                                        </div>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Signed Bill HO MPSCSC(in No's)" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblqty4" runat="server" Text='<%# Eval("NoOfSignBill_CSMS") %>' />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <div style="text-align: right;">
                                            <asp:Label ID="lblTotalqty4" runat="server" Font-Bold="true" />
                                        </div>
                                    </FooterTemplate>
                                </asp:TemplateField>
                              
                                 <asp:TemplateField HeaderText="Approve by AGM (A/C's) of RO(in No's)" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblqty5" runat="server" Text='<%# Eval("NoOfGdwnRM") %>' />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <div style="text-align: right;">
                                            <asp:Label ID="lblTotalqty5" runat="server" Font-Bold="true" />
                                        </div>
                                    </FooterTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="DSC By RM(in No's)" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblqty6" runat="server" Text='<%# Eval("NoOfRMDSC") %>' />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <div style="text-align: right;">
                                            <asp:Label ID="lblTotalqty6" runat="server" Font-Bold="true" />
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
                filename: "State_Level_District_Branch_Wise_Rent_Summary.xls"
            });
        });
    </script>
</asp:Content>
