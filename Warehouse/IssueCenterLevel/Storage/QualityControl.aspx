<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QualityControl.aspx.cs" Inherits="IssueCenterLevel_QualityControl"  
    MasterPageFile="~/MasterPage/Gdwn.master" Title="Stackwise Quality Control Page"%>
    <%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

      
<script type="text/javascript">
		
        

//percentage
function validate() {
  // Percent = document.frmPost.percent.value
  
  if ((document.ctl00_ContentPlaceHolder1_txtmoisture.value.indexOf(".") == -1) && (document.ctl00_ContentPlaceHolder1_txtmoisture.value.length >= 3)) {
    alert("Percentage format is not correct");
    document.ctl00_ContentPlaceHolder1_txtmoisture.value = "";
    document.ctl00_ContentPlaceHolder1_txtmoisture.focus();
    return false;
  }
  if ((document.ctl00_ContentPlaceHolder1_txtmoisture.value.indexOf(".")) == 4 || (document.ctl00_ContentPlaceHolder1_txtmoisture.value.indexOf(".")) == 3 || (document.ctl00_ContentPlaceHolder1_txtmoisture.value.indexOf(".")) == 0) {
    alert("Invalid Percentage");
     document.ctl00_ContentPlaceHolder1_txtmoisture.value = "";
    document.ctl00_ContentPlaceHolder1_txtmoisture.focus();
    return false;
  }
  if (isNaN(document.ctl00_ContentPlaceHolder1_txtmoisture.value)==true) {
    alert("Enter Numeric values");
     document.ctl00_ContentPlaceHolder1_txtmoisture.value = "";
    document.ctl00_ContentPlaceHolder1_txtmoisture.focus();
    return false;
  }	
  return true;
}	

function validate1() {
  // Percent = document.frmPost.percent.value
  
  if ((document.ctl00_ContentPlaceHolder1_txtbordamage.value.indexOf(".") == -1) && (document.ctl00_ContentPlaceHolder1_txtbordamage.value.length >= 3)) {
    alert("Percentage format is not correct");
    document.ctl00_ContentPlaceHolder1_txtbordamage.value = "";
    document.ctl00_ContentPlaceHolder1_txtbordamage.focus();
    return false;
  }
  if ((document.ctl00_ContentPlaceHolder1_txtbordamage.value.indexOf(".")) == 4 || (document.ctl00_ContentPlaceHolder1_txtbordamage.value.indexOf(".")) == 3 || (document.ctl00_ContentPlaceHolder1_txtbordamage.value.indexOf(".")) == 0) {
    alert("Invalid Percentage2");
     document.ctl00_ContentPlaceHolder1_txtbordamage.value = "";
    document.ctl00_ContentPlaceHolder1_txtbordamage.focus();
    return false;
  }
  if (isNaN(document.ctl00_ContentPlaceHolder1_txtbordamage.value)==true) {
    alert("Enter Numeric values");
     document.ctl00_ContentPlaceHolder1_txtbordamage.value = "";
    document.ctl00_ContentPlaceHolder1_txtbordamage.focus();
    return false;
  }	
  return true;
}	


function validate2() {
  // Percent = document.frmPost.percent.value
  
  if ((document.ctl00_ContentPlaceHolder1_Txtchalky.value.indexOf(".") == -1) && (document.ctl00_ContentPlaceHolder1_Txtchalky.value.length >= 3)) {
    alert("Percentage format is not correct");
    document.ctl00_ContentPlaceHolder1_Txtchalky.value = "";
    document.ctl00_ContentPlaceHolder1_Txtchalky.focus();
    return false;
  }
  if ((document.ctl00_ContentPlaceHolder1_Txtchalky.value.indexOf(".")) == 4 || (document.ctl00_ContentPlaceHolder1_Txtchalky.value.indexOf(".")) == 3 || (document.ctl00_ContentPlaceHolder1_Txtchalky.value.indexOf(".")) == 0) {
    alert("Invalid Percentage2");
     document.ctl00_ContentPlaceHolder1_Txtchalky.value = "";
    document.ctl00_ContentPlaceHolder1_Txtchalky.focus();
    return false;
  }
  if (isNaN(document.ctl00_ContentPlaceHolder1_Txtchalky.value)==true) {
    alert("Enter Numeric values");
     document.ctl00_ContentPlaceHolder1_Txtchalky.value = "";
    document.ctl00_ContentPlaceHolder1_Txtchalky.focus();
    return false;
  }	
  return true;
}	


