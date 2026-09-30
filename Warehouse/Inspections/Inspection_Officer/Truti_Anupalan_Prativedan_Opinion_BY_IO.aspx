<%@ Page Title="" Language="C#" MasterPageFile="~/Inspections/Masters/Inspection_Officer.master" AutoEventWireup="true" CodeFile="Truti_Anupalan_Prativedan_Opinion_BY_IO.aspx.cs" Inherits="Inspections_Truti_Anupalan_Prativedan_Opinion_BY_IO" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        body {
            font-family: Arial, Helvetica, sans-serif;
            /*background-color: black;*/
        }

        * {
            box-sizing: border-box;
        }

        /* Add padding to containers */
        .container {
            padding: 16px;
            background-color: white;
        }

        /* Full-width input fields */
        .tt {
            width: 100%;
            padding: 15px;
            margin: 5px 0 22px 0;
            display: inline-block;
            border: none;
            background: #f1f1f1;
        }

        .ttt {
            background-color: #ddd;
            outline: none;
        }

        /* Overwrite default styles of hr */
        hr {
            border: 1px solid #f1f1f1;
            margin-bottom: 25px;
        }

        /* Set a style for the submit button */
        .btn {
            background-color: #4CAF50;
            color: white;
            padding: 16px 20px;
            margin: 8px 0;
            border: none;
            cursor: pointer;
            width: 100%;
            opacity: 0.9;
        }

            .btn:hover {
                opacity: 1;
            }

        /* Add a blue text color to links */
        a {
            color: dodgerblue;
        }

        /* Set a grey background color and center the text of the "sign in" section */
        .signin {
            background-color: #f1f1f1;
            text-align: center;
        }
    </style>
    <div class="container">
        <h1 style="text-align: center;">मध्य प्रदेश वेयरहाउसिंग एवं लॉजिस्टिक्स कार्पोरेशन क्षैं.का.<asp:Label ID="lblregionname" runat="server"></asp:Label></h1>
        <h1 style="text-align: center;">निरीक्षण अनुपालन प्रतिवेदन.</h1>
        <asp:Label ID="Label3" runat="server"><b>निरीक्षण अधिकारी का नाम</b></asp:Label>
        -
        <asp:Label ID="lblinspectionofficername" runat="server"></asp:Label>
        <br />
        <asp:Label ID="Label5" runat="server"><b>निरीक्षण अवधि</b></asp:Label>
        -
        <asp:Label ID="lblavdhi" runat="server"></asp:Label>
        <br />
        <asp:Label ID="Label1" runat="server"><b>शाखा का नाम</b></asp:Label>
        -
        <asp:Label ID="lblbranch" runat="server"></asp:Label>
        <hr>
        <b style="color: red;">नोट - एक बार में एक ही विसंगति की एंट्री करे , एक विसंगति की एंट्री सेव करने के बाद दूसरी एंट्री करे आप इसमें अपने हिसाब से जितनी चाहे एंट्री कर सकते है </b>

        <div class="row" id="grd" runat="server" visible="false">
            <div class="col-lg-12">
                <h5 class="bd" style="color: Red;">निरीक्षण में पाई गई विसंगतियों की जानकारी :-</h5>
                <asp:GridView ID="GrdInsp" runat="server" AutoGenerateColumns="false" Visible="true" OnRowUpdating="GrdInsp_RowUpdating">
                    <Columns>
                        <asp:TemplateField HeaderText="S.No." ItemStyle-Width="5%">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                                <asp:HiddenField ID="CIT_ID" runat="server" Value='<%#Eval("ID") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="निरीक्षण में पाई गई विसंगतियां">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="txtRemark" placeholder="रिमार्क" Text='<%# Eval("Error_Details_By_IO")%>' TextMode="MultiLine"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="शाखा प्रबंधक का अनुपालन">
                            <ItemTemplate>
                                <asp:Label ID="txtanupalanbybm" runat="server" Text='<%# Eval("Branch_manager_compliance")%>' placeholder="शाखा प्रबंधक का अनुपालन" TextMode="MultiLine" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="निरीक्षण प्रबंधक का अनुपालन" ItemStyle-Width="30%">
                            <ItemTemplate>
                                <asp:TextBox ID="txtIMC" runat="server" Width="100%" CssClass="tt ttt" Text='<%# Eval("Inspection_officer_opinion")%>' placeholder="शाखा प्रबंधक का अनुपालन" TextMode="MultiLine" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Submit">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnUpdate" Text="Submit" class="btn btn-small btn-info" runat="server" CommandName="Update" OnClientClick="return confirm('कृपया अपनी कार्रवाई की पुष्टि करें')" />
                            </ItemTemplate>
                            <ItemStyle Width="5%" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

            </div>
            <div class="row">
                <div class="col-lg-3">
                    <center>
                             
                                        <asp:Button runat="server" ID="btnfinalsubmit"  Text="Final Submit to RM Office" CssClass="btn btn-warning" OnClick="btnfinalsubmit_Click"/>
                                 
                                    </center>
                </div>
            </div>
        </div>
    </div>

    <div class="container signin">
    </div>
    <asp:HiddenField ID="inspid" runat="server" Value="0" />
</asp:Content>

