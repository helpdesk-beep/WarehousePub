<%@ Page Language="C#" MasterPageFile="~/MasterPage/StateMaster.master" AutoEventWireup="true" CodeFile="UpdateLicNoDate.aspx.cs" Inherits="StatePages_UpdateLicNoDate" Title="Lic Update" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" src="<%= ResolveUrl("~/NEW_CSS/js/jquery-1.12.4.js") %>"></script>
    <link href="../assets/New/css/select2.min.css" rel="stylesheet" />
    <script type="text/javascript" src="../assets/New/js/select2.min.js"></script>
    <script type="text/javascript">
        function initSelect2() {
            $("[id*=DropDownList1]").select2();
            $("[id*=ddlBranch]").select2();
        }
        $(document).ready(function () {
            initSelect2();
        });
    </script>
    <link href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../assets/New/css/bootstrap.min.css" rel="stylesheet" />

    <style>
        .main-container { padding: 20px; background-color: #f4f7f6; }
        .card-box { background: #fff; padding: 20px; border-radius: 10px; box-shadow: 0 2px 10px rgba(0,0,0,0.1); border-top: 4px solid #1e40af; }
        .header-title { font-size: 18px; font-weight: bold; color: #1e40af; margin-bottom: 20px; border-bottom: 1px solid #eee; padding-bottom: 10px; }
        
        .custom-grid { width: 100% !important; border-collapse: collapse; margin-top: 15px; }
        .custom-grid th { background-color: #f8fafc !important; color: #334155 !important; padding: 12px !important; border: 1px solid #e2e8f0 !important; text-align: center; font-size: 13px; }
        .custom-grid td { padding: 10px !important; border: 1px solid #e2e8f0 !important; color: #475569 !important; font-size: 13px; }
        
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
        .btn-edit-icon:hover { background-color: #00acc1; }

        .modal-window { background: white; width: 850px; border-radius: 8px; overflow: hidden; border: none !important; }
        .modal-header-blue { background: #1e40af; color: white; padding: 15px; font-weight: bold; }
        .modal-body-padding { padding: 25px; }

        #myspindiv { display: none; position: fixed; top: 0; left: 0; width: 100%; height: 100%; background: rgba(255,255,255,0.8); z-index: 10000; align-items: center; justify-content: center; }
    </style>

    <div class="main-container">
        <div class="card-box">
            <div class="header-title"><i class="fa fa-edit"></i> Update Godown License & Capacity (2026-27)</div>

            <div class="row mb-4">
                <div class="col-md-3">
                    <label class="small font-weight-bold">District</label>
                    <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" />
                </div>
                <div class="col-md-3">
                    <label class="small font-weight-bold">Branch</label>
                    <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" />
                </div>
                <div class="col-md-4">
                    <label class="small font-weight-bold">Quick Search</label>
                    <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Search..." onkeyup="filterGrid()" />
                </div>
                <div class="col-md-2">
                    <label>&nbsp;</label>
                    <asp:Button ID="Button2" runat="server" Text="Search" CssClass="btn btn-primary btn-block" OnClick="btnSearch_Click" OnClientClick="return validateAndShowLoader();" />
                </div>
            </div>

            <div class="table-responsive">
                <asp:GridView ID="Depositor_Gridview" runat="server" AutoGenerateColumns="False" CssClass="custom-grid" DataKeyNames="Godown_ID" GridLines="None">
                    <Columns>
                        <asp:TemplateField HeaderText="S.No.">
                            <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Godown ID">
                            <ItemTemplate><asp:Label ID="lblGodown_ID" runat="server" Text='<%# Eval("Godown_ID") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Godown Name">
                            <ItemTemplate><asp:Label ID="lblGodown_Name" runat="server" Text='<%# Eval("Godown_Name") %>' /></ItemTemplate>
                        </asp:TemplateField>
                         <%-- RESTORED HIRED TYPE COLUMN --%>
 <asp:TemplateField HeaderText="Hired Type">
     <ItemTemplate>
         <asp:Label ID="lblHired_Type" runat="server" Text='<%# Eval("Hired_Type") %>' />
     </ItemTemplate>
 </asp:TemplateField>

                        <asp:TemplateField HeaderText="Storage Type">
                            <ItemTemplate><asp:Label ID="lblStorage_Type" runat="server" Text='<%# Eval("Storage_Type") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Scientific Cap.">
                            <ItemTemplate><asp:Label ID="lblGodown_Scientific_Capacity" runat="server" Text='<%# Eval("Godown_Scientific_Capacity") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Max Cap.">
                            <ItemTemplate><asp:Label ID="lblGodown_Capacity" runat="server" Text='<%# Eval("Godown_Capacity") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="License No.">
                            <ItemTemplate><asp:Label ID="lblLicNum" runat="server" Text='<%# Eval("LicNum") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Expiry Date">
                            <ItemTemplate><asp:Label ID="lblLicDate" runat="server" Text='<%# Eval("LicDate") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        
                       
                        <asp:TemplateField HeaderText="Action">
                            <ItemStyle HorizontalAlign="Center" Width="60px" />
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkBtnEdit" runat="server" CssClass="btn-edit-icon" OnClick="Display">
                                    <i class="fa fa-pencil-square-o"></i>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField Visible="false">
                            <ItemTemplate><asp:Label ID="lblClosing_Balance" runat="server" Text='<%# Eval("Closing_Balance") %>' /></ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <div class="text-center p-4">No Record Found</div>
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>
        </div>
    </div>

    <asp:Panel ID="pnllogin" runat="server" CssClass="modal-window" style="display:none;">
        <div class="modal-header-blue">Update Godown Information</div>
        <div class="modal-body-padding" id="divNewInsp" runat="server">
            <div class="row mb-3">
                <div class="col-md-8">
                    <label class="small font-weight-bold">Godown Name</label>
                    <asp:TextBox ID="lblgodownname" runat="server" CssClass="form-control" />
                </div>
                <div class="col-md-4">
                    <label class="small font-weight-bold">Godown ID</label>
                    <asp:TextBox ID="txtGdwnID" runat="server" CssClass="form-control bg-light" ReadOnly="true" />
                </div>
            </div>
            <div class="row mb-3">
                <div class="col-md-4">
                    <label class="small font-weight-bold">Scientific Cap.</label>
                    <asp:TextBox ID="txtscieCPT" runat="server" CssClass="form-control" />
                </div>
                <div class="col-md-4">
                    <label class="small font-weight-bold">Maximum Cap.</label>
                    <asp:TextBox ID="txtMaxCpt" runat="server" CssClass="form-control" />
                </div>
                <div class="col-md-4">
                    <label class="small font-weight-bold">Closing Balance</label>
                    <asp:TextBox ID="txtclosing" runat="server" CssClass="form-control" />
                </div>
            </div>
            <div class="row mb-3">
                <div class="col-md-6">
                    <label class="small font-weight-bold">Hired Type</label>
                    <asp:DropDownList ID="ddllst_hired" runat="server" CssClass="form-control" />
                </div>
                <div class="col-md-6">
                    <label class="small font-weight-bold">Storage Type</label>
                    <asp:DropDownList ID="ddllst_storage" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Covered" Value="Covered" />
                        <asp:ListItem Text="Permanent(CAP)" Value="Permanent(CAP)" />
                        <asp:ListItem Text="Temporary(CAP)" Value="Temporary(CAP)" />
                        <asp:ListItem Text="Silo Bag" Value="SiloBag" />
                        <asp:ListItem Text="Steel Silo" Value="Steel Silo" />
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row mb-4">
                <div class="col-md-6">
                    <label class="small font-weight-bold">License No.</label>
                    <asp:TextBox ID="txtlicno" runat="server" CssClass="form-control" />
                </div>
                <div class="col-md-6">
                    <label class="small font-weight-bold">Expiry Date (DD/MM/YYYY)</label>
                    <asp:TextBox ID="txtlicdate" runat="server" CssClass="form-control" placeholder="DD/MM/YYYY" />
                </div>
            </div>
            <div class="text-right pt-3 border-top">
                <asp:Button ID="btnGenerateBill" runat="server" Text="Cancel" CssClass="btn btn-secondary mr-2" />
                <asp:Button ID="btnAddCompany" runat="server" Text="Save Changes" CssClass="btn btn-success" OnClick="btnAddCompany_Click" />
            </div>
        </div>
    </asp:Panel>

    <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="hdnTrigger" PopupControlID="pnllogin" BackgroundCssClass="modal-backdrop" CancelControlID="btnGenerateBill" />
    <asp:HiddenField ID="hdnTrigger" runat="server" />

    <div id="myspindiv">
        <div class="text-center">
            <i class="fa fa-refresh fa-spin fa-3x text-primary mb-2"></i>
            <div>Processing...</div>
        </div>
    </div>

    <script type="text/javascript">
        function filterGrid() {
            var input = document.getElementById('<%= txtSearch.ClientID %>').value.toLowerCase();
            var table = document.getElementById('<%= Depositor_Gridview.ClientID %>');
            var trs = table.getElementsByTagName("tr");
            for (var i = 1; i < trs.length; i++) {
                trs[i].style.display = trs[i].innerText.toLowerCase().indexOf(input) > -1 ? "" : "none";
            }
        }
        function validateAndShowLoader() {
            document.getElementById("myspindiv").style.display = "flex";
            return true;
        }
    </script>
</asp:Content>