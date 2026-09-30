<%@ Page Title="" Language="C#" MasterPageFile="~/Inspections/Masters/Inspection_RO.master" AutoEventWireup="true" CodeFile="~/Inspections/RO/Godown_Open_Request_From_Inspection_Officer.aspx.cs" Inherits="Inspections_RO_Godown_Open_Request_From_Inspection_Officer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:GridView ID="gvRequests" runat="server" AutoGenerateColumns="False"
        CssClass="table table-bordered custom-grid" OnRowCommand="gvRequests_RowCommand"
        EmptyDataText="No Record Found">
        <Columns>
            <asp:BoundField DataField="OfficerName" HeaderText="Officer Name" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="Mobile_No" HeaderText="Mobile No" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="Branch_Name" HeaderText="Inspected Branch" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="Godown_Name" HeaderText="Godown Name" ItemStyle-HorizontalAlign="Center" />
            <asp:TemplateField HeaderText="Inspection Quarter" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate><%# Eval("Inspection_Quarter") %><asp:HiddenField ID="hfQuarter" runat="server" Value='<%# Eval("Quarter") %>' /></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Verification Type" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate><%# Eval("VerificationType") %><asp:HiddenField ID="hfVerificationType" runat="server" Value='<%# Eval("PV_Type") %>' /></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Mobile Number" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate><%# Eval("Mobile_No") %><asp:HiddenField ID="hfMobile_No" runat="server" Value='<%# Eval("Mobile_No") %>' /></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Branch Id" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate><%# Eval("Branch_ID") %><asp:HiddenField ID="hfBranch_ID" runat="server" Value='<%# Eval("Branch_ID") %>' /></ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Financial_Year" HeaderText="Financial Year" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="Total_PV_Bags" HeaderText="Total Bags" ItemStyle-HorizontalAlign="Center" />
            <asp:TemplateField HeaderText="Approve" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate><asp:Button ID="btnApprove" runat="server" Text="Approve" CommandName="Approve" CommandArgument='<%# Eval("Godown_Id") %>' CssClass="btn btn-success btn-sm" /></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Reject" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Button ID="btnReject" runat="server" Text="Reject" CommandName="Reject" CommandArgument='<%# Eval("Godown_Id") %>' CssClass="btn btn-danger btn-sm" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EmptyDataRowStyle HorizontalAlign="Center" ForeColor="Red" Font-Bold="true" />
    </asp:GridView>
</asp:Content>

