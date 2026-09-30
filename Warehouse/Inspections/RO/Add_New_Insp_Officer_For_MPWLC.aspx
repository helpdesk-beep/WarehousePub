<%@ Page Title="" Language="C#" MasterPageFile="~/Inspections/Masters/Inspection_RO.master" AutoEventWireup="true" CodeFile="Add_New_Insp_Officer_For_MPWLC.aspx.cs" Inherits="Inspections_RO_Add_New_Insp_Officer_For_MPWLC" %>

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
                        AutoGenerateColumns="False" Width="100%" Font-Size="10pt" Font-Bold="true"
                        BackColor="White" BorderColor="#008CBA" BorderStyle="Double" BorderWidth="1px"
                        CellPadding="2" CellSpacing="2">

                      <Columns>                                                  
                            <asp:TemplateField HeaderText="S.No." ItemStyle-Width="3%">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                    <asp:HiddenField ID="hdndstid" runat="server" Value='<%# Bind("District_ID") %>' />
                                    <asp:HiddenField ID="hdnbranchid" runat="server" Value='<%# Bind("Branch_ID") %>' />
                                    <asp:HiddenField ID="hdndob" runat="server" Value='<%# Bind("DOB") %>' />
                                    <asp:HiddenField ID="hdndoj" runat="server" Value='<%# Bind("DOJ") %>' />
                                    <asp:HiddenField ID="hdnmobileno" runat="server" Value='<%# Bind("Per_MobileNo") %>' />
                                    <asp:HiddenField ID="hdnpfid" runat="server" Value='<%# Bind("PF_ID") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="PF_ID" HeaderText="Unique/PF ID" ReadOnly="True" SortExpression="PF_ID" />
                            <asp:TemplateField HeaderText="Name">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lbname" Width="100%" Text='<%# Eval("Officer_Name")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Designation">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lbldesignation" Width="100%" Text='<%# Eval("Designation")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Rec_Office">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblRec_Office" Width="100%" Text='<%# Eval("Rec_Office")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Mobile No">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblcugmobile" Width="100%" Text='<%# Eval("CUG_mobileNo")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>

                          
                            <asp:TemplateField HeaderText="Edit">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton2" runat="server" Text="Edit" CssClass="btn btn-info"
                                        OnClick="Display2"></asp:LinkButton>
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
                                <asp:Label ID="Label3" runat="server" Text="Alternate Mobile No : "></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtcug" runat="server" class="text" type="text" Height="25px"
                                    MaxLength="10"></asp:TextBox>
                            </td>
                            <%--<td>
                                <asp:Label ID="Label4" runat="server" Text="DOB : "></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtdob" runat="server" class="text" type="text" Height="25px"
                                    onkeydown="javascript:preventInput(event);" onpaste="return false;"></asp:TextBox>

                                <cc1:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy"
                                    TargetControlID="txtdob">
                                </cc1:CalendarExtender>

                            </td>--%>
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
                                    <asp:ListItem Value="AGM">AGM</asp:ListItem>
                                    <asp:ListItem Value="AQC">AQC</asp:ListItem>
                                    <asp:ListItem Value="AQC(C)">AQC(C)</asp:ListItem>
                                    <asp:ListItem Value="QC">QC</asp:ListItem>
                                    <asp:ListItem Value="Manager(QC)">Manager(QC)</asp:ListItem>
                                    <asp:ListItem Value="Manager(General)">Manager(General)</asp:ListItem>
                                    <asp:ListItem Value="Assistant accountant">Assistant accountant</asp:ListItem>
                                    <asp:ListItem Value="Senior assistant">Senior assistant</asp:ListItem>
                                    <asp:ListItem Value="Junior Assistant">Junior Assistant</asp:ListItem>
                                    <asp:ListItem Value="Stenographer">Stenographer</asp:ListItem>

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
                           <%-- <td>&nbsp&nbsp
                                <asp:Label ID="Label7" runat="server" Text="Unique ID / PF ID : "></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_pfid" runat="server" class="text" type="text" Height="25px"></asp:TextBox>
                            </td>--%>
                           <%-- <td>
                                <asp:Label ID="Label8" runat="server" Text="Date Of Joining : "></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_doj" runat="server" class="text" type="text" Height="25px"
                                    onkeydown="javascript:preventInput(event);" onpaste="return false;"></asp:TextBox>

                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                    TargetControlID="txt_doj">
                                </cc1:CalendarExtender>
                            </td>--%>
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

