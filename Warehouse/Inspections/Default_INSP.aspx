<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default_INSP.aspx.cs" Inherits="Default_INSP" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!-- The above 3 meta tags *must* come first in the head; any other head content must come *after* these tags -->
    <meta name="description" content="">
    <meta name="author" content="">
    <title>MP Warehouse Online Inspections</title>

    <style type="text/css">
        header, nav, section, article, aside, footer {
            display: block;
        }
    </style>
    <!-- Bootstrap core CSS -->
    <link href="assets/css/bootstrap.min.css" rel="stylesheet" type="text/css">
    <link href="assets/css/bootstrap-theme.min.css" rel="stylesheet" type="text/css">

    <!-- font awesome -->
    <link href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet">

    <link href="assets/css/style.css" rel="stylesheet" type="text/css">
    <link href="assets/css/custome.css" rel="stylesheet" type="text/css">
    <link href="CSS/style.css" rel="stylesheet" />
    <style type="text/css">
        body {
            font-family: Arial;
            font-size: 10pt;
        }

        table {
            border: 1px solid #ccc;
        }

            table th {
                background-color: #F7F7F7;
                color: #333;
                font-weight: bold;
            }

            table th, table td {
                padding: 5px;
                border-color: #ccc;
            }
    </style>
    <style>
        * {
            margin: 0px;
            padding: 0px;
        }

        ul {
            list-style: none;
        }

        a {
            text-decoration: none;
        }

        .clear {
            clear: both;
        }

        .wrap {
            width: 1200;
            margin: auto;
        }

        .navtop {
            /*background: #333;*/
            height: 30px;
            color: #fff;
        }

            .navtop > ul {
                position: relative;
                z-index: 1;
            }

                .navtop > ul > li {
                    float: left;
                    border-left: 1px solid #fff;
                }

            .navtop ul li a {
                display: block;
                padding: 10px 20px;
                color: #ccc;
            }

                .navtop ul li a:hover {
                    background: #1e364a;
                    color: #fff;
                }

            .navtop ul li:first-child {
                border: none;
            }

            .navtop > ul > li:hover > ul {
                display: block;
            }

            .navtop > ul > li > ul > li:hover ul {
                display: block;
            }

            .navtop > ul > li > ul li {
                height: 41px;
            }

            .navtop > ul > li > ul {
                display: none;
                position: absolute;
                background: #333;
            }

                .navtop > ul > li > ul > li > ul {
                    display: none;
                    position: relative;
                    left: 132px;
                    top: -41px;
                    background: #333;
                }

            .navtop ul ul li {
                border-top: 1px solid #fff;
            }
    </style>
    <style type="text/css">
        .wrap {
            margin: 0 auto;
            /*width: 960px;*/
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
            border-radius: 25px;
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

        .button2 {
            background-color: white;
            color: black;
            border: 2px solid #DBAF75;
        }

            .button2:hover {
                background-color: #DBAF75;
                color: white;
            }
    </style>

    <script type="text/javascript">

        $(document).ready(function () {

            $("#Panel1").hide();

            $("#Button1").click(function () {

                $("#Panel1").show();

            });

        });
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
    <script type="text/javascript" src="<%= ResolveUrl("~/NEW_CSS/js/jquery-1.12.4.js") %>"></script>
    <script type="text/javascript" src="<%= ResolveUrl("~/assets/js/bootstrap.min.js") %>"></script>
    <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />
</head>

<body>

    <header>

        <div class="logo_bar">
            <div class="">
                <img src="../images/insp.jpg" style="width: 100%" alt="" height="140" />
            </div>
        </div>
    </header>

    <nav class="navbar">
        <div class="">
            <!-- Brand and toggle get grouped for better mobile display -->
            <div class="navbar-header">
                <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1" aria-expanded="false">
                    <span class="sr-only">Toggle navigation</span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                <!-- <a class="navbar-brand" href="#">Human Resource Management System</a> -->
            </div>


            <div class="wrap">
                <div class="navtop">
                    <ul>
                        <li><a href="../Inspections/Default.aspx">Home</a></li>

                    </ul>
                    <div class="clear"></div>
                </div>
                <!--Nav Ends-->
            </div>
        </div>
        <!-- /.container-fluid -->
    </nav>

    <section class="content_wrapper" style="min-height: 450px;">
        <div class="container">
            <div class="row">
                <div class="col-md-4 col-lg-4">
                    <h3 class="head_text"><strong>INSTRUCTIONS</strong></h3>
                    <%-- <ul>
                        <li>Lorem ipsum dolor sit amet, consectetur adipiscing elit.</li>
                        <li>Quisque id nisi a est sodales luctus.</li>
                        <li>Nunc consectetur magna ac massa eleifend dictum.</li>
                        <li>Pellentesque non justo sodales, interdum leo id, fermentum elit.</li>
                        <li>Curabitur ut odio in diam posuere volutpat.</li>
                    </ul>--%>
                </div>
                <div class="col-md-8 col-lg-8">
                    <div class="panel panel-default login_wrapper">
                        <div class="panel-heading text-uppercase lead">Login to your Account</div>
                        <div class="panel-body">
                            <form role="form" runat="server">
                                <table align="center" style="border: 6px solid #DBAF75; background-color: #FDFAF7; margin-top: 20px;">
                                    <tr>
                                        <td align="center" colspan="2" valign="middle"
                                            style="background-color: #F0DBBF">
                                            <span style="color: #cb4e48; font-weight: bolder; font-size: 15pt">Login</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="2" style="color: #BA8132;">
                                            <br />
                                            <asp:RadioButton ID="rdogodown" runat="server" Text="Godown" Checked="false" GroupName="rb" Font-Bold="True"
                                                AutoPostBack="True" OnCheckedChanged="rdogodown_CheckedChanged"></asp:RadioButton>

                                            &nbsp&nbsp&nbsp<asp:RadioButton ID="rdoBranch" runat="server" Text=" Branch" Checked="false" GroupName="rb" Font-Bold="True"
                                                AutoPostBack="True" OnCheckedChanged="rdoBranch_CheckedChanged"></asp:RadioButton>

                                            &nbsp&nbsp&nbsp<asp:RadioButton ID="rbbranch" runat="server" Text=" Inspection Officer" GroupName="rb"
                                                Font-Bold="True" AutoPostBack="True" OnCheckedChanged="rbbranch_CheckedChanged"></asp:RadioButton>

                                            &nbsp;&nbsp;&nbsp;
                                            <asp:RadioButton ID="rbrm" runat="server" Text=" Region" GroupName="rb" Checked="true" Font-Bold="True"
                                                AutoPostBack="True" OnCheckedChanged="rbrm_CheckedChanged"></asp:RadioButton>

                                            &nbsp;&nbsp;&nbsp;<asp:RadioButton ID="rbHo" runat="server" Text=" State" GroupName="rb" Font-Bold="True"
                                                AutoPostBack="True" OnCheckedChanged="rbHo_CheckedChanged"></asp:RadioButton>

                                            &nbsp;&nbsp;&nbsp;<asp:RadioButton ID="RbTQRegion" runat="server" Text=" TQ Region" GroupName="rb" Font-Bold="True"
                                                AutoPostBack="True" OnCheckedChanged="RbTQRegion_CheckedChanged"></asp:RadioButton>

                                            &nbsp;&nbsp;&nbsp;<asp:RadioButton ID="RBTQHO" runat="server" Text=" TQ HO" GroupName="rb" Font-Bold="True"
                                                AutoPostBack="True" OnCheckedChanged="RBTQHO_CheckedChanged"></asp:RadioButton>

                                            <%-- &nbsp;&nbsp;&nbsp;<asp:RadioButton ID="RadioButton2" runat="server" Text=" Technical" GroupName="rb" Font-Bold="True"
                                                AutoPostBack="True" OnCheckedChanged="RbTechnical_CheckedChanged" ></asp:RadioButton>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="width: 60%; height: 20px;"></td>
                                    </tr>
                                    <tr>
                                        <td style="height: 10px">

                                            <asp:Label ID="lbllogintype" runat="server" Text="Login Type:" Visible="False"></asp:Label>

                                        </td>
                                        <td align="left">

                                            <asp:DropDownList ID="ddllogintype" runat="server"
                                                CssClass="tb6" Font-Bold="true" ForeColor="Navy" Height="30px"
                                                Width="205px" Visible="False" AutoPostBack="True" OnSelectedIndexChanged="ddllogintype_SelectedIndexChanged">
                                            </asp:DropDownList>

                                        </td>
                                    </tr>
                                    <tr runat="server" id="trregion">
                                        <td style="width: 250px;">&nbsp&nbsp&nbsp&nbsp&nbsp Region:
                                        </td>
                                        <td style="width: 235px;">
                                            <asp:DropDownList ID="ddlregion" runat="server" CssClass="tb6" Font-Bold="true" ForeColor="Navy"
                                                Height="30px" Visible="True" Width="200px" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlregion_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="trdest">
                                        <td>&nbsp&nbsp&nbsp&nbsp&nbsp District:</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_Dist" runat="server" AutoPostBack="true" Width="200px" Height="30px" Font-Bold="true"
                                                ForeColor="Navy" CssClass="tb6" OnSelectedIndexChanged="DDL_Dist_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="trBranch">
                                        <td>&nbsp&nbsp&nbsp&nbsp&nbsp Branch:</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_Branch" runat="server" AutoPostBack="true"
                                                Width="200px" Height="30px" Font-Bold="true" ForeColor="Navy" CssClass="tb6" OnSelectedIndexChanged="DDL_Branch_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="trOfNm">
                                        <td>&nbsp&nbsp&nbsp&nbsp&nbsp Inspection Officer Name:</td>
                                        <td>
                                            <asp:DropDownList ID="ddlInspOff" runat="server" AutoPostBack="false"
                                                Width="200px" Height="30px" Font-Bold="true" ForeColor="Navy" CssClass="tb6">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lbl_godown" runat="server" Text="Godown/Silo:" Visible="False"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="ddl_godown" runat="server" AutoPostBack="false" CssClass="tb6" Font-Bold="true" ForeColor="Navy" Height="30px" Visible="False" Width="205px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 170PX;">&nbsp&nbsp&nbsp&nbsp&nbsp Password:
                                        </td>
                                        <td>
                                            <input id="txtlogpwd" name="rname" class="text" runat="server" onkeypress="return processKeystroke(event);" placeholder="Password" type="password" tabindex="10" style="width: 200px; height: 28px;" /></td>
                                        <asp:HiddenField ID="hdnActualPassword" runat="server" />
                                    </tr>
                                    <tr>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:Button class="button button2" ID="btnlogin" runat="server" Text="Login"
                                                TabIndex="11" OnClick="btnlogin_Click" Width="200px" Height="30px"></asp:Button>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="width: 60%; height: 15px;"></td>
                                    </tr>
                                </table>
                            </form>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </section>
    <script src='http://www.pib.nic.in/js/jquery.flexisel.js'></script>
    <footer>

        <div class="footer-bottom">
            <div class="container">

                <p style="text-align: center;">
                    Copyright &copy; 2020 MPWLC - All Rights Reserved | Madhya Pradesh Warehousing and Logistics Corporation
                </p>
                <p style="text-align: center;">The desired screen resolution is 1024x768 or above. Site Best Viewed In Microsoft IE-6+, Mozilla Firefox, Google Chrome, Safari etc... </p>

                <div class="clearfix"></div>
            </div>
        </div>
    </footer>

    <!--Java Script -->
    <script src="<%= ResolveUrl("~/NEW_CSS/js/jquery-1.12.4.js") %>"></script>
    <script src="<%= ResolveUrl("~/assets/js/bootstrap.min.js") %>"></script>


    <script type="text/javascript" src="assets/js/custom.js"></script>

</body>
</html>
