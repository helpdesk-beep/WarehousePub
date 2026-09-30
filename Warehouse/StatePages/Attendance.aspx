<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Attendance.aspx.cs" Inherits="Attendance" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Employees Attendance</title>
    <style>
        * {
            box-sizing: border-box;
            margin: 0;
            padding: 0;
        }

        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background-color: #f4f7f6;
            color: #333;
        }

        .container {
            width: 95%;
            margin: 30px auto;
            background-color: #fff;
            padding: 30px;
            border-radius: 10px;
            box-shadow: 0 6px 15px rgba(0,0,0,0.1);
        }

        .page-header {
            text-align: center;
            margin-bottom: 30px;
        }

            .page-header h2 {
                color: #1a237e;
                font-size: 2rem;
                margin-bottom: 5px;
            }

            .page-header h3 {
                color: #1a237e;
                font-weight: 600;
                font-size: 1.5rem;
                
            }

        .filter-section {
            display: flex;
            flex-wrap: wrap;
            align-items: center;
            gap: 15px;
            padding-bottom: 20px;
            border-bottom: 1px solid #eee;
            margin-bottom: 25px;
        }

            .filter-section label {
                font-weight: 600;
            }

            .filter-section select {
                padding: 8px 12px;
                border-radius: 6px;
                border: 1px solid #ccc;
                background-color: #f9f9f9;
                font-size: 1rem;
                transition: 0.3s;
            }

                .filter-section select:focus {
                    border-color: #4db6ac;
                    box-shadow: 0 0 5px rgba(77,182,172,0.5);
                    outline: none;
                }

            .filter-section .btn {
                padding: 8px 18px;
                border-radius: 6px;
                font-weight: 600;
                cursor: pointer;
                border: 1px solid transparent;
                transition: 0.3s;
            }

        .btn-print {
            background-color: #007bff;
            color: #fff;
        }

            .btn-print:hover {
                background-color: #0056b3;
            }

        .btn-export {
            background-color: #28a745;
            color: #fff;
        }

            .btn-export:hover {
                background-color: #1e7e34;
            }

        .table-responsive {
            overflow-x: auto;
        }

        .attendance-table {
            width: 100%;
            border-collapse: collapse;
            table-layout: fixed;
            font-size: 0.95rem;
            margin-bottom: 30px;
            word-wrap: break-word;
            border:1px solid;
        }

            .attendance-table th, .attendance-table td {
                border: 1px solid #000;
                padding: 12px;
                text-align: center;
                font-weight: 700;
                vertical-align: middle;
            }

            .attendance-table th {
                background-color: #e0f2f1;
                color: #1a237e;
                font-weight: 600;
                border-color: Black;
                text-transform: uppercase;
            }

            .attendance-table tr:nth-child(even) {
                background-color: #f9f9f9;
            }

            .attendance-table input[type="text"] {
                width: 100%;
                padding: 6px 8px;
                box-sizing: border-box;
                border: 1px solid #ccc;
                border-radius: 4px;
                font-size: 0.9rem;
                text-align: center;
                transition: 0.3s;
            }

                .attendance-table input[type="text"]:focus {
                    border-color: #4db6ac;
                    box-shadow: 0 0 4px rgba(77,182,172,0.4);
                    outline: none;
                }

            .attendance-table .btn-edit, .attendance-table .btn-update, .attendance-table .btn-cancel {
                padding: 5px 10px;
                border-radius: 4px;
                font-weight: 600;
                cursor: pointer;
                border: none;
                transition: 0.3s;
                color: white;
            }

            .attendance-table .btn-edit {
                background-color: #007bff;
            }

                .attendance-table .btn-edit:hover {
                    background-color: #0056b3;
                }

            .attendance-table .btn-update {
                background-color: #28a745;
            }

                .attendance-table .btn-update:hover {
                    background-color: #1e7e34;
                }

            .attendance-table .btn-cancel {
                background-color: #dc3545;
            }

                .attendance-table .btn-cancel:hover {
                    background-color: #b52a37;
                }

        .signature-area {
            text-align: right;
            margin-top: 40px;
        }

            .signature-area p {
                font-weight: 600;
            }

        @media print {
            body {
                background-color: #fff;
            }

            .container {
                box-shadow: none;
                border-radius: 0;
                margin: 0;
                width: 100%;
                padding: 0;
            }

            .filter-section, .no-print {
                display: none !important;
            }

            .attendance-table th, .attendance-table td {
                font-size: 8pt;
                padding: 8px;
            }
        }

        @media (max-width:768px) {
            .attendance-table th, .attendance-table td {
                font-size: 0.85rem;
                padding: 8px;
            }
        }
    </style>
    <style>
        @media print {
            /* Hide Action column */
            .no-print,
            .no-print *,
            .action-col,
            .action-col * {
                display: none !important;
            }

            /* Force table header background color */
            th, td {
                -webkit-print-color-adjust: exact;
                print-color-adjust: exact;
            }
        }
    </style>



    <script type="text/javascript">
        function PrintGrid() { window.print(); }
    </script>
