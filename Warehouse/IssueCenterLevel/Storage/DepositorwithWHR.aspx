<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DepositorwithWHR.aspx.cs"
    Inherits="IssueCenterLevel_DepositorSlip" MasterPageFile="~/MasterPage/Gdwn.master"
    Title="Depositor Page" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 <%--    <script type="text/javascript" src="../../JS/allFormValidations.js"></script>

    <script type="text/javascript" src="../JS/allFormValidations.js"></script>

    <script type="text/javascript" src="../JS/chksql.js"></script>

    <script type="text/javascript" src="../JS/MD5.js"></script>--%>
    <link href="../../css/Calander.css" rel="stylesheet" />

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
    
        .style1
        {
            color: #FF0000;
        }
    
    </style>

    <script type="text/javascript">
        function checkDate(sender, args) {
            if (sender._selectedDate > new Date()) {
                alert("You cannot select a day greater than today!");
                sender._selectedDate = new Date();
                // set the date back to the current date
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
            }
        }
    </script>


    <script type="text/javascript">
			function compare()
			{
			 
			 a=(document.getElementById('AvlStackCap'));
			 b =( document.getElementById('txtwt'));
			//ram
			if(b.value<0)
		    {
			     alert("Negetive value not accetped");
			     b.value="";
		         b.focus();
			
			}
		    else
			    {
			        if ( Number(a.value) < Number(b.value))
			        {
			        alert("stack insufficent to Stack Pl check the value");
                    b.focus();
			        }
			 
			
			}
		
			var checkOK = "0123456789/";
		    var checkStr = document.getElementById('txtwt').value;
		    var allValid = true;
		    var allChr = "";
		    for (i = 0;  i < checkStr.length;  i++)
		    {
			    ch = checkStr.charAt(i);
			    for (j = 0;  j < checkOK.length;  j++)
			    if (ch == checkOK.charAt(j))
			    break;
			    if (j == checkOK.length)
			    {
				    allValid = false;
				    break;
			    }
			    if (ch != ",")
				    allChr += ch;
		    }
			if (!allValid)
			{
				alert("This Character is Not Allowed");
				document.getElementById('txtwt').value=""
				document.getElementById('txtwt').focus();
				return (false);
			} 
			
	        }
     

function AskForComment()
 {
      var bags = document.ctl00_ContentPlaceHolder1_txtnobags.value;
       var wt = document.ctl00_ContentPlaceHolder1_txtwt.value;
        

      
    if ( bags == '' || wt == '')
    {
     if ( bags == '')
          {
              alert("No. of Bags cannot be empty.");
				
				document.ctl00_ContentPlaceHolder1_txtnobags.focus();
				return (false);
		}
 if ( wt == '')
          {
              alert("Weight cannot be empty.");
				
				document.ctl00_ContentPlaceHolder1_txtwt.focus();
				return (false);
		}
	
		
		}		
  }
  
  //count dropdownlist box items
  function dropdowncount()
 {
      var _count = document.ctl00_ContentPlaceHolder1_txtnobags.itms.count;
      
       if ( _count == 0)
          {
              alert("No. of Bags cannot be empty.");
				
				document.ctl00_ContentPlaceHolder1_txtnobags.focus();
				return (false);
		}
 
  }
  
  //To Pop Up A Window
  function popMe(url)
  {
    var newWindow;
    newWindow=window.open(url,'MyWin','width=275,height=390,top=1,left=1');

  }
  
 
