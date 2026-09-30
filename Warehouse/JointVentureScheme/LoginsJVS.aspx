<%@ Page Language="C#" AutoEventWireup="true" CodeFile="~/JointVentureScheme/Logins.aspx.cs" Inherits="JointVentureScheme_Logins" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="urf-8" />
    <meta name="viewport" content="width=device-width" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <%--<link rel="stylesheet" href="css/main.css" type="text/css" />--%>
    <%--<link rel="stylesheet" href="css/main.css" type="text/css" />--%>
    <script type="text/javascript" src="js/jquery-1.4.1.min.js"></script>
    <script type="text/javascript" src="js/menu.js"></script>
    <script type="text/javascript" src="js/slideshow.js"></script>
    <script type="text/javascript" src="js/cufon-yui.js"></script>
    <script type="text/javascript" src="js/Arial.font.js"></script>
    <title>Admin Login</title>
    <style type="text/css">
        .wrap {
            margin: 0 auto;
            width: 960px;
            -moz-box-shadow: 0px 5px 23px #000;
            -webkit-box-shadow: 0px 5px 23px #000;
            box-shadow: 0px 5px 23px #000;
        }

        input.submit {
            color: #fff;
            padding: 7px 10px;
            border: 0;
            font-weight: bold;
            background: #777;
        }

        input.text {
            border: 2px solid rgb(173, 204, 204);
            height: 20px;
            width: 223px;
            font-size: 16px;
            box-shadow: 0px 0px 27px rgb(204, 204, 204) inset;
            transition: 500ms all ease;
            padding: 3px 3px 3px 3px;
        }
    </style>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#Panel1").hide();
            $("#Button1").click(function () {
                $("#Panel1").show();
            });
        });
    </script>
    <script type="text/javascript">
        function processKeystroke(e) {
            var hdn = document.getElementById('<%= hdnActualPassword.ClientID %>');
            var txt = document.getElementById('<%= txtlogpwd.ClientID %>');
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
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </cc1:ToolkitScriptManager>
        <div id="bg">
            <div class="wrap">
                <img src="../images/CH.jpg" style="width: 100%" alt="" height="160" />
                <br />
                <center>
                    <div style="width: 100%;">
                        <br />
                        <table align="center" style="width: 500px; border: 10px solid #cb4e48; background-color: #EBEBEB;">
                            <tr>
                                <td align="center" colspan="2" valign="middle" style="background-color: #007ACC">
                                    <span style="color: White; font-weight: bolder; font-size: 15pt">Login</span>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    <br />
                                    <asp:RadioButton ID="rbDSO" runat="server" Text="DSO" Checked="false" GroupName="rb" Font-Bold="True" ForeColor="Blue" AutoPostBack="True" OnCheckedChanged="rbDSO_CheckedChanged"></asp:RadioButton>
                                    <asp:RadioButton ID="rdoBranch" runat="server"
                                        Text="Branch" Checked="True" GroupName="rb" Font-Bold="True" ForeColor="Blue"
                                        AutoPostBack="True" OnCheckedChanged="rdoBranch_CheckedChanged"></asp:RadioButton>

                                    &nbsp;&nbsp;
                                                <asp:RadioButton ID="rbbranch" runat="server" Text="District" Checked="false" GroupName="rb" Font-Bold="True" ForeColor="Blue" AutoPostBack="True" OnCheckedChanged="rbbranch_CheckedChanged"></asp:RadioButton>&nbsp;
                                                <asp:RadioButton ID="rbrm" runat="server" Text="Region" GroupName="rb" Font-Bold="True" ForeColor="Blue" AutoPostBack="True" OnCheckedChanged="rbrm_CheckedChanged"></asp:RadioButton>&nbsp;&nbsp;&nbsp;<asp:RadioButton
                                                    ID="rbHo" runat="server" Text="State" GroupName="rb" Font-Bold="True"
                                                    ForeColor="Blue" AutoPostBack="True" OnCheckedChanged="rbHo_CheckedChanged"></asp:RadioButton>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">----------------------------------------------------------------------------------------
                                </td>
                            </tr>
                            <tr runat="server" id="trregion">
                                <td>Region:</td>
                                <td>

                                    <asp:DropDownList ID="ddlregion" runat="server"
                                        CssClass="tb6" Font-Bold="true" ForeColor="Navy" Height="30px" Visible="True"
                                        Width="205px">
                                    </asp:DropDownList>

                                </td>
                            </tr>
                            <tr runat="server" id="trdest">
                                <td>District:</td>
                                <td>

                                    <asp:DropDownList ID="DDL_Dist" runat="server" AutoPostBack="true"
                                        Width="205px" Height="30px" Font-Bold="true" ForeColor="Navy" CssClass="tb6" OnSelectedIndexChanged="DDL_Dist_SelectedIndexChanged">
                                    </asp:DropDownList>

                                </td>
                            </tr>
                            <tr runat="server" id="trBranch">
                                <td>Branch:</td>
                                <td>

                                    <asp:DropDownList ID="DDL_Branch" runat="server" AutoPostBack="false"
                                        Width="205px" Height="30px" Font-Bold="true" ForeColor="Navy" CssClass="tb6">
                                    </asp:DropDownList>

                                </td>
                            </tr>
                            <tr>
                                <td>Password:
                                </td>
                                <td>
                                    <input id="txtlogpwd" name="rname" class="text" runat="server" placeholder="Password" type="password" tabindex="10" style="width: 200px; height: 28px;" />
                                    <asp:HiddenField ID="hdnActualPassword" runat="server" />
                                    <span style="color: #FF0000; font-size: medium;">*</span>
                                </td>
                            </tr>

                            <tr>
                                <td></td>
                                <td>
                                    <%--<input id="Submit1" type="Submit" class="submit"  value="Submit"  />--%>
                                    <asp:Button ID="btnlogin" runat="server" Text="Login" class="submit" TabIndex="11" ValidationGroup="l" OnClick="btnlogin_Click"></asp:Button>
                                </td>
                            </tr>
                            <%--<tr>
                        <td colspan="2"><input id="Button1" type="button" class="submit" value="Forget Password" tabindex="16" />
                        </td>
                    </tr>--%>
                        </table>
                        <br />
                        <br />
                    </div>
                    <div style="background-image: url('../images/div_bg.png')">
                        <table style="width: 100%">
                            <tr>
                                <td style="height: 20px;" colspan="5">
                                    <img id="Img2" src="../Images/line.png" height="30px" width="100%" alt="" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%" align="center">
                                    <a href="http://www.mp.nic.in/">
                                        <img src="../Images/NIC-logo.png" width="200px" height="50px" alt="" />
                                    </a>
                                </td>
                                <td style="width: 1%" align="center">
                                    <img id="Img1" src="../Images/Linevertical.png" style="width: 20px; height: 50px" alt="" />
                                </td>
                                <td style="color: Navy; font-size: 8pt; width: 58%;" align="center">
                                    <b>© 2015 &nbsp;National Informatics Centre.All Rights Reserved
                                                    <br />
                                        Developed By : National Informatics Centre
                                                    <br />
                                        Madhya Pradesh, Ministry of Communications and Information Technology</b>
                                </td>
                                <td style="width: 1%" align="center">
                                    <img id="Logo" src="/../Images/Linevertical.png" style="width: 20px; height: 50px" alt="" />
                                </td>
                                <td style="width: 20%" align="center">
                                    <table>
                                        <tr>
                                            <td><a href="http://india.gov.in/">
                                                <img src="../Images/natindialogo.png" width="100px" height="50px" alt="" />
                                            </a>
                                            </td>
                                            <td>
                                                <a href="http://www.digitalindia.gov.in/">
                                                    <img src="../Images/di.png" width="100px" height="50px" alt="" />
                                                </a>
                                            </td>
                                        </tr>
                                    </table>

                                </td>
                            </tr>
                        </table>
                    </div>
                </center>
            </div>
            <br />
        </div>
    </form>
</body>
</html>
