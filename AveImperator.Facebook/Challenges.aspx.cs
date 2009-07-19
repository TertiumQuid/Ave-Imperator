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
using AveImperator.Library;
using AveImperator.Library.Security;

public partial class Challenges : System.Web.UI.Page
{
    protected void Page_Load( object sender, EventArgs e )
    {
        if ( !Page.IsPostBack ) 
        { 
            BindOrdinarii(); 
        }

        RetractChallenge.Click += new EventHandler( RetractChallenge_Click );
        SubmitChallenge.Click += new EventHandler( SubmitChallenge_Click );
        RejectChallenge.Click += new EventHandler( RejectChallenge_Click );
        AcceptChallenge.Click += new EventHandler( AcceptChallenge_Click );
    }

    void AcceptChallenge_Click( object sender, EventArgs e )
    {
        int id = 0;
        if ( Request.QueryString["id"] != null )
        {
            try { id = Convert.ToInt32( Request.QueryString["id"] ); }
            catch { id = 0; }
        }

        Challenge challenge = Challenge.GetChallenge( id );
        Master.SessionChallenge = challenge;

        Server.Transfer( "Tactics.aspx" );
    }

    void RejectChallenge_Click( object sender, EventArgs e )
    {
        int id = 0;
        if ( Request.QueryString["id"] != null )
        {
            try { id = Convert.ToInt32( Request.QueryString["id"] ); }
            catch { id = 0; }
        }

        Challenge challenge = Challenge.GetChallenge( id );
        challenge.ChallengeStatusId = 3;

        challenge.Save();

        Server.Transfer( "ViewGladiator.aspx" );
    }

    void RetractChallenge_Click( object sender, EventArgs e )
    {
        int id = 0;
        if ( Request.QueryString["id"] != null )
        {
            try { id = Convert.ToInt32( Request.QueryString["id"] ); }
            catch { id = 0; }
        }

        Challenge challenge = Challenge.GetChallenge( id );
        challenge.ChallengeStatusId = 4;

        challenge.Save();

        Server.Transfer( "ViewGladiator.aspx" );
    }

    void SubmitChallenge_Click( object sender, EventArgs e )
    {
        int id = 0;
        int challengerId = Master.Identity.GladiatorId;
        int challengedId = 0;

        if ( Request.QueryString["id"] != null )
        {
            try { id = Convert.ToInt32( Request.QueryString["id"] ); }
            catch { id = 0; }

            Challenge c = Challenge.GetChallenge( id );
            challengerId = c.ChallengerId;
            challengedId = c.ChallengedId;
        }
        else if ( Request.QueryString["challengedId"] != null )
        {
            try { challengedId = Convert.ToInt32( Request.QueryString["challengedId"] ); }
            catch { challengedId = 0; }

            if ( challengedId > 0 ) id = Challenge.GetChallenge( challengerId, challengedId ).Id;
        }
        else if ( Request.QueryString["userId"] != null )
        {
            long userId = 0;
            try { userId = Convert.ToInt64( Request.QueryString["userId"] ); }
            catch { userId = 0; }

            if ( userId > 0 ) id = Challenge.GetChallenge( challengerId, Gladiator.GetUserGladiator( userId ).Id ).Id;
        }

        Challenge challenge = Challenge.NewChallenge();
        challenge.ChallengerId = challengerId;
        challenge.ChallengedId = challengedId;
        challenge.ChallengerOpeningWords = OpeningWords.Text;
        challenge.ChallengedOpeningWords = string.Empty;
        challenge.CityId = Convert.ToInt32( Cities.SelectedValue );

        // Dont's save the challenge until the user has assigned tactics. If there abort the process somewhere, they'll just have to make another challenge.
        // challenge.Save();
        Master.SessionChallenge = challenge;

        Gladiator challenger = Gladiator.GetGladiator( challengerId );
        Gladiator challenged = Gladiator.GetGladiator( challengedId );

        string challengerMessage = string.Empty;
        challengerMessage = challenger.User + "'s gladiator <a href=\"http://apps.facebook.com/aveimperator/ViewGladiator.aspx?id=" + challenger.Id.ToString() + "\">" + challenger.Name + "</a>, a " + challenger.Race + " " + challenger.GladiatorClass + ", ";
        challengerMessage += "challenged the " + challenged.Race + " " + challenged.GladiatorClass + " <a href=\"http://apps.facebook.com/aveimperator/ViewGladiator.aspx?id=" + challenged.Id.ToString() + "\">" + challenged.Name + "</a> to battle in the blood-slaked arena sands.";

        if (challenge.ChallengerOpeningWords.Length>0)
        {
            challengerMessage += "<p>" + challenger.Name + " says: <i>" + challenge.ChallengerOpeningWords + "</i></p>";
        }

        // Master.FBApplication.Service.Feed.PublishMiniFeedStory( " Calls for Battle!", challengerMessage, null );

        Server.Transfer( "Tactics.aspx" );
    }
    
