<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Region_Master.master" AutoEventWireup="true" CodeFile="RM_Generate_Passing_Order.aspx.cs" Inherits="Region_RM_Generate_Passing_Order" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="<%= ResolveUrl("~/NEW_CSS/js/jquery-1.12.4.js") %>"></script>
    <link href="../assets/New/css/select2.min.css" rel="stylesheet" />
    <script type="text/javascript" src="../assets/New/js/select2.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $("[id*=ddlBranch]").select2();
        });
    </script>
    <script type="text/javascript">
        $(function () {
            $("[id*=ddlGodownType]").select2();
        });
    </script>
    <script type="text/javascript">
        $(function () {
            $("[id*=ddlgdwn]").select2();
        });

    </script>
    <script type="text/javascript">
        $(function () {
            $("[id*=ddlbillno]").select2();
        });

    </script>
    <style type="text/css">
        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=60);
            opacity: 0.6;
        }

        .modalPopup {
            background-color: #FFFFFF;
            width: 80%;
            border: 3px solid #0DA9D0;
            border-radius: 6px;
            padding: 0
        }

            .modalPopup .header {
                background-color: #2FBDF1;
                height: 30px;
                color: White;
                line-height: 30px;
                text-align: center;
                font-weight: bold;
                border-top-left-radius: 6px;
                border-top-right-radius: 6px;
            }

            .modalPopup .body {
                min-height: 50px;
                line-height: 30px;
                text-align: center;
                font-weight: bold;
            }

            .modalPopup .footer {
                padding: 6px;
            }

            .modalPopup .yes, .modalPopup .no {
                height: 23px;
                color: White;
                line-height: 23px;
                text-align: center;
                font-weight: bold;
                cursor: pointer;
                border-radius: 4px;
            }

            .modalPopup .yes {
                background-color: #2FBDF1;
                border: 1px solid #0DA9D0;
            }

            .modalPopup .no {
                background-color: #9F9F9F;
                border: 1px solid #5C5C5C;
            }
    </style>

    <script type="text/javascript">
        function PrintDiv() {
            var divContents = document.getElementById("PrintDiv").innerHTML;
            var printWindow = window.open('', '', 'height=700,width=1000');
            //   printWindow.document.write('<html><head><title>WHR</title>');
            printWindow.document.write('</head><body >');
            printWindow.document.write(divContents);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            printWindow.print();
            printWindow.close();
        }
    </script>

    <script type="text/javascript">
        function PrintDiv_det() {
            var divContents = document.getElementById("PrintDiv_Det").innerHTML;
            var printWindow = window.open('', '', 'height=700,width=1000');
            //   printWindow.document.write('<html><head><title>WHR</title>');
            printWindow.document.write('</head><body >');
            printWindow.document.write(divContents);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            printWindow.print();
            printWindow.close();
        }
    </script>


    <style type="text/css">
        .button {
            background-color: #4CAF50; /* Green */
            border: none;
            color: white;
            padding: 0px 0px;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            font-size: 12px;
            font-weight: bold;
            margin: 4px 2px;
            -webkit-transition-duration: 0.4s; /* Safari */
            transition-duration: 0.4s;
            cursor: pointer;
        }

        .button1 {
            background-color: white;
            color: black;
            border: 2px solid #4CAF50;
        }

            .button1:hover {
                background-color: #4CAF50;
                color: white;
            }

        .button2 {
            background-color: white;
            color: black;
            border: 2px solid #008CBA;
        }

            .button2:hover {
                background-color: #008CBA;
                color: white;
            }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset style="width: 900px; border: 2px solid navy; margin-left: 5px; background-color: white;">
        <center>
            <div>

                <table width="100%">
                    <tr style="background-color: #0bb6e6; height: 25px">
                        <td colspan="4" align="center">
                            <asp:Label ID="Label37" runat="server" Font-Bold="True" Font-Size="12pt" ForeColor="WhiteSmoke"
                                Text="JVS/RENTAL/PPP etc Mode Godown/Other Generated Bill Passed/ Senction By RO"></asp:Label></td>
                    </tr>

                    <tr>
                        <td>
                            <table width="100%">

                                <tr>
                                    <td style="height: 40px; font-size: 14px" align="left">Branch
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlBranch" runat="server"
                                            Height="25px" Width="250px" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="height: 40px; font-size: 14px" align="left">Godown Type
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlGodownType" runat="server" Width="250px" Height="25px"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlGodownType_SelectedIndexChanged">
                                            <asp:ListItem Value="0" Selected="True">--Select--</asp:ListItem>
                                            <asp:ListItem Value="2">JVS Godowns</asp:ListItem>
                                            <asp:ListItem Value="3">Hired Godowns</asp:ListItem>
                                            <asp:ListItem Value="4">Silo Bags</asp:ListItem>
                                            <asp:ListItem Value="6">Tribal Scheme</asp:ListItem>
                                            <asp:ListItem Value="7">PMS CAP</asp:ListItem>
                                            <asp:ListItem Value="8">BOT</asp:ListItem>
                                            <asp:ListItem Value="9">BOT-AUB</asp:ListItem>
                                            <asp:ListItem Value="10">PVT.PEG</asp:ListItem>

                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>

                                    <td style="height: 40px; font-size: 14px" align="left">Godown 
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlgdwn" runat="server"
                                            Height="25px" Width="250px" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlgdwn_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="height: 30px; font-size: 14px" align="left">Rent Bill No.
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlbillno" runat="server"
                                            Height="25px" Width="250px" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlbillno_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblSBill" runat="server" Visible="false"
                                            Text=""></asp:Label>
                                    </td>
                                    <%--</tr> 
