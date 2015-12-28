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

using EZWiki.Data;

namespace EZWiki.Search
{
	/// <summary>
	/// Summary description for SearchWordIndexMember.
	/// </summary>
	public class SearchWordIndexMember : EZWiki.Data.Persistence.ObjectBase
	{
		#region Private Members
		private int _searchWordIndexMemberID;
		private int _searchWordID;
		private int _topicID;
		#endregion

		#region Constructors
		public SearchWordIndexMember()
		{
		}

		public SearchWordIndexMember(SqlDataReader reader):base(reader)
		{
			// calls inherited constructor
		}
		#endregion

		#region Properties
		public int SearchWordIndexMemberID
		{
			get { return _searchWordIndexMemberID; }
			set { _searchWordIndexMemberID = value; }
		}

		public int SearchWordID
		{
			get { return _searchWordID; }
			set { _searchWordID = value; }
		}
		
		public int TopicID
		{
			get { return _topicID; }
			set { _topicID = value; }
		}
		#endregion

		#region Member Methods
		#endregion

		#region Static Methods
		public static void DeleteAll()
		{
			SqlCommand command = new SqlCommand("SearchWordIndexMember_DeleteAll", Defaults.DBConnection);
			command.CommandType = CommandType.StoredProcedure;
			command.ExecuteNonQuery();
		}

		public static void DeleteByTopicID(int TopicID)
		{
			SqlCommand command = new SqlCommand("SearchWordIndexMember_DeleteByTopicID", Defaults.DBConnection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add("@TopicID", TopicID);
			command.ExecuteNonQuery();
		}
		#endregion
	}
}
