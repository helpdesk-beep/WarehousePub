<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/PrivateWarehouse.master" AutoEventWireup="true" CodeFile="Vacant_Godown_Rent_Bill.aspx.cs" Inherits="WarehouseLevel_Rent_Bill_SteelSilo_Vacant_Godown_Rent_Bill" %>

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
                    <tr style="background-color: #0bb6e6; height: 25px">
                        <td colspan="4" align="center">
                            <asp:Label ID="lblheading" runat="server" Font-Bold="True" Font-Size="15pt" ForeColor="WhiteSmoke"
                                Text="Steel Silo Vacant Rent Bill"></asp:Label></td>
                    </tr>
                    <tr align="center">
                        <td align="center" colspan="4">
                            <asp:Label ID="Label2" runat="server" Text="Godown Type" Font-Bold="True" Font-Size="8pt"
                                ForeColor="Navy"></asp:Label>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
       <asp:DropDownList ID="ddlGodownType" runat="server" Width="200px" Height="25px"
           AutoPostBack="True"
           OnSelectedIndexChanged="ddlGodownType_SelectedIndexChanged" Enabled="false">
           <asp:ListItem Value="0">--Select--</asp:ListItem>
           <asp:ListItem Value="1">Joint Venture Scheme(JVS)</asp:ListItem>
           <%--<asp:ListItem Value="2">Hired Capacity</asp:ListItem>--%>
           <asp:ListItem Value="3">Tribal Scheme</asp:ListItem>
           <asp:ListItem Value="4">Silo Bags</asp:ListItem>
           <asp:ListItem Value="5">Steel Silo</asp:ListItem>

       </asp:DropDownList>
                        </td>
                    </tr>
                    <tr id="trJVSGodownRent" visible="false" runat="server">
                        <td colspan="4">
                            <fieldset style="width: 980px; border: 1px solid navy;">
                                <center>
                                    <div style="overflow: scroll; height: 150px; overflow-x: hidden">
                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr>
                                                <td colspan="6" align="center" style="background-color: #0bb6e6; height: 25px">
                                                    <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Size="15pt" ForeColor="WhiteSmoke"
                                                        Text="Steel Silo Vacant Rent"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" style="height: 5px"></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label Font-Size="8pt" Font-Bold="true" ForeColor="navy" ID="lblgodown" runat="server" Text="Godown"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlgodown" runat="server" AutoPostBack="True" TabIndex="1" Enabled="false"
                                                        Height="25px" Width="200px" CssClass="tb6" Font-Size="10pt" OnSelectedIndexChanged="ddlgodown_SelectedIndexChanged">
                                                    </asp:DropDownList></td>
                                                <td>
                                                    <asp:Label ID="lblcommodity" runat="server" Text="Commodity Name" Font-Bold="True" Font-Size="8pt"
                                                        ForeColor="Navy"></asp:Label></td>
                                                <td>
                                                    <asp:DropDownList ID="ddlcomodity" runat="server" Width="200px" Height="25px"
                                                        AutoPostBack="True"
                                                        OnSelectedIndexChanged="ddlcomodity_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" style="height: 5px"></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblCropYear" runat="server" Text="Crop Year" Font-Bold="True" Font-Size="8pt"
                                                        ForeColor="Navy"></asp:Label></td>
                                                <td>
                                                    <asp:DropDownList ID="ddlCropYear" runat="server" AutoPostBack="True"
                                                        TabIndex="1" Height="25px" Width="200px" Font-Size="10pt" OnSelectedIndexChanged="ddlCropYear_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblmonth" runat="server" Text="Financial Year/Month" Font-Bold="True" Font-Size="8pt"
                                                        ForeColor="Navy"></asp:Label></td>
                                                <td>
                                                    <asp:DropDownList ID="ddlFyear" runat="server" AutoPostBack="false" Visible="true"
                                                        TabIndex="1" Height="25px" Width="90px" Font-Size="10pt" Enabled="true">
                                                        <asp:ListItem Value="0" Text="Financial Year"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="ddlmonth" runat="server" AutoPostBack="True"
                                                        TabIndex="1" Height="25px" Width="115px" Font-Size="10pt" OnSelectedIndexChanged="ddlmonth_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" style="height: 5px"></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblcrate" Visible="true" runat="server" Text="Rate(Month/Day)" Font-Bold="True" Font-Size="8pt"
                                                        ForeColor="Navy"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtcomrate" Visible="true" ReadOnly="false"
                                                        AutoPostBack="true" BackColor="white"
                                                        TabIndex="9" CssClass="tb6" Width="90px" Height="20px" OnTextChanged="txtcomrate_TextChanged"></asp:TextBox>
                                                    <asp:TextBox runat="server" ID="txtCPRate" Visible="true" ReadOnly="true" AutoPostBack="true" BackColor="white"
                                                        TabIndex="9" CssClass="tb6" Width="90px" Height="20px" Enabled="false"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="txtcomrate"
                                                        ValidChars="0123456789.">
                                                    </cc1:FilteredTextBoxExtender>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtCPRate"
                                                        ValidChars="0123456789.">
                                                    </cc1:FilteredTextBoxExtender>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label7" Visible="true" runat="server" Text="Invoice No" Font-Bold="True" Font-Size="8pt"
                                                        ForeColor="Navy"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtInvoiceNo" Visible="true"
                                                        BackColor="white" TabIndex="10" CssClass="tb6" Width="100%"></asp:TextBox>

                                                </td>
                                            </tr>


                                        </table>

                                    </div>
                                    <table width="100%">
                                        <tr>
                                            <td align="center" colspan="2">
                                                <asp:Button ID="btnSumbmitRent" runat="server" Text="Check Stock Balance"
                                                    CssClass="BTNBLUE" Enabled="true" OnClick="btnSumbmitRent_Click" />
                                                <asp:Button ID="brnCancel" runat="server" Text="Close"
                                                    CssClass="BTNBLUE" OnClick="brnCancel_Click1" />
                                            </td>

                                        </tr>
                                    </table>
                                </center>
                            </fieldset>
                        </td>
                    </tr>
                    <tr id="trHiredGodownRent" visible="false" runat="server">
                        <td colspan="4">
                            <fieldset style="width: 980px; border: 1px solid navy;">
                                <center>
                                    <div style="overflow: scroll; height: 150px; overflow-x: hidden">
                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr>
                                                <td colspan="4" align="center" style="background-color: #0bb6e6; height: 25px">
                                                    <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Size="15pt" ForeColor="WhiteSmoke"
                                                        Text="Hired Godown Rent"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblFromDate" runat="server" Text="From Date" Font-Bold="True" Font-Size="8pt"
                                                        ForeColor="Navy"></asp:Label></td>
                                                <td>
                                                    <asp:TextBox ID="txtfdate" runat="server" BackColor="LemonChiffon" AutoPostBack="true"
                                                        TabIndex="9" CssClass="tb6" Width="190px" Height="20px"
                                                        OnTextChanged="txtfdate_TextChanged"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="txtfdate">
                                                    </cc1:CalendarExtender>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblTodate" runat="server" Text="To Date" Font-Bold="True" Font-Size="8pt"
                                                        ForeColor="Navy"></asp:Label></td>
                                                <td>
                                                    <asp:TextBox ID="txttodate" runat="server" AutoPostBack="true" BackColor="LemonChiffon"
                                                        TabIndex="9" CssClass="tb6" Width="190px" Height="20px"
                                                        OnTextChanged="txttodate_TextChanged1"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="txttodate">
                                                    </cc1:CalendarExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label Font-Size="8pt" Font-Bold="true" ForeColor="navy" ID="Label5" runat="server" Text="Godown"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlGodown2" runat="server" AutoPostBack="True" TabIndex="1"
                                                        Height="25px" Width="200px" CssClass="tb6" Font-Size="10pt" OnSelectedIndexChanged="ddlGodown2_SelectedIndexChanged">
                                                    </asp:DropDownList></td>
                                                <td>
                                                    <asp:Label ID="Label10" runat="server" Text="Financial Year" Font-Bold="True" Font-Size="8pt"
                                                        ForeColor="Navy"></asp:Label></td>
                                                <td>
                                                    <asp:DropDownList ID="ddlFyear2" runat="server" AutoPostBack="True"
                                                        TabIndex="1" Height="25px" Width="200px" Font-Size="10pt" OnSelectedIndexChanged="ddlFyear2_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label9" Visible="true" runat="server" Text="Rate(Month/Day) MT" Font-Bold="True" Font-Size="8pt"
                                                        ForeColor="Navy"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtgratePM" Visible="true" ReadOnly="false"
                                                        AutoPostBack="true" BackColor="LemonChiffon"
                                                        TabIndex="9" CssClass="tb6" Width="90px" Height="20px" OnTextChanged="txtgratePM_TextChanged"></asp:TextBox>
                                                    <asp:TextBox runat="server" ID="txtgratePD" Visible="true" ReadOnly="false"
                                                        AutoPostBack="true" BackColor="LemonChiffon"
                                                        TabIndex="9" CssClass="tb6" Width="90px" Height="20px" OnTextChanged="txtgratePD_TextChanged"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtgratePM"
                                                        ValidChars="0123456789.">
                                                    </cc1:FilteredTextBoxExtender>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtgratePD"
                                                        ValidChars="0123456789.">
                                                    </cc1:FilteredTextBoxExtender>
                                                </td>

                                                <td>
                                                    <asp:Label ID="Label6" Visible="true" runat="server" Text="Storage Capacity(In MT)" Font-Bold="True" Font-Size="8pt"
                                                        ForeColor="Navy"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtSCapacity" runat="server" AutoPostBack="false" BackColor="LemonChiffon" ReadOnly="true"
                                                        TabIndex="9" CssClass="tb6" Width="190px" Height="20px"></asp:TextBox>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblmob" runat="server" Visible="true" Text="Rent" Font-Bold="True" Font-Size="8pt"
                                                        ForeColor="Navy"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtRent" Visible="true" ReadOnly="true"
                                                        AutoPostBack="false" BackColor="LemonChiffon"
                                                        TabIndex="9" Width="190px" Height="20px"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>

                                    </div>
                                    <table width="100%">
                                        <tr>
                                            <td align="center" colspan="2">
                                                <asp:Button ID="btnHSubmit" runat="server" Text="Check Stock Balance"
                                                    CssClass="BTNBLUE" Enabled="true" OnClick="btnHSubmit_Click" />
                                                <asp:Button ID="btnHCancel" runat="server" Text="Close"
                                                    CssClass="BTNBLUE" OnClick="btnHCancel_Click" />
                                            </td>

                                        </tr>
                                    </table>
                                </center>
                            </fieldset>
                        </td>
                    </tr>
                    <tr id="trReportsView" visible="false" runat="server">
                        <td colspan="4">
                            <%--<fieldset style="width: 980px; border: 1px solid navy;">
