<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/SuperAdmin.master" AutoEventWireup="true" CodeFile="LastThreeYearStockPositionEntryByBM.aspx.cs" Inherits="Admin_LastThreeYearStockPositionEntryByBM" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href=”https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/0.1.0/css/footable.min.css”
        rel=”stylesheet” type=”text/css” />
    <script type="text/javascript" src="<%= ResolveUrl("~/NEW_CSS/js/jquery-1.12.4.js") %>"></script>
    <script type=”text/javascript” src=”https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/0.1.0/js/footable.min.js”></script>
     <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
    <script type=”text/javascript”>
        $(function () {
            $(‘[id*=GV_CommodityInfo]’).footable();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <h4>गोदामों में भंडारित स्कंध की जानकारी (अवधि अनुसार)</h4>
    <hr />
    <%-- <asp:UpdatePanel runat="server">--%>
    <%--<ContentTemplate>--%>
    <div class="col-md-12">
        <div class="form-group">
            <div class="col-sm-8 col-sm-offset-4">
                <asp:Label ID="lblmsg" runat="server"></asp:Label>
                <asp:HiddenField ID="hfId" Value="0" runat="server" />
                <asp:HiddenField ID="hfFileName" Value="0" runat="server" />
            </div>
        </div>

        <div class="form-group" id="divregion" runat="server" visible="true">
            <div class="col-sm-3">
                <label class="control-label">Region</label>
                <asp:DropDownList ID="ddlregion" CssClass="form-control" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlregion_SelectedIndexChanged">
                    <asp:ListItem Value="0">Select</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="ddlregion" ValidationGroup="A" ErrorMessage="* required" ForeColor="Red" InitialValue="0" runat="server"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="form-group" id="divdistrict" runat="server" visible="true">
            <div class="col-sm-3">
                <label class="control-label">District</label>
                <asp:DropDownList ID="ddldistrict" CssClass="form-control" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged">
                    <asp:ListItem Value="0">Select</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="ddldistrict" ValidationGroup="A" ErrorMessage="* required" ForeColor="Red" InitialValue="0" runat="server"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="form-group" id="divbranch" runat="server" visible="true">
            <div class="col-sm-3">
                <label class="control-label">Branch</label>
                <asp:DropDownList ID="ddlbranch" CssClass="form-control" AutoPostBack="true" runat="server">
                    <asp:ListItem Value="0">Select</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="ddlbranch" ValidationGroup="A" ErrorMessage="* required" ForeColor="Red" InitialValue="0" runat="server"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="form-group" id="div2" runat="server" visible="true">
            <div class="col-sm-3">
                <label class="control-label">Godown Type</label>
                <asp:DropDownList ID="ddlgodowntype" CssClass="form-control" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlgodowntype_SelectedIndexChanged">
                    <asp:ListItem Value="0">Select</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="ddlgodowntype" ValidationGroup="A" ErrorMessage="* required" ForeColor="Red" InitialValue="0" runat="server"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="form-group" id="divgodown" runat="server" visible="true">
            <div class="col-sm-3">
                <label class="control-label">Godown</label>
                <asp:DropDownList ID="ddlgodown" CssClass="form-control" runat="server">
                    <asp:ListItem Value="0">Select</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="ddlgodown" ValidationGroup="A" ErrorMessage="* required" ForeColor="Red" InitialValue="0" runat="server"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="form-group">
            <div class="col-sm-3">
                <label class="control-label">Depositer</label>
                <asp:DropDownList ID="ddlrole" CssClass="form-control" AutoPostBack="false" runat="server" OnSelectedIndexChanged="ddlrole_SelectedIndexChanged">
                    <asp:ListItem Value="0">Select</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="ddlrole" ValidationGroup="A" ErrorMessage="* required" ForeColor="Red" InitialValue="0" runat="server"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="form-group" id="div1" runat="server" visible="true">
            <div class="col-sm-3">
                <label class="control-label">Commodity</label>
                <asp:DropDownList ID="ddlcommodity" CssClass="form-control" runat="server">
                    <asp:ListItem Value="0">Select</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="ddlcommodity" ValidationGroup="A" ErrorMessage="* required" ForeColor="Red" InitialValue="0" runat="server"></asp:RequiredFieldValidator>
            </div>
        </div>
        <br />
        <div class="form-group"></div>


    </div>
    <div class="col-md-12">
        <div class="form-group">
            <div class="col-sm-offset-4 col-sm-8">
            </div>
        </div>
    </div>
    <h1>मात्रा मे.टन में प्रविष्ट करे</h1>
    <div class="col-md-12">
        <div class="table-responsive">

            <div style="overflow-x: scroll;" >
                <%--<asp:GridView ID="gvGodown" runat="server" AutoGenerateColumns="False" ShowFooter="true" Width="100%"
                         EnableModelValidation="True"  
                         GridLines="Horizontal" onrowdeleting="gvGodown_RowDeleting" AutoGenerateDeleteButton="false"
                           CellPadding="4" rules="all" ForeColor="#333333">
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <RowStyle BackColor="White" CssClass="ADFieldsGridText" HorizontalAlign="Left" />
                           
                            <Columns>
                            <asp:BoundField DataField="RowNumber" HeaderText="Godown No." />
                             <asp:TemplateField HeaderText="Licence Type">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlgdwntype" runat="server" Height="21px" Width="70px"> 
                                                <asp:ListItem Value="-1">--Select--</asp:ListItem>                                  
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Length in Feet">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtLenght" runat="server" Width="70px" Text='<%# Eval("Lenght") %>'>0</asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Width in Feet">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtWidth" runat="server" Width="70px" Text='<%# Eval("Width") %>'>0</asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Height in Feet">
                                    <ItemTemplate>
                                         <asp:TextBox ID="txtHeight" runat="server" Width="70px" Text='<%# Eval("Height") %>'>0</asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Select">
                                   <ItemTemplate>
                                     <asp:CheckBox ID="ckstack" runat="server" AutoPostBack="True" OnCheckedChanged="ckstack_CheckedChanged" />
                                    </ItemTemplate>
                                   </asp:TemplateField>
                                <asp:TemplateField HeaderText="Capacity (MT)">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtCapacity" runat="server" Enabled="false" Width="70px" Text='<%# Eval("Capacity") %>'>0</asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Construction Year">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtConstY" runat="server" Width="70px" Text='<%# Eval("ConstY") %>'>0</asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                           
 
                               
                                <asp:TemplateField HeaderText="Licence No./Application No." HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:TextBox ID="Gtxtlicno" runat="server" Width="150px"  Font-Bold="true" Height="15px" align="Center" Text='<%# Eval("LNo") %>' placeholder="Enter Licence Number"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Lic. Issue/Application Date">
                                    <ItemTemplate>
                                        <asp:TextBox ID="GtxtLicIssuedate" runat="server" Width="70px" Font-Bold="true" Height="15px"  Text='<%# Eval("LIssueDate") %>' onkeydown="javascript:preventInput(event);" onpaste="return false;"></asp:TextBox>
                                                       
                                    </ItemTemplate>
                                </asp:TemplateField>                                 
                                <asp:TemplateField HeaderText="Lic. Expiry Date">
                                    <ItemTemplate>
                                        <asp:TextBox ID="GtxtLicExpdate" runat="server" Width="80px" Font-Bold="true" Height="15px" Text='<%# Eval("LExpDate") %>' onkeydown="javascript:preventInput(event);" onpaste="return false;"></asp:TextBox>
                                    
                                    </ItemTemplate>
             <FooterStyle HorizontalAlign="Right" />
            <FooterTemplate>
             <asp:Button ID="ButtonAdd" runat="server" Text="Add Godown" Width="80px"  Font-Size="12px"
                    onclick="ButtonAdd_Click" />
            </FooterTemplate>                                    
                                </asp:TemplateField>                          
                            </Columns>
                            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                            <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                            <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                            <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        </asp:GridView>--%>
                <%--<asp:GridView ID="gvGodown" runat="server" Width="100%" AutoGenerateColumns="False" ShowFooter="true"
                                CellPadding="4" rules="all" ForeColor="#333333" onrowdeleting="gvGodown_RowDeleting">
                                <RowStyle BackColor="White" CssClass="ADFieldsGridText" HorizontalAlign="Left" />
                                <Columns>
                                     <asp:TemplateField HeaderText="क्रमांक">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1%>
                                        </ItemTemplate>
                                        <ItemStyle Width="1px" />
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateField>
                             <asp:TemplateField HeaderText="क्राप ईयर">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlgdwntype" runat="server" Width="100px"> 
                                                <asp:ListItem Value="-1">Select</asp:ListItem>                                  
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="6 माह से भण्‍डारित मात्रा">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt6thmonth" runat="server" Width="70px" Text='<%# Eval("6thmonth") %>'>0</asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="9 माह से भण्‍डारित मात्रा">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt9thmonth" runat="server" Width="70px" Text='<%# Eval("9thmonth") %>'>0</asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="12 माह से भण्‍डारित मात्रा">
                                    <ItemTemplate>
                                         <asp:TextBox ID="txt12thmonth" runat="server" Width="70px" Text='<%# Eval("12thmonth") %>'>0</asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                              
                                <asp:TemplateField HeaderText="18 माह से भण्‍डारित मात्रा">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt18thmonth" runat="server" Width="70px" Text='<%# Eval("18thmonth") %>'>0</asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                               
                                <asp:TemplateField HeaderText="24 माह से भण्‍डारित मात्रा">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt24month" runat="server" Width="70px"  Text='<%# Eval("24month") %>'>0</asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="30 माह से भण्‍डारित मात्रा">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt30thmonth" runat="server" Width="70px" Text='<%# Eval("30thmonth") %>'>0</asp:TextBox>
                                                       
                                    </ItemTemplate>
                                </asp:TemplateField>                                 
                                <asp:TemplateField HeaderText="36 माह से भण्‍डारित मात्रा">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt36thmonth" runat="server" Width="70px" Text='<%# Eval("36thmonth") %>'>0</asp:TextBox>
                                    
                                    </ItemTemplate>
             <FooterStyle HorizontalAlign="Right" />
            <FooterTemplate>
             <asp:Button ID="ButtonAdd" runat="server" Text="Add Godown" Width="80px"  Font-Size="12px"
                    onclick="ButtonAdd_Click" />
            </FooterTemplate>                                    
                                </asp:TemplateField>                          
                            </Columns>
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <AlternatingRowStyle BackColor="#e1f3ff" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                 <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                            <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                            <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                            <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                            </asp:GridView>--%>

                <asp:GridView ID="GV_CommodityInfo" runat="server" AutoGenerateColumns="False" Width="93%" BackColor="White"
                    EnableModelValidation="True"
                    CellPadding="4" rules="all" ForeColor="#333333" ShowFooter="true"
                    OnRowDataBound="GV_CommodityInfo_OnRowDataBound">
                    <RowStyle BackColor="White" CssClass="ADFieldsGridText table-responsive" HorizontalAlign="Left" />
                    <Columns>

                        <asp:BoundField DataField="RowNumber" HeaderText="S.No" ItemStyle-Width="10px" />

                        <asp:TemplateField HeaderText="Crop Year">
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlCropYear" runat="server" Height="21px" Width="70px">
                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                    <asp:ListItem Value="2020-21">2020-21</asp:ListItem>
                                    <asp:ListItem Value="2021-22">2021-22</asp:ListItem>
                                    <asp:ListItem Value="2022-23">2022-23</asp:ListItem>
                                    <asp:ListItem Value="2023-24">2023-24</asp:ListItem>
                                    <asp:ListItem Value="2024-25">2024-25</asp:ListItem>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="6 माह से भण्‍डारित मात्रा">
                            <ItemTemplate>
                                <asp:TextBox ID="txtSixMonth" runat="server" Width="70px" Text='<%# Eval("StockPositionIn6Months") %>'>0</asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="9 माह से भण्‍डारित मात्रा">
                            <ItemTemplate>
                                <asp:TextBox ID="txtNineMonth" runat="server" Width="70px" Text='<%# Eval("StockPositionIn9Months") %>'>0</asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="12 माह से भण्‍डारित मात्रा">
                            <ItemTemplate>
                                <asp:TextBox ID="txtTwelveMonth" runat="server" Width="70px" Text='<%# Eval("StockPositionIn12Months") %>'>0</asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="18 माह से भण्‍डारित मात्रा">
                            <ItemTemplate>
                                <asp:TextBox ID="txtEighteenMonth" runat="server" Width="70px" Text='<%# Eval("StockPositionIn18Months") %>'>0</asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="24 माह से भण्‍डारित मात्रा">
                            <ItemTemplate>
                                <asp:TextBox ID="txtTwentyFourMonth" runat="server" Width="70px" Text='<%# Eval("StockPositionIn24Months") %>'>0</asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="30 माह से भण्‍डारित मात्रा">
                            <ItemTemplate>
                                <asp:TextBox ID="txtThirtyMonth" runat="server" Width="70px" Text='<%# Eval("StockPositionIn30Months") %>'>0</asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="36 माह से भण्‍डारित मात्रा">
                            <ItemTemplate>
                                <asp:TextBox ID="txtThirtySixMonth" runat="server" Width="70px" Text='<%# Eval("StockPositionIn36Months") %>'>0</asp:TextBox>
                            </ItemTemplate>

                            <FooterStyle HorizontalAlign="Right" />
                            <FooterTemplate>
                                <asp:Button ID="btnAddRow" runat="server" Text="Add New Row" Width="80px" Font-Size="12px"
                                    OnClick="ButtonAdd_Click" />
                            </FooterTemplate>
                        </asp:TemplateField>
                    </Columns>
                   <%-- <FooterStyle BackColor="#719cb6" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" Font-Size="12pt" />
                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                        Height="20px" Font-Size="12pt" />
                    <AlternatingRowStyle BackColor="#eeeeee" />--%>
                </asp:GridView>
            </div>
        </div>
    </div>
    <div class="col-md-12">
        <div class="form-group">
            <div class="col-sm-offset-4 col-sm-8">
                <asp:Button ID="btnSave" CssClass="btn btn-info" ValidationGroup="A"
                    runat="server" Text="SAVE" OnClick="btnSubmit_Click" />

               <%-- <asp:Button ID="btnCancel" CssClass="btn btn-default" Text="Cancel"
                    runat="server" OnClick="btnCancel_Click" />--%>
            </div>
        </div>
    </div>
    <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="contbootm" runat="Server">
    <script src="../assets/js/bootstrap-datepicker.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $('[id*=txtExpDate]').datepicker({
                changeMonth: true,
                changeYear: true,
                format: "dd/mm/yyyy",
                language: "tr"
            });
        });
    </script>
    <style>
        tbody, td, tfoot, th, thead, tr {
            width: 10%;
            font-size: 1.05em;
            padding: 10px;
            border-color: #3D0859;
            border-style: solid;
            border-width: 3px !important;
        }
    </style>
</asp:Content>
