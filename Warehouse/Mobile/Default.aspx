<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Mobile_Default" %>
<%@ Register TagPrefix="mobile" Namespace="System.Web.UI.MobileControls" Assembly="System.Web.Mobile" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<body>
     
    <mobile:Form id="Form1" runat="server">
        <mobile:DeviceSpecific><Choice>

            <HeaderTemplate>
                
                  <mobile:Image ID="Image1" ImageUrl="../images/MpwlcM.png" Runat="server"></mobile:Image>
       <mobile:Label ID="Label1" Runat="server" ForeColor="#cc0000" Font-Size="Normal">MPWLC</mobile:Label>
            </HeaderTemplate>
                               </Choice> </mobile:DeviceSpecific>
      
        
    </mobile:Form>
</body>
</html>
