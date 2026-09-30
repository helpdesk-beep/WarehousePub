<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="GodownEntryForm.aspx.cs" Inherits="Accounting_GodownEntryForm"  EnableEventValidation="false"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css">
    
 <link type="text/css" rel="Stylesheet" href="css/style_new.css" />
    
    <script src="../JS/Jquery.3.6.0.js"></script>

    
<%--    <script language="javascript" type="text/javascript" src="js/MD5.js"></script>

    <script language="javascript" type="text/javascript" src="js/chksql.js"></script>--%>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <script  type="text/javascript">  

            $(document).ready(function () {
                $('#<%=rdolist.ClientID%>').change(function () {
                    var vdist = $('#<%=rdolist.ClientID%>').val();
                   // var vvv = document.getElementById("<%=rdolist.SelectedValue%>");
                    //var vvv = document.getElementById("ContentPlaceHolder1_rdolist_0");
                    //alert(vvv);
                    if (vdist == "1" || vdist == "0")
                    {

                        document.getElementById("ContentPlaceHolder1_Givdata").style.visibility = "visible";
                        document.getElementById("ContentPlaceHolder1_Givdata").style.display = "";


                        document.getElementById("ContentPlaceHolder1_Panel1").style.visibility = "hidden";
                        document.getElementById("ContentPlaceHolder1_Panel1").style.display = "none";

                        

                        document.getElementById("ContentPlaceHolder1_StoreGrid").style.visibility = "visible";
                        document.getElementById("ContentPlaceHolder1_StoreGrid").style.display = "";

                        document.getElementById("ContentPlaceHolder1_GiveGrid").style.visibility = "hidden";
                        document.getElementById("ContentPlaceHolder1_GiveGrid").style.display = "none";


                    }
                    else
                    {


                        document.getElementById("ContentPlaceHolder1_Panel1").style.visibility = "visible";
                        document.getElementById("ContentPlaceHolder1_Panel1").style.display = "";


                        document.getElementById("ContentPlaceHolder1_Givdata").style.visibility = "hidden";
                        document.getElementById("ContentPlaceHolder1_Givdata").style.display = "none";

                        document.getElementById("ContentPlaceHolder1_GiveGrid").style.visibility = "visible";
                        document.getElementById("ContentPlaceHolder1_GiveGrid").style.display = "";

                        document.getElementById("ContentPlaceHolder1_StoreGrid").style.visibility = "hidden";
                        document.getElementById("ContentPlaceHolder1_StoreGrid").style.display = "none";

                        
                    }
               
            }); });


            function NumberOnly(e) {
                var charCode = (e.which) ? e.which : e.keyCode;
                if ((charCode >= 48 && charCode <= 57)) {
                    return true;
                }
                if (charCode == 46) { return true; }
                if (charCode == 8) { return true; }
                if (charCode == 9) { return true; }
                else { return false; }
            }
           
            function Numbersonly(event) {
                event = (event) ? event : window.event;
                var charCode = (event.which) ? event.which : event.keyCode;
                if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                    return false;
                }
                return true;
            }

            function check() {

               // alert(a);


                if ($('#<%=DropDownList1.ClientID%>').val() == "0") {
                    alert("जिला चुने ");
                    $('#<%=DropDownList1.ClientID%>').focus();
                return false;
            }

            
            if ($('#<%=ddlbranchname.ClientID%>').val() == "0") {
                alert("ब्रांच चुने ");
                    $('#<%=ddlbranchname.ClientID%>').focus();
                    return false;
                }


            if ($('#<%=TextBox1.ClientID%>').val() == "") {
                alert("कल दिनाँक तक प्रेषित कुल मात्रा दर्ज करे ");
                $('#<%=TextBox1.ClientID%>').focus();
                return false;
            }


            if ($('#<%=TextBox2.ClientID%>').val() == "") {
                alert("MPWLC OWN Godown मात्रा दर्ज करे ");
                $('#<%=TextBox2.ClientID%>').focus();
                return false;
            }

            
            if ($('#<%=TextBox3.ClientID%>').val() == "") {
                alert("आज  दिनाँक को  प्रेषित  मात्रा दर्ज करे ");
                $('#<%=TextBox3.ClientID%>').focus();
                return false;
            }

            
<%--            if ($('#<%=TextBox4.ClientID%>').val() == "") {
                alert("plz fill Data");
                $('#<%=TextBox4.ClientID%>').focus();
                return false;
            }--%>

            if ($('#<%=TextBox5.ClientID%>').val() == "") {
                alert("Hirred + adhigra han Godown  मात्रा दर्ज करे ");
                $('#<%=TextBox5.ClientID%>').focus();
                return false;
            }

            
            if ($('#<%=TextBox6.ClientID%>').val() == "") {
                alert("JVS Godown  मात्रा दर्ज करे");
                $('#<%=TextBox6.ClientID%>').focus();
                return false;
            }

            
            if ($('#<%=TextBox7.ClientID%>').val() == "") {
                alert("CAP मात्रा दर्ज करे");
                $('#<%=TextBox7.ClientID%>').focus();
                return false;
            }

            

            
            if ($('#<%=TextBox9.ClientID%>').val() == "") {
                alert("Godown मात्रा दर्ज करे");
                $('#<%=TextBox9.ClientID%>').focus();
                return false;
            }

            
            if ($('#<%=TextBox10.ClientID%>').val() == "") {
                alert("CAP मात्रा दर्ज करे");
                $('#<%=TextBox10.ClientID%>').focus();
                return false;
            }

            
            if ($('#<%=TextBox11.ClientID%>').val() == "") {
                alert("CWC मात्रा दर्ज करे");
                $('#<%=TextBox11.ClientID%>').focus();
                return false;
            }

            
            if ($('#<%=TextBox12.ClientID%>').val() == "") {
                alert("OLD FED मात्रा दर्ज करे");
                $('#<%=TextBox12.ClientID%>').focus();
                return false;
            }

            
            

            
           
            }




            function checks() {



                if ($('#<%=DropDownList5.ClientID%>').val() == "0") {
                    alert("जिला चुने ");
                    $('#<%=DropDownList5.ClientID%>').focus();
                return false;
            }

            
            if ($('#<%=DropDownList6.ClientID%>').val() == "0") {
                alert("ब्रांच चुने ");
                $('#<%=DropDownList6.ClientID%>').focus();
                return false;
            }

                if ($('#<%=TextBox14.ClientID%>').val() == "") {
                    alert("कल दिनाँक तक प्रेषित कुल मात्रा दर्ज करे");
                    $('#<%=TextBox14.ClientID%>').focus();
                    return false;
                }


                if ($('#<%=TextBox15.ClientID%>').val() == "") {
                    alert("आज  दिनाँक को  प्रेषित  मात्रा  दर्ज करे");
                    $('#<%=TextBox15.ClientID%>').focus();
                    return false;
                }


<%--                if ($('#<%=TextBox16.ClientID%>').val() == "") {
                    alert("plz fill Data");
                    $('#<%=TextBox16.ClientID%>').focus();
                    return false;
                }--%>


                if ($('#<%=TextBox17.ClientID%>').val() == "") {
                    alert("MPWLC OWN Godown  मात्रा  दर्ज करे");
                    $('#<%=TextBox17.ClientID%>').focus();
                    return false;
                }

                if ($('#<%=TextBox18.ClientID%>').val() == "") {
                    alert("Hirred + adhigra han Godown  मात्रा  दर्ज करे");
                    $('#<%=TextBox18.ClientID%>').focus();
                    return false;
                }


                if ($('#<%=TextBox19.ClientID%>').val() == "") {
                    alert("JVS Godown  मात्रा  दर्ज करे");
                    $('#<%=TextBox19.ClientID%>').focus();
                    return false;
                }


                if ($('#<%=TextBox21.ClientID%>').val() == "") {
                    alert("CAP  मात्रा  दर्ज करे");
                    $('#<%=TextBox21.ClientID%>').focus();
                    return false;
                }




                if ($('#<%=TextBox22.ClientID%>').val() == "") {
                    alert("Godown  मात्रा  दर्ज करे");
                $('#<%=TextBox22.ClientID%>').focus();
                return false;
            }

            
            if ($('#<%=TextBox23.ClientID%>').val() == "") {
                alert("CAP  मात्रा  दर्ज करे");
                $('#<%=TextBox23.ClientID%>').focus();
                return false;
            }

            
            if ($('#<%=TextBox24.ClientID%>').val() == "") {
                alert("CWC  मात्रा  दर्ज करे");
                $('#<%=TextBox24.ClientID%>').focus();
                return false;
            }

            
            if ($('#<%=TextBox25.ClientID%>').val() == "") {
                alert("OLD FED  मात्रा  दर्ज करे");
                $('#<%=TextBox25.ClientID%>').focus();
                return false;
            }

            
<%--            if ($('#<%=TextBox26.ClientID%>').val() == "") {
                alert("plz fill Data");
                $('#<%=TextBox26.ClientID%>').focus();
                return false;
            }--%>

            }
        </script>


        <script type="text/javascript">
            $(document).ready(function () {
                $('#<%=DropDownList5.ClientID%>').change(function () {
                var vdist = $('#<%=DropDownList5.ClientID%>').val();
                // alert("A");
                $.ajax({
                    type: "POST",
                    url: "GodownEntryForm.aspx/getBranchbydistidN",
                    data: "{'distid':'" + vdist + "'}",
                    contentType: "application/json;charsetutf-8",
                    datatype: "json",
                    success: function (msg) {
                        var FResult = JSON.parse(msg.d);
                        $("#<%= DropDownList6.ClientID %>").empty();
                        $("#<%= DropDownList6.ClientID %>").append("<option value='0'>-ब्रांच चुने -</option>");
                        for (var i = 0; i < FResult.length; i++) {
                            $("#<%= DropDownList6.ClientID %>").append($("<option></option>").val(FResult[i].DepotID).html(FResult[i].DepotName));
                        }
                    },
                    error: function (xhr, status, error) {
                        alert(xhr.responseText);
                    }
                });
            });
        


            $('#<%=DropDownList6.ClientID%>').change(function () {
                var vdist = $('#<%=DropDownList6.ClientID%>').val();
                $('#<%=hdnBranchid.ClientID%>').val(vdist);

            });
        });
        </script>

    <style>
        body {
                font-size: 15px;
        }
    </style>

    <div id="msg" runat="server"></div>
    <asp:HiddenField ID="hdnBranchid" runat="server" />
               <section>
                            <div class="container">
                        <div class="card-body">

                   <div class="row">
            


                           <div class="col-md-6">
                           <div class="form-group">

