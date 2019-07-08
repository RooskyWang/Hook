using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationMgr
{
	private List<Enemy> enemies = new List<Enemy>();

	public Vector3 formationDir;

	public Enemy mainEnemy;

	public void AddEnemy(Enemy enemy, bool isMainEnemy)
	{
		enemies.Add(enemy);

		mainEnemy = isMainEnemy ? enemy : null;
	}

	public void DestoryEnemy(Enemy enemy)
	{
		if (enemies.Contains(enemy))
		{
			enemies.Remove(enemy);
			enemy.Destory();
			EnemiesMgr.Instance.RemoveEnemy(enemy);
		}
	}

	public void Destory()
	{
		foreach (var item in enemies)
		{
			EnemiesMgr.Instance.RemoveEnemy(item);
			item.Destory();
		}
	}

	public void Update()
	{
		foreach (var item in enemies)
		{
			item.Update();
		}
	}
}
