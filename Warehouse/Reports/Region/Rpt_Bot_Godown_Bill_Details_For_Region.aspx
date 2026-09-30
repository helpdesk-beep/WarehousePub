<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Region_Master.master" AutoEventWireup="true" CodeFile="~/Reports/Region/Rpt_Bot_Godown_Bill_Details_For_Region.aspx.cs" Inherits="Reports_Region_Rpt_Bot_Godown_Bill_Details_For_Region" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../../assets/New/css/style.css" rel="stylesheet" />
    <%--<link href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" />--%>
    <link href="../../assets/New/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../assets/New/css/bootstrap.min2.css" rel="stylesheet" />
    <link href="../../CSS/style.css" rel="stylesheet" />
    <link href="../../assets/New/css/font-awesome.min.css" rel="stylesheet" />
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

        /* Use a media query to add a breakpoint at 800px: */
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
        function confirmUpdate(btn) {
            // find the row
            var row = btn.closest("tr");

            // find the dropdown inside this row
            var ddl = row.querySelector("select[id*='ddlApprovalStatus']");
            var selectedValue = ddl ? ddl.value : "";

            // build message
            var msg = "";
            if (selectedValue === "Y") {
                msg = "Are you sure you want to approve this record?";
            } else if (selectedValue === "N") {
                msg = "Are you sure you want to reject this record?";
            } else {
                msg = "Are you sure you want to update this record?";
            }

            return confirm(msg);
        }
    </script>
    <script type="text/javascript">
        function confirmUpdate(btn) {
            var row = btn.closest("tr");

            var ddl = row.querySelector("select[id*='ddlApprovalStatus']");
            var remark = row.querySelector("input[id*='txtRemark']");

            if (!ddl) return true;

            if (ddl.value === "Y") {
                return confirm("Are you sure you want to approve this record?");
            }
            else if (ddl.value === "N") {
                if (!remark || remark.value.trim() === "") {
                    alert("Please enter remark before rejecting the record.");
                    return false;
                }
                return confirm("Are you sure you want to reject this record?");
            }
            else {
                return confirm("Are you sure you want to update this record?");
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <fieldset>
            <legend>Bills Details</legend>
            <div class="row">
                <div class="col-md-12">
                    <div class="table-responsive">
                        <asp:GridView runat="server" ID="GrdRegion" ShowFooter="false" OnRowCommand="GrdRegion_RowCommand"
                            CssClass="table table-bordered table-hover datatable" ClientIDMode="Static" AutoGenerateColumns="False">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="District Name" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblDistrict_Name" Text='<%# Eval("District_Name") %>'></asp:Label>
                                        <asp:HiddenField runat="server" ID="hdnDistrict_Id" Value='<%#Eval("District_Id")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Branch Name" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblBranch_Name" Text='<%# Eval("Branch_Name") %>'></asp:Label>
                                        <asp:HiddenField runat="server" ID="hdnBranch_Id" Value='<%#Eval("BranchId")%>' />
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
                                <asp:TemplateField HeaderText="Current Godown Capacity" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblGodownCapacity" Text='<%# Eval("GodownCapacity") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Current Available Capacity" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblAvlQty" Text='<%# Eval("AvlQty") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Current Vacant Capacity" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblVacant_Capacity" Text='<%# Eval("Vacant_Capacity") %>'></asp:Label>
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
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Net Amount" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblNet_Amount" Text='<%# Eval("Net_Amount") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="TDS Amount" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblTDS_Amount" Text='<%# Eval("TDS_Amount") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Other Deduction" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtOther_Deduction" runat="server" Text='<%# Eval("Other_Deduction") %>' TabIndex="8" Width="100px" Height="20px" AutoPostBack="true"
                                            OnTextChanged="txtOther_Deduction_TextChanged"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Payable Amount" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblPayable_Amount" Text='<%# Eval("Payable_Amount") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Approve" HeaderStyle-BackColor="LightBlue" ItemStyle-VerticalAlign="Middle">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlApprovalStatus" runat="server" Width="100px" Height="20px" AutoPostBack="true" OnSelectedIndexChanged="ddlApprovalStatus_SelectedIndexChanged">
                                            <asp:ListItem Value="Y">Approve</asp:ListItem>
                                            <asp:ListItem Value="N">Reject</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:TextBox ID="txtRemark" runat="server" Visible="false" Width="125" CssClass="form-control" Text="NA" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Update" HeaderStyle-BackColor="LightBlue" ItemStyle-VerticalAlign="Middle">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnUpdate" runat="server" CausesValidation="false" CommandName="EditRow" CommandArgument='<%# Eval("Bill_Number")%>' Text="Update" class="button button2" TabIndex="8" Width="100px" Height="20px" OnClientClick="return confirmUpdate(this);" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Print Bill" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnPrint" runat="server" CausesValidation="false" CommandName="Print" CommandArgument='<%# Eval("Bill_Number")%>' Text="Print" class="button button1" TabIndex="8" Width="50px" Height="20px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>No Record Found</EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                </div>
            </div>

        </fieldset>
    </div>
</asp:Content>

