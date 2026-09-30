<%@ Page Title="" Language="C#" MasterPageFile="~/Special_PV/State/StateMaster_SP.master" AutoEventWireup="true" CodeFile="Special_PV_REgion_Wise_With_Remark.aspx.cs" Inherits="Special_PV_REgion_Wise_With_Remark" %>

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
                    <asp:DropDownList ID="ddlBranch" runat="server" AutoPostBack="true" class="form-control">
                        <asp:ListItem Text="All" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row" style="margin-top:20px">
                <div class="col-md-5"></div>
                <div class="col-md-2">
                     <asp:Button runat="server" ID="btnsearch" CssClass="btn btn-info btn-block" ValidationGroup="a" Text="Search" OnClick="btnsearch_Click" />
                </div>
            </div>
        </fieldset>
        <fieldset id="divgodown" runat="server" visible="false">
            <legend>Details</legend>
            <div class="row">
                <div class="table-responsive">
                    <asp:GridView runat="server" ID="grdgodown" ShowFooter="true"
                        OnRowDataBound="grdgodown_RowDataBound" OnRowCreated="grdgodown_RowCreated" OnDataBound="grdgodown_DataBound"
                        AutoGenerateColumns="false" CssClass="table table-bordered table-hover" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No.">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1%>
                                    <asp:HiddenField runat="server" ID="hdnBranchId" Value='<%# Eval("BranchId") %>' />
                                </ItemTemplate>
                                <ItemStyle Width="1%" />
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>
                            <asp:BoundField DataField="DepotName" HeaderText="DepotName" />
                            <asp:BoundField DataField="Godown_Name" HeaderText="Godown Name"/>
                            <asp:TemplateField HeaderText="Clossing Bags Date on 31/12/2024">
                                <ItemTemplate>
                                    <asp:Label ID="lblTotalBags" runat="server" Text='<%# Eval("TotalBags") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Blance As Per PV By Branch">
                                <ItemTemplate>
                                    <asp:Label ID="lblBlanceasperbranch" runat="server" Text='<%# Eval("Blance_As_Per_PV_By_Branch") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Diffirence Online and PV">
                                <ItemTemplate>
                                    <asp:Label ID="lblDiffirenceOnlineandPV" runat="server" Text='<%# Eval("DiffirenceOnlineandPV") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="BM Remark">
                                <ItemTemplate>
                                    <asp:Label ID="lblBM_Remark" runat="server" Text='<%# Eval("BM_Remark") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="RM Remark">
                                <ItemTemplate>
                                    <asp:Label ID="lblRMREmark" runat="server" Text='<%# Eval("Rm_Remark") %>'></asp:Label>
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

