<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fr_WHR_Insert_CSMS.aspx.cs" Inherits="Kharif2020_21_fr_WHR_Insert_CSMS" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 1526px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table style="border:solid" class="auto-style1">
        <tr>
            <td style="background-color: #6699FF" align="center">
                 <asp:Label ID="lbl_header" runat="server" Text="Insert WHR CSMS" Font-Bold="True" Font-Size="X-Large" ForeColor="#000066"></asp:Label>
                
                 <br />
                
            </td>
        </tr>
         <tr>
            <td style="" align="center">
                 
                <asp:Button ID="btn_update" runat="server" Text="Update" Width="153px" OnClick="btn_update_Click" />
            </td>
        </tr>
        
        <tr>
            <td style="" align="center">
                Max Created Date :
                <asp:Label ID="lbl_maxtcrtdate" runat="server" Font-Bold="True" ForeColor="#006600"></asp:Label>
                <br />
                <asp:Label ID="lbl_rowcount" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                <asp:Label ID="lbl_msg" runat="server" Font-Bold="True" ForeColor="#FF3300"></asp:Label>
                <asp:GridView ID="grdview_data" runat="server">
                </asp:GridView>
            </td>
        </tr>

    </table> 
    </div>
    </form>
</body>
</html>