<tr>
<td style="font-size:14px" align="left" >
     Storage Charges Bill No. 
</td>
<td align="left">     
     <asp:DropDownList ID="ddlactualbill" runat="server" 
             Height="25px" Width="250px"  AutoPostBack="true" 
         onselectedindexchanged="ddlactualbill_SelectedIndexChanged"> </asp:DropDownList>
</td>

<%--<td style="height: 30px ; font-size:14px" align="left">
     Rent Bill No.
</td>
<td align="left">     
     <asp:DropDownList ID="ddlbillno" runat="server" 
             Height="25px" Width="250px"  AutoPostBack="true"
         onselectedindexchanged="ddlbillno_SelectedIndexChanged"> </asp:DropDownList>
</td>

</tr>--%>
                            </table>
                        </td>
                    </tr>

                    <tr id="trdet" runat="server" visible="false">
                        <td>
                            <table width="100%">


                                <tr>
                                    <td align="center" colspan="4">
                                        <div style="width: 100%;">
                                            <img id="Img1" src="../Images/line.png" height="15px" width="100%" alt="" />
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" align="center" style="vertical-align: top; height: 30px">
                                        <asp:Label ID="Label18" runat="server" Font-Size="12pt" ForeColor="Blue"
                                            Text="ब्रांच मैनेजर द्वारा बनाए गए बिल एवम्‌ किये गए कटोत्रा का विवरण Part ('A')"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td align="right" style="font-size: 14px;">
                                        <asp:Label ID="Label25" runat="server" Text="Month :"></asp:Label>&nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td align="left" style="font-size: 14px;">
                                        <asp:Label ID="lblrmmonth" runat="server" Text="माह :"></asp:Label></td>
                                    <td align="right" style="font-size: 14px;">
                                        <asp:Label ID="Label14" runat="server" Text="Financial Year :"></asp:Label>&nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td align="left" style="font-size: 14px;">
                                        <asp:Label ID="lblfinancial" runat="server" Text="माह :"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td align="right" style="font-size: 14px;">
                                        <asp:Label ID="Label6" runat="server" Text="From Date :"></asp:Label>&nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td align="left" style="font-size: 14px;">
                                        <asp:Label ID="lblfrmdate" runat="server" Text="माह :"></asp:Label></td>
                                    <td align="right" style="font-size: 14px;">
                                        <asp:Label ID="Label10" runat="server" Text="To Date :"></asp:Label>&nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td align="left" style="font-size: 14px;">
                                        <asp:Label ID="lbltodate" runat="server" Text="माह :"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td align="right" style="font-size: 14px;">
                                        <asp:Label ID="Label27" runat="server" Text="Commodity :"></asp:Label>&nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td align="left" style="font-size: 14px;">
                                        <asp:Label ID="lblrmcmd" runat="server" Text="माह :"></asp:Label></td>
                                    <td align="right" style="font-size: 14px;">
                                        <asp:Label ID="Label20" runat="server" Text="Rate :"></asp:Label>&nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td align="left" style="font-size: 14px;">
                                        <asp:Label ID="lblCPYear" runat="server"></asp:Label></td>
                                </tr>




                                <tr>
                                    <td style="height: 30px; font-size: 14px; width: 200px" align="right">Rent Bill Amount :&nbsp;&nbsp;&nbsp;
                                    </td>

                                    <td>
                                        <asp:TextBox runat="server" ID="txtPAmount" Width="100px" ForeColor="Red"
                                            Font-Bold="true" Text="0" ReadOnly="True"></asp:TextBox>

                                    </td>

                                    <td style="height: 30px; font-size: 14px;" align="right">विभिन्न मदो मे कुल कटोत्रा राशि :&nbsp;&nbsp;&nbsp;
                                    </td>

                                    <td>
                                        <%--<asp:Label ID="Label20" runat="server" Font-Size="14px" Font-Bold="true" Text="5258" ForeColor="Red" ></asp:Label>--%>
                                        <asp:TextBox runat="server" ID="txtresdetAmt" Width="100px" ForeColor="Red"
                                            Font-Bold="true" Text="0"
                                            ReadOnly="True"></asp:TextBox>
                                    </td>
                                </tr>

                                <tr runat="server" visible="false">
                                    <td style="height: 30px; font-size: 14px; width: 200px" align="right">Rent Bill Amount :&nbsp;&nbsp;&nbsp;
                                    </td>

                                    <td>
                                        <asp:TextBox runat="server" ID="txtBillAmt" Width="100px" ForeColor="Red"
                                            Font-Bold="true" Text="0" ReadOnly="True"></asp:TextBox>
                                    </td>
                                </tr>

                                <tr>
                                    <td style="height: 5px;"></td>
                                </tr>
                                <tr>
                                    <td colspan="4" align="center" style="height: 40px; vertical-align: top; height: 30px">
                                        <asp:Label ID="Label2" runat="server" Font-Size="12pt" ForeColor="Blue"
                                            Text="शाखा प्रबंधक द्वारा परिपत्र क्रमांक 1628 दिनांक 23/09/2022 की कंडिका 11,12 के तहत किये गये कटोत्रा का विवरण।"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="height: 30px; font-size: 14px;" align="right" colspan="3">1  प्रमुख सचिव म.प्र. शाशन खाद्य नागरिक आपूर्ति एवं उपभोगता संरक्षण विभाग भोपाल द्वारा जारी परिपत्र  क्रमांक <a href="https://mpwarehousing.mp.gov.in/Upload/Letter%201628%20Date%2023-09-2022_220923_170348.pdf" target="_blank" style="color: red;">1628 दिनांक 23/09/2022 </a>की कंडिका 11 के तहत  कटोत्रा"
                                    </td>
                                    <td align="left">

                                        <asp:TextBox runat="server" ID="txtlocknotopen" Width="203px" ForeColor="Red" Text="0" onkeypress="return isNumberKey(event)" OnTextChanged="txtlocknotopen_TextChanged" AutoPostBack="true"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="reqName" ControlToValidate="txtlocknotopen" ValidationGroup="LoginFrame"
                                            runat="server" ErrorMessage="यदी कोई भी कटोत्रा नहीं है तो '0' की प्रविष्टि करें" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>

                                <tr>
                                    <td style="height: 30px; font-size: 14px;" align="right" colspan="3">2 प्रमुख सचिव म.प्र. शाशन खाद्य नागरिक आपूर्ति एवं उपभोगता संरक्षण विभाग भोपाल द्वारा जारी परिपत्र  क्रमांक <a href="https://mpwarehousing.mp.gov.in/Upload/Letter%201628%20Date%2023-09-2022_220923_170348.pdf" target="_blank" style="color: red;">1628 दिनांक 23/09/2022 </a>की कंडिका 12 के तहत  कटोत्रा
                                    </td>
                                    <td align="left">

                                        <asp:TextBox runat="server" ID="txtRoadBlock" Width="203px" ForeColor="Red" Text="0" onkeypress="return isNumberKey(event)" OnTextChanged="txtRoadBlock_TextChanged" AutoPostBack="true"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtRoadBlock" ValidationGroup="LoginFrame"
                                            runat="server" ErrorMessage="यदी कोई भी कटोत्रा नहीं है तो '0' की प्रविष्टि करें" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </td>

                                </tr>
                                <%--द्वारा परिपत्र क्रमांक 1628 दिनांक 23/09/2022 की कंडिका 11,12 के तहत--%>
                                <tr>
                                    <td style="height: 5px;"></td>
                                </tr>
                                <tr>
                                    <td style="height: 30px; font-size: 14px;" align="right" colspan="3">शाखा प्रबंधक क़े द्वारा किये गये कटोत्रा से क्या आप सहमत/असहमत विकल्प चुने
                                    </td>
                                    <td align="left">

                                        <asp:DropDownList ID="ddlyesno" runat="server" Width="203px" Height="25px"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlyesno_SelectedIndexChanged">
                                            <asp:ListItem Value="0" Selected="True">--Select--</asp:ListItem>
                                            <asp:ListItem Value="1">सहमत</asp:ListItem>
                                            <asp:ListItem Value="2">असहमत</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ControlToValidate="ddlyesno" ID="RequiredFieldValidator2"
                                            ValidationGroup="LoginFrame" CssClass="errormesg" ErrorMessage="Please select सहमत / असहमत"
                                            InitialValue="0" runat="server" Display="Dynamic">
                                        </asp:RequiredFieldValidator>
                                    </td>

                                </tr>

                                <tr>
                                    <td style="height: 5px;"></td>
                                </tr>
                                <tr>
                                    <td colspan="4" align="center" style="height: 40px; vertical-align: top; height: 30px">
                                        <asp:Label ID="Label1" runat="server" Font-Size="12pt" ForeColor="Blue"
                                            Text="वैज्ञानिक भंडारण पर किराया"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="height: 30px; font-size: 14px; width: 200px" align="right">Scientific Capacity :&nbsp;&nbsp;&nbsp;
                                    </td>

                                    <td>
                                        <asp:TextBox runat="server" ID="txtSC" Width="100px" ForeColor="Red"
                                            Font-Bold="true" Text="0" ReadOnly="True"></asp:TextBox>
                                    </td>

                                    <td style="height: 30px; font-size: 14px;" align="right">No. of Days :&nbsp;&nbsp;&nbsp;
                                    </td>

                                    <td>
                                        <asp:TextBox runat="server" ID="txtnoofdays" Width="100px" ForeColor="Red"
                                            Font-Bold="true" Text="0"
                                            ReadOnly="True"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 30px; font-size: 14px; width: 200px" align="right">Per Day Rate :&nbsp;&nbsp;&nbsp;
                                    </td>

                                    <td>
                                        <asp:TextBox runat="server" ID="txtPer_Day_Rate" Width="100px" ForeColor="Red"
                                            Font-Bold="true" Text="0" ReadOnly="True"></asp:TextBox>
                                    </td>

                                    <td style="height: 30px; font-size: 14px;" align="right">Rent Amount on scientific Capacity :&nbsp;&nbsp;&nbsp;
                                    </td>

                                    <td>
                                        <asp:TextBox runat="server" ID="txtRASC" Width="100px" ForeColor="Red"
                                            Font-Bold="true" Text="0"
                                            ReadOnly="True"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="4">
                                        <div style="width: 100%;">
                                            <img id="Img3" src="../Images/line.png" height="15px" width="100%" alt="" />
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" align="center" style="height: 40px; vertical-align: top; height: 30px">
                                        <asp:Label ID="Label24" runat="server" Font-Size="12pt" ForeColor="Blue"
                                            Text="बनाए गए बिल मे RM द्वारा किये जाने वाले कटोत्रा का भाग Part ('B')"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="height: 30px; font-size: 14px;" align="right">TDS :&nbsp;&nbsp;&nbsp;
                                    </td>

                                    <td>
                                        <asp:TextBox runat="server" ID="txtTDSAmt" Width="100px" ForeColor="Red" AutoPostBack="true"
                                            Font-Bold="true" Text="0" OnTextChanged="txtTDSAmt_TextChanged"
                                            ToolTip="If Any Change you can enter it other wise by default 10%"></asp:TextBox>
                                    </td>

                                    <td style="height: 30px; font-size: 14px;" align="right">गेन / कमी के विरुध रोकी / काटी गई राशि :&nbsp;&nbsp;&nbsp;
                                    </td>

                                    <td>
                                        <asp:TextBox runat="server" ID="txtGainAmt" Width="100px" ForeColor="Red" AutoPostBack="true"
                                            Font-Bold="true" Text="0" OnTextChanged="txtGainAmt_TextChanged"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 40px; font-size: 14px;" align="right">सुरक्षा निधि राशि :&nbsp;&nbsp;&nbsp;
                                    </td>

                                    <td>
                                        <asp:TextBox runat="server" ID="txtscrDep" Width="100px" ForeColor="Red" AutoPostBack="true"
                                            Font-Bold="true" Text="0" OnTextChanged="txtscrDep_TextChanged"></asp:TextBox>
                                    </td>

                                    <td style="height: 30px; font-size: 14px;" align="right">कोई अन्य कटोत्रा :&nbsp;&nbsp;&nbsp;
                    <asp:TextBox runat="server" ID="txtotherdetuction" Width="100px" AutoPostBack="true"
                        ForeColor="Red" Font-Bold="true" Text="0" OnTextChanged="txtotherdetuction_TextChanged"></asp:TextBox>
                                    </td>

                                    <td>
                                        <asp:TextBox runat="server" ID="txtODResion" Width="100px" AutoPostBack="true" TextMode="MultiLine" placeholder="अन्य कटौत्रा कारण"
                                            ForeColor="Red" Font-Bold="true" Text=""></asp:TextBox>
                                    </td>

                                </tr>
                                <tr>
                                    <td style="height: 5px;"></td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="4">
                                        <div style="width: 100%;">
                                            <img id="Img2" src="../Images/line.png" height="15px" width="100%" alt="" />
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" align="center" style="vertical-align: top; height: 30px">
                                        <asp:Label ID="Label22" runat="server" Font-Size="12pt" ForeColor="Blue"
                                            Text="बिल मे कुल कटौत्रे एवं गोदाम संचालक/सर्विस प्रोवाइडर को प्राप्त होने वाली राशि का विवरण"></asp:Label></td>
                                </tr>


                                <%--<tr>
