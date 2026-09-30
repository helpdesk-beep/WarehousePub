<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Branch_FillInspection_Rabi_JVS25_26.aspx.cs" Inherits="JointVentureScheme_Branch_FillInspection_Rabi_JVS25_26" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Warehouse Inspection</title>
    <meta charset="urf-8" />
    <meta name="viewport" content="width=device-width" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link rel="stylesheet" href="css/main.css" type="text/css" />
    <script type="text/javascript" src="js/jquery-1.4.1.min.js"></script>
    <script type="text/javascript" src="js/menu.js"></script>
    <script type="text/javascript" src="js/slideshow.js"></script>
    <script type="text/javascript" src="js/cufon-yui.js"></script>
    <script type="text/javascript" src="js/Arial.font.js"></script>
    <script type="text/javascript">
        Cufon.replace('h1,h2,h3,h4,h5,#menu,#copy,.blog-date');
    </script>
    <script type="text/javascript">
        function preventInput(evnt) {
            //Checked In IE9,Chrome,FireFox
            if (evnt.which != 9) evnt.preventDefault();
        }
    </script>


     
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

        .style2 {
            height: 11px;
        }

        .style4 {
            height: 10px;
        }
    </style>
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
            border-radius: 12px;
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

        .auto-style1 {
            height: 50px;
        }

        .auto-style2 {
            width: 397px;
        }
    </style>

    <script type="text/javascript">
        function preventInput(evnt) {
            //Checked In IE9,Chrome,FireFox
            if (evnt.which != 9) evnt.preventDefault();
        }



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
    </script >
