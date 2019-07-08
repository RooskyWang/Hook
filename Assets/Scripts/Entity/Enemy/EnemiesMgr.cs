using System.Collections.Generic;
using UnityEngine;

public class EnemiesMgr : Sington<EnemiesMgr>
{
	private GameObject enemyPrefab;

	private List<Enemy> enemies = new List<Enemy>();

	public List<Enemy> AllEnemies
	{
		get { return enemies; }
	}

	public void AddEnemy(Enemy enemy)
	{
		enemies.Add(enemy);
	}

	public void KillEnemy(Enemy obj)
	{
		if (obj == null)
			return;

		enemies.Remove(obj);
		obj.Destory();
	}

	public void Update()
	{

	}

	public FormationMgr CreateFormation(Vector3 position, int formationId)
	{
		FormationMgr tm = new FormationMgr();

		GameObject fPrefab = GlobalRefMgr.Instance.AssetsLoader.SyncLoad_Object<GameObject>("Prefab/Formation/Formation_Cube");

		GameObject fObj = Object.Instantiate(fPrefab);
		fObj.transform.position = position;
		fObj.transform.LookAt(PlayerMgr.Instance.MainPlayer.Position, Vector3.up);

		MapE_EnemyInfo[] emInfoArr = fObj.GetComponentsInChildren<MapE_EnemyInfo>();

		for (int i = 0; i < emInfoArr.Length; i++)
		{
			MapE_EnemyInfo info = emInfoArr[i];

			GameObject enemyPrefab = GlobalRefMgr.Instance.AssetsLoader.SyncLoad_Object<GameObject>("Prefab/Enemy/Enemy");
			GameObject enemyObj = Object.Instantiate(enemyPrefab);
			enemyObj.transform.position = info.transform.position;
			enemyObj.transform.rotation = info.transform.rotation;

			Enemy en = new Enemy();
			en.Init(enemyObj);
			tm.AddEnemy(en);
		}

		return tm;
	}
}
