<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WLC_Deposit_From_New.aspx.cs" Inherits="IssueCenterLevel_Storage_WLC_Deposit_From_New" MasterPageFile="~/MasterPage/Gdwn.master" Title="Deposit at Issue Centre" %>

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
</style>
<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
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

   <fieldset style="width: 960px; border: 1px solid navy; box-shadow: 1px 2px 8px; border-radius: 10px 10px 10px 10px;
        padding-left: 0px; margin-left: 15px">
        <center>
            <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>--%>
                      <div >
                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td colspan="4" align="center" valign="top">
                                    <fieldset style="width: 930px; border: 1px solid navy;">
                                        <center>
                                            <div>
                                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                    <tr style="background-color: #0bb6e6; height: 25px">
                                                        <td colspan="4" align="center">
                                                            <asp:Label ID="lblDepositDetail" runat="server" Text="Deposit at Branch " Font-Size="15px"
                                                                Font-Bold="true" ForeColor="whitesmoke"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr align="center">
                                                        <td colspan="4" align="center">
                                                           <table>
                                                           <tr>
                                                           <td>
                                                           <span style="color: #FF0000">
                                                           Important instructions
                                                           </span>

                                                           </td>
                                                           
                                                           </tr>
                                                           <tr>
                                                           <td style="font-size: small; font-weight: bold; font-style: normal; color: #FF0000; text-decoration: blink">
                                                           1. 
                                                               Other depot के case मे रिसीविंग लेने के लिए Date wise&nbsp; ऑप्शन का use किया जा सकता है जिसमे एक डेट की सारी रिसीविंग एक साथ दिख जाएगी</td>
                                                           </tr>
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
                                                            <asp:DropDownList ID="ddldepositortype" runat="server" AutoPostBack="True" Width="200px"
                                                                Height="30px" CssClass="tb6" OnSelectedIndexChanged="ddldepositortype_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td align="center" style="width: 200px">
                                                            <asp:Label ID="lblDepositorName" runat="server" Font-Size="12px" Font-Bold="true"
                                                                Text="Depositor Name" ForeColor="navy"></asp:Label>
                                                        </td>
                                                        <td align="left" style="width: 200px">
                                                            <asp:DropDownList ID="ddlDepositor" runat="server" AutoPostBack="True" Width="200px"
                                                                OnSelectedIndexChanged="ddlDepositor_SelectedIndexChanged" Height="30px" CssClass="tb6">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4" style="height: 5px">
                                                        </td>
                                                    </tr>
                                                    <tr align="center">
                                                        <td colspan="4" style="height: 5px">
                                                            <asp:RadioButton ID="RadioButton1" runat="server" AutoPostBack="True" 
                                                                Checked="True" oncheckedchanged="RadioButton1_CheckedChanged" 
                                                                Text="Crop Yearly" GroupName="a" />
                                                            <asp:RadioButton ID="RadioButton2" runat="server" AutoPostBack="True" 
                                                                oncheckedchanged="RadioButton2_CheckedChanged" Text="Between Date" 
                                                                GroupName="a" Visible="false" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4" style="height: 5px">
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4" style="height: 5px" align="center" valign="middle">
                                                            <asp:Panel ID="pnlcropyr" runat="server">
                                                            
                                                             <asp:Label ID="lblProcComm" runat="server" Font-Bold="True" 
                                                                Font-Size="12px" ForeColor="Navy" Text="Procurement Commodity"></asp:Label>
                                                            <asp:DropDownList ID="ddlProcCmd" runat="server" AutoPostBack="True" 
                                                                    onselectedindexchanged="ddlProcCmd_SelectedIndexChanged"> 
                                                                <asp:ListItem Value="22" Selected="True">Wheat-PSS</asp:ListItem>
                                                                <asp:ListItem Value="63">GRAM</asp:ListItem>
                                                                <asp:ListItem Value="64">LENTIL</asp:ListItem>
                                                                <asp:ListItem Value="33">Mustard-Sarason</asp:ListItem>
                                                                <asp:ListItem Value="13">Paddy-Common</asp:ListItem>
                                                                <asp:ListItem Value="14">Paddy-Grade-A</asp:ListItem>
                                                                <asp:ListItem Value="8">Bajra</asp:ListItem>
                                                                <asp:ListItem Value="40">Jau</asp:ListItem>
                                                                <asp:ListItem Value="11">Jowar</asp:ListItem>
                                                                <asp:ListItem Value="12">Maize(Makka)</asp:ListItem>
                                                                <asp:ListItem Value="106">Onion</asp:ListItem>
                                                                <asp:ListItem Value="92">Moong</asp:ListItem>
                                                                <asp:ListItem Value="52">Arahar</asp:ListItem>
                                                                <asp:ListItem Value="27">Urad</asp:ListItem>
                                                            </asp:DropDownList>
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                            <asp:Label ID="lblSourceOfDeposit0" runat="server" Font-Bold="True" 
                                                                Font-Size="12px" ForeColor="Navy" Text="Crop Year"></asp:Label>
                                                            <asp:DropDownList ID="ddlcropyear" runat="server" AutoPostBack="True" 
                                                                onselectedindexchanged="ddlcropyear_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            </asp:Panel>
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                            <asp:Panel ID="pnldate" runat="server" Visible="false">
                                                           
                                                            <asp:Label ID="lblSourceOfDeposit1" runat="server" Font-Bold="True" 
                                                                Font-Size="12px" ForeColor="Navy" Text="Date From "></asp:Label>
                                                            <asp:TextBox ID="txtdatefrom" runat="server"></asp:TextBox>
                                                                <asp:CalendarExtender ID="txtdatefrom_CalendarExtender" runat="server" 
                                                                    Enabled="True" TargetControlID="txtdatefrom">
                                                                </asp:CalendarExtender>
                                                             &nbsp;&nbsp;&nbsp;
                                                             <asp:Label ID="Label1" runat="server" Font-Bold="True" 
                                                                Font-Size="12px" ForeColor="Navy" Text="Date To "></asp:Label>
                                                                <asp:TextBox ID="txtdateto" runat="server"></asp:TextBox>
                                                                 <asp:CalendarExtender ID="txtdateto_CalendarExtender" runat="server" 
                                                                    Enabled="True" TargetControlID="txtdateto">
                                                                </asp:CalendarExtender>
                                                                 &nbsp;&nbsp;&nbsp;
                                                            <asp:Button ID="btnsearch" runat="server" Text="Search" onclick="btnsearch_Click" />
                                                             </asp:Panel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4" style="height: 5px">
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr align="center">
                                                        <td align="left">
                                                            <asp:Label ID="lblSourceOfDeposit" runat="server" Text="Deposit at Issue Centre From"
                                                                Font-Size="12px" Font-Bold="true" ForeColor="navy"></asp:Label>
                                                        </td>
                                                        <td align="left" colspan="3">
                                                            <asp:DropDownList ID="ddlArrival_Source" runat="server" Height="30px" Width="450px"
                                                                OnSelectedIndexChanged="ddlArrival_Source_SelectedIndexChanged" AutoPostBack="True"
                                                                CssClass="tb6">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4" style="height: 5px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4" style="height: 5px">
                                                            <asp:RadioButton ID="rbchallan" runat="server" AutoPostBack="True" 
                                                                Checked="True" GroupName="rb" Text="Challan Wise" 
                                                                oncheckedchanged="rbchallan_CheckedChanged" />
                                                            <asp:RadioButton ID="rbdate" runat="server" AutoPostBack="True" GroupName="rb" 
                                                                Text="Date Wise" oncheckedchanged="rbdate_CheckedChanged" />
                                                        </td>
                                                    </tr>
                                                    <tr runat="server" id="trdatewise" visible="false">
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
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4" style="height: 5px">
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr runat="server" id="Show_soc" visible="false">
                                                        <td colspan="4" style="height: 5px">
                                                            <table cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td align="left" style="width: 245px">
                                                                        <asp:Label runat="server" ID="lbl_ss" Text="Select Society - " Font-Bold="true" Font-Size="8pt"
                                                                            ForeColor="Navy"></asp:Label>
                                                                    </td>
                                                                    <td align="left" colspan="3">
                                                                        <asp:DropDownList ID="ddl_society" runat="server" Width="450px" AutoPostBack="True"
                                                                            Height="30px" CssClass="tb6" OnSelectedIndexChanged="ddl_society_SelectedIndexChanged">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4" style="height: 5px">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
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
                            <tr id="trnewproc" runat="server" visible="true">
                                <td colspan="4" align="center" valign="top">
                                    <fieldset style="width: 930px; border: 1px solid navy;">
                                        <center>
                                            <div>
                                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                    <tr style="background-color: #0bb6e6; height: 25px">
                                                        <td align="center" valign="middle">
                                                            <span style="color: White; font-size: 12pt; font-weight: bold">Dispatch from Procurement
                                                                Details-<asp:Label ID="lblcropyr" runat="server" Text="All"></asp:Label></span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 5px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" valign="top">
                                                            <asp:GridView ID="gdnewproc" runat="server" AutoGenerateColumns="False"
                                                                DataKeyNames="IssueCenter_ID,WHR_Request" AllowPaging="True" Width="100%"
                                                                Font-Size="10pt" BorderColor="Navy" BorderWidth="1px" OnPageIndexChanging="gdnewproc_PageIndexChanging"
                                                                PageSize="20" TabIndex="4" CellPadding="4" CellSpacing="2">
                                                                <Columns>
                                                                    
                                                                  
                                                                    <asp:BoundField DataField="whr_request" HeaderText="Depositor Form No." SortExpression="whr_request">
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Acceptance_Date" HeaderText="Acceptance Date" SortExpression="Acceptance_Date">
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Recd_Bags" HeaderText="Bags" SortExpression="Recd_Bags">
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Recd_Qty" HeaderText="Qty" SortExpression="Recd_Qty">
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity" SortExpression="Commodity_Name">
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Godown" HeaderText="Godown" />
                                                                    <asp:TemplateField HeaderText="Deposit">
                                                                        <EditItemTemplate>
                                                                            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                                                        </EditItemTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkDepositProcNew" Text="Click to Deposit" runat="server" CommandName="EditProc"
                                                                                CommandArgument='<%#Eval("WHR_Request") +","+Eval("Acceptance_Date") %>' OnClick="lnkDepositProcNew_Click"
                                                                                ForeColor="Blue"></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <FooterStyle BackColor="#CCCC99" />
                                                                <PagerStyle BackColor="#719cb6" ForeColor="Black" HorizontalAlign="center" />
                                                                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                                <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                                                                    Height="20px" Font-Size="10pt" />
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

                            <tr id="tr_Disfromprc" runat="server" visible="false">
                                <td colspan="4" align="center" valign="top">
                                    <fieldset style="width: 930px; border: 1px solid navy;">
                                        <center>
                                            <div>
                                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                    <tr style="background-color: #0bb6e6; height: 25px">
                                                        <td align="center" valign="middle">
                                                            <span style="color: White; font-size: 12pt; font-weight: bold">Dispatch from Procurement
                                                                Details</span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 5px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" valign="top">
                                                            <asp:GridView ID="GvuDispatchFromPC" runat="server" AutoGenerateColumns="False" GridLines="Both"
                                                                DataKeyNames="Distt_ID,IssueCenter_ID,TC_Number" AllowPaging="True" Width="100%"
                                                                Font-Size="10pt" BorderColor="navy" BorderWidth="1px" OnPageIndexChanging="GvuDispatchFromPC_PageIndexChanging"
                                                                PageSize="20" TabIndex="4" CellPadding="4" CellSpacing="2">
                                                                <Columns>
                                                                    <asp:BoundField DataField="TC_Number" HeaderText="TC No." SortExpression="TC_Number">
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Truck_Number" HeaderText="Truck No." SortExpression="Truck_Number">
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Acceptance_No" HeaderText="Acceptance No." SortExpression="Acceptance_No">
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Acceptance_Date" HeaderText="Acceptance Date" SortExpression="Acceptance_Date">
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Recd_Bags" HeaderText="Bags" SortExpression="Recd_Bags">
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Recd_Qty" HeaderText="Qty" SortExpression="Recd_Qty">
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity" SortExpression="Commodity_Name">
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:TemplateField HeaderText="Deposit Truck Challan">
                                                                        <EditItemTemplate>
                                                                            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                                                        </EditItemTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkDepositProc" Text="Click to Deposit" runat="server" CommandName="EditProc"
                                                                                CommandArgument='<%#Eval("Distt_ID") + "," + Eval("IssueCenter_ID") + "," + Eval("TC_Number") +","
                                                                    +Eval("Acceptance_No") +"," +Eval("IssueID")+","+Eval("Truck_Number") %>' OnClick="lnkDepositProc_Click"
                                                                                ForeColor="Blue"></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <FooterStyle BackColor="#CCCC99" />
                                                                <PagerStyle BackColor="#719cb6" ForeColor="Black" HorizontalAlign="center" />
                                                                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                                <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                                                                    Height="20px" Font-Size="10pt" />
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
                            <tr id="trfromothdepot" runat="server" visible="false">
                                <td colspan="4" align="center" valign="top">
                                    <fieldset style="width: 930px; border: 1px solid navy;">
                                        <center>
                                            <div>
                                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                    <tr style="background-color: #0bb6e6; height: 25px">
                                                        <td align="center" valign="middle">
                                                            <span style="color: White; font-size: 12pt; font-weight: bold">Dispatch&nbsp; from FCI
                                                                to Other Depot Details</span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4" style="height: 5px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" valign="top">
                                                            <asp:GridView ID="GvuFromFCI_OTHDepot" runat="server" AutoGenerateColumns="False"
                                                                GridLines="Both" DataKeyNames="Receipt_id" AllowPaging="True" Width="100%" BorderColor="navy"
                                                                BorderWidth="1px" AllowSorting="True" PageSize="20" SelectedIndex="0" OnPageIndexChanging="GvuFromFCI_OTHDepot_PageIndexChanging"
                                                                TabIndex="4" OnRowCommand="GvuFromFCI_OTHDepot_RowCommand" Font-Size="10pt" CellPadding="4"
                                                                CellSpacing="2">
                                                                <Columns>
                                                                    <asp:BoundField DataField="challan_no" HeaderText="TC No." SortExpression="challan_no">
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Vehile_no" HeaderText="Truck No." SortExpression="Vehile_no">
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="RO_No" HeaderText="FCI RO No." SortExpression="RO_No">
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="RO_date" HeaderText="FCI RO Date" SortExpression="RO_date">
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Recieved_Bags" HeaderText="Bags" SortExpression="Recieved_Bags">
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Recd_Qty" HeaderText="Qty" SortExpression="Recd_Qty">
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity" SortExpression="Commodity_Name">
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:TemplateField HeaderText="Deposit Truck Challan">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="LinkButton1" Text="Click to Deposit" runat="server" CommandName="EditFCI_OTHDepot"
                                                                                ForeColor="Blue"></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <FooterStyle BackColor="#CCCC99" />
                                                                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                                                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                                <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                                                                    Height="20px" Font-Size="10pt" />
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
                            <tr id="trfromrailhead" runat="server" visible="false">
                                <td colspan="4" align="center" valign="top">
                                    <fieldset style="width: 930px; border: 1px solid navy;">
                                        <center>
                                            <div>
                                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                    <tr style="background-color: #0bb6e6; height: 25px">
                                                        <td align="center" valign="middle">
                                                            <span style="color: White; font-size: 12pt; font-weight: bold">Dispatch from RailHead
                                                                Details</span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 5px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" valign="top">
                                                            <asp:GridView ID="gvuFrom_RailHead" runat="server" AutoGenerateColumns="False" GridLines="Both"
                                                                DataKeyNames="Dist_Id,Depot_ID,challan_no" AllowPaging="True" Width="100%" BackColor="White"
                                                                BorderColor="navy" BorderWidth="1px" AllowSorting="True" Font-Size="10pt" PageSize="20"
                                                                OnPageIndexChanging="gvuFrom_RailHead_PageIndexChanging" TabIndex="4" CellPadding="4"
                                                                CellSpacing="2">
                                                                <Columns>
                                                                  <asp:BoundField DataField="Rack_No" HeaderText="Rail Rack No.">
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="challan_no" HeaderText="TC No." SortExpression="challan_no">
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Vehile_no" HeaderText="Truck No." SortExpression="Vehile_no">
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="No_of_Bags" HeaderText="Bags" SortExpression="No_of_Bags">
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Recd_Qty" HeaderText="Qty" SortExpression="Recd_Qty">
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity" SortExpression="Commodity_Name">
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:TemplateField HeaderText="Deposit Truck Challan">
                                                                        <EditItemTemplate>
                                                                            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                                                        </EditItemTemplate>
                                                                        <ItemTemplate>
                                                                            &nbsp;<asp:LinkButton ID="lnkRailHead" Text="Click to Deposit" runat="server" CommandName="EditRailHead"
                                                                                ForeColor="Blue" CommandArgument='<%#Eval("Dist_Id") + "," + Eval("Depot_ID") + "," + Eval("challan_no") %>'
                                                                                OnClick="lnkRailHead_Click"></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <FooterStyle BackColor="#CCCC99" />
                                                                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                                                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                                <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                                                                    Height="20px" Font-Size="10pt" />
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
                        </table>
                    </div>
              <%--  </ContentTemplate>
            </asp:UpdatePanel>--%>
        </center>
    </fieldset>
</asp:Content>
