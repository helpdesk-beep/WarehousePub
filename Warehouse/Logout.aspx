<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Logout.aspx.cs" Inherits="Logout" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <h2>
        <span style="font-size: 10pt; color: #6600ff; font-family: Verdana">
        <br />
                <br />
                        <br />
                                <br />
                                        <br />
                                                <br />
                                                
                                                        <br />
            &nbsp;<table align ="center" style="width: 600px; font-weight: bold; border-top-style: solid; border-right-style: solid; border-left-style: solid; border-bottom-style: solid; border-left-color: #000000; border-bottom-color: #000000; border-top-color: #000000; border-right-color: #000000; height: 1px;" border="2" id="TABLE1" language="javascript" onclick="return TABLE1_onclick()" >
                <tr>
                    <td style="height: 100px; width: 300px;" bordercolor="#000066">
                        <table align="center" border="0" style="width: 616px; height: 56px" >
                            <tr>
                    <td colspan="3" style="width: 616px; height: 11px; color: #990033; background-color: #cccc66; font-size: 15px;" align="center">
                        Error-Your login session
            has expired.</td>
                </tr>
                <tr>
                    <td style="width: 616px; height: 16px;" colspan="3" align=center>
                        <span style="color: #000000; font-size: 10px; font-style: normal; font-weight: bold;">Why does my session expire?</span></td>
                </tr>
                <tr>
                    <td style="width: 616px; font-size: 10px; height: 22px; color: #000000;" colspan="3" align=center>
        Login sessions expire for this reasons:</td>
                </tr>
                <tr>
                    <td colspan="3" style="width: 616px; height: 9px; font-size: 10px; color: #000000;">
        For your security, your Application session <b>expires a maximum of twenty minutes</b>
        after you have logged in.</td>
                </tr>
               <tr>
                <td align="center" style="color: #b22222; font-family: Verdana; height: 20px; background-color: #cccc66">
                    <asp:HyperLink ID="HyperLink1" runat="server" ForeColor="Maroon" NavigateUrl="~/login.aspx">Go to the login page</asp:HyperLink></td>
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

</body>
</html>
