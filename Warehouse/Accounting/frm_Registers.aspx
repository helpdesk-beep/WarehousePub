<%@ Page Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="frm_Registers.aspx.cs" Inherits="Accounting_frm_Reservation_Register" Title="Register" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript">
////////On Pass amount//////
     function amtcalculate() {


        var PassAmt = document.getElementById('<%= txtPAmt.ClientID %>');
        var TD = document.getElementById('<%= txtTD.ClientID %>');

        document.getElementById('<%= txtNetAmt.ClientID %>').value = (PassAmt.value - TD.value);
    }
    ////////On Deduction amount//////
        function Deductionamtcalculate() {

            var PassAmt = "0";
            var NetAmt = "0";
            var TDS = "0";
            var SD = "0";
            var Resources = "0";
            var Other = "0";
         
            PassAmt = document.getElementById('<%= txtPAmt.ClientID %>');
            NetAmt = document.getElementById('<%= txtNetAmt.ClientID %>');
            
            TDS = document.getElementById('<%= txtTDS.ClientID %>');
            SD = document.getElementById('<%= txtSD.ClientID %>');
            Resources = document.getElementById('<%= txtResources.ClientID %>');
            Other = document.getElementById('<%= txtOther.ClientID %>');

            var TotalD = findNull(TDS) + findNull(SD) + findNull(Resources) + findNull(Other);
            document.getElementById('<%= txtTD.ClientID %>').value = TotalD;
            document.getElementById('<%= txtNetAmt.ClientID %>').value = (PassAmt.value - TotalD);

       }
    /////////////////////Null function///////////////////////
    function findNull(x) {
        
        var TDS = x.value;
        if (TDS == "") {
            TDS = 0;
//            alert(TDS);
        }
        else {
            TDS = parseFloat(TDS);
        }
        return TDS;
//        alert(TDS+1);
    } 
