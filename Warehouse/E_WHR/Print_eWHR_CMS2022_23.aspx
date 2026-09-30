<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/StateMasterMfd.master" AutoEventWireup="true" CodeFile="Print_eWHR_CMS2022_23.aspx.cs" Inherits="E_WHR_Print_eWHR_CMS2022_23" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <style type="text/css">
       
    .popbag
    {
    background-color:gray;
    filter:alpha(opacity=90);
    opacity:0.8;
    z-index:10000;
    }
    .modalpop
    {
    background-color:#FFFFFF;
    border-width:3px;
    border-color:Black;
    padding-top:10px;
    padding-left:10px;
    width:250px;
    height:120px;
     border-radius: 25px;
    text-shadow:yellow;	
    
    
    }
    </style>
    <%--<script type = "text/javascript">
        function DisableButton() {
            document.getElementById("<%=btnprint.ClientID %>").disabled = true;
        }
        window.onbeforeunload = DisableButton;
</script>--%>

<style type="text/css">
.protected {
    -moz-user-select:none;
    -webkit-user-select:none;
    user-select:none;
}

</style>
<style>
@media print{
#ReportDiv {background-color:#999;
            background-image:url('../../images/whrback.png');
            -webkit-print-color-adjust:exact;
}
</style>

<style type="text/css">
    .fountcolor{
   
color: #FF5050;


} 
    
    .style1
    {
        height: 17px;
    }
    
    </style>
<script language="JavaScript" type="text/javascript">
    //Message to display whenever right click on website
    var message = "Sorry, Right Click have been disabled.";
    function click(e) {
        if (document.all) {
            if (event.button == 2 || event.button == 3) {
                alert(message);
                return false;
            }
        }
        else {
            if (e.button == 2 || e.button == 3) {
                e.preventDefault();
                e.stopPropagation();
                alert(message);
                return false;
            }
        }
    }
    if (document.all) {
        document.onmousedown = click;
    }
    else {
        document.onclick = click;
    }
</script>
 
<script type="text/javascript">
    function PrintDivw() {
        var contents = document.getElementById("ReportDiv").innerHTML;
        var frame1 = document.createElement('iframe');
        frame1.name = "frame1";
        frame1.style.position = "absolute";
        frame1.style.top = "-1000000px";
        document.body.appendChild(frame1);
        var frameDoc = (frame1.contentWindow) ? frame1.contentWindow : (frame1.contentDocument.document) ? frame1.contentDocument.document : frame1.contentDocument;
        frameDoc.document.open();
        frameDoc.document.write('<html><head><title>WHR</title>');
        frameDoc.document.write('</head><body>');
        frameDoc.document.write(contents);
        frameDoc.document.write('</body></html>');
        frameDoc.document.close();
        setTimeout(function () {
            window.frames["frame1"].focus();
            window.frames["frame1"].print();
            document.body.removeChild(frame1);
        }, 500);
        return false;
    }
</script>
<script type="text/javascript">
    function PrintDiv() {
        var divContents = document.getElementById("ReportDiv").innerHTML;
        var printWindow = window.open('', '', 'height=200,width=400');
        //   printWindow.document.write('<html><head><title>WHR</title>');
        printWindow.document.write('</head><body >');
        printWindow.document.write(divContents);
        printWindow.document.write('</body></html>');
        printWindow.document.close();
        printWindow.print();
        printWindow.close();
    }
</script>
 
 <table cellpadding="0" cellspacing="0" 
        style="width: 100%; color: #FF5050;" >
        <tr>
        <td><br /></td>
        </tr>
        <tr>
        <td><br /></td>
        </tr>
       <tr style="background-color: #0bb6e6; height: 25px">
                                                        <td colspan="4" align="center">
                                                            <span style="color: White; font-size: 12pt; font-weight: bold">Print e-WHR 2022-23</span>
                                                        </td>
                                                    </tr>
                                                      <tr>
        <td><br /></td>
        </tr>
        <tr>
                                                        <td style="width: 150px" align="left">
                                                            <asp:Label ID="lblDistrict" runat="server" Text="District" Font-Size="10pt" Font-Bold="true" ForeColor="Navy"></asp:Label>
                                                        </td>
                                                        <td style="width: 150px" align="left">
                                                            <asp:DropDownList ID="ddlDistrict" runat="server" Height="25px" Width="155px" AutoPostBack="True"
                                                                CssClass="tb6" onselectedindexchanged="ddlDistrict_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="width: 100px" align="left">
                                                            <asp:Label ID="lblBranch" runat="server" Text="Branch" Font-Size="10pt" Font-Bold="true" ForeColor="Navy"></asp:Label>
                                                        </td>
                                                        <td style="width: 200px" align="left">
                                                            <asp:DropDownList ID="ddlDepotList" runat="server" Height="25px" Width="155px" AutoPostBack="True"
                                                                 CssClass="tb6" onselectedindexchanged="ddlDepotList_SelectedIndexChanged" 
                                                                >
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
        <td><br /></td>
        </tr>
                <tr>
                  <td align="left">
                        <asp:Label ID="Label3" runat="server" Text="Commodity" ForeColor="Navy" Font-Bold="true"
                            Font-Size="10pt"></asp:Label>
                    
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlCommodity" runat="server" Height="25px" Width="250px"
                            AutoPostBack="true" 
                            onselectedindexchanged="ddlCommodity_SelectedIndexChanged">
                            <asp:ListItem Value="-1">--Select--</asp:ListItem>
                              <asp:ListItem Value="63">GRAM</asp:ListItem>
                              <asp:ListItem Value="64">LENTIL</asp:ListItem>
                              <asp:ListItem Value="33">Mustard-Sarason</asp:ListItem>
                            <asp:ListItem Value="92">Moong</asp:ListItem>
                            <asp:ListItem Value="27">Urad</asp:ListItem>
                            <%--  <asp:ListItem Value="52">Arahar</asp:ListItem>--%>
                        </asp:DropDownList>
                        &nbsp;
                    </td>
                    <td align="left">
                        <asp:Label ID="lblwhr" runat="server" Text="Select WHR - " ForeColor="Navy" Font-Bold="true"
                            Font-Size="10pt"></asp:Label>
                    
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="DDLwhr" runat="server" Height="25px" Width="250px" OnSelectedIndexChanged="DDLwhr_SelectedIndexChanged"
                            AutoPostBack="true">
                        </asp:DropDownList>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        &nbsp;</td>
                    <td align="left">
                        OR</td>
                </tr>
                <tr id="Tr1" visible="false" runat="server">
                    <td align="left" style="width: 150px">
                        <asp:Label ID="Label1" runat="server" Text="WHR No-" ForeColor="Navy" 
                            Font-Bold="True" Font-Size="10pt"></asp:Label>
                       </td>
                    <td align="left">
                        <asp:TextBox ID="TextBox1" runat="server" autocomplete="off"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                </tr>
                <tr>
        <td><br /></td>
        </tr>
                <tr>
                    <td align="left" style="width: 150px">
                        &nbsp;</td>
                    <td align="left">
