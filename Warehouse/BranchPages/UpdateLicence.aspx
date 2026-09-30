<%@ Page Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="UpdateLicence.aspx.cs" Inherits="BranchPages_UpdateLicence" Title="Update Licence" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        function CheckNumeric(e, tx) {
            var AsciiCode = e.keyCode ? e.keyCode : e.which ? e.which : e.charCode;
            if ((AsciiCode < 46 && AsciiCode != 8 && AsciiCode != 9) || (AsciiCode > 57)) {
                alert('Please enter only numbers !');
                return false;
            }

        }

        function isNumberKey(key) {
            //getting key code of pressed key
            var keycode = (key.which) ? key.which : key.keyCode;
            //comparing pressed keycodes

            if (keycode > 31 && (keycode < 48 || keycode > 57) && keycode != 47) {
                alert(" You can enter only characters 0 to 9 ");
                return false;
            }
            else return true;
        }

        function isNumberKey2(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode

            if (charCode == 46) {
                var inputValue = $("#inputfield").val()
                if (inputValue.indexOf('.') < 1) {
                    return true;
                }
                return false;
            }
            if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }

  function popMe(url)
  {
    var newWindow;
    newWindow=window.open(url,'MyWin','width=275,height=390,top=1,left=1');

  }
  
  function chkgod()
  {
  
    if(document.getElementById("ctl00_ContentPlaceHolder1_dprlst_Godown").value=="--Select--")
  {
  
  alert('Please Select Godown No')
  
  }
  }
  
    </script>
    <script type="text/javascript">

        function Validate() 
        {
            var email = document.getElementById('ctl00_ContentPlaceHolder1_txt_emailid');
            var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
            if (document.getElementById("ctl00_ContentPlaceHolder1_txtGodownName").value == "") {
                alert("Please Enter Godown Name");
                document.getElementById("ctl00_ContentPlaceHolder1_txtGodownName").focus();
                return false;
            }
            else if (document.getElementById("ctl00_ContentPlaceHolder1_txt_mobile").value == "") {
                alert("Please Enter Mobile No.");
                document.getElementById("ctl00_ContentPlaceHolder1_txt_mobile").focus();
                return false;
            }
           
            else if (document.getElementById("ctl00_ContentPlaceHolder1_txt_APN").value == "") {
                alert("Please Enter Authorize Person Name");
                document.getElementById("ctl00_ContentPlaceHolder1_txt_APN").focus();
                return false;
            }
            else if (document.getElementById("ctl00_ContentPlaceHolder1_txt_emailid").value == "") {
                alert("Please Enter Email Id");
                document.getElementById("ctl00_ContentPlaceHolder1_txt_emailid").focus();
                return false;
            }
            else if (document.getElementById("ctl00_ContentPlaceHolder1_txtlicnum").value == "") {
                alert("Please Enter Licence No");
                document.getElementById("ctl00_ContentPlaceHolder1_txtlicnum").focus();
                return false;
            }
            else if (document.getElementById("ctl00_ContentPlaceHolder1_txtlicdate").value == "") {
                alert("Please Enter Licence Date");
                document.getElementById("ctl00_ContentPlaceHolder1_txtlicdate").focus();
                return false;
            }
            else if (document.getElementById("ctl00_ContentPlaceHolder1_txt_address").value == "") {
                alert("Please Enter Address");
                document.getElementById("ctl00_ContentPlaceHolder1_txt_address").focus();
                return false;
            }
            
            else if (document.getElementById("ctl00_ContentPlaceHolder1_txtCapacity").value == "") {
                alert("Please Enter Maximum Capacity");
                document.getElementById("ctl00_ContentPlaceHolder1_txtCapacity").focus();
                return false;
            }
            else if (document.getElementById("ctl00_ContentPlaceHolder1_txtScientificCapacity").value == "") {
                alert("Please Enter Scientific Capacity");
                return false;
                document.getElementById("ctl00_ContentPlaceHolder1_txtScientificCapacity").focus();
                return false;
            }
            else if (document.getElementById("ctl00_ContentPlaceHolder1_ddlWeightmentS").value == "0") {
            alert("गोदाम पर धर्मकाटा/तौलकाटा उपलब्ध है/नहीं चुने");
                return false;
                document.getElementById("ctl00_ContentPlaceHolder1_ddlWeightmentS").focus();
                return false;
            }
            else if (document.getElementById("ctl00_ContentPlaceHolder1_ddlWeightmentS").value == "2" && document.getElementById("ctl00_ContentPlaceHolder1_ddlWeightmentType").value == "0") {
            alert("धर्मकाटा/तौलकाटा चुने");
                return false;
                document.getElementById("ctl00_ContentPlaceHolder1_ddlWeightmentType").focus();
                return false;
            }

            
            else {
                return true;

            }
        }
       
   </script>
    <fieldset style="width: 1000px; border: 2px solid navy;">
        <center>
            <div>
                <table cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td align="center" valign="top">
                            <fieldset style="width: 980px; border: 1px solid navy;">
                                <center>
                                    <div>
                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr style="background-color: #0bb6e6; height: 25px">
                                                <td colspan="4" align="center">
                                                    <asp:Label ID="lblGodownMaster" runat="server" Text="Verified Godown  Master" Font-Bold="true"
                                                        Font-Size="12pt" ForeColor="whitesmoke"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 5px" colspan="2">
                                                </td>
                                            </tr>
                                             <%-- <tr visible="false">
                                             <td align="center" colspan="2">
                                               <asp:Label ID="Label18" runat="server" ForeColor="red" Font-Bold="true" Text="पहले से बने हुए गोदामो मे Updation शाखा स्तर से किया जा सकता है। New गोदाम Creation हेतु होम पेज पर प्रदर्शित ईमेल आईडी पर सम्पूर्ण जानकारी के साथ मेल करे।
                                                "
                                                        Font-Size="10pt"></asp:Label>
                                             </td>
                                               
                                            </tr>--%>
                                            <tr>
                                             <td align="left">
                                               <asp:Label ID="lblRowCount" runat="server" ForeColor="navy" Font-Bold="true" Text="Total Record "
                                                        Font-Size="10pt"></asp:Label>
                                             </td>
                                                <td align="right">
                                                    <a href="javascript:popMe('../SampleQuantity.htm');" style="text-decoration: underline">
                                                        (Qty. in Qtls.kgsgms)</a></td>
                                            </tr>
                                            <tr>
                                                <td style="height: 5px" colspan="2">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" valign="top" colspan="2">
                                                    <asp:GridView ID="godown_GridView" runat="server" DataKeyNames="Godown_ID" AutoGenerateColumns="False"
                                                        CellPadding="2" Width="100%" AllowPaging="True" AllowSorting="True"
                                                        OnRowDataBound="godown_GridView_RowDataBound" OnSelectedIndexChanged="godown_GridView_SelectedIndexChanged"
                                                        OnRowDeleting="godown_GridView_RowDeleting" 
                                                        OnPageIndexChanging="godown_GridView_PageIndexChanging" PageSize="20"
                                                        Font-Size="9pt">
                                                        <Columns>
                                                            <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" 
                                                                ItemStyle-ForeColor="red" >
