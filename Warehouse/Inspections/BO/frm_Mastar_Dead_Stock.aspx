<%@ Page Language="C#" MasterPageFile="~/Inspections/Masters/Inspection_BO.master" AutoEventWireup="true" CodeFile="frm_Mastar_Dead_Stock.aspx.cs" Inherits="Inspections_BO_frm_frm_Mastar_Dead_Stock" Title="Account Detail" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <%--  <fieldset style="width: 1000px; border: 2px solid navy;">
        <center>--%>    
    <div>
        <table style="width: 100%;">
            <tr id="msg">
                <td colspan="4">
                    <asp:Label ID="lblmsg" Text="" ForeColor="red" Font-Bold="true" runat="server"></asp:Label></td>
            </tr>
            <tr style="background-color: #0bb6e6; height: 25px">
                <td colspan="6" align="center">
                    <asp:Label ID="Label37" runat="server" Font-Bold="True" Font-Size="15pt" ForeColor="WhiteSmoke"
                        Text="Insecticide Opening Balance Entry"></asp:Label></td>
            </tr>

           <%-- <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="11pt"
                        ForeColor="Navy" Text="Insecticide Name"></asp:Label>
                </td>
                <td valign="middle">
                    <asp:DropDownList ID="ddlinsecticide" Height="35px" Class="form-control" runat="server">
                        <asp:ListItem Value="0">Select</asp:ListItem>
                        <asp:ListItem Value="1">Alluminium Phosphide</asp:ListItem>
                        <asp:ListItem Value="2">Malathion</asp:ListItem>
                        <asp:ListItem Value="3">Deltamethrin</asp:ListItem>
                    </asp:DropDownList></td>
                <td>
                    <asp:Label Font-Size="11pt" Font-Bold="true" ForeColor="navy" ID="Label8" runat="server" Text="Unit"></asp:Label></td>
                <td>
                    <asp:DropDownList ID="ddlUnitStock" Height="35px" Class="form-control" runat="server">
                        <asp:ListItem Value="0">Select</asp:ListItem>
                        <asp:ListItem Value="1">KiloGram(KG)</asp:ListItem>
                        <asp:ListItem Value="2">Litter</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>--%>
            <tr>
                <td colspan="4" style="height: 5px"></td>
            </tr>


            <tr>
                <td>
                    <asp:label id="label1" font-size="11pt" font-bold="true" forecolor="navy" runat="server" text="Id"></asp:label>
                </td>
                <td>
                    <asp:textbox runat="server" class="form-control" id="txtID" placeholder="Id"/>

                </td>
                <td>
                    <asp:Label ID="Label10" Font-Size="11pt" Font-Bold="true" ForeColor="navy" runat="server" Text="Category Name"></asp:Label>
                </td>
                <td valign="middle">
                    <asp:TextBox runat="server" class="form-control" ID="txtCategory_Name" placeholder="Category Name" AutoPostBack="false" />
                </td>
                
            </tr>
          
            <tr>
                <td colspan="4" style="height: 5px"></td>
            </tr>
            <tr>
                <td align="center" colspan="6">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" Visible="true" Width="100px"
                        CssClass="BTNBLUE" OnClick="btnSubmit_Click" />

                    <asp:Button ID="btnUpdate" runat="server" Text="Update" Visible="false" Width="100px"
                        CssClass="BTNBLUE" OnClick="btnUpdate_Click"/>
                    <%-- <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="100px"
                                CssClass="BTNBLUE" OnClick="btnCancel_Click" />--%>
                </td>
            </tr>
        </table>
        <div class="container pt-2">
            <h4 class="text-info">Category Name Add</h4>
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
                                    <asp:HiddenField ID="hdnID" runat="server" Value='<%# Bind("ID") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ID">
                                <ItemTemplate>
                                    <asp:Label ID="txtID" runat="server" Text='<%#Eval("ID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Category Name">
                                <ItemTemplate>
                                    <asp:Label ID="txtCategory_Name" runat="server" Text='<%#Eval("Category_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                           
                           
                          
                           
                            <asp:TemplateField HeaderText="Edit Details" ItemStyle-Width="15%">
                                <ItemTemplate>
                                    <asp:Button ID="btnfilloverallinsp" Text="Edit" runat="server" CommandName="Overallinsp" CssClass="btn btn-info" CommandArgument='<%# Container.DataItemIndex %>' />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                          
                        </Columns>
                    </asp:GridView>

                </div>
            </div>
        </div>
    </div>
    <%-- </center>
    </fieldset>--%>
</asp:Content>

