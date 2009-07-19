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
    public class ActionList : ReadOnlyListBase<ActionList, Action>
    {
        #region Factory Methods

        public static ActionList NewActionList()
        {
            return DataPortal.Create<ActionList>();
        }

        /// <summary>
        /// Return a list of all actions assigned to a given tactic.
        /// </summary>
        public static ActionList GetActionList( int tacticId )
        {
            return DataPortal.Fetch<ActionList>( new Criteria( tacticId ) );
        }

        private ActionList()
        { /* require use of factory methods */ }

        #endregion

        #region Data Access
        [Serializable()]
        private class Criteria
        {
            private int _tacticId;
            public int TacticId
            {
                get { return _tacticId; }
            }

            public Criteria( int tacticId )
            {
                _tacticId = tacticId;
            }
        }

        [RunLocal()]
        private void DataPortal_Create()
        {
            IsReadOnly = false;
        }

        private void DataPortal_Fetch( Criteria criteria )
        {
            Fetch( criteria );
        }

        private void Fetch( Criteria criteria )
        {
            this.RaiseListChangedEvents = false;
            using ( SqlConnection cn = new SqlConnection( Database.ImperatorConnection ) )
            {
                cn.Open();
                using ( SqlCommand cm = cn.CreateCommand() )
                {
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.CommandText = "GetActions";
                    cm.Parameters.AddWithValue( "@TacticId", criteria.TacticId );
                    using ( SafeDataReader dr = new SafeDataReader( cm.ExecuteReader() ) )
                    {
                        IsReadOnly = false;
                        while ( dr.Read() )
                        {
                            Action action = new Action(
                                dr.GetInt32( "Id" ),
                                dr.GetInt32( "TacticId" ),
                                dr.GetInt32( "ManeuverId" ),
                                dr.GetInt32( "Ordinal" ),
                                dr.GetInt32( "Score" ),
                                dr.GetInt32( "ItemId" ),
                                dr.GetString( "Maneuver" ),
                                dr.GetString( "Item" ),
                                (Maneuver.ItemType)dr.GetInt32( "ItemType" ) );

                            this.Add( action );
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
