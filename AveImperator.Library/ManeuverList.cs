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
    public class ManeuverList : ReadOnlyListBase<ManeuverList, Maneuver>
    {
        #region Factory Methods

        /// <summary>
        /// Return a list of all maneuvers.
        /// </summary>
        public static ManeuverList GetManeuverList( int gladiatorId )
        {
            return DataPortal.Fetch<ManeuverList>( new GladiatorCriteria( gladiatorId ) );
        }

        public static ManeuverList GetWeaponManeuverList( int weaponId )
        {
            return DataPortal.Fetch<ManeuverList>( new WeaponCriteria( weaponId ) );
        }

        public static ManeuverList GetArmorManeuverList( int armorId )
        {
            return DataPortal.Fetch<ManeuverList>( new ArmorCriteria( armorId ) );
        }

        private ManeuverList()
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
        private class WeaponCriteria
        {
            private int _weaponId;
            public int WeaponId
            {
                get { return _weaponId; }
            }

            public WeaponCriteria( int weaponId )
            {
                _weaponId = weaponId;
            }
        }

        [Serializable()]
        private class ArmorCriteria
        {
            private int _armorId;
            public int ArmorId
            {
                get { return _armorId; }
            }

            public ArmorCriteria( int armorId )
            {
                _armorId = armorId;
            }
        }

        private void DataPortal_Fetch( GladiatorCriteria criteria )
        {
            Fetch( criteria );
        }

        private void DataPortal_Fetch( WeaponCriteria criteria )
        {
            Fetch( criteria );
        }

        private void DataPortal_Fetch( ArmorCriteria criteria )
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
                    cm.CommandText = "GetManeuvers";
                    cm.Parameters.AddWithValue( "@GladiatorId", criteria.GladiatorId );
                    using ( SafeDataReader dr = new SafeDataReader( cm.ExecuteReader() ) )
                    {
                        IsReadOnly = false;
                        while ( dr.Read() )
                        {
                            Maneuver maneuver = new Maneuver(
                                dr.GetInt32( "Id" ),
                                dr.GetInt32( "Score" ),
                                dr.GetInt32( "Endurance" ),
                                dr.GetInt32( "ItemId" ),
                                dr.GetString( "Name" ),
                                dr.GetString( "Item" ),
                                (Maneuver.ItemType)dr.GetInt32( "ItemType" ) );

                            this.Add( maneuver );
                        }
                        IsReadOnly = true;
                    }
                }
            }
            this.RaiseListChangedEvents = true;
        }

        private void Fetch( WeaponCriteria criteria )
        {
            this.RaiseListChangedEvents = false;
            using ( SqlConnection cn = new SqlConnection( Database.ImperatorConnection ) )
            {
                cn.Open();
                using ( SqlCommand cm = cn.CreateCommand() )
                {
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.CommandText = "GetManeuvers";
                    cm.Parameters.AddWithValue( "@WeaponId", criteria.WeaponId );
                    using ( SafeDataReader dr = new SafeDataReader( cm.ExecuteReader() ) )
                    {
                        IsReadOnly = false;
                        while ( dr.Read() )
                        {
                            Maneuver maneuver = new Maneuver(
                                dr.GetInt32( "Id" ),
                                dr.GetInt32( "Score" ),
                                dr.GetInt32( "Endurance" ),
                                dr.GetInt32( "ItemId" ),
                                dr.GetString( "Name" ),
                                dr.GetString( "Item" ),
                                Maneuver.ItemType.Weapon );

                            this.Add( maneuver );
                        }
                        IsReadOnly = true;
                    }
                }
            }
            this.RaiseListChangedEvents = true;
        }

        private void Fetch( ArmorCriteria criteria )
        {
            this.RaiseListChangedEvents = false;
            using ( SqlConnection cn = new SqlConnection( Database.ImperatorConnection ) )
            {
                cn.Open();
                using ( SqlCommand cm = cn.CreateCommand() )
                {
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.CommandText = "GetManeuvers";
                    cm.Parameters.AddWithValue( "@ArmorId", criteria.ArmorId );
                    using ( SafeDataReader dr = new SafeDataReader( cm.ExecuteReader() ) )
                    {
                        IsReadOnly = false;
                        while ( dr.Read() )
                        {
                            Maneuver maneuver = new Maneuver(
                                dr.GetInt32( "Id" ),
                                dr.GetInt32( "Score" ),
                                dr.GetInt32( "Endurance" ),
                                dr.GetInt32( "ItemId" ),
                                dr.GetString( "Name" ),
                                dr.GetString( "Item" ),
                                Maneuver.ItemType.Armor );

                            this.Add( maneuver );
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