function validate3() {
  // Percent = document.frmPost.percent.value
  
  if ((document.ctl00_ContentPlaceHolder1_txtgerdiscolored.value.indexOf(".") == -1) && (document.ctl00_ContentPlaceHolder1_txtgerdiscolored.value.length >= 3)) {
    alert("Percentage format is not correct");
    document.ctl00_ContentPlaceHolder1_txtgerdiscolored.value = "";
    document.ctl00_ContentPlaceHolder1_txtgerdiscolored.focus();
    return false;
  }
  if ((document.ctl00_ContentPlaceHolder1_txtgerdiscolored.value.indexOf(".")) == 4 || (document.ctl00_ContentPlaceHolder1_txtgerdiscolored.value.indexOf(".")) == 3 || (document.ctl00_ContentPlaceHolder1_txtgerdiscolored.value.indexOf(".")) == 0) {
    alert("Invalid Percentage2");
     document.ctl00_ContentPlaceHolder1_txtgerdiscolored.value = "";
    document.ctl00_ContentPlaceHolder1_txtgerdiscolored.focus();
    return false;
  }
  if (isNaN(document.ctl00_ContentPlaceHolder1_txtgerdiscolored.value)==true) {
    alert("Enter Numeric values");
     document.ctl00_ContentPlaceHolder1_txtgerdiscolored.value = "";
    document.ctl00_ContentPlaceHolder1_txtgerdiscolored.focus();
    return false;
  }	
  return true;
}	
function popMe(url)
  {
    var newWindow;
    newWindow=window.open(url,'MyWin','width=275,height=390,top=1,left=1');

  }
</script>
 <fieldset style="width:750px ; border: 2px solid navy; ">
                                
    <div>
        <table style="width:600px; border-collapse: collapse; ">
<%--           <tr class="HeadingBlue">
                <td colspan="4" style="border-collapse: collapse ;
                    height: 15px; text-align: center; background-color: dimgray">
                </td>
            </tr>--%>
            
            <tr>
                <td style="text-align: center;border-collapse: collapse ;background-color: #0bb6e6; height: 27px; " colspan="6"  >
                    <span style="font-size: 10pt; "><strong><asp:Label ID="lblFortQulRept" ForeColor="whitesmoke"
                        runat="server" Text="Stack Wise Fortnightly Quality Control Report"></asp:Label>
                        </strong></span>
                </td>
            </tr>
            <tr>
                <td colspan="6" style="border-collapse: collapse ; width: 800px; text-align: center; " class="mandatory">
                    <asp:Label ID="lblmsg" runat="server" Font-Size="10px" ForeColor="Red"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="6" style="border-collapse: collapse ;
                    border-collapse: collapse ; text-align: justify; width: 800px;">
                    <span style="font-family: Times New Roman"><span style="">
                        <span style="color: #ff0000"><span style=" font-size: 10pt"><span style="color: #cc0000;"><span style="color: #660033"></span></span><span style="color: #003300"><span style="font-family: Verdana">
                           
<a href="javascript:popMe('../../SampleQuantity.htm');" style="text-decoration: underline">&nbsp&nbsp(Qty. in Qtls.kgsgms)</a> <asp:Label ID="lblInstruction" runat="server" Text="* Mandatory Fields" Font-Bold="True"></asp:Label></span></span></span></span></span></span></td>
            </tr>
            <tr><td style= "width:80px; height: 5px"></td></tr>
            <tr>
                <td style="border-collapse: collapse ; width:75px; height: 26px ">
                    <span style=" font-size: 10pt">&nbsp&nbsp
                        <asp:Label ID="lblGodownNo" runat="server" Text="Godown Number"></asp:Label></span></td>
                <td style="border-collapse: collapse ; font-size:9px ">
                    <asp:DropDownList ID="ddlgodownlist" CssClass="dropdownBig" runat="server" Width="160px" Height="25px" AutoPostBack="True"  OnSelectedIndexChanged="ddlgodownlist_SelectedIndexChanged" TabIndex="1">
                    </asp:DropDownList>
                    <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="ddlgodownlist"
                        ErrorMessage="Godown" OnServerValidate="CustomValidator1_ServerValidate" ValidationGroup="SaveValid"></asp:CustomValidator></td>
                <td style="border-collapse: collapse ;">
                    <span style=" font-size: 10pt">
                        <asp:Label ID="lblStackNo" runat="server" Text="Stack Number"></asp:Label></span></td>
                <td style="border-collapse: collapse ;">
                  
                    <asp:DropDownList ID="ddlstacklist" runat="server" Width="160px" Height="25px" AutoPostBack="True"  OnSelectedIndexChanged="ddlstacklist_SelectedIndexChanged" TabIndex="2" CssClass="dropdownSml" >
                    
                    </asp:DropDownList>
                            <asp:CustomValidator ID="CustomValidator2" runat="server" ControlToValidate="ddlstacklist"
                                ErrorMessage="Stack" OnServerValidate="CustomValidator2_ServerValidate" ValidationGroup="SaveValid"></asp:CustomValidator>
                       
                </td>
            </tr>
