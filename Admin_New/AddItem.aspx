<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/adminMaster.master" AutoEventWireup="true" CodeFile="~/Admin_New/AddItem.aspx.cs" Inherits="Admin_New_AddItem" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PM" runat="Server">
    <link href="../../Inventory/assets/plugins/select2/css/select2-bootstrap4.css" rel="stylesheet" />
    <script src="../../assets/plugins/select2/js/select2.min.js"></script>
    <script src="../../Inventory/assets/plugins/select2/js/select2.min.js"></script>
    <script type="text/javascript">
        window.history.forward();
        function noBack() { window.history.forward(); }
    </script>
    <style>
        .row {
            margin-left: 0px !important;
            margin-right: 0px !important;
        }
    </style>

    <div class="wrapper">
        <!-- Excel Upload Section -->
        <fieldset class="mb-3">
            <legend style="background-color: aliceblue;">Bulk Upload Items via Excel</legend>
            <div class="row">
                <div class="col-md-5">
                    <label>Select Excel File (.xlsx) :</label>
                    <div class="form-group">
                        <asp:FileUpload ID="fileUploadExcel" runat="server" CssClass="form-control" />
                    </div>
                </div>
                <div class="col-md-3" style="margin-top: 25px;">
                    <asp:Button ID="btnUploadExcel" runat="server" Text="Upload Excel" CssClass="btn btn-success btn-block" OnClick="btnUploadExcel_Click" />
                </div>
            </div>
        </fieldset>

        <!-- Manual Add Item Section -->
        <fieldset>
            <legend style="background-color: aliceblue;">Add Item </legend>
            <div class="row">
                <div class="col-md-3">
                    <label>Item Name :</label>
                    <div class="form-group">
                        <asp:TextBox runat="server" ID="txtItemName" CssClass="form-control" AutoComplete="off" placeholder="Enter Item Name"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-3">
                    <label>Item Price / Per Item :</label>
                    <div class="form-group">
                        <asp:TextBox runat="server" ID="txtPrice" CssClass="form-control" AutoComplete="off" placeholder="Enter Item Price"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-3">
                    <label>Available Quantity :</label>
                    <div class="form-group">
                        <asp:TextBox runat="server" ID="txtavailablequan" CssClass="form-control" AutoComplete="off" placeholder="Enter Item Quantity"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-3">
                    <label>Year :</label>
                    <div class="form-group">
                        <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control" Style="width: 100% !important;">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-4"></div>
                <div class="col-md-2" style="margin-top: 15px">
                    <asp:Button runat="server" ID="btnsave" CssClass="btn btn-info btn-block" ValidationGroup="a" Text="Save" OnClick="btnsave_Click" />
                </div>
                <div class="col-md-2" style="margin-top: 15px">
                    <a href="AddItem.aspx" class="btn btn-warning btn-block">Clear</a>
                </div>
            </div>
        </fieldset>

        <!-- Details GridView Section -->
        <fieldset>
            <legend style="background-color: aliceblue;">Details</legend>
            <div class="row">
                <div class="col-md-12">
                    <div class="table-responsive">
                        <asp:GridView runat="server" DataKeyNames="Inventory_Id" ID="Grdinventory"
                            CssClass="table table-bordered table-hover datatable" AutoGenerateColumns="False" OnRowCommand="Grdinventory_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' ToolTip='<%# Eval("Inventory_Id").ToString()%>' runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Inventory Name" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblInventoryName" Text='<%# Eval("Inventory_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Inventory Rate" HeaderStyle-Font-Bold="true" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblInventoryRate" Text='<%# Eval("Inventory_Rate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Available Quantity" HeaderStyle-Font-Bold="true" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblQuentity" Text='<%# Eval("Inventory_Quentity") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Year" HeaderStyle-Font-Bold="true" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblYear" Text='<%# Eval("Year") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action" HeaderStyle-Font-Bold="true" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEdit" CssClass="btn btn-primary" runat="server" CommandName="EditRecord" CommandArgument='<%# Eval("Inventory_Id").ToString()%>' Text="Edit"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </fieldset>
    </div>
</asp:Content>