&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnsubmit" runat="server" onclick="btnsubmit_Click" Enabled="false" 
                            Text="Submit" OnClientClick="this.disabled = true; this.value='Please wait'" UseSubmitBehavior="false"  />
&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnbmpassword" runat="server" Text="BM Password" Visible="true" />
                        &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnprint" runat="server" Text="Print" Enabled="true"
                            onclick="btnprint_Click" OnClientClick="PrintDiv();" />
                           
                        </td>
                </tr>
                </table>
                <table>
                <tr>
                <td>
                <%--<p visible="false" style="font-size: medium; color: #FF0000">NOTE: WHR का प्रिंट निकालने के लिए अब ब्रांच मैनेजर पासवर्ड लगेगा जो Default रूप से bm2015 है । ब्रांच मैनेजर ये सुनिश्चित कर लें की इसे 
                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="../../IssueCenterLevel/Change_Password.aspx">change Password</asp:HyperLink>  बाले page से change कर लें </p>--%>
                1.WHR का प्रिंट लेने के पहले ये सुनिश्चित कर लें की आपके प्रिंटर की setting ए4 size के paper के लिए सेट हो।<br />
                2.आप जिस भी ब्राउज़र का प्रयोग कर रहे हो उसमे background प्रिंट enable कर लें ताकि WHR का सही प्रिंट निकल सके.<br />
                background प्रिंट enable करने का तरीका इस प्रकार है।<br />
                a. Internet explorer-- go to print--PageSetup--Check the Print Background Colors and Images box<br />
                b. Firfox-- go to Print-- PageSetup--Check the Print Background (colors & images) box<br />
                c. Crome--Go to Print--Click the Background colors and images box     
                </td>
                </tr>
                </table>
                <br />
                <br />
    <asp:Panel ID="Panel1" runat="server" Width="950px" 
        BackImageUrl="~/images/tran.png" CssClass="protected">
    
<div id="ReportDiv">
    <table id="WHR" 
        style="border-style: solid; border-width: thin; color: #FF5050; " 
        width="900px" runat="server" >
<tr>
<td align="left" valign="top" style="color: #FF5050;" rowspan="2">
<table>
<tr>
<td valign="top">
फार्म क्रमांक-3 (Form-III)<br />
(देखिये नियम-14)<br /> (See Rule-14)
</td>
<td>
&nbsp;
    <asp:Image ID="Image2" runat="server" Height="65px" 
        ImageUrl="~/images/mpwlc.png" Width="71px" />
</td>
</tr>
</table>

</td>
<td style="font-size:medium; font-family: Arial, Helvetica, sans-serif; color: #0000FF;" 
        align="center">माल गोदाम रसीद<br /> WAREHOUSE RECEIPT </td>
<td style="border-style: solid; border-width: thin; border-color: inherit;">
    <asp:Label ID="lblcopytype" runat="server" Text="Label" Font-Size="Small" 
        ForeColor="#FF5050"></asp:Label>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</td>
</tr>
<tr>

<td style="font-size:small; color: #FF5050;" align="center">(मध्यप्रदेश कृषि माल 
    गोदाम अधिनियम-1947 के अधीन)<br /> (Under the Madhya Pradesh Agriculture 
    Warehouse Act,1947) </td>
<td style="color: #FF5050;">
    माल/गोदाम स्थल
    <br />
Warehouse at:<asp:Label ID="lblbrachname1" runat="server" Text="Label"></asp:Label></td>
</tr>
<tr>
<td>
    <asp:Label ID="lblinstType" runat="server" Text="Label"></asp:Label>
    <br /> 
    
    <asp:Label ID="instypeEng" runat="server" Text="Label"></asp:Label>
    </td>
<td style="font-size:small" align="center">
    <asp:Label ID="lblcorpnamehnd" runat="server" 
        Text="(म.प्र. वेयरहाउसिंग एंड लाजिस्टिक्स कार्पोरेशन)"></asp:Label>
    
    
    
    <br>
    <asp:Label ID="lblcorpname" runat="server" Text="(Madhya Pradesh Warehousing and Logistics Corporation)"></asp:Label>
    &nbsp;<br>
    
    
    
