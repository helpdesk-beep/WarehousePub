<%@ Page Title="" Language="C#" MasterPageFile="~/WareHouseMaster.master" AutoEventWireup="true" CodeFile="~/Reports/NewStorageCapacityReport/Godown_Wise_Rabi_Kharif_Multiple_CropYear_Report.aspx.cs" Inherits="Reports_NewStorageCapacityReport_Godown_Wise_Rabi_Kharif_Multiple_CropYear_Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPageHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPageBody" runat="Server">
    <style>
        .btnMargin {
            margin-bottom: 10px !important;
        }
    </style>
    <style>
        .Grid {
            background-color: #fff;
            margin: 5px 0 10px 0;
            border: solid 1px #525252;
            border-collapse: collapse;
            font-family: Calibri;
            color: #474747;
            width: 100%;
        }

            .Grid td {
                padding: 2px;
                border: solid 1px #c1c1c1;
            }

            .Grid th {
                padding: 4px 2px;
                color: #fff;
                background: #00aad2 url(Images/grid-header.png) repeat-x top;
                border-left: solid 1px #525252;
                font-size: 15px;
                text-align: center;
            }

            .Grid .alt {
                background: #fcfcfc url(Images/grid-alt.png) repeat-x top;
            }

            .Grid .pgr {
                background: #00aad2 url(Images/grid-pgr.png) repeat-x top;
            }

                .Grid .pgr table {
                    margin: 3px 0;
                }

                .Grid .pgr td {
                    border-width: 0;
                    padding: 0 6px;
                    border-left: solid 1px #666;
                    font-weight: bold;
                    color: #fff;
                    line-height: 12px;
                }

                .Grid .pgr a {
                    color: Gray;
                    text-decoration: none;
                }

                    .Grid .pgr a:hover {
                        color: #000;
                        text-decoration: none;
                    }
    </style>
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
            Width: 200px;
            height: 50px;
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
    </style>

    <div class="row justify-content-center">
        <div class="col-md-12">
            <div class="p-3 mb-4 bg-light shadow-sm rounded">
                <h3 class="text-center text-danger mb-0">Godown Wise Rabi-Kharif Crop Year Details</h3>
            </div>
        </div>

        <div class="col-md-12">
            <div class="row form-group">
                <div class="col-md-2 fw-bold text-end">
                    <asp:Label ID="Label2" runat="server" Text="Select CropYear"></asp:Label>
                </div>
                <div class="col-md-2">
                    <asp:ListBox ID="ddlCropYear" runat="server" SelectionMode="Multiple" CssClass="checkbox-multiselect form-control"></asp:ListBox>
                    <asp:RequiredFieldValidator ID="rfvCropYear" runat="server" ControlToValidate="ddlCropYear"
                        ErrorMessage="Select Crop Year" ForeColor="Red" Display="Dynamic" ValidationGroup="VGRpt" />
                </div>

                <div class="col-md-2 fw-bold text-end">
                    <asp:Label ID="Label1" runat="server" Text="Select Commodity"></asp:Label>
                </div>
                <div class="col-md-2">
                    <asp:ListBox ID="ddlComodity" runat="server" SelectionMode="Multiple" CssClass="checkbox-multiselect form-control"></asp:ListBox>
                    <asp:RequiredFieldValidator ID="rfvCommodity" runat="server" ControlToValidate="ddlComodity"
                        ErrorMessage="Select Commodity" ForeColor="Red" Display="Dynamic" ValidationGroup="VGRpt" />
                </div>

                <div class="col-md-2 fw-bold text-end">
                    <asp:Label ID="lblDate" runat="server" Text="Date (DD-MM-YYYY) : "></asp:Label>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtdate" runat="server" CssClass="datepicker form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvDate" runat="server" ControlToValidate="txtdate"
                        ErrorMessage="Select Date" ForeColor="Red" Display="Dynamic" ValidationGroup="VGRpt" />
                </div>
            </div>

            <div class="row mt-3">
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-info show-loader" OnClick="btnSubmit_Click" ValidationGroup="VGRpt" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger" OnClick="btnCancel_Click" CausesValidation="false" />
                </div>
            </div>
        </div>

        <div class="col-md-12 mt-4">
            <asp:GridView ID="gvGodownReport" runat="server" AutoGenerateColumns="True" ShowFooter="true"
                CssClass="Grid table-bordered table-hover" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
                <%-- <Columns>
                    <asp:TemplateField HeaderText="S.No.">
                        <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                    </asp:TemplateField>
                </Columns>--%>
            </asp:GridView>
        </div>
    </div>

    <script>
        var grid = $('#<%= gvGodownReport.ClientID %>');
        grid.prepend($("<thead></thead>").append(grid.find("tr:first")));

        var t = grid.DataTable({
            "dom": 'Bfrtip',
            "buttons": ['excel', 'pdf', 'print'],
            "columnDefs": [{
                "searchable": false,
                "orderable": false, // Prevents users from sorting the sequence column itself
                "targets": 0        // Targets the first column
            }],
            "order": [[1, 'asc']]   // Default sort on the second column
        });

        // This is the "Magic" part for the sequence
        t.on('order.dt search.dt', function () {
            t.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                cell.innerHTML = i + 1;
            });
        }).draw();
        BindDatatable(grid);
    </script>



</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPageWidget" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPageScript" runat="Server">
</asp:Content>

