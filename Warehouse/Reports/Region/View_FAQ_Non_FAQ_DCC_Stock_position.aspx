<%@ Page Language="C#" MasterPageFile="~/MasterPage/Region_Master.master" AutoEventWireup="true" CodeFile="View_FAQ_Non_FAQ_DCC_Stock_position.aspx.cs" Inherits="StatePages_View_FAQ_Non_FAQ_DCC_Stock_position" Title="View Pvt Godown Complaint" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        * {
            box-sizing: border-box;
        }

        input[type=text], select, textarea {
            width: 100%;
            padding: 12px;
            border: 1px solid rgb(70, 68, 68);
            border-radius: 4px;
            resize: vertical;
        }

        input[type=email], select, textarea {
            width: 100%;
            padding: 12px;
            border: 1px solid rgb(70, 68, 68);
            border-radius: 4px;
            resize: vertical;
        }

        label {
            padding: 12px 12px 12px 0;
            display: inline-block;
        }

        input[type=submit] {
            background-color: rgb(37, 116, 161);
            color: white;
            padding: 12px 20px;
            border: none;
            border-radius: 4px;
            cursor: pointer;
            float: right;
        }

            input[type=submit]:hover {
                background-color: #45a049;
            }

        .container {
            border-radius: 5px;
            background-color: #f2f2f2;
            padding: 20px;
        }

        .col-25 {
            float: left;
            width: 25%;
            margin-top: 6px;
        }

        .col-75 {
            float: left;
            width: 75%;
            margin-top: 6px;
        }

        /* Clear floats after the columns */
        .row:after {
            content: "";
            display: table;
            clear: both;
        }

        /* Responsive layout - when the screen is less than 600px wide, make the two columns stack on top of each other instead of next to each other */
    </style>
    <fieldset style="border: 2px solid navy;">
        <center>
            <div style="text-align: center; font-size: large;">
                <h2 class="header">District wise FAQ, Non-FAQ and DCC Stock Entry by BM &nbsp;&nbsp;<h3><asp:Label ID="lbldate" runat="server"></asp:Label></h3> </h2>
            </div>
            <div>
                <table style="border: solid 5px #e3e3e8; width: 100%; vertical-align: central;">
                    <tr>
                        <%-- <td>
                            <asp:Label ID="Label1" runat="server" Text="Region : "></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlregion" Width="205px" Height="30px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlregion_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>--%>
                        <td  align="right">
                            <asp:Label ID="Label5" runat="server" Text="Select Commodity : "></asp:Label>
                        </td>
                        <td  align="left">
                            <asp:DropDownList ID="ddlCommodity" runat="server" Width="205px" Height="50px" AutoPostBack="true" OnSelectedIndexChanged="ddlCommodity_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>

                    </tr>
                </table>

               
                <div>
                    <asp:GridView runat="server" ID="GrdOfficerPreviousInsp"
                        AutoGenerateColumns="false" CssClass="Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr" ShowFooter="true">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No.">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1%>
                                </ItemTemplate>
                                <ItemStyle Width="1%" />
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="District Name">
                                    <itemtemplate>
                                        <asp:HyperLink ID="sdffsft" runat="server" Target="_blank" NavigateUrl='<%#"~/Reports/Region/Pending_FAQ_Non_FAQ_DCC_Stock_position.aspx?DistrictId="+ (Eval("District_Id").ToString())+"&Commodity_Id=" +(Eval("Commodity_Id").ToString())%>'
                                            title="District Name" Text=' <%# Eval("District_Name") %>' ForeColor="Blue">                                                       
                                        </asp:HyperLink>
                                    </itemtemplate>
                                    <itemstyle horizontalalign="Left"></itemstyle>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Commodity" HeaderText="Commodity" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="OnlineStockPosition" HeaderText="Online Stock Position" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="FAQStockEntryByBM" HeaderText="FAQ Stock Entry By BM" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="NonFAQStockEntryByBM" HeaderText="Non FAQ Stock Entry By BM" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="DCCStockEntryByBM" HeaderText="DCC Stock Entry By BM" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="PendingEntryatBM" HeaderText="Pending Entry at BM" ItemStyle-HorizontalAlign="Right" />
                        </Columns>
                        <%--<FooterStyle BackColor="#719cb6" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" />--%>
                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" Height="20px" Font-Size="10pt" />
                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#F7ECDD" Font-Bold="True" ForeColor="#cb4e48" HorizontalAlign="center"
                            Height="20px" Font-Size="10pt" />
                        <AlternatingRowStyle BackColor="#eeeeee" />
                    </asp:GridView>
                </div>
            </div>
        </center>
    </fieldset>
    &nbsp;
</asp:Content>
