<%@ Page Title="" Language="C#" MasterPageFile="~/Inspections/Masters/State.master" AutoEventWireup="true" CodeFile="Rpt_Schedule_Inspection_Complited.aspx.cs" Inherits="Inspections_State_Rpt_Schedule_Inspection_Complited" %>

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
                    <asp:DropDownList ID="ddlquater" runat="server" AutoPostBack="false" class="form-control" >
                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                        <asp:ListItem Value="1">1st Quarter</asp:ListItem>
                        <asp:ListItem Value="2">2nd Quarter</asp:ListItem>
                        <asp:ListItem Value="3">3rd Quarter</asp:ListItem>
                        <asp:ListItem Value="4">4th Quarter</asp:ListItem>
                        <asp:ListItem Value="5">Half Yearly Inspection</asp:ListItem>
                    </asp:DropDownList>
                </td>
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
            </tr>
            <tr>
                <td style="text-align: right;">
                    <asp:Label ID="Label7" runat="server" Text="Verification Type : "></asp:Label>
                </td>
                <td style="text-align: left;">
                    <asp:DropDownList ID="ddlverification" runat="server" AutoPostBack="false" class="form-control">
                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                        <%--<asp:ListItem Value="1">General Inspection</asp:ListItem>--%>
                        <asp:ListItem Value="2">Physical Verification</asp:ListItem>
                        <%--<asp:ListItem Value="3">Both</asp:ListItem>--%>

                    </asp:DropDownList>
                </td>
                <td style="text-align: center;" colspan="4">
                    <asp:Button ID="Button1" runat="server" Text="Search" Visible="true" Width="100px" ValidationGroup="A"
                        CssClass="btn btn-info" OnClick="Button1_Click" /></td>

            </tr>
        </table>
        <table align="center" style="width: 100%; border: #E6C79D; border-style: solid; border-width: 0px;">
            <tr>
                <td align="center" style="border: #E6C79D; border-style: solid; border-width: 2px;" colspan="4">
                    <span style="color: #cb4e48; font-weight: bold; font-size: 17px">निरीक्षण अधिकारीयो का नाम जिन्होंने अपना निरीक्षण पूर्ण कर लिया हैं  </span>
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
                    <td colspan="4" valign="top" align="center">
                        <asp:GridView runat="server" ID="GrdOfficerPreviousInsp" OnRowDataBound="GrdOfficerPreviousInsp_RowDataBound"
                            AutoGenerateColumns="false" CssClass="Grid" AlternatingRowStyle-CssClass="alt" 
                            OnRowCommand="GrdOfficerPreviousInsp_RowCommand" PagerStyle-CssClass="pgr">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No.">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1%>
                                        <asp:HiddenField runat="server" ID="hdndistrictid" Value='<%# Eval("District_ID") %>' />
                                        <asp:HiddenField runat="server" ID="hdnbranchid" Value='<%# Eval("Branch_ID") %>' />
                                        <asp:HiddenField runat="server" ID="hdnVerificationType" Value='<%# Eval("Verification_Type") %>' />
                                        <asp:HiddenField runat="server" ID="hdnemployeeid" Value='<%# Eval("PF_ID") %>' />
                                        <asp:HiddenField runat="server" ID="hdnInspection_ID" Value='<%# Eval("Inspection_ID") %>' />
                                    </ItemTemplate>
                                    <ItemStyle Width="1%" />
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Inspection Officer Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOfficer_Name" runat="server" Text='<%# Eval("Officer_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="District">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDistirct_name" runat="server" Text='<%# Eval("Distirct_name") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Branch">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDepotname" runat="server" Text='<%# Eval("Depotname") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Order No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOrder_No" runat="server" Text='<%# Eval("Order_No") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Order Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOrder_Date" runat="server" Text='<%# Eval("Order_Date") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Inspection Period">
                                    <ItemTemplate>
                                        <asp:Label ID="lblInsp_Period" runat="server" Text='<%# Eval("Inspection_Status") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Inspection Month">
                                    <ItemTemplate>
                                        <asp:Label ID="lblInsp_Month" runat="server" Text='<%# Eval("Inspection_Month") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Inspection Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lblInsp_Type" runat="server" Text='<%# Eval("VerificationType") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>
                               
                                <asp:TemplateField HeaderText="Annexure B" ItemStyle-Width="15%">
                                    <ItemTemplate>
                                        <asp:Button ID="btnAnnexure_B" Text="View Annexure B" runat="server" CommandName="Annexure_B" CssClass="btn btn-info" CommandArgument='<%# Container.DataItemIndex %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                              
                                <asp:TemplateField HeaderText="Gadna Patrak" ItemStyle-Width="15%">
                                    <ItemTemplate>
                                        <asp:Button ID="btnGadnaPatrak" Text="View Gadna Patrak" runat="server" CommandName="GadnaPatrak" CssClass="btn btn-info" CommandArgument='<%# Container.DataItemIndex %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
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
                <tbody id="PVGenGrd" runat="server" visible="false">
                    <td colspan="4" valign="top" align="center">
                        <asp:GridView runat="server" ID="Grdgenboth" OnRowDataBound="GrdOfficerPreviousInsp_RowDataBound"
                            AutoGenerateColumns="false" CssClass="Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No.">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1%>
                                        <asp:HiddenField runat="server" ID="hdndistrictid" Value='<%# Eval("District_ID") %>' />
                                        <asp:HiddenField runat="server" ID="hdnbranchid" Value='<%# Eval("Branch_ID") %>' />
                                        <asp:HiddenField runat="server" ID="hdnVerificationType" Value='<%# Eval("Verification_Type") %>' />
                                        <asp:HiddenField runat="server" ID="hdnInspection_ID" Value='<%# Eval("Inspection_ID") %>' />
                                    </ItemTemplate>
                                    <ItemStyle Width="1%" />
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateField>
                                <%--<asp:TemplateField HeaderText="Officer Mobile No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblpfid" runat="server" Text='<%# Eval("PF_ID") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Inspection Officer Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOfficer_Name" runat="server" Text='<%# Eval("Officer_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="District">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDistirct_name" runat="server" Text='<%# Eval("Distirct_name") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Branch">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDepotname" runat="server" Text='<%# Eval("Depotname") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>
                                <%-- <asp:TemplateField HeaderText="Manager Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblBranchManagerName" runat="server" Text='<%# Eval("BranchManagerName") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Manager Mobile No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblBranchManagerCUGNo" runat="server" Text='<%# Eval("BranchManagerCUGNo") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Order No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOrder_No" runat="server" Text='<%# Eval("Order_No") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Order Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOrder_Date" runat="server" Text='<%# Eval("Order_Date") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Inspection Period">
                                    <ItemTemplate>
                                        <asp:Label ID="lblInsp_Period" runat="server" Text='<%# Eval("Inspection_Status") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Inspection Month">
                                    <ItemTemplate>
                                        <asp:Label ID="lblInsp_Month" runat="server" Text='<%# Eval("Inspection_Month") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Inspection Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lblInsp_Type" runat="server" Text='<%# Eval("VerificationType") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Annexure A" ItemStyle-Width="15%">
                                    <ItemTemplate>
                                        <asp:Button ID="btnAnnexure_B" Text="Fill Annexure B" runat="server" CommandName="Annexure_B" CssClass="btn btn-info" CommandArgument='<%# Container.DataItemIndex %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Annexure B" ItemStyle-Width="15%">
                                    <ItemTemplate>
                                        <asp:Button ID="btnAnnexure_B" Text="Fill Annexure B" runat="server" CommandName="Annexure_B" CssClass="btn btn-info" CommandArgument='<%# Container.DataItemIndex %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Annexure C" ItemStyle-Width="15%">
                                    <ItemTemplate>
                                        <asp:Button ID="btnAnnexure_B" Text="Fill Annexure B" runat="server" CommandName="Annexure_B" CssClass="btn btn-info" CommandArgument='<%# Container.DataItemIndex %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Depositer For" ItemStyle-Width="15%">
                                    <ItemTemplate>
                                        <asp:Button ID="btnAnnexure_B" Text="Fill Annexure B" runat="server" CommandName="Annexure_B" CssClass="btn btn-info" CommandArgument='<%# Container.DataItemIndex %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delevery Form" ItemStyle-Width="15%">
                                    <ItemTemplate>
                                        <asp:Button ID="btnAnnexure_B" Text="Fill Annexure B" runat="server" CommandName="Annexure_B" CssClass="btn btn-info" CommandArgument='<%# Container.DataItemIndex %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
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

