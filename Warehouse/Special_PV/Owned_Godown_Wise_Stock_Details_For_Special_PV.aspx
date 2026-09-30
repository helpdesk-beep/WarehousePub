<%@ Page Title="" Language="C#" MasterPageFile="~/Special_PV/Special_PV.master" AutoEventWireup="true" CodeFile="~/Special_PV/Owned_Godown_Wise_Stock_Details_For_Special_PV.aspx.cs" Inherits="Special_PV_Owned_Godown_Wise_Stock_Details_For_Special_PV" %>

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
        <fieldset style="border: 1px solid navy; border-radius: 10px 10px 10px 10px;">
            <legend>Godown Special PV Stock Details </legend>
            <div class="row">
                <div class="col-md-2">
                    <label style="margin-top: 10px">Godown</label>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList ID="ddl_gdwn" runat="server" AutoPostBack="true" class="form-control" OnSelectedIndexChanged="ddl_gdwn_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>
            <div div="div" runat="server" style="text-align: center; margin-top: 10px;">
                <asp:Button class="button button2" ID="btnshow" runat="server" Text="Show"
                    TabIndex="11" CssClass="btn btn-warning" OnClick="btnshow_Click"></asp:Button>
            </div>
        </fieldset>
        <div id="divshow" runat="server" visible="false">
            <fieldset>
                <legend>Details</legend>
                <div runat="server">
                    <asp:GridView runat="server" ID="GrdOfficerPreviousInsp" OnRowCommand="GrdOfficerPreviousInsp_RowCommand" ShowFooter="true"
                        AutoGenerateColumns="false" CssClass="table table-bordered table-hover" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr" OnRowDataBound="GrdOfficerPreviousInsp_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No.">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1%>
                                    <asp:HiddenField runat="server" ID="hdnGodown_ID" Value='<%# Eval("Godown_ID") %>' />
                                    <asp:HiddenField runat="server" ID="hdnbranchid" Value='<%# Eval("BranchID") %>' />
                                    <asp:HiddenField runat="server" ID="hdnCommodity_Id" Value='<%# Eval("Commodity_Id") %>' />
                                    <asp:HiddenField runat="server" ID="hdnCropYear" Value='<%# Eval("CropYear") %>' />
                                    <%--<asp:HiddenField runat="server" ID="hdnEmployee_ID" Value='<%# Eval("Employee_ID") %>' />--%>
                                    <asp:HiddenField runat="server" ID="hdnDepositor_ID" Value='<%# Eval("DepositorID") %>' />
                                </ItemTemplate>
                                <ItemStyle Width="1%" />
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Godown_Name" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblGodown_Name" runat="server" Text='<%# Eval("Godown_Name") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Depositor Name" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblDepositor_Name" runat="server" Text='<%# Eval("Depositor_Name") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="commodity" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblcommodity" runat="server" Text='<%# Eval("commodity") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CropYear" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblCropYear" runat="server" Text='<%# Eval("CropYear") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="recbags" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblrecbags" runat="server" Text='<%# Eval("recbags") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="delbags" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lbldelbags" runat="server" Text='<%# Eval("delbags") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Rec_Weight" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblRec_Weight" runat="server" Text='<%# Eval("Rec_Weight") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Del_Weight" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblDel_Weight" runat="server" Text='<%# Eval("Del_Weight") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Bag Balance" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblBag_Balance" runat="server" Text='<%# Eval("Bag_Balance") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Weight Balance" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblBalance" runat="server" Text='<%# Eval("Balance") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="View Statck Wise Details" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Button ID="btnfilloverallinsp" Text="View Statck Wise Details" runat="server" CommandName="Overallinsp" CssClass="btn btn-info" CommandArgument='<%# Container.DataItemIndex %>' />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" Height="20px" Font-Size="10pt" />
                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#F7ECDD" Font-Bold="True" ForeColor="#cb4e48" HorizontalAlign="center"
                            Height="20px" Font-Size="10pt" />
                        <AlternatingRowStyle BackColor="#eeeeee" />
                    </asp:GridView>

                </div>
            </fieldset>
        </div>
    </div>
</asp:Content>

