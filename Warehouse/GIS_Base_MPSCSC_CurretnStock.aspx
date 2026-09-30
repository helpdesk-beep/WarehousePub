<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GIS_Base_MPSCSC_CurretnStock.aspx.cs"
    Inherits="WPMS2017_State_ReportState_frm_rpt_StateWise_details_MapFReg" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Current Stock</title>
    
    
</head>
<body>


    <form id="form1" runat="server">
        <div class="box round first">
            <div class="block">
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 1393px; height: 30px; background-color: #4f637c" align="center">
                            <table style="width: 606px;" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="width: 60px; height: 25px; font-size: 11px;">
                                    </td>
<%--                                    <td style="width: 200px; height: 30px;" align="left">
                                        <asp:DropDownList ID="ddlmineral" runat="server" Width="150px" OnSelectedIndexChanged="ddlmineral_SelectedIndexChanged" AutoPostBack="true" Visible="False">
                                         <asp:ListItem Value="1" Text="खरीदी मात्रा " ></asp:ListItem>
                                    <asp:ListItem Value="2" Text="विक्रेता किसान " ></asp:ListItem>
                                    <asp:ListItem Value="3" Text="पंजीकृत किसान " ></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>--%>
                                    <td style="width: 100px; height: 30px; color:White">
                                   <%-- <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" Width="201px" >पिछले पृष्ठ पर जाये</asp:LinkButton>--%>
                                        <%--<asp:Button ID="Button1" runat="server" Text="देखें" OnClick="Button1_Click" />&nbsp;--%>
                                    <asp:Label ID="Label2" runat="server" Text="GIS Base MPSCSC Current Stock Report"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 1393px">
                        <asp:Panel ID="pnlqty" runat ="server" Width="90%" Height="27px">
                            <table style="border: 1px solid #000000">
                                <tr>
                                    <td style="border-width: 1px; border-color: #000000; width: 50px; height: 15px; background-color: #F75353;
                                        border-right-style: solid; border-bottom-style: solid;">
                                    </td>
                                    <td style="border-width: 1px; border-color: #000000; width: 140px; height: 15px;
                                        border-bottom-style: ridge; font-size: 14px" align="left">
                                        <asp:Label ID="lblRed" runat="server" Text="100000 M.T से कम "></asp:Label>
                                    </td>
                                    <td style="border-width: 1px; border-color: #000000; width: 50px; height: 15px; background-color: #FCFF00;
                                        border-right-style: solid; border-bottom-style: solid;">
                                    </td>
                                    <td style="border-width: 1px; border-color: #000000; width: 169px; height: 15px; border-bottom-style: solid;
                                        font-size: 14px;" align="left">
                                        <asp:Label ID="lblYello" runat="server" Text="100000 M.T.से 200000 M.T. तक" Width="254px"></asp:Label>
                                    </td>
                                    <td style="border-width: 1px; border-color: #000000; width: 50px; height: 15px; background-color: #8BF980;
                                        border-right-style: solid; border-bottom-style: solid;">
                                    </td>
                                    <td style="border-width: 1px; border-color: #000000; width: 192px; height: 15px;
                                        border-bottom-style: solid; font-size: 14px" align="center">
                                        <asp:Label ID="lblGreen" runat="server" Text="200000 M.T. से अधिक"></asp:Label></td>

                                </tr>
                            </table> </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 1393px; height: 700px; background-color: White;">
                            <div id="divPrint" style="z-index: 99; position: absolute; top: 30px; left: -49px;"
                                runat="server" visible="false">
                                <div id="divPrint1">
                                    <div style="z-index: 100; position: absolute; top: 102px; left: 272px;">
                                        <img src="images/MapImg/mppp.png" runat="server" id="img1" />
                                    </div>
                                    <div style="z-index: 102; position: absolute; top: 523px; left: 327px; height: 107px;
                                        width: 121px;" id="divdharL" visible="false" runat="server">
                                        <img src="images/MapImg/dharL.png" />
                                    </div>
                                    <div style="z-index: 102; position: absolute; top: 523px; left: 327px; height: 107px;
                                        width: 121px;" id="divdharM" visible="false" runat="server">
                                        <img src="images/MapImg/dharM.png" />
                                    </div>
                                    <div style="z-index: 102; position: absolute; top: 524px; left: 327px; height: 107px;
                                        width: 121px;" id="divdharH" visible="false" runat="server">
                                        <img src="images/MapImg/dharH.png" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; top: 589px; left: 566px; height: 107px;
                                        width: 121px;" id="divhardaL" visible="false" runat="server">
                                        <img src="images/MapImg/hardaL.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; top: 589px; left: 566px; height: 107px;
                                        width: 121px;" id="divhardaM" visible="false" runat="server">
                                        <img src="images/MapImg/hardaM.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; top: 589px; left: 565px; height: 107px;
                                        width: 121px;" id="divhardaH" visible="false" runat="server">
                                        <img src="images/MapImg/hardaH.gif" />
                                    </div>
                                    <%-- Sagar--%>
                                    <div style="z-index: 104; position: absolute; width: 121px; top: 391px; left: 622px;"
                                        id="divVidhishaL" visible="false" runat="server">
                                        <img src="images/MapImg/VidhishaL.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 390px; left: 621px;"
                                        id="divVidhishaM" visible="false" runat="server">
                                        <img src="images/MapImg/VidhishaM.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 390px; left: 624px;"
                                        id="divVidhishaH" visible="false" runat="server">
                                        <img src="images/MapImg/VidhishaH.gif" />
                                    </div>
                                    <%-- Gwalior--%>
                                    <div style="z-index: 104; position: absolute; width: 121px; top: 510px; left: 311px;"
                                        id="divJhabuaL" visible="false" runat="server">
                                        <img src="images/MapImg/JhabuaL.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 510px; left: 311px;"
                                        id="divJhabuaM" visible="false" runat="server">
                                        <img src="images/MapImg/JhabuaM.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 510px; left: 311px;"
                                        id="divJhabuaH" visible="false" runat="server">
                                        <img src="images/MapImg/JhabuaH.gif" />
                                    </div>
                                    <%-- Morena--%>
                                    <div style="z-index: 104; position: absolute; width: 121px; top: 293px; left: 1007px;"
                                        id="divRewaL" visible="false" runat="server">
                                        <img src="images/MapImg/RewaL.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 293px; left: 1007px;"
                                        id="divRewaM" visible="false" runat="server">
                                        <img src="images/MapImg/RewaM.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 293px; left: 1008px;"
                                        id="divRewaH" visible="false" runat="server">
                                        <img src="images/MapImg/RewaH.gif" />
                                    </div>
                                    <%--Bhind--%>
                                    <div style="z-index: 104; position: absolute; width: 121px; top: 380px; left: 704px;"
                                        id="divSagarL" visible="false" runat="server">
                                        <img src="images/MapImg/SagarL.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 380px; left: 704px;"
                                        id="divSagarM" visible="false" runat="server">
                                        <img src="images/MapImg/SagarM.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 380px; left: 704px;"
                                        id="divSagarH" visible="false" runat="server">
                                        <img src="images/MapImg/SagarH.gif" />
                                    </div>
                                    <%--Chhatarpur--%>
                                    <div style="z-index: 104; position: absolute; width: 121px; top: 166px; left: 661px;"
                                        id="divGwaliorL" visible="false" runat="server">
                                        <img src="images/MapImg/GwaliorL.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 167px; left: 660px;"
                                        id="divGwaliorM" visible="false" runat="server">
                                        <img src="images/MapImg/GwaliorM.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 166px; left: 660px;"
                                        id="divGwaliorH" visible="false" runat="server">
                                        <img src="images/MapImg/GwaliorH.gif" />
                                    </div>
                                    <%--Rajgarh--%>
                                    <div style="z-index: 104; position: absolute; width: 121px; top: 108px; left: 606px;"
                                        id="divMorenaL" visible="false" runat="server">
                                        <img src="images/MapImg/MorenaL.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 108px; left: 606px;"
                                        id="divMorenaM" visible="false" runat="server">
                                        <img src="images/MapImg/MorenaM.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 108px; left: 606px;"
                                        id="divMorenaH" visible="false" runat="server">
                                        <img src="images/MapImg/MorenaH.gif" />
                                    </div>
                                    <%--Indore--%>
                                    <div style="z-index: 104; position: absolute; width: 121px; top: 116px; left: 716px;"
                                        id="divBhindL" visible="false" runat="server">
                                        <img src="images/MapImg/BhindL.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 116px; left: 716px;"
                                        id="divBhindM" visible="false" runat="server">
                                        <img src="images/MapImg/BhindM.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 116px; left: 716px;"
                                        id="divBhindH" visible="false" runat="server">
                                        <img src="images/MapImg/BhindH.gif" />
                                    </div>
                                    <%--Ratlam--%>
                                    <div style="z-index: 104; position: absolute; width: 121px; top: 269px; left: 797px;"
                                        id="divChhatarpurL" visible="false" runat="server">
                                        <img src="images/MapImg/ChhatarpurL.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 270px; left: 797px;"
                                        id="divChhatarpurM" visible="false" runat="server">
                                        <img src="images/MapImg/ChhatarpurM.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 270px; left: 797px;"
                                        id="divChhatarpurH" visible="false" runat="server">
                                        <img src="images/MapImg/ChhatarpurH.gif" />
                                    </div>
                                    <%--Dewas--%>
                                    <div style="z-index: 104; position: absolute; width: 121px; top: 398px; left: 508px;"
                                        id="divRajgarhL" visible="false" runat="server">
                                        <img src="images/MapImg/RajgarhL.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 397px; left: 508px;"
                                        id="divRajgarhM" visible="false" runat="server">
                                        <img src="images/MapImg/RajgarhM.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 399px; left: 508px;"
                                        id="divRajgarhH" visible="false" runat="server">
                                        <img src="images/MapImg/RajgarhH.gif" />
                                    </div>
                                    <%--Hoshangabad--%>
                                    <div style="z-index: 104; position: absolute; width: 121px; top: 530px; left: 427px;"
                                        id="divIndoreL" visible="false" runat="server">
                                        <img src="images/MapImg/IndoreL.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 529px; left: 427px;"
                                        id="divIndoreM" visible="false" runat="server">
                                        <img src="images/MapImg/IndoreM.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 529px; left: 426px;"
                                        id="divIndoreH" visible="false" runat="server">
                                        <img src="images/MapImg/IndoreH.gif" />
                                    </div>
                                    <%--Bhopal--%>
                                    <div style="z-index: 104; position: absolute; width: 121px; top: 437px; left: 334px;"
                                        id="divRatlamL" visible="false" runat="server">
                                        <img src="images/MapImg/RatlamL.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 437px; left: 334px;"
                                        id="divRatlamM" visible="false" runat="server">
                                        <img src="images/MapImg/RatlamM.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 437px; left: 333px;"
                                        id="divRatlamH" visible="false" runat="server">
                                        <img src="images/MapImg/RatlamH.gif" />
                                    </div>
                                    <%--Raisen--%>
                                    <div style="z-index: 104; position: absolute; width: 121px; top: 504px; left: 475px;"
                                        id="divDewasL" visible="false" runat="server">
                                        <img src="images/MapImg/DewasL.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 504px; left: 475px;"
                                        id="divDewasM" visible="false" runat="server">
                                        <img src="images/MapImg/DewasM.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 504px; left: 476px;"
                                        id="divDewasH" visible="false" runat="server">
                                        <img src="images/MapImg/DewasH.gif" />
                                    </div>
                                    <%--Sehore--%>
                                    <div style="z-index: 104; position: absolute; width: 121px; top: 544px; left: 612px;"
                                        id="divHoshL" visible="false" runat="server">
                                        <img src="images/MapImg/HoshL.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 544px; left: 612px;"
                                        id="divHoshM" visible="false" runat="server">
                                        <img src="images/MapImg/HoshM.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 544px; left: 612px;"
                                        id="divHoshH" visible="false" runat="server">
                                        <img src="images/MapImg/HoshH.gif" />
                                    </div>
                                    <%--Shajapur--%>
                                    <div style="z-index: 104; position: absolute; width: 121px; top: 448px; left: 609px;"
                                        id="divBhopalL" visible="false" runat="server">
                                        <img src="images/MapImg/BhopalL.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 447px; left: 608px;"
                                        id="divBhopalM" visible="false" runat="server">
                                        <img src="images/MapImg/BhopalM.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 448px; left: 608px;"
                                        id="divBhopalH" visible="false" runat="server">
                                        <img src="images/MapImg/BhopalH.gif" />
                                    </div>
                                    <%--Ujjain--%>
                                    <div style="z-index: 104; position: absolute; width: 121px; top: 458px; left: 628px;"
                                        id="divRaisenL" visible="false" runat="server">
                                        <img src="images/MapImg/RaisenL.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 458px; left: 628px;"
                                        id="divRaisenM" visible="false" runat="server">
                                        <img src="images/MapImg/RaisenM.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 458px; left: 628px;"
                                        id="divRaisenH" visible="false" runat="server">
                                        <img src="images/MapImg/RaisenH.gif" />
                                    </div>
                                    <%--Agar--%>
                                    <div style="z-index: 104; position: absolute; width: 121px; top: 467px; left: 530px;"
                                        id="divSehoreL" visible="false" runat="server">
                                        <img src="images/MapImg/SehoreL.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 467px; left: 531px;"
                                        id="divSehoreM" visible="false" runat="server">
                                        <img src="images/MapImg/SehoreM.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 467px; left: 531px;"
                                        id="divSehoreH" visible="false" runat="server">
                                        <img src="images/MapImg/SehoreH.gif" />
                                    </div>
                                    <%--Betul--%>
                                    <div style="z-index: 104; position: absolute; width: 121px; top: 460px; left: 498px;"
                                        id="divShajapurL" visible="false" runat="server">
                                        <img src="images/MapImg/ShajapurL.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 460px; left: 498px;"
                                        id="divShajapurM" visible="false" runat="server">
                                        <img src="images/MapImg/ShajapurM.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 460px; left: 498px;"
                                        id="divShajapurH" visible="false" runat="server">
                                        <img src="images/MapImg/ShajapurH.gif" />
                                    </div>
                                    <%--Neemuch--%>
                                    <div style="z-index: 104; position: absolute; width: 121px; top: 455px; left: 397px;"
                                        id="divUjjainL" visible="false" runat="server">
                                        <img src="images/MapImg/UjjainL.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 455px; left: 397px;"
                                        id="divUjjainM" visible="false" runat="server">
                                        <img src="images/MapImg/UjjainM.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 455px; left: 397px;"
                                        id="divUjjainH" visible="false" runat="server">
                                        <img src="images/MapImg/UjjainH.gif" />
                                    </div>
                                    <%--Datia--%>
                                    <div style="z-index: 104; position: absolute; width: 121px; top: 392px; left: 456px;"
                                        id="divAgarL" visible="false" runat="server">
                                        <img src="images/MapImg/AgarL.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 392px; left: 456px;"
                                        id="divAgarM" visible="false" runat="server">
                                        <img src="images/MapImg/AgarM.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 392px; left: 456px;"
                                        id="divAgarH" visible="false" runat="server">
                                        <img src="images/MapImg/AgarH.gif" />
                                    </div>
                                    <%--Panna--%>
                                    <div style="z-index: 104; position: absolute; width: 121px; top: 609px; left: 596px;"
                                        id="divBetulL" visible="false" runat="server">
                                        <img src="images/MapImg/BetulL.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 609px; left: 596px;"
                                        id="divBetulM" visible="false" runat="server">
                                        <img src="images/MapImg/BetulM.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 609px; left: 596px;"
                                        id="divBetulH" visible="false" runat="server">
                                        <img src="images/MapImg/BetulH.gif" />
                                    </div>
                                    <%--narp--%>
                                    <div style="z-index: 104; position: absolute; width: 121px; top: 308px; left: 356px;"
                                        id="divNeemuchL" visible="false" runat="server">
                                        <img src="images/MapImg/NeenuchL.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 308px; left: 356px;"
                                        id="divNeemuchM" visible="false" runat="server">
                                        <img src="images/MapImg/NeemuchM.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 308px; left: 355px;"
                                        id="divNeemuchH" visible="false" runat="server">
                                        <img src="images/MapImg/NeemuchH.gif" />
                                    </div>
                                    <%--Jabalpur--%>
                                    <div style="z-index: 104; position: absolute; width: 121px; top: 173px; left: 720px;"
                                        id="divDatiaL" visible="false" runat="server">
                                        <img src="images/MapImg/DatiaL.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 173px; left: 722px;"
                                        id="divDatiaM" visible="false" runat="server">
                                        <img src="images/MapImg/DatiaM.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 173px; left: 721px;"
                                        id="divDatiaH" visible="false" runat="server">
                                        <img src="images/MapImg/DatiaH.gif" />
                                    </div>
                                    <%--Katani--%>
                                    <div style="z-index: 104; position: absolute; width: 121px; top: 302px; left: 872px;"
                                        id="divPannaL" visible="false" runat="server">
                                        <img src="images/MapImg/PannaL.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 301px; left: 873px;"
                                        id="divPannaM" visible="false" runat="server">
                                        <img src="images/MapImg/PannaM.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 301px; left: 873px;"
                                        id="divPannaH" visible="false" runat="server">
                                        <img src="images/MapImg/PannaH.gif" />
                                    </div>
                                    <%--Umariya--%>
                                    <div style="z-index: 104; position: absolute; width: 121px; top: 513px; left: 741px;"
                                        id="divnarpL" visible="false" runat="server">
                                        <img src="images/MapImg/narpL.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 513px; left: 741px;"
                                        id="divnarpM" visible="false" runat="server">
                                        <img src="images/MapImg/narpM.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 513px; left: 741px;"
                                        id="divnarpH" visible="false" runat="server">
                                        <img src="images/MapImg/narpH.gif" />
                                    </div>
                                    <%--Damoh--%>
                                    <div style="z-index: 104; position: absolute; width: 121px; top: 472px; left: 834px;"
                                        id="divJabalpurL" visible="false" runat="server">
                                        <img src="images/MapImg/JabalpurL.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 472px; left: 834px;"
                                        id="divJabalpurM" visible="false" runat="server">
                                        <img src="images/MapImg/JabalpurM.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 472px; left: 834px;"
                                        id="divJabalpurH" visible="false" runat="server">
                                        <img src="images/MapImg/JabalpurH.gif" />
                                    </div>
                                    <%--Seoni--%>
                                    <div style="z-index: 104; position: absolute; width: 121px; top: 413px; left: 882px;"
                                        id="divKataniL" visible="false" runat="server">
                                        <img src="images/MapImg/KataniL.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 413px; left: 882px;"
                                        id="divKataniM" visible="false" runat="server">
                                        <img src="images/MapImg/KataniM.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 413px; left: 882px;"
                                        id="divKataniH" visible="false" runat="server">
                                        <img src="images/MapImg/KataniH.gif" />
                                    </div>
                                    <%--Chhindwara--%>
                                    <div style="z-index: 104; position: absolute; width: 121px; top: 420px; left: 956px;"
                                        id="divUmariyaL" visible="false" runat="server">
                                        <img src="images/MapImg/UmariyaL.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 420px; left: 956px;"
                                        id="divUmariyaM" visible="false" runat="server">
                                        <img src="images/MapImg/UmariyaM.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 420px; left: 956px;"
                                        id="divUmariyaH" visible="false" runat="server">
                                        <img src="images/MapImg/UmariyaH.gif" />
                                    </div>
                                    <%--Balaghat--%>
                                    <div style="z-index: 104; position: absolute; width: 121px; top: 380px; left: 805px;"
                                        id="divDamohL" visible="false" runat="server">
                                        <img src="images/MapImg/DamohL.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 380px; left: 805px;"
                                        id="divDamohM" visible="false" runat="server">
                                        <img src="images/MapImg/DamohM.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 381px; left: 805px;"
                                        id="divDamohH" visible="false" runat="server">
                                        <img src="images/MapImg/DamohH.gif" />
                                    </div>
                                    <%--Mandla--%>
                                    <div style="z-index: 104; position: absolute; width: 121px; top: 546px; left: 821px;"
                                        id="divSeoniL" visible="false" runat="server">
                                        <img src="images/MapImg/SeoniL.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 546px; left: 821px;"
                                        id="divSeoniM" visible="false" runat="server">
                                        <img src="images/MapImg/SeoniM.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 546px; left: 821px;"
                                        id="divSeoniH" visible="false" runat="server">
                                        <img src="images/MapImg/SeoniH.gif" />
                                    </div>
                                    <%--Anuppur--%>
                                    <div style="z-index: 104; position: absolute; width: 121px; top: 565px; left: 720px;"
                                        id="divChhindwaraL" visible="false" runat="server">
                                        <img src="images/MapImg/ChhindwaraL.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 565px; left: 720px;"
                                        id="divChhindwaraM" visible="false" runat="server">
                                        <img src="images/MapImg/ChhindwaraM.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 565px; left: 720px;"
                                        id="divChhindwaraH" visible="false" runat="server">
                                        <img src="images/MapImg/ChhindwaraH.gif" />
                                    </div>
                                    <%--Satna--%>
                                    <div style="z-index: 104; position: absolute; width: 121px; top: 607px; left: 854px;"
                                        id="divBalaghatL" visible="false" runat="server">
                                        <img src="images/MapImg/BalaghatL.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 607px; left: 854px;"
                                        id="divBalaghatM" visible="false" runat="server">
                                        <img src="images/MapImg/BalaghatM.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 607px; left: 854px;"
                                        id="divBalaghatH" visible="false" runat="server">
                                        <img src="images/MapImg/BalaghatH.gif" />
                                    </div>
                                    <%--Sidhi--%>
                                    <div style="z-index: 104; position: absolute; width: 121px; top: 521px; left: 897px;"
                                        id="divMandalL" visible="false" runat="server">
                                        <img src="images/MapImg/MandalL.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 521px; left: 897px;"
                                        id="divMandalM" visible="false" runat="server">
                                        <img src="images/MapImg/MandalM.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 521px; left: 897px;"
                                        id="divMandalH" visible="false" runat="server">
                                        <img src="images/MapImg/MandalH.gif" />
                                    </div>
                                    <%--Sheopur--%>
                                    <div style="z-index: 104; position: absolute; width: 121px; top: 489px; left: 1019px;"
                                        id="divAnuppurL" visible="false" runat="server">
                                        <img src="images/MapImg/AnuppurL.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 489px; left: 1019px;"
                                        id="divAnuppurM" visible="false" runat="server">
                                        <img src="images/MapImg/AnuppurM.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 489px; left: 1019px;"
                                        id="divAnuppurH" visible="false" runat="server">
                                        <img src="images/MapImg/AnuppurH.gif" />
                                    </div>
                                    <%--Shivpur--%>
                                    <div style="z-index: 104; position: absolute; width: 121px; top: 295px; left: 938px;"
                                        id="divSatnaL" visible="false" runat="server">
                                        <img src="images/MapImg/SatanL.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 295px; left: 938px;"
                                        id="divSatnaM" visible="false" runat="server">
                                        <img src="images/MapImg/SatanM.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 295px; left: 938px;"
                                        id="divSatnaH" visible="false" runat="server">
                                        <img src="images/MapImg/SatanH.gif" />
                                    </div>
                                    <%--Ashok--%>
                                    <div style="z-index: 104; position: absolute; width: 121px; top: 356px; left: 1034px;"
                                        id="divSidhiL" visible="false" runat="server">
                                        <img src="images/MapImg/SidhiL.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 356px; left: 1034px;"
                                        id="divSidhiM" visible="false" runat="server">
                                        <img src="images/MapImg/SidhiM.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 356px; left: 1034px;"
                                        id="divSidhiH" visible="false" runat="server">
                                        <img src="images/MapImg/SidhiH.gif" />
                                    </div>
                                    <%--Guna--%>
                                    <div style="z-index: 104; position: absolute; width: 121px; top: 182px; left: 542px;"
                                        id="divSheopurL" visible="false" runat="server">
                                        <img src="images/MapImg/SheopurL.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 182px; left: 542px;"
                                        id="divSheopurM" visible="false" runat="server">
                                        <img src="images/MapImg/SheopurM.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 182px; left: 542px;"
                                        id="divSheopurH" visible="false" runat="server">
                                        <img src="images/MapImg/SheopurH.gif" />
                                    </div>
                                    <%--Khandwa--%>
                                    <div style="z-index: 104; position: absolute; width: 121px; top: 216px; left: 594px;"
                                        id="divShivpurL" visible="false" runat="server">
                                        <img src="images/MapImg/ShivpurL.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 216px; left: 594px;"
                                        id="divShivpurM" visible="false" runat="server">
                                        <img src="images/MapImg/ShivpurM.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 216px; left: 594px;"
                                        id="divShivpurH" visible="false" runat="server">
                                        <img src="images/MapImg/ShivpurH.gif" />
                                    </div>
                                    <%--Khargone--%>
                                    <div style="z-index: 104; position: absolute; width: 121px; top: 319px; left: 641px;"
                                        id="divAshokL" visible="false" runat="server">
                                        <img src="images/MapImg/AshokL.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 318px; left: 642px;"
                                        id="divAshokM" visible="false" runat="server">
                                        <img src="images/MapImg/AshokM.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 318px; left: 642px;"
                                        id="divAshokH" visible="false" runat="server">
                                        <img src="images/MapImg/AshokH.gif" />
                                    </div>
                                    <%--Burhanpur--%>
                                    <div style="z-index: 104; position: absolute; width: 121px; top: 304px; left: 572px;"
                                        id="divGunaL" visible="false" runat="server">
                                        <img src="images/MapImg/GunaL.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 304px; left: 572px;"
                                        id="divGunaM" visible="false" runat="server">
                                        <img src="images/MapImg/GunaM.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 304px; left: 572px;"
                                        id="divGunaH" visible="false" runat="server">
                                        <img src="images/MapImg/GunaH.gif" />
                                    </div>
                                    <%--Barwani--%>
                                    <div style="z-index: 104; position: absolute; width: 121px; top: 606px; left: 486px;"
                                        id="divKhandwaL" visible="false" runat="server">
                                        <img src="images/MapImg/KhandwaL.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 606px; left: 486px;"
                                        id="divKhandwaM" visible="false" runat="server">
                                        <img src="images/MapImg/KhandwaM.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 606px; left: 486px;"
                                        id="divKhandwaH" visible="false" runat="server">
                                        <img src="images/MapImg/KhandwaH.gif" />
                                    </div>
                                    <%--Alirajpur--%>
                                    <div style="z-index: 104; position: absolute; width: 121px; top: 592px; left: 401px;"
                                        id="divKhargoneL" visible="false" runat="server">
                                        <img src="images/MapImg/KhargoneL.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 592px; left: 401px;"
                                        id="divKhargoneM" visible="false" runat="server">
                                        <img src="images/MapImg/KhargoneM.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 592px; left: 401px;"
                                        id="divKhargoneH" visible="false" runat="server">
                                        <img src="images/MapImg/KhargoneH.gif" />
                                    </div>
                                    <%--Mansour--%>
                                    <div style="z-index: 104; position: absolute; width: 121px; top: 697px; left: 479px;"
                                        id="divBurhanpurL" visible="false" runat="server">
                                        <img src="images/MapImg/BurhanpurL.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 697px; left: 479px;"
                                        id="divBurhanpurM" visible="false" runat="server">
                                        <img src="images/MapImg/BurhanpurM.gif" /><%--images/MapImg/BurhanpurM.gif--%>
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 697px; left: 479px;"
                                        id="divBurhanpurH" visible="false" runat="server">
                                        <img src="images/MapImg/BurhanpurH.gif" />
                                    </div>
                                    <%--Dindori--%>
                                    <div style="z-index: 104; position: absolute; width: 121px; top: 635px; left: 321px;"
                                        id="divBarwaniL" visible="false" runat="server">
                                        <img src="images/MapImg/BarwaniL.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 635px; left: 321px;"
                                        id="divBarwaniM" visible="false" runat="server">
                                        <img src="images/MapImg/BarwaniM.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 636px; left: 321px;"
                                        id="divBarwaniH" visible="false" runat="server">
                                        <img src="images/MapImg/BarwaniH.gif" />
                                    </div>
                                    <%--Shahadol--%>
                                    <div style="z-index: 104; position: absolute; width: 121px; top: 576px; left: 280px;"
                                        id="divAlirajpurL" visible="false" runat="server">
                                        <img src="images/MapImg/AlirajpurL.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 576px; left: 280px;"
                                        id="divAlirajpurM" visible="false" runat="server">
                                        <img src="images/MapImg/AlirajpurM.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 576px; left: 280px;"
                                        id="divAlirajpurH" visible="false" runat="server">
                                        <img src="images/MapImg/AlirajpurH.gif" />
                                    </div>
                                    <%--Singrauli--%>
                                    <div style="z-index: 104; position: absolute; width: 121px; top: 343px; left: 373px;"
                                        id="divMansourL" visible="false" runat="server">
                                        <img src="images/MapImg/MansourL.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 343px; left: 373px;"
                                        id="divMansourM" visible="false" runat="server">
                                        <img src="images/MapImg/MansourM.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 343px; left: 373px;"
                                        id="divMansourH" visible="false" runat="server">
                                        <img src="images/MapImg/MansourH.gif" />
                                    </div>
                                    <%--Tikamgarh--%>
                                    <div style="z-index: 104; position: absolute; width: 121px; top: 498px; left: 952px;"
                                        id="divDindoriL" visible="false" runat="server">
                                        <img src="images/MapImg/DindoriL.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 498px; left: 952px;"
                                        id="divDindoriM" visible="false" runat="server">
                                        <img src="images/MapImg/DindoriM.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 498px; left: 951px;"
                                        id="divDindoriH" visible="false" runat="server">
                                        <img src="images/MapImg/DindoriH.gif" />
                                    </div>
                                    <%--Shahadol--%>
                                    <div style="z-index: 104; position: absolute; width: 121px; top: 395px; left: 1002px;"
                                        id="divShahadolL" visible="false" runat="server">
                                        <img src="images/MapImg/ShahadolL.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 396px; left: 1001px;"
                                        id="divShahadolM" visible="false" runat="server">
                                        <img src="images/MapImg/ShahadolM.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 396px; left: 1001px;"
                                        id="divShahadolH" visible="false" runat="server">
                                        <img src="images/MapImg/ShahadolH.gif" />
                                    </div>
                                    <%--Singrauli--%>
                                    <div style="z-index: 104; position: absolute; width: 121px; top: 346px; left: 1093px;"
                                        id="divSingrauliL" visible="false" runat="server">
                                        <img src="images/MapImg/SingrauliL.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 346px; left: 1093px;"
                                        id="divSingrauliM" visible="false" runat="server">
                                        <img src="images/MapImg/SingraulM.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 346px; left: 1093px;"
                                        id="divSingrauliH" visible="false" runat="server">
                                        <img src="images/MapImg/SingraulH.gif" />
                                    </div>
                                    <%--Tikamgarh--%>
                                    <div style="z-index: 104; position: absolute; width: 121px; top: 255px; left: 744px;"
                                        id="divTikamgarhL" visible="false" runat="server">
                                        <img src="images/MapImg/TikamgarhL.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 255px; left: 744px;"
                                        id="divTikamgarhM" visible="false" runat="server">
                                        <img src="images/MapImg/TikamgarhM.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 121px; top: 255px; left: 744px;"
                                        id="divTikamgarhH" visible="false" runat="server">
                                        <img src="images/MapImg/TikamgarhH.gif" />
                                    </div>
                                    <div style="z-index: 103; position: absolute; width: 250px; top: 615px; left: 1030px;"
                                        id="div1" runat="server">
                                        
                                    </div>
                                </div>
                                <div style="z-index: 104; position: absolute; width: 276px; top: 152px; left: 876px;
                                    font-size: 16px;" id="divmineral" runat="server">
                                    <asp:Label ID="Label1" runat="server"></asp:Label>
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
