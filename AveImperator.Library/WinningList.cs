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
    public class WinningList : ReadOnlyListBase<WinningList, Winning>
    {
        #region Factory Methods

        /// <summary>
        /// Return a list of all current winnings for a gladiator.
        /// </summary>
        public static WinningList GetGladiatorWinnings( int gladiatorId )
        {
            return DataPortal.Fetch<WinningList>( new GladiatorCriteria( gladiatorId ) );
        }

        /// <summary>
        /// Return a list of all winnings for a city.
        /// </summary>
        public static WinningList GetCityWinnings( int cityId )
        {
            return DataPortal.Fetch<WinningList>( new CityCriteria( cityId ) );
        }

        private WinningList()
        { /* require use of factory methods */ }

        #endregion

        #region Data Access
        [Serializable()]
        private class GladiatorCriteria
        {
            private int _gladiatorId;
            public int GladiatorId
            {
                get { return _gladiatorId; }
            }

            public GladiatorCriteria( int gladiatorId )
            {
                _gladiatorId = gladiatorId;
            }
        }

        [Serializable()]
        private class CityCriteria
        {
            private int _cityId;
            public int CityId
            {
                get { return _cityId; }
            }

            public CityCriteria( int cityId )
            {
                _cityId = cityId;
            }
        }

        private void DataPortal_Fetch( GladiatorCriteria criteria )
        {
            Fetch( criteria );
        }

        private void DataPortal_Fetch( CityCriteria criteria )
        {
            Fetch( criteria );
        }

        private void Fetch( GladiatorCriteria criteria )
        {
            this.RaiseListChangedEvents = false;
            using ( SqlConnection cn = new SqlConnection( Database.ImperatorConnection ) )
            {
                cn.Open();
                using ( SqlCommand cm = cn.CreateCommand() )
                {
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.CommandText = "GetWinnings";
                    cm.Parameters.AddWithValue( "@GladiatorId", criteria.GladiatorId );
                    using ( SafeDataReader dr = new SafeDataReader( cm.ExecuteReader() ) )
                    {
                        IsReadOnly = false;
                        while ( dr.Read() )
                        {
                            Winning winning = new Winning(
                                dr.GetInt32( "Id" ),
                                dr.GetInt32( "Value" ),
                                dr.GetInt32( "Quantity" ),
                                dr.GetString( "Name" ),
                                dr.GetString( "Description" ),
                                dr.GetString( "Unit" ) );

                            this.Add( winning );
                        }
                        IsReadOnly = true;
                    }
                }
            }
            this.RaiseListChangedEvents = true;
        }

        private void Fetch( CityCriteria criteria )
        {
            this.RaiseListChangedEvents = false;
            using ( SqlConnection cn = new SqlConnection( Database.ImperatorConnection ) )
            {
                cn.Open();
                using ( SqlCommand cm = cn.CreateCommand() )
                {
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.CommandText = "GetWinnings";
                    cm.Parameters.AddWithValue( "@CityId", criteria.CityId );
                    using ( SafeDataReader dr = new SafeDataReader( cm.ExecuteReader() ) )
                    {
                        IsReadOnly = false;
                        while ( dr.Read() )
                        {
                            Winning winning = new Winning(
                                dr.GetInt32( "Id" ),
                                dr.GetInt32( "Value" ),
                                dr.GetInt32( "Min" ),
                                dr.GetInt32( "Max" ),
                                dr.GetInt32( "Chance" ),
                                dr.GetString( "Name" ),
                                dr.GetString( "Description" ),
                                dr.GetString( "Unit" ) );

                            this.Add( winning );
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
