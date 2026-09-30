<%@ Page Language="C#" MasterPageFile="~/Inspections/Masters/Inspection_RO.master" AutoEventWireup="true" CodeFile="RO_To_RO_Received_Insecticide_Entry.aspx.cs" Inherits="Inspections_RO_RO_To_RO_Received_Insecticide_Entry" Title="Transfer Insecticide RO To RO" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <div class="pt-2">
            <h4 class="text-info">RO To RO Received Insecticide Entry</h4>
            <div class="row">
                <div class="col-12">
                    <asp:GridView ID="GVOfStock" OnRowEditing="GVOfStock_RowEditing" OnRowUpdating="GVOfStock_RowUpdating"
                        AutoGenerateColumns="false" runat="server"
                        OnRowCancelingEdit="GVOfStock_RowCancelingEdit1"
                        OnRowCommand="GVOfStock_RowCommand">
                        <HeaderStyle
                            BackColor="#D69758"
                            Font-Italic="false"
                            ForeColor="Snow" />
                        <Columns>
                            <asp:TemplateField HeaderText="S.No." ItemStyle-Width="3%">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                    <asp:HiddenField ID="hdninsID" runat="server" Value='<%# Bind("Insecticide_ID") %>' />
                                    <asp:HiddenField ID="hdnunitid" runat="server" Value='<%# Bind("Unit") %>' />
                                    <asp:HiddenField ID="hdnRegion" runat="server" Value='<%# Bind("Region_ID") %>' />
                                    <asp:HiddenField ID="hdnRegionTo" runat="server" Value='<%# Bind("RegionTo") %>' />
                                    <asp:HiddenField ID="hdnID" runat="server" Value='<%# Bind("id") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%-- <asp:TemplateField HeaderText="HO Transfer Date">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Opening_Date" runat="server" Text='<%#Eval("Date_Expriy") %>'></asp:Label>
                                </ItemTemplate>                               
                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Region From">
                                <ItemTemplate>
                                    <asp:Label ID="lblRegionTo" runat="server" Text='<%#Eval("RegionNsmeTo") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Region To">
                                <ItemTemplate>
                                    <asp:Label ID="lblRegion" runat="server" Text='<%#Eval("RegionFrom") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="UNIT NMAE">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Unit_Name" runat="server" Text='<%#Eval("Unit_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Insecticide Name">
                                <ItemTemplate>
                                    <asp:Label ID="Insecticide_Name" runat="server" Text='<%#Eval("Insecticide_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Quantity">
                                <ItemTemplate>
                                    <asp:Label ID="ob_quantity" runat="server" Text='<%#Eval("Opening_Balance_quantity") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Market Value">
                                <ItemTemplate>
                                    <asp:Label ID="ob_market_value" runat="server" Text='<%#Eval("Opening_Balance_market_value") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total Value">
                                <ItemTemplate>
                                    <asp:Label ID="ob_value" runat="server" Text='<%#Eval("Opening_Balance_value") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%-- <asp:TemplateField HeaderText="Expriy Date">
                                <ItemTemplate>
                                    <asp:Label ID="ob_Expriy" runat="server" Text='<%#Eval("Date_Expriy") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Received Insecticide" ItemStyle-Width="15%">
                                <ItemTemplate>
                                    <asp:Button ID="btnfilloverallinsp" Text="Received" runat="server" CommandName="Overallinsp" CssClass="btn btn-info" CommandArgument='<%# Container.DataItemIndex %>' />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <%-- <asp:TemplateField HeaderText="EDIT">
                                        <ItemTemplate>
                                            <asp:Button ID="btn_Edit" runat="server" Text="Edit" CommandName="Edit" />
                                       </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Button ID="btn_Update" runat="server" Text="Update" CommandName="Update" />
                                            <asp:Button ID="btn_Cancel" runat="server" Text="Cancel" CommandName="Cancel" />
                                        </EditItemTemplate>
                                    </asp:TemplateField>--%>
                        </Columns>
                    </asp:GridView>

                </div>
            </div>
        </div>
    </div>
    <%-- </center>
    </fieldset>--%>
</asp:Content>

