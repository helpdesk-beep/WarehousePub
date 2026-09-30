<%@ Page Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="Branch_Available_Resources.aspx.cs" Inherits="BranchPages_Branch_Available_Resources" Title="Resource Avalability" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <fieldset style="width: 1000px; border: 2px solid navy;">
        <center>
            <div>
                <table cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td align="center" valign="top">
                            <fieldset style="width: 980px; border: 1px solid navy;">
                                <center>
                                    <div>
                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr style="background-color: #0bb6e6; height: 25px">
                                                <td align="center">
                                                    <asp:Label ID="lblDepotMaster" runat="server" Text="Branch Detail" ForeColor="whitesmoke"
                                                        Font-Size="12pt" Font-Bold="true"></asp:Label>
                                                </td>
                                            </tr>
                                            </table>
                                    </div>
                                </center>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" valign="top">
                            <fieldset style="width: 980px; border: 1px solid navy;">
                                <center>
                                    <div>
                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr style="background-color: #0bb6e6; height: 25px">
                                                <td colspan="4" align="center">
                                                    <asp:Label ID="lblNodalOfficeDetails" runat="server" Font-Bold="True" Text="Branch Manager/Issue Center Manager Details"
                                                        ForeColor="WhiteSmoke" Font-Size="12pt"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 5px" colspan="4">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" style="width: 200px" valign="top">
                                                    <asp:Label ID="lblNodalOffice" runat="server" Text="Branch Manager Name" Font-Size="8pt"
                                                        Font-Bold="True" ForeColor="Navy"></asp:Label></td>
                                                <td align="left" valign="top">
                                                    <asp:TextBox ID="txtNodalOfficerName" runat="server" Width="200px"></asp:TextBox></td>
                                                <td align="left" style="width: 200px" valign="top">
                                                    <asp:Label ID="lblbmContact" runat="server" Text=" Branch Manager Contact" Font-Size="8pt" Font-Bold="True"
                                                        ForeColor="navy"></asp:Label></td>
                                                <td align="left" valign="top">
                                                      <asp:TextBox ID="txtBMContact" runat="server" Width="200px" MaxLength="11" 
                                                        ></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="txtBMContact"
                                        ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender></td>
                                            </tr>
                                            <tr>
                                                <td style="height: 5px" colspan="4">
                                                </td>
                                            </tr>
                                            <tr>
                                               <%-- <td align="left">
                                                    <asp:Label ID="lblPhNo1" runat="server" Text="Phone No." Font-Size="8pt" ForeColor="navy"
                                                        Font-Bold="True"></asp:Label></td>--%>

                                                <td align="left">
                                                    <asp:Label ID="lblIMName" runat="server" Text="Issue Center Manager Name" Font-Size="8pt" Font-Bold="True"
                                                        ForeColor="navy"></asp:Label></td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtIMName" runat="server" Width="200px" MaxLength="11" 
                                                        ></asp:TextBox>
                                                      
                                                        </td>
                                                        <td align="left">
                                                    <asp:Label ID="lblEmail1" runat="server" Text="Issue Center Manager Contact" Width="142px" Font-Size="8pt" Font-Bold="True"
                                                        ForeColor="navy"></asp:Label></td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtIMContact" runat="server" Width="200px"
                                                        Font-Names="Verdana" Font-Size="9pt" TabIndex="12"></asp:TextBox></td>
                                            </tr>
                                             <tr>
                                                <td style="height: 5px" colspan="4">
                                                </td>
                                            </tr>
                                            <tr>
                                               <%-- <td align="left">
                                                    <asp:Label ID="lblPhNo1" runat="server" Text="Phone No." Font-Size="8pt" ForeColor="navy"
                                                        Font-Bold="True"></asp:Label></td>--%>

                                                <td align="left">
                                                    <asp:Label ID="Label3" runat="server" Text="Total No of Staff at Branch(संख्या)" Font-Size="8pt" Font-Bold="True"
                                                        ForeColor="navy"></asp:Label></td>
                                                <td align="left">
                                                    <asp:TextBox ID="TextBox46" runat="server" Width="200px" MaxLength="11" 
                                                        ></asp:TextBox>
                                                      
                                                        </td>
                                                        
                                            </tr>
                                            <tr>
                                                <td style="height: 5px" colspan="4">
                                                </td>
                                                
                                            </tr>
                                           
                                            
                                        </table>
                                    </div>
                                </center>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 5px">
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 5px">
                        </td>
                    </tr>
                    <tr>
                        <td align="center" valign="top">
                            <fieldset style="width: 980px; border: 1px solid navy;">
                                <center>
                                    <div>
                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr style="background-color: #0bb6e6; height: 25px">
                                                <td colspan="4" align="center">
                                                    <asp:Label ID="lblDepotDetails" runat="server" Font-Bold="True" Text="Other Details"
                                                        ForeColor="WhiteSmoke" Font-Size="12pt"></asp:Label>
                                                </td>
                                            </tr>
                                            
                                            
                                            <tr>
                                                
                                            </tr>
                                            
                                            
                                            
                                            <tr>
                                                <td style="height: 5px" colspan="4">
                                                </td>
                                            </tr>
                                            <tr>
                                            <td align="left">
                                                    <asp:Label ID="lblDepotCapacity" runat="server" Text="Branch Capicity in M.T." Font-Size="8pt"
                                                        Font-Bold="True" ForeColor="navy"></asp:Label></td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtDepoCapicity" runat="server" Width="200px" onkeyup="NumericDecimalCheck(this,2)"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="txtDepoCapicity"
                                        ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender>
                                                </td>
                                                                                            
                                                <td align="left">
                                                    <asp:Label ID="lblRailSliding" runat="server" Text="Branch distance from Rack Point(If Rail Siding)" Font-Size="8pt"
                                                        ForeColor="navy" Font-Bold="True"></asp:Label></td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtDFRP" runat="server" Width="200px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="height: 5px" colspan="4">
                                                </td>
                                            </tr>                                                                                     
                                            
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="Label6" runat="server" Text="Internet Connectivity" Font-Size="8pt"
                                                        Font-Bold="True" ForeColor="navy"></asp:Label></td>
                                                <td align="left">
                                                    <asp:DropDownList ID="IntConnctddl" runat="server" Width="205px" Height="25px"  AutoPostBack="false"
                                                        >
                                                        <asp:ListItem>--Select--</asp:ListItem>
                                                        <asp:ListItem Value="Y" >Yes</asp:ListItem>
                                                        <asp:ListItem Value="N" >No</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                
                                                <td align="left">
                                                    <asp:Label ID="Label11" runat="server" Text="Type Of Connectivity" Font-Size="8pt" ForeColor="navy" Visible="true"
                                                        Font-Bold="True" ></asp:Label></td>
                                                <td align="left">
                                                    <asp:DropDownList ID="typeOfIntrConnctddl" runat="server" Width="205px" Height="25px" Visible="true">
                                                     <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                        <asp:ListItem Value="Fixed Broadband" Text="Fixed Broadband Connection"></asp:ListItem>
                                                        <asp:ListItem Value="Dongel" Text="Dongel"></asp:ListItem>
                                                      
                                                       
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>                                            
                                            
                                            <tr>
                                                <td style="height: 5px" colspan="4">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="Label5" runat="server" Text="ISP Company Name" Font-Size="8pt"
                                                        Font-Bold="True" ForeColor="navy"></asp:Label></td>
                                                <td align="left">
                                                   <asp:TextBox ID="txtISPName" runat="server" Width="200px"></asp:TextBox>
                                                </td>
                                                
                                                <td align="left">
                                                    <asp:Label ID="Label16" runat="server" Text="SWAN Connectivity" Font-Size="8pt" ForeColor="navy" Visible="true"
                                                        Font-Bold="True" ></asp:Label></td>
                                                <td align="left">
                                                    <asp:DropDownList ID="ddlSWAN" runat="server" Width="205px" Height="25px" Visible="true">
                                                     <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                        <asp:ListItem Value="Y" Text="Yes"></asp:ListItem>
                                                        <asp:ListItem Value="N" Text="No"></asp:ListItem>
                                                      
                                                       
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>                                            
                                            
                                            <tr>
                                                <td style="height: 5px" colspan="4">
                                                </td>
                                            </tr>
                                            <tr>
                                            <td align="left">
                                                    <asp:Label ID="Label13" runat="server" Text="Power Suply" Font-Size="8pt"
                                                        Font-Bold="True" ForeColor="navy"></asp:Label></td>
                                                <td align="left">
                                                    <asp:DropDownList ID="PowerSuplyddl" runat="server" Width="205px" Height="25px">
                                                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                        <asp:ListItem Value="1">1 Phase</asp:ListItem>
                                                        <asp:ListItem Value="2">2 Phase</asp:ListItem>
                                                        <asp:ListItem Value="3">3 Phase</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            
                                            
                                                <td align="left">
                                                    <asp:Label ID="Label12" runat="server" Text="Electronic Weighbridge" Font-Size="8pt"
                                                        Font-Bold="True" ForeColor="navy"></asp:Label></td>
                                                <td align="left">
                                                    <asp:DropDownList ID="ddlEWAvail" runat="server" Width="205px" Height="25px">
                                                        <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                        <asp:ListItem Value="Y" Text="Yes"></asp:ListItem>
                                                        <asp:ListItem Value="N" Text="No"></asp:ListItem>
                                                   </asp:DropDownList>
                                                </td>
                                            </tr> 
                                             <tr>
                                                <td style="height: 5px" colspan="4">
                                                    </td>
                                            </tr>  
                                              <tr>
                                            <td align="left">
                                                    <asp:Label ID="Label24" runat="server" Text="Weighbridged Type" Font-Size="8pt"
                                                        Font-Bold="True" ForeColor="navy"></asp:Label></td>
                                                <td align="left">
                                                    <asp:DropDownList ID="ddlWBTYpe" runat="server" Width="205px" Height="25px">
                                                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                        <asp:ListItem Value="Analog">Analog</asp:ListItem>
                                                        <asp:ListItem Value="Digital">Digital</asp:ListItem>   
                                                        <asp:ListItem Value="NA">Not Available</asp:ListItem>   
                                                        
                                                    </asp:DropDownList>
                                                </td>
                                            
                                            
                                                <td align="left">
                                                    <asp:Label ID="Label25" runat="server" Text="Weighbridge Operator Name" Font-Size="8pt"
                                                        Font-Bold="True" ForeColor="navy"></asp:Label></td>
                                                <td align="left">
                                                     <asp:TextBox ID="txtEWOName" runat="server" Width="200px"></asp:TextBox>
                                                </td>
                                            </tr> 
                                            <tr>
                                                <td style="height: 5px" colspan="4">
                                                    </td>
                                            </tr>                                            
                                              
                                              <tr>
                                            <td align="left">
                                                    <asp:Label ID="Label1" runat="server" Text="Drinkig Water Availability" Font-Size="8pt"
                                                        Font-Bold="True" ForeColor="navy"></asp:Label></td>
                                                <td align="left">
                                                    <asp:DropDownList ID="ddlDrinkingWater" runat="server" Width="205px" Height="25px">
                                                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                        <asp:ListItem Value="Y">Yes</asp:ListItem>
                                                        <asp:ListItem Value="N">No</asp:ListItem>
                                                        
                                                    </asp:DropDownList>
                                                </td>
                                            
                                            
                                                <td align="left">
                                                    <asp:Label ID="Label2" runat="server" Text="Toilet Availability" Font-Size="8pt"
                                                        Font-Bold="True" ForeColor="navy"></asp:Label></td>
                                                <td align="left">
                                                     <asp:DropDownList ID="ddlToilet" runat="server" Width="205px" Height="25px">
                                                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                        <asp:ListItem Value="Y">Yes</asp:ListItem>
                                                        <asp:ListItem Value="N">No</asp:ListItem>
                                                        
                                                    </asp:DropDownList>
                                                </td>
                                            </tr> 
                                            <tr>
                                                <td style="height: 5px" colspan="4">
                                                    </td>
                                            </tr>                                                                                               
                                            <tr>
                                                <td style="height: 5px" colspan="4">
                                                    &nbsp;</td>
                                            </tr>
                                        </table>
                                    </div>
                                </center>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 5px">
                        </td>
                    </tr>
                    
                    
                    <tr>
                        <td align="center" valign="top">
                            <fieldset style="width: 980px; border: 1px solid navy;">
                                <center>
                                    <div>
                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr style="background-color: #0bb6e6; height: 25px">
                                                <td colspan="4" align="center">
                                                    <asp:Label ID="Label4" runat="server" Font-Bold="True" Text="Operator Details"
                                                        ForeColor="WhiteSmoke" Font-Size="12pt"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 5px" colspan="4">
                                                </td>
                                            </tr>
                                           
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="Label7" runat="server" Text="Name of Data Entry Operators" ForeColor="navy" Font-Size="8pt" Width="142px"
                                                        Font-Bold="True"></asp:Label></td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtDEONAme" runat="server" Width="198px" MaxLength="100"
                                                        Onkeyup="Spc_validator1(this)"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="Label8" runat="server" Text="No of Data Entry Operators(संख्या)" Font-Size="8pt" Font-Bold="True" Width="142px"
                                                        ForeColor="navy"></asp:Label></td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtDEONo" runat="server" Width="200px"
                                                        MaxLength="10" ></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtDEONo"
                                        ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender>
                                                        </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 5px" colspan="4">
                                                </td>
                                            </tr>
                                                <tr>
                                                <td align="left">
                                                    <asp:Label ID="Label14" runat="server" Text="Name of Gate Pass Operators" ForeColor="navy" Font-Size="8pt" Width="142px"
                                                        Font-Bold="True"></asp:Label></td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtGPONAme" runat="server" Width="198px" MaxLength="100"
                                                        Onkeyup="Spc_validator1(this)"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="Label15" runat="server" Text="No of Gate Pass Operators(संख्या)" Font-Size="8pt" Font-Bold="True" Width="142px"
                                                        ForeColor="navy"></asp:Label></td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtGPONo" runat="server" Width="200px"
                                                        MaxLength="10" ></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtGPONo"
                                        ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender>
                                                        </td>
                                            </tr>                                        
                                            <tr>
                                                <td style="height: 5px" colspan="4">
                                                    &nbsp;</td>
                                            </tr>
                                        </table>
                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr style="background-color: #0bb6e6; height: 25px">
                                                <td colspan="4" align="center">
                                                    <asp:Label ID="Label9" runat="server" Font-Bold="True" Text="Hardware Details"
                                                        ForeColor="WhiteSmoke" Font-Size="12pt"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 5px" colspan="4">
                                                </td>
                                            </tr>
                                           
                                            <tr>
                                                           <td colspan="4">
                                                           <table width="100%" border="1px" cellspacing="0px" cellpadding="0px">
                                                           <tr>
                                                         
                                        <td align="center" colspan="5" class="style1"><asp:Label ID="Label10" runat="server" Font-Bold="True" Font-Size="10pt" ForeColor="Blue"
                                        Text="Supplied Desktop"></asp:Label></td>
                                                           </tr>
                                                            <tr>
                                                           <th>IISFM</th>
                                                            <th>MPSCSC</th>
                                                             <th>MPWLC</th>
                                                              
                                                           </tr>
                                                           <tr>
                                                           <th>कुल की संख्या&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;कार्यरत की संख्या</th>
                                                            <th>कुल की संख्या&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;कार्यरत की संख्या</th>
                                                            <th>कुल की संख्या&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;कार्यरत की संख्या</th>
                                                            
                                                              
                                                           </tr>
                                                            <tr>
                                                          <td align="center">
                                                           <asp:TextBox ID="txtTDS" runat="server" BackColor="LemonChiffon" AutoPostBack="false" onkeyup="Deductionamtcalculate()" Text="0"
        TabIndex="9" CssClass="tb6" Width="100px" Height="20px" 
          ></asp:TextBox>
           <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" TargetControlID="txtTDS"
                                        ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:TextBox ID="TextBox22" runat="server" BackColor="LemonChiffon" AutoPostBack="false" onkeyup="Deductionamtcalculate()" Text="0"
        TabIndex="9" CssClass="tb6" Width="100px" Height="20px" 
          ></asp:TextBox>
           <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender31" runat="server" TargetControlID="txtTDS"
                                        ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                                          </td>
                                                          <td align="center">
                                                            <asp:TextBox ID="txtSD" runat="server" BackColor="LemonChiffon" AutoPostBack="false" onkeyup="Deductionamtcalculate()" Text="0"
        TabIndex="9" CssClass="tb6" Width="100px" Height="20px"  
          ></asp:TextBox>
           <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" TargetControlID="txtSD"
                                        ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:TextBox ID="TextBox23" runat="server" BackColor="LemonChiffon" AutoPostBack="false" onkeyup="Deductionamtcalculate()" Text="0"
        TabIndex="9" CssClass="tb6" Width="100px" Height="20px"  
          ></asp:TextBox>
           <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender32" runat="server" TargetControlID="txtSD"
                                        ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                                          </td>
                                                          <td align="center">
                                                           <asp:TextBox ID="txtResources" runat="server" BackColor="LemonChiffon" AutoPostBack="false" onkeyup="Deductionamtcalculate()" Text="0"
        TabIndex="9" CssClass="tb6" Width="100px" Height="20px" 
          ></asp:TextBox>
           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
           <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" TargetControlID="txtResources"
                                        ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                    
                                       <asp:TextBox ID="TextBox24" runat="server" BackColor="LemonChiffon" AutoPostBack="false" onkeyup="Deductionamtcalculate()" Text="0"
        TabIndex="9" CssClass="tb6" Width="100px" Height="20px" 
          ></asp:TextBox>
           <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender33" runat="server" TargetControlID="txtResources"
                                        ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                                          </td>
                                                         
                                                       
                                                           </tr>
                                                           </table>
                                                           </td>
                                                           </tr>
                                                           <tr>
                                                           <td colspan="4">
                                                           <table width="100%" border="1px" cellspacing="0px" cellpadding="0px">
                                                           <tr>
                                                         
                                        <td align="center" colspan="5" class="style1"><asp:Label ID="Label17" runat="server" Font-Bold="True" Font-Size="10pt" ForeColor="Green"
                                        Text="Supplied Laptop"></asp:Label></td>
                                                           </tr>
                                                           <tr>
                                                           <th>IISFM</th>
                                                            <th>MPSCSC</th>
                                                             <th>MPWLC</th>
                                                              
                                                           </tr>
                                                                                     <tr>
                                                           <th>कुल की संख्या&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;कार्यरत की संख्या</th>
                                                            <th>कुल की संख्या&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;कार्यरत की संख्या</th>
                                                            <th>कुल की संख्या&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;कार्यरत की संख्या</th>
                                                            
                                                              
                                                           </tr>
                                                            <tr>
                                                          <td align="center">
                                                           <asp:TextBox ID="TextBox1" runat="server" BackColor="LemonChiffon" AutoPostBack="false" onkeyup="Deductionamtcalculate()" Text="0"
        TabIndex="9" CssClass="tb6" Width="100px" Height="20px" 
          ></asp:TextBox>
           <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtTDS"
                                        ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:TextBox ID="TextBox2" runat="server" BackColor="LemonChiffon" AutoPostBack="false" onkeyup="Deductionamtcalculate()" Text="0"
        TabIndex="9" CssClass="tb6" Width="100px" Height="20px" 
          ></asp:TextBox>
           <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" TargetControlID="txtTDS"
                                        ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                                          </td>
                                                          <td align="center">
                                                            <asp:TextBox ID="TextBox3" runat="server" BackColor="LemonChiffon" AutoPostBack="false" onkeyup="Deductionamtcalculate()" Text="0"
        TabIndex="9" CssClass="tb6" Width="100px" Height="20px"  
          ></asp:TextBox>
           <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server" TargetControlID="txtSD"
                                        ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:TextBox ID="TextBox25" runat="server" BackColor="LemonChiffon" AutoPostBack="false" onkeyup="Deductionamtcalculate()" Text="0"
        TabIndex="9" CssClass="tb6" Width="100px" Height="20px"  
          ></asp:TextBox>
           <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender34" runat="server" TargetControlID="txtSD"
                                        ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                                          </td>
                                                          <td align="center">
                                                           <asp:TextBox ID="TextBox26" runat="server" BackColor="LemonChiffon" AutoPostBack="false" onkeyup="Deductionamtcalculate()" Text="0"
        TabIndex="9" CssClass="tb6" Width="100px" Height="20px" 
          ></asp:TextBox>
           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
           <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender35" runat="server" TargetControlID="txtResources"
                                        ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                    
                                       <asp:TextBox ID="TextBox27" runat="server" BackColor="LemonChiffon" AutoPostBack="false" onkeyup="Deductionamtcalculate()" Text="0"
        TabIndex="9" CssClass="tb6" Width="100px" Height="20px" 
          ></asp:TextBox>
           <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender36" runat="server" TargetControlID="txtResources"
                                        ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                                          </td>
                                                         
                                                       
                                                           </tr>
                                                           </table>
                                                           </td>
                                                           </tr>
                                                           <tr>
                                                           <td colspan="4">
                                                           <table width="100%" border="1px" cellspacing="0px" cellpadding="0px">
                                                           <tr>
                                                         
                                        <td align="center" colspan="5" class="style1"><asp:Label ID="Label18" runat="server" Font-Bold="True" Font-Size="10pt" ForeColor="Blue"
                                        Text="Supplied Printer"></asp:Label></td>
                                                           </tr>
                                                           <tr>
                                                           <th>IISFM</th>
                                                            <th>MPSCSC</th>
                                                             <th>MPWLC</th>
                                                              
                                                           </tr>
                                                                                     <tr>
                                                           <th>कुल की संख्या&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;कार्यरत की संख्या</th>
                                                            <th>कुल की संख्या&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;कार्यरत की संख्या</th>
                                                            <th>कुल की संख्या&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;कार्यरत की संख्या</th>
                                                            
                                                              
                                                           </tr>
                                                            <tr>
                                                          <td align="center">
                                                           <asp:TextBox ID="TextBox4" runat="server" BackColor="LemonChiffon" AutoPostBack="false" onkeyup="Deductionamtcalculate()" Text="0"
        TabIndex="9" CssClass="tb6" Width="100px" Height="20px" 
          ></asp:TextBox>
           <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server" TargetControlID="txtTDS"
                                        ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:TextBox ID="TextBox5" runat="server" BackColor="LemonChiffon" AutoPostBack="false" onkeyup="Deductionamtcalculate()" Text="0"
        TabIndex="9" CssClass="tb6" Width="100px" Height="20px" 
          ></asp:TextBox>
           <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender14" runat="server" TargetControlID="txtTDS"
                                        ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                                          </td>
                                                          <td align="center">
                                                            <asp:TextBox ID="TextBox6" runat="server" BackColor="LemonChiffon" AutoPostBack="false" onkeyup="Deductionamtcalculate()" Text="0"
        TabIndex="9" CssClass="tb6" Width="100px" Height="20px"  
          ></asp:TextBox>
           <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender15" runat="server" TargetControlID="txtSD"
                                        ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:TextBox ID="TextBox28" runat="server" BackColor="LemonChiffon" AutoPostBack="false" onkeyup="Deductionamtcalculate()" Text="0"
        TabIndex="9" CssClass="tb6" Width="100px" Height="20px"  
          ></asp:TextBox>
           <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender37" runat="server" TargetControlID="txtSD"
                                        ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                                          </td>
                                                          <td align="center">
                                                           <asp:TextBox ID="TextBox29" runat="server" BackColor="LemonChiffon" AutoPostBack="false" onkeyup="Deductionamtcalculate()" Text="0"
        TabIndex="9" CssClass="tb6" Width="100px" Height="20px" 
          ></asp:TextBox>
           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
           <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender38" runat="server" TargetControlID="txtResources"
                                        ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                    
                                       <asp:TextBox ID="TextBox30" runat="server" BackColor="LemonChiffon" AutoPostBack="false" onkeyup="Deductionamtcalculate()" Text="0"
        TabIndex="9" CssClass="tb6" Width="100px" Height="20px" 
          ></asp:TextBox>
           <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender39" runat="server" TargetControlID="txtResources"
                                        ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                                          </td>
                                                         
                                                       
                                                           </tr>
                                                           </table>
                                                           </td>
                                                           </tr>
                                                           <tr>
                                                           <td colspan="4">
                                                           <table width="100%" border="1px" cellspacing="0px" cellpadding="0px">
                                                           <tr>
                                                         
                                        <td align="center" colspan="5" class="style1"><asp:Label ID="Label19" runat="server" Font-Bold="True" Font-Size="10pt" ForeColor="Green"
                                        Text="Supplied UPS"></asp:Label></td>
                                                           </tr>
                                                           <tr>
                                                           <th>IISFM</th>
                                                            <th>MPSCSC</th>
                                                             <th>MPWLC</th>
                                                              
                                                           </tr>
                                                                                     <tr>
                                                           <th>कुल की संख्या&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;कार्यरत की संख्या</th>
                                                            <th>कुल की संख्या&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;कार्यरत की संख्या</th>
                                                            <th>कुल की संख्या&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;कार्यरत की संख्या</th>
                                                            
                                                              
                                                           </tr>
                                                            <tr>
                                                          <td align="center">
                                                           <asp:TextBox ID="TextBox7" runat="server" BackColor="LemonChiffon" AutoPostBack="false" onkeyup="Deductionamtcalculate()" Text="0"
        TabIndex="9" CssClass="tb6" Width="100px" Height="20px" 
          ></asp:TextBox>
           <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender16" runat="server" TargetControlID="txtTDS"
                                        ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:TextBox ID="TextBox8" runat="server" BackColor="LemonChiffon" AutoPostBack="false" onkeyup="Deductionamtcalculate()" Text="0"
        TabIndex="9" CssClass="tb6" Width="100px" Height="20px" 
          ></asp:TextBox>
           <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender17" runat="server" TargetControlID="txtTDS"
                                        ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                                          </td>
                                                          <td align="center">
                                                            <asp:TextBox ID="TextBox9" runat="server" BackColor="LemonChiffon" AutoPostBack="false" onkeyup="Deductionamtcalculate()" Text="0"
        TabIndex="9" CssClass="tb6" Width="100px" Height="20px"  
          ></asp:TextBox>
           <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender18" runat="server" TargetControlID="txtSD"
                                        ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:TextBox ID="TextBox31" runat="server" BackColor="LemonChiffon" AutoPostBack="false" onkeyup="Deductionamtcalculate()" Text="0"
        TabIndex="9" CssClass="tb6" Width="100px" Height="20px"  
          ></asp:TextBox>
           <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender40" runat="server" TargetControlID="txtSD"
                                        ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                                          </td>
                                                          <td align="center">
                                                           <asp:TextBox ID="TextBox32" runat="server" BackColor="LemonChiffon" AutoPostBack="false" onkeyup="Deductionamtcalculate()" Text="0"
        TabIndex="9" CssClass="tb6" Width="100px" Height="20px" 
          ></asp:TextBox>
           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
           <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender41" runat="server" TargetControlID="txtResources"
                                        ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                    
                                       <asp:TextBox ID="TextBox33" runat="server" BackColor="LemonChiffon" AutoPostBack="false" onkeyup="Deductionamtcalculate()" Text="0"
        TabIndex="9" CssClass="tb6" Width="100px" Height="20px" 
          ></asp:TextBox>
           <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender42" runat="server" TargetControlID="txtResources"
                                        ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                                          </td>
                                                         
                                                       
                                                           </tr>
                                                           </table>
                                                           </td>
                                                           </tr>
                                                           <tr>
                                                           <td colspan="4">
                                                           <table width="100%" border="1px" cellspacing="0px" cellpadding="0px">
                                                           <tr>
                                                         
                                        <td align="center" colspan="5" class="style1"><asp:Label ID="Label20" runat="server" Font-Bold="True" Font-Size="10pt" ForeColor="Blue"
                                        Text="Supplied Thermal Printer for barcode scanning"></asp:Label></td>
                                                           </tr>
                                                           <tr>
                                                           <th>IISFM</th>
                                                            <th>MPSCSC</th>
                                                             <th>MPWLC</th>
                                                              
                                                           </tr>
                                                                                     <tr>
                                                           <th>कुल की संख्या&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;कार्यरत की संख्या</th>
                                                            <th>कुल की संख्या&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;कार्यरत की संख्या</th>
                                                            <th>कुल की संख्या&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;कार्यरत की संख्या</th>
                                                            
                                                              
                                                           </tr>
                                                            <tr>
                                                          <td align="center">
                                                           <asp:TextBox ID="TextBox10" runat="server" BackColor="LemonChiffon" AutoPostBack="false" onkeyup="Deductionamtcalculate()" Text="0"
        TabIndex="9" CssClass="tb6" Width="100px" Height="20px" 
          ></asp:TextBox>
           <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender19" runat="server" TargetControlID="txtTDS"
                                        ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:TextBox ID="TextBox11" runat="server" BackColor="LemonChiffon" AutoPostBack="false" onkeyup="Deductionamtcalculate()" Text="0"
        TabIndex="9" CssClass="tb6" Width="100px" Height="20px" 
          ></asp:TextBox>
           <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender20" runat="server" TargetControlID="txtTDS"
                                        ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                                          </td>
                                                          <td align="center">
                                                            <asp:TextBox ID="TextBox12" runat="server" BackColor="LemonChiffon" AutoPostBack="false" onkeyup="Deductionamtcalculate()" Text="0"
        TabIndex="9" CssClass="tb6" Width="100px" Height="20px"  
          ></asp:TextBox>
           <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender21" runat="server" TargetControlID="txtSD"
                                        ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:TextBox ID="TextBox34" runat="server" BackColor="LemonChiffon" AutoPostBack="false" onkeyup="Deductionamtcalculate()" Text="0"
        TabIndex="9" CssClass="tb6" Width="100px" Height="20px"  
          ></asp:TextBox>
           <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender43" runat="server" TargetControlID="txtSD"
                                        ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                                          </td>
                                                          <td align="center">
                                                           <asp:TextBox ID="TextBox35" runat="server" BackColor="LemonChiffon" AutoPostBack="false" onkeyup="Deductionamtcalculate()" Text="0"
        TabIndex="9" CssClass="tb6" Width="100px" Height="20px" 
          ></asp:TextBox>
           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
           <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender44" runat="server" TargetControlID="txtResources"
                                        ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                    
                                       <asp:TextBox ID="TextBox36" runat="server" BackColor="LemonChiffon" AutoPostBack="false" onkeyup="Deductionamtcalculate()" Text="0"
        TabIndex="9" CssClass="tb6" Width="100px" Height="20px" 
          ></asp:TextBox>
           <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender45" runat="server" TargetControlID="txtResources"
                                        ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                                          </td>
                                                         
                                                       
                                                           </tr>
                                                           </table>
                                                           </td>
                                                           </tr>
                                                           <tr>
                                                           <td colspan="4">
                                                           <table width="100%" border="1px" cellspacing="0px" cellpadding="0px">
                                                           <tr>
                                                         
                                        <td align="center" colspan="5" class="style1"><asp:Label ID="Label21" runat="server" Font-Bold="True" Font-Size="10pt" ForeColor="Green"
                                        Text="Supplied Tablets"></asp:Label></td>
                                                           </tr>
                                                           <tr>
                                                           <th>IISFM</th>
                                                            <th>MPSCSC</th>
                                                             <th>MPWLC</th>
                                                              
                                                           </tr>
                                                                                     <tr>
                                                           <th>कुल की संख्या&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;कार्यरत की संख्या</th>
                                                            <th>कुल की संख्या&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;कार्यरत की संख्या</th>
                                                            <th>कुल की संख्या&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;कार्यरत की संख्या</th>
                                                            
                                                              
                                                           </tr>
                                                            <tr>
                                                          <td align="center">
                                                           <asp:TextBox ID="TextBox13" runat="server" BackColor="LemonChiffon" AutoPostBack="false" onkeyup="Deductionamtcalculate()" Text="0"
        TabIndex="9" CssClass="tb6" Width="100px" Height="20px" 
          ></asp:TextBox>
           <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender22" runat="server" TargetControlID="txtTDS"
                                        ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:TextBox ID="TextBox14" runat="server" BackColor="LemonChiffon" AutoPostBack="false" onkeyup="Deductionamtcalculate()" Text="0"
        TabIndex="9" CssClass="tb6" Width="100px" Height="20px" 
          ></asp:TextBox>
           <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender23" runat="server" TargetControlID="txtTDS"
                                        ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                                          </td>
                                                          <td align="center">
                                                            <asp:TextBox ID="TextBox15" runat="server" BackColor="LemonChiffon" AutoPostBack="false" onkeyup="Deductionamtcalculate()" Text="0"
        TabIndex="9" CssClass="tb6" Width="100px" Height="20px"  
          ></asp:TextBox>
           <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender24" runat="server" TargetControlID="txtSD"
                                        ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:TextBox ID="TextBox37" runat="server" BackColor="LemonChiffon" AutoPostBack="false" onkeyup="Deductionamtcalculate()" Text="0"
        TabIndex="9" CssClass="tb6" Width="100px" Height="20px"  
          ></asp:TextBox>
           <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender46" runat="server" TargetControlID="txtSD"
                                        ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                                          </td>
                                                          <td align="center">
                                                           <asp:TextBox ID="TextBox38" runat="server" BackColor="LemonChiffon" AutoPostBack="false" onkeyup="Deductionamtcalculate()" Text="0"
        TabIndex="9" CssClass="tb6" Width="100px" Height="20px" 
          ></asp:TextBox>
           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
           <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender47" runat="server" TargetControlID="txtResources"
                                        ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                    
                                       <asp:TextBox ID="TextBox39" runat="server" BackColor="LemonChiffon" AutoPostBack="false" onkeyup="Deductionamtcalculate()" Text="0"
        TabIndex="9" CssClass="tb6" Width="100px" Height="20px" 
          ></asp:TextBox>
           <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender48" runat="server" TargetControlID="txtResources"
                                        ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                                          </td>
                                                         
                                                       
                                                           </tr>
                                                           </table>
                                                           </td>
                                                           </tr>
                                                           <tr>
                                                           <td colspan="4">
                                                           <table width="100%" border="1px" cellspacing="0px" cellpadding="0px">
                                                           <tr>
                                                         
                                        <td align="center" colspan="5" class="style1"><asp:Label ID="Label22" runat="server" Font-Bold="True" Font-Size="10pt" ForeColor="Blue"
                                        Text="Supplied Digital Weigh Meter"></asp:Label></td>
                                                           </tr>
                                                           <tr>
                                                           <th>IISFM</th>
                                                            <th>MPSCSC</th>
                                                             <th>MPWLC</th>
                                                              
                                                           </tr>
                                                                                     <tr>
                                                           <th>कुल की संख्या&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;कार्यरत की संख्या</th>
                                                            <th>कुल की संख्या&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;कार्यरत की संख्या</th>
                                                            <th>कुल की संख्या&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;कार्यरत की संख्या</th>
                                                            
                                                              
                                                           </tr>
                                                            <tr>
                                                          <td align="center">
                                                           <asp:TextBox ID="TextBox16" runat="server" BackColor="LemonChiffon" AutoPostBack="false" onkeyup="Deductionamtcalculate()" Text="0"
        TabIndex="9" CssClass="tb6" Width="100px" Height="20px" 
          ></asp:TextBox>
           <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender25" runat="server" TargetControlID="txtTDS"
                                        ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:TextBox ID="TextBox17" runat="server" BackColor="LemonChiffon" AutoPostBack="false" onkeyup="Deductionamtcalculate()" Text="0"
        TabIndex="9" CssClass="tb6" Width="100px" Height="20px" 
          ></asp:TextBox>
           <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender26" runat="server" TargetControlID="txtTDS"
                                        ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                                          </td>
                                                          <td align="center">
                                                            <asp:TextBox ID="TextBox18" runat="server" BackColor="LemonChiffon" AutoPostBack="false" onkeyup="Deductionamtcalculate()" Text="0"
        TabIndex="9" CssClass="tb6" Width="100px" Height="20px"  
          ></asp:TextBox>
           <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender27" runat="server" TargetControlID="txtSD"
                                        ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:TextBox ID="TextBox40" runat="server" BackColor="LemonChiffon" AutoPostBack="false" onkeyup="Deductionamtcalculate()" Text="0"
        TabIndex="9" CssClass="tb6" Width="100px" Height="20px"  
          ></asp:TextBox>
           <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender49" runat="server" TargetControlID="txtSD"
                                        ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                                          </td>
                                                          <td align="center">
                                                           <asp:TextBox ID="TextBox41" runat="server" BackColor="LemonChiffon" AutoPostBack="false" onkeyup="Deductionamtcalculate()" Text="0"
        TabIndex="9" CssClass="tb6" Width="100px" Height="20px" 
          ></asp:TextBox>
           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
           <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender50" runat="server" TargetControlID="txtResources"
                                        ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                    
                                       <asp:TextBox ID="TextBox42" runat="server" BackColor="LemonChiffon" AutoPostBack="false" onkeyup="Deductionamtcalculate()" Text="0"
        TabIndex="9" CssClass="tb6" Width="100px" Height="20px" 
          ></asp:TextBox>
           <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender51" runat="server" TargetControlID="txtResources"
                                        ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                                          </td>
                                                         
                                                       
                                                           </tr>
                                                           </table>
                                                           </td>
                                                           </tr>
                                                           <tr>
                                                           <td colspan="4">
                                                           <table width="100%" border="1px" cellspacing="0px" cellpadding="0px">
                                                           <tr>
                                                         
                                        <td align="center" colspan="3" class="style1"><asp:Label ID="Label23" runat="server" Font-Bold="True" Font-Size="10pt" ForeColor="green"
                                        Text="Supplied Sim Card Dongle"></asp:Label></td>
                                                           </tr>
                                                           <tr>
                                                           <th>IISFM</th>
                                                            <th>MPSCSC</th>
                                                             <th>MPWLC</th>
                                                              
                                                           </tr>
                                                                                     <tr>
                                                           <th>कुल की संख्या&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;कार्यरत की संख्या</th>
                                                            <th>कुल की संख्या&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;कार्यरत की संख्या</th>
                                                            <th>कुल की संख्या&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;कार्यरत की संख्या</th>
                                                            
                                                              
                                                           </tr>
                                                            <tr>
                                                          <td align="center">
                                                           <asp:TextBox ID="TextBox19" runat="server" BackColor="LemonChiffon" AutoPostBack="false" onkeyup="Deductionamtcalculate()" Text="0"
        TabIndex="9" CssClass="tb6" Width="100px" Height="20px" 
          ></asp:TextBox>
           <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender28" runat="server" TargetControlID="txtTDS"
                                        ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:TextBox ID="TextBox20" runat="server" BackColor="LemonChiffon" AutoPostBack="false" onkeyup="Deductionamtcalculate()" Text="0"
        TabIndex="9" CssClass="tb6" Width="100px" Height="20px" 
          ></asp:TextBox>
           <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender29" runat="server" TargetControlID="txtTDS"
                                        ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                                          </td>
                                                          <td align="center">
                                                            <asp:TextBox ID="TextBox21" runat="server" BackColor="LemonChiffon" AutoPostBack="false" onkeyup="Deductionamtcalculate()" Text="0"
        TabIndex="9" CssClass="tb6" Width="100px" Height="20px"  
          ></asp:TextBox>
           <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender30" runat="server" TargetControlID="txtSD"
                                        ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:TextBox ID="TextBox43" runat="server" BackColor="LemonChiffon" AutoPostBack="false" onkeyup="Deductionamtcalculate()" Text="0"
        TabIndex="9" CssClass="tb6" Width="100px" Height="20px"  
          ></asp:TextBox>
           <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender52" runat="server" TargetControlID="txtSD"
                                        ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                                          </td>
                                                          <td align="center">
                                                           <asp:TextBox ID="TextBox44" runat="server" BackColor="LemonChiffon" AutoPostBack="false" onkeyup="Deductionamtcalculate()" Text="0"
        TabIndex="9" CssClass="tb6" Width="100px" Height="20px" 
          ></asp:TextBox>
           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
           <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender53" runat="server" TargetControlID="txtResources"
                                        ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                    
                                       <asp:TextBox ID="TextBox45" runat="server" BackColor="LemonChiffon" AutoPostBack="false" onkeyup="Deductionamtcalculate()" Text="0"
        TabIndex="9" CssClass="tb6" Width="100px" Height="20px" 
          ></asp:TextBox>
           <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender54" runat="server" TargetControlID="txtResources"
                                        ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                                          </td>
                                                         
                                                       
                                                           </tr>
                                                               <tr>
                                                                   <td>
                                                                       l</td>
                                                               </tr>
                                                           </table>
                                                           </td>
                                                           </tr>
                                            <tr>
                                                <td style="height: 5px" colspan="4">
                                                </td>
                                            </tr>
                                                                                        
                                            <tr>
                                                <td style="height: 5px" colspan="4">
                                                    &nbsp;</td>
                                            </tr>
                                        </table>
                                    </div>
                                </center>
                            </fieldset>
                        </td>
                    </tr>
                    
                    <tr>
                        <td style="height: 5px">
                        </td>
                    </tr>                                        
                    <tr>
                        <td align="center">
                            <asp:Button ID="btnupdate" runat="server" Text="Save" Width="100px" CssClass="BTNBLUE"
                                ValidationGroup="validate" onclick="btnupdate_Click"/>
                            &nbsp; &nbsp; &nbsp;
                            <asp:Button ID="btn_Close" runat="server" Text="Close" Width="100px" CssClass="BTNBLUE"
                                CausesValidation="false"/>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 5px">
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Visible="true" Font-Size="10pt"
                                Font-Bold="True"></asp:Label></td>
                    </tr>
                </table>
            </div>
        </center>
    </fieldset>
