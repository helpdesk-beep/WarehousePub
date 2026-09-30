<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="Branch_Welcome.aspx.cs" Inherits="Branch_Welcome" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
<script type="text/javascript">

    function Deductionamtcalculate() {

        var Lenght = "0";
        var Width = "0";
        var Height = "0";
        //        var Other = "0";


        Lenght = document.getElementById('<%= txtLenght.ClientID %>');
        Width = document.getElementById('<%= txtWidth.ClientID %>');
        Height = document.getElementById('<%= txtHeight.ClientID %>');

        var TotalD = findNull(Lenght) * findNull(Width) * ((findNull(Height) - 3) / 80);
        document.getElementById('<%= txtSciCap.ClientID %>').value = TotalD.toFixed(2);

    }
    /////////////////////Null function///////////////////////
    function findNull(x) {

        var TDS = x.value;
        if (TDS == "") {
            TDS = 0;
            //            alert(TDS);
        }
        else {
            TDS = parseFloat(TDS);
        }
        return TDS;
        //        alert(TDS+1);
    } 
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
height:98%;
margin: 0 auto;
position: fixed;
z-index: 101;
}
.pop{
min-width: 900px;
width: 900px;
min-height: 150px;
margin: 0px auto;
background: #FFFFFF;
position: relative;
z-index: 103;
padding: 10px;
border-radius: 5px;
box-shadow: 0 5px 10px #000;
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


    <%--<asp:GridView ID="gvprocdata" runat="server" AutoGenerateColumns="False">
        <Columns>
            <asp:BoundField DataField="WhrRequest" HeaderText="Total Depositor Form" />
            <asp:BoundField DataField="AcceptQty" HeaderText="Depositor form Qty" />
            <asp:BoundField DataField="totalwhr" HeaderText="WHR from Depositor Form" />
            <asp:BoundField DataField="TotalQty" HeaderText="WHR Qty" />
            <asp:BoundField DataField="MenualWhr" HeaderText="Menul WHR (CSMS)" />
            <asp:BoundField DataField="MenualTotal" HeaderText="Menual Total" />
            <asp:BoundField DataField="totalwhrtruck" HeaderText="TruckWiseWHR " />
            <asp:BoundField DataField="TotalQtyTruck" HeaderText="Truck Wise Qty" />
            <asp:BoundField HeaderText="Total WHR" />
            <asp:BoundField HeaderText="Total WHR Qty" />
            <asp:BoundField HeaderText="% of Total whr Qty">
                <ItemStyle BackColor="#3366FF" Font-Bold="True" />
            </asp:BoundField>
        </Columns>
    </asp:GridView>--%>
    <table>
    <tr>
    <td>
    <span style="font-size: small">
    1.भावांतर योजना के अंतर्गत जमा हो रहे स्टाक की रसीद(WHR) जारी करने के लिए सर्वप्रथम Bhavanter Farmer(Depositor Type) के अंतर्गत Depositor Create करे एवं WHR Generate करने के समय Source of Arrival Bhavanter Scheme सिलेक्ट करे।<br /> 
    </span>  
    </td>
    <td>
    <img alt="New" src="images/new6.gif" id="Img1" runat="server" />
    </td>
   
    </tr>
    <tr>
                                <td colspan="4" align="center" valign="top">
                                <h2 style=" color:White; background-color:#719cb6; height:25px">Godown Wise Stock Status</h2>
                                <asp:Label ID="lblCount" runat="server" Font-Bold="True" Font-Size="10pt" 
                                                                ForeColor="Navy" Text=""></asp:Label>
             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="lblcropyear" runat="server" Font-Bold="True" Font-Size="10pt" 
                                                                ForeColor="Navy" Text="Crop Year : "></asp:Label>
                                <asp:DropDownList ID="ddlcropyr" runat="server" AutoPostBack="True" 
           TabIndex="1" Height="25px" Width="200px" Font-Size="10pt" 
                                        onselectedindexchanged="ddlcropyr_SelectedIndexChanged">
                                                            </asp:DropDownList>
           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                             <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="10pt" 
                                                                ForeColor="Navy" Text="Unit in Qtl."></asp:Label>
                                    <asp:Panel ID="panelContainer" runat="server" Height="380px" ScrollBars="Vertical"
                                        Width="100%" BorderColor="navy" BorderWidth="1px">
                                        <asp:GridView ID="gvsummary" runat="server" AutoGenerateColumns="false" EnableModelValidation="True" ShowFooter="true"
                                            Width="100%" Font-Size="11pt" onrowdatabound="gvsummary_RowDataBound">
                                            <Columns>
                                             <asp:TemplateField HeaderText="SNo." ItemStyle-HorizontalAlign="Center">
                                                     <FooterTemplate>
                                                          <asp:Label ID="SNo" runat="server" Text=""></asp:Label>
                                                     </FooterTemplate>
                               
                                                      <ItemTemplate>
                                                                <%#Container.DataItemIndex+1 %>
                                                      </ItemTemplate>
                                                 </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Godown Name">
                                                     <FooterTemplate>
                                                          <asp:Label ID="Godown_Name" runat="server" Text="Total :"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemTemplate>
                                                          <asp:Label ID="lblGodown_Name" runat="server" Text='<%# Eval("Godown_Name") %>'></asp:Label>
                                                      </ItemTemplate>
                                                 </asp:TemplateField>
                                               <%--  <asp:BoundField DataField="Godown_Name" HeaderText="Godown">
                                                    <ItemStyle Width="150px" HorizontalAlign="Left" />
                                                    <HeaderStyle Width="150px" />
                                                </asp:BoundField>--%>
                                                 <asp:TemplateField HeaderText="Godown Type">
                                                     <FooterTemplate>
                                                          <asp:Label ID="Hired_Type" runat="server" Text=""></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemTemplate>
                                                          <asp:Label ID="lblHired_Type" runat="server" Text='<%# Eval("Hired_Type") %>'></asp:Label>
                                                      </ItemTemplate>
                                                 </asp:TemplateField>
                                               <%--  <asp:BoundField DataField="Hired_Type" HeaderText="Godown Type">
                                                    <ItemStyle Width="50px" HorizontalAlign="Left" />
                                                    <HeaderStyle Width="50px" />
                                                </asp:BoundField>--%>
                                                 <asp:TemplateField HeaderText="Godown Capacity" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
                                                     <FooterTemplate>
                                                          <asp:Label ID="Godown_Capacity" runat="server" Text="Godown Capacity"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemTemplate>
                                                          <asp:Label ID="lblGodown_Capacity" runat="server" Text='<%# Eval("Godown_Capacity") %>'></asp:Label>
                                                      </ItemTemplate>
                                                 </asp:TemplateField>
                                           <%--     <asp:BoundField DataField="Godown_Capacity" HeaderText="Godown Capacity">
                                                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                                                    <HeaderStyle Width="50px" />--%>
                                               <%-- </asp:BoundField>--%>
                                                 <asp:TemplateField HeaderText="Receive Bags" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
                                                     <FooterTemplate>
                                                          <asp:Label ID="RecBags" runat="server" Text="Receive Bags"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemTemplate>
                                                          <asp:Label ID="lblRecBags" runat="server" Text='<%# Eval("RecBags") %>'></asp:Label>
                                                      </ItemTemplate>
                                                 </asp:TemplateField>
                                                <%--<asp:BoundField DataField="RecBags" HeaderText="Receive Bags">
                                                    <ItemStyle Width="50px" HorizontalAlign="Center" ForeColor="Purple" />
                                                    <HeaderStyle Width="50px" />
                                                </asp:BoundField>--%>
                                                 <asp:TemplateField HeaderText="Receive Qty" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
                                                     <FooterTemplate>
                                                          <asp:Label ID="RecQty" runat="server" Text="Receive Qty"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemTemplate>
                                                          <asp:Label ID="lblRecQty" runat="server" Text='<%# Eval("RecQty") %>'></asp:Label>
                                                      </ItemTemplate>
                                                 </asp:TemplateField>
                                               <%-- <asp:BoundField DataField="RecQty" HeaderText="Receive Qty">
                                                    <ItemStyle Width="50px" HorizontalAlign="Center" ForeColor="Purple" />
                                                      <HeaderStyle Width="50px" />
                                                </asp:BoundField>--%>
                                                 <asp:TemplateField HeaderText="Issue Bags" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
                                                     <FooterTemplate>
                                                          <asp:Label ID="DelBags" runat="server" Text="Issue Bags"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemTemplate>
                                                          <asp:Label ID="lblDelBags" runat="server" Text='<%# Eval("DelBags") %>'></asp:Label>
                                                      </ItemTemplate>
                                                 </asp:TemplateField>
                                               <%--  <asp:BoundField DataField="DelBags" HeaderText="Issue Bags">
                                                   <ItemStyle Width="50px" HorizontalAlign="Center" ForeColor="Maroon"/>
                                                      <HeaderStyle Width="50px" />
                                                </asp:BoundField>--%>
                                                 <asp:TemplateField HeaderText="Issue Qty" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
                                                     <FooterTemplate>
                                                          <asp:Label ID="DelQty" runat="server" Text="Issue Qty"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemTemplate>
                                                          <asp:Label ID="lblDelQty" runat="server" Text='<%# Eval("DelQty") %>'></asp:Label>
                                                      </ItemTemplate>
                                                 </asp:TemplateField>
                                                <%-- <asp:BoundField DataField="DelQty" HeaderText="Issue Qty">
                                                    <ItemStyle Width="50px" HorizontalAlign="Center" ForeColor="Maroon"/>
                                                      <HeaderStyle Width="50px" />
                                                </asp:BoundField>--%>
                                                 <asp:TemplateField HeaderText="Available Bags" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
                                                     <FooterTemplate>
                                                          <asp:Label ID="AvaBags" runat="server" Text="Available Bags"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemTemplate>
                                                          <asp:Label ID="lblAvaBags" runat="server" Text='<%# Eval("AvaBags") %>'></asp:Label>
                                                      </ItemTemplate>
                                                 </asp:TemplateField>
                                               <%-- <asp:BoundField DataField="AvaBags" HeaderText="Available Bags">
                                                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                                                      <HeaderStyle Width="50px" />
                                                </asp:BoundField>--%>
                                                 <asp:TemplateField HeaderText="Available Qty" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
                                                     <FooterTemplate>
                                                          <asp:Label ID="AvaQty" runat="server" Text="Available Qty"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemTemplate>
                                                          <asp:Label ID="lblAvaQty" runat="server" Text='<%# Eval("AvaQty") %>'></asp:Label>
                                                      </ItemTemplate>
                                                 </asp:TemplateField>
                                               <%--<asp:BoundField DataField="AvaQty" HeaderText="Available Qty">
                                                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                                                      <HeaderStyle Width="50px" />
                                                </asp:BoundField>--%>
                                                <asp:TemplateField HeaderText="Utilization %" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
                                                     <FooterTemplate>
                                                          <asp:Label ID="UtilPerc" runat="server" Text="Utilization %"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemTemplate>
                                                          <asp:Label ID="lblUtilPerc" runat="server" Text='<%# Eval("UtilPerc") %>'></asp:Label>
                                                      </ItemTemplate>
                                                 </asp:TemplateField>
                                                <%--<asp:BoundField DataField="UtilPerc" HeaderText="Utilization %">
                                                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                                                      <HeaderStyle Width="50px" />
                                                </asp:BoundField>--%>
                                                 <asp:TemplateField HeaderText="Available Capacity" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
                                                     <FooterTemplate>
                                                          <asp:Label ID="AvaCapacity" runat="server" Text="Available Capacity"></asp:Label>
                                                     </FooterTemplate>
                                                     <ItemTemplate>
                                                          <asp:Label ID="lblAvaCapacity" runat="server" Text='<%# Eval("AvaCapacity") %>'></asp:Label>
                                                      </ItemTemplate>
                                                 </asp:TemplateField>
                                                 <%-- <asp:BoundField DataField="AvaCapacity" HeaderText="Available Capacity">
                                                    <ItemStyle Width="50px" HorizontalAlign="Center" ForeColor="Blue" />
                                                      <HeaderStyle Width="50px" />
                                                </asp:BoundField>--%>
                                            </Columns>
                                            <FooterStyle BackColor="Maroon" Font-Bold="true" ForeColor="White" Height="20pt" />
                                            <RowStyle Height="20pt"/>
                                            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                                                Height="20px" Font-Size="10pt" />
                                            <AlternatingRowStyle BackColor="White" />
                                        </asp:GridView>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
    <td align="center">
    <h2 style="color: #FF0000">
    Important Instructions
    </h2>
    </td>
    </tr>
                            <tr>

    <td>
   <hr />
    <span style="font-size: small">
    1.अपना ब्रांच प्रोफ़ाइल अपडेट रखें । WHR के लिए पता,गोदामपाल का नाम ,लाइसेन्स क्रमांक ओर लाइसेन्स दिनांक उसी का प्रदर्शित होगा।<br />
    2. WHR प्रिंट लेने के साथ साथ page के दूसरी ओर भंडारण की शर्तें का भी प्रिंट लेना है जो 
         <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="BranchPages/WHRInstructions.aspx">Print whr instruction</asp:HyperLink> बाले page से मिल जाएगा।<br />
    3. सॉफ्टवेर मे एंट्री करने मे किसी प्रकार का विलंब ना करे। स्कंध जमा या भुगतान के दौरान ऑनलाइन एंट्री उसी दिनांक मे करे । <br />
    4. Online Bill जारी करने के लिए "Account Operation" के अंतर्गत Storage Charges Bill एवं Godown Rent Bill का उपयोग करे एवं इस माह से समस्त जमाकर्ताओ एव गोदामो के बिल ऑनलाइन ही जारी करे ।<br />     
    5.WHR प्रिंट करने के लिए Print(WHR)Receipt का उपयोग करें। WHR प्रिंट करने पर पहला प्रिंट orignal कॉपी रहेगी दूसरी बार प्रिंट करने पर office कॉपी ओर उसके बाद प्रिंट नहीं लिया जा सकता। <br />
    6.किसी भी प्रकार की डिलीट request के लिए Delete Request page का उपयोग करते हुये जो प्रिंट निकलेगा उसे ब्रांच मैनेजर के sign द्वारा scan कॉपी संबंधित <b>क्षेत्रीय प्रबंधक </b> तथा <b> <a href="mailto:helpdesk@mpwlc.co.in">helpdesk@mpwlc.co.in</a> , ho@mpwlc.co.in </b> पर मेल करें। <br />
    7.किसी भी प्रकार की समस्या के लिए अपने तहसील व ब्रांच के नाम के साथ समस्या <a href="mailto:helpdesk@mpwlc.co.in">helpdesk@mpwlc.co.in</a> , ho@mpwlc.co.in पर मेल करें।
    <br />
    </span>
    </td>
    </tr>
    <tr>
    <td>
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
    <img alt="New" src="images/new6.gif" id="new" runat="server" />
         <%--<asp:Panel ID="pnllogin" class="popup" runat="server">
     <div class="popup" >
<div class="pop" style="background-color:#FFFFCC0">
<img visible="true" runat="server" src="~/images/close-icon.png" alt="quit" class="x" id="x" />

<table cellspacing="1" cellpadding="3">
<tr>

<td colspan="2">
<center><h1 style="color:Red; font-size:x-large">आवश्यक निर्देश</h1></center>
<p style="color:Maroon; font-size:medium;">आपकी शाखा के समस्त गोदामो को तहसील एवं गाँव से 3 दिवस मे मेप करे एवं किसी भी प्रकार की समस्या के लिए उक्त Mail ID : helpdesk@mpwlc.co.in.com पर मेल करे।
</p>
</td>
</tr>

<tr>
      <td align="left">
                                                            <asp:Label ID="lblGodown" runat="server" Font-Bold="True" Font-Size="10pt" ForeColor="Navy"
                                                                Text="Godown"></asp:Label>
                                                        </td>
                                                        <td align="left" valign="middle">
                                                            <asp:DropDownList ID="ddl_godown" runat="server" Width="400px" AutoPostBack="false"
                                                                CssClass="tb6" Height="20px" 
                                                                >
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                               
                                            <tr>
      <td align="left">
                                                            <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="10pt" ForeColor="Navy"
                                                                Text="Tehsil"></asp:Label>
                                                        </td>
                                                        <td align="left" valign="middle">
                                                            <asp:DropDownList ID="ddlPBlock" runat="server" Width="400px" AutoPostBack="true"
                                                                CssClass="tb6" Height="20px" onselectedindexchanged="ddlPBlock_SelectedIndexChanged"
                                                                >
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                        <tr>
      <td align="left">
                                                            <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Size="10pt" ForeColor="Navy"
                                                                Text="Village"></asp:Label>
                                                        </td>
                                                        <td align="left" valign="middle">
                                                            <asp:DropDownList ID="ddlVillage" runat="server" Width="400px" AutoPostBack="false"
                                                                CssClass="tb6" Height="20px"
                                                                >
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                            <tr>
<td colspan="2" align="center">
<asp:Button ID="btn1" runat="server" Text="Submit" onclick="btn1_Click" CssClass="BTNBLUE"/>
</td>
</tr>
<tr>
<td colspan="2">
<p  style="color:Maroon; font-size:medium;">नोट : कृपया सही जानकारी मैप करे। भरी गई जानकारी मे किसी प्रकार की त्रुटि पाये जाने पर आप स्वयं जिम्मेदार रहेंगे।</p>
</td>
</tr>
                                            </table>
<br/>
<br/>

</div>
</div>
</asp:Panel>
    <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="new" BackgroundCssClass="popup" PopupControlID="pnllogin">
    
    </asp:ModalPopupExtender> 
    
    <asp:AnimationExtender ID="popupAnimation" runat="server" TargetControlID="new">
        <Animations>
                <OnClick>
         <Parallel AnimationTarget="pnllogin" Duration="0.4" Fps="10">
            <FadeIn />
          </Parallel>
       </OnClick>
        </Animations>
    </asp:AnimationExtender>--%>
    
    
           <asp:Panel ID="pnllogin" class="popup" runat="server">
<div class="pop" style="background-color:#FFFFCC0">
<img visible="true" runat="server" src="~/images/close-icon.png" alt="quit" class="x" id="x" />

<table cellspacing="1" cellpadding="3">
<tr>

<td colspan="2">
<center><h1 style="color:Red; font-size:x-large">आवश्यक निर्देश</h1></center>
<p style="color:Maroon; font-size:medium;">
<%--खरीफ 2018-19 के भंडारण एवं मैपिंग की द्रष्टि से गोदाम संबंधी निम्न जानकारी सही करना अनिवार्य है, ऐसे गोदाम जो शाखा मे प्रदर्शित हो रहे है किन्तु इस वर्ष Existence मे नहीं है उनका Existence "No" सिलैक्ट करे एवं वे गोदाम जो शाखा पर इस वर्ष उपलब्ध है या Existence मे है चाहे वो भरा हो अथवा खाली हो उनका Existence "YES" सिलैक्ट कर गोदाम की निम्नानुसार समस्त जानकारी वैज्ञानिक क्षमता सहित आज सायं 05:30 तक अनिवार्य रूप से प्रविष्ट करे। किसी भी प्रकार की समस्या के लिए उक्त Mail ID : helpdesk@mpwlc.co.in.com पर मेल करे।--%>

 गोदाम की  समस्त जानकारी वैज्ञानिक भण्डारण क्षमता,भण्डारित स्कंध का क्लोसिंग बैलेंस,रजिस्ट्रेशन आईडी अनिवार्य रूप से प्रविष्ट करे । <br />
 वैज्ञानिक भण्डारण क्षमता में  गोदाम की कुल क्षमता प्रविष्ट करे चाहे स्कंध भण्डारित हो अथवा खाली ।

</p>
</td>
</tr>
   <tr>
    <td>
        <table>
        



<tr>
      <td align="left">
                                                            <asp:Label ID="lblGodown" runat="server" Font-Bold="True" Font-Size="10pt" ForeColor="Navy"
                                                                Text="Godown Name"></asp:Label>
                                                        </td>
                                                        <td align="left" valign="middle" colspan="3">
                                                            <asp:DropDownList ID="ddl_godown" runat="server" Width="300px" AutoPostBack="true"
                                                                CssClass="tb6" Height="25px" onselectedindexchanged="ddl_godown_SelectedIndexChanged" >
                                                            </asp:DropDownList>
                                                            
                                                      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label14" runat="server" Font-Bold="True" Font-Size="10pt" ForeColor="Navy"
                                                                Text="Godown No."></asp:Label>&nbsp;&nbsp;&nbsp;
<asp:TextBox ID="txtGNo" Width="50px" Height="20px" runat="server"></asp:TextBox>                                                                
                                                        </td>
                                                        
                                                    </tr>
                                                    <tr>
      <td align="left">
                                                            <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Size="10pt" ForeColor="Navy"
                                                                Text="Godown ID"></asp:Label>
                                                        </td>
                                                        <td align="left" valign="middle">
                                                             <asp:Label ID="lblGodownId" runat="server" Font-Bold="True" Font-Size="10pt" ForeColor="Navy"
                                                                Text=""></asp:Label>
                                                             <asp:Label ID="lblGodownName" runat="server" Font-Bold="True" Font-Size="10pt" ForeColor="Navy"
                                                               Visible="false" ></asp:Label>                                                                
                                                        
                                                        </td>
                                                    </tr>
                                                    
<tr>
      <td align="left" style="width:350px;">
                                                            <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Size="10pt" ForeColor="Navy"
                                                                Text="JVS Registration ID"></asp:Label>
                                                        </td>
                                                        <td align="left" valign="middle">
                                                            <asp:DropDownList ID="ddlRegID" runat="server" Width="300px" CssClass="tb6" 
                                                                Height="25px" AutoPostBack="True" 
                                                                onselectedindexchanged="ddlRegID_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            
                                      <asp:Label ID="lblwhname" runat="server" Font-Bold="True" Font-Size="10pt" ForeColor="Navy" Visible="false"></asp:Label>                                                            
                                                        </td>
                                                    </tr>                                                    
<%--                                                    <tr>
      <td align="left">
                                                            <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Size="10pt" ForeColor="Navy"
                                                                Text="क्या वर्तमान मे गोदाम Existence मे है?"></asp:Label>
                                                        </td>
                                                        <td align="left" valign="middle">
                                                            <asp:DropDownList ID="ddlExist" runat="server" Width="100px" AutoPostBack="false"
                                                                CssClass="tb6" Height="25px" onselectedindexchanged="ddlExist_SelectedIndexChanged" 
                                                                >
                                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                                                                 <asp:ListItem Text="No" Value="N"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>--%>
                                                 <tr>
      <td align="left">
                                                            <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Size="10pt" ForeColor="Navy"
                                                                Text="L x W x H (in Feet)"></asp:Label>
                                                        </td>
                                                        <td align="left" valign="middle">
                                                            <asp:TextBox ID="txtLenght" Width="50px" Height="20px" runat="server" AutoPostBack="true"
                                                                 Text="0"
                                                                ontextchanged="txtLenght_TextChanged"></asp:TextBox>
                                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" TargetControlID="txtLenght"
                                        ValidChars="0123456789.">
                                    </asp:FilteredTextBoxExtender>
                                                            <asp:TextBox ID="txtWidth" Width="50px" Height="20px" runat="server"  AutoPostBack="true"
                                                                Text="0" ontextchanged="txtWidth_TextChanged"></asp:TextBox>
                                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" TargetControlID="txtWidth"
                                        ValidChars="0123456789.">
                                    </asp:FilteredTextBoxExtender>
                                                            <asp:TextBox ID="txtHeight" Width="50px" Height="20px" runat="server"  AutoPostBack="true"
                                                                 Text="0" 
                                                                ontextchanged="txtHeight_TextChanged"></asp:TextBox>&nbsp;&nbsp;&nbsp;Height<=18 Feet
                                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" TargetControlID="txtHeight"
                                        ValidChars="0123456789.">
                                    </asp:FilteredTextBoxExtender>
                                                        </td>
                                                    </tr>
                                                       <tr>
      <td align="left">
                                                            <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Size="10pt" ForeColor="Navy"
                                                                Text="Scientific Capacity (In M.T)"></asp:Label>
                                                        </td>
                                                        <td align="left" valign="middle">
                                                            <asp:TextBox ID="txtSciCap" Width="125px" Height="20px" runat="server" Text="0" Enabled="false"></asp:TextBox>
                                                            <asp:Label ID="lblsc" runat="server"></asp:Label>
                                                            &nbsp;&nbsp;&nbsp;Calculated on Formula LxWx(H-3)/80
                                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" TargetControlID="txtSciCap"
                                        ValidChars="0123456789.">
                                    </asp:FilteredTextBoxExtender>
                                                           
                                                        </td>
                                                    </tr>
                                                   
<%--<tr>
                                                <td>
                                                    <asp:Label ID="Label17" runat="server" Font-Bold="True" Font-Size="10pt" ForeColor="Navy" Text="Godown Closing Balance(In MT) at 30/09/2017"></asp:Label>
                                                </td>
                                                <td>
                                                   <asp:TextBox ID="txtclosing" runat="server" Width="300px"></asp:TextBox>
                                                   <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="txtclosing"
                                        ValidChars="0123456789.">
                                    </asp:FilteredTextBoxExtender>
                                                </td>
                                            </tr>--%>
                                            <tr>
      <td align="left">
                                                            <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="10pt" ForeColor="Navy"
                                                                Text="Hired Type"></asp:Label>
                                                        </td>
                                                        <td align="left" valign="middle">
                                                            <asp:DropDownList ID="ddlHiredType" runat="server" Width="130px" AutoPostBack="false"
                                                                CssClass="tb6" Height="25px"
                                                                >
                                                            </asp:DropDownList>
                                                        
&nbsp;&nbsp;&nbsp;<asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Size="10pt" ForeColor="Navy"
                                                                Text="Storage Type"></asp:Label></td>
                                                        <td align="left" valign="middle">
                                                            <asp:DropDownList ID="ddlStorageType" runat="server" Width="130px" AutoPostBack="false"
                                                                CssClass="tb6" Height="25px"
                                                                >
                                                         <asp:ListItem Text="--Select--" ></asp:ListItem>
                                                         <asp:ListItem Text="Covered" Value="Covered"></asp:ListItem>
                                                         <asp:ListItem Text="Permanent(CAP)" Value="Permanent(CAP)"></asp:ListItem>
                                                         <asp:ListItem Text="Temporary(CAP)" Value="Temporary(CAP)"></asp:ListItem>
                                                         <asp:ListItem Text="Silo Bag"  Value="SiloBag"></asp:ListItem>
                                                         <asp:ListItem Text="Steel Silo"  Value="SteelSilo"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        
                                                    </tr>
                                                      <tr>
      <td align="left">
                                                            <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Size="10pt" ForeColor="Navy"
                                                                Text="परिसर में कुल गोदामो की भण्डारण क्षमता(In M.T.)"></asp:Label>
                                                        </td>
                                                        <td align="left" valign="middle">
                                                            <asp:TextBox ID="txtPremiseCpt" Width="125px" Height="20px" runat="server" Text="0"></asp:TextBox>
                                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtPremiseCpt"
                                        ValidChars="0123456789.">
                                    </asp:FilteredTextBoxExtender>
                                         &nbsp;&nbsp;&nbsp;<asp:Label ID="Label11" runat="server" Font-Bold="True" Font-Size="10pt" ForeColor="Navy"
                                                                Text="इलेक्ट्रॉनिक वेब्रिज"></asp:Label></td>

                                                        <td align="left" valign="middle">
                                                            <asp:DropDownList ID="ddlWB" runat="server" Width="130px"
                                                                CssClass="tb6" Height="25px">
                                                         <asp:ListItem Text="--Select--" ></asp:ListItem>
                                                         <asp:ListItem Text="Yes" Value="WB"></asp:ListItem>
                                                         <asp:ListItem Text="No" Value="N"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>                                                        

                                                    </tr>
                                                      <tr>
      <td align="left">
                                                            <asp:Label ID="Label8" runat="server" Font-Bold="True" Font-Size="10pt" ForeColor="Navy"
                                                                Text="दिनांक 15/06/2019 की स्थिति मे गोदाम में  भण्डारित स्कंध का Closing Balance(In M.T.)"></asp:Label>
                                                        </td>
                                                        <td align="left" valign="middle">
                                                            <asp:TextBox ID="txtClosing" Width="125px" Height="20px" runat="server" Text="0"></asp:TextBox>
                                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtClosing"
                                        ValidChars="0123456789.">
                                    </asp:FilteredTextBoxExtender>
                                                           &nbsp;&nbsp;&nbsp;<asp:Label ID="Label10" runat="server" Font-Bold="True" Font-Size="10pt" ForeColor="Navy"
                                                                Text="तह्सिल"></asp:Label></td>
                                                        <td>
                                                          <asp:DropDownList ID="DDLTehsil" runat="server" Width="130px"
                                                                CssClass="tb6" Height="25px"  AutoPostBack="True"
                                                                onselectedindexchanged="DDLTehsil_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    
<tr>
      <td align="left">
                                                            <asp:Label ID="Label12" runat="server" Font-Bold="True" Font-Size="10pt" ForeColor="Navy"
                                                                Text="गाँव का नाम "></asp:Label>
                                                        </td>
                                                        <td align="left" valign="middle">
                                                            <asp:DropDownList ID="ddlVillage" runat="server" Width="130px" AutoPostBack="false"
                                                                CssClass="tb6" Height="25px">
                                                            </asp:DropDownList>
                                                        
&nbsp;&nbsp;&nbsp;<asp:Label ID="Label13" runat="server" Font-Bold="True" Font-Size="10pt" ForeColor="Navy"
                                                                Text="विकास खण्ड"></asp:Label></td>
                                                        <td align="left" valign="middle">
                                                            <asp:DropDownList ID="ddlblocknew" runat="server" Width="130px" AutoPostBack="false"
                                                                CssClass="tb6" Height="25px">
                                                            </asp:DropDownList>
                                                        </td>

                                                    </tr> 
                                                <tr>
                                                <td style="height:10px;">
                                                
                                                </td>
                                                </tr>                                                   
                                            <tr>
<td colspan="4" align="center">
<asp:Button ID="btn1" runat="server" Text="Submit" onclick="btn1_Click" onkeyup="Deductionamtcalculate()" Width="100px"  CssClass="BTNBLUE"/>
</td>
</tr>
        </table>
    </td>
   </tr>
<tr>
<td colspan="2">
<p  style="color:Maroon; font-size:13px;">नोट : 1. दिनांक 15/06/2019 की स्थिति मे गोदाम में भण्डारित स्कंध का Closing Balance सही प्रविस्ट करें । दर्ज कि गई भण्डारित स्कंध का Closing Balance <br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;एवम्‌ ऑनलाइन WHMS में भण्डारित स्कंध का Closing Balance का मिलान किया जाना है । <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2. यदि JVS मॉड्यूल में रजिस्ट्रेशन नहि किया है तो तत्काल रजिस्ट्रेशन कराएँ ।<br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;3. यदि तह्सिल,गाँव का नाम,विकास खण्ड मैं यदि नाम प्रदर्शित नहीं हो रहा हे तो नजदीकी तह्सिल,गाँव का नाम,विकास खण्ड का नाम सेलेक्ट करें । <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;4. किसी भी प्रकार की समस्या के लिए उक्त Mail ID : helpdeskmpwlc@gmail.com पर मेल करे ।
</p>
</td>

</tr>
                                            </table>
</div>
</asp:Panel>
    <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="new" BackgroundCssClass="popup" PopupControlID="pnllogin" CancelControlID="x" >
    
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



