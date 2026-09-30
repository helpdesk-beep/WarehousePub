<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StockPosition_Depot.aspx.cs"
    Inherits="StockPosition_Depot" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="~/GoogleMapForASPNet.ascx" TagName="GoogleMapForASPNet" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Current Stock Position</title>

    <script type="text/javascript">
        function flasher() {
            if (document.getElementById("lbl_h1")) {
                var h1 = document.getElementById("lbl_h1");
                var h2 = document.getElementById("lbl_h2");
                h1.style.color = (h1.style.color == 'purple' ? 'green' : 'purple');
                h2.style.color = (h2.style.color == 'purple' ? 'green' : 'purple');
                setTimeout('flasher()', 2000);
            }
        }                       
    </script>

</head>
<body onload="flasher()">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
        <table width="100%" style="background-color: #fefbea">
            <tr>
                <td align="center" valign="top">
                    <table width="500px" border="1" cellspacing="0" cellpadding="0" style="border-color: Navy">
                        <tr>
                            <td colspan="2" align="center">
                                <span style="font-size: 12pt; font-weight: bolder; color: Green">
                                     All Branches with Geographical Locations </span>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <img alt="" src="icons/Green.png" width="20px" height="30px" />
                            </td>
                            <td align="left">
                                <span style="font-size: 10pt; font-weight: bolder; color: Navy">Storage in Branch is
                                    Empty</span>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <img alt="" src="icons/Red.png" width="20px" height="30px" />
                            </td>
                            <td align="left">
                                <span style="font-size: 10pt; font-weight: bolder; color: Navy">More Than 80% Storage
                                    in Branch</span>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <img alt="" src="icons/Blue.png" width="20px" height="30px" />
                            </td>
                            <td align="left">
                                <span style="font-size: 10pt; font-weight: bolder; color: Navy">Space Available For
                                    Storage in Branch</span>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <uc1:GoogleMapForASPNet ID="GoogleMapForASPNet1" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
