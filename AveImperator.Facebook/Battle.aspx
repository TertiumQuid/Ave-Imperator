<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Battle.aspx.cs" Inherits="Battle" Title="Untitled Page" %>
<%@ MasterType virtualpath="~/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <h1 class="title"><asp:Label ID="BattleTitle" runat="server" /></h1>
    <h2 style="padding:5px;font-size:16px;color:rgb(255,215,0);background-color:rgb(0,0,0);width:620px;margin:5px 0 8px 0;"><asp:Label ID="VictorLabel" runat="server" /></h2>
    <div class="contentbox" style="width:625px;">
        <div class="contentboxtitle" style="width:621px;">Here's What Happened...</div>
        <p><asp:Label ID="BattleDescription" runat="server" /></p>
    </div>
</asp:Content>

