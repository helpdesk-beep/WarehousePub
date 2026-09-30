<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AllpendingReciving.aspx.cs" Inherits="Reports_States_AllpendingReciving" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Pending Data</title>
    <script type="text/javascript">
        function printGrid() {
            var gridData = document.getElementById('<%= gvpendingdata.ClientID %>');
            var windowUrl = 'about:blank';
            //set print document name for gridview
            var uniqueName = new Date();
            var windowName = 'Print_' + uniqueName.getTime();

            var prtWindow = window.open(windowUrl, windowName,
        'left=100,top=100,right=100,bottom=100,width=700,height=500');
            prtWindow.document.write('<html><head></head>');
            prtWindow.document.write('<body style="background:none !important">');
            prtWindow.document.write(gridData.outerHTML);
            prtWindow.document.write('</body></html>');
            prtWindow.document.close();
            prtWindow.focus();
            prtWindow.print();
            prtWindow.close();
        }
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <center>
    <div style="background-color: #808080">
        <asp:HyperLink ID="HyperLink1" NavigateUrl="~/IssueCenterLevel/Storage/Report_Region.aspx" runat="server">Go to previous page</asp:HyperLink>
        </div>

        <table>
        <tr>
        <td>
        <asp:Button ID="btnPrint" runat="server" Text="Print" OnClientClick="printGrid()" />
        </td>
        </tr>
        <tr>
        <td>

            <asp:GridView ID="gvpendingdata" runat="server">
            </asp:GridView>
        </td>
        </tr>
        </table>
        </center>
    </div>
    </form>
</body>
</html>
