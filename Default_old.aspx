<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="main.master" CodeFile="Default_old.aspx.cs" Inherits="_Default" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
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
</asp:Content>

<asp:Content ContentPlaceHolderID="banner" runat="server">
     
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
    <%--<section class="banner_wrapper">
         <link href="css/bootstrap.min.css" rel="stylesheet" />
            <script src="js/jquery.min.js"></script>
            <script src="js/bootstrap.min.js"></script>
        <div class="overlay_bg">
            <div class="container">
                <div class="col-xs-12 col-sm-6 col-md-8 col-lg-9">
                    <div id="carousel-example-generic" class="carousel slide slider" data-ride="carousel">
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
                </div>
                <div class="col-xs-12 col-sm-6 col-md-4 col-lg-3">
                    <div class="well minister_wrap">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="thumbnail text-center">
                                    <img class="img-center img-circle" title="Food Minister of Food Civil Supplies and Consumer Protection." src="assets/img/minister1.jpg" class="img-rounded img-responsive" style="width: 80px; height: 80px;">
                                    <strong>Shri Bisahulal Singh</strong><br>
                                    <small class="caps">Food Minister</small>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="thumbnail text-center">
                                    <img class="img-center img-circle" src="assets/img/Chairman.jpeg" class="img-rounded img-responsive" style="width: 80px; height: 80px;">
                                    <strong>Shri Rahul Singh</strong><br>
                                    <small class="caps">Chairman</small>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="thumbnail text-center">
                                    <img src="assets/img/PS_Food.jpg" class="img-rounded img-circle" style="width: 80px; height: 80px;">
                                    <strong>Shri Faiz Ahmad Kidwai</strong><br>
                                    <small class="caps">PS Food</small>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="thumbnail text-center">
                                    <img src="assets/img/minister2.jpg" class="img-circle img-responsive" style="width: 80px; height: 80px;">
                                    <strong>Shri Tarun Kumar Pithode</strong><br>
                                    <small class="caps">MD</small>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>

            <div id="count">
                <div class="container counters">
                    <div class="carousel slide" id="myCarousel">
                        <div class="carousel-inner">
                            <div class="item active">
                                <div class="row-fluid">
                                    <div class="col-xs-12 col-sm-4 col-md-4 text-center count-div-brd">
                                        <div class="count-div">
                                            <p>Head Office</p>

                                            <p class="org"><span class="count">AT BHOPAL</span> </p>
                                        </div>
                                    </div>

                                    <div class="col-xs-12 col-sm-4 col-md-4 text-center count-div-brd">
                                        <div class="count-div">

                                            <p class="gree"><span class="count">8</span></p>
                                            <p>Regional Office</p>
                                        </div>
                                    </div>

                                    <div class="col-xs-12 col-sm-4 col-md-4 text-center">
                                        <div class="count-div">
                                            <p class="blu"><span class="count">270</span> </p>
                                            <p>Branch Office</p>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="item">
                                <div class="row-fluid">
                                    <div class="col-xs-12 col-sm-4 col-md-4 text-center count-div-brd">
                                        <div class="count-div">
                                            <p>MPWLC Godown</p>

                                            <p class="org"><span class="count">1516</span> </p>
                                        </div>
                                    </div>

                                    <div class="col-xs-12 col-sm-4 col-md-4 text-center ">
                                        <div class="count-div">
                                            <p>Private Godown</p>

                                            <p class="gree"><span class="count">4178</span> </p>
                                        </div>
                                    </div>

                                    <div class="col-xs-12 col-sm-4 col-md-4 text-center count-div-brd">
                                        <div class="count-div">
                                            <p>Total Godown</p>

                                            <p class="blu"><span class="count">5694</span> </p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <a class="left carousel-control" data-slide="prev" href="#myCarousel">‹</a> <a class="right carousel-control" data-slide="next" href="#myCarousel">›</a>
                    </div>

                </div>
            </div>
        </div>
    </section>--%>

</asp:Content>


