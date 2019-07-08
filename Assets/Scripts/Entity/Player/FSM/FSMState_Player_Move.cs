using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMState_Player_Move : ABaseFSMState
{
	public FSMState_Player_Move(AFSMMachine fsmMachine, AEntityBase selfEntity)
		: base(fsmMachine, selfEntity)
	{

	}

	public override EFSMState GetStateType()
	{
		return EFSMState.Move;
	}

	public override void OnEnterState(object data)
	{
		EventMessage.Instance.RegisterEvent<EventCls_Player_Move>(OnEvent_Move);
		selfEntity.MoveByPath((List<Vector3>)data, MoveEndCallBack);
	}

	public override void OnExitState()
	{
		EventMessage.Instance.UnRegisterEventCallBack<EventCls_Player_Move>(OnEvent_Move);
	}

	public override void OnUpdateState()
	{

	}

	public void MoveEndCallBack()
	{
		fsmMachine.SwitchState(EFSMState.Idle, null);
	}

	private void OnEvent_Move(EventCls_Player_Move obj)
	{
		if (obj.recvObj != this.selfEntity.selfObj)
			return;

		selfEntity.MoveByPath(obj.path, MoveEndCallBack);
	}
}
