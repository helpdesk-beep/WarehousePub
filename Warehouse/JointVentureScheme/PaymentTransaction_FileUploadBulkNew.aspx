<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PaymentTransaction_FileUploadBulkNew.aspx.cs" Inherits="JointVentureScheme_PaymentTransaction_FileUploadBulkNew" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Warehouse Home</title>
    <meta charset="urf-8" />
    <meta name="viewport" content="width=device-width" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <%--<link href="css/bootstrap.css" rel="stylesheet"/>
<link href="css/bootstrap-responsive.css" rel="stylesheet"/>--%>
    <link rel="stylesheet" href="css/main.css" type="text/css" />
    <script type="text/javascript" src="js/jquery-1.4.1.min.js"></script>
    <script type="text/javascript" src="js/menu.js"></script>
    <script type="text/javascript" src="js/slideshow.js"></script>
    <script type="text/javascript" src="js/cufon-yui.js"></script>
    <script type="text/javascript" src="js/Arial.font.js"></script>
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
                background-color: #2FBDF1;
                height: 30px;
                color: White;
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
            border: 2px solid #E47D21;
        }

            .button6:hover {
                background-color: #E47D21;
                color: white;
            }

        .button4 {
            background-color: white;
            color: black;
            border: 2px solid #FC00B3;
        }

            .button4:hover {
                background-color: #FC00B3;
                color: white;
            }

        .button5 {
            background-color: white;
            color: black;
            border: 2px solid #CBE555;
        }

            .button5:hover {
                background-color: #CBE555;
                color: white;
            }

        .button7 {
            background-color: white;
            color: black;
            border: 2px solid #AEB6BF;
        }

            .button7:hover {
                background-color: #AEB6BF;
                color: white;
            }

        .button8 {
            background-color: white;
            color: black;
            border: 2px solid #F4D03F;
        }

            .button8:hover {
                background-color: #F4D03F;
                color: white;
            }

        .button9 {
            background-color: white;
            color: black;
            border: 2px solid #117A65;
        }

            .button9:hover {
                background-color: #117A65;
                color: white;
            }
    </style>
</head>
<body>
    <div>

        <div id="bg" style="background-color: White">
            <div class="wrap">
                <img src="../images/CH.jpg" style="width: 100%" alt="" height="160" />
                <form id="form1" runat="server">
                    <table style="width: 100%">
                        <tr>
                            <td colspan="4" style="font-size: medium; width: 1000px;">
                                <table style="width: 100%; height: 32px; font-size: medium;">
                                    <tr>
                                        <td style="background-color: #008CBA; width: 70PX;" align="center">
                                            <asp:LinkButton ID="LinkButton10" Text="Home" runat="server" PostBackUrl="~/JointVentureScheme/BranchHome.aspx" ForeColor="White"></asp:LinkButton>
                                        </td>
                                        <td colspan="2" style="background-color: #008CBA; font-size: medium; color: White; width: 100px" align="center">Welcome&nbsp;<asp:Label ID="lbluser" runat="server" ForeColor="White"></asp:Label></td>
                                        <td style="background-color: #008CBA; width: 70px;" align="center">
                                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" ForeColor="White">Log out</asp:LinkButton></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>

                        <tr>
                            <td colspan="4" style="border-color: #008CBA; border-style: solid; border-width: 2px; background-color: White; width: 100%;" align="center">
                                <p style="font-size: 14px; color: Black;">
                                    File Upload For Payment Status
                                </p>
                            </td>
                        </tr>

                        <tr>
                            <td colspan="4" align="center">
                                <asp:Label ID="lblmsg" Text="" ForeColor="red" Font-Bold="true" runat="server"></asp:Label>
                            </td>
                        </tr>

                        <tr>

                            <td colspan="4" align="center">File Upload :
                                    <asp:FileUpload ID="upFile" runat="server" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="upFile" ValidationGroup="A" ErrorMessage="* required" ForeColor="Red" runat="server"></asp:RequiredFieldValidator>

                            </td>
                        </tr>

                        <tr>
                            <td colspan="4" align="center">
                                <asp:Button ID="btnUpload" runat="server" Text="Upload" Visible="true" class="button button2" Width="150px" Height="30px" OnClick="btnUpload_Click" ValidationGroup="A" />
                            </td>
                        </tr>
                    </table>
                    <div>
                    </div>
                </form>
            </div>
        </div>
    </div>
</body>
</html>
