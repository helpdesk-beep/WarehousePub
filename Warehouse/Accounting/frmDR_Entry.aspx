<%@ Page Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="frmDR_Entry.aspx.cs" Inherits="Depot_frmDR_Entry" Title="Godown Reservation Entry" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">  

    <script type="text/javascript">
function CheckCalDate(tx)
{
var AsciiCode = event.keyCode;
var txt=tx.value;
var txt2 = String.fromCharCode(AsciiCode);
var txt3=txt2*1;
if ((AsciiCode > 0))
{
alert('Please Click on Calander Controll to Enter Date');
event.cancelBubble = true;
event.returnValue = false;
}
}
</script>
<fieldset style="width: 1000px; border: 2px solid navy;">
        <center>
<div>
<table style="width:90%;">
<tr> 

<td align="center">


<table style="width: 100%; height: 815px">
       <tr style="background-color: #0bb6e6; height: 25px">
        <td colspan="4" align="center" class="style2">
            <asp:Label ID="Label37" runat="server" Font-Bold="True" Font-Size="15pt" ForeColor="WhiteSmoke"
                                        Text="Reservation Detail"></asp:Label></td>
    </tr>

    <tr>
        <td>
            <asp:Label ID="Label3" runat="server" Text="Receipt Number"></asp:Label></td>
        <td>
            <asp:TextBox ID="txtrecdno" runat="server"></asp:TextBox>
             <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtrecdno"
                                        ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtrecdno" InitialValue="getdate()"
                ErrorMessage=" Receipt Number Required" ValidationGroup="1">*</asp:RequiredFieldValidator></td>
        <td>
            <asp:Label ID="Label4" runat="server" Text="Entry Date"></asp:Label></td>
        <td>
        <asp:TextBox ID="txtentrydt" runat="server" Width="133px"></asp:TextBox>
         <cc1:CalendarExtender ID="txtdate_CalendarExtender" runat="server" Format="dd/MM/yyyy" 
        TargetControlID="txtentrydt" Enabled="True"></cc1:CalendarExtender> 
        
        </td>
    </tr>
<tr>
<td>
    <asp:Label ID="Label5" runat="server" Text="Type Of Depositor"></asp:Label></td>
<td> 
    <asp:DropDownList ID="ddldepositor" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddldepositor_SelectedIndexChanged" Width="178px">
    </asp:DropDownList></td>
<td> 
    <asp:Label ID="Label6" runat="server" Text="Name of Depositor"></asp:Label></td>
    <td>
        <asp:DropDownList ID="ddldepos_name" runat="server" OnSelectedIndexChanged="ddldepos_name_SelectedIndexChanged"
            Width="190px" AutoPostBack="True">
        </asp:DropDownList></td>
