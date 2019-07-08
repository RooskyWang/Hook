using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMMachine_Player : AFSMMachine
{
	private AEntityBase player;
	public FSMMachine_Player(AEntityBase player)
	{
		this.player = player;
		base.InitState();
	}

	public override List<ABaseFSMState> GetAllState()
	{
		return new List<ABaseFSMState>()
		{
			new FSMState_Player_Idle(this, player),
			new FSMState_Player_Attack(this, player),
			new FSMState_Player_Move(this, player),
		};
	}
}
