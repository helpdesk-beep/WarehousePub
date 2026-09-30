<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="MRecivingDtl.aspx.cs" Inherits="BranchPages_MRecivingDtl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h3>Mobile द्वारा रिसीव की गई commodity की stacking यहा से करें: </h3>
    <table>
         <tr>
                                                        <td align="left" style="width: 200px" valign="middle">
                                                            <asp:Label ID="lblDepositorName" runat="server" Text="Depositor Name" ForeColor="navy"
                                                                Font-Bold="true" Font-Size="8pt"></asp:Label>
                                                        </td>
                                                        <td align="left" style="width: 200px" valign="bottom">
                                                            <asp:DropDownList ID="ddldepositorname" runat="server" Width="155px" Height="25px"
                                                                TabIndex="1" CssClass="tb6">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="lblWHRDate" runat="server" Text="Deposite Date" Font-Bold="true" Font-Size="8pt"
                                                                ForeColor="navy"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtdepositdate" runat="server" MaxLength="12" Width="150px" 
                                                                CssClass="tb6"></asp:TextBox>
                                                            <asp:ImageButton ID="Imgpop" runat="server" ImageUrl="~/images/cal.gif" CausesValidation="false" />
                                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" TargetControlID="txtdepositdate"
                                                                Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy" PopupButtonID="Imgpop">
                                                            </asp:CalendarExtender>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtdepositdate"
                                                                Display="Dynamic" ErrorMessage="Deposite Date is required" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
          <tr>
                                                        <td align="left">
                                                            <asp:Label ID="lblCommodity" runat="server" Text="Commodity" ForeColor="navy" Font-Bold="true"
                                                                Font-Size="8pt"></asp:Label>
                                                        </td>
                                                        <td align="left" valign="middle">
                                                            <asp:DropDownList ID="ddlcommodity" runat="server" Width="155px" Height="25px" TabIndex="4"
                                                                CssClass="tb6" 
                                   >
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="lblGodown" runat="server" Font-Bold="True" Font-Size="8pt" ForeColor="Navy"
                                                                Text="Godown Name"></asp:Label>
                                                        </td>
                                                        <td align="left" valign="middle">
                                                            <asp:DropDownList ID="ddl_godown" runat="server"
                                                                CssClass="tb6"  Height="25px">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
        <tr>
            <td>

            </td>
            <td>
                <asp:Button ID="btnsearch" runat="server" Text="Submit" OnClick="btnsearch_Click" />
            </td>
        </tr>
    </table>
    <asp:GridView ID="gvrec" runat="server" EnableModelValidation="True" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:BoundField DataField="ChallanNo" HeaderText="Challan No" />
            <asp:BoundField DataField="TruckNo" HeaderText="TruckNo" />
            <asp:BoundField DataField="WCMNO" HeaderText="WCMNO" />
            <asp:BoundField DataField="MOWgt" HeaderText="Mode of Whgt." />
            <asp:BoundField DataField="BagsRec" HeaderText="Bags" />
            <asp:BoundField DataField="QtyRec" HeaderText="Qty" />
            <asp:BoundField DataField="MRid" HeaderText="MRid"/>
            <asp:BoundField DataField="DepositorType" HeaderText="DepositorType"/>
            <asp:BoundField DataField="DepositorID" HeaderText="DepositorID"/>
            <asp:BoundField DataField="DepositFrom" HeaderText="DepositFrom"/>
            <asp:BoundField DataField="ReceiptID" HeaderText="ReceiptID"/>
            <asp:BoundField DataField="category" HeaderText="category"/>
            <asp:BoundField DataField="CropYear" HeaderText="CropYear"/>
            <asp:BoundField DataField="Transpoter" HeaderText="Transpoter"/>
            <asp:BoundField DataField="QtySent" HeaderText="QtySent"/>
            <asp:BoundField DataField="BagsSent" HeaderText="BagsSent"/>
            <asp:BoundField DataField="DateofReceipt" HeaderText="DateofReceipt"/>
            
        </Columns>
        <EditRowStyle BackColor="#999999" />
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
</asp:GridView>
    <table>
        <tr>
<td style="color: navy">
Total Bags:
</td>
<td>
    <asp:Label ID="lblbagstotal" runat="server" Text="0"></asp:Label>
</td>
<td style="color: navy">
Total Qty:
</td>
<td>
    <asp:Label ID="lblqtytotal" runat="server" Text="0"></asp:Label>
