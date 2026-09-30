<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/StateMaster.master" AutoEventWireup="true" CodeFile="AddGodown.aspx.cs" Inherits="StatePages_AddGodown" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


    <table>
        <tr>
            <td>
                <asp:Label ID="lbldist" runat="server" Text="District Name"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlDistrict" runat="server" AutoPostBack="True" onselectedindexchanged="ddlDistrict_SelectedIndexChanged">
                
                </asp:DropDownList>
            </td>
            <td>
                <asp:Label ID="Label1" runat="server" Text="Branch Name"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlbranch" runat="server" AutoPostBack="True" 
                    onselectedindexchanged="ddlbranch_SelectedIndexChanged">
                </asp:DropDownList>
                &nbsp; <asp:Label ID="lblIssueCenterId" runat="server"></asp:Label>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblbranchid" runat="server"></asp:Label>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
             
            </td>
        </tr>
    </table>
    <table>
    <tr>
    <td>
        <asp:GridView ID="gvgodowns" runat="server" CellPadding="4" ForeColor="#333333" 
            GridLines="None" EnableModelValidation="True" OnRowDataBound="gvgodowns_RowDataBound" OnRowDeleting="gvgodowns_RowDeleting" OnSelectedIndexChanged="gvgodowns_SelectedIndexChanged" DataKeyNames="Godown_ID">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" />
                <asp:CommandField HeaderText="Edit" ShowSelectButton="True" />
            </Columns>
            <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
           
        </asp:GridView>
    </td>
    </tr>
    </table>
    <asp:Panel ID="Panel1" runat="server">
    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr style="background-color: #0bb6e6; height: 25px">
                                                <td colspan="2" align="center">
                                                    <asp:Label ID="Label7" runat="server" Text="Godown  Master" Font-Bold="true" Font-Size="12pt"
                                                        ForeColor="whitesmoke"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 5px" colspan="2">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 300px;">
                                                    <asp:Label ID="Label3" runat="server" Text="Godown Name" Font-Bold="True" ForeColor="Navy"
                                                        Font-Size="8pt"></asp:Label></td>
                                                <td>
                                                    <asp:TextBox ID="txtGodownName" runat="server" Width="300px" Font-Size="8pt" AutoComplete="off"></asp:TextBox>
                                                    </td>
                                            </tr>
                                          <%--  <tr>
                                                <td style="height: 5px" colspan="2">
                                                </td>
                                            </tr>--%>
                                           <%--<tr>
                                                <td style="width: 300px;">
                                                    <asp:Label ID="lbl_godownname" runat="server" Text="Godown Name" Font-Bold="True" ForeColor="Navy"
                                                        Font-Size="8pt"></asp:Label></td>
                                                <td>
                                                    <asp:TextBox ID="txt_godownName" runat="server" Width="300px" Font-Size="8pt"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtGodownName"
                                                        Display="Dynamic" ErrorMessage="Godown Name field cannot be empty" SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
                                            </tr>--%>
                                            <tr>
                                                <td style="width: 300px;">
                                                    &nbsp;</td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td style="width: 300px;">
                                                    <asp:Label ID="Label11" runat="server" Font-Bold="True" Font-Size="8pt" 
                                                        ForeColor="Navy" Text="Comodity Name" Visible="False"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="dprlst_Commodity" runat="server" Visible="False">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 300px;">&nbsp;</td>
                                                <td>&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td style="width: 300px;">
                                                    <asp:Label ID="Label14" runat="server" Font-Bold="True" Font-Size="8pt" ForeColor="Navy" Text="Godown Number"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtgodownnum" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 5px" colspan="2">
                                                    &nbsp;</td>

                                            </tr>
                                           <tr>
                                                <td style="width: 300px;">
                                                    <asp:Label ID="lbl_apn" runat="server" Text="Authorize Person Name" Font-Bold="True" ForeColor="Navy"
                                                        Font-Size="8pt"></asp:Label></td>
                                                <td>
                                                    <asp:TextBox ID="txt_APN" runat="server" Width="300px" Font-Size="8pt" AutoComplete="off"></asp:TextBox>
                                                    </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 300px;">
                                                    &nbsp;</td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td style="width: 300px;">
                                                    <asp:Label ID="lbl_apn0" runat="server" Font-Bold="True" Font-Size="8pt" 
                                                        ForeColor="Navy" Text="WLC Co. Sign" Visible="False"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtwlccosign" runat="server" Height="16px" Width="297px" 
                                                        Visible="False"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 5px" colspan="2">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lbl_emailid" runat="server" Text="Email Id" Font-Bold="True" ForeColor="Navy"
                                                        Font-Size="8pt"></asp:Label></td>
                                                <td>
                                                    <asp:TextBox ID="txt_emailid" runat="server" Width="300px" Font-Size="8pt" AutoComplete="off"></asp:TextBox>
                                                    </td>
                                            </tr>
                                                <tr>
                                                <td style="height: 5px" colspan="2">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lbl_mobile" runat="server" Text="Mobile" Font-Bold="True" ForeColor="Navy"
                                                        Font-Size="8pt"></asp:Label></td>
                                                <td>
                                                    <asp:TextBox ID="txt_mobile" runat="server" Width="300px" AutoComplete="off" Font-Size="8pt" onkeypress="CheckNumeric(event);"></asp:TextBox>
                                                    </td>
                                            </tr>
                                                 <tr>
                                                <td style="height: 5px" colspan="2">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label4" runat="server" Text="Maximum Capacity" Font-Bold="true" ForeColor="navy"
                                                        Font-Size="8pt"></asp:Label>(Qty. in Qtls.kgsgms)</td>
                                                <td>
                                                    <asp:TextBox ID="txtCapacity" runat="server" Width="150px" AutoComplete="off" onkeypress="return isNumberKey2(event)"></asp:TextBox>
                                                  
                                                    </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 5px" colspan="2">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label2" runat="server" Text="Scientific Capacity" Font-Bold="true"
                                                        ForeColor="navy" Font-Size="8pt"></asp:Label>(Qty. in Qtls.kgsgms)</td>
                                                <td>
                                                    <asp:TextBox ID="txtScientificCapacity" runat="server" Width="150px" 
                                                        AutoComplete="off" AutoPostBack="True" 
                                                         onkeypress="return isNumberKey2(event)"></asp:TextBox>
                                                   <%-- <asp:FilteredTextBoxExtender ID="txtScientificCapacity_FilteredTextBoxExtender" 
                                                        runat="server" TargetControlID="txtScientificCapacity" FilterType="Custom, Numbers" ValidChars=".">
                                                    </asp:FilteredTextBoxExtender>--%>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtScientificCapacity"
                                                        Display="Dynamic" ErrorMessage="Scientific Capacity field cannot be empty" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                                    <asp:Label ID="lbl_checkcapcity" runat="server" Font-Bold="True" 
                                                        ForeColor="Red" Visible="False"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 5px" colspan="2">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label5" runat="server" Text="Hired Type" Font-Bold="true" ForeColor="navy"
                                                        Font-Size="8pt"></asp:Label></td>
                                                <td>
                                                    <asp:DropDownList ID="ddllst_hired" runat="server" Width="155px" Height="25px">
                                     
                                                       
                                                    </asp:DropDownList></td>
                                            </tr>
                                            <tr>
                                                <td style="height: 5px" colspan="2">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label6" runat="server" Text="Storage Type" Font-Bold="true" ForeColor="navy"
                                                        Font-Size="8pt"></asp:Label></td>
                                                <td>
                                                    <asp:DropDownList ID="ddllst_storage" runat="server" Width="155px" Height="25px">
                                                        <asp:ListItem Text="Covered" Value="Covered"></asp:ListItem>
                                                        <asp:ListItem Text="Permanent(CAP)" Value="Permanent(CAP)"></asp:ListItem>
                                                           <asp:ListItem Text="Temporary(CAP)" Value="Temporary(CAP)"></asp:ListItem>
                                                         <asp:ListItem Text="Silo Bag"  Value="SiloBag"></asp:ListItem>
                                                        <asp:ListItem Text="Steel Silo"  Value="SteelSilo"></asp:ListItem>
                                                    </asp:DropDownList></td>
                                            </tr>
                                             <tr>
                                                 <td>
                                                     &nbsp;</td>
                                                 <td>
                                                     &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Size="8pt" 
                                                        ForeColor="Navy" Text="Licence Number"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtlicnum" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;</td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label10" runat="server" Font-Bold="True" Font-Size="8pt" 
                                                        ForeColor="Navy" Text="LiceneceDate"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtlicdate" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                             <tr>
                                                <td style="height: 5px" colspan="2">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label8" runat="server" Text="Address" Font-Bold="True" ForeColor="Navy"
                                                        Font-Size="8pt"></asp:Label></td>
                                                <td>
                                                    <asp:TextBox ID="txt_address" runat="server" Width="300px" AutoComplete="off" Font-Size="8pt" Height="50px" TextMode="MultiLine"></asp:TextBox>
                                                    </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 5px" colspan="2">
                                                </td>
                                            </tr>
         <tr>
                                                <td>
