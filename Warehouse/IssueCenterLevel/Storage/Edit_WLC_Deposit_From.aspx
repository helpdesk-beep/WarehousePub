<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Edit_WLC_Deposit_From.aspx.cs"
    Inherits="IssueCenterLevel_Storage_Edit_WLC_Deposit_From" MasterPageFile="~/MasterPage/Gdwn.master"
    Title="Edit WLC Deposte Form" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset style="width: 1000px; border: 2px solid navy;">
        <center>
            <div>
                <table cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td colspan="4" align="center" valign="top">
                            <fieldset style="width: 980px; border: 1px solid navy;">
                                <center>
                                    <div>
                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr style="background-color: #0bb6e6; height: 25px">
                                                <td colspan="4" align="center">
                                                    <asp:Label ID="lblEDitDepositDetail" runat="server" Text="Edit Deposited Record"
                                                        Font-Size="12pt" ForeColor="whitesmoke" Font-Bold="true"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 10px" colspan="4">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" style="width: 250px">
                                                    <asp:Label ID="lblDepositorType" runat="server" Font-Size="8pt" Font-Bold="true"
                                                        ForeColor="navy" Text="Type of Depositor"></asp:Label></td>
                                                <td align="left">
                                                    <asp:DropDownList ID="ddldepositortype" runat="server" AutoPostBack="True" Font-Size="8pt"
                                                        OnSelectedIndexChanged="ddldepositortype_SelectedIndexChanged" TabIndex="1" Height="25px"
                                                        Width="155px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="left" style="width: 250px">
                                                    <asp:Label ID="lblDepositorName" runat="server" Font-Size="8pt" Font-Bold="true"
                                                        ForeColor="navy" Text="Depositor Name"></asp:Label></td>
                                                <td align="left">
                                                    <asp:DropDownList ID="ddlDepositor" runat="server" AutoPostBack="True" Font-Size="8pt"
                                                        OnSelectedIndexChanged="ddlDepositor_SelectedIndexChanged" TabIndex="2" Height="25px"
                                                        Width="155px" ValidationGroup="SaveValid">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 5px" colspan="4">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lblSourceOfDeposit" runat="server" Font-Size="8pt" Font-Bold="true"
                                                        ForeColor="navy" Text="Deposit at Issue Centre From"></asp:Label>
                                                </td>
                                                <td align="left" colspan="3">
                                                    <asp:DropDownList ID="ddlArrival_Source" runat="server" AutoPostBack="True" Font-Size="8pt"
                                                        Height="25px" Width="155px" OnSelectedIndexChanged="ddlArrival_Source_SelectedIndexChanged"
                                                        TabIndex="3" ValidationGroup="SaveValid">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 5px" colspan="4">
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </center>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 10px" colspan="4">
                        </td>
                    </tr>
                     <tr>
                        <td align="center" colspan="4">
                            <asp:Label ID="lbl_msg" runat="server" Text="No Record Found" Font-Bold="true" ForeColor="Red" Font-Size="10pt" Visible="false" ></asp:Label>
                        </td>
                    </tr>
                    <tr id="pnlGrid" runat="server" visible="false">
                        <td colspan="4" align="center" valign="top">
                            <fieldset style="width: 980px; border: 1px solid navy;">
                                <center>
                                    <div>
                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr style="background-color: #0bb6e6; height: 25px">
                                                <td align="center" valign="middle">
                                                    <asp:Label ID="lbl_head" runat="server" Text="" ForeColor="whitesmoke" Font-Bold="true" Font-Size="12pt"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 5px">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" valign="top">
                                                    <asp:GridView ID="GvuEditFromFCI_OTHDepot" runat="server" AutoGenerateColumns="False"
                                                        CellPadding="4" GridLines="both" DataKeyNames="StorageReceipt_Id" AllowPaging="True"
                                                        Width="100%" Font-Size="9pt" AllowSorting="True" PageSize="20" OnPageIndexChanging="GvuEditFromFCI_OTHDepot_PageIndexChanging">
                                                        <Columns>
                                                            <asp:BoundField DataField="Challan_No" HeaderText="TC No." SortExpression="Challan_No">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Truck_No" HeaderText="Truck No." SortExpression="Truck_No">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Acpt_FCIRO_No" HeaderText="FCI RO No." SortExpression="Acpt_FCIRO_No">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Acpt_FCIRO_Date" HeaderText="FCI RO Date" SortExpression="Acpt_FCIRO_Date">
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Qty_Rvd_No_of_Bags" HeaderText="Bags" SortExpression="Qty_Rvd_No_of_Bags">
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Qty_Rvd_Weight" HeaderText="Qty" SortExpression="Qty_Rvd_Weight">
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity" SortExpression="Commodity_Name">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="Edit Record">
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="LinkButton1" Text="Click to Edit" runat="server" CommandName="EditFCI_OTHDepot"
                                                                        ForeColor="Blue" CommandArgument='<%#Eval("StorageReceipt_Id") %>' OnClick="LinkButton1_Click"></asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="center" />
                                                            </asp:TemplateField>
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
                                                <td align="center" valign="top">
                                                    <asp:GridView ID="gvuEditFrom_RailHead" runat="server" AutoGenerateColumns="False"
                                                        GridLines="both" DataKeyNames="StorageReceipt_Id" AllowPaging="True" Width="100%" Font-Size="9pt"
                                                        CellPadding="4" AllowSorting="True" PageSize="20" OnPageIndexChanging="gvuEditFrom_RailHead_PageIndexChanging">
                                                        <Columns>
                                                            <asp:BoundField DataField="Challan_No" HeaderText="TC No." SortExpression="Challan_No">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Truck_No" HeaderText="Truck No." SortExpression="Truck_No">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Qty_Rvd_No_of_Bags" HeaderText="Bags" SortExpression="Qty_Rvd_No_of_Bags">
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Qty_Rvd_Weight" HeaderText="Qty" SortExpression="Qty_Rvd_Weight">
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity" SortExpression="Commodity_Name">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="Edit Record">
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    &nbsp;<asp:LinkButton ID="lnkRailHead" Text="Click to Edit" runat="server" CommandName="EditRailHead"
                                                                        ForeColor="Blue" CommandArgument='<%#Eval("StorageReceipt_Id") %>' OnClick="lnkRailHead_Click"></asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="center" />
                                                            </asp:TemplateField>
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
                                                <td align="center" valign="top">
                                                    <asp:GridView ID="GvuEditDispatchFromPC" runat="server" AutoGenerateColumns="False"
                                                        GridLines="both" DataKeyNames="ArrivalStock_Id" AllowPaging="True" Width="100%" Font-Size="9pt"
                                                        CellPadding="4" PageSize="20" OnPageIndexChanging="GvuEditDispatchFromPC_PageIndexChanging">
                                                        <Columns>
                                                            <asp:BoundField DataField="Challan_No" HeaderText="TC No." >
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Truck_No" HeaderText="Truck No." >
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Acpt_FCIRO_No" HeaderText="Acceptance No." >
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="AcceptDate" HeaderText="Acceptance Date" >
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Bags" HeaderText="Bags" >
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Weight" HeaderText="Quantity(In QTLS.)" >
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity" >
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="Edit Record" ItemStyle-HorizontalAlign="Right">
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                   <asp:LinkButton ID="lnkDepositProc" Text="Click to Edit" runat="server" CommandName="EditProc"
                                                                        CommandArgument='<%#Eval("ArrivalStock_Id") %>' OnClick="lnkDepositProc_Click"
                                                                        ForeColor="Blue"></asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="center" />
                                                            </asp:TemplateField>
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
                                                <td align="center" valign="top">
                                                    <asp:GridView ID="gvuNonMPSCSC" runat="server" AutoGenerateColumns="False" GridLines="both"
                                                        DataKeyNames="StorageReceipt_Id,ArrivalSource_ID" AllowPaging="True" Width="100%" Font-Size="9pt"
                                                        CellPadding="4" PageSize="20" OnPageIndexChanging="gvuNonMPSCSC_PageIndexChanging"
                                                        TabIndex="4">
                                                        <Columns>
                                                            <asp:BoundField DataField="Challan_No" HeaderText="TC No." SortExpression="Challan_No">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Truck_No" HeaderText="Truck No." SortExpression="Truck_No">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Qty_Rvd_No_of_Bags" HeaderText="Bags" SortExpression="Qty_Rvd_No_of_Bags">
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Qty_Rvd_Weight" HeaderText="Qty" SortExpression="Qty_Rvd_Weight">
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity" SortExpression="Commodity_Name">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Source_Name" HeaderText="Source of Deposit" SortExpression="Source_Name">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="Edit Record">
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkDepositNonMPSCSC" Text="Click to Edit" runat="server" CommandName="EditNonMPSCSC"
                                                                        CommandArgument='<%#Eval("StorageReceipt_Id")+ "," + Eval("ArrivalSource_ID") %>'
                                                                        ForeColor="Blue" OnClick="lnkDepositNonMPSCSC_Click"></asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="center" />
                                                            </asp:TemplateField>
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
</asp:Content>