<%--            <tr><td style=" width: 80px; height:5px"></td> </tr>
            <tr>
                <td colspan="6" style="border-collapse: collapse ; text-align: left; width: 800px; height:25px"; visible="false">
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    <asp:Button ID="btnsearch" runat="server" Font-Size="X-Small" Text="Search" OnClick="btnsearch_Click" CausesValidation="False" ForeColor="Indigo" BackColor="Control" Font-Bold="True" Width="89px" Height="23px"  TabIndex="3" ValidationGroup="SaveValid" /><span
                        style=" font-size: 10pt; color: #cc0066"><strong><asp:Label ID="lblRetrieveStackInfo" runat="server"
                            Font-Size="X-Small" ForeColor="Red" Style="position: static">(*Click to reterive stack information)</asp:Label></strong></span></td>
            </tr>--%>
           <tr>
                    <td style=" width: 80px; height:10px"></td></tr>

            <tr><td style=" width: 60px; height:8px"></td>
                 <td colspan="6" style="border-collapse: collapse ;text-align: center; width:400px;">
                    <asp:GridView ID="gdstackdetail" runat="server" BackColor="White" BorderColor="#CC9966"
                        BorderStyle="None" BorderWidth="1px" CellPadding="4" OnPreRender="gdstackdetail_PreRender" OnRowCreated="gdstackdetail_RowCreated">
                        <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                        <RowStyle BackColor="White" ForeColor="#330099" />
                        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                        <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                    </asp:GridView>
                  
                </td>
            </tr>
           <tr> <td style=" width: 80px; height:8px"></td></tr>
            <tr>
                <td class="mandatory" colspan="1" style="border-collapse: collapse ; text-align: left; width: 158px;">
                    <span style=" font-size: 10pt">&nbsp&nbsp
                        <asp:Label ID="lblDOinspect" runat="server" Text="Date of Inspection"></asp:Label></span></td>
                <td colspan="6" class="mandatory"  style="border-collapse: collapse ;  text-align: left; width: 800px;">
                    <asp:TextBox ID="txtinsdate" onblur= "Spc_validatordate(this)" runat="server" Width="150px" Height="20px" ReadOnly="True"  MaxLength="10" Font-Size="10px" TabIndex="4" CssClass="txtFldSmall"></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server"  Format="dd/MM/yyyy"
        TargetControlID="txtinsdate"></cc1:CalendarExtender>
                   <%-- <a onclick="ShowCalendar(ctl00_ContentPlaceHolder1_txtinsdate, ctl00_ContentPlaceHolder1_txtinsdate);"
																href="javascript:;"><IMG height="16" alt="Click Here to Pick up the date" src="../../images/cal.gif" width="16"
																	border="0" id="IMG1" onclick="return IMG1_onclick()"></a> <span style="color: #cc0000">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtinsdate"
                        Display="Dynamic" ErrorMessage="Inspection Date Field cannot be empty" SetFocusOnError="True">></asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtinsdate"
                        Display="Dynamic" ErrorMessage="Inspection Date is not valid" SetFocusOnError="True"
                        ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$">></asp:RegularExpressionValidator>--%></td>
            
            </tr>
          <tr>  <td style=" height:5px"></td></tr>
            <tr>
                <td colspan="6" style="border-collapse: collapse ; text-align: center; width: 800px; height: 25px ; border-top: navy 1px solid ">
                    <strong><span style="font-size: 10pt; color: activecaption">
                        <asp:Label ID="lblClassOfStock" runat="server" Text="Classification  of Stock" Font-Bold="True" ForeColor="Navy"></asp:Label>
                        <asp:Label ID="lblQltsKgsgms" runat="server" ForeColor="Navy" Text="(Qtls.Kgsgms)" Font-Bold="True"></asp:Label></span></strong></td>
            </tr>
            <tr>  <td style=" width: 50px; height:3px"></td></tr>
            <tr>
                <td colspan="6" style="width: 800px; " >
                    <table style="width: 800px ; border-collapse: collapse ;">
                        <tr>
                            <td style="border-collapse: collapse ; width: 75px; ">
                                <span style=" font-size: 10pt">&nbsp&nbsp
                                    <asp:Label ID="lblClear" runat="server" Text="Clear "></asp:Label></span></td>
                            <td style="border-collapse: collapse ; width: 150px;">
                                <asp:TextBox ID="txtclear"  onkeyup= "NumericDecimalCheck(this,3)"  runat="server" Width="125px" MaxLength="19" TabIndex="5" CssClass="txtFldSmall">0</asp:TextBox>
                                <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txtclear"
                                    Display="Dynamic" ErrorMessage="This character not allowed" MinimumValue="0"
                                    SetFocusOnError="True" Type="Double">></asp:RangeValidator></td>
                            <td style="border-collapse: collapse ; width: 85px;">
                                <span style=" font-size: 10pt">&nbsp&nbsp&nbsp
                                    <asp:Label ID="lblFew" runat="server" Text="Few"></asp:Label></span></td>
                            <td style="border-collapse: collapse ; width: 181px;">
                                <asp:TextBox ID="txtfew"  onkeyup= "NumericDecimalCheck(this,3)"  runat="server" Width="125px" MaxLength="19" TabIndex="6" CssClass="txtFldSmall">0</asp:TextBox>
                                <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="txtfew"
                                    Display="Dynamic" ErrorMessage="This character not allowed" MinimumValue="0"
                                    SetFocusOnError="True" Type="Double">></asp:RangeValidator></td>
                            <td style="border-collapse: collapse ; width: 133px;">
                                <span style=" font-size: 10pt">
                                    <asp:Label ID="lblHeavy" runat="server" Text="Heavy"></asp:Label></span></td>
                            <td style="border-collapse: collapse ; ">
                                <asp:TextBox ID="txtheavy" onkeyup= "NumericDecimalCheck(this,3)" runat="server" Width="125px" MaxLength="19" OnTextChanged="txtheavy_TextChanged" TabIndex="7" CssClass="txtFldSmall">0</asp:TextBox>
                                <asp:RangeValidator ID="RangeValidator4" runat="server" ControlToValidate="txtheavy"
                                    Display="Dynamic" ErrorMessage="This character not allowed" MinimumValue="0"
                                    SetFocusOnError="True" Type="Double">></asp:RangeValidator></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>  <td style=" width: 80px; height:5px"></td></tr>
            
            <tr>
                <td colspan="6" style="border-collapse: collapse ; text-align: center; height: 25px ;border-top: navy 1px solid ; width:800;">
                    <strong><span style="font-size: 10pt; color: activecaption">
                        <asp:Label ID="lblPercentOfWeevilledGrains" runat="server" Style="position: static"
                            Text="Percentage of Weevilled  Grains" Width="345px" Font-Bold="True" ForeColor="Navy"></asp:Label></span></strong></td>
            </tr>
            <tr>  <td style=" width: 80px; height:5px"></td></tr>
            <tr>
                <td colspan="8" style=" width: 806px;">
                    <table  style="width: 800px ;border-collapse: collapse ">
                        <tr>
                            <td style="border-collapse: collapse ; width: 140px;">
                                <span style=" font-size: 10pt">&nbsp&nbsp
                                    <asp:Label ID="lblBoredDamage" runat="server" Text="Bored/Damage"></asp:Label></span></td>
                            <td style="border-collapse: collapse ; width: 150px;">
                                <asp:TextBox ID="txtbordamage" onblur= "validate1() " runat="server" Width="120px" MaxLength="2" TabIndex="8" CssClass="txtFldSmall">0</asp:TextBox>
                                <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtbordamage"
                                    Display="Dynamic" ErrorMessage="This character not allowed" MinimumValue="0"
                                    SetFocusOnError="True" Type="Double">></asp:RangeValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtbordamage"
                                    Display="Dynamic" ErrorMessage="Not a valid entry in Bored/Damage" SetFocusOnError="True"
                                    ValidationExpression="^((100)|(\d{0,2}))$">></asp:RegularExpressionValidator></td>
                                   
                                <td style="border-collapse: collapse ; width: 111px;">
                                <span style=" font-size: 10pt">
                                    <asp:Label ID="lblChalky" runat="server" Text="Chalky"></asp:Label></span></td>
                            <td style="border-collapse: collapse ; width: 210px;">
                                <asp:TextBox ID="Txtchalky" onblur= "validate2()" runat="server" Width="120px" MaxLength="2" TabIndex="9" CssClass="txtFldSmall">0</asp:TextBox>
                                <asp:RangeValidator ID="RangeValidator10" runat="server" ControlToValidate="txtbordamage"
                                    Display="Dynamic" ErrorMessage="This character not allowed" MinimumValue="0"
                                    SetFocusOnError="True" Type="Double">></asp:RangeValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtbordamage"
                                    Display="Dynamic" ErrorMessage="Not a valid entry in Bored/Damage" SetFocusOnError="True"
                                    ValidationExpression="^((100)|(\d{0,2}))$">></asp:RegularExpressionValidator></td>
                                   
                                
                            <td style="border-collapse: collapse ;  width: 137px;">
                                <span style=" font-size: 10pt">
                                    <asp:Label ID="lblGermiDiscolor" runat="server" Text="Discolored"></asp:Label></span></td>
                            <td style="border-collapse: collapse ; width: 154px;">
                                <asp:TextBox ID="txtgerdiscolored"  onblur= "validate3()" runat="server" Width="115px" MaxLength="2" TabIndex="10" CssClass="txtFldSmall">0</asp:TextBox>
                                <asp:RangeValidator ID="RangeValidator5" runat="server" ControlToValidate="txtgerdiscolored"
                                    Display="Dynamic" ErrorMessage="This character not allowed" MinimumValue="0"
                                    SetFocusOnError="True" Type="Double">></asp:RangeValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtgerdiscolored"
                                    Display="Dynamic" ErrorMessage="Not a valid entry in Germinated/Discolored field"
                                    SetFocusOnError="True" ValidationExpression="^((100)|(\d{0,2}))$">></asp:RegularExpressionValidator></td>
                        </tr>
                    </table>
                </td>
         
            </tr>
            <tr>  <td style=" width: 80px; height:5px"></td></tr>
          
