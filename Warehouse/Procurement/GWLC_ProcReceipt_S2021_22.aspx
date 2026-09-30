<%@ Page Language="C#" MasterPageFile="~/MasterPage/PrivateWarehouse.master" AutoEventWireup="true" CodeFile="GWLC_ProcReceipt_S2021_22.aspx.cs" Inherits="WarehouseLevel_WLC_Procurement_GWLC_ProcReceipt_S2021_22" Title="Acceptance Detail" %>

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
   
   <style type="text/css">
    .modal
    {
        position: fixed;
        top: 0;
        left: 0;
        background-color: black;
        z-index: 99;
        opacity: 0.8;
        filter: alpha(opacity=80);
        -moz-opacity: 0.8;
        
        min-height: 100%;
        width: 100%;
    }
    .loading
    {
        font-family: Arial;
        font-size: 10pt;
        border: 5px solid #67CFF5;
        width: 200px;
        height: 100px;
        display: none;
        position: fixed;
        background-color: White;
        z-index: 999;
    }
       .style2
       {
           height: 20px;
       }
   </style>
   <style type="text/css">
    .modalBackground
    {
        background-color: Black;
        filter: alpha(opacity=60);
        opacity: 0.6;
    }
    .modalPopup
    {
        background-color: #FFFFFF;
        width: 80%;
        border: 3px solid #0DA9D0;
        border-radius: 12px;
        padding:0
      
    }
    .modalPopup .header
    {
        background-color: #2FBDF1;
        height: 30px;
        color: White;
        line-height: 30px;
        text-align: center;
        font-weight: bold;
        border-top-left-radius: 6px;
        border-top-right-radius: 6px;
    }
    .modalPopup .body
    {
        min-height: 50px;
        line-height: 30px;
        text-align: center;
        font-weight: bold;
    }
    .modalPopup .footer
    {
        padding: 6px;
    }
    .modalPopup .yes, .modalPopup .no
    {
        height: 23px;
        color: White;
        line-height: 23px;
        text-align: center;
        font-weight: bold;
        cursor: pointer;
        border-radius: 4px;
    }
    .modalPopup .yes
    {
        background-color: #2FBDF1;
        border: 1px solid #0DA9D0;
    }
    .modalPopup .no
    {
        background-color: #9F9F9F;
        border: 1px solid #5C5C5C;
    }
</style>    
<style type="text/css">
.button {
    background-color: #4CAF50; /* Green */
    border: none;
    color: white;
    padding: 0px 0px;
    text-align: center;
    text-decoration: none;
    display: inline-block;
    font-size: 12px;
    font-weight:bold;
    margin: 4px 2px;
    
    -webkit-transition-duration: 0.4s; /* Safari */
    transition-duration: 0.4s;
    cursor: pointer;
}
.button1 {
    background-color: white; 
    color: black; 
    border: 2px solid #4CAF50;
}

.button1:hover {
    background-color: #4CAF50;
    color: white;
}
.button2 {
    background-color: white; 
    color: black; 
    border: 2px solid #008CBA;
}

.button2:hover {
    background-color: #008CBA;
    color: white;
}

.button3 {
    background-color: white; 
    color: black; 
    border: 2px solid #f44336;
}

.button3:hover {
    background-color: #f44336;
    color: white;
}
.button6 {
    background-color: white;
    color: black;
    border: 2px solid #008CBA;
}

.button6:hover {
    background-color: #008CBA;
    color: white;
}
    .style3
    {
        width: 200px;
        height: 11px;
    }
    </style>     
<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
<script type="text/javascript">
    function ShowProgress() {
        setTimeout(function() {
            var modal = $('<div />');
            modal.addClass("modal");
            $('body').append(modal);
            var loading = $(".loading");
            loading.show();
            var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
            var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
            loading.css({ top: top, left: left });
        }, 200);
    }
    $('form').live("submit", function() {
        ShowProgress();
    });
