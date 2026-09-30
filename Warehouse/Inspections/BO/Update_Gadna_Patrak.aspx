<%@ Page Title="" Language="C#" MasterPageFile="~/Inspections/Masters/Inspection_BO.master" AutoEventWireup="true" CodeFile="Update_Gadna_Patrak.aspx.cs" Inherits="Inspections_BO_Owned_Edit_Bolck_Wise_Entry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css">
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.8.3/themes/base/jquery-ui.css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="http://code.jquery.com/ui/1.8.3/jquery-ui.js"></script>
    <style type="text/css">
        .wrap {
            margin: 0 auto;
            width: 960px;
            -moz-box-shadow: 0px 5px 23px #000;
            -webkit-box-shadow: 0px 5px 23px #000;
            box-shadow: 0px 5px 23px #000;
        }

        input.submit {
            color: #fff;
            padding: 7px 10px;
            border: 0;
            font-weight: bold;
            background: #777;
            border-radius: 25px;
        }

        input.text {
            border: 2px solid rgb(173, 204, 204);
            height: 20px;
            width: 223px;
            font-size: 16px;
            box-shadow: 0px 0px 27px rgb(204, 204, 204) inset;
            transition: 500ms all ease;
            padding: 3px 3px 3px 3px;
        }
    </style>
    <style type="text/css">
        .button {
            background-color: #4CAF50; /* Green */
            border: none;
            color: white;
            padding: 0px 0px;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            font-size: 12px;
            font-weight: bold;
            margin: 4px 2px;
            -webkit-transition-duration: 0.4s; /* Safari */
            transition-duration: 0.4s;
            cursor: pointer;
        }

        .button1 {
            background-color: white;
            color: black;
            border: 2px solid #4CAF50;
        }

            .button1:hover {
                background-color: #4CAF50;
                color: white;
            }

        .button2 {
            background-color: white;
            color: black;
            border: 2px solid #008CBA;
        }

            .button2:hover {
                background-color: #008CBA;
                color: white;
            }

        .button3 {
            background-color: white;
            color: black;
            border: 2px solid #f44336;
        }

            .button3:hover {
                background-color: #f44336;
                color: white;
            }

        .button6 {
            background-color: white;
            color: black;
            border: 2px solid #008CBA;
        }

            .button6:hover {
                background-color: #008CBA;
                color: white;
            }

        .style1 {
            height: 30px;
        }
    </style>
    <div style="background-color: #FDFAF7; width: 100%;">
        
        <table align="center" style="width: 100%; border: #008CBA; border-style: solid; border-width: 0px;">
            <tr>
                <td align="center" style="border: #E6C79D; border-style: solid; border-width: 2px;" colspan="8">
                    <span style="color: #cb4e48; font-weight: bold; font-size: 17px">स्टेक प्लानिंग बिछान </span>
                </td>
            </tr>
            <tr>
                <td align="left" colspan="8">
                    <span style="color: #cb4e48; font-weight: bold; font-size: 17px">कोई भी फील्ड को खाली नहीं छोड़े , यदि कोई जानकारी नहीं हैं तो शून्य अवश्य डाले </span>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label9" runat="server" Text="लम्बाई : "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtLendth" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)" OnTextChanged="txtLendth_TextChanged" AutoPostBack="true"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="चौड़ाई : "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtwidth" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)" OnTextChanged="txtwidth_TextChanged" AutoPostBack="true"></asp:TextBox>

                </td>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="अतिरिक्त : "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtextralendth" runat="server" onkeypress="return isNumberKey(event)" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtextralendth_TextChanged"></asp:TextBox>
                </td>

                <td>
                    <asp:Label ID="Label3" runat="server" Text="योग (ल. + चौ.+अति.): "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTotal" runat="server" CssClass="form-control"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label5" runat="server" Text="बोरो के लेयर की ऊंचाई : "></asp:Label>
                </td>

                <td>
                    <asp:TextBox ID="txtheight" runat="server" onkeypress="return isNumberKey(event)" CssClass="form-control" OnTextChanged="txtheight_TextChanged" AutoPostBack="true"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="ब्लॉक क्र./संख्या : "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtnoofblock" runat="server" onkeypress="return isNumberKey(event)" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtnoofblock_TextChanged"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label6" runat="server" Text="बोरियो की संख्या (योग*बोरो के लेयर की ऊंचाई*ब्लॉक क्र./संख्या) : "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="lbltotalbags" runat="server" CssClass="form-control"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left" colspan="8">
                    <span style="color: #cb4e48; font-weight: bold; font-size: 17px">अतिरिक्त पाई गई बोरियो की संख्या </span>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label8" runat="server" Text="ऊपर : "></asp:Label>
                </td>

                <td>
                    <asp:TextBox ID="txtup" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)" OnTextChanged="txtup_TextChanged" AutoPostBack="true"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label10" runat="server" Text="निचे : "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtbelow" runat="server" CssClass="form-control" AutoPostBack="true" onkeypress="return isNumberKey(event)" OnTextChanged="txtbelow_TextChanged"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label12" runat="server" Text="टोटल बौरे : "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txttotalnoofbags" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label7" runat="server" Text="Spillage Bag : "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtspillagebag" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label11" runat="server" Text="Remark : "></asp:Label>
                </td>
                <td colspan="5">
                    <asp:TextBox ID="txtremark" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="10" align="center">
                    <asp:Button class="button button2" ID="btnupdate" runat="server" Text="Update"
                        TabIndex="11" CssClass="btn btn-warning" OnClick="btnupdate_Click"></asp:Button>
                    <asp:Label ID="Label79" ForeColor="Red" runat="server" Visible="False"></asp:Label>
                </td>
            </tr>
        </table>
        <div class="row">
            <div class="col-lg-12">
            </div>
        </div>
    </div>

    <script type="text/javascript">

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode != 46 && charCode > 31
                && (charCode < 48 || charCode > 57)) {
                alert("This field will not accept the alphabet, Please Enter Only number");
                return false;
            }
            return true;
        }
        //
    </script>
</asp:Content>

