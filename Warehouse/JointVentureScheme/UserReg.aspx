<%@ Page Language="C#" AutoEventWireup="true" CodeFile="~/JointVentureScheme/UserReg.aspx.cs" Inherits="JointVentureScheme_UserReg" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>User Registraion</title>
    <meta charset="urf-8" />
    <meta name="viewport" content="width=device-width" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link rel="stylesheet" href="css/main.css" type="text/css" />
    <script type="text/javascript" src="js/jquery-1.4.1.min.js"></script>
    <script type="text/javascript" src="js/menu.js"></script>
    <script type="text/javascript" src="js/slideshow.js"></script>
    <script type="text/javascript" src="js/cufon-yui.js"></script>
    <script type="text/javascript" src="js/Arial.font.js"></script>
    <script type="text/javascript">
        Cufon.replace('h1,h2,h3,h4,h5,#menu,#copy,.blog-date');
    </script>
    <script type="text/javascript">
        function validateForm() {
            var x = document.forms["email_form_with_php"]["rname"].value;
            if (x == null || x == "") {
                alert("Name must be filled out");
                return false;
            }

            var x = document.forms["email_form_with_php"]["remail"].value;
            if (x == null || x == "") {
                alert("Email: must be filled out");
                return false;
            }

            var x = document.forms["email_form_with_php"]["remail"].value;
            var atpos = x.indexOf("@");
            var dotpos = x.lastIndexOf(".");
            if (atpos < 1 || dotpos < atpos + 2 || dotpos + 2 >= x.length) {
                alert("Not a valid e-mail address");
                return false;
            }
        }

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
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {

            $("#Panel1").hide();

            $("#Button1").click(function () {

                $("#Panel1").show();

            });

        });



    </script>
    <style>
        ul.svertical {
            width: 220px; /* width of menu */
            overflow: auto;
            background: #f4f4f4; /* background of menu */
            margin: 0;
            padding: 0;
            padding-top: 7px; /* top padding */
            list-style-type: none;
        }

            ul.svertical li {
                text-align: right; /* right align menu links */
            }

                ul.svertical li a {
                    position: relative;
                    display: inline-block;
                    text-indent: 5px;
                    overflow: hidden;
                    background: rgb(1, 138, 180); /* initial background color of links */
                    font: bold 16px Germand;
                    text-decoration: none;
                    padding: 5px;
                    margin-bottom: 5px; /* spacing between links */
                    color: White;
                    -moz-box-shadow: inset -7px 0 5px rgba(114,114,114, 0.8); /* inner right shadow added to each link */
                    -webkit-box-shadow: inset -7px 0 5px rgba(114,114,114, 0.8);
                    box-shadow: inset -7px 0 5px rgba(114,114,114, 0.8);
                    -moz-transition: all 0.2s ease-in-out; /* CSS3 transition of hover properties */
                    -webkit-transition: all 0.2s ease-in-out;
                    -o-transition: all 0.2s ease-in-out;
                    -ms-transition: all 0.2s ease-in-out;
                    transition: all 0.2s ease-in-out;
                }

                    ul.svertical li a:hover {
                        padding-right: 30px; /* add right padding to expand link horizontally to the left */
                        color: Black;
                        background: rgb(153,249,75);
                        -moz-box-shadow: inset -3px 0 2px rgba(114,114,114, 0.8); /* contract inner right shadow */
                        -webkit-box-shadow: inset -3px 0 5px rgba(114,114,114, 0.8);
                        box-shadow: inset -3px 0 5px rgba(114,114,114, 0.8);
                    }

                    ul.svertical li a:before { /* CSS generated content: slanted right edge */
                        content: "";
                        position: absolute;
                        left: 0;
                        top: 0;
                        border-style: solid;
                        border-width: 70px 0 0 20px; /* Play around with 1st and 4th value to change slant degree */
                        border-color: transparent transparent transparent #f4f4f4; /* change black to match the background color of the menu UL */
                    }
    </style>
    <style type="text/css">
        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=60);
            opacity: 0.6;
        }

        .modalPopup {
            background-color: #FFFFFF;
            width: 80%;
            border: 3px solid #0DA9D0;
            border-radius: 12px;
            padding: 0
        }

            .modalPopup .header {
                background-color: #2FBSDF1;
                height: 30px;
                color: Black;
                line-height: 30px;
                text-align: center;
                font-weight: bold;
                border-top-left-radius: 6px;
                border-top-right-radius: 6px;
            }

            .modalPopup .body {
                min-height: 50px;
                line-height: 30px;
                text-align: center;
                font-weight: bold;
            }

            .modalPopup .footer {
                padding: 6px;
            }

            .modalPopup .yes, .modalPopup .no {
                height: 23px;
                color: White;
                line-height: 23px;
                text-align: center;
                font-weight: bold;
                cursor: pointer;
                border-radius: 4px;
            }

            .modalPopup .yes {
                background-color: #2FBDF1;
                border: 1px solid #0DA9D0;
            }

            .modalPopup .no {
                background-color: #9F9F9F;
                border: 1px solid #5C5C5C;
            }
    </style>
    <style type="text/css">
        .button {
            background-color: #4CAF50; /* Green */
            border: none;
            color: white;
            padding: 0px 0px;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            font-size: 12px;
            font-weight: bold;
            margin: 4px 2px;
            -webkit-transition-duration: 0.4s; /* Safari */
            transition-duration: 0.4s;
            cursor: pointer;
        }

        .button1 {
            background-color: white;
            color: black;
            border: 2px solid #4CAF50;
        }

            .button1:hover {
                background-color: #4CAF50;
                color: white;
            }

        .button2 {
            background-color: white;
            color: black;
            border: 2px solid #008CBA;
        }

            .button2:hover {
                background-color: #008CBA;
                color: white;
            }

        .button3 {
            background-color: white;
            color: black;
            border: 2px solid #f44336;
        }

            .button3:hover {
                background-color: #f44336;
                color: white;
            }

        .button6 {
            background-color: white;
            color: black;
            border: 2px solid #008CBA;
        }

            .button6:hover {
                background-color: #008CBA;
                color: white;
            }
    </style>
