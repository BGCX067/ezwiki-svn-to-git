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
using System.Text.RegularExpressions;
using System.Collections;

using EZWiki.Data;

namespace EZWiki.Search
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public class IndexBuilder
	{
		private IndexBuilder()
		{
		}

		public static void BuildIndex()
		{
			SearchWordIndexMember.DeleteAll();
			SearchWord.DeleteAll();

			Hashtable keys = new Hashtable();

			TopicCollection allTopics = Topic.GetAllTopics();
			foreach (Topic topicToIndex in allTopics)
			{
				int wordCount = 0;
				string key = "";
				Regex r = new Regex(@"\s+");
				string wordsOnly = r.Replace(topicToIndex.RenderContentAsWords(), " ");
				string[] wordsOnlyArr = wordsOnly.Split(' ');
				Hashtable indexMembers = new Hashtable();

				foreach (string word in wordsOnlyArr)
				{
					key = word.Trim(' ', '?', '\"', ',', '\'', ';', ':', '.', '(', ')', '[', ']', '|').ToLower();

					SearchWord searchWordToAdd;
					
					if (!keys.Contains(key))
					{
						searchWordToAdd = new SearchWord();
						searchWordToAdd.Word = key;
						searchWordToAdd.Persist();
					
						keys.Add(key, searchWordToAdd);
					}
					else
					{
						searchWordToAdd = (SearchWord)keys[key];
					}

					if (!indexMembers.Contains(searchWordToAdd.SearchWordID.ToString() + topicToIndex.TopicID.ToString()))
					{
						SearchWordIndexMember wordIndexToAdd = new SearchWordIndexMember();
						wordIndexToAdd.SearchWordID = searchWordToAdd.SearchWordID;
						wordIndexToAdd.TopicID = topicToIndex.TopicID;
						wordIndexToAdd.Persist();

						indexMembers.Add(wordIndexToAdd.SearchWordID.ToString() + wordIndexToAdd.TopicID.ToString(), wordIndexToAdd);
					}

					wordCount++;
				}
			}
		}
	}
}
