<%@ Page Title="" Language="C#" ViewStateEncryptionMode="Always" MasterPageFile="../MasterPages/adminMaster.master" AutoEventWireup="true" CodeFile="ViewTourProgram.aspx.cs" Inherits="Admin_ViewTourProgram" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PM" runat="Server">
    <link href="../NEW_CSS/Gallery/family.css" rel="stylesheet" />
    <link href="../NEW_CSS/Gallery/baguetteBox.min.css" rel="stylesheet" />
    <link href="../NEW_CSS/Gallery/thumbnail-gallery.css" rel="stylesheet" />


    <div class="container gallery-container">
        <div class="alert-warning img-thumbnail" style="margin-bottom: 10px!important;">
            <b style="font-size: large; font-family: 'Times New Roman', Times, serif">View Tour Program</b>
        </div>
        <div>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" PageSize="30"
                AllowPaging="true" CssClass="alert-heading" OnPageIndexChanging="GridView1_PageIndexChanging">
                <Columns>
                    <asp:TemplateField HeaderText="S.No.">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Tour Program in English" ControlStyle-Width="500px">
                        <ItemTemplate>
                            <asp:Label ID="lbl_Caption" runat="server" Text='<%#Eval("tour_prog_e") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Tour Program in Hindi" ControlStyle-Width="200px">
                        <ItemTemplate>
                            <asp:Label ID="lbltour" runat="server" Text='<%#Eval("tour_prog_h") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Tour Date">
                        <ItemTemplate>
                            <asp:Label ID="lbl_Date" runat="server" Text='<%#Eval("tour_date") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="alert-warning" />
                <AlternatingRowStyle CssClass=" alert-danger" />
            </asp:GridView>
        </div>
    </div>
</asp:Content>