</head>

<body>
    <form id="form1" runat="server">
        <div class="container">

            <div class="page-header">
                <h2>Employees Attendance</h2>
                <h3 id="H3Title" runat="server"></h3>
            </div>

            <div class="filter-section">
                <asp:Label ID="Label1" runat="server" Text="Select Year:"></asp:Label>
                <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged" Width="180px"></asp:DropDownList>

                <asp:Label ID="Label2" runat="server" Text="Select Month:"></asp:Label>
                <asp:DropDownList ID="ddlMonth" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged" Width="180px"></asp:DropDownList>

                <asp:Button Text="Show IT Slip" ID="btnITSalarySlip" runat="server" CssClass="btn btn-export" OnClick="btnITSalarySlip_Click" />
                <asp:Button Text="Show NIC Slip" ID="btnNICSalarySlip" runat="server" CssClass="btn btn-export" OnClick="btnNICSalarySlip_Click" />
                <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="btn btn-print" OnClientClick="PrintGrid(); return false;" />
            </div>

            <div class="table-responsive">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" CssClass="attendance-table table-bordered" DataKeyNames="SNo" GridLines="None"
                    OnRowEditing="GridView1_RowEditing" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowUpdating="GridView1_RowUpdating"
                    OnRowCreated="GridView1_RowCreated">
                    <Columns>
                        <asp:BoundField DataField="SNo" HeaderText="S.No." ReadOnly="true" />
                        <asp:BoundField DataField="Name" HeaderText="Name" ReadOnly="true" />
                        <asp:BoundField DataField="PlaceOfPosting" HeaderText="Place of Posting" ReadOnly="true" />
                        <asp:BoundField DataField="Designation" HeaderText="Designation" ReadOnly="true" />
                        <asp:BoundField DataField="WorkingPeriodFrom" HeaderText="From" ReadOnly="true" />
                        <asp:BoundField DataField="WorkingPeriodTo" HeaderText="To" ReadOnly="true" />
                        <asp:TemplateField HeaderText="Absent">
                            <ItemTemplate>
                                <asp:Label ID="lblAbsent" runat="server" Text='<%# Eval("Absent") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtAbsent" runat="server" Text='<%# Bind("Absent") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowEditButton="True" HeaderText="Action" ItemStyle-CssClass="no-print" HeaderStyle-CssClass="no-print" />
                    </Columns>
                </asp:GridView>
            </div>

            <div class="table-responsive">
                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="false" CssClass="attendance-table" DataKeyNames="S_No" GridLines="None"
                    OnRowEditing="GridView2_RowEditing" OnRowCancelingEdit="GridView2_RowCancelingEdit" OnRowUpdating="GridView2_RowUpdating"
                    OnRowCreated="GridView2_RowCreated">
                    <Columns>
                        <asp:BoundField DataField="S_No" HeaderText="S.No." ReadOnly="true" />
                        <asp:BoundField DataField="Name" HeaderText="Name" ReadOnly="true" />
                        <asp:BoundField DataField="PlaceOfPosting" HeaderText="Place of Posting" ReadOnly="true" />
                        <asp:BoundField DataField="Designation" HeaderText="Designation" ReadOnly="true" />
                        <asp:BoundField DataField="WorkingPeriodFrom" HeaderText="From" ReadOnly="true" />
                        <asp:BoundField DataField="WorkingPeriodTo" HeaderText="To" ReadOnly="true" />
                        <asp:TemplateField HeaderText="Absent">
                            <ItemTemplate>
                                <asp:Label ID="lblAbsent" runat="server" Text='<%# Eval("Absent") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtAbsent" runat="server" Text='<%# Bind("Absent") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowEditButton="True" HeaderText="Action" ItemStyle-CssClass="no-print" HeaderStyle-CssClass="no-print" />
                    </Columns>
                </asp:GridView>
            </div>
            <br />
            <br />
            <br />
            <br />

            <div class="signature-area">
                <p>Signature of Reporting Officer</p>
            </div>

        </div>
    </form>
</body>
</html>