<%@ Page Language="C#" MasterPageFile="~/MasterPage/PrivateWarehouse.master" AutoEventWireup="true" CodeFile="GWLC_ProcurementReceipt.aspx.cs" Inherits="WarehouseLevel_WLC_Procurement_GWLC_ProcurementReceipt" Title="Procurement Receipt" %>

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
       .style1
       {
           height: 5px;
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

   <fieldset style="width: 1100px; border: 1px solid navy; box-shadow: 1px 2px 8px; border-radius: 10px 10px 10px 10px;
        padding-left: 0px; margin-left: 15px">
        <center>
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
                                                            <asp:Label ID="lblDepositDetail" runat="server" Text="Deposit at Branch " Font-Size="15px"
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
                                                            <asp:DropDownList ID="ddldepositortype" runat="server" AutoPostBack="True" Width="200px" Enabled="true"
                                                                Height="25px" CssClass="tb6" OnSelectedIndexChanged="ddldepositortype_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td align="center" style="width: 200px">
                                                            <asp:Label ID="lblDepositorName" runat="server" Font-Size="12px" Font-Bold="true"
                                                                Text="Depositor Name" ForeColor="navy"></asp:Label>
                                                        </td>
                                                        <td align="left" style="width: 200px">
                                                            <asp:DropDownList ID="ddlDepositor" runat="server" AutoPostBack="True" Width="200px"
                                                                OnSelectedIndexChanged="ddlDepositor_SelectedIndexChanged" Height="25px" CssClass="tb6">
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
                                                                  Height="25px" Width="200px" onselectedindexchanged="ddlProcCmd_SelectedIndexChanged"
                                                                   > 
                                                                <%--<asp:ListItem Value="22" Selected="True">Wheat-PSS</asp:ListItem>--%>
                                                                <asp:ListItem Value="63">GRAM</asp:ListItem>
                                                                <asp:ListItem Value="64">LENTIL</asp:ListItem>
                                                                <asp:ListItem Value="33">Mustard-Sarason</asp:ListItem>
                                                                 <asp:ListItem Value="52">Arahar</asp:ListItem>
                                                              <%--  
                                                                <asp:ListItem Value="40">Jau</asp:ListItem>
                                                                <asp:ListItem Value="11">Jowar</asp:ListItem>--%>
                                                                <asp:ListItem Value="22">Wheat-PSS</asp:ListItem>
                                                                <asp:ListItem Value="123">RAM TIL</asp:ListItem>
                                                              <asp:ListItem Value="31">Ground-Nut</asp:ListItem>
                                                                <asp:ListItem Value="92">Moong</asp:ListItem>
                                                                <asp:ListItem Value="65">Tilli</asp:ListItem>
                                                                <asp:ListItem Value="27">Urad</asp:ListItem>
                                                                <asp:ListItem Value="13" Selected="True">Paddy-Common</asp:ListItem>
                                                                <asp:ListItem Value="14">Paddy-Grade-A</asp:ListItem>
                                                                <asp:ListItem Value="8">Bajra</asp:ListItem>
                                                                <asp:ListItem Value="11">Jowar</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td align="center" style="width: 200px">
                                                            <asp:Label ID="Label2" runat="server" Font-Size="12px" Font-Bold="true"
                                                                Text="Crop Year" ForeColor="navy"></asp:Label>
                                                        </td>
                                                        <td align="left" style="width: 200px">
                                                             <asp:DropDownList ID="ddlcropyear" runat="server" AutoPostBack="True" Height="25px" Width="200px" Enabled="true"
                                                                >
                                                            </asp:DropDownList>
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
                            <tr id="trnewproc" runat="server" visible="true">
                                <td colspan="4" align="center" valign="top">
                                    <fieldset style="width: 930px; border: 1px solid navy;">
                                        <center>
                                            <div>
                                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                    <tr style="background-color: #0bb6e6; height: 25px">
                                                        <td align="center" valign="middle">
                                                            <span style="color: White; font-size: 12pt; font-weight: bold">Depositor Form detail
                                                                 <asp:Label ID="lblcropyr" runat="server" Text=""></asp:Label></span></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 5px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" valign="top">
                                                            <asp:GridView ID="gdnewproc" runat="server" AutoGenerateColumns="False"
                                                                DataKeyNames="Acceptance_No,DepositerNo" AllowPaging="True" Width="100%"
                                                                Font-Size="10pt" BorderColor="Navy" BorderWidth="1px" OnPageIndexChanging="gdnewproc_PageIndexChanging"
                                                                PageSize="20" TabIndex="4" CellPadding="4" CellSpacing="2">
                                                                <Columns>
                                                                    
                                                                  
                                                                    <asp:BoundField DataField="DepositerNo" HeaderText="Depositor Form No." SortExpression="DepositerNo">
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                       <asp:BoundField DataField="Acceptance_No" HeaderText="Acceptance_No" SortExpression="Acceptance_No">
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Acceptance_Date" HeaderText="Acceptance Date" SortExpression="Acceptance_Date">
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:BoundField>
                                                                      <asp:BoundField DataField="TC_Number" HeaderText="TC_Number" SortExpression="TC_Number">
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:BoundField>
                                                                      <asp:BoundField DataField="Truck_Number" HeaderText="Truck_Number" SortExpression="Truck_Number">
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
                                                                                CommandArgument='<%#Eval("DepositerNo") +","+Eval("Acceptance_Date") %>' OnClick="lnkDepositProcNew_Click"
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

                           
                           
                        </table>
                    </div>           
        </center>
    </fieldset>
</asp:Content>
