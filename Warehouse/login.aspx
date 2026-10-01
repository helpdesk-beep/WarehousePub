<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>

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

    <script type="text/javascript">
        function ShowHide(val) {
            var lblShowHide = document.getElementById('<% = lbl_Depot.ClientID %>');
            var lblShowHideddl = document.getElementById('<% = DDL_Depot.ClientID %>');
            if (val == 1) {
                lblShowHide.style.visibility = 'visible';
                lblShowHideddl.style.visibility = 'visible';
            }
            else {
                lblShowHide.style.visibility = 'hidden';
                lblShowHideddl.style.visibility = 'hidden';

            }
        }
    </script>
    <%--Updated By Ashutosh--%>
    <script src="<%= ResolveUrl("~/NEW_CSS/js/jquery-1.12.4.js") %>"></script>
    <link href="assets/New/css/select2.min.css" rel="stylesheet" />
    <script type="text/javascript" src="assets/New/js/select2.min.js"></script>
    <script type="text/javascript">
        function initSelect2() {
            $("[id*=DDL_Dist]").select2();
            $("[id*=DDL_Branch]").select2();
            $("[id*=ddllogintype]").select2();
            $("[id*=ddl_godown]").select2();
            $("[id*=ddlregion]").select2();
            $("[id*=DDL_Depot]").select2();
        }

        $(document).ready(function () {
            initSelect2();
        });
        <%--function processKeystroke(e) {
            var hdn = document.getElementById('<%= hdnActualPassword.ClientID %>');

            var txt = document.getElementById('<%= txt_password.ClientID %>');
            var charCode = (e.which) ? e.which : e.keyCode;
            
            // Handle Backspace (keyCode 8)
            if (charCode == 8) {
                hdn.value = hdn.value.slice(0, -1);
                return true;
            }

            // Capture actual character and store in hidden field
            var actualChar = String.fromCharCode(charCode);
            hdn.value += actualChar;

            // Generate a random character for the visible textbox
            var randomChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*";
            var randomChar = randomChars.charAt(Math.floor(Math.random() * randomChars.length));

            // Manually add the random character to the textbox
            txt.value += randomChar;

            // Prevent the actual character from appearing
            return false;
        }--%>
        function processKeystroke(e) {
            var hdn = document.getElementById('<%= hdnActualPassword.ClientID %>');

            var txt = document.getElementById('<%= txt_password.ClientID %>');
            var charCode = (e.which) ? e.which : e.keyCode;

            // Handle Backspace (keyCode 8)
            if (charCode == 8) {
                hdn.value = hdn.value.slice(0, -1);
                return true;
            }

            // Capture actual character and store in hidden field
            var actualChar = String.fromCharCode(charCode);
            hdn.value = txt.value;

            // Generate a random character for the visible textbox
            var randomChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ!@#$%^&*";
            var randomChar = randomChars.charAt(Math.floor(Math.random() * randomChars.length));

            // Manually add the random character to the textbox
            txt.value = "ABCDEFGHIJKL!@#$%^&*";

            // Prevent the actual character from appearing
            return false;
        }
    </script>
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
                            <td align="center">
                                <img src="Images/CH.jpg" style="width: 100%" alt="" height="160" />
                            </td>
                        </tr>
                        <tr style="background-color: Gray">
                            <td style="height: 1px"></td>
                        </tr>
                        <tr>
                            <td align="center" style="height: 430px">
                                <center>
                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                                        <ProgressTemplate>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
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
                                                                <asp:ListItem Selected="True" Value="1">Mpwlc</asp:ListItem>
                                                                <asp:ListItem Value="2">Region</asp:ListItem>
                                                                <asp:ListItem Value="3">State</asp:ListItem>
                                                                <%-- <asp:ListItem Value="4">Collector</asp:ListItem>--%>
                                                                <asp:ListItem Value="4">District</asp:ListItem>
                                                                <asp:ListItem Value="5">Admin</asp:ListItem>
                                                                <asp:ListItem Value="6">Others</asp:ListItem>

                                                            </asp:RadioButtonList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 5px" colspan="2" align="center">
                                                            <asp:RadioButton ID="RadioButton1" runat="server" Checked="True" Font-Bold="True" ForeColor="#3366CC" GroupName="br" Text="Branch" AutoPostBack="True" OnCheckedChanged="RadioButton1_CheckedChanged" />
                                                            <asp:RadioButton ID="RadioButton2" runat="server" Font-Bold="True" ForeColor="#3366CC" GroupName="br" Text="District" AutoPostBack="True" OnCheckedChanged="RadioButton2_CheckedChanged" />

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
                                                                ForeColor="Navy" Text="Login Type:" Visible="False"></asp:Label>

                                                        </td>
                                                        <td align="left">

                                                            <asp:DropDownList ID="ddllogintype" runat="server"
                                                                CssClass="tb6" Font-Bold="true" ForeColor="Navy" Height="30px"
                                                                Width="205px" Visible="False" AutoPostBack="True" OnSelectedIndexChanged="ddllogintype_SelectedIndexChanged">
                                                            </asp:DropDownList>

                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" style="height: 10px">&nbsp;</td>
                                                    </tr>
                                                    <tr runat="server" id="regionblock">
                                                        <td align="center">

                                                            <asp:Label ID="lbllogintype0" runat="server" Font-Bold="True" Font-Size="12pt"
                                                                ForeColor="Navy" Text="Region:"></asp:Label>

                                                        </td>
                                                        <td style="height: 10px" align="left">
                                                            <asp:DropDownList ID="ddlregion" runat="server" AutoPostBack="True"
                                                                CssClass="tb6" Font-Bold="true" ForeColor="Navy" Height="30px" Visible="True"
                                                                Width="205px" OnSelectedIndexChanged="ddlregion_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" style="height: 10px">&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" style="width: 100px;">
                                                            <asp:Label ID="lbl_dist" runat="server" Font-Bold="True" ForeColor="Navy" Text="District:"
                                                                Font-Size="12pt"></asp:Label>
                                                        </td>
                                                        <td align="left" style="width: 100px;">
                                                            <asp:DropDownList ID="DDL_Dist" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDL_Dist_SelectedIndexChanged"
                                                                Width="205px" Height="30px" Font-Bold="true" ForeColor="Navy" CssClass="tb6">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 10px" colspan="2"></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center">
                                                            <asp:Label ID="lbl_Depot" runat="server" ForeColor="Navy" Text="Branch:" Font-Bold="True"
                                                                Font-Size="12pt"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:DropDownList ID="DDL_Depot" runat="server" Width="205px" Height="30px" Font-Bold="true"
                                                                ForeColor="Navy" CssClass="tb6" OnSelectedIndexChanged="DDL_Depot_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center">&nbsp;</td>
                                                        <td align="left">&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center">
                                                            <asp:Label ID="lbl_godown" runat="server" Font-Bold="True" Font-Size="12pt" ForeColor="Navy" Text="Godown/Silo:" Visible="False"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:DropDownList ID="ddl_godown" runat="server" AutoPostBack="true" CssClass="tb6" Font-Bold="true" ForeColor="Navy" Height="30px" Visible="False" Width="205px" OnSelectedIndexChanged="ddl_godown_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 10px" colspan="2"></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center">
                                                            <asp:Label ID="Label3" runat="server" ForeColor="Navy" Text="Password:" Font-Bold="True"
                                                                Font-Size="12pt"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txt_password" AutoComplete="off" runat="server" TextMode="Password" Width="200px" Height="20px" onblur="return processKeystroke(event);"
                                                                Font-Bold="true" ForeColor="Navy" Font-Size="12pt" MaxLength="50" CssClass="tb6"></asp:TextBox>
                                                            <asp:HiddenField ID="hdnActualPassword" runat="server" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_password"
                                                                ErrorMessage="Password Can not be blank" Font-Bold="True" ValidationGroup="1">*</asp:RequiredFieldValidator>
                                                        </td>
                                                        <%-- <td align="left">
                                                            <asp:TextBox ID="txt_password" AutoComplete="off" runat="server" TextMode="Password" Width="200px" Height="20px" onkeypress="return processKeystroke(event);"
                                                                Font-Bold="true" ForeColor="Navy" Font-Size="12pt" MaxLength="50" CssClass="tb6"></asp:TextBox>
                                                            <asp:HiddenField ID="hdnActualPassword" runat="server" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_password"
                                                                ErrorMessage="Password Can not be blank" Font-Bold="True" ValidationGroup="1">*</asp:RequiredFieldValidator>
                                                        </td>--%>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 10px" colspan="2"></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center">
                                                            <asp:Label ID="lbl_code" runat="server" Font-Bold="True" Font-Size="12pt" ForeColor="Navy" Text="Enter Display Code:"></asp:Label>
                                                        </td>
                                                        <td align="left">

                                                            <asp:TextBox ID="txtCaptcha" runat="server" CssClass="tb6" Font-Bold="true" Font-Size="12pt" ForeColor="Navy" Height="20px" MaxLength="50" Width="200px"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtCaptcha" ErrorMessage="Please Fill Captcha" ValidationGroup="1">*</asp:RequiredFieldValidator>

                                                            <asp:Image ID="imgCaptcha" runat="server" Style="width: 120px; height: 25px;" />
                                                            <asp:ImageButton ID="btnRefresh" runat="server" ImageUrl="~/Images/ref.png" OnClick="btnRefresh_Click" Width="20px" />

                                                            <%--<asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="#C00000" Visible="False"></asp:Label>--%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center">
                                                            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="#C00000" Font-Size="7pt" Text="(Captcha Code is Case Sensitive)"></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="lbl_cmsg" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="#C00000"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" style="height: 10px"></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" colspan="2">
							                                                            <asp:Button ID="btn_login" runat="server" Text="Login" OnClick="btn_login_Click"
                                                                ValidationGroup="1" ToolTip="Click for Login" CssClass="myButton" />
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
                                                    <tr>
                                                        <td align="center" colspan="2">
                                                            <asp:CheckBox ID="CheckBox1" runat="server" Text="Hindi Version" Font-Size="10pt"
                                                                Font-Bold="true" ForeColor="Navy" />
                                                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                         <img src="images/pdflogo.png" style="width: 21px" alt="" height="21px" />
                                                            <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/UserManual/Godown Mapping menual.pdf"
                                                                Font-Size="10pt" Font-Bold="true" Target="_blank" ForeColor="Navy">UserManual for Godown longitude latitude entry</asp:HyperLink>

                                                        </td>
                                                    </tr>
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
                                        <tr>
                                            <td style="height: 20px;" colspan="5">
                                                <img id="Img2" src="Images/line.png" height="30px" width="100%" alt="" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 20%" align="center">
                                                <a href="http://www.mp.nic.in/">
                                                    <img src="Images/NIC-logo.png" width="200px" height="50px" alt="" />
                                                </a>
                                            </td>
                                            <td style="width: 1%" align="center">
                                                <img id="Img1" src="Images/Linevertical.png" style="width: 20px; height: 50px" alt="" />
                                            </td>
                                            <td style="color: Navy; font-size: 8pt; width: 58%;" align="center">
                                                <b>© 2011 &nbsp;National Informatics Centre.All Rights Reserved
                                                <br />
                                                    Developed By : National Informatics Centre
                                                <br />
                                                    Madhya Pradesh, Ministry of Communications and Information Technology</b>
                                            </td>
                                            <td style="width: 1%" align="center">
                                                <img id="Logo" src="Images/Linevertical.png" style="width: 20px; height: 50px" alt="" />
                                            </td>
                                            <td style="width: 20%" align="center">
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

                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
        </center>
        <triggers>
            <asp:PostBackTrigger ControlID="btn_login" />
        </triggers>

    </form>
</body>
</html>
