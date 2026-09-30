<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/adminMaster.master" AutoEventWireup="true" CodeFile="DeleteAchalSampatti.aspx.cs" Inherits="Admin_DeleteAchalSampatti" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PM" Runat="Server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
  <link href="../Admin_New/css/jquery-ui.css" rel="stylesheet" />
  <script src="../Admin_New/js/jquery-1.12.4.js"></script>
  <script src="../Admin_New/js/jquery-ui.js"></script>

     <div class="alert-warning img-thumbnail" style="margin-bottom:10px!important;">
        <b style="font-size:large; font-family:'Times New Roman', Times, serif">Delete Achal Sampatti</b> 
        </div> 
         <br /><div>
         <center>
          <asp:Label ID="lblErr" runat="server" CssClass="alert-danger" Font-Bold="true" Font-Size="Large"></asp:Label>  </center>
    </div>
    
            <table style="width:100%;">  
             <tr>
                <td class="style1" style="float:right;">  
                    Select Year:</td>  
                <td class="style2">  
                        <asp:DropDownList ID="ddlYear" runat="server" Width="180px"  AutoPostBack="true"
                          OnSelectedIndexChanged="ddlYear_SelectedIndexChanged"    >
        </asp:DropDownList>
                  <asp:RequiredFieldValidator ID="rvDocType" runat="server"
           ControlToValidate="ddlYear" ErrorMessage="Please Select Document Type" ForeColor="#CC3300" 
                              ValidationGroup="myValidator"></asp:RequiredFieldValidator>  
                </td>  
                <td>  
                     </td>  
            </tr></table>
           <br />
    <div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="6" AllowPaging="true" PageSize ="30" 
            OnPageIndexChanging="GridView1_PageIndexChanging" 
   CssClass ="alert-heading" >  
            <Columns>               
                <asp:TemplateField HeaderText="S.No.">
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                        <asp:HiddenField ID="hdnID" runat="server" Value='<%#Eval("id") %>'></asp:HiddenField>
                    </ItemTemplate>
                </asp:TemplateField> 
                <asp:TemplateField HeaderText="Name in Hindi" >  
                    <ItemTemplate>  
                        <asp:Label ID="lbl_NameH" runat="server" Text='<%#Eval("empNameH") %>'></asp:Label>  
                    </ItemTemplate>                     
                </asp:TemplateField>  
                 <asp:TemplateField HeaderText="Name in English" >  
                    <ItemTemplate>  
                        <asp:Label ID="lbl_NameE" runat="server" Text='<%#Eval("empNameE") %>'></asp:Label>  
                    </ItemTemplate>                     
                </asp:TemplateField>  
                 <asp:TemplateField HeaderText="Designation in Hindi" >  
                    <ItemTemplate>  
                        <asp:Label ID="lbl_DesigH" runat="server" Text='<%#Eval("designationH") %>'></asp:Label>  
                    </ItemTemplate>                     
                </asp:TemplateField>  
                 <asp:TemplateField HeaderText="Designation in English" >  
                    <ItemTemplate>  
                        <asp:Label ID="lbl_DesigE" runat="server" Text='<%#Eval("designationE") %>'></asp:Label>  
                    </ItemTemplate>                     
                </asp:TemplateField>  
                <asp:BoundField DataField="fileName" HeaderText="File Name"/>
        <asp:TemplateField ItemStyle-HorizontalAlign = "Center">
            <ItemTemplate>
                <asp:LinkButton ID="lnkDownload" runat="server" Text="Download" OnClick="DownloadFile"
                    CommandArgument='<%# Eval("Id") %>'></asp:LinkButton>
         <asp:LinkButton ID="lnkView" runat="server" Text="View" OnClick="View" CommandArgument='<%# Eval("Id") %>'></asp:LinkButton>
            
            </ItemTemplate>
        </asp:TemplateField>  
                <asp:TemplateField>  
                   <ItemTemplate>  
                        <asp:Button ID="btn_Delete" CssClass="btn-danger" OnClick="Delete" CommandArgument='<%# Eval("Id") %>' runat="server" Text="Delete" CommandName="Delete" />  
                    </ItemTemplate>                     
                </asp:TemplateField>  
            </Columns>  
            <HeaderStyle CssClass="alert-warning" />   
            <AlternatingRowStyle CssClass=" alert-danger" />        
        </asp:GridView>        
    </div>   
</asp:Content>

