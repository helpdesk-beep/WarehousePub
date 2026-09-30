<%@ Page Title="" Language="C#" MasterPageFile="~/Inspections/Masters/Inspection_RO_TQ.master" AutoEventWireup="true" CodeFile="View_Branch_Wise_Ann_Counting_Sheat.aspx.cs" Inherits="Inspections_TQRO_View_Branch_Wise_Ann_Counting_Sheat" %>

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

        <div id="divshow" runat="server" visible="true" class="widget-content">
        <asp:Button ID="Button1" runat="server" Text="Print" OnClientClick="return PrintPanel();" CssClass="btn btn-warning" />
            <div>
                <span>निरक्षण अधिकारी का नाम:&nbsp;
                    <asp:Label ID="lblname" runat="server"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    निरीक्षण अधिकारी के द्वारा निरीक्षण/भौतिक सत्यापन की गई शाखा का नाम:&nbsp;
                    <asp:Label ID="lblbranchname" runat="server"></asp:Label>
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
                            <asp:HiddenField runat="server" ID="hdnbranchid" Value='<%# Eval("Branch_Id") %>'/>
                            <asp:HiddenField runat="server" ID="hdnqauterid" Value='<%# Eval("Inspection_Quarter") %>' />
                            <asp:HiddenField runat="server" ID="hdnverificationid" Value='<%# Eval("Verification_Type") %>' />
                            <asp:HiddenField runat="server" ID="hdnempid" Value='<%# Eval("Employee_ID") %>' />
                            <asp:HiddenField runat="server" ID="hdnfinancialYear" Value='<%# Eval("Financial_Year") %>' />
                       
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
                    <asp:TemplateField HeaderText="View Details Reports">
                        <ItemTemplate>
                            <asp:Button ID="btnEdit" Text="View Details" runat="server" CssClass="btneditstyle" CommandName="EditRow" CommandArgument='<%# Container.DataItemIndex %>' />
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
                         <asp:HiddenField runat="server" ID="hdnfinancialYear" Value='<%# Eval("Financial_Year") %>' />
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
                    <asp:TemplateField HeaderText="View Details Reports">
                        <ItemTemplate>
                            <asp:Button ID="btnEdit" Text="View Details" runat="server" CssClass="btneditstyle" CommandName="EditRow" CommandArgument='<%# Container.DataItemIndex %>' />
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
                            <asp:HiddenField runat="server" ID="hdnfinancialYear" Value='<%# Eval("Financial_year") %>' />
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
                     <asp:TemplateField HeaderText="View Details Reports">
                        <ItemTemplate>
                            <asp:Button ID="btnEdit" Text="View Details" runat="server" CssClass="btneditstyle" CommandName="EditRow" CommandArgument='<%# Container.DataItemIndex %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" Height="20px" Font-Size="10pt" />
                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#F7ECDD" Font-Bold="True" ForeColor="#cb4e48" HorizontalAlign="center"
                    Height="20px" Font-Size="10pt" />
                <AlternatingRowStyle BackColor="#eeeeee" />
            </asp:GridView>

            <br />
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
                            <asp:HiddenField runat="server" ID="hdnfinancialYear" Value='<%# Eval("Financial_year") %>' />
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
                     <asp:TemplateField HeaderText="View Details Reports">
                        <ItemTemplate>
                            <asp:Button ID="btnEdit" Text="View Details" runat="server" CssClass="btneditstyle" CommandName="EditRow" CommandArgument='<%# Container.DataItemIndex %>' />
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
        </div>
    </div>

</asp:Content>

