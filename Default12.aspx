<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="main.master" CodeFile="Default12.aspx.cs" Inherits="_Default12" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
    <style>
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
    <script type="text/javascript">
          function open_curtain() {
              $("#curtain1").animate({ width: 20 }, 30000);
              $("#curtain2").animate({ width: 20 }, 30000);
              document.getElementById("audio_mp3").play();
          }
          function close_curtain() {
              $("#curtain1").animate({ width: 800 }, 1000);
              $("#curtain2").animate({ width: 800 }, 1000);
              document.getElementById("audio_mp3").stop();
          }
    </script>
</asp:Content>

<asp:Content ContentPlaceHolderID="banner" runat="server">
    <div id="wrapper" onclick="open_curtain();">
        <section class="banner_wrapper">
            <div class="overlay_bg">
                <div class="container" style="min-height: 450px;">
                    <div class="col-xs-12 col-sm-6 col-md-8 col-lg-9">
                        <div id="carousel-example-generic" class="carousel slide slider" data-ride="carousel">
                            <ol class='carousel-indicators'>
                                <li data-target='#carousel-example-generic' data-slide-to='0' class='active'></li>
                                <li data-target='#carousel-example-generic' data-slide-to='1' class=''></li>
                                <li data-target='#carousel-example-generic' data-slide-to='2' class=''></li>
                                <li data-target='#carousel-example-generic' data-slide-to='3' class=''></li>
                                <li data-target='#carousel-example-generic' data-slide-to='4' class=''></li>
                                <li data-target='#carousel-example-generic' data-slide-to='5' class=''></li>
                                <li data-target='#carousel-example-generic' data-slide-to='6' class=''></li>
                            </ol>
                            <div class="carousel-inner">
                                <div class="item active">
                                    <img src="Images/MPWLC_Image/silo_3.jpeg">
                                </div>

                                <div class="item">
                                    <img src="Images/MPWLC_Image/MPWLC_6.jpeg">
                                    <%--<img src="assets/img/Indore.jpg" style="width: 100%;">--%>
                                </div>

                                <div class="item">
                                    <img src="Images/MPWLC_Image/silo_5.jpeg">
                                    <%--<img src="assets/img/MajgawanSagar.jpg" style="width: 100%;">--%>
                                </div>

                                <div class="item">
                                    <img src="Images/MPWLC_Image/MPWLC_2.jpeg">
                                    <%--<img src="assets/img/RehliSagar.jpg" style="width: 100%;">--%>
                                </div>

                                <div class="item">
                                    <img src="Images/MPWLC_Image/Silo_Bag_1.jpg">
                                    <%--<img src="assets/img/Satna.jpg" style="width: 100%;">--%>
                                </div>

                                <div class="item">
                                    <img src="Images/MPWLC_Image/Silo_5.jpeg">
                                    <%--<img src="assets/img/Sehore.jpg" style="width: 100%;">--%>
                                </div>

                                <div class="item">
                                    <img src="Images/MPWLC_Image/Silo_4.jpeg">
                                    <%--<img src="assets/img/Shahnagar.jpg" style="width: 100%;">--%>
                                </div>

                            </div>
                            <a class="left carousel-control" href="#carousel-example-generic" data-slide="prev">
                                <i class="fa fa-angle-left" aria-hidden="true"></i>
                            </a>
                            <a class="right carousel-control" href="#carousel-example-generic" data-slide="next">
                                <i class="fa fa-angle-right " aria-hidden="true"></i>
                            </a>
                        </div>
                    </div>
                    <div class="col-xs-12 col-sm-6 col-md-4 col-lg-3">
                    <div class="well minister_wrap">
                        <br />
                        <div class="row">
                            <div class="col-md-6">
                                <div class="thumbnail text-center">
                                    <img title="Food Minister of Food Civil Supplies and Consumer Protection." src="assets/img/Minister_2.jpg" class="img-responsive" style="height: 110px;">
                                    <strong>Shri Bisahulal</strong><br>
                                    <strong>Singh</strong><br />
                                    <small class="caps">Food Minister</small>
                                    <br />
                                </div>
                            </div>
                           
                            <div class="col-md-6">
                                <div class="thumbnail text-center">
                                    <img title="Chairman of Madhya pradesh warehousing & Logistics Corporation." src="assets/img/Chairman_2.jpeg" class="img-responsive" style="height: 110px;">
                                    <strong>Shri Rahul Singh</strong><br>
                                    <small class="caps">Chairman
                                    </small>
                                    <br />
                                    <br />
                                </div>
                            </div>
                        </div>
                         <br />
                        <div class="row">
                            <div class="col-md-6">
                                <div class="thumbnail text-center">
                                    <img title="Principal Secretary of Food Civil Supplies and Consumer Protection." src="assets/img/PS_3.jpg" class="img-circle" style="height: 110px;">
                                    <strong>Shri Faiz Ahmad Kidwai</strong><br>
                                    <small class="caps">PS Food</small>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="thumbnail text-center">
                                    <img title="Managing Director of Madhya pradesh warehousing & Logistics Corporation." src="assets/img/minister2.jpg" class="img-responsive" style="height: 110px;">
                                    <strong>Shri Tarun Kumar Pithode</strong><br>
                                    <small class="caps">MD</small>
                                </div>
                            </div>
                        </div>
                        <br />
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

                                                <p class="org"><span class="count">BHOPAL (M.P.)</span> </p>
                                            </div>
                                        </div>

                                        <div class="col-xs-12 col-sm-4 col-md-4 text-center count-div-brd">
                                            <div class="count-div">
                                                <p>Regional Offices</p>
                                                <p class="gree"><span class="count">8</span></p>
                                            </div>
                                        </div>

                                        <div class="col-xs-12 col-sm-4 col-md-4 text-center">
                                            <div class="count-div">
                                                <p>Branch Offices</p>
                                                <p class="blu"><span class="count">277</span> </p>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="item">
                                    <div class="row-fluid">
                                        <div class="col-xs-12 col-sm-4 col-md-4 text-center count-div-brd">
                                            <div class="count-div">
                                                <p>MPWLC Godowns</p>

                                                <p class="org"><span class="count">1522</span> </p>
                                            </div>
                                        </div>

                                        <div class="col-xs-12 col-sm-4 col-md-4 text-center ">
                                            <div class="count-div">
                                                <p>Private Godowns</p>

                                                <p class="gree"><span class="count">5291</span> </p>
                                            </div>
                                        </div>

                                        <div class="col-xs-12 col-sm-4 col-md-4 text-center count-div-brd">
                                            <div class="count-div">
                                                <p>Total Godowns</p>

                                                <p class="blu"><span class="count">6813</span> </p>
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
        </section>

        <%--</asp:Content>
