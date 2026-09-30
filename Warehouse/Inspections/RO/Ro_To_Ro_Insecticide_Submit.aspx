<%@ Page Language="C#" MasterPageFile="~/Inspections/Masters/Inspection_RO.master" AutoEventWireup="true" CodeFile="Ro_To_Ro_Insecticide_Submit.aspx.cs" Inherits="Inspections_RO_Ro_To_Ro_Insecticide_Submit"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


  
    <div>
        <table style="width: 100%;">
            <tr id="msg">
                <td colspan="4">
                    <asp:Label ID="lblmsg" Text="" ForeColor="red" Font-Bold="true" runat="server"></asp:Label></td>
            </tr>
            <tr style="background-color: #0bb6e6; height: 25px">
                <td colspan="12" align="center">
                    <asp:Label ID="Label37" runat="server" Font-Bold="True" Font-Size="15pt" ForeColor="WhiteSmoke"
                        Text="Ro To Ro Transfer Insecticide Entry"></asp:Label></td>
            </tr>Ro_To_Ro_Insecticide_Submit.aspx
            <tr>
                 <td>
                    <asp:Label ID="lbltxtRmTransferDate" Font-Size="11pt" Font-Bold="true" ForeColor="navy" runat="server" Text="Rm Transfer Date"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtRmTransferDate" runat="server" class="form-control" placeholder="DD/MM/YYYY"></asp:TextBox>
                    <asp:RegularExpressionValidator runat="server" ControlToValidate="txtRmTransferDate" ValidationExpression="(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$"
                        ErrorMessage="Invalid date format." ValidationGroup="A" ForeColor="Red" />
                </td>
                <td>
                    <asp:Label ID="Label6" Font-Size="11pt" Font-Bold="true" ForeColor="navy" runat="server" Text="Advices No"></asp:Label>
                </td>
                <td valign="middle">
                    <asp:TextBox runat="server" class="form-control" ID="txtAdivcesNo" placeholder="Advices No"  />
                </td>
                <td>
                    <asp:Label ID="Label4" Font-Size="11pt" Font-Bold="true" ForeColor="navy" runat="server" Text="Region"></asp:Label>
                </td>
                 <td style="text-align: left;">
                    <asp:DropDownList ID="ddlregionFrom" runat="server" AutoPostBack="true" class="form-control">
                    </asp:DropDownList>
                </td>
                
              
               
                
            </tr>
            <tr>
                <td colspan="4" style="height: 5px"></td>
            </tr>


            <tr>
                <td>
                    <asp:Label ID="Label12" Font-Size="11pt" Font-Bold="true" ForeColor="navy" runat="server" Text="Region To"></asp:Label>
                </td>
                 <td style="text-align: left;">
                    <asp:DropDownList ID="ddlregionTo" runat="server" AutoPostBack="true" class="form-control">
                    </asp:DropDownList>
                </td>
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
                <td>
                    <asp:Label ID="Label10" Font-Size="11pt" Font-Bold="true" ForeColor="navy" runat="server" Text="Quantity"></asp:Label>
                </td>
                <td valign="middle">
                    <asp:TextBox runat="server" class="form-control" ID="txtQuantity" placeholder="Quantity" AutoPostBack="true" OnTextChanged="txtQuantity_TextChanged" />
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
                    <asp:Label ID="Label7" Font-Size="11pt" Font-Bold="true" ForeColor="navy" runat="server" Text="Tranfport Paymant"></asp:Label>
                </td>
                <td>
                    <asp:TextBox runat="server" class="form-control" ID="txtTransportPaymant" placeholder="Transport Paymant" />

                </td>
                <td style="font-weight: bold; text-align: right;">
                    <%--<asp:Label ID="Label20" runat="server" Text="Remarks : "></asp:Label>--%>
                    <asp:Label ID="Label11" Font-Size="11pt" Font-Bold="true" ForeColor="navy" runat="server" Text="Remarks"></asp:Label>
                </td>
                <td colspan="5">
                    <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" Height="80px" TextMode="MultiLine" MaxLength="300"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="4" style="height: 5px"></td>
            </tr>
            <tr>
                <td align="center" colspan="6">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" Visible="true" Width="100px"
                        CssClass="BTNBLUE" OnClick="btnSubmit_Click" ValidationGroup="A" Height="26px" />

                    <asp:Button ID="btnUpdate" runat="server" Text="Update" Visible="false" Width="100px"
                        CssClass="BTNBLUE" OnClick="btnUpdate_Click" />
                  
                </td>
            </tr>
        </table>
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
                                    <asp:HiddenField ID="hdnID" runat="server" Value='<%# Bind("id") %>'/>
                                    <asp:HiddenField ID="hdnRegion" runat="server" Value='<%# Bind("Region") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Ro Transfer Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblRo_Transfer_Date" runat="server" Text='<%#Eval("Ro_Transfer_Date") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtRo_Transfer_Date" runat="server" Text='<%#Eval("Ro_Transfer_Date") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Advices No">
                                <ItemTemplate>
                                    <asp:Label ID="lblAdvicesNo" runat="server" Text='<%#Eval("AdvicesNo") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtAdvicesNo" runat="server" Text='<%#Eval("AdvicesNo") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Region To">
                                <ItemTemplate>
                                    <asp:Label ID="lblRegionFrom" runat="server" Text='<%#Eval("RegionNameTO") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtRegionFrom" runat="server" Text='<%#Eval("RegionNameTO") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Region From">
                                <ItemTemplate>
                                    <asp:Label ID="lblRegionNsmeTo" runat="server" Text='<%#Eval("RegionNsmeFrom") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtRegionNsmeTo" runat="server" Text='<%#Eval("RegionNsmeFrom") %>'></asp:TextBox>
                                </EditItemTemplate>
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
                            <asp:TemplateField HeaderText="Transport Payment">
                                <ItemTemplate>
                                    <asp:Label ID="lblTransportpayment" runat="server" Text='<%#Eval("Transportpayment") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtTransportpayment" runat="server" Text='<%#Eval("Transportpayment") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remarks">
                                <ItemTemplate>
                                    <asp:Label ID="lblRemarks" runat="server" Text='<%#Eval("Remarks") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtRemarks" runat="server" Text='<%#Eval("Remarks") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                          <%--  <asp:TemplateField HeaderText="Edit Details" ItemStyle-Width="15%">
                                <ItemTemplate>
                                    <asp:Button ID="btnfilloverallinsp" Text="Edit" runat="server" CommandName="Overallinsp" CssClass="btn btn-info" CommandArgument='<%# Container.DataItemIndex %>' />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>--%>
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

