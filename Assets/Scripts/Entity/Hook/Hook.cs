using System.Collections.Generic;
using UnityEngine;

public class Hook
{
	private GameObject rootObj = null;
	private GameObject hookObj = null;
	private Vector3 localScale;
	private Vector3 srcPos;
	private float srcZ;
	private Vector3 startPos;
	private float hookLength;
	private float speed = 20;
	private HookGroup hookGroup;

	public void Init(GameObject rootObj, HookGroup hookGroup)
	{
		this.rootObj = rootObj;
		this.hookGroup = hookGroup;
		this.hookObj = rootObj.transform.Find("Obj").gameObject;
		localScale = new Vector3(0.1f, 0.1f, 0f);
		srcPos = this.hookObj.transform.localPosition;
		srcZ = srcPos.z;
		SetLength(0);
	}

	public void Fire(Vector3 startPos, Vector3 dir)
	{
		if (dir == Vector3.zero)
			return;

		this.startPos = startPos;
		rootObj.transform.position = startPos;
		rootObj.transform.rotation = Quaternion.LookRotation(dir, Vector3.up);

		SetLength(0);
	}

	public void IncLength()
	{
		SetLength(this.hookLength + Time.deltaTime * speed);
	}

	public void DecLength()
	{
		SetLength(this.hookLength - Time.deltaTime * speed);
	}

	private void SetLength(float length)
	{
		this.hookLength = length;
		localScale.z = length;
		srcPos.z = srcZ + length / 2;
		this.hookObj.transform.localScale = localScale;
		this.hookObj.transform.localPosition = srcPos;
	}

	public Vector3 GetHookStartPos()
	{
		return this.startPos;
	}

	public Vector3 GetHookHeadPos()
	{
		return rootObj.transform.position + rootObj.transform.forward * hookLength;
	}

	public Vector3 GetHookDir()
	{
		return rootObj.transform.forward;
	}

	public bool BMaxLength()
	{
		return this.hookLength >= 5;
	}

	public bool BMinLength()
	{
		return this.hookLength <= 0;
	}

	public void Destory()
	{
		Object.DestroyImmediate(this.rootObj);
	}

}