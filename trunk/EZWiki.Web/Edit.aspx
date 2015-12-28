<!--
Copyright (C) 2006  Mark Garner

This program is free software; you can redistribute it and/or
modify it under the terms of the GNU General Public License
as published by the Free Software Foundation; either version 2
of the License, or (at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program; if not, write to the Free Software
Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
-->

<%@ Page language="c#" Codebehind="Edit.aspx.cs" AutoEventWireup="false" Inherits="EZWiki.Web.Edit" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Edit</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="stylesheet" type="text/css" href="http://localhost/EZWiki.Web/Styles.css">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" cellSpacing="1" cellPadding="4" width="100%" border="0">
				<TR>
					<TD vAlign="top" width="150">&nbsp;
						<asp:textbox id="TextBoxSearchTerms" runat="server"></asp:textbox><asp:button id="ButtonSearch" runat="server" Text="Search"></asp:button></TD>
					<TD>
						<TABLE class="maintable" id="Table1" cellSpacing="0" cellPadding="4" width="100%" border="0">
							<TR>
								<TD class="unselectedbutton">
									<asp:HyperLink id="HyperLinkText" runat="server" NavigateUrl="Default.aspx">Text</asp:HyperLink></TD>
								<TD class="buttonspace">&nbsp;</TD>
								<TD class="selectedbutton"><asp:label id="LabelEdit" runat="server">Edit</asp:label></TD>
								<TD class="buttonspace">&nbsp;</TD>
								<TD class="unselectedbutton">
									<asp:HyperLink id="HyperLinkSearch" runat="server" NavigateUrl="Search.aspx">Search</asp:HyperLink></TD>
								<TD class="buttonspace">&nbsp;</TD>
								<TD class="unselectedbutton">
									<asp:HyperLink id="HyperLinkHistory" runat="server" NavigateUrl="History.aspx">History</asp:HyperLink></TD>
								<TD class="buttonspace">&nbsp;</TD>
								<TD class="unselectedbutton">
									<asp:HyperLink id="HyperlinkHelp" runat="server" NavigateUrl="Default.aspx?topic=Help - Editing">Help</asp:HyperLink></TD>
								<TD width="100%" class="buttonspace">
									&nbsp;</TD>
							</TR>
							<TR>
								<TD class="wikiarticle" colspan="10">
									<asp:TextBox id="TextBoxTitle" runat="server"></asp:TextBox>
									<asp:TextBox id="TextBoxContent" runat="server" TextMode="MultiLine" Width="100%" Height="400px"></asp:TextBox>
									<asp:Button id="ButtonSave" runat="server" Text="Save"></asp:Button></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
