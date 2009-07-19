<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Record.aspx.cs" Inherits="Record" Title="Untitled Page" %>
<%@ MasterType virtualpath="~/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <h1 class="title"><asp:Label ID="TitleLabel" runat="server" /></h1>
    <div class="contentbox" style="width:625px;">
        <div class="contentboxtitle" style="width:621px;">Battles</div>
        <br />
        <asp:UpdatePanel ID="BattlesUpdatePanel" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <p>
                Show: 
                <asp:DropDownList ID="OutcomeFilter" runat="server">
                    <asp:ListItem Text="All Battles" Value="1" Selected="true" />
                    <asp:ListItem Text="Only Victories" Value="2" />
                    <asp:ListItem Text="Only Defeats" Value="3" />
                    <asp:ListItem Text="Only Draws" Value="4" />
                </asp:DropDownList>
                Since
                <asp:TextBox ID="BattleDate" runat="server" />
                </p>
                <asp:GridView ID="Battles" runat="server" 
                    DataSourceID="BattleListDataSource" 
                    DataKeyNames="Id"   
                    AllowSorting="true"
                    AutoGenerateColumns="false" 
                    EmptyDataText="This novice is fresh to the arena with no battles to his name."
                    AllowPaging="true" 
                    PageSize="25" 
                    PagerSettings-Mode="NumericFirstLast" 
                    PagerSettings-PageButtonCount="25" 
                    CssClass="grid"
                    GridLines="none" Width="624" 
                    CellPadding="0" 
                    CellSpacing="0"> 
                    <Columns>
                        <asp:BoundField DataField="ChallengeDate" HeaderText="Date" SortExpression="ChallengeDate" DataFormatString="{0:MM/dd/yyyy}" />
                        <asp:BoundField DataField="Victor" HeaderText="Victor" SortExpression="Victor" />
                    </Columns>
                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <csla:CslaDataSource ID="BattleListDataSource" runat="server" 
        TypeName="AveImperator.Library.BattleList" 
        TypeAssemblyName="AveImperator.Library"
        OnSelectObject="BattleListDataSource_SelectObject" /> 
</asp:Content>

