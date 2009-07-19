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
    public class CityList : ReadOnlyListBase<CityList, City>
    {
        #region Factory Methods

        private static CityList _list;

        public static CityList NewCityList()
        {
            return DataPortal.Create<CityList>();
        }

        /// <summary>
        /// Return a list of all cities.
        /// </summary>
        public static CityList GetCityList()
        {
            if ( _list == null ) _list = DataPortal.Fetch<CityList>();

            return _list;
        }

        /// <summary>
        /// Return a list of all cities that meet a given fame requirment.
        /// </summary>
        public static CityList GetCityList( int fame )
        {
            if ( _list == null ) _list = DataPortal.Fetch<CityList>();

            CityList list = CityList.NewCityList();
            foreach ( City city in _list )
            {
                if ( city.FameRequired <= fame ) list.Add( city );
            }

            return list;
        }

        /// <summary>
        /// Clears the static CityList that's been cached; rarely if ever will this need to happen.
        /// </summary>
        public static void InvalidateCache()
        {
            _list = null;
        }

        private CityList()
        { /* require use of factory methods */ }

        #endregion

        #region Data Access

        [RunLocal()]
        private void DataPortal_Create()
        {
            IsReadOnly = false;
        }

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
                    cm.CommandText = "GetCities";
                    using ( SafeDataReader dr = new SafeDataReader( cm.ExecuteReader() ) )
                    {
                        IsReadOnly = false;
                        while ( dr.Read() )
                        {
                            City city = new City(
                                dr.GetInt32( "Id" ),
                                dr.GetInt32( "FameRequired" ),
                                dr.GetString( "Name" ),
                                dr.GetString( "Description" ) );

                            this.Add( city );
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
