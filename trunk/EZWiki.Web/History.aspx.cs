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
	/// Summary description for History.
	/// </summary>
	public class History : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.TextBox TextBoxSearchTerms;
		protected System.Web.UI.WebControls.Button ButtonSearch;
		protected System.Web.UI.WebControls.HyperLink HyperLinkText;
		protected System.Web.UI.WebControls.HyperLink HyperLinkEdit;
		protected System.Web.UI.WebControls.Label LabelHistory;
		protected System.Web.UI.WebControls.HyperLink HyperLinkSearch;
		protected System.Web.UI.WebControls.DataGrid DataGridHistory;
		protected System.Web.UI.WebControls.Label LabelTitle;
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
					BindGrid();
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
			this.DataGridHistory.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGridHistory_ItemCommand);
			this.DataGridHistory.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGridHistory_ItemDataBound);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void BindGrid()
		{
			Area currentArea = (Area)Session["CurrentArea"];
			Topic topic = Topic.GetTopicByTitleAndArea(_topicTitle, currentArea);
			if (topic != null)
			{
				LabelTitle.Text = "History for : " + topic.Title;

				TopicCollection historyItems = topic.GetHistory();
				DataGridHistory.DataSource = historyItems;
				DataGridHistory.DataBind();
			}
			else
			{
				//						TextBoxTitle.Text = _topicTitle;
			}
		}

		private void DataGridHistory_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			if (e.CommandName == "Delete")
			{
				int topicID = Convert.ToInt32(e.CommandArgument);
				SearchWordIndexMember.DeleteByTopicID(topicID);
				Topic topic = Topic.GetTopicByTopicID(topicID);
				topic.Delete();
				BindGrid();
			}

			if (e.CommandName == "View")
			{
				int topicID = Convert.ToInt32(e.CommandArgument);
				Topic topic = Topic.GetTopicByTopicID(topicID);
				Response.Redirect("Default.aspx?topic=" + topic.Title + "&version=" + topic.Version.ToString());
			}
		}

		private void DataGridHistory_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
			{
				LinkButton buttonDelete = (LinkButton)e.Item.FindControl("ButtonDelete");
				buttonDelete.CommandArgument = e.Item.Cells[0].Text;

				LinkButton buttonView = (LinkButton)e.Item.FindControl("ButtonView");
				buttonView.CommandArgument = e.Item.Cells[0].Text;
			}
		}

	}
}
