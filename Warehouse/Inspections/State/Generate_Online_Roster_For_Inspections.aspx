<%@ Page Title="" Language="C#" MasterPageFile="~/Inspections/Masters/State.master" AutoEventWireup="true" CodeFile="~/Inspections/State/Generate_Online_Roster_For_Inspections.aspx.cs" Inherits="Inspections_State_Generate_Online_Roster_For_Inspections" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="assets/datatable/css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <link href="assets/datatable/css/buttons.dataTables.min.css" rel="stylesheet" />
    <link href="assets/datatable/css/jquery.dataTables.min.css" rel="stylesheet" />
    <!-- Bootstrap -->
    <script type="text/javascript" src='<%= ResolveUrl("~/NEW_CSS/js/jquery-1.12.4.js") %>'></script>
    <script type="text/javascript">
        window.history.forward();
        function noBack() { window.history.forward(); }
    </script>
    <link href="../Assets/css/bootstrap-datepicker.css" rel="stylesheet" />
    <script src="../Assets/js/bootstrap-datepicker.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <asp:ValidationSummary ID="ValidationSummary1" ShowSummary="false" ShowMessageBox="true" runat="server" ForeColor="Red" ValidationGroup="a" />
        <asp:Label ID="lblMsg" runat="server"></asp:Label>
        <div runat="server" id="divallot" visible="false">
            <fieldset>
                <legend style="text-align: center; background-color: aliceblue;">Allot Branch Inspection</legend>
                <div class="row">
                    <div class="col-md-2">
                        <label>Inspection Type :</label>
                        <span class="fa-pull-right">
                            <asp:RequiredFieldValidator runat="server" ErrorMessage="Select Inspection Quarter" ID="RequiredFieldValidator6" Display="Dynamic" InitialValue="0" ControlToValidate="ddlquater" ValidationGroup="a" Text="<i class='fa fa-exclamation-circle' title='Select Inspection Quarter !'></i>" ForeColor="Red"></asp:RequiredFieldValidator>
                        </span>
                        <div class="form-group">
                            <asp:DropDownList ID="ddlquater" runat="server" AutoPostBack="true"
                                CssClass="form-control" OnSelectedIndexChanged="ddlquater_SelectedIndexChanged">
                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                                <asp:ListItem Value="1">1st Quarter</asp:ListItem>
                                <asp:ListItem Value="2">2nd Quarter</asp:ListItem>
                                <asp:ListItem Value="3">3rd Quarter</asp:ListItem>
                                <asp:ListItem Value="4">4th Quarter</asp:ListItem>
                                <asp:ListItem Value="5">Half Yearly Inspection</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <label>Allot Month :</label>
                        <span class="fa-pull-right">
                            <asp:RequiredFieldValidator runat="server" ErrorMessage="Select Month" ID="RequiredFieldValidator5" Display="Dynamic" InitialValue="0" ControlToValidate="ddlmonth" ValidationGroup="a" Text="<i class='fa fa-exclamation-circle' title='Select Month !'></i>" ForeColor="Red"></asp:RequiredFieldValidator>
                        </span>
                        <div class="form-group">
                            <asp:DropDownList ID="ddlmonth" runat="server" AutoPostBack="false" CssClass="form-control">
                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <label>Verification Type :</label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                            ErrorMessage="Select Verification Type" ToolTip="Select Verification Type" Text="<i class='fa fa-exclamation-circle' title='Select Verification Type !'></i>"
                            ControlToValidate="ddlverification" InitialValue="0" CssClass="fa fa-pull-right" ForeColor="Red" Display="Dynamic" runat="server">
                        </asp:RequiredFieldValidator>
                        <div class="form-group">
                            <asp:DropDownList ID="ddlverification" runat="server" CssClass="form-control">
                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                                <asp:ListItem Value="1">General Inspection</asp:ListItem>
                                <asp:ListItem Value="2">Physical Verification</asp:ListItem>
                                <asp:ListItem Value="3">Both</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <label>Region :</label>
                        <asp:RequiredFieldValidator ID="rfv1" ValidationGroup="a"
                            ErrorMessage="Select Region" ToolTip="Select Region" Text="<i class='fa fa-exclamation-circle' title='Select Region !'></i>"
                            ControlToValidate="ddlregion" InitialValue="0" CssClass="fa fa-pull-right" ForeColor="Red" Display="Dynamic" runat="server">
                        </asp:RequiredFieldValidator>
                        <div class="form-group">
                            <asp:DropDownList ID="ddlregion" runat="server"
                                CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlregion_SelectedIndexChanged">
                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <label>District :</label>
                        <span class="fa-pull-right">
                            <asp:RequiredFieldValidator runat="server" ErrorMessage="Select District" ID="RequiredFieldValidator12" Display="Dynamic" InitialValue="0" ControlToValidate="ddldistrict" ValidationGroup="a" Text="<i class='fa fa-exclamation-circle' title='Select District !'></i>" ForeColor="Red"></asp:RequiredFieldValidator>
                        </span>
                        <div class="form-group">
                            <asp:DropDownList ID="ddldistrict" runat="server"
                                CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged">
                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <label>Branch:</label>
                        <span class="fa-pull-right">
                            <asp:RequiredFieldValidator runat="server" ErrorMessage="Select Branch" ID="RequiredFieldValidator2" Display="Dynamic" InitialValue="0" ControlToValidate="ddlbranch" ValidationGroup="a" Text="<i class='fa fa-exclamation-circle' title='Select Branch !'></i>" ForeColor="Red"></asp:RequiredFieldValidator>
                        </span>
                        <div class="form-group">
                            <asp:DropDownList ID="ddlbranch" runat="server" AutoPostBack="true" CssClass="form-control">
                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        <label>Financial Year :</label>
                        <span class="fa-pull-right">
                            <asp:RequiredFieldValidator runat="server" ErrorMessage="Select financial year" ID="RequiredFieldValidator3" Display="Dynamic" InitialValue="0" ControlToValidate="ddlfinancialyear" ValidationGroup="a" Text="<i class='fa fa-exclamation-circle' title='Select financial year !'></i>" ForeColor="Red"></asp:RequiredFieldValidator>
                        </span>
                        <div class="form-group">
                            <asp:DropDownList ID="ddlfinancialyear" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <label>Order (Letter No.) :</label>
                        <span class="fa-pull-right">
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" Display="Dynamic" ControlToValidate="txt_OrderNo" ValidationGroup="a" Text="<i class='fa fa-exclamation-circle' title='Enter Order (Letter No.) !'></i>" ErrorMessage="Please Enter Order (Letter No.)" ForeColor="Red"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                ControlToValidate="txt_OrderNo" ValidationGroup="a" Display="Dynamic" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' Enter Order (Letter No.)></i>" ErrorMessage="Please Enter Order (Letter No.)"
                                ValidationExpression="^0*[1-9][0-9]*(\.[0-9]+)?"></asp:RegularExpressionValidator>
                        </span>
                        <div class="form-group">
                            <asp:TextBox ID="txt_OrderNo" runat="server" onkeypress="return isNumber()" AutoComplete="off" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <label>Order Date :</label>
                        <span class="fa-pull-right">
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator7" Display="Dynamic" ControlToValidate="txt_InspDate" ValidationGroup="a" Text="<i class='fa fa-exclamation-circle' title='Enter Order Date !'></i>" ErrorMessage="Please Enter Order Date" ForeColor="Red"></asp:RequiredFieldValidator>
                        </span>
                        <div class="form-group">
                            <asp:TextBox ID="txt_InspDate" runat="server" placeholder="dd/mm/yyyy"
                                CssClass="form-control dateAdd" data-date-end-date="0d" autocomplete="off" data-provide="datepicker"
                                onpaste="return false ;" data-date-autoclose="true" data-date-format="dd/mm/yyyy"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-1" style="margin-top: 25px">
                        <asp:Button CssClass="btn btn-success btn-block" ID="btn_saveInspDate" runat="server" ValidationGroup="a" Text="Submit"
                            OnClick="btn_saveInspDate_Click"></asp:Button>
                    </div>
                    <div class="col-md-1" style="margin-top: 25px">
                        <asp:Button class="btn btn-danger btn-block" ID="btnclear" runat="server" Text="Clear All"></asp:Button>
                    </div>
                </div>
            </fieldset>
        </div>
        <fieldset>
            <legend>Details</legend>
            <div class="table-responsive">
                <asp:GridView runat="server" DataKeyNames="Generate_RosID" ID="generateinspection" HeaderStyle-Font-Size="Medium"
                    CssClass="table table-bordered table-hover datatable" BorderColor="Black" OnRowCommand="generateinspection_RowCommand"
                    AutoGenerateColumns="False">
                    <Columns>
                        <asp:TemplateField HeaderStyle-BackColor="Wheat" HeaderText="S.No" HeaderStyle-Font-Size="Medium" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:HiddenField ID="hdnId" runat="server" Value='<%# Bind("Generate_RosID") %>' />
                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' ToolTip='<%# Eval("Generate_RosID").ToString()%>' runat="server" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-BackColor="Wheat" HeaderText="Inspection Type" HeaderStyle-Font-Size="Medium" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblInspection_type_ID" Text='<%# Eval("Inspection_type_ID") %>' Visible="false"></asp:Label>
                                <asp:Label runat="server" ID="lblInspection_Status" Text='<%# Eval("Inspection_Status") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-BackColor="Wheat" HeaderText="Allot Month" HeaderStyle-Font-Size="Medium" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblID" Text='<%# Eval("Inspection_month_ID") %>' Visible="false"></asp:Label>
                                <asp:Label runat="server" ID="lblMonth_Name" Text='<%# Eval("Month_Name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-BackColor="Wheat" HeaderText="Verification Type" HeaderStyle-Font-Size="Medium" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblVerification_Type" Text='<%# Eval("Verification_Type") %>' Visible="false"></asp:Label>
                                <asp:Label runat="server" ID="lblVerificationType_Stutes" Text='<%# Eval("VerificationType_Stutes") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-BackColor="Wheat" HeaderText="Region Name" HeaderStyle-Font-Size="Medium" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblRegion_ID" Text='<%# Eval("Region_ID") %>' Visible="false"></asp:Label>
                                <asp:Label runat="server" ID="lblRegionnm" Text='<%# Eval("Regionnm") %>'></asp:Label>
                                <asp:HiddenField ID="hdnregion" runat="server" Value='<%# Bind("Region_ID") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-BackColor="Wheat" HeaderText="District Name" HeaderStyle-Font-Size="Medium" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblDistrict_Id" Text='<%# Eval("District_Id") %>' Visible="false"></asp:Label>
                                <asp:Label runat="server" ID="lblDistrict_Name" Text='<%# Eval("District_Name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-BackColor="Wheat" HeaderText="Branch Name" HeaderStyle-Font-Size="Medium" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblBranch_ID" Text='<%# Eval("BranchId") %>' Visible="false"></asp:Label>
                                <asp:Label runat="server" ID="lblDepotName" Text='<%# Eval("DepotName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-BackColor="Wheat" HeaderText="Financial year" HeaderStyle-Font-Size="Medium" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblFinancial_year" Text='<%# Eval("Financial_year") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-BackColor="Wheat" HeaderText="Order (Letter No.)" HeaderStyle-Font-Size="Medium" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblOrder_No" Text='<%# Eval("Order_No") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-BackColor="Wheat" HeaderText="Order Date" HeaderStyle-Font-Size="Medium" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblOrder_Date" Text='<%# Eval("Order_Date") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-BackColor="Wheat" HeaderText="Schedule Inspection" HeaderStyle-Font-Size="Medium" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkBtnEdit" runat="server" Text="Schedule" CssClass="btn btn-info" OnClick="Display"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-BackColor="Wheat" HeaderText="Action" HeaderStyle-Font-Size="Medium" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Button ID="btnRemove" Text="Remove" runat="server" CssClass="btn btn-primary" CommandName="RemoveRow" CommandArgument='<%# Container.DataItemIndex %>' OnClientClick="return confirm('Do you want to Remove this row?');" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>No Record Found</EmptyDataTemplate>
                </asp:GridView>
            </div>
        </fieldset>
    </div>
</asp:Content>

