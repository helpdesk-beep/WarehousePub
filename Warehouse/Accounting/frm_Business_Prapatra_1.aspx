<%@ Page Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="frm_Business_Prapatra_1.aspx.cs" Inherits="Accounting_frm_Business_Prapatra_1" Title="Prapatra 1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Label ID="lbldate2" runat="server" Visible="false"></asp:Label>
  <asp:Label ID="lbldate3" runat="server" Visible="false"></asp:Label>
  <asp:Label ID="lbldate1" runat="server" Visible="false"></asp:Label>
            <fieldset style="width: 1000px; border: 2px solid navy;">
                <center>

                    <div>
                        <div>
                            <h3>मध्य प्रदेश वेयरहाउसिंग एंड लॉजिस्टिक्स कॉर्पोरेशन, शाखा :-
                        <asp:Label ID="lblbranchname" runat="server"></asp:Label>
                            </h3>
                            <h5>खरीफ विपणन वर्ष 
                         <asp:DropDownList ID="ddlcropyear" runat="server" AutoPostBack="false"
                             Width="80px">
                             <asp:ListItem Value="2021-22">2021-22</asp:ListItem>
                             <asp:ListItem Value="2022-23">2022-23</asp:ListItem>
                             <asp:ListItem Value="2023-24">2023-24</asp:ListItem>
                             <asp:ListItem Value="2024-25">2024-25</asp:ListItem>
                         </asp:DropDownList>
                                में उपार्जित धान एवं मोटा अनाज के भण्डारण हेतु समस्त गोदामों तथा समस्त कैप की रिक्त वैज्ञानिक भण्डारण क्षमता की जानकारी </h5>
                        </div>
                        <table style="width: 100%; border: 1px solid navy;">
                            <tr id="msg">
                                <td colspan="4">
                                    <asp:Label ID="lblmsg" Text="" ForeColor="red" Font-Bold="true" runat="server"></asp:Label></td>
                            </tr>
                            <tr style="background-color: #0bb6e6; height: 25px">
                                <td colspan="8" align="left">
                                    <asp:Label ID="Label37" runat="server" Font-Bold="True" Font-Size="12pt" ForeColor="WhiteSmoke"
                                        Text="अनुमानित उपार्जन"></asp:Label></td>
                            </tr>

                            <tr>
                                <%--  <td>
                            <asp:Label ID="Label2" runat="server" Text="स्कंद का नाम"></asp:Label>
                        </td>
                        <td valign="middle">
                            <asp:DropDownList ID="ddlcommodity" runat="server" Width="100px" AutoPostBack="false">
                            </asp:DropDownList></td>--%>
                                <td>
                                    <asp:Label ID="Label57" runat="server" Text="धान"></asp:Label></td>
                                <td>
                                    <asp:TextBox ID="txtdhan" runat="server" class="form-control" Text="0" onkeypress="return NumberOnly(event);"></asp:TextBox></td>

                                <td>
                                    <asp:Label ID="Label8" runat="server" Text="मोटा अनाज "></asp:Label></td>
                                <td>
                                    <asp:TextBox ID="txtmotaanaj" runat="server" Text="0" class="form-control" onkeypress="return NumberOnly(event);"></asp:TextBox></td>
                                <td>
                                    <asp:Label ID="Label1" runat="server" Text="कुल अनुमानित उपार्जन "></asp:Label>
                                </td>
                                <td valign="middle">
                                    <asp:TextBox ID="txtTotalEstimatedEarnings" Text="0" runat="server" class="form-control" onkeypress="return NumberOnly(event);"></asp:TextBox></td>
                                <td>
                                    <asp:Label ID="Label3" runat="server" Text="भण्डारण हेतु कुल आवश्यक क्षमता "></asp:Label>
                                </td>
                                <td valign="middle">
                                    <asp:TextBox ID="txttotalstoragecapacity" runat="server" class="form-control" Text="0" onkeypress="return NumberOnly(event);"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td colspan="4" style="height: 5px"></td>
                            </tr>
                            <tr style="background-color: #0bb6e6; height: 25px">
                                <td colspan="8" align="left">
                                    <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Size="12pt" ForeColor="WhiteSmoke"
                                        Text="  "></asp:Label>

                                    <asp:Label runat="server" Font-Bold="True" Font-Size="12pt" ForeColor="WhiteSmoke">कवर्ड गोडाम एवं कैप की दिनांक</asp:Label>
                                    <asp:TextBox ID="txtIntimationRegDate" runat="server" MaxLength="10" Width="70px"></asp:TextBox>
                                    <asp:RegularExpressionValidator runat="server" ControlToValidate="txtIntimationRegDate" ValidationExpression="(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$"
                                        ErrorMessage="*" ValidationGroup="A" ForeColor="WhiteSmoke" />
                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtIntimationRegDate" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                                    <asp:Label runat="server" Font-Bold="True" Font-Size="12pt" ForeColor="WhiteSmoke">की स्थिति में कुल रिक्त भण्डारण क्षमता (में.टन)</asp:Label>

                                </td>
                            </tr>
                            <tr>
                                <td colspan="8" align="left">
                                    <asp:Label ID="Label10" runat="server" Font-Bold="True" Font-Size="12pt" ForeColor="Red"
                                        Text="GODOWN CAPACITY"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label5" runat="server" Text="MPWLC OWN"></asp:Label>
                                </td>
                                <td valign="middle">
                                    <asp:TextBox ID="txtMpwlcown1" runat="server" Text="0" AutoPostBack="true" class="form-control" onkeypress="return NumberOnly(event);" OnTextChanged="txtMpwlcown1_TextChanged"></asp:TextBox></td>
                                <td>
                                    <asp:Label ID="Label6" runat="server" Text="MPWLC JVS"></asp:Label></td>
                                <td>
                                    <asp:TextBox ID="txtMpwlcJVS1" runat="server" AutoPostBack="true" Text="0" class="form-control" onkeypress="return NumberOnly(event);" OnTextChanged="txtMpwlcJVS1_TextChanged"></asp:TextBox></td>
                                <td>
                                    <asp:Label ID="Label7" runat="server" Text="MPWLC Hirred + Adhigrahan"></asp:Label>
                                </td>
                                <td valign="middle">
                                    <asp:TextBox ID="txtHiredA1" runat="server" AutoPostBack="true" Text="0" class="form-control" onkeypress="return NumberOnly(event);" OnTextChanged="txtHiredA1_TextChanged"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label12" runat="server" Text="CWC"></asp:Label></td>
                                <td>
                                    <asp:TextBox ID="txtCWC1" runat="server" AutoPostBack="true" Text="0" class="form-control" onkeypress="return NumberOnly(event);" OnTextChanged="txtCWC1_TextChanged"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td colspan="0">
                                    <asp:Label ID="Label9" runat="server" Text="Arremented PVT/PEG Godown"></asp:Label>
                                </td>
                                <td valign="middle">
                                    <asp:TextBox ID="txtAPPG1" runat="server" AutoPostBack="true" Text="0" class="form-control" onkeypress="return NumberOnly(event);" OnTextChanged="txtAPPG1_TextChanged"></asp:TextBox></td>

                                <td>
                                    <asp:Label ID="Label13" runat="server" Text="Markfed"></asp:Label>
                                </td>
                                <td valign="middle">
                                    <asp:TextBox ID="txtMarkfed1" runat="server" AutoPostBack="true" Text="0" class="form-control" onkeypress="return NumberOnly(event);" OnTextChanged="txtMarkfed1_TextChanged"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label14" runat="server" Text="OILFED"></asp:Label>
                                </td>
                                <td valign="middle">
                                    <asp:TextBox ID="txtoilfed1" runat="server" AutoPostBack="true" Text="0" class="form-control" onkeypress="return NumberOnly(event);" OnTextChanged="txtoilfed1_TextChanged"></asp:TextBox></td>

                                <td>
                                    <asp:Label ID="Label15" runat="server" Text="Total Godown Capacity"></asp:Label></td>
                                <td>
                                    <asp:TextBox ID="txtTGC1" runat="server" class="form-control" Text="0" onkeypress="return NumberOnly(event);"></asp:TextBox></td>

                            </tr>
                            <tr>
                                <td colspan="8" align="left">
                                    <asp:Label ID="Label16" runat="server" Font-Bold="True" Font-Size="12pt" ForeColor="Red"
                                        Text="CAP CAPACITY"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label18" runat="server" Text="MPWLC OWN"></asp:Label>
                                </td>
                                <td valign="middle">
                                    <asp:TextBox ID="txtMpwlcown2" runat="server" AutoPostBack="true" Text="0" class="form-control" onkeypress="return NumberOnly(event);" OnTextChanged="txtMpwlcown2_TextChanged"></asp:TextBox></td>
                                <td>
                                    <asp:Label ID="Label19" runat="server" Text="MPWLC Mandi Cap"></asp:Label></td>
                                <td>
                                    <asp:TextBox ID="txtMMC2" runat="server" AutoPostBack="true" Text="0" class="form-control" onkeypress="return NumberOnly(event);" OnTextChanged="txtMMC2_TextChanged"></asp:TextBox></td>
                                <td>
                                    <asp:Label ID="Label20" runat="server" Text="MPWLC Mandi Shed"></asp:Label>
                                </td>
                                <td valign="middle">
                                    <asp:TextBox ID="txtMMS2" runat="server" AutoPostBack="true" Text="0" class="form-control" onkeypress="return NumberOnly(event);" OnTextChanged="txtMMS2_TextChanged"></asp:TextBox>
                                </td>

                                <td colspan="0">
                                    <asp:Label ID="Label21" runat="server" Text="Markfed"></asp:Label>
                                </td>
                                <td valign="middle">
                                    <asp:TextBox ID="txtMarkfed2" runat="server" AutoPostBack="true" Text="0" class="form-control" onkeypress="return NumberOnly(event);" OnTextChanged="txtMarkfed2_TextChanged"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label22" runat="server" Text="Pvt. PEG Cap"></asp:Label></td>
                                <td>
                                    <asp:TextBox ID="txtPPC2" runat="server" AutoPostBack="true" Text="0" class="form-control" onkeypress="return NumberOnly(event);" OnTextChanged="txtPPC2_TextChanged"></asp:TextBox></td>
                                <td>
                                    <asp:Label ID="Label23" runat="server" Text="Total Cap Capacity"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtTCC2" runat="server" class="form-control" Text="0" onkeypress="return NumberOnly(event);"></asp:TextBox></td>
                                <td>
                            </tr>

                            <tr>
                                <td colspan="4" style="height: 5px"></td>
                            </tr>
                            <tr style="background-color: #0bb6e6; height: 25px">
                                <td colspan="8" align="left">

                                    <asp:Label runat="server" Font-Bold="True" Font-Size="12pt" ForeColor="WhiteSmoke">दिनांक</asp:Label>
                                    <asp:TextBox ID="txtdate" runat="server" MaxLength="10" Width="70px"></asp:TextBox>
                                    <asp:RegularExpressionValidator runat="server" ControlToValidate="txtdate" ValidationExpression="(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$"
                                        ErrorMessage="*" ValidationGroup="A" ForeColor="WhiteSmoke" />
                                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtdate" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                                    <asp:Label runat="server" Font-Bold="True" Font-Size="12pt" ForeColor="WhiteSmoke"> के बाद आज दिनांक</asp:Label>
                                    <asp:TextBox ID="txtdate2" runat="server" MaxLength="10" Width="70px"></asp:TextBox>
                                    <asp:RegularExpressionValidator runat="server" ControlToValidate="txtdate2" ValidationExpression="(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$"
                                        ErrorMessage="*" ValidationGroup="A" ForeColor="WhiteSmoke" />
                                    <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtdate2" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                                    <asp:Label runat="server" Font-Bold="True" Font-Size="12pt" ForeColor="WhiteSmoke"> तक मिली गोदाम / कैप की कुल रिक्त भण्डारण क्षमता (स्कंध के उठाव तथा नविन प्राप्त क्षमता )(में.टन)
                                    </asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="8" align="left">
                                    <asp:Label ID="Label28" runat="server" Font-Bold="True" Font-Size="12pt" ForeColor="Red"
                                        Text="GODOWN CAPACITY"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label29" runat="server" Text="MPWLC OWN"></asp:Label>
                                </td>
                                <td valign="middle">
                                    <asp:TextBox ID="txtMpwlcown3" runat="server" class="form-control" onkeypress="return NumberOnly(event);" OnTextChanged="txtMpwlcown3_TextChanged" AutoPostBack="true" Text="0"></asp:TextBox></td>
                                <td>
                                    <asp:Label ID="Label30" runat="server" Text="MPWLC JVS"></asp:Label></td>
                                <td>
                                    <asp:TextBox ID="txtMpwlcJVS3" runat="server" class="form-control" onkeypress="return NumberOnly(event);" OnTextChanged="txtMpwlcJVS3_TextChanged" AutoPostBack="true" Text="0"></asp:TextBox></td>
                                <td>
                                    <asp:Label ID="Label31" runat="server" Text="MPWLC Hirred + Adhigrahan"></asp:Label>
                                </td>
                                <td valign="middle">
                                    <asp:TextBox ID="txtMpwlcHA3" runat="server" class="form-control" onkeypress="return NumberOnly(event);" OnTextChanged="txtMpwlcHA3_TextChanged" AutoPostBack="true" Text="0"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label32" runat="server" Text="CWC"></asp:Label></td>
                                <td>
                                    <asp:TextBox ID="txtCWC3" runat="server" class="form-control" onkeypress="return NumberOnly(event);" OnTextChanged="txtCWC3_TextChanged" AutoPostBack="true" Text="0"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td colspan="0">
                                    <asp:Label ID="Label33" runat="server" Text="Arremented PVT/PEG Godown"></asp:Label>
                                </td>
                                <td valign="middle">
                                    <asp:TextBox ID="txtAPPG3" runat="server" class="form-control" onkeypress="return NumberOnly(event);" OnTextChanged="txtAPPG3_TextChanged" AutoPostBack="true" Text="0"></asp:TextBox></td>

                                <td>
                                    <asp:Label ID="Label34" runat="server" Text="Markfed"></asp:Label>
                                </td>
                                <td valign="middle">
                                    <asp:TextBox ID="txtMarkfed3" runat="server" class="form-control" onkeypress="return NumberOnly(event);" OnTextChanged="txtMarkfed3_TextChanged" AutoPostBack="true" Text="0"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label35" runat="server" Text="OILFED"></asp:Label>
                                </td>
                                <td valign="middle">
                                    <asp:TextBox ID="txtoilfed3" runat="server" class="form-control" onkeypress="return NumberOnly(event);" OnTextChanged="txtoilfed3_TextChanged" AutoPostBack="true" Text="0"></asp:TextBox></td>

                                <td>
                                    <asp:Label ID="Label36" runat="server" Text="Total Godown Capacity"></asp:Label></td>
                                <td>
                                    <asp:TextBox ID="txtTGC3" runat="server" class="form-control" Text="0" onkeypress="return NumberOnly(event);"></asp:TextBox></td>

                            </tr>
                            <tr>
                                <td colspan="8" align="left">
                                    <asp:Label ID="Label38" runat="server" Font-Bold="True" Font-Size="12pt" ForeColor="Red"
                                        Text="CAP CAPACITY"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label39" runat="server" Text="MPWLC OWN"></asp:Label>
                                </td>
                                <td valign="middle">
                                    <asp:TextBox ID="txtMpwlcown4" runat="server" class="form-control" onkeypress="return NumberOnly(event);" OnTextChanged="txtMpwlcown4_TextChanged" AutoPostBack="true" Text="0"></asp:TextBox></td>
                                <td>
                                    <asp:Label ID="Label40" runat="server" Text="MPWLC Mandi Cap"></asp:Label></td>
                                <td>
                                    <asp:TextBox ID="txtMMC4" runat="server" class="form-control" onkeypress="return NumberOnly(event);" OnTextChanged="txtMMC4_TextChanged" AutoPostBack="true" Text="0"></asp:TextBox></td>
                                <td>
                                    <asp:Label ID="Label41" runat="server" Text="MPWLC Mandi Shed"></asp:Label>
                                </td>
                                <td valign="middle">
                                    <asp:TextBox ID="txtMMS4" runat="server" class="form-control" onkeypress="return NumberOnly(event);" OnTextChanged="txtMMS4_TextChanged" AutoPostBack="true" Text="0"></asp:TextBox>
                                </td>

                                <td colspan="0">
                                    <asp:Label ID="Label42" runat="server" Text="Markfed"></asp:Label>
                                </td>
                                <td valign="middle">
                                    <asp:TextBox ID="txtMarkfed4" runat="server" class="form-control" onkeypress="return NumberOnly(event);" OnTextChanged="txtMarkfed4_TextChanged" AutoPostBack="true" Text="0"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label43" runat="server" Text="Pvt. PEG Cap"></asp:Label></td>
                                <td>
                                    <asp:TextBox ID="txtPPC4" runat="server" class="form-control" onkeypress="return NumberOnly(event);" OnTextChanged="txtPPC4_TextChanged" AutoPostBack="true" Text="0"></asp:TextBox></td>
                                <td>
                                    <asp:Label ID="Label44" runat="server" Text="Total Cap Capacity"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtTCC4" runat="server" class="form-control" Text="0" onkeypress="return NumberOnly(event);"></asp:TextBox></td>
                            </tr>
                            
                            <tr>
                                <td colspan="4" style="height: 5px"></td>
                            </tr>
                            <tr style="background-color: #0bb6e6; height: 25px">
                                <td colspan="8" align="left">

                                    <asp:Label runat="server" Font-Bold="True" Font-Size="12pt" ForeColor="WhiteSmoke">आज दिनांक तक उपलब्ध कुल प्रगतिशील रिक्त क्षमता (में.टन)</asp:Label>
                                   
                                </td>
                            </tr>
                            <tr>
                                <td colspan="8" align="left">
                                    <asp:Label ID="Label17" runat="server" Font-Bold="True" Font-Size="12pt" ForeColor="Red"
                                        Text="GODOWN CAPACITY"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label24" runat="server" Text="MPWLC OWN"></asp:Label>
                                </td>
                                <td valign="middle">
                                    <asp:TextBox ID="txtMpwlcOwn5" runat="server" class="form-control" onkeypress="return NumberOnly(event);" OnTextChanged="txtMpwlcOwn5_TextChanged" AutoPostBack="true" Text="0"></asp:TextBox></td>
                                <td>
                                    <asp:Label ID="Label25" runat="server" Text="MPWLC JVS"></asp:Label></td>
                                <td>
                                    <asp:TextBox ID="txtMpwlcJvs5" runat="server" class="form-control" onkeypress="return NumberOnly(event);" OnTextChanged="txtMpwlcJvs5_TextChanged" AutoPostBack="true" Text="0"></asp:TextBox></td>
                                <td>
                                    <asp:Label ID="Label26" runat="server" Text="MPWLC Hirred + Adhigrahan"></asp:Label>
                                </td>
                                <td valign="middle">
                                    <asp:TextBox ID="txtMpwlcHired5" runat="server" class="form-control" onkeypress="return NumberOnly(event);" OnTextChanged="txtMpwlcHired5_TextChanged" AutoPostBack="true" Text="0"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label27" runat="server" Text="CWC"></asp:Label></td>
                                <td>
                                    <asp:TextBox ID="txtCwc5" runat="server" class="form-control" onkeypress="return NumberOnly(event);" OnTextChanged="txtCwc5_TextChanged" AutoPostBack="true" Text="0"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td colspan="0">
                                    <asp:Label ID="Label45" runat="server" Text="Arremented PVT/PEG Godown"></asp:Label>
                                </td>
                                <td valign="middle">
                                    <asp:TextBox ID="txtArremPvtPegGo5" runat="server" class="form-control" onkeypress="return NumberOnly(event);" OnTextChanged="txtArremPvtPegGo5_TextChanged" AutoPostBack="true" Text="0"></asp:TextBox></td>

                                <td>
                                    <asp:Label ID="Label46" runat="server" Text="Markfed"></asp:Label>
                                </td>
                                <td valign="middle">
                                    <asp:TextBox ID="txtMarkfrd5" runat="server" class="form-control" onkeypress="return NumberOnly(event);" OnTextChanged="txtMarkfrd5_TextChanged" AutoPostBack="true" Text="0"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label47" runat="server" Text="OILFED"></asp:Label>
                                </td>
                                <td valign="middle">
                                    <asp:TextBox ID="txtOilfed5" runat="server" class="form-control" onkeypress="return NumberOnly(event);" OnTextChanged="txtOilfed5_TextChanged" AutoPostBack="true" Text="0"></asp:TextBox></td>

                                <td>
                                    <asp:Label ID="Label48" runat="server" Text="Total Godown Capacity"></asp:Label></td>
                                <td>
                                    <asp:TextBox ID="txtTotalGodownCap5" runat="server" class="form-control" onkeypress="return NumberOnly(event);" Text="0" OnTextChanged="txtTotalGodownCap5_TextChanged"></asp:TextBox></td>

                            </tr>
                            <tr>
                                <td colspan="8" align="left">
                                    <asp:Label ID="Label49" runat="server" Font-Bold="True" Font-Size="12pt" ForeColor="Red"
                                        Text="CAP CAPACITY"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label50" runat="server" Text="MPWLC OWN"></asp:Label>
                                </td>
                                <td valign="middle">
                                    <asp:TextBox ID="txtMPwlcown6" runat="server" class="form-control" onkeypress="return NumberOnly(event);" OnTextChanged="txtMPwlcown6_TextChanged" AutoPostBack="true" Text="0"></asp:TextBox></td>
                                <td>
                                    <asp:Label ID="Label51" runat="server" Text="MPWLC Mandi Cap"></asp:Label></td>
                                <td>
                                    <asp:TextBox ID="txtMpwlcmanCap5" runat="server" class="form-control" onkeypress="return NumberOnly(event);" OnTextChanged="txtMpwlcmanCap5_TextChanged" AutoPostBack="true" Text="0"></asp:TextBox></td>
                                <td>
                                    <asp:Label ID="Label52" runat="server" Text="MPWLC Mandi Shed"></asp:Label>
                                </td>
                                <td valign="middle">
                                    <asp:TextBox ID="txtMpwlcmandished5" runat="server" class="form-control" onkeypress="return NumberOnly(event);" OnTextChanged="txtMpwlcmandished5_TextChanged" AutoPostBack="true" Text="0"></asp:TextBox>
                                </td>

                                <td colspan="0">
                                    <asp:Label ID="Label53" runat="server" Text="Markfed"></asp:Label>
                                </td>
                                <td valign="middle">
                                    <asp:TextBox ID="txtMarked6" runat="server" class="form-control" onkeypress="return NumberOnly(event);" OnTextChanged="txtMarked6_TextChanged" AutoPostBack="true" Text="0"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label54" runat="server" Text="Pvt. PEG Cap"></asp:Label></td>
                                <td>
                                    <asp:TextBox ID="txtPvtpegcap5" runat="server" class="form-control" onkeypress="return NumberOnly(event);" OnTextChanged="txtPvtpegcap5_TextChanged" AutoPostBack="true" Text="0"></asp:TextBox></td>
                                <td>
                                    <asp:Label ID="Label55" runat="server" Text="Total Cap Capacity"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtTcapCapacity5" runat="server" class="form-control" Text="0" onkeypress="return NumberOnly(event);"></asp:TextBox></td>
                            </tr>

                            <tr>
                                <td>
                                    <asp:Label ID="Label11" runat="server" Text="शार्टफाल की पूर्ति हेतु विकल्प"></asp:Label>
                                </td>
                                <td colspan="8">
                                    <asp:TextBox ID="txtRemark" runat="server" class="form-control" Width="90%" TextMode="MultiLine"></asp:TextBox>
                                </td>
                            </tr>



                            <tr>
                                <td colspan="8" align="center">
                                    <asp:Button ID="btnSumbmitRent" runat="server" ValidationGroup="A" Text="Save" CssClass="BTNBLUE" Width="100px" OnClick="btnSumbmitRent_Click" />
                                </td>
                            </tr>

                        </table>
                        <div style="width: 1000px; overflow: auto;">

                            <asp:GridView ID="GrdPrapatraI" runat="server" AutoGenerateColumns="False" Width="100%" Font-Names="Arial"
                                BorderStyle="Double" CellPadding="3" CellSpacing="5" ShowFooter="true"
                                Font-Size="11px" BorderColor="#CCCCCC" OnRowCreated="GrdPrapatraI_RowCreated">
                                <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <Columns>
                                    <asp:TemplateField HeaderText="1">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1%>
                                        </ItemTemplate>
                                        <ItemStyle Width="1%" />
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateField>
                                    <%--<asp:BoundField DataField="Commodity_Name" HeaderText="1"></asp:BoundField>--%>
                                    <asp:BoundField DataField="paddy" HeaderText="2" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>

                                    <asp:BoundField DataField="Fat_grain_Weight" HeaderText="3" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>

                                    <asp:BoundField DataField="Total_Estimated_Earnings" HeaderText="4" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>

                                    <asp:BoundField DataField="Total_storage_capacity" HeaderText="5" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>

                                    <asp:BoundField DataField="MPWLC_OWN1" HeaderText="6" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>

                                    <asp:BoundField DataField="MPWLC_JVS1" HeaderText="7" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>

                                    <asp:BoundField DataField="MPWLC_HA1" HeaderText="8" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="MPWLC_CWC1" HeaderText="9" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Arremented_PVT_PEG1" HeaderText="10" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Markfed1" HeaderText="11" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="OILFED1" HeaderText="12" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Total_Godown_Capacity1" HeaderText="13" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="MPWLC_OWN2" HeaderText="14" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="MPWLC_Mandi_Cap2" HeaderText="15" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="MPWLC_Mandi_Shed2" HeaderText="16" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Markfed2" HeaderText="17" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Pvt_PEG_Cap2" HeaderText="18" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Total_Cap_Capacity2" HeaderText="19" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="MPWLC_OWN3" HeaderText="20" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="MPWLC_JVS3" HeaderText="21" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="MPWLC_HA3" HeaderText="22" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="MPWLC_CWC3" HeaderText="23" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Arremented_PVT_PEG3" HeaderText="24" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Markfed3" HeaderText="25" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="OILFED3" HeaderText="26" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Total_Godown_Capacity3" HeaderText="27" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="MPWLC_OWN4" HeaderText="28" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="MPWLC_Mandi_Cap4" HeaderText="29" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="MPWLC_Mandi_Shed4" HeaderText="30" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Markfed4" HeaderText="31" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Pvt_PEG_Cap4" HeaderText="32" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Total_Cap_Capacity4" HeaderText="33" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <%--<asp:BoundField DataField="Remark" HeaderText="34" ItemStyle-HorizontalAlign="Left">
                                <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>--%>
                                    <asp:BoundField DataField="MPWLC_OWN5" HeaderText="34" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="MPWLC_JVS5" HeaderText="35" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="MPWLC_HA5" HeaderText="36" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="MPWLC_CWC5" HeaderText="37" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Arremented_PVT_PEG5" HeaderText="38" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Markfed5" HeaderText="39" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="OILFED5" HeaderText="40" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Total_Godown_Capacity5" HeaderText="41" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="MPWLC_OWN6" HeaderText="42" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="MPWLC_Mandi_Cap5" HeaderText="43" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="MPWLC_Mandi_Shed5" HeaderText="44" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Markfed6" HeaderText="45" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Pvt_PEG_Cap5" HeaderText="46" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Total_Cap_Capacity5" HeaderText="47" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                </Columns>
                                <HeaderStyle ForeColor="#C70039" HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="11px" />
                            </asp:GridView>

                        </div>
                    </div>


                </center>
            </fieldset>
       
    <script type="text/javascript" src="<%= ResolveUrl("~/NEW_CSS/js/jquery-1.12.4.js") %>"></script>
    <script type="text/javascript" src="<%= ResolveUrl("~/NEW_CSS/js/jquery-ui.js") %>"></script>
    <script language="javascript" type="text/javascript">
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
    </script>
</asp:Content>

