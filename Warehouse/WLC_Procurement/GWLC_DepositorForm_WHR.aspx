<%@ Page Language="C#" MasterPageFile="~/MasterPage/PrivateWarehouse.master" AutoEventWireup="true" CodeFile="GWLC_DepositorForm_WHR.aspx.cs" Inherits="WarehouseLevel_WLC_Procurement_GWLC_DepositorForm_WHR" Title="Untitled Page" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../../css/CSS.css" rel="stylesheet" type="text/css" />
    <script src="../../JS/Extension.min.js" type="text/javascript"></script>
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
            function validateData() {
                //clear textbox
                return true;
            }
    </script>
 <script type="text/javascript" src="http://maps.googleapis.com/maps/api/js?sensor=false"></script>
 <script type="text/javascript">
     function LLFunction() {
         if (navigator.geolocation) {
             navigator.geolocation.getCurrentPosition(function(p) {
                 var LatLng = new google.maps.LatLng(p.coords.latitude, p.coords.longitude);
//                 var Latitude = p.coords.latitude;
//                 var longitude = p.coords.longitude;
//                 document.cookie = "Lati=" + Latitude;
                 //                 document.cookie = "Longi=" + longitude;
//                 document.getElementById("txtlet").value = p.coords.latitude;
                 document.getElementById("n_lati").value = p.coords.latitude;
//                 document.getElementById("txtlong").value = p.coords.longitude;
                 document.getElementById("n_long").value = p.coords.longitude;
             });
         } else {
             alert('Geo Location feature is not supported in this browser.');
         }
     }
    window.onload = LLFunction();
