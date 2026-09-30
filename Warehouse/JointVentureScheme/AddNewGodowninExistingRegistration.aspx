<%@ Page Language="C#" AutoEventWireup="true" CodeFile="~/JointVentureScheme/AddNewGodowninExistingRegistration.aspx.cs" Inherits="JointVentureScheme_AddNewGodowninExistingRegistration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Warehouse Registration</title>
    <meta charset="urf-8" />
    <meta name="viewport" content="width=device-width" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <%--<link rel="stylesheet" href="css/main.css" type="text/css" />
    <script type="text/javascript" src="js/jquery-1.4.1.min.js"></script>
    <script type="text/javascript" src="js/menu.js"></script>
    <script type="text/javascript" src="js/slideshow.js"></script>
    <script type="text/javascript" src="js/cufon-yui.js"></script>
    <script type="text/javascript" src="js/arial.font.js"></script>--%>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <link href="../assets/css/style.css" rel="stylesheet" />
    <!-- font awesome -->
    <link href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../assets/New/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assets/New/css/bootstrap.min2.css" rel="stylesheet" />
    <link href="../CSS/style.css" rel="stylesheet" />
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />
    <script type="text/javascript">
        Cufon.replace('h1,h2,h3,h4,h5,#menu,#copy,.blog-date');
    </script>
    <script type="text/javascript" language="javascript">
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
    </script>
    <script language="javascript" type="text/javascript">
        $(function () {
            var _URL = window.URL;

            $("#fileuploadimage").change(function (e) {

                var fileUpload = document.getElementById("fileuploadimage");
                if (typeof (fileuploadimage.files) != "undefined") {
                    var size = parseFloat(fileuploadimage.files[0].size / 1024).toFixed(2);
                    if (size > 100) {
                        alert("फोटो का साइज़ 100 KB से कम होना चाहिए!");
                        document.getElementById("fileuploadimage").value = '';
                    }
                } else {
                    alert("This browser does not support HTML5.");
                }
            });
        });

        $(function () {
            var _URL = window.URL;
            $("#fileuploadDoc").change(function (e) {
                var fileUpload = document.getElementById("fileuploadDoc");
                if (typeof (fileuploadDoc.files) != "undefined") {
                    var size = parseFloat(fileuploadDoc.files[0].size / 1024).toFixed(2);
                    if (size > 400) {
                        alert("रोजगार कार्यालय पंजीयन का साइज़ 400 KB से कम होना चाहिए!");
                        document.getElementById("fileuploadDoc").value = '';
                    }
                } else {
                    alert("This browser does not support HTML5.");
                }
            });
        });
    </script>
    <script language="javascript" type="text/javascript">

        $(function () {
            var _URL = window.URL;

            $("#fileuploadCert").change(function (e) {

                var fileUpload = document.getElementById("fileuploadCert");

                if (typeof (fileuploadCert.files) != "undefined") {
                    var size = parseFloat(fileuploadCert.files[0].size / 1024).toFixed(2);
                    if (size > 400) {

                        alert("शैक्षणिक प्रमाण पत्र का साइज़ 400 KB से कम होना चाहिए!");
                        document.getElementById("fileuploadCert").value = '';
                    }
                } else {
                    alert("This browser does not support HTML5.");
                }
            });
        });

        $(function () {
            var _URL = window.URL;

            $("#fileuploadCast").change(function (e) {

                var fileUpload = document.getElementById("fileuploadCast");
                if (typeof (fileuploadCast.files) != "undefined") {
                    var size = parseFloat(fileuploadCast.files[0].size / 1024).toFixed(2);
                    if (size > 400) {
                        alert("जाति प्रमाण पत्र का साइज़ 400 KB से कम होना चाहिए!");
                        document.getElementById("fileuploadCast").value = '';
                    }
                } else {
                    alert("This browser does not support HTML5.");
                }
            });
        });
    </script>
    <script type="text/javascript">
        function disablefield() {
            if (document.getElementById('rdoNo').checked == 1) {
                document.getElementById('txtWLicNo').disabled = 'disabled';
                document.getElementById('txtWLicNo').value = '';
                document.getElementById('txtSLDate').disabled = 'disabled';
                document.getElementById('txtSLDate').value = '';
            } else {
                document.getElementById('txtWLicNo').disabled = '';
                document.getElementById('txtWLicNo').value = '';
                document.getElementById('txtSLDate').disabled = '';
                document.getElementById('txtSLDate').value = '';
            }
        }
    </script>
    <script type="text/javascript">
        function disablefield1() {
            if (document.getElementById('rdoNoW').checked == 1) {
                document.getElementById('txtWDRALicenceNo').disabled = 'disabled';
                document.getElementById('txtWDRALicenceNo').value = '';
                document.getElementById('txtWDRALDate').disabled = 'disabled';
                document.getElementById('txtWDRALDate').value = '';
            } else {
                document.getElementById('txtWDRALicenceNo').disabled = '';
                document.getElementById('txtWDRALicenceNo').value = '';
                document.getElementById('txtWDRALDate').disabled = '';
                document.getElementById('txtWDRALDate').value = '';
            }
        }
    </script>
    <%-- <style type="text/css">
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

        .style2 {
            height: 22px;
        }
    </style>--%>
    <script type="text/javascript">
        function preventInput(evnt) {
            //Checked In IE9,Chrome,FireFox
            if (evnt.which != 9) evnt.preventDefault();
        }
    </script>
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
            padding: 0;
        }

            .modalPopup .header {
                background-color: #D69758;
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
        #popupwin {
            position: fixed;
            top: 0;
            left: 0;
            width: 90%;
            height: 90%;
            background-color: #000;
            filter: alpha(opacity=65);
            -moz-opacity: 0.7;
            display: none;
            opacity: 0.7;
            z-index: 100;
        }

        .pop a {
            text-decoration: none;
        }

        .popup {
            width: 100%;
            height: 98%;
            margin: 0 auto;
            position: fixed;
            z-index: 101;
            padding-left: 90px;
        }

        .pop {
            /*min-width: 900px;*/
            width: 80%;
            min-height: 150px;
            margin: 0px auto;
            background: #FFFFFF;
            position: relative;
            z-index: 103;
            padding: 10px;
            border-radius: 5px;
            box-shadow: 0 5px 10px #000;
            /*margin-top:200px;*/
        }

            .pop p {
                color: #555555;
                text-align: justify;
                font-size: medium;
            }

                .pop p a {
                    color: #d91900;
                }

            .pop .x {
                float: right;
                height: 35px;
                /*left: 22px;*/
                position: relative;
                /*top: -20px;*/
                width: 35px;
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

        .auto-style1 {
            height: 10px;
            width: 458px;
        }

        .auto-style2 {
            width: 458px;
        }
    </style>
    <style type="text/css">
        fieldset {
            border: 1px solid #2095A1;
            padding: 0.35em 0.625em 0.75em;
            margin: 10px;
            border-radius: 5px;
            padding-left: 20px;
        }

        legend {
            padding: 2px 8px;
            border-radius: 10px;
            width: auto;
            border: 1px solid #2095A1;
            font-size: 17px;
            font-weight: bold;
            color: #030203;
        }

        .content-wrapper {
            padding: 1.75rem 1.25rem;
        }

        .table-bordered th, .table-bordered td {
            border: 1px solid #030203;
        }

        .form-control {
            border: 1px solid #767B83;
            border-radius: 8px;
        }

        .table th {
            text-align: center;
        }

        .form-inline {
            display: block !important;
        }

        th.sorting, th.sorting_asc, th.sorting_desc {
            background: #647e68 !important;
            color: black !important;
        }

        .table > tbody > tr > td, .table > tbody > tr > th, .table > tfoot > tr > td, .table > tfoot > tr > th, .table > thead > tr > td, .table > thead > tr > th {
            padding: 8px 5px;
            color: black !important;
        }

        element.style {
            font-size: medium !important;
        }
    </style>
</head>
<body>

    <div id="bg" style="background-color: White">
        <div class="wrap">
            <img src="../images/CH.jpg" style="width: 100%" alt="" height="160" />

            <div id="PrintDiv">
                <form id="form1" runat="server">
                    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></cc1:ToolkitScriptManager>

                    <center>
                        <table style="width: 100%">
                            <tr>
                                <td colspan="4" style="font-size: medium;">
                                    <table style="width: 100%; height: 32px; font-size: medium;">
                                        <tr>
                                            <td style="background-color: #008CBA; width: 70PX;" align="center">
                                                <asp:LinkButton ID="link1" Text="Home" runat="server" PostBackUrl="~/JointVentureScheme/WarehouseHome.aspx" ForeColor="White"></asp:LinkButton>
                                            </td>
                                            <td colspan="2" style="background-color: #008CBA; font-size: medium; color: White; width: 100px" align="center">Welcome&nbsp;<asp:Label ID="lbluser" runat="server" ForeColor="White"></asp:Label></td>
                                            <td style="background-color: #008CBA; width: 70px;" align="center">
                                                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" ForeColor="White">Log out</asp:LinkButton></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <table style="width: 100%">
                            <tr>
                                <td style="font-weight: bold; word-wrap: break-word; color: Black">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Registration ID :
                                    <asp:TextBox ID="txtregistrationid" runat="server" Width="200px" Height="30px"></asp:TextBox>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="Button1" runat="server" Text="Search" class="button button2" Width="150px" Height="30px" OnClick="Button1_Click"></asp:Button>
                                </td>
                            </tr>
                        </table>
                        <table style="width: 100%" id="tblupdateregistration" runat="server" visible="false">
                            <tr>
                                <td colspan="4">
                                    <marquee direction="left" behavior="alternate">
                                        <p style="font-size: 16px; color: #008080; font-weight: bold">
                                            Add New Godown in Existing Registration
                                        </p>
                                    </marquee>
                                </td>

                            </tr>

                            <tr>
                                <td colspan="4" style="background-color: #66CCFF; height: 25px" align="center">
                                    <p style="font-size: 14px; color: Black; font-weight: bold">
                                        Owner Details <span style="color: #FF0000; font-size: x-large;">*</span>
                                    </p>
                                </td>
                            </tr>
                            <tr>
                                <td style="font-weight: bold; word-wrap: break-word; color: Black">&nbsp;&nbsp;Owner Name :
                                </td>
                                <td style="font-weight: bold; word-wrap: break-word; color: Black">
                                    <asp:Label ID="lblAuthPerson" runat="server"></asp:Label>
                                </td>
                                <td style="font-weight: bold; word-wrap: break-word; color: Black">Registered Email ID:
                                </td>
                                <td style="font-weight: bold; word-wrap: break-word; color: Black">
                                    <asp:Label ID="lblEmail" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>

                                <td class="style2" style="font-weight: bold; word-wrap: break-word; color: Black">&nbsp;&nbsp;Mobile Number:
                                </td>
                                <td class="style2" style="font-weight: bold; word-wrap: break-word; color: Black">
                                    <asp:Label ID="lblMob" runat="server"></asp:Label>
                                </td>
                                <td style="font-weight: bold; word-wrap: break-word; color: Black">Warehouse Name  :
                                </td>
                                <td style="font-weight: bold; word-wrap: break-word; color: Black">
                                    <asp:Label ID="lblWarehousename" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>

                                <td style="font-weight: bold; word-wrap: break-word; color: Black">&nbsp;&nbsp;District :
                                </td>
                                <td style="font-weight: bold; word-wrap: break-word; color: Black">
                                    <asp:Label ID="lblDistrict" runat="server"></asp:Label>

                                </td>
                                <td style="font-weight: bold; word-wrap: break-word; color: Black">&nbsp;&nbsp;Branch :
                                </td>
                                <td style="font-weight: bold; word-wrap: break-word; color: Black">
                                    <asp:Label ID="lblbranch" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="font-weight: bold; word-wrap: break-word; color: Black">&nbsp;&nbsp;Total Capacity (In M.T) :
                                </td>
                                <td style="font-weight: bold; word-wrap: break-word; color: red">
                                    <asp:Label ID="lblCapt" runat="server"></asp:Label>
                                </td>
                                <td style="font-weight: bold; word-wrap: break-word; color: Black">&nbsp;&nbsp;Payable Registration Fee Rs :
                                </td>
                                <td style="font-weight: bold; word-wrap: break-word; color: red">
                                    <asp:Label ID="lblRegFee" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="border-color: #66CCFF; background-color: #66CCFF; border-style: solid; height: 25px; border-width: 2px;" align="center">
                                    <p style="font-size: 14px; color: Black; font-weight: bold">
                                        Godown Description <span style="color: #FF0000; font-size: x-large;">*</span>
                                    </p>
                                </td>

                            </tr>
                            <tr>
                                <td colspan="4" style="color: Red;">Note :</td>
                            </tr>

                            <tr>
                                <td colspan="4" style="background-color: none; font-size: small;">&nbsp;&nbsp;1.एक गोदाम संचालक के रजिस्ट्रेशन मे शामिल किए जाने वाले समस्त गोदाम एक ही परिसर मे स्थित होने चाहिए, अन्य परिसर अथवा अन्य स्थान मे स्थित गोदामो के लिए प्रथक रजिस्ट्रेशन करे।
                                </td>
                            </tr>
                            <%--  <tr>
                                &nbsp;&nbsp;2.Formula for Capacity = [Length*Breadth*(Height-3)/80].
                                </td>
                            </tr>--%>
                            <%--<tr>
                                &nbsp;&nbsp;3.गोदाम की ऊंचाई 14 से 18 फीट के बीच होना चाहिए(Godown height should be between 14 ft to 18 ft.). 
                                </td>
                            </tr>--%>
                            <tr>
                                <td style="height: 10px;"></td>
                            </tr>
                            <tr>
                                <td colspan="4" id="Td1" runat="server" visible="true" align="center">
                                    <br />
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                                        Width="90%" EnableModelValidation="True" BackColor="White" BorderColor="Black"
                                        BorderStyle="Solid" BorderWidth="1px" CellPadding="3">
                                        <Columns>
                                            <asp:BoundField DataField="Godown_No" HeaderText="Godown No." />
                                            <asp:TemplateField HeaderText="Godown ID">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblGodown_ID" Width="100%" Text='<%# Eval("Godown_ID")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Length in Feet">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblG_Length" Width="100%" Text='<%# Eval("G_Length")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Width in Feet">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblG_Width" Width="100%" Text='<%# Eval("G_Width")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Height in Feet">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblG_Height" Width="100%" Text='<%# Eval("G_Height")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Capacity (MT)">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblG_ScientificCapacity" Width="100%" Text='<%# Eval("G_ScientificCapacity")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Construction Year">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblG_ConstructedYear" Width="100%" Text='<%# Eval("G_ConstructedYear")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Licence Type">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblLicType" Width="100%" Text='<%# Eval("LicType")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Licence No.">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblLicNo" Width="100%" Text='<%# Eval("LicNo")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Licence Issue Date">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblLicIssueDate" Width="100%" Text='<%# Eval("LicIssueDate")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Licence Validity Date">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblLicValidityDate" Width="100%" Text='<%# Eval("LicValidityDate")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:BoundField DataField="G_Length" HeaderText="Length in Feet" />
                                            <asp:BoundField DataField="G_Width" HeaderText="Width in Feet" />
                                            <asp:BoundField DataField="G_Height" HeaderText="Height in Feet" />
                                            <asp:BoundField DataField="G_ScientificCapacity" HeaderText="Capacity (MT)" />
                                            <asp:BoundField DataField="G_ConstructedYear" HeaderText="Construction Year" />--%>
                                            <%--<asp:BoundField DataField="LicType" HeaderText="Licence Type" />--%>
                                            <%--<asp:BoundField DataField="LicNo" HeaderText="Licence No." />--%>
                                            <%--<asp:BoundField DataField="LicIssueDate" HeaderText="Licence Issue Date" />--%>
                                            <%--<asp:BoundField DataField="LicValidityDate" HeaderText="Licence Validity Date" />--%>
                                            <asp:TemplateField HeaderText="Update Lincance Details">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkBtnEdit" runat="server" Text="Update" CssClass="btn btn-sm"
                                                        OnClick="Display"></asp:LinkButton>
                                                </ItemTemplate>
                                                <ControlStyle Font-Bold="True" ForeColor="Black" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                        <HeaderStyle BackColor="#99CCFF" Font-Bold="True" ForeColor="Black" />
                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                        <RowStyle ForeColor="#000066" HorizontalAlign="Center"
                                            VerticalAlign="Middle" />
                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                    </asp:GridView>
                                    <br />

                                </td>
                            </tr>
                            <tr>

                                <td colspan="4" id="GVGodowns" runat="server" visible="true" style="text-align: center; width: 100%;" align="center">
                                    <asp:GridView ID="gvGodown" runat="server" AutoGenerateColumns="False" ShowFooter="true" Width="100%"
                                        EnableModelValidation="True" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                        GridLines="Horizontal" OnRowDeleting="gvGodown_RowDeleting" AutoGenerateDeleteButton="false">
                                        <AlternatingRowStyle BackColor="#F7F7F7" />
                                        <Columns>
                                            <asp:BoundField DataField="RowNumber" HeaderText="Godown No." />
                                            <asp:TemplateField HeaderText="Length in Feet">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtLenght" runat="server" Width="70px" Text='<%# Eval("Lenght") %>'>0</asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Width in Feet">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtWidth" runat="server" Width="70px" Text='<%# Eval("Width") %>'>0</asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Height in Feet">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtHeight" runat="server" Width="70px" Text='<%# Eval("Height") %>'>0</asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Select">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="ckstack" runat="server" AutoPostBack="True" OnCheckedChanged="ckstack_CheckedChanged" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Capacity (MT)">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtCapacity" runat="server" Enabled="false" Width="70px" Text='<%# Eval("Capacity") %>'>0</asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Construction Year">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtConstY" runat="server" Width="70px" Text='<%# Eval("ConstY") %>'>0</asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Licence Type">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlgdwntype" runat="server" Height="21px" Width="70px">
                                                        <asp:ListItem Value="-1">--Select--</asp:ListItem>
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Licence  No./Application No." HeaderStyle-Width="100px">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="Gtxtlicno" runat="server" Width="150px" Font-Bold="true" Height="15px" align="Center" Text='<%# Eval("LNo") %>' placeholder="Enter Licence Number"></asp:TextBox>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Lic. Issue/Application Date">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="GtxtLicIssuedate" runat="server" Width="70px" Font-Bold="true" Height="15px" Text='<%# Eval("LIssueDate") %>' onkeydown="javascript:preventInput(event);" onpaste="return false;"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="GtxtLicIssuedate">
                                                    </cc1:CalendarExtender>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Lic. Expiry Date">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="GtxtLicExpdate" runat="server" Width="80px" Font-Bold="true" Height="15px" Text='<%# Eval("LExpDate") %>' onkeydown="javascript:preventInput(event);" onpaste="return false;"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="GtxtLicExpdate">
                                                    </cc1:CalendarExtender>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <FooterTemplate>
                                                    <asp:Button ID="ButtonAdd" runat="server" Text="Add Godown" Width="80px" Font-Size="12px"
                                                        OnClick="ButtonAdd_Click" />
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                                        <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                                        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                    </asp:GridView>



                                </td>
                            </tr>

                            <tr>

                                <td colspan="4" align="center">Total Capacity :
                                    
                                    
                                    <asp:Label ID="lblTotalCapacity" runat="server" Text="0.00" Font-Bold="true" ForeColor="Red"></asp:Label>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        Total Registration Fee:
                                    <asp:Label ID="lblTotalRegAmt" runat="server" Text="0.00" Font-Bold="true" ForeColor="Red"></asp:Label>
                                </td>

                            </tr>

                            <tr>

                                <td align="center" colspan="4">

                                    <asp:Button ID="btnsubmit" runat="server" Text="Submit" class="button button2" Width="150px" Height="30px" OnClick="btnsubmit_Click"></asp:Button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <%--<input id="Button1" name="Print" type="button" style=" color:Maroon; background-color:Silver; font-size:large; font-family:Times New Roman Baltic;" value="Print"  onclick="PrintDiv();" />--%>

                                    <asp:Button ID="btnpayment" runat="server" Text="Proceed to Payment" class="button button2" Width="150px" Height="30px" Visible="false"
                                        OnClick="btnpayment_Click"></asp:Button>

                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            
                            <asp:Button ID="btnprint" runat="server" Text="Print Registration" Width="150px" Height="30px" Visible="false"
                                class="button button2" OnClick="btnprint_Click"></asp:Button>

                                </td>
                            </tr>
                        </table>
                        <%--  <asp:Label ID="Label8" runat="server" Font-Bold="true" Font-Size="Medium"></asp:Label>
                        <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" PopupControlID="pnlCofirmmsg" TargetControlID="Label8"
                            CancelControlID="btnNo" BackgroundCssClass="modalBackground">
                        </cc1:ModalPopupExtender>--%>
                        <div>
                            <asp:Panel ID="pnllogin" class="popup" runat="server">
                                <div class="pop" style="background-color: white; min-height: 300PX; max-height: 500px; width: 1500px; border: #008CBA; border-style: solid; border-width: 10px;">
                                    <fieldset>
                                        <legend>Update Registration Capacity</legend>
                                        <div class="row">
                                            <div class="col-md-2">
                                                <label>Warehouse Name</label>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtWarehouse" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                                <asp:TextBox ID="txtGdwnID" CssClass="form-control" runat="server" ReadOnly="true"
                                                    Width="150px" Height="20px"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2">
                                                <label>Length</label>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtLength" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2">
                                                <label>Width</label>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtWidth" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row" style="margin-top: 20px">
                                            <div class="col-md-2">
                                                <label>Height</label>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtheight" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2">
                                                <label>Godown Capacity</label>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtCapacity" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2">
                                                <label>Construction Year</label>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtConstruction" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row" style="margin-top: 20px">
                                            <div class="col-md-2">
                                                <label>Lic Type</label>
                                            </div>
                                            <div class="col-md-2">
                                                <%--<asp:TextBox ID="txtlictype" CssClass="form-control" runat="server"></asp:TextBox>--%>
                                                <asp:DropDownList ID="ddllictype" runat="server" CssClass="form-control">
                                                    <asp:ListItem Value="00">APPLIED For NON WDRA</asp:ListItem>
                                                    <asp:ListItem Value="0">APPLIED For WDRA</asp:ListItem>
                                                    <asp:ListItem Value="63">NON WDRA</asp:ListItem>
                                                    <asp:ListItem Value="68">WDRA</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-2">
                                                <label>Lic No</label>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtlicno" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2">
                                                <label>Lic Date</label>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtlicdate" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row" style="margin-top: 20px">
                                            <div class="col-md-2">
                                                <label>Expire Date</label>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtExpire" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2">
                                                <label>Registration Capacity</label>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtregcapacity" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>

                                        </div>
                                        <div class="row" style="margin-top: 20px">
                                            <div class="col-md-5"></div>
                                            <div class="col-md-1">
                                                <asp:Button class="btn btn-success" ID="Button2"
                                                    runat="server" Text="Update" align="Center" OnClick="Button2_Click1" />
                                            </div>
                                            <div class="col-md-1">
                                                <asp:Button class="btn bg-primary" ID="btnGenerateBill" runat="server" Text="Close"></asp:Button>
                                            </div>
                                        </div>
                                    </fieldset>
                                </div>
                            </asp:Panel>
                            <asp:Label ID="Label8" runat="server" Font-Bold="true" Font-Size="Medium"></asp:Label>
                            <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Label8" BackgroundCssClass="popup" PopupControlID="pnllogin">
                            </cc1:ModalPopupExtender>
                        </div>
                    </center>
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
        </div>
    </div>
</body>
</html>
