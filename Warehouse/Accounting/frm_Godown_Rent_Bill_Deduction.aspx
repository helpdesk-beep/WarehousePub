    <%@ Page Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="frm_Godown_Rent_Bill_Deduction.aspx.cs" Inherits="Accounting_frm_Godown_Rent_Bill_Deduction" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1/jquery.min.js"></script>
    <style type="text/css">
   
        .style1
        {
            width: 346px;
        }
   
    </style>
   <script type="text/javascript">
       function calc() {

           var remember = document.getElementById('<%= chkDP.ClientID %>').checked;
//           alert(remember);
           if (remember==true) {
//              alert("checked");
              var a = document.getElementById('<%= lblDP.ClientID %>').value;
               var b = a * 5;
//               alert(b);
               document.getElementById('<%= txtDP.ClientID %>').value = b;
               document.getElementById('<%= txtTotalDeduction.ClientID %>').value = b;
               
           } else {
           document.getElementById('<%= txtDP.ClientID %>').value = 0;
           
           }
        
       }
       function calc2() {

           var remember = document.getElementById('<%= CheckBox2.ClientID %>').checked;
           //           alert(remember);
           if (remember == true) {
               //              alert("checked");
               var a = document.getElementById('<%= TextBox24.ClientID %>').value;
               var b = a * 0.25;
               //               alert(b);
               document.getElementById('<%= TextBox1.ClientID %>').value = b;
               var T = document.getElementById('<%= txtTotalDeduction.ClientID %>').value;
               var X = parseFloat(b) + parseFloat(T);
               document.getElementById('<%= txtTotalDeduction.ClientID %>').value = X;

           } else {
               document.getElementById('<%= txtDP.ClientID %>').value = 0;

           }

       }
       function calc3() {

           var remember = document.getElementById('<%= CheckBox1.ClientID %>').checked;
           //           alert(remember);
           if (remember == true) {
               //              alert("checked");
               var a = document.getElementById('<%= TextBox25.ClientID %>').value;
               var b = a * 0.25;
               //               alert(b);
               document.getElementById('<%= TextBox2.ClientID %>').value = b;
               var T = document.getElementById('<%= txtTotalDeduction.ClientID %>').value;
               var X = parseFloat(b) + parseFloat(T);
               document.getElementById('<%= txtTotalDeduction.ClientID %>').value = X;

           } else {
           document.getElementById('<%= TextBox2.ClientID %>').value = 0;

           }

       }
       function calc4() {

           var remember = document.getElementById('<%= CheckBox3.ClientID %>').checked;
           //           alert(remember);
           if (remember == true) {
               //              alert("checked");
               var a = document.getElementById('<%= TextBox26.ClientID %>').value;
               var b = a * 0.10;
               //               alert(b);
               document.getElementById('<%= TextBox3.ClientID %>').value = b;
               var T = document.getElementById('<%= txtTotalDeduction.ClientID %>').value;
               var X = parseFloat(b) + parseFloat(T);
               document.getElementById('<%= txtTotalDeduction.ClientID %>').value = X;

           } else {
           document.getElementById('<%= TextBox3.ClientID %>').value = 0;

           }

       }
       function calc5() {

           var remember = document.getElementById('<%= CheckBox5.ClientID %>').checked;
           //           alert(remember);
           if (remember == true) {
               //              alert("checked");
               var a = document.getElementById('<%= TextBox23.ClientID %>').value;
               var b = a * 3;
               //               alert(b);
               document.getElementById('<%= TextBox5.ClientID %>').value = b;
               var T = document.getElementById('<%= txtTotalDeduction.ClientID %>').value;
               var X = parseFloat(b) + parseFloat(T);
               document.getElementById('<%= txtTotalDeduction.ClientID %>').value = X;

           } else {
           document.getElementById('<%= TextBox5.ClientID %>').value = 0;

           }

       }
       function calc6() {

           var remember = document.getElementById('<%= CheckBox6.ClientID %>').checked;
           //           alert(remember);
           if (remember == true) {
               //              alert("checked");
               var a = document.getElementById('<%= TextBox27.ClientID %>').value;
               var b = a * 0.50;
               //               alert(b);
               document.getElementById('<%= TextBox6.ClientID %>').value = b;
               var T = document.getElementById('<%= txtTotalDeduction.ClientID %>').value;
               var X = parseFloat(b) + parseFloat(T);
               document.getElementById('<%= txtTotalDeduction.ClientID %>').value = X;

           } else {
               document.getElementById('<%= TextBox6.ClientID %>').value = 0;

           }

       }
       function calc7() {

           var remember = document.getElementById('<%= CheckBox7.ClientID %>').checked;
           //           alert(remember);
           if (remember == true) {
               //              alert("checked");
               var a = document.getElementById('<%= TextBox28.ClientID %>').value;
               var b = a * 0.50;
               //               alert(b);
               document.getElementById('<%= TextBox7.ClientID %>').value = b;
               var T = document.getElementById('<%= txtTotalDeduction.ClientID %>').value;
               var X = parseFloat(b) + parseFloat(T);
               document.getElementById('<%= txtTotalDeduction.ClientID %>').value = X;

           } else {
               document.getElementById('<%= TextBox7.ClientID %>').value = 0;

           }

       }
       function calc8() {

           var remember = document.getElementById('<%= CheckBox8.ClientID %>').checked;
           //           alert(remember);
           if (remember == true) {
               //              alert("checked");
               var a = document.getElementById('<%= TextBox29.ClientID %>').value;
               var b = a * 7700;
               //               alert(b);
               document.getElementById('<%= TextBox8.ClientID %>').value = b;
               var T = document.getElementById('<%= txtTotalDeduction.ClientID %>').value;
               var X = parseFloat(b) + parseFloat(T);
               document.getElementById('<%= txtTotalDeduction.ClientID %>').value = X;

           } else {
               document.getElementById('<%= TextBox8.ClientID %>').value = 0;

           }

       }
       function calc9() {

           var remember = document.getElementById('<%= CheckBox9.ClientID %>').checked;
           //           alert(remember);
           if (remember == true) {
               //              alert("checked");
               var a = document.getElementById('<%= TextBox30.ClientID %>').value;
               var b = a * 2;
               //               alert(b);
               document.getElementById('<%= TextBox9.ClientID %>').value = b;
               var T = document.getElementById('<%= txtTotalDeduction.ClientID %>').value;
               var X = parseFloat(b) + parseFloat(T);
               document.getElementById('<%= txtTotalDeduction.ClientID %>').value = X;

           } else {
               document.getElementById('<%= TextBox9.ClientID %>').value = 0;

           }

       }
       function calc10() {

           var remember = document.getElementById('<%= CheckBox10.ClientID %>').checked;
           //           alert(remember);
           if (remember == true) {
               //              alert("checked");
               var a = document.getElementById('<%= TextBox31.ClientID %>').value;
               var b = a * 2;
               //               alert(b);
               document.getElementById('<%= TextBox10.ClientID %>').value = b;
               var T = document.getElementById('<%= txtTotalDeduction.ClientID %>').value;
               var X = parseFloat(b) + parseFloat(T);
               document.getElementById('<%= txtTotalDeduction.ClientID %>').value = X;

           } else {
               document.getElementById('<%= TextBox10.ClientID %>').value = 0;

           }

       }
       function calc11() {

           var remember = document.getElementById('<%= CheckBox11.ClientID %>').checked;
           //           alert(remember);
           if (remember == true) {
               //              alert("checked");
               var a = document.getElementById('<%= TextBox32.ClientID %>').value;
               var b = a * 7700;
               //               alert(b);
               document.getElementById('<%= TextBox11.ClientID %>').value = b;
               var T = document.getElementById('<%= txtTotalDeduction.ClientID %>').value;
               var X = parseFloat(b) + parseFloat(T);
               document.getElementById('<%= txtTotalDeduction.ClientID %>').value = X;

           } else {
               document.getElementById('<%= TextBox11.ClientID %>').value = 0;

           }

       }
       function calc12() {

           var remember = document.getElementById('<%= CheckBox4.ClientID %>').checked;
           //           alert(remember);
           if (remember == true) {
               //              alert("checked");
               var a = document.getElementById('<%= TextBox366.ClientID %>').value;
               var b = a * 590;
               //               alert(b);
               document.getElementById('<%= TextBox37.ClientID %>').value = b;
               var T = document.getElementById('<%= txtTotalDeduction.ClientID %>').value;
               var X = parseFloat(b) + parseFloat(T);
               document.getElementById('<%= txtTotalDeduction.ClientID %>').value = X;

           } else {
           document.getElementById('<%= TextBox37.ClientID %>').value = 0;

           }

       }


   </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<fieldset  style=" width:1000px; border:2px solid navy">
