<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Region_Master.master" AutoEventWireup="true" CodeFile="Delete_Bill_DSign_For_Steel_Silo_Test.aspx.cs" Inherits="Region_Delete_Bill_DSign_For_Steel_Silo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset style="width: 1000px; border: 2px solid navy;">
        <center>
            <div>
                <table cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td colspan="6" align="center" valign="top">
                            <fieldset style="width: 1100px; border: 1px solid navy;">
                                <center>
                                    <div>
                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr style="background-color: #0bb6e6; height: 25px">
                                                <td colspan="4" align="center">
                                                    <span style="color: White; font-size: 12pt; font-weight: bold">Delete Steel Silo Bill's Details Before DSC</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 10px" colspan="4"></td>
                                            </tr>
                                            <tr>
                                                <td align="left" colspan="4">
                                                    <span style="color: Navy; font-size: 10pt; font-weight: bold">Note :- Record Will be
                                                                Deleted on the basis of Bill No.This will remove all records that belongs to selected
                                                                Bill No.</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" colspan="4">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td style="height: 10px" colspan="4" align="center">&nbsp;</td>
                                            </tr>
                                            <tr align="center">
                                                <td align="center" colspan="6">
                                                    <asp:Label ID="Label1" runat="server" Text="Bill Type" Font-Bold="True" Font-Size="8pt"
                                                        ForeColor="Navy"></asp:Label>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                       <asp:DropDownList ID="ddlBillType" runat="server" Width="200px" Height="25px" AutoPostBack="false">
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
                                                <td style="height: 10px" colspan="4" align="center">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td style="height: 10px" colspan="4" align="center">&nbsp;</td>
                                            </tr>

                                            <tr>
                                                <td style="width: 100px" align="left">
                                                    <asp:Label ID="Label2" runat="server" Text="Enter Bill No." Font-Size="10pt" Font-Bold="true" Visible="true"></asp:Label>
                                                </td>
                                                <td style="width: 400px" align="left">
                                                    <asp:TextBox ID="txtbillnumber" class="text" runat="server" Style="width: 250px; height: 22px;"></asp:TextBox>&nbsp;
                                                            <asp:Button ID="btnSerachWHR" runat="server" Text="Search" Width="100px" CssClass="BTNBLUE"
                                                                CausesValidation="false" OnClick="btnSerachWHR_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 10px" colspan="4"></td>
                                            </tr>
                                        </table>
                                    </div>
                                </center>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 5px" colspan="4"></td>
                    </tr>
                    <tr>
                        <td colspan="4" align="right">
                            <asp:Label ID="lblRowCount" runat="server" ForeColor="navy" Text="Total Record "
                                Font-Size="10pt" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 5px" colspan="4"></td>
                    </tr>
                    <tr>
                        <td colspan="4" align="center">
                            <asp:Label ID="lbl_notfound" runat="server" Text="" Font-Bold="True" Font-Size="15pt"
                                ForeColor="red" Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="center" valign="top">
                            <asp:Panel ID="panelContainer" runat="server" Height="380px" ScrollBars="Vertical"
                                Width="100%" BorderColor="navy" BorderWidth="1px">
                                <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" Width="100%" Font-Size="10pt"
                                    DataKeyNames="Bill_Number" OnRowCommand="gv_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No." ItemStyle-Width="3%">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                                <asp:HiddenField ID="hdnbillnumber" runat="server" Value='<%# Bind("Bill_Number") %>' />
                                                <asp:HiddenField ID="hdnbilltype" runat="server" Value='<%# Bind("Bill_Category_Type_ID") %>' />
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
                                        <%--<asp:TemplateField HeaderText="">
                                                                <HeaderTemplate>
                                                                    <div style="text-align: center;">
                                                                        Print Bill
                                                                    </div>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <div style="text-align: right;">
                                                                       
                                                                        <asp:LinkButton ID="btnLock" runat="server" CommandArgument='<%# Eval("Bill_Number")%>'
                                                                            CssClass="btn btn-primary" EnableTheming="false" CommandName="Print">Print</asp:LinkButton>
                                                                        <asp:HiddenField ID="hdnBillCategoryID" runat="server" Value='<%# Eval("Bill_Category_Type_ID")%>' />
                                                                    </div>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Remove">
                                            <ItemTemplate>
                                                <asp:Button ID="btnRemove" Text="Remove" runat="server" CommandName="RemoveRow" CssClass="btneditstyle" CommandArgument='<%# Container.DataItemIndex %>' OnClientClick="return confirm('Do you want to Remove this row?');" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <%--<Columns>
                                                <asp:TemplateField HeaderText="Select">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chk_Delete" runat="server" />
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" Font-Names="Arial" Font-Size="12pt" HorizontalAlign="Center"
                                                        Width="40px" />
                                                    <ItemStyle HorizontalAlign="Center" Width="10px" />
                                                    <ControlStyle Width="15px" />
                                                </asp:TemplateField>
                                                 <asp:BoundField DataField="Bill_Number" HeaderText="Bill No.">
                                                    <ItemStyle Width="100px" HorizontalAlign="center" />
                                                    <HeaderStyle Width="100px" />
                                                </asp:BoundField>
                                                 <asp:BoundField DataField="Bill_Type" HeaderText="Bill Type">
                                                    <ItemStyle Width="50px" HorizontalAlign="center" />
                                                    <HeaderStyle Width="50px" />
                                                </asp:BoundField>
                                                 <asp:BoundField DataField="Bill_Name" HeaderText="Bill Name">
                                                    <ItemStyle Width="200px" HorizontalAlign="center" />
                                                    <HeaderStyle Width="200px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Depositor_Name" HeaderText="Depositor Name">
                                                    <ItemStyle Width="150px" HorizontalAlign="center" />
                                                    <HeaderStyle Width="150px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="DateOfBill" HeaderText="Date Of Bill">
                                                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                                                      <HeaderStyle Width="80px" />
                                                </asp:BoundField>
                                             <%--   <asp:BoundField DataField="To_Date" HeaderText="To Date">
                                                    <ItemStyle Width="200px" HorizontalAlign="center"/>
                                                      <HeaderStyle Width="100px" HorizontalAlign="center"/>
                                                </asp:BoundField>
                                             
                                                <asp:BoundField DataField="Net_Amount" HeaderText="Net Amount">
                                                    <ItemStyle HorizontalAlign="center" Width="50px" />
                                                </asp:BoundField>
                                               
                                            </Columns>--%>
                                    <FooterStyle BackColor="#CCCC99" />
                                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                                        Height="20px" Font-Size="10pt" />
                                    <AlternatingRowStyle BackColor="White" />
                                </asp:GridView>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 15px" colspan="4"></td>
                    </tr>
                    <tr>
                        <td align="center" colspan="4">
                          <%--  <asp:Button ID="Btn_Delete" runat="server" Text="Delete Record" Width="100px"
                                CssClass="BTNBLUE" OnClick="Btn_Delete_Click" />--%>
                            <%--<asp:Button ID="btn_Close" runat="server" Text="Close" Width="100px"
                                        CssClass="BTNBLUE" onclick="btn_Close_Click" />--%>
                        </td>
                    </tr>
                </table>
            </div>

        </center>
    </fieldset>
</asp:Content>

