<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GodownCheckByName.aspx.cs" Inherits="StatePages_GodownCheckByName" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script type="text/javascript">
        preid = "ctl00_ContentPlaceHolder1_";
    </script>
    <style>
        /* Container styling */
        .godown-container {
            text-align: center;
            padding: 15px;
            background: #f9fbfd;
            border: 1px solid #d0d7de;
            border-radius: 10px;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        }

            /* Label */
            .godown-container b {
                font-size: 16px;
                margin-right: 10px;
                color: #333;
            }

        /* Search box */
        .search-box {
            width: 320px;
            padding: 8px 12px;
            border: 1px solid #ccc;
            border-radius: 8px;
            outline: none;
            font-size: 14px;
            transition: all 0.3s ease;
            margin: 0 15px;
        }

            .search-box:focus {
                border-color: #007bff;
                box-shadow: 0 0 6px rgba(0, 123, 255, 0.4);
            }

        /* Button */
        .BTNBLUE {
            background: linear-gradient(135deg, #007bff, #0056b3);
            border: none;
            color: #fff !important;
            padding: 8px 25px;
            font-size: 14px;
            font-weight: bold;
            border-radius: 8px;
            cursor: pointer;
            transition: 0.3s ease-in-out;
        }

            .BTNBLUE:hover {
                background: linear-gradient(135deg, #0056b3, #00408a);
                transform: translateY(-2px);
                box-shadow: 0px 4px 8px rgba(0,0,0,0.15);
            }
    </style>
    <script type="text/javascript">
        // Show loader on full postback
        function showLoader() {
            document.getElementById("myspindiv").style.display = "block";
        }

        // For AJAX requests (UpdatePanel, ModalPopup, etc.)
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(function () {
            showLoader();
        });

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
            document.getElementById("myspindiv").style.display = "none";
        });

        // For normal postbacks
        window.onload = function () {
            var theForm = document.forms[0];
            if (theForm.attachEvent) {
                theForm.attachEvent("onsubmit", showLoader);
            } else {
                theForm.addEventListener("submit", showLoader, false);
            }
        };
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div>
                <table cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td align="center" valign="top">

                            <center>
                                <div>
                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                        <tr style="background-color: #0bb6e6; height: 25px">
                                            <td colspan="8" align="center">
                                                <asp:Label ID="lblGodownMaster" runat="server" Text="Godown Details" Font-Bold="true"
                                                    Font-Size="15pt" ForeColor="whitesmoke"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 5px" colspan="8"></td>
                                        </tr>
                                        <tr>
                                            <td colspan="8" class="godown-container">
                                                <b>Godown Name :</b>
                                                <asp:TextBox runat="server" CssClass="search-box" placeholder="Search by Godown Name ........"
                                                    onkeyup="filterGrid()" ID="txtGodownID"></asp:TextBox>

                                                <asp:Button runat="server" CssClass="BTNBLUE" Text="Check" ID="btnCheck"
                                                    onmousedown="fnChkEmptyData();"
                                                    OnClick="btnCheck_Click"
                                                    OnClientClick="showLoader()" />

                                                <asp:HiddenField ID="hdnSearchValue" runat="server" />
                                            </td>
                                        </tr>

                                        <%--<tr>
                                            <td align="center" style="width: 200px">
                                                <asp:Button ID="Button1" runat="server" Text="Search" Visible="true" Width="100px" ValidationGroup="A"
                                                    CssClass="BTNBLUE" OnClick="Button1_Click" />
                                            </td>
                                        </tr>--%>
                                        <tr>
                                            <td colspan="8" valign="top" align="center">
                                                <asp:GridView ID="Depositor_Gridview" runat="server" AutoGenerateColumns="False" Width="100%" BackColor="White"
                                                    BorderColor="#CC9966" BorderStyle="Double" BorderWidth="1px" CellPadding="5"
                                                    CellSpacing="2" Font-Size="15pt">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No." ItemStyle-Width="3%">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="District Name">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblDistrict_Name" Width="100%" Text='<%# Eval("District_Name")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <%--<ItemStyle HorizontalAlign="Left" Width="30%" />--%>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Branch Name">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblDepotName" Width="100%" Text='<%# Eval("DepotName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <%--<ItemStyle HorizontalAlign="Left" Width="30%" />--%>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Godown ID">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGodown_ID" Width="100%" Text='<%# Eval("Godown_ID")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <%--<ItemStyle HorizontalAlign="Left" Width="30%" />--%>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Registration No">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGodownReg_ID" Width="100%" Text='<%# Eval("GodownReg_No")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <%--<ItemStyle HorizontalAlign="Left" Width="30%" />--%>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Godown Password">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGodownPwd" Width="100%" Text='<%# Eval("GodownPassword")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <%--<ItemStyle HorizontalAlign="Left" Width="30%" />--%>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Godown Name">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGodown_Name" Width="100%" Text='<%# Eval("Godown_Name")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <%--<ItemStyle HorizontalAlign="Left" Width="30%" />--%>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Godown Capacity">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGodown_Capacity" Width="100%" Text='<%# Eval("Godown_Capacity")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <%-- <ItemStyle HorizontalAlign="Left" Width="30%" />--%>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Godown Scientific Capacity">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGodown_Scientific_Capacity" Width="100%" Text='<%# Eval("Godown_Scientific_Capacity")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <%--<ItemStyle HorizontalAlign="Left" Width="30%" />--%>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Hired Type">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblHired_Type" Width="100%" Text='<%# Eval("Hired_Type")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <%--<ItemStyle HorizontalAlign="Left" Width="30%" />--%>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Is Active">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblIsActive" Width="100%" Text='<%# Eval("IsActive")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <%-- <ItemStyle HorizontalAlign="Left" Width="30%" />--%>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="CreatedDate">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblCreatedDate" Width="100%" Text='<%# Eval("CreatedDate")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <%--<ItemStyle HorizontalAlign="Left" Width="30%" />--%>
                                                        </asp:TemplateField>

                                                    </Columns>
                                                    <FooterStyle BackColor="#719cb6" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" Font-Size="12pt" />
                                                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
                                                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                    <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                                                        Height="20px" Font-Size="12pt" />
                                                    <AlternatingRowStyle BackColor="#eeeeee" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </center>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 5px" colspan="4"></td>
                    </tr>
                </table>
            </div>
        </div>
        <div id="myspindiv" style="display: none; position: fixed; top: 0; left: 0; width: 100%; height: 100%; background-color: rgba(255, 255, 255, 0.7); z-index: 9999; text-align: center;">
            <div style="position: absolute; top: 50%; left: 50%; transform: translate(-50%, -50%);">
                <img src="../images/mpwlc3.gif" alt="Loading..." />
            </div>
        </div>
    </form>

    <script type="text/javascript">
        function fnChkEmptyData() {
            var txt = document.getElementById("txtGodownID");
            if (txt.value.trim() === "") {
                alert("Godown Name is required.");
                txt.focus();
                return false;   // यही सही है
            }
            return true;
        }
    </script>
    <script type="text/javascript">
        function filterGrid() {
            var input = document.getElementById("txtGodownID");
            var filter = input.value.toLowerCase();
            var table = document.getElementById("<%= Depositor_Gridview.ClientID %>");
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
    </script>

</body>
</html>
