<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Nccf_Master.master"
    AutoEventWireup="true" CodeFile="~/NCCF/NCCF_WHR_Dtails_For_PSS_PSF.aspx.cs"
    Inherits="NCCF_NCCF_WHR_Dtails_For_PSS_PSF" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%--<link rel="shortcut icon" type="image/x-icon" href="assets/img/favicon.ico" />
    <link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet nofollow" />--%>
    <%--Ashutosh--%>
    <%--Data Table Footer--%>
    <script type="text/javascript">
        var dataTable;

        function initDataTable() {
            var table = $('#<%= GridView1.ClientID %>');

            if ($.fn.DataTable.isDataTable(table)) {
                table.DataTable().clear().destroy();
            }

            dataTable = table.DataTable({
                paging: false,         // pagination off
                searching: true,       // only search bar visible
                ordering: false,       // sorting off
                info: false,           // "showing X of Y entries" hide
                lengthChange: false,   // hide page length dropdown
                responsive: true,
                dom: 'f',              // only search bar (filter input)
            });
        }

        $(document).ready(function () {
            initDataTable();
        });

        if (typeof (Sys) !== "undefined") {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
                initDataTable();
            });
        }
    </script>
    <style>
        /* Fix for dropdown clipping inside DataTable */
        .table-container {
            position: relative;
            overflow-x: auto; /* horizontal scroll maintain */
            overflow-y: visible; /* dropdown visible rahe vertically */
            padding: 5px;
        }

        fieldset {
            border: 2px solid #ddd;
            border-radius: 8px;
            padding: 10px 15px;
            margin-top: 10px;
        }

        legend {
            font-weight: bold;
            font-size: 14px;
            color: #333;
        }

        table.dataTable {
            width: 100% !important;
        }

            table.dataTable td, table.dataTable th {
                white-space: nowrap !important;
            }

        .dataTables_wrapper {
            overflow: hidden; /* wrapper control */
            position: relative;
        }

        /* Dropdown look */
        select.form-control {
            min-width: 130px;
            height: 30px;
            font-size: 13px;
            padding: 2px 6px;
        }

        /* Update button */
        .btn-sm {
            padding: 2px 8px;
            font-size: 13px;
        }

        /* Fix overflow bug causing table to leak out of fieldset */
        .content_wrapper {
            overflow: hidden;
        }
    </style>
    <%--Data Table Footer END--%>
    <%--Data Table Footer END--%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <div class="content_wrapper">
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="a" ForeColor="Red" ShowMessageBox="true" ShowSummary="false" />
        <fieldset>
            <legend>NCCF WHR Details For PSS And PSF</legend>
            <div class="row">
                <div class="col-md-1" style="margin-top: 6px">
                    <asp:Label ID="lblRegion" runat="server" Text="Region" Font-Size="10pt" Font-Bold="true" />
                </div>
                <div class="col-md-2">
                    <asp:DropDownList ID="ddlRegion" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                </div>
                <div class="col-md-1" style="margin-top: 6px">
                    <asp:Label ID="Label1" runat="server" Text="Crop Year" Font-Size="10pt" Font-Bold="true" />
                </div>
                <div class="col-md-2">
                    <asp:DropDownList ID="DdlCropYear" runat="server" CssClass="form-control">
                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                        <asp:ListItem Value="2025-26">2025-26</asp:ListItem>
                        <asp:ListItem Value="2024-25">2024-25</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvCropYear" runat="server"
                        ControlToValidate="DdlCropYear" InitialValue="0" ErrorMessage="Please select Crop Year" ForeColor="Red"
                        ValidationGroup="a" Display="None" />
                </div>
                <div class="col-md-2" style="text-align: center;">
                    <asp:Button runat="server" CssClass="btn btn-success" ValidationGroup="a" Text="Search" ID="btnSearch1" OnClick="btnSearch1_Click" Autopostback="true" />
                </div>
            </div>
        </fieldset>

        <fieldset id="divgrid" runat="server" visible="false">
            <legend>WHR Details</legend>
            <div class="table-responsive">
                <asp:GridView ID="GridView1" runat="server" OnPreRender="GridView1_PreRender"
                    AutoGenerateColumns="False" DataKeyNames="Depositor_WHR_Id" CssClass="table table-bordered table-hover display nowrap"
                    ClientIDMode="Static" OnRowCommand="GridView1_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="S.No.">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="District_Name" HeaderText="District" />
                        <asp:BoundField DataField="District_Id" HeaderText="District_Id" />
                        <asp:BoundField DataField="Godown_Name" HeaderText="Godown_Name" />
                        <asp:TemplateField HeaderText="Depositor WHR Id">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblDepositor_WHR_Id" Text='<%# Eval("Depositor_WHR_Id") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--<asp:BoundField DataField="Depositor_WHR_Id" HeaderText="Depositor_WHR_Id" />--%>
                        <asp:BoundField DataField="Depositor_Name" HeaderText="Depositor_Name" />
                        <asp:BoundField DataField="whrdate" HeaderText="whrdate" />
                        <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity_Name" />
                        <asp:BoundField DataField="Bags" HeaderText="Bags" DataFormatString="{0:N0}" />
                        <asp:BoundField DataField="Qty" HeaderText="Qty" DataFormatString="{0:N0}" />
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="PSS" Value="PSS"></asp:ListItem>
                                    <asp:ListItem Text="PSF" Value="PSF"></asp:ListItem>
                                    <asp:ListItem Text="OTHER" Value="OTHER"></asp:ListItem>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <asp:Button ID="btnUpdate" runat="server" CausesValidation="false" CommandName="EditRow" CommandArgument='<%# Eval("Depositor_WHR_Id")%>' Text="Update" CssClass="BTNBLUE" />
                                <%--  <asp:LinkButton ID="btnUpdate" runat="server" Text="Update"
                                    CssClass="btn btn-primary btn-sm"
                                    CommandName="EditRow"
                                    CommandArgument='<%# Eval("Depositor_WHR_Id") %>' />--%>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </fieldset>
    </div>
</asp:Content>
