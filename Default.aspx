<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/main.master" CodeFile="~/Default.aspx.cs" Inherits="Default" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="banner" runat="server">
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
                <div class="col-xs-12 col-sm-6 col-md-4 col-lg-3" style="font-size: 10px;">
                    <div class="well minister_wrap">
                        <div class="row">
                            <div class="col-md-6" style="margin-left: 80px;">
                                <%--<div class="thumbnail text-center">
                                    <img title="Food Minister of Food Civil Supplies and Consumer Protection." src="assets/img/CM_Image.jpg" class="img-responsive" style="height: 100px;">
                                    <strong>Shri shivraj singh chauhan</strong><br>
                                    <small class="caps">Chief Minister</small>
                                    <br />
                                </div>--%>
                            </div>
                        </div>

                        <div class="row" style="display: flex; flex-wrap: wrap; margin: 0 -12px;">

                            <!-- Profile 1 -->
                            <div style="flex: 0 0 50%; max-width: 50%; padding: 0 12px; margin-bottom: 24px;">
                                <div style="text-align: center; padding: 3px; background: #ffffff; border-radius: 16px; box-shadow: 0 4px 12px rgba(0,0,0,0.08); transition: transform 0.2s; border: 1px solid #eef2f6;">
                                    <img title="Principal Secretary of Food Civil Supplies and Consumer Protection."
                                        src="assets/img/WhatsApp Image 2026-05-07 at 5.45.04 PM.jpeg"
                                        style="height: 160px; width: 160px; object-fit: cover; border-radius: 50%; border: 2px solid #e9ecef;">
                                    <div style="margin-top: 6px;">
                                        <strong style="display: block; font-size: 1.25rem; font-weight: 700; color: #1e293b;">Shri Govind Singh Rajput</strong>
                                        <small style="display: inline-block; font-size: 1.2rem; text-transform: uppercase; letter-spacing: 1px; color: #000; background: #f8f9fa;">Food Minister
                                            <br />
                                            [ Food Civil Supplies and Consumer Protection ]</small>
                                    </div>
                                </div>
                            </div>

                            <!-- Profile 2 -->
                            <div style="flex: 0 0 50%; max-width: 50%; padding: 0 12px; margin-bottom: 24px;">

                                <div style="text-align: center; padding: 3px; background: #ffffff; border-radius: 16px; box-shadow: 0 4px 12px rgba(0,0,0,0.08); transition: transform 0.2s; border: 1px solid #eef2f6;">
                                    <img title="Principal Secretary of Food Civil Supplies and Consumer Protection."
                                        src="assets/img/WhatsApp Image 2026-05-07 at 5.42.09 PM.jpeg"
                                        style="height: 160px; width: 160px; object-fit: cover; border-radius: 50%; border: 2px solid #e9ecef;">
                                    <div style="margin-top: 6px;">
                                        <strong style="display: block; font-size: 1.35rem; font-weight: 700; color: #1e293b;">Shri Sanjay Nagayach</strong>
                                        <small style="display: inline-block; font-size: 1.2rem; text-transform: uppercase; letter-spacing: 1px; color: #000; background: #f8f9fa;">Chairman<br />
                                            [ Madhya pradesh warehousing & Logistics Corporation ]</small>
                                    </div>
                                </div>
                            </div>

                            <!-- Profile 3 -->
                            <div style="flex: 0 0 50%; max-width: 50%; padding: 0 12px; margin-bottom: 24px;">
                                <div style="text-align: center; background: #ffffff; padding: 3px; border-radius: 16px; box-shadow: 0 4px 12px rgba(0,0,0,0.08); transition: transform 0.2s; border: 1px solid #eef2f6;">
                                    <img title="Principal Secretary of Food Civil Supplies and Consumer Protection."
                                        src="assets/img/WhatsApp Image 2026-05-07 at 5.48.49 PM.jpeg"
                                        style="height: 160px; width: 160px; object-fit: cover; border-radius: 50%; border: 2px solid #e9ecef;">
                                    <div style="margin-top: 6px;">
                                        <strong style="display: block; font-size: 1.25rem; font-weight: 700; color: #1e293b;">Smt.Rashmi Arun Shami (IAS)</strong>
                                        <small style="display: inline-block; font-size: 1.2rem; text-transform: uppercase; letter-spacing: 1px; color: #000; background: #f8f9fa;">Additional Chief Secretary
                                            <br />
                                            [Food Civil Supplies and Consumer Protection]
                                        </small>
                                    </div>
                                </div>
                            </div>

                            <!-- Profile 4 -->
                            <div style="flex: 0 0 50%; max-width: 50%; padding: 0 12px; margin-bottom: 24px;">

                                <div style="text-align: center; background: #ffffff; padding: 3px; border-radius: 16px; box-shadow: 0 4px 12px rgba(0,0,0,0.08); transition: transform 0.2s; border: 1px solid #eef2f6;">
                                    <img title="Principal Secretary of Food Civil Supplies and Consumer Protection."
                                        src="assets/img/WhatsApp Image 2026-05-08 at 4.20.43 PM.jpeg"
                                        style="height: 160px; width: 160px; object-fit: cover; border-radius: 50%; border: 2px solid #e9ecef;">
                                    <div style="margin-top: 6px;">
                                        <strong style="display: block; font-size: 1.25rem; font-weight: 700; color: #1e293b;">Shri Anurag Verma(IAS)</strong>
                                        <small style="display: inline-block; font-size: 1.2rem; text-transform: uppercase; letter-spacing: 1px; color: #000; background: #f8f9fa;">MD
                                            <br />
                                            [ Madhya pradesh warehousing & Logistics Corporation ]</small>
                                    </div>
                                </div>
                            </div>

                        </div>


                        <%-- <div class="row">
                            <div class="col-md-6">
                                <div class="thumbnail text-center">
                                    <img title="Food Minister of Food Civil Supplies and Consumer Protection." src="assets/img/Govind_Singh_Rajput_Big.jpg" class="img-responsive" style="height: 100px;">
                                    <strong>Shri Govind Singh Rajput</strong><br>
                                   <br />
                                    <small class="caps">Food Minister</small>
                                    <br />
                                </div>
                            </div>

                            <div class="col-md-6">
                                <div class="thumbnail text-center">
                                    <img title="Chairman of Madhya pradesh warehousing & Logistics Corporation." src="assets/img/Chaiman2026.jpeg" class="img-responsive" style="height: 100px;">
                                    <strong>Shri Sanjay Nagayach</strong><br>
                                    <small class="caps">Chairman
                                    </small>
                                    <br />
                                    <br />
                                </div>
                            </div>
                        </div>--%>

                        <%-- <div class="row">
                            <div class="col-md-12">
                                <div class="thumbnail text-center">
                                    <img title="Food Minister of Food Civil Supplies and Consumer Protection." src="assets/img/Govind_Singh_Rajput_Big.jpg" style="height: 180px; width: 150px;">
                                    <strong>Shri Govind Singh Rajput 
                                    <br>                                       
                                        <small class="caps">Minister of Food, Civil Supplies and Consumer Protection<br />
                                            Chairman (MPWLC)</small></strong>
                                    <br />
                                </div>
                            </div>
                        </div>--%>

                        <%-- <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-6">
                                    <div class="thumbnail text-center">
                                        <img title="Principal Secretary of Food Civil Supplies and Consumer Protection." src="assets/img/PSFOOD_2024.jpeg" class="img-responsive" style="height: 100px;">
                                        <strong>Smt. Smita </strong><br>
                                        <strong>Bharadwaj</strong><br />
                                        <small class="caps">PS Food</small>
                                        <br />
                                    </div>
                                </div>

                                <div class="col-md-6">
                                    <div class="thumbnail text-center">
                                        <img title="Managing Director of Madhya pradesh warehousing & Logistics Corporation." src="assets/img/MDMPWLC2024.jpeg" class="img-responsive" style="height: 100px;">
                                        <strong>Shri Ravindra Singh</strong><br>
                                        <small class="caps">MD
                                        </small>
                                        <br />
                                        <br />
                                    </div>
                                </div>
                            </div>
                        </div>--%>

                        <%--  <div class="row">
                            <div class="col-md-6">
                                <div class="thumbnail text-center">
                                    <img title="Principal Secretary of Food Civil Supplies and Consumer Protection." src="assets/img/PSFOOD2024.PNG" class="img-responsive" style="height: 160px;">
                                    <strong>Smt.Rashmi Arun Shami (IAS)<br>
                                        <small class="caps">ACS Food<br />
                                        </small>

                                    </strong>
                                    <br />
                                </div>
                            </div>--%>
                        <%-- </div>
                         <div class="row">--%>
                        <%--   <div class="col-md-6">
                                <div class="thumbnail text-center">
                                    <img title="Managing Director of Madhya pradesh warehousing & Logistics Corporation." src="assets/img/MD2025_AnuragVerma.jpeg" class="img-responsive" style="height: 160px;">
                                    <strong>Shri Anurag Verma(IAS)<br>
                                        <small class="caps">MD MPWLC
                                        </small></strong>
                                    <br />
                                    <br />
                                </div>
                            </div>
                        </div>--%>

                        <%--<div class="row">
                            <div class="col-md-12">
                                <div class="thumbnail text-center">
                                    <img title="Chairman of Madhya pradesh warehousing & Logistics Corporation." src="assets/img/Chairman_2.jpeg" class="img-responsive" style="height: 180px;">
                                    <strong>Shri Rahul Singh</strong><br>
                                    <small class="caps">Chairman
                                    </small>
                                </div>
                            </div>
                        </div>--%>
                        <%-- <div class="row">
                            <div class="col-md-12">
                                <div class="thumbnail text-center">
                                    <img title="Principal Secretary of Food Civil Supplies and Consumer Protection." src="assets/img/PSFOOD_2024.jpeg" class="img-responsive" style="height: 180px;">
                                    <strong>Shri Rahul Singh</strong><br>
                                    <small class="caps">Chairman
                                    </small>
                                </div>
                            </div>
                        </div>--%>

                        <%-- <div class="row">
                            <div class="col-md-12">
                                <div class="thumbnail text-center">
                                    <img title="Principal Secretary of Food Civil Supplies and Consumer Protection." src="assets/img/UmakantUmrao_PS_Food.jpg" style="height: 180px; width:150px;">
                                    <strong>Shri Umakant Umrao</strong><br>
                                    <small class="caps">PS Food</small>
                                </div>
                          </div>
                            </div>
                            <div class="row">
                            <div class="col-md-12">
                                <div class="thumbnail text-center">
                                    <img title="Managing Director of Madhya pradesh warehousing & Logistics Corporation." src="assets/img/MD_MPWLC.jpg" class="img-responsive" style="height: 180px;">
                                    <strong>Shri Deepak Saxena</strong><br>
                                    <small class="caps">MD</small>
                                </div>
                            </div>
                        </div>--%>
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
                                            <p class="blu"><span class="count">288</span> </p>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="item">
                                <div class="row-fluid">
                                    <div class="col-xs-12 col-sm-4 col-md-4 text-center count-div-brd">
                                        <div class="count-div">
                                            <p>MPWLC Godowns</p>
                                            <p class="org"><span class="count">1604</span> </p>
                                        </div>
                                    </div>
                                    <div class="col-xs-12 col-sm-4 col-md-4 text-center ">
                                        <div class="count-div">
                                            <p>Private Godowns</p>
                                            <p class="gree"><span class="count">7048</span> </p>
                                        </div>
                                    </div>
                                    <div class="col-xs-12 col-sm-4 col-md-4 text-center count-div-brd">
                                        <div class="count-div">
                                            <p>Total Godowns</p>
                                            <p class="blu"><span class="count">9891</span> </p>
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
</asp:Content>


