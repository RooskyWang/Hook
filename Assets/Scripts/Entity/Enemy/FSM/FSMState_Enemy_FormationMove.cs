using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMState_Enemy_FormationMove : ABaseFSMState
{
	public FSMState_Enemy_FormationMove(AFSMMachine fsmMachine, AEntityBase selfEntity)
		: base(fsmMachine, selfEntity)
	{

	}

	public override EFSMState GetStateType()
	{
		return EFSMState.FormationMove;
	}

	public override void OnEnterState(object data)
	{
		selfEntity.MoveByPath(new List<Vector3>() { (Vector3)data }, MoveEndCallBack);
	}

	public override void OnExitState()
	{
	}

	public override void OnUpdateState()
	{
	}
	public void MoveEndCallBack()
	{
		fsmMachine.SwitchState(EFSMState.Idle, null);
	}
}
