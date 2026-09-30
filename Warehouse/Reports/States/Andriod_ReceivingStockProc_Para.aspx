<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Andriod_ReceivingStockProc_Para.aspx.cs" Inherits="Reports_States_Andriod_ReceivingStockProc_Para" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%--<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>--%>
  
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Mobile/Tablet Receiving Stock Procurement Godownwise Details</title>
    <style>
     #divGridGodown 
        {
         height: auto;
         overflow: scroll;
         max-height: 500px;
         }

     .Freezing 
    { 
    position: relative;  
    top:expression(this.offsetParent.scrollTop);
    z-index: 10; 
    }
     #checkdiv { 
   min-height: 500px; 
   height:auto !important; 
   height: 500px; 
}
        .auto-style1 {
            width: 246px;
        }
        .auto-style2 {
            width: 105px;
        }
        .auto-style3 {
            height: 26px;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">
          <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
              </asp:ToolkitScriptManager>
        <div id="checkdiv">
         <div style="margin-bottom:10px;">
            <table style="width: 1020px">
                <tr>
                    <td colspan="3">
                    <asp:Label ID="Label2" runat="server" Text="Please Select Branch from Dropdown and click on Search Godown Button to View Related Godown" Font-Bold="True" Font-Size="Medium" Font-Underline="True"></asp:Label>
                    </td>
                 </tr>
                <tr>
                    <td class="auto-style3"></td>
                </tr>
                <tr>
                    <td class="auto-style2">
                       <asp:Label ID="lblBranch" runat="server" Text="Select Branch"></asp:Label>
                    </td>
                    <td class="auto-style1">
                      <asp:DropDownList ID="ddlBranch" runat="server" AutoPostBack="true" Height="24px" Width="220px" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged"></asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="btnGodownSearch" runat="server" Text="Search Godown" style="margin-left:100px;height:25px;" OnClick="btnGodownSearch_Click"/> </td>
                </tr>
               
            </table>
        </div>
          
         <div id="divGridGodown" runat="server">
             <table>
                 <tr>
                     <td>
                       <asp:Label ID="Label1" runat="server" Text="Please Select Godown from List Below to Generate Mobile/Tablet based Procurement Report" Font-Bold="True" Font-Overline="False" Font-Size="Medium" Font-Underline="True"></asp:Label>
                     </td>
                 </tr>
                 <tr>
                    <td>
                    <asp:GridView ID="gvGodown" runat="server"  AutoGenerateColumns="False" EnableModelValidation="True" CellPadding="4" ForeColor="#333333" 
                        GridLines="None" style="overflow: scroll;">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775"/>
                        <Columns>
                           <asp:TemplateField HeaderText="Select">
                               <ItemTemplate> 
                                   <asp:CheckBox ID="chbGodown" runat="server" OnCheckedChanged="chbGodown_CheckedChanged" AutoPostBack="true"></asp:CheckBox>
                               </ItemTemplate>              
                            </asp:TemplateField>
                            <asp:BoundField DataField="Godown_ID" HeaderText="Godown Id" >
                            <ItemStyle Font-Size="Small" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Godown_Name" HeaderText="Godown Name" >
                            <ItemStyle Font-Size="Small" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Hired_Type" HeaderText="Hired Type" >
                            <ItemStyle Font-Size="Small" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Storage_Type" HeaderText="Storage Type" >
                            <ItemStyle Font-Size="Small" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Godown_APN" HeaderText="Godown APN" >
                            <ItemStyle Font-Size="Small" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Godown_Email" HeaderText="Email" >
                            <ItemStyle Font-Size="Small" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Godown_Mobile" HeaderText="Mobile" >
                            <ItemStyle Font-Size="Small" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Godown_Address" HeaderText="Address" >
                            <ItemStyle Font-Size="Small" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Latitude" HeaderText="Latitude" >
                            <ItemStyle Font-Size="Small" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Longitude" HeaderText="Longitude" >
                            <ItemStyle Font-Size="Small" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Godown_Scientific_Capacity" HeaderText="Scientific Capacity" >
                            <ItemStyle Font-Size="Small" />
                            </asp:BoundField>
                        </Columns>

                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" CssClass="Freezing"/>
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />

                    </asp:GridView>
                        </td>
                </tr>
                 </table>
         </div>
           
         <center>
        <div id="rptDiv" runat="server">
            <table style="width: 100%">
                 <tr>
                    <td align="center">
                        <rsweb:ReportViewer ID="RV_ARStockProc_Para" runat="server" SizeToReportContent="True"
                            Width="100%" ProcessingMode="Remote" ZoomMode="PageWidth" Height="600px">
                        </rsweb:ReportViewer>
                    </td>
                </tr>
            </table>
        </div>
        </center>
        </div>
    </form>
</body>
</html>
