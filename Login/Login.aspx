<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login_Login" %>
<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc1" %>
<!DOCTYPE HTML>
<html xmlns="http://www.w3.org/1999/xhtml">
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
    <!-- Bootstrap core CSS -->
    <link href="../assets/css/bootstrap.css" rel="stylesheet" type="text/css">
    <link href="../assets/css/bootstrap-theme.css" rel="stylesheet" type="text/css">

    <!-- font awesome -->
    <link href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet">

    <link href="/assets/css/style.css" rel="stylesheet" type="text/css">
    <link href="/assets/css/custome.css" rel="stylesheet" type="text/css">
    <script src="../assets/js/md5.js"></script>
  <%--  <script type="text/javascript">

        function HashPwdwithSalt(salt) {

            if (document.getElementById("txtPassword").value != "") {

                document.getElementById("txtPassword").value =
                    hex_md5(document.getElementById("txtPassword").value);

                document.getElementById("txtPassword").value =
                    hex_md5(document.getElementById("txtPassword").value + salt);
            }
        }
    </script>
     <script type="text/javascript">
         function onload1() {
             document.getElementById('<%=txtPassword.ClientID%>').value = "";
         }

         function doLogout() {
             var backlen = history.length;
             history.go(-backlen);
             window.location.replace("Login.aspx");
         }

