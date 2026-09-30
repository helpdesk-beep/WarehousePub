<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="Advertisement.aspx.cs" Inherits="Advertisement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="Server">
     <style>
        .btnMargin {
            margin-bottom: 10px !important;
        }
    </style>
    <style>
        .Grid {
            background-color: #fff;
            margin: 5px 0 10px 0;
            border: solid 1px #525252;
            border-collapse: collapse;
            font-family: Calibri;
            color: #474747;
            width: 100%;
        }

            .Grid td {
                padding: 2px;
                border: solid 1px #c1c1c1;
            }

            .Grid th {
                padding: 4px 2px;
                color: #fff;
                background: #00aad2 url(Images/grid-header.png) repeat-x top;
                border-left: solid 1px #525252;
                font-size: 15px;
                text-align: center;
            }

            .Grid .alt {
                background: #fcfcfc url(Images/grid-alt.png) repeat-x top;
            }

            .Grid .pgr {
                background: #00aad2 url(Images/grid-pgr.png) repeat-x top;
            }

                .Grid .pgr table {
                    margin: 3px 0;
                }

                .Grid .pgr td {
                    border-width: 0;
                    padding: 0 6px;
                    border-left: solid 1px #666;
                    font-weight: bold;
                    color: #fff;
                    line-height: 12px;
                }

                .Grid .pgr a {
                    color: Gray;
                    text-decoration: none;
                }

                    .Grid .pgr a:hover {
                        color: #000;
                        text-decoration: none;
                    }
    </style>
   <br />
    <asp:Label ID="lblErr" runat="server"></asp:Label>
    <div>
        <table style="width: 100%;">
            <tr>
                <td colspan="2">
                    <asp:Label ID="Label1" runat="server" Font-Bold="true"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style1" style="float: right;">Advertiesment:</td>
                <td class="style2">
                    <asp:DropDownList ID="ddlDocType" runat="server" Width="180px" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlDocType_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rvDocType" runat="server"
                        ControlToValidate="ddlDocType" ErrorMessage="Please Select Document Type" ForeColor="#CC3300"
                        ValidationGroup="myValidator"></asp:RequiredFieldValidator>
                </td>
                <td></td>
            </tr>
        </table>
        <br />
        <div>
        <asp:GridView ID="gvHindi" runat="server" AutoGenerateColumns="False"
            CssClass="Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
            OnPageIndexChanging="gvHindi_PageIndexChanging" AllowPaging="true" PageSize="30">
            
            <Columns>
                <asp:TemplateField HeaderText="S.No.">
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                        <asp:HiddenField ID="hdnID" runat="server" Value='<%#Eval("id") %>'></asp:HiddenField>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Title" ControlStyle-Width="500px">
                    <ItemTemplate>
                        <asp:Label ID="lbl_TitleHn" runat="server" Text='<%#Eval("title") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Release Date">
                    <ItemTemplate>
                        <asp:Label ID="releaseDate" runat="server" Text='<%#Eval("releaseDate","{0:d}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Expire Date">
                    <ItemTemplate>
                        <asp:Label ID="expireDate" runat="server" Text='<%#Eval("expireDate","{0:d}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>


                <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <a href='<%#Eval("viewfile") %>' target="_blank"><%#Eval("viewfileName") %></a>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
          <%--  <HeaderStyle CssClass="alert-warning" />
            <AlternatingRowStyle CssClass=" alert-danger" />--%>
        </asp:GridView>
     
        </div>
    </div>
</asp:Content>

