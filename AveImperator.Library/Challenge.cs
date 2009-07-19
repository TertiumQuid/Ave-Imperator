using System;
using System.Data;
using System.Data.SqlClient;
using Csla;
using Csla.Data;
using AveImperator.Library.Security;

namespace AveImperator.Library
{
    [Serializable()]
    public class Challenge : BusinessBase<Challenge>
    {
        #region Business Methods

        private int _id;
        private int _challengeStatusId;
        private int _challengerId;
        private int _challengedId;
        private int _challengerTacticId;
        private int _challengedTacticId;

        private string _challengeStatus;

        private string _challengerOpeningWords;
        private string _challengedOpeningWords;

        private string _challengerName;
        private string _challengedName;

        private SmartDate _challengeDate;

        private City _city;

        [System.ComponentModel.DataObjectField( true, true )]
        public int Id
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _id;
            }
        }

        public int ChallengeStatusId
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _challengeStatusId;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( _challengeStatusId != value )
                {
                    _challengeStatusId = value;
                    PropertyHasChanged();
                }
            }
        }

        public int ChallengerId
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _challengerId;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( _challengerId != value )
                {
                    _challengerId = value;
                    PropertyHasChanged();
                }
            }
        }

        public int ChallengedId
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _challengedId;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( _challengedId != value )
                {
                    _challengedId = value;
                    PropertyHasChanged();
                }
            }
        }

        public int ChallengerTacticId
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _challengerTacticId;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( _challengerTacticId != value )
                {
                    _challengerTacticId = value;
                    PropertyHasChanged();
                }
            }
        }

        public int ChallengedTacticId
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _challengedTacticId;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( _challengedTacticId != value )
                {
                    _challengedTacticId = value;
                    PropertyHasChanged();
                }
            }
        }


        public string ChallengeStatus
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _challengeStatus;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( value == null ) value = string.Empty;
                if ( _challengeStatus != value )
                {
                    _challengeStatus = value;
                    PropertyHasChanged();
                }
            }
        }

        public string ChallengerOpeningWords
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _challengerOpeningWords;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( value == null ) value = string.Empty;
                if ( _challengerOpeningWords != value )
                {
                    _challengerOpeningWords = value;
                    PropertyHasChanged();
                }
            }
        }

        public string ChallengedOpeningWords
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _challengedOpeningWords;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( value == null ) value = string.Empty;
                if ( _challengedOpeningWords != value )
                {
                    _challengedOpeningWords = value;
                    PropertyHasChanged();
                }
            }
        }

        public string ChallengerName
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _challengerName;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( value == null ) value = string.Empty;
                if ( _challengerName != value )
                {
                    _challengerName = value;
                    PropertyHasChanged();
                }
            }
        }

        public string ChallengedName
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _challengedName;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( value == null ) value = string.Empty;
                if ( _challengedName != value )
                {
                    _challengedName = value;
                    PropertyHasChanged();
                }
            }
        }

        public string City
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                if ( _city == null ) _city = AveImperator.Library.City.NewCity();
                return _city.Name;
            }
        }

        public int CityId
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                if ( _city == null ) return 0;
                return _city.Id;
            }
            set
            {
                _city = AveImperator.Library.City.GetCity( value );
            }
        }

        public SmartDate ChallengeDate
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _challengeDate;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( _challengeDate != value )
                {
                    _challengeDate = value;
                    PropertyHasChanged();
                }
            }
        }

        public int DaysAgo
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                if ( _challengeDate.IsEmpty ) return 0;

                TimeSpan ts = _challengeDate.Date - DateTime.Now;
                return ts.Days;
            }
        }

        public bool Exists
        {
            get { return _id > 0; }
        }

        public override bool IsValid
        {
            get { return base.IsValid; }
        }

        public override bool IsDirty
        {
            get { return base.IsDirty; }
        }

        protected override object GetIdValue()
        {
            return _id;
        }

        #endregion

        #region Factory Methods
        public static Challenge NewChallenge()
        {
            return DataPortal.Create<Challenge>();
        }

        public static Challenge GetChallenge( int challengerId, int challengedId )
        {
            return DataPortal.Fetch<Challenge>( new GladiatorCriteria( challengerId, challengedId ) );
        }

        public static Challenge GetChallenge( int id )
        {
            return DataPortal.Fetch<Challenge>( new Criteria( id ) );
        }

        public override Challenge Save()
        {
            return base.Save();
        }

        private Challenge()
        { /* require use of factory methods */ }

        internal Challenge( int id, int challengeStatusId, int challengerId, int challengedId, int challengerTacticId, int challengedTacticId, int cityId, string challengeStatus, string challengerName, string challengedName, string challengerOpeningWords, string challengedOpeningWords, SmartDate challengeDate )
        {
            _id = id;
            _challengeStatusId = challengeStatusId;

            _challengerId = challengerId;
            _challengedId = challengedId;

            _challengeStatus = challengeStatus;

            _challengerName = challengerName;
            _challengedName = challengedName;

            _challengerOpeningWords = challengerOpeningWords;
            _challengedOpeningWords = challengedOpeningWords;

            _challengeDate = challengeDate;

            _city = AveImperator.Library.City.GetCity( cityId );
        }

        #endregion

        #region Data Access
        [Serializable()]
        private class Criteria
        {
            private int _id;
            public int ID
            {
                get { return _id; }
            }

            public Criteria( int id )
            {
                _id = id;
            }
        }

        [Serializable()]
        private class GladiatorCriteria
        {
            private int _challengerId;
            public int ChallengerId
            {
                get { return _challengerId; }
            }

            private int _challengedId;
            public int ChallengedId
            {
                get { return _challengedId; }
            }

            public GladiatorCriteria( int challengerId, int challengedId )
            {
                _challengerId = challengerId;
                _challengedId = challengedId;
            }
        }

        [RunLocal()]
        protected override void DataPortal_Create()
        {
            ValidationRules.CheckRules();
        }

        private void DataPortal_Fetch( GladiatorCriteria criteria )
        {
            using ( SqlConnection cn = new SqlConnection( Database.ImperatorConnection ) )
            {
                cn.Open();
                using ( SqlCommand cm = cn.CreateCommand() )
                {
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.CommandText = "GetChallenge";
                    cm.Parameters.AddWithValue( "@ChallengerId", criteria.ChallengerId );
                    cm.Parameters.AddWithValue( "@ChallengedId", criteria.ChallengedId );

                    using ( SafeDataReader dr = new SafeDataReader( cm.ExecuteReader() ) )
                    {
                        if ( dr.Read() )
                        {
                            _id = dr.GetInt32( "Id" );
                            _challengeStatusId = dr.GetInt32( "ChallengeStatusId" );

                            _challengerId = dr.GetInt32( "ChallengerId" );
                            _challengedId = dr.GetInt32( "ChallengedId" );

                            _challengeStatus = dr.GetString( "ChallengeStatus" );

                            _challengerOpeningWords = dr.GetString( "ChallengerOpeningWords" );
                            _challengedOpeningWords = dr.GetString( "ChallengedOpeningWords" );
                            _challengerName = dr.GetString( "Challenger" );
                            _challengedName = dr.GetString( "Challenged" );

                            _challengeDate = dr.GetSmartDate( "ChallengeDate" );

                            _city = AveImperator.Library.City.GetCity( dr.GetInt32( "CityId" ) );
                        }
                    }
                }
            }
        }

        private void DataPortal_Fetch( Criteria criteria )
        {
            using ( SqlConnection cn = new SqlConnection( Database.ImperatorConnection ) )
            {
                cn.Open();
                using ( SqlCommand cm = cn.CreateCommand() )
                {
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.CommandText = "GetChallenge";
                    cm.Parameters.AddWithValue( "@Id", criteria.ID );

                    using ( SafeDataReader dr = new SafeDataReader( cm.ExecuteReader() ) )
                    {
                        if ( dr.Read() )
                        {
                            _id = dr.GetInt32( "Id" );
                            _challengeStatusId = dr.GetInt32( "ChallengeStatusId" );

                            _challengerId = dr.GetInt32( "ChallengerId" );
                            _challengedId = dr.GetInt32( "ChallengedId" );
                            _challengerTacticId = dr.GetInt32( "ChallengerTacticId" );
                            _challengedTacticId = dr.GetInt32( "ChallengedTacticId" );

                            _challengeStatus = dr.GetString( "ChallengeStatus" );

                            _challengerOpeningWords = dr.GetString( "ChallengerOpeningWords" );
                            _challengedOpeningWords = dr.GetString( "ChallengedOpeningWords" );
                            _challengerName = dr.GetString( "Challenger" );
                            _challengedName = dr.GetString( "Challenged" );

                            _challengeDate = dr.GetSmartDate( "ChallengeDate" );

                            _city = AveImperator.Library.City.GetCity( dr.GetInt32( "CityId" ) );
                        }
                    }
                }
            }
        }

        [Transactional( TransactionalTypes.TransactionScope )]
        protected override void DataPortal_Insert()
        {
            using ( SqlConnection cn = new SqlConnection( Database.ImperatorConnection ) )
            {
                cn.Open();
                ApplicationContext.LocalContext["cn"] = cn;
                using ( SqlCommand cm = cn.CreateCommand() )
                {
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.CommandText = "AddChallenge";
                    cm.Parameters.AddWithValue( "@ChallengerId", _challengerId );
                    cm.Parameters.AddWithValue( "@ChallengedId", _challengedId );
                    cm.Parameters.AddWithValue( "@ChallengerOpeningWords", _challengerOpeningWords );
                    cm.Parameters.AddWithValue( "@ChallengedOpeningWords", _challengedOpeningWords );
                    cm.Parameters.AddWithValue( "@ChallengerTacticId", _challengerTacticId );
                    cm.Parameters.AddWithValue( "@ChallengedTacticId", _challengedTacticId );
                    cm.Parameters.AddWithValue( "@CityId", CityId );
                    SqlParameter param = new SqlParameter( "@NewId", SqlDbType.Int );
                    param.Direction = ParameterDirection.Output;
                    cm.Parameters.Add( param );

                    cm.ExecuteNonQuery();

                    _id = (int)cm.Parameters["@NewId"].Value;
                }

                // removing of item only needed for local data portal
                if ( ApplicationContext.ExecutionLocation == ApplicationContext.ExecutionLocations.Client )
                    ApplicationContext.LocalContext.Remove( "cn" );
            }
        }

        [Transactional( TransactionalTypes.TransactionScope )]
        protected override void DataPortal_Update()
        {
            using ( SqlConnection cn = new SqlConnection( Database.ImperatorConnection ) )
            {
                cn.Open();
                ApplicationContext.LocalContext["cn"] = cn;
                if ( base.IsDirty )
                {
                    using ( SqlCommand cm = cn.CreateCommand() )
                    {
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.CommandText = "UpdateChallenge";
                        cm.Parameters.AddWithValue( "@Id", _id );
                        cm.Parameters.AddWithValue( "@ChallengeStatusId", _challengeStatusId );
                        cm.Parameters.AddWithValue( "@ChallengerOpeningWords", _challengerOpeningWords );
                        cm.Parameters.AddWithValue( "@ChallengedOpeningWords", _challengedOpeningWords );
                        cm.Parameters.AddWithValue( "@ChallengerTacticId", _challengerTacticId );
                        cm.Parameters.AddWithValue( "@ChallengedTacticId", _challengedTacticId );

                        cm.ExecuteNonQuery();
                    }
                }

                // removing of item only needed for local data portal
                if ( ApplicationContext.ExecutionLocation == ApplicationContext.ExecutionLocations.Client )
                    ApplicationContext.LocalContext.Remove( "cn" );
            }
        }
        #endregion
    }
}
