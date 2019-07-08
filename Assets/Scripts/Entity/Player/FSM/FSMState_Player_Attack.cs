using UnityEngine;

public class FSMState_Player_Attack : ABaseFSMState
{
	private HookGroup hg;

	public FSMState_Player_Attack(AFSMMachine fsmMachine, AEntityBase selfEntity)
		: base(fsmMachine, selfEntity)
	{
		GameObject hookObj = GlobalRefMgr.Instance.AssetsLoader.SyncLoad_Object<GameObject>("Prefab/Hook");
		hg = new HookGroup(hookObj);
	}

	public override EFSMState GetStateType()
	{
		return EFSMState.Attack;
	}

	public override void OnEnterState(object data)
	{
		hg.StartFire(this.selfEntity.selfTran.position, (Vector3)data, HookEndCallBack);
	}

	public override void OnExitState()
	{

	}

	public override void OnUpdateState()
	{
		hg.Update();
	}

	public void HookEndCallBack(bool isBreak)
	{
		fsmMachine.SwitchState(EFSMState.Idle, null);
	}
}