<center>
<div>
<table width="1000px">
<tr id="msg">
<td><asp:Label ID="lblmsg" Text="" ForeColor="red" Font-Bold="true" runat="server"></asp:Label></td>
</tr>
<tr style="background-color: #0bb6e6; height: 25px">
 <td align="center">
            <asp:Label ID="lblheading" runat="server" Font-Bold="True" Font-Size="15pt" ForeColor="WhiteSmoke"
                                        Text="Godown Bill Deductions"></asp:Label></td>
</tr>
<tr>
                                                        <td colspan="1" style="height: 5px">
                                                        </td>
                                                        </tr>
                                                 
                                                    <tr id="tr1" visible="true" runat="server">
<td>
<fieldset style="width: 980px; border: 1px solid navy;">
                                                    <center>
                                                           <div id="div2" style="overflow: scroll; height:150px; overflow-x: hidden">
                                                           <table id="Table1" cellpadding="0" border="0px" cellspacing="0" style="width: 100%">
                                                           <tr>
                                                           <td>
           <asp:Label Font-Size="8pt" Font-Bold="true" ForeColor="navy" ID="Label1" runat="server" Text="Godown"></asp:Label>
            </td>
            <td class="style1">
            <asp:DropDownList ID="ddlgodown" runat="server" AutoPostBack="True" TabIndex="1" 
                    Height="25px" Width="200px" Font-Size="10pt" onselectedindexchanged="ddlgodown_SelectedIndexChanged"
               >
            </asp:DropDownList></td>
            <td>
            <asp:Label ID="lblcommodity" runat="server" Text="Commodity Name" Font-Bold="True" Font-Size="8pt" 
                                                                ForeColor="Navy"></asp:Label></td>
        <td><asp:DropDownList ID="ddlcomodity" runat="server" Width="200px" Height="25px" 
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
           <asp:Label Font-Size="8pt" Font-Bold="true" ForeColor="navy" ID="Label3" runat="server" Text="Bill No."></asp:Label>
            </td>
            <td class="style1">
            <asp:DropDownList ID="ddlBill" runat="server" AutoPostBack="True" TabIndex="1" 
                    Height="25px" Width="200px" Font-Size="10pt" onselectedindexchanged="ddlBill_SelectedIndexChanged"
               >
            </asp:DropDownList></td>
                                                          <td>
           <asp:Label Font-Size="8pt" Font-Bold="true" ForeColor="navy" ID="Label5" runat="server" Text="Month"></asp:Label>
            </td>
            <td class="style1">
          
                <asp:TextBox runat="server" ID="lblMonth" Width="200px"></asp:TextBox>
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
            <asp:TextBox runat="server" ID="lblRPM" Width="200px"></asp:TextBox>
          </td>
                                                          <td>
           <asp:Label Font-Size="8pt" Font-Bold="true" ForeColor="navy" ID="Label8" runat="server" Text="Bill Amount"></asp:Label>
            </td>
            <td class="style1">
          
               <asp:TextBox runat="server" ID="txtBilAmt" Width="200px"></asp:TextBox>
            </td>
       
           </tr>
            <tr>
                                                        <td colspan="1" style="height: 5px">
                                                        </td>
                                                        </tr>
           <tr>
                                                           <td>
           <asp:Label Font-Size="8pt" Font-Bold="true" ForeColor="navy" ID="Label24" runat="server" Text="Stock in Godown"></asp:Label>
            </td>
            <td class="style1">
            <asp:TextBox runat="server" ID="txtGodownBalance" Width="200px" Text="2200"></asp:TextBox>
          </td>
                                                          <td>
                                                       
           <asp:Label Font-Size="8pt" Font-Bold="true" ForeColor="navy" ID="lbl29" runat="server" Text="Closing Balance"></asp:Label>
            </td>
            <td class="style1">
           
              <asp:TextBox runat="server" ID="txtBillClosingBalance" Width="200px" Text="1500"></asp:TextBox>
               
            </td>
       
           </tr>
            <tr>
                                                        <td colspan="1" style="height: 5px">
                                                        </td>
                                                        </tr>
           <tr>
                                                           <td>
           <asp:Label Font-Size="8pt" Font-Bold="true" ForeColor="navy" ID="Label6" runat="server" Text="TDS Deduction Amount"></asp:Label>
            </td>
            <td class="style1">
            <asp:TextBox runat="server" ID="txtTDSAmt" Width="200px" Text="0"></asp:TextBox>
          </td>
                                                          <td>
                                                       
           <asp:Label Font-Size="8pt" Font-Bold="true" ForeColor="navy" ID="Label10" runat="server" Text="1% Gain Deduction Amount "></asp:Label>
            </td>
            <td class="style1">
           
              <asp:TextBox runat="server" ID="txtGainDeductAmt" Width="200px" Text="0"></asp:TextBox>
               
            </td>
       
           </tr>
                                                           
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
                                                    <center>
                                                           <div id="div1" style="overflow: scroll; height:550px; overflow-x: hidden">
                                                           <table id="tbl1" cellpadding="0" border="0px" cellspacing="0" style="width: 100%">
                                                           <tr>
