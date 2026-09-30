<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/StateMaster.master" AutoEventWireup="true" CodeFile="VerifyGodown_Old.aspx.cs" Inherits="StatePages_VerifyGodown" %>

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
                                                    <asp:Label ID="lblGodownMaster" runat="server" Text="Verify Godown" Font-Bold="true"
                                                        Font-Size="12pt" ForeColor="whitesmoke"></asp:Label>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td style="height: 5px" colspan="2"></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lbldist" runat="server" Text="District Name" Font-Size="10pt" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlDistrict" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged" Height="25px" Width="200px" CssClass="tb6">
                                                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label1" runat="server" Text="Branch Name" Font-Size="10pt" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlbranch" runat="server" Height="25px" Width="200px" CssClass="tb6" OnSelectedIndexChanged="ddlbranch_SelectedIndexChanged" AutoPostBack="true">
                                                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <%-- <asp:Label ID="lblRowCount" runat="server" ForeColor="navy" Font-Bold="true" Text="Total Record "
                                                        Font-Size="10pt"></asp:Label>--%>
                                                </td>
                                                <td align="right">
                                                    <%--<a href="javascript:popMe('../SampleQuantity.htm');" style="text-decoration: underline">(Qty. in Qtls.kgsgms)</a>--%>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 5px" colspan="2"></td>
                                            </tr>
                                            <tr>
                                                <td align="center" valign="top" colspan="4">
                                                    <asp:GridView ID="godown_GridView" runat="server" DataKeyNames="Godown_ID" AutoGenerateColumns="False"
                                                        CellPadding="2" Width="100%" AllowPaging="True" AllowSorting="True"
                                                        OnSelectedIndexChanged="godown_GridView_SelectedIndexChanged"
                                                        OnPageIndexChanging="godown_GridView_PageIndexChanging" PageSize="20"
                                                        Font-Size="9pt">
                                                        <Columns>
                                                            <%--<asp:CommandField ShowSelectButton="True" HeaderText="Verify"
                                                                ItemStyle-ForeColor="green">
                                                                <ItemStyle ForeColor="green"></ItemStyle>
                                                            </asp:CommandField>--%>

                                                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="Verify">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkEdit" runat="server" ForeColor="Green" Text="" OnClick="Edit">
                                                                    Verify
                                                                    </asp:LinkButton>
                                                                    <%-- <asp:LinkButton ID="lnkDelete" runat="server" ForeColor="Red" Text="" OnClick="Delete">
                                                                    Delete
                                                                    </asp:LinkButton>--%>
                                                                    <asp:HiddenField ID="hdnGodown_ID" runat="server" Value='<%# Eval("Godown_ID") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="S.N.">
                                                                <ItemTemplate>
                                                                    <%#Container.DataItemIndex+1%>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" Width="20px" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="District_Name" HeaderText="District Name" />
                                                            <asp:BoundField DataField="District_Id" HeaderText="District Id" />
                                                            <asp:BoundField DataField="Branch" HeaderText="Branch" />
                                                            <asp:BoundField DataField="BranchID" HeaderText="Branch ID" />
                                                            <asp:BoundField DataField="Godown_ID" HeaderText="Godown ID" />
                                                            <asp:BoundField DataField="Godown_Name" HeaderText="Godown Name" />
                                                            <asp:BoundField DataField="Godown_Capacity" HeaderText="Max Capacity" />
                                                            <asp:BoundField DataField="Godown_Scientific_Capacity" HeaderText="Scientific Capacity" />
                                                            <asp:BoundField DataField="Hired_Type" HeaderText="Hired Type" />
                                                            <asp:BoundField DataField="Storage_Type" HeaderText="Storage Type" />
                                                            <asp:BoundField DataField="Licence_No" HeaderText="Licence No" />
                                                            <asp:BoundField DataField="Licence_Validity" HeaderText="Licence Validity" />
                                                            <asp:BoundField DataField="CreatedDate" HeaderText="Created Date Time" />
                                                            <asp:BoundField DataField="gdnStatus" HeaderText="Godown Status" />
                                                            <asp:BoundField DataField="Godown_ID">
                                                                <HeaderStyle Font-Size="0pt" />
                                                                <ItemStyle Font-Size="0pt" ForeColor="White" />
                                                            </asp:BoundField>
                                                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="Delete">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkdelete" runat="server" ForeColor="Red" Text="" OnClick="Delete">
                                                                    Delete
                                                                    </asp:LinkButton>
                                                                    <%-- <asp:LinkButton ID="lnkDelete" runat="server" ForeColor="Red" Text="" OnClick="Delete">
                                                                    Delete
                                                                    </asp:LinkButton>--%>
                                                                    <asp:HiddenField ID="hdnGodown_ID2" runat="server" Value='<%# Eval("Godown_ID") %>' />
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
                            <img alt="New" src="images/new6.gif" id="new" runat="server" />

                        </td>
                    </tr>
                    <tr>
                        <td style="height: 5px" colspan="2"></td>

                    </tr>
                    <tr>
                        <td style="height: 10px" colspan="2"></td>
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