</td>
<td class="fountcolor">
    रुपये के लिए आबद्ध माल गोदाम<br /> Warehouse Bounded for Rs.</td>
</tr>
<tr>
<td colspan="2">
WHR No:<asp:Label ID="lblwhrno" runat="server" Text="Label" Font-Bold="True"></asp:Label></td>

<td>
    <p style="opacity:0.2;"> <asp:Label ID="lblorico" runat="server" Text="Original"></asp:Label> <asp:Label ID="lbldatetime" runat="server" Text="Label"></asp:Label></p>
</td>
</tr>
<tr>
<td colspan="2" class="fountcolor">
व्यक्ति,फर्म,कम्पनी/सरकारी संस्था का नाम:- 
    <asp:Label ID="lbldepositorname1" runat="server" Text="Label" Font-Bold="True"></asp:Label>
</td>
<td rowspan="3">
    <asp:Image ID="Image3" runat="server" />

<%--"https://chart.googleapis.com/chart?chs=100x100&amp;cht=qr&amp;chl=WHR No='' &amp;First(Fields!Depositor_WHR_Id.Value, &quot;WHR&quot;) &amp;&quot;Commodity=&quot;&amp;First(Fields!Commodity_Name.Value, &quot;WHR&quot;) &amp;&quot;WHR Date=&quot;&amp; First(Fields!Depositdate.Value, &quot;WHR&quot;) &amp;&quot;Total Bags=&quot;&amp; Sum(Fields!TotalBags_Received.Value, &quot;WHR&quot;) &amp;&quot;Total Weight=&quot;&amp; Sum(Fields!Total_Qty_Received.Value, &quot;WHR&quot;) &amp;&quot;&quot;"--%>
</td>
</tr>
<tr>
<td colspan="2" class="fountcolor">
    पता:-
    <asp:Label ID="lbladdr" runat="server" Text="Label" Font-Bold="True"></asp:Label>
    </td>

</tr>
<tr>
<td colspan="2" class="fountcolor" nowrap="nowrap">
    लाईसेन्स क्रमांक:-&nbsp;
    <asp:Label ID="lbllicensno" runat="server" Font-Bold="True" Text="Label"></asp:Label>
    &nbsp;&nbsp; लाइसेंस दिनांक को समाप्त होता है&nbsp;&nbsp;
    <asp:Label ID="lbllicensedate" runat="server" Font-Bold="True" Text="Label"></asp:Label>
    </td>
</tr>
<tr>
<td colspan="3" class="fountcolor" nowrap="nowrap">
    Depositor Form No.:-&nbsp;
    <asp:Label ID="lblDFNo" runat="server" Font-Bold="True" Text=""></asp:Label>
    &nbsp;&nbsp; <asp:Label ID="lblAccRej" runat="server" Text="Label" Font-Bold="True" Visible="false"></asp:Label>&nbsp;&nbsp;
    <asp:Label ID="lblAccNo" runat="server" Font-Bold="True" Text="" Visible="false"></asp:Label>
    
    <asp:Label ID="lblSname" runat="server" Font-Bold="True" Text="" Visible="false"></asp:Label>
    </td>
</tr>

<tr>
<td colspan="3" class="fountcolor">
   <p align="justify"> यह प्रमाणित किया जाता है कि हमें संग्रहण माल गोदाम : 
    <asp:Label ID="lblgodownname" runat="server" Text="Label" Font-Bold="True"></asp:Label>&nbsp;&nbsp;(OWN)
     <%-- एवं स्टेक :<asp:Label ID="lblStack" runat="server" Text="" Font-Bold="True"></asp:Label>&nbsp;&nbsp;&nbsp;--%>
    में श्री 
    <asp:Label ID="lbldepositor2" runat="server" Text="Label" Font-Bold="True"></asp:Label>
    &nbsp;से इसमें दिए गए निबंधनों तथा शर्तों के अध्यधीन नीचे वर्णित सम्पति इसमे दी गई स्थिति को छोड़ _______________को लेखे में उपरी रूप
    से अच्छी हालत में प्राप्त हुई । यह संपत्ति श्री_________________________________________________&nbsp;&nbsp; को या उसके द्वारा आदेशित 
    व्यक्ति या धारक को सभी संग्रहण उठाई-धराई और अन्य खर्चो का भुगतान करने तथा समुचित रूप से 
    पृष्ठांकित माल गोदाम रसीद प्रस्तुत करने पर सुपुर्द कर दी जायेगी !
    </p>
</td>
</tr>

<tr class="fountcolor">
<td colspan="3" class="fountcolor">
    <asp:ListView ID="ListView1" runat="server">
     <LayoutTemplate>
     <table  style="border-style: solid; border-width: thin; padding: inherit; margin: auto; border-collapse: collapse;">
    <tr runat="server" id="headerRow" style="background-color: #C0C0C0">
    <td width="110" align="center" valign="top" style="border-style: solid; border-width: thin; border-collapse: separate">
    उपज का विवरण
    </td>
    <td width="40" align="center" valign="top" style="border-style: solid; border-width: thin; border-collapse: separate">
    श्रेणी
    </td>
    <td width="150" align="center" valign="top" style="border-style: solid; border-width: thin; border-collapse: separate">
    बोरों की संख्या<br />
(अंको में व शब्दों में)
    </td>
    <td width="150" align="center" valign="top" style="border-style: solid; border-width: thin; border-collapse: separate">
    कुल वजन(In Qtls)<br />
