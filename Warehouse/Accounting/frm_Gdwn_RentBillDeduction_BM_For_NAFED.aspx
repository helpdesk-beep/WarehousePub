<%@ Page Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="frm_Gdwn_RentBillDeduction_BM_For_NAFED.aspx.cs" Inherits="Accounting_frm_Gdwn_RentBillDeduction_BM_For_NAFED" Title="कटोत्रा" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 <script type="text/javascript" src="<%= ResolveUrl("~/NEW_CSS/js/jquery-1.12.4.js") %>"></script>
    <style type="text/css">
        .style1 {
            width: 346px;
        }

        .auto-style1 {
            width: 539px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<fieldset  style=" width:1000px; border:2px solid navy; background-color:white;">
<center>
<div>
<table width="1000px">
<tr id="msg">
<td><asp:Label ID="lblmsg" Text="" ForeColor="red" Font-Bold="true" runat="server"></asp:Label></td>
</tr>
<tr style="background-color: #0bb6e6; height: 25px">
 <td align="center">
            <asp:Label ID="lblheading" runat="server" Font-Bold="True" Font-Size="15pt" ForeColor="WhiteSmoke"
                                        Text="Godown Rent Bill Deductions"></asp:Label></td>
</tr>
<tr>
                                                        <td colspan="1" style="height: 5px">
                                                        </td>
                                                        </tr>
                                                        
    <tr>
    <td style="color:Red;font-size:12px;"  align="left">&nbsp;Note :<br />
        <p color:#008080;" style="color:Red;font-size:12px;">
    &nbsp;1) Godown Rent Bill No. को Storage Charges Bill No. से लिंक करें ।   </p>
        <p color:#008080;" style="color:Red;font-size:12px;">
    &nbsp;2) यदि कोई कटोत्रा नहि हे तो कटोत्रा मद का विवरण मे अन्य select करे, कटोत्रा का कारण एवम्‌ रिमार्क मे ( कोई कतोत्र नहीं ) टाइप करें एवम राशि मे सुन्य (0) दर्ज करे फिर  calculate total बटन पे <br />&nbsp;&nbsp;&nbsp;&nbsp; क्लिक्क करे फिर Submit करे ।. </p>    
    </td>
    </tr> 
                                                      
                                                 
                <tr id="tr1" visible="true" runat="server">
<td>
<fieldset style="width: 980px; border: 1px solid navy;">
                <center>
                       <div id="div2" style=" height:150px;">
                       <table id="Table1" cellpadding="0" border="0px" cellspacing="0" style="width: 100%">
                       <tr>
                       <td style="width:200px;">
<asp:Label Font-Size="8pt" Font-Bold="true" ForeColor="navy" ID="Label1" runat="server" Text="Godown"></asp:Label>
</td>
<td class="style1">
<asp:DropDownList ID="ddlgodown" runat="server" AutoPostBack="True" TabIndex="1" 
Height="25px" Width="208px" Font-Size="10pt" onselectedindexchanged="ddlgodown_SelectedIndexChanged"
>
</asp:DropDownList></td>
<td style="width:200px;">
<asp:Label ID="lblcommodity" runat="server" Text="Commodity Name" Font-Bold="True" Font-Size="8pt" 
                            ForeColor="Navy"></asp:Label></td>
<td><asp:DropDownList ID="ddlcomodity" runat="server" Width="208px" Height="25px" 
AutoPostBack="True" onselectedindexchanged="ddlcomodity_SelectedIndexChanged" 
>
</asp:DropDownList>
</td>
</tr>
<tr>
                    <td colspan="1" style="height: 5px">
                    </td>
                    </tr>


                 <tr>
