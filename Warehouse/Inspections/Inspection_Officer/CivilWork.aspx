<%@ Page Title="" Language="C#" MasterPageFile="~/Inspections/Masters/Inspection_Officer.master" AutoEventWireup="true" CodeFile="CivilWork.aspx.cs" Inherits="Inspections_Inspection_Officer_CivilWork" %>

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
                                 :</p>
                        </div>
                    </td>
                </tr>
                 <tr>
                    <td>
                        Branch
                    </td>
                    <td>
                          <asp:DropDownList ID="ddlbranch" runat="server" AutoPostBack="false" class="form-control" >
                              <%--OnSelectedIndexChanged="ddlbranch_SelectedIndexChanged"--%>
                    </asp:DropDownList>
                       
                        <%--<input id="txtG_plinth" name="Gname" runat="server" class="text" type="text" />--%>
                    </td>
                    <td>
                        Godown
                    </td>
                    <td>
                       <asp:DropDownList ID="ddl_gdwn" runat="server" AutoPostBack="false" class="form-control">
                    </asp:DropDownList>
                        <%--<input id="txtG_InsOfficerTeam" name="Inspctionsname" runat="server" class="text" type="text" />--%>
                    </td>
                    <%--<td>
                        Remark
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox3" runat="server" class="text" type="text"></asp:TextBox>
                      
                    </td>   --%>
                   
                </tr>
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
                        <asp:TextBox ID="txtGhight_plinth" runat="server" class="text" type="text"></asp:TextBox>
                        <%--<input id="txtG_plinth" name="Gname" runat="server" class="text" type="text" />--%>
                    </td>
                    <td>
                        निरीक्षण अधिकारी की टीम
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtGhight_InsOfficerTeam" class="text" type="text" ></asp:TextBox>
                        <%--<input id="txtG_InsOfficerTeam" name="Inspctionsname" runat="server" class="text" type="text" />--%>
                    </td>
                    <td>
                        Remark
                    </td>
                    <td>
                        <asp:TextBox ID="txtGhight_Remark" runat="server" class="text" type="text"></asp:TextBox>
                      
                    </td>   
                   
                </tr>
                <tr>
                    <td>
                        प्लिंथ सुरक्षा
                    </td>
                    <td>
                        <%--<input id="Text1" name="Gname" runat="server" class="text" type="text" />--%>
                        <asp:TextBox ID="txtGprotec_Plinthprotection" runat="server"  class="text" type="text"></asp:TextBox>
                    </td>
                    <td>
                        निरीक्षण अधिकारी की टीम
                    </td>
                    <td>
                        <%--<input id="Text2" name="Inspctionsname" runat="server" class="text" type="text" />--%>
                        <asp:TextBox ID="txtGprotec_Insofficerteam" runat="server" class="text" type="text"></asp:TextBox>
                    </td>
                    <td>
                        Remark
                    </td>
                    <td>
                        <asp:TextBox ID="txtGprotec_Remark" runat="server" class="text" type="text"></asp:TextBox>
                      
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
                         <asp:DropDownList ID="ddlWallFloors_Floors" Height="35px" Class="form-control" runat="server">
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
                        <asp:TextBox ID="txtWallFloors_InsOfficerTeam" runat="server" class="text" type="text"></asp:TextBox>
                        <%--<input id="Text4" name="Inspctionsname" runat="server" class="text" type="text" />--%>
                    </td>
                    <td>
                        Remark
                    </td>
                    <td>
                        <asp:TextBox ID="txtWallFloors_Remark" runat="server" class="text" type="text"></asp:TextBox>
                      
                    </td>   
                   
                </tr>

                <tr>
                    <td>
                        फर्श की स्थिति
                    </td>
                    <td>
                       <asp:DropDownList ID="ddlFloorPos_FloorPosition" Height="35px" Class="form-control" runat="server">
                        <asp:ListItem Value="0">-----Select------</asp:ListItem>
                        <asp:ListItem Value="1">डेमेज</asp:ListItem>
                        <asp:ListItem Value="2">सही</asp:ListItem>
                    </asp:DropDownList>
                    </td>
                    <td>
                        निरीक्षण अधिकारी की टीम
                    </td>
                    <td>
                        <asp:TextBox ID="txtFloorPos_InsOfficerTeam" runat="server" class="text" type="text" ></asp:TextBox>
                        <%--<input id="Text3" name="Inspctionsname" runat="server" class="text" type="text" />--%>
                    </td>
                    <td>
                        Remark
                    </td>
                    <td>
                        <asp:TextBox ID="txtFloorPos_Remark" runat="server" class="text" type="text"></asp:TextBox>
                      
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
                         <asp:DropDownList ID="ddlceilingrooftype_RoofType" Height="35px" Class="form-control" runat="server">
                        <asp:ListItem Value="0">-----Select------</asp:ListItem>
                        <asp:ListItem Value="1"> सीमेंट चादर  की छत</asp:ListItem>
                        <asp:ListItem Value="2">Galvanised चादर छत (...शीट)</asp:ListItem>
                        <asp:ListItem Value="2">जी.आई.शीट</asp:ListItem>
                        <asp:ListItem Value="2">सही</asp:ListItem>
                    </asp:DropDownList></td>
                     <td>
                        छत की स्थिति :roof position
                    </td>  
                        <td>
                            <asp:DropDownList ID="ddlceilingrooftype_RoofPosition" Height="35px" Class="form-control" runat="server">
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
                        <asp:TextBox ID="txtceilingrooftype_Remak" runat="server" class="text" TextMode="MultiLine" type="text"></asp:TextBox>
                      
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        <div style="background-color: #66CCFF">
                            <p style="color: #008080; font-size:large; text-align:center">
                                ट्रस :</p>
                        </div>
                    </td>
                </tr>
              <tr>
                    <td>
                        ट्रस की स्थिति :
                    </td>
                    <td>
                         <asp:DropDownList ID="ddltrussposition_TrussPosition" Height="35px" Class="form-control" runat="server">
                        <asp:ListItem Value="0">-----Select------</asp:ListItem>
                        <asp:ListItem Value="1">Patent</asp:ListItem>
                        <asp:ListItem Value="2">Non Patent</asp:ListItem>
                        
                    </asp:DropDownList>
                       
                    </td>
                    <td>
                       निरीक्षण अधिकारी की टीम:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddltrussposition_InsOfficerTeam" Height="35px" Class="form-control" runat="server">
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
                        <asp:TextBox ID="txttrussposition_Remark" runat="server" class="text" TextMode="MultiLine" type="text"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                      फर्श से ट्रस की उचाई:
                    </td>
                    <td>
                        <asp:TextBox ID="txttrusshegiht_TrussHeightFloor" runat="server" class="text" type="text"></asp:TextBox>
                    </td>
                   
                <%--</tr>
                   
              <tr>--%>
                    <td>
                        बराण्डा (प्लेटफार्म) :
                    </td>
                    <td>
                         <asp:DropDownList ID="txttrusshegiht_Baranda" Height="35px" Class="form-control" runat="server">
                        <asp:ListItem Value="0">-----Select------</asp:ListItem>
                        <asp:ListItem Value="1">हाँ</asp:ListItem>
                        <asp:ListItem Value="2">नहीं</asp:ListItem>
                    </asp:DropDownList>
                       
                    </td>                   
                    <td>
                      रिमार्क
                    </td>
                    <td>
                        <asp:TextBox ID="txttrusshegiht_Remark" runat="server" class="text" TextMode="MultiLine" type="text"></asp:TextBox>
                    </td>
                </tr>

                <tr>
                    <td>
                      शटर की स्थिति गोदाम के:
                    </td>
                    <td>
                        <asp:DropDownList ID="txtShutterposition_ShutterPosition" Height="35px" Class="form-control" runat="server">
                        <asp:ListItem Value="0">-----Select------</asp:ListItem>
                        <asp:ListItem Value="1">1 साईड</asp:ListItem>
                        <asp:ListItem Value="2">2 साईड</asp:ListItem>
                    </asp:DropDownList>
                    </td>
                   
                <%--</tr>
                   
              <tr>--%>
                    <td>
                        शटर (जाली वाला) :shutter (forged)
                    </td>
                    <td>
                         <asp:DropDownList ID="txtShutterposition_ShutterForged" Height="35px" Class="form-control" runat="server">
                        <asp:ListItem Value="0">-----Select------</asp:ListItem>
                        <asp:ListItem Value="1">हाँ</asp:ListItem>
                        <asp:ListItem Value="2">नहीं</asp:ListItem>
                    </asp:DropDownList>
                       
                    </td> 
                     <td>
                        शटर की संख्या :
                    </td>
                    <td>
                        <asp:TextBox ID="txtShutterposition_NumberShutters" runat="server" class="text" type="text"></asp:TextBox>
                       
                    </td> 
                    
                </tr>
                <tr>
                    <td>
                        एरियेशन :
                    </td>
                    <td>
                        <asp:TextBox ID="txtShutterposition_Erosion" runat="server" class="text" type="text"></asp:TextBox>
                        </td> 
                    <td>
                         <asp:DropDownList ID="ddlShutterposition_Erosion" Height="35px" Class="form-control" runat="server">
                        <asp:ListItem Value="0">-----Select------</asp:ListItem>
                        <asp:ListItem Value="1">पर्याप्त</asp:ListItem>
                        <asp:ListItem Value="2">अपर्याप्त</asp:ListItem>
                    </asp:DropDownList>
                       
                    </td> 
                    <td>
                      रिमार्क
                    </td>
                    <td>
                        <asp:TextBox ID="txtShutterposition_Remark" runat="server" class="text" TextMode="MultiLine" type="text"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        <div style="background-color: #66CCFF">
                            <p style="color: #008080; font-size: large; text-align:center">
                               वेंटीलैटर की स्थिति  :</p>
                        </div>
                    </td>
                </tr>
                <tr>
                     <td>
                        टॉप वेंटीलैटर की संख्या :
                    </td>
                    <td>
                        <asp:TextBox ID="txtconditionventilator_NuTopVentilators" runat="server" class="text" type="text"></asp:TextBox>
                       
                    </td> 
                    <td>
                        निरिक्षण आधिकारी की टीप :
                    </td>
                    <td>
                        <asp:TextBox ID="txtconditionventilator_InsOfficerTeam" runat="server" class="text" type="text"></asp:TextBox>
                       
                    </td> 
                     <td>
                      रिमार्क
                    </td>
                    <td>
                        <asp:TextBox ID="txtconditionventilator_Remark" runat="server" class="text" TextMode="MultiLine" type="text"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    
                    <td>
                        लोअर वेंटीलैटर की संख्या :
                    </td>
                    <td>
                        <asp:TextBox ID="txtlowerventilators_NuLowerVen" runat="server" class="text" type="text"></asp:TextBox>
                       
                    </td> 
                    <td>
                        निरिक्षण आधिकारी की टीप :
                    </td>
                    <td>
                        <asp:TextBox ID="txtlowerventilators_InsOfficerTeam" runat="server" class="text" type="text"></asp:TextBox>
                       
                    </td> 
                     <td>
                      रिमार्क
                    </td>
                    <td>
                        <asp:TextBox ID="txtlowerventilators_Remark" runat="server" class="text" TextMode="MultiLine" type="text"></asp:TextBox>
                    </td> 
                </tr>
                <tr>
                     
                    <td>
                        टर्बो वेंटीलैटर की संख्या :
                    </td>
                    <td>
                        <asp:TextBox ID="txtTurboventilator_NuTurboVen" runat="server" class="text" type="text"></asp:TextBox>
                       
                    </td> 
                     <td>
                        निरिक्षण आधिकारी की टीप :
                    </td>
                    <td>
                        <asp:TextBox ID="txtTurboventilator_InsOfficerTeam" runat="server" class="text" type="text"></asp:TextBox>
                       
                    </td> 
                     <td>
                      रिमार्क
                    </td>
                    <td>
                        <asp:TextBox ID="txtTurboventilator_Remark" runat="server" class="text" TextMode="MultiLine" type="text"></asp:TextBox>
                    </td>
                </tr>

                 <tr>
                    <td colspan="6">
                        <div style="background-color: #66CCFF">
                            <p style="color: #008080; font-size:large; text-align:center">
                               परिसर का ड्रेनेज सिस्टम(गोदाम के आसपास)  :</p>
                        </div>
                    </td>
                </tr>
                <tr>
                     <td>
                        पानी भराव की स्थिति  :
                    </td>
                    <td>
                        <asp:TextBox ID="txtDrainage_WaterFillingPosition" runat="server" class="text" type="text"></asp:TextBox>
                       
                    </td> 
                    <td>
                        निरिक्षण आधिकारी की टीप :
                    </td>
                    <td>
                        <asp:TextBox ID="txtDrainage_InsOfficerTeam" runat="server" class="text" type="text"></asp:TextBox>
                       
                    </td> 
                     <td>
                      रिमार्क
                    </td>
                    <td>
                        <asp:TextBox ID="txtDrainage_Remark" runat="server" class="text" TextMode="MultiLine" type="text"></asp:TextBox>
                    </td>
                </tr>

                <tr>
                     <td>
                        नाली  :
                    </td>
                    <td>
                        <asp:TextBox ID="txtDrain_Drain" runat="server" class="text" type="text"></asp:TextBox>
                       
                    </td> 
                    <td>
                        निरिक्षण आधिकारी की टीप :
                    </td>
                    <td>
                        <asp:TextBox ID="txtDrain_InsOfficerTeam" runat="server" class="text" type="text"></asp:TextBox>
                       
                    </td> 
                     <td>
                      रिमार्क
                    </td>
                    <td>
                        <asp:TextBox ID="txtDrain_Remark" runat="server" class="text" TextMode="MultiLine" type="text"></asp:TextBox>
                    </td>
                </tr>

                  <tr>
                    <td colspan="6">
                        <div style="background-color: #66CCFF">
                            <p style="color: #008080; font-size: large; text-align:center">
                               बाउंड्रीवाल की स्थिति  :</p>
                        </div>
                    </td>
                </tr>
                <tr>
                     <td>
                        बाउंड्रीवाल का प्रकार :
                    </td>
                    <td>
                       <asp:DropDownList ID="ddlboundary_BoundaryType" Height="35px" Class="form-control" runat="server">
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
                        <asp:TextBox ID="txtboundary_InsOfficerTeam" runat="server" class="text" type="text"></asp:TextBox>
                       
                    </td> 
                     <td>
                      रिमार्क
                    </td>
                    <td>
                        <asp:TextBox ID="txtboundary_Remark" runat="server" class="text" TextMode="MultiLine" type="text"></asp:TextBox>
                    </td>
                </tr>

                 <tr>
                    <td colspan="6">
                        <div style="background-color: #66CCFF">
                            <p style="color: #008080; font-size: large; text-align:center">
                               मेन गेट(मुख्या द्वार) की स्थिति  :</p>
                        </div>
                    </td>
                </tr>
                <tr>
                     <td>
                        मेन गेट की संख्या :
                    </td>
                    <td>
                       <asp:TextBox ID="txtMainGate_MainGateNo" runat="server" class="text" type="text"></asp:TextBox>
                    </td>
                    <td>
                        मेन गेट की स्थिति :
                    </td>
                    <td>
                       <asp:TextBox ID="txtMainGate_MainGateposition" runat="server" class="text" type="text"></asp:TextBox>
                    </td>
                    <td>
                        निरिक्षण आधिकारी की टीप :
                    </td>
                    <td >
                        <asp:TextBox ID="txtMainGate_InsOfficerTeam" runat="server" class="text" type="text"></asp:TextBox>
                       
                    </td> 
                    </tr>
                <tr>
                     <td>
                      रिमार्क
                    </td>
                    <td>
                        <asp:TextBox ID="txtMainGate_Remark" runat="server" class="text" TextMode="MultiLine" type="text"></asp:TextBox>
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
                       <asp:DropDownList ID="ddlRoadType_RoadType" Height="35px" Class="form-control" runat="server">
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
                       <asp:DropDownList ID="ddlRoadType_InsOfficerTeam" Height="35px" Class="form-control" runat="server">
                        <asp:ListItem Value="0">-----Select------</asp:ListItem>
                        <asp:ListItem Value="1">हाँ</asp:ListItem>
                        <asp:ListItem Value="2">नहीं</asp:ListItem>
                    </asp:DropDownList>
                    </td>
                    <td>
                        रोड की स्थिति :
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlRoadType_RoadCondition" Height="35px" Class="form-control" runat="server">
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
                        <asp:TextBox ID="txtRoadType_Remark" runat="server" class="text" TextMode="MultiLine" type="text"></asp:TextBox>
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
                        <asp:TextBox ID="txtfiresafsys_NuFireEx" runat="server" class="text" type="text"></asp:TextBox>

                      <%-- <asp:DropDownList ID="ddlfiresafsys_NuFireEx" Height="35px" Class="form-control" runat="server">
                        <asp:ListItem Value="0">-----Select------</asp:ListItem>
                        <asp:ListItem Value="1">डब्ल्यु बी.एम.</asp:ListItem>
                        <asp:ListItem Value="2">सी.सी.</asp:ListItem>
                        <asp:ListItem Value="2">टार रोड</asp:ListItem>
                    </asp:DropDownList>--%>
                    </td>
                    <td>
                       अग्नि रिफिल की तारिक :
                    </td>
                    <td>
                       <asp:DropDownList ID="ddlfiresafsys_FireRefillDate" Height="35px" Class="form-control" runat="server">
                        <asp:ListItem Value="0">-----Select------</asp:ListItem>
                        <asp:ListItem Value="1">हाँ</asp:ListItem>
                        <asp:ListItem Value="2">नहीं</asp:ListItem>
                    </asp:DropDownList>
                    </td>
                    <td>
                        वैधता तिथि :validity date
                    </td>
                    <td>
                        <%--<asp:DropDownList ID="ddlfiresafsys_ValidityDate" Height="35px" Class="form-control" runat="server">
                        <asp:ListItem Value="0">-----Select------</asp:ListItem>
                        <asp:ListItem Value="1">उत्तम</asp:ListItem>
                        <asp:ListItem Value="2">औसत</asp:ListItem>
                        <asp:ListItem Value="2">क्षतिग्रस्त</asp:ListItem>
                        <asp:ListItem Value="2">दयनीय</asp:ListItem>
                    </asp:DropDownList>--%>
                        <asp:TextBox ID="txtfiresafsys_ValidityDate" runat="server" class="text" placholder="DD/MM/YYYY"  type="text"></asp:TextBox>

                    </td>
                  
                    </tr>
                <tr>
                     <td>
                      रेत की बाल्टी की संख्या
                    </td>
                    <td>
                        <asp:TextBox ID="txtfiresafsys_NuSandBuck" runat="server" class="text" TextMode="MultiLine" type="text"></asp:TextBox>
                    </td>
                    <td>
                      गोदाम में आवश्यक आपातकालीन दूरभाष नम्बर लिखे 
                    </td>
                    <td>
                         <asp:DropDownList ID="ddltxtfiresafsys_PhoneNu" Height="35px" Class="form-control" runat="server">
                        <asp:ListItem Value="0">-----Select------</asp:ListItem>
                        <asp:ListItem Value="1">हाँ</asp:ListItem>
                        <asp:ListItem Value="2">नहीं</asp:ListItem>
                    </asp:DropDownList>
                    </td>
                     <td>
                      रिमार्क
                    </td>
                    <td>
                        <asp:TextBox ID="txtfiresafsys_Remark" runat="server" class="text" TextMode="MultiLine" type="text"></asp:TextBox>
                    </td>
                </tr>


                 <tr>
                    <td colspan="6">
                        <div style="background-color: #66CCFF">
                            <p style="color: #008080; font-size:large; text-align:center">
                               वेब्रिज़ (धर्मकाँटा)  :</p>
                        </div>
                    </td>
                </tr>
                <tr>
                     <td>
                       परिसर में धर्मकाँटा हें या नहीं  :
                    </td>
                    <td>
                       <asp:DropDownList ID="ddlWeybridge_DharmInPremises" Height="35px" Class="form-control" runat="server">
                        <asp:ListItem Value="0">-----Select------</asp:ListItem>
                        <asp:ListItem Value="1">हां</asp:ListItem>
                        <asp:ListItem Value="2">नहीं</asp:ListItem>
                        <%--<asp:ListItem Value="2">टार रोड</asp:ListItem>--%>
                    </asp:DropDownList>
                    </td>
                    <td>
                       क्षमता (मे.टन.):
                    </td>
                    <td>
                        <asp:TextBox ID="txtWeybridge_Ability" runat="server" class="text" type="text"></asp:TextBox>

                       <%--<asp:DropDownList ID="ddlWeybridge_Ability" Height="35px" Class="form-control" runat="server">
                        <asp:ListItem Value="0">-----Select------</asp:ListItem>
                        <asp:ListItem Value="1">हाँ</asp:ListItem>
                        <asp:ListItem Value="2">नहीं</asp:ListItem>
                    </asp:DropDownList>--%>
                    </td>
                    <td>
                       केलीब्रेशन का दिनांक  :
                    </td>
                    <td>
                       <%-- <asp:DropDownList ID="ddlWeybridge_DateCalibration" Height="35px" Class="form-control" runat="server">
                        <asp:ListItem Value="0">-----Select------</asp:ListItem>
                        <asp:ListItem Value="1">उत्तम</asp:ListItem>
                        <asp:ListItem Value="2">औसत</asp:ListItem>
                        <asp:ListItem Value="2">क्षतिग्रस्त</asp:ListItem>
                        <asp:ListItem Value="2">दयनीय</asp:ListItem>
                    </asp:DropDownList>--%>
                        <asp:TextBox ID="txtWeybridge_DateCalibration" runat="server" class="text"  type="text"></asp:TextBox>
                    </td>
                  
                    </tr>
                <tr>
                     <td>
                      प्रमाण पत्र का क्र.
                    </td>
                    <td>
                        <asp:TextBox ID="txtWeybridge_CertificateNo" runat="server" class="text" TextMode="MultiLine" type="text"></asp:TextBox>
                    </td>
                    <td>
                      की दिनांक (वैधता तिथि) 
                    </td>
                    <td>
                        <asp:TextBox ID="txtWeybridge_ValidityDate" runat="server" class="text"  type="text"></asp:TextBox>

                        <%-- <asp:DropDownList ID="ddlWeybridge_ValidityDate" Height="35px" Class="form-control" runat="server">
                        <asp:ListItem Value="0">-----Select------</asp:ListItem>
                        <asp:ListItem Value="1">हाँ</asp:ListItem>
                        <asp:ListItem Value="2">नहीं</asp:ListItem>
                    </asp:DropDownList>--%>
                    </td>
                     <td>
                      धर्मकाँटा परिसर में नहीं हैं तो परिसर से दुरी
                    </td>
                    <td>
                        <asp:TextBox ID="txtWeybridge_DistanceCampus" runat="server" class="text" TextMode="MultiLine" type="text"></asp:TextBox>
                    </td>
                </tr>

                 <tr>
                    <td colspan="6">
                        <div style="background-color: #66CCFF">
                            <p style="color: #008080; font-size:large; text-align:center">
                               कार्यालय क्षेत्र पर्याप्त/अपर्याप्त  :</p>
                        </div>
                    </td>
                </tr>
                <tr>
                     <td>
                       छत की स्थिति  :
                    </td>
                    <td>
                        <%--<asp:TextBox ID="txtOfficeArea_RoofPosition" runat="server" class="text"  type="text"></asp:TextBox>--%>

                       <asp:DropDownList ID="ddlOfficeArea_RoofPosition" Height="35px" Class="form-control" runat="server">
                        <asp:ListItem Value="0">-----Select------</asp:ListItem>
                         <asp:ListItem Value="1">उत्तम</asp:ListItem>
                        <asp:ListItem Value="2">औसत</asp:ListItem>
                        <asp:ListItem Value="3">क्षतिग्रस्त</asp:ListItem>
                        <asp:ListItem Value="4">दयनीय</asp:ListItem>
                    </asp:DropDownList>
                    </td>
                    <td>
                       फ्लोअर की स्थिति:
                    </td>
                    <td>
                       <asp:DropDownList ID="ddlOfficeArea_FloorPosition" Height="35px" Class="form-control" runat="server">
                        <asp:ListItem Value="0">-----Select------</asp:ListItem>
                        <asp:ListItem Value="1">उत्तम</asp:ListItem>
                        <asp:ListItem Value="2">औसत</asp:ListItem>
                        <asp:ListItem Value="3">क्षतिग्रस्त</asp:ListItem>
                        <asp:ListItem Value="4">दयनीय</asp:ListItem>
                    </asp:DropDownList>
                    </td>
                    <td>
                       खिड़की दरवाजे की स्थिति  :
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlOfficeArea_WindowDoorPosition" Height="35px" Class="form-control" runat="server">
                        <asp:ListItem Value="0">-----Select------</asp:ListItem>
                        <asp:ListItem Value="1">उत्तम</asp:ListItem>
                        <asp:ListItem Value="2">औसत</asp:ListItem>
                        <asp:ListItem Value="3">क्षतिग्रस्त</asp:ListItem>
                        <asp:ListItem Value="4">दयनीय</asp:ListItem>
                    </asp:DropDownList>
                    </td>
                  
                    </tr>
                <tr>
                     <td>
                      शोचालय की स्थिति
                    </td>
                    <td>
                         <asp:DropDownList ID="ddlOfficeArea_ToiletStatus" Height="35px" Class="form-control" runat="server">
                        <asp:ListItem Value="0">-----Select------</asp:ListItem>
                        <asp:ListItem Value="1">उत्तम</asp:ListItem>
                        <asp:ListItem Value="2">औसत</asp:ListItem>
                        <asp:ListItem Value="3">क्षतिग्रस्त</asp:ListItem>
                        <asp:ListItem Value="4">दयनीय</asp:ListItem>
                    </asp:DropDownList>
                        <%--<asp:TextBox ID="txtOfficeArea_ToiletStatus" runat="server" class="text" TextMode="MultiLine" type="text"></asp:TextBox>--%>
                    </td>
                    <td>
                      विद्धुत वियावस्था की स्थिति electrical system status
                    </td>
                    <td>
                         <asp:DropDownList ID="txtOfficeArea_ElesyStemStatus" Height="35px" Class="form-control" runat="server">
                        <asp:ListItem Value="0">-----Select------</asp:ListItem>
                        <asp:ListItem Value="1">हाँ</asp:ListItem>
                        <asp:ListItem Value="2">नहीं</asp:ListItem>
                    </asp:DropDownList>
                    </td>
                     <td>
                      पुताई की स्थिति
                    </td>
                    <td>
                        <asp:TextBox ID="txtOfficeArea_PantingPosition" runat="server" class="text" TextMode="MultiLine" type="text"></asp:TextBox>
                    </td>
                </tr>

                <tr>
                     <td>
                      पेंटिंग की स्थिति
                    </td>
                    <td>
                        <asp:TextBox ID="txtOfficeArea_PaintingPosition" runat="server" class="text" TextMode="MultiLine" type="text"></asp:TextBox>
                    </td>
                    <td>
                      रिमार्क
                    </td>
                    <td>
                        <asp:TextBox ID="txtOfficeArea_Remark" runat="server" class="text" TextMode="MultiLine" type="text"></asp:TextBox>

                         <%--<asp:DropDownList ID="ddlOfficeArea_Remark" Height="35px" Class="form-control" runat="server">
                        <asp:ListItem Value="0">-----Select------</asp:ListItem>
                        <asp:ListItem Value="1">हाँ</asp:ListItem>
                        <asp:ListItem Value="2">नहीं</asp:ListItem>
                    </asp:DropDownList>--%>
                    </td>
                    
                </tr>
                   <tr>
                    <td>
                    
                    </td>
                    <td>
                        <%--<asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="BTNBLUE" />--%>
                        <%--<asp:Button ID="btnsubmit" runat="server" Text="Submit" CssClass="BTNBLUE" OnClick="btnsubmit_Click" ></asp:Button>--%>
                        <%--<asp:Label ID="lblmsg" runat="server"></asp:Label>--%>
                    &nbsp;
                       <%-- <asp:LinkButton ID="LinkButton2" runat="server" PostBackUrl="~/Inspection/OldBranchInsp.aspx">Old Audits</asp:LinkButton--%>
                    </td>
                      
                </tr>
               <%-- <tr>
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" Visible="true" Width="100px"
                        CssClass="BTNBLUE" />
                </tr>--%>
               <%-- <asp:Button ID="btnSubmit" runat="server" Text="Submit" Visible="true" Width="100px"
                        CssClass="BTNBLUE" OnClick="btnSubmit_Click"/>--%>

            </table>
            <div style="text-align:center">
                 <asp:Button ID="btnSubmit" runat="server" Text="Submit" Visible="true" Width="100px"
                        CssClass="BTNBLUE" OnClick="btnSubmit_Click"/>
            </div>
           
                    
               

        </div>
    </div>
</asp:Content>

