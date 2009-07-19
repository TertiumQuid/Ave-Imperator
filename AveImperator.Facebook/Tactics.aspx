<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Tactics.aspx.cs" Inherits="Tactics" Title="Untitled Page" %>
<%@ MasterType virtualpath="~/MasterPage.master" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="MainContent" Runat="Server">
    <style type="text/css">
        ul { padding-left:1px;margin-left:0; }
        li { list-style:none; margin:2px; }
        .actionbutton { background-repeat:no-repeat;height:22px;display:block;width:84px;padding:4px 0 0 0;margin:0;color:rgb(255,255,255);text-decoration:none;font-weight:bold;text-align:center;alpha(opacity=80);opacity:0.8; }
        .actionbutton:hover { alpha(opacity=100);opacity:1;text-decoration:none; }
    </style>
    <h1 class="title"><asp:Label ID="PageTitle" runat="server" Text="Tactics" /></h1>
    <ave:ErrorLabel ID="ErrorMessage" runat="server" Title="Plan Tactics Error:" />
    <div class="contentbox" style="width:625px;">
        <div class="contentboxtitle" style="width:621px;">Plan Tactics</div>
        <asp:UpdatePanel ID="TacticsUpdatePanel" runat="server">
            <ContentTemplate>
                <p style="padding:3px;">
                    Build your list of actions for this tactic by clicking the action buttons below. During battle, actions will be excuted in the given order, starting over once the set is complete. 
                    Like Seneca says: <i>Luck is what happens when preparation meets opportunity.</i>
                </p>
                <ajax:ReorderList ID="Actions" runat="server" 
                    DataSourceID="ActionListDataSource" 
                    PostBackOnReorder="true" 
                    DragHandleAlignment="Left"
                    ItemInsertLocation="End" 
                    ShowInsertItem="true"
                    SortOrderField="Ordinal">
                    <DragHandleTemplate><div style="background-image:url(Images/move.gif);width:20px;height:20px;margin-left:3px;cursor:pointer;">&nbsp;</div></DragHandleTemplate>
                    <ItemTemplate>
                        <table style="width:450px;border:0px solid black;padding:2px;margin:0" cellpadding=0 cellspacing=0>
                            <tr>
                                <td style="width:25px;"># <%# Eval( "Ordinal" )%>:</td>
                                <td style="font-size:12px;font-weight:bold;"><%# Eval( "Maneuver" )%> </td>
                                <td style="text-align:right;"><asp:LinkButton ID="RemoveItem" runat="server" Text="[remove]" /></td>
                            </tr>
                        </table>
                        
                    </ItemTemplate>
                    <EditItemTemplate><asp:LinkButton ID="RemoveAction" runat="server" text="[remove]" CommandName="Delete" />
                    </EditItemTemplate>
                    <InsertItemTemplate>
                        <br />
                        <asp:DataList ID="Maneuvers" runat="server" DataSourceID="ManeuverListDataSource" OnItemCommand="Maneuvers_ItemCommand" RepeatColumns="6" RepeatLayout="table">
                            <ItemTemplate>
                                <asp:LinkButton ID="ManeuverLink" runat="server" Visible='<%# CanAddMoreActions %>'
                                    onMouseOver='<%# "javascript:this.innerHTML=\"" + Eval( "Item" ) + "\";" %>' 
                                    onMouseOut='<%# "javascript:this.innerHTML=\"" + Eval( "Name" ) + " (" +  Eval( "Score" ) + ")\";" %>' 
                                    Text='<%# Eval( "Name" ) + " (" +  Eval( "Score" ) + ")" %>' 
                                    CommandName='<%# Eval( "Name" ) %>' 
                                    CommandArgument='<%# Eval( "Id" ) + "|" + Eval( "ItemId" ) + "|" + (int)Eval( "ManeuverItemType" ) %>' 
                                    style='<%# "background-image:url(Images/Buttons/action_" + Eval( "ManeuverActionType" ) + ".gif);font-size:95%;" %>' 
                                    CssClass="actionbutton"
                                    />
                            </ItemTemplate>
                        </asp:DataList> 
                    </InsertItemTemplate>
                </ajax:ReorderList>
                <asp:Label ID="LimitLabel" runat="server" style="padding:5px;" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <br />    
    <asp:LinkButton ID="SubmitLink" runat="server" Text="Ready For Battle" CssClass="inputsubmit" />
    
    <csla:CslaDataSource ID="ActionListDataSource" runat="server" 
        TypeName="AveImperator.Library.ActionList" 
        TypeAssemblyName="AveImperator.Library" 
        OnSelectObject="ActionListDataSource_SelectObject" />
    
    <csla:CslaDataSource ID="ManeuverListDataSource" runat="server" 
        TypeName="AveImperator.Library.ManeuverList" 
        TypeAssemblyName="AveImperator.Library"
        OnSelectObject="ManeuverListDataSource_SelectObject" />
</asp:Content>

