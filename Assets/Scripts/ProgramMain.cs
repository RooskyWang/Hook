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
		PlayerMgr.Instance.MainPlayer.Position = new Vector3(3.5f, 0, 3.5f);
		mainCamera.GetComponent<CameraFollowTarget>().Init(PlayerMgr.Instance.MainPlayer.selfObj);

		//初始化UI
		UIMgr.Instance.Init();
		UIMgr.Instance.OpenUI(UIType.GameMain);

		//加载基地
		GameObject basePrefab = GlobalRefMgr.Instance.AssetsLoader.SyncLoad_Object<GameObject>("Prefab/Build/Build_BaseTower");
		GameObject baseObj = Instantiate(basePrefab);
		baseObj.transform.position = new Vector3(2, 0, 2);
		BuildingMgr.Instance.AddBaseBuild(baseObj);
	}

	void Update()
	{
		PlayerMgr.Instance.Update();
		EnemiesMgr.Instance.Update();
		UIMgr.Instance.Update();
	}
}
