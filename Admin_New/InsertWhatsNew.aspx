<%@ Page Title="" Language="C#" MasterPageFile="../MasterPages/adminMaster.master" AutoEventWireup="true" CodeFile="InsertWhatsNew.aspx.cs" Inherits="Admin_InsertWhatsNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PM" runat="Server">
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
    <script src="../NEW_CSS/js/jquery-1.12.4.js"></script>
    <script src="../NEW_CSS/js/jquery-ui.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>

    <script type="text/javascript">

        $(function () {
            from = $('#<%=releaseDate.ClientID%>')
            .datepicker({
                dateFormat: 'dd/mm/yy',
                defaultDate: "+1w",
                changeMonth: true,
                changeYear: true,
                minDate: new Date("01/01/2011"),
                maxDate: "0",
                onSelect: function (selected) {
                    $('#<%=expireDate.ClientID%>').datepicker("option", "minDate", selected)
                }


            })
				.on("change", function () {
				    to.datepicker("option", "minDate", getDate(this));

				}),
			to = $('#<%=expireDate.ClientID%>').datepicker({
			    dateFormat: 'dd/mm/yy',
			    defaultDate: "+1w",
			    changeMonth: true,
			    changeYear: true,
			    minDate: new Date("01/23/2011"),
			    //	maxDate: "0",
			    onSelect: function (selected) {
			        if ($('#<%=releaseDate.ClientID%>').datepicker("getDate") == null) {
			            alert("From date should not be empty!!!");
			        };
			        $('#<%=releaseDate.ClientID%>').datepicker("option", "maxDate", selected);

			    }

			})
			.on("change", function () {
			    from.datepicker("option", "maxDate", getDate(this));
			});

            function getDate(element) {
                var date;
                try {
                    date = $.datepicker.parseDate(dateFormat, element.value);
                } catch (error) {
                    date = null;
                }

                return date;
            }
        });
    </script>
    <div class="alert-warning img-thumbnail" style="margin-bottom: 10px!important;">
        <b style="font-size: large; font-family: 'Times New Roman', Times, serif">Insert What's New?</b>
    </div>
    <div>

        <table style="width: 100%;">
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblErr" runat="server" Font-Bold="true"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style1">Select Document Type:</td>
                <td class="style2">
                    <asp:DropDownList ID="ddlDocType" runat="server" Width="180px" AutoPostBack="true">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rvDocType" runat="server"
                        ControlToValidate="ddlDocType" ErrorMessage="Please Select Document Type" ForeColor="#CC3300"
                        ValidationGroup="myValidator"></asp:RequiredFieldValidator>
                </td>
                <td></td>
            </tr>
            <tr>
                <td class="style1">Document Title :</td>
                <td class="style2">
                    <textarea runat="server" id="txtTitleHn" width="80%" style="width: 800px; height: 110px;"></textarea>
                    <asp:RequiredFieldValidator ID="rvTitle" runat="server"
                        ControlToValidate="txtTitleHn" ErrorMessage="Please Insert Title in Hindi" ForeColor="#CC3300"
                        ValidationGroup="myValidator"></asp:RequiredFieldValidator>
                </td>
                <td></td>
            </tr>

            <tr>
                <td>Document Release Date:</td>

                <td>
                    <input type="text" runat="server" id="releaseDate" name="releaseDate" maxlength="10" style="width: 176px!important;" />
                    <asp:RequiredFieldValidator ID="rvDatepicker" runat="server"
                        ControlToValidate="releaseDate" ErrorMessage="Please Insert Release Date" ForeColor="#CC3300"
                        ValidationGroup="myValidator"></asp:RequiredFieldValidator>

                </td>
            </tr>
            <tr>
                <td>Document Expire Date:</td>

                <td>

                    <input type="text" runat="server" id="expireDate" name="expireDate" maxlength="10" style="width: 176px!important;" />
                </td>
            </tr>
            <tr>
                <td class="style1">Attachment File :</td>
                <td class="style2">
                    <asp:FileUpload ID="FileUpload1" runat="server" />
                    <asp:RequiredFieldValidator ID="rvFileUpload1" runat="server"
                        ControlToValidate="FileUpload1" ErrorMessage="Please Insert File" ForeColor="#CC3300"
                        ValidationGroup="myValidator"></asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:Label ID="Label1" runat="server"></asp:Label>
                </td>
            </tr>

            <tr>
                <td class="style1"></td>
                <td class="style2">
                    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Upload"
                        ValidationGroup="myValidator" />
                </td>
                <td></td>
            </tr>
        </table>

    </div>
</asp:Content>