<%--        </table>
        
    
    
        <table  style="width:808px; border-collapse: collapse  ;margin-left : 20px; border-width: navy 1px solid ">--%>
             
            <tr>
                <td colspan="6" style="text-align: center;border-collapse: collapse ; height: 25px ; border-top: navy 1px solid">
                    <strong><span style="font-size: 10pt; color: activecaption">
                        <asp:Label ID="lblCatgryOfStock" runat="server" Text="Quantity Details on the basis of Category of Stock" Width="493px" Font-Bold="True" ForeColor="Navy"></asp:Label></span></strong></td>
            </tr>
             <tr><td class="style12" ></td></tr>
            <tr>
                <td style="border-collapse: collapse; text-align: left; " class="style13">
                    <span style=" font-size: 10pt">&nbsp&nbsp&nbsp
                        <asp:Label ID="lblCategoryA" runat="server" Text="Category  A"></asp:Label></span></td>
                <td style="border-collapse: collapse ; text-align: left;" class="style9">
                    <asp:TextBox ID="txtcatA" onkeyup= "NumericDecimalCheck(this,3)"  runat="server" Width="120px" MaxLength="19" TabIndex="11" CssClass="txtFldSmall">0</asp:TextBox>&nbsp;<asp:RangeValidator
                        ID="RangeValidator6" runat="server" ControlToValidate="txtcatA" Display="Dynamic"
                        ErrorMessage="This character not allowed" MinimumValue="0" SetFocusOnError="True"
                        Type="Double">></asp:RangeValidator></td>
                <td style="border-collapse: collapse; text-align : left; " class="style10">
                    <span style=" font-size: 10pt">&nbsp&nbsp&nbsp&nbsp;<asp:Label ID="lblCategoryB" runat="server" Text="Category B"></asp:Label></span></td>
                <td style="border-collapse: collapse ; text-align: left; width: 200px;">
                    <asp:TextBox ID="txtcatB" onkeyup= "NumericDecimalCheck(this,3)" runat="server" Width="120px" MaxLength="19" TabIndex="12" CssClass="txtFldSmall">0</asp:TextBox>&nbsp;<asp:RangeValidator
                        ID="RangeValidator7" runat="server" ControlToValidate="txtcatB" Display="Dynamic"
                        ErrorMessage="This character not allowed" MinimumValue="0" SetFocusOnError="True"
                        Type="Double">></asp:RangeValidator></td>
            </tr>
            <tr><td class="style14" ></td></tr>
            <tr>
                <td style="border-collapse: collapse; text-align: left; " class="style5">
                    <span style=" font-size: 10pt">&nbsp&nbsp&nbsp
                        <asp:Label ID="lblCategoryC" runat="server" Text="Category C"></asp:Label></span></td>
                <td style="border-collapse: collapse; text-align: left; " class="style6">
                    <asp:TextBox ID="txtcatC" onkeyup= "NumericDecimalCheck(this,3)"  runat="server" Width="120px" MaxLength="19" TabIndex="13" CssClass="txtFldSmall">0</asp:TextBox>
                    <asp:RangeValidator ID="RangeValidator8" runat="server" ControlToValidate="txtcatC"
                        Display="Dynamic" ErrorMessage="This character not allowed" MinimumValue="0"
                        SetFocusOnError="True" Type="Double">></asp:RangeValidator></td>
                <td style="border-collapse: collapse; text-align: left; " class="style7">
                    <span style=" font-size: 10pt">&nbsp&nbsp&nbsp
                        <asp:Label ID="lblCategoryD" runat="server" Text="Category D"></asp:Label></span></td>
                <td style="border-collapse: collapse; text-align: left; " class="style8">
                    <asp:TextBox ID="txtcatD" onkeyup= "NumericDecimalCheck(this,3)"  runat="server" Width="120px" MaxLength="19" TabIndex="14" CssClass="txtFldSmall">0</asp:TextBox>
                    <asp:RangeValidator ID="RangeValidator9" runat="server" ControlToValidate="txtcatD"
                        Display="Dynamic" ErrorMessage="This character not allowed" MinimumValue="0"
                        SetFocusOnError="True" Type="Double">></asp:RangeValidator></td>
            </tr>
            <tr><td class="style14" ></td></tr>
            <tr>
                <td style="border-collapse: collapse; " class="style13">
                    <span style=" font-size: 10pt">&nbsp&nbsp&nbsp
                        <asp:Label ID="lblMoisture" runat="server" Text="Moisture Content(%) "></asp:Label></span></td>
                <td style="border-collapse: collapse ;" class="style9">
                    <asp:TextBox ID="txtmoisture" onblur="validate()" runat="server" Width="120px" MaxLength="6" TabIndex="15" CssClass="txtFldSmall">0</asp:TextBox><span
                        style="color: #cc0000"></span>
                    </td>
                <td style="border-collapse: collapse; " class="style10">
                    <span style=" font-size: 10pt">&nbsp&nbsp&nbsp
                        <asp:Label ID="lblNatureofInfest" runat="server" Text="Nature of Infestation"></asp:Label></span></td>
                <td style=" border-collapse: collapse ; width: 184px;">
                    <asp:TextBox ID="txtinfestation" onblur= "Spc_charactermultiline(this)" 
                        runat="server" Width="175px" MaxLength="50" TabIndex="16" 
                        CssClass="txtFldSmall"></asp:TextBox></td>
            </tr>
            <tr><td class="style14" ></td></tr>
            <tr >
                <td style="border-collapse: collapse; " class="style2">
                    <span style=" font-size: 9pt">&nbsp&nbsp&nbsp
                        <asp:Label ID="lbltreatRecomd" runat="server" Text="Treatment Recommended"></asp:Label></span></td>
                <td style="border-collapse: collapse;" class="style3">
                    <asp:TextBox ID="txtrecdata" onKeyUp="textCounter(this.form.txtrecdata,50);"  
                        onblur= "Spc_charactermultiline(this)" runat="server" TextMode="MultiLine" 
                        Width="160px"  MaxLength="50" TabIndex="17" CssClass="txtFldSmall" 
                        Height="62px"></asp:TextBox></td>
                <td style="border-collapse: collapse; " class="style11">
                    <span style=" font-size: 10pt">&nbsp&nbsp&nbsp
                        <asp:Label ID="lblRemark" runat="server" Text="Remark"></asp:Label></span></td>
                <td style="border-collapse: collapse; " class="style4">
                    <asp:TextBox ID="txtremark" onKeyUp="textCounter(this.form.txtremark,50);" runat="server" onblur= "Spc_charactermultiline(this)" TextMode="MultiLine" Width="175px" MaxLength="50" TabIndex="18" CssClass="txtFldSmall" Height="60px"></asp:TextBox></td>
            </tr>
            <tr><td class="style15" ></td></tr>
            <tr>
            

                <td colspan="6" 
                    style="text-align: center;border-collapse: collapse ; height: 25px;border-top: navy 1px solid; ">
                    <strong><span style="font-size: 10pt; color: activecaption">
                        <asp:Label ID="lblProgress" runat="server" Text="Progress of Disinfestation Work" Width="356px" Font-Bold="True" ForeColor="Navy"></asp:Label></span></strong></td>
            </tr>
            <tr>
                <td colspan="4" style=" border-collapse: collapse; height:25px;
                     text-align: center">
                    <span style=" font-size: 10pt; color: #990066"><strong>
                        <asp:Label ID="lblSprayedDetail" runat="server" Text="Sprayed Details" Width="234px" Height="20px" ForeColor="#400040"></asp:Label>
                    </strong></span>
                </td>
            </tr>
            <tr>
                <td style="border: 1px solid white; border-collapse: collapse; text-align: left; "
                    valign="top" class="style13">
                    <span style=" font-size: 10pt">&nbsp&nbsp&nbsp
                        <asp:Label ID="lblTreatType" runat="server" Text="Type of Treatment"></asp:Label></span></td>
                <td style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid;
                    border-bottom: white 1px solid; border-collapse: collapse; text-align: left; "
                    valign="top" class="style9">
                    <asp:TextBox ID="txtsprayedtreatment" runat="server" Width="160px" 
                        MaxLength="19" TabIndex="19" CssClass="txtFldSmall" 
                        >N</asp:TextBox>
               </td>
                <td style="border: 1px solid white; border-collapse: collapse; text-align: left"
                    valign="top" class="style10">
                    &nbsp;<asp:Label ID="lbltreatDate" runat="server" Text="Date of Treatment"></asp:Label><span style=" font-size: 10pt"></span></td>
                <td style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid;
                    border-bottom: white 1px solid; border-collapse: collapse; text-align: left; width: 204px; "
                    valign="top">
                    <asp:TextBox ID="txtspraydate" onblur= "Spc_validatordate(this)" runat="server" Width="120px" MaxLength="10" Font-Size="10px" TabIndex="20" CssClass="txtFldSmall" ></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server"  Format="dd/MM/yyyy"
        TargetControlID="txtspraydate"></cc1:CalendarExtender>
                    <%--<a onclick="ShowCalendar(ctl00_ContentPlaceHolder1_txtspraydate, ctl00_ContentPlaceHolder1_txtspraydate);"
																href="javascript:;"><IMG height="16" alt="Click Here to Pick up the date" src="../../images/cal.gif" width="16"
																	border="0" id="IMG2" onclick="return IMG2_onclick()"></a>&nbsp;--%>
                    </td>
            </tr>
            <tr>
                <td  valign ="top" 
                    style="border-collapse: collapse; text-align: left;  border-color: White; border-bottom: white 1px solid ;" 
                    class="style13">
                    <span style=" font-size: 10pt">&nbsp&nbsp&nbsp
                        <asp:Label ID="lblNameChemicalUsed" runat="server" Text="Name of Chemical Used"></asp:Label></span></td>
                <td valign ="top" style="border-collapse: collapse ; text-align: left;  border-bottom: white 1px solid; " colspan="3">
                   
                    <asp:TextBox ID="txtsprayedchemical"   runat="server" Width="160px" MaxLength="30" TabIndex="21" CssClass="txtFldSmall" >N</asp:TextBox>
                    &nbsp;</td>
            </tr>
            <tr>  <td style=" width: 50px; height:5px"></td></tr>
            <tr>
                <td colspan="4" style=" border-collapse: collapse; height:25px;
                    text-align: center" valign="top">
                    <span style=" font-size: 10pt; color: #990066"><strong>
                        <asp:Label ID="lblFumigatedDetails" runat="server" Text="Fumigated Details" Width="223px" height="23px" ForeColor="#400040"></asp:Label>
                    </strong></span>
                </td>
            </tr>
             <tr>  <td style=" width:80px; height:3px"></td></tr>
            <tr>
                <td style="border: 1px solid white; border-collapse: collapse; text-align: left ; "
                    valign="top" class="style13">
                    <span style=" font-size: 10pt">&nbsp&nbsp&nbsp
                        <asp:Label ID="lblTypeOfTreatment" runat="server" Text="Type of Treatment"></asp:Label></span></td>
                <td style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid;
                    border-bottom: white 1px solid; border-collapse: collapse; text-align: left"
                    valign="top" class="style9">
                    <asp:TextBox ID="txtfumitreatment" runat="server" MaxLength="19"  Width="160px" TabIndex="22" CssClass="txtFldSmall">N</asp:TextBox></td>
                <td style="border: 1px solid white; border-collapse: collapse; text-align: left"
                    valign="top" class="style10">
                    <span style=" font-size: 10pt">&nbsp;<asp:Label ID="lblDateOfTreatment" runat="server" Text="Date of Treatment"></asp:Label></span></td>
                <td style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid;
                    border-bottom: white 1px solid; border-collapse: collapse; text-align: left; width: 204px;"
                    valign="top">
                    <asp:TextBox ID="txtfungdate" onblur= "Spc_validatordate(this)" runat="server" Width="120px" MaxLength="10" Font-Size="10px" TabIndex="23" CssClass="txtFldSmall"></asp:TextBox>
                     <cc1:CalendarExtender ID="CalendarExtender3" runat="server"  Format="dd/MM/yyyy"
        TargetControlID="txtfungdate"></cc1:CalendarExtender>
                    <%--<a onclick="ShowCalendar(ctl00_ContentPlaceHolder1_txtfungdate, ctl00_ContentPlaceHolder1_txtfungdate);"
																href="javascript:;"><IMG height="16" alt="Click Here to Pick up the date" src="../../images/cal.gif" width="16"
																	border="0"></a>--%>
                    </td>
            </tr>
            <tr>
                <td style="border: 1px solid white; border-collapse: collapse; text-align: left ; "
                    valign="top" class="style13">
                    <span style=" font-size: 10pt">&nbsp&nbsp&nbsp
                        <asp:Label ID="lblChemicalName" runat="server" Text="Name of Chemical Used"></asp:Label></span></td>
                <td colspan="3" style="border-right: white 1px solid; border-top: white 1px solid;
                    border-left: white 1px solid; border-bottom: white 1px solid; border-collapse: collapse;
                    text-align: left" valign="top">
                    <asp:TextBox ID="txtfumichemical" runat="server" MaxLength="30"  Width="160px" TabIndex="24" CssClass="txtFldSmall">N</asp:TextBox></td>
            </tr>
            <tr>  <td style=" width: 80px; height:10px"></td></tr>
            <tr>
                <td colspan="4" style="border-collapse: collapse ; text-align: center ; height:40px ; ">
                    &nbsp;<asp:Button ID="btnsave"  Text="Submit" runat="server" ImageUrl="~/images/SUBMIT.GIF"
                        OnClick="btnsave_Click" TabIndex="25" Enabled="False" 
                        ValidationGroup="SaveValid" Width="102px" Height="30px"/>
                </td>
            </tr>