<th>
           क़.
            </th>
            <th>
           संसाधन विवरण
            </th>
            <th align="left">
            उपलब्ध संसाधन मात्रा
            </th>
            <th align="left">
           कटौत्रे हेतु मात्रा
            </th>
            <th align="left">
            दर
           </th>
            <th align="left">
            कटौत्रे हेतु चुने
            </th>
          <th align="left"> कटौत्रा राशी

            </th>
</tr>
                                                           <tr>
                                                        <td colspan="7" style="height: 5px">
                                                        <hr />
                                                        </td>
                                                    </tr>
                                                           <tr>
<td>
           <asp:Label Font-Size="8pt" Font-Bold="true" ForeColor="navy" ID="lblgodown" runat="server" Text="1."></asp:Label>
            </td>
            <td>
            <p>डनेज पोलीथीन/डनेज शीट</p>
            </td>
            <td>
            <asp:TextBox runat="server" ID="txt1" Text="" Width="80px"></asp:TextBox>
            </td>
            <td>
             <asp:TextBox runat="server" ID="lblDP" Text="" Width="80px"></asp:TextBox>
            <%--<asp:Label ID="lblDP" runat="server" Text="2000"></asp:Label>--%>
            </td>
            <td>
            <asp:Label ID="lblDPP" runat="server" Text="5/-"></asp:Label>
            </td>
             <td>
           <asp:CheckBox ID="chkDP" runat="server" onclick="calc()"/>
           <%-- <asp:CheckBox ID="chkDP" runat="server"/>--%>
            </td>
        <td><asp:TextBox runat="server" ID="txtDP" Width="80px" Text="0"></asp:TextBox>
            </td>