<asp:Content ContentPlaceHolderID="body" runat="server">
    <section class="content_wrapper">
        <div class="container">
            <!-- Example row of columns -->
            <div class="row">
                <div class="col-md-3">
                    <span style="margin-left: 260px;">
                        <%--<img src="Images/animated-new-image-0151.gif" />--%></span>
                </div>

                <div class="col-md-9">
                    <marquee>
                        <h3 class="red" style="letter-spacing: 1px;">
                            <%-- &nbsp;&nbsp; || &nbsp;&nbsp;<a href="http://mpsc.mp.nic.in/Warehouse/JointVentureScheme/JointVentureSchemeApp.aspx" runat="server">Online Choice Filling For JVS 2022-23 have started From 20/12/2021</a>--%>
                            <%-- &nbsp;&nbsp; || &nbsp;&nbsp;<a href="http://mpsc.mp.nic.in/Warehouse/JointVentureScheme/JointVentureSchemeApp.aspx" runat="server">Online Choice Filling For JVS 2022-23 have started From 20/12/2021</a>--%>
                            <%-- &nbsp;&nbsp; || &nbsp;&nbsp;<a href="https://mpwarehousing.mp.gov.in/Warehouse/QCRegistration/RegistrationForm.aspx" runat="server">स्कंध की गुणवत्ता जांच के लिए प्रशिक्षण हेतु रजिस्ट्रेशन</a>--%>
                            &nbsp;&nbsp; || &nbsp;&nbsp;<a href="https://mpwarehousing.mp.gov.in/Warehouse/JointVentureScheme/JointVentureSchemeApp.aspx" runat="server">JVS Scheme Offer For 2026-27 has started from 18/03/2026 </a>
                            <%--&nbsp;&nbsp; || &nbsp;&nbsp;<a href="/Upload/2318001.pdf" runat="server" target="_blank">विशेष भर्ती अभियान अंंतर्गत दिव्‍यांगजनों के रिक्‍त पदों की पूर्ति हेतु विज्ञप्ति।</a>--%>
                            <%--&nbsp;&nbsp; || &nbsp;&nbsp;<a href="/Upload/JVS_Policy_2025_26_Pdf_0002.pdf" unat="server">रबी सीजन 2025-26 हेतु नवीन JVS पालिसी के तहत गोदामों के ऑफर हेतु पालिसी </a>--%>
                            &nbsp;&nbsp; || &nbsp;&nbsp;<a href="/Upload/JVS_Policy_2026_27.pdf" unat="server">रबी सीजन 2026-27 हेतु नवीन JVS पालिसी के तहत गोदामों के ऑफर हेतु पालिसी </a>
                            <%-- &nbsp;&nbsp; || &nbsp;&nbsp;<a href="https://mpwarehousing.mp.gov.in/Warehouse/Inspections/" runat="server">Online Inspection(PV/General) for Warehouse</a>
                            &nbsp;&nbsp; || &nbsp;&nbsp;<span style="color: tomato;">जिन गोदामों में लम्बे समय से स्कन्ध रखा हुआ हैं एवं यदि किसी गोदाम से FIFO का पालन किये बिना निकासी की जा रही हैं तो आप अपनी शिकायत WMS Portal के Login पर दी गई लिंक के माध्यम से उच्च स्तर पर कर सकते हैं।</span>--%>
                            <%--&nbsp;&nbsp; || &nbsp;&nbsp;<a href="/Upload/NEW JVS KHARIF POLICY 2022-23.pdf" runat="server">खरीफ सीजन 2022-23 हेतु नवीन JVS पालिसी के तहत गोदामों के ऑफर हेतु पालिसी </a>--%>
                        </h3></marquee>
                </div>
            </div>
            <div class="row">
                <div class="col-md-3">
                    <span style="margin-left: 260px;">
                        <%--<img src="Images/animated-new-image-0151.gif" />--%></span>
                </div>
                <div class="col-md-9">
                    <marquee>
                        <h3 class="red" style="letter-spacing: 1px;">
                            <%-- &nbsp;&nbsp; || &nbsp;&nbsp;<a href="http://mpsc.mp.nic.in/Warehouse/JointVentureScheme/JointVentureSchemeApp.aspx" runat="server">Online Choice Filling For JVS 2022-23 have started From 20/12/2021</a>--%>
                            <%-- &nbsp;&nbsp; || &nbsp;&nbsp;<a href="http://mpsc.mp.nic.in/Warehouse/JointVentureScheme/JointVentureSchemeApp.aspx" runat="server">Online Choice Filling For JVS 2022-23 have started From 20/12/2021</a>--%>
                            <%-- &nbsp;&nbsp; || &nbsp;&nbsp;<a href="https://mpwarehousing.mp.gov.in/Warehouse/QCRegistration/RegistrationForm.aspx" runat="server">स्कंध की गुणवत्ता जांच के लिए प्रशिक्षण हेतु रजिस्ट्रेशन</a>--%>
                            <%-- &nbsp;&nbsp; || &nbsp;&nbsp;<a href="https://mpwarehousing.mp.gov.in/Warehouse/JointVentureScheme/JointVentureSchemeApp.aspx" runat="server">JVS Scheme Offer For 2024-25 has started from 25/10/2024 </a>
                            &nbsp;&nbsp; || &nbsp;&nbsp;<a href="/Upload/2318001.pdf" runat="server" target="_blank">विशेष भर्ती अभियान अंंतर्गत दिव्‍यांगजनों के रिक्‍त पदों की पूर्ति हेतु विज्ञप्ति।</a>--%>
                            <%-- &nbsp;&nbsp; || &nbsp;&nbsp;<span><img src="Images/animated-new-image-0151.gif" /></span><a href="/Upload/11zon_merged-PDF_11zon.pdf" unat="server">दिव्‍यांगजनों के विशेष भर्ती अभियांन अंतर्गत तृतीय श्रेणी के सेवा हेतु दिनांक15.01.2025 को प्रात: 11.00 बजे से वॉक-इन-इंटरव्‍यू की तीथि निर्धारित है। चतुर्थश्रेणी के पदों हेतु दिनांक 16.01.2025 एवं 17.01.2025 को प्रात: 11.00 बजे से वॉक-इन-इंटरव्‍यूकी निर्धारित है। इस हेतु उपयुक्‍त पाये गये उम्‍मीदवारों का वॉक-इन-इंटरव्‍यू हेतुआमंत्रित किया गया है। सूचना पत्र संलग्‍न है। </a>
                            <span>
                                <img src="Images/animated-new-image-0151.gif" /></span>--%>
                            <%-- &nbsp;&nbsp; || &nbsp;&nbsp;<a href="https://mpwarehousing.mp.gov.in/Warehouse/Inspections/" runat="server">Online Inspection(PV/General) for Warehouse</a>
                            &nbsp;&nbsp; || &nbsp;&nbsp;<span style="color: tomato;">जिन गोदामों में लम्बे समय से स्कन्ध रखा हुआ हैं एवं यदि किसी गोदाम से FIFO का पालन किये बिना निकासी की जा रही हैं तो आप अपनी शिकायत WMS Portal के Login पर दी गई लिंक के माध्यम से उच्च स्तर पर कर सकते हैं।</span>--%>
                            <%--&nbsp;&nbsp; || &nbsp;&nbsp;<a href="/Upload/NEW JVS KHARIF POLICY 2022-23.pdf" runat="server">खरीफ सीजन 2022-23 हेतु नवीन JVS पालिसी के तहत गोदामों के ऑफर हेतु पालिसी </a>--%>
                        </h3></marquee>
                </div>
            </div>
            <div class="row">
                <div class="col-md-3">
                    <span style="margin-left: 260px;">
                        <%--<img src="Images/animated-new-image-0151.gif" />--%></span>
                </div>
                <div class="col-md-9">
                    <marquee>
                        <h3 class="red" style="letter-spacing: 1px;">
                            <%-- &nbsp;&nbsp; || &nbsp;&nbsp;<a href="http://mpsc.mp.nic.in/Warehouse/JointVentureScheme/JointVentureSchemeApp.aspx" runat="server">Online Choice Filling For JVS 2022-23 have started From 20/12/2021</a>--%>
                            <%-- &nbsp;&nbsp; || &nbsp;&nbsp;<a href="http://mpsc.mp.nic.in/Warehouse/JointVentureScheme/JointVentureSchemeApp.aspx" runat="server">Online Choice Filling For JVS 2022-23 have started From 20/12/2021</a>--%>
                            <%-- &nbsp;&nbsp; || &nbsp;&nbsp;<a href="https://mpwarehousing.mp.gov.in/Warehouse/QCRegistration/RegistrationForm.aspx" runat="server">स्कंध की गुणवत्ता जांच के लिए प्रशिक्षण हेतु रजिस्ट्रेशन</a>--%>
                            <%-- &nbsp;&nbsp; || &nbsp;&nbsp;<a href="https://mpwarehousing.mp.gov.in/Warehouse/JointVentureScheme/JointVentureSchemeApp.aspx" runat="server">JVS Scheme Offer For 2024-25 has started from 25/10/2024 </a>
                            &nbsp;&nbsp; || &nbsp;&nbsp;<a href="/Upload/2318001.pdf" runat="server" target="_blank">विशेष भर्ती अभियान अंंतर्गत दिव्‍यांगजनों के रिक्‍त पदों की पूर्ति हेतु विज्ञप्ति।</a>--%>
                            <%-- &nbsp;&nbsp; || &nbsp;&nbsp;<span><img src="Images/animated-new-image-0151.gif" /></span><a href="/Upload/Dastavej.pdf" unat="server">विशेष भर्ती अभियान अंतर्गत दिव्‍यांगजनों के प्राप्‍त आवेदन पत्रों के साथ विज्ञप्ति की शर्त अनुसार संलग्‍न/वॉक इन इंटरव्‍यू में प्रस्‍तुत किये जाने वाले दस्‍तावेजों की सूची। </a>
                            <span>
                                <img src="Images/animated-new-image-0151.gif" /></span>--%>
                            <%-- &nbsp;&nbsp; || &nbsp;&nbsp;<a href="https://mpwarehousing.mp.gov.in/Warehouse/Inspections/" runat="server">Online Inspection(PV/General) for Warehouse</a>
                            &nbsp;&nbsp; || &nbsp;&nbsp;<span style="color: tomato;">जिन गोदामों में लम्बे समय से स्कन्ध रखा हुआ हैं एवं यदि किसी गोदाम से FIFO का पालन किये बिना निकासी की जा रही हैं तो आप अपनी शिकायत WMS Portal के Login पर दी गई लिंक के माध्यम से उच्च स्तर पर कर सकते हैं।</span>--%>
                            <%--&nbsp;&nbsp; || &nbsp;&nbsp;<a href="/Upload/NEW JVS KHARIF POLICY 2022-23.pdf" runat="server">खरीफ सीजन 2022-23 हेतु नवीन JVS पालिसी के तहत गोदामों के ऑफर हेतु पालिसी </a>--%>
                        </h3></marquee>
                </div>
            </div>
            <div class="row">
                <div class="col-md-3">
                    <div class="sidebar">
                        <ul class="list-unstyled side_nav" style="border: 1px solid #ccc;">
                            <li style="background: #d90406; color: white; text-transform: uppercase; font-weight: bold; padding: 0.5em; letter-spacing: 1px;">Quick Links</li>
                            <%--<li><a href="https://mpwarehousing.mp.gov.in/Upload/Image_040_230324_145136.pdf">Agreement draft 2023-24</a><span> <img src="Images/animated-new-image-0151.gif" /></span></li>--%>
                            <%-- <li><a href="https://mpwarehousing.mp.gov.in/Upload/Agreementdraft202324.pdf">Agreement draft 2023-24</a><span>--%> <%--<img src="Images/animated-new-image-0151.gif" />--%></span></li>
                            <%--<li><a href="https://mpwarehousing.mp.gov.in/Upload/JVSPolicyCorrection.pdf">JVS पॉलिसी में आंशिक संशोधन</a><span>--%> <%--<img src="Images/animated-new-image-0151.gif" />--%></span></li>
                            <%--<li><a href="https://mpwarehousing.mp.gov.in/Upload/moistureinwheat.pdf">Moisture content related to wheat</a><span>
                                <img src="Images/animated-new-image-0151.gif" /></span></li>--%>
                            <%--<li><a href="https://mpwarehousing.mp.gov.in/Warehouse/QCRegistration/RegistrationForm.aspx">स्कंध की गुणवत्ता जांच के लिए प्रशिक्षण हेतु रजिस्ट्रेशन</a><span>
                                <img src="Images/animated-new-image-0151.gif" /></span></li>--%>
                            <%-- <li><a href="https://mpwarehousing.mp.gov.in/Upload/Advertisement2425.pdf">Advertisement(EOI) For JVS Offer 2024-25</a><span> <%--<img src="Images/animated-new-image-0151.gif" /></span></li>--%>
                            <li><a href="https://mpwarehousing.mp.gov.in/Upload/New_Ad_2026_27.pdf">Advertisement(EOI) For JVS Offer 2026-27</a><span><img src="Images/animated-new-image-0151.gif" /></span></li>
                            <%-- <li><a href="https://mpwarehousing.mp.gov.in/Upload/GodownjvsPolicy23-24.pdf">JVS Scheme Policy For 2023-24</a><span> <img src="Images/animated-new-image-0151.gif" /></span></li>--%>
                            <%--<li><a href="https://mpwarehousing.mp.gov.in/Upload/JVS_Policy_2025_26_Pdf_0002.pdf">JVS Scheme Policy For 2025-26</a><span> <img src="Images/animated-new-image-0151.gif" /></span></li>--%>
                            <li><a href="https://mpwarehousing.mp.gov.in/Upload/JVS_Policy_2026_27.pdf">JVS Scheme Policy For 2026-27</a><span>
                                <img src="Images/animated-new-image-0151.gif" /></span></li>
                            <li><a href="https://mpwarehousing.mp.gov.in/Upload/RentdeductionduetooperationaldeficienciesinMPWLCwarehouses.pdf">Rent deduction due to operational deficiencies in MPWLC warehouses.</a><span>
                                <img src="Images/animated-new-image-0151.gif" /></span></li>
                            <%--<li><a href="https://mpwarehousing.mp.gov.in/Upload/GodownjvsPolicy24-25.pdf">JVS Scheme Policy For 2025-26</a><span>--%> <%--<img src="Images/animated-new-image-0151.gif" />--%><%--</span></li>--%>
                            <%--<li><a href="https://mpwarehousing.mp.gov.in/PDF/JVS_Policy_24-25_agreement.pdf">RMS Agreement 2024-25</a><span>--%> <%--<img src="Images/animated-new-image-0151.gif" />--%><%--</span></li>--%>
                            <%-- <li><a href="https://mpwarehousing.mp.gov.in/PDF/JVS_AGREEMENT_2025_26_PDF.pdf">RMS Agreement 2025-26</a><span> <img src="Images/animated-new-image-0151.gif" /></span></li>--%>
                            <li><a href="https://mpwarehousing.mp.gov.in/PDF/JVS_Agreement_2026_27.pdf">RMS Agreement 2026-27</a><span>
                                <img src="Images/animated-new-image-0151.gif" /></span></li>


                            <%--<li><a href="Advertisement.aspx">Advertiesment</a><span> <img src="Images/animated-new-image-0151.gif" /></span></li>--%>
                            <li><a href="ViewMettingLinks.aspx">Upcoming Meeting Links</a><span>
                                <img src="Images/animated-new-image-0151.gif" /></span></li>
                            <%-- <li><a href="https://mpwarehousing.mp.gov.in/Warehouse/StatePages/Rpt_District_wise_Stock_Position.aspx" target="_blank">District Wise Stock Position</a><span> <img src="Images/animated-new-image-0151.gif" /></span></li>--%>
                            <%--<li><a href="Upload/Tripartite Agreement.pdf" target="_blank">Tripartite Agreement.</a><span> <img src="Images/animated-new-image-0151.gif" /></span></li>--%>
                            <li><a href="PDF/Insuranceclaimrecovery2022_17032022.pdf">Insurance claim recovery 2022 . Dated 17.03.2022</a></li>
                            <%--<li><a href="PDF/JVS202223_2022_23.pdf">Joint Venture Scheme for 2022-23</a></li>
                            <li><a href="PDF/JVS_Agreement_2022_23.pdf">Joint Venture Agreement for 2022-23</a></li>--%>
                            <%--<li><a href="PDF/JVS202122.pdf">Joint Venture Scheme for 2021-22</a></li>--%>
                            <%--<li><a href="http://mpwlp2012.com" target="_blank">Online Applications under MPWLP-2012</a></li>--%>
                            <li><a href="Tender_New.aspx">Tenders</a></li>
                            <%-- <li><a href="Agreement.aspx">Agreement</a></li>--%>
                            <li><a href="Circular_New.aspx">Orders, Letters & Circulars</a></li>
                            <%-- <li><a href="Appointment.aspx">Appointment Orders</a></li>
                            <li><a href="Recruitment.aspx">Recruitment</a></li>--%>
                            <li><a href="BusinessReport.aspx">Business Reports</a></li>
                            <%--<li><a href="RTIAct.aspx">The RTI Act 2005</a></li>--%>
                            <li><a href="useful_file/1393827543mpwcl_ad.pdf">FAQ's</a></li>
                            <li><a href="https://mpwarehousing.mp.gov.in/Warehouse/login.aspx">JVS Application</a></li>
                            <li><a href="https://mpwarehousing.mp.gov.in/Warehouse/inspections/">Online Inspection</a></li>
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
                            <%--<li><a data-toggle="tab" href="#download">DOWNLOAD</a></li>--%>
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
                                        <%-- <li>
                                            <a href='/Upload/Kharif 22-23_11112022.pdf' target="_blank" class="heading"><i class="fa fa-hand-o-right red"></i>खरीफ उपार्जन 2022-23 हेतु नवीन सयुक्त भागीदारी योजना की कंडिका 12 में किये गए संसोधन के संबध में शासनादेश|  <span> <img src="Images/animated-new-image-0151.gif" /></span></a>
                                        </li>
                                        <li>
                                            <a href='/Upload/NEW_JVS_KHARIF_POLICY_2022_23.pdf' target="_blank" class="heading"><i class="fa fa-hand-o-right red"></i>खरीफ सीजन 2022-23 हेतु नवीन JVS पालिसी  <span> <img src="Images/animated-new-image-0151.gif" /></span></a>
                                        </li>
                                         <li>
                                            <a href='/Upload/NEW JVS POLICY OFFER NOTICE.pdf' target="_blank" class="heading"><i class="fa fa-hand-o-right red"></i>खरीफ सीजन 2022-23 हेतु नवीन JVS गोदाम हेतु सूचना  <span> <img src="Images/animated-new-image-0151.gif" /></span></a>
                                        </li>--%>

                                        <li>
                                            <a href='/PDF/chal_achal_sampatti_2025_26.pdf' target="_blank" class="heading"><i class="fa fa-hand-o-right red"></i>View Achal Sampatti Vivran Year 2025-26 </a><span><%--<img src="Images/animated-new-image-0151.gif" />--%></span>
                                        </li>
                                        <li>
                                            <a href='/Upload/CHALACHALSAMPATTI2024.pdf' target="_blank" class="heading"><i class="fa fa-hand-o-right red"></i>CHAL ACHAL SAMPATTI 2024  </a><span>
                                                <%--<img src="Images/animated-new-image-0151.gif" />--%></span>
                                        </li>
                                        <li>
                                            <%-- <a href='/Upload/GodownjvsPolicy24-25.pdf' target="_blank" class="heading"><i class="fa fa-hand-o-right red"></i>RMS 2024_25 JVS Policy  </a><span>--%>
                                            <a href='/Upload/JVS_Policy_2026_27.pdf' target="_blank" class="heading"><i class="fa fa-hand-o-right red"></i>RMS 2026-27 JVS Policy  </a><span>
                                                <%--<img src="Images/animated-new-image-0151.gif" />--%></span>
                                        </li>
                                        <li>
                                            <%--<a href='/PDF/JVS_Policy_24-25_agreement.pdf' target="_blank" class="heading"><i class="fa fa-hand-o-right red"></i>RMS 2024_25 JVS Agreement  </a><span>--%>
                                            <a href='/PDF/JVS_Agreement_2026_27.pdf' target="_blank" class="heading"><i class="fa fa-hand-o-right red"></i>RMS 2026-27 JVS Agreement  </a><span>
                                                <%--<img src="Images/animated-new-image-0151.gif" />--%></span>
                                        </li>
                                        <%--<li>
                                            <a href='/PDF/KMS2023_24_JVSPolicy.pdf' target="_blank" class="heading"><i class="fa fa-hand-o-right red"></i>KMS 2023_24 JVS Policy  </a><span>
                                                <img src="Images/animated-new-image-0151.gif" /></span>
                                        </li>
                                        <li>
                                            <a href='/PDF/KMS2023_24_JVSAgreement.pdf' target="_blank" class="heading"><i class="fa fa-hand-o-right red"></i>KMS 2023_24 JVS Agreement  </a><span>
                                                <img src="Images/animated-new-image-0151.gif" /></span>
                                        </li>--%>

                                        <li>
                                            <a href='/PDF/Tenderno2940Dated23082023.pdf' target="_blank" class="heading"><i class="fa fa-hand-o-right red"></i>Short notice inviting Tender MPWLC/Commr/Insur/2023/2940 Date 23-08-2023  </a><span>
                                                <%--<img src="Images/animated-new-image-0151.gif" />--%></span>
                                        </li>
                                        <li>
                                            <a href='/PDF/finalAuditProfarmaforBranchesandRegionaloffices.pdf' target="_blank" class="heading"><i class="fa fa-hand-o-right red"></i>Audit Profarma for Branches and Regional offices  </a>
                                        </li>
                                        <%-- ----------%>

                                        <li>
                                            <a href='/PDF/chalachalsampatti2023.pdf' target="_blank" class="heading"><i class="fa fa-hand-o-right red"></i>View Achal Sampatti Vivran Year 2023-24 </a><span><%--<img src="Images/animated-new-image-0151.gif" />--%></span>
                                        </li>
                                        <li>
                                            <a href='/AchalSampatti.aspx' target="_blank" class="heading"><i class="fa fa-hand-o-right red"></i>View Achal Sampatti Vivran Year 2022-23 </a>
                                        </li>


                                        <%-- =====================--%>


                                        <li>
                                            <a href='/PDF/ACHAL_SAMPATTI201819.pdf' target="_blank" class="heading"><i class="fa fa-hand-o-right red"></i>Achal Sampatti Vivran Year 2018-19 </a>
                                        </li>

                                        <li>
                                            <a href='/PDF/AcahalSampatti2018.pdf' target="_blank" class="heading"><i class="fa fa-hand-o-right red"></i>Achal Sampatti Vivran Year 2017-18  </a>
                                        </li>

                                        <%--<li>
                                            <a href='http://www.mptreasury.org/mpt/dynamic/cybertreasuryhome.htm' target="_blank" class="heading"><i class="fa fa-hand-o-right red"></i>Link of Cyber Treasury </a>
                                        </li>--%>
                                        <%-- <li>
                                            <a href='http://mpwarehousing.com/letter/1500016708GST.pdf' target="_blank" class="heading"><i class="fa fa-hand-o-right red"></i>GST Lagu Karne Ke sambandh Me  </a>
                                        </li>--%>
                                        <li>
                                            <a href='/PDF/AcahalSampatti2017.pdf' target="_blank" class="heading"><i class="fa fa-hand-o-right red"></i>Achal Sampatti 2016 - 17 </a>
                                        </li>
                                        <li>
                                            <a href='/PDF/Achal_Sampatti_Vivran_201516.pdf' target="_blank" class="heading"><i class="fa fa-hand-o-right red"></i>Achal Sampatti Vivran 2015-2016  </a>
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
                        <%-- <ul class="list-unstyled side_nav" style="border: 1px solid #ccc;">
                            <li style="background: #d90406; color: white; text-transform: uppercase; font-weight: bold; padding: 0.5em; letter-spacing: 1px;">FIFO Related Important Updates</li>
                            <li>
                                <a href='/Upload/Letter 1628 Date 23-09-2022_220923_170348.pdf' target="_blank" class="heading">स्‍कंध निकासी संबंधित FIFO व्‍यवस्‍था <span>
                                    <img src="Images/animated-new-image-0151.gif" /></span></a>
                            </li>
                            <li><a href="https://mpwarehousing.mp.gov.in/Warehouse/fifodashboard/default.aspx" target="_blank">FIFO Dashboard</a><span>
                                <img src="Images/animated-new-image-0151.gif" /></span></li>
                            <li><a href="https://mpwarehousing.mp.gov.in/Warehouse/StatePages/Rpt_Branch_wise_FIFO.aspx" target="_blank">Godown Wise FIFO Status For FCI</a><span>
                                <img src="Images/animated-new-image-0151.gif" /></span></li>
                            <li><a href="https://mpwarehousing.mp.gov.in/Warehouse/StatePages/Rpt_Branch_wise_FIFO_For_PDS.aspx" target="_blank">Godown Wise FIFO Status For PDS</a><span>
                                <img src="Images/animated-new-image-0151.gif" /></span></li>

                            <li><a href="https://mpwarehousing.mp.gov.in/Warehouse/StatePages/Rpt_Branch_wise_FIFO_For_Rabi_2023_24.aspx" target="_blank">Godown Wise FIFO For Rabi 2023-24</a><span>
                                <img src="Images/animated-new-image-0151.gif" /></span></li>
                        </ul>--%>
                        <%--New Div Created For FIFO BEGIN--%>
                        <ul class="list-unstyled side_nav" style="border: 1px solid #ccc;">
                            <li style="background: #d90406; color: white; text-transform: uppercase; font-weight: bold; padding: 0.5em; letter-spacing: 1px;">Current Update</li>
                            <%-- <li>Hon. Shri Bisahulal Singh Ji, Minister, Department of Food,Civil Supplies and Consumer Protection </li>
                            <li>Hon. Shri Rahul Singh Ji, Chairman, Madhaya Pradesh Warehousing & Logistics Corporation</li>--%>
                            <li>
                                <strong style="text-align: center;">Smt.Rashmi Arun</strong>&nbsp;<strong>Shami (IAS)</strong>,<br />
                                Additional Chief Secretary, Department of Food,Civil Supplies and Consumer Protection

                            </li>
                            <li>
                                <strong>Shri Anurag Verma(IAS)</strong>,<br />
                                Managing Director, Madhaya Pradesh Warehousing & Logistics Corporation</li>
                            <%--<li><a href="http://mpwarehousing.com/news_image/1406024188Award%20Photo.pdf">Innovation in Polymers in Agriculture and Water Conservation in Silo Bags used for storage of Foodgrain.</a></li>--%>
                            <%-- <li><a href="http://mpwarehousing.com/news_image/1395127645EXCELLENCE%20AWARD.pdf">Excellence Award</a></li>
                            <li><a href="http://mpwarehousing.com/news_image/1395127605SiloBags.pdf">Silo Bags used for storage of Foodgrain</a></li>
                            <li><a href="http://mpwarehousing.com/news_image/1393673295MANTHAN%20AWARD.pdf">Manthan Award in IT Sector</a></li>
                            <li><a href="http://mpwarehousing.com/news_image/1393673191e-India%20PSE%20Awards.pdf">e-India PSE Awards.</a></li>--%>
                        </ul>
                        <%--END--%>
                    </div>
                </div>

            </div>

        </div>
        <!-- /container -->
    </section>

    </span>

