<%@ Page Title="" Language="C#" MasterPageFile="~/Inspections/Masters/State.master" AutoEventWireup="true" CodeFile="View_Insp.aspx.cs" Inherits="Inspections_Reports_View_Branch_Wise_Ann_Counting_Sheat" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css">
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.8.3/themes/base/jquery-ui.css" />
    <script type="text/javascript" src="<%= ResolveUrl("~/NEW_CSS/js/jquery-1.12.4.js") %>"></script>
    <script type="text/javascript" src="<%= ResolveUrl("~/NEW_CSS/js/jquery-ui.js") %>"></script>
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
    </style>

    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=divshow.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head><title>DIV Contents</title>');
            printWindow.document.write('</head><body >');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 500);
            return false;
        }
    </script>
    <div style="background-color: #FDFAF7; width: 100%;">
        <asp:Button ID="Button1" runat="server" Text="Print" OnClientClick="return PrintPanel();" CssClass="btn btn-warning" />

        <div id="divshow" runat="server" visible="true" class="widget-content">

            <div>
                <span>शाखा:&nbsp;
                    <asp:Label ID="lblInsBranch" runat="server"></asp:Label>&nbsp; का &nbsp;
                    <asp:Label ID="lblInstype" runat="server"></asp:Label> &nbsp;&nbsp;
                    श्री :&nbsp;
                    <asp:Label ID="lblname" runat="server"></asp:Label>
                    &nbsp;&nbsp; पद :<asp:Label ID="lbldesignation" runat="server"></asp:Label>&nbsp;&nbsp;
                   निरीक्षण अधिकारी की शाखा:<asp:Label ID="lblbranchname" runat="server"></asp:Label>&nbsp;&nbsp; 
                    के द्वारा क्षेत्रीय प्रबंधक का आर्डर क्र.:<asp:Label ID="lblorderno" runat="server"></asp:Label>
                    &nbsp;&nbsp;आर्डर दिनांक :<asp:Label ID="lblorderdate" runat="server"></asp:Label>&nbsp;&nbsp; से
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    दिनांक &nbsp;&nbsp; <asp:Label ID="todate" runat="server"></asp:Label>&nbsp;&nbsp; तक &nbsp;&nbsp;<asp:Label ID="lblInstype2" runat="server"></asp:Label> &nbsp;&nbsp; किया गया :&nbsp;
                    
                </span>
            </div>

            <br />
            <asp:GridView runat="server" ID="GrdOfficerPreviousInsp" ShowFooter="false"
                AutoGenerateColumns="false" CssClass="Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
                OnRowCommand="GrdOfficerPreviousInsp_RowCommand">
                <Columns>
                    <asp:TemplateField HeaderText="क्रमांक">
                        <ItemTemplate>
                            <%#Container.DataItemIndex+1%>
                            <asp:HiddenField runat="server" ID="hdnbranchid" Value='<%# Eval("Branch_Id") %>' />
                            <asp:HiddenField runat="server" ID="hdnqauterid" Value='<%# Eval("Inspection_Quarter") %>' />
                            <asp:HiddenField runat="server" ID="hdnverificationid" Value='<%# Eval("Verification_Type") %>' />
                            <asp:HiddenField runat="server" ID="hdnempid" Value='<%# Eval("Employee_ID") %>' />
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
                    <asp:TemplateField HeaderText="रिकार्ड अनुसार">
                        <ItemTemplate>
                            <asp:Label ID="lblDepositor_Name" runat="server" Text='<%# Eval("AVl_Bags") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="निरीक्षण में पाए गए बोरे">
                        <ItemTemplate>
                            <asp:Label ID="lblcommodity" runat="server" Text='<%# Eval("AVl_Bags_in_PV") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Spilage_Bags">
                        <ItemTemplate>
                            <asp:Label ID="lblTotal_Bags" runat="server" Text='<%# Eval("Spilage_Bags") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="निरीक्षण में पाये गये ज्यादा बोरे">
                        <ItemTemplate>
                            <asp:Label ID="lblSpillage_bag" runat="server" Text='<%# Eval("TotalAvlBags2") %>'></asp:Label>
                        </ItemTemplate>

                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="निरीक्षण में पाये गये कम बोरे">
                        <ItemTemplate>
                            <asp:Label ID="lblclassifi" runat="server" Text='<%# Eval("TotalAvlBags") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                   <%-- <asp:TemplateField HeaderText="View Details Reports">
                        <ItemTemplate>
                            <asp:Button ID="btnEdit" Text="View Details" runat="server" CssClass="btneditstyle" CommandName="EditRow" CommandArgument='<%# Container.DataItemIndex %>' />
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                </Columns>
                <%--<FooterStyle BackColor="#719cb6" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" />--%>
                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" Height="20px" Font-Size="10pt" />
                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#F7ECDD" Font-Bold="True" ForeColor="#cb4e48" HorizontalAlign="center"
                    Height="20px" Font-Size="10pt" />
                <AlternatingRowStyle BackColor="#eeeeee" />
            </asp:GridView>

            <br />
            <div id="divremarkA" runat="server" visible="true">
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
                </table>
            </div>
            <br />

            <asp:GridView runat="server" ID="grdannaxureB" ShowFooter="false"
                AutoGenerateColumns="false" CssClass="Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
                OnRowCommand="grdannaxureB_RowCommand">
                <Columns>
                    <asp:TemplateField HeaderText="क्रमांक">
                        <ItemTemplate>
                            <%#Container.DataItemIndex+1%>
                            <asp:HiddenField runat="server" ID="hdnbranchid" Value='<%# Eval("Branch_Id") %>' />
                            <asp:HiddenField runat="server" ID="hdnqauterid" Value='<%# Eval("Inspection_Quarter") %>' />
                            <asp:HiddenField runat="server" ID="hdnverificationid" Value='<%# Eval("Verification_Type") %>' />
                            <asp:HiddenField runat="server" ID="hdnempid" Value='<%# Eval("Employee_ID") %>' />
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
                            <asp:Label ID="lblDepositor_Name" runat="server" Text='<%# Eval("Available_Bags") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="निरीक्षण में पाए गए बोरे">
                        <ItemTemplate>
                            <asp:Label ID="lblcommodity" runat="server" Text='<%# Eval("Available_Bags_as_Per_PV") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Spilage_Bags">
                        <ItemTemplate>
                            <asp:Label ID="lblTotal_Bags" runat="server" Text='<%# Eval("Spillage_bags") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="निरीक्षण में पाये गये ज्यादा बोरे">
                        <ItemTemplate>
                            <asp:Label ID="lblSpillage_bag" runat="server" Text='<%# Eval("TotalAvlBags2") %>'></asp:Label>
                        </ItemTemplate>

                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="निरीक्षण में पाये गये कम बोरे">
                        <ItemTemplate>
                            <asp:Label ID="lblclassifi" runat="server" Text='<%# Eval("TotalAvlBags") %>'></asp:Label>

                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:TemplateField HeaderText="View Details Reports">
                        <ItemTemplate>
                            <asp:Button ID="btnEdit" Text="View Details" runat="server" CssClass="btneditstyle" CommandName="EditRow" CommandArgument='<%# Container.DataItemIndex %>' />
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                </Columns>
                <%--<FooterStyle BackColor="#719cb6" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" />--%>
                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" Height="20px" Font-Size="10pt" />
                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#F7ECDD" Font-Bold="True" ForeColor="#cb4e48" HorizontalAlign="center"
                    Height="20px" Font-Size="10pt" />
                <AlternatingRowStyle BackColor="#eeeeee" />
            </asp:GridView>
            <br />
            <div id="divRemarkB" runat="server" visible="true">
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
                   
                </table>
            </div>
            <br />

            <asp:GridView runat="server" ID="GrdAnnaxureC" ShowFooter="false"
                AutoGenerateColumns="false" CssClass="Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
                OnRowCommand="GrdAnnaxureC_RowCommand">
                <Columns>
                    <asp:TemplateField HeaderText="क्रमांक">
                        <ItemTemplate>
                            <%#Container.DataItemIndex+1%>
                            <asp:HiddenField runat="server" ID="hdnbranchid" Value='<%# Eval("Branch_Id") %>' />
                            <asp:HiddenField runat="server" ID="hdnqauterid" Value='<%# Eval("Inspection_Quarter") %>' />
                            <asp:HiddenField runat="server" ID="hdnverificationid" Value='<%# Eval("Verification_Type") %>' />
                            <asp:HiddenField runat="server" ID="hdnempid" Value='<%# Eval("Employee_ID") %>' />
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
                    <asp:TemplateField HeaderText="रिकार्ड अनुसार">
                        <ItemTemplate>
                            <asp:Label ID="lblDepositor_Name" runat="server" Text='<%# Eval("Available_Bags") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="बैंक में रहन रखी गई रसीदों की संख्या">
                        <ItemTemplate>
                            <asp:Label ID="lblclassifi" runat="server" Text='<%# Eval("Placeinbank") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%-- <asp:TemplateField HeaderText="View Details Reports">
                        <ItemTemplate>
                            <asp:Button ID="btnEdit" Text="View Details" runat="server" CssClass="btneditstyle" CommandName="EditRow" CommandArgument='<%# Container.DataItemIndex %>' />
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                </Columns>
                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" Height="20px" Font-Size="10pt" />
                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#F7ECDD" Font-Bold="True" ForeColor="#cb4e48" HorizontalAlign="center"
                    Height="20px" Font-Size="10pt" />
                <AlternatingRowStyle BackColor="#eeeeee" />
            </asp:GridView>

            <br />
            <div id="divRemarkC" runat="server" visible="true">
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
                   
                </table>
            </div>
            <br />

            <asp:GridView runat="server" ID="Grddagnapatrak" ShowFooter="false"
                AutoGenerateColumns="false" CssClass="Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
                OnRowCommand="Grddagnapatrak_RowCommand">
                <Columns>
                    <asp:TemplateField HeaderText="क्रमांक">
                        <ItemTemplate>
                            <%#Container.DataItemIndex+1%>
                             <asp:HiddenField runat="server" ID="hdnbranchid" Value='<%# Eval("Branch_Id") %>' />
                            <asp:HiddenField runat="server" ID="hdnqauterid" Value='<%# Eval("Inspection_Quarter") %>' />
                            <asp:HiddenField runat="server" ID="hdnverificationid" Value='<%# Eval("Verification_Type") %>' />
                            <asp:HiddenField runat="server" ID="hdnempid" Value='<%# Eval("Emp_ID") %>' />
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
                    <asp:TemplateField HeaderText="रिकार्ड अनुसार">
                        <ItemTemplate>
                            <asp:Label ID="lblDepositor_Name" runat="server" Text='<%# Eval("Total_Bags") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Spillage bag">
                        <ItemTemplate>
                            <asp:Label ID="lblclassifi" runat="server" Text='<%# Eval("Spillage_bag") %>'></asp:Label>

                        </ItemTemplate>
                    </asp:TemplateField>
                     <%--<asp:TemplateField HeaderText="View Details Reports">
                        <ItemTemplate>
                            <asp:Button ID="btnEdit" Text="View Details" runat="server" CssClass="btneditstyle" CommandName="EditRow" CommandArgument='<%# Container.DataItemIndex %>' />
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                </Columns>
                <%--<FooterStyle BackColor="#719cb6" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" />--%>
                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" Height="20px" Font-Size="10pt" />
                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#F7ECDD" Font-Bold="True" ForeColor="#cb4e48" HorizontalAlign="center"
                    Height="20px" Font-Size="10pt" />
                <AlternatingRowStyle BackColor="#eeeeee" />
            </asp:GridView>
            <br />
            <div id="divGadnaPAtrak" runat="server" visible="true">
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
                   
                </table>
            </div>
            <br />

            <asp:GridView runat="server" ID="AnnaxureADetaisls" ShowFooter="true"
                OnRowCreated="AnnaxureADetaisls_RowCreated" OnRowCommand="AnnaxureADetaisls_RowCommand"
                OnRowDataBound="AnnaxureADetaisls_RowDataBound" AutoGenerateColumns="false"
                CssClass="Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
                <Columns>
                    
                    <asp:BoundField DataField="Godown_Name" HeaderText="गोदाम का नाम नंबर" />
                    <asp:BoundField DataField="Godown_ID" HeaderText="Godown_ID" />
                    <asp:TemplateField HeaderText="क्षमता">
                        <ItemTemplate>
                            <asp:Label ID="lblGodown_Scientific_Capacity" runat="server" Text='<%# Eval("Godown_Scientific_Capacity") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="जमाकर्ता का नाम">
                        <ItemTemplate>
                            <asp:Label ID="lblDepositer_Name" runat="server" Text='<%# Eval("Depositor_Name") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="जिन्स">
                        <ItemTemplate>
                            <asp:Label ID="lblCommodity" runat="server" Text='<%# Eval("Commodity_Name") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Crop Year">
                        <ItemTemplate>
                            <asp:Label ID="lblCrop_Year" runat="server" Text='<%# Eval("CropYear") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="उपलब्ध बोरो की संख्या">
                        <ItemTemplate>
                            <asp:Label ID="lblAvlBagsGS" runat="server" Text='<%# Eval("AvlBags") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="उपलब्ध बजन क़्वींटल में">
                        <ItemTemplate>
                            <asp:Label ID="lblAvlQtyGS" runat="server" Text='<%# Eval("AvlQty") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="गोदाम में वास्तविक स्कंध भौतिक सत्यापन में जो पाया गया">
                        <ItemTemplate>
                            <%--<asp:Label ID="lblAvlBags" runat="server" Text='<%# Eval("") %>'></asp:Label>--%>
                            <asp:Label ID="txtAvlBagsasPV" runat="server" Text='<%# Eval("No_of_bage_in_PV") %>' TextMode="MultiLine"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Spillage bag">
                        <ItemTemplate>
                            <%--<asp:TextBox ID="lblSpillage_bag" runat="server" Text='<%# Eval("Spilage_Bags") %>'></asp:TextBox>--%>
                            <asp:Label ID="lblSpillage_bag" runat="server" Text='<%# Eval("Spilage_Bags") %>' TextMode="MultiLine"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="भौतिक सत्यापन में यदि कार्यालय के रिकार्ड में अंतर पाया जावे तो उसका उल्लेख किया जावे और निरिक्षण अधिकारी प्रकरण की जाँच कर जांच प्रतिवेदन पूर्ण विवरण सहित अलग से निरिक्षण प्रतिवेदन दे">
                        <ItemTemplate>
                            <asp:Label ID="txtDiffirance_in_PV" runat="server" Text='<%# Eval("PV_Verification_Details_By_IO") %>' TextMode="MultiLine"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="रिमार्क">
                        <ItemTemplate>
                            <asp:Label ID="GtxtRemark" runat="server" Text='<%# Eval("Remark") %>' TextMode="MultiLine"></asp:Label>
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

            <br />
            <br />
            <asp:GridView runat="server" ID="AnnaxureBDetails" ShowFooter="true"
                OnRowCreated="AnnaxureBDetails_RowCreated"
                OnRowDataBound="AnnaxureBDetails_RowDataBound"
                AutoGenerateColumns="false" CssClass="Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
                <Columns>
                    <asp:TemplateField HeaderText="S.No.">
                        <ItemTemplate>
                            <%#Container.DataItemIndex+1%>
                            <asp:HiddenField runat="server" ID="hdnGodown_ID" Value='<%# Eval("Godown_ID") %>' />
                        </ItemTemplate>
                        <ItemStyle Width="1%" />
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Godown ID">
                        <ItemTemplate>
                            <asp:Label ID="lblGodown_ID" runat="server" Text='<%# Eval("Godown_ID") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Godown Name">
                        <ItemTemplate>
                            <asp:Label ID="lblGodown_Name" runat="server" Text='<%# Eval("Godown_Name") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Stack ID">
                        <ItemTemplate>
                            <asp:Label ID="lblStack_ID" runat="server" Text='<%# Eval("Stack_ID") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Stack Name">
                        <ItemTemplate>
                            <asp:Label ID="lblStack_Name" runat="server" Text='<%# Eval("Stack_Name") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Depositer Name">
                        <ItemTemplate>
                            <asp:Label ID="lblDepositor_Name" runat="server" Text='<%# Eval("Depositor_Name") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Commodity Name">
                        <ItemTemplate>
                            <asp:Label ID="lblcommodity" runat="server" Text='<%# Eval("Commodity") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Available Bags">
                        <ItemTemplate>
                            <asp:Label ID="lblTotal_Bags" runat="server" Text='<%# Eval("Available_Bags") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Spillage bags">
                        <ItemTemplate>
                            <asp:Label ID="lblSpillage_bag" runat="server" onkeypress="return isNumberKey(event)" Text='<%# Eval("Spillage_bags") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txt_Spillage_bag" runat="server" Text='<%#Eval("Spillage_bags") %>' onkeypress="return isNumberKey(event)"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Available Bags as Per PV">
                        <ItemTemplate>
                            <asp:Label ID="lblPV_Bags" runat="server" onkeypress="return isNumberKey(event)" Text='<%# Eval("PV_Bags") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txt_PV_Bags" runat="server" Text='<%#Eval("PV_Bags") %>' onkeypress="return isNumberKey(event)"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Classification">
                        <ItemTemplate>
                            <asp:Label ID="lblclassifi" runat="server" Text='<%# Eval("Classification") %>'></asp:Label>

                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Fumigation Date" HeaderStyle-Width="100px">
                        <ItemTemplate>
                            <asp:Label ID="lblStackType" runat="server" Text='<%# Eval("Fumigation_date") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txt_Fumigation_date" runat="server" Text='<%#Eval("Fumigation_date") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                   
                    <asp:TemplateField HeaderText="Remark" HeaderStyle-Width="120px">
                        <ItemTemplate>
                            <asp:Label ID="GtxtRemark" runat="server" Width="120px" align="Center" Height="25px" TextMode="MultiLine" Text='<%# Eval("Remark") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txt_Remark" runat="server" Text='<%#Eval("Remark") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                   
                </Columns>
                <%--<FooterStyle BackColor="#719cb6" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" />--%>
                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" Height="20px" Font-Size="10pt" />
                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#F7ECDD" Font-Bold="True" ForeColor="#cb4e48" HorizontalAlign="center"
                    Height="20px" Font-Size="10pt" />
                <AlternatingRowStyle BackColor="#eeeeee" />
            </asp:GridView>

            <br />
            <br />
            <asp:GridView runat="server" ID="Grd_AnnaxureC" OnRowDataBound="Grd_AnnaxureC_RowDataBound" ShowFooter="true"
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
                            <asp:TemplateField HeaderText="Godown Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblGodown_Name" runat="server" Text='<%# Eval("Godown_Name") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                           
                            <asp:TemplateField HeaderText="Depositor Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblDepositor_Name" runat="server" Text='<%# Eval("Depositer_Name") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Commodity Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblCommodity_Name" runat="server" Text='<%# Eval("Commodity_Name") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Depositor whr id">
                                <ItemTemplate>
                                    <asp:Label ID="lblDepositor_whr_id" runat="server" Text='<%# Eval("WHR_ID") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="WHR Issue Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblWHR_Issue_Date" runat="server" Text='<%# Eval("WHR_Issue_Date") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Available Bags">
                                <ItemTemplate>
                                    <asp:Label ID="lblAvlBags" runat="server" Text='<%# Eval("Avl_Bags") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Available Qty">
                                <ItemTemplate>
                                    <asp:Label ID="lblAvlQty" runat="server" Text='<%# Eval("Avl_Quantity") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Placed in Bank">
                                <ItemTemplate>
                                    <asp:Label ID="lblPlace_In_Bank" runat="server" Text='<%# Eval("Place_In_Bank") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remark">
                                <ItemTemplate>
                                    <asp:Label ID="lblRemark" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>

                        <%--<FooterStyle BackColor="#719cb6" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" />--%>
                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#E6C79D" Font-Bold="True" ForeColor="#cb4e48" HorizontalAlign="center" />
                        <AlternatingRowStyle BackColor="#eeeeee" />
                    </asp:GridView>

            <br />
            <br />
            <asp:GridView runat="server" ID="GD_StackBal" OnRowCreated="GD_StackBal_RowCreated" 
                OnRowCommand="GD_StackBal_RowCommand" OnRowDataBound="GD_StackBal_RowDataBound"
                AutoGenerateColumns="false" CssClass="Grid" AlternatingRowStyle-CssClass="alt" 
                PagerStyle-CssClass="pgr" ShowFooter="true">

                <Columns>
                    <asp:TemplateField HeaderText="क्रमांक">
                        <ItemTemplate>
                            <%#Container.DataItemIndex+1%> 
                        </ItemTemplate>
                        <ItemStyle Width="1%" />
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Godown ID">
                        <ItemTemplate>
                            <asp:Label ID="lblGodown_ID" runat="server" Text='<%# Eval("Godown_ID") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="गोदाम का नाम">
                        <ItemTemplate>
                            <asp:Label ID="lblGodown_Name" runat="server" Text='<%# Eval("Godown_Name") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="जमाकर्ता का नाम">
                        <ItemTemplate>
                            <asp:Label ID="lblDepositor_Name" runat="server" Text='<%# Eval("Depositer_Name") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="स्कंध का नाम">
                        <ItemTemplate>
                            <asp:Label ID="lblcommodity" runat="server" Text='<%# Eval("Commodity_Name") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="वर्ष">
                        <ItemTemplate>
                            <asp:Label ID="lblCrop_Year" runat="server" Text='<%# Eval("Crop_Year") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="स्टैक आईडी">
                        <ItemTemplate>
                            <asp:Label ID="lblstack_id" runat="server" Text='<%# Eval("stack_id") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="स्टैक नाम">
                        <ItemTemplate>
                            <asp:Label ID="lblStack_Name" runat="server" Text='<%# Eval("Stack_Name") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="लम्बाई">
                        <ItemTemplate>
                            <asp:Label ID="lblLength" runat="server" Text='<%# Eval("Length") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="चौड़ाई">
                        <ItemTemplate>
                            <asp:Label ID="lblWidth" runat="server" Text='<%# Eval("Width") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="अतिरिक्त">
                        <ItemTemplate>
                            <asp:Label ID="lblExtra" runat="server" Text='<%# Eval("Extra") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="योग (7+8+9)">
                        <ItemTemplate>
                            <asp:Label ID="lblTotal_L_W_E" runat="server" Text='<%# Eval("Total_L_W_E") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="बोरो के लेयर की ऊंचाई">
                        <ItemTemplate>
                            <asp:Label ID="lblHeight" runat="server" Text='<%# Eval("Height") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ब्लॉक क्र./संख्या">
                        <ItemTemplate>
                            <asp:Label ID="lblNumber_Of_Block" runat="server" Text='<%# Eval("Number_Of_Block") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="बोरियो की संख्या (10*11*12)">
                        <ItemTemplate>
                            <asp:Label ID="lblNo_of_Bags" runat="server" Text='<%# Eval("No_of_Bags") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ऊपर">
                        <ItemTemplate>
                            <asp:Label ID="lblUp" runat="server" Text='<%# Eval("Up") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="निचे">
                        <ItemTemplate>
                            <asp:Label ID="lblBelow" runat="server" Text='<%# Eval("Below") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="टोटल बौरे">
                        <ItemTemplate>
                            <asp:Label ID="lblTotal_Bags" runat="server" Text='<%# Eval("Total_Bags") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                  <asp:TemplateField HeaderText="Spillage Bag">
                        <ItemTemplate>
                            <asp:Label ID="lblSpillage_bag" runat="server" Text='<%# Eval("Spillage_bag") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Remark">
                        <ItemTemplate>
                            <asp:Label ID="lblRemark" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>
                </Columns>
                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#E6C79D" Font-Bold="True" ForeColor="#cb4e48" HorizontalAlign="center" />
                <AlternatingRowStyle BackColor="#eeeeee" />
            </asp:GridView>
        </div>
    </div>

</asp:Content>

