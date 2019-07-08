using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMState_Enemy_BCatch : ABaseFSMState
{
	public FSMState_Enemy_BCatch(AFSMMachine fsmMachine, AEntityBase selfEntity)
		: base(fsmMachine, selfEntity)
	{

	}

	public override EFSMState GetStateType()
	{
		return EFSMState.BCatched;
	}

	public override void OnEnterState(object data)
	{
	}

	public override void OnExitState()
	{
	}

	public override void OnUpdateState()
	{
	}
}
