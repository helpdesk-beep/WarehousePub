<%@ Page Title="" Debug="true" ViewStateEncryptionMode="Always" Language="C#" MasterPageFile="~/MasterPages/BranchMasters.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Branch_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PM" runat="Server">

    <div class="card" style="height: 650px;">
        <div class="card-header deep-orange lighten-1 white-text">

            <b>Welcome to your dashboard&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</b>
            &nbsp;<b style="text-align:right;"><a href="Employee_Details.aspx" class="drop">Update Branch Profile</a></b>
        </div>

        <div>
            <div class="box-header  text-center with-border">
                <p style="text-align: left; font-size: 20px;"><i class="fa fa-home" aria-hidden="true"></i>Welcome to your dashboard </p>
                <p style="text-align: right; font-size: 20px;">
                    (current posting details) <i class="fa fa-level-down"></i>
                </p>

            </div>

            <div class="box-body bg-green" style="background-color:#808080;">
                <div class="form-group">
                    <div class="col-md-2">
                        <asp:Image ID="IMGEmployee" runat="server" Height="90px" Width="90px" CssClass="img-circle img-responsive text-center" />
                    </div>

                    <div class="col-md-5">

                        <div class="form-group">
                            <asp:Label ID="Label1" runat="server" Text="Name :" Font-Bold="true"></asp:Label>
                            <asp:Label ID="lblEmpName" runat="server" Text="--" ForeColor="White"></asp:Label>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="Label2" runat="server" Text="Branch Name :" Font-Bold="true"></asp:Label>
                            <asp:Label ID="lbldist" runat="server" Text="--" ForeColor="White"></asp:Label>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="Label3" runat="server" Text="EmailID:- " Font-Bold="true"></asp:Label>
                            <asp:Label ID="lblEmpID" runat="server"  Text="--" ForeColor="White"></asp:Label>
                        </div>
                         <div class="form-group">
                            <asp:Label ID="Label8" runat="server" Text="Total Branch Capacity(M.T.):-" Font-Bold="true"></asp:Label>
                            <asp:Label ID="lblTBC" runat="server"  Text="--"  Font-Underline="true" ForeColor="White"></asp:Label>
                        </div>
                    </div>
                    <div class="col-md-5">
                        <div class="form-group">
                            <asp:Label ID="Label4" runat="server" Text="Date of Joining :" Font-Bold="true"></asp:Label>
                            <asp:Label ID="lblDOJ" runat="server" Text="--" ForeColor="White"></asp:Label>
                        </div>
                        <div class=" form-group">
                            <asp:Label ID="Label7" runat="server" Text="Designation :" Font-Bold="true"></asp:Label>
                            <asp:Label ID="lbldesignation" runat="server" Text="Branch Manager" ForeColor="White"></asp:Label>
                        </div>
                        <div class=" form-group">
                            <asp:Label ID="Label5" runat="server" Text="Mobile Number :" Font-Bold="true"></asp:Label>
                            <asp:Label ID="lblmobile" runat="server" Text="" ForeColor="White"></asp:Label>
                        </div>
                         <div class="form-group">
                            <asp:Label ID="Label10" runat="server" Text="Total Branch Utilized Capacity(M.T.):- " Font-Bold="true"></asp:Label>
                            <asp:Label ID="lblTBUC" runat="server"  Text="--" Font-Underline="true" ForeColor="White"></asp:Label>
                        </div>
                        <div class="row"></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

