<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Admin_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
    <h4>Home</h4><hr />

   <%-- <asp:Label ID="lblAuthCookie" runat="server"></asp:Label>
    <br />--%>
    <asp:Label ID="TextBox1" runat="server" EnableViewState="false" Visible="false"/>
    <asp:Label ID="TextBox2" runat="server" EnableViewState="false" Visible="false"/>
    <asp:Label ID="lblAuthCookie" runat="server" EnableViewState="false" Visible="false"/>
    <asp:Label ID="lblMessage" runat="server"></asp:Label>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="contbootm" Runat="Server">
</asp:Content>

