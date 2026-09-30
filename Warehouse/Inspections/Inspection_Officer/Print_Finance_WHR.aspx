<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Print_Finance_WHR.aspx.cs"
    Inherits="Inspections_Inspection_officer_Print_Finance_WHR" %>

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
            <a href="../Inspection_Officer/View_and_Print_Finance_WHR.aspx">Back</a>
        </div>
        <div style="width: 90%; margin: 0 auto; margin-top: 0px; border: none;">
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
                </div>
                <table width="100%" cellspacing="0" cellpadding="0" class="cssTbl">
                    <tr>
                        <td>
                            <span style="font-size: 12pt; text-align: center;">प्रमाणित किया जाता है की मेरे द्वारा विभिन्न जमाकर्ताओ हेतु की गई विभिन्न भण्डारगृह रसीद में से निम्न 
                                भण्डारगृह रसीदें उनके सम्मुख दर्शाये गये बैंक /निजी व्यक्तियों के 
                                पास आज दिनांक को रहन रखी गई है जिसकी सुचना 
                                हमें समय-समय पर प्राप्त हुई हैं तथा सम्बंधित नस्ती में उक्त सुचना पत्र सुरक्षित रखा गया है | 
                            </span>
                            <br />

                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                </table>
                <br />
                <table width="100%" cellspacing="0" cellpadding="1" border="1px">                   
                    <tr>
                        <td colspan="5" align="right">
                            <asp:GridView ID="gvPWD" ClientIDMode="Static" runat="server" AutoGenerateColumns="False"
                                ShowHeaderWhenEmpty="True" CssClass="EU_DataTable" Width="100%">
                                <Columns>
                                    <asp:TemplateField HeaderText="क्रमांक">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex + 1%>
                                            <asp:HiddenField ID="hdndavatypeid" Value='<%# Eval("ID") %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="वेयरहाउस रसीद क्रमांक">
                                        <ItemTemplate>
                                            <asp:Label ID="lblWH_Receipt_No" runat="server" Text='<%# Eval("WH_Receipt_No") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="दिनांक">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDate" runat="server" Text='<%# Eval("Date") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="स्कंध">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCommodity_Name" runat="server" Text='<%# Eval("Commodity_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="बोरे">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBags" runat="server" Text='<%# Eval("Bags") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="वजन की मात्रा">
                                        <ItemTemplate>
                                            <asp:Label ID="lblWeight" runat="server" Text='<%# Eval("Weight") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="बैंक /निजी व्यक्ति का नाम जिसके पास रहन रखी गई हो">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBank_Self_Name" runat="server" Text='<%# Eval("Bank_Self_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="लयन पत्र की तिथि">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLetter_Date" runat="server" Text='<%# Eval("Letter_Date") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Middle" />
                                    </asp:TemplateField>


                                   <%-- <asp:TemplateField HeaderText="Dava Remark">
                                        <ItemTemplate>
                                            <div style="text-align: left">
                                                <asp:Label ID="lblInformations" runat="server" Text='<%# Eval("Dava_status_remarks") %>'></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    
                </table>
            </div>
        </div>
        <asp:HiddenField ID="hdnemployeeid" runat="server" Value="0" />
    </form>
</body>
</html>