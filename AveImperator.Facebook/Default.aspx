<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ Import Namespace="Facebook.Service" %>

<fb:FacebookApplication ID="AveFacebookApplication" Mode="Fbml" ApplicationName="aveimperator" runat="server" EnableExternalBrowsing="true" EnableDebugging="false" />
<fb:fbml version="1.0">
    <div style="padding:5px 1px 1px 5px;">
        <img src="http://aveimperator.bellwethersystems.com/images/banner.jpg" style="border: solid 1px rgb(150,165,200);width:630px;" />
        <asp:Literal id="MasterFrame" runat="server" />
    </div>
</fb:fbml>