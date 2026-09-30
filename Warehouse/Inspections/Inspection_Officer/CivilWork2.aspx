<%@ Page Title="" Language="C#" MasterPageFile="~/Inspections/Masters/Account_Audit.master" AutoEventWireup="true" CodeFile="CivilWork2.aspx.cs" Inherits="Inspections_Audit_Account_Audit" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
      <style type="text/css">
        ul.svertical {
            width: 220px; /* width of menu */
            overflow: auto;
            background: #f4f4f4; /* background of menu */
            margin: 0;
            padding: 0;
            padding-top: 7px; /* top padding */
            list-style-type: none;
        }

            ul.svertical li {
                text-align: right; /* right align menu links */
            }

                ul.svertical li a {
                    position: relative;
                    display: inline-block;
                    text-indent: 5px;
                    overflow: hidden;
                    background: rgb(1, 138, 180); /* initial background color of links */
                    font: bold 16px Germand;
                    text-decoration: none;
                    padding: 5px;
                    margin-bottom: 5px; /* spacing between links */
                    color: White;
                    -moz-box-shadow: inset -7px 0 5px rgba(114,114,114, 0.8); /* inner right shadow added to each link */
                    -webkit-box-shadow: inset -7px 0 5px rgba(114,114,114, 0.8);
                    box-shadow: inset -7px 0 5px rgba(114,114,114, 0.8);
                    -moz-transition: all 0.2s ease-in-out; /* CSS3 transition of hover properties */
                    -webkit-transition: all 0.2s ease-in-out;
                    -o-transition: all 0.2s ease-in-out;
                    -ms-transition: all 0.2s ease-in-out;
                    transition: all 0.2s ease-in-out;
                }

                    ul.svertical li a:hover {
                        padding-right: 30px; /* add right padding to expand link horizontally to the left */
                        color: Black;
                        background: rgb(153,249,75);
                        -moz-box-shadow: inset -3px 0 2px rgba(114,114,114, 0.8); /* contract inner right shadow */
                        -webkit-box-shadow: inset -3px 0 5px rgba(114,114,114, 0.8);
                        box-shadow: inset -3px 0 5px rgba(114,114,114, 0.8);
                    }

                    ul.svertical li a:before { /* CSS generated content: slanted right edge */
                        content: "";
                        position: absolute;
                        left: 0;
                        top: 0;
                        border-style: solid;
                        border-width: 70px 0 0 20px; /* Play around with 1st and 4th value to change slant degree */
                        border-color: transparent transparent transparent #f4f4f4; /* change black to match the background color of the menu UL */
                    }


        .tb6 {
            border: 3px double #CCCCCC;
            width: 230px;
        }

        table, th, td {
            border: 0.5px solid;
            padding-left: 15px;
        }

        .BTNBLUE {
            border: 1px solid #7eb9d0;
            -webkit-border-radius: 3px;
            -moz-border-radius: 3px;
            border-radius: 3px;
            font-size: 12px;
            font-family: arial, helvetica, sans-serif;
            padding: 10px 10px 10px 10px;
            text-decoration: none;
            display: inline-block;
            text-shadow: -1px -1px 0 rgba(0,0,0,0.3);
            font-weight: bold;
            color: #FFFFFF;
            background-color: #a7cfdf;
            background-image: linear-gradient(to bottom, #a7cfdf, #23538a);
        }
    </style>
    <div id="bg">
        <%--<div class="wrap">--%>
        <div >
            <table>
                 <tr>
                    <td colspan="6">

                        <div style="background-color: #66CCFF">
                            <p style="color: #008080; font-size:large; text-align:center">
                                 गोदाम:</p>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        प्लिंथ ऊंचाई
                    </td>
                    <td>
                        <input id="txt_Godown" name="Gname" runat="server" class="text" type="text" />
                    </td>
                    <td>
                        निरीक्षण अधिकारी की टीम
                    </td>
                    <td>
                        <input id="txt_inspection_officer" name="Inspctionsname" runat="server" class="text" type="text" />
                    </td>
                    <td>
                        Remark
                    </td>
                    <td>
                        <asp:TextBox ID="txtGRemark" runat="server" class="text" type="text"></asp:TextBox>
                      
                    </td>   
                   
                </tr>
                <tr>
                    <td>
                        प्लिंथ सुरक्षा
                    </td>
                    <td>
                        <input id="Text1" name="Gname" runat="server" class="text" type="text" />
                    </td>
                    <td>
                        निरीक्षण अधिकारी की टीम
                    </td>
                    <td>
                        <input id="Text2" name="Inspctionsname" runat="server" class="text" type="text" />
                    </td>
                    <td>
                        Remark
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox2" runat="server" class="text" type="text"></asp:TextBox>
                      
                    </td>   
                   
                </tr>
                
             
                <tr>
                    <td></td>
                    <td colspan="3">
                       <%-- <asp:GridView ID="gdstackingdetails" runat="server" AutoGenerateDeleteButton="True"
                            CellPadding="4" ForeColor="#333333" GridLines="None"
                            
                            OnRowCreated="gdstackingdetails_RowCreated" 
                            OnRowDeleting="gdstackingdetails_RowDeleting"
                            >
                            <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                            <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>--%>

                    </td>
                </tr>
                <tr>
                    <td colspan="6">

                        <div style="background-color: #66CCFF">
                            <p style="color: #008080; font-size: large; text-align:center">
                                दीवार की (30 C.M)/ फर्श:</p>
                        </div>
                    </td>
                </tr>

                <tr>
                    <td>
                        फर्श (फ्लोर)
                    </td>
                    <td>
                         <asp:DropDownList ID="ddlfarsh" Height="35px" Class="form-control" runat="server">
                        <asp:ListItem Value="0">-----Select------</asp:ListItem>
                        <asp:ListItem Value="1">पत्थर का फर्श</asp:ListItem>
                        <asp:ListItem Value="2">सीमेंट कंक्रीट</asp:ListItem>
                        <asp:ListItem Value="3">RCC स्टील सहित</asp:ListItem>
                        
                    </asp:DropDownList>
                    </td>
                    <td>
                        निरीक्षण अधिकारी की टीम
                    </td>
                    <td>
                        <input id="Text4" name="Inspctionsname" runat="server" class="text" type="text" />
                    </td>
                    <td>
                        Remark
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox1" runat="server" class="text" type="text"></asp:TextBox>
                      
                    </td>   
                   
                </tr>

                <tr>
                    <td>
                        फर्श की स्थिति
                    </td>
                    <td>
                       <asp:DropDownList ID="ddlfarsstatus" Height="35px" Class="form-control" runat="server">
                        <asp:ListItem Value="0">-----Select------</asp:ListItem>
                        <asp:ListItem Value="1">डेमेज</asp:ListItem>
                        <asp:ListItem Value="2">सही</asp:ListItem>
                    </asp:DropDownList>
                    </td>
                    <td>
                        निरीक्षण अधिकारी की टीम
                    </td>
                    <td>
                        <input id="Text3" name="Inspctionsname" runat="server" class="text" type="text" />
                    </td>
                    <td>
                        Remark
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox5" runat="server" class="text" type="text"></asp:TextBox>
                      
                    </td>   
                   
                </tr>
                

                
                <tr>
                    <td colspan="6">
                        <div style="background-color: #66CCFF">
                            <p style="color: #008080; font-size:large; text-align:center">
                                छत :</p>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        छत का प्रकार :
                    </td>
                    <td>
                         <asp:DropDownList ID="ddlrooftype" Height="35px" Class="form-control" runat="server">
                        <asp:ListItem Value="0">-----Select------</asp:ListItem>
                        <asp:ListItem Value="1"> सीमेंट चादर  की छत</asp:ListItem>
                        <asp:ListItem Value="2">Galvanised चादर छत (...शीट)</asp:ListItem>
                        <asp:ListItem Value="2">जी.आई.शीट</asp:ListItem>
                        <asp:ListItem Value="2">सही</asp:ListItem>
                    </asp:DropDownList></td>
                     <td>
                        छत की स्थिति :
                    </td>  
                        <td>
                            <asp:DropDownList ID="ddlroofstatus" Height="35px" Class="form-control" runat="server">
                        <asp:ListItem Value="0">-----Select------</asp:ListItem>
                        <asp:ListItem Value="1"> क्षतिग्रस्त</asp:ListItem>
                        <asp:ListItem Value="2">लीकेज</asp:ListItem>
                        <asp:ListItem Value="2">सही</asp:ListItem>
                    </asp:DropDownList>
                        </td>
                    
                   <td>
                        Remark
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox6" runat="server" class="text" TextMode="MultiLine" type="text"></asp:TextBox>
                      
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        <div style="background-color: #66CCFF">
                            <p style="color: #008080; font-size: small">
                                 :</p>
                        </div>
                    </td>
                </tr>
              <tr>
                    <td>
                        ट्रस की स्थिति :
                    </td>
                    <td>
                         <asp:DropDownList ID="ddltrusstatus" Height="35px" Class="form-control" runat="server">
                        <asp:ListItem Value="0">-----Select------</asp:ListItem>
                        <asp:ListItem Value="1">Patent</asp:ListItem>
                        <asp:ListItem Value="2">Non Patent</asp:ListItem>
                        
                    </asp:DropDownList>
                       
                    </td>
                    <td>
                       निरीक्षण अधिकारी की टीम:
                    </td>
                    <td>
                        <asp:DropDownList ID="DropDownList1" Height="35px" Class="form-control" runat="server">
                        <asp:ListItem Value="0">-----Select------</asp:ListItem>
                        <asp:ListItem Value="1">हाँ</asp:ListItem>
                        <asp:ListItem Value="2">नहीं</asp:ListItem>
                        
                    </asp:DropDownList>

                    </td>
                    <td>
                      रिमार्क
                        <%--<br /> प्रयास का विवरण :--%>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox3" runat="server" class="text" TextMode="MultiLine" type="text"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                      फर्श से ट्रस की उचाई:
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox7" runat="server" class="text" type="text"></asp:TextBox>
                    </td>
                   
                <%--</tr>
                   
              <tr>--%>
                    <td>
                        बराण्डा (प्लेटफार्म) :
                    </td>
                    <td>
                         <asp:DropDownList ID="DropDownList2" Height="35px" Class="form-control" runat="server">
                        <asp:ListItem Value="0">-----Select------</asp:ListItem>
                        <asp:ListItem Value="1">हाँ</asp:ListItem>
                        <asp:ListItem Value="2">नहीं</asp:ListItem>
                    </asp:DropDownList>
                       
                    </td>                   
                    <td>
                      रिमार्क
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox4" runat="server" class="text" TextMode="MultiLine" type="text"></asp:TextBox>
                    </td>
                </tr>

                <tr>
                    <td>
                      शटर की स्थिति गोदाम के:
                    </td>
                    <td>
                        <asp:DropDownList ID="DropDownList4" Height="35px" Class="form-control" runat="server">
                        <asp:ListItem Value="0">-----Select------</asp:ListItem>
                        <asp:ListItem Value="1">1 साईड</asp:ListItem>
                        <asp:ListItem Value="2">2 साईड</asp:ListItem>
                    </asp:DropDownList>
                    </td>
                   
                <%--</tr>
                   
              <tr>--%>
                    <td>
                        शटर (जाली वाला) :
                    </td>
                    <td>
                         <asp:DropDownList ID="DropDownList3" Height="35px" Class="form-control" runat="server">
                        <asp:ListItem Value="0">-----Select------</asp:ListItem>
                        <asp:ListItem Value="1">हाँ</asp:ListItem>
                        <asp:ListItem Value="2">नहीं</asp:ListItem>
                    </asp:DropDownList>
                       
                    </td> 
                     <td>
                        शटर की संख्या :
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox8" runat="server" class="text" type="text"></asp:TextBox>
                       
                    </td> 
                    
                </tr>
                <tr>
                    <td>
                        एरियेशन :
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox10" runat="server" class="text" type="text"></asp:TextBox>
                        </td> 
                    <td>
                         <asp:DropDownList ID="DropDownList5" Height="35px" Class="form-control" runat="server">
                        <asp:ListItem Value="0">-----Select------</asp:ListItem>
                        <asp:ListItem Value="1">पर्याप्त</asp:ListItem>
                        <asp:ListItem Value="2">अपर्याप्त</asp:ListItem>
                    </asp:DropDownList>
                       
                    </td> 
                    <td>
                      रिमार्क
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox9" runat="server" class="text" TextMode="MultiLine" type="text"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        <div style="background-color: #66CCFF">
                            <p style="color: #008080; font-size: small">
                               वेंटीलैटर की स्थिति  :</p>
                        </div>
                    </td>
                </tr>
                <tr>
                     <td>
                        टॉप वेंटीलैटर की संख्या :
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox11" runat="server" class="text" type="text"></asp:TextBox>
                       
                    </td> 
                    <td>
                        निरिक्षण आधिकारी की टीप :
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox12" runat="server" class="text" type="text"></asp:TextBox>
                       
                    </td> 
                     <td>
                      रिमार्क
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox13" runat="server" class="text" TextMode="MultiLine" type="text"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    
                    <td>
                        लोअर वेंटीलैटर की संख्या :
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox15" runat="server" class="text" type="text"></asp:TextBox>
                       
                    </td> 
                    <td>
                        निरिक्षण आधिकारी की टीप :
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox16" runat="server" class="text" type="text"></asp:TextBox>
                       
                    </td> 
                     <td>
                      रिमार्क
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox20" runat="server" class="text" TextMode="MultiLine" type="text"></asp:TextBox>
                    </td> 
                </tr>
                <tr>
                     
                    <td>
                        टर्बो वेंटीलैटर की संख्या :
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox19" runat="server" class="text" type="text"></asp:TextBox>
                       
                    </td> 
                     <td>
                        निरिक्षण आधिकारी की टीप :
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox14" runat="server" class="text" type="text"></asp:TextBox>
                       
                    </td> 
                     <td>
                      रिमार्क
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox21" runat="server" class="text" TextMode="MultiLine" type="text"></asp:TextBox>
                    </td>
                </tr>

                 <tr>
                    <td colspan="6">
                        <div style="background-color: #66CCFF">
                            <p style="color: #008080; font-size: small">
                               परिसर का ड्रेनेज सिस्टम(गोदाम के आसपास)  :</p>
                        </div>
                    </td>
                </tr>
                <tr>
                     <td>
                        पानी भराव की स्थिति  :
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox17" runat="server" class="text" type="text"></asp:TextBox>
                       
                    </td> 
                    <td>
                        निरिक्षण आधिकारी की टीप :
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox18" runat="server" class="text" type="text"></asp:TextBox>
                       
                    </td> 
                     <td>
                      रिमार्क
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox22" runat="server" class="text" TextMode="MultiLine" type="text"></asp:TextBox>
                    </td>
                </tr>

                <tr>
                     <td>
                        नाली  :
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox23" runat="server" class="text" type="text"></asp:TextBox>
                       
                    </td> 
                    <td>
                        निरिक्षण आधिकारी की टीप :
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox24" runat="server" class="text" type="text"></asp:TextBox>
                       
                    </td> 
                     <td>
                      रिमार्क
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox25" runat="server" class="text" TextMode="MultiLine" type="text"></asp:TextBox>
                    </td>
                </tr>

                  <tr>
                    <td colspan="6">
                        <div style="background-color: #66CCFF">
                            <p style="color: #008080; font-size: small">
                               बाउंड्रीवाल की स्थिति  :</p>
                        </div>
                    </td>
                </tr>
                <tr>
                     <td>
                        बाउंड्रीवाल का प्रकार :
                    </td>
                    <td>
                       <asp:DropDownList ID="DropDownList6" Height="35px" Class="form-control" runat="server">
                        <asp:ListItem Value="0">-----Select------</asp:ListItem>
                        <asp:ListItem Value="1">जाली फैंसिंग</asp:ListItem>
                        <asp:ListItem Value="2">बाउंड्रीवाल</asp:ListItem>
                        <asp:ListItem Value="2">कटीली तार की फैंसिंग</asp:ListItem>
                        <asp:ListItem Value="2">अपर्याप्त</asp:ListItem>
                    </asp:DropDownList>
                    </td> 
                    <td>
                        निरिक्षण आधिकारी की टीप :
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox27" runat="server" class="text" type="text"></asp:TextBox>
                       
                    </td> 
                     <td>
                      रिमार्क
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox28" runat="server" class="text" TextMode="MultiLine" type="text"></asp:TextBox>
                    </td>
                </tr>

                 <tr>
                    <td colspan="6">
                        <div style="background-color: #66CCFF">
                            <p style="color: #008080; font-size: small">
                               मेन गेट(मुख्या द्वार) की स्थिति  :</p>
                        </div>
                    </td>
                </tr>
                <tr>
                     <td>
                        मेन गेट की संख्या :
                    </td>
                    <td>
                       <asp:TextBox ID="TextBox30" runat="server" class="text" type="text"></asp:TextBox>
                    </td>
                    <td>
                        मेन गेट की स्थिति :
                    </td>
                    <td>
                       <asp:TextBox ID="TextBox31" runat="server" class="text" type="text"></asp:TextBox>
                    </td>
                    <td>
                        निरिक्षण आधिकारी की टीप :
                    </td>
                    <td >
                        <asp:TextBox ID="TextBox26" runat="server" class="text" type="text"></asp:TextBox>
                       
                    </td> 
                    </tr>
                <tr>
                     <td>
                      रिमार्क
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox32" runat="server" class="text" TextMode="MultiLine" type="text"></asp:TextBox>
                    </td>
                </tr>
                 <tr>
                    <td colspan="6">
                        <div style="background-color: #66CCFF">
                            <p style="color: #008080; font-size: small">
                               रोड का प्रकार  :</p>
                        </div>
                    </td>
                </tr>
                <tr>
                     <td>
                       रोड का प्रकार :
                    </td>
                    <td>
                       <asp:DropDownList ID="DropDownList7" Height="35px" Class="form-control" runat="server">
                        <asp:ListItem Value="0">-----Select------</asp:ListItem>
                        <asp:ListItem Value="1">डब्ल्यु बी.एम.</asp:ListItem>
                        <asp:ListItem Value="2">सी.सी.</asp:ListItem>
                        <asp:ListItem Value="2">टार रोड</asp:ListItem>
                    </asp:DropDownList>
                    </td>
                    <td>
                       निरिक्षण आधिकारी की टीप :
                    </td>
                    <td>
                       <asp:DropDownList ID="DropDownList8" Height="35px" Class="form-control" runat="server">
                        <asp:ListItem Value="0">-----Select------</asp:ListItem>
                        <asp:ListItem Value="1">हाँ</asp:ListItem>
                        <asp:ListItem Value="2">नहीं</asp:ListItem>
                    </asp:DropDownList>
                    </td>
                    <td>
                        रोड की स्थिति :
                    </td>
                    <td>
                        <asp:DropDownList ID="DropDownList9" Height="35px" Class="form-control" runat="server">
                        <asp:ListItem Value="0">-----Select------</asp:ListItem>
                        <asp:ListItem Value="1">उत्तम</asp:ListItem>
                        <asp:ListItem Value="2">औसत</asp:ListItem>
                        <asp:ListItem Value="2">क्षतिग्रस्त</asp:ListItem>
                        <asp:ListItem Value="2">दयनीय</asp:ListItem>
                    </asp:DropDownList>
                    </td>
                  
                    </tr>
                <tr>
                     <td>
                      रिमार्क
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox29" runat="server" class="text" TextMode="MultiLine" type="text"></asp:TextBox>
                    </td>
                </tr>


                 <tr>
                    <td colspan="6">
                        <div style="background-color: #66CCFF">
                            <p style="color: #008080; font-size: small">
                               अग्नि सुरक्षा की वियावस्था  :</p>
                        </div>
                    </td>
                </tr>
                <tr>
                     <td>
                       अग्नि शमंक यंत्र की संख्या :
                    </td>
                    <td>
                       <asp:DropDownList ID="DropDownList10" Height="35px" Class="form-control" runat="server">
                        <asp:ListItem Value="0">-----Select------</asp:ListItem>
                        <asp:ListItem Value="1">डब्ल्यु बी.एम.</asp:ListItem>
                        <asp:ListItem Value="2">सी.सी.</asp:ListItem>
                        <asp:ListItem Value="2">टार रोड</asp:ListItem>
                    </asp:DropDownList>
                    </td>
                    <td>
                       अग्नि रिफिल की तारिक :
                    </td>
                    <td>
                       <asp:DropDownList ID="DropDownList11" Height="35px" Class="form-control" runat="server">
                        <asp:ListItem Value="0">-----Select------</asp:ListItem>
                        <asp:ListItem Value="1">हाँ</asp:ListItem>
                        <asp:ListItem Value="2">नहीं</asp:ListItem>
                    </asp:DropDownList>
                    </td>
                    <td>
                        वैधता तिथि :
                    </td>
                    <td>
                        <asp:DropDownList ID="DropDownList12" Height="35px" Class="form-control" runat="server">
                        <asp:ListItem Value="0">-----Select------</asp:ListItem>
                        <asp:ListItem Value="1">उत्तम</asp:ListItem>
                        <asp:ListItem Value="2">औसत</asp:ListItem>
                        <asp:ListItem Value="2">क्षतिग्रस्त</asp:ListItem>
                        <asp:ListItem Value="2">दयनीय</asp:ListItem>
                    </asp:DropDownList>
                    </td>
                  
                    </tr>
                <tr>
                     <td>
                      रेत की बाल्टी की संख्या
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox33" runat="server" class="text" TextMode="MultiLine" type="text"></asp:TextBox>
                    </td>
                    <td>
                      गोदाम में आवश्यक आपातकालीन दूरभाष नम्बर लिखे 
                    </td>
                    <td>
                         <asp:DropDownList ID="DropDownList13" Height="35px" Class="form-control" runat="server">
                        <asp:ListItem Value="0">-----Select------</asp:ListItem>
                        <asp:ListItem Value="1">हाँ</asp:ListItem>
                        <asp:ListItem Value="2">नहीं</asp:ListItem>
                    </asp:DropDownList>
                    </td>
                     <td>
                      रिमार्क
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox34" runat="server" class="text" TextMode="MultiLine" type="text"></asp:TextBox>
                    </td>
                </tr>


                 <tr>
                    <td colspan="6">
                        <div style="background-color: #66CCFF">
                            <p style="color: #008080; font-size: small">
                               वेब्रिज़ (धर्मकाँटा)  :</p>
                        </div>
                    </td>
                </tr>
                <tr>
                     <td>
                       परिसर में धर्मकाँटा हें या नहीं  :
                    </td>
                    <td>
                       <asp:DropDownList ID="DropDownList14" Height="35px" Class="form-control" runat="server">
                        <asp:ListItem Value="0">-----Select------</asp:ListItem>
                        <asp:ListItem Value="1">डब्ल्यु बी.एम.</asp:ListItem>
                        <asp:ListItem Value="2">सी.सी.</asp:ListItem>
                        <asp:ListItem Value="2">टार रोड</asp:ListItem>
                    </asp:DropDownList>
                    </td>
                    <td>
                       क्षमता (मे.टन.):
                    </td>
                    <td>
                       <asp:DropDownList ID="DropDownList15" Height="35px" Class="form-control" runat="server">
                        <asp:ListItem Value="0">-----Select------</asp:ListItem>
                        <asp:ListItem Value="1">हाँ</asp:ListItem>
                        <asp:ListItem Value="2">नहीं</asp:ListItem>
                    </asp:DropDownList>
                    </td>
                    <td>
                       केलीब्रेशन का दिनांक  :
                    </td>
                    <td>
                        <asp:DropDownList ID="DropDownList16" Height="35px" Class="form-control" runat="server">
                        <asp:ListItem Value="0">-----Select------</asp:ListItem>
                        <asp:ListItem Value="1">उत्तम</asp:ListItem>
                        <asp:ListItem Value="2">औसत</asp:ListItem>
                        <asp:ListItem Value="2">क्षतिग्रस्त</asp:ListItem>
                        <asp:ListItem Value="2">दयनीय</asp:ListItem>
                    </asp:DropDownList>
                    </td>
                  
                    </tr>
                <tr>
                     <td>
                      प्रमाण पत्र का क्र.
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox35" runat="server" class="text" TextMode="MultiLine" type="text"></asp:TextBox>
                    </td>
                    <td>
                      की दिनांक (वैधता तिथि) 
                    </td>
                    <td>
                         <asp:DropDownList ID="DropDownList17" Height="35px" Class="form-control" runat="server">
                        <asp:ListItem Value="0">-----Select------</asp:ListItem>
                        <asp:ListItem Value="1">हाँ</asp:ListItem>
                        <asp:ListItem Value="2">नहीं</asp:ListItem>
                    </asp:DropDownList>
                    </td>
                     <td>
                      धर्मकाँटा परिसर में नहीं हैं तो परिसर से दुरी
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox36" runat="server" class="text" TextMode="MultiLine" type="text"></asp:TextBox>
                    </td>
                </tr>

                 <tr>
                    <td colspan="6">
                        <div style="background-color: #66CCFF">
                            <p style="color: #008080; font-size: small">
                               कार्यालय क्षेत्र पर्याप्त/अपर्याप्त  :</p>
                        </div>
                    </td>
                </tr>
                <tr>
                     <td>
                       छत की स्थिति  :
                    </td>
                    <td>
                       <asp:DropDownList ID="DropDownList18" Height="35px" Class="form-control" runat="server">
                        <asp:ListItem Value="0">-----Select------</asp:ListItem>
                        <asp:ListItem Value="1">डब्ल्यु बी.एम.</asp:ListItem>
                        <asp:ListItem Value="2">सी.सी.</asp:ListItem>
                        <asp:ListItem Value="2">टार रोड</asp:ListItem>
                    </asp:DropDownList>
                    </td>
                    <td>
                       फ्लोअर की स्थिति:
                    </td>
                    <td>
                       <asp:DropDownList ID="DropDownList19" Height="35px" Class="form-control" runat="server">
                        <asp:ListItem Value="0">-----Select------</asp:ListItem>
                        <asp:ListItem Value="1">हाँ</asp:ListItem>
                        <asp:ListItem Value="2">नहीं</asp:ListItem>
                    </asp:DropDownList>
                    </td>
                    <td>
                       खिड़की दरवाजे की स्थिति  :
                    </td>
                    <td>
                        <asp:DropDownList ID="DropDownList20" Height="35px" Class="form-control" runat="server">
                        <asp:ListItem Value="0">-----Select------</asp:ListItem>
                        <asp:ListItem Value="1">उत्तम</asp:ListItem>
                        <asp:ListItem Value="2">औसत</asp:ListItem>
                        <asp:ListItem Value="2">क्षतिग्रस्त</asp:ListItem>
                        <asp:ListItem Value="2">दयनीय</asp:ListItem>
                    </asp:DropDownList>
                    </td>
                  
                    </tr>
                <tr>
                     <td>
                      शोचालय की स्थिति
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox37" runat="server" class="text" TextMode="MultiLine" type="text"></asp:TextBox>
                    </td>
                    <td>
                      विद्धुत वियावस्था की स्थिति
                    </td>
                    <td>
                         <asp:DropDownList ID="DropDownList21" Height="35px" Class="form-control" runat="server">
                        <asp:ListItem Value="0">-----Select------</asp:ListItem>
                        <asp:ListItem Value="1">हाँ</asp:ListItem>
                        <asp:ListItem Value="2">नहीं</asp:ListItem>
                    </asp:DropDownList>
                    </td>
                     <td>
                      पुताई की स्थिति
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox38" runat="server" class="text" TextMode="MultiLine" type="text"></asp:TextBox>
                    </td>
                </tr>

                <tr>
                     <td>
                      पेंटिंग की स्थिति
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox39" runat="server" class="text" TextMode="MultiLine" type="text"></asp:TextBox>
                    </td>
                    <td>
                      रिमार्क
                    </td>
                    <td>
                         <asp:DropDownList ID="DropDownList22" Height="35px" Class="form-control" runat="server">
                        <asp:ListItem Value="0">-----Select------</asp:ListItem>
                        <asp:ListItem Value="1">हाँ</asp:ListItem>
                        <asp:ListItem Value="2">नहीं</asp:ListItem>
                    </asp:DropDownList>
                    </td>
                    
                </tr>
                   <tr>
                    <td>
                    
                    </td>
                    <td>
                        <%--<asp:Button ID="btnsubmit" runat="server" Text="Submit" CssClass="BTNBLUE" OnClick="btnsubmit_Click" ></asp:Button>--%>
                        <asp:Label ID="lblmsg" runat="server"></asp:Label>
                    &nbsp;
                       <%-- <asp:LinkButton ID="LinkButton2" runat="server" PostBackUrl="~/Inspection/OldBranchInsp.aspx">Old Audits</asp:LinkButton--%>
                    </td>
                </tr>

            </table>


        </div>
    </div>
</asp:Content>

