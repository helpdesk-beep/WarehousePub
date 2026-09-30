<%@ Page Title="" Language="C#" MasterPageFile="~/Inspections/Masters/State.master" AutoEventWireup="true" CodeFile="DeleteScheduleInspection.aspx.cs" Inherits="Inspections_State_DeleteScheduleInspection" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        window.history.forward();

        function noBack() { window.history.forward(); }
    </script>
    <script type="text/javascript">
        Cufon.replace('h1,h2,h3,h4,h5,#menu,#copy,.blog-date');
    </script>
    <style type="text/css">
        ul.svertical {
            width: 220px; /* width of menu */
            overflow: auto;
            background: #f4f4f4; /* background of menu */
            margin: 0;
            padding: 0;
            padding-top: 7px; /* top padding */
            list-style-type: none;
        }

            ul.svertical li {
                text-align: right; /* right align menu links */
            }

                ul.svertical li a {
                    position: relative;
                    display: inline-block;
                    text-indent: 5px;
                    overflow: hidden;
                    background: rgb(1, 138, 180); /* initial background color of links */
                    font: bold 16px Germand;
                    text-decoration: none;
                    padding: 5px;
                    margin-bottom: 5px; /* spacing between links */
                    color: White;
                    -moz-box-shadow: inset -7px 0 5px rgba(114,114,114, 0.8); /* inner right shadow added to each link */
                    -webkit-box-shadow: inset -7px 0 5px rgba(114,114,114, 0.8);
                    box-shadow: inset -7px 0 5px rgba(114,114,114, 0.8);
                    -moz-transition: all 0.2s ease-in-out; /* CSS3 transition of hover properties */
                    -webkit-transition: all 0.2s ease-in-out;
                    -o-transition: all 0.2s ease-in-out;
                    -ms-transition: all 0.2s ease-in-out;
                    transition: all 0.2s ease-in-out;
                }

                    ul.svertical li a:hover {
                        padding-right: 30px; /* add right padding to expand link horizontally to the left */
                        color: Black;
                        background: rgb(153,249,75);
                        -moz-box-shadow: inset -3px 0 2px rgba(114,114,114, 0.8); /* contract inner right shadow */
                        -webkit-box-shadow: inset -3px 0 5px rgba(114,114,114, 0.8);
                        box-shadow: inset -3px 0 5px rgba(114,114,114, 0.8);
                    }

                    ul.svertical li a:before { /* CSS generated content: slanted right edge */
                        content: "";
                        position: absolute;
                        left: 0;
                        top: 0;
                        border-style: solid;
                        border-width: 70px 0 0 20px; /* Play around with 1st and 4th value to change slant degree */
                        border-color: transparent transparent transparent #f4f4f4; /* change black to match the background color of the menu UL */
                    }

        /*print*/


        * {
            box-sizing: border-box;
            -moz-box-sizing: border-box;
        }

        .page {
            width: 21cm;
            min-height: 29.7cm;
            padding: 2cm;
            margin: 1cm auto;
            border: 1px #D3D3D3 solid;
            border-radius: 5px;
            background: white;
            box-shadow: 0 0 5px rgba(0, 0, 0, 0.1);
        }

        .subpage {
            padding: 1cm;
            border: 5px red solid;
            height: 237mm;
            outline: 2cm #FFEAEA solid;
        }

        @page {
            size: A4;
            margin: 0;
            font-size: smaller;
        }

        @media print {
            .page {
                margin: 0;
                border: initial;
                border-radius: initial;
                width: initial;
                min-height: initial;
                box-shadow: initial;
                background: initial;
                page-break-after: always;
                font-size: smaller;
            }
        }

        @media print {
            html, body {
                width: 210mm;
                height: 297mm;
                font-size: smaller;
            }
            /* ... the rest of the rules ... */
        }
        /*td{font-size:smaller;}*/
        page[size="A4"] {
            background: white;
            width: 21cm;
            height: 29.7cm;
            display: block;
            margin: 0 auto;
            margin-bottom: 0.5cm;
            box-shadow: 0 0 0.5cm rgba(0,0,0,0.5);
        }

        @media print {
            body, page[size="A4"] {
                margin: 0;
                box-shadow: 0;
                font-size: smaller;
            }
        }

        .style7 {
            height: 15px;
        }

        .style8 {
            height: 20px;
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
    </style>

    <script type="text/javascript">

        $(document).ready(function () {

            $("#Panel1").hide();

            $("#Button1").click(function () {

                $("#Panel1").show();

            });

        });
    </script>
    <script type="text/javascript">
        function preventInput(evnt) {
            //Checked In IE9,Chrome,FireFox
            if (evnt.which != 9) evnt.preventDefault();
        }
    </script>
    <script type="text/javascript" language="javascript">
        function ConfirmOnDelete() {
            if (confirm("Are you sure want to Delete This Inspection ?") == true)
                return true;
            else
                return false;
        }
    </script>
    <%--Update Start--%>

    <script type="text/javascript">
        // Page load pe textbox clear aur grid reset
        window.onload = function () {
            // Clear the search textbox
            var input = document.getElementById("<%= txtSearch_IsnpOff.ClientID %>");
        if (input) {
            input.value = "";
        }

        // Reset grid display
        var table = document.getElementById("<%= Gridview_IsnpOff.ClientID %>");
        if(table) {
            var trs = table.getElementsByTagName("tr");
            for(var i = 1; i < trs.length; i++) { // skip header
                trs[i].style.display = "";
            }
        }
    }

    function filterGrid_IsnpOff() {
        var input = document.getElementById("<%= txtSearch_IsnpOff.ClientID %>");
        var filter = input.value.toLowerCase();

        var table = document.getElementById("<%= Gridview_IsnpOff.ClientID %>");
            var trs = table.getElementsByTagName("tr");

            for (var i = 1; i < trs.length; i++) {
                var display = false;
                var tds = trs[i].getElementsByTagName("td");

                for (var j = 0; j < tds.length; j++) {
                    var cell = tds[j];
                    if (cell && cell.textContent.toLowerCase().indexOf(filter) > -1) {
                        display = true;
                        break;
                    }
                }

                trs[i].style.display = display ? "" : "none";
            }
        }
    </script>


    <script type="text/javascript">
        function filterGrid_OfficerPrev() {
            // TextBox value
            var input = document.getElementById("<%= txtSearch_OfficerPrev.ClientID %>");
    var filter = input.value.toLowerCase();

    // GridView table
    var table = document.getElementById("<%= Gridview_OfficerPreviousInsp.ClientID %>");
            var trs = table.getElementsByTagName("tr");

            // Loop through rows (skip header row)
            for (var i = 1; i < trs.length; i++) {
                var display = false;
                var tds = trs[i].getElementsByTagName("td");

                for (var j = 0; j < tds.length; j++) {
                    var cell = tds[j];
                    if (cell && cell.textContent.toLowerCase().indexOf(filter) > -1) {
                        display = true;
                        break;
                    }
                }

                trs[i].style.display = display ? "" : "none";
            }
        }
    </script>


    <%--Update End--%>

    <style type="text/css">
        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=60);
            opacity: 0.6;
        }

        .modalPopup {
            background-color: #FFFFFF;
            width: 80%;
            border: 3px solid #0DA9D0;
            border-radius: 12px;
            padding: 0;
        }

            .modalPopup .header {
                background-color: #D69758;
                height: 30px;
                color: White;
                line-height: 30px;
                text-align: center;
                font-weight: bold;
                border-top-left-radius: 6px;
                border-top-right-radius: 6px;
            }

            .modalPopup .body {
                min-height: 50px;
                line-height: 30px;
                text-align: center;
                font-weight: bold;
            }

            .modalPopup .footer {
                padding: 6px;
            }

            .modalPopup .yes, .modalPopup .no {
                height: 23px;
                color: White;
                line-height: 23px;
                text-align: center;
                font-weight: bold;
                cursor: pointer;
                border-radius: 4px;
            }

            .modalPopup .yes {
                background-color: #2FBDF1;
                border: 1px solid #0DA9D0;
            }

            .modalPopup .no {
                background-color: #9F9F9F;
                border: 1px solid #5C5C5C;
            }
    </style>

    <div runat="server" class="container">

        <table align="center" style="width: 100%; border: #008CBA; border-style: solid; border-width: 0px;">

            <tr>
                <td align="center" style="border: #E6C79D; border-style: solid; border-width: 2px;" colspan="4">
                    <span style="color: #cb4e48; font-weight: bold; font-size: 17px">Inspection Officer Details</span>
                </td>
            </tr>
            <tr>
                <td style="height: 5px;" colspan="4"></td>
            </tr>
            <tr>
                <td colspan="4" align="center" style="font-size: small;">Total Record :
                                            <asp:Label ID="lblOfficerList" runat="server"></asp:Label>
                </td>
            </tr>

            <tr>

                <%--Search Textbox--%>
                <tr>
                    <td colspan="6" align="left">
                        <asp:TextBox ID="txtSearch_IsnpOff" runat="server"
                            placeholder="Search in Officer Grid..."
                            Style="width: 380px; height: 36px; margin-top: 10px; padding: 0 15px; font-size: 15px; border: 1px solid #444; border-radius: 8px; box-shadow: 0 2px 6px rgba(0,0,0,0.15);"
                            onkeyup="filterGrid_IsnpOff();" />
                    </td>
                </tr>


                <%--Search Textbox End--%>
                <td colspan="4" valign="top" align="center">
                    <asp:GridView ID="Gridview_IsnpOff" runat="server" DataKeyNames="PF_ID"
                        AutoGenerateColumns="False" Width="100%" Font-Size="10pt" Font-Bold="true"
                        BackColor="White" BorderColor="#008CBA" BorderStyle="Double" BorderWidth="1px"
                        CellPadding="2" CellSpacing="2" OnSelectedIndexChanged="Gridview_IsnpOff_SelectedIndexChanged">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No." ItemStyle-Width="3%">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                    <asp:HiddenField ID="hdnpfid" runat="server" Value='<%# Bind("PF_ID") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="PF_ID" HeaderText="Unique/PF ID" ItemStyle-Width="8%" ReadOnly="True" SortExpression="PF_ID" />
                            <asp:TemplateField HeaderText="Name">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lbname" Width="100%" Text='<%# Eval("Officer_Name")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="30%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Designation">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lbldesignation" Width="100%" Text='<%# Eval("Designation")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="30%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CUG_mobileNo">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblcugmobile" Width="100%" Text='<%# Eval("CUG_mobileNo")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="30%" />
                            </asp:TemplateField>

                            <asp:BoundField DataField="Rec_Office" HeaderText="Rec_Office" ReadOnly="True" SortExpression="Rec_Office" />
                            <%-- <asp:CommandField SelectText="Select" HeaderText="Schedule " ShowSelectButton="True">
                                <ControlStyle Font-Bold="True" ForeColor="#008CBA" />
                            </asp:CommandField>--%>
                            <asp:TemplateField HeaderText="Schedule">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkBtnEdit" runat="server" Text="Schedule" CssClass="btn btn-info"
                                        OnClick="Display"></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                        <%--<FooterStyle BackColor="#719cb6" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" />--%>
                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" Height="15px" Font-Size="8pt" />
                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#F7ECDD" Font-Bold="True" ForeColor="#cb4e48" HorizontalAlign="center"
                            Height="20px" Font-Size="10pt" />
                        <AlternatingRowStyle BackColor="#eeeeee" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
        <%-----------------------end of First Gride----------------%>
        <asp:Panel ID="pnllogin" class="popup" runat="server">
            <div class="pop" style="background-color: #FFFFCC0; min-height: 600PX; max-height: 500px; overflow: auto;">
                <%-- <div class="col-sm-12 col-md-12 col-xs-12">--%>
                <img visible="true" runat="server" src="~/images/close-icon.png" alt="quit" class="x" id="x" />


                <div id="divNewInsp" runat="server" visible="false" style="width: 100%;">
                    <table align="center" style="width: 100%; border: #008CBA; border-style: solid; border-width: 0px;">
                        <tr>
                            <td style="height: 5px;"></td>
                        </tr>



                        <tr>
                            <td align="center" style="border: #E6C79D; border-style: solid; border-width: 2px; width: 100%" colspan="6">
                                <span style="color: #cb4e48; font-weight: bold; font-size: 17px">Scheduled Branch Inspection Officer Details</span>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 5px;" colspan="6"></td>
                        </tr>
                        <tr>
                            <td colspan="6" align="center" style="font-size: small;">Total Record :
                                            <asp:Label ID="lblTotalInsp" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <%--Search Textbox--%>
                            <tr>
                                <td colspan="6" align="left" style="padding-bottom: 10px;">
                                    <asp:TextBox ID="txtSearch_OfficerPrev" runat="server"
                                        placeholder="Search in Scheduled Officer Grid..."
                                        Style="width: 380px; height: 36px; padding: 0 15px; font-size: 15px; border: 1px solid #444; border-radius: 8px; box-shadow: 0 2px 6px rgba(0,0,0,0.15);"
                                        onkeyup="filterGrid_OfficerPrev();" />
                                </td>
                            </tr>
                            <%--Search Textbox--%>

                            <tr>
                                <td align="center" colspan="6" valign="top">
                                    <asp:GridView ID="Gridview_OfficerPreviousInsp" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#008CBA" BorderStyle="Double" BorderWidth="1px" CellPadding="2" CellSpacing="2" DataKeyNames="ID" Font-Bold="true" Font-Size="10pt" OnRowCommand="Gridview_OfficerPreviousInsp_RowCommand" OnRowDeleting="Gridview_OfficerPreviousInsp_RowDeleting" OnRowEditing="Gridview_OfficerPreviousInsp_RowEditing" Width="100%">
                                        <Columns>
                                            <asp:TemplateField HeaderText="क्रमांक">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1%>
                                                    <asp:HiddenField ID="hdnId" runat="server" Value='<%# Bind("ID") %>' />
                                                    <asp:HiddenField ID="hdnEmpID" runat="server" Value='<%# Bind("Employee_ID") %>' />
                                                </ItemTemplate>
                                                <ItemStyle Width="1%" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ID" HeaderText="Inspection ID" ReadOnly="True" SortExpression="ID" />
                                            <asp:BoundField DataField="Employee_ID" HeaderText="Unique/PF ID" ReadOnly="True" SortExpression="Employee_ID" />
                                            <asp:BoundField DataField="Officer_Name" HeaderText="Officer Name" ReadOnly="True" SortExpression="Officer_Name" />
                                            <asp:BoundField DataField="Distirct_name" HeaderText="Distirct" ReadOnly="True" SortExpression="Distirct_name" />
                                            <asp:BoundField DataField="Depotname" HeaderText="Branch" ReadOnly="True" SortExpression="Depotname" />
                                            <asp:BoundField DataField="Order_Date" HeaderText="Order Date" ReadOnly="True" SortExpression="Order_Date" />
                                            <asp:BoundField DataField="Order_No" HeaderText="Order Number" ReadOnly="True" SortExpression="Order_No" />
                                            <asp:BoundField DataField="Insp_Type" HeaderText="Inspection Type" ReadOnly="True" SortExpression="Insp_Type" />
                                            <asp:BoundField DataField="Verification_Type1" HeaderText="Verification Type" ReadOnly="True" SortExpression="Verification_Type1" />
                                            <asp:BoundField DataField="Status" HeaderText="Status" ReadOnly="True" SortExpression="Status" />
                                            <asp:TemplateField HeaderText="Remove">
                                                <ItemTemplate>
                                                    <asp:Button ID="btnRemove" runat="server" CommandArgument="<%# Container.DataItemIndex %>" CommandName="RemoveRow" CssClass="btneditstyle" OnClientClick="return confirm('Do you want to Remove this row?');" Text="Remove" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <%--<FooterStyle BackColor="#719cb6" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" />--%>
                                        <PagerStyle BackColor="#F7F7DE" Font-Size="10pt" ForeColor="Black" Height="20px" HorizontalAlign="Center" />
                                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#F7ECDD" Font-Bold="True" Font-Size="10pt" ForeColor="#cb4e48" Height="20px" HorizontalAlign="center" />
                                        <AlternatingRowStyle BackColor="#eeeeee" />
                                    </asp:GridView>
                                </td>
                            </tr>
                            <%-------------End of Second Gride --------%>
                        </tr>

                    </table>
                    <asp:Label ID="Label12" runat="server" Font-Bold="true" Font-Size="Medium"></asp:Label>

                </div>


                <%--------End Of Third Section -------------%>
                <%-- </div>--%>
            </div>
            <img alt="New" src="images/new6.gif" id="new" runat="server" />

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
    </div>
</asp:Content>