</tr>
    <tr>
        <td
            class="style1">
            <asp:Label ID="Label7" runat="server" Text="Address"></asp:Label></td>
        <td colspan="3" 
           
            class="style1">
            <asp:TextBox ID="txtaddress" runat="server" Width="508px" TextMode="MultiLine" OnTextChanged="txtaddress_TextChanged"></asp:TextBox></td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label12" runat="server" Text="Date from When Space Required" Width="133px"></asp:Label></td>
        <td>
            <asp:TextBox ID="txtfdate" runat="server"></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender1" runat="server"  Format="dd/MM/yyyy"
        TargetControlID="txtfdate"></cc1:CalendarExtender>
            </td>
        <td>
            <asp:Label ID="Label13" runat="server" Text="Reserved Till"></asp:Label></td>
        <td>
        <asp:TextBox ID="txttodate" runat="server"></asp:TextBox>
         <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
        TargetControlID="txttodate"></cc1:CalendarExtender>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblcateg" runat="server" Text="Depositor Category"></asp:Label></td>
        <td>
            <asp:DropDownList ID="ddlcategory" runat="server" Width="109px" Enabled="False">
                <asp:ListItem>GEN</asp:ListItem>
                <asp:ListItem>SC/ST</asp:ListItem>
                <asp:ListItem>Other</asp:ListItem>
            </asp:DropDownList></td>
        <td>
            <asp:Label ID="Label9" runat="server" Text="Commodity Category"></asp:Label></td>
        <td>
            <asp:DropDownList ID="ddlverity" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlverity_SelectedIndexChanged" Width="179px">
            </asp:DropDownList></td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label8" runat="server" Text="Commodity Name"></asp:Label></td>
        <td>
            <asp:DropDownList ID="ddlcomodity" runat="server" Width="191px" AutoPostBack="True" OnSelectedIndexChanged="ddlcomodity_SelectedIndexChanged">
            </asp:DropDownList></td>
        <td>
            <asp:Label ID="lblrate" runat="server" Text="Rate"></asp:Label></td>
        <td>
            <asp:TextBox ID="txtcrate" runat="server" ReadOnly="True"></asp:TextBox></td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label11" runat="server" Text="Quantity"></asp:Label></td>
        <td>
            <asp:TextBox ID="txtqty" runat="server"></asp:TextBox>
             <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtqty"
                                        ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender></td>
        <td>
            &nbsp;<asp:Label ID="Label10" runat="server" Text="No. of Bags"></asp:Label></td>
        <td>
            <asp:TextBox ID="txtbags" runat="server" AutoPostBack="True" OnTextChanged="txtbags_TextChanged"></asp:TextBox>
             <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtbags"
                                        ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender></td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label14" runat="server" Text="Amount to be paid"></asp:Label></td>
        <td>
            <asp:TextBox ID="txtamount" runat="server" ReadOnly="True"></asp:TextBox></td>
        <td>
       </td>
        <td>
            </td>
    </tr>
    
    <tr>
        <td>
            <asp:Label ID="Label41" runat="server" Text="Advance Reservation" Width="164px"></asp:Label></td>
        <td>
            <asp:RadioButtonList ID="rdbadvres" runat="server" RepeatDirection="Horizontal"
                RepeatLayout="Flow" OnSelectedIndexChanged="rdbadvres_SelectedIndexChanged" AutoPostBack="True">
                <asp:ListItem Value="Y">Yes</asp:ListItem>
                <asp:ListItem Value="N">No</asp:ListItem>
            </asp:RadioButtonList></td>
        <td>
            <asp:Label ID="Label15" runat="server" Text="Advance payment (100%)" Width="164px"></asp:Label></td>
        <td><asp:RadioButtonList ID="rdbadpay" runat="server" RepeatDirection="Horizontal"
                RepeatLayout="Flow" OnSelectedIndexChanged="rdbadpay_SelectedIndexChanged" AutoPostBack="True">
            <asp:ListItem Value="Y">Yes</asp:ListItem>
            <asp:ListItem Value="N">No</asp:ListItem>
        </asp:RadioButtonList></td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:Label ID="lbldiscmsg" runat="server" Font-Italic="True" ForeColor="Blue"></asp:Label></td>
    </tr>
  <tr style="background-color: #0bb6e6; height: 25px">
        <td colspan="4" align="center" class="style2">
            <asp:Label ID="Label38" runat="server" Font-Bold="True" Font-Size="15pt" ForeColor="WhiteSmoke"
                                        Text="Payment Detail"></asp:Label></td>
    </tr>
    <tr>
        <td align="right" colspan="3" position: static;">
            <asp:Label ID="lblreqdoc" runat="server" ForeColor="#C00000" Text="Required Document for Discount is Submitted by Fertilizer ?"
                Width="384px" Font-Bold="False" Visible="False"></asp:Label></td>
        <td position: static;">
            <asp:RadioButtonList ID="rdbdocst" runat="server" AutoPostBack="True" RepeatDirection="Horizontal"
                RepeatLayout="Flow" OnSelectedIndexChanged="rdbdocst_SelectedIndexChanged" Visible="False">
                <asp:ListItem>Yes</asp:ListItem>
                <asp:ListItem>No</asp:ListItem>
            </asp:RadioButtonList></td>
    </tr>
    <tr>
        <td position: static;">
            <asp:Label ID="Label49" runat="server" Text="Discount in (%)"></asp:Label></td>
        <td style=" position: static;">
            <asp:DropDownList ID="ddldicount" runat="server" Enabled="False">
                <asp:ListItem>0%</asp:ListItem>
                <asp:ListItem>10%</asp:ListItem>
                <asp:ListItem>20%</asp:ListItem>
                <asp:ListItem>30%</asp:ListItem>
                <asp:ListItem>40%</asp:ListItem>
            </asp:DropDownList></td>
        <td style="position: static;">
            <asp:Label ID="Label42" runat="server" Text="Total Months: " ForeColor="Navy"></asp:Label>
            <asp:Label ID="lblmonth" runat="server" Font-Bold="True" ForeColor="Green"></asp:Label></td>
        <td style=" position: static;">
            <asp:Label ID="Label43" runat="server" Text="Total Days:" ForeColor="#000040"></asp:Label>
            <asp:Label ID="lbldays" runat="server" Font-Bold="True" ForeColor="Green"></asp:Label></td>
    </tr>
    <tr>
        <td style=" position: static;">
            <asp:Label ID="Label16" runat="server" Text="Discount"></asp:Label>
            <asp:Label ID="lbldiscnt" runat="server" ForeColor="#C00000" Visible="False"></asp:Label></td>
        <td style=" position: static;">
            <asp:TextBox ID="txtdiscount" runat="server" Width="104px" ForeColor="Navy" ReadOnly="True"></asp:TextBox>
            <asp:Label ID="Label22" runat="server" Text="(In Rs.)"></asp:Label></td>
        <td style=" position: static;">
            <asp:Label ID="Label17" runat="server" Text="Tax & TDS Invoked"></asp:Label></td>
        <td style=" position: static;">
            <asp:DropDownList ID="ddltaxtds" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddltaxtds_SelectedIndexChanged">
                <asp:ListItem>--Se|ect--</asp:ListItem>
                <asp:ListItem Value="Y">Yes</asp:ListItem>
                <asp:ListItem Value="N">No</asp:ListItem>
            </asp:DropDownList></td>
    </tr>
    <tr>
        <td style=" position: static;">
            <asp:Label ID="Label18" runat="server" Text="Service Tax"></asp:Label>
            <asp:Label ID="lblstax" runat="server" ForeColor="#C00000"></asp:Label>
            <asp:Label ID="Label50" runat="server" ForeColor="#C00000">%</asp:Label></td>
        <td style=" position: static;">
            <asp:TextBox ID="txtstax" runat="server" Width="101px" ForeColor="Navy" ReadOnly="True"></asp:TextBox>
            <asp:Label ID="Label27" runat="server" Text="(In Rs.)"></asp:Label></td>
        <td style=" position: static;">
            <asp:Label ID="Label19" runat="server" Text="TDS (if applicable)"></asp:Label>
            <asp:Label ID="lbltds" runat="server" ForeColor="#C00000"></asp:Label>
            <asp:Label ID="Label51" runat="server" ForeColor="#C00000">%</asp:Label></td>
        <td style=" position: static;">
            <asp:TextBox ID="txttds" runat="server" Width="100px" ForeColor="Navy" ReadOnly="True"></asp:TextBox>
            <asp:Label ID="Label30" runat="server" Text="(In Rs.)"></asp:Label></td>
    </tr>
    <tr>
        <td style=" position: static;">
        </td>
        <td style=" position: static;">
        </td>
        <td style=" position: static;">
        </td>
        <td style=" position: static;">
        </td>
    </tr>
    <tr>
        <td style=" position: static;">
            <asp:Label ID="Label20" runat="server" Text="Net Amount to be paid"></asp:Label></td>
        <td style=" position: static;">
            <asp:TextBox ID="txtnetamt" runat="server" Width="103px" ForeColor="Navy" ReadOnly="True"></asp:TextBox>
            <asp:Label ID="Label28" runat="server" Text="(In Rs.)"></asp:Label></td>
        <td style=" position: static;">
            <asp:Label ID="Label21" runat="server" Text="Amount Deposited(excluding TDS)" Width="152px"></asp:Label></td>
        <td style=" position: static;">
            <asp:TextBox ID="txtamtdeposited" runat="server" Width="101px"></asp:TextBox>
               <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtamtdeposited"
                                        ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender>
            <asp:Label ID="Label29" runat="server" Text="(In Rs.)"></asp:Label></td>
    </tr>
    <tr>
        <td style=" position: static;>
            <asp:Label ID="lblPaymentMode" runat="server" Font-Size="12px" Text="Payment Mode"
                Width="92px"></asp:Label></td>
        <td style="position: static;">
            <asp:DropDownList ID="ddl_pmode" runat="server">
                <asp:ListItem Value="D">DD / Cheque</asp:ListItem>
                <asp:ListItem Value="A">Cash</asp:ListItem>
                <asp:ListItem Value="E">E-Payment</asp:ListItem>
            </asp:DropDownList></td>
        <td style=" position: static;">
            <asp:Label ID="lblddchekno" runat="server" Font-Size="12px" Text="DD/Chq. No. "></asp:Label></td>
        <td style="position: static;">
            <asp:TextBox ID="tx_dd_no" runat="server" MaxLength="50" Width="100px"></asp:TextBox><asp:Label
                ID="lbl_ddno" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="Red"
                Text="*"></asp:Label></td>
    </tr>
    <tr>
        <td style=" position: static;">
            <asp:Label ID="lblBankName" runat="server" Font-Size="12px" Text="Bank Name"></asp:Label></td>
        <td style="position: static;">
            <asp:DropDownList ID="ddl_bank" runat="server">
            </asp:DropDownList></td>
        <td style=" position: static;">
            <asp:Label ID="lblddchekdate" runat="server" Font-Size="12px" Text="DD/Chq. Date"></asp:Label></td>
        <td style="position: static;">
            <asp:TextBox ID="txtdd_date" runat="server" Width="99px"></asp:TextBox>
           <cc1:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy"
        TargetControlID="txtdd_date"></cc1:CalendarExtender>
            
            </td>
    </tr>
    <tr>
        <td style="font-size: 10pt; position: static;">
            <asp:Label ID="Label44" runat="server"></asp:Label></td>
        <td style="font-size: 10pt; position: static;">
        </td>
        <td style="font-size: 10pt; position: static;">
            <asp:Label ID="Label45" runat="server"></asp:Label></td>
        <td style="font-size: 10pt; position: static;">
        </td>
    </tr>
   <tr style="background-color: #0bb6e6; height: 25px">
        <td colspan="4" align="center" class="style2">
            <asp:Label ID="Label39" runat="server" Font-Bold="True" Font-Size="15pt" ForeColor="WhiteSmoke"
                                        Text="Godown Detail"></asp:Label></td>
    </tr>
    <tr>
        <td style="">
            <asp:Label ID="Label23" runat="server" Text="Godown"></asp:Label></td>
        <td style="">
            <asp:DropDownList ID="ddlgodown" runat="server" Width="157px" OnSelectedIndexChanged="ddlgodown_SelectedIndexChanged" AutoPostBack="True">
            </asp:DropDownList></td>
        <td >
        </td>
        <td >
        </td>
    </tr>
    <tr>
        <td >
            <asp:Label ID="Label26" runat="server" Text="Total Capacity of Godown" Width="161px"></asp:Label></td>
        <td >
            <asp:TextBox ID="txtmaxcap" runat="server" ForeColor="Navy" ReadOnly="True" Width="105px"></asp:TextBox>
            <asp:Label ID="Label34" runat="server" Text="(in wt.)" Width="45px"></asp:Label></td>
        <td >
            <asp:TextBox ID="txtmaxbags" runat="server" ForeColor="Navy" ReadOnly="True" Width="102px"></asp:TextBox><asp:Label ID="Label31" runat="server" Text="(in Bags)"></asp:Label></td>
        <td >
            &nbsp;</td>
    </tr>
    <tr>
        <td >
            <asp:Label ID="Label24" runat="server" Text="Used Capacity"></asp:Label></td>
        <td >
            <asp:TextBox ID="txtusecap" runat="server" ForeColor="Navy" ReadOnly="True" Width="104px"></asp:TextBox>
            <asp:Label ID="Label35" runat="server" Text="(in wt.)"></asp:Label></td>
        <td >
            <asp:TextBox ID="txtusebags" runat="server" ForeColor="Navy" ReadOnly="True" Width="101px"></asp:TextBox><asp:Label ID="Label32" runat="server" Text="(in Bags)"></asp:Label></td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td >
            <asp:Label ID="Label25" runat="server" Text="Vacant Capacity"></asp:Label></td>
        <td >
            <asp:TextBox ID="txtvaccap" runat="server" ForeColor="Navy" ReadOnly="True" Width="105px"></asp:TextBox>
            <asp:Label ID="Label36" runat="server" Text="(in wt.)"></asp:Label></td>
        <td >
            <asp:TextBox ID="txtvacbags" runat="server" ForeColor="Navy" ReadOnly="True" Width="101px"></asp:TextBox><asp:Label ID="Label33" runat="server" Text="(in Bags)"></asp:Label></td>
        <td style="font-size: 12px; font-family: Verdana; background-color: none">
            &nbsp;</td>
    </tr>
    <tr>
        <td >
            <asp:Label ID="Label40" runat="server" Text="Remarks"></asp:Label></td>
        <td colspan="3" >
            <asp:TextBox ID="txtremarks" runat="server" TextMode="MultiLine" Width="511px">
