<%@ Page Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="frm_Business_Prapatra_V.aspx.cs" Inherits="Accounting_frm_Business_Prapatra_V" Title="Prapatra V" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset style="width: 1000px; border: 2px solid navy;">
        <center>
            <div>
                <div>
                    <h3>मध्य प्रदेश वेयरहाउसिंग एंड लॉजिस्टिक्स कॉर्पोरेशन, शाखा :-
                        <asp:Label ID="lblbranchname" runat="server"></asp:Label>
                    </h3>
                    <h4>
                        केप हायरिंग योजना - २०१९ अंतर्गत निजी उघमियो द्वारा निर्मित सह संचालित केपो पर
                    </h4>
                    <h5>खरीफ विपणन वर्ष 
                         <asp:DropDownList ID="ddlcropyear" runat="server" AutoPostBack="false"
                             Width="80px">
                             <asp:ListItem Value="2021-22">2021-22</asp:ListItem>
                             <asp:ListItem Value="2022-23">2022-23</asp:ListItem>
                             <asp:ListItem Value="2023-24">2023-24</asp:ListItem>
                             <asp:ListItem Value="2024-25">2024-25</asp:ListItem>
                         </asp:DropDownList>
                       के भंडारित धान एवं शेष रिक्त मात्रा की प्रतिदिवास की जानकारी </h5>
                </div>

                <div style="width: 1000px; overflow: auto;">

                    <asp:GridView ID="GrdPrapatraI" runat="server" AutoGenerateColumns="False" Width="100%" Font-Names="Arial"
                        BorderStyle="Double" CellPadding="3" CellSpacing="5" ShowFooter="true" OnRowDataBound="GrdPrapatraI_RowDataBound"
                        Font-Size="11px" BorderColor="#CCCCCC" OnRowCreated="GrdPrapatraI_RowCreated">
                        <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <Columns>
                            <asp:TemplateField HeaderText="1">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1%>
                                    <asp:HiddenField runat="server" ID="hdnGodown_ID" Value='<%# Eval("Godown_ID") %>' />
                                </ItemTemplate>
                                <ItemStyle Width="1%" />
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Godown_Name" HeaderText="2" ItemStyle-HorizontalAlign="Left">
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Org_Name" HeaderText="3" ItemStyle-HorizontalAlign="Left">
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundField>

                          <asp:TemplateField HeaderText="3">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtCap_Capacity" runat="server" Width="100px" Text='<%# Eval("Cap_Capacity") %>'></asp:TextBox>
                                </ItemTemplate>
                                <%--<ItemStyle Width="10%" />--%>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="4">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtQSUTY" runat="server" Width="100px" Text='<%# Eval("Quantity_stored_up_to_yesterdays") %>'></asp:TextBox>
                                </ItemTemplate>
                                <%--<ItemStyle Width="10%" />--%>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="5">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtQST" runat="server" Width="100px" Text='<%# Eval("Quantity_stored_today") %>'></asp:TextBox>
                                </ItemTemplate>
                                <%--<ItemStyle Width="10%" />--%>
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="6">
                                <ItemTemplate>
                                    <asp:Label ID="txtPSV" runat="server" Width="100px" Text='<%# Eval("progressive_stored_volume") %>'></asp:Label>
                                </ItemTemplate>
                                <%--<ItemStyle Width="10%" />--%>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="7">
                                <ItemTemplate>
                                    <asp:Label ID="txtPBQ" runat="server" Width="100px" Text='<%# Eval("progressive_Balank_Quantity") %>'></asp:Label>
                                </ItemTemplate>
                                <%--<ItemStyle Width="10%" />--%>
                            </asp:TemplateField>

                        </Columns>
                        <HeaderStyle ForeColor="#C70039" HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="11px" />
                    </asp:GridView>

                </div>
                <div id="btnhideshow" runat="server" visible="true" class="row" style="text-align: center;">

                    <asp:Button CssClass="BTNBLUE" Width="100px" ID="btn_saveInspDate" runat="server" Text="Save" OnClick="btn_saveInspDate_Click"></asp:Button>
                   

                </div>
            </div>
        </center>
    </fieldset>
    <script type="text/javascript" src="<%= ResolveUrl("~/NEW_CSS/js/jquery-1.12.4.js") %>"></script>
    <script type="text/javascript" src="<%= ResolveUrl("~/NEW_CSS/js/jquery-ui.js") %>"></script>
    <script language="javascript" type="text/javascript">
        function NumberOnly(e) {
            var charCode = (e.which) ? e.which : e.keyCode;
            if ((charCode >= 48 && charCode <= 57)) {
                return true;
            }
            if (charCode == 46) { return true; }
            if (charCode == 8) { return true; }
            if (charCode == 9) { return true; }
            else { return false; }
        }
    </script>
</asp:Content>

