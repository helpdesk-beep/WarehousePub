<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default2.aspx.cs" Inherits="Default2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" lang="en">
<head id="Head1">
    <title>Raj Bhavan MP | The Hon'ble Governor 
    </title>
    <link rel="icon" href="images/logo/favicon.png" />
    <meta property="fb:app_id" content="930300783691811" />
    <meta property="og:title" content="Welcome to Facebook" />
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta http-equiv="X-UA-Compatible" content="IE=Edge" />
    <meta property="og:url" content="http://governor.mp.gov.in/" />
    <meta name="description" content="A web based system having multiple modules that handles the offline activities of Governor Secretariat as well as provides the information of Honorable Governor’s daily activity to the general public." />
    <meta name="keywords" content="Governor House Madhya Pradesh" />
    <meta name="author" content="NIC, National Informatics Center, Madhya Pradesh, MP, Group-A" />
    <link href="css/style.css" rel="stylesheet" />
  
    <style type="text/css">
        .vertical {
            background-color: yellow;
            text-align: left;
        }

        .horizontal {
            display: inline;
            background-color: aqua;
        }
    </style>
    <style media="screen">
        html, body {
            font-size: 100%;
        }

        .release_list li a {
            min-height: 30px !important;
        }

        .sf-menu li a {
            padding: 12px 36px !important;
        }

        .fa {
            color: ghostwhite !important;
        }

        .sf-menu li li a {
            font-weight: 300 !important;
            padding: 11px 3px 8px 8px;
        }

        .carousel-inner img {
            width: 100%;
            height: 440px;
        }

        .glyphicon {
            margin-right: 4px !important; /*override*/
        }

        .pagination .glyphicon {
            margin-right: 0px !important; /*override*/
        }

        .pagination a {
            color: #555;
        }

        .panel ul {
            padding: 0px;
            margin: 0px;
            list-style: none;
        }

        .news-item {
            padding: 4px 4px;
            margin: 0px;
            border-bottom: 1px dotted #555;
        }

        .img12 {
            width: 130px !important;
        }

        .pt40 pb50 {
        }

        /*  bhoechie tab */
        div.bhoechie-tab-container {
            z-index: 10;
            background-color: #ffffff;
            padding: 0 !important;
            border-radius: 4px;
            -moz-border-radius: 4px;
            border: 1px solid #ddd;
            margin-top: 20px;
            margin-left: 50px;
            -webkit-box-shadow: 0 6px 12px rgba(0,0,0,.175);
            box-shadow: 0 6px 12px rgba(0,0,0,.175);
            -moz-box-shadow: 0 6px 12px rgba(0,0,0,.175);
            background-clip: padding-box;
            opacity: 0.97;
            filter: alpha(opacity=97);
        }

        div.bhoechie-tab-menu {
            padding-right: 0;
            padding-left: 0;
            padding-bottom: 0;
        }

            div.bhoechie-tab-menu div.list-group {
                margin-bottom: 0;
            }

                div.bhoechie-tab-menu div.list-group > a {
                    margin-bottom: 0;
                }

                    div.bhoechie-tab-menu div.list-group > a .glyphicon,
                    div.bhoechie-tab-menu div.list-group > a .fa {
                        color: #5A55A3;
                    }

                    div.bhoechie-tab-menu div.list-group > a:first-child {
                        border-top-right-radius: 0;
                        -moz-border-top-right-radius: 0;
                    }

                    div.bhoechie-tab-menu div.list-group > a:last-child {
                        border-bottom-right-radius: 0;
                        -moz-border-bottom-right-radius: 0;
                    }

                    div.bhoechie-tab-menu div.list-group > a.active,
                    div.bhoechie-tab-menu div.list-group > a.active .glyphicon,
                    div.bhoechie-tab-menu div.list-group > a.active .fa {
                        background-color: #5A55A3;
                        color: #ffffff;
                    }

                        div.bhoechie-tab-menu div.list-group > a.active:after {
                            content: '';
                            position: absolute;
                            left: 100%;
                            top: 50%;
                            margin-top: -13px;
                            border-left: 0;
                            border-bottom: 13px solid transparent;
                            border-top: 13px solid transparent;
                            border-left: 10px solid #5A55A3;
                        }

        div.bhoechie-tab-content {
            background-color: #ffffff;
            /* border: 1px solid #eeeeee; */
            padding-left: 20px;
            padding-top: 10px;
        }

        div.bhoechie-tab div.bhoechie-tab-content:not(.active) {
            display: none;
        }
    </style>