<td colspan="3" align="left" style="height:25px;  font-size:14px;">
1) MPWLC द्वारा भंडारित स्कंध की जमाकर्ता को प्रस्तुत की जाने वाली राशि : 
</td>
<td style="height:25px; font-size:14px;" align="left">
<asp:Label ID="lblPrastut_netamount" runat="server" Font-Size="14px" Font-Bold="true" ForeColor="Red" Text="0"> </asp:Label>
</td>
</tr>   --%>
                                <tr>
                                    <td colspan="3" align="left" style="height: 25px; font-size: 14px;">1) Rent Bill Amount  : 
                                    </td>
                                    <td style="height: 25px; font-size: 14px;" align="left">
                                        <asp:Label ID="lblRentAmt" runat="server" Font-Size="14px" Font-Bold="true" ForeColor="Red" Text="0"> </asp:Label>
                                        <asp:Label ID="lblPrastut_netamount" runat="server" Font-Size="14px" Font-Bold="true" ForeColor="Red" Text="0" Visible="false"> </asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 25px; font-size: 14px;" align="left" colspan="3">2) Rent Bill Amount मे कुल किये गए कटोत्रा की राशि : 
    
                                    </td>
                                    <td style="height: 25px; font-size: 14px;" align="left">
                                        <asp:Label ID="lbltotDetuc" runat="server" Font-Size="14px" Font-Bold="true" ForeColor="Red" Text="0"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 25px; font-size: 14px;" align="left" colspan="3">3) TDS : 
    
                                    </td>
                                    <td style="height: 25px; font-size: 14px;" align="left">
                                        <asp:Label ID="lblFinalTDS" runat="server" Font-Size="14px" Font-Bold="true" ForeColor="Red" Text="0"></asp:Label>
                                    </td>
                                </tr>

                                <tr>
                                    <td style="height: 25px; font-size: 14px;" align="left" colspan="3">4) गोदाम संचालक को देय शुध राशि  (After Detuction) : 
    
                                    </td>
                                    <td style="height: 25px; font-size: 14px;" align="left">
                                        <asp:Label ID="lblgrandtotal" runat="server" Font-Size="14px" Font-Bold="true" ForeColor="Red" Text="0"></asp:Label>
                                        <asp:Label ID="lblMPWLCPartAmt" runat="server" Font-Size="14px" Font-Bold="true" ForeColor="Red" Text="0" Visible="false"></asp:Label>
                                    </td>
                                </tr>

                                <%--<tr>
    <td style="height:25px; font-size:14px;" align="left" colspan="3" >5) MPWLC के पार्ट की राशि : 
    
    </td>
