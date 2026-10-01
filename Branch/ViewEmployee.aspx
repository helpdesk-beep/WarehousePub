<%@ Page Title="" Debug="true" ViewStateEncryptionMode="Always" Language="C#" MasterPageFile="~/MasterPages/RMMasters.master" AutoEventWireup="true" CodeFile="ViewEmployee.aspx.cs" Inherits="Region_ViewEmployee" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PM" runat="Server">
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
    <script src="../NEW_CSS/js/jquery-1.12.4.js"></script>
    <script src="../NEW_CSS/js/jquery-ui.js"></script>
    <script src="<%= ResolveUrl("~/NEW_CSS/js/jquery-ui.js") %>"></script>

    <%--<script type="text/javascript">

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
    </script>--%>
    <style type="text/css">
        .circle {
            width: 220px;
            height: 120px;
            background: LightPink;
            -moz-border-radius: 160px;
            -webkit-border-radius: 160px;
            border-radius: 160px;
            border: groove;
        }
    </style>
    <style>
        .Grid {
            background-color: #fff;
            margin: 5px 0 10px 0;
            border: solid 1px #525252;
            border-collapse: collapse;
            font-family: Calibri;
            color: #474747;
            width: 100%;
        }

            .Grid td {
                padding: 2px;
                border: solid 1px #c1c1c1;
            }

            .Grid th {
                padding: 4px 2px;
                color: #fff;
                background: #00aad2 url(Images/grid-header.png) repeat-x top;
                border-left: solid 1px #525252;
                font-size: 15px;
                text-align: center;
            }

            .Grid .alt {
                background: #fcfcfc url(Images/grid-alt.png) repeat-x top;
            }

            .Grid .pgr {
                background: #00aad2 url(Images/grid-pgr.png) repeat-x top;
            }

                .Grid .pgr table {
                    margin: 3px 0;
                }

                .Grid .pgr td {
                    border-width: 0;
                    padding: 0 6px;
                    border-left: solid 1px #666;
                    font-weight: bold;
                    color: #fff;
                    line-height: 12px;
                }

                .Grid .pgr a {
                    color: Gray;
                    text-decoration: none;
                }

                    .Grid .pgr a:hover {
                        color: #000;
                        text-decoration: none;
                    }
    </style>

    <style type="text/css">
        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=60);
            opacity: 0.6;
        }

        .modalPopup {
            background-color: #FFFFFF;
            width: 100%;
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
            /*min-width: 900px;*/
            width: 80%;
            min-height: 150px;
            margin: 0px auto;
            background: #FFFFFF;
            position: relative;
            z-index: 103;
            padding: 10px;
            border-radius: 5px;
            box-shadow: 0 5px 10px #000;
            /*margin-top:200px;*/
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
                /*left: 22px;*/
                position: relative;
                /*top: -20px;*/
                width: 35px;
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
    <div class="container">
        <div>
            <b style="font-size: large; font-family: 'Times New Roman', Times, serif">
                <asp:Literal ID="Literal80" runat="server" Text="Employee List For Verification" /></b>
        </div>
        <br />
        <div class="img-thumbnail" style="width: 100%">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="2" Width="100%"
                CssClass="Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
                <Columns>
                    <asp:TemplateField HeaderText="S.No.">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                            <asp:HiddenField ID="hdnId" runat="server" Value='<%# Bind("ID") %>' />
                            <asp:HiddenField ID="hdnEmpID" runat="server" Value='<%# Bind("Employee_ID") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Name">
                        <ItemTemplate>
                            <asp:Label ID="lbl_Name" runat="server" Text='<%#Eval("Name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="District">
                        <ItemTemplate>
                            <asp:Label ID="lbl_District" runat="server" Text='<%#Eval("District") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Branch">
                        <ItemTemplate>
                            <asp:Label ID="lbl_Branch" runat="server" Text='<%#Eval("Branch") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Date of Birth">
                        <ItemTemplate>
                            <asp:Label ID="lbl_DOJ" runat="server" Text='<%#Eval("DOJ") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Gender">
                        <ItemTemplate>
                            <asp:Label ID="lbl_Gender" runat="server" Text='<%#Eval("Gender") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Designation">
                        <ItemTemplate>
                            <asp:Label ID="lbl_Designation" runat="server" Text='<%#Eval("Designation") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Update Verify Employee Details">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkBtnEdit" runat="server" Text="Update" CssClass="btn btn-info"
                                OnClick="Display"></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <%--<asp:TemplateField HeaderText="मोबाइल न.">
                        <ItemTemplate>
                            <asp:Label ID="lbl_Mobile" runat="server" Text='<%#Eval("Mobile") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <%-- <asp:TemplateField HeaderText="फोटो">
                        <ItemTemplate>
                            <asp:Image runat="server" ImageUrl='<%#Eval("Image") %>' CssClass="circle" Width="120px" />
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                </Columns>
                <%--  <HeaderStyle CssClass="alert-warning" />
                <AlternatingRowStyle CssClass="alert-success" />--%>
            </asp:GridView>
        </div>
        <%-----------------------end of First Gride----------------%>
        <asp:Panel ID="pnllogin" class="popup" runat="server">
            <div class="pop" style="background-color: #FFFFCC0; min-height: 600PX; max-height: 500px; overflow: auto;">
                <%-- <div class="col-sm-12 col-md-12 col-xs-12">--%>


                <div id="divNewInsp" runat="server" visible="false" style="width: 100%;">
                    <div style="width: 50px; text-align: right;">
                        <img visible="true" runat="server" src="~/images/close-icon.png" alt="quit" class="x" id="x" />
                    </div>
                    <table align="center" style="width: 100%; border: #008CBA; border-style: solid; border-width: 0px;">
                        <tr>
                            <td style="height: 5px;"></td>
                        </tr>



                        <tr>
                            <td align="center" style="border: #E6C79D; border-style: solid; border-width: 2px; width: 100%" colspan="6">
                                <span style="color: #cb4e48; font-weight: bold; font-size: 17px">Verify Employee Details</span>
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
                            <td colspan="2" align="center" style="font-size: small;">Employee Name :
                                            <asp:Label ID="lblempname" runat="server"></asp:Label>
                            </td>
                            <td align="center" style="font-size: small;">Designation :
                                            <asp:Label ID="lbldesignation" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6" valign="top" align="center">
                                <asp:GridView ID="Gridview_OfficerPreviousInsp" runat="server"
                                    AutoGenerateColumns="False" Width="100%" Font-Size="10pt" Font-Bold="true" BackColor="White"
                                    BorderColor="#008CBA" BorderStyle="Double"
                                    BorderWidth="1px" CellPadding="2" CellSpacing="2">

                                    <Columns>
                                        <asp:TemplateField HeaderText="क्रमांक">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1%>
                                                <asp:HiddenField ID="hdnId" runat="server" Value='<%# Bind("ID") %>' />
                                                <asp:HiddenField ID="hdnEmpID" runat="server" Value='<%# Bind("Employee_ID") %>' />
                                            </ItemTemplate>
                                            <ItemStyle Width="1%" />
                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Region" HeaderText="Region" ReadOnly="True" SortExpression="Region" />
                                        <asp:BoundField DataField="District" HeaderText="Distirct" ReadOnly="True" SortExpression="District" />
                                        <asp:BoundField DataField="Branch" HeaderText="Branch" ReadOnly="True" SortExpression="Branch" />
                                        <asp:BoundField DataField="fromdate" HeaderText="Date of Joining_(DD/MM/YYYY) From" ReadOnly="True" SortExpression="fromdate" />
                                        <asp:BoundField DataField="todate" HeaderText="To (DD/MM/YYYY)" ReadOnly="True" SortExpression="todate" />
                                        <asp:TemplateField HeaderText="Edit">
                                            <ItemTemplate>
                                                <asp:Button ID="btnEdit" Text="Edit" runat="server" CommandName="Edit" CssClass="btneditstyle" CommandArgument='<%# Container.DataItemIndex %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>

                                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" Height="20px" Font-Size="10pt" />
                                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#F7ECDD" Font-Bold="True" ForeColor="#cb4e48" HorizontalAlign="center"
                                        Height="20px" Font-Size="10pt" />
                                    <AlternatingRowStyle BackColor="#eeeeee" />
                                </asp:GridView>
                            </td>
                        </tr>

                        <%-------------End of Second Gride --------%>

                        <tr>
                            <td align="center" style="border: #E6C79D; border-style: solid; border-width: 2px;" colspan="6">
                                <span style="color: #cb4e48; font-weight: bold; font-size: 17px">Allot Branch Inspection</span>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 10px;"></td>
                        </tr>

                        <tr>
                            <td>
                                <asp:Label ID="Label5" runat="server" Text="Designation :"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDesig" runat="server" class="text" type="text" Height="25px" Width="222px" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label6" runat="server" Text="Mobile Number :"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCug" runat="server" class="text" type="text" Height="25px" Width="222px" ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp&nbsp&nbsp&nbsp
                        <asp:Label ID="Label9" runat="server" Text="District : "></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddl_dist" runat="server" AutoPostBack="true" Width="222px"
                                    Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6"
                                    OnSelectedIndexChanged="ddl_dist_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text="Branch : "></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddl_branch" runat="server" AutoPostBack="false" Width="222px"
                                    Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6">
                                </asp:DropDownList>
                            </td>

                        </tr>
                        <tr>
                            <td>&nbsp&nbsp&nbsp&nbsp<asp:Label ID="Label4" runat="server" Text="From Date : "></asp:Label></td>
                            <td>
                                <asp:TextBox ID="txt_Fromdate" runat="server" class="text" type="text" Height="25px" Width="222px"
                                        onkeydown="javascript:preventInput(event);" onpaste="return false;"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                        TargetControlID="txt_Fromdate">
                                    </cc1:CalendarExtender>
                            </td>
                            <td>&nbsp&nbsp&nbsp&nbsp<asp:Label ID="Label11" runat="server" Text="TO Date : "></asp:Label></td>
                            <td>
                                 <asp:TextBox ID="txttodate" runat="server" class="text" type="text" Height="25px" Width="222px"
                                        onkeydown="javascript:preventInput(event);" onpaste="return false;"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                        TargetControlID="txttodate">
                                    </cc1:CalendarExtender>
                            </td>


                        </tr>


                        <tr>

                            <td style="height: 10px;"></td>
                        </tr>
                        <tr>
                            <td colspan="6" align="center">
                                <asp:Button class="button button6" ID="btn_saveInspDate" runat="server" Text="Verify"
                                    TabIndex="11" Width="222px" Height="30px"></asp:Button>
                                &nbsp&nbsp&nbsp&nbsp
                                            <asp:Button class="button button6" ID="btnclear" runat="server" Text="Clear All"
                                                TabIndex="12" Width="222px" Height="30px"></asp:Button>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 10px;"></td>
                        </tr>

                        <tr>
                            <td style="height: 10px;"></td>
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

