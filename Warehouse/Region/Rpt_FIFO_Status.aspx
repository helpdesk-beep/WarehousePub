<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Region_Master.master" AutoEventWireup="true" CodeFile="Rpt_FIFO_Status.aspx.cs" Inherits="Reports_Region_Rpt_FIFO_Status" %>

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

        .button3 {
            background-color: white;
            color: black;
            border: 2px solid #f44336;
        }

            .button3:hover {
                background-color: #f44336;
                color: white;
            }

        .button6 {
            background-color: white;
            color: black;
            border: 2px solid #008CBA;
        }

            .button6:hover {
                background-color: #008CBA;
                color: white;
            }

        .style3 {
            width: 200px;
            height: 11px;
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
        $('form').live("submit", function () {
            ShowProgress();
        });
    </script>

    <fieldset style="width: 1100px; border: 1px solid navy; box-shadow: 1px 2px 8px; border-radius: 10px 10px 10px 10px; padding-left: 0px; margin-left: 15px">
        <center>
            <%--     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>--%>
            <div style="background-color: white">
                <table cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td colspan="4" align="center" valign="top">
                            <fieldset style="width: 1000px; border: 1px solid navy;">
                                <center>
                                    <div>
                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr style="background-color: #0bb6e6; height: 25px">
                                                <td colspan="4" align="center">
                                                    <asp:Label ID="lblDepositDetail" runat="server" Text="FIFO पद्धति से किये गए भुगतान की जानकारी" Font-Size="17px"
                                                        Font-Bold="true" ForeColor="whitesmoke"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" style="height: 5px"></td>
                                            </tr>
                                            <tr>
                                               <%-- <td style="width: 100px" align="left">
                                                    <asp:Label ID="Label3" runat="server" Text="Month" Font-Size="10pt" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td style="width: 200px" align="left">
                                                    <asp:DropDownList ID="ddlmonth" runat="server" Height="25px" Width="200px" AutoPostBack="True"
                                                        CssClass="tb6" OnSelectedIndexChanged="ddlmonth_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>--%>
                                                <td style="width: 100px" align="left">
                                                    <asp:Label ID="Label2" runat="server" Text="Region" Font-Size="10pt" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td style="width: 200px" align="left">
                                                    <asp:DropDownList ID="ddlregion" runat="server" Height="25px" Width="200px" AutoPostBack="false"
                                                        CssClass="tb6">
                                                    </asp:DropDownList>
                                                </td>
                                           
                                               
                                           
                                                <td style="width: 100px" align="left">
                                                    <asp:Label ID="lblDistrict" runat="server" Text="District" Font-Size="10pt" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td style="width: 200px" align="left">
                                                    <asp:DropDownList ID="ddlDistrict" runat="server" Height="25px" Width="200px" AutoPostBack="True"
                                                        CssClass="tb6" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                 </tr>
                                            <tr>
                                                <td style="width: 100px" align="left">
                                                    <asp:Label ID="lblBranch" runat="server" Text="Branch" Font-Size="10pt" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td style="width: 200px" align="left">
                                                    <asp:DropDownList ID="ddlDepotList" runat="server" Height="25px" Width="200px" AutoPostBack="true"
                                                        CssClass="tb6" OnSelectedIndexChanged="ddlDepotList_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="width: 100px" align="left">
                                                    <asp:Label ID="Label1" runat="server" Text="Godown" Font-Size="10pt" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td style="width: 200px" align="left">
                                                    <asp:DropDownList ID="ddlgodown" runat="server" Height="25px" Width="200px" AutoPostBack="true"
                                                        CssClass="tb6" OnSelectedIndexChanged="ddlgodown_SelectedIndexChanged" >
                                                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" style="height: 5px"></td>
                                            </tr>
                                        </table>
                                    </div>
                                </center>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="height: 5px"></td>
                    </tr>
                    <tr id="trnewproc" runat="server" visible="true">
                        <td style="text-align: left;">
                    <asp:GridView ID="gdstackdetail" runat="server" CellPadding="2" TabIndex="10" Width="100%"
                        ForeColor="Navy" AutoGenerateColumns="False" OnRowDataBound="gdstackdetail_RowDataBound" ShowFooter="true">
                        <columns>
                            <asp:TemplateField HeaderText="S.No." ItemStyle-Width="3%">
                                <itemtemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                     <asp:HiddenField runat="server" ID="hdndiffirence" Value='<%# Eval("FIFOFrizwedStock") %>' />
                                </itemtemplate>
                            </asp:TemplateField>
                            <%--<asp:BoundField DataField="SNo" HeaderText="S.No.">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>--%>
                            <asp:BoundField DataField="Depositor_Name" HeaderText="Depositer Name">
                                <itemstyle horizontalalign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CropYear" HeaderText="Crop Year">
                                <itemstyle horizontalalign="Center" width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Godown" HeaderText="Godown">
                                <itemstyle horizontalalign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity Name">
                                <itemstyle horizontalalign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Depositor_whr_id" HeaderText="Whr No">
                                <itemstyle horizontalalign="Left" width="100px" />
                            </asp:BoundField>

                            <asp:BoundField DataField="Qty" HeaderText="Freeze Qty. for FIFO">
                                <itemstyle horizontalalign="Right" />
                            </asp:BoundField>
                             <asp:BoundField DataField="FIFOFrizwedStock" HeaderText="Delivered Qty.">
                                <itemstyle horizontalalign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="AvailQty" HeaderText="Real Time Balance Qty.">
                                <itemstyle horizontalalign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Godown_ID" HeaderText="Godown ID">
                                <itemstyle horizontalalign="Left" />
                            </asp:BoundField>

                            <asp:BoundField DataField="WHR_Issue_Date" HeaderText="WHR Date" />
                        </columns>
                        <footerstyle backcolor="#5D7B9D" font-bold="True" forecolor="White" />
                        <rowstyle backcolor="#FFFBD6" forecolor="#333333" bordercolor="#FFC080" horizontalalign="Center" />
                        <selectedrowstyle backcolor="#FFCC66" font-bold="True" forecolor="Navy" />
                        <pagerstyle backcolor="#FFCC66" forecolor="#333333" horizontalalign="Center" bordercolor="White" />
                        <headerstyle backcolor="#719cb6" font-bold="True" forecolor="White" horizontalalign="center"
                            height="20px" font-size="10pt" />
                        <alternatingrowstyle backcolor="White" />
                    </asp:GridView>
                </td>
                    </tr>



                </table>
            </div>
        </center>
    </fieldset>
</asp:Content>

