<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="curtain_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta name="viewport" content="width=device-width, height=device-height, initial-scale=1, user-scalable=no" />
    <link rel="shortcut icon" href="sites/default/files/indianembelem_0_0.png" type="image/png" />
    <link rel="alternate" type="application/rss+xml" title="Welcome" href="rss.xml" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
    <title>Welcome | Sugamya Portal Madhya Pradesh</title>
    <style media="all">
        @import url("/core/assets/vendor/normalize-css/normalize7298.css?ous34m");
        @import url("/core/themes/stable/css/system/components/ajax-progress.module7298.css?ous34m");
        @import url("/core/themes/stable/css/system/components/align.module7298.css?ous34m");
        @import url("/core/themes/stable/css/system/components/autocomplete-loading.module7298.css?ous34m");
        @import url("/core/themes/stable/css/system/components/fieldgroup.module7298.css?ous34m");
        @import url("/core/themes/stable/css/system/components/container-inline.module7298.css?ous34m");
        @import url("/core/themes/stable/css/system/components/clearfix.module7298.css?ous34m");
        @import url("/core/themes/stable/css/system/components/details.module7298.css?ous34m");
        @import url("/core/themes/stable/css/system/components/hidden.module7298.css?ous34m");
        @import url("/core/themes/stable/css/system/components/item-list.module7298.css?ous34m");
        @import url("/core/themes/stable/css/system/components/js.module7298.css?ous34m");
        @import url("/core/themes/stable/css/system/components/nowrap.module7298.css?ous34m");
        @import url("/core/themes/stable/css/system/components/position-container.module7298.css?ous34m");
        @import url("/core/themes/stable/css/system/components/progress.module7298.css?ous34m");
        @import url("/core/themes/stable/css/system/components/reset-appearance.module7298.css?ous34m");
        @import url("/core/themes/stable/css/system/components/resize.module7298.css?ous34m");
        @import url("/core/themes/stable/css/system/components/sticky-header.module7298.css?ous34m");
        @import url("/core/themes/stable/css/system/components/tabledrag.module7298.css?ous34m");
        @import url("/core/themes/stable/css/system/components/tablesort.module7298.css?ous34m");
        @import url("/core/themes/stable/css/system/components/tree-child.module7298.css?ous34m");
        @import url("/core/themes/stable/css/views/views.module7298.css?ous34m");
        @import url("/modules/views_slideshow/modules/views_slideshow_cycle/css/views_slideshow_cycle7298.css?ous34m");
        @import url("/modules/social_media_links/font-awesome/css/font-awesome.min7298.css?ous34m");
        @import url("/modules/jcarousel/assets/vendor/jcarousel/skins/default/jcarousel-default7298.css?ous34m");
        @import url("/modules/colorbox/styles/default/colorbox_style7298.css?ous34m");
        @import url("/modules/social_media_links/css/social_media_links.theme7298.css?ous34m");
        @import url("/modules/text_resize/text_resize7298.css?ous34m");
    </style>
    <style media="all">
        @import url("/core/themes/bartik/css/base/elements7298.css?ous34m");
        @import url("/core/themes/bartik/css/layout7298.css?ous34m");
        @import url("/core/themes/classy/css/components/action-links7298.css?ous34m");
        @import url("/core/themes/classy/css/components/breadcrumb7298.css?ous34m");
        @import url("/core/themes/classy/css/components/button7298.css?ous34m");
        @import url("/core/themes/classy/css/components/collapse-processed7298.css?ous34m");
        @import url("/core/themes/classy/css/components/container-inline7298.css?ous34m");
        @import url("/core/themes/classy/css/components/details7298.css?ous34m");
        @import url("/core/themes/classy/css/components/exposed-filters7298.css?ous34m");
        @import url("/core/themes/classy/css/components/field7298.css?ous34m");
        @import url("/core/themes/classy/css/components/form7298.css?ous34m");
        @import url("/core/themes/classy/css/components/icons7298.css?ous34m");
        @import url("/core/themes/classy/css/components/inline-form7298.css?ous34m");
        @import url("/core/themes/classy/css/components/item-list7298.css?ous34m");
        @import url("/core/themes/classy/css/components/link7298.css?ous34m");
        @import url("/core/themes/classy/css/components/links7298.css?ous34m");
        @import url("/core/themes/classy/css/components/menu7298.css?ous34m");
        @import url("/core/themes/classy/css/components/more-link7298.css?ous34m");
        @import url("/core/themes/classy/css/components/pager7298.css?ous34m");
        @import url("/core/themes/classy/css/components/tabledrag7298.css?ous34m");
        @import url("/core/themes/classy/css/components/tableselect7298.css?ous34m");
        @import url("/core/themes/classy/css/components/tablesort7298.css?ous34m");
        @import url("/core/themes/classy/css/components/tabs7298.css?ous34m");
        @import url("/core/themes/classy/css/components/textarea7298.css?ous34m");
        @import url("/core/themes/classy/css/components/ui-dialog7298.css?ous34m");
        @import url("/core/themes/classy/css/components/node7298.css?ous34m");
        @import url("/core/themes/classy/css/components/messages7298.css?ous34m");
        @import url("/core/themes/bartik/css/components/block7298.css?ous34m");
        @import url("/core/themes/bartik/css/components/book7298.css?ous34m");
        @import url("/core/themes/bartik/css/components/breadcrumb7298.css?ous34m");
        @import url("/core/themes/bartik/css/components/captions7298.css?ous34m");
    </style>
    <style media="all">
        @import url("/core/themes/bartik/css/components/comments7298.css?ous34m");
        @import url("/core/themes/bartik/css/components/contextual7298.css?ous34m");
        @import url("/core/themes/bartik/css/components/demo-block7298.css?ous34m");
        @import url("/core/themes/bartik/css/components/dropbutton.component7298.css?ous34m");
        @import url("/core/themes/bartik/css/components/featured-top7298.css?ous34m");
        @import url("/core/themes/bartik/css/components/feed-icon7298.css?ous34m");
        @import url("/core/themes/bartik/css/components/field7298.css?ous34m");
        @import url("/core/themes/bartik/css/components/form7298.css?ous34m");
        @import url("/core/themes/bartik/css/components/forum7298.css?ous34m");
        @import url("/core/themes/bartik/css/components/header7298.css?ous34m");
        @import url("/core/themes/bartik/css/components/help7298.css?ous34m");
        @import url("/core/themes/bartik/css/components/highlighted7298.css?ous34m");
        @import url("/core/themes/bartik/css/components/item-list7298.css?ous34m");
        @import url("/core/themes/bartik/css/components/list-group7298.css?ous34m");
        @import url("/core/themes/bartik/css/components/list7298.css?ous34m");
        @import url("/core/themes/bartik/css/components/main-content7298.css?ous34m");
        @import url("/core/themes/bartik/css/components/menu7298.css?ous34m");
        @import url("/core/themes/bartik/css/components/messages7298.css?ous34m");
        @import url("/core/themes/bartik/css/components/node7298.css?ous34m");
        @import url("/core/themes/bartik/css/components/node-preview7298.css?ous34m");
        @import url("/core/themes/bartik/css/components/page-title7298.css?ous34m");
        @import url("/core/themes/bartik/css/components/pager7298.css?ous34m");
        @import url("/core/themes/bartik/css/components/panel7298.css?ous34m");
        @import url("/core/themes/bartik/css/components/primary-menu7298.css?ous34m");
        @import url("/core/themes/bartik/css/components/search-form7298.css?ous34m");
        @import url("/core/themes/bartik/css/components/search-results7298.css?ous34m");
        @import url("/core/themes/bartik/css/components/secondary-menu7298.css?ous34m");
        @import url("/core/themes/bartik/css/components/shortcut7298.css?ous34m");
        @import url("/core/themes/bartik/css/components/skip-link7298.css?ous34m");
        @import url("/core/themes/bartik/css/components/sidebar7298.css?ous34m");
        @import url("/core/themes/bartik/css/components/site-branding7298.css?ous34m");
    </style>
    <style media="all">
        @import url("/core/themes/bartik/css/components/site-footer7298.css?ous34m");
        @import url("/core/themes/bartik/css/components/table7298.css?ous34m");
        @import url("/core/themes/bartik/css/components/tablesort-indicator7298.css?ous34m");
        @import url("/core/themes/bartik/css/components/tabs7298.css?ous34m");
        @import url("/core/themes/bartik/css/components/text-formatted7298.css?ous34m");
        @import url("/core/themes/bartik/css/components/toolbar7298.css?ous34m");
        @import url("/core/themes/bartik/css/components/featured-bottom7298.css?ous34m");
        @import url("/core/themes/bartik/css/components/password-suggestions7298.css?ous34m");
        @import url("/core/themes/bartik/css/components/ui.widget7298.css?ous34m");
        @import url("/core/themes/bartik/css/components/vertical-tabs.component7298.css?ous34m");
        @import url("/core/themes/bartik/css/components/views7298.css?ous34m");
        @import url("/core/themes/bartik/css/components/buttons7298.css?ous34m");
        @import url("/core/themes/bartik/css/components/image-button7298.css?ous34m");
        @import url("/core/themes/bartik/css/components/ui-dialog7298.css?ous34m");
        @import url("/sites/default/files/color/bartik-c066b813/colors7298.css?ous34m");
    </style>
    <style media="print">
        @import url("core/themes/bartik/css/print7298.css?ous34m");
    </style>
    <style>
        body {
            text-align: center;
            width: 100%;
            margin: 0 auto;
            padding: 0px;
            font-family: helvetica;
        }

        #wrapper {
            text-align: center;
            margin: 0 auto;
            padding: 0px;
            width: 100%;
        }

        #effect {
            position: relative;
            width: 60%;
            height: 280px;
            margin: auto;
            box-shadow: 0px 0px 6px 2px #003B83;
            margin-top: 100px;
            background: #FFF;
        }

        /*#effect h1 {
                line-height: 5.5em;
                /*margin: auto;
                font-size: 50px;
                color: #525252;
                height:810px;
                width:100%;
            }*/

        #curtain1 {
            top: 0px;
            position: absolute;
            left: 0px;
            height: 1000px;
            margin-top: -8px;
            margin-left: 10px;
        }

        #curtain2 {
            top: 0px;
            position: absolute;
            height: 1000px;
            right: 0px;
            margin-top: -8px;
            margin-right: 30px;
        }

        #curtain_buttons input[type="button"] {
            margin-top: 150px;
            width: 150px;
            height: 45px;
            border-radius: 2px;
            color: white;
            background-color: #0061BC;
            border: none;
            border-bottom: 6px solid #003D9C;
        }
    </style>
    <script src="<%= ResolveUrl("~/NEW_CSS/js/jquery-1.12.4.js") %>"></script>
    <script type="text/javascript">
        function open_curtain() {
            $("#curtain1").animate({ width: 20 }, 10000);
            $("#curtain2").animate({ width: 20 }, 10000);
            document.getElementById("audio_mp3").play();
        }
        function close_curtain() {
            $("#curtain1").animate({ width: 800 }, 1000);
            $("#curtain2").animate({ width: 800 }, 1000);
        }
    </script>
    <!--<script type="text/javascript">
        window.onload = function () {
            document.getElementById("audio_mp3").play();
        }
    </script>-->
