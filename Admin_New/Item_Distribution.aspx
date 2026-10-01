<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/adminMaster.master" AutoEventWireup="true" CodeFile="Item_Distribution.aspx.cs" Inherits="Admin_New_Item_Distribution" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PM" runat="Server">
    <link href="assets/datatable/css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <link href="assets/datatable/css/buttons.dataTables.min.css" rel="stylesheet" />
    <link href="assets/datatable/css/jquery.dataTables.min.css" rel="stylesheet" />
    <!-- Bootstrap -->
    <script type="text/javascript" src='<%= ResolveUrl("~/NEW_CSS/js/jquery-1.12.4.js") %>'></script>
    <script type="text/javascript">
        window.history.forward();
        function noBack() { window.history.forward(); }
    </script>
    <link href="../assets/css/bootstrap-datepicker.css" rel="stylesheet" />
    <script src="js/uxsolutions-bootstrap-datepicker-133aa3d/dist/js/bootstrap-datepicker.js"></script>
    <style>
        .row {
            margin-left: 0px !important;
            margin-right: 0px !important;
        }
    </style>
    <style>
        @media print {
            @page {
                size: A4;
                padding: 0,0,0,0 !important;
                width: 21.0cm;
                height: 19.7cm;
                display: inline-block;
                left: 0 !important;
            }

            body {
                margin: 0,0,0,0 !important;
                padding: 0,0,0,0 !important;
                height: 100%;
                width: 100%;
            }

            #div {
                margin-left: 0px !important;
                padding-left: 0px !important;
                margin-right: 0px !important;
                padding-right: 0px !important;
                display: inline-block;
                margin: 0,0,0,0 !important;
                padding: 0,0,0,0 !important;
            }
        }
    </style>
    <div class="wrapper">
        <asp:Label ID="lblMsg" runat="server"></asp:Label>
        <fieldset>
            <legend style="background-color: aliceblue;">Item Distribution</legend>
            <div class="row">
                <div class="col-md-2">
                    <label>Item Name<i style="color: red;">*</i></label>
                    <asp:RequiredFieldValidator ID="rfv1" ValidationGroup="a"
                        ErrorMessage="Select Item Name" ToolTip="Enter Inventory Name" Text="<i class='fa fa-exclamation-circle' title='Enter Item Name !'></i>"
                        ControlToValidate="ddlitem" InitialValue="0" CssClass="fa fa-pull-right" ForeColor="Red" Display="Dynamic" runat="server">
                    </asp:RequiredFieldValidator>
                    <asp:DropDownList runat="server" ID="ddlitem" CssClass="form-control" OnSelectedIndexChanged="ddlinventory_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <label>Available Quentity</label>
                        <asp:TextBox runat="server" ReadOnly="true" ID="txtremainning" onkeyup="multiply()" onkeypress="return isNumberKey(this, event);" oninput="validate(this)" CssClass="form-control" AutoComplete="off" placeholder="Enter Available Quentity"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-2">
                    <label>Distribution Date</label>
                    <asp:RequiredFieldValidator runat="server" CssClass="fa fa-pull-right" ID="RequiredFieldValidator6" Display="Dynamic" ControlToValidate="txtDate" ValidationGroup="a" Text="<i class='fa fa-exclamation-circle'></i>" ErrorMessage="Please Enter Date" ForeColor="Red"></asp:RequiredFieldValidator>
                    <div class="form-group">
                        <%--<asp:TextBox ID="txtDate" runat="server" placeholder="dd/mm/yyyy"
                            CssClass="form-control dateAdd" data-date-end-date="0d" autocomplete="off" data-provide="datepicker"
                            onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy"></asp:TextBox>--%>
                        <asp:TextBox runat="server" ID="txtDate" data-date-end-date="0d" data-provide="datepicker" placeholder="DD/MM/YYYY" autocomplete="off" data-date-format="dd/mm/yyyy" data-date-autoclose="true" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-2">
                    <label>Department Name</label>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                        ErrorMessage="Select Department Name" ToolTip="Select department Name" Text="<i class='fa fa-exclamation-circle' title='Enter Item Name !'></i>"
                        ControlToValidate="ddldepartment" InitialValue="0" CssClass="fa fa-pull-right" ForeColor="Red" Display="Dynamic" runat="server">
                    </asp:RequiredFieldValidator>
                    <asp:DropDownList runat="server" ID="ddldepartment" CssClass="form-control">
                    </asp:DropDownList>
                </div>
                <div class="col-md-2">
                    <label>Designation</label>
                    <asp:RequiredFieldValidator runat="server" CssClass="fa fa-pull-right" ID="RequiredFieldValidator4" Display="Dynamic" ControlToValidate="txtDesignationname" ValidationGroup="a" Text="<i class='fa fa-exclamation-circle'></i>" ErrorMessage="Please Enter Designation Name" ForeColor="Red"></asp:RequiredFieldValidator>
                    <asp:TextBox runat="server" ID="txtDesignationname" placeholder="Enter Designation Name" AutoComplete="off" CssClass="form-control" onkeypress="return lettersOnly()"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <label>Employee Name</label>
                        <asp:RequiredFieldValidator runat="server" CssClass="fa fa-pull-right" ID="RequiredFieldValidator2" Display="Dynamic" ControlToValidate="txtemp" ValidationGroup="a" Text="<i class='fa fa-exclamation-circle'></i>" ErrorMessage="Please Enter Employee Name" ForeColor="Red"></asp:RequiredFieldValidator>
                        <asp:TextBox runat="server" ID="txtemp" onkeypress="return lettersOnly()" CssClass="form-control" AutoComplete="off" placeholder="Enter Employee Name"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <div class="form-group">
                        <label>Distributer Name</label>
                        <asp:RequiredFieldValidator runat="server" CssClass="fa fa-pull-right" ID="RequiredFieldValidator3" Display="Dynamic" ControlToValidate="txtdistributer" ValidationGroup="a" Text="<i class='fa fa-exclamation-circle'></i>" ErrorMessage="Please Enter Distributer Name" ForeColor="Red"></asp:RequiredFieldValidator>
                        <asp:TextBox runat="server" ID="txtdistributer" onkeypress="return lettersOnly()" CssClass="form-control" AutoComplete="off" placeholder="Enter Distributer Name"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <label>Distribute Quantity</label>
                        <asp:RequiredFieldValidator runat="server" CssClass="fa fa-pull-right" ID="RequiredFieldValidator5" Display="Dynamic" ControlToValidate="txtdistribute" ValidationGroup="a" Text="<i class='fa fa-exclamation-circle'></i>" ErrorMessage="Please Enter Distribute quantity" ForeColor="Red"></asp:RequiredFieldValidator>
                        <%--<asp:TextBox runat="server" ID="txtdistribute" onkeyup="multiply(), compare()" onkeypress="return isNumber()" MaxLength="6" CssClass="form-control" OnTextChanged="txtdistribute_TextChanged" AutoComplete="true" placeholder="Enter Distribute quantity"></asp:TextBox>--%>
                        <asp:TextBox ID="txtdistribute" runat="server" onkeyup="multiply()" placeholder="Enter Distribute quantity" onkeypress="return isNumber()" CssClass="form-control" MaxLength="8" OnTextChanged="txtdistribute_TextChanged" AutoPostBack="true" autocomplete="off"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <label>Remaining Quantity</label>
                        <asp:TextBox runat="server" ReadOnly="true" ID="txtremai" onkeyup="multiply()" onkeypress="return isNumberKey(this, event);" oninput="validate(this)" CssClass="form-control" AutoComplete="off" placeholder="Enter Remainning quantity"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label>Remark</label>
                        <asp:TextBox runat="server" ID="txtremark" TextMode="MultiLine" onkeypress="return lettersOnly()" CssClass="form-control" AutoComplete="off" placeholder="Enter Remark"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-1" style="margin-top: 25px">
                    <asp:Button runat="server" ID="btnsave" CssClass="btn btn-info btn-block" ValidationGroup="a" Text="Save" OnClick="btnsave_Click" />
                </div>
                <div class="col-md-1" style="margin-top: 25px">
                    <a href="Item_Distribution.aspx" class="btn btn-warning btn-block">Clear</a>
                </div>
            </div>
        </fieldset>
        <div class="row" style="align-content: center">
            <div class="col-md-12">
                <fieldset>
                    <legend>Details</legend>
                    <div class="table-responsive">
                        <asp:GridView runat="server" DataKeyNames="Distribution_Id" ID="GrdDistribution" HeaderStyle-Font-Size="Medium"
                            CssClass="table table-bordered table-hover datatable" AutoGenerateColumns="False" OnRowCommand="GrdDistribution_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No" HeaderStyle-Font-Bold="true" HeaderStyle-BackColor="LightBlue" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' ToolTip='<%# Eval("Distribution_Id").ToString()%>' runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Name" HeaderStyle-Font-Bold="true" HeaderStyle-BackColor="LightBlue" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblInventoryId" Text='<%# Eval("Inventory_Id") %>' Visible="false"></asp:Label>
                                        <asp:Label runat="server" ID="lblInventoryName" Text='<%# Eval("Inventory_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Available Qty" HeaderStyle-Font-Bold="true" HeaderStyle-BackColor="LightBlue" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblAvailable_Qty" Text='<%# Eval("Available_Qty") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Distribution Date" HeaderStyle-Font-Bold="true" HeaderStyle-BackColor="LightBlue" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblDistribution_Date" Text='<%# Eval("Distribution_Date") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Deparment Name" HeaderStyle-Font-Bold="true" HeaderStyle-BackColor="LightBlue" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblDepartment_Id" Visible="false" Text='<%# Eval("Department_Id") %>'></asp:Label>
                                        <asp:Label runat="server" ID="lblDeparment_Name" Text='<%# Eval("Deparment_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Designation" HeaderStyle-Font-Bold="true" HeaderStyle-BackColor="LightBlue" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblDesignation" Text='<%# Eval("Designation") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Employee_Name" HeaderStyle-Font-Bold="true" HeaderStyle-BackColor="LightBlue" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblEmployee_Name" Text='<%# Eval("Employee_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Distributer Name" HeaderStyle-Font-Bold="true" HeaderStyle-BackColor="LightBlue" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblDistributer_Name" Text='<%# Eval("Distributer_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Distribute Quantity" HeaderStyle-Font-Bold="true" HeaderStyle-BackColor="LightBlue" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblDistribute_Quantity" Text='<%# Eval("Distribute_Quantity") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remaining Quantity" HeaderStyle-Font-Bold="true" HeaderStyle-BackColor="LightBlue" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblRemaining_Quantity" Text='<%# Eval("Remaining_Quantity") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remark" HeaderStyle-Font-Bold="true" HeaderStyle-BackColor="LightBlue" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" CssClass="paraGraphtext" ID="lblRemark" Text='<%# Eval("Remark") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action" HeaderStyle-Font-Bold="true" HeaderStyle-BackColor="LightBlue" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEdit" CssClass="btn btn-primary" runat="server" CommandName="EditRecord" CommandArgument='<%# Eval("Distribution_Id").ToString()%>' Text="Edit"></asp:LinkButton>
                                        <%--<asp:LinkButton runat="server" ID="btnEdit" CssClass="fa fa-edit" CommandName="EditRecord" CommandArgument='<%# Eval("Inventory_Id").ToString() %>'></asp:LinkButton>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>No Record Found</EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                </fieldset>
            </div>
        </div>
    </div>
</asp:Content>