</tr>
 <tr>
                                                        <td colspan="6" class="style1">
                                                        </td>
                                                    </tr>
<tr>
<td> 
            2.</td>
        <td><p>इलेक्ट्रोनिक तौल काटा(200 कि॰ग्रा॰ तक) ISI Mark</p>
            </td>
            <td>
            <asp:TextBox runat="server" ID="TextBox4" Text="" Width="80px"></asp:TextBox>
            </td>
            <td>
            <asp:TextBox runat="server" ID="TextBox24" Text="" Width="80px"></asp:TextBox></td>
        <td><asp:Label ID="lblETKR" runat="server" Text="0.25/-"></asp:Label>
            </td>
             <td>
            <asp:CheckBox ID="CheckBox2" runat="server" onclick="calc2()"/>
            </td>
        <td><asp:TextBox runat="server" ID="TextBox1" Width="80px"  Text="0"></asp:TextBox>
            </td>
</tr>
 <tr>
                                                        <td colspan="6" style="height: 5px">
                                                        </td>
                                                    </tr>
<tr>
<td> 
            3.</td>
        <td><p>लकड़ी की फड़ी</p>
            </td>
             <td>
            <asp:TextBox runat="server" ID="TextBox13" Text="" Width="80px"></asp:TextBox>
            </td>
            <td>
            <asp:TextBox runat="server" ID="TextBox25" Text="" Width="80px"></asp:TextBox></td>
        <td><asp:Label ID="Label2" runat="server" Text="0.25/-"></asp:Label>
            </td>
             <td>
            <asp:CheckBox ID="CheckBox1" runat="server" onclick="calc3()" />
            </td>
        <td><asp:TextBox runat="server" ID="TextBox2" Width="80px" Text="0"></asp:TextBox>
            </td>
