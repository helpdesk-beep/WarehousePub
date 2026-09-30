<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="~/BranchPages/WebBridgeMappingWithGodown.aspx.cs" Inherits="BranchPages_WebBridgeMappingWithGodown" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../assets/New/css/bootstrap.min.css" rel="stylesheet" />
    <%--Updated By Ashutosh--%>
    <script src="https://code.jquery.com/jquery-1.11.1.min.js"></script>
    <link href="../assets/New/css/select2.min.css" rel="stylesheet" />
    <script type="text/javascript" src="../assets/New/js/select2.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $("[id*=ddlgdwn]").select2();
        });
    </script>
    <script type="text/javascript">
        $(function () {
            $("[id*=ddlWB]").select2();
        });
    </script>
    <%--Updated By Ashutosh End--%>
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

        .auto-style3 {
            position: relative;
            min-height: 1px;
            float: left;
            width: 50%;
            left: 0px;
            top: 0px;
            padding-left: 15px;
            padding-right: 15px;
        }

        .table-hover tbody tr:hover {
            background-color: #f2f7ff;
        }

        .btn {
            margin: 2px;
        }

        .thead-dark th {
            background-color: #caf0f8;
            color: white;
            font-size: 14px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container mt-4">
        <fieldset class="border p-3">
            <legend class="w-auto px-2 font-weight-bold">Godown – Weight Bridge Mapping
            </legend>
            <div class="row mt-3">
                <!-- Godown Dropdown -->
                <div class="col-md-4">
                    <label class="font-weight-bold">Select Godown</label>
                    <asp:DropDownList ID="ddlgdwn" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                </div>
                <!-- Weight Bridge Dropdown -->
                <div class="col-md-4">
                    <label class="font-weight-bold">Select Weight Bridge</label>
                    <asp:DropDownList ID="ddlWB" runat="server"
                        CssClass="form-control">
                    </asp:DropDownList>

                    <asp:RequiredFieldValidator ID="rfvWB" runat="server"
                        ControlToValidate="ddlWB"
                        InitialValue="0"
                        ErrorMessage="Please select Weight Bridge"
                        ForeColor="Red" ValidationGroup="a" />
                </div>
                <!-- Save Button -->
                <div class="col-md-2 align-self-end" style="margin-top: 20px">
                    <asp:Button ID="btnSave" runat="server"
                        Text="SAVE"
                        CssClass="btn btn-success btn-block"
                        ValidationGroup="a"
                        OnClick="btnSave_Click" />
                </div>
            </div>
            <asp:Label ID="lblMsg" runat="server"
                CssClass="text-success font-weight-bold mt-3 d-block">
            </asp:Label>
        </fieldset>
        <fieldset>
            <legend>Details</legend>
            <div class="col-md-12">
                <asp:GridView ID="gvMapping" runat="server" CssClass="table table-bordered table-hover" AutoGenerateColumns="False" EmptyDataText="No Mapping Found"
                    HeaderStyle-CssClass="thead-dark" GridLines="None" OnRowCommand="gvMapping_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="S.No">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Godown_name" HeaderText="Godown Name" />
                        <asp:BoundField DataField="WB_Name" HeaderText="Weight Bridge Name" />
                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkRemove" runat="server" Text="Remove" CssClass="btn btn-danger btn-sm" CommandName="RemoveMapping"
                                    CommandArgument='<%# Eval("Mapping_ID") %>' OnClientClick="return confirm('Are you sure you want to remove this mapping?');">
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </fieldset>
    </div>
</asp:Content>

