<%@ Page Language="C#" MasterPageFile="~/MasterPage/PrivateWarehouse.master" AutoEventWireup="true" CodeFile="Godown_Compalint.aspx.cs" Inherits="WarehouseLevel_PvtGodownProfile" Title="Pvt Godown Profile" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        * {
            box-sizing: border-box;
        }

        input[type=text], select, textarea {
            width: 100%;
            padding: 12px;
            border: 1px solid rgb(70, 68, 68);
            border-radius: 4px;
            resize: vertical;
        }

        input[type=email], select, textarea {
            width: 100%;
            padding: 12px;
            border: 1px solid rgb(70, 68, 68);
            border-radius: 4px;
            resize: vertical;
        }

        label {
            padding: 12px 12px 12px 0;
            display: inline-block;
        }

        input[type=submit] {
            background-color: rgb(37, 116, 161);
            color: white;
            padding: 12px 20px;
            border: none;
            border-radius: 4px;
            cursor: pointer;
            float: right;
        }

            input[type=submit]:hover {
                background-color: #45a049;
            }

        .container {
            border-radius: 5px;
            background-color: #f2f2f2;
            padding: 20px;
        }

        .col-25 {
            float: left;
            width: 25%;
            margin-top: 6px;
        }

        .col-75 {
            float: left;
            width: 75%;
            margin-top: 6px;
        }

        /* Clear floats after the columns */
        .row:after {
            content: "";
            display: table;
            clear: both;
        }

        /* Responsive layout - when the screen is less than 600px wide, make the two columns stack on top of each other instead of next to each other */
    </style>
    <fieldset style="width: 1000px; border: 2px solid navy;">
        <center>
            <div>
                <table cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr style="background-color: #066; height: 80px">
                        <td colspan="4" align="center" style="color:red;">
                            <asp:Label ID="Label8" runat="server" Text="जिन गोदामों में लम्बे समय से स्कन्द रखा हुआ हैं और यदि किसी गोदाम से जल्दी निकासी की जा रही हैं तो आप इस सम्बन्ध में निचे दिए गए बॉक्स के माध्यम से  आप अपनी शिकायत उच्य स्तर पर आप अपनी शिकायत भेज सकते हैं" 
                                ForeColor="White"
                                Font-Size="12pt"></asp:Label>
                        </td>
                    </tr>
                </table>
                <div class="row">
                    <div class="col-25" style="padding-top:50px;">
                        <asp:Label id="lblms" runat="server" Font-Size="12pt" Text="शिकायत / आपत्ति" ></asp:Label>
                    </div>
                    <div class="col-75">
                        <textarea id="txtremark" runat="server" name="subject" placeholder="Write something.." style="height: 200px"></textarea>
                    </div>
                </div>
                <div class="row">
                     <div class="col-25">
                    </div>
                     <div class="col-25">
                       <asp:Button ID="btnsave" runat="server" Text="शिकायत दर्ज करे" OnClick="btnsave_Click" />
                    </div>
                    
                </div>
                <div>
                     <asp:GridView runat="server" ID="GrdOfficerPreviousInsp" 
                        AutoGenerateColumns="false" CssClass="Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No.">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1%>
                                </ItemTemplate>
                                <ItemStyle Width="1%" />
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Region">
                                <ItemTemplate>
                                    <asp:Label ID="lblRegionnm" runat="server" Text='<%# Eval("Regionnm") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="District Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblDistrict_Name" runat="server" Text='<%# Eval("District_Name") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Branch">
                                <ItemTemplate>
                                    <asp:Label ID="lblDepotName" runat="server" Text='<%# Eval("DepotName") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Godown Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblGodown_Name" runat="server" Text='<%# Eval("Godown_Name") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date of Complaint">
                                <ItemTemplate>
                                    <asp:Label ID="lblComplaintDate" runat="server" Text='<%# Eval("ComplaintDate") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remark">
                                <ItemTemplate>
                                    <asp:Label ID="lblRemark" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                        </Columns>
                        <%--<FooterStyle BackColor="#719cb6" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" />--%>
                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" Height="20px" Font-Size="10pt" />
                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#F7ECDD" Font-Bold="True" ForeColor="#cb4e48" HorizontalAlign="center"
                            Height="20px" Font-Size="10pt" />
                        <AlternatingRowStyle BackColor="#eeeeee" />
                    </asp:GridView>
                </div>
            </div>
        </center>
    </fieldset>
    &nbsp;
</asp:Content>