<%--                               <asp:RadioButtonList ID="rdolist1" runat="server" >
                                   <asp:ListItem Text="अन्य जिले से प्रेषित मात्रा का विवरण" Value="1"></asp:ListItem>
                                     <asp:ListItem Text="अन्य जिले से प्राप्त सह भंडारित मात्रा  का विवरण " Value="2"></asp:ListItem>
                               </asp:RadioButtonList>--%>
                             

                            <asp:DropDownList ID="rdolist" runat="server"  Width="200px" Height="25px" CssClass="tb6">
                                <asp:ListItem Text="चुने" Value="0"></asp:ListItem>

                                   <asp:ListItem Text="अन्य जिले से प्रेषित मात्रा का विवरण" Value="1"></asp:ListItem>
                                   <asp:ListItem Text="अन्य जिले से प्राप्त सह भंडारित मात्रा  का विवरण " Value="2"></asp:ListItem>
                               </asp:DropDownList>
                               
                           </div>
                       </div>
                       </div>
                            </div>
                                </div>



                   <asp:Panel ID="Givdata" runat="server" >

                           <div class="container">
<fieldset style="width: 980px; border: 1px solid navy;">
                                        <center>
                                            <div>
                                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                    <tr style="background-color: #0bb6e6; height: 25px">
                                                        <td colspan="4" align="center">
                                                            <asp:Label ID="Label2" runat="server" Text="अन्य जिले से प्रेषित मात्रा का विवरण" Font-Bold="true"
                                                                Font-Size="12pt" ForeColor="whitesmoke"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr align="center">
                                                        <td colspan="4" style="height: 5px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" style="width: 200px">
                                                            <asp:Label ID="lblDepositDate" runat="server" Text="जिले का नाम जहाँ  स्कंध प्रेषित किया गया " ForeColor="navy" Font-Bold="true"
                                                                Font-Size="8pt"></asp:Label>
                                                            <span class="style1"></span></td>
                                                        <td align="left" style="width: 200px">
                                                              <asp:DropDownList ID="DropDownList1"  runat="server"  Width="155px" Height="25px" CssClass="tb6"  OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" AutoPostBack="true" ></asp:DropDownList>
                                                                                                                       
                                                        </td>
                                                        <td align="left" style="width: 200px">
                                                            <asp:Label ID="lblLotNo" runat="server" Text="शाखा का नाम" ForeColor="navy" Font-Bold="true"
                                                                Font-Size="8pt"></asp:Label>
                                                        </td>
                                                        <td align="left" colspan="3">                                                            
                                                      <asp:DropDownList ID="ddlbranchname"  runat="server"  Width="155px" Height="25px" CssClass="tb6" ></asp:DropDownList>
                               
                                                            <%--<asp:TextBox ID="txtlotnumber" runat="server" MaxLength="30" Width="150px" CssClass="tb6"></asp:TextBox>--%>
                                                        </td>
                                                    </tr>
                                                    <tr align="center">
                                                        <td colspan="4" style="height: 5px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <asp:Label ID="lbltotalReceivedBags" runat="server" Text="कल दिनाँक तक प्रेषित कुल मात्रा" ForeColor="navy"
                                                                Font-Bold="true" Font-Size="8pt"></asp:Label>
                                                        </td>
                                                        <td align="left">                                                            
                                  <asp:TextBox ID="TextBox1" runat="server" CssClass="tb6" onkeypress="return NumberOnly(event)"  ></asp:TextBox>
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="lblTotalQuantityReceived" runat="server" Text="आज  दिनाँक को  प्रेषित  मात्रा"
                                                                ForeColor="navy" Font-Bold="true" Font-Size="8pt"></asp:Label>
                                                        </td>
                                                        <td align="left">                                                           
                               <asp:TextBox ID="TextBox3" runat="server" onkeypress="return NumberOnly(event)" CssClass="tb6" ></asp:TextBox>                               
                                                        </td>
                                                    </tr>
                                                    <tr align="center">
                                                        <td colspan="4" style="height: 5px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <asp:Label ID="lblWeighmentMode" runat="server" Text="MPWLC OWN Godown" ForeColor="navy"
                                                                Font-Bold="true" Font-Size="8pt"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                <asp:TextBox ID="TextBox2" runat="server" onkeypress="return NumberOnly(event)"  CssClass="tb6" ></asp:TextBox>                               
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="lblWeightmentOn" runat="server" Text="Hirred + adhigra han Godown" ForeColor="navy"
                                                                Font-Bold="true" Font-Size="8pt"></asp:Label>
                                                        </td>
                                                        <td align="left">                               
                             <asp:TextBox ID="TextBox5" runat="server" onkeypress="return NumberOnly(event)"  CssClass="tb6" ></asp:TextBox> 
                               
                                                        </td>
                                                    </tr>
                                                    <tr align="center">
                                                        <td colspan="4" style="height: 5px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <asp:Label ID="lblAvgMoistureContent" runat="server" Text="JVS Godown"
                                                                ForeColor="navy" Font-Bold="true" Font-Size="8pt"></asp:Label>
                                                        </td>
                                                        <td align="left">
                             <asp:TextBox ID="TextBox6" runat="server" onkeypress="return NumberOnly(event)"  CssClass="tb6" ></asp:TextBox>                               
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="lbl_To" runat="server" Text="CAP" ForeColor="navy" Font-Bold="true"
                                                                Font-Size="8pt"></asp:Label>
                                                        </td>
                                                        <td align="left">
                             <asp:TextBox ID="TextBox7" runat="server" onkeypress="return NumberOnly(event)"  CssClass="tb6" ></asp:TextBox>          
                               
                                                        </td>
                                                    </tr>
                                                    <tr align="center">
                                                        <td colspan="4" style="height: 5px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <asp:Label ID="lblMarketValue" runat="server" Text="Godown" ForeColor="Navy"
                                                                Font-Bold="True" Font-Size="8pt"></asp:Label>
                                                        </td>
                                                        <td align="left" >
                                                
                                            <asp:TextBox ID="TextBox9" runat="server" onkeypress="return NumberOnly(event)" CssClass="tb6" ></asp:TextBox>
                               
                                                        </td>
                                                        <td>
                                                        
                                                            <asp:Label ID="lblMarketValue0" runat="server" Font-Bold="True" Font-Size="8pt" 
                                                                ForeColor="Navy" Text="CAP"></asp:Label>
                                                        
                                                        </td>
                                                        <td align="left">                                  
                                                            
                             <asp:TextBox ID="TextBox10" runat="server" onkeypress="return NumberOnly(event)" CssClass="tb6" ></asp:TextBox>                                
                                                        
                                                        </td>
                                                    </tr>
                                                    <tr align="center">
                                                        <td colspan="4" style="height: 5px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                     <td align="left">
                                            <asp:Label ID="lblCategoty_Non" runat="server" Font-Size="8pt" ForeColor="navy" Font-Bold="true"
                                                Text="CWC"></asp:Label></td>
                                                     <td align="left">

                             <asp:TextBox ID="TextBox11" runat="server" onkeypress="return NumberOnly(event)" CssClass="tb6" ></asp:TextBox>
                               
                                        </td>
                                                    <td align="left">
                                                    
                                                        <asp:Label ID="lblMarketValue1" runat="server" Font-Bold="True" Font-Size="8pt" 
                                                            ForeColor="Navy" Text="OLD FED"></asp:Label>
                                                    
                                                    </td>
                                                    
                                                         
                                                        <td align="left">

                             <asp:TextBox ID="TextBox12" runat="server" onkeypress="return NumberOnly(event)" CssClass="tb6" ></asp:TextBox>
                               
                                                        </td>
                                                    </tr>

                                                   
                                                    <tr>
                                                        <td colspan="4" align="center">
                                                            

                                                            <asp:Button ID="btn" runat="server" CssClass="BTNBLUE" Width="100px" Text="submit" OnClientClick="return check()" OnClick="btn_Click" />
                                                            
                                                        </td>
                                                    </tr>
                                                    
                                                </table>
                                            </div>
                                        </center>
                                    </fieldset>



