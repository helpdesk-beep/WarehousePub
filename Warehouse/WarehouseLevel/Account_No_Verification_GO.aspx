<%@ Page Language="C#" MasterPageFile="~/MasterPage/PrivateWarehouse.master" AutoEventWireup="true" CodeFile="Account_No_Verification_GO.aspx.cs" Inherits="WarehouseLevel_Account_No_Verification_GO" Title="Untitled Page" %>

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
                                        Text="Godown Owners Accounts Verification for Online Payment"></asp:Label></td>
    </tr>
<tr>
<tr style="background-color: none; height: 25px">
        <td colspan="4" align="center">
            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="10pt" ForeColor="Red"
                                        Text="गोदाम संचालक प्रदर्शित बैंक के खाते(यह खाता वही होगा जो अनुबंध मे दर्शित किया गया है तथा जिसमे विगत समय से क्षेत्रीय कार्यालय द्वारा भुगतान किया जा रहा है। ) संबन्धित जानकारी ध्यान पूर्वक सत्यापित करे। उक्त खाते मे गोदाम किराए की राशि का ऑनलाइन भुगतान किया जाएगा। यदि उक्त दर्शित खाते मे कोई संसोधन करना हो तो संबधित क्षेत्रीय प्रबंधक स्तर पर अपना पक्ष रखकर तत्काल निराकारण करावे। खाते संबन्धित जानकारी मे किसी प्रकार की त्रुटि पाये जाने पर गोदाम संचालक/मालिक स्वयं जिम्मेदार होगा। "></asp:Label></td>
    </tr>
<tr>
<td>
<table>
<tr>
<td colspan="4">
    <asp:GridView ID="gvDSCUserVer" runat="server" 
       CellPadding="5" 
        CellSpacing="10" onrowcommand="gvDSCUserVer_RowCommand"
        >
        <Columns>
         <%--   <asp:CommandField ButtonType="Button" HeaderText="Approve" ShowHeader="True" 
                ShowSelectButton="True" SelectText="Approve" />
                  <asp:CommandField ButtonType="Button" HeaderText="Reject" ShowHeader="True" 
                ShowSelectButton="True" SelectText="Reject" />--%>
                
                  <asp:ButtonField CommandName="Approve" HeaderText="Approve" ButtonType="Button" Text="Approve" />
                   <%-- <asp:ButtonField CommandName="Reject" HeaderText="Reject" ButtonType="Button" Text="Reject" />--%>
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
</asp:Content>

