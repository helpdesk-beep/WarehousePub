<%@ Page Language="C#" MasterPageFile="~/MasterPage/PrivateWarehouse.master" AutoEventWireup="true" CodeFile="PvtGodownProfile.aspx.cs" Inherits="WarehouseLevel_PvtGodownProfile" Title="Pvt Godown Profile" %>

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
                                                    <asp:Label ID="lblDepotMaster" runat="server" Text="Godown Profile" ForeColor="whitesmoke"
                                                        Font-Size="12pt" Font-Bold="true"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 5px" colspan="4">
                                                <p style=" color:Red; font-size:small">यदि आपने Warehouse Incharge की प्रोफ़ाइल अन्य गोदाम के लिए पहले बना चुके है तब नीचे दिये Warehouse Incharge लिस्ट मे से नाम का चयन करे and Update करे</p>
                                                </td>
                                            </tr>
                                            <tr>
                                                
                                                
                                            </tr>
                                            <tr>
                                                <td align="left" style="width: 200px">
                                                    <asp:Label ID="lblState" runat="server" Text="State Name " Font-Bold="True" Font-Size="8pt"
                                                        ForeColor="navy"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:DropDownList ID="ddlStateName" runat="server" Width="205px" AutoPostBack="True"
                                                        Height="25px">
                                                    </asp:DropDownList>
                                                </td>
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
                                            </tr>
                                            <tr>
                                                <td style="height: 5px" colspan="4">
                                                </td>
                                            </tr>
                                            <tr>
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
                                            </tr>
                                            <tr>
                                                <td style="height: 5px" colspan="4">
                                                </td>
                                            </tr>
                                            <tr>
                                             
                                              
                                                 <td>
                                                    <asp:Label ID="Label5" runat="server" Text="Organization/Company Name" Font-Size="8pt" Font-Bold="True"
                                                        ForeColor="navy"></asp:Label></td>
                                                <td>
                                                    <asp:TextBox ID="txtOrg" runat="server" Width="200px" MaxLength="100" Visible="true"></asp:TextBox></td>
                                                    <td style="width: 200px">
                                                    <asp:Label ID="Label4" runat="server" Text="Godown Incharge Name" Font-Bold="True" Font-Size="8pt"
                                                        ForeColor="navy"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlWManager" runat="server" Width="205px" AutoPostBack="True"
                                                        Height="25px" onselectedindexchanged="ddlWManager_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 5px" colspan="4">
                                                </td>
                                            </tr>
                                            <tr id="type1" runat="server" visible="false">
                                                <%--<td align="left">
                                                    <asp:Label ID="lblDepotType" runat="server" Text="Godown Type" Font-Size="8pt" ForeColor="navy"
                                                        Font-Bold="True" Visible="False"></asp:Label></td>
                                                <td align="left">
                                                    <asp:DropDownList ID="ddlDepoType" runat="server" Width="205px" Height="25px" Visible="False">
                                                        <asp:ListItem Value="0">Open</asp:ListItem>
                                                        <asp:ListItem Value="1">Covered</asp:ListItem>
                                                        <asp:ListItem Value="2">Plinth</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>--%>
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
                                                    <asp:Label ID="lblDepotDetails" runat="server" Font-Bold="True" Text="Godown Details"
                                                        ForeColor="WhiteSmoke" Font-Size="12pt"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 5px">
                                                </td>
                                            </tr>
                                            <tr>
                                               <td align="left" style="width: 200px" valign="top">
                                                    <asp:Label ID="Label7" runat="server" Text="Owner Name" Font-Size="8pt"
                                                        Font-Bold="True" ForeColor="Navy"></asp:Label></td>
                                                <td align="left" valign="top">
                                                    <asp:TextBox ID="txtAPN" runat="server" Width="200px"></asp:TextBox></td>
                                                <td style="width: 200px" align="left" valign="top">
                                                    <asp:Label ID="lblAddress" runat="server" Text=" Address" Font-Size="8pt" Font-Bold="True"
                                                        ForeColor="navy"></asp:Label></td>
                                                <td align="left" valign="top">
                                                    <asp:TextBox ID="txtLocationAddress" runat="server" TextMode="MultiLine" Width="200px"
                                                        Height="30px"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="height: 5px" colspan="4">
                                                </td>
                                            </tr>
                                           <tr>
                                                <td>
                                                    <asp:Label ID="Label17" runat="server" Font-Bold="True" Font-Size="8pt" ForeColor="Navy" Text="Tehsil"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlPBlock" runat="server" AutoPostBack="true"  Width="190px" Height="25px" OnSelectedIndexChanged="ddlPBlock_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                                 <td>
                                                    <asp:Label ID="Label18" runat="server" Font-Bold="True" Font-Size="8pt" ForeColor="Navy" Text="Village"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlVillage" runat="server" Width="190px" Height="25px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 5px" colspan="4">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
