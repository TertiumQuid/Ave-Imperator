using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using AveImperator.Library.Security;
using Csla;
using Csla.Data;

namespace AveImperator.Library
{
    [Serializable()]
    public class BattleCommand : CommandBase
    {
        private Battle _battle;
        private int _challengeId = 0;

        public static Battle DoBattle( int challengeId )
        {
            BattleCommand command;
            command = DataPortal.Execute<BattleCommand>( new BattleCommand( challengeId ) );

            return command._battle;
        }

        private BattleCommand( int challengeId )
        {
            _challengeId = challengeId;
        }

        protected override void DataPortal_Execute()
        {
            Challenge challenge = Challenge.GetChallenge( _challengeId );

            Gladiator challenger = Gladiator.GetGladiator( challenge.ChallengerId );
            Gladiator challenged = Gladiator.GetGladiator( challenge.ChallengedId );

            ActionList challengerActions = ActionList.GetActionList( challenge.ChallengerTacticId );
            ActionList challengedActions = ActionList.GetActionList( challenge.ChallengedTacticId );

            int challengerHealth = challenger.Health;
            int challengerStamina = challenger.Stamina;

            int challengedHealth = challenged.Health;
            int challengedStamina = challenged.Stamina;

            int challengerIndex = 0;
            int challengedIndex = 0;

            int challengerEntertainment = 0;
            int challengedEntertainment = 0;

            BattleStatus battleStatus = BattleStatus.Active;

            int turnsLasted = 0;
            int? victorId = null;

            StringBuilder description = new StringBuilder();

            while ( battleStatus == BattleStatus.Active )
            {
                ScoreComparisonResult result = CompareScores( challengerActions[challengerIndex], challengedActions[challengedIndex] );

                int damage = 0;
                int defense = 0;
                switch ( result )
                {
                    case ScoreComparisonResult.EqualOffense:
                        damage = GetDamage( challenged, challengedActions[challengedIndex].ManeuverId );
                        defense = GetDefense( challenger, 1 );
                        if ( damage - defense > 0 ) challengerHealth -= ( damage - defense );

                        description.Append( "<br>(" + damage.ToString() + " [dam] - " + defense.ToString() + " [def] = " + ( damage - defense ) + ")<br>" );

                        damage = GetDamage( challenger, challengerActions[challengerIndex].ManeuverId );
                        defense = GetDefense( challenged, 1 );
                        if ( damage - defense > 0 ) challengedHealth -= ( damage - defense );

                        description.Append( "<br>(" + damage.ToString() + " [dam] - " + defense.ToString() + " [def] = " + ( damage - defense ) + ")<br>" );

                        description.Append( "At the same instant " + challenger.Name + " " + PluralizeMove( challengerActions[challengerIndex].Maneuver ).ToLower() + " at his foe, " );
                        description.Append( challenged.Name + " " + PluralizeMove( challengedActions[challengedIndex].Maneuver ).ToLower() + " back, each man landing a painful blow on the other. " );
                        description.Append( "The crowd cheers! " );

                        challengerEntertainment += challengerActions[challengerIndex].Score;
                        challengedEntertainment += challengedActions[challengedIndex].Score;
                        break;
                    case ScoreComparisonResult.OffensiveUndercut:
                        if ( challengerActions[challengerIndex].Score < challengedActions[challengedIndex].Score )
                        {
                            damage = GetDamage( challenger, challengerActions[challengerIndex].ManeuverId ) * 2;
                            defense = GetDefense( challenged, challengedActions[challengedIndex].Score );
                            if ( damage - defense > 0 ) challengedHealth -= ( damage - defense );

                            description.Append( "<br>(" + damage.ToString() + " [dam] - " + defense.ToString() + " [def] = " + ( damage - defense ) + ")<br>" );

                            description.Append( challenged.Name + " carelessly " + PluralizeMove( challengedActions[challengedIndex].Maneuver ).ToLower() + " " );
                            description.Append( "but the cunning " + challenger.GladiatorClass.ToLower() + "'s " + PluralizeMove( challengerActions[challengerIndex].Item ) + " is too quick " );
                            description.Append( "and delivers a skillful " + PluralizeMove( challengerActions[challengerIndex].Maneuver ).ToLower() + ", leaving " + challenged.Name + " scrambling on his knees, bloody and confused. " );

                            challengerEntertainment += challengerActions[challengerIndex].Score * 3;
                        }
                        else
                        {
                            damage = GetDamage( challenged, challengedActions[challengedIndex].ManeuverId ) * 2;
                            defense = GetDefense( challenger, challengerActions[challengedIndex].Score );
                            if ( damage - defense > 0 ) challengerHealth -= ( damage - defense );

                            description.Append( "<br>(" + damage.ToString() + " [dam] - " + defense.ToString() + " [def] = " + ( damage - defense ) + ")<br>" );

                            description.Append( challenger.Name + " carelessly " + PluralizeMove( challengerActions[challengerIndex].Maneuver ).ToLower() + " " );
                            description.Append( "but the relentless " + challenged.GladiatorClass.ToLower() + " " + PluralizeMove( challengedActions[challengedIndex].Maneuver ).ToLower() + " " );
                            description.Append( "and delivers a wicked riposte, leaving " + challenger.Name + " spilling his blood onto the sands. " );

                            challengedEntertainment += challengedActions[challengedIndex].Score * 3;
                        }
                        description.Append( "The crowd erupts in a frenzy of applause. " );
                        break;
                    case ScoreComparisonResult.DefensiveUndercut:
                        if ( challengerActions[challengerIndex].Score < challengedActions[challengedIndex].Score )
                        {
                            damage = GetDamage( challenged, challengedActions[challengedIndex].ManeuverId );
                            challengedHealth -= damage;

                            description.Append( "<br>(" + damage.ToString() + " [dam])<br>" );

                            description.Append( challenged.Name + " comes in strong with a " + challengedActions[challengedIndex].Maneuver.ToLower() + " from his " + challengedActions[challengedIndex].Item.ToLower() + ". " );
                            description.Append( challenger.Name + " reads the " + challenged.Race + " perfectly, however, and " + PluralizeMove( challengerActions[challengerIndex].Maneuver ).ToLower() + ", " );
                            description.Append( "turning " + challenged.Name + " over and delivering a frightful blow. " );

                            challengerEntertainment += challengerActions[challengerIndex].Score * 5;
                        }
                        else
                        {
                            damage = GetDamage( challenger, challengerActions[challengerIndex].ManeuverId );
                            challengerHealth -= damage;

                            description.Append( "<br>(" + damage.ToString() + " [dam])<br>" );

                            description.Append( "As " + challenger.Name + " overextends himself with a gawkish " + challengerActions[challengerIndex].Maneuver.ToLower() + " from his " + challengerActions[challengerIndex].Item.ToLower() + ". " );
                            description.Append( challenged.Name + " capitalizes on the mistake and and " + PluralizeMove( challengedActions[challengedIndex].Maneuver ).ToLower() + ", " );
                            description.Append( "sliding behind " + challenger.Name + "'s flank to deliver a punishing reposte! " );

                            challengedEntertainment += challengedActions[challengedIndex].Score * 5;
                        }
                        description.Append( "The roar of the crowd shakes the arena. " );
                        break;
                    case ScoreComparisonResult.GreaterOffenseLesserDefense:
                        if ( challengerActions[challengerIndex].Score < challengedActions[challengedIndex].Score )
                        {
                            damage = GetDamage( challenged, challengedActions[challengedIndex].ManeuverId );
                            defense = GetDefense( challenger, challengerActions[challengedIndex].Score );
                            if ( damage - defense > 0 ) challengerHealth -= ( damage - defense );

                            description.Append( "<br>(" + damage.ToString() + " [dam] - " + defense.ToString() + " [def] = " + ( damage - defense ) + ")<br>" );

                            description.Append( "Sorely harried, " + challenger.Name + " " + PluralizeMove( challengerActions[challengerIndex].Maneuver ).ToLower() + ",  " );
                            description.Append( "but is caught by a " + challengedActions[challengedIndex].Maneuver.ToLower() + " from " + challenged.Name + "'s " + challengedActions[challengedIndex].Item.ToLower() + ". " );

                            challengedEntertainment += challengedActions[challengedIndex].Score;
                        }
                        else
                        {
                            damage = GetDamage( challenger, challengerActions[challengerIndex].ManeuverId );
                            defense = GetDefense( challenged, challengedActions[challengedIndex].Score );
                            if ( damage - defense > 0 ) challengedHealth -= ( damage - defense );

                            description.Append( "<br>(" + damage.ToString() + " [dam] - " + defense.ToString() + " [def] = " + ( damage - defense ) + ")<br>" );

                            description.Append( "In an act of defense, " + challenged.Name + " moves to " + challengedActions[challengedIndex].Maneuver.ToLower() + ",  " );
                            description.Append( "but is caught by a " + challengerActions[challengerIndex].Maneuver.ToLower() + " from " + challenger.Name + "'s " + challengerActions[challengerIndex].Item.ToLower() + ". " );

                            challengerEntertainment += challengerActions[challengerIndex].Score;
                        }
                        description.Append( "\"Salvum lotum!\" chants the crowd. " );
                        break;
                    case ScoreComparisonResult.GreaterOffenseLesserOffense:
                        if ( challengerActions[challengerIndex].Score < challengedActions[challengedIndex].Score )
                        {
                            damage = GetDamage( challenged, challengedActions[challengedIndex].ManeuverId );
                            defense = GetDefense( challenger, 1 );
                            if ( damage - defense > 0 ) challengerHealth -= ( damage - defense );

                            description.Append( "<br>(" + damage.ToString() + " [dam] - " + defense.ToString() + " [def] = " + ( damage - defense ) + ")<br>" );

                            description.Append( "The brave " + challenger.Race + " " + challenger.Name + " advances with a " + challengerActions[challengerIndex].Maneuver.ToLower() + ", " );
                            description.Append( "only to be struck back by a bold " + challengedActions[challengedIndex].Maneuver.ToLower() + " from " + challenged.Name + "'s " + challengedActions[challengedIndex].Item.ToLower() + ". " );

                            challengedEntertainment += challengedActions[challengedIndex].Score;
                        }
                        else
                        {
                            damage = GetDamage( challenger, challengerActions[challengerIndex].ManeuverId );
                            defense = GetDefense( challenged, 1 );
                            if ( damage - defense > 0 ) challengedHealth -= ( damage - defense );

                            description.Append( "<br>(" + damage.ToString() + " [dam] - " + defense.ToString() + " [def] = " + ( damage - defense ) + ")<br>" );

                            description.Append( "The brave " + challenged.Race + " " + challenged.Name + " advances with a " + challengedActions[challengerIndex].Maneuver.ToLower() + ", " );
                            description.Append( "only to be struck back by a bold " + challengerActions[challengerIndex].Maneuver.ToLower() + " from " + challenger.Name + "'s " + challengerActions[challengerIndex].Item.ToLower() + ". " );

                            challengerEntertainment += challengerActions[challengerIndex].Score;
                        }
                        description.Append( "\"Habet! Habet!\", screams the mob. " );
                        break;
                    case ScoreComparisonResult.GreaterDefenseLesserOffense:
                        if ( challengerActions[challengerIndex].Score < challengedActions[challengedIndex].Score )
                        {
                            description.Append( "The " + challenger.GladiatorClass + " " + challenger.Name + " goes in for blood with a " + challengerActions[challengerIndex].Maneuver.ToLower() + " from his " + challengerActions[challengerIndex].Item.ToLower() + ", " );
                            description.Append( " but his foe deftly " + PluralizeMove( challengedActions[challengedIndex].Maneuver ).ToLower() + " and avoids the advance. " );

                            challengedEntertainment += challengedActions[challengedIndex].Score;
                        }
                        else
                        {
                            description.Append( challenged.Name + " comes at " + challenger.Name + " " + challengedActions[challengedIndex].Maneuver.ToLower() + "ing with his " + challengedActions[challengedIndex].Item.ToLower() + ", " );
                            description.Append( " but his target " + PluralizeMove( challengerActions[challengerIndex].Maneuver ).ToLower() + ", skillfully avoiding the attack. " );

                            challengerEntertainment += challengerActions[challengerIndex].Score;
                        }
                        break;
                    case ScoreComparisonResult.GreaterDefenseLesserDefense:
                        description.Append( challenger.Name + " " + PluralizeMove( challengerActions[challengerIndex].Maneuver ).ToLower() + ", " + challenged.Name + " " + PluralizeMove( challengedActions[challengedIndex].Maneuver ).ToLower() + ", " );
                        description.Append( "a brave and skillful display of fear. " );
                        break;
                    case ScoreComparisonResult.MutualDefensive:
                        description.Append( challenger.Name + " " + PluralizeMove( challengerActions[challengerIndex].Maneuver ).ToLower() + ", " + challenged.Name + " " + PluralizeMove( challengedActions[challengedIndex].Maneuver ).ToLower() + ", " );
                        description.Append( "both dancing about harmlessly. " );
                        break;
                }

                challengerStamina -= GetEndurance( challenger, challengerActions[challengerIndex].ManeuverId );
                challengedStamina -= GetEndurance( challenged, challengedActions[challengedIndex].ManeuverId );

                battleStatus = GetBattleStatus( challengerHealth, challengedHealth, challengerStamina, challengedStamina );

                if ( !( battleStatus == BattleStatus.Active ) )
                {
                    if ( (int)battleStatus > 4 )
                    {
                        if ( battleStatus == BattleStatus.ChallengedFellsChallenger )
                            description.Append( challenged.Name + "'s mighty " + challengedActions[challengedIndex].Maneuver.ToLower() + " is telling, and " + challenger.Name + " drops bleeding in agony. " );
                        else if ( battleStatus == BattleStatus.ChallengedExhaustsChallenger )
                            description.Append( challenger.Name + " wheezes out a last breath then slumps to his knees, choking and exhausted from the battle's exertion and the arena heat. " );

                        victorId = challenged.Id;
                        description.Append( challenged.Name + " is victorious! " + challenger.Name + " is defeated." );
                    }
                    else if ( (int)battleStatus > 2 )
                    {
                        if ( battleStatus == BattleStatus.ChallengerFellsChallenged )
                            description.Append( challenger.Name + "'s cruel " + challengerActions[challengerIndex].Maneuver.ToLower() + " finishes the contest, and " + challenged.Name + " goes toppling over, spilling blood this way and that. " );
                        else if ( battleStatus == BattleStatus.ChallengerExhaustsChallenged )
                            description.Append( challenged.Name + " wheezes out a last breath then slumps to his knees, choking and exhausted from the battle's exertion and the arena heat. " );

                        victorId = challenger.Id;
                        description.Append( challenger.Name + " is victorious! " + challenged.Name + " is defeated. " );
                    }
                    else
                    {
                        if ( battleStatus == BattleStatus.MutualExhaustion )
                            description.Append( "Falling to their knees, beyond exhaustion and useless for further battle, neither gladiator is victorious over the other and the fight ends. " );
                        if ( battleStatus == BattleStatus.MutualFell )
                            description.Append( "Collapsing lin a pool of blood, Charon will have to be called as both gladiators give to over their wounds and the battle ends. " );
                        
                        description.Append( "Neither gladiator is victorious over the other. " );
                    }
                }

                challengerIndex++;
                challengedIndex++;
                if ( challengerIndex == challengerActions.Count ) challengerIndex = 0;
                if ( challengedIndex == challengedActions.Count ) challengedIndex = 0;

                turnsLasted++;
            }
            TimeSpan duration = new TimeSpan( 0, 0, ( turnsLasted * 10 ) );
            description.Append( "The battle lasted " + ( Math.Floor( duration.TotalMinutes ) > 0 ? Math.Floor( duration.TotalMinutes ) + " minutes " : "" ) + ( duration.Seconds > 0 ? duration.Seconds + " seconds" : "" ) + "." );

            challenger.Fame += ( challengerEntertainment / 5 );
            challenged.Fame += ( challengedEntertainment / 5 );

            description.Append( "<br /><br />" );
            description.Append( challenger.Name + " gained " + ( challengerEntertainment / 5 ) + " points of fame.<br />" );
            description.Append( challenged.Name + " gained " + ( challengedEntertainment / 5 ) + " points of fame.<br /><br />" );

            Battle battle = Battle.NewBattle();
            battle.ChallengeId = challenge.Id;
            battle.VictorId = victorId.GetValueOrDefault( 0 );

            #region Winnings

            description.Append( "The Editor of the games has gratiously bestowed the following purse to the stalwart gladiators for their performance in the arena:" );
            description.Append( "<br /><ul>" );

            // 1. Add Sesterce
            double sesterce = 0;

            if ( battle.VictorId == challenger.Id )
            {
                battle.Victor = challenger.Name;
                sesterce = ( ( challengerEntertainment / 50 ) * Math.Log( turnsLasted ^ 3 ) + 3 ) * ( 1 - ( ( challengerHealth / challenger.Health ) ) );
                Winning.AddGladiatorWinning( challenger.Id, 1, Convert.ToInt32( sesterce ) );
                description.Append( "<li>" + challenger.Name + " was awarded " + Convert.ToInt32( sesterce ).ToString() + " sesterce.</li>" );

                sesterce = ( ( challengedEntertainment / 50 ) * Math.Log( turnsLasted ^ 3 ) + 3 ) * 0.15;
                Winning.AddGladiatorWinning( challenged.Id, 1, Convert.ToInt32( sesterce ) );
                description.Append( "<li>" + challenged.Name + " was awarded " + Convert.ToInt32( sesterce ).ToString() + " sesterce.</li>" );
            }
            else if ( battle.VictorId == challenged.Id )
            {
                battle.Victor = challenged.Name;
                sesterce = ( ( challengedEntertainment / 50 ) * Math.Log( turnsLasted ^ 3 ) + 3 ) * ( 1 - ( ( challengedHealth / challenged.Health ) ) );
                Winning.AddGladiatorWinning( challenged.Id, 1, Convert.ToInt32( sesterce ) );
                description.Append( "<li>" + challenged.Name + " was awarded " + Convert.ToInt32( sesterce ).ToString() + " sesterce.</li>" );

                sesterce = ( ( challengerEntertainment / 50 ) * Math.Log( turnsLasted ^ 3 ) + 3 ) * 0.15;
                Winning.AddGladiatorWinning( challenger.Id, 1, Convert.ToInt32( sesterce ) );
                description.Append( "<li>" + challenger.Name + " was awarded " + Convert.ToInt32( sesterce ).ToString() + " sesterce.</li>" );
            }
            else
            {
                battle.Victor = "Neither Gladiator";
                sesterce = ( ( challengedEntertainment / 50 ) * Math.Log( turnsLasted ^ 3 ) + 3 ) * 0.25;
                Winning.AddGladiatorWinning( challenged.Id, 1, Convert.ToInt32( sesterce ) );
                description.Append( "<li>" + challenged.Name + " was awarded " + Convert.ToInt32( sesterce ).ToString() + " sesterce.</li>" );

                sesterce = ( ( challengerEntertainment / 50 ) * Math.Log( turnsLasted ^ 3 ) + 3 ) * 0.25;
                Winning.AddGladiatorWinning( challenger.Id, 1, Convert.ToInt32( sesterce ) );
                description.Append( "<li>" + challenger.Name + " was awarded " + Convert.ToInt32( sesterce ).ToString() + " sesterce.</li>" );
            }

            description.Append( "</ul>" );

            Random random = new Random();
            WinningList winnings = City.GetCity( challenge.CityId ).Winnings;

            #endregion
            
            battle.Description = description.ToString();

            if ( battle.VictorId == challenger.Id )
            {
                challenger.Victories++;
                challenged.Defeats++;
            }
            else if ( battle.VictorId == challenged.Id )
            {
                challenger.Defeats++;
                challenged.Victories++;
            }
            else
            {
                challenger.Draws++;
                challenged.Draws++;
            }

            battle.Save();
            challenge.ChallengeStatusId = 5;
            challenge.Save();

            challenger.Save();
            challenged.Save();

            _battle = battle;
        }

