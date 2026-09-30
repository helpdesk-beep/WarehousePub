<%@ Page Language="C#" MasterPageFile="~/Inspections/Masters/Inspection_BO.master" AutoEventWireup="true" CodeFile="Branch_To_Branch_Received_Insecticide.aspx.cs" Inherits="Inspections_BO_Branch_To_Branch_Received_Insecticide" Title="Branch_To_Branch_Received_Insecticide" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <div class="pt-2">
            <h4 class="text-info">Ro Insecticide Opening Balance Entry</h4>
            <div class="row">
                <div class="col-12">
                    <asp:GridView ID="GVOfStock" OnRowEditing="GVOfStock_RowEditing" OnRowUpdating="GVOfStock_RowUpdating"
                        AutoGenerateColumns="false" runat="server"
                        OnRowCancelingEdit="GVOfStock_RowCancelingEdit1"
                        OnRowCommand="GVOfStock_RowCommand">
                        <HeaderStyle
                            BackColor="#D69758"
                            Font-Italic="false"
                            ForeColor="Snow"/>
                        <Columns>
                            <asp:TemplateField HeaderText="S.No." ItemStyle-Width="3%">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                    <asp:HiddenField ID="hdnid" runat="server" Value='<%# Bind("id") %>'/>
                                    <asp:HiddenField ID="hdninsID" runat="server" Value='<%# Bind("Insecticide_ID") %>'/>
                                    <asp:HiddenField ID="hdnunitid" runat="server" Value='<%# Bind("unit") %>' />
                                    <%--<asp:HiddenField ID="hdnDistricT" runat="server" Value='<%#Bind("Branch_T")%>'/>
                                    <asp:HiddenField ID="hdnBranchT" runat="server" Value='<%#Bind("District_T")%>'/>--%>

                                    <%--<asp:HiddenField ID="hdnBranchT" runat="server" Value='<%# Bind("BranchT") %>' />--%>

                                </ItemTemplate>
                                </asp:TemplateField>
                            <%--<asp:TemplateField HeaderText="District">
                                <ItemTemplate>
                                    <asp:Label ID="lblDistrictT" runat="server" Text='<%#Eval("District_T") %>'></asp:Label>
                                </ItemTemplate>                               
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Branch">
                                <ItemTemplate>
                                    <asp:Label ID="lblBranch" runat="server" Text='<%#Eval("Branch_T") %>'></asp:Label>
                                </ItemTemplate>                               
                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Branch Transfer">
                                <ItemTemplate>
                                    <asp:Label ID="lblBranch" runat="server" Text='<%#Eval("DepotName") %>'></asp:Label>
                                </ItemTemplate>                               
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="BO Transfer Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblExpriyDate" runat="server" Text='<%#Eval("Date_Expriy") %>'></asp:Label>
                                </ItemTemplate>                               
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="UNIT NMAE">
                                <ItemTemplate>
                                    <asp:Label ID="lblUnitName" runat="server" Text='<%#Eval("Unit_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Insecticide Name">
                                <ItemTemplate>
                                    <asp:Label ID="InsecticideName" runat="server" Text='<%#Eval("Insecticide_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Quantity">
                                <ItemTemplate>
                                    <asp:Label ID="lblOpBalQua" runat="server" Text='<%#Eval("Transfer_Balance_quantity") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Market Value">
                                <ItemTemplate>
                                    <asp:Label ID="lblOpBalMarVal" runat="server" Text='<%#Eval("Transfer_Balance_market_value") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total Value">
                                <ItemTemplate>
                                    <asp:Label ID="lblOpBalVal" runat="server" Text='<%#Eval("Transfer_Balance_value") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                           <%-- <asp:TemplateField HeaderText="Expriy Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblDateExpriy" runat="server" Text='<%#Eval("Date_Expriy") %>'></asp:Label>
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

