<%@ Page Language="C#" AutoEventWireup="true" CodeFile="~/Inspections/State/Rpt_Stack_Moisture_FCI.aspx.cs" Inherits="Inspections_State_Rpt_Stack_Moisture_FCI" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Stack Moisture Report</title>

    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />

    <style>
        body {
            background: #f4f6f9;
        }

        .grid-container {
            max-height: 600px;
            overflow: auto;
            background: white;
            border-radius: 10px;
        }

            .grid-container thead th {
                position: sticky;
                top: 0;
                z-index: 10;
                background-color: #212529;
                color: white;
            }

        .card-box {
            border-radius: 12px;
            padding: 15px;
            background: white;
            box-shadow: 0 2px 10px rgba(0,0,0,0.1);
        }

        .amount {
            font-size: 22px;
            font-weight: bold;
        }
    </style>
</head>

<body>
    <form runat="server">
        <div class="container-fluid p-4">

            <div class="d-flex justify-content-between mb-3">
                <h3>📊 Stack Moisture & FCI Report</h3>

                <asp:Button ID="btnExport" runat="server" Text="Export Excel"
                    CssClass="btn btn-success" OnClick="btnExport_Click" />
            </div>

            <!-- Summary Cards -->
            <div class="row mb-3">
                <div class="col-md-3">
                    <div class="card-box">
                        <div>Total Stack</div>
                        <div class="amount">
                            <asp:Label ID="lblTotalStack" runat="server" /></div>
                    </div>
                </div>

                <div class="col-md-3">
                    <div class="card-box">
                        <div>Moisture Entry</div>
                        <div class="amount">
                            <asp:Label ID="lblMoisture" runat="server" /></div>
                    </div>
                </div>

                <div class="col-md-3">
                    <div class="card-box">
                        <div>Submitted to FCI</div>
                        <div class="amount">
                            <asp:Label ID="lblFCI" runat="server" /></div>
                    </div>
                </div>
            </div>

            <!-- Grid -->
            <div class="grid-container">
                <asp:GridView ID="gvReport" runat="server" AutoGenerateColumns="False"
                    CssClass="table table-bordered table-sm"
                    OnPreRender="gvReport_PreRender">

                    <Columns>
                        <asp:BoundField DataField="S.No" HeaderText="S.No" />
                        <asp:BoundField DataField="Godown Name" HeaderText="Godown Name" />
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <%# "Prev Stack (" + ViewState["PrevDate"] + " तक)" %>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%# Eval("Prev Stack") %>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <HeaderTemplate>
                                <%# "Current Stack (" + ViewState["CurrDate"] + " तक)" %>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%# Eval("Current Stack") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Total Stack" HeaderText="Total Stack" />
                        <asp:BoundField DataField="Moisture Entry" HeaderText="Moisture Entry" />
                        <asp:BoundField DataField="Submit To FCI" HeaderText="Submit To FCI" />
                    </Columns>

                </asp:GridView>
            </div>

        </div>
    </form>
</body>
</html>
