<%@ Page Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="DeleteGatepassBranch.aspx.cs" Inherits="BranchPages_DeleteGatepassBranch"  Title="Delete Delivery Gatepass ::" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style type="text/css">
    .modal
    {
        position: fixed;
        top: 0;
        left: 0;
        background-color: black;
        z-index: 99;
        opacity: 0.8;
        filter: alpha(opacity=80);
        -moz-opacity: 0.8;
        min-height: 100%;
        width: 100%;
    }
    .loading
    {
        font-family: Arial;
        font-size: 10pt;
        border: 5px solid #67CFF5;
        width: 200px;
        height: 100px;
        display: none;
        position: fixed;
        background-color: White;
        z-index: 999;
    }
</style>


<script type="text/javascript" src="<%= ResolveUrl("~/NEW_CSS/js/jquery-1.12.4.js") %>"></script>
<script type="text/javascript">
    function ShowProgress() {
        setTimeout(function () {
            var modal = $('<div />');
            modal.addClass("modal");
            $('body').append(modal);
            var loading = $(".loading");
            loading.show();
            var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
            var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
            loading.css({ top: top, left: left });
        }, 200);
    }
    $(document).on("submit", "form", function () {
        ShowProgress();
    });
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


    <fieldset style="width: 1000px; border: 2px solid navy;">
        <center>
          
                    <div>
                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td colspan="6" align="center" valign="top">
                                    <fieldset style="width: 980px; border: 1px solid navy;">
                                        <center>
                                            <div>
                                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                    <tr style="background-color: #0bb6e6; height: 25px">
                                                        <td colspan="4" align="center">
                                                            <asp:Label ID="lblPendingGatePAssList" runat="server" Font-Bold="True" Font-Size="12pt"
                                                                ForeColor="whitesmoke" Text="Delete Delivery Gate Pass"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4" align="center">
                                                            <asp:Label ID="lblmsg" runat="server" ForeColor="red" Visible="False" Font-Bold="True"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 10px" colspan="4">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 150px" align="left">
                                                            &nbsp;</td>
                                                        <td style="width: 150px" align="left">
                                                            &nbsp;</td>
                                                        <td style="width: 100px" align="left">
                                                            &nbsp;</td>
                                                        <td style="width: 200px" align="left">
                                                            &nbsp;</td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </center>
                                    </fieldset>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 10px" colspan="4">
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" align="center">
                                    <asp:Label ID="lbl_Empty" runat="server" Text="" Font-Bold="true" Font-Size="12pt"
                                        ForeColor="red" Visible="false"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 5px" colspan="4">
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="4">
                                    <asp:Label ID="lblRowCount" runat="server" ForeColor="navy" Font-Bold="true" Text="Total Record "
                                        Font-Size="10pt"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 5px" colspan="4">
                                </td>
                            </tr>
                            <tr>
                                <td align="center" valign="top" colspan="4">
                                    <asp:Panel ID="panelContainer" runat="server" Height="350px" ScrollBars="Vertical"
                                        Width="100%" BorderColor="navy" BorderWidth="1px">
                                        <asp:GridView ID="gv_gatepass" runat="server" AutoGenerateColumns="False" Width="100%"
                                            CellPadding="2" Font-Names="Verdana" Font-Size="8pt" DataKeyNames="GatePass_No"
                                             AllowPaging="True" 
                                            AllowSorting="True" onpageindexchanging="gv_PageIndexChanging"
                                             BackColor="#FFFBD6">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Select">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chk_Delete" runat="server" />
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" Font-Names="Arial" Font-Size="12pt" HorizontalAlign="Center"
                                                        Width="80px" />
                                                    <ItemStyle HorizontalAlign="Center" Width="10px" />
                                                    <ControlStyle Width="15px" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="GatePass_No" HeaderText="GatePass No.">
                                                    <ItemStyle Width="100px" HorizontalAlign="Right" />
                                                    <HeaderStyle Width="60px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity">
                                                    <ItemStyle Width="200px" />
                                                </asp:BoundField>
                                                  <asp:BoundField DataField="NO_of_Bage" HeaderText="No. of Bags">
                                                    <ItemStyle Width="100px" HorizontalAlign="Right" />
                                                    <HeaderStyle Width="60px" />
                                                </asp:BoundField>
                                                  <asp:BoundField DataField="Weight" HeaderText="Quantity(In Qtls)">
                                                    <ItemStyle Width="200px" HorizontalAlign="Right" />
                                                    <HeaderStyle Width="200px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Issue_Date" HeaderText="GatePass Issue Date">
                                                    <ItemStyle Width="200px" />
                                                </asp:BoundField>
                                            </Columns>
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
                                <td style="height: 5px" colspan="4">
                                </td>
                            </tr>
                               <tr>
                                <td align="center" colspan="4">
                                    <asp:Button ID="Btn_Delete" runat="server" Text="Delete Record" Width="100px" OnClick="Btn_Delete_Click"
                                        CssClass="BTNBLUE" />
                                    <asp:Button ID="btn_Close" runat="server" Text="Close" Width="100px" OnClick="btn_Close_Click"
                                        CssClass="BTNBLUE" />
                                </td>
                            </tr>
                        </table>
                    </div>
              
        </center>
    </fieldset>
</asp:Content>

