<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DeliveryOrderCSC.aspx.cs"
    Inherits="IssueCenterLevel_Storage_DeliveryOrderCSC" MasterPageFile="~/MasterPage/Gdwn.master"
    Title="Delivery Order" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript">
        function popMe(url) {
            var newWindow;
            newWindow = window.open(url, 'MyWin', 'width=275,height=390,top=1,left=1');
        }
        function OpenWindow(Sno) {
            window.open("PrintDeliveryOrder.aspx?do=" + Deliveryid, "_new", "height=800,width=780");
        }
   
    </script>


    <script src="<%= ResolveUrl("~/NEW_CSS/js/jquery-1.12.4.js") %>"></script>
  <script src="http://malsup.github.io/jquery.blockUI.js"></script>

  <script type="text/javascript">
      $(document).ready(function () {
          $('#btnsave').click(function () {
              $('.blockMe').block({
                  message: 'Please wait...<br /><img src="mpwlc3.gif" />',
                  css: { padding: '10px' }
              });
          });
      });
</script>


 <%--   <style type="text/css">
    .divWaiting{
   
position: absolute;
background-color: #FAFAFA;
z-index: 2147483647 !important;
opacity: 0.8;
overflow: hidden;
text-align: center; top: 0; left: 0;
height: 100%;
width: 100%;
padding-top:20%;
} 
    
    </style>--%>

     <%--<asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
    <ProgressTemplate>
     <div class="divWaiting">            
	<asp:Label ID="lblWait" runat="server" 
	Text=" " />
	<asp:Image ID="imgWait" runat="server" 
	ImageAlign="Middle" ImageUrl="~/images/mpwlc3.gif" />
  </div>
    
    </ProgressTemplate>
    </asp:UpdateProgress>--%>

   
            <div class="blockMe">
                <table cellpadding="0" cellspacing="0" style="width: 950px">
                    <tr>
                        <td colspan="6" align="center" valign="top">
                            <fieldset style="width: 980px; border: 1px solid navy;">
                                <center>
                                    <div>
                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr style="background-color: #0bb6e6; height: 25px">
                                                <td colspan="4" align="center">
                                                    <asp:Label ID="lblDeliveryOrderCSC" runat="server" Text="Delivery Order" ForeColor="whitesmoke"
                                                        Font-Bold="true" Font-Size="12pt"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 10px" colspan="4" align="center">
                                                    <asp:Label ID="lbl_message" runat="server" Style="font-weight: 700" Text="Your Delivery Order No. is"
                                                        Visible="False"></asp:Label>
                                                    <asp:Label ID="lbl_del_no" runat="server" Style="font-weight: 700; font-size: small"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" align="center">
                                                    <asp:Label ID="lblmsg" runat="server" Font-Size="10pt" ForeColor="Red" EnableViewState="False"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 10px" colspan="4">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lblDepositorType" runat="server" Text="Depositor Type :" ForeColor="navy"
                                                        Font-Bold="true" Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:DropDownList ID="ddlDepositorType" runat="server" OnSelectedIndexChanged="ddlDepositorType_SelectedIndexChanged"
                                                        AutoPostBack="True" TabIndex="1" Height="25px" Width="155px" CssClass="tb6">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic"
                                                        ValidationGroup="SaveValid" ErrorMessage="Please Select Depositor Type" SetFocusOnError="True"
                                                        ControlToValidate="ddlDepositorType" InitialValue="--Select--">*</asp:RequiredFieldValidator>
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="lblDepositorName" runat="server" Text="Depositor Name :" ForeColor="navy"
                                                        Font-Bold="true" Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:DropDownList ID="ddlDepositor" runat="server" TabIndex="2" Height="25px" Width="155px"
                                                        CssClass="tb6" onselectedindexchanged="ddlDepositor_SelectedIndexChanged" 
                                                        AutoPostBack="True">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic"
                                                        ValidationGroup="SaveValid" ErrorMessage="Please Select Depositor" SetFocusOnError="True"
                                                        ControlToValidate="ddlDepositor" InitialValue="--Select--">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    &nbsp;</td>
                                                <td align="left">
                                                    &nbsp;</td>
                                                <td align="left">
                                                    &nbsp;</td>
                                                <td align="left">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lblDepositorType0" runat="server" Font-Bold="True" 
                                                        Font-Size="8pt" ForeColor="Navy" Text="Godown Name:"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:DropDownList ID="ddlgodown" runat="server" AutoPostBack="True" 
                                                        CssClass="tb6" Height="25px" 
                                                        TabIndex="1" 
                                                        Width="155px" onselectedindexchanged="ddlgodown_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="lblDepositorType1" runat="server" Font-Bold="True" 
                                                        Font-Size="8pt" ForeColor="Navy" Text="Commodity:"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:DropDownList ID="ddlcommodty" runat="server" AutoPostBack="True" 
                                                        CssClass="tb6" Height="25px" 
                                                        TabIndex="1" 
                                                        Width="155px" OnSelectedIndexChanged="ddlcommodty_SelectedIndexChanged" >
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 5px" colspan="4">
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </center>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 5px" colspan="6">
                        </td>
                    </tr>
                    <tr id="Gatepdetails" runat="server" visible="false">
                        <td colspan="6" align="center" valign="top">
                            <fieldset style="width: 980px; border: 1px solid navy;">
                                <center>
                                    <div>
                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr>
                                                <td colspan="4" align="left">
                                                    <span style="color: Red; font-size: 10pt; font-weight: bold">Note:- उपरोक्त में से जिस
                                                        गेटपास का डिलेवरी आर्डर बनाना है उसका चयन करें !</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 5px" colspan="4">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" colspan="6">
                                                    <asp:Label ID="lblRowCount" runat="server" ForeColor="navy" Font-Bold="true" Text="Total Record "
                                                        Font-Size="10pt"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 5px" colspan="4">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" align="center" valign="top">
                                                    <div style="overflow: scroll; height: 200px; overflow-x: hidden">
                                                        <asp:GridView ID="gdGPHelp" runat="server" CellPadding="2" CellSpacing="1" TabIndex="7"
                                                            Width="100%" Font-Size="9pt" ForeColor="Navy" AutoGenerateColumns="False" DataKeyNames="gatepass_no"
                                                            EnableModelValidation="True">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Select " ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="40px">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="ckGP" runat="server" OnCheckedChanged="ckGP_CheckedChanged" AutoPostBack="True" />
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Width="40px" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="gatepass_no" HeaderText="GatePass No.">
                                                                    <ItemStyle Width="120px" HorizontalAlign="left" />
                                                                    <HeaderStyle Width="60px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="depo" HeaderText="Depositor">
                                                                    <ItemStyle Width="100px" HorizontalAlign="left" />
                                                                    <HeaderStyle Width="60px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity">
                                                                    <ItemStyle Width="100px" HorizontalAlign="left" />
                                                                    <HeaderStyle Width="60px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Vehicle_No" HeaderText="Vehicle No.">
                                                                    <ItemStyle Width="100px" HorizontalAlign="left" />
                                                                    <HeaderStyle Width="60px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Bags" HeaderText="No. Of Bags">
                                                                    <ItemStyle Width="100px" HorizontalAlign="right" />
                                                                    <HeaderStyle Width="60px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Weight" HeaderText="Weight">
                                                                    <ItemStyle Width="100px" HorizontalAlign="right" />
                                                                    <HeaderStyle Width="60px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Issue_Date" HeaderText="Issue Date">
                                                                    <ItemStyle Width="100px" HorizontalAlign="center" />
                                                                    <HeaderStyle Width="60px" />
                                                                </asp:BoundField>
                                                            </Columns>
                                                            <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                                                            <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                                                                Height="20px" Font-Size="10pt" />
                                                            <AlternatingRowStyle BackColor="White" />
                                                        </asp:GridView>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </center>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 10px" colspan="6" align="center">
                            <asp:Label ID="lbl_notfound" runat="server" Font-Size="10pt" ForeColor="Red" Font-Bold="true"
                                Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr id="trdo_Details" runat="server" visible="false">
                        <td colspan="6" align="center" valign="top">
                            <fieldset style="width: 980px; border: 1px solid navy;">
                                <center>
                                    <div>
                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr style="background-color: #0bb6e6; height: 25px">
                                                <td colspan="4" align="center">
                                                    <span style="color: White; font-size: 12pt; font-weight: bold">Delivery Order Details</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 10px" colspan="4">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lblDeliveryOrderDate" runat="server" Text="Delivery Order Date :"
                                                        ForeColor="navy" Font-Bold="true" Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td align="left" colspan="3">
                                                    <asp:TextBox ID="txtDoDate" runat="server" MaxLength="12" Height="20px" Width="150px"
                                                        CssClass="tb6"></asp:TextBox>
                                                    <asp:CalendarExtender ID="CalendarExtender" runat="server" Enabled="True" TargetControlID="txtDoDate"
                                                        Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy" CssClass="cal_Theme1">
                                                    </asp:CalendarExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 5px" colspan="4">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lblIssuedBags" runat="server" Text="Issued Bags :" ForeColor="navy"
                                                        Font-Bold="true" Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtIssueBags" runat="server" BackColor="#FFFFC0" Enabled="False"
                                                        Height="20px" Width="150px" CssClass="tb6" Text="0"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="lblIssuedweight" runat="server" Text="Issued Weight :" ForeColor="navy"
                                                        Font-Bold="true" Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtIssueWt" runat="server" Width="150px" Height="20px" CssClass="tb6"
                                                        BackColor="#FFFFC0" Enabled="False" Text="0"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 5px" colspan="4">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="4">
                                                    <asp:Button ID="btnsave" runat="server" Text="Save Record" Width="100px" CssClass="BTNBLUE"
                                                        OnClick="btnsave_Click" ClientIDMode="Static" TabIndex="10" ValidationGroup="SaveValid" />
                                                    <asp:Button ID="btn_Close" runat="server" Text="Close" Width="100px" OnClick="btn_Close_Click"
                                                        CssClass="BTNBLUE" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 5px" colspan="4">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 5px" colspan="4">
                                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                                        ShowSummary="False" ValidationGroup="SaveValid" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </center>
                            </fieldset>
                        </td>
                    </tr>
                    <tr id="trDO" runat="server" visible="false">
                        <td align="right" colspan="6">
                            <asp:HyperLink ID="HPL_ShowDO" runat="server" Width="127px" Font-Bold="True" Font-Size="X-Small"
                                ForeColor="Maroon" Target="_blank" 
                                NavigateUrl="~/IssueCenterLevel/Storage/PrintDeliveryOrder.aspx"><u>Show Delivery Order</u></asp:HyperLink>
                        </td>
                    </tr>
                </table>
            </div>
       
</asp:Content>
