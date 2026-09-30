<%@ Page Language="C#" MasterPageFile="~/MasterPage/WarehouseApplication.master" AutoEventWireup="true" CodeFile="DepotProfile.aspx.cs" Inherits="State_DepotProfile" Title="Depot Profile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width :500px ;background-image: url(../images/images[26].jpg);">
                    <tr>
                        <td colspan="4" class="HeadingBlue" style="background-color: dimgray; height: 15px;">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center; height:20px;border-collapse: collapse; border:solid 1px white;">
                            <strong><span style="font-size: 8pt; color: #990033">
                                <asp:Label ID="lblDepotMaster" runat="server" Text="Branch Master"></asp:Label></span></strong></td>
                    </tr>
                    
                    <tr>
                        <td style="width: 196px; height: 27px; text-align: left;border-collapse: collapse; border:solid 1px white;" >
                            <span style="font-size: 9pt; font-family: Verdana">
                                <asp:Label ID="lblState" runat="server" Text="State Name " Font-Bold="True" Font-Names="Verdana" Font-Size="7pt"></asp:Label></span></td>
                        <td style="width: 144px; height: 27px;border-collapse: collapse; border:solid 1px white;" >
                                <asp:DropDownList ID="ddlStateName" runat="server" Width="202px"  AutoPostBack="True"  Font-Names="Verdana" Font-Size="9pt" CssClass="dropdownSml">
                                </asp:DropDownList>
                            </td>
                        <td style="width: 48px; height: 27px;border-collapse: collapse; border:solid 1px white;" >
                            <asp:Label ID="lblRegion" runat="server" Text="Region" Font-Names="Verdana" Font-Size="7pt" Font-Bold="True"></asp:Label></td>
                        <td style="border-collapse: collapse; border:solid 1px white;">
                        <asp:DropDownList ID="ddlRegion" runat="server" Width="202px" AutoPostBack="True" CssClass="dropdownSml">
                            
                        </asp:DropDownList>
                            </td>
                    </tr>
                    <tr>
                        <td style="width: 196px; height: 24px; text-align: left;border-collapse: collapse; border:solid 1px white;" >
                            <asp:Label ID="lblDistrict" runat="server" Text="District Name" Width="112px" Font-Size="7pt" Font-Names="Verdana" Font-Bold="True"></asp:Label></td>
                        <td style="width: 144px; height: 24px;border-collapse: collapse; border:solid 1px white;" >
                                <asp:DropDownList ID="ddlDistrictName" runat="server"  Width="203px" Font-Names="Verdana" Font-Size="9pt" CssClass="dropdownSml">
                                
                                </asp:DropDownList>
                            </td>
                        <td style="width: 48px; height: 24px;border-collapse: collapse; border:solid 1px white;" >
                                <asp:Label ID="lblDepotName" runat="server" Text="Branch Name" Width="121px" Font-Names="Verdana" Font-Size="7pt" Font-Bold="True"></asp:Label></td>
                        <td style="width: 412px; height: 24px;border-collapse: collapse; border:solid 1px white;">
                                <asp:TextBox ID="txtLocationName" runat="server" Width="196px"  BorderColor="White" Font-Names="Verdana" Font-Size="9pt" MaxLength="100" Onkeyup = "Spc_validator(this)" Visible="true"></asp:TextBox></td>
                            <asp:DropDownList ID="ddlTehsilName" runat="server"  Width="203px" Visible="false" Font-Names="Verdana" Font-Size="9pt" CssClass="dropdownSml" AutoPostBack="True">
                                </asp:DropDownList></tr>
                    <tr>
                    <td style="border-top-width: thin; width: 196px; border-collapse: collapse; border:solid 1px white;" >
                            <asp:Label ID="lblDepotBelongs" runat="server" Text="Depot Belongs To" Width="152px" Font-Names="Verdana" Font-Size="7pt" Font-Bold="True"></asp:Label></td>
                        <td style="border-top-width: thin; width: 144px; border-collapse: collapse; border:solid 1px white;
                            text-align: left" >
                            <asp:DropDownList ID="ddlDepoBeloggsTo" runat="server" Width="203px" Font-Names="Verdana" Font-Size="9pt" CssClass="dropdownSml">
                                <asp:ListItem>FCI</asp:ListItem>
                                <asp:ListItem>CWC</asp:ListItem>
                                <asp:ListItem>SWC</asp:ListItem>
                                <asp:ListItem>MarkFed</asp:ListItem>                                
                                <asp:ListItem>CSC</asp:ListItem>
                                <asp:ListItem>MPWLC</asp:ListItem>
                                
                            </asp:DropDownList>
                            </td>
                        
                        <td style="border-top-width: thin; width: 48px; border-collapse: collapse; border:solid 1px white;" >
                                <asp:Label ID="lblHiredType" runat="server" Text="Hired Type" Font-Size="7pt" Font-Names="Verdana" Font-Bold="True" Visible="False" Width="124%"></asp:Label></td>
                        <td style="border-top-width: thin; width: 412px; border-collapse: collapse; border:solid 1px white;">
                                <asp:DropDownList ID="ddlCategory" runat="server" Width="202px" Font-Names="Verdana" Font-Size="9pt" CssClass="dropdownSml" Visible="False">
                                    <asp:ListItem Value="0">Owned</asp:ListItem>
                                    <asp:ListItem Value="1">Hired</asp:ListItem>
                                    <asp:ListItem Value="2">Joint Venture</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                    </tr>
                    <tr id="type1" runat="server" visible="false">
                        <td style="border-top-width: thin; width: 196px; border-collapse: collapse; border:solid 1px white;" >
                            &nbsp;<asp:Label ID="lblDepotType" runat="server" Text="Branch Type" Width="103px" Font-Size="7pt" Font-Names="Verdana" Font-Bold="True" Visible="False"></asp:Label></td>
                        <td style="border-top-width: thin; width: 144px; border-collapse: collapse; border:solid 1px white;
                            text-align: left" >
                                <asp:DropDownList ID="ddlDepoType" runat="server" Width="203px" Font-Names="Verdana" Font-Size="9pt" CssClass="dropdownSml" Visible="False">
                                    <asp:ListItem Value="0">Open</asp:ListItem>
                                    <asp:ListItem Value="1">Covered</asp:ListItem>
                                    <asp:ListItem Value="2">Plinth</asp:ListItem>
                                </asp:DropDownList>
                            </td></tr>
                    <tr>
                        <td colspan="4" style="height: 17px; border-collapse: collapse; border:solid 1px white; text-align: center;" >
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                    <asp:Label ID="lblDepotDetails" runat="server" Font-Bold="True" Text="Depot Details" ForeColor="Black" Font-Names="Verdana" Font-Size="10pt"></asp:Label>
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;<br />
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp;&nbsp;
                            &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="border-top-width: thin; width: 196px; border-collapse: collapse; border:solid 1px white;" >
                                <asp:Label ID="lblTehsil_Block" runat="server" Text="Tehsil/Block Name" Width="158px" Font-Size="7pt" Font-Names="Verdana" Font-Bold="True"></asp:Label></td>
                        <td style="border-top-width: thin; width: 144px; border-collapse: collapse; border:solid 1px white;
                            text-align: left" >
                            <asp:TextBox ID="txtTehsilName" runat="server"  Width="198px" Font-Names="Verdana" Font-Size="9pt" MaxLength="15" Onkeyup = "Spc_validator(this)" Visible="true" TabIndex="1"></asp:TextBox><%--<asp:DropDownList ID="ddl_depo" runat="server" Width="202px"  AutoPostBack="false" Visible="false"  Font-Names="Verdana" Font-Size="9pt" CssClass="dropdownSml" >
                                </asp:DropDownList>--%></td>
                        <td style="border-top-width: thin; width: 196px; border-collapse: collapse; border:solid 1px white;" >
                                <asp:Label ID="lblAddress" runat="server" Text=" Address" Width="139px" Font-Names="Verdana" Font-Size="7pt" Font-Bold="True"></asp:Label></td>
                        <td style="border-top-width: thin; width: 412px; border-collapse: collapse; border:solid 1px white;" dir="ltr">
                                <asp:TextBox ID="txtLocationAddress" runat="server" TextMode="MultiLine" Width="198px" Font-Names="Verdana" Font-Size="9pt" 
 TabIndex="2"
