using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Csla;
using AveImperator.Library;
using AveImperator.Library.Security;
using Facebook.Service;
using Facebook.Web;

public partial class AddGladiator : System.Web.UI.Page
{
    protected void Page_Load( object sender, EventArgs e )
    {
        Race.DataBound += new EventHandler( Race_DataBound );
        God.DataBound += new EventHandler( God_DataBound );
        GladiatorClass.DataBound += new EventHandler( GladiatorClass_DataBound );

        ErrorMessage.Visible = false;

        Race.SelectedIndexChanged += new EventHandler( Profile_SelectedIndexChanged );
        God.SelectedIndexChanged += new EventHandler( Profile_SelectedIndexChanged );
        GladiatorClass.SelectedIndexChanged += new EventHandler( Profile_SelectedIndexChanged );
        Add.Click += new EventHandler( Add_Click );
    }

    #region Profile Updates
    void Profile_SelectedIndexChanged( object sender, EventArgs e )
    {
        int constitution = 0;
        int cunning = 0;
        int endurance = 0;
        int strength = 0;

        ClassDescLabel.Text = string.Empty;
        RaceDescLabel.Text = string.Empty;
        GodDescLabel.Text = string.Empty;

        if ( GladiatorClass.SelectedIndex > 0 )
        {
            AveImperator.Library.GladiatorClass gladiatorClass = AveImperator.Library.GladiatorClass.GetGladiatorClass( Convert.ToInt32( GladiatorClass.SelectedValue ) );
            constitution += gladiatorClass.Constitution;
            cunning += gladiatorClass.Cunning;
            endurance += gladiatorClass.Endurance;
            strength += gladiatorClass.Strength;

            ClassDescLabel.Text = gladiatorClass.Description;
        }

        if ( Race.SelectedIndex > 0 )
        {
            AveImperator.Library.Race race = AveImperator.Library.Race.GetRace( Convert.ToInt32( Race.SelectedValue ) );
            constitution += race.Constitution;
            cunning += race.Cunning;
            endurance += race.Endurance;
            strength += race.Strength;

            RaceDescLabel.Text = race.Description;
        }

        if ( God.SelectedIndex > 0 )
        {
            AveImperator.Library.God god = AveImperator.Library.God.GetGod( Convert.ToInt32( God.SelectedValue ) );
            constitution += god.Constitution;
            cunning += god.Cunning;
            endurance += god.Endurance;
            strength += god.Strength;

            GodDescLabel.Text = god.Description;
        }

        ConstitutionLabel.Text = constitution.ToString();
        CunningLabel.Text = cunning.ToString();
        EnduranceLabel.Text = endurance.ToString();
        StrengthLabel.Text = strength.ToString();
    }
    #endregion

    #region Registration / Gladiator Creation
    void Add_Click( object sender, EventArgs e )
    {
        if ( !ValidateRegistration() ) return;

        string name = Name.Text;

        int raceId = Convert.ToInt32( Race.SelectedValue );
        int godId = Convert.ToInt32( God.SelectedValue );
        int gladiatorClassId = Convert.ToInt32( GladiatorClass.SelectedValue );

        Gladiator gladiator = Gladiator.NewGladiator();

        gladiator.Name = Name.Text;
        gladiator.RaceId = Convert.ToInt32( Race.SelectedValue );
        gladiator.GodId = Convert.ToInt32( God.SelectedValue );
        gladiator.GladiatorClassId = Convert.ToInt32( GladiatorClass.SelectedValue );

        Race race = AveImperator.Library.Race.GetRace( gladiator.RaceId );
        God god = AveImperator.Library.God.GetGod( gladiator.GodId );
        GladiatorClass gladiatorClass = AveImperator.Library.GladiatorClass.GetGladiatorClass( gladiator.GladiatorClassId );

        gladiator.Constitution = race.Constitution + god.Constitution + gladiatorClass.Constitution;
        gladiator.Cunning = race.Cunning + god.Cunning + gladiatorClass.Cunning;
        gladiator.Endurance = race.Endurance + god.Endurance + gladiatorClass.Endurance;
        gladiator.Strength = race.Strength + god.Strength + gladiatorClass.Strength;

        Facebook.Service.User user = Master.FBApplication.Service.Users.GetUser( null, "name" );

        AIPrincipal.Register( Convert.ToInt64( user.ID ), user.Name, gladiator );
        AIPrincipal.Login( Convert.ToInt64( user.ID ) );

        HttpContext.Current.Session["CslaPrincipal"] = Csla.ApplicationContext.User;

        //Master.FBApplication.Redirect( this, "http://apps.facebook.com/aveimperator/viewgladiator.aspx" );
        Server.Transfer( "viewgladiator.aspx" );
    }

    bool ValidateRegistration()
    {
        bool isValid = true;

        List<string> errors = new List<string>();

        if ( Race.SelectedIndex < 1 ) errors.Add( "You must select a race." );
        if ( God.SelectedIndex < 1 ) errors.Add( "You must select a patron god." );
        if ( GladiatorClass.SelectedIndex < 1 ) errors.Add( "You must select a fighting class." );

        if ( Name.Text.Length == 0 ) errors.Add( "You must enter a name." );

        if ( errors.Count > 0 )
        {
            ErrorMessage.Message = "Your gladiator could <u>not be created</u> for the following reasons:<br /><br />";
            foreach ( string error in errors )
            {
                ErrorMessage.Message += "<li style=\"margin-left:13px;\">" + error + "</li>";
            }
            isValid = false;

            ErrorMessage.Visible = true;
        }

        return isValid;
    }
    #endregion

    #region RaceListDataSource
    protected void RaceListDataSource_SelectObject( object sender, Csla.Web.SelectObjectArgs e )
    {
        e.BusinessObject = RaceList.GetRaceList();
    }

    void Race_DataBound( object sender, EventArgs e )
    {
        Race.Items.Insert( 0, new ListItem( "[ SELECT RACE ]", "" ) );
    }
    #endregion

    #region GodListDataSource
    protected void GodListDataSource_SelectObject( object sender, Csla.Web.SelectObjectArgs e )
    {
        e.BusinessObject = GodList.GetGodList();
    }

    void God_DataBound( object sender, EventArgs e )
    {
        God.Items.Insert( 0, new ListItem( "[ SELECT GOD ]", "" ) );
    }
    #endregion

    #region GladiatorClassListDataSource
    protected void GladiatorClassListDataSource_SelectObject( object sender, Csla.Web.SelectObjectArgs e )
    {
        e.BusinessObject = GladiatorClassList.GetGladiatorClassList();
    }

    void GladiatorClass_DataBound( object sender, EventArgs e )
    {
        GladiatorClass.Items.Insert( 0, new ListItem( "[ SELECT CLASS ]", "" ) );
    }
    #endregion
}
