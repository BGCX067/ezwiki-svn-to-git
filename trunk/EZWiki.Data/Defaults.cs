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

namespace EZWiki.Data
{
	/// <summary>
	/// Summary description for Defaults.
	/// </summary>
	public class Defaults
	{
		private static SqlConnection _conn = new SqlConnection();

		/// <summary>
		/// 
		/// </summary>
		private Defaults()
		{
		}

		/// <summary>
		/// 
		/// </summary>
		public static string ConnectionString
		{
			get { return "Password=ezwikipass;User ID=ezwikiuser;Initial Catalog=EZWiki;Data Source=c06037"; }
		}

		/// <summary>
		/// 
		/// </summary>
		public static SqlConnection DBConnection
		{
			get 
			{
				_conn.Close();
				if (_conn.State == System.Data.ConnectionState.Closed)
				{
					_conn = new SqlConnection(Defaults.ConnectionString);
					_conn.Open();
				}
				return _conn;
			}
		}

	}
}
