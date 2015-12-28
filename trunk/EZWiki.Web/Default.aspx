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

<%@ Page language="c#" Codebehind="Default.aspx.cs" AutoEventWireup="false" Inherits="EZWiki.Web.Default" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>WebForm1</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="http://localhost/EZWiki.Web/Styles.css" type="text/css" rel="stylesheet">
		<script src="dynamic.js" language="javascript"></script>
		<script src="popup.js" language="javascript"></script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" cellSpacing="1" cellPadding="4" width="100%" border="0">
				<TR>
					<TD vAlign="top" width="150">&nbsp;
						<asp:textbox id="TextBoxSearchTerms" runat="server"></asp:textbox><asp:button id="ButtonSearch" runat="server" Text="Search"></asp:button><asp:button id="ButtonIndex" runat="server" Text="Index All"></asp:button></TD>
					<TD>
						<TABLE class="maintable" id="Table1" cellSpacing="0" cellPadding="4" width="100%" border="0">
							<TR>
								<TD class="selectedbutton"><asp:label id="LabelText" runat="server">Text</asp:label></TD>
								<TD class="buttonspace">&nbsp;</TD>
								<TD class="unselectedbutton">
									<asp:HyperLink id="HyperLinkEdit" runat="server" NavigateUrl="Edit.aspx">Edit</asp:HyperLink></TD>
								<TD class="buttonspace">&nbsp;</TD>
								<TD class="unselectedbutton">
									<asp:HyperLink id="HyperLinkSearch" runat="server" NavigateUrl="Search.aspx">Search</asp:HyperLink></TD>
								<TD class="buttonspace">&nbsp;</TD>
								<TD class="unselectedbutton">
									<asp:HyperLink id="HyperLinkHistory" runat="server" NavigateUrl="History.aspx">History</asp:HyperLink></TD>
								<TD class="buttonspace">&nbsp;</TD>
								<TD class="unselectedbutton">
									<asp:HyperLink id="HyperlinkHelp" runat="server" NavigateUrl="Default.aspx?topic=Help - General">Help</asp:HyperLink></TD>
								<TD width="100%" class="buttonspace" align="right"></TD>
							</TR>
							<TR>
								<TD class="wikiarticle" colspan="10">
									<asp:Label id="LabelWiki" runat="server">Label</asp:Label></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
