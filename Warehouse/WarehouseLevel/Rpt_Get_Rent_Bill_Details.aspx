<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/PrivateWarehouse.master" AutoEventWireup="true" CodeFile="Rpt_Get_Rent_Bill_Details.aspx.cs" Inherits="WarehouseLevel_Rpt_Get_Rent_Bill_Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellpadding="0" cellspacing="0" style="width: 100%">

        <tr id="Stockdetails" runat="server" visible="true">
            <td colspan="6" align="left" valign="top">
                <center>
                    <div>
                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td style="height: 10px"></td>
                            </tr>
                            <tr>
                                <td valign="top" align="center">
                                    <div style="overflow: scroll; height: 800px; overflow-x: hidden">
                                        <asp:GridView ID="gdstackdetail" runat="server" CellPadding="2" TabIndex="10" Width="100%"
                                            ForeColor="Navy" AutoGenerateColumns="False" ShowFooter="true">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No." ItemStyle-Width="3%">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Godown_Id" HeaderText="Godown Id">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Godown_Name" HeaderText="Godown Name">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Rent_Bill_No" HeaderText="Rent Bill No">
                                                    <ItemStyle HorizontalAlign="Left" Width="100px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Bill_Number" HeaderText="Storage Bill Number">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Net_Amount" HeaderText="Amount">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Crop_Year" HeaderText="Crop Year">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Financial_Year" HeaderText="Financial Year">
                                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Bill_Month" HeaderText="Bill Month">
                                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="DSC_BY_BM_GR" HeaderText="DSC BY BM GR">
                                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="DSC_BY_MG_GR" HeaderText="DSC BY MG GR">
                                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="RC_Dituction" HeaderText="RC Dituction">
                                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity Name">
                                                    <ItemStyle HorizontalAlign="Left" Width="100px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CREDIT_AMOUNT" HeaderText="Credit Amount">
                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                </asp:BoundField>
                                                <%--  <asp:BoundField DataField="UTR_NUMBER" HeaderText="UTR Number">
                                                    <ItemStyle HorizontalAlign="Left" Width="100px" />
                                                </asp:BoundField>--%>
                                                <asp:BoundField DataField="TRANSACTION_DATE" HeaderText="Transaction Date">
                                                    <ItemStyle HorizontalAlign="Left" Width="100px" />
                                                </asp:BoundField>
                                            </Columns>
                                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                            <RowStyle BackColor="#FFFBD6" ForeColor="#333333" BorderColor="#FFC080" HorizontalAlign="Center" />
                                            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                                            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" BorderColor="White" />
                                            <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                                                Height="20px" Font-Size="10pt" />
                                            <AlternatingRowStyle BackColor="White" />
                                        </asp:GridView>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </center>
            </td>
        </tr>
        <tr>
            <td valign="top" align="center">
                <asp:Label ID="lblnotfound" runat="server" Text="" ForeColor="red" Font-Bold="true"
                    Font-Size="12pt" Visible="false"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="height: 5px" colspan="6"></td>
        </tr>


    </table>
</asp:Content>


