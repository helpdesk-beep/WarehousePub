<%@ Page Language="C#" MasterPageFile="~/MasterPage/PrivateWarehouse.master" AutoEventWireup="true" CodeFile="DeleteRequestform_PvtW.aspx.cs" Inherits="WarehouseLevel_DeleteRequestform_PvtW" Title="Delete Request" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset style="width: 1000px; border: 2px solid navy;">
        <center>
         
                    <div>
                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr style="background-color: #0bb6e6; height: 25px">
                                <td colspan="4" align="center">
                                    <asp:Label ID="lblhead" runat="server" Text="Delete Request Form" Font-Size="12pt"
                                        ForeColor="whitesmoke" Font-Bold="true"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="height: 10px">
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 150px">
                                    <asp:Label ID="lblopname" runat="server" Font-Size="8pt" ForeColor="navy" Font-Bold="true"
                                        Text="Operator Name"></asp:Label>
                                </td>
                                <td align="left" style="width: 200px">
                                    <asp:TextBox ID="txtopname" runat="server" MaxLength="50" Width="300px" Height="20px"
                                        CssClass="tb6"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic"
                                        ValidationGroup="SaveValid" ErrorMessage="Please Enter Operator Name" SetFocusOnError="True"
                                        ControlToValidate="txtopname">*</asp:RequiredFieldValidator>
                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtopname"
                                        ValidChars="abcdefghijklmnopqrstuvwxyz ">
                                    </asp:FilteredTextBoxExtender>
                                </td>
                                <td align="left" style="width: 150px">
                                    <asp:Label ID="lblopmobile" runat="server" Font-Size="8pt" ForeColor="navy" Font-Bold="true"
                                        Text="Operator Mobile No."></asp:Label>
                                </td>
                                <td align="left" style="width: 200px">
                                    <asp:TextBox ID="txtopmobile" runat="server" MaxLength="12" Width="200px" Height="20px"
                                        CssClass="tb6"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic"
                                        ValidationGroup="SaveValid" ErrorMessage="Please Enter Operator Mobile Number"
                                        SetFocusOnError="True" ControlToValidate="txtopmobile">*</asp:RequiredFieldValidator>
                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtopmobile"
                                        ValidChars="0123456789">
                                    </asp:FilteredTextBoxExtender>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="height: 5px">
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 150px">
                                    <asp:Label ID="lblbmname" runat="server" Font-Size="8pt" ForeColor="navy" Font-Bold="true"
                                        Text="Branch Manager"></asp:Label>
                                </td>
                                <td align="left" style="width: 200px">
                                    <asp:TextBox ID="txtbmname" runat="server" MaxLength="50" Width="300px" Height="20px"
                                        CssClass="tb6"></asp:TextBox>
                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtbmname"
                                        ValidChars="abcdefghijklmnopqrstuvwxyz ">
                                    </asp:FilteredTextBoxExtender>
                                </td>
                                <td align="left" style="width: 150px">
                                    <asp:Label ID="lblbmmob" runat="server" Font-Size="8pt" ForeColor="navy" Font-Bold="true"
                                        Text="B.M Mobile No."></asp:Label>
                                </td>
                                <td align="left" style="width: 200px">
                                    <asp:TextBox ID="txtbmmob" runat="server" MaxLength="12" Width="200px" Height="20px"
                                        CssClass="tb6"></asp:TextBox>
                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtbmmob"
                                        ValidChars="0123456789">
                                    </asp:FilteredTextBoxExtender>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="height: 5px">
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 150px">
                                    <asp:Label ID="Label3" runat="server" Font-Size="8pt" ForeColor="navy" Font-Bold="true"
                                        Text="Delete Request For -"></asp:Label>
                                </td>
                                <td align="left" colspan="3">
                                    <asp:RadioButtonList ID="rbtdellist" runat="server" RepeatDirection="Horizontal"
                                        CellPadding="4" CellSpacing="2" Font-Size="8pt" ForeColor="navy" AutoPostBack="true"
                                        Font-Bold="true" OnSelectedIndexChanged="rbtdellist_SelectedIndexChanged">
                                        <asp:ListItem Value="01">Opening Balance</asp:ListItem>
                                        <asp:ListItem Value="02">Receipt Details</asp:ListItem>
                                        <asp:ListItem Value="03">Depositor (WHR)</asp:ListItem>
                                        <asp:ListItem Value="04">Delivery Gatepass</asp:ListItem>
                                        <asp:ListItem Value="05">Delivery Order</asp:ListItem>
                                        <asp:ListItem Value="06">Stack</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="height: 5px">
                                </td>
                            </tr>
                            <tr id="trgdnlist" runat="server" visible="false">
                                <td align="left">
                                    <asp:Label ID="lblCommodity" runat="server" Text="Commodity" ForeColor="navy" Font-Bold="true"
                                        Font-Size="8pt"></asp:Label>
                                </td>
                                <td align="left" valign="middle">
                                    <asp:DropDownList ID="ddlcommodity" runat="server" Width="305px" Height="25px" TabIndex="4"
                                        CssClass="tb6">
                                    </asp:DropDownList>
                                </td>
                                <td align="left">
                                    <asp:Label ID="lblGodown" runat="server" Font-Bold="True" Font-Size="8pt" ForeColor="Navy"
                                        Text="Godown Name"></asp:Label>
                                </td>
                                <td align="left" valign="middle">
                                    <asp:DropDownList ID="ddl_godown" runat="server" Width="205px" AutoPostBack="True"
                                        CssClass="tb6" Height="25px" OnSelectedIndexChanged="ddl_godown_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="height: 5px">
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="4">
                                    <asp:Label ID="lblRowCount" runat="server" ForeColor="navy" Font-Bold="true" Text=""
                                        Font-Size="10pt"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 5px">
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" align="center" valign="top">
                                    <asp:GridView ID="gv_Opening" runat="server" AutoGenerateColumns="False" Width="100%"
                                        Font-Size="10pt" DataKeyNames="Depositor_WHR_Id">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Select">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chk_Delete" runat="server" />
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Names="Arial" Font-Size="12pt" HorizontalAlign="Center"
                                                    Width="80px" />
                                                <ItemStyle HorizontalAlign="Center" Width="10px" />
                                                <ControlStyle Width="15px" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Depositor_WHR_Id" HeaderText="WHR No.">
                                                <ItemStyle Width="120px" HorizontalAlign="center" />
                                                <HeaderStyle Width="120px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="whrdate" HeaderText="WHR Date">
                                                <ItemStyle Width="100px" HorizontalAlign="center" />
                                                <HeaderStyle Width="60px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Depositor_Name" HeaderText="Depositor">
                                                <ItemStyle Width="100px" HorizontalAlign="Center" />
                                                <HeaderStyle Width="100px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity Name">
                                                <ItemStyle Width="200px" HorizontalAlign="center" />
                                                <HeaderStyle Width="100px" HorizontalAlign="center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Bags" HeaderText="No. of Bags">
                                                <ItemStyle HorizontalAlign="Right" Width="100px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Qty" HeaderText="Quantity (in Qtls.)">
                                                <ItemStyle HorizontalAlign="Right" Width="120px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Godown_Name" HeaderText="Godown Name">
                                                <ItemStyle Width="400px" HorizontalAlign="center" VerticalAlign="Bottom" />
                                            </asp:BoundField>
                                        </Columns>
                                        <FooterStyle BackColor="#CCCC99" />
                                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                                            Height="20px" Font-Size="10pt" />
                                        <AlternatingRowStyle BackColor="White" />
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" align="center" valign="top">
                                    <asp:GridView ID="gv_Receiptdetails" runat="server" AutoGenerateColumns="False" GridLines="both"
                                        DataKeyNames="ArrivalStock_Id" Width="100%" Font-Size="9pt" CellPadding="4">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Select">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chk_Delete" runat="server" />
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Names="Arial" Font-Size="12pt" HorizontalAlign="Center"
                                                    Width="80px" />
                                                <ItemStyle HorizontalAlign="Center" Width="10px" />
                                                <ControlStyle Width="15px" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Challan_No" HeaderText="TC No.">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Truck_No" HeaderText="Truck No.">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Acpt_FCIRO_No" HeaderText="Acceptance No.">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="AcceptDate" HeaderText="Acceptance Date">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DepositDate" HeaderText="Deposit Date">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Bags" HeaderText="Bags">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Weight" HeaderText="Quantity(In QTLS.)">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Receipt_ID" HeaderText="ReceiptId">
                                                <ItemStyle HorizontalAlign="Left" Width="0px" />
                                                <HeaderStyle Width="0px" />
                                            </asp:BoundField>
                                        </Columns>
                                        <FooterStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Transparent" />
                                        <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                                        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                                        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                                        <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                                            Height="20px" Font-Size="10pt" />
                                        <AlternatingRowStyle BackColor="White" />
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" align="center" valign="top">
                                    <asp:GridView ID="gv_whr" runat="server" AutoGenerateColumns="False" Width="100%"
                                        Font-Size="10pt" DataKeyNames="Depositor_WHR_Id">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Select">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chk_Delete" runat="server" />
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Names="Arial" Font-Size="12pt" HorizontalAlign="Center"
                                                    Width="80px" />
                                                <ItemStyle HorizontalAlign="Center" Width="10px" />
                                                <ControlStyle Width="15px" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Depositor_WHR_Id" HeaderText="WHR No.">
                                                <ItemStyle Width="120px" HorizontalAlign="center" />
                                                <HeaderStyle Width="120px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="whrdate" HeaderText="WHR Date">
                                                <ItemStyle Width="100px" HorizontalAlign="center" />
                                                <HeaderStyle Width="60px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Depositor_Name" HeaderText="Depositor">
                                                <ItemStyle Width="100px" HorizontalAlign="Center" />
                                                <HeaderStyle Width="100px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity Name">
                                                <ItemStyle Width="200px" HorizontalAlign="center" />
                                                <HeaderStyle Width="100px" HorizontalAlign="center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Bags" HeaderText="No. of Bags">
                                                <ItemStyle HorizontalAlign="Right" Width="100px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Qty" HeaderText="Quantity (in Qtls.)">
                                                <ItemStyle HorizontalAlign="Right" Width="120px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Godown_Name" HeaderText="Godown Name">
                                                <ItemStyle Width="400px" HorizontalAlign="center" VerticalAlign="Bottom" />
                                            </asp:BoundField>
                                        </Columns>
                                        <FooterStyle BackColor="#CCCC99" />
                                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                                            Height="20px" Font-Size="10pt" />
                                        <AlternatingRowStyle BackColor="White" />
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" align="center" valign="top">
                                    <asp:GridView ID="gv_gatepass" runat="server" AutoGenerateColumns="False" Width="100%"
                                        CellPadding="2" Font-Names="Verdana" Font-Size="8pt" DataKeyNames="GatePass_No"
                                        BackColor="#FFFBD6">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Select">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chk_Delete" runat="server" />
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Names="Arial" Font-Size="12pt" HorizontalAlign="Center"
                                                    Width="80px" />
                                                <ItemStyle HorizontalAlign="Center" Width="10px" />
                                                <ControlStyle Width="15px" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="GatePass_No" HeaderText="GatePass No.">
                                                <ItemStyle Width="100px" HorizontalAlign="Right" />
                                                <HeaderStyle Width="60px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity">
                                                <ItemStyle Width="200px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="NO_of_Bage" HeaderText="No. of Bags">
                                                <ItemStyle Width="100px" HorizontalAlign="Right" />
                                                <HeaderStyle Width="60px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Weight" HeaderText="Quantity(In Qtls)">
                                                <ItemStyle Width="200px" HorizontalAlign="Right" />
                                                <HeaderStyle Width="200px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Issue_Date" HeaderText="GatePass Issue Date">
                                                <ItemStyle Width="200px" />
                                            </asp:BoundField>
                                        </Columns>
                                        <FooterStyle BackColor="#CCCC99" />
                                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                                            Height="20px" Font-Size="10pt" />
                                        <AlternatingRowStyle BackColor="White" />
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" align="center" valign="top">
                                    <asp:GridView ID="gvDeliveryOrder" runat="server" AutoGenerateColumns="False" BackColor="White"
                                        BorderColor="#DEDFDE" BorderStyle="Solid" BorderWidth="1px" CellPadding="4" ForeColor="Black"
                                        Font-Size="10pt" GridLines="Vertical" DataKeyNames="StockDeliveryOrder_Id" Width="100%">
                                        <RowStyle BackColor="#F7F7DE" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Delete">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chk_Delete" runat="server" Enabled="true" />
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Names="Arial" Font-Size="12pt" HorizontalAlign="Center"
                                                    Width="80px" />
                                                <ItemStyle HorizontalAlign="Center" Width="10px" />
                                                <ControlStyle Width="15px" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="StockDeliveryOrder_Id" HeaderText="Delivery Order No.">
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Depositor/Issuer_Name" HeaderText="Depositor Name">
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:BoundField>
                                            <%--<asp:BoundField DataField="Commodity_Name" HeaderText="Commodity">
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:BoundField>--%>
                                            <asp:BoundField DataField="DO_Date" HeaderText="DO_Date">
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="QtyBags" HeaderText="No. of Bags">
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="QtyWeight" HeaderText="Quantity">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                           <%-- <asp:BoundField DataField="GatePassNO" HeaderText="GatePass No.">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>--%>
                                        </Columns>
                                        <FooterStyle BackColor="#CCCC99" />
                                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" BorderColor="#404040"
                                            BorderStyle="Solid" BorderWidth="1px" />
                                        <AlternatingRowStyle BackColor="White" />
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" align="center" valign="top">
                                    <asp:GridView ID="stack_GridView" runat="server" AutoGenerateColumns="False" CellPadding="2"
                                        Width="100%" DataKeyNames="Stack_ID" Font-Size="8pt">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Select">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chk_Delete" runat="server" />
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Names="Arial" Font-Size="12pt" HorizontalAlign="Center"
                                                    Width="80px" />
                                                <ItemStyle HorizontalAlign="Center" Width="10px" />
                                                <ControlStyle Width="15px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="S.N.">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1%>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" Width="20px" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Stack_ID" HeaderText="Stack Id" />
                                            <asp:BoundField DataField="Stack_Name" HeaderText="Stack Name" SortExpression="Stack_Name" />
                                            <asp:BoundField DataField="Category_Name" HeaderText="Category Name" SortExpression="Category_Name" />
                                            <asp:BoundField DataField="StackingStatus" HeaderText="Stacking Status" SortExpression="StackingStatus"
                                                FooterText="Total" />
                                            <asp:TemplateField HeaderText="Maximum Capacity">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Stack_capacity").ToString()%>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblTotal" runat="server" />
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Current_Capacity" HeaderText="Current Stock">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Storage_Type" HeaderText="Storage Type" SortExpression="Storage_Type" />
                                            <asp:BoundField DataField="Hired_type" HeaderText="Hired Type" SortExpression="Hired_type" />
                                            <asp:BoundField DataField="Stack_ID" Visible="False">
                                                <ItemStyle BackColor="White" Font-Size="0pt" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Godown_ID" Visible="False">
                                                <HeaderStyle Font-Size="0pt" />
                                                <ItemStyle Font-Size="0pt" ForeColor="White" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Category_Id" Visible="False">
                                                <HeaderStyle Font-Size="0pt" />
                                                <ItemStyle Font-Size="0pt" ForeColor="White" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Commodity_Id" Visible="False">
                                                <HeaderStyle Font-Size="0pt" />
                                                <ItemStyle Font-Size="0pt" ForeColor="White" />
                                            </asp:BoundField>
                                        </Columns>
                                        <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                                            Height="20px" Font-Size="10pt" />
                                        <AlternatingRowStyle BackColor="#eeeeee" />
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 15px">
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="4">
                                    <asp:Button ID="btnsave" runat="server" Text="Submit" Width="120px" CssClass="BTNBLUE"
                                        ValidationGroup="SaveValid" OnClick="btnsave_Click"/>
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnPrint" runat="server" Text="Print" Width="120px" CssClass="BTNBLUE"
                                        CausesValidation="false" Visible="false" onclick="btnPrint_Click" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                        ShowSummary="False" ValidationGroup="SaveValid" />
                                </td>
                            </tr>
                        </table>
                    </div>
               
        </center>
    </fieldset>
</asp:Content>

