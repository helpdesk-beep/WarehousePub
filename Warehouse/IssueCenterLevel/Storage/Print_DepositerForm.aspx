<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="Print_DepositerForm.aspx.cs" Inherits="IssueCenterLevel_Storage_Print_DepositerForm" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript" src="../../JS/jquery-1.7.1.min.js"></script>


     <script type="text/javascript">
         $(document).ready(function () {
             if ($.browser.mozilla) {
                 try {
                     var ControlName = 'ContentPlaceHolder1_ReportViewer_Depot';
                     var innerScript = '<scr' + 'ipt type="text/javascript">document.getElementById("' + ControlName + '_print").Controller = new ReportViewerHoverButton("' + ControlName + '_print", false, "", "", "", "#ECE9D8", "#DDEEF7", "#99BBE2", "1px #ECE9D8 Solid", "1px #336699 Solid", "1px #336699 Solid");</scr' + 'ipt>';
                     var innerTbody = '<tbody><tr><td><input type="image" style="border-width: 0px; padding: 2px; height: 16px; width: 16px;" alt="Print" src="/Reserved.ReportViewerWebControl.axd?OpType=Resource&amp;Version=9.0.30729.1&amp;Name=Microsoft.Reporting.WebForms.Icons.Print.gif" title="Print"></td></tr></tbody>';
                     var innerTable = '<table title="Print" onmouseout="this.Controller.OnNormal();" onmouseover="this.Controller.OnHover();" onclick="PrintFunc(\'' + ControlName + '\'); return false;" id="' + ControlName + '_print" style="border: 1px solid rgb(236, 233, 216); background-color: rgb(236, 233, 216); cursor: default;">' + innerScript + innerTbody + '</table>'
                     var outerScript = '<scr' + 'ipt type="text/javascript">document.getElementById("' + ControlName + '_print").Controller.OnNormal();</scr' + 'ipt>';
                     var outerDiv = '<div style="display: inline; font-size: 8pt; height: 30px;" class=" "><table cellspacing="0" cellpadding="0" style="display: inline;"><tbody><tr><td height="28px">' + innerTable + outerScript + '</td></tr></tbody></table></div>';

                     $("#" + ControlName + " > div > div").append(outerDiv);

                 }
                 catch (e) { alert(e); }
             }
         });

         function PrintFunc(ControlName) {
             setTimeout('ReportFrame' + ControlName + '.print();', 100);
         }
     
     </script>

    <table cellpadding="0" cellspacing="0" 
        style="width: 100%; color: #FF5050;" >
                <tr>
                    <td align="left">
                        <asp:Label ID="lblwhr" runat="server" Text="Select WHR - " ForeColor="Navy" Font-Bold="true"
                            Font-Size="10pt"></asp:Label>
                    
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="DDLwhr" runat="server" Height="25px" Width="250px" 
                            AutoPostBack="true" onselectedindexchanged="DDLwhr_SelectedIndexChanged">
                        </asp:DropDownList>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        &nbsp;</td>
                    <td align="left">
                        OR</td>
                </tr>
                <tr>
                    <td align="left" style="width: 150px">
                        <asp:Label ID="Label1" runat="server" Text="WHR No-" ForeColor="Navy" 
                            Font-Bold="True" Font-Size="10pt"></asp:Label>
                       </td>
                    <td align="left">
                        <asp:TextBox ID="txtwhrnu" runat="server"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                </tr>
                <tr>
                    <td align="left" style="width: 150px">
                        &nbsp;</td>
                    <td align="left">
&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnsubmit" runat="server"  
                            Text="Submit" onclick="btnsubmit_Click" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                   
                        </td>
                </tr>
                </table>
    
    <asp:Panel ID="Panel1" runat="server">
        <rsweb:ReportViewer ID="ReportViewer_Depot" 
    runat="server" Width="850px" ProcessingMode="Remote"
                            Height="600px" ShowExportControls="true" 
    ShowPrintButton="true">
        </rsweb:ReportViewer>

    </asp:Panel>
</asp:Content>