</asp:Content>

<asp:Content ContentPlaceHolderID="widget" runat="server">
    <section class="widget_wrapper">
        <div class="container">
            <div class="row-fluid">
                <div class="col-md-4"></div>
                <div class="col-md-4">
                    <img src="assets/img/info_red.png" class="icon">
                    <h4 class="title">MPWLC Info</h4>

                    <p class="text-justify" style="font-size: 15px;">
                        MP Warehouse and Logistics Corporation (MPWLC) is one of the oldest State Warehousing Corporation in the country .It was started with <b>8</b> regional offices and has now grown up to the extent of <b><span>
                            <asp:Label ID="lblbranchcount" runat="server"></asp:Label>
                        </span></b>Branches as at present with total Godown <b><span>
                            <asp:Label ID="lblTotalGodown" runat="server"></asp:Label>
                        </span></b>& its total capacity of <b><span>
                            <asp:Label ID="lblTotalcapacity" runat="server"></asp:Label>
                        </span>LMT</b> <%--and total utilized capacity <b><span><asp:Label ID="lblutiliaedCapacity" runat="server"></asp:Label> </span></b>--%>.
                    </p>
                    <p class="text-justify" style="font-size: 15px;">
                        MPWLC has own total Godown <b><span>
                            <asp:Label ID="lblTotalownedGodown" runat="server"></asp:Label>
                        </span></b>and its total capacity of <b><span>
                            <asp:Label ID="lblOwnedGodownCapcity" runat="server"></asp:Label>
                        </span>LMT</b> <%--and total utilized capacity <b><span><asp:Label ID="lblOwnedGodownUtilized" runat="server"></asp:Label> </span></b>--%> private Godown <b><span>
                            <asp:Label ID="lblPrivateGodown" runat="server"></asp:Label>
                        </span></b>and its total Capacity of  <b><span>
                            <asp:Label ID="lblPrivateGodownCapacity" runat="server"></asp:Label>
                        </span>LMT </b><%--and total utilized capacity <b><span><asp:Label ID="lblPrivateUtilizedcapacity" runat="server"></asp:Label> </span></b>--%>.

                    </p>

                </div>
                <div class="col-md-4"></div>
                <%-- <div class="col-md-4">
                    <img src="assets/img/services_red.png" class="icon">

                    <h4 class="title">Sevices we offer</h4>
                    <ul>
                        <li><a href="http://mpwarehousing.com/services.php">Scientific storage facility</a></li>
                        <li><a href="http://mpwarehousing.com/services.php">D.E.S.S.</a></li>
                         <li><a href="#">Scientific storage facility</a></li>
                        <li><a href="#">D.E.S.S.</a></li>
                    </ul>
                </div>--%>
                <%--<div class="col-md-4">
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
                </div>--%>
            </div>
        </div>

    </section>
</asp:Content>
