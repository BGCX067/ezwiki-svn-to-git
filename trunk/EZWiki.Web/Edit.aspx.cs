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

namespace EZWiki.Web
{
	/// <summary>
	/// Summary description for Edit.
	/// </summary>
	public class Edit : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.TextBox TextBoxSearchTerms;
		protected System.Web.UI.WebControls.Button ButtonSearch;
		protected System.Web.UI.WebControls.Label LabelText;
		protected System.Web.UI.WebControls.LinkButton LButtonEdit;
		protected System.Web.UI.WebControls.Label LabelEdit;
		protected System.Web.UI.WebControls.HyperLink HyperLinkText;
		protected System.Web.UI.WebControls.HyperLink HyperLinkSearch;
		protected System.Web.UI.WebControls.Button ButtonSave;
		protected System.Web.UI.WebControls.LinkButton LinkButton1;
		protected System.Web.UI.WebControls.TextBox TextBoxTitle;
		protected System.Web.UI.WebControls.TextBox TextBoxContent;
		protected System.Web.UI.WebControls.HyperLink HyperLinkHistory;
		protected System.Web.UI.WebControls.HyperLink HyperlinkHelp;

		private string _topicTitle;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (Request.QueryString["topic"] == null)
				_topicTitle = string.Empty;
			else
				_topicTitle = Request.QueryString["topic"];

			if (!IsPostBack)
			{
				if (_topicTitle != string.Empty)
				{
					Area currentArea = (Area)Session["CurrentArea"];
					Topic topic = Topic.GetTopicByTitleAndArea(_topicTitle, currentArea);
					if (topic != null)
					{
						TextBoxTitle.Text = topic.Title;
						TextBoxTitle.Enabled = false;
						TextBoxContent.Text = topic.Content;
					}
					else
					{
						string [] words = _topicTitle.Split(' ');
						foreach (string word in words)
						{
							if (TextBoxTitle.Text.Length > 0)
								TextBoxTitle.Text += " ";
							TextBoxTitle.Text += word.Substring(0,1).ToUpper() + word.Substring(1).ToLower();
						}
					}
				}

				HyperLinkText.NavigateUrl = "Default.aspx?topic=" + _topicTitle;
				HyperLinkHistory.NavigateUrl = "History.aspx?topic=" + _topicTitle;
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
			this.ButtonSave.Click += new System.EventHandler(this.ButtonSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void ButtonText_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("Default.aspx");
		}

		private void ButtonSearch_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("Search.aspx?terms=" + TextBoxSearchTerms.Text); 
		}

		private void ButtonSave_Click(object sender, System.EventArgs e)
		{
			if (_topicTitle != string.Empty)
			{
				Area currentArea = (Area)Session["CurrentArea"];
				Topic oldTopic = Topic.GetTopicByTitleAndArea(_topicTitle, currentArea);
				int nextVersionNumber = 1;
				if (oldTopic != null)
				{
					oldTopic.IsCurrent = false;
					oldTopic.Persist();
					nextVersionNumber = oldTopic.Version + 1;
				}

				currentArea = (Area)Session["CurrentArea"];

				Topic newTopic = new Topic();
				newTopic.Title = TextBoxTitle.Text;
				newTopic.Content = TextBoxContent.Text;
				newTopic.Version = nextVersionNumber;
				newTopic.CreatedDate = DateTime.Now;
				newTopic.IsCurrent = true;
				newTopic.AreaID = currentArea.AreaID;
				newTopic.Persist();

				Response.Redirect("Default.aspx?topic=" + newTopic.Title);
			}
		}
	}
}
