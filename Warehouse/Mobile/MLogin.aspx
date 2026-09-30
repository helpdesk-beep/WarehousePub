<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MLogin.aspx.cs" Inherits="Mobile_MLogin" %>

<!DOCTYPE html>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>MPWLC Storage Login</title>
   <link rel="stylesheet" href="style.css" type="text/css" />
     <script type="text/javascript">
         var divMsg = $("<div>Please wait...page is loading...</div>");

         function pageLoad() {
             $("#divContainer").show();
             divMsg.remove();
         }
     </script> 
</head>
<body>
    <form id="login" runat="server">
        
<cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</cc1:ToolkitScriptManager>

        <div id="dvLoading">
   <div class="loginbox radius">
<h2 style="color:#FFF; text-align:center">MPWLC Storage Login</h2>
	<div class="loginboxinner radius">
    	<div class="loginheader">
    		<h1 class="title">Login</h1>
        	<div class="logo"><img src="../images/MpwlcM.PNG" /></div>
    	</div><!--loginheader-->
        
        <div class="loginform">
                	
        	
            	<p>
                	<label for="District" class="bebas">District</label>
                   
                 
                    <asp:DropDownList ID="DDL_Dist" runat="server" class="radius2" AutoPostBack="True" OnSelectedIndexChanged="DDL_Dist_SelectedIndexChanged" Width="95%"></asp:DropDownList>
                </p>
            <p>
                	<label for="Branch" class="bebas" id="lbl_Depot" runat="server" >Branch</label>
                   
                   
                <asp:DropDownList ID="DDL_Depot" runat="server" class="radius2" Width="95%"></asp:DropDownList>
                </p>
                <p>
                	<label for="password" class="bebas">Password</label>
                    
                    <asp:TextBox ID="txt_password" runat="server" TextMode="Password"></asp:TextBox>
                </p>

                <p>
                	
                    <asp:Button ID="btn_login" class="radius title" runat="server" Text="Login" BorderColor="#990000" OnClick="btnlogin_Click" OnClientClick="this.disabled = true; this.value='Please wait'" UseSubmitBehavior="false"  />
                </p>
            
        </div><!--loginform-->
    </div><!--loginboxinner-->
</div>
            </div>
    </form>
</body>
</html>
