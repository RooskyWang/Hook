using System.Collections.Generic;

public class FSMMachine_Enemy : AFSMMachine
{
	private AEntityBase selfEntity;
	public FSMMachine_Enemy(AEntityBase selfEntity)
	{
		this.selfEntity = selfEntity;
		base.InitState();
	}

	public override List<ABaseFSMState> GetAllState()
	{
		return new List<ABaseFSMState>()
		{
			new FSMState_Enemy_Idle(this, selfEntity),
			new FSMState_Enemy_Move(this, selfEntity),
			new FSMState_Enemy_Attack(this, selfEntity),
			new FSMState_Enemy_FormationMove(this, selfEntity),
			new FSMState_Enemy_BCatch(this, selfEntity),
		};
	}
}
