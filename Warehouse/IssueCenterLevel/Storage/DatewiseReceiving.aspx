<%@ Page Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="DatewiseReceiving.aspx.cs" Inherits="IssueCenterLevel_Storage_DatewiseReceiving" Title="Untitled Page" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script type="text/javascript">
        function doMath() {
            var nValue; var amount;
            var taxgrid = document.getElementById('<%=gvchallan.ClientID %>');
            var taxip = taxgrid.getElementsByTagName('input');
            var price = 100;
            nValue = document.getElementById("message").value;
            amount = (nValue * price);
            document.getElementById("lblbagstotal").value = amount;
        }

    </script>

     <script type="text/javascript">
       <!--
    function isNumberKey(evt) {
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode != 46 && charCode > 31
          && (charCode < 48 || charCode > 57))
            return false;

        return true;
    }
    //-->
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table>
     <tr style="background-color: #0bb6e6; height: 25px">
                                                <td colspan="6" align="center">
                                                    <asp:Label ID="Label2" runat="server" Text="Challan Details"
                                                        ForeColor="whitesmoke" Font-Bold="true" Font-Size="12pt"></asp:Label>
                                                </td>
                                            </tr>
<tr>
<td>
    <asp:GridView ID="gvchallan" runat="server" EnableModelValidation="True" OnRowDataBound="gvchallan_RowDataBound" AutoGenerateColumns="False">
        <Columns>
            <asp:BoundField DataField="challan_no" HeaderText="Challan No." />
            <asp:BoundField DataField="No_of_Bags" HeaderText="Rec.Bags" />
            <asp:BoundField DataField="Recd_Qty" HeaderText="Rec.Qty" />
            <asp:BoundField DataField="District_Name" HeaderText="Sending District" />
            <asp:BoundField DataField="DepotName" HeaderText="Sending Branch" />
            <asp:TemplateField HeaderText="Bags">
                <ItemTemplate>
                    <asp:TextBox ID="txtbagsrec" runat="server" Width="70" onkeypress="return isNumberKey(event)" autocomplete="off"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Qty">
                <ItemTemplate>
                    <asp:TextBox ID="txtqtyrec" runat="server" Width="70" onkeypress="return isNumberKey(event)" autocomplete="off"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="WCM No.">
                <ItemTemplate>
                    <asp:TextBox ID="txtwcmno" runat="server" Width="90" autocomplete="off"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Mode of Wgt.">
                <ItemTemplate>
                    <asp:DropDownList ID="ddlmow" runat="server">
                         <asp:ListItem>10%</asp:ListItem>
                                                <asp:ListItem>100%</asp:ListItem>

                    </asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Moisture">
                <ItemTemplate>
                    <asp:TextBox ID="txtmoisture" runat="server" Width="40" autocomplete="off"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Crop Year">
                 <ItemTemplate>
                    <asp:DropDownList ID="ddlcropy" runat="server">
                    </asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Category">

                   <ItemTemplate>
                    <asp:DropDownList ID="ddlgcategory" runat="server">
                         <asp:ListItem>A</asp:ListItem>
                                                <asp:ListItem>B</asp:ListItem>
                         <asp:ListItem>C</asp:ListItem>
                                                <asp:ListItem>D</asp:ListItem>
                         <asp:ListItem>E</asp:ListItem>
                                                <asp:ListItem>F</asp:ListItem>
                        
                    </asp:DropDownList>
                </ItemTemplate>

            </asp:TemplateField>
            <asp:TemplateField HeaderText="Select">

                <ItemTemplate>

                    <asp:CheckBox ID="ckboxtrucklist" runat="server" AutoPostBack="True"  OnCheckedChanged="ckboxtrucklist_CheckedChanged" />
                </ItemTemplate>

            </asp:TemplateField>
            <asp:BoundField DataField="Godown" HeaderText="GodownID" />
            <asp:BoundField DataField="A_Depo" HeaderText="A_Depo" />
            <asp:BoundField DataField="A_Dist" HeaderText="A_Dist" />
            <asp:BoundField DataField="Category" HeaderText="Category" />
            <asp:BoundField DataField="Commodity" HeaderText="Commodity" />
            <asp:BoundField DataField="Transporter" HeaderText="Transporter" />
            <asp:BoundField DataField="arrivaldate" HeaderText="arrivaldate" />
            <asp:BoundField DataField="Vehile_no" HeaderText="VhicleNo" />
        </Columns>
    </asp:GridView>
