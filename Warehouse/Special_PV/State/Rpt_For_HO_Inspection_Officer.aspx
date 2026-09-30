<%@ Page Title="" Language="C#" MasterPageFile="~/Special_PV/State/StateMaster_SP.master" AutoEventWireup="true" CodeFile="~/Special_PV/State/Rpt_For_HO_Inspection_Officer.aspx.cs" Inherits="Special_PV_State_Rpt_For_HO_Inspection_Officer" %>

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
        <fieldset>
            <legend>Region Wise Special PV</legend>
            <div class="row">
                <div class="col-md-2">
                    <label>Region</label>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList ID="ddlRegion" runat="server" AutoPostBack="True" class="form-control" OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
                <div class="col-md-2">
                    <label>District</label>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList ID="ddldistrict" runat="server" AutoPostBack="True" class="form-control" OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
                <div class="col-md-2">
                    <label>Branch</label>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList ID="ddlBranch" runat="server" AutoPostBack="true" class="form-control" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                        <asp:ListItem Text="All" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </fieldset>
        <fieldset id="divRegion" runat="server" visible="false">
            <legend>Details</legend>
            <div class="row">
                <div class="table-responsive">
                    <asp:GridView runat="server" ID="grdRegion" ShowFooter="true"
                        AutoGenerateColumns="false" CssClass="Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No.">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1%>
                                </ItemTemplate>
                                <ItemStyle Width="1%" />
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>
                            <asp:BoundField DataField="District_Name" HeaderText="District Name" />
                            <asp:BoundField DataField="DepotName" HeaderText="Branch Name" />
                            <asp:BoundField DataField="Godown_Name" HeaderText="Godown Name" />
                            <asp:BoundField DataField="Depositor_Name" HeaderText="Depositor Name" />
                            <asp:BoundField DataField="Commodity" HeaderText="Commodity Name" />
                            <asp:BoundField DataField="Officer_Name" HeaderText="Officer Name" />
                            <asp:BoundField DataField="Mobile_No" HeaderText="Mobile No" />
                            <asp:BoundField DataField="CropYear" HeaderText="Crop Year" />
                            <%--<asp:BoundField DataField="Godown_Name" HeaderText="Godown Name" />
                            <asp:BoundField DataField="Hired_Type" HeaderText="Hired Type" />--%>
                            <asp:TemplateField HeaderText="Clossing Bags Date on 31/12/2024">
                                <ItemTemplate>
                                    <asp:Label ID="lblClossing_Bags" runat="server" Text='<%# Eval("Clossing_Bags") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Blance As Per PV By Branch">
                                <ItemTemplate>
                                    <asp:Label ID="lblPV_By_BM" runat="server" Text='<%# Eval("PV_By_BM") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Diffirence Online and PV">
                                <ItemTemplate>
                                    <asp:Label ID="lblDiffrence" runat="server" Text='<%# Eval("Diffrence") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Jama Bags After 31/12/2024">
                                <ItemTemplate>
                                    <asp:Label ID="lblJama_Bags" runat="server" Text='<%# Eval("Jama_Bags") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Paid Bags After 31/12/2024">
                                <ItemTemplate>
                                    <asp:Label ID="txtPaid_Bags" runat="server" Text='<%# Eval("Paid_Bags") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="No Of Bags As Per PV">
                                <ItemTemplate>
                                    <asp:Label ID="txtNo_Of_Bags_As_Per_PV" runat="server" Text='<%# Eval("No_Of_Bags_As_Per_PV") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Insp Date">
                                <ItemTemplate>
                                    <asp:Label ID="txtInsp_Date" runat="server" Text='<%# Eval("Insp_Date") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remark By HO">
                                <ItemTemplate>
                                    <asp:Label ID="lblRemark_By_HO" runat="server" Text='<%# Eval("Remark_By_HO") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Summary Remark">
                                <ItemTemplate>
                                    <asp:Label ID="lblSummary_Remark" runat="server" Text='<%# Eval("Summary_Remark") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </fieldset>
    </div>
</asp:Content>

