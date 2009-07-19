using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Facebook.Service;
using Facebook.Web;
using AveImperator.Library.Security;

public partial class FrameDefault : System.Web.UI.Page
{
    protected void Page_Load( object sender, EventArgs e )
    {
        // All user routing / authentication is handled here.

        if ( Master.FBApplication.IsSessionCreated == false ) { return; }

        if ( !Master.Identity.IsAuthenticated )
        {
            User user = Master.FBApplication.Service.Users.GetUser( null, "name" );

            // Attempt to log user into the system based on Facebook credentials
            bool isValid = AIPrincipal.Login( Convert.ToInt64( user.ID ) );

            if ( !Master.Identity.IsAuthenticated )
            {
                Server.Transfer( "AddGladiator.aspx", true );
            }
            else
            {
                HttpContext.Current.Session["CslaPrincipal"] = Csla.ApplicationContext.User;
                Server.Transfer( "ViewGladiator.aspx", true );
            }
        }
        else
        {
            Server.Transfer( "ViewGladiator.aspx", true );
        }
    }
}
