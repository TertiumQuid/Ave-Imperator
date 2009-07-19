using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Facebook.Service;
using Csla;
using Facebook.Web;
using AveImperator.Library;
using AveImperator.Library.Security;

public partial class Record : System.Web.UI.Page
{
    protected void Page_Load( object sender, EventArgs e )
    {

    }

    #region BattleListDataSource
    protected void BattleListDataSource_SelectObject( object sender, Csla.Web.SelectObjectArgs e )
    {
        e.BusinessObject = GetBattleList();
    }

    private Csla.SortedBindingList<AveImperator.Library.Battle> GetBattleList()
    {
        object businessObject = Session["CurrentObject"];
        if ( businessObject == null || !( businessObject is BattleList ) )
        {
            businessObject = BattleList.GetBattleList( Master.Identity.GladiatorId );
            Session["CurrentObject"] = businessObject;
        }

        Csla.SortedBindingList<AveImperator.Library.Battle> list = new Csla.SortedBindingList<AveImperator.Library.Battle>( (BattleList)businessObject );
        list.ApplySort( Master.SortExpression, Master.SortDirection );
        return list;
    }
    #endregion
}
