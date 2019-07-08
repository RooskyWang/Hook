using System.Collections.Generic;
using UnityEngine;

public class EnemiesMgr : Sington<EnemiesMgr>
{
	private GameObject enemyPrefab;

	private List<Enemy> enemies = new List<Enemy>();

	private List<FormationMgr> forList = new List<FormationMgr>();

	public List<Enemy> AllEnemies
	{
		get { return enemies; }
	}

	public void AddEnemy(Enemy enemy)
	{
		enemies.Add(enemy);
	}

	public void RemoveEnemy(Enemy enemy)
	{
		enemies.Remove(enemy);
	}

	public void KillEnemy(Enemy enemy)
	{
		if (enemy == null)
			return;

		if (enemy.bMainEnemy)
		{
			//杀死全队
			enemy.forMgr.Destory();
			forList.Remove(enemy.forMgr);
		}
		else
		{
			//只杀死这个敌人
			enemy.forMgr.DestoryEnemy(enemy);
		}
	}

	public void Update()
	{
		foreach (var item in forList)
		{
			item.Update();
		}
	}

	public FormationMgr CreateFormation(Vector3 position, int formationId)
	{
		FormationMgr forMgr = new FormationMgr();

		GameObject fPrefab = GlobalRefMgr.Instance.AssetsLoader.SyncLoad_Object<GameObject>("Prefab/Formation/Formation_Cube");

		GameObject fObj = Object.Instantiate(fPrefab);
		fObj.transform.position = position;
		fObj.transform.LookAt(BuildingMgr.Instance.baseBuild.buildPos, Vector3.up);

		//创建敌人
		MapE_EnemyInfo[] emInfoArr = fObj.GetComponentsInChildren<MapE_EnemyInfo>();

		for (int i = 0; i < emInfoArr.Length; i++)
		{
			MapE_EnemyInfo info = emInfoArr[i];

			GameObject enemyPrefab = GlobalRefMgr.Instance.AssetsLoader.SyncLoad_Object<GameObject>("Prefab/Enemy/Enemy");
			GameObject enemyObj = Object.Instantiate(enemyPrefab);
			enemyObj.transform.position = info.transform.position;
			enemyObj.transform.rotation = info.transform.rotation;

			Enemy enemy = new Enemy();
			enemy.Init(enemyObj);
			enemy.forMgr = forMgr;
			enemy.bMainEnemy = info.bMainEnemy;

			//寻找队伍行走路径
			//之后商讨方案吧，目前先按照阵型向前3米
			enemy.MoveDistance(fObj.transform.forward * 3);
			forMgr.AddEnemy(enemy, info.bMainEnemy);
			AddEnemy(enemy);
		}
		forMgr.formationDir = fObj.transform.forward;
		Object.DestroyImmediate(fObj);

		forList.Add(forMgr);
		return forMgr;
	}
}
