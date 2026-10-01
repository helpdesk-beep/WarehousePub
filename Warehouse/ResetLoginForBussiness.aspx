<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ResetLoginForBussiness.aspx.cs" Inherits="ResetLoginForBussiness" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Import Namespace="System.Text.RegularExpressions" %>
<%
    string u = Request.ServerVariables["HTTP_USER_AGENT"];
    Regex b = new Regex(@"(android|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|iris|kindle|lge |maemo|midp|mmp|mobile.+firefox|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows ce|xda|xiino", RegexOptions.IgnoreCase | RegexOptions.Multiline);
    Regex v = new Regex(@"1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-", RegexOptions.IgnoreCase | RegexOptions.Multiline);
    if ((b.IsMatch(u) || v.IsMatch(u.Substring(0, 4))))
    {
        Response.Redirect("Mobile/MLogin.aspx");
    }
%>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Welcome to Warehouse login Form</title>
    <link type="text/css" rel="Stylesheet" href="css/style_new.css" />
    <link rel="shortcut icon" href="images/favicon.ico" />

    <script type="text/javascript">
        $(function () {

            $('.box').on('mouseenter mouseleave', function (e) {
                $(this).siblings().stop().fadeTo(300, e.type == 'mouseenter' ? 0.5 : 1);
            });

        });
    </script>


    <script language="javascript" type="text/javascript" src="js/MD5.js"></script>

    <script language="javascript" type="text/javascript" src="js/chksql.js"></script>
    <style type="text/css">
        .hit-the-floor {
            color: #fff;
            font-size: 2em;
            font-weight: bold;
            font-family: Helvetica;
            text-shadow: 0 1px 0 #ccc, 0 2px 0 #c9c9c9, 0 3px 0 #bbb, 0 4px 0 #b9b9b9, 0 5px 0 #aaa, 0 6px 1px rgba(0,0,0,.1), 0 0 5px rgba(0,0,0,.1), 0 1px 3px rgba(0,0,0,.3), 0 3px 5px rgba(0,0,0,.2), 0 5px 10px rgba(0,0,0,.25), 0 10px 10px rgba(0,0,0,.2), 0 20px 20px rgba(0,0,0,.15);
        }

        .hit-the-floor {
            text-align: center;
        }

        .button {
            -webkit-border-radius: 5px;
            -moz-border-radius: 5px;
            border-radius: 5px;
            background-image: -webkit-gradient(linear, left bottom, left top, color-stop(0.16, rgb(207, 207, 207)), color-stop(0.79, rgb(252, 252, 252)));
            background-image: -moz-linear-gradient(center bottom, rgb(207, 207, 207) 16%, rgb(252, 252, 252) 79%);
            background-image: linear-gradient(to top, rgb(207, 207, 207) 16%, rgb(252, 252, 252) 79%);
            padding: 3px;
            border: 1px solid #000;
            color: black;
            text-decoration: none;
        }
    </style>
    <style type="text/css">
        .divWaiting {
            position: absolute;
            background-color: #FAFAFA;
            z-index: 2147483647 !important;
            opacity: 0.8;
            overflow: hidden;
            text-align: center;
            top: 0;
            left: 0;
            height: 100%;
            width: 100%;
            padding-top: 20%;
        }
    </style>


    <%--Updated By Ashutosh--%>
    <script src="<%= ResolveUrl("~/NEW_CSS/js/jquery-1.12.4.js") %>"></script>
    <link href="assets/New/css/select2.min.css" rel="stylesheet" />
    <script type="text/javascript" src="assets/New/js/select2.min.js"></script>

    <%--Updated By Ashutosh End--%>