<td style="height:25px; font-size:14px;" align="left">
<asp:Label ID="lblMPWLCPartAmt" runat="server" Font-Size="14px" Font-Bold="true" ForeColor="Red" Text="0"></asp:Label>
</td>    
</tr>--%>
                                <tr>
                                    <td align="center" colspan="4" style="height: 50px;">
                                        <asp:Button class="button button2" ID="btnGenerateBill" Style="width: 150px"
                                            runat="server" Text="Submit" Height="29px" OnClick="btnGenerateBill_Click"></asp:Button>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="color: Red" align="left">&nbsp;Note :</td>
                                </tr>
                                <tr>
                                    <td colspan="4" align="left" style="color: Red; font-size: 14px;">&nbsp;1)  गोदाम संचालक को देय शुध राशि =  JVS Bill Amount - ( विभिन्न मदो मे कुल कतोत्र + TDS + गेन/कमी के विरुध रोकी/<br />
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;काटी गई राशि + सुरक्षा निधि राशि +  कोई अन्य कटोत्रा )                            
                                    </td>
                                </tr>
                                <%--  <tr>
                          <td colspan="4" align="left" style="color:Red ; font-size:14px;" >
                            &nbsp;2)  MPWLC के पार्ट की राशि =  MPWLC द्वारा भंडारित स्कंध की जमाकर्ता को प्रस्तुत की जाने वाली राशि - JVS Bill Amount                           
                            </td>
                              <td colspan="4" align="left" style="color:Red ; font-size:14px;" >
                            &nbsp;2)  MPWLC के पार्ट की राशि =  JVS Bill Amount - गोदाम संचालक को देय शुध राशि                           
                            </td>
                        </tr>           --%>
                            </table>
                        </td>
                    </tr>
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

