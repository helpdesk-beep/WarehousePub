<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        // Code that runs on application startup
         System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12| System.Net.SecurityProtocolType.Ssl3;
        System.Net.ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)3072;
    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // Code that runs when an unhandled error occurs

        //System.Exception oops = Server.GetLastError();
        //if (oops.GetBaseException() != null)
        //{
        //    Response.Redirect("Default2.aspx?Logout=true");
        //}
        //else if (oops.GetBaseException() is System.Web.HttpRequestValidationException)
        //{
        //    Response.Redirect("Default2.aspx?Logout=true");
        //}
        //else if (oops.GetBaseException() is System.Web.HttpException)
        //{
        //    Response.Redirect("Default2.aspx?Logout=true");
        //}

    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started
        Session.Timeout = 20;

    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }
       
</script>
