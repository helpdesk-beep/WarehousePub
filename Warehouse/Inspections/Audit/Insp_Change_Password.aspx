<%@ Page Title="" Language="C#" MasterPageFile="~/Inspections/Masters/Account_Audit.master" AutoEventWireup="true" CodeFile="Insp_Change_Password.aspx.cs" Inherits="Inspections_Inspection_Officer_Insp_Change_Password" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <div runat="server" style="background-color: #FDFAF7; width: 100%;">
        <table  style="width: 50%;  text-align:center; margin:0 auto; background-color:honeydew; border: #008CBA; border-style: solid; border-width: 0px;  ">
           
            <tr style="align-content:center">
                <td style="width:500px;">
                    <asp:Label ID="lbloldpass" runat="server" Text="Old Password"></asp:Label>
                </td>
                <td style="width:500px;">
                    <asp:TextBox ID="txtOldPass" class="form-control" runat="server" TextMode="Password" CssClass="form-control" ></asp:TextBox>
                </td>

            </tr>
            <tr>
                <td style="width: 500px;">
                    <asp:Label ID="lblNewPass" runat="server" Text="New Password"></asp:Label>
                </td>
                <td style="width:500px;">
                    <asp:TextBox ID="txtNewPass" runat="server" TextMode="Password" CssClass="form-control" ></asp:TextBox>
                </td>

            </tr>
             <tr>
                <td style="width: 175px;">
                    <asp:Label ID="lblConPass" runat="server" Text="Conform Password"></asp:Label>
                </td>
                <td style="width:500px;">
                    <asp:TextBox ID="txtConPass" runat="server" TextMode="Password" CssClass="form-control" ></asp:TextBox>
                </td>
                 
            </tr>
        </table>
        </br>
        <div style="text-align:center">
        <asp:Button CssClass="btn btn-success"  ID="Button1" runat="server" OnClick="btnsubmit_Click" Text="SUBMIT"/>
        </div>

    </div>
</asp:Content>

