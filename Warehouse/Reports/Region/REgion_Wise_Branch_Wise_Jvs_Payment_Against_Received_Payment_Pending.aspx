<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Region_Master.master" AutoEventWireup="true" CodeFile="~/Reports/Region/REgion_Wise_Branch_Wise_Jvs_Payment_Against_Received_Payment_Pending.aspx.cs" Inherits="Reports_Region_REgion_Wise_Branch_Wise_Jvs_Payment_Against_Received_Payment_Pending" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title style="color: white;">Region Level Rerport</title>

    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <script src="scripts/jquery-3.3.1.min.js"></script>
    <script src="scripts/bootstrap.min.js"></script>
    <link href="Content/dataTables.bootstrap4.min.css" rel="stylesheet" />
    <link href="../../assets/css/style.css" rel="stylesheet" />
    <script src="scripts/dataTables.bootstrap4.min.js"></script>
    <script src="scripts/jquery.dataTables.min.js"></script>
    <script type="text/javascript">  
        $(document).ready(function () {
            $("#EmployeeGridViewList").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable();
        });
    </script>
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
            Width: 200px;
            height: 50px;
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>--%>
    <div>
        <br />
        <br />
        <table cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td style="padding-top: 20px; padding-bottom: 20px; background-color: skyblue; font-family: 'Times New Roman'; font-size: 20pt; color: white; text-align: center;" colspan="4">District Wise<br />
                    M.P. Warehousing & Logistics Corporarion<br />
                    Branch Wise JVS Payment Status Against Received Payment From MPSCSC(From August 2020)
                </td>
            </tr>
            <tr>
                <td align="center" colspan="4">&nbsp;
                </td>
            </tr>
            <tr>
                <td style="text-align: center;">Received Payment Date from MPSCSC:
                    
                        <asp:TextBox ID="txtdatefrom" runat="server" Height="25px"></asp:TextBox>
                    <asp:CalendarExtender ID="txtdatefrom_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtdatefrom" Format="dd/MM/yyyy">
                    </asp:CalendarExtender>
                    &nbsp;&nbsp;&nbsp;
                    Received Payment Date To MPSCSC:
                        <asp:TextBox ID="txtdateto" runat="server" Height="25px"></asp:TextBox>
                    <asp:CalendarExtender ID="txtdateto_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtdateto" Format="dd/MM/yyyy">
                    </asp:CalendarExtender>
                    &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnDateSearch" runat="server" Text="Search" OnClick="btnDateSearch_Click" />
                </td>
            </tr>
            <tr>
                <td align="center" colspan="4">&nbsp;
                </td>
            </tr>
            <tr>
                <td align="center" colspan="4">OR
                </td>
            </tr>
            <tr>
                <td align="center" colspan="4">&nbsp;
                </td>
            </tr>
            <tr>
                <td style="text-align: center;">UTR Number:
                        <asp:TextBox ID="txtUTRNo" runat="server" onkeypress="return NumberOnly(event);" Height="25px" Width="255px"></asp:TextBox>

                    &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnUTRSearch" runat="server" Text="Search" OnClick="btnUTRSearch_Click" />
                </td>
            </tr>
        </table>
        <table style="border: solid 5px #e3e3e8; width: 100%; vertical-align: central;">
            <tr>
                <td style="text-align: left;">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" ShowFooter="true"
                        OnRowDataBound="GridView1_RowDataBound" OnRowCreated="GridView1_RowCreated" OnDataBound="OnDataBound"
                        CssClass="Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No.">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Regionnm" HeaderText="Region" />
                            <asp:BoundField DataField="District_Name" HeaderText="District" />
                            <asp:BoundField DataField="DepotName" HeaderText="Branch" />
                            <asp:TemplateField HeaderText="No of Bills" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblUTR2BillNo" runat="server" Text='<%# Eval("UTR2BillNo") %>' />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <div style="text-align: right;">
                                        <asp:Label ID="lblTotalqty1" runat="server" Font-Bold="true" />
                                    </div>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="No of Rent Bill Pending For Generated by Godown Owner" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblUTR9RBillNo" runat="server" Text='<%# Eval("UTR9RBillNo") %>' />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <div style="text-align: right;">
                                        <asp:Label ID="lblTotalqty1" runat="server" Font-Bold="true" />
                                    </div>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="No of Rent Bill Amount Pending For Generated by Godown Owner" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblUTR9R_Amount" runat="server" Text='<%# Eval("UTR9R_Amount") %>' />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <div style="text-align: right;">
                                        <asp:Label ID="lblTotalqty2" runat="server" Font-Bold="true" />
                                    </div>
                                </FooterTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle Font-Bold="True" ForeColor="Black" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
    <script type="text/javascript" src="<%= ResolveUrl("~/NEW_CSS/js/jquery-1.12.4.js") %>"></script>
    <script type="text/javascript" src="../../JS/table2excel.js"></script>
    <script type="text/javascript">
        $("body").on("click", "#btnExport", function () {
            $("[id*=GridView1]").table2excel({
                filename: "State_Level_District_Branch_Wise_Rent_Summary.xls"
            });
        });
    </script>
    <script type="text/javascript">
        function NumberOnly(e) {
            var charCode = (e.which) ? e.which : e.keyCode;
            if ((charCode >= 48 && charCode <= 57)) {
                return true;
            }
            if (charCode == 8) { return true; }
            if (charCode == 9) { return true; }
            else { return false; }
        }
    </script>
</asp:Content>

