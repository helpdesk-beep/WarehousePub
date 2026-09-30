<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/StateMaster.master" AutoEventWireup="true" CodeFile="Welcome_Crop_Year_Wise_DashBoard.aspx.cs" Inherits="StatePages_Welcome_DashBoard" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../Inspections/Assets/css/bootstrap.css" rel="stylesheet" type="text/css">
    <link href="../Inspections/Assets/css/bootstrap-theme.css" rel="stylesheet" type="text/css">

    <!-- font awesome -->
    <link href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet">

    <link href="../Inspections/Assets/css/style.css" rel="stylesheet" type="text/css">
    <link href="../Inspections/Assets/css/custome.css" rel="stylesheet" type="text/css">
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
            background-color: #4CAF50;
            color: white;
            border: 2px solid #4CAF50;
        }

            .button1:hover {
                background-color: #4CAF50;
                color: white;
            }

        .button2 {
            background-color: #008CBA;
            color: white;
            border: 2px solid #008CBA;
        }

            .button2:hover {
                background-color: #008CBA;
                color: white;
            }

        .button3 {
            background-color: #f44336;
            color: white;
            border: 2px solid #f44336;
        }

            .button3:hover {
                background-color: #f44336;
                color: white;
            }

        .button6 {
            background-color: #E47D21;
            color: white;
            border: 2px solid #E47D21;
        }

            .button6:hover {
                background-color: #E47D21;
                color: white;
            }
    </style>

    <style type="text/css">
        #popupwin {
            position: fixed;
            top: 0;
            left: 0;
            width: 90%;
            height: 90%;
            background-color: #000;
            filter: alpha(opacity=65);
            -moz-opacity: 0.7;
            display: none;
            opacity: 0.7;
            z-index: 100;
        }

        .pop {
            margin: 0px auto;
            background: #FFFFFF;
            z-index: 100;
            padding: 10px;
            border-radius: 5px;
            box-shadow: 0 3px 5px #000;
        }
    </style>

    <%--<script language="JavaScript"> var message = 'Right Click is disabled';
