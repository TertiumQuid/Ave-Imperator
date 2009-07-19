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
    public class GladiatorClassList : ReadOnlyListBase<GladiatorClassList, GladiatorClass>
    {
        #region Factory Methods

        private static GladiatorClassList _list;

        /// <summary>
        /// Return a list of all gladiator classess.
        /// </summary>
        public static GladiatorClassList GetGladiatorClassList()
        {
            if ( _list == null ) _list = DataPortal.Fetch<GladiatorClassList>();

            return _list;
        }

        /// <summary>
        /// Clears the static GladiatorClassList that's been cached; rarely if ever will this need to happen.
        /// </summary>
        public static void InvalidateCache()
        {
            _list = null;
        }

        private GladiatorClassList()
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
                    cm.CommandText = "GetGladiatorClasses";
                    using ( SafeDataReader dr = new SafeDataReader( cm.ExecuteReader() ) )
                    {
                        IsReadOnly = false;
                        while ( dr.Read() )
                        {
                            GladiatorClass gladiatorClass = new GladiatorClass(
                                dr.GetInt32( "Id" ),
                                dr.GetInt32( "Constitution" ),
                                dr.GetInt32( "Cunning" ),
                                dr.GetInt32( "Endurance" ),
                                dr.GetInt32( "Strength" ),
                                dr.GetString( "Name" ),
                                dr.GetString( "Description" ) );

                            this.Add( gladiatorClass );
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
