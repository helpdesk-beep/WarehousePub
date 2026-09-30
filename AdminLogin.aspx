<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdminLogin.aspx.cs" Inherits="AdminLogin" EnableEventValidation="false" %>

<!DOCTYPE HTML>
<!--[if lt IE 7]>      <html class="no-js lt-ie9 lt-ie8 lt-ie7"> <![endif]-->
<!--[if IE 7]>         <html class="no-js lt-ie9 lt-ie8 ie7"> <![endif]-->
<!--[if IE 8]>         <html class="no-js lt-ie9"> <![endif]-->
<!--[if gt IE 8]><!--> <html class="no-js"> <!--<![endif]-->
<html lang="en">
<head>
<meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!-- The above 3 meta tags *must* come first in the head; any other head content must come *after* these tags -->
    <meta name="description" content="">
    <meta name="author" content="">
<title>MP Warehousing</title>

<style type="text/css">
header, nav, section, article, aside, footer {
   display:block;
}
</style>  
<!--[if lt IE 9]>
   <script>
      document.createElement('header');
      document.createElement('nav');
      document.createElement('section');
      document.createElement('article');
      document.createElement('aside');
      document.createElement('footer');
   </script>
<![endif]-->

    <!-- Bootstrap core CSS -->
    <link href="assets/css/bootstrap.css" rel="stylesheet" type="text/css">
    <link href="assets/css/bootstrap-theme.css" rel="stylesheet" type="text/css">

    <!-- font awesome -->
  <%--  <link href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet">--%>

    <link href="assets/css/style.css" rel="stylesheet" type="text/css">
    <link href="assets/css/custome.css" rel="stylesheet" type="text/css">

  </head>

  <body style="background:#ddd;">
    <form id="form1" runat="server">
    
    <section>
        <div class="container">
        <!-- Example row of columns -->
          <div class="row">
            <div class="col-md-5 col-lg-5 center_block" style="margin-top:15em">
              <div class="panel panel-info">
                <div class="panel-heading text-uppercase lead">Admin Login</div>
                <div class="panel-body">
                  <form role="form">
                    <div class="form-group">
                        <label>User Name</label>
                        <asp:TextBox ID="txtUserName" placeholder="User Name" class="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label>Password</label>
                        <asp:TextBox ID="txtPassword" TextMode="Password" placeholder="Password" class="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="form-group">
                    <%--    <label><a href="#">Forgot Password?</a></label><br>--%>
                    <%--    <label><input type="checkbox"> Remember me </label>--%>

                    </div>
                  </form>
                </div>
                <div class="panel-footer">
                  <div class="row-fluid text-right">
                  <asp:Button ID="btnLogin" class="btn btn-info caps" runat="server" Text="Login" 
                          onclick="btnLogin_Click"></asp:Button>
                </div>
                </div>
              </div>
          </div>
          </div>

         </div> <!-- /container -->
   </section>
    

<!--Java Script -->
<script src="https://code.jquery.com/jquery-1.11.1.min.js"></script>
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.0/js/bootstrap.min.js"></script>

 <!--[if IE]>

    <link href="assets/css/custom-ie-lt-8.css" rel="stylesheet" />

<![endif]-->

<!-- HTML5 shim and Respond.js IE8 support of HTML5 elements and media queries -->
<!--[if lt IE 9]>
  <script type='text/javascript' src="http://html5shiv.googlecode.com/svn/trunk/html5.js"></script>
  <script type='text/javascript' src="http://cdnjs.cloudflare.com/ajax/libs/respond.js/1.4.2/respond.js"></script>
<![endif]-->

</form>
</body>
</html>

