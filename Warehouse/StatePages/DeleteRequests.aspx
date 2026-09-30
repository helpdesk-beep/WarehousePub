<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/StateMaster.master" AutoEventWireup="true" CodeFile="DeleteRequests.aspx.cs" Inherits="StatePages_DeleteRequests" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        preid = "ctl00_ContentPlaceHolder1_";
    </script>
    <style>
        /* General Page Styling */
        .page-container {
            padding: 20px;
            font-family: Arial, sans-serif;
            margin-left: 20px;
        }

        /* Card Style */
        .card {
            background: #fff;
            border-radius: 8px;
            box-shadow: 0 2px 8px rgba(0,0,0,0.1);
            margin-bottom: 25px;
            padding: 20px;
        }

        .card-header {
            background: teal;
            color: #fff;
            padding: 8px 15px;
            border-radius: 6px 6px 0 0;
        }

        .card-headerNew {
            background: teal;
            color: #fff;
            font-size: 17px;
            width: 20%;
            padding: 1px 0px 1px 10px;
            border-radius: 6px 6px 0 0;
        }

        .card-header h3 {
            margin: 0;
            font-size: 16px;
        }

        .card-body {
            padding: 15px 0px 0px 0px;
            font-size: 12px;
        }

        /* Grid Styling */
        .styled-grid {
            width: 100%;
            border-collapse: collapse;
            margin-top: 10px;
        }

            .styled-grid th {
                /*background: #00796b;*/
                /*color: #fff;*/
                padding: 15px;
                text-align: center;
            }

            .styled-grid td {
                padding: 8px;
                border: 1px solid #ddd;
                text-align: center;
            }

            .styled-grid tr:nth-child(even) {
                background: #f9f9f9;
            }

            .styled-grid tr:hover {
                background: #f1f1f1;
            }

        /* Label count */
        .count-label {
            font-weight: bold;
            color: #00796b;
            margin-left: 10px;
        }

        /* Dropdown */
        .dropdown-style {
            padding: 5px;
            font-size: 14px;
            border: 1px solid #ccc;
            border-radius: 4px;
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

        .fontClass {
            font-size: 18px;
            display: flex;
        }

        .flex-container {
            display: flex;
            justify-content: space-between;
            align-items: center;
        }

        .left {
            text-align: left;
        }

        .right {
            text-align: right;
            padding: 0px 5px 0px 0px;
        }

        .search-box {
            width: 320px;
            padding: 8px 12px;
            border: 1px solid #ccc;
            border-radius: 8px;
            outline: none;
            font-size: 14px;
            transition: all 0.3s ease;
            margin: 5px 15px;
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

        .cardNew {
            border: 1px solid #ccc;
            border-radius: 8px;
            background: #fff;
            box-shadow: 0 2px 6px rgba(0,0,0,0.1);
            margin-bottom: 0px;
        }

        .card-headerNew {
            padding: 15px 20px;
            background: #fff;
            border-bottom: 1px solid #ddd;
            border-radius: 8px 8px 0 0;
        }

        .flex-containerNew {
            display: flex;
            align-items: center;
            justify-content: space-between;
            width: 400px;
        }

        .card-titleNew {
            font-size: 18px;
            font-weight: 600;
            color: #333;
            margin: 0;
        }

        .dropdown-styleNew {
            padding: 6px 10px;
            font-size: 15px;
            border: 1px solid #ccc;
            border-radius: 6px;
            min-width: 220px;
            cursor: pointer;
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

    <div class="page-container">

        <!-- Region Dropdown -->
        <div class="cardNew">
            <div class="card-headerNew flex-containerNew">
                <label class="card-titleNew">Select Region</label>
                <asp:DropDownList ID="ddlRegion" runat="server" TabIndex="1"
                    CssClass="dropdown-styleNew"
                    AutoPostBack="True" OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
        </div>


        <!-- WHR Delete Request -->
        <%--<div class="card">
    <div class="card-header">
        <h3>WHR Delete Request</h3>
    </div>
    <div class="card-body">
        <h2>Total Requests:
            <asp:Label ID="lblwhrcount" runat="server" CssClass="count-label" Text="0"></asp:Label></h2>
        <asp:GridView ID="gvdepositorwhr" runat="server" CssClass="styled-grid"
            AllowPaging="True" AllowSorting="false"
            OnPageIndexChanging="gvdepositorwhr_PageIndexChanging"
            OnRowCommand="gvdepositorwhr_RowCommand">
            <Columns>
                <asp:TemplateField HeaderText="Approve">
                    <ItemTemplate>
                        <asp:LinkButton CssClass="status-red" ID="lnkApprove" runat="server"
                            CommandName="Approve"
                            Text="Approve"
                            OnClientClick="return confirm('Are you sure you want to approve?');">
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Reject">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkReject" runat="server"
                            CommandName="Reject"
                            Text="Reject"
                            OnClientClick="return confirm('Are you sure you want to reject?');">
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>

        </asp:GridView>
    </div>
</div>--%>



        <div class="card">
            <div class="card-header">
                <h3>WHR Delete Request</h3>
            </div>
            <div class="card-body">
                <div class="fontClass flex-container">
                    <div class="left">
                        Total Requests:
                        <asp:Label ID="lblwhrcount" runat="server" CssClass="count-label" Text="0"></asp:Label>
                    </div>
                    <div class="right">
                        <asp:TextBox runat="server" CssClass="search-box" placeholder="Search by WHR Id ........"
                            onkeyup="filterGrid()" ID="txtWHR_ID"></asp:TextBox>

                        <asp:Button runat="server" CssClass="BTNBLUE" Text="Search" ID="btnCheck"
                            onmousedown="fnChkEmptyData();"
                            OnClick="btnSearch_Click"
                            OnClientClick="showLoader()" />

                        <asp:HiddenField ID="hdnSearchValue" runat="server" />
                    </div>
                </div>


                <%--<asp:GridView ID="gvdepositorwhr" runat="server" CssClass="styled-grid"
                    AllowPaging="True" AllowSorting="false" AutoGenerateColumns="False"
                    OnPageIndexChanging="gvdepositorwhr_PageIndexChanging" PageSize="20"
                    OnRowCommand="gvdepositorwhr_RowCommand">

                    <Columns>
                        <asp:TemplateField HeaderText="Delete"> 
                            <ItemTemplate>
                                <asp:LinkButton CssClass="status-red" ID="lnkApprove" runat="server"
                                    CommandName="Approve"
                                    Text="Delete"
                                    OnClientClick="return confirm('Are you sure you want to delete this WHR?');">
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="Req_id" HeaderText="Req ID" />
                        <asp:BoundField DataField="Depotid" HeaderText="Depot Id" />
                        <asp:BoundField DataField="DepotName" HeaderText="Branch Name" />
                        <asp:BoundField DataField="Depositor_Name" HeaderText="Depositor Name" />
                        <asp:BoundField DataField="whr_id" HeaderText="WHR ID" />
                        <asp:BoundField DataField="commodity_id" HeaderText="Commodity ID" />
                        <asp:BoundField DataField="Godown_id" HeaderText="Godown Id" />
                        <asp:BoundField DataField="GodownName" HeaderText="Godown Name" />
                        <asp:BoundField DataField="WHRDate" HeaderText="WHR Date" />
                        <asp:BoundField DataField="No_of_Bags" HeaderText="No. of Bags" />
                        <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                        <asp:BoundField DataField="Operator_Name" HeaderText="Operator Name" />
                        <asp:BoundField DataField="Bm_Name" HeaderText="BM Name" />
                        <asp:BoundField DataField="RequestDate" HeaderText="Request Date" />
                        <asp:BoundField DataField="Status" HeaderText="Status" />

                        <asp:TemplateField HeaderText="Reject">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkReject" runat="server"
                                    CommandName="Reject" CssClass="status-redReject"
                                    Text="Reject"
                                    OnClientClick="return confirm('Are you sure you want to reject?');">
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>--%>

                <%--<asp:GridView ID="gvdepositorwhr" runat="server" CssClass="styled-grid"
                    AllowPaging="True" AllowSorting="false" AutoGenerateColumns="False"
                    OnPageIndexChanging="gvdepositorwhr_PageIndexChanging" PageSize="20"
                    OnRowCommand="gvdepositorwhr_RowCommand">

                    <Columns>
                        <asp:TemplateField HeaderText="Delete">
                            <ItemTemplate>
                                <asp:LinkButton CssClass="status-red" ID="lnkApprove" runat="server"
                                    CommandName="Approve"
                                    CommandArgument="<%# Container.DataItemIndex %>"
                                    Text="Delete"
                                    OnClientClick="return confirm('Are you sure you want to delete this WHR?');">
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="Req_id" HeaderText="Req ID" />
                        <asp:BoundField DataField="Depotid" HeaderText="Depot Id" />
                        <asp:BoundField DataField="DepotName" HeaderText="Branch Name" />
                        <asp:BoundField DataField="Depositor_Name" HeaderText="Depositor Name" />
                        <asp:BoundField DataField="whr_id" HeaderText="WHR ID" />
                        <asp:BoundField DataField="commodity_id" HeaderText="Commodity ID" />
                        <asp:BoundField DataField="Godown_id" HeaderText="Godown Id" />
                        <asp:BoundField DataField="GodownName" HeaderText="Godown Name" />
                        <asp:BoundField DataField="WHRDate" HeaderText="WHR Date" />
                        <asp:BoundField DataField="No_of_Bags" HeaderText="No. of Bags" />
                        <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                        <asp:BoundField DataField="Operator_Name" HeaderText="Operator Name" />
                        <asp:BoundField DataField="Bm_Name" HeaderText="BM Name" />
                        <asp:BoundField DataField="RequestDate" HeaderText="Request Date" />
                        <asp:BoundField DataField="Status" HeaderText="Status" />

                        <asp:TemplateField HeaderText="Reject">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkReject" runat="server"
                                    CommandName="Reject"
                                    CommandArgument="<%# Container.DataItemIndex %>"
                                    CssClass="status-redReject"
                                    Text="Reject"
                                    OnClientClick="return confirm('Are you sure you want to reject?');">
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>--%>


                <asp:GridView ID="gvdepositorwhr" runat="server" CssClass="styled-grid"
                    AllowPaging="True" AllowSorting="false" AutoGenerateColumns="False"
                    OnPageIndexChanging="gvdepositorwhr_PageIndexChanging" PageSize="20"
                    OnRowCommand="gvdepositorwhr_RowCommand"
                    DataKeyNames="whr_id,Req_id,Depotid">

                    <Columns>
                        <asp:TemplateField HeaderText="Delete">
                            <ItemTemplate>
                                <asp:LinkButton CssClass="status-red" ID="lnkDelete" runat="server"
                                    CommandName="DeleteRow"
                                    CommandArgument='<%# Container.DataItemIndex %>'
                                    Text="Delete"
                                    OnClientClick="return confirm('Are you sure you want to delete this WHR?');">
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="Req_id" HeaderText="Req ID" />
                        <asp:BoundField DataField="Depotid" HeaderText="Depot Id" />
                        <asp:BoundField DataField="DepotName" HeaderText="Branch Name" />
                        <asp:BoundField DataField="Depositor_Name" HeaderText="Depositor Name" />
                        <asp:BoundField DataField="whr_id" HeaderText="WHR ID" />
                        <asp:BoundField DataField="commodity_id" HeaderText="Commodity ID" />
                        <asp:BoundField DataField="Godown_id" HeaderText="Godown Id" />
                        <asp:BoundField DataField="GodownName" HeaderText="Godown Name" />
                        <asp:BoundField DataField="WHRDate" HeaderText="WHR Date" />
                        <asp:BoundField DataField="No_of_Bags" HeaderText="No. of Bags" />
                        <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                        <asp:BoundField DataField="Operator_Name" HeaderText="Operator Name" />
                        <asp:BoundField DataField="Bm_Name" HeaderText="BM Name" />
                        <asp:BoundField DataField="RequestDate" HeaderText="Request Date" />
                        <asp:BoundField DataField="Status" HeaderText="Status" />

                        <asp:TemplateField HeaderText="Reject">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkReject" runat="server"
                                    CommandName="Reject"
                                    CommandArgument='<%# Container.DataItemIndex %>'
                                    CssClass="status-redReject"
                                    Text="Reject"
                                    OnClientClick="return confirm('Are you sure you want to reject?');">
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>



                <div id="myspindiv" style="display: none; position: fixed; top: 0; left: 0; width: 100%; height: 100%; background-color: rgba(255, 255, 255, 0.7); z-index: 9999; text-align: center;">
                    <div style="position: absolute; top: 50%; left: 50%; transform: translate(-50%, -50%);">
                        <img src="../images/mpwlc3.gif" alt="Loading..." />
                    </div>
                </div>
            </div>
        </div>


        <!-- Receipt Delete Request -->
        <%--<div class="card">
            <div class="card-header">
                <h3>Receipt Delete Request</h3>
            </div>
            <div class="card-body">
                <h2>Total Records:--%>
        <asp:Label ID="lblreceiptcount" runat="server" CssClass="count-label" Text="0" Visible="false"></asp:Label>
        <%--</h2>--%>
        <asp:GridView ID="gvreceipt" runat="server" CssClass="styled-grid" Visible="false"
            AllowPaging="True" AllowSorting="True"
            OnPageIndexChanging="gvreceipt_PageIndexChanging"
            OnRowCommand="gvreceipt_RowCommand">
            <Columns>
                <asp:CommandField HeaderText="Approve" SelectText="Approve" ShowSelectButton="True" />
                <asp:ButtonField HeaderText="Reject" Text="Reject" CommandName="reject" />
            </Columns>
        </asp:GridView>
        <%--</div>
        </div>--%>

        <!-- WHR Delete Request (Opening) -->
        <%--<div class="card">
            <div class="card-header">
                <h3>WHR Delete Request (Opening)</h3>
            </div>
            <div class="card-body">
                <h2>Total Requests:--%>
        <asp:Label ID="lblwhrdel" runat="server" CssClass="count-label" Text="0" Visible="false"></asp:Label><%--</h2>--%>
        <asp:GridView ID="gvwhrs" runat="server" CssClass="styled-grid" Visible="false"
            AllowPaging="True" AllowSorting="True"
            OnPageIndexChanging="gvwhrs_PageIndexChanging">
            <Columns>
                <asp:CommandField HeaderText="Approve" SelectText="Approve" ShowSelectButton="True" />
            </Columns>
        </asp:GridView>
        <%-- </div>
        </div>--%>

        <!-- Gatepass Delete Request -->
        <%--<div class="card">
            <div class="card-header">
                <h3>Gatepass Delete Request</h3>
            </div>
            <div class="card-body">
                <h2>Total Requests:--%>
        <asp:Label ID="lblgatepasscount" runat="server" CssClass="count-label" Text="0" Visible="false"></asp:Label><%--</h2>--%>
        <asp:GridView ID="gvgatepass" runat="server" CssClass="styled-grid" Visible="false"
            AllowPaging="True" AllowSorting="True"
            OnPageIndexChanging="gvgatepass_PageIndexChanging">
            <Columns>
                <asp:CommandField HeaderText="Approve" SelectText="Approve" ShowSelectButton="True" />
            </Columns>
        </asp:GridView>
        <%-- </div>
        </div>--%>
    </div>
    <script type="text/javascript">
        function fnChkEmptyData() {
            var txt = document.getElementById(preid + "txtWHR_ID");
            if (txt.value.trim() === "") {
                alert("Please enter WHR Id.");
                txt.focus();
                return false;   // यही सही है
            }
            return true;
        }
    </script>
    <%--<script type="text/javascript">
        function filterGrid() {
            var input = document.getElementById(preid + "txtWHR_ID");
            var filter = input.value.toLowerCase();
            var table = document.getElementById("<%= gvdepositorwhr.ClientID %>");
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
            var input = document.getElementById("<%= txtWHR_ID.ClientID %>");
            var filter = input.value.toLowerCase();
            var table = document.getElementById("<%= gvdepositorwhr.ClientID %>");
            var trs = table.getElementsByTagName("tr");

            document.getElementById("<%= hdnSearchValue.ClientID %>").value = input.value;
            //alert("aaya" + input.value);

            for (var i = 1; i < trs.length; i++) { // skip header row
                var tds = trs[i].getElementsByTagName("td");
                var show = false;
                for (var j = 0; j < tds.length; j++) {
                    if (tds[j].textContent.toLowerCase().indexOf(filter) > -1) {
                        show = true;
                        break;
                    }
                }
                trs[i].style.display = show ? "" : "none";
            }
        }
    </script>

</asp:Content>
