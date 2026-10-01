<%@ Page Title="" Language="C#" MasterPageFile="~/Inspections/Masters/Inspection_Officer.master" AutoEventWireup="true" CodeFile="~/Inspections/Inspection_Officer/Request_For_Godown_Open_by_Inspection_Officer.aspx.cs" Inherits="Inspections_Inspection_Officer_Request_For_Godown_Open_by_Inspection_Officer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="<%= ResolveUrl("~/NEW_CSS/js/jquery-1.12.4.js") %>"></script>
    <link href="../assets/New/css/select2.min.css" rel="stylesheet" />
    <script type="text/javascript" src="../assets/New/js/select2.min.js"></script>
    <script type="text/javascript">
        function initSelect2() {
            $('#<%= ddlGodown.ClientID %>').select2({
                width: '100%'
            });
        }

        // Page load (first time)
        $(document).ready(function () {
            initSelect2();
        });

        // Postback ke baad bhi reinitialize hoga
        Sys.Application.add_load(function () {
            initSelect2();
        });
</script>
    <link href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../assets/New/css/bootstrap.min.css" rel="stylesheet" />

    <style>
        .main-container {
            padding: 20px;
            background-color: #f4f7f6;
        }

        .card-box {
            background: #fff;
            padding: 20px;
            border-radius: 10px;
            box-shadow: 0 2px 10px rgba(0,0,0,0.1);
            border-top: 4px solid #1e40af;
        }

        .header-title {
            font-size: 18px;
            font-weight: bold;
            color: #1e40af;
            margin-bottom: 20px;
            border-bottom: 1px solid #eee;
            padding-bottom: 10px;
        }

        .custom-grid {
            width: 100% !important;
            border-collapse: collapse;
            margin-top: 15px;
        }

            .custom-grid th {
                background-color: #f8fafc !important;
                color: #334155 !important;
                padding: 12px !important;
                border: 1px solid #e2e8f0 !important;
                text-align: center;
                font-size: 13px;
            }

            .custom-grid td {
                padding: 10px !important;
                border: 1px solid #e2e8f0 !important;
                color: #475569 !important;
                font-size: 13px;
            }

        .btn-edit-icon {
            background-color: #00cae3;
            color: white !important;
            padding: 6px 10px;
            border-radius: 4px;
            display: inline-block;
            text-decoration: none !important;
            font-size: 16px;
            border: none;
        }

            .btn-edit-icon:hover {
                background-color: #00acc1;
            }

        .modal-window {
            background: white;
            width: 850px;
            border-radius: 8px;
            overflow: hidden;
            border: none !important;
        }

        .modal-header-blue {
            background: #1e40af;
            color: white;
            padding: 15px;
            font-weight: bold;
        }

        .modal-body-padding {
            padding: 25px;
        }

        #myspindiv {
            display: none;
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background: rgba(255,255,255,0.8);
            z-index: 10000;
            align-items: center;
            justify-content: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-container">
        <div class="card-box">
            <div class="header-title"><i class="fa fa-edit"></i>Request For Godown Open</div>
            <div class="row mb-4">
                <div class="col-md-3">
                    <label class="small font-weight-bold">Godown Name</label>
                    <asp:DropDownList ID="ddlGodown" runat="server" AutoPostBack="True" CssClass="form-control" Font-Size="10pt">
                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-2">
                    <label class="small font-weight-bold">Financial Year</label>
                    <asp:DropDownList ID="ddlFinancialYear" runat="server"
                        CssClass="form-control">
                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                        
                        <asp:ListItem Value="2026-27">2026-27</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-2">
                    <label class="small font-weight-bold">Inspection Quarter</label>
                    <asp:DropDownList ID="ddlQuarter" runat="server"
                        CssClass="form-control">
                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                        <asp:ListItem Value="1">1st Quarter</asp:ListItem>
                        <asp:ListItem Value="2">2nd Quarter</asp:ListItem>
                        <asp:ListItem Value="3">3rd Quarter</asp:ListItem>
                        <asp:ListItem Value="4">4th Quarter</asp:ListItem>
                        <asp:ListItem Value="5">Half Yearly Inspection</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-2">
                    <label class="small font-weight-bold">Verification Type</label>
                    <asp:DropDownList ID="ddlVerificationType" runat="server"
                        CssClass="form-control">
                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                        <asp:ListItem Value="1">General Inspection</asp:ListItem>
                        <asp:ListItem Value="2">Physical Verification</asp:ListItem>
                        <asp:ListItem Value="3">Both</asp:ListItem>

                    </asp:DropDownList>
                </div> 
                <div class="col-md-2" style="margin-top: 28px;">
                    <asp:Button ID="btnSubmit" runat="server" Text="Open Godown For Edit Request To RM" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
                </div>
            </div>
        </div>
    </div>

</asp:Content>

