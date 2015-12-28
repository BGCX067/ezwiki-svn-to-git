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
	/// Summary description for Search.
	/// </summary>
	public class Search : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.TextBox TextBoxSearchTerms;
		protected System.Web.UI.WebControls.Button ButtonSearch;
		protected System.Web.UI.WebControls.Label LabelSearch;
		protected System.Web.UI.WebControls.HyperLink HyperLinkEdit;
		protected System.Web.UI.WebControls.HyperLink HyperLinkText;
		protected System.Web.UI.WebControls.Button ButtonSearch2;
		protected System.Web.UI.WebControls.TextBox TextBoxSearchTerms2;
		protected System.Web.UI.WebControls.HyperLink HyperLinkHistory;
		protected System.Web.UI.WebControls.HyperLink HyperlinkHelp;
		protected System.Web.UI.WebControls.Table TableSearchResultsDisplay;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (Request.QueryString["terms"] != null)
			{
				bool searchOnlyCurrent = true;
				TopicCollection topics = Searcher.PerformSearch(Request.QueryString["terms"].Split(' '), searchOnlyCurrent);

				if (topics.Count > 0)
				{
					foreach (Topic topic in topics)
					{
						TableRow tr = new TableRow();
						TableCell tc = new TableCell();
				
						tc.Text = "<a href=Default.aspx?topic=" + topic.Title + ">" + topic.Title.ToLower() + "</a>";
						tr.Cells.Add(tc);
				
						TableSearchResultsDisplay.Rows.Add(tr);
					}
				}
				else
				{
					TableRow tr = new TableRow();
					TableCell tc = new TableCell();
				
					tc.Text = "No items were returned.";
					tr.Cells.Add(tc);
				
					TableSearchResultsDisplay.Rows.Add(tr);
				}
			}
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
			this.ButtonSearch2.Click += new System.EventHandler(this.ButtonSearch2_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void ButtonSearch2_Click(object sender, System.EventArgs e)
		{
			bool searchOnlyCurrent = true;
			TopicCollection topics = Searcher.PerformSearch(TextBoxSearchTerms2.Text.Split(' '), searchOnlyCurrent);

			if (topics.Count > 0)
			{
				
				foreach (Topic topic in topics)
				{
					TableRow tr = new TableRow();
					TableCell tc = new TableCell();
				
					tc.Text = "<a href=Default.aspx?topic=" + topic.Title + ">" + topic.Title.ToLower() + "</a>";
					tr.Cells.Add(tc);
				
					TableSearchResultsDisplay.Rows.Add(tr);
				}
			}
			else
			{
				TableRow tr = new TableRow();
				TableCell tc = new TableCell();
				
				tc.Text = "No items were returned.";
				tr.Cells.Add(tc);
				
				TableSearchResultsDisplay.Rows.Add(tr);
			}		
		}

		private void ButtonSearch_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("Search.aspx?terms=" + TextBoxSearchTerms.Text); 
		}
	}
}
