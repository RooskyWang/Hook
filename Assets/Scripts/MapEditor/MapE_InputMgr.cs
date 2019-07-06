using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapE_InputMgr : MonoBehaviour
{
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

	private MapE_DrawGrids gridsMgr = null;

	private void Awake()
	{
		gridsMgr = GetComponent<MapE_DrawGrids>();
	}

	private void OnGUI()
	{
		GUILayout.Label(string.Format("当前选择的位置: {0} ~ {1} , {2}", gridsMgr.curSelectPosX, gridsMgr.curSelectPosY, gridsMgr.BCurSelIsBlock() ? "障碍" : "可通行"));
	}

	void Update()
	{
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out hit, 500))
		{
			gridsMgr.curSelectPosX = (int)hit.point.x;
			gridsMgr.curSelectPosY = (int)hit.point.z;
		}

		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			gridsMgr.selectGridSize++;
		}

		else if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			gridsMgr.selectGridSize--;
		}

		//左键涂刷可通行
		if (Input.GetMouseButton(0))
		{
			gridsMgr.SetSelectGridsType(EGridType.Feasible);
		}

		//右键涂刷障碍
		if (Input.GetMouseButton(1))
		{
			gridsMgr.SetSelectGridsType(EGridType.Obstacle);
		}

		//设置怪物出生点
		if (Input.GetKeyDown(KeyCode.B))
		{
			gridsMgr.SetBirthPoint();
		}

		//空格保存信息
		if (Input.GetKeyDown(KeyCode.Space))
		{
			gridsMgr.SaveData();
		}
	}
}
