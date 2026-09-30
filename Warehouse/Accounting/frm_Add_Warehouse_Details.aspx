<%@ Page Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="frm_Add_Warehouse_Details.aspx.cs" Inherits="Accounting_frm_Add_Warehouse_Details" Title="Add Warehouse Detail" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset style="width: 1000px; border: 2px solid navy;">
        <center>
            <div>
                <h3 style="color: red;">संबंधित जानकारी भारत सरकार को दी जाना हैं कृप्या सावधानी पूर्वक एंट्री करे</h3>
                <h4 style="color: red;">कैम्पस बार गोदामों की मेपिंग की जाना हैं, अतः एक कैंपस/परिसर में जितने गोदाम हैं, उनके सामने चेक बॉक्स में सिलेक्ट करें, तदउपरांत नीचे दी गई MAP बटन पर क्लिक करें !
नोट:- एक बाउंड्री वॉल के अंदर जितने गोदाम हैं, वह सभी गोदाम एक ही समूह में आएंगे और इस स्थान को एक परिसर/कैम्पस कहा जाएगा !
                </h4>
                <table style="width: 100%;">
                    <tr id="msg">
                        <td colspan="4">
                            <asp:Label ID="lblmsg" Text="" ForeColor="red" Font-Bold="true" runat="server"></asp:Label></td>
                    </tr>
                    <tr style="background-color: #0bb6e6; height: 25px">
                        <td colspan="4" align="center">
                            <asp:Label ID="Label37" runat="server" Font-Bold="True" Font-Size="15pt" ForeColor="WhiteSmoke"
                                Text="Add Warehosue Detail"></asp:Label></td>
                    </tr>



                    <td>
                        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="8pt"
                            ForeColor="Navy" Text="गोदाम का प्रकार चुने"></asp:Label>
                    </td>
                    <td valign="middle">
                        <asp:DropDownList ID="ddlWHT" runat="server" AutoPostBack="True"
                            TabIndex="1" Height="25px" Width="200px" Font-Size="10pt">
                            <asp:ListItem Value="0">-----Select------</asp:ListItem>
                            <asp:ListItem Value="1">MPWLC</asp:ListItem>
                            <asp:ListItem Value="2">CWC</asp:ListItem>
                            <asp:ListItem Value="3">Markfed</asp:ListItem>
                            <asp:ListItem Value="4">FCI</asp:ListItem>
                            <asp:ListItem Value="5">PVT.</asp:ListItem>
                        </asp:DropDownList></td>
                    <td>
                        <asp:Label Font-Size="8pt" Font-Bold="true" ForeColor="navy" ID="Label3" runat="server" Text="स्वामित्व का प्रकार चुने"></asp:Label></td>
                    <td>
                        <asp:DropDownList ID="ddlOST" runat="server" AutoPostBack="false"
                            TabIndex="1" Height="25px" Width="200px" Font-Size="10pt">
                            <asp:ListItem Value="0">-----Select------</asp:ListItem>
                            <asp:ListItem Value="1">OWNED</asp:ListItem>
                            <asp:ListItem Value="2">MPWLC HIRED</asp:ListItem>
                            <asp:ListItem Value="3">COLLECTOR HIRED</asp:ListItem>
                            <asp:ListItem Value="4">PEG</asp:ListItem>
                            <asp:ListItem Value="5">PVT.</asp:ListItem>
                            <asp:ListItem Value="6">CAP-PMS</asp:ListItem>
                            <asp:ListItem Value="7">Joint Venture(JV)</asp:ListItem>
                            <asp:ListItem Value="8">Tribal Scheme</asp:ListItem>
                            <asp:ListItem Value="9">WDRA</asp:ListItem>
                        </asp:DropDownList></td>

                    <tr>
                        <td colspan="4" style="height: 5px"></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label Font-Size="8pt" Font-Bold="true" ForeColor="navy" ID="Label12" runat="server" Text="गोदाम भंडारण का प्रकार चुने"></asp:Label></td>
                        <td>
                            <asp:DropDownList ID="ddlWST" runat="server" AutoPostBack="false"
                                TabIndex="1" Height="25px" Width="200px" Font-Size="10pt">
                                <asp:ListItem Value="0">-----Select------</asp:ListItem>
                                <asp:ListItem Value="1">Covered</asp:ListItem>
                                <asp:ListItem Value="2">SiloBag</asp:ListItem>
                                <asp:ListItem Value="3">Permanent(CAP)</asp:ListItem>
                                <asp:ListItem Value="4">Temporary(CAP)</asp:ListItem>
                                <asp:ListItem Value="5">Silo Bag</asp:ListItem>
                                <asp:ListItem Value="6">Steel Silo</asp:ListItem>
                            </asp:DropDownList></td>
                        <td>
                            <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="8pt"
                                ForeColor="Navy" Text="वेयरहाउस का नाम"></asp:Label>
                        </td>
                        <td valign="middle">
                            <asp:TextBox ID="txtWHName" runat="server" Height="22px" Width="200px"></asp:TextBox>

                        </td>
                        <%-- <td>
                            <asp:Label Font-Size="8pt" Font-Bold="true" ForeColor="navy" ID="Label8" runat="server" Text="Account Holder Name"></asp:Label></td>
                        <td>
                            <asp:TextBox ID="txtGOwnerName" runat="server" Height="22px" Width="200px"></asp:TextBox></td>--%>
                    </tr>
                    <tr>
                        <td colspan="4" style="height: 5px"></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblcropyear" runat="server" Font-Bold="True" Font-Size="8pt"
                                ForeColor="Navy" Text="वेयरहाउस मालिक का नाम"></asp:Label>
                        </td>
                        <td valign="middle">
                            <asp:TextBox ID="txtWHON" runat="server" Height="22px" Width="200px"></asp:TextBox>

                        </td>
                        <td>
                            <asp:Label ID="lblcrate" Visible="true" runat="server" Text="वेयरहाउस प्रबंधक का नाम" Font-Bold="True" Font-Size="8pt"
                                ForeColor="Navy"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtWHMN"
                                Height="22px" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="height: 5px"></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lbltod" Font-Size="8pt" Font-Bold="true" ForeColor="navy" runat="server" Text="वेयरहाउस संपर्क नंबर"></asp:Label>
                        </td>
                        <td valign="middle">
                            <asp:TextBox ID="txtWCN" runat="server" Height="22px" Width="200px" onkeypress="return isNumberKey(event)" MaxLength="10"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="Label4" Font-Size="8pt" Font-Bold="true" ForeColor="navy" runat="server" Text="वेयरहाउस ईमेल-आईडी"></asp:Label>
                        </td>
                        <td valign="middle">
                            <asp:TextBox ID="txtWEID" runat="server" Height="22px" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="height: 5px"></td>
                    </tr>

                    <tr>
                        <td>
                            <asp:Label ID="Label11" Font-Size="8pt" Font-Bold="true" ForeColor="navy" runat="server" Text="वेयरहाउस का पिन कोड"></asp:Label>
                        </td>
                        <td valign="middle">
                            <asp:TextBox ID="txtpincode" runat="server" Height="22px" Width="200px" onkeypress="return isNumberKey(event)" MaxLength="6"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="Label8" Font-Size="8pt" Font-Bold="true" ForeColor="navy" runat="server" Text="वेयरहाउस की भंडारण क्षमता मीट्रिक टन (एमटी) में "></asp:Label>
                        </td>
                        <td valign="middle">
                            <asp:TextBox ID="txtSCMT" runat="server" Height="22px" Width="200px" onkeypress="return isNumberKey(event)"></asp:TextBox>
                        </td>

                    </tr>
                    <tr>
                        <td colspan="4" style="height: 5px"></td>
                    </tr>
                    <tr>
                        <td colspan="4" style="color: red;">
                            <h3>(यदि परिसर में एक से ज्यादा गोडाउन हें और उनके लाइसेंस नम्बर अलग अलग हें तो इस स्थिति में आप किसी भी एक गोडाउन का लाइसेंस नम्बर डाल सकते हैं )</h3>

                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label Font-Size="8pt" Font-Bold="true" ForeColor="navy" ID="Label13" runat="server" Text="यदि लाइसेंस है (हाँ/नहीं) चुने"></asp:Label></td>
                        <td>
                            <asp:DropDownList ID="ddliflicense" runat="server" AutoPostBack="True"
                                TabIndex="1" Height="25px" Width="200px" Font-Size="10pt" OnSelectedIndexChanged="ddliflicense_SelectedIndexChanged">
                                <asp:ListItem Value="1">YES</asp:ListItem>
                                <asp:ListItem Value="2">NO</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr id="iflicense" runat="server" visible="false">
                        <td>
                            <asp:Label ID="Label14" Font-Size="8pt" Font-Bold="true" ForeColor="navy" runat="server" Text="लाइसेंस पंजीकरण संख्या"></asp:Label>
                        </td>
                        <td valign="middle">
                            <asp:TextBox ID="txtLRN" runat="server" Height="22px" Width="200px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="Label15" Font-Size="8pt" Font-Bold="true" ForeColor="navy" runat="server" Text="लाइसेंस की वैधता"></asp:Label>
                        </td>
                        <td valign="middle">
                            <asp:TextBox ID="txtVoL" runat="server" Height="22px" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="height: 5px"></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label16" Font-Size="8pt" Font-Bold="true" ForeColor="navy" runat="server" Text="Latitude of Warehouse"></asp:Label>
                        </td>
                        <td valign="middle">
                            <asp:TextBox ID="txtLat" runat="server" Height="22px" Width="200px" onkeypress="return isNumberKey(event)"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="Label17" Font-Size="8pt" Font-Bold="true" ForeColor="navy" runat="server" Text="Longitude of Warehouse"></asp:Label>
                        </td>
                        <td valign="middle">
                            <asp:TextBox ID="txtLog" runat="server" Height="22px" Width="200px" onkeypress="return isNumberKey(event)"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="height: 5px"></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label10" Font-Size="8pt" Font-Bold="true" ForeColor="navy" runat="server" Text="वेयरहाउस का पूरा पता"></asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtAdd1" runat="server" Height="50px" placeholder="Enter Postal Address of Warehouse" Width="100%" TextMode="MultiLine"></asp:TextBox></td>

                    </tr>
                    <tr>
                        <td colspan="4" style="height: 5px"></td>
                    </tr>
                    <tr>
                        <td align="center" colspan="4">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" Visible="true" Width="100px"
                                CssClass="BTNBLUE" OnClick="btnSubmit_Click" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="100px"
                                CssClass="BTNBLUE" OnClick="btnCancel_Click" />
                        </td>
                    </tr>
                    <%--  <tr>
   <td colspan="2" align="right"><asp:Button ID="btnsubmit" Visible="false" 
           runat="server" Text="Submit" CssClass="BTNBLUE" onclick="btnsubmit_Click"/>
   </td>
   <td colspan="2" align="left"><asp:Button ID="btncancel" Visible="false" 
           runat="server" Text="Cancel" CssClass="BTNBLUE" onclick="btncancel_Click"/>
   </td>
   </tr>--%>
                </table>
            </div>
        </center>
    </fieldset>
    <script type="text/javascript">

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode != 46 && charCode > 31
                && (charCode < 48 || charCode > 57)) {
                alert("This field will not accept the alphabet, Please Enter Only number");
                return false;
            }
            return true;
        }
        //
    </script>
</asp:Content>

