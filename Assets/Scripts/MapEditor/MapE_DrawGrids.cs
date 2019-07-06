using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapE_DrawGrids : MonoBehaviour
{
	#region 网格信息

	/// <summary>
	/// 网格大小
	/// </summary>
	private int gridSize = 1;
	/// <summary>
	/// 列数量
	/// </summary>
	private int rankCount = 50;
	/// <summary>
	/// 行数量
	/// </summary>
	private int lineCount = 64;

	/// <summary>
	/// 网格偏移高度
	/// </summary>
	private float offsetY = 0.1f;

	/// <summary>
	/// 高度图信息
	/// </summary>
	private GridInfo[,] heightMap;

	#endregion

	/// <summary>
	/// 选中格子大小
	/// </summary>
	[HideInInspector]
	public int selectGridSize = 1;
	/// <summary>
	/// 当前选中格子的X
	/// </summary>
	[HideInInspector]
	public int curSelectPosX;
	/// <summary>
	/// 当前选中格子的Y
	/// </summary>
	[HideInInspector]
	public int curSelectPosY;

	/// <summary>
	/// 焦点目标
	/// </summary>
	private Transform focusTrm;

	/// <summary>
	/// 画线的 材质球
	/// </summary>
	private Material lineMat = null;

	/// <summary>
	/// 可见范围（显示格子）
	/// </summary>
	public int visibleSize = 10;

	private void Start()
	{
		lineMat = CreateLineMaterial();
#if MAP_GRID
		CreateHeightMap();
#else
		LoadHeightMap();
#endif
		//focusTrm = CameraFollowTarget.instance.targetObj.transform;
		focusTrm = transform;
	}

	public void SaveData()
	{
		XMLFile_MapInfo.SaveMapInfoToXMLFile(heightMap);
	}

	public void SetSelectGridsType(EGridType type)
	{
		for (int i = 1 - selectGridSize; i < selectGridSize; i++)
		{
			for (int j = 1 - selectGridSize; j < selectGridSize; j++)
			{
				heightMap[curSelectPosX + i, curSelectPosY + j].gridType = type;
			}
		}
	}

	public bool BCurSelIsBlock()
	{
		return heightMap[curSelectPosX, curSelectPosY].gridType == EGridType.Obstacle;
	}

	public bool CheckIsBlock(float line, float rank)
	{
		if (line < 0 || rank < 0 || line >= lineCount || rank >= rankCount)
		{
			return true;
		}
		return heightMap[(int)line, (int)rank].gridType == EGridType.Obstacle;
	}

	#region 绘制格子

	public void OnRenderObject()
	{
		DrawRedGrids();
		DrawWhiteGrids();
		DrawSelectGrid();
	}

	/// <summary>
	/// 绘制所有红格子
	/// </summary>
	private void DrawRedGrids()
	{
		lineMat.SetPass(0);

		GL.Begin(GL.LINES);

		GL.Color(Color.red);

		int tarX = (int)focusTrm.position.x;
		int tarY = (int)focusTrm.position.z;

		for (int i = 0; i < lineCount; i++)
		{
			for (int j = 0; j < rankCount; j++)
			{
				if (i > tarX - visibleSize && i < tarX + visibleSize && j > tarY - visibleSize && j < tarY + visibleSize)
				{
					if (heightMap[i, j].gridType != EGridType.Obstacle)
					{
						continue;
					}
					DrawOneGrid(i, j);
				}
			}
		}
		GL.End();
	}

	/// <summary>
	/// 绘制所有白格子
	/// </summary>
	private void DrawWhiteGrids()
	{
		lineMat.SetPass(0);

		GL.Begin(GL.LINES);

		GL.Color(Color.white);

		int tarX = (int)focusTrm.position.x;
		int tarY = (int)focusTrm.position.z;

		for (int i = 0; i < lineCount; i++)
		{
			for (int j = 0; j < rankCount; j++)
			{
				if (i > tarX - visibleSize && i < tarX + visibleSize && j > tarY - visibleSize && j < tarY + visibleSize)
				{
					if (heightMap[i, j].gridType != EGridType.Feasible)
					{
						continue;
					}
					DrawOneGrid(i, j);
				}
			}
		}
		GL.End();
	}

	/// <summary>
	/// 绘制当前选择的格子
	/// </summary>
	private void DrawSelectGrid()
	{
		lineMat.SetPass(0);

		GL.Begin(GL.LINES);

		GL.Color(Color.green);

		for (int i = 1 - selectGridSize; i < selectGridSize; i++)
		{
			for (int j = 1 - selectGridSize; j < selectGridSize; j++)
			{
				DrawOneGrid(curSelectPosX + i, curSelectPosY + j);
			}
		}

		GL.End();
	}

	/// <summary>
	/// 绘制一个格子
	/// </summary>
	/// <param name="i"></param>
	/// <param name="j"></param>
	private void DrawOneGrid(int i, int j)
	{
		GL.Vertex3(i * gridSize, offsetY + GetPointHeight(i, j), j * gridSize);
		GL.Vertex3((i + 1) * gridSize, offsetY + GetPointHeight(i + 1, j), j * gridSize);

		GL.Vertex3((i + 1) * gridSize, offsetY + GetPointHeight(i + 1, j), j * gridSize);
		GL.Vertex3((i + 1) * gridSize, offsetY + GetPointHeight(i + 1, j + 1), (j + 1) * gridSize);

		GL.Vertex3((i + 1) * gridSize, offsetY + GetPointHeight(i + 1, j + 1), (j + 1) * gridSize);
		GL.Vertex3(i * gridSize, offsetY + GetPointHeight(i, j + 1), (j + 1) * gridSize);

		GL.Vertex3(i * gridSize, offsetY + GetPointHeight(i, j + 1), (j + 1) * gridSize);
		GL.Vertex3(i * gridSize, offsetY + GetPointHeight(i, j), j * gridSize);
	}

	#endregion

	#region 工具方法

	/// <summary>
	/// 生成地图高度图
	/// </summary>
	private void CreateHeightMap()
	{
		heightMap = new GridInfo[lineCount, rankCount];

		Vector3 origin = Vector3.zero;
		Ray ray = new Ray(origin, Vector3.down);
		RaycastHit hit = default(RaycastHit);
		float heightValue = 0;

		for (int i = 0; i < lineCount; i++)
		{
			for (int j = 0; j < rankCount; j++)
			{
				origin.x = i * gridSize;
				origin.z = j * gridSize;
				ray.origin = origin;
				if (Physics.Raycast(ray, out hit, 500))
				{
					heightValue = hit.point.y;
				}
				else
				{
					heightValue = 0;
				}
				heightMap[i, j].height = heightValue;
				heightMap[i, j].gridType = EGridType.Obstacle;
			}
		}

	}

	private void LoadHeightMap()
	{
		heightMap = XMLFile_MapInfo.LoadXMLFileToMapInfo();
	}

	/// <summary>
	/// 获取点的高度
	/// </summary>
	private float GetPointHeight(int line, int rank)
	{
		if (line < 0 || rank < 0 || line >= lineCount || rank >= rankCount)
		{
			return 0;
		}
		return heightMap[line, rank].height;
	}

	/// <summary>
	/// 画线框用的mat
	/// </summary>
	/// <returns></returns>
	private Material CreateLineMaterial()
	{
		Material mat = new Material(Shader.Find("Lines/Colored Blended"));
		mat.hideFlags = HideFlags.HideAndDontSave;
		mat.shader.hideFlags = HideFlags.HideAndDontSave;

		return mat;
	}
	#endregion

}
