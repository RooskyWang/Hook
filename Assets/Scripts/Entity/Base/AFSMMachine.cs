using System.Collections.Generic;

public abstract class AFSMMachine
{
	protected Dictionary<EFSMState, ABaseFSMState> stateDic;
	protected ABaseFSMState curState;
	protected EFSMState curStateType;

	protected void InitState()
	{
		List<ABaseFSMState> allState = GetAllState();
		stateDic = new Dictionary<EFSMState, ABaseFSMState>(allState.Count);

		foreach (var item in allState)
		{
			stateDic.Add(item.GetStateType(), item);
		}
	}

	public abstract List<ABaseFSMState> GetAllState();

	public void SwitchState(EFSMState state, object data = null)
	{
		if (stateDic == null || !stateDic.ContainsKey(state) || stateDic[state] == null)
			return;

		//UnityEngine.Debug.LogError("switch to :" + state);
		if (curState != null)
			curState.OnExitState();
		curState = stateDic[state];

		curState.OnEnterState(data);
	}

	public void Update()
	{
		if (curState != null)
		{
			curState.OnUpdateState();
		}
	}
}