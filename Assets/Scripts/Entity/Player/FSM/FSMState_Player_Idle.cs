using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventCls_Player_Attack
{
	public GameObject recvObj;
	public Vector3 dir;
}

public class EventCls_Player_Move
{
	public GameObject recvObj;
	public List<Vector3> path;
}

public class FSMState_Player_Idle : ABaseFSMState
{
	public FSMState_Player_Idle(AFSMMachine fsmMachine, AEntityBase selfEntity)
	: base(fsmMachine, selfEntity)
	{

	}

	public override EFSMState GetStateType()
	{
		return EFSMState.Idle;
	}

	public override void OnEnterState(object data)
	{
		EventMessage.Instance.RegisterEvent<EventCls_Player_Attack>(OnEvent_FireHook);
		EventMessage.Instance.RegisterEvent<EventCls_Player_Move>(OnEvent_Move);
	}

	public override void OnExitState()
	{
		EventMessage.Instance.UnRegisterEventCallBack<EventCls_Player_Attack>(OnEvent_FireHook);
		EventMessage.Instance.UnRegisterEventCallBack<EventCls_Player_Move>(OnEvent_Move);
	}

	public override void OnUpdateState()
	{

	}

	private void OnEvent_FireHook(EventCls_Player_Attack obj)
	{
		if (obj.recvObj != this.selfEntity.selfObj)
			return;

		fsmMachine.SwitchState(EFSMState.Attack, obj.dir);
	}

	private void OnEvent_Move(EventCls_Player_Move obj)
	{
		if (obj.recvObj != this.selfEntity.selfObj)
			return;

		fsmMachine.SwitchState(EFSMState.Move, obj.path);
	}
}
