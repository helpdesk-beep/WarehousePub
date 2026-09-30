<%@ Page Title="" Language="C#" MasterPageFile="~/Inspections/Masters/Inspection_Officer.master" AutoEventWireup="true" CodeFile="Truti_Anupalan_Prativedan.aspx.cs" Inherits="Inspections_Inspection_Officer_Truti_Anupalan_Prativedan" %>


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
        <br />
        <asp:Label ID="lblemail" runat="server"><b>निरीक्षण में पाई गई विसंगतियों की जानकारी</b></asp:Label>
        <asp:TextBox ID="txttruti" runat="server" CssClass="tt ttt" placeholder="निरीक्षण में पाई गई विसंगतियों की जानकारी" TextMode="MultiLine" />

        <div class="row">
            <div class="col-lg-3">
                <asp:Button ID="btnsave" Text="Register" runat="server" class="btn" OnClick="btnsave_Click"></asp:Button>
            </div>
        </div>
        <div class="row" id="grd" runat="server" visible="false">
            <div class="col-lg-12">
                <h5 class="bd" style="color: Red;">निरीक्षण में पाई गई विसंगतियों की जानकारी :-</h5>
                <asp:GridView ID="GrdInsp" runat="server" AutoGenerateColumns="false" Visible="true">
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
                    </Columns>
                </asp:GridView>

            </div>
            <div class="row">
                <div class="col-lg-3">
                    <center>
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>      
                                        <asp:Button runat="server" ID="btnfinalsubmit"  Text="Final Submit to BM" CssClass="btn btn-warning" OnClick="btnfinalsubmit_Click"/>
                                 </ContentTemplate>
                                            </asp:UpdatePanel>
                                    </center>
                </div>
            </div>
        </div>
    </div>

    <div class="container signin">
    </div>

</asp:Content>

