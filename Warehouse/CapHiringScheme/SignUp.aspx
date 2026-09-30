<%@ Page Title="" Language="C#" MasterPageFile="~/CapHiringScheme/Main.master" AutoEventWireup="true" CodeFile="SignUp.aspx.cs" Inherits="SignUp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="body" Runat="Server">
    
<section style="background:#f2f2f2;padding:3em 0">
   <div class="container">
    
            <div class="row">
                <div class="col-xs-12 col-sm-12 col-md-5 col-lg-5 center-block">

                    <div class="panel mt2 login_panel" >
                      <div class="panel-body">

                            <span class="login_icon"><i class="fa fa-user"></i></span>
                            <h3>Sign Up New User</h3>
                            <br />

                        <div class="form-horizontal">

                             <div class="form-group">
                                <label class="control-label col-sm-4">Miller Name</label>
                                <div class="col-sm-7">
                                    <asp:TextBox ID="txtMiller" class="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-sm-1">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtMiller"  ErrorMessage="*" ForeColor="Red"  ValidationGroup="A" runat="server"></asp:RequiredFieldValidator>
                                </div>
                            </div>

                             <div class="form-group">
                                <label class="control-label col-sm-4">Email</label>
                                <div class="col-sm-7">
                                    <asp:TextBox ID="txtEmail" class="form-control" runat="server"></asp:TextBox>

                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" 
                                        ControlToValidate="txtEmail"  ErrorMessage="* Email not correct format" ForeColor="Red"  
                                        ValidationGroup="A" runat="server" 
                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ></asp:RegularExpressionValidator>
                                </div>
                                <div class="col-sm-1">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtEmail"  ErrorMessage="*" ForeColor="Red"  ValidationGroup="A" runat="server"></asp:RequiredFieldValidator>
                                
                                </div>
                            </div>

                             <div class="form-group">
                                <label class="control-label col-sm-4">Mobile No</label>
                                <div class="col-sm-7">
                                    <asp:TextBox ID="txtMobile" class="allow-numeric form-control" runat="server" maxlength="10"></asp:TextBox>

                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"  
                                    ControlToValidate="txtMobile" ErrorMessage="* 10 digit mobile no." ForeColor="Red" ValidationGroup="A"
                                    ValidationExpression="[0-9]{10}"></asp:RegularExpressionValidator> 
                                </div>
                                <div class="col-sm-1">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="txtMobile"  ErrorMessage="*" ForeColor="Red"  ValidationGroup="A" runat="server"></asp:RequiredFieldValidator>
                                </div>
                            </div>


                            <div class="form-group">
                                <label class="control-label col-sm-4">Aadhar No</label>
                                <div class="col-sm-7">
                                    <asp:TextBox ID="txtAadhar" class="allow-numeric form-control" runat="server" MaxLength="12"></asp:TextBox>
                                </div>
                                <div class="col-sm-1">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtAadhar"  ErrorMessage="*" ForeColor="Red"  ValidationGroup="A" runat="server"></asp:RequiredFieldValidator>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="control-label col-sm-4">Pan No</label>
                                <div class="col-sm-7">
                                    <asp:TextBox ID="txtPan" class="form-control" runat="server" MaxLength="10"></asp:TextBox>
                                </div>
                                <div class="col-sm-1">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtPan"  ErrorMessage="*" ForeColor="Red"  ValidationGroup="A" runat="server"></asp:RequiredFieldValidator>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="control-label col-sm-4">Password</label>
                                <div class="col-sm-7">
                                    <asp:TextBox ID="txtPassword" TextMode="Password" class="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-sm-1">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="txtPassword"  ErrorMessage="*" ForeColor="Red"  ValidationGroup="A" runat="server"></asp:RequiredFieldValidator>
                                </div>
                            </div>

                             <div class="form-group">
                                <label class="control-label col-sm-4">Confirm Password</label>
                                <div class="col-sm-7">
                                    <asp:TextBox ID="txtCPassword" TextMode="Password" class="form-control" runat="server"></asp:TextBox>
                                    <asp:CompareValidator ID="CompareValidator1" ControlToCompare="txtPassword" ControlToValidate="txtCPassword" ErrorMessage="* Password does not match" ForeColor="Red"  ValidationGroup="A" runat="server"></asp:CompareValidator>
                                </div>
                                <div class="col-sm-1">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="txtCPassword"  ErrorMessage="*" ForeColor="Red"  ValidationGroup="A" runat="server"></asp:RequiredFieldValidator>
                                </div>

                            </div>

                            <div class="form-group">
                                <div class="col-sm-12 text-center">
                                     <asp:Button ID="btnRegister" Text="Sign Up" CssClass="btn btn-info caps" 
                                     ValidationGroup="A" runat="server" onclick="btnRegister_Click"  />
                                </div>
                            </div>

                          </div>
                           
                      </div>
                      
                    </div> 
                   
                </div>
                <!-- /.col-lg-12 -->
            </div>

   </div>
</section>
        
</asp:Content>


<asp:Content ContentPlaceHolderID="script" runat="server">
    <script type="text/javascript">

        $(document).ready(function () {
            $(".allow-numeric").bind("keypress", function (e) {
                var keyCode = e.which ? e.which : e.keyCode

                if (!(keyCode >= 48 && keyCode <= 57)) {
                    // $(".error").css("display", "inline");
                    return false;
                } else {
                    //  $(".error").css("display", "none");
                }
            });
        });
     
    </script>

</asp:Content>

