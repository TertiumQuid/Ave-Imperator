using System;
using System.Data;
using System.Data.SqlClient;
using Csla;
using Csla.Data;
using AveImperator.Library.Security;

namespace AveImperator.Library
{
    [Serializable()]
    public class Winning : BusinessBase<Winning>
    {
        #region Business Methods

        private int _id;
        private int _value;
        private int _quantity;

        private string _name;
        private string _description;
        private string _unit;

        private int _min;
        private int _max;
        private int _chance;

        [System.ComponentModel.DataObjectField( true, true )]
        public int Id
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _id;
            }
        }

        public int Value
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _value;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( _value != value )
                {
                    _value = value;
                    PropertyHasChanged();
                }
            }
        }

        public int Quantity
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _quantity;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( _quantity != value )
                {
                    _quantity = value;
                    PropertyHasChanged();
                }
            }
        }

        public int Min
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _min;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( _min != value )
                {
                    _min = value;
                    PropertyHasChanged();
                }
            }
        }


        public int Max
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _max;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( _max != value )
                {
                    _max = value;
                    PropertyHasChanged();
                }
            }
        }


        public int Chance
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _chance;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( _chance != value )
                {
                    _chance = value;
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

        public string Unit
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _unit;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( value == null ) value = string.Empty;
                if ( _unit != value )
                {
                    _unit = value;
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
        private Winning()
        { /* require use of factory methods */ }

        internal Winning( int id, int value, int quantity, string name, string description, string unit )
        {
            _id = id;
            _value = value;
            _quantity = quantity;

            _min = 0;
            _max = 0;
            _chance = 0;

            _name = name;
            _description = description;
            _unit = unit;
        }

        internal Winning( int id, int value, int min, int max, int chance, string name, string description, string unit )
        {
            _id = id;
            _value = value;
            _quantity = 0;

            _min = min;
            _max = max;
            _chance = chance;

            _name = name;
            _description = description;
            _unit = unit;
        }

        #endregion

        #region Add Gladiator Winnings
        public static void AddGladiatorWinning( int gladiatorId, int winningId, int quantity )
        {
            DataPortal.Execute<AddGladiatorWinningCommand>( new AddGladiatorWinningCommand( gladiatorId, winningId, quantity ) );
        }

        [Serializable()]
        private class AddGladiatorWinningCommand : CommandBase
        {
            private int _gladiatorId;
            private int _winningId;
            private int _quantity;

            public AddGladiatorWinningCommand( int gladiatorId, int winningId, int quantity )
            {
                _gladiatorId = gladiatorId;
                _winningId = winningId;
                _quantity = quantity;
            }

            protected override void DataPortal_Execute()
            {
                using ( SqlConnection cn = new SqlConnection( Database.ImperatorConnection ) )
                {
                    cn.Open();
                    using ( SqlCommand cm = cn.CreateCommand() )
                    {
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.CommandText = "AddGladiatorWinning";
                        cm.Parameters.AddWithValue( "@GladiatorId", _gladiatorId );
                        cm.Parameters.AddWithValue( "@WinningId", _winningId );
                        cm.Parameters.AddWithValue( "@Quantity", _quantity );
                        cm.ExecuteNonQuery();
                    }
                }
            }
        }
        #endregion
    }
}