</tr>
<tr>
                                                        <td colspan="6" class="style1">
                                                        </td>
                                                    </tr>
<tr>
<td> 
            4.</td>
        <td><p>एनलायसिस फिट सेट</p>
            </td>
             <td>
            <asp:TextBox runat="server" ID="TextBox14" Text="" Width="80px"></asp:TextBox>
            </td>
            <td>
            <asp:TextBox runat="server" ID="TextBox26" Text="" Width="80px"></asp:TextBox></td>
        <td><asp:Label ID="Label4" runat="server" Text="0.10/-"></asp:Label>
            </td>
             <td>
            <asp:CheckBox ID="CheckBox3" runat="server" onclick="calc4()"/>
            </td>
        <td><asp:TextBox runat="server" ID="TextBox3" Width="80px" Text="0"></asp:TextBox>
            </td>
</tr>
<tr>
                                                        <td colspan="6" class="style1">
                                                        </td>
                                                    </tr>
<tr>
<td> 
            5.</td>
        <td><p>फ्यूमीगेशन कवर</p>
            </td>
             <td>
            <asp:TextBox runat="server" ID="TextBox15" Text="" Width="80px"></asp:TextBox>
            </td>
            <td>
            <asp:TextBox runat="server" ID="TextBox23" Text="" Width="80px"></asp:TextBox></td>
        <td><asp:Label ID="Label9" runat="server" Text="3/-"></asp:Label>
            </td>
             <td>
            <asp:CheckBox ID="CheckBox5" runat="server" onclick="calc5()"/>
            </td>
        <td><asp:TextBox runat="server" ID="TextBox5" Width="80px" Text="0"></asp:TextBox>
            </td>
</tr>
<tr>
                                                        <td colspan="6" class="style1">
                                                        </td>
                                                    </tr>
<tr>
<td> 
            6.</td>
        <td><p>Fire Extinguisher ISI Mark</p>
            </td>
             <td>
            <asp:TextBox runat="server" ID="TextBox16" Text="" Width="80px"></asp:TextBox>
            </td>
            <td>
            <asp:TextBox runat="server" ID="TextBox27" Text="" Width="80px"></asp:TextBox></td>
        <td><asp:Label ID="Label11" runat="server" Text="0.50/-"></asp:Label>
            </td>
             <td>
            <asp:CheckBox ID="CheckBox6" runat="server" onclick="calc6()" />
            </td>
        <td><asp:TextBox runat="server" ID="TextBox6" Width="80px" Text="0"></asp:TextBox>
            </td>