</head >
            <body>

                <div id="bg" style="background-color: White; color: Black">
                    <div class="wrap">
                        <img src="../images/CH.jpg" style="width: 100%" alt="" height="160" />

                        <div>

                            <form id="form1" runat="server">
                                <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></cc1:ToolkitScriptManager>


                                <center>

                                    <div>
                                    </div>

                                    <table style="width: 100%">
                                        <tr>
                                            <td style="font-size: medium; width: 1000px;">
                                                <table style="width: 100%; height: 32px; font-size: medium;">
                                                    <tr>
                                                        <td style="background-color: #008CBA; width: 70PX;" align="center">
                                                            <asp:LinkButton ID="LinkButton10" Text="Home" runat="server" PostBackUrl="~/JointVentureScheme/BranchHome.aspx" ForeColor="White"></asp:LinkButton>
                                                        </td>
                                                        <td style="background-color: #008CBA; font-size: medium; color: White; width: 100px" align="center">Welcome&nbsp;<asp:Label ID="lbluser" runat="server" ForeColor="White"></asp:Label></td>
                                                        <td style="background-color: #008CBA; width: 70px;" align="center">
                                                            <asp:LinkButton ID="LinkButton1" runat="server" ForeColor="White"
                                                                OnClick="LinkButton1_Click">Log out</asp:LinkButton></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="border-color: #008CBA; border-style: solid; border-width: 2px; background-color: White" align="center">
                                                <p style="font-size: 14px; color: Black;">
                                                    Inspection for Offered Godown(JVS Rabi 2025-26)
                                                </p>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <p style="color: Red;">
                                                                महत्वपूर्ण नोट :-<br />
                                                                कृपया समस्त जानकारी अँग्रेजी भाषा मे टाइप करे।
                                                                <br />
                                                                यदि WHMS ड्रॉपडाउन मे गोदाम का नाम नहीं दिख रहा हे ओर वह गोदाम पूर्व मे भी आफ़र किया गया है एवं WHR जारी करने के लिए लॉगिन भी उपलब्ध कराया गया तो पहले शुनिश्चित करे की वह गोदाम Verified/Existance List मे हो।
                                                            </p>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblWarehouse" runat="server" Text="Warehouse Name "></asp:Label>
                                                            &nbsp;&nbsp;
                                                            <asp:DropDownList ID="ddlWarName" runat="server" AutoPostBack="true"
                                                                Width="200px" Height="25px"
                                                                OnSelectedIndexChanged="ddlWarName_SelectedIndexChanged">
                                                            </asp:DropDownList>

                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblgodown" runat="server" Text="Godown No."></asp:Label>&nbsp;&nbsp;<asp:DropDownList ID="ddlgodown" runat="server" AutoPostBack="true"
                                                                Width="100px" Height="25px" OnSelectedIndexChanged="ddlgodown_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label1" runat="server" Text="Godown ID (AS Per WMS)"></asp:Label>&nbsp;&nbsp;<asp:DropDownList ID="ddlWMSGodownID" runat="server"
                                                                Width="200px" Height="25px" OnSelectedIndexChanged="ddlWMSGodownID_SelectedIndexChanged" AutoPostBack="true">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>

                                        <tr>
                                <%--                      <td  colspan="4" style="background-color: #66CCFF;width:954px">
                          <p style="font-size: medium; color: #008080;">
                          A) Inspection Officer Detail  </p>
                      </td>--%>
                                <td style="background-color: #66CCFF; height: 25px" align="center">
                                    <p style="font-size: 14px; color: Black; font-weight: bold">A) Inspection Officer Detail</p>
                                </td>

                            </tr>
                        </table>


                        <table style="width: 100%">
                            <tr>
                                <td>शाखा प्रबन्धक का नाम :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtBranchBM" runat="server" class="text" type="text" ReadOnly="true"></asp:TextBox>

                                </td>
                                <td>निरीक्षण दिनांक :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtInspDate" runat="server" class="text" type="text" onkeydown="javascript:preventInput(event);" onpaste="return false;"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                        TargetControlID="txtInspDate">
                                    </cc1:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="background-color: #66CCFF; height: 25px" align="center">
                                    <p style="font-size: 14px; color: Black; font-weight: bold">B)	Inspected Warehouse Campus Details</p>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                            </tr>
                            <tr>
                                <td style="height: 20px">जिले का नाम :
                                </td>
                                <td>
                                    <asp:DropDownList ID="DDLDistrict" runat="server" Enabled="false"
                                        Width="232px" Height="27px">
                                    </asp:DropDownList>
                                </td>

                                <td>तहसील का नाम :
                                </td>
                                <td>
                                    <asp:DropDownList ID="DDLTehsil" runat="server" Enabled="false"
                                        Width="232px" Height="27px">
                                    </asp:DropDownList>

                                </td>
                            </tr>
                            <tr>
                                <td>विकासखंड का नाम  :
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlblocknew" runat="server" Enabled="false"
                                        Width="232px" Height="27px">
                                    </asp:DropDownList>
                                </td>
                                <td style="height: 20px">गाँव का नाम :
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlvillage" runat="server"
                                        Width="232px" Height="27px">
                                    </asp:DropDownList>

                                </td>
                            </tr>
                            <tr>
                                <td>वेअरहाउस की निकटतम शाखा का नाम  :
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlNearBranch" runat="server" Enabled="false"
                                        Width="232px" Height="27px">
                                    </asp:DropDownList>
                                </td>
                                <td style="height: 20px">गोदाम से दूरी(km) :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDistance" runat="server" class="text" type="text"></asp:TextBox>

                                </td>
                            </tr>
                            <tr>
                                <td style="height: 20px; width: 225px">वेअरहाउस/गोदाम का नाम :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtWareName" runat="server" class="text" type="text"></asp:TextBox>

                                </td>
                                <td>वेअरहाउस/गोदाम का  नंबर :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtGodNo" runat="server" class="text" type="text" ReadOnly="true"></asp:TextBox>


                                </td>
                            </tr>

                            <tr>
                                <td style="height: 20px">वेअरहाउस का पता :
                                </td>
                                <td>

                                    <textarea id="txtGodAdd" maxlength="150" runat="server" cols="20" rows="2" name="radr" class="text" style="width: 223px;"></textarea>
                                </td>
                                <td>वेअरहाउस परिसर का कुल क्षेत्रफल (in Hect.):
                                </td>
                                <td>
                                    <asp:TextBox ID="txtWareArea" runat="server" class="text" type="text"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender14" runat="server" TargetControlID="txtWareArea"
                                        ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender>


                                </td>
                            </tr>
                            <tr>
                                <td>अक्षांश :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtLatitude" runat="server" class="text" type="text"></asp:TextBox>


                                </td>
                                <td style="height: 20px">देशान्त :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtLongitude" runat="server" class="text" type="text"></asp:TextBox>

                                </td>
                            </tr>
                            <tr>

                                <td>वेअरहाउस कार्यालय का मोबाइल नंबर :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtGodContact" runat="server" class="text" type="text"></asp:TextBox>


                                </td>
                                <td style="height: 20px">वेअरहाउस प्रभारी का मोबाइल नंबर :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtInchMbNo" runat="server" class="text" type="text"></asp:TextBox>

                                </td>
                            </tr>


                            <tr>

                                <td>वेअरहाउस की निकटतम रेल्वे रेक पॉइंट  का नाम  :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtrackpoint" runat="server" class="text" type="text"></asp:TextBox>

                                </td>
                                <td style="height: 20px">गोदाम से दूरी(km) :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtrackpintdist" runat="server" class="text" type="text"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtrackpintdist"
                                        ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender>

                                </td>
                            </tr>
                            <tr>
                                <td></td>
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <td colspan="4" style="background-color: #66CCFF; height: 25px" align="center">
                                    <p style="font-size: 14px; color: Black; font-weight: bold">C)	Warehouse Owner Details </p>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                            </tr>
                            <tr>
                                <td style="height: 20px; width: 225px">वेअरहाउस  संचालक  का नाम :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtOwnerName" runat="server" class="text" type="text"></asp:TextBox>

                                </td>
                                <td style="height: 20px; width: 250px">वेअरहाउस  संचालक  का पता :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtOwnerAdd" runat="server" class="text" type="text"></asp:TextBox>


                                </td>
                            </tr>
                            <tr>
                                <td style="height: 20px">वेअरहाउस  संचालक  का मोबाइल नंबर :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtOwnerMob" runat="server" class="text" type="text" ReadOnly="true"></asp:TextBox>
                                </td>
                                <td>वेअरहाउस  संचालक  का email ID :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtOwnerEmail" runat="server" class="text" type="text" ReadOnly="true"></asp:TextBox>


                                </td>
                            </tr>
                            <tr>
                                <td style="height: 20px">PAN No.:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPanNo" runat="server" class="text" type="text"></asp:TextBox>

                                </td>
                                <td>Aadhaar No.   :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtAadharNo" runat="server" class="text" type="text"></asp:TextBox>


                                </td>
                            </tr>

                            <tr>
                                <td></td>
                            </tr>
                            <tr>

                                <td colspan="4" style="background-color: #66CCFF; height: 25px" align="center">
                                    <p style="font-size: 14px; color: Black; font-weight: bold">D)	Inspected Warehouse / Godown  Details</p>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                            </tr>
                            <tr>
                                <td style="height: 20px">लंबाई मापन(फीट) :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtLength" runat="server" class="text" type="text"></asp:TextBox>

                                </td>
                                <td>चौडाई  मापन(फीट)  :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtWidth" runat="server" class="text" type="text"></asp:TextBox>


                                </td>
                            </tr>
                            <tr>
                                <td style="height: 20px">ऊँचाई मापन(फीट)(अधिकतम 18 फिट) :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtHeight" runat="server" class="text" type="text"></asp:TextBox>

                                </td>
                                <td>संगणित भण्डारण क्षमता(में टन )-सूत्र (LxWx(H-3)/80) अनुसार भण्डारण क्षमता  :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtMaxCpt" runat="server" class="text" type="text"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 20px">निर्माण वर्ष :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtConsYear" runat="server" class="text" type="text"></asp:TextBox>

                                </td>
                                <td>गोदाम की एक दिन में अनुमानित उतराई क्षमता (मे.टन):
                                </td>
                                <td>
                                    <asp:TextBox ID="txtUnloadCpt" runat="server" class="text" type="text"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtUnloadCpt"
                                        ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender>

                                </td>
                            </tr>
                            <tr>
                                <td style="height: 20px">भण्डारण प्रकार :
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlStorageType" runat="server"
                                        Width="100" Height="27px" Enabled="false">
                                        <asp:ListItem>--Select--</asp:ListItem>
                                        <asp:ListItem Value="1" Selected="True">Covered </asp:ListItem>
                                        <asp:ListItem Value="2">Permanent(CAP)</asp:ListItem>
                                        <asp:ListItem Value="3">Temporary (CAP) </asp:ListItem>
                                        <asp:ListItem Value="4">Silo Bag  </asp:ListItem>
                                        <asp:ListItem Value="5">Steel Silo </asp:ListItem>
                                    </asp:DropDownList>

                                </td>
                                <td>वर्तमान में गोदाम में संग्रहित स्कंधो का नाम :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCommodityName" runat="server" class="text" type="text"></asp:TextBox>


                                </td>
                            </tr>
                            <tr>
                                <td style="height: 20px">गोदाम में संग्रहित स्कंध जमाकर्ता का नाम (MPSCSC/Markfed/Nafed/Private/Other)
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDepositorName" runat="server" class="text" type="text"></asp:TextBox>

                                </td>
                                <td>वर्तमान में गोदाम में संग्रहित स्कंध की क्षमता (मे.टन)  :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCurrentStoredComm" runat="server" class="text" type="text" Text="0"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtCurrentStoredComm"
                                        ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender>

                                </td>
                            </tr>
                            <tr>
                                <td style="height: 20px">वर्तमान में गोदाम की रिक्त क्षमता (मे.टन) (Vacant Capacity) :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtvacantcpt" runat="server" class="text" type="text"></asp:TextBox>

                                </td>
                                <%--                    <td>
                    गोदाम संचालक स्वतः अथवा आउट सोर्स के माध्यम से फ्यूमीगेशन कार्य करने हेतु सहमत / असहमत :
                    </td>
                    <td>
                                        <asp:DropDownList ID="ddlfumigation" runat="server" Height="25px" Width="232px"> 
                                            <asp:ListItem >--Select--</asp:ListItem>
                                            <asp:ListItem Value="Y">सहमत</asp:ListItem>
                                            <asp:ListItem Value="N">असहमत</asp:ListItem>
                                                                                   
                                        </asp:DropDownList>                    
                    </td>--%>
                            </tr>

                            <tr>

                                <td colspan="4" style="background-color: #66CCFF; height: 25px" align="center">
                                    <p style="font-size: 14px; color: Black; font-weight: bold">E)	Warehouse/Godown Licence Details- WDRA </p>
                                </td>
                            </tr>

                            <tr id="trWdralic1" runat="server">
                                <td>लायसेंस का प्रकार
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlWdragdwntype" runat="server" Height="25px" Width="232px" AutoPostBack="True" OnSelectedIndexChanged="ddlWdragdwntype_SelectedIndexChanged">
                                        <asp:ListItem>--Select--</asp:ListItem>
                                        <asp:ListItem Value="1" Text="Available"></asp:ListItem>
                                        <asp:ListItem Value="0" Text="Applied"></asp:ListItem>

                                    </asp:DropDownList>
                                </td>
                                <td>लायसेंस नंबर
                                </td>
                                <td>
                                    <asp:TextBox ID="txtlicno" runat="server" class="text" type="text"></asp:TextBox>
                                </td>
                            </tr>
                            <tbody id="show1" runat="server" visible="false">


                                <tr id="trWdralic2" runat="server">
                                    <td>जारी दिनांक
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWdralicissuedate" runat="server" class="text" type="text"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy"
                                            TargetControlID="txtWdralicissuedate">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td>वैधता दिनांक
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWdralicExpdate" runat="server" class="text" type="text"></asp:TextBox>

                                        <cc1:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy"
                                            TargetControlID="txtWdralicExpdate">
                                        </cc1:CalendarExtender>
                                    </td>
                                </tr>
                            </tbody>



                            <%/////////start/////////////%>
                            <tr>

                                <td colspan="4" style="background-color: #66CCFF; height: 25px" align="center">
                                    <p style="font-size: 14px; color: Black; font-weight: bold">E)	Warehouse/Godown Licence Details - FSSAI Certificate </p>
                                </td>
                            </tr>
                            <tr id="trfssailic1" runat="server">
                                <td>लायसेंस का प्रकार
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlfssaigdwntype" runat="server" Height="25px" AutoPostBack="True" Width="232px" OnSelectedIndexChanged="ddlfssaigdwntype_SelectedIndexChanged">
                                        <asp:ListItem>--Select--</asp:ListItem>
                                        <asp:ListItem Value="1" Text="Available"></asp:ListItem>
                                        <asp:ListItem Value="0" Text="Applied"></asp:ListItem>

                                    </asp:DropDownList>
                                </td>
                                <td>FSSAI लायसेंस नंबर
                                </td>
                                <td>
                                    <asp:TextBox ID="txtfssailicno" runat="server" class="text" type="text"></asp:TextBox>
                                </td>
                            </tr>
                            <tbody id="Tbody2" runat="server" visible="false">
                                <tr id="trfssailic2" runat="server">
                                    <td>FSSAI जारी दिनांक
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtfssailicissuedate" runat="server" class="text" type="text"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender7" runat="server" Format="dd/MM/yyyy"
                                            TargetControlID="txtfssailicissuedate">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td>FSSAI वैधता दिनांक
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtfssailicExpdate" runat="server" class="text" type="text"></asp:TextBox>

                                        <cc1:CalendarExtender ID="CalendarExtender8" runat="server" Format="dd/MM/yyyy"
                                            TargetControlID="txtfssailicExpdate">
                                        </cc1:CalendarExtender>
                                    </td>
                                </tr>
                            </tbody>


                            <%/////////End/////////////%>

                            <tr>

                                <td colspan="4" style="background-color: #66CCFF; height: 25px" align="center">
                                    <p style="font-size: 14px; color: Black; font-weight: bold">E)	Warehouse/Godown Licence Details - Directed Food </p>
                                </td>
                            </tr>

                            <tr id="trNonWdralic1" runat="server">
                                <td>लायसेंस का प्रकार
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlNonWdragdwntype" runat="server" AutoPostBack="True" Height="25px" Width="232px" OnSelectedIndexChanged="ddlNonWdragdwntype_SelectedIndexChanged">
                                        <asp:ListItem>--Select--</asp:ListItem>
                                        <asp:ListItem Value="1" Text="Available"></asp:ListItem>
                                        <asp:ListItem Value="0" Text="Applied"></asp:ListItem>

                                    </asp:DropDownList>
                                </td>
                                <td>Non Wdra लायसेंस नंबर
                                </td>
                                <td>
                                    <asp:TextBox ID="txtNonWdralicno" runat="server" class="text" type="text"></asp:TextBox>
                                </td>
                            </tr>
                            <tbody id="NonWdra" runat="server" visible="false">

                                <tr id="trNonWdralic2" runat="server">
                                    <td>Non Wdra जारी दिनांक
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNonWdralicissuedate" runat="server" class="text" type="text"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender9" runat="server" Format="dd/MM/yyyy"
                                            TargetControlID="txtNonWdralicissuedate">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td>Non Wdra वैधता दिनांक
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNonWdralicExpdate" runat="server" class="text" type="text"></asp:TextBox>

                                        <cc1:CalendarExtender ID="CalendarExtender10" runat="server" Format="dd/MM/yyyy"
                                            TargetControlID="txtNonWdralicExpdate">
                                        </cc1:CalendarExtender>
                                    </td>
                                </tr>

                            </tbody>

                            <tr>
                                <td align="center" colspan="4">
                                    <br />
                                    <asp:Label ID="lbllicmessage" runat="server" ForeColor="Red"></asp:Label><br />
                                </td>
                            </tr>

                            <%--  <tr>
                                <td colspan="4" align="right">

                                    <asp:LinkButton ID="lnklicdetail" Font-Underline="true" ForeColor="Blue"
                                        Font-Bold="true" Font-Size="11px" Text="जिले अनुसार जारी लायसेंस/पंजीयन धारको की सूची देखने के लिए क्लिक करे"
                                        runat="server" OnClick="lnklicdetail_Click"></asp:LinkButton>
                                </td>
                            </tr>
                            <tr>

                                <td align="center" colspan="4">
                                    <asp:Label ID="lblchklicnovalid" runat="server" Visible="false"></asp:Label>

                                    <asp:Button ID="btnvalidatelicno" runat="server" Text="Check Licence" class="submit"
                                        Width="150px" Height="30px" OnClick="btnvalidatelicno_Click"></asp:Button>
                                </td>
                            </tr>--%>

                           <%-- <tr>
                                <td colspan="4">
                                    <p style="color: Red;">
                                        नोट :-
                                        <br />
                                        1. दुर्ज किये गए गोदाम लायसेंस/पंजीयन नंबर को चेक करने के बाद ही गोदाम का निरीक्षण पुर्ण किया जाना संभव हे ।<br />
                                        1. यदि गोदाम लायसेंस/पंजीयन नंबर गलत हे तो WDRA/खाद्य नागरिक आपूर्ति एवं उपभोक्ता संरक्षण, मध्यप्रदेश जिले अनुसार धारको की सूची देखने के लिए दी गई लिंक पर क्लिक करे ।
                                    </p>
                                </td>

                            </tr>--%>


                            <tr>

                                <td colspan="4" style="background-color: #66CCFF; height: 25px" align="center">
                                    <p style="font-size: 14px; color: Black; font-weight: bold">F)	Godown Offer Details</p>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                            </tr>
                            <tr>
                                <td style="height: 20px; width: 225px">JVS में ऑनलाइन ऑफर दिनांक और समय :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtOnlineOffer" runat="server" class="text" type="text"
                                        ReadOnly="True"></asp:TextBox>

                                </td>

                                <td>श्रेणी ऑनलाइन ऑफर अनुसार :
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlJVCategory" runat="server"
                                        Width="232px" Height="27px" Enabled="false">
                                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                                        <asp:ListItem Value="81">A-SPMS(81)</asp:ListItem>
                                        <asp:ListItem Value="76">B-SPMS(76)</asp:ListItem>


                                    </asp:DropDownList>

                                </td>
                            </tr>
                            <tr>
                                <td style="height: 20px">ऑफर की गयी भंडारण क्षमता का प्रकार - (आंशिक/पूर्ण ) :
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlCptOffer" runat="server"
                                        Width="232px" Height="27px" Enabled="False">
                                        <asp:ListItem>--Select--</asp:ListItem>
                                        <asp:ListItem Value="P">Partial</asp:ListItem>
                                        <asp:ListItem Value="F">Full</asp:ListItem>

                                    </asp:DropDownList>

                                </td>
                                <td>JVS में ऑफर की गयी क्षमता :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtjvsofrcpt" runat="server" class="text" type="text" ReadOnly="true"></asp:TextBox>

                                </td>
                            </tr>

                        </table>
                        <table>

                            <tr>
                                <td colspan="4" style="background-color: #66CCFF; height: 25px" align="center">
                                    <p style="font-size: 14px; color: Black; font-weight: bold">G)	Important aspect of Godown to become Category/Scheme &nbsp;&nbsp;&nbsp; 'A-81' &nbsp;or &nbsp;'B-76&#39;</p>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 400px">परिसर में सी.सी./सुदृढ़सुदृढ़ डामरीकृत/WBM रोड़ के साथ-साथ बाउड्रीवाल एवं बारहमासी पहुंच मार्ग होना अनिवार्य हैं।
                                    <asp:Label ID="lblelectronicWeib" Visible="false" runat="server" ForeColor="White"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlElectWeigh" runat="server"
                                        Width="100px" Height="27px" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlElectWeigh_SelectedIndexChanged">
                                        <asp:ListItem>--Select--</asp:ListItem>
                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                        <asp:ListItem Value="0">No</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td class="auto-style2">गोदाम प‍रिसर में सत्‍यापित(न्‍यूनतम 60 मे.टन की क्षमता का) इलेक्ट्रॉनिक वेब्रिज हो।
                                    <asp:Label ID="lblgatechk" runat="server" Visible="false" ForeColor="White"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlGateType" runat="server"
                                        Width="100" Height="27px" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlGateType_SelectedIndexChanged">
                                        <asp:ListItem>--Select--</asp:ListItem>
                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                        <asp:ListItem Value="0">No</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>गोदाम के प्रत्‍येक शटर/गेट पर जालीदार शटर/जालीदार गेट मानक स्‍तर के होना चाहिए।
                                    <asp:Label ID="lblboundry" Visible="false" runat="server" ForeColor="White"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlboundrytype" runat="server"
                                        Width="100px" Height="27px" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlboundrytype_SelectedIndexChanged">
                                        <asp:ListItem>--Select--</asp:ListItem>
                                        <asp:ListItem Value="1">YES</asp:ListItem>
                                        <asp:ListItem Value="0">NO</asp:ListItem>

                                    </asp:DropDownList>
                                </td>
                                <td class="auto-style2">गोदाम परिसर में आवश्‍यकतानुसार नाइट विजन चालू हालत में कैमरे स्‍थापित होनी चाहिए।
                                    <asp:Label ID="lblrode" Visible="false" runat="server" ForeColor="White"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlRoadType" runat="server"
                                        Width="100" Height="27px" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlRoadType_SelectedIndexChanged">
                                        <asp:ListItem>--Select--</asp:ListItem>
                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                        <asp:ListItem Value="0">No</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>

                            <tr>
                                <td>गोदाम परिसर में सुव्‍यवस्थित कार्यालय,विद्युत व्यवस्था, इंटरनेट सुविधायुक्‍त होना चाहिए।
                                    <asp:Label ID="lblInternet" Visible="false" runat="server" ForeColor="White"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlInternet" runat="server"
                                        Width="100px" Height="27px" AutoPostBack="true" OnSelectedIndexChanged="ddlInternet_SelectedIndexChanged"
                                    >
                                        <asp:ListItem>--Select--</asp:ListItem>
                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                        <asp:ListItem Value="0">NO</asp:ListItem>

                                    </asp:DropDownList>
                                </td>
                            </tr>

                            <tr>
                                <%--                    <td>
                     क्या वेअरहाउस लायसेंस की वर्तमान वेधता है यदि नहीं हे तो क्या लायसेंस के लिए आवेदन किया गया हे
                    </td>
                    <td>
                      <asp:DropDownList ID="ddllicvalidapplied" runat="server"
                            Width="100px" Height="27px"  AutoPostBack="true"
                            onselectedindexchanged="ddllicvalidapplied_SelectedIndexChanged" >
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                        </asp:DropDownList>
                    </td>--%>

                                <td style="background-color: #F8CF6A; font-weight: bold; font-size: 14px;">निरीक्षण में पाई गई श्रेणी (निम्न अर्हताऐं अनुसार)
                                </td>
                                <td style="background-color: #F8CF6A;">
                                    <%--'A-SPMS (80 %)' &nbsp;or &nbsp;'A-PMS (55 %)'or &nbsp;'B-SPMS (75 %)'or &nbsp;'B-PMS (50 %)'--%>
                                    <asp:DropDownList ID="ddlinspectionScheme" runat="server"
                                        Width="100" Height="27px" Enabled="False">
                                       <%-- <asp:ListItem Value="0">--Select--</asp:ListItem>
                                        <asp:ListItem Value="80">A-SPMS(80%)</asp:ListItem>
                                        <asp:ListItem Value="75">A-PMS(75%)</asp:ListItem>

                                        <asp:ListItem Value="55">B-SPMS(55%)</asp:ListItem>
                                        <asp:ListItem Value="50">B-PMS(50%)</asp:ListItem>--%>

                                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                                        <asp:ListItem Value="81">A-SPMS(81)</asp:ListItem>
                                        <asp:ListItem Value="76">B-SPMS(76)</asp:ListItem>

                                        <asp:ListItem Value="55">A-PMS(55%)</asp:ListItem>
                                        <asp:ListItem Value="50">B-PMS(50%)</asp:ListItem>

                                    </asp:DropDownList>
                                </td>
                            </tr>

                            <tr>
                                <td colspan="4" style="background-color: #66CCFF; height: 25px" align="center">
                                    <p style="font-size: 14px; color: Black; font-weight: bold">खरीदी केंद्र स्थापित करने संबंधी जानकारी</p>
                                </td>
                            </tr>
                            <tr>
                                <td>क्या परिसर मे गोदाम स्तरीय खरीदी केंद्र स्थापित किया जा सकता हैं ?</td>
                                <td>
                                    <asp:DropDownList ID="ddlProcCenterEsta" runat="server"
                                        Width="100" Height="27px" AutoPostBack="false">
                                        <asp:ListItem>--Select--</asp:ListItem>
                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                        <asp:ListItem Selected="true" Value="2">No</asp:ListItem>
                                    </asp:DropDownList></td>
                                <td class="auto-style2">क्या गोदाम स्तरीय खरीदी केन्द्र बनाने हेतु गोदाम परिसर में पर्याप्त स्थान, सुविधाऐं उपलब्ध हैं ?
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlProcCenterFacility" runat="server"
                                        Width="100" Height="27px" AutoPostBack="false"
                                    >
                                        <asp:ListItem>--Select--</asp:ListItem>
                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                        <asp:ListItem selected="true" Value="2">No</asp:ListItem>
                                    </asp:DropDownList>
                                </td>

                            </tr>

                        </table>


                        <table style="width: 100%">
                            <tr>
                                <td colspan="6" style="background-color: #66CCFF; height: 25px" align="center">
                                    <p style="font-size: 14px; color: Black; font-weight: bold">H)	Important aspect of Warehouse to become UNFIT </p>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                            </tr>
                            <tr>
                                <td>1.
                                </td>
                                <td style="height: 20px;">क्या गोदाम निर्माणाधीन है ?
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlWareConstruct" runat="server" AutoPostBack="true"
                                        Width="100" Height="27px"
                                        OnSelectedIndexChanged="ddlWareConstruct_SelectedIndexChanged">
                                        <asp:ListItem>--Select--</asp:ListItem>
                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                        <asp:ListItem Value="0" Selected="True">No</asp:ListItem>

                                    </asp:DropDownList>

                                </td>
                                <td>2.
                                </td>

                                <td>क्या गोदामों में विगत वर्ष भण्डारित स्कंध की गुणवत्ता प्रतिकूल रूप से प्रभावित होने पर क्या ऐसे गोदाम संचालकों द्वारा आवेदन प्रस्तुत करते हुए, शपथ पत्र प्रस्तुत किया हैं ?
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlWarelitigation" runat="server" AutoPostBack="true"
                                        Width="100" Height="27px"
                                        OnSelectedIndexChanged="ddlWarelitigation_SelectedIndexChanged">
                                        <asp:ListItem>--Select--</asp:ListItem>
                                        <asp:ListItem Value="1" Selected="True">Yes</asp:ListItem>
                                        <asp:ListItem Value="0" >No</asp:ListItem>

                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>3.
                                </td>
                                <td style="height: 20px">क्या गोदाम क्षतिग्रस्त/विवादित/नयायालय प्रकरण प्रचलीत है ?
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlWareDamage" runat="server" AutoPostBack="true"
                                        Width="100" Height="27px"
                                        OnSelectedIndexChanged="ddlWareDamage_SelectedIndexChanged">
                                        <asp:ListItem>--Select--</asp:ListItem>
                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                        <asp:ListItem Value="0" Selected="True">No</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>4.
                                </td>
                                <td>क्या गोदाम के रिक्त कम्पार्टमेंट में निजी स्कंध भण्डारित है ?
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlWarePrivateDepositor" runat="server" AutoPostBack="true"
                                        Width="100" Height="27px"
                                        OnSelectedIndexChanged="ddlWarePrivateDepositor_SelectedIndexChanged">
                                        <asp:ListItem>--Select--</asp:ListItem>
                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                        <asp:ListItem Value="0" Selected="True">No</asp:ListItem>

                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>5.
                                </td>
                                <td style="height: 20px">क्या गोदाम 'ब्लेक लिस्टेड' है ?
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlBlackList" runat="server" AutoPostBack="true"
                                        Width="100" Height="27px"
                                        OnSelectedIndexChanged="ddlBlackList_SelectedIndexChanged">
                                        <asp:ListItem>--Select--</asp:ListItem>
                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                        <asp:ListItem Value="0" Selected="True">No</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>6.
                                </td>
                                <td>क्या गोदाम की भण्डारण क्षमता एक परिसर में न्यूनतम 500 मे.टन से कम है ?
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlcptless" runat="server" AutoPostBack="true"
                                        Width="100" Height="27px"
                                        OnSelectedIndexChanged="ddlcptless_SelectedIndexChanged">
                                        <asp:ListItem>--Select--</asp:ListItem>
                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                        <asp:ListItem Value="0" Selected="True">No</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>7.
                                </td>
                                <td style="width: 350px">क्या गोदाम वैज्ञानिक भण्डारण की दृष्टि से वायुसंचरण हेतु रोशनदान के साथ-साथ गोदाम की उंचाई 16 फिट से कम है ?
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlFacilities" runat="server" AutoPostBack="true"
                                        Width="100" Height="27px"
                                        OnSelectedIndexChanged="ddlFacilities_SelectedIndexChanged">
                                        <asp:ListItem>--Select--</asp:ListItem>
                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                        <asp:ListItem Value="0" Selected="True">No</asp:ListItem>

                                    </asp:DropDownList>

                                </td>
                                <td>8.
                                </td>
                                <td style="width: 350px">क्या निजी गोदाम, निजी फर्म, कंपनी, पार्टनरशिप फर्म, सहकारी संस्थाऐं अथवा अन्य संस्थाओं के उक्त गोदाम MPWLC के ऑनलाईन पोर्टल पर अनुबंध अवधि तक पंजीकृत है ?
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlofrinfo" runat="server" AutoPostBack="true"
                                        Width="100" Height="27px"
                                        OnSelectedIndexChanged="ddlofrinfo_SelectedIndexChanged">
                                        <asp:ListItem>--Select--</asp:ListItem>
                                        <asp:ListItem Value="1" Selected="True">Yes</asp:ListItem>
                                        <asp:ListItem Value="0">No</asp:ListItem>

                                    </asp:DropDownList>

                                </td>

                            </tr>


                            <tr>
                                <td>9.
                                </td>
                                <td style="width: 350px">क्या गोदाम में बारदाना (Gunny bells ) भंडारित  हैं ?
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlGunnybals" runat="server" AutoPostBack="true"
                                        Width="100" Height="27px"
                                        OnSelectedIndexChanged="ddlGunnybals_SelectedIndexChanged">
                                        <asp:ListItem>--Select--</asp:ListItem>
                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="0">No</asp:ListItem>

                                    </asp:DropDownList>

                                </td>
                                <td>10.
                                </td>
                                <td style="width: 350px">क्या गोदाम में खरीदी किया जा रहा स्कंध पूर्व से भंडारित हैं  ?
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlpurchaceCommodity" runat="server" AutoPostBack="true"
                                        Width="100" Height="27px"
                                        OnSelectedIndexChanged="ddlpurchaceCommodity_SelectedIndexChanged">
                                        <asp:ListItem>--Select--</asp:ListItem>
                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="0">No</asp:ListItem>

                                    </asp:DropDownList>

                                </td>

                            </tr>

                            <tr>
                                <td>11.
                                </td>
                                <td style="width: 350px">क्या गोदाम की निरीक्षण दिनांक को WDRA पंजीयन अथवा राज्य शासन का वेयरहाउस लाइसेंस उपलब्ध हैं या आवेदन किया गया  है?
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlIslicenseAvailable" runat="server" AutoPostBack="true"
                                        Width="100" Height="27px" OnSelectedIndexChanged="ddlIslicenseAvailable_SelectedIndexChanged" >


                                        <asp:ListItem>--Select--</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="1">Yes</asp:ListItem>
                                        <asp:ListItem Value="0">No</asp:ListItem>

                                    </asp:DropDownList>

                                </td>
                                <td>12.
                                </td>
                                <td style="width: 350px">क्या गोदाम सरफेसी (वित्तीय आस्तियो के प्रतिभूतिकरण और पुननिर्माण और सुरक्षाहित) एक्ट-2002 तहत बैंक का कब्ज़ा है?
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlIsCaptureByBank" runat="server" AutoPostBack="true"
                                        Width="100" Height="27px" OnSelectedIndexChanged="ddlIsCaptureByBank_SelectedIndexChanged"
                                    >
                                        <asp:ListItem>--Select--</asp:ListItem>
                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="0">No</asp:ListItem>

                                    </asp:DropDownList>

                                </td>

                            </tr>


                            <tr>
                                <td>13.
                                </td>
                                <td style="width: 350px">क्या जे.व्ही.एस. पालिसी में वर्णित कण्डिका 3 की समस्त अहार्ताएं पूर्ण करता हैं?
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlIsFullfillPointThreeInstruction" runat="server" AutoPostBack="true"
                                        Width="100" Height="27px" OnSelectedIndexChanged="ddlIsFullfillPointThreeInstruction_SelectedIndexChanged"
                                    >
                                        <asp:ListItem>--Select--</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="1">Yes</asp:ListItem>
                                        <asp:ListItem Value="0">No</asp:ListItem>

                                    </asp:DropDownList>

                                </td>
                                <td>14.
                                </td>
                                <td style="width: 350px">क्या गोदाम में NON FAQ /FCI द्वारा रिजेक्ट किया गेहूँ भंडारित है जिसका अपग्रडेशन एवं उठाव नहीं हुआ है?
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlIsNONFAQFCIReject" runat="server" AutoPostBack="true"
                                        Width="100" Height="27px" OnSelectedIndexChanged="ddlIsNONFAQFCIReject_SelectedIndexChanged"
                                    >
                                        <asp:ListItem>--Select--</asp:ListItem>
                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="0">No</asp:ListItem>

                                    </asp:DropDownList>

                                </td>

                            </tr>

                            <tr>

                                <td>15.
                                </td>
                                <td style="width: 350px">क्या गोदाम के ऊपर हाईटेंशन(HT) विद्युत लाइन है?
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlIsHT" runat="server" AutoPostBack="true"
                                        Width="100" Height="27px" OnSelectedIndexChanged="ddlIsHT_SelectedIndexChanged"
                                    >
                                        <asp:ListItem>--Select--</asp:ListItem>
                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="0">No</asp:ListItem>

                                    </asp:DropDownList>

                                </td>

                            </tr>
                            <tr>
                                <td></td>
                            </tr>
                            <tr>
                                <td colspan="6" style="color: Red;">
                                    <br />
                                    <p>

                                    </p>
                                </td>

                            </tr>
                        </table>





                        <table style="width: 100%" runat="server" visible="false">
                            <tr>
                                <td colspan="6" style="background-color: #66CCFF; height: 25px" align="center">
                                    <p style="font-size: 14px; color: Black; font-weight: bold">I)	Important aspect of Warehouse insurance_Detail </p>
                                </td>
                            </tr>
                            <tr>
                                <td>1.
                                </td>
                                <td style="height: 20px;">क्या गोदाम में रवि गोदाम का इंश्योरेंस  हैं ?
                                </td>
                                <td>

                                    <asp:DropDownList runat="server" ID="ddlinsurance" AutoPostBack="true" OnSelectedIndexChanged="ddlinsurance_SelectedIndexChanged" Style="height: 27px; width: 100px;">
                                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="YES" Value="YES"></asp:ListItem>
                                        <asp:ListItem selected="true" Text="NO" Value="NO"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>2.
                                </td>
                                <td style="height: 20px;">इंश्योरेंस दिनांक
                                </td>


                                <td>
                                    <asp:TextBox ID="txtSLDate" runat="server" CssClass="text"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender5" runat="server" Format="dd/MM/yyyy"
                                        TargetControlID="txtSLDate">
                                    </cc1:CalendarExtender>
                                </td>


                                <td>3.
                                </td>
                                <td>इंश्योरेंस की समाप्ति दिनांक
                                </td>


                                <td>
                                    <asp:TextBox ID="TextBox1" runat="server" CssClass="text"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender6" runat="server" Format="dd/MM/yyyy"
                                        TargetControlID="TextBox1">
                                    </cc1:CalendarExtender>
                                </td>
                            </tr>



                            <tr>
                                <td>4.
                                </td>
                                <td style="height: 20px;">इंश्योरेंस किये गए स्टॉक की मात्रा
                                </td>


                                <td>
                                    <asp:TextBox ID="txtInsuranceCapacity" class="text" onkeypress="return NumberOnly(event)" runat="server"></asp:TextBox>

                                </td>


                                <td>5.
                                </td>
                                <td>इंश्योरेंस किये गए स्टॉक की कीमत
                                </td>


                                <td>
                                    <asp:TextBox ID="txtvaluestk" class="text" onkeypress="return NumberOnly(event)" runat="server"></asp:TextBox>

                                </td>
                            </tr>

                            <tr>
                                <td>6.
                                </td>
                                <td style="height: 20px;">Premium / अधिमूल्य राशि
                                </td>

                                <td>
                                    <asp:TextBox ID="txtPremium" class="text" onkeypress="return NumberOnly(event)" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                        </table>







                        <table style="width: 100%">
                            <tr>
                                <td style="background-color: #66CCFF; height: 25px" align="center">
                                    <p style="font-size: 14px; color: Black; font-weight: bold">Required Material/Facilities provide by Godown Owner for JVS 2023-24</p>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <div id="Div1" runat="server">
                                        <table>
                                            <tr>
                                                <td> i)</td>
                                                <td> <p>
                                                    कार्यालयीन व्यवस्था हेतु फर्नीचरयुक्त कक्ष,शौचालय एवं विद्युत व्यवस्था?(हाँ/नहीं):</p></td>
                                                <td>
                                                    &nbsp;&nbsp
                                                    <asp:DropDownList ID="ddlFurniture_Office" runat="server" Height="25px" Width="100px">

                                                        <asp:ListItem Value="1">हाँ</asp:ListItem>
                                                        <asp:ListItem Value="2">नहीं</asp:ListItem>

                                                    </asp:DropDownList>
                                                    <br />

                                                </td>
                                            </tr>
                                            <tr>
                                                <td> ii)</td>
                                                <td> <p>
                                                    एक डाटा एन्ट्री ऑपरेटर जो WHMS साफ्टवेयर तथा अन्य साफ्टवेयर में कार्य कर सके । उपार्जन अवधि में पूर्णकालिक ऑपरेटर उपलब्ध कराना होगा ?(हाँ/नहीं):</p></td>
                                                <td class="auto-style1">

                                                    <asp:DropDownList ID="ddlDataEntry_Operator" runat="server" Height="25px" Width="100px">

                                                        <asp:ListItem Value="1">हाँ</asp:ListItem>
                                                        <asp:ListItem Value="2">नहीं</asp:ListItem>

                                                    </asp:DropDownList>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td>  iii)</td>
                                                <td> <p>
                                                    ऐसे गोदाम जिनकी भंडारण क्षमता 5000 मे.टन या अधिक है उन्हें गोदामों के संचालन एवं वैज्ञानिक भण्डारण हेतु एक अनुभवी तकनीकी कर्मचारी(शैक्षणिक योग्यता- बीएससी अथवा तकनीकी अनुभव न्यूनतम 02 वर्ष) उपलब्ध कराना होगा?(हाँ/नहीं):</p></td>
                                                <td class="auto-style1">
                                                    &nbsp;&nbsp
                                                    <asp:DropDownList ID="ddlTechnical_Manpower" runat="server" Height="25px" Width="100px">

                                                        <asp:ListItem Value="1">हाँ</asp:ListItem>
                                                        <asp:ListItem Value="2">नहीं</asp:ListItem>

                                                    </asp:DropDownList>

                                                </td>
                                            </tr>

                                            <tr>
                                                <td>  iv)</td>
                                                <td> <p>
                                                    उच्च गुणवत्ता के CCTV कैमरे (Night Vision सुविधा सहित) जिनकी मेमोरी न्यूनतम
                                                    1 माह की हो,  जिन्हें हमेशा चालू हालत में रखना अनिवार्य होगा, साथ ही रिकार्डिंग को
                                                    सुरक्षित रखेगा एवं प्रथम पक्षकार द्वारा चाहे जाने पर रिकार्डिंग उपलब्ध करायेगा
                                                    ?(हाँ/नहीं):</p> </td>
                                                <td class="auto-style1">
                                                    &nbsp;&nbsp
                                                    <asp:DropDownList ID="ddlCCTV_Camera" runat="server" Height="25px" Width="100px">

                                                        <asp:ListItem Value="1">हाँ</asp:ListItem>
                                                        <asp:ListItem Value="2">नहीं</asp:ListItem>

                                                    </asp:DropDownList>

                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                        </table>

                        <table style="width: 100%">

                            <tr>
                                <td style="background-color: #719CB6; height: 25px" align="center">
                                    <p style="font-size: 14px; color: white; font-weight: bold">गोदाम की भंडारण क्षमता अनुसार उपलब्ध करायी जाने वाली सामग्री/ संसाधन की मात्रा</p>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <div id="toexport" runat="server">
                                        <p>
                                            <asp:GridView ID="AgreeGrid" runat="server" AutoGenerateColumns="False" CellPadding="2"
                                                Width="940px" OnDataBound="OnDataBound"
                                                Font-Size="10pt" ShowFooter="true">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="क्र.">
                                                        <ItemTemplate>
                                                                         <%#Container.DataItemIndex+1%>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" Width="20px" />
                                                    </asp:TemplateField>

                                                    <asp:BoundField DataField="Facilities" HeaderText="विवरण" />
                                                    <asp:BoundField DataField="Till_2K_MT" HeaderText="2000 मे.टन तक" />
                                                    <asp:BoundField DataField="Till_5K_MT" HeaderText="5000 मे.टन तक" />
                                                    <asp:BoundField DataField="Till_10K_MT" HeaderText="10000 मे.टन तक" />
                                                    <asp:BoundField DataField="Till_20K_MT" HeaderText="20000 मे.टन तक" />
                                                                 <%--  <asp:BoundField DataField="Is_Available" HeaderText="क्षमता अनुसार सामग्री उपलब्ध है/नहीं है"/> --%>
                                                    <asp:TemplateField HeaderText="क्षमता अनुसार सामग्री उपलब्ध है/नहीं है">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlFacilities" runat="server" Width="90">

                                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                                                <asp:ListItem Value="2">No</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" Height="25px" Font-Size="10pt" />
                                                <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                                                    Height="30px" Font-Size="10pt" />
                                                <AlternatingRowStyle BackColor="#eeeeee" />
                                            </asp:GridView>
                                        </p>
                                    </div>
                                </td>
                            </tr>
                        </table>
                        <table style="width: 100%">
                            <tr>
                                <td colspan="2" style="background-color: #66CCFF; height: 25px" align="center">
                                    <p style="font-size: 14px; color: Black; font-weight: bold">J)	Declaration of Inspection Officer / Branch Manager </p>
                                </td>
                            </tr>
                            <tr>
                                <td></td>

                            </tr>

                            <tr>
                                <td>According To System FIT / UNFIT/PENDING &nbsp&nbsp<asp:TextBox ID="txtSystemFitunfit" Width="90px" runat="server" ReadOnly="true" class="text" type="text"></asp:TextBox></td>

                                <td style="margin-top: 0px; text-align: center">Remark For Recommendation :&nbsp
                                    <textarea id="txtRemark" maxlength="200" runat="server" cols="20" rows="2" name="radr" class="text" style="width: 250px;"></textarea>
                                </td>
                            </tr>

                            <tr>

                                <td></td>
                            </tr>


                            <tr>
                                <td colspan="2">
                                    <p>
                                        <asp:CheckBox ID="chkDeclaration" runat="server"></asp:CheckBox>
                                        I hereby declare and affirm that the information provided by us is true and correct to the best of our knowledge and found during the inspection by me.</p>
                                    <br />

                                    <p>Also this is to certify that after inspection based on the points mentioned in inspection report format, that (<asp:Label ID="lblDecWareName" runat="server"></asp:Label>), has been found
                                        <asp:Label ID="lblDecFitUnfit" runat="server"></asp:Label>
                                        for storage of commodity</p>
                                    <br />
                                    Date :-
                                    <asp:Label ID="lblDate" runat="server"></asp:Label>
                                </td>

                            </tr>

                            <tr>
                                <td colspan="2">
                                    <p style="color: Red;">
                                        नोट :-
                                        <br />
                                        1. यदि गोदाम अपात्र होता हे तो अपात्र के कारणों की फोटोग्राप्स रिकॉर्ड हेतु लिया जाएँ ।<br />
                            <%-- 2. यदि गोदाम अपात्र होता हे तो अपात्र के कारणों कै साथ वेयरहाउस औंनर को अग्वत कराए ।<br />--%>
                                        2. यदि गोदाम अपात्र होता हे तो अपात्र के कारणों कै साथ वेयरहाउस औंनर को सूचित करें एवं   अगवत कराए की 5 दिवस के भीतर उनके लॉगिन से अपील किया जाना संभव हे ।<br />
                                        3. गोदाम के निरीक्षण संबन्धित समस्त महत्वपूर्ण दस्तावेज़ आगामी निर्देश तक कार्यालय मे संग्रहीत करके रखे।

                                    </p>
                                </td>

                            </tr>


                            <tr>

                                <td align="center" colspan="2">

                                    <asp:Button ID="btnsubmit" runat="server" Text="Submit" class="submit"
                                        Width="140px" OnClick="btnsubmit_Click"></asp:Button>

                                </td>
                            </tr>
                            <tr align="center">
                                <td>
                                    <asp:Label ID="lblmsg" runat="server" visible="False" Text="Data Successfully Insert : "></asp:Label>

                                </td>
                            </tr>
                            <tr id="trlblHide" runat="server" visible="false">
                                <td colspan="2">
                                    <asp:Label ID="lblDistID" runat="server"></asp:Label><asp:Label ID="lblBranchID" runat="server"></asp:Label>
                                    <asp:Label ID="lblOfferID" runat="server"></asp:Label><asp:Label ID="lblGodownOfferID" runat="server"></asp:Label>
                                    <asp:Label ID="lblRegionID" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>


                        <asp:Label ID="Label5" runat="server" Font-Bold="true" Font-Size="Medium"></asp:Label>
                        <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="pnlofferpopup" TargetControlID="Label5"
                            BackgroundCssClass="modalBackground">
                        </cc1:ModalPopupExtender>
                        <asp:Panel ID="pnlofferpopup" runat="server" CssClass="modalPopup" Height="150px" Width="250px">
                            <div class="header">
                                <table style="width: 100%;">
                                    <tr>
                                        <td style="color: White; font-weight: bold" align="center">Confirmation Message</td>
                                        <td></td>
                                    </tr>

                                </table>
                            </div>
                            <div class="body">
                                <table align="center" style="width: 100%; border: #008CBA; border-style: solid; border-width: 0px;">
                                    <tr>
                                        <td style="height: 10px;"></td>
                                    </tr>

                                    <tr>
                                        <td align="center">Successfully Update Inspection
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 10px;"></td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:Button class="button button2" Width="100px" Height="30px" ID="Button3"
                                                runat="server" Text="Ok" align="Center" OnClick="Button3_Click" />

                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </asp:Panel>

            <%-- Start First Div Popup Data--%>
                        <asp:Label ID="Label2" runat="server"></asp:Label>
                        <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" CancelControlID="btncancelpwd" TargetControlID="Label2"
                            BackgroundCssClass="modalBackground" PopupControlID="pnllogin">
                        </cc1:ModalPopupExtender>
                        <asp:Panel ID="pnllogin" runat="server" ScrollBars="Vertical" class="modalpop" Height="600px" Width="80%">
                            <div class="header">
                                <table style="width: 100%;">
                                    <tr>
                                        <td align="center" valign="top" style="color: White; font-weight: bold; background-color: #008CBA; height: 25px; width: 400px; font-size: 16px; border-radius: 2px">खाद्य नागरिक आपूर्ति एवं उपभोक्ता संरक्षण, मध्यप्रदेश का पंजीयन/लायसेंस धारको का विवरण |</td>
                                        <td align="center" valign="top" style="color: White; font-weight: bold; background-color: #008CBA; height: 25px; width: 30px; font-size: 16px; border-radius: 2px">
                                            <asp:Button ID="btncancelpwd" runat="server" Text="Close" />
                                        </td>
                                    </tr>

                                </table>
                            </div>
                            <table id="modalpop" align="center" style="width: 100%;">
                                <tr>
                                    <td>
                                        <asp:GridView ID="GD_Paddy" runat="server" DataKeyNames="Whr_ID" AutoGenerateColumns="False" Width="100%" Height="300px" Font-Size="8pt"
                                            Font-Bold="true" BackColor="White" BorderColor="#008CBA" BorderStyle="Double" BorderWidth="1px" HeaderStyle-BorderColor="White"
                                            CellPadding="2" CellSpacing="2" ShowFooter="true">

                                            <Columns>
                                                <asp:TemplateField HeaderText="SNO." ItemStyle-Width="40px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="District" HeaderText="DISTRICT" ReadOnly="True" ItemStyle-HorizontalAlign="Left" />
                                                <asp:BoundField DataField="Whr_Name" HeaderText="WAREHOUSE NAME" ReadOnly="True" ItemStyle-HorizontalAlign="Left" />
                                                <asp:BoundField DataField="Whr_ID" HeaderText="LICENCE NO." ReadOnly="True" HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="Name_of_Owner" HeaderText="OWNER NAME" ReadOnly="True" HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Left" />
                                                <asp:BoundField DataField="Whr_Address" HeaderText="WAREHOUSE ADDRESS" ReadOnly="True" ItemStyle-HorizontalAlign="Left" />
                                                <asp:BoundField DataField="Total_capicity" HeaderText="CAPACITY" ReadOnly="True" HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Right" />
                                                <asp:BoundField DataField="ExpDate" HeaderText="EXP. DATE" ReadOnly="True" HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Center" />

                                            </Columns>
                                            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" Height="15px" Font-Size="8pt" />
                                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                            <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <HeaderStyle BackColor="#008CBA" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                                                Height="30px" Font-Size="12PX" />
                                            <AlternatingRowStyle BackColor="#eeeeee" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center"></td>
                                </tr>
                            </table>
                        </asp:Panel>


            <%--    <cc1:AnimationExtender ID="popupAnimation" runat="server" TargetControlID="Label2">
        <Animations>
                <OnClick>
         <Parallel AnimationTarget="pnllogin" Duration="0.4" Fps="10">
            <FadeIn />
          </Parallel>
       </OnClick>
        </Animations>
    </cc1:AnimationExtender> --%>

            <%-- End Of First Div Popup Data--%>

            <%-- Start Decond Div Popup Data--%>
                        <asp:Label ID="Label3" runat="server"></asp:Label>
                        <cc1:ModalPopupExtender ID="ModalPopupExtender3" runat="server" CancelControlID="Button1" TargetControlID="Label3"
                            BackgroundCssClass="modalBackground" PopupControlID="Panel1">
                        </cc1:ModalPopupExtender>
                        <asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical" class="modalpop" Height="600px" Width="80%">
                            <div class="header">
                                <table style="width: 100%;">
                                    <tr>
                                        <td align="center" valign="top" style="color: White; font-weight: bold; background-color: #008CBA; height: 25px; width: 400px; font-size: 16px; border-radius: 2px">मध्यप्रदेश के WDRA पंजीयन/लायसेंस धारको का विवरण (28/08/2018) तक  |</td>
                                        <td align="center" valign="top" style="color: White; font-weight: bold; background-color: #008CBA; height: 25px; width: 30px; font-size: 16px; border-radius: 2px">
                                            <asp:Button ID="Button1" runat="server" Text="Close" />
                                        </td>
                                    </tr>

                                </table>
                            </div>
                            <table id="Table1" align="center" style="width: 100%;">
                                <tr>
                                    <td>
                                        <asp:GridView ID="GridView1" runat="server" DataKeyNames="WHCode" AutoGenerateColumns="False" Width="100%" Height="300px" Font-Size="8pt"
                                            Font-Bold="true" BackColor="White" BorderColor="#008CBA" BorderStyle="Double" BorderWidth="1px" HeaderStyle-BorderColor="White"
                                            CellPadding="2" CellSpacing="2" ShowFooter="true">

                                            <Columns>
                                                <asp:TemplateField HeaderText="SNO." ItemStyle-Width="40px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:BoundField DataField="NameandAddress" HeaderText="WAREHOUSE NAME & ADDRESS" ReadOnly="True" ItemStyle-HorizontalAlign="Left" />
                                                <asp:BoundField DataField="WHCode" HeaderText="LICENCE NO." ReadOnly="True" HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="WarehousemanName" HeaderText="OWNER NAME" ReadOnly="True" HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Left" />
                                                <asp:BoundField DataField="Mobile" HeaderText="MOBILE NO" ReadOnly="True" HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Left" />
                                                <asp:BoundField DataField="Capacity" HeaderText="CAPACITY" ReadOnly="True" HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Right" />
                                                <asp:BoundField DataField="ExpDate" HeaderText="EXP. DATE" ReadOnly="True" HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Center" />

                                            </Columns>
                                            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" Height="15px" Font-Size="8pt" />
                                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                            <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <HeaderStyle BackColor="#008CBA" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                                                Height="30px" Font-Size="12PX" />
                                            <AlternatingRowStyle BackColor="#eeeeee" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">

                                    </td>

                                </tr>
                            </table>
                        </asp:Panel>


            <%--    <cc1:AnimationExtender ID="popupAnimation" runat="server" TargetControlID="Label2">
        <Animations>
                <OnClick>
         <Parallel AnimationTarget="pnllogin" Duration="0.4" Fps="10">
            <FadeIn />
          </Parallel>
       </OnClick>
        </Animations>
    </cc1:AnimationExtender> --%>

            <%-- End Of Second Div Popup Data--%>

                                </center>


                            </form>

                            <div style="background-image: url('../images/div_bg.png')">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="height: 20px;" colspan="5">
                                            <img id="Img2" src="../Images/line.png" height="30px" width="100%" alt="" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 20%" align="center">
                                            <a href="http://www.mp.nic.in/">
                                                <img src="../Images/NIC-logo.png" width="200px" height="50px" alt="" />
                                            </a>
                                        </td>
                                        <td style="width: 1%" align="center">
                                            <img id="Img1" src="../Images/Linevertical.png" style="width: 20px; height: 50px" alt="" />
                                        </td>
                                        <td style="color: Navy; font-size: 8pt; width: 58%;" align="center">
                                            <b>© 2018 &nbsp;National Informatics Centre.All Rights Reserved
                                                <br />
                                                Developed By : National Informatics Centre
                                                <br />
                                                Madhya Pradesh, Ministry of Communications and Information Technology</b>
                                        </td>
                                        <td style="width: 1%" align="center">
                                            <img id="Logo" src="/../Images/Linevertical.png" style="width: 20px; height: 50px" alt="" />
                                        </td>
                                        <td style="width: 20%" align="center">
                                            <table>
                                                <tr>
                                                    <td><a href="http://india.gov.in/">
                                                        <img src="../Images/natindialogo.png" width="100px" height="50px" alt="" />
                                                    </a>
                                                    </td>
                                                    <td>
                                                        <a href="http://www.digitalindia.gov.in/">
                                                            <img src="../Images/di.png" width="100px" height="50px" alt="" />
                                                        </a>
                                                    </td>
                                                </tr>
                                            </table>

                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div >
                    </div >
            </body >
</html >
