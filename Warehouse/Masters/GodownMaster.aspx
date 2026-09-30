<%@ Page Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="GodownMaster.aspx.cs"
    Inherits="Masters_GodownMaster" EnableEventValidation="false" ValidateRequest="false" Title="Godown Master" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

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

        function popMe(url) {
            var newWindow;
            newWindow = window.open(url, 'MyWin', 'width=275,height=390,top=1,left=1');

        }

        function chkgod() {

            if (document.getElementById("ctl00_ContentPlaceHolder1_dprlst_Godown").value == "--Select--") {

                alert('Please Select Godown No')

            }
        }

    </script>
    <script type="text/javascript">

        function Validate() {
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
                                                    <asp:Label ID="lblGodownMaster" runat="server" Text="Godown  Master" Font-Bold="true"
                                                        Font-Size="12pt" ForeColor="whitesmoke"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 5px" colspan="2"></td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="2">
                                                    <asp:Label ID="Label18" runat="server" ForeColor="red" Font-Bold="true" Text="पहले से बने हुए गोदामो मे Updation शाखा स्तर से किया जा सकता है। New गोदाम Creation हेतु होम पेज पर प्रदर्शित ईमेल आईडी पर सम्पूर्ण जानकारी के साथ मेल करे।
                                                "
                                                        Font-Size="10pt"></asp:Label>
                                                </td>

                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lblRowCount" runat="server" ForeColor="navy" Font-Bold="true" Text="Total Record "
                                                        Font-Size="10pt"></asp:Label>
                                                </td>
                                                <td align="right">
                                                    <a href="javascript:popMe('../SampleQuantity.htm');" style="text-decoration: underline">(Qty. in Qtls.kgsgms)</a></td>
                                            </tr>
                                            <tr>
                                                <td style="height: 5px" colspan="2"></td>
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
                                                                ItemStyle-ForeColor="red">
                                                                <ItemStyle ForeColor="Red"></ItemStyle>
                                                            </asp:CommandField>
                                                            <asp:CommandField ShowSelectButton="True" HeaderText="Edit"
                                                                ItemStyle-ForeColor="blue">
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
                                                            <asp:BoundField DataField="Licence_No" HeaderText="Licence No" />
                                                            <asp:BoundField DataField="Licence_Validity" HeaderText="Licence Validity" />
                                                            <asp:BoundField DataField="Godown_ID">
                                                                <HeaderStyle Font-Size="0pt" />
                                                                <ItemStyle Font-Size="0pt" ForeColor="White" />
                                                            </asp:BoundField>
                                                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="Print">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkEdit" runat="server" ForeColor="Blue" OnClick="PrintMaster">
                                                                                Print
                                                                    </asp:LinkButton>
                                                                    <asp:HiddenField ID="hdnGodown_ID" runat="server" Value='<%# Eval("Godown_ID") %>' />
                                                                    <asp:HiddenField ID="hdnPrint" runat="server" Value='<%# Eval("[Print]") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
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
                            <img alt="New" src="../images/new6.gif" id="new" runat="server" />

                        </td>
                    </tr>
                    <tr>
                        <td style="height: 5px" colspan="2"></td>

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
                                                        <td style="height: 5px" colspan="2">&nbsp;</td>

                                                    </tr>
                                                    <tr>
                                                        <td style="width: 300px;">
                                                            <asp:Label ID="Label19" runat="server" Font-Bold="True" Font-Size="10pt" ForeColor="Navy"
                                                                Text="JVS Registration ID"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlRegID" runat="server" Width="300px" CssClass="tb6"
                                                                Height="25px" AutoPostBack="True" OnSelectedIndexChanged="ddlRegID_SelectedIndexChanged">
                                                            </asp:DropDownList>

                                                            <asp:Label ID="lblwhname" runat="server" Font-Bold="True" Font-Size="10pt" ForeColor="Navy" Visible="false"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 5px" colspan="2"></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 300px;">
                                                            <asp:Label ID="Label3" runat="server" Text="Godown Name" Font-Bold="True" ForeColor="Navy"
                                                                Font-Size="8pt"></asp:Label></td>
                                                        <td>
                                                            <asp:TextBox ID="txtGodownName" runat="server" Width="300px" Font-Size="8pt" AutoComplete="off"></asp:TextBox>
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td style="width: 300px;">&nbsp;</td>
                                                        <td>&nbsp;</td>
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
                                                        <td style="height: 5px" colspan="2">&nbsp;</td>

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
                                                        <td style="height: 5px" colspan="2">&nbsp;</td>
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
                                                        <td style="height: 5px" colspan="2">&nbsp;</td>
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
                                                        <td style="height: 5px" colspan="2"></td>

                                                    </tr>
                                                    <tr>
                                                        <td style="width: 300px;">
                                                            <asp:Label ID="Label20" runat="server" Font-Bold="True" Font-Size="10pt" ForeColor="Navy"
                                                                Text="L x W x H (in Feet)"></asp:Label>
                                                        </td>
                                                        <td align="left" valign="middle">
                                                            <asp:TextBox ID="txtLenght" Width="50px" Height="20px" runat="server" AutoPostBack="true"
                                                                Text="0"
                                                                OnTextChanged="txtLenght_TextChanged"></asp:TextBox>
                                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" TargetControlID="txtLenght"
                                                                ValidChars="0123456789.">
                                                            </asp:FilteredTextBoxExtender>
                                                            <asp:TextBox ID="txtWidth" Width="50px" Height="20px" runat="server" AutoPostBack="true"
                                                                Text="0" OnTextChanged="txtWidth_TextChanged"></asp:TextBox>
                                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" TargetControlID="txtWidth"
                                                                ValidChars="0123456789.">
                                                            </asp:FilteredTextBoxExtender>
                                                            <asp:TextBox ID="txtHeight" Width="50px" Height="20px" runat="server" AutoPostBack="true"
                                                                Text="0"
                                                                OnTextChanged="txtHeight_TextChanged"></asp:TextBox>&nbsp;&nbsp;&nbsp;Height<=18 Feet
                                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" TargetControlID="txtHeight"
                                                                ValidChars="0123456789.">
                                                            </asp:FilteredTextBoxExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 5px" colspan="2"></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label1" runat="server" Text="Scientific Capacity" Font-Bold="true"
                                                                ForeColor="navy" Font-Size="8pt"></asp:Label>(Qty. in Qtls.kgsgms)</td>
                                                        <td>
                                                            <asp:TextBox ID="txtScientificCapacity" runat="server" Width="150px"
                                                                AutoComplete="off" AutoPostBack="True"
                                                                OnTextChanged="txtScientificCapacity_TextChanged" onkeypress="return isNumberKey2(event)"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtScientificCapacity"
                                                                Display="Dynamic" ErrorMessage="Scientific Capacity field cannot be empty" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                                            <asp:Label ID="lbl_checkcapcity" runat="server" Font-Bold="True"
                                                                ForeColor="Red" Visible="False"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 5px" colspan="2">&nbsp;</td>
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
                                                        <td style="height: 5px" colspan="2"></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label5" runat="server" Text="Hired Type/Godown Type" Font-Bold="true" ForeColor="navy"
                                                                Font-Size="8pt"></asp:Label></td>
                                                        <td>
                                                            <asp:DropDownList ID="ddllst_hired" runat="server" Width="155px" Height="25px"
                                                                AutoPostBack="True" OnSelectedIndexChanged="ddllst_hired_SelectedIndexChanged">
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
                                                        <td style="height: 5px" colspan="2"></td>
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
                                                                <asp:ListItem Text="Silo Bag" Value="SiloBag"></asp:ListItem>
                                                                <asp:ListItem Text="Steel Silo" Value="SteelSilo"></asp:ListItem>


                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 5px" colspan="2"></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <asp:Label ID="Label21" runat="server" Font-Bold="True" Font-Size="10pt" ForeColor="Navy"
                                                                Text="गोदाम परिसर की क्षमता(In M.T.) खरीदी केंद्र स्थापित करने के उद्देश्य से "></asp:Label>
                                                        </td>
                                                        <td align="left" valign="middle">
                                                            <asp:TextBox ID="txtPremiseCpt" Width="150px" Height="20px" runat="server" Text="0"></asp:TextBox>
                                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtPremiseCpt"
                                                                ValidChars="0123456789.">
                                                            </asp:FilteredTextBoxExtender>

                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
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
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label22" runat="server" Font-Bold="True" Font-Size="8pt"
                                                                ForeColor="Navy" Text="Licence Issue Date"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtLicIssueDate" runat="server">
                                                            </asp:TextBox><asp:CalendarExtender ID="CalendarExtender2"
                                                                runat="server" TargetControlID="txtLicIssueDate" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy">
                                                            </asp:CalendarExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lbllidate" runat="server" Font-Bold="True" Font-Size="8pt"
                                                                ForeColor="Navy" Text="Licence Expiry Date"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtlicdate" runat="server">
                                                            </asp:TextBox><asp:CalendarExtender ID="CalendarExtender1"
                                                                runat="server" TargetControlID="txtlicdate" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy">
                                                            </asp:CalendarExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 5px" colspan="2"></td>
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
                                                        <td style="height: 5px" colspan="2"></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label10" runat="server" Text="Latitude" Font-Bold="True" ForeColor="Navy"
                                                                Font-Size="8pt"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtlatitude" runat="server">0</asp:TextBox>ex:26.203194
                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1"
                                                            runat="server" TargetControlID="txtlatitude" FilterType="Custom, Numbers" ValidChars=".">
                                                        </asp:FilteredTextBoxExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 5px" colspan="2"></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label11" runat="server" Text="Longitude" Font-Bold="True" ForeColor="Navy"
                                                                Font-Size="8pt"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtlongitude" runat="server">0</asp:TextBox>ex:78.209267
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
                                                            <asp:DropDownList ID="ddlPBlock" runat="server" AutoPostBack="true" Width="190px" OnSelectedIndexChanged="ddlPBlock_SelectedIndexChanged1">
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
                                                                OnSelectedIndexChanged="ddlWeightmentS_SelectedIndexChanged">
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
                                                        <td>
                                                            <asp:Label ID="Label23" runat="server" Font-Bold="True" Font-Size="8pt" ForeColor="Navy" Text="क्या यह गोदाम PMS एजेंसी द्वारा संचालित किया जावेगा? "></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlselfpms" runat="server" Width="100px" AutoPostBack="true">
                                                                <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                                                <asp:ListItem Value="1">No</asp:ListItem>
                                                                <asp:ListItem Value="0">Yes</asp:ListItem>
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
                                                        <td style="height: 5px" colspan="2"></td>
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
                        <td style="height: 10px" colspan="2"></td>
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
    <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="new" BackgroundCssClass="popup" PopupControlID="pnllogin" CancelControlID="x">
    </asp:ModalPopupExtender>

    <asp:AnimationExtender ID="popupAnimation" runat="server" TargetControlID="new">
        <Animations>
                <OnClick>
         <Parallel AnimationTarget="pnllogin" Duration="0.4" Fps="10">
            <FadeIn />
          </Parallel>
       </OnClick>
        </Animations>
    </asp:AnimationExtender>
    <asp:HiddenField ID="hdnJVS_RegNo" runat="server" Value="0" />
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
    <script language="JavaScript1.2">
var message="MPWLC STORAGE MODULE"
var neonbasecolor="gray"
var neontextcolor="yellow"
var flashspeed=100  //in milliseconds

///No need to edit below this line/////

var n=0
if (document.all||document.getElementById){
document.write('<font color="'+neonbasecolor+'">')
for (m=0;m<message.length;m++)
document.write('<span id="neonlight'+m+'">'+message.charAt(m)+'</span>')
document.write('</font>')
}
else
document.write(message)

function crossref(number){
var crossobj=document.all? eval("document.all.neonlight"+number) : document.getElementById("neonlight"+number)
return crossobj
}

function neon(){

//Change all letters to base color
if (n==0){
for (m=0;m<message.length;m++)
//eval("document.all.neonlight"+m).style.color=neonbasecolor
crossref(m).style.color=neonbasecolor
}

//cycle through and change individual letters to neon color
crossref(n).style.color=neontextcolor

if (n<message.length-1)
n++
else{
n=0
clearInterval(flashing)
setTimeout("beginneon()",1500)
return
}
}

function beginneon(){
if (document.all||document.getElementById)
flashing=setInterval("neon()",flashspeed)
}
beginneon()
    </script>
    <style type="text/css">
        .auto-style1 {
            height: 13px;
        }
    </style>
</asp:Content>