</td>
</tr> 
          <tr style="background-color: #0bb6e6; height: 25px">
                                                <td colspan="6" align="center">
                                                    <asp:Label ID="lblDeliveryOrderOfStock" runat="server" Text="Depositing Details"
                                                        ForeColor="whitesmoke" Font-Bold="true" Font-Size="12pt"></asp:Label>
                                                </td>
                                            </tr>
           <tr>
                                                <td align="left" style="width: 150px">
                                                    <asp:Label ID="lblGodownNo" runat="server" Font-Size="8pt" Font-Bold="true" ForeColor="navy"
                                                        Text="Godown No."></asp:Label></td>
                                                <td align="left" style="width: 150px">
                                                    <asp:DropDownList ID="ddlGodownNo" runat="server" AutoPostBack="True" Width="155px"
                                                        Height="25px" TabIndex="18" OnSelectedIndexChanged="ddlGodownNo_SelectedIndexChanged1" 
                                                        >
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="left" style="width: 150px">
                                                    <asp:Label ID="lblStackNo" runat="server" Font-Size="8pt" Font-Bold="true" ForeColor="navy"
                                                        Text="Stack No."></asp:Label></td>
                                                <td align="left" style="width: 150px">
                                                    <asp:DropDownList ID="ddlStackNo" runat="server" Height="25px" Width="155px" AutoPostBack="True"
                                                        TabIndex="19" OnSelectedIndexChanged="ddlStackNo_SelectedIndexChanged" >
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="left" style="width: 150px">
                                                    <asp:Label ID="lblStackBags" runat="server" Font-Size="8pt" Font-Bold="true" ForeColor="navy"
                                                        Text="No.of Bags"></asp:Label></td>
                                                <td align="left" style="width: 150px">
                                                    <asp:TextBox ID="txtStackBags" runat="server" MaxLength="20" Width="150px" onblur="Spc_validator(this)"
                                                        TabIndex="20"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="height: 10px" colspan="6">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" style="width: 150px">
                                                    <asp:Label ID="lblStackWt" runat="server" Font-Size="8pt" Font-Bold="true" ForeColor="navy"
                                                        Text="Weight"></asp:Label></td>
                                                <td align="left" style="width: 150px">
                                                    <asp:TextBox ID="txtStackWt" runat="server" MaxLength="20" Width="150px" onkeyup="NumericDecimalCheck(this,5)"
                                                        onblur="compare()" TabIndex="21"></asp:TextBox></td>
                                                <td align="left">
                                                    <asp:Label ID="lblStackMaxCap" runat="server" Font-Size="8pt" Font-Bold="true" ForeColor="navy"
                                                        Text="Maximum Capacity"></asp:Label></td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtStackMaxCap" runat="server" MaxLength="20" Width="150px" BackColor="#FFFFC0"
                                                        Enabled="False"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    &nbsp;</td>
                                                <td align="left">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td style="height: 10px" colspan="6">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lblStackCurrentCapacity" runat="server" Font-Size="8pt" Font-Bold="true"
                                                        ForeColor="navy" Text="Current Capacity"></asp:Label></td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtStackCurrentCapacity" runat="server" MaxLength="20" Width="150px"
                                                        BackColor="#FFFFC0" Enabled="False"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="lblStackAvailable" runat="server" Font-Size="8pt" Font-Bold="true"
                                                        ForeColor="navy" Text="Available Capacity"></asp:Label></td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtStackAvailable" runat="server" MaxLength="20" Width="150px" BackColor="#FFFFC0"
                                                        Enabled="False"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtArrivalSrcId" runat="server" Width="155px" Enabled="False"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 10px" colspan="6">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="6">
                                                    <asp:Label ID="lblStackingInform" runat="server" Font-Size="8pt" ForeColor="red"
                                                        Font-Bold="true" Text="(Click the Add Stack Button to save the Stacking Information) "></asp:Label>
                                                    <asp:Button ID="btnAddStack" runat="server" TabIndex="22" Text="Add Stack" Width="100px"
                                                        CssClass="BTNBLUE" OnClick="btnAddStack_Click"   /></td>
                                            </tr>
                                             <tr>
                                                <td style="height: 10px" colspan="6">
                                                    <asp:Label ID="lblmsg" runat="server" ForeColor="#FF3300"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="6">
                                                    <asp:GridView ID="gdstackingdetails" runat="server" AutoGenerateDeleteButton="True"
                                                        CellPadding="4" ForeColor="#333333" GridLines="None" OnPreRender="gdstackingdetails_PreRender"
                                                        OnRowCreated="gdstackingdetails_RowCreated" OnRowDeleting="gdstackingdetails_RowDeleting">
                                                        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                                        <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                                                        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                                                        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                                                        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                                        <AlternatingRowStyle BackColor="White" />
                                                    </asp:GridView>
                                                    <asp:GridView ID="gdEditStackingDetails" runat="server" AutoGenerateDeleteButton="True"
                                                        AutoGenerateEditButton="true" CellPadding="4" ForeColor="#333333" GridLines="None"
                                                        OnPreRender="gdEditStackingDetails_PreRender" OnRowCreated="gdEditStackingDetails_RowCreated"
                                                        OnRowDeleting="gdEditStackingDetails_RowDeleting" OnRowEditing="gdEditStackingDetails_RowEditing">
                                                        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                                        <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                                                        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                                                        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                                                        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                                        <AlternatingRowStyle BackColor="White" />
                                                    </asp:GridView>
                                                </td>
                                            </tr>
           <tr><br />
                                                <td colspan="4" align="center">
                                                    <asp:Button ID="btnsave" runat="server" Text="Save Details" Width="120px" CssClass="BTNBLUE"
                                                        TabIndex="24" ValidationGroup="GD_Stack,Non" Enabled="False"  OnClientClick="this.disabled = true; this.value='Please wait...'" UseSubmitBehavior="false" OnClick="btnsave_Click" />
                                                    &nbsp; &nbsp; &nbsp;
                                                    &nbsp; &nbsp; &nbsp;
                                                    <asp:Button ID="btn_Close" runat="server" Text="Close" Width="120px" CssClass="BTNBLUE"
                                                        CausesValidation="false" />
                                                </td>
                                            </tr>

    </table>
</asp:Content>

