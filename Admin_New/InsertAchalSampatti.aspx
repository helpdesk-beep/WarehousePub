<%@ Page Title="" Language="C#" MasterPageFile="../MasterPages/adminMaster.master" AutoEventWireup="true" CodeFile="InsertAchalSampatti.aspx.cs" Inherits="Admin_InsertAchalSampatti" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PM" Runat="Server">
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
    

     <div class="alert-warning img-thumbnail" style="margin-bottom:10px!important;">
        <b style="font-size:large; font-family:'Times New Roman', Times, serif">Insert Achal Sampatti</b>  
    </div>
    <div>  
      
        <table style="width:100%;">  
             <tr> <td colspan="2">  
                    <asp:Label ID="lblErr" runat="server" Font-Bold="true"></asp:Label>  
                </td>  </tr> 
            <tr>  
                <td class="style1">  
                    Employee Name in Hindi:</td>  
                <td class="style2">  
                    
                    <input type="text" runat="server" id="empNameHindi"  style="width:300px;" />
                  <asp:RequiredFieldValidator ID="rvTitle" runat="server"
           ControlToValidate="empNameHindi" ErrorMessage="Please Insert Title" ForeColor="#CC3300" 
                              ValidationGroup="myValidator"></asp:RequiredFieldValidator>  
                </td>  
                <td>  
                     </td>  
            </tr>  
              <tr>  
                <td class="style1">  
                    Employee Name in English:</td>  
                <td class="style2">  
                    
                    <input type="text" runat="server" id="empNameEng" style="width:300px;" />
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
           ControlToValidate="empNameEng" ErrorMessage="Please Insert Title" ForeColor="#CC3300" 
                              ValidationGroup="myValidator"></asp:RequiredFieldValidator>  
                </td>  
                <td>  
                     </td>  
            </tr>  
             <tr>  
                <td class="style1">  
                    Designation in Hindi:</td>  
                <td class="style2">  
                    
                    <input type="text" runat="server" id="designationHindi" style="width:300px;" />
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
           ControlToValidate="designationHindi" ErrorMessage="Please Insert Title" ForeColor="#CC3300" 
                              ValidationGroup="myValidator"></asp:RequiredFieldValidator>  
                </td>  
                <td>  
                     </td>  
            </tr>  
             <tr>  
                <td class="style1">  
                    Designation in English:</td>  
                <td class="style2">  
                    
                    <input type="text" runat="server" id="designationEnglish" style="width:300px;" />
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
           ControlToValidate="designationEnglish" ErrorMessage="Please Insert Title" ForeColor="#CC3300" 
                              ValidationGroup="myValidator"></asp:RequiredFieldValidator>  
                </td>  
                <td>  
                     </td>  
            </tr>  
             <tr><td>Select Year:</td>

                <td> <asp:DropDownList ID="ddlYear" runat="server" Width="180px"  AutoPostBack="true">
        </asp:DropDownList>
                </td>
            </tr>   
            <tr>  
                <td>  
                    News Attachment:</td>  
                <td class="style2">  
                    <asp:FileUpload ID="FileUpload1" runat="server" />  
                     <asp:RequiredFieldValidator ID="rvFileUpload1" runat="server"
           ControlToValidate="FileUpload1" ErrorMessage="Please Insert File" ForeColor="#CC3300"
           ValidationGroup="myValidator"></asp:RequiredFieldValidator>
                </td>  
                <td>  
                    <asp:Label ID="Label1" runat="server"></asp:Label>  
                </td>  
            </tr>  
           
            <tr>  
                <td class="style1">  
                     </td>  
                <td class="style2">  
                    <asp:Button ID="btnSave" runat="server" onclick="btnSave_Click" Text="Upload" 
                        ValidationGroup="myValidator"/>  
                </td>  
                <td>  
                     </td>  
            </tr>  
        </table>  
      
    </div>

</asp:Content>

