<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EncryptPassword.aspx.cs" Inherits="EncryptPassword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript"language="JavaScript" src="js/MD5.js"></script>
    <script language="javascript" type="text/javascript">
    
      
        function md5auth()
        {
        //alert('mesg');      
        //var password = "mpproc123";
        password =document.getElementById("txt02").value;
        var Masterpassword = "food2009F$";
        //alert(password);
        //alert(Masterpassword);
            if(password.length>0)
            {
            var hash =MD5(document.getElementById("txt02").value);
            //var hash=hash.substring(0,20);
            //document.getElementById("txt02").value="";
            document.getElementById("txt02").value=hash;
            }
            if(Masterpassword.length>0)
            {

            var Mhash =MD5(Masterpassword);
            //var Mhash=Mhash.substring(0,20);
            //document.getElementById("txt03").value="";
            document.getElementById("txt03").value=Mhash;
            
            }
         
        }
       function MDS(abc)
       {
            
           var checkStr = abc.id;
           var ID ="";
           // alert(abc.id);
           var Masterpassword = "food2009F$";
        
        if(checkStr.length==23)
        {
            //alert("23");
            ID = checkStr.substring(11,13);
           
            var val=document.getElementById("gvPwdEN_ctl"+ID+"_txtPwd").value;
                        
            document.getElementById("gvPwdEN_ctl"+ID+"_txtEncrypted").value=MD5(val);
            if(Masterpassword.length>0)
            {
                var UID = document.getElementById("gvPwdEN_ctl"+ID+"_txtUID").value
                var Mpwd = UID+Masterpassword;
                //alert(Mpwd);
                var Mhash =MD5(Mpwd);
           
                document.getElementById("gvPwdEN_ctl"+ID+"_txtMEncrypted").value=Mhash;
            
            }
        }
        else if(checkStr.length==24)
        {
            
            ID = checkStr.substring(11,14);
            
            var val=document.getElementById("gvPwdEN_ctl"+ID+"_txtPwd").value;
            document.getElementById("gvPwdEN_ctl"+ID+"_txtEncrypted").value=MD5(val);
            //document.getElementById("gvPwdEN_ctl"+ID+"_txtEncrypted").value=document.getElementById("gvPwdEN_ctl"+ID+"_txtPwd").value
            if(Masterpassword.length>0)
            {

                
                var UID = document.getElementById("gvPwdEN_ctl"+ID+"_txtUID").value
                var Mpwd = UID+Masterpassword;
                //alert(Mpwd);
                var Mhash =MD5(Mpwd);
                
                //var Mhash =MD5(Masterpassword);
                
                document.getElementById("gvPwdEN_ctl"+ID+"_txtMEncrypted").value=Mhash;
            
            }
        }
        
         /* password =SourceControl;
         
         var Masterpassword = "nic2010F$";
         if(SourceControl.length>0)
            {
            var hash =MD5(SourceControl);
            alert(SourceControl);
            //document.getElementById("txt02").value="";
            document.getElementById("txt02").value=hash;
            }
            if(Masterpassword.length>0)
            {

            var Mhash =MD5(Masterpassword);
            //var Mhash=Mhash.substring(0,20);
            //document.getElementById("txt03").value="";
            document.getElementById("txt03").value=Mhash;
            
            }*/
        }
        
        function GenKeyPwd()
        {
        
        var frm = document.forms[0];
            var ctr=0;
            for (i=0;i<frm.elements.length;i++) 
            {
                
                    ctr=ctr+1;
                           
            }
            
              var Newpassword = '<%=Session["SP"]%>';
              var concatStr;
              var randomnumber;
              
           for (k=2;k<=203;k++)
           {
                 //alert("hi");             
                     //for single digit and double digit it convert to double digit leading zero...gvPwdEN_ctl03_txtPwd
                     if (k<100) 
                         concatStr="00" + k;
                     if (k<10) 
                         concatStr="0" + k;
                     else
                         concatStr=k;
                  // randomnumber=Math.floor(Math.random()*111111);
                   // document.getElementById("gvPwdEN"+concatStr+"_lblrn").value=randomnumber;
                   //alert(document.getElementById("gvPwdEN_ctl03_txtPwd").value);
                   //alert(document.getElementById("gvPwdEN_ctl"+concatStr+"_txtPwd").value);
                   
                   // randomnumber=MD5(document.getElementById("gvPwdEN_ctl"+concatStr+"_txtPwd").value);
                    document.getElementById("gvPwdEN_ctl"+concatStr+"_txtEncrypted").value=MD5(document.getElementById("gvPwdEN_ctl"+concatStr+"_txtPwd").value);
                 
                     
           }
               
        //End Call MD5 for old flow..
        md5auth();
        alert("Done");
        }
        
        </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td>
                    <asp:GridView ID="gvPwdEN" runat="server" CellPadding="0" ForeColor="#FFC080" BorderColor="Silver" HorizontalAlign="left" BackColor="Silver" TabIndex="8" AutoGenerateColumns="False">
                        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                        <Columns>
                            
                            <asp:TemplateField HeaderText="Encrypted">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtEncrypted" runat="server" ></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                  
                                    <asp:TextBox ID="txtEncrypted"  runat="server" ></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle Width="50px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="pwd">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtPwd" runat="server"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                   
                                    <asp:TextBox ID="txtPwd" runat="server" Text='<%# Bind("pwd") %>'></asp:TextBox>
                                </ItemTemplate>
                                
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="login_id">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtUID" runat="server"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                   
                                    <asp:TextBox ID="txtUID" runat="server" Text='<%# Bind("login_id") %>'></asp:TextBox>
                                </ItemTemplate>
                                
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="User_Name">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                   
                                    <asp:TextBox ID="txtName" runat="server" Text='<%# Bind("User_Name") %>'></asp:TextBox>
                                </ItemTemplate>
                                
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="MEncrypted">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtMEncrypted" runat="server" ></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                  
                                    <asp:TextBox ID="txtMEncrypted"  runat="server" ></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle Width="50px" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Button ID="btnUpdate" runat="server" Text="EnCrypt" OnClientClick="javascript:MDS(this);" />
                                </ItemTemplate>
                                
                            </asp:TemplateField>
                            
                        </Columns>
                        <RowStyle BackColor="#FFFBD6" ForeColor="#333333" BorderColor="#FFC080" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" BorderColor="White" />
                        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" BorderColor="Silver"  />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td>
                    </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" /></td>
            </tr>
        </table>
        
    </div>
    </form>
</body>
</html>
