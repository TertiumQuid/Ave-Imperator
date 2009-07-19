using System;
using System.Data;
using System.Data.SqlClient;
using Csla;
using Csla.Data;
using AveImperator.Library.Security;
namespace AveImperator.Library
{
    [Serializable()]
    public class Action : BusinessBase<Action>
    {
        #region Business Methods

        private int _id;
        private int _tacticId;
        private int _maneuverId;
        private int _ordinal;
        private int _itemId;

        private int _score;

        private string _maneuver;
        private string _item;

        private Maneuver.ItemType _itemType;

        [System.ComponentModel.DataObjectField( true, true )]
        public int Id
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _id;
            }
        }

        public int TacticId
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _tacticId;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( _tacticId != value )
                {
                    _tacticId = value;
                    PropertyHasChanged();
                }
            }
        }

        public int ManeuverId
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _maneuverId;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( _maneuverId != value )
                {
                    _maneuverId = value;
                    PropertyHasChanged();
                }
            }
        }

        public int Ordinal
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _ordinal;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( _ordinal != value )
                {
                    _ordinal = value;
                    PropertyHasChanged();
                }
            }
        }

        public int Score
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _score;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( _score != value )
                {
                    _score = value;
                    PropertyHasChanged();
                }
            }
        }

        public int ItemId
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _itemId;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( _itemId != value )
                {
                    _itemId = value;
                    PropertyHasChanged();
                }
            }
        }

        public string Maneuver
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _maneuver;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( value == null ) value = string.Empty;
                if ( _maneuver != value )
                {
                    _maneuver = value;
                    PropertyHasChanged();
                }
            }
        }

        public string Item
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _item;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( value == null ) value = string.Empty;
                if ( _item != value )
                {
                    _item = value;
                    PropertyHasChanged();
                }
            }
        }

        public Maneuver.ItemType ItemType
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _itemType;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( _itemType != value )
                {
                    _itemType = value;
                    PropertyHasChanged();
                }
            }
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
        public static Action NewAction()
        {
            return DataPortal.Create<Action>();
        }

        public override Action Save()
        {
            return base.Save();
        }

        private Action()
        { /* require use of factory methods */ }

        internal Action( int id, int tacticId, int maneuverId, int ordinal, int score, int itemId, string maneuver, string item, Maneuver.ItemType itemType )
        {
            _id = id;
            _tacticId = tacticId;
            _maneuverId = maneuverId;
            _ordinal = ordinal;
            _score = score;
            _itemId = itemId;

            _maneuver = maneuver;
            _item = item;

            _itemType = itemType;
        }

        #endregion

        #region Data Access

        [RunLocal()]
        protected override void DataPortal_Create()
        {
            ValidationRules.CheckRules();
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
                    cm.CommandText = "AddAction";
                    cm.Parameters.AddWithValue( "@TacticId", _tacticId );
                    cm.Parameters.AddWithValue( "@ManeuverId", _maneuverId );
                    cm.Parameters.AddWithValue( "@Ordinal", _ordinal );
                    cm.Parameters.AddWithValue( "@ItemId", _itemId );
                    cm.Parameters.AddWithValue( "@ItemType", (int)_itemType );
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

        #endregion
    }
}