</head>
<body background="image.png">
    <div id="wrapper" onclick="open_curtain();">

        <div id="effect" style="width: 1370PX; height: 1000PX; margin-top: -18px;">
            <!--<h1><img src="/Images/IMG-20181024-WA0010.jpg" id="img"></h1>-->
            <div id="main-wrapper" class="layout-main-wrapper layout-container clearfix">
                <div id="main" class="layout-main clearfix">
                    <main id="content" class="column main-content">
                        <div class="homeslider">
                            <div class="layout-container">
                                <div class="region region-homeslider">
                                    <div class="views-element-container block block-views block-views-blockmain-slider-block-1" id="block-views-block-main-slider-block-1-2">
                                        <div class="content">
                                            <div>

                                                <div id="myCarousel" class="carousel slide" data-ride="carousel">
                                                    <!-- Indicators -->
                                                    <ol class="carousel-indicators">
                                                        <li data-target="#myCarousel" data-slide-to="0" class="active"></li>
                                                        <li data-target="#myCarousel" data-slide-to="1"></li>
                                                        <li data-target="#myCarousel" data-slide-to="2"></li>
                                                        <li data-target="#myCarousel" data-slide-to="3"></li>
                                                        <%--
                                                <li data-target="#myCarousel" data-slide-to="4"></li>--%>
                                                        <li data-target="#myCarousel" data-slide-to="5"></li>
                                                        <li data-target="#myCarousel" data-slide-to="6"></li>
                                                        <li data-target="#myCarousel" data-slide-to="7"></li>
                                                        <li data-target="#myCarousel" data-slide-to="8"></li>
                                                        <li data-target="#myCarousel" data-slide-to="9"></li>
                                                        <li data-target="#myCarousel" data-slide-to="10"></li>
                                                        <li data-target="#myCarousel" data-slide-to="11"></li>
                                                    </ol>

                                                    <!-- Wrapper for slides -->
                                                    <div class="carousel-inner">
                                                        <div class="item active">
                                                            <img height="50%" src="/Images/IMG-20180920-WA0103.jpg" alt="Chania">
                                                            <div class="carousel-caption">
                                                            </div>
                                                        </div>

                                                        <div class="item">
                                                            <img height="50%" src="/Images/IMG-20181024-WA0004.jpg" alt="ECI">
                                                            <div class="carousel-caption">
                                                            </div>
                                                        </div>

                                                        <div class="item">
                                                            <img height="50%" src="/Images/IMG-20181024-WA0005.jpg" alt="New York">
                                                            <div class="carousel-caption">
                                                            </div>
                                                        </div>
                                                        <div class="item">
                                                            <img height="50%" src="/Images/IMG-20181024-WA0006.jpg" alt="New York">
                                                            <div class="carousel-caption">
                                                            </div>
                                                        </div>

                                                        <div class="item">
                                                            <img height="50%" src="/Images/IMG-20181024-WA0008.jpg" alt="New York">
                                                            <div class="carousel-caption">
                                                            </div>
                                                        </div>
                                                        <div class="item">
                                                            <img height="50%" src="/Images/IMG-20181024-WA0009.jpg" alt="New York">
                                                            <div class="carousel-caption">
                                                            </div>
                                                        </div>
                                                        <div class="item">
                                                            <img height="50%" src="/Images/IMG-20181024-WA0010.jpg" alt="New York">
                                                            <div class="carousel-caption">
                                                            </div>
                                                        </div>
                                                        <div class="item">
                                                            <img height="50%" src="/Images/IMG-20181024-WA0011.jpg" alt="New York">
                                                            <div class="carousel-caption">
                                                            </div>
                                                        </div>
                                                        <div class="item">
                                                            <img height="50%" src="/Images/IMG-20181024-WA0012.jpg" alt="New York">
                                                            <div class="carousel-caption">
                                                            </div>
                                                        </div>
                                                        <div class="item">
                                                            <img height="50%" src="/Images/IMG-20181024-WA0018.jpg" alt="New York">
                                                            <div class="carousel-caption">
                                                            </div>
                                                        </div>

                                                    </div>

                                                    <!-- Left and right controls -->
                                                    <a class="left carousel-control" href="#myCarousel" data-slide="prev">
                                                        <span class="glyphicon glyphicon-chevron-left"></span>
                                                        <span class="sr-only">Previous</span>
                                                    </a>
                                                    <a class="right carousel-control" href="#myCarousel" data-slide="next">
                                                        <span class="glyphicon glyphicon-chevron-right"></span>
                                                        <span class="sr-only">Next</span>
                                                    </a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </main>
                </div>
            </div>
            <!--<audio src="/music/good_enough.mp3" controls>
                <p>If you are reading this, it is because your browser does not support the audio element.</p>
            </audio>-->
            <img src="C1.jpg" id="curtain1">
            <img src="C2.jpg" id="curtain2">

            <audio id="audio_mp3" src="music/Audio.mp3" loop="loop"></audio>
        </div>

    </div>
</body>
</html>
