using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : AEntityBase
{
	public static Player instance;

	private GameObject selfObj;

	private FSMMachine_Player fsmMachine;

	private AStarMoveCtroller moveCtrl;

	public void Init(GameObject gameObject)
	{
		moveCtrl = new AStarMoveCtroller();
		instance = this;
		selfObj = gameObject;

		//初始化状态机
		fsmMachine = new FSMMachine_Player(this);
		fsmMachine.SwitchState(EFSMState.Idle, null);
	}

	public void Update()
	{
		moveCtrl.Update();
		fsmMachine.Update();
	}

	public void FireHook(Vector3 dir)
	{
		EventMessage.Instance.DispatchEvent(new EventCls_FireHook() { recvObj = this.selfObj, dir = dir });
	}

	public void Move(List<Vector3> path)
	{
		EventMessage.Instance.DispatchEvent(new EventCls_Move() { recvObj = this.selfObj, path = path });
	}
}
