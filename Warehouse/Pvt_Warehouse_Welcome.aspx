<%@ Page Language="C#" MasterPageFile="~/MasterPage/PrivateWarehouse.master" AutoEventWireup="true" CodeFile="Pvt_Warehouse_Welcome.aspx.cs" Inherits="Pvt_Warehouse_Welcome" Title="Pvt Warehouse Welcome" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">
       function validateData() {
           //clear textbox
           return true;
       }
    </script>
    
  
 <script type="text/javascript" src="http://maps.googleapis.com/maps/api/js?sensor=false"></script>
<script type="text/javascript">
    function LLFunction() {
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(function(p) {
                var LatLng = new google.maps.LatLng(p.coords.latitude, p.coords.longitude);
                var Latitude = p.coords.latitude;
                var longitude = p.coords.longitude;
                document.cookie = "Lati=" + Latitude;
                document.cookie = "Longi=" + longitude;
            });
        } else {
            alert('Geo Location feature is not supported in this browser.');
        }
    }
    window.onload = LLFunction();
</script>

 <style type="text/css">
#popupwin {
position:fixed;
top: 0;
left: 0;
width: 90%;
height: 90%;
background-color: #000;
filter:alpha(opacity=65);
-moz-opacity:0.7;
display: none;
opacity: 0.7;
z-index: 100;

}
.pop a{
text-decoration: none;
}
.popup{
width: 100%;
margin: 0 auto;
position: fixed;
z-index: 101;
}
.pop{
min-width: 600px;
width: 600px;
min-height: 150px;
margin: 100px auto;
background: #f3f3f3;
position: relative;
z-index: 103;
padding: 10px;
border-radius: 5px;
box-shadow: 0 2px 5px #000;
}
.pop p{
color: #555555;
text-align: justify;
font-size:medium;
}
.pop p a{
color: #d91900;
}
.pop .x{
float: right;
height: 35px;
left: 22px;
position: relative;
top: -20px;
width: 35px;
}

</style>

    <table cellpadding="0" cellspacing="0" class="auto-style1">
        <tr>
            <td align="center"> <asp:Label ID="UxName" runat="server" Text=""></asp:Label>
        &nbsp;to State Foodgrains Management System</td>
        </tr>
        <tr>
            <td align="center">Start time-<asp:Label ID="lbl_start" runat="server" Text="Label"></asp:Label><br />
                End time-<asp:Label ID="lbl_end" runat="server" Text="Label"></asp:Label></td>
        </tr>
    </table>
    
    <table>
    <tr>
    <td align="center">
    <h3 style="color: #FF0000">
    Important instructions
    </h3>
    </td>
    </tr>
    
    <tr>

    <td>
   <hr />
     <span style="font-size: small">
    1.WHR प्रिंट करने के लिए Print(WHR)Receipt का उपयोग करें। WHR प्रिंट करने पर पहला प्रिंट orignal कॉपी रहेगी दूसरी बार प्रिंट करने पर office कॉपी ओर उसके बाद प्रिंट नहीं लिया जा सकता। <br />
    2.MPSCSC के case मे depositor form csms module मे ही बनेगा। ओर mpscsc के अलावा depositor form बनाने के लिए print depositor form का उपयोग करें।<br />
    3.csms module मे द्वार प्रदाय योजना ओर ट्रांसपोर्ट ऑर्डर(TO) से बनाए गए डीओ का भुगतान करने के लिए delivery gatepass page मे issue To के अंतर्गत Door Step Delivery का चयन करें।<br />
    4.Private depositor का जमा व भुगतान करने के लिए receipt details ओर delivery gatepass page का ही उसे करना है जिसमे depositor type institution के अतिरिक्त दूसरा select करना है<br />
    5.किसी भी प्रकार की डिलीट request के लिए Delete Request page का उपयोग करते हुये जो प्रिंट निकलेगा उसे ब्रांच मैनेजर के sign द्वारा scan कॉपी <b> <a href="mailto:helpdesk@mpwlc.co.in">helpdesk@mpwlc.co.in</a> , ho@mpwlc.co.in </b> पर मेल करें। <br />
    6.किसी भी प्रकार की समस्या के लिए अपने तहसील व ब्रांच के नाम के साथ समस्या <a href="mailto:helpdesk@mpwlc.co.in">helpdesk@mpwlc.co.in</a> , <b>ho@mpwlc.co.in</b> पर मेल करें।
    <br />
    
    
    </span>
    <hr />
        <asp:LinkButton ID="LinkButton1" runat="server" Font-Size="Medium" Visible="false" 
             ForeColor="#6600FF">Wheat Procurement 2015-16 Details</asp:LinkButton>

    </td>
    </tr>
    <tr>
    <td>
    <img alt="New" src="images/new6.gif" id="new" runat="server" />
    </td>
    </tr>
    <tr>
    <td>
    <hr />

     <p style="color: #FF0000; text-decoration: blink;"  >
    
    1 WHR प्रिंट लेने के साथ साथ page के दूसरी ओर भंडारण की शर्तें का भी प्रिंट लेना है जो 
         <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/WarehouseLevel/WHRInstructions_PvtW.aspx">Print whr instruction</asp:HyperLink> बाले page से मिल जाएगा।
    </p>
    

    <span style="font-size: small">
        2. Door step Delivery/TO के Gatepass बनाने के लिए अब डेट wise का option भी 
        उपलब्ध है जिससे उस डेट मे किसी गोडाउन के लिए बने सभी DO/TO की perticular 
        commodity का एक साथ gatepass बनाया जा सकता है&nbsp; । 
        <br />
    </span>
                 <img src="images/pdflogo.png" style="width: 21px" alt="" height="21px" />
                                                        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/UserManual/Warehouse_UMHindi.pdf"
                                                            Font-Size="10pt" Font-Bold="true" ForeColor="Navy">UserManual Hindi</asp:HyperLink>
                                                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                        <img src="images/pdflogo.png" style="width: 21px" alt="" height="21px" />
                                                        <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/UserManual/User_Mannual.doc"
                                                            Font-Size="10pt" Font-Bold="true" ForeColor="Navy">UserManual English</asp:HyperLink>
                                                       
    </td>
    </tr>
        
    </table>
    <div>
       <%-- <asp:BarChart ID="BarChart1" runat="server" ChartWidth="900" ChartType="Column">
        </asp:BarChart>--%>
    </div>

     <asp:Panel ID="pnllogin" class="popup" runat="server">
     <div class="popup">
<div class="pop">
<img src="~/images/close-icon.png" alt="quit" runat="server" class="x" id="x" />
<p>
 
<br/>
</p>
</div>
</div>
</asp:Panel>
    <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" CancelControlID="x" TargetControlID="new"  BackgroundCssClass="popup" PopupControlID="pnllogin">
    
    </asp:ModalPopupExtender>
    
    <asp:AnimationExtender ID="popupAnimation" runat="server" TargetControlID="new">
        <Animations>
                <OnClick>
         <Parallel AnimationTarget="pnllogin" Duration="0.4" Fps="10">
            <FadeIn />
          </Parallel>
       </OnClick>
        </Animations>
    </asp:AnimationExtender>
</asp:Content>

