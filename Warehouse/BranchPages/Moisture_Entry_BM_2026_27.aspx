<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="~/BranchPages/Moisture_Entry_BM_2026_27.aspx.cs" Inherits="BranchPages_Moisture_Entry_BM_2026_27" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>FCI Moisture Entry</title>

    <link href="https://cdn.jsdelivr.net/npm/bootstrap@3.4.1/dist/css/bootstrap.min.css" rel="stylesheet" />

    <style>
        body {
            background: #f5f5f5;
        }

        .panel-custom {
            background: #fff;
            padding: 20px;
            border-radius: 5px;
            box-shadow: 0 2px 5px rgba(0,0,0,.15);
            margin-top: 20px;
        }

        .section-title {
            background: #337ab7;
            color: white;
            padding: 10px;
            margin-bottom: 15px;
            font-size: 18px;
            font-weight: bold;
        }

        .form-group {
            margin-bottom: 15px;
        }

        .label-value {
            font-weight: bold;
            color: #337ab7;
        }

        .grid-container {
            overflow-x: auto;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">

        <div class="panel-custom">

            <div class="section-title">
                Moisture Entry
            </div>

            <div class="row">
                <div class="col-md-6">
                    <label>Branch Name</label>
                    <asp:Label ID="lblBranchName"
                        runat="server"
                        CssClass="form-control label-value">
                    </asp:Label>
                </div>
            </div>

            <hr />

            <div class="row">

                <div class="col-md-6">
                    <label>Godown</label>
                    <asp:DropDownList ID="ddlGodown"
                        runat="server"
                        CssClass="form-control"
                        AutoPostBack="true"
                        OnSelectedIndexChanged="ddlGodown_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>

                <div class="col-md-6">
                    <label>Stack</label>
                    <asp:DropDownList ID="ddlStack"
                        runat="server"
                        CssClass="form-control"
                        AutoPostBack="true"
                        OnSelectedIndexChanged="ddlStack_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>

            </div>

            <hr />


            <div class="section-title">
                Stack Details
            </div>

            <div class="row">

                <div class="col-md-3">
                    <label>Stack ID</label>
                    <asp:Label ID="lblStackId"
                        runat="server"
                        CssClass="form-control label-value">
                    </asp:Label>
                </div>

                <div class="col-md-3">
                    <label>Stack Name</label>
                    <asp:Label ID="lblStackName"
                        runat="server"
                        CssClass="form-control label-value">
                    </asp:Label>
                </div>

                <div class="col-md-3">
                    <label>Bags</label>
                    <asp:Label ID="lblBags"
                        runat="server"
                        CssClass="form-control label-value">
                    </asp:Label>
                </div>

                <div class="col-md-3">
                    <label>Stack Qty (qty)</label>
                    <asp:Label ID="lblQty"
                        runat="server"
                        CssClass="form-control label-value">
                    </asp:Label>
                </div>
                <div class="col-md-3">
                    <label>Stack Qty (qty)</label>
                    <asp:Label ID="lblCommodity"
                        runat="server"
                        CssClass="form-control label-value">
                    </asp:Label>
                </div>
            </div>

            <br />


            <div class="section-title">
                Moisture Information
            </div>

            <div class="row">

                <div class="col-md-3">
                    <label>Moisture Date</label>
                    <asp:TextBox ID="txtMoistureDate"
                        runat="server"
                        TextMode="Date"
                        CssClass="form-control">
                    </asp:TextBox>
                </div>

                <div class="col-md-3">
                    <label>Average Moisture</label>
                    <asp:TextBox ID="txtAvgMoisture"
                        runat="server"
                        CssClass="form-control">
                    </asp:TextBox>
                </div>

                <div class="col-md-3">
                    <label>Sent FCI / DM</label>
                    <asp:DropDownList ID="ddlSentFCI"
                        runat="server"
                        CssClass="form-control">
                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                        <asp:ListItem Text="No" Value="N"></asp:ListItem>
                    </asp:DropDownList>
                </div>

                <div class="col-md-3">
                    <label>Sent Date</label>
                    <asp:TextBox ID="txtSentDate"
                        runat="server"
                        TextMode="Date"
                        CssClass="form-control">
                    </asp:TextBox>
                </div>

            </div>

            <br />

            <div class="row">
                <div class="col-md-3">
                    <label>Stack Completion Date</label>
                    <asp:TextBox ID="txtStackCompletionDate" runat="server" CssClass="form-control" ReadOnly="true">
                    </asp:TextBox>
                </div>
                <div class="col-md-3">
                    <label>FCI Checked</label>
                    <asp:DropDownList ID="ddlFCIChecked" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Select" Value=""></asp:ListItem>
                        <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                        <asp:ListItem Text="No" Value="N"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-3">
                    <label>FCI Check Date</label>
                    <asp:TextBox ID="txtFCICheckDate" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-3">
                    <label>FCI Moisture</label>
                    <asp:TextBox ID="txtFCIMoisture" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <br />
            <br />
            <div class="text-center">
                <asp:Button ID="btnAddStack" runat="server" Text="Add Stack" CssClass="btn btn-primary" OnClick="btnAddStack_Click" />
            </div>
        </div>
        <br />
        <div class="panel-custom" runat="server" id="divfile" visible="false">
            <div class="section-title">
                Added Stack List
           
            </div>
            <div class="grid-container">
                <asp:GridView ID="gvTempStack"
                    runat="server"
                    CssClass="table table-bordered table-striped"
                    AutoGenerateColumns="false">

                    <Columns>

                        <asp:BoundField DataField="GodownName" HeaderText="Godown" />
                        <asp:BoundField DataField="StackName" HeaderText="Stack" />
                        <asp:BoundField DataField="Commodity" HeaderText="Commodity" />
                        <asp:BoundField DataField="Bags" HeaderText="Bags" />
                        <asp:BoundField DataField="Qty" HeaderText="Qty(Ton)" />
                        <asp:BoundField DataField="MoistureDate" HeaderText="Moisture Date" />
                        <asp:BoundField DataField="AvgMoisture" HeaderText="Avg Moisture" />
                        <asp:BoundField DataField="SentFCI" HeaderText="Sent FCI" />
                        <asp:BoundField DataField="SentDate" HeaderText="Sent Date" />
                        <asp:BoundField DataField="FCIChecked" HeaderText="FCI Checked" />
                        <asp:BoundField DataField="FCICheckDate" HeaderText="FCI Check Date" />
                        <asp:BoundField DataField="FCIMoisture" HeaderText="FCI Moisture" />

                    </Columns>

                </asp:GridView>

            </div>
            <div class="row" style="margin-top: 10px">
                <div class="section-title">Documents Upload</div>
                <div class="col-md-6">
                    <label>Branch Document (PDF)</label>
                    <asp:FileUpload ID="fuBranchDoc" runat="server" CssClass="form-control" AllowMultiple="true" /> 
                    <asp:RequiredFieldValidator ID="rfvBranchDoc" runat="server" ControlToValidate="fuBranchDoc" ErrorMessage="Branch Document Required"
                        ForeColor="Red" ValidationGroup="Save">Branch Document Required</asp:RequiredFieldValidator>
                </div>
                <div class="col-md-6">
                    <label>FCI Document (PDF)</label>
                    <asp:FileUpload ID="fuFCIDoc" runat="server" CssClass="form-control" AllowMultiple="true"/>
                    <asp:RequiredFieldValidator ID="rfvFCIDoc" runat="server" ControlToValidate="fuFCIDoc" ErrorMessage="FCI Document Required"
                        ForeColor="Red" ValidationGroup="Save">FCI Document Required</asp:RequiredFieldValidator>
                </div>
                <div class="text-center">
                    <asp:Button ID="btnSaveStack" runat="server" Text=" Save Stack" ValidationGroup="Save" CssClass="btn btn-success" OnClick="btnSave_Click" Visible="false" />
                </div>
            </div>
        </div>
        <div class="panel-custom">
            <div class="section-title">
                Moisture Entry List
            </div>
            <div class="grid-container">
                <asp:GridView ID="gvData"
                    runat="server"
                    CssClass="table table-bordered table-striped table-hover"
                    AutoGenerateColumns="false">
                    <Columns>
                        <asp:TemplateField HeaderText="S.No">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                        <asp:BoundField DataField="Godown_Name" HeaderText="Godown Name" />
                        <asp:BoundField DataField="StackName" HeaderText="Stack Name" />
                        <asp:BoundField DataField="AvgMoisture" HeaderText="Avg Moisture" />
                        <asp:BoundField DataField="FCIMoisture" HeaderText="FCI Moisture" />
                        <asp:BoundField DataField="MoistureDate" HeaderText="Moisture Date" />
                        <asp:BoundField DataField="SentFCI" HeaderText="Sent FCI" />
                        <asp:BoundField DataField="IsFCIChecked" HeaderText="FCI Checked" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>