(अंको में व शब्दों में)
    </td>
    <td width="100" align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
    दर प्रति (Qtls)
    (In Rs.)
    </td>
    <td width="170" align="center" valign="top" style="border-style: solid; border-width: thin; border-collapse: separate">
    कुल कीमत(In Rs.)<br />
(अंको में व शब्दों में)
    </td>
    <td width="90" align="center" valign="top" style="border-style: solid; border-width: thin; border-collapse: separate">
    रिमार्क
    </td>
    </tr>
    <tr style="border: thin solid #000000; background-color: #C0C0C0">
    <td align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
    (1)
    </td>
    <td align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
    (2)
    </td>
    <td align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
    (3)
    </td>
    <td align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
    (4)
    </td>
    <td align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
    (5)
    </td>
    <td align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
    (6)
    </td>
    <td align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
    (7)
    </td>
    </tr>
    <tr runat="server" id="itemPlaceholder"></tr>
    </table>

     </LayoutTemplate>
    <ItemTemplate>
    
    <tr class="fountcolor">
    <td align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
    <asp:Label ID="EmpnoLabel" runat="server" Text='<%# Eval("Commodity_Name") %>' />
    </td>
    <td align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
    <%# Eval("Category_Name")%>
    </td>
    <td align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
    <%# Eval("TotalBags_Received")%>
    </td>
    <td align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
    <%# Eval("Total_Qty_Received")%>
    </td>
    <td align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
    <%# Eval("MktValue_of_Commodityno")%>
    </td>
    <td align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
    <%# Eval("MktValue_of_Commodity")%>
    </td>
   <td align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
       <%# Eval("Remark")%>
    </td>
    </tr>
   
    </ItemTemplate>
    </asp:ListView>
</td>
</tr>
<tr class="fountcolor">
<td colspan="3">
    <br />
    संग्रहण (दर)
    <asp:Label ID="Label2" runat="server" Font-Bold="True" Text="As per tarrif"></asp:Label>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; प्रति (इकाई) __________________________प्रतिमाह कब से (दिनांक):-&nbsp;
    <asp:Label ID="lbldatefrom" runat="server" Text="Label" Font-Bold="True"></asp:Label>
   &nbsp; फसल वर्ष &nbsp;
    <asp:Label ID="lblCropYear" runat="server" Text="फसल वर्ष" Font-Bold="True"></asp:Label>
    </td>
</tr>
<tr>
<td colspan="3">
    <br />
उठाई-धराइ (खर्च)______________________प्रति (इकाई) __________________________(खर्च) अंदर ले जाने तथा बाहर निकालने का सम्मिलित कर !
</td>
</tr>
<tr>
<td colspan="3">
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        अग्रिम दे दिए गए है और इस माल पर हुआ दायित्व इस प्रकार है -
</td>
</tr>
<tr>
<td colspan="3">
    गाड़ी-भाड़ा (काटेज)___________________________वस्तु 
    भाड़ा(फ्रेट)_______________________वजन_____________________________</td>
</tr>
<tr>
<td colspan="3">
    अग्रिम_______________________________ब्यौरे (विनिर्दिष्ट किये जाय) अन्य 
    खर्च______________________________(विनिर्दिष्ट किये जाये)
</td>
</tr>
<tr>
<td colspan="3" align="center">
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <br />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;<asp:Label 
        ID="lblgodampal" runat="server" Text="Label" Font-Bold="True"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
    <br />(माल गोदामपाल का नाम लिखें)
</td>
</tr>
<tr>
<td colspan="3" align="justify">
    माल के संग्रहण तथा परिरक्षण से सम्बंधित समस्त विधिमान्य खर्चो के लिए,साथ ही 
    अग्रिम दी गई धनराशि,ब्याज,बीमा,परिवहन,मजदूरी,तुलाई तथा ऐसे माल से सबंधित अन्य 
    दूसरे खर्च और व्यय के समस्त विधिमान्य दावों के लिए भी अधिकार का दावा करता हूँ ।
</td>
</tr>
<tr class="fountcolor">
<td align="right" colspan="3">
<asp:Image ID="Image5" runat="server" Height="30px" Visible="false"
        ImageUrl="~/images/dsc1.png" Width="80px" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </td>
</tr>
<tr class="fountcolor">
<td align="right" colspan="3">
<asp:Label ID="lbldscT" runat="server" Text="" Font-Bold="True"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
</tr>
<tr class="fountcolor">
<td align="right" colspan="3">
<asp:Label ID="lblDSC_Holder" runat="server" Text="" Font-Bold="false"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
</tr>
<%--<tr class="fountcolor">
<td>
<br />
</td>
<td colspan="2">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblSerNo" runat="server" Text="" Font-Bold="false"></asp:Label></td>
</tr>--%>
<tr class="fountcolor">
<td align="right" colspan="3">
<asp:Label ID="lblSigningDate" runat="server" Text="" Font-Bold="false"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
</tr>
<tr class="fountcolor">
<td align="right" colspan="3">
<asp:Label ID="lblIpAdd" runat="server" Text="" Font-Bold="false"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
</tr>
<tr class="fountcolor">
<td>
<br />
</td>
<td>
<br />
</td>
<td>
<br />
</td>
</tr>
<tr class="fountcolor">
<td align="right" colspan="3">
<asp:Label ID="Label4" runat="server" Text="माल गोदामपाल के हस्ताक्षर" Font-Bold="false"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
</tr>
<tr class="fountcolor">
<td>
<br />
</td>
<td>
<br />
</td>
<td>
<br />
</td>
</tr>
<tr class="fountcolor">
<td colspan="3">
    WHR&nbsp;जारी दिनांक: 
    <asp:Label ID="lbldate3" runat="server" Text="Label" Font-Bold="True"></asp:Label>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label 
        ID="mpwlcAothO" runat="server"></asp:Label></td>
