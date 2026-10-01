<%@ Page Title="" Language="C#" MasterPageFile="~/Inspections/Masters/Inspection_RO.master" AutoEventWireup="true" CodeFile="~/Inspections/RO/Generate_Online_Roster_For_Inspections.aspx.cs" Inherits="Inspections_RO_Generate_Online_Roster_For_Inspections" %>

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
        <asp:Label ID="lblMsg" runat="server"></asp:Label>
        <div runat="server" id="divallot" visible="false">
            <fieldset>
                <legend style="text-align: center; background-color: aliceblue;">Allot Branch Inspection</legend>
                <div class="row">
                    <div class="col-md-2">
                        <label>Inspection Type :</label>
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
                        <div class="form-group">
                            <asp:DropDownList ID="ddlmonth" runat="server" AutoPostBack="false" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <label>Verification Type :</label>
                        <div class="form-group">
                            <asp:DropDownList ID="ddlverification" runat="server" AutoPostBack="false" CssClass="form-control">
                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                                <asp:ListItem Value="1">General Inspection</asp:ListItem>
                                <asp:ListItem Value="2">Physical Verification</asp:ListItem>
                                <asp:ListItem Value="3">Both</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <label>Region :</label>
                        <div class="form-group">
                            <asp:RequiredFieldValidator ID="rfv1" ValidationGroup="a"
                                ErrorMessage="Select Region" ToolTip="Select Region" Text="<i class='fa fa-exclamation-circle' title='Select Region !'></i>"
                                ControlToValidate="ddlregion" InitialValue="0" CssClass="fa fa-pull-right" ForeColor="Red" Display="Dynamic" runat="server">
                            </asp:RequiredFieldValidator>
                            <asp:DropDownList ID="ddlregion" runat="server"
                                CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlregion_SelectedIndexChanged">
                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <label>District :</label>
                        <div class="form-group">
                            <asp:DropDownList ID="ddldistrict" runat="server"
                                CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <label>Branch:</label>
                        <div class="form-group">
                            <asp:DropDownList ID="ddlbranch" runat="server" AutoPostBack="true"
                                CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        <label>Financial Year :</label>
                        <div class="form-group">
                            <asp:DropDownList ID="ddlfinancialyear" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <label>Order (Letter No.) :</label>
                        <div class="form-group">
                            <asp:TextBox ID="txt_OrderNo" runat="server" AutoComplete="off" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <label>Order Date :</label>
                        <div class="form-group">
                            <asp:TextBox ID="txt_InspDate" runat="server" placeholder="dd/mm/yyyy"
                                CssClass="form-control dateAdd" data-date-end-date="0d" autocomplete="off" data-provide="datepicker"
                                onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-1" style="margin-top: 25px">
                        <asp:Button CssClass="btn btn-success btn-block" ID="btn_saveInspDate" runat="server" Text="Submit"
                            OnClick="btn_saveInspDate_Click"></asp:Button>
                    </div>
                    <div class="col-md-1" style="margin-top: 25px">
                        <asp:Button class="btn btn-danger btn-block" ID="btnclear" runat="server" Text="Clear All"></asp:Button>
                    </div>
                </div>
            </fieldset>
        </div>
        <fieldset>
            <legend style="text-align: center; background-color: aliceblue;">Allot Branch Inspection</legend>
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
                                <asp:LinkButton ID="lnkBtnEdit" runat="server" Text="Schedule" CssClass="btn btn-info" OnClick="Display" CommandArgument='<%# Container.DataItemIndex %>' OnClientClick="return confirm('Do you want to Schedule this ?');"></asp:LinkButton>
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

