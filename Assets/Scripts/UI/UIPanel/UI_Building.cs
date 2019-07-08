using UnityEngine;
using UnityEngine.UI;

public class EventCls_BuildClick
{

}

public class UI_Building : AUIBase
{
	private Button btn_Back;
	private Button btn_Build1;
	private Button btn_BuildTower;

	private GameObject curSelObj = null;

	public override void OnDestoryUI()
	{
		EventMessage.Instance.UnRegisterEventCallBack<EventCls_BuildClick>(OnClick_BuildClick);
	}

	public override void OnShowUI()
	{
		btn_Back = uiRoot.transform.Find("Btn_Back").GetComponent<Button>();
		btn_Back.onClick.AddListener(OnClick_Back);
		btn_Build1 = uiRoot.transform.Find("Build_1").GetComponent<Button>();
		btn_Build1.onClick.AddListener(OnClick_BuildNormal);
		btn_BuildTower = uiRoot.transform.Find("Build_2").GetComponent<Button>();
		btn_BuildTower.onClick.AddListener(OnClick_BuildTower);

		EventMessage.Instance.RegisterEvent<EventCls_BuildClick>(OnClick_BuildClick);
	}

	private void OnClick_BuildClick(EventCls_BuildClick obj)
	{
		curSelObj = null;
	}

	private void OnClick_BuildNormal()
	{
		if (curSelObj != null)
			Object.DestroyImmediate(curSelObj);
		GameObject prefab = GlobalRefMgr.Instance.AssetsLoader.SyncLoad_Object<GameObject>("Prefab/Build/Build_1");
		curSelObj = Object.Instantiate(prefab);
		BuildingMgr.Instance.isBuildingMode = true;
		BuildingMgr.Instance.buildObj = curSelObj;
		BuildingMgr.Instance.buildType = 1;
	}

	private void OnClick_BuildTower()
	{
		if (curSelObj != null)
			Object.DestroyImmediate(curSelObj);
		GameObject prefab = GlobalRefMgr.Instance.AssetsLoader.SyncLoad_Object<GameObject>("Prefab/Build/Build_Tower");
		curSelObj = Object.Instantiate(prefab);
		BuildingMgr.Instance.isBuildingMode = true;
		BuildingMgr.Instance.buildObj = curSelObj;
		BuildingMgr.Instance.buildType = 2;
	}

	private void OnClick_Back()
	{
		if (curSelObj != null)
			Object.DestroyImmediate(curSelObj);
		BuildingMgr.Instance.isBuildingMode = false;
		UIMgr.Instance.OpenUI(UIType.GameMain);
	}

	public override void OnUpdate()
	{
		if (curSelObj != null)
		{
			curSelObj.transform.position = new Vector3(InputManager.instance.curSelectPosX, 0, InputManager.instance.curSelectPosY);
		}
	}
}
