<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Region_Master.master" AutoEventWireup="true" CodeFile="~/Region/NCCF_Rent_RM_Approval_Status.aspx.cs" Inherits="Region_NCCF_Rent_RM_Approval_Status" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../assets/New/css/style.css" rel="stylesheet" />
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
            text-align: center !important;
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <fieldset style="border: 1px solid navy; border-radius: 10px 10px 10px 10px;">
            <legend>NCCF Rent Bills Approve/Reject By Regional Manager</legend>
            <div class="table-responsive">
                <asp:GridView runat="server" DataKeyNames="Bill_Number" ID="GrdBills"
                    CssClass="table table-bordered table-hover datatable" AutoGenerateColumns="False" OnRowCommand="GrdBills_RowCommand"
                    autopostback="true">
                    <Columns>
                        <asp:TemplateField HeaderText="S.No" HeaderStyle-BackColor="LightBlue">
                            <ItemTemplate>
                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                <%--                                        <asp:HiddenField ID="hdnId" runat="server" Value='<%# Bind("SNo") %>' />--%>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="District Name" HeaderStyle-BackColor="LightBlue">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblDistrict_Name" Text='<%# Eval("District_Name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Branch Name" HeaderStyle-BackColor="LightBlue">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblDepotName" Text='<%# Eval("DepotName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Godown Name" HeaderStyle-BackColor="LightBlue">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblGodown_Name" Text='<%# Eval("Godown_Name") %>'></asp:Label>
                                <asp:HiddenField runat="server" ID="hdnGodown_Id" Value='<%#Eval("Godown_Id")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Bill Number" HeaderStyle-BackColor="LightBlue">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblBill_Number" Text='<%# Eval("Bill_Number") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Commodity Name" HeaderStyle-BackColor="LightBlue">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblCommodity_Name" Text='<%# Eval("Commodity_Name") %>'></asp:Label>
                                <asp:HiddenField runat="server" ID="hdnCommodity_Id" Value='<%#Eval("Commodity_Id")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Crop Year" HeaderStyle-BackColor="LightBlue">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblCrop_Year" Text='<%# Eval("Crop_Year") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Financial Year" HeaderStyle-BackColor="LightBlue">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblFinancial_Year" Text='<%# Eval("Financial_Year") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Month" HeaderStyle-BackColor="LightBlue">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblMonth" Text='<%# Eval("Month") %>'></asp:Label>
                                <asp:HiddenField runat="server" ID="hdnMonth" Value='<%#Eval("Month")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Net Amount" HeaderStyle-BackColor="LightBlue">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblNet_Amount" Text='<%# Eval("Net_Amount") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Approve" HeaderStyle-BackColor="LightBlue">
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlApprovalStatus" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlApprovalStatus_SelectedIndexChanged">
                                    <asp:ListItem Value="Y">Approve</asp:ListItem>
                                    <asp:ListItem Value="N">Reject</asp:ListItem>
                                </asp:DropDownList>
                                <asp:TextBox ID="txtRemark" runat="server" Visible="false" Width="125" CssClass="form-control" Text='NA' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Update" HeaderStyle-BackColor="LightBlue" ItemStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnUpdate" runat="server" CausesValidation="false" CommandName="EditRow" CommandArgument='<%# Eval("Bill_Number")%>' Text="Update"  class="button button2" TabIndex="8" Width="100px" Height="20px"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Print Bill" HeaderStyle-BackColor="LightBlue">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnPrint" runat="server" CausesValidation="false" CommandName="Print" CommandArgument='<%# Eval("Bill_Number")%>' Text="Print" class="button button2" TabIndex="8" Width="100px" Height="20px"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>No Record Found</EmptyDataTemplate>
                </asp:GridView>
            </div>
        </fieldset>
    </div>
</asp:Content>

