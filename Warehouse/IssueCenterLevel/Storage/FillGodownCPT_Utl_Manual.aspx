<%@ Page Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="FillGodownCPT_Utl_Manual.aspx.cs" Inherits="IssueCenterLevel_Storage_FillGodownCPT_Utl_Manual" Title="Untitled Page" %>

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
           width: 200px;
       }
       .style2
       {
           height: 30px;
           width: 200px;
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
  <script type="text/javascript">
      function getValues() {
          var numVal1;
          var numVal2;
          if (document.getElementById("txtSciCpt").value == 0) {
              numVal1 = 0;
          }
          else {
              numVal1 = parseInt(document.getElementById("txtSciCpt").value);
          }

          var totalValue = ((numVal1 * 25) / 100) + numVal1;

          document.getElementById("txtGdwnMaxCpt").value = totalValue;
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
                                                        <td colspan="5" align="center">
                                                            <asp:Label ID="lblDepositDetail" runat="server" Text="Fill Godown Deposit Details" Font-Size="15px"
                                                                Font-Bold="true" ForeColor="whitesmoke"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr align="center">
                                                        <td colspan="5" align="center">
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
                                                    <tr align="center">
                                                        <td align="left" style="width: 250px;height:30px">
                                                            <asp:Label ID="lblDepositorType" runat="server" Font-Size="12px" Font-Bold="true"
                                                                Text="Godown Name(WHMS) :" ForeColor="navy"></asp:Label>
                                                        </td>
                                                        <td class="style1">
                                                        <asp:DropDownList ID="ddlGdwn" runat="server"  Width="200px"
                                                                Height="25px" CssClass="tb6"  AutoPostBack="true"
                                                                onselectedindexchanged="ddlGdwn_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="width:10px"></td>
                                                        </tr>
                                                        <tr>
                                                        <td align="left" style="width: 250px;height:30px">
                                                               <asp:Label ID="Label1" runat="server" Font-Size="12px" Font-Bold="true"
                                                                Text="Godown ID(WHMS) :" ForeColor="navy"></asp:Label>
                                                        </td>
                                                        <td class="style1">
                                                       <asp:TextBox ID="txtGdwnID" runat="server" Width="196px"  Height="20px" align="left" 
                                                                ReadOnly="True"></asp:TextBox>
                                                        </td>
                                                        <td style="width:5px"></td>
                                                        <td align="left" style="width: 250px;height:30px">
                                                         <asp:Label ID="Label7" runat="server" Font-Size="12px" Font-Bold="true"
                                                                Text="Branch(Actual) :" ForeColor="navy"></asp:Label>
                                                        </td>
                                                        <td>
                                                        <asp:DropDownList ID="ddlBranchActual" runat="server"  Width="200px"
                                                                Height="25px" CssClass="tb6">
                                                            </asp:DropDownList>
                                                        </td>
                                                        
                                                        </tr>
                                                        <tr>
                                                      
                                                        <td align="left" style="width: 250px;height:30px">
                                                            <asp:Label ID="lblDepositorName" runat="server" Font-Size="12px" Font-Bold="true"
                                                                Text="Godown Name(If Changed)" ForeColor="navy"></asp:Label>
                                                        </td>
                                                        
                                                        <td align="left" class="style2">
                                                                <asp:TextBox ID="txtGdwnChangeName" runat="server" Width="196px" Height="20px" align="left"></asp:TextBox>
                                                        </td>
                                                        <td style="width:5px"></td><td align="left">
                                                        <asp:Label ID="Label2" runat="server" Font-Size="12px" Font-Bold="true"
                                                                Text="Godown No" ForeColor="navy"></asp:Label>
                                                        </td>
                                                        <td>
                                                        <asp:TextBox ID="txtGdwnNo" runat="server" Width="196px" Height="20px" align="left"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" style="height: 30px">
                                                         <asp:Label ID="Label3" runat="server" Font-Size="12px" Font-Bold="true"
                                                                Text="Godown Type" ForeColor="navy"></asp:Label>
                                                        </td>
                                                        <td class="style1">
                                                        <asp:DropDownList ID="ddlHiredType" runat="server"  Width="200px"
                                                                Height="25px" CssClass="tb6">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td></td>
                                                        <td align="left">
                                                        <asp:Label ID="Label4" runat="server" Font-Size="12px" Font-Bold="true"
                                                                Text="Storage Type" ForeColor="navy"></asp:Label>
                                                        </td>
                                                        <td>
                                                        <asp:DropDownList ID="ddlstoragetype" runat="server"  Width="200px"
                                                                Height="25px" CssClass="tb6" align="left">
                                                        <asp:ListItem Text="Covered" Value="Covered"></asp:ListItem>
                                                        <asp:ListItem Text="Permanent(CAP)" Value="Permanent(CAP)"></asp:ListItem>
                                                           <asp:ListItem Text="Temporary(CAP)" Value="Temporary(CAP)"></asp:ListItem>
                                                         <asp:ListItem Text="Silo Bag"  Value="SiloBag"></asp:ListItem>
                                                        <asp:ListItem Text="Steel Silo"  Value="SteelSilo"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                     <tr>
                                                        <td align="left" style="height: 30px">
                                                         <asp:Label ID="Label5" runat="server" Font-Size="12px" Font-Bold="true"
                                                                Text="Godown Scientific Capacity (In M.T)" ForeColor="navy"></asp:Label>
                                                        </td>
                                                        <td class="style1">
                                                        <asp:TextBox ID="txtSciCpt" runat="server" Width="195px" Height="20px" align="left" onkeyup="getValues()"></asp:TextBox>
                                                        </td>
                                                        <td></td>
                                                        <td align="left">
                                                        <asp:Label ID="Label6" runat="server" Font-Size="12px" Font-Bold="true"
                                                                Text="Godown Max Capacity (125% In M.T) " ForeColor="navy"></asp:Label>
                                                        </td>
                                                        <td>
                                                        <asp:TextBox ID="txtGdwnMaxCpt" runat="server" Width="196px" Height="20px" align="left"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                    
                                                    <td colspan="5" align="center">
                                                    <br />
                                       <asp:Button ID="btnSubmit" runat="server" class="submit" Text="Submit" 
                                                            Width="80px" Height="30px" onclick="btnSubmit_Click"/> 
                                                           &nbsp;&nbsp;&nbsp;&nbsp 
                                       <asp:Button ID="btnNew" runat="server" class="submit" Text="New" 
                                                            Width="80px" Height="30px" onclick="btnNew_Click" />                                                              
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
                                                    <tr style="background-color: #0bb6e6; height: 25px">
                                                        <td align="center" valign="middle">
                                                            <span style="color: White; font-size: 12pt; font-weight: bold">Stock/Deposit Godown
                                                                Details-<asp:Label ID="lblcropyr" runat="server"></asp:Label></span></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 5px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" valign="top">
                                                            <asp:GridView ID="gridgdwn" runat="server" AutoGenerateColumns="False" 
                                                                DataKeyNames="WHMS_GodownID" AllowPaging="True" Width="100%"
                                                                Font-Size="10pt" BorderColor="Navy" BorderWidth="1px" PageSize="20" 
                                                                TabIndex="4" CellPadding="4" CellSpacing="2" 
                                                                onselectedindexchanged="gridgdwn_SelectedIndexChanged">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="S.No.">
                                                                 <ItemTemplate>
                                                    <%#Container.DataItemIndex+1%>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" Width="20px" />
                                            </asp:TemplateField>
                                                                    <asp:BoundField DataField="WHMS_GodownName" HeaderText="Godown Name" SortExpression="WHMS_GodownName">
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="WHMS_GodownID" HeaderText="Godown Id" SortExpression="WHMS_GodownID">
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Hired_Type" HeaderText="Hired Type" SortExpression="Hired_Type">
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Storage_Type" HeaderText="Storage Type" SortExpression="Storage_Type">
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Godown_Scientific_Capacity" HeaderText="Scientific Capacity" SortExpression="Godown_Scientific_Capacity">
                                                                        <ItemStyle HorizontalAlign="Right" Width="100px"/>
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Godown_Capacity" HeaderText="Capacity(125%)" SortExpression="Godown_Capacity">
                                                                        <ItemStyle HorizontalAlign="Right" Width="50px" />
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

