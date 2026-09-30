<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DataImportToMPSCSC.aspx.cs" Inherits="DataImportToMPSCSC" %>

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
                                            <td class="auto-style3"><asp:Label ID="Label1" runat="server" Text="tbl_MetaData_STORAGE_COMMODITY" Font-Bold="true"
                                                    Font-Size="12pt"></asp:Label></td>
                                                                                        
                                        </tr>
                                        <tr>
                                        <td style="height: 50px;"><b>1.</b>
                                            </td>

                                            <td class="auto-style3"><asp:Label ID="Label6" runat="server" Text="Commodity_Name" Font-Bold="true"
                                                    Font-Size="12pt"></asp:Label></td>
                                            <td><asp:TextBox ID="txtCommodityName" runat="server" AutoPostBack="true" BackColor="LemonChiffon" 
        TabIndex="9" CssClass="tb6" Width="190px" Height="20px" 
                 ></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                        <td style="height: 50px;"><b>2.</b>

                                            </td>

                                            <td class="auto-style3"><asp:Label ID="Label11" runat="server" Text="Status" Font-Bold="true"
                                                    Font-Size="12pt"></asp:Label></td>

                                            <td><asp:TextBox ID="txtStatus" runat="server" AutoPostBack="true" BackColor="LemonChiffon" 
        TabIndex="9" CssClass="tb6" Width="190px" Height="20px" 
                 ></asp:TextBox></td>
                                            
                                        </tr>
                                        <tr>
                                        <td style="height: 50px;"><b>3.</b>

                                            </td>

                                            <td class="auto-style3"><asp:Label ID="Label15" runat="server" Text="Rep Group Code" Font-Bold="true"
                                                    Font-Size="12pt"></asp:Label></td>

                                            <td><asp:TextBox ID="txtRepGroupCode" runat="server" AutoPostBack="true" BackColor="LemonChiffon" 
        TabIndex="9" CssClass="tb6" Width="190px" Height="20px" 
                 ></asp:TextBox></td>
                                            
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