></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="border-top-width: thin; width: 196px; border-collapse: collapse; border:solid 1px white;" >
                                <asp:Label ID="lnlPhNo" runat="server" Text="Phone No." Font-Names="Verdana" Font-Size="7pt" Font-Bold="True"></asp:Label></td>
                        <td style="border-top-width: thin; width: 144px; border-collapse: collapse; border:solid 1px white;
                            text-align: left">
                                <asp:TextBox ID="txtLocationPhoneNo" runat="server" Width="198px" Font-Names="Verdana" Font-Size="9pt" MaxLength="15" Onkeyup = "Spc_validator1(this)" TabIndex="3"></asp:TextBox>&nbsp;
                        </td>
                        <td style="border-top-width: thin; width: 48px; border-collapse: collapse; border:solid 1px white;">
                                <asp:Label ID="lblFax" runat="server" Text="Fax No." Font-Names="Verdana" Font-Size="7pt" Font-Bold="True"></asp:Label></td>
                        <td style="border-top-width: thin; width: 412px; border-collapse: collapse; border:solid 1px white;">
                                <asp:TextBox ID="txtLocationFaxNo" runat="server" Width="198px" Font-Names="Verdana" Font-Size="9pt" Onkeyup = "Spc_validator1(this)" MaxLength="15" TabIndex="4"></asp:TextBox></td>
                    </tr>
                    <tr>
                        
                        <td style="border-top-width: thin; width: 48px; border-collapse: collapse; border:solid 1px white;">
                            <asp:Label ID="lblDepotCapacity" runat="server" Text="Branch Capicity" Width="147px" Font-Names="Verdana" Font-Size="7pt" Font-Bold="True"></asp:Label></td>
                        <td style="border-top-width: thin; width: 412px; border-collapse: collapse; border:solid 1px white;">
                            <asp:TextBox ID="txtDepoCapicity" runat="server" Width="198px" Font-Names="Verdana" Font-Size="9pt" onkeyup= "NumericDecimalCheck(this,2)" TabIndex="5"></asp:TextBox></td>
                            <td style="border-top-width: thin; width: 48px; border-collapse: collapse; border:solid 1px white;">
                                <asp:Label ID="lblEmail" runat="server" Text="E-Mail Address" Width="142px" Font-Names="Verdana" Font-Size="7pt" Font-Bold="True"></asp:Label></td>
                                <td style="border-top-width: thin; width: 196px; border-collapse: collapse; border:solid 1px white;">
                                <asp:TextBox ID="txtLocationEMailAddress" runat="server" Width="198px" Font-Names="Verdana" Font-Size="9pt" TabIndex="6" CausesValidation="True"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="border-top-width: thin; width: 196px; border-collapse: collapse; border:solid 1px white;" >
                            <asp:Label ID="lblRailSliding" runat="server" Text="Rail Siding" Font-Names="Verdana" Font-Size="7pt" Font-Bold="True"></asp:Label></td>
                        <td style="border-top-width: thin; width: 144px; border-collapse: collapse; border:solid 1px white;
                            text-align: left" >
                            <asp:DropDownList ID="ddlRailSiding" runat="server" Width="202px" Font-Names="Verdana" Font-Size="9pt" CssClass="dropdownSml" TabIndex="7">
                                <asp:ListItem Value="Y">Yes</asp:ListItem>
                                <asp:ListItem Value="N">No</asp:ListItem>
                            </asp:DropDownList>
                            </td>
                        <td style="border-top-width: thin; width: 48px; border-collapse: collapse; border:solid 1px white;" >
                                </td>
                        <td style="border-top-width: thin; width: 412px; border-collapse: collapse; border:solid 1px white;">
                                </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="height: 13px; border-collapse: collapse; border:solid 1px white; text-align: center;" >
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                    <asp:Label ID="lblNodalOfficeDetails" runat="server" Font-Bold="True" Text="Nodal Officer Details" ForeColor="Black" Font-Names="Verdana" Font-Size="10pt"></asp:Label>
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp;
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="border-top-width: thin; width: 196px; border-collapse: collapse; border:solid 1px white;" >
                                <asp:Label ID="lblNodalOffice" runat="server" Text="Nodal Officer Name" Width="162px" Font-Names="Verdana" Font-Size="7pt" Font-Bold="True"></asp:Label></td>
                        <td style="border-top-width: thin; width: 144px; border-collapse: collapse; border:solid 1px white;
                            text-align: left">
                                <asp:TextBox ID="txtNodalOfficerName" runat="server" Height="25px" Width="198px" Font-Names="Verdana" Font-Size="9pt" Onkeyup = "Spc_validator(this)" TabIndex="8"></asp:TextBox></td>
                        <td style="border-top-width: thin; width: 48px; border-collapse: collapse; border:solid 1px white;">
                                <asp:Label ID="lblAddress1" runat="server" Text=" Address" Width="139px" Font-Names="Verdana" Font-Size="7pt" Font-Bold="True"></asp:Label></td>
                        <td style="border-top-width: thin; width: 412px; border-collapse: collapse; border:solid 1px white;">
                                <asp:TextBox ID="txtNodalOfficerAddress" runat="server" TextMode="MultiLine" Width="198px" Font-Names="Verdana" Font-Size="9pt" onKeyDown="textCounter(this.form.txtNodalOfficerAddress,200);"
