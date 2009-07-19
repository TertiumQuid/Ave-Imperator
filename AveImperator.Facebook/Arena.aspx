<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Arena.aspx.cs" Inherits="Arena" Title="Untitled Page" %>
<%@ MasterType virtualpath="~/MasterPage.master" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="MainContent" Runat="Server">
    <style type="text/css">
        .pagebutton { width:75px;color:rgb(60,70,150);background-color:rgb(255,255,255);display:inline-block;padding:2px 4px 2px 4px;margin-top:6px;font-weight:bold; }
        .pagebutton:hover { width:75px;color:rgb(255,255,255);background-color:rgb(60,70,150);;text-decoration:none; }
    </style>
    <h1 class="title">The Arena</h1>
    <div class="contentbox" style="width:625px;">
        <div class="contentboxtitle" style="width:621px;">Gladiators</div>
        <br />
        <asp:UpdatePanel ID="ChallengersUpdatePanel" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:GridView ID="Gladiators" runat="server" 
                    DataSourceID="GladiatorListDataSource" 
                    DataKeyNames="ID"   
                    AllowSorting="true"
                    AutoGenerateColumns="false" 
                    EmptyDataText="No more gladiators are available for you to challenge."
                    AllowPaging="true" 
                    PageSize="25" 
                    PagerSettings-Mode="NumericFirstLast" 
                    PagerSettings-PageButtonCount="25" 
                    CssClass="grid"
                    GridLines="none" Width="624" 
                    CellPadding="0" 
                    CellSpacing="0"> 
                    <Columns>
                        <asp:ButtonField ButtonType="link" CommandName="ViewProfile" DataTextField="Name" />
                        <asp:TemplateField HeaderText="Name" SortExpression="Name">
                            <ItemTemplate> 
                                <fb:FacebookHyperLink ID="ProfileFBLink" runat="server" Text='<%# Eval( "Name" ) %>' NavigateUrl="ViewGladiator.aspx" />
                                <asp:LinkButton ID="ProfileLink" runat="server" OnCommand="Gladiators_LinkClick" CommandName="ViewProfile" CommandArgument='<%# Eval( "Id" ) %>' Text='<%# Eval( "Name" ) %>' /> 
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Race" HeaderText="Race" SortExpression="Race" />
                        <asp:BoundField DataField="GladiatorClass" HeaderText="Fighting Class" SortExpression="GladiatorClass" />
                        <asp:BoundField DataField="God" HeaderText="Religion" SortExpression="God" />
                        <asp:BoundField DataField="User" HeaderText="Patron" SortExpression="User" />
                        <asp:TemplateField HeaderText="" ItemStyle-Width="70">
                            <ItemTemplate>
                                <asp:LinkButton ID="ChallengeLink" runat="server" OnCommand="Gladiators_LinkClick" CommandName="Challenge" CommandArgument='<%# Eval( "Id" ) %>' CssClass="microbutton" style="background:url(Images/Buttons/challenge.gif);background-repeat: no-repeat;" /> 
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <br />
    <div class="contentbox" style="width:625px;">
        <div class="contentboxtitle" style="width:621px;">&quot;Friends&quot;</div>
        <asp:Panel ID="ChallengeFriendsPanel" runat="server" style="padding:6px;">
            <h2><asp:Label ID="CowardsLabel" runat="server" /><asp:LinkButton ID="ChallengeFriends" runat="server" Text="Make Them Bow Before You..." /></h2>
        </asp:Panel>
        <hr style="background-color:rgb(175,175,175);width:100%;" />
        <asp:UpdatePanel ID="FriendsUpdatePanel" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div style="padding:6px;">
                    <asp:DataList ID="Friends" runat="server" 
                        DataSourceID="FriendListDataSource" 
                        DataKeyField="uid" 
                        Width="615" 
                        RepeatColumns="4" 
                        RepeatDirection="Horizontal"  
                        GridLines="none"
                        RepeatLayout="Table" >
                        <ItemTemplate>
                            <asp:HyperLink ID="ChallengeFriend" runat="server" style="display:block" CssClass="fadeelement" target="_parent" NavigateUrl='<%# "http://apps.facebook.com/aveimperator/challenges.aspx?userId=" + Eval( "uid") %>'>
                                <div style='<%# "padding:3px;" + ((bool)Eval( "has_added_app" ) ? "filter: alpha(opacity=100);opacity:1;" : "filter: alpha(opacity=55);opacity:0.55;") %>'>
                                    <%# Eval( "name" ) %><br />
                                    <asp:Image ID="FacebookAvatar" runat="server" ImageUrl='<%# Eval( "pic_square" ) %>' />
                                </div>
                                <div style="margin: 4px 0px 2px 2px">
                                    <span class="microbutton" style="background:url(Images/Buttons/challenge.gif);background-repeat: no-repeat;">&nbsp;</span>
                                </div>
                            </asp:HyperLink>
                        </ItemTemplate>
                    </asp:DataList>
                    <br />
                    <asp:LinkButton ID="PreviousLink" runat="server" Text="Previous" class="pagebutton" CommandArgument="-1" style="margin-right:8px;" />
                    <asp:LinkButton ID="NextLink" runat="server" Text="Next" class="pagebutton" CommandArgument="1" />  
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <csla:CslaDataSource ID="GladiatorListDataSource" runat="server" 
        TypeName="AveImperator.Library.GladiatorList" 
        TypeAssemblyName="AveImperator.Library"
        OnSelectObject="GladiatorListDataSource_SelectObject" /> 
    
    <fb:FqlDataSource runat="server" ID="FriendListDataSource"
      FqlQuery="SELECT name, uid, pic_square, has_added_app from user WHERE uid IN (SELECT uid2 FROM friend WHERE uid1=@UserID) ORDER BY uid LIMIT 8 OFFSET 0" />
</asp:Content>

