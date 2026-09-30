<%@ Page Title="" Language="C#" MasterPageFile="~/Inspections/Masters/Inspection_Officer.master" AutoEventWireup="true" CodeFile="View_Annaxures_Branch_Wise_with_Remark.aspx.cs" Inherits="Inspections_Inspection_Officer_View_Annaxures_Branch_Wise_with_Remark" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css">
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.8.3/themes/base/jquery-ui.css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="http://code.jquery.com/ui/1.8.3/jquery-ui.js"></script>
    <style type="text/css">
        .wrap {
            margin: 0 auto;
            width: 960px;
            -moz-box-shadow: 0px 5px 23px #000;
            -webkit-box-shadow: 0px 5px 23px #000;
            box-shadow: 0px 5px 23px #000;
        }

        input.submit {
            color: #fff;
            padding: 7px 10px;
            border: 0;
            font-weight: bold;
            background: #777;
            border-radius: 25px;
        }

        input.text {
            border: 2px solid rgb(173, 204, 204);
            height: 20px;
            width: 223px;
            font-size: 16px;
            box-shadow: 0px 0px 27px rgb(204, 204, 204) inset;
            transition: 500ms all ease;
            padding: 3px 3px 3px 3px;
        }
    </style>
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

        .button3 {
            background-color: white;
            color: black;
            border: 2px solid #f44336;
        }

            .button3:hover {
                background-color: #f44336;
                color: white;
            }

        .button6 {
            background-color: white;
            color: black;
            border: 2px solid #008CBA;
        }

            .button6:hover {
                background-color: #008CBA;
                color: white;
            }

        .style1 {
            height: 30px;
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
            padding: 0;
        }

            .modalPopup .header {
                background-color: #D69758;
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
            width: 123px;
        }
    </style>
    <script type="text/javascript">
        function printGrid() {
            var gridData = document.getElementById('<%= GrdOfficerPreviousInsp.ClientID %>');
            var divContents = document.getElementById("GFG").innerHTML;
            var windowUrl = 'about:blank';

            //set print document name for gridview
            var uniqueName = new Date();
            var windowName = 'Print_' + uniqueName.getTime();

            var prtWindow = window.open(windowUrl, windowName,
                'left=100,top=100,right=100,bottom=100,width=700,height=500');
            prtWindow.document.write('<html><head></head>');
            prtWindow.document.write('<body style="background:none !important">');
            prtWindow.document.write(gridData.outerHTML);
            prtWindow.document.write('</body></html>');
            prtWindow.document.close();
            prtWindow.focus();
            prtWindow.print();
            prtWindow.close();
        }
    </script>

    <div style="background-color: #FDFAF7; width: 100%;">
        <table align="center" style="width: 100%; border: #008CBA; border-style: solid; border-width: 0px;">

            <tr>

                <td style="text-align:right">
                    <asp:Label ID="Label12" runat="server" Text="Financial Year : "></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlfinancialyear" runat="server" AutoPostBack="true" Width="222px"
                        Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6" OnSelectedIndexChanged="ddlfinancialyear_SelectedIndexChanged">
                        <asp:ListItem Value="0">--Select Financial Year--</asp:ListItem>
                    
                    </asp:DropDownList>
                </td>
            </tr>

        </table>
        <div id="divshow" runat="server" visible="true">
            <div class="card-body">
                <%--<asp:Button ID="btnExportToWord" CssClass="btnMargin btn btn-outline-primary rounded-0" runat="server" Text="ExportToWord"  />--%>
                <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-warning" Text="Print" OnClientClick="printGrid()" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
               <span style="color:red;"> रिकार्ड अनुसार :-  <asp:Label ID="lblOnlineRecord" runat="server"></asp:Label></span>
                
            </div>
            <asp:GridView runat="server" ID="GrdOfficerPreviousInsp" ShowFooter="false"
                AutoGenerateColumns="false" CssClass="Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
                <Columns>
                    <asp:TemplateField HeaderText="क्रमांक">
                        <ItemTemplate>
                            <%#Container.DataItemIndex+1%>
                        </ItemTemplate>
                        <ItemStyle Width="1%" />
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="जिले का नाम">
                        <ItemTemplate>
                            <asp:Label ID="lblStack_ID" runat="server" Text='<%# Eval("District_Name") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="शाखा का नाम">
                        <ItemTemplate>
                            <asp:Label ID="lblStack_Name" runat="server" Text='<%# Eval("DepotName") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>
                    <%--uncomment Savan--%> 
                    <asp:TemplateField HeaderText="रिकार्ड अनुसार">
                        <ItemTemplate>
                            <asp:Label ID="lblAVl_Bags" runat="server" Text='<%# Eval("AVl_Bags") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>
                    <%--End--%>

                    <asp:TemplateField HeaderText="निरीक्षण में पाए गए बोरे">
                        <ItemTemplate>
                            <asp:Label ID="lblAVl_Bags_in_PV" runat="server" Text='<%# Eval("AVl_Bags_in_PV") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Spilage_Bags">
                        <ItemTemplate>
                            <asp:Label ID="lblSpilage_Bags" runat="server" Text='<%# Eval("Spilage_Bags") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="निरीक्षण में पाये गये ज्यादा बोरे">
                        <ItemTemplate>
                            <asp:Label ID="lblTotalAvlBags2" runat="server" Text='<%# Eval("TotalAvlBags2") %>'></asp:Label>
                        </ItemTemplate>

                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="निरीक्षण में पाये गये कम बोरे">
                        <ItemTemplate>
                            <asp:Label ID="lblTotalAvlBags" runat="server" Text='<%# Eval("TotalAvlBags") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <%--<FooterStyle BackColor="#719cb6" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" />--%>
                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" Height="20px" Font-Size="10pt" />
                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#F7ECDD" Font-Bold="True" ForeColor="#cb4e48" HorizontalAlign="center"
                    Height="20px" Font-Size="10pt" />
                <AlternatingRowStyle BackColor="#eeeeee" />
            </asp:GridView>
            <div id="divremarkA" runat="server" visible="false">
                <h5 style="color: red;">Annexure A में कोई त्रुटि पाई जाती हैं या बोरे काम /ज्यादा दिख रहे हैं तो आप उसका काम/ज्यादा होने का कारण यहाँ लिख सकते हैं यदि आपको Annexure A से सम्बंधित और कोई कारण भी लिखना हैं तो उसे भी यहाँ लिख सकते हैं</h5>

                <table align="center" style="width: 100%; border: #008CBA; border-style: solid; border-width: 0px;">
                    <tr>
                        <td style="font-weight: bold; text-align: right;" class="auto-style1">
                            <asp:Label ID="Label20" runat="server" Text="Remarks : "></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <center>
                                <asp:Button CssClass="btn btn-warning" ID="btnA" runat="server" Text="Submit Annaxure A"
                                    TabIndex="11" Width="150px" Height="30px" ValidationGroup="A" OnClick="btnA_Click"></asp:Button>

                            </center>
                        </td>
                    </tr>
                </table>
            </div>

            <asp:GridView runat="server" ID="grdannaxureB" ShowFooter="false"
                AutoGenerateColumns="false" CssClass="Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
               >
                <Columns>
                    <asp:TemplateField HeaderText="क्रमांक">
                        <ItemTemplate>
                            <%#Container.DataItemIndex+1%>
                            
                        </ItemTemplate>
                        <ItemStyle Width="1%" />
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <%-- <asp:TemplateField HeaderText="Region">
                        <ItemTemplate>
                            <asp:Label ID="lblGodown_Name" runat="server" Text='<%# Eval("Regionnm") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="जिले का नाम">
                        <ItemTemplate>
                            <asp:Label ID="lblStack_ID" runat="server" Text='<%# Eval("District_Name") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="शाखा का नाम">
                        <ItemTemplate>
                            <asp:Label ID="lblStack_Name" runat="server" Text='<%# Eval("DepotName") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="रिकार्ड अनुसार">
                        <ItemTemplate>
                            <asp:Label ID="lblAvailable_Bags" runat="server" Text='<%# Eval("Available_Bags") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="निरीक्षण में पाए गए बोरे">
                        <ItemTemplate>
                            <asp:Label ID="lblAvailable_Bags_as_Per_PV" runat="server" Text='<%# Eval("Available_Bags_as_Per_PV") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Spilage_Bags">
                        <ItemTemplate>
                            <asp:Label ID="lblSpillage_bags" runat="server" Text='<%# Eval("Spillage_bags") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="निरीक्षण में पाये गये ज्यादा बोरे">
                        <ItemTemplate>
                            <asp:Label ID="lblTotalAvlBags2" runat="server" Text='<%# Eval("TotalAvlBags2") %>'></asp:Label>
                        </ItemTemplate>

                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="निरीक्षण में पाये गये कम बोरे">
                        <ItemTemplate>
                            <asp:Label ID="lblTotalAvlBags" runat="server" Text='<%# Eval("TotalAvlBags") %>'></asp:Label>

                        </ItemTemplate>
                    </asp:TemplateField>
                    
                </Columns>
                <%--<FooterStyle BackColor="#719cb6" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" />--%>
                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" Height="20px" Font-Size="10pt" />
                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#F7ECDD" Font-Bold="True" ForeColor="#cb4e48" HorizontalAlign="center"
                    Height="20px" Font-Size="10pt" />
                <AlternatingRowStyle BackColor="#eeeeee" />
            </asp:GridView>
            <div id="divRemarkB" runat="server" visible="false">
                <h5 style="color: red;">Annexure B में कोई त्रुटि पाई जाती हैं या बोरे काम /ज्यादा दिख रहे हैं तो आप उसका काम/ज्यादा होने का कारण यहाँ लिख सकते हैं यदि आपको Annexure B से सम्बंधित और कोई कारण भी लिखना हैं तो उसे भी यहाँ लिख सकते हैं</h5>

                <table align="center" style="width: 100%; border: #008CBA; border-style: solid; border-width: 0px;">
                    <tr>
                        <td style="font-weight: bold; text-align: right;" class="auto-style1">
                            <asp:Label ID="Label1" runat="server" Text="Remarks : "></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtremarkB" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <center>
                                <asp:Button CssClass="btn btn-warning" ID="btnB" runat="server" Text="Submit Annaxure B"
                                    TabIndex="11" Width="150px" Height="30px" ValidationGroup="A" OnClick="btnB_Click"></asp:Button>

                            </center>
                        </td>
                    </tr>
                </table>
            </div>
          
            <asp:GridView runat="server" ID="GrdAnnaxureC" ShowFooter="false"
                AutoGenerateColumns="false" CssClass="Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
                <Columns>
                    <asp:TemplateField HeaderText="क्रमांक">
                        <ItemTemplate>
                            <%#Container.DataItemIndex+1%>
                        </ItemTemplate>
                        <ItemStyle Width="1%" />
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <%-- <asp:TemplateField HeaderText="Region">
                        <ItemTemplate>
                            <asp:Label ID="lblGodown_Name" runat="server" Text='<%# Eval("Regionnm") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="जिले का नाम">
                        <ItemTemplate>
                            <asp:Label ID="lblStack_ID" runat="server" Text='<%# Eval("District_Name") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="शाखा का नाम">
                        <ItemTemplate>
                            <asp:Label ID="lblStack_Name" runat="server" Text='<%# Eval("DepotName") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="रिकार्ड अनुसार">
                        <ItemTemplate>
                            <asp:Label ID="lblAvailable_Bags" runat="server" Text='<%# Eval("Available_Bags") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="बैंक में रहन रखी गई रसीदों की संख्या">
                        <ItemTemplate>
                            <asp:Label ID="lblPlaceinbank" runat="server" Text='<%# Eval("Placeinbank") %>'></asp:Label>

                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
                <%--<FooterStyle BackColor="#719cb6" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" />--%>
                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" Height="20px" Font-Size="10pt" />
                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#F7ECDD" Font-Bold="True" ForeColor="#cb4e48" HorizontalAlign="center"
                    Height="20px" Font-Size="10pt" />
                <AlternatingRowStyle BackColor="#eeeeee" />
            </asp:GridView>
            <div id="divRemarkC" runat="server" visible="false">
                <h5 style="color: red;">Annexure C में कोई त्रुटि पाई जाती हैं या बोरे काम /ज्यादा दिख रहे हैं तो आप उसका काम/ज्यादा होने का कारण यहाँ लिख सकते हैं यदि आपको Annexure C से सम्बंधित और कोई कारण भी लिखना हैं तो उसे भी यहाँ लिख सकते हैं</h5>

                <table align="center" style="width: 100%; border: #008CBA; border-style: solid; border-width: 0px;">
                    <tr>
                        <td style="font-weight: bold; text-align: right;" class="auto-style1">
                            <asp:Label ID="Label2" runat="server" Text="Remarks : "></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtremarkc" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <center>
                                <asp:Button CssClass="btn btn-warning" ID="btnc" runat="server" Text="Submit Annaxure C"
                                    TabIndex="11" Width="150px" Height="30px" ValidationGroup="A" OnClick="btnc_Click"></asp:Button>

                            </center>
                        </td>
                    </tr>
                </table>
            </div>

            <asp:GridView runat="server" ID="Grddagnapatrak" ShowFooter="false"
                AutoGenerateColumns="false" CssClass="Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
                <Columns>
                    <asp:TemplateField HeaderText="क्रमांक">
                        <ItemTemplate>
                            <%#Container.DataItemIndex+1%>
                        </ItemTemplate>
                        <ItemStyle Width="1%" />
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <%-- <asp:TemplateField HeaderText="Region">
                        <ItemTemplate>
                            <asp:Label ID="lblGodown_Name" runat="server" Text='<%# Eval("Regionnm") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="जिले का नाम">
                        <ItemTemplate>
                            <asp:Label ID="lblStack_ID" runat="server" Text='<%# Eval("District_Name") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="शाखा का नाम">
                        <ItemTemplate>
                            <asp:Label ID="lblStack_Name" runat="server" Text='<%# Eval("DepotName") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="रिकार्ड अनुसार">
                        <ItemTemplate>
                            <asp:Label ID="lblTotal_Bags" runat="server" Text='<%# Eval("Total_Bags") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Spillage bag">
                        <ItemTemplate>
                            <asp:Label ID="lblSpillage_bag" runat="server" Text='<%# Eval("Spillage_bag") %>'></asp:Label>

                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
                <%--<FooterStyle BackColor="#719cb6" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" />--%>
                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" Height="20px" Font-Size="10pt" />
                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#F7ECDD" Font-Bold="True" ForeColor="#cb4e48" HorizontalAlign="center"
                    Height="20px" Font-Size="10pt" />
                <AlternatingRowStyle BackColor="#eeeeee" />
            </asp:GridView>
            <div id="divGadnaPAtrak" runat="server" visible="false">
                <h5 style="color: red;">गड़ना पत्रक में कोई त्रुटि पाई जाती हैं या बोरे काम /ज्यादा दिख रहे हैं तो आप उसका काम/ज्यादा होने का कारण यहाँ लिख सकते हैं यदि आपको गड़ना पत्रक से सम्बंधित और कोई कारण भी लिखना हैं तो उसे भी यहाँ लिख सकते हैं</h5>

                <table align="center" style="width: 100%; border: #008CBA; border-style: solid; border-width: 0px;">
                    <tr>
                        <td style="font-weight: bold; text-align: right;" class="auto-style1">
                            <asp:Label ID="Label4" runat="server" Text="Remarks : "></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtgadnapatrak" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <center>
                                <asp:Button CssClass="btn btn-warning" ID="btngadnapatrak" runat="server" Text="Submit Gadna Patrak"
                                    TabIndex="11" Width="150px" Height="30px" ValidationGroup="A" OnClick="btngadnapatrak_Click"></asp:Button>

                            </center>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>

</asp:Content>

