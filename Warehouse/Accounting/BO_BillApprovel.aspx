<%@ Page Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="BO_BillApprovel.aspx.cs" Inherits="Accounting_BO_BillApprovel" Title="BO Bill Submission" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <style type="text/css">
#popupwin {
position:fixed;
top: 0;
left: 0;
width: 90%;
height: 100%;
background-color: #000;
filter:alpha(opacity=65);
-moz-opacity:0.7;
display: none;
opacity: 0.7;
z-index: 100;

}
.pop a{
text-decoration: none;
}
.popup{
width: 100%;
height:500px;
margin: 0 auto;
position: fixed;
z-index: 101;
}
.pop{
min-width: 800px;
width: 700px;
min-height: 150px;
margin: 0px auto;
background: #f3f3f3;
position: relative;
z-index: 103;
padding: 10px;
border-radius: 5px;
box-shadow: 0 2px 5px #000;
}
.pop p{
color: #555555;
text-align: justify;
font-size:medium;
}
.pop p a{
color: #d91900;
}
.pop .x{
float: right;
height: 35px;
left: 22px;
position: relative;
top: -20px;
width: 35px;
}

</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <fieldset style="width: 1000px; border: 2px solid navy;">
        <center>
        <div>
<table width="100%">
<tr style="background-color: #0bb6e6; height: 25px">
        <td colspan="4" align="center">
            <asp:Label ID="Label37" runat="server" Font-Bold="True" Font-Size="15pt" ForeColor="WhiteSmoke"
                                        Text="Bill Submission for Verification"></asp:Label></td>
    </tr>
<tr>
<td>
<table>
<tr>
<td colspan="4">
    <asp:GridView ID="gvBOBillApp" runat="server" 
       CellPadding="5" 
        CellSpacing="10" onselectedindexchanged="gvBOBillApp_SelectedIndexChanged">
        <Columns>
            <asp:CommandField ButtonType="Button" HeaderText="Submit Bill" ShowHeader="True" 
                ShowSelectButton="True" SelectText="Submit" />
        </Columns>
        <FooterStyle BackColor="#719cb6" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" />
                                                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />  
                                                        <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                                                            Height="20px" Font-Size="10pt" />
                                                        <AlternatingRowStyle BackColor="#eeeeee" />
    </asp:GridView>
</td>
</tr>
</table>
</td>
</tr>
    
</table>
</div>
        </center>
        </fieldset>
        <img alt="" src="" id="new" runat="server" />
         <asp:Panel ID="pnllogin" class="popup" runat="server">
     <div class="popup" >
<div class="pop" style="background-color:#FFFFCC0">
<%--<img visible="true" runat="server" src="~/images/close-icon.png" alt="quit" class="x" id="x" />
--%>
<table cellspacing="1" cellpadding="3">
                                                        
                                            <tr>
<td align="center">
<%--<asp:Button ID="btn1" runat="server" Text="Submit" CssClass="BTNBLUE"/>--%>
<center><h3>Verify Bill by OTP</h3></center>
</td>
</tr>
<tr>
            <td align = "left" class="style1" style="font-size:11px; color:Red">
                Note :&nbsp; Kindly Verify - OTP इस सीयूजी मोबाइल नंबर : <asp:Label ID="lblcug" runat="server" Font-Bold="true"></asp:Label> &nbsp;पर भेजा जाएगा । यदि यह दिया गया नंबर गलत है तो कृप्या शाखा प्रोफाइल मे सीयूजी मोबाइल नंबर अपडेट करें ।
                </td> 
        </tr>
<tr>
        <td align ="left" >
           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnOTP" runat="server" Text="Generate OTP" Height="30px" 
                Width="142px" onclick="btnOTP_Click"/>
        </td>
        </tr>
        
        
         <tr id="trbtn" runat="server">
        <td align ="left" >
           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
            
           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:TextBox ID="txtCheckOTP" runat="server" Height="22px" Width="140px" placeholder="Enter OTP Here"></asp:TextBox>  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Btn_Submit" runat="server" Text="Submit" 
              Height="30px" Width="80px" onclick="Btn_Submit_Click" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btn_close" runat="server" Text="Close" Height="30px" 
                Width="80px" />
        </td>
        </tr>
        <tr>
        <td class="style2"><input id="hdfOTP" type="hidden" runat="server" /> 
        <asp:TextBox ID="txtMobNum" runat="server" Height="22px" Width="140px" Visible="false"></asp:TextBox> </td>
        </tr>
                                            </table>
<br/>
<br/>

</div>
</div>
</asp:Panel>
    <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="new" BackgroundCssClass="popup" PopupControlID="pnllogin">
    
    </asp:ModalPopupExtender>
    
    <asp:AnimationExtender ID="popupAnimation" runat="server" TargetControlID="new">
        <Animations>
                <OnClick>
         <Parallel AnimationTarget="pnllogin" Duration="0.4" Fps="10">
            <FadeIn />
          </Parallel>
       </OnClick>
        </Animations>
    </asp:AnimationExtender>
</asp:Content>