<td> 
            <asp:Label ID="lblCropYear" runat="server" Text="Crop Year" Font-Bold="True" Font-Size="8pt" 
                                                                ForeColor="Navy"></asp:Label></td>
        <td><asp:DropDownList ID="ddlCropYear" runat="server" AutoPostBack="True" 
                TabIndex="1" Height="25px" Width="200px" Font-Size="10pt" 
              >
            </asp:DropDownList>
            </td>
            <td>
            <asp:Label ID="lblmonth" runat="server" Text="Month" Font-Bold="True" Font-Size="8pt" 
                                                                ForeColor="Navy"></asp:Label></td>
        <td>
            <asp:DropDownList ID="ddlFyear" runat="server" AutoPostBack="false" Visible="true"
                TabIndex="1" Height="25px" Width="90px" Font-Size="10pt" Enabled="true"
              >
            </asp:DropDownList>
            <asp:DropDownList ID="ddlmonth" runat="server" AutoPostBack="True" 
                TabIndex="1" Height="25px" Width="115px" Font-Size="10pt" onselectedindexchanged="ddlmonth_SelectedIndexChanged" 
              >
            </asp:DropDownList>
            </td>
</tr>

                           <tr>
                    <td colspan="1" style="height: 5px">
                    </td>
                    </tr>
    <tbody id="billdetails" runat="server" visible="false">
    <tr>
                       <td>
<asp:Label Font-Size="8pt" Font-Bold="true" ForeColor="navy" ID="Label3" runat="server" Text="Rent Bill No."></asp:Label>
</td>
<td class="style1">
<%--<asp:DropDownList ID="ddlBill" runat="server" AutoPostBack="True" TabIndex="1" 
Height="25px" Width="208px" Font-Size="10pt" onselectedindexchanged="ddlBill_SelectedIndexChanged"
>
</asp:DropDownList>--%>
    <asp:Label runat="server" ID="lblrentbillno" Width="203px" ReadOnly="True"></asp:Label>
</td>
                      <td>
<asp:Label Font-Size="8pt" Font-Bold="true" ForeColor="navy" ID="Label5" runat="server" Text="Month"></asp:Label>
</td>
<td class="style1">

<asp:Label runat="server" ID="lblMonth2" Width="203px" ReadOnly="True"></asp:Label>
</td>

</tr>
<tr>
                    <td colspan="1" style="height: 5px">
                    </td>
                    </tr>
<tr>
                       <td>
<asp:Label Font-Size="8pt" Font-Bold="true" ForeColor="navy" ID="Label7" runat="server" Text="Rate Per Month"></asp:Label>
</td>
<td class="style1">
<asp:Label runat="server" ID="lblRPM" Width="203px" ReadOnly="True"></asp:Label>
</td>
                      <td>
<asp:Label Font-Size="8pt" Font-Bold="true" ForeColor="navy" ID="Label8" runat="server" Text="Rent Bill Amount"></asp:Label>
</td>
<td class="style1">

<asp:Label runat="server" ID="txtBilAmt" Width="203px" ReadOnly="True"></asp:Label>
</td>

</tr>

<tr>
 <td style="height:36px;">
<asp:Label Font-Size="8pt" Font-Bold="true" ForeColor="navy" ID="Label2" runat="server" Text="Storage Charges Bill No."></asp:Label>
</td>
<td class="style1">
<%--<asp:DropDownList ID="ddlActualBillNo" runat="server" AutoPostBack="True" TabIndex="1" 
Height="25px" Width="208px" Font-Size="10pt" 
        onselectedindexchanged="ddlActualBillNo_SelectedIndexChanged" >
</asp:DropDownList>--%>
    <asp:Label runat="server" ID="lblActualBillNo" Width="203px" ReadOnly="True"></asp:Label>
</td>
                      <td>
<asp:Label Font-Size="8pt" Font-Bold="true" ForeColor="navy" ID="Label4" runat="server" Text="Storage Charges Amount"></asp:Label>
</td>
<td class="style1">

<asp:Label runat="server" ID="txtActAmt" Width="203px" ReadOnly="True"></asp:Label>
</td>

</tr>
</tbody>
<tr>
                    <td colspan="1" style="height: 5px">
                    </td>
                    </tr>
<%--<tr>
                       <td>
<asp:Label Font-Size="8pt" Font-Bold="true" ForeColor="navy" ID="Label24" runat="server" Text="Stock in Godown"></asp:Label>
</td>
<td class="style1">
<asp:TextBox runat="server" ID="txtGodownBalance" Width="200px"></asp:TextBox>
</td>
                      <td>
                   
