using System.Collections.Generic;
using UnityEngine;

public class HookGroup
{
	private enum EState
	{
		None,
		Fire,
		Back,
		End,
	}

	private GameObject prefab;
	private EState state = EState.None;
	private Enemy catchEnemy;
	private static readonly int MAX_HOOK_COUNT = 3;

	private Stack<Hook> hookList = new Stack<Hook>(MAX_HOOK_COUNT);

	private System.Action<bool> endCallBack;

	public HookGroup(GameObject prefab)
	{
		this.prefab = prefab;
	}

	public void Update()
	{
		Hook firstHook = null;
		if (hookList.Count > 0)
		{
			firstHook = hookList.Peek();
		}

		switch (state)
		{
			case EState.None:
				break;
			case EState.Fire:
				{
					// 如果在发射过程中，就延长
					firstHook.IncLength();
					Vector3 firstHookHeadPos = firstHook.GetHookHeadPos();
					CrossWallInfo cwi = null;

					catchEnemy = CatchEnemy(firstHookHeadPos);
					if (catchEnemy != null)
					{
						// 抓到敌人了
						state = EState.Back;
					}
					else if ((cwi = WallMgr.Instance.GetCrossWall(firstHook.GetHookStartPos(), firstHookHeadPos, firstHook.GetHookDir().normalized)) != null)
					{
						// 检测是否撞到墙
						Vector3 curDir = firstHook.GetHookDir();
						Vector3 reflectDir = MathHelper.GetReflectDir(curDir, cwi.crossObj.transform.forward);
						if (hookList.Count < MAX_HOOK_COUNT)
						{
							// 撞墙了，反射
							JoinHook(cwi.crossPos, reflectDir);
						}
						else
						{
							state = EState.Back;
						}
					}
					else if (firstHook.BMaxLength())
					{
						// 到达最大距离
						if (hookList.Count < MAX_HOOK_COUNT)
						{
							JoinHook(firstHookHeadPos, firstHook.GetHookDir());
						}
						else
						{
							state = EState.Back;
						}
					}
					break;
				}
			case EState.Back:
				{
					// 如果在捕获过程中，就回缩
					firstHook.DecLength();
					Vector3 firstHookHeadPos = firstHook.GetHookHeadPos();

					if (catchEnemy != null)
					{
						catchEnemy.Position = firstHookHeadPos;
					}
					else
					{
						catchEnemy = CatchEnemy(firstHookHeadPos);
					}

					if (firstHook.BMinLength())
					{
						// 此节钩子回缩完毕
						firstHook.Destory();
						hookList.Pop();

						if (hookList.Count == 0)
						{
							state = EState.End;
							EnemiesMgr.Instance.KillEnemy(catchEnemy);
						}
					}
					break;
				}
			case EState.End:
				if (endCallBack != null)
				{
					endCallBack(false);
					endCallBack = null;
				}
				state = EState.None;
				break;
			default:
				break;
		}
	}

	public void StartFire(Vector3 pos, Vector3 dir, System.Action<bool> endCallBack)
	{
		if (state != EState.None)
			return;

		this.endCallBack = endCallBack;
		state = EState.Fire;
		JoinHook(pos, dir);
	}

	/// <summary>
	/// 添加一节钩子
	/// </summary>
	/// <param name="pos">钩子的起始位置</param>
	/// <param name="dir">钩子的朝向</param>
	private void JoinHook(Vector3 pos, Vector3 dir)
	{
		GameObject hk = Object.Instantiate(this.prefab);
		Hook h = new Hook();
		h.Init(hk, this);
		h.Fire(pos, dir);

		hookList.Push(h);
	}

	private Enemy CatchEnemy(Vector3 hookPos)
	{
		List<Enemy> enemies = EnemiesMgr.Instance.AllEnemies;
		for (int i = 0; i < enemies.Count; i++)
		{
			if (Vector3.Distance(hookPos, enemies[i].Position) <= 0.5f)
			{
				// 抓到了
				return enemies[i];
			}
		}

		return null;
	}
}
