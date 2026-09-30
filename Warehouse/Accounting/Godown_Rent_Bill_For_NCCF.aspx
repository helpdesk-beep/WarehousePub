<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="Godown_Rent_Bill_For_NCCF.aspx.cs" Inherits="Accounting_Godown_Rent_Bill_For_NCCF" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        /* MPWLC Theme Colors */
        :root {
            --primary-color: #003366;    /* Dark Blue - MPWLC Primary */
            --secondary-color: #0bb6e6;  /* Light Blue - Accent */
            --success-color: #28a745;    /* Green - Success */
            --danger-color: #dc3545;     /* Red - Error/Danger */
            --warning-color: #ffc107;    /* Yellow - Warning */
            --info-color: #17a2b8;        /* Teal - Info */
            --light-bg: #f8f9fa;          /* Light Gray Background */
            --border-color: #dee2e6;      /* Border Color */
            --text-dark: #333333;          /* Dark Text */
            --text-light: #ffffff;         /* White Text */
        }

        /* Main Container Styling */
        .mpwlc-container {
            width: 97%;
            /*max-width: 1200px;*/
            margin: 0 auto;
            padding: 20px;
            background-color: var(--light-bg);
            border-radius: 8px;
            box-shadow: 0 2px 10px rgba(0,0,0,0.1);
        }

        /* Fieldset Styling */
        .mpwlc-fieldset {
            border: 2px solid var(--primary-color);
            border-radius: 8px;
            background-color: white;
            margin-bottom: 20px;
            box-shadow: 0 2px 5px rgba(0,0,0,0.05);
        }

        /* Section Header Styling */
        .section-header {
            background: linear-gradient(135deg, var(--primary-color), var(--secondary-color));
            padding: 12px 20px;
            border-radius: 6px 6px 0 0;
            color: var(--text-light);
            font-size: 16px;
            font-weight: bold;
            text-transform: uppercase;
            letter-spacing: 0.5px;
            margin-bottom: 15px;
        }

        .section-header h3 {
            margin: 0;
            font-size: 18px;
            font-weight: 600;
        }

        /* Form Group Styling */
        .form-grid {
            display: grid;
            grid-template-columns: repeat(auto-fit, minmax(250px, 1fr));
            gap: 20px;
            padding: 20px;
        }

        .form-group {
            display: flex;
            flex-direction: column;
            gap: 5px;
        }

        .form-label {
            font-size: 12px;
            font-weight: bold;
            color: var(--primary-color);
            text-transform: uppercase;
            letter-spacing: 0.3px;
        }

        /* Control Styling */
        .mpwlc-dropdown {
            height: 35px;
            width: 100%;
            padding: 5px 10px;
            border: 2px solid var(--border-color);
            border-radius: 4px;
            font-size: 13px;
            color: var(--text-dark);
            background-color: white;
            transition: all 0.3s ease;
        }

        .mpwlc-dropdown:focus {
            border-color: var(--secondary-color);
            outline: none;
            box-shadow: 0 0 0 3px rgba(11, 182, 230, 0.1);
        }

        .mpwlc-textbox {
            height: 35px;
            width: 100%;
            padding: 5px 10px;
            border: 2px solid var(--border-color);
            border-radius: 4px;
            font-size: 13px;
            transition: all 0.3s ease;
        }

        .mpwlc-textbox:focus {
            border-color: var(--secondary-color);
            outline: none;
            box-shadow: 0 0 0 3px rgba(11, 182, 230, 0.1);
        }

        .mpwlc-textbox[readonly] {
            background-color: #e9ecef;
            cursor: not-allowed;
        }

        /* Button Styling - MPWLC Style */
        .mpwlc-btn {
            padding: 10px 20px;
            border: none;
            border-radius: 4px;
            font-size: 13px;
            font-weight: bold;
            text-transform: uppercase;
            letter-spacing: 0.5px;
            cursor: pointer;
            transition: all 0.3s ease;
            margin: 0 5px;
            min-width: 150px;
        }

        .mpwlc-btn-primary {
            background: linear-gradient(135deg, var(--primary-color), #004080);
            color: var(--text-light);
            box-shadow: 0 2px 5px rgba(0,51,102,0.3);
        }

        .mpwlc-btn-primary:hover {
            background: linear-gradient(135deg, #004080, var(--primary-color));
            transform: translateY(-2px);
            box-shadow: 0 4px 10px rgba(0,51,102,0.4);
        }

        .mpwlc-btn-secondary {
            background: linear-gradient(135deg, #6c757d, #5a6268);
            color: var(--text-light);
            box-shadow: 0 2px 5px rgba(108,117,125,0.3);
        }

        .mpwlc-btn-secondary:hover {
            background: linear-gradient(135deg, #5a6268, #6c757d);
            transform: translateY(-2px);
        }

        .mpwlc-btn-success {
            background: linear-gradient(135deg, var(--success-color), #218838);
            color: var(--text-light);
        }

        .mpwlc-btn-success:hover {
            background: linear-gradient(135deg, #218838, var(--success-color));
            transform: translateY(-2px);
        }

        /* Scrollable Container Styling */
        .mpwlc-scroll-container {
            max-height: 300px;
            overflow-y: auto;
            overflow-x: hidden;
            border: 1px solid var(--border-color);
            border-radius: 4px;
            padding: 15px;
            margin: 10px 0;
            background-color: white;
        }

        .mpwlc-scroll-container::-webkit-scrollbar {
            width: 8px;
        }

        .mpwlc-scroll-container::-webkit-scrollbar-track {
            background: #f1f1f1;
            border-radius: 4px;
        }

        .mpwlc-scroll-container::-webkit-scrollbar-thumb {
            background: var(--primary-color);
            border-radius: 4px;
        }

        .mpwlc-scroll-container::-webkit-scrollbar-thumb:hover {
            background: var(--secondary-color);
        }

        /* GridView Styling */
        .mpwlc-gridview {
            width: 100%;
            border-collapse: collapse;
            font-size: 12px;
        }

        .mpwlc-gridview th {
            background: linear-gradient(135deg, var(--primary-color), var(--secondary-color));
            color: var(--text-light);
            padding: 12px;
            font-weight: bold;
            text-align: center;
            border: 1px solid var(--border-color);
            white-space: nowrap;
        }

        .mpwlc-gridview td {
            padding: 8px 12px;
            border: 1px solid var(--border-color);
            text-align: left;
        }

        .mpwlc-gridview tr:nth-child(even) {
            background-color: var(--light-bg);
        }

        .mpwlc-gridview tr:hover {
            background-color: rgba(11, 182, 230, 0.1);
        }

        /* Message Label Styling */
        .mpwlc-message {
            padding: 12px;
            border-radius: 4px;
            margin: 10px 0;
            font-weight: bold;
            text-align: center;
        }

        .mpwlc-message-success {
            background-color: #d4edda;
            border: 1px solid #c3e6cb;
            color: #155724;
        }

        .mpwlc-message-error {
            background-color: #f8d7da;
            border: 1px solid #f5c6cb;
            color: #721c24;
        }

        /* Button Container */
        .button-container {
            display: flex;
            justify-content: center;
            gap: 15px;
            margin: 20px 0;
            flex-wrap: wrap;
        }

        /* Responsive Design */
        @media (max-width: 768px) {
            .form-grid {
                grid-template-columns: 1fr;
                gap: 15px;
            }
            
            .mpwlc-btn {
                width: 100%;
                margin: 5px 0;
            }
            
            .button-container {
                flex-direction: column;
            }
            
            .mpwlc-gridview {
                font-size: 11px;
            }
            
            .mpwlc-gridview th,
            .mpwlc-gridview td {
                padding: 6px;
            }
        }

        /* Center Alignment */
        .mpwlc-center {
            text-align: center;
        }

        /* Table cell spacing */
        .mpwlc-cell-spacing {
            padding: 8px;
        }

        /* Total Charges Alignment */
        .total-charges {
            text-align: right;
            font-weight: bold;
            color: var(--primary-color);
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="mpwlc-container">
        <fieldset class="mpwlc-fieldset">
            <div class="section-header">
                <h3>Godown/Silo Rent Bill For NCCF</h3>
            </div>
            
            <!-- Message Row -->
            <div id="msg" runat="server" class="mpwlc-center" style="padding: 10px;">
                <asp:Label ID="lblmsg" Text="" CssClass="mpwlc-message-error" Font-Bold="true" runat="server"></asp:Label>
            </div>

            <!-- Godown Type Selection -->
            <div class="form-group" style="padding: 20px; align-items: center;">
                <asp:Label ID="Label2" runat="server" Text="Godown Type" CssClass="form-label"></asp:Label>
                <asp:DropDownList ID="ddlGodownType" runat="server" Width="300px" CssClass="mpwlc-dropdown"
                    AutoPostBack="True" OnSelectedIndexChanged="ddlGodownType_SelectedIndexChanged">
                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                    <asp:ListItem Value="1">Joint Venture Scheme (JVS)</asp:ListItem>
                    <asp:ListItem Value="2">Hired</asp:ListItem>
                    <asp:ListItem Value="3">Tribal Scheme</asp:ListItem>
                    <asp:ListItem Value="4">Silo Bags</asp:ListItem>
                    <asp:ListItem Value="5">CAP-PMS</asp:ListItem>
                    <asp:ListItem Value="15">BOT</asp:ListItem>
                </asp:DropDownList>
            </div>

            <!-- JVS Godown Rent Section -->
            <div id="trJVSGodownRent" visible="false" runat="server">
                <fieldset class="mpwlc-fieldset" style="margin: 20px;">
                    <div class="section-header">
                        <h3>JVS Godown Rent</h3>
                    </div>
                    
                    <div class="mpwlc-scroll-container">
                        <div class="form-grid">
                            <div class="form-group">
                                <asp:Label ID="lblgodown" runat="server" Text="Godown" CssClass="form-label"></asp:Label>
                                <asp:DropDownList ID="ddlgodown" runat="server" AutoPostBack="True" 
                                    CssClass="mpwlc-dropdown" OnSelectedIndexChanged="ddlgodown_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            
                            <div class="form-group">
                                <asp:Label ID="lblcommodity" runat="server" Text="Commodity Name" CssClass="form-label"></asp:Label>
                                <asp:DropDownList ID="ddlcomodity" runat="server" AutoPostBack="True" 
                                    CssClass="mpwlc-dropdown" OnSelectedIndexChanged="ddlcomodity_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            
                            <div class="form-group">
                                <asp:Label ID="lblCropYear" runat="server" Text="Crop Year" CssClass="form-label"></asp:Label>
                                <asp:DropDownList ID="ddlCropYear" runat="server" AutoPostBack="True" 
                                    CssClass="mpwlc-dropdown" OnSelectedIndexChanged="ddlCropYear_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            
                            <div class="form-group">
                                <asp:Label ID="lblmonth" runat="server" Text="Financial Year/Month" CssClass="form-label"></asp:Label>
                                <div style="display: flex; gap: 10px;">
                                    <asp:DropDownList ID="ddlFyear" runat="server" CssClass="mpwlc-dropdown" Width="100px">
                                        <asp:ListItem Value="0" Text="Financial Year"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="ddlmonth" runat="server" AutoPostBack="True" 
                                        CssClass="mpwlc-dropdown" Width="120px" OnSelectedIndexChanged="ddlmonth_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            
                            <div class="form-group">
                                <asp:Label ID="lblcrate" runat="server" Text="Rate (Month/Day)" CssClass="form-label"></asp:Label>
                                <div style="display: flex; gap: 10px;">
                                    <asp:TextBox runat="server" ID="txtcomrate" CssClass="mpwlc-textbox" Width="100px"
                                        AutoPostBack="true" OnTextChanged="txtcomrate_TextChanged"></asp:TextBox>
                                    <asp:TextBox runat="server" ID="txtCPRate" CssClass="mpwlc-textbox" Width="100px"
                                        ReadOnly="true" Enabled="false"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" 
                                        TargetControlID="txtcomrate" ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" 
                                        TargetControlID="txtCPRate" ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="button-container">
                        <asp:Button ID="btnSumbmitRent" runat="server" Text="Check Stock Balance" 
                            CssClass="mpwlc-btn mpwlc-btn-primary" OnClick="btnSumbmitRent_Click" />
                        <asp:Button ID="brnCancel" runat="server" Text="Close" 
                            CssClass="mpwlc-btn mpwlc-btn-secondary" OnClick="brnCancel_Click1" />
                    </div>
                </fieldset>
            </div>

            <!-- Hired Godown Rent Section -->
            <div id="trHiredGodownRent" visible="false" runat="server">
                <fieldset class="mpwlc-fieldset" style="margin: 20px;">
                    <div class="section-header">
                        <h3>Hired Godown Rent</h3>
                    </div>
                    
                    <div class="mpwlc-scroll-container">
                        <div class="form-grid">
                            <div class="form-group">
                                <asp:Label ID="lblFromDate" runat="server" Text="From Date" CssClass="form-label"></asp:Label>
                                <asp:TextBox ID="txtfdate" runat="server" CssClass="mpwlc-textbox" 
                                    AutoPostBack="true" OnTextChanged="txtfdate_TextChanged"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                    TargetControlID="txtfdate">
                                </cc1:CalendarExtender>
                            </div>
                            
                            <div class="form-group">
                                <asp:Label ID="lblTodate" runat="server" Text="To Date" CssClass="form-label"></asp:Label>
                                <asp:TextBox ID="txttodate" runat="server" CssClass="mpwlc-textbox" 
                                    AutoPostBack="true" OnTextChanged="txttodate_TextChanged1"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                    TargetControlID="txttodate">
                                </cc1:CalendarExtender>
                            </div>
                            
                            <div class="form-group">
                                <asp:Label ID="Label5" runat="server" Text="Godown" CssClass="form-label"></asp:Label>
                                <asp:DropDownList ID="ddlGodown2" runat="server" AutoPostBack="True" 
                                    CssClass="mpwlc-dropdown" OnSelectedIndexChanged="ddlGodown2_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            
                            <div class="form-group">
                                <asp:Label ID="Label10" runat="server" Text="Financial Year" CssClass="form-label"></asp:Label>
                                <asp:DropDownList ID="ddlFyear2" runat="server" AutoPostBack="True" 
                                    CssClass="mpwlc-dropdown" OnSelectedIndexChanged="ddlFyear2_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            
                            <div class="form-group">
                                <asp:Label ID="Label9" runat="server" Text="Rate (Month/Day) MT" CssClass="form-label"></asp:Label>
                                <div style="display: flex; gap: 10px;">
                                    <asp:TextBox runat="server" ID="txtgratePM" CssClass="mpwlc-textbox" Width="100px"
                                        AutoPostBack="true" OnTextChanged="txtgratePM_TextChanged"></asp:TextBox>
                                    <asp:TextBox runat="server" ID="txtgratePD" CssClass="mpwlc-textbox" Width="100px"
                                        AutoPostBack="true" OnTextChanged="txtgratePD_TextChanged"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" 
                                        TargetControlID="txtgratePM" ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" 
                                        TargetControlID="txtgratePD" ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender>
                                </div>
                            </div>
                            
                            <div class="form-group">
                                <asp:Label ID="Label6" runat="server" Text="Storage Capacity (In MT)" CssClass="form-label"></asp:Label>
                                <asp:TextBox ID="txtSCapacity" runat="server" CssClass="mpwlc-textbox" 
                                    ReadOnly="true"></asp:TextBox>
                            </div>
                            
                            <div class="form-group">
                                <asp:Label ID="lblmob" runat="server" Text="Rent" CssClass="form-label"></asp:Label>
                                <asp:TextBox runat="server" ID="txtRent" CssClass="mpwlc-textbox" 
                                    ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="button-container">
                        <asp:Button ID="btnHSubmit" runat="server" Text="Check Stock Balance" 
                            CssClass="mpwlc-btn mpwlc-btn-primary" OnClick="btnHSubmit_Click" />
                        <asp:Button ID="btnHCancel" runat="server" Text="Close" 
                            CssClass="mpwlc-btn mpwlc-btn-secondary" OnClick="btnHCancel_Click" />
                    </div>
                </fieldset>
            </div>

            <!-- Reports View Section (Commented) -->
            <div id="trReportsView" visible="false" runat="server">
                <!-- Report Viewer content -->
            </div>

            <!-- Rent Bill Detail Section -->
            <div id="trRentBill" visible="false" runat="server">
                <fieldset class="mpwlc-fieldset" style="margin: 20px;">
                    <div class="section-header">
                        <h3>Godown Rent Bill Detail</h3>
                    </div>
                    
                    <div class="mpwlc-scroll-container" style="max-height: 400px;">
                        <asp:GridView ID="gvIStorageCharge" runat="server" AutoGenerateColumns="false" 
                            CssClass="mpwlc-gridview" OnRowDataBound="gvIStorageCharge_RowDataBound">
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
                                <asp:BoundField HeaderText="Closing Weight" DataField="Closing_Weight" />
                                <asp:BoundField HeaderText="Per Day Rate" DataField="Per_Day_Rate" />
                                <asp:TemplateField HeaderText="Total Charges">
                                    <HeaderTemplate>
                                        <div style="text-align: center;">Total Charges</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="total-charges">
                                            <asp:Label ID="lblCharges" runat="server" Text='<%# Eval("Charges")%>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Total Charges" DataField="Charges" />
                            </Columns>
                        </asp:GridView>
                    </div>

                    <div class="button-container">
                        <asp:Button ID="btnGenBill" runat="server" Text="Generate Bill" Visible="false" 
                            CssClass="mpwlc-btn mpwlc-btn-success" OnClick="btnGenBill_Click" />
                        <asp:Button ID="btncancel2" runat="server" Text="Cancel" Visible="false" 
                            CssClass="mpwlc-btn mpwlc-btn-secondary" OnClick="btncancel2_Click1" />
                    </div>

                    <div class="mpwlc-center" style="padding: 10px;">
                        <asp:Label ID="Lblmsg2" runat="server" Font-Bold="true" ForeColor="#dc3545"></asp:Label>
                    </div>
                </fieldset>
            </div>

            <asp:HiddenField ID="hdnAmt" runat="server" Value="0" />
        </fieldset>
    </div>
</asp:Content>