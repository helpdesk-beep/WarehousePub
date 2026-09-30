<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/FCI_Master_New.master" AutoEventWireup="true" CodeFile="Schedule_FCI_Employee.aspx.cs" Inherits="FCI_Schedule_FCI_Employee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <script type="text/javascript">
        window.history.forward();
        function noBack() { window.history.forward(); }
    </script>
    
    <style type="text/css">
        ul.svertical {
            width: 220px;
            overflow: auto;
            background: #f4f4f4;
            margin: 0;
            padding: 0;
            padding-top: 7px;
            list-style-type: none;
        }
        ul.svertical li {
            text-align: right;
        }
        ul.svertical li a {
            position: relative;
            display: inline-block;
            text-indent: 5px;
            overflow: hidden;
            background: rgb(1, 138, 180);
            font: bold 16px Germand;
            text-decoration: none;
            padding: 5px;
            margin-bottom: 5px;
            color: White;
            -moz-box-shadow: inset -7px 0 5px rgba(114,114,114, 0.8);
            -webkit-box-shadow: inset -7px 0 5px rgba(114,114,114, 0.8);
            box-shadow: inset -7px 0 5px rgba(114,114,114, 0.8);
            -moz-transition: all 0.2s ease-in-out;
            -webkit-transition: all 0.2s ease-in-out;
            -o-transition: all 0.2s ease-in-out;
            -ms-transition: all 0.2s ease-in-out;
            transition: all 0.2s ease-in-out;
        }
        ul.svertical li a:hover {
            padding-right: 30px;
            color: Black;
            background: rgb(153,249,75);
            -moz-box-shadow: inset -3px 0 2px rgba(114,114,114, 0.8);
            -webkit-box-shadow: inset -3px 0 5px rgba(114,114,114, 0.8);
            box-shadow: inset -3px 0 5px rgba(114,114,114, 0.8);
        }
        ul.svertical li a:before {
            content: "";
            position: absolute;
            left: 0;
            top: 0;
            border-style: solid;
            border-width: 70px 0 0 20px;
            border-color: transparent transparent transparent #f4f4f4;
        }

        * { box-sizing: border-box; -moz-box-sizing: border-box; }
        .page {
            width: 21cm;
            min-height: 29.7cm;
            padding: 2cm;
            margin: 1cm auto;
            border: 1px #D3D3D3 solid;
            border-radius: 5px;
            background: white;
            box-shadow: 0 0 5px rgba(0, 0, 0, 0.1);
        }
        .subpage {
            padding: 1cm;
            border: 5px red solid;
            height: 237mm;
            outline: 2cm #FFEAEA solid;
        }
        @page {
            size: A4;
            margin: 0;
            font-size: smaller;
        }
        @media print {
            .page {
                margin: 0;
                border: initial;
                border-radius: initial;
                width: initial;
                min-height: initial;
                box-shadow: initial;
                background: initial;
                page-break-after: always;
                font-size: smaller;
            }
            html, body {
                width: 210mm;
                height: 297mm;
                font-size: smaller;
            }
        }
        page[size="A4"] {
            background: white;
            width: 21cm;
            height: 29.7cm;
            display: block;
            margin: 0 auto;
            margin-bottom: 0.5cm;
            box-shadow: 0 0 0.5cm rgba(0,0,0,0.5);
        }
        @media print {
            body, page[size="A4"] {
                margin: 0;
                box-shadow: 0;
                font-size: smaller;
            }
        }
        .style7 { height: 15px; }
        .style8 { height: 20px; }

        .button {
            background-color: #4CAF50;
            border: none;
            color: white;
            padding: 0px 0px;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            font-size: 12px;
            font-weight: bold;
            margin: 4px 2px;
            -webkit-transition-duration: 0.4s;
            transition-duration: 0.4s;
            cursor: pointer;
        }
        .button1 { background-color: white; color: black; border: 2px solid #4CAF50; }
        .button1:hover { background-color: #4CAF50; color: white; }
        .button2 { background-color: white; color: black; border: 2px solid #008CBA; }
        .button2:hover { background-color: #008CBA; color: white; }
        .button3 { background-color: white; color: black; border: 2px solid #f44336; }
        .button3:hover { background-color: #f44336; color: white; }
        .button6 { background-color: white; color: black; border: 2px solid #008CBA; }
        .button6:hover { background-color: #008CBA; color: white; }

        .modal-backdrop {
            display: none;
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background-color: rgba(0,0,0,0.6);
            z-index: 1000;
        }
        .modern-modal {
            display: none;
            position: fixed;
            top: 50%;
            left: 50%;
            transform: translate(-50%, -50%);
            width: 90%;
            max-width: 1200px;
            max-height: 90vh;
            background: #fff;
            border-radius: 10px;
            border: 3px solid #008CBA;
            z-index: 1001;
            overflow: hidden;
            box-shadow: 0 5px 15px rgba(0,0,0,0.3);
        }
        .modal-container {
            padding: 20px;
            overflow-y: auto;
            max-height: 85vh;
            position: relative;
        }
        .modal-close-btn {
            position: absolute;
            right: 10px;
            top: 10px;
            background: #f44336;
            color: white;
            border: none;
            border-radius: 50%;
            width: 35px;
            height: 35px;
            font-size: 18px;
            font-weight: bold;
            cursor: pointer;
            display: flex;
            align-items: center;
            justify-content: center;
            z-index: 10;
        }
        .modal-close-btn:hover {
            background-color: #c62828;
        }

        .main-table { width: 100%; border-collapse: collapse; }
        .header-title { text-align: center; background: #F7ECDD; color: #cb4e48; font-size: 18px; font-weight: bold; padding: 8px; border: 1px solid #ddd; }
        .text-center { text-align: center; padding: 5px; }
        .input { width: 95%; height: 28px; padding: 5px; }
        .dropdown { width: 95%; height: 32px; }
        .btn-primary { background-color: #008CBA; color: white; border: none; padding: 8px 20px; margin: 5px; cursor: pointer; }
        .btn-primary:hover { background-color: #005f73; }
        .btn-danger { background-color: #f44336; color: white; border: none; padding: 8px 20px; }
        .btn-danger:hover { background-color: #c62828; }
        .grid { width: 100%; border: 1px solid #ddd; }
        .grid th { background: #F7ECDD; color: #cb4e48; }
        .grid tr:nth-child(even) { background: #f2f2f2; }
        
        .text {
            border: 1px solid #ddd;
            padding: 5px;
        }
        .tb6 {
            border: 1px solid #ddd;
            padding: 3px;
        }
        .btneditstyle {
            background-color: #008CBA;
            color: white;
            border: none;
            padding: 5px 10px;
            cursor: pointer;
        }
        .btneditstyle:hover {
            background-color: #005f73;
        }
        
        .modal-show {
            display: block !important;
        }
        
        .gridview-container {
            overflow-x: auto;
            width: 100%;
        }
        .gridview-container table {
            width: 100%;
            border-collapse: collapse;
        }
        .gridview-container th, .gridview-container td {
            padding: 8px;
            text-align: left;
            border: 1px solid #ddd;
        }
    </style>

    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            window.showModal = function () {
                $('#modalBackdrop').fadeIn(200);
                $('#modernModal').fadeIn(200);
                $('body').css('overflow', 'hidden');
            };

            window.closeModal = function () {
                $('#modalBackdrop').fadeOut(200);
                $('#modernModal').fadeOut(200);
                $('body').css('overflow', '');
            };

            $(document).on('click', '#modalBackdrop', function () {
                closeModal();
            });

            $(document).on('click', '#modernModal', function (e) {
                e.stopPropagation();
            });

            $(document).on('click', '.modal-close-btn', function () {
                closeModal();
            });
        });

        function preventInput(evnt) {
            if (evnt.which != 9) evnt.preventDefault();
        }

        function ConfirmOnDelete() {
            return confirm("Are you sure want to Delete This Inspection ?");
        }

        function openModalAndShowPanel() {
            showModal();
            return false;
        }

        function ShowModalFromServer() {
            showModal();
        }
    </script>

    <div runat="server">
        <div id="modalBackdrop" class="modal-backdrop"></div>
        <div id="modernModal" class="modern-modal">
            <button class="modal-close-btn" title="Close">X</button>
            <div class="modal-container">
                <asp:Panel ID="pnllogin" runat="server">
                    <div style="background-color: rgba(255,255,255); max-height: 85vh; overflow-y: auto;">
                        <div id="divNewInsp" runat="server" style="width: 100%;">
                            <table align="center" style="width: 100%; border: #008CBA; border-style: solid; border-width: 0px;">
                                <tr>
                                    <td style="height: 5px;"></td>
                                </tr>
                                <tr>
                                    <td align="center" style="border: #E6C79D; border-style: solid; border-width: 2px; width: 100%" colspan="6">
                                        <span style="color: #cb4e48; font-weight: bold; font-size: 17px">Scheduled Branch Inspection Officer Details</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 5px;" colspan="6"></td>
                                </tr>
                                <tr>
                                    <td colspan="6" align="center" style="font-size: small;">
                                        Total Record : <asp:Label ID="lblTotalInsp" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6" valign="top" align="center">
                                        <div class="gridview-container">
                                            <asp:GridView ID="Gridview_OfficerPreviousInsp" runat="server" DataKeyNames="ID"
                                                AutoGenerateColumns="False" Width="100%" Font-Size="10pt" Font-Bold="true" BackColor="White"
                                                BorderColor="#008CBA" BorderStyle="Solid" BorderWidth="1px" CellPadding="4" CellSpacing="0" 
                                                OnRowCommand="Gridview_OfficerPreviousInsp_RowCommand"
                                                OnRowDataBound="Gridview_OfficerPreviousInsp_RowDataBound">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No.">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1%>
                                                            <asp:HiddenField ID="hdnId" runat="server" Value='<%# Eval("ID") %>' />
                                                            <asp:HiddenField ID="hdnEmpID" runat="server" Value='<%# Eval("Employee_ID") %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle Width="5%" HorizontalAlign="Center" />
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Employee_ID" HeaderText="Unique/PF ID" />
                                                    <asp:BoundField DataField="Officer_Name" HeaderText="Officer Name" />
                                                    <asp:BoundField DataField="Distirct_name" HeaderText="District" />
                                                    <asp:BoundField DataField="Depotname" HeaderText="Branch" />
                                                    <asp:BoundField DataField="Insp_Type" HeaderText="Inspection Type" />
                                                    <asp:TemplateField HeaderText="Edit">
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnEdit" Text="Edit" runat="server" CommandName="EditInspection" CssClass="btneditstyle" CommandArgument='<%# Container.DataItemIndex %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle Width="10%" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <HeaderStyle BackColor="#F7ECDD" Font-Bold="True" ForeColor="#cb4e48" HorizontalAlign="Center" Height="30px" />
                                                <AlternatingRowStyle BackColor="#eeeeee" />
                                                <RowStyle Height="25px" />
                                            </asp:GridView>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 5px;"></td>
                                </tr>
                                <tr>
                                    <td align="center" style="border: #E6C79D; border-style: solid; border-width: 2px;" colspan="6">
                                        <span style="color: #cb4e48; font-weight: bold; font-size: 17px">Allot Branch Inspection</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 10px;"></td>
                                </tr>
                                <tr>
                                    <td style="padding: 5px; width: 15%;">
                                        <asp:Label ID="Label13" runat="server" Text="Inspection Type : "></asp:Label>
                                    </td>
                                    <td style="padding: 5px; width: 18%;">
                                        <asp:DropDownList ID="ddlquater" runat="server" AutoPostBack="true" Width="100%" Height="30px" Font-Bold="true" ForeColor="Navy" CssClass="tb6" OnSelectedIndexChanged="ddlquater_SelectedIndexChanged">
                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                            <asp:ListItem Value="1">1st Quarter</asp:ListItem>
                                            <asp:ListItem Value="2">2nd Quarter</asp:ListItem>
                                            <asp:ListItem Value="3">3rd Quarter</asp:ListItem>
                                            <asp:ListItem Value="4">4th Quarter</asp:ListItem>
                                            <asp:ListItem Value="5">Half Yearly Inspection</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td style="padding: 5px; width: 15%;">
                                        <asp:Label ID="Label14" runat="server" Text="Allot Month :"></asp:Label>
                                    </td>
                                    <td style="padding: 5px; width: 18%;">
                                        <asp:DropDownList ID="ddlmonth" runat="server" AutoPostBack="false" Width="100%" Height="30px" Font-Bold="true" ForeColor="Navy" CssClass="tb6"></asp:DropDownList>
                                    </td>
                                    <td style="padding: 5px; width: 15%;">
                                        <asp:Label ID="Label7" runat="server" Text="Verification Type : "></asp:Label>
                                    </td>
                                    <td style="padding: 5px; width: 19%;">
                                        <asp:DropDownList ID="ddlverification" runat="server" AutoPostBack="false" Width="100%" Height="30px" Font-Bold="true" ForeColor="Navy" CssClass="tb6">
                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                            <asp:ListItem Value="1">General Inspection</asp:ListItem>
                                            <asp:ListItem Value="2">Physical Verification</asp:ListItem>
                                            <asp:ListItem Value="3">Both</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding: 5px;">
                                        <asp:Label ID="Label4" runat="server" Text="Officer Name : "></asp:Label>
                                    </td>
                                    <td style="padding: 5px;">
                                        <asp:TextBox ID="txtInspOffName" runat="server" class="text" type="text" Height="30px" Width="100%" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td style="padding: 5px;">
                                        <asp:Label ID="Label5" runat="server" Text="Designation :"></asp:Label>
                                    </td>
                                    <td style="padding: 5px;">
                                        <asp:TextBox ID="txtDesig" runat="server" class="text" type="text" Height="30px" Width="100%" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td style="padding: 5px;">
                                        <asp:Label ID="Label6" runat="server" Text="CUG/Alternet Mob No :"></asp:Label>
                                    </td>
                                    <td style="padding: 5px;">
                                        <asp:TextBox ID="txtCug" runat="server" class="text" type="text" Height="30px" Width="100%" ReadOnly="true"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding: 5px;">
                                        <asp:Label ID="Label9" runat="server" Text="District : "></asp:Label>
                                    </td>
                                    <td style="padding: 5px;">
                                        <asp:DropDownList ID="ddl_dist" runat="server" AutoPostBack="true" Width="100%" Height="30px" Font-Bold="true" ForeColor="Navy" CssClass="tb6" OnSelectedIndexChanged="ddl_dist_SelectedIndexChanged"></asp:DropDownList>
                                    </td>
                                    <td style="padding: 5px;">
                                        <asp:Label ID="Label1" runat="server" Text="Branch : "></asp:Label>
                                    </td>
                                    <td style="padding: 5px;">
                                        <asp:DropDownList ID="ddl_branch" runat="server" AutoPostBack="True" Width="100%" Height="30px" Font-Bold="true" ForeColor="Navy" CssClass="tb6" OnSelectedIndexChanged="ddl_branch_SelectedIndexChanged"></asp:DropDownList>
                                    </td>
                                    <td style="padding: 5px;">
                                        <asp:Label ID="Label8" runat="server" Text="Order (Letter No.) : "></asp:Label>
                                    </td>
                                    <td style="padding: 5px;">
                                        <asp:TextBox ID="txt_OrderNo" runat="server" class="text" type="text" Height="30px" Width="100%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding: 5px;">
                                        <asp:Label ID="Label11" runat="server" Text="Order Date : "></asp:Label>
                                    </td>
                                    <td style="padding: 5px;">
                                        <asp:TextBox ID="txt_InspDate" runat="server" class="text" type="date" Height="30px" Width="100%"></asp:TextBox>
                                    </td>
                                    <td style="padding: 5px;">
                                        <asp:Label ID="Label2" runat="server" Text="Manager Name : "></asp:Label>
                                    </td>
                                    <td style="padding: 5px;">
                                        <asp:TextBox ID="txtManagerNM" runat="server" class="text" type="text" Height="30px" Width="100%"></asp:TextBox>
                                    </td>
                                    <td style="padding: 5px;">
                                        <asp:Label ID="Label3" runat="server" Text="Manager CUG No : "></asp:Label>
                                    </td>
                                    <td style="padding: 5px;">
                                        <asp:TextBox ID="txtcugno" runat="server" class="text" type="text" Height="30px" Width="100%" MaxLength="10"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 10px;"></td>
                                </tr>
                                <tr>
                                    <td colspan="6" align="center" style="padding: 10px;">
                                        <asp:Button class="button button6" ID="btn_saveInspDate" runat="server" Text="Submit" TabIndex="11" Width="200px" Height="35px" OnClick="btn_saveInspDate_Click"></asp:Button>
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button class="button button6" ID="btnclear" runat="server" Text="Reset" TabIndex="12" Width="200px" Height="35px" OnClick="btnclear_Click"></asp:Button>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 10px;"></td>
                                </tr>
                            </table>
                            <asp:Label ID="Label12" runat="server" Font-Bold="true" Font-Size="Medium"></asp:Label>
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </div>

        <!-- Main Content Table -->
        <table align="center" style="width: 100%; border: #008CBA; border-style: solid; border-width: 0px;">
            <tr>
                <td align="center" style="border: #E6C79D; border-style: solid; border-width: 2px;" colspan="4">
                    <span style="color: #cb4e48; font-weight: bold; font-size: 17px">FCI Employee Details</span>
                </td>
            </tr>
            <tr>
                <td style="height: 5px;" colspan="4"></td>
            </tr>
            <tr>
                <td colspan="4" align="center" style="font-size: small;">
                    Total Record : <asp:Label ID="lblOfficerList" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="4" valign="top" align="center">
                    <div class="gridview-container">
                        <asp:GridView ID="Gridview_IsnpOff" runat="server" DataKeyNames="FCI_ID"
                            AutoGenerateColumns="False" Width="100%" Font-Size="10pt" Font-Bold="true"
                            BackColor="White" BorderColor="#008CBA" BorderStyle="Solid" BorderWidth="1px"
                            CellPadding="4" CellSpacing="0"
                            OnRowCommand="Gridview_IsnpOff_RowCommand"
                            OnRowDataBound="Gridview_IsnpOff_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No." ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                        <asp:HiddenField ID="hdnpfid" runat="server" Value='<%# Eval("FCI_ID") %>' />
                                        <asp:HiddenField ID="hdnMobile" runat="server" Value='<%# Eval("Mobile_No") %>' />
                                        <asp:HiddenField ID="hdnName" runat="server" Value='<%# Eval("Emp_Name") %>' />
                                        <asp:HiddenField ID="hdnDesignation" runat="server" Value='<%# Eval("Designation") %>' />
                                        <asp:HiddenField ID="hdnDistrict" runat="server" Value='<%# Eval("District_ID") %>' />
                                        <asp:HiddenField ID="hdnBranch" runat="server" Value='<%# Eval("Branch_ID") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="FCI_ID" HeaderText="FCI ID" />
                                <asp:BoundField DataField="Emp_Name" HeaderText="Name" />
                                <asp:BoundField DataField="Designation" HeaderText="Designation" />
                                <asp:BoundField DataField="Mobile_No" HeaderText="Mobile No" />
                                <asp:BoundField DataField="District_Name" HeaderText="District" />
                                <asp:BoundField DataField="DepotName" HeaderText="Branch" />
                                <asp:TemplateField HeaderText="Schedule">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkBtnEdit" runat="server" Text="Schedule" 
                                            CommandName="ScheduleInspection" CommandArgument='<%# Container.DataItemIndex %>'
                                            Style="background-color: #008CBA; color: white; padding: 5px 10px; text-decoration: none; border-radius: 3px;">
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="10%" />
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle BackColor="#F7ECDD" Font-Bold="True" ForeColor="#cb4e48" HorizontalAlign="Center" Height="30px" />
                            <AlternatingRowStyle BackColor="#eeeeee" />
                            <RowStyle Height="28px" />
                        </asp:GridView>
                    </div>
                </td>
            </tr>
        </table>
    </div>

    <script type="text/javascript">
        if (typeof Sys !== 'undefined' && Sys.WebForms && Sys.WebForms.PageRequestManager) {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
                $('.modal-close-btn').off('click').on('click', function () {
                    if (typeof closeModal === 'function') closeModal();
                });
            });
        }
    </script>
</asp:Content>