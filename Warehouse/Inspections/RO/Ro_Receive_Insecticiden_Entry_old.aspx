<%@ Page Language="C#" MasterPageFile="~/Inspections/Masters/Inspection_RO.master" AutoEventWireup="true" CodeFile="Ro_Receive_Insecticiden_Entry.aspx.cs" Inherits="Inspections_Ro_Receive_Insecticiden_Entry" Title="Account Detail" %>

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
            <tr>
                <%--<td>
                    <asp:Label ID="Label4" Font-Size="11pt" Font-Bold="true" ForeColor="navy" runat="server" Text="Region"></asp:Label>
                </td>
                 <td style="text-align: left;">
                    <asp:DropDownList ID="ddlregion" runat="server" AutoPostBack="true" class="form-control">
                    </asp:DropDownList>
                </td>--%>
               <%-- <td>
                    <asp:TextBox ID="txtPurchase" runat="server" class="form-control" placeholder="DD/MM/YYYY"></asp:TextBox>
                    <asp:RegularExpressionValidator runat="server" ControlToValidate="txtPurchase" ValidationExpression="(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$"
                        ErrorMessage="Invalid date format." ValidationGroup="A" ForeColor="Red" />
                </td>--%>

                <%--<td valign="middle">
                    <asp:TextBox ID="txtdob" placeholder="DD/MM/YY" onclick="return ValidateDOB()" CssClass="form-control" runat="server" AutoComplete="off"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtdob" ValidationGroup="A" ErrorMessage="* required" runat="server"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator runat="server" ControlToValidate="txtdob" ValidationExpression="(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$"
                        ErrorMessage="Invalid date format." ValidationGroup="A" ForeColor="Red" />
                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtdob" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                </td>--%>
                <td>
                    <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="11pt"
                        ForeColor="Navy" Text="Insecticide Name"></asp:Label>
                </td>
                <td valign="middle">
                    <asp:DropDownList ID="ddlinsecticide" Height="35px" Class="form-control" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlinsecticide_SelectedIndexChanged">
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
            </tr>
            <tr>
                <td colspan="4" style="height: 5px"></td>
            </tr>


            <tr>
                <td>
                    <asp:Label ID="Label10" Font-Size="11pt" Font-Bold="true" ForeColor="navy" runat="server" Text="Quantity"></asp:Label>
                </td>
                <td valign="middle">
                    <asp:TextBox runat="server" class="form-control" ID="txtQuantity" placeholder="Quantity" AutoPostBack="false" />
                </td>
                <td>
                    <asp:Label ID="Label1" Font-Size="11pt" Font-Bold="true" ForeColor="navy" runat="server" Text="Market Rate"></asp:Label>
                </td>
                <td>
                    <asp:TextBox runat="server" class="form-control" ID="txtMarketRed" placeholder="Market Rate" OnTextChanged="txtMarketRed_TextChanged" AutoPostBack="true" />

                </td>
                <td>
                    <asp:Label ID="Label3" Font-Size="11pt" Font-Bold="true" ForeColor="navy" runat="server" Text="Total Value"></asp:Label>
                </td>
                <td valign="middle">
                    <input type="text" runat="server" class="form-control" id="txtValue" placeholder="Value" readonly="true" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label5" Font-Size="11pt" Font-Bold="true" ForeColor="navy" runat="server" Text="Expriy Date"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtExpriy" runat="server" class="form-control" placeholder="DD/MM/YYYY"></asp:TextBox>
                    <asp:RegularExpressionValidator runat="server" ControlToValidate="txtExpriy" ValidationExpression="(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$"
                        ErrorMessage="Invalid date format." ValidationGroup="A" ForeColor="Red" />
                </td>
                <td valign="middle">
                    <asp:Label Font-Size="11pt" Font-Bold="true" ForeColor="navy" ID="lblbal" Text="Total Opening Balance" runat="server" ></asp:Label>
                </td>
                 <td>
                    <asp:Label Font-Size="11pt" class="form-control" Font-Bold="true" ForeColor="navy" ID="lblincbal" runat="server" ></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="4" style="height: 5px"></td>
            </tr>
            <tr>
                <td align="center" colspan="6">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" Visible="true" Width="100px"
                        CssClass="BTNBLUE" OnClick="btnSubmit_Click" ValidationGroup="A" />

                    <asp:Button ID="btnUpdate" runat="server" Text="Update" Visible="false" Width="100px"
                        CssClass="BTNBLUE" OnClick="btnUpdate_Click" />
                    <%-- <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="100px"
                                CssClass="BTNBLUE" OnClick="btnCancel_Click" />--%>
                </td>
            </tr>
        </table>
        <div class="container pt-2">
            <h4 class="text-info">List Insecticide Opening Balance Entry</h4>
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
                            <%--<asp:TemplateField HeaderText="Region">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Region" runat="server" Text='<%#Eval("region") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txt_Purchase" runat="server" Text='<%#Eval("region") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>--%>
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
                                <EditItemTemplate>
                                    <asp:TextBox ID="txt_ob_quantity" runat="server" Text='<%#Eval("Opening_Balance_quantity") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Market Value">
                                <ItemTemplate>
                                    <asp:Label ID="ob_market_value" runat="server" Text='<%#Eval("Opening_Balance_market_value") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txt_op_market_value" runat="server" Text='<%#Eval("Opening_Balance_market_value") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total Value">
                                <ItemTemplate>
                                    <asp:Label ID="ob_value" runat="server" Text='<%#Eval("Opening_Balance_value") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txt_ob_value" runat="server" Text='<%#Eval("Opening_Balance_value") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Expriy Date">
                                <ItemTemplate>
                                    <asp:Label ID="ob_Expriy" runat="server" Text='<%#Eval("Date_Expriy") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txt_Expiy" runat="server" Text='<%#Eval("Date_Expriy") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Edit Details" ItemStyle-Width="15%">
                                <ItemTemplate>
                                    <asp:Button ID="btnfilloverallinsp" Text="Edit" runat="server" CommandName="Overallinsp" CssClass="btn btn-info" CommandArgument='<%# Container.DataItemIndex %>' />
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

