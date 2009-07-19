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
    public class WeaponList : ReadOnlyListBase<WeaponList, Weapon>
    {
        #region Factory Methods

        /// <summary>
        /// Return a list of all weapons for a gladiator.
        /// </summary>
        public static WeaponList GetGladiatorWeapons( int gladiatorId )
        {
            return DataPortal.Fetch<WeaponList>( new GladiatorCriteria( gladiatorId ) );
        }

        private WeaponList()
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

        private void DataPortal_Fetch( GladiatorCriteria criteria )
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
                    cm.CommandText = "GetGladiatorsWeapons";
                    cm.Parameters.AddWithValue( "@GladiatorId", criteria.GladiatorId );
                    using ( SafeDataReader dr = new SafeDataReader( cm.ExecuteReader() ) )
                    {
                        IsReadOnly = false;
                        while ( dr.Read() )
                        {
                            Weapon weapon = new Weapon(
                                dr.GetInt32( "Id" ),
                                dr.GetInt32( "Damage" ),
                                dr.GetInt32( "ItemConditionId" ),
                                dr.GetString( "Name" ),
                                dr.GetString( "Description" ),
                                dr.GetString( "ItemCondition" ) );

                            this.Add( weapon );
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