<asp:Label Font-Size="8pt" Font-Bold="true" ForeColor="navy" ID="lbl29" runat="server" Text="Closing Balance"></asp:Label>
</td>
<td class="style1">

<asp:TextBox runat="server" ID="txtBillClosingBalance" Width="200px"></asp:TextBox>

</td>

</tr>--%>


                       
                       </table>
                       </div>
                       </center>
                       </fieldset>
                       </td>
                       </tr>

                                                     <tr>
                                                        <td colspan="1" style="height: 5px">
                                                        </td>
                                                    </tr>

<tr id="trJVSGodownRent" visible="true" runat="server">
            <td>
            <fieldset style="width: 980px; border: 1px solid navy;">

            <table width="100%">
            <tr>
            <td colspan="4" id="GVGodowns" runat="server" visible="true" style="text-align:center; width:100%; " align="center">

            <%--<asp:GridView ID="gvGodown" runat="server" AutoGenerateColumns="False" 
                                        ShowFooter="true" Width="90%"
                                     EnableModelValidation="True" BackColor="White" BorderColor="#E7E7FF" 
                                        BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Horizontal" 
                                        AutoGenerateDeleteButton="false" OnRowDataBound="gvGodown_OnRowDataBound">
                                        <AlternatingRowStyle BackColor="#F7F7F7" />
                                        <Columns>
                                            <asp:BoundField DataField="Tid" HeaderText="S.No"  ItemStyle-Width="30px"/>

                                            <asp:TemplateField HeaderText="कतोत्र मद का विवरण"  ItemStyle-Width="100px">
                                                <ItemTemplate>
                                                <asp:DropDownList ID="ddlD_Vivran" runat="server"
                                                    Width="200" Height="27px">
                                                </asp:DropDownList>                                     
                                             </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="कतोत्र का कारण"  ItemStyle-Width="200px">
                                                <ItemTemplate>
                                                 <asp:TextBox ID="txtK_Karand" runat="server" Width="200px" Text='<%# Eval("K_Karand") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="कतित्रा राशि"  ItemStyle-Width="100px">
                                                <ItemTemplate>
                                                 <asp:TextBox ID="txtK_Rashi" runat="server" Width="100px" Text='<%# Eval("K_Rashi") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>                                
                                            <asp:TemplateField HeaderText="रिमार्क"  ItemStyle-Width="200px">
                                                <ItemTemplate>
                                                 <asp:TextBox ID="txtK_Remark" runat="server" Width="200px" Text='<%# Eval("K_Remark") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                              
                         <asp:TemplateField HeaderText=""  ItemStyle-Width="100px">                                              
                         <FooterStyle HorizontalAlign="Right" />
                        <FooterTemplate>
                         <asp:Button ID="ButtonAdd" runat="server" Text="Add New Row" Width="100px" 
                                onclick="ButtonAdd_Click" />
                        </FooterTemplate>
                                            </asp:TemplateField>
                                                                                                      
                                        </Columns>
                                       
                                        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                                       
                                        
                                    </asp:GridView>--%>
                                    
                                    
                                    
                                    
            <asp:GridView ID="gvGodown" runat="server" AutoGenerateColumns="False" 
                                        ShowFooter="true" Width="100%"
                                     EnableModelValidation="True" CellPadding="3" GridLines="Horizontal" 
                                        AutoGenerateDeleteButton="false" OnRowDataBound="gvGodown_OnRowDataBound">
                                        
                                        <Columns>
                                            <asp:BoundField DataField="Tid" HeaderText="क्र."  ItemStyle-Width="30px"/>

                                            <asp:TemplateField HeaderText="कटोत्रा मद का विवरण"  ItemStyle-Width="300px">
                                                <ItemTemplate>
                                                        <asp:DropDownList ID="ddl_RDetuction" runat="server" Text='<%# Eval("RDetuction") %>'
                                                             Width="350px" Height="30px">
                                                             <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                             <asp:ListItem Value="1" Text="डनेज पोलीथीन/डनेज शीट"></asp:ListItem>
                                                             <asp:ListItem Value="2" Text="इलेक्ट्रोनिक तौल काटा(200 कि॰ग्रा॰ तक) ISI Mark"></asp:ListItem>
                                                             <asp:ListItem Value="3" Text="लकड़ी की फड़ी"></asp:ListItem>
                                                             <asp:ListItem Value="4" Text="एनलायसिस फिट सेट"></asp:ListItem>
                                                             <asp:ListItem Value="5" Text="फ्यूमीगेशन कवर"></asp:ListItem>
                                                             <asp:ListItem Value="6" Text="Fire Extinguisher ISI Mark"></asp:ListItem>
                                                             <asp:ListItem Value="7" Text="Fire Buckets ISI Mark"></asp:ListItem>
                                                             <asp:ListItem Value="8" Text="गोदाम की सुरक्षा व्यवस्था हेतु चौकीदरी हेतु सुरक्षा कर्मी उपलब्ध कराना"></asp:ListItem>
                                                             <asp:ListItem Value="9" Text="पावर स्प्रे पंप अथवा फुट स्प्रेयर पंप"></asp:ListItem>
                                                             <asp:ListItem Value="10" Text="डिजिटल नमीमापक यंत्र(मल्टी कमोडिटी) ISI Mark"></asp:ListItem>
                                                             <asp:ListItem Value="11" Text="कीटोपचार/सफाई हेतु उपलब्ध कराये जाने वाले श्रमिक"></asp:ListItem>
                                                             <asp:ListItem Value="12" Text="बिजली पानी एवं शोचालय सूविधा"></asp:ListItem>
                                                             <asp:ListItem Value="13" Text="Insectiside Cost(Celphos) @ 500/Rs./Kg + 18% GST"></asp:ListItem>
                                                             <asp:ListItem Value="14" Text="125% से अधिक भंडारित स्कंध का दणिक कटोत्रा"></asp:ListItem>
                                                             <asp:ListItem Value="100" Text="अन्य"></asp:ListItem>
                                                        </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>  
                                            
                                            <asp:TemplateField HeaderText="कटोत्रा का कारण"  ItemStyle-Width="150px">
                                                <ItemTemplate>
                                                 <asp:TextBox ID="txtK_Vivran" Wrap="true"  runat="server" Width="150px" Height="40px" Text='<%# Eval("K_Vivran") %>' TextMode="MultiLine"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="कटोत्रा राशि (रू)"  ItemStyle-Width="100px">
                                                <ItemTemplate>
                                                 <asp:TextBox ID="txtK_Rashi" runat="server" Width="100px" Height="25px" Text='<%# Eval("K_Rashi") %>' ></asp:TextBox>                                   
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="रिमार्क"  ItemStyle-Width="150px">
                                                <ItemTemplate>
                                                 <asp:TextBox ID="txtK_Remark" runat="server" Width="150px" Height="40px" Text='<%# Eval("K_Remark") %>' TextMode="MultiLine"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                                                                                         
                         <asp:TemplateField HeaderText=""  ItemStyle-Width="100px">                                              
                         <FooterStyle HorizontalAlign="Right" />
                        <FooterTemplate>
                         <asp:Button ID="ButtonAdd" runat="server" Text="Add New Row" Width="100px" 
                                onclick="ButtonAdd_Click" />
                        </FooterTemplate>
                                            </asp:TemplateField>
                                                                                                      
                                        </Columns>
                                       
                                        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" Height="30px" Font-Size="14px" />
            <%--                            <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                                        <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />--%>
                                        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                    </asp:GridView> 
                                </td>
            </tr>


               
 <%--<tr>
    <td style="color:Red;font-size:12px;"  align="left" colspan="4">&nbsp;Important Note :<br />
        <p color:#008080;" style="color:Red;font-size:12px;">
    &nbsp;1)प्रमुख सचिव म.प्र. शाशन खाद्य नागरिक आपूर्ति एवं उपभोगता संरक्षण विभाग भोपाल द्वारा जारी परिपत्र  क्रमांक 1628 दिनांक 23/09/2022  की कंडिका 11 ,12 के तहत  कटोत्रा</p><br />
        <p color:#008080;" style="color:Red;font-size:12px;">
    &nbsp;2) गोदाम से स्कंध की निकासी में गोदाम संचालक द्वारा अवरोध (ताला नहीं खोलना, रास्ता अवरुद्ध कर देना, निकासी से बचने  के लिए जानबूझ कर कीटनाशकों का छिड़काव कर देना आदि) पैदा करने की वजह से स्कंध की निकासी नहीं होने की स्थिति में निकासी हेतु निर्धारित दिनाँक से गोदाम में भंडारित सम्पूर्ण स्कंध का किराया देय नहीं होगा और ऐसे गोदाम को क्षेत्रीय प्रबंधक द्वारा अनुबंध की कंडिकाओ के उल्लंघन के परिणामस्वरूप अनिवार्य रूप से ब्लैक लिस्ट किया जावेगा ।   </p>
        <p color:#008080;" style="color:Red;font-size:12px;">
    &nbsp;2) यह गोदाम संचालक की जिम्मेदारी होगी कि वह स्कंध की निकासी हेतु निर्धारित दिनाँक को स्कंध गुणवत्ता पूर्ण स्थिति में रखे। यदि कीटग्रस्तता के कारण किसी गोदाम विशेष से स्कंध की निकासी संभव नहीं होती है तो ऐसी स्थिति में भी निर्धारित दिनाँक से गोदाम में भंडारित सम्पूर्ण स्कंध का किराया देय नहीं होगा, यद्यपि गोदाम संचालक को स्कंध को कीटग्रस्थ घोषित किए जाने के संबंध में DM,MPSCSC/BM MPWLC के समक्ष तत्काल आपत्ति प्रस्तुत करने का अधिकार होगा। इस संबंध में की गई आपत्ति का निराकरण DM,MPSCSC/BM MPWLC को मौका निरीक्षण कर 72 घंटे में करना अनिवार्य होगा।      <br />&nbsp;&nbsp;&nbsp;&nbsp; क्लिक्क करे फिर Submit करे ।. </p>    
   <p color:#008080;" style="color:Red;font-size:20px;">
    &nbsp;2) यदी कोई भी कटोत्रा नहीं है तो '0' की प्रविष्टि करें | </p>
        </td>
    </tr>--%>
                               <tr>
                       <td class="auto-style1">
