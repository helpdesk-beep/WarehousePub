<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/adminMaster.master" AutoEventWireup="true" CodeFile="Item_Purchase.aspx.cs" Inherits="Admin_New_Item_Purchase" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PM" runat="Server">
    <link href="assets/datatable/css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <link href="assets/datatable/css/buttons.dataTables.min.css" rel="stylesheet" />
    <link href="assets/datatable/css/jquery.dataTables.min.css" rel="stylesheet" />
    <!-- Bootstrap -->
    <script type="text/javascript" src='https://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.8.3.min.js'></script>
    <script type="text/javascript">
        window.history.forward();
        function noBack() { window.history.forward(); }
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
            <legend style="background-color: aliceblue;">Item Purchase</legend>
            <div class="row">
                <div class="col-md-2">
                    <label>Item Name<i style="color: red;">*</i></label>

                    <asp:DropDownList runat="server" ID="ddlitem" CssClass="form-control" OnSelectedIndexChanged="ddlitem_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                        ControlToValidate="ddlitem" InitialValue="0" ErrorMessage="Enter Item Name" ForeColor="#CC3300"
                        ValidationGroup="a"></asp:RequiredFieldValidator>
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
                        <%--<asp:RequiredFieldValidator ToolTip="Enter Bill Number" ErrorMessage="Enter Bill Number" ControlToValidate="txtbillnumber" ID="rfveBillno" CssClass="fa-pull-right" runat="server" ForeColor="Red" ValidationGroup="a" Display="Dynamic" Text="<i class='fa fa-exclamation-circle'></i>"> </asp:RequiredFieldValidator>--%>
                        <asp:RegularExpressionValidator ID="rev16" runat="server"
                            CssClass="fa-pull-right mt-1" Text="<i class='fa fa-exclamation-circle' title='Enter valid Bill Number'></i>" ControlToValidate="txtbillnumber" ValidationGroup="a" Display="Dynamic" ForeColor="Red"
                            ValidationExpression="^[a-zA-Z]{3}?[0-9]{8}$"></asp:RegularExpressionValidator>
                        <asp:TextBox runat="server" ID="txtbillnumber" MaxLength="11" onkeypress="return isNumberKey(this, event);" oninput="validate(this)" CssClass="form-control" AutoComplete="off" placeholder="Enter Bill Number"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                            ControlToValidate="txtbillnumber" ErrorMessage="Enter Bill Number" ForeColor="#CC3300"
                            ValidationGroup="a"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <label>Item Purchase Date<i style="color: red;">*</i></label>
                        <%--<asp:TextBox ID="txtpurchasedate" runat="server" placeholder="dd/mm/yyyy"
                            CssClass="form-control dateAdd" data-date-end-date="0d" autocomplete="off" data-provide="datepicker"
                            onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy"></asp:TextBox>--%>
                        <asp:TextBox runat="server" ID="txtpurchasedate" data-date-end-date="0d" data-provide="datepicker" placeholder="DD/MM/YYYY" autocomplete="off" data-date-format="dd/mm/yyyy" data-date-autoclose="true" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <label>Item Quantity<i style="color: red;">*</i></label>
                        <asp:TextBox runat="server" ID="txtQuantity" onkeyup="multiply()" MaxLength="2" onkeypress="return isNumber()" OnTextChanged="txtQuantity_TextChanged" AutoPostBack="true" CssClass="form-control" AutoComplete="off" placeholder="Enter Quentity"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" Display="Dynamic" ForeColor="Red" ErrorMessage="Enter Item Quentity"
                            ControlToValidate="txtQuantity" CssClass="fa fa-pull-right" ValidationGroup="a" SetFocusOnError="true">
                        </asp:RequiredFieldValidator>--%>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                            ControlToValidate="txtQuantity" ErrorMessage="Enter Item Quentity" ForeColor="#CC3300"
                            ValidationGroup="a"></asp:RequiredFieldValidator>
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
                <div class="col-md-3">
                    <div class="form-group">
                        <label>Upload Invoice</label>
                        <asp:FileUpload ID="FileUpload1" runat="server" />
                        <asp:Label ID="lblfileuploadName" runat="server" Visible='<%# Eval("Doc").ToString() !=""?true:false %>' Text='<%# Eval("Doc").ToString() %>'></asp:Label>
                        <asp:RequiredFieldValidator ID="rvFileUpload1" runat="server"
                            ControlToValidate="FileUpload1" ErrorMessage="Please Insert File in PDF Format" ForeColor="#CC3300"
                            ValidationGroup="a"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="col-md-2" style="margin-top: 25px">
                    <asp:Button runat="server" ID="btnsave" CssClass="btn btn-info btn-block" ValidationGroup="a" Text="Save" OnClick="btnsave_Click" />
                </div>
                <div class="col-md-2" style="margin-top: 25px">
                    <a href="Item_Purchase.aspx" class="btn btn-warning btn-block">Clear</a>
                </div>
            </div>
        </fieldset>
        <div class="row" style="align-content: center">
            <div class="col-md-12">
                <fieldset>
                    <legend style="background-color: aliceblue;">Details</legend>
                    <div class="table-responsive">
                        <asp:GridView runat="server" DataKeyNames="Item_Id" ID="GrdItem" HeaderStyle-Font-Size="Medium"
                            CssClass="table table-bordered table-hover datatable" AutoGenerateColumns="False" OnRowCommand="GrdItem_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No" HeaderStyle-Font-Bold="true" HeaderStyle-BackColor="LightBlue" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' ToolTip='<%# Eval("Item_Id").ToString()%>' runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Name" HeaderStyle-Font-Bold="true" HeaderStyle-BackColor="LightBlue" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblInventoryId" Text='<%# Eval("Inventory_Id") %>' Visible="false"></asp:Label>
                                        <asp:Label runat="server" ID="lblInventoryName" Text='<%# Eval("Inventory_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Rate" HeaderStyle-Font-Bold="true" HeaderStyle-BackColor="LightBlue" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblItemRate" Text='<%# Eval("Item_Rate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Available Quantity" HeaderStyle-Font-Bold="true" HeaderStyle-BackColor="LightBlue" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblAvaiQuantity" Text='<%# Eval("Available_Quantity") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bill Number" HeaderStyle-Font-Bold="true" HeaderStyle-BackColor="LightBlue" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblBillNumber" Text='<%# Eval("Bill_Number") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Purchase Date" HeaderStyle-Font-Bold="true" HeaderStyle-BackColor="LightBlue" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblItemPurchaseDate" Text='<%# Eval("Item_PurchaseDate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Quantity" HeaderStyle-Font-Bold="true" HeaderStyle-BackColor="LightBlue" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblItemQuantity" Text='<%# Eval("Item_Quantity") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total_Amount" HeaderStyle-Font-Bold="true" HeaderStyle-BackColor="LightBlue" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblTotalAmount" Text='<%# Eval("Total_Amount") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Quantity" HeaderStyle-Font-Bold="true" HeaderStyle-BackColor="LightBlue" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblTotalQuantity" Text='<%# Eval("Total_Quantity") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Invoice Image" HeaderStyle-Font-Bold="true" HeaderStyle-BackColor="LightBlue" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink" runat="server" Target="_blank" CssClass="fa fa-eye" Visible='<%# Eval("Doc").ToString() !=""?true:false %>' NavigateUrl='<%# "../Invoice/" + Eval("Doc")%>'></asp:HyperLink>

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action" HeaderStyle-Font-Bold="true" HeaderStyle-BackColor="LightBlue" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEdit" CssClass="btn btn-primary" runat="server" CommandName="EditRecord" CommandArgument='<%# Eval("Item_Id").ToString()%>' Text="Edit"></asp:LinkButton>
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

