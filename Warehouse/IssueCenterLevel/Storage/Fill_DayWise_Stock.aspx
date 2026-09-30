<%@ Page Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="Fill_DayWise_Stock.aspx.cs" Inherits="IssueCenterLevel_Storage_Fill_DayWise_Stock" Title="Untitled Page" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
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
       .style3
       {
           width: 200px;
           height: 30px;
       }
       .style4
       {
           height: 30px;
       }
       .style5
       {
           height: 35px;
       }
       .style6
       {
           height: 25px;
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
	   <script type="text/javascript">
	       function preventInput(evnt) {
	           //Checked In IE9,Chrome,FireFox
	           if (evnt.which != 9) evnt.preventDefault();
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

   <fieldset style="width: 960px; border: 1px solid navy; box-shadow: 1px 2px 8px; border-radius: 10px 10px 10px 10px;
        padding-left: 0px; margin-left: 15px">
        <center>
            <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>--%>
                      <div >
                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td align="center" valign="top">
                                    <fieldset style="width: 930px; border: 1px solid navy;">
                                        <center>
                                            <div>
                                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                    <tr style="background-color: #0bb6e6; height: 25px">
                                                        <td colspan="4" align="center">
                                                            <asp:Label ID="lblDepositDetail" runat="server" Text="Fill Daily Stock Deposit Details" Font-Size="15px"
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
                                                           </table>
                                                           </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4" style="height: 5px">
                                                        
                                                        </td>
                                                    </tr>
                                                    <tr align="Right">
                                                        <td align="left" style="width: 250px;height:30px">
                                                            <asp:Label ID="lblDepositorType" runat="server" Font-Size="12px" Font-Bold="true"
                                                                Text="Godown Name(WHMS) :" ForeColor="navy"></asp:Label>
                                                        </td>
                                                        <td align="left" height:35px>
                                                        <asp:DropDownList ID="ddlGodWHMS" runat="server"  Width="200px"
                                                                Height="25px" CssClass="tb6" >
                                                            </asp:DropDownList>
                                                        </td>
                                                                                                                <td align="left" style="height:35px">
                                                            <asp:Label ID="lblDepositorName" runat="server" Font-Size="12px" Font-Bold="true"
                                                                Text="Deposite Date :" ForeColor="navy"></asp:Label>
                                                        </td>
                                                        
                                                        <td align="left" class="style3">
                                                                <asp:TextBox ID="txtDate" runat="server" class="text" type="text" 
                            onkeydown="javascript:preventInput(event);" onpaste="return false;" Height="20px" Width="198px" 
                           ></asp:TextBox>
                     <cc1:CalendarExtender ID="CalendarExtender2" runat="server"  Format="dd/MM/yyyy"
                         TargetControlID="txtDate"></cc1:CalendarExtender>
                                                        &nbsp;&nbsp;</td>
                                                        </tr>
                                                        <tr id="trCom" runat="server" visible="false">
                                                        <td align="left" style="width: 250px;height:35px">
                                                               <asp:Label ID="Label1" runat="server" Font-Size="12px" Font-Bold="true"
                                                                Text="Commodity :" ForeColor="navy"></asp:Label>
                                                        </td>
                                                        <td >
                                                       <asp:DropDownList ID="ddlCommodity" runat="server"  Width="200px" AutoPostBack="true"
                                                                Height="25px" CssClass="tb6" 
                                                                onselectedindexchanged="ddlCommodity_SelectedIndexChanged">
                                                            </asp:DropDownList>&nbsp;&nbsp;
                                                        </td>
                                                        <td align="left" style="width: 250px;height:30px">
                                                         <asp:Label ID="Label7" runat="server" Font-Size="12px" Font-Bold="true"
                                                                Text="Depositor :" ForeColor="navy"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                        <asp:DropDownList ID="ddlDepositor" runat="server"  Width="200px" AutoPostBack="true"
                                                                Height="25px" CssClass="tb6">
                                                            </asp:DropDownList>
                                                        </td>
                                                        
                                                        </tr>
                                                        <tr>
                                                      
<%--                                                        <td align="left" style="height:35px">
                                                            <asp:Label ID="lblDepositorName" runat="server" Font-Size="12px" Font-Bold="true"
                                                                Text="Date :" ForeColor="navy"></asp:Label>
                                                        </td>
                                                        
                                                        <td align="left" class="style3">
                                                                <asp:TextBox ID="txtDate" runat="server" class="text" type="text" 
                            onkeydown="javascript:preventInput(event);" onpaste="return false;" Height="20px" Width="198px" 
                           ></asp:TextBox>
                     <cc1:CalendarExtender ID="CalendarExtender2" runat="server"  Format="dd/MM/yyyy"
                         TargetControlID="txtDate"></cc1:CalendarExtender>
                                                        &nbsp;&nbsp;</td>--%>
                                                    </tr>
                                                    <tr>
                                                        <%--<td align="left" class="style5">
                                                         <asp:Label ID="Label3" runat="server" Font-Size="12px" Font-Bold="true"
                                                                Text="Closing Date :" ForeColor="navy"></asp:Label>
                                                        </td>
                                                        <td class="style5" align="left">
                                                       <asp:TextBox ID="txtClosingDate" runat="server" class="text" type="text" 
                            onkeydown="javascript:preventInput(event);" onpaste="return false;" Height="21px" Width="196px" 
                           ></asp:TextBox>
                     <cc1:CalendarExtender ID="CalendarExtender1" runat="server"  Format="dd/MM/yyyy"
                         TargetControlID="txtClosingDate"></cc1:CalendarExtender>
                                                        </td>--%>
                                                        <td align="left" class="style5">
                                                        <asp:Label ID="Label4" runat="server" Font-Size="12px" Font-Bold="true"
                                                                Text="Opening Balance (In M.T) :" ForeColor="navy"></asp:Label>
                                                        </td>
                                                        <td class="style5">
                                                         <asp:TextBox ID="txtClosingCpt" runat="server" Width="196px" Height="20px" align="left"></asp:TextBox>
                                                        </td>
                                                        <td align="left" class="style4">
                                                        <asp:Label ID="Label2" runat="server" Font-Size="12px" Font-Bold="true"
                                                                Text="Deposite Quantity (In M.T) :" ForeColor="navy"></asp:Label>
                                                        </td>
                                                        <td class="style4">
                                                        <asp:TextBox ID="txtStorageCpt" runat="server" Width="196px" Height="20px" align="left"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                    <td align="left" class="style5">
                                                        <asp:Label ID="Label3" runat="server" Font-Size="12px" Font-Bold="true"
                                                                Text="Deliever Quantity (In M.T) :" ForeColor="navy"></asp:Label>
                                                        </td>
                                                        <td class="style5">
                                                         <asp:TextBox ID="txtDelQty" runat="server" Width="196px" Height="20px" align="left"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                     
                                                    <tr>
                                                    
                                                    <td colspan="4" align="center">
                                                    <br />
                                      <asp:Button ID="btnSubmit" runat="server" class="submit" Text="Submit" Width="80px" Height="25px" onclick="btnSubmit_Click"/>
                                                        &nbsp;&nbsp;&nbsp;
                                          <asp:Button ID="btnNew" runat="server" class="submit" Text="New" Width="80px" Height="25px" 
                                                            onclick="btnNew_Click"/>
                                          <br />
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
                            <tr id="trnewproc" runat="server" visible="true">
                                <td align="center" valign="top">
                                    <fieldset style="width: 930px; border: 1px solid navy;">
                                        <center>
                                            <div>
                                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                    <tr style="background-color: #0bb6e6; ">
                                                        <td align="center" valign="middle" class="style6">
                                                            <span style="color: White; font-size: 12pt; font-weight: bold">Stock/Deposit Godown
                                                                Details-<asp:Label ID="lblcropyr" runat="server"></asp:Label></span></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" >
                                                        <br />
                                                         <asp:Label ID="Label5" runat="server" Font-Size="12px" Font-Bold="true"
                                                                Text="Select Date :" ForeColor="navy"></asp:Label>
                                                        
                                                       <asp:TextBox ID="txtSelectDate" runat="server" class="text" type="text" 
                            onkeydown="javascript:preventInput(event);" onpaste="return false;" Height="21px" 
                           ></asp:TextBox>
                     <cc1:CalendarExtender ID="CalendarExtender3" runat="server"  Format="dd/MM/yyyy"
                         TargetControlID="txtSelectDate"></cc1:CalendarExtender>
                         <asp:Button ID="btnSearch" runat="server" Text="Search" onclick="btnSearch_Click" Width="80px" Height="25px" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" valign="top"> <br />
                                                            <asp:GridView ID="gdnewproc" runat="server" AutoGenerateColumns="False"
                                                                DataKeyNames="WHMS_GodownID" AllowPaging="True" Width="100%"
                                                                Font-Size="10pt" BorderColor="Navy" BorderWidth="1px" 
                                                                PageSize="20" TabIndex="4" CellPadding="4" CellSpacing="2" 
                                                                onselectedindexchanged="gdnewproc_SelectedIndexChanged">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="S.No.">
                                                                 <ItemTemplate>
                                                    <%#Container.DataItemIndex+1%>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" Width="20px" />
                                            </asp:TemplateField>
                                                                    <asp:BoundField DataField="Godown_Name" HeaderText="Godown Name" SortExpression="Godown_Name">
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="WHMS_GodownID" HeaderText="Godown Id" SortExpression="WHMS_GodownID">
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Deposite_Date" HeaderText="Date" SortExpression="Deposite_Date">
                                                                        <ItemStyle HorizontalAlign="Right" Width="100px"/>
                                                                    </asp:BoundField>                                                                    
                                                                      <asp:BoundField DataField="Deposite_Qty_PreDay" HeaderText="Opening Balance" SortExpression="Deposite_Qty_PreDay">
                                                                        <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                    </asp:BoundField>

                                                                    <asp:BoundField DataField="Deposite_Qty_Today" HeaderText="Deposite Quantity" SortExpression="Deposite_Qty_Today">
                                                                        <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Delievery_Qty_PerDay" HeaderText="Delievery Quantity" SortExpression="Delievery_Qty_PerDay">
                                                                        <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                    </asp:BoundField>
                                <asp:CommandField SelectText="Edit" HeaderText="Update" ShowSelectButton="True" >
                                <ControlStyle Font-Bold="True" ForeColor="Blue" />
                                </asp:CommandField>                                                                      
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

                            <tr id="trfromrailhead" runat="server" visible="false">
                                <td align="center" valign="top">
                                    &nbsp;</td>
                            </tr>
                        </table>
                    </div>
              <%--  </ContentTemplate>
            </asp:UpdatePanel>--%>
        </center>
    </fieldset>
</asp:Content>