</script>
<script type="text/javascript">
    var TotalChkBx;
    var Counter;

    window.onload = function() {
        //Get total no. of CheckBoxes in side the GridView.
        TotalChkBx = parseInt('<%= this.gdnewproc.Rows.Count %>');

        //Get total no. of checked CheckBoxes in side the GridView.
        Counter = 0;
    }

    function HeaderClick(CheckBox) {

        //Get target base & child control.
        var TargetBaseControl =
       document.getElementById('<%= this.gdnewproc.ClientID %>');
        var TargetChildControl = "chk_Sum";

        //Get all the control of the type INPUT in the base control.
        var Inputs = TargetBaseControl.getElementsByTagName("input");

        //Checked/Unchecked all the checkBoxes in side the GridView.
        for (var n = 0; n < Inputs.length; ++n)
            if (Inputs[n].type == 'checkbox' &&
                Inputs[n].id.indexOf(TargetChildControl, 0) >= 0)
            Inputs[n].checked = CheckBox.checked;

        //Reset Counter
        Counter = CheckBox.checked ? TotalChkBx : 0;
        //Get Data for all Check
        //        function calculate() {
        //            alert("hii");
        var txtTotalRecBags = 0;
        var txtTotalRecQty = 0;
        var Check = "N";
        var grid = document.getElementById("<%= gdnewproc.ClientID%>");
        for (var i = 0; i < grid.rows.length - 1; i++) {

            var txtBagSend = $("input[id*=sendb]")
            var txtQtySend = 0.0;
            txtQtySend = $("input[id*=sendq]")

            var txtBagReceive = $("input[id*=txtbagnumber]")
            var txtQtyReceive = 0.0;
            txtQtyReceive = $("input[id*=txtweight]")

            if (txtBagReceive[i].value != '' && txtQtyReceive[i].value != '') {
                var checkBoxes = $("input[id*=chk_Sum]")
                if (checkBoxes[i].checked == true) {
                    if ((parseInt(txtBagReceive[i].value) <= parseInt(txtBagSend[i].value)) && (parseFloat(txtQtyReceive[i].value) <= parseFloat(txtQtySend[i].value))) {
                        txtTotalRecBags = txtTotalRecBags + parseInt(txtBagReceive[i].value);
                        txtTotalRecQty = txtTotalRecQty + parseFloat(txtQtyReceive[i].value);
                    }
                    else {
                        //                        Check == "Y";
                        alert('You Can not Receive Greater Bags/Qty.then Send Bags/Qty.');
                    }
                }
            }
        }
        //        alert(Check);
        //        if (Check == "N") {
        document.getElementById('<%=lblTotalBags.ClientID%>').innerHTML = txtTotalRecBags.toString();
        document.getElementById('<%=lblTotalQty.ClientID%>').innerHTML = (txtTotalRecQty.toFixed(5)).toString();
        document.getElementById('<%= hdnLabelState.ClientID %>').value = txtTotalRecBags.toString()
        document.getElementById('<%= hdnLabelStateQty.ClientID %>').value = (txtTotalRecQty.toFixed(5)).toString();
        //        }
        //        else if (Check == "Y") {
        //        alert('You Can not Receive Greater Bags/Qty.then Send Bags/Qty.');
        //        }


        //        }
        //End
    }

    function ChildClick(CheckBox, HCheckBox) {
        //        alert("hii");
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
    <%--<script type="text/javascript">
        $(function() {
        $('[id*=chk_Delete]').on('change', function() {
                var value = 0;
                $('[id*=chk_Delete]:checked').each(function() {
                var row = $(this).closest('tr');
                alert(value);
                    value = value + parseInt(row.find('[id*=txtbagnumber]').html());
                });
                alert(value);
                $('[id*=lblTotalBags]').html(value);
            });
        });
    </script>--%>
    <script type="text/javascript">
        function calculateOld() {

            var sum = 0.00;
            var itemsum = 0.00;
            // alert(itemsum);
            var gridview = document.getElementById('<%=gdnewproc.ClientID %>');
            //alert(gridview);
            for (var row = 1; row < gridview.rows.length; row++) {

                //                var quantity = document.getElementById(gridview.rows[row].cells[7].all[0].id); //gridview.rows[row].cells[4].innerText;
                var quantity = gridview.rows[row].cells[7].innerHTML;
                //                alert(gridview.rows[row].cells[5].innerText);   //working
                var txtAmountReceive = $("input[id*=txtQty]")
                alert(gridview.rows[row].cells[5].innerText);
                //var rows = cntrlname.getElementsByTagName("tr");
                //alert(quantity);
                var chkBox = document.getElementById(gridview.rows[row].cells[10].all[1].id); // + (row - 1).toString());

                alert(gridview.rows[row].cells[7].innerText);
                if (!isNaN(gridview.rows[row].cells[3].innerText)) {
                    if (!isNaN(txtbagnumber.value)) {
                        if (chk_Delete.checked) {
                            itemsum = parseFloat(gridview.rows[row].cells[3].innerText) * parseFloat(txtbagnumber.value);
                            sum += itemsum;
                        }
                    }
                }
            }

            lblTotalBags.innerText = sum.toString();
            //alert(sum.toString());
        }
     </script>
     <script type="text/javascript">
         function calculate() {
             var txtTotalRecBags = 0;
             var txtTotalRecQty = 0;
             var grid = document.getElementById("<%= gdnewproc.ClientID%>");
             for (var i = 0; i < grid.rows.length - 1; i++) {

                 var txtBagSend = $("input[id*=sendb]")
                 var txtQtySend = 0.0;
                 txtQtySend = $("input[id*=sendq]")

                 var txtBagReceive = $("input[id*=txtbagnumber]")
                 var txtQtyReceive = 0.0;
                 txtQtyReceive = $("input[id*=txtweight]")
                 //                 if (i == 0) {
                 //                     alert(parseFloat($("input[id*=txtweight]")[0].value));
                 //                 }

                 if (txtBagReceive[i].value != '' && txtQtyReceive[i].value != '') {
                     var checkBoxes = $("input[id*=chk_Sum]")
                     if (checkBoxes[i].checked == true) {
                         if ((parseInt(txtBagReceive[i].value) <= parseInt(txtBagSend[i].value)) && (parseFloat(txtQtyReceive[i].value) <= parseFloat(txtQtySend[i].value))) {
                             txtTotalRecBags = txtTotalRecBags + parseInt(txtBagReceive[i].value);
                             txtTotalRecQty = txtTotalRecQty + parseFloat(txtQtyReceive[i].value);
                         }
                         else {
                             alert('You Can not Receive Greater Bags/Qty.then Send Bags/Qty.');
                         }

                     }
                 }
             }
             document.getElementById('<%=lblTotalBags.ClientID%>').innerHTML = txtTotalRecBags.toString();
             document.getElementById('<%=lblTotalQty.ClientID%>').innerHTML = (txtTotalRecQty.toFixed(5)).toString();
             document.getElementById('<%= hdnLabelState.ClientID %>').value = txtTotalRecBags.toString()
             document.getElementById('<%= hdnLabelStateQty.ClientID %>').value = (txtTotalRecQty.toFixed(5)).toString();
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


            <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>--%>
                      <div >
                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td colspan="4" align="center" valign="top">
                                    <fieldset style="width: 1000px; border: 1px solid navy;">
                                        <center>
                                            <div>
                                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                    <tr style="background-color: #0bb6e6; height: 25px">
                                                        <td colspan="4" align="center">
                                                            <asp:Label ID="lblDepositDetail" runat="server" Text="Deposit at Branch " Font-Size="17px"
                                                                Font-Bold="true" ForeColor="whitesmoke"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr align="center">
                                                        <td colspan="4" align="center">
                                                           <table>
                                                          <%-- <tr>
                                                           <td>
                                                           <span style="color: #FF0000">
                                                           Important instructions
                                                           </span>

                                                           </td>
                                                           
                                                           </tr>--%>
                                                           <%--<tr>
                                                           <td style="font-size: small; font-weight: bold; font-style: normal; color: #FF0000; text-decoration: blink">
                                                           1. 
                                                               Other depot के case मे रिसीविंग लेने के लिए Date wise&nbsp; ऑप्शन का use किया जा सकता है जिसमे एक डेट की सारी रिसीविंग एक साथ दिख जाएगी</td>
                                                           </tr>--%>
                                                           </table>
                                                           </td>
                                                    </tr>
                                                    <tr align="center">
                                                        <td colspan="4" align="center">
                                                            <asp:Label ID="lblMsg" runat="server" Font-Bold="True" ForeColor="Red" Visible="False"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4" style="height: 5px">
                                                        </td>
                                                    </tr>
                                                    <tr align="center">
                                                        <td align="left" style="width: 200px">
                                                            <asp:Label ID="lblDepositorType" runat="server" Font-Size="12px" Font-Bold="true"
                                                                Text="Type of Depositor" ForeColor="navy"></asp:Label>
                                                        </td>
                                                        <td align="left" style="width: 200px">
                                                            <asp:DropDownList ID="ddldepositortype" runat="server" AutoPostBack="True" Width="200px" Enabled="false"
                                                                Height="25px" CssClass="tb6" OnSelectedIndexChanged="ddldepositortype_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td align="center" style="width: 200px">
                                                            <asp:Label ID="lblDepositorName" runat="server" Font-Size="12px" Font-Bold="true"
                                                                Text="Depositor Name" ForeColor="navy"></asp:Label>
                                                        </td>
                                                        <td align="left" style="width: 200px">
                                                            <asp:DropDownList ID="ddlDepositor" runat="server" AutoPostBack="false" Width="290px" Enabled="true"
                                                                Height="25px" CssClass="tb6">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                     <tr>
                                                        <td colspan="4" style="height: 5px">
                                                        </td>
                                                    </tr>
                                                   <tr align="center">
                                                        <td align="left" style="width: 200px">
                                                              <asp:Label ID="Label1" runat="server" Font-Bold="True" 
                                                                Font-Size="12px" ForeColor="Navy" Text="Procurement Commodity"></asp:Label>
                                                        </td>
                                                        <td align="left" style="width: 200px">
                                                              <asp:DropDownList ID="ddlProcCmd" runat="server" AutoPostBack="True" 
                                                                  Height="25px" Width="200px" onselectedindexchanged="ddlProcCmd_SelectedIndexChanged" Enabled="true"
                                                                   > 
                                                              <%--<asp:ListItem Value="22" Selected="True">Wheat-PSS</asp:ListItem>--%>
                                                               <asp:ListItem Value="22">Wheat-PSS</asp:ListItem>
                                                              <asp:ListItem Value="63">GRAM</asp:ListItem>
                                                                <asp:ListItem Value="64">LENTIL</asp:ListItem>
                                                                <asp:ListItem Value="33">Mustard-Sarason</asp:ListItem>
                                                                  <asp:ListItem Value="13">Paddy-Common</asp:ListItem>
                                                                  <asp:ListItem Value="11">Jowar</asp:ListItem>
                                                                  <asp:ListItem Value="8">Bajra</asp:ListItem>
                                                                  <asp:ListItem Value="3">Rice-Raw-Common</asp:ListItem>
                                                                  <asp:ListItem Value="92">Moong</asp:ListItem>
                                                                <asp:ListItem Value="27">Urad</asp:ListItem>
                                                                <%--<asp:ListItem Value="52">Arahar</asp:ListItem>--%>
                                                              <%--  <asp:ListItem Value="13">Paddy-Common</asp:ListItem>
                                                                <asp:ListItem Value="14">Paddy-Grade-A</asp:ListItem>
                                                                <asp:ListItem Value="8">Bajra</asp:ListItem>
                                                                <asp:ListItem Value="40">Jau</asp:ListItem>
                                                                <asp:ListItem Value="11">Jowar</asp:ListItem>--%>
                                                               <%-- <asp:ListItem Value="123">RAM TIL</asp:ListItem>
                                                              <asp:ListItem Value="31">Ground-Nut</asp:ListItem>
                                                                <asp:ListItem Value="92">Moong</asp:ListItem>
                                                                <asp:ListItem Value="65">Tilli</asp:ListItem>
                                                                <asp:ListItem Value="27">Urad</asp:ListItem>
                                                                <asp:ListItem Value="11">Jowar</asp:ListItem>
                                                                <asp:ListItem Value="8">Bajra</asp:ListItem>
                                                                <asp:ListItem Value="13" Selected="True">Paddy-Common</asp:ListItem>
                                                                <asp:ListItem Value="14">Paddy-Grade-A</asp:ListItem>--%>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td align="center" style="width: 200px">
                                                            <asp:Label ID="Label2" runat="server" Font-Size="12px" Font-Bold="true"
                                                                Text="Crop Year" ForeColor="navy"></asp:Label>
                                                        </td>
                                                        <td align="left" style="width: 200px">
                                                             <asp:DropDownList ID="ddlcropyear" runat="server" AutoPostBack="True" Height="25px" Width="120px" Enabled="true"
                                                                >
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>



                                                        <tr id="IDLuster" runat="server" align="center" visible="false">

                                                        <td align="left" style="width: 200px">
                                                              <asp:Label ID="Label9" runat="server" Font-Bold="True" 
                                                                Font-Size="12px" ForeColor="Navy" Text="select Luster Loss"></asp:Label>
                                                        </td>

                                                             <td align="left" style="width: 200px">
                                                              <asp:DropDownList ID="DdlLusterLoss" runat="server"  Height="25px" Width="200px"> 

                                                              <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                               <asp:ListItem Text="Wheat Without Luster Loss" Value="1"></asp:ListItem>
                                                              <asp:ListItem Text="Wheat With Luster Loss" Value="2"></asp:ListItem>
                                                              
                                                            
                                                            </asp:DropDownList>
                                                        </td>
                                                             </tr>




                                                    <tr>
                                                        <td colspan="4" style="height: 5px">
                                                        </td>
                                                    </tr>
                                                    <tr align="center">
                                                     <td align="left" style="width: 200px" visible="false" runat="server">
                                                            <asp:Label ID="Label4" runat="server" Font-Size="12px" Font-Bold="true"
                                                                Text="Godown" ForeColor="navy"></asp:Label>
                                                        </td>
                                                        <td align="left" style="width: 200px" visible="false" runat="server">
                                                             <asp:DropDownList ID="ddl_godown" runat="server" AutoPostBack="True" 
                                                                 Height="25px" Width="200px" Enabled="false" onselectedindexchanged="ddl_godown_SelectedIndexChanged"
                                                                >
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td align="left" style="width: 200px">
                                                              <asp:Label ID="Label3" runat="server" Font-Bold="True" 
                                                                Font-Size="12px" ForeColor="Navy" Text="Deposit Date"></asp:Label>
                                                        </td>
                                                        <td align="left" style="width: 200px">
                                                               <asp:TextBox ID="txtdepositdate" runat="server" MaxLength="18" Width="175px" Height="18px" AutoPostBack="true" 
                                                                CssClass="tb6" ontextchanged="txtdepositdate_TextChanged"></asp:TextBox>
                                                            <asp:ImageButton ID="Imgpop" runat="server" ImageUrl="~/images/cal.gif" CausesValidation="false" />
                                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" TargetControlID="txtdepositdate"
                                                                Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy" PopupButtonID="Imgpop">
                                                            </asp:CalendarExtender>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtdepositdate"
                                                                Display="Dynamic" ErrorMessage="Deposite Date is required" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                        </td>
                                                       
                                                    </tr>
                                                    
                                                    <%--<tr align="center">
                                                        <td colspan="4" style="height: 5px">
                                                            <asp:RadioButton ID="RadioButton1" runat="server" AutoPostBack="True" 
                                                                Checked="True" oncheckedchanged="RadioButton1_CheckedChanged" 
                                                                Text="Crop Yearly" GroupName="a" />
                                                            <asp:RadioButton ID="RadioButton2" runat="server" AutoPostBack="True" 
                                                                oncheckedchanged="RadioButton2_CheckedChanged" Text="Between Date" 
                                                                GroupName="a" Visible="false" />
                                                        </td>
                                                    </tr>--%>
                                                    <tr>
                                                        <td colspan="4" style="height: 5px">
                                                            &nbsp;</td>
                                                    </tr>
                                                    
                                       
                                              
                                      <%--              <tr runat="server" id="trdatewise" visible="false">
                                                        <td colspan="4" style="height: 5px">
                                                            Select Date:
                                                            <asp:TextBox ID="txtdatewisedate" runat="server"></asp:TextBox>
                                                            <asp:CalendarExtender ID="txtdatewisedate_CalendarExtender" runat="server" 
                                                                Enabled="True" TargetControlID="txtdatewisedate" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy">
                                                            </asp:CalendarExtender>
&nbsp;Commodity:
                                                            <asp:DropDownList ID="ddlcomm" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlcomm_SelectedIndexChanged">
                                                            </asp:DropDownList>
&nbsp;Godown:
                                                            <asp:DropDownList ID="ddlgodown" runat="server">
                                                            </asp:DropDownList>
                                                            &nbsp;
                                                            <asp:Button ID="btndatesub" runat="server" Text="Submit" 
                                                                OnClientClick="this.disabled = true; this.value='Please Wait'" 
                                                                UseSubmitBehavior="false" onclick="btndatesub_Click" />
                                                                                            </td>
                                                    </tr>--%>
                                                 
                                                </table>
                                            </div>
                                        </center>
                                    </fieldset>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="height: 5px">
                                </td>
                            </tr>
                            <tr id="trnewproc" runat="server" visible="false">
                                <td colspan="4" align="center" valign="top">
                                
                                            <div>
                                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                    <tr style="background-color: #ff9966; height: 25px">
                                                        <td  valign="Center">
                                                         <span style="color: White; font-size: 10pt; font-weight: bold;">Total Record:
                                                                 <asp:Label ID="lblNoofAC" runat="server" Text=""></asp:Label></span>
                                                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                                 &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                                 &nbsp; &nbsp; &nbsp; &nbsp;
                                                            <span style="color: White; font-size: 12pt; font-weight: bold">Depositor Form Detail
                                                                 <asp:Label ID="lblcropyr" runat="server" Text=""></asp:Label></span></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 5px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" valign="top">
                                                            <div style="height:340px; widows:100%; overflow:scroll;">
                                                            <asp:GridView ID="gdnewproc" runat="server" AutoGenerateColumns="False"
                                                                DataKeyNames="Acceptance_No,DepositerNo" AllowPaging="false" Width="100%"
                                                                Font-Size="10pt" BorderColor="Navy" BorderWidth="1px" OnPageIndexChanging="gdnewproc_PageIndexChanging"
                                                                 TabIndex="4" CellPadding="4" CellSpacing="2">
                                                                <Columns>
                                                                    
                                                                  
                                                                    <asp:BoundField DataField="DepositerNo" HeaderText="Depositor Form No." SortExpression="DepositerNo">
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                       <asp:BoundField DataField="Acceptance_No" HeaderText="Acceptance No" SortExpression="Acceptance_No">
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Acceptance_Date" HeaderText="Acceptance Date" SortExpression="Acceptance_Date">
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:BoundField>
                                                                      <asp:BoundField DataField="TC_Number" HeaderText="TC Number" SortExpression="TC_Number">
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:BoundField>
                                                                      <asp:BoundField DataField="Truck_Number" HeaderText="Truck Number" SortExpression="Truck_Number">
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:BoundField>
                                                             
                                                                      <asp:TemplateField HeaderText="Send Bags">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="sendb" BackColor="Transparent" Enabled="false" Font-Bold="true" runat="server" Width="60px" MaxLength="5" onblur="Spc_validatorInt(this)" Text='<%# Eval("Recd_Bags") %>'>0</asp:TextBox>
                                                                            </ItemTemplate>
                                                                            <ItemStyle Width="80px" />
                                                                        </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="Send Qty.">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="sendq"  BackColor="Transparent"  Enabled="false" Font-Bold="true" runat="server" Width="65px" MaxLength="12" onkeyup="NumericDecimalCheck(this,5)"
                                                                                    onblur="Spc_validatornumeric(this)" Text='<%# Eval("Recd_Qty") %>'>0</asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                     <asp:TemplateField HeaderText="Receive Bags">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtbagnumber" BackColor="#ffe09f" Font-Bold="true" runat="server" Width="60px" MaxLength="5" onblur="Spc_validatorInt(this)" Text='<%# Eval("Recd_Bags2") %>'>0</asp:TextBox>
                                                                            </ItemTemplate>
                                                                            <ItemStyle Width="80px" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Receive Qty.">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtweight" BackColor="#ffe09f" Font-Bold="true" runat="server" Width="65px" MaxLength="12" onkeyup="NumericDecimalCheck(this,6)"
                                                                                   Text='<%# Eval("Recd_Qty2") %>'>0</asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>


                                                                       <asp:BoundField DataField="RecdBags_JuteNew" HeaderText="Recd_Bags_JuteNew" SortExpression="RecdBags_JuteNew">
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:BoundField>

                                                                        <asp:BoundField DataField="RecdBags_PP" HeaderText="Recd_Bags_PP" SortExpression="RecdBags_PP">
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:BoundField>

                                                                  
                                                                            <asp:BoundField DataField="RecdBags_JuteOld" HeaderText="Recd_Bags_JuteOld" SortExpression="RecdBags_JuteOld">
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:BoundField>

                                                                             <asp:BoundField DataField="Moisture" HeaderText="Moisture" SortExpression="Moisture">
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:BoundField>
                                                                      
                                                                         <asp:TemplateField HeaderText="Select">
                                                                            <HeaderTemplate>
                                                                           <asp:CheckBox ID="chkBxHeader" Text="All"  onclick="javascript:HeaderClick(this);" runat="server" />
                                                                            </HeaderTemplate>
                                                                             <ItemTemplate>
                                                                            
                                                                         <asp:CheckBox ID="chk_Sum" runat="server" onclick ="calculate();"/>
                                                                            </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" Font-Names="Arial" Font-Size="12pt" HorizontalAlign="Center"
                                                        Width="80px" />
                                                    <ItemStyle HorizontalAlign="Center" Width="10px" />
                                                    <ControlStyle Width="15px" />
                                                </asp:TemplateField>
                                                                </Columns>
                                                                <FooterStyle BackColor="#CCCC99" />
                                                                <PagerStyle BackColor="#719cb6" ForeColor="Black" HorizontalAlign="center" />
                                                                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                                <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                                                                    Height="20px" Font-Size="10pt" />
                                                                <AlternatingRowStyle BackColor="White" />
                                                            </asp:GridView>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                     <tr>
                                <td colspan="4" style="height: 5px">
                                </td>
                            </tr>
                              <tr>
                                <td colspan="4" style="height: 5px">
                                <hr />
                                </td>
                            </tr>
                            <tr>
                            <td>
                            <table id="tblbtn" runat="server" visible="false">
                              <tr align="center">
                                                        <td align="left" class="style3">
                                                              <asp:Label ID="Label7" runat="server" Font-Bold="True" 
                                                                Font-Size="14px" ForeColor="Navy" Text="Total Bags Send"></asp:Label>
                                                        </td>
                                                        <td align="left" class="style3">
                                                             <asp:Label ID="lblTotalBagSend" runat="server" Text="0" Font-Size="14px" Font-Bold="true"
                                                               ForeColor="navy"></asp:Label>
                                                        </td>
                                                        <td align="left" class="style3">
                                                            <asp:Label ID="Label10" runat="server" Font-Size="14px" Font-Bold="true"
                                                                Text="Total Qty. Send" ForeColor="navy"></asp:Label>
                                                        </td>
                                                        <td align="left" class="style3">
                                                               <asp:Label ID="lblTotalQtySend" runat="server" Font-Size="14px" Font-Bold="true"
                                                                Text="0" ForeColor="navy"></asp:Label>
                                                         
                                                        </td>
                                                    </tr>
                                                       <tr>
                                <td colspan="4" style="height: 10px">
                                </td>
                            </tr>
                                <tr align="center">
                                                        <td align="left" style="width: 200px">
                                                              <asp:Label ID="Label5" runat="server" Font-Bold="True" ForeColor="#c64f00"
                                                                Font-Size="14px"  Text="Total Bags Received"></asp:Label>
                                                        </td>
                                                        <td align="left" style="width: 200px">
                                                             <asp:Label ID="lblTotalBags" EnableViewState="false"  ViewStateMode="Disabled" runat="server" Text="0" Font-Size="14px" Font-Bold="true" ForeColor="#c64f00"
                                                             ></asp:Label>
                                                              <asp:HiddenField runat="server" ID="hdnLabelState" />
                                                              
                                                        </td>
                                                        <td align="left" style="width: 200px">
                                                            <asp:Label ID="Label6" runat="server" Font-Size="14px" Font-Bold="true"
                                                                Text="Total Qty. Received" ForeColor="#c64f00"></asp:Label>
                                                        </td>
                                                        <td align="left" style="width: 200px">
                                                               <asp:Label ID="lblTotalQty" runat="server" Font-Size="14px" Font-Bold="true"
                                                                Text="0" ForeColor="#c64f00"></asp:Label>
                                                        <asp:HiddenField runat="server" ID="hdnLabelStateQty" />
                                                        </td>
                                                    </tr>
                                                       <tr>
                                <td colspan="4" style="height: 20px">
                                </td>
                            </tr>
                                                    <tr>
                                                        <td colspan="4" align="center">
                                                            <asp:Button ID="btn_save" runat="server" Text="Submit" CssClass="BTNBLUE" Width="100px"
                                                                Enabled="False" TabIndex="13" ValidationGroup="SaveValid" 
                                                                onclick="btn_save_Click"/>
                                                            &nbsp;&nbsp;&nbsp;
                                                            <asp:Button ID="btn_Close" runat="server" Text="Cancel"
                                                                CssClass="BTNBLUE" Width="100px" CausesValidation="false" 
                                                                onclick="btn_Close_Click" />
                                                       
                                                            
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                    <td>
<asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" PopupControlID="pnlCofirmmsg" TargetControlID="Label8"
   CancelControlID="btnNo" BackgroundCssClass="modalBackground">
</asp:ModalPopupExtender>
<asp:Panel ID="pnlCofirmmsg" runat="server" CssClass="modalPopup" Height="330px" Width="700px" >
    <div class="header">
        <table style="width:100%;">
            <tr>
                   <td style="color:White; font-weight:bold; font-size:large;" align="center">Receiving Detail</td>
                   <td style="width:50px"> <asp:Button ID="btnNo" runat="server" Text="Close" CssClass="no" align="left"/> </td>
            </tr> 
                                         
        </table> 
    </div>
    <div class="body">
                    <table cellspacing="1" style="width:100%;">
                         <tr>
                        <td style="height:15px">
                        </td>
                        </tr>
                         <tr>
                            <td style="font-size:12px; font-family:Arial; font-weight:bold" align="left">
                        <table width="100%" >
                            <tr style="height:15px;">
                            <td style="width:130px;">
                            &nbsp;Date of Deposit :
                            </td>
                                    <td>
                                     <asp:Label ID="lblDepostDate" runat="server" ></asp:Label>
                                    </td>
                            <td style="width:150px;">
                            Godown Name :
                            </td>                                    
                                    <td>
                                     <asp:Label ID="lblGodown" runat="server" ></asp:Label>
                                    </td>
                            </tr>  
                             <tr style="Height:15px;">
                            <td> &nbsp;Send Bags :
                            </td>
                                    <td>
                                    <asp:Label ID="lblSendBags" runat="server" ></asp:Label>
                                    </td> 
                                    <td>
                                    Send Qty.(In Qtl.) :</td>                                   
                                    <td>
                                     <asp:Label ID="lblSendQty" runat="server" ></asp:Label>
                                    </td>
                             
                             </tr> 
                              <tr style="Height:15px;">
                            <td> &nbsp;Received Bags :
                            </td>
                                    <td>
                                    <asp:Label ID="lblRcdBags" runat="server" ></asp:Label>
                                    </td> 
                                    <td>
                                    Received Qty.(In Qtl.) :</td>                                   
                                    <td>
                                     <asp:Label ID="lblRecdQty" runat="server" ></asp:Label>
                                    </td>
                             
                             </tr> 
                            <tr style="Height:15px;">
                            <td> &nbsp;Depositor :
                            </td>
                                    <td>
                                    <asp:Label ID="lblDepositor" runat="server" ></asp:Label>
                                    </td> 
                                    <td>
                                    Commodity :</td>                                   
                                    <td>
                                     <asp:Label ID="lblCommodity" runat="server" ></asp:Label>
                                    </td>
                             
                             </tr> 
                             <tr>
                             <td class="style2">
                             &nbsp;Crop Year :</td>
                                    <td class="style2">
                                     <asp:Label ID="lblCropYear" runat="server" ></asp:Label>
                                    </td>
                                                                                                                                                   
                           </tr>
                          </table>                            
                            </td>
                         </tr>                       
                                                  
                        <tr>
                            <td align="center">
                                       <asp:Button class="button button2" Width="150px" Height="30px" ID="Button2" 
                                        runat="server" Text="Proceed" align="Center" onclick="Button2_Click" />
                                                                            
                            </td>                     
                        </tr>   
                        <tr>
                            <td align="center">
                                       <asp:Label ID="hdnGodownID" runat="server" Visible="false" ></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                                       <asp:Label ID="hdnDepositorID" runat="server" Visible="false"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                                       <asp:Label ID="hdnCommodityID" runat="server" Visible="false"></asp:Label>
                                                                            
                            </td>                     
                        </tr>                  
                    </table>
    </div>                        
</asp:Panel>   
                                                    </td>
                                                    </tr>
                                                    </table>
                            </td>
                            </tr>
                             
                                                    <asp:Label ID="Label8" runat="server" Font-Bold="true" Font-Size="Medium"></asp:Label>
 
                            
                                                </table>
                                            </div>
                                
                                </td>
                            </tr>

                           
                           
                        </table>
                    </div>           
 
</asp:Content>

