<%@ Page Title="" Language="C#" MasterPageFile="~/Special_PV/Region/Inspection_RO_For_Special_PV.master" AutoEventWireup="true" CodeFile="~/Special_PV/Region/View_Reports_New_For_Special_PV.aspx.cs" Inherits="Special_PV_Region_View_Reports_New_For_Special_PV" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../assets/css/style.css" rel="stylesheet" />
    <!-- font awesome -->
    <link href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../assets/New/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assets/New/css/bootstrap.min2.css" rel="stylesheet" />
    <link href="../CSS/style.css" rel="stylesheet" />
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />
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
    <style type="text/css">
        .left, .right {
            float: left;
            width: 20%; /* The width is 20%, by default */
        }

        .main {
            float: left;
            width: 60%; /* The width is 60%, by default */
        }

        @media screen and (max-width: 800px) {
            .left, .main, .right {
                width: 100%; /* The width is 100%, when the viewport is 800px or smaller */
            }
        }
    </style>
    <style type="text/css">
        fieldset {
            border: 1px solid #2095A1;
            padding: 0.35em 0.625em 0.75em;
            margin: 10px;
            border-radius: 5px;
            padding-left: 20px;
        }

        legend {
            padding: 2px 8px;
            border-radius: 10px;
            width: auto;
            border: 1px solid #2095A1;
            font-size: 17px;
            font-weight: bold;
            color: #030203;
        }

        .content-wrapper {
            padding: 1.75rem 1.25rem;
        }

        .table-bordered th, .table-bordered td {
            border: 1px solid #030203;
        }

        .form-control {
            border: 1px solid #767B83;
            border-radius: 8px;
        }

        .table th {
            text-align: center;
        }

        .form-inline {
            display: block !important;
        }

        th.sorting, th.sorting_asc, th.sorting_desc {
            background: #647e68 !important;
            color: black !important;
        }

        .table > tbody > tr > td, .table > tbody > tr > th, .table > tfoot > tr > td, .table > tfoot > tr > th, .table > thead > tr > td, .table > thead > tr > th {
            padding: 8px 5px;
            color: black !important;
        }

        element.style {
            font-size: medium !important;
        }
    </style>
    <script type="text/javascript">

        $(document).ready(function () {

            $("#Panel1").hide();

            $("#Button1").click(function () {

                $("#Panel1").show();

            });

        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <table style="width: 100%; margin-top: 10px;">
            <tr>
                <td style="text-align: right;">
                    <asp:Label ID="Label9" runat="server" Text="Region : "></asp:Label>
                </td>
                <td style="text-align: left;">
                    <asp:TextBox ID="txt_Region" runat="server" ReadOnly="true" class="form-control">
                    </asp:TextBox>
                </td>
                <td style="text-align: right;">
                    <asp:Label ID="Label1" runat="server" Text="District : "></asp:Label>
                </td>
                <td style="text-align: left;">
                    <asp:DropDownList ID="ddldistrict" runat="server" AutoPostBack="True" class="form-control" OnSelectedIndexChanged="ddl_dist_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td style="text-align: right;">
                    <asp:Label ID="Label7" runat="server" Text="Branch : "></asp:Label>
                </td>
                <td style="text-align: left;">
                    <asp:DropDownList ID="ddlBranch" runat="server" AutoPostBack="false" class="form-control">
                    </asp:DropDownList>
                </td>
                <td style="text-align: center;" colspan="4">
                    <asp:Button ID="Button1" runat="server" Text="Search" Visible="true" Width="100px" ValidationGroup="A"
                        CssClass="btn btn-info" OnClick="Button1_Click" /></td>
            </tr>
        </table>
        <table align="center" style="width: 100%; margin-top: 20px; border: #E6C79D; border-style: solid; border-width: 0px;">
            <tr>
                <td align="center" style="border: #E6C79D; border-style: solid; border-width: 2px;" colspan="4">
                    <span style="color: #cb4e48; font-weight: bold; font-size: 17px">निरीक्षण अधिकारीयो का नाम जिन्होंने अपना निरीक्षण पूर्ण कर लिया हैं  </span>
                    <asp:Label ID="lbl_user" runat="server" Text="Label" Visible="false"></asp:Label>
                </td>

            </tr>
            <tr>
                <td colspan="4" valign="top" align="center">
                    <asp:GridView runat="server" ID="GrdOfficerPreviousInsp" OnRowCreated="GrdOfficerPreviousInsp_RowCreated"
                            OnRowDataBound="GrdOfficerPreviousInsp_RowDataBound" OnRowCommand="GrdOfficerPreviousInsp_RowCommand"
                        AutoGenerateColumns="false" CssClass="Grid" AlternatingRowStyle-CssClass="alt"
                        PagerStyle-CssClass="pgr" ShowFooter="true">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No.">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1%>
                                    <asp:HiddenField runat="server" ID="hdndistrictid" Value='<%# Eval("District_Id") %>' />
                                    <asp:HiddenField runat="server" ID="hdnbranchid" Value='<%# Eval("Branch_ID") %>' />
                                    <asp:HiddenField runat="server" ID="hdnRM_Verify_Status" Value='<%# Eval("RM_Verify_Status") %>' />
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
                                    <asp:Label ID="lblDepotname" runat="server" Text='<%# Eval("Depositer_Name") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Commodity Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblCommodity_Name" runat="server" Text='<%# Eval("Commodity_Name") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Crop Year">
                                <ItemTemplate>
                                    <asp:Label ID="lblCrop_Year" runat="server" Text='<%# Eval("Crop_Year") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Length">
                                <ItemTemplate>
                                    <asp:Label ID="lblLength" runat="server" Text='<%# Eval("Length") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Width">
                                <ItemTemplate>
                                    <asp:Label ID="lblWidth" runat="server" Text='<%# Eval("Width") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Extra">
                                <ItemTemplate>
                                    <asp:Label ID="lblExtra" runat="server" Text='<%# Eval("Extra") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total_L_W_E">
                                <ItemTemplate>
                                    <asp:Label ID="lblTotal_L_W_E" runat="server" Text='<%# Eval("Total_L_W_E") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Height">
                                <ItemTemplate>
                                    <asp:Label ID="lblHeight" runat="server" Text='<%# Eval("Height") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Number Of Block">
                                <ItemTemplate>
                                    <asp:Label ID="lblNumber_Of_Block" runat="server" Text='<%# Eval("Number_Of_Block") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="No of Bags">
                                <ItemTemplate>
                                    <asp:Label ID="lblNo_of_Bags" runat="server" Text='<%# Eval("No_of_Bags") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Up">
                                <ItemTemplate>
                                    <asp:Label ID="lblUp" runat="server" Text='<%# Eval("Up") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Below">
                                <ItemTemplate>
                                    <asp:Label ID="lblBelow" runat="server" Text='<%# Eval("Below") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total_Bags">
                                <ItemTemplate>
                                    <asp:Label ID="lblTotal_Bags" runat="server" Text='<%# Eval("Total_Bags") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Spillage bag">
                                <ItemTemplate>
                                    <asp:Label ID="lblSpillage_bag" runat="server" Text='<%# Eval("Spillage_bag") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                           <%-- <asp:TemplateField HeaderText="Financial Year">
                                <ItemTemplate>
                                    <asp:Label ID="lblFinancial_Year" runat="server" Text='<%# Eval("Financial_Year") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>--%>
                             <asp:TemplateField HeaderText="Remark">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRemark" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>
                        </Columns>
                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" Height="20px" Font-Size="10pt" />
                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#F7ECDD" Font-Bold="True" ForeColor="#cb4e48" HorizontalAlign="center"
                            Height="20px" Font-Size="10pt" />
                        <AlternatingRowStyle BackColor="#eeeeee" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
        <table align="center" style="width: 100%; margin-top: 20px; border: #E6C79D; border-style: solid; border-width: 0px;">
            <tr style="margin-top: 20px" id="divremark" runat="server" visible="false">
                <td style="text-align: left;">
                    <asp:Label ID="Label2" runat="server" Text="Remark : "></asp:Label>
                </td>
                <td style="text-align: left;">
                    <asp:TextBox ID="txtremark" runat="server" TextMode="MultiLine" Height="25" Width="200" CssClass="form-control">
                    </asp:TextBox>
                </td>
            </tr>
        </table>
        <table align="center" style="width: 100%; margin-top: 20px; border: #E6C79D; border-style: solid; border-width: 0px;">
            <%-- <tr style="margin-top: 20px" id="divremark" runat="server" visible="false">
                <td style="text-align: left;">
                    <asp:Label ID="Label3" runat="server" Text="Remark : "></asp:Label>
                </td>
                <td style="text-align: left;">
                    <asp:TextBox ID="txtremark" runat="server" ReadOnly="true" class="form-control">
                    </asp:TextBox>
                </td>
            </tr>--%>
            <tr style="margin-top: 20px" id="divverify" runat="server" visible="false">
                <td style="text-align: center;">
                    <asp:Button CssClass="btn btn-warning" ID="btn_saveInspDate" runat="server" Text="Verify" OnClick="btn_saveInspDate_Click"></asp:Button>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

