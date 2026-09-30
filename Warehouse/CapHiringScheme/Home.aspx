<%@ Page Title="" Language="C#" MasterPageFile="~/CapHiringScheme/Main.master" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="_Home" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
<style>
    

</style>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="body" Runat="Server">
     <section class="banner_wrapper">
        <img src="assets/img/caps.png" style="width:100%; height:350px;" />
     </section>

     <%--<section class="news_wrapper">
         <div class="container">
        <div class="row">
            <div class="col-md-12 col-lg-12">
                <marquee direction="left" scrollamount="5" loop="true" onmouseover="this.stop();" onmouseout="this.start();" >
                     <a href="#" target="_blank">Online Offer Under JVS 2020-21 Start From 20/02/2020</a>
                </marquee>
            </div>
         </div>
        </div>
     </section>--%>


     <section class="alert-container">
		<div class="container">
			<div class="row">
				<div class="col-md-2 col-lg-2 alert_head">
					CURRENT UPDATE
				</div>

				<div class="col-md-10 col-lg-10 alert_news">
					<marquee direction="left" scrollamount="5" loop="true" onmouseover="this.stop();" onmouseout="this.start();" >
                        <a href="#" target="_blank">Online Offer Cap hiring Scheme</a>
                    </marquee>
				</div>
			</div>	
		</div>
	</section>
 

  <section class="container_wrapper">
        <div class="container">
        <div class="row">
            <div class="col-md-3">
                <ul class="list-group caps">
                  <%--<li class="list-group-item"><a href="#">Expression of Interest</a></li>--%>
                  <li class="list-group-item"><a href="CCF11062020.pdf">Cap Hiring Scheme for 2020-21</a></li>
                  <li class="list-group-item"><a href="#">User Manual</a></li>
                </ul>

                <%--<div class="panel panel-info">
                  <div class="panel-heading"><h5 class="panel-title">CURRENT UPDATES</h5></div>
                  <div class="panel-body">
                    <marquee direction="up" scrollamount="2" loop="true" onmouseover="this.stop();" onmouseout="this.start();" style="min-height:10em;">
                        <a href="#" target="_blank">Online Offer Under JVS 2020-21 Start From 20/02/2020</a>
                    </marquee>
                  
                  </div>
                </div>--%>

            </div>
            <div class="col-md-9">
                <div class="panel panel-info">
                  <div class="panel-heading"><h4 class="panel-title">आवश्यक दिशानिर्देश :-</h4></div>
                  <div class="panel-body">
                  <ol style="line-height:2em;font-size:14px;">
                    <li> CAP आफर हेतु पूर्व मे किये गये रजिस्ट्रेशन को पुनः अपडेट करना होगा जिसका कोई चार्ज देय नहीं होगा किन्तु नवीन रजिस्ट्रेशन अथवा क्षमता व्रद्धि होने पर उतनी क्षमता पर निर्धारित शुल्क देना होगा । </li>
                    <li>यदि आपने Registration कर लिया है तो लॉगिन का प्रयोग करें । </li>
                    <li>(*) वाले कॉलम अनिवार्य हैं। </li>
                    <li>आवेदक के पास एक वैध मेल ID,वैध मोबाइल नंबर होना चाहिए|</li>
                    <li>इस आवेदन के लिए अपनी पसंद का पासवर्ड बनाएँ | पासवर्ड की लंबाई कम से कम 8 से 15 अक्षर की होनी चाहिए | और उसमे कोई एक कैपिटल एवं लोवर लेटर, एक न्यूमेरिक नंबर ,एक स्पेशल केरेक्टर (@,%,$,#) होना चाहिए | </li>
                    <li>आवेदक का नाम, संस्था के प्रकार, मोबाइल नंबर एवं अन्य जानकारी एक बार सुरक्षित करने के उपरांत बदली नहीं जा सकती हैं, रैजिस्टर्ड मोबाइल नंबर पर ही जानकारी भेजी जाएगी । </li>
                    <li>आवेदक सावधानीपूर्वक जानकारी प्रविष्ट करे, प्रविष्ट की गई जानकारी मे किसी भी प्रकार की त्रुटि के लिए आवेदक स्वयं जिम्मेदार होंगा |</li>
                    <li>ऑनलाइन आवेदन करने के संबन्ध मे आवश्यक निर्देश निगम की वैबसाइट पर देखे जा सकते है | </li>
                    <li>उक्त सॉफ्टवेर से संबन्धित किसी भी तरह की समस्या हेतु निगम मुख्यालय मे निम्नलिखित दूरभाष नंबरो <a> 0755-2600505  </a> तथा <a> 0755-2600287  </a> पर (समय दोपहर 03 बजे से सायं 05 बजे तक) संपर्क कर सकते हैं अथवा ई-मेल <big><a href="mailto:mpwlchelpdesk@gmail.com"> <strong> helpdeskmpwlc@gmail.com </strong></a></big> पर भी मेल कर सकते हैं । </li>
                </ol>
             </div>
                </div>
            </div>
        </div>
        </div>
  
  </section>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="script" Runat="Server">
</asp:Content>