</div>

               
</asp:Panel>



                                      <asp:Panel ID="Panel1" runat="server" style="visibility:hidden;display:none"   >
               <div class="container">
                   



<fieldset style="width: 980px; border: 1px solid navy;">
                                        <center>
                                            <div>
                                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                    <tr style="background-color: #0bb6e6; height: 25px">
                                                        <td colspan="4" align="center">
                                                            <asp:Label ID="Label1" runat="server" Text="अन्य जिले से प्राप्त सह भंडारित मात्रा  का विवरण" Font-Bold="true"
                                                                Font-Size="12pt" ForeColor="whitesmoke"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr align="center">
                                                        <td colspan="4" style="height: 5px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" style="width: 200px">
                                                            <asp:Label ID="Label3" runat="server" Text="उस  जिले का नाम जहा से स्कंध प्राप्त हुआ" ForeColor="navy" Font-Bold="true"
                                                                Font-Size="8pt"></asp:Label>
                                                            <span class="style1"></span></td>
                                                        <td align="left" style="width: 200px">                         
                                                            
                                                  <asp:DropDownList ID="DropDownList5"   runat="server"  Width="155px" Height="25px" CssClass="tb6"  ></asp:DropDownList><%--OnSelectedIndexChanged="DropDownList5_SelectedIndexChanged"  AutoPostBack="true"--%>

                                                        </td>
                                                        <td align="left" style="width: 200px">
                                                            <asp:Label ID="Label4" runat="server" Text="शाखा का नाम" ForeColor="navy" Font-Bold="true"
                                                                Font-Size="8pt"></asp:Label>
                                                        </td>
                                                        <td align="left" colspan="3">                                                            
                                                      <asp:DropDownList ID="DropDownList6"  runat="server"  Width="155px" Height="25px" CssClass="tb6" ></asp:DropDownList>
                               
                                                            <%--<asp:TextBox ID="txtlotnumber" runat="server" MaxLength="30" Width="150px" CssClass="tb6"></asp:TextBox>--%>
                                                        </td>
                                                    </tr>

                                                    <tr align="center">
                                                        <td colspan="4" style="height: 5px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <asp:Label ID="Label5" runat="server" Text="कल दिनाँक तक प्राप्त कुल मात्रा" ForeColor="navy"
                                                                Font-Bold="true" Font-Size="8pt"></asp:Label>
                                                        </td>
                                                        <td align="left">                                                    
                                                            
                                  <asp:TextBox ID="TextBox14" runat="server" onkeypress="return NumberOnly(event)" CssClass="tb6" ></asp:TextBox>
                               
                                  
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="Label6" runat="server" Text="आज  दिनाँक को  प्राप्त  मात्रा"
                                                                ForeColor="navy" Font-Bold="true" Font-Size="8pt"></asp:Label>
                                                        </td>
                                                        <td align="left">    
                                                            
                               <asp:TextBox ID="TextBox15" runat="server" onkeypress="return NumberOnly(event)" CssClass="tb6" ></asp:TextBox>
                               
                                                        </td>
                                                    </tr>
                                                    <tr align="center">
                                                        <td colspan="4" style="height: 5px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <asp:Label ID="Label7" runat="server" Text="MPWLC OWN Godown" ForeColor="navy"
                                                                Font-Bold="true" Font-Size="8pt"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                <asp:TextBox ID="TextBox17" runat="server" onkeypress="return NumberOnly(event)"  CssClass="tb6" ></asp:TextBox>                               
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="Label8" runat="server" Text="Hirred + adhigra han Godown" ForeColor="navy"
                                                                Font-Bold="true" Font-Size="8pt"></asp:Label>
                                                        </td>
                                                        <td align="left">                               
                             <asp:TextBox ID="TextBox18" runat="server" onkeypress="return NumberOnly(event)"  CssClass="tb6" ></asp:TextBox> 
                               
                                                        </td>
                                                    </tr>
                                                    <tr align="center">
                                                        <td colspan="4" style="height: 5px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <asp:Label ID="Label9" runat="server" Text="JVS Godown"
                                                                ForeColor="navy" Font-Bold="true" Font-Size="8pt"></asp:Label>
                                                        </td>
                                                        <td align="left">
                             <asp:TextBox ID="TextBox19" runat="server" onkeypress="return NumberOnly(event)"  CssClass="tb6" ></asp:TextBox>                               
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="Label10" runat="server" Text="CAP" ForeColor="navy" Font-Bold="true"
                                                                Font-Size="8pt"></asp:Label>
                                                        </td>
                                                        <td align="left">
                             <asp:TextBox ID="TextBox21" runat="server" onkeypress="return NumberOnly(event)"  CssClass="tb6" ></asp:TextBox>          
                               
                                                        </td>
                                                    </tr>
                                                    <tr align="center">
                                                        <td colspan="4" style="height: 5px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <asp:Label ID="Label11" runat="server" Text="Godown" ForeColor="Navy"
                                                                Font-Bold="True" Font-Size="8pt"></asp:Label>
                                                        </td>
                                                        <td align="left" >
                                                
                                            <asp:TextBox ID="TextBox22" runat="server" onkeypress="return NumberOnly(event)" CssClass="tb6" ></asp:TextBox>
                               
                                                        </td>
                                                        <td>
                                                        
                                                            <asp:Label ID="Label12" runat="server" Font-Bold="True" Font-Size="8pt" 
                                                                ForeColor="Navy" Text="CAP"></asp:Label>
                                                        
                                                        </td>
                                                        <td align="left">                                  
                                                            
                             <asp:TextBox ID="TextBox23" runat="server" onkeypress="return NumberOnly(event)" CssClass="tb6" ></asp:TextBox>                                
                                                        
                                                        </td>
                                                    </tr>
                                                    <tr align="center">
                                                        <td colspan="4" style="height: 5px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                     <td align="left">
                                            <asp:Label ID="Label13" runat="server" Font-Size="8pt" ForeColor="navy" Font-Bold="true"
                                                Text="CWC"></asp:Label></td>
                                                     <td align="left">

                             <asp:TextBox ID="TextBox24" runat="server" onkeypress="return NumberOnly(event)" CssClass="tb6" ></asp:TextBox>
                               
                                        </td>
                                                    <td align="left">
                                                    
                                                        <asp:Label ID="Label14" runat="server" Font-Bold="True" Font-Size="8pt" 
                                                            ForeColor="Navy" Text="OLD FED"></asp:Label>
                                                    
                                                    </td>
                                                    
                                                         
                                                        <td align="left">

                             <asp:TextBox ID="TextBox25" runat="server" onkeypress="return NumberOnly(event)" CssClass="tb6" ></asp:TextBox>
                               
                                                        </td>
                                                    </tr>

                                                   
                                                    <tr>
                                                        <td colspan="4" align="center">
                                                             <asp:Button ID="Button1" runat="server" CssClass="BTNBLUE" Width="100px" Text="submit" OnClientClick="return checks()" OnClick="Button1_Click" />

                                                          
                                                            
                                                        </td>
                                                    </tr>
                                                    
                                                </table>
                                            </div>
                                        </center>
                                    </fieldset>






                   
                   </div>