<asp:Label ID="Label12" runat="server" Text="Latitude" Font-Bold="True" ForeColor="Navy"
                                                        Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td><asp:TextBox ID="txtlatitude" runat="server"></asp:TextBox>ex:26.203194</td>
                                                 <td>
<asp:Label ID="Label13" runat="server" Text="Longitude" Font-Bold="True" ForeColor="Navy"
                                                        Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td><asp:TextBox ID="txtlongitude" runat="server"></asp:TextBox>ex:78.209267</td>
                                            </tr>
                                            <tr>
                                                <td style="height: 5px" colspan="4">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lnlPhNo" runat="server" Text="Mobile No." ForeColor="navy" Font-Size="8pt"
                                                        Font-Bold="True"></asp:Label></td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtLocationPhoneNo" runat="server" Width="200px" MaxLength="15"
                                                        Onkeyup="Spc_validator1(this)"></asp:TextBox>
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
                                                    <asp:Label ID="lblDepotCapacity" runat="server" Text="Godown Capacity" Font-Size="8pt" Enabled="false" 
                                                        Font-Bold="True" ForeColor="navy"></asp:Label></td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtDepoCapicity" runat="server" Width="200px" onkeyup="NumericDecimalCheck(this,2)" Enabled="false"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="Label6" runat="server" Text="Godown No" Font-Size="8pt"
                                                        Font-Bold="True" ForeColor="navy"></asp:Label>
                                                         
                                                        </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtGodownNo" runat="server" Width="200px"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtGodownNo"
                                        ValidChars="0123456789/">
                                    </cc1:FilteredTextBoxExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 5px" colspan="4">
                                                </td>
                                            </tr>
                                            <%--<tr visible="false">
                                                <td align="left">
                                                    <asp:Label ID="lblRailSliding" runat="server" Text="Rail Siding" Font-Size="8pt"
                                                        ForeColor="navy" Font-Bold="True"></asp:Label></td>
                                                <td align="left" colspan="3">
                                                    <asp:DropDownList ID="ddlRailSiding" runat="server" Width="205px" Height="25px">
                                                        <asp:ListItem Value="Y">Yes</asp:ListItem>
                                                        <asp:ListItem Value="N">No</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>--%>
                                           
                                            <tr>
                                                <td style="height: 5px" align="left">

                                                    <asp:Label ID="lblRailSliding0" runat="server" Text="Lincese No:" Font-Size="8pt"
                                                        ForeColor="Navy" Font-Bold="True"></asp:Label>

                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtlicNo" runat="server"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="lblRailSliding1" runat="server" Text="Lincese Date:" Font-Size="8pt"
                                                        ForeColor="Navy" Font-Bold="True"></asp:Label></td>
                                                <td align="left">
                                                
                                                    <asp:TextBox ID="txtlicDate" runat="server"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="txtlicDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" 
                                                        Enabled="True" TargetControlID="txtlicDate">
                                                    </cc1:CalendarExtender>
                                                
                                                </td>
                                            </tr>
                                            <tr>
    <td>
<asp:Label ID="lblPan" runat="server" Visible="true" Text="PAN No." Font-Bold="True" Font-Size="8pt" 
                                                                ForeColor="Navy"></asp:Label>
</td>
<td>
<asp:TextBox runat="server" ID="txtPan" Visible="true" ReadOnly="false" 
        AutoPostBack="false" 
        TabIndex="9" Width="190px" Height="20px" 
        ></asp:TextBox>
</td>
<td>
            <asp:Label ID="lblBank" Visible="true" runat="server" Text="Name of Bank" Font-Size="8pt" Font-Bold="true" ForeColor="navy"></asp:Label></td>
        <td>
         <asp:DropDownList ID="ddlBank" runat="server" AutoPostBack="false" TabIndex="1" 
                    Height="25px" Width="200px" CssClass="tb6" Font-Size="10pt"
               >
            </asp:DropDownList>
 </td>
    </tr>
    <tr>
    <td>