<%--<asp:Label Font-Size="8pt" Font-Bold="true" ForeColor="navy" ID="Label6" runat="server" Text="प्रमुख सचिव म.प्र. शाशन खाद्य नागरिक आपूर्ति एवं उपभोगता संरक्षण विभाग भोपाल द्वारा जारी परिपत्र  क्रमांक <a href="https://mpwarehousing.mp.gov.in/Upload/Letter%201628%20Date%2023-09-2022_220923_170348.pdf"> 1628 दिनांक 23/09/2022 </a> की कंडिका 11 के तहत  कटोत्रा"></asp:Label>--%>
                           <span style="font-size:8pt; color:navy;font-weight:bold">1  प्रमुख सचिव म.प्र. शाशन खाद्य नागरिक आपूर्ति एवं उपभोगता संरक्षण विभाग भोपाल द्वारा जारी परिपत्र  क्रमांक <a href="https://mpwarehousing.mp.gov.in/Upload/Letter%201628%20Date%2023-09-2022_220923_170348.pdf" target="_blank" style="color:red;"> 1628 दिनांक 23/09/2022 </a> की कंडिका 11 के तहत  कटोत्रा"</span>
</td>
<td class="style1">

    <asp:TextBox runat="server" ID="txtlocknotopen" Width="203px" onkeypress="return isNumberKey(event)"></asp:TextBox>
    <asp:RequiredFieldValidator ID="reqName" ControlToValidate="txtlocknotopen" ValidationGroup="LoginFrame"
 runat="server" ErrorMessage="यदी कोई भी कटोत्रा नहीं है तो '0' की प्रविष्टि करें" ForeColor="Red"></asp:RequiredFieldValidator>