</asp:Panel>
           </section>


    <br />
    <asp:Panel ID="StoreGrid" runat="server"  >

       <%--   <asp:Repeater ID="Repeater1" runat="server" >
            <HeaderTemplate>
                <table>
                    <tr>
                        <td style="text-align:center">अन्य जिले से प्रेषित मात्रा का विवरण </td>
                    
                    </tr>
                </table>
         

           
                <table>
                    <td>क्र.</td>
              
                     <td>जिले का नाम यहा स्कंध प्रेषित किया गया </td>
                     <td>कल दिनाँक तक प्रेषित कुल मात्रा </td>
                     <td>आज  दिनाँक को  प्रेषित  मात्रा</td>
                     <td>कुल प्रेषित  मात्रा योग (4+5)</td>
                    <td> आज दिनाँक से</td>
                      <td>कल दिनाँक तक</td>
            
                    <td>शाखा का नाम </td>

                    <td>
                       <table>
                           <tr>
                              
                        
                               <tr>
                                   <td>RUN & Superwised BY MPWLC  </td>
                                  <td> MARKFED  </td>
                                 
                                   <td>  CWC</td>
                                   <td> OLD FED </td>
                                   <td>  Total (Sum 9 to 16 ) </td>
                              
                                       
                               </tr>
                          <tr>
                              <td colspan="2">
                                         <table>
                              <tr>
                                  <td >MPWLC OWN Godown</td>
                                  <td >Hirred + adhigra han Godown</td>
                                  <td >JVS Godown</td>  
                                  <td >CAP</td>
                                  <td >Godown</td>
                                  <td >CAP</td>
                            
                                 
                              </tr>
                                    </table> 
                              </td>
                          </tr>
                    
                            
                        
                           
                             
                           </tr>
                       </table>
                    </td>


              
                </table>
              </HeaderTemplate>
            <ItemTemplate>
                <table>
                    <tr>
                        <td><asp:Label ID="lblRowNumber" Text='<%# Container.ItemIndex + 1 %>' runat="server" /></td>
                       <td><asp:Label ID="lblComment" runat="server" Text='<%#Eval("DistName") %>'/> </td>
                        <td><asp:Label ID="Label16" runat="server" Text='<%#Eval("Before_TodayDate_Qty") %>'/> </td>
                         <td><asp:Label ID="Label17" runat="server" Text='<%#Eval("TodayDate_Qty") %>'/> </td>
                         <td><asp:Label ID="Label18" runat="server" Text='<%#Eval("TotaldayDate_Qty") %>'/> </td>
                         <td><asp:Label ID="Label19" runat="server" Text='<%#Eval("Insert_Date") %>'/> </td>
                         <td><asp:Label ID="Label21" runat="server" Text='<%#Eval("Before_TodayDate") %>'/> </td>
                         <td><asp:Label ID="Label22" runat="server" Text='<%#Eval("Branch_Name") %>'/> </td>
                         <td><asp:Label ID="Label23" runat="server" Text='<%#Eval("MPWLC_Own_Godown_Qty") %>'/> </td>
                         <td><asp:Label ID="Label20" runat="server" Text='<%#Eval("MPWLC_Hired_Godown_Qty") %>'/> </td>

                         <td><asp:Label ID="Label24" runat="server" Text='<%#Eval("MPWLC_Jvs_Godown_Qty") %>'/> </td>
                         <td><asp:Label ID="Label25" runat="server" Text='<%#Eval("MPWLC_Cap_Godown_Qty") %>'/> </td>
                         <td><asp:Label ID="Label15" runat="server" Text='<%#Eval("Markfed_Godown_Qty") %>'/> </td>
                         <td><asp:Label ID="Label26" runat="server" Text='<%#Eval("Markfed_Cap_Qty") %>'/> </td>

                        <td><asp:Label ID="Label27" runat="server" Text='<%#Eval("Cwc_Qty") %>'/> </td>
                        <td><asp:Label ID="Label28" runat="server" Text='<%#Eval("Oil_Fed_Qty") %>'/> </td>
                        <td><asp:Label ID="Label29" runat="server" Text='<%#Eval("Total_Qty") %>'/> </td>
                    </tr>
                </table>
            </ItemTemplate>
            
        </asp:Repeater>--%>

             <asp:GridView ID="GridView1"  CssClass="Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr" runat="server" AutoGenerateColumns="false" Visible="true" >
                                    <Columns>
                 


                                        <asp:TemplateField>
                                            <HeaderTemplate>

                 

                
                                        
                                                    <th style="text-align: center;">क्र.</th>
                                                    <th style="text-align: center;">जिला यहा स्कंध प्रेषित किया गया </th>
                                                    <th style="text-align: center;">कल दिनाँक तक प्रेषित कुल मात्रा</th>
                                                    <th style="text-align: center;">आज  दिनाँक को  प्रेषित  मात्रा </th>
                                                    <th style="text-align: center;">कुल प्रेषित  मात्रा योग (4+5)</th>
                                                    <th style="text-align: center;">शाखा का नाम </th>
                                                    <th style="text-align: center;">आज दिनाँक से</th>
                                                    <th style="text-align: center;">कल दिनाँक तक</th>                             
                                                   <th style="text-align: center;">MPWLC OWN Godown </th>                                                    
                                                    <th style="text-align: center;">MPWLC(Hirred + adhigra han Godown)</th>
                                                      <th style="text-align: center;">MPWLC(JVS Godown)</th>
                                                    <th style="text-align: center;">MPWLC(CAP) </th>                                            

                                                    <th style="text-align: center;">MARKFED(Godown)</th>                                                    
                                                      <th style="text-align: center;">MARKFED(CAP) </th>


                                                    <th  style="text-align: center;">CWC</th>
                                                    <th  style="text-align: center;"> OLD FED </th>
                                                    <th style="text-align: center;">Total (Sum 9 to 16 )</th>
                                                 

                                                </tr>
                        
                                            </HeaderTemplate>
                                            <ItemTemplate>

                                                    


                                                 <td><%# Container.DataItemIndex + 1 %></td>
                    <td><asp:Label ID="lblComment" runat="server" Text='<%#Eval("DistName") %>'/> </td>
                        <td><asp:Label ID="Label16" runat="server" Text='<%#Eval("Before_TodayDate_Qty") %>'/> </td>
                         <td><asp:Label ID="Label17" runat="server" Text='<%#Eval("TodayDate_Qty") %>'/> </td>
                         <td><asp:Label ID="Label18" runat="server" Text='<%#Eval("TotaldayDate_Qty") %>'/> </td>
                            <td><asp:Label ID="Label22" runat="server" Text='<%#Eval("Branch_Name") %>'/> </td>
                         <td><asp:Label ID="Label19" runat="server" Text='<%#Eval("Insert_Date") %>'/> </td>
                         <td><asp:Label ID="Label21" runat="server" Text='<%#Eval("Before_TodayDate") %>'/> </td>
                        
                         <td><asp:Label ID="Label23" runat="server" Text='<%#Eval("MPWLC_Own_Godown_Qty") %>'/> </td>
                         <td><asp:Label ID="Label20" runat="server" Text='<%#Eval("MPWLC_Hired_Godown_Qty") %>'/> </td>

                         <td><asp:Label ID="Label24" runat="server" Text='<%#Eval("MPWLC_Jvs_Godown_Qty") %>'/> </td>
                         <td><asp:Label ID="Label25" runat="server" Text='<%#Eval("MPWLC_Cap_Godown_Qty") %>'/> </td>
                         <td><asp:Label ID="Label15" runat="server" Text='<%#Eval("Markfed_Godown_Qty") %>'/> </td>
                         <td><asp:Label ID="Label26" runat="server" Text='<%#Eval("Markfed_Cap_Qty") %>'/> </td>

                        <td><asp:Label ID="Label27" runat="server" Text='<%#Eval("Cwc_Qty") %>'/> </td>
                        <td><asp:Label ID="Label28" runat="server" Text='<%#Eval("Oil_Fed_Qty") %>'/> </td>
                        <td><asp:Label ID="Label29" runat="server" Text='<%#Eval("Total_Qty") %>'/> </td>
                                            </ItemTemplate>

                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
    </asp:Panel>


    <asp:Panel ID="GiveGrid" runat="server" style="visibility:hidden;display:none" >
        <%--  <asp:Repeater ID="Repeater2" runat="server">
            <HeaderTemplate>
                <table>
                    <tr>
                  
                         <td style="text-align:center">अन्य जिले से प्राप्त सह भंडारित मात्रा  का विवरण </td>
                    </tr>
                </table>
         

           
                <table>
                    <td>क्र.</td>
     
                          <td>उस  जिले का नाम जहा से स्कंध प्राप्त हुआ </td>
                     <td>कल दिनाँक तक प्रेषित कुल मात्रा </td>
                     <td>आज  दिनाँक को  प्रेषित  मात्रा</td>
                     <td>कुल प्रेषित  मात्रा योग (4+5)</td>
               
                    <td>शाखा का नाम </td>
                         <td>आज दिनाँक से</td>
                      <td>कल दिनाँक तक</td>

              


                    <td>
                         <table>
                           <tr>
                         
                               <tr>
                               <td >
                                   RUN & Superwised BY MPWLC 
                               </td>
                                 <td >
                                 MARKFED 
                               </td>
                                  <td >
                                 CWC
                               </td>
                                   <td >
                                 OLD FED 
                               </td>
                                     <td>
                                Total (Sum 19 to 25 )
                               </td>
                           </tr>


                                     <tr>
                              <td colspan="2">
                                         <table>
                              <tr>
                                   <td >MPWLC OWN Godown</td>
                                  <td >Hirred + adhigra han Godown</td>
                                  <td >JVS Godown</td>  
                                  <td >CAP</td>
                                  <td >Godown</td>
                                  <td >MARKEFD</td>
                            
                                 
                              </tr>
                                    </table> 
                              </td>
                          </tr>
                               </tr></table>
                    </td>
                </table>
              </HeaderTemplate>
            <ItemTemplate>
                <table>
                    <tr>
                        <td><asp:Label ID="lblRowNumber" Text='<%# Container.ItemIndex + 1 %>' runat="server" />
                    <td><asp:Label ID="lblComment" runat="server" Text='<%#Eval("DistName") %>'/> </td>
                        <td><asp:Label ID="Label16" runat="server" Text='<%#Eval("Before_TodayDate_Qty") %>'/> </td>
                         <td><asp:Label ID="Label17" runat="server" Text='<%#Eval("TodayDate_Qty") %>'/> </td>
                         <td><asp:Label ID="Label18" runat="server" Text='<%#Eval("TotaldayDate_Qty") %>'/> </td>
                         <td><asp:Label ID="Label19" runat="server" Text='<%#Eval("Insert_Date") %>'/> </td>
                         <td><asp:Label ID="Label21" runat="server" Text='<%#Eval("Before_TodayDate") %>'/> </td>
                         <td><asp:Label ID="Label22" runat="server" Text='<%#Eval("Branch_Name") %>'/> </td>
                         <td><asp:Label ID="Label23" runat="server" Text='<%#Eval("MPWLC_Own_Godown_Qty") %>'/> </td>
                         <td><asp:Label ID="Label20" runat="server" Text='<%#Eval("MPWLC_Hired_Godown_Qty") %>'/> </td>

                         <td><asp:Label ID="Label24" runat="server" Text='<%#Eval("MPWLC_Jvs_Godown_Qty") %>'/> </td>
                         <td><asp:Label ID="Label25" runat="server" Text='<%#Eval("MPWLC_Cap_Godown_Qty") %>'/> </td>
                         <td><asp:Label ID="Label15" runat="server" Text='<%#Eval("Markfed_Godown_Qty") %>'/> </td>
                         <td><asp:Label ID="Label26" runat="server" Text='<%#Eval("Markfed_Cap_Qty") %>'/> </td>

                        <td><asp:Label ID="Label27" runat="server" Text='<%#Eval("Cwc_Qty") %>'/> </td>
                        <td><asp:Label ID="Label28" runat="server" Text='<%#Eval("Oil_Fed_Qty") %>'/> </td>
                        <td><asp:Label ID="Label29" runat="server" Text='<%#Eval("Total_Qty") %>'/> </td>
                    </tr>
                </table>
            </ItemTemplate>
            
        </asp:Repeater>--%>

              <asp:GridView ID="gvCol3"  CssClass="Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr" runat="server" AutoGenerateColumns="false" Visible="true" >
                                    <Columns>
                 


                                        <asp:TemplateField>
                                            <HeaderTemplate>

                 


                                        
                                                    <th style="text-align: center;">क्र.</th>
                                                    <th style="text-align: center;">जिला जहा से स्कंध प्राप्त हुआ </th>
                                                    <th style="text-align: center;">कल दिनाँक तक प्राप्त कुल मात्रा</th>
                                                    <th style="text-align: center;">आज  दिनाँक को  प्राप्त  मात्रा </th>
                                                    <th style="text-align: center;">कुल प्राप्त  मात्रा योग (4+5)</th>
                                                    <th style="text-align: center;">शाखा का नाम </th>
                                                    <th style="text-align: center;">आज दिनाँक से</th>
                                                    <th style="text-align: center;">कल दिनाँक तक</th>


                             
                                                   <th style="text-align: center;">MPWLC OWN Godown </th>                                                    
                                                    <th style="text-align: center;">MPWLC(Hirred + adhigra han Godown)</th>
                                                      <th style="text-align: center;">MPWLC(JVS Godown)</th>
                                                    <th style="text-align: center;">MPWLC(CAP) </th>

                                             

                                                    <th style="text-align: center;">MARKFED(Godown)</th>                                                    
                                                      <th style="text-align: center;">MARKFED(CAP) </th>


                                                    <th  style="text-align: center;">CWC</th>
                                                    <th  style="text-align: center;"> OLD FED </th>
                                                    <th style="text-align: center;">Total (Sum 19 to 25 )</th>
                                                 

                                                </tr>
                 
                                            </HeaderTemplate>
                                            <ItemTemplate>

                                                    


                                                 <td><%# Container.DataItemIndex + 1 %></td>
                    <td><asp:Label ID="lblComment" runat="server" Text='<%#Eval("DistName") %>'/> </td>
                        <td><asp:Label ID="Label16" runat="server" Text='<%#Eval("Before_TodayDate_Qty") %>'/> </td>
                         <td><asp:Label ID="Label17" runat="server" Text='<%#Eval("TodayDate_Qty") %>'/> </td>
                         <td><asp:Label ID="Label18" runat="server" Text='<%#Eval("TotaldayDate_Qty") %>'/> </td>
                            <td><asp:Label ID="Label22" runat="server" Text='<%#Eval("Branch_Name") %>'/> </td>
                         <td><asp:Label ID="Label19" runat="server" Text='<%#Eval("Insert_Date") %>'/> </td>
                         <td><asp:Label ID="Label21" runat="server" Text='<%#Eval("Before_TodayDate") %>'/> </td>
                        
                         <td><asp:Label ID="Label23" runat="server" Text='<%#Eval("MPWLC_Own_Godown_Qty") %>'/> </td>
                         <td><asp:Label ID="Label20" runat="server" Text='<%#Eval("MPWLC_Hired_Godown_Qty") %>'/> </td>

                         <td><asp:Label ID="Label24" runat="server" Text='<%#Eval("MPWLC_Jvs_Godown_Qty") %>'/> </td>
                         <td><asp:Label ID="Label25" runat="server" Text='<%#Eval("MPWLC_Cap_Godown_Qty") %>'/> </td>
                         <td><asp:Label ID="Label15" runat="server" Text='<%#Eval("Markfed_Godown_Qty") %>'/> </td>
                         <td><asp:Label ID="Label26" runat="server" Text='<%#Eval("Markfed_Cap_Qty") %>'/> </td>

                        <td><asp:Label ID="Label27" runat="server" Text='<%#Eval("Cwc_Qty") %>'/> </td>
                        <td><asp:Label ID="Label28" runat="server" Text='<%#Eval("Oil_Fed_Qty") %>'/> </td>
                        <td><asp:Label ID="Label29" runat="server" Text='<%#Eval("Total_Qty") %>'/> </td>
                                            </ItemTemplate>

                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
    </asp:Panel>


            
    

      
</asp:Content>

