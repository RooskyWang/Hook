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
			new FSMState_Idle(this, player),
			new FSMState_FireHook(this, player),
			new FSMState_Move(this, player),
		};
	}
}
