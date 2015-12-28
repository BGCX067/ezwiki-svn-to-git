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


function toggleItem (item)
{
	if (item.className == "Collapsed")
	{
		item.className = "Showing";
	}
	else
	{
		item.className = "Collapsed";
	}
}


function toggleTable (item)
{
	var i;
	for (i=1;i<=item.children[0].children.length-1;i++)
	{
		if (item.children[0].children[i].className == "Collapsed")
		{
			item.children[0].children[i].className = "Showing";
		}
		else
		{
			item.children[0].children[i].className = "Collapsed";
		}
	}
}

function toggleIcon (item)
{
	if (item.nameProp == "minus.gif")
	{
		item.src = "images/plus.gif";
	}
	else
	{
		item.src = "images/minus.gif";
	}
}