<asp:Label ID="Label12" runat="server" Text="Latitude" Font-Bold="True" ForeColor="Navy"
                                                        Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td><asp:TextBox ID="txtlatitude" runat="server"></asp:TextBox>ex:26.203194</td>
                                            </tr>
                                            <tr>
                                                <td style="height: 5px" colspan="2">

                                                </td>
                                            </tr>
                                             <tr>
                                                <td>
<asp:Label ID="Label13" runat="server" Text="Longitude" Font-Bold="True" ForeColor="Navy"
                                                        Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td><asp:TextBox ID="txtlongitude" runat="server"></asp:TextBox>ex:78.209267</td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                            </tr>

         <tr>
                                                <td>
                                                    <asp:Label ID="Label17" runat="server" Font-Bold="True" Font-Size="8pt" ForeColor="Navy" Text="खण्ड"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlPBlock" runat="server" AutoPostBack="true"  Width="190px" OnSelectedIndexChanged="ddlPBlock_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label18" runat="server" Font-Bold="True" Font-Size="8pt" ForeColor="Navy" Text="गाँव"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlVillage" runat="server" Width="190px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                            </tr>

          <tr>
                                                <td>
                                                    <asp:Label ID="Label15" runat="server" Font-Bold="True" Font-Size="8pt" ForeColor="Navy" Text="खशरा नंबर"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtkhasra" runat="server">0</asp:TextBox>
                                                    
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style1"></td>
                                                <td class="auto-style1"></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label16" runat="server" Font-Bold="True" Font-Size="8pt" ForeColor="Navy" Text="राकवा"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtrakwa" runat="server">0</asp:TextBox>
                                                   
                                                </td>
                                            </tr>
                                             <tr>
                                                <td>
                                                    <asp:Label ID="Label19" runat="server" Font-Bold="True" Font-Size="8pt" ForeColor="Navy" Text="गोदाम पर धर्मकाटा/तौलकाटा उपलब्ध है/नहीं "></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlWeightmentS" runat="server" Width="100px" 
                                                        AutoPostBack="true" onselectedindexchanged="ddlWeightmentS_SelectedIndexChanged"
                                                        >
                                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                     <asp:ListItem Value="1">No</asp:ListItem>
                                                      <asp:ListItem Value="2">Yes</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="ddlWeightmentType" runat="server" Width="190px" Visible="false">
                                                     <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                    <asp:ListItem Value="WB">Weighbridge</asp:ListItem>
                                                     <asp:ListItem Value="BS">Beam Scale</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" align="center">
                                                    <asp:Button ID="btnUpdate" runat="server" Text="Insert" Width="100px" 
                                                        OnClientClick="return Validate()" CssClass="BTNBLUE" onclick="btnUpdate_Click"
                                                         />
                                                    &nbsp;&nbsp;&nbsp;
                                                    <asp:Button ID="btnCan" runat="server" Text="Cancel" Width="100px" 
                                                        CssClass="BTNBLUE" OnClientClick="return Validate()"
                                                         CausesValidation="false" onclick="btnCan_Click" /></td>
                                            </tr>
                                            <tr>
                                                <td style="height: 5px" colspan="2">
                                                </td>
                                            </tr>
                                        </table>
    </asp:Panel>
</asp:Content>

