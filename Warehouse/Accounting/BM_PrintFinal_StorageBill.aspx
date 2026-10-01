<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="BM_PrintFinal_StorageBill.aspx.cs" Inherits="Accounting_BM_PrintFinal_StorageBill" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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

        .pop a {
            text-decoration: none;
        }

        .popup {
            width: 100%;
            height: 98%;
            margin: 0 auto;
            position: fixed;
            z-index: 101;
        }

        .pop {
            min-width: 1200px;
            width: 900px;
            min-height: 150px;
            margin: 0px auto;
            background: #FFFFFF;
            position: relative;
            z-index: 103;
            padding: 10px;
            border-radius: 5px;
            box-shadow: 0 5px 10px #000;
        }

            .pop p {
                color: #555555;
                text-align: justify;
                font-size: medium;
            }

                .pop p a {
                    color: #d91900;
                }

            .pop .x {
                float: right;
                height: 35px;
                left: 22px;
                position: relative;
                top: -20px;
                width: 35px;
            }


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

        .auto-style1 {
            height: 10px;
            width: 558px;
        }

        .auto-style2 {
            width: 558px;
        }
    </style>
    <script type="text/javascript" src="<%= ResolveUrl("~/NEW_CSS/js/jquery-1.12.4.js") %>"></script>
    <link href="../assets/New/css/select2.min.css" rel="stylesheet" />
    <script type="text/javascript" src="../assets/New/js/select2.min.js"></script>
    <script type="text/javascript">
        function initSelect2() {
            $("[id*=ddlBillNo]").select2();
        }
        $(document).ready(function () {
            initSelect2();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset style="width: 800px; border: 1px solid navy; box-shadow: 1px 2px 8px; border-radius: 10px 10px 10px 10px; padding-left: 0px; margin-left: 15px">
        <center>
            <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>--%>
            <div style="background-color: white;">
                <table cellpadding="0" cellspacing="0" style="width: 90%">
                    <tr>
                        <td align="left" style="width: 200px">
                            <asp:Label ID="lblBillNo" runat="server" Font-Size="12px" Font-Bold="true"
                                Text="Select Bill Number" ForeColor="navy"></asp:Label>
                        </td>
                        <td align="left" style="width: 200px">
                            <asp:DropDownList ID="ddlBillNo" runat="server" AutoPostBack="True" Width="200px" Enabled="true"
                                Height="25px" CssClass="tb6" OnSelectedIndexChanged="ddlBillNo_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>

                    <tr>
                        <td colspan="4" align="left"></td>
                    </tr>
                </table>
            </div>
        </center>
    </fieldset>
    <%--    <asp:Panel ID="Panel2" runat="server" CssClass="modalPopup" style="width: 850px; height:620px; display: none;"  ScrollBars="Vertical"--%>
    <asp:Panel ID="Panel2" runat="server" CssClass="modalPopup" Style="width: 850px; height: 550px; box-shadow: 1px 2px 8px; border-radius: 10px 10px 10px 10px;" Visible="false" BorderStyle="Outset">
        <div>
            <table cellspacing="1" cellpadding="3" style="width: 100%;">
                <tr>
                    <td align="center" width="100%">
                        <div id="printActualBill">
                            <table width="100%">
                                <tr>
                                    <td colspan="2" align="center">
                                        <asp:Image ID="Image3" runat="server" ImageUrl="~/images/mpwlc.png" Height="36px" Width="40px" ImageAlign="Left" />
                                        <asp:Label ID="Label7" runat="server" Text="M.P. Warehousing & Logistics Corporation -" Font-Size="14px" Font-Bold="true" ForeColor="#0066cc"></asp:Label>
                                        <asp:Label ID="Label12" runat="server" Font-Size="14px" Font-Bold="true" ForeColor="#0066cc"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center">
                                        <asp:Label ID="Label22" runat="server" Text="STORAGE CHARGES BILL" Font-Bold="true" underline="True" Font-Size="14px" ForeColor="#cc0066"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style1"></td>
                                </tr>

                                <tr>
                                    <td align="left" class="auto-style2">
                                        <asp:Label ID="Label28" runat="server" Text="District    :-" Font-Size="12px"></asp:Label>&nbsp;
                                            <asp:Label ID="lblDistrict" runat="server" Text=":- " Font-Size="12px"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="Label36" runat="server" Text="Branch     :-" Font-Size="12px"></asp:Label>&nbsp;
                                            <asp:Label ID="lblBranch" runat="server" Text=":- " Font-Size="12px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" class="auto-style2">
                                        <asp:Label ID="Label11" runat="server" Text="Billing Date :- " Font-Size="12px"></asp:Label>&nbsp;
                                            <asp:Label ID="lblBillingDate" runat="server" Text=" :- " Font-Size="12px"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="Label18" runat="server" Text="Rate(Per MT) :- " Font-Size="12px"></asp:Label>&nbsp;
                                            <asp:Label ID="lblRate" runat="server" Text=" :- " Font-Size="12px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style1"></td>
                                </tr>
                                <tr>
                                    <td align="left" class="auto-style2">
                                        <asp:Label ID="Label13" runat="server" Text="Storage Agency Details :- " Font-Size="12px" Font-Bold="true" ForeColor="#333399"></asp:Label>&nbsp;
                                           <%-- <asp:Label ID="Label14" runat="server" Text="माह :- " Font-Size="12px"></asp:Label>--%>
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="Label15" runat="server" Text="Depositor Agency Details :- " Font-Size="12px" Font-Bold="true" ForeColor="#333399"></asp:Label>&nbsp;
                                           <%-- <asp:Label ID="Label16" runat="server" Text="माह :- " Font-Size="12px"></asp:Label>--%>
                                    </td>
                                </tr>

                                <tr>
                                    <td align="left" class="auto-style2">
                                        <asp:Label ID="Label23" runat="server" Text="Name :" Font-Size="12px"></asp:Label>&nbsp;
                                            <asp:Label ID="lbldatefromto" runat="server" Text="  MPWLC" Font-Size="12px"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="Label10" runat="server" Text="Name :" Font-Size="12px"></asp:Label>&nbsp;
                                            <asp:Label ID="lbldepositor" runat="server" Text=" MPSCSC" Font-Size="12px"></asp:Label>
                                    </td>
                                </tr>

                                <tr>
                                    <td align="left" class="auto-style2">
                                        <asp:Label ID="Label30" runat="server" Text="PAN :- " Font-Size="12px"></asp:Label>&nbsp;
                                            <asp:Label ID="lblSPAN" runat="server" Text="" Font-Size="12px"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="Label34" runat="server" Text="PAN :- " Font-Size="12px"></asp:Label>&nbsp;
                                            <asp:Label ID="lblcmd_ac" runat="server" Text="XXXXXXXXX" Font-Size="12px"></asp:Label>
                                    </td>
                                </tr>

                                <tr>
                                    <td align="left" class="auto-style2">
                                        <asp:Label ID="Label41" runat="server" Text="GSTN :" Font-Size="12px"></asp:Label>&nbsp;
                                            <asp:Label ID="lblSGST" runat="server" Text="XXXXXXXXX" Font-Size="12px"></asp:Label>&nbsp;
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="Label16" runat="server" Text="GSTN :" Font-Size="12px"></asp:Label>&nbsp;
                                            <asp:Label ID="Label42" runat="server" Text="XXXXXXXXX" Font-Size="12px"></asp:Label>&nbsp;
                                    </td>
                                </tr>


                                <tr>
                                    <td class="auto-style1"></td>
                                </tr>

                                <tr>
                                    <td align="center" colspan="2">
                                        <asp:GridView ID="gvBill" runat="server" AutoGenerateColumns="False" Width="100%" Font-Names="Arial"
                                            DataKeyNames="Bill_Number" BorderStyle="Double" CellPadding="3" CellSpacing="5" ShowFooter="true"
                                            Font-Size="11px" BorderColor="#CCCCCC" OnSelectedIndexChanged="gvBill_SelectedIndexChanged">
                                            <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <Columns>
                                                <asp:BoundField DataField="Bill_Number" HeaderText="Bill Number"></asp:BoundField>
                                                <%--   <asp:BoundField DataField="Billing_Date" HeaderText="Billing Date" >
                                                    </asp:BoundField>                                                   --%>

                                                <asp:BoundField DataField="Commodity" HeaderText="Commodity" ItemStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                                </asp:BoundField>

                                                <%--          <asp:BoundField DataField="Opening_Weight" HeaderText="Commodity" ItemStyle-HorizontalAlign="Left" > 
                                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                                    </asp:BoundField>--%>

                                                <asp:BoundField DataField="Crop_Year" HeaderText="Crop Year" ItemStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                                </asp:BoundField>

                                                <asp:BoundField DataField="Bill_Month" HeaderText="Period" ItemStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="TotalGodown" HeaderText="Warehouses Detail" ItemStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                                </asp:BoundField>
                                                <%--                                                   <asp:HyperLinkField DataTextField="TotalGodown" DataNavigateUrlFields="TotalGodown" ControlStyle-Font-Underline="true" DataNavigateUrlFormatString="Destination.aspx?QS={0}" HeaderText="Warehouses Details" SortExpression="ARecord" />--%>

                                                <asp:BoundField DataField="Charges_Amount" HeaderText="Charges Amount" ItemStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                                </asp:BoundField>

                                                <asp:BoundField DataField="GST_AMT" HeaderText="GST Amount" ItemStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                                </asp:BoundField>

                                                <asp:BoundField DataField="Sup_Charges_Amt" HeaderText="Supervision Charges" ItemStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                                </asp:BoundField>

                                                <asp:BoundField DataField="GST_Sup_Amt" HeaderText="GST on Supervision" ItemStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                                </asp:BoundField>

                                                <asp:BoundField DataField="Net_Amount" HeaderText="Total Bill Amount" ItemStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                                </asp:BoundField>
                                                <%-- <asp:CommandField HeaderText="Show Detail" ShowSelectButton="true" ButtonType="Link"
                                                                ItemStyle-ForeColor="red"><ItemStyle ForeColor="Red"></ItemStyle>
                                                          </asp:CommandField>--%>
                                                <%-- <asp:HyperLinkField DataTextField="TotalGodown" DataNavigateUrlFields="TotalGodown" ControlStyle-Font-Underline="true" DataNavigateUrlFormatString="Destination.aspx?QS={0}" HeaderText="Warehouses Details" SortExpression="ARecord" />--%>
                                            </Columns>
                                            <%--<FooterStyle Font-Bold="True" HorizontalAlign="center" Height="15px" Font-Size="11px" />--%>
                                            <HeaderStyle ForeColor="#C70039" HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="11px" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="height: 30px;">
                                        <asp:Label ID="lblmsg" ForeColor="Red" Font-Bold="true" runat="server" Text="नोट : देयक मे सम्मिलित गोदाम/केप/साइलों का विस्त्रत विवरण देखने के लिए सामने प्रदर्शित लिंक पर क्लिक करे।-->"></asp:Label>
                                        <asp:LinkButton ID="btnDetail" runat="server" ForeColor="#0066cc" Font-Bold="true" Font-Underline="true" Text="" OnClick="btnDetail_Click"></asp:LinkButton>


                                    </td>
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
                                <tr class="fountcolor">
                                    <td colspan="2">
                                        <br />
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
                            runat="server" Text="Close" align="Center" OnClick="Button4_Click" />
                        &nbsp &nbsp &nbsp 
     
                            
                        <input id="Button5" name="Print" type="button" style="height: 30px; width: 150px;"
                            class="button button2" value="Print" onclick="PrintDiv_Actual();" />
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnllogin" class="popup" runat="server">
        <div class="pop" style="background-color: #FFFFCC0">
            <img visible="true" runat="server" src="~/images/close-icon.png" alt="quit" class="x" id="x" />

            <table cellspacing="1" cellpadding="3">
                <tr>
                    <td colspan="2" align="center">
                        <asp:Label ID="Label1" runat="server" Text="GODOWN WISE STORAGE CHARGES BILLS DETAIL" Font-Bold="true" underline="True" Font-Size="14px" ForeColor="#0066cc"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2">
                        <div style="height: 550px; overflow: scroll; width: 1200px;">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="1200px
                                                "
                                Font-Names="Arial"
                                DataKeyNames="Bill_Number" BorderStyle="Double" CellPadding="3" CellSpacing="5" ShowFooter="true"
                                Font-Size="11px" BorderColor="#CCCCCC" OnSelectedIndexChanged="gvBill_SelectedIndexChanged">
                                <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <Columns>

                                    <asp:BoundField DataField="Bill_Number" HeaderText="Bill Number">
                                        <ItemStyle Width="50px" HorizontalAlign="Center"></ItemStyle>

                                    </asp:BoundField>


                                    <asp:BoundField DataField="Commodity" HeaderText="Commodity" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>

                                    <%--          <asp:BoundField DataField="Opening_Weight" HeaderText="Commodity" ItemStyle-HorizontalAlign="Left" > 
                                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                                    </asp:BoundField>--%>

                                    <asp:BoundField DataField="Crop_Year" HeaderText="Crop Year" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="50px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>

                                    <asp:BoundField DataField="Bill_Month" HeaderText="Period" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="50px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Godown" HeaderText="Godown Name">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Godown_Id" HeaderText="Godown ID" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="50px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>

                                    <asp:BoundField DataField="Charges_Amount" HeaderText="Charges Amount" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="50px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>

                                    <asp:BoundField DataField="GST_AMT" HeaderText="GST Amount" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="50px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>

                                    <asp:BoundField DataField="Sup_Charges_Amt" HeaderText="Supervision Charges" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="50px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>

                                    <asp:BoundField DataField="GST_Sup_Amt" HeaderText="GST on Supervision" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="50px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>

                                    <asp:BoundField DataField="Net_Amount" HeaderText="Total Bill Amount" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="50px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <%--                                                  <asp:HyperLinkField DataNavigateUrlFields="TotalGodown" ControlStyle-Font-Underline="true" DataNavigateUrlFormatString="Destination.aspx?QS={0}" HeaderText="Warehouses Details" SortExpression="ARecord" />--%>
                                    <asp:CommandField HeaderText="Show Detail" ShowSelectButton="true" ButtonType="Link"
                                        ItemStyle-ForeColor="red">
                                        <ItemStyle ForeColor="Red"></ItemStyle>
                                        <ItemStyle Width="50px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:CommandField>

                                    <asp:BoundField DataField="JVS_Bill" HeaderText="JVS Bill" ItemStyle-HorizontalAlign="Left"></asp:BoundField>
                                </Columns>
                                <FooterStyle Font-Bold="True" HorizontalAlign="center" Height="15px" Font-Size="11px" ForeColor="#cc0066" />
                                <HeaderStyle ForeColor="#C70039" HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="11px" />
                            </asp:GridView>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="height: 30px;">
                        <asp:Label ID="Label2" ForeColor="Red" Font-Bold="true" runat="server" Text="नोट : देयक का विस्त्रत विवरण देखने के लिए बिल के Show Detail वाले कॉलम मे प्रदर्शित लिंक Select पर क्लिक करे।"></asp:Label>


                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label runat="server" ID="new"></asp:Label></td>
                </tr>

            </table>
        </div>
    </asp:Panel>
    <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="new" BackgroundCssClass="popup" PopupControlID="pnllogin" CancelControlID="x">
    </asp:ModalPopupExtender>

    <asp:AnimationExtender ID="popupAnimation" runat="server" TargetControlID="new">
        <Animations>
                <OnClick>
         <Parallel AnimationTarget="pnllogin" Duration="0.4" Fps="10">
            <FadeIn />
          </Parallel>
       </OnClick>
        </Animations>
    </asp:AnimationExtender>
</asp:Content>

