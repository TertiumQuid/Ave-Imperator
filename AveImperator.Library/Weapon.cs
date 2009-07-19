using System;
using System.Data;
using System.Data.SqlClient;
using Csla;
using Csla.Data;
using AveImperator.Library.Security;

namespace AveImperator.Library
{
    [Serializable()]
    public class Weapon : BusinessBase<Weapon>
    {
        #region Business Methods

        private int _id;
        private int _damage;
        private int _itemConditionId;

        private string _name;
        private string _description;
        private string _itemCondition;

        private ManeuverList _maneuvers;

        [System.ComponentModel.DataObjectField( true, true )]
        public int Id
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _id;
            }
        }

        public int Damage
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _damage;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( _damage != value )
                {
                    _damage = value;
                    PropertyHasChanged();
                }
            }
        }

        public int ItemConditionId
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _itemConditionId;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( _itemConditionId != value )
                {
                    _itemConditionId = value;
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

        public string Description
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _description;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( value == null ) value = string.Empty;
                if ( _description != value )
                {
                    _description = value;
                    PropertyHasChanged();
                }
            }
        }

        public string ItemCondition
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _itemCondition;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( value == null ) value = string.Empty;
                if ( _itemCondition != value )
                {
                    _itemCondition = value;
                    PropertyHasChanged();
                }
            }
        }

        public ManeuverList Maneuvers
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                if ( _maneuvers == null )
                {
                    _maneuvers = ManeuverList.GetWeaponManeuverList( _id ); 
                }

                return _maneuvers;
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
        private Weapon()
        { /* require use of factory methods */ }

        internal Weapon( int id, int damage, int itemConditionId, string name, string description, string itemCondition )
        {
            _id = id;
            _damage = damage;
            _itemConditionId= itemConditionId;

            _name = name;
            _description = description;
            _itemCondition = itemCondition;
        }

        #endregion
    }
}
