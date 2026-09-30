<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="~/BranchPages/Godown_And_Stack_Wise_Fumigation_Entry.aspx.cs" Inherits="BranchPages_Godown_And_Stack_Wise_Fumigation_Entry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Godown & Stack Management</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet">
    <style>
        body {
            background-color: #f8f9fa;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        }

        @mixin make-col($size: false, $columns: $grid-columns) {
            @if $size {
                flex: 0 0 auto;
                width: percentage(divide($size, $columns));
            }

            @else {
                flex: 1 1 0;
                max-width: 100%;
            }
        }

        .card {
            border: none;
            border-radius: 12px;
            box-shadow: 0 4px 12px rgba(0,0,0,0.1);
        }

        .card-header {
            background: linear-gradient(135deg, #1e3c72, #2a5298);
            color: white;
            border-top-left-radius: 12px !important;
            border-top-right-radius: 12px !important;
        }

        .form-label {
            font-weight: 600;
            color: #495057;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>

    <div class="container">
        <div class="row justify-content-center">
            <div class="col-lg-12">
                <div class="card">
                    <div class="card-header text-center py-3">
                        <h4 class="mb-0">Stack-Wise Fumigation Entry</h4>
                    </div>
                    <div class="card-body p-4">

                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>

                                <div class="row g-3">
                                    <div class="col-md-6">
                                        <label for="ddlGodown" class="form-label">Select Godown Name</label>
                                        <asp:DropDownList ID="ddlGodown" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlGodown_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-md-6">
                                        <label for="ddlStack" class="form-label">Select Stack</label>
                                        <asp:DropDownList ID="ddlStack" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlStack_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>

                                    <hr class="my-4 text-muted" />

                                    <div class="col-md-6">
                                        <label class="form-label">Stack Name</label>
                                        <asp:TextBox ID="txtStackName" runat="server" CssClass="form-control bg-light" ReadOnly="true" placeholder="Automatic filled"></asp:TextBox>
                                    </div>

                                    <div class="col-md-6">
                                        <label class="form-label">Available Quantity</label>
                                        <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control bg-light" ReadOnly="true" placeholder="0.00"></asp:TextBox>
                                    </div>

                                    <div class="col-md-6">
                                        <label for="txtlastFumigationDate" class="form-label">Last Fumigation Date</label>
                                        <asp:TextBox ID="txtlastFumigationDate" runat="server" TextMode="Date" CssClass="form-control" onkeydown="return false;" onpaste="return false;"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6">
                                        <label for="txtFumigationDate" class="form-label">Fumigation Date</label>
                                        <asp:TextBox ID="txtFumigationDate" runat="server" TextMode="Date" CssClass="form-control" onkeydown="return false;" onpaste="return false;"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6">
                                        <label for="txtAlpQty" class="form-label">Used ALP Qty (Tablets)</label>
                                        <asp:TextBox ID="txtAlpQty" runat="server" TextMode="Number" CssClass="form-control" min="1" placeholder="Enter number of tablets" onblur="validateQty(this);"></asp:TextBox>
                                    </div>

                                    <div class="col-md-6">
                                        <label for="ddlAlpBrand" class="form-label">ALP Brand (Insecticide)</label>
                                        <asp:DropDownList ID="ddlAlpBrand" runat="server" CssClass="form-select">
                                        </asp:DropDownList>
                                    </div>
                                </div>

                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <div class="d-grid gap-2 mt-4">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit Details" CssClass="btn btn-primary btn-lg fs-6" OnClick="btnSubmit_Click" Style="background: linear-gradient(135deg, #1e3c72, #2a5298); border: none;" />
                        </div>

                    </div>
                    <div class="card mt-4">
                        <div class="card-header text-center py-2">
                            <h5 class="mb-0">Fumigation Entry Details</h5>
                        </div>
                        <div class="card-body">
                            <asp:GridView ID="gvFumigation" runat="server" CssClass="table table-bordered table-striped table-hover" AutoGenerateColumns="false" EmptyDataText="No Record Found">
                                <Columns>
                                    <asp:BoundField DataField="Godown_Name" HeaderText="Godown Name" />
                                    <asp:BoundField DataField="Stack_Details" HeaderText="Stack Details" />
                                    <asp:BoundField DataField="Available_Qty" HeaderText="Available Qty" />
                                    <asp:BoundField DataField="Last_Fumigation_Date" HeaderText="Last Fumigation Date" />
                                    <asp:BoundField DataField="Fumigation_Date" HeaderText="Fumigation Date" />
                                    <asp:BoundField DataField="Used_ALP_Qty" HeaderText="Used ALP Qty" />
                                    <asp:BoundField DataField="Insecticide_Brand" HeaderText="Insecticide Brand" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        function validateQty(input) {
            var val = parseInt(input.value);
            if (isNaN(val) || val <= 0) {
                alert("ALP Quantity 0 ya usse kam nahi ho sakti! Kam se kam 1 tablet enter karein.");
                input.value = ""; // Box ko khali kar dega taaki sahi value bhari jaye
            }
        }
    </script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>
</asp:Content>

