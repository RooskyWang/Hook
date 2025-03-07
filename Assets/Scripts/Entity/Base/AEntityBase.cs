﻿using System.Collections.Generic;
using UnityEngine;

public abstract class AEntityBase
{
	protected AStarMoveCtroller moveCtrl;
	protected AFSMMachine fsmMachine;
	public GameObject selfObj;
	public Transform selfTran;

	public virtual void Init(GameObject obj)
	{
		if (obj == null)
			throw new System.Exception("GameObject is null..");

		selfObj = obj;
		selfTran = obj.transform;

		moveCtrl = new AStarMoveCtroller();
		moveCtrl.Init(selfTran);

		fsmMachine = GetFSM();
	}

	public abstract AFSMMachine GetFSM();

	public virtual void Update()
	{
		if (moveCtrl != null)
			moveCtrl.Update();

		if (fsmMachine != null)
			fsmMachine.Update();
	}

	public void MoveByPath(List<Vector3> path, System.Action endCallBack)
	{
		if (moveCtrl != null)
			moveCtrl.SetPath(path, endCallBack);
	}

	public virtual void Destory()
	{
		Object.DestroyImmediate(selfObj);
	}

	public Vector3 Position
	{
		get { return selfObj.transform.position; }
		set { selfObj.transform.position = value; }
	}
}