    #region Gladiator Data
    private void BindOrdinarii()
    {
        int id = 0;
        int challengerId = Master.Identity.GladiatorId;
        int challengedId = 0;
        Challenge challenge = Challenge.NewChallenge();

        if ( Request.QueryString["id"] != null )
        {
            try { id = Convert.ToInt32( Request.QueryString["id"] ); }
            catch { id = 0; }

            challenge = Challenge.GetChallenge( id );
            challengerId = challenge.ChallengerId;
            challengedId = challenge.ChallengedId;
        }
        else if ( Request.QueryString["challengedId"] != null )
        {
            try { challengedId = Convert.ToInt32( Request.QueryString["challengedId"] ); }
            catch { challengedId = 0; }

            if ( challengedId > 0 )
            {
                challenge = Challenge.GetChallenge( challengerId, challengedId );
                id = challenge.Id;
            }
        }
        else if ( Request.QueryString["userId"] != null )
        {
            long userId = 0;
            try { userId = Convert.ToInt64( Request.QueryString["userId"] ); }
            catch { userId = 0; }

            if ( userId > 0 )
            {
                challenge = Challenge.GetChallenge( challengerId, Gladiator.GetUserGladiator( userId ).Id );
                id = challenge.Id;
            }
        }

        // Ensure gladiator can't challenge themself
        if ( challengedId == challengerId ) Server.Transfer( "ViewGladiator.aspx" );

        Gladiator challenger = Gladiator.GetGladiator( challengerId );
        Gladiator challenged = Gladiator.GetGladiator( challengedId );

        if ( !challenger.Exists || !challenged.Exists ) Server.Transfer( "Default.aspx" );

        PageTitleLabel.Text = challenger.Name + " Challenges " + challenged.Name + " to Battle";

        ChallengerName.Text = challenger.Name.ToUpper();
        ChallengerClass.Text = challenger.GladiatorClass;
        ChallengerRace.Text = challenger.Race;
        ChallengerConstitution.Text = challenger.Constitution.ToString();
        ChallengerCunning.Text = challenger.Cunning.ToString();
        ChallengerEndurance.Text = challenger.Endurance.ToString();
        ChallengerStrength.Text = challenger.Strength.ToString();
        ChallengerAvatar.ImageUrl = "~/Images/Gladiators/" + challenger.GladiatorClass + "avatar.jpg";
        ChallengerUser.Text = challenger.User;
        ChallengerWeapons.Text = challenger.WeaponSummary;
        ChallengerArmor.Text = challenger.ArmorSummary;

        ChallengedName.Text = challenged.Name.ToUpper();
        ChallengedClass.Text = challenged.GladiatorClass;
        ChallengedRace.Text = challenged.Race;
        ChallengedConstitution.Text = challenged.Constitution.ToString();
        ChallengedCunning.Text = challenged.Cunning.ToString();
        ChallengedEndurance.Text = challenged.Endurance.ToString();
        ChallengedStrength.Text = challenged.Strength.ToString();
        ChallengedAvatar.ImageUrl = "~/Images/Gladiators/" + challenged.GladiatorClass + "avatar.jpg";
        ChallengedUser.Text = challenged.User;
        ChallengedWeapons.Text = challenged.WeaponSummary;
        ChallengedArmor.Text = challenged.ArmorSummary;

        if ( challengerId == Master.Identity.GladiatorId && id == 0 ) // Making a new challenge
        {
            ButtonsView.ActiveViewIndex = 0;
        }
        else if ( challengerId == Master.Identity.GladiatorId ) // Viewing an existing challenge the user made
        {
            ButtonsView.ActiveViewIndex = 1;
        }
        else if ( challengedId == Master.Identity.GladiatorId ) // Viewing an existing challenge made against the user
        {
            ButtonsView.ActiveViewIndex = 2;
        }
        else // Viewing as a 3rd party to the challenge
        {
            ButtonsView.ActiveViewIndex = -1;
        }
    }
    #endregion

    #region CityListDataSource
    protected void CityListDataSource_SelectObject( object sender, Csla.Web.SelectObjectArgs e )
    {
        e.BusinessObject = GetCityList();
    }

    private CityList GetCityList()
    {
        int id = 0;
        int challengerId = Master.Identity.GladiatorId;
        int challengedId = 0;

        if ( Request.QueryString["id"] != null )
        {
            try { id = Convert.ToInt32( Request.QueryString["id"] ); }
            catch { id = 0; }

            Challenge c = Challenge.GetChallenge( id );
            challengerId = c.ChallengerId;
            challengedId = c.ChallengedId;
        }
        else if ( Request.QueryString["challengedId"] != null )
        {
            try { challengedId = Convert.ToInt32( Request.QueryString["challengedId"] ); }
            catch { challengedId = 0; }
        }
        else if ( Request.QueryString["userId"] != null )
        {
            long userId = 0;
            try { userId = Convert.ToInt64( Request.QueryString["userId"] ); }
            catch { userId = 0; }

            challengedId = Gladiator.GetUserGladiator( userId ).Id;
        }

        int fame = Math.Min( Gladiator.GetGladiator( challengerId ).Fame, Gladiator.GetGladiator( challengedId ).Fame );

        return CityList.GetCityList( fame );
    }
    #endregion
}
