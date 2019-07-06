using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventCls_FireHook
{
	public GameObject recvObj;
	public Vector3 dir;
}

public class EventCls_Move
{
	public GameObject recvObj;
	public List<Vector3> path;
}

public class FSMState_Idle : ABaseFSMState
{
	public FSMState_Idle(AFSMMachine fsmMachine, AEntityBase selfEntity)
	: base(fsmMachine, selfEntity)
	{

	}

	public override EFSMState GetStateType()
	{
		return EFSMState.Idle;
	}

	public override void OnEnterState(object data)
	{
		EventMessage.Instance.RegisterEvent<EventCls_FireHook>(OnEvent_FireHook);
		EventMessage.Instance.RegisterEvent<EventCls_Move>(OnEvent_Move);
	}

	public override void OnExitState()
	{
		EventMessage.Instance.UnRegisterEventCallBack<EventCls_FireHook>(OnEvent_FireHook);
		EventMessage.Instance.UnRegisterEventCallBack<EventCls_Move>(OnEvent_Move);
	}

	public override void OnUpdateState()
	{

	}

	private void OnEvent_FireHook(EventCls_FireHook obj)
	{
		if (obj.recvObj != this.selfEntity.selfObj)
			return;

		fsmMachine.SwitchState(EFSMState.Attack, obj.dir);
	}

	private void OnEvent_Move(EventCls_Move obj)
	{
		if (obj.recvObj != this.selfEntity.selfObj)
			return;

		fsmMachine.SwitchState(EFSMState.Move, obj.path);
	}
}
