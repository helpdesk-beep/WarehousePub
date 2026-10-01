<%@ Page Language="C#" MasterPageFile="~/MasterPage/StateMaster.master" AutoEventWireup="true" CodeFile="Delete_Uploaded_DSC.aspx.cs" Inherits="E_WHR_Delete_Uploaded_DSC" Title="Delete DSC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        preid = "ctl00_ContentPlaceHolder1_";
    </script>
    <link href="../assets/New/css/select2.min.css" rel="stylesheet" />
    <style>
        .search-box {
            display: block;
            width: 20%;
            max-width: 500px;
            padding: 10px;
            margin: 0 0 0px 5px;
            font-size: 16px;
            border: 1px solid #ccc;
            border-radius: 6px;
            box-sizing: border-box;
            text-align: left;
            margin-bottom: 10px;
        }
    </style>
    <fieldset style="width: 100%; border: 2px solid navy;">
        <center>
            <%--  </ContentTemplate>
            </asp:UpdatePanel>--%>
            <div>
                <table cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td colspan="6" align="center" valign="top">
                            <fieldset style="width: 100%; border: 1px solid navy;">
                                <center>
                                    <div>
                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr style="background-color: #0bb6e6; height: 25px">
                                                <td colspan="4" align="center">
                                                    <span style="color: White; font-size: 12pt; font-weight: bold">Uploaded DSC</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 10px" colspan="4"></td>
                                            </tr>

                                            <tr>
                                                <td align="left" colspan="4">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td style="height: 10px" colspan="4" align="center">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <!-- District Label -->
                                                <td style="width: 25%;" align="left">
                                                    <asp:Label ID="lblDistrict" runat="server" Text="District" Font-Size="17px"></asp:Label>
                                                </td>

                                                <!-- District Dropdown -->
                                                <td style="width: 25%;" align="left">
                                                    <asp:DropDownList ID="ddlDistrict" runat="server" Height="25px" Width="155px" AutoPostBack="True"
                                                        CssClass="tb6" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>

                                                <!-- Search TextBox -->
                                                <td style="width: 10%;" align="left">
                                                    <asp:TextBox ID="txtSearch" runat="server"
                                                        CssClass="search-box"
                                                        Width="200px"
                                                        placeholder="Search by ..."
                                                        onkeyup="filterGrid()" />
                                                </td>

                                                <!-- Search Button -->
                                                <td style="width: 10%;" align="left">
                                                    <asp:Button ID="Button2" runat="server"
                                                        Text="Search"
                                                        Width="100px"
                                                        CssClass="BTNBLUE"
                                                        OnClick="btnSearch_Click"
                                                        OnClientClick="$('#myspindiv').show();"
                                                        onmousedown="fnChkEmptyData();" />
                                                </td>

                                                <!-- Hidden Field (can be hidden inside a td or outside row) -->
                                                <td style="display: none;">
                                                    <asp:HiddenField ID="hdnSearchValue" runat="server" />
                                                </td>
                                            </tr>

                                            <tr>
                                                <td style="width: 100px" align="left">
                                                    <asp:Label ID="lblBranch" runat="server" Text="Branch" Font-Size="10pt" Font-Bold="true" Visible="false"></asp:Label>
                                                </td>
                                                <td style="width: 200px" align="left">
                                                    <asp:DropDownList ID="ddlDepotList" runat="server" Height="25px" Width="155px" AutoPostBack="True" Visible="false"
                                                        CssClass="tb6"
                                                        OnSelectedIndexChanged="ddlDepotList_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 10px" colspan="4"></td>
                                            </tr>
                                            <tr>

                                                <td align="center" valign="top" colspan="4">
                                                    <asp:GridView ID="godown_GridView" runat="server" DataKeyNames="Aid" AutoGenerateColumns="False"
                                                        CellPadding="2" Width="100%" AllowSorting="True"
                                                        Font-Size="9pt" OnRowDeleting="godown_GridView_RowDeleting">
                                                        <Columns>

                                                            <asp:TemplateField HeaderText="Delete">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton
                                                                        ID="lnkDelete"
                                                                        runat="server"
                                                                        CommandName="Delete"
                                                                        Text="Delete"
                                                                        ForeColor="Red"
                                                                        OnClientClick="return confirm('Are you sure you want to delete this record?');">
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="S.N.">
                                                                <ItemTemplate>
                                                                    <%#Container.DataItemIndex+1%>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" Width="20px" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="SerialNumber" HeaderText="Serial Number" />
                                                            <asp:BoundField DataField="Branch" HeaderText="Branch" />
                                                            <asp:BoundField DataField="Branch_Godown_Name" HeaderText="Branch_Godown_Name" />
                                                            <asp:BoundField DataField="DSC_HOLDER_NAME" HeaderText="DSC_HOLDER_NAME" />
                                                            <asp:BoundField DataField="DSC_IssuerName" HeaderText="DSC_IssuerName" />
                                                            <asp:BoundField DataField="ValidUpto" HeaderText="Valid Upto" />
                                                            <asp:BoundField DataField="DSC_UploadDate" HeaderText="DSC_UploadDate" />
                                                            <asp:BoundField DataField="User_Type" HeaderText="User_Type" />
                                                            <asp:BoundField DataField="Verification_Status" HeaderText="Verification" />
                                                        </Columns>
                                                        <FooterStyle BackColor="#719cb6" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" />
                                                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
                                                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                        <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                                                            Height="20px" Font-Size="10pt" />
                                                        <AlternatingRowStyle BackColor="#eeeeee" />
                                                    </asp:GridView>
                                                    <asp:Label ID="Label3" runat="server" Font-Size="X-Small" ForeColor="#400040"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td style="height: 10px" colspan="4"></td>
                                            </tr>

                                            <tr>
                                                <td>&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" align="center">
                                                    <%--<asp:Button ID="btnupdate" runat="server" Text="Save" Width="100px" CssClass="BTNBLUE"
                                ValidationGroup="validate" onclick="btnupdate_Click" />--%>
                                                    <%-- &nbsp; &nbsp; &nbsp;
                            <asp:Button ID="btn_Close" runat="server" Text="Close" Width="100px" CssClass="BTNBLUE"
                                CausesValidation="false" OnClick="btn_Close_Click" />--%>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </center>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 5px" colspan="4"></td>
                    </tr>
                </table>
            </div>
            <%--  </ContentTemplate>
            </asp:UpdatePanel>--%>
        </center>
    </fieldset>
    <script type="text/javascript" src='<%= ResolveUrl("~/NEW_CSS/js/jquery-1.12.4.js") %>'></script>
    <script type="text/javascript" src="../assets/New/js/select2.min.js"></script>
    <script type="text/javascript">
        $(function () { $("[id*=ddlDistrict]").select2(); });
    </script>
    <%--<script type="text/javascript">
        function filterGrid() {
            var input = document.getElementById(preid + "txtSearch");
            var filter = input.value.toLowerCase();
            var table = document.getElementById("<%= godown_GridView.ClientID %>");
            var trs = table.getElementsByTagName("tr");

            // hidden field me textbox ka value set karo
            document.getElementById("<%= hdnSearchValue.ClientID %>").value = input.value;

            for (var i = 1; i < trs.length; i++) { // skip header row
                var tds = trs[i].getElementsByTagName("td");
                var show = false;
                for (var j = 0; j < tds.length; j++) {
                    if (tds[j].innerText.toLowerCase().indexOf(filter) > -1) {
                        show = true;
                        break;
                    }
                }
                trs[i].style.display = show ? "" : "none";
            }
        }
    </script>--%>

    <script type="text/javascript">
        function filterGrid() {
            var input = document.getElementById('<%= txtSearch.ClientID %>');
            var filter = input.value.toLowerCase();
            var table = document.getElementById('<%= godown_GridView.ClientID %>');
            var trs = table.getElementsByTagName("tr");

            // hidden field me textbox ka value set karo
            document.getElementById('<%= hdnSearchValue.ClientID %>').value = input.value;

            for (var i = 1; i < trs.length; i++) { // skip header row
                var tds = trs[i].getElementsByTagName("td");
                var show = false;
                for (var j = 0; j < tds.length; j++) {
                    if (tds[j].innerText.toLowerCase().indexOf(filter) > -1) {
                        show = true;
                        break;
                    }
                }
                trs[i].style.display = show ? "" : "none";
            }
        }
    </script>

    <script type="text/javascript">
        function fnChkEmptyData() {
            if (document.getElementById(preid + "txtSearch").value == "") {
                alert("Seral Number is required.");
                document.getElementById(preid + "txtSearch").focus();
                validSubmit = 0;
                return returnFalse();
            }
        }
    </script>
    <script type="text/javascript">
        function fnConfirm() {
            if (confirm("The item will be deleted. Are you sure want to continue?") == true)
                return true;
            else
                return false;
        } function fnConfirm() {
            if (confirm("The item will be deleted. Are you sure want to continue?") == true)
                return true;
            else
                return false;
        }
    </script>
</asp:Content>
