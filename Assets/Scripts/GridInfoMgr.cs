using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

public class GridInfoMgr : Sington<GridInfoMgr>
{

	/// <summary>
	/// 高度图信息
	/// </summary>
	private GridInfo[,] heightMap;

	public bool CheckIsBlock(float line, float rank)
	{
		if (line < 0 || rank < 0 || line >= heightMap.GetLength(0) || rank >= heightMap.GetLength(1))
		{
			return true;
		}
		return heightMap[(int)line, (int)rank].gridType == EGridType.Obstacle;
	}

	public void LoadHeightMap()
	{
		heightMap = XMLFile_MapInfo.LoadXMLFileToMapInfo();
	}

	public void SaveHeightMap()
	{
		XMLFile_MapInfo.SaveMapInfoToXMLFile(heightMap);
	}

}