</head>
<body id="fontSize" style="background-image: url('images/logo/bg1231.jpg')">
    <form method="post" runat="server" id="form1">
             
        
        <div class="mean-container">
        </div>
      
        <!-- Menu Part End -->
        <div class="clearfix">
        </div>
        <div id="fb-root"></div>
        
        <section class="banner_bg pt20" style="background-image: url(images/logo/bg1231.jpg)">
            <link href="css/bootstrap.min.css" rel="stylesheet" />
            <script src="js/jquery.min.js"></script>
            <script src="js/bootstrap.min.js"></script>
            <div class="container" style="background-color: antiquewhite!important;">
                <br />                
                <div class="row">

                    <div class="col-md-9 col-sm-8 col-xs-12">
                        <div id="myCarousel" class="carousel slide" data-ride="carousel">

                            <div class="carousel-inner" role="listbox">

                                <asp:Repeater ID="rptCarousel" runat="server">
                                    <ItemTemplate>
                                        <div class="carousel-item <%#GetActiveClass(Container.ItemIndex) %>">
                                            <img src='<%# Eval("image") %>' runat="server" alt='<%# Eval("imageCaption") %>' />
                                            <div class="carousel-caption">
                                                <p><%# Eval("imageCaption") %></p>
                                            </div>
                                        </div>

                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                            <a class="left carousel-control" href="#myCarousel" data-slide="prev">
                                <span class="glyphicon glyphicon-chevron-left"></span>
                                <span class="sr-only">Previous</span>
                            </a>
                            <a class="right carousel-control" href="#myCarousel" data-slide="next">
                                <span class="glyphicon glyphicon-chevron-right"></span>
                                <span class="sr-only">Next</span>
                            </a>
                        </div>
                        <div class="text-center res-shadow">
                            <img src="images/logo/shadow-banner.png" alt="logo">
                        </div>
                    </div>
                </div>
            </div>
        </section>
         
        
        

        <script src='http://www.pib.nic.in/js/jquery.flexisel.js'></script>
        <footer>
            <div class="footer-top">
                <div class="container">
                    <div class="row">
                        <script src="js/jssor.slider-27.5.0.min.js"></script>
                        <script>
                            jssor_1_slider_init = function () {

                                var jssor_1_options = {
                                    $AutoPlay: 1,
                                    $Idle: 0,
                                    $SlideDuration: 5000,
                                    $SlideEasing: $Jease$.$Linear,
                                    $PauseOnHover: 4,
                                    $SlideWidth: 140,
                                    $Align: 0
                                };
                                var jssor_1_slider = new $JssorSlider$("jssor_1", jssor_1_options);

                                /*#region responsive code begin*/

                                var MAX_WIDTH = 980;

                                function ScaleSlider() {
                                    var containerElement = jssor_1_slider.$Elmt.parentNode;
                                    var containerWidth = containerElement.clientWidth;

                                    if (containerWidth) {

                                        var expectedWidth = Math.min(MAX_WIDTH || containerWidth, containerWidth);

                                        jssor_1_slider.$ScaleWidth(expectedWidth);
                                    }
                                    else {
                                        window.setTimeout(ScaleSlider, 30);
                                    }
                                }

                                ScaleSlider();

                                $Jssor$.$AddEvent(window, "load", ScaleSlider);
                                $Jssor$.$AddEvent(window, "resize", ScaleSlider);
                                $Jssor$.$AddEvent(window, "orientationchange", ScaleSlider);
                                /*#endregion responsive code end*/
                            };
                        </script>
                        <style>
                            /*js or slid
       
                                         
                        -009-spin mg 
                                                              a imation-durat
                                 
                                nf n
                                    animation- iming-function:
                                  
                            pin {
                            from {
                              t
                                ansform  otate
                                0deg ;
                                                                 
                                                                                 t  
                                    
                                                                             ran
                                
                                                                                 
                                    }
                                                          }
                        </style>
                        <div id="jssor_1" style="position: relative; margin: 0 auto; top: 0px; left: 0px; width: 700px; height: 50px; overflow: hidden; visibility: hidden;">
                            <!-- Loading Screen -->
                            <div data-u="loading" class="jssorl-009-spin" style="position: absolute; top: 0px; left: 0px; width: 100%; height: 100%; text-align: center; background-color: rgba(0,0,0,0.7);">
                                <img alt="Loading" style="margin-top: -19px; position: relative; top: 50%; width: 38px; height: 38px;" src="img/spin.svg" />
                            </div>
                            <div data-u="slides" style="cursor: default; position: relative; top: 0px; left: 0px; width: 700px; height: 50px; overflow: hidden;">
                                <div>
                                    <a href="https://presidentofindia.nic.in/" title="The President of India, Shri Ram Nath Kovind"
                                        onclick="return confirm('This link will take you to a webpage outside this website interactive page. Click OK to continue. Click Cancel to stop')">
                                        <img class="img12 img-rounded" data-u="image" src="images/logo/ThePresidentofIndia.jpg" alt="The President of India, Shri Ram Nath Kovind" /></a>
                                </div>
                                <div>
                                    <a href="http://digitalindia.gov.in/" title="Ministry of Electronics & Information Technology Government of India"
                                        onclick="return confirm('This link will take you to a webpage outside this website interactive page. Click OK to continue. Click Cancel to stop')">
                                        <img class="img12 img-rounded" data-u="image" src="images/logo/digitalIndia.jpg" alt="Ministry of Electronics & Information Technology Government of India" /></a>
                                </div>
                                <div>
                                    <a href="http://mpvidhansabha.nic.in/" title="Madhya Pradesh Legislative Assembly"
                                        onclick="return confirm('This link will take you to a webpage outside this website interactive page. Click OK to continue. Click Cancel to stop')">
                                        <img class="img12 img-rounded" data-u="image" src="images/logo/mp_vidhan_sabha.jpg" alt="Madhya Pradesh Legislative Assembly" />
                                    </a>
                                </div>
                                <div>
                                    <a href="https://www.mygov.in/" title="Ministry of Home Affairs Government of India"
                                        onclick="return confirm('This link will take you to a webpage outside this website interactive page. Click OK to continue. Click Cancel to stop')">
                                        <img class="img12 img-rounded" data-u="image" src="images/logo/MyGov.jpg" alt="Ministry of Home Affairs Government of India" />
                                    </a>
                                </div>
                                <div>
                                    <a href="http://www.mp.gov.in/" title="Ministry of Home Affairs Government of Madhya Pradesh"
                                        onclick="return confirm('This link will take you to a webpage outside this website interactive page. Click OK to continue. Click Cancel to stop')">
                                        <img class="img12 img-rounded" data-u="image" src="images/logo/logo_mp.png" alt="Ministry of Home Affairs Government of Madhya Pradesh" />
                                    </a>
                                </div>
                                <div>
                                    <a href="https://www.india.gov.in/" title="National Portal of India"
                                        onclick="return confirm('This link will take you to a webpage outside this website interactive page. Click OK to continue. Click Cancel to stop')">
                                        <img class="img12 img-rounded" data-u="image" src="images/logo/national-portal.jpg" alt="National Portal of India" />
                                    </a>
                                </div>
                                <div>
                                    <a href="https://www.india.gov.in/" title="150th Years of Celebrating the Mahatma"
                                        onclick="return confirm('This link will take you to a webpage outside this website interactive page. Click OK to continue. Click Cancel to stop')">
                                        <img class="img12 img-rounded" data-u="image" src="images/logo/gandhi.png" alt="150th Years of Celebrating the Mahatma" />
                                    </a>
                                </div>
                            </div>
                        </div>
                        <script>jssor_1_slider_init();</script>
                    </div>
                </div>
            </div>
            
            <div class="footer-bottom">
                <div class="container">
                    <ul>
                        <li><a title='Home' href='Default.aspx'>Home</a></li>
                        <li><a title='About Us' href='AboutUs.aspx' target='_blank'>About Us</a></li>
                        <li><a title='Archives' href='Disclaimer.aspx'>Disclaimer</a></li>
                        <li><a title='Terms & Conditions' href='Terms_n_Condition.aspx'>Terms & Conditions</a></li>
                        <%--    <li><a title='RTI' href='RTI.aspx'>RTI</a></li>--%>
                        <li><a title='RTI' href='pdf/RTI2019.pdf'>RTI</a></li>
                        <li><a title='Site Map' href='sitemap.aspx'>Site Map</a></li>
                        <li><a title='Accessibility Statement' href='AccessibilityStatement.aspx'>Accessibility Statement</a></li>
                        <li><a title='Help' href='Help.aspx'>Help</a></li>
                        <li><a title='Contact Us' href='ContactUs.aspx'>Contact Us</a></li>
                    </ul>

                    <%--<p style="text-align: center;">Website Designed & Developed By National Informatics Centre (NIC).<img alt="NIC Logo" src="images/logo/niclogo.png" style="width: 100px;" /></p>
                    <p class="text-center"><span class="footer-nic">Copyright ©2017- All Rights Reserved - Raj Bhavan, Madhya Pradesh. Content on this website is published, managed & maintained by Governor's Secretariat Bhopal, Madhya Pradesh. </span></p>
                    <p style="text-align: center;">The desired screen resolution is 1024x768 or above. Site Best Viewed In Microsoft IE-6+, Mozilla Firefox, Google Chrome, Safari etc... </p>--%>

                    <p style="text-align: center;">
                        Raj Bhavan, Madhya Pradesh-All Rights Reserved&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Website Designed & Developed By <a href="http://www.mp.nic.in/" target="_blank">National Informatics Centre</a>.&nbsp;&nbsp;&nbsp;&nbsp;
                        <img alt="NIC Logo" src="images/logo/niclogo.png" style="width: 100px;" />
                    </p>
                    <%--<p class="text-center"><span class="footer-nic">Copyright ©2017- All Rights Reserved - Raj Bhavan, Madhya Pradesh. Content on this website is published, managed & maintained by Governor's Secretariat Bhopal, Madhya Pradesh. </span></p>--%>
                    <p style="text-align: center;">The desired screen resolution is 1024x768 or above. Site Best Viewed In Microsoft IE-6+, Mozilla Firefox, Google Chrome, Safari etc... </p>

                    <div class="clearfix"></div>
                </div>
            </div>
        </footer>
    </form>
    
</body>
</html>
