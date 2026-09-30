<%@ Page Title="Warehouse Dashboard" Language="C#" MasterPageFile="~/WareHouseMaster.master" AutoEventWireup="true" CodeFile="~/Reports/NewStorageCapacityReport/DashboardNew.aspx.cs" Inherits="Reports_NewStorageCapacityReport_DashboardNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPageHead" runat="Server">
    <style type="text/css">
        /* Dashboard Styles - MPWLC Theme */
        .dashboard-container {
            padding: 20px;
            background: linear-gradient(135deg, #f8f9fa 0%, #e9ecef 100%);
            min-height: 100vh;
        }

        .dashboard-header {
            display: flex;
            justify-content: space-between;
            align-items: center;
            margin-bottom: 25px;
            padding-bottom: 15px;
            border-bottom: 3px solid #2E5B96;
            background: white;
            padding: 20px;
            border-radius: 10px;
            box-shadow: 0 2px 10px rgba(46, 91, 150, 0.1);
        }

        .dashboard-title {
            font-size: 28px;
            font-weight: 700;
            color: #2E5B96;
            text-transform: uppercase;
            letter-spacing: 0.5px;
            display: flex;
            align-items: center;
            gap: 10px;
        }

            .dashboard-title:before {
                content: "";
                display: block;
                width: 4px;
                height: 30px;
                background: #FF6B35;
                border-radius: 2px;
            }

        .current-date-box {
            background: linear-gradient(135deg, #2E5B96 0%, #1E3A6B 100%);
            color: white;
            padding: 12px 25px;
            border-radius: 8px;
            font-size: 16px;
            font-weight: 600;
            box-shadow: 0 4px 12px rgba(46, 91, 150, 0.3);
            border: 2px solid #FF6B35;
        }

        .dashboard-grid {
            display: grid;
            grid-template-columns: repeat(5, 1fr);
            gap: 25px;
            margin-bottom: 30px;
            text-decoration: none;
        }

        .dashboard-card {
            background: white;
            border-radius: 12px;
            padding: 25px;
            box-shadow: 0 6px 15px rgba(46, 91, 150, 0.08);
            border-top: 4px solid #2E5B96;
            transition: all 0.3s ease;
            position: relative;
            overflow: hidden;
            text-decoration: none !important;
        }

            .dashboard-card:before {
                content: "";
                position: absolute;
                top: 0;
                left: 0;
                width: 100%;
                height: 4px;
                background: linear-gradient(90deg, #2E5B96 0%, #FF6B35 100%);
            }

            .dashboard-card:hover {
                transform: translateY(-8px);
                box-shadow: 0 12px 25px rgba(46, 91, 150, 0.15);
                text-decoration: none !important;
            }

        a.dashboard-card {
            text-decoration: none;
            color: inherit;
        }

            a.dashboard-card:hover {
                text-decoration: none;
                color: inherit;
            }

        .dashboard-grid a {
            text-decoration: none;
        }

            .dashboard-grid a:hover {
                text-decoration: none;
            }

        .card-title {
            font-size: 15px;
            color: #5A6C7D;
            margin-bottom: 12px;
            font-weight: 600;
            text-transform: uppercase;
            letter-spacing: 0.5px;
        }

        .card-value {
            font-size: 36px;
            font-weight: 800;
            color: #2E5B96;
            margin-bottom: 5px;
            text-shadow: 1px 1px 2px rgba(0, 0, 0, 0.1);
        }

        .card-subtitle {
            font-size: 13px;
            color: #8A9BA8;
            font-weight: 500;
        }

        .dashboard-section {
            background: white;
            border-radius: 12px;
            padding: 30px;
            margin-bottom: 30px;
            box-shadow: 0 6px 15px rgba(46, 91, 150, 0.08);
            border: 1px solid #E3E8F0;
        }

        .section-title {
            font-size: 22px;
            font-weight: 700;
            color: #2E5B96;
            margin-bottom: 25px;
            padding-bottom: 15px;
            border-bottom: 2px solid #E3E8F0;
            position: relative;
        }

            .section-title:after {
                content: "";
                position: absolute;
                bottom: -2px;
                left: 0;
                width: 80px;
                height: 2px;
                background: #FF6B35;
            }

        .section-grid {
            display: grid;
            grid-template-columns: repeat(3, 1fr);
            gap: 25px;
        }

        .data-table {
            width: 100%;
            border-collapse: separate;
            border-spacing: 0;
            margin-top: 15px;
            border-radius: 8px;
            overflow: hidden;
            box-shadow: 0 2px 8px rgba(46, 91, 150, 0.1);
        }

            .data-table th {
                background: linear-gradient(135deg, #2E5B96 0%, #1E3A6B 100%);
                padding: 16px 20px;
                text-align: left;
                font-weight: 700;
                color: white;
                text-transform: uppercase;
                font-size: 13px;
                letter-spacing: 0.5px;
                border: none;
            }

            .data-table td {
                padding: 14px 11px;
                color: #4A5C6B;
                font-weight: 500;
                border-bottom: 1px solid #F0F4F8;
                background: white;
            }

            .data-table tr:nth-child(even) td {
                background: #F8FAFC;
            }

            .data-table tr:hover td {
                background: #F0F7FF;
            }

            .data-table tr:last-child td {
                border-bottom: none;
            }

        .highlight-box {
            background: linear-gradient(135deg, #2E5B96 0%, #1E3A6B 100%);
            color: white;
            padding: 25px;
            border-radius: 10px;
            margin-top: 10px;
            box-shadow: 0 6px 15px rgba(46, 91, 150, 0.2);
            border: 2px solid #FF6B35;
            position: relative;
            overflow: hidden;
        }

            .highlight-box:before {
                content: "";
                position: absolute;
                top: -50%;
                right: -50%;
                width: 200px;
                height: 200px;
                background: rgba(255, 107, 53, 0.1);
                border-radius: 50%;
            }

        .highlight-value {
            font-size: 32px;
            font-weight: 800;
            margin: 12px 0;
            color: white;
            position: relative;
            z-index: 1;
        }

        .status-badge {
            display: inline-block;
            padding: 8px 16px;
            border-radius: 20px;
            font-size: 12px;
            font-weight: 700;
            text-transform: uppercase;
            letter-spacing: 0.5px;
        }

        .status-complete {
            background: linear-gradient(135deg, #10B981 0%, #059669 100%);
            color: white;
            box-shadow: 0 2px 5px rgba(16, 185, 129, 0.3);
        }

        .status-pending {
            background: linear-gradient(135deg, #F59E0B 0%, #D97706 100%);
            color: white;
            box-shadow: 0 2px 5px rgba(245, 158, 11, 0.3);
        }

        .branch-type-container {
            display: flex;
            flex-wrap: wrap;
            gap: 15px;
            margin-top: 15px;
        }

        .branch-type-item {
            background: linear-gradient(135deg, #F8FAFC 0%, #F0F4F8 100%);
            padding: 15px 20px;
            border-radius: 10px;
            border-left: 5px solid #2E5B96;
            box-shadow: 0 3px 8px rgba(46, 91, 150, 0.08);
            flex: 1;
            min-width: 120px;
            transition: transform 0.3s ease;
        }

            .branch-type-item:hover {
                transform: translateY(-3px);
                box-shadow: 0 5px 15px rgba(46, 91, 150, 0.15);
            }

        .branch-type-label {
            font-size: 13px;
            color: #6B7C8D;
            font-weight: 600;
            text-transform: uppercase;
            margin-bottom: 5px;
            letter-spacing: 0.5px;
        }

        .branch-type-value {
            font-size: 22px;
            font-weight: 800;
            color: #2E5B96;
        }

        /* MPWLC Specific Colors */
        .mpwlc-primary {
            color: #2E5B96;
        }

        .mpwlc-secondary {
            color: #FF6B35;
        }

        .mpwlc-bg-primary {
            background-color: #2E5B96;
        }

        .mpwlc-bg-secondary {
            background-color: #FF6B35;
        }

        .mpwlc-gradient {
            background: linear-gradient(135deg, #2E5B96 0%, #FF6B35 100%);
        }

        /* Card Variations */
        .card-total {
            border-top-color: #2E5B96;
        }

        .card-receiving {
            border-top-color: #10B981;
        }

        .card-dispatch {
            border-top-color: #EF4444;
        }

        .card-payment {
            border-top-color: #8B5CF6;
        }

        /* Animations */
        @keyframes fadeIn {
            from {
                opacity: 0;
                transform: translateY(20px);
            }

            to {
                opacity: 1;
                transform: translateY(0);
            }
        }

        .dashboard-card {
            animation: fadeIn 0.5s ease forwards;
            text-decoration: none;
        }

            .dashboard-card:nth-child(2) {
                animation-delay: 0.1s;
            }

            .dashboard-card:nth-child(3) {
                animation-delay: 0.2s;
            }

            .dashboard-card:nth-child(4) {
                animation-delay: 0.3s;
            }

        /* Enhanced Table Styles */
        .table-container {
            overflow-x: auto;
            border-radius: 10px;
            box-shadow: 0 4px 12px rgba(46, 91, 150, 0.08);
        }

        /* Button Styles */
        .dashboard-btn {
            background: linear-gradient(135deg, #2E5B96 0%, #1E3A6B 100%);
            color: white;
            border: none;
            padding: 12px 24px;
            border-radius: 6px;
            font-weight: 600;
            cursor: pointer;
            transition: all 0.3s ease;
            box-shadow: 0 4px 12px rgba(46, 91, 150, 0.2);
        }

            .dashboard-btn:hover {
                transform: translateY(-2px);
                box-shadow: 0 6px 15px rgba(46, 91, 150, 0.3);
            }

        /* Responsive Design */
        @media (max-width: 1200px) {
            .dashboard-grid {
                grid-template-columns: repeat(2, 1fr);
                gap: 20px;
            }

            .section-grid {
                grid-template-columns: repeat(2, 1fr);
                gap: 20px;
            }
        }

        @media (max-width: 768px) {
            .dashboard-grid {
                grid-template-columns: 1fr;
                gap: 15px;
            }

            .section-grid {
                grid-template-columns: 1fr;
                gap: 20px;
            }

            .dashboard-header {
                flex-direction: column;
                align-items: flex-start;
                gap: 15px;
                padding: 15px;
            }

            .dashboard-title {
                font-size: 22px;
            }

            .card-value {
                font-size: 28px;
            }

            .highlight-value {
                font-size: 26px;
            }

            .branch-type-item {
                min-width: calc(50% - 10px);
            }
        }

        @media (max-width: 480px) {
            .dashboard-container {
                padding: 10px;
            }

            .dashboard-section {
                padding: 20px 15px;
            }

            .branch-type-item {
                min-width: 100%;
            }

            .data-table th,
            .data-table td {
                padding: 12px 15px;
                font-size: 13px;
            }
        }

        /* Loading Animation */
        .loading-shimmer {
            background: linear-gradient(90deg, #f0f0f0 25%, #e0e0e0 50%, #f0f0f0 75%);
            background-size: 200% 100%;
            animation: shimmer 1.5s infinite;
            border-radius: 4px;
        }

        @keyframes shimmer {
            0% {
                background-position: -200% 0;
            }

            100% {
                background-position: 200% 0;
            }
        }
    </style>
    <%--New CSS--%>
    <style>
        .table-container {
            max-height: 280px;
            overflow-y: auto;
            /*border: 1px solid #ccc;*/
        }

        .data-table {
            width: 100%;
            border-collapse: collapse;
        }



            .data-table th, .data-table td {
                padding: 8px;
                border: 1px solid #ddd;
                text-align: left;
            }

        .type-link {
            text-decoration: none;
            color: #0d47a1;
            font-weight: 500;
        }

            .type-link:hover {
                color: #e65100;
                text-decoration: none;
            }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPageBody" runat="Server">
    <div class="dashboard-container">
        <!-- Dashboard Header -->
        <div class="dashboard-header">
            <div class="dashboard-title">Warehouse Management Dashboard</div>
            <div class="current-date-box">
                <span id="currentDate"></span>
            </div>
        </div>

        <!-- Top Statistics Cards -->
        <div class="dashboard-grid">
            <a class="dashboard-card show-loader" href="../DashboardPages/AllBranch.aspx">
                <div class="card-title">Total Branches</div>
                <div class="card-value" id="totalBranches" runat="server"></div>
                <div class="card-subtitle">Across all regions</div>
            </a>

            <a class="dashboard-card show-loader" href="../DashboardPages/AllGodowns.aspx">
                <div class="card-title">Total Godowns</div>
                <div class="card-value" id="totalGodowns" runat="server"></div>
                <div class="card-subtitle">Storage facilities</div>
            </a>

            <a class="dashboard-card show-loader" href="AllAvailableStock.aspx">
                <div class="card-title">Available Stock</div>
                <div class="card-value" id="totalStock">12,450 MT</div>
                <div class="card-subtitle">Current inventory</div>
            </a>

            <a class="dashboard-card show-loader" href="AllBags.aspx">
                <div class="card-title">Total Bags</div>
                <div class="card-value" id="totalBags">248,500</div>
                <div class="card-subtitle">In storage</div>
            </a>

            <a class="dashboard-card show-loader" href="AllWight.aspx">
                <div class="card-title">Total Wight</div>
                <div class="card-value" id="totalWight">50,000</div>
                <div class="card-subtitle">In M.T</div>
            </a>
        </div>

        <!-- Branch and Godown Details Section -->
        <div class="dashboard-section">
            <div class="section-title">Branch & Godown Details</div>

            <div class="section-grid">
                <!-- Branch Types -->
                <div>
                    
                    <h3 style="color: #1a237e; margin-bottom: 15px;">Branch Types</h3>

                    <div class="branch-type-container">
                        <asp:Repeater ID="rptBranchType" runat="server">
                            <ItemTemplate>
                                <a href='../DashboardPages/BranchType.aspx?DepoBelongs=<%# Eval("DepoBelongs") %>' style="text-decoration: none;">
                                    <div class="branch-type-item show-loader">
                                        <div class="branch-type-label">
                                            <%# Eval("DepoBelongs") %>
                                        </div>
                                        <div class="branch-type-value">
                                            <%# Eval("Total_Count") %>
                                        </div>
                                    </div>
                                </a>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>


                </div>

                <!-- Godown Types -->
                <div>
                    
                    <h3 style="color: #1a237e; margin-bottom: 15px;">Godown Types</h3>

                    <div class="table-container">
                        <table class="data-table">
                            <tr>
                                <th>Type</th>
                                <th>Count</th>
                            </tr>

                            <asp:Repeater ID="rptGodownTypes" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <a href='../DashboardPages/GodownType.aspx?type=<%# Eval("Hired_Type_Group") %>' class="type-link">
                                                <%# Eval("Hired_Type_Group") %>
                                            </a>
                                        </td>
                                        <td>
                                            <a class="type-link">
                                                <%# Eval("Total_Count") %>
                                            </a>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>

                        </table>
                    </div>


                </div>

                <!-- Today's Activities -->
                <div class="highlight-box">
                    <div style="font-size: 16px; opacity: 0.9;">Today's Activities</div>
                    <div class="highlight-value" id="todayReceiving">1,250 MT</div>
                    <div style="font-size: 14px; margin-bottom: 15px;">Total Receiving</div>

                    <div class="highlight-value" id="todayDispatch">980 MT</div>
                    <div style="font-size: 14px;">Total Dispatch</div>
                </div>
            </div>
        </div>

        <!-- Payment Status Section -->
        <div class="dashboard-section">
            <div class="section-title">Payment Status</div>

            <div class="section-grid">
                <!-- JVS Godown Payment Status at MPSCSC -->
                <div>
                    <h3 style="color: #1a237e; margin-bottom: 15px;">JVS Godown Payment Status at MPSCSC</h3>
                    <table class="data-table">
                        <tr>
                            <th>Description</th>
                            <th>Amount</th>
                            <th>Status</th>
                        </tr>
                        <tr>
                            <td>Total Rent Amount</td>
                            <td>₹ 12,50,000</td>
                            <td><span class="status-badge status-complete">Paid</span></td>
                        </tr>
                        <tr>
                            <td>Total Received from MPSCSC</td>
                            <td>₹ 10,80,000</td>
                            <td><span class="status-badge status-complete">Received</span></td>
                        </tr>
                        <tr>
                            <td>Pendency at MPSCSC</td>
                            <td>₹ 1,70,000</td>
                            <td><span class="status-badge status-pending">Pending</span></td>
                        </tr>
                    </table>
                </div>

                <!-- Owned Godown Payment Status at MPSCSC -->
                <div>
                    <h3 style="color: #1a237e; margin-bottom: 15px;">Owned Godown Payment Status at MPSCSC</h3>
                    <table class="data-table">
                        <tr>
                            <th>Description</th>
                            <th>Amount</th>
                            <th>Status</th>
                        </tr>
                        <tr>
                            <td>Total Rent Amount</td>
                            <td>₹ 8,40,000</td>
                            <td><span class="status-badge status-complete">Paid</span></td>
                        </tr>
                        <tr>
                            <td>Total Received from MPSCSC</td>
                            <td>₹ 7,90,000</td>
                            <td><span class="status-badge status-complete">Received</span></td>
                        </tr>
                        <tr>
                            <td>Pendency at MPSCSC</td>
                            <td>₹ 50,000</td>
                            <td><span class="status-badge status-pending">Pending</span></td>
                        </tr>
                    </table>
                </div>

                <!-- JVS Payment Status at MPWLC -->
                <div>
                    <h3 style="color: #1a237e; margin-bottom: 40px;">JVS Payment Status at MPWLC</h3>
                    <table class="data-table">
                        <tr>
                            <th>Description</th>
                            <th>Amount</th>
                            <th>Status</th>
                        </tr>
                        <tr>
                            <td>Total Rent Received Amount</td>
                            <td>₹ 15,20,000</td>
                            <td><span class="status-badge status-complete">Received</span></td>
                        </tr>
                        <tr>
                            <td>Pay to Godown Owner by MPWLC</td>
                            <td>₹ 13,50,000</td>
                            <td><span class="status-badge status-complete">Paid</span></td>
                        </tr>
                        <tr>
                            <td>Pending at MPWLC</td>
                            <td>₹ 1,70,000</td>
                            <td><span class="status-badge status-pending">Pending</span></td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>

        <!-- Total Payment Status at MPSCSC -->
        <div class="dashboard-section">
            <div class="section-title">Total Payment Status at MPSCSC</div>

            <div style="max-width: 800px; margin: 0 auto;">
                <table class="data-table">
                    <tr>
                        <th>Description</th>
                        <th>Amount</th>
                        <th>Status</th>
                        <th>Last Updated</th>
                    </tr>
                    <tr>
                        <td>Total Submit Amount from MPWLC</td>
                        <td>₹ 25,40,000</td>
                        <td><span class="status-badge status-complete">Submitted</span></td>
                        <td>15 Nov 2023</td>
                    </tr>
                    <tr>
                        <td>Total Received Amount from MPSCSC</td>
                        <td>₹ 23,20,000</td>
                        <td><span class="status-badge status-complete">Received</span></td>
                        <td>10 Nov 2023</td>
                    </tr>
                    <tr>
                        <td>Total Pendency at MPSCS</td>
                        <td>₹ 2,20,000</td>
                        <td><span class="status-badge status-pending">Pending</span></td>
                        <td>15 Nov 2023</td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPageWidget" runat="Server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPageScript" runat="Server">
    <script type="text/javascript">
        // Set current date
        function setCurrentDate() {
            var now = new Date();
            var options = {
                weekday: 'long',
                year: 'numeric',
                month: 'long',
                day: 'numeric'
            };
            document.getElementById('currentDate').textContent = now.toLocaleDateString('en-IN', options);
        }

        // Initialize dashboard data
        function initializeDashboard() {
            setCurrentDate();

            // In a real application, these values would come from server-side code
            // For demonstration, we're setting static values

            // Simulate loading animation
            setTimeout(function () {
                // You could fetch data from server here
                console.log("Dashboard initialized");
            }, 500);
        }

        // Initialize when page loads
        document.addEventListener('DOMContentLoaded', initializeDashboard);

        // Refresh dashboard data periodically (every 5 minutes)
        setInterval(function () {
            // In a real app, this would fetch fresh data from the server
            console.log("Refreshing dashboard data...");
        }, 300000);
    </script>
</asp:Content>