onKeyUp="textCounter(this.form.txtNodalOfficerAddress,200);" TabIndex="9"

></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="border-top-width: thin; width: 196px; border-collapse: collapse; border:solid 1px white;">
                            <asp:Label ID="lblPhNo1" runat="server" Text="Phone No." Font-Names="Verdana" Font-Size="7pt" Font-Bold="True"></asp:Label></td>
                        <td style="border-top-width: thin; width: 144px; border-collapse: collapse; border:solid 1px white;
                            text-align: left">
                            <asp:TextBox ID="txtNodalOfficerPhoneNo" runat="server" Height="25px" Width="198px" Font-Names="Verdana" Font-Size="9pt" Onkeyup = "Spc_validator1(this)" MaxLength="15" TabIndex="10"></asp:TextBox></td>
                        <td style="border-top-width: thin; width: 48px; border-collapse: collapse; border:solid 1px white;">
                            <asp:Label ID="lblMobileNo" runat="server" Text="Mobile No." Font-Names="Verdana" Font-Size="7pt" Font-Bold="True" Width="123%"></asp:Label></td>
                        <td style="border-top-width: thin; width: 412px; border-collapse: collapse; border:solid 1px white;">
                            <asp:TextBox ID="txtNodalOfficerMobileNo" runat="server" Height="25px" Width="198px" Font-Names="Verdana" Font-Size="9pt" MaxLength="11" Onkeyup = "Spc_validator1(this)" TabIndex="11"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="border-top-width: thin; width: 113px; border-collapse: collapse; border:solid 1px white;" >
                                <asp:Label ID="lblEmail1" runat="server" Text="E-Mail Address" Width="142px" Visible="False" Font-Names="Verdana" Font-Size="7pt" Font-Bold="True"></asp:Label></td>
                        <td style="border-top-width: thin; width: 144px; border-collapse: collapse; border:solid 1px white;">
                                <asp:TextBox ID="txtNodalOfficerEmailAddress" runat="server" Width="198px" Visible="False" Font-Names="Verdana" Font-Size="9pt" Height="25px" TabIndex="12"></asp:TextBox></td>
                        <td style="border-top-width: thin; width: 48px; border-collapse: collapse; border:solid 1px white;" >
                                <asp:Label ID="lblFax1" runat="server" Text="Fax No." Width="91px" Visible="False" Font-Names="Verdana" Font-Size="7pt" Font-Bold="True"></asp:Label></td>
                        <td style="border-top-width: thin; width: 412px; border-collapse: collapse; border:solid 1px white;">
                                <asp:TextBox ID="txtNodalOfficerFaxNo" runat="server" Width="198px" Visible="False" Font-Names="Verdana" Font-Size="9pt" TabIndex="13"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="border-top-width: thin; width: 107px; border-collapse: collapse; border:solid 1px white; text-align: left; ">
                            <asp:Label ID="lblRemarks" runat="server" Text="Remarks" Font-Names="Verdana" Font-Size="7pt" Font-Bold="True"></asp:Label></td>
                        <td colspan="3" style="border-top-width: thin; border-collapse: collapse; border:solid 1px white; text-align: left; ">
                            <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Width="564px" Font-Names="Verdana" Font-Size="9pt" onKeyDown="textCounter(this.form.txtRemarks,200);"
