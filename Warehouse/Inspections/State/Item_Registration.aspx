<%@ Page Title="" Language="C#" MasterPageFile="~/Inspections/Masters/State.master" AutoEventWireup="true" CodeFile="Item_Registration.aspx.cs" Inherits="Inspections_State_Item_Registration" %>

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
        <fieldset>
            <legend>Item Purchase</legend>
            <div class="row">
                <div class="col-md-2">
                    <label>Item Name<i style="color: red;">*</i></label>
                    <asp:RequiredFieldValidator ID="rfv1" ValidationGroup="a"
                        ErrorMessage="Select Item Name" ToolTip="Enter Item Name" Text="<i class='fa fa-exclamation-circle' title='Enter Item Name !'></i>"
                        ControlToValidate="ddlitem" InitialValue="0" CssClass="fa fa-pull-right" ForeColor="Red" Display="Dynamic" runat="server">
                    </asp:RequiredFieldValidator>
                    <asp:DropDownList runat="server" ID="ddlitem" CssClass="form-control" OnSelectedIndexChanged="ddlitem_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <label>Item Rate</label>
                        <asp:TextBox runat="server" ReadOnly="true" ID="txtprice" onkeyup="multiply()" onkeypress="return isNumberKey(this, event);" oninput="validate(this)" CssClass="form-control" AutoComplete="off" placeholder="Enter Invetory Rate"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <label>Available Quentity</label>
                        <asp:TextBox runat="server" ReadOnly="true" ID="txtAvailable" onkeyup="multiply()" onkeypress="return isNumberKey(this, event);" oninput="validate(this)" CssClass="form-control" AutoComplete="off" placeholder="Enter Available Quentity"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <label>Bill Number<i style="color: red;">*</i></label>
                        <asp:RequiredFieldValidator ToolTip="Enter Bill Number" ErrorMessage="Enter Bill Number" ControlToValidate="txtbillnumber" ID="rfveBillno" CssClass="fa-pull-right" runat="server" ForeColor="Red" ValidationGroup="a" Display="Dynamic" Text="<i class='fa fa-exclamation-circle'></i>"> </asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="rev16" runat="server"
                            CssClass="fa-pull-right mt-1" Text="<i class='fa fa-exclamation-circle' title='Enter valid Bill Number'></i>" ControlToValidate="txtbillnumber" ValidationGroup="a" Display="Dynamic" ForeColor="Red"
                            ValidationExpression="^[a-zA-Z]{3}?[0-9]{8}$"></asp:RegularExpressionValidator>
                        <asp:TextBox runat="server" ID="txtbillnumber" MaxLength="11" onkeypress="return isNumberKey(this, event);" oninput="validate(this)" CssClass="form-control" AutoComplete="off" placeholder="Enter Bill Number"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <label>Item Purchase Date<i style="color: red;">*</i></label>
                        <asp:TextBox ID="txtpurchasedate" runat="server" placeholder="dd/mm/yyyy"
                            CssClass="form-control dateAdd" data-date-end-date="0d" autocomplete="off" data-provide="datepicker"
                            onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <label>Item Quantity<i style="color: red;">*</i></label>
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" Display="Dynamic" ForeColor="Red" ErrorMessage="Enter Item Quentity"
                            ControlToValidate="txtQuantity" CssClass="fa fa-pull-right" ValidationGroup="a" ToolTip="Enter Item Quentity" SetFocusOnError="true" Text="<i class='fa fa-exclamation-circle' title=' Enter Item Quentity!'></i>">
                        </asp:RequiredFieldValidator>
                        <asp:TextBox runat="server" ID="txtQuantity" onkeyup="multiply()" MaxLength="2" onkeypress="return isNumber()" OnTextChanged="txtQuantity_TextChanged" AutoPostBack="true" CssClass="form-control" AutoComplete="off" placeholder="Enter Quentity"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <div class="form-group">
                        <label>Total Amount</label>
                        <asp:TextBox runat="server" ReadOnly="true" ID="txtAmount" CssClass="form-control" placeholder="Enter Total Amount"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <label>Total Quentity</label>
                        <asp:TextBox runat="server" ReadOnly="true" ID="txttotalquentity" CssClass="form-control" placeholder="Enter Total Amount"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-1" style="margin-top: 25px">
                    <asp:Button runat="server" ID="btnsave" CssClass="btn btn-info btn-block" ValidationGroup="a" Text="Save" OnClick="btnsave_Click" />
                </div>
                <div class="col-md-1" style="margin-top: 25px">
                    <a href="Item_Registration.aspx" class="btn btn-warning btn-block">Clear</a>
                </div>
            </div>
        </fieldset>
        <div class="row" style="align-content: center">
            <div class="col-md-12">
                <fieldset>
                    <legend>Details</legend>
                    <div class="table-responsive">
                        <asp:GridView runat="server" DataKeyNames="Item_Id" ID="GrdItem" HeaderStyle-Font-Size="Medium"
                            CssClass="table table-bordered table-hover datatable" AutoGenerateColumns="False" OnRowCommand="GrdItem_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No" HeaderStyle-Font-Bold="true" HeaderStyle-BackColor="LightBlue" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowNumber" Font-Bold="true" Text='<%# Container.DataItemIndex + 1 %>' ToolTip='<%# Eval("Item_Id").ToString()%>' runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Name" HeaderStyle-Font-Bold="true" HeaderStyle-BackColor="LightBlue" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label Font-Bold="true" runat="server" ID="lblInventoryId" Text='<%# Eval("Inventory_Id") %>' Visible="false"></asp:Label>
                                        <asp:Label runat="server" Font-Bold="true" ID="lblInventoryName" Text='<%# Eval("Inventory_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Rate" HeaderStyle-Font-Bold="true" HeaderStyle-BackColor="LightBlue" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Font-Bold="true" ID="lblItemRate" Text='<%# Eval("Item_Rate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Available Quantity" HeaderStyle-Font-Bold="true" HeaderStyle-BackColor="LightBlue" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Font-Bold="true" ID="lblAvaiQuantity" Text='<%# Eval("Available_Quantity") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bill Number" HeaderStyle-Font-Bold="true" HeaderStyle-BackColor="LightBlue" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Font-Bold="true" ID="lblBillNumber" Text='<%# Eval("Bill_Number") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Purchase Date" HeaderStyle-Font-Bold="true" HeaderStyle-BackColor="LightBlue" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Font-Bold="true" ID="lblItemPurchaseDate" Text='<%# Eval("Item_PurchaseDate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Quantity" HeaderStyle-Font-Bold="true" HeaderStyle-BackColor="LightBlue" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Font-Bold="true" ID="lblItemQuantity" Text='<%# Eval("Item_Quantity") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total_Amount" HeaderStyle-Font-Bold="true" HeaderStyle-BackColor="LightBlue" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Font-Bold="true" ID="lblTotalAmount" Text='<%# Eval("Total_Amount") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Quantity" HeaderStyle-Font-Bold="true" HeaderStyle-BackColor="LightBlue" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Font-Bold="true" ID="lblTotalQuantity" Text='<%# Eval("Total_Quantity") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action" HeaderStyle-Font-Bold="true" HeaderStyle-BackColor="LightBlue" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEdit" Font-Bold="true" runat="server" CommandName="EditRecord" CommandArgument='<%# Eval("Item_Id").ToString()%>' Text="Edit"></asp:LinkButton>
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

