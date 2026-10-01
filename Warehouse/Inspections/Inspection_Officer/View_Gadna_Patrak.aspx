<%@ Page Title="" Language="C#" MasterPageFile="~/Inspections/Masters/Inspection_BO.master" AutoEventWireup="true" CodeFile="View_Gadna_Patrak.aspx.cs" Inherits="Inspections_BO_View_Gadna_Patrak" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css">
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.8.3/themes/base/jquery-ui.css" />
    <script type="text/javascript" src="<%= ResolveUrl("~/NEW_CSS/js/jquery-1.12.4.js") %>"></script>
    <script type="text/javascript" src="<%= ResolveUrl("~/NEW_CSS/js/jquery-ui.js") %>"></script>
    <script type="text/javascript">
        function PrintGridData() {
            var prtGrid = document.getElementById('<%=GD_StackBal.ClientID %>');
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
            var gridData = document.getElementById('<%= GD_StackBal.ClientID %>');
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
    <div style="background-color: #FDFAF7; width: 100%;">
        <div class="card-body">
            <asp:Button ID="Button2" runat="server" Text="ExportToPDF" CssClass="button button2" OnClientClick="printGrid()" />
            &nbsp;&nbsp;&nbsp;&nbsp;
                    <input type="button" id="btnExport" value="Exporttoexcel" class="button button2" />
        </div>
        <table align="center" style="width: 100%; border: #008CBA; border-style: solid; border-width: 0px;">

            <tr id="tr_griddata" runat="server" visible="false">
                <td colspan="4">
                    <table align="center" style="width: 100%;">

                        <tr>
                            <td colspan="6" valign="top" align="center">

                                <asp:GridView runat="server" ID="GD_StackBal" OnRowCreated="GD_StackBal_RowCreated" OnRowCommand="GD_StackBal_RowCommand"
                                    AutoGenerateColumns="false" CssClass="Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">

                                    <Columns>
                                        <asp:TemplateField HeaderText="1">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1%>
                                                <asp:HiddenField runat="server" ID="hdnDepositer_ID" Value='<%# Eval("Depositer_ID") %>' />
                                                <asp:HiddenField runat="server" ID="hdnCommodity_ID" Value='<%# Eval("Commodity_ID") %>' />
                                                <asp:HiddenField runat="server" ID="hdncropyear" Value='<%# Eval("Crop_Year") %>' />
                                                <asp:HiddenField runat="server" ID="hdnID" Value='<%# Eval("ID") %>' />
                                            </ItemTemplate>
                                            <ItemStyle Width="1%" />
                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="2">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDepositor_Name" runat="server" Text='<%# Eval("Depositer_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="3">
                                            <ItemTemplate>
                                                <asp:Label ID="lblcommodity" runat="server" Text='<%# Eval("Commodity_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="4">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCrop_Year" runat="server" Text='<%# Eval("Crop_Year") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="5">
                                            <ItemTemplate>
                                                <asp:Label ID="lblstack_id" runat="server" Text='<%# Eval("stack_id") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="6">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStack_Name" runat="server" Text='<%# Eval("Stack_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="7">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLength" runat="server" Text='<%# Eval("Length") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="8">
                                            <ItemTemplate>
                                                <asp:Label ID="lblWidth" runat="server" Text='<%# Eval("Width") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="9">
                                            <ItemTemplate>
                                                <asp:Label ID="lblExtra" runat="server" Text='<%# Eval("Extra") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="10">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTotal_L_W_E" runat="server" Text='<%# Eval("Total_L_W_E") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="11">
                                            <ItemTemplate>
                                                <asp:Label ID="lblHeight" runat="server" Text='<%# Eval("Height") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="12">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNumber_Of_Block" runat="server" Text='<%# Eval("Number_Of_Block") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="13">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNo_of_Bags" runat="server" Text='<%# Eval("No_of_Bags") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="14">
                                            <ItemTemplate>
                                                <asp:Label ID="lblUp" runat="server" Text='<%# Eval("Up") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="15">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBelow" runat="server" Text='<%# Eval("Below") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="16">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTotal_Bags" runat="server" Text='<%# Eval("Total_Bags") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#E6C79D" Font-Bold="True" ForeColor="#cb4e48" HorizontalAlign="center" />
                                    <AlternatingRowStyle BackColor="#eeeeee" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <div class="row">
            <div class="col-lg-12">
            </div>
        </div>

    </div>

    <script type="text/javascript">

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode != 46 && charCode > 31
                && (charCode < 48 || charCode > 57)) {
                alert("This field will not accept the alphabet, Please Enter Only number");
                return false;
            }
            return true;
        }
        //
    </script>
</asp:Content>

