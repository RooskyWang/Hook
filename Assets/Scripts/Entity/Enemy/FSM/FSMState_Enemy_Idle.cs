using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMState_Enemy_Idle : ABaseFSMState
{
	public FSMState_Enemy_Idle(AFSMMachine fsmMachine, AEntityBase selfEntity)
		: base(fsmMachine, selfEntity)
	{

	}

	public override EFSMState GetStateType()
	{
		return EFSMState.Idle;
	}

	public override void OnEnterState(object data)
	{
		//进入状态，立马寻找玩家
		List<Vector3> path = UnityAStar.Instance.FindPath(this.selfEntity.Position, PlayerMgr.Instance.MainPlayer.Position, true);
		if (path != null)
		{
			fsmMachine.SwitchState(EFSMState.Move, path);
		}
	}

	public override void OnExitState()
	{

	}

	public override void OnUpdateState()
	{

	}

}
