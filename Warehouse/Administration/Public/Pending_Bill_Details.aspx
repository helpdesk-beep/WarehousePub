<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Administration.master" AutoEventWireup="true" CodeFile="Pending_Bill_Details.aspx.cs" Inherits="Administration_Public_Pending_Bill_Details" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row" style="margin-bottom: 10px;">
        <div class="col-md-12 mg-t-10">
            <div id="div20" runat="server" visible="true" style="padding-top: 1%;">
                <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False" ShowFooter="True" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2">
                    <Columns>
                        <asp:TemplateField HeaderText="S.No.">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1%>
                            </ItemTemplate>
                            <ItemStyle Width="1%" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="GodownID" HeaderText="Godown ID" />
                        <asp:BoundField DataField="Godown" HeaderText="Godown" />
                        <asp:BoundField DataField="Commodity" HeaderText="Commodity" />
                        <asp:BoundField DataField="CropYear" HeaderText="Crop Year" />
                        <asp:BoundField DataField="Month" HeaderText="Month" />
                        <asp:BoundField DataField="Year" HeaderText="Year" />
                    </Columns>
                    <EmptyDataTemplate>
                        <div class="alert alert-danger" style="text-align: center; margin: auto; margin-top: 10px; margin-bottom: 5px; font-size: 10pt; color: Green">
                            WARNING: No Records Found
                        </div>
                    </EmptyDataTemplate>
                    <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                    <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                    <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                    <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                    <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>

