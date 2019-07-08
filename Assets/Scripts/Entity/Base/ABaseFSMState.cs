using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EFSMState
{
	//玩家状态
	None,
	Idle,
	Attack,
	Move,

	//敌人状态
	FormationMove,
	BCatched,
	Dead,
}

public abstract class ABaseFSMState
{
	protected AEntityBase selfEntity;
	protected AFSMMachine fsmMachine;

	public ABaseFSMState(AFSMMachine fsmMachine, AEntityBase selfEntity)
	{
		this.selfEntity = selfEntity;
		this.fsmMachine = fsmMachine;
	}

	public abstract EFSMState GetStateType();

	public abstract void OnEnterState(object data);

	public abstract void OnUpdateState();

	public abstract void OnExitState();
}
