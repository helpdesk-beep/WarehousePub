<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportViewer_Depot.aspx.cs"
    Inherits="IssueCenterLevel_Storage_ReportViewer_Depot" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=B03F5F7F11D50A3A"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Warehouse Reports</title>
    <style type="text/css">
 body
 {
  background-image: url(images/Mainbg.jpg);
  background-repeat:repeat;
 }
 .BTNBLUE
{
border:1px solid #7eb9d0; -webkit-border-radius: 3px; -moz-border-radius: 3px;border-radius: 3px;font-size:12px;font-family:arial, helvetica, sans-serif; padding: 10px 10px 10px 10px; text-decoration:none; display:inline-block;text-shadow: -1px -1px 0 rgba(0,0,0,0.3);font-weight:bold; color: #FFFFFF;
 background-color: #a7cfdf; background-image: -webkit-gradient(linear, left top, left bottom, from(#a7cfdf), to(#23538a));
 background-image: -webkit-linear-gradient(top, #a7cfdf, #23538a);
 background-image: -moz-linear-gradient(top, #a7cfdf, #23538a);
 background-image: -ms-linear-gradient(top, #a7cfdf, #23538a);
 background-image: -o-linear-gradient(top, #a7cfdf, #23538a);
 background-image: linear-gradient(to bottom, #a7cfdf, #23538a);filter:progid:DXImageTransform.Microsoft.gradient(GradientType=0,startColorstr=#a7cfdf, endColorstr=#23538a);
}

.BTNBLUE:hover{
 border:1px solid #5ca6c4;
 background-color: #82bbd1; background-image: -webkit-gradient(linear, left top, left bottom, from(#82bbd1), to(#193b61));
 background-image: -webkit-linear-gradient(top, #82bbd1, #193b61);
 background-image: -moz-linear-gradient(top, #82bbd1, #193b61);
 background-image: -ms-linear-gradient(top, #82bbd1, #193b61);
 background-image: -o-linear-gradient(top, #82bbd1, #193b61);
 background-image: linear-gradient(to bottom, #82bbd1, #193b61);filter:progid:DXImageTransform.Microsoft.gradient(GradientType=0,startColorstr=#82bbd1, endColorstr=#193b61);
}
 
 </style>

    <script type="text/javascript" src="../../JS/popcalendar.js"></script>

    <script type="text/javascript" src="../../JS/ShowCalender.js"></script>

</head>
<body>
    <form id="form1" runat="server">
        <center>
            <div>
                <table cellpadding="0" cellspacing="0" style="width: 1000px; border-width: 1px; border-color: Navy"
                    border="1px">
                    <tr style="background-color: #95ccff;">
                        <td align="left" style="width: 200px">
                            <asp:Label ID="lbl_District" runat="server" Text="District - " ForeColor="navy" Font-Bold="true"
                                Font-Size="8pt"></asp:Label>
                        </td>
                        <td align="left" style="width: 250px">
                            <asp:DropDownList ID="ddl_District" runat="server" Width="155px" Height="25px">
                            </asp:DropDownList>
                        </td>
                        <td align="left" style="width: 150px">
                            <asp:Label ID="lbl_depot" runat="server" Text="Depot - " ForeColor="navy" Font-Bold="true"
                                Font-Size="8pt"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddl_Depot" runat="server" Width="155px" Height="25px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr style="background-color: #95ccff;">
                        <td align="left" colspan="4" style="height: 5px">
                        </td>
                    </tr>
                    <tr style="background-color: #95ccff;">
                        <td align="left">
                            <asp:Label ID="lbl_todate" runat="server" Text="From Date - " ForeColor="navy" Font-Bold="true"
                                Font-Size="8pt"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txt_From" runat="server" Width="150px"></asp:TextBox>
                            <a onclick="ShowCalendar(txt_From,txt_From);" href="javascript:;">
                                <img height="16" alt="Click Here to Pick up the date" src="../../images/cal.gif"
                                    width="16" border="0" /></a>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txt_From"
                                Display="Dynamic" ErrorMessage="Delivery Order From Date field cannot be empty"
                                SetFocusOnError="True" ValidationGroup="a">*</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txt_From"
                                Display="Dynamic" ErrorMessage="Delivery Order To Date is not valid" SetFocusOnError="True"
                                ValidationGroup="a" ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$">></asp:RegularExpressionValidator>
                        </td>
                        <td align="left">
                            <asp:Label ID="lbl_fromdate" runat="server" Text="To Date - " ForeColor="navy" Font-Bold="true"
                                Font-Size="8pt"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txt_to" runat="server" Width="150px"></asp:TextBox>
                            <a onclick="ShowCalendar(txt_to,txt_to);" href="javascript:;">
                                <img height="16" alt="Click Here to Pick up the date" src="../../images/cal.gif"
                                    width="16" border="0" /></a>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_to"
                                Display="Dynamic" ErrorMessage="Delivery Order From Date field cannot be empty"
                                SetFocusOnError="True" ValidationGroup="a">*</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txt_to"
                                Display="Dynamic" ErrorMessage="Delivery Order To Date is not valid" SetFocusOnError="True"
                                ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$">></asp:RegularExpressionValidator>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                            <asp:Button ID="btn_Search" runat="server" Text="Search" CssClass="BTNBLUE" OnClick="btn_Search_Click"
                                Width="100px" ValidationGroup="a" />
                        </td>
                    </tr>
                    <tr style="background-color: #95ccff;">
                        <td align="left" colspan="4" style="height: 5px">
                        </td>
                    </tr>
                    <tr style="background-color: #95ccff;">
                        <td align="left">
                            <asp:Label ID="lbl_do_no" runat="server" Text="Delivery Order No. - " ForeColor="navy"
                                Font-Bold="true" Font-Size="8pt"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddl_do_no" runat="server" Width="155px" Height="25px" AutoPostBack="True"
                                OnSelectedIndexChanged="ddl_do_no_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td align="left">
                            <asp:Label ID="Label4" runat="server" Text="Language - " ForeColor="navy" Font-Bold="true"
                                Font-Size="8pt"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddl_Language" runat="server" Width="155px" Height="25px">
                                <asp:ListItem Value="2">English</asp:ListItem>
                                <asp:ListItem Value="1">Hindi</asp:ListItem>
                            </asp:DropDownList>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                            <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="~/IssueCenterLevel/Storage/Reports.aspx"
                                ForeColor="indianred" Font-Bold="true">पिछले पृष्ठ पर जाये</asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" valign="top" colspan="4">
                            <rsweb:ReportViewer ID="ReportViewer_Depot" runat="server" Width="1000px" ProcessingMode="Remote"
                                Height="550px">
                            </rsweb:ReportViewer>
                        </td>
                    </tr>
                </table>
            </div>
        </center>
    </form>
</body>
</html>