</tr>
<tr>
<td colspan="3" align="justify">
    नीचे उल्लेखित माल एतदद्वारा माल गोदाम से सुपुर्दगी के लिए इस रसीद से निकाल दिया 
    गया है/न दिया गया शेष माल,दिए गए माल के भाग पर अदत्त खर्चे तथा अग्रिम के 
    लिए अधिकार के अध्यधीन है :-
</td>
</tr>
<tr>
<td colspan="3">


    <asp:ListView ID="ListView2" runat="server">
     <LayoutTemplate>
     <table style="border-width: thin; border-style: solid; border-collapse: collapse;" align="center">
    <tr runat="server" id="headerRow0" style="background-color: #C0C0C0">
    <td width="150" align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
    दिनांक
    </td>
    <td width="230" align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
    निकाली गई मात्रा
    </td>
     <td width="230" align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
    निकाले गए बोरे
    </td>
    <td width="220" align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
   हस्ताक्षर
    </td>
    <td width="180" align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
   रसीद पर देय(शेष) मात्रा
       </td>
    <td width="180" align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
   रसीद पर देय(शेष) बोरे
       </td>
    </tr>
    <tr style="background-color: #C0C0C0">
    <td align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
    (1)
    </td>
    <td align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
    (2)
    </td>
    <td align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
    (3)
    </td>
    <td align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
    (4)
    </td>
    <td align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
    (5)
    </td>
      <td align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
    (6)
    </td>
    </tr>
    <tr runat="server" id="itemPlaceholder"></tr>
    </table>

     </LayoutTemplate>
    <ItemTemplate>
    
    <tr>
    <td align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
       <%# Eval("Date")%>
    </td>
    <td align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
    <%# Eval("DelQty")%>
    </td>
    <td align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
    <%# Eval("DelBags")%>
    </td>
    <td align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
   
    </td>
    <td align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
    <%# Eval("AvlQty")%>
    </td>
   <td align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
    <%# Eval("AvlBags")%>
    </td>
    
    </tr>
   
    </ItemTemplate>
     <EmptyDataTemplate>
              
              <table style="border-width: thin; border-style: solid; border-collapse: collapse;" align="center">
    <tr runat="server" id="headerRow0" style="background-color: #C0C0C0">
    <td width="150" align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
    दिनांक
    </td>
    <td width="230" align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
    निकाली गई मात्रा
    </td>
      <td width="230" align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
    निकाले गए बोरे
    </td>
    <td width="220" align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
   हस्ताक्षर
    </td>
    <td width="180" align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
   रसीद पर देय(शेष) मात्रा
       </td>
      <td width="180" align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
   रसीद पर देय(शेष) बोरे
       </td>
    </tr>
    <tr style="background-color: #C0C0C0">
    <td align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
    (1)
    </td>
    <td align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
    (2)
    </td>
    <td align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
    (3)
    </td>
    <td align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
    (4)
    </td>
     <td align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
    (5)
    </td>
     <td align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
    (6)
    </td>
    </tr>
   <tr>
    <td align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
    <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        
        
    </td>
    <td align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
     <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        
        
    </td>
    <td align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
   <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        
        
    </td>
    <td align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
 <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        
        
    </td>
     <td align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
     <br />
        <br />
        <br />
        <br />
        <br />
        <br />
         
        
    </td>
     <td align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
   <br />
        <br />
        <br />
        <br />
        <br />
        <br />
         
    </td>
    </tr>
    </table>

          </EmptyDataTemplate>

    </asp:ListView>


</td>
</tr>
</table>

<div style="page-break-after:always"></div>
  <div>&nbsp;
</div>
   <%-- /second whr/--%>
   
<table id="Table1" visible="false"
        style="border-style: solid; border-width: thin; color: #FF5050; " 
        width="900px" runat="server">
<tr>
<td align="left" valign="top" style="color: #FF5050;" rowspan="2">
<table>
<tr>
<td valign="top">
फार्म क्रमांक-3 (Form-III)<br />
(देखिये नियम-14)<br /> (See Rule-14)
</td>
<td>
&nbsp;
    <asp:Image ID="Image1" runat="server" Height="65px" 
        ImageUrl="~/images/mpwlc.png" Width="71px" />
</td>
</tr>
</table>

</td>
<td style="font-size:medium; font-family: Arial, Helvetica, sans-serif; color: #0000FF;" 
        align="center">माल गोदाम रसीद<br /> WAREHOUSE RECEIPT </td>
<td style="border-style: solid; border-width: thin; border-color: inherit;">
    <asp:Label ID="lblcopytypeoff" runat="server" Text="OFFICE COPY" Font-Size="Small" 
        ForeColor="#FF5050"></asp:Label>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</td>
</tr>
<tr>

<td style="font-size:small; color: #FF5050;" align="center">(मध्यप्रदेश कृषि माल 
    गोदाम अधिनियम-1947 के अधीन)<br /> (Under the Madhya Pradesh Agriculture 
    Warehouse Act,1947) </td>
<td style="color: #FF5050;">
    माल/गोदाम स्थल
    <br />
Warehouse at:<asp:Label ID="lblbranchoff" runat="server" Text="Label"></asp:Label></td>
</tr>
<tr>
<td>
    <asp:Label ID="lblnegooff" runat="server" Text="lblnegooff"></asp:Label>
    <br /> 
    
    <asp:Label ID="lblnegohoff" runat="server" Text="lblnegohoff"></asp:Label>
    </td>
