<%@ Page Title="" Language="C#" MasterPageFile="~/CapHiringScheme/Main.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="body" Runat="Server">
<section style="background:#f4f4f4;padding:3em 0">
   <div class="container">
            <div class="row">
                <div class="col-xs-12 col-sm-12 col-md-4 col-lg-4 center-block">
                   <div class="panel mt2 login_panel" >
                    
                      <div class="panel-body">
                        
                         <span class="login_icon"><i class="fa fa-user"></i></span>
                            <h3>Login User</h3>

                             <div class="form-group">
                                <label>Email</label>
                                <asp:TextBox ID="txtEmail" class="form-control" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ControlToValidate="txtEmail"  ErrorMessage="*" ForeColor="Red"  ValidationGroup="B" runat="server"></asp:RequiredFieldValidator>
                            </div>
                             
                            <div class="form-group">
                                <label>Password</label>
                                <asp:TextBox ID="txtPassword" TextMode="Password" class="form-control" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator15" ControlToValidate="txtPassword"  ErrorMessage="*" ForeColor="Red"  ValidationGroup="B" runat="server"></asp:RequiredFieldValidator>
                            </div>

                            <div class="form-group">
                                <label>Enter Code</label>
                                <asp:TextBox ID="txtCapcha" class="form-control" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="txtCapcha"  ErrorMessage="*" ForeColor="Red"  ValidationGroup="B" runat="server"></asp:RequiredFieldValidator>
                                <small class="help-block red text-left"><i>(Captcha Code is Case Sensitive)</i></small>

                                <br />
                                <asp:Image ID="imgCaptcha" runat="server" Style="width: 120px; height: 25px;" />
                                
                            </div>

                            <div class="form-group">
                                    <asp:Button ID="btnLogin" Text="Login" CssClass="btn btn-info btn-block caps" 
                                     ValidationGroup="B" runat="server" onclick="btnLogin_Click"  />
                            </div>

                      </div>
                      
                    </div> 
                </div>
             </div>
   </div>
</section>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="script" Runat="Server">
</asp:Content>

