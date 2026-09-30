<%@ Page Language="C#" MasterPageFile="~/MasterPage/StateMaster.master" AutoEventWireup="true" CodeFile="View_Godown_Compalint.aspx.cs" Inherits="StatePages_View_Godown_Compalint" Title="View Pvt Godown Complaint" %>

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
            <div>
                <table cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr style="background-color: #066; height: 80px">
                        <td colspan="4" align="center" style="color: red;">
                            <asp:Label ID="Label8" runat="server" Text="Pvt Godown Complaint"
                                ForeColor="White"
                                Font-Size="12pt"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table style="border: solid 5px #e3e3e8; width: 100%; vertical-align: central;">
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="Region : "></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlregion" Width="205px" Height="30px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlregion_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Label ID="Label5" runat="server" Text="District : "></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddldistrict" runat="server" Width="205px" Height="30px" AutoPostBack="true" OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Label ID="Label7" runat="server" Text="Branch : "></asp:Label>
                        </td>
                        <td>
                            <%--<asp:DropDownList ID="ddlbranch" runat="server" AutoPostBack="false" class="form-control">
                        </asp:DropDownList>--%>
                            <asp:DropDownList ID="ddlbranch" runat="server" Width="205px" Height="30px" AutoPostBack="false" >
                            </asp:DropDownList>
                        </td>

                    </tr>
                </table>
             
                <div class="row">
                    <div class="col-25">
                    </div>
                    <div class="col-25">
                       <asp:Button ID="btnshow" runat="server" Text="View Details" CssClass="button button2" OnClick="btnshow_Click" />
                    </div>

                </div>
                <div>
                    <asp:GridView runat="server" ID="GrdOfficerPreviousInsp"
                        AutoGenerateColumns="false" CssClass="Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No.">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1%>
                                </ItemTemplate>
                                <ItemStyle Width="1%" />
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Region">
                                <ItemTemplate>
                                    <asp:Label ID="lblRegionnm" runat="server" Text='<%# Eval("Regionnm") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="District Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblDistrict_Name" runat="server" Text='<%# Eval("District_Name") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Branch">
                                <ItemTemplate>
                                    <asp:Label ID="lblDepotName" runat="server" Text='<%# Eval("DepotName") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Godown Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblGodown_Name" runat="server" Text='<%# Eval("Godown_Name") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date of Complaint">
                                <ItemTemplate>
                                    <asp:Label ID="lblComplaintDate" runat="server" Text='<%# Eval("ComplaintDate") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Godown Complaint">
                                <ItemTemplate>
                                    <asp:Label ID="lblRemark" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
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