<td style="font-size:small" align="center">
    <asp:Label ID="lblcorpnamehndof" runat="server" 
        Text="(म.प्र. वेयरहाउसिंग एंड लाजिस्टिक्स कार्पोरेशन)"></asp:Label>
    <br /> 
    <asp:Label ID="lblcorpnameof" runat="server" 
        Text="(Madhya Pradesh Warehousing and Logistics Corporation)"></asp:Label>
    &nbsp;</td>
<td class="fountcolor">
    रुपये के लिए आबद्ध माल गोदाम<br /> Warehouse Bounded for Rs.</td>
</tr>
<tr>
<td colspan="2">
WHR No:<asp:Label ID="lblwhrNooff" runat="server" Text="Label" Font-Bold="True"></asp:Label></td>

<td>
    <asp:Label ID="lblcuurentdateoff" runat="server" Text="Label"></asp:Label>
</td>
</tr>
<tr>
<td colspan="2" class="fountcolor">
व्यक्ति,फर्म,कम्पनी/सरकारी संस्था का नाम:- 
    <asp:Label ID="Label9" runat="server" Text="MPWLC" Font-Bold="True"></asp:Label>
</td>
<td rowspan="3">
    <asp:Image ID="Image4" runat="server" />

<%--"https://chart.googleapis.com/chart?chs=100x100&amp;cht=qr&amp;chl=WHR No='' &amp;First(Fields!Depositor_WHR_Id.Value, &quot;WHR&quot;) &amp;&quot;Commodity=&quot;&amp;First(Fields!Commodity_Name.Value, &quot;WHR&quot;) &amp;&quot;WHR Date=&quot;&amp; First(Fields!Depositdate.Value, &quot;WHR&quot;) &amp;&quot;Total Bags=&quot;&amp; Sum(Fields!TotalBags_Received.Value, &quot;WHR&quot;) &amp;&quot;Total Weight=&quot;&amp; Sum(Fields!Total_Qty_Received.Value, &quot;WHR&quot;) &amp;&quot;&quot;"--%>
</td>
</tr>
<tr>
<td colspan="2" class="fountcolor">
    पता:-
    <asp:Label ID="lbladdoffice" runat="server" Text="Label" Font-Bold="True"></asp:Label>
    </td>

</tr>
<tr>
<td colspan="2" class="fountcolor" nowrap="nowrap">
    लाईसेन्स क्रमांक:-&nbsp;
    <asp:Label ID="lbllicofficeno" runat="server" Font-Bold="True" Text="Label"></asp:Label>
    &nbsp;&nbsp; लाइसेंस दिनांक को समाप्त होता है&nbsp;&nbsp;
    <asp:Label ID="lbllicsnceoffedate" runat="server" Font-Bold="True" Text="Label"></asp:Label>
    </td>
</tr>
<tr>
<td colspan="3" class="fountcolor" nowrap="nowrap">
    Depositor Form No.:-&nbsp;
    <asp:Label ID="lblDFNo2" runat="server" Font-Bold="True" Text=""></asp:Label>
    &nbsp;&nbsp;<asp:Label ID="lblAccRej2" runat="server" Text="Label" Font-Bold="True"></asp:Label>&nbsp;&nbsp;
    <asp:Label ID="lblAccNo2" runat="server" Font-Bold="True" Text=""></asp:Label>
    &nbsp;&nbsp;Society Name :&nbsp;&nbsp;
    <asp:Label ID="lblSname2" runat="server" Font-Bold="True" Text=""></asp:Label>
    </td>
</tr>
<tr>
<td colspan="3" class="fountcolor">
   <p align="justify"> यह प्रमाणित किया जाता है कि हमें संग्रहण माल गोदाम : 
    <asp:Label ID="lblgodownoffice" runat="server" Text="Label" Font-Bold="True"></asp:Label>&nbsp;&nbsp;&nbsp;
     एवं स्टेक :<asp:Label ID="lblStack2" runat="server" Text="" Font-Bold="True"></asp:Label>&nbsp;&nbsp;&nbsp;
    में श्री 
    <asp:Label ID="lbldepositopoffice" runat="server" Text="Label" Font-Bold="True"></asp:Label>
    &nbsp;से इसमें दिए गए निबंधनों तथा शर्तों के अध्यधीन नीचे वर्णित सम्पति इसमे दी गई स्थिति को छोड़ _______________को लेखे में उपरी रूप
    से अच्छी हालत में प्राप्त हुई । यह संपत्ति श्री_________________________________________________&nbsp;&nbsp; को या उसके द्वारा आदेशित 
    व्यक्ति या धारक को सभी संग्रहण उठाई-धराई और अन्य खर्चो का भुगतान करने तथा समुचित रूप से 
    पृष्ठांकित माल गोदाम रसीद प्रस्तुत करने पर सुपुर्द कर दी जायेगी !
    </p>
</td>
</tr>

<tr class="fountcolor">
<td colspan="3" class="fountcolor">
    <asp:ListView ID="lvofficem" runat="server">
     <LayoutTemplate>
     <table  style="border-style: solid; border-width: thin; padding: inherit; margin: auto; border-collapse: collapse;">
    <tr runat="server" id="headerRow" style="background-color: #C0C0C0">
    <td width="110" align="center" valign="top" style="border-style: solid; border-width: thin; border-collapse: separate">
    उपज का विवरण
    </td>
    <td width="40" align="center" valign="top" style="border-style: solid; border-width: thin; border-collapse: separate">
    श्रेणी
    </td>
    <td width="150" align="center" valign="top" style="border-style: solid; border-width: thin; border-collapse: separate">
    बोरों की संख्या<br />