</script>
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
        //function ShowCalendar(SourceControl, DestinationControl)

        //percentage
        function validate() {
            // Percent = document.frmPost.percent.value

            if ((ctl00_ContentPlaceHolder1_txtmoisturecontent.value.indexOf(".") == -1) && (ctl00_ContentPlaceHolder1_txtmoisturecontent.value.length >= 3)) {
                alert("Percentage format is not correct");
                ctl00_ContentPlaceHolder1_txtmoisturecontent.value = "";
                ctl00_ContentPlaceHolder1_txtmoisturecontent.focus();
                return false;
            }
            if ((ctl00_ContentPlaceHolder1_txtmoisturecontent.value.indexOf(".")) == 4 || (ctl00_ContentPlaceHolder1_txtmoisturecontent.value.indexOf(".")) == 3 || (ctl00_ContentPlaceHolder1_txtmoisturecontent.value.indexOf(".")) == 0) {
                alert("Invalid Percentage2");
                ctl00_ContentPlaceHolder1_txtmoisturecontent.value = "";
                ctl00_ContentPlaceHolder1_txtmoisturecontent.focus();
                return false;
            }
            if (isNaN(ctl00_ContentPlaceHolder1_txtmoisturecontent.value) == true) {
                alert("Enter Numeric values");
                ctl00_ContentPlaceHolder1_txtmoisturecontent.value = "";
                ctl00_ContentPlaceHolder1_txtmoisturecontent.focus();
                return false;
            }
            return true;
        }

        function popMe(url) {
            var newWindow;
            newWindow = window.open(url, 'MyWin', 'width=275,height=390,top=1,left=1');
        }


    </script>

    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
    <ProgressTemplate>
     <div class="divWaiting">            
	<asp:Label ID="lblWait" runat="server" 
	Text=" " />
	<asp:Image ID="imgWait" runat="server" 
	ImageAlign="Middle" ImageUrl="~/images/mpwlc3.gif" />
  </div>
    
    </ProgressTemplate>
    </asp:UpdateProgress>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    
    <fieldset style="width: 980px; border: 2px solid navy; height: auto">
        <center>
            <div>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr id="Tr1" runat="server" visible="false">
                                <td align="center" valign="top">
                                    <fieldset style="width: 980px; border: 1px solid navy;">
                                        <center>
                                            <div>
                                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                    <tr style="background-color: #0bb6e6; height: 25px">
                                                        <td colspan="4" align="center">
                                                            <asp:Label ID="lblDepositorFormhead" runat="server" Text="Depositor Form (WHR)" Font-Bold="true"
                                                                Font-Size="12pt" ForeColor="whitesmoke"></asp:Label>
                                                        </td>
                                                       
                                                    </tr>
                                                    <tr align="center">
                                                        <td colspan="2" align="left">
                                                            <asp:Label ID="lblInstruction" runat="server" Text="निर्देश :- "
                                                                Font-Bold="True" ForeColor="navy" Font-Size="10pt"></asp:Label>
                                                            <a href="#" style="text-decoration: underline;
                                                                color: Red; font-size: 10pt">एक जमा फॉर्म के विरुद्ध एक WHR ही जारी करे।</a>
                                                        </td>
                                                         <td colspan="2" align="left">
                                                            <asp:Label ID="Label3" runat="server" Text="Note :- "
                                                                Font-Bold="True" ForeColor="navy" Font-Size="10pt"></asp:Label>
                                                            <a href="javascript:popMe('../../SampleQuantity.htm');" style="text-decoration: underline;
                                                                color: Red; font-size: 10pt">Values in Rs. and Qty. in Qtls.kgsgms</a>
                                                        </td>
                                                    </tr>
                                                  
                                                    <tr align="center">
                                                        <td colspan="4" style="height: 10px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                     <td align="left" style="width: 200px">
                                                            <asp:Label ID="lblDepositorType" runat="server" Font-Size="12px" Font-Bold="true"
                                                                Text="Type of Depositor" ForeColor="navy"></asp:Label>
                                                        </td>
                                                        <td align="left" style="width: 200px">
                                                            <asp:DropDownList ID="ddldepositortype" runat="server" AutoPostBack="True" Width="155px"
                                                                Height="25px" CssClass="tb6" 
                                                                onselectedindexchanged="ddldepositortype_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td align="left" style="width: 200px" valign="middle">
                                                            <asp:Label ID="lblDepositorName" runat="server" Text="Depositor Name" ForeColor="navy"
                                                                Font-Bold="true" Font-Size="8pt"></asp:Label>
                                                        </td>
                                                        <td align="left" style="width: 200px" valign="bottom">
                                                            <asp:DropDownList ID="ddldepositorname" runat="server" Width="250px" Height="25px" AutoPostBack="true"
                                                                OnPreRender="ddldepositorname_PreRender" TabIndex="1" CssClass="tb6" 
                                                                onselectedindexchanged="ddldepositorname_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                       
                                                    </tr>
                                                    <tr align="center">
                                                        <td colspan="4" style="height: 5px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <asp:Label ID="lblCommodity" runat="server" Text="Commodity" ForeColor="navy" Font-Bold="true"
                                                                Font-Size="8pt"></asp:Label>
                                                        </td>
                                                        <td align="left" valign="middle">
                                                            <asp:DropDownList ID="ddlcommodity" runat="server" Width="155px" Height="25px" TabIndex="4"
                                                                CssClass="tb6" onselectedindexchanged="ddlcommodity_SelectedIndexChanged" 
                                                                AutoPostBack="True">
                                                            </asp:DropDownList>
                                                        </td>
                                                         <td align="left">
                                                            <asp:Label ID="lblWHRDate" runat="server" Text="Deposite Date" Font-Bold="true" Font-Size="8pt"
                                                                ForeColor="navy"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtdepositdate" runat="server" MaxLength="12" Width="150px" 
                                                                CssClass="tb6"></asp:TextBox>
                                                            <asp:ImageButton ID="Imgpop" runat="server" ImageUrl="~/images/cal.gif" CausesValidation="false" />
                                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" TargetControlID="txtdepositdate"
                                                                Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy" PopupButtonID="Imgpop">
                                                            </asp:CalendarExtender>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtdepositdate"
                                                                Display="Dynamic" ErrorMessage="Deposite Date is required" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                         <tr align="center">
                                                        <td colspan="4" style="height: 5px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <asp:Label ID="lblcropyear" runat="server" Font-Bold="True" Font-Size="8pt" 
                                                                ForeColor="Navy" Text="Crop Year:"></asp:Label>
                                                        </td>
                                                        <td align="left" valign="middle">
                                                            <asp:DropDownList ID="ddlcropyr" runat="server" AutoPostBack="True" 
                                                                onselectedindexchanged="ddlcropyr_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                      <td align="left">
                                                            <asp:Label ID="lblGodown" runat="server" Font-Bold="True" Font-Size="8pt" ForeColor="Navy"
                                                                Text="Godown Name"></asp:Label>
                                                        </td>
                                                        <td align="left" valign="middle">
                                                            <asp:DropDownList ID="ddl_godown" runat="server" Width="400px" AutoPostBack="True"
                                                                CssClass="tb6" OnSelectedIndexChanged="ddl_godown_SelectedIndexChanged" Height="25px">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </center>
                                    </fieldset>
                                </td>
                            </tr>
                            <tr align="left">
                                <td style="height: 5px">

                                    &nbsp;</td>
                               
                            </tr>
                            <tr>
                                <td align="center" valign="top">
                                    <fieldset style="width: 980px; border: 1px solid navy;">
                                        <center>
                                            <div>
                                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                    <tr>
                                                        <td style="height: 5px">
                                                        </td>
                                                    </tr>
                                                    <tr align="center">
                                                        <td align="center">
                                                            <asp:Label ID="lbl_message" runat="server" ForeColor="Red" Font-Bold="true" Font-Size="12pt"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <asp:Label ID="lblRowCount" runat="server" ForeColor="navy" Font-Bold="true" Text="" Visible="false"
                                                                Font-Size="10pt"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 5px">
                                                        </td>
                                                    </tr>
                                                    <tr align="center">
                                                        <td align="center" valign="middle">
                                                            <asp:GridView ID="gdtruckdetail" runat="server" CellPadding="4" ForeColor="navy"
                                                                AutoGenerateColumns="false" GridLines="Both" OnRowCreated="gdtruckdetail_RowCreated"
                                                                Width="980px" TabIndex="7">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Select">
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="ckboxtrucklist" runat="server" Enabled="false" Checked="true"/>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="Challan_No" HeaderText="Challan No.">
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Truck_No" HeaderText="Truck No.">
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity">
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Acpt_FCIRO_No" HeaderText="Depositor No.">
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="AcceptDate" HeaderText="Acceptance Date">
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Bags" HeaderText="No. of Bags">
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Weight" HeaderText="Weight(In QTLS.)">
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Mode_of_weighment" HeaderText="Weighment Mode">
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Beamscale" HeaderText="Beamscale">
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Moisture" HeaderText="Moisture(%)">
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="DepositDate" HeaderText="Deposit Date">
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                     <asp:BoundField DataField="Receipt_ID" HeaderText="Receipt_ID">
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                </Columns>
                                                                <FooterStyle BackColor="#FFCC66" Font-Bold="false" ForeColor="Transparent" Font-Size="7pt" />
                                                                <RowStyle BackColor="#FFFBD6" ForeColor="#333333" Font-Size="7pt" Width="100px" />
                                                                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="False" ForeColor="Navy" Font-Size="7pt" />
                                                                <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" Font-Size="7pt" />
                                                                <HeaderStyle BackColor="#719cb6" Font-Bold="false" ForeColor="White" HorizontalAlign="center"
                                                                    Height="20px" Font-Size="7pt" Width="100px" />
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
                            <tr align="center">
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
                                                        <td colspan="4" align="center">
                                                            <asp:Label ID="Label2" runat="server" Text="Depositor Form Details" Font-Bold="true"
                                                                Font-Size="12pt" ForeColor="whitesmoke"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr align="center">
                                                        <td colspan="4" style="height: 5px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" style="width: 200px">
                                                            <asp:Label ID="lblDepositDate" runat="server" Text="WHR Date" ForeColor="navy" Font-Bold="true"
                                                                Font-Size="8pt"></asp:Label>
                                                            <span class="style1"></span></td>
                                                        <td align="left" style="width: 200px">
                                                            <asp:TextBox ID="txtwhrdate" runat="server" MaxLength="10" Width="150px" TabIndex="12" 
                                                                CssClass="disable_future_dates" ReadOnly="true" ></asp:TextBox>

                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtwhrdate"
                                                                Display="Dynamic" ErrorMessage="WHR Date is required" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                            <%--<asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/cal.gif" CausesValidation="false" />--%>
                                                            <%--<asp:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" TargetControlID="txtwhrdate"
                                                                Format="dd/MM/yyyy" OnClientDateSelectionChanged="checkDate" TodaysDateFormat="dd/MM/yyyy"   PopupButtonID="ImageButton1">
                                                            </asp:CalendarExtender>--%>
                                                        </td>
                                                        <td align="left" style="width: 200px">
                                                            <asp:Label ID="lblLotNo" runat="server" Text="Lot No." ForeColor="navy" Font-Bold="true"
                                                                Font-Size="8pt"></asp:Label>
                                                        </td>
                                                        <td align="left" colspan="3">
                                                            <asp:TextBox ID="txtlotnumber" runat="server" MaxLength="30" Width="150px" CssClass="tb6"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr align="center">
                                                        <td colspan="4" style="height: 5px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <asp:Label ID="lbltotalReceivedBags" runat="server" Text="Total Bags Received" ForeColor="navy"
                                                                Font-Bold="true" Font-Size="8pt"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txttotalbags" runat="server" CssClass="tb6" Enabled="False" MaxLength="30"
                                                                Width="150px" BackColor="#FFFFC0"></asp:TextBox>
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="lblTotalQuantityReceived" runat="server" Text="Total Quantity Received"
                                                                ForeColor="navy" Font-Bold="true" Font-Size="8pt"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txttotalweight" runat="server" Enabled="False" MaxLength="30" Width="150px"
                                                                BackColor="#FFFFC0" CssClass="tb6"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr align="center">
                                                        <td colspan="4" style="height: 5px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <asp:Label ID="lblWeighmentMode" runat="server" Text="Mode of Weighment" ForeColor="navy"
                                                                Font-Bold="true" Font-Size="8pt"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:DropDownList ID="ddlmode" runat="server" Width="155px" Height="25px" CssClass="tb6" Enabled="false">
                                                                <asp:ListItem Value="10%">10%</asp:ListItem>
                                                                <asp:ListItem Selected="True" Value="100%">100%</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="lblWeightmentOn" runat="server" Text="Weightment On" ForeColor="navy"
                                                                Font-Bold="true" Font-Size="8pt"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:DropDownList ID="ddlweighon" runat="server" Width="155px" Height="25px" CssClass="tb6" Enabled="false">
                                                                <asp:ListItem>LWB</asp:ListItem>
                                                                <asp:ListItem>Beam Scale</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr align="center">
                                                        <td colspan="4" style="height: 5px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <asp:Label ID="lblAvgMoistureContent" runat="server" Text="Moisture Content(%) From"
                                                                ForeColor="navy" Font-Bold="true" Font-Size="8pt"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtmoisturecontent" runat="server" MaxLength="10" Width="150px"
                                                                TabIndex="8" CssClass="tb6"></asp:TextBox>
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="lbl_To" runat="server" Text="To" ForeColor="navy" Font-Bold="true"
                                                                Font-Size="8pt"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtmoistcontent_To" runat="server" MaxLength="10" TabIndex="9" Width="150px"
                                                                CssClass="tb6"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr align="center">
                                                        <td colspan="4" style="height: 5px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <asp:Label ID="lblMarketValue" runat="server" Text="Market Value(per Qtls/Bells)" ForeColor="Navy"
                                                                Font-Bold="True" Font-Size="8pt"></asp:Label>
                                                        </td>
                                                        <td align="left" >
                                                            <asp:TextBox ID="txtmarketvalue" runat="server" MaxLength="10" Width="150px" TabIndex="10"
                                                                CssClass="tb6"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                        
                                                            <asp:Label ID="lblMarketValue0" runat="server" Font-Bold="True" Font-Size="8pt" 
                                                                ForeColor="Navy" Text="Storage(Rate) Starting Date:"></asp:Label>
                                                        
                                                        </td>
                                                        <td align="left">
                                                        
                                                            <asp:TextBox ID="TextBox1" runat="server" Height="16px" Width="148px"></asp:TextBox>
                                                            <asp:CalendarExtender ID="TextBox1_CalendarExtender" runat="server" OnClientDateSelectionChanged="checkDate" 
                                                                Enabled="True" TargetControlID="TextBox1" Format="dd/MM/yyyy">
                                                            </asp:CalendarExtender>
                                                        
                                                        </td>
                                                    </tr>
                                                    <tr align="center">
                                                        <td colspan="4" style="height: 5px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                     <td align="left">
                                            <asp:Label ID="lblCategoty_Non" runat="server" Font-Size="8pt" ForeColor="navy" Font-Bold="true"
                                                Text="Categoty"></asp:Label></td>
                                        <td align="left">
                                            <asp:DropDownList ID="ddlCategory_Non" runat="server" Height="25px" Width="155px"
                                                TabIndex="9">
                                            </asp:DropDownList>
                                        </td> <td align="left">
                                                            <asp:Label ID="lblSorcePfArrival" runat="server" Text="Source of Arrival" Font-Bold="true"
                                                                Font-Size="8pt" ForeColor="navy"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:DropDownList ID="ddlsource" runat="server" Width="155px" AutoPostBack="True"
                                                                Height="25px" Enabled="false">
                                                            </asp:DropDownList>
                                                        </td>
                                                   
                                                    <td align="left">
                                                            <br />
                                                            <br />
                                                    </td>
                                                    <td align="left">
                                                        
                                                    </td>
                                                    </tr>
                                                     <tr align="center">
                                                        <td colspan="4" style="height: 5px">
                                                        </td>
                                                    </tr>
                                                    <tr id="Tr2" runat="server" visible="true">
                                                        <td align="left">
                                                    
                                                        <asp:Label ID="lblMarketValue1" runat="server" Font-Bold="True" Font-Size="8pt" 
                                                            ForeColor="Navy" Text="Remark"></asp:Label>
                                                    
                                                    </td>
                                                    <td  align="left" colspan="3">
                                                    
                                                        <asp:TextBox ID="TextBox2" Width="550px" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                    
                                                    </td>
                                                         <td>
              <%--  <input id="txtlet"  type="text"/>--%>
                <input type="hidden" id="n_lati" name="n_lati" />
            </td>
             <td>
             <%--<input id="txtlong" type="text" />--%>
                <input type="hidden" id="n_long" name="n_long" />
            </td>
                                                    </tr>
                                                    <tr align="center">
                                                        <td colspan="4" style="height: 5px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <asp:Label ID="lblWHRNumber" runat="server" Text="WHR No" Visible="false" Font-Bold="true"
                                                                Font-Size="8pt" ForeColor="navy"></asp:Label>
                                                        </td>
                                                        <td align="left" colspan="3">
                                                            <asp:TextBox ID="txtwhrnumber" runat="server" MaxLength="30" Visible="false" Width="150px"
                                                                TabIndex="11" ReadOnly="True"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr align="center" id="showc" runat="server" visible="false">
                                                        <td colspan="4" style="height: 10px; font-size: medium; color: #800000; font-weight: bolder;">
                                                            &nbsp;Your WHR NO. is -
                                                            <asp:Label ID="lbl_whrno" runat="server"></asp:Label>
                                                            &nbsp; kindly note this
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4" align="center">
                                                            <asp:Button ID="btn_save" runat="server" Text="Submit" CssClass="BTNBLUE" Width="100px"
                                                                Enabled="False" TabIndex="13" ValidationGroup="SaveValid" OnClick="btn_save_Click" />
                                                            &nbsp;&nbsp;&nbsp;
                                                            <asp:Button ID="btn_Close" runat="server" Text="Cancel" OnClick="btn_Close_Click"
                                                                CssClass="BTNBLUE" Width="100px" CausesValidation="false" />
                                                            &nbsp;&nbsp;&nbsp;
                                                            <asp:Button ID="btnNewMC" runat="server" Text="New Depositor Form" OnClick="btnNewMC_Click"
                                                                Width="150px" CausesValidation="False" TabIndex="14" Visible="false" CssClass="BTNBLUE" Enabled="false" />
                                                        </td>
                                                    </tr>
                                                    <tr align="center">
                                                        <td colspan="4" style="height: 5px">
                                                            <asp:Label ID="lbl_didid" runat="server" Visible="False"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </center>
                                    </fieldset>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </center>
    </fieldset>

    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>



