using System;
using System.Data;
using System.Data.SqlClient;
using Csla;
using Csla.Data;
using AveImperator.Library.Security;

namespace AveImperator.Library
{
    [Serializable()]
    public class Gladiator : BusinessBase<Gladiator>
    {
        #region Business Methods

        private int _id;
        private int _userId;

        private int _raceId;
        private int _godId;
        private int _gladiatorClassId;

        private int _constitution;
        private int _cunning;
        private int _endurance;
        private int _strength;

        private int _victories;
        private int _defeats;
        private int _draws;
        private int _fame;

        private string _name;
        private string _user;

        private string _race;
        private string _god;
        private string _gladiatorClass;

        private WeaponList _weapons;
        private ArmorList _armor;

        private Diet _diet;

        [System.ComponentModel.DataObjectField( true, true )]
        public int Id
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _id;
            }
        }

        public int UserId
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _userId;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( _userId != value )
                {
                    _userId = value;
                    PropertyHasChanged();
                }
            }
        }


        public int RaceId
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _raceId;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( _raceId != value )
                {
                    _raceId = value;
                    PropertyHasChanged();
                }
            }
        }

        public int GodId
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _godId;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( _godId != value )
                {
                    _godId = value;
                    PropertyHasChanged();
                }
            }
        }

        public int GladiatorClassId
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _gladiatorClassId;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( _gladiatorClassId != value )
                {
                    _gladiatorClassId = value;
                    PropertyHasChanged();
                }
            }
        }

        public int Constitution
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _constitution;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( _constitution != value )
                {
                    _constitution = value;
                    PropertyHasChanged();
                }
            }
        }

        public int Cunning
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _cunning;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( _cunning != value )
                {
                    _cunning = value;
                    PropertyHasChanged();
                }
            }
        }

        public int Endurance
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _endurance;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( _endurance != value )
                {
                    _endurance = value;
                    PropertyHasChanged();
                }
            }
        }

        public int Strength
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _strength;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( _strength != value )
                {
                    _strength = value;
                    PropertyHasChanged();
                }
            }
        }

        public int Victories
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _victories;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( _victories != value )
                {
                    _victories = value;
                    PropertyHasChanged();
                }
            }
        }

        public int Defeats
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _defeats;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( _defeats != value )
                {
                    _defeats = value;
                    PropertyHasChanged();
                }
            }
        }

        public int Draws
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _draws;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( _draws != value )
                {
                    _draws = value;
                    PropertyHasChanged();
                }
            }
        }

        public int Fame
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _fame;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( _fame != value )
                {
                    _fame = value;
                    PropertyHasChanged();
                }
            }
        }

        public int Battles
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _victories + _defeats + _draws;
            }
        }

        public int Health
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                // Health is a derived stat
                return ( _constitution * 4 ) + _endurance;
            }
        }

        public int Stamina
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                // Stamina is a derived stat
                return ( _endurance * 5 ) + ( _constitution * _diet.Score );
            }
        }

        public string Name
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _name;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( value == null ) value = string.Empty;
                if ( _name != value )
                {
                    _name = value;
                    PropertyHasChanged();
                }
            }
        }

        public string User
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _user;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( value == null ) value = string.Empty;
                if ( _user != value )
                {
                    _user = value;
                    PropertyHasChanged();
                }
            }
        }

        public string Race
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _race;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( value == null ) value = string.Empty;
                if ( _race != value )
                {
                    _race = value;
                    PropertyHasChanged();
                }
            }
        }

        public string God
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _god;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( value == null ) value = string.Empty;
                if ( _god != value )
                {
                    _god = value;
                    PropertyHasChanged();
                }
            }
        }

        public string GladiatorClass
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _gladiatorClass;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( value == null ) value = string.Empty;
                if ( _gladiatorClass != value )
                {
                    _gladiatorClass = value;
                    PropertyHasChanged();
                }
            }
        }

        public int DietId
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _diet != null ? _diet.Id : 0;
            }
        }


        public string Diet
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _diet != null ? _diet.Description : string.Empty;
            }
        }

        public WeaponList Weapons
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                if ( _weapons == null ) _weapons = WeaponList.GetGladiatorWeapons( Id );
                return _weapons;
            }
        }

        public string WeaponSummary
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                string weapons = string.Empty;
                foreach ( Weapon weapon in Weapons )
                {
                    weapons += weapon.ItemCondition + " " + weapon.Name + ", ";
                }
                if ( weapons.EndsWith( ", " ) ) weapons = weapons.Substring( 0, weapons.Length - 2 ) + ".";
                return weapons;
            }
        }

        public ArmorList Armor
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                if ( _armor == null ) _armor = ArmorList.GetGladiatorArmor( Id );
                return _armor;
            }
        }

        public string ArmorSummary
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                string armors = string.Empty;
                foreach ( Armor armor in this.Armor )
                {
                    armors += armor.ItemCondition + " " + armor.Name + ", ";
                }
                if ( armors.EndsWith( ", " ) ) armors = armors.Substring( 0, armors.Length - 2 ) + ".";
                return armors;
            }
        }

        public int ActionsPerTactic
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return (int)Math.Floor( _cunning * 2.5 );
            }
        }

        public bool Exists
        {
            get { return _id > 0; }
        }

        public override bool IsValid
        {
            get { return base.IsValid; }
        }

        public override bool IsDirty
        {
            get { return base.IsDirty; }
        }

        protected override object GetIdValue()
        {
            return _id;
        }

        #endregion

        #region Factory Methods
        public static Gladiator NewGladiator()
        {
            return DataPortal.Create<Gladiator>();
        }

        public static Gladiator GetGladiator( int id )
        {
            return DataPortal.Fetch<Gladiator>( new Criteria( id ) );
        }

        public static Gladiator GetUserGladiator( long id )
        {
            return DataPortal.Fetch<Gladiator>( new FacebookUserCriteria( id ) );
        }

        public override Gladiator Save()
        {
            return base.Save();
        }

        private Gladiator()
        { /* require use of factory methods */ }

        internal Gladiator( int id, int userId, int raceId, int godId, int gladiatorClassId, int dietId, int constitution, int cunning, int endurance, int strength, int victories, int defeats, int draws, int fame, string name, string user, string race, string god, string gladiatorClass )
        {
            _id = id;
            _userId = userId;

            _raceId = raceId;
            _godId = godId;
            _gladiatorClassId = gladiatorClassId;

            _constitution = constitution;
            _cunning = cunning;
            _endurance = endurance;
            _strength = strength;

            _victories = victories;
            _defeats = defeats;
            _draws = draws;
            _fame = fame;

            _name = name;
            _user = user;
            _race = race;
            _god = god;
            _gladiatorClass = gladiatorClass;

            _diet = AveImperator.Library.Diet.GetDiet( dietId );
        }

        #endregion

        #region Data Access

        [Serializable()]
        private class Criteria
        {
            private int _id;
            public int Id
            {
                get { return _id; }
            }

            public Criteria( int id )
            {
                _id = id;
            }
        }

        [Serializable()]
        private class FacebookUserCriteria
        {
            private long _facebookId;
            public long FacebookId
            {
                get { return _facebookId; }
            }

            public FacebookUserCriteria( long facebookId )
            {
                _facebookId = facebookId;
            }
        }


        [RunLocal()]
        protected override void DataPortal_Create()
        {
            ValidationRules.CheckRules();
        }

        private void DataPortal_Fetch( Criteria criteria )
        {
            using ( SqlConnection cn = new SqlConnection( Database.ImperatorConnection ) )
            {
                cn.Open();
                using ( SqlCommand cm = cn.CreateCommand() )
                {
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.CommandText = "GetGladiator";
                    cm.Parameters.AddWithValue( "@Id", criteria.Id );

                    using ( SafeDataReader dr = new SafeDataReader( cm.ExecuteReader() ) )
                    {
                        if ( dr.Read() )
                        {
                            _id = dr.GetInt32( "Id" );
                            _userId = dr.GetInt32( "UserId" );

                            _raceId = dr.GetInt32( "RaceId" );
                            _godId = dr.GetInt32( "GodId" );
                            _gladiatorClassId = dr.GetInt32( "GladiatorClassId" );

                            _constitution = dr.GetInt32( "Constitution" );
                            _cunning = dr.GetInt32( "Cunning" );
                            _endurance = dr.GetInt32( "Endurance" );
                            _strength = dr.GetInt32( "Strength" );

                            _victories = dr.GetInt32( "Victories" );
                            _defeats = dr.GetInt32( "Defeats" );
                            _draws = dr.GetInt32( "Draws" );
                            _fame = dr.GetInt32( "Fame" );

                            _name = dr.GetString( "Name" );
                            _user = dr.GetString( "User" );

                            _race = dr.GetString( "Race" );
                            _god = dr.GetString( "God" );
                            _gladiatorClass = dr.GetString( "GladiatorClass" );

                            _diet = AveImperator.Library.Diet.GetDiet( dr.GetInt32( "DietId" ) );
                        }
                    }
                }
            }
        }

        private void DataPortal_Fetch( FacebookUserCriteria criteria )
        {
            using ( SqlConnection cn = new SqlConnection( Database.ImperatorConnection ) )
            {
                cn.Open();
                using ( SqlCommand cm = cn.CreateCommand() )
                {
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.CommandText = "GetGladiator";
                    cm.Parameters.AddWithValue( "@FacebookId", criteria.FacebookId );

                    using ( SafeDataReader dr = new SafeDataReader( cm.ExecuteReader() ) )
                    {
                        if ( dr.Read() )
                        {
                            _id = dr.GetInt32( "Id" );
                            _userId = dr.GetInt32( "UserId" );

                            _raceId = dr.GetInt32( "RaceId" );
                            _godId = dr.GetInt32( "GodId" );
                            _gladiatorClassId = dr.GetInt32( "GladiatorClassId" );

                            _constitution = dr.GetInt32( "Constitution" );
                            _cunning = dr.GetInt32( "Cunning" );
                            _endurance = dr.GetInt32( "Endurance" );
                            _strength = dr.GetInt32( "Strength" );

                            _victories = dr.GetInt32( "Victories" );
                            _defeats = dr.GetInt32( "Defeats" );
                            _draws = dr.GetInt32( "Draws" );
                            _fame = dr.GetInt32( "Fame" );

                            _name = dr.GetString( "Name" );
                            _user = dr.GetString( "User" );

                            _race = dr.GetString( "Race" );
                            _god = dr.GetString( "God" );
                            _gladiatorClass = dr.GetString( "GladiatorClass" );

                            _diet = AveImperator.Library.Diet.GetDiet( dr.GetInt32( "DietId" ) );
                        }
                    }
                }
            }
        }


        [Transactional( TransactionalTypes.TransactionScope )]
        protected override void DataPortal_Insert()
        {
            using ( SqlConnection cn = new SqlConnection( Database.ImperatorConnection ) )
            {
                cn.Open();
                ApplicationContext.LocalContext["cn"] = cn;
                using ( SqlCommand cm = cn.CreateCommand() )
                {
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.CommandText = "AddGladiator";
                    cm.Parameters.AddWithValue( "@UserID", _userId );
                    cm.Parameters.AddWithValue( "@RaceId", _raceId );
                    cm.Parameters.AddWithValue( "@GodId", _godId );
                    cm.Parameters.AddWithValue( "@GladiatorClassId", _gladiatorClassId );
                    cm.Parameters.AddWithValue( "@Name", _name );
                    cm.Parameters.AddWithValue( "@Constitution", _constitution );
                    cm.Parameters.AddWithValue( "@Cunning", _cunning );
                    cm.Parameters.AddWithValue( "@Endurance", _endurance );
                    cm.Parameters.AddWithValue( "@Strength", _strength );
                    SqlParameter param = new SqlParameter( "@NewId", SqlDbType.Int );
                    param.Direction = ParameterDirection.Output;
                    cm.Parameters.Add( param );

                    cm.ExecuteNonQuery();

                    _id = (int)cm.Parameters["@NewId"].Value;
                }

                // removing of item only needed for local data portal
                if ( ApplicationContext.ExecutionLocation == ApplicationContext.ExecutionLocations.Client )
                    ApplicationContext.LocalContext.Remove( "cn" );
            }
        }

        [Transactional( TransactionalTypes.TransactionScope )]
        protected override void DataPortal_Update()
        {
            using ( SqlConnection cn = new SqlConnection( Database.ImperatorConnection ) )
            {
                cn.Open();
                ApplicationContext.LocalContext["cn"] = cn;
                if ( base.IsDirty )
                {
                    using ( SqlCommand cm = cn.CreateCommand() )
                    {
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.CommandText = "UpdateGladiator";
                        cm.Parameters.AddWithValue( "@Id", _id );
                        cm.Parameters.AddWithValue( "@Victories", _victories );
                        cm.Parameters.AddWithValue( "@Defeats", _defeats );
                        cm.Parameters.AddWithValue( "@Draws", _draws );
                        cm.Parameters.AddWithValue( "@Fame", _fame );

                        cm.ExecuteNonQuery();
                    }
                }

                // removing of item only needed for local data portal
                if ( ApplicationContext.ExecutionLocation == ApplicationContext.ExecutionLocations.Client )
                    ApplicationContext.LocalContext.Remove( "cn" );
            }
        }
        #endregion
    }
}
