<%@ Page Language="C#" MasterPageFile="~/MasterPage/StateMaster.master" AutoEventWireup="true"
    CodeFile="Delete_Receipt_Details.aspx.cs" Inherits="Region_Delete_Receipt_Details"
    Title="Delete Receipt Details::" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<%--<style type="text/css">
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
<script type="text/javascript">
    var TotalChkBx;
    var Counter;

    window.onload = function () {
        //Get total no. of CheckBoxes in side the GridView.
        TotalChkBx = parseInt('<%= this.gv.Rows.Count %>');

        //Get total no. of checked CheckBoxes in side the GridView.
        Counter = 0;
    }

    function HeaderClick(CheckBox) {
        //Get target base & child control.
        var TargetBaseControl =
       document.getElementById('<%= this.gv.ClientID %>');
        var TargetChildControl = "chk_Delete";

        //Get all the control of the type INPUT in the base control.
        var Inputs = TargetBaseControl.getElementsByTagName("input");

        //Checked/Unchecked all the checkBoxes in side the GridView.
        for (var n = 0; n < Inputs.length; ++n)
            if (Inputs[n].type == 'checkbox' &&
                Inputs[n].id.indexOf(TargetChildControl, 0) >= 0)
                Inputs[n].checked = CheckBox.checked;

        //Reset Counter
        Counter = CheckBox.checked ? TotalChkBx : 0;
    }

    function ChildClick(CheckBox, HCheckBox) {
        //get target control.
        var HeaderCheckBox = document.getElementById(HCheckBox);

        //Modifiy Counter; 
        if (CheckBox.checked && Counter < TotalChkBx)
            Counter++;
        else if (Counter > 0)
            Counter--;

        //Change state of the header CheckBox.
        if (Counter < TotalChkBx)
            HeaderCheckBox.checked = false;
        else if (Counter == TotalChkBx)
            HeaderCheckBox.checked = true;
    }
</script>

<asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
    <ProgressTemplate>
     <div class="divWaiting">            
	<asp:Label ID="lblWait" runat="server" 
	Text=" Please wait... " />
	<asp:Image ID="imgWait" runat="server" 
	ImageAlign="Middle" ImageUrl="~/images/mpwlc3.gif" />
  </div>
    
    </ProgressTemplate>
    </asp:UpdateProgress>
    <fieldset style="width: 1000px; border: 2px solid navy;">
        <center>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div>
                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td colspan="6" align="center" valign="top">
                                    <fieldset style="width: 980px; border: 1px solid navy;">
                                        <center>
                                            <div>
                                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                    <tr style="background-color: #0bb6e6; height: 25px">
                                                        <td colspan="4" align="center">
                                                            <span style="color: White; font-size: 12pt; font-weight: bold">Delete Receipt Details</span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 10px" colspan="4">
                                                            <asp:RadioButton ID="RadioButton1" runat="server" AutoPostBack="True" 
                                                                Checked="True" GroupName="rr" oncheckedchanged="RadioButton1_CheckedChanged" 
                                                                Text="Godown Wise" />
                                                            <asp:RadioButton ID="RadioButton2" Visible="false" runat="server" AutoPostBack="True" 
                                                                GroupName="rr" oncheckedchanged="RadioButton2_CheckedChanged" 
                                                                Text="Branch Wise" />
                                                            &nbsp;
                                                            <asp:Label ID="Label1" runat="server" Font-Bold="True" ForeColor="Red" 
                                                                Text="एक साथ सारी रिसीविंग डिलीट करने के लिए ब्रांच select करके डिलीट बटन पर क्लिक करें।"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4" style="height: 10px">
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 150px" align="left">
                                                            <asp:Label ID="lblDistrict" runat="server" Text="District" Font-Size="10pt" Font-Bold="true"></asp:Label>
                                                        </td>
                                                        <td style="width: 150px" align="left">
                                                            <asp:DropDownList ID="ddlDistrict" runat="server" Height="25px" Width="155px" AutoPostBack="True"
                                                                OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged" CssClass="tb6">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="width: 100px" align="left">
                                                            <asp:Label ID="lblBranch" runat="server" Text="Branch" Font-Size="10pt" Font-Bold="true"></asp:Label>
                                                        </td>
                                                        <td style="width: 200px" align="left">
                                                            <asp:DropDownList ID="ddlDepotList" runat="server" Height="25px" Width="155px" AutoPostBack="True"
                                                                CssClass="tb6" OnSelectedIndexChanged="ddlDepotList_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </center>
                                    </fieldset>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 5px" colspan="4">
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" align="right">
                                    <asp:Label ID="lblRowCount" runat="server" ForeColor="navy" Text="Total Record "
                                        Font-Size="10pt" Font-Bold="true"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 5px" colspan="4">
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" align="center">
                                    <asp:Label ID="lbl_notfound" runat="server" Text="District" Font-Bold="True" Font-Size="15pt"
                                        ForeColor="red" Visible="false"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" align="center" valign="top">
                                    <asp:Panel ID="panelContainer" runat="server" Height="380px" ScrollBars="Vertical"
                                        Width="100%" BorderColor="navy" BorderWidth="1px">
                                        <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" GridLines="both"
                                            DataKeyNames="ArrivalStock_Id" Width="100%" Font-Size="9pt" 
                                            CellPadding="4" onrowcreated="gv_RowCreated" AllowPaging="True" 
                                            AllowSorting="True" onpageindexchanging="gv_PageIndexChanging">
                                            <Columns>
                                                 <asp:TemplateField HeaderText="Select">
                                                     <HeaderTemplate>
                                                          <asp:CheckBox ID="chkBxHeader" 
                 onclick="javascript:HeaderClick(this);" runat="server" />
                                                     </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chk_Delete" runat="server" />
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" Font-Names="Arial" Font-Size="12pt" HorizontalAlign="Center"
                                                        Width="80px" />
                                                    <ItemStyle HorizontalAlign="Center" Width="10px" />
                                                    <ControlStyle Width="15px" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Challan_No" HeaderText="TC No.">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Truck_No" HeaderText="Truck No.">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Acpt_FCIRO_No" HeaderText="Acceptance No.">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="AcceptDate" HeaderText="Acceptance Date">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                 <asp:BoundField DataField="DepositDate" HeaderText="Deposit Date">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Bags" HeaderText="Bags">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Weight" HeaderText="Quantity(In QTLS.)">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                            </Columns>
                                            <FooterStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Transparent" />
                                            <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                                            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                                            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                                            <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                                                Height="20px" Font-Size="10pt" />
                                            <AlternatingRowStyle BackColor="White" />
                                        </asp:GridView>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 15px" colspan="4">
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="4">
                                    <asp:Button ID="Btn_Delete" runat="server" Text="Delete Record" Width="100px" OnClick="Btn_Delete_Click"
                                        CssClass="BTNBLUE" />
                                    <asp:Button ID="btn_Close" runat="server" Text="Close" Width="100px" OnClick="btn_Close_Click"
                                        CssClass="BTNBLUE" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </center>
    </fieldset>
</asp:Content>
