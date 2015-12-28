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
	/// Summary description for SearchWord.
	/// </summary>
	public class SearchWord : EZWiki.Data.Persistence.ObjectBase
	{
		#region Private Members
		private int _searchWordID;
		private string _word;
		#endregion

		#region Constructors
		public SearchWord()
		{
		}

		public SearchWord(SqlDataReader reader):base(reader)
		{
			// calls inherited constructor
		}
		#endregion

		#region Properties
		public int SearchWordID
		{
			get { return _searchWordID; }
			set { _searchWordID = value; }
		}

		public string Word
		{
			get { return _word; }
			set { _word = value; }
		}
		#endregion

		#region Member Methods
		#endregion

		#region Static Methods
		public static void DeleteAll()
		{
			SqlCommand command = new SqlCommand("SearchWord_DeleteAll", Defaults.DBConnection);
			command.CommandType = CommandType.StoredProcedure;
			command.ExecuteNonQuery();
		}
		#endregion
	}
}
