<%@ Page Title="" Language="C#" ViewStateEncryptionMode="Always" MasterPageFile="../MasterPages/adminMaster.master" Debug="true" AutoEventWireup="true" CodeFile="InsertTourProgram.aspx.cs" Inherits="Admin_InsertTourProgram" %>
 

<asp:Content ID="Content1" ContentPlaceHolderID="PM" Runat="Server">
<link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
<script src="../NEW_CSS/js/jquery-1.12.4.js"></script>
<script src="../NEW_CSS/js/jquery-ui.js"></script>
<script src="<%= ResolveUrl("~/NEW_CSS/js/jquery-ui.js") %>"></script>

<script type="text/javascript">
        $(function () {
            $('#<%=datepicker.ClientID%>').datepicker({
                dateFormat: 'dd/mm/yy',
                defaultDate: -1,
                minDate: -1,
                maxDate: 28
            });
        });

        $(function () {
            $('#<%=btnSave.ClientID%>').bind('click', function () {
                var txtVal =  $('#<%=datepicker.ClientID%>').val();

                if (isDate(txtVal))
                    return true;
                else
                    alert('Invalid Date');
        });

        function isDate(datepicker) {
            var currVal = datepicker;
            if (currVal == '')
                return false;

            var rxDatePattern = /^(\d{1,2})(\/|-)(\d{1,2})(\/|-)(\d{4})$/; //Declare Regex
            var dtArray = currVal.match(rxDatePattern); // is format OK?

            if (dtArray == null)
                return false;

            //Checks for mm/dd/yyyy format.
            dtDay  = dtArray[1];
            dtMonth = dtArray[3];
            dtYear = dtArray[5];

            if (dtMonth < 1 || dtMonth > 12)
                return false;
            else if (dtDay < 1 || dtDay > 31)
                return false;
            else if ((dtMonth == 4 || dtMonth == 6 || dtMonth == 9 || dtMonth == 11) && dtDay == 31)
                return false;
            else if (dtMonth == 2) {
                var isleap = (dtYear % 4 == 0 && (dtYear % 100 != 0 || dtYear % 400 == 0));
                if (dtDay > 29 || (dtDay == 29 && !isleap))
                    return false;
            }
            return true;
            }
        });
 
  </script>
     <div class="alert-warning img-thumbnail" style="margin-bottom:10px!important;">
        <b style="font-size:large; font-family:'Times New Roman', Times, serif">Insert Tour Program</b>  
    </div>
    <div>  
      
        <table style="width:100%;"> 
            <tr> <td colspan="2">  
                    <asp:Label ID="lblErr" runat="server" Font-Bold="true"></asp:Label>  
                </td>  </tr> 
            <tr>  
                <td class="style1">  
                    Tour Program in English:</td>  
                <td class="style2">  
                   <textarea runat="server" id="txtTPE" width="80%" style="width:800px; height:110px;"></textarea>
                          <asp:RequiredFieldValidator ID="rfvTourProgramE" runat="server"
           ControlToValidate="txtTPE" ErrorMessage="Please Insert Tour Program in English" ForeColor="#CC3300" 
                              ValidationGroup="myValidator"></asp:RequiredFieldValidator>             
                </td>  
                <td>  
                     </td>  
            </tr>
              <tr>  
                <td class="style1">  
                    Tour Program in Hindi:</td>  
                <td class="style2">  
                   <textarea runat="server" id="txtTPH" width="80%" style="width:800px; height:110px;"></textarea>
                          <asp:RequiredFieldValidator ID="rfvTourProgramH" runat="server"
           ControlToValidate="txtTPH" ErrorMessage="Please Insert Tour Program in Hindi" ForeColor="#CC3300" 
                              ValidationGroup="myValidator"></asp:RequiredFieldValidator>             
                </td>  
                <td>  
                     </td>  
            </tr>
             <tr><td>Date:</td>

                <td><input runat="server" type="text" id="datepicker" maxlength="10" />
                  <asp:RequiredFieldValidator ID="rvDatepicker" color="red" runat="server"
           ControlToValidate="datepicker" ErrorMessage="Please Insert Date" ForeColor="#CC3300"
           ValidationGroup="myValidator"></asp:RequiredFieldValidator>
          
                </td>
            </tr>   
             
           
            <tr>  
                <td class="style1">  
                     </td>  
                <td class="style2">  
                    <asp:Button ID="btnSave" runat="server" onclick="btnSave_Click" Text="Submit" ValidationGroup="myValidator"/>  
                </td>  
                <td>  
                     </td>  
            </tr>  
        </table>  
      
    </div>  
    

</asp:Content>

