<%@ Page Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="Upload_WHR.aspx.cs" Inherits="Procurement_Upload_WHR" Title="E-WHR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div>
<table  cellpadding="0" cellspacing="0" style="width: 100%">
<tr style="background-color: #0bb6e6; height: 25px">
                                                        <td colspan="4" align="center">
                                                            <asp:Label ID="lblDepositDetail" runat="server" Text="Upload WHR File" Font-Size="15px"
                                                                Font-Bold="true" ForeColor="whitesmoke"></asp:Label>
                                                        </td>
                                                        </tr>
                                                         <tr>
                                                        <td colspan="4" style="height: 5px">
                                                        </td>
                                                    </tr>
<tr align="center">
<td>
<asp:Label ID="lblWHR" runat="server" Text="Enter WHR No"></asp:Label>
</td>
<td>
<asp:TextBox ID="txtUploadWHRId" runat="server"></asp:TextBox>
</td>
                                        <td>
                                        <asp:FileUpload ID="fileuploadimage"
                                                runat="server"
                                                ></asp:FileUpload>
                                        
                                 </td>
                                 <td>
                                 <asp:Button ID="btnSubmit" runat="server" Text="Upload WHR" onclick="btnSubmit_Click" />
                                 </td>
</tr>
 <tr>
                                                        <td colspan="4" style="height: 5px">
                                                        </td>
                                                    </tr>
                                                    <tr>
  
                                        <td align="center" colspan="2">
                                        
                                        
                                       
                                        
                                 </td>
</tr>
                                                    <tr>
                                                        <td colspan="4" style="height: 5px">
                                                        </td>
                                                    </tr>
                                                    <tr style="background-color: #0bb6e6; height: 25px">
                                                        <td colspan="4" align="center">
                                                            <asp:Label ID="Label1" runat="server" Text="Download WHR File" Font-Size="15px"
                                                                Font-Bold="true" ForeColor="whitesmoke"></asp:Label>
                                                        </td>
                                                        </tr>
                                                        <tr>
                                                        <td colspan="4" style="height: 5px">
                                                        </td>
                                                    </tr>
                                                   
                                                    <tr>
                                                    <td>
<asp:Label ID="Label2" runat="server" Text="Enter WHR No"></asp:Label>
</td>
<td>
<asp:TextBox ID="txtDownloadWHRId" runat="server"></asp:TextBox>
</td>
                                                    <td align="center">
                                                     <asp:Button ID="BtnPdf" runat="server" Text="Download Request WHR" onclick="BtnPdf_Click"/>
                                        <asp:Button ID="btnPdfSigned" runat="server" Text="Download E-Signed WHR" 
                                                onclick="btnPdfSigned_Click"/>
                                                    </td>
                                                    
                                                    </tr>
                                                        <tr>
                                                        <td colspan="4" style="height: 5px">
                                                        </td>
                                                    </tr>

</table>
</div>
</asp:Content>

