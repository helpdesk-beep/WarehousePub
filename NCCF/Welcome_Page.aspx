<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Nccf_Master.master" AutoEventWireup="true" CodeFile="~/NCCF/Welcome_Page.aspx.cs" Inherits="NCCF_Welcome_Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        /* Dashboard custom styles */
        .dashboard-cards {
            display: grid;
            grid-template-columns: repeat(auto-fit, minmax(250px, 1fr));
            gap: 25px;
            margin-top: 20px;
        }

        .card-box {
            background: linear-gradient(145deg, #ffffff, #e6f0ff);
            border-radius: 15px;
            box-shadow: 0 6px 12px rgba(0,0,0,0.1);
            padding: 25px;
            text-align: center;
            transition: transform 0.2s ease, box-shadow 0.2s ease;
        }

            .card-box:hover {
                transform: translateY(-6px);
                box-shadow: 0 10px 20px rgba(0,0,0,0.15);
            }

            .card-box i {
                font-size: 45px;
                color: #007bff;
                margin-bottom: 15px;
            }

        .card-title {
            font-weight: 600;
            font-size: 18px;
            color: #333;
            margin-bottom: 8px;
        }

        .card-value {
            font-size: 26px;
            color: #068def;
            font-weight: 700;
        }

        .welcome-banner {
            background: linear-gradient(120deg, #068def, #3aa6ff);
            color: white;
            border-radius: 15px;
            padding: 30px;
            margin-bottom: 30px;
            box-shadow: 0 6px 12px rgba(0,0,0,0.2);
            display: flex;
            align-items: center;
            justify-content: space-between;
            flex-wrap: wrap;
        }

            .welcome-banner h2 {
                font-size: 24px;
                font-weight: 700;
            }

            .welcome-banner p {
                font-size: 16px;
                opacity: 0.9;
            }

        .chart-container {
            margin-top: 40px;
            background: #fff;
            border-radius: 15px;
            padding: 25px;
            box-shadow: 0 6px 12px rgba(0,0,0,0.1);
        }

        canvas {
            width: 100% !important;
            height: 350px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <div class="welcome-banner">
        <div>
            <h2>Welcome, Admin!</h2>
            <p>Here’s a quick overview of the system performance and activities.</p>
        </div>
        <i class="fa fa-line-chart fa-3x"></i>
    </div>

    <div class="dashboard-cards">
        <div class="card-box">
            <i class="fa fa-warehouse"></i>
            <div class="card-title">Total Godowns</div>
            <div class="card-value">124</div>
        </div>
        <div class="card-box">
            <i class="fa fa-users"></i>
            <div class="card-title">Active Users</div>
            <div class="card-value">58</div>
        </div>
        <div class="card-box">
            <i class="fa fa-cubes"></i>
            <div class="card-title">Stock Items</div>
            <div class="card-value">8,543</div>
        </div>
        <div class="card-box">
            <i class="fa fa-truck"></i>
            <div class="card-title">Deliveries Today</div>
            <div class="card-value">27</div>
        </div>
    </div>

    <div class="chart-container mt-5">
        <h4>Monthly Performance</h4>
        <canvas id="myChart"></canvas>
    </div>

    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
    <script>
        const ctx = document.getElementById('myChart');
        new Chart(ctx, {
            type: 'bar',
            data: {
                labels: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun'],
                datasets: [{
                    label: 'Stock Movement',
                    data: [120, 90, 150, 200, 170, 230],
                    borderWidth: 1,
                    backgroundColor: '#068def'
                }]
            },
            options: {
                scales: { y: { beginAtZero: true } }
            }
        });
    </script>
</asp:Content>

