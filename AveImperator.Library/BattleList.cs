using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Csla;
using Csla.Data;
using AveImperator.Library.Security;

namespace AveImperator.Library
{
    [Serializable()]
    public class BattleList : ReadOnlyListBase<BattleList, Battle>
    {
        #region Factory Methods

        /// <summary>
        /// Return a list of all battles for a given gladiator.
        /// </summary>
        public static BattleList GetBattleList( int gladiatorId )
        {
            return DataPortal.Fetch<BattleList>( new Criteria( gladiatorId ) );
        }

        private BattleList()
        { /* require use of factory methods */ }

        #endregion

        #region Data Access
        [Serializable()]
        private class Criteria
        {
            private int _gladiatorId;
            public int GladiatorId
            {
                get { return _gladiatorId; }
            }

            public Criteria( int gladiatorId )
            {
                _gladiatorId = gladiatorId;
            }
        }

        private void DataPortal_Fetch( Criteria criteria )
        {
            Fetch( criteria );
        }

        private void Fetch( Criteria criteria )
        {
            this.RaiseListChangedEvents = false;
            using ( SqlConnection cn = new SqlConnection( Database.ImperatorConnection ) )
            {
                cn.Open();
                using ( SqlCommand cm = cn.CreateCommand() )
                {
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.CommandText = "GetBattles";
                    cm.Parameters.AddWithValue( "@GladiatorId", criteria.GladiatorId );
                    using ( SafeDataReader dr = new SafeDataReader( cm.ExecuteReader() ) )
                    {
                        IsReadOnly = false;
                        while ( dr.Read() )
                        {
                            Battle battle = new Battle(
                                dr.GetInt32( "Id" ),
                                dr.GetInt32( "ChallengeId" ),
                                dr.GetInt32( "VictorId" ),
                                dr.GetString( "Description" ),
                                dr.GetString( "Victor" ),
                                dr.GetSmartDate( "ChallengeDate" ) );

                            this.Add( battle );
                        }
                        IsReadOnly = true;
                    }
                }
            }
            this.RaiseListChangedEvents = true;
        }
        #endregion
    }
}
