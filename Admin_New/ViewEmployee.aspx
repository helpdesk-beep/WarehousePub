<%@ Page Title="" Debug="true" ViewStateEncryptionMode="Always" Language="C#" MasterPageFile="../MasterPages/adminMaster.master" AutoEventWireup="true" CodeFile="ViewEmployee.aspx.cs" Inherits="Admin_ViewEmployee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PM" runat="Server">
    <style type="text/css">
        .circle {
            width: 220px;
            height: 120px;
            background: LightPink;
            -moz-border-radius: 160px;
            -webkit-border-radius: 160px;
            border-radius: 160px;
            border: groove;
        }
    </style>
    <div class="container">
        <div class="alert-warning img-thumbnail" style="margin-bottom: 10px!important;">
            <b style="font-size: large; font-family: 'Times New Roman', Times, serif">
                <asp:Literal ID="Literal80" runat="server" Text="<%$Resources:language, EmpDetail%>" /></b>
        </div>
        <br />
        <div class="img-thumbnail" style="width: 100%">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="2" Width="100%"
                CssClass="alert-heading table">
                <Columns>
                    <asp:TemplateField HeaderText="स.क्र.">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="नाम">
                        <ItemTemplate>
                            <asp:Label ID="lbl_Name" runat="server" Text='<%#Eval("EmpName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="पदनाम">
                        <ItemTemplate>
                            <asp:Label ID="lbl_Designation" runat="server" Text='<%#Eval("Designation") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="जन्म दिनांक">
                        <ItemTemplate>
                            <asp:Label ID="lbl_DOB" runat="server" Text='<%#Eval("DOB") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ईमेल">
                        <ItemTemplate>
                            <asp:Label ID="lbl_Designation" runat="server" Text='<%#Eval("Email") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="मोबाइल न.">
                        <ItemTemplate>
                            <asp:Label ID="lbl_Mobile" runat="server" Text='<%#Eval("Mobile") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="फोटो">
                        <ItemTemplate>
                            <asp:Image runat="server" ImageUrl='<%#Eval("Image") %>' CssClass="circle" Width="120px" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="alert-warning" />
                <AlternatingRowStyle CssClass="alert-success" />
            </asp:GridView>
        </div>
    </div>
</asp:Content>

