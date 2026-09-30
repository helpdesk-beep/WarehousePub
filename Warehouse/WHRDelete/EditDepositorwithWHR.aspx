<%@ Page Language="C#" MasterPageFile="~/MasterPage/WarehouseApplication.master"
    AutoEventWireup="true" CodeFile="EditDepositorwithWHR.aspx.cs" Inherits="IssueCenterLevel_Storage_EditDepositorwithWHR"
    Title="Delete Opening Balance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <table style="border-collapse: collapse; width: 745px; margin-left: 5px;
            height: 100px;">
            <tr class="HeadingBlue">
                <td colspan="4" style="border-collapse: collapse; border: solid 1px white; height: 20px;
                    text-align: center; background-color: dimgray; font-size: 9pt;" align="center"
                    valign="top">
                    <asp:Label ID="Label3" runat="server" Font-Size="9pt" ForeColor="White" Height="10px"
                        Text="Delete Opening Balance"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="4" style="border-right: white 1px solid; border-top: white 1px solid;
                    border-left: white 1px solid; border-bottom: white 1px solid; border-collapse: collapse;
                    text-align: left; width: 818px; height: 16px;">
                    <span style="font-size: 8pt; color: #009900; font-family: Times New Roman"><span
                        style="font-family: Verdana"><span style="color: #cc0000">
                            <%-- <a href="javascript:window.open('../../SampleQuantity.htm'); ">(Qty.in Qtls.kgsGms)</a> --%>
                        </span></span></span>
                </td>
            </tr>
            <tr>
                <td style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid;
                    width: 115px; border-bottom: white 1px solid; border-collapse: collapse; height: 16px;
                    text-align: left">
                    &nbsp;<asp:Label ID="lblDistrict" runat="server" Text="District" Font-Size="8pt"></asp:Label></td>
                <td style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid;
                    width: 110px; border-bottom: white 1px solid; border-collapse: collapse; height: 16px;
                    text-align: left">
                    <asp:DropDownList ID="ddlDistrict" runat="server" AutoPostBack="True" Font-Size="X-Small"
                        Width="140px" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">
                    </asp:DropDownList></td>
                <td style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid;
                    width: 201px; border-bottom: white 1px solid; border-collapse: collapse; height: 16px;
                    text-align: left">
                    &nbsp;<asp:Label ID="lblBranch" runat="server" Text="Issue Center" Font-Size="8pt"></asp:Label></td>
                <td style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid;
                    width: 369px; border-bottom: white 1px solid; border-collapse: collapse; height: 16px;
                    text-align: left">
                    <asp:DropDownList ID="ddlDepotList" runat="server" AutoPostBack="True" Font-Size="X-Small"
                        Width="140px" OnSelectedIndexChanged="ddlDepotList_SelectedIndexChanged">
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid;
                    width: 115px; border-bottom: white 1px solid; border-collapse: collapse; height: 16px;
                    text-align: left">
                    <asp:Label ID="Label2" runat="server" Text=" According To Created Date" Width="161px"
                        Font-Size="8pt"></asp:Label></td>
                <td style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid;
                    width: 110px; border-bottom: white 1px solid; border-collapse: collapse; height: 16px;
                    text-align: left">
                    <asp:DropDownList ID="ddlDateWise" runat="server" AutoPostBack="True" Font-Size="X-Small"
                        Width="140px" OnSelectedIndexChanged="ddlDateWise_SelectedIndexChanged">
                    </asp:DropDownList></td>
                <td style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid;
                    border-bottom: white 1px solid; border-collapse: collapse; height: 16px; text-align: left"
                    colspan="2">
                    <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Size="7.5pt" ForeColor="Maroon"
                        Text="Delete record on the basis of WHR No.This will remove all records that belongs to selected WHR No."
                        Width="437px"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="4" style="border-right: white 1px solid; border-top: black 1px solid;
                    border-left: white 1px solid; border-bottom: white 1px solid; border-collapse: collapse;
                    height: 28px; margin: 0px; padding-right: 0px; padding-left: 0px; padding-bottom: 0px;
                    padding-top: 0px;">
                    <asp:Panel ID="panelContainer" runat="server" BorderColor="WhiteSmoke" Height="300px"
                        ScrollBars="Vertical" Width="100%">
                        <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" Width="768px" Height="15px"
                            Font-Size="8pt" OnSelectedIndexChanging="gv_SelectedIndexChanging">
                            <Columns>
                                <asp:CommandField HeaderText="Action" ShowSelectButton="True" />
                                <asp:TemplateField HeaderText="WHRID" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# bind("WHRID") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="CreatDate" HeaderText=" Date">
                                    <ItemStyle Width="100px" HorizontalAlign="Right" />
                                    <HeaderStyle Width="60px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Depositor_Name" HeaderText="Depositor">
                                    <ItemStyle Width="200px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CommdName" HeaderText="Commodity">
                                    <ItemStyle Width="200px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CatName" HeaderText="Category">
                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    <HeaderStyle Width="60px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="WHRNo" HeaderText="WHRNo">
                                    <ItemStyle Width="200px" ForeColor="#0000C0" HorizontalAlign="Right" VerticalAlign="Bottom" />
                                    <HeaderStyle Width="50px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Bags" HeaderText="Bags">
                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Qty" HeaderText="Qty">
                                    <ItemStyle HorizontalAlign="Right" Width="120px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="GodownName" HeaderText="Godwon">
                                    <ItemStyle Width="300px" HorizontalAlign="Right" VerticalAlign="Bottom" />
                                </asp:BoundField>
                                <asp:BoundField DataField="StackName" HeaderText="Stack">
                                    <ItemStyle Width="50px" HorizontalAlign="Right" />
                                    <HeaderStyle Width="50px" />
                                </asp:BoundField>
                            </Columns>
                            <SelectedRowStyle BackColor="#FFE0C0" />
                            <HeaderStyle BackColor="Gray" ForeColor="White" BorderColor="White" />
                            <RowStyle VerticalAlign="Bottom" Font-Size="8pt" />
                            <FooterStyle VerticalAlign="Bottom" />
                        </asp:GridView>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td style="height: 22px">
                    <asp:Label ID="Label5" runat="server" Text="Current Stock In Stack" Width="170px"></asp:Label></td>
                <td style="height: 22px" align="left">
                    <asp:Label ID="lblCrtCapStk" runat="server" Font-Bold="True" ForeColor="Maroon"></asp:Label></td>
                <td style="height: 22px">
                    <asp:Label ID="Label6" runat="server" Text="Available Capacity Of Stack" Width="218px"></asp:Label></td>
                <td style="height: 22px; width: 369px;">
                    <asp:Label ID="lblAvailCapStk" runat="server" Font-Bold="True" ForeColor="Maroon"></asp:Label></td>
            </tr>
            <tr>
                <td style="height: 22px">
                </td>
                <td align="center" style="height: 22px" colspan="2">
                    <asp:ImageButton ID="ImageButton1" runat="server" OnClick="ImageButton1_Click" Height="24px"
                        ImageUrl="~/images/delNew.jpg" Width="75px" /></td>
                <td style="height: 22px; width: 369px;">
                    <asp:ImageButton ID="ImageButton2" runat="server" Height="20px" ImageUrl="~/images/close.jpg"
                        OnClick="ImageButton2_Click" /></td>
            </tr>
            <tr style="color: #000000">
                <td align="center" colspan="4" style="border-collapse: collapse; border: solid 1px white;
                    height: 19px;">
                    <asp:HiddenField ID="HiddenField1" runat="server" />
                    <asp:HiddenField ID="HiddenField2" runat="server" />
                    &nbsp; &nbsp;
                </td>
            </tr>
            <tr class="HeadingBlue">
                <td colspan="4" style="border-collapse: collapse; border: solid 1px white; height: 15px;
                    text-align: center; background-color: dimgray;" align="left">
                    <asp:Label ID="lblRowCount" runat="server" ForeColor="White" Text="Total Record "
                        Font-Size="8.5pt"></asp:Label></td>
            </tr>
        </table>
    </div>
</asp:Content>
