using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Security.Principal;
using Csla;
using Csla.Data;

namespace AveImperator.Library.Security
{
    [Serializable()]
    public class AIIdentity : ReadOnlyBase<AIIdentity>, IIdentity
    {
        #region Business Methods
        private bool _isAuthenticated;

        private int _id;
        private long _facebookId;
        private string _facebookName;

        private int _gladiatorId;

        private Gladiator _gladiator;

        private List<string> _roles = new List<string>();

        public string AuthenticationType
        {
            get { return "Csla"; }
        }

        public bool IsAuthenticated
        {
            get { return _isAuthenticated; }
        }

        public string Name
        {
            get { return _facebookName; }
        }

        public int GladiatorId
        {
            get { return _gladiatorId; }
        }

        public Gladiator Champion
        {
            get 
            {
                if ( _gladiator == null )
                {
                    if ( _gladiatorId > 0 ) _gladiator = Gladiator.GetGladiator( _gladiatorId );
                    else { _gladiator = Gladiator.NewGladiator(); }
                }

                return _gladiator; }
        }

        public void InvalidateChampion()
        {
            _gladiator = null;
        }

        protected override object GetIdValue()
        {
            return _id;
        }

        internal bool IsInRole( string role )
        {
            foreach ( string r in _roles )
            {
                if ( r == role ) return true;
            }

            return false;
        }

        #endregion

        #region Factory Methods

        internal static AIIdentity UnauthenticatedIdentity()
        {
            return new AIIdentity();
        }

        internal static AIIdentity GetIdentity( long facebookId )
        {
            return DataPortal.Fetch<AIIdentity>( new FacebookCriteria( facebookId ) );
        }

        internal static void Register( long facebookId, string facebookName, Gladiator gladiator )
        {
            DataPortal_Insert( new RegistrationCriteria( facebookId, facebookName, gladiator ) );
        }

        private AIIdentity()
        {
            /* require use of factory methods */
        }

        #endregion

        #region Data Access

        [Serializable()]
        private class FacebookCriteria
        {
            private long _facebookId;
            public long FacebookId
            {
                get { return _facebookId; }
            }

            public FacebookCriteria( long facebookId )
            {
                _facebookId = facebookId;
            }
        }

        [Serializable()]
        private class RegistrationCriteria
        {
            private long _facebookId;
            private string _facebookName;

            private Gladiator _gladiator;

            public long FacebookId
            {
                get { return _facebookId; }
            }

            public string FacebookName
            {
                get { return _facebookName; }
            }

            public Gladiator Gladiator
            {
                get { return _gladiator; }
            }

            public RegistrationCriteria( long facebookId, string facebookName, Gladiator gladiator )
            {
                _facebookId = facebookId;
                _facebookName = facebookName;
                _gladiator = gladiator;
            }
        }

        private void DataPortal_Fetch( FacebookCriteria criteria )
        {
            using ( SqlConnection cn = new SqlConnection( Database.ImperatorConnection ) )
            {
                cn.Open();
                using ( SqlCommand cm = cn.CreateCommand() )
                {
                    cm.CommandText = "Login";
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.AddWithValue( "@FacebookId", criteria.FacebookId );
                    using ( SafeDataReader dr = new SafeDataReader( cm.ExecuteReader() ) )
                    {
                        if ( dr.Read() )
                        {
                            _id = dr.GetInt32( "Id" );
                            _facebookId = dr.GetInt64( "FacebookId" );
                            _facebookName = dr.GetString( "FacebookName" );

                            _gladiatorId = dr.GetInt32( "GladiatorId" );

                            _isAuthenticated = true;
                        }
                        else
                        {
                            _id = 0;
                            _facebookId = 0;
                            _gladiatorId = 0;

                            _facebookName = string.Empty;

                            _isAuthenticated = false;
                        }
                    }
                }
            }
        }

        [Transactional( TransactionalTypes.TransactionScope )]
        private static void DataPortal_Insert( RegistrationCriteria criteria )
        {
            using ( SqlConnection cn = new SqlConnection( Database.ImperatorConnection ) )
            {
                cn.Open();
                ApplicationContext.LocalContext["cn"] = cn;

                int userId = 0;

                using ( SqlCommand cm = cn.CreateCommand() )
                {
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.CommandText = "AddUser";
                    cm.Parameters.AddWithValue( "@FacebookId", criteria.FacebookId );
                    cm.Parameters.AddWithValue( "@FacebookName", criteria.FacebookName );

                    SqlParameter param = new SqlParameter( "@NewId", SqlDbType.Int );
                    param.Direction = ParameterDirection.Output;
                    cm.Parameters.Add( param );

                    cm.ExecuteNonQuery();

                    userId = (int)cm.Parameters["@NewId"].Value;
                }

                criteria.Gladiator.UserId = userId;
                criteria.Gladiator.Save();

                // removing of item only needed for local data portal
                if ( ApplicationContext.ExecutionLocation == ApplicationContext.ExecutionLocations.Client )
                    ApplicationContext.LocalContext.Remove( "cn" );
            }
        }
        #endregion
    }
}
