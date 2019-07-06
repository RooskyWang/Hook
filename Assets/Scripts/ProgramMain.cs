using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgramMain : MonoBehaviour
{
	public Player player;

	void Awake()
	{
		//初始化全局引用
		GlobalRefMgr.Instance.Init();

		//初始化地图信息
		GridInfoMgr.Instance.LoadHeightMap();
		UnityAStar.Instance.InitAStar(GridInfoMgr.Instance.CheckIsBlock);
		WallMgr.Instance.Init();

		//加载玩家
		GameObject obj = GlobalRefMgr.Instance.AssetsLoader.SyncLoad_Object<GameObject>("");
		Player player = new Player();

		//创建一批敌人
		EnemiesMgr.Instance.CreateEnemies();
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			player.FireHook(GetFireDir());
		}
	}

	private Vector3 GetFireDir()
	{
		Vector3 pos;
		Vector3 panelPos = player.Position;
		Vector3 linePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 2));
		Vector3 lineDir = Vector3.Normalize(linePos - Camera.main.transform.position);

		if (MathHelper.GetLineAndPanelCrossPos(panelPos, player.selfTran.up, linePos, lineDir, out pos))
		{
			return Vector3.Normalize(pos - player.selfTran.position);
		}
		return Vector3.zero;
	}
}
