using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy
{
	private GameObject rootObj;
	private AStarMoveCtroller moveCtrl;

	public GameObject RootObj { get { return rootObj; } }

	public void Init(GameObject rootObj)
	{
		this.rootObj = rootObj;
	}

	public void Update()
	{

	}

	public void Destory()
	{
		Object.DestroyImmediate(rootObj);
	}

	public Vector3 Position
	{
		get { return rootObj.transform.position; }
		set { rootObj.transform.position = value; }
	}
}