</td>
</tr>
                <tr>
                    <td>
<%--<asp:Label Font-Size="8pt" Font-Bold="true" ForeColor="navy" ID="Label10" runat="server" Text="प्रमुख सचिव म.प्र. शाशन खाद्य नागरिक आपूर्ति एवं उपभोगता संरक्षण विभाग भोपाल द्वारा जारी परिपत्र  क्रमांक 1628 दिनांक 23/09/2022  की कंडिका 12 के तहत  कटोत्रा"></asp:Label>--%>
                        <span style="font-size:8pt; color:navy;font-weight:bold">2 प्रमुख सचिव म.प्र. शाशन खाद्य नागरिक आपूर्ति एवं उपभोगता संरक्षण विभाग भोपाल द्वारा जारी परिपत्र  क्रमांक <a href="https://mpwarehousing.mp.gov.in/Upload/Letter%201628%20Date%2023-09-2022_220923_170348.pdf" target="_blank" style="color:red;"> 1628 दिनांक 23/09/2022 </a> की कंडिका 12 के तहत  कटोत्रा</span>
</td>
<td class="style1">

<asp:TextBox runat="server" ID="txtRoadBlock" Width="203px" onkeypress="return isNumberKey(event)"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtRoadBlock" ValidationGroup="LoginFrame"
 runat="server" ErrorMessage="यदी कोई भी कटोत्रा नहीं है तो '0' की प्रविष्टि करें" ForeColor="Red"></asp:RequiredFieldValidator>
