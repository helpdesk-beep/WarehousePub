<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Gate_Pass.aspx.cs" Inherits="IssueCenterLevel_Storage_Gate_Pass" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <link href="../../css/style.css" rel="stylesheet" type="text/css" />    
<script language="javascript" type="text/javascript">
function getPrint()
{
   
    document.getElementById("btnclk").click();
   
}

function keyTrap(e)
{
// Netscape uses e, IE window.event
var eventObj = (e)?e:window.event;
var keyPressed = (e)?e.which:window.event.keyCode;
var ctrlPressed = (e)?(e.ctrlKey):window.event.ctrlKey;

if (ctrlPressed)
{
switch (keyPressed)
{
case 80: // CTRL-P.
window.event.keyCode = 0;
alert('Ctrl+P Key is Disabled in Gatepass, use print button to print the GatePass !');
return false;
//else
//return true;

}
}
}
if(document.layers)
{
//NS4+
document.captureEvents(Event.ONKEYDOWN);
}
document.onkeydown=keyTrap;

</script>

</head>
<body>
    <form id="form1" runat="server">
    <div style="background-color:White">
        <table style=" width: 700px;" bgcolor="#ffffff">
            <tr>
                <td align="right" colspan="7">
                    &nbsp;<asp:Button ID="btnclk" runat="server" OnClick="btnclk_Click" BorderColor="Transparent" Width="0px" />
                        <img src="../../images/printer_7.JPG" id="IMG1" style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none" onclick="getPrint()"/></td>
            </tr>
            
            <tr>
                <td colspan="7" align=center>
                <span style="font-size: 10pt; color: #990033">
                    <asp:Label ID="lblCGSWC" runat="server" Text="MP WAREHOUSING LOGISTIC CORPORATION" Font-Bold="True"></asp:Label></span></td>
            </tr>
            <tr id="trDuplicate" runat="server" visible="false">
                <td colspan="2" >
                <span style="font-size: 8pt; color: #990033">
                    &nbsp;&nbsp;&nbsp;<asp:Label ID="lblDuplicate" runat="server" Text="Duplicate" Font-Bold="True"></asp:Label></span></td>
                <td colspan="3">
                </td>
                <td colspan="2" align="right" style="height: 15px"></td>
                
                
            </tr>
            <tr height=25>
                <td style="height: 15px; width: 48px;">
                    </td>
                <td style="width: 180px; height: 15px;" align="left">
                    <asp:Label ID="lblDepot" runat="server" Text="Depot :" Font-Bold="True"></asp:Label></td>
                <td style="height: 15px;" colspan="3">
                    <asp:Label ID="lbldepot1" runat="server" Text="Label" Font-Bold="True"></asp:Label></td>
                <td style="width: 114px; height: 15px;" align="left">
                    <asp:Label ID="lblDistrict" runat="server" Text="District :" Font-Bold="True"></asp:Label></td>
                <td style="width: 100px; height: 15px;">
                    <asp:Label ID="lbldistrict1" runat="server" Text="Label" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 48px">
                </td>
                <td style="width: 180px">
                </td>
                <td colspan="3">
                </td>
                <td style="width: 114px">
                </td>
                <td style="width: 100px">
                </td>
            </tr>
            <tr height="30px">
                <td align="center" colspan="7"><span style="font-size: 10pt; color: #990033">
                    <asp:Label ID="lblGatePass" runat="server" Font-Bold="True" Visible="true"></asp:Label></span></td>
            </tr>
            <tr>
                <td style="width: 48px">
                </td>
                <td style="width: 180px">
                </td>
                <td colspan="3">
                </td>
                <td style="width: 114px">
                </td>
                <td style="width: 100px">
                </td>
            </tr>
            <tr height=25>
                <td style="width: 48px; height: 25px;" >
                    <asp:Label ID="lblSerialNo" runat="server" Text="SN :" Font-Bold="True"></asp:Label></td>
                <td style="width: 180px; height: 25px;">
                    <asp:Label ID="lblSN" runat="server" Text="Label" Font-Bold="True"></asp:Label></td>
                <td colspan="3" style="height: 25px">
                    </td>
                <td style="width: 114px; height: 25px;">
                    <asp:Label ID="lblDate_GP" runat="server" Text="Date :" Font-Bold="True"></asp:Label></td>
                <td style="width: 100px; height: 25px;">
                    <asp:Label ID="lblDate" runat="server" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr height="25">
                <td style="width: 48px" >
                    <strong>
                    1.</strong></td>
                <td style="width: 180px">
                    <asp:Label ID="lblNameDepot" runat="server" Text="Name of Depot :" Font-Bold="True"></asp:Label></td>
                <td colspan="3">
                    <asp:Label ID="lblNameOfDepot" runat="server" Text="Label" Font-Bold="True"></asp:Label></td>
                <td style="width: 114px">
                </td>
                <td style="width: 100px">
                    </td>
            </tr>
            <tr height="25" id="trNormal" runat="server" visible="false">
                <td style="width: 48px">
                    <strong>2.</strong></td>
                <td>
                    <asp:Label ID="lblGodownNo" runat="server" Text="Godown No. :" Font-Bold="True"></asp:Label></td>
                <td colspan="3">
                    <asp:Label ID="lblGodownNo1" runat="server" Text="Label" Font-Bold="True"></asp:Label></td>
                <td style="width: 114px">
                    <asp:Label ID="lblStackNo" runat="server" Text="Stack No. :" Font-Bold="True" ></asp:Label></td>
                <td>
                    <asp:Label ID="lblStackno1" runat="server" Text="label" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr height="25" id="trFinal" runat="server" visible="false">
                <td valign="top" style="width: 48px"><strong>2.</strong>
                </td>
                <td colspan="6" valign="top">
                <asp:DataGrid ID="GDStack" runat="server" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" Font-Bold="True" GridLines="None" HorizontalAlign="Left" Width="57%" AutoGenerateColumns="False">
                    <AlternatingItemStyle BorderStyle="None" Font-Bold="True" />
                    <ItemStyle BorderStyle="None" />
                    <Columns>
                        <asp:BoundColumn DataField="GodownName" HeaderText="Godown Name :"></asp:BoundColumn>
                        <asp:BoundColumn DataField="StackName" HeaderText="Stack Name :"></asp:BoundColumn>
                    </Columns>
                </asp:DataGrid>
                </td>
            </tr>
            <tr height=25>
                <td style="width: 48px" >
                    <strong>2.</strong></td>
                <td style="width: 180px">
                    <asp:Label ID="lblDepositorReceiverName" runat="server" Text="Depositor/Receiver Name :" Font-Bold="True"></asp:Label></td>
                <td colspan="3">
                    <asp:Label ID="lblDepositorName1" runat="server" Text="Label" Font-Bold="True"></asp:Label></td>
                <td style="width: 114px">
                </td>
                <td style="width: 100px">
                    </td>
            </tr>
            <tr height=25>
                <td style="width: 48px">
                    <strong>3.</strong></td>
                <td>
                    <asp:Label ID="lblCommodityName" runat="server" Text="Commodity Name :" Font-Bold="True"></asp:Label></td>
                <td colspan="3">
                    <asp:Label ID="lblCommodityName1" runat="server" Text="Label" Font-Bold="True"></asp:Label></td>
                <td style="width: 114px">
                    <asp:Label ID="lblSchemeName" runat="server" Text="Scheme Name :" Font-Bold="True"></asp:Label></td>
                <td>
                    <asp:Label ID="lblschemeName1" runat="server" Text="Label" Font-Bold="True" Width="100%"></asp:Label></td>
            </tr>
            <tr height=25>
                <td style="height: 25px; width: 48px;">
                    <strong>4.</strong></td>
                <td style="height: 25px">
                    <asp:Label ID="lblTCNo" runat="server" Text="TC No. :" Font-Bold="True"></asp:Label></td>
                <td colspan="3" style="height: 25px">
                    <asp:Label ID="lblTCNo1" runat="server" Text="Label" Font-Bold="True"></asp:Label></td>
                <td style="width: 114px; height: 25px;">
                    <asp:Label ID="lblTruckNumber" runat="server" Text="Truck No. :" Font-Bold="True" ></asp:Label></td>
                <td style="height: 25px">
                    <asp:Label ID="lblTruckNo" runat="server" Text="Label" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr height="25">
                <td style="width: 48px">
                    <strong>5.</strong></td>
                <td style="width: 180px">
                    <asp:Label ID="lblNoofBags" runat="server" Text="No. of Bags :" Font-Bold="True"></asp:Label></td>
                <td colspan="3">
                    <asp:Label ID="lblNoOfBags1" runat="server" Text="Label" Font-Bold="True"></asp:Label></td>
                <td style="width: 114px">
                    <asp:Label ID="lblWeight" runat="server" Text="Weight :" Font-Bold="True"></asp:Label>
                    <asp:Label ID="lblQltsKgsgms" runat="server" Text="(Qtl.kgGms)"></asp:Label></td>
                <td style="width: 100px">
                    <asp:Label ID="lblweight1" runat="server" Text="Label" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr height="25">
                <td style="width: 48px; height: 65px;" valign="top">
                    <strong>6.</strong></td>
                <td style="width: 180px; height: 65px;" valign="top">
                    <asp:Label ID="lblRemarks" runat="server" Text="Remarks :" Font-Bold="True"></asp:Label></td>
                <td colspan="5" style="height: 65px" valign="top">
                    </td>
            </tr>
            <tr height=25 visible="false" id="trDriver" runat="server">
                <td style="width: 48px" >
                </td>
                <td style="width: 180px">
                    <asp:Label ID="lblDriverName" runat="server" Text="Driver Name :" Font-Bold="True"></asp:Label>
                </td>
                <td colspan="3">
                    <asp:Label ID="lblDriverName1" runat="server" Text="Label" Font-Bold="True"></asp:Label></td>
                <td style="width: 114px">
                    <asp:Label ID="lblMiller" runat="server" Text="Miller Name :" Font-Bold="True"></asp:Label>
                </td>
                <td style="width: 100px">
                    <asp:Label ID="lblMillerName" runat="server" Font-Bold="True" Text="Label" Width="100%"></asp:Label>
                    </td>
            </tr>
            <tr height=25 visible="false" id="trDL" runat="server">
                <td style="width: 48px; height: 15px;">
                </td>
                <td style="width: 180px; height: 15px;">
                    <asp:Label ID="lblLicenseNo" runat="server" Text="License No. :" Font-Bold="True"></asp:Label></td>
                <td colspan="3" style="height: 15px">
                    <asp:Label ID="lbllicense1" runat="server" Font-Bold="True"></asp:Label></td>
                <td style="width: 114px; height: 15px;">
                    <asp:Label ID="lblTypeVehicle" runat="server" Text="Type of Vehicle :" Font-Bold="True"></asp:Label></td>
                <td style="width: 100px; height: 15px;">
                    <asp:Label ID="lblTypeofVehicle" runat="server" Text="Label" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr height=25 visible="false" id="trDLValidity" runat="server">
                <td style="width: 48px" >
                </td>
                <td style="width: 180px">
                    <asp:Label ID="lblValidUpto" runat="server" Text="Valid Upto :" Font-Bold="True"></asp:Label></td>
                <td colspan="3">
                    <asp:Label ID="lblValid1" runat="server" Font-Bold="True"></asp:Label></td>
                <td style="width: 114px">
                </td>
                <td style="width: 100px">
                </td>
            </tr>
            <tr height=25 visible="false" id="trArrTime" runat="server">
                <td style="width: 48px; height: 15px;" align="left" valign="middle">
                    <strong>7.</strong></td>
                <td style="width: 180px; height: 15px;" align="left" valign="middle">
                    <asp:Label ID="lblArrivalDepTime" runat="server" Text="Arrival" Font-Bold="True"></asp:Label><asp:Label ID="Label2" runat="server" Font-Bold="True" Text="/"></asp:Label><asp:Label ID="lblDeparture" runat="server" Font-Bold="True" Text="Departure"></asp:Label><asp:Label
                        ID="lblTime" runat="server" Font-Bold="True" Text=" Time :"></asp:Label></td>
                <td colspan="3" style="height: 15px" align="left" valign="middle">
                    <asp:Label ID="lblArrivalDate" runat="server" Font-Bold="True"></asp:Label></td>
                <td style="width: 114px; height: 15px;" align="left" valign="middle">
                </td>
                <td style="width: 100px; height: 15px;" align="left" valign="middle">
                </td>
            </tr>
            <tr height=25 visible="false">
                <td style="height: 33px; " >
                    </td>
                <td style="height: 33px;">
                    </td>
                <td colspan="3">
                    </td>
                <td style="height: 33px;">
                    &nbsp;<br />
                    <br />
                    </td>
                <td style="height: 33px;">
                    </td>
            </tr>
            <tr height=25 visible="false" id="trGPRemarks" runat="server">
                <td valign="top" >
                    <strong>8.</strong></td>
                <td valign="top">
                    <asp:Label ID="lblDesc" runat="server" Text="Description of Available Papers of  Gatepass in Truck :" Font-Bold="True"></asp:Label></td>
                <td colspan="5" valign="top">
                    <asp:Label ID="Label10" runat="server" Font-Bold="True" Width="320px"></asp:Label></td>
            </tr>
            <tr height=55>
                <td style="width: 48px" >
                </td>
                <td style="width: 180px">
                </td>
                <td colspan="3">
                </td>
                <td colspan="2">
                </td>
            </tr>
            <tr height=25>
                <td style="width: 48px" >
                </td>
                <td style="width: 180px">
                    <asp:Label ID="lblSignTruckDriver" runat="server" Text="Signature of Truck Driver " Font-Bold="True"></asp:Label></td>
                <td align="center" colspan="3">
                    <asp:Label ID="lblSignGodwnIncharge" runat="server" Text="Signature of Godown Incharge" Font-Bold="True"></asp:Label></td>
                <td align="center" colspan="2">
                    &nbsp; &nbsp;
                    <asp:Label ID="lblSignBranchManager" runat="server" Text="Signature of Branch Manager" Font-Bold="True"></asp:Label></td>
            </tr>
        </table>
    
    </div>
    <asp:Image ID="imgcancelled" style="Z-INDEX: 101; LEFT: 113px; POSITION: absolute; TOP: 122px" runat="server" Width="435px" Height="452px" Visible="False" ImageUrl="~/images/Cancelled.gif"/>
    </form>
</body>
</html>
