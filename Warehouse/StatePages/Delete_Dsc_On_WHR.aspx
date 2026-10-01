<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/StateMaster.master" AutoEventWireup="true" CodeFile="Delete_Dsc_On_WHR.aspx.cs" Inherits="StatePages_Delete_Dsc_On_WHR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Update Acceptance Note</title>
    <link href="../assets/css/style.css" rel="stylesheet" />
    <link href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../assets/New/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../CSS/style.css" rel="stylesheet" />
    <script type="text/javascript" src='<%= ResolveUrl("~/NEW_CSS/js/jquery-1.12.4.js") %>'></script>
    
    <style type="text/css">
        fieldset {
            border: 2px solid navy;
            padding: 15px;
            margin: 10px;
            border-radius: 5px;
        }
        legend {
            padding: 2px 8px;
            width: auto;
            border: 1px solid #2095A1;
            font-size: 17px;
            font-weight: bold;
            color: #030203;
        }
        .content-wrapper { padding: 1.75rem 1.25rem; }
        .table-bordered th { background: #647e68 !important; color: white !important; text-align: center; }
        /* Ensures the delete button container has spacing */
        .action-area { margin-top: 20px; margin-bottom: 20px; }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <fieldset>
            <legend>Delete WHR Dsc</legend>
            <div class="row">
                <div class="col-md-2">
                    <label>Session</label>
                    <asp:DropDownList ID="ddlcropyear" runat="server" CssClass="form-control">
                        <asp:ListItem Value="0">Select</asp:ListItem>
                        <asp:ListItem Value="2026-27">2026-27</asp:ListItem>
                        <asp:ListItem Value="2025-26">2025-26</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-2">
                    <label>WHR No.</label>
                    <asp:TextBox ID="txtacceptanceno" runat="server" AutoComplete="off" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-2" style="margin-top: 24px;">
                    <asp:Button runat="server" ID="btnsearch" CssClass="btn btn-info btn-block" Text="SEARCH" OnClick="btnsearch_Click" />
                </div>
            </div>
        </fieldset>
                <fieldset>
            <legend>Details</legend>
            <div class="table-responsive">
               <%-- <asp:GridView runat="server" ID="GridView1" CssClass="table table-bordered table-hover" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="District_Name" HeaderText="District Name" />
                        <asp:BoundField DataField="BranchName" HeaderText="Branch Name" />
                        <asp:BoundField DataField="Godown_Name" HeaderText="Godown" />
                        <asp:BoundField DataField="Whr_No" HeaderText="Whr No" />
                        <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity" />
                        <asp:BoundField DataField="B_Submit" HeaderText="Branch Submit" />
                        <asp:BoundField DataField="G_Submit" HeaderText="Godown Submit" />
                        <asp:BoundField DataField="Digitally_Signed" HeaderText="Digitally Signed" />
                    </Columns>
                    <EmptyDataTemplate>No Record Found</EmptyDataTemplate>
                </asp:GridView>--%>
                <asp:GridView runat="server" ID="GridView1" CssClass="table table-bordered table-hover" AutoGenerateColumns="False">
    <Columns>
        <asp:TemplateField HeaderText="S.No.">
            <ItemTemplate>
                <%# Container.DataItemIndex + 1 %>
            </ItemTemplate>
        </asp:TemplateField>
        
        <asp:BoundField DataField="District_Name" HeaderText="District Name" />
        <asp:BoundField DataField="BranchName" HeaderText="Branch Name" />
        <asp:BoundField DataField="Godown_Name" HeaderText="Godown" />
        <asp:BoundField DataField="Whr_No" HeaderText="WHR No" />
        <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity" />
        <asp:BoundField DataField="B_Submit" HeaderText="Branch Submit" />
        <asp:BoundField DataField="G_Submit" HeaderText="Godown Submit" />
        
        <%-- Matching the combined Date and User Type columns --%>
        <asp:BoundField DataField="Digitally_Signed" HeaderText="Digitally Signed" />
        <asp:BoundField DataField="D_Signature" HeaderText="D-Signature" />
        <asp:BoundField DataField="DX_Signature" HeaderText="DX-Signature" />
    </Columns>
    <EmptyDataTemplate>
        <div class="alert alert-warning">No Record Found for the provided WHR Number.</div>
    </EmptyDataTemplate>
</asp:GridView>
            </div>

            <div class="row action-area">
                <div class="col-md-12 text-center">
                    <asp:Button ID="btndlt" runat="server" 
                        Text="DELETE" 
                        Visible="false" 
                        CssClass="btn btn-danger btn-lg" 
                        Width="150px"
                        OnClick="btndlt_Click" 
                        OnClientClick="return confirm('Are you sure you want to delete this record?');" />
                </div>
            </div>
        </fieldset>
    </div>
</asp:Content>