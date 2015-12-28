/*
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
*/

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using EZWiki.Data;
using EZWiki.Search;

namespace EZWiki.Web
{
	/// <summary>
	/// Summary description for WebForm1.
	/// </summary>
	public class Default : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.TextBox TextBoxSearchTerms;
		protected System.Web.UI.WebControls.Button ButtonIndex;
		protected System.Web.UI.WebControls.Label LabelWiki;
		protected System.Web.UI.WebControls.Label LabelText;
		protected System.Web.UI.WebControls.Button ButtonSearch;
		protected System.Web.UI.WebControls.HyperLink HyperLinkEdit;
		protected System.Web.UI.WebControls.HyperLink HyperLinkSearch;
		protected System.Web.UI.WebControls.HyperLink HyperLinkHistory;
		protected System.Web.UI.WebControls.HyperLink HyperlinkHelp;

		private string _topicTitle;
		private int _topicVersion;
		protected System.Web.UI.WebControls.DropDownList DropDownList1;
		private bool _isHistorical;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (Request.QueryString["topic"] == null)
				_topicTitle = "Home Page";
			else
			{
				_topicTitle = Request.QueryString["topic"];
				if (string.Empty == _topicTitle)
					_topicTitle = "Home Page";

				if (Request.QueryString["version"] == null)
					_isHistorical = false;
				else
					_isHistorical = true;

				if (_isHistorical)
					_topicVersion = Convert.ToInt32(Request.QueryString["version"]);
			}

			// set up hyperlinks
			if (_isHistorical)
			{
				HyperLinkEdit.Enabled = false;
			}
			else
			{
				HyperLinkEdit.NavigateUrl = "Edit.aspx?topic=" + _topicTitle;
			}
			HyperLinkHistory.NavigateUrl = "History.aspx?topic=" + _topicTitle;

			// retrieve topic from db
			Area currentArea = (Area)Session["CurrentArea"];
			Topic topic;
			if (!_isHistorical)
				topic = Topic.GetTopicByTitleAndArea(_topicTitle, currentArea); // gets current document
			else
				topic = Topic.GetTopicByTitleVersionAndArea(_topicTitle, _topicVersion, currentArea); // gets by version

			if (topic != null)
				RenderTopicToPage(topic);
			else
			{
				string article = string.Empty;
				string formattedTitle = string.Empty;
				string [] words = _topicTitle.Split(' ');
				foreach (string word in words)
				{
					if (formattedTitle.Length > 0)
						formattedTitle += " ";
					formattedTitle += word.Substring(0,1).ToUpper() + word.Substring(1).ToLower();
				}

				article += "<table width=\"100%\"><tr><td width=\"100%\"><h2>" + formattedTitle + "</h2></td><td><span class=\"EditLink\"><a href=\"Edit.aspx?topic=" + _topicTitle + "\">[Edit]</a></span></td></tr></table>";
				article += "<hr width=100%>";
				article += "<p>EZWiki does not have an aricle with this exact name.  Please search for " + _topicTitle + " in EZWiki or click edit to contribute on this topic.</p>";
				article += "<p>You could also contribute to " + _topicTitle + " by <a href=\"Edit.aspx?topic=" + _topicTitle + "\">creating it</a>.</p>";
				LabelWiki.Text = article;
			}
		}

		private void RenderTopicToPage(Topic topic)
		{
			Area currentArea = (Area)Session["CurrentArea"];
			string article = string.Empty;
			if (_isHistorical) // put version number in the title label
			{
				article += "<table width=\"100%\"><tr><td width=\"100%\"><h2>" + topic.Title + " (Version " + topic.Version.ToString() + ")</h2></td><td>&nbsp;</td></tr></table>";
			}
			else
			{
				article += "<table width=\"100%\"><tr><td width=\"100%\"><h2>" + topic.Title + "</h2></td>";
				article += "<td><span class=\"EditLink\"><a href=\"Edit.aspx?topic=" + _topicTitle + "\">[Edit]</a></span></td></tr></table>";
			}
			article += "<hr width=100%>";
			article += "<p>" + topic.RenderContentAsHTML() + "</p>";
			LabelWiki.Text = article;
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.ButtonSearch.Click += new System.EventHandler(this.ButtonSearch_Click);
			this.ButtonIndex.Click += new System.EventHandler(this.ButtonIndex_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void ButtonSearch_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("Search.aspx?terms=" + TextBoxSearchTerms.Text); 
		}

		private void ButtonIndex_Click(object sender, System.EventArgs e)
		{
			IndexBuilder.BuildIndex();
		}

		private void ButtonEdit_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("Edit.aspx");
		}


	}
}
