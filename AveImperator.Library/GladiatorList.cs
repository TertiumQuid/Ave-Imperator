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
    public class GladiatorList : ReadOnlyListBase<GladiatorList, Gladiator>
    {
        #region Factory Methods

        /// <summary>
        /// Return a list of all gladiators.
        /// </summary>
        public static GladiatorList GetGladiatorList()
        {
            return DataPortal.Fetch<GladiatorList>();
        }
        
        /// <summary>
        /// Gets a list of gladiators for the arena view of a given gladiator
        /// </summary>
        /// <returns></returns>
        public static GladiatorList GetGladiatorList( int gladiatorId )
        {
            return DataPortal.Fetch<GladiatorList>( new ArenaCriteria( gladiatorId ) );
        }

        private GladiatorList()
        { /* require use of factory methods */ }

        #endregion

        #region Data Access
        [Serializable()]
        private class ArenaCriteria
        {
            private int _gladiatorId;
            public int GladiatorId
            {
                get { return _gladiatorId; }
            }

            public ArenaCriteria( int gladiatorId )
            {
                _gladiatorId = gladiatorId;
            }
        }

        private void DataPortal_Fetch()
        {
            Fetch();
        }

        private void DataPortal_Fetch( ArenaCriteria criteria )
        {
            Fetch( criteria );
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
                    cm.CommandText = "GetGladiators";
                    using ( SafeDataReader dr = new SafeDataReader( cm.ExecuteReader() ) )
                    {
                        IsReadOnly = false;
                        while ( dr.Read() )
                        {
                            Gladiator gladiator = new Gladiator(
                                dr.GetInt32( "Id" ),
                                dr.GetInt32( "UserId" ),
                                dr.GetInt32( "RaceId" ),
                                dr.GetInt32( "GodId" ),
                                dr.GetInt32( "GladiatorClassId" ),
                                dr.GetInt32( "DietId" ),
                                dr.GetInt32( "Constitution" ),
                                dr.GetInt32( "Cunning" ),
                                dr.GetInt32( "Endurance" ),
                                dr.GetInt32( "Strength" ),
                                dr.GetInt32( "Victories" ),
                                dr.GetInt32( "Defeats" ),
                                dr.GetInt32( "Draws" ),
                                dr.GetInt32( "Fame" ),
                                dr.GetString( "Name" ),
                                dr.GetString( "User" ),
                                dr.GetString( "Race" ),
                                dr.GetString( "God" ),
                                dr.GetString( "GladiatorClass" ) );

                            this.Add( gladiator );
                        }
                        IsReadOnly = true;
                    }
                }
            }
            this.RaiseListChangedEvents = true;
        }

        private void Fetch( ArenaCriteria criteria )
        {
            this.RaiseListChangedEvents = false;
            using ( SqlConnection cn = new SqlConnection( Database.ImperatorConnection ) )
            {
                cn.Open();
                using ( SqlCommand cm = cn.CreateCommand() )
                {
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.CommandText = "GetGladiators";
                    cm.Parameters.AddWithValue( "@GladiatorId", criteria.GladiatorId );
                    using ( SafeDataReader dr = new SafeDataReader( cm.ExecuteReader() ) )
                    {
                        IsReadOnly = false;
                        while ( dr.Read() )
                        {
                            Gladiator gladiator = new Gladiator(
                                dr.GetInt32( "Id" ),
                                dr.GetInt32( "UserId" ),
                                dr.GetInt32( "RaceId" ),
                                dr.GetInt32( "GodId" ),
                                dr.GetInt32( "GladiatorClassId" ),
                                dr.GetInt32( "DietId" ),
                                dr.GetInt32( "Constitution" ),
                                dr.GetInt32( "Cunning" ),
                                dr.GetInt32( "Endurance" ),
                                dr.GetInt32( "Strength" ),
                                dr.GetInt32( "Victories" ),
                                dr.GetInt32( "Defeats" ),
                                dr.GetInt32( "Draws" ),
                                dr.GetInt32( "Fame" ),
                                dr.GetString( "Name" ),
                                dr.GetString( "User" ),
                                dr.GetString( "Race" ),
                                dr.GetString( "God" ),
                                dr.GetString( "GladiatorClass" ) );

                            this.Add( gladiator );
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
