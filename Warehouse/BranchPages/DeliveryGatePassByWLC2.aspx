<%@ Page Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="DeliveryGatePassByWLC2.aspx.cs" Inherits="BranchPages_DeliveryGatePassByWLC2" Title="Delivery Gate Pass" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <fieldset style="width: 1000px; border: 2px solid navy;">
        <center>
            <%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>--%>
                    <div class="blockMe" id="blockMe">
                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td colspan="6" align="center" valign="top">
                                    <fieldset style="width: 980px; border: 1px solid navy;">
                                        <center>
                                            <div>
                                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                    <tr style="background-color: #0bb6e6; height: 25px">
                                                        <td colspan="4" align="center">
                                                            <asp:Label ID="lblDeliveryOrderOfStock" runat="server" Text="Generate Delivery GatePass(For Old Stock only)"
                                                                ForeColor="whitesmoke" Font-Bold="true" Font-Size="12pt"></asp:Label>
                                                        </td>
                                                       
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 10px" colspan="4">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4" align="center">
                                                            <asp:Label ID="lblmsg" runat="server" Font-Size="10pt" ForeColor="Red" EnableViewState="False"
                                                                Font-Bold="true"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4" align="left">
                                                            <asp:Label ID="lblInstruction" runat="server" Text="Note :- "
                                                                Font-Bold="True" ForeColor="red"></asp:Label><a href="javascript:popMe('../../SampleQuantity.htm');"
                                                                    style="text-decoration: underline; color: Red; font-size: 12pt">यह स्क्रीन आज दिनांक तक भुगतान किए गए स्टॉक हेतु Gatepass जारी करने के लिए है, Gatepass की Entry शाखा मे भौतिक रूप से उपलब्ध स्टॉक के आधार पर Date wise Gatepass जारी करते हुए करे।</a>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 10px" colspan="4">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" style="width: 120px">
                                                            <asp:Label ID="lblDepositorType" runat="server" Text="Depositor Type " Font-Size="8pt"
                                                                Font-Bold="true" ForeColor="navy"></asp:Label>
                                                        </td>
                                                        <td align="left" style="width: 200px" valign="middle">
                                                            <asp:DropDownList ID="ddlDepositorType" runat="server" Font-Size="10pt" OnSelectedIndexChanged="ddlDepositorType_SelectedIndexChanged"
                                                                AutoPostBack="True" TabIndex="1" Height="30px" Width="200px" CssClass="tb6">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td align="left" style="width: 150px">
                                                            <asp:Label ID="lblDepositorName" runat="server" Text="Depositor Name " Font-Size="8pt"
                                                                Font-Bold="true" ForeColor="navy"></asp:Label>
                                                        </td>
                                                        <td align="left" style="width: 200px" valign="middle">
                                                            <asp:DropDownList ID="ddlDepositor" runat="server" Font-Size="10pt" TabIndex="2"
                                                                OnSelectedIndexChanged="ddlDepositor_SelectedIndexChanged" AutoPostBack="True"
                                                                Height="30px" Width="200px" CssClass="tb6">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 5px" colspan="4">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <%--<td align="left" style="width: 100px">
                                                            <asp:Label ID="lblIssuedTo" runat="server" Text="Issued To" Font-Size="8pt" Font-Bold="true"
                                                                ForeColor="navy"></asp:Label>
                                                        </td>
                                                        <td align="left" style="width: 120px">
                                                            <asp:DropDownList ID="ddlDeliveredAgnt" runat="server" Font-Size="10pt" TabIndex="3"
                                                                Height="30px" Width="200px"  CssClass="tb6" AutoPostBack="true" OnSelectedIndexChanged="ddlDeliveredAgnt_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>--%>
                                                        <td align="left">
                                                            <asp:Label ID="lblGodownNo" runat="server" Font-Size="8pt" Font-Bold="true" ForeColor="navy"
                                                                Text="Godown -"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:DropDownList ID="ddlGodown" runat="server" Font-Size="10pt" Height="30px" Width="200px"
                                                                TabIndex="6"  AutoPostBack="false"
                                                                CssClass="tb6">
                                                            </asp:DropDownList>
                                                        </td>
                                                          <td align="left" class="auto-style1">
                                                            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="8pt" 
                                                                ForeColor="Navy" Text="Date of WHR"></asp:Label>
                                                        </td>
                                                        <td align="left" valign="middle" class="auto-style1">
                                                                                                                          <asp:TextBox ID="txtWHRDate" runat="server" Width="150px" Height="20px"></asp:TextBox>
                                                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" 
                                                                    Enabled="True" TargetControlID="txtWHRDate" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy">
                                                                </asp:CalendarExtender>
                                                        </td>
                                                        
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 5px" colspan="4">
                                                        </td>
                                                    </tr>
                                                   <%-- <tr id="trGPCY" runat="server" visible="false">
                                                        <td align="left">
                                                            <asp:Label ID="Label3" runat="server" Text="Crop Year" Font-Size="8pt"
                                                                ForeColor="navy" Font-Bold="true"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:DropDownList ID="GPddl" runat="server" AutoPostBack="True" Width="200px" 
                                                                Height="30px" onselectedindexchanged="GPddl_SelectedIndexChanged"
                                                                >
                                                            </asp:DropDownList>
                                                            <asp:TextBox ID="TextBox3" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                MaxLength="10" Width="0px"></asp:TextBox>
                                                        </td>
                                                    </tr>--%>
                                                    <tr>
                                                         <td align="left">

                                                        <asp:Label ID="lblComm" runat="server" Font-Bold="True" Font-Size="8pt" ForeColor="Navy"
                                                            Text="Commodity"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                 <asp:DropDownList ID="ddlcomm" CssClass="tb6" runat="server" Height="30px" Width="200px"
                                                    AutoPostBack="True" onselectedindexchanged="ddlcomm_SelectedIndexChanged" >
                                                        </asp:DropDownList>
                                                        
                                                            <asp:Label ID="lblcom" runat="server" Font-Size="8pt" ForeColor="Transparent" Font-Bold="true"
                                                                Text=""></asp:Label>
                                                        
                                                        </td>
                                                     <td align="left" class="auto-style1">
                                                            <asp:Label ID="lblcropyear" runat="server" Font-Bold="True" Font-Size="8pt" 
                                                                ForeColor="Navy" Text="Crop Year:"></asp:Label>
                                                        </td>
                                                        <td align="left" valign="middle" class="auto-style1">
                                                            <asp:DropDownList ID="ddlcropyr" runat="server" AutoPostBack="True" 
                                                                Height="30px" Width="200px" onselectedindexchanged="ddlcropyr_SelectedIndexChanged"
                                                                >
                                                            </asp:DropDownList>
                                                        </td>
                                                      

                                                        </tr>
                                                    <tr id="trdo" runat="server" visible="false">
                                                        <%--<td align="left">
                                                            <asp:Label ID="lblDONo" runat="server" Text="Delivery Order No.(CSMS)- " Font-Size="8pt"
                                                                ForeColor="navy" Font-Bold="true"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:DropDownList ID="ddlDONumber" runat="server" Font-Size="10pt" AutoPostBack="True"
                                                                OnSelectedIndexChanged="ddlDONumber_SelectedIndexChanged" TabIndex="5" Height="30px"
                                                                Width="200px" CssClass="tb6">
                                                            </asp:DropDownList>
                                                            <asp:TextBox ID="txtTransId" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                MaxLength="10" Width="0px"></asp:TextBox>
                                                        </td>--%>
                                                       <%-- <td align="left">
                                                        <asp:Label ID="lbldate" runat="server" Text="Date of Delivery " Font-Size="8pt"
                                                                ForeColor="navy" Font-Bold="true"></asp:Label>
                                                        </td >
                                                        <td align="left">
                                                            <asp:TextBox ID="txtdateofissue" runat="server" AutoPostBack="True" 
                                                                ontextchanged="txtdateofissue_TextChanged"></asp:TextBox>
                                                            <asp:CalendarExtender ID="txtdateofissue_CalendarExtender" runat="server" 
                                                                Enabled="True" TargetControlID="txtdateofissue" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy">
                                                            </asp:CalendarExtender>
                                                        </td>--%>
                                                    </tr>
                                                    <tr id="trdo2" runat="server" visible="false">
                                                        <td align="left">
                                                            &nbsp;</td>
                                                        <td align="left">
                                                            &nbsp;</td>
                                                        <td align="left">
                                                            &nbsp;</td >
                                                        <td align="left">
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr id="trcomm" runat="server" visible="false">
                                                   
                                                       <%-- <td>

                                                        <asp:Label ID="lblgatepasstype" runat="server" Text="Gatepass type" Font-Size="8pt"
                                                                ForeColor="navy" Font-Bold="true"></asp:Label>

                                                        
                                                        
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rbdate" runat="server" Checked="True" GroupName="r" 
                                                                AutoPostBack="True" oncheckedchanged="rbdate_CheckedChanged" 
                                                                Text="Date Wise" />
                                                            <asp:RadioButton ID="rbdo"
                                                                runat="server" GroupName="r" AutoPostBack="True" 
                                                                oncheckedchanged="rbdo_CheckedChanged" Text="DO Wise" />
                                                        </td>--%>
                                                    </tr>
                                                </table>
                                            </div>
                                        </center>
                                    </fieldset>
                                </td>
                            </tr>
                            <tr id="Stockdetails" runat="server" visible="false">
                                <td colspan="6" align="left" valign="top">
                                    <fieldset style="width: 980px; border: 1px solid navy;">
                                        <center>
                                            <div>
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                    <tr style="background-color: #0bb6e6; height: 25px">
                                                        <td align="center">
                                                            <asp:Label ID="lblStockforIssue" runat="server" Text="WHR Details & Available Stock For Issue"
                                                                ForeColor="whitesmoke" Font-Bold="true" Font-Size="12pt"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 10px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <span style="color: Red; font-weight: bold; font-size: 10pt">Note - जिस WHR से स्टॉक
                                                                Issue करना है,उसमे बोरी की मात्रा नंबर में,वजन (Qty. in Qtls.kgsgms) में प्रविष्ट
                                                                करें ! <br />
                                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;टॉप 200 WHR क़े रिकॉर्ड ही प्रदर्शित होंगे जिनमें बैलेंस शेष हे |
                                                                 </span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 10px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top" align="center">
                                                            <div style="overflow: scroll; height: 200px; overflow-x: hidden">
                                                                <asp:GridView ID="gdstackdetail" runat="server" CellPadding="2" TabIndex="10" Width="100%"
                                                                    ForeColor="Navy" AutoGenerateColumns="False">
                                                                    <Columns>
                                                                        <asp:BoundField DataField="SNo" HeaderText="S.No.">
                                                                            <ItemStyle HorizontalAlign="center" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="Depositor_Name" HeaderText="Depositer Name">
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity Name">
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="Depositor_whr_id" HeaderText="Whr No">
                                                                            <ItemStyle HorizontalAlign="Left" Width="100px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="Stack_Name" HeaderText="StackNo">
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="AvailBags" HeaderText="Available Bags">
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="AvailQty" HeaderText="Available Weight">
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="Lot_no" HeaderText="Lot No.">
                                                                            <ItemStyle HorizontalAlign="Left" Width="100px" />
                                                                        </asp:BoundField>
                                                                        <asp:TemplateField HeaderText="Bags">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtbagnumber" runat="server" Width="60px" MaxLength="5" onblur="Spc_validatorInt(this)">0</asp:TextBox>
                                                                            </ItemTemplate>
                                                                            <ItemStyle Width="80px" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Weight">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtweight" runat="server" Width="65px" MaxLength="12" onkeyup="NumericDecimalCheck(this,5)"
                                                                                    onblur="Spc_validatornumeric(this)">0</asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Select">
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="ckstack" runat="server" AutoPostBack="True" OnCheckedChanged="ckstack_CheckedChanged" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField DataField="Godown_ID" HeaderText="Godown_ID">
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="Stack_ID" HeaderText="stackid">
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                        <asp:TemplateField HeaderText="Gain">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtgain" runat="server" Height="21px" Width="51px" onkeyup="NumericDecimalCheck(this,5)"
                                                                                    onblur="Spc_validatornumeric(this)" Enabled="True">0</asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Loss">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtloss" runat="server" Height="21px" Width="51px" onkeyup="NumericDecimalCheck(this,5)"
                                                                                    onblur="Spc_validatornumeric(this)" Enabled="True">0</asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField DataField="WHR_Issue_Date" HeaderText="WHR Date" />
                                                                    </Columns>
                                                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                                    <RowStyle BackColor="#FFFBD6" ForeColor="#333333" BorderColor="#FFC080" HorizontalAlign="Center" />
                                                                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                                                                    <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" BorderColor="White" />
                                                                    <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                                                                        Height="20px" Font-Size="10pt" />
                                                                    <AlternatingRowStyle BackColor="White" />
                                                                </asp:GridView>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                                             </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </center>
                                    </fieldset>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" align="center">
                                    <asp:Label ID="lblnotfound" runat="server" Text="" ForeColor="red" Font-Bold="true"
                                        Font-Size="12pt" Visible="false"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 5px" colspan="6">
                                </td>
                            </tr>
                            <tr id="Issuedetails" runat="server" visible="false">
                                <td colspan="6" align="center" valign="top">
                                    <fieldset style="width: 980px; border: 1px solid navy;">
                                        <center>
                                            <div>
                                                 <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                    <ContentTemplate>
                                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                    <tr style="background-color: #0bb6e6; height: 25px">
                                                        <td colspan="4" align="center">
                                                            <asp:Label ID="Label4" runat="server" Text="Issuing Details" ForeColor="whitesmoke"
                                                                Font-Bold="true" Font-Size="12pt"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 10px" colspan="4">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" style="width: 200px">
                                                            <asp:Label ID="lblIssuedBags" runat="server" Text="Issuing Bags" 
                                                                Font-Size="8pt" Font-Bold="True"
                                                                ForeColor="Navy"></asp:Label>
                                                        </td>
                                                        <td align="left" style="width: 200px">
                                                            <asp:TextBox ID="txtissuedbags" runat="server" Width="150px" Height="20px"
                                                                BackColor="LemonChiffon" TabIndex="9" CssClass="tb6"></asp:TextBox>
                                                        </td>
                                                        <td align="left" style="width: 200px">
                                                            <asp:Label ID="lblIssuedweight" runat="server" Style="position: static" Text="Issuing Weight -"
                                                                Font-Bold="True" ForeColor="Navy"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtissuedwt" runat="server" Width="170px" Height="20px"
                                                                BackColor="LemonChiffon" TabIndex="10" CssClass="tb6"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 5px" colspan="4">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <asp:Label ID="lblPercentMoisture" runat="server" Text="Moisture Content (%) -" Font-Size="8pt"
                                                                Font-Bold="true" ForeColor="navy"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtmoisture" runat="server" Width="150px" Height="20px" MaxLength="13"
                                                                TabIndex="11" CssClass="tb6"></asp:TextBox>
                                                        </td>
                                                        <td align="left" style="width: 150px">
                                                            <asp:Label ID="lblTrans" runat="server" Text="Name of Transporter -" Font-Size="8pt"
                                                                Font-Bold="true" ForeColor="navy"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:DropDownList ID="UxTrans" runat="server" Font-Size="8pt" Height="30px" Width="175px"
                                                                TabIndex="14" CssClass="tb6">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 5px" colspan="4">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <asp:Label ID="lblTruckNumber" runat="server" Text="Vehicle No." Font-Size="8pt"
                                                                Font-Bold="true" ForeColor="navy"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txttruckno" runat="server" Width="150px" Height="20px" MaxLength="20"
                                                                TabIndex="16" CssClass="tb6"></asp:TextBox>
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="lblArrivalTime" runat="server" Text="Departure Time" Font-Size="8pt"
                                                                Font-Bold="true" ForeColor="navy"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:DropDownList ID="ddl1" runat="server" Font-Size="8pt" Height="30px" TabIndex="17"
                                                                CssClass="tb6" Width="50px">
                                                                <asp:ListItem Text="01" Value="01"></asp:ListItem>
                                                                <asp:ListItem Text="02" Value="02"></asp:ListItem>
                                                                <asp:ListItem Text="03" Value="03"></asp:ListItem>
                                                                <asp:ListItem Text="04" Value="04"></asp:ListItem>
                                                                <asp:ListItem Text="05" Value="05"></asp:ListItem>
                                                                <asp:ListItem Text="06" Value="06"></asp:ListItem>
                                                                <asp:ListItem Text="07" Value="07"></asp:ListItem>
                                                                <asp:ListItem Text="08" Value="08"></asp:ListItem>
                                                                <asp:ListItem Text="09" Value="09"></asp:ListItem>
                                                                <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                                <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                                                <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <strong>: </strong>
                                                            <asp:DropDownList ID="ddl2" runat="server" Font-Size="8pt" Height="30px" Width="50px"
                                                                TabIndex="18" CssClass="tb6">
                                                            </asp:DropDownList>
                                                            <strong>: </strong>
                                                            <asp:DropDownList ID="ddl3" runat="server" Font-Size="8pt" Height="30px" CssClass="tb6"
                                                                TabIndex="19" Width="50px">
                                                                <asp:ListItem Text="AM" Value="AM"></asp:ListItem>
                                                                <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 5px" colspan="4">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <asp:Label ID="lblTypeVehicle" runat="server" Text="Type of Vehicle" Font-Size="8pt"
                                                                Font-Bold="true" ForeColor="navy"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:DropDownList ID="ddlVehicleType" runat="server" Font-Size="8pt" onChange="DisabledDetails();"
                                                                Width="155px" AutoPostBack="false" TabIndex="15" Height="30px" CssClass="tb6">
                                                                <asp:ListItem Text="Bullock Cart" Value="Bullock Cart"></asp:ListItem>
                                                                <asp:ListItem Selected="True" Text="Truck" Value="Truck"></asp:ListItem>
                                                                <asp:ListItem Text="Tractor" Value="Tractor"></asp:ListItem>
                                                                <asp:ListItem Text="Others" Value="Others"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td align="left">
                                                           <%-- <asp:Label ID="lbIssusedQty" runat="server" Font-Bold="true" Font-Size="8pt" ForeColor="navy"
                                                                Text="Issued Qty : "></asp:Label>--%>
                                                        </td>
                                                        <td align="left">
                                                           <%-- <asp:Label ID="lblIssusedQty" runat="server" Font-Bold="true" Font-Size="8pt" ForeColor="navy"
                                                                Text="0"></asp:Label>--%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 5px" colspan="4">
                                                        </td>
                                                    </tr>
                                                    <%--<tr visible="false" runat="server" id="trOtherDepot">
                                                        <td align="left">
                                                            <asp:Label ID="lblRecDistrict" runat="server" Text="Receipient District" Font-Size="8pt"
                                                                Font-Bold="true" ForeColor="navy"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:DropDownList ID="ddlRecDistrict" runat="server" AutoPostBack="True" DataTextField="District_Name"
                                                                Width="155px" Height="25px" DataValueField="District_Id" TabIndex="12" Font-Size="8pt"
                                                                OnSelectedIndexChanged="ddlRecDistrict_SelectedIndexChanged" CssClass="tb6">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="lblRecDepot" runat="server" Text="Receipient Depot" Font-Size="8pt"
                                                                Font-Bold="true" ForeColor="navy"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:DropDownList ID="ddlRecDepot" runat="server" DataTextField="DepotName" DataValueField="DepotID"
                                                                TabIndex="13" Font-Size="8pt" Width="155px" Height="25px" CssClass="tb6">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>--%>
                                                    <tr>
                                                        <td style="height: 5px" colspan="4">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td  style="height: 5px" align="left">
                                                            <asp:Label ID="lblRecDistrict0" runat="server" Font-Bold="True" Font-Size="8pt" 
                                                                ForeColor="Navy" Text="GatePass Date"></asp:Label>
                                                            <span class="style1">(dd/MM/yyyy)</span></td>
                                                            <td align="left">
                                                            
                                                                <asp:TextBox ID="TextBox2" runat="server" Width="150px" Height="20px"></asp:TextBox>
                                                                <asp:CalendarExtender ID="TextBox2_CalendarExtender" runat="server" 
                                                                    Enabled="True" TargetControlID="TextBox2" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy">
                                                                </asp:CalendarExtender>
                                                           <%--<asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="TextBox2"
                                        ValidChars="@">
                                    </asp:FilteredTextBoxExtender>--%>
                                                            
                                                            </td>
                                                            <td align="left">
                                                            
                                                          <%--  <asp:Label ID="lblisubags" runat="server" Text="Issued Bags:" Font-Size="8pt"
                                                                Font-Bold="True" ForeColor="Navy"></asp:Label>--%>
                                                            
                                                            </td>
                                                            <td align="left">
                                                            
                                                           <%-- <asp:Label ID="lblIssusedBags" runat="server" Font-Bold="True" Font-Size="8pt" ForeColor="Navy"
                                                                Text="0"></asp:Label>--%>
                                                            
                                                            </td>
                                                    </tr>
                                                     <tr>
                                                        <td style="height: 5px" colspan="4">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                      <td align="left">
                                                            <asp:Label ID="Label2" runat="server" Text="Financial Year" Font-Size="8pt"
                                                                Font-Bold="true" ForeColor="navy"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:DropDownList ID="ddlcropyear" runat="server" AutoPostBack="True" Height="30px" Width="150px"
                                                                >
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                     
                                                    <tr>
                                                        <td colspan="3" align="left" visible="true">
                                                            <asp:Label ID="lblDesc" runat="server" Font-Size="8pt" Font-Bold="true" ForeColor="navy"
                                                                Text="Description of Available Papers of Gatepass in Truck"> </asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtGpid" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                                                Width="0px" BorderStyle="None" CssClass="tb6"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4" visible="true">
                                                            <asp:TextBox ID="txtTruckDetails" runat="server" Height="50px" MaxLength="1000" TextMode="MultiLine"
                                                                Width="750px" TabIndex="20" CssClass="tb6"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                                        </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </center>
                                    </fieldset>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 5px" colspan="6">
                                </td>
                            </tr>
                            <tr visible="true" id="Trbutton" runat="server">
                                <td align="center" colspan="6">
                                    <asp:Button ID="btnsave" runat="server" CausesValidation="False" Text="Save Details"
                                        TabIndex="21" CssClass="BTNBLUE" ClientIDMode="Static" 
                                        OnClientClick="ShowBalance();" Enabled="true"
                                        ValidationGroup="SaveValid,WLC_OD,FPS_LS" onclick="btnsave_Click"/>
                                    <asp:Button ID="btnNewMC" runat="server" CausesValidation="False" Text="New Delivery GatePass"
                                         OnClientClick="this.disabled = true; this.value='Refreshing...'" TabIndex="22" UseSubmitBehavior="false" CssClass="BTNBLUE" Enabled="true" />
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 5px" colspan="6">
                                </td>
                            </tr>
                            <tr id="trlnk" runat="server" visible="false">
                                <td runat="server" align="center" colspan="6" visible="true" id="Td1">
                                    <span style="font-size: 8pt; color: blue; text-decoration: underline;"><a href="#"
                                        onclick="OpenWindow(<%= txtGpid.Text %>);"><u><b>Issue GatePass</b></u></a></span>
                                </td>
                            </tr>
                        </table>
                    </div>
              <%--  </ContentTemplate>
            </asp:UpdatePanel>--%>
        </center>
    </fieldset>
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="head">
    <script language="JavaScript1.2">