</tr>
<tr>
                                                        <td colspan="6" class="style1">
                                                        </td>
                                                    </tr>
<tr>
<td> 
            7.</td>
        <td><p>Fire Buckets ISI Mark</p>
            </td>
            <td>
            <asp:TextBox runat="server" ID="TextBox17" Text="" Width="80px"></asp:TextBox>
            </td>
           <td>
            <asp:TextBox runat="server" ID="TextBox28" Text="" Width="80px"></asp:TextBox></td>
        <td><asp:Label ID="Label13" runat="server" Text="0.50/-"></asp:Label>
            </td>
             <td>
            <asp:CheckBox ID="CheckBox7" runat="server"  onclick="calc7()"/>
            </td>
       <td><asp:TextBox runat="server" ID="TextBox7" Width="80px" Text="0"></asp:TextBox>
            </td>
</tr>
<tr>
                                                        <td colspan="6" class="style1">
                                                        </td>
                                                    </tr>
<tr>
<td> 
            8.</td>
        <td><p>गोदाम की सुरक्षा व्यवस्था हेतु चौकीदरी हेतु सुरक्षा कर्मी उपलब्ध कराना</p>
            </td>
            <td>
            <asp:TextBox runat="server" ID="TextBox18" Text="" Width="80px"></asp:TextBox>
            </td>
            <td>
            <asp:TextBox runat="server" ID="TextBox29" Text="" Width="80px"></asp:TextBox></td>
        <td><asp:Label ID="Label15" runat="server" Text="7700/-"></asp:Label>
            </td>
             <td>
            <asp:CheckBox ID="CheckBox8" runat="server" onclick="calc8()"/>
            </td>
        <td><asp:TextBox runat="server" ID="TextBox8" Width="80px" Text="0"></asp:TextBox>
            </td>
</tr>
<tr>
                                                        <td colspan="6" class="style1">
                                                        </td>
                                                    </tr>
<tr>
<td> 
            9.</td>
        <td><p>पावर स्प्रे पंप अथवा फुट स्प्रेयर पंप</p>
            </td>
            <td>
            <asp:TextBox runat="server" ID="TextBox19" Text="" Width="80px"></asp:TextBox>
            </td>
           <td>
            <asp:TextBox runat="server" ID="TextBox30" Text="" Width="80px"></asp:TextBox></td>
        <td><asp:Label ID="Label17" runat="server" Text="2/-"></asp:Label>
            </td>
             <td>
            <asp:CheckBox ID="CheckBox9" runat="server" onclick="calc9()"/>
            </td>
       <td><asp:TextBox runat="server" ID="TextBox9" Width="80px" Text="0"></asp:TextBox>
            </td>
</tr>
<tr>
                                                        <td colspan="6" class="style1">
                                                        </td>
                                                    </tr>
<tr>
<td> 
            10.</td>
        <td><p>डिजिटल नमीमापक यंत्र(मल्टी कमोडिटी) ISI Mark</p>
            </td>
            <td>
            <asp:TextBox runat="server" ID="TextBox20" Text="" Width="80px"></asp:TextBox>
            </td>
             <td>
            <asp:TextBox runat="server" ID="TextBox31" Text="" Width="80px"></asp:TextBox></td>
        <td><asp:Label ID="Label19" runat="server" Text="2/-"></asp:Label>
            </td>
             <td>
            <asp:CheckBox ID="CheckBox10" runat="server" onclick="calc10()" />
            </td>
       <td><asp:TextBox runat="server" ID="TextBox10" Width="80px" Text="0"></asp:TextBox>
            </td>
</tr>
<tr>
                                                        <td colspan="6" class="style1">
                                                        </td>
                                                    </tr>