</asp:Content>

<asp:Content ID="Content2" runat="server" contentplaceholderid="head">

    <script language="JavaScript1.2">
var message="MPWLC STORAGE MODULE"
var neonbasecolor="gray"
var neontextcolor="yellow"
var flashspeed=100  //in milliseconds

///No need to edit below this line/////

var n=0
if (document.all||document.getElementById){
document.write('<font color="'+neonbasecolor+'">')
for (m=0;m<message.length;m++)
document.write('<span id="neonlight'+m+'">'+message.charAt(m)+'</span>')
document.write('</font>')
}
else
document.write(message)

function crossref(number){
var crossobj=document.all? eval("document.all.neonlight"+number) : document.getElementById("neonlight"+number)
return crossobj
}

function neon(){

//Change all letters to base color
if (n==0){
for (m=0;m<message.length;m++)
//eval("document.all.neonlight"+m).style.color=neonbasecolor
crossref(m).style.color=neonbasecolor
}

//cycle through and change individual letters to neon color
crossref(n).style.color=neontextcolor

if (n<message.length-1)
n++
else{
n=0
clearInterval(flashing)
setTimeout("beginneon()",1500)
return
}
}

function beginneon(){
if (document.all||document.getElementById)
flashing=setInterval("neon()",flashspeed)
}
beginneon()
</script>
    <style type="text/css">
        .style1
        {
            height: 18px;
        }
    </style>

</asp:Content>


