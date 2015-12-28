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

namespace EZWiki.Data
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public class Area : Persistence.ObjectBase
	{
		#region Private Members
		private int _areaID;
		private string _name;
		private bool _isProduction;
		#endregion

		#region Constructors
		/// <summary>
		/// 
		/// </summary>
		public Area()
		{
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="reader"></param>
		public Area(SqlDataReader reader):base(reader)
		{
			//calls inherited constructor
		}
		#endregion

		#region Properties
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
		public string Name
		{
			get { return _name; }
			set { _name = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public bool IsProduction
		{
			get { return _isProduction; }
			set { _isProduction = value; }
		}
		#endregion

		#region Member Methods
		#endregion

		#region Static Methods
		/// <summary>
		/// 
		/// </summary>
		/// <param name="AreaID"></param>
		/// <returns></returns>
		public static Area GetAreaByAreaID(int AreaID)
		{
			SqlCommand command = new SqlCommand("Area_GetByAreaID", Defaults.DBConnection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add("@AreaID", 1);
			SqlDataReader reader = command.ExecuteReader();
			
			Area toReturn = null;
			if (reader.Read())
			{
				toReturn = new Area(reader);
			}
			reader.Close();
			return toReturn;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public static AreaCollection GetAllAreas()
		{
			SqlCommand command = new SqlCommand("Area_GetAll", Defaults.DBConnection);
			command.CommandType = CommandType.StoredProcedure;
			SqlDataReader reader = command.ExecuteReader();
			
			AreaCollection toReturn = new AreaCollection();
			while (reader.Read())
			{
				Area newObj = new Area(reader);
				toReturn.Add(newObj);
			}
			reader.Close();
			return toReturn;
		}
		#endregion
	}
}
