<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AuditChangePassword.aspx.cs" Inherits="Audit_AuditChangePassword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Change Password</title>
    <script type="text/javascript">
function checksqlkey_psw(e,tx)
    {     
        var AsciiCode = e.keyCode ? e.keyCode : e.which ? e.which : e.charCode; 
        var num=tx.value;
        
        //alert(AsciiCode);              
       if (AsciiCode == 59 || AsciiCode == 32)
        {            
            alert('Semi Colon (;) & Blank Space Not Allowed ...');
            return false;
        } 
       else if (AsciiCode=="36" || AsciiCode=="37" || AsciiCode=="38" || AsciiCode=="40" || AsciiCode=="41" || AsciiCode=="43" || AsciiCode=="92" || AsciiCode=="124" || AsciiCode=="34" || AsciiCode=="39" || AsciiCode=="60" || AsciiCode=="62" || AsciiCode=="44" || AsciiCode=="64" || AsciiCode=="61")
        {
            alert('Do not use SQL Key-Words, Semi Colon(;) and Special Characters(&,%,$)..etc');
	        return false;
        }
       else if(num.length>9)
        {
            alert('Password Length Maximum 10 Characters ...');
            return false;
        }                          
    }
    
     // For Copy/Paste Checking
    
    function checksqlkey_special(e,tx)
    {
        var AsciiCode = e.keyCode ? e.keyCode : e.which ? e.which : e.charCode; 
        var num=tx.value;     
        //alert(AsciiCode);         
       if (AsciiCode == 17)
        {            
            alert('Copy/Paste Not Allowed ...');
            return false;
        }                  
    }
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table style="width: 500px; margin-left: 15px; background-image: url(../images/images[26].jpg);">
            <tr style="border-collapse: collapse; border: solid 1px white" class="HeadingBlue">
                <td colspan="4" style="background-color: dimgray; height: 15px;">
                </td>
            </tr>
            <tr>
                <td align="center" style="height: 18px" colspan="4">
                    <asp:Label ID="lblChangePassword" runat="server" 
                        Text="Change Password(Branch Login)" Font-Bold="True"
                        Style="position: static" Width="152px"></asp:Label></td>
            </tr>
            <tr>
                <td align="center" style="height: 18px" colspan="4">
                    <asp:Label ID="Uxmsg" runat="server" Font-Bold="True" ForeColor="Red" Style="position: static"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 23px; height: 18px;" align="left">
                    <asp:Label ID="lblOldPassword" runat="server" Font-Bold="True" Text="Old Password"
                        Width="120px"></asp:Label></td>
                <td style="width: 46px; height: 18px;">
                    <asp:TextBox ID="txt_Old_Pass" runat="server" TextMode="Password" 
                        ValidationGroup="bl"></asp:TextBox></td>
                <td style="width: 8px; height: 18px">
                </td>
                <td style="width: 12px; height: 18px;">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_Old_Pass"
                        ErrorMessage="Provide Old Password" ValidationGroup="bl">*</asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td style="width: 23px" align="left">
                    <asp:Label ID="lblNewPassword" runat="server" Font-Bold="True" Text="New Password"
                        Width="136px"></asp:Label></td>
                <td style="width: 46px">
                    <asp:TextBox ID="txt_Pass_New" runat="server" TextMode="Password" 
                        ValidationGroup="bl"></asp:TextBox></td>
                <td style="width: 8px">
                </td>
                <td style="width: 12px">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_Pass_New"
                        ErrorMessage="Provide New Password" ValidationGroup="bl">*</asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td style="width: 23px" align="left">
                    <asp:Label ID="lblConfirmPassword" runat="server" Text="Confirm Password" Font-Bold="True"
                        Width="128px"></asp:Label></td>
                <td style="width: 46px">
                    <asp:TextBox ID="txt_Pass_Confirm" runat="server" TextMode="Password" 
                        ValidationGroup="bl"></asp:TextBox></td>
                <td style="width: 8px">
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txt_Pass_New"
                        ControlToValidate="txt_Pass_Confirm" 
                        ErrorMessage="New Password and Confirm  password Must be same" 
                        ValidationGroup="bl">*</asp:CompareValidator></td>
                <td style="width: 12px">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_Pass_Confirm"
                        ErrorMessage="Confirm Password" ValidationGroup="bl">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="4">
                    <asp:Button ID="btn_Change_Pass" runat="server" Text="Change" Font-Size="X-Small"
                        OnClick="btn_Change_Pass_Click" Style="position: static" 
                        ValidationGroup="bl" />
                    <asp:Button ID="btn_Cancle" runat="server" Text="Close" Font-Size="X-Small" Style="position: static"
                         CausesValidation="false" />
                </td>
            </tr>
          
            <tr style="border-collapse: collapse; border: solid 1px white" class="HeadingBlue">
                <td colspan="4" style="background-color: dimgray; height: 15px;">
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