<asp:Content ContentPlaceHolderID="body" runat="server">--%>
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
                                <li><a href="http://www.mpsc.mp.nic.in/Warehouse/login.aspx">JVS Application</a></li>
                                <li><a href="http://www.mpsc.mp.nic.in/Warehouse/inspections/">Online Inspection</a></li>
                            </ul>
                        </div>
                    </div>

                    <div class="col-md-6">

                        <div class="row-fluid">
                            <h3 class="red" style="letter-spacing: 1px;">Madhya Pradesh Warehousing & Logistics Corporation</h3>
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
                    <div class="col-md-3">
                        <div class="sidebar">
                            <ul class="list-unstyled side_nav" style="border: 1px solid #ccc;">
                                <li style="background: #d90406; color: white; text-transform: uppercase; font-weight: bold; padding: 0.5em; letter-spacing: 1px;">Current Update</li>
                                <li>Hon. Shri Bisahulal Singh Ji, Minister, Department of Food,Civil Supplies and Consumer Protection </li>
                                <li>Hon. Shri Rahul Singh Ji, Chairman, Madhaya Pradesh Warehousing & Logistics Corporation</li>
                                <li>Shri Faiz Ahmad Kidwai, Principal Secretary, Department of Food,Civil Supplies and Consumer Protection</li>
                                <li>Shri. Tarun Kumar Pithode, Managing Director, Madhaya Pradesh Warehousing & Logistics Corporation</li>
                                <%--<li><a href="http://mpwarehousing.com/news_image/1406024188Award%20Photo.pdf">Innovation in Polymers in Agriculture and Water Conservation in Silo Bags used for storage of Foodgrain.</a></li>--%>
                                <li><a href="http://mpwarehousing.com/news_image/1395127645EXCELLENCE%20AWARD.pdf">Excellence Award</a></li>
                                <li><a href="http://mpwarehousing.com/news_image/1395127605SiloBags.pdf">Silo Bags used for storage of Foodgrain</a></li>
                                <li><a href="http://mpwarehousing.com/news_image/1393673295MANTHAN%20AWARD.pdf">Manthan Award in IT Sector</a></li>
                                <li><a href="http://mpwarehousing.com/news_image/1393673191e-India%20PSE%20Awards.pdf">e-India PSE Awards.</a></li>

                            </ul>
                        </div>
                    </div>

                </div>

            </div>
            <!-- /container -->
        </section>

        <%--</asp:Content>--%>

        <%--<asp:Content ContentPlaceHolderID="widget" runat="server">--%>
        <section class="widget_wrapper">
            <div class="container">
                <div class="row-fluid">
                    <div class="col-md-4">
                        <img src="assets/img/info_red.png" class="icon">
                        <h4 class="title">MPWLC Info</h4>

                        <p class="text-justify" style="font-size: 15px;">
                            MP Warehouse and Logistics Corporation (MPWLC) is one of the oldest State Warehousing Corporation in the country .It was started with 8 regional offices and has now grown up to the extent of 277 Branches as at present with total Godown <b>6813</b> & his total capacity of <b>2,29,57,030</b>.
                        </p>
                        <p class="text-justify" style="font-size: 15px;">
                            MPWLC has own total Godown <b>1522</b> and his total capacity of <b>55,96,226</b>, private Godown <b>5291</b> and his total Capacity of  <b>1,73,60,804 </b>and total utilized capacity <b>1,95,96,901</b>.

                        </p>

                    </div>
                    <div class="col-md-4">
                        <img src="assets/img/services_red.png" class="icon">

                        <h4 class="title">Sevices we offer</h4>
                        <ul>
                            <li><a href="http://mpwarehousing.com/services.php">Scientific storage facility</a></li>
                            <li><a href="http://mpwarehousing.com/services.php">D.E.S.S.</a></li>
                        </ul>
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
         <img src="/curtain/C1.jpg" id="curtain1">
    <img src="/curtain/C2.jpg" id="curtain2">

    <audio id="audio_mp3" src="/curtain/music/MP_Gaan.mpeg" loop="loop"></audio>
    </div>
   
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>

</asp:Content>
