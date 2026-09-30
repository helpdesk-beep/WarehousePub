<%@ Page Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="PrintDepositor_Acknowledgement.aspx.cs" Inherits="IssueCenterLevel_Storage_PrintDepositor_Acknowledgement" Title="Depositor Form Acknowledgement::" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <center>
        <div style="width: 1000px">
            <table cellpadding="0" cellspacing="0" style="width: 100%">
                <tr>
                    <td align="left" style="width: 50%">
                        <asp:Label ID="lblwhr" runat="server" Text="Select Depositor No/Depositor Name/Commodity/Quantity/Bags - " ForeColor="Navy" Font-Bold="true"
                            Font-Size="10pt"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="DDLwhr" runat="server" Height="30px" Width="400px" Font-Bold="true" Font-Size="10pt" OnSelectedIndexChanged="DDLwhr_SelectedIndexChanged"
                            AutoPostBack="true">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <rsweb:ReportViewer ID="ReportViewer_Depot" runat="server" Width="850px" ProcessingMode="Remote"
                            Height="600px">
                        </rsweb:ReportViewer>
                    </td>
                </tr>
            </table>
        </div>
    </center>
</asp:Content>


