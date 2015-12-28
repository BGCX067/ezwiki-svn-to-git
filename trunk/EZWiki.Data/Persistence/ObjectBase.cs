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
using System.Reflection;
using System.Data;
using System.Data.SqlClient;

namespace EZWiki.Data.Persistence
{
	/// <summary>
	/// Summary description for ObjectBase.
	/// </summary>
	public class ObjectBase
	{
		private bool _isPersistent = false;

		/// <summary>
		/// 
		/// </summary>
		public ObjectBase()
		{
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="reader"></param>
		public ObjectBase(SqlDataReader reader)
		{
			Type type = this.GetType();
			PropertyInfo[] properties;
			properties = type.GetProperties();
			foreach (PropertyInfo propertyInfo in properties)
			{
				if (propertyInfo.CanWrite)
				{
					propertyInfo.SetValue(this, reader[propertyInfo.Name], null);
				}
			}
			_isPersistent = true;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public bool Persist()
		{
			Type type = this.GetType();
			string [] objectTypeNameParts = type.Name.Split('.');
			string objectTypeName = objectTypeNameParts[objectTypeNameParts.GetUpperBound(0)];

			string storedProcedureName = objectTypeName + "_Persist";

			SqlCommand command = new SqlCommand(storedProcedureName, Defaults.DBConnection);
			command.CommandType = CommandType.StoredProcedure;
			
			PropertyInfo[] properties;
			properties = type.GetProperties();
			foreach (PropertyInfo propertyInfo in properties)
			{
				if (propertyInfo.CanWrite)
				{
					if (propertyInfo.Name == objectTypeName+"ID")
					{
						if (_isPersistent)
						{
							command.Parameters.Add("@"+propertyInfo.Name, propertyInfo.GetValue(this, null));
						}
					}
					else
					{
						command.Parameters.Add("@"+propertyInfo.Name, propertyInfo.GetValue(this, null));
					}
				}
			}
			
			if (_isPersistent)
			{
				command.ExecuteNonQuery();
			}
			else
			{
				int newID = Convert.ToInt32(command.ExecuteScalar());
				properties = type.GetProperties();
				foreach (PropertyInfo propertyInfo in properties)
				{
					if (propertyInfo.CanWrite)
					{
						if (propertyInfo.Name == objectTypeName+"ID")
						{
							propertyInfo.SetValue(this, newID, null);
						}
					}
				}

			}
			_isPersistent = true;

			return true;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public bool Delete()
		{
			if (_isPersistent)
			{
				Type type = this.GetType();
				string [] objectTypeNameParts = type.Name.Split('.');
				string objectTypeName = objectTypeNameParts[objectTypeNameParts.GetUpperBound(0)];

				string storedProcedureName = objectTypeName + "_Delete";

				SqlCommand command = new SqlCommand(storedProcedureName, Defaults.DBConnection);
				command.CommandType = CommandType.StoredProcedure;
			
				PropertyInfo[] properties;
				properties = type.GetProperties();
				foreach (PropertyInfo propertyInfo in properties)
				{
					if (propertyInfo.CanWrite)
					{
						if (propertyInfo.Name == objectTypeName+"ID")
						{
							command.Parameters.Add("@"+propertyInfo.Name, propertyInfo.GetValue(this, null));
						}
					}
				}

				command.ExecuteNonQuery();
			}

			return true;
		}
	}
}
