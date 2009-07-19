using System;
using System.Data;
using System.Data.SqlClient;
using Csla;
using Csla.Data;
using AveImperator.Library.Security;

namespace AveImperator.Library
{
    [Serializable()]
    public class Maneuver : BusinessBase<Maneuver>
    {
        #region Business Methods

        private int _id;
        private int _score;
        private int _itemId;

        private int _endurance;

        private string _name;
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

        public ItemType ManeuverItemType
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

        public ActionType ManeuverActionType
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return (ActionType)( _score % 2 );
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
        private Maneuver()
        { /* require use of factory methods */ }

        internal Maneuver( int id, int score, string name )
        {
            _id = id;
            _score = score;
            _name = name;
            _endurance = 0;
        }

        internal Maneuver( int id, int score, int endurance, int itemId, string name, string item, ItemType itemType)
        {
            _id = id;
            _score = score;
            _itemId = itemId;

            _name = name;
            _item = item;

            _endurance = endurance;

            _itemType = itemType;
        }
        #endregion

        public enum ActionType
        {
            Offense = 0,
            Defense = 1
        }

        public enum ItemType
        {
            Unknown = 0,
            Weapon = 1,
            Armor = 2
        }
    }
}