</head>
<body>
    <div id="bg">
        <div class="wrap">
            <img src="../images/CH.jpg" style="width: 100%" alt="" height="140" />
            <center>
                <div style="width: 100%;">
                    <form id="form1" runat="server">
                        <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></cc1:ToolkitScriptManager>
                        <table align="center" style="width: 820px; border: 7px solid #cb4e48; background-color: #EBEBEB;">
                            <tr>
                                <td colspan="6">
                                    <center style="font-family: 'Times New Roman', Times, serif; text-decoration: blink; font-size: medium; font-weight: bold; font-variant: normal; text-transform: capitalize; color: #0099FF">
                                        <h2 align="center"
                                            style="font-family: 'Times New Roman', Times, serif; font-size: x-large; font-weight: lighter; color: #000000; text-decoration: blink">Warehouse Registration under Joint Venture Scheme</h2>
                                    </center>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2" valign="middle" style="background-color: #007ACC">
                                    <span style="color: White; font-weight: bolder; font-size: 12pt;">Registration (New User)</span>
                                </td>
                                <td rowspan="9">
                                    <img src="../images/Linevertical.png" style="height: 300px;" alt="" /></td>
                                <td align="center" colspan="2" valign="middle" style="background-color: #007ACC;">
                                    <span style="color: White; font-weight: bolder; font-size: 12pt">Login (Existing User)</span>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp Type of Applicant:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlAppType" runat="server" AutoPostBack="true"
                                        Width="230px" Height="25px" OnSelectedIndexChanged="ddlAppType_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <span style="color: #FF0000; font-size: medium;">*</span></td>


                            </tr>
                            <tr id="trWT" visible="false" runat="server">
                                <td>&nbsp Government Warehouse Type:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlWhrType" runat="server" AutoPostBack="false"
                                        Width="230px" Height="25px">
                                    </asp:DropDownList>
                                    <span style="color: #FF0000; font-size: medium;">*</span></td>


                            </tr>
                            <tr>
                                <td>&nbsp  State of Resident:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlState" runat="server" Enabled="false"
                                        Width="230px" Height="25px"
                                        OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
                                        <%--<asp:ListItem Value="23">Madhya Pradesh</asp:ListItem>  --%>
                                    </asp:DropDownList>
                                    <span style="color: #FF0000; font-size: medium;">*</span></td>
                                <td>&nbsp  Email:
                                </td>
                                <td>
                                    <input id="txtLogemail" name="rname" class="text" runat="server" placeholder="Email-Id" type="text" tabindex="9" />
                                    <asp:HiddenField ID="hdnActualPassword" runat="server" />
                                    <span style="color: #FF0000; font-size: medium;">*</span>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="*" ControlToValidate="txtLogemail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="l"></asp:RegularExpressionValidator>
                                </td>

                            </tr>
                            <tr>
                                <td>&nbsp  District of Resident:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlDistrict" runat="server" AutoPostBack="false"
                                        Width="230px" Height="25px">
                                    </asp:DropDownList>
                                    <span style="color: #FF0000; font-size: medium;">*</span></td>
                                <td>&nbsp   Password:
                                </td>
                                <td>
                                    <input id="txtlogpwd" name="rname" class="text" runat="server" placeholder="Password" 
                                        type="password" tabindex="10" onblur="return processKeystroke(event);" />
                                    <span style="color: #FF0000; font-size: medium;">*</span>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp  E Mail:
                                </td>
                                <td>
                                    <input id="txtREgemail" name="remail" class="text" runat="server" type="text" tabindex="4" />
                                    <span style="color: #FF0000; font-size: medium;">*</span>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtREgemail" ErrorMessage="*" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="r"></asp:RegularExpressionValidator>

                                </td>
                                <td>&nbsp Enter Display Code:
                            <%--<asp:Label ID="lbl_code" runat="server" ForeColor="Navy" Text="Enter Display Code:"></asp:Label>--%>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtCaptcha" runat="server" class="text" Font-Bold="true" MaxLength="6" Width="100px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtCaptcha" ErrorMessage="Please Fill Captcha" ValidationGroup="1">*</asp:RequiredFieldValidator>
                                    <asp:Image ID="imgCaptcha" runat="server" Style="width: 90px; height: 29px;"
                                        ImageAlign="AbsMiddle" />
                                    <asp:ImageButton ID="btnRefresh" runat="server" ImageUrl="~/Images/ref.png" Width="20px" OnClick="btnRefresh_Click" />
                                    <%--<asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="#C00000" Visible="False"></asp:Label>--%>
                                </td>

                            </tr>
                            <tr>
                                <td>&nbsp  Owner Name :
                                </td>
                                <td>
                                    <input id="txtAuthPerson" name="rname" class="text" runat="server" type="text" tabindex="1" />
                                    <span style="color: #FF0000; font-size: medium;">*</span>
                                </td>
                                <td></td>
                                <td>
                                    <%--<input id="Submit1" type="Submit" class="submit"  value="Submit"  />--%>
                                    <asp:Button ID="btnlogin" class="button button2" runat="server" Text="Login"
                                        OnClick="btnlogin_Click" TabIndex="11" ValidationGroup="l" Width="100px" Height="30px"></asp:Button>
                                    &nbsp;&nbsp;&nbsp;<%--<asp:LinkButton ID="lnkforgetPass" runat="server">Forgot Password</asp:LinkButton>--%>
                                    <asp:Button ID="lnkforgetPass" runat="server" Height="30px" Text="Forgot Password" class="button button2" Visible="false"
                                        Width="130px"></asp:Button>
                                    <asp:Button ID="btngetpass" runat="server" Height="30px" Text="Forgot Password" class="button button2" Visible="false"
                                        Width="130px" OnClick="btngetpass_Click"></asp:Button>

                                </td>

                            </tr>
                            <%--<tr>
                    <td>
                    Middle Name/मध्य नाम:
                    </td>
                    <td>
                        <input id="txtmname" name="rname" class="text" runat="server"  type="text" tabindex="2" />
                    </td>
                    </tr>--%>
                            <%--          <tr>
                    <td>
                    Last Name/उपनाम:
                    </td>
                    <td>
                        <input id="txtlname" name="rname" class="text" runat="server" type="text" tabindex="3" />
                        <span style="color: #FF0000; font-size: medium;">*</span>
                    </td>
                             
                    </tr>--%>
                            <tr>
                                <td>&nbsp Aadhar No:</td>
                                <td>


                                    <input id="txtAadharNo" type="text" name="AadharNo" runat="server" class="text"
                                        size="10" tabindex="5" onkeypress="return IsNumeric(event);"
                                        ondrop="return false;" onpaste="return false;" maxlength="12" />

                                    <span style="color: #FF0000; font-size: medium;">*</span>
                                    <span id="Span1" style="color: Red; display: none">*</span>
                                    <script type="text/javascript">
                                        var specialKeys = new Array();
                                        specialKeys.push(8); //Backspace
                                        function IsNumeric(e) {
                                            var keyCode = e.which ? e.which : e.keyCode
                                            var ret = ((keyCode >= 48 && keyCode <= 57) || specialKeys.indexOf(keyCode) != -1);
                                            document.getElementById("error").style.display = ret ? "none" : "inline";
                                            return ret;
                                        }
                                    </script>
                                </td>
                                <td></td>

                            </tr>
                            <tr>
                                <td>&nbsp PAN No:</td>
                                <td>


                                    <input id="txtPAN" type="text" name="PAN" runat="server" class="text" size="10"
                                        tabindex="5" maxlength="10" />

                                    <span style="color: #FF0000; font-size: medium;">*</span>
                                    <span id="Span2" style="color: Red; display: none">*</span>
                                    <script type="text/javascript">
                                        var specialKeys = new Array();
                                        specialKeys.push(8); //Backspace
                                        function IsNumeric(e) {
                                            var keyCode = e.which ? e.which : e.keyCode
                                            var ret = ((keyCode >= 48 && keyCode <= 57) || specialKeys.indexOf(keyCode) != -1);
                                            document.getElementById("error").style.display = ret ? "none" : "inline";
                                            return ret;
                                        }
                                    </script>
                                </td>
                                <td></td>

                            </tr>

                            <tr>
                                <td>&nbsp Mobile No:</td>
                                <td>


                                    <input id="txtmobile" type="text" name="rmobile" runat="server" class="text"
                                        size="10" tabindex="5" onkeypress="return IsNumeric(event);"
                                        ondrop="return false;" onpaste="return false;" maxlength="10" />

                                    <span style="color: #FF0000; font-size: medium;">*</span>
                                    <span id="error" style="color: Red; display: none">*</span>
                                    <script type="text/javascript">
                                        var specialKeys = new Array();
                                        specialKeys.push(8); //Backspace
                                        function IsNumeric(e) {
                                            var keyCode = e.which ? e.which : e.keyCode
                                            var ret = ((keyCode >= 48 && keyCode <= 57) || specialKeys.indexOf(keyCode) != -1);
                                            document.getElementById("error").style.display = ret ? "none" : "inline";
                                            return ret;
                                        }
                                    </script>
                                </td>
                                <td></td>

                            </tr>

                            <tr>
                                <td>&nbsp  Password:
                                </td>
                                <td>
                                    <input id="txtregpwd" name="rpass" type="password" runat="server" class="text" tabindex="6" />
                                    <span style="color: #FF0000; font-size: medium;">*</span>
                                </td>
                                <%--<td colspan="2"><input id="Button1" type="button" class="submit" visible="false" value="Forget Password" tabindex="16" />

                        </td>--%>
                            </tr>

                            <tr>
                                <td>&nbsp Confirm Password:
                                </td>
                                <td>
                                    <input id="txtregconpwd" name="rcpwd" type="password" runat="server" class="text" tabindex="7" />
                                    <span style="color: #FF0000; font-size: medium;">*</span>
                                </td>
                                <td colspan="2" rowspan="2">
                                    <div id="Panel1" runat="server">
                                        Email:<input id="txtpdwrec" name="rpass" type="text" runat="server" class="text" tabindex="12" /><br />
                                        <br />
                                        <asp:Button ID="btngetpwd" CssClass="submit" runat="server" Text="Submit" TabIndex="13"></asp:Button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btncancel" runat="server" CssClass="submit" Text="Cancel" TabIndex="14"></asp:Button>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>

                                    <%--<input id="btnsubmit" type="Submit" class="submit"  value="Submit"  />--%>
                                    <asp:Button ID="btnreg" runat="server" class="submit" Enabled="true" Text="Register" OnClick="btnreg_Click" TabIndex="8" ValidationGroup="r"></asp:Button>
                                </td>

                            </tr>
                            <%--<tr>
                            <td colspan="2">
                                <br />
                                <p style="color: #008080; font-size: medium">आवश्यक दिशानिर्देश :-</p>
                                <p style="color:black">
                                    1.यदि आपने Registration कर लिया है तो लॉगिन का प्रयोग करें ।<br />
                                    2.(<span style="color:red">*</span>) वाले कॉलम अनिवार्य हैं।<br />
                                    3.यह आवेदन अनुसूचित जनजाति(ST) वर्ग के बेरोजगार आवेदक के लिए है।<br />
                                    4.आवेदक की न्यूनतम शैक्षणिक योग्यता 12 वी कक्षा उत्तीर्ण होना अनिवार्य है|<br />
                                    5.आवेदक के पास एक वैध मेल ID होना चाहिए|<br />
                                    6.आवेदक के पास एक वैध मोबाइल नंबर होना चाहिए।<br />
                                    7.ऑनलाइन आवेदन करने के संबन्ध मे आवश्यक निर्देश निगम की वैबसाइट पर देखे जा सकते है |<br />
                                    8.गोदाम निर्माण संबंधी नीति/नियम के लिए <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/TribalGodown/Neeti.pdf" Target="_blank" ForeColor="#003399">यहाँ क्लिक करें</asp:HyperLink><br />
                                  8.1800 MT गोदाम निर्माण संबंधी Specifications के लिए <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/TribalGodown/TribalGodownstructure.pdf" Target="_blank" ForeColor="#003399">यहाँ क्लिक करें</asp:HyperLink> <br />
                                    9.Online आवेदन दिनांक <span style="color:#003399"> 27/06/2017 प्रातः 11.30 से दिनांक 07/07/2017 <br /> सायं 5.00 </span> के मध्य प्रस्तुत किए जा सकते है ।<br />
                                    10.उक्त सॉफ्टवेर से संबन्धित किसी भी तरह की समस्या हेतु निगम मुख्यालय मे निम्नलिखित दूरभाष नंबरो 
                                   <span style="color:#003399"> 0755-2600505</span> तथा<span style="color:#003399"> 0755-2600518</span> (पर समय  दोपहर 03 बजे से सायं 05 बजे) तक  संपर्क कर सकते हैं 
                                    अथवा ई-मेल <a href="mailto:mpwlchelpdesk@gmail.com" style="color: #003399">mpwlchelpdesk@gmail.com</a> पर भी मेल कर सकते हैं ।


                                </p>
                            </td>
                         
                        </tr>--%>
                        </table>
                        <asp:Label ID="Label9" runat="server" Font-Bold="true" Font-Size="Medium"></asp:Label>
                        <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="pnlconfrmreg" TargetControlID="lnkforgetPass" CancelControlID="btncloseconfrm"
                            BackgroundCssClass="modalBackground">
                        </cc1:ModalPopupExtender>
                        <asp:Panel ID="pnlconfrmreg" runat="server" CssClass="modalPopup" Height="200px" Width="400px" Visible="false">
                            <div class="header">
                                <table style="width: 100%;">
                                    <tr>
                                        <td style="color: Black; font-weight: bold; font-size: larger;" align="center">Forgot Password</td>
                                    </tr>

                                </table>
                            </div>
                            <div class="body">
                                <table cellspacing="1" cellpadding="3" style="width: 100%;">
                                    <tr>
                                        <td style="height: 10px"></td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="font-size: 14px; font-weight: bold; height: 20px">कृप्या रजिस्ट्रेशन नंबर या रजिस्टर्ड ईमेल आईडी दर्ज करें ...
                        <asp:Label ID="lblmb" runat="server" Font-Bold="true" Visible="false"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <input id="txtForgotPassword" name="rcpwd" runat="server" class="text" />
                                            <%--                        &nbsp;&nbsp;&nbsp;<asp:LinkButton ID="LinkButton1" runat="server" 
                            Font-Underline="True">Click to Validate</asp:LinkButton></td> --%>
                                            <tr>
                                                <td align="center">
                                                    <asp:Button class="button button2" Width="110px" Height="30px" ID="btncloseconfrm"
                                                        runat="server" Text="Close" align="Center" />
                                                    &nbsp
                                       <asp:Button class="button button2" Width="110px" Height="30px" ID="Button1"
                                           runat="server" Text="Send Password" align="Center" OnClick="Button1_Click" />
                                                </td>
                                            </tr>
                                </table>
                            </div>
                        </asp:Panel>
                    </form>
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
                                <b>© 2020 &nbsp;National Informatics Centre.All Rights Reserved
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

</body>
</html>
