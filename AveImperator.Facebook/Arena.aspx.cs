using System;
using System.Text;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Security.Cryptography;
using System.IO;
using Facebook.Service;
using Facebook.Web;
using AveImperator.Library;
using AveImperator.Library.Security;

public partial class Arena : System.Web.UI.Page
{
    protected void Page_Load( object sender, EventArgs e )
    {
        Master.RequireAuthentication();

        if ( !IsPostBack )
        {
            Session["CurrentObject"] = null;
            FriendListDataSource.Parameters.Add( new Parameter( "UserID", TypeCode.String, Master.FBApplication.UserID ) );

            ICollection<string> friends = Master.FBApplication.Service.Friends.GetFriends( FriendFilter.AppUsers );

            StringBuilder sb = new StringBuilder();
            foreach ( string str in friends )
            {
                if (sb.Length == 0)
                    sb.Append( str );
                else
                    sb.Append( "," + str );
            }
            ChallengeFriends.CommandArgument = sb.ToString();

            friends = Master.FBApplication.Service.Friends.GetFriends( FriendFilter.NonAppUsers );
            ChallengeFriendsPanel.Visible = friends.Count > 0;
            CowardsLabel.Text = "You have " + friends.Count.ToString() + " cowardly friends who remain unchallenged. ";
        }

        Gladiators.RowCommand += new GridViewCommandEventHandler( Gladiators_RowCommand );
        Gladiators.Sorting += new GridViewSortEventHandler( Gladiators_Sorting );
        Gladiators.RowCreated += new GridViewRowEventHandler( Gladiators_RowCreated );
        Friends.ItemCommand += new DataListCommandEventHandler( Friends_ItemCommand );
        ChallengeFriends.Click += new EventHandler( ChallengeFriends_Click );

        PreviousLink.Click += new EventHandler( PagingLink_Click );
        NextLink.Click += new EventHandler( PagingLink_Click );
    }

    void Gladiators_RowCreated( object sender, GridViewRowEventArgs e )
    {
        //LinkButton btn = (LinkButton)e.Row.FindControl( "ProfileLink" );
        //Master.ScriptManager.RegisterPostBackControl( ctrl );
        //if ( btn == null )
        //{
        //    foreach ( Control ctrl in e.Row.Cells[1].Controls )
        //    {
        //        if ( ctrl is LinkButton )
        //        {
        //            Master.ScriptManager.RegisterPostBackControl( ctrl );
        //            return;
        //        }
        //    }
        //}

        foreach ( TableCell cell in e.Row.Cells )
        {
            foreach ( Control ctrl in cell.Controls )
            {
                if ( ctrl is LinkButton )
                {
                    //Master.ScriptManager.RegisterPostBackControl( ctrl );
                    return;
                }
            }
        }
    }

    void Gladiators_RowCommand( object sender, GridViewCommandEventArgs e )
    {
        Server.Transfer( "rules.aspx", true );
    }

    protected void Gladiators_LinkClick( object sender, EventArgs e )
    {
        Server.Transfer( "rules.aspx", true );
    }

    void PagingLink_Click( object sender, EventArgs e )
    {
        int pageChange = Convert.ToInt32( ( (LinkButton)sender ).CommandArgument );
        FriendsPage += pageChange;

        if ( FriendsPage <= 0 ) FriendsPage = 0;

        FriendListDataSource.FqlQuery = "SELECT name, uid, pic_square, has_added_app from user WHERE uid IN (SELECT uid2 FROM friend WHERE uid1=@UserID) ORDER BY uid LIMIT 8 OFFSET " + ( FriendsPage * 8 ).ToString();
        Friends.DataBind();        
    }

    public int FriendsPage
    {
        get
        {
            object obj = ViewState["FriendsPage"];
            if ( obj == null || !( obj is int ) )
            {
                obj = 0;
                ViewState["FriendsPage"] = obj;
            }

            return Convert.ToInt32( obj );
        }
        set { ViewState["FriendsPage"] = value; }
    }

    void ChallengeFriends_Click( object sender, EventArgs e )
    {
        InviteFriends( ChallengeFriends.CommandArgument );
    }

    void Friends_ItemCommand( object source, DataListCommandEventArgs e )
    {
        //long uid = Convert.ToInt64( Friends.DataKeys[e.Item.ItemIndex] );
        //bool hasAddedApp = Convert.ToBoolean( e.CommandArgument );

        //if ( hasAddedApp )
        //{
        //    int gladiatorId = Gladiator.GetUserGladiator( uid ).Id;
        //    Master.FBApplication.Redirect( this, "http://apps.facebook.com/aveimperator/challenges.aspx?challengedId=" + gladiatorId.ToString() );
        //}

        //StringBuilder fbml = new StringBuilder();
        //fbml.Append( "<div>" );
        //fbml.Append( "<fb:name capitalize=\"true\" uid=\"" );
        //fbml.Append( Master.FBApplication.Service.UserID );
        //fbml.Append( "\" firstnameonly=\"true\" possessive =\"true\" /> " );
        //fbml.Append( "gladiator challenged you to battle in the arena." );
        //fbml.Append( "<a href=\"http://apps.facebook.com/aveimperator\">Ave Imperator! I accept...</a>" );
        //fbml.Append( "</div>" );
    }

