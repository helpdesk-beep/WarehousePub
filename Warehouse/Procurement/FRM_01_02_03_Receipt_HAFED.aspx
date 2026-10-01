<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="FRM_01_02_03_Receipt_HAFED.aspx.cs" Inherits="Procurement_FRM_01_02_03_Receipt_HAFED" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
</style>

<script type="text/javascript" src="<%= ResolveUrl("~/NEW_CSS/js/jquery-1.12.4.js") %>"></script>
<script type="text/javascript">
    function ShowProgress() {
        setTimeout(function () {
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
    $('form').live("submit", function () {
        ShowProgress();
    });
</script>


    <script language="javascript" type="text/javascript">

         function compare()
		{
		     a=document.getElementById('txtStackAvailable');
		     b = document.getElementById('txtStackWt');
		    //Ram
		    if(b.value<0)
		    {
		        alert("Negative value not allowed");
		        b.value="";
		        b.focus;
		    }
		    else 
		    {
		        if (Number(a.value) <Number(b.value))
		         {
		         alert("Stack insufficient to stack!!Pl check the value");
                 
		         }
            }
        } 
      
        function popMe(url)
          {
            var newWindow;
            newWindow=window.open(url,'MyWin','width=275,height=390,top=1,left=1');
       
          } 
        function OpenWindow()
        {
        var asid=0;
        asid=document.getElementById("txtArrivalSrcId").value;
         
          window.open("Gate_Pass.aspx?src=MC&id="+asid,"_new","height=800,width=780");
          

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

   <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>--%>
    
    <fieldset style="width: 1000px; border: 2px solid navy;">
        <center>
            <div class="blockMe">
                <table cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr style="background-color: #0bb6e6; height: 25px">
                        <td colspan="6" align="center">
                            <asp:Label ID="lblDepositDetail" runat="server" Text="Deposit at Issue Centre " Font-Size="12pt"
                                ForeColor="whitesmoke" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" style="height: 5px">
                        </td>
                    </tr>
                      <tr>
                        <td colspan="6" align="center">
                            <asp:Label ID="lblmsg" runat="server" Font-Size="10pt" ForeColor="Red" EnableViewState="False"
                                Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" style="height: 5px">
                        </td>
                    </tr>
                    <tr align="center">
                        <td colspan="6" align="left">
                            &nbsp;
                            <asp:Label ID="lblInstruction" runat="server" Text="Note :- Mark (*) fields are Mandatory"
                                Font-Bold="True" ForeColor="red" Font-Size="10pt"></asp:Label>
                            <a href="javascript:popMe('../../SampleQuantity.htm');" style="text-decoration: underline;
                                color: Navy">(Qty. in Qtls.kgsgms)</a>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" style="height: 5px">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6">
                            <asp:Panel ID="pnlFCI_OTDepot" runat="server" Width="100%" Visible="false">
                                <table cellpadding="0" cellspacing="0">
                                    <tr id="trFCIa_dist_depo">
                                        <td align="left" style="width: 200px">
                                            <asp:Label ID="lblA_Dist" runat="server" Text="District" Font-Size="8pt" ForeColor="navy" Font-Bold="true"></asp:Label></td>
                                        <td align="left" style="width: 200px">
                                            <asp:DropDownList ID="ddlA_Dist" runat="server" TabIndex="1" Width="155px" Height="25px">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="left" style="width: 200px">
                                            <asp:Label ID="lblA_Depo" runat="server" Font-Size="8pt" ForeColor="navy" Font-Bold="true"></asp:Label></td>
                                        <td align="left" style="width: 200px">
                                            <asp:DropDownList ID="ddlA_Depo" runat="server" Width="155px" Height="25px" TabIndex="2">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="height: 5px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="lblTCNo" runat="server" Font-Size="8pt" ForeColor="navy" Font-Bold="true"
                                                Text="Truck Challan No."></asp:Label></td>
                                        <td align="left">
                                            <asp:TextBox ID="txtTCNo" runat="server" MaxLength="20" Width="150px" BackColor="#FFFFC0"></asp:TextBox></td>
                                        <td align="left">
                                            <asp:Label ID="lblTruckNo" runat="server" Font-Size="8pt" ForeColor="navy" Font-Bold="true"
                                                Text="Truck No."></asp:Label></td>
                                        <td align="left">
                                            <asp:TextBox ID="txtTruckNo" runat="server" MaxLength="20" Width="155px" BackColor="#FFFFC0"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="height: 5px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="lblCommodity" runat="server" Font-Size="8pt" ForeColor="navy" Font-Bold="true"
                                                Text="Commodity"></asp:Label></td>
                                        <td align="left">
                                            <asp:DropDownList ID="ddlCommodity" runat="server" Width="155px" Height="25px" TabIndex="3">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblCategoty" runat="server" Font-Size="8pt" ForeColor="navy" Font-Bold="true"
                                                Text="Categoty"></asp:Label></td>
                                        <td align="left">
                                            <asp:DropDownList ID="ddlCategory" runat="server" Width="155px" Height="25px" TabIndex="4">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="height: 5px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="lblDepositDate" runat="server" Font-Size="8pt" ForeColor="navy" Font-Bold="true"
                                                Text="Date Of Deposit (DD/MM/YYYY)"></asp:Label></td>
                                        <td align="left">
                                            <asp:TextBox ID="txtDepositDate" runat="server" MaxLength="10" Width="150px" onblur="validateDatenew('txtDepositDate')"
                                                TabIndex="5"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtDepositDate_CalendarExtender" runat="server" 
                                                Enabled="True" TargetControlID="txtDepositDate" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy">
                                            </cc1:CalendarExtender>
                                          <%--  <a onclick="ShowCalendar(ctl00_ContentPlaceHolder1_txtDepositDate, ctl00_ContentPlaceHolder1_txtDepositDate);"
                                                href="javascript:;">
                                                <img height="16" id="imgFCI" runat="server" alt="Click Here to Pick up the date"
                                                    src="../../images/cal.gif" width="16" border="0" /></a>--%>
                                        </td>
                                        <td align="left">
                                            <asp:HiddenField ID="hfRoNo" runat="server" />
                                            <asp:Label ID="lblScheme" runat="server" Font-Size="8pt" ForeColor="navy" Font-Bold="true"
                                                Text="Scheme" Visible="False"></asp:Label>
                                            <asp:DropDownList ID="ddlScheme" runat="server" Height="25px" Width="155px" Visible="False">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="left">
                                            <asp:HiddenField ID="hfBags" runat="server" />
                                            <asp:HiddenField ID="hfRODate" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="height: 5px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="lblBags" runat="server" Font-Size="8pt" ForeColor="Navy" Font-Bold="True"
                                                Text="No. of Bags/Bells Deposited"></asp:Label></td>
                                        <td align="left">
                                            <asp:TextBox ID="txtBags" runat="server" BackColor="#FFFFC0" MaxLength="20" onkeyup="NumericDecimalCheck(this,2)"
                                                Width="150px"></asp:TextBox></td>
                                        <td align="left">
                                            <asp:Label ID="lblQtyDeposit" runat="server" Font-Size="8pt" ForeColor="navy" Font-Bold="true"
                                                Text="Qty. Deposited"></asp:Label></td>
                                        <td align="left">
                                            <asp:TextBox ID="txtQtyDeposit" runat="server" MaxLength="20" Width="150px" BackColor="#FFFFC0"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="height: 5px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="lblWCMNo" runat="server" Font-Size="8pt" ForeColor="navy" Font-Bold="true"
                                                Text="WCM No."></asp:Label></td>
                                        <td align="left">
                                            <asp:TextBox ID="txtWCMNo" runat="server" MaxLength="20" Width="150px" TabIndex="6"></asp:TextBox></td>
                                        <td align="left">
                                            <asp:Label ID="lblweigmentMode" runat="server" Font-Size="8pt" ForeColor="navy" Font-Bold="true"
                                                Text="Mode of weigment"></asp:Label></td>
                                        <td align="left">
                                            <asp:DropDownList ID="ddlWeigmentMode" runat="server" Width="155px" Height="25px"
                                                TabIndex="7">
                                                <asp:ListItem>10%</asp:ListItem>
                                                <asp:ListItem>100%</asp:ListItem>
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="height: 5px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="lblBagsAcceptable" runat="server" Font-Size="8pt" ForeColor="Navy"
                                                Font-Bold="True" Text="No. of Bags/Bells Recieved"></asp:Label></td>
                                        <td align="left">
                                            <asp:TextBox ID="txtBagsAcceptable" runat="server" MaxLength="18" onkeyup="NumericDecimalCheck(this,2)"
                                                Width="150px" TabIndex="8"></asp:TextBox></td>
                                        <td align="left">
                                            <asp:Label ID="lblQtyAcceptable" runat="server" Font-Size="8pt" ForeColor="navy"
                                                Font-Bold="true" Text="Qty. Recieved"></asp:Label></td>
                                        <td align="left">
                                            <asp:TextBox ID="txtQtyAcceptable" runat="server" MaxLength="18" Width="150px" TabIndex="9"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="height: 5px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="lblTransporter" runat="server" Font-Size="8pt" ForeColor="navy" Font-Bold="true"
                                                Text="Transporter"></asp:Label></td>
                                        <td align="left">
                                            <asp:DropDownList ID="ddlTransporter" runat="server" Width="155px" Height="25px"
                                                TabIndex="10">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblCMR_Rice_Miller" runat="server" Font-Size="8pt" ForeColor="navy"
                                                Font-Bold="true" Text="Miller"></asp:Label></td>
                                        <td align="left">
                                            <asp:DropDownList ID="ddlCMR_Rice_Miller" runat="server" Width="155px" Height="25px"
                                                TabIndex="11">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="height: 5px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="lblMoisture" runat="server" Font-Size="8pt" ForeColor="navy" Font-Bold="true"
                                                Text="Mositure Content"></asp:Label></td>
                                        <td align="left">
                                            <asp:TextBox ID="txtMoisture" runat="server" MaxLength="13" Width="150px" onkeyup="NumericDecimalCheck(this,2)"
                                                TabIndex="12"></asp:TextBox></td>
                                        <td align="left">
                                        </td>
                                        <td align="left">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="height: 5px">
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6">
                            <asp:Panel ID="pnlNONMPSCSC" runat="server" Width="100%">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="left" style="width: 200px">
                                            <asp:Label ID="lblDepositorType" runat="server" Font-Size="8pt" Text="Type of Depositor"
                                                ForeColor="navy" Font-Bold="true"></asp:Label></td>
                                        <td align="left" style="width: 200px">
                                            <asp:DropDownList ID="ddldepositortype" runat="server" AutoPostBack="True" Height="25px"
                                                OnSelectedIndexChanged="ddldepositortype_SelectedIndexChanged" TabIndex="1" Width="155px">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="left" style="width: 200px">
                                            <asp:Label ID="lblDepositorName" runat="server" Font-Size="8pt" Text="Depositor Name"
                                                ForeColor="navy" Font-Bold="true"></asp:Label></td>
                                        <td align="left">
                                            <asp:DropDownList ID="ddlDepositor" runat="server" Height="25px" OnSelectedIndexChanged="ddlDepositor_SelectedIndexChanged"
                                                TabIndex="2" Width="155px" AutoPostBack="True">
                                                <asp:ListItem Text="HAFED" Value="14966"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="height: 5px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="lblSource" runat="server" Font-Size="8pt" ForeColor="navy" Font-Bold="true"
                                                Text="Source of Arrival"></asp:Label></td>
                                        <td align="left">
                                            <asp:DropDownList ID="ddlArrival_Source" runat="server" AutoPostBack="True" Height="25px"
                                                Width="155px" OnSelectedIndexChanged="ddlArrival_Source_SelectedIndexChanged"
                                                TabIndex="3">
                                                <asp:ListItem Text="Other Source" Value="06"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td align="left">
                                        </td>
                                        <td align="left">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="height: 5px">
                                        </td>
                                    </tr>
                                    <tr runat="server" visible="false">
                                        <td align="left">
                                            <asp:Label ID="lblDistrict" runat="server" Font-Size="8pt" ForeColor="navy" Font-Bold="true"
                                                Text="District"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="ddlDist_Non" runat="server" AutoPostBack="True" Height="25px"
                                                Width="155px" OnSelectedIndexChanged="ddlDist_Non_SelectedIndexChanged" TabIndex="4">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblDepot" runat="server" Font-Size="8pt" ForeColor="navy" Font-Bold="true"
                                                Text="Branch"></asp:Label>
                                            <asp:Label ID="lblSSociety" runat="server" Font-Size="8pt" ForeColor="navy" Font-Bold="true"
                                                Text="Source Society"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="ddlDepo_Non" runat="server" TabIndex="5" Width="155px" Height="25px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="height: 5px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="lblTCNo_Non" runat="server" Font-Size="8pt" ForeColor="navy" Font-Bold="true"
                                                Text="Truck Challan No."></asp:Label></td>
                                        <td align="left">
                                            <asp:TextBox ID="txtTCNo_Non" runat="server" MaxLength="20" Width="150px" TabIndex="6"></asp:TextBox></td>
                                        <td align="left">
                                            <asp:Label ID="lblTruckNo_Non" runat="server" Font-Size="8pt" ForeColor="navy" Font-Bold="true"
                                                Text="Truck No."></asp:Label></td>
                                        <td align="left">
                                            <asp:TextBox ID="txtTruckNo_Non" runat="server" MaxLength="20" Width="135px" TabIndex="7"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="height: 5px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="lblCommodity_Non" runat="server" Font-Size="8pt" ForeColor="navy"
                                                Font-Bold="true" Text="Commodity"></asp:Label></td>
                                        <td align="left">
                                            <asp:DropDownList ID="ddlCommodity_Non" runat="server" Height="25px" Width="155px"
                                                AutoPostBack="True" TabIndex="8" OnSelectedIndexChanged="ddlCommodity_Non_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblCategoty_Non" runat="server" Font-Size="8pt" ForeColor="navy" Font-Bold="true"
                                                Text="Categoty"></asp:Label></td>
                                        <td align="left">
                                            <asp:DropDownList ID="ddlCategory_Non" runat="server" Height="25px" Width="155px"
                                                TabIndex="9">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="height: 5px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="lblDepositDate_Non" runat="server" Font-Size="8pt" ForeColor="navy"
                                                Font-Bold="true" Text="Date Of Deposit (DD/MM/YYYY)"></asp:Label></td>
                                        <td align="left">
                                            <asp:TextBox ID="txtDepositDate_Non" runat="server" MaxLength="10" Width="150px"
                                                onblur="validateDatenew('txtDepositDate_Non')" TabIndex="10"></asp:TextBox>
                                            &nbsp; <a onclick="ShowCalendar(ctl00_ContentPlaceHolder1_txtDepositDate_Non, ctl00_ContentPlaceHolder1_txtDepositDate_Non);"
                                                href="javascript:;">
                                                <img height="16" id="imgCalNon" runat="server" alt="Click Here to Pick up the date"
                                                    src="../../images/cal.gif" width="16" border="0" />&nbsp;</a>
                                        </td>
                                        <td align="left">
                                        </td>
                                        <td align="left">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="height: 5px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="lblBags_Non" runat="server" Font-Size="8pt" ForeColor="navy" Font-Bold="true"
                                                Text="No. of Bags Deposited"></asp:Label></td>
                                        <td align="left">
                                            <asp:TextBox ID="txtBags_Non" runat="server" MaxLength="20" onkeyup="NumericDecimalCheck(this,2)"
                                                Width="150px" TabIndex="11"></asp:TextBox></td>
                                        <td align="left">
                                            <asp:Label ID="lblQtyDeposit_Non" runat="server" Font-Size="8pt" ForeColor="navy"
                                                Font-Bold="true" Text="Qty. Deposited"></asp:Label></td>
                                        <td align="left">
                                            <asp:TextBox ID="txtQty_Non" runat="server" MaxLength="20" Width="150px" BackColor="White"
                                                TabIndex="12"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="height: 5px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="lblWCMNo_Non" runat="server" Font-Size="8pt" ForeColor="navy" Font-Bold="true"
                                                Text="WCM No."></asp:Label></td>
                                        <td align="left">
                                            <asp:TextBox ID="txtWCMNo_Non" runat="server" MaxLength="20" Width="150px" TabIndex="13"></asp:TextBox></td>
                                        <td align="left">
                                            <asp:Label ID="lblweigmentMode_Non" runat="server" Font-Size="8pt" ForeColor="navy"
                                                Font-Bold="true" Text="Mode of weigment"></asp:Label></td>
                                        <td align="left">
                                            <asp:DropDownList ID="ddlWeighmentMode_Non" runat="server" Width="155px" Height="25px"
                                                TabIndex="14">
                                                <asp:ListItem>10%</asp:ListItem>
                                                <asp:ListItem>100%</asp:ListItem>
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="height: 5px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="lblBagsAcceptable_Non" runat="server" Font-Size="8pt" ForeColor="navy"
                                                Font-Bold="true" Text="No. of Bags Recieved"></asp:Label></td>
                                        <td align="left">
                                            <asp:TextBox ID="txtBagsAcceptable_Non" runat="server" MaxLength="18" onkeyup="NumericDecimalCheck(this,2)"
                                                Width="150px" BackColor="#FFFFC0"></asp:TextBox></td>
                                        <td align="left">
                                            <asp:Label ID="lblQtyAcceptable_Non" runat="server" Font-Size="8pt" ForeColor="navy"
                                                Font-Bold="true" Text="Qty. Recieved"></asp:Label></td>
                                        <td align="left">
                                            <asp:TextBox ID="txtQtyAcceptable_Non" runat="server" BackColor="#FFFFC0" MaxLength="18"
                                                Width="150px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="height: 5px">
                                        </td>
                                    </tr>
                                    <tr runat="server" visible="false">
                                        <td align="left">
                                            <asp:Label ID="lblTransporter_Non" runat="server" Font-Size="8pt" ForeColor="navy"
                                                Font-Bold="true" Text="Transporter"></asp:Label></td>
                                        <td align="left">
                                            <asp:DropDownList ID="ddlTransporter_Non" runat="server" Height="25px" Width="155px"
                                                TabIndex="15">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblCMR_Rice_Miller_Non" runat="server" Font-Size="8pt" ForeColor="navy"
                                                Font-Bold="true" Text="Miller" Visible="False"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="ddlMiller_Non" runat="server" Height="25px" Width="155px" Visible="False"
                                                TabIndex="16">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="height: 5px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="lblMoisture_Non" runat="server" Font-Size="8pt" ForeColor="navy" Font-Bold="true"
                                                Text="Mositure Content"></asp:Label></td>
                                        <td align="left">
                                            <asp:TextBox ID="txtMoisture_Non" runat="server" MaxLength="13" Width="150px" onkeyup="NumericDecimalCheck(this,2)"
                                                TabIndex="17"></asp:TextBox></td>
                                        <td align="left">
                                            <asp:Label ID="lblScheme_Non" runat="server" Font-Size="8pt" ForeColor="navy" Font-Bold="true"
                                                Text="Scheme" Visible="False"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="ddlScheme_Non" runat="server" Height="25px" Width="155px" Visible="False">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="height: 5px">
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" style="height: 5px">
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
                                                        Height="25px" OnSelectedIndexChanged="ddlGodownNo_SelectedIndexChanged" TabIndex="18">
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="left" style="width: 150px">
                                                    <asp:Label ID="lblStackNo" runat="server" Font-Size="8pt" Font-Bold="true" ForeColor="navy"
                                                        Text="Stack No."></asp:Label></td>
                                                <td align="left" style="width: 150px">
                                                    <asp:DropDownList ID="ddlStackNo" runat="server" Height="25px" Width="155px" AutoPostBack="True"
                                                        TabIndex="19" OnPreRender="ddlStackNo_PreRender">
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="left" style="width: 150px">
                                                    <asp:Label ID="lblStackBags" runat="server" Font-Size="8pt" Font-Bold="True" ForeColor="Navy"
                                                        Text="No.of Bags/Bells"></asp:Label></td>
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
                                                    <asp:Label ID="Label1" runat="server" Font-Size="8pt" Font-Bold="true" ForeColor="navy"
                                                        Text="Crop Year"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:DropDownList ID="ddlcropyear" runat="server" Height="25px" TabIndex="10" Width="155px">
                                                    </asp:DropDownList>
                                                </td>
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
                                                        CssClass="BTNBLUE" OnClick="btnAddStack_Click" /></td>
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
                        <td style="height: 10px" colspan="6">
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
                                                    <asp:Button ID="btnUpdate" runat="server" Text="Update Details" Width="120px" CssClass="BTNBLUE"
                                                        OnClick="btnupdate_Click" Enabled="False" OnClientClick="this.disabled = true; this.value='Updating...'" UseSubmitBehavior="false" />
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
                                                    <span style="font-size: 8pt; color: blue; text-decoration: underline;"><a href="#"
                                                        onclick="OpenWindow()"><u>Issue Gate Passa></span>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </center>
                            </fieldset>
                        </td>
                    </tr>
                </table>
            </div>
        </center>
    </fieldset>
   
  <%--  </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>