<tr>
<td> 
            11.</td>
        <td><p>कीटोपचार/सफाई हेतु उपलब्ध कराये जाने वाले श्रमिक</p>
            </td>
            <td>
            <asp:TextBox runat="server" ID="TextBox21" Text="" Width="80px"></asp:TextBox>
            </td>
            <td>
            <asp:TextBox runat="server" ID="TextBox32" Text="" Width="80px"></asp:TextBox></td>
        <td><asp:Label ID="Label21" runat="server" Text="7700/-"></asp:Label>
            </td>
             <td>
            <asp:CheckBox ID="CheckBox11" runat="server" onclick="calc11()"/>
            </td>
        <td><asp:TextBox runat="server" ID="TextBox11" Width="80px" Text="0"></asp:TextBox>
            </td>
</tr>
<tr>
                                                        <td colspan="6" class="style1">
                                                        </td>
                                                    </tr>
<tr>
<td> 
            12.</td>
        <td><p>बिजली पानी एवं शोचालय सूविधा</p>
            </td>
            <td>
            <%--<asp:TextBox runat="server" ID="TextBox22" Text="" Width="80px"></asp:TextBox>--%>
                <asp:RadioButton ID="rdoYes" runat="server" GroupName="Facility" Text="Yes" />
                <asp:RadioButton ID="rdoNo" runat="server" GroupName="Facility" Text="No" />
            </td>
            <td>
            <%--<asp:TextBox runat="server" ID="TextBox33" Text="" Width="80px"></asp:TextBox>--%></td>
        <td><asp:Label ID="Label23" runat="server" Text="0.50/-"></asp:Label>
            </td>
             <td>
            <asp:CheckBox ID="CheckBox12" runat="server" />
            </td>
       <td><asp:TextBox runat="server" ID="TextBox12" Width="80px" Text="0"></asp:TextBox>
            </td>
</tr>
<tr>
                                                        <td colspan="6" class="style1">
                                                        </td>
                                                    </tr>
<tr>
<td> 
            13.</td>
        <td><p>Insectiside Cost(Celphos) @ 500/Rs./Kg + 18% GST</p>
            </td>
            <td>
            <%--<asp:TextBox runat="server" ID="TextBox35" Text="" Width="80px"></asp:TextBox>--%>
            </td>
            <td>
            <asp:TextBox runat="server" ID="TextBox366" Text="" Width="80px"></asp:TextBox></td>
        <td><asp:Label ID="Label66" runat="server" Text="590/-"></asp:Label>
            </td>
             <td>
            <asp:CheckBox ID="CheckBox4" runat="server" onclick="calc12()"/>
            </td>
       <td><asp:TextBox runat="server" ID="TextBox37" Width="80px" Text="0"></asp:TextBox>
            </td>
</tr>
<tr>
                                                        <td colspan="6" class="style1">
                                                        </td>
                                                    </tr>
<tr>
<td> 
            &nbsp;</td>
        <td>&nbsp;
            </td>
            <td>
            &nbsp;
            </td>
            <td>
            &nbsp;</td>
             <th colspan="2">
            Total Resources Deduction :
            </th>
       <td><asp:TextBox runat="server" ID="txtTotalDeduction" Width="80px" Text="0"></asp:TextBox>
            </td>
</tr>
                                                           </table>
                                                             
                                                            &nbsp;</div>
                                                            <table width="100%">
                                                            <tr>
                                                            <td align="center" colspan="2">
                                                       <asp:Button ID="btnSumbmitRent" runat="server" Text="Submit" 
             CssClass="BTNBLUE" Enabled="true" onclick="btnSumbmitRent_Click"
                />
            <asp:Button ID="brnCancel" runat="server" Text="Close"
              CssClass="BTNBLUE"/>
                                                            </td>
                                                           
                                                            </tr>
                                                            <tr id="trRentBill" visible="false" runat="server">
<td colspan="4">
&nbsp;</td>
</tr>
                                                            </table>
                                                            </center>
                                                            </fieldset>
                                                            </td>
</tr>
    </table>
</div>
</center>
</fieldset>
</asp:Content>

