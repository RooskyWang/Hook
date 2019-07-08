using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationMgr : Sington<FormationMgr>
{
	private List<Enemy> enemies = new List<Enemy>();

	public void AddEnemy(Enemy enemy)
	{
		enemies.Add(enemy);
	}

	public void KillEnemy(Enemy enemy)
	{
		if (enemies.Contains(enemy))
		{
			enemies.Remove(enemy);
			enemy.Destory();
		}
	}

}