</td>

</tr>
                 <tr>
                    <td align="center" colspan="4" style="height:70px;">
                        <asp:Button ID="Button2" Height="35px" runat="server" Text="Calculate Total" 
                            onclick="Button2_Click" ValidationGroup="LoginFrame" />
                        &nbsp;&nbsp;
                        &nbsp; <span style="font-size:14px;">Rs.</span> &nbsp;<asp:TextBox ID="txt_GrandTotal" runat="server" Width="100px" 
                            Height="25px" ReadOnly="True" ></asp:TextBox></td>
                </tr>
<%--<tr>
                       <td>
<asp:Label Font-Size="8pt" Font-Bold="true" ForeColor="navy" ID="Label9" runat="server" Text="निकासी से बचने के लिये जानबूझकर कीटनाशकों का छिड़काव कर देना आदी"></asp:Label>
</td>
<td class="style1">

    <asp:TextBox runat="server" ID="txtUnwantedFumigation" Width="203px" onkeypress="return isNumberKey(event)"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtUnwantedFumigation" ValidationGroup="LoginFrame"
 runat="server" ErrorMessage="यदी कोई भी कटोत्रा नहीं है तो '0' की प्रविष्टि करें" ForeColor="Red"></asp:RequiredFieldValidator>
</td>
                     

</tr>--%>
                <tr>
                    <td align="center" colspan="4">
                        <asp:Button ID="btnSumbmitRent" runat="server" Text="Submit" CssClass="BTNBLUE" Width="100px" Enabled="true" onclick="btnSumbmitRent_Click" ValidationGroup="LoginFrame"/> &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="brnCancel" runat="server" Text="Close" Width="100px"  CssClass="BTNBLUE"/>&nbsp;&nbsp;&nbsp;&nbsp;
                        
                        <asp:Button ID="Button1" runat="server" Text="New" Width="100px" 
                            CssClass="BTNBLUE" onclick="Button1_Click"/>
                    </td>
                </tr>
            <tr id="trRentBill" visible="false" runat="server">
            <td colspan="4">
                &nbsp;</td>
            </tr>
            </table>
                                                                        </fieldset>
                                                                        </td>
            </tr>
                </table>
            </div>
            </center>
            </fieldset>
    <script type="text/javascript">

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode != 46 && charCode > 31
                && (charCode < 48 || charCode > 57)) {
                alert("This field will not accept the alphabet, Please Enter Only number");
                return false;
            }
            return true;
        }
        //
    </script>
</asp:Content>

