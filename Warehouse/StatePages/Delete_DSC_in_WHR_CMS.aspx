<%@ Page Language="C#" MasterPageFile="~/MasterPage/StateMaster.master" AutoEventWireup="true" CodeFile="Delete_DSC_in_WHR_CMS.aspx.cs" Inherits="StatePages_Delete_DSC_in_WHR_CMS" Title="Delete DSC in WHR CMS" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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



    <fieldset style="width: 100%; border: 2px solid navy; margin-left: 10px; margin-right: 10px">
        <center>
            <div>
                <table cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td align="center" valign="top">

                            <center>
                                <div>
                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                        <tr style="background-color: #0bb6e6; height: 25px">
                                            <td colspan="8" align="center">
                                                <asp:Label ID="lblGodownMaster" runat="server" Text="Delete DSC in WHR CMS " Font-Bold="true"
                                                    Font-Size="12pt" ForeColor="whitesmoke"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 5px" colspan="8"></td>
                                        </tr>


                                        <tr>


                                            <td style="height: 50px;" colspan="8" align="center">&nbsp;&nbsp;&nbsp;&nbsp  WHR No : &nbsp;&nbsp;<asp:TextBox ID="txtwhrno" runat="server" AutoPostBack="false"
                                                Height="25px" Width="168px">
                                            </asp:TextBox>

                                                &nbsp;&nbsp;&nbsp;&nbsp 
                                                <asp:Button ID="Button1" runat="server" Text="Search" Visible="true" Width="100px" ValidationGroup="A"
                                                    CssClass="button button2" OnClick="btnSubmit_Click" />


                                            </td>
                                        </tr>

                                        <tr>
                                            <td colspan="8" valign="top" align="center">
                                                <asp:GridView ID="Depositor_Gridview" runat="server" DataKeyNames="Godown_ID" AutoGenerateColumns="False" Width="100%" BackColor="White"
                                                    BorderColor="#CC9966" BorderStyle="Double" BorderWidth="1px" CellPadding="5" OnRowCommand="Depositor_Gridview_RowCommand"
                                                    CellSpacing="2">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No." ItemStyle-Width="3%">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                                <asp:HiddenField ID="hdngodownid" runat="server" Value='<%# Eval("Godown_id") %>' />
                                                                <asp:HiddenField ID="hdnBranchID" runat="server" Value='<%# Eval("BranchID") %>' />
                                                                <asp:HiddenField ID="hdncommodity_id" runat="server" Value='<%# Eval("commodity_id") %>' />
                                                                <asp:HiddenField ID="hdndepositerid" runat="server" Value='<%# Eval("Depositor_ID") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="District_Name" HeaderText="District Name" />
                                                        <asp:BoundField DataField="DepotName" HeaderText="Branch Name" />
                                                        <asp:TemplateField HeaderText="Godown Name">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGodown_Name" Width="100%" Text='<%# Eval("GodownName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="30%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Whr_No">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblWhr_No" Width="100%" Text='<%# Eval("whr_id")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="30%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Commodity Name">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblCommodity_Name" Width="100%" Text='<%# Eval("Commodity_Name")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="30%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Crop Year">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblCropYear" Width="100%" Text='<%# Eval("CropYear")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="30%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Depositor Name">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblDepositor_Name" Width="100%" Text='<%# Eval("Depositor_Name")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="30%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Bags">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblNo_of_Bags" Width="100%" Text='<%# Eval("No_of_Bags")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="30%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Qty">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblQuantity" Width="100%" Text='<%# Eval("Quantity")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="30%" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Delete">
                                                            <ItemTemplate>
                                                                <asp:Button ID="btnRemove" Text="Remove" runat="server" CommandName="RemoveRow" CssClass="btneditstyle" CommandArgument='<%# Container.DataItemIndex %>' OnClientClick="return confirm('Do you want to Delete this WHR Details?');" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <FooterStyle BackColor="#719cb6" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" />
                                                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
                                                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                    <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
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

