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

public partial class ViewGladiator : System.Web.UI.Page
{
    protected void Page_Load( object sender, EventArgs e )
    {
        if ( !Page.IsPostBack )
        {
            Session["CurrentObject"] = null;
            BindGladiator(); 
        }

        Challenges.RowCommand += new GridViewCommandEventHandler( Challenges_RowCommand );
    }
    public int ViewingGladiatorId
    {
        get
        {
            object obj = ViewState["GladiatorId"];
            if ( obj == null )
            {
                int gladiatorId = 0;

                if ( Request.QueryString["id"] != null )
                {
                    try { gladiatorId = Convert.ToInt32( Request.QueryString["id"] ); }
                    catch { gladiatorId = 0; }
                }
                else
                {
                    gladiatorId = Master.Identity.GladiatorId;
                }

                ViewState["GladiatorId"] = gladiatorId;
                return gladiatorId;
            }

            return Convert.ToInt32( obj );
        }

    }

    #region Gladiator Data
    private void BindGladiator()
    {
        Gladiator gladiator = Gladiator.GetGladiator( ViewingGladiatorId );
        if ( !gladiator.Exists ) Server.Transfer( "Default.aspx" );

        PageTitleLabel.Text = "Viewing " + gladiator.User + "'s Gladiator";

        GladiatorAvatar.ImageUrl = "~/Images/Gladiators/" + gladiator.GladiatorClass + "avatar.jpg";
        GladiatorAvatar.ToolTip = gladiator.GladiatorClass;

        GladiatorNameLabel.Text = gladiator.Name;
        GladiatorSummaryLabel.Text = gladiator.Race + " " + gladiator.GladiatorClass;
        GodLabel.Text = "Religion: " + gladiator.God;

        ConstitutionLabel.Text = "Constitution: " + gladiator.Constitution.ToString();
        CunningLabel.Text = "Cunning: " + gladiator.Cunning.ToString();
        EnduranceLabel.Text = "Endurance: " + gladiator.Endurance.ToString();
        StrengthLabel.Text = "Strength: " + gladiator.Strength.ToString();

        BattleLabel.Text = gladiator.Battles.ToString();
        VictoryLabel.Text = gladiator.Victories.ToString();
        DefeatLabel.Text = gladiator.Defeats.ToString();
        DrawLabel.Text = gladiator.Draws.ToString();

        WeaponsLabel.Text = "Weapons: <i>" + gladiator.WeaponSummary + "</i>";
        ArmorLabel.Text = "Armor: <i>" + gladiator.ArmorSummary + "</i>";

        DietLabel.Text = "Diet: <i>" + gladiator.Diet + "</i>";

        FameLabel.Text = gladiator.Fame.ToString();
        
    }
    #endregion

    #region ChallengeListGridView

    void Challenges_RowCommand( object sender, GridViewCommandEventArgs e )
    {
        int id = Convert.ToInt32( e.CommandArgument );
        switch ( e.CommandName )
        {
            case "reject":
                SetChallengeStatus( id, 3 );
                break;
            case "retract":
                SetChallengeStatus( id, 4 );
                break;
        }
    }

    private void SetChallengeStatus( int id, int statusId )
    {
        Challenge challenge = Challenge.GetChallenge( id );
        challenge.ChallengeStatusId = statusId;

        challenge.Save();

        Session["CurrentObject"] = null;
        Challenges.DataBind();
    }


    public string HowLongAgo( SmartDate challengeDate )
    {
        TimeSpan ts = DateTime.Now - challengeDate.Date;

        if ( ts.TotalDays < 1 )
        {
            if ( ts.TotalMinutes < 60 )
            {
                if ( ts.TotalMinutes < 2 )
                {
                    return "Just now";
                }
                else
                {
                    return Math.Floor( ts.TotalMinutes ).ToString() + " minutes ago today";
                }
            }
            else
            {
                return ts.Hours == 1 ? "1 hour ago today" : ts.Hours.ToString() + " hours ago today";
            }
        }
        else if ( ts.TotalDays < 2 )
        {
            return "Yesterday";
        }
        else
        {
            return Math.Floor( ts.TotalDays ).ToString() + " days ago";
        }
    }

    public string ActionOption( int challengerId, int challengerTacticsId )
    {
        if ( Master.Identity.GladiatorId != challengerId )
        {
            return "accept";
        }
        else if ( Master.Identity.GladiatorId == challengerId && challengerTacticsId == 0 )
        {
            return "view";
        }

        return string.Empty;
    }

    public string CancelOption( int challengerId )
    {
        return Master.Identity.GladiatorId == challengerId ? "retract" : "reject";
    }
    #endregion

    #region ChallengeListDataSource
    protected void ChallengeListDataSource_SelectObject( object sender, Csla.Web.SelectObjectArgs e )
    {
        e.BusinessObject = GetChallengeList();

        if ( Master.Identity.GladiatorId != ViewingGladiatorId )
        {
            Challenges.Columns[1].Visible = true;
            Challenges.Columns[2].Visible = false;
            Challenges.Columns[3].Visible = false;
        }
        else
        {
            Challenges.Columns[1].Visible = false;
            Challenges.Columns[2].Visible = true;
            Challenges.Columns[3].Visible = true;
        }
    }

    private Csla.SortedBindingList<Challenge> GetChallengeList()
    {
        object businessObject = Session["CurrentObject"];
        if ( businessObject == null || !( businessObject is ChallengeList ) )
        {
            businessObject = ChallengeList.GetChallengeList( ViewingGladiatorId, ChallengeList.ChallengeFilter.All );
            Session["CurrentObject"] = businessObject;
        }

        Master.SortExpression = "ChallengeDate";
        Master.SortDirection = System.ComponentModel.ListSortDirection.Descending;

        SortedBindingList<Challenge> list = new SortedBindingList<Challenge>( (ChallengeList)businessObject );
        list.ApplySort( Master.SortExpression, Master.SortDirection );
        return list;
    }
    #endregion
}
