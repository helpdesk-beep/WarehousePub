<%@ Page Title="FCI Offline Inspection Entry" Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="~/BranchPages/Fci_Check_moisture_offline.aspx.cs" Inherits="BranchPages_Fci_Check_moisture_offline" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>FCI Moisture Entry</title>

    <link href="https://cdn.jsdelivr.net/npm/bootstrap@3.4.1/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/css/select2.min.css" rel="stylesheet" />

    <style type="text/css">
        body {
            background: #f5f5f5;
        }

        .panel-custom {
            background: #fff;
            padding: 20px;
            border-radius: 5px;
            box-shadow: 0 2px 5px rgba(0,0,0,.15);
            margin-top: 20px;
        }

        .section-title {
            background: #337ab7;
            color: white;
            padding: 10px;
            margin-bottom: 15px;
            font-size: 18px;
            font-weight: bold;
        }

        .form-group {
            margin-bottom: 15px;
        }

        .grid-container {
            overflow-x: auto;
        }

        /* Select2 bootstrap styling */
        .select2-container--default .select2-selection--single {
            height: 34px !important;
            border: 1px solid #ccc !important;
            border-radius: 4px !important;
        }

            .select2-container--default .select2-selection--single .select2-selection__rendered {
                line-height: 32px !important;
            }

            .select2-container--default .select2-selection--single .select2-selection__arrow {
                height: 34px !important;
            }

        .final-submit-panel {
            background: #fff3cd;
            border: 1px solid #ffeeba;
            padding: 15px;
            border-radius: 5px;
            margin-top: 20px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <asp:Label ID="lblMsg" runat="server"></asp:Label>

        <div class="panel panel-custom">
            <div class="section-title">FCI Offline Inspection Entry</div>

            <%-- INPUT FIELDS --%>
            <div class="row">
                <div class="col-md-1"></div>
                <div class="col-md-3">
                    <asp:Label ID="lblGodown" runat="server" Text="Godown:" Font-Bold="true"></asp:Label>
                    <asp:DropDownList ID="ddlGodown" runat="server" CssClass="form-control search-dropdown" OnSelectedIndexChanged="ddlGodown_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                </div>
                <div class="col-md-3">
                    <label>Stack Name</label>
                    <asp:DropDownList ID="ddlStack" runat="server" CssClass="form-control search-dropdown"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                        ControlToValidate="ddlStack" InitialValue="0" ErrorMessage="Enter Stack Name" ForeColor="#CC3300"
                        ValidationGroup="a"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-3">
                    <label>FCI Checked</label>
                    <asp:DropDownList ID="ddlFCIChecked" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                        <asp:ListItem Text="No" Value="N"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                        ControlToValidate="ddlFCIChecked" InitialValue="0" ErrorMessage="Select FCI Checked" ForeColor="#CC3300"
                        ValidationGroup="a"></asp:RequiredFieldValidator>
                </div>
            </div>

            <div class="row" style="margin-top: 10px">
                <div class="col-md-1"></div>
                <div class="col-md-3">
                    <label>FCI Check Date</label>
                    <asp:TextBox ID="txtfcidate" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-3">
                    <label>FCI Moisture</label>
                    <asp:TextBox ID="txtfcimoisture" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                        ControlToValidate="txtfcimoisture" ErrorMessage="Enter FCI Moisture" ForeColor="#CC3300"
                        ValidationGroup="a"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-3" style="margin-top: 25px;">
                    <%-- Abhi sirf grid me add karne ka button kaam karega --%>
                    <asp:Button ID="btnAddToGrid" runat="server" ValidationGroup="a" Text="➕ Add to Grid" CssClass="btn btn-primary btn-block" OnClick="btnAddToGrid_Click" />
                </div>
            </div>

            <hr />

            <%-- GRID VIEW FOR TEMPORARY RECORDS --%>
            <div class="row" style="margin-top: 25px;" id="Addgrid" runat="server" visible="false">
                <div class="col-md-12">
                    <div class="section-title" style="background: #28a745;">FCI Moisture Inspection Records (Current Batch)</div>

                    <div class="grid-container table-responsive">
                        <asp:GridView ID="gvFciRecords" runat="server" AutoGenerateColumns="False"
                            CssClass="table table-striped table-bordered table-hover"
                            DataKeyNames="Stack_ID" OnRowCommand="gvFciRecords_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="Sr. No.">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="Godown_Name" HeaderText="Godown Name" />
                                <asp:BoundField DataField="Stack_Name" HeaderText="Stack Name" />
                                <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity" />
                                <asp:TemplateField HeaderText="FCI Checked" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFciChecked" runat="server"
                                            Text='<%# Eval("FCI_Checked").ToString() == "Y" ? "Yes" : "No" %>'
                                            CssClass='<%# Eval("FCI_Checked").ToString() == "Y" ? "label label-success" : "label label-danger" %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="FCI_Checked_Date" HeaderText="Check Date" DataFormatString="{0:dd-MM-yyyy}" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="FCI_Moisture" HeaderText="Moisture (%)" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:F2}" />
                                <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnDelete" runat="server" Text="❌ Remove"
                                            CommandName="RemoveRecord" CommandArgument='<%# Container.DataItemIndex %>'
                                            CssClass="btn btn-xs btn-danger" CausesValidation="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <div class="alert alert-warning text-center" style="margin-bottom: 0; font-weight: bold;">
                                    ⚠️ Pehle upar se data add karke "Add to Grid" karein.
                               
                                </div>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                </div>
            </div>

            <%-- FINAL SUBMIT SECTION (Yeh tabhi dikhega jab grid me data hoga) --%>
            <asp:Panel ID="pnlFinalSubmit" runat="server" CssClass="final-submit-panel" Visible="false">
                <div class="row">
                    <div class="col-md-6">
                        <div class="form-group">
                            <label style="color: #856404; font-weight: bold;">📑 Attach Final FCI Document (PDF)</label>
                            <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control" />
                            <asp:RequiredFieldValidator ID="rvFileUpload1" runat="server"
                                ControlToValidate="FileUpload1" ErrorMessage="Final upload ke liye PDF select karna compulsory hai." ForeColor="#CC3300"
                                ValidationGroup="finalSubmit"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-md-6" style="margin-top: 25px; text-align: right;">
                        <asp:Button ID="btnSave" runat="server" ValidationGroup="finalSubmit" Text="💾 Save & Submit to Database" CssClass="btn btn-success" OnClick="btnSave_Click" />
                        <asp:Button ID="btnReset" runat="server" Text="↻ Reset All" CssClass="btn btn-default" CausesValidation="false" OnClick="btnReset_Click" />
                    </div>
                </div>
            </asp:Panel>
            <div class="row" style="margin-top: 25px;">
                <div class="col-md-12">
                    <div class="section-title" style="background: #28a745;">FCI Moisture Inspection Records</div>
                    <div class="grid-container table-responsive">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                            CssClass="table table-striped table-bordered table-hover"
                            DataKeyNames="Stack_ID" OnRowCommand="gvFciRecords_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="Sr. No.">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="Godown_Name" HeaderText="Godown Name" SortExpression="Godown_Name" />
                                <asp:BoundField DataField="Stack_Name" HeaderText="Stack Name" SortExpression="Stack_Name" />
                                <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity" SortExpression="Commodity_Name" />
                                <asp:BoundField DataField="Recorded_By" HeaderText="Recorded By" SortExpression="Recorded_By" />
                                <asp:TemplateField HeaderText="FCI Checked" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFciChecked" runat="server"
                                            Text='<%# Eval("FCI_Checked").ToString() == "Y" ? "Yes" : (Eval("FCI_Checked").ToString() == "N" ? "No" : Eval("FCI_Checked")) %>'
                                            CssClass='<%# Eval("FCI_Checked").ToString() == "Y" ? "label label-success" : "label label-danger" %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="FCI_Checked_Date" HeaderText="Check Date"
                                    DataFormatString="{0:dd-MM-yyyy}" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="FCI_Moisture" HeaderText="Moisture (%)"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:F2}" />
                                <asp:TemplateField HeaderText="Document" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="lnkDoc" runat="server" Text="📄 View PDF"
                                            NavigateUrl='<%# ResolveUrl("~/FCI_Document/" + Eval("FCI_Document").ToString()) %>'
                                            Target="_blank"
                                            Visible='<%# !string.IsNullOrEmpty(Eval("FCI_Document").ToString()) %>'
                                            CssClass="btn btn-xs btn-info" />

                                        <asp:Label ID="lblNoDoc" runat="server" Text="No File" ForeColor="#999"
                                            Visible='<%# string.IsNullOrEmpty(Eval("FCI_Document").ToString()) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center" Visible="false">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEdit" runat="server" Text="✏️ Edit"
                                            CommandName="EditRecord" CommandArgument='<%# Eval("Stack_ID") %>'
                                            CssClass="btn btn-xs btn-warning" CausesValidation="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <div class="alert alert-danger text-center" style="margin-bottom: 0; font-weight: bold;">
                                    ⚠️ Note: Koi bhi data nahi mila (No records found for the selected query).
                    
                                </div>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script type="text/jscript" src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script type="text/javascript" src="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/js/select2.min.js"></script>
    <script type="text/javascript">
        function pageLoad() {
            $('.search-dropdown').select2({
                placeholder: "Type to search...",
                allowClear: true
            });
        }
    </script>
</asp:Content>
