<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UploadPaddyFile.aspx.cs" Inherits="Inspection_State_UploadPaymentFile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Upload Full Payment File</title>
</head>
<body >
    <form id="form1" runat="server">
  <div id="GridPageHeader">
        <div class="ui-dialog ui-widget  ui-corner-all" style="width: 100%; position: relative;">
            <div class="alert" style="font-size: 12pt; font-weight: bold; background-color: #796e6f; color: White; border: 1px solid #47759e; border-radius: 4px; margin-bottom: 20px; padding: 8px 35px 8px 14px; text-shadow: 0 1px 0 rgba(255, 255, 255, 0.5);">
                यहाँ से फुल पेमेंट फाइल अपलोड करे ,पेमेंट फाइल अपलोड करने से पहले <b> "Truncate Table"</b> पर क्लिक करे उसके बाद फाइल अपलोड करे : 
            </div>
        </div>
      <asp:Button ID="Button2" runat="server" Text="Back" CssClass="btn btn-success btn-small" OnClick="Button2_Click" />
        <div class="row-fluid">
            <table class="table table-bordered alertt-success" width="97%" style="border-color: #0066CC;"
                border="1">
                
                <tr>
                    <td style="text-align: right; " width="30%">
                        <span style="color: Red">*</span>
                        <label style="font-size: small;">
                            Select Excel File :
                        </label>
                    </td>
                    <td style="text-align: left;" width="60%">
                        <asp:FileUpload ID="FileUpload1" runat="server" />

                        <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server"  
                            ErrorMessage="Select  Excel File" Text="*" ControlToValidate="FileUpload1" ValidationGroup="btn_Submit">
                        </asp:RequiredFieldValidator>
                    </td>

                  
                </tr>
                 

            </table>
        </div>
        <div class="row-fluid">
            <table class="table table-bordered alertt-success" width="97%" style="border-color: #0066CC;"
                border="1">
                <tr>
                    <td colspan="4" style="text-align: center; width: 25%">

                        <asp:Button ID="btnSaveRecord" runat="server" Text="Upload Full Payment File" ValidationGroup="btn_Submit"
                            CssClass="btn btn-success btn-small" EnableTheming="false" OnClick="btnSaveRecord_Click" />

                         <asp:Button ID="Button1" runat="server" Text="Truncate Table" CssClass="btn btn-success btn-small" OnClick="Button1_Click" />
                    </td>
                </tr>
            </table>
        </div>


        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="btn_Submit" ShowMessageBox ="true" ShowSummary ="false" />
    </div>
    </form>
</body>
</html>
