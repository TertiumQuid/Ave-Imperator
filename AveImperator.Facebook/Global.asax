<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        // Code that runs on application startup

    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.
    }

    protected void Application_AcquireRequestState( object sender, EventArgs e )
    {
        if ( Csla.ApplicationContext.AuthenticationType == "Windows" ) return;

        AveImperator.Library.Security.AIPrincipal principal;
        try
        {
            principal = (AveImperator.Library.Security.AIPrincipal) HttpContext.Current.Session["CslaPrincipal"];
        }
        catch
        {
            principal = null;
        }

        if ( principal == null )
        {
            // didn't get a principal from Session, so
            // set it to an unauthenticted LPrincipal
            AveImperator.Library.Security.AIPrincipal.Logout();
        }
        else
        {
            // use the principal from Session
            Csla.ApplicationContext.User = principal;
        }
    }
       
</script>
