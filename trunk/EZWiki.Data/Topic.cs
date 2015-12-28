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
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Xml;

namespace EZWiki.Data
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public class Topic : Persistence.ObjectBase
	{
		#region Private Members
		private int _topicID;
		private string _title;
		private string _content;
		private bool _isLocked;
		private int _version;
		private int _areaID;
		private bool _isCurrent;
		private DateTime _createdDate;
		#endregion

		#region Constructors
		/// <summary>
		/// 
		/// </summary>
		public Topic()
		{
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="reader"></param>
		public Topic(SqlDataReader reader):base(reader)
		{
			//calls inherited constructor
		}
		#endregion

		#region Properties
		/// <summary>
		/// 
		/// </summary>
		public int TopicID
		{
			get { return _topicID; }
			set { _topicID = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string Title
		{
			get { return _title; }
			set { _title = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string Content
		{
			get { return _content; }
			set { _content = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public bool IsLocked
		{
			get { return _isLocked; }
			set { _isLocked = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public int Version
		{
			get { return _version; }
			set { _version = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public int AreaID
		{
			get { return _areaID; }
			set { _areaID = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public bool IsCurrent
		{
			get { return _isCurrent; }
			set { _isCurrent = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public DateTime CreatedDate
		{
			get { return _createdDate; }
			set { _createdDate = value; }
		}
		#endregion

		#region Member Methods
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public string RenderContentAsHTML()
		{
			// I bet I could find a way to do this without looping through it so many times :)
			StringBuilder sb = new StringBuilder();
			string contentTemp = "<p>" + this.Content.Replace(Environment.NewLine, "</p><p>") + "</p>";
			sb.Append(contentTemp);

			// Inline Query (first column from the first row and not put in a table)
			// I'm going to do it first so that it will be evaluated first so that
			// you can use it in a link
			contentTemp = sb.ToString();
			sb = new StringBuilder();
			int i = 0;
			int posNextBeginTag = 0;
			int posNextEndTag = 0;
			while (contentTemp.Length > 0)
			{
				posNextBeginTag = contentTemp.IndexOf("++", i);
				if (posNextBeginTag >= 0)
				{
					sb.Append(contentTemp.Substring(i, posNextBeginTag));
					contentTemp = contentTemp.Remove(i, posNextBeginTag+2);
					posNextEndTag = contentTemp.IndexOf("++", i);
					string queryTag = contentTemp.Substring(0, posNextEndTag);
					contentTemp = contentTemp.Remove(i, posNextEndTag+2);

					string [] queryTagParts = queryTag.Split('|');
					if (queryTagParts.GetUpperBound(0) == 1)
					{
						// ((CONNECTION_NAME|QUERY_TEXT))
						string connectionName = queryTagParts[0];
						string queryText = queryTagParts[1];
						Area area = Area.GetAreaByAreaID(this.AreaID);
						DBConnection dbConnection = DBConnection.GetDBConnectionByNameAndArea(connectionName, area);
						if (dbConnection != null)
						{
							DataTable results = dbConnection.PerformQuery(queryText);
							if (results != null)
							{
								sb.Append(results.Rows[0][0].ToString());
							}
							else
							{
								sb.Append("(Syntax error in query - Error in query text or invalid connection)");
							}
						}
						else
						{
							sb.Append("(Syntax error in query - DBConnection by that name not found)");
						}
						
					}
					else
					{
						sb.Append("(Syntax error in query tag - wrong number of parameters)");
					}
				}
				else
				{
					sb.Append(contentTemp);
					contentTemp = string.Empty;
				}
			}

			// LINKS
			contentTemp = sb.ToString();
			sb = new StringBuilder();
			i = 0;
			posNextBeginTag = 0;
			posNextEndTag = 0;
			while (contentTemp.Length > 0)
			{
				posNextBeginTag = contentTemp.IndexOf("[[", i);
				if (posNextBeginTag >= 0)
				{
					sb.Append(contentTemp.Substring(i, posNextBeginTag));
					contentTemp = contentTemp.Remove(i, posNextBeginTag+2);
					posNextEndTag = contentTemp.IndexOf("]]", i);
					string link = contentTemp.Substring(0, posNextEndTag);
					contentTemp = contentTemp.Remove(i, posNextEndTag+2);

					string [] linkParts = link.Split('|');
					if (linkParts.GetUpperBound(0) == 1)
					{
						if (linkParts[0].ToString().StartsWith("http://"))
						{
							sb.Append("<a href=\"" + linkParts[0] + "\">" + linkParts[1] + "</a>");
						}
						else
						{
							sb.Append("<a href=\"?topic=" + linkParts[0] + "\">" + linkParts[1] + "</a>");
						}
					}
					else
					{
						if (linkParts[0].ToString().StartsWith("http://"))
						{
							sb.Append("<a href=\"" + linkParts[0] + "\">" + linkParts[0] + "</a>");
						}
						else
						{
							sb.Append("<a href=\"?topic=" + linkParts[0] + "\">" + linkParts[0] + "</a>");
						}
					}
				}
				else
				{
					sb.Append(contentTemp);
					contentTemp = string.Empty;
				}
			}

			// BOLD
			contentTemp = sb.ToString();
			sb = new StringBuilder();
			i = 0;
			posNextBeginTag = 0;
			posNextEndTag = 0;
			while (contentTemp.Length > 0)
			{
				posNextBeginTag = contentTemp.IndexOf("'''", i);
				if (posNextBeginTag >= 0)
				{
					sb.Append(contentTemp.Substring(i, posNextBeginTag));
					contentTemp = contentTemp.Remove(i, posNextBeginTag+3);
					posNextEndTag = contentTemp.IndexOf("'''", i);
					string boldedText = contentTemp.Substring(0, posNextEndTag);
					contentTemp = contentTemp.Remove(i, posNextEndTag+3);

					sb.Append("<b>" + boldedText + "</b>");
				}
				else
				{
					sb.Append(contentTemp);
					contentTemp = string.Empty;
				}
			}
			
			// SECTIONS
			contentTemp = sb.ToString();
			sb = new StringBuilder();
			i = 0;
			posNextBeginTag = 0;
			posNextEndTag = 0;
			while (contentTemp.Length > 0)
			{
				posNextBeginTag = contentTemp.IndexOf("==", i);
				if (posNextBeginTag >= 0)
				{
					sb.Append(contentTemp.Substring(i, posNextBeginTag));
					contentTemp = contentTemp.Remove(i, posNextBeginTag+2);
					posNextEndTag = contentTemp.IndexOf("==", i);
					string sectionHeaderText = contentTemp.Substring(0, posNextEndTag);
					contentTemp = contentTemp.Remove(i, posNextEndTag+2);

					sb.Append("<h3>" + sectionHeaderText + "</h3><hr />");
				}
				else
				{
					sb.Append(contentTemp);
					contentTemp = string.Empty;
				}
			}

			// DB QUERIES (Stored Procedures Only)
			contentTemp = sb.ToString();
			sb = new StringBuilder();
			i = 0;
			posNextBeginTag = 0;
			posNextEndTag = 0;
			int tableCount = 1;
			while (contentTemp.Length > 0)
			{
				posNextBeginTag = contentTemp.IndexOf("((", i);
				if (posNextBeginTag >= 0)
				{
					sb.Append(contentTemp.Substring(i, posNextBeginTag));
					contentTemp = contentTemp.Remove(i, posNextBeginTag+2);
					posNextEndTag = contentTemp.IndexOf("))", i);
					string queryTag = contentTemp.Substring(0, posNextEndTag);
					contentTemp = contentTemp.Remove(i, posNextEndTag+2);

					string [] queryTagParts = queryTag.Split('|');
					if (queryTagParts.GetUpperBound(0) == 1)
					{
						// ((CONNECTION_NAME|QUERY_TEXT))
						string connectionName = queryTagParts[0];
						string queryText = queryTagParts[1];
						Area area = Area.GetAreaByAreaID(this.AreaID);
						DBConnection dbConnection = DBConnection.GetDBConnectionByNameAndArea(connectionName, area);
						if (dbConnection != null)
						{
							DataTable results = dbConnection.PerformQuery(queryText);
							if (results != null)
							{
								sb.Append("<table border=0><tr><td valign=top>");
								sb.Append("<img src=\"images/plus.gif\" class=\"Clickable\" onclick=\"toggleIcon(this); toggleTable(table" + tableCount.ToString() + ")\" />");
								sb.Append("</td><td>");
								sb.Append("<table id=\"table" + tableCount.ToString() + "\"class=\"QueryResultsTable\">");
								tableCount++;
								sb.Append("<tr>");
								foreach (DataColumn dc in results.Columns)
								{
									sb.Append("<td class=\"QueryResultsHeader\">");
									sb.Append(dc.ColumnName);
									sb.Append("</td>");
								}
								sb.Append("</tr>");

								foreach (DataRow dr in results.Rows)
								{
									sb.Append("<tr class=\"Collapsed\">");
									foreach (object item in dr.ItemArray)
									{
										sb.Append("<td class=\"QueryResultsItem\">");
										sb.Append(Convert.ToString(item));
										sb.Append("</td>");
									}
									sb.Append("</tr>");
								}
								sb.Append("</table>");
								sb.Append("</td></tr></table>");
							}
							else
							{
								sb.Append("(Syntax error in query - Error in query text or invalid connection)");
							}
						}
						else
						{
							sb.Append("(Syntax error in query - DBConnection by that name not found)");
						}
						
					}
					else
					{
						sb.Append("(Syntax error in query tag - wrong number of parameters)");
					}
				}
				else
				{
					sb.Append(contentTemp);
					contentTemp = string.Empty;
				}
			}

			// TABLE INFO QUERIES
			contentTemp = sb.ToString();
			sb = new StringBuilder();
			i = 0;
			posNextBeginTag = 0;
			posNextEndTag = 0;
			while (contentTemp.Length > 0)
			{
				posNextBeginTag = contentTemp.IndexOf("__", i);
				if (posNextBeginTag >= 0)
				{
					sb.Append(contentTemp.Substring(i, posNextBeginTag));
					contentTemp = contentTemp.Remove(i, posNextBeginTag+2);
					posNextEndTag = contentTemp.IndexOf("__", i);
					string queryTag = contentTemp.Substring(0, posNextEndTag);
					contentTemp = contentTemp.Remove(i, posNextEndTag+2);

					string [] queryTagParts = queryTag.Split('|');
					if (queryTagParts.GetUpperBound(0) == 1 || queryTagParts.GetUpperBound(0) == 2)
					{
						// ((CONNECTION_NAME|TABLE_NAME))
						string connectionName = queryTagParts[0];
						string tableName = queryTagParts[1];
						Area area = Area.GetAreaByAreaID(this.AreaID);
						DBConnection dbConnection = DBConnection.GetDBConnectionByNameAndArea(connectionName, area);
						if (dbConnection != null)
						{
							DataTable results;
							if (queryTagParts.GetUpperBound(0) == 1) // table info query
							{
								results = dbConnection.GetTableInfo(tableName);
							}
							else // column info query
							{
								string columnName = queryTagParts[2];
								results = dbConnection.GetColumnInfo(tableName, columnName);
							}

							if (results != null)
							{
								sb.Append("<table border=0><tr><td valign=top>");
								sb.Append("<img src=\"images/plus.gif\" class=\"Clickable\" onclick=\"toggleIcon(this); toggleTable(table" + tableCount.ToString() + ")\" />");
								sb.Append("</td><td>");
								sb.Append("<table id=\"table" + tableCount.ToString() + "\"class=\"QueryResultsTable\">");
								tableCount++;
								sb.Append("<tr>");
								foreach (DataColumn dc in results.Columns)
								{
									sb.Append("<td class=\"QueryResultsHeader\">");
									sb.Append(dc.ColumnName);
									sb.Append("</td>");
								}
								sb.Append("</tr>");

								foreach (DataRow dr in results.Rows)
								{
									sb.Append("<tr class=\"Collapsed\">");
									int columnOrdinal = 0;
									foreach (object item in dr.ItemArray)
									{
										sb.Append("<td class=\"QueryResultsItem\">");
										if (columnOrdinal <= 1) // if it is the Name column or Data Type column
											sb.Append("<a href=\"Default.aspx?topic=" + Convert.ToString(item) + "\">" + Convert.ToString(item) + "</a>");
										else
											sb.Append(Convert.ToString(item));
										sb.Append("</td>");
										columnOrdinal++;
									}
									sb.Append("</tr>");
								}
								sb.Append("</table>");
								sb.Append("</td></tr></table>");
							}
							else
							{
								sb.Append("(Syntax error in query - Error in query text or invalid connection)");
							}
						}
						else
						{
							sb.Append("(Syntax error in query - DBConnection by that name not found)");
						}
						
					}
					else
					{
						sb.Append("(Syntax error in query tag - wrong number of parameters)");
					}
				}
				else
				{
					sb.Append(contentTemp);
					contentTemp = string.Empty;
				}
			}

			// SSIS Column Lineage Query
			contentTemp = sb.ToString();
			sb = new StringBuilder();
			i = 0;
			posNextBeginTag = 0;
			posNextEndTag = 0;
			while (contentTemp.Length > 0)
			{
				posNextBeginTag = contentTemp.IndexOf("**", i);
				if (posNextBeginTag >= 0)
				{
					sb.Append(contentTemp.Substring(i, posNextBeginTag));
					contentTemp = contentTemp.Remove(i, posNextBeginTag+2);
					posNextEndTag = contentTemp.IndexOf("**", i);
					string queryTag = contentTemp.Substring(0, posNextEndTag);
					contentTemp = contentTemp.Remove(i, posNextEndTag+2);

					string ColumnName = queryTag;

					sb.Append("Column Lineage For: " + ColumnName + "<br>");

					XmlDocument dtsxDocument = new XmlDocument(); 
					dtsxDocument.Load("C:\\dev\\EZWiki\\EZWiki.Web\\TestPackage.dtsx");

					XmlNamespaceManager nsmgr = new XmlNamespaceManager(dtsxDocument.NameTable);
					nsmgr.AddNamespace("DTS", "www.microsoft.com/SqlServer/Dts");

					// This finds the External Metadata Column node that helps me start the path of finding where this data goes
					// The ComponentClassID is hardcoded here.
					// I'll need to have a few of these queries
					string xPath = "/DTS:Executable/DTS:Executable[@DTS:ExecutableType='DTS.Pipeline.1']/DTS:ObjectData/pipeline/components/component[@componentClassID='{2C0A8BE5-1EDC-4353-A0EF-B778599C65A0}']/outputs/output[@isErrorOut='false']/externalMetadataColumns/externalMetadataColumn[@name='" + ColumnName + "']";

					// Go back and find the Output Column node - look for name change.
					XmlNode externalMetadataColumnNode = dtsxDocument.SelectSingleNode(xPath, nsmgr);
					if (externalMetadataColumnNode == null)
					{
						sb.Append("Column Not Found");
					}
					else
					{
						XmlNode sourceComponent = externalMetadataColumnNode.ParentNode.ParentNode.ParentNode.ParentNode;
						sb.Append("OLEDB Source: " + sourceComponent.Attributes["name"].Value + "<br>");
			
						xPath = "/DTS:Executable/DTS:Executable[@DTS:ExecutableType='DTS.Pipeline.1']/DTS:ObjectData/pipeline/components/component[@componentClassID='{2C0A8BE5-1EDC-4353-A0EF-B778599C65A0}']/outputs/output[@isErrorOut='false']/outputColumns/outputColumn[@externalMetadataColumnId='" + externalMetadataColumnNode.Attributes["id"].Value + "']";
						XmlNode outputColumnNode = dtsxDocument.SelectSingleNode(xPath, nsmgr);

						// Navigate to parent
						XmlNode outputNode = outputColumnNode.ParentNode.ParentNode;
						XmlNode nextComponentNode = FindNextComponentNode(dtsxDocument, outputNode, nsmgr);

						// begin recursion
						EvaluateNextNode(dtsxDocument, nextComponentNode, nsmgr, sb, ColumnName);
					}
				}
				else
				{
					sb.Append(contentTemp);
					contentTemp = string.Empty;
				}
			}

			return sb.ToString();
		}

		private XmlNode FindNextComponentNode(XmlDocument dtsxDocument, XmlNode outputNode, XmlNamespaceManager nsmgr)
		{
			string xPath;
			// Find the path node so I can follow it to the next component
			xPath = "/DTS:Executable/DTS:Executable[@DTS:ExecutableType='DTS.Pipeline.1']/DTS:ObjectData/pipeline/paths/path[@startId='" + outputNode.Attributes["id"].Value + "']";
			XmlNode pathNode = dtsxDocument.SelectSingleNode(xPath, nsmgr);
			if (pathNode == null)  // this means there is no path from this component - the end of the line
			{
				return null;
			}

			xPath = "/DTS:Executable/DTS:Executable[@DTS:ExecutableType='DTS.Pipeline.1']/DTS:ObjectData/pipeline/components/component/inputs/input[@id='" + pathNode.Attributes["endId"].Value + "']";
			XmlNode nextComponentInput = dtsxDocument.SelectSingleNode(xPath, nsmgr);

			return nextComponentInput.ParentNode.ParentNode;
		}

		private void EvaluateNextNode(XmlDocument dtsxDocument, XmlNode ThisNode, XmlNamespaceManager nsmgr, StringBuilder sb, string ColumnName)
		{

			// Derived Column Task
			if (ThisNode.Attributes["componentClassID"].Value == "{9CF90BF0-5BCC-4C63-B91D-1F322DC12C26}")
			{
				// TODO: Need to query by the ID of the column and then go back and get the friendly expression
				XmlNodeList friendlyExpressionInputNodes = ThisNode.SelectNodes("inputs/input/inputColumns/inputColumn/properties/property[@name='FriendlyExpression']", nsmgr);
				foreach (XmlNode friendlyExpressionNode in friendlyExpressionInputNodes)
				{
					if (friendlyExpressionNode.InnerText.IndexOf(ColumnName) > -1)
						sb.Append("Derived Column Task: " + ThisNode.Attributes["name"].Value + " | Expression: " + friendlyExpressionNode.InnerText + "<br>");
				}

				XmlNodeList friendlyExpressionOuputNodes = ThisNode.SelectNodes("outputs/output[@isErrorOut='false']/outputColumns/outputColumn/properties/property[@name='FriendlyExpression']", nsmgr);
				foreach (XmlNode friendlyExpressionNode in friendlyExpressionOuputNodes)
				{
					if (friendlyExpressionNode.InnerText.IndexOf(ColumnName) > -1)
						sb.Append("Derived Column Task: " + ThisNode.Attributes["name"].Value + " | Expression: " + friendlyExpressionNode.InnerText + "<br>");
				}
			}

			// Lookup Task
			if (ThisNode.Attributes["componentClassID"].Value == "{0FB4AABB-C027-4440-809A-1198049BF117}")
			{
				sb.Append("Lookup Task: " + ThisNode.Attributes["name"].Value + "<br>");
			}

			// OLEDB Destination
			if (ThisNode.Attributes["componentClassID"].Value == "{E2568105-9550-4F71-A638-B7FE42E66922}")
			{
				sb.Append("OLEDB Destination: " + ThisNode.Attributes["name"].Value + "<br>");
			}

			// Other Tasks
			if (ThisNode.Attributes["componentClassID"].Value != "{E2568105-9550-4F71-A638-B7FE42E66922}" && ThisNode.Attributes["componentClassID"].Value != "{0FB4AABB-C027-4440-809A-1198049BF117}" && ThisNode.Attributes["componentClassID"].Value != "{9CF90BF0-5BCC-4C63-B91D-1F322DC12C26}")
			{
				sb.Append("Unknown Task Type: " + ThisNode.Attributes["name"].Value + "<br>");
			}

			XmlNodeList outputNodes = ThisNode.SelectNodes("outputs/output[@isErrorOut='false']", nsmgr);
			bool multipleOutputs = false;
			if (outputNodes.Count > 1)
				multipleOutputs = true;

			if (multipleOutputs)
			{
				sb.Append("<table border=0 cellspacing=0 cellpadding=5><tr>");
			}

			foreach (XmlNode outputNode in outputNodes)
			{
				XmlNode nextComponentNode = null;
				if (outputNode != null)
					nextComponentNode = FindNextComponentNode(dtsxDocument, outputNode, nsmgr);

				if (nextComponentNode != null) // only if we found a "next" component
				{
					if (multipleOutputs)
						sb.Append("<td><p>");
					EvaluateNextNode(dtsxDocument, nextComponentNode, nsmgr, sb, ColumnName); // recurse to the next component
					if (multipleOutputs)
						sb.Append("</p></td>");

				}
			}

			if (multipleOutputs)
			{
				sb.Append("</tr></table>");
			}
		}

		/// <summary>
		/// Renders first as HTML then strips out all HTML tags
		/// This allows for query results to be indexed
		/// </summary>
		/// <returns></returns>
		public string RenderContentAsWords()
		{
			string htmlContent = this.RenderContentAsHTML();
			StringBuilder sb = new StringBuilder();
			while (htmlContent.Length > 0)
			{
				if (!sb.ToString().EndsWith(" "))
					sb.Append(" ");
				sb.Append(htmlContent.Substring(0, htmlContent.IndexOf("<")));
				htmlContent = htmlContent.Remove(0, htmlContent.IndexOf("<")+1);
				htmlContent = htmlContent.Remove(0, htmlContent.IndexOf(">")+1);
			}
			return sb.ToString();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public TopicCollection GetHistory()
		{
			SqlCommand command = new SqlCommand("Topic_GetHistoryByTitle", Defaults.DBConnection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add("@Title", this.Title);
			SqlDataReader reader = command.ExecuteReader();
			
			TopicCollection toReturn = new TopicCollection();
			while (reader.Read())
			{
				Topic newObj = new Topic(reader);
				toReturn.Add(newObj);
			}
			reader.Close();
			return toReturn;
		}
		#endregion

		#region Static Methods
		/// <summary>
		/// 
		/// </summary>
		/// <param name="TopicID"></param>
		/// <returns></returns>
		public static Topic GetTopicByTopicID(int TopicID)
		{
			SqlCommand command = new SqlCommand("Topic_GetByTopicID", Defaults.DBConnection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add("@TopicID", TopicID);
			SqlDataReader reader = command.ExecuteReader();

			Topic toReturn = null;
			if (reader.Read())
			{
				toReturn = new Topic(reader);
			}
			reader.Close();
			return toReturn;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="Title"></param>
		/// <param name="Area"></param>
		/// <returns></returns>
		public static Topic GetTopicByTitleAndArea(string Title, Area Area)
		{
			SqlCommand command = new SqlCommand("Topic_GetByTitleAndAreaID", Defaults.DBConnection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add("@Title", Title);
			command.Parameters.Add("@AreaID", Area.AreaID);
			SqlDataReader reader = command.ExecuteReader();

			Topic toReturn = null;
			if (reader.Read())
			{
				toReturn = new Topic(reader);
			}
			reader.Close();
			return toReturn;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="Title"></param>
		/// <param name="Version"></param>
		/// <param name="Area"></param>
		/// <returns></returns>
		public static Topic GetTopicByTitleVersionAndArea(string Title, int Version, Area Area)
		{
			SqlCommand command = new SqlCommand("Topic_GetByTitleVersionAndAreaID", Defaults.DBConnection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add("@Title", Title);
			command.Parameters.Add("@Version", Version);
			command.Parameters.Add("@AreaID", Area.AreaID);
			SqlDataReader reader = command.ExecuteReader();

			Topic toReturn = null;
			if (reader.Read())
			{
				toReturn = new Topic(reader);
			}
			reader.Close();
			return toReturn;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public static TopicCollection GetAllTopics()
		{
			SqlCommand command = new SqlCommand("Topic_GetAll", Defaults.DBConnection);
			command.CommandType = CommandType.StoredProcedure;
			SqlDataReader reader = command.ExecuteReader();
			
			TopicCollection toReturn = new TopicCollection();
			while (reader.Read())
			{
				Topic newObj = new Topic(reader);
				toReturn.Add(newObj);
			}
			reader.Close();
			return toReturn;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="SearchWord"></param>
		/// <param name="IsCurrent"></param>
		/// <returns></returns>
		public static TopicCollection GetTopicsBySearchWordAndCurrency(string SearchWord, bool IsCurrent)
		{
			SqlCommand command = new SqlCommand("Topic_GetBySearchWordAndCurrency", Defaults.DBConnection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add("@SearchWord", SearchWord);
			command.Parameters.Add("@IsCurrent", IsCurrent);
			SqlDataReader reader = command.ExecuteReader();
			
			TopicCollection toReturn = new TopicCollection();
			while (reader.Read())
			{
				Topic newObj = new Topic(reader);
				toReturn.Add(newObj);
			}
			reader.Close();
			return toReturn;
		}
		#endregion
	}
}
