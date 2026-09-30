<%@ Page Language="C#" MasterPageFile="~/MasterPage/Gdwn.master"
    AutoEventWireup="true" CodeFile="DepotMaster.aspx.cs" Inherits="State_DepotProfile"
    Title="Depot Profile" %>

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
                                                <td colspan="4" align="center">
                                                    <asp:Label ID="lblDepotMaster" runat="server" Text="Branch Master" ForeColor="whitesmoke"
                                                        Font-Size="12pt" Font-Bold="true"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 5px" colspan="4">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" style="width: 200px">
                                                    <asp:Label ID="lblRegion" runat="server" Text="Region" Font-Bold="True" Font-Size="8pt"
                                                        ForeColor="navy"></asp:Label></td>
                                                <td align="left">
                                                    <asp:DropDownList ID="ddlRegion" runat="server" Width="205px" AutoPostBack="True"
                                                        OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged" Height="25px">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlRegion"
                                                        InitialValue="--Select--" ErrorMessage="Please Select Region" ValidationGroup="validate">*</asp:RequiredFieldValidator>
                                                </td>  
                                                
                                                <td align="left">
                                                    <asp:Label ID="lblDistrict" runat="server" Text="District Name" Font-Size="8pt" Font-Bold="True"
                                                        ForeColor="navy"></asp:Label></td>
                                                <td align="left">
                                                    <asp:DropDownList ID="ddlDistrictName" runat="server" Width="205px" Font-Names="Verdana"
                                                        Height="25px">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlDistrictName"
                                                        InitialValue="--Select--" ErrorMessage="Please Select District" ValidationGroup="validate">*</asp:RequiredFieldValidator>
                                                </td>                                                                                          

                                            </tr>
                                            <tr>
                                                <td style="height: 5px" colspan="4">
                                                </td>
                                            </tr>
                                            <tr>

                                                <td align="left">
                                                    <asp:Label ID="lblDepotName" runat="server" Text="Branch Name" Font-Size="8pt" Font-Bold="True"
                                                        ForeColor="navy"></asp:Label></td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtLocationName" runat="server" Width="200px" MaxLength="100" Visible="true"></asp:TextBox></td>
                                                <asp:DropDownList ID="ddlTehsilName" runat="server" Width="205px" Visible="false"
                                                    Height="25px" AutoPostBack="True">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtLocationName"
                                                    ErrorMessage="Please Enter Branch Name" ValidationGroup="validate">*</asp:RequiredFieldValidator>
                                                    
                                                <td align="left" style="width: 200px">
                                                    <asp:Label ID="Label5" runat="server" Text="Issue Center " Font-Bold="True" Font-Size="8pt"
                                                        ForeColor="navy"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="issuecentertxt" runat="server" Width="200px" MaxLength="100" ReadOnly="true"></asp:TextBox></td>                                                    
                                            </tr>
                                            <tr>
                                                <td style="height: 5px" colspan="4">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lblDepotBelongs" runat="server" Text="Depot Belongs To" Font-Size="8pt"
                                                        Font-Bold="True" ForeColor="navy"></asp:Label></td>
                                                <td align="left">
                                                    <asp:DropDownList ID="ddlDepoBeloggsTo" runat="server" Width="205px" Height="25px">
                                                        <asp:ListItem>FCI</asp:ListItem>
                                                        <asp:ListItem>CWC</asp:ListItem>
                                                        <asp:ListItem>SWC</asp:ListItem>
                                                        <asp:ListItem>MarkFed</asp:ListItem>
                                                        <asp:ListItem>CSC</asp:ListItem>
                                                        <asp:ListItem>MPWLC</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="lblHiredType" runat="server" Text="Hired Type" Font-Size="8pt" ForeColor="navy"
                                                        Font-Bold="True" Visible="False"></asp:Label></td>
                                                <td align="left">
                                                    <asp:DropDownList ID="ddlCategory" runat="server" Width="205px" Height="25px" Visible="False">
                                                        <asp:ListItem Value="0">Owned</asp:ListItem>
                                                        <asp:ListItem Value="1">Hired</asp:ListItem>
                                                        <asp:ListItem Value="2">Joint Venture</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td style="height: 5px" colspan="4">
                                                </td>
                                            </tr> 
                                            <tr>
                                                <td align="left" style="width: 200px">
                                                    <asp:Label ID="lblState" runat="server" Text="State Name " Font-Bold="True" Font-Size="8pt" Visible="false"
                                                        ForeColor="navy"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:DropDownList ID="ddlStateName" runat="server" Width="205px" AutoPostBack="True" Visible="false"
                                                        Height="25px">
                                                    </asp:DropDownList>
                                                </td>                                            
                                            </tr>  
                                            
                                            <tr>
                                                <td style="height: 5px" colspan="4">
                                                </td>
                                            </tr>                                                                                     
                                            <tr id="type1" runat="server" visible="false">
                                                <td align="left">
                                                    <asp:Label ID="lblDepotType" runat="server" Text="Branch Type" Font-Size="8pt" ForeColor="navy"
                                                        Font-Bold="True" Visible="False"></asp:Label></td>
                                                <td align="left" colspan="3">
                                                    <asp:DropDownList ID="ddlDepoType" runat="server" Height="25px" Visible="False" Width="205px">
                                                        <asp:ListItem Value="0">Open</asp:ListItem>
                                                        <asp:ListItem Value="1">Covered</asp:ListItem>
                                                        <asp:ListItem Value="2">Plinth</asp:ListItem>
                                                    </asp:DropDownList>
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
                        <td style="height: 5px" colspan="2">
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
                                                    <asp:Label ID="lblDepotDetails" runat="server" Font-Bold="True" Text="Branch Details"
                                                        ForeColor="WhiteSmoke" Font-Size="12pt"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 5px" colspan="4">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 200px" align="left" valign="top">
                                                    <asp:Label ID="lblTehsil_Block" runat="server" Text="Tehsil/Block Name" Font-Size="8pt"
                                                        Font-Bold="True" ForeColor="Navy"></asp:Label></td>
                                                <td align="left" valign="top">
                                                    <asp:TextBox ID="txtTehsilName" runat="server" Width="200px" MaxLength="15" Visible="true"></asp:TextBox>
                                                </td>
                                                <td style="width: 200px" align="left" valign="top">
                                                    <asp:Label ID="lblAddress" runat="server" Text="Postal Address Of Branch With Landmark & PinCode" Font-Size="8pt" Font-Bold="True"
                                                        ForeColor="navy"></asp:Label></td>
                                                <td align="left" valign="top">
                                                    <asp:TextBox ID="txtLocationAddress" runat="server" TextMode="MultiLine" Width="200px"
                                                        Height="50px"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="height: 5px" colspan="4">
                                                </td>
                                            </tr>
                                            <tr>
                                               
                                               
                                               
                                                <td align="left">
                                                    <asp:Label ID="lnlPhNo" runat="server" Text="Phone No." ForeColor="navy" Font-Size="8pt"
                                                        Font-Bold="True"></asp:Label></td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtLocationPhoneNo" runat="server" Width="200px" MaxLength="15" 
                                                        Onkeyup="Spc_validator1(this)"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtNodalOfficerPhoneNo"
                                        ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender>
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="Label14" runat="server" Text="Branch Manager CUG No." Font-Size="8pt"
                                                        Font-Bold="True" ForeColor="navy"></asp:Label></td>
                                              <%--  <td align="left">
                                                    <asp:TextBox ID="CUGtxt" runat="server" Width="200px" MaxLength="10" ></asp:TextBox>
                                                </td>--%>
                                                <td align="left">
                                                    <asp:TextBox ID="txtNodalOfficerPhoneNo" runat="server" Width="200px" 
                                                        MaxLength="11"></asp:TextBox>
                                                               <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" TargetControlID="txtNodalOfficerPhoneNo"
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
                                                    <asp:Label ID="lblFax" runat="server" Text="Fax No." Font-Size="8pt" Font-Bold="True"
                                                        ForeColor="navy"></asp:Label></td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtLocationFaxNo" runat="server" Width="200px" Onkeyup="Spc_validator1(this)"
                                                        MaxLength="11"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtLocationFaxNo"
                                        ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender>
                                                        </td>
                                                <td align="left">
                                                    <asp:Label ID="lblEmail" runat="server" Text="E-Mail Address" Font-Size="8pt" Font-Bold="True"
                                                        ForeColor="navy"></asp:Label></td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtLocationEMailAddress" runat="server" Width="200px" CausesValidation="True"></asp:TextBox></td>
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
                                                    <asp:Label ID="lblRailSliding" runat="server" Text="Rail Siding" Font-Size="8pt"
                                                        ForeColor="navy" Font-Bold="True"></asp:Label></td>
                                                <td align="left" colspan="3">
                                                    <asp:DropDownList ID="ddlRailSiding" runat="server" Width="205px" Height="25px">
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
                                                <td style="height: 5px" align="left">

                                                    <asp:Label ID="lblRailSliding0" runat="server" Text="Lincese No:" Font-Size="8pt"
                                                        ForeColor="Navy" Font-Bold="True"></asp:Label>

                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtlicNo" runat="server" Width="200px"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="lblRailSliding1" runat="server" Text="Lincese Expiry Date:" Font-Size="8pt"
                                                        ForeColor="Navy" Font-Bold="True"></asp:Label></td>
                                                <td align="left">
                                                
                                                    <asp:TextBox ID="txtlicDate" runat="server" Width="175px"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="txtlicDate_CalendarExtender" runat="server" 
                                                        Enabled="True" TargetControlID="txtlicDate">
                                                    </cc1:CalendarExtender>
                                                
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
                                                    <asp:DropDownList ID="IntConnctddl" runat="server" Width="205px" Height="25px"  AutoPostBack="True"
                                                        onselectedindexchanged="IntConnctddl_SelectedIndexChanged">
                                                        <asp:ListItem>--Select--</asp:ListItem>
                                                        <asp:ListItem Value="1" >Yes</asp:ListItem>
                                                        <asp:ListItem Value="0" >No</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                
                                                <td align="left">
                                                    <asp:Label ID="Label11" runat="server" Text="Type Of Connectivity" Font-Size="8pt" ForeColor="navy" Visible="false"
                                                        Font-Bold="True" ></asp:Label></td>
                                                <td align="left">
                                                    <asp:DropDownList ID="typeOfIntrConnctddl" runat="server" Width="205px" Height="25px" Visible="false">
                                                     <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                        <asp:ListItem Value="Fixed Broadband Connection" Text="Fixed Broadband Connection"></asp:ListItem>
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
                                                    <asp:DropDownList ID="elctWeighbridgeddl" runat="server" Width="205px" Height="25px">
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
                                                    <asp:Label ID="Label15" runat="server" Text="Hardware Availability" Font-Size="8pt"
                                                        Font-Bold="True" ForeColor="navy"></asp:Label></td>
                                                <td align="left">
                                                    <asp:DropDownList ID="HerdAvlddl" runat="server" Width="205px" Height="25px"  AutoPostBack="True"
                                                        onselectedindexchanged="HerdAvlddl_SelectedIndexChanged">
                                                        <asp:ListItem>--Select--</asp:ListItem>
                                                        <asp:ListItem Value="1" >Yes</asp:ListItem>
                                                        <asp:ListItem Value="2" >No</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            
                                            
                                                <td align="left">
                                                    <asp:Label ID="Label16" runat="server" Text="Year Of Installation" Font-Size="8pt" Visible="false"
                                                        Font-Bold="True" ForeColor="navy"></asp:Label></td>
                                                <td align="left">
                                                    <asp:DropDownList ID="YearOfIstallddl" runat="server" Width="205px" Height="25px" Visible="false"> 
                                                        <asp:ListItem>--Select--</asp:ListItem>
                                                        <asp:ListItem Value="2006-07">2006-07</asp:ListItem>
                                                        <asp:ListItem Value="2007-08">2007-08</asp:ListItem>
                                                        <asp:ListItem Value="2008-09">2008-09</asp:ListItem>
                                                        <asp:ListItem Value="2009-10">2009-10</asp:ListItem>
                                                        <asp:ListItem Value="2010-11">2010-11</asp:ListItem>
                                                        <asp:ListItem Value="2011-12">2011-12</asp:ListItem>
                                                        <asp:ListItem Value="2012-13">2012-13</asp:ListItem>
                                                        <asp:ListItem Value="2013-14">2013-14</asp:ListItem>
                                                        <asp:ListItem Value="2014-15">2014-15</asp:ListItem>
                                                        <asp:ListItem Value="2015-16">2015-16</asp:ListItem>
                                                        <asp:ListItem Value="2016-17">2016-17</asp:ListItem>
                                                        <asp:ListItem Value="2017-18">2017-18</asp:ListItem>                                                                                                                                                                        
                                                    </asp:DropDownList>
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
                        <td style="height: 5px" colspan="2">
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
                                                    <asp:Label ID="lblNodalOfficeDetails" runat="server" Font-Bold="True" Text="Branch Manager Details"
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
                                                    <asp:Label ID="lblAddress1" runat="server" Text=" Address" Font-Size="8pt" Font-Bold="True"
                                                        ForeColor="navy"></asp:Label></td>
                                                <td align="left" valign="top">
                                                    <asp:TextBox ID="txtNodalOfficerAddress" runat="server" TextMode="MultiLine" Width="200px"
                                                        Height="30px" onKeyDown="textCounter(this.form.txtNodalOfficerAddress,200);"
                                                        onKeyUp="textCounter(this.form.txtNodalOfficerAddress,200);"></asp:TextBox></td>
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
                                                    <asp:Label ID="lblMobileNo" runat="server" Text="Personal Mobile No." Font-Size="8pt" Font-Bold="True"
                                                        ForeColor="navy"></asp:Label></td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtNodalOfficerMobileNo" runat="server" Width="200px" MaxLength="11" 
                                                        ></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtNodalOfficerMobileNo"
                                        ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender>
                                                        </td>
                                                        <td align="left">
                                                    <asp:Label ID="lblEmail1" runat="server" Text="Personal E-Mail ID" Width="142px" Font-Size="8pt" Font-Bold="True"
                                                        ForeColor="navy"></asp:Label></td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtNodalOfficerEmailAddress" runat="server" Width="200px"
                                                        Font-Names="Verdana" Font-Size="9pt" TabIndex="12"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="height: 5px" colspan="4">
                                                </td>
                                                
                                            </tr>
                                            <tr>
                                                
                                                <td align="left">
                                                    <asp:Label ID="lblFax1" runat="server" Text="Fax No." Width="91px" Visible="False"
                                                        Font-Names="Verdana" Font-Size="7pt" Font-Bold="True"></asp:Label></td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtNodalOfficerFaxNo" runat="server" Width="200px" Visible="False"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="height: 5px" colspan="4">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" valign="top">
                                                    <asp:Label ID="lblRemarks" runat="server" Text="Remarks" Font-Size="8pt" ForeColor="navy"
                                                        Font-Bold="True"></asp:Label></td>
                                                <td colspan="3" align="left">
                                                    <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Width="690px" onKeyDown="textCounter(this.form.txtRemarks,200);"
                                                        onKeyUp="textCounter(this.form.txtRemarks,200);"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="height: 5px" colspan="4">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4">
                                                    <asp:RegularExpressionValidator ID="LocationEmail" Display="Static" ControlToValidate="txtLocationEMailAddress"
                                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" runat="server"
                                                        Font-Size="10pt" ErrorMessage="  **Enter Valid Location E-Mail Address" Font-Names="Verdana"
                                                        SetFocusOnError="True" ValidationGroup="validemail"></asp:RegularExpressionValidator>
                                                    <asp:RegularExpressionValidator ID="NodalOfficerEmailAddress" Display="Static" ControlToValidate="txtNodalOfficerEmailAddress"
                                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" runat="server"
                                                        Font-Size="Small" ErrorMessage="  **Enter Valid Nodal Officer  E-Mail Address"></asp:RegularExpressionValidator></td>
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
                        <td style="height: 5px" colspan="2">
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
                                                    <asp:Label ID="Label7" runat="server" Text="Operator Name" ForeColor="navy" Font-Size="8pt" Width="142px"
                                                        Font-Bold="True"></asp:Label></td>
                                                <td align="left">
                                                    <asp:TextBox ID="oprtnametxt" runat="server" Width="198px" MaxLength="20"
                                                        Onkeyup="Spc_validator1(this)"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="Label8" runat="server" Text="Mobile No." Font-Size="8pt" Font-Bold="True" Width="142px"
                                                        ForeColor="navy"></asp:Label></td>
                                                <td align="left">
                                                    <asp:TextBox ID="oprtmobiletxt" runat="server" Width="200px"
                                                        MaxLength="10" ></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="oprtmobiletxt"
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
                                                    <asp:Label ID="Label10" runat="server" Text="E-Mail Address" Font-Size="8pt" Font-Bold="True"
                                                        ForeColor="navy"></asp:Label></td>
                                                <td align="left">
                                                    <asp:TextBox ID="oprtemailtxt" runat="server" Width="198px" CausesValidation="True"></asp:TextBox></td>                                            
                                                <td align="left">
                                                    <asp:Label ID="Label9" runat="server" Text="Qualification" Font-Size="8pt"
                                                        Font-Bold="True" ForeColor="navy"></asp:Label></td>
                                                <td align="left" colspan="3">
                                                    <asp:DropDownList ID="OprtQulddl" runat="server" Width="205px" Height="25px">
                                                        <asp:ListItem >--Select--</asp:ListItem>
                                                        <asp:ListItem Value="High School" Text="High School"></asp:ListItem>
                                                        <asp:ListItem Value="Diploma" Text="Diploma"></asp:ListItem>
                                                        <asp:ListItem Value="Graduation" Text="Graduation"></asp:ListItem>
                                                    </asp:DropDownList>
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
                        <td style="height: 5px" colspan="2">
                        </td>
                    </tr>                                        
                    <tr>
                        <td colspan="4" align="center">
                            <asp:Button ID="btnupdate" runat="server" Text="Save" Width="100px" CssClass="BTNBLUE"
                                ValidationGroup="validate" OnClick="btnupdate_Click" />
                            &nbsp; &nbsp; &nbsp;
                            <asp:Button ID="btn_Close" runat="server" Text="Close" Width="100px" CssClass="BTNBLUE"
                                CausesValidation="false" OnClick="btn_Close_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 5px" colspan="2">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:Panel ID="pnlPass" runat="server" Width="409px" Visible="False">
                                <table width="60%">
                                    <tr>
                                        <td colspan="3" style="height: 14px">
                                            <asp:Label ID="Label1" runat="server" Text="Save Password For Branch" Font-Bold="True"
                                                ForeColor="Maroon"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100px" align="left">
                                            <asp:Label ID="Label2" runat="server" Text="Branch Name:" Font-Bold="True" Width="101px"></asp:Label></td>
                                        <td align="left" colspan="2">
                                            <asp:Label ID="lblDepot" runat="server" Font-Bold="True" Width="142px"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100px" align="left">
                                            <asp:Label ID="Label3" runat="server" Text="Password:" Font-Bold="True"></asp:Label></td>
                                        <td style="width: 100px" align="left">
                                            <asp:Label ID="lblPass" runat="server" Font-Bold="True">*********</asp:Label></td>
                                        <td style="width: 100px">
                                        </td>
                                    </tr>
                                </table>
                                <asp:Button ID="btnSavePass" runat="server" OnClick="btnSavePass_Click" Text="Save  Password "
                                    OnClientClick="javascript:MDS(this);" /></asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 5px" colspan="2">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:HiddenField ID="txtEncrypted" runat="server" />
                            <asp:HiddenField ID="txtPwd" runat="server" />
                            <asp:HiddenField ID="txtUID" runat="server" />
                            <asp:HiddenField ID="txtName" runat="server" />
                            <asp:HiddenField ID="txtMEncrypted" runat="server" />
                            <asp:LinkButton ID="linkEncpass" runat="server" ForeColor="Maroon" OnClick="linkEncpass_Click">Pending Branch Password </asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 5px" colspan="2">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="center">
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="validate"
                                ShowMessageBox="true" ShowSummary="false" />
                            <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Visible="true" Font-Size="10pt"
                                Font-Bold="True"></asp:Label></td>
                    </tr>
                </table>
            </div>
        </center>
    </fieldset>
</asp:Content>
