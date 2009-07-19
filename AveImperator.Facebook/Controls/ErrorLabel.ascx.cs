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

public partial class Controls_ErrorLabel : System.Web.UI.UserControl
{
    protected void Page_Load( object sender, EventArgs e )
    {
    }

    public string Title
    {
        get { return TitleLabel.Text; }
        set { TitleLabel.Text = value; }
    }

    public string Message
    {
        get { return MessageLabel.Text; }
        set { MessageLabel.Text = value; }
    }
}
