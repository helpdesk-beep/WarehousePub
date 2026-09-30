<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Mobile_Default3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>MPWLC Storage Module</title>
     <meta charset="urf-8" />
    <meta name="viewport" content="width=device-width" />
 <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
<link href="assets/css/bootstrap.css" rel="stylesheet"/>
<link href="assets/css/bootstrap-responsive.css" rel="stylesheet"/>

</head>
<body>
    <form id="form1" runat="server">

        <div class="navbar navbar-fixed-top">
<div class="navbar-inner">
<a class="btn btn-navbar" data-toggle="collapse" data-target=".nav-collapse">
<span class="icon-bar"></span>
<span class="icon-bar"></span>
<span class="icon-bar"></span>
 <span class="icon-bar"></span>
</a>


<a class="brand" href="#"><img src="../images/MpwlcM.PNG" height="20" width="20" /> MPWLC Storage Module</a>
<div class="nav-collapse">
<ul class="nav">
<li class="active"><a href="Home.aspx">Home</a></li>
<li><a href="MReceivning.aspx">Recieving/रिसीविंग</a></li>
<li><a href="MDelevery.aspx">Delevery/डेलेवेरी</a></li>    
    <li><a href="GodwonCordinates.aspx">Godown Coordinates</a></li>
 <li><a href="MLogin.aspx">Logout</a></li>         

</ul>           
</div>


</div>
</div>
 <div >

    <ul>
        <li>
            1.MPWLC Storage Module के मोबाइल version मे आप केबल रिसीविंग ओर डेलेवेरी की एंट्री कर सकते हैं। ओर रिसीविंग का status चेक कर सकते हैं। 
        </li>
        <li>
            2. की गई रिसीविंग का डबल्यूएचआर मुख्य सॉफ्टवेर की सहायता से ही बनेगा ।
        </li>
         <li>
             <span style="color: #FF0000">
            3. एक गोदाम के coordinates लेने के बाद logout करके दूसरे गोदाम पर जा कर फिरसे लॉगिन करें ताकि current location डेटेक्ट हो सके।</span>
        </li>
        <li>
            4.किसी भी प्रकार की समस्या के लिए अपने District व Branch के नाम के साथ समस्या <a href="mailto:mpwlchelpdesk@gmail">mpwlchelpdesk@gmail</a> , ankitsoni1516@gmail.com , ho@mpwarehousing.com पर मेल करें।
        </li>
         <li>
             <span style="color: #FF0000">
            5.गोदाम के coordinates(आपकी current location)भरने के लिए <a href="GodownCordinates2.aspx">यहाँ क्लिक करें</a>
            </span>
        </li>
        <li>
            6.गोदाम के coordinates के user manual के लिए <a href="../UserManual/Godown Mapping menual.pdf" target="_blank">यहाँ क्लिक करें</a>
        </li>
         <li>
             <br />
            7.यदि किन्ही कारणों से Godown coordinates वाले page पर Map नहीं आ रहा हो तो आप <a href="https://play.google.com/store/apps/details?id=com.location.test&hl=en" target="_blank">इस URL</a>  पर जा कर app download कर सकते हैं 
        </li>

    </ul>


</div>

    </form>
 <script src="assets/js/jquery.js"></script>
<script src="assets/js/bootstrap-transition.js"></script>
<script src="assets/js/bootstrap-alert.js"></script>
<script src="assets/js/bootstrap-modal.js"></script>
<script src="assets/js/bootstrap-dropdown.js"></script>
<script src="assets/js/bootstrap-scrollspy.js"></script>
<script src="assets/js/bootstrap-tab.js"></script>
<script src="assets/js/bootstrap-tooltip.js"></script>
<script src="assets/js/bootstrap-popover.js"></script>
<script src="assets/js/bootstrap-button.js"></script>
<script src="assets/js/bootstrap-collapse.js"></script>
<script src="assets/js/bootstrap-carousel.js"></script>
<script src="assets/js/bootstrap-typeahead.js"></script>
</body>
</html>
