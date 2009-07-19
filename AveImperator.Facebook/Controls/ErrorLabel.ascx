<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ErrorLabel.ascx.cs" Inherits="Controls_ErrorLabel" %>

<div style="border:1px solid rgb(245,30,30);background-color:rgb(255,255,204);color:rgb(255,0,0);margin:3px 0 3px 0;">
    <div style="background-image: url(Images/error.jpg);background-repeat:no-repeat;width:50px;height:55px;border:1px solid rgb(245,30,30);float:left;margin:6px 12px 0 5px;">&nbsp;</div>
    <div style="width:400px;margin:6px 0 20px 65px;">
        <div style="margin: 0 0 8px 0;"><h2 style="color:rgb(255,0,0);font-weight:bold;"><asp:Label ID="TitleLabel" runat="server" Text="ERROR" /></h2></div>
        <div style="padding-left:8px;"><asp:Label ID="MessageLabel" runat="server" Text="&nbsp;" /></div>
    </div>
</div>
