<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="Circular_New.aspx.cs" Inherits="Circular_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="Server">
     <style>
        .btnMargin {
            margin-bottom: 10px !important;
        }
    </style>
    <style>
        .Grid {
            background-color: #fff;
            margin: 5px 0 10px 0;
            border: solid 1px #525252;
            border-collapse: collapse;
            font-family: Calibri;
            color: #474747;
            width: 100%;
        }

            .Grid td {
                padding: 2px;
                border: solid 1px #c1c1c1;
            }

            .Grid th {
                padding: 4px 2px;
                color: #fff;
                background: #00aad2 url(Images/grid-header.png) repeat-x top;
                border-left: solid 1px #525252;
                font-size: 15px;
                text-align: center;
            }

            .Grid .alt {
                background: #fcfcfc url(Images/grid-alt.png) repeat-x top;
            }

            .Grid .pgr {
                background: #00aad2 url(Images/grid-pgr.png) repeat-x top;
            }

                .Grid .pgr table {
                    margin: 3px 0;
                }

                .Grid .pgr td {
                    border-width: 0;
                    padding: 0 6px;
                    border-left: solid 1px #666;
                    font-weight: bold;
                    color: #fff;
                    line-height: 12px;
                }

                .Grid .pgr a {
                    color: Gray;
                    text-decoration: none;
                }

                    .Grid .pgr a:hover {
                        color: #000;
                        text-decoration: none;
                    }


                     /* Search box styles */
        .search-container {
            margin-bottom: 15px;
            float: right;
        }

            .search-container input {
                padding: 8px 12px;
                border: 1px solid #ddd;
                border-radius: 4px;
                width: 150px;
                font-size: 14px;
            }

                .search-container input:focus {
                    outline: none;
                    border-color: #007bff;
                    box-shadow: 0 0 5px rgba(0,123,255,0.3);
                }

            .search-container i {
                position: relative;
                left: -25px;
                color: #888;
                cursor: pointer;
            }

        .clear-search {
            position: relative;
            left: -30px;
            color: #888;
            cursor: pointer;
            font-size: 14px;
        }

            .clear-search:hover {
                color: #dc3545;
            }

        .no-records {
            text-align: center;
            padding: 20px;
            color: #666;
            font-style: italic;
        }

        .table-responsive {
            overflow-x: auto;
        }
    </style>
    <%--<div class="alert-warning img-thumbnail" style="margin-bottom: 10px!important;">
        <b style="font-size: large; font-family: 'Times New Roman', Times, serif">View What's New?</b>
    </div>--%>
    <asp:Label ID="lblErr" runat="server"></asp:Label>
    <div>
        <table style="width: 100%;" runat="server" visible="false">
            <tr>
                <td colspan="2">
                    <asp:Label ID="Label1" runat="server" Font-Bold="true"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style1" style="float: right;">Select Document Type:</td>
                <td class="style2">
                    <asp:DropDownList ID="ddlDocType" runat="server" Width="180px" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlDocType_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rvDocType" runat="server"
                        ControlToValidate="ddlDocType" ErrorMessage="Please Select Document Type" ForeColor="#CC3300"
                        ValidationGroup="myValidator"></asp:RequiredFieldValidator>
                </td>
                <td></td>
            </tr>
        </table>
        <br />
        <div>
            
            <div class="search-container">
                <asp:TextBox ID="txtSearch" runat="server"
                    Width="500px"
                    AutoPostBack="true"
                    OnTextChanged="txtSearch_TextChanged"
                    onkeyup="triggerSearch();" />

                <asp:Button ID="btnClear" runat="server" Text="Clear"
                    OnClick="btnClear_Click" />
            </div>
        <asp:GridView ID="gvHindi" runat="server" AutoGenerateColumns="False"
            CssClass="Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
            OnPageIndexChanging="gvHindi_PageIndexChanging" AllowPaging="true" PageSize="30">
            
            <Columns>
                <asp:TemplateField HeaderText="S.No.">
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                        <asp:HiddenField ID="hdnID" runat="server" Value='<%#Eval("id") %>'></asp:HiddenField>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Title" ControlStyle-Width="500px">
                    <ItemTemplate>
                        <asp:Label ID="lbl_TitleHn" runat="server" Text='<%#Eval("title") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Release Date">
                    <ItemTemplate>
                        <asp:Label ID="releaseDate" runat="server" Text='<%#Eval("releaseDate","{0:d}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Expire Date">
                    <ItemTemplate>
                        <asp:Label ID="expireDate" runat="server" Text='<%#Eval("expireDate","{0:d}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>


                <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <a href='<%#Eval("viewfile") %>' target="_blank"><%#Eval("viewfileName") %></a>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
          <%--  <HeaderStyle CssClass="alert-warning" />
            <AlternatingRowStyle CssClass=" alert-danger" />--%>
        </asp:GridView>
     
        </div>
    </div>

    <script type="text/javascript">
        var typingTimer;
        var doneTypingInterval = 500;

        function triggerSearch() {
            clearTimeout(typingTimer);

            typingTimer = setTimeout(function () {
                __doPostBack('<%= txtSearch.UniqueID %>', '');
            }, doneTypingInterval);
        }

        window.onload = function () {
            var txt = document.getElementById('<%= txtSearch.ClientID %>');

            if (txt) {
                txt.focus();

                // cursor end me set hoga
                var len = txt.value.length;
                txt.setSelectionRange(len, len);
            }
        };
    </script>


    <script type="text/javascript">
        function searchGridView() {
            var input = document.getElementById("txtSearch");
            var filter = input.value.toUpperCase();
            var table = document.getElementById("<%= gvHindi.ClientID %>");

            if (!table) return;

            var tr = table.getElementsByTagName("tr");
            var visibleCount = 0;

            // Header skip
            for (var i = 1; i < tr.length; i++) {
                var td = tr[i].getElementsByTagName("td");
                var found = false;

                for (var j = 0; j < td.length; j++) {   // ALL columns include
                    var cellText = td[j].innerText || td[j].textContent;

                    if (cellText.toUpperCase().indexOf(filter) > -1) {
                        found = true;
                        break;
                    }
                }

                if (found) {
                    tr[i].style.display = "";
                    visibleCount++;
                } else {
                    tr[i].style.display = "none";
                }
            }

            // Clear button
            var clearBtn = document.querySelector('.clear-search');
            clearBtn.style.display = filter.length > 0 ? 'inline' : 'none';

            showNoRecordsMessage(visibleCount, table);
        }

        function showNoRecordsMessage(visibleCount, table) {
            var oldMsg = document.getElementById("noRecordsMsg");
            if (oldMsg) oldMsg.remove();

            if (visibleCount === 0) {
                var tbody = table.getElementsByTagName("tbody")[0];
                if (tbody) {
                    var row = tbody.insertRow(1);
                    row.id = "noRecordsMsg";

                    var cell = row.insertCell(0);
                    cell.colSpan = table.rows[0].cells.length;
                    cell.className = "no-records";
                    cell.innerHTML = "No matching records found";
                }
            }
        }

        function clearSearch() {
            document.getElementById("txtSearch").value = "";
            searchGridView();
            document.getElementById("txtSearch").focus();
        }
    </script>
</asp:Content>

