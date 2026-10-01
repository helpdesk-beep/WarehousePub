<%@ Page Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="frm_Business_Prapatra_7.aspx.cs" Inherits="Accounting_frm_Business_Prapatra_7" Title="Prapatra III" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset style="width: 1000px; border: 2px solid navy;">
        <center>
            <div>
                <div>
                    <h3>मध्य प्रदेश वेयरहाउसिंग एंड लॉजिस्टिक्स कॉर्पोरेशन, शाखा :-
                        <asp:Label ID="lblbranchname" runat="server"></asp:Label>
                    </h3>
                    <h5>खरीफ विपणन वर्ष 
                         <asp:DropDownList ID="ddlcropyear" runat="server" AutoPostBack="false"
                             Width="80px">
                             <asp:ListItem Value="2021-22">2021-22</asp:ListItem>
                             <asp:ListItem Value="2022-23">2022-23</asp:ListItem>
                             <asp:ListItem Value="2023-24">2023-24</asp:ListItem>
                             <asp:ListItem Value="2024-25">2024-25</asp:ListItem>
                         </asp:DropDownList>
                        में कैप की क्षमता एवं भंडारित मात्रा की जानकारी प्रतिदिवास</h5>
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
                                    <asp:HiddenField runat="server" ID="hdnBranch_ID" Value='<%# Eval("Branch_ID") %>' />
                                </ItemTemplate>
                                <ItemStyle Width="1%" />
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>
                            <asp:BoundField DataField="DepotName" HeaderText="2" ItemStyle-HorizontalAlign="Left">
                                <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>

                            <asp:TemplateField HeaderText="3">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtMPWLC_HIRED_JVS1" runat="server" Width="100px" Text='<%# Eval("MPWLC_HIRED_JVS1") %>'></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle Width="10%" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="4">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtPVT_PEG1" runat="server" Width="100px" Text='<%# Eval("PVT_PEG1") %>'></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle Width="10%" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="5">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtMARKFED1" runat="server" Width="100px" Text='<%# Eval("MARKFED1") %>'></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="6">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtQMANDI_BOARD1" runat="server" Width="100px" Text='<%# Eval("QMANDI_BOARD1") %>'></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="7">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtOILFED1" runat="server" Width="100px" Text='<%# Eval("OILFED1") %>'></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="8">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtCWC1" runat="server" Width="100px" Text='<%# Eval("CWC1") %>'></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle Width="10%" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="9">
                                <ItemTemplate>
                                    <asp:Label ID="txttotal3to8" runat="server" Width="100px" Text='<%# Eval("Total3to8") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="10%" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="10">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtMPWLC_HIRED_JVS2" runat="server" Width="100px" Text='<%# Eval("MPWLC_HIRED_JVS2") %>'></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="11">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtPVT_PEG2" runat="server" Width="100px" Text='<%# Eval("PVT_PEG2") %>'></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="12">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtMARKFED2" runat="server" Width="100px" Text='<%# Eval("MARKFED2") %>'></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="13">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtQMANDI_BOARD2" runat="server" Width="100px" Text='<%# Eval("QMANDI_BOARD2") %>'></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="14">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtOILFED2" runat="server" Width="100px" Text='<%# Eval("OILFED2") %>'></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="15">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtCWC2" runat="server" Width="100px" Text='<%# Eval("CWC2") %>'></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle Width="10%" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="16">
                                <ItemTemplate>
                                    <asp:Label ID="txttotal10to15" runat="server" Width="100px" Text='<%# Eval("Total10to15") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="10%" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="17">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtMPWLC_HIRED_JVS3" runat="server" Width="100px" Text='<%# Eval("MPWLC_HIRED_JVS3") %>'></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="18">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtPVT_PEG3" runat="server" Width="100px" Text='<%# Eval("PVT_PEG3") %>'></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="19">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtMARKFED3" runat="server" Width="100px" Text='<%# Eval("MARKFED3") %>'></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="20">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtQMANDI_BOARD3" runat="server" Width="100px" Text='<%# Eval("QMANDI_BOARD3") %>'></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="21">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtOILFED3" runat="server" Width="100px" Text='<%# Eval("OILFED3") %>'></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="22">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtCWC3" runat="server" Width="100px" Text='<%# Eval("CWC3") %>'></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="23">
                                <ItemTemplate>
                                    <asp:Label ID="txttotal7to22" runat="server" Width="100px" Text='<%# Eval("Total17to22") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="10%" />
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

