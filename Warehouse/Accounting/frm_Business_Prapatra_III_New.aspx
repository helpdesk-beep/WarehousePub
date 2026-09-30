<%@ Page Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="frm_Business_Prapatra_III_New.aspx.cs" Inherits="Accounting_frm_Business_Prapatra_III_New" Title="Prapatra III" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .form-control {
            width: 50%;
            height: 30px;
            padding: 6px 12px;
            background-color: #fff;
            border: 1px solid #ccc;
            border-radius: 4px;
            -webkit-box-shadow: inset 0 1px 1px rgba(0,0,0,.075);
            box-shadow: inset 0 1px 1px rgba(0,0,0,.075);
            -webkit-transition: border-color ease-in-out .15s,-webkit-box-shadow ease-in-out .15s;
            -o-transition: border-color ease-in-out .15s,box-shadow ease-in-out .15s;
            transition: border-color ease-in-out .15s,box-shadow ease-in-out .15s;
        }
    </style>
</asp:Content>
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
                                    <asp:HiddenField runat="server" ID="hdnGodown_ID" Value='<%# Eval("Godown_ID") %>' />
                                </ItemTemplate>
                                <ItemStyle Width="1%" />
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Godown_Name" HeaderText="2" ItemStyle-HorizontalAlign="Left">
                                <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>

                            <asp:TemplateField HeaderText="3">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtPakkacap1" runat="server" Width="100px" Text='<%# Eval("Pakkacap1") %>'></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle Width="10%" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="4">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtMandished1" runat="server" Width="100px" Text='<%# Eval("Mandished1") %>'></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle Width="10%" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="5">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtKachchacap1" runat="server" Width="100px" Text='<%# Eval("Kachchacap1") %>'></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="6">
                                <ItemTemplate>
                                    <asp:Label ID="txtTotal1" runat="server" Width="100px" Text='<%# Eval("Total1") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="7">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtPakkacap2" runat="server" Width="100px" Text='<%# Eval("Pakkacap2") %>'></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="8">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtMandished2" runat="server" Width="100px" Text='<%# Eval("Mandished2") %>'></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="9">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtKachchacap2" runat="server" Width="100px" Text='<%# Eval("Kachchacap2") %>'></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="10">
                                <ItemTemplate>
                                    <asp:Label ID="txtTotal2" runat="server" Width="100px" Text='<%# Eval("Total2") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="11">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtPakkacap3" runat="server" Width="100px" Text='<%# Eval("Pakkacap3") %>'></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="12">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtMandished3" runat="server" Width="100px" Text='<%# Eval("Mandished3") %>'></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="13">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtKachchacap3" runat="server" Width="100px" Text='<%# Eval("Kachchacap3") %>'></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="14">
                                <ItemTemplate>
                                    <asp:Label ID="txtTotal3" runat="server" Width="100px" Text='<%# Eval("Total3") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="10%" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="15">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtPakkacap4" runat="server" Width="100px" Text='<%# Eval("Pakkacap4") %>'></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="16">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtMandished4" runat="server" Width="100px" Text='<%# Eval("Mandished4") %>'></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="17">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtKachchacap4" runat="server" Width="100px" Text='<%# Eval("Kachchacap4") %>'></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="18">
                                <ItemTemplate>
                                    <asp:Label ID="txtTotal4" runat="server" Width="100px" Text='<%# Eval("Total4") %>'></asp:Label>
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
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.9.1.js"></script>
    <script type="text/javascript" src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
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

