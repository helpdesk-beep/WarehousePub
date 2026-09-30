<%@ Page Title="" Language="C#" MasterPageFile="~/Inspections/Masters/State.master" AutoEventWireup="true" CodeFile="Third_Party_Truti_Anupalan_Prativedan.aspx.cs" Inherits="Inspections_State_Third_Party_Truti_Anupalan_Prativedan" %>


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
        <h3 style="text-align: center;">मध्य प्रदेश वेयरहाउसिंग एवं लॉजिस्टिक्स कार्पोरेशन निरीक्षण अनुपालन प्रतिवेदन.</h3>
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
                                          
                    </Columns>
                </asp:GridView>

            </div>
           
            
        </div>
        <br />
        <div id="btnfinalsybmit" runat="server" visible="true">
            <div>
                <asp:Label ID="lblTQROReamark" runat="server" Text="Reamrk" ForeColor="Red">Reamrk by TQ RO:-</asp:Label>
                <asp:Label ID="txtremrktqro" TextMode="MultiLine" Width="100%" Height="150px" runat="server"></asp:Label>
            </div>
            <br />
            <div>
                <asp:Label ID="Label2" runat="server" Text="Reamrk" ForeColor="Red">Reamrk by TQ HO:-</asp:Label>
                <asp:Label ID="txtremarkTQHO" TextMode="MultiLine" Width="100%" Height="150px" runat="server"></asp:Label>
            </div>
           
                </div>
            <br />
    </div>

    <div class="container signin">
    </div>

</asp:Content>

