using UnityEngine;

public class PlayerMgr : Sington<PlayerMgr>
{
	private Player mainPlayer;

	public Player MainPlayer { get { return mainPlayer; } }

	public void CreateMainPlayer()
	{
		//加载玩家
		GameObject playerPrefab = GlobalRefMgr.Instance.AssetsLoader.SyncLoad_Object<GameObject>("Prefab/Player");
		GameObject playerObj = Object.Instantiate(playerPrefab);
		mainPlayer = new Player();
		mainPlayer.Init(playerObj);
		GlobalRefMgr.Instance.mainCamera.GetComponent<CameraFollowTarget>().Init(playerObj);
	}

	public void Update()
	{
		mainPlayer.Update();
	}
}
