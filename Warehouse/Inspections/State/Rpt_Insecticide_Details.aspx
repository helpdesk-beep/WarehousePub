<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Rpt_Insecticide_Details.aspx.cs" Inherits="Inspections_State_Rpt_Insecticide_Details" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Insecticide Details</title>
    <link href="../assets/New/css/bootstrap.min.css" rel="stylesheet" />
    <link href="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/css/select2.min.css" rel="stylesheet" />
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/js/select2.min.js"></script>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css" rel="stylesheet" />

    <style>
        .table {
            width: 100%;
            font-size: 14px;
        }

            .table th {
                background-color: #3c8dbc;
                color: white;
                text-align: center;
                font-weight: bold;
            }

            .table td {
                text-align: center;
            }

        .table-hover tbody tr:hover {
            background-color: #f5f5f5;
        }

        .table-responsive {
            width: 100%;
            overflow-x: auto;
        }

        .gridheader {
            background: #2f4050;
            color: white;
        }

        .gridaltrow {
            background: #f9f9f9;
        }

        .gridrow {
            background: white;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="content-wrapper">
            <fieldset>
                <legend>Insecticide Details</legend>

                <div style="margin-bottom: 10px;">
                    <asp:Button ID="btnBack" runat="server" Text=" Back"
                        CssClass="btn btn-primary"
                        OnClick="btnBack_Click" />
                    <i class="fas fa-arrow-left"></i>
                </div>

                <asp:GridView ID="grdDetails" runat="server" AutoGenerateColumns="False"
                    CssClass="table table-bordered table-striped table-hover"
                    HeaderStyle-CssClass="gridheader"
                    RowStyle-CssClass="gridrow"
                    AlternatingRowStyle-CssClass="gridaltrow">
                    <%--  <asp:GridView ID="grdDetails" runat="server"
                    AutoGenerateColumns="false" AlternatingRowStyle-CssClass="alt" CssClass="Grid">--%>
                    <Columns>
                        <asp:BoundField DataField="Regionnm" HeaderText="Region" />
                        <asp:BoundField DataField="District_Name" HeaderText="District Name" />
                        <asp:BoundField DataField="DepotName" HeaderText="Depot Name" />
                        <asp:BoundField DataField="Insecticide_Name" HeaderText="Insecticide Name" />
                        <asp:BoundField DataField="RO_Transfer_to_Branch" HeaderText="RO Transfer to Branch" />
                        <asp:BoundField DataField="Consumption_TO_OWN_Godown" HeaderText="Consumption TO OWN Godown" />
                        <asp:BoundField DataField="Transfer_To_JVS_Godown" HeaderText="Transfer To JVS Godown" />
                        <asp:BoundField DataField="Pending_At_Branch" HeaderText="Pending At Branch" />
                    </Columns>
                </asp:GridView>
            </fieldset>
        </div>
    </form>
</body>
</html>
