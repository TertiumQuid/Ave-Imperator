<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Challenges.aspx.cs" Inherits="Challenges" Title="Untitled Page" %>
<%@ MasterType virtualpath="~/MasterPage.master" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="MainContent" Runat="Server">
    <h1 class="title"><asp:Label ID="PageTitleLabel" runat="server" /></h1>
    <table style="color:rgb(255,255,255);font-size:115%;border:1px solid rgb(0,0,0);" cellpadding="4" cellspacing="1">
        <colgroup>
            <col style="width:240px;background-color:rgb(30,30,30);" />
            <col style="background-color:rgb(180,0,0);width:144px;text-align:center;" />
            <col style="width:240px;background-color:rgb(30,30,30);" />
        </colgroup>
        <tr>
            <td style="font-weight:bold;font-size:14px;text-align:right;"><asp:Label ID="ChallengerName" runat="server" /></td>
            <th style="font-size:14px;text-align:center;">Ordinarii</th>
            <td style="font-weight:bold;font-size:14px;"><asp:Label ID="ChallengedName" runat="server" /></td>
        </tr>
        <tr>
            <td style="text-align:right;color:rgb(155,155,155);"><asp:Label ID="ChallengerClass" runat="server" /></td>
            <th style="color:rgb(255,255,255);text-align:center;">Fighting Class</th>
            <td style="color:rgb(155,155,155);"><asp:Label ID="ChallengedClass" runat="server" /></td>
        </tr>
        <tr>
            <td style="text-align:right;color:rgb(155,155,155);"><asp:Label ID="ChallengerRace" runat="server" /></td>
            <th style="color:rgb(255,255,255);text-align:center;">Race</th>
            <td style="color:rgb(155,155,155);"><asp:Label ID="ChallengedRace" runat="server" /></td>
        </tr>
        <tr>
            <td style="text-align:right;color:rgb(155,155,155);"><asp:Label ID="ChallengerConstitution" runat="server" /></td>
            <th style="color:rgb(255,255,255);text-align:center;">Constitution</th>
            <td style="color:rgb(155,155,155);"><asp:Label ID="ChallengedConstitution" runat="server" /></td>
        </tr>
        <tr>
            <td style="text-align:right;color:rgb(155,155,155);"><asp:Label ID="ChallengerCunning" runat="server" /></td>
            <th style="color:rgb(255,255,255);text-align:center;">Cunning</th>
            <td style="color:rgb(155,155,155);"><asp:Label ID="ChallengedCunning" runat="server" /></td>
        </tr>
        <tr>
            <td style="text-align:right;color:rgb(155,155,155);"><asp:Label ID="ChallengerEndurance" runat="server" /></td>
            <th style="color:rgb(255,255,255);text-align:center;">Endurance</th>
            <td style="color:rgb(155,155,155);"><asp:Label ID="ChallengedEndurance" runat="server" /></td>
        </tr>
        <tr>
            <td style="text-align:right;color:rgb(155,155,155);"><asp:Label ID="ChallengerStrength" runat="server" /></td>
            <th style="color:rgb(255,255,255);text-align:center;">Strength</th>
            <td style="color:rgb(155,155,155);"><asp:Label ID="ChallengedStrength" runat="server" /></td>
        </tr>
        <tr>
            <td style="text-align:right;color:rgb(155,155,155);"><asp:Label ID="ChallengerWeapons" runat="server" /></td>
            <th style="color:rgb(255,255,255);text-align:center;">Weapons</th>
            <td style="color:rgb(155,155,155);"><asp:Label ID="ChallengedWeapons" runat="server" /></td>
        </tr>
        <tr>
            <td style="text-align:right;color:rgb(155,155,155);"><asp:Label ID="ChallengerArmor" runat="server" /></td>
            <th style="color:rgb(255,255,255);text-align:center;">Armor</th>
            <td style="color:rgb(155,155,155);"><asp:Label ID="ChallengedArmor" runat="server" /></td>
        </tr>
        <tr>
            <td style="text-align:right;color:rgb(155,155,155);"><asp:Image ID="ChallengerAvatar" runat="server" style="border: 2px solid rgb(155,155,155);" /></td>
            <th style="color:rgb(255,255,255);">&nbsp;</th>
            <td style="color:rgb(155,155,155);"><asp:Image ID="ChallengedAvatar" runat="server" style="border: 2px solid rgb(155,155,155);" /></td>
        </tr>
        <tr>
            <td style="text-align:right;color:rgb(155,155,155);"><asp:Label ID="ChallengerUser" runat="server" /></td>
            <th style="color:rgb(255,255,255);text-align:center;">Patron</th>
            <td style="color:rgb(155,155,155);"><asp:Label ID="ChallengedUser" runat="server" /></td>
        </tr>
    </table>
    <asp:MultiView ID="ButtonsView" runat="server">
        <asp:View ID="ChallengingView" runat="server">
            <p>
                Choose where you will fight your battle: <asp:DropDownList ID="Cities" runat="server" DataSourceID="CityListDataSource" DataValueField="Id" DataTextField="Name" style="width:125px;" />
            </p>
            <p>
                Entering stinging opening words to shame your foe: <asp:TextBox ID="OpeningWords" runat="server" style="width:300px;" MaxLength="128" />&nbsp;(optional)
            </p>
            <br />
            <asp:LinkButton ID="SubmitChallenge" runat="server" Text="Issue Challenge" CssClass="inputsubmit" />
            <fb:FacebookHyperLink ID="Cancel" runat="server" NavigateUrl="http://apps.facebook.com/aveimperator/Arena.aspx" Text="Find Another Challenge" CssClass="inputsubmit" />
        </asp:View>
        <asp:View ID="ChallengerView" runat="server">
            <br />
            <asp:LinkButton ID="RetractChallenge" runat="server" Text="Retract Challenge" CssClass="inputsubmit" />
        </asp:View>
        <asp:View ID="ChallengedView" runat="server">
            <br />
            <asp:LinkButton ID="AcceptChallenge" runat="server" Text="Accept Challenge" CssClass="inputsubmit" />
            <asp:LinkButton ID="RejectChallenge" runat="server" Text="Reject Challenge" CssClass="inputsubmit" />
        </asp:View>
    </asp:MultiView>
    
    <csla:CslaDataSource ID="CityListDataSource" runat="server" 
        TypeName="AveImperator.Library.CityList" 
        TypeAssemblyName="AveImperator.Library"
        OnSelectObject="CityListDataSource_SelectObject" />
</asp:Content>

