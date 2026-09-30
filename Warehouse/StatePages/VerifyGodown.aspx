<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/StateMaster.master" AutoEventWireup="true" CodeFile="~/StatePages/VerifyGodown.aspx.cs" Inherits="StatePages_VerifyGodown" Async="true" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <script type="text/javascript" src='https://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.8.3.min.js'></script>
 <script type="text/javascript" src="../assets/New/js/select2.min.js"></script>
 <link href="../assets/New/css/select2.min.css" rel="stylesheet" />
     <script type="text/javascript">
         $(function () {
             $("[id*=ddlDistrict]").select2();
         });
     </script>
<script type="text/javascript">
    $(function () {
        $("[id*=ddlbranch]").select2();
    });
</script>
    <script type="text/javascript">
        function filterGrid() {
            var input = document.getElementById("<%= txtSearch.ClientID %>");
            var filter = input.value.toLowerCase();
            var table = document.getElementById("<%= godown_GridView.ClientID %>");
            var trs = table.getElementsByTagName("tr");

            for (var i = 1; i < trs.length; i++) {
                var display = false;
                var tds = trs[i].getElementsByTagName("td");
                for (var j = 0; j < tds.length; j++) {
                    var cell = tds[j];
                    if (cell && cell.textContent.toLowerCase().indexOf(filter) > -1) {
                        display = true;
                        break;
                    }
                }
                trs[i].style.display = display ? "" : "none";
            }
        }
    </script>

    <link href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" />
       <style type="text/css">
        /* Main Theme Colors */
        :root {
            --navy-primary: #1a4e8a;
            --navy-dark: #003366;
            --text-dark: #333333;
            --border-light: #dee2e6;
        }

        /* Search Box */
        .search-box-container { margin-bottom: 15px; }
        .custom-search {
            width: 380px; height: 38px; padding: 0 15px; font-size: 14px;
            border: 1px solid var(--navy-primary); border-radius: 8px;
            outline: none; transition: 0.25s; box-shadow: 0 2px 4px rgba(0,0,0,0.05);
        }
        .custom-search:focus { border-color: #007bff; box-shadow: 0 0 8px rgba(0,123,255,0.2); }

        /* Professional Grid Styling */
        .grid-container { width: 100%; overflow-x: auto; background: #fff; padding: 10px; border-radius: 8px; }
        
        .ProfessionalGrid { border-collapse: collapse !important; width: 100%; border: 1px solid var(--border-light); }
        
        .ProfessionalGrid th {
            background-color: var(--navy-primary) !important;
            color: #ffffff !important;
            padding: 12px 8px !important;
            text-align: center;
            font-size: 13px;
            border: 1px solid var(--navy-dark) !important;
            white-space: nowrap;
        }

        .ProfessionalGrid td {
            padding: 10px 8px !important;
            color: var(--text-dark) !important;
            background-color: #ffffff !important;
            border: 1px solid var(--border-light) !important;
            font-size: 12px;
            vertical-align: middle;
        }

        .ProfessionalGrid tr:hover td { background-color: #f1f5f9 !important; }
        .alt-row-style td { background-color: #f8f9fa !important; }

        /* Action Buttons */
        .btn-verify {
            color: #28a745 !important; border: 1px solid #28a745;
            font-weight: bold; padding: 4px 8px; border-radius: 4px;
            text-decoration: none; display: inline-block; transition: 0.2s;
        }
        .btn-verify:hover { background-color: #28a745; color: #fff !important; }

        .btn-delete {
            color: #dc3545 !important; border: 1px solid #dc3545;
            font-weight: bold; padding: 4px 8px; border-radius: 4px;
            text-decoration: none; display: inline-block; transition: 0.2s;
        }
        .btn-delete:hover { background-color: #dc3545; color: #fff !important; }

        /* Status Label */
        .status-badge { font-weight: bold; padding: 2px 5px; border-radius: 3px; }
        .status-deleted { color: #dc3545; }
        .status-active { color: var(--navy-primary); }

        fieldset { border: 1px solid var(--navy-primary); border-radius: 8px; padding: 20px; margin-bottom: 20px; }
        .header-strip { background-color: #0bb6e6; color: white; padding: 10px; font-weight: bold; border-radius: 4px 4px 0 0; }
    </style>

    <fieldset>
        <div class="search-box-container" align="left">
            <asp:TextBox ID="txtSearch" runat="server" placeholder="Search Godown records..." CssClass="custom-search" onkeyup="filterGrid();" />
        </div>

        <div class="header-strip" align="center">
            <asp:Label ID="lblGodownMaster" runat="server" Text="Verify Godown Details" Font-Size="12pt"></asp:Label>
        </div>

        <table style="width: 100%; margin-top: 15px; margin-bottom: 15px;">
            <tr>
                <td align="center">
                    <asp:Label runat="server" Text="District Name:" Font-Bold="true" />
                    <asp:DropDownList ID="ddlDistrict" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged" Width="200px" CssClass="select2">
                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td align="left">
                    <asp:Label runat="server" Text="Branch Name:" Font-Bold="true" />
                    <asp:DropDownList ID="ddlbranch" runat="server" Width="200px" CssClass="select2" OnSelectedIndexChanged="ddlbranch_SelectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>

        <div class="grid-container">
            <asp:GridView ID="godown_GridView" runat="server" DataKeyNames="Godown_ID" AutoGenerateColumns="False"
                CssClass="ProfessionalGrid" GridLines="None" Width="100%" AllowPaging="True" 
                OnSelectedIndexChanged="godown_GridView_SelectedIndexChanged"
                OnPageIndexChanging="godown_GridView_PageIndexChanging" PageSize="20">
                
                <AlternatingRowStyle CssClass="alt-row-style" />
                
                <Columns>
                    <asp:TemplateField HeaderText="Verify" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkEdit" runat="server" CssClass="btn-verify" OnClick="Edit" 
                                OnClientClick="return confirm('Are you sure you want to VERIFY this Godown?');">
                                <i class="fa fa-check-circle"></i> Verify
                            </asp:LinkButton>
                            <asp:HiddenField ID="hdngodownname" runat="server" Value='<%# Eval("Godown_Name") %>' />
                            <asp:HiddenField ID="hdnbranchid" runat="server" Value='<%# Eval("BranchID") %>' />
                            <asp:HiddenField ID="hdnGodown_ID" runat="server" Value='<%# Eval("Godown_ID") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="S.N.">
                        <ItemTemplate><%#Container.DataItemIndex+1%></ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="30px" />
                    </asp:TemplateField>

                    <asp:BoundField DataField="District_Name" HeaderText="District" />
                    <asp:BoundField DataField="Branch" HeaderText="Branch" />
                    <asp:BoundField DataField="Godown_ID" HeaderText="Godown ID" />
                    <asp:BoundField DataField="Godown_Name" HeaderText="Godown Name" ItemStyle-Font-Bold="true" />
                    
                    <%-- Value remains exactly as in DB --%>
                    <asp:BoundField DataField="Godown_Capacity" HeaderText="Max Capacity" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="Godown_Scientific_Capacity" HeaderText="Scientific Capacity" ItemStyle-HorizontalAlign="Right" />
                    
                    <asp:BoundField DataField="Hired_Type" HeaderText="Hired Type" />
                    <asp:BoundField DataField="Storage_Type" HeaderText="Storage" />
                    <asp:BoundField DataField="Licence_No" HeaderText="Licence No" />
                    <asp:BoundField DataField="Licence_Validity" HeaderText="Validity" />
                    <asp:BoundField DataField="CreatedDate" HeaderText="Created Date Time" />
                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <span class='<%# Eval("gdnStatus").ToString() == "Deleted" ? "status-badge status-deleted" : "status-badge status-active" %>'>
                                <%# Eval("gdnStatus") %>
                            </span>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkdelete" runat="server" CssClass="btn-delete" OnClick="Delete" 
                                OnClientClick="return confirm('WARNING: Are you sure you want to DELETE this Godown record?');">
                                <i class="fa fa-trash"></i> Delete
                            </asp:LinkButton>
                            <asp:HiddenField ID="hdnGodown_ID2" runat="server" Value='<%# Eval("Godown_ID") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                
                <PagerStyle BackColor="#f1f5f9" ForeColor="Black" HorizontalAlign="Center" Height="30px" />
            </asp:GridView>
        </div>
        
        <div style="margin-top: 20px;">
            <img alt="New" src="images/new6.gif" id="new" runat="server" />
            <asp:ValidationSummary ID="godown_Validationerror" runat="server" ShowMessageBox="True" ShowSummary="False" />
        </div>
    </fieldset>

    <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="new" BackgroundCssClass="popup" PopupControlID="pnllogin" CancelControlID="x" />
</asp:Content>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
    
<%--    <script type="text/javascript">
        $(function () {
            $("[id*=ddlDistrict], [id*=ddlbranch]").select2();
        });
    </script>--%>
</asp:Content>