using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : AEntityBase
{
	public FormationMgr forMgr;

	public bool bMainEnemy;

	public override void Init(GameObject obj)
	{
		base.Init(obj);
	}

	public override void Destory()
	{
		base.Destory();
	}

	public override AFSMMachine GetFSM()
	{
		return new FSMMachine_Enemy(this);
	}

	public void MoveDistance(Vector3 dir)
	{
		fsmMachine.SwitchState(EFSMState.FormationMove, Position + dir);
	}
}
