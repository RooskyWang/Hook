using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct BirthInfo
{
	public int x;
	public int y;
}

public struct GridInfo
{
	public GridInfo(float height, EGridType gridType)
	{
		this.height = height;
		this.gridType = gridType;
	}

	public float height;
	public EGridType gridType;
}

public enum EGridType
{
	/// <summary>
	/// 障碍
	/// </summary>
	Obstacle = 0,

	/// <summary>
	/// 可通行
	/// </summary>
	Feasible = 1,

	/// <summary>
	/// 选择的点
	/// </summary>
	Select = 2,
}

public class MapInfo
{
	/// <summary>
	/// 格子信息
	/// </summary>
	public GridInfo[,] heightMap;

	/// <summary>
	/// 出生点
	/// </summary>
	public List<BirthInfo> birthInfoList;
}

public class MapInfoMgr : Sington<MapInfoMgr>
{
	private MapInfo mapInfo;

	public bool CheckIsBlock(float line, float rank)
	{
		if (line < 0 || rank < 0 || line >= mapInfo.heightMap.GetLength(0) || rank >= mapInfo.heightMap.GetLength(1))
		{
			return true;
		}
		return mapInfo.heightMap[(int)line, (int)rank].gridType == EGridType.Obstacle;
	}

	public void LoadHeightMap()
	{
		mapInfo = XMLFile_MapInfo.LoadXMLFileToMapInfo();
	}

	public void SaveHeightMap()
	{
		XMLFile_MapInfo.SaveMapInfoToXMLFile(mapInfo);
	}

}
