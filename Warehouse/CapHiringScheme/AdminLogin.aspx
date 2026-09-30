<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdminLogin.aspx.cs" Inherits="AdminLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="description" content="">
    <meta name="author" content="">

    <title>Cap Hiring Admin Login</title>

   <!-- CSS -->

      <!-- bootstrap -->
      <link rel="stylesheet" type="text/css" href="assets/css/bootstrap.css">
      <link rel="stylesheet" type="text/css" href="assets/css/bootstrap-theme.css">
 

      <!-- font awesome -->
      <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.5.0/css/font-awesome.min.css">
      <link href='https://fonts.googleapis.com/css?family=Open+Sans' rel='stylesheet' type='text/css'>

      <link href="assets/css/style.css" rel="stylesheet">
      <link href="assets/css/customize.css" rel="stylesheet">

    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->

    <!--/ CSS -->

</head>
<body class="admin_page">
    <form id="form1"  runat="server">
        
        <section style="margin-top:5em;">
           <div class="container">
                <div class="row">
                    <div class="col-xs-12 col-sm-12 col-md-4 col-lg-4 center-block admin_login">
                        <div class="panel panel-danger" >
                            <div class="panel-heading">
                                <div class="panel-title">
                                    <span class="logo_Img" ><img src="assets/img/warehouse.png" class="img-responsive"></span>
                                    <br />
                                 <h4 class="text-center">Madhya Pradesh Warehousing And Logistics Corporation </h4> 
                                 <h6 class="text-center"> Government of India </h6>
                                                                 
                                </div>
                             </div>
                          <div class="panel-body">
                            <div class="login_container">
                                <asp:Panel ID="panelUserLevel" Visible="true" runat="server">
                                    <h5 class="text-center"> Choose an User</h5><hr />
                                    
                                   
                                    <ul class="list-unstyled level_list">
                                        <li>
                                            <asp:LinkButton ID="lbtnState" runat="server" onclick="lbtnState_Click">
                                                <div class="icon"><i class="fa fa-university"></i></div>  
                                                <div class="level">
                                                    <strong> STATE</strong><br/>
                                                    <small>Login State/HO Users</small>
                                                </div>
                                            </asp:LinkButton>
                                        </li>
                                        <li>
                                            <asp:LinkButton ID="lbtnRegion" runat="server" onclick="lbtnRegion_Click">
                                                <div class="icon"><i class="fa fa-building"></i></div>
                                                <div class="level">
                                                    <strong> REGION</strong><br/>
                                                    <small>Login Region/RO Users</small>
                                                </div>
                                            </asp:LinkButton>
                                        </li>

                                        <%--<li>
                                            <asp:LinkButton ID="lbtnDistrict" runat="server" onclick="lbtnDistrict_Click">
                                                <div class="icon"><i class="fa fa-building"></i></div>
                                                <div class="level">
                                                    <strong> DISTRICT</strong><br/>
                                                    <small>Login District Users</small>
                                                </div>
                                            </asp:LinkButton>
                                        </li>--%>

                                        <li>
                                            <asp:LinkButton ID="lbtnBranch" runat="server" onclick="lbtnBranch_Click">
                                                <div class="icon"><i class="fa fa-users"></i></div>
                                                <div class="level">
                                                    <strong> BRANCH</strong><br/>
                                                    <small>Login Branch/BO Users</small>
                                                </div>
                                            </asp:LinkButton>
                                        </li>
                                    </ul>
                                </asp:Panel>

                                <asp:Panel ID="panelLogin" Visible="false" runat="server">
                                    <h5 class="text-center"><i class="fa fa-key"></i> <asp:Literal ID="litHeading" runat="server"></asp:Literal></h5><hr />
                                    
                                    <div class="form-horizontal">
                                     
                                      <div class="form-group" id="divRegion" Visible="false" runat="server">
                                            <asp:DropDownList ID="ddlRegion" CssClass="form-control" runat="server">
                                                <asp:ListItem Text="Select Region" Value="Select"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ControlToValidate="ddlRegion" InitialValue="0" runat="server"></asp:RequiredFieldValidator>
                                      </div>

                                      <div class="form-group" id="divDistrict" Visible="false" runat="server">
                                            <asp:DropDownList ID="ddlDistrict" AutoPostBack="true" CssClass="form-control" 
                                                runat="server" onselectedindexchanged="ddlDistrict_SelectedIndexChanged">
                                                <asp:ListItem Text="Select District" Value="Select"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ControlToValidate="ddlDistrict" InitialValue="0" runat="server"></asp:RequiredFieldValidator>
                                      </div>

                                      <div class="form-group" id="divBranch" Visible="false" runat="server">
                                            <asp:DropDownList ID="ddlBranch" CssClass="form-control" runat="server">
                                                <asp:ListItem Text="Select Branch" Value="Select"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ControlToValidate="ddlBranch" InitialValue="0" runat="server"></asp:RequiredFieldValidator>
                                      </div>

                                      <div class="form-group" id="divPass" Visible="true" runat="server">
                                          <asp:TextBox ID="txtPassword" placeholder="Enter Password" TextMode="Password" class="form-control" runat="server"></asp:TextBox>
                                          <asp:RequiredFieldValidator ControlToValidate="txtPassword" ErrorMessage="*" ForeColor="Red"  ValidationGroup="A" runat="server"></asp:RequiredFieldValidator>
                                      </div>

                                      <div class="form-group">
                                        <asp:HiddenField ID="hdnLevel" Value="" runat="server" />

                                         <asp:Button ID="btnLogin" Text="Login" CssClass="btn btn-danger btn-block caps" 
                                              ValidationGroup="A" runat="server" onclick="btnLogin_Click" />
                                      </div>
                                  </div>
                              </asp:Panel>
                              </div>

                            

                          </div>
                      
                    </div>
                    </div>
                </div>
            </div>
        </section>


        <footer class="text-center">
            <p>© 2020 MPWLC. All rights reserved.</p>
        </footer>


   </form>
    <!-- Java Script -->

    <script type="text/javascript" src="assets/js/jquery-1.11.3.min.js"></script>
    <script type="text/javascript" src="assets/js/bootstrap.min.js"></script>
   
 

</body>
</html>