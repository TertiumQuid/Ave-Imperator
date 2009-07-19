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
using Facebook.Web;
using AveImperator.Library;
using AveImperator.Library.Security;

public partial class Default : Facebook.WebControls.CanvasFBMLBasePage
{
    protected void Page_Load( object sender, EventArgs e )
    {
        base.Api = AveFacebookApplication.ApplicationKey;
        base.Secret = AveFacebookApplication.Secret;
        base.Page_Load( sender, e );

        ProcessURLRequest();
    }

    protected void ProcessURLRequest()
    {
        if ( Request.QueryString["url"] != null )
        {
            MasterFrame.Text = "<fb:iframe src=\"http://aveimperator.bellwethersystems.com/" + Request.QueryString["url"] + "\" frameborder=\"0\" scrolling=\"0\" height=\"800\" style=\"width:635px;\" />";
        }
        else
        {
            MasterFrame.Text = "<fb:iframe src=\"http://aveimperator.bellwethersystems.com/framedefault.aspx\" frameborder=\"0\" scrolling=\"0\" height=\"800\" style=\"width:635px;\" />";
        }
    }
}