<%--            <tr class="HeadingBlue">
                <td colspan="4" style="border-collapse: collapse ;
                    height: 15px; text-align: center; background-color: dimgray">
                </td>
            </tr>--%>
          
        </table>
      </div>
      
      </fieldset>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
            ShowSummary="False" />
        <br />
      
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="head">

    <script language="JavaScript1.2">
var message="MPWLC STORAGE MODULE"
var neonbasecolor="gray"
var neontextcolor="yellow"
var flashspeed=100  //in milliseconds

///No need to edit below this line/////

var n=0
if (document.all||document.getElementById){
document.write('<font color="'+neonbasecolor+'">')
for (m=0;m<message.length;m++)
document.write('<span id="neonlight'+m+'">'+message.charAt(m)+'</span>')
document.write('</font>')
}
else
document.write(message)

function crossref(number){
var crossobj=document.all? eval("document.all.neonlight"+number) : document.getElementById("neonlight"+number)
return crossobj
}

function neon(){

//Change all letters to base color
if (n==0){
for (m=0;m<message.length;m++)
//eval("document.all.neonlight"+m).style.color=neonbasecolor
crossref(m).style.color=neonbasecolor
}

//cycle through and change individual letters to neon color
crossref(n).style.color=neontextcolor

if (n<message.length-1)
n++
else{
n=0
clearInterval(flashing)
setTimeout("beginneon()",1500)
return
}
}

function beginneon(){
if (document.all||document.getElementById)
flashing=setInterval("neon()",flashspeed)
}
beginneon()
</script>
    <style type="text/css">
        .style2
        {
            width: 186px;
            height: 46px;
        }
        .style3
        {
            height: 46px;
            width: 180px;
        }
        .style4
        {
            width: 204px;
            height: 46px;
        }
        .style5
        {
            width: 186px;
            height: 18px;
        }
        .style6
        {
            height: 18px;
            width: 180px;
        }
        .style7
        {
            width: 196px;
            height: 18px;
        }
        .style8
        {
            width: 204px;
            height: 18px;
        }
        .style9
        {
            width: 180px;
        }
        .style10
        {
            width: 196px;
        }
        .style11
        {
            width: 196px;
            height: 46px;
        }
        .style12
        {
            height: 10px;
            width: 186px;
        }
        .style13
        {
            width: 186px;
        }
        .style14
        {
            height: 2px;
            width: 186px;
        }
        .style15
        {
            height: 20px;
            width: 186px;
        }
        .style16
        {
            height: 5px;
            width: 186px;
        }
    </style>

</asp:Content>
