<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/StateMaster.master" AutoEventWireup="true" CodeFile="~/StatePages/UpdateGodownDetailsByBM.aspx.cs" Inherits="StatePages_UpdateGodownDetailsByBM" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="<%= ResolveUrl("~/NEW_CSS/js/jquery-1.12.4.js") %>"></script>
    <script src="../assets/New/js/select2.min.js"></script>
    <link href="../assets/New/css/select2.min.css" rel="stylesheet" />
     <script>
         $(function () {
             $("[id*=DropDownList1]").select2();
             $("[id*=ddlBranch]").select2();
         });
     </script>
    <style type="text/css">
        body {
            font-family: Arial, sans-serif;
        }

        .header-title {
            text-align: center;
            font-size: 15px;
            font-weight: bold;
            text-transform: uppercase;
            color: blue;
            margin-bottom: 5px;
        }

        .header-note {
            text-align: center;
            font-size: 15px;
            font-weight: bold;
            text-transform: uppercase;
            color: red;
            margin-bottom: 20px;
        }

        .msg-label {
            text-align: center;
            font-size: 30px;
            font-weight: bold;
            color: red;
            margin-bottom: 20px;
        }

        .form-label {
            font-weight: bold;
            color: navy;
            font-size: 11pt;
        }

        .form-input {
            width: 150px;
            padding: 5px;
            border-radius: 4px;
            border: 1px solid #ccc;
        }

        .btn-blue {
            background-color: #007bff;
            color: white;
            padding: 5px 15px;
            border-radius: 4px;
            border: none;
            cursor: pointer;
            margin: 5px;
        }

            .btn-blue:hover {
                background-color: #0056b3;
            }

        .Grid {
            width: 100%;
            border-collapse: collapse;
            font-size: 18px;
        }

            .Grid th {
                background-color: #2FBDF1;
                color: white;
                padding: 8px;
                border: 1px solid #ccc;
            }

            .Grid td {
                padding: 8px;
                border: 1px solid #ccc;
                text-align: center;
            }

        .alt {
            background-color: #f2f2f2;
        }

        .pgr {
            font-weight: bold;
            text-align: center;
        }

        .modalBackground {
            background-color: rgba(0,0,0,0.6);
        }

        .modalPopup {
            background-color: #FFFFFF;
            width: 80%;
            max-width: 800px;
            border: 3px solid #0DA9D0;
            border-radius: 6px;
            padding: 20px;
            margin: auto;
        }

            .modalPopup .header {
                background-color: #2FBDF1;
                height: 40px;
                color: White;
                width: 100%;
                line-height: 40px;
                text-align: center;
                font-weight: bold;
                border-top-left-radius: 6px;
                border-top-right-radius: 6px;
                margin-bottom: 15px;
            }

            .modalPopup .body {
                min-height: 50px;
                font-weight: bold;
                margin-bottom: 15px;
            }

            .modalPopup .footer {
                text-align: center;
                padding: 10px;
            }

            .modalPopup .yes, .modalPopup .no {
                height: 30px;
                color: White;
                line-height: 30px;
                text-align: center;
                font-weight: bold;
                cursor: pointer;
                border-radius: 4px;
                padding: 0 10px;
                margin: 5px;
            }

            .modalPopup .yes {
                background-color: #2FBDF1;
                border: 1px solid #0DA9D0;
            }

            .modalPopup .no {
                background-color: #9F9F9F;
                border: 1px solid #5C5C5C;
            }

        .container {
            width: 95%;
            margin: auto;
        }

        .row {
            display: flex;
            flex-wrap: wrap;
            margin-bottom: 15px;
        }

        .col-lg-6 {
            flex: 0 0 50%;
            max-width: 50%;
            padding-right: 10px;
            padding-left: 10px;
        }

        label {
            display: block;
            margin-bottom: 5px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="header-title">Update Godown Capacity, Storage Capcity, Vacant Capacity, Latitude, Longitude And Godown Status(YES/No)</div>
    <div class="header-note">Note:- ऐसे गोदाम जिनका किराया भुगतान कर दिया गया हैं और वर्तमान में उसमे कोई भी स्कंध नहीं रखा हैं ऐसे गोदामों का स्टेटस "No" करे </div>
    <div class="msg-label">
        <asp:Label ID="lblmsg" runat="server"></asp:Label>
    </div>

    <asp:HiddenField ID="ddd" runat="server" />
    <asp:Panel ID="StoreGrid" runat="server">
        <asp:HiddenField ID="hdngdnid" runat="server" />
        <asp:HiddenField ID="Hiddendistid" runat="server" />
        <asp:HiddenField ID="Hiddenbranch" runat="server" />
        <asp:HiddenField ID="EnterHiddendistid" runat="server" />

        <div style="width: 95%; margin: auto; display: flex; flex-wrap: wrap; justify-content: space-between; margin-bottom: 20px;">
            <div style="flex: 0 0 48%; display: flex; flex-direction: column; margin-bottom: 10px;">
                <label for="DropDownList1" style="font-weight: bold; color: navy; margin-bottom: 5px;">District :</label>
                <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true"
                    Style="width: 100%; height: 30px; padding: 5px; border-radius: 4px; border: 1px solid #ccc;"
                    OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
            <div style="flex: 0 0 48%; display: flex; flex-direction: column; margin-bottom: 10px;">
                <label for="ddlBranch" style="font-weight: bold; color: navy; margin-bottom: 5px;">Branch :</label>
                <asp:DropDownList ID="ddlBranch" runat="server" AutoPostBack="true"
                    Style="width: 100%; height: 30px; padding: 5px; border-radius: 4px; border: 1px solid #ccc;"
                    OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
        </div>


        <asp:GridView ID="GridView1" CssClass="Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr" runat="server" AutoGenerateColumns="false" Visible="true">
            <Columns>
                <asp:TemplateField HeaderText="S.N.">
                    <ItemTemplate><%#Container.DataItemIndex+1%></ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Godown_ID" HeaderText="Godown_ID" />
                <asp:BoundField DataField="Godown_Name" HeaderText="Godown_Name" />
                <asp:BoundField DataField="Hired_Type" HeaderText="Hired_Type" />
                <asp:TemplateField HeaderText="Godown Max Capacity">
                    <ItemTemplate>
                        <asp:Label ID="Godown_Max_Capacity" runat="server" Text='<%# (Eval("Godown_Capacity","{0:N0}"))%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Godown Scientific Capacity">
                    <ItemTemplate>
                        <asp:Label ID="Godown_Scientific_Capacity" runat="server" Text='<%# (Eval("Godown_Scintific_Capacity","{0:N0}"))%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Godown Vacant Capacity">
                    <ItemTemplate>
                        <asp:Label ID="Godown_Vacant_Capacity" runat="server" Text='<%# (Eval("Vacant_Capacity","{0:N0}"))%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Latitude">
                    <ItemTemplate>
                        <asp:Label ID="Latitude" runat="server" Text='<%# (Eval("Latitude"))%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Longitude">
                    <ItemTemplate>
                        <asp:Label ID="Longitude" runat="server" Text='<%# (Eval("Longitude"))%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="IsActive" HeaderText="Status" />
                <asp:TemplateField HeaderText="Edit">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkEdit" runat="server" ForeColor="Blue" Text="Update" OnClick="Edit"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </asp:Panel>

    <asp:Panel ID="pnlAddEdit" runat="server" CssClass="modalPopup" Style="display: none;">
        <div class="header">Godown Details</div>
        <div class="body">
            <table align="center">
                <tr>
                    <td colspan="2" style="text-align: center; font-size: 20px; font-weight: bold;">Godown Info</td>
                </tr>
                <tr>
                    <td>Godown_Id -:
                        <asp:Label ID="Label8" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>Godown_Name -:
                        <asp:Label ID="Label9" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td><span class="form-label">Godown Capacity (MT):</span></td>
                    <td>
                        <asp:TextBox ID="txtVacantCapacity" runat="server" CssClass="form-input" onkeypress="return NumberOnly(event)"></asp:TextBox></td>
                </tr>
                <tr>
                    <td><span class="form-label">Godown Scientific Capacity (MT):</span></td>
                    <td>
                        <asp:TextBox ID="txtUnloadCapacity" runat="server" CssClass="form-input" onkeypress="return NumberOnly(event)"></asp:TextBox></td>
                </tr>
                <tr>
                    <td><span class="form-label">Latitude:</span></td>
                    <td>
                        <asp:TextBox ID="TxtLatitude" runat="server" CssClass="form-input" onkeypress="return NumberOnly(event)"></asp:TextBox></td>
                </tr>
                <tr>
                    <td><span class="form-label">Longitude:</span></td>
                    <td>
                        <asp:TextBox ID="TextLongitude" runat="server" CssClass="form-input" onkeypress="return NumberOnly(event)"></asp:TextBox></td>
                </tr>
                <tr>
                    <td><span class="form-label">Godown Status (YES/NO):</span></td>
                    <td>
                        <asp:DropDownList ID="Godownflag" runat="server" CssClass="form-input">
                            <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                            <asp:ListItem Text="YES" Value="Y"></asp:ListItem>
                            <asp:ListItem Text="NO" Value="N"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center;">
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn-blue" OnClick="btnSave_Click" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn-blue" OnClientClick="return Hidepopup()" />
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>

    <asp:LinkButton ID="lnkFake" runat="server" Style="display: none;"></asp:LinkButton>
    <asp:ModalPopupExtender ID="popup" runat="server" DropShadow="false" PopupControlID="pnlAddEdit" TargetControlID="lnkFake" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>

</asp:Content>
