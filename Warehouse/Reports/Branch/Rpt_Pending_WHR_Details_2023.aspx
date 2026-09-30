<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="Rpt_Pending_WHR_Details_2023.aspx.cs" Inherits="Reports_Branch_Rpt_Pending_WHR_Details_2023" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .button {
            background-color: #4CAF50; /* Green */
            border: none;
            color: white;
            padding: 0px 0px;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            font-size: 12px;
            font-weight: bold;
            margin: 4px 2px;
            -webkit-transition-duration: 0.4s; /* Safari */
            transition-duration: 0.4s;
            cursor: pointer;
        }

        .button1 {
            background-color: white;
            color: black;
            border: 2px solid #4CAF50;
        }

            .button1:hover {
                background-color: #4CAF50;
                color: white;
            }

        .button2 {
            background-color: white;
            color: black;
            border: 2px solid #008CBA;
        }

            .button2:hover {
                background-color: #008CBA;
                color: white;
            }
    </style>
    <fieldset style="width: 800px; border: 2px solid navy; margin-left: 10px; margin-right: 10px">
        <center>
            <div>
                <table cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td align="center" valign="top">

                            <center>
                                <div>
                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                        <tr>
                                            <td style="height: 5px" colspan="4"></td>
                                        </tr>

                                        <tr>
                                            <td colspan="4" valign="top" align="center">

                                                <asp:GridView ID="Depositor_Gridview" runat="server" AutoGenerateColumns="False" Width="100%" BackColor="White"
                                                    BorderColor="#CC9966" BorderStyle="Double" BorderWidth="1px" CellPadding="5"
                                                    CellSpacing="2" OnPageIndexChanging="Depositor_Gridview_PageIndexChanging">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No." ItemStyle-Width="3%">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="District_Name" HeaderText="District" SortExpression="District_Name">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Branch" HeaderText="Branch" SortExpression="Branch">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Godown_Name" HeaderText="Godown" SortExpression="Godown_Name">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="DepositerNo" HeaderText="Depositor Form No." SortExpression="DepositerNo">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Acceptance_No" HeaderText="Acceptance No" SortExpression="Acceptance_No">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Acceptance_Date" HeaderText="Acceptance Date" SortExpression="Acceptance_Date">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="TC_Number" HeaderText="TC Number" SortExpression="TC_Number">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Truck_Number" HeaderText="Truck Number" SortExpression="Truck_Number">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Send Bags">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="sendb" BackColor="Transparent" Enabled="false" Font-Bold="true" runat="server" Width="60px" MaxLength="5" onblur="Spc_validatorInt(this)" Text='<%# Eval("Recd_Bags") %>'>0</asp:TextBox>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="80px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Send Qty.">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="sendq" BackColor="Transparent" Enabled="false" Font-Bold="true" runat="server" Width="65px" MaxLength="12" onkeyup="NumericDecimalCheck(this,5)"
                                                                    onblur="Spc_validatornumeric(this)" Text='<%# Eval("Recd_Qty") %>'>0</asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <FooterStyle BackColor="#719cb6" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" />
                                                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
                                                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                    <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center" Wrap="true"
                                                        Height="20px" Font-Size="10pt" />
                                                    <AlternatingRowStyle BackColor="#eeeeee" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </center>

                        </td>
                    </tr>
                    <tr>
                        <td style="height: 5px" colspan="4"></td>
                    </tr>
                </table>
            </div>
        </center>
    </fieldset>
</asp:Content>

