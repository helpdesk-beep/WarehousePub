<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Godown_ChoiceFillingJvs.aspx.cs" Inherits="JointVentureScheme_Godown_ChoiceFillingJvs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Offer Godown Capacity</title>
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

        .style4 {
            height: 20px;
        }

        .style5 {
            height: 46px;
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

        .style6 {
            color: #FF3300;
            font-weight: bold;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <div id="bg" style="background-color: White">
            <div class="wrap">
                <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></cc1:ToolkitScriptManager>
                <img src="../images/CH.jpg" style="width: 100%" alt="" height="160" />

                <table style="width: 100%" height="160">

                    <tr>
                         <td  style="background-color: #008CBA; width:70PX ; " align="center"> 
                             <asp:LinkButton ID="link1" Text="Home" runat="server" PostBackUrl="~/JointVentureScheme/WarehouseHome.aspx" ForeColor="White"></asp:LinkButton>
                            </td>
                        <td colspan="4" style="border-color: #008CBA; border-style: solid; border-width: 2px; background-color: White" >
                            <p style="font-size: 16px; text-align:center;" class="style6">
                                Category Choice Filling For JVS Scheme 2022-23
                            </p>
                            <%--<asp:LinkButton  style="background-color: #008CBA; width:70px; text-align:left;"  ID="LinkButton7" runat="server" OnClick="LinkButton7_Click" ForeColor="White">Log out</asp:LinkButton>--%>
                        </td>
                        <td style="background-color: #008CBA; width:70px;" align="center" >
                            <asp:LinkButton ID="LinkButton7" runat="server" OnClick="LinkButton7_Click" ForeColor="White">Log out</asp:LinkButton></td>

                    </tr>


                    <tr>
                        <td>
                            <asp:Label ID="lblDist" runat="server" Visible="false"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblbranch" runat="server" Visible="false"></asp:Label>
                        </td>
                    </tr>



                    <tr>
                        <td colspan="6" style="border-color: #008CBA; border-style: solid; border-width: 2px; background-color: White" align="Center">
                            <p style="font-size: 16px; color: red; font-weight: 700;">
                                श्रेणी का चयन   करे  
                            </p>
                        </td>

                    </tr>
                    <tr>

                        <td colspan="4" align="center">
                            <%--   <p style="font-weight:600; font-size:20px; color:blue;">  श्रेणी का चयन   करे  </p>--%>
                            <asp:RadioButton ID="RadioButton1" runat="server" GroupName="S" />
                            <asp:Label ID="lvl1" runat="server" Style="font-weight: 600; font-size: 15px; color: green;">श्रेणी "अ"</asp:Label>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 
            <asp:RadioButton ID="RadioButton2" runat="server" GroupName="S" />
                            <asp:Label ID="Label1" runat="server" Style="font-weight: 600; font-size: 15px; color: green;">श्रेणी "ब"</asp:Label>
                        </td>
                    </tr>
                    <tr>
                         <td colspan="4" align="center">
               <asp:Button class="button button2"  ID="btnsubmit" runat="server" Text="Update"  Width="130px" Height="28px"
                   OnClick="btnsubmit_Click"></asp:Button>
            </td>
                    </tr>
                </table>
                <br />

                <%----------------------------------------------------%>

                <%----------------------------------------------------%>

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
            </div>
        </div>
         <asp:HiddenField ID="RedioHiddenField" runat="server" />
    </form>
</body>
</html>