(अंको में व शब्दों में)
    </td>
    <td width="150" align="center" valign="top" style="border-style: solid; border-width: thin; border-collapse: separate">
    कुल वजन(In Qtls)<br />
(अंको में व शब्दों में)
    </td>
    <td width="100" align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
    दर प्रति (Qtls)
    (In Rs.)
    </td>
    <td width="170" align="center" valign="top" style="border-style: solid; border-width: thin; border-collapse: separate">
    कुल कीमत(In Rs.)<br />
(अंको में व शब्दों में)
    </td>
    <td width="90" align="center" valign="top" style="border-style: solid; border-width: thin; border-collapse: separate">
    रिमार्क
    </td>
    </tr>
    <tr style="border: thin solid #000000; background-color: #C0C0C0">
    <td align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
    (1)
    </td>
    <td align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
    (2)
    </td>
    <td align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
    (3)
    </td>
    <td align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
    (4)
    </td>
    <td align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
    (5)
    </td>
    <td align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
    (6)
    </td>
    <td align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
    (7)
    </td>
    </tr>
    <tr runat="server" id="itemPlaceholder"></tr>
    </table>

     </LayoutTemplate>
    <ItemTemplate>
    
    <tr class="fountcolor">
    <td align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
    <asp:Label ID="EmpnoLabel" runat="server" Text='<%# Eval("Commodity_Name") %>' />
    </td>
    <td align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
    <%# Eval("Category_Name")%>
    </td>
    <td align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
    <%# Eval("TotalBags_Received")%>
    </td>
    <td align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
    <%# Eval("Total_Qty_Received")%>
    </td>
    <td align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
    <%# Eval("MktValue_of_Commodityno")%>
    </td>
    <td align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
    <%# Eval("MktValue_of_Commodity")%>
    </td>
   <td align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
       <%# Eval("Remark")%>
    </td>
    </tr>
   
    </ItemTemplate>
    </asp:ListView>
</td>
</tr>
<tr class="fountcolor">
<td colspan="3">
    <br />
    संग्रहण (दर)
    <asp:Label ID="Label15" runat="server" Font-Bold="True" Text="As per tarrif"></asp:Label>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; प्रति (इकाई) __________________________प्रतिमाह कब से (दिनांक):-&nbsp;
    <asp:Label ID="lblstordateoff" runat="server" Text="" Font-Bold="True"></asp:Label>
	 &nbsp;
    <asp:Label ID="lblCropYear2" runat="server" Text="फसल वर्ष" Font-Bold="True"></asp:Label>
    </td>
</tr>
<tr>
<td colspan="3">
    <br />
उठाई-धराइ (खर्च)______________________प्रति (इकाई) __________________________(खर्च) अंदर ले जाने तथा बाहर निकालने का सम्मिलित कर !
</td>
</tr>
<tr>
<td colspan="3">
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        अग्रिम दे दिए गए है और इस माल पर हुआ दायित्व इस प्रकार है -
</td>
</tr>
<tr>
<td colspan="3">
    गाड़ी-भाड़ा (काटेज)___________________________वस्तु 
    भाड़ा(फ्रेट)_______________________वजन_____________________________</td>
</tr>
<tr>
<td colspan="3">
    अग्रिम_______________________________ब्यौरे (विनिर्दिष्ट किये जाय) अन्य 
    खर्च______________________________(विनिर्दिष्ट किये जाये)
</td>
</tr>
<tr>
<td colspan="3" align="center">
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <br />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;<asp:Label 
        ID="lblgodampaloffice" runat="server" Text="Label" Font-Bold="True"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
    <br />(माल गोदामपाल का नाम लिखें)
</td>
</tr>
<tr>
<td colspan="3" align="justify">
    माल के संग्रहण तथा परिरक्षण से सम्बंधित समस्त विधिमान्य खर्चो के लिए,साथ ही 
    अग्रिम दी गई धनराशि,ब्याज,बीमा,परिवहन,मजदूरी,तुलाई तथा ऐसे माल से सबंधित अन्य 
    दूसरे खर्च और व्यय के समस्त विधिमान्य दावों के लिए भी अधिकार का दावा करता हूँ ।
</td>
</tr>
<tr class="fountcolor">
<td align="right" colspan="3">
<asp:Image ID="Image6" runat="server" Height="30px" Visible="false"
        ImageUrl="~/images/dsc1.png" Width="80px" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </td>
</tr>
<tr class="fountcolor">
<td align="right" colspan="3">
<asp:Label ID="lbldscT2" runat="server" Text="" Font-Bold="True"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
</tr>
<tr class="fountcolor">
<td align="right" colspan="3">
<asp:Label ID="lblDSC_Holder2" runat="server" Text="" Font-Bold="false"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
</tr>
<%--<tr class="fountcolor">
<td>
<br />
</td>
<td colspan="2">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblSerNo2" runat="server" Text="" Font-Bold="false"></asp:Label></td>
</tr>--%>
<tr class="fountcolor">
<td align="right" colspan="3">
<asp:Label ID="lblSigningDate2" runat="server" Text="" Font-Bold="false"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
</tr>
<tr class="fountcolor">
<td align="right" colspan="3" class="style1">
<asp:Label ID="lblIpAdd2" runat="server" Text="" Font-Bold="false"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
</tr>
<tr class="fountcolor">
<td>
<br />
</td>
<td>
<br />
</td>
<td>
<br />
</td>
</tr>
<tr class="fountcolor">
<td colspan="3">
    WHR&nbsp;जारी दिनांक: 
    <asp:Label ID="lblwhrdateoffice" runat="server" Text="Label" Font-Bold="True"></asp:Label>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label 
        ID="mpwlcothOf" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; माल गोदामपाल के हस्ताक्षर
