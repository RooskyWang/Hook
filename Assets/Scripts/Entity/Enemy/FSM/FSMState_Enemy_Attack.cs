﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMState_Enemy_Attack : ABaseFSMState
{
	public FSMState_Enemy_Attack(AFSMMachine fsmMachine, AEntityBase selfEntity)
	: base(fsmMachine, selfEntity)
	{

	}

	public override EFSMState GetStateType()
	{
		return EFSMState.Attack;
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
