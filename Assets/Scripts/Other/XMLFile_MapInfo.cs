using System;
using System.Collections;
using System.Xml;
using UnityEngine;

public class XMLFile_MapInfo
{

    public static void SaveMapInfoToXMLFile(GridInfo[,] heightMap)
    {
        XmlDocument xmlDoc = new XmlDocument();

        XmlNode root = xmlDoc.CreateElement("Root");

        //保存地图大小信息
        XmlElement size = xmlDoc.CreateElement("MapSize");
        size.SetAttribute("Line", heightMap.GetLength(0).ToString());
        size.SetAttribute("Rank", heightMap.GetLength(1).ToString());
        root.AppendChild(size);

        //保存每个点的信息
        XmlNode mapInfoRoot = xmlDoc.CreateElement("MapInfo");
        for (int i = 0,iMax = heightMap.GetLength(0); i < iMax; i++)
        {
            for (int j = 0,jMax = heightMap.GetLength(1); j < jMax; j++)
            {
                XmlElement xmlEle = xmlDoc.CreateElement("Point");
                xmlEle.SetAttribute("X", i.ToString());
                xmlEle.SetAttribute("Y", j.ToString());
                xmlEle.SetAttribute("gridType", heightMap[i, j].gridType.ToString());
                xmlEle.SetAttribute("height", heightMap[i, j].height.ToString());
                mapInfoRoot.AppendChild(xmlEle);
            }
        }
        root.AppendChild(mapInfoRoot);
        xmlDoc.AppendChild(root);
        xmlDoc.Save(Application.dataPath.Replace("/Assets", "") + "/mapInfo.xml");
    }

	public static bool HasSceneMapInfo()
	{
		return System.IO.File.Exists(Application.dataPath.Replace("/Assets", "") + "/mapInfo.xml");
	}

    public static GridInfo[,] LoadXMLFileToMapInfo()
    {
        //加载XML文件
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(Application.dataPath.Replace("/Assets", "") + "/mapInfo.xml");

        XmlElement root = (XmlElement)xmlDoc.SelectSingleNode("Root");

        //读取地图大小信息
        XmlElement size = (XmlElement)root.SelectSingleNode("MapSize");
        int line = int.Parse(size.GetAttribute("Line"));
        int rank = int.Parse(size.GetAttribute("Rank"));
        GridInfo[,] heightMap = new GridInfo[line, rank];

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

            heightMap[x, y].gridType = type;
            heightMap[x, y].height = height;
        }
        return heightMap;
    }

}