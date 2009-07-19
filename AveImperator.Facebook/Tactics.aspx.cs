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

public partial class Tactics : System.Web.UI.Page
{
    protected void Page_Load( object sender, EventArgs e )
    {
        Master.RequireAuthentication();

        if ( !Page.IsPostBack )
        {
            SetLimit();

            Challenge challenge = Master.SessionChallenge;
            Gladiator opponent = Gladiator.GetGladiator( challenge.ChallengerId == Master.Identity.Champion.Id ? challenge.ChallengedId : challenge.ChallengerId );

            if ( !challenge.Exists && !opponent.Exists ) Server.Transfer( "Default.aspx" );

            PageTitle.Text = "Tactics against " + opponent.Name + " The " + opponent.GladiatorClass;
        }

        Actions.ItemReorder += new EventHandler<AjaxControlToolkit.ReorderListItemReorderEventArgs>( Actions_ItemReorder );
        Actions.ItemCommand += new EventHandler<AjaxControlToolkit.ReorderListCommandEventArgs>( Actions_ItemCommand );

        SubmitLink.Click += new EventHandler( SubmitLink_Click );

        ErrorMessage.Visible = false;
    }

    protected bool CanAddMoreActions
    {
        get
        {
            object obj = ViewState["CanAddMoreActions"];
            if ( obj == null || !( obj is bool ) )
            {
                ViewState["CanAddMoreActions"] = true;
            }

            return (bool)obj;
        }
        set { ViewState["CanAddMoreActions"] = value; }
    }

    void SubmitLink_Click( object sender, EventArgs e )
    {
        ActionList actions = GetActionList();

        if ( actions.Count == 0 )
        {
            ErrorMessage.Message += "<li style=\"margin-left:13px;\">You must add *at least* one action.</li>";
            ErrorMessage.Visible = true;
            return;
        }

        Challenge challenge = Master.SessionChallenge;
        Gladiator opponent = Gladiator.GetGladiator( challenge.ChallengerId == Master.Identity.Champion.Id ? challenge.ChallengedId : challenge.ChallengerId );

        Tactic tactic = Tactic.NewTactic();
        tactic.GladiatorId = Master.Identity.Champion.Id;
        tactic.Name = "Tactics against " + opponent.Name + " The " + opponent.GladiatorClass;
        tactic.Actions = actions;

        tactic.Save();

        foreach ( Action action in actions )
        {
            action.TacticId = tactic.Id;
            action.Save();
        }

        if ( challenge.ChallengerId == Master.Identity.Champion.Id )
        {
            challenge.ChallengeStatusId = 1;
            challenge.ChallengerTacticId = tactic.Id;
        }
        else
        {
            challenge.ChallengeStatusId = 2;
            challenge.ChallengedTacticId = tactic.Id;
        }

        challenge.Save();

        Master.SessionChallenge = null;
        SessionTactic = null;

        if ( challenge.ChallengerId == Master.Identity.Champion.Id )
            Server.Transfer( "Arena.aspx" );
        else
            Server.Transfer( "Battle.aspx?challengeId=" + challenge.Id.ToString() );
    }

    private void SetLimit()
    {
        int remainingActions = ( Master.Identity.Champion.ActionsPerTactic - SessionTactic.Actions.Count );
        if ( remainingActions > 0 )
            LimitLabel.Text = "Based on your gladiator's cunning, <strong>you can add " + remainingActions.ToString() + " more action" + ( remainingActions > 1 ? "s" : "" ) + "</strong> to this tactic.";
        else
            LimitLabel.Text = "<strong>You can add no more actions</strong> to this tactic.";

        CanAddMoreActions = ( remainingActions > 0 );
    }

    public Tactic SessionTactic
    {
        get
        {
            object businessObject = Session["Tactic"];
            if ( businessObject == null || !( businessObject is Tactic ) )
            {
                Tactic tactic = Tactic.NewTactic();
                tactic.GladiatorId = Master.Identity.GladiatorId;

                businessObject = tactic;
                Session["Tactic"] = businessObject;
            }
            return (Tactic)businessObject;
        }
        set { Session["Tactic"] = value; }
    }

    #region ActionListDataSource
    protected void ActionListDataSource_SelectObject( object sender, Csla.Web.SelectObjectArgs e )
    {
        e.BusinessObject = GetActionList();
    }

    private ActionList GetActionList()
    {
        return SessionTactic.Actions;
    }

    void Actions_ItemCommand( object sender, AjaxControlToolkit.ReorderListCommandEventArgs e )
    {
        ActionList actions = GetActionList();

        actions.RemoveAt( e.Item.ItemIndex );

        for ( int i = 0; i < actions.Count; i++ )
            actions[i].Ordinal = i + 1;

        SessionTactic.Actions = actions;
        Actions.DataBind();
        SetLimit();
    }

    void Actions_ItemReorder( object sender, AjaxControlToolkit.ReorderListItemReorderEventArgs e )
    {
        ActionList actions = GetActionList();

        Action action = actions[e.OldIndex];
        actions.RemoveAt( e.OldIndex );
        actions.Insert( e.NewIndex, action );

        for ( int i = 0; i < actions.Count; i++ )
            actions[i].Ordinal = i + 1;

        SessionTactic.Actions = actions;
        Actions.DataBind();
    }


    protected void Maneuvers_ItemCommand( object source, DataListCommandEventArgs e )
    {
        Action action = Action.NewAction();
        string[] parameters = e.CommandArgument.ToString().Split( '|' );

        action.ManeuverId = Convert.ToInt32( parameters[0] );
        action.ItemId = Convert.ToInt32( parameters[1] );
        action.ItemType = (Maneuver.ItemType)Convert.ToInt32( parameters[2] );
        action.Maneuver = e.CommandName;
        action.Ordinal = Actions.Items.Count;

        SessionTactic.Actions.Add( action );
        SetLimit();

        Actions.DataBind();
    }
    #endregion

    #region ManeuverListDataSource
    protected void ManeuverListDataSource_SelectObject( object sender, Csla.Web.SelectObjectArgs e )
    {
        e.BusinessObject = GetManeuverList();
    }

    private ManeuverList GetManeuverList()
    {
        return ManeuverList.GetManeuverList( Master.Identity.GladiatorId );
    }
    #endregion
}
