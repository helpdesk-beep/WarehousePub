<%@ Page Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="WLC_DepositeForm_ChkQuality.aspx.cs" Inherits="IssueCenterLevel_Storage_WLC_DepositeForm_ChkQuality" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            height: 17px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 <table style="width: 871px" >
        <tr>
            <td class="style1" colspan="2">
                </td>
        </tr>
        <tr>
            <td class="style3" colspan="2" align = "left" style="font-size:12px">
                Select Commodity&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:DropDownList ID="ddl_commodity" runat="server" AutoPostBack="True" 
                    Height="28px" Width="200px" 
                    onselectedindexchanged="ddl_commodity_SelectedIndexChanged">
                </asp:DropDownList>
                &nbsp;&nbsp;&nbsp;&nbsp;
                                Select Proviosnal&nbsp; Depositer Form Number
               &nbsp;&nbsp; <asp:DropDownList ID="ddl_depositerform" runat="server" Height="27px" 
                    Width="200px" AutoPostBack="True" 
                    onselectedindexchanged="ddl_depositerform_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="style5" colspan="2" align = "left">

            </td>
        </tr>
        <tr>
            <td  colspan="2" align = "left" class="style1">
                </td>
        </tr>
        <tr>
            <td  colspan="2" align = "left" class="style1" style="font-size:11px; color:Red">
                Note :&nbsp; Kindly Verify - OTP इस सीयूजी मोबाइल नंबर : <asp:Label ID="lblcug" runat="server" Font-Bold="true"></asp:Label> &nbsp;पर भेजा जाएगा । यदि यह दिया गया नंबर गलत है तो कृप्या शाखा प्रोफाइल मे सीयूजी मोबाइल नंबर अपडेट करें ।
                </td> 
        </tr>        
        <tr>
            <td colspan="2" align = "left" style="font-size:11px">
                जमा स्‍कंध जिनका स्‍वीकृति पत्रक निम्‍न विवरण अनुसार उपार्जन केन्‍द्र एवं 
                संग्रहण केन्‍द्र पर पदस्‍थ सर्वेयर एवं व्‍दारा स्‍कंध गुणवत्‍ता की जांच से 
                MPSCSC व्‍दारा सहमत होकर केवल FAQ स्‍कंध गोदाम में<br /> जमा किया गया है । NAFED के 
                पक्ष में वेअर हाउस रसीदें जारी करनें लिये तदर्थ डिपाजिट पत्रक को ऑन लाइन MP SWLC 
                STORAGE मॉडूल्‍य में टांसफर किया जा रहा है , कृपया तदर्थ डिपाजिट<br /> ऑन लाईन 
                स्‍वीकृति / रीजेक्‍ट की स्थिति स्‍वीकृति पत्रक के समक्ष बाक्‍स में दर्ज कर 
                सुरक्षित बटन से सुरक्षित कर वापिस MPSCSC माडूल्‍य में वापिस करें ताकि उसका अंतिम 
                डिपाजिट पत्रक NAFED के<br /> पक्ष में WHR हेतु जारी किया जा सके। </td>
        </tr>
        <tr>
            <td >
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:GridView ID="GridView2_Chana" runat="server" AutoGenerateColumns="False" 
                    BackColor="#DEBA84" BorderColor="#DEBA84" CellPadding="4" GridLines="None" 
                   
                    ShowFooter="True" Width="100%" Visible="False">
                    <Columns>
                        <asp:BoundField DataField="GdnId" HeaderStyle-HorizontalAlign="Center" 
                            HeaderText="GdnId" ItemStyle-HorizontalAlign="Center" 
                            SortExpression="GdnId">
                           <ItemStyle HorizontalAlign="Center" Width="0px" Font-Size="0px" />
                           <HeaderStyle HorizontalAlign="Center" Width="0px" Font-Size="0px" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="Godown_Name" HeaderStyle-HorizontalAlign="Center" 
                            HeaderText="Godown Name." ItemStyle-HorizontalAlign="Center" 
                            SortExpression="Godown_Name">
                            <ItemStyle CssClass="griditemlaro" Font-Size="Small" />
                           <HeaderStyle CssClass="gridlarohead" Font-Size="Small" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="StackName" HeaderStyle-HorizontalAlign="Center" 
                            HeaderText="Stack Name." ItemStyle-HorizontalAlign="Center" 
                            SortExpression="StackName">
                            <ItemStyle CssClass="griditemlaro" Font-Size="Small" />
                             <HeaderStyle CssClass="gridlarohead" Font-Size="Small" />
                        </asp:BoundField>
                        
                         <asp:BoundField DataField="TC_Number" HeaderStyle-HorizontalAlign="Center" 
                            HeaderText="TC_Number." ItemStyle-HorizontalAlign="Center" 
                            SortExpression="TC_Number">
                            <ItemStyle CssClass="griditemlaro" Font-Size="Small" />
                             <HeaderStyle CssClass="gridlarohead" Font-Size="Small" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="Society_Id" HeaderStyle-HorizontalAlign="Center" 
                            HeaderText="Society Id" ItemStyle-HorizontalAlign="Center" 
                            SortExpression="Society_Id">
                            <ItemStyle HorizontalAlign="Center" Width="0px" Font-Size="0px" />
                                    <HeaderStyle HorizontalAlign="Center" Width="0px" Font-Size="0px" />
                        </asp:BoundField>

                        <asp:BoundField DataField="Society_Name" HeaderStyle-HorizontalAlign="Center" 
                            HeaderText="Society Name" ItemStyle-HorizontalAlign="Center" 
                            SortExpression="Society_Name">
                             <ItemStyle HorizontalAlign="Center" Font-Size="Small" />
                            <HeaderStyle CssClass="gridlarohead" Font-Size="Small" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="Acceptance_No" HeaderStyle-HorizontalAlign="Center" 
                            HeaderText="Acceptance_No" ItemStyle-HorizontalAlign="Center" 
                            SortExpression="Acceptance_No">
                            <ItemStyle CssClass="griditemlaro" Font-Size="Small" />
                             <HeaderStyle CssClass="gridlarohead" Font-Size="Small" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="Acceptance_Date" HeaderStyle-HorizontalAlign="Center" 
                            HeaderText="Acceptance_Date" ItemStyle-HorizontalAlign="Center" 
                            SortExpression="Acceptance_Date">
                            <ItemStyle CssClass="griditemlaro" Font-Size="Small" />
                           <HeaderStyle CssClass="gridlarohead" Font-Size="Small" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="Recd_Bags" HeaderStyle-HorizontalAlign="Center" 
                            HeaderText="Recd_Bags" ItemStyle-HorizontalAlign="Center" 
                            SortExpression="Recd_Bags">
                            <ItemStyle CssClass="griditemlaro" Font-Size="Small" />
                           <HeaderStyle CssClass="gridlarohead" Font-Size="Small" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="Jute_Newbags" HeaderStyle-HorizontalAlign="Center" 
                            HeaderText="Jute_Newbags" ItemStyle-HorizontalAlign="Center" 
                            SortExpression="Jute_Newbags">
                            <ItemStyle CssClass="griditemlaro" Font-Size="Small" />
                           <HeaderStyle CssClass="gridlarohead" Font-Size="Small" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="PP_Bags" HeaderStyle-HorizontalAlign="Center" 
                            HeaderText="PP_Bags" ItemStyle-HorizontalAlign="Center" 
                            SortExpression="PP_Bags">
                            <ItemStyle CssClass="griditemlaro" Font-Size="Small" />
                           <HeaderStyle CssClass="gridlarohead" Font-Size="Small" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="Jute_oldBags" HeaderStyle-HorizontalAlign="Center" 
                            HeaderText="Jute_oldBags" ItemStyle-HorizontalAlign="Center" 
                            SortExpression="Jute_oldBags">
                            <ItemStyle CssClass="griditemlaro" Font-Size="Small" />
                             <HeaderStyle CssClass="gridlarohead" Font-Size="Small" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="Atwill_Bags" HeaderStyle-HorizontalAlign="Center" 
                            HeaderText="Atwill_Bags" ItemStyle-HorizontalAlign="Center" 
                            SortExpression="Atwill_Bags">
                            <ItemStyle CssClass="griditemlaro" Font-Size="Small" />
                             <HeaderStyle CssClass="gridlarohead" Font-Size="Small" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="Gram_ForeignMatter" HeaderStyle-HorizontalAlign="Center" 
                            HeaderText="Gdwn_ForeignMatter" ItemStyle-HorizontalAlign="Center" 
                            SortExpression="Gram_ForeignMatter">
                            <ItemStyle CssClass="griditemlaro" Font-Size="Small" />
                            <HeaderStyle CssClass="gridlarohead" Font-Size="Small" />
                        </asp:BoundField>
                        
                         <asp:BoundField DataField="NetWeight" HeaderStyle-HorizontalAlign="Center" 
                            HeaderText="NetWeight" ItemStyle-HorizontalAlign="Center" 
                            SortExpression="NetWeight">
                            <ItemStyle CssClass="griditemlaro" Font-Size="Small" />
                             <HeaderStyle CssClass="gridlarohead" Font-Size="Small" />
                        </asp:BoundField>
                        
                        
                        <asp:BoundField DataField="Gram_OtherFoodGrains" HeaderStyle-HorizontalAlign="Center" 
                            HeaderText="Gdwn_OtherFoodGrains" ItemStyle-HorizontalAlign="Center" 
                            SortExpression="Gram_OtherFoodGrains">
                            <ItemStyle CssClass="griditemlaro" Font-Size="Small" />
                             <HeaderStyle CssClass="gridlarohead" Font-Size="Small" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="Gram_DamagedGrains" HeaderStyle-HorizontalAlign="Center" 
                            HeaderText="Gdwn_DamagedGrains" ItemStyle-HorizontalAlign="Center" 
                            SortExpression="Gram_DamagedGrains">
                            <ItemStyle CssClass="griditemlaro" Font-Size="Small" />
                            <HeaderStyle CssClass="gridlarohead" Font-Size="Small" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="Gram_SligDamagedTouchGrains" HeaderStyle-HorizontalAlign="Center" 
                            HeaderText="Gdwn_SligDamagedTouchGrains" ItemStyle-HorizontalAlign="Center" 
                            SortExpression="Gram_SligDamagedTouchGrains">
                            <ItemStyle CssClass="griditemlaro" Font-Size="Small" />
                            <HeaderStyle CssClass="gridlarohead" Font-Size="Small" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="Gram_ImmaShrivAndBroGrains" HeaderStyle-HorizontalAlign="Center" 
                            HeaderText="Gdwn_ImmaShrivAndBroGrains" ItemStyle-HorizontalAlign="Center" 
                            SortExpression="Gram_ImmaShrivAndBroGrains">
                            <ItemStyle CssClass="griditemlaro" Font-Size="Small" />
                             <HeaderStyle CssClass="gridlarohead" Font-Size="Small" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="Gram_AdmixOfOtherVarieties" HeaderStyle-HorizontalAlign="Center" 
                            HeaderText="Gdwn_AdmixOfOtherVarieties" ItemStyle-HorizontalAlign="Center" 
                            SortExpression="Gram_AdmixOfOtherVarieties">
                            <ItemStyle CssClass="griditemlaro" Font-Size="Small" />
                             <HeaderStyle CssClass="gridlarohead" Font-Size="Small" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="Gram_WeevilleGrains" HeaderStyle-HorizontalAlign="Center" 
                            HeaderText="Gdwn_WeevilleGrains" ItemStyle-HorizontalAlign="Center" 
                            SortExpression="Gram_WeevilleGrains">
                            <ItemStyle CssClass="griditemlaro" Font-Size="Small" />
                             <HeaderStyle CssClass="gridlarohead" Font-Size="Small" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="Gram_MositureContent" HeaderStyle-HorizontalAlign="Center" 
                            HeaderText="Gdwn_MositureContent" ItemStyle-HorizontalAlign="Center" 
                            SortExpression="Gram_MositureContent">
                            <ItemStyle CssClass="griditemlaro" Font-Size="Small" />
                            <HeaderStyle CssClass="gridlarohead" Font-Size="Small" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="Srvyr_Nm" HeaderStyle-HorizontalAlign="Center" 
                            HeaderText="Surveyor" ItemStyle-HorizontalAlign="Center" 
                            SortExpression="Srvyr_Nm">
                             <ItemStyle HorizontalAlign="Center" Width="0px" Font-Size="0px" />
                              <HeaderStyle HorizontalAlign="Center" Width="0px" Font-Size="0px" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="foreignMatter" HeaderStyle-HorizontalAlign="Center" 
                            HeaderText="Mandi foreignMatter" ItemStyle-HorizontalAlign="Center" 
                            SortExpression="foreignMatter">
                            <ItemStyle CssClass="griditemlaro" Font-Size="Small" />
                            <HeaderStyle CssClass="gridlarohead" Font-Size="Small" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="OtherFoodGrains" HeaderStyle-HorizontalAlign="Center" 
                            HeaderText="Mandi OtherFoodGrains" ItemStyle-HorizontalAlign="Center" 
                            SortExpression="OtherFoodGrains">
                            <ItemStyle CssClass="griditemlaro" Font-Size="Small" />
                             <HeaderStyle CssClass="gridlarohead" Font-Size="Small" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="DamagedGrains" HeaderStyle-HorizontalAlign="Center" 
                            HeaderText="Mandi DamagedGrains" ItemStyle-HorizontalAlign="Center" 
                            SortExpression="DamagedGrains">
                            <ItemStyle CssClass="griditemlaro" Font-Size="Small" />
                           <HeaderStyle CssClass="gridlarohead" Font-Size="Small" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="SligDamagedTouchGrains" HeaderStyle-HorizontalAlign="Center" 
                            HeaderText="Mandi SligDamagedTouchGrains" ItemStyle-HorizontalAlign="Center" 
                            SortExpression="SligDamagedTouchGrains">
                            <ItemStyle CssClass="griditemlaro" Font-Size="Small" />
                            <HeaderStyle CssClass="gridlarohead" Font-Size="Small" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="ImmaShrivAndBroGrains" HeaderStyle-HorizontalAlign="Center" 
                            HeaderText="Mandi ImmaShrivAndBroGrains" ItemStyle-HorizontalAlign="Center" 
                            SortExpression="ImmaShrivAndBroGrains">
                            <ItemStyle CssClass="griditemlaro" Font-Size="Small" />
                            <HeaderStyle CssClass="gridlarohead" Font-Size="Small" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="AdmixOfOtherVarieties" HeaderStyle-HorizontalAlign="Center" 
                            HeaderText="Mandi AdmixOfOtherVarieties" ItemStyle-HorizontalAlign="Center" 
                            SortExpression="AdmixOfOtherVarieties">
                            <ItemStyle CssClass="griditemlaro" Font-Size="Small" />
                            <HeaderStyle CssClass="gridlarohead" Font-Size="Small" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="WeevilleGrains" HeaderStyle-HorizontalAlign="Center" 
                            HeaderText="Mandi WeevilleGrains" ItemStyle-HorizontalAlign="Center" 
                            SortExpression="WeevilleGrains">
                            <ItemStyle CssClass="griditemlaro" Font-Size="Small" />
                           <HeaderStyle CssClass="gridlarohead" Font-Size="Small" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="MositureContent" HeaderStyle-HorizontalAlign="Center" 
                            HeaderText="Mandi MositureContent" ItemStyle-HorizontalAlign="Center" 
                            SortExpression="MositureContent">
                            <ItemStyle CssClass="griditemlaro" Font-Size="Small" />
                            <HeaderStyle CssClass="gridlarohead" Font-Size="Small" />
                        </asp:BoundField>
                        
                        <asp:TemplateField HeaderText="20 % जाँच की स्थिति">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddl_status_1" runat="server" Width="100px">
                                        <asp:ListItem Value="N">नहीं </asp:ListItem>
                                        <asp:ListItem Value="Y">हाँ </asp:ListItem>
                                        
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                             <asp:TemplateField HeaderText="रिजेक्ट की स्थिति">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddl_status_2" runat="server" Width="100px">
                                        
                                        <asp:ListItem Value="N">नहीं </asp:ListItem>
                                        <asp:ListItem Value="Y">हाँ </asp:ListItem>
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                       
                    </Columns>
                    <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" HorizontalAlign="Right" />
                    <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                    <SelectedRowStyle BackColor="#738A9C" ForeColor="#333333" />
                    <PagerStyle BackColor="#284775" ForeColor="#8C4510" HorizontalAlign="Center" />
                    <HeaderStyle BackColor="#A55129" ForeColor="White" />
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <EditRowStyle BackColor="#999999" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td >
                <asp:GridView ID="GridView2_Masoor" runat="server" AutoGenerateColumns="False" 
                    BackColor="#DEBA84" BorderColor="#DEBA84" CellPadding="4" GridLines="None" 
                   
                    ShowFooter="True" Width="100%" Visible="False">
                    <Columns>
                        <asp:BoundField DataField="GdnId" HeaderStyle-HorizontalAlign="Center" 
                            HeaderText="GdnId" ItemStyle-HorizontalAlign="Center" 
                            SortExpression="GdnId">
                            <ItemStyle HorizontalAlign="Center" Width="0px" Font-Size="0px" />
                           <HeaderStyle HorizontalAlign="Center" Width="0px" Font-Size="0px" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="Godown_Name" HeaderStyle-HorizontalAlign="Center" 
                            HeaderText="Godown Name." ItemStyle-HorizontalAlign="Center" 
                            SortExpression="Godown_Name">
                            <ItemStyle CssClass="griditemlaro" />
                           <HeaderStyle CssClass="gridlarohead" Font-Size="Small" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="StackName" HeaderStyle-HorizontalAlign="Center" 
                            HeaderText="Stack Name." ItemStyle-HorizontalAlign="Center" 
                            SortExpression="StackName">
                            <ItemStyle CssClass="griditemlaro" />
                             <HeaderStyle CssClass="gridlarohead" Font-Size="Small" />
                        </asp:BoundField>
                         <asp:BoundField DataField="TC_Number" HeaderStyle-HorizontalAlign="Center" 
                            HeaderText="TC_Number." ItemStyle-HorizontalAlign="Center" 
                            SortExpression="TC_Number">
                            <ItemStyle CssClass="griditemlaro" Font-Size="Small" />
                             <HeaderStyle CssClass="gridlarohead" Font-Size="Small" />
                        </asp:BoundField>                        
                        
                        <asp:BoundField DataField="Society_Id" HeaderStyle-HorizontalAlign="Center" 
                            HeaderText="Society Id" ItemStyle-HorizontalAlign="Center" 
                            SortExpression="Society_Id">
                            <ItemStyle HorizontalAlign="Center" Width="0px" Font-Size="0px" />
                           <HeaderStyle HorizontalAlign="Center" Width="0px" Font-Size="0px" />
                        </asp:BoundField>

                        <asp:BoundField DataField="Society_Name" HeaderStyle-HorizontalAlign="Center" 
                            HeaderText="Society Name" ItemStyle-HorizontalAlign="Center" 
                            SortExpression="Society_Name">
                             <ItemStyle HorizontalAlign="Center" Width="0px" Font-Size="0px" />
                           <HeaderStyle HorizontalAlign="Center" Width="0px" Font-Size="0px" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="Acceptance_No" HeaderStyle-HorizontalAlign="Center" 
                            HeaderText="Acceptance_No" ItemStyle-HorizontalAlign="Center" 
                            SortExpression="Acceptance_No">
                            <ItemStyle CssClass="griditemlaro" />
                             <HeaderStyle CssClass="gridlarohead" Font-Size="Small" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="Acceptance_Date" HeaderStyle-HorizontalAlign="Center" 
                            HeaderText="Acceptance_Date" ItemStyle-HorizontalAlign="Center" 
                            SortExpression="Acceptance_Date">
                            <ItemStyle CssClass="griditemlaro" />
                           <HeaderStyle CssClass="gridlarohead" Font-Size="Small" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="Recd_Bags" HeaderStyle-HorizontalAlign="Center" 
                            HeaderText="Recd_Bags" ItemStyle-HorizontalAlign="Center" 
                            SortExpression="Recd_Bags">
                            <ItemStyle CssClass="griditemlaro" />
                           <HeaderStyle CssClass="gridlarohead" Font-Size="Small" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="Jute_Newbags" HeaderStyle-HorizontalAlign="Center" 
                            HeaderText="Jute_Newbags" ItemStyle-HorizontalAlign="Center" 
                            SortExpression="RecdBags_JuteNew">
                            <ItemStyle CssClass="griditemlaro" />
                           <HeaderStyle CssClass="gridlarohead" Font-Size="Small" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="PP_Bags" HeaderStyle-HorizontalAlign="Center" 
                            HeaderText="PP_Bags" ItemStyle-HorizontalAlign="Center" 
                            SortExpression="PP_Bags">
                            <ItemStyle CssClass="griditemlaro" />
                           <HeaderStyle CssClass="gridlarohead" Font-Size="Small" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="Jute_oldBags" HeaderStyle-HorizontalAlign="Center" 
                            HeaderText="Jute_oldBags" ItemStyle-HorizontalAlign="Center" 
                            SortExpression="Jute_oldBags">
                            <ItemStyle CssClass="griditemlaro" />
                             <HeaderStyle CssClass="gridlarohead" Font-Size="Small" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="Atwill_Bags" HeaderStyle-HorizontalAlign="Center" 
                            HeaderText="Atwill_Bags" ItemStyle-HorizontalAlign="Center" 
                            SortExpression="Atwill_Bags">
                            <ItemStyle CssClass="griditemlaro" />
                             <HeaderStyle CssClass="gridlarohead" Font-Size="Small" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="NetWeight" HeaderStyle-HorizontalAlign="Center" 
                            HeaderText="NetWeight" ItemStyle-HorizontalAlign="Center" 
                            SortExpression="NetWeight">
                            <ItemStyle CssClass="griditemlaro" />
                             <HeaderStyle CssClass="gridlarohead" Font-Size="Small" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="Massur_ForeignMatter" HeaderStyle-HorizontalAlign="Center" 
                            HeaderText="Gdwn_ForeignMatter" ItemStyle-HorizontalAlign="Center" 
                            SortExpression="Massur_ForeignMatter">
                            <ItemStyle CssClass="griditemlaro" />
                            <HeaderStyle CssClass="gridlarohead" Font-Size="Small" />
                        </asp:BoundField>
                        
                        
                        <asp:BoundField DataField="Massur_Admixture" HeaderStyle-HorizontalAlign="Center" 
                            HeaderText="Gdwn_Admixture" ItemStyle-HorizontalAlign="Center" 
                            SortExpression="Massur_Admixture">
                            <ItemStyle CssClass="griditemlaro" />
                             <HeaderStyle CssClass="gridlarohead" Font-Size="Small" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="Massur_DamagedPulses" HeaderStyle-HorizontalAlign="Center" 
                            HeaderText="Gdwn_DamagedPulses" ItemStyle-HorizontalAlign="Center" 
                            SortExpression="Massur_DamagedPulses">
                            <ItemStyle CssClass="griditemlaro" />
                            <HeaderStyle CssClass="gridlarohead" Font-Size="Small" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="Massur_SligDamagedPulses" HeaderStyle-HorizontalAlign="Center" 
                            HeaderText="Gdwn_SligDamagedPulses" ItemStyle-HorizontalAlign="Center" 
                            SortExpression="Massur_SligDamagedPulses">
                            <ItemStyle CssClass="griditemlaro" />
                            <HeaderStyle CssClass="gridlarohead" Font-Size="Small" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="Massur_ImmaShrivellPulses" HeaderStyle-HorizontalAlign="Center" 
                            HeaderText="Gdwn_ImmaShrivellPulses" ItemStyle-HorizontalAlign="Center" 
                            SortExpression="Massur_ImmaShrivellPulses">
                            <ItemStyle CssClass="griditemlaro" />
                             <HeaderStyle CssClass="gridlarohead" Font-Size="Small" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="Massur_weevilledPulses" HeaderStyle-HorizontalAlign="Center" 
                            HeaderText="Gdwn_weevilledPulses" ItemStyle-HorizontalAlign="Center" 
                            SortExpression="Massur_weevilledPulses">
                            <ItemStyle CssClass="griditemlaro" />
                             <HeaderStyle CssClass="gridlarohead" Font-Size="Small" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="Massur_MoistureContent" HeaderStyle-HorizontalAlign="Center" 
                            HeaderText="Gdwn_MoistureContent" ItemStyle-HorizontalAlign="Center" 
                            SortExpression="Massur_MoistureContent">
                            <ItemStyle CssClass="griditemlaro" />
                             <HeaderStyle CssClass="gridlarohead" Font-Size="Small" />
                        </asp:BoundField>
                        
                        
                        <asp:BoundField DataField="Srvyr_Nm" HeaderStyle-HorizontalAlign="Center" 
                            HeaderText="Mandi Surveyor" ItemStyle-HorizontalAlign="Center" 
                            SortExpression="Srvyr_Nm">
                            <ItemStyle HorizontalAlign="Center" Width="0px" Font-Size="0px" />
                           <HeaderStyle HorizontalAlign="Center" Width="0px" Font-Size="0px" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="Foreignmatter" HeaderStyle-HorizontalAlign="Center" 
                            HeaderText="Mandi Foreignmatter" ItemStyle-HorizontalAlign="Center" 
                            SortExpression="Foreignmatter">
                            <ItemStyle CssClass="griditemlaro" />
                            <HeaderStyle CssClass="gridlarohead" Font-Size="Small" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="Admixture" HeaderStyle-HorizontalAlign="Center" 
                            HeaderText="Mandi Admixture" ItemStyle-HorizontalAlign="Center" 
                            SortExpression="Admixture">
                            <ItemStyle CssClass="griditemlaro" />
                             <HeaderStyle CssClass="gridlarohead" Font-Size="Small" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="Damagedpuls" HeaderStyle-HorizontalAlign="Center" 
                            HeaderText="Mandi Damagedpuls" ItemStyle-HorizontalAlign="Center" 
                            SortExpression="Damagedpuls">
                            <ItemStyle CssClass="griditemlaro" />
                           <HeaderStyle CssClass="gridlarohead" Font-Size="Small" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="Slightlydamaged" HeaderStyle-HorizontalAlign="Center" 
                            HeaderText="Mandi Slightlydamaged" ItemStyle-HorizontalAlign="Center" 
                            SortExpression="Slightlydamaged">
                            <ItemStyle CssClass="griditemlaro" />
                            <HeaderStyle CssClass="gridlarohead" Font-Size="Small" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="ImmatureShrivelled" HeaderStyle-HorizontalAlign="Center" 
                            HeaderText="Mandi ImmatureShrivelled" ItemStyle-HorizontalAlign="Center" 
                            SortExpression="ImmatureShrivelled">
                            <ItemStyle CssClass="griditemlaro" />
                            <HeaderStyle CssClass="gridlarohead" Font-Size="Small" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="Weevilled" HeaderStyle-HorizontalAlign="Center" 
                            HeaderText="Mandi Weevilled" ItemStyle-HorizontalAlign="Center" 
                            SortExpression="Weevilled">
                            <ItemStyle CssClass="griditemlaro" />
                            <HeaderStyle CssClass="gridlarohead" Font-Size="Small" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="MoistureContent" HeaderStyle-HorizontalAlign="Center" 
                            HeaderText="Mandi MoistureContent" ItemStyle-HorizontalAlign="Center" 
                            SortExpression="MoistureContent">
                            <ItemStyle CssClass="griditemlaro" />
                           <HeaderStyle CssClass="gridlarohead" Font-Size="Small" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="20 % जाँच की स्थिति">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddl_status_3" runat="server" Width="100px">
                                        <asp:ListItem Value="N">नहीं </asp:ListItem>
                                        <asp:ListItem Value="Y">हाँ </asp:ListItem>
                                        
                                    </asp:DropDownList>
                                </ItemTemplate>
                         </asp:TemplateField>  
                             <asp:TemplateField HeaderText="रिजेक्ट की स्थिति">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddl_status_4" runat="server" Width="100px">
                                        
                                        <asp:ListItem Value="N">नहीं </asp:ListItem>
                                        <asp:ListItem Value="Y">हाँ </asp:ListItem>
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>                                               
                                              
                       
                    </Columns>
                    <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" HorizontalAlign="Right" />
                    <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                    <SelectedRowStyle BackColor="#738A9C" ForeColor="#333333" />
                    <PagerStyle BackColor="#284775" ForeColor="#8C4510" HorizontalAlign="Center" />
                    <HeaderStyle BackColor="#A55129" ForeColor="White" />
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <EditRowStyle BackColor="#999999" />
                </asp:GridView>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        
        <tr>
            <td >
                <asp:GridView ID="GridView2_Sarso" runat="server" AutoGenerateColumns="False" 
                    BackColor="#DEBA84" BorderColor="#DEBA84" CellPadding="4" GridLines="None" 
                   
                    ShowFooter="True" Width="100%" Visible="False">
                    <Columns>
                        <asp:BoundField DataField="GdnId" HeaderStyle-HorizontalAlign="Center" 
                            HeaderText="GdnId" ItemStyle-HorizontalAlign="Center" 
                            SortExpression="GdnId">
                             <ItemStyle HorizontalAlign="Center" Width="0px" Font-Size="0px" />
                           <HeaderStyle HorizontalAlign="Center" Width="0px" Font-Size="0px" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="Godown_Name" HeaderStyle-HorizontalAlign="Center" 
                            HeaderText="Godown Name." ItemStyle-HorizontalAlign="Center" 
                            SortExpression="Godown_Name">
                            <ItemStyle CssClass="griditemlaro" />
                           <HeaderStyle CssClass="gridlarohead" Font-Size="Small" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="StackName" HeaderStyle-HorizontalAlign="Center" 
                            HeaderText="Stack Name." ItemStyle-HorizontalAlign="Center" 
                            SortExpression="StackName">
                            <ItemStyle CssClass="griditemlaro" />
                             <HeaderStyle CssClass="gridlarohead" Font-Size="Small" />
                        </asp:BoundField>
                        
                         <asp:BoundField DataField="TC_Number" HeaderStyle-HorizontalAlign="Center" 
                            HeaderText="TC_Number." ItemStyle-HorizontalAlign="Center" 
                            SortExpression="TC_Number">
                            <ItemStyle CssClass="griditemlaro" Font-Size="Small" />
                             <HeaderStyle CssClass="gridlarohead" Font-Size="Small" />
                        </asp:BoundField>                        
                        
                        <asp:BoundField DataField="Society_Id" HeaderStyle-HorizontalAlign="Center" 
                            HeaderText="Society Id" ItemStyle-HorizontalAlign="Center" 
                            SortExpression="Society_Id">
                             <ItemStyle HorizontalAlign="Center" Width="0px" Font-Size="0px" />
                           <HeaderStyle HorizontalAlign="Center" Width="0px" Font-Size="0px" />
                        </asp:BoundField>

                        <asp:BoundField DataField="Society_Name" HeaderStyle-HorizontalAlign="Center" 
                            HeaderText="Society Name" ItemStyle-HorizontalAlign="Center" 
                            SortExpression="Society_Name">
                             <ItemStyle HorizontalAlign="Center" Width="0px" Font-Size="0px" />
                           <HeaderStyle HorizontalAlign="Center" Width="0px" Font-Size="0px" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="Acceptance_No" HeaderStyle-HorizontalAlign="Center" 
                            HeaderText="Acceptance_No" ItemStyle-HorizontalAlign="Center" 
                            SortExpression="Acceptance_No">
                            <ItemStyle CssClass="griditemlaro" />
                             <HeaderStyle CssClass="gridlarohead" Font-Size="Small" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="Acceptance_Date" HeaderStyle-HorizontalAlign="Center" 
                            HeaderText="Acceptance_Date" ItemStyle-HorizontalAlign="Center" 
                            SortExpression="Acceptance_Date">
                            <ItemStyle CssClass="griditemlaro" />
                           <HeaderStyle CssClass="gridlarohead" Font-Size="Small" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="Recd_Bags" HeaderStyle-HorizontalAlign="Center" 
                            HeaderText="Recd_Bags" ItemStyle-HorizontalAlign="Center" 
                            SortExpression="Recd_Bags">
                            <ItemStyle CssClass="griditemlaro" />
                           <HeaderStyle CssClass="gridlarohead" Font-Size="Small" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="Jute_Newbags" HeaderStyle-HorizontalAlign="Center" 
                            HeaderText="Jute_Newbags" ItemStyle-HorizontalAlign="Center" 
                            SortExpression="Jute_Newbags">
                            <ItemStyle CssClass="griditemlaro" />
                           <HeaderStyle CssClass="gridlarohead" Font-Size="Small" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="PP_Bags" HeaderStyle-HorizontalAlign="Center" 
                            HeaderText="PP_Bags" ItemStyle-HorizontalAlign="Center" 
                            SortExpression="PP_Bags">
                            <ItemStyle CssClass="griditemlaro" />
                           <HeaderStyle CssClass="gridlarohead" Font-Size="Small" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="Jute_oldBags" HeaderStyle-HorizontalAlign="Center" 
                            HeaderText="Jute_oldBags" ItemStyle-HorizontalAlign="Center" 
                            SortExpression="Jute_oldBags">
                            <ItemStyle CssClass="griditemlaro" />
                             <HeaderStyle CssClass="gridlarohead" Font-Size="Small" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="Atwill_Bags" HeaderStyle-HorizontalAlign="Center" 
                            HeaderText="Atwill_Bags" ItemStyle-HorizontalAlign="Center" 
                            SortExpression="Atwill_Bags">
                            <ItemStyle CssClass="griditemlaro" />
                             <HeaderStyle CssClass="gridlarohead" Font-Size="Small" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="NetWeight" HeaderStyle-HorizontalAlign="Center" 
                            HeaderText="NetWeight" ItemStyle-HorizontalAlign="Center" 
                            SortExpression="NetWeight">
                            <ItemStyle CssClass="griditemlaro" />
                             <HeaderStyle CssClass="gridlarohead" Font-Size="Small" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="Sarson_ImpForeignmatt_IncTaramira" HeaderStyle-HorizontalAlign="Center" 
                            HeaderText="Gdwn_ImpForeignmatt_IncTaramira" ItemStyle-HorizontalAlign="Center" 
                            SortExpression="Sarson_ImpForeignmatt_IncTaramira">
                            <ItemStyle CssClass="griditemlaro" />
                            <HeaderStyle CssClass="gridlarohead" Font-Size="Small" />
                        </asp:BoundField>
                                                
                        <asp:BoundField DataField="Sarson_Admix_WOT_IncToria" HeaderStyle-HorizontalAlign="Center" 
                            HeaderText="Gdwn_Admix_WOT_IncToria" ItemStyle-HorizontalAlign="Center" 
                            SortExpression="Sarson_Admix_WOT_IncToria">
                            <ItemStyle CssClass="griditemlaro" />
                             <HeaderStyle CssClass="gridlarohead" Font-Size="Small" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="Sarson_Unripe_ShirvellImmature" HeaderStyle-HorizontalAlign="Center" 
                            HeaderText="Gdwn_Unripe_ShirvellImmature" ItemStyle-HorizontalAlign="Center" 
                            SortExpression="Sarson_Unripe_ShirvellImmature">
                            <ItemStyle CssClass="griditemlaro" />
                            <HeaderStyle CssClass="gridlarohead" Font-Size="Small" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="Sarson_DamagedAndWeevilled" HeaderStyle-HorizontalAlign="Center" 
                            HeaderText="Gdwn_DamagedAndWeevilled" ItemStyle-HorizontalAlign="Center" 
                            SortExpression="Sarson_DamagedAndWeevilled">
                            <ItemStyle CssClass="griditemlaro" />
                            <HeaderStyle CssClass="gridlarohead" Font-Size="Small" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="Sarson_SmallAtrophiedSeeds" HeaderStyle-HorizontalAlign="Center" 
                            HeaderText="Gdwn_SmallAtrophiedSeeds" ItemStyle-HorizontalAlign="Center" 
                            SortExpression="Sarson_SmallAtrophiedSeeds">
                            <ItemStyle CssClass="griditemlaro" />
                             <HeaderStyle CssClass="gridlarohead" Font-Size="Small" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="Sarson_MoistureContent" HeaderStyle-HorizontalAlign="Center" 
                            HeaderText="Gdwn_MoistureContent" ItemStyle-HorizontalAlign="Center" 
                            SortExpression="Sarson_MoistureContent">
                            <ItemStyle CssClass="griditemlaro" />
                             <HeaderStyle CssClass="gridlarohead" Font-Size="Small" />
                        </asp:BoundField>
                                             
                        
                        <asp:BoundField DataField="Srvyr_Nm" HeaderStyle-HorizontalAlign="Center" 
                            HeaderText="Mandi Surveyor" ItemStyle-HorizontalAlign="Center" 
                            SortExpression="Srvyr_Nm">
                              <ItemStyle HorizontalAlign="Center" Width="0px" Font-Size="0px" />
                           <HeaderStyle HorizontalAlign="Center" Width="0px" Font-Size="0px" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="Foreignmatter" HeaderStyle-HorizontalAlign="Center" 
                            HeaderText="Mandi Foreignmatter" ItemStyle-HorizontalAlign="Center" 
                            SortExpression="Foreignmatter">
                            <ItemStyle CssClass="griditemlaro" />
                            <HeaderStyle CssClass="gridlarohead" Font-Size="Small" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="Admixture" HeaderStyle-HorizontalAlign="Center" 
                            HeaderText="Mandi Admixture" ItemStyle-HorizontalAlign="Center" 
                            SortExpression="Admixture">
                            <ItemStyle CssClass="griditemlaro" />
                             <HeaderStyle CssClass="gridlarohead" Font-Size="Small" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="Damagedpuls" HeaderStyle-HorizontalAlign="Center" 
                            HeaderText="Mandi Damagedpuls" ItemStyle-HorizontalAlign="Center" 
                            SortExpression="Damagedpuls">
                            <ItemStyle CssClass="griditemlaro" />
                           <HeaderStyle CssClass="gridlarohead" Font-Size="Small" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="Slightlydamaged" HeaderStyle-HorizontalAlign="Center" 
                            HeaderText="Mandi Slightlydamaged" ItemStyle-HorizontalAlign="Center" 
                            SortExpression="Slightlydamaged">
                            <ItemStyle CssClass="griditemlaro" />
                            <HeaderStyle CssClass="gridlarohead" Font-Size="Small" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="ImmatureShrivelled" HeaderStyle-HorizontalAlign="Center" 
                            HeaderText="Mandi ImmatureShrivelled" ItemStyle-HorizontalAlign="Center" 
                            SortExpression="ImmatureShrivelled">
                            <ItemStyle CssClass="griditemlaro" />
                            <HeaderStyle CssClass="gridlarohead" Font-Size="Small" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="Weevilled" HeaderStyle-HorizontalAlign="Center" 
                            HeaderText="Mandi Weevilled" ItemStyle-HorizontalAlign="Center" 
                            SortExpression="Weevilled">
                            <ItemStyle CssClass="griditemlaro" />
                            <HeaderStyle CssClass="gridlarohead" Font-Size="Small" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="MoistureContent" HeaderStyle-HorizontalAlign="Center" 
                            HeaderText="Mandi MoistureContent" ItemStyle-HorizontalAlign="Center" 
                            SortExpression="MoistureContent">
                            <ItemStyle CssClass="griditemlaro" />
                           <HeaderStyle CssClass="gridlarohead" Font-Size="Small" />
                        </asp:BoundField>
                        
                        <asp:TemplateField HeaderText="20 % जाँच की स्थिति">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddl_status_5" runat="server" Width="100px">
                                        <asp:ListItem Value="N">नहीं </asp:ListItem>
                                        <asp:ListItem Value="Y">हाँ </asp:ListItem>
                                        
                                    </asp:DropDownList>
                                </ItemTemplate>
                         </asp:TemplateField>                         
                             <asp:TemplateField HeaderText="रिजेक्ट की स्थिति">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddl_status_6" runat="server" Width="100px">
                                        
                                        <asp:ListItem Value="N">नहीं </asp:ListItem>
                                        <asp:ListItem Value="Y">हाँ </asp:ListItem>
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>                                              
                       
                    </Columns>
                    <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" HorizontalAlign="Right" />
                    <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                    <SelectedRowStyle BackColor="#738A9C" ForeColor="#333333" />
                    <PagerStyle BackColor="#284775" ForeColor="#8C4510" HorizontalAlign="Center" />
                    <HeaderStyle BackColor="#A55129" ForeColor="White" />
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <EditRowStyle BackColor="#999999" />
                </asp:GridView>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        
        <tr>
        <td>
        </td>
        <td>
        </td>
        </tr>
        <asp:Panel ID="pnltr" runat="server" Visible="false">
        <tr>
        <td colspan="2" align ="left" >
           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnOTP" runat="server" Text="Generate OTP" Height="30px" 
                Width="142px" onclick="btnOTP_Click" />
        </td>
        </tr>
        
        
         <tr id="trbtn" runat="server" visible="False">
        <td colspan="2" align ="left" >
           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
            OTP Is <asp:Label ID="lblotpis" runat="server"></asp:Label>
           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:TextBox ID="txtCheckOTP" runat="server" Height="22px" Width="140px" placeholder="Enter OTP Here"></asp:TextBox>  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Btn_Submit" runat="server" Text="Submit" 
                onclick="Btn_Submit_Click" Height="30px" Width="80px" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btn_close" runat="server" Text="Close" Height="30px" Width="80px" />
        </td>
        </tr>
        <tr>
        <td class="style2"><input id="hdfOTP" type="hidden" runat="server" /> 
        <asp:TextBox ID="txtMobNum" runat="server" Height="22px" Width="140px" Visible="false"></asp:TextBox></td>
        </tr>
        </asp:Panel>
    </table>
</asp:Content>

