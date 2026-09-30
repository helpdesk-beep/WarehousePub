<%@ Page Language="C#" MasterPageFile="~/Inspections/Masters/Inspection_BO.master" AutoEventWireup="true" CodeFile="Update_Transfer_Entry.aspx.cs" Inherits="Inspections_BO_Update_Transfer_Entry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    >
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
                        Text="Transfer During Entry"></asp:Label></td>

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
                    <asp:DropDownList ID="ddlinsecticide" Height="35px" Class="form-control" runat="server">
                        <asp:ListItem Value="0">-------Select------</asp:ListItem>
                        <asp:ListItem Value="1">Alluminium Phosphide</asp:ListItem>
                        <asp:ListItem Value="2">Melaphion</asp:ListItem>
                        <asp:ListItem Value="3">Deltamethrin</asp:ListItem>
                    </asp:DropDownList></td>

                <td>
                    <asp:Label Font-Size="11pt" Font-Bold="true" ForeColor="navy" ID="Label8" runat="server" Text="Unit"></asp:Label></td>
                <td class="auto-style4">
                    <asp:DropDownList ID="ddlUnitStock" Height="35px" Class="form-control" runat="server">
                        <asp:ListItem Value="0">-----Select------</asp:ListItem>
                        <asp:ListItem Value="1">KiloGram(KG)</asp:ListItem>
                        <asp:ListItem Value="2">Litter</asp:ListItem>
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
                        <%--<asp:ListItem Value="1">Consumption Of Self Branch</asp:ListItem>--%>
                        <asp:ListItem Value="2">Transfer To Other Branch</asp:ListItem>
                    </asp:DropDownList>
                </td>

                <td>
                    <asp:Label ID="Label7" Font-Size="11pt" Font-Bold="true" ForeColor="navy" runat="server" Text="Quantity"></asp:Label>
                </td>
                <td>
                    <asp:TextBox runat="server" class="form-control" ID="txtTfQuantity" placeholder="Quantity" AutoPostBack="true" Height="25px" OnTextChanged="txtTfQuantity_TextChanged" />
                </td>
            </tr>
            <tr>

                <td>
                    <asp:Label ID="Label9" Font-Size="11pt" Font-Bold="true" ForeColor="navy" runat="server" Text="Market Rate"></asp:Label>
                </td>
                <td>
                    <asp:TextBox runat="server" class="form-control" ID="txtTfMarketRate" placeholder="Market Rate" OnTextChanged="txtTfMarketRate_TextChanged" AutoPostBack="true" Height="25px" />

                </td>
            
                <td>
                    <asp:Label ID="Label11" Font-Size="11pt" Font-Bold="true" ForeColor="navy" runat="server" Text="Value"></asp:Label>
                </td>
                <td>
                    <input type="text" runat="server" class="auto-style7" id="txtTfValue" placeholder="Value" height="25px" />
                </td>

            </tr>

            <tr>
                <td align="center" colspan="6">
                    <asp:Button ID="btnSubmit" runat="server" Text="Update" Visible="true" Width="100px" ValidationGroup="A"
                        CssClass="BTNBLUE" OnClick="btnSubmit_Click" />

                </td>
            </tr>
        </table>
    </div>
</asp:Content>

