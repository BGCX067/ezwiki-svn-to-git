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

namespace EZWiki.Data
{
	/// <summary>
	/// Summary description for SectionCollection.
	/// </summary>
	public class AreaCollection : CollectionBase
	{
		/// <summary>
		/// 
		/// </summary>
		public Area this[ int index ]  
		{
			get  
			{
				return( (Area) List[index] );
			}
			set  
			{
				List[index] = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public int Add( Area value )  
		{
			return( List.Add( value ) );
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public int IndexOf( Area value )  
		{
			return( List.IndexOf( value ) );
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="index"></param>
		/// <param name="value"></param>
		public void Insert( int index, Area value )  
		{
			List.Insert( index, value );
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="value"></param>
		public void Remove( Area value )  
		{
			List.Remove( value );
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public bool Contains( Area value )  
		{
			// If value is not of type Int16, this will return false.
			return( List.Contains( value ) );
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="index"></param>
		/// <param name="value"></param>
		protected override void OnInsert( int index, Object value )  
		{
			if ( value.GetType() != Type.GetType("EZWiki.Data.Area") )
				throw new ArgumentException( "value must be of type Area.", "value" );
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="index"></param>
		/// <param name="value"></param>
		protected override void OnRemove( int index, Object value )  
		{
			if ( value.GetType() != Type.GetType("EZWiki.Data.Area") )
				throw new ArgumentException( "value must be of type Area.", "value" );
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="index"></param>
		/// <param name="oldValue"></param>
		/// <param name="newValue"></param>
		protected override void OnSet( int index, Object oldValue, Object newValue )  
		{
			if ( newValue.GetType() != Type.GetType("EZWiki.Data.Area") )
				throw new ArgumentException( "newValue must be of type Area.", "newValue" );
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="value"></param>
		protected override void OnValidate( Object value )  
		{
			if ( value.GetType() != Type.GetType("EZWiki.Data.Area") )
				throw new ArgumentException( "value must be of type Area." );
		}
	}
}