var message="MPWLC STORAGE MODULE"
var neonbasecolor="gray"
var neontextcolor="yellow"
var flashspeed=100  //in milliseconds

///No need to edit below this line/////

var n=0
if (document.all||document.getElementById){
document.write('<font color="'+neonbasecolor+'">')
for (m=0;m<message.length;m++)
document.write('<span id="neonlight'+m+'">'+message.charAt(m)+'</span>')
document.write('</font>')
}
else
document.write(message)

function crossref(number){
var crossobj=document.all? eval("document.all.neonlight"+number) : document.getElementById("neonlight"+number)
return crossobj
}

function neon(){

//Change all letters to base color
if (n==0){
for (m=0;m<message.length;m++)
//eval("document.all.neonlight"+m).style.color=neonbasecolor
crossref(m).style.color=neonbasecolor
}

//cycle through and change individual letters to neon color
crossref(n).style.color=neontextcolor

if (n<message.length-1)
n++
else{
n=0
clearInterval(flashing)
setTimeout("beginneon()",1500)
return
}
}

function beginneon(){
if (document.all||document.getElementById)
flashing=setInterval("neon()",flashspeed)
}
beginneon()
</script>
    <style type="text/css">
        .auto-style1 {
            height: 29px;
        }
    </style>
</asp:Content>

