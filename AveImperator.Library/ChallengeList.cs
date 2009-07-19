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
    public class ChallengeList : ReadOnlyListBase<ChallengeList, Challenge>
    {
        #region Factory Methods

        /// <summary>
        /// Return a list of all challenges for a given gladiator.
        /// </summary>
        public static ChallengeList GetChallengeList( int gladiatorId, ChallengeFilter filter )
        {
            return DataPortal.Fetch<ChallengeList>( new Criteria( gladiatorId, filter ) );
        }

        private ChallengeList()
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

            private ChallengeFilter _filter;
            public ChallengeFilter Filter
            {
                get { return _filter; }
            }

            public Criteria( int gladiatorId, ChallengeFilter filter )
            {
                _gladiatorId = gladiatorId;
                _filter = filter;
            }
        }

        public enum ChallengeFilter
        {
            All = 0,
            ChallengingOnly = 1,
            ChallengedOnly = 2
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
                    cm.CommandText = "GetChallenges";
                    cm.Parameters.AddWithValue( "@GladiatorId", criteria.GladiatorId );
                    using ( SafeDataReader dr = new SafeDataReader( cm.ExecuteReader() ) )
                    {
                        IsReadOnly = false;
                        while ( dr.Read() )
                        {
                            Challenge challenge = new Challenge(
                                dr.GetInt32( "Id" ),
                                dr.GetInt32( "ChallengeStatusId" ),
                                dr.GetInt32( "ChallengerId" ),
                                dr.GetInt32( "ChallengedId" ),
                                dr.GetInt32( "ChallengerTacticId" ),
                                dr.GetInt32( "ChallengedTacticId" ),
                                dr.GetInt32( "CityId" ),
                                dr.GetString( "ChallengeStatus" ),
                                dr.GetString( "ChallengerName" ),
                                dr.GetString( "ChallengedName" ),
                                dr.GetString( "ChallengerOpeningWords" ),
                                dr.GetString( "ChallengedOpeningWords" ),
                                dr.GetSmartDate( "ChallengeDate" ) );

                            this.Add( challenge );
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
