<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TribalGodownApp.aspx.cs" Inherits="TribalGodown_TribalGodownApp" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Tribal Godown App</title>
   
    <meta name="viewport" content="width=device-width" />
 <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
     <link rel="stylesheet" href="css/main.css" type="text/css" />
    <script type="text/javascript" src="js/jquery-1.4.1.min.js"></script>
	<script type="text/javascript" src="js/menu.js"></script>
	<script type="text/javascript" src="js/slideshow.js"></script>
	<script type="text/javascript" src="js/cufon-yui.js"></script>
	<script type="text/javascript" src="js/Arial.font.js"></script>
	<script type="text/javascript">
	    Cufon.replace('h1,h2,h3,h4,h5,#menu,#copy,.blog-date');
	</script>
  
    <style type="text/css">

ul
{
	margin-left:0px;
}
li
{
	list-style:none;
	height:30px;
	margin-left:0px;
}
li a
{
	color:#003366;
	font-size:15px;
	font-family:@Arial Unicode MS;
	font-weight:bold;
	margin-left:0px;
	text-decoration:underline;
	
}

</style>
</head>
<body>
    <div id="bg">
		<div class="wrap">
			<img src="../images/CH.jpg" style="width: 100%" alt="" height="150" />
          
            <br />
            
             <center>
            <div  style="width: 100%;">
                                           
                                   <br /> 
                                   <form id="form1" runat="server">
                    <table align="center" style="width: 85%; border: 7px solid #cb4e48; background-color: #EBEBEB;">       
                          <tr>
                          <td colspan="6">
                          <center><h2>Application Form For Applicants of Tribal Areas</h2></center>
                          </td>
                          </tr>

                         
                    </table>
                    <table align="center" style="width: 85%; border: 7px solid #cb4e48; background-color: #EBEBEB; border-top:0px red solid;">   
                    <tr>
                    <td colspan="3">
                    
                    
                 <marquee direction="left" scrollamount="6" loop="true" width="100%" bgcolor="#ffffff" onmouseover="this.stop();" onmouseout="this.start();">

    <asp:Label id="lblMarquee" runat="server" Font-Size="Medium" ForeColor="ForestGreen" Font-Bold="True" Text="Registration for Online Application for Warehouse Construction under Tribal Area begins on 05/04/2021  and ends on 26/04/2021" ></asp:Label>

