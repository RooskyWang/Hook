using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveMgr : Sington<EnemyWaveMgr>
{
	public void NextWave()
	{
		//随机地方阵型

		//随机出生点

		EnemiesMgr.Instance.CreateFormation(new Vector3(14.5f, 0, 13.5f), 0);
	}
}