onKeyUp="textCounter(this.form.txtRemarks,200);" TabIndex="14"
></asp:TextBox></td>
                    </tr>
        <tr>
            <td  style="border-collapse: collapse; border:solid 1px white; text-align: center;" colspan="4">
                <asp:RegularExpressionValidator ID="LocationEmail" Display="Static" ControlToValidate="txtLocationEMailAddress"
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" runat="server"
                    Font-Size="10pt" ErrorMessage="  **Enter Valid Location E-Mail Address" Font-Names="Verdana" SetFocusOnError="True" ValidationGroup="validemail"></asp:RegularExpressionValidator>
                <asp:RegularExpressionValidator ID="NodalOfficerEmailAddress" Display="Static" ControlToValidate="txtNodalOfficerEmailAddress"
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" runat="server"
                    Font-Size="Small" ErrorMessage="  **Enter Valid Nodal Officer  E-Mail Address"></asp:RegularExpressionValidator></td>
        </tr>
        <tr>
            <td style="border-collapse: collapse; border:solid 1px white; text-align: center;"
                 colspan="4">
                &nbsp
                    <%--<asp:Button ID="btnSave" runat="server" Text="Save" Font-Bold="True" Width="77px"
                    OnClick="btnSave_Click" BackColor="Silver" Font-Size="X-Small" BorderStyle="Solid"
                    BorderWidth="1px" CssClass="buttonBig"/>--%>
                    <asp:Button ID="btnupdate" runat="server" Text="Save" Width="77px"  BackColor="Silver" Font-Size="X-Small" BorderStyle="Solid"
                    BorderWidth="1px" CssClass="buttonBig" TabIndex="15" CausesValidation="False" ValidationGroup="validate" />
                </td>
        </tr>
        <tr>
            <td colspan="4" style="border-collapse: collapse; border:solid 1px white; text-align: center" >
                &nbsp;<asp:Label ID="lblMsg" runat="server" ForeColor="Red" Visible="true" Font-Size="X-Small"
                    Font-Bold="True"></asp:Label></td>
        </tr>
        <tr style=" border-collapse: collapse; border:solid 1px white" class="HeadingBlue">
                <td colspan="4" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid; border-collapse: collapse; height: 15px; text-align: center; background-color: dimgray;">
                </td>
            </tr>
    </table>
</asp:Content>

