using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AUIBase
{
	protected GameObject uiRoot;

	public void OnUILoaded(GameObject uiRoot)
	{
		this.uiRoot = uiRoot;
	}

	public void DestoryObj()
	{
		Object.DestroyImmediate(uiRoot);
	}

	public abstract void OnShowUI();

	public abstract void OnUpdate();

	public abstract void OnDestoryUI();
}
