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
    public class ArmorList : ReadOnlyListBase<ArmorList, Armor>
    {
        #region Factory Methods

        /// <summary>
        /// Return a list of all armor for a gladiator.
        /// </summary>
        public static ArmorList GetGladiatorArmor( int gladiatorId )
        {
            return DataPortal.Fetch<ArmorList>( new GladiatorCriteria( gladiatorId ) );
        }

        private ArmorList()
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
                    cm.CommandText = "GetGladiatorsArmor";
                    cm.Parameters.AddWithValue( "@GladiatorId", criteria.GladiatorId );
                    using ( SafeDataReader dr = new SafeDataReader( cm.ExecuteReader() ) )
                    {
                        IsReadOnly = false;
                        while ( dr.Read() )
                        {
                            Armor armor = new Armor(
                                dr.GetInt32( "Id" ),
                                dr.GetInt32( "Damage" ),
                                dr.GetInt32( "ItemConditionId" ),
                                dr.GetString( "Name" ),
                                dr.GetString( "Description" ),
                                dr.GetString( "ItemCondition" ) );

                            this.Add( armor );
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
