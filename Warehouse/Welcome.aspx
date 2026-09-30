<%@ Page Language="C#" MasterPageFile="~/MasterPage/Region_Master.master" AutoEventWireup="true" CodeFile="Welcome.aspx.cs" Inherits="Welcome" Title="Welcome" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="http://mpsc.mp.nic.in/Warehouse/Administration/assets/jQuery%20Package/jquery-1.10.2/jquery-1.10.2.js" type="text/javascript"></script>
    <script src="http://mpsc.mp.nic.in/Warehouse/Administration/assets/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="http://mpsc.mp.nic.in/Warehouse/Administration/assets/js/bootstrap.bundle.min.js" type="text/javascript"></script>
    <style type="text/css">
        #popupwin {
            position: fixed;
            top: 0;
            left: 0;
            width: 90%;
            height: 90%;
            background-color: #000;
            filter: alpha(opacity=65);
            -moz-opacity: 0.7;
            display: none;
            opacity: 0.7;
            z-index: 100;
        }

        .pop a {
            text-decoration: none;
        }

        .popup {
            width: 100%;
            height: 98%;
            margin: 0 auto;
            position: fixed;
            z-index: 101;
        }

        .pop {
            min-width: 900px;
            width: 1012px;
            min-height: 150px;
            margin: 0px auto;
            background: #FFFFFF;
            position: relative;
            z-index: 103;
            padding: 10px;
            border-radius: 5px;
            box-shadow: 0 5px 10px #000;
        }

            .pop p {
                color: #555555;
                text-align: justify;
                font-size: medium;
            }

                .pop p a {
                    color: #d91900;
                }

            .pop .x {
                float: right;
                height: 35px;
                left: 22px;
                position: relative;
                top: -20px;
                width: 35px;
            }
    </style>
    <table width="100%" border="0" cellspacing="0" cellpadding="0">

        <tr>
            <td class="boldTxt" style="text-align: center;">Welcome 
        <asp:Label ID="UxName" runat="server" Text="Label"></asp:Label>
                &nbsp;to State Foodgrains Management System </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
    </table>
    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/AdminSearch.aspx">Search Page</asp:HyperLink><br />
    <asp:HyperLink ID="HyperLink2" runat="server"
        NavigateUrl="~/StatePages/datatransfer.aspx">Transfer Godown</asp:HyperLink><br />
    <asp:HyperLink ID="HyperLink3" runat="server"
        NavigateUrl="~/StatePages/UpdateBranchIssuecenter.aspx">Update Issuecenter ID of Branch</asp:HyperLink>

    <asp:Panel ID="r1" runat="server" Width="1000px">
        <div style="width: 1000px; margin-left: 0px">
            <table cellpadding="5" cellspacing="0" style="width: 100%; border-width: 1px; border-color: Green"
                border="1px">
                <tr style="background-color: #1258af; height: 25px">
                    <td colspan="2" align="center">
                        <asp:Label ID="Label9" runat="server" Text="Godown Loss/Gain Details" ForeColor="whitesmoke"
                            Font-Bold="true" Font-Size="12pt"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">1.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton126" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton126_Click">गोदाम संचालको के देयकों से 20% किया गया कटोत्रा की जानकारी अपडेट करे </asp:LinkButton>
                    </td>
                </tr>

                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">2.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton127" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton127_Click">क्षेत्रीय कार्यालय द्वारा अपडेट की गई कटोत्रा की जानकारी</asp:LinkButton>
                    </td>
                </tr>

                <tr style="background-color: #1258af; height: 25px">
                    <td colspan="2" align="center">
                        <asp:Label ID="Label8" runat="server" Text="New Activity Monitoring Reports" ForeColor="whitesmoke"
                            Font-Bold="true" Font-Size="12pt"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">1.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton106" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton106_Click">FIFO पद्धति से उठाव हेतु शाखाओ द्वारा फ्रिज किये गये Stock की जानकारी </asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">2.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton107" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton107_Click">FIFO पद्धति से किये गए भुगतान की जानकारी </asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">3.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton105" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton105_Click">Report Stock Reconciliation</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">4.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton111" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton111_Click">Report Stock Reconciliation with Remark</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">5.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton113" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton113_Click">View Private Godown Complaint</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">6.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton123" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton123_Click">District wise FAQ, Non-FAQ and DCC Stock Entry by BM</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">7.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton133" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton133_Click">Godown Wise Stock Position in MT</asp:LinkButton>
                    </td>
                </tr>

                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">8.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton135" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton135_Click">Report For Review of District , Crop Year Wise, Commodity Wise Stock Position Report (Month wise in MT)</asp:LinkButton>
                    </td>
                </tr>

                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">9.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton136" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton136_Click">जमाकर्ता से भुगतान की स्थिति (प्रोफोर्मा-अ)</asp:LinkButton>
                    </td>
                </tr>

                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">10.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton137" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton137_Click">निजी गोदामों के भुगतान की स्थिति (प्रोफोर्मा-ब)</asp:LinkButton>
                    </td>
                </tr>

                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">11.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton141" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton141_Click">जमाकर्ता से निजी गोदामों के भुगतान की स्थिति (नया प्रोफोर्मा)</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">12.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton144" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton141_Click">District, Branch, Commodity, Crop Year Wise FAQ, Non-FAQ and DCC Stock Monitoring </asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">13.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton145" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton145_Click">Stock Entry By Branch Manager </asp:LinkButton>
                    </td>
                </tr>

                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">14.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton146" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton146_Click">DCC Stock Entry By Branch Manager </asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">15.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton157" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton157_Click">Stack Wise Fumigation Entry Report </asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">16.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton158" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton158_Click">Stack Wise Moisture Entry Report </asp:LinkButton>
                    </td>
                </tr>

                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">17.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton159" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton159_Click">Stack Wise Moisture Entry as per Online (WHR बनाते समय ली गई औसत नमी) </asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">18.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton162" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton162_Click">Branch/Godown Wise Post Mansoon Fumigation Report </asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">19.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton167" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton167_Click">Godown Mapping With WeighBridge</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">20.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton168" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton168_Click">Pending Update WeighBridge Name</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">21.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton169" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton169_Click">Pending Weighbridge to Godown Mapping</asp:LinkButton>
                    </td>
                </tr>

                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">22.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton170" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton170_Click">स्कंध वार जमाकर्ता से भुगतान की स्थिति</asp:LinkButton>
                    </td>
                </tr>

                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">23.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton171" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton171_Click">Compare Old vs New Depositor Entry Report</asp:LinkButton>
                    </td>
                </tr>

                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">24.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton172" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton172_Click">JIT Bill Status Report</asp:LinkButton>
                    </td>
                </tr>
                <tr style="background-color: #af1d12; height: 25px">
                    <td colspan="2" align="center">
                        <asp:Label ID="Label7" runat="server" Text="Billing Reports New" ForeColor="whitesmoke"
                            Font-Bold="true" Font-Size="12pt"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">1.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton100" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton100_Click1">MPSCSC से लंबित भुगतान का विवरण </asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">2.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton102" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton102_Click">MPSCSC से प्राप्त भुगतान के पश्यात विभिन्य स्तर पर लंबित बिलों की जानकारी </asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">3.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton101" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton101_Click">Godown Wise MPSCSC से लंबित भुगतान का विवरण</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">4.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton103" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton103_Click">क्षेत्रीय प्रबंधक के स्तर पर NEFT फाइल बनाने के लिए लंबित देयक</asp:LinkButton>
                    </td>
                </tr>

                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">5.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton117" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton117_Click">	Pendency Payment Details(After August)</asp:LinkButton>
                    </td>
                </tr>

                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">6.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton124" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton124_Click">Godown Wise Payment Status(Only JVS)</asp:LinkButton>
                    </td>
                </tr>

                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">7.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton128" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton128_Click">District Wise Payment Status(Only JVS)</asp:LinkButton>
                    </td>
                </tr>

                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">8.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton129" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton129_Click">Payment Status of BOT Godowns (Filled Capacity)</asp:LinkButton>
                    </td>
                </tr>

                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">9.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton131" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton131_Click">District Wise Payment Status( Receiving,Pending and Pay to godown)</asp:LinkButton>
                    </td>
                </tr>

                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">10.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton132" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton132_Click">ब्रांच मैनेजर द्वारा गोदामों के रेंट बिलो के लंबित कटोत्रा की जानकारी </asp:LinkButton>
                    </td>
                </tr>

                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">11.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton134" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton134_Click">District,Commodity Wise Payment Status( Receiving,Pending and Pay to godown) </asp:LinkButton>
                    </td>
                </tr>

                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">12.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton138" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton138_Click">Owned Godown Wise Payment Status</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">13.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton139" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton139_Click">Godown Type Wise Payment Status</asp:LinkButton>
                    </td>
                </tr>

                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">14.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton140" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton140_Click">ब्रांच मेनेजर द्वारा बनाने हेतु शेष स्टोरेज चार्जेज बिल</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">15.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton151" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton151_Click">Branch Wise Rent Bill Pending Against Receive Storage Bill From MPSCSC</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">16.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton152" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton152_Click">District Godown Wise Pending Rent Bill</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">17.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton161" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton161_Click">Region Wise Pending final bill For Generation And Submition</asp:LinkButton>
                    </td>
                </tr>

                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">18.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton163" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton163_Click">Region Wise Vaccant Capacity Payment Status</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">19.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton166" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton166_Click">District Wise Bill Pending Payment Status At Various Levels(From August)</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">19.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton173" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton173_Click">Godown Wise Payment Advice Details from MPSCSC</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">20.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton177" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton177_Click">Passing Order Report For Income Tax Date Wise</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">21.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton183" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton183_Click">Godown Bill Wise Deduction Report</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">22.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton186" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton186_Click">Godown Wise Pending DSC Bill From DM MPSCSC Report</asp:LinkButton>
                    </td>
                </tr>
                <tr style="background-color: #af1d12; height: 25px">
                    <td colspan="2" align="center">
                        <asp:Label ID="Label10" runat="server" Text="NAFED/NCCF Billing Reports" ForeColor="whitesmoke"
                            Font-Bold="true" Font-Size="12pt"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">1.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton130" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton130_Click">NAFED Billing Status</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">2.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton148" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton148_Click">NAFED Rent Bill</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">3.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton149" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton149_Click">NAFED Received Payment Details</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">4.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton150" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton150_Click">Print NAFED Rent Bill</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">5.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton165" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton165_Click">NCCF Pending Bills Status</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">6.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton178" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton178_Click">Receive Payment From NCCF</asp:LinkButton>
                    </td>
                </tr>

                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">7.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton179" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton179_Click">JIT Payment Status MPWLC To Godown Owner</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">8.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton180" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton180_Click">NCCF Payment to Godown Owner Details</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">9.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton181" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton181_Click">Godown Wise Payment Status(NCCF)</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">10.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton182" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton182_Click">Date Wise Receive Payment From NAFED</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">11.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton184" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton184_Click">NAFED Wrong Storage Bill Ganerate From Branch</asp:LinkButton>
                    </td>
                </tr>
                <tr style="background-color: #167515; height: 25px">
                    <td colspan="2" align="center">
                        <asp:Label ID="lblStorageReports" runat="server" Text="Godown Details" ForeColor="whitesmoke"
                            Font-Bold="true" Font-Size="12pt"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">1.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton4" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton4_Click">Godown List</asp:LinkButton>
                    </td>
                </tr>

                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">2.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton32" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton32_Click">Issue Center Wise MPWLC Branch Detail</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">3.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton11" runat="server" Font-Bold="true" Font-Size="10pt" ForeColor="navy" ValidationGroup="link1" OnClick="LinkButton11_Click">Godown Geo location</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">4.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton42" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy" ValidationGroup="link1"
                            OnClick="LinkButton42_Click">गोदाम मास्टर एवं प्राप्त जानकारी अनुसार परिसर वार गोदाम वार रिपोर्ट </asp:LinkButton>
                    </td>
                </tr>

                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">5.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton43" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy" ValidationGroup="link1"
                            OnClick="LinkButton43_Click">JVS Godown Details </asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">6.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton58" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy" ValidationGroup="link1" OnClick="LinkButton58_Click">New Verify Godown Summary against Old Godown</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">7.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton59" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy" ValidationGroup="link1" OnClick="LinkButton59_Click">New Verify Godown Type wise Summary Rerport</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">8.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton60" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy" ValidationGroup="link1" OnClick="LinkButton60_Click">New Verify Godown capacity Summary Rerport</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">9.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton61" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy" ValidationGroup="link1" OnClick="LinkButton61_Click">New Verify Godown Available Stock and Vacant Capacity Report(Based on 15/10/2018 Closing Balance)</asp:LinkButton>
                    </td>
                </tr>

                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">10.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton66" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy" ValidationGroup="link1" OnClick="LinkButton66_Click">View Uploaded Digital Signature Certificate(DSC)</asp:LinkButton>
                    </td>
                </tr>

                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">11.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton68" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy" ValidationGroup="link1" OnClick="LinkButton68_Click">e-WHR Online Submission and Print Detail (Dalhan Procurement 2019-20)</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">12.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton71" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy" ValidationGroup="link1" OnClick="LinkButton71_Click">Godown Owners Account Details</asp:LinkButton>
                    </td>
                </tr>
                <tr style="background-color: #24C4BF; height: 25px">
                    <td colspan="2" align="center">
                        <asp:Label ID="Label3" runat="server" Text="Stock Reports" ForeColor="whitesmoke"
                            Font-Bold="true" Font-Size="12pt"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">1. </td>
                    <td>
                        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" ValidationGroup="link1"
                            ForeColor="navy" Font-Bold="true" Font-Size="10pt">Commodity Wise - Godown Wise WHR Status (With Crop Year)</asp:LinkButton>

                    </td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">2. </td>
                    <td>
                        <asp:LinkButton ID="LinkButton23" runat="server" OnClick="LinkButton23_Click" ValidationGroup="link1"
                            ForeColor="navy" Font-Bold="true" Font-Size="10pt">Godown Wise Stock Report (With Crop Year)</asp:LinkButton>

                    </td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">3.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton24" runat="server" ValidationGroup="link1"
                            ForeColor="navy" Font-Bold="true" Font-Size="10pt"
                            OnClick="LinkButton24_Click">Crop Year Wise - Commodity Wise Stock Report</asp:LinkButton>

                    </td>
                </tr>

                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">4.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton27" runat="server" ValidationGroup="link1"
                            ForeColor="navy" Font-Bold="true" Font-Size="10pt" OnClick="LinkButton27_Click">Commodity Wise Godown Wise MPSCSC Current Stock Report</asp:LinkButton>

                    </td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">5.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton28" runat="server" ValidationGroup="link1"
                            ForeColor="navy" Font-Bold="true" Font-Size="10pt" OnClick="LinkButton28_Click">Commodity Wise Storage Type Wise MPSCSC Stock Report Till Date</asp:LinkButton>

                    </td>
                </tr>
                <%--                <tr>
    <td style="width:30px ; color: Navy ; font-weight:bold" align="center">6.</td>
    <td>
    <asp:LinkButton ID="LinkButton29" runat="server" ValidationGroup="link1"
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt" 
            onclick="LinkButton29_Click">Commodity and Crop Year Wise Stock Report Till Date</asp:LinkButton>
       
    </td>
    </tr>--%>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">6.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton30" runat="server" ValidationGroup="link1"
                            ForeColor="navy" Font-Bold="true" Font-Size="10pt" OnClick="LinkButton30_Click">District Wise Paddy Stock Report</asp:LinkButton>

                    </td>
                </tr>

                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">7.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton31" runat="server" ValidationGroup="link1"
                            ForeColor="navy" Font-Bold="true" Font-Size="10pt" OnClick="LinkButton31_Click">District Wise Rice Stock Report</asp:LinkButton>

                    </td>
                </tr>

                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">8.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton33" runat="server" ValidationGroup="link1"
                            ForeColor="navy" Font-Bold="true" Font-Size="10pt" OnClick="LinkButton33_Click">Commodity Wise Stock In Graph Representation</asp:LinkButton>
                    </td>
                </tr>

                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">9.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton34" runat="server" ValidationGroup="link1"
                            ForeColor="navy" Font-Bold="true" Font-Size="10pt" OnClick="LinkButton34_Click">CropYear Wise MPSCSC Stock In Chart Representation</asp:LinkButton>
                    </td>
                </tr>

                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">10.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton36" runat="server" ValidationGroup="link1"
                            ForeColor="navy" Font-Bold="true" Font-Size="10pt" OnClick="LinkButton36_Click">Godown Wise - Commodity Wise Stack Stock Position</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">11.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton39" runat="server" ValidationGroup="link1"
                            ForeColor="navy" Font-Bold="true" Font-Size="10pt" OnClick="LinkButton39_Click">Gatepass Wise Stock Issue Detail B/W Two Dates</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">12.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton56" runat="server" ValidationGroup="link1"
                            ForeColor="navy" Font-Bold="true" Font-Size="10pt" OnClick="LinkButton56_Click">Godown Type Wise Godown Capacity and Available Stock Report</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">13.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton57" runat="server" ValidationGroup="link1"
                            ForeColor="navy" Font-Bold="true" Font-Size="10pt" OnClick="LinkButton57_Click">District Wise Vacant Capacity Status</asp:LinkButton>
                    </td>
                </tr>


                <tr style="background-color: #91210F; height: 25px">
                    <td colspan="2" align="center">
                        <asp:Label ID="Label1" runat="server" Text="Procurement Details" ForeColor="whitesmoke"
                            Font-Bold="true" Font-Size="12pt"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">1.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton10" runat="server" Font-Bold="true" Font-Size="10pt" ForeColor="navy" ValidationGroup="link1" OnClick="LinkButton10_Click">Paddy Procurement 2015-16</asp:LinkButton>
                    </td>
                </tr>

                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">2.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton2" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton2_Click">Panding Delivery Gatepass</asp:LinkButton>
                    </td>
                </tr>


                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">3.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton5" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton5_Click">Procurement work Status </asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">4.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton6" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton6_Click">Procurement work Status(2015-16) </asp:LinkButton></td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">5.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton7" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton7_Click">Procurement work Status with menual & truck wise(2015-16) </asp:LinkButton></td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">6</td>
                    <td>
                        <asp:LinkButton ID="LinkButton13" runat="server" Font-Bold="true" Font-Size="10pt" ForeColor="navy" ValidationGroup="link1" OnClick="LinkButton13_Click">Wheat Procurement 2016-17</asp:LinkButton></td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">7.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton25" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton25_Click">Paddy - Kharif Procurement 2016-17 </asp:LinkButton></td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">8.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton26" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton26_Click">All Commodity - Kharif Procurement 2016-17 
                        </asp:LinkButton></td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">9.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton37" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton37_Click">Wheat Procurement 2017-18 
                        </asp:LinkButton></td>
                </tr>

                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">10.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton38" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton38_Click">Remaining Depositor Form for Creating WHR Wheat Procurement 2017-18 
                        </asp:LinkButton></td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">11.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton40" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton40_Click">Arhar Procurement 2017-18  
                        </asp:LinkButton></td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">12.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton41" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton41_Click">Onion Procurement 2017-18  
                        </asp:LinkButton></td>
                </tr>

                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">13.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton29" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton29_Click1">Kharif Procurement 2017-18 Commodity Wise  
                        </asp:LinkButton></td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">14.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton45" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton45_Click">Remaining Depositor Form for Creating WHR Paddy Procurement 2017-18  
                        </asp:LinkButton></td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">15.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton46" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton46_Click">Wheat Procurement 2018-19  
                        </asp:LinkButton></td>
                </tr>

                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">16.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton47" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton47_Click">GRAM,LENTIL,Mustard-Sarason Procurement 2018-19
                        </asp:LinkButton></td>
                </tr>

                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">17.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton48" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton48_Click">Wheat Procurement 2018-19(WLC)
                        </asp:LinkButton></td>
                </tr>

                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">18.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton49" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton49_Click">Wheat Procurement 2018-19 (NON-WLC)
                        </asp:LinkButton></td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">19.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton50" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton50_Click">Comparative Report of Provisional D.F , Final D.F & WHR Issued Quantity Chana,Sarson,Masoor Procurement 2018-19
                        </asp:LinkButton></td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">20.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton51" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton51_Click">GRAM,LENTIL,Mustard-Sarason Procurement 2018-19 (WLC)
                        </asp:LinkButton></td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">21.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton52" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton52_Click">GRAM,LENTIL,Mustard-Sarason Procurement 2018-19 (NON-WLC)
                        </asp:LinkButton></td>
                </tr>

                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">22.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton62" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton62_Click">Kharif Procurement 2018-19 (Ground-Nut,Moong,RAM TIL,Urad,Tilli)
                        </asp:LinkButton></td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">23.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton63" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton63_Click">Coarse Grain Procurement 2018-19 (Bajra,Jowar)
                        </asp:LinkButton></td>
                </tr>

                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">24.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton64" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton64_Click">Paddy Procurement 2018-19
                        </asp:LinkButton></td>
                </tr>

                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">25.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton65" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton65_Click">Wheat Procurement 2019-20
                        </asp:LinkButton></td>
                </tr>

                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">26.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton67" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton67_Click">Dalhan e-WHR 2019-20
                        </asp:LinkButton></td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">27.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton69" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton69_Click">Arahar e-WHR 2019-20
                        </asp:LinkButton></td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">28.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton70" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton70_Click">Wheat-PSS e-WHR 2019-20
                        </asp:LinkButton></td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">29.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton75" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton75_Click">Paddy Procurement 2019-20
                        </asp:LinkButton></td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">30.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton76" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton76_Click1">Course Grain Procurement 2019-20
                        </asp:LinkButton></td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">31.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton79" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton79_Click">Wheat Procurement 2020-21
                        </asp:LinkButton></td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">32.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton80" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton80_Click">Chana,Masoor,Sarson Procurement 2020-21
                        </asp:LinkButton></td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">33.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton81" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton81_Click">Chana,Masoor,Sarson e-whr submission 2020-21
                        </asp:LinkButton></td>
                </tr>

                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">34.</td>
                    <td>
                        <asp:LinkButton ID="btnPCBJCD" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="btnPCBJCD_Click">Kharif Procurement WHR 2020-21
                        </asp:LinkButton></td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">35.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton83" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton83_Click">Wheat Procurement 2021-22
                        </asp:LinkButton></td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">36.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton85" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton85_Click">Chana,Masoor,Sarson Procurement 2021-22
                        </asp:LinkButton></td>
                </tr>

                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">37.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton88" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton88_Click">Chana,Masoor,Sarson e-whr submission 2020-21
                        </asp:LinkButton></td>
                </tr>


                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">38.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton90" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton90_Click"> Moong Procurement 2021-22
                        </asp:LinkButton></td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">39.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton91" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton91_Click"> Urad Procurement 2021-22
                        </asp:LinkButton></td>
                </tr>

                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">40.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton93" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton93_Click">Paddy Procurement 2021-22
                        </asp:LinkButton></td>
                </tr>

                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">41.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton94" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton94_Click">Bajra Procurement 2021-22
                        </asp:LinkButton></td>
                </tr>

                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">42.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton95" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton95_Click">Jowar Procurement 2021-22
                        </asp:LinkButton></td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">42.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton98" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton99_Click">Wheat Procurement 2022-23
                        </asp:LinkButton></td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">43.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton99" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton100_Click"> Dalhan Procurement 2022-23
                        </asp:LinkButton></td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">44.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton104" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton104_Click"> Moong Urad Procurement 2022-23
                        </asp:LinkButton></td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">45.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton112" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton112_Click">Paddy Procurement 2022-23
                        </asp:LinkButton></td>
                </tr>

                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">46.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton114" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton114_Click">Wheat Procurement 2023-24
                        </asp:LinkButton></td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">47.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton115" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton115_Click">Dalhan Procurement 2023-24
                        </asp:LinkButton></td>
                </tr>

                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">48.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton116" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton116_Click"> Moong Urad Procurement 2023-24
                        </asp:LinkButton></td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">49.</td>
                    <td>
                        <asp:LinkButton ID="btnPaddy202324" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="btnPaddy202324_Click"> Paddy Procurement 2023-24
                        </asp:LinkButton></td>
                </tr>

                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">50.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton119" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton118_Click"> Moong Urad Procurement 2023-24 Submission
                        </asp:LinkButton></td>
                </tr>

                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">51.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton118" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton118_Click1">Wheat Procurement 2024-25
                        </asp:LinkButton></td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">52.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton120" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton120_Click">Dalhan Procurement 2024-25
                        </asp:LinkButton></td>
                </tr>

                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">53.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton121" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton121_Click"> Moong Urad Procurement 2024-25
                        </asp:LinkButton></td>
                </tr>

                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">54.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton122" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton122_Click"> Chana,Masoor,Sarson e-whr submission 2024-25
                        </asp:LinkButton></td>
                </tr>

                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">55.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton125" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton125_Click"> Moong Urad e-whr submission 2024-25
                        </asp:LinkButton></td>
                </tr>

                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">56.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton142" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton142_Click"> Soya-Beens Procurement 2024-25
                        </asp:LinkButton></td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">57.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton143" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton143_Click"> Soya-Beens e-whr submission 2024-25
                        </asp:LinkButton></td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">58.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton147" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton147_Click"> Paddy Procurement 2024-25
                        </asp:LinkButton></td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">59.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton153" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton153_Click1">Wheat Procurement 2025-26
                        </asp:LinkButton></td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">60.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton154" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton154_Click">Dalhan Procurement 2025-26
                        </asp:LinkButton></td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">61.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton155" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton155_Click1"> Moong Urad Procurement 2025-26
                        </asp:LinkButton></td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">62.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton156" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton156_Click"> Chana,Masoor,Sarson e-whr submission 2025-26
                        </asp:LinkButton></td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">63.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton160" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton160_Click"> Moong Urad e-whr submission 2025-26
                        </asp:LinkButton></td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">64.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton164" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton164_Click"> Paddy Procurement 2025-26
                        </asp:LinkButton></td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">65.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton96" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton96_Click"> Kodo Kutki Procurement 2025-26
                        </asp:LinkButton></td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">65.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton174" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton174_Click"> Wheat Procurement 2026-27
                        </asp:LinkButton></td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">65.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton175" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton175_Click"> Dalhan Procurement 2026-27
                        </asp:LinkButton></td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">62.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton176" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton176_Click"> Chana,Masoor,Sarson e-whr submission 2026-27
                        </asp:LinkButton></td>
                </tr>
                  <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">63.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton185" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton185_Click"> Moong Urad Procurement 2026-27
                        </asp:LinkButton></td>
                </tr>
                <tr style="background-color: #6A45CD; height: 25px">
                    <td colspan="2" align="center">
                        <asp:Label ID="Label2" runat="server" Text="Capacity and Utilization Reports" ForeColor="whitesmoke"
                            Font-Bold="true" Font-Size="12pt"></asp:Label>
                    </td>
                </tr>

                <%--        <tr>
            <td style="width:30px ; color: Navy ; font-weight:bold" align="center">
                1.</td>
            <td>
                <asp:LinkButton ID="LinkButton8" runat="server" Font-Bold="true" 
                    Font-Size="10pt" ForeColor="navy" 
                    ValidationGroup="link1" onclick="LinkButton8_Click">Godown Capacity Utilization </asp:LinkButton></td>
        </tr>--%>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">1.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton9" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton9_Click">Godown Wise Capacity & Utilization</asp:LinkButton>
                    </td>
                </tr>

                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">2.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton17" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy" ValidationGroup="link1" OnClick="LinkButton17_Click">Steel Silo Capacity & Utilization</asp:LinkButton></td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">3.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton16" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy" ValidationGroup="link1" OnClick="LinkButton16_Click">PVT. PEG Godowns Capacity and Utilization</asp:LinkButton></td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">4.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton18" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy" ValidationGroup="link1"
                            OnClick="LinkButton18_Click">WDRA Godowns Capacity and Utilization</asp:LinkButton></td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">5.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton19" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy" ValidationGroup="link1"
                            OnClick="LinkButton19_Click">JVS Godowns Capacity and Utilization</asp:LinkButton></td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">6.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton20" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy" ValidationGroup="link1"
                            OnClick="LinkButton20_Click">Owned Godowns Capacity and Utilization</asp:LinkButton></td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">7.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton35" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy" ValidationGroup="link1" OnClick="LinkButton35_Click"> Godowns Capacity and Utilization in Graph Representation</asp:LinkButton></td>
                </tr>

                <tr style="background-color: #24C4BF; height: 25px">
                    <td colspan="2" align="center">
                        <asp:Label ID="Label6" runat="server" Text="Deletion Reports" ForeColor="whitesmoke"
                            Font-Bold="true" Font-Size="12pt"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">1.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton53" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy" ValidationGroup="link1" OnClick="LinkButton53_Click">QC Deleted Report (GRAM,LENTIL,Mustard-Sarason Procurement 2018-19)</asp:LinkButton></td>
                </tr>

                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">2.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton54" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy" ValidationGroup="link1" OnClick="LinkButton54_Click">WHR Deleted Report (GRAM,LENTIL,Mustard-Sarason Procurement 2018-19)</asp:LinkButton></td>
                </tr>

                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">3.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton55" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy" ValidationGroup="link1" OnClick="LinkButton55_Click">WHR Deleted Report Against WHR Delete Request Date (GRAM,LENTIL,Mustard-Sarason Procurement 2018-19)</asp:LinkButton></td>
                </tr>


                <tr style="background-color: Gray; height: 25px">
                    <td colspan="2" align="center">
                        <asp:Label ID="Label4" runat="server" Text="Billing Reports" ForeColor="whitesmoke"
                            Font-Bold="true" Font-Size="12pt"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">1.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton44" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy" ValidationGroup="link1" OnClick="LinkButton44_Click">Month wise Created Bill Status by Branch</asp:LinkButton></td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">2.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton72" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy" OnClick="LinkButton72_Click">Owned Godown Storage Charges Bill Summary</asp:LinkButton></td>
                </tr>

                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">3.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton73" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy" OnClick="LinkButton73_Click"> PVT Godown Storage Charges Bill Summary</asp:LinkButton></td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">4.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton74" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy" OnClick="LinkButton74_Click"> Godown Rent Passing Order Detail Success/Failure</asp:LinkButton></td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">5.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton78" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy" OnClick="LinkButton78_Click"> Godown Rent Passing Order Detail</asp:LinkButton></td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">6.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton84" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy" OnClick="LinkButton77_Click"> Online Payment Status</asp:LinkButton></td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">7.</td>
                    <td>
                        <asp:LinkButton ID="btn18" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy" OnClick="btn18_Click"> 	Branch Wise PVT Godown Storage Charges Bill in Amount(From August)</asp:LinkButton></td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">8.</td>
                    <td>
                        <asp:LinkButton ID="btn19" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy" OnClick="btn19_Click"> PVT Godown Storage Charges Bill(Before August)</asp:LinkButton></td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">9.</td>
                    <td>
                        <asp:LinkButton ID="btn10" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy" OnClick="btn10_Click"> PVT Godown Storage Charges Bill(After August)</asp:LinkButton></td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">10.</td>
                    <td>
                        <asp:LinkButton ID="btn20" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy" OnClick="btn20_Click"> Branch Wise PVT Godown Storage Charges Bill in Amount(Before August)</asp:LinkButton></td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">11.</td>
                    <td>
                        <asp:LinkButton ID="btn21" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy" OnClick="btn21_Click"> PVT Godown Storage Charges Bill(From August)Pending at DM MPSCSC Level</asp:LinkButton></td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">12.</td>
                    <td>
                        <asp:LinkButton ID="btnpasfromaug" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy" OnClick="btnpasfromaug_Click"> Godown Rent Passing Order Detail(From Aug.)</asp:LinkButton></td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">13.</td>
                    <td>
                        <asp:LinkButton ID="btnPaymentreceivedfrommpscsc" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy" OnClick="btnPaymentreceivedfrommpscsc_Click"> Payment Received Details From MPSCSC(From Aug.)</asp:LinkButton></td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">14.</td>
                    <td>
                        <asp:LinkButton ID="btnbranchwisereceivedpayment" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy" OnClick="btnbranchwisereceivedpayment_Click"> Branch Wise Payment Received Details From MPSCSC(From Aug.)</asp:LinkButton></td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">15.</td>
                    <td>
                        <asp:LinkButton ID="btngodownwisepayment" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy" OnClick="btngodownwisepayment_Click"> Godown Wise Payment Received Details From MPSCSC(From Aug.)</asp:LinkButton></td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">16.</td>
                    <td>
                        <asp:LinkButton ID="btnpendingbillgenerationFAug" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy" OnClick="btnpendingbillgenerationFAug_Click"> Pending Bill for Generation from August</asp:LinkButton></td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">17.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton77" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy" OnClick="LinkButton77_Click1">Account Benificiary Status</asp:LinkButton></td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">18.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton82" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy" OnClick="LinkButton82_Click">Payment to Godown Owner by MPWLC(From August)</asp:LinkButton></td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">19.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton86" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy" OnClick="LinkButton86_Click">	Pending Storage Bills Report for Payment(From August)</asp:LinkButton></td>
                </tr>

                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">20.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton87" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy" OnClick="LinkButton87_Click">	Payment Credit From MPWLC To Godown (From August) NEW</asp:LinkButton></td>
                </tr>

                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">21.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton89" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy" OnClick="LinkButton89_Click">View EPF/ NEFT File</asp:LinkButton></td>
                </tr>

                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">22.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton97" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy" OnClick="LinkButton97_Click">View EPF/ NEFT File Godown Wise (New)</asp:LinkButton></td>
                </tr>


                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">23.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton92" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy" OnClick="LinkButton92_Click">Pendency at Various Level(After August)</asp:LinkButton></td>
                </tr>


                <%-- <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">24.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton96" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy" OnClick="LinkButton96_Click">Pendency at Various Level(After August New)</asp:LinkButton></td>
                </tr>--%>

                <tr style="background-color: #0bb6e6; height: 25px">
                    <td colspan="2" align="center">
                        <asp:Label ID="Label5" runat="server" Text="Other Reports" ForeColor="whitesmoke"
                            Font-Bold="true" Font-Size="12pt"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">1.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton21" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy" ValidationGroup="link1" OnClick="LinkButton21_Click">WHR Wise Date Difference B/W WHR Issue Date and Entry Date</asp:LinkButton></td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">2.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton22" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy" ValidationGroup="link1" OnClick="LinkButton22_Click">Gate Pass Wise Date Difference B/W Gate Pass Issue Date and Entry Date</asp:LinkButton></td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">3.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton15" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy" ValidationGroup="link1"
                            OnClick="LinkButton15_Click">Branch Wise Paddy Loss Detail</asp:LinkButton></td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">4.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton12" runat="server" Font-Bold="true" Font-Size="10pt" ForeColor="navy" ValidationGroup="link1" OnClick="LinkButton12_Click">Lambit Rashi Detail</asp:LinkButton>

                    </td>
                </tr>

                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">5.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton14" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy" ValidationGroup="link1"
                            OnClick="LinkButton14_Click">Branch Wise Bill Detail</asp:LinkButton></td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">6.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton3" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton3_Click">District Wise Total WHR,Receiving & Pending</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">7.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton8" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton8_Click1">Wheat-PSS Gain Menual Entry Report</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">8.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton108" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton108_Click">Mapping For Rackpoint Godown Wise</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">9.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton109" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton109_Click">Pending List of Warehouses for Updating Weighbridge Information</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">9.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton110" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton110_Click">WMS के FCI से इंटिग्रेशन हेतु स्टेक Updation की स्थति</asp:LinkButton>
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>

    <img alt="New" src="images/new6.gif" id="new" runat="server" />

    <asp:Panel ID="pnllogin" class="popup" runat="server">
        <div class="pop" style="background-color: #FFFFCC0">
            <div class="row">
                <img visible="true" runat="server" src="~/images/close-icon.png" alt="quit" class="x" id="x" />
            </div>
            <div class="row" style="margin-bottom: 20px;">
                <div class="col-md-4 mg-t-20">
                    <div class="bg-royal rounded overflow-hidden zoom">
                        <div class="pd-x-20 pd-t-20 d-flex align-items-center">
                            <label style="color: blue">
                                Pending Bill for Passing Order(Acc. Mgr.)</label>
                            <hr />
                            <div class="mg-l-20">
                                <a href="Region/Reports/Rpt_Pendency_at_RM.aspx" target="_blank">
                                    <center>
                                        <u>

                                            <span class="count" id="PendingBillforPassing" runat="server" style="font-familyigital-7 !important; font-size: 30px; color: red;">00</span>

                                        </u>
                                        <div>
                                            <span class="count1" id="PendingAmtforPassing" runat="server" style="font-familyigital-7 !important; font-size: 30px; color: red; font-weight: bold;">00</span><span style="font-familyigital-7 !important; font-size: 30px; color: red;"></span>

                                        </div>
                                    </center>
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-4 mg-t-20">
                    <div class="bg-flickr rounded overflow-hidden zoom">
                        <div class="pd-x-20 pd-t-20 d-flex align-items-center">
                            <label style="color: blue">
                                Pending Bill for DSC(Acc. Mgr.)</label>
                            <hr />
                            <div class="mg-l-20">
                                <a href="Region/Reports/Rpt_Pendency_at_RM.aspx" target="_blank">
                                    <center>
                                        <u>

                                            <span class="count" id="BillPendingAGMDSC" runat="server" style="font-familyigital-7 !important; font-size: 30px; color: red;">00</span>

                                        </u>
                                        <div>
                                            <span class="count1" id="AmtPendingAGMDSC" runat="server" style="font-familyigital-7 !important; font-size: 30px; color: red; font-weight: bold;">00</span> <span style="font-familyigital-7 !important; font-size: 30px; color: red;"></span>

                                        </div>
                                    </center>
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- col-3 -->
                <div class="col-md-4 mg-t-20">
                    <div class="bg-crystal-clear rounded overflow-hidden zoom">
                        <div class="pd-x-20 pd-t-20 d-flex align-items-center">
                            <label style="color: blue; text-align: center;">
                                Pending Bill for DSC(RM)
                            </label>
                            <hr />
                            <div class="mg-l-20 ">
                                <center>
                                    <a href="Region/Reports/Rpt_Pendency_at_RM.aspx" target="_blank">
                                        <u>

                                            <span class="count" id="ApprovedBillbyAGM" runat="server" style="font-familyigital-7 !important; font-size: 30px; color: red;">00</span>
                                        </u>
                                        <div>

                                            <span class="count1" id="ApprovedAmtbyAGM" runat="server" style="font-familyigital-7 !important; font-size: 30px; color: red; font-weight: bold;">00</span><span style="font-familyigital-7 !important; font-size: 30px; color: red;"></span>

                                        </div>
                                    </a>
                                </center>
                            </div>
                        </div>

                    </div>
                </div>
                <!-- col-3 -->

                <!-- col-3 -->
            </div>
            <!-- col-3 -->
            <div class="row" style="margin-bottom: 20px;">

                <div class="col-md-4 mg-t-20">
                    <div class="bg-crystal-Opal rounded overflow-hidden zoom">
                        <div class="pd-x-20 pd-t-20 d-flex align-items-center">
                            <label style="color: blue">
                                Pending EPF for Generation
                            </label>
                            <hr />
                            <div class="mg-l-20 ">
                                <center>
                                    <a href="Region/Reports/Rpt_Pendency_at_RM.aspx" target="_blank">
                                        <u>

                                            <span class="count" id="PendingBillForNeft" runat="server" style="font-familyigital-7 !important; font-size: 30px; color: red;">00</span>
                                        </u>
                                        <div>

                                            <span class="count1" id="PendingAmtForNeft" runat="server" style="font-familyigital-7 !important; font-size: 30px; color: red; font-weight: bold;">00</span><span style="font-familyigital-7 !important; font-size: 30px; color: red;"></span>

                                        </div>
                                    </a>
                                </center>
                            </div>
                        </div>
                    </div>
                </div>

                <!-- col-3 -->


            </div>
        </div>
    </asp:Panel>
    <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="new" BackgroundCssClass="popup" PopupControlID="pnllogin" CancelControlID="x">
    </asp:ModalPopupExtender>

    <asp:AnimationExtender ID="popupAnimation" runat="server" TargetControlID="new">
        <Animations>
                <OnClick>
         <Parallel AnimationTarget="pnllogin" Duration="0.4" Fps="10">
            <FadeIn />
          </Parallel>
       </OnClick>
        </Animations>
    </asp:AnimationExtender>

    <!-- Modal -->

</asp:Content>