<asp:Label ID="lblBranch" runat="server" Visible="true" Text="Bank Branch Add." Font-Bold="True" Font-Size="8pt" 
                                                                ForeColor="Navy"></asp:Label>
</td>
<td>
<asp:TextBox runat="server" ID="txtBAdd" Visible="true" ReadOnly="false" 
        AutoPostBack="false" 
        TabIndex="9" Width="190px" Height="20px" 
        ></asp:TextBox>
</td>
<td>
            <asp:Label ID="lblAno" Visible="true" runat="server" Text="Account No." Font-Size="8pt" Font-Bold="true" ForeColor="navy"></asp:Label></td>
        <td>
         <asp:TextBox runat="server" ID="txtAcc" Visible="true" ReadOnly="false"
        AutoPostBack="false" 
        TabIndex="9" Width="190px" Height="20px" TextMode="SingleLine"
        ></asp:TextBox>
 </td>
    </tr>
    <tr>
    <td>
            <asp:Label ID="lblIfsc" Visible="true" runat="server" Text="IFSC Code." Font-Size="8pt" Font-Bold="true" ForeColor="navy"></asp:Label></td>
        <td>
         <asp:TextBox runat="server" ID="txtIfsc" Visible="true" ReadOnly="false" 
        AutoPostBack="false"  
        TabIndex="9" Width="190px" Height="20px" 
        ></asp:TextBox>
 </td>

    </tr>
     <tr>
                                                <td>
                                                    <asp:Label ID="Label15" runat="server" Font-Bold="True" Font-Size="8pt" ForeColor="Navy" Text="Khasra No."></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtkhasra" runat="server">0</asp:TextBox>
                                                    
                                                </td>
                                                 <td>
                                                    <asp:Label ID="Label16" runat="server" Font-Bold="True" Font-Size="8pt" ForeColor="Navy" Text="Rakba No."></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtrakwa" runat="server">0</asp:TextBox>
                                                   
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
                                                    <asp:Label ID="lblNodalOfficeDetails" runat="server" Font-Bold="True" Text="Godown Incharge Details"
                                                        ForeColor="WhiteSmoke" Font-Size="12pt"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 5px" colspan="4">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" style="width: 200px" valign="top">
                                                    <asp:Label ID="lblNodalOffice" runat="server" Text="Godown/Silo Incharge(गोदामपाल)" Font-Size="8pt"
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
                                                <%--<td align="left">
                                                    <asp:Label ID="lblPhNo1" runat="server" Text="Phone No." Font-Size="8pt" ForeColor="navy"
                                                        Font-Bold="True"></asp:Label></td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtNodalOfficerPhoneNo" runat="server" Width="200px" Onkeyup="Spc_validator1(this)"
                                                        MaxLength="15"></asp:TextBox></td>--%>
                                                <td align="left">
                                                    <asp:Label ID="lblMobileNo" runat="server" Text="Mobile No." Font-Size="8pt" Font-Bold="True"
                                                        ForeColor="navy"></asp:Label></td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtNodalOfficerMobileNo" runat="server" Width="200px" MaxLength="11"
                                                        Onkeyup="Spc_validator1(this)"></asp:TextBox></td>
                                                        <td align="left">
                                                    <asp:Label ID="lblEmail1" runat="server" Text="E-Mail Address" Width="142px" Visible="true"
                                                        Font-Size="8pt" ForeColor="navy"
                                                        Font-Bold="True"></asp:Label></td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtNodalOfficerEmailAddress" runat="server" Width="198px" Visible="true"
                                                        Font-Names="Verdana" Font-Size="9pt" Height="25px" TabIndex="12"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="height: 5px" colspan="4">
                                                </td>
                                            </tr>
                                            <tr>
                                                
                                                <%--<td align="left">
                                                    <asp:Label ID="lblFax1" runat="server" Text="Fax No." Width="91px" Visible="true"
                                                        Font-Size="8pt" ForeColor="navy"
                                                        Font-Bold="True"></asp:Label></td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtNodalOfficerFaxNo" runat="server" Width="200px" Visible="true"></asp:TextBox></td>--%>
                                            </tr>
                                            <tr>
                                                <td style="height: 5px" colspan="4">
                                                </td>
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
                        &nbsp;</td></tr><tr>
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
                            <asp:LinkButton ID="linkEncpass" runat="server" Visible="false" ForeColor="Maroon" OnClick="linkEncpass_Click">Pending Branch Password </asp:LinkButton>
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
&nbsp;</asp:Content>