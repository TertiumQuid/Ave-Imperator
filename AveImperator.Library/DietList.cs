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
    public class DietList : ReadOnlyListBase<DietList, Diet>
    {
        #region Factory Methods

        private static DietList _list;

        /// <summary>
        /// Return a list of all diets.
        /// </summary>
        public static DietList GetDietList()
        {
            if ( _list == null ) _list = DataPortal.Fetch<DietList>();

            return _list;
        }

        /// <summary>
        /// Clears the static DietList that's been cached; rarely if ever will this need to happen.
        /// </summary>
        public static void InvalidateCache()
        {
            _list = null;
        }

        private DietList()
        { /* require use of factory methods */ }

        #endregion

        #region Data Access
        private void DataPortal_Fetch()
        {
            Fetch();
        }

        private void Fetch()
        {
            this.RaiseListChangedEvents = false;
            using ( SqlConnection cn = new SqlConnection( Database.ImperatorConnection ) )
            {
                cn.Open();
                using ( SqlCommand cm = cn.CreateCommand() )
                {
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.CommandText = "GetDiets";
                    using ( SafeDataReader dr = new SafeDataReader( cm.ExecuteReader() ) )
                    {
                        IsReadOnly = false;
                        while ( dr.Read() )
                        {
                            Diet diet = new Diet(
                                dr.GetInt32( "Id" ),
                                dr.GetInt32( "Score" ),
                                dr.GetString( "Description" ) );

                            this.Add( diet );
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
