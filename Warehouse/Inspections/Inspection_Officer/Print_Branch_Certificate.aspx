<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Print_Branch_Certificate.aspx.cs"
    Inherits="Inspections_Inspection_officer_Print_Branch_Certificate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
            <a href="../Inspection_Officer/Print_Order.aspx">Back</a>
        </div>
        <div style="width: 100%; margin: 0 auto; margin-top: 0px; border: none;">
            <div id="tabPrint" style="border: 1px solid gray;">
                <table width="100%" cellspacing="0" cellpadding="1" border="1px">
                    <tr>
                        <td colspan="2">
                            <table width="100%">
                                <tr>
                                    <td width="100px">
                                        <img src="../../images/mpwlc.png" height="70px" />
                                    </td>
                                    <td align="center">
                                        <div style="text-align: center;">
                                            <div style="text-align: center;">
                                                <div>
                                                    <div>
                                                        <b><span style="font-size: 12pt;">Madhya Pradesh Warehousing & Logistics Corporation</span></b>
                                                    </div>
                                                </div>
                                                <hr />
                                                <br />
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 50%">Branch :
                        <asp:Label ID="lblbranchname" Font-Bold="true" runat="server"></asp:Label>
                        </td>

                    </tr>

                </table>
                <br />
                <div style="text-align: center;">
                    <b><span style="font-size: 12pt; text-align: center;">शाखा प्रबंधक का प्रमाण पत्र</span></b><br />
                    <br />
                    <b><span style="font-size: 12pt; text-align: center; text-underline-position: below;">(शाखा पर निरिक्षण की दिनांक तक जमा करने हेतु प्राप्त स्कंध के संबंध में प्रमाण पत्र)</span></b>
                </div>
                <br />
                <table width="100%" cellspacing="0" cellpadding="0" class="cssTbl" >
                    <tr>
                        <td>
                            <span style="font-size: 12pt; text-align: center;">प्रमाणित किया जाता है कि आज दिनांक 
                                <asp:TextBox ID="lbltdate" runat="server"></asp:TextBox>
                                तक विभिन्न जमाकर्ताओं से जो स्कंध                          
                                जमा करने हेतु इस भण्डारण में प्राप्त हुआ है ,उन सभी स्कंधों की भण्डारगृह रसीदे (W.H.R)
                                 मैने जारी कर दी है तथा उनका इन्द्राज शाखा के S.R./D.I. में तथा अन्य संबंधित पंजी (Register)
                                 में कर लिया गया है |मेरी जानकारी के अनुसार आज दिनांक 
                                <asp:TextBox ID="lbldate" runat="server"></asp:TextBox>तक समस्त जमा स्कंधों की
                                  भण्डारगृह रसीदे (W.H.R) जारी कर दी गई हैं |
                            </span>
                            <br />

                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                </table>

                <br />
                <table width="100%" cellspacing="0" cellpadding="0" class="cssTbl" >
                    <tr>
                        <td width="50%" align="left">Date:<br />
                            Place:
                        </td>
                        <td width="50%" align="center" style="font-weight: bold">
                            <br />
                            <br />
                            शाखा प्रबंधक<br />
                            म.प्र. वेयरहाउसिंग एंड लॉजिस्टिक कार्पोरेशन<br />
                            <asp:Label ID="lblbranch" Font-Bold="true" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table width="100%" cellspacing="0" cellpadding="1" border="1px">
                    </table>
                <br />
                <table width="100%" cellspacing="0" cellpadding="1" border="1px">
                    <tr>
                        <td colspan="2">
                            <table width="100%">
                                <tr>
                                    <td width="100px">
                                        <img src="../../images/mpwlc.png" height="70px" />
                                    </td>
                                    <td align="center">
                                        <div style="text-align: center;">
                                            <div style="text-align: center;">
                                                <div>
                                                    <div>
                                                        <b><span style="font-size: 12pt;">Madhya Pradesh Warehousing & Logistics Corporation</span></b>
                                                    </div>
                                                </div>
                                                <hr />
                                                <br />
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 50%">Branch :
                        <asp:Label ID="lblbranchname2" Font-Bold="true" runat="server"></asp:Label>
                        </td>

                    </tr>

                </table>
                <br />
                <div style="text-align: center;">
                    <b><span style="font-size: 12pt; text-align: center;">शाखा प्रबंधक का प्रमाण पत्र</span></b><br />
                    <br />
                    <b><span style="font-size: 12pt; text-align: center; text-underline-position: below;">(शाखा पर निरीक्षण की दिनांक तक भुगतान किये गये समस्त स्कंध के संबंध )</span></b>
                </div>
                <br />
                <table width="100%" cellspacing="0" cellpadding="0" class="cssTbl" >
                    <tr>
                        <td>
                            <span style="font-size: 12pt; text-align: center;">प्रमाणित किया जाता है कि आज दिनांक 
                                <asp:TextBox ID="lbldatet" runat="server"></asp:TextBox>
                                तक विभिन्न जमाकर्ताओं का जो स्कंध वेयरहाउस से भुगतान (Delivery) का इन्द्राज संबंधित वेयर हाउस रसीदों (W.H.R.) 
                                की मूल प्रति एवं कार्यालयीन प्रति में मेरे द्वारा इन्द्राज कर दिया गया हैं | समस्त डिलीवरी आर्डर (Delivery Order) 
                                का इन्द्राज भी शाखा के S.R./D.I.में कर लिया गया है | आज दिनांक 
                                <asp:TextBox ID="lbldatett" runat="server"></asp:TextBox>तक कोई भी डिलीवरी स्कंधक का इन्द्राज करना शेष नहीं है |
                            </span>
                            <br />

                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                </table>

                <br />
                <table width="100%" cellspacing="0" cellpadding="0" class="cssTbl" >
                    <tr>
                        <td width="50%" align="left">Date:<br />
                            Place:
                        </td>
                        <td width="50%" align="center" style="font-weight: bold">
                            <br />
                            <br />
                            शाखा प्रबंधक<br />
                            म.प्र. वेयरहाउसिंग एंड लॉजिस्टिक कार्पोरेशन<br />
                             <asp:Label ID="lblbranch2" Font-Bold="true" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <asp:HiddenField ID="hdnemployeeid" runat="server" Value="0" />
    </form>
</body>
</html>
</script> 