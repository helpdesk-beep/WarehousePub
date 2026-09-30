<%@ Page Title="Godown Wise Insecticide Cap Cover Details" Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="~/BranchPages/Godown_Wise_Insecticide_Cap_Cover_Details.aspx.cs" Inherits="BranchPages_Godown_Wise_Insecticide_Cap_Cover_Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <link href="../assets/New/css/bootstrap.min.css" rel="stylesheet" />
    <%--<link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.1/css/all.min.css" rel="stylesheet" />--%>

    <style>
        body {
            background: #f4f6f9;
        }

        .page-container {
            margin-top: 25px;
            margin-bottom: 25px;
        }

        .main-card {
            border: none;
            border-radius: 15px;
            overflow: hidden;
            background: #fff;
            box-shadow: 0 5px 25px rgba(0,0,0,0.08);
        }

        .card-header-custom {
            background: linear-gradient(135deg,#0d6efd,#4e73df);
            color: #fff;
            padding: 18px;
        }

            .card-header-custom h4 {
                margin: 0;
                font-weight: 600;
            }

        .card-body-custom {
            padding: 30px;
        }

        .custom-fieldset {
            border: 2px solid #dbe3ff !important;
            border-radius: 12px;
            padding: 25px !important;
            background: #fafcff;
            margin-top: 20px;
            margin-bottom: 20px;
        }

        .custom-legend {
            background: linear-gradient(135deg,#0d6efd,#4e73df);
            color: #fff;
            padding: 8px 18px;
            border-radius: 25px;
            font-size: 14px !important;
            font-weight: 600;
            width: auto;
            border: none;
        }

        .col-form-label {
            font-weight: 600;
            color: #495057;
        }

        .required {
            color: red;
        }

        .form-control,
        .form-select {
            border-radius: 8px;
            min-height: 45px;
        }

            .form-control:focus,
            .form-select:focus {
                border-color: #0d6efd;
                box-shadow: 0 0 10px rgba(13,110,253,.15);
            }

        .input-group-text {
            background: #f8f9fa;
            color: #0d6efd;
            font-weight: bold;
        }

        .btn-save {
            background: linear-gradient(135deg,#0d6efd,#4e73df);
            color: #fff;
            border: none;
            border-radius: 30px;
            padding: 10px 35px;
            font-weight: 600;
            transition: all .3s;
        }

            .btn-save:hover {
                color: #fff;
                transform: translateY(-2px);
                box-shadow: 0 5px 15px rgba(13,110,253,.30);
            }

        @media(max-width:768px) {

            .col-form-label {
                text-align: left !important;
                margin-bottom: 5px;
            }

            .card-body-custom {
                padding: 20px;
            }
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container-fluid page-container">
        <div class="row justify-content-center">
            <div class="col-xl-8 col-lg-10 col-md-12">
                <div class="main-card">
                    <div class="card-header-custom text-center">
                        <h4><i class="fas fa-warehouse me-2"></i>Godown Wise Insecticide / Cap Cover Details</h4>
                    </div>
                    <div class="card-body-custom">
                        <div class="row mb-3 align-items-center">
                            <asp:Label ID="lblInsecticide" runat="server" AssociatedControlID="txtInsecticideName" CssClass="col-md-4 col-form-label text-md-end">Insecticide Name</asp:Label>
                            <div class="col-md-7">
                                <div class="input-group">
                                    <span class="input-group-text"><i class="fas fa-flask"></i></span>
                                    <asp:TextBox Style="margin-bottom: 10px;" ID="txtInsecticideName" runat="server" CssClass="form-control" placeholder="Enter Insecticide Name"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row mb-3 align-items-center">
                            <asp:Label ID="lblClosingBalance" runat="server" AssociatedControlID="txtClosingBalance" CssClass="col-md-4 col-form-label text-md-end">Closing Balance <span style="color:red;">(01/04/2026)</span></asp:Label>
                            <div class="col-md-7">
                                <div class="d-flex align-items-center">
                                    <div class="input-group">
                                        <span class="input-group-text"><i class="fas fa-scale-balanced"></i></span>
                                        <asp:TextBox ID="txtClosingBalance" runat="server" CssClass="form-control" placeholder="Enter Closing Balance"></asp:TextBox>
                                    </div>
                                </div>
                                <span style="color: red; font-weight: bold; margin-left: 10px; white-space: nowrap;">Quantity In KG</span>
                            </div>
                        </div>
                        <!-- Godown Details -->
                        <fieldset class="custom-fieldset">
                            <legend class="custom-legend"><i class="fas fa-warehouse me-1"></i>Godown Information</legend>
                            <div class="row mb-3 mt-3 align-items-center">
                                <asp:Label ID="lblGodownType" runat="server" AssociatedControlID="ddlGodownType" CssClass="col-md-4 col-form-label text-md-end">Godown Type</asp:Label>
                                <div class="col-md-7">
                                    <asp:DropDownList Style="margin-bottom: 10px;" ID="ddlGodownType" runat="server" CssClass="form-select" OnSelectedIndexChanged="ddlGodownType_SelectedIndexChanged"
                                        AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <asp:Panel ID="pnlGodown" runat="server">
                                <div class="row mb-3 align-items-center">
                                    <asp:Label ID="lblGodownName" runat="server" CssClass="col-md-4 col-form-label text-md-end">Godown Name</asp:Label>

                                    <div class="col-md-7">
                                        <asp:DropDownList ID="ddlGodown" runat="server" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlGodown_SelectedIndexChanged">
                                            <asp:ListItem>Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </asp:Panel>
                            <div class="row align-items-center">
                                <asp:Label ID="lblTotalCap" runat="server" AssociatedControlID="txtTotalCapCover" CssClass="col-md-4 col-form-label text-md-end"> Total Cap Cover</asp:Label>
                                <div class="col-md-7">
                                    <asp:TextBox ID="txtTotalCapCover" runat="server" CssClass="form-control" placeholder="Enter Total Cap Cover"></asp:TextBox>
                                </div>
                            </div>
                        </fieldset>
                        <div class="text-center mt-4">
                            <asp:Button ID="btnSubmit" runat="server" Text="Save Details" CssClass="btn btn-save" OnClick="btnSubmit_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row" style="margin-top: 20px">
            <div class="mt-4">

                <asp:GridView ID="gvCapCoverDetails"
                    runat="server"
                    CssClass="table table-bordered table-striped table-hover"
                    AutoGenerateColumns="False"
                    EmptyDataText="No Record Found">

                    <Columns>
                        <asp:TemplateField HeaderText="S.No">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="Insecticide_Name"
                            HeaderText="Insecticide Name" />

                    
                        <asp:BoundField DataField="Godown_Type"
                            HeaderText="Godown Type" />

                        <asp:BoundField DataField="Godown_Name"
                            HeaderText="Godown Name" />

                        <asp:BoundField DataField="Total_Cap_Cover"
                            HeaderText="Total Cap Cover" />


                    </Columns>

                </asp:GridView>

            </div>
        </div>
    </div>
</asp:Content>
