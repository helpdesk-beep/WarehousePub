<%@ Page Language="C#" AutoEventWireup="true" CodeFile="State_TrackPaymentStaus_For_Region_Rabi_2026_27.aspx.cs" Inherits="JointVentureScheme_State_TrackPaymentStaus_For_Region_Rabi_2026_27" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Warehouse Payment Status | Rabi 2026-27</title>
    <meta charset="utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.2/css/all.min.css" />

    <style type="text/css">
        body { font-family: 'Segoe UI', Tahoma, Arial, sans-serif; background-color: #f4f7f6; margin: 0; }
        .wrap { width: 1150px; margin: 20px auto; background: white; border-radius: 12px; overflow: hidden; box-shadow: 0 10px 30px rgba(0,0,0,0.1); }
        
        /* Navbar Styling */
        .modern-navbar {
            display: flex; justify-content: space-between; align-items: center;
            background: linear-gradient(135deg, #1e3c72, #2a5298);
            padding: 15px 25px; color: white; border-bottom: 4px solid #f39c12;
        }
        .nav-link { color: white !important; text-decoration: none; font-weight: 500; display: flex; align-items: center; gap: 10px; padding: 10px 20px; border-radius: 50px; transition: 0.3s; }
        .nav-link:hover { background: rgba(255,255,255,0.2); }
        .logout-btn { background: #d63031; }

        /* Search Section */
        .search-container { padding: 40px; text-align: center; }
        .search-card { display: inline-block; padding: 30px; border-radius: 15px; background: #f8f9fa; border: 1px solid #e9ecef; }
        .custom-input { padding: 12px; border: 2px solid #dfe6e9; border-radius: 8px; width: 250px; font-size: 1rem; outline: none; }
        .search-button { padding: 12px 30px; background: #00b894; color: white; border: none; border-radius: 8px; font-weight: bold; cursor: pointer; margin-left: 10px; transition: 0.3s; }
        .search-button:hover { background: #009470; transform: translateY(-2px); }

        /* Grid Header & Table Styling */
        .grid-wrapper { padding: 0 25px 30px 25px; }
        .modern-grid { border-radius: 8px; overflow: hidden; border: none !important; border-collapse: collapse; }
        .modern-grid th { 
            background: linear-gradient(to bottom, #5dade2, #2e86c1) !important; 
            color: white !important; padding: 12px !important; text-align: center; font-size: 0.85rem; border: 1px solid #ffffff33 !important;
        }
        .modern-grid td { padding: 12px !important; border-bottom: 1px solid #eee !important; text-align: center; font-size: 0.85rem; color: #2c3e50; }

        /* Status & Zero Value Highlights */
        .status-confirmed { color: #27ae60; font-weight: bold; background-color: #eafaf1; padding: 4px 10px; border-radius: 4px; display: inline-block; border: 1px solid #27ae60; }
        .status-pending { color: #e74c3c; font-weight: bold; background-color: #fdedec; padding: 4px 10px; border-radius: 4px; display: inline-block; border: 1px solid #e74c3c; }
        .highlight-red { color: #e74c3c !important; font-weight: bold; }

        .note-card { margin: 0 25px 30px 25px; padding: 20px; background: #fff5f5; border-left: 5px solid #ff7675; border-radius: 4px; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></cc1:ToolkitScriptManager>
        <div class="wrap">
            <img src="../images/CH.jpg" style="width:100%; height:140px; object-fit: cover;" alt="Banner" />

            <div class="modern-navbar">
                <asp:LinkButton ID="link1" runat="server" CssClass="nav-link" PostBackUrl="~/JointVentureScheme/JVSRegionReport.aspx">
                    <i class="fa fa-home"></i> Home
                </asp:LinkButton>
                <div><i class="fa fa-user-circle"></i> Welcome: <asp:Label ID="lbluser" runat="server" ForeColor="White" Font-Bold="true"></asp:Label></div>
                <asp:LinkButton ID="LinkButton1" runat="server" CssClass="nav-link logout-btn" OnClick="LinkButton1_Click">
                    <i class="fa fa-power-off"></i> Logout
                </asp:LinkButton>
            </div>

            <div class="search-container">
                <div class="search-card">
                    <div style="margin-bottom:15px; font-weight:bold; font-size:1.2rem; color:#2c3e50;">Track Registration/Offer Payment Status (Rabi 2026-27)</div>
                    <asp:TextBox ID="txtRegID" runat="server" CssClass="custom-input" placeholder="Registration ID Enter करें"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" CssClass="search-button" Text="SEARCH" OnClick="btnSearch_Click" />
                </div>
            </div>

            <div class="grid-wrapper">
                <asp:GridView ID="RegGrid" runat="server" AutoGenerateColumns="False" 
                    CssClass="modern-grid" Width="100%" DataKeyNames="Registration_Id" GridLines="None" OnRowDataBound="RegGrid_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="क्रमांक">
                            <ItemTemplate><%#Container.DataItemIndex+1%></ItemTemplate>
                            <HeaderStyle Width="40px" />
                        </asp:TemplateField>
                        
                        <asp:BoundField DataField="Registration_Id" HeaderText="रजिस्ट्रेशन आईडी" />
                        <asp:BoundField DataField="RegCapacity" HeaderText="रजिस्ट्रेशन की छमता" />
                        <asp:BoundField DataField="RegAmt" HeaderText="जमा की जाने वाली राशि" />
                        <asp:BoundField DataField="DepositedRegAmt" HeaderText="जमा की गई राशि" />
                        
                        <asp:TemplateField HeaderText="रजिस्ट्रेशन की स्थिति">
                            <ItemTemplate>
                                <asp:Label ID="lblRegStatus" runat="server" Text='<%# Eval("RegPaymentStatus") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="Offer_Capacity" HeaderText="आफर की गई छमता" />                                            
                        <asp:BoundField DataField="OfferAmt" HeaderText="आफर जमा राशि" />
                        <asp:BoundField DataField="ofrdepositedamt" HeaderText="आफर जमा की गई राशि" />
                        
                        <asp:TemplateField HeaderText="आफर की स्थिति">
                            <ItemTemplate>
                                <asp:Label ID="lblOfferStatus" runat="server" Text='<%# Eval("OfferPaymentStatus") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <AlternatingRowStyle BackColor="#f9f9f9" />
                </asp:GridView>
            </div>

           <%-- <div class="note-card">
                <strong style="color: #d63031;"><i class="fa fa-info-circle"></i> Note:</strong>
                <ul style="margin: 10px 0 0 0; padding-left: 20px; line-height: 1.6; color: #444;">
                    <li>रजिस्ट्रेशन एवं आफर के भुगतान की पुष्टि पश्चात ही आपका आफर पूर्ण माना जावेगा।</li>
                    <li>यदि भुगतान पोर्टल पर अपडेट नहीं हुआ है तो कृपया पेमेंट रिसीप्ट की जांच करें।</li>
                </ul>
            </div>--%>
        </div>
    </form>
</body>
</html>