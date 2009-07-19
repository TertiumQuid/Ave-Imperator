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
using Csla;
using Facebook.Web;
using AveImperator.Library;
using AveImperator.Library.Security;

public partial class Battle : System.Web.UI.Page
{
    protected void Page_Load( object sender, EventArgs e )
    {
        Master.RequireAuthentication();

        if ( !Page.IsPostBack ) GetBattle();
    }

    private void GetBattle()
    {
        int id = 0;
        int challengeId = 0;

        if ( Request.QueryString["id"] != null )
        {
            try { id = Convert.ToInt32( Request.QueryString["id"] ); }
            catch { id = 0; }
        }
        else if ( Request.QueryString["challengeId"] != null )
        {
            try { challengeId = Convert.ToInt32( Request.QueryString["challengeId"] ); }
            catch { challengeId = 0; }
        }

        if ( id > 0 )
        {
            // bind battle for viewing, return.
            AveImperator.Library.Battle viewBattle = AveImperator.Library.Battle.GetBattle( id );
            Challenge viewChallenge = Challenge.GetChallenge( viewBattle.ChallengeId );

            BattleTitle.Text = viewChallenge.ChallengerName + " and " + viewChallenge.ChallengedName + " Battled in " + viewChallenge.City;
            BattleDescription.Text = viewBattle.Description;
            VictorLabel.Text = "<u>" + viewBattle.Victor + "</u> Was Victorious";
            return;
        }

        AveImperator.Library.Battle battle = BattleCommand.DoBattle( challengeId );
        Challenge challenge = Challenge.GetChallenge( challengeId );

        BattleTitle.Text = challenge.ChallengerName + " and " + challenge.ChallengedName + " Battled in " + challenge.City;
        BattleDescription.Text = battle.Description;
        VictorLabel.Text = "<u>" + battle.Victor + "</u> Was Victorious";
    }
}
