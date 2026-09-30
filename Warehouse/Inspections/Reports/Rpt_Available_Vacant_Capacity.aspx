<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Rpt_Available_Vacant_Capacity.aspx.cs" Inherits="Inspections_Reports_Rpt_Available_Vacant_Capacity" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<html xmlns="http://www.w3.org/1999/xhtml">
<link href="http://mpsc.mp.nic.in/Warehouse/Administration/assets/css/Table-MultiDesigns.css" rel="stylesheet" type="text/css" />
<script src="http://mpsc.mp.nic.in/Warehouse/Administration/assets/jQuery%20Package/jquery-1.10.2/jquery-1.10.2.js" type="text/javascript"></script>
<link href="http://mpsc.mp.nic.in/Warehouse/Administration/assets/Resources/js/Alerts/sweetalert.css" rel="stylesheet" type="text/css" />
<script src="http://mpsc.mp.nic.in/Warehouse/Administration/assets/Resources/js/Alerts/sweetalert-dev.js"></script>
<style type="text/css">
    table {
        opacity: 0.65;
    }

    .title {
        margin-bottom: 1%;
        margin-top: 1%;
    }

    form {
        margin-top: 0;
        border: 2px dashed black;
        padding: 5px;
    }

    body {
        border: 4px solid black;
        padding: 5px;
        min-width: 900px;
        max-width: 1000px;
        margin: 0 auto;
        text-transform: capitalize;
    }

    strong span {
        color: black;
        font-family: Cambria;
    }

    #HeaderText strong {
        border: 1px solid black;
        padding: 2px;
        min-width: 150px;
    }
</style>
<head id="Head1" runat="server">
    <title>Vacant Available Capacity</title>
</head>

<body>
    <form id="form1" runat="server">
        <div class="content-wrapper">
            <div class="row" style="align-content: center" runat="server" id="grdbill" visible="false">
                <div class="col-md-12">
                    <fieldset>
                        <legend>Vacant And Available Capacity</legend>
                        <div class="table-responsive">
                            <div class="row" style="margin-bottom: 10px">
                                <div class="col-md-6">
                                    <asp:Button ID="Button2" runat="server" Text="ExportToPDF" CssClass="button button2" OnClientClick="printGrid()" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                 <input type="button" id="btnExport" value="Exporttoexcel" class="button button2" />
                                </div>
                            </div>
                            <asp:GridView runat="server" ID="GrdBills"
                                CssClass="table table-bordered table-hover datatable" AutoGenerateColumns="False" autopostback="true">
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No" HeaderStyle-BackColor="LightBlue">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Region Name" HeaderStyle-BackColor="LightBlue">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblRegion_Name" Text='<%# Eval("Region") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="District Name" HeaderStyle-BackColor="LightBlue">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblDistrict_Name" Text='<%# Eval("District_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Godown Capacity" HeaderStyle-BackColor="LightBlue">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblGodownCapacity" Text='<%# Eval("GodownCapacity") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Available Quantity" HeaderStyle-BackColor="LightBlue">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblAvlQty" Text='<%# Eval("AvlQty") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Vacant Capacity" HeaderStyle-BackColor="LightBlue">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblVacant_Capacity" Text='<%# Eval("Vacant_Capacity") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>No Record Found</EmptyDataTemplate>
                            </asp:GridView>
                        </div>
                    </fieldset>
                </div>
            </div>
            <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
            <script src="../../JS/table2excel.js"></script>
            <script type="text/javascript">
                $("body").on("click", "#btnExport", function () {
                    $("[id*=GrdBills]").table2excel({
                        filename: "Vacant_Available_Capacity.xls"
                    });
                });
            </script>
        </div>
    </form>
</body>
</html>