//percentage
function validate() {
  // Percent = document.frmPost.percent.value
  
  if ((ctl00_ContentPlaceHolder1_txtmoistcontent.value.indexOf(".") == -1) && (ctl00_ContentPlaceHolder1_txtmoistcontent.value.length >= 3)) {
   alert("Percentage format is not correct");
    ctl00_ContentPlaceHolder1_txtmoistcontent.value = "";
    ctl00_ContentPlaceHolder1_txtmoistcontent.focus();
    return false;
  }
  if ((ctl00_ContentPlaceHolder1_txtmoistcontent.value.indexOf(".")) == 4 || (ctl00_ContentPlaceHolder1_txtmoistcontent.value.indexOf(".")) == 3 || (ctl00_ContentPlaceHolder1_txtmoistcontent.value.indexOf(".")) == 0) {
    alert("Invalid Percentage");
     ctl00_ContentPlaceHolder1_txtmoistcontent.value = "";
    ctl00_ContentPlaceHolder1_txtmoistcontent.focus();
    return false;
  }
  if (isNaN(ctl00_ContentPlaceHolder1_txtmoistcontent.value)==true) {
    alert("Enter Numeric values");
     ctl00_ContentPlaceHolder1_txtmoistcontent.value = "";
    ctl00_ContentPlaceHolder1_txtmoistcontent.focus();
    return false;
  }	
  return true;
}	
 
 
 function Spc_character(abc)
             {
		var checkOK = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz.,()_&\ ";;
		var checkStr = abc.value;
		var allValid = true;
		var allChr = "";
		for (i = 0;  i < checkStr.length;  i++)
		{
			ch = checkStr.charAt(i);
			for (j = 0;  j < checkOK.length;  j++)
			if (ch == checkOK.charAt(j))
			break;
			if (j == checkOK.length)
			{
				allValid = false;
				break;
			}
			if (ch != ",")
				allChr += ch;
		}
			if (!allValid)
			{
				alert("This Character is Not Allowed");
				abc.value=""
				abc.focus();
				return (false);
			}  
			} 
			
			function cvb()
			{
			alert("jff");
			}
 
    </script>

    
    
    <fieldset style="width: 1000px; border: 2px solid navy;">
        <center>
            <div>
             
                        
                <table cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td align="center" valign="top">
                            <fieldset style="width: 980px; border: 1px solid navy;">
                                <center>
                                    <div>
                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr style="background-color: #0bb6e6; height: 25px">
                                                <td colspan="4" align="center">
                                                    <asp:Label ID="lblWHRDetailStackingInfo" runat="server" Text="WHR Details with Stacking Information"
                                                        ForeColor="whitesmoke" Font-Bold="true" Font-Size="12pt"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 5px" colspan="4">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" align="center">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td style="height: 5px" colspan="4">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6" align="left">
                                                    <asp:Label ID="lblInstruction" runat="server" Text="Note :- (*) Fields are Mandatory (Market Value in Rupees.Paisa upto 2 decimal places)"
                                                        ForeColor="red" Font-Size="8pt" Font-Bold="true"></asp:Label>
                                                    <a href="javascript:popMe('../../SampleQuantity.htm');">(Qty. in Qtls.kgsgms)</a>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 10px" colspan="4">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lblDepositorType" runat="server" Font-Size="8pt" Text="Type of Depositor"
                                                        Font-Bold="true" ForeColor="navy"></asp:Label></td>
                                                <td align="left">
                                                    <asp:DropDownList ID="ddldepositortype" runat="server" AutoPostBack="True" Width="155px"
                                                        OnSelectedIndexChanged="ddldepositortype_SelectedIndexChanged" TabIndex="1" ValidationGroup="SaveValid"
                                                        Height="25px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="lblWHRDate" runat="server" Text="WHR Date" Font-Size="8pt" Font-Bold="true"
                                                        ForeColor="navy"></asp:Label><span class="style1"></span></td>
                                                <td align="left">

                                                    <asp:TextBox ID="txtwhrdate" onblur="validateDatenew('txtwhrdate')" runat="server"
                                                        Width="150px" MaxLength="10" TabIndex="4"></asp:TextBox>
                                                    <asp:CalendarExtender ID="txtwhrdate_CalendarExtender" runat="server" CssClass="red"
                                    Enabled="True" Format="dd/MM/yyyy" OnClientDateSelectionChanged="checkDate" PopupButtonID="rte" TargetControlID="txtwhrdate"
                                   >
                                </asp:CalendarExtender>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6"
                                                                    runat="server" Display="Dynamic" ErrorMessage="WHR Date field cannot be empty"
                                                                    SetFocusOnError="True" ControlToValidate="txtwhrdate"></asp:RequiredFieldValidator>

                                                    <span style="font-size: 10pt; color: #cc0000">*</span></td>
                                            </tr>
                                            <tr>
                                                <td style="height: 5px" colspan="4">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lblDepositorName" runat="server" Text="Depositor Name" Font-Size="8pt"
                                                        Font-Bold="true" ForeColor="navy"></asp:Label></td>
                                                <td align="left">
                                                    <span style="font-size: 10pt; color: #cc0000"></span>
                                                    <asp:DropDownList ID="ddlDepositor" runat="server" Width="155px" Height="25px" OnSelectedIndexChanged="ddlDepositor_SelectedIndexChanged"
                                                        TabIndex="2" AutoPostBack="True" ValidationGroup="SaveValid">
                                                    </asp:DropDownList>
                                                    <span style="font-size: 10pt; color: #cc0000">*</span>
                                                    <asp:TextBox ID="txtwhrno" onblur="Spc_character(this)" runat="server" Width="150px"
                                                        MaxLength="20" TabIndex="3" Visible="False"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic"
                                                        ErrorMessage="WHR  No. Bags field cannot be empty" SetFocusOnError="True" ControlToValidate="txtwhrno" Visible="False">*</asp:RequiredFieldValidator></td>
                                                <td align="left">
                                                    <asp:Label ID="lblLotNo" runat="server" Text="Lot No." Visible="False" Font-Size="8pt"
                                                        ForeColor="navy" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td align="left">

                                                    <asp:TextBox ID="txtlotnumber" runat="server" CssClass="txtFldSmall" MaxLength="30"
                                                        onblur="Spc_character(this)" Width="100px" TabIndex="15" Visible="False"></asp:TextBox>


                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 5px" colspan="4">
                                                </td>
                                            </tr>
                                            <tr id="trArrivalS" runat="server">
                                                <td align="left">
                                                    <asp:Label ID="lblSorcePfArrival" runat="server" Text="Source of Arrival" Font-Size="8pt"
                                                        Font-Bold="true" ForeColor="navy"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddl_Sofarrival" runat="server" Width="155px" Height="25px" TabIndex="5"
                                                        ValidationGroup="SaveValid">
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="lblAvgMoisture" runat="server" Text="Moisture Content Range (%)" Font-Size="8pt"
                                                        ForeColor="navy" Font-Bold="true"></asp:Label></td>
                                                <td valign="middle" align="left">
                                                    <asp:Label ID="lbl_From" runat="server" Text="From" Font-Size="8pt" ForeColor="navy"
                                                        Font-Bold="true"></asp:Label>
                                                    <asp:TextBox ID="txtmoistcontent" runat="server" MaxLength="13" onblur="validate()"
                                                        onkeyup="NumericDecimalCheck(this,3)" TabIndex="12" Width="100px" AutoComplete="off">9</asp:TextBox>
                                                    <asp:Label ID="lbl_To" runat="server" Text="To" Font-Size="8pt" ForeColor="navy"
                                                        Font-Bold="true"></asp:Label>
                                                    <asp:TextBox ID="txtmoistcontent_To" runat="server" Width="100px" MaxLength="13" AutoComplete="off"
                                                        onblur="validate()" onkeyup="NumericDecimalCheck(this,3)" TabIndex="13">12</asp:TextBox>
                                                </td>
                                                <td>
                                                    &nbsp;</td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td style="height: 5px" colspan="4">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lblScheme" runat="server" Text=" Scheme" Font-Size="8pt" ForeColor="navy"
                                                        Font-Bold="true"></asp:Label>
                                                </td>
                                                <td colspan="1" align="left">
                                                    <asp:DropDownList ID="ddlpurpose" runat="server" Width="255px" TabIndex="8" ValidationGroup="SaveValid"
                                                        Height="25px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="lblCropYear" runat="server" Text="Crop Year" Font-Size="8pt" Font-Bold="true"
                                                        ForeColor="navy"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <a onclick="ShowCalendar(ctl00_ContentPlaceHolder1_Uxentrydate, ctl00_ContentPlaceHolder1_Uxentrydate);"
                                                        href="javascript:;">
                                                    <asp:DropDownList ID="ddlcropyear" runat="server" OnSelectedIndexChanged="UxCommodity_SelectedIndexChanged"
                                                        Width="155px" TabIndex="11" ValidationGroup="SaveValid" Height="25px">
                                                    </asp:DropDownList>
                                                    </a>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 5px" colspan="4">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 5px" colspan="4">
                                                    <asp:TextBox ID="txtdepositorname" runat="server" MaxLength="20" onblur="Spc_character(this)"
                                                        Width="0px" Visible="False" BackColor="Transparent" BorderColor="Transparent"
                                                        BorderStyle="None"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 5px" colspan="4">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lblCommodity" runat="server" Text="Commodity:" Font-Size="8pt" ForeColor="navy"
                                                        Font-Bold="true"></asp:Label></td>
                                                <td align="left">
                                                    <asp:DropDownList ID="UxCommodity" runat="server" AutoPostBack="True" Width="155px"
                                                        Height="25px" OnSelectedIndexChanged="UxCommodity_SelectedIndexChanged" TabIndex="9"
                                                        ValidationGroup="SaveValid">
                                                        <asp:ListItem>Wheat</asp:ListItem>
                                                        <asp:ListItem>CMR</asp:ListItem>
                                                        <asp:ListItem>Levi Rice </asp:ListItem>
                                                        <asp:ListItem>Sugar</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="lblMarketValue" runat="server" Text="Market Value of Commodity/ Qtls."
                                                        Font-Size="8pt" ForeColor="navy" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtmarketval" onblur="NumericDecimalCheck(this,3)" AutoComplete="off" runat="server"
                                                        Width="100px" MaxLength="15" TabIndex="14"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
                                                        ControlToValidate="txtmarketval" Display="Dynamic" 
                                                        ErrorMessage="Plz enter the Market value upto five decimal place" 
                                                        SetFocusOnError="True" ValidationExpression="^\d{1,9}(\.\d{1,5})?$" Width="6px">*</asp:RegularExpressionValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 5px" colspan="4">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lblCommodity0" runat="server" Font-Bold="True" Font-Size="8pt" 
                                                        ForeColor="Navy" Text="Storage(Rate) starting Date:"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                                    <asp:CalendarExtender ID="TextBox1_CalendarExtender" runat="server" 
                                                        Enabled="True" TargetControlID="TextBox1" Format="dd/MM/yyyy">
                                                    </asp:CalendarExtender>
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="lblCommodity1" runat="server" Font-Bold="True" Font-Size="8pt" 
                                                        ForeColor="Navy" Text="Remark:"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <strong><span
                                                            style="font-size: 8pt"></span>
                                                    <asp:TextBox ID="TextBox2" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                    </strong></td>
                                            </tr>
                                            <tr>
                                                <td style="height: 5px" colspan="4">
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
                        <td style="height: 5px">
                        </td>
                    </tr>
                    <tr>
                        <td align="center" valign="top">
                            <fieldset style="width: 980px; border: 1px solid navy;">
                                <center>
                                    <div>
                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr style="background-color: #0bb6e6; height: 25px">
                                                <td colspan="6" align="center">
                                                    <asp:Label ID="lblStackingInfo" runat="server" Style="position: static" Text="Stacking Information"
                                                        Font-Bold="True" ForeColor="whitesmoke" Font-Size="12pt"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td style="height: 5px" colspan="6">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 5px" colspan="6">
                                                    <asp:Label ID="lblStackingInform" runat="server" Text="Note :-स्टैक की जानकारी भरकर 'Add Stack' Button पर क्लिक करें,अगर दूसरा स्टैक जोड़ना है,तो यही प्रक्रिया दोहराएँ !"
                                                        ForeColor="red" Font-Bold="true" Font-Size="8pt"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 5px" colspan="6">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lblGodownNo" runat="server" Text="Godown No." Font-Size="8pt" ForeColor="navy"
                                                        Font-Bold="true"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:DropDownList ID="ddlgodownlist" runat="server" AutoPostBack="True" Height="25px"
                                                        Width="330px" OnSelectedIndexChanged="ddlgodownlist_SelectedIndexChanged" TabIndex="16"
                                                        ValidationGroup="SaveValid">
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="lblStackNo" runat="server" Text="Stack No." Font-Size="8pt" ForeColor="navy"
                                                        Font-Bold="true"></asp:Label>
                                                </td>
                                                <td align="left" colspan="3">
                                                    <asp:DropDownList ID="ddlstacklist" runat="server" DataTextField="Stack_Name" DataValueField="Stack_ID"
                                                        OnSelectedIndexChanged="ddlstacklist_SelectedIndexChanged" Height="25px" Width="155px"
                                                        AutoPostBack="True" TabIndex="17" ValidationGroup="SaveValid">
                                                    </asp:DropDownList>
                                                    <asp:Label ID="txtsvrdate" runat="server" Visible="False"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 5px" colspan="6">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lblNoofBags" runat="server" Text="No.of Bags" Font-Size="8pt" Font-Bold="true"
                                                        ForeColor="navy"></asp:Label></td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtnobags" onblur="Spc_validator(this)" runat="server" Width="100px" AutoComplete="off"
                                                         TabIndex="18"></asp:TextBox>*<asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                                            runat="server" ErrorMessage="No Of Bags Required !" ControlToValidate="txtnobags"
                                                            Visible="true" SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
                                                <td align="left">
                                                    <asp:Label ID="lblwt" runat="server" Text="Weight" Font-Size="8pt" ForeColor="navy"
                                                        Font-Bold="true"> </asp:Label>
                                                </td>
                                                <td colspan="3">
                                                    <asp:TextBox ID="txtwt" runat="server" onblur="NumericDecimalCheck(this, 5)" Width="100px" AutoComplete="off"
                                                        MaxLength="15" TabIndex="19"></asp:TextBox><span style="font-size: 8pt; color: #cc0000">*</span>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Weight is Required!!"
                                                        ControlToValidate="txtwt" Visible="true" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtwt"
                                                        Display="Dynamic" ErrorMessage="PLz enter Weight upto five place of decimal"
                                                        SetFocusOnError="True" ValidationExpression="^\d{1,9}(\.\d{1,5})?$"></asp:RegularExpressionValidator></td>
                                            </tr>
                                            <tr>
                                                <td style="height: 5px" colspan="6">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lblMaxCap" runat="server" Font-Size="8pt" Text="Maximum Capacity"
                                                        ForeColor="navy" Font-Bold="true"></asp:Label></td>
                                                <td align="left">
                                                    <asp:TextBox ID="MaxStackCap" runat="server" BackColor="#FFFFC0" ReadOnly="True"
                                                        Width="100px"></asp:TextBox>
                                                         <span style="color:Red; font-weight:bold">(Qtls.kgsgms)</span>
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="lblCurStackCap" runat="server" Font-Size="8pt" Text="Current Capacity"
                                                        ForeColor="navy" Font-Bold="true"></asp:Label></td>
                                                <td align="left">
                                                    <asp:TextBox ID="CurStackCap" runat="server" BackColor="#FFFFC0" ReadOnly="True"
                                                        Width="100px"></asp:TextBox>
                                                         <span style="color:Red; font-weight:bold">(Qtls.kgsgms)</span>
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="lblAvailable" runat="server" Font-Size="8pt" Text="Available Capacity"
                                                        ForeColor="navy" Font-Bold="true"></asp:Label></td>
                                                <td align="left">
                                                    <asp:TextBox ID="AvlStackCap" runat="server" BackColor="#FFFFC0" Width="100px" ReadOnly="True"></asp:TextBox>
                                                     <span style="color:Red; font-weight:bold">(Qtls.kgsgms)</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 10px" colspan="6">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6" align="center">
                                                    <asp:Button ID="btnadd" runat="server" Text="Add Stack" Width="100px" OnClick="btnadd_Click"
                                                        TabIndex="20" CssClass="BTNBLUE" />&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 5px" colspan="6">
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </center>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 5px">
                        </td>
                    </tr>
                    <tr>
                        <td align="center" valign="top">
                            <asp:GridView ID="gdstackingdetails" runat="server" AutoGenerateDeleteButton="True"
                                BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px"
                                CellPadding="4" OnRowCreated="gdstackingdetails_RowCreated" OnRowDeleting="gdstackingdetails_RowDeleting"
                                OnPreRender="gdstackingdetails_PreRender" TabIndex="21">
                                <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                                <RowStyle BackColor="White" ForeColor="#330099" />
                                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                                <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 5px">
                                                    <asp:Label ID="lblmsg" runat="server" ForeColor="Red" 
                                Font-Bold="True" Font-Size="Small"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Button ID="btnsave" runat="server" Text="Save Details" Width="100px" CssClass="BTNBLUE"
                                OnClick="btnsave_Click" ValidationGroup="SaveValid" />
                            <asp:Button ID="btn_Close" runat="server" Text="Close" Width="100px" OnClick="btn_Close_Click"
                                CssClass="BTNBLUE" CausesValidation="false" />
                            <asp:Button ID="btnNewMC" runat="server" Text="New Opening Balance" OnClick="btnNewMC_Click"
                                Width="150px" CausesValidation="False" CssClass="BTNBLUE" Visible="false" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center" runat="server" id="showmsg" height="30px" style="font-size: medium; font-weight: bolder; color: #800000">
                            Your WHR NO. is -
                                                    <asp:Label ID="lbl_whrno" runat="server"></asp:Label>
                                                    &nbsp; kindly note this

                        </td>
                    </tr>
                    <tr>
                        <td style="height: 5px">
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                ShowSummary="False" />
                        </td>
                    </tr>
                </table>
                     
            </div>
        </center>
    </fieldset>
   
</asp:Content>
