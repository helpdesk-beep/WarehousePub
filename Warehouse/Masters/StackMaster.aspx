<%@ Page Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true"
    CodeFile="StackMaster.aspx.cs" Inherits="Masters_StackMaster" EnableEventValidation="false"
    ValidateRequest="false" Title="Stack Master" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<style type="text/css">
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
    
    </style>
    <script type="text/javascript" src="../../JS/allFormValidations.js"></script>

    <script type="text/javascript">
        function popMe(url) {
            var newWindow;
            newWindow = window.open(url, 'MyWin', 'width=275,height=390,top=1,left=1');

        }

        function chkgod() {

            if (document.getElementById("ctl00_ContentPlaceHolder1_dprlst_Godown").value == "--Select--") {

                alert('Please Select Godown No')

            }
        }

        function isNumberKey2(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode

            if (charCode == 46) {
                var inputValue = $("#inputfield").val()
                if (inputValue.indexOf('.') < 1) {
                    return true;
                }
                return false;
            }
            if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }

    </script>

    <script type="text/javascript" language="javascript">
        function ConfirmOnDelete() {
            if (confirm("Are you sure want to Delete?") == true)
                return true;
            else
                return false;
        }
    </script>
    <%--<asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
    <ProgressTemplate>
     <div class="divWaiting">            
	<asp:Label ID="lblWait" runat="server" 
	Text=" " />
	<asp:Image ID="imgWait" runat="server" 
	ImageAlign="Middle" ImageUrl="~/images/mpwlc3.gif" />
  </div>
    
    </ProgressTemplate>
    </asp:UpdateProgress>--%>
    <center>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <fieldset style="width: 1000px; border: 2px solid navy;">
                    <center>
                        <div>
                            <table cellpadding="0" cellspacing="0" style="width: 100%">
                                <tr>
                                    <td style="height: 5px" colspan="4">
                                        <asp:Label ID="lbl" runat="server" Style="font-weight: 700; font-size: x-small" Text="Last Added Stack id No.-"
                                            Visible="False"></asp:Label>
                                        <asp:Label ID="lbl_stackid" runat="server" Style="font-weight: 700; font-size: small;
                                            color: #800000" Visible="False"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" align="center" valign="top">
                                        <fieldset style="width: 980px; border: 1px solid navy;">
                                            <center>
                                                <div>
                                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                        <tr style="background-color: #0bb6e6; height: 25px">
                                                            <td colspan="4" align="center">
                                                                <asp:Label ID="Label11" runat="server" Text="Available Stack Details " Font-Bold="true"
                                                                    ForeColor="whiteSmoke" Font-Size="12pt"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="height: 10px" colspan="4" align="center">
                                                                <asp:LinkButton ID="LinkButton1" runat="server" Font-Underline="True" ForeColor="#0000CC"
                                                                    PostBackUrl="~/Masters/NewStackMaster.aspx" CausesValidation="False">Back</asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2" align="left">
                                                                <asp:Label ID="lblRowCount" runat="server" ForeColor="navy" Font-Bold="true" Text="Total Record "
                                                                    Font-Size="10pt"></asp:Label>
                                                            </td>
                                                            <td colspan="2" align="right">
                                                                <a href="javascript:popMe('../SampleQuantity.htm');" style="text-decoration: underline">
                                                                    (Qty. in Qtls.kgsgms)</a>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="height: 5px" colspan="4">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="4" align="center" valign="top">
                                                                <asp:Panel ID="panelContainer" runat="server" Height="350px" ScrollBars="Vertical">
                                                                    <asp:GridView ID="stack_GridView" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                                        Width="100%" DataKeyNames="Stack_ID" Font-Size="9pt" 
                                                                        OnRowCommand="stack_GridView_RowCommand" CellSpacing="4">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="S.N.">
                                                                                <ItemTemplate>
                                                                                    <%#Container.DataItemIndex+1%>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                                                                <ItemStyle HorizontalAlign="Center" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Delete">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lnk_Delete" runat="server" CommandName="Deletes" ForeColor="red"
                                                                                        Text="Delete" OnClientClick="return ConfirmOnDelete();"></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle Font-Bold="True" Font-Names="Arial" Font-Size="10pt" HorizontalAlign="Center"
                                                                                    Width="50px" />
                                                                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                                                <ControlStyle Width="50px" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Edit">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lnk_Update" runat="server" CommandName="Updates" ForeColor="navy"
                                                                                        Text="Edit"></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle Font-Bold="True" Font-Names="Arial" Font-Size="10pt" HorizontalAlign="Center"
                                                                                    Width="50px" />
                                                                                <ItemStyle HorizontalAlign="Center" Width="30px" />
                                                                                <ControlStyle Width="50px" />
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField DataField="Godown_Name" HeaderText="Godown No." SortExpression="Godown_Name" />
                                                                            <asp:BoundField DataField="Stack_ID" HeaderText="Stack Id">
                                                                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="Stack_Name" HeaderText="Stack Name" SortExpression="Stack_Name">
                                                                                <ItemStyle HorizontalAlign="Left" Width="150px" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity" 
                                                                                SortExpression="Commodity_Name">
                                                                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="StackingStatus" HeaderText="Stacking Status" SortExpression="StackingStatus">
                                                                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="Stack_capacity" HeaderText="Stack Capacity">
                                                                                <ItemStyle HorizontalAlign="Right" Width="50px" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="Current_Capacity" HeaderText="Current Stock">
                                                                                <ItemStyle HorizontalAlign="Right" Width="50px" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="Storage_Type" HeaderText="Storage Type" SortExpression="Storage_Type">
                                                                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="Hired_type" HeaderText="Hired Type" SortExpression="Hired_type">
                                                                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="Godown_ID" Visible="true">
                                                                                <HeaderStyle Font-Size="0pt" />
                                                                                <ItemStyle Font-Size="0pt" ForeColor="White" />
                                                                            </asp:BoundField>
                                                                             <asp:BoundField DataField="Commodity_Id" Visible="true">
                                                                                <HeaderStyle Font-Size="0pt" />
                                                                                <ItemStyle Font-Size="0pt" ForeColor="White" />
                                                                            </asp:BoundField>
                                                                              <asp:BoundField DataField="MarketingSeason" HeaderText="Marketing Season" SortExpression="MarketingSeason">
                                                                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                                            </asp:BoundField>
                                                                              <asp:BoundField DataField="BagType" HeaderText="Bag Type" SortExpression="BagType">
                                                                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                                            </asp:BoundField>
                                                                              <asp:BoundField DataField="CropYear" HeaderText="Crop Year" SortExpression="CropYear">
                                                                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                                            </asp:BoundField>
                                                                        </Columns>
                                                                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                                        <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                                                                            Height="20px" Font-Size="10pt" />
                                                                        <AlternatingRowStyle BackColor="#eeeeee" />
                                                                    </asp:GridView>
                                                                </asp:Panel>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </center>
                                        </fieldset>
                                    </td>
                                </tr>
                              
                                <tr id="PanelStack" runat="server" visible="false">
                                    <td colspan="4" align="center" valign="top">
                                        <fieldset style="width: 980px; border: 1px solid navy;">
                                            <center>
                                                <div>
                                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                        <tr style="background-color: #0bb6e6; height: 25px">
                                                            <td colspan="2" align="center">
                                                                <span style="color: White; font-size: 12pt; font-weight: bold" runat="server" id="Newstackhead">
                                                                </span>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="height: 10px" colspan="2">
                                                                <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Font-Bold="true" Font-Size="10pt"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 300px" align="left">
                                                                <asp:Label ID="Label3" runat="server" Text="Godown No. -" Font-Bold="true" ForeColor="navy"
                                                                    Font-Size="8pt"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:DropDownList ID="dprlst_Godown" runat="server" Width="205px" AutoPostBack="True"
                                                                    Height="25px">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="height: 5px" colspan="2">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <asp:Label ID="Label4" runat="server" Text="Commodity Name -" Font-Bold="true" ForeColor="navy"
                                                                    Font-Size="8pt"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:DropDownList ID="dprlst_Commodity" runat="server" Width="205px" Height="25px"
                                                                    AutoPostBack="True" OnSelectedIndexChanged="dprlst_Commodity_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="height: 5px" colspan="2">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <asp:Label ID="lbl_ParentStack" runat="server" Text="Base Stack/Parent Stack" Font-Bold="true"
                                                                    ForeColor="navy" Font-Size="8pt" Visible="true"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:DropDownList ID="drplst_ParentStack" runat="server" Width="205px" Height="25px"
                                                                    Visible="true">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="height: 5px" colspan="2">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <asp:Label ID="Label6" runat="server" Text="Stack Name -" Font-Bold="true" ForeColor="navy"
                                                                    Font-Size="8pt"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtStackNumber" runat="server" Width="200px" AutoComplete="off"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtStackNumber"
                                                                    Display="Dynamic" ErrorMessage="Stack Name Cannot be empty" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="height: 5px" colspan="2">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" valign="top">
                                                                <asp:Label ID="lbl_Remarks" runat="server" Text="Remarks(MD's Approval) -" Font-Bold="true"
                                                                    ForeColor="navy" Font-Size="8pt"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txt_Remarks" runat="server" TextMode="MultiLine" Width="200px" Height="50px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="height: 5px" colspan="2">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <asp:Label ID="Label7" runat="server" Text="Stack Capacity (Qtls .kgsgms) -" Font-Bold="true"
                                                                    ForeColor="navy" Font-Size="8pt"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtStackQty" runat="server" Width="200px" AutoComplete="off" 
                                                                    MaxLength="6">2000</asp:TextBox>
                                                                <asp:FilteredTextBoxExtender ID="txtStackQty_FilteredTextBoxExtender" runat="server"
                                                                    TargetControlID="txtStackQty" FilterType="Custom, Numbers" ValidChars=".">
                                                                </asp:FilteredTextBoxExtender>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtStackQty"
                                                                    Display="Dynamic" ErrorMessage="Stack Capacity cannot be empty" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                                               (Stack capacity should not be gretter than 2000 Qntl)</td>

                                                        </tr>
                                                        <tr>
                                                            <td style="height: 5px" colspan="2">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <asp:Label ID="Label8" runat="server" Text="Storage Type -" Font-Bold="true" ForeColor="navy"
                                                                    Font-Size="8pt"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:DropDownList ID="dprlst_Storage" runat="server" Width="205px" Enabled="False"
                                                                    Height="25px">
                                                                    <asp:ListItem Text="Covered" Value="Covered"></asp:ListItem>
                                                                    <asp:ListItem Text="Permanent(CAP)" Value="Permanent(CAP)"></asp:ListItem>
                                                                    <asp:ListItem Text="Temporary(CAP)" Value="Temporary(CAP)"></asp:ListItem>
                                                                    <asp:ListItem Text="Silo Bag" Value="SiloBag"></asp:ListItem>
                                                                    <asp:ListItem Text="Steel Silo" Value="SteelSilo"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="height: 5px" colspan="2">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <asp:Label ID="Label9" runat="server" Text="Hired Type -" Font-Bold="true" ForeColor="navy"
                                                                    Font-Size="8pt"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:DropDownList ID="dprlst_Hired" runat="server" Width="205px" Enabled="False"
                                                                    Height="25px">
                                                                    <asp:ListItem Text="Owned" Value="Owned"></asp:ListItem>
                                                                    <asp:ListItem Text="Hired" Value="Hired"></asp:ListItem>
                                                                    <asp:ListItem Text="Joint Venture(JV)" Value="Joint Venture(JV)"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                           <tr>
                                                            <td style="height: 5px" colspan="2">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <asp:Label ID="Label1" runat="server" Text="Marketing Season" Font-Bold="true" ForeColor="navy"
                                                                    Font-Size="8pt"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:DropDownList ID="ddlCropType" runat="server" Width="205px" Enabled="true"
                                                                    Height="25px">
                                                                     <asp:ListItem Text="--Select--" Value="-1" Selected="True"></asp:ListItem>
                                                                    <asp:ListItem Text="Kharif" Value="1"></asp:ListItem>
                                                                    <asp:ListItem Text="Rabi" Value="2"></asp:ListItem>
                                                                   
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                         <tr>
                                                            <td style="height: 5px" colspan="2">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <asp:Label ID="Label5" runat="server" Text="Bag Type" Font-Bold="true" ForeColor="navy"
                                                                    Font-Size="8pt"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:DropDownList ID="ddlBagType" runat="server" Width="205px" Enabled="true"
                                                                    Height="25px">
                                                                     <asp:ListItem Text="--Select--" Value="-1"></asp:ListItem>
                                                                    <asp:ListItem Text="SBT(580)" Value="1"></asp:ListItem>
                                                                    <asp:ListItem Text="SBT" Value="2"></asp:ListItem>
                                                                    <asp:ListItem Text="HDPE" Value="3"></asp:ListItem>
                                                                    
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                         <tr>
                                                            <td style="height: 5px" colspan="2">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <asp:Label ID="lblCropYr" runat="server" Text="Crop Year" Font-Bold="true" ForeColor="navy"
                                                                    Font-Size="8pt"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:DropDownList ID="ddlCropYear" runat="server" Width="205px" Enabled="true"
                                                                    Height="25px">
                                                                     <asp:ListItem Text="--Select--" Value="-1"></asp:ListItem>
                                                                    <asp:ListItem Text="2026-2027" Value="2026-2027"></asp:ListItem>
                                                                    <asp:ListItem Text="2025-2026" Value="2025-2026"></asp:ListItem>
                                                                    <asp:ListItem Text="2024-2025" Value="2024-2025"></asp:ListItem>
                                                                    <asp:ListItem Text="2023-2024" Value="2023-2024"></asp:ListItem>
                                                                    <asp:ListItem Text="2022-2023" Value="2022-2023"></asp:ListItem>
                                                                    <asp:ListItem Text="2021-2022" Value="2021-2022"></asp:ListItem>
                                                                    <asp:ListItem Text="2020-2021" Value="2020-2021"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="height: 5px" colspan="2">
                                                                <asp:Label ID="lblsckstatus" runat="server" Text="Label" Visible="False"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" colspan="2">
                                                                <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click"
                                                                    CssClass="BTNBLUE" Width="100px" />
                                                                &nbsp;&nbsp;&nbsp;
                                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                                                    CssClass="BTNBLUE" Width="100px" CausesValidation="false" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="height: 5px" colspan="2">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </center>
                                        </fieldset>
                                    </td>
                                </tr>
                                                        <tr>
                                                            <td style="height: 5px" colspan="2">
                                                            </td>
                                                        </tr>                                
                                <tr>
                                    <td style="height: 5px" colspan="4" align="center">
                                        <asp:Button ID="btnAddNew" runat="server" Text="Addnew" OnClick="btnAddNew_Click"
                                            CssClass="BTNBLUE" Width="100px" />
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btn_Close" runat="server" Text="Close" Width="100px" CssClass="BTNBLUE"
                                            OnClick="btn_Close_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:Label ID="Label2" runat="server" Font-Size="X-Small" ForeColor="#400040"></asp:Label>
                                        <asp:Label ID="lblselectedstkcpt" runat="server"  Font-Size="8pt" Visible="false"></asp:Label>
                                        <asp:ValidationSummary ID="stack_sdsvalidations" runat="server" ShowMessageBox="True"
                                            ShowSummary="False" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </center>
                </fieldset>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnUpdate" />
            </Triggers>
        </asp:UpdatePanel>
    </center>
</asp:Content>
