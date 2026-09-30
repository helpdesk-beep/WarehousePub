<%@ Page Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="frm_MPSCSC_SC_Bill_Test.aspx.cs" Inherits="Accounting_frm_MPSCSC_SC_Bill_Test" Title="MPSCSC SC Bill" %>

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
                        <td>
                            <asp:Label ID="lblmsg" Text="" ForeColor="red" Font-Bold="true" runat="server"></asp:Label></td>
                    </tr>
                    <tr style="background-color: #0bb6e6; height: 25px">
                        <td align="center">
                            <asp:Label ID="lblheading" runat="server" Font-Bold="True" Font-Size="15pt" ForeColor="WhiteSmoke"
                                Text="MPSCSC Storage Charges Bill"></asp:Label></td>
                    </tr>
                    <tr>
                        <td colspan="4" style="height: 5px"></td>
                    </tr>

                    <tr id="trJVSGodownRent" visible="false" runat="server">
                        <td>
                            <fieldset style="width: 980px; border: 1px solid navy;">
                                <center>
                                    <div style="overflow: scroll; height: 180px; overflow-x: hidden">
                                        <table cellpadding="0" cellspacing="0" style="width: 100%">

                                            <tr align="center">
                                                <td align="center" colspan="4">
                                                    <asp:Label ID="Label2" runat="server" Text="Godown Type" Font-Bold="True" Font-Size="8pt"
                                                        ForeColor="Navy"></asp:Label>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
       <asp:DropDownList ID="ddlGodownType" runat="server" Width="200px" Height="25px"
           AutoPostBack="True"
           OnSelectedIndexChanged="ddlGodownType_SelectedIndexChanged">
           <asp:ListItem Value="0">--Select--</asp:ListItem>
           <asp:ListItem Value="1" Selected="True">MPWLC Godowns</asp:ListItem>
           <asp:ListItem Value="2">JVS Godowns</asp:ListItem>
           <asp:ListItem Value="3">Hired Godowns</asp:ListItem>
           <asp:ListItem Value="4">Silo Bags</asp:ListItem>
           <asp:ListItem Value="5">MPWLC Owned Cap</asp:ListItem>
           <asp:ListItem Value="6">Tribal Scheme</asp:ListItem>
           <asp:ListItem Value="7">CAP-PMS</asp:ListItem>
           <asp:ListItem Value="8">BOT</asp:ListItem>
       </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" style="height: 5px"></td>
                                            </tr>
                                            <tr>
                                                <td colspan="6" align="center" style="background-color: #0bb6e6; height: 25px">
                                                    <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Size="15pt" ForeColor="WhiteSmoke"
                                                        Text=""></asp:Label>
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
                                                    <asp:DropDownList ID="ddlgodown" runat="server" AutoPostBack="True" TabIndex="1"
                                                        Height="25px" Width="200px" CssClass="tb6" Font-Size="10pt" OnSelectedIndexChanged="ddlgodown_SelectedIndexChanged">
                                                    </asp:DropDownList></td>
                                                <td>
                                                    <asp:Label ID="lblcommodity" runat="server" Text="Commodity Name" Font-Bold="True" Font-Size="8pt"
                                                        ForeColor="Navy"></asp:Label></td>
                                                <td>
                                                    <asp:DropDownList ID="ddlcomodity" runat="server" Width="200px" Height="25px"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlcomodity_SelectedIndexChanged">
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
                                                    <asp:Label ID="lblmonth" runat="server" Text="Month" Font-Bold="True" Font-Size="8pt"
                                                        ForeColor="Navy"></asp:Label></td>
                                                <td>
                                                    <asp:DropDownList ID="ddlFYearNew" runat="server" AutoPostBack="True"
                                                        TabIndex="1" Height="25px" Width="100px" Font-Size="10pt">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="ddlmonth" runat="server" AutoPostBack="True"
                                                        TabIndex="1" Height="25px" Width="100px" Font-Size="10pt" OnSelectedIndexChanged="ddlmonth_SelectedIndexChanged">
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
                                                    <asp:TextBox runat="server" ID="txtcomrate" Visible="true" ReadOnly="false" Text=""
                                                        AutoPostBack="true" BackColor="white"
                                                        TabIndex="9" CssClass="tb6" Width="90px" Height="20px" OnTextChanged="txtcomrate_TextChanged"></asp:TextBox>
                                                    <asp:TextBox runat="server" ID="txtCPRate" Visible="true" ReadOnly="false" AutoPostBack="true" BackColor="white" Text=""
                                                        TabIndex="9" CssClass="tb6" Width="90px" Height="20px"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="txtcomrate"
                                                        ValidChars="0123456789.">
                                                    </cc1:FilteredTextBoxExtender>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtCPRate"
                                                        ValidChars="0123456789.">
                                                    </cc1:FilteredTextBoxExtender>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblFyear" runat="server" Text="Financial Year" Font-Bold="True" Font-Size="8pt"
                                                        ForeColor="Navy"></asp:Label></td>
                                                <td>
                                                    <asp:DropDownList ID="ddlFyear" runat="server" AutoPostBack="True" Enabled="true"
                                                        TabIndex="1" Height="25px" Width="200px" Font-Size="10pt">
                                                    </asp:DropDownList>
                                                </td>

                                            </tr>
                                            <tr>
                                                <td colspan="4" style="height: 5px"></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblstax" Visible="true" runat="server" Text="GST %" Font-Bold="True" Font-Size="8pt"
                                                        ForeColor="Navy"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtstax" Visible="true" ReadOnly="false" BackColor="white" Text="0"
                                                        TabIndex="9" Width="190px" Height="20px"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" TargetControlID="txtstax"
                                                        ValidChars="0123456789.">
                                                    </cc1:FilteredTextBoxExtender>
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
                                                    CssClass="BTNBLUE" OnClick="brnCancel_Click" />
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
                                                                            Text="Storage Bill Detail"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="6" align="center">
                                                                        <asp:GridView ID="gvIStorageCharge" runat="server" AutoGenerateColumns="false">
                                                                            <Columns>
                                                                                <asp:BoundField HeaderText="Date" DataFormatString="{0:dd/MM/yyyy}" DataField="Deposit_Date" />
                                                                                <asp:BoundField HeaderText="Opening Balance" DataField="Opening_Balance" />
                                                                                <asp:BoundField HeaderText="Receive Bags" DataField="Receive_Bags" />
                                                                                <asp:BoundField HeaderText="Issue Bags" DataField="Issue_Bags" />
                                                                                <asp:BoundField HeaderText="Closing Bag Balance" DataField="Closing_Balance" />
                                                                                <asp:BoundField HeaderText="Per Day Rate(For Bag)" DataField="Per_Day_Rate" />
                                                                                <asp:BoundField HeaderText="Per Day Charges" DataField="Charges" />

                                                                                <asp:BoundField HeaderText="Godown_Id" DataField="Godown_Id" />
                                                                                <asp:BoundField HeaderText="Opening Weight" DataField="Opening_Weight" />
                                                                                <asp:BoundField HeaderText="Receive Weight" DataField="Receive_Weight" />
                                                                                <asp:BoundField HeaderText="Issue Weight" DataField="Issue_Weight" />
                                                                                <asp:BoundField HeaderText="Closing Weight Balance" DataField="Closing_Weight" />
                                                                                <asp:BoundField HeaderText="Per Day Rate(For MT)" DataField="Per_Day_Rate_Weight" />
                                                                                <asp:BoundField HeaderText="Per Day Charges" DataField="Charges_Weight" />
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>


                                                            </table>

                                                        </div>
                                                        <table width="100%">
                                                            <tr>
                                                                <td align="center" colspan="2">


                                                                    <asp:Button ID="btnGenBill" runat="server" Text="Stock Verification for Final Bill" Visible="false"
                                                                        CssClass="BTNBLUE" OnClick="btnGenBill_Click" />
                                                                    <asp:Button ID="btncancel2" runat="server" Text="Cancel" Visible="false" Width="110px"
                                                                        CssClass="BTNBLUE" OnClick="btncancel2_Click" />
                                                                    <asp:Button ID="Button1" runat="server" Text="New Verification" Visible="false" Width="110px"
                                                                        CssClass="BTNBLUE" OnClick="Button1_Click" />
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