<asp:Content ContentPlaceHolderID="body" runat="server">
    <section class="content_wrapper">
        <div class="container">
            <!-- Example row of columns -->
            <div class="row">
                <div class="col-md-3">
                    <div class="sidebar">
                        <ul class="list-unstyled side_nav" style="border: 1px solid #ccc;">
                            <li style="background: #d90406; color: white; text-transform: uppercase; font-weight: bold; padding: 0.5em; letter-spacing: 1px;">Quick Links</li>
                            <li><a href="PDF/JVS202021.pdf">Joint Venture Scheme for 2020-21</a></li>
                            <li><a href="http://mpwlp2012.com" target="_blank">Online Applications under MPWLP-2012</a></li>
                            <li><a href="Tender.aspx">Tenders</a></li>
                            <li><a href="Agreement.aspx">Agreement</a></li>
                            <li><a href="Circular.aspx">Orders, Letters & Circulars</a></li>
                            <li><a href="Appointment.aspx">Appointment Orders</a></li>
                            <li><a href="Recruitment.aspx">Recruitment</a></li>
                            <li><a href="BusinessReport.aspx">Business Reports</a></li>
                            <li><a href="RTIAct.aspx">The RTI Act 2005</a></li>
                            <li><a href="useful_file/1393827543mpwcl_ad.pdf">FAQ's</a></li>
                        </ul>
                    </div>
                </div>

                <div class="col-md-7">

                    <div class="row-fluid">
                        <h3 class="red" style="letter-spacing: 1px;">MP Warehousing & Logistics Corporation</h3>
                        <hr class="line-red" />
                        <p class="text-justify" style="font-size: 16px;">
                            MPWLC is running warehouses for the scientific storage of 
                agriculture and minors forest produce, seeds, manures, fertilizers, agricultural implements and notified 
                commodities offered by individuals, co-operative societies and other institutions. Corporation is committed 
                to provide scientific storage facilities with entire satisfaction of our customer.
                        </p>
                        <hr />



                        <ul class="nav nav-tabs">
                            <li class="active"><a data-toggle="tab" href="#use">USEFUL INFORMATION </a></li>
                            <li><a data-toggle="tab" href="#download">DOWNLOAD</a></li>
                        </ul>


                        <div class="tab-content">
                            <div id="use" class="tab-pane fade in active">
                                <p>
                                    <%--<ul class="list-unstyled">

                        <li>
                            <a href='http://www.mpwarehousing.com/ACHAL_SAMPATTI201819.pdf' target="_blank" class="heading"><i class="fa fa-hand-o-right red"></i>   Achal Sampatti Vivran Year 2018-19 </a>               
                        </li>
                        
                         <li>
                            <a href='http://mpwarehousing.com/useful_file/AcahalSampatti2018.pdf' target="_blank" class="heading"><i class="fa fa-hand-o-right red"></i>   Achal Sampatti Vivran Year 2017-18  </a>               
                            </li>

                        <li>
                            <a href='http://www.mptreasury.org/mpt/dynamic/cybertreasuryhome.htm'   target="_blank" class="heading"><i class="fa fa-hand-o-right red"></i>  Link of Cyber Treasury </a>               
                            </li>

                        <li>
                            <a href='admin/ajax/../../useful_file/15016549302435    - 21.7-17.pdf'  target="_blank" class="heading"><i class="fa fa-hand-o-right red"></i>  GST Ke Sambandh Me Disha Nirdesh 2435/21-7-2017 </a>              
                             </li>
                        
                        <li>
                        <a href='http://mpwarehousing.com/letter/1500016708GST.pdf'   target="_blank" class="heading"><i class="fa fa-hand-o-right red"></i>  GST Lagu Karne Ke sambandh Me  </a>               
                        </li>

                        <li>
                            <a href='admin/ajax/../../useful_file/1496918498Anubandh Ka Prarup.pdf'  target="_blank" class="heading"><i class="fa fa-hand-o-right red"></i>  Agreement of Godown Construction Under Trible Blocks</a>              
                        </li>

                        <li>
                            <a href='http://mpwarehousing.com/useful_file/AcahalSampatti2017.pdf'   target="_blank" class="heading"><i class="fa fa-hand-o-right red"></i>  Achal Sampatti 2016 - 17 </a>              
                        </li>

                        <li>
                            <a href='admin/ajax/../../useful_file/1474353932MPWLC MOU 2016-17.pdf'  target="_blank" class="heading"><i class="fa fa-hand-o-right red"></i>  MOU of MPWLC 2016-17  </a>              
                        </li>

                        <li>
                            <a href='http://mpwarehousing.com/useful_file/Achal_Sampatti_Vivran.pdf'   target="_blank" class="heading"><i class="fa fa-hand-o-right red"></i>  Achal Sampatti Vivran 2015-2016  </a>               
                        </li>

                        <li>
                            <a href='http://mpsc.mp.nic.in/Warehouse/audit/inspectionlogin.aspx'   target="_blank" class="heading"><i class="fa fa-hand-o-right red"></i>  Ankeshan prativedan(Audit) form(For Password Please contact 0755-2600505)</a>               
                        </li>

                        <li>
                             <a href='admin/ajax/../../useful_file/1455262093JVS Advertisment.pdf'  target="_blank" class="heading"><i class="fa fa-hand-o-right red"></i>  Advertisement for Registration and Submission of offer of Private Warehouses under Joint Venture Scheme 2016-17 </a>               
                        </li>

                        <li>
                            <a href='http://mpwarehousing.com/useful_file/Employee.pdf' target="_blank" class="heading"><i class="fa fa-hand-o-right red"></i>  Achal Sampatti Vivran 2014-2015  </a>               
                        </li>

                        <li>
                            <a href='http://www.ceomadhyapradesh.nic.in/' target="_blank" class="heading"><i class="fa fa-hand-o-right red"></i>  Link Your Voter ID with Adhar Card  </a>              
                        </li>

                        <li>
                            <a href='http://www.mponline.gov.in/Portal/Services/Warehousing/G2G/frmLstRegWarehouses.aspx' target="_blank" class="heading"><i class="fa fa-hand-o-right red"></i>  List of Pvt. Warehouse holders  </a>           
                        </li>

                        <li>
                          <a href='WLCMOU.doc'   target="_blank" class="heading"><i class="fa fa-hand-o-right red"></i>  MOU of MPWLC</a>               
                        </li>

                        <li>
                            <a href='PDF/Agreement.pdf'   target="_blank" class="heading"><i class="fa fa-hand-o-right red"></i>  Copy of Agreement under MPWLP-2012 </a>               
                        </li>

                        <li>
                              <a href='PDF/File_372.pdf'   target="_blank" class="heading"><i class="fa fa-hand-o-right red"></i>  Silo Project Through PPP At Ten Locations in Madhya Pradesh  </a>              
                       </li>

                        <li>
                           <a href='admin/ajax/../../useful_file/1393827491mpwcl_ad.pdf'  target="_blank" class="heading"><i class="fa fa-hand-o-right red"></i>  Advertisement for Submission of Online Application under M. P. Warehousing &amp; Logistics Policy-2012 </a>               
                        </li>

                        <li>
                           <a href='admin/ajax/../../useful_file/1393827543mpwcl_ad.pdf'  target="_blank" class="heading"><i class="fa fa-hand-o-right red"></i>  Frequently Ask Question (FAQs) </a>              
                        </li>

                        <li>
                            <a href=''   target="_blank" class="heading"><i class="fa fa-hand-o-right red"></i>  User Manual of Extension of IISFM Project to De-Centralized Procurement. </a>               
                        </li>

                        <li>
                            <a href='http://www.mpsc.mp.nic.in/Warehouse/login.aspx'   target="_blank" class="heading"><i class="fa fa-hand-o-right red"></i>  IISFM Software. </a>               
                        </li>

                        <li>
                             <a href='admin/ajax/../../useful_file/1393827729User_Mannual.pdf'  target="_blank" class="heading"><i class="fa fa-hand-o-right red"></i>  PEG Scheme 2009 - Guidline </a>               
                         </li>
                        <li>
                          <a href='https://www.nabard.org/english/home.aspx'   target="_blank" class="heading"><i class="fa fa-hand-o-right red"></i>  Rural Godown Scheme of Nabard  </a>               
                        </li>

                        <li>
                            <a href='https://www.nabard.org/english/home.aspx'   target="_blank" class="heading"><i class="fa fa-hand-o-right red"></i>  RIDF</a>               
                        </li>

                        <li>
                            <a href='http://agmarknet.nic.in/amrscheme/ruralhead.htm'   target="_blank" class="heading"><i class="fa fa-hand-o-right red"></i>  Farmers Facilitation Centre   </a>               
                        </li>
                      </ul>--%>
                                    <ul class="list-unstyled">

                                        <li>
                                            <a href='http://www.mpwarehousing.com/ACHAL_SAMPATTI201819.pdf' target="_blank" class="heading"><i class="fa fa-hand-o-right red"></i>Achal Sampatti Vivran Year 2018-19 </a>
                                        </li>

                                        <li>
                                            <a href='http://mpwarehousing.com/useful_file/AcahalSampatti2018.pdf' target="_blank" class="heading"><i class="fa fa-hand-o-right red"></i>Achal Sampatti Vivran Year 2017-18  </a>
                                        </li>

                                        <li>
                                            <a href='http://www.mptreasury.org/mpt/dynamic/cybertreasuryhome.htm' target="_blank" class="heading"><i class="fa fa-hand-o-right red"></i>Link of Cyber Treasury </a>
                                        </li>
                                        <li>
                                            <a href='http://mpwarehousing.com/letter/1500016708GST.pdf' target="_blank" class="heading"><i class="fa fa-hand-o-right red"></i>GST Lagu Karne Ke sambandh Me  </a>
                                        </li>
                                        <li>
                                            <a href='http://mpwarehousing.com/useful_file/AcahalSampatti2017.pdf' target="_blank" class="heading"><i class="fa fa-hand-o-right red"></i>Achal Sampatti 2016 - 17 </a>
                                        </li>
                                        <li>
                                            <a href='http://mpwarehousing.com/useful_file/Achal_Sampatti_Vivran.pdf' target="_blank" class="heading"><i class="fa fa-hand-o-right red"></i>Achal Sampatti Vivran 2015-2016  </a>
                                        </li>

                                        <li>
                                            <a href='http://mpsc.mp.nic.in/Warehouse/audit/inspectionlogin.aspx' target="_blank" class="heading"><i class="fa fa-hand-o-right red"></i>Ankeshan prativedan(Audit) form(For Password Please contact 0755-2600505)</a>
                                        </li>
                                        <li>
                                            <a href='http://mpwarehousing.com/useful_file/Employee.pdf' target="_blank" class="heading"><i class="fa fa-hand-o-right red"></i>Achal Sampatti Vivran 2014-2015  </a>
                                        </li>

                                        <li>
                                            <a href='http://www.ceomadhyapradesh.nic.in/' target="_blank" class="heading"><i class="fa fa-hand-o-right red"></i>Link Your Voter ID with Adhar Card  </a>
                                        </li>

                                        <li>
                                            <a href='http://www.mponline.gov.in/Portal/Services/Warehousing/G2G/frmLstRegWarehouses.aspx' target="_blank" class="heading"><i class="fa fa-hand-o-right red"></i>List of Pvt. Warehouse holders  </a>
                                        </li>
                                        <li>
                                            <a href='admin/ajax/../../useful_file/1393827543mpwcl_ad.pdf' target="_blank" class="heading"><i class="fa fa-hand-o-right red"></i>Frequently Ask Question (FAQs) </a>
                                        </li>

                                        <li>
                                            <a href='http://www.mpsc.mp.nic.in/Warehouse/login.aspx' target="_blank" class="heading"><i class="fa fa-hand-o-right red"></i>IISFM Software. </a>
                                        </li>

                                        <li>
                                            <a href='https://www.nabard.org/english/home.aspx' target="_blank" class="heading"><i class="fa fa-hand-o-right red"></i>Rural Godown Scheme of Nabard  </a>
                                        </li>

                                    </ul>
                                </p>
                            </div>
                            <div id="download" class="tab-pane fade">
                                <asp:Repeater ID="rptDownload" runat="server">
                                    <HeaderTemplate>
                                        <ul class="list-unstyled">
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <li>
                                            <i class="fa fa-download red"></i>
                                            <a href='<%# "Admin/download_file/" + Eval("Filename")%>' target="_blank"><%#Eval("Title") %></a>
                                        </li>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </ul>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </div>
                            <div id="menu2" class="tab-pane fade">
                            </div>
                        </div>
                    </div>


                </div>

                <div class="col-md-2">
                    <div class="row-fluid">

                        <asp:Repeater ID="rptNewsUpdate" Visible="false" runat="server">
                            <HeaderTemplate>
                                <h5 class="red"><strong>CURRENT UPDATES</strong></h5>
                                <hr class="line-red-left" />
                                <marquee direction="up" scrollamount="2" onmouseover="this.stop();" onmouseout="this.start();" class="news_box">
                  <ul class="list-unstyled">
                            </HeaderTemplate>
                            <ItemTemplate>
                                <li><i class="fa fa-newspaper-o red"></i><a href='<%# "NewsDetails.aspx?Id=" + Eval("Id")%>'><%#Eval("Title") %></a></li>

                                <%--<a href='<%# "Admin/news_file/" + Eval("Filename")%>'>--%>
                            </ItemTemplate>
                            <FooterTemplate>
                                </ul>
                </marquee>
                            </FooterTemplate>
                        </asp:Repeater>

                    </div>

                    <div class="row-fluid">
                        <h5 class="red"><strong>GALLERY</strong></h5>
                        <hr class="line-red-left" />
                        <img src="assets/img/Dewas.jpg" class="img-responsive">
                    </div>





                </div>



            </div>

        </div>
        <!-- /container -->
    </section>

