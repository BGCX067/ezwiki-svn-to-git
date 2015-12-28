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
using System.Xml;

namespace EZWiki.Data
{
	/// <summary>
	/// Summary description for SSISFile.
	/// </summary>
	public class SSISFile
	{
		private XmlDocument _dtsxDocument = new XmlDocument();

		/// <summary>
		/// 
		/// </summary>
		public SSISFile()
		{
		}

		/// <summary>
		/// 
		/// </summary>
		public void Load()
		{
			_dtsxDocument.Load("C:\\dev\\EZWiki\\EZWiki.Web\\TestPackage.dtsx");
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="DataItemName"></param>
		public void GetDataItemSourceInfo(string DataItemName)
		{
			XmlNamespaceManager nsmgr = new XmlNamespaceManager(_dtsxDocument.NameTable);
			nsmgr.AddNamespace("DTS", "www.microsoft.com/SqlServer/Dts");

			// This finds the External Metadata Column node that helps me start the path of finding where this data goes
			string xPath = "/DTS:Executable/DTS:Executable[@DTS:ExecutableType='DTS.Pipeline.1']/DTS:ObjectData/pipeline/components/component[@componentClassID='{2C0A8BE5-1EDC-4353-A0EF-B778599C65A0}']/outputs/output[@isErrorOut='false']/externalMetadataColumns/externalMetadataColumn[@name='" + DataItemName + "']";
			// Go back and find the Output Column node - look for name change.
			XmlNode externalMetadataColumnNode = _dtsxDocument.SelectSingleNode(xPath, nsmgr);
			
			xPath = "/DTS:Executable/DTS:Executable[@DTS:ExecutableType='DTS.Pipeline.1']/DTS:ObjectData/pipeline/components/component[@componentClassID='{2C0A8BE5-1EDC-4353-A0EF-B778599C65A0}']/outputs/output[@isErrorOut='false']/outputColumns/outputColumn[@externalMetadataColumnId='" + externalMetadataColumnNode.Attributes["id"].Value + "']";
			XmlNode outputColumnNode = _dtsxDocument.SelectSingleNode(xPath, nsmgr);

			// Navigate to parent
			XmlNode outputNode = outputColumnNode.ParentNode.ParentNode;
			XmlNode nextComponentNode = FindNextComponentNode(outputNode, nsmgr);

			// begin recursion
			EvaluateNextNode(nextComponentNode, nsmgr);
		}

		private XmlNode FindNextComponentNode(XmlNode outputNode, XmlNamespaceManager nsmgr)
		{
			string xPath;
			// Find the path node so I can follow it to the next component
			xPath = "/DTS:Executable/DTS:Executable[@DTS:ExecutableType='DTS.Pipeline.1']/DTS:ObjectData/pipeline/paths/path[@startId='" + outputNode.Attributes["id"].Value + "']";
			XmlNode pathNode = _dtsxDocument.SelectSingleNode(xPath, nsmgr);
			if (pathNode == null)  // this means there is no path from this component - the end of the line
			{
				return null;
			}

			xPath = "/DTS:Executable/DTS:Executable[@DTS:ExecutableType='DTS.Pipeline.1']/DTS:ObjectData/pipeline/components/component/inputs/input[@id='" + pathNode.Attributes["endId"].Value + "']";
			XmlNode nextComponentInput = _dtsxDocument.SelectSingleNode(xPath, nsmgr);

			return nextComponentInput.ParentNode.ParentNode;
		}

		private void EvaluateNextNode(XmlNode ThisNode, XmlNamespaceManager nsmgr)
		{
			// Derived Column Task
			if (ThisNode.Attributes["componentClassID"].Value == "{9CF90BF0-5BCC-4C63-B91D-1F322DC12C26}")
			{
			}

			// Lookup Task
			if (ThisNode.Attributes["componentClassID"].Value == "{WHATEVER IT IS}")
			{
			}

			XmlNode outputNode = ThisNode.SelectSingleNode("outputs/output[@isErrorOut='false']", nsmgr);

			XmlNode nextComponentNode = FindNextComponentNode(outputNode, nsmgr);

			if (nextComponentNode != null) // only if we found a "next" component
				EvaluateNextNode(nextComponentNode, nsmgr); // recurse to the next component

		}

		
	}
}
