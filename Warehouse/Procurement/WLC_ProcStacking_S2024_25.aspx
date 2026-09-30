<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="WLC_ProcStacking_S2024_25.aspx.cs" Inherits="Procurement_WLC_ProcStacking_S2024_25" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1/jquery.min.js"></script>
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
    
    .style1
    {
        width: 200px;
        height: 42px;
    }
    
    </style>


    <script language="javascript" type="text/javascript">

        function compare() {
            a = document.getElementById('txtStackAvailable');
            b = document.getElementById('txtStackWt');
            //Ram
            if (b.value < 0) {
                alert("Negative value not allowed");
                b.value = "";
                b.focus;
            }
            else {
                if (Number(a.value) < Number(b.value)) {
                    alert("Stack insufficient to stack!!Pl check the value");

                }
            }
        }
    </script>
     <script language="javascript" type="text/javascript">

         function Partial_Rejection() {

             var a = document.getElementById('<%= txtRejBags.ClientID %>').value;
             var b = document.getElementById('<%= txtRejQty.ClientID %>').value;
             var c = document.getElementById('<%= txtProcBagsAcceptable.ClientID %>').value;
             var d = document.getElementById('<%= txtProcQtyAcceptable.ClientID %>').value;

//             alert(ss);
             //Ram
             NewBags = c - a;
            NewQty = d - b;
//            alert(NewBags);
             document.getElementById('<%= txtProcBagsAcceptable.ClientID %>').value = NewBags;
            //document.getElementById('txtProcBagsAcceptable').value = NewBags;
            document.getElementById("<%= txtProcQtyAcceptable.ClientID %>").value = NewQty;
             }
     
     </script>
    
  <%--  <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
    <ProgressTemplate>
    <div class="divWaiting">            
	<asp:Label ID="lblWait" runat="server" 
	Text=" Please wait... " />
	<asp:Image ID="imgWait" runat="server" 
	ImageAlign="Middle" ImageUrl="~/images/mpwlc3.gif" />
  </div>
    </ProgressTemplate>
    </asp:UpdateProgress>--%>
    <fieldset style="width: 1000px; border: 2px solid navy;">
        <center>
            
                    <div class="blockMe">
                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr style="background-color: #0bb6e6; height: 25px">
                                <td colspan="6" align="center">
                                    <asp:Label ID="lblDepositDetail" runat="server" Text="Truck wise Received Stock Detail"
                                        Font-Size="12pt" ForeColor="whitesmoke" Font-Bold="true"></asp:Label>
                                </td>
                            </tr>
                            <tr visible="false" runat="server">
                                <td colspan="6" style="height: 5px">
                                </td>
                            </tr>
                            <tr visible="false"  runat="server">
                                <td colspan="6" align="center">
                                    <asp:Label ID="lblmsg" runat="server" Font-Size="10pt" ForeColor="Red" EnableViewState="False"
                                        Font-Bold="true"></asp:Label>
                                </td>
                            </tr>
                        
                            <tr>
                                <td colspan="6">
                                    <fieldset style="width: 980px; border: 1px solid navy;">
                                        <center>
                                            <div>
                                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                <tr>
                                                       <asp:Label ID="Label4" Text="" runat="server" Font-Size="9pt" ForeColor="Red" EnableViewState="False"
                                        Font-Bold="true"></asp:Label>
                                                    </tr>
                                                       <tr visible="false"  runat="server">
                                                        <td colspan="4" style="height: 5px">
                                                        </td>
                                                    </tr>
                                                <tr visible="false"  runat="server">
                                                        <td align="right" colspan="2">
                                                            <asp:Label ID="Label3" runat="server" Font-Size="8pt" ForeColor="navy"
                                                                Font-Bold="true" Text="Quality Check by Warehouse Manager"></asp:Label>
                                                        </td>
                                                        <td align="left" colspan="2">
                                                            <asp:DropDownList ID="ddlWLCQC" runat="server" Width="200px" Height="25px" Enabled="false" AutoPostBack="true"
                                                                TabIndex="5" CssClass="tb6" 
                                                                onselectedindexchanged="ddlWLCQC_SelectedIndexChanged">
                                                                <asp:ListItem Value="-1">--Select--</asp:ListItem>
                                                                <asp:ListItem Selected="True" Value="Accepted">Accepted</asp:ListItem>
                                                                <asp:ListItem Value="Fully Rejected">Fully Rejected</asp:ListItem>
                                                                <asp:ListItem Value="Partially Rejected">Partially Rejected</asp:ListItem>
                                                                 
                                                            </asp:DropDownList>
                                                        </td>
                                                       <%-- <td align="left">
                                                            <asp:Label ID="Label4" runat="server" Font-Size="8pt" ForeColor="navy" Font-Bold="true"
                                                                Text="WCM No."></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="TextBox1" runat="server" MaxLength="20" Width="150px" TabIndex="6" Enabled="false" BackColor="#FFFFC0"
                                                                CssClass="tb6" Height="20px"></asp:TextBox>
                                                        </td>--%>
                                                        <%-- <td colspan="2" style="height: 5px">
                                                           <asp:Label ID="Label4" Text="नोट : यदि स्कंध NON-FAQ क्वालिटी का पाया जाता है ऐसी स्थति मे शाखा प्रबंधक 'Rejected' का चयन करे, अन्यथा 'Accepted' रहने देवे।" runat="server" Font-Size="9pt" ForeColor="Red" EnableViewState="False"
                                        Font-Bold="true"></asp:Label>
                                                         
                                                         </td>--%>
                                                    </tr>
                                                       <tr visible="false"  runat="server">
                                                        <td colspan="4" style="height: 5px">
                                                        </td>
                                                    </tr>
                                                    <tr visible="false">
                                                    <td colspan="6" align="center" valign="top">
                                    <fieldset id="FSWLCQC" style="width: 600px; border: 1px solid navy;" visible="false" runat="server">
                                        <center>
                                            <div>
                                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                 <tr>
                                                        <td align="left">
                                                            <asp:Label ID="Label6" runat="server" Font-Size="8pt" ForeColor="navy" Font-Bold="true"
                                                                Text="No. of Bags Rejected"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtRejBags" runat="server" BackColor="#FFFFC0" MaxLength="20" onkeyup="NumericDecimalCheck(this,2)"
                                                                Width="150px" CssClass="tb6" Height="20px"></asp:TextBox>
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="Label7" runat="server" Font-Size="8pt" ForeColor="navy"
                                                                Font-Bold="true" Text="Qty. Rejected"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtRejQty" runat="server" MaxLength="20" Width="150px" BackColor="#FFFFC0"
                                                                CssClass="tb6" Height="20px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                      <tr>
                                                        <td colspan="4" style="height: 5px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" colspan="4">
                                                            <asp:Label ID="Label8" runat="server" Font-Size="8pt" ForeColor="red"
                                                                Font-Bold="true" Text="(Click the Button to remove Partially rejected Qty. from Depositor form Qty.) "></asp:Label>
                                                            <br />
                                                            <asp:Button ID="btnPRejection" runat="server" TabIndex="22" AutoPostBack="true"
                                                                Text="Partially Reject" Width="100px"
                                                                CssClass="BTNBLUE" onclick="btnPRejection_Click"/>
                                                        </td>
                                                    </tr>
                                                       <tr>
                                                        <td colspan="4" style="height: 5px">
                                                        </td>
                                                    </tr>
                                                </table>
                                                </div>
                                                </center>
                                                </fieldset>
                                     </td>
                                                    </tr>
                                                   
                                                   <tr visible="false"  runat="server">
                                                    
                                                        <td align="left" class="style1">
                                                            <asp:Label ID="Label5" runat="server" Font-Size="8pt" ForeColor="Navy"
                                                                Font-Bold="True" Text="Depositor" 
                                                                ></asp:Label>
                                                        </td>
                                                       
                                                         <td align="left" colspan="3" class="style1">
                                                            <asp:DropDownList ID="ddlDepositorName" runat="server" Height="25px" Width="400px" Enabled="false"
                                                                Visible="true">
                                                            </asp:DropDownList>
                                                            <asp:CheckBox ID="chkDepositor" runat="server" Text="Change Depositor" AutoPostBack="true" 
                                                                 oncheckedchanged="chkDepositor_CheckedChanged"/>
                                                        </td>
                                                        <td align="left" colspan="2" class="style1">
                                                            <asp:Label ID="lblBook_No" runat="server" Font-Size="8pt" ForeColor="Navy" Visible="false"
                                                                Font-Bold="True" Text="" 
                                                                ></asp:Label>
                                                        </td>
                                                    </tr>
                                                     <tr visible="false"  runat="server">
                                                        <td colspan="4" style="height: 5px">
                                                        </td>
                                                    </tr>
                                                    <tr visible="false"  runat="server">
                                                        <td align="left" style="width: 200px">
                                                            <asp:Label ID="lblAcceptanceNote" runat="server" Font-Size="8pt" ForeColor="Navy"
                                                                Font-Bold="True" Text="Depositor Form No." 
                                                                meta:resourcekey="lblAcceptanceNoteResource1"></asp:Label>
                                                        </td>
                                                        <td align="left" style="width: 200px">
                                                            <asp:TextBox ID="txtAcceptanceNote" runat="server" MaxLength="20" Width="150px" Height="20px"
                                                                BackColor="#FFFFC0" CssClass="tb6" 
                                                                meta:resourcekey="txtAcceptanceNoteResource1"></asp:TextBox>
                                                        </td>
                                                         <td align="left" style="width: 200px">
                                                            <asp:Label ID="Label2" runat="server" Font-Size="8pt" ForeColor="Navy"
                                                                Font-Bold="True" Text="Acceptance No." meta:resourcekey="Label2Resource1"></asp:Label>
                                                        </td>
                                                        <td align="left" style="width: 200px">
                                                            <asp:TextBox ID="txtADno" runat="server" MaxLength="20" Width="150px" Height="20px" Enabled="false"
                                                                BackColor="#FFFFC0" CssClass="tb6" meta:resourcekey="txtADnoResource1"></asp:TextBox>
                                                        </td>
                                                        <td align="left" style="width: 200px">
                                                            <asp:Label ID="lblSourcesociety" runat="server" Font-Size="8pt" ForeColor="Navy"
                                                                Font-Bold="True" Text="Source Society" 
                                                                meta:resourcekey="lblSourcesocietyResource1"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="lbl_societyname" runat="server" Font-Bold="True" ForeColor="Navy"
                                                                Font-Size="12pt" meta:resourcekey="lbl_societynameResource1"></asp:Label>
                                                            <asp:DropDownList ID="ddlSourceS" runat="server" Height="25px" Width="50px" Enabled="False"
                                                                Visible="False" meta:resourcekey="ddlSourceSResource1">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr visible="false"  runat="server">
                                                        <td colspan="4" style="height: 5px">
                                                        </td>
                                                    </tr>
                                                   <tr visible="false"  runat="server">
                                                        <td align="left">
                                                            <asp:Label ID="lblProcTCNo" runat="server" Font-Size="8pt" ForeColor="navy" Font-Bold="true"
                                                                Text="Truck Challan No."></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtProcTCNo" runat="server" MaxLength="20" Height="20px" CssClass="tb6"
                                                                Width="150px" BackColor="#FFFFC0"></asp:TextBox>
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="lblProcTruckNo" runat="server" Font-Size="8pt" ForeColor="navy" Font-Bold="true"
                                                                Text="Truck No."></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtProcTruckNo" runat="server" MaxLength="20" Height="20px" CssClass="tb6" Enabled="false"
                                                                Width="150px" BackColor="#FFFFC0"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4" style="height: 5px">
                                                        </td>
                                                    </tr>
                                                    <tr visible="false"  runat="server">
                                                        <td align="left">
                                                            <asp:Label ID="lblProcCommodity" runat="server" Font-Size="8pt" ForeColor="navy"
                                                                Font-Bold="true" Text="Commodity"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:DropDownList ID="ddlProcCommodity" runat="server" Width="155px" Height="30px"
                                                                CssClass="tb6" BackColor="#FFFFC0">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="lblProcDepostiDate" runat="server" Font-Size="8pt" ForeColor="navy"
                                                                Font-Bold="true" Text="Date Of Deposit"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                           <%-- <asp:TextBox ID="txtProcDepostiDate" runat="server" MaxLength="10" Width="150px" Enabled="false" BackColor="#FFFFC0"
                                                                Height="20px" CssClass="tb6" TabIndex="4"></asp:TextBox>
                                                            <asp:CalendarExtender ID="CalendarExtender" runat="server" Enabled="True" TargetControlID="txtProcDepostiDate"
                                                                Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy">
                                                            </asp:CalendarExtender>--%>
                                                        </td>
                                                    </tr>
                                                    <tr visible="false"  runat="server">
                                                        <td colspan="4" style="height: 5px">
                                                        </td>
                                                    </tr>
                                                    <tr visible="false"  runat="server">
                                                        <td align="left">
                                                            <asp:Label ID="lblProcBags" runat="server" Font-Size="8pt" ForeColor="navy" Font-Bold="true"
                                                                Text="No. of Bags Deposited"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtProcBags" runat="server" BackColor="#FFFFC0" MaxLength="20" onkeyup="NumericDecimalCheck(this,2)"
                                                                Width="150px" CssClass="tb6" Height="20px"></asp:TextBox>
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="lblProcQtyDeposit" runat="server" Font-Size="8pt" ForeColor="navy"
                                                                Font-Bold="true" Text="Qty. Deposited"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtProcQtyDeposit" runat="server" MaxLength="20" Width="150px" BackColor="#FFFFC0"
                                                                CssClass="tb6" Height="20px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4" style="height: 5px">
                                                        </td>
                                                    </tr>
                                                    <tr visible="false"  runat="server">
                                                        <td align="left">
                                                            <asp:Label ID="lblProcWeigmentMode" runat="server" Font-Size="8pt" ForeColor="navy"
                                                                Font-Bold="true" Text="Mode of weigment"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:DropDownList ID="ddlProcWeigmentMode" runat="server" Width="155px" Height="30px" Enabled="false"
                                                                TabIndex="5" CssClass="tb6">
                                                                <asp:ListItem Value="10%">10%</asp:ListItem>
                                                                <asp:ListItem Selected="True" Value="100%">100%</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="lblProcWCMNo" runat="server" Font-Size="8pt" ForeColor="navy" Font-Bold="true"
                                                                Text="WCM No."></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtProcWCMNo" runat="server" MaxLength="20" Width="150px" TabIndex="6" Enabled="false" BackColor="#FFFFC0"
                                                                CssClass="tb6" Height="20px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                              <tr>
                                                        <td colspan="4" style="height: 5px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <asp:Label ID="Label9" runat="server" Font-Size="8pt" ForeColor="navy"
                                                                Font-Bold="true" Text="Depositor Name"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                             <asp:Label ID="lblDepositor" runat="server" Font-Size="8pt" ForeColor="navy"
                                                                Font-Bold="true" Text=""></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="Label10" runat="server" Font-Size="8pt" ForeColor="navy"
                                                                Font-Bold="true" Text="Commodity"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="lblCommodity" runat="server" Font-Size="8pt" ForeColor="navy"
                                                                Font-Bold="true" Text=""></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4" style="height: 5px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4" style="height: 5px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                       <%-- <td align="left">
                                                            <asp:Label ID="lblProcMoisture" runat="server" Font-Size="8pt" ForeColor="navy" Font-Bold="true"
                                                                Text="Average Mositure Content %"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                          
                                                        </td>--%>
                                                        <td align="left">
                                                            <asp:Label ID="Label11" runat="server" Font-Size="8pt" ForeColor="navy"
                                                                Font-Bold="true" Text="WLC Depositor Form No."></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                             <asp:Label ID="lblWLC_DFN" runat="server" Font-Size="8pt" ForeColor="navy"
                                                                Font-Bold="true" Text=""></asp:Label>
                                                                  <asp:TextBox ID="txtProcMoisture" runat="server" MaxLength="13" Width="150px" TabIndex="3"
                                                                Height="20px" CssClass="tb6" Text="0" Visible="false"></asp:TextBox>
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="Label1" runat="server" Font-Size="8pt" Font-Bold="true" ForeColor="navy"
                                                                Text="Date of Deposit"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                         <asp:TextBox ID="txtProcDepostiDate" runat="server" MaxLength="10" Width="150px" Enabled="false" BackColor="#ffffcc" Font-Bold="true"
                                                                Height="20px" CssClass="tb6" TabIndex="4"></asp:TextBox>
                                                            <asp:CalendarExtender ID="CalendarExtender" runat="server" Enabled="True" TargetControlID="txtProcDepostiDate"
                                                                Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy">
                                                            </asp:CalendarExtender>
                                                            <asp:DropDownList ID="ddlcropyear" runat="server" Height="30px" CssClass="tb6" TabIndex="10" Enabled="true"
                                                                Width="155px" Visible="true">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                       <tr>
                                                        <td colspan="4" style="height: 5px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <asp:Label ID="lblProcBagsAcceptable" runat="server" Font-Size="8pt" ForeColor="navy"
                                                                Font-Bold="true" Text="No. of Bags Recieved"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtProcBagsAcceptable" runat="server" MaxLength="18" onkeyup="NumericDecimalCheck(this,2)" Enabled="false" BackColor="#FFFFC0"
                                                                Width="150px" TabIndex="7" CssClass="tb6" Height="20px" EnableViewState="true" Font-Bold="true"></asp:TextBox>
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="lblProcQtyAcceptable" runat="server" Font-Size="8pt" ForeColor="navy"
                                                                Font-Bold="true" Text="Qty. Recieved(In Qtl.)"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtProcQtyAcceptable" runat="server" MaxLength="18" Enabled="false" BackColor="#FFFFC0" Font-Bold="true"
                                                                Width="150px" TabIndex="8" CssClass="tb6" Height="20px" AutoPostBack="false"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    
                                                      <tr>
                                                        <td colspan="4" style="height: 5px">
                                                        </td>
                                                    </tr>
                                                    <%--<tr id="Tr1" runat="server" visible="true">
                                                        <td align="left" style="width: 150px">
                                                            <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Size="8pt" ForeColor="Navy"
                                                                Text="Category" meta:resourcekey="Label3Resource1"></asp:Label>
                                                        </td>
                                                        <td align="left" style="width: 150px">
                                                             <asp:DropDownList ID="ddlCategory_Non" runat="server" Height="25px" Width="155px"
                                                TabIndex="9" meta:resourcekey="ddlCategory_NonResource1">
                                            </asp:DropDownList>
                                                        </td>
                                                        <td align="left" style="width: 150px">
                                                            <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Size="8pt" ForeColor="Navy"
                                                                Text="Market Value(Per Qtls.)" meta:resourcekey="Label4Resource1"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="TextBox2" runat="server" BackColor="#FFFFC0" CssClass="tb6" 
                                                                Height="20px" MaxLength="20" Text="1600" Enabled="False"
                                                                onblur="compare()" onkeyup="NumericDecimalCheck(this,5)" TabIndex="21" 
                                                                Width="150px" meta:resourcekey="TextBox2Resource1"></asp:TextBox>
                                                        </td>
                                                    </tr>--%>
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
                            <tr>
                                <td colspan="6" align="center" valign="top">
                                    <fieldset style="width: 980px; border: 1px solid navy;">
                                        <center>
                                            <div>
                                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                    <tr style="background-color: #ff9966; height: 25px">
                                                        <td colspan="4" align="center">
                                                            <asp:Label ID="lblDeliveryOrderOfStock" runat="server" Text="Stack Assignment for Received Stock"
                                                                ForeColor="whitesmoke" Font-Bold="true" Font-Size="12pt"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 5px" colspan="4">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" style="width: 200px">
                                                            <asp:Label ID="lblGodownNo" runat="server" Font-Size="8pt" Font-Bold="true" ForeColor="navy"
                                                                Text="Godown No./Name"></asp:Label>
                                                        </td>
                                                        <td align="left" style="width: 200px">
                                                            <asp:DropDownList ID="ddlGodownNo" runat="server" AutoPostBack="True" Enabled="false"
                                                                Height="30px" Width="170px" CssClass="tb6" OnSelectedIndexChanged="ddlGodownNo_SelectedIndexChanged"
                                                                TabIndex="18">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td align="left" style="width: 200px">
                                                            <asp:Label ID="lblStackNo" runat="server" Font-Size="8pt" Font-Bold="true" ForeColor="navy"
                                                                Text="Stack No."></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:DropDownList ID="ddlStackNo" runat="server" Height="30px" Width="155px" AutoPostBack="True"
                                                                TabIndex="19" OnPreRender="ddlStackNo_PreRender" CssClass="tb6">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 5px" colspan="4">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" style="width: 150px">
                                                            <asp:Label ID="lblStackBags" runat="server" Font-Bold="true" Font-Size="8pt" ForeColor="navy"
                                                                Text="No.of Bags"></asp:Label>
                                                        </td>
                                                        <td align="left" style="width: 150px">
                                                            <asp:TextBox ID="txtStackBags" runat="server" CssClass="tb6" Height="20px" MaxLength="20"
                                                                onblur="Spc_validator(this)" TabIndex="20" Width="150px"></asp:TextBox>
                                                        </td>
                                                        <td align="left" style="width: 150px">
                                                            <asp:Label ID="lblStackWt" runat="server" Font-Bold="true" Font-Size="8pt" ForeColor="navy"
                                                                Text="Weight(In Qtl.)"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtStackWt" runat="server" CssClass="tb6" Height="20px" MaxLength="20"
                                                                onblur="compare()" onkeyup="NumericDecimalCheck(this,5)" TabIndex="21" Width="150px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 10px" colspan="4">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <asp:Label ID="lblStackCurrentCapacity" runat="server" Font-Size="8pt" Font-Bold="true"
                                                                ForeColor="navy" Text="Current Stock(In Qtl.)"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtStackCurrentCapacity" runat="server" MaxLength="20" Width="150px"
                                                                BackColor="#FFFFC0" Enabled="False" CssClass="tb6" Height="20px"></asp:TextBox>
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="lblStackAvailable" runat="server" Font-Size="8pt" Font-Bold="true"
                                                                ForeColor="navy" Text="Vacant Capacity(In Qtl.)"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtStackAvailable" runat="server" MaxLength="20" Width="150px" BackColor="#FFFFC0"
                                                                Enabled="False" CssClass="tb6" Height="20px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 5px" colspan="4">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <asp:Label ID="lblStackMaxCap" runat="server" Font-Size="8pt" Font-Bold="true" ForeColor="navy"
                                                                Text="Maximum Capacity(In Qtl.)"></asp:Label>
                                                        </td>
                                                        <td align="left" colspan="3">
                                                            <asp:TextBox ID="txtStackMaxCap" runat="server" MaxLength="20" Width="150px" BackColor="#FFFFC0"
                                                                Enabled="False" CssClass="tb6" Height="20px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 5px" colspan="4">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" colspan="4">
                                                            <asp:Label ID="lblStackingInform" runat="server" Font-Size="8pt" ForeColor="red"
                                                                Font-Bold="true" Text="(Click the Add Stack Button to save the Stacking Information) "></asp:Label>
                                                            <br />
                                                            <asp:Button ID="btnAddStack" runat="server" TabIndex="22" Text="Add Stack" Width="100px"
                                                                CssClass="BTNBLUE" OnClick="btnAddStack_Click" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 5px" colspan="4">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" colspan="4">
                                                            <asp:GridView ID="gdstackingdetails" runat="server" AutoGenerateDeleteButton="True"
                                                                CellPadding="4" ForeColor="#333333" GridLines="None" OnPreRender="gdstackingdetails_PreRender"
                                                                OnRowCreated="gdstackingdetails_RowCreated" OnRowDeleting="gdstackingdetails_RowDeleting">
                                                                <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                                                <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                                                                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                                                                <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                                                                <HeaderStyle BackColor="#0bb6e6" Font-Bold="True" ForeColor="White" />
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
                                <td style="height: 5px" colspan="6">
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
                                                                Text="Remarks (If Any)"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtRemarks" runat="server" Height="50px" MaxLength="250" TabIndex="23"
                                                                TextMode="MultiLine" Width="500px" CssClass="tb6"></asp:TextBox>
                                                                 <asp:Label ID="lblDepositorIds" runat="server" Font-Size="8pt" ForeColor="navy" Font-Bold="true"
                                                                Text="" Visible="false"></asp:Label>
                                                                <asp:Label ID="lblDeositorNames" runat="server" Font-Size="8pt" ForeColor="navy" Font-Bold="true"
                                                                Text="" Visible="false"></asp:Label>
                                                                  <asp:Label ID="lblCommodityId" runat="server" Font-Size="8pt" ForeColor="navy" Font-Bold="true"
                                                                Text="" Visible="false"></asp:Label>
                                                                <asp:Label ID="lblDF_Receipt_ID" runat="server" Font-Size="8pt" ForeColor="navy" Font-Bold="true"
                                                                Text="" Visible="false"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 10px" colspan="2">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" align="center">
                                                            <asp:Button ID="btnsave" runat="server" Text="Save Details" Width="120px" CssClass="BTNBLUE"
                                                                TabIndex="24" ValidationGroup="GD_Stack,Non" Enabled="False" OnClick="btnsave_Click" />
                                                            &nbsp; &nbsp; &nbsp;
                                                            <asp:Button ID="btnUpdate" runat="server" Text="Update Details" Width="120px" CssClass="BTNBLUE"
                                                                OnClick="btnupdate_Click" Enabled="False" />
                                                            &nbsp; &nbsp; &nbsp;
                                                            <asp:Button ID="btn_Close" runat="server" Text="Close" Width="120px" CssClass="BTNBLUE"
                                                                CausesValidation="false" OnClick="btn_Close_Click" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" style="height: 5px">
                                                            <asp:HiddenField ID="hfSending_Dist" runat="server" />
                                                            <asp:HiddenField ID="hfAcptDate" runat="server" />
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
   
</asp:Content>

