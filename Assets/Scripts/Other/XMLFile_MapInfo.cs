using System;
using System.Collections;
using System.Xml;
using UnityEngine;

public class XMLFile_MapInfo
{

	public static void SaveMapInfoToXMLFile(MapInfo mapInfo)
	{
		XmlDocument xmlDoc = new XmlDocument();

		XmlNode root = xmlDoc.CreateElement("Root");

		//保存地图大小信息
		XmlElement size = xmlDoc.CreateElement("MapSize");
		size.SetAttribute("Line", mapInfo.heightMap.GetLength(0).ToString());
		size.SetAttribute("Rank", mapInfo.heightMap.GetLength(1).ToString());
		root.AppendChild(size);

		//保存每个点的信息
		XmlNode mapInfoRoot = xmlDoc.CreateElement("MapInfo");
		for (int i = 0, iMax = mapInfo.heightMap.GetLength(0); i < iMax; i++)
		{
			for (int j = 0, jMax = mapInfo.heightMap.GetLength(1); j < jMax; j++)
			{
				XmlElement xmlEle = xmlDoc.CreateElement("Point");
				xmlEle.SetAttribute("X", i.ToString());
				xmlEle.SetAttribute("Y", j.ToString());
				xmlEle.SetAttribute("gridType", mapInfo.heightMap[i, j].gridType.ToString());
				xmlEle.SetAttribute("height", mapInfo.heightMap[i, j].height.ToString());
				mapInfoRoot.AppendChild(xmlEle);
			}
		}
		root.AppendChild(mapInfoRoot);

		//出生点size
		size = xmlDoc.CreateElement("BirthSize");
		size.SetAttribute("Count", mapInfo.birthInfoList.Count.ToString());
		root.AppendChild(size);

		//出生点信息
		XmlNode birthInfoRoot = xmlDoc.CreateElement("BirthInfo");
		for (int i = 0, iMax = mapInfo.birthInfoList.Count; i < iMax; i++)
		{
			XmlElement xmlEle = xmlDoc.CreateElement("Point");
			xmlEle.SetAttribute("X", mapInfo.birthInfoList[i].x.ToString());
			xmlEle.SetAttribute("Y", mapInfo.birthInfoList[i].y.ToString());
			birthInfoRoot.AppendChild(xmlEle);
		}
		root.AppendChild(birthInfoRoot);

		xmlDoc.AppendChild(root);
		xmlDoc.Save(Application.dataPath.Replace("/Assets", "") + "/mapInfo.xml");
	}

	public static bool HasSceneMapInfo()
	{
		return System.IO.File.Exists(Application.dataPath.Replace("/Assets", "") + "/mapInfo.xml");
	}

	public static MapInfo LoadXMLFileToMapInfo()
	{
		MapInfo result = new MapInfo();

		//加载XML文件
		XmlDocument xmlDoc = new XmlDocument();
		xmlDoc.Load(Application.dataPath.Replace("/Assets", "") + "/mapInfo.xml");

		XmlElement root = (XmlElement)xmlDoc.SelectSingleNode("Root");

		//读取地图大小信息
		XmlElement size = (XmlElement)root.SelectSingleNode("MapSize");
		int line = int.Parse(size.GetAttribute("Line"));
		int rank = int.Parse(size.GetAttribute("Rank"));
		result.heightMap = new GridInfo[line, rank];

		//开始读取每个节点的信息
		XmlNode mapInfoRoot = root.SelectSingleNode("MapInfo");
		XmlNodeList nodeList = mapInfoRoot.SelectNodes("Point");
		foreach (XmlNode item in nodeList)
		{
			XmlElement ele = (XmlElement)item;
			int x = int.Parse(ele.GetAttribute("X"));
			int y = int.Parse(ele.GetAttribute("Y"));
			EGridType type = (EGridType)Enum.Parse(typeof(EGridType), ele.GetAttribute("gridType"));
			float height = float.Parse(ele.GetAttribute("height"));

			result.heightMap[x, y].gridType = type;
			result.heightMap[x, y].height = height;
		}

		//读取出生点size
		size = (XmlElement)root.SelectSingleNode("BirthSize");
		int birthCount = int.Parse(size.GetAttribute("Count"));
		result.birthInfoList = new System.Collections.Generic.List<BirthInfo>(birthCount);

		//读取出生点信息
		XmlNode birthInfoRoot = root.SelectSingleNode("BirthInfo");
		nodeList = birthInfoRoot.SelectNodes("Point");
		foreach (XmlNode item in nodeList)
		{
			XmlElement ele = (XmlElement)item;
			int x = int.Parse(ele.GetAttribute("X"));
			int y = int.Parse(ele.GetAttribute("Y"));

			result.birthInfoList.Add(new BirthInfo() { x = x, y = y });
		}

		return result;
	}

}