<%@ Page Language="C#" MasterPageFile="~/MasterPage/StateMaster.master" AutoEventWireup="true" CodeFile="frmStorage_Rate.aspx.cs" Inherits="HOWLC_frmStorage_Rate" Title="Storage Rate Master" %>
  <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <span class="style2">
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
    </span>
 <fieldset style="width: 1000px; border: 2px solid navy;">
        <center>
<div id ="cal1">
<center >
<table style=" width: 100%;height: 355px;">
<tr> 

<td align="center">


<table style="width:100%">
 <tr style="background-color: #0bb6e6; height: 25px">
        <td colspan="4" align="center" class="style2">
            <asp:Label ID="Label37" runat="server" Font-Bold="True" Font-Size="15pt" ForeColor="WhiteSmoke"
                                        Text="Commodity Rate Master"></asp:Label></td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:Label ID="lblmsg" runat="server" Font-Italic="True" ForeColor="Maroon" Visible="False"></asp:Label></td>
    </tr>
    <tr>
        <td align="left" rowspan="2">
            <asp:Label ID="lbldtype" Font-Size="8pt" Font-Bold="true" ForeColor="navy"  runat="server" Text="Depositor Type" Visible="true"></asp:Label></td>
        <td rowspan="2">
            <asp:DropDownList ID="ddldepositor" runat="server" AutoPostBack="True" TabIndex="1" Height="30px" Width="200px" CssClass="tb6" Font-Size="10pt">
    </asp:DropDownList></td>
        <td align="left" rowspan="1"
            valign="top">
            <asp:Label ID="Label12" Font-Size="8pt" Font-Bold="true" ForeColor="navy"  runat="server" Text="Select Commodity"></asp:Label></td>
        <td rowspan="10">
            <asp:Label ID="lblchk" runat="server" Font-Bold="True" ForeColor="#C00000" Text="Rate already  Exist for the Commodity"
                Visible="False" Width="103px"></asp:Label>
            <asp:ListBox ID="ListBox1" runat="server" Visible="False"></asp:ListBox></td>
    </tr>
    <tr>
        <td rowspan="9"
            valign="top" align="left">
            <asp:Panel ID="Panel1" runat="server" BorderStyle="Double" Height="240px" ScrollBars="Both"
                Width="223px">
                <asp:CheckBoxList ID="CheckBoxList1" runat="server" BorderStyle="Double" Width="209px">
                </asp:CheckBoxList></asp:Panel>
        </td>
    </tr>
    <tr>
        <td align="left">
            <asp:Label ID="Label9" Font-Size="8pt" Font-Bold="true" ForeColor="navy"  runat="server" Text="Commodity Category" Width="127px"></asp:Label></td>
        <td>
            <asp:DropDownList ID="ddlverity" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlverity_SelectedIndexChanged" TabIndex="1" Height="30px" Width="200px" CssClass="tb6" Font-Size="10pt">
            </asp:DropDownList></td>
    </tr>
    <tr>
        <td align="left">
            <asp:Label Font-Size="8pt" Font-Bold="true" ForeColor="navy"  ID="Label1" runat="server" Text="Packing Type"></asp:Label></td>
        <td>
            <asp:DropDownList ID="ddlpacktype" runat="server" TabIndex="1" Height="30px" Width="200px" CssClass="tb6" Font-Size="10pt">
            </asp:DropDownList></td>
    </tr>
    <tr>
        <td align="left">
            <asp:Label Font-Size="8pt" Font-Bold="true" ForeColor="navy"  ID="Label2" runat="server" Text="Weight"></asp:Label></td>
        <td>
            <asp:DropDownList ID="ddlweight" runat="server" TabIndex="1" Height="30px" Width="200px" CssClass="tb6" Font-Size="10pt">
            </asp:DropDownList></td>
    </tr>
    <tr>
        <td align="left">
            <asp:Label ID="Label5" Font-Size="8pt" Font-Bold="true" ForeColor="navy"  runat="server" Text="Effective From"></asp:Label></td>
        <td align="left">
                
            <asp:TextBox ID="fromdate" runat="server" TabIndex="1" Height="23px" Width="195px" CssClass="tb6" Font-Size="10pt"></asp:TextBox>
          <cc1:CalendarExtender ID="TextBox1_CalendarExtender" runat="server" 
                                                        Enabled="True" TargetControlID="fromdate" Format="dd/MM/yyyy">
                                                    </cc1:CalendarExtender>
            
            </td>
    </tr>
    <tr>
        <td align="left">
            <asp:Label ID="Label6" Font-Size="8pt" Font-Bold="true" ForeColor="navy"  runat="server" Text="Rate"></asp:Label></td>
        <td align="left">
            <asp:TextBox ID="txtrate" runat="server" TabIndex="1" Height="23px" Width="195px" CssClass="tb6" Font-Size="10pt"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtrate"
                                        ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender>
            <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Size="10px" Text="(Rs.)"></asp:Label></td>
    </tr>
    <tr>
        <td align="left">
            <asp:Label ID="Label7" Font-Size="8pt" Font-Bold="true" ForeColor="navy"  runat="server" Text="Revised Date"></asp:Label></td>
        <td align="left">
         <asp:TextBox ID="revdate" runat="server" TabIndex="1" Height="23px" Width="195px" CssClass="tb6" Font-Size="10pt"></asp:TextBox>
             <cc1:CalendarExtender ID="CalendarExtender1" runat="server" 
                                                        Enabled="True" TargetControlID="revdate" Format="dd/MM/yyyy">
                                          
                                                    </cc1:CalendarExtender>
                                                  <asp:Button ID="addRevisedDate" Text="Add More" runat="server" 
            onclick="addRevisedDate_Click" />
