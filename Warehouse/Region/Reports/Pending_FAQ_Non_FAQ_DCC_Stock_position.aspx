<%@ Page Language="C#" MasterPageFile="~/MasterPage/Region_Master.master" AutoEventWireup="true" CodeFile="Pending_FAQ_Non_FAQ_DCC_Stock_position.aspx.cs" Inherits="StatePages_Pending_FAQ_Non_FAQ_DCC_Stock_position" Title="View Pvt Godown Complaint" %>

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
                                
                                <asp:BoundField DataField="DepotName" HeaderText="Branch" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="Godown_Name" HeaderText="Godown_Name" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="Commodity" HeaderText="Commodity" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="CropYear" HeaderText="CropYear" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="Whr_No" HeaderText="Whr_No" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="WHR_Issue_Date" HeaderText="WHR_Issue_Date" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="OnlineStockPosition" HeaderText="Online Stock Position" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="FAQStockEntryByBM" HeaderText="FAQ Stock Entry By BM" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="NonFAQStockEntryByBM" HeaderText="Non FAQ Stock Entry By BM" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="DCCStockEntryByBM" HeaderText="DCC Stock Entry By BM" ItemStyle-HorizontalAlign="Right" />
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