</td>

</tr>

</table>
<table>
<tr>
<td style="color: navy">
Date of Deposit:
</td>
<td>
    <asp:TextBox ID="txtdateofdepo" runat="server"></asp:TextBox>
    <cc1:CalendarExtender ID="txtdateofdepo_CalendarExtender" runat="server" 
        Enabled="True" TargetControlID="txtdateofdepo" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy">
    </cc1:CalendarExtender>
</td>
<td style="color: navy">
    Commodity:
</td>
<td>
    <asp:Label ID="lblcommodityname" runat="server" Text="Label"></asp:Label>
</td>
<td>
    &nbsp;</td>
<td>
    &nbsp;</td>
</tr>

<tr>
<td style="color: navy">
Total Bags:
</td>
<td>
    <asp:Label ID="lblbagstotal" runat="server" Text="0"></asp:Label>
</td>
<td style="color: navy">
Total Qty:
</td>
<td>
    <asp:Label ID="lblqtytotal" runat="server" Text="0"></asp:Label>
</td>
</tr>
<tr>
                        <td colspan="6" align="center" valign="top">
                            <fieldset style="width: 980px; border: 1px solid navy;">
                                <center>
                                    <div>
                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr style="background-color: #0bb6e6; height: 25px">
                                                <td colspan="6" align="center">
                                                    <asp:Label ID="lblDeliveryOrderOfStock" runat="server" Text="Depositing Details"
                                                        ForeColor="whitesmoke" Font-Bold="true" Font-Size="12pt"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 10px" colspan="6">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" style="width: 150px">
                                                    <asp:Label ID="lblGodownNo" runat="server" Font-Size="8pt" Font-Bold="true" ForeColor="navy"
                                                        Text="Godown No."></asp:Label></td>
                                                <td align="left" style="width: 150px">
                                                    <asp:DropDownList ID="ddlGodownNo" runat="server" AutoPostBack="True" Width="155px"
                                                        Height="25px" TabIndex="18" 
                                                        onselectedindexchanged="ddlGodownNo_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="left" style="width: 150px">
                                                    <asp:Label ID="lblStackNo" runat="server" Font-Size="8pt" Font-Bold="true" ForeColor="navy"
                                                        Text="Stack No."></asp:Label></td>
                                                <td align="left" style="width: 150px">
                                                    <asp:DropDownList ID="ddlStackNo" runat="server" Height="25px" Width="155px" AutoPostBack="True"
                                                        TabIndex="19" OnSelectedIndexChanged="ddlStackNo_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="left" style="width: 150px">
                                                    <asp:Label ID="lblStackBags" runat="server" Font-Size="8pt" Font-Bold="true" ForeColor="navy"
                                                        Text="No.of Bags"></asp:Label></td>
                                                <td align="left" style="width: 150px">
                                                    <asp:TextBox ID="txtStackBags" runat="server" MaxLength="20" Width="150px" onblur="Spc_validator(this)"
                                                        TabIndex="20"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="height: 10px" colspan="6">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" style="width: 150px">
                                                    <asp:Label ID="lblStackWt" runat="server" Font-Size="8pt" Font-Bold="true" ForeColor="navy"
                                                        Text="Weight"></asp:Label></td>
                                                <td align="left" style="width: 150px">
                                                    <asp:TextBox ID="txtStackWt" runat="server" MaxLength="20" Width="150px" onkeyup="NumericDecimalCheck(this,5)"
                                                        onblur="compare()" TabIndex="21"></asp:TextBox></td>
                                                <td align="left">
                                                    <asp:Label ID="lblStackMaxCap" runat="server" Font-Size="8pt" Font-Bold="true" ForeColor="navy"
                                                        Text="Maximum Capacity"></asp:Label></td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtStackMaxCap" runat="server" MaxLength="20" Width="150px" BackColor="#FFFFC0"
                                                        Enabled="False"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    &nbsp;</td>
                                                <td align="left">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td style="height: 10px" colspan="6">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lblStackCurrentCapacity" runat="server" Font-Size="8pt" Font-Bold="true"
                                                        ForeColor="navy" Text="Current Capacity"></asp:Label></td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtStackCurrentCapacity" runat="server" MaxLength="20" Width="150px"
                                                        BackColor="#FFFFC0" Enabled="False"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="lblStackAvailable" runat="server" Font-Size="8pt" Font-Bold="true"
                                                        ForeColor="navy" Text="Available Capacity"></asp:Label></td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtStackAvailable" runat="server" MaxLength="20" Width="150px" BackColor="#FFFFC0"
                                                        Enabled="False"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtArrivalSrcId" runat="server" Width="155px" Enabled="False"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 10px" colspan="6">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="6">
                                                    <asp:Label ID="lblStackingInform" runat="server" Font-Size="8pt" ForeColor="red"
                                                        Font-Bold="true" Text="(Click the Add Stack Button to save the Stacking Information) "></asp:Label>
                                                    <asp:Button ID="btnAddStack" runat="server" TabIndex="22" Text="Add Stack" Width="100px"
                                                        CssClass="BTNBLUE" onclick="btnAddStack_Click"  /></td>
                                            </tr>
                                             <tr>
                                                <td style="height: 10px" colspan="6">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="6">
                                                    <asp:GridView ID="gdstackingdetails" runat="server" AutoGenerateDeleteButton="True"
                                                        CellPadding="4" ForeColor="#333333" GridLines="None" OnPreRender="gdstackingdetails_PreRender"
                                                        OnRowCreated="gdstackingdetails_RowCreated" OnRowDeleting="gdstackingdetails_RowDeleting">
                                                        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                                        <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                                                        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                                                        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                                                        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                                        <AlternatingRowStyle BackColor="White" />
                                                    </asp:GridView>
                                                    <asp:GridView ID="gdEditStackingDetails" runat="server" AutoGenerateDeleteButton="True"
                                                        AutoGenerateEditButton="true" CellPadding="4" ForeColor="#333333" GridLines="None"
                                                        OnPreRender="gdEditStackingDetails_PreRender" OnRowCreated="gdEditStackingDetails_RowCreated"
                                                        OnRowDeleting="gdEditStackingDetails_RowDeleting" OnRowEditing="gdEditStackingDetails_RowEditing">
                                                        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                                        <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                                                        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                                                        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                                                        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                                        <AlternatingRowStyle BackColor="White" />
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </center>
                            </fieldset>
                        </td>
                    </tr>
                    
                    <tr>
                        <td colspan="6" align="center" valign="top">
                            <fieldset style="width: 980px; border: 1px solid navy;">
                                <center>
                                    <div>
                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr>
                                                <td align="left" valign="top" style="width: 200px">
                                                    <asp:Label ID="lblRemarks" runat="server" Font-Size="8pt" ForeColor="navy" Font-Bold="true"
                                                        Text="Remarks (If Any)"></asp:Label></td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtRemarks" runat="server" Height="50px" MaxLength="250" TabIndex="23"
                                                        TextMode="MultiLine" Width="500px"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="height: 10px" colspan="2">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" align="center">
                                                    <asp:Button ID="btnsave" runat="server" Text="Save Details" Width="120px" CssClass="BTNBLUE"
                                                        TabIndex="24" ValidationGroup="GD_Stack,Non" Enabled="False" OnClick="btnsave_Click" OnClientClick="this.disabled = true; this.value='Please wait...'" UseSubmitBehavior="false" />
                                                    &nbsp; &nbsp; &nbsp;
                                                    &nbsp; &nbsp; &nbsp;
                                                    <asp:Button ID="btn_Close" runat="server" Text="Close" Width="120px" CssClass="BTNBLUE"
                                                        CausesValidation="false" OnClick="btn_Close_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 10px" colspan="6">
                                                </td>
                                            </tr>
                                            <tr id="trlnk" runat="server" visible="false" align="right">
                                                <td colspan="2" align="center">
                                                   
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </center>
                            </fieldset>
                        </td>
                    </tr>
</table>
</asp:Content>

