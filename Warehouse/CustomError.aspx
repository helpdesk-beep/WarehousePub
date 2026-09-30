<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustomError.aspx.cs" Inherits="CustomError" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Custom Error Page</title>
     <link href="../css/style.css" rel="stylesheet" type="text/css" />
   </head>
<body>
<script language="javascript" type="text/javascript">
window.history.forward(1);

    function CustError()
		{
			if (history.length > 0)
			{
				history.back();
			}
			else
			{
				alert("Reload page Again!!");
			}
					
		}
    </script>
 
    <form id="form1" runat="server" method="post">
         <h2>
        <span style="font-size: 10pt; color: #6600ff; font-family: Verdana">
            <table align ="center" style="width: 600px; font-weight: bold; border-top-style: solid; border-right-style: solid; border-left-style: solid; border-bottom-style: solid; border-left-color: #000000; border-bottom-color: #000000; border-top-color: #000000; border-right-color: #000000; height: 1px;" border="2" id="TABLE1" language="javascript"  >
                <tr>
                    <td style="height: 100px; width: 300px;" bordercolor="#000066">
                        <table align="center" border="0" style="width: 616px; height: 56px" >
                            <tr>
                    <td colspan="3" style="width: 616px; height: 11px; color: #990033; background-color: #999999; font-size: 15px;" align="center">
                        Error-Your login session
            has expired.</td>
                </tr>
                <tr>
                    <td style="width: 616px; height: 16px;" colspan="3" align=center>
                        <span style="color: #000000; font-size: 10px; font-style: normal; font-weight: bold;"></span></td>
                </tr>
                <tr>
                    <td style="width: 616px; font-size: 10px; height: 22px; color: #000000;" colspan="3" align=center>
                        <asp:Label ID="lblErrorMsg" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label></td>
                </tr>
                <tr>
                    <td colspan="3" style="width: 616px; height: 9px; font-size: 10px; color: #000000;">
        </td>
                </tr>
                <tr>
                    <td colspan="3" style="width: 616px; height: 4px; background-color: #999999;"  align="center">
                        <asp:LinkButton id="lnlLogin" runat="server" OnClick="lnlLogin_Click" style="font-size: 17px; color: #000066" >Go to the login page</asp:LinkButton>
                    </td>
                </tr>
                        </table>
                    </td>
                </tr>
                
            </table>
        </span></h2>
    <h3 style="margin: 0.5em 0px">
        <span style="font-size: 10pt; color: #3300ff"></span>&nbsp;</h3>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p style="text-align: center">
        <b><span style="color: #003399"><a href="login.aspx?Logout=true" target="_parent"></a>
        </span></b></p>
           </form>
</body>
</html>