</asp:Content>

<asp:Content ContentPlaceHolderID="widget" runat="server">
    <section class="widget_wrapper">
        <div class="container">
            <div class="row-fluid">
                <div class="col-md-4">
                    <img src="assets/img/info_red.png" class="icon">
                    <h4 class="title">MPWLC Info</h4>

                    <p class="text-justify" style="font-size: 15px;">
                        MP Warehouse and Logistics Corporation (MPWLC) is one of the oldest State Warehousing Corporation in the country .It was started with 8 regional offices and has now grown up to the extent of 270 Branches as at present with total Godown <b>5694</b> & his total capacity of <b>1,57,85,853</b>.
                    </p>
                    <p class="text-justify" style="font-size: 15px;">
                        MPWLC has own total Godown <b>1516</b> and his total capacity of <b>36,14,554</b>, private Godown <b>4178</b> and his total Capacity of  <b>1,21,71,299</b>.

                    </p>
                </div>
                <div class="col-md-4">
                    <img src="assets/img/services_red.png" class="icon">

                    <h4 class="title">Sevices we offer</h4>
                    <%-- <ul>
                <li><a href="#">Scientific storage facility</a></li>
                <li><a href="#">D.E.S.S.</a></li>
                <li><a href="#">License for Warehousing</a></li>
              </ul>--%>
                </div>
                <div class="col-md-4">
                    <img src="assets/img/network_red.png" class="icon">

                    <h4 class="title">Our Network</h4>
                    <ul class="list-unstyled">
                        <li><i class="fa fa-caret-right red"></i><a href="BranchContact.aspx">District wise</a></li>
                        <li><i class="fa fa-caret-right red"></i><a href="RegionalContact.aspx">Region wise</a>
                            <ul>
                                <li>Bhopal</li>
                                <li>Indore</li>
                                <li>Ujjain</li>
                                <li>Jablpur</li>
                                <li>Gwalior</li>
                                <li>Sagar</li>
                                <li>Rewa</li>
                                <li>Narmadapuram</li>
                            </ul>
                        </li>
                        <li><a href="GeographicalView.aspx">Geography wise</a></li>
                    </ul>
                </div>
            </div>
        </div>

    </section>
</asp:Content>