</td>
    </tr>
    <tr>
        <td align="left">
            <asp:Label ID="Label10" Font-Size="8pt" Font-Bold="true" ForeColor="navy"  runat="server" Text="Rate"></asp:Label></td>
        <td align="left">
            <asp:TextBox ID="txtrevrate" runat="server" TabIndex="1" Height="23px" Width="195px" CssClass="tb6" Font-Size="10pt"></asp:TextBox>
             <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtrevrate"
                                        ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender>
            <asp:Label ID="Label11" runat="server" Font-Bold="True" Font-Size="10px" Text="(Rs.)"></asp:Label></td>
    </tr>
    <tr>
    <td align="center" colspan="4">
    
<asp:GridView ID="gvRevisedDate" runat="server" AutoGenerateColumns="False" 
            onrowdeleting="gvRevisedDate_RowDeleting" BackColor="White" 
            BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" 
            CellSpacing="1" GridLines="None">
    <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
<Columns>
    <asp:BoundField DataField="RevisedDate" HeaderText="RevisedDate" ItemStyle-Width="150" />
    <asp:BoundField DataField="Rate" HeaderText="Rate" ItemStyle-Width="150" />
    <asp:TemplateField>
        <ItemTemplate>
            <asp:LinkButton ID="LinkButton1" Text="Delete" runat="server" CommandName="Delete" />
        </ItemTemplate>
        <EditItemTemplate>
            <asp:LinkButton ID="LinkButton2" Text="Update" runat="server" />
            <asp:LinkButton ID="LinkButton3" Text="Cancel" runat="server" />
        </EditItemTemplate>
    </asp:TemplateField>
</Columns>
    <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
    <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
    <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
    <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" />
</asp:GridView>

                                       
                                                        </td>
    </tr>
    <tr>
        <td align="left">
            <asp:Label ID="Label13" Font-Size="8pt" Font-Bold="true" ForeColor="navy"  runat="server" Text="Remarks"></asp:Label></td>
        <td align="left" colspan="2">
            <asp:TextBox ID="txtremark" runat="server" TextMode="MultiLine" TabIndex="1" Height="50px" Width="421px" CssClass="tb6" Font-Size="10pt"></asp:TextBox></td>
        <td>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Button ID="btnnew" runat="server" OnClick="btnnew_Click" Text="New" Width="96px" CssClass="BTNBLUE"  /></td>
        <td align="center">
            <asp:Button ID="btnsave" runat="server" Text="Save" Width="141px" CssClass="BTNBLUE" 
                OnClick="btnsave_Click"/>
        </td>
        <td align="left">
            <asp:Button ID="btnclose" CssClass="BTNBLUE"  runat="server" OnClick="btnclose_Click" Text="Close" Width="137px" /></td>
        <td>
            &nbsp;</td>
    </tr>
</table>
</td></tr>
 </table>
 </center>
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
if ((AsciiCode < 46) || (AsciiCode > 57))
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
if (count > 2)  
{
 alert("Only 2 decimal digits allowed");
 event.cancelBubble = true;
 event.returnValue = false;
}
}

}



    </script>
</asp:Content>




