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

using System.Net;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Security.Cryptography;
using System.IO;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load( object sender, EventArgs e )
    {
        MenuStrip.Visible = Identity.GladiatorId > 0;

        ProfileLink.Click += new EventHandler( MenuLink_Click );
        ArenaLink.Click += new EventHandler( MenuLink_Click );
        RecordLink.Click += new EventHandler( MenuLink_Click );
        RulesLink.Click += new EventHandler( MenuLink_Click );
    }

    void MenuLink_Click( object sender, EventArgs e )
    {
        string url = ( (LinkButton)sender ).CommandArgument;
        Server.Transfer( url );
    }

    protected override void OnLoad( EventArgs e )
    {
        base.OnLoad( e );

        if ( FBApplication.IsSessionCreated == false ) { Response.End(); }

        if ( !Identity.IsAuthenticated )
        {
        }
    }

    /// <summary>
    /// Requires a user to have been authenticated or else they are redirected to the default page.
    /// </summary>
    public void RequireAuthentication()
    {
        if ( !Identity.IsAuthenticated ) Server.Transfer( "default.aspx" );
    }

    public string GetMd5Hash( string input )
    {
        MD5 md5Hash = MD5.Create();
        Byte[] data = md5Hash.ComputeHash( System.Text.Encoding.Default.GetBytes( input ) );
        StringBuilder sb = new StringBuilder();
        for ( int i = 0; i < data.Length; i++ )
        {
            sb.AppendFormat( CultureInfo.InvariantCulture, "{0:x2}", data[i] );
        }

        return sb.ToString();
    }

    public ScriptManager ScriptManager
    {
        get { return AveScriptManager; }
    }

    public FacebookApplication FBApplication
    {
        get { return AveFacebookApplication; }
    }

    public AIIdentity Identity
    {
        get { return ( (AIIdentity)Csla.ApplicationContext.User.Identity ); }
    }

    public Challenge SessionChallenge
    {
        get { return ( Session["Challenge"] != null && Session["Challenge"] is Challenge ? (Challenge)Session["Challenge"] : Challenge.NewChallenge() ); }
        set { Session["Challenge"] = value; }
    }

    public string SortExpression
    {
        get
        {
            object exp = ViewState["_SortExpression"];
            if ( exp == null || !( exp is string ) )
            {
                ViewState.Add( "_SortExpression", "Rank" );
                exp = ViewState["_SortExpression"];
            }

            return exp.ToString();
        }
        set { ViewState["_SortExpression"] = value; }
    }

    public System.ComponentModel.ListSortDirection SortDirection
    {
        get
        {
            object dir = ViewState["_SortDirection"];
            if ( dir == null || !( dir is System.ComponentModel.ListSortDirection ) )
            {
                ViewState.Add( "_SortDirection", System.ComponentModel.ListSortDirection.Ascending );
                dir = ViewState["_SortDirection"];
            }

            return (System.ComponentModel.ListSortDirection)dir;
        }
        set { ViewState["_SortDirection"] = value; }
    }
}
