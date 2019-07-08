using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIType
{
	GameMain,
	Building,
}

public class UIMgr : Sington<UIMgr>
{
	private GameObject uiRoot;
	private Transform baseUIRoot;
	private AUIBase curUI;

	public void Init()
	{
		GameObject uiRootPrefab = GlobalRefMgr.Instance.AssetsLoader.SyncLoad_Object<GameObject>("Prefab/UI/UIRoot");
		uiRoot = Object.Instantiate(uiRootPrefab);
		baseUIRoot = uiRoot.transform.Find("BaseUI");
	}

	public void Update()
	{
		if (curUI != null)
			curUI.OnUpdate();
	}

	public void OpenUI(UIType type)
	{
		if (curUI != null)
		{
			curUI.OnDestoryUI();
			curUI.DestoryObj();
		}

		string path = "";
		switch (type)
		{
			case UIType.GameMain:
				path = "Prefab/UI/UI_GameMain";
				curUI = new UI_GameMain();
				break;
			case UIType.Building:
				path = "Prefab/UI/UI_Building";
				curUI = new UI_Building();
				break;
		}
		GameObject prefab = GlobalRefMgr.Instance.AssetsLoader.SyncLoad_Object<GameObject>(path);
		GameObject uiObj = Object.Instantiate(prefab);
		uiObj.transform.parent = baseUIRoot;
		uiObj.transform.localPosition = Vector3.zero;
		curUI.OnUILoaded(uiObj);
		curUI.OnShowUI();
	}
}
