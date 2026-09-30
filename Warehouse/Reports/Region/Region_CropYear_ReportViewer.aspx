<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Region_CropYear_ReportViewer.aspx.cs" Inherits="Reports_Region_Region_CropYear_ReportViewer" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Region Report</title>
    <style type="text/css">
        .style1
        {
            width: 131px;
        }
    </style>
</head>
<body>

      
    <form id="form1" runat="server">
      <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
      </cc1:ToolkitScriptManager>
      
    <div>
    
          <center><h2 style="background-color:Silver">Crop Year Wise Stock Report</h2></center>
    <table border="1" cellpadding="0" cellspacing="0" style="border-style:double; border-width:3; padding:1; BORDER-COLLAPSE: collapse; border-color :Maroon  ; width: 100%; background-color: #ece9d8;" id="">
            <tr>
                <td align="center" style="vertical-align: middle; " class="style1">
                    <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="10pt" Text="Crop Year"></asp:Label>
                </td>
                
                <td align="left" class="style2">
                  <asp:DropDownList ID="ddlcropyear" runat="server" AutoPostBack="True"  Height="25px"
                   Width="150px" onselectedindexchanged="ddlcropyear_SelectedIndexChanged"></asp:DropDownList>
               </td>
                            
           </tr> 

                     
           
   </table>
              <table>
                   <tr>
                       <td style="height: 5px">
                      </td>
                  </tr>
             </table>
             
            <rsweb:ReportViewer ID="Region_CropYearViewer" runat="server" SizeToReportContent="True" Width="100%"  ProcessingMode="Remote"
             ZoomMode="PageWidth" Height="600px">
            </rsweb:ReportViewer>

    
    
    
    
    </div>
    </form>
</body>
</html>
