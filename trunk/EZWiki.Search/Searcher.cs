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

using EZWiki.Data;

namespace EZWiki.Search
{
	/// <summary>
	/// Summary description for Searcher.
	/// </summary>
	public class Searcher
	{
		private Searcher()
		{
		}

		public static TopicCollection PerformSearch(string[] searchTerms, bool SearchOnlyCurrent)
		{
			Hashtable currentlySelectedTopics = new Hashtable();
			Hashtable topicsStillIncluded = new Hashtable();
			bool isFirstRetrieve = true;
			
			foreach (string term in searchTerms)
			{
				topicsStillIncluded.Clear();

				TopicCollection topics = Topic.GetTopicsBySearchWordAndCurrency(term.ToLower(), SearchOnlyCurrent);
				if (isFirstRetrieve)
				{
					foreach (Topic topic in topics)
					{
						currentlySelectedTopics.Add(topic.TopicID, topic);
					}
					isFirstRetrieve = false;
				}

				foreach (Topic topic in topics)
				{
					if (currentlySelectedTopics.Contains(topic.TopicID))
					{
						topicsStillIncluded.Add(topic.TopicID, topic);
					}
				}

				currentlySelectedTopics.Clear();
				
				IDictionaryEnumerator etr = topicsStillIncluded.GetEnumerator();
				while(etr.MoveNext())
					currentlySelectedTopics.Add(etr.Key, etr.Value);

			}

			TopicCollection toReturn = new TopicCollection();
			IDictionaryEnumerator etr2 = topicsStillIncluded.GetEnumerator();
			while(etr2.MoveNext())
			{
				Topic topic = (Topic)etr2.Value;
				toReturn.Add(topic);
			}
			return toReturn;
		}
	}
}