        private string PluralizeMove( string move )
        {
            return move + ( move.EndsWith( "h" ) ? "es" : "s" );
        }

        private BattleStatus GetBattleStatus( int challengerHealth, int challengedHealth, int challengerStamina, int challengedStamina )
        {
            BattleStatus battleIsActive = BattleStatus.Active;

            if ( challengerHealth + challengedHealth <= 0 )
            {
                battleIsActive = BattleStatus.MutualFell;
            }
            else if ( challengerHealth <= 0 && challengedStamina <= 0 )
            {
                battleIsActive = BattleStatus.ChallengedFellsChallenger;
            }
            else if ( challengedHealth <= 0 && challengerStamina <= 0 )
            {
                battleIsActive = BattleStatus.ChallengerFellsChallenged;
            }
            else if ( challengerStamina + challengedStamina <= 0 )
            {
                battleIsActive = BattleStatus.MutualExhaustion;
            }
            else if ( challengedHealth <= 0 )
            {
                battleIsActive = BattleStatus.ChallengerFellsChallenged;
            }
            else if ( challengerHealth <= 0 )
            {
                battleIsActive = BattleStatus.ChallengedFellsChallenger;
            }
            else if ( challengerStamina <= 0 )
            {
                battleIsActive = BattleStatus.ChallengedExhaustsChallenger;
            }
            else if ( challengedStamina <= 0 )
            {
                battleIsActive = BattleStatus.ChallengerExhaustsChallenged;
            }

            return battleIsActive;
        }