<ItemStyle ForeColor="Red"></ItemStyle>
                                                            </asp:CommandField>
                                                            <asp:CommandField ShowSelectButton="True" HeaderText="Edit" 
                                                                ItemStyle-ForeColor="blue" >
<ItemStyle ForeColor="Blue"></ItemStyle>
                                                            </asp:CommandField>
                                                            <asp:TemplateField HeaderText="S.N.">
                                                                <ItemTemplate>
                                                                    <%#Container.DataItemIndex+1%>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" Width="20px" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="Godown_ID" HeaderText="Godown ID" />
                                                            <asp:BoundField DataField="Godown_Name" HeaderText="Godown Name" />
                                                            <asp:BoundField DataField="Godown_Capacity" HeaderText="Max Capacity" />
                                                            <asp:BoundField DataField="Godown_Scientific_Capacity" HeaderText="Scientific Capacity" />
                                                            <asp:BoundField DataField="Hired_Type" HeaderText="Hired Type" />
                                                            <asp:BoundField DataField="Storage_Type" HeaderText="Storage Type" />
                                                            <asp:BoundField DataField="Godown_ID">
                                                                <HeaderStyle Font-Size="0pt" />
                                                                <ItemStyle Font-Size="0pt" ForeColor="White" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Licence_Validity" HeaderText="Licence Validity"/>
                                                            <asp:BoundField DataField="Licence_No" HeaderText="Licence No"/>
                                                        </Columns>
                                                        <FooterStyle BackColor="#719cb6" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" />
                                                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
                                                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                        <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                                                            Height="20px" Font-Size="10pt" />
                                                        <AlternatingRowStyle BackColor="#eeeeee" />
                                                    </asp:GridView>
                                                    <asp:Label ID="Label2" runat="server" Font-Size="X-Small" ForeColor="#400040"></asp:Label></td>
                                            </tr>
                                        </table>
                                    </div>
                                </center>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 5px" colspan="2">
                        </td>
                    </tr>
                    <tr id="PanelGodown" runat="server" visible="False">
                        <td align="center" valign="top">
                            <fieldset style="width: 980px; border: 1px solid navy;">
                                <center>
                                    <div>
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr style="background-color: #0bb6e6; height: 25px">
                                                <td colspan="2" align="center">
                                                    <asp:Label ID="Label7" runat="server" Text="Godown  Master" Font-Bold="true" Font-Size="12pt"
                                                        ForeColor="whitesmoke"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 5px" colspan="2">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 300px;">
                                                    <asp:Label ID="Label3" runat="server" Text="Godown Name" Font-Bold="True" ForeColor="Navy"
                                                        Font-Size="8pt"></asp:Label></td>
                                                <td>
                                                    <asp:TextBox ID="txtGodownName" runat="server" Width="300px" Font-Size="8pt" AutoComplete="off"></asp:TextBox>
                                                    </td>
                                            </tr>
                                          <%--  <tr>
                                                <td style="height: 5px" colspan="2">
                                                </td>
                                            </tr>--%>
                                           <%--<tr>
                                                <td style="width: 300px;">
                                                    <asp:Label ID="lbl_godownname" runat="server" Text="Godown Name" Font-Bold="True" ForeColor="Navy"
                                                        Font-Size="8pt"></asp:Label></td>
                                                <td>
                                                    <asp:TextBox ID="txt_godownName" runat="server" Width="300px" Font-Size="8pt"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtGodownName"
                                                        Display="Dynamic" ErrorMessage="Godown Name field cannot be empty" SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
                                            </tr>--%>
                                            <tr>
                                                <td style="width: 300px;">
                                                    &nbsp;</td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td style="width: 300px;">
                                                    <asp:Label ID="Label12" runat="server" Font-Bold="True" Font-Size="8pt" ForeColor="Navy" Text="Godown Number"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtgodownnum" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 300px;">&nbsp;</td>
                                                <td>&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td style="width: 300px;">
                                                    <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Size="8pt" 
                                                        ForeColor="Navy" Text="BranchName"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlbranch" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 5px" colspan="2">
                                                    &nbsp;</td>

                                            </tr>
                                           <tr>
                                                <td style="width: 300px;">
                                                    <asp:Label ID="lbl_apn" runat="server" Text="Authorize Person Name" Font-Bold="True" ForeColor="Navy"
                                                        Font-Size="8pt"></asp:Label></td>
                                                <td>
                                                    <asp:TextBox ID="txt_APN" runat="server" Width="300px" Font-Size="8pt" AutoComplete="off"></asp:TextBox>
                                                    </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 5px" colspan="2">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lbl_emailid" runat="server" Text="Email Id" Font-Bold="True" ForeColor="Navy"
                                                        Font-Size="8pt"></asp:Label></td>
                                                <td>
                                                    <asp:TextBox ID="txt_emailid" runat="server" Width="300px" Font-Size="8pt" AutoComplete="off"></asp:TextBox>
                                                    </td>
                                            </tr>
                                                <tr>
                                                <td style="height: 5px" colspan="2">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lbl_mobile" runat="server" Text="Mobile" Font-Bold="True" ForeColor="Navy"
                                                        Font-Size="8pt"></asp:Label></td>
                                                <td>
                                                    <asp:TextBox ID="txt_mobile" runat="server" Width="300px" AutoComplete="off" Font-Size="8pt" onkeypress="CheckNumeric(event);"></asp:TextBox>
                                                    </td>
                                            </tr>
                                                 <tr>
                                                <td style="height: 5px" colspan="2">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label4" runat="server" Text="Maximum Capacity" Font-Bold="true" ForeColor="navy"
                                                        Font-Size="8pt"></asp:Label>(Qty. in Qtls.kgsgms)</td>
                                                <td>
                                                    <asp:TextBox ID="txtCapacity" runat="server" Width="150px" AutoComplete="off" onkeypress="return isNumberKey2(event)"></asp:TextBox>
                                                    <asp:FilteredTextBoxExtender ID="txtCapacity_FilteredTextBoxExtender" 
                                                        runat="server" TargetControlID="txtCapacity" FilterType="Custom, Numbers" ValidChars=".">
                                                    </asp:FilteredTextBoxExtender>
                                                    </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 5px" colspan="2">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label1" runat="server" Text="Scientific Capacity" Font-Bold="true"
                                                        ForeColor="navy" Font-Size="8pt"></asp:Label>(Qty. in Qtls.kgsgms)</td>
                                                <td>
                                                    <asp:TextBox ID="txtScientificCapacity" runat="server" Width="150px" 
                                                        AutoComplete="off" AutoPostBack="True" 
                                                        ontextchanged="txtScientificCapacity_TextChanged" onkeypress="return isNumberKey2(event)"></asp:TextBox>
                                                   <%-- <asp:FilteredTextBoxExtender ID="txtScientificCapacity_FilteredTextBoxExtender" 
                                                        runat="server" TargetControlID="txtScientificCapacity" FilterType="Custom, Numbers" ValidChars=".">
                                                    </asp:FilteredTextBoxExtender>--%>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtScientificCapacity"
                                                        Display="Dynamic" ErrorMessage="Scientific Capacity field cannot be empty" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                                    <asp:Label ID="lbl_checkcapcity" runat="server" Font-Bold="True" 
                                                        ForeColor="Red" Visible="False"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 5px" colspan="2">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label5" runat="server" Text="Hired Type/Godown Type" Font-Bold="true" ForeColor="navy"
                                                        Font-Size="8pt"></asp:Label></td>
                                                <td>
                                                    <asp:DropDownList ID="ddllst_hired" runat="server" Width="155px" Height="25px" 
                                                        AutoPostBack="True" onselectedindexchanged="ddllst_hired_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <%--  <asp:DropDownList ID="DropDownList1" runat="server" Width="155px" Height="25px" 
                                                        AutoPostBack="True" onselectedindexchanged="ddllst_hired_SelectedIndexChanged">
                                                        <asp:ListItem Text="Owned" Value="Owned"></asp:ListItem>
                                                        <asp:ListItem Text="Hired" Value="Hired"></asp:ListItem>
                                                        <asp:ListItem Text="Joint Venture(JV)" Value="JointVenture(JV)"> </asp:ListItem>
                                                        <asp:ListItem Text="Others" Value="OtherAgency"></asp:ListItem>
                                                        <asp:ListItem Text="Steel Silo"  Value="SteelSilo"></asp:ListItem>
                                                          <asp:ListItem Text="Markfed" Value="Markfed"></asp:ListItem>
                                                         <asp:ListItem Text="Oil-Fed  "  Value="Oil-Fed"></asp:ListItem>
                                                        <asp:ListItem Text="WDRA"  Value="WDRA"></asp:ListItem>
                                                         <asp:ListItem Text="PVT.PEG"  Value="PVT.PEG"></asp:ListItem>
                                                         <asp:ListItem Text="FCI"  Value="FCI"></asp:ListItem>
                                                         <asp:ListItem Text="CWC"  Value="CWC"></asp:ListItem>
                                                        <asp:ListItem Value="SiloBags">Silo Bags</asp:ListItem>
                                                    </asp:DropDownList>--%>
                                                    </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 5px" colspan="2">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label6" runat="server" Text="Storage Type" Font-Bold="true" ForeColor="navy"
                                                        Font-Size="8pt"></asp:Label></td>
                                                <td>
                                                    <asp:DropDownList ID="ddllst_storage" runat="server" Width="155px" Height="25px">
                                                        <asp:ListItem Text="Covered" Value="Covered"></asp:ListItem>
                                                        <asp:ListItem Text="Permanent(CAP)" Value="Permanent(CAP)"></asp:ListItem>
                                                           <asp:ListItem Text="Temporary(CAP)" Value="Temporary(CAP)"></asp:ListItem>
                                                         <asp:ListItem Text="Silo Bag"  Value="SiloBag"></asp:ListItem>
                                                        <asp:ListItem Text="Steel Silo"  Value="SteelSilo"></asp:ListItem>
                                                         

                                                    </asp:DropDownList> </td>
                                            </tr>
                                             <tr>
                                                 <td>
                                                     &nbsp;</td>
                                                 <td>
                                                     &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lbllicnu" runat="server" Font-Bold="True" Font-Size="8pt" 
                                                        ForeColor="Navy" Text="Licence No"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtlicnum" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;</td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lbllidate" runat="server" Font-Bold="True" Font-Size="8pt" 
                                                        ForeColor="Navy" Text="Licence Date"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtlicdate" runat="server">
                                                    </asp:TextBox><asp:CalendarExtender ID="CalendarExtender1"
                                                        runat="server" TargetControlID="txtlicdate" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy">
                                                    </asp:CalendarExtender>
                                                </td>
                                            </tr>
                                             <tr>
                                                <td style="height: 5px" colspan="2">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label8" runat="server" Text="Address" Font-Bold="True" ForeColor="Navy"
                                                        Font-Size="8pt"></asp:Label></td>
                                                <td>
                                                    <asp:TextBox ID="txt_address" runat="server" Width="300px" AutoComplete="off" Font-Size="8pt" Height="50px" TextMode="MultiLine"></asp:TextBox>
                                                    </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 5px" colspan="2">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