<center>
<div style="overflow: scroll; height: 500px; overflow-x: hidden">
  
    <rsweb:ReportViewer ID="ReportViewer_SC" runat="server" Width="100%" >
    </rsweb:ReportViewer>
</div>
</center>
</fieldset>--%>
                        </td>
                    </tr>
                    <tr id="trRentBill" visible="false" runat="server">
                        <td colspan="4">
                            <fieldset style="width: 980px; border: 1px solid navy;">
                                <center>
                                    <div style="overflow: scroll; height: 400px; overflow-x: hidden">
                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr>
                                                <td colspan="6" align="center" style="background-color: #0bb6e6; height: 25px">
                                                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="15pt" ForeColor="WhiteSmoke"
                                                        Text="Steel Silo Vacant Rent Bill Detail"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6" align="center">
                                                    <asp:GridView ID="gvIStorageCharge" runat="server" AutoGenerateColumns="false" OnRowDataBound="gvIStorageCharge_RowDataBound">
                                                        <Columns>
                                                            <asp:BoundField HeaderText="Date" DataFormatString="{0:dd/MM/yyyy}" DataField="Deposit_Date" />
                                                            <asp:BoundField HeaderText="Opening Balance" DataField="Opening_Balance" />
                                                            <asp:BoundField HeaderText="Receive Bags" DataField="Receive_Bags" />
                                                            <asp:BoundField HeaderText="Issue Bags" DataField="Issue_Bags" />
                                                            <asp:BoundField HeaderText="Closing Bag Balance" DataField="Closing_Balance" />

                                                            <asp:BoundField HeaderText="Godown_Id" DataField="Godown_Id" />
                                                            <asp:BoundField HeaderText="Opening Weight" DataField="Opening_Weight" />
                                                            <asp:BoundField HeaderText="Receive Weight" DataField="Receive_Weight" />
                                                            <asp:BoundField HeaderText="Issue Weight" DataField="Issue_Weight" />
                                                            <asp:BoundField HeaderText="Closing Weight Balance" DataField="Closing_Weight" />
                                                            <asp:BoundField HeaderText="Chargeable Weight Balance" DataField="Chargeable_Weight" />
                                                            <asp:BoundField HeaderText="Per Day Rate" DataField="Per_Day_Rate" />
                                                            <asp:TemplateField HeaderText="">
                                                                <HeaderTemplate>
                                                                    <div style="text-align: center;">
                                                                        Total Charges
                                                                    </div>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <div style="text-align: right;">
                                                                        <asp:Label ID="lblCharges" runat="server" Text='<%# Eval("Charges")%>'></asp:Label>
                                                                    </div>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="Total Charges" DataField="Charges" />
                                                            <%--<asp:BoundField HeaderText="Total Charges" DataField="Weight_Charges" />--%>
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>

                                    </div>
                                    <table width="100%">
                                        <tr>
                                            <td align="center" colspan="2">
                                                <asp:Button ID="btnGenBill" runat="server" Text="Generate Bill" Visible="false"
                                                    CssClass="BTNBLUE" OnClick="btnGenBill_Click" />
                                                <asp:Button ID="btncancel2" runat="server" Text="Cancel" Visible="false" Width="110px"
                                                    CssClass="BTNBLUE" OnClick="btncancel2_Click1" />
                                            </td>

                                        </tr>
                                        <tr>
                                            <td align="center" colspan="2">
                                                <asp:Label ID="Lblmsg2" runat="server" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>
                                            </td>

                                        </tr>
                                    </table>
                                </center>
                            </fieldset>
                        </td>
                    </tr>
                </table>
            </div>
        </center>
    </fieldset>
    <asp:HiddenField ID="hdnAmt" runat="server" Value="0" />
</asp:Content>