</td>
</tr>
<tr>
<td colspan="3" align="justify">
    नीचे उल्लेखित माल एतदद्वारा माल गोदाम से सुपुर्दगी के लिए इस रसीद से निकाल दिया 
    गया है/न दिया गया शेष माल,दिए गए माल के भाग पर अदत्त खर्चे तथा अग्रिम के 
    लिए अधिकार के अध्यधीन है :-
</td>
</tr>
<tr>
<td colspan="3">


    <asp:ListView ID="lvoffice" runat="server">
     <LayoutTemplate>
     <table style="border-width: thin; border-style: solid; border-collapse: collapse;" align="center">
    <tr runat="server" id="headerRow0" style="background-color: #C0C0C0">
    <td width="150" align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
    दिनांक
    </td>
    <td width="230" align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
    निकाली गई मात्रा
    </td>
     <td width="230" align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
    निकाले गए बोरे
    </td>
    <td width="220" align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
   हस्ताक्षर
    </td>
    <td width="180" align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
   रसीद पर देय(शेष) मात्रा
       </td>
      <td width="180" align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
   रसीद पर देय(शेष) बोरे
       </td>
    </tr>
    <tr style="background-color: #C0C0C0">
    <td align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
    (1)
    </td>
    <td align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
    (2)
    </td>
     <td align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
    (3)
    </td>
    <td align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
    (4)
    </td>
    <td align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
    (5)
    </td>
     <td align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
    (6)
    </td>
    </tr>
    <tr runat="server" id="itemPlaceholder"></tr>
    </table>

     </LayoutTemplate>
    <ItemTemplate>
    
    <tr>
    <td align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
       <%# Eval("Date")%>
    </td>
    <td align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
    <%# Eval("DelQty")%>
    </td>
     <td align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
    <%# Eval("DelBags")%>
    </td>
    <td align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
   
    </td>
    <td align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
    <%# Eval("AvlQty")%>
    </td>
    <td align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
    <%# Eval("AvlBags")%>
    </td>
    </tr>
   
    </ItemTemplate>
     <EmptyDataTemplate>
              
              <table style="border-width: thin; border-style: solid; border-collapse: collapse;" align="center">
    <tr runat="server" id="headerRow0" style="background-color: #C0C0C0">
    <td width="150" align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
    दिनांक
    </td>
    <td width="230" align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
    निकाली गई मात्रा
    </td>
     <td width="230" align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
    निकाले गए बोरे
    </td>
    <td width="220" align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
   हस्ताक्षर
    </td>
    <td width="180" align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
   रसीद पर देय(शेष) मात्रा
       </td>
      <td width="180" align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
   रसीद पर देय(शेष) बोरे
       </td>
    </tr>
    <tr style="background-color: #C0C0C0">
    <td align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
    (1)
    </td>
    <td align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
    (2)
    </td>
    <td align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
    (3)
    </td>
    <td align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
    (4)
    </td>
     <td align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
    (5)
    </td>
     <td align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
    (6)
    </td>
    </tr>
    <tr >
    <td align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
    <br />
        <br />
        <br />
        <br />
        <br />
        <br />
    </td>
    <td align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
     <br />
        <br />
        <br />
        <br />
        <br />
        <br />
    </td>
    <td align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
   <br />
        <br />
        <br />
        <br />
        <br />
        <br />
    </td>
    <td align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
 <br />
        <br />
        <br />
        <br />
        <br />
        <br />
    </td>
     <td align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
     <br />
        <br />
        <br />
        <br />
        <br />
        <br />
    </td>
     <td align="center" style="border-style: solid; border-width: thin; border-collapse: separate">
   <br />
        <br />
        <br />
        <br />
        <br />
        <br />
    </td>
    </tr>
    </table>

          </EmptyDataTemplate>

    </asp:ListView>


</td>
</tr>
</table>
</div>
</asp:Panel>
    
  <asp:Panel ID="pnllogin" runat="server">
    <table id="modalpop" class="modalpop">
    <tr>
    <td colspan="2" align="center"  
            valign="top">
    <h4>Branch Manager's Passowrd:</h4>
    </td>
    </tr>
    <tr>
    <td>
    Password:
    </td>
    <td>
        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" autocomplete="off"></asp:TextBox>
    </td>
    </tr>
    <tr>
    <td>
       <%-- <asp:Button ID="btnsubmitpwd" runat="server" Text="Submit" 
            onclick="btnsubmitpwd_Click" OnClientClick="PrintDiv();" />--%>
          &nbsp;&nbsp;
          <asp:Button ID="btnsubmitpwd" runat="server" Text="Submit" 
            onclick="btnsubmitpwd_Click" />
    </td>
    <td>
        <asp:Button ID="btncancelpwd" runat="server" Text="Cancel" />
    </td>
    </tr>
    </table>
    </asp:Panel>
   
   
    <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" CancelControlID="btncancelpwd" TargetControlID="btnbmpassword" BackgroundCssClass="popbag" PopupControlID="pnllogin">
    
    </cc1:ModalPopupExtender>
    
    <cc1:AnimationExtender ID="popupAnimation" runat="server" TargetControlID="btnbmpassword">
        <Animations>
                <OnClick>
         <Parallel AnimationTarget="pnllogin" Duration="0.4" Fps="10">
            <FadeIn />
          </Parallel>
       </OnClick>
        </Animations>
    </cc1:AnimationExtender>
</asp:Content>