    protected void InviteFriends( string excludeIds )
    {
        string api_key = Master.FBApplication.ApplicationSettings.ApiKey;
        string secret = Master.FBApplication.Secret;
        string action = "http://apps.facebook.com/aveimperator/arena.aspx";
        string actionText = "Challenge Your Friends to Gladiator Battles in The Arena";
        string content = "<fb:fbml><fb:name capitalize=\"true\" uid=\"" + Master.FBApplication.Service.UserID + "\" firstnameonly=\"true\" possessive =\"true\" /> gladiator <a href=\"http://apps.facebook.com/avemperator/viewgladiator.aspx?id=" + Master.Identity.Champion.Id.ToString() + "\">" + Master.Identity.Champion.Name + "</a> challenged you to battle in the arena. Train a gladiator and your wit and skill to make  <fb:req-choice url=\"http://apps.facebook.com/aveimperator/addgladiator.aspx\" label=\"Ave Imperator! I accept...\" /></<fb:fbml>";
        string invite = "true";
        string type = "Ave Imperator";

        string data = "action=" + action + "actiontext=" + actionText + "api_key=" + api_key + "content=" + content + "exclude_ids=" + excludeIds + "invite=" + invite + "type=" + type + secret;
        string sig = Master.GetMd5Hash( data );

        Response.Write( "<form id=\"postForm\" target=\"_top\" action=\"http://www.facebook.com/multi_friend_selector.php\" method=\"post\">" );
        Response.Write( "</form>" );

        StringBuilder sb = new StringBuilder();
        sb.Append( "function setElem(form, name, value) {" );
        sb.Append( "var el = document.createElement(\"input\");" );
        sb.Append( "el.type = \"hidden\";" );
        sb.Append( "el.name = name;" );
        sb.Append( "el.value = value;" );
        sb.Append( "form.appendChild(el);" );
        sb.Append( "}" );

        sb.Append( "var myForm = document.getElementById('postForm');" );
        sb.Append( "setElem(myForm, 'action', '" + action + "');" );
        sb.Append( "setElem(myForm, 'actiontext', '" + actionText + "');" );
        sb.Append( "setElem(myForm, 'api_key', '" + api_key + "');" );
        sb.Append( "setElem(myForm, 'content', '" + content + "');" );
        sb.Append( "setElem(myForm, 'invite', '" + invite + "');" );
        sb.Append( "setElem(myForm, 'exclude_ids', '" + excludeIds + "');" );
        sb.Append( "setElem(myForm, 'type', '" + type + "');" );
        sb.Append( "setElem(myForm, 'sig', '" + sig + "');" );
        sb.Append( "myForm.submit();" );

        ClientScript.RegisterClientScriptBlock( typeof( Page ), "InviteFriendsAveScript", sb.ToString(), true );

    }

   // Private Function getMembers(ByVal api_key As String, ByVal secret As String) As String
   //    Dim strExcludeIds As String = ""
   //    Dim _fbService As New Facebook.Components.FacebookService()
   //    _fbService.ApplicationKey = api_key
   //    _fbService.Secret = secret
   //    _fbService.IsDesktopApplication = False
   //    _fbService.SessionKey = Session("facebook_session_key")
   //    _fbService.UserId = Session("facebook_userId")
   //    Try
   //        Dim otherFriends As System.Collections.ObjectModel.Collection(Of Facebook.Entity.User) = _fbService.GetFriendsAppUsers
   //        For Each myFriend As Facebook.Entity.User In otherFriends
   //            strExcludeIds &= myFriend.UserId & ","
   //        Next
   //        If strExcludeIds.Length > 0 Then
   //            strExcludeIds.TrimEnd(",")
   //        End If
   //    Catch ex As Exception
   //    End Try
   //    Return strExcludeIds
   //End Function


    #region GladiatorGridView
    void Gladiators_Sorting( object sender, GridViewSortEventArgs e )
    {
        Master.SortExpression = e.SortExpression;

        switch ( e.SortDirection.ToString() )
        {
            case "Ascending":
                Master.SortDirection = System.ComponentModel.ListSortDirection.Ascending;
                break;
            case "Descending":
                Master.SortDirection = System.ComponentModel.ListSortDirection.Descending;
                break;
        }
    }
    #endregion

    #region GladiatorListDataSource
    protected void GladiatorListDataSource_SelectObject( object sender, Csla.Web.SelectObjectArgs e )
    {
        e.BusinessObject = GetGladiatorList();
    }

    private Csla.SortedBindingList<Gladiator> GetGladiatorList()
    {
        object businessObject = Session["CurrentObject"];
        if ( businessObject == null || !( businessObject is GladiatorList ) )
        {
            businessObject = GladiatorList.GetGladiatorList( Master.Identity.GladiatorId );
            Session["CurrentObject"] = businessObject;
        }
        
        Csla.SortedBindingList<Gladiator> list = new Csla.SortedBindingList<Gladiator>( (GladiatorList)businessObject );
        list.ApplySort( Master.SortExpression, Master.SortDirection );
        return list;
    }
    #endregion
}
