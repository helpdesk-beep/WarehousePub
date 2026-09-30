<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/PrivateWarehouse.master" AutoEventWireup="true" CodeFile="Steel_Silo_Bill_Details.aspx.cs" Inherits="WarehouseLevel_Rent_Bill_SteelSilo_Steel_Silo_Bill_Details" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset style="width: 1000px; border: 2px solid navy; background-color: white;">
        <center>
            <div>
                <table width="1000px">
                    <tr id="msg">
                        <td colspan="4">
                            <asp:Label ID="lblmsg" Text="" ForeColor="red" Font-Bold="true" runat="server"></asp:Label></td>
                    </tr>
                    <tr id="trRentBill">
                        <td colspan="4">
                            <fieldset style="width: 980px; border: 1px solid navy;">
                                <center>
                                    <div style="overflow: scroll; height: 400px; overflow-x: hidden">
                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr>
                                                <td colspan="6" align="center" style="background-color: #0bb6e6; height: 25px">
                                                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="15pt" ForeColor="WhiteSmoke"
                                                        Text="Steel Silo Filled Rent Bill Detail"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr align="center">
                                                <td align="center" colspan="6">
                                                    <asp:Label ID="Label2" runat="server" Text="Bill Type" Font-Bold="True" Font-Size="8pt"
                                                        ForeColor="Navy"></asp:Label>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                       <asp:DropDownList ID="ddlBillType" runat="server" Width="200px" Height="25px" AutoPostBack="True" OnSelectedIndexChanged="ddlBillType_SelectedIndexChanged">
                                                           <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                           <asp:ListItem Value="1">Filled Rent Bill</asp:ListItem>
                                                           <asp:ListItem Value="2">Vacant Capacity Rent Bill</asp:ListItem>
                                                           <asp:ListItem Value="3">Service Charges Rent Bill</asp:ListItem>
                                                           <asp:ListItem Value="4">Procurment Charges Rent Bill</asp:ListItem>
                                                           <asp:ListItem Value="5">Variable Charges Rent Bill</asp:ListItem>
                                                           <asp:ListItem Value="6">MPSCSC Filled Storage Charges Bill</asp:ListItem>
                                                           <asp:ListItem Value="7">Dir.Food Vacant Capacity Storage Charges Bill</asp:ListItem>
                                                       </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6" align="center">
                                                    <asp:GridView ID="gvIStorageCharge" runat="server" AutoGenerateColumns="false" OnRowCommand="gvIStorageCharge_RowCommand">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="S.No." ItemStyle-Width="3%">
                                                                <ItemTemplate>
                                                                    <%# Container.DataItemIndex + 1 %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="Bill Number" DataField="Bill_Number" />
                                                            <asp:BoundField HeaderText="Invoice Number" DataField="Invoice_No" />
                                                            <asp:BoundField HeaderText="Commodity" DataField="Commodity_Name" />
                                                            <asp:BoundField HeaderText="Crop Year" DataField="Crop_Year" />
                                                            <asp:BoundField HeaderText="Financial Year" DataField="Financial_Year" />
                                                            <asp:BoundField HeaderText="Month" DataField="Month" />
                                                            <asp:BoundField HeaderText="From Date" DataField="FromDate" />
                                                            <asp:BoundField HeaderText="To Date" DataField="ToDate" />
                                                            <asp:BoundField HeaderText="Amount" DataField="Net_Amount" />
                                                            <asp:TemplateField HeaderText="">
                                                                <HeaderTemplate>
                                                                    <div style="text-align: center;">
                                                                        Print Bill
                                                                    </div>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <div style="text-align: right;">
                                                                        <%--<asp:HyperLink ID="HyperLink1" runat="server" Target="_blank" NavigateUrl='<%#"Print_Filld_Godown_Rent_Bill.aspx?BN="+ Base64Encode(Eval("Bill_Number").ToString()) %>'
                                                                            CssClass="btn btn-large btn-success" title="Print Bill"> <i class="fa fa-print"></i></asp:HyperLink>--%>
                                                                        <asp:LinkButton ID="btnLock" runat="server" CommandArgument='<%# Eval("Bill_Number")%>'
                                                                            CssClass="btn btn-primary" EnableTheming="false" CommandName="Print">Print</asp:LinkButton>
                                                                        <asp:HiddenField ID="hdnBillCategoryID" runat="server" Value='<%# Eval("Bill_Category_Type_ID")%>' />
                                                                    </div>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>

                                    </div>
                                </center>
                            </fieldset>
                        </td>
                    </tr>
                </table>
            </div>
        </center>
    </fieldset>
    <asp:HiddenField ID="hdnBillNumber" runat="server" Value="0" />
    <asp:HiddenField ID="hdnBillCategory" runat="server" Value="0" />
    <asp:HiddenField ID="hdnAmt" runat="server" Value="0" />
</asp:Content>
