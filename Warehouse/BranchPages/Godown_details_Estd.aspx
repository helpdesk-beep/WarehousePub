<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="~/BranchPages/Godown_details_Estd.aspx.cs" Inherits="BranchPages_Godown_details_Estd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../assets/css/style.css" rel="stylesheet" />
    <!-- Bootstrap core CSS -->
    <%--  <link href="../assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/css/bootstrap-theme.min.css" rel="stylesheet" type="text/css" />--%>

    <!-- font awesome -->
    <link href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" />

    <%-- <link href="../assets/css/style.css" rel="stylesheet" type="text/css" />
    <link href="../assets/css/custome.css" rel="stylesheet" type="text/css" />--%>
    <link href="../assets/New/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assets/New/css/bootstrap.min2.css" rel="stylesheet" />
    <link href="../CSS/style.css" rel="stylesheet" />
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />
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
    </style>
    <style type="text/css">
        .left, .right {
            float: left;
            width: 20%; /* The width is 20%, by default */
        }

        .main {
            float: left;
            width: 60%; /* The width is 60%, by default */
        }

        /* Use a media query to add a breakpoint at 800px: */
        @media screen and (max-width: 800px) {
            .left, .main, .right {
                width: 100%; /* The width is 100%, when the viewport is 800px or smaller */
            }
        }
    </style>
    <style type="text/css">
        fieldset {
            border: 1px solid #2095A1;
            padding: 0.35em 0.625em 0.75em;
            margin: 10px;
            border-radius: 5px;
            padding-left: 20px;
        }

        legend {
            padding: 2px 8px;
            border-radius: 10px;
            width: auto;
            border: 1px solid #2095A1;
            font-size: 17px;
            font-weight: bold;
            color: #030203;
        }

        .content-wrapper {
            padding: 1.75rem 1.25rem;
        }

        .table-bordered th, .table-bordered td {
            border: 1px solid #030203;
        }

        .form-control {
            border: 1px solid #767B83;
            border-radius: 8px;
        }

        .table th {
            text-align: center;
        }

        .form-inline {
            display: block !important;
        }

        th.sorting, th.sorting_asc, th.sorting_desc {
            background: #647e68 !important;
            color: black !important;
        }

        .table > tbody > tr > td, .table > tbody > tr > th, .table > tfoot > tr > td, .table > tfoot > tr > th, .table > thead > tr > td, .table > thead > tr > th {
            padding: 8px 5px;
            color: black !important;
        }

        element.style {
            font-size: medium !important;
        }

        .auto-style3 {
            position: relative;
            min-height: 1px;
            float: left;
            width: 50%;
            left: 0px;
            top: 0px;
            padding-left: 15px;
            padding-right: 15px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <asp:Label ID="lblMsg" runat="server"></asp:Label>
        <fieldset>
            <legend>Godown Detail</legend>
            <div class="Row" style="margin-top: 10px">
                <div class="col-md-2" style="margin-top: 5px">
                    <label>Division Name</label>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:DropDownList CssClass="form-control select2" ID="ddldivision" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddldivision_SelectedIndexChanged">
                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-2" style="margin-top: 5px">
                    <label>District Name</label>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <div class="form-group">
                            <asp:DropDownList CssClass="form-control select2" ID="ddldistrict" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged">
                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="col-md-2" style="margin-top: 5px">
                    <label>Branch Name</label>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:DropDownList CssClass="form-control select2" ID="ddlbranch" AutoPostBack="true" runat="server">
                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="Row" style="margin-top: 10px">
                <div class="col-md-2" style="margin-top: 5px">
                    <label>WDRA Compliant</label>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:DropDownList ID="ddlwdra" runat="server" CssClass="form-control" AutoPostBack="true">
                            <asp:ListItem Value="0">Select</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="2">No</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-2" style="margin-top: 5px">
                    <label>Godown Type :</label>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:DropDownList CssClass="form-control select2" ID="ddlgodowntype" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlgodowntype_SelectedIndexChanged">
                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-2" style="margin-top: 5px">
                    <label>Godown Name :</label>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:DropDownList CssClass="form-control select2" ID="ddlGodown" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlGodown_SelectedIndexChanged">
                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

            </div>
            <div class="Row">
                <div class="col-md-2" style="margin-top: 5px">
                    <label>Godown ID</label>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:TextBox runat="server" ReadOnly="true" ID="txtgodownId" CssClass="form-control" AutoComplete="off"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-2" style="margin-top: 5px">
                    <label>Establised</label>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:TextBox runat="server" ReadOnly="true" ID="txtestd" CssClass="form-control" AutoComplete="off"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-2" style="margin-top: 5px">
                    <label>Operation</label>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:TextBox runat="server" ReadOnly="true" ID="txtOpration" CssClass="form-control" AutoComplete="off"></asp:TextBox>
                    </div>
                </div>

            </div>
        </fieldset>
        <fieldset>
            <legend>Godown Stock Detail</legend>
            <div class="Row">
                <div class="col-md-2" style="margin-top: 5px">
                    <label>Crop Year</label>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:DropDownList ID="ddlcropyear" runat="server" CssClass="form-control" AutoPostBack="true" selectionmode="Multiple">
                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-2" style="margin-top: 5px">
                    <label>Depositor Type</label>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:DropDownList ID="ddlDepositorType" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDepositorType_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-2" style="margin-top: 5px">
                    <label>Depositor Name</label>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:DropDownList ID="ddlDepositor" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDepositor_SelectedIndexChanged">
                            <asp:ListItem Text="Select" Value="0">Select</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>

            <div class="row" style="align-content: center" runat="server" id="grddetails" visible="false">
                <div class="col-md-12">
                    <fieldset>
                        <legend>Details</legend>
                        <div class="table-responsive" style="height: 200px;">
                            <asp:GridView runat="server" ID="GrdDipositor" HeaderStyle-Font-Size="Medium"
                                CssClass="table table-bordered table-hover datatable" AutoGenerateColumns="False">
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No" HeaderStyle-Font-Bold="true" HeaderStyle-BackColor="LightBlue">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Depositor ID" HeaderStyle-Font-Bold="true" HeaderStyle-BackColor="LightBlue">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblDepositor_ID" Text='<%# Eval("Depositor_ID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Depositor Name" HeaderStyle-Font-Bold="true" HeaderStyle-BackColor="LightBlue">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblDepositor_Name" Text='<%# Eval("Depositor_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Contact No" HeaderStyle-Font-Bold="true" HeaderStyle-BackColor="LightBlue">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblContact_No" Text='<%# Eval("Contact_No") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Address" HeaderStyle-Font-Bold="true" HeaderStyle-BackColor="LightBlue">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblDepartment_Id" Text='<%# Eval("Address") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>No Record Found</EmptyDataTemplate>
                            </asp:GridView>
                        </div>
                    </fieldset>
                </div>
            </div>
            <fieldset>
                <div class="Row" style="margin-top: 10px">
                    <div class="col-md-2" style="margin-top: 5px">
                        <label>Commodity Type</label>
                    </div>
                    <div class="col-md-2">
                        <div class="form-group">
                            <asp:DropDownList ID="ddlCommoditytype" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCommoditytype_SelectedIndexChanged">
                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-2" style="margin-top: 5px">
                        <label>Commodity</label>
                    </div>
                    <div class="col-md-2">
                        <div class="form-group">
                            <asp:DropDownList ID="ddlcommodity" runat="server" CssClass="form-control" AutoPostBack="true" selectionmode="Multiple">
                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-2" style="margin-top: 5px">
                        <label>WHR Status</label>
                    </div>
                    <div class="col-md-2">
                        <div class="form-group">
                            <asp:DropDownList ID="ddlWHRStatus" runat="server" CssClass="form-control" AutoPostBack="true" selectionmode="Multiple" OnSelectedIndexChanged="ddlWHRStatus_SelectedIndexChanged">
                                <asp:ListItem Value="0">Select</asp:ListItem>
                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                <asp:ListItem Value="2">No</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="Row">
                    <div class="col-md-2" style="margin-top: 5px">
                        <label runat="server" id="lblwhr" visible="false">WHR Issued Status</label>
                    </div>
                    <div class="col-md-2">
                        <div class="form-group">
                            <asp:DropDownList ID="ddlWHRIssued" Visible="false" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlWHRIssued_SelectedIndexChanged">
                                <asp:ListItem Value="0">Select</asp:ListItem>
                                <asp:ListItem Value="1">Online</asp:ListItem>
                                <asp:ListItem Value="2">Offiline</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>

            </fieldset>
            <div class="row" style="align-content: center" runat="server" id="Div1" visible="false">
                <div class="col-md-12">
                    <fieldset>
                        <legend>Details</legend>
                        <div class="table-responsive">
                            <asp:GridView runat="server" ID="grdwhr" HeaderStyle-Font-Size="Medium"
                                CssClass="table table-bordered table-hover datatable" AutoGenerateColumns="False">
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No" HeaderStyle-Font-Bold="true" HeaderStyle-BackColor="LightBlue">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Stack No" HeaderStyle-Font-Bold="true" HeaderStyle-BackColor="LightBlue">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblStack_ID" Text='<%# Eval("Stack_ID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="WHR No" HeaderStyle-Font-Bold="true" HeaderStyle-BackColor="LightBlue">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblWHR_No" Text='<%# Eval("Depositor_whr_id") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Available Qty" HeaderStyle-Font-Bold="true" HeaderStyle-BackColor="LightBlue">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblAvailQty" Text='<%# Eval("AvailQty") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Available Bags" HeaderStyle-Font-Bold="true" HeaderStyle-BackColor="LightBlue">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblAvailBags" Text='<%# Eval("AvailBags") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>No Record Found</EmptyDataTemplate>
                            </asp:GridView>
                        </div>
                    </fieldset>
                </div>
            </div>
        </fieldset>
        <fieldset>
            <div class="row" style="margin-top: 10px">
                <div class="col-md-7" style="margin-top: 5px">
                    <label style="font-size: medium">यदि गोदाम में किसी भी Commodity का स्कंध Offline WHR बना कर रखा गया है :-</label>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:DropDownList ID="ddlofflinewhr" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlofflinewhr_SelectedIndexChanged">
                            <asp:ListItem Value="0">Select</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="2">No</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
        </fieldset>
        <fieldset>
            <div class="Row" style="margin-top: 10px" runat="server" id="divstack" visible="false">
                <div class="col-md-2" style="margin-top: 5px">
                    <label>Stack No</label>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:TextBox runat="server" ID="txtstack" CssClass="form-control" AutoComplete="off"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-2" style="margin-top: 5px">
                    <label>WHR No</label>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:TextBox runat="server" ID="txtWHR" CssClass="form-control" AutoComplete="off"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-2" style="margin-top: 5px">
                    <label>Type of Bags</label>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:DropDownList ID="ddlTypeOfBags" runat="server" CssClass="form-control" AutoPostBack="true">
                            <asp:ListItem Value="0">Select</asp:ListItem>
                            <asp:ListItem Value="1">Gunny Bags</asp:ListItem>
                            <asp:ListItem Value="2">Jute Bags</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="Row" runat="server" id="divstack1" visible="false">
                <div class="col-md-2" style="margin-top: 5px">
                    <label>No. of Bags</label>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:TextBox runat="server" ID="txtBags" CssClass="form-control" AutoComplete="off"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-2" style="margin-top: 5px">
                    <label>Weight In QTY(In QUINTAl)</label>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:TextBox runat="server" ID="txtweight" CssClass="form-control" AutoComplete="off"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-1">
                    <asp:Button runat="server" ID="btnAdd" CssClass="btn btn-info btn-block" ValidationGroup="a" Text="Add" OnClick="btnAdd_Click" />
                </div>
            </div>
            <div class="Row" runat="server" id="offlinestack" visible="false">
                <div class="col-md-12">
                    <fieldset>
                        <legend>Details</legend>
                        <div class="table-responsive">
                            <asp:GridView runat="server" ID="grdstack" HeaderStyle-Font-Size="Medium"
                                CssClass="table table-bordered table-hover datatable" AutoGenerateColumns="False">
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No" HeaderStyle-Font-Bold="true" HeaderStyle-BackColor="LightBlue">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Stack No" HeaderStyle-Font-Bold="true" HeaderStyle-BackColor="LightBlue">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtStack_No" runat="server" Text='<%# Eval("OfflineStackID") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="WHR No" HeaderStyle-Font-Bold="true" HeaderStyle-BackColor="LightBlue">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtWHR_No" runat="server" Text='<%# Eval("OfflineDepositor_WHR_Id") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Available Qty" HeaderStyle-Font-Bold="true" HeaderStyle-BackColor="LightBlue">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtBags" runat="server" Text='<%# Eval("Offline_TotalQtyAvailable") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Available Bags" HeaderStyle-Font-Bold="true" HeaderStyle-BackColor="LightBlue">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtWeight" runat="server" Text='<%# Eval("Offline_TotalBagsAvailable") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>No Record Found</EmptyDataTemplate>
                            </asp:GridView>
                        </div>
                    </fieldset>
                </div>
            </div>
        </fieldset>
        <fieldset>
            <div>
                 <h5 style="color: red">टोटल बैग्स  और टोटल वेट में  एन्ट्री  करते  समय यह सुनिश्चित करले  की ऑनलाइन  और ऑफलाइन दोनों की एंट्रिया मिलाकर यह पर फील करना है !<br />क्वांटिटी को क्विंटल में दर्ज करे/In QUINTAl</h5>
            </div>
            <br />
           <%-- <div class="Row">
                <div class="col-md-6">
                    <h5 style="color: red">टोटल बैग्स  और टोटल वेट में  एन्ट्री  करते  समय यह सुनिश्चित करले  की ऑनलाइन  और ऑफलाइन दोनों की एंट्रिया मिलाकर यह पर फील करना है !<br />क्वांटिटी को क्विंटल में दर्ज करे/In QUINTAl</h5>
                </div>
            </div>--%>
            <div class="row">
                <div class="col-md-2" style="margin-top: 5px">
                    <label>Total Available Bags in No</label>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:TextBox runat="server" ID="txtTotalAvailableBags" CssClass="form-control" AutoComplete="off"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-2" style="margin-top: 5px">
                    <label>Total Available Weight In QTY(In QUINTAl)</label>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:TextBox runat="server" ID="txtTotalAvailableWeight" CssClass="form-control" AutoComplete="off"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="Row" style="margin-top: 10px">
                <div class="col-md-2" style="margin-top: 5px">
                    <label>Remark</label>
                </div>
                <div class="col-md-6">
                    <div class="form-group">
                        <asp:TextBox runat="server" ID="txtremarks" CssClass="form-control" TextMode="MultiLine" AutoComplete="off"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-5"></div>
                <div class="col-md-2">
                    <asp:Button runat="server" ID="btnsave" CssClass="btn btn-info btn-block" ValidationGroup="a" Text="SUBMIT" OnClick="btnsave_Click" />
                </div>
            </div>

        </fieldset>
    </div>
</asp:Content>

