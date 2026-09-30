<%@ Page Title="" Language="C#" MasterPageFile="~/Inspections/Masters/Inspection_BO.master" AutoEventWireup="true" CodeFile="Add_New_Insp_Officer.aspx.cs" Inherits="Inspections_BO_Add_New_Insp_Officer" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style type="text/css">
        .modal-dialog {
            width: 1000px;
            margin: 30px auto;
        }

        .btn-info {
            color: #fff;
            background-color: #5bc0de;
            border-color: #46b8da;
        }

        .btn {
            display: inline-block;
            padding: 6px 12px;
            margin-bottom: 0;
            font-size: 14px;
            font-weight: 400;
            line-height: 1.42857143;
            text-align: center;
            white-space: nowrap;
            vertical-align: middle;
            -ms-touch-action: manipulation;
            touch-action: manipulation;
            cursor: pointer;
            -webkit-user-select: none;
            -moz-user-select: none;
            -ms-user-select: none;
            user-select: none;
            background-image: none;
            border: 1px solid transparent;
            border-radius: 4px;
        }

        .btn-info:hover {
            color: black;
            background-color: #31b0d5;
            border-color: #269abc;
        }

        .btn.active, .btn:active {
            background-image: none;
            outline: 0;
            -webkit-box-shadow: inset 0 3px 5px rgba(0,0,0,.125);
            box-shadow: inset 0 3px 5px rgba(0,0,0,.125);
        }
    </style>
    <div runat="server">
          <div style="text-align: right;">
                <asp:LinkButton ID="LinkButton1" runat="server" Text="Add New Officer" CssClass="btn btn-info"
                    OnClick="Display"></asp:LinkButton>
            </div>
        <table align="center" style="width: 100%; border: #E6C79D; border-style: solid; border-width: 0px;">
            <tr>
                <td align="center" style="border: #E6C79D; border-style: solid; border-width: 2px;" colspan="4">
                    <span style="color: #cb4e48; font-weight: bold; font-size: 17px">Inspection Officer Details</span>
                    <asp:Label ID="lbl_user" runat="server" Text="Label" Visible="false"></asp:Label>
                </td>

            </tr>
            <tr>
                <td colspan="4" align="center" style="font-size: small;">Total Record :
                                            <asp:Label ID="lblOfficerList" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="4" valign="top" align="center">
                    <asp:GridView ID="Gridview_IsnpOff" runat="server" DataKeyNames="PF_ID"
                        AutoGenerateColumns="False" Width="70%" Font-Size="10pt" Font-Bold="true"
                        BackColor="White" BorderColor="#008CBA" BorderStyle="Double" BorderWidth="1px"
                        CellPadding="2" CellSpacing="2">

                        <Columns>
                            <asp:BoundField DataField="PF_ID" HeaderText="PF_ID" ReadOnly="True" SortExpression="PF_ID" />
                            <asp:BoundField DataField="Officer_Name" HeaderText="Officer_Name" ReadOnly="True" SortExpression="Officer_Name" />
                            <asp:BoundField DataField="Designation" HeaderText="Designation" ReadOnly="True" SortExpression="Designation" />
                            <asp:BoundField DataField="Rec_Office" HeaderText="Rec_Office" ReadOnly="True" SortExpression="Rec_Office" />
                            <asp:BoundField DataField="CUG_mobileNo" HeaderText="CUG_mobileNo" ReadOnly="True" SortExpression="CUG_mobileNo" />
                            <asp:CommandField SelectText="Update" HeaderText="Updaet " ShowSelectButton="True">
                                <ControlStyle Font-Bold="True" ForeColor="#008CBA" />
                            </asp:CommandField>
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

        <div>

            <div style="text-align: center;">
                <asp:LinkButton ID="lnkBtnEdit" runat="server" Text="Add New Officer" CssClass="btn btn-info"
                    OnClick="Display"></asp:LinkButton>
            </div>
        </div>
        <asp:Panel ID="pnllogin" class="popup" runat="server">
            <div class="pop" style="background-color: #FFFFCC0">
                <div class="col-sm-12 col-md-12 col-xs-12">
                <img visible="true" runat="server" src="~/images/close-icon.png" alt="quit" class="x" id="x" />

                    <table align="center" style="width: 100%; border: #008CBA; border-style: solid; border-width: 0px;">


                        <tr>
                            <td align="center" style="border: #E6C79D; border-style: solid; border-width: 2px; color: #008CBA; font-weight: bolder; font-size: 20px" colspan="4">
                                <span style="color: #cb4e48; font-weight: bolder; font-size: 17px">Add New Officer Details</span>
                            </td>

                        </tr>
                        <tr>
                            <td colspan="4" style="height: 10px;"></td>
                        </tr>
                        <tr>

                            <td>&nbsp&nbsp
                                <asp:Label ID="Label1" runat="server" Text="Inspection Officer Name : "></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtofficername" runat="server" class="text" type="text" Height="25px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label2" runat="server" Text="Personal Mobile No : "></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtMob" runat="server" class="text" type="text" Height="25px"
                                    MaxLength="10"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp&nbsp
                                <asp:Label ID="Label3" runat="server" Text="CUG/Alternate Mobile No : "></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtcug" runat="server" class="text" type="text" Height="25px"
                                    MaxLength="10"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label4" runat="server" Text="DOB : "></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtdob" runat="server" class="text" type="text" Height="25px"
                                    onkeydown="javascript:preventInput(event);" onpaste="return false;"></asp:TextBox>

                                <cc1:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy"
                                    TargetControlID="txtdob">
                                </cc1:CalendarExtender>

                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp&nbsp
                                <asp:Label ID="Label5" runat="server" Text="Place Of Posting : "></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddl_recoffice" runat="server" AutoPostBack="true" Width="222px"
                                    Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6"
                                    OnSelectedIndexChanged="ddl_recoffice_SelectedIndexChanged">
                                    <asp:ListItem Selected="True">--Select--</asp:ListItem>
                                    <%--<asp:ListItem Value="HO">Head Office</asp:ListItem>--%>
                                    <asp:ListItem Value="RO">Regional Office</asp:ListItem>
                                    <asp:ListItem Value="DO">District Office</asp:ListItem>
                                    <asp:ListItem Value="BO">Branch Office</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Label ID="Label6" runat="server" Text="Designation : "></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddl_Desig" runat="server" AutoPostBack="false" Width="222px"
                                    Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6">
                                    <asp:ListItem Selected="True">--Select--</asp:ListItem>
                                    <asp:ListItem Value="AQC">AGM</asp:ListItem>
                                    <asp:ListItem Value="AQC">AQC</asp:ListItem>
                                    <asp:ListItem Value="AQC">AQC(C)</asp:ListItem>
                                    <asp:ListItem Value="AQC">QC</asp:ListItem>
                                    <asp:ListItem Value="AQC">Manager(QC)</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr id="trDist" runat="server">
                            <td>&nbsp&nbsp
                                <asp:Label ID="Label9" runat="server" Text="District : "></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddl_dist" runat="server" AutoPostBack="true" Width="222px"
                                    Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6"
                                    OnSelectedIndexChanged="ddl_dist_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Label ID="Label10" runat="server" Text="Branch : "></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddl_branch" runat="server" AutoPostBack="false" Width="222px"
                                    Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp&nbsp
                                <asp:Label ID="Label7" runat="server" Text="Unique ID / PF ID : "></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_pfid" runat="server" class="text" type="text" Height="25px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label8" runat="server" Text="Date Of Joining : "></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_doj" runat="server" class="text" type="text" Height="25px"
                                    onkeydown="javascript:preventInput(event);" onpaste="return false;"></asp:TextBox>

                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                    TargetControlID="txt_doj">
                                </cc1:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" style="height: 10px;"></td>
                        </tr>

                        <tr>
                            <td colspan="4" align="center">
                                <asp:Button class="button button2" ID="btn_addnewoff" runat="server" Text="Submit"
                                    TabIndex="11" Width="150px" Height="30px" OnClick="btn_addnewoff_Click"></asp:Button>
                                &nbsp;
                                 <asp:Button class="button button2" ID="btn_clear" runat="server" Text="Clear"
                                     TabIndex="11" Width="150px" Height="30px" OnClick="btn_clear_Click"></asp:Button>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" style="height: 5px;"></td>
                        </tr>
                    </table>
                </div>
                <img alt="New" src="images/new6.gif" id="new" runat="server" />
               
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
    </div>

    <%--<script type='text/javascript'>
        function openModal() {
            $('[id*=myModal]').modal('show');
        }
    </script>--%>
</asp:Content>

