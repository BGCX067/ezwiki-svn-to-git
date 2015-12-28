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
	/// Summary description for DBConnection.
	/// </summary>
	public class DBConnection : Persistence.ObjectBase
	{
		#region Private Members
		private int _dbConnectionID;
		private string _connectionName;
		private string _serverName;
		private string _databaseName;
		private int _areaID;
		private string _username;
		private string _password;
		#endregion

		#region Constructors
		/// <summary>
		/// 
		/// </summary>
		public DBConnection()
		{
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="reader"></param>
		public DBConnection(SqlDataReader reader):base(reader)
		{
			//calls inherited constructor
		}
		#endregion

		#region Properties
		/// <summary>
		/// 
		/// </summary>
		public int DBConnectionID
		{
			get { return _dbConnectionID; }
			set { _dbConnectionID = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string ConnectionName
		{
			get { return _connectionName; }
			set { _connectionName = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string ServerName
		{
			get { return _serverName; }
			set { _serverName = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string DatabaseName
		{
			get { return _databaseName; }
			set { _databaseName = value; }
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
		public string Username
		{
			get { return _username; }
			set { _username = value; }		
		}

		/// <summary>
		/// 
		/// </summary>
		public string Password
		{
			get { return _password; }
			set { _password = value; }
		}
		#endregion

		#region Member Methods
		/// <summary>
		/// 
		/// </summary>
		/// <param name="QueryText"></param>
		/// <returns></returns>
		public DataTable PerformQuery(string QueryText)
		{
			DataTable toReturn = new DataTable();

			// Only allow stored procedures (One word in QueryText).  This is for avoiding SQL injection.
			if (QueryText.Split(' ').GetUpperBound(0) > 0)
			{
				toReturn = null;
			}
			else
			{

				string connectionString;
				connectionString = "Persist Security Info=False;User ID=" + this.Username + ";Password=" + this.Password + ";Initial Catalog=" + this.DatabaseName + ";Data Source=" + this.ServerName;
				SqlConnection conn = new SqlConnection(connectionString);
				conn.Open();
				SqlCommand command = conn.CreateCommand();
				command.CommandText = "__" + QueryText;
				if (QueryText.Split(' ').GetUpperBound(0) == 0)
					command.CommandType = CommandType.StoredProcedure;

				SqlDataAdapter adapter = new SqlDataAdapter(command);
				try
				{
					adapter.Fill(toReturn);
				}
				catch 
				{
					toReturn = null;
				}
			}
			return toReturn;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="TableName"></param>
		/// <returns></returns>
		public DataTable GetTableInfo(string TableName)
		{
			DataTable toReturn = new DataTable();

			string connectionString;
			connectionString = "Persist Security Info=False;User ID=" + this.Username + ";Password=" + this.Password + ";Initial Catalog=" + this.DatabaseName + ";Data Source=" + this.ServerName;
			SqlConnection conn = new SqlConnection(connectionString);
			conn.Open();
			SqlCommand command = conn.CreateCommand();
			string queryText = string.Empty;
			queryText = "select " +
			"sc.name as Name" +
			",case sc.xtype when '127' then 'bigint' " +
			"when '56' then 'int' " +
			"when '52' then 'smallint' " +
			"when '48' then 'tinyint' " +
			"when '104' then 'bit' " +
			"when '175' then 'char' " +
			"when '239' then 'nchar' " +
			"when '167' then 'varchar' " +
			"when '231' then 'nvarchar' " +
			"when '99' then 'ntext' " +
			"when '61' then 'datetime' " +
			"when '34' then 'image' " +
			"when '58' then 'smalldatetime' " +
			"when '106' then 'decimal' " +
			"when '62' then 'float' " +
			"when '59' then 'real' " +
			"when '108' then 'numeric' " +
			"when '122' then 'small money' " +
			"when '60' then 'money' " +
			"else cast(sc.xtype as varchar(30)) end as [Data Type] " +
			",case sc.xtype " +
			"when '167' then sc.length " +  //varchar
			"when '175' then sc.length " + //char
			"when '239' then sc.length " +  //nchar
			"when '231' then sc.length " + //nvarchar
			"else NULL end as Length " +
			",case sc.xtype when '106' then prec " + //decimal
			"when '62' then prec "  + //float
			"when '106' then prec " + //decimal
			"when '108' then prec " + //numeric
			"when '122' then prec " + //small money
			"when '60' then prec " + //money
			"when '59' then prec " + //real
			"else NULL end as [Precision] " +
			",case sc.xtype when '106' then scale " + //decimal
			"when '62' then scale "  + //float
			"when '106' then scale " + //decimal
			"when '108' then scale " + //numeric
			"when '122' then scale " + //small money
			"when '60' then scale " + //money
			"when '59' then scale " + //real
			"else NULL end as Scale " +
			"from syscolumns sc " +
			"inner join sysobjects so " +
			"on sc.id = so.id " +
			"where so.xtype='U' " +
			"and so.name = '" + TableName + "'";
			command.CommandText = queryText;

			SqlDataAdapter adapter = new SqlDataAdapter(command);
			try
			{
				adapter.Fill(toReturn);
			}
			catch
			{
				toReturn = null;
			}
			return toReturn;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="TableName"></param>
		/// <param name="ColumnName"></param>
		/// <returns></returns>
		public DataTable GetColumnInfo(string TableName, string ColumnName)
		{
			DataTable toReturn = new DataTable();

			string connectionString;
			connectionString = "Persist Security Info=False;User ID=" + this.Username + ";Password=" + this.Password + ";Initial Catalog=" + this.DatabaseName + ";Data Source=" + this.ServerName;
			SqlConnection conn = new SqlConnection(connectionString);
			conn.Open();
			SqlCommand command = conn.CreateCommand();
			string queryText = string.Empty;
			queryText = "select " +
				"sc.name as Name" +
				",case sc.xtype when '127' then 'bigint' " +
				"when '56' then 'int' " +
				"when '52' then 'smallint' " +
				"when '48' then 'tinyint' " +
				"when '104' then 'bit' " +
				"when '175' then 'char' " +
				"when '239' then 'nchar' " +
				"when '167' then 'varchar' " +
				"when '231' then 'nvarchar' " +
				"when '99' then 'ntext' " +
				"when '61' then 'datetime' " +
				"when '34' then 'image' " +
				"when '58' then 'smalldatetime' " +
				"when '106' then 'decimal' " +
				"when '62' then 'float' " +
				"when '59' then 'real' " +
				"when '108' then 'numeric' " +
				"when '122' then 'small money' " +
				"when '60' then 'money' " +
				"else cast(sc.xtype as varchar(30)) end as [Data Type] " +
				",case sc.xtype " +
				"when '167' then sc.length " +  //varchar
				"when '175' then sc.length " + //char
				"when '239' then sc.length " +  //nchar
				"when '231' then sc.length " + //nvarchar
				"else NULL end as Length " +
				",case sc.xtype when '106' then prec " + //decimal
				"when '62' then prec "  + //float
				"when '106' then prec " + //decimal
				"when '108' then prec " + //numeric
				"when '122' then prec " + //small money
				"when '60' then prec " + //money
				"when '59' then prec " + //real
				"else NULL end as [Precision] " +
				",case sc.xtype when '106' then scale " + //decimal
				"when '62' then scale "  + //float
				"when '106' then scale " + //decimal
				"when '108' then scale " + //numeric
				"when '122' then scale " + //small money
				"when '60' then scale " + //money
				"when '59' then scale " + //real
				"else NULL end as Scale " +
				"from syscolumns sc " +
				"inner join sysobjects so " +
				"on sc.id = so.id " +
				"where so.xtype='U' " +
				"and so.name = '" + TableName + "' " + 
				"and sc.name = '" + ColumnName + "'";
			command.CommandText = queryText;

			SqlDataAdapter adapter = new SqlDataAdapter(command);
			try
			{
				adapter.Fill(toReturn);
			}
			catch
			{
				toReturn = null;
			}
			return toReturn;
		}
		#endregion

		#region Static Methods
		/// <summary>
		/// 
		/// </summary>
		/// <param name="ConnectionName"></param>
		/// <param name="Area"></param>
		/// <returns></returns>
		public static DBConnection GetDBConnectionByNameAndArea(string ConnectionName, Area Area)
		{
			SqlCommand command = new SqlCommand("DBConnection_GetByConnectionNameAndAreaID", Defaults.DBConnection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add("@ConnectionName", ConnectionName);
			command.Parameters.Add("@AreaID", Area.AreaID);
			SqlDataReader reader = command.ExecuteReader();

			DBConnection toReturn = null;
			if (reader.Read())
			{
				toReturn = new DBConnection(reader);
			}
			reader.Close();
			return toReturn;
		}
		#endregion
	}
}
