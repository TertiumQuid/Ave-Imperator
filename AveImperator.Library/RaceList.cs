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
    public class RaceList : ReadOnlyListBase<RaceList, Race>
    {
        #region Factory Methods

        private static RaceList _list;

        /// <summary>
        /// Return a list of all races.
        /// </summary>
        public static RaceList GetRaceList()
        {
            if ( _list == null ) _list = DataPortal.Fetch<RaceList>();

            return _list;
        }

        /// <summary>
        /// Clears the static RaceList that's been cached; rarely if ever will this need to happen.
        /// </summary>
        public static void InvalidateCache()
        {
            _list = null;
        }

        private RaceList()
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
                    cm.CommandText = "GetRaces";
                    using ( SafeDataReader dr = new SafeDataReader( cm.ExecuteReader() ) )
                    {
                        IsReadOnly = false;
                        while ( dr.Read() )
                        {
                            Race race = new Race(
                                dr.GetInt32( "Id" ),
                                dr.GetInt32( "Constitution" ),
                                dr.GetInt32( "Cunning" ),
                                dr.GetInt32( "Endurance" ),
                                dr.GetInt32( "Strength" ),
                                dr.GetString( "Name" ),
                                dr.GetString( "Description" ) );

                            this.Add( race );
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