        private int GetDamage( Gladiator gladiator, int maneuverId )
        {
            foreach ( Weapon w in gladiator.Weapons )
            {
                foreach ( Maneuver m in w.Maneuvers )
                {
                    if ( m.Id == maneuverId ) return (int)Math.Floor( ( gladiator.Strength * w.ItemConditionId ) + ( w.Damage * m.Score ) * 0.5 );
                }
            }

            return 0;
        }

        private int GetEndurance( Gladiator gladiator, int maneuverId )
        {
            foreach ( Weapon w in gladiator.Weapons )
            {
                foreach ( Maneuver m in w.Maneuvers )
                {
                    if ( m.Id == maneuverId ) return m.Endurance;
                }
            }
            foreach ( Armor a in gladiator.Armor )
            {
                foreach ( Maneuver m in a.Maneuvers )
                {
                    if ( m.Id == maneuverId ) return m.Endurance;
                }
            }

            return 0;
        }

        private int GetDefense( Gladiator gladiator, int maneuverScore )
        {
            int armor = 0;
            foreach ( Armor a in gladiator.Armor )
                armor += ( a.Damage * a.ItemConditionId );

            return maneuverScore * armor;
        }

        private static ScoreComparisonResult CompareScores( Action a1, Action a2 )
        {
            if ( a1.Score == a2.Score )
            {
                if ( !IsOffensiveAction( a1.Score ) ) return ScoreComparisonResult.MutualDefensive;
                else return ScoreComparisonResult.EqualOffense;
            }
            else if ( Math.Abs( a1.Score - a2.Score ) == 1 )
            {
                if ( ( IsOffensiveAction( a1.Score ) && a1.Score < a2.Score ) || ( IsOffensiveAction( a2.Score ) && a2.Score < a1.Score ) ) return ScoreComparisonResult.OffensiveUndercut;
                else return ScoreComparisonResult.DefensiveUndercut;
            }
            else if ( IsOffensiveAction( a1.Score ) && IsOffensiveAction( a2.Score ) )
            {
                return ScoreComparisonResult.GreaterOffenseLesserOffense;
            }
            else if (( IsOffensiveAction( a1.Score ) && a1.Score > a2.Score ) || ( IsOffensiveAction( a2.Score ) && a2.Score > a1.Score ))
            {
                return ScoreComparisonResult.GreaterOffenseLesserDefense;
            }
            else if ( ( !IsOffensiveAction( a1.Score ) && a1.Score > a2.Score ) || ( !IsOffensiveAction( a2.Score ) && a2.Score > a1.Score ) )
            {
                return ScoreComparisonResult.GreaterDefenseLesserOffense;
            }
            else
            {
                return ScoreComparisonResult.GreaterDefenseLesserDefense;
            }
        }

        private static bool IsOffensiveAction( int score )
        {
            return score % 2 == 0;
        }

        #region Enums
        public enum ScoreComparisonResult
        {
            MutualDefensive = 0,
            EqualOffense = 1,
            OffensiveUndercut = 2,
            DefensiveUndercut = 3,
            GreaterOffenseLesserDefense = 4,
            GreaterOffenseLesserOffense = 5,
            GreaterDefenseLesserOffense = 7,
            GreaterDefenseLesserDefense = 8
        }

        public enum BattleStatus
        {
            Active = 0,
            MutualExhaustion = 1,
            MutualFell = 2,
            ChallengerExhaustsChallenged = 3,
            ChallengerFellsChallenged = 4,
            ChallengedExhaustsChallenger = 5,
            ChallengedFellsChallenger = 6
        }
        #endregion
    }
}
