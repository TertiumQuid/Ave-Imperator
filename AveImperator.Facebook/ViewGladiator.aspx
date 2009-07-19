<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ViewGladiator.aspx.cs" Inherits="ViewGladiator" Title="Untitled Page" %>
<%@ MasterType virtualpath="~/MasterPage.master" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="MainContent" Runat="Server">
    <h1 class="title"><asp:Label ID="PageTitleLabel" runat="server" /></h1>
    <asp:Table ID="ProfileTable" runat="server">
        <asp:TableRow>
            <asp:TableCell>
                <asp:Image ID="GladiatorAvatar" runat="server" style="border: 1px solid rgb(80,110,170);" />
                <asp:Panel ID="SummaryPanel" runat="server" style="margin:6px 0 0 0;">
                    <h2 style="margin:0 0 5px 0;">Arena Record:</h2>
                    Battles: <asp:Label ID="BattleLabel" runat="server" /><br />
                    Victories <asp:Label ID="VictoryLabel" runat="server" /><br />
                    Defeats: <asp:Label ID="DefeatLabel" runat="server" /><br />
                    Draws: <asp:Label ID="DrawLabel" runat="server" />
                    
                    <h2 style="margin-top:10px;">Fame: <asp:Label ID="FameLabel" runat="server" /></h2>
                </asp:Panel>
            </asp:TableCell>
            <asp:TableCell>
                <div class="contentbox" style="width:490px;">
                    <div class="contentboxtitle" style="width:486px;">
                        <asp:Label ID="GladiatorNameLabel" runat="server" style="font-size:15px;" />
                    </div>
                    <div style="margin:0 0 0 2px">
                        <div style="margin:3px 0 10px 0;">
                            <h2 style="margin:12px 0 0 0;"><asp:Label ID="GladiatorSummaryLabel" runat="server" /></h2>
                            <asp:Label ID="GodLabel" runat="server" />
                        </div>
                        <div style="margin:0 0 10px 0;">
                            <asp:Label ID="ConstitutionLabel" runat="server" /><br />
                            <asp:Label ID="CunningLabel" runat="server" /><br />
                            <asp:Label ID="EnduranceLabel" runat="server" /><br />
                            <asp:Label ID="StrengthLabel" runat="server" />
                        </div>
                        <div>
                            <asp:Label ID="WeaponsLabel" runat="server" /><br />
                            <asp:Label ID="ArmorLabel" runat="server" />
                            <br /><br />
                            <asp:Label ID="DietLabel" runat="server" />
                        </div>
                    </div>
                </div>
                <br />
                <div class="contentbox" style="width:490px;">
                    <div class="contentboxtitle" style="width:486px;">
                        Unanswered Challenges
                    </div>
                    <div style="margin:6px 0 0 2px">
                        <asp:UpdatePanel ID="ChallengesUpdatePanel" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:GridView ID="Challenges" runat="server" 
                                    DataSourceID="ChallengeListDataSource" 
                                    DataKeyNames="ID"  
                                    AllowSorting="true"
                                    AutoGenerateColumns="false" 
                                    AllowPaging="true" 
                                    PageSize="25" 
                                    PagerSettings-Mode="NumericFirstLast" 
                                    PagerSettings-PageButtonCount="25" 
                                    ShowHeader="false"
                                    EmptyDataText="<p>No challenges issued. Visit the arena to challenge your rivals.</p>"
                                    CssClass="grid"
                                    GridLines="none" 
                                    Width="100%" 
                                    CellPadding="0" 
                                    CellSpacing="0"> 
                                    <Columns>
                                        <asp:TemplateField ItemStyle-Wrap="true">
                                            <ItemTemplate>
                                                <%# HowLongAgo( (Csla.SmartDate)Eval( "ChallengeDate" ) ) %>
                                                <%# ViewingGladiatorId == Convert.ToInt32( Eval( "ChallengerId" ) ) ? Eval( "ChallengerName" ) + " challenged " : Eval( "ChallengedName" ) + " was challenged by " %>
                                                <fb:FacebookHyperLink ID="ChallengeLink" runat="server"
                                                    Text='<%# ViewingGladiatorId == Convert.ToInt32( Eval( "ChallengerId" ) ) ? Eval( "ChallengedName" ) : Eval( "ChallengerName" ) %>'
                                                    NavigateUrl='<%# "http://apps.facebook.com/aveimperator/ViewGladiator.aspx?id=" + (ViewingGladiatorId == Convert.ToInt32( Eval( "ChallengedId" ) ) ? Eval("ChallengerId") : Eval("ChallengedId") ) %>' />.
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="75">
                                            <ItemTemplate>
                                                <fb:FacebookHyperLink ID="ViewLink" runat="server" CssClass="microbutton" NavigateUrl='<%# "http://apps.facebook.com/aveimperator/Challenges.aspx?id=" + Eval( "Id" ) %>' style="background:url(Images/Buttons/view.gif);background-repeat: no-repeat;margin-top:2px;" />
                                            </ItemTemplate>
                                        </asp:TemplateField>  
                                        <asp:TemplateField ItemStyle-Width="75">
                                            <ItemTemplate>
                                                <fb:FacebookHyperLink ID="ActionLink" runat="server" CssClass="microbutton" NavigateUrl='<%# "http://apps.facebook.com/aveimperator/Challenges.aspx?id=" + Eval( "Id" ) %>' style='<%# "background:url(Images/Buttons/" + ActionOption( Convert.ToInt32( Eval( "ChallengerId" ) ), Convert.ToInt32( Eval( "ChallengerTacticId" ) ) ) + ".gif);background-repeat: no-repeat;margin-top:2px;" %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>  
                                        <asp:TemplateField ItemStyle-Width="75">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="CancelButton" runat="server" CssClass="microbutton" CommandArgument='<%# Eval( "Id" ) %>' CommandName='<%# CancelOption( Convert.ToInt32( Eval( "ChallengerId" ) ) ) %>' ImageUrl='<%# "~/Images/Buttons/" + CancelOption( Convert.ToInt32( Eval( "ChallengerId" ) ) ) + ".gif" %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>           
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    
    <csla:CslaDataSource ID="ChallengeListDataSource" runat="server" 
        TypeName="AveImperator.Library.ChallengeList" 
        TypeAssemblyName="AveImperator.Library"
        OnSelectObject="ChallengeListDataSource_SelectObject" /> 
</asp:Content>