</asp:TextBox></td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:Label ID="lblerror" runat="server" Visible="False" Font-Bold="True" Font-Italic="True" ForeColor="Maroon"></asp:Label></td>
    </tr>
    <tr>
        <td >
        </td>
        <td align="right">
        <asp:Button ID="btnnew" runat="server" Text="New" Width="141px" CssClass="BTNBLUE" 
                 onclick="btnnew_Click" />
            <asp:Button ID="Button2" runat="server" Text="Save" Width="141px" CssClass="BTNBLUE"  OnClick="Button2_Click" ValidationGroup="1" /></td>
        <td >
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" CssClass="BTNBLUE"  Text="Close" Width="137px" /></td>
        <td >
        </td>
    </tr>
    <tr>
        <td >
        </td>
        <td>
            &nbsp;<asp:Label ID="Label46" runat="server" ForeColor="#004000" Height="21px" Text=""></asp:Label></td>
        <td>
            </td>
        <td>
            </td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                ValidationGroup="1" Width="473px" />
            </td>
    </tr>
    <tr>
        <td colspan="4">
        </td>
    </tr>
</table>
</td></tr>
 </table>
</div>
</center>
</fieldset>
<script type="text/javascript">

function CheckIsNumeric(tx)
{
var AsciiCode = event.keyCode;
var txt=tx.value;
var txt2 = String.fromCharCode(AsciiCode);
var txt3=txt2*1;
if ((AsciiCode <46) || (AsciiCode > 57))
{
alert('Please enter only numbers.');
event.cancelBubble = true;
event.returnValue = false;
}

var num=tx.value;
var len=num.length;
var indx=-1;
indx=num.indexOf('.');
if (indx != -1)
{
var dgt=num.substr(indx,len);
var count= dgt.length;
//alert (count);
if (count > 5)  
{
 alert("Only 5 decimal digits allowed");
 event.cancelBubble = true;
 event.returnValue = false;
}
}

}
    </script>
</asp:Content>

<asp:Content ID="Content2" runat="server" contentplaceholderid="head">

    <style type="text/css">
        .style1
        {
            height: 40px;
        }
    </style>

</asp:Content>