<asp:Label ID="Label10" runat="server" Text="Latitude" Font-Bold="True" ForeColor="Navy"
                                                        Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td><asp:TextBox ID="txtlatitude" runat="server">0</asp:TextBox>ex:26.203194
                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" 
                                                        runat="server" TargetControlID="txtlatitude" FilterType="Custom, Numbers" ValidChars=".">
                                                    </asp:FilteredTextBoxExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 5px" colspan="2">

                                                </td>
                                            </tr>
                                             <tr>
                                                <td>
<asp:Label ID="Label11" runat="server" Text="Longitude" Font-Bold="True" ForeColor="Navy"
                                                        Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td><asp:TextBox ID="txtlongitude" runat="server">0</asp:TextBox>ex:78.209267
                                                      <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" 
                                                        runat="server" TargetControlID="txtlongitude" FilterType="Custom, Numbers" ValidChars=".">
                                                    </asp:FilteredTextBoxExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label15" runat="server" Font-Bold="True" Font-Size="8pt" ForeColor="Navy" Text="खण्ड"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlPBlock" runat="server" AutoPostBack="true"  Width="190px" OnSelectedIndexChanged="ddlPBlock_SelectedIndexChanged1">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label16" runat="server" Font-Bold="True" Font-Size="8pt" ForeColor="Navy" Text="गाँव"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlVillage" runat="server" Width="190px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label13" runat="server" Font-Bold="True" Font-Size="8pt" ForeColor="Navy" Text="खशरा नंबर"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtkhasra" runat="server">0</asp:TextBox>
                                                   
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style1"></td>
                                                <td class="auto-style1"></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label14" runat="server" Font-Bold="True" Font-Size="8pt" ForeColor="Navy" Text="राकवा"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtrakwa" runat="server">0</asp:TextBox>
                                                    
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label17" runat="server" Font-Bold="True" Font-Size="8pt" ForeColor="Navy" Text="गोदाम पर धर्मकाटा/तौलकाटा उपलब्ध है/नहीं "></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlWeightmentS" runat="server" Width="100px" AutoPostBack="true"
                                                        onselectedindexchanged="ddlWeightmentS_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                     <asp:ListItem Value="1">No</asp:ListItem>
                                                      <asp:ListItem Value="2">Yes</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="ddlWeightmentType" runat="server" Width="190px" Visible="false">
                                                     <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                    <asp:ListItem Value="WB">Weighbridge</asp:ListItem>
                                                     <asp:ListItem Value="BS">Beam Scale</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 5px" colspan="2"></td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" align="center">
                                                    <asp:Button ID="btnUpdate" runat="server" Text="Update" Width="100px" OnClientClick="return Validate()" CssClass="BTNBLUE"
                                                        OnClick="btnUpdate_Click" />
                                                    &nbsp;&nbsp;&nbsp;
                                                    <asp:Button ID="btnCan" runat="server" Text="Cancel" Width="100px" CssClass="BTNBLUE" OnClientClick="return Validate()"
                                                        OnClick="btnCan_Click" CausesValidation="false" /></td>
                                            </tr>
                                            <tr>
                                                <td style="height: 5px" colspan="2">
                                                </td>
                                            </tr>
                                        </table>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="btnUpdate" />
                                            </Triggers>
                                            </asp:UpdatePanel>
                                    </div>
                                </center>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 10px" colspan="2">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <asp:Button ID="btnaddnew" runat="server" Width="100px" CssClass="BTNBLUE" OnClick="btnaddnew_Click"
                                Text="Add New" CausesValidation="False" />
                            &nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btn_Close" runat="server" Text="Close" Width="100px" CssClass="BTNBLUE"
                                CausesValidation="false" OnClick="btn_Close_Click" />
                            <asp:Label ID="lblMsg" ForeColor="Red" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 10px" colspan="2">
                            <asp:ValidationSummary ID="godown_Validationerror" runat="server" ShowMessageBox="True"
                                ShowSummary="False" />
                        </td>
                    </tr>
                </table>
            </div>
        </center>
    </fieldset>
</asp:Content>

