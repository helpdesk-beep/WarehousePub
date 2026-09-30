<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UploadWarehouse.aspx.cs" Inherits="StatePages_UploadWarehouse" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Warehouse Capacity Upload</title>
    <!-- Bootstrap for a professional administrative look -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        .table-container { max-height: 450px; overflow-y: auto; }
        .card-header { background-color: #0d6efd; color: white; }
    </style>
</head>
<body class="bg-light">
    <form id="form1" runat="server">
        <div class="container py-5">
            <div class="card shadow-sm">
                <div class="card-header">
                    <h4 class="mb-0">Update Warehouse Capacity</h4>
                </div>
               <%-- <div class="card-body">
                    <div class="alert alert-info">
                        <strong>Upload Details:</strong><br />
                      <%--  - <em>गोदाम का नाम</em> will be saved as <strong>Godown ID</strong>.<br />
                        - <em>गोदाम I.D</em> will be saved as <strong>Godown Capacity</strong>.--%>
                   <%-- </div>--%>
                                        <div class="row g-3 align-items-center" style="margin-top:20px">
                        <div class="col-md-6">
                            <asp:FileUpload ID="fuExcel" runat="server" CssClass="form-control" />
                        </div>
                        <div class="col-md-6">
                            <asp:Button ID="btnPreview" runat="server" Text="Preview Excel" CssClass="btn btn-primary" OnClick="btnPreview_Click" />
                            <asp:Button ID="btnUpload" runat="server" Text="Update Table" CssClass="btn btn-success" OnClick="btnUpload_Click" Enabled="false" />
                        </div>
                    </div>

                    <hr />
                    <asp:Label ID="lblMessage" runat="server" CssClass="d-block mb-3 fw-bold"></asp:Label>

                    <div class="table-container border rounded">
                        <asp:GridView ID="gvPreview" runat="server" CssClass="table table-hover table-striped mb-0" AutoGenerateColumns="true">
                            <HeaderStyle CssClass="table-dark" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>