using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgramMain : MonoBehaviour
{
	public Camera mainCamera;

	void Awake()
	{
		//初始化全局引用
		GlobalRefMgr.Instance.Init(mainCamera);

		//初始化地图信息
		MapInfoMgr.Instance.LoadHeightMap();
		UnityAStar.Instance.InitAStar(MapInfoMgr.Instance.CheckIsBlock);
		WallMgr.Instance.Init();

		//加载玩家
		PlayerMgr.Instance.CreateMainPlayer();
		mainCamera.GetComponent<CameraFollowTarget>().Init(PlayerMgr.Instance.MainPlayer.selfObj);

		//创建一批敌人
		EnemiesMgr.Instance.CreateEnemies();
	}

	void Update()
	{
		PlayerMgr.Instance.Update();
		EnemiesMgr.Instance.Update();
	}
}
