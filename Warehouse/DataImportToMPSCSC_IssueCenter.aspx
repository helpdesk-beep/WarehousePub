<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DataImportToMPSCSC_IssueCenter.aspx.cs" Inherits="DataImportToMPSCSC_IssueCenter" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 839px;
        }
        .auto-style2 {
            margin-left: 0px;
        }
        .auto-style3 {
            width: 402px;
        }
        .auto-style4 {
            float: left;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div>
                <table cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td align="center" valign="top" class="auto-style1">
                            <center>
                                <div>
                                    <table cellpadding="0" cellspacing="0" style="width: 100%" class="auto-style4">
                                        <tr style="background-color: #0bb6e6; height: 25px">
                                            <td colspan="8" align="center">
                                                <asp:Label ID="lblDataTransferMPSCSC" runat="server" Text="Table Details" Font-Bold="true"
                                                    Font-Size="15pt"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 5px" colspan="8"></td>
                                        </tr>
                                        <tr>
                                        <td style="height: 50px;"><b></b>
                                            </td>
                                            <td class="auto-style3"><asp:Label ID="Label1" runat="server" Text="MetaDataBranchWithIssueCenter" Font-Bold="true"
                                                    Font-Size="12pt"></asp:Label></td>
                                                                                        
                                        </tr>
                                        <tr>
                                        <td style="height: 50px;">&nbsp;</td>

                                            <td class="auto-style3">&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                        <td style="height: 50px;">&nbsp;</td>

                                            <td class="auto-style3">&nbsp;</td>

                                            <td>&nbsp;</td>
                                            
                                        </tr>
                                        <tr>
                                        <td style="height: 50px;">&nbsp;</td>

                                            <td class="auto-style3">&nbsp;</td>

                                            <td>&nbsp;</td>
                                            
                                        </tr>
                                        <tr>
                                        <td style="height: 50px;"><b></b>
                                            </td>
                                     
                                            
                                            <td><asp:Button runat="server" Text="Import" ID="Button4" OnClick="btnImportCommodity_Click" Height="25px"  Font-Bold="true" CssClass="auto-style2" /></td>
                                        </tr>


                                        <tr>
                                            <td colspan="8" valign="top" align="center">
                                                
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </center>
                        </td>
                    </tr>
                    
                 
                </table>
            </div>
        </div>
     
    </form>
</body>
</html>
