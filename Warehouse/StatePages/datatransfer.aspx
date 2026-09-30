<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Region_Master.master" AutoEventWireup="true" CodeFile="datatransfer.aspx.cs" Inherits="StatePages_datatransfer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
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

        .status-red {
            background-color: #ffe5e6;
            border: 1px solid #fe0000;
            color: #fe0000;
            border-radius: 4px;
            display: inline-block;
            font-size: 12px;
            min-width: 60px;
            padding: 1px 10px;
            text-align: center;
            text-decoration: none;
        }

        .status-redReject {
            background-color: #cccce3ab;
            border: 1px solid #0000ff;
            /*color: #fe0000;*/
            border-radius: 4px;
            display: inline-block;
            font-size: 12px;
            min-width: 60px;
            padding: 1px 10px;
            text-align: center;
            text-decoration: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="background-color: #808080">
        <h3>From Branch
        </h3>
    </div>
    <table>
        <tr>
            <td>District:
            </td>
            <td>

                <asp:DropDownList ID="DDL_Dist" runat="server"
                    AutoPostBack="True" Width="205px"
                    Height="30px" Font-Bold="true" ForeColor="Navy"
                    CssClass="tb6" OnSelectedIndexChanged="DDL_Dist_SelectedIndexChanged">
                </asp:DropDownList>

            </td>
            <td>Depot:
            </td>
            <td>
                <asp:DropDownList ID="DDL_Depot" runat="server" Width="205px" Height="30px" Font-Bold="true"
                    ForeColor="Navy"
                    CssClass="tb6" AutoPostBack="True" OnSelectedIndexChanged="DDL_Depot_SelectedIndexChanged">
                </asp:DropDownList>

            </td>
        </tr>

    </table>
    <div style="background-color: #808080">
        <h3>To Branch
        </h3>
    </div>
    <table>
        <tr>
            <td>District:
            </td>
            <td>

                <asp:DropDownList ID="ddldist2" runat="server"
                    AutoPostBack="True" Width="205px"
                    Height="30px" Font-Bold="true" ForeColor="Navy"
                    CssClass="tb6" OnSelectedIndexChanged="ddldist2_SelectedIndexChanged">
                </asp:DropDownList>

            </td>
            <td>Depot:
            </td>
            <td>
                <asp:DropDownList ID="ddlbranch2" runat="server" Width="205px" Height="30px" Font-Bold="true"
                    ForeColor="Navy"
                    CssClass="tb6">
                </asp:DropDownList>

            </td>
        </tr>

    </table>
    <table>
        <tr style="margin-left: 50%">
            <td class="col-md-2">
                <asp:TextBox ID="txtSearch" runat="server"
                    CssClass="search-box"
                    Width="250px"
                    placeholder="Search by Godown Details..."
                    onkeyup="filterGrid()" />
                <asp:HiddenField ID="hdnSearchValue" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvgodown" runat="server"
                    OnSelectedIndexChanged="gvgodown_SelectedIndexChanged" CellPadding="5"
                    CellSpacing="10">
                    <Columns>
                        <asp:CommandField ButtonType="Button" HeaderText="Transfer" ShowHeader="True"
                            ShowSelectButton="True" SelectText="Transfer" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
    <script type="text/javascript">
        function filterGrid() {
            var input = document.getElementById('<%= txtSearch.ClientID %>');
            var filter = input.value.toLowerCase();
            var table = document.getElementById('<%= gvgodown.ClientID %>');
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
</asp:Content>

