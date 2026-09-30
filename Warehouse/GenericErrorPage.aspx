<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GenericErrorPage.aspx.cs" Inherits="GenericErrorPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>error Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <br />
        <br />
        &nbsp;<table align="center" border="0" style="width: 616px; height: 56px">
            <tr>
                <td align="center" colspan="3" style="font-size: 15px; width: 616px; color: #990033;
                    height: 11px; background-color: #999999">
                    Some &nbsp;&nbsp; Error&nbsp;
                    Occured on Page</td>
            </tr>
            <tr>
                <td align="center" colspan="3" style="width: 616px; height: 16px">
                    <span style="font-weight: bold; font-size: 10px; color: #000000; font-style: normal">
                    </span>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="3" style="font-size: 10px; width: 616px; color: #000000;
                    height: 22px">
                    </td>
            </tr>
            <tr>
                <td colspan="3" style="font-size: 10px; width: 616px; color: #000000; height: 9px">
                </td>
            </tr>
            <tr>
                <td align="center" colspan="3" style="width: 616px; height: 4px; background-color: #999999">
                    <asp:LinkButton ID="lnlLogin" runat="server" OnClick="lnlLogin_Click" Style="font-size: 17px;
                        color: #000066">Go to the login page</asp:LinkButton>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