</script>--%>
  </head>
    <%--<body onload="onload1();">--%>
    <body>
    <form id="form1" runat="server" autocomplete="off" method="post" defaultbutton="btnLogin" action="Login.aspx">
    <header>
      <div class="top_bar">
        <div class="container">
          <span class="pull-left">
                <asp:Repeater ID="rptMarquee" runat="server">
                <HeaderTemplate>
                    <marquee direction="left" scrollamount="3" onmouseover="this.stop();" onmouseout="this.start();" >
                </HeaderTemplate>
                    <ItemTemplate>
                        <a href='<%#Eval("Link")%>' target="_blank" class="red"><%#Eval("Title") %></a> | 
                    </ItemTemplate>
                    <FooterTemplate>
                         </marquee>
                    </FooterTemplate>
                </asp:Repeater>
         
          </span>
         
          <span class="pull-right">
                <asp:Label ID="lblVCount" class="label label-danger" runat="server"></asp:Label> |
                <a> <strong><i class="fa fa-calendar"></i> <%= DateTime.Now.ToString("dd MMMM yyyy")%> |&nbsp;<i class="fa fa-clock-o"></i>&nbsp;<label id="lblTime"></label></strong></a>
          </span>
           
        </div>
    </div>

    <div class="mdsliding">
        <div class="row-fluid">
            <div class="col-md-4 ">
                <div class="mdbox">
                    <a href="http://www.mpsc.mp.nic.in/Warehouse/login.aspx">
                        <span class="mdicon">
                            <img src="/assets/img/icons/logo.png"/>
                            <h5>WHMS</h5>
                        </span>
                    </a>
                </div>
            </div>
            <div class="col-md-4">
                 <div class="mdbox">
                <a href="http://mpsc.mp.nic.in/Warehouse/JointVentureScheme/JointVentureSchemeApp.aspx">
                <span class="mdicon">
                    <img src="/assets/img/icons/jvs.png" />
                    <h5>JVS</h5>
                    </span>
                </a>
                </div>
            </div>
            <div class="col-md-4">
             <div class="mdbox">
                <a href="http://mpsc.mp.nic.in/Warehouse/TribalGodown/Tribalgodownapp.aspx">
                  <span class="mdicon">
                    <img src="/assets/img/icons/godown.png"/>
                    <h5>Tribal Godown</h5>
                    </span>
                </a>
                </div>
            </div>
            
         </div>
         <div class="row-fluid">
            <div class="col-md-4">
            <div class="mdbox">
                <a href="http://mpsc.mp.nic.in/Warehouse/Inspection/InspectionLogin.aspx">
                   
                   <span class="mdicon">
                    <img src="/assets/img/icons/inspection.png" />
                    <h5>Inspection</h5>
                    </span>
                </a>
                </div>
            </div>
            
            <div class="col-md-4">
             <div class="mdbox">
                 <a href="http://mpwarehousing.com/useful_file/MPWLC D-Sign V 3.8.exe">
                 <span class="mdicon">
                    <img src="/assets/img/icons/signature.png"/>
                    <h5>Digital Sign</h5>
                    </span>
                </a>
                </div>
            </div>
            <div class="col-md-4">
             <div class="mdbox">
                 <a href="#">
                 <span class="mdicon">
                    <img src="/assets/img/icons/audit.png"/>
                    <h5>Audit List</h5>
                    </span>
                </a>
                </div>
            </div>
         </div>
            
        <div class="col-md-12 text-center">
            <br />
             <a href="EmpCorner.aspx" class="btn btn-default thumbnail  col-md-6 center-block"> More...</a><br>
        </div>
    </div>
     <a href="#" class="trigger"><i class="fa fa-plus-circle"></i> Employee Corner</a>

    
    <div class="logo_bar">
      <div class="container">
      <div class="pull-left">
     <%--     <img src="assets/img/logo1.png" style="width:100px;height: 100px;border-right:1px solid #ddd">
          <img src="assets/img/logo_text.png" style="width:230px;">--%>
          <a href="Default.aspx"><img src="/assets/img/logo_with_text.png" class="img-responsive logo" /></a>
      </div>
      <nav class="pull-right">
        <ul class="list-unstyled icon_bar">
          <li>
            <a href="/InfoProfile.aspx">
             <span class="icon">
              <img src="/assets/img/profile.png">
             </span>
             <p>Profile</p>
            </a>
          </li>
          <li>
            <a href="/Network.aspx">
            <span class="icon">
              <img src="/assets/img/icons/network.png">
              </span>
               <p>Network</p>
           </a>
          </li>
           <li>
            <a href="/Sevices.aspx">
            <span class="icon">
              <img src="/assets/img/icons/services.png">
              </span>
            <p>Services</p>
           </a>
          </li>
           
           <li>
            <a href="/business.aspx">
            <span class="icon">
              <img src="/assets/img/icons/Business.png">
              </span>
             <p>Business</p>
          </a>
          </li>
          <li>
            <a href="/Quality.aspx">
            <span class="icon">
              <img src="/assets/img/icons/quality.png">
              </span>
              <p>Quality</p>
           </a>
          </li>
          <li>
            <a href="/Tariff.aspx">
            <span class="icon">
              <img src="/assets/img/icons/tariff.png">
              </span>
               <p>Tariff</p>
             </a>
          </li>
        </ul>
      </nav>
    </div>
    </div>
    </header>

    <nav class="navbar navbar-default">
      <div class="container">
        <!-- Brand and toggle get grouped for better mobile display -->
        <div class="navbar-header">
          <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1" aria-expanded="false">
            <span class="sr-only">Toggle navigation</span>
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
          </button>
          <!-- <a class="navbar-brand" href="#">Brand -->
        </div>

        <!-- Collect the nav links, forms, and other content for toggling -->
        <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
          <ul class="nav navbar-nav">
            <li><a href="/Default.aspx">Home <span class="sr-only">(current)</span></a></li>
           <%-- <li><a href="/News.aspx">News</a></li>
            <li><a href="/Gallery.aspx">Gallery</a></li>--%>
            <li><%--<a href="#">New Updates</a>--%></li>
            <li><a href="/Contact.aspx">Contact Us</a></li>
          </ul>
        </div><!-- /.navbar-collapse -->
      </div><!-- /.container-fluid -->
    </nav>
    <div style="max-height:500px;">
    <div class="container">
        <!-- Example row of columns -->
        <div class="row">
            <div class="col-md-5 col-lg-5 center_block" style="margin-top: 3em">
                <div class="panel panel-info">
                    <div class="panel-heading text-uppercase lead">Login</div>
                    <div class="panel-body">
                        <asp:Label ID="lblErr" runat="server" Font-Size="X-Large" ForeColor="Red"></asp:Label>
                        <div class="form-group">
                            <label>User Name</label>
                            <asp:TextBox ID="txtUserName" placeholder="User Name" class="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>Password</label>
                            <asp:TextBox ID="txtPassword" TextMode="Password" placeholder="Password" class="form-control" runat="server" ></asp:TextBox>
                        </div>
                        <div class="form-group">
                           <cc1:CaptchaControl ID="cptCaptcha" runat="server"
                                CaptchaBackgroundNoise="Low" CaptchaLength="5"
                                CaptchaHeight="60" CaptchaWidth="200"
                                CaptchaLineNoise="None" CaptchaMinTimeout="5"
                                CaptchaMaxTimeout="240" FontColor="#529E00" />
                        </div>
                        <div class="form-group">
                            <asp:TextBox ID="txtCaptcha" runat="server" class="form-control"></asp:TextBox>
                           <%-- <br />
                            <asp:Button ID="btnVerify" runat="server" Text="Verify Image" OnClick="btnVerify_Click" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*Required" ControlToValidate="txtCaptcha"></asp:RequiredFieldValidator>
                            <br />
                            <br />--%>
                            <asp:Label ID="lblErrorMessage" runat="server" Font-Names="Arial" Text=""></asp:Label>
                        </div>
                    </div>
                    <div class="panel-footer">
                        <div class="row-fluid text-right">
                            <asp:Button ID="btnLogin" class="btn btn-info caps" runat="server" Text="Login"
                                OnClick="btnLogin_Click"></asp:Button>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </div>
    </div>           
  
    <footer>
        <ul class="list-inline">
      
          <li><a href="/Default.aspx">Home</a></li> |
          <li><a href="/Network.aspx">Network</a></li> |
          <%--<li><a href="/InfoProfile.aspx">Profile</a></li> |--%>
         <%-- <li><a href="/business.aspx">Business</a></li> |
          <li><a href="/News.aspx">News</a></li> |--%>
         <%-- <li><a href="#">Future Plan</a></li> |--%>
          <li><a href="/Sevices.aspx">Services</a></li> |
          <li><a href="/Tender.aspx">Tender</a></li> |
          <%--<li><a href="#">Feedback</a></li> |--%>
          <li><a href="/Quality.aspx">Quality</a></li> |
          <li><a href="/Tariff.aspx">Tariff</a></li> |
         <%-- <li><a href="#">FAQ's</a></li> |--%>
          <li><a href="Contact.aspx">Contact</a></li> 
        </ul>
      <hr class="line-white-center"/>
      <small>Copyright &copy; 2020 MPWLC - All Rights Reserved | Madhya Pradesh Warehousing and Logistics Corporation </small>
    </footer>

    
    


<!--Java Script -->
<script src="https://code.jquery.com/jquery-1.11.1.min.js"></script>
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.0/js/bootstrap.min.js"></script>

 
<script type="text/javascript">

    $(document).ready(function () {
        ShowTime();
    });
    function ShowTime() {
        var dt = new Date();
        document.getElementById("lblTime").innerHTML = dt.toLocaleTimeString();
        window.setTimeout("ShowTime()", 1000); // Here 1000(milliseconds) means one 1 Sec  
    }
    $(document).ready(function () {
        $(".trigger").click(function () {
            $(".mdsliding").toggle("fast");
            $(this).toggleClass("active");
            return false;
        });
    });

</script>

</form>
</body>
</html>
