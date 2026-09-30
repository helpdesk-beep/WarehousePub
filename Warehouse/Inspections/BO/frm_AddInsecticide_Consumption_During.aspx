<%@ Page Language="C#" MasterPageFile="~/Inspections/Masters/Inspection_BO.master" AutoEventWireup="true" CodeFile="frm_AddInsecticide_Consumption_During.aspx.cs" Inherits="Inspections_BO_frm_AddInsecticide_Consumption_During" Title="Account Detail" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .auto-style1 {
            margin-left: 0px;
        }
    </style>
    <style type="text/css">
        .auto-style1 {
            width: 340px;
        }

        .auto-style2 {
            width: 253px;
        }

        .auto-style3 {
            margin-left: 13px;
            margin-top: 17px;
        }
    </style>
    <style type="text/css">
        .auto-style1 {
            width: 341px;
        }

        .auto-style2 {
            width: 223px;
        }
    </style>
    <style type="text/css">
        .auto-style1 {
            width: 232px;
        }

        .auto-style2 {
            width: 233px;
        }

        .auto-style3 {
            width: 235px;
        }

        .auto-style4 {
            width: 385px;
        }

        .auto-style5 {
            height: 23px;
            margin-top: 0px;
        }

        .auto-style6 {
            height: 1px;
        }

        .auto-style7 {
            height: 25px;
        }

        .auto-style8 {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <%--  <fieldset style="width: 1000px; border: 2px solid navy;">
        <center>--%>
    <div>
        <table class="auto-style8">
            <tr id="msg">

                <td colspan="4">
                    <asp:Label ID="lblmsg" Text="" ForeColor="red" Font-Bold="true" runat="server"></asp:Label></td>
            </tr>
            <tr style="background-color: #0bb6e6; height: 25px">
                <td colspan="8" align="center">
                    <asp:Label ID="Label37" runat="server" Font-Bold="True" Font-Size="15pt" ForeColor="WhiteSmoke"
                        Text=" Consumption / Transfer During Entry"></asp:Label></td>

            </tr>
            <tr>
                <td></td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="11pt"
                        ForeColor="Navy" Text="Insecticide Name"></asp:Label>
                </td>
                <td valign="middle" class="auto-style4">
                    <asp:DropDownList ID="ddlinsecticide" Height="35px" Class="form-control" runat="server"
                        OnSelectedIndexChanged="ddlinsecticide_SelectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem Value="0">-------Select------</asp:ListItem>
                        <asp:ListItem Value="1">Alluminium Phosphide</asp:ListItem>
                        <asp:ListItem Value="2">Melathion</asp:ListItem>
                        <asp:ListItem Value="3">Deltamethrin</asp:ListItem>
                    </asp:DropDownList></td>

                <td>
                    <asp:Label Font-Size="11pt" Font-Bold="true" ForeColor="navy" ID="Label8" runat="server" Text="Unit"></asp:Label></td>
                <td class="auto-style4">
                    <asp:DropDownList ID="ddlUnitStock" Height="35px" Class="form-control" runat="server">
                        <asp:ListItem Value="0">-----Select------</asp:ListItem>
                        <asp:ListItem Value="1">KiloGram(KG)</asp:ListItem>
                        <asp:ListItem Value="2">Liter</asp:ListItem>
                    </asp:DropDownList>
                </td>

            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label4" Font-Size="11pt" Font-Bold="true" ForeColor="navy" runat="server" Text="Date Of Delivery/Consumption"></asp:Label>
                </td>
                <td valign="middle" class="auto-style2">
                    <asp:TextBox ID="txtdob" placeholder="DD/MM/YY" onclick="return ValidateDOB()" CssClass="form-control" runat="server" Height="24px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtdob" ValidationGroup="A" ErrorMessage="* required" runat="server"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator runat="server" ControlToValidate="txtdob" ValidationExpression="(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$"
                        ErrorMessage="Invalid date format." ValidationGroup="A" ForeColor="Red" />
                </td>
                <td>
                    <asp:Label ID="Label6" Font-Size="11pt" Font-Bold="true" ForeColor="navy" runat="server" Text="Godown NO"></asp:Label>
                </td>
                <td valign="middle" class="auto-style2">
                    <asp:DropDownList ID="ddl_gdwn" runat="server" AutoPostBack="false" class="form-control" Height="28px">
                    </asp:DropDownList>
                    <%--<input type="text" runat="server" class="form-control" id="txtGodownNo" placeholder="Godown No"/>--%>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label Font-Size="11pt" Font-Bold="true" ForeColor="navy" ID="Label13" runat="server" Text="Sources Of Delivery"></asp:Label>
                </td>
                <td class="auto-style4">
                    <asp:DropDownList ID="DropDownList1" Height="30px" Class="form-control" AutoPostBack="false" runat="server">
                        <%--<asp:ListItem Value="0">Select</asp:ListItem>--%>
                        <asp:ListItem Value="1">Consumption Of Self Branch</asp:ListItem>
                        <%--<asp:ListItem Value="2">Transfer To Other Branch</asp:ListItem>--%>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="Label10" Font-Size="11pt" Font-Bold="true" ForeColor="navy" runat="server" Text="Quantity"></asp:Label>
                </td>
                <td valign="middle">
                    <asp:TextBox runat="server" class="form-control" ID="txtQuantity" placeholder="Quantity" AutoPostBack="true" Height="25px" OnTextChanged="txtQuantity_TextChanged"/>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label Font-Size="11pt" Font-Bold="true" ForeColor="navy" ID="Label5" runat="server" Text="Total Quantity"></asp:Label>
                </td>
                <td valign="middle">
                    <asp:TextBox runat="server" class="form-control" ID="txttotalQuantity" ReadOnly="true" placeholder="Total Quantity" AutoPostBack="false" Height="25px"/>
                </td>
                <td>
                    <asp:Label ID="Label1" Font-Size="11pt" Font-Bold="true" ForeColor="navy" runat="server" Text="Market Rate"></asp:Label>
                </td>
                <td class="auto-style2">
                    <asp:TextBox runat="server" class="form-control" ID="txtMarketRed" placeholder="Market Rate" OnTextChanged="txtMarketRed_TextChanged" AutoPostBack="true" Height="25px" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label3" Font-Size="11pt" Font-Bold="true" ForeColor="navy" runat="server" Text="Value"></asp:Label>
                </td>
                <td valign="middle">
                    <input type="text" runat="server" class="auto-style5" id="txtValue" placeholder="Value" height="25px" readonly="true" />
                </td>
            </tr>

            <tr>
                <td align="center" colspan="6">
                    <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-success" Text="Submit" Visible="true" Width="100px" ValidationGroup="A"
                        OnClick="btnSubmit_Click" />

                </td>
            </tr>
        </table>
        <div class="container pt-2">
            <h4 class="text-info">List Consumption Entry</h4>
            <div class="row">
                <div class="col-12">

                    <asp:GridView ID="GVOfStock" OnRowCommand="GVOfStock_RowCommand"
                        AutoGenerateColumns="false" runat="server"
                        OnRowCreated="GVOfStock_RowCreated">
                        <HeaderStyle
                            BackColor="#D69758"
                            Font-Italic="false"
                            ForeColor="Snow" />
                        <Columns>

                            <asp:TemplateField HeaderText="S.No." ItemStyle-Width="3%">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                    <asp:HiddenField ID="hdnpfid" runat="server" Value='<%# Bind("ID") %>' />
                                    <asp:HiddenField ID="hdnpfbid" runat="server" Value='<%# Bind("Branch_ID") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Godwon Name">
                                <ItemTemplate>
                                    <asp:Label ID="godown_no" runat="server" Text='<%#Eval("Godown_Name") %>'></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date">
                                <ItemTemplate>
                                    <asp:Label ID="txt_date" runat="server" Text='<%#Eval("date") %>'></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="UNIT NAME">
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
                                    <asp:Label ID="ob_quantity" runat="server" Text='<%#Eval("Consumption_Balance_quantity") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txt_ob_quantity" runat="server" Text='<%#Eval("Consumption_Balance_quantity") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Market Value">
                                <ItemTemplate>
                                    <asp:Label ID="Con_market_value" runat="server" Text='<%#Eval("Consumption_Balance_market_value") %>'></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total Value">
                                <ItemTemplate>
                                    <asp:Label ID="Con_value" runat="server" Text='<%#Eval("Consumption_Balance_value") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Edit">
                                <ItemTemplate>
                                    <asp:Button ID="btnEdit" Text="Edit" runat="server" CssClass="btn btn-primary" CommandName="EditRow" CommandArgument='<%# Container.DataItemIndex %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remove">
                                <ItemTemplate>
                                    <asp:Button ID="btnRemove" Text="Remove" runat="server" CssClass="btn btn-danger" CommandName="RemoveRow" CommandArgument='<%# Container.DataItemIndex %>' OnClientClick="return confirm('Do you want to Remove this row?');" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>

                </div>
            </div>
        </div>
    </div>
</asp:Content>

