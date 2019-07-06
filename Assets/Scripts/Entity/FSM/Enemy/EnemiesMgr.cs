using System.Collections.Generic;
using UnityEngine;

public class EnemiesMgr : Sington<EnemiesMgr>
{
	private GameObject enemyPrefab;

	private List<Enemy> enemies;

	public List<Enemy> AllEnemies
	{
		get { return enemies; }
	}

	public void CreateEnemies()
	{
		enemyPrefab = GlobalRefMgr.Instance.AssetsLoader.SyncLoad_Object<GameObject>("Prefab/Enemy/Enemy");
		// 创建一批敌人
		enemies = new List<Enemy>(10);
		for (int i = 0; i < 10; i++)
		{
			GameObject newObj = Object.Instantiate(enemyPrefab);
			newObj.transform.position = new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
			Enemy em = new Enemy();
			em.Init(newObj);
			enemies.Add(em);
		}
	}

	public void Update()
	{

	}

	public void KillEnemy(Enemy obj)
	{
		if (obj == null)
			return;

		enemies.Remove(obj);
		obj.Destory();
	}

}
