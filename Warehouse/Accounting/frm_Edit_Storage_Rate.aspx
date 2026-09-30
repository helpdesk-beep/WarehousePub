<%@ Page Language="C#" MasterPageFile="~/MasterPage/StateMaster.master" AutoEventWireup="true" CodeFile="frm_Edit_Storage_Rate.aspx.cs" Inherits="HOWLC_frm_Edit_Storage_Rate" Title="Storage Rate Master" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script type="text/javascript">

{
function validiation(tx)
{
if(tx.Text="")
{
alet("plz enter Value"); 
}
}
}
</script>
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
<table style="width: 100%; height: 355px; margin-left:0px; background-color:none;">
<tr> 
<td align="center">
<table style=" width:100%">
    <tr style="background-color: #0bb6e6; height: 25px">
        <td colspan="4" align="center" class="style2">
            <asp:Label ID="Label37" runat="server" Font-Bold="True" Font-Size="15pt" ForeColor="WhiteSmoke"
                                        Text="Edit Storage Rate Master"></asp:Label></td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:Label ID="lblmsg" runat="server" Visible="False" ForeColor="Red" Font-Bold="true"></asp:Label></td>
    </tr>
    <tr>
        <td align="left">
            
            <asp:Label ID="Label12" runat="server" Font-Size="8pt" Font-Bold="true" ForeColor="navy" Text="Depositor Type"></asp:Label></td>
        <td align="left">
        <asp:DropDownList ID="ddldepositor" runat="server" TabIndex="1" Height="30px" Width="160px" CssClass="tb6" Font-Size="10pt" Enabled="true">
            </asp:DropDownList>
            </td>
        <td rowspan="9"
            valign="top" align="left">
            <asp:Panel ID="Panel1" runat="server" BorderStyle="Double" Height="220px" ScrollBars="Both"
                Width="800px">
                 <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
                    OnRowDataBound ="GridView1_RowDataBound"  Width="758px" BackColor="White" 
                     BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" 
                     GridLines="None" onselectedindexchanged="GridView1_SelectedIndexChanged1" 
                     CellSpacing="1" onrowdeleting="GridView1_RowDeleting" 
                    >
                    <Columns>
                         <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" 
                                                                ItemStyle-ForeColor="red" >
                          <ItemStyle ForeColor="Red"></ItemStyle>
                                                            </asp:CommandField>
                        <asp:CommandField HeaderText="Update" ShowSelectButton="True">
                            <HeaderStyle Font-Size="10px" />
                            <ItemStyle Font-Size="9px" />
                        </asp:CommandField>
                        <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity" SortExpression="Commodity_Name">
                            <HeaderStyle Font-Size="10px" />
                            <ItemStyle Font-Size="9px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Packing_Name" HeaderText="Packing Type" SortExpression="Packing_Name">
                            <HeaderStyle Font-Size="10px" />
                            <ItemStyle Font-Size="9px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Weight_Type" HeaderText="Weight" SortExpression="Weight_Type">
                            <HeaderStyle Font-Size="10px" />
                            <ItemStyle Font-Size="9px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Effective From">     
                <ItemTemplate>
                <asp:Label ID="lbldate" runat="server" 
                Text='<%# Eval("EDates").ToString()%>'>
                </asp:Label>
                </ItemTemplate>
                            <HeaderStyle Font-Size="10px" />
                            <ItemStyle Font-Size="9px" />
                 </asp:TemplateField>                        
                       
                        <asp:BoundField DataField="Rate" HeaderText="Effective Rate" SortExpression="Rate">
                            <HeaderStyle Font-Size="10px" />
                            <ItemStyle Font-Size="9px" />
                        </asp:BoundField>
                      
                        <asp:BoundField DataField="Commodity_Id" HeaderText="CID" SortExpression="Commodity_Id">
                            <HeaderStyle Font-Size="10px" />
                            <ItemStyle Font-Size="9px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Packing_Id" HeaderText="PID" SortExpression="Packing_type">
                            <HeaderStyle Font-Size="10px" />
                            <ItemStyle Font-Size="9px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Weight_ID" HeaderText="WID" SortExpression="Weight">
                            <HeaderStyle Font-Size="1pt" />
                            <ItemStyle Font-Size="9px" />
                        </asp:BoundField>
                      
                       <asp:TemplateField HeaderText="Remarks">
               <ItemTemplate>
                <asp:Label ID="lblrmk" runat="server" 
                Text='<%# Eval("Remark").ToString()%>'>
                </asp:Label>
                </ItemTemplate>
                            <HeaderStyle Font-Size="10px" />
                            <ItemStyle Font-Size="9px" />
                 </asp:TemplateField>
                 <asp:BoundField DataField="Id" HeaderText="RID">
                            <HeaderStyle Font-Size="1pt" />
                            <ItemStyle Font-Size="9px" />
                        </asp:BoundField>
                    </Columns>
                    <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
                    <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                    <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                    <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" />
                </asp:GridView>
                </asp:Panel>
        </td>
        <td rowspan="9">
            &nbsp;</td>
    </tr>
    <tr>
        <td align="left">
            <asp:Label ID="Label9" runat="server" Font-Size="8pt" Font-Bold="true" ForeColor="navy" Text="Category"></asp:Label></td>
        <td align="left">
        <asp:DropDownList ID="ddlverity" TabIndex="1" Height="30px" Width="160px" CssClass="tb6" Font-Size="10pt" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlverity_SelectedIndexChanged">
            </asp:DropDownList>
            </td>
    </tr>
    <tr>
        <td align="left">
            <asp:Label ID="Label8" runat="server" Font-Size="8pt" Font-Bold="true" ForeColor="navy" Text="Commodity Name"></asp:Label></td>
        <td align="left">
            <asp:DropDownList ID="ddlcomodity" runat="server" TabIndex="1" Height="30px" Width="160px" CssClass="tb6" Font-Size="10pt" OnSelectedIndexChanged="ddlcomodity_SelectedIndexChanged" Enabled="False">
            </asp:DropDownList></td>
    </tr>
    <tr>
        <td align="left">
            <asp:Label ID="Label1" Font-Size="8pt" Font-Bold="true" ForeColor="navy" runat="server" Text="Packing Type"></asp:Label></td>
        <td align="left">
            <asp:DropDownList ID="ddlpacktype" runat="server" TabIndex="1" Height="30px" Width="160px" CssClass="tb6" Font-Size="10pt">
            </asp:DropDownList></td>
    </tr>
    <tr>
        <td align="left">
            <asp:Label ID="Label2" Font-Size="8pt" Font-Bold="true" ForeColor="navy" runat="server" Text="Weight"></asp:Label></td>
        <td align="left">
            <asp:DropDownList ID="ddlweight" runat="server" TabIndex="1" Height="30px" Width="160px" CssClass="tb6" Font-Size="10pt">
            </asp:DropDownList></td>
    </tr>
    <tr>
        <td align="left">
            <asp:Label ID="Label5" Font-Size="8pt" Font-Bold="true" ForeColor="navy" runat="server" Text="Effective From"></asp:Label></td>
        <td align="left">
                   
            <asp:TextBox ID="fromdate" Enabled="true" runat="server" BackColor="LemonChiffon" TabIndex="9" CssClass="tb6" Width="100px" Height="20px"></asp:TextBox>
        <cc1:CalendarExtender ID="txtdate_CalendarExtender" runat="server" Format="dd/MM/yyyy" 
        TargetControlID="fromdate">
    </cc1:CalendarExtender>
            </td>
    </tr>
    <tr>
        <td align="left">
            <asp:Label ID="Label6" Font-Size="8pt" Font-Bold="true" ForeColor="navy" runat="server" Text="Rate"></asp:Label></td>
        <td align="left">
            <asp:TextBox ID="txtrate" runat="server" BackColor="LemonChiffon" TabIndex="9" CssClass="tb6" Width="100px" Height="20px"></asp:TextBox>
           <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtrate"
                                        ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender>
                         
            <asp:Label ID="Label4"
                runat="server" Font-Bold="True" Font-Size="10px" Text="(Rs.)"></asp:Label></td>
    </tr>
    <tr>
        <td align="left">
            <asp:Label ID="Label13" Font-Size="8pt" Font-Bold="true" ForeColor="navy" runat="server" Text="Remarks"></asp:Label></td>
        <td align="left" colspan="2" >
            <asp:TextBox ID="txtremark" runat="server" TextMode="MultiLine" Width="421px" BackColor="LemonChiffon" TabIndex="9" CssClass="tb6"></asp:TextBox></td>
        <td>
        </td>
    </tr>
    <tr>
        <td > 
        <asp:Button ID="btnsave" runat="server" Text="Update" Width="141px" OnClick="btnsave_Click" CssClass="BTNBLUE"  />
            <%--<asp:Button ID="btnnew" runat="server" OnClick="btnnew_Click" Text="New" Width="136px" CssClass="BTNBLUE" />--%></td>
        <td align="center">
            <asp:Button ID="btnclose" runat="server" OnClick="btnclose_Click" Text="Close" Width="137px" CssClass="BTNBLUE"  />
        </td>
        <%--<td align="left">
            <asp:Button ID="btnclose" runat="server" OnClick="btnclose_Click" Text="Close" Width="137px" CssClass="BTNBLUE"  />
            <asp:Label ID="lbltid" runat="server" Visible="false"></asp:Label></td>--%>
        <td>
            &nbsp;</td>
    </tr>
</table>
</td></tr>
 </table>
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