<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AddGladiator.aspx.cs" Inherits="AddGladiator" Title="Untitled Page" %>
<%@ MasterType virtualpath="~/MasterPage.master" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="MainContent" Runat="Server">
    <h1 class="title">Create Your Gladiator</h1>
    <ave:ErrorLabel ID="ErrorMessage" runat="server" Title="Create Gladiator Error:" />
    <asp:UpdatePanel ID="ChallengersUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Table ID="RegistrationTable" runat="server" CssClass="formtable">
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="3">
                        To create a gladiator, you simply provide a name and then choose a race, god, and class. 
                        These three factors will determine your gladiator’s individual abilities (the coliseum 
                        is ecumenical enough for the strangest histories). The doctores from your school will 
                        provide you with weapons and armor appropriate to your class. But remember Seneca: <i>A 
                        sword never kills; it is a tool in the killer's hand.</i> It is with your <u>wits</u> that 
                        you shall conquer your rivals.
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow><asp:TableCell ColumnSpan="3">&nbsp;</asp:TableCell></asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell><asp:Label ID="NameLabel" runat="server" AssociatedControlID="Name" Text="Galdiator's Name:" /></asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="Name" runat="server" MaxLength="64" style="width:200px;" /></asp:TableCell>
                    <asp:TableCell RowSpan="4" style="text-align:left;" Width="200" Height="200">
                        <table style="background-color:rgb(215,220,235);border:1px solid rgb(80,110,170);width:100%;">
                            <tr><td colspan="2" style="font-weight:bold;">GLADIATOR PROFILE:</td></tr>
                            <tr><td>Starting Constition:</td><td><asp:Label ID="ConstitutionLabel" runat="server" Text="0" /></td></tr>
                            <tr><td>Starting Cunning:</td><td><asp:Label ID="CunningLabel" runat="server" Text="0" /></td></tr>
                            <tr><td>Starting Endurance:</td><td><asp:Label ID="EnduranceLabel" runat="server" Text="0" /></td></tr>
                            <tr><td>Starting Strength:</td><td><asp:Label ID="StrengthLabel" runat="server" Text="0" /></td></tr>
                            <tr><td colspan="2"><asp:Label ID="ClassDescLabel" runat="server" /></td></tr>
                            <tr><td colspan="2"><asp:Label ID="RaceDescLabel" runat="server" /></td></tr>
                            <tr><td colspan="2"><asp:Label ID="GodDescLabel" runat="server" /></td></tr>
                        </table>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell><asp:Label ID="GladiatorClassLabel" runat="server" AssociatedControlID="GladiatorClass" Text="Fighting Class:" /></asp:TableCell>
                    <asp:TableCell><asp:DropDownList ID="GladiatorClass" runat="server" DataSourceID="GladiatorClassListDataSource" DataTextField="Name" DataValueField="Id" AutoPostBack="true" /></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell><asp:Label ID="RaceLabel" runat="server" AssociatedControlID="Race" Text="Race:" /></asp:TableCell>
                    <asp:TableCell><asp:DropDownList ID="Race" runat="server" DataSourceID="RaceListDataSource" DataTextField="Name" DataValueField="Id" AutoPostBack="true" /></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell><asp:Label ID="GodLabel" runat="server" AssociatedControlID="God" Text="Patron God:" /></asp:TableCell>
                    <asp:TableCell><asp:DropDownList ID="God" runat="server" DataSourceID="GodListDataSource" DataTextField="Name" DataValueField="Id" AutoPostBack="true" /></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow><asp:TableCell ColumnSpan="3">&nbsp;</asp:TableCell></asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="3">
                        <asp:CheckBox ID="Oath" runat="server" Checked="true" Enabled="false" Text="I will endure to be burned, to be bound, to be beaten, and to be killed by the sword." TextAlign="left" />
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            <br />
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:LinkButton ID="Add" runat="server" Text="Make Me A Champion" CssClass="inputsubmit" />
    
    <csla:CslaDataSource ID="RaceListDataSource" runat="server" 
        TypeName="AveImperator.Library.RaceList" 
        TypeAssemblyName="AveImperator.Library"
        OnSelectObject="RaceListDataSource_SelectObject" /> 
    <csla:CslaDataSource ID="GodListDataSource" runat="server" 
        TypeName="AveImperator.Library.GodList" 
        TypeAssemblyName="AveImperator.Library"
        OnSelectObject="GodListDataSource_SelectObject" /> 
    <csla:CslaDataSource ID="GladiatorClassListDataSource" runat="server" 
        TypeName="AveImperator.Library.GladiatorClassList" 
        TypeAssemblyName="AveImperator.Library"
        OnSelectObject="GladiatorClassListDataSource_SelectObject" /> 
</asp:Content>

