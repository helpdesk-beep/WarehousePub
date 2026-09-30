<%@ Page Title="" Language="C#" MasterPageFile="~/CapHiringScheme/AdminDashboard/Admin.master"
    AutoEventWireup="true" CodeFile="RegisteredMillerDetails.aspx.cs" Inherits="CapHiringScheme_AdminDashboard_RegisteredMillersDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <!-- page-wrapper -->
    <div id="page-wrapper">
        <div class="row">
            <div class="col-md-12">
                <asp:Repeater ID="rptRegMillerDetails" runat="server">
                    <ItemTemplate>
                        <div class="panel box-primary">
                            <div class="panel-header with-border">
                                <h3 class="panel-title">
                                    Personal Details</h3>
                                <hr />
                            </div>
                            <!-- /.box-header -->
                            <div class="panel-body">
                                <div class="row">
                                    <asp:HiddenField ID="hdnMillId" Value='<%#Eval("Mill_Id")%>' runat="server" />
                                    <asp:HiddenField ID="hdnMRegId" runat="server" />
                                    <div class="form-group col-md-4">
                                        <label>
                                            Registration Id :
                                        </label>
                                        <span class="text-primary">
                                            <label class="text-primary">
                                                <%#Eval("Registration_ID")%></label>
                                        </span>
                                    </div>
                                    <div class="form-group col-md-4">
                                        <label>
                                            Registration Date :
                                        </label>
                                        <span class="text-primary">
                                            <label class="text-primary">
                                                <%#Eval("CreateOn", "{0:dd/M/yyyy}")%></label>
                                        </span>
                                    </div>
                                    <div class="form-group col-md-4">
                                        <label>
                                            Miller Name :</label>
                                        <span class="text-primary">
                                            <label class="text-primary">
                                                <%#Eval("Miller")%></label>
                                        </span>
                                    </div>
                                    <div class="form-group col-md-4">
                                        <label>
                                            Mobile No. :
                                        </label>
                                        <span class="text-primary">
                                            <label class="text-primary">
                                                <%#Eval("Mobile")%></label>
                                        </span>
                                    </div>
                                    <div class="form-group col-md-4">
                                        <label>
                                            Email :</label>
                                        <span class="text-primary">
                                            <label class="text-primary">
                                                <%#Eval("Email")%></label>
                                        </span>
                                    </div>
                                    <div class="form-group col-md-4">
                                        <label>
                                            Aadhar No. :
                                        </label>
                                        <span class="text-primary">
                                            <label class="text-primary">
                                                <%#Eval("Aadhar")%></label>
                                        </span>
                                    </div>
                                    <div class="form-group col-md-4">
                                        <label>
                                            Pan No. :
                                        </label>
                                        <span class="text-primary">
                                            <label class="text-primary">
                                                <%#Eval("Pan")%></label>
                                        </span>
                                    </div>
                                </div>
                                <!-- /.row -->
                            </div>
                            <!-- /.box-body -->
                            <div class="panel-header with-border">
                                <h3 class="panel-title">
                                    मिलर के मिल का विवरण
                                </h3>
                                <hr />
                            </div>
                            <!-- /.box-header -->
                            <div class="panel-body">
                                <div class="row">
                                    <div class="form-group col-md-4">
                                        <label>
                                            Mill Name :</label>
                                        <label class="text-primary">
                                            <%#Eval("Mill_Name")%></label>
                                    </div>
                                    <div class="form-group col-md-4">
                                        <label>
                                            Capacity of Rice Miller (in MT per day) :</label>
                                        <label class="text-primary">
                                            <%#Eval("Rice_Capacity", "{0:#}")%></label>
                                    </div>
                                    <div class="form-group col-md-4">
                                        <label>
                                            Office Contact No./ Mobile No. :</label>
                                        <label class="text-primary">
                                            <%#Eval("Office_Contact")%></label>
                                    </div>
                                    <div class="form-group col-md-4">
                                        <label>
                                            Landmark Near Mills :</label>
                                        <label class="text-primary">
                                            <%#Eval("Mill_Landmark")%></label>
                                    </div>
                                    <div class="form-group col-md-4">
                                        <label>
                                            District :</label>
                                        <label class="text-primary">
                                            <%#Eval("District_Name")%></label>
                                    </div>
                                    <div class="form-group col-md-4">
                                        <label>
                                            Tehsil :</label>
                                        <label class="text-primary">
                                            <%#Eval("Tehsil_Name")%></label>
                                    </div>
                                    <div class="form-group col-md-4">
                                        <label>
                                            Block :</label>
                                        <label class="text-primary">
                                            <%#Eval("Block_Name")%></label>
                                    </div>
                                    <div class="form-group col-md-4">
                                        <label>
                                            Nearest Branch of MPWLC :
                                        </label>
                                        <label class="text-primary">
                                            <%#Eval("BranchName")%></label>
                                    </div>
                                    <div class="form-group col-md-4">
                                        <label>
                                            Distance from nearest branch of MPWLC (in KM):</label>
                                        <label class="text-primary">
                                            <%#Eval("Near_Distance")%></label>
                                    </div>
                                    <div class="form-group col-md-4">
                                        <label>
                                            Mills/ Office Address with Postal Address :</label>
                                        <label class="text-primary">
                                            <%#Eval("Mill_Office_Address")%></label>
                                    </div>
                                </div>
                                <!-- /.row -->
                            </div>
                            <!-- /.box-body -->
                            <div class="panel-header with-border">
                                <h3 class="panel-title">
                                    जिला उद्योग से मिलिंग हेतु प्राप्त रजिस्ट्रेशन का विवरण
                                </h3>
                                <hr />
                            </div>
                            <!-- /.box-header -->
                            <div class="panel-body">
                                <div class="row">
                                    <div class="form-group col-md-4">
                                        <label>
                                            Registration No :</label>
                                        <label class="text-primary">
                                            <%#Eval("Dist_Ind_RegNo")%></label>
                                    </div>
                                    <div class="form-group col-md-4">
                                        <label>
                                            Issue Date :
                                        </label>
                                        <label class="text-primary">
                                            <%#Eval("RegNo_Issue_Date", "{0:dd/M/yyyy}")%>
                                        </label>
                                    </div>
                                    <div class="form-group col-md-4">
                                        <label>
                                            Attached Document :
                                        </label>
                                        <label class="text-primary">
                                            <asp:LinkButton ID="lnkDownloadFile" OnClick="lnkDownloadFile_OnClick" runat="server">Download File</asp:LinkButton></label>
                                    </div>
                                </div>
                                <!-- /.row -->
                            </div>
                            <!-- /.box-body -->
                            <div class="panel-header with-border">
                                <h3 class="panel-title">
                                    <%#Eval("Mpscsc_Markfed")%>
                                    से मिलिंग हेतु किये गए एग्रीमेंट का विवरण</h3>
                                <hr />
                            </div>
                            <!-- /.box-header -->
                            <div class="panel-body">
                                <div class="row">
                                    <div class="form-group col-md-4">
                                        <label>
                                            Miller Registration Id :</label>
                                        <label class="text-primary">
                                            <%#Eval("Agree_Miller_Id")%></label>
                                    </div>
                                    <div class="form-group col-md-4">
                                        <label>
                                            Agreement Date :
                                        </label>
                                        <label class="text-primary">
                                            <%#Eval("Agree_Date", "{0:dd/M/yyyy}")%>
                                        </label>
                                    </div>
                                    <div class="form-group col-md-4">
                                        <label>
                                            Agreement Capacity (in MT) :</label>
                                        <label class="text-primary">
                                            <%#Eval("Agree_Capacity", "{0:#}")%></label>
                                    </div>
                                </div>
                            </div>
                            <!-- /.row -->
                            <!-- /.box-body -->
                            <div class="panel-header with-border">
                                <h3 class="panel-title">
                                    <%#Eval("Mpscsc_Markfed")%>
                                    से धान (Paddy) को स्‍वतंत्र रूप से रखने की क्षमता का विवरण
                                    <hr />
                            </div>
                            <!-- /.box-header -->
                            <div class="panel-body">
                                <div class="row">
                                    <div class="form-group col-md-4">
                                        <label>
                                            Paddy Capacity (in MT) :</label>
                                        <label class="text-primary">
                                            <%#Eval("Paddy_Capacity", "{0:#}")%>
                                        </label>
                                    </div>
                                </div>
                                <!-- /.row -->
                            </div>
                            <!-- /.box-body -->
                            <div class="panel-header with-border">
                                <h3 class="panel-title">
                                    Miller Incharge/Manager Details
                                </h3>
                                <hr />
                            </div>
                            <!-- /.box-header -->
                            <div class="panel-body">
                                <div class="row">
                                    <div class="form-group col-md-4">
                                        <label>
                                            Authorised Person :</label>
                                        <label class="text-primary">
                                            <%#Eval("Incharge_Peson")%></label>
                                    </div>
                                    <div class="form-group col-md-4">
                                        <label>
                                            Designation :
                                        </label>
                                        <label class="text-primary">
                                            <%#Eval("Incharge_Post")%></label>
                                    </div>
                                    <div class="form-group col-md-4">
                                        <label>
                                            Email ID :</label>
                                        <label class="text-primary">
                                            <%#Eval("Incharge_Email")%></label>
                                    </div>
                                    <div class="form-group col-md-4">
                                        <label>
                                            Mobile No. :</label>
                                        <label class="text-primary">
                                            <%#Eval("Incharge_Mobile")%></label>
                                    </div>
                                    <div class="form-group col-md-4">
                                        <label>
                                            Authorised Person Address with Postal Address :</label>
                                        <label class="text-primary">
                                            <%#Eval("Incharge_Address")%></label>
                                    </div>
                                </div>
                                <!-- /.row -->
                            </div>
                            <!-- /.box-body -->
                            <div class="panel-header with-border">
                                <h3 class="panel-title">
                                    Geogrophical Information
                                </h3>
                                <hr />
                            </div>
                            <!-- /.box-header -->
                            <div class="panel-body">
                                <div class="row">
                                    <div class="form-group col-md-6">
                                        <label>
                                            Mills Latitude :</label>
                                        <label class="text-primary">
                                            <%#Eval("Mill_Lat")%></label>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label>
                                            Mills Longitude :
                                        </label>
                                        <label class="text-primary">
                                            <%#Eval("Mill_Long")%></label>
                                    </div>
                                </div>
                                <!-- /.row -->
                            </div>
                            <!-- /.box-body -->
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <!-- /.col-lg-12 -->
        </div>
        <!-- /.row -->
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">
</asp:Content>
