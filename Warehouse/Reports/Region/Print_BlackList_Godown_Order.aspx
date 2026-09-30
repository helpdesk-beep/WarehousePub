<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Print_BlackList_Godown_Order.aspx.cs"
    Inherits="Reports_Region_Print_BlackList_Godown_Order" %>

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

        .square {
            /*  background-color: #2ecc71;*/
            /*width: 200px;*/
            line-height: 40px;
            height: 150px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align: right;">
            <a href="javascript:void(0);" onclick="javascript:window.print();">Print</a>
            <a href="../Region/Black_List_Godown_in_FIFO.aspx">Back</a>
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
                                                        <b><span style="font-size: 20pt;">मध्यप्रदेश वेयरहाउसिंग एण्ड लॉजिस्टिक्स कॉर्पोरेशन</span></b>
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

                </table>
                <br />
                <div style="text-align: center;">
                    <b><span style="font-size: 15pt; margin-left: -950px;">प्रति</span></b>
                    <br />
                    <b><span style="font-size: 15pt; margin-left: -650px;">गोदाम संचालक ,</span></b>
                    <br />
                    <b><span style="font-size: 15pt; margin-left: -400px;">
                       गोदाम- <asp:Label ID="lblwarehousename" runat="server"></asp:Label>
                    </span>
                        <br />
                         <span style="font-size: 15pt; margin-left: -650px;">
                       शाखा -  <asp:Label ID="lblbarnch" runat="server"></asp:Label>
                    </span>
                        <br />
                              <span style="font-size: 15pt; margin-left: -650px;">
                       जिला - <asp:Label ID="lbldistirict1" runat="server"></asp:Label>
                    </span>
                    </b>
                    <br />
                    <br />
                    <br />
                    <b><span style="font-size: 15pt; margin-left: -650px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; विषय:-&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; स्कंध का उठाव प्रभावित होने बावत |</span></b>
                </div>
                <br />
                <div style="font-size: 15pt; margin-left: 200px;">
                    <p>
                        शासन द्वारा निर्धारित "फीफो" अनुसार आपके गोदाम से स्कंध का उठाव निर्धारित था,
                                जिसका निरिक्षण FCI     
                    </p>
                </div>
                <div class="square" style="font-size: 15pt; margin-left: 50px;">
                    <p>
                        द्वारा किये जाने पर
                       <b> <asp:Label ID="lblression" runat="server"></asp:Label></b>
                        होने से स्कंध का उठाव "बाधित" हुआ जो अनुबंध की कंडिका क्रमांक 7.2 का स्पष्ट उल्लंगन हैं, जिसके कारण आपके गोदाम का तत्काल 
                <%--</p>
                <p>--%>
                    किराया रोका जाकर आपके गोदाम को आगामी "3 वर्षो" के लिए "ब्लैकलिस्टेड" किया जा रहा हैं |
                <%--</p>
                <p>--%>
                    कृप्या आगामी 3 कार्य दिवसों में अपना पक्ष प्रस्तुत करे ,अन्यथा एकतरफा कार्यवाही की जावेगी
                    </p>
                </div>


                <br />
                <table width="100%" cellspacing="0" cellpadding="0" class="cssTbl">
                    <tr>
                        <td width="50%" align="left"><br />
                            
                        </td>
                        <td width="50%" align="center" style="font-weight: bold; font-size: 15pt;">
                            <br />
                            <br />
                            अधिकृत प्रतिनिधि<br />
                            MPWLC<br />

                        </td>
                    </tr>
                </table>

                <br />
                <div style="font-size: 15pt; margin-left: 50px;">
                    <p>
                        प्रतिलिपि:-    
                    </p>
                </div>
                <div class="square" style="font-size: 15pt; margin-left: 50px;">
                    <p>
                        1. संचालक ,खाद्य नागरिक आपूर्ति एवं उपभोक्ता संरक्षण ,मध्यप्रदेश भोपाल |
                    </p>
                    <p>
                        2. प्रबंध संचालक ,म.प्र.वेयरहाउसिंग एण्ड लॉजिस्टिक्स कॉर्पोरेशन ,भोपाल |
                    </p>
                    <p>
                       3. क्षेत्रीय प्रबंधक ,म.प्र.वेयरहाउसिंग एण्ड लॉजिस्टिक्स कॉर्पोरेशन
                        <asp:Label ID="lblregionname" Font-Bold="true" runat="server"></asp:Label>
                        को सूचनार्थ एवं आवश्यक कार्यवाही हेतु |
                    </p>
                </div>

                <table width="100%" cellspacing="0" cellpadding="0" class="cssTbl">
                    <tr>
                        <td width="50%" align="left"><br />
                            
                        </td>
                        <td width="50%" align="center" style="font-weight: bold; font-size: 15pt;">
                            <br />
                            <br />
                            अधिकृत प्रतिनिधि<br />
                            MPWLC<br />

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