</script>
    <style type="text/css">
        .style1
        {
            height: 22px;
          
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<fieldset  style=" width:1000px; border:2px solid navy">
<center>
<div>
<table width="1000px">
<tr id="msg">
<td colspan="6"><asp:Label ID="lblmsg" Text="" ForeColor="red" Font-Bold="true" runat="server"></asp:Label></td>
</tr>
<tr style="background-color: #0bb6e6; height: 25px">
 <td colspan="6" align="center">
            <asp:Label ID="lblheading" runat="server" Font-Bold="True" Font-Size="15pt" ForeColor="WhiteSmoke"
                                        Text="Registers"></asp:Label></td>
</tr>
<tr align="center">
<td>
     <asp:Label ID="lblcropyear" runat="server" Font-Bold="True" Font-Size="8pt" 
                                                                ForeColor="Navy" Text="Financial Year:"></asp:Label>
    </td>
<td valign="middle"> 
       <asp:DropDownList ID="ddlcropyr" runat="server" AutoPostBack="True" 
           TabIndex="1" Height="25px" Width="200px" Font-Size="10pt" 
           onselectedindexchanged="ddlcropyr_SelectedIndexChanged" >
                                                            </asp:DropDownList></td>
<td>
 <asp:Label ID="lblRegType" Font-Size="8pt" Font-Bold="true" ForeColor="navy" runat="server" Text="Register Type"></asp:Label>
</td>
<td>
    <asp:DropDownList ID="ddlRegType" runat="server" AutoPostBack="True"
        TabIndex="1" Height="25px" Width="200px" Font-Size="10pt" 
        onselectedindexchanged="ddlRegType_SelectedIndexChanged" >
        <asp:ListItem Value="0">--Select--</asp:ListItem>
        <asp:ListItem Value="1">Reservation Register</asp:ListItem>
        <asp:ListItem Value="2">Hired Godown Register</asp:ListItem>
    </asp:DropDownList></td>
    
</tr>
    <tr id="trReservationRegister" visible="false" runat="server">
<td colspan="4">
<fieldset style="width: 980px; border: 1px solid navy;">
                                                    <center>
                                                           <div style="overflow: scroll; height: 200px; overflow-x: hidden">
                                                           <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                           <tr>
                                                           <td colspan="6" align="center"  style="background-color: #0bb6e6; height: 25px">
                                                           <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="15pt" ForeColor="WhiteSmoke"
                                        Text="Reservation Register"></asp:Label>
                                                           </td>
                                                           </tr>
                                                           
                                                           <tr>
<td> 
    <asp:Label ID="lbltod" Font-Size="8pt" Font-Bold="true" ForeColor="navy" runat="server" Text="Type Of Depositor"></asp:Label>
    </td>
<td valign="middle"> 
    <asp:DropDownList ID="ddldepositor" runat="server" AutoPostBack="True" 
        TabIndex="1" Height="25px" Width="200px" Font-Size="10pt" 
        onselectedindexchanged="ddldepositor_SelectedIndexChanged">
    </asp:DropDownList></td>
<td> 
    <asp:Label Font-Size="8pt" Font-Bold="true" ForeColor="navy" ID="Label8" runat="server" Text="Name of Depositor"></asp:Label></td>
    <td>
        <asp:DropDownList ID="ddldepos_name" runat="server" TabIndex="1" Height="25px" 
            Width="200px" Font-Size="10pt"
            AutoPostBack="True" 
            onselectedindexchanged="ddldepos_name_SelectedIndexChanged">
        </asp:DropDownList></td>
</tr>
<tr>
        <td class="style1">
            <asp:Label ID="lblcc" runat="server" Text="Commodity Type" Font-Bold="True" Font-Size="8pt" 
                                                                ForeColor="Navy"></asp:Label></td>
        <td class="style1"><asp:DropDownList ID="ddlverity" runat="server" AutoPostBack="True" 
                TabIndex="1" Height="25px" Width="200px" Font-Size="10pt" onselectedindexchanged="ddlverity_SelectedIndexChanged" 
                >
            </asp:DropDownList>
            </td>
        <td class="style1">
            <asp:Label ID="lblcommodity" runat="server" Text="Commodity Name" Font-Bold="True" Font-Size="8pt" 
                                                                ForeColor="Navy"></asp:Label></td>
        <td class="style1"><asp:DropDownList ID="ddlcomodity" runat="server" Width="200px" Height="25px" 
                AutoPostBack="True" 
                onselectedindexchanged="ddlcomodity_SelectedIndexChanged">
            </asp:DropDownList>
            </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblFromDate" runat="server" Text="Reserve From Date" Font-Bold="True" Font-Size="8pt" 
                                                                ForeColor="Navy"></asp:Label></td>
        <td>
            <asp:TextBox ID="txtfdate" runat="server" BackColor="LemonChiffon" AutoPostBack="true"
        TabIndex="9" CssClass="tb6" Width="190px" Height="20px" 
           ></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender1" runat="server"  Format="dd/MM/yyyy"
        TargetControlID="txtfdate"></cc1:CalendarExtender>
            </td>
        <td>
            <asp:Label ID="lblTodate" runat="server" Text="Reserve UpTo Date" Font-Bold="True" Font-Size="8pt" 
                                                                ForeColor="Navy"></asp:Label></td>
        <td>
        <asp:TextBox ID="txttodate" runat="server" AutoPostBack="true" BackColor="LemonChiffon" 
        TabIndex="9" CssClass="tb6" Width="190px" Height="20px" 
          ></asp:TextBox>
         <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
        TargetControlID="txttodate"></cc1:CalendarExtender>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label Font-Size="8pt" Font-Bold="true" ForeColor="navy"  ID="lblpacking" runat="server" Text="Packing Type"></asp:Label></td>
        <td>
            <asp:DropDownList ID="ddlpacktype" runat="server" TabIndex="1" Height="25px"  AutoPostBack="true"
                Width="200px" Font-Size="10pt" onselectedindexchanged="ddlpacktype_SelectedIndexChanged" 
                >
            </asp:DropDownList></td>
            <td>
            <asp:Label Font-Size="8pt" Font-Bold="true" ForeColor="navy"  ID="lblweight" runat="server" Text="Weight"></asp:Label></td>
        <td>
            <asp:DropDownList ID="ddlweight" runat="server" TabIndex="1" Height="25px" AutoPostBack="true"
            Width="200px" Font-Size="10pt" onselectedindexchanged="ddlweight_SelectedIndexChanged" 
               >
            </asp:DropDownList></td>
    </tr>
    <tr>
    <td>
<asp:Label ID="lblrunit" runat="server" Visible="true" Text="No of Quantity to Reserve" Font-Bold="True" Font-Size="8pt" 
                                                                ForeColor="Navy"></asp:Label>
</td>
<td>
<asp:TextBox runat="server" ID="txtrunit" Visible="true" ReadOnly="false" 
        AutoPostBack="true" BackColor="LemonChiffon" 
        TabIndex="9" Width="190px" Height="20px" 
        ></asp:TextBox>
         <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtrunit"
                                        ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender>
</td>
<td>
            <asp:Label ID="lblConfirmBy" Visible="true" runat="server" Text="Confirm By MPWLC" Font-Size="8pt" Font-Bold="true" ForeColor="navy"></asp:Label></td>
        <td>
            <asp:DropDownList Enabled="true" ID="ddlConfirmBy" runat="server" Visible="true" 
                 TabIndex="1" Height="25px" Width="200px" Font-Size="10pt" 
                 >
                <asp:ListItem Value="-1">--Select--</asp:ListItem>
                <asp:ListItem Value="0">HO</asp:ListItem>
                <asp:ListItem Value="1">RO</asp:ListItem>
                <asp:ListItem Value="2">BO</asp:ListItem>
            </asp:DropDownList></td>
    </tr>
    <tr>
    <td>
<asp:Label ID="lbldltr" runat="server" Visible="true" Text="Reservation Demand Letter No./Date" Font-Bold="True" Font-Size="8pt" 
                                                                ForeColor="Navy"></asp:Label>
</td>
<td>
<asp:TextBox runat="server" ID="txtDltr" Visible="true" ReadOnly="false" placeholder="Letter No."
        AutoPostBack="false" BackColor="LemonChiffon" 
        TabIndex="9" Width="90px" Height="20px" 
        ></asp:TextBox>
         <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtDltr"
                                        ValidChars="0123456789.-/">
                                    </cc1:FilteredTextBoxExtender>
 <asp:TextBox runat="server" ID="txtDltrd" Visible="true" ReadOnly="false" placeholder="Date"
        AutoPostBack="false" BackColor="LemonChiffon" 
        TabIndex="9" Width="90px" Height="20px" 
        ></asp:TextBox>
         <cc1:CalendarExtender ID="CalendarExtender11" runat="server"  Format="dd/MM/yyyy"
        TargetControlID="txtDltrd"></cc1:CalendarExtender>
         <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender14" runat="server" TargetControlID="txtDltrd"
                                        ValidChars="0123456789.-/">
                                    </cc1:FilteredTextBoxExtender>
</td>
<td>
            <asp:Label ID="lblcltr" Visible="true" runat="server" Text="MPWLC Confirmation Letter No./Date" Font-Size="8pt" Font-Bold="true" ForeColor="navy"></asp:Label></td>
        <td>
         <asp:TextBox runat="server" ID="txtCltr" Visible="true" ReadOnly="false" placeholder="Letter No." 
        AutoPostBack="false" BackColor="LemonChiffon" 
        TabIndex="9" Width="90px" Height="20px" 
        ></asp:TextBox>
         <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtCltr"
                                        ValidChars="0123456789.-/">
                                    </cc1:FilteredTextBoxExtender>
                                      <asp:TextBox runat="server" ID="txtCltrd" Visible="true" ReadOnly="false" placeholder="Date"
        AutoPostBack="false" BackColor="LemonChiffon" 
        TabIndex="9" Width="90px" Height="20px" 
        ></asp:TextBox>
          <cc1:CalendarExtender ID="CalendarExtender12" runat="server"  Format="dd/MM/yyyy"
        TargetControlID="txtCltrd"></cc1:CalendarExtender>
         <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender15" runat="server" TargetControlID="txtCltrd"
                                        ValidChars="0123456789.-/">
                                    </cc1:FilteredTextBoxExtender>
                                    </td>
    </tr>
    
                                                           </table>
                                                             
                                                            </div>
                                                            <table width="100%">
                                                            <tr>
                                                            <td align="center" colspan="2">
                                                            <asp:Button ID="btnRSubmit" runat="server" Text="Submit" Visible="true" 
                                                                CssClass="BTNBLUE" onclick="btnRSubmit_Click" />
                                                                <asp:Button ID="btnRCancel" runat="server" Text="Cancel" Visible="true"
                                                                CssClass="BTNBLUE" onclick="btnRCancel_Click" />
                                                            </td>
                                                           
                                                            </tr>
                                                            </table>
                                                            </center>
                                                            </fieldset>
                                                            </td>
</tr>
<tr id="trHiredGodownRegister" visible="false" runat="server">
<td colspan="4">
<fieldset style="width: 980px; border: 1px solid navy;">
                                                    <center>
                                                           <div style="overflow: scroll; height: 350px; overflow-x: hidden">
                                                           <table cellpadding="2" cellspacing="0" style="width: 100%">
                                                           <tr>
                                                           <td colspan="6" align="center"  style="background-color: #0bb6e6; height: 25px">
                                                           <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="15pt" ForeColor="WhiteSmoke"
                                        Text="Hired Godown Register"></asp:Label>
                                                           </td>
                                                           </tr>
                                                           <tr>
               <td>
           <asp:Label Font-Size="8pt" Font-Bold="true" ForeColor="navy" ID="lblgodown" runat="server" Text="Godown"></asp:Label>
            </td>
            <td>
            <asp:DropDownList ID="ddlgodown" runat="server" AutoPostBack="True" TabIndex="1" 
                    Height="25px" Width="200px" CssClass="tb6" Font-Size="10pt" onselectedindexchanged="ddlgodown_SelectedIndexChanged"
               >
            </asp:DropDownList></td>
            <td>
            <asp:Label ID="lblgdwno" Visible="true" runat="server" Text="Godown No." Font-Size="8pt" Font-Bold="true" ForeColor="navy"></asp:Label></td>
        <td>
         <asp:TextBox runat="server" ID="txtgno" Visible="true" ReadOnly="false" 
        AutoPostBack="false" BackColor="LemonChiffon" 
        TabIndex="9" Width="190px" Height="20px"  
        ></asp:TextBox>
 </td>
                                                        </tr>
                                                           <tr>
    <td>
<asp:Label ID="lblnow" runat="server" Visible="true" Text="Name of the Owner" Font-Bold="True" Font-Size="8pt" 
                                                                ForeColor="Navy"></asp:Label>
</td>
<td>
<asp:TextBox runat="server" ID="txtnow" Visible="true" ReadOnly="false" 
        AutoPostBack="false" BackColor="LemonChiffon" 
        TabIndex="9" Width="190px" Height="20px" 
        ></asp:TextBox>
</td>
<td>
            <asp:Label ID="lbladd" Visible="true" runat="server" Text="Address." Font-Size="8pt" Font-Bold="true" ForeColor="navy"></asp:Label></td>
        <td>
         <asp:TextBox runat="server" ID="txtaddress" Visible="true" ReadOnly="false"
        AutoPostBack="false" BackColor="LemonChiffon" 
        TabIndex="9" Width="190px" Height="20px" TextMode="MultiLine" 
        ></asp:TextBox>
 </td>
    </tr>
 <tr>
    <td>
<asp:Label ID="lblmob" runat="server" Visible="true" Text="Contact No." Font-Bold="True" Font-Size="8pt" 
                                                                ForeColor="Navy"></asp:Label>
</td>
<td>
<asp:TextBox runat="server" ID="txtMob" Visible="true" ReadOnly="false" 
        AutoPostBack="false" BackColor="LemonChiffon" 
        TabIndex="9" Width="190px" Height="20px" 
        ></asp:TextBox>
</td>
<td>
            <asp:Label ID="lblEmail" Visible="true" runat="server" Text="Email Add." Font-Size="8pt" Font-Bold="true" ForeColor="navy"></asp:Label></td>
        <td>
         <asp:TextBox runat="server" ID="txtEmail" Visible="true" ReadOnly="false"
        AutoPostBack="false" BackColor="LemonChiffon" 
        TabIndex="9" Width="190px" Height="20px" TextMode="SingleLine"
        ></asp:TextBox>
 </td>
    </tr>                                                          
    <tr>
    <td>
<asp:Label ID="lblPan" runat="server" Visible="true" Text="PAN No." Font-Bold="True" Font-Size="8pt" 
                                                                ForeColor="Navy"></asp:Label>
</td>
<td>
<asp:TextBox runat="server" ID="txtPan" Visible="true" ReadOnly="false" 
        AutoPostBack="false" BackColor="LemonChiffon" 
        TabIndex="9" Width="190px" Height="20px" 
        ></asp:TextBox>
</td>
<td>
            <asp:Label ID="lblBank" Visible="true" runat="server" Text="Name of Bank" Font-Size="8pt" Font-Bold="true" ForeColor="navy"></asp:Label></td>
        <td>
         <asp:DropDownList ID="ddlBank" runat="server" AutoPostBack="false" TabIndex="1" 
                    Height="25px" Width="200px" CssClass="tb6" Font-Size="10pt"
               >
            </asp:DropDownList>
 </td>
    </tr>
    <tr>
    <td>
<asp:Label ID="lblBranch" runat="server" Visible="true" Text="Bank Branch Add." Font-Bold="True" Font-Size="8pt" 
                                                                ForeColor="Navy"></asp:Label>
</td>
<td>
<asp:TextBox runat="server" ID="txtBAdd" Visible="true" ReadOnly="false" 
        AutoPostBack="false" BackColor="LemonChiffon" 
        TabIndex="9" Width="190px" Height="20px" 
        ></asp:TextBox>
</td>
<td>
            <asp:Label ID="lblAno" Visible="true" runat="server" Text="Account No." Font-Size="8pt" Font-Bold="true" ForeColor="navy"></asp:Label></td>
        <td>
         <asp:TextBox runat="server" ID="txtAcc" Visible="true" ReadOnly="false"
        AutoPostBack="false" BackColor="LemonChiffon" 
        TabIndex="9" Width="190px" Height="20px" TextMode="SingleLine"
        ></asp:TextBox>
 </td>
    </tr>
    <tr>
    <td>
            <asp:Label ID="lblIfsc" Visible="true" runat="server" Text="IFSC Code." Font-Size="8pt" Font-Bold="true" ForeColor="navy"></asp:Label></td>
        <td>
         <asp:TextBox runat="server" ID="txtIfsc" Visible="true" ReadOnly="false" 
        AutoPostBack="false" BackColor="LemonChiffon" 
        TabIndex="9" Width="190px" Height="20px" 
        ></asp:TextBox>
 </td>
<td>
<asp:Label ID="lblscapacity" runat="server" Visible="true" Text="Storage Capacity(In MT)" Font-Bold="True" Font-Size="8pt" 
                                                                ForeColor="Navy"></asp:Label>
</td>
<td>
<asp:TextBox runat="server" ID="txtscapacity" Visible="true" ReadOnly="true" 
        AutoPostBack="false" BackColor="LemonChiffon" 
        TabIndex="9" Width="190px" Height="20px" 
        ></asp:TextBox>
         <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtscapacity"
                                        ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender>
</td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblrent" runat="server" Text="Rent Per Month" Font-Bold="True" Font-Size="8pt" 
                                                                ForeColor="Navy"></asp:Label></td>
        <td>
        <asp:TextBox ID="txtrent" runat="server" BackColor="LemonChiffon" 
        TabIndex="9" CssClass="tb6" Width="190px" Height="20px" 
          ></asp:TextBox>
           <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="txtrent"
                                        ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender>
        </td>
        <td>
            <asp:Label ID="lblsond" runat="server" Text="RO Sanction Order No. & Date" Font-Bold="True" Font-Size="8pt" 
                                                                ForeColor="Navy"></asp:Label></td>
        <td>  <asp:TextBox runat="server" ID="txtson" Visible="true" ReadOnly="false" placeholder="Letter No."
        AutoPostBack="false" BackColor="LemonChiffon" 
        TabIndex="9" Width="90px" Height="20px" 
        ></asp:TextBox>
         <asp:TextBox ID="txtsond" runat="server" BackColor="LemonChiffon" placeholder="Date"
        TabIndex="9" CssClass="tb6" Width="90px" Height="20px" 
           ></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender6" runat="server"  Format="dd/MM/yyyy"
        TargetControlID="txtsond"></cc1:CalendarExtender>
            </td>
    </tr>
<tr>
        
        <td>
            <asp:Label ID="lbldvon" runat="server" Text="Date of Taking over Possession" Font-Bold="True" Font-Size="8pt" 
                                                                ForeColor="Navy"></asp:Label></td>
        <td>
            <asp:TextBox ID="txtdvon" runat="server" BackColor="LemonChiffon"
        TabIndex="9" CssClass="tb6" Width="190px" Height="20px" 
           ></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender5" runat="server"  Format="dd/MM/yyyy"
        TargetControlID="txtdvon"></cc1:CalendarExtender>
            </td>
            <td>
            <asp:Label ID="Label5" Visible="true" runat="server" Text="Date of Vacation" Font-Size="8pt" Font-Bold="true" ForeColor="navy"></asp:Label></td>
        <td>
     <asp:TextBox ID="txtVacDate" runat="server" BackColor="LemonChiffon"
        TabIndex="9" CssClass="tb6" Width="190px" Height="20px" 
           ></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender4" runat="server"  Format="dd/MM/yyyy"
        TargetControlID="txtVacDate"></cc1:CalendarExtender>
 </td>
    </tr>
    
    <tr>
<td>
            <asp:Label ID="Label4" runat="server" Text="RO Vacation Order No. & Date" Font-Bold="True" Font-Size="8pt" 
                                                                ForeColor="Navy"></asp:Label></td>
        <td>  <asp:TextBox runat="server" ID="txtvon" Visible="true" ReadOnly="false" placeholder="Letter No."
        AutoPostBack="false" BackColor="LemonChiffon" 
        TabIndex="9" Width="90px" Height="20px" 
        ></asp:TextBox>
         <asp:TextBox ID="txtvond" runat="server" BackColor="LemonChiffon" placeholder="Date"
        TabIndex="9" CssClass="tb6" Width="90px" Height="20px" 
           ></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender3" runat="server"  Format="dd/MM/yyyy"
        TargetControlID="txtvond"></cc1:CalendarExtender>
            </td>
            <td>
<asp:Label ID="Label3" runat="server" Visible="true" Text="Remark" Font-Bold="True" Font-Size="8pt" 
                                                                ForeColor="Navy"></asp:Label>
</td>
<td>
<asp:TextBox runat="server" ID="txtremark" Visible="true" ReadOnly="false" 
        AutoPostBack="false" BackColor="LemonChiffon" 
        TabIndex="9" Width="190px" Height="20px" MaxLength="100" TextMode="MultiLine"
        ></asp:TextBox>
        
</td>

    </tr>
    
                                                           </table>
                                                             
                                                            </div>
                                                            <table width="100%">
                                                            <tr>
                                                            <td align="center" colspan="2">
                                                            <asp:Button ID="btnSHired" runat="server" Text="Submit" Visible="true" 
                                                                CssClass="BTNBLUE" onclick="btnSHired_Click" />
                                                                <asp:Button ID="btnCHired" runat="server" Text="Cancel" Visible="true" 
                                                                CssClass="BTNBLUE" onclick="btnCHired_Click" />
                                                            </td>
                                                           
                                                            </tr>
                                                            </table>
                                                            </center>
                                                            </fieldset>
                                                            </td>
</tr>
<tr id="trHiredGodownPayment" visible="false" runat="server">
<td colspan="4">
<fieldset style="width: 980px; border: 1px solid navy;">
                                                    <center>
                                                           <div style="overflow: scroll; height: 270px; overflow-x: hidden">
                                                           <table cellpadding="2" cellspacing="0" style="width: 100%">
                                                           <tr>
                                                           <td colspan="4" align="center"  style="background-color: #0bb6e6; height: 25px">
                                                           <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Size="15pt" ForeColor="WhiteSmoke"
                                        Text="Hired Godown Payment Detail"></asp:Label>
                                                           </td>
                                                           </tr>
                                                           <tr>
                                                           <td>
            <asp:Label ID="Label7" runat="server" Text="From Date" Font-Bold="True" Font-Size="8pt" 
                                                                ForeColor="Navy"></asp:Label></td>
        <td>
            <asp:TextBox ID="txtbFrom" runat="server" BackColor="LemonChiffon" AutoPostBack="true"
        TabIndex="9" CssClass="tb6" Width="190px" Height="20px" ontextchanged="txtbFrom_TextChanged"
              ></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender7" runat="server"  Format="dd/MM/yyyy"
        TargetControlID="txtbFrom"></cc1:CalendarExtender>
            </td>
        <td>
            <asp:Label ID="Label9" runat="server" Text="To Date" Font-Bold="True" Font-Size="8pt" 
                                                                ForeColor="Navy"></asp:Label></td>
        <td>
        <asp:TextBox ID="txtbTo" runat="server" AutoPostBack="true" BackColor="LemonChiffon" 
        TabIndex="9" CssClass="tb6" Width="190px" Height="20px" ontextchanged="txtbTo_TextChanged" 
               ></asp:TextBox>
         <cc1:CalendarExtender ID="CalendarExtender8" runat="server" Format="dd/MM/yyyy"
        TargetControlID="txtbTo"></cc1:CalendarExtender>
        </td>
                                                           </tr>
                                                           <tr>
                                                           <td>
            <asp:Label ID="Label10" runat="server" Text="Amount Claimed" Font-Bold="True" Font-Size="8pt" 
                                                                ForeColor="Navy"></asp:Label></td>
        <td>
        <asp:TextBox ID="txtCAmt" runat="server" BackColor="LemonChiffon" 
        TabIndex="9" CssClass="tb6" Width="190px" Height="20px" 
          ></asp:TextBox>
           <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="txtCAmt"
                                        ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender>
        </td>
        <td>
            <asp:Label ID="Label11" runat="server" Text="Amount Passed" Font-Bold="True" Font-Size="8pt" 
                                                                ForeColor="Navy"></asp:Label></td>
        <td>
        <asp:TextBox ID="txtPAmt" runat="server" BackColor="LemonChiffon" 
                AutoPostBack="false" onkeyup="amtcalculate()"
        TabIndex="9" CssClass="tb6" Width="190px" Height="20px"
          ></asp:TextBox>
           <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" TargetControlID="txtPAmt"
                                        ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender>
        </td>
                                                           </tr>
                                                           <tr>
                                                           <td colspan="4">
                                                           <table width="100%" border="1px" cellspacing="0px" cellpadding="0px">
                                                           <tr>
                                                         
                                        <td align="center" colspan="5"><asp:Label ID="Label12" runat="server" Font-Bold="True" Font-Size="10pt" ForeColor="Blue"
                                        Text="Deduction Amount Detail"></asp:Label></td>
                                                           </tr>
                                                           <tr>
                                                           <th>TDS</th>
                                                            <th>SD/EMD</th>
                                                             <th>Resources</th>
                                                              <th>Other</th>
                                                               <th>Total</th>
                                                           </tr>
                                                            <tr>
                                                          <td align="center">
                                                           <asp:TextBox ID="txtTDS" runat="server" BackColor="LemonChiffon" AutoPostBack="false" onkeyup="Deductionamtcalculate()" Text="0"
        TabIndex="9" CssClass="tb6" Width="100px" Height="20px" 
          ></asp:TextBox>
           <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" TargetControlID="txtTDS"
                                        ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender>
                                                          </td>
                                                          <td align="center">
                                                            <asp:TextBox ID="txtSD" runat="server" BackColor="LemonChiffon" AutoPostBack="false" onkeyup="Deductionamtcalculate()" Text="0"
        TabIndex="9" CssClass="tb6" Width="100px" Height="20px"  
          ></asp:TextBox>
           <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" TargetControlID="txtSD"
                                        ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender>
                                                          </td>
                                                          <td align="center">
                                                           <asp:TextBox ID="txtResources" runat="server" BackColor="LemonChiffon" AutoPostBack="false" onkeyup="Deductionamtcalculate()" Text="0"
        TabIndex="9" CssClass="tb6" Width="100px" Height="20px" 
          ></asp:TextBox>
           <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" TargetControlID="txtResources"
                                        ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender>
                                                          </td>
                                                         <td align="center">
                                                          <asp:TextBox ID="txtOther" runat="server" BackColor="LemonChiffon" AutoPostBack="false" onkeyup="Deductionamtcalculate()" Text="0"
        TabIndex="9" CssClass="tb6" Width="100px" Height="20px" 
          ></asp:TextBox>
           <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" TargetControlID="txtOther"
                                        ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender>
                                                          </td>
                                                         <td align="center">
                                                          <asp:TextBox ID="txtTD" runat="server" BackColor="LemonChiffon" AutoPostBack="false" Text="0" Enabled="false"
        TabIndex="9" CssClass="tb6" Width="100px" Height="20px"  
          ></asp:TextBox>
                                                          </td>
                                                           </tr>
                                                           </table>
                                                           </td>
                                                           </tr>
                                                           <tr>
                                                           <td>
            <asp:Label ID="Label16" runat="server" Text="Net Amount" Font-Bold="True" Font-Size="8pt" 
                                                                ForeColor="Navy"></asp:Label></td>
        <td>
        <asp:TextBox ID="txtNetAmt" runat="server" BackColor="LemonChiffon" onkeyup="amtcalculate()" Text="0"
        TabIndex="9" CssClass="tb6" Width="190px" Height="20px" 
          ></asp:TextBox>
           <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server" TargetControlID="txtNetAmt"
                                        ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender>
        </td>
        <td>
            <asp:Label ID="Label17" Visible="true" runat="server" Text="Modes of Payment" Font-Size="8pt" Font-Bold="true" ForeColor="navy"></asp:Label></td>
        <td>
         <asp:DropDownList ID="ddlPayType" runat="server" AutoPostBack="True" TabIndex="1" 
                    Height="25px" Width="200px" CssClass="tb6" Font-Size="10pt" 
                onselectedindexchanged="ddlPayType_SelectedIndexChanged">
                    <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                     <asp:ListItem Value="CD" Text="Cheque/Draft Payment"></asp:ListItem>
                     <asp:ListItem Value="OP" Text="Online Payment"></asp:ListItem>
                      <asp:ListItem Value="CP" Text="Cash Payment"></asp:ListItem>
            </asp:DropDownList>
 </td>
                                                           </tr>
                                                           <tr>
                                                           <td>
            <asp:Label ID="Label18" Visible="true" runat="server" Text="RTGS/NEFT" Font-Size="8pt" Font-Bold="true" ForeColor="navy"></asp:Label></td>
        <td>
         <asp:DropDownList ID="ddlOPType" runat="server" AutoPostBack="True" TabIndex="1" Enabled="false" 
                    Height="25px" Width="200px" CssClass="tb6" Font-Size="10pt">
                    <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
            </asp:DropDownList>
 </td>
 <td>
            <asp:Label ID="Label20" runat="server" Text="RTGS/NEFT/CHEQUE/DD No." Font-Bold="True" Font-Size="8pt" 
                                                                ForeColor="Navy"></asp:Label></td>
        <td>
        <asp:TextBox ID="txtRefNo" runat="server" BackColor="LemonChiffon" 
        TabIndex="9" CssClass="tb6" Width="190px" Height="20px" 
          ></asp:TextBox>
           <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server" TargetControlID="txtRefNo"
                                        ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender>
        </td>
 
                                                           </tr>
                                                           <tr>
                                                           <td>
            <asp:Label ID="Label19" runat="server" Text="Date of Payment" Font-Bold="True" Font-Size="8pt" 
                                                                ForeColor="Navy"></asp:Label></td>
        <td>
            <asp:TextBox ID="txtPDate" runat="server" BackColor="LemonChiffon" AutoPostBack="false"
        TabIndex="9" CssClass="tb6" Width="190px" Height="20px" 
              ></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender9" runat="server"  Format="dd/MM/yyyy"
        TargetControlID="txtPDate"></cc1:CalendarExtender>
            </td>
            <td>
            <asp:Label ID="Label21" runat="server" Text="RO Advice No. & Date" Font-Bold="True" Font-Size="8pt" 
                                                                ForeColor="Navy"></asp:Label></td>
        <td>  <asp:TextBox runat="server" ID="txtAdvNo" Visible="true" ReadOnly="false" placeholder="Letter No."
        AutoPostBack="false" BackColor="LemonChiffon" 
        TabIndex="9" Width="90px" Height="20px" 
        ></asp:TextBox>
         <asp:TextBox ID="txtAdvDate" runat="server" BackColor="LemonChiffon" placeholder="Date"
        TabIndex="9" CssClass="tb6" Width="90px" Height="20px" 
           ></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender10" runat="server"  Format="dd/MM/yyyy"
        TargetControlID="txtAdvDate"></cc1:CalendarExtender>
            </td>
            
                                                           </tr>
                                                           </table>
                                                             
                                                            </div>
                                                            <table width="100%">
                                                            <tr>
                                                            <td align="center" colspan="2">
                                                            <asp:Button ID="btnPSubmit" runat="server" Text="Submit" Visible="true" 
                                                                CssClass="BTNBLUE" onclick="btnPSubmit_Click" />
                                                                <asp:Button ID="btnPCancel" runat="server" Text="Cancel" Visible="true" 
                                                                CssClass="BTNBLUE" />
                                                            </td>
                                                           
                                                            </tr>
                                                            </table>
                                                            </center>
                                                            </fieldset>
                                                        </td>
</tr>
</table>
</div>
</center>
</fieldset>
</asp:Content>

