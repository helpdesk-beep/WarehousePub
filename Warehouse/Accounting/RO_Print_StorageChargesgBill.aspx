<%@ Page Language="C#" MasterPageFile="~/MasterPage/Region_Master.master" AutoEventWireup="true" CodeFile="RO_Print_StorageChargesgBill.aspx.cs" Inherits="Accounting_RO_Print_StorageChargesgBill" Title="RO Print Bill" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

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

    <script type="text/javascript">
        function PrintDiv_Actual() {
            var divContents = document.getElementById("printActualBill").innerHTML;
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
        function PrintDiv_Epo() {
            var divContents = document.getElementById("PrintDivEpo").innerHTML;
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
    <fieldset style="width: 700px; border: 2px solid navy; margin-left: 5px;">
        <center>
            <div>

                <table width="100%">
                    <tr style="background-color: #0bb6e6; height: 25px">
                        <td colspan="4" align="center">
                            <asp:Label ID="Label37" runat="server" Font-Bold="True" Font-Size="12pt" ForeColor="WhiteSmoke"
                                Text="View/Print Storage Charges and Godown Rent Bill"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="height: 50px; font-size: 14px" align="center" colspan="4">Branch &nbsp;<asp:DropDownList ID="ddlBranch" runat="server"
                            Height="25px" Width="160px" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                        </asp:DropDownList>
                            &nbsp;&nbsp;&nbsp;&nbsp;
     Godown &nbsp;
                            <asp:DropDownList ID="ddlgdwn" runat="server"
                                Height="25px" Width="200px" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlgdwn_SelectedIndexChanged">
                            </asp:DropDownList>

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
                                    <td colspan="4" align="center" style="height: 30px;">
                                        <asp:Label ID="Label18" runat="server" Font-Size="12pt" ForeColor="Blue"
                                            Text="भंडारण शुल्क बिल का प्रिन्ट लेने के लिये दी गई लिंक पे क्लिक करें"></asp:Label></td>
                                </tr>

                                <tr>
                                    <td colspan="4" align="center" style="height: 40px; font-size: 14px;">Financial Year &nbsp;<asp:DropDownList ID="ddlFY" runat="server"
                                        Height="25px" Width="200px" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlFY_SelectedIndexChanged">

                                    </asp:DropDownList>
                                    </td>

                                    <td colspan="4" align="center" style="height: 40px; font-size: 14px;">Bill No. &nbsp;<asp:DropDownList ID="ddlactualbillno" runat="server"
                                        Height="25px" Width="200px" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlactualbillno_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    </td>
                                </tr>


                                <tr align="center">
                                    <td align="center" style="font-size: 14px; height: 40px;" colspan="8">
                                        <table border="1" width="90%">
                                            <tbody align="center">
                                                <th>Commodity</th>

                                                <th>Crop Year</th>
                                                <th>Financial Year </th>
                                                <th>Month</th>
                                                <th>From Date </th>
                                                <th>To Date</th>
                                            </tbody>
                                            <tr align="center">
                                                <td style="width: 20%;">
                                                    <asp:Label ID="lblrmcmd" runat="server"></asp:Label>
                                                </td>

                                                <td style="width: 20%;">
                                                    <asp:Label ID="lblCPY" runat="server"></asp:Label>
                                                </td>
                                                <td style="width: 20%;">
                                                    <asp:Label ID="lblfinancial" runat="server"></asp:Label>
                                                </td>
                                                <td style="width: 15%;">
                                                    <asp:Label ID="lblrmmonth" runat="server"></asp:Label>
                                                </td>
                                                <td style="width: 15%;">
                                                    <asp:Label ID="lblfrmdate" runat="server"></asp:Label>
                                                </td>
                                                <td style="width: 15%;">
                                                    <asp:Label ID="lbltodate" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>


                                <tr>
                                    <td colspan="5" style="font-size: 14px; height: 50px;">अक्चुअल देयक के आधार पर बनाएँ गए बिल का विवरण देखने के लिये क्लिक करें |

                                    </td>
                                    <td align="center" colspan="8">
                                        <asp:Button class="button button2" ID="Button2" Style="width: 150px" runat="server" Text="View Actual Bill" Height="29px"></asp:Button>
                                    </td>
                                </tr>

                                <tr>
                                    <td colspan="5" style="font-size: 14px; height: 30px;">अक्चुअल देयक के आधार पर बनाएँ गए EPO विवरण देखने के लिये क्लिक करें |
                                    </td>
                                    <td align="center" colspan="8">
                                        <asp:Button class="button button2" ID="btnepo" Style="width: 150px" runat="server" Text="View EPO" Height="29px"></asp:Button>
                                    </td>
                                </tr>

                                <tr>
                                    <td align="center" colspan="8">
                                        <div style="width: 100%;">
                                            <img id="Img2" src="../Images/line.png" height="15px" width="100%" alt="" />
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="8" align="center" style="height: 30px;">
                                        <asp:Label ID="Label46" runat="server" Font-Size="12pt" ForeColor="Blue"
                                            Text="गोडाउन रेंट बिल का प्रिन्ट लेने के लिये दी गई लिंक पे क्लिक करें"></asp:Label></td>
                                </tr>

                                <tr>
                                    <td colspan="8" align="center" style="height: 40px; font-size: 14px;">JVS Bill No. &nbsp;<asp:DropDownList ID="ddlbillno" runat="server"
                                        Height="25px" Width="200px" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlbillno_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    </td>
                                </tr>

                                <tr align="center">
                                    <td align="center" style="font-size: 14px; height: 40px;" colspan="8">
                                        <table border="1" width="90%">
                                            <tbody align="center">
                                                <th>Commodity</th>

                                                <th>Crop Year</th>
                                                <th>Financial Year </th>
                                                <th>Month</th>
                                                <th>From Date </th>
                                                <th>To Date</th>
                                            </tbody>
                                            <tr align="center">
                                                <td style="width: 20%;">
                                                    <asp:Label ID="lblRentCmd" runat="server"></asp:Label>
                                                </td>

                                                <td style="width: 20%;">
                                                    <asp:Label ID="lblRentCropYear" runat="server"></asp:Label>
                                                </td>
                                                <td style="width: 20%;">
                                                    <asp:Label ID="lblRentF_Year" runat="server"></asp:Label>
                                                </td>
                                                <td style="width: 15%;">
                                                    <asp:Label ID="lblRentMonth" runat="server"></asp:Label>
                                                </td>
                                                <td style="width: 15%;">
                                                    <asp:Label ID="lblRentFromDate" runat="server"></asp:Label>
                                                </td>
                                                <td style="width: 15%;">
                                                    <asp:Label ID="lblRentToDate" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>

                                <tr>
                                    <td colspan="5" style="font-size: 14px; height: 50px;">बनाएँ गए Rent बिल का विवरण देखने के लिये क्लिक करें |

                                    </td>
                                    <td align="center" colspan="8">
                                        <asp:Button class="button button2" ID="btnviewbill" Style="width: 150px" runat="server" Text="View Bill" Height="29px"></asp:Button>
                                    </td>
                                </tr>

                                <tr>
                                    <td colspan="5" style="font-size: 14px; height: 30px;">बनाएँ गए बिल मे किये गए कतोत्र का विवरण देखने के लिये क्लिक करें |

                                    </td>
                                    <td align="center" colspan="8">
                                        <asp:Button class="button button2" ID="btnviewdetuction" Style="width: 150px" runat="server" Text="View Bill Detuction" Height="29px"></asp:Button>
                                    </td>
                                </tr>


                            </table>
                        </td>
                    </tr>
                </table>

                <%------------------------------------------------------------------------%>

                <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="pnlviewbilldetails" TargetControlID="btnviewbill"
                    CancelControlID="btncloseconfrm" BackgroundCssClass="modalBackground">
                </cc1:ModalPopupExtender>

                <asp:Panel ID="pnlviewbilldetails" runat="server" CssClass="modalPopup" Style="width: 850px; height: 620px; display: none;" ScrollBars="Vertical">

                    <%--<asp:Panel ID="pnlviewbilldetails" runat="server" CssClass="modalPopup" style="width: 850px; height:650px;" ScrollBars="Vertical">--%>

                    <div>
                        <table cellspacing="1" cellpadding="3" style="width: 100%;">
                            <tr>
                                <td align="center" width="100%">
                                    <div id="PrintDiv">
                                        <table width="100%">
                                            <tr>
                                                <td colspan="2" align="center">
                                                    <asp:Label ID="Label1" runat="server" Text="मध्य प्रदेश वेयर हाउसिंग एवं लॉजिस्टिक्स कार्पोरेशन, क्षेत्रीय कार्यालय -" Font-Size="14px" Font-Bold="true"></asp:Label>
                                                    <asp:Label ID="lblP_regionnm" runat="server" Font-Size="14px" Font-Bold="true"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" align="center">
                                                    <asp:Label ID="Label2" runat="server" Text="-: वास्तविक भंडारित मात्रा अंतर्गत J.V. योजना मे लिए गए गोदामो के देयक :-" Font-Size="14px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 10px;"></td>
                                            </tr>
                                            <tr>
                                                <td width="60%" align="left">
                                                    <asp:Label ID="Label5" runat="server" Text="शाखा का नाम :- " Font-Size="12px"></asp:Label>&nbsp;
                                            <asp:Label ID="lblbranch" runat="server" Text="माह :- " Font-Size="12px"></asp:Label>
                                                </td>
                                                <td width="40%" align="left">
                                                    <asp:Label ID="Label3" runat="server" Text="माह :- " Font-Size="12px"></asp:Label>&nbsp;
                                            <asp:Label ID="lblbillmonth" runat="server" Text="माह :- " Font-Size="12px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="Label9" runat="server" Text="वेयरहाउस का नाम :-  " Font-Size="12px"></asp:Label>&nbsp;
                                            <asp:Label ID="lblgdwnname" runat="server" Text="माह :- " Font-Size="12px"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="Label7" runat="server" Text="गोदाम क्र. :- " Font-Size="12px"></asp:Label>&nbsp;
                                            <asp:Label ID="lblGdnum" runat="server" Text="माह :- " Font-Size="12px"></asp:Label>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="Label11" runat="server" Text="बिल क्रमांक :- " Font-Size="12px"></asp:Label>&nbsp;
                                            <asp:Label ID="lblbillno" runat="server" Text="माह :- " Font-Size="12px"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="Label13" runat="server" Text="भंडारित स्कंध का नाम :- " Font-Size="12px"></asp:Label>&nbsp;
                                            <asp:Label ID="lblcmd" runat="server" Text="माह :- " Font-Size="12px"></asp:Label>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="Label15" runat="server" Text="GSTN : 23AADCM7742B1ZU" Font-Size="12px"></asp:Label>&nbsp;
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="Label17" runat="server" Text="मात्रा :- मेट्रिक टन मे" Font-Size="12px"></asp:Label>&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="Label21" runat="server" Text="PAN : AADCM7742B" Font-Size="12px"></asp:Label>&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 10px;"></td>
                                            </tr>

                                            <tr>
                                                <td align="center" colspan="2">
                                                    <asp:GridView ID="GD1" runat="server" AutoGenerateColumns="False" Width="100%" Font-Names="Arial"
                                                        DataKeyNames="Date" BorderStyle="Double" CellPadding="3" CellSpacing="5" ShowFooter="true"
                                                        Font-Size="11px" BorderColor="#CCCCCC">
                                                        <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="क्र." ItemStyle-Width="50px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="Date" HeaderText="दिनाँक"></asp:BoundField>

                                                            <asp:BoundField DataField="Opening_Weight" HeaderText="प्रारंभिक मात्रा" ItemStyle-HorizontalAlign="Left">
                                                                <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                                            </asp:BoundField>

                                                            <asp:BoundField DataField="Receive_Weight" HeaderText="जमा मात्रा" ItemStyle-HorizontalAlign="Left">
                                                                <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                                            </asp:BoundField>

                                                            <asp:BoundField DataField="Issue_Weight" HeaderText="भुगतान मात्रा" ItemStyle-HorizontalAlign="Left">
                                                                <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                                            </asp:BoundField>

                                                            <asp:BoundField DataField="Closing_Weight" HeaderText="शेष मात्रा" ItemStyle-HorizontalAlign="Left">
                                                                <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                                            </asp:BoundField>

                                                            <asp:BoundField DataField="Per_Day_Rate" HeaderText="शुल्क दर प्रति दिन" ItemStyle-HorizontalAlign="Left">
                                                                <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                                            </asp:BoundField>

                                                            <asp:BoundField DataField="Total_Charges" HeaderText="राशि (6x7)" ItemStyle-HorizontalAlign="Left">
                                                                <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                                            </asp:BoundField>
                                                        </Columns>
                                                        <FooterStyle Font-Bold="True" ForeColor="#C70039" HorizontalAlign="center" Height="15px" Font-Size="11px" />
                                                        <HeaderStyle ForeColor="#C70039" HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="11px" />
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 30px;"></td>
                                            </tr>

                                            <%-----------------DSC Div-----------   --%>
                                            <tr>
                                                <td align="right" colspan="2" style="width: 100%;">

                                                    <table style="width: 100%;">

                                                        <tr class="fountcolor">
                                                            <td align="right">
                                                                <asp:Image ID="Image5B" runat="server" Height="30px" Visible="false"
                                                                    ImageUrl="~/images/dsc1.png" Width="80px" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                            </td>
                                                            <td align="right">
                                                                <asp:Image ID="Image5" runat="server" Height="30px" Visible="false"
                                                                    ImageUrl="~/images/dsc1.png" Width="80px" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr class="fountcolor">
                                                            <td align="right">
                                                                <asp:Label ID="lbldscTB" runat="server" Text="" Font-Bold="True"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                            <td align="right">
                                                                <asp:Label ID="lbldscT" runat="server" Text="" Font-Bold="True"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                        </tr>
                                                        <tr class="fountcolor">
                                                            <td align="right">
                                                                <asp:Label ID="lblDSC_HolderB" runat="server" Text="" Font-Bold="false"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                            <td align="right">
                                                                <asp:Label ID="lblDSC_Holder" runat="server" Text="" Font-Bold="false"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                        </tr>
                                                        <tr class="fountcolor">
                                                            <td align="right">
                                                                <asp:Label ID="lblSigningDateB" runat="server" Text="" Font-Bold="false"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                            <td align="right">
                                                                <asp:Label ID="lblSigningDate" runat="server" Text="" Font-Bold="false"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                        </tr>
                                                        <tr class="fountcolor">
                                                            <td align="right">
                                                                <asp:Label ID="lblIpAddB" runat="server" Text="" Font-Bold="false"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                            <td align="right">
                                                                <asp:Label ID="lblIpAdd" runat="server" Text="" Font-Bold="false"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                        </tr>
                                                        <tr class="fountcolor">
                                                            <td colspan="2">
                                                                <br />
                                                            </td>
                                                        </tr>
                                                        <tr class="fountcolor">
                                                            <td align="right">
                                                                <asp:Label ID="Label43" runat="server" Text="शाखा प्रबंधक(MPWLC) के हस्ताक्षर" Font-Bold="true"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                            <td align="right">
                                                                <asp:Label ID="Label44" runat="server" Text="माल गोदामपाल के हस्ताक्षर" Font-Bold="true"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                        </tr>

                                                    </table>

                                                </td>
                                            </tr>


                                            <%------------DSC DIv END----------------- --%>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="height: 50px">
                                    <asp:Button class="button button2" Width="150px" Height="30px" ID="btncloseconfrm"
                                        runat="server" Text="Close" align="Center" />
                                    &nbsp &nbsp &nbsp 
     
                            
                        <input id="Button1" name="Print" type="button" style="height: 30px; width: 150px;"
                            class="button button2" value="Print" onclick="PrintDiv();" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </asp:Panel>

                <%------------------------------END Of JVS Bill----------------------------------------------%>

                <%--------------------------------JSV Detuction----------------------------------------%>

                <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" PopupControlID="Panel1" TargetControlID="btnviewdetuction"
                    CancelControlID="btndetuctionclose" BackgroundCssClass="modalBackground">
                </cc1:ModalPopupExtender>

                <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" Style="width: 850px; height: 620px; display: none;" ScrollBars="Vertical">

                    <%--<asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" style="width: 850px; height:650px;" ScrollBars="Vertical">
                    --%>
                    <div>
                        <table cellspacing="1" cellpadding="3" style="width: 100%;">
                            <tr>
                                <td align="center" width="100%">
                                    <div id="PrintDiv_Det">
                                        <table width="100%">
                                            <tr>
                                                <td colspan="2" align="center">
                                                    <asp:Label ID="Label8" runat="server" Text="मध्य प्रदेश वेयर हाउसिंग एवं लॉजिस्टिक्स कार्पोरेशन, क्षेत्रीय कार्यालय -" Font-Size="14px" Font-Bold="true"></asp:Label>
                                                    <asp:Label ID="lbld_rmname" runat="server" Font-Size="14px" Font-Bold="true"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" align="center">
                                                    <asp:Label ID="Label16" runat="server" Text="-: वास्तविक भंडारित मात्रा अंतर्गत J.V. योजना मे लिए गए गोदामो के देयक मे किये गए कतोत्र का विवरण :-" Font-Size="14px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 10px;"></td>
                                            </tr>
                                            <tr>
                                                <td width="60%" align="left">
                                                    <asp:Label ID="Label19" runat="server" Text="शाखा का नाम :- " Font-Size="12px"></asp:Label>&nbsp;
                                            <asp:Label ID="lbld_branch" runat="server" Text="माह :- " Font-Size="12px"></asp:Label>
                                                </td>
                                                <td width="40%" align="left">
                                                    <asp:Label ID="Label26" runat="server" Text="माह :- " Font-Size="12px"></asp:Label>&nbsp;
                                            <asp:Label ID="lbld_month" runat="server" Text="माह :- " Font-Size="12px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="Label29" runat="server" Text="वेयरहाउस का नाम :-  " Font-Size="12px"></asp:Label>&nbsp;
                                            <asp:Label ID="lbld_warehousename" runat="server" Text="माह :- " Font-Size="12px"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="Label31" runat="server" Text="गोदाम क्र. :- " Font-Size="12px"></asp:Label>&nbsp;
                                            <asp:Label ID="lbld_gdno" runat="server" Text="माह :- " Font-Size="12px"></asp:Label>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="Label33" runat="server" Text="बिल क्रमांक :- " Font-Size="12px"></asp:Label>&nbsp;
                                            <asp:Label ID="lbld_billno" runat="server" Text="माह :- " Font-Size="12px"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="Label35" runat="server" Text="भंडारित स्कंध का नाम :- " Font-Size="12px"></asp:Label>&nbsp;
                                            <asp:Label ID="lbld_commodity" runat="server" Text="माह :- " Font-Size="12px"></asp:Label>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="Label38" runat="server" Text="GSTN : 23AADCM7742B1ZU" Font-Size="12px"></asp:Label>&nbsp;
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="Label39" runat="server" Text="दर : रुपये मे" Font-Size="12px"></asp:Label>&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="Label40" runat="server" Text="PAN : AADCM7742B" Font-Size="12px"></asp:Label>&nbsp;
                                                </td>
                                            </tr>

                                            <tr>
                                                <td colspan="2">

                                                    <%------------------Tbody------------------------%>

                                                    <%--<table border="1" style="width:100%; font-size:11px;"  >
                            <tbody>
                                <tr>
                                    <th>
                                        क़.</th>
                                    <th>
                                        संसाधन विवरण</th>
                                    <th>
                                        उपलब्ध संसाधन मात्र</th>
                                    <th>
                                        कटौत्रे हेतु मात्र</th>
                                    <th>
                                        दर</th>
                                    <th>
                                        कटौत्रा राश</th>
                                </tr>
                            </tbody>
                            <tr>
                            <td>1</td>                           
                            <td>डनेज पोलीथीन/डनेज शीट</td>
                            <td><asp:Label ID="lbl_Dunnage_PS" runat="server" Text=""></asp:Label></td>
                            <td><asp:Label ID="lbl_Dunnage_PS_DET" runat="server" Text=""></asp:Label></td>
                            <td>5/-</td>
                            <td><asp:Label ID="lbl_Dunnage_PS_Amt" runat="server" Text=""></asp:Label></td>                            
                                </tr>
                            <tr>
                                <td>2</td>                           
                            <td>इलेक्ट्रोनिक तौल काटा(200 कि॰ग्रा॰ तक) ISI Mark</td>
                            <td><asp:Label ID="lbl_Elect_Beam_Scale" runat="server" Text=""></asp:Label></td>
                            <td><asp:Label ID="lbl_Elect_Beam_Scale_DET" runat="server" Text=""></asp:Label></td>
                            <td>0.25/-</td>
                            <td><asp:Label ID="lbl_Elect_Beam_Scale_Amt" runat="server" Text=""></asp:Label></td>                           
                            </tr>
                            <tr>
                            <td>3</td>                           
                            <td>लकड़ी की फड़ी</td>
                            <td><asp:Label ID="lbl_Wooden_Planke" runat="server" Text=""></asp:Label></td>
                            <td><asp:Label ID="lbl_Wooden_Planke_DET" runat="server" Text=""></asp:Label></td>
                            <td>0.25/-</td>
                            <td><asp:Label ID="lbl_Wooden_Planke_Amt" runat="server" Text=""></asp:Label></td>                               
                                </tr>
                            <tr>
                                <td>4</td>                       
                            <td>एनलायसिस फिट सेट</td>
                            <td><asp:Label ID="lbl_Ana_Kit_Set" runat="server" Text="" ></asp:Label></td>
                            <td><asp:Label ID="lbl_Ana_Kit_Set_DET" runat="server" Text=""></asp:Label></td>
                            <td>	0.10/-</td>
                            <td><asp:Label ID="lbl_Ana_Kit_Set_Amt" runat="server" Text=""></asp:Label></td>                               
                            </tr>
                            <tr>
                            <td>5</td>                           
                            <td>फ्यूमीगेशन कवर</td>
                            <td><asp:Label ID="lbl_Fumigation_Cover" runat="server" Text=""></asp:Label></td>
                            <td><asp:Label ID="lbl_Fumigation_Cover_DET" runat="server" Text=""></asp:Label></td>
                            <td>	3/-</td>
                            <td><asp:Label ID="lbl_Fumigation_Cover_Amt" runat="server" Text=""></asp:Label></td>                               
                                </tr>
                            <tr>
                            <td>6</td>                           
                            <td>Fire Extinguisher ISI Mark</td>
                            <td><asp:Label ID="lbl_Fire_Extinguisher" runat="server" Text=""></asp:Label></td>
                            <td><asp:Label ID="lbl_Fire_Extinguisher_DET" runat="server" Text=""></asp:Label></td>
                            <td>	0.50/-</td>
                            <td><asp:Label ID="lbl_Fire_Extinguisher_Amt" runat="server" Text=""></asp:Label></td>                               
                            
                                </tr>
                            <tr>
                            <td>7</td>                           
                            <td>Fire Buckets ISI Mark</td>
                            <td><asp:Label ID="lbl_Fire_Buckets" runat="server" Text=""></asp:Label></td>
                            <td><asp:Label ID="lbl_Fire_Buckets_DET" runat="server" Text=""></asp:Label></td>
                            <td>	0.50/-</td>
                            <td><asp:Label ID="lbl_Fire_Buckets_Amt" runat="server" Text=""></asp:Label></td>                               
                                </tr>
                            <tr>
                            <td>8</td>                           
                            <td> गोदाम की सुरक्षा व्यवस्था हेतु चौकीदरी हेतु सुरक्षा कर्मी उपलब्ध कराना </td>
                            <td><asp:Label ID="lbl_Security_Guard" runat="server" Text=""></asp:Label></td>
                            <td><asp:Label ID="lbl_Security_Guard_DET" runat="server" Text=""></asp:Label></td>
                            <td>7700/-</td>
                            <td><asp:Label ID="lbl_Security_Guard_Amt" runat="server" Text=""></asp:Label></td>                               
                                </tr>
                            <tr>
                            <td>9</td>                           
                            <td>पावर स्प्रे पंप अथवा फुट स्प्रेयर पंप</td>
                            <td><asp:Label ID="lbl_Sprey_Pump" runat="server" Text=""></asp:Label></td>
                            <td><asp:Label ID="lbl_Sprey_Pump_DET" runat="server" Text=""></asp:Label></td>
                            <td>	2/-</td>
                            <td><asp:Label ID="lbl_Sprey_Pump_Amt" runat="server" Text=""></asp:Label></td>                               
                                </tr>
                            <tr>
                            <td>10</td>                           
                            <td>डिजिटल नमीमापक यंत्र(मल्टी कमोडिटी) ISI Mark</td>
                            <td><asp:Label ID="lbl_Digital_Moisture_Meter" runat="server" Text=""></asp:Label></td>
                            <td><asp:Label ID="lbl_Digital_Moisture_Meter_DET" runat="server" Text=""></asp:Label></td>
                            <td>2/-</td>
                            <td><asp:Label ID="lbl_Digital_Moisture_Meter_Amt" runat="server" Text=""></asp:Label></td>                            
                                </tr>
                            <tr>
                            <td>11</td>                           
                            <td>कीटोपचार/सफाई हेतु उपलब्ध कराये जाने वाले श्रमिक</td>
                            <td><asp:Label ID="lbl_Fumigation_Emloyee" runat="server" Text=""></asp:Label></td>
                            <td><asp:Label ID="lbl_Fumigation_Emloyee_DET" runat="server" Text=""></asp:Label></td>
                            <td>7700/-</td>
                            <td><asp:Label ID="lbl_Fumigation_Emloyee_Amt" runat="server" Text=""></asp:Label></td>                            
                                </tr>
                            <tr>
                            <td>12</td>                           
                            <td>बिजली पानी एवं शोचालय सूविधा</td>
                            <td><asp:Label ID="lbl_Common_Facility" runat="server" Text=""></asp:Label></td>
                            <td><asp:Label ID="lbl_Common_Facility_DET" runat="server" Text=""></asp:Label></td>
                            <td>	0.50/-</td>
                            <td><asp:Label ID="lbl_Common_Facility_Amt" runat="server" Text=""></asp:Label></td>                            
                                </tr>
                            <tr>
                            <td>13</td>                           
                            <td>Insectiside Cost(Celphos) @ 500/Rs./Kg + 18% GST</td>
                            <td><asp:Label ID="lbl_Insectiside_Quantity" runat="server" Text=""></asp:Label></td>
                            <td><asp:Label ID="lbl_Insectiside_Quantity_DET" runat="server" Text=""></asp:Label></td>
                            <td>	590/-</td>
                            <td><asp:Label ID="lbl_Insectiside_Cost" runat="server" Text=""></asp:Label></td>                            
                                </tr>
                            <tr>
                        
                            <td colspan="2"> Total Resources Deduction : </td>
                            <td colspan="4" align="right">  <asp:Label ID="lbl_TResources_Deduct_Amt" runat="server" Text=""></asp:Label> </td>
                                </tr>

                            </table>--%>


                                                    <%--------------------Tbody---------------------%>

                                                    <asp:GridView ID="GD_Detuc" runat="server" AutoGenerateColumns="False" Width="100%" Font-Names="Arial"
                                                        DataKeyNames="Res_ID" BorderStyle="Double" CellPadding="3" CellSpacing="5" ShowFooter="true"
                                                        Font-Size="11px" BorderColor="#CCCCCC">
                                                        <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="क्र." ItemStyle-Width="50px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:BoundField DataField="Res_Det_Vivran" HeaderText="कटोत्रा मद का विवरण" ItemStyle-HorizontalAlign="Left">
                                                                <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                                            </asp:BoundField>

                                                            <asp:BoundField DataField="Res_Det_Karan" HeaderText="कटोत्रा का कारण" ItemStyle-HorizontalAlign="Left">
                                                                <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                                            </asp:BoundField>

                                                            <asp:BoundField DataField="Deduction_Amt" HeaderText="कटोत्रा राशि (रू)" ItemStyle-HorizontalAlign="Left">
                                                                <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                                            </asp:BoundField>

                                                            <asp:BoundField DataField="Res_Det_Remark" HeaderText="रिमार्क" ItemStyle-HorizontalAlign="Left">
                                                                <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                                            </asp:BoundField>

                                                        </Columns>
                                                        <FooterStyle Font-Bold="True" ForeColor="#C70039" HorizontalAlign="center" Height="15px" Font-Size="11px" />
                                                        <HeaderStyle ForeColor="#C70039" HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="11px" />
                                                    </asp:GridView>

                                                </td>
                                            </tr>

                                            <tr>
                                                <td align="right" colspan="2" style="width: 100%;">

                                                    <table style="width: 100%;">

                                                        <tr class="fountcolor">

                                                            <td align="right">
                                                                <asp:Image ID="Image4" runat="server" Height="30px" Visible="false"
                                                                    ImageUrl="~/images/dsc1.png" Width="80px" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                            </td>

                                                        </tr>
                                                        <tr class="fountcolor">
                                                            <td align="right">
                                                                <asp:Label ID="lblDSNo" runat="server" Text="" Font-Bold="True"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>

                                                        </tr>
                                                        <tr class="fountcolor">
                                                            <td align="right">
                                                                <asp:Label ID="lblDHolderNm" runat="server" Text="" Font-Bold="false"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>

                                                        </tr>
                                                        <tr class="fountcolor">
                                                            <td align="right">
                                                                <asp:Label ID="lblDSignDate" runat="server" Text="" Font-Bold="false"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>

                                                        </tr>
                                                        <tr class="fountcolor">
                                                            <td align="right">
                                                                <asp:Label ID="lblDIP" runat="server" Text="" Font-Bold="false"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>

                                                        </tr>
                                                        <tr class="fountcolor">
                                                            <td>
                                                                <br />
                                                            </td>
                                                        </tr>
                                                        <tr class="fountcolor">
                                                            <td align="right">
                                                                <asp:Label ID="Label57" runat="server" Text="शाखा प्रबंधक(MPWLC) के हस्ताक्षर" Font-Bold="true"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                        </tr>

                                                    </table>

                                                </td>
                                            </tr>

                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="height: 50px">
                                    <asp:Button class="button button2" Width="150px" Height="30px" ID="btndetuctionclose"
                                        runat="server" Text="Close" align="Center" />
                                    &nbsp &nbsp &nbsp 
     
                            
                        <input id="Button3" name="Print" type="button" style="height: 30px; width: 150px;"
                            class="button button2" value="Print" onclick="PrintDiv_det();" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </asp:Panel>

                <%----------------------------------------------------------------------------%>

                <%------------------------------------------------------------------------%>

                <cc1:ModalPopupExtender ID="ModalPopupExtender3" runat="server" PopupControlID="Panel2" TargetControlID="Button2"
                    CancelControlID="Button4" BackgroundCssClass="modalBackground">
                </cc1:ModalPopupExtender>

                <asp:Panel ID="Panel2" runat="server" CssClass="modalPopup" Style="width: 850px; height: 620px; display: none;" ScrollBars="Vertical">

                    <%--<asp:Panel ID="Panel2" runat="server" CssClass="modalPopup" style="width: 850px; height:650px;" ScrollBars="Vertical">--%>
                    <div>
                        <table cellspacing="1" cellpadding="3" style="width: 100%;">
                            <tr>
                                <td align="center" width="100%">
                                    <div id="printActualBill">
                                        <table width="100%">
                                            <tr>
                                                <td colspan="2" align="center">
                                                    <asp:Label ID="Label4" runat="server" Text="M.P. Warehousing & Logistics Corporation -" Font-Size="14px" Font-Bold="true"></asp:Label>
                                                    <asp:Label ID="Label12" runat="server" Font-Size="14px" Font-Bold="true"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" align="center">
                                                    <asp:Label ID="Label22" runat="server" Text="STORAGE CHARGES" underline="True" Font-Size="14px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 10px;"></td>
                                            </tr>

                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="Label28" runat="server" Text="शाखा का नाम :- " Font-Size="12px"></asp:Label>&nbsp;
                                            <asp:Label ID="lblAcBranch" runat="server" Text="माह :- " Font-Size="12px"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="Label36" runat="server" Text="वेयरहाउस का नाम :- " Font-Size="12px"></asp:Label>&nbsp;
                                            <asp:Label ID="lblAcGdwnName" runat="server" Text="माह :- " Font-Size="12px"></asp:Label>
                                                </td>
                                            </tr>


                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="Label23" runat="server" Text="माह :-  " Font-Size="12px"></asp:Label>&nbsp;
                                            <asp:Label ID="lbldatefromto" runat="server" Text="माह :- " Font-Size="12px"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="Label24" runat="server" Text="जमाकर्ता :- " Font-Size="12px"></asp:Label>&nbsp;
                                            <asp:Label ID="lbldepositor" runat="server" Text="माह :- " Font-Size="12px"></asp:Label>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="Label30" runat="server" Text="बिल क्रमांक :- " Font-Size="12px"></asp:Label>&nbsp;
                                            <asp:Label ID="lblbillno_Actual" runat="server" Text="माह :- " Font-Size="12px"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="Label34" runat="server" Text="स्कंध का नाम :- " Font-Size="12px"></asp:Label>&nbsp;
                                            <asp:Label ID="lblcmd_ac" runat="server" Text="माह :- " Font-Size="12px"></asp:Label>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="Label41" runat="server" Text="GSTN : 23AADCM7742B1ZU" Font-Size="12px"></asp:Label>&nbsp;
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="Label42" runat="server" Text=" मात्रा :- मेट्रिक टन मे" Font-Size="12px"></asp:Label>&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="Label32" runat="server" Text="PAN : AADCM7742B" Font-Size="12px"></asp:Label>&nbsp;
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="Label45" runat="server" Text="Crop Year :- " Font-Size="12px"></asp:Label>&nbsp;
                                            <asp:Label ID="lblCropyear" runat="server" Text="माह :- " Font-Size="12px"></asp:Label>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td style="height: 10px;"></td>
                                            </tr>

                                            <tr>
                                                <td align="center" colspan="2">
                                                    <asp:GridView ID="GD2" runat="server" AutoGenerateColumns="False" Width="100%" Font-Names="Arial"
                                                        DataKeyNames="Date" BorderStyle="Double" CellPadding="3" CellSpacing="5" ShowFooter="true"
                                                        Font-Size="11px" BorderColor="#CCCCCC" OnRowDataBound="GD2_RowDataBound">
                                                        <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="क्र." ItemStyle-Width="50px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="Date" HeaderText="दिनाँक"></asp:BoundField>

                                                            <asp:BoundField DataField="Opening_Weight" HeaderText="प्रारंभिक मात्रा" ItemStyle-HorizontalAlign="Left">
                                                                <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                                            </asp:BoundField>

                                                            <asp:BoundField DataField="Receive_Weight" HeaderText="जमा मात्रा" ItemStyle-HorizontalAlign="Left">
                                                                <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                                            </asp:BoundField>

                                                            <asp:BoundField DataField="Issue_Weight" HeaderText="भुगतान मात्रा" ItemStyle-HorizontalAlign="Left">
                                                                <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                                            </asp:BoundField>

                                                            <%-- <asp:BoundField DataField="Closing_Weight" HeaderText="शेष मात्रा" ItemStyle-HorizontalAlign="Left">
                                                                <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                                            </asp:BoundField>--%>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>शेष मात्रा</HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <div style="text-align: right">
                                                                        <asp:Label ID="lblClosing_Weight" runat="server" Text='<%# Eval("Closing_Weight") %>'></asp:Label>
                                                                    </div>
                                                                </ItemTemplate>

                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="Per_Day_Rate" HeaderText="शुल्क दर प्रति दिन" ItemStyle-HorizontalAlign="Left">
                                                                <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                                            </asp:BoundField>

                                                            <%--<asp:BoundField DataField="Total_Charges" HeaderText="राशि (6x7)" ItemStyle-HorizontalAlign="Left" > 
                                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                                    </asp:BoundField>   --%>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>राशि (6x7)</HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <div style="text-align: right">
                                                                        <asp:Label ID="lblTotal_Charges" runat="server" Text='<%# Eval("Total_Charges") %>'></asp:Label>
                                                                    </div>
                                                                </ItemTemplate>

                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <FooterStyle Font-Bold="True" ForeColor="#C70039" HorizontalAlign="center" Height="15px" Font-Size="11px" />
                                                        <HeaderStyle ForeColor="#C70039" HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="11px" />
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 30px;"></td>
                                            </tr>

                                            <%-------------DSC Actual Bill Stars------------%>


                                            <tr>
                                                <td align="right" colspan="2" style="width: 100%;">

                                                    <table style="width: 100%;">

                                                        <tr class="fountcolor">
                                                            <td align="right">
                                                                <asp:Image ID="Image2" runat="server" Height="30px" Visible="false"
                                                                    ImageUrl="~/images/dsc1.png" Width="80px" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                            </td>
                                                            <td align="right">
                                                                <asp:Image ID="Image1" runat="server" Height="30px" Visible="false"
                                                                    ImageUrl="~/images/dsc1.png" Width="80px" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                            </td>

                                                        </tr>
                                                        <tr class="fountcolor">
                                                            <td align="right">
                                                                <asp:Label ID="lblICSerialNo" runat="server" Text="" Font-Bold="True"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                            <td align="right">
                                                                <asp:Label ID="lblBSerialNo" runat="server" Text="" Font-Bold="True"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>

                                                        </tr>
                                                        <tr class="fountcolor">
                                                            <td align="right">
                                                                <asp:Label ID="lblICHoldername" runat="server" Text="" Font-Bold="false"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                            <td align="right">
                                                                <asp:Label ID="lblBHolderName" runat="server" Text="" Font-Bold="false"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>

                                                        </tr>
                                                        <tr class="fountcolor">
                                                            <td align="right">
                                                                <asp:Label ID="lblICCreatedDate" runat="server" Text="" Font-Bold="false"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                            <td align="right">
                                                                <asp:Label ID="lblBCreatedDate" runat="server" Text="" Font-Bold="false"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>

                                                        </tr>
                                                        <tr class="fountcolor">
                                                            <td align="right">
                                                                <asp:Label ID="lblICIp" runat="server" Text="" Font-Bold="false"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                            <td align="right">
                                                                <asp:Label ID="lblBIP" runat="server" Text="" Font-Bold="false"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>

                                                        </tr>
                                                        <tr class="fountcolor">
                                                            <td colspan="2">
                                                                <br />
                                                            </td>
                                                        </tr>
                                                        <tr class="fountcolor">
                                                            <td align="right">
                                                                <asp:Label ID="Label51" runat="server" Text="प्रदाय केन्द्र प्रभारी के हस्ताक्षर" Font-Bold="true"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                            <td align="right">
                                                                <asp:Label ID="Label50" runat="server" Text="शाखा प्रबंधक(MPWLC) के हस्ताक्षर" Font-Bold="true"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                        </tr>

                                                    </table>

                                                </td>
                                            </tr>


                                            <%--
----------DSC-MPSCSC-----%>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="height: 50px">
                                    <asp:Button class="button button2" Width="150px" Height="30px" ID="Button4"
                                        runat="server" Text="Close" align="Center" />
                                    &nbsp &nbsp &nbsp 
     
                            
                        <input id="Button5" name="Print" type="button" style="height: 30px; width: 150px;"
                            class="button button2" value="Print" onclick="PrintDiv_Actual();" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </asp:Panel>

                <%----------------------------------END Actual Bill ------------------------------------------%>


                <%--------------------------------JSV Detuction----------------------------------------%>

                <cc1:ModalPopupExtender ID="ModalPopupExtender4" runat="server" PopupControlID="pnlepo" TargetControlID="btnepo"
                    CancelControlID="btnepoclose" BackgroundCssClass="modalBackground">
                </cc1:ModalPopupExtender>

                <asp:Panel ID="pnlepo" runat="server" CssClass="modalPopup" Style="width: 850px; height: 620px; display: none" ScrollBars="Vertical">

                    <%--<asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" style="width: 850px; height:650px;" display:none; ScrollBars="Vertical">
                    --%>
                    <div>
                        <table cellspacing="1" cellpadding="3" style="width: 100%;">
                            <tr>
                                <td align="center" width="100%">
                                    <div id="PrintDivEpo">
                                        <table width="100%">
                                            <tr>
                                                <td align="right" style="width: 10px; height: 30px;">
                                                    <asp:Image ID="Image6" runat="server" Height="40px" Width="40px"
                                                        ImageUrl="~/images/mpwlc.png" />
                                                </td>
                                                <td align="center">
                                                    <asp:Label ID="Label6" runat="server" Text="मध्य प्रदेश वेयर हाउसिंग एवं लॉजिस्टिक्स कार्पोरेशन, क्षेत्रीय कार्यालय -" Font-Size="14px" Font-Bold="true"></asp:Label>
                                                    <asp:Label ID="lbleporm" runat="server" Font-Size="14px" Font-Bold="true"></asp:Label><br />
                                                    <asp:Label ID="Label61" runat="server" Text="देयक राशि के विभाजन का विवरण" Font-Size="14px" Font-Bold="true"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" align="Center">&nbsp;
                                                </td>
                                            </tr>

                                            <tr>
                                                <td align="center" width="100%" colspan="2">
                                                    <table width="100%">

                                                        <tr>
                                                            <td align="left">
                                                                <asp:Label ID="Label25" runat="server" Text="शाखा का नाम :- " Font-Size="12px"></asp:Label>&nbsp;
                                            <asp:Label ID="lblepobranch" runat="server" Font-Size="12px"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:Label ID="Label47" runat="server" Text="वेयरहाउस का नाम :- " Font-Size="12px"></asp:Label>&nbsp;
                                            <asp:Label ID="lblepogdwn" runat="server" Font-Size="12px"></asp:Label>
                                                            </td>
                                                        </tr>


                                                        <tr>
                                                            <td align="left">
                                                                <asp:Label ID="Label49" runat="server" Text="माह :-  " Font-Size="12px"></asp:Label>&nbsp;
                                            <asp:Label ID="lblepomonth" runat="server" Font-Size="12px"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:Label ID="Label53" runat="server" Text="जमाकर्ता :- " Font-Size="12px"></asp:Label>&nbsp;
                                            <asp:Label ID="lblepodepos" runat="server" Font-Size="12px"></asp:Label>
                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td align="left">
                                                                <asp:Label ID="Label55" runat="server" Text="बिल क्रमांक :- " Font-Size="12px"></asp:Label>&nbsp;
                                            <asp:Label ID="lblepobillno" runat="server" Font-Size="12px"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:Label ID="Label58" runat="server" Text="स्कंध का नाम :- " Font-Size="12px"></asp:Label>&nbsp;
                                            <asp:Label ID="lblepocmd" runat="server" Font-Size="12px"></asp:Label>
                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td align="left">
                                                                <asp:Label ID="Label60" runat="server" Text="GSTN : 23AADCM7742B1ZU" Font-Size="12px"></asp:Label>&nbsp;
                                                            </td>
                                                            <td align="left">
                                                                <asp:Label ID="Label67" runat="server" Text="Financial Year :- " Font-Size="12px"></asp:Label>
                                                                <asp:Label ID="lblepofinan" runat="server" Text="" Font-Size="12px"></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <asp:Label ID="Label68" runat="server" Text="PAN : AADCM7742B" Font-Size="12px"></asp:Label>&nbsp;
                                                            </td>
                                                            <td align="left">
                                                                <asp:Label ID="Label69" runat="server" Text="Crop Year :- " Font-Size="12px"></asp:Label>&nbsp;
                                            <asp:Label ID="lblepocropyear" runat="server" Text="माह :- " Font-Size="12px"></asp:Label>
                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td style="height: 10px;"></td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" colspan="4">
                                                                <div style="width: 100%;">
                                                                    <img id="Img3" src="../Images/line.png" height="15px" width="100%" alt="" />
                                                                </div>
                                                            </td>
                                                        </tr>

                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" align="center" style="height: 40px;">
                                                    <asp:Label ID="Label14" runat="server" Text="भंडारण शुल्क देयक की मुल राशि : रू " Font-Size="14px"></asp:Label>
                                                    <asp:Label ID="lblepoGrandTotal" runat="server" Text="0" Font-Size="14px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 5px;"></td>
                                            </tr>


                                            <tr>
                                                <td colspan="2" align="center">

                                                    <%------------------Tbody------------------------%>

                                                    <table border="1" style="width: 100%; font-size: 11px;">
                                                        <tbody>
                                                            <tr>
                                                                <th style="width: 5%;">क़.</th>
                                                                <th style="width: 40%;">मद</th>
                                                                <th style="width: 15%;">MPWLC को देय राशि</th>
                                                                <th style="width: 15%;">JVS/Hired/PPP/Other संचालक क़ो देय राशि</th>
                                                            </tr>
                                                        </tbody>
                                                        <tr>
                                                            <td>1</td>
                                                            <td align="left">MPWLC का हिस्सा</td>
                                                            <td align="center">
                                                                <asp:Label ID="lblE_MPWLCHissa" runat="server" Text="0"></asp:Label></td>
                                                            <td align="center">
                                                                <asp:Label ID="lblE_JVSHisaa" runat="server" Text="0"></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td>2</td>
                                                            <td align="left">JVS/Hired/PPP/Other संचालक क़ो देय राशि
                                                                <asp:Label ID="lblE_DeykRashi" runat="server" Text="0"></asp:Label></td>
                                                            <td align="center">
                                                                <asp:Label ID="lblE_MPWLCSanchaluk" runat="server" Text="0"></asp:Label></td>
                                                            <td align="center">
                                                                <asp:Label ID="lblE_JVSSanchaluk" runat="server" Text="0"></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td>2.1</td>
                                                            <td align="left">TDS Amount</td>
                                                            <td align="center">
                                                                <asp:Label ID="lblE_MPWLCTDS" runat="server" Text="0"></asp:Label></td>
                                                            <td align="center">
                                                                <asp:Label ID="lblE_JVSTDS" runat="server" Text="0"></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td>2.2</td>
                                                            <td align="left">Other Detuction </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblE_MPWLCDetuc" runat="server" Text="0"></asp:Label></td>
                                                            <td align="center">
                                                                <asp:Label ID="lblE_JVSDetuc" runat="server" Text="0"></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td>2.3</td>
                                                            <td align="left">देय शुध राशि</td>
                                                            <td align="center">
                                                                <asp:Label ID="lblE_MPWLCDeyak" runat="server" Text="0"></asp:Label></td>
                                                            <td align="center">
                                                                <asp:Label ID="lblE_JVSDeyak" runat="server" Text="0"></asp:Label></td>

                                                        </tr>
                                                        <tr>
                                                            <td></td>
                                                            <td align="left" style="font-weight: bold;">योग</td>
                                                            <td align="center" style="font-weight: bold;">
                                                                <asp:Label ID="lblE_MPWLCNetpay" runat="server" Text="0"></asp:Label></td>
                                                            <td align="center" style="font-weight: bold;">
                                                                <asp:Label ID="lblE_JVSNetPay" runat="server" Text="0"></asp:Label></td>
                                                        </tr>

                                                    </table>

                                                    <%--------------------Tbody---------------------%>
        

        
                                                </td>
                                            </tr>

                                            <tr>
                                                <td align="right" colspan="2" style="width: 100%;">

                                                    <table style="width: 100%;">

                                                        <tr class="fountcolor">

                                                            <td align="right">
                                                                <asp:Image ID="Image3" runat="server" Height="30px" Visible="false"
                                                                    ImageUrl="~/images/dsc1.png" Width="80px" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                            </td>
                                                        </tr>

                                                        <tr class="fountcolor">


                                                            <td align="right">
                                                                <asp:Label ID="lblRMMSei" runat="server" Text="" Font-Bold="True"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>

                                                        </tr>
                                                        <tr class="fountcolor">


                                                            <td align="right">
                                                                <asp:Label ID="lblRMMHN" runat="server" Text="" Font-Bold="false"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>

                                                        </tr>
                                                        <tr class="fountcolor">

                                                            <td align="right">
                                                                <asp:Label ID="lblRMMIP" runat="server" Text="" Font-Bold="false"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>

                                                        </tr>
                                                        <tr class="fountcolor">

                                                            <td align="right">
                                                                <asp:Label ID="lblRMMDate" runat="server" Text="" Font-Bold="false"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>

                                                        </tr>
                                                        <tr class="fountcolor">
                                                            <td>
                                                                <br />
                                                            </td>
                                                        </tr>
                                                        <tr class="fountcolor">

                                                            <td align="right" colspan="2">
                                                                <asp:Label ID="Label66" runat="server" Text="MGR./IA (Actt.)" Font-Bold="true"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td style="height: 15px;"></td>
                                                        </tr>

                                                        <tr>
                                                            <td align="right">
                                                                <asp:Image ID="Image7" runat="server" Height="30px" Visible="false"
                                                                    ImageUrl="~/images/dsc1.png" Width="80px" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                            </td>
                                                        </tr>


                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="lblrmdscserial" runat="server" Text="" Font-Bold="false"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                        </tr>

                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="lblrmdscHN" runat="server" Text="" Font-Bold="false"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                        </tr>

                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="lblrmdscSD" runat="server" Text="" Font-Bold="false"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                        </tr>

                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="lblrmdscip" runat="server" Text="" Font-Bold="false"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                        </tr>

                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="Label10" runat="server" Text="Regional Manager" Font-Bold="true"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                        </tr>



                                                    </table>

                                                </td>
                                            </tr>

                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="height: 50px">
                                    <asp:Button class="button button2" Width="150px" Height="30px" ID="btnepoclose"
                                        runat="server" Text="Close" align="Center" />
                                    &nbsp &nbsp &nbsp 
     
                            
                        <input id="Button8" name="Print" type="button" style="height: 30px; width: 150px;"
                            class="button button2" value="Print" onclick="PrintDiv_Epo();" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </asp:Panel>

                <%----------------------------------------------------------------------------%>
            </div>
        </center>
    </fieldset>
</asp:Content>