</head>
<body onload="noBack()">
    <form id="form1" runat="server" defaultbutton="btn_login">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <center>
            <div style="width: 1010px; height: 100%">
                <fieldset style="width: 100%; border: 1px solid navy;">
                    <table align="center" width="100%" cellpadding="0" style="border: 1px; border-color: Navy"
                        cellspacing="0">

                        <tr>
                            <td align="center" style="height: 430px">
                                <center>
                                    
                                            <div class="box" style="width: 650px">
                                                <table align="center" cellpadding="0" cellspacing="0" style="width: 650px; border: 10px solid #cb4e48; background-color: #F5ECCE">
                                                    <tr style="background-color: #d53e21">
                                                        <td align="center" colspan="2" valign="middle">
                                                            <span style="color: White; font-weight: bolder; font-size: 15pt">LOGIN PANEL</span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 10px" colspan="2"></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" colspan="2">
                                                            <asp:RadioButtonList ID="rblLoginType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rblLoginType_SelectedIndexChanged"
                                                                RepeatDirection="Horizontal" Font-Bold="True" Font-Size="10pt" ForeColor="Navy"
                                                                CellSpacing="10">
                                                                <asp:ListItem Value="3" Selected>State</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td colspan="2" align="center">
                                                            <span style="color: Navy; font-weight: bolder; font-size: 15pt">----------------------------------------------------------</span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 10px" align="center">

                                                            <asp:Label ID="lbllogintype" runat="server" Font-Bold="True" Font-Size="12pt"
                                                                ForeColor="Navy" Text="Login Type:"></asp:Label>

                                                        </td>
                                                        <td align="left">

                                                            <asp:DropDownList ID="ddllogintype" runat="server"
                                                                CssClass="tb6" Font-Bold="true" ForeColor="Navy" Height="30px"
                                                                Width="205px" AutoPostBack="True">
                                                            </asp:DropDownList>

                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" style="height: 10px">&nbsp;</td>
                                                    </tr>

                                                    <tr>
                                                        <td align="center" colspan="2">
                                                            <asp:Button ID="btn_login" runat="server" Text="Update First Time Login" OnClick="btn_login_Click"
                                                                ValidationGroup="1" ToolTip="Click for Login" CssClass="button" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 10px" colspan="2"></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                                                ValidationGroup="1" ShowSummary="false" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" align="center">
                                                            <span style="color: Navy; font-weight: bolder; font-size: 15pt">----------------------------------------------------------</span>
                                                        </td>
                                                    </tr>
                                                    <%-- <tr>
                                                        <td align="center" colspan="2">
                                                            <asp:CheckBox ID="CheckBox1" runat="server" Text="Hindi Version" Font-Size="10pt"
                                                                Font-Bold="true" ForeColor="Navy" />
                                                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                         <img src="images/pdflogo.png" style="width: 21px" alt="" height="21px" />
                                                            <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/UserManual/Godown Mapping menual.pdf"
                                                                Font-Size="10pt" Font-Bold="true" Target="_blank" ForeColor="Navy">UserManual for Godown longitude latitude entry</asp:HyperLink>

                                                        </td>
                                                    </tr>--%>
                                                    <tr>
                                                        <td style="height: 10px; font-size: 10px; font-weight: bold" colspan="2" align="center">
                                                            <br />
                                                            <br />

                                                            <%--<a href="Reports/States/Rpt_Deliveryissuedetails.aspx">Godown wise current stock details</a>--%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" align="center">
                                                            <asp:Label ID="lblError" runat="server" ForeColor="#C00000"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </center>
                            </td>
                        </tr>
                        <tr style="background-color: Gray">
                            <td style="height: 2px"></td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <div style="background-image: url('../images/div_bg.png')">
                                    <table style="width: 100%">
                                        <%-- <tr>
                                            <td style="height: 20px;" colspan="5">
                                                <img id="Img2" src="Images/line.png" height="30px" width="100%" alt="" />
                                            </td>
                                        </tr>--%>
                                        <tr>
                                            <%-- <td style="width: 20%" align="center">
                                                <a href="http://www.mp.nic.in/">
                                                    <img src="Images/NIC-logo.png" width="200px" height="50px" alt="" />
                                                </a>
                                            </td>--%>
                                            <%--<td style="width: 1%" align="center">
                                                <img id="Img1" src="Images/Linevertical.png" style="width: 20px; height: 50px" alt="" />
                                            </td>--%>
                                            <td style="color: Navy; font-size: 8pt; width: 58%;" align="center">
                                                <b>© 2011 &nbsp;National Informatics Centre.All Rights Reserved
                                                <br />
                                                    Developed By : National Informatics Centre
                                                <br />
                                                    Madhya Pradesh, Ministry of Communications and Information Technology</b>
                                            </td>
                                            <%--<td style="width: 1%" align="center">
                                                <img id="Logo" src="Images/Linevertical.png" style="width: 20px; height: 50px" alt="" />
                                            </td>--%>
                                            <%--<td style="width: 20%" align="center">
                                                <table>
                                                    <tr>
                                                        <td><a href="http://india.gov.in/">
                                                            <img src="Images/natindialogo.png" width="100px" height="50px" alt="" />
                                                        </a>
                                                        </td>
                                                        <td>
                                                            <a href="http://www.digitalindia.gov.in/">
                                                                <img src="Images/di.png" width="100px" height="50px" alt="" />
                                                            </a>
                                                        </td>
                                                    </tr>
                                                </table>

                                            </td>--%>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
        </center>
       

    </form>
</body>
</html>
