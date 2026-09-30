<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/PrivateWarehouse.master" AutoEventWireup="true" CodeFile="~/WarehouseLevel/Rent_Bill/Godown_Rent_Bill_For_NCCF.aspx.cs" Inherits="WarehouseLevel_Rent_Bill_Godown_Rent_Bill_For_NCCF" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!-- Bootstrap 5 -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        .section-card {
            border: 2px solid #0b5ed7;
            background: #fff;
            border-radius: 12px;
            box-shadow: 0 0 10px rgba(0,0,0,0.1);
            padding: 20px;
            margin: 20px auto;
            max-width: 1700px;
        }


        .section-header {
            background: #0bb6e6;
            color: #fff;
            font-weight: 600;
            font-size: 1.2rem;
            text-align: center;
            padding: 8px 0;
            border-radius: 8px;
            margin-bottom: 20px;
        }

        .form-label {
            font-weight: 600;
            color: navy;
            font-size: 0.9rem;
        }

        .form-select, .form-control {
            height: 38px;
            font-size: 0.95rem;
        }

        .BTNBLUE {
            background-color: #0bb6e6 !important;
            color: white !important;
            border: none;
            border-radius: 6px;
            padding: 6px 18px;
            font-weight: 500;
        }

            .BTNBLUE:hover {
                background-color: #088bb1 !important;
            }

        .scroll-box {
            max-height: 300px;
            overflow-y: auto;
            overflow-x: hidden;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="section-card">

        <div class="text-center mb-3">
            <asp:Label ID="lblmsg" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
        </div>
        <div class="text-center mt-2">
            <asp:Label ID="Lblmsg2" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>
        </div>
        <div class="section-header">Godown/Silo Rent Bill For NCCF</div>

        <div class="row justify-content-center mb-3">
            <div class="col-md-6 text-center">
                <label class="form-label" for="ddlGodownType">Godown Type</label>
                <asp:DropDownList ID="ddlGodownType" runat="server" CssClass="form-select text-center"
                    AutoPostBack="True" OnSelectedIndexChanged="ddlGodownType_SelectedIndexChanged" Enabled="false">
                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                    <asp:ListItem Value="1">Joint Venture Scheme (JVS)</asp:ListItem>
                    <asp:ListItem Value="3">Tribal Scheme</asp:ListItem>
                    <asp:ListItem Value="4">Silo Bags</asp:ListItem>
                    <asp:ListItem Value="5">CAP-PMS</asp:ListItem>
                    <asp:ListItem Value="15">BOT</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>

        <!-- JVS Godown Rent Section -->
        <div id="trJVSGodownRent" runat="server" visible="false" class="mt-4">
            <div class="section-header">JVS Godown Rent</div>

            <div class="scroll-box mb-3">
                <div class="row g-3">
                    <div class="col-md-6">
                        <label class="form-label">Godown</label>
                        <asp:DropDownList ID="ddlgodown" runat="server" CssClass="form-select"
                            AutoPostBack="True" OnSelectedIndexChanged="ddlgodown_SelectedIndexChanged" Enabled="false">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-6">
                        <label class="form-label">Commodity Name</label>
                        <asp:DropDownList ID="ddlcomodity" runat="server" CssClass="form-select"
                            AutoPostBack="True" OnSelectedIndexChanged="ddlcomodity_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-6">
                        <label class="form-label">Crop Year</label>
                        <asp:DropDownList ID="ddlCropYear" runat="server" CssClass="form-select"
                            AutoPostBack="True" OnSelectedIndexChanged="ddlCropYear_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <label class="form-label">Financial Year</label>
                        <asp:DropDownList ID="ddlFyear" runat="server" CssClass="form-select">
                            <asp:ListItem Value="0" Text="Financial Year"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <label class="form-label">Month</label>
                        <asp:DropDownList ID="ddlmonth" runat="server" CssClass="form-select"
                            AutoPostBack="True" OnSelectedIndexChanged="ddlmonth_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-6">
                        <label class="form-label">Rate (Month/Day)</label>
                        <div class="input-group">
                            <asp:TextBox ID="txtcomrate" runat="server" CssClass="form-control"
                                AutoPostBack="true" OnTextChanged="txtcomrate_TextChanged"></asp:TextBox>
                            <asp:TextBox ID="txtCPRate" runat="server" CssClass="form-control" ReadOnly="true" Enabled="false"></asp:TextBox>
                        </div>
                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="txtcomrate" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtCPRate" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                    </div>
                </div>
            </div>

            <div class="text-center mt-3">
                <asp:Button ID="btnSumbmitRent" runat="server" Text="Check Stock Balance" CssClass="BTNBLUE me-2" OnClick="btnSumbmitRent_Click" />
                <asp:Button ID="brnCancel" runat="server" Text="Close" CssClass="BTNBLUE" OnClick="brnCancel_Click1" />
            </div>
        </div>

        <!-- Hired Godown Rent Section -->
        <div id="trHiredGodownRent" runat="server" visible="false" class="mt-4">
            <div class="section-header">Hired Godown Rent</div>

            <div class="scroll-box mb-3">
                <div class="row g-3">
                    <div class="col-md-6">
                        <label class="form-label">From Date</label>
                        <asp:TextBox ID="txtfdate" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtfdate_TextChanged"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtfdate" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                    </div>
                    <div class="col-md-6">
                        <label class="form-label">To Date</label>
                        <asp:TextBox ID="txttodate" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txttodate_TextChanged1"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txttodate" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                    </div>
                    <div class="col-md-6">
                        <label class="form-label">Godown</label>
                        <asp:DropDownList ID="ddlGodown2" runat="server" CssClass="form-select"
                            AutoPostBack="True" OnSelectedIndexChanged="ddlGodown2_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-6">
                        <label class="form-label">Financial Year</label>
                        <asp:DropDownList ID="ddlFyear2" runat="server" CssClass="form-select"
                            AutoPostBack="True" OnSelectedIndexChanged="ddlFyear2_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-6">
                        <label class="form-label">Rate (Month/Day MT)</label>
                        <div class="input-group">
                            <asp:TextBox ID="txtgratePM" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtgratePM_TextChanged"></asp:TextBox>
                            <asp:TextBox ID="txtgratePD" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtgratePD_TextChanged"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <label class="form-label">Storage Capacity (In MT)</label>
                        <asp:TextBox ID="txtSCapacity" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="col-md-6">
                        <label class="form-label">Rent</label>
                        <asp:TextBox ID="txtRent" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                </div>
            </div>

            <div class="text-center mt-3">
                <asp:Button ID="btnHSubmit" runat="server" Text="Check Stock Balance" CssClass="BTNBLUE me-2" OnClick="btnHSubmit_Click" />
                <asp:Button ID="btnHCancel" runat="server" Text="Close" CssClass="BTNBLUE" OnClick="btnHCancel_Click" />
            </div>
        </div>


        <!-- Rent Bill Grid -->
        <div id="trRentBill" runat="server" visible="false" class="mt-4">
            <div class="section-header">Godown Rent Bill Detail</div>

            <div class="scroll-box mb-3" style="max-height: 1200px;">
                <asp:GridView ID="gvIStorageCharge" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-striped table-hover small" OnRowDataBound="gvIStorageCharge_RowDataBound">
                    <Columns>
                        <asp:BoundField HeaderText="Date" DataFormatString="{0:dd/MM/yyyy}" DataField="Deposit_Date" />
                        <asp:BoundField HeaderText="Opening Balance" DataField="Opening_Balance" />
                        <asp:BoundField HeaderText="Receive Bags" DataField="Receive_Bags" />
                        <asp:BoundField HeaderText="Issue Bags" DataField="Issue_Bags" />
                        <asp:BoundField HeaderText="Closing Bag Balance" DataField="Closing_Balance" />
                        <asp:BoundField HeaderText="Godown Id" DataField="Godown_Id" />
                        <asp:BoundField HeaderText="Opening Weight" DataField="Opening_Weight" />
                        <asp:BoundField HeaderText="Receive Weight" DataField="Receive_Weight" />
                        <asp:BoundField HeaderText="Issue Weight" DataField="Issue_Weight" />
                        <asp:BoundField HeaderText="Closing Weight Balance" DataField="Closing_Weight" />
                        <asp:BoundField HeaderText="Per Day Rate" DataField="Per_Day_Rate" />
                        <asp:TemplateField HeaderText="Total Charges">
                            <ItemTemplate>
                                <asp:Label ID="lblCharges" runat="server" Text='<%# Eval("Charges")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Total Charges" DataField="Charges" />
                    </Columns>
                </asp:GridView>
            </div>

            <div class="text-center">
                <asp:Button ID="btnGenBill" runat="server" Text="Generate Bill" CssClass="BTNBLUE me-2" Visible="false" OnClick="btnGenBill_Click" />
                <asp:Button ID="btncancel2" runat="server" Text="Cancel" CssClass="BTNBLUE" Visible="false" OnClick="btncancel2_Click1" />
            </div>

        </div>

        <asp:HiddenField ID="hdnAmt" runat="server" Value="0" />
    </div>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>
</asp:Content>

