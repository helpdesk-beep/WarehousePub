<%@ Page Title="" Language="C#" MasterPageFile="~/Inspections/Masters/State.master" AutoEventWireup="true" CodeFile="~/Inspections/State/Add_Item.aspx.cs" Inherits="Inspections_State_Add_Item" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../../Inventory/assets/plugins/select2/css/select2-bootstrap4.css" rel="stylesheet" />
    <script src="../../assets/plugins/select2/js/select2.min.js"></script>
    <script src="../../Inventory/assets/plugins/select2/js/select2.min.js"></script>
    <script type="text/javascript">
        window.history.forward();

        function noBack() { window.history.forward(); }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <asp:Label ID="lblMsg" runat="server"></asp:Label>
        <fieldset>
            <legend>Add Item</legend>
            <div class="row">
                <div class="col-md-2">
                    <label>Item Name :</label>
                    <div class="form-group">
                        <asp:TextBox runat="server" ID="txtItemName" CssClass="form-control" AutoComplete="off" placeholder="Enter Item Name"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-2">
                    <label>Item Price :</label>
                    <div class="form-group">
                        <asp:TextBox runat="server" ID="txtPrice" CssClass="form-control" AutoComplete="off" placeholder="Enter Item Price"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-2">
                    <label>Available Quantity :</label>
                    <div class="form-group">
                        <asp:TextBox runat="server" ID="txtavailablequan" CssClass="form-control" AutoComplete="off" placeholder="Enter Item Quantity"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-1" style="margin-top: 25px">
                    <asp:Button runat="server" ID="btnsave" CssClass="btn btn-info btn-block" ValidationGroup="a" Text="Save" OnClick="btnsave_Click" />
                </div>
                <div class="col-md-1" style="margin-top: 25px">
                    <a href="Add_Item.aspx" class="btn btn-warning btn-block">Clear</a>
                </div>
            </div>
        </fieldset>
        <div class="row" style="align-content: center">
            <div class="col-md-12">
                <fieldset>
                    <legend>Details</legend>
                    <div class="table-responsive">
                        <asp:GridView runat="server" DataKeyNames="Inventory_Id" ID="Grdinventory" HeaderStyle-Font-Size="Large"
                            CssClass="table table-bordered table-hover datatable" AutoGenerateColumns="False" OnRowCommand="Grdinventory_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No" HeaderStyle-Font-Bold="true" HeaderStyle-BackColor="LightBlue" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' ToolTip='<%# Eval("Inventory_Id").ToString()%>' runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Inventory Name" HeaderStyle-Font-Bold="true" HeaderStyle-BackColor="LightBlue" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblInventoryName" Text='<%# Eval("Inventory_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Inventory Rate" HeaderStyle-Font-Bold="true" HeaderStyle-BackColor="LightBlue" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblInventoryRate" Text='<%# Eval("Inventory_Rate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Available Quentity" HeaderStyle-Font-Bold="true" HeaderStyle-BackColor="LightBlue" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblQuentity" Text='<%# Eval("Inventory_Quentity") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action" HeaderStyle-Font-Bold="true" HeaderStyle-BackColor="LightBlue" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEdit" runat="server" CommandName="EditRecord" CommandArgument='<%# Eval("Inventory_Id").ToString()%>' Text="Edit"></asp:LinkButton>
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

