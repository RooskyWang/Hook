using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_GameMain : AUIBase
{
	private Button btn_Build;
	private Button btn_Next;

	public override void OnDestoryUI()
	{

	}

	public override void OnShowUI()
	{
		btn_Build = uiRoot.transform.Find("Btn_Build").GetComponent<Button>();
		btn_Build.onClick.AddListener(OnClick_Build);

		btn_Next = uiRoot.transform.Find("Btn_Next").GetComponent<Button>();
		btn_Next.onClick.AddListener(OnClick_Next);
		
	}

	public override void OnUpdate()
	{

	}

	private void OnClick_Build()
	{
		UIMgr.Instance.OpenUI(UIType.Building);
	}

	private void OnClick_Next()
	{

	}
}
