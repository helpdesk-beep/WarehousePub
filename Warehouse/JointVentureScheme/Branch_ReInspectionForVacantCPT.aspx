<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Branch_ReInspectionForVacantCPT.aspx.cs" Inherits="JointVentureScheme_Branch_ReInspectionForVacantCPT" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Re-Inspection For Vacant Capacity</title>
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

        <style type="text/css" >
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
            border: 2px solid #008CBA;
        }

            .button1:hover {
                background-color: #008CBA;
                color: white;
            }
    </style>


    <script type="text/javascript">
        function preventInput(evnt) {
            //Checked In IE9,Chrome,FireFox
            if (evnt.which != 9) evnt.preventDefault();
        }
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
    </style>
</head>
<body>

    <div id="bg" style="background-color: White">
        <div class="wrap">
            <img src="../images/CH.jpg" style="width: 100%" alt="" height="150" />

            <div>
                <form id="form1" runat="server">
                    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></cc1:ToolkitScriptManager>
                    <center>


                        <table style="width: 100%">
                            <%--                        <tr >
                            <td  colspan="4" style="background-color: #66CCFF"> 
                                <p style="font-size: medium; color: #008080; width: 953px;">&nbsp&nbsp&nbsp<asp:LinkButton 
                                        ID="link1" Text="Home" runat="server" PostBackUrl="~/JointVentureScheme/BranchHome.aspx"></asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Agreement For Inspected Godown&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="~/JointVentureScheme/Logins.aspx">Log out</asp:LinkButton></p>
                            </td>
                        </tr>--%>

                            <tr>
                                <td colspan="4" style="font-size: medium; width: 1000px;">
                                    <table style="width: 100%; height: 32px; font-size: medium;">
                                        <tr>
                                            <td style="background-color: #008CBA; width: 70PX;" align="center">
                                                <asp:LinkButton ID="LinkButton10" Text="Home" runat="server" PostBackUrl="~/JointVentureScheme/BranchHome.aspx" ForeColor="White"></asp:LinkButton>
                                            </td>
                                            <td colspan="2" style="background-color: #008CBA; font-size: medium; color: White; width: 100px" align="center">Welcome&nbsp;<asp:Label ID="lbluser" runat="server" ForeColor="White"></asp:Label></td>
                                            <td style="background-color: #008CBA; width: 70px;" align="center">
                                                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" ForeColor="White">Log out</asp:LinkButton></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="border-color: #008CBA; border-style: solid; border-width: 2px; background-color: White; width: 100%;" align="center">
                                    <p style="font-size: 14px; color: Black;">
                                        Re-Inspection for Vacant Capacity
                                    </p>
                                </td>
                            </tr>

                            <tr>
                                <td class="style7"></td>
                            </tr>
                            <tr>
                                <td align="center">Session &nbsp&nbsp&nbsp&nbsp&nbsp
                        <asp:DropDownList ID="ddl_session" runat="server" Width="150px" Height="25px" Enabled="true">
                            <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                            <asp:ListItem Selected="True" Value="Rab2026_27">Rabi 2026-27</asp:ListItem>
                            <asp:ListItem Value="Rab2025_26">Rabi 2025-26</asp:ListItem>
                            <asp:ListItem Value="Kharif2024_25">Kharif 2024-25</asp:ListItem>
                            <asp:ListItem Value="Rab2024_25">Rabi 2024-25</asp:ListItem>
                            <asp:ListItem Value="Rab2024_25">Rabi 2024-25</asp:ListItem>
                            <asp:ListItem Value="Kharif2023_24">Kharif 2023-24</asp:ListItem>
                            <asp:ListItem Value="Rab2023_24">Rabi 2023-24</asp:ListItem>
                            <asp:ListItem Value="JVS2023">Rabi 2022-23</asp:ListItem>
                            <asp:ListItem Value="JVS2022">JVS 2021-22</asp:ListItem>
                            <asp:ListItem Value="JVS2021">JVS 2020-21</asp:ListItem>
                            <asp:ListItem Value="Kharif_2022_23">Kharif_2022_23</asp:ListItem>
                            <%-- <asp:ListItem Value="Rabi1920">Rabi 2019-20</asp:ListItem>
                        <asp:ListItem Value="Kharif1819">Kharif 2018-19</asp:ListItem>
                         <asp:ListItem Value="Rabi1819">Rabi 2018-19</asp:ListItem>--%>
                        </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="style7"></td>
                            </tr>
                            <tr>
                                <td align="center" colspan="4">Registration ID : &nbsp;&nbsp;
                                    <asp:TextBox ID="txtSearch" runat="server" Style="width: 150px;"></asp:TextBox>
                                    &nbsp;&nbsp;
                                    <asp:Button ID="btnSearch" class="button button1" runat="server" Text="Search" OnClick="btnSearch_Click" Height="25px" Width="100px" /></td>
                            </tr>
                            <tr>
                                <td colspan="4" style="border-color: #008CBA; border-style: solid; border-width: 2px; background-color: #66CCFF; width: 100%;" align="center">
                                    <p style="font-size: 14px; color: Black;">
                                        Warehouse Inspection Deatils
                                    </p>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" id="GVGodowns" runat="server" visible="true" align="center" style="width: 90%;">
                                    <asp:GridView ID="gvGodown" runat="server" AutoGenerateColumns="False"
                                        EnableModelValidation="True" BackColor="White" BorderColor="Black"
                                        BorderStyle="Solid" BorderWidth="1px" CellPadding="3"
                                        OnSelectedIndexChanged="gvGodown_SelectedIndexChanged" Font-Size="12px">
                                        <AlternatingRowStyle Font-Size="12px" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="SNo." ItemStyle-Width="100">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                </ItemTemplate>

                                                <ItemStyle Width="25px"></ItemStyle>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Inspection_id" HeaderText="Inspection_id" />
                                            <asp:BoundField DataField="Godown_Offer_id" HeaderText="Godown_Offer_id" />
                                            <asp:BoundField DataField="Warehouse_name" HeaderText="Warehouse_name" />
                                            <asp:BoundField DataField="Registration_id" HeaderText="Registration_id" />
                                            <asp:BoundField DataField="Godownid" HeaderText="Godownid" />
                                            <asp:BoundField DataField="Godown_no" HeaderText="Godown_no" />
                                            <asp:BoundField DataField="InspDate" HeaderText="Inspection Date" />
                                            <asp:BoundField DataField="Stored_Commodities" HeaderText="Stored Commodities" />
                                            <asp:BoundField DataField="Stored_Com_Depositor" HeaderText="Depositor" ReadOnly="true" />
                                            <asp:BoundField DataField="Utilized_Capacity" HeaderText="Utilized Capacity" ReadOnly="true" />
                                            <asp:BoundField DataField="Vacant_Capacity" HeaderText="Vacant Capacity" ReadOnly="true" />
                                            <asp:BoundField DataField="isWareS_Comm_NGovt" HeaderText="isWareS_Comm_NGovt" ReadOnly="true" />
                                            <asp:BoundField DataField="G_OfferCapacity" HeaderText="Offer Capacity" ReadOnly="true" />
                                            <asp:CommandField SelectText="Update" HeaderText="Select" ShowSelectButton="True">
                                                <ControlStyle Font-Bold="True" ForeColor="Red" />
                                            </asp:CommandField>
                                        </Columns>
                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                        <HeaderStyle BackColor="#99CCFF" Font-Bold="True" ForeColor="Black" Height="30px" />
                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                        <RowStyle ForeColor="#000066" HorizontalAlign="Center"
                                            VerticalAlign="Middle" />
                                        <SelectedRowStyle BackColor="#669999" ForeColor="White" />
                                    </asp:GridView>
                                    <br />

                                </td>
                            </tr>

                            <tr id="TRHide" visible="false" runat="server">
                                <td>
                                    <table style="width: 100%">

                                        <tr>
                                            <td>पुनः निरीक्षण दिनांक :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtInspDate" runat="server" class="text" type="text" onkeydown="javascript:preventInput(event);" onpaste="return false;"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                                    TargetControlID="txtInspDate">
                                                </cc1:CalendarExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 200px">वर्तमान में गोदाम में संग्रहित स्कंधो का नाम :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCommodityName" runat="server" class="text" type="text"></asp:TextBox>
                                            </td>
                                            <td style="height: 20px">गोदाम में संग्रहित स्कंध जमाकर्ता का नाम (MPSCSC/Markfed/Nafed/Private/Other)
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDepositorName" runat="server" class="text" type="text"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>

                                            <td>वर्तमान में गोदाम में संग्रहित स्कंध की क्षमता (मे.टन)  :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCurrentStoredComm" runat="server" class="text" type="text" Text="0"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtCurrentStoredComm"
                                                    ValidChars="0123456789.">
                                                </cc1:FilteredTextBoxExtender>
                                            </td>
                                            <td style="height: 20px">गोदाम की कुल रिक्त क्षमता (मे.टन) (Total Vacant Capacity) :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtvacantcpt" runat="server" class="text" type="text"></asp:TextBox>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 10px"></td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                <center>
                                                    <asp:Button ID="btnUpdate" class="button button1" runat="server" Text="Update"
                                                        Height="25px" Width="100px" OnClick="btnUpdate_Click"></asp:Button>

                                                </center>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <p style="color: Red;">
                                        नोट :-
                                        <br />
                                        1.गोदाम की कुल रिक्त क्षमता में , पुर्व निरीक्षण में दुर्ज की गई क्षमता और पुर्व निरीक्षण क़े बाद  रिक्त क्षमता दोनों को जोड़ कर दुर्ज करें  ।<br />
                                        2.यदि कुल  रिक्त  क्षमता क़े अनुसार एग्रीमेंट किया जा चुका है  तो ऑनलाइन एग्रीमेंट भि अपडेट  करें  ।<br />

                                    </p>
                                </td>

                            </tr>


                        </table>
                        <div id="divOwner" runat="server">


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

                        </div>
                    </center>
                </form>
            </div>
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
        </div>
    </div>
</body>
</html>