function clickIE() { if (event.button == 2) { alert(message); return false; } }
function clickNS(e) {
if (document.layers || (document.getElementById && !document.all)) {
if (e.which == 2 || e.which == 3) { alert(message); return false; }
}
}
if (document.layers) { document.captureEvents(Event.MOUSEDOWN); document.onmousedown = clickNS; }
else if (document.all && !document.getElementById) { document.onmousedown = clickIE; }
document.oncontextmenu = new Function('alert(message);return false') </script>--%>

    <style type="text/css">
        .td:hover /* Highlight Current Cell*/ {
            box-shadow: 0 5px 10px #000;
            font-weight: bold;
        }
    </style>

    <fieldset style="height: 100%; width: 98%; border: 0px solid navy; margin-left: 5px; margin-right: 5px; background-color: #E7E7E7;">
        <center>
            <div>
                <table style="width: 98%;">
                    <tr>
                        <td align="center" style="border: #FFFFFF; border-style: solid; border-width: 2px; background-color: #5D6D7E; height: 25px;" colspan="3">
                            <span style="color: #FFFFFF; font-weight: bolder; font-size: 20px; font-family: Arial;">Rabi Procurement 2022-23 in (M.T.)</span>
                        </td>
                    </tr>
                    <td colspan="3" align="center">
                        <table width="95%">

                            <tr>
                                <td align="center" style="border: #FFFFFF; border-style: solid; border-width: 2px; background-color: #5D6D7E; height: 25px;" colspan="8">
                                    <span style="color: #FFFFFF; font-weight: bolder; font-size: 20px; font-family: Arial;">Wheat</span>
                                </td>
                            </tr>
                            <tr>
                                <td style="background-color: #fcba03; height: 160px; width: 32%; border-radius: 10px;" class="td" align="center">
                                    <a href="../Reports/States/Rpt_Procurement_Rabi2022_District.aspx" target="_blank">
                                        <asp:Label ID="lblPaddyAcceptanceQuantity" Font-Size="28px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server"></asp:Label>
                                        <br />
                                        <br />
                                        <asp:Label ID="Label4" Font-Size="16px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="Acceptance Quantity"></asp:Label><br />
                                    </a>
                                </td>

                                <td style="width: 5px"></td>
                                <td style="background-color: #fcba03; height: 160; width: 32%; border-radius: 10px;" class="td" align="center">
                                    <a href="../Reports/States/Rpt_Procurement_Rabi2022_District.aspx" target="_blank">
                                        <asp:Label ID="lblpaddyWHRQuantity" Font-Size="28px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server"></asp:Label>
                                        <br />
                                        <br />
                                        <asp:Label ID="Label9" Font-Size="16px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="WHR Quantity"></asp:Label>
                                    </a>
                                    <br />
                                </td>
                                <td style="width: 5px"></td>
                                <td style="background-color: #fcba03; height: 160px; width: 32%; border-radius: 10px;" class="td" align="center">
                                    <a href="../Reports/States/Rpt_Procurement_Rabi2022_District.aspx" target="_blank">
                                        <asp:Label ID="lblpaddytotalQty" Font-Size="28px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server"></asp:Label>
                                        <br />
                                        <br />

                                        <asp:Label ID="Label15" Font-Size="16px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="% Total Over Quantity"></asp:Label><br />
                                    </a>
                                </td>
                            </tr>


                            <tr>
                                <td align="center" style="border: #FFFFFF; border-style: solid; border-width: 2px; background-color: #5D6D7E; height: 25px;" colspan="8">
                                    <span style="color: #FFFFFF; font-weight: bolder; font-size: 20px; font-family: Arial;">Pulses</span>
                                </td>
                            </tr>
                            <tr>

                                <td style="background-color: #03c6fc; height: 160px; width: 32%; border-radius: 10px;" class="td" align="center">
                                    <a href="../Reports/States/Rpt_Procurement_KharifBajra2021_For_Deshboard.aspx" target="_blank">
                                        <asp:Label ID="lblBajraAcceptanceQuantity" Font-Size="28px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server"></asp:Label>
                                        <br />
                                        <br />

                                        <asp:Label ID="Label19" Font-Size="16px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="Acceptance Quantity"></asp:Label><br />
                                    </a>
                                </td>

                                <td style="width: 5px"></td>

                                <td style="background-color: #03c6fc; height: 160px; width: 32%; border-radius: 10px;" class="td" align="center">
                                    <a href="../Reports/States/Rpt_Procurement_KharifBajra2021_For_Deshboard.aspx" target="_blank">
                                        <asp:Label ID="lblBajraWHRQuantity" Font-Size="28px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server"></asp:Label>

                                        <br />
                                        <br />

                                        <asp:Label ID="Label22" Font-Size="16px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="WHR Quantity"></asp:Label><br />
                                    </a>
                                </td>
                                <td style="width: 5px"></td>
                                <td style="background-color: #03c6fc; height: 160px; width: 32%; border-radius: 10px;" class="td" align="center">
                                    <a href="../Reports/States/Rpt_Procurement_KharifBajra2021_For_Deshboard.aspx" target="_blank">
                                        <asp:Label ID="lblBajraTotalQty" Font-Size="28px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server"></asp:Label>
                                        <br />
                                        <br />

                                        <asp:Label ID="Label24" Font-Size="16px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="% Total Over Quantity"></asp:Label><br />
                                    </a>
                                </td>

                            </tr>
                        </table>
                    </td>
                </table>
                <table style="width: 98%;">
                    <tr>
                        <td align="center" style="border: #FFFFFF; border-style: solid; border-width: 2px; background-color: #5D6D7E; height: 25px;" colspan="6">
                            <span style="color: #FFFFFF; font-weight: bolder; font-size: 20px; font-family: Arial;">
                                <asp:LinkButton ID="LinkButton1" runat="server" ForeColor="White"
                                    OnClick="LinkButton1_Click">Available Stock Position in (Lakh M.T.)</asp:LinkButton>
                            </span>
                        </td>
                    </tr>

                    <tr>
                        <td style="height: 30px; font-size: 14px" colspan="3" align="center">Crop Year:-
                            <asp:DropDownList ID="ddlCropYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCropYear_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td style="height: 30px; font-size: 14px" colspan="3" align="center">Commodity:-
                            <asp:DropDownList ID="ddlcommodity" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlcommodity_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>

                    <tbody id="allcropyear" runat="server" visible="true">
                        <td colspan="6" align="center">
                            <table width="95%">
                                <%--<tr>
                                    <td style="background-color: darkorange; height: 160px; width: 32%; border-radius: 10px;" class="td" align="center">

                                        <asp:LinkButton ID="LinkButton33" runat="server" ValidationGroup="lik2"
                                            ForeColor="navy" Font-Bold="true"
                                            Font-Size="10pt" OnClick="LinkButton33_Click">
                                            <asp:Label ID="lblRecBags" Font-Size="28px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server"></asp:Label>
                                        </asp:LinkButton>
                                        <br />
                                        <br />
                                        <asp:Label ID="Label2" Font-Size="16px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="Receive Bags"></asp:Label><br />
                                    </td>

                                    <td style="width: 5px"></td>
                                    <td style="background-color: darkorange; height: 160; width: 32%; border-radius: 10px;" class="td" align="center">
                                        <asp:LinkButton ID="LinkButton1" runat="server" ValidationGroup="lik2"
                                            ForeColor="navy" Font-Bold="true"
                                            Font-Size="10pt" OnClick="LinkButton33_Click">
                                            <asp:Label ID="lblIssueBags" Font-Size="28px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server"></asp:Label>
                                        </asp:LinkButton>
                                        <br />
                                        <br />
                                        <asp:Label ID="Label6" Font-Size="16px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="Issue Bags"></asp:Label>
                                        <br />
                                    </td>
                                    <td style="width: 5px"></td>
                                    <td style="background-color: darkorange; height: 160px; width: 32%; border-radius: 10px;" class="td" align="center">
                                        <asp:LinkButton ID="LinkButton2" runat="server" ValidationGroup="lik2"
                                            ForeColor="navy" Font-Bold="true"
                                            Font-Size="10pt" OnClick="LinkButton33_Click">
                                            <asp:Label ID="lblAvlBags" Font-Size="28px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server"></asp:Label>
                                        </asp:LinkButton>
                                        <br />
                                        <br />

                                        <asp:Label ID="Label5" Font-Size="16px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="Available Bags"></asp:Label><br />
                                    </td>
                                </tr>--%>


                                <%--   <tr>
                                    <td style="height: 20px;"></td>
                                </tr>--%>

                                <tr>

                                    <td style="background-color: green; height: 160px; width: 32%; border-radius: 10px;" class="td" align="center">
                                        <asp:LinkButton ID="LinkButton3" runat="server" ValidationGroup="lik2"
                                            ForeColor="navy" Font-Bold="true"
                                            Font-Size="10pt" OnClick="LinkButton33_Click">
                                            <asp:Label ID="lblRecQty" Font-Size="28px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server"></asp:Label>
                                        </asp:LinkButton>
                                        <br />
                                        <br />

                                        <asp:Label ID="Label20" Font-Size="16px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="Receive Quantity"></asp:Label><br />
                                    </td>
                                    <td style="width: 5px"></td>

                                    <td style="background-color: green; height: 160px; width: 32%; border-radius: 10px;" class="td" align="center">
                                        <asp:LinkButton ID="LinkButton4" runat="server" ValidationGroup="lik2"
                                            ForeColor="navy" Font-Bold="true"
                                            Font-Size="10pt" OnClick="LinkButton33_Click">
                                            <asp:Label ID="lblIssueQty" Font-Size="28px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server"></asp:Label>
                                        </asp:LinkButton>
                                        <br />
                                        <br />

                                        <asp:Label ID="Label11" Font-Size="16px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="Issue Quantity"></asp:Label><br />
                                    </td>
                                    <td style="width: 5px"></td>
                                    <td style="background-color: green; height: 160px; width: 32%; border-radius: 10px;" class="td" align="center">
                                        <asp:LinkButton ID="LinkButton5" runat="server" ValidationGroup="lik2"
                                            ForeColor="navy" Font-Bold="true"
                                            Font-Size="10pt" OnClick="LinkButton33_Click">
                                            <asp:Label ID="lblAvlQty" Font-Size="28px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server"></asp:Label>
                                        </asp:LinkButton>
                                        <br />
                                        <br />

                                        <asp:Label ID="Label13" Font-Size="16px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="Available Quantity"></asp:Label><br />
                                    </td>

                                </tr>
                            </table>
                        </td>
                    </tbody>

                    <tbody id="cropyearwise" runat="server" visible="false">
                        <td colspan="6" align="center">
                            <table width="95%">

                                <tr>
                                    <td colspan="5" style="width: 5px"></td>
                                </tr>
                                <%--<tr>
                                    <td style="background-color: darkorange; height: 160px; width: 32%; border-radius: 10px;" class="td" align="center">
                                        <asp:LinkButton ID="LinkButton6" runat="server" ValidationGroup="lik2"
                                            ForeColor="navy" Font-Bold="true"
                                            Font-Size="10pt" OnClick="LinkButton33_Click">
                                            <asp:Label ID="lblcropRecBags" Font-Size="28px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server"></asp:Label>
                                        </asp:LinkButton>
                                        <br />
                                        <br />
                                        <asp:Label ID="Label3" Font-Size="16px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="Receive Bags"></asp:Label><br />
                                    </td>
                                    <td style="width: 5px"></td>
                                    <td style="background-color: darkorange; height: 160; width: 32%; border-radius: 10px;" class="td" align="center">
                                        <asp:LinkButton ID="LinkButton7" runat="server" ValidationGroup="lik2"
                                            ForeColor="navy" Font-Bold="true"
                                            Font-Size="10pt" OnClick="LinkButton33_Click">
                                            <asp:Label ID="lblcropIssueBags" Font-Size="28px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server"></asp:Label>
                                        </asp:LinkButton>
                                        <br />
                                        <br />
                                        <asp:Label ID="Label7" Font-Size="16px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="Issue Bags"></asp:Label><br />
                                    </td>
                                    <td style="width: 5px"></td>
                                    <td style="background-color: darkorange; height: 160px; width: 32%; border-radius: 10px;" class="td" align="center">
                                        <asp:LinkButton ID="LinkButton8" runat="server" ValidationGroup="lik2"
                                            ForeColor="navy" Font-Bold="true"
                                            Font-Size="10pt" OnClick="LinkButton33_Click">
                                            <asp:Label ID="lblcropAvlBags" Font-Size="28px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server"></asp:Label>
                                        </asp:LinkButton>
                                        <br />
                                        <br />

                                        <asp:Label ID="Label10" Font-Size="16px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="Available Bags"></asp:Label><br />
                                    </td>
                                </tr>--%>


                                <%--  <tr>
                                    <td style="height: 20px;"></td>
                                </tr>--%>

                                <%--<tr>

                                    <td style="background-color: green; height: 160px; width: 32%; border-radius: 10px;" class="td" align="center">
                                        <asp:LinkButton ID="LinkButton9" runat="server" ValidationGroup="lik2"
                                            ForeColor="navy" Font-Bold="true"
                                            Font-Size="10pt" OnClick="LinkButton33_Click">
                                            <asp:Label ID="lblCropRecQty" Font-Size="28px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server"></asp:Label>
                                        </asp:LinkButton>
                                        <br />
                                        <br />

                                        <asp:Label ID="Label14" Font-Size="16px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="Receive Quantity"></asp:Label><br />
                                    </td>
                                    <td style="width: 5px"></td>

                                    <td style="background-color: green; height: 160px; width: 32%; border-radius: 10px;" class="td" align="center">
                                        <asp:LinkButton ID="LinkButton10" runat="server" ValidationGroup="lik2"
                                            ForeColor="navy" Font-Bold="true"
                                            Font-Size="10pt" OnClick="LinkButton33_Click">
                                            <asp:Label ID="lblCropIssueQty" Font-Size="28px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server"></asp:Label>
                                        </asp:LinkButton>
                                        <br />
                                        <br />

                                        <asp:Label ID="Label16" Font-Size="16px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="Issue Quantity"></asp:Label><br />
                                    </td>
                                    <td style="width: 5px"></td>
                                    <td style="background-color: green; height: 160px; width: 32%; border-radius: 10px;" class="td" align="center">
                                        <asp:LinkButton ID="LinkButton11" runat="server" ValidationGroup="lik2"
                                            ForeColor="navy" Font-Bold="true"
                                            Font-Size="10pt" OnClick="LinkButton33_Click">
                                            <asp:Label ID="lblCropAvlQty" Font-Size="28px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server"></asp:Label>
                                        </asp:LinkButton>
                                        <br />
                                        <br />

                                        <asp:Label ID="Label18" Font-Size="16px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="Available Quantity"></asp:Label><br />
                                    </td>

                                </tr>--%>
                            </table>
                        </td>
                    </tbody>


                    <%-- <tbody id="Commoditywise" runat="server" visible="false">
                        <td colspan="6" align="center">
                            <table width="95%">

                                <tr>
                                    <td colspan="5" style="width: 5px"></td>
                                </tr>
                              
                                <tr>

                                    <td style="background-color: green; height: 160px; width: 32%; border-radius: 10px;" class="td" align="center">
                                        <asp:LinkButton ID="LinkButton1" runat="server" ValidationGroup="lik2"
                                            ForeColor="navy" Font-Bold="true"
                                            Font-Size="10pt" OnClick="LinkButton33_Click">
                                            <asp:Label ID="lblcmdRQ" Font-Size="28px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server"></asp:Label>
                                        </asp:LinkButton>
                                        <br />
                                        <br />

                                        <asp:Label ID="Label2" Font-Size="16px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="Receive Quantity"></asp:Label><br />
                                    </td>
                                    <td style="width: 5px"></td>

                                    <td style="background-color: green; height: 160px; width: 32%; border-radius: 10px;" class="td" align="center">
                                        <asp:LinkButton ID="LinkButton2" runat="server" ValidationGroup="lik2"
                                            ForeColor="navy" Font-Bold="true"
                                            Font-Size="10pt" OnClick="LinkButton33_Click">
                                            <asp:Label ID="lblcmdIQ" Font-Size="28px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server"></asp:Label>
                                        </asp:LinkButton>
                                        <br />
                                        <br />

                                        <asp:Label ID="Label5" Font-Size="16px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="Issue Quantity"></asp:Label><br />
                                    </td>
                                    <td style="width: 5px"></td>
                                    <td style="background-color: green; height: 160px; width: 32%; border-radius: 10px;" class="td" align="center">
                                        <asp:LinkButton ID="LinkButton6" runat="server" ValidationGroup="lik2"
                                            ForeColor="navy" Font-Bold="true"
                                            Font-Size="10pt" OnClick="LinkButton33_Click">
                                            <asp:Label ID="lblcmdAQ" Font-Size="28px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server"></asp:Label>
                                        </asp:LinkButton>
                                        <br />
                                        <br />

                                        <asp:Label ID="Label7" Font-Size="16px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="Available Quantity"></asp:Label><br />
                                    </td>

                                </tr>
                            </table>
                        </td>
                    </tbody>

                    <tbody id="BothCropCommodity" runat="server" visible="false">
                        <td colspan="6" align="center">
                            <table width="95%">

                                <tr>
                                    <td colspan="5" style="width: 5px"></td>
                                </tr>
                              
                                <tr>

                                    <td style="background-color: green; height: 160px; width: 32%; border-radius: 10px;" class="td" align="center">
                                        <asp:LinkButton ID="LinkButton7" runat="server" ValidationGroup="lik2"
                                            ForeColor="navy" Font-Bold="true"
                                            Font-Size="10pt" OnClick="LinkButton33_Click">
                                            <asp:Label ID="lblCropCMDRQ" Font-Size="28px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server"></asp:Label>
                                        </asp:LinkButton>
                                        <br />
                                        <br />

                                        <asp:Label ID="Label3" Font-Size="16px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="Receive Quantity"></asp:Label><br />
                                    </td>
                                    <td style="width: 5px"></td>

                                    <td style="background-color: green; height: 160px; width: 32%; border-radius: 10px;" class="td" align="center">
                                        <asp:LinkButton ID="LinkButton8" runat="server" ValidationGroup="lik2"
                                            ForeColor="navy" Font-Bold="true"
                                            Font-Size="10pt" OnClick="LinkButton33_Click">
                                            <asp:Label ID="lblCropCMDIQ" Font-Size="28px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server"></asp:Label>
                                        </asp:LinkButton>
                                        <br />
                                        <br />

                                        <asp:Label ID="Label10" Font-Size="16px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="Issue Quantity"></asp:Label><br />
                                    </td>
                                    <td style="width: 5px"></td>
                                    <td style="background-color: green; height: 160px; width: 32%; border-radius: 10px;" class="td" align="center">
                                        <asp:LinkButton ID="LinkButton12" runat="server" ValidationGroup="lik2"
                                            ForeColor="navy" Font-Bold="true"
                                            Font-Size="10pt" OnClick="LinkButton33_Click">
                                            <asp:Label ID="lblCropCMDAQ" Font-Size="28px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server"></asp:Label>
                                        </asp:LinkButton>
                                        <br />
                                        <br />

                                        <asp:Label ID="Label34" Font-Size="16px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="Available Quantity"></asp:Label><br />
                                    </td>

                                </tr>
                            </table>
                        </td>
                    </tbody>--%>
                </table>
                <%-- Godown Information Start--%>
                <table style="width: 98%;">
                    <tr>
                        <td align="center" style="border: #FFFFFF; border-style: solid; border-width: 2px; background-color: #5D6D7E; height: 25px;" colspan="3">
                            <span style="color: #FFFFFF; font-weight: bolder; font-size: 20px; font-family: Arial;">Number of Godown and Capacity</span>
                        </td>
                    </tr>
                    <%--  <tbody id="Tbody1" runat="server" visible="true">--%>
                    <td colspan="3" align="center">
                        <table width="95%">
                            <tr>
                                <td style="background-color: #cd552f; height: 160px; width: 32%; border-radius: 10px;" class="td" align="center">
                                    <asp:LinkButton ID="LinkButton2" runat="server" ValidationGroup="lik2"
                                            ForeColor="navy" Font-Bold="true"
                                            Font-Size="10pt" OnClick="LinkButton141_Click">
                                        <asp:Label ID="lbltg" Font-Size="28px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server"></asp:Label>
                                       <span style="Font-Size:28px; color:white;">/</span><asp:Label ID="lbltgcapacity" Font-Size="28px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server"></asp:Label>
                                        <br />
                                        <br />
                                        <asp:Label ID="Label2" Font-Size="16px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="No. of Total Godown and Scientific Capacity "></asp:Label><br />
                                   </asp:LinkButton>
                                </td>

                                <td style="width: 5px"></td>
                                <td style="background-color: #cd552f; height: 160; width: 32%; border-radius: 10px;" class="td" align="center">
                                   <asp:LinkButton ID="LinkButton6" runat="server" ValidationGroup="lik2"
                                            ForeColor="navy" Font-Bold="true"
                                            Font-Size="10pt" OnClick="LinkButton141_Click">
                                        <asp:Label ID="lblcoveredgdn" Font-Size="28px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server"></asp:Label>
                                        <span style="Font-Size:28px; color:white;">/</span><asp:Label ID="lblcoveredgdncapacity" Font-Size="28px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server"></asp:Label>
                                        <br />
                                        <br />
                                        <asp:Label ID="Label5" Font-Size="16px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="No. of Covered Godown and Scientific Capacity"></asp:Label>
                                    </asp:LinkButton>
                                    <br />
                                </td>
                                <td style="width: 5px"></td>
                                <td style="background-color: #cd552f; height: 160px; width: 32%; border-radius: 10px;" class="td" align="center">
                                   <asp:LinkButton ID="LinkButton7" runat="server" ValidationGroup="lik2"
                                            ForeColor="navy" Font-Bold="true"
                                            Font-Size="10pt" OnClick="LinkButton141_Click">
                                        <asp:Label ID="lblsilobaggdn" Font-Size="28px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server"></asp:Label>
                                        <span style="Font-Size:28px; color:white;">/</span>
                                        <asp:Label ID="lblsilobaggdncapacity" Font-Size="28px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server"></asp:Label>
                                        <br />
                                        <br />

                                        <asp:Label ID="Label7" Font-Size="16px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="No. of Silo Bags and Scientific Capacity"></asp:Label><br />
                                    </asp:LinkButton>
                                </td>
                            </tr>


                        </table>
                    </td>
                    <%-- </tbody>--%>
                    <tr>
                        <td align="center" style="border: #FFFFFF; border-style: solid; border-width: 2px; background-color: white; height: 25px;" colspan="8"></td>
                    </tr>

                    <td colspan="3" align="center">
                        <table width="95%">
                            <tr>
                                <td style="background-color: #cd552f; height: 160px; width: 32%; border-radius: 10px;" class="td" align="center">
                                   <asp:LinkButton ID="LinkButton8" runat="server" ValidationGroup="lik2"
                                            ForeColor="navy" Font-Bold="true"
                                            Font-Size="10pt" OnClick="LinkButton141_Click">
                                        <asp:Label ID="lblcapgdn" Font-Size="28px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server"></asp:Label>
                                        <span style="Font-Size:28px; color:white;">/</span><asp:Label ID="lblcapgdncapacity" Font-Size="28px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server"></asp:Label>
                                        <br />
                                        <br />
                                        <asp:Label ID="Label14" Font-Size="16px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="No. of CAP and Scientific Capacity"></asp:Label><br />
                                    </asp:LinkButton>
                                </td>

                                <td style="width: 5px"></td>
                                <td style="background-color: #cd552f; height: 160; width: 32%; border-radius: 10px;" class="td" align="center">
                                    <asp:LinkButton ID="LinkButton9" runat="server" ValidationGroup="lik2"
                                            ForeColor="navy" Font-Bold="true"
                                            Font-Size="10pt" OnClick="LinkButton141_Click">
                                        <asp:Label ID="lblstealsilogdn" Font-Size="28px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server"></asp:Label>
                                        <span style="Font-Size:28px; color:white;">/</span>
                                        <asp:Label ID="lblstealsilogdncapacity" Font-Size="28px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server"></asp:Label>
                                        <br />
                                        <br />
                                        <asp:Label ID="Label18" Font-Size="16px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="No. of Steal Silo and Scientific Capacity"></asp:Label>
                                    </asp:LinkButton>
                                    <br />
                                </td>
                                <td style="width: 70px"></td>
                                <%--<td style="background-color: #32a852; height: 160px; width: 32%; border-radius: 10px;" class="td" align="center">
                                    <a href="../Reports/States/Rpt_Region_Wise_Summary_of_Panding_Payment_For_Godown.aspx" target="_blank">
                                        <asp:Label ID="Label29" Font-Size="28px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server"></asp:Label>
                                        <br />
                                        <br />

                                        <asp:Label ID="Label34" Font-Size="16px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="Total Pending At MPWLC"></asp:Label><br />
                                    </a>
                                </td>--%>
                            </tr>


                        </table>
                    </td>
                    <%-- </tbody>--%>
                </table>
                <%--END--%>

                


                <table style="width: 98%;">
                    <tr>
                        <td align="center" style="border: #FFFFFF; border-style: solid; border-width: 2px; background-color: #5D6D7E; height: 25px;" colspan="3">
                            <span style="color: #FFFFFF; font-weight: bolder; font-size: 20px; font-family: Arial;">Online Billing Status From MPSCSC</span>
                        </td>
                    </tr>
                    <%--  <tbody id="Tbody1" runat="server" visible="true">--%>
                    <td colspan="3" align="center">
                        <table width="95%">
                            <tr>
                                <td style="background-color: #32a852; height: 160px; width: 32%; border-radius: 10px;" class="td" align="center">
                                    <a href="../Reports/States/Rpt_Get_Recived_and_Pending_Amount_From_Aug.aspx" target="_blank">
                                        <asp:Label ID="lblATSBMPSCSC" Font-Size="28px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server"></asp:Label>
                                        <br />
                                        <br />
                                        <asp:Label ID="Label12" Font-Size="16px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="Amount's of Total Submited Bill's to MPSCSC "></asp:Label><br />
                                    </a>
                                </td>

                                <td style="width: 5px"></td>
                                <td style="background-color: #32a852; height: 160; width: 32%; border-radius: 10px;" class="td" align="center">
                                    <a href="../Reports/States/Rpt_Get_Recived_and_Pending_Amount_From_Aug.aspx" target="_blank">
                                        <asp:Label ID="lblTARFMPSCSC" Font-Size="28px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server"></asp:Label>
                                        <br />
                                        <br />
                                        <asp:Label ID="Label25" Font-Size="16px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="Total Amount's Received From MPSCSC"></asp:Label>
                                    </a>
                                    <br />
                                </td>
                                <td style="width: 5px"></td>
                                <td style="background-color: #32a852; height: 160px; width: 32%; border-radius: 10px;" class="td" align="center">
                                    <a href="../Reports/States/Rpt_District_Wise_Pending_Bill_Details.aspx" target="_blank">
                                        <asp:Label ID="lblTPBAFMPSCSC" Font-Size="28px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server"></asp:Label>
                                        <br />
                                        <br />

                                        <asp:Label ID="Label32" Font-Size="16px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="Total Pending Bill's Amount's From MPSCSC"></asp:Label><br />
                                    </a>
                                </td>
                            </tr>


                        </table>
                    </td>
                    <%-- </tbody>--%>
                </table>


                <table style="width: 98%;">
                    <tr>
                        <td align="center" style="border: #FFFFFF; border-style: solid; border-width: 2px; background-color: #5D6D7E; height: 25px;" colspan="3">
                            <span style="color: #FFFFFF; font-weight: bolder; font-size: 20px; font-family: Arial;">Online Billing Status At MPWLC</span>
                        </td>
                    </tr>
                    <%--  <tbody id="Tbody1" runat="server" visible="true">--%>
                    <td colspan="3" align="center">
                        <table width="95%">
                            <tr>
                                <td style="background-color: #32a852; height: 160px; width: 32%; border-radius: 10px;" class="td" align="center">
                                    <a href="../Reports/States/Rpt_Region_Wise_Summary_of_Panding_Payment_For_Godown.aspx" target="_blank">
                                        <asp:Label ID="lblTAMPWLC" Font-Size="28px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server"></asp:Label>
                                        <br />
                                        <br />
                                        <asp:Label ID="Label21" Font-Size="16px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="Total Payable Amount's to Godown Owner's"></asp:Label><br />
                                    </a>
                                </td>

                                <td style="width: 5px"></td>
                                <td style="background-color: #32a852; height: 160; width: 32%; border-radius: 10px;" class="td" align="center">
                                    <a href="../Reports/States/Rpt_Region_Wise_Summary_of_Panding_Payment_For_Godown.aspx" target="_blank">
                                        <asp:Label ID="lblTACMPWLC" Font-Size="28px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server"></asp:Label>
                                        <br />
                                        <br />
                                        <asp:Label ID="Label33" Font-Size="16px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="Total Amount's Credit to Godown Owner's From MPWLC"></asp:Label>
                                    </a>
                                    <br />
                                </td>
                                <td style="width: 5px"></td>
                                <td style="background-color: #32a852; height: 160px; width: 32%; border-radius: 10px;" class="td" align="center">
                                    <a href="../Reports/States/Rpt_Region_Wise_Summary_of_Panding_Payment_For_Godown.aspx" target="_blank">
                                        <asp:Label ID="lblTPMPWLC" Font-Size="28px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server"></asp:Label>
                                        <br />
                                        <br />

                                        <asp:Label ID="Label35" Font-Size="16px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="Total Pending At MPWLC"></asp:Label><br />
                                    </a>
                                </td>
                            </tr>


                        </table>
                    </td>
                    <%-- </tbody>--%>
                </table>



                <%--<table style="width: 98%;">
                    <tr>
                        <td align="center" style="border: #FFFFFF; border-style: solid; border-width: 2px; background-color: #5D6D7E; height: 25px;" colspan="3">
                            <span style="color: #FFFFFF; font-weight: bolder; font-size: 20px; font-family: Arial;">Kharif Procurement 2021-22 in (M.T.)</span>
                        </td>
                    </tr>
                    <td colspan="3" align="center">
                        <table width="95%">

                            <tr>
                                <td align="center" style="border: #FFFFFF; border-style: solid; border-width: 2px; background-color: #5D6D7E; height: 25px;" colspan="8">
                                    <span style="color: #FFFFFF; font-weight: bolder; font-size: 20px; font-family: Arial;">Paddy</span>
                                </td>
                            </tr>
                            <tr>
                                <td style="background-color: #fcba03; height: 160px; width: 32%; border-radius: 10px;" class="td" align="center">
                                    <a href="../Reports/States/Rpt_Procurement_Kharif2021_For_Deshboard.aspx" target="_blank">
                                        <asp:Label ID="lblPaddyAcceptanceQuantity" Font-Size="28px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server"></asp:Label>
                                        <br />
                                        <br />
                                        <asp:Label ID="Label4" Font-Size="16px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="Acceptance Quantity"></asp:Label><br />
                                    </a>
                                </td>

                                <td style="width: 5px"></td>
                                <td style="background-color: #fcba03; height: 160; width: 32%; border-radius: 10px;" class="td" align="center">
                                    <a href="../Reports/States/Rpt_Procurement_Kharif2021_For_Deshboard.aspx" target="_blank">
                                        <asp:Label ID="lblpaddyWHRQuantity" Font-Size="28px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server"></asp:Label>
                                        <br />
                                        <br />
                                        <asp:Label ID="Label9" Font-Size="16px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="WHR Quantity"></asp:Label>
                                    </a>
                                    <br />
                                </td>
                                <td style="width: 5px"></td>
                                <td style="background-color: #fcba03; height: 160px; width: 32%; border-radius: 10px;" class="td" align="center">
                                    <a href="../Reports/States/Rpt_Procurement_Kharif2021_For_Deshboard.aspx" target="_blank">
                                        <asp:Label ID="lblpaddytotalQty" Font-Size="28px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server"></asp:Label>
                                        <br />
                                        <br />

                                        <asp:Label ID="Label15" Font-Size="16px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="% Total Over Quantity"></asp:Label><br />
                                    </a>
                                </td>
                            </tr>


                            <tr>
                                <td align="center" style="border: #FFFFFF; border-style: solid; border-width: 2px; background-color: #5D6D7E; height: 25px;" colspan="8">
                                    <span style="color: #FFFFFF; font-weight: bolder; font-size: 20px; font-family: Arial;">Bajra</span>
                                </td>
                            </tr>
                            <tr>

                                <td style="background-color: #03c6fc; height: 160px; width: 32%; border-radius: 10px;" class="td" align="center">
                                    <a href="../Reports/States/Rpt_Procurement_KharifBajra2021_For_Deshboard.aspx" target="_blank">
                                        <asp:Label ID="lblBajraAcceptanceQuantity" Font-Size="28px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server"></asp:Label>
                                        <br />
                                        <br />

                                        <asp:Label ID="Label19" Font-Size="16px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="Acceptance Quantity"></asp:Label><br />
                                    </a>
                                </td>

                                <td style="width: 5px"></td>

                                <td style="background-color: #03c6fc; height: 160px; width: 32%; border-radius: 10px;" class="td" align="center">
                                    <a href="../Reports/States/Rpt_Procurement_KharifBajra2021_For_Deshboard.aspx" target="_blank">
                                        <asp:Label ID="lblBajraWHRQuantity" Font-Size="28px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server"></asp:Label>

                                        <br />
                                        <br />

                                        <asp:Label ID="Label22" Font-Size="16px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="WHR Quantity"></asp:Label><br />
                                    </a>
                                </td>
                                <td style="width: 5px"></td>
                                <td style="background-color: #03c6fc; height: 160px; width: 32%; border-radius: 10px;" class="td" align="center">
                                    <a href="../Reports/States/Rpt_Procurement_KharifBajra2021_For_Deshboard.aspx" target="_blank">
                                        <asp:Label ID="lblBajraTotalQty" Font-Size="28px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server"></asp:Label>
                                        <br />
                                        <br />

                                        <asp:Label ID="Label24" Font-Size="16px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="% Total Over Quantity"></asp:Label><br />
                                    </a>
                                </td>

                            </tr>

                            <tr>
                                <td align="center" style="border: #FFFFFF; border-style: solid; border-width: 2px; background-color: #5D6D7E; height: 25px;" colspan="8">
                                    <span style="color: #FFFFFF; font-weight: bolder; font-size: 20px; font-family: Arial;">Jowar</span>
                                </td>
                            </tr>
                            <tr>

                                <td style="background-color: #ff0000; height: 160px; width: 32%; border-radius: 10px;" class="td" align="center">
                                    <a href="../Reports/States/Rpt_Procurement_KharifJwar2021_For_Deshboard.aspx" target="_blank">
                                        <asp:Label ID="lblJowarAcceptanceQuantity" Font-Size="28px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server"></asp:Label>
                                        <br />
                                        <br />

                                        <asp:Label ID="Label26" Font-Size="16px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="Acceptance Quantity"></asp:Label><br />
                                    </a>
                                </td>
                                <td style="width: 5px"></td>

                                <td style="background-color: #ff0000; height: 160px; width: 32%; border-radius: 10px;" class="td" align="center">
                                    <a href="../Reports/States/Rpt_Procurement_KharifJwar2021_For_Deshboard.aspx" target="_blank">
                                        <asp:Label ID="lblJowarWHRQuantity" Font-Size="28px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server"></asp:Label>
                                        <br />
                                        <br />

                                        <asp:Label ID="Label28" Font-Size="16px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="WHR Quantity"></asp:Label><br />
                                    </a>
                                </td>
                                <td style="width: 5px"></td>
                                <td style="background-color: #ff0000; height: 160px; width: 32%; border-radius: 10px;" class="td" align="center">
                                    <a href="../Reports/States/Rpt_Procurement_KharifJwar2021_For_Deshboard.aspx" target="_blank">
                                        <asp:Label ID="lblJowarTotalQty" Font-Size="28px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server"></asp:Label>
                                        <br />
                                        <br />

                                        <asp:Label ID="Label30" Font-Size="16px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="% Total Over Quantity"></asp:Label><br />
                                    </a>
                                </td>

                            </tr>
                        </table>
                    </td>
                </table>--%>

                

                <table style="width: 98%;">
                    <tr>
                        <td align="center" style="border: #FFFFFF; border-style: solid; border-width: 2px; background-color: #5D6D7E; height: 25px;" colspan="3">
                            <span style="color: #FFFFFF; font-weight: bolder; font-size: 20px; font-family: Arial;">Choice Filling For JVS 2022-23</span>
                        </td>
                    </tr>
                    <%--  <tbody id="Tbody1" runat="server" visible="true">--%>
                    <td colspan="3" align="center">
                        <table width="95%">
                            <tr>
                                <td style="background-color: #32a852; height: 160px; width: 32%; border-radius: 10px;" class="td" align="center">
                                    <a href="../StatePages/Rpt_shredi_selecttion.aspx" target="_blank">
                                        <asp:Label ID="lblTR" Font-Size="28px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server"></asp:Label>
                                        <br />
                                        <br />
                                        <asp:Label ID="Label8" Font-Size="16px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="Total Registration"></asp:Label><br />
                                    </a>
                                </td>

                                <td style="width: 5px"></td>
                                <td style="background-color: #32a852; height: 160; width: 32%; border-radius: 10px;" class="td" align="center">
                                    <a href="../StatePages/Rpt_shredi_selecttion.aspx" target="_blank">
                                        <asp:Label ID="lblTC" Font-Size="28px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server"></asp:Label>
                                        <br />
                                        <br />
                                        <asp:Label ID="Label17" Font-Size="16px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="Total Choices"></asp:Label>
                                    </a>
                                    <br />
                                </td>
                                <td style="width: 5px"></td>
                                <td style="background-color: #32a852; height: 160px; width: 32%; border-radius: 10px;" class="td" align="center">
                                    <a href="../StatePages/Rpt_shredi_selecttion.aspx" target="_blank">
                                        <asp:Label ID="lblRFC" Font-Size="28px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server"></asp:Label>
                                        <br />
                                        <br />

                                        <asp:Label ID="Label23" Font-Size="16px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="Ramining For Choices"></asp:Label><br />
                                    </a>
                                </td>
                            </tr>


                            <tr>
                                <td align="center" style="border: #FFFFFF; border-style: solid; border-width: 2px; background-color: white; height: 25px;" colspan="8"></td>
                            </tr>
                            <tr>

                                <td style="background-color: #03c6fc; height: 160px; width: 32%; border-radius: 10px;" class="td" align="center">
                                    <a href="../States/Rpt_shredi_selecttion.aspx" target="_blank">
                                        <asp:Label ID="lblFCA" Font-Size="28px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server"></asp:Label>
                                        <br />
                                        <br />

                                        <asp:Label ID="Label27" Font-Size="16px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="Choices for Category A"></asp:Label><br />
                                    </a>
                                </td>

                                <td style="width: 5px"></td>

                                <td style="background-color: #03c6fc; height: 160px; width: 32%; border-radius: 10px;" class="td" align="center">
                                    <a href="../States/Rpt_shredi_selecttion.aspx" target="_blank">
                                        <asp:Label ID="lblFCB" Font-Size="28px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server"></asp:Label>

                                        <br />
                                        <br />

                                        <asp:Label ID="Label31" Font-Size="16px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="Choices for Category B"></asp:Label><br />
                                    </a>
                                </td>
                                <td style="width: 5px"></td>
                            </tr>
                        </table>
                    </td>
                    <%-- </tbody>--%>
                </table>



                <div class="panel box-primary">
                    <table style="width: 98%;">
                        <tr>
                            <td align="center" style="border: #FFFFFF; border-style: solid; border-width: 2px; background-color: #5D6D7E; height: 25px;" colspan="3">
                                <span style="color: #FFFFFF; font-weight: bolder; font-size: 20px; font-family: Arial;">Online Inspection Details</span>
                            </td>
                        </tr>
                    </table>
                    <%-- <div class="panel-header">
                <div class="panel-title">
                    <h3>Online Inspection Reports</h3>
                </div>
                <hr />
            </div>--%>
                    <!-- /.box-header -->
                    <div class="panel-body">

                        <div class="row">
                            <div class="col-md-4">
                                <div class="mbox ">

                                    <span class="micon">
                                        <span style="font-size: xx-large;">
                                            <asp:Label ID="lbltiarm" runat="server"></asp:Label></span>
                                    </span>
                                    <h4>Total Inspections Alloted By RM Office</h4>


                                </div>
                            </div>

                            <div class="col-md-4">
                                <div class="mbox ">

                                    <span class="micon">
                                        <span style="font-size: xx-large;">
                                            <asp:Label ID="lblinspdone" runat="server"></asp:Label></span>
                                    </span>
                                    <h4>Total Inspection Done By Inspection Officer</h4>


                                </div>
                            </div>

                            <div class="col-md-4">
                                <div class="mbox ">

                                    <span class="micon">
                                        <span style="font-size: xx-large;">
                                            <asp:Label ID="lblpendinginsp" runat="server"></asp:Label></span>
                                    </span>
                                    <h4>Pending Inspection</h4>


                                </div>
                            </div>
                        </div>
                        <!-- /.row -->
                        <%-- <div class="panel-header">
                    <div class="panel-title">
                        <h3>Online Inspection Type Summary Reports</h3>
                    </div>
                    <hr />
                </div>--%>
                        <div class="row">
                            <div class="col-md-4">
                                <div class="mbox ">
                                    <span>
                                        <span style="font-size: x-large;">Total:-
                                    <asp:Label ID="lblgi" runat="server"></asp:Label></span>
                                        <br />
                                        <a href="/Warehouse/Inspections/Technical/Rpt_Inspection_Complited.aspx" target="_blank"><span style="color: green; font-size: x-large;">Complite:-
                                    <asp:Label ID="lblgic" runat="server"></asp:Label></span></a>
                                        <br />
                                        <a href="/Warehouse/Inspections/Technical/Rpt_Inspection_Complited_GIP.aspx" target="_blank"><span style="color: red; font-size: x-large;">Pending:- 
                                    <asp:Label ID="lblgip" runat="server"></asp:Label></span></a>
                                    </span>
                                    <h4>Total Complite General Inspections</h4>

                                </div>
                            </div>

                            <div class="col-md-4">
                                <div class="mbox ">
                                    <a href="#" target="_blank">
                                        <span>
                                            <span style="font-size: x-large;">Total:-
                                        <asp:Label ID="lblpvi" runat="server"></asp:Label></span>
                                            <br />
                                            <a href="/Warehouse/Inspections/Technical/Rpt_Inspection_Complited_PVC.aspx" target="_blank"><span style="color: green; font-size: x-large;">Complite:- 
                                        <asp:Label ID="lblpviC" runat="server"></asp:Label></span></a>
                                            <br />
                                            <a href="/Warehouse/Inspections/Technical/Rpt_Inspection_Complited_PVP.aspx" target="_blank"><span style="color: red; font-size: x-large;">Pending:-
                                        <asp:Label ID="lblpviP" runat="server"></asp:Label></span></a>
                                        </span>
                                        <h4>Total Complite Physical Verification</h4>

                                    </a>
                                </div>
                            </div>

                            <div class="col-md-4">
                                <div class="mbox ">
                                    <a href="#" target="_blank">
                                        <span>
                                            <span style="font-size: x-large;">Total:-
                                        <asp:Label ID="lblboth" runat="server"></asp:Label></span>
                                            <br />
                                            <a href="/Warehouse/Inspections/Technical/Rpt_Inspection_Complited_BothC.aspx" target="_blank"><span style="color: green; font-size: x-large;">Complite:- 
                                        <asp:Label ID="lblbothc" runat="server"></asp:Label></span></a>
                                            <br />
                                            <a href="/Warehouse/Inspections/Technical/Rpt_Inspection_Complited_BothP.aspx" target="_blank"><span style="color: red; font-size: x-large;">Pending:-
                                        <asp:Label ID="lblbothP" runat="server"></asp:Label></span></a>
                                        </span>
                                        <h4>Total Complite Both</h4>

                                    </a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </center>
    </fieldset>
    <asp:HiddenField ID="hdnDate" runat="server" Value="" />
</asp:Content>


