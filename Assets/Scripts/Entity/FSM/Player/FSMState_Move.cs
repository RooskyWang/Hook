using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMState_Move : ABaseFSMState
{
	public FSMState_Move(AFSMMachine fsmMachine, AEntityBase selfEntity)
		: base(fsmMachine, selfEntity)
	{

	}

	public override EFSMState GetStateType()
	{
		return EFSMState.Move;
	}

	public override void OnEnterState(object data)
	{
		selfEntity.MoveByPath((List<Vector3>)data, MoveEndCallBack);
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