<%--    <asp:Label id="lblMarquee" runat="server" Font-Size="Medium" ForeColor="ForestGreen" Font-Bold="True" Text="Registration for Online Application for Warehouse Construction under Tribal Area has been closed" ></asp:Label>
--%>
</marquee>
                    
                    </td>
                    </tr>    
                          <tr>
                          <td style=" background-color:none;" align="left">
                          <div runat="server" visible="true" style="width: 300px">
                                                  
                                                  <ul>
                                                  <li>
                                                   <a id="at" href="http://mpwarehousing.com/tender/1617174540.pdf">Tender Notification & Instructions</a>
                                                 
                                                  </li>
                                                <%--  <li>
                                                   <a id="a1" href="Corrigendum.pdf">Corrigendum</a>
                                                 
                                                  </li>--%>
                                                   <li>
                                                   <a id="a2" href="UserManual.pdf">User Manual</a>
                                                 
                                                  </li>
                                                   <li>
                                                   <a id="a3" href="SampleAppForm.pdf">Specimen Application Form</a>
                                                 
                                                  </li>
                                                    
                                                <%--   <li>
                                                   <a id="a4" href="Main_home.aspx">User Guide</a>
                                                 
                                                  </li>--%>
                                                  <li>
                                                   <a id="a5" href="UserReg.aspx">Registration Form(Login)</a>
                                                 
                                                  </li>
                                                   <li>
                                                   <a id="a6" href="TribalGodownstructure.pdf">Specification for 1800 MT Godown</a>
                                                 
                                                  </li>
                                                 
                                                  <li>
                                                   <a id="a4" href="StateRegionDistrictLogin.aspx">Admin Login(Officialy Use)</a>
                                                 
                                                  </li>
                                                  </ul>
                                                 
                                                </div>
                          </td>
                          <td ><img src="../images/Linevertical.png" style="height:350px;" alt="" /></td>
                          <td style="height:20%;">
                          <p style="color: #008080; font-size: medium">आवश्यक दिशानिर्देश :-</p>
                                <p style="color:black">
                                    1.यदि आपने Registration कर लिया है तो लॉगिन का प्रयोग करें ।<br />
                                    2.(<span style="color:red">*</span>) वाले कॉलम अनिवार्य हैं।<br />
                                    3.यह आवेदन अनुसूचित जनजाति(ST) वर्ग के बेरोजगार आवेदको के लिए है।<br />
                                    4.आवेदक की न्यूनतम शैक्षणिक योग्यता 12 वी कक्षा उत्तीर्ण होना अनिवार्य है|<br />
                                    5.आवेदक के पास एक वैध मेल ID होना चाहिए|<br />
                                    6.आवेदक के पास एक वैध मोबाइल नंबर होना चाहिए।<br />
                                    7.ऑनलाइन आवेदन करने के संबन्ध मे आवश्यक निर्देश निगम की वैबसाइट पर <br /> &nbsp;&nbsp;&nbsp; देखे जा सकते है |<br />
                                    <%--8.गोदाम निर्माण संबंधी नीति/नियम के लिए <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/TribalGodown/Neeti.doc" Target="_blank" ForeColor="#003399">यहाँ क्लिक करें</asp:HyperLink><br />--%>
                                    8.1800 MT गोदाम निर्माण संबंधी Specifications के लिए <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/TribalGodown/TribalGodownstructure.pdf" Target="_blank" ForeColor="#003399">यहाँ क्लिक करें</asp:HyperLink> <br />
                                    9.Online आवेदन दिनांक <span style="color:#003399"> 05/04/2021 प्रात: 11.30 बजे से दिनांक 26/04/2021 <br />&nbsp;&nbsp;&nbsp;सायंकाल 5.00 बजे </span> के मध्य प्रस्तुत किए जा सकते है ।<br />
                                    10.उक्त सॉफ्टवेर से संबन्धित किसी भी तरह की समस्या हेतु निगम मुख्यालय मे निम्नलिखित दूरभाष नंबरो 
                                   <span style="color:#003399"> 0755-2600505</span> तथा<span style="color:#003399"> 0755-2600518</span> पर (समय  दोपहर 03 बजे से सायं 05 बजे तक) संपर्क कर सकते हैं 
                                    अथवा ई-मेल <a href="helpdeskmpwlc@gmail.com" style="color: #003399">helpdeskmpwlc@gmail.com</a> पर भी मेल कर सकते हैं ।


                                </p>
                          </td>
                          </tr>
                    </table>
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
                                            <b>© 2015 &nbsp;National Informatics Centre.All Rights Reserved
                                                <br />
                                                Developed By : National Informatics Centre
                                                <br />
                                                Madhya Pradesh, Ministry of Communications and Information Technology</b>
                                        </td>
                                        <td style="width: 1%" align="center">
                                            <img id="Logo" src="/../Images/Linevertical.png" style="width: 20px; height: 50px" alt="" />
                                        </td>
                                        <td style="width: 20%" align="center">
                                        <table><tr>    
                                        <td>                                        <a href="http://india.gov.in/">
                                                <img src="../Images/natindialogo.png" width="100px" height="50px" alt="" />
                                            </a>
                                            </td>
                                            <td>
                                             <a href="http://www.digitalindia.gov.in/">
                                                <img src="../Images/di.png" width="100px" height="50px" alt="" />
                                            </a>
                                            </td>
                                            </tr></table>

                                        </td>
                                    </tr>
                                </table>
                            </div>

                 </center>
         </div>
  <br />
                 </div>
   
</body>
</html>
