<%@ Page Title="" Language="C#" MasterPageFile="~/Inspections/Masters/State.master" AutoEventWireup="true" CodeFile="View_Complite_Inspection_Region_Wise.aspx.cs" Inherits="Inspections_State_View_Complite_Inspection_Region_Wise" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style type="text/css">
        .modal-dialog {
            width: 1000px;
            margin: 30px auto;
        }

        .btn-info {
            color: #fff;
            background-color: #5bc0de;
            border-color: #46b8da;
        }

        .btn {
            display: inline-block;
            padding: 6px 12px;
            margin-bottom: 0;
            font-size: 14px;
            font-weight: 400;
            line-height: 1.42857143;
            text-align: center;
            white-space: nowrap;
            vertical-align: middle;
            -ms-touch-action: manipulation;
            touch-action: manipulation;
            cursor: pointer;
            -webkit-user-select: none;
            -moz-user-select: none;
            -ms-user-select: none;
            user-select: none;
            background-image: none;
            border: 1px solid transparent;
            border-radius: 4px;
        }

        .btn-info:hover {
            color: black;
            background-color: #31b0d5;
            border-color: #269abc;
        }

        .btn.active, .btn:active {
            background-image: none;
            outline: 0;
            -webkit-box-shadow: inset 0 3px 5px rgba(0,0,0,.125);
            box-shadow: inset 0 3px 5px rgba(0,0,0,.125);
        }
    </style>

    <script type="text/javascript">
        function printGrid() {
            var gridData = document.getElementById('<%= GrdOfficerPreviousInsp.ClientID %>');
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
    <div runat="server">
        <table style="width: 100%;">
            <tr>
                <td style="text-align: right;">
                    <asp:Label ID="Label9" runat="server" Text="Region : "></asp:Label>
                </td>
                <td style="text-align: left;">
                    <asp:DropDownList ID="ddl_dist" runat="server" AutoPostBack="false" class="form-control">
                    </asp:DropDownList>
                </td>
                <td style="text-align: right;">
                    <asp:Label ID="Label1" runat="server" Text="Quarter : "></asp:Label>
                </td>
                <td style="text-align: left;">
                    <asp:DropDownList ID="ddlquater" runat="server" AutoPostBack="false" class="form-control">
                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                        <asp:ListItem Value="1">1st Quarter</asp:ListItem>
                        <asp:ListItem Value="2">2nd Quarter</asp:ListItem>
                        <asp:ListItem Value="3">3rd Quarter</asp:ListItem>
                        <asp:ListItem Value="4">4th Quarter</asp:ListItem>
                        <asp:ListItem Value="5">Half Yearly Inspection</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">
                    <asp:Label ID="Label2" runat="server" Text="Year : "></asp:Label>
                </td>
                <td style="text-align: left;">
                    <asp:DropDownList ID="ddlyear" runat="server" AutoPostBack="false" class="form-control">
                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                        <asp:ListItem Value="2021">2021</asp:ListItem>
                        <asp:ListItem Value="2022">2022</asp:ListItem>
                        <asp:ListItem Value="2023">2023</asp:ListItem>
                        <asp:ListItem Value="2024">2024</asp:ListItem>
                        <asp:ListItem Value="2025">2025</asp:ListItem>
                    </asp:DropDownList>
                </td>

                <td style="text-align: right;">
                    <asp:Label ID="Label7" runat="server" Text="Verification Type : "></asp:Label>
                </td>
                <td style="text-align: left;">
                    <asp:DropDownList ID="ddlverification" runat="server" AutoPostBack="false" class="form-control">
                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                        <asp:ListItem Value="1">General Inspection</asp:ListItem>
                        <asp:ListItem Value="2">Physical Verification</asp:ListItem>
                        <asp:ListItem Value="3">Both</asp:ListItem>
                    </asp:DropDownList>
                </td>

            </tr>
            <tr>
                <td style="text-align: center;" colspan="4">
                    <asp:Button ID="Button1" runat="server" Text="Search" Visible="true" Width="100px" ValidationGroup="A"
                        CssClass="btn btn-info" OnClick="Button1_Click" /></td>
            </tr>
        </table>
        <table align="center" style="width: 100%; border: #E6C79D; border-style: solid; border-width: 0px;">
            <tr>
                <td align="center" style="border: #E6C79D; border-style: solid; border-width: 2px;" colspan="4">
                    <%--<span style="color: #cb4e48; font-weight: bold; font-size: 17px">निरीक्षण अधिकारीयो का नाम जिन्होंने अपना निरीक्षण पूर्ण कर लिया हैं  </span>--%>
                    <asp:Label ID="lbl_user" runat="server" Text="Label" Visible="false"></asp:Label>
                </td>

            </tr>
            <tr>
                <td colspan="4" align="center" style="font-size: small;">Total Record :
                                            <asp:Label ID="lblOfficerList" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <tbody id="PVGrd" runat="server" visible="false">
                    <div class="card-body">
                        <%--<asp:Button ID="btnExportToWord" CssClass="btnMargin btn btn-outline-primary rounded-0" runat="server" Text="ExportToWord"  />--%>
                        <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-warning" Text="Print" OnClientClick="printGrid()" />
                    </div>
                    <td colspan="4" valign="top" align="center">
                        <asp:GridView runat="server" ID="GrdOfficerPreviousInsp" OnRowDataBound="GrdOfficerPreviousInsp_RowDataBound"
                            AutoGenerateColumns="false" CssClass="Grid" AlternatingRowStyle-CssClass="alt" OnRowCreated="GrdOfficerPreviousInsp_RowCreated"
                            OnRowCommand="GrdOfficerPreviousInsp_RowCommand" PagerStyle-CssClass="pgr">
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
                                <asp:TemplateField HeaderText="रिकार्ड अनुसार">
                                    <ItemTemplate>
                                        <asp:Label ID="lblA_AVLBags" runat="server" Text='<%# Eval("A_AVLBags") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="निरीक्षण में पाए गए बोरे">
                                    <ItemTemplate>
                                        <asp:Label ID="lblA_AVl_BPV" runat="server" Text='<%# Eval("A_AVl_BPV") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Spilage_Bags">
                                    <ItemTemplate>
                                        <asp:Label ID="lblA_SB" runat="server" Text='<%# Eval("A_SB") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="निरीक्षण में पाये गये ज्यादा बोरे">
                                    <ItemTemplate>
                                        <asp:Label ID="lblA_TotalAvlBags" runat="server" Text='<%# Eval("A_TotalAvlBags") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="निरीक्षण में पाये गये कम बोरे">
                                    <ItemTemplate>
                                        <asp:Label ID="lblA_TotalAvlBags2" runat="server" Text='<%# Eval("A_TotalAvlBags2") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="रिकार्ड अनुसार">
                                    <ItemTemplate>
                                        <asp:Label ID="lblB_AB" runat="server" Text='<%# Eval("B_AB") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="निरीक्षण में पाए गए बोरे">
                                    <ItemTemplate>
                                        <asp:Label ID="lblB_ABPV" runat="server" Text='<%# Eval("B_ABPV") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Spilage_Bags">
                                    <ItemTemplate>
                                        <asp:Label ID="lblB_SB" runat="server" Text='<%# Eval("B_SB") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="निरीक्षण में पाये गये ज्यादा बोरे">
                                    <ItemTemplate>
                                        <asp:Label ID="lblB_TA" runat="server" Text='<%# Eval("B_TA") %>'></asp:Label>
                                    </ItemTemplate>

                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="निरीक्षण में पाये गये कम बोरे">
                                    <ItemTemplate>
                                        <asp:Label ID="lblB_TA2" runat="server" Text='<%# Eval("B_TA2") %>'></asp:Label>

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="रिकार्ड अनुसार">
                                    <ItemTemplate>
                                        <asp:Label ID="lblC_AB" runat="server" Text='<%# Eval("C_AB") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="बैंक में रहन रखी गई रसीदों की संख्या">
                                    <ItemTemplate>
                                        <asp:Label ID="lblC_Placeinbank" runat="server" Text='<%# Eval("C_Placeinbank") %>'></asp:Label>

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
                    </td>
                </tbody>

            </tr>
        </table>

    </div>
</asp:Content>

