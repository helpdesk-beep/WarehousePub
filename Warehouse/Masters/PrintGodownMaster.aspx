<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintGodownMaster.aspx.cs" Inherits="Masters_PrintGodownMaster" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <script type="text/javascript" src="http://cdn.nic.in/Samagra/Javascripts/minv1.8.2.js"></script>
    <script src="http://cdn.nic.in/SCSP/scripts/Barcode.js" type="text/javascript"></script>
    <link href="/Assets/css/StyleEU.css" rel="stylesheet" type="text/css" />
    <link href="/Assets/css/Table-MultiDesigns.css" rel="stylesheet" type="text/css" />
    <style>
        table tr {
            background: #f3f2f2;
            font-weight: bold;
            font-family: Cambria;
        }

        .CheckBox {
            width: 15px;
            height: 15px;
            border: 1px solid #000;
            background: white;
        }

        .Header {
            background: #AD3E3E none repeat scroll 0 0;
            color: white;
            padding: 8px;
            width: 50%;
        }

        .HighLightValue {
            color: Green;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align: right;">
            <a href="javascript:void(0);" onclick="javascript:window.print();">Print</a>
            <a href="GodownMaster.aspx">Back</a>
        </div>
        <div style="width: 90%; margin: 0 auto; margin-top: 0px; border: none;">
            <div id="tabPrint" style="border: 1px solid gray;">
                <table width="100%" cellspacing="0" cellpadding="1" border="1px">
                    <tr>
                        <td colspan="2">
                            <table width="100%">
                                <tr>
                                    <td width="100px">
                                        <img src="../images/mpwlc.png" height="70px" />
                                    </td>
                                    <td align="center">
                                        <div style="text-align: center;">
                                            <div style="text-align: center;">
                                                <div>
                                                    <div>
                                                        <b><span style="font-size: 14pt;">कार्यालय म0प्र0 वेयरहाउसिंग & लॉजिस्टिक कार्पोरेशन</span></b><br />
                                                        <b><span>
                                                            <asp:Label ID="lblbranchname" Font-Bold="true" runat="server"></asp:Label>,&nbsp;&nbsp;जिला&nbsp;&nbsp;
                                                            <asp:Label ID="lblDistrict" Font-Bold="true" runat="server"></asp:Label>&nbsp;&nbsp;(म.प्र.)</span></b><br />
                                                        <b><span>ई-मेल:&nbsp;&nbsp;<asp:Label ID="lblEmail" Font-Bold="true" runat="server"></asp:Label>
                                                        </span></b>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td align="left">क्रं./म.प्र.वे.लॉ.का./<%=lblbranchname.Text %>/2021/</td>
                        <td align="right">दिनाँक:-&nbsp;&nbsp;
                            <asp:Label ID="lblDate" Font-Bold="true" runat="server"></asp:Label></td>
                    </tr>
                </table>
                <table width="100%" cellspacing="0" cellpadding="1" border="1px">
                    <tr>
                        <td>जेवीएस पंजीकरण आईडी</td>
                        <td>
                            <asp:Label ID="lblRegID" runat="server" Font-Bold="True" ForeColor="Navy"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>गोडाउन आईडी</td>
                        <td>
                            <asp:Label ID="lblGodownID" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>गोडाउन नंबर</td>
                        <td>
                            <asp:Label ID="lblgodownnum" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>गोडाउन का नाम</td>
                        <td>
                            <asp:Label ID="lblGodownName" runat="server" AutoComplete="off"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>अधिकृत व्यक्ति का नाम</td>
                        <td>
                            <asp:Label ID="lblAPN" runat="server" AutoComplete="off"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>ई-मेल आई डी</td>
                        <td>
                            <asp:Label ID="lblemailid" runat="server" AutoComplete="off"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>मोबाइल नंबर</td>
                        <td>
                            <asp:Label ID="lblmobile" runat="server" AutoComplete="off"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>अधिकतम क्षमता (Qty. in Qtls.kgsgms)</td>
                        <td>
                            <asp:Label ID="lblCapacity" runat="server" AutoComplete="off"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>वैज्ञानिक क्षमता (Qty. in Qtls.kgsgms)</td>
                        <td>
                            <asp:Label ID="lblScientificCapacity" runat="server" AutoComplete="off"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>गोडाउन टाइप</td>
                        <td>
                            <asp:Label ID="lblGodownType" runat="server" AutoComplete="off"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>भण्डारण का प्रकार</td>
                        <td>
                            <asp:Label ID="lblStorageType" runat="server" AutoComplete="off"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>गोदाम परिसर की क्षमता(In M.T.) खरीदी केंद्र स्थापित करने के उद्देश्य से</td>
                        <td>
                            <asp:Label ID="lblPremiseCpt" runat="server" Text="0"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>लाइसेंस नंबर</td>
                        <td>
                            <asp:Label ID="lbllicnum" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>लाइसेंस जारी करने की तारीख</td>
                        <td>
                            <asp:Label ID="lblLicIssueDate" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>लाइसेंस समाप्ति की तारीख</td>
                        <td>
                            <asp:Label ID="lbllicdate" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>पता</td>
                        <td>
                            <asp:Label ID="lbladdress" runat="server" AutoComplete="off"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>अक्षांश</td>
                        <td>
                            <asp:Label ID="lbllatitude" runat="server">0</asp:Label></td>
                    </tr>
                    <tr>
                        <td>देशान्तर</td>
                        <td>
                            <asp:Label ID="lbllongitude" runat="server">0</asp:Label></td>
                    </tr>
                    <tr>
                        <td>खण्ड</td>
                        <td>
                            <asp:Label ID="lblKhand" runat="server">0</asp:Label></td>
                    </tr>
                    <tr>
                        <td>गाँव</td>
                        <td>
                            <asp:Label ID="lblVillage" runat="server">0</asp:Label></td>
                    </tr>
                    <tr>
                        <td>खशरा नंबर</td>
                        <td>
                            <asp:Label ID="lblkhasra" runat="server">0</asp:Label></td>
                    </tr>
                    <tr>
                        <td>राकवा</td>
                        <td>
                            <asp:Label ID="lblrakwa" runat="server">0</asp:Label></td>
                    </tr>
                    <tr>
                        <td>गोदाम पर धर्मकाटा/तौलकाटा उपलब्ध है/नहीं</td>
                        <td>
                            <asp:Label ID="lblWeightmentS" runat="server">0</asp:Label></td>
                    </tr>

                </table>
                <div style="text-align: right; width: 97%;margin-top:10%">
                    मुहर और हस्ताक्षर<br />
                    शाखा प्रबंधक&nbsp;<asp:Label ID="lblBM" runat="server"></asp:Label>
                    <br />
                    म.प्र.वे.लॉ.का.&nbsp;<%=lblbranchname.Text %>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hdnemployeeid" runat="server" Value="0" />
    </form>
</body>
</html>
