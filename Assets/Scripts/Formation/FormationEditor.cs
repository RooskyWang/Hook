using UnityEngine;

public class FormationEditor : MonoBehaviour
{
	public GameObject showPrefab;

	public void CreatePos(int count)
	{
		string name = "";
		for (int i = 0; i < count; i++)
		{
			name = "Pos_" + i.ToString("D2");
			if (transform.Find(name) == null)
			{
				GameObject obj = new GameObject();
				obj.transform.parent = transform;
				obj.transform.localPosition = Vector3.zero;
				obj.transform.localScale = Vector3.one;
				obj.transform.localRotation = Quaternion.identity;
				obj.name = name;

				obj.AddComponent<MapE_EnemyInfo>();
			}
		}
	}

	public void CreateShowUnit()
	{
		if (showPrefab == null)
		{
			Debug.LogError("没有设置要展示的单位预设【showPrefab】");
			return;
		}

		DestoryShowUnit();

		foreach (Transform child in transform)
		{
			if (child.name.StartsWith("Pos_"))
			{
				GameObject obj = Instantiate(showPrefab);
				obj.transform.parent = child;
				obj.transform.localPosition = Vector3.zero;
				obj.transform.localScale = Vector3.one;
				obj.transform.localRotation = Quaternion.identity;
			}
		}
	}

	public void DestoryShowUnit()
	{
		foreach (Transform child in transform)
		{
			if (child.name.StartsWith("Pos_"))
			{
				//清空子节点
				foreach (Transform cc in child)
				{
					DestroyImmediate(cc.gameObject);
				}
			}
		}
	}
}
