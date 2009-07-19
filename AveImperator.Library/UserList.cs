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
    public class UserList : ReadOnlyListBase<UserList, User>
    {
        #region Factory Methods

        private static UserList _list;

        /// <summary>
        /// Return a list of all users.
        /// </summary>
        public static UserList GetUserList()
        {
            if ( _list == null ) _list = DataPortal.Fetch<UserList>();

            return _list;
        }

        /// <summary>
        /// Clears the static UserList that's been cache - should happen whenever user data changes, new user registers, &c.
        /// </summary>
        public static void InvalidateCache()
        {
            _list = null;
        }

        private UserList()
        { /* require use of factory methods */ }

        #endregion

        #region Data Access
        private void DataPortal_Fetch()
        {
            Fetch();
        }

        private void Fetch()
        {
            this.RaiseListChangedEvents = false;
            using ( SqlConnection cn = new SqlConnection( Database.ImperatorConnection ) )
            {
                cn.Open();
                using ( SqlCommand cm = cn.CreateCommand() )
                {
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.CommandText = "GetUsers";
                    using ( SafeDataReader dr = new SafeDataReader( cm.ExecuteReader() ) )
                    {
                        IsReadOnly = false;
                        while ( dr.Read() )
                        {
                            User user = new User(
                                dr.GetInt32( "Id" ),
                                dr.GetInt64( "FacebookId" ),
                                dr.GetInt32( "GladiatorId" ),
                                dr.GetString( "FacebookName" ),
                                dr.GetString( "Gladiator" ) );

                            this.Add( user );
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
