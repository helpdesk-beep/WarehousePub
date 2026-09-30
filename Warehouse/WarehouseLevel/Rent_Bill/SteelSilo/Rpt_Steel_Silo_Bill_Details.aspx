<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/PrivateWarehouse.master" AutoEventWireup="true" CodeFile="Rpt_Steel_Silo_Bill_Details.aspx.cs" Inherits="WarehouseLevel_Rent_Bill_SteelSilo_Rpt_Steel_Silo_Bill_Details" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">  

        function printPartOfPage(elementId) {
            var printContent = document.getElementById(elementId);
            var windowUrl = 'about:blank';
            var uniqueName = new Date();
            var windowName = 'Print' + uniqueName.getTime();
            var printWindow = window.open(windowUrl, windowName, 'left=50000,top=50000,width=0,height=0');
            printWindow.document.write(printContent.innerHTML);
            printWindow.document.close();
            printWindow.focus();
            printWindow.print();
            printWindow.close();
        }
    </script>
    <script type="text/javascript">
        function printGrid() {
            var gridData = document.getElementById('<%= gvIStorageCharge.ClientID %>');
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset style="width: 80%; border: 2px solid navy; background-color: white;">
        <center>
            <div>
                <table width="100%">
                    <tr id="msg">
                        <td colspan="4">
                            <asp:Label ID="lblmsg" Text="" ForeColor="red" Font-Bold="true" runat="server"></asp:Label></td>
                    </tr>
                    <tr id="trRentBill">
                        <td colspan="4">
                            <fieldset style="width: 97%; border: 1px solid navy;">
                                <center>
                                    <%-- <div style="overflow: scroll; height: 400px; overflow-x: hidden">--%>
                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                        <tr>
                                            <td colspan="6" align="center" style="background-color: #0bb6e6; height: 25px">
                                                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="15pt" ForeColor="WhiteSmoke"
                                                    Text="Steel Silo Filled Rent Bill Report"></asp:Label>
                                            </td>
                                        </tr>
                                      
                                        <tr>
                                          
                                          
                                            <td style="font-size:20px; font-weight:600; color:red;">
                                            Enter Bill Number:-  <asp:TextBox runat="server" align="center" ID="txtBillNu"></asp:TextBox>
                                               &nbsp;&nbsp; &nbsp; &nbsp;   <asp:Button style="background-color:cornflowerblue;" ID="btnSerch" runat="server"  Text="Search" OnClick="btnSerch_Click"/>
                                            </td>
                                            
                                        

                                               <td>
                                                <div style="text-align: right; color: red; width: auto; height: 50px; border-bottom-color: brown">
                                                    <%--<input type="button" class="btn btn-primary" value="PRINT" onclick="JavaScript:printPartOfPage('abc');" />--%>
                                               <asp:Button ID="Button2" runat="server" Text="PRINT" CssClass="button button2" OnClientClick="printGrid()" />
                                                    </div>
                                            </td>
                                        </tr>
                               
                                      <td colspan="6" align="center" style="background-color: #0bb6e6; height: 25px" runat="server" visible="false">
                                                <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="15pt" ForeColor="WhiteSmoke"
                                                    Text="Adani Agri Logistics(MP) Ltd."></asp:Label>
                                          &nbsp;
                                          <%--<asp:Label ID="lblbranchname" runat="server" Font-Bold="True" Font-Size="15pt" ForeColor="WhiteSmoke"
                                                    ></asp:Label>--%>
                                          <asp:Label ID="lblbranchname" runat="server" Font-Bold="True" Font-Size="15pt" ForeColor="WhiteSmoke"
                                                    ></asp:Label>
                                            </td>
                                        <tr id="abc" runat="server" visible="false">   
                                        <td colspan="6" align="center" style="background-color: #0bb6e6; height: 25px">
                                                <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Size="15pt" ForeColor="WhiteSmoke"
                                                    Text="Stock Statement of Wheat for the month of May-2022"></asp:Label>
                                            </td>
                                            </tr>

                                        <tr>
                                            <td colspan="6" align="center">
                                                
                                           
                                        
                                                <asp:GridView ID="gvIStorageCharge" runat="server" AutoGenerateColumns="false" ShowFooter="true" OnRowCommand="gvIStorageCharge_RowCommand"
                                                    FooterStyle-Font-Bold="true" FooterStyle-CssClass="alert-danger">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No." ItemStyle-Width="3%">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%--<asp:BoundField HeaderText="Bill Number" DataField="Bill_Number" />
                                                            <asp:BoundField HeaderText="Invoice Number" DataField="Invoice_No" />
                                                            <asp:BoundField HeaderText="Commodity" DataField="Commodity_Name" />
                                                            <asp:BoundField HeaderText="Crop Year" DataField="Crop_Year" />
                                                            <asp:BoundField HeaderText="Financial Year" DataField="Financial_Year" />
                                                            <asp:BoundField HeaderText="Month" DataField="Month" />
                                                            <asp:BoundField HeaderText="From Date" DataField="FromDate" />
                                                            <asp:BoundField HeaderText="To Date" DataField="ToDate" />
                                                            <asp:BoundField HeaderText="Amount" DataField="Net_Amount" />--%>
                                                        <asp:BoundField HeaderText="Date" DataField="Date" />
                                                        <asp:BoundField HeaderText="Commodity Name" DataField="Commodity" />
                                                        <asp:BoundField HeaderText="Opening Stock (MT)" DataField="Opening_Weight" />
                                                        <asp:BoundField HeaderText="Receipts (MT)" DataField="Receive_Weight" />
                                                        <asp:BoundField HeaderText="Dispach (MT)" DataField="Issue_Weight" />
                                                        <asp:BoundField HeaderText="Closing Stock (MT)" DataField="Closing_Weight" />
                                                        <%--<asp:BoundField HeaderText="Chargeable Closing Weight" DataField="Chargeable_Closing_Weight" />
                                                            <asp:BoundField HeaderText="Total Charges" DataField="Total_Charges" />--%>
                                                        <%--<asp:BoundField HeaderText="To Date" DataField="ToDate" />
                                                            <asp:BoundField HeaderText="Amount" DataField="Net_Amount" />--%>
                                                        <%-- <asp:TemplateField HeaderText="">
                                                                <HeaderTemplate>
                                                                    <div style="text-align: center;">
                                                                        Print Bill
                                                                    </div>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <div style="text-align: right;">--%>
                                                        <%--<asp:HyperLink ID="HyperLink1" runat="server" Target="_blank" NavigateUrl='<%#"Print_Filld_Godown_Rent_Bill.aspx?BN="+ Base64Encode(Eval("Bill_Number").ToString()) %>'
                                                                            CssClass="btn btn-large btn-success" title="Print Bill"> <i class="fa fa-print"></i></asp:HyperLink>--%>
                                                        <%-- <asp:LinkButton ID="btnLock" runat="server" CommandArgument='<%# Eval("Bill_Number")%>'
                                                                            CssClass="btn btn-primary" EnableTheming="false" CommandName="Print">Print</asp:LinkButton>
                                                                        <asp:HiddenField ID="hdnBillCategoryID" runat="server" Value='<%# Eval("Bill_Category_Type_ID")%>' />--%>
                                                        <%--  </div>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>--%>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>

                                    <%--</div>--%>
                                </center>
                            </fieldset>
                        </td>
                    </tr>
                </table>
            </div>
        </center>
    </fieldset>
    <asp:HiddenField ID="hdnBillNumber" runat="server" Value="0" />
    <asp:HiddenField ID="hdnBillCategory" runat="server" Value="0" />
    <asp:HiddenField ID="hdnAmt" runat="server" Value="0" />
</asp:Content>
