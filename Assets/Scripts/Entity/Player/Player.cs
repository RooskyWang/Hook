using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : AEntityBase
{
	public override void Init(GameObject gameObject)
	{
		base.Init(gameObject);

		//初始化状态机
		fsmMachine.SwitchState(EFSMState.Idle, null);
	}

	public void FireHook()
	{
		EventMessage.Instance.DispatchEvent(new EventCls_Player_Attack() { recvObj = this.selfObj, dir = GetFireDir() });
	}

	public void Move(List<Vector3> path)
	{
		EventMessage.Instance.DispatchEvent(new EventCls_Player_Move() { recvObj = this.selfObj, path = path });
	}

	private Vector3 GetFireDir()
	{
		Vector3 pos;
		Vector3 panelPos = Position;
		Vector3 linePos = GlobalRefMgr.Instance.mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 2));
		Vector3 lineDir = Vector3.Normalize(linePos - GlobalRefMgr.Instance.mainCamera.transform.position);

		if (MathHelper.GetLineAndPanelCrossPos(panelPos, selfTran.up, linePos, lineDir, out pos))
		{
			return Vector3.Normalize(pos - selfTran.position);
		}
		return Vector3.zero;
	}

	public override AFSMMachine GetFSM()
	{
		return new FSMMachine_Player(this);
	}
}
