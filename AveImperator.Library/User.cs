using System;
using System.Data;
using System.Data.SqlClient;
using Csla;
using Csla.Data;
using AveImperator.Library.Security;

namespace AveImperator.Library
{
    [Serializable()]
    public class User : BusinessBase<User>
    {
        #region Business Methods

        private int _id;
        private int _gladiatorId;
        private long _facebookId;

        private string _facebookName;
        private string _gladiator;

        [System.ComponentModel.DataObjectField( true, true )]
        public int Id
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _id;
            }
        }

        public int GladiatorId
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _gladiatorId;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( _gladiatorId != value )
                {
                    _gladiatorId = value;
                    PropertyHasChanged();
                }
            }
        }

        public long FacebookId
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _facebookId;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( _facebookId != value )
                {
                    _facebookId = value;
                    PropertyHasChanged();
                }
            }
        }

        public string FacebookName
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _facebookName;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( value == null ) value = string.Empty;
                if ( _facebookName != value )
                {
                    _facebookName = value;
                    PropertyHasChanged();
                }
            }
        }

        public string Gladiator
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _gladiator;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( value == null ) value = string.Empty;
                if ( _gladiator != value )
                {
                    _gladiator = value;
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
        private User()
        { /* require use of factory methods */ }

        internal User( int id, long facebookId, int gladiatorId, string facebookName, string gladiator )
        {
            _id = id;
            _gladiatorId = gladiatorId;

            _facebookId = facebookId;

            _facebookName = facebookName;
            _gladiator = gladiator;
        }

        #endregion
    